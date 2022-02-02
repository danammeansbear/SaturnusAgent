   #! /usr/local/bin/python3
import zmq
import time
import random
context = zmq.Context()
socket = context.socket(zmq.PUB)
socket.bind("tcp://*:12345")

while True:
	message = str(random.uniform(39.375346, 39.375349)) + " " + str(random.uniform(-84.208137, -84.208139)) + " " + str(random.uniform(1.0, 5.0))
	socket.send_string(message)
	print(message)
	time.sleep(1)
