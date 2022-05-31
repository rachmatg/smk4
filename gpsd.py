import io

import pynmea2
import serial


ser = serial.Serial('COM4', 9600, timeout=5.0)
sio = io.TextIOWrapper(io.BufferedRWPair(ser, ser))

while 1:
    try:
        line = ser.readline()
        msg = pynmea2.parse(line.decode("utf-8") )
        if msg.sentence_type == 'GGA':
            print(repr(msg))
    except serial.SerialException as e:
        print('Device error: {}'.format(e))
        break
    except pynmea2.ParseError as e:
        print('Parse error: {}'.format(e))
        continue