@echo off
SET SCRIPT_PATH=%~dp0

:APPEND_CLASSPATH
SET CLASSPATH=%SCRIPT_PATH%\lib\*

call mvn clean test^
 -Dbaseurl="https://transcription-dev.qsrulurudev.com"^
 -Dbrowser=chrome^
 -Dplatform=monitor_1^
 -Dusername="linhqa@yopmail.com"^
 -Dpassword="Linh1995"^
 -Dtest=AllPageTest
