# DISA STIG — Microsoft Office 365 ProPlus (V3R5) &mdash; Part 2 of 2

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_The full DISA Office 365 STIG — overwhelmingly high-value anti-document-malware controls (macro blocking, Protected View, ActiveX/DDE hardening, unsigned add-in blocking). The Recommended profile keeps these but omits the legacy file-format blocks that would stop old .doc/.xls/.ppt files from opening._

**53 settings** on this page &mdash; **44** are part of the Recommended profile.

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


