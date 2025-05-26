# TALL-I

TALL-I is a small puzzle-solving game originally developed as an educational project at the University of Twente. Its core gameplay mechanic revolves around moving objects backwards in time (sort of like portals in Portal 1/2). The game is in early development stage. My goal is to have 10 complete levels, that explore challenges based on kinetic object behavior and spatial reasoning. 

![Alt text](/Screenshots/{5699C62C-5232-4AEB-9219-D548E810A213}.png "First chamber: cube and a pressure plate")

### Current Focus and Completions

- The core time-reversal mechanic is fully implemented and operational. It currently supports carriable physical objects and moving platforms.

- A flexible interface has been developed to allow future expansion as new gameplay elements are introduced.

- Functional objects implemented so far include: 
	- Pressure plates
	- Moving platforms
	- Doors
	- Keypads and switches

The current focus is to polish the afromentioed mechanics and complete tutorial and first levels featuring them.

All 3D assets were taken from here: https://quaternius.com/packs/modularscifimegakit.html

![Alt text](/Screenshots/{F86140D5-00FA-4B11-A096-4554D5488178}.png "Time bending mechnaic: a cube is frozen in time")

### What's Next
Once the game mechanics are polished and assembled into a few levels, the next steps are:
- UI: cuurently it is very basic and requires a lot of work later.
- Sound: currently only main character movement, single room ambient and physical cube have sound effects.
- Simple save system: currently if you exit the prototype, the next time you start from the beginning.
- Graphics: currently has a very simple graphics and only 1 style of chambers; the plan is to improve lightning in exsisting chambers, and add 2-3 other styles for levels.

### Control shematic 

- Hold ```left mouse button``` for holding objects.
- To throw an object, press ```right click button```, while holding ```left click button```.	
- You can move and sprint with ```WASD``` and ```Shift```.
- To use time bending mechanic: 
	- Look at the cube (in current prototype works only for cubes) and press ```F``` if it starts flickering with blue light you are now locked on the cube.
	- Onlce locked press one of the following: 
		- ```1``` for moving object back in time. 
		- ```2``` for freezing object in time. 
		- ```3``` for manual control, similar to ```1```, but now you need to hold ```q``` for moving object in time, you can stop at any moment and hold ```e``` to move it forward in time.  