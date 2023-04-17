#include "stdlib.h"
#include "math.h"
#include <stdint.h>

#define lsb_Gauss_xy 450
#define lsb_Gauss_z 400
#define acc_full_scale 2.0f //Akselerometro duomenu pilnas diapozonas +-2g



int main(void){

	int D = 0;
	//Vertes iš sensoriaus //32767 = 0x7FFF -32767 = 0x8001
	int16_t accX = 0x0;
	int16_t accY = 0x0;
	int16_t accZ = 0x0;
	int16_t rawX = 0x0; 
	int16_t rawY = 0x0;
	int16_t rawZ = 0x0;
	
	float pitch = 0;
	float roll = 0;

	float MX1 = 0x0;
	float MY1 = 0x0;
	float MZ1 = 0x0;

	float MX2 = 0x0;
	float MY2 = 0x0;
	float MZ2 = 0x0;
	
	//Kalkuliavimas
	
	//Pasiverciame magnetine reiksme i gausus nuo 0 iki 1
	MX1 = (float)rawX / lsb_Gauss_xy;
	MY1 = (float)rawY / lsb_Gauss_xy;
	MZ1 = (float)rawZ / lsb_Gauss_z;
	
	//Skaiciuojame formules
	pitch = asin(-((float)accX/ 32768.0f/acc_full_scale)); //Daliname is 2^15 gauti reiksmei nuo 0 iki 1
	roll = asin((float)accY/ 32768.0f/acc_full_scale/cos((float)pitch)); //32768.0f
	
	MX2 = MX1 * cos(pitch) + MZ1 * sin(pitch);
	MY2 = MX1 * sin(roll) * sin(pitch) + MY1 * cos(roll) - MZ1*sin(roll)*cos(pitch);
	MZ2 = -MX1*cos(roll)*sin(pitch)+MY1*sin(roll)+MZ1*cos(roll)*cos(pitch);
	
	D = atan2(MY2, MX2) * 180.0f/3.14f; //Su atkompensavimais
	
	if(D < 0){
		D += 360;
	}
		
	
	uint32_t hex_value = 0x3de147ae; // Example hex value
	float float_value;

	// Type cast the hex value as a float pointer, then dereference it to obtain the float value
	float_value = *((float*)&hex_value);
	while(1){
	}
}
