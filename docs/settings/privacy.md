# Privacy

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_Reduces the data Windows sends to Microsoft and third parties — telemetry, advertising ID, location, Cortana/Bing and consumer "suggestions". These are privacy improvements rather than anti-malware controls, which is why they are excluded from the Recommended profile._

**12 settings** on this page &mdash; **0** are part of the Recommended profile.

### Block Language List Access

Prevent websites from accessing local language list

- **Change:** Sets `HKCU\Control Panel\International\User Profile\HttpAcceptLanguageOptOut` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Clear Recent Documents on Exit

Clear recent documents list when user logs off

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\ClearRecentDocsOnExit` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  ACSC Essential Eight

### Deny Location Access

Disable location services for apps

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\Location` = `Deny` (REG_SZ)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Advertising ID

Disable the unique advertising ID for this device

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\AdvertisingInfo\DisabledByGroupPolicy` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Bing Search

Disable Bing web search in Start Menu

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Search\BingSearchEnabled` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Consumer Features

Disable Microsoft consumer features and suggestions

*Why it matters:* Microsoft consumer experiences provides suggestions and notifications to users, which may include the installation of Windows Store apps. Organizations may control the execution of applications through other means such as allowlisting. Turning off Microsoft consumer experiences will help prevent the…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\CloudContent\DisableWindowsConsumerFeatures` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Cortana

Disable Cortana consent and suggestions

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Search\CortanaConsent` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable GameDVR

Disable Windows Game DVR and broadcasting

*Why it matters:* Windows Game Recording and Broadcasting is intended for use with games; however, it could potentially record screen shots of other applications and expose sensitive data. Disabling the feature will prevent this from occurring.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\GameDVR\AllowGameDVR` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Inventory Collector

Disable application inventory data collection

*Why it matters:* Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Turning off this capability will prevent potentially sensitive information from being sent outside the enterprise and uncontrolled updates to the system. This setting will pre…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\AppCompat\DisableInventory` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Lock Screen Notifications

Prevent toast notifications on lock screen

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\CurrentVersion\PushNotifications\NoToastApplicationNotificationOnLockScreen` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Settings Sync

Disable synchronization of Windows settings to cloud

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\SettingSync\DisableSettingSync` = `2` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Telemetry

Set Windows telemetry to security only level

*Why it matters:* Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Limiting this capability will prevent potentially sensitive information from being sent outside the enterprise. The "Security" option for Telemetry configures the lowest amoun…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\DataCollection\AllowTelemetry` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only


