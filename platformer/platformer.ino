// LEDS
int pinHealth1 = 13;
int pinHealth2 = 12;
int pinHealth3 = 11;
int pinCoin = 10;
// BUTTON
int pinButton = 3;
int buttonValue;
// HRSR04
int pinTrigger = 6;
int pinEcho = 5;
unsigned long echoValue;
float distcm;

String dist = "DIST ";
String debug = "DEBUG ";

void setup()
{
  Serial.begin(115200);
  
  // set up leds
  pinMode(pinHealth1, OUTPUT);
  pinMode(pinHealth2, OUTPUT);
  pinMode(pinHealth3, OUTPUT);
  pinMode(pinCoin, OUTPUT);
  digitalWrite(pinHealth1, HIGH);
  digitalWrite(pinHealth2, HIGH);
  digitalWrite(pinHealth3, HIGH);
  digitalWrite(pinCoin, LOW);
  
  // set up button
  pinMode(pinButton, INPUT_PULLUP);
  
  //set up sr04
  pinMode(pinTrigger, OUTPUT);
  pinMode(pinEcho, INPUT);
  
  while (!Serial.availableForWrite());
}

void loop()
{
  // Button logic
  buttonValue = digitalRead(pinButton);
  if (buttonValue == LOW) {
  	Serial.println("JUMP");
  }
  
  // HRSR04 logic
  digitalWrite(pinTrigger, HIGH);
  delayMicroseconds(10);
  digitalWrite(pinTrigger, LOW);
  echoValue = pulseIn(pinEcho, HIGH);
  distcm = echoValue/58;
  Serial.println(dist + distcm);
  delay(60);

  // Input logic
  if (Serial.available() > 0) {
    String message = Serial.readStringUntil('\n');
    Serial.println(debug + message);
    if (message == "COIN") {
      digitalWrite(pinCoin, HIGH);
      delay(250);
      digitalWrite(pinCoin, LOW);
    }
    if (message == "2") {
      digitalWrite(pinHealth3, LOW);
    }
    if (message == "1") {
      digitalWrite(pinHealth2, LOW);
    }
    if (message == "0") {
      digitalWrite(pinHealth1, LOW);
    }
  }
}