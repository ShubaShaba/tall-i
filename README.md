# TALL-I

TALL-I is a small puzzle-solving game originally developed as an educational project at the University of Twente. Its core gameplay mechanic revolves around moving objects backwards in time (sort of like portals in Portal 1/2). The game is in early development stage. My goal is to have 10 complete levels, that explore challenges based on kinetic object behavior and spatial reasoning. 

![Alt text](/Screenshots/{5699C62C-5232-4AEB-9219-D548E810A213}.png "First chamber: cube and a pressure plate")

## 📄 Current Focus and Completions

- The core time-reversal mechanic is fully implemented and operational. It currently supports carriable physical objects and moving platforms.

- A flexible interface has been developed to allow future expansion as new gameplay elements are introduced.

- Functional objects implemented so far include: 
	- Pressure plates
	- Moving platforms
	- Doors
	- Keypads and switches

The current focus is to polish the afromentioed mechanics and complete tutorial and first levels featuring them.

![Alt text](/Screenshots/{F86140D5-00FA-4B11-A096-4554D5488178}.png "Time bending mechnaic: a cube is frozen in time")

## 🚩 What's Next
Once the game mechanics are polished and assembled into a few levels, the next steps are:
- UI: currently it is very basic and requires a lot of work later.
- Sound: currently only main character movement, single room ambient and physical cube have sound effects.
- Simple save system: currently if you exit the prototype, the next time you start from the beginning.
- Graphics: currently has a very simple graphics and only 1 style of chambers; the plan is to improve lightning in exsisting chambers, and add 2-3 other styles for levels.

## 🎮 Control shematic 

- Hold ```left mouse button``` for holding objects.
- To throw an object, press ```right click button```, while holding ```left click button```.	
- You can move and sprint with ```WASD``` and ```Shift```.
- To use time bending mechanic: 
	- Look at the cube (in current prototype works only for cubes) and press ```F``` if it starts flickering with blue light you are now locked on the cube.
	- Onlce locked press one of the following: 
		- ```1``` for moving object back in time. 
		- ```2``` for freezing object in time. 
		- ```3``` for manual control, similar to ```1```, but now you need to hold ```q``` for moving object in time, you can stop at any moment and hold ```e``` to move it forward in time.

## 📦 Running the Game (Pre-built Releases)

Grab the latest binaries from the **[Releases](https://github.com/ShubaShaba/tall-i/releases)** page of this repository.

#### 🪟 Windows
1. Download the Windows `.zip`.
2. Extract it anywhere.
3. Double-click the executable (e.g., `tall-i.exe`) inside the extracted folder.

#### 🐧 Linux
1. Download the Linux `.zip`.
2. Extract it.
3. Make the file executable (first time only):

       chmod +x tall-i.x86_64

4. Run it:

       ./tall-i.x86_64

## 🛠️ Building the Project Yourself

### 🧰 Requirements
- **Unity 2022.3.44f1 (LTS)**
- Unity Hub (recommended)
- Build modules you need installed via Unity Hub  
  (Windows, macOS, Linux, WebGL, Android, etc.)

### 🔧 Steps

1. Clone the repo:

       git clone https://github.com/ShubaShaba/tall-i.git
       cd ./tall-i

2. Open the project with **Unity 2022.3.44f1** in Unity Hub.

3. In Unity:

   1. `File ▸ Build Settings…`
   2. Select your target platform (Windows, Linux, macOS).
   3. Click **Switch Platform** if needed.
   4. Click **Build**.
   5. Choose an output folder **and type a file name** (e.g., `MyBuild`).

4. Unity places the finished build inside the folder you picked.

## 🙌 Credits

The original project was developed by 4 people ([Finn Lageveen](https://github.com/FinnJoyAlt), [Felipe Carbone Pereira Pinto](https://github.com/carbonara21), [Andreas Aarflot Adli](https://github.com/Andreas-1006) and [Danylo Liashenko](https://github.com/ShubaShaba)(me)). The idea of the core mechanic, levels, custom assets and some parts of the code were devloped by other team members (or in close collaboration with other team members). That version is available in a separate branch ```M5-CreaTe-Submission-2024```.

I ([u/ShubaShaba](https://github.com/ShubaShaba)) authored the majority of the code in the original version and continue to maintain this repository. The current version is being developed as a personal project independent of the original team. All original levels and 3D assets have been discarded. All new 3D assets were taken from [Quaternius – Modular Sci‑Fi Mega Kit](https://quaternius.com/packs/modularscifimegakit.html). 