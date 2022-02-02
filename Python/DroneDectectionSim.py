#calculate the 3D point given the distance from the camera, heading, and pitch
39

from pyproj import Proj, transform
import math
import numpy as np
import zmq
import time
import random
context = zmq.Context()
socket = context.socket(zmq.PUB)
socket.bind("tcp://*:12345")
#create a drone location
x,y,z = 0,10,0
cL = (x,y,z)

# assign angle variables 

#gimble angle
ga = math.degrees(45)
#drone facing direction
a1 = math.radians(45)



#180 degrees = west
#90 degrees = south
#270 degrees = north
#0 degrees equals east. 
#[]
# <>
# |\
# | \
# |  \
#a|   \ c
# |    \
# |     \   \0/
# |______\   |
#    b   .   /\

a = y
c = a/(math.cos(a))
b = math.sqrt(c ** 2 + a ** 2)
x1 = (b * math.cos(a1) + x)
y1 = ((y - y) + 4)
z1 = (b * math.sin(ga) + z)
nL = (x1,y1,z1)
while True:
    message = str(x) + " " + str(y) + " " + str(z)
    droneLocation = str(x) + " " + str(y) + " " + str(z)
    objectLocation = str(x1) + " " + str(y1) + " " + str(z1)
    socket.send_string(message)
    socket.send_string(droneLocation)
    socket.send_string(objectLocation)
    print(droneLocation)
    print(message)
    print(objectLocation)
    time.sleep(1)