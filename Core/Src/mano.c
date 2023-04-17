//HAL IT https://controllerstech.com/uart-transmit-in-stm32/
#include "mano.h"
#include "ssd1306.h"
#include "fonts.h"
#include "math.h"
#include "main.h"
#include <stdio.h>

uint8_t sendData[3];
uint8_t receiveData[6];
uint8_t status;

int calibrationValue = 0;

int D = 0;

void I2C_Init(){
	//Ijungti akselorometra
	sendData[0] = 0x20;
	sendData[1] = 0x77; //ODR: 0111, Normal mode 400Hz
			
	status = HAL_I2C_Master_Transmit(&hi2c1, I2C_Accelerometer_Adress, (uint8_t *)sendData, 2, 100);
	if(status == HAL_OK){
		
	}
	
	//Ijungti magnetini kompasa
	sendData[0] = 0x02;
	sendData[1] = 0x00; //Continuous conversion
	status = HAL_I2C_Master_Transmit(&hi2c1, I2C_Compass_Adress, (uint8_t *)sendData, 2, 100);
	
	sendData[0] = 0x01;
	sendData[1] = 0x80; //GN: 100, +- 4Gauss
	status = HAL_I2C_Master_Transmit(&hi2c1, I2C_Compass_Adress, (uint8_t *)sendData, 2, 100);
}

int nuskaitymu_skaicius = 0;

int accX, accY, accZ, rawX, rawY, rawZ = 0;

int16_t accX_temp = 0;
int16_t accY_temp = 0;
int16_t accZ_temp = 0;

int16_t rawX_temp = 0x0; //Magnetiniai duomenys diapozonas: -2048 to +2047
int16_t rawY_temp = 0x0;
int16_t rawZ_temp = 0x0;

void Get_Compass_Data()
{
	//Ijungti accelerometra
	sendData[0] = 0x20;
	sendData[1] = 0x77;
	status = HAL_I2C_Master_Transmit(&hi2c1, I2C_Accelerometer_Adress, (uint8_t *)sendData, 2, 1);
	
	//Skaityti accelerometra
	sendData[0] = 0xA8;
	status = HAL_I2C_Master_Transmit(&hi2c1, I2C_Accelerometer_Adress, (uint8_t *)sendData, 1, 1);
	status = HAL_I2C_Master_Receive(&hi2c1, I2C_Accelerometer_Adress, (uint8_t *)receiveData, 6, 1);
	accX_temp = receiveData[0] | (receiveData[1] << 8);
	accY_temp = receiveData[2] | (receiveData[3] << 8);
	accZ_temp = receiveData[4] | (receiveData[5] << 8);
	
	//Gauti magnetinius duomenis
	sendData[0] = 0x03;
	status = HAL_I2C_Master_Transmit(&hi2c1, I2C_Compass_Adress, (uint8_t *)sendData, 1, 1);
	status = HAL_I2C_Master_Receive(&hi2c1, I2C_Compass_Adress, (uint8_t *)receiveData, 6, 1);
	rawX_temp = (receiveData[0] << 8) | receiveData[1]; //Pasisukimas i sona
	rawZ_temp = (receiveData[2] << 8) | receiveData[3]; //Pasisukimas is virsaus
	rawY_temp = (receiveData[4] << 8) | receiveData[5]; //persivertimas
	
	//Prideti vidurkio skaiciavimui
	nuskaitymu_skaicius++;
	
	rawX += rawX_temp;
	rawY += rawY_temp;
	rawZ += rawZ_temp;
	
	accX += accX_temp;
	accY += accY_temp;
	accZ += accZ_temp;
}

float pitch = 0;
float roll = 0;

float MX1 = 0x0;
float MY1 = 0x0;
float MZ1 = 0x0;

float MX2 = 0x0;
float MY2 = 0x0;
float MZ2 = 0x0;

int Calculate_compass_data(){
	//Randame vidurki
	rawX = rawX / nuskaitymu_skaicius;
	rawY = rawY / nuskaitymu_skaicius;
	rawZ = rawZ / nuskaitymu_skaicius;
	
	accX = accX / nuskaitymu_skaicius;
	accY = accY / nuskaitymu_skaicius;
	accZ = accZ / nuskaitymu_skaicius;
	
	//Pasiverciame magnetine reiksme i gausus nuo 0 iki 1
	MX1 = (float)rawX / lsb_Gauss_xy;
	MY1 = (float)rawY / lsb_Gauss_xy;
	MZ1 = (float)rawZ / lsb_Gauss_z;
	
	//Skaiciuojame formules
	pitch = asin(-((float)accX/ 32768.0f/acc_full_scale)); //Daliname is 2^15 gauti reiksmei nuo 0 iki 1
	roll = asin((float)accY/ 32768.0f/acc_full_scale/cos((float)pitch)); //32768.0f padariau dalyba vietoj daugybos is 2 ISTESTUOTI TAI
	
	MX2 = MX1 * cos(pitch) + MZ1 * sin(pitch);
	MY2 = MX1 * sin(roll) * sin(pitch) + MY1 * cos(roll) - MZ1*sin(roll)*cos(pitch);
	MZ2 = -MX1*cos(roll)*sin(pitch)+MY1*sin(roll)+MZ1*cos(roll)*cos(pitch);
	
	D = atan2(MY2, MX2) * 180.0f/3.14f; //Su atkompensavimais
	
	#if Enable_calibration
		D += calibrationValue;
		D %= 360;
	#endif
	if(D < 0){
		D += 360;
	}
	
	//Graziname reiksmes i 0
	nuskaitymu_skaicius = 0;
	rawX = 0;
	rawY = 0;
	rawZ = 0;
	accX = 0;
	accY = 0;
	accZ = 0;
	
	return D;
}

void Update_Screen(){
	SSD1306_Clear();
	//Irasyti pasisukimo reiksme ekrane
	char headingString[13];
	sprintf(headingString, "Heading: %3d", compassRotation);
	SSD1306_GotoXY (0,0);
	SSD1306_Puts (headingString, &Font_7x10, 1);
	//Irasyti konfiguracimo reiksme ekrane
	char calibrationString[9];
	sprintf(calibrationString, "Clb: %3d", calibrationValue);
	SSD1306_GotoXY (0,50);
	SSD1306_Puts (calibrationString, &Font_7x10, 1);
	
	//Nupiesti komapasa
	SSD1306_DrawCircle(screen_width/2, screen_height/2, 20,1);
	int comppasLineX = sin((float)compassRotation * 3.14f / (float)180) * 20.0;
  int comppasLineY = cos((float)compassRotation * 3.14f / (float)180) * 20.0;
	SSD1306_DrawLine(screen_width/2, screen_height/2, screen_width/2 + comppasLineX, screen_height/2 + comppasLineY, 1);
	SSD1306_UpdateScreen(); // update screen
}

void Update_PC_Heading()
{
	sendData[0] = 0x03; //Komandos pavadinimas
	sendData[1] = compassRotation >> 8;
	sendData[2] = compassRotation & 0xFF;
	HAL_UART_Transmit_IT(&huart4, sendData, 3);
}

//NUsiusti i kompiuteri esama kalibracijos reiksme
void Update_PC_Calibration()
{
	sendData[0] = 0x04; //Komandos pavadinimas
	sendData[1] = calibrationValue >> 8;
	sendData[2] = calibrationValue & 0xFF;
	if(HAL_UART_Transmit(&huart4, sendData, 3, 1000)  != HAL_OK){
		
	}
}

void Read_Calibration()
{
	uint32_t dataIn[1];
	Flash_Read_Data(0x0803F800, dataIn, 1);
	calibrationValue = (dataIn[0] >> 16);
}

//Irasyti i atminti nauja kalibracijos reiksme
void Update_Calibration(uint16_t new_calibration)
{
		uint32_t data[1];
		data[0] = new_calibration << 16;
		Flash_Write_Data(0x0803F800, data, 1);
}

//Irasymas i flasha
uint32_t Flash_Write_Data (uint32_t StartPageAddress, uint32_t *Data, uint16_t numberofwords)
{
	static FLASH_EraseInitTypeDef EraseInitStruct;
	uint32_t PAGEError;
	int sofar=0;

	  /* Unlock the Flash to enable the flash control register access *************/
	   HAL_FLASH_Unlock();

	   /* Erase the user Flash area*/

	  uint32_t StartPage =  StartPageAddress;
	  uint32_t EndPageAdress = StartPageAddress + numberofwords*4;
	  uint32_t EndPage = EndPageAdress;

	   /* Fill EraseInit structure*/
	   EraseInitStruct.TypeErase   = FLASH_TYPEERASE_PAGES;
	   EraseInitStruct.PageAddress = StartPage;
	   EraseInitStruct.NbPages     = ((EndPage - StartPage)/FLASH_PAGE_SIZE) +1;

	   if (HAL_FLASHEx_Erase(&EraseInitStruct, &PAGEError) != HAL_OK)
	   {
	     /*Error occurred while page erase.*/
		  return HAL_FLASH_GetError ();
	   }

	   /* Program the user Flash area word by word*/

	   while (sofar<numberofwords)
	   {
	     if (HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD, StartPageAddress, Data[sofar]) == HAL_OK)
	     {
	    	 StartPageAddress += 4;  // use StartPageAddress += 2 for half word and 8 for double word
	    	 sofar++;
	     }
	     else
	     {
	       /* Error occurred while writing data in Flash memory*/
	    	 return HAL_FLASH_GetError ();
	     }
	   }

	   /* Lock the Flash to disable the flash control register access (recommended
	      to protect the FLASH memory against possible unwanted operation) *********/
	   HAL_FLASH_Lock();
		 return 0;
}

void Flash_Read_Data (uint32_t StartPageAddress, uint32_t *RxBuf, uint16_t numberofwords)
{
	while (1)
	{
		*RxBuf = *(__IO uint32_t *)StartPageAddress;
		StartPageAddress += 4;
		RxBuf++;
		if (!(numberofwords--)) break;
	}
}

