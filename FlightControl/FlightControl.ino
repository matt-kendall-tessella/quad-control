#include "Wire.h"
#include "math.h"
#include "ADXL345.h"
#include "L3G4200D.h"

const int SAMPLE_FREQ = 75; //Hz
const int ZERO_SAMPLES = 100;

float X_OFF = -3.94;
const float X_SCALE = 256.9;
float Y_OFF = -5.03;
const float Y_SCALE = 260.2;
float Z_OFF = -41.9;
const float Z_SCALE = 252.0;

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
	readAccel();
	X_OFF = Xa;
	Y_OFF = Ya;
	Z_OFF = Za - 256;	
	for (int i = 1; i <= ZERO_SAMPLES; i++)
	{	
		X_OFF = (X_OFF*i + Xa) / (i+1);
		Y_OFF = (Y_OFF*i + Ya) / (i+1);
		Z_OFF = (Z_OFF*i + (Za - 256)) / (i+1);
		readAccel();
		delay(1000.0/SAMPLE_FREQ);
	} 
}

void readAccel()
{
	acc.read(&Xa, &Ya, &Za);
  
	Xa = Xa * 256;
	Ya = Ya * 256;
	Za = Za * 256;
}

void loop()
{
  readAccel();

  //Xa = (Xa - X_OFF) / X_SCALE;
  //Ya = (Ya - Y_OFF) / Y_SCALE;
  //Za = (Za - Z_OFF) / Z_SCALE;

  roll = atan2(-Ya, Za) * (180 / PI);
  pitch = atan2(Xa, sqrt(Ya * Ya + Za * Za)) * (180 / PI);
  yaw = 360;

  //gyro.read();  
  //Xg = gyro.g.x;
  //Yg = gyro.g.y;
  //Zg = gyro.g.z;
  
 //outputAttitude();
  outputRawCsv();
  
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
  Serial.print(Xa);
  Serial.print(",");
  Serial.print(Ya);
  Serial.print(",");
  Serial.println(Za);
}





    