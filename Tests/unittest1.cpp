#include "stdafx.h"
#include "CppUnitTest.h"
#define UNIT_TEST 1
//#include "CppRes.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace Tests
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(TestMethod1)
		{
		Assert::AreEqual(1,1);
		//CPPRES.delayTime();
		}

	};
}