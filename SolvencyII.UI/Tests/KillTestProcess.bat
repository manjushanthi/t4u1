tasklist /FI "IMAGENAME eq  vstest.executionengine.x86.exe" 2>NUL | find /I /N "vstest.executionengine.x8">NUL
if "%ERRORLEVEL%"=="0" taskkill /IM vstest.executionengine.x86.exe /F