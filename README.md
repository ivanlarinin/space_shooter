# SpaceShooter

A small 2D space shooter game built with **Unity**.

## Features

* Multiple ships with different stats (speed, agility, hitpoints).
* Power-ups (shields, energy, ammo, weapons).
* Levels with enemies, asteroids, and debris spawners.
* Score, kills, and lives tracking with UI indicators.
* Level goals and conditions (score, position, survival).
* Simple parallax background and camera follow system.

## How to Run

1. Open the project in **Unity** (2021+ recommended).
2. Load the `main_menu` scene.
3. Press **Play** in the Unity editor.

## Controls

* **W / Up Arrow** – Thrust
* **A / D / Left / Right Arrows** – Rotate ship
* **Space / Left Mouse** – Fire primary weapon
* **Right Mouse** – Fire secondary weapon
* **Esc** – Pause menu

## Project Structure

* `Core/Entities/` – Ships, player, destructibles, shields.
* `Combat/Weapons/` – Turrets, projectiles, damage.
* `Bonus/Powerups/` – Pickups and buffs.
* `UI/` – Menus, HUD, indicators.
* `Spawner/Level/` – Enemy/asteroid spawning, boundaries.
* `Utilities/` – Level logic, singletons, services.
