#include "main.h"

#define Enable_screen 0
#define Enable_sensor 1
#define Enable_pc 1
#define Enable_calibration 1

#define I2C_Compass_Adress 0x3C
#define I2C_Accelerometer_Adress 0x32
#define screen_width 128
#define screen_height 64

#define lsb_Gauss_xy 450
#define lsb_Gauss_z 400
#define acc_full_scale 2.0f //Akselerometro duomenu pilnas diapozonas +-2g

extern I2C_HandleTypeDef hi2c1;
extern UART_HandleTypeDef huart4;

extern int compassRotation;

void I2C_Init();
void Get_Compass_Data();
void Update_Screen();
void Update_PC_Heading();
void Update_PC_Calibration();
void Update_Calibration(uint16_t new_calibration);
void Read_Calibration();
int Calculate_compass_data();
uint32_t Flash_Write_Data (uint32_t StartPageAddress, uint32_t *Data, uint16_t numberofwords);
void Flash_Read_Data (uint32_t StartPageAddress, uint32_t *RxBuf, uint16_t numberofwords);