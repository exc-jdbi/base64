/*
 * Copyright © exc-jdbi 2016
 *
 * © mnBase64 2016
 *
 * mnBase64 - www.github.com/exc-jdbi/base64
 *
 * mnBase64 is Free and Opensource Software
*/

#include "stdafx.h"
#include <vector>
#include <iostream>
#include <Windows.h>

#include "mnBase64.h"


int _tmain(int argc, _TCHAR* argv[]){
	char cText[]="A Ansistring with special characters ¾½¼™Þ‰Æ€#èüä$!$£{°§1*-+±'?Øø}[»²³$£/&%ç¬›|‹¢@©®÷«]. ";
	wchar_t wcText[]=L"A Widestring with special characters 日本人, ไทย, Український, עברית, العربية, भारतीय,فارسی, русский, සිංහල, தமிழ். ";

	std::wstring ws;
	std::string s;
	for(int i=0;i<60;i++){
		ws+=wcText;
		s+=cText;
	}
	
	wchar_t *bufferW1=0,**pOutW1=&bufferW1;
	wchar_t *bufferW2=0,**pOutW2=&bufferW2;
	int iEncW=b64EncryptW(ws.c_str(),ws.length(),pOutW1,true);
	int iDecW=b64DecryptW(*pOutW1,iEncW,pOutW2,true);
	std::wstring wcDec(*pOutW2),wcEnc(*pOutW1);

	char *bufferA1=0,**pOutA1=&bufferA1;
	char *bufferA2=0,**pOutA2=&bufferA2;
	int iEncA=b64EncryptA(s.c_str(),s.length(),pOutA1);
	int iDecA=b64DecryptA(*pOutA1,iEncA,pOutA2);
	std::string cDec(*pOutA2),cEnc(*pOutA1);
	closeGarbage();

	startGarbage();
	wchar_t *bufferW3=0,**pOutW3=&bufferW3;
	wchar_t *bufferW4=0,**pOutW4=&bufferW4;
	int iEncW2=b64EncryptW(ws.c_str(),ws.length(),pOutW3,true);
	int iDecW2=b64DecryptW(*pOutW3,iEncW2,pOutW4,true);
	std::wstring wcDec2(*pOutW4),wcEnc2(*pOutW3);

	char *bufferA3=0,**pOutA3=&bufferA3;
	char *bufferA4=0,**pOutA4=&bufferA4;
	int iEncA2=b64EncryptA(s.c_str(),s.length(),pOutA3);
	int iDecA2=b64DecryptA(*pOutA3,iEncA2,pOutA4);
	std::string cDec2(*pOutA4),cEnc2(*pOutA3);
	closeGarbage();

	Sleep(300);
	return 0;
}

/*
OUTPUT:
*******
Start GarbageStart ...
... Exit GarbageStart.

SetNews 08975040
SetNews 04E50C48
SetNews 11310040
SetNews 141H0040
SetNews 0AK60240
SetNews 0K400040
SetNews 18B70840
SetNews 19C40B41

Start GarbageClose ...
19C40B41 delete ...
18B70840 delete ...
0AK60240 delete ...
141H0040 delete ...
04E50C48 delete ...
08975040 delete ...
0K400040 delete ...
11310040 delete ...
... Exit GarbageClose.


Start GarbageStart ...
... Exit GarbageStart.

SetNews 08975040
SetNews 04E50C48
SetNews 0AK60240
SetNews 10E30C40
SetNews 126A0J40
SetNews 201E0L40
SetNews 18B70840
SetNews 19C40B41

Start GarbageClose ...
19C40B41 delete ...
18B70840 delete ...
126A0J40 delete ...
10E30C40 delete ...
04E50C48 delete ...
08975040 delete ...
201E0L40 delete ...
0AK60240 delete ...
... Exit GarbageClose.

*/
