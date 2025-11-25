# LockedInTheCode
- 3D Escape Room Game
- 🔐 Escape Room Puzzle Game – Detailed Scope

Link to current gameplay video (as of 11/24/25): https://youtu.be/8MvktXqKlqU?si=vaIq7KSGaNCN55fD

## 🎯 Goal:
Escape a futuristic lab by solving puzzles in three themed rooms before the system shuts down.

## 🧩 Game Structure:
- Room 1: Control Room
  - Puzzle 1: Keypad code (find digits hidden in the room)
  - Puzzle 2: Button sequence (based on blinking lights)
  - Puzzle 3: Object placement (drag items to correct slots)
- Room 2: Power Core
  - Puzzle 1: Laser redirection (rotate mirrors to hit targets)
  - Puzzle 2: Circuit connection (connect wires to restore power)
  - Puzzle 3: Pressure plates (step on plates in correct order)
- Room 3: Memory Vault
  - Puzzle 1: Audio clue matching (playback logs to find correct sequence)
  - Puzzle 2: Symbol decoding (match symbols to meanings)
  - Puzzle 3: Maze navigation (find exit using clues from previous rooms)

## 🧠 Mechanics:
- Physics system: Pick up and use items (keys, tools, notes)
- Trigger system: Activate puzzles, doors, and effects
- Timer (optional): Escape within 10 minutes or restart
- Hint system (optional): Limited hints per room

## 🌆 Environment:
- Sci-fi lab with flickering lights, glitch effects, and ambient sounds
- Each room has distinct lighting and color palette
- Use particle effects and sound cues for feedback

## 🧭 Progression:
- Solve all puzzles in a room to unlock the next
- Final escape triggers a cinematic or message
- Optional: hidden collectibles or bonus room

## 🎮 Controls
**Player Movement**
- WASD: Horizontal movement (forward, left, backwards, and right)
- Space bar: Jump

**Camera Movement**
- Camera moves based on cursor movement
- Click screen to lock cursor
- Esc: Unlock cursor
- 1: Switch to first-person mode
- 2: Switch to third-person mode
In first-person mode, point the crosshair and click on the object you intend to pickup. This will make the object freeze in place in front of the character, allowing the player to “hold” it. When an object is held, it will follow the player’s camera movements that are dependent on the mouse input. To drop the object currently held, click again.

## 🎯 General Goals for each person
**Antonio**
- Scripts for puzzle systems (locked doors, keys, object triggers, etc).
- Implement puzzle logic and connections between puzzles and room progression.
- Create reusable puzzle prefabs and communicate with level design to place them correctly.
- Level design and direction.
- Handles merging teammate contributions in GitHub.

**Simmon**
- Assist in writing scripts & creating game objects for puzzle systems (ordered button activations, hidden keys/objects, hidden passcodes, moving platforms, etc).
- Assist in designing the room layout and platforming segments for each level.
- Test gameplay to ensure puzzles and physics of interactable objects work as intended within each room/level.
- Handle game UI components (level selection screen, countdown timer until game over, etc).

**Deeni**
- Import any wanted art assets into the game as models, props, and environment pieces.
- Set up animations for environmental elements, such as doors, moving objects, and lights.
- Apply visual polish to each room (lighting tweaks, material setup, scene decoration, and environmental effects).
- Help ensure each themed level (jungle, dungeon, scifi, etc.) is cohesive.

**Ryan**
- Implement and manage sound effects and background music.
- Set up audio objects to trigger sound cues for puzzles, doors, buttons, pickups, UI, and other gameplay elements.
- Create an option that allows users to manage the volume of both music and sound effects.
