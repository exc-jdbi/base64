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
#include <Windows.h>
#include <process.h>

#include "mnBase64.h"
#include "garbage.h"

HINSTANCE	hInst=0;		// DLL handle

bool start_Garbage(){return startGarbage();}
bool _stdcall startGarbage(){
	std::cout<<"Start GarbageStart ..."<<std::endl;
	bool ret=Garbage::start();
	std::cout<<"... Exit GarbageStart.\n"<<std::endl;
	return ret;
}

bool close_Garbage(){return closeGarbage();}
bool _stdcall closeGarbage(){
	std::cout<<"\nStart GarbageClose ..."<<std::endl;
	bool ret=Garbage::close();
	std::cout<<"... Exit GarbageClose."<<std::endl<<std::endl<<std::endl;
	return ret;
}

BOOL setInstance(HMODULE h){
	hInst  = h;
	if(startGarbage())
		return TRUE;
	return FALSE;
};
BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 ){
	switch (ul_reason_for_call){
	case DLL_PROCESS_ATTACH:
		return setInstance(hModule);
	case DLL_THREAD_ATTACH:		
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

