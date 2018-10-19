@echo off

SET CurrentFolderPath=%~dp0

ECHO.
SET hour=%time:~0,2%
IF "%hour:~0,1%" == " " SET hour=0%hour:~1,1%
SET min=%time:~3,2%
IF "%min:~0,1%" == " " SET min=0%min:~1,1%
SET secs=%time:~6,2%
IF "%secs:~0,1%" == " " SET secs=0%secs:~1,1%

SET FolderName=%date:~10,4%%date:~4,2%%date:~7,2%_%hour%%min%%secs%

SET ReportDir=%CurrentFolderPath%\TestReport\%FolderName%

REM ===================== Clean up Test Data folders and files ================
IF NOT EXIST .\TestData GOTO StartTest
RMDIR /S /Q .\TestData
:StartTest

REM ===================== Set up environment Name =====================
REM ===== Current supporting environments: QA or Test
SET TestEnv=Test

REM ======================== ========================= ========================
REM ======================== Automation Test Execution ========================
REM ======================== ========================= ========================

REM ===== Download Nuget package for Test Audio Files
call nuget.exe install QSR.Uluru.UI.Test.Data -Version 1.3.0^
 -OutputDirectory TestData^
 -ExcludeVersion^
 -Source https://qsrinternational.myget.org/F/uluru/auth/03be7d63-603c-443a-bcb1-e5683906b9fb/api/v2

REM ===== Automation UI for Transcription Page
REM ===== Windows 10/Laptop 14" full screen
call .\packages\NUnit.ConsoleRunner.3.9.0\tools\nunit3-console.exe^
 .\Functionality\bin\Debug\Functionality.dll^
 --work=%ReportDir%\Transcription^
 --result=Automation.UI.chrome.xml^
 --params=Browser=chrome;ScreenSize=max

REM =========== Remove "REM" to run the following test on other browsers:

REM call .\packages\NUnit.ConsoleRunner.3.9.0\tools\nunit3-console.exe^
REM  .\Functionality\bin\Debug\Functionality.dll^
REM  --work=%ReportDir%\Transcription^
REM  --result=Automation.UI.firefox.xml^
REM  --params=Browser=firefox;ScreenSize=max

REM call .\packages\NUnit.ConsoleRunner.3.9.0\tools\nunit3-console.exe^
REM  .\Functionality\bin\Debug\Functionality.dll^
REM  --work=%ReportDir%\Transcription^
REM  --result=Automation.UI.edge.xml^
REM  --params=Browser=edge;ScreenSize=max

REM call .\packages\NUnit.ConsoleRunner.3.9.0\tools\nunit3-console.exe^
REM  .\Functionality\bin\Debug\Functionality.dll^
REM  --work=%ReportDir%\Transcription^
REM  --result=Automation.UI.ie.xml^
REM  --params=Browser=ie;ScreenSize=max

REM ===== Automation UI for Platform Page
REM ===== Windows 10/Laptop 14" full screen
call .\packages\NUnit.ConsoleRunner.3.9.0\tools\nunit3-console.exe^
 .\Platform\bin\Debug\Platform.dll^
 --work=%ReportDir%\Platform^
 --result=Automation.UI.chrome.xml^
 --params=Browser=chrome;ScreenSize=max

REM =========== Remove "REM" to run the following test on other browsers:

REM call .\packages\NUnit.ConsoleRunner.3.9.0\tools\nunit3-console.exe^
REM  .\Platform\bin\Debug\Platform.dll^
REM  --work=%ReportDir%\Platform^
REM  --result=Automation.UI.firefox.xml^
REM  --params=Browser=firefox;ScreenSize=max

REM call .\packages\NUnit.ConsoleRunner.3.9.0\tools\nunit3-console.exe^
REM  .\Platform\bin\Debug\Platform.dll^
REM  --work=%ReportDir%\Platform^
REM  --result=Automation.UI.edge.xml^
REM  --params=Browser=edge;ScreenSize=max

REM call .\packages\NUnit.ConsoleRunner.3.9.0\tools\nunit3-console.exe^
REM  .\Platform\bin\Debug\Platform.dll^
REM  --work=%ReportDir%\Platform^
REM  --result=Automation.UI.ie.xml^
REM  --params=Browser=ie;ScreenSize=max

REM ====== Enable BrowserStack ========
REM --params=browserstack.enabled=true
