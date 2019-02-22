@echo off
set PROJNAME=%2
set projDir=%~dp0%PROJNAME%
set targetDir=%~dp0..\..\platform\PlatformService.WebHost\Plugins\%PROJNAME%
set commonLibDir=%~dp0..\..\platform\PlatformService.WebHost\Plugins\CommonLib

if not exist %targetDir% md %targetDir%
if not exist %commonLibDir% md %commonLibDir%

COPY %projDir%\%1%PROJNAME%.dll %targetDir% /y
COPY %projDir%\%1%PROJNAME%.XML %targetDir% /y

COPY %projDir%\%1HISP.Authorization.dll %commonLibDir% /y