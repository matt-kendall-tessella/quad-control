#include "Wire.h"
#include "math.h"
#include "ADXL345.h"
#include "L3G4200D.h"

const int SAMPLE_FREQ = 75; //Hz
const int ZERO_SAMPLES = 100;

float X_OFF = -3.94;
float Y_OFF = -5.03;
float Z_OFF = -41.9;
const float X_SCALE = 257.1;
const float Y_SCALE = 260.3;
const float Z_SCALE = 252.3;

double Xa, Ya, Za, Xg, Yg, Zg, Xm, Ym, Zm;
double roll, pitch, yaw;

ADXL345 acc;
L3G4200D gyro;

void setup()
{
  Serial.begin(115200);
  Wire.begin();
  acc.begin();
  gyro.enableDefault(); 
  //zero(); 
}

void zero()
{	
	int x, y, z;
	acc.read(&x, &y, &z);
	X_OFF = x;
	Y_OFF = y;
	Z_OFF = z - Z_SCALE;	
	for (int i = 1; i <= ZERO_SAMPLES; i++)
	{	
		X_OFF = (X_OFF*i + Xa) / (i+1);
		Y_OFF = (Y_OFF*i + Ya) / (i+1);
		Z_OFF = (Z_OFF*i + (Za - Z_SCALE)) / (i+1);
		readAccel();
		delay(1000.0/SAMPLE_FREQ);
	} 
}

void readAccel()
{
	// Read raw ADU from accelerometer
	int x, y, z;
	acc.read(&x, &y, &z);
	calibrateAccel(x, y, z);
}

void calibrateAccel(int x, int y, int z)
{
	// Convert uncalibrated ADU readings into calibrated g-reading.
	Xa = (x - X_OFF) / X_SCALE;
	Ya = (y - Y_OFF) / Y_SCALE;
	Za = (z - Z_OFF) / Z_SCALE;
}

void calculateOrientation(double x, double y, double z)
{
	roll = atan2(-y, z) * (180 / PI);
	pitch = atan2(x, sqrt(y * y + z * z)) * (180 / PI);
	yaw = 360;
}

void loop()
{
  readAccel();
  calculateOrientation(Xa, Ya, Za);

  
 //outputAttitude();
 //outputRawCsv();
  outputEverything();
  
  delay(1000.0/SAMPLE_FREQ);
}

void outputAttitude()
{
 Serial.print("A");
  Serial.print(" PITCH");
  Serial.print(pitch);
  Serial.print(" ROLL");
  Serial.print(roll);
  Serial.print(" YAW");
  Serial.println(yaw);
  Serial.println("Z");
}

void outputRawCsv() {
  Serial.print("A");
  Serial.print("XA");
  Serial.print(Xa);
  Serial.print("YA");
  Serial.print(Ya);
  Serial.print("ZA");
  Serial.print(Za);
  Serial.print("XG");
  Serial.print(Xa);
  Serial.print("YG");
  Serial.print(Ya);
  Serial.print("ZG");
  Serial.print(Za);
  Serial.print("XM");
  Serial.print(Xa);
  Serial.print("YM");
  Serial.print(Ya);
  Serial.print("ZM");
  Serial.print(Za);
  Serial.println("Z");
}

void outputEverything() {
  Serial.print("AA");
  Serial.print("XA");
  Serial.print(Xa);
  Serial.print("YA");
  Serial.print(Ya);
  Serial.print("ZA");
  Serial.print(Za);
  Serial.print("XG");
  Serial.print(Xa);
  Serial.print("YG");
  Serial.print(Ya);
  Serial.print("ZG");
  Serial.print(Za);
  Serial.print("XM");
  Serial.print(Xa);
  Serial.print("YM");
  Serial.print(Ya);
  Serial.print("ZM");
  Serial.print(Za);
  Serial.print("AP");
  Serial.print(pitch);
  Serial.print("AR");
  Serial.print(roll);
  Serial.print("AY");
  Serial.print(yaw);
  Serial.println("ZZ");

}





    