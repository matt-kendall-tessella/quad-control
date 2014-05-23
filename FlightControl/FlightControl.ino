int ledPin = 13;

void setup()
{
	pinMode(ledPin, OUTPUT);
  /* add setup code here */

}

void loop()
{
	digitalWrite(ledPin, LOW);
	delay(100);
	digitalWrite(ledPin, HIGH);
	delay(500);
  /* add main program code here */

}
