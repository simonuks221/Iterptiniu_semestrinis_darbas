import serial
import sys

serial_port = 'COM4'


# replace 'COM1' with the actual name of your COM port
try:
    ser = serial.Serial(serial_port, 9600)
except:
    print("Negalima atidaryti COM porto")
    sys.exit()  # Isjungia koda


print("Pasirinkite ka norite daryti:")
print("1 - Skaityti duomenis")
print("2 - konfiguruoti")
try:
    pasirinkimas = int(input("\n"))
except ValueError:
    print("Blogas pasirinkimas")
    sys.exit()  # Isjungia koda

if (pasirinkimas == 1):
    print("Pasirinktas duomenu skaitymas")
    read_com = True
    while read_com:
        data = ser.read(3)  # read 3 bytes from the COM port
        byte1 = data[1]
        byte2 = data[2]
        result = (byte1 << 8) | byte2
        print(result)
elif (pasirinkimas == 2):
    print("Pasirinktas kofiguravimas")
    data = b'\x01\x02\x03'
    ser.write(data)  # send the data to the COM port
else:
    print("Blogas pasirinkimas")


ser.close()  # close the COM port
