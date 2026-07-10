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
cmd /c "C:\Program Files (x86)\Microsoft\Temp\takeown.bat"
