#ifndef _VSARDUINO_H_
#define _VSARDUINO_H_
//Board = Arduino Diecimila or Duemilanove w/ ATmega168
#define __AVR_ATmega168__
#define 
#define _VMDEBUG 1
#define ARDUINO 105
#define ARDUINO_MAIN
#define __AVR__
#define F_CPU 16000000L
#define __cplusplus
#define __inline__
#define __asm__(x)
#define __extension__
#define __ATTR_PURE__
#define __ATTR_CONST__
#define __inline__
#define __asm__ 
#define __volatile__

#define __builtin_va_list
#define __builtin_va_start
#define __builtin_va_end
#define __DOXYGEN__
#define __attribute__(x)
#define NOINLINE __attribute__((noinline))
#define prog_void
#define PGM_VOID_P int
            
typedef unsigned char byte;
extern "C" void __cxa_pure_virtual() {;}

//
void zero();
void readAccel();
//
void outputAttitude();
void outputRawCsv();

#include "C:\Program Files (x86)\Arduino\hardware\arduino\variants\standard\pins_arduino.h" 
#include "C:\Program Files (x86)\Arduino\hardware\arduino\cores\arduino\arduino.h"
#include "C:\Users\kenm\Documents\GitHub\quad-control\FlightControl\FlightControl.ino"
#include "C:\Users\kenm\Documents\GitHub\quad-control\FlightControl\CppRes.cpp"
#include "C:\Users\kenm\Documents\GitHub\quad-control\FlightControl\CppRes.h"
#include "C:\Users\kenm\Documents\GitHub\quad-control\FlightControl\Tests.cpp"
#endif
