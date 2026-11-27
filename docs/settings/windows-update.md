# Windows Update

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_Unpatched software is one of the most common ways attackers get in. These settings keep everything updating automatically: Windows itself (security updates installed without delay), drivers and device firmware, Office and other Microsoft products (.NET, SQL Server, Visual Studio) through Microsoft Update, Microsoft 365 Apps, Store apps, and the Edge and Chrome browsers._

**15 settings** on this page &mdash; **15** are part of the Recommended profile.

### Don't delay security updates

Turns off any deferral of monthly quality (security) updates so they install as soon as they're released.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\DeferQualityUpdates` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Don't let Office updates be switched off

Hides the Enable/Disable Updates option in Office so updates can't be turned off from inside the apps.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\office\16.0\common\officeupdate\hideenabledisableupdates` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Download and install updates automatically

Updates are downloaded and installed automatically; Windows restarts outside your active hours (Configure Automatic Updates = 4, auto download and schedule the install).

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU\AUOptions` = `4` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended
- **&#9888; Impact:** Windows may restart outside active hours to finish installing updates.

### Download updates over metered connections

Lets Windows Update download over connections marked as metered (phone hotspots, capped plans).

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AllowAutoWindowsUpdateDownloadOverMeteredNetwork` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended
- **&#9888; Impact:** Can use mobile data on a hotspot or capped connection.

### Get drivers from Windows Update for new devices

When a device is connected, Windows looks for its driver on Windows Update.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\DriverSearching\SearchOrderConfig` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Get the latest updates as soon as they're available

Opts in to receiving new Windows features and non-security improvements early, as they roll out.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\WindowsUpdate\UX\Settings\IsContinuousInnovationOptedIn` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended
- **&#9888; Impact:** New Windows features can appear before the general rollout.

### Include driver and firmware updates

Lets Windows Update install driver and device firmware updates (UEFI/BIOS, storage, Wi-Fi, TPM and others that ship through Windows Update), instead of excluding them.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\ExcludeWUDriversInQualityUpdate` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Keep Google Chrome updating

Makes sure Chrome updates are allowed (Update policy override default = always allow).

- **Change:** Sets `HKLM\SOFTWARE\Policies\Google\Update\UpdateDefault` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Keep Microsoft Edge updating

Makes sure Edge updates are allowed (Update policy override default = always allow).

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\EdgeUpdate\UpdateDefault` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Keep Microsoft Office updating automatically

Turns on automatic updates for Microsoft 365 Apps / Office 2016-2024 (Click-to-Run).

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\office\16.0\common\officeupdate\enableautomaticupdates` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Notify me when a restart is needed to finish updating

Shows a notification when an update needs a restart, so pending security fixes aren't left half-installed.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\WindowsUpdate\UX\Settings\RestartNotificationsAllowed2` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Receive updates for other Microsoft products

Also installs updates for Microsoft Office, .NET, SQL Server, Visual Studio and other Microsoft software through Windows Update (Microsoft Update).

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU\AllowMUUpdateService` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Remove the option to pause updates

Hides "Pause updates" in Settings so updates can't be postponed.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\SetDisablePauseUXAccess` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended
- **&#9888; Impact:** Users can no longer pause updates, even temporarily.

### Turn on automatic updates

Make sure Windows Update is not switched off by policy (NoAutoUpdate = 0).

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU\NoAutoUpdate` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Update Microsoft Store apps automatically

Keeps Store apps (including App Installer/winget, Photos, Terminal and Store-installed Office) up to date.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\WindowsStore\AutoDownload` = `4` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended


