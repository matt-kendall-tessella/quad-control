// CppRes.h

#ifndef _CPPRES_h
#define _CPPRES_h


	#if defined(ARDUINO) && ARDUINO >= 100
		#include "Arduino.h"
	#else
		#include "WProgram.h"
	#endif



class CppRes
{
 private:


 public:
	void init();

	void delayTime();
};

extern CppRes CPPRES;

#endif

