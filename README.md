# \# Operation Cross-Fire

# 

# A local 2D co-op prototype where two players control one shared ship and defend against enemies and hazards for 60 seconds.

# 

# \## What We Have

# 

# \* One shared player ship

# \* Pilot and Gunner roles for Player 1 and Player 2

# \* Keyboard/mouse controls

# \* Mobile touch controls

# \* Horizontal ship movement

# \* Player aiming and firing

# \* Boost and Shield abilities

# \* Enemy drones

# \* Enemy projectiles

# \* Debris

# \* Breach hazards

# \* Score and hull system

# \* 60-second round timer

# \* Patrol, Alert and Critical phases

# \* Quantum Flux role switching at 20s and 40s

# \* Flux countdown and role-swapped UI

# \* Spawn position checking

# 

# \## Pooling

# 

# Frequently spawned objects use a simple pooling system instead of repeatedly creating and destroying objects during gameplay.

# 

# Currently pooled:

# 

# \* Enemies

# \* Player projectiles

# \* Enemy projectiles

# \* Debris

# \* Breach hazards

# 

# Pooled objects implement `IPoolable` so their state can be reset when spawned and returned.

# 

# \## Role Switching

# 

# Player 1 starts as Pilot and Player 2 as Gunner.

# 

# At 20 seconds:

# 

# \* Roles swap

# \* Difficulty changes to Alert

# \* Breach hazards start spawning

# 

# At 40 seconds:

# 

# \* Roles swap again

# \* Game enters Critical phase

# \* Spawn and projectile difficulty increases

# 

# During Quantum Flux, active movement, firing, abilities and mobile touches are cancelled before the roles change.

# 

# \## Main Systems

# 

# \* `RoundManager` — controls timer, phases, Flux and round ending

# \* `RoleManager` — controls Player 1/Player 2 roles

# \* `MobileInputController` — stores mobile input

# \* `EnemySpawner` — spawns enemies

# \* `DebrisSpawner` — spawns debris

# \* `BreachSpawner` — spawns breaches

# \* `ObjectPool` — handles pooled objects

# \* `PoolReference` / `IPoolable` — manages pool lifecycle

# 

# \## Notes

# 

# The project is intentionally kept simple and focused on gameplay and engineering requirements rather than detailed art or extra features.

# 

# AI tools were used during development for coding assistance and debugging. All generated code was reviewed and tested in Unity.



