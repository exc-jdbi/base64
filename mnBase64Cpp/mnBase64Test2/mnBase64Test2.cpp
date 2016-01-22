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
#include <iostream>
#include <windows.h>

#include "mnBase64.h"

#define mnClose		"close_Garbage"
#define mnStart		"start_Garbage"

#define wcEncrypt	"b64_EncryptW"
#define wcDecrypt	"b64_DecryptW"

#define cEncrypt	"b64_EncryptA"
#define cDecrypt	"b64_DecryptA"

#define confDebug	L"../mnBase64Test/Debug/mnBase64.dll"
#define confRelease	L"../mnBase64Test/Release/mnBase64.dll"

typedef int (DLLFUNCG)();
typedef int (DLLFUNCA)(const char *const,const int,char**);
typedef int (DLLFUNCW)(const wchar_t *const,const int,wchar_t**,bool);

DLLFUNCA	*pDllAEncrypt,*pDllADecrypt;
DLLFUNCW	*pDllWEncrypt,*pDllWDecrypt;
DLLFUNCG	*pDllCloseGarbage,*pDllStartGarbage;

int _tmain(int argc, _TCHAR* argv[]){
	std::string cDec,cEnc,cDec2,cEnc2;
	std::wstring wcDec,wcEnc,wcDec2,wcEnc2;

	char cText[]="A Ansistring with special characters ¾½¼™Þ‰Æ€#èüä$!$£{°§1*-+±'?Øø}[»²³$£/&%ç¬›|‹¢@©®÷«]. ";
	wchar_t wcText[]=L"A Widestring with special characters 日本人, ไทย, Український, עברית, العربية, भारतीय,فارسی, русский, සිංහල, தமிழ். ";

	std::wstring ws;
	std::string s;
	for(int i=0;i<100000;i++){
		ws+=wcText;
		s+=cText;
	}

	HINSTANCE dllHandle;
    dllHandle=::LoadLibrary(confDebug);

	if((dllHandle)){
		pDllAEncrypt=(DLLFUNCA*)::GetProcAddress((HMODULE)dllHandle,cEncrypt);
		pDllADecrypt=(DLLFUNCA*)::GetProcAddress((HMODULE)dllHandle,cDecrypt);
		pDllWEncrypt=(DLLFUNCW*)::GetProcAddress((HMODULE)dllHandle,wcEncrypt);
		pDllWDecrypt=(DLLFUNCW*)::GetProcAddress((HMODULE)dllHandle,wcDecrypt);
		pDllCloseGarbage=(DLLFUNCG*)::GetProcAddress((HMODULE)dllHandle,mnClose);
		pDllStartGarbage=(DLLFUNCG*)::GetProcAddress((HMODULE)dllHandle,mnStart);		
		
		if((pDllCloseGarbage)&&(pDllStartGarbage)){
			if((pDllWEncrypt)&&(pDllWDecrypt)){
				wchar_t *bufferW1=0,**pOutW1=&bufferW1;
				wchar_t *bufferW2=0,**pOutW2=&bufferW2;
				int iEncW=(*pDllWEncrypt)(ws.c_str(),ws.length(),pOutW1,true);
				int iDecW=(*pDllWDecrypt)(*pOutW1,iEncW,pOutW2,true);
				wcDec.assign(*pOutW2);wcEnc.assign(*pOutW1);
			}
			if((pDllAEncrypt)&&(pDllADecrypt)){
				char *bufferA1=0,**pOutA1=&bufferA1;
				char *bufferA2=0,**pOutA2=&bufferA2;
				int iEncA=(*pDllAEncrypt)(s.c_str(),s.length(),pOutA1);
				int iDecA=(*pDllADecrypt)(*pOutA1,iEncA,pOutA2);
				cDec.assign(*pOutA2);cEnc.assign(*pOutA1);
			}
			(*pDllCloseGarbage)();

			//If you want  :D
			(*pDllStartGarbage)();
			if((pDllWEncrypt)&&(pDllWDecrypt)){
				wchar_t *bufferW3=0,**pOutW3=&bufferW3;
				wchar_t *bufferW4=0,**pOutW4=&bufferW4;
				int iEncW2=(*pDllWEncrypt)(ws.c_str(),ws.length(),pOutW3,true);
				int iDecW2=(*pDllWDecrypt)(*pOutW3,iEncW2,pOutW4,true);
				wcDec2.assign(*pOutW4);wcEnc2.assign(*pOutW3);
			}
			if((pDllAEncrypt)&&(pDllADecrypt)){
				char *bufferA3=0,**pOutA3=&bufferA3;
				char *bufferA4=0,**pOutA4=&bufferA4;
				int iEncA2=(*pDllAEncrypt)(s.c_str(),s.length(),pOutA3);
				int iDecA2=(*pDllADecrypt)(*pOutA3,iEncA2,pOutA4);
				cDec2.assign(*pOutA4);cEnc2.assign(*pOutA3);
			}
			(*pDllCloseGarbage)();
		}		
	}

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