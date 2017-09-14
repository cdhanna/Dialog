echo Launching Grunt To %1
set curr=%CD%
cd %~dp0
echo %curr%\%1
grunt build --output="%curr%\%1"