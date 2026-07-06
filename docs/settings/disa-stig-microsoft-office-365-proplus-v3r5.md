# DISA STIG — Microsoft Office 365 ProPlus (V3R5)

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_The full DISA Office 365 STIG — overwhelmingly high-value anti-document-malware controls (macro blocking, Protected View, ActiveX/DDE hardening, unsigned add-in blocking). The Recommended profile keeps these but omits the legacy file-format blocks that would stop old .doc/.xls/.ppt files from opening._

**106 settings** in this category &mdash; **77** are part of the Recommended profile.

### Active X One-Off forms must only be enabled to load with Outlook Controls.

By default, third-party ActiveX controls are not allowed to run in one-off forms in Outlook. You can change this behavior so that Safe Controls (Microsoft Forms 2.0 controls and the Outlook Recipient and Body controls) are allowed in one-off forms, or so that all ActiveX controls are allowed to run.

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\allowactivexoneoffforms` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000007 (Vuln V-223352)

### AutoRepublish in Excel must be disabled.

This policy setting allows administrators to disable the AutoRepublish feature in Excel. If users choose to publish Excel data to a static Web page and enable the AutoRepublish feature, Excel saves a copy of the data to the Web page every time the user saves the workbook. By default, a message dialo…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\excel\options\disableautorepublish` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000021 (Vuln V-223330)

### AutoRepublish warning alert in Excel must be enabled.

This policy setting allows administrators to disable the AutoRepublish feature in Excel. If users choose to publish Excel data to a static Web page and enable the AutoRepublish feature, Excel saves a copy of the data to the Web page every time the user saves the workbook. By default, a message dialo…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Excel\Options\disableautorepublishwarning` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000022 (Vuln V-223331)

### Custom user interface (UI) code must be blocked from loading in all Office applications.

This policy setting controls whether Office 365 ProPlus applications load any custom user interface (UI) code included with a document or template. Office 365 ProPlus allows developers to extend the UI with customization code that is included in a document or template. If this policy setting is enab…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\common\toolbars\noextensibilitycustomizationfromdocument` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-CO-000004 (Vuln V-223287)

### Document metadata for rights managed Office Open XML files must be protected.

This policy setting determines whether metadata is encrypted in Office Open XML files that are protected by Information Rights Management (IRM). If this policy setting is enabled, Excel, PowerPoint, and Word encrypt metadata stored in rights-managed Office Open XML files and override any configurati…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\common\security\DRMEncryptProperty` = `1` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-CO-000002 (Vuln V-223285)

### Dynamic Data Exchange (DDE) server launch in Excel must be blocked.

This policy setting allows you to control whether Dynamic Data Exchange (DDE) server launch is allowed. By default, DDE server launch is turned off, but users can turn on DDE server launch by going to File >> Options >> Trust Center >> Trust Center Settings >> External Content. For security reasons,…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\external content\disableddeserverlaunch` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000003 (Vuln V-223312)

### Dynamic Data Exchange (DDE) server lookup in Excel must be blocked.

This policy setting allows you to control whether Dynamic Data Exchange (DDE) server lookup is allowed. By default, DDE server lookup is turned on, but users can turn off DDE server lookup by going to File >> Options >> Trust Center >> Trust Center Settings >> External Content. If you enable this po…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\external content\disableddeserverlookup` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000004 (Vuln V-223313)

### Extraction options must be blocked when opening corrupt Excel workbooks.

This policy setting controls whether Excel presents users with a list of data extraction options before beginning an Open and Repair operation when users choose to open a corrupt workbook in repair or extract mode. If you enable this policy setting, Excel opens the file using the Safe Load process a…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\excel\options\extractdatadisableui` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000018 (Vuln V-223327)

### File attachments from Outlook must be opened in Excel in Protected mode.

This policy setting allows you to determine if Excel files in Outlook attachments open in Protected View. If you enable this policy setting, Outlook attachments do not open in Protected View. If you disable or do not configure this policy setting, Outlook attachments open in Protected View.

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\protectedview\DisableAttachmentsInPV` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000034 (Vuln V-223343)

### File extensions must be enabled to match file types in Excel.

This policy setting controls how Excel loads file types that do not match their extension. Excel can load files with extensions that do not match the files' type. For example, if a comma-separated values (CSV) file named example.csv is renamed example.xls (or any other file extension supported by Ex…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Excel\Security\extensionhardening` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000023 (Vuln V-223332)

### File validation in Excel must be enabled.

This policy setting allows you turn off the file validation feature. If you enable this policy setting, file validation will be turned off. If you disable or do not configure this policy setting, file validation will be turned on. Office Binary Documents (97-2003) are checked to see if they conform…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\filevalidation\enableonload` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000025 (Vuln V-223334)

### File validation in PowerPoint must be enabled.

This policy setting allows you to turn off the file validation feature. If you enable this policy setting, file validation will be turned off. If you disable or do not configure this policy setting, file validation will be turned on. Office Binary Documents (97-2003) are checked to see if they confo…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\filevalidation\EnableOnLoad` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-PT-000006 (Vuln V-223382)

### File validation in Word must be enabled.

This policy setting allows the file validation feature to be turned off. If this policy setting is enabled, file validation will be turned off. If this policy setting is disabled or not configured, file validation will be turned on. Office Binary Documents (97-2003) are checked to see if they confor…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\word\security\filevalidation\enableonload` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-WD-000019 (Vuln V-223418)

### Files downloaded from the Internet must be opened in Protected view in PowerPoint.

This policy setting allows you to determine if files downloaded from the Internet zone open in Protected View. If you enable this policy setting, files downloaded from the Internet zone do not open in Protected View. If you disable or do not configure this policy setting, files downloaded from the I…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\protectedview\DisableInternetFilesInPV` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-PT-000009 (Vuln V-223385)

### Files dragged from an Outlook e-mail to the file system must be created in ANSI format.

This policy setting controls whether e-mail messages dragged from Outlook to the file system are saved in Unicode or ANSI format.

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\outlook\options\general\msgformat` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000005 (Vuln V-223350)

### Files failing file validation must be opened in Excel in Protected view mode and disallow edits.

This policy setting controls how Office handles documents when they fail file validation. If you enable this policy setting, you can configure the following options for files that fail file validation: - Block files completely. Users cannot open the files. - Open files in Protected View and disallow…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Excel\Security\FileValidation\DisableEditFromPV` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000033 (Vuln V-223342.b)

### Files in unsafe locations must be opened in Protected view in PowerPoint.

This policy setting determines whether files located in unsafe locations will open in Protected View. If unsafe locations have not been specified, only the "Downloaded Program Files" and "Temporary Internet Files" folders are considered unsafe locations. If enabling this policy setting, files locate…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\protectedview\DisableUnsafeLocationsInPV` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-PT-000011 (Vuln V-223387)

### Flash player activation must be disabled in all Office programs.

This policy setting controls whether the Adobe Flash control can be activated by Office documents. Note that activation blocking applies only within Office processes. If you enable this policy setting, you can choose from three options to control whether and how Flash is blocked from activation: 1.…

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Office\Common\COM Compatibility\COMMENT` = `Block all Flash activation` (REG_SZ)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-CO-000027 (Vuln V-223309)

### If file validation fails, files must be opened in Protected view in PowerPoint with ability to edit disabled.

This policy setting controls how Office handles documents when they fail file validation. If you enable this policy setting, you can configure the following options for files that fail file validation: - Block files completely. Users cannot open the files. - Open files in Protected View and disallow…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\Security\FileValidation\DisableEditFromPV` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-PT-000012 (Vuln V-223388.b)

### If file validation fails, files must be opened in Protected view in Word with ability to edit disabled.

This policy setting controls how Office handles documents when they fail file validation. If you enable this policy setting, you can configure the following options for files that fail file validation: - Block files completely. Users cannot open the files. - Open files in Protected View and disallow…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Word\Security\FileValidation\openinprotectedview` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-WD-000005 (Vuln V-223404.a)

### If file validation fails, files must be opened in Protected view in Word with ability to edit disabled.

This policy setting controls how Office handles documents when they fail file validation. If you enable this policy setting, you can configure the following options for files that fail file validation: - Block files completely. Users cannot open the files. - Open files in Protected View and disallow…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Word\Security\FileValidation\DisableEditFromPV` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-WD-000005 (Vuln V-223404.b)

### In Word, macros must be blocked from running, even if Enable all macros is selected in the Macro Settings sect…

This policy setting allows you to block macros from running in Office files that come from the Internet. If you enable this policy setting, macros are blocked from running, even if "Enable all macros" is selected in the Macro Settings section of the Trust Center. Also, instead of having the choice t…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-WD-000016 (Vuln V-223415)

### Internet must not be included in Safe Zone for picture download in Outlook.

This policy setting controls whether pictures and external content in HTML e-mail messages from untrusted senders on the Internet are downloaded without Outlook users explicitly choosing to do so. If you enable this policy setting, Outlook will automatically download external content in all e-mail m…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Outlook\options\mail\Internet` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000009 (Vuln V-223354)

### Loading of pictures from Web pages not created in Excel must be disabled.

This policy setting controls whether Excel loads graphics when opening Web pages that were not created in Excel. It configures the "Load pictures from Web pages not created in Excel" option under the File tab >> Options >> Advanced >> General >> Web Options... >> General tab. If you enable or do not…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\excel\internet\donotloadpictures` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000020 (Vuln V-223329)

### Macros from the Internet must be blocked from running in PowerPoint.

This policy setting allows you to block macros from running in Office files that come from the Internet. If you enable this policy setting, macros are blocked from running, even if "Enable all macros" is selected in the Macro Settings section of the Trust Center. Also, instead of having the choice t…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\powerpoint\security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-PT-000007 (Vuln V-223383)

### Macros in all Office applications that are opened programmatically by another application must be opened based…

This policy setting controls whether macros can run in an Office 365 ProPlus application that is opened programmatically by another application. If this policy setting is enabled, the user can choose from three options for controlling macro behavior in Excel, PowerPoint, and Word when the applicatio…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\Common\Security\AutomationSecurity` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-CO-000006 (Vuln V-223289)

### Macros must be blocked from running in Access files from the Internet.

This policy setting allows you to block macros from running in Office files that come from the Internet. If you enable this policy setting, macros are blocked from running, even if “Enable all macros” is selected in the Macro Settings section of the Trust Center. Also, instead of having the choice t…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\access\security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-AC-000001 (Vuln V-223280)

### Macros must be blocked from running in Excel files from the Internet.

This policy setting allows you to block macros from running in Office files that come from the Internet. If you enable this policy setting, macros are blocked from running, even if “Enable all macros” is selected in the Macro Settings section of the Trust Center. Also, instead of having the choice t…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000027 (Vuln V-223336)

### Macros must be blocked from running in Visio files from the Internet.

This policy setting allows you to block macros from running in Office files that come from the Internet. If you enable this policy setting, macros are blocked from running, even if “Enable all macros” is selected in the Macro Settings section of the Trust Center. Also, instead of having the choice t…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\visio\security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-VI-000007 (Vuln V-223399)

### Office applications must be configured to specify encryption type in password-protected Office 97-2003 files.

This policy setting enables you to specify an encryption type for password-protected Office 97-2003 files. If you enable this policy setting, you can specify the type of encryption that Office applications will use to encrypt password-protected files in the older Office 97-2003 file formats. The cho…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Common\Security\defaultencryption12` = `Microsoft Enhanced RSA and AES Cryptographic Provider,AES 256,256` (REG_SZ)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-CO-000008 (Vuln V-223291)

### Office applications must be configured to specify encryption type in password-protected Office Open XML files.

This policy setting allows you to specify an encryption type for Office Open XML files. If you enable this policy setting, you can specify the type of encryption that Office applications use to encrypt password-protected files in the Office Open XML file formats used by Excel, PowerPoint, and Word.…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Common\Security\OpenXMLEncryption` = `Microsoft Enhanced RSA and AES Cryptographic Provider,AES 256,256` (REG_SZ)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-CO-000009 (Vuln V-223292)

### Office applications must not load XML expansion packs with Smart Documents.

This policy setting controls whether Office 365 ProPlus applications can load an XML expansion pack manifest file with a Smart Document.

- **Change:** Sets `HKCU\software\policies\microsoft\office\common\smart tag\for neverloadmanifests` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-CO-000012 (Vuln V-223294)

### Open/save of dBase III / IV format files must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\DBaseFiles` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-EX-000005 (Vuln V-223314)

### Open/save of Dif and Sylk format files must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\DifandSylkFiles` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-EX-000006 (Vuln V-223315)

### Open/save of Excel 2 macrosheets and add-in files must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL2Macros` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000007 (Vuln V-223316)

### Open/save of Excel 2 worksheets must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL2Worksheets` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-EX-000008 (Vuln V-223317)

### Open/save of Excel 3 macrosheets and add-in files must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL3Macros` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000009 (Vuln V-223318)

### Open/save of Excel 3 worksheets must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL3Worksheets` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-EX-000010 (Vuln V-223319)

### Open/save of Excel 4 macrosheets and add-in files must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL4Macros` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000011 (Vuln V-223320)

### Open/save of Excel 4 workbooks must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL4Workbooks` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-EX-000012 (Vuln V-223321)

### Open/save of Excel 4 worksheets must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL4Worksheets` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-EX-000013 (Vuln V-223322)

### Open/save of Excel 95 workbooks must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\fileblock\xl95workbooks` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-EX-000014 (Vuln V-223323)

### Open/save of Excel 95-97 workbooks and templates must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\office\16.0\excel\security\fileblock\XL9597WorkbooksandTemplates` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-EX-000015 (Vuln V-223324)

### Open/Save of PowerPoint 97-2003 presentations, shows, templates, and add-in files must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save PowerPoint files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be select…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\powerpoint\security\fileblock\binaryfiles` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-PT-000003 (Vuln V-223379)

### Open/save of Web pages and Excel 2003 XML spreadsheets must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\fileblock\htmlandxmlssfiles` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-EX-000017 (Vuln V-223326)

### Open/Save of Word 2 and earlier binary documents and templates must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\Word2Files` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-WD-000008 (Vuln V-223407)

### Open/Save of Word 2000 binary documents and templates must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\Word2000Files` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-WD-000009 (Vuln V-223408)

### Open/Save of Word 2003 binary documents and templates must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\word2003files` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-WD-000010 (Vuln V-223409)

### Open/Save of Word 2007 and later binary documents and templates must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\word2007files` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-WD-000011 (Vuln V-223410)

### Open/Save of Word 6.0 binary documents and templates must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\word60files` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-WD-000012 (Vuln V-223411)

### Open/Save of Word 95 binary documents and templates must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\word95files` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-WD-000013 (Vuln V-223412)

### Open/Save of Word 97 binary documents and templates must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\word97files` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-WD-000014 (Vuln V-223413)

### Open/Save of Word XP binary documents and templates must be blocked.

This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\wordxpfiles` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-WD-000015 (Vuln V-223414)

### Outlook must be configured to allow retrieving of Certificate Revocation Lists (CRLs) always when online.

This policy setting controls how Outlook retrieves Certificate Revocation Lists to verify the validity of certificates. Certificate revocation lists (CRLs) are lists of digital certificates that have been revoked by their controlling certificate authorities (CAs), typically because the certificates…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Outlook\security\usecrlchasing` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000013 (Vuln V-223358)

### Outlook must be configured to not allow hyperlinks in suspected phishing messages.

This policy setting controls whether hyperlinks in suspected phishing e-mail messages in Outlook are allowed. If you enable this policy setting, Outlook will allow hyperlinks in suspected phishing messages that are not also classified as junk e-mail. If you disable or do not configure this policy se…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\outlook\options\mail\JunkMailEnableLinks` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000027 (Vuln V-223372)

### Outlook must be configured to not run scripts in forms in which the script and the layout are contained within…

This policy setting controls whether scripts can run in Outlook forms in which the script and layout are contained within the message. If you enable this policy setting, scripts can run in one-off Outlook forms. If you disable or do not configure this policy setting, Outlook does not run scripts in…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\outlook\security\EnableOneOffFormScripts` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000019 (Vuln V-223364)

### Outlook must be configured to prevent users overriding attachment security settings.

This policy setting prevents users from overriding the set of attachments blocked by Outlook. If you enable this policy setting users will be prevented from overriding the set of attachments blocked by Outlook. Outlook also checks the "Level1Remove" registry key when this setting is specified. If yo…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\outlook\disallowattachmentcustomization` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000008 (Vuln V-223353)

### Outlook must use remote procedure call (RPC) encryption to communicate with Microsoft Exchange servers.

This policy setting controls whether Outlook uses remote procedure call (RPC) encryption to communicate with Microsoft Exchange servers. If you enable this policy setting, Outlook uses RPC encryption when communicating with an Exchange server. Note: RPC encryption only encrypts the data from the Out…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\outlook\rpc\enablerpcencryption` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000002 (Vuln V-223347)

### PowerPoint attachments opened from Outlook must be in Protected View.

This policy setting allows for determining whether PowerPoint files in Outlook attachments open in Protected View. If enabling this policy setting, Outlook attachments do not open in Protected View. If disabling or not configuring this policy setting, Outlook attachments open in Protected View.

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\protectedview\DisableAttachmentsInPV` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-PT-000010 (Vuln V-223386)

### Project must automatically disable unsigned add-ins without informing users.

This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…

- **Change:** Sets `HKCU\software\policies\Microsoft\office\16.0\ms project\security\notbpromptunsignedaddin` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-PR-000002 (Vuln V-223375)

### Publisher must automatically disable unsigned add-ins without informing users.

This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\publisher\security\notbpromptunsignedaddin` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-PU-000002 (Vuln V-223391)

### Publisher must be configured to prompt the user when another application programmatically opens a macro.

This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if you enable the "Require that application add-ins are signed by Trusted Publishe…

- **Change:** Sets `HKCU\software\policies\microsoft\office\common\security\automationsecuritypublisher` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-PU-000001 (Vuln V-223390)

### Scripts associated with public folders must be prevented from execution in Outlook.

This policy setting controls whether Outlook executes scripts that are associated with custom forms or folder home pages for public folders.

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\publicfolderscript` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000003 (Vuln V-223348)

### Scripts associated with shared folders must be prevented from execution in Outlook.

This policy setting controls whether Outlook executes scripts associated with custom forms or folder home pages for shared folders.

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\sharedfolderscript` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000004 (Vuln V-223349)

### Sending of diagnostic data to Microsoft must be disabled.

Diagnostic data is used to keep Office secure and up to date; detect, diagnose and remediate problems; and make product improvements.

- **Change:** Sets `HKCU\software\policies\Microsoft\office\common\clienttelemetry\SendTelemetry is REG_DWORD = 3, this is not a finding. If the registry key does not exist or` = `3` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-CO-000028 (Vuln V-278355)

### The ability to demote attachments from Level 2 to Level 1 must be disabled.

This policy setting controls whether Outlook users can demote attachments to Level 2 by using a registry key, which will allow them to save files to disk and open them from that location. Outlook uses two levels of security to restrict access to files attached to e-mail messages or other items. File…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\allowuserstolowerattachments` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000015 (Vuln V-223360)

### The default file block behavior must be set to not open blocked files in Excel.

This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\OpenInProtectedView` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-EX-000016 (Vuln V-223325)

### The default file block behavior must be set to not open blocked files in PowerPoint.

This policy setting allows you to determine if users can open, view, or edit Word files. If you enable this policy setting, you can set one of these options: - Blocked files are not opened. - Blocked files open in Protected View and cannot be edited. - Blocked files open in Protected View and can be…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\fileblock\OpenInProtectedView` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-PT-000004 (Vuln V-223380)

### The default file block behavior must be set to not open blocked files in Word.

This policy setting allows you to determine if users can open, view, or edit Word files. If you enable this policy setting, you can set one of these options: - Blocked files are not opened. - Blocked files open in Protected View and cannot be edited. - Blocked files open in Protected View and can be…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\OpenInProtectedView` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-WD-000007 (Vuln V-223406)

### The display of Level 1 attachments must be disabled in Outlook.

This policy setting controls whether Outlook blocks potentially dangerous attachments designated Level 1. Outlook uses two levels of security to restrict users' access to files attached to e-mail messages or other items. Files with specific extensions can be categorized as Level 1 (users cannot view…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\outlook\security\ShowLevel1Attach` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000016 (Vuln V-223361)

### The HTTP fallback for SIP connection in Lync must be disabled.

Prevents from HTTP being used for SIP connection in case TLS or TCP fail.

- **Change:** Sets `HKLM\Software\Policies\Microsoft\office\16.0\lync\disablehttpconnect` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-LY-000002 (Vuln V-223345)

### The junk email protection level must be set to No Automatic Filtering.

This policy setting controls the Junk E-mail protection level. The Junk E-mail Filter in Outlook helps to prevent junk email messages, also known as spam, from cluttering a user's Inbox. The filter evaluates each incoming message based on several factors, including the time when the message was sent…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Outlook\Options\Mail\junkmailprotection` = `3` (REG_SZ)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-OU-000006 (Vuln V-223351)

### The load of controls in Forms3 must be blocked.

This policy setting allows the user to control how ActiveX controls in UserForms should be initialized based upon whether they are Safe for Initialization (SFI) or Unsafe for Initialization (UFI). ActiveX controls are Component Object Model (COM) objects and have unrestricted access to users' comput…

- **Change:** Sets `HKCU\SOFTWARE\Policies\Microsoft\vba\security\LoadControlsInForms` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-CO-000013 (Vuln V-223295)

### The Macro Runtime Scan Scope must be enabled for all documents.

This policy setting specifies for which documents the VBA Runtime Scan feature is enabled. If the feature is disabled for all documents, no runtime scanning of enabled macros will be performed. If the feature is enabled for low trust documents, the feature will be enabled for all documents for which…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\common\security\macroruntimescanscope` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-CO-000001 (Vuln V-223284)

### The Office client must be prevented from polling the SharePoint Server for published links.

This policy setting controls whether Office 365 ProPlus applications can poll Office servers to retrieve lists of published links. If this policy setting is enabled, Office 365 ProPlus applications cannot poll an Office server for published links. If this policy setting is disabled or not configured…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\common\portal\linkpublishingdisabled` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-CO-000003 (Vuln V-223286)

### The Outlook Security Mode must be enabled to always use the Outlook Security Group Policy.

This policy setting controls which set of security settings are enforced in Outlook. If you enable this policy setting, you can choose from four options for enforcing Outlook security settings: - Outlook Default Security - This option is the default configuration in Outlook. Users can configure secu…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Outlook\security\adminsecuritymode` = `3` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000014 (Vuln V-223359)

### The Publish to Global Address List (GAL) button must be disabled in Outlook.

This policy setting controls whether Outlook users can publish e-mail certificates to the Global Address List (GAL). If you enable this policy setting, the "Publish to GAL" button does not display in the "E-mail Security" section of the Trust Center. If you disable or do not configure this policy se…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Outlook\security\publishtogaldisabled` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000010 (Vuln V-223355)

### The Security Level for macros in Outlook must be configured to Warn for signed and disable unsigned.

This policy setting controls the security level for macros in Outlook. If you enable this policy setting, you can choose from four options for handling macros in Outlook: - Always warn. This option corresponds to the "Warnings for all macros" option in the "Macro Security" section of the Outlook Tru…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\level` = `3` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000028 (Vuln V-223373)

### The SIP security mode in Lync must be enabled.

When Lync connects to the server, it supports various authentication mechanisms. This policy allows the user to specify whether Digest and Basic authentication are supported. Disabled (default): NTLM/Kerberos/TLS-DSK/Digest/Basic Enabled: Authentication mechanisms: NTLM/Kerberos/TLS-DSK Gal Download…

- **Change:** Sets `HKLM\Software\Policies\Microsoft\office\16.0\lync\enablesiphighsecuritymode` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-LY-000001 (Vuln V-223344)

### The use of network locations must be ignored in PowerPoint.

This policy setting controls whether trusted locations on the network can be used. If you enable this policy setting, users can specify trusted locations on network shares or in other remote locations that are not under their direct control by clicking the "Add new location" button in the Trusted Lo…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\trusted locations\AllowNetworkLocations` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-PT-000013 (Vuln V-223389)

### The warning about invalid digital signatures must be enabled to warn Outlook users.

This policy setting controls how Outlook warns users about messages with invalid digital signatures. If you enable this policy setting, you can choose from three options for controlling how Outlook users are warned about invalid signatures: - Let user decide if they want to be warned. This option en…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\warnaboutinvalid` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000012 (Vuln V-223357)

### Trust Bar notification must be enabled for unsigned application add-ins in Excel and blocked.

This policy setting controls whether the specified Office 2016 applications notify users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the ''Require that application add-ins are signed by Trusted Publisher'' po…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\notbpromptunsignedaddin` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000028 (Vuln V-223337)

### Trust Bar Notifications for unsigned application add-ins in Access must be disabled and blocked.

This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\access\security\NoTBPromptUnsignedAddin` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-AC-000002 (Vuln V-223281)

### Trust Bar notifications must be configured to display information in the Message Bar about the content that ha…

This policy setting controls whether Office 365 ProPlus applications notify users when potentially unsafe features or content are detected, or whether such features or content are silently disabled without notification. The Message Bar in Office 365 ProPlus applications is used to identify security…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\common\trustcenter\trustbar` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-CO-000007 (Vuln V-223290)

### Trusted Locations on the network must be disabled in Excel.

This policy setting controls whether trusted locations on the network can be used. If you enable this policy setting, users can specify trusted locations on network shares or in other remote locations that are not under their direct control by selecting the "Allow Trusted Locations on my network (no…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\trusted locations\AllowNetworkLocations` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000001 (Vuln V-223310)

### Trusted Locations on the network must be disabled in Project.

This policy setting controls whether trusted locations on the network can be used. If you enable this policy setting, users can specify trusted locations on network shares or in other remote locations that are not under their direct control by clicking the "Add new location" button in the Trusted Lo…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\ms project\security\trusted locations\allownetworklocations` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-PR-000001 (Vuln V-223374)

### Trusted Locations on the network must be disabled in Visio.

This policy setting controls whether trusted locations on the network can be used. If you enable this policy setting, users can specify trusted locations on network shares or in other remote locations that are not under their direct control by clicking the "Add new location" button in the Trusted Lo…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\visio\security\trusted locations\allownetworklocations` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-VI-000002 (Vuln V-223394)

### Trusted Locations on the network must be disabled in Word.

This policy setting controls whether trusted locations on the network can be used. If you enable this policy setting, users can specify trusted locations on network shares or in other remote locations that are not under their direct control by clicking the "Add new location" button in the Trusted Lo…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\word\security\trusted locations\allownetworklocations` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-WD-000017 (Vuln V-223416)

### Unsigned add-ins in PowerPoint must be blocked with no Trust Bar Notification to the user.

This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…

- **Change:** Sets `HKCU\software\policies\Microsoft\office\16.0\powerpoint\security\notbpromptunsignedaddin` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-PT-000008 (Vuln V-223384)

### Untrusted database files must be opened in Excel in Protected View mode.

This policy setting controls whether database files (.dbf) opened from an untrusted location are always opened in Protected View. If you enable this policy setting, database files opened from an untrusted location are always opened in Protected View. Users will not be able to change this setting und…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\protectedview\enabledatabasefileprotectedview` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000030 (Vuln V-223339)

### Untrusted Microsoft Query files must be blocked from opening in Excel.

This policy setting controls whether Microsoft Query files (.iqy, oqy, .dqy, and .rqy) in an untrusted location are prevented from opening. If you enable this policy setting, Microsoft Query files in an untrusted location are prevented from opening. Users will not be able to change this setting unde…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\external content\enableblockunsecurequeryfiles` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000029 (Vuln V-223338)

### Updating of links in Excel must be prompted and not automatic.

This policy setting controls whether Excel prompts users to update automatic links, or whether the updates occur in the background with no prompt. If you enable or do not configure this policy setting, Excel will prompt users to update automatic links. In addition, the "Ask to update automatic links…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\excel\options\binaryoptions\fupdateext_78_1` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-EX-000019 (Vuln V-223328)

### Users must be prevented from creating new trusted locations in the Trust Center.

This policy setting controls whether trusted locations can be defined by users, the Office Customization Tool (OCT), and Group Policy, or if they must be defined by Group Policy alone. If you enable this policy setting, users can specify any location as a trusted location, and a computer can have a…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\common\security\trusted locations\allow user locations` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-CO-000010 (Vuln V-223293)

### Visio 2000-2002 Binary Drawings, Templates and Stencils must be blocked.

This policy setting allows you to determine whether users can open or save Visio files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open or save files. The options that can be selected are below. Note: Not all opt…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\visio\security\fileblock\visio2000files` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-VI-000004 (Vuln V-223396)

### Visio 2003-2010 Binary Drawings, Templates and Stencils must be blocked.

This policy setting allows you to determine whether users can open or save Visio files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open or save files. The options that can be selected are below. Note: Not all opt…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\visio\security\fileblock\visio2003files` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-VI-000005 (Vuln V-223397)

### Visio 5.0 or earlier Binary Drawings, Templates and Stencils must be blocked.

This policy setting allows you to determine whether users can open or save Visio files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open or save files. The options that can be selected are below. Note: Not all opt…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\visio\security\fileblock\visio50andearlierfiles` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** O365-VI-000006 (Vuln V-223398)

### Visio must automatically disable unsigned add-ins without informing users.

This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\visio\security\notbpromptunsignedaddin` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-VI-000003 (Vuln V-223395)

### When a custom action is executed that uses the Outlook object model, Outlook must automatically deny it.

This policy setting controls whether Outlook prompts users before executing a custom action. Custom actions add functionality to Outlook that can be triggered as part of a rule. Among other possible features, custom actions can be created that reply to messages in ways that circumvent the Outlook mo…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoomcustomaction` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000020 (Vuln V-223365)

### When a user designs a custom form in Outlook and attempts to bind an Address Information field to a combinatio…

This policy setting controls what happens when a user designs a custom form in Outlook and attempts to bind an Address Information field to a combination or formula custom field. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to acces…

- **Change:** Sets `HKCU\Software\Policies\Microsoft\Office\16.0\outlook\security\PromptOOMFormulaAccess` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000022 (Vuln V-223367)

### When an untrusted program attempts to gain access to a recipient field, such as the, To: field, using the Outl…

This policy setting controls what happens when an untrusted program attempts to gain access to a recipient field, such as the ''To:'' field, using the Outlook object model. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to access a re…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoomaddressinformationaccess` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000024 (Vuln V-223369)

### When an untrusted program attempts to programmatically access an Address Book using the Outlook object model,…

This policy setting controls what happens when an untrusted program attempts to gain access to an Address Book using the Outlook object model. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to programmatically access an Address Book u…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoomaddressbookaccess` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000021 (Vuln V-223366)

### When an untrusted program attempts to programmatically send e-mail in Outlook using the Response method of a t…

This policy setting controls what happens when an untrusted program attempts to programmatically send e-mail in Outlook using the Response method of a task or meeting request. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to programm…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoommeetingtaskrequestresponse` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000025 (Vuln V-223370)

### When an untrusted program attempts to send e-mail programmatically using the Outlook object model, Outlook mus…

This policy setting controls what happens when an untrusted program attempts to send e-mail programmatically using the Outlook object model. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to send e-mail programmatically using the Outl…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoomsend` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000026 (Vuln V-223371)

### When an untrusted program attempts to use the Save As command to programmatically save an item, Outlook must a…

This policy setting controls what happens when an untrusted program attempts to use the Save As command to programmatically save an item. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to use the Save As command to programmatically sa…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoomsaveas` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-OU-000023 (Vuln V-223368)

### Word attachments opened from Outlook must be in Protected View.

This policy setting allows you to determine if Word files in Outlook attachments open in Protected View. If you enable this policy setting, Outlook attachments do not open in Protected View. If you disable or do not configure this policy setting, Outlook attachments open in Protected View.

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\word\security\protectedview\disableattachmentsinpv` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-WD-000006 (Vuln V-223405)

### Word must automatically disable unsigned add-ins without informing users.

This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…

- **Change:** Sets `HKCU\software\policies\microsoft\office\16.0\word\security\notbpromptunsignedaddin` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** O365-WD-000001 (Vuln V-223400)


