# SpaceDuel
A simple example of kspaceduel game using [Entity Component System (ECS)](https://en.wikipedia.org/wiki/Entity_component_system) implementation by [Morpeh](https://github.com/scellecs/morpeh) for gameplay logic and Unity Engine for rendering, physics, resource management and other.
# Gameplay
#TODO: Create and Add gameplay gif
## How To Play
### Part 1: Game Overview
In the arcade game "Space Duel," players each take control of a spaceship with various capabilities. These ships can execute maneuvers such as turning, accelerating, firing projectiles, and deploying mines. However, these actions consume energy, and each spaceship has a limited energy reserve.

### Part 2: Energy Management
To replenish energy, spaceships rely on solar panels. The amount of energy a ship receives depends on its proximity and orientation to the sun. When closer to the sun, a ship gains more energy, whereas it receives less energy near the battlefield's borders. The optimal energy gain occurs when the sun's rays directly hit the ship's batteries. Energy intake is reduced at oblique angles and becomes nonexistent when the sun shines at the ends of the batteries.

### Part 3: Consequences of Energy Depletion
Running out of energy leads to a loss of control, rendering the ship unable to fire or perform maneuvers. Additionally, collisions with projectiles, mines, or other ships can damage a spaceship, depleting its health. In the event of two ships colliding, the weaker ship is destroyed, while the stronger one sustains damage equal to the weaker ship's health value plus an additional amount known as "Crash Damage."

### Part 4: Hazards and Solar Dynamics
A ship meets its demise if it flies directly into the sun. Projectiles, including shells, follow paths similar to the ships as they orbit the sun. Mines have their own energy reserves to remain stationary, and when that energy is exhausted, they fall into the sun. Mines positioned closer to the sun require more energy to maintain their position and are more susceptible to solar attraction.

### Part 5: Interactions and Power-ups
Projectiles can destroy mines, adding an element of strategy and timing to the gameplay. Periodically, refueling stations emerge on the battlefield, offering an opportunity for players to replenish their energy and continue their space duel.

### Control
- **W** / **Up Arrow** - accelerate spaceship.
- **AD** / **Left Arrow/ Right Arrow** - turn direction
- **Space** / **<** - primary weapon shot
- **Left Ctrl** / **>** - secondary weapon shot
All control logic is encapsulated in a class system [InputSystem](Assets/Scripts/Engine/Input/Systems/InputSystem.cs).

# Architecture
An important issue is how the core logic communicates with the engine's external logic. For dependency inversion uses interfaces within the core assembly referenced by the engine assembly. Thus, the core assembly does not know anything about the game engine used. ECS dictates a flat architecture and there is nothing special in core assembly. 
