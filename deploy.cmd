@echo off
echo Deploying Functions ...
xcopy "%DEPLOYMENT_SOURCE%\src\wwwroot" %DEPLOYMENT_TARGET% /Y /S