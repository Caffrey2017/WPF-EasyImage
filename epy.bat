@echo off
::set "%0=%0 %*"

::#####Ԥ����-��������˫���ŵĸ����Ƿ�Ϊ����
:: break "%*"&goto :MAIN
:: echo �����﷨����ȷ��
:: exit /b 1
::#####Ԥ����-��������˫���ŵĸ����Ƿ�Ϊ����

:: ====

:: *************** start of 'main'----������
:MAIN

setlocal enabledelayedexpansion

RegExpIsMatch_ "/\?" "%*"
if 0==%errorlevel% call :USAGE&exit /b 0


set sourcepath=F:\����Ŀ¼\EasyImage
set targetpath=f:\����Ŀ¼\EasyImage\EasyImage\Plugins

copy /y %sourcepath%\ArtDeal\bin\Release\ArtDeal.dll %targetpath%\ArtDeal.dll
copy /y %sourcepath%\EasyDeal\bin\Release\EasyDeal.dll %targetpath%\EasyDeal.dll
copy /y %sourcepath%\Drawing\bin\Release\Drawing.dll %targetpath%\Drawing.dll
copy /y %sourcepath%\Beauty\bin\Release\Beauty.dll %targetpath%\Beauty.dll
copy /y %sourcepath%\Property\bin\Release\Property.dll %targetpath%\Property.dll


REM copy /y F:\����Ŀ¼\EasyImage\ArtDeal\bin\Release\ArtDeal.dll C:\Users\cheng\AppData\Local\Apps\2.0\Y38C8Z4A.G46\LD61W7T6.J3E\easy..tion_e673c794dde2cfee_0001.0000_466e6fad5bcf9763\Plugins\ArtDeal.dll
REM copy /y F:\����Ŀ¼\EasyImage\EasyDeal\bin\Release\EasyDeal.dll C:\Users\cheng\AppData\Local\Apps\2.0\Y38C8Z4A.G46\LD61W7T6.J3E\easy..tion_e673c794dde2cfee_0001.0000_466e6fad5bcf9763\Plugins\EasyDeal.dll
REM copy /y F:\����Ŀ¼\EasyImage\Drawing\bin\Release\Drawing.dll C:\Users\cheng\AppData\Local\Apps\2.0\Y38C8Z4A.G46\LD61W7T6.J3E\easy..tion_e673c794dde2cfee_0001.0000_466e6fad5bcf9763\Plugins\Drawing.dll
REM copy /y F:\����Ŀ¼\EasyImage\Beauty\bin\Release\Beauty.dll C:\Users\cheng\AppData\Local\Apps\2.0\Y38C8Z4A.G46\LD61W7T6.J3E\easy..tion_e673c794dde2cfee_0001.0000_466e6fad5bcf9763\Plugins\Beauty.dll
REM copy /y F:\����Ŀ¼\EasyImage\Property\bin\Release\Property.dll C:\Users\cheng\AppData\Local\Apps\2.0\Y38C8Z4A.G46\LD61W7T6.J3E\easy..tion_e673c794dde2cfee_0001.0000_466e6fad5bcf9763\Plugins\Property.dll


::endlocal
exit /b 0
:: *************** end of 'main'

:: *************** Functions begin here ****************************

:: *************** start of procedure USAGE----��ʾ�÷�
:USAGE
	echo.
	echo ��ʾ�÷���Ϣ��
goto :EOF
:: *************** end of procedure USAGE