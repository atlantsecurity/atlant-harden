# DISA STIG — Microsoft Office 365 ProPlus (V3R5) &mdash; Part 1 of 2

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_The full DISA Office 365 STIG — overwhelmingly high-value anti-document-malware controls (macro blocking, Protected View, ActiveX/DDE hardening, unsigned add-in blocking). The Recommended profile keeps these but omits the legacy file-format blocks that would stop old .doc/.xls/.ppt files from opening._

**53 settings** on this page &mdash; **33** are part of the Recommended profile.

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


