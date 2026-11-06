# Unity Endless Runner

A starter 3D endless-runner Unity project for PC. The project creates a playable scene automatically when opened in Unity.

## Requirements
- Unity 2022.3 LTS or Unity 6
- Windows / macOS / Linux editor

## Open the project
1. Extract this ZIP.
2. In Unity Hub, choose **Add > Add project from disk**.
3. Select the extracted `Unity-Endless-Runner` folder (the folder containing `Assets`, `Packages`, and `ProjectSettings`).
4. Open it in Unity.
5. The editor setup script creates `Assets/Scenes/Main.unity` automatically.
6. Press Play.

## Controls
- A / Left Arrow: move left
- D / Right Arrow: move right
- Space / Up Arrow: jump
- S / Down Arrow: slide
- R: restart after game over

## Project layout
- `Assets/Scripts/`: player, spawner, obstacle, coin and game logic
- `Assets/Editor/`: creates a playable starter scene on first editor load
- `Packages/manifest.json`: Unity package manifest
- `ProjectSettings/ProjectVersion.txt`: editor version hint

This is a self-contained starter project using Unity primitives; replace the primitive visuals with your own art whenever you like.
