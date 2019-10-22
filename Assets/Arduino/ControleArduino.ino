// Variaveis e portas
int botaoEsquerda = 12;
int botaoBaixo = 11;
int botaoCima = 10;
int botaoDireita = 9;

int botaoY = 6;
int botaoB = 5;
int botaoX = 4;
int botaoA = 3;


int botaoStart = 8;
int botaoSelect = 7;

int sensorLuz = A0;

//Variavel:
int nivelMonstros = 0;

void setup()
{
	pinMode(botaoEsquerda,INPUT);
	pinMode(botaoBaixo,INPUT);
	pinMode(botaoCima,INPUT);
	pinMode(botaoDireita,INPUT);

	pinMode(botaoY,INPUT);
	pinMode(botaoB,INPUT);
	pinMode(botaoX,INPUT);
	pinMode(botaoA,INPUT);
	
	pinMode(botaoStart,INPUT);
	pinMode(botaoSelect,INPUT);
	
	pinMode(sensorLuz, INPUT);
  
  	Serial.begin(9600);
}

void loop()
{
	int leituraBotaoEsquerda = digitalRead(botaoEsquerda);
	int leituraBotaoBaixo = digitalRead(botaoBaixo);
	int leituraBotaoCima = digitalRead(botaoCima);
	int leituraBotaoDireita = digitalRead(botaoDireita);

	int leituraBotaoY = digitalRead(botaoY);
	int leituraBotaoB = digitalRead(botaoB);
	int leituraBotaoX = digitalRead(botaoX);
	int leituraBotaoA = digitalRead(botaoA);
	
	int leituraBotaoStart = digitalRead(botaoStart);
	int leituraBotaoSelect = digitalRead(botaoSelect);
	
	int leituraSensorLuz = analogRead(sensorLuz);
  
  	Serial.print(leituraBotaoEsquerda);
	Serial.print(";");
	Serial.print(leituraBotaoBaixo);
	Serial.print(";");
	Serial.print(leituraBotaoCima);
	Serial.print(";");
	Serial.print(leituraBotaoDireita);
	Serial.print(";");
  	

	
	Serial.print(leituraBotaoY);
	Serial.print(";");
	Serial.print(leituraBotaoB);
	Serial.print(";");
	Serial.print(leituraBotaoA);
	Serial.print(";");
	Serial.print(leituraBotaoX);
	Serial.print(";");

	Serial.print(leituraBotaoStart);
	Serial.print(";");
	Serial.print(leituraBotaoSelect);
	Serial.print(";");

	Serial.print(leituraSensorLuz);
	Serial.print(";");
  	Serial.println(" ");
	

	
	delay(10);
}