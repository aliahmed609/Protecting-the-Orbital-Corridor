A 2D local co-op space shooter built in Unity where two players control one shared interceptor.

Gameplay
Pilot: Movement and Boost
Gunner: Aiming, Firing, and Shield
Quantum Flux: Switches the players' roles and increases difficulty.
Enemies, debris, and breaches spawn throughout the round.
Controls
Mobile

The screen is split between Player 1 and Player 2, with multi-touch support for simultaneous controls.

Pilot → Movement + Boost
Gunner → AimPad + Fire + Shield
AimPad uses relative movement, allowing the Gunner to aim without moving their finger across the screen.
PC
A / D → Move
Space → Boost
Right Mouse Button → Shield
Architecture

The project uses separate systems for roles, movement, aiming, weapons, abilities, spawning, and input.

Key systems include:

RoleManager — Handles player roles and Quantum Flux switching.
PlayerShipController — Handles movement and screen boundaries.
GunnerAim — Handles aiming and reticle movement.
PlayerWeapon — Handles weapon firing.
PlayerBoost / PlayerShield — Handle abilities and cooldowns.
MobileInputController / PCInputController — Handle platform-specific input.
Spawner systems handle enemies, debris, breaches, and projectiles.

Object pooling is used for frequently spawned objects to reduce Instantiate/Destroy overhead.

Performance & Screen Support

The game is designed around a 1920 × 1080 (16:9) reference resolution, with responsive UI scaling for different screen sizes and aspect ratios. Camera-based movement boundaries help keep the player within the visible play area.

AI Usage

AI was used as a development assistant for code structure, debugging, input implementation, optimization discussions, and troubleshooting. All code was reviewed, tested, and integrated manually.
