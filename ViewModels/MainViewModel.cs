using AtlantHarden.Models;
using AtlantHarden.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace AtlantHarden.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly HardeningService _hardeningService;
        private readonly BackupService _backupService;
        private bool _isInitialized = false;

        public MainViewModel()
        {
            _hardeningService = new HardeningService();
            _backupService = new BackupService();

            Categories = new ObservableCollection<CategoryInfo>();
            CurrentSettings = new ObservableCollection<HardeningSetting>();
            Backups = new ObservableCollection<BackupInfo>();
            StigProductSummaries = new ObservableCollection<StigProductStat>();

            InitializeCommands();
            InitializeCategories();

            // Start with Dashboard
            CurrentView = "Dashboard";
        }

        /// <summary>
        /// Initialize settings asynchronously with progress reporting (for splash screen)
        /// </summary>
        public async Task InitializeAsync(IProgress<(int current, int total, string message)>? progress = null)
        {
            if (_isInitialized) return;

            try
            {
                // Check all settings including ASR rules
                await _hardeningService.CheckAllStatusAsync(progress);

                // Initialize toggle state to match current applied state (first load only).
                // Skip Bloatware: its selection is curated (clearly-junk pre-checked), and
                // IsApplied for a removal is backwards (installed = not-yet-removed).
                foreach (var setting in _hardeningService.GetAllSettings())
                {
                    if (setting.Category == SettingCategory.Bloatware) continue;
                    setting.IsEnabled = setting.IsApplied;
                }

                // Update UI
                UpdateCategoryStatistics();
                
                _ = LoadBackupsAsync();
                
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during initialization: {ex.Message}");
            }
        }

        #region Properties

        private CategoryInfo? _selectedCategory;
        private string _statusMessage = "Ready";
        private bool _isProcessing;
        private int _progressValue;
        private int _progressMax = 100;
        private string _searchText = "";
        private string _currentView = "Dashboard";
        private bool _isShowingAllSettings;
        private string? _shownProfile;   // null, "Basic", or "Recommended" (Maximum reuses _isShowingAllSettings)
        private int _selectedRiskFilter;
        private bool _isSidebarExpanded = true;
        
        // Imported configuration tracking
        private bool _hasImportedSettings;
        private string _importedProfileName = "";
        private List<string> _importedSettingIds = new List<string>();

        public ObservableCollection<CategoryInfo> Categories { get; }
        public ICollectionView? CategoriesView { get; private set; }
        public ObservableCollection<HardeningSetting> CurrentSettings { get; }
        public ObservableCollection<BackupInfo> Backups { get; }

        /// <summary>Sidebar collapse state. When false, the sidebar shows an icon-only rail.</summary>
        public bool IsSidebarExpanded
        {
            get => _isSidebarExpanded;
            set
            {
                if (SetProperty(ref _isSidebarExpanded, value))
                    OnPropertyChanged(nameof(SidebarWidth));
            }
        }

        /// <summary>Width of the sidebar column: full when expanded, narrow rail when collapsed.</summary>
        public System.Windows.GridLength SidebarWidth =>
            _isSidebarExpanded ? new System.Windows.GridLength(240) : new System.Windows.GridLength(56);

        /// <summary>Per-product DISA STIG compliance summary shown on the dashboard.</summary>
        public ObservableCollection<StigProductStat> StigProductSummaries { get; }

        public CategoryInfo? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (SetProperty(ref _selectedCategory, value))
                {
                    _isShowingAllSettings = false;
                    if (_shownProfile != null)
                    {
                        _shownProfile = null;
                        OnPropertyChanged(nameof(IsShowingRecommended));
                        OnPropertyChanged(nameof(IsShowingBasic));
                    }
                    LoadCategorySettings();
                }
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public bool IsProcessing
        {
            get => _isProcessing;
            set => SetProperty(ref _isProcessing, value);
        }

        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        public int ProgressMax
        {
            get => _progressMax;
            set => SetProperty(ref _progressMax, value);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterSettings();
                }
            }
        }

        public string CurrentView
        {
            get => _currentView;
            set
            {
                if (SetProperty(ref _currentView, value))
                {
                    if (value == "Backups")
                        _ = LoadBackupsAsync();
                    else if (value == "Dashboard")
                        UpdateCategoryStatistics();
                }
            }
        }

        public int SelectedRiskFilter
        {
            get => _selectedRiskFilter;
            set
            {
                if (SetProperty(ref _selectedRiskFilter, value))
                {
                    FilterSettings();
                }
            }
        }

        public bool IsShowingAllSettings
        {
            get => _isShowingAllSettings;
            set => SetProperty(ref _isShowingAllSettings, value);
        }

        public bool IsShowingRecommended => _shownProfile == "Recommended";
        public bool IsShowingBasic => _shownProfile == "Basic";

        // Dashboard Properties
        // Bloatware removal is a separate cleanup action, not a security control, so it is
        // excluded from the security score (removing Candy Crush shouldn't move a security %).
        public int TotalSettingsCount => _hardeningService.GetAllSettings().Count(s => s.Category != SettingCategory.Bloatware);
        public int AppliedSettingsCount => _hardeningService.GetAllSettings().Count(s => s.Category != SettingCategory.Bloatware && s.IsApplied);
        
        public int SecurityScore
        {
            get
            {
                var total = TotalSettingsCount;
                if (total == 0) return 0;
                return (int)Math.Round((double)AppliedSettingsCount / total * 100);
            }
        }

        public string SecurityLevel
        {
            get
            {
                var score = SecurityScore;
                if (score >= 80) return "Excellent Protection";
                if (score >= 60) return "Good Protection";
                if (score >= 40) return "Moderate Protection";
                if (score >= 20) return "Basic Protection";
                return "Minimal Protection";
            }
        }

        // STIG Compliance Properties
        public int STIGSettingsCount => _hardeningService.GetAllSettings().Count(s => s.IsStig);
        public int AppliedSTIGCount => _hardeningService.GetAllSettings().Count(s => s.IsStig && s.IsApplied);
        
        public int STIGComplianceScore
        {
            get
            {
                var total = STIGSettingsCount;
                if (total == 0) return 0;
                return (int)Math.Round((double)AppliedSTIGCount / total * 100);
            }
        }

        public string STIGComplianceLevel
        {
            get
            {
                var score = STIGComplianceScore;
                if (score >= 90) return "Compliant";
                if (score >= 70) return "Mostly Compliant";
                if (score >= 50) return "Partially Compliant";
                return "Non-Compliant";
            }
        }

        // ACSC (Australian Cyber Security Centre) Compliance Properties
        public int ACSCSettingsCount => _hardeningService.GetAllSettings().Count(s => s.IsACSC);
        public int AppliedACSCCount => _hardeningService.GetAllSettings().Count(s => s.IsACSC && s.IsApplied);
        
        public int ACSCComplianceScore
        {
            get
            {
                var total = ACSCSettingsCount;
                if (total == 0) return 0;
                return (int)Math.Round((double)AppliedACSCCount / total * 100);
            }
        }

        public string ACSCComplianceLevel
        {
            get
            {
                var score = ACSCComplianceScore;
                if (score >= 90) return "Compliant";
                if (score >= 70) return "Mostly Compliant";
                if (score >= 50) return "Partially Compliant";
                return "Non-Compliant";
            }
        }

        // Profile Counts
        public int BasicProfileCount => GetBasicProfileSettings().Count;
        public int RecommendedProfileCount => GetRecommendedProfileSettings().Count;
        public int MaximumProfileCount => _hardeningService.GetAllSettings().Count(s => s.Category != SettingCategory.Bloatware);

        // How much of each profile is already in place (applied) vs. still missing.
        public int BasicAppliedCount => GetBasicProfileSettings().Count(s => s.IsApplied);
        public int BasicRemainingCount => BasicProfileCount - BasicAppliedCount;
        public int RecommendedAppliedCount => GetRecommendedProfileSettings().Count(s => s.IsApplied);
        public int RecommendedRemainingCount => RecommendedProfileCount - RecommendedAppliedCount;
        public int MaximumAppliedCount => AppliedSettingsCount;
        public int MaximumRemainingCount => MaximumProfileCount - MaximumAppliedCount;

        // Imported Settings Properties
        public bool HasImportedSettings
        {
            get => _hasImportedSettings;
            set
            {
                if (SetProperty(ref _hasImportedSettings, value))
                {
                    OnPropertyChanged(nameof(QuickActionButtonText));
                    OnPropertyChanged(nameof(QuickActionButtonIcon));
                    OnPropertyChanged(nameof(ImportedSettingsCount));
                }
            }
        }

        public int ImportedSettingsCount => _importedSettingIds.Count;

        public string QuickActionButtonText => HasImportedSettings 
            ? $"Apply Imported ({ImportedSettingsCount})" 
            : "Apply Recommended";

        public string QuickActionButtonIcon => HasImportedSettings ? "📥" : "🚀";

        /// <summary>
        /// Clears the imported settings state
        /// </summary>
        public void ClearImportedSettings()
        {
            _importedSettingIds.Clear();
            _importedProfileName = "";
            HasImportedSettings = false;
        }

        public bool HasNoBackups => Backups.Count == 0;

        // Version and Build Information
        public string VersionInfo => $"Version {GetAppVersion()}";

        private static string GetAppVersion()
        {
            // Read from the assembly so the UI never drifts from the .csproj <Version>.
            var v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return v is null ? "2.0.0" : $"{v.Major}.{v.Minor}.{v.Build}";
        }
        
        public string BuildInfo
        {
            get
            {
                var buildDate = GetBuildDate();
                var buildNumber = GetBuildNumber();
                return $"Build {buildNumber} • {buildDate:MMMM d, yyyy}";
            }
        }
        
        public string CopyrightText => $"© {DateTime.Now.Year} Atlant Security LTD. All rights reserved.";
        
        private DateTime GetBuildDate()
        {
            // Use the executable's timestamp. Assembly.Location is empty in single-file
            // builds, so fall back to the process path (the published .exe).
            try
            {
                var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                if (string.IsNullOrEmpty(location))
                    location = Environment.ProcessPath;
                if (!string.IsNullOrEmpty(location))
                {
                    return System.IO.File.GetLastWriteTime(location);
                }
            }
            catch { }
            return DateTime.Now;
        }
        
        private string GetBuildNumber()
        {
            // Generate build number from date (YYDDD format where DDD is day of year)
            var buildDate = GetBuildDate();
            return $"{buildDate:yy}{buildDate.DayOfYear:D3}";
        }

        #endregion

        #region Commands

        public ICommand ApplySelectedCommand { get; private set; } = null!;
        public ICommand SelectAllCommand { get; private set; } = null!;
        public ICommand DeselectAllCommand { get; private set; } = null!;
        public ICommand RefreshStatusCommand { get; private set; } = null!;
        public ICommand RestoreBackupCommand { get; private set; } = null!;
        public ICommand DeleteBackupCommand { get; private set; } = null!;
        public ICommand OpenBackupFolderCommand { get; private set; } = null!;
        public ICommand ShowDashboardCommand { get; private set; } = null!;
        public ICommand ShowSettingsCommand { get; private set; } = null!;
        public ICommand ShowBackupsCommand { get; private set; } = null!;
        public ICommand ShowAboutCommand { get; private set; } = null!;
        public ICommand SelectCategoryCommand { get; private set; } = null!;
        public ICommand ShowAllSettingsCommand { get; private set; } = null!;
        public ICommand ShowRecommendedCommand { get; private set; } = null!;
        public ICommand ShowProfileCommand { get; private set; } = null!;
        public ICommand ShowComplianceCommand { get; private set; } = null!;
        public ICommand CreateRestorePointCommand { get; private set; } = null!;
        public ICommand OpenAtlantSecurityCommand { get; private set; } = null!;
        public ICommand OpenLicenseContactCommand { get; private set; } = null!;
        public ICommand ToggleSidebarCommand { get; private set; } = null!;
        
        // Profile Commands
        public ICommand ApplyBasicProfileCommand { get; private set; } = null!;
        public ICommand ApplyRecommendedCommand { get; private set; } = null!;
        public ICommand ApplyMaximumProfileCommand { get; private set; } = null!;
        public ICommand TightenPrivacyCommand { get; private set; } = null!;
        public ICommand CleanUpBloatCommand { get; private set; } = null!;
        public ICommand ExportReportCommand { get; private set; } = null!;
        public ICommand OpenSystemRestoreCommand { get; private set; } = null!;
        
        // Configuration Import/Export Commands
        public ICommand ExportConfigurationCommand { get; private set; } = null!;
        public ICommand ImportConfigurationCommand { get; private set; } = null!;

        private void InitializeCommands()
        {
            ApplySelectedCommand = new AsyncRelayCommand(ApplySelectedSettingsAsync, () => !IsProcessing && CurrentSettings.Any(s => s.IsEnabled));
            SelectAllCommand = new RelayCommand(SelectAllSettings);
            DeselectAllCommand = new RelayCommand(DeselectAllSettings);
            RefreshStatusCommand = new AsyncRelayCommand(RefreshStatusAsync, () => !IsProcessing);
            RestoreBackupCommand = new AsyncRelayCommand(p => RestoreBackupAsync(p), p => !IsProcessing);
            DeleteBackupCommand = new AsyncRelayCommand(p => DeleteBackupAsync(p), p => !IsProcessing);
            OpenBackupFolderCommand = new RelayCommand(OpenBackupFolder);
            ShowDashboardCommand = new RelayCommand(() => CurrentView = "Dashboard");
            ShowSettingsCommand = new RelayCommand(() => CurrentView = "Settings");
            ShowBackupsCommand = new RelayCommand(() => CurrentView = "Backups");
            ShowAboutCommand = new RelayCommand(() => CurrentView = "About");
            SelectCategoryCommand = new RelayCommand(p => SelectCategory(p));
            ShowAllSettingsCommand = new RelayCommand(ShowAllSettings);
            ShowRecommendedCommand = new RelayCommand(() => ShowProfileSettings("Recommended"));
            ShowProfileCommand = new RelayCommand(p => ShowProfileSettings(p as string));
            ShowComplianceCommand = new RelayCommand(p => ShowCompliance(p as string));
            CreateRestorePointCommand = new AsyncRelayCommand(CreateRestorePointAsync, () => !IsProcessing);
            OpenAtlantSecurityCommand = new RelayCommand(OpenAtlantSecurityWebsite);
            OpenLicenseContactCommand = new RelayCommand(OpenLicenseContact);
            ToggleSidebarCommand = new RelayCommand(() => IsSidebarExpanded = !IsSidebarExpanded);
            
            ApplyBasicProfileCommand = new AsyncRelayCommand(ApplyBasicProfileAsync, () => !IsProcessing);
            ApplyRecommendedCommand = new AsyncRelayCommand(ApplyRecommendedProfileAsync, () => !IsProcessing);
            ApplyMaximumProfileCommand = new AsyncRelayCommand(ApplyMaximumProfileAsync, () => !IsProcessing);
            TightenPrivacyCommand = new AsyncRelayCommand(TightenPrivacyAsync, () => !IsProcessing);
            CleanUpBloatCommand = new RelayCommand(CleanUpBloat, () => !IsProcessing);
            ExportReportCommand = new RelayCommand(ExportSecurityReport);
            OpenSystemRestoreCommand = new RelayCommand(OpenSystemRestore);
            
            // Configuration Import/Export
            ExportConfigurationCommand = new RelayCommand(ExportConfiguration);
            ImportConfigurationCommand = new RelayCommand(ImportConfigurationFromFile);
        }

        #endregion

        #region Profile Methods

        private List<HardeningSetting> GetBasicProfileSettings()
        {
            // Basic / Essentials: the highest-ROI core with effectively zero friction.
            return _hardeningService.GetAllSettings()
                .Where(RecommendedProfile.IsEssential)
                .ToList();
        }

        private List<HardeningSetting> GetRecommendedProfileSettings()
        {
            // Recommended: the smart default — controls that stop how malware and attackers
            // actually get in and run, while skipping high-friction / low-value lockdowns
            // (disabling password managers, InPrivate, history deletion, Controlled Folder
            // Access, BitLocker pre-boot PIN, FIPS, etc.). See RecommendedProfile for rationale.
            return _hardeningService.GetAllSettings()
                .Where(RecommendedProfile.IsRecommended)
                .ToList();
        }

        private async Task ApplyBasicProfileAsync()
        {
            var settings = GetBasicProfileSettings();
            await ApplyProfileAsync(settings, "Basic");
        }

        private async Task ApplyRecommendedProfileAsync()
        {
            // Check if we have imported settings to apply instead
            if (HasImportedSettings && _importedSettingIds.Count > 0)
            {
                await ApplyImportedSettingsAsync();
                return;
            }
            
            var settings = GetRecommendedProfileSettings();
            await ApplyProfileAsync(settings, "Recommended");
        }

        /// <summary>
        /// Apply imported settings only
        /// </summary>
        private async Task ApplyImportedSettingsAsync()
        {
            var allSettings = _hardeningService.GetAllSettings();
            var importedIds = new HashSet<string>(_importedSettingIds);
            // Never uninstall apps from an imported configuration — bloatware removal is
            // destructive and machine-specific, and only ever happens through interactive,
            // review-first selection in the Bloatware category. A shared/reused config.json
            // must not silently trigger removals the importer never reviewed.
            var settings = allSettings
                .Where(s => importedIds.Contains(s.Id) && s.Category != SettingCategory.Bloatware)
                .ToList();

            if (settings.Count == 0)
            {
                MessageBox.Show("No matching settings found for the imported configuration.",
                    "Import Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                ClearImportedSettings();
                return;
            }

            var result = MessageBox.Show(
                $"Apply Imported Configuration?\n\n" +
                $"Profile: {_importedProfileName}\n" +
                $"Settings: {settings.Count}\n\n" +
                "A backup will be created before making changes.",
                "Apply Imported Settings",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            // Enable the imported settings
            foreach (var setting in settings)
            {
                setting.IsEnabled = true;
            }

            await ApplySettingsAsync(settings);
            UpdateDashboardProperties();

            // Clear the imported state after applying
            ClearImportedSettings();
            
            StatusMessage = $"Imported configuration applied: {settings.Count} settings";
        }

        private async Task ApplyMaximumProfileAsync()
        {
            var highRiskCount = _hardeningService.GetAllSettings().Count(s => s.Category != SettingCategory.Bloatware && s.Risk == RiskLevel.High);
            
            var result = MessageBox.Show(
                "⚠️ MAXIMUM SECURITY PROFILE\n\n" +
                $"This will apply ALL {MaximumProfileCount} settings including {highRiskCount} HIGH-RISK options.\n\n" +
                "HIGH-RISK settings will significantly affect daily use:\n\n" +
                "• Controlled Folder Access - blocks apps from writing to Documents/Desktop\n" +
                "• Office ASR Rules - may break macros, add-ins, and Office workflows\n" +
                "• USB Blocking - blocks portable apps from USB drives\n" +
                "• LOLBin Firewall Rules - blocks admin tools (certutil, wmic, etc.)\n" +
                "• IPv6 Disabled - may break Microsoft services\n" +
                "• DCOM Disabled - breaks remote administration\n" +
                "• SmartScreen hard-block - may block launching this (unsigned) tool again\n\n" +
                "If you ever get locked out, you can revert WITHOUT this tool:\n" +
                "  • double-click the restore_*.reg file in the Backups folder, or\n" +
                "  • use Windows System Restore.\n\n" +
                "Only use this if you understand the implications.\n" +
                "A full backup will be created. Continue?",
                "Confirm Maximum Security",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            // Maximum = all security settings, but NOT bloatware removal (that's a separate,
            // review-first action reached from the Bloatware category / "Clean Up Bloat" button).
            var settings = _hardeningService.GetAllSettings()
                .Where(s => s.Category != SettingCategory.Bloatware)
                .ToList();
            await ApplyProfileAsync(settings, "Maximum");
        }

        private async Task TightenPrivacyAsync()
        {
            // One-click "Tighten Up Privacy": apply every Privacy-category control (telemetry,
            // advertising ID, activity history, tailored experiences, suggested content, etc.).
            // Low-risk and reversible; a backup is still created first.
            var settings = _hardeningService.GetAllSettings()
                .Where(s => s.Category == SettingCategory.Privacy)
                .ToList();
            await ApplyProfileAsync(settings, "Privacy");
        }

        private void CleanUpBloat()
        {
            // Bloat removal is destructive, so nothing is checked at rest. Clicking this button
            // IS the explicit intent to clean up: pre-select the "recommended" items that are
            // still installed (clearly-junk Store apps + detected OEM/AV trials), leave the
            // debatable ones (Xbox, Teams, Phone Link, vendor update tools) unchecked, then route
            // to the Bloatware category so the user reviews, adjusts, and applies.
            var category = Categories.FirstOrDefault(c => c.Category == SettingCategory.Bloatware);
            if (category == null) { CurrentView = "Settings"; return; }

            var bloat = _hardeningService.GetAllSettings()
                .Where(s => s.Category == SettingCategory.Bloatware).ToList();
            foreach (var s in bloat) _hardeningService.CheckCurrentStatus(s);

            // Seed the default selection (still-installed "recommended" junk) only when nothing is
            // already checked. If the user has a selection in progress they've curated this list —
            // don't clobber a deliberate deselection when they re-open Clean Up Bloat.
            if (!bloat.Any(s => s.IsEnabled))
            {
                foreach (var s in bloat)
                    s.IsEnabled = !s.IsApplied && s.Tags.Contains("recommended");
            }
            SelectCategory(category);
        }

        private async Task ApplyProfileAsync(List<HardeningSetting> settings, string profileName)
        {
            // Plain-English summary of what each profile does, so a one-click user knows
            // whether it will protect them or get in their way.
            var blurb = profileName switch
            {
                "Recommended" =>
                    "Applies the controls that stop how malware and attackers actually get in and run:\n" +
                    "  • Attack Surface Reduction + Defender + SmartScreen (anti-malware)\n" +
                    "  • Browser site isolation, exploit & phishing protection\n" +
                    "  • Office macro / Protected View / script-file blocking\n" +
                    "  • Credential-theft protection (LSASS, WDigest, NTLMv2), SMBv1 off\n" +
                    "  • UAC, autorun off, exploit mitigations, security logging\n\n" +
                    "It deliberately SKIPS high-friction lockdowns that don't stop real attacks\n" +
                    "(disabling password managers, InPrivate/Incognito, history deletion,\n" +
                    "Controlled Folder Access, BitLocker pre-boot PIN, FIPS, etc.).\n\n",
                "Basic" =>
                    "The highest-impact, zero-friction core: anti-malware (ASR/Defender/SmartScreen),\n" +
                    "macro & script blocking, credential-theft protection, UAC, and logging.\n\n",
                "Maximum" =>
                    "Applies EVERYTHING, including strict DISA STIG lockdowns that WILL add friction\n" +
                    "(password managers off, InPrivate disabled, Controlled Folder Access, etc.).\n\n",
                "Privacy" =>
                    "Tightens Windows privacy — all reversible and safe for daily use:\n" +
                    "  • Turns off telemetry / diagnostic data and the advertising ID\n" +
                    "  • Disables activity history and app-launch tracking\n" +
                    "  • Turns off tailored experiences and suggested / \"recommended\" content\n" +
                    "    in Start, Settings, and File Explorer\n\n",
                _ => string.Empty
            };

            var result = MessageBox.Show(
                $"Apply {profileName} Security Profile?\n\n" +
                blurb +
                $"This will configure {settings.Count} security settings.\n" +
                "A backup will be created before making changes.",
                $"Apply {profileName} Profile",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            // Clear any imported settings state when applying a standard profile
            if (HasImportedSettings)
            {
                ClearImportedSettings();
            }

            foreach (var setting in settings)
            {
                setting.IsEnabled = true;
            }

            await ApplySettingsAsync(settings);
            UpdateDashboardProperties();
        }

        #endregion

        #region Core Methods

        private void InitializeCategories()
        {
            var allSettings = _hardeningService.GetAllSettings();
            var categoryGroups = allSettings.GroupBy(s => s.Category);

            // Baseline (curated) categories first, then the DISA STIG product categories,
            // so the sidebar reads "Hardening Categories" then "DISA STIG Compliance".
            foreach (var group in categoryGroups
                         .OrderBy(g => CategorySortOrder(g.Key))
                         .ThenBy(g => g.Key.ToString()))
            {
                var info = CategoryInfo.Create(group.Key);
                info.SettingsCount = group.Count();
                info.EnabledCount = group.Count(s => s.IsApplied);
                Categories.Add(info);
            }

            // Grouped, scrollable view for the sidebar (sections by CategoryInfo.Group).
            // Use a dedicated CollectionViewSource (not the shared default view) so grouping
            // does not affect other bindings to Categories (e.g. the dashboard overview grid).
            var sidebarView = new CollectionViewSource { Source = Categories };
            sidebarView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(CategoryInfo.Group)));
            CategoriesView = sidebarView.View;
        }

        private static int CategorySortOrder(SettingCategory c) => c switch
        {
            SettingCategory.StigWindows11 => 101,
            SettingCategory.StigEdge => 102,
            SettingCategory.StigChrome => 103,
            SettingCategory.StigFirefox => 104,
            SettingCategory.StigOffice365 => 105,
            _ => 0
        };

        private void UpdateCategoryStatistics()
        {
            var allSettings = _hardeningService.GetAllSettings();
            
            // Update each setting's status
            foreach (var setting in allSettings)
            {
                _hardeningService.CheckCurrentStatus(setting);
            }

            // Update category counts
            foreach (var category in Categories)
            {
                var categorySettings = allSettings.Where(s => s.Category == category.Category).ToList();
                category.SettingsCount = categorySettings.Count;
                category.EnabledCount = categorySettings.Count(s => s.IsApplied);
            }

            // Notify dashboard properties changed
            OnPropertyChanged(nameof(SecurityScore));
            OnPropertyChanged(nameof(SecurityLevel));
            OnPropertyChanged(nameof(AppliedSettingsCount));
            OnPropertyChanged(nameof(TotalSettingsCount));
            
            // Notify STIG compliance properties changed
            OnPropertyChanged(nameof(STIGSettingsCount));
            OnPropertyChanged(nameof(AppliedSTIGCount));
            OnPropertyChanged(nameof(STIGComplianceScore));
            OnPropertyChanged(nameof(STIGComplianceLevel));
            RefreshStigProductSummaries(allSettings);

            // Notify ACSC compliance properties changed
            OnPropertyChanged(nameof(ACSCSettingsCount));
            OnPropertyChanged(nameof(AppliedACSCCount));
            OnPropertyChanged(nameof(ACSCComplianceScore));
            OnPropertyChanged(nameof(ACSCComplianceLevel));

            NotifyProfileAppliedCounts();
        }

        // Refresh the "X of Y already applied" figures on the dashboard profile cards.
        private void NotifyProfileAppliedCounts()
        {
            OnPropertyChanged(nameof(BasicAppliedCount));
            OnPropertyChanged(nameof(BasicRemainingCount));
            OnPropertyChanged(nameof(RecommendedAppliedCount));
            OnPropertyChanged(nameof(RecommendedRemainingCount));
            OnPropertyChanged(nameof(MaximumAppliedCount));
            OnPropertyChanged(nameof(MaximumRemainingCount));
        }

        private void UpdateDashboardProperties()
        {
            UpdateCategoryStatistics();
        }

        /// <summary>
        /// Rebuilds the per-product STIG compliance breakdown (Windows 11, Edge, Chrome,
        /// Firefox, Office 365) used by the dashboard, from the live applied state.
        /// </summary>
        private void RefreshStigProductSummaries(List<HardeningSetting> allSettings)
        {
            var meta = StigCatalogService.GetMetadata();
            StigProductSummaries.Clear();
            foreach (var product in meta.Products)
            {
                // Catalog rule count is the authoritative denominator; count applied from live state.
                var applied = allSettings.Count(s => s.IsStig && s.StigProduct == product.Product && s.IsApplied);
                StigProductSummaries.Add(new StigProductStat(product.Name, product.StigVersion, applied, product.Rules));
            }
        }

        private void SelectCategory(object? parameter)
        {
            if (parameter is CategoryInfo category)
            {
                _isShowingAllSettings = false;
                SelectedCategory = category;
                CurrentView = "Settings";
            }
        }

        private void ShowAllSettings()
        {
            _isShowingAllSettings = true;
            _shownProfile = null;
            _selectedCategory = null;
            OnPropertyChanged(nameof(SelectedCategory));
            OnPropertyChanged(nameof(IsShowingAllSettings));
            OnPropertyChanged(nameof(IsShowingRecommended));
            OnPropertyChanged(nameof(IsShowingBasic));

            LoadAllSettings();
            CurrentView = "Settings";
        }

        /// <summary>
        /// Dashboard shortcut: open the Settings screen filtered to a compliance framework —
        /// "Stig" (all DISA STIG requirements) or "Acsc" (the ACSC Essential Eight) — since
        /// neither has its own sidebar category.
        /// </summary>
        private void ShowCompliance(string? which)
        {
            ShowAllSettings();                              // all-settings mode + Settings view
            SearchText = string.Empty;
            _selectedRiskFilter = 0;                        // force the next assignment to register as a change
            SelectedRiskFilter = which == "Acsc" ? 5 : 4;   // 5 = ACSC, 4 = DISA STIG → triggers FilterSettings
        }

        // Ordering for every review list: surface what still needs doing — settings that are
        // NOT yet applied first, then by descending criticality (High → Medium → Low), then name.
        // Callers must refresh IsApplied (CheckCurrentStatus) before calling this.
        private static IEnumerable<HardeningSetting> SortForReview(IEnumerable<HardeningSetting> settings) =>
            settings.OrderBy(s => s.IsApplied)
                    .ThenByDescending(s => (int)s.Risk)
                    .ThenBy(s => s.Name);

        /// <summary>
        /// Opens the Settings screen showing the settings for one profile (Basic, Recommended,
        /// or Maximum), so the user can scroll through each one (name, description, what it
        /// changes) before applying.
        /// </summary>
        private void ShowProfileSettings(string? profile)
        {
            if (string.IsNullOrEmpty(profile)) return;
            if (profile == "Maximum") { ShowAllSettings(); return; }   // Maximum = every setting

            _shownProfile = profile;
            _isShowingAllSettings = false;
            _selectedCategory = null;
            SearchText = string.Empty;
            _selectedRiskFilter = 0;
            OnPropertyChanged(nameof(SelectedCategory));
            OnPropertyChanged(nameof(IsShowingAllSettings));
            OnPropertyChanged(nameof(IsShowingRecommended));
            OnPropertyChanged(nameof(IsShowingBasic));
            OnPropertyChanged(nameof(SelectedRiskFilter));

            LoadProfileSettings(profile);
            CurrentView = "Settings";
        }

        private void LoadAllSettings()
        {
            CurrentSettings.Clear();
            // Bloatware removal lives only in its own category (sidebar or the "Clean Up Bloat"
            // button) — keep the destructive uninstalls out of the general "all settings" list.
            var items = _hardeningService.GetAllSettings()
                .Where(s => s.Category != SettingCategory.Bloatware)
                .ToList();
            foreach (var setting in items)
                _hardeningService.CheckCurrentStatus(setting);
            foreach (var setting in SortForReview(items))
                CurrentSettings.Add(setting);
            StatusMessage = $"Showing all {items.Count} settings";
        }

        private static bool InProfile(HardeningSetting s, string profile) => profile switch
        {
            "Basic" => RecommendedProfile.IsEssential(s),
            "Recommended" => RecommendedProfile.IsRecommended(s),
            _ => s.Category != SettingCategory.Bloatware   // Maximum = all security settings, not bloatware cleanup
        };

        private void LoadProfileSettings(string profile)
        {
            CurrentSettings.Clear();
            var items = _hardeningService.GetAllSettings()
                .Where(s => InProfile(s, profile))
                .ToList();
            foreach (var setting in items)
                _hardeningService.CheckCurrentStatus(setting);   // refresh status before sorting
            foreach (var setting in SortForReview(items))
                CurrentSettings.Add(setting);
            StatusMessage = $"Showing {CurrentSettings.Count} {profile} settings";
        }

        /// <summary>Reloads the Settings list for whichever view mode is active.</summary>
        private void RefreshSettingsList()
        {
            if (_shownProfile != null) LoadProfileSettings(_shownProfile);
            else if (_isShowingAllSettings) LoadAllSettings();
            else if (SelectedCategory != null) LoadCategorySettings();
        }

        private void LoadCategorySettings()
        {
            if (SelectedCategory == null) return;

            CurrentSettings.Clear();
            var settings = _hardeningService.GetSettingsByCategory(SelectedCategory.Category).ToList();
            foreach (var setting in settings)
                _hardeningService.CheckCurrentStatus(setting);
            foreach (var setting in SortForReview(settings))
                CurrentSettings.Add(setting);

            StatusMessage = $"Loaded {settings.Count} settings for {SelectedCategory.Name}";
        }

        private void FilterSettings()
        {
            IEnumerable<HardeningSetting> baseSettings;

            if (string.IsNullOrWhiteSpace(SearchText) && SelectedRiskFilter == 0)
            {
                RefreshSettingsList();
                return;
            }

            baseSettings = _shownProfile != null
                ? _hardeningService.GetAllSettings().Where(s => InProfile(s, _shownProfile))
                : _isShowingAllSettings
                    // Keep bloatware/AppRemoval out of "All Settings" even when a search or risk
                    // filter is active — same exclusion LoadAllSettings() applies — so a user can't
                    // Select All + Apply destructive uninstalls from the general settings screen.
                    ? _hardeningService.GetAllSettings().Where(s => s.Category != SettingCategory.Bloatware)
                    : (SelectedCategory != null
                        ? _hardeningService.GetSettingsByCategory(SelectedCategory.Category)
                        : _hardeningService.GetAllSettings().Where(s => s.Category != SettingCategory.Bloatware));

            var filtered = baseSettings;

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(s =>
                    s.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    s.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    s.Tags.Any(t => t.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) ||
                    s.Category.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            // Apply risk/compliance filter
            // 0=All, 1=Low, 2=Medium, 3=High, 4=STIG, 5=ACSC,
            // 6=STIG CAT I (High), 7=STIG CAT II (Medium), 8=STIG CAT III (Low)
            switch (SelectedRiskFilter)
            {
                case 1: filtered = filtered.Where(s => s.Risk == RiskLevel.Low); break;
                case 2: filtered = filtered.Where(s => s.Risk == RiskLevel.Medium); break;
                case 3: filtered = filtered.Where(s => s.Risk == RiskLevel.High); break;
                case 4: filtered = filtered.Where(s => s.IsStig); break;
                case 5: filtered = filtered.Where(s => s.IsACSC); break;
                case 6: filtered = filtered.Where(s => s.IsStig && s.Risk == RiskLevel.High); break;
                case 7: filtered = filtered.Where(s => s.IsStig && s.Risk == RiskLevel.Medium); break;
                case 8: filtered = filtered.Where(s => s.IsStig && s.Risk == RiskLevel.Low); break;
            }

            var filteredList = filtered.ToList();
            foreach (var setting in filteredList)
                _hardeningService.CheckCurrentStatus(setting);   // refresh status before sorting
            CurrentSettings.Clear();
            foreach (var setting in SortForReview(filteredList))
                CurrentSettings.Add(setting);

            StatusMessage = $"Found {filteredList.Count} settings";
        }

        private void SelectAllSettings()
        {
            foreach (var setting in CurrentSettings)
                setting.IsEnabled = true;
        }

        private void DeselectAllSettings()
        {
            foreach (var setting in CurrentSettings)
                setting.IsEnabled = false;
        }

        private async Task ApplySelectedSettingsAsync()
        {
            // Get ALL settings across ALL categories that have IsEnabled toggled (either ON to apply, or OFF to disable)
            // This ensures settings modified in other categories are not lost when switching views
            var allSettings = _hardeningService.GetAllSettings();
            var settingsToApply = allSettings.Where(s => s.IsEnabled && !s.IsApplied).ToList();
            // Bloatware removal isn't a reversible toggle — you can't "un-remove" an app — so an
            // already-removed bloat item that's unchecked must NOT be treated as "disable/revert"
            // (otherwise it would show up as a change to revert on every single Apply).
            var settingsToDisable = allSettings.Where(s => !s.IsEnabled && s.IsApplied
                                                           && s.Category != SettingCategory.Bloatware).ToList();
            
            int totalChanges = settingsToApply.Count + settingsToDisable.Count;

            if (totalChanges == 0)
            {
                MessageBox.Show("No changes to apply.\n\nToggle settings ON to enable them, or OFF to disable them.", 
                    "No Changes", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var message = new StringBuilder();
            message.AppendLine($"Changes to apply:");
            if (settingsToApply.Count > 0)
                message.AppendLine($"  • Enable {settingsToApply.Count} settings");
            if (settingsToDisable.Count > 0)
                message.AppendLine($"  • Disable {settingsToDisable.Count} settings");
            message.AppendLine();
            message.AppendLine("A backup will be created first. Continue?");

            var result = MessageBox.Show(message.ToString(), "Confirm Apply", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            await ApplySettingsWithStateAsync(settingsToApply, settingsToDisable);
        }

        private async Task ApplySettingsWithStateAsync(List<HardeningSetting> toEnable, List<HardeningSetting> toDisable)
        {
            IsProcessing = true;
            ProgressValue = 0;
            ProgressMax = toEnable.Count + toDisable.Count;

            var errors = new List<string>();

            try
            {
                // Create backup for settings being changed
                var allChangedSettings = toEnable.Concat(toDisable).ToList();
                if (allChangedSettings.Any())
                {
                    StatusMessage = "Creating backup...";
                    var backup = await _backupService.CreateBackupAsync(allChangedSettings);
                    Backups.Insert(0, backup);
                    OnPropertyChanged(nameof(HasNoBackups));
                }

                // Allow-list this (unsigned) tool for ASR / Controlled Folder Access BEFORE we
                // enable any settings, so the hardening can't lock the tool out of running or
                // writing backups later. Only matters when enabling protections.
                if (toEnable.Any())
                {
                    StatusMessage = "Protecting tool access...";
                    await SelfProtectionService.EnsureToolAllowlistedAsync();
                }

                int successCount = 0;
                int failCount = 0;
                var succeeded = new List<HardeningSetting>();

                // Enable settings
                foreach (var setting in toEnable)
                {
                    StatusMessage = $"Enabling: {setting.Name}";
                    
                    try
                    {
                        var success = await _hardeningService.ApplyOrRevertSettingAsync(setting, true);
                        if (success)
                        {
                            setting.IsApplied = true;
                            successCount++;
                            succeeded.Add(setting);
                        }
                        else
                        {
                            failCount++;
                            errors.Add($"❌ {setting.Name}: Failed to apply");
                        }
                    }
                    catch (Exception ex)
                    {
                        failCount++;
                        errors.Add($"❌ {setting.Name}: {ex.Message}");
                    }

                    ProgressValue++;
                    await Task.Yield();
                }

                // Disable settings
                foreach (var setting in toDisable)
                {
                    StatusMessage = $"Disabling: {setting.Name}";
                    
                    try
                    {
                        var success = await _hardeningService.ApplyOrRevertSettingAsync(setting, false);
                        if (success)
                        {
                            setting.IsApplied = false;
                            successCount++;
                            succeeded.Add(setting);
                        }
                        else
                        {
                            failCount++;
                            errors.Add($"❌ {setting.Name}: Failed to revert");
                        }
                    }
                    catch (Exception ex)
                    {
                        failCount++;
                        errors.Add($"❌ {setting.Name}: {ex.Message}");
                    }

                    ProgressValue++;
                    await Task.Yield();
                }

                // Refresh status to verify changes - force ASR refresh
                StatusMessage = "Verifying changes...";
                
                // If any ASR settings were changed, force refresh ASR status from Windows Defender
                var hasAsrChanges = toEnable.Concat(toDisable).Any(s => !string.IsNullOrEmpty(s.ASRGuid));
                if (hasAsrChanges)
                {
                    StatusMessage = "Verifying ASR rule changes...";
                    await _hardeningService.RefreshASRStatusAsync();
                }
                
                await _hardeningService.CheckAllStatusAsync();
                
                // Refresh the current view
                RefreshSettingsList();

                UpdateDashboardProperties();

                // Build result message. "Applied" = writes/commands that succeeded. Some settings
                // don't read back as active immediately — they need a reboot to take effect, or have
                // no auto-readable state — so report those honestly rather than as a lower count that
                // looks like a failure.
                int totalSelected = toEnable.Count + toDisable.Count;
                var pending = succeeded
                    .Where(s => toDisable.Contains(s) ? s.IsApplied : !s.IsApplied)
                    .ToList();

                var sb = new StringBuilder();
                sb.AppendLine($"✓ Applied {succeeded.Count} of {totalSelected} settings");

                if (pending.Count > 0)
                {
                    sb.AppendLine();
                    sb.AppendLine($"ℹ {pending.Count} applied but not auto-confirmed (they ran OK; some need a restart to take effect):");
                    foreach (var s in pending.Take(12))
                        sb.AppendLine($"   • {s.Name}");
                    if (pending.Count > 12)
                        sb.AppendLine($"   …and {pending.Count - 12} more");
                }

                if (failCount > 0)
                {
                    sb.AppendLine();
                    sb.AppendLine($"✗ {failCount} failed:");
                    foreach (var error in errors.Take(10))
                        sb.AppendLine(error);
                    if (errors.Count > 10)
                        sb.AppendLine($"…and {errors.Count - 10} more");
                }

                sb.AppendLine();
                sb.AppendLine("💾 Backup created in Backups folder");

                StatusMessage = $"Complete: {succeeded.Count} of {totalSelected} applied";
                
                MessageBox.Show(sb.ToString(), "Apply Complete", MessageBoxButton.OK, 
                    failCount > 0 ? MessageBoxImage.Warning : MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsProcessing = false;
            }
        }

        private async Task ApplySettingsAsync(List<HardeningSetting> settings)
        {
            var toEnable = settings.Where(s => s.IsEnabled).ToList();
            await ApplySettingsWithStateAsync(toEnable, new List<HardeningSetting>());
        }

        private async Task LoadBackupsAsync()
        {
            try
            {
                var backups = await _backupService.GetAllBackupsAsync();
                Backups.Clear();
                foreach (var backup in backups)
                    Backups.Add(backup);
                OnPropertyChanged(nameof(HasNoBackups));
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error loading backups: {ex.Message}";
            }
        }

        private async Task RestoreBackupAsync(object? parameter)
        {
            if (parameter is not BackupInfo backup) return;

            if (backup.Status == BackupStatus.Corrupted)
            {
                MessageBox.Show("This backup is corrupted.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show(
                $"Restore backup from {backup.CreatedAt:MMM d, yyyy h:mm tt}?\n\n" +
                $"This will revert {backup.SettingsCount} registry entries to their original values.\n\n" +
                "Note: Some changes may require a restart to take effect.",
                "Confirm Restore",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            IsProcessing = true;
            StatusMessage = "Restoring backup...";

            try
            {
                var (success, restored, failed, errors) = await _backupService.RestoreBackupAsync(backup.FilePath);
                
                var message = $"Restored {restored} settings.";
                if (failed > 0)
                {
                    message += $"\n{failed} settings failed to restore.";
                    if (errors.Any())
                    {
                        message += "\n\nErrors:\n" + string.Join("\n", errors.Take(5));
                        if (errors.Count > 5)
                            message += $"\n...and {errors.Count - 5} more";
                    }
                }
                message += "\n\nSome changes may require a restart.";

                StatusMessage = success ? "Backup restored successfully" : "Restore completed with errors";
                MessageBox.Show(message, success ? "Restore Complete" : "Restore Completed with Issues", 
                    MessageBoxButton.OK, success ? MessageBoxImage.Information : MessageBoxImage.Warning);
                
                UpdateDashboardProperties();
                RefreshSettingsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsProcessing = false;
            }
        }

        private async Task DeleteBackupAsync(object? parameter)
        {
            if (parameter is not BackupInfo backup) return;

            var result = MessageBox.Show(
                $"Delete backup from {backup.CreatedAt:MMM d, yyyy}?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            var success = await _backupService.DeleteBackupAsync(backup.FilePath);
            if (success)
            {
                Backups.Remove(backup);
                OnPropertyChanged(nameof(HasNoBackups));
                StatusMessage = "Backup deleted";
            }
        }

        private async Task CreateRestorePointAsync()
        {
            IsProcessing = true;
            StatusMessage = "Creating system restore point...";

            try
            {
                var (success, message) = await _backupService.CreateSystemRestorePointAsync(
                    $"AtlantHarden - {DateTime.Now:yyyy-MM-dd HH:mm}");

                StatusMessage = message;

                MessageBox.Show(success 
                    ? "System restore point created successfully!" 
                    : $"Could not create restore point:\n\n{message}",
                    success ? "Success" : "Failed",
                    MessageBoxButton.OK,
                    success ? MessageBoxImage.Information : MessageBoxImage.Warning);
            }
            finally
            {
                IsProcessing = false;
            }
        }

        private void OpenBackupFolder()
        {
            try
            {
                var path = _backupService.BackupDirectory;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                Process.Start("explorer.exe", path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open folder: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenSystemRestore()
        {
            _backupService.OpenSystemRestoreDialog();
        }

        private void OpenAtlantSecurityWebsite()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://atlantsecurity.com",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open website: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenLicenseContact()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://atlantsecurity.com/contact",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the contact form: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task RefreshStatusAsync()
        {
            IsProcessing = true;
            StatusMessage = "Refreshing status...";

            try
            {
                // Force refresh ASR rules from Windows Defender
                StatusMessage = "Querying ASR rules...";
                await _hardeningService.RefreshASRStatusAsync();
                
                // Refresh all other settings
                StatusMessage = "Checking registry settings...";
                await _hardeningService.CheckAllStatusAsync();
                
                // Reload current view
                RefreshSettingsList();

                UpdateDashboardProperties();
                StatusMessage = $"Status refreshed for {CurrentSettings.Count} settings";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Refresh failed: {ex.Message}";
            }
            finally
            {
                IsProcessing = false;
            }
        }

        private void ExportSecurityReport()
        {
            try
            {
                var allSettings = _hardeningService.GetAllSettings();
                foreach (var s in allSettings)
                    _hardeningService.CheckCurrentStatus(s);

                var html = GenerateHtmlReport(allSettings);
                
                var fileName = $"SecurityReport_{DateTime.Now:yyyyMMdd_HHmmss}.html";
                var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
                
                File.WriteAllText(filePath, html);
                
                Process.Start(new ProcessStartInfo { FileName = filePath, UseShellExecute = true });
                
                StatusMessage = $"Report exported to Desktop";
                MessageBox.Show($"Report saved to:\n{filePath}", "Export Complete", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GenerateHtmlReport(List<HardeningSetting> settings)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html><html><head>");
            sb.AppendLine("<title>AtlantHarden Report</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("body{font-family:'Segoe UI',sans-serif;background:#0d1117;color:#f0f6fc;margin:40px;}");
            sb.AppendLine("h1{color:#58a6ff;} h2{color:#8b949e;border-bottom:1px solid #21262d;padding-bottom:10px;}");
            sb.AppendLine(".score{font-size:48px;font-weight:bold;color:#238636;}");
            sb.AppendLine(".card{background:#161b22;border:1px solid #21262d;border-radius:8px;padding:20px;margin:16px 0;}");
            sb.AppendLine(".applied{color:#238636;} .not-applied{color:#da3633;}");
            sb.AppendLine("table{width:100%;border-collapse:collapse;margin:20px 0;}");
            sb.AppendLine("th,td{text-align:left;padding:12px;border-bottom:1px solid #21262d;}");
            sb.AppendLine("th{color:#8b949e;font-weight:normal;}");
            sb.AppendLine(".badge{display:inline-block;padding:2px 8px;border-radius:4px;font-size:12px;}");
            sb.AppendLine(".low{background:#1a3d1a;color:#238636;} .medium{background:#3d3d1a;color:#d29922;}");
            sb.AppendLine(".high{background:#3d1a1a;color:#da3633;} .critical{background:#4d1a1a;color:#ff6b6b;}");
            sb.AppendLine("</style></head><body>");
            
            sb.AppendLine($"<h1>🛡️ AtlantHarden Report</h1>");
            sb.AppendLine($"<p>Generated: {DateTime.Now:MMMM d, yyyy h:mm tt}</p>");
            sb.AppendLine($"<p>Computer: {Environment.MachineName}</p>");
            
            sb.AppendLine("<div class='card'>");
            sb.AppendLine($"<div class='score'>{SecurityScore}/100</div>");
            sb.AppendLine($"<p>{SecurityLevel}</p>");
            sb.AppendLine($"<p>{AppliedSettingsCount} of {TotalSettingsCount} settings applied</p>");
            sb.AppendLine("</div>");

            var groups = settings.GroupBy(s => s.Category).OrderBy(g => g.Key.ToString());
            
            foreach (var group in groups)
            {
                var applied = group.Count(s => s.IsApplied);
                var total = group.Count();
                
                sb.AppendLine($"<h2>{group.Key} ({applied}/{total})</h2>");
                sb.AppendLine("<table><tr><th>Setting</th><th>Risk</th><th>Status</th><th>Current Value</th></tr>");
                
                foreach (var setting in group)
                {
                    var status = setting.IsApplied ? "<span class='applied'>✓ Applied</span>" : "<span class='not-applied'>✗ Not Applied</span>";
                    var risk = $"<span class='badge {setting.Risk.ToString().ToLower()}'>{setting.Risk}</span>";
                    
                    // HTML-encode setting-supplied text — some of it (e.g. an OEM app's registry
                    // DisplayName) is not fully under our control, so it must not inject markup
                    // into the auto-opened report.
                    var name = System.Net.WebUtility.HtmlEncode(setting.Name);
                    var desc = System.Net.WebUtility.HtmlEncode(setting.Description);
                    var cur = System.Net.WebUtility.HtmlEncode(setting.CurrentValue);
                    sb.AppendLine($"<tr><td><strong>{name}</strong><br/><small style='color:#8b949e'>{desc}</small></td>");
                    sb.AppendLine($"<td>{risk}</td><td>{status}</td><td><code>{cur}</code></td></tr>");
                }
                
                sb.AppendLine("</table>");
            }

            sb.AppendLine($"<p style='margin-top:40px;color:#6e7681;'>© {DateTime.Now.Year} Atlant Security LTD | https://atlantsecurity.com</p>");
            sb.AppendLine("</body></html>");
            
            return sb.ToString();
        }

        #endregion

        #region Configuration Import/Export

        /// <summary>
        /// Export current configuration to a JSON file
        /// </summary>
        private void ExportConfiguration()
        {
            try
            {
                var dialog = new Microsoft.Win32.SaveFileDialog
                {
                    Title = "Export Configuration",
                    Filter = "JSON Configuration (*.json)|*.json|All Files (*.*)|*.*",
                    DefaultExt = ".json",
                    FileName = $"AtlantHarden_Config_{DateTime.Now:yyyyMMdd_HHmmss}.json",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };

                if (dialog.ShowDialog() == true)
                {
                    var allSettings = _hardeningService.GetAllSettings();
                    var enabledCount = allSettings.Count(s => s.IsEnabled);

                    if (enabledCount == 0)
                    {
                        MessageBox.Show("No settings are currently enabled. Please select settings to include in the configuration.",
                            "No Settings Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Ask for profile name
                    var profileName = "Custom Profile";
                    
                    if (ConfigurationService.ExportConfiguration(allSettings, dialog.FileName, profileName))
                    {
                        StatusMessage = $"Configuration exported: {enabledCount} settings";
                        MessageBox.Show($"Configuration exported successfully!\n\nFile: {dialog.FileName}\nSettings: {enabledCount}",
                            "Export Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to export configuration.", "Export Error", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting configuration: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Import configuration from a JSON file
        /// </summary>
        private void ImportConfigurationFromFile()
        {
            try
            {
                var dialog = new Microsoft.Win32.OpenFileDialog
                {
                    Title = "Import Configuration",
                    Filter = "JSON Configuration (*.json)|*.json|All Files (*.*)|*.*",
                    DefaultExt = ".json",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };

                if (dialog.ShowDialog() == true)
                {
                    var (isValid, message, profile) = ConfigurationService.ValidateConfiguration(dialog.FileName);
                    
                    if (!isValid || profile == null)
                    {
                        MessageBox.Show($"Invalid configuration file:\n{message}", "Import Error", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Show confirmation with profile details
                    var summary = ConfigurationService.GetProfileSummary(profile);
                    var result = MessageBox.Show(
                        $"Import this configuration?\n\n{summary}\n\nThis will update your current setting selections (not apply them).",
                        "Confirm Import", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        ImportConfiguration(profile);
                        
                        // Track the imported settings
                        _importedProfileName = profile.Name;
                        _importedSettingIds = new List<string>(profile.EnabledSettingIds);
                        HasImportedSettings = true;
                        
                        StatusMessage = $"Configuration imported: {profile.EnabledCount} settings enabled";
                        MessageBox.Show($"Configuration imported successfully!\n\n{profile.EnabledCount} settings are now selected.\n\nClick 'Apply Imported ({profile.EnabledCount})' in the dashboard to apply these settings.",
                            "Import Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing configuration: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Import a configuration profile (used by command line and UI)
        /// </summary>
        public void ImportConfiguration(ConfigurationService.ConfigurationProfile profile)
        {
            var allSettings = _hardeningService.GetAllSettings();
            var changedCount = ConfigurationService.ApplyConfiguration(profile, allSettings);
            
            // Refresh the current view
            UpdateCategoryStatistics();
            FilterSettings();
            
            // Notify property changes for dashboard
            OnPropertyChanged(nameof(SecurityScore));
            OnPropertyChanged(nameof(SecurityLevel));
            OnPropertyChanged(nameof(AppliedSettingsCount));
            NotifyProfileAppliedCounts();
        }

        /// <summary>
        /// Apply all enabled settings (used for command line automation)
        /// </summary>
        public async Task ApplyAllEnabledAsync()
        {
            // Silent CLI automation (--config --apply --silent) runs with no confirmation dialog,
            // so it must NEVER uninstall apps. Bloatware removal is destructive and machine-specific
            // and only ever happens through interactive, review-first selection.
            var enabledSettings = _hardeningService.GetAllSettings()
                .Where(s => s.IsEnabled && s.Category != SettingCategory.Bloatware).ToList();

            if (enabledSettings.Count == 0)
                return;

            IsProcessing = true;
            ProgressMax = enabledSettings.Count;
            ProgressValue = 0;

            try
            {
                // Create backup first
                StatusMessage = "Creating backup...";
                await _backupService.CreateBackupAsync(enabledSettings);

                int applied = 0;
                int failed = 0;

                foreach (var setting in enabledSettings)
                {
                    ProgressValue++;
                    StatusMessage = $"Applying: {setting.Name}";

                    var success = await _hardeningService.ApplySettingAsync(setting);
                    if (success)
                    {
                        setting.IsApplied = true;
                        applied++;
                    }
                    else
                    {
                        failed++;
                    }

                    _hardeningService.CheckCurrentStatus(setting);
                }

                StatusMessage = $"Complete: {applied} applied, {failed} failed";
                UpdateCategoryStatistics();
            }
            finally
            {
                IsProcessing = false;
            }
        }

        #endregion
    }

    /// <summary>
    /// Dashboard view-model for a single product's DISA STIG compliance
    /// (e.g. "Windows 11" · "V2R7" · 12/114 applied).
    /// </summary>
    public sealed class StigProductStat
    {
        public StigProductStat(string name, string stigVersion, int applied, int total)
        {
            Name = name;
            StigVersion = stigVersion;
            Applied = applied;
            Total = total;
        }

        public string Name { get; }
        public string StigVersion { get; }
        public int Applied { get; }
        public int Total { get; }
        public string Progress => $"{Applied}/{Total}";
        public int Percent => Total == 0 ? 0 : (int)System.Math.Round((double)Applied / Total * 100);
    }
}
