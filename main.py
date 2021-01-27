import machine
import time
import socket


# D1 and D2 Pins
BUTTON_PINS = [5, 4]

class Button:
	
    button = None
    id = 0
    is_up = False

    def __init__(self, pin, id):
	self.button = machine.Pin(pin, machine.Pin.IN, machine.Pin.PULL_UP) 
	self.id = id

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

def connect():
    s.connect(('192.168.0.165', 6060))

def start():
    	
    nums = [0, 0]

    buttons = [Button(BUTTON_PINS[0], 0),
	       Button(BUTTON_PINS[1], 1)]

    while True:
	for button in buttons:
	    if button.button.value() == 0 and button.is_up == False:
		button.is_up = True

		if button.id == 1:
		    s.send(bytes('inc', 'utf-8'))

		elif button.id == 0:
		    s.send(bytes('dec', 'utf-8'))

	#	display.fill(0)
	#	display.text('nums: {a}, {b}'.format(a=nums[0], b=nums[1]), 0, 0)
	#	display.show()
	    if button.button.value() == 1 and button.is_up == True:
		button.is_up = False
