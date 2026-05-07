# AI_CONTEXT.md — Delmon Diver

> **Purpose**: This file provides AI coding assistants with full project context so they can contribute effectively without repeated explanations.

---

## Project Overview

**Delmon Diver** is a **3D level-based survival adventure game** built in **Unity (C#)** as a university group project for the course **IT8101: Game Development** at **Bahrain Polytechnic** (Department of ICT, Section 6 Group 3).

### Team Members

| Name               | Student ID  |
|--------------------|-------------|
| Batool AlBonni     | 202301567   |
| Abbas Kadhem       | 202303883   |
| Hoor Hasan         | 202301820   |
| Mohamed Abdulla    | 202304048   |
| Abdulla Ebrahim    | 202302970   |

**Instructor**: Dr. Haetham Alhaddad

### Game Premise

The player is a **diver** whose boat breaks in the middle of the sea during a storm. He must survive, collect resources, craft tools, build boats, explore islands and underwater areas, and ultimately **return to his home island**. An optional **treasure-hunting side story** can change the game's ending.

The game is culturally inspired by **Bahrain's pearl diving traditions and maritime heritage** (the ancient Dilmun/Delmon civilization). Visual references include *Subnautica*, *Uncharted 3*, *Ark: Survival Evolved*, and *Sea of Thieves*.

---

## Tech Stack

| Component            | Technology                                      |
|----------------------|-------------------------------------------------|
| **Engine**           | Unity **6000.1.13f1** (Unity 6)                 |
| **Language**         | C#                                              |
| **Render Pipeline**  | Universal Render Pipeline (URP) **17.3.0**      |
| **Input**            | Unity Input System **1.19.0**                   |
| **UI**               | Unity uGUI (Canvas-based) + UI Toolkit (UXML/USS) |
| **Dialogue/Narrative** | **Fungus** (visual novel / flowchart scripting) |
| **Timeline**         | Unity Timeline **1.8.12**                       |
| **AI Navigation**    | Unity AI Navigation **2.0.11**                  |
| **Version Control**  | Git + GitHub                                    |
| **IDEs**             | JetBrains Rider or VS Code (see README.md)      |
| **Target Platform**  | PC (Windows) — keyboard & mouse primary, controller secondary |

### Third-Party Assets

| Asset                              | Location in Project                          |
|------------------------------------|----------------------------------------------|
| LeartesStudios – Underwater Ship   | `Assets/ThirdParty/LeartesStudios/` and `Assets/LeartesStudios/` |
| Low Poly Tropical Environment LITE | `Assets/LowPolyTropicalEnvironment_LITE/`    |
| Fungus (dialogue system)           | `Assets/Fungus/`                             |
| Input Sprites for TextMesh Pro     | `Assets/Input Sprites for TextMesh Pro/`     |

### Fonts Used

- **Cinzel** (Regular)
- **Cinzel Decorative** (Regular, Bold)
- **IM Fell English** (Regular) — also has a TMP font asset

---

## Repository Structure

```
IT8101-Delmon-Diver/
├── Build/                       # Game executable output (currently empty)
├── Delmon Diver/                # Unity project root
│   ├── Assets/
│   │   ├── Animations/          # Animation Clips & Controllers
│   │   │   ├── Creatures/
│   │   │   ├── Environment/
│   │   │   ├── NPC/
│   │   │   └── Player/
│   │   ├── Audio/
│   │   │   ├── Master.mixer     # AudioMixer with "MusicVolume" & "SFXVolume" params
│   │   │   ├── Music/
│   │   │   └── Sound/
│   │   ├── Fonts/
│   │   ├── Fungus/              # Fungus dialogue plugin (DO NOT modify)
│   │   ├── Materials/
│   │   ├── Prefabs/
│   │   │   ├── Boats/
│   │   │   ├── Characters/
│   │   │   ├── Environments/
│   │   │   ├── Land Creatures/
│   │   │   ├── Sea Creatures/
│   │   │   ├── Tools/
│   │   │   └── UI/
│   │   ├── Scenes/              # All game levels (see Scene Flow below)
│   │   ├── ScriptableObjects/   # Data containers (XP, Items, Balancing) — mostly empty stubs
│   │   ├── Scripts/             # C# game logic
│   │   │   ├── AI/              # LLM-based parrot companion (on LLM_Parrot branch)
│   │   │   ├── Audio/           # Sound & music management
│   │   │   ├── Enemies/         # Enemy scripts (stub)
│   │   │   ├── Managers/        # Game managers (MainMenuManager, etc.)
│   │   │   ├── Player/          # Player movement & interaction (stub)
│   │   │   ├── Progression/     # XPManager, LevelProgression, SaveLoadManager (stubs)
│   │   │   ├── Systems/         # Inventory, crafting, boat controls (stub)
│   │   │   └── UI/              # UI helpers (AutoTextScroll, ButtonHoverUI)
│   │   ├── Shaders/
│   │   ├── Sprites/
│   │   │   ├── IntroStorySprites/
│   │   │   ├── MainMenu/
│   │   │   ├── MapIntro/
│   │   │   ├── gameLogo.png
│   │   │   └── shockedDiver.png
│   │   ├── Settings/            # URP & rendering settings
│   │   ├── ThirdParty/          # External Asset Store packages
│   │   ├── UI Toolkit/          # UXML and USS files
│   │   ├── Visual Effects/
│   │   └── _Recovery/           # Recovered scene backups
│   ├── Packages/
│   │   └── manifest.json
│   ├── ProjectSettings/
│   └── Delmon Diver.sln
├── Project File/                # Notes (empty, .gitkeep only)
├── README.md
└── AI_CONTEXT.md                # ← This file
```

---

## Scene Flow & Build Order

The game scenes are loaded in this order (as configured in `EditorBuildSettings.asset`):

```
IntroStory → MainMenu → L1_Map → L1_BrokenBoat → L1ToL2_Map → L2_SmallIsland
→ L2ToL3_Map → L3_OpenSea_ToMain → GameEnding
```

### Scene Naming Convention

| Scene Name                | Purpose                                          |
|---------------------------|--------------------------------------------------|
| `IntroStory`              | Narrative intro (Timeline-driven), auto-loads MainMenu |
| `MainMenu`                | Main menu with Start, Controls, Settings, Credits, Quit |
| `L1_Map`                  | Map/video transition into Level 1                |
| `L1_BrokenBoat`           | **Level 1** — Escape the sinking ship            |
| `L1ToL2_Map`              | Map transition between Level 1 → 2               |
| `L2_SmallIsland`          | **Level 2** — Small island, build a boat         |
| `L2ToL3_Map`              | Map transition between Level 2 → 3               |
| `L3_OpenSea_ToMain`       | **Level 3** — Open sea journey to main island    |
| `L3ToL4_Map`              | Map transition between Level 3 → 4               |
| `L4_MainIsland`           | **Level 4.1** — Main survival island (Delmon)    |
| `L4ToL4.2_Map`            | Map transition to optional cave level             |
| `L4.2_UnderwaterCave`     | **Level 4.2** — Optional treasure cave + boss    |
| `L4.2ToL5_Map`            | Map transition from cave → Level 5               |
| `L4toL5_Map`              | Map transition from island → Level 5 (no cave)   |
| `L5_OpenSea_ToHome`       | **Level 5** — Final sea journey home             |
| `GameEnding`              | End screen (outcome depends on treasure choice)  |

> **Map scenes** (`*_Map`) play a video/animation then auto-load the next gameplay scene using `LoadAfterMap.cs` (VideoPlayer finish callback) or `LoadMainMenu.cs` (Timeline finish callback).

---

## Level Design Summary

### Level 1: The Broken Boat
- **Setting**: Damaged ship in a storm at sea
- **Objective**: Escape the sinking ship
- **Difficulty**: Easy
- **Mechanics**: Parkour/movement through debris, time pressure (ship sinking)
- **Characters**: Player (Diver)

### Level 2: Small Island
- **Setting**: Tiny island in the Persian Gulf
- **Objective**: Collect resources, build a small boat
- **Difficulty**: Easy
- **Mechanics**: Resource collection, crafting introduction, shelter building
- **Characters**: Player, Parrot (AI Guide introduced here)

### Level 3: Open Sea Journey → Main Island
- **Setting**: Open sea
- **Objective**: Sail to the main island, survive the journey
- **Difficulty**: Medium
- **Mechanics**: Boat sailing, boat repair, weather hazards, sea creature encounters
- **Characters**: Player, Parrot, Sea Creatures

### Level 4.1: Main Survival Island (Delmon)
- **Setting**: Large island with diverse biomes
- **Objective**: Survive, gather resources, build a better boat, find treasure map (optional)
- **Difficulty**: Medium–Hard
- **Mechanics**: Full survival (food, shelter, tools), exploration, combat with wildlife
- **Characters**: Player, Parrot, Wild Animals (gazelle, birds, falcon, lion, wolf)

### Level 4.2: The Cave (Optional)
- **Setting**: Underwater cave on the main island
- **Objective**: Find and defeat the Treasure Keeper, claim the treasure
- **Difficulty**: Hard
- **Mechanics**: Diving, oxygen management, puzzles/traps, boss fight
- **Characters**: Player, Sea Creatures, Treasure Keeper (Guardian — boss)

### Level 5: Open Sea Journey → Home Island
- **Setting**: Open sea (final stretch)
- **Objective**: Survive the journey home
- **Difficulty**: Hard
- **Mechanics**: Advanced sea navigation, pirate attacks, storm weather, boat management
- **Characters**: Player, Pirates (NPCs), Sea Creatures

### Endings
- **Took treasure** → Rich & famous "Nokheda" ending
- **Did not take treasure** → Safe return home as a humble diver

---

## Characters

| Character          | Role                         | Notes                                      |
|--------------------|------------------------------|--------------------------------------------|
| **The Diver**      | Player character             | Courageous, resourceful, curious           |
| **The Parrot**     | AI Guide / Companion         | LLM-powered, witty, knowledgeable about Bahraini culture |
| **Pirates**        | Enemies (Level 5)            | AI-controlled, board/attack player's boat  |
| **Treasure Keeper**| Boss (Level 4.2)             | Guardian of the cave treasure              |
| **Land Creatures** | Wildlife (Level 4)           | Gazelle, birds, falcon, lion, wolf         |
| **Sea Creatures**  | Environmental threats (L3/5) | Sharks, large fish, territorial fauna      |

---

## Implemented Systems (Current State)

### ✅ Implemented / In Progress
- **Main Menu** — Full UI with Start, Controls, Settings, Credits, Quit (`MainMenuManager.cs`)
- **Audio System** — `MusicManager` (singleton, crossfade), `SoundManager` (2D/3D SFX), `MusicLibrary`/`SoundLibrary` (name→clip lookup), `MainAudio` (volume sliders + PlayerPrefs persistence), `MainMenuAudio`
- **Scene Flow** — `LoadAfterMap.cs` (video→next scene), `LoadMainMenu.cs` (timeline→next scene)
- **Credits** — Auto-scrolling credits panel (`CreditsAutoScroll` / `AutoTextScroll.cs`)
- **Button Hover** — Font size change on hover (`ButtonHoverUI.cs`)
- **Intro Story** — Timeline-driven narrative introduction
- **Level 1 Map Transitions** — Video-based map intros between levels
- **Fungus Dialogue** — Level 1 intro narrative using Fungus flowcharts
- **Ship Environment** — LeartesStudios underwater ship asset integrated
- **Character & Movement** — Basic character added (on `testShip`/`Batool` branch)
- **Settings** — Audio mixer, display settings, resolution, volume controls (on `Mohamed2` branch)

### 🔲 Stub / Not Yet Implemented
- `LevelProgression.cs` — Empty stub
- `SaveLoadManager.cs` — Empty stub
- `XPManager.cs` — Empty stub
- `Scripts/Player/` — Empty (`.gitkeep` only)
- `Scripts/Enemies/` — Empty (`.gitkeep` only)
- `Scripts/Systems/` — Empty (`.gitkeep` only)
- `Scripts/AI/` — Empty on main (LLM Parrot work is on `LLM_Parrot` branch)
- Inventory, Crafting, Combat, Weather, Diving mechanics — not yet implemented
- Reward system — basic foundation on `Abdulla` branch

---

## Git Branching Strategy

The team uses **per-member feature branches** that merge into integration branches.

### Branch Overview

| Branch                     | Owner/Purpose                                                    | Status       |
|----------------------------|------------------------------------------------------------------|--------------|
| `main`                     | Stable baseline (initial project setup)                          | Base         |
| `testShip` ★               | **Currently checked out**. Character, movement, loading, sprites, environment detection | Active       |
| `Batool`                   | Character + movement, loading screen, sprites, ship environment  | Active       |
| `Abbas`                    | Base only (no additional commits)                                | Inactive     |
| `Hoor`                     | Intro story, main menu, UI                                      | Merged       |
| `Mohamed`                  | Water, boat, main menu                                          | Merged       |
| `Mohamed2`                 | Settings (audio, display, resolution, volume, mute, reset)       | Active       |
| `Abdulla`                  | Basic reward system foundation                                  | Feature      |
| `Settings`                 | Audio mixer, settings menu                                      | Merged→Hoor  |
| `Fungus_Level1_Intro`      | Level 1 narrative intro using Fungus, scene renaming, map scenes | Merged       |
| `merge_level1`             | Integration branch merging Fungus + main menu + settings + map videos | Integration |
| `LLM_Parrot`               | LLM-based AI parrot companion scripts, chat UI                  | Feature      |
| `the_ship_level1`          | Water shader, buoyancy system, ship scripts                     | Feature      |

### Branching Conventions
- Personal branches are named after team members (e.g., `Batool`, `Abbas`, `Mohamed2`)
- Feature branches are descriptive (e.g., `LLM_Parrot`, `Fungus_Level1_Intro`)
- `merge_level1` serves as the Level 1 integration branch
- PRs are used for cross-branch merges (e.g., PR #1 merged `Mohamed2` into `merge_level1`)

---

## Code Architecture & Patterns

### Singletons
Both `MusicManager` and `SoundManager` use the singleton pattern with `DontDestroyOnLoad`:
```csharp
public static MusicManager Instance;
void Awake() {
    if (Instance != null) Destroy(gameObject);
    else { Instance = this; DontDestroyOnLoad(gameObject); }
}
```

### Audio System Design
```
MusicManager (singleton)
├── MusicLibrary (trackName → AudioClip mapping)
└── AudioSource (crossfade between tracks)

SoundManager (singleton)
├── SoundLibrary (groupID → AudioClip[] with random selection)
└── AudioSource (2D playback)
    + PlayClipAtPoint for 3D positional audio

MainAudio
├── AudioMixer ("MusicVolume", "SFXVolume" exposed params)
└── PlayerPrefs persistence for volume settings
```

### Scene Loading Pattern
- **Video transitions**: `LoadAfterMap` listens to `VideoPlayer.loopPointReached`, waits `delay` seconds, then loads `nextScene`
- **Timeline transitions**: `LoadMainMenu` listens to `PlayableDirector.stopped`, then loads `nextScene`
- **Menu navigation**: `MainMenuManager.StartGame()` directly calls `SceneManager.LoadScene("L1_Map")`

### UI Pattern
- Canvas-based UI with `CanvasGroup` for fade transitions
- Panel show/hide via `SetActive(true/false)`
- Fade coroutine using `Mathf.Lerp` on `CanvasGroup.alpha`

---

## Planned Core Systems (From GDD)

These are the MVP systems described in the Game Design Document:

1. **Player Movement** — Walking, jumping, swimming on surface (KBM + controller)
2. **Parkour Traversal** — Vaulting, wall-climb, ledge grab (context-sensitive)
3. **Diving** — Dive key, underwater swimming, oxygen/air timer, buoyancy physics
4. **Combat** — Basic melee attack + block, enemy hit reactions, player health
5. **Enemy AI** — State machine (idle → pursue → attack), patrol areas
6. **Level Objectives** — HUD tracker (list style), trigger-based completion
7. **Level Progression** — Complete objective → next level loads
8. **Core UI/HUD** — Health bar, oxygen bar (diving), objective tracker, pause menu, inventory

### Planned Custom Features
1. **Journey Guide (LLM AI Parrot)** — Real-time conversational AI, context-aware advice, Bahraini cultural facts, voice/text interaction
2. **Inventory System** — Categorized (Tools, Artifacts), quick-access hotbar, artifact codex with lore
3. **Current Location Map** — Fog of war, objective markers, weather-affected visibility
4. **Combat System** — Dual modes: land (melee) vs underwater (slow/strategic)
5. **Weather System** — Sandstorms, storms, heat waves, visibility reduction, water currents, enemy behavior changes

---

## Control Scheme Reference

### PC (Keyboard & Mouse)
| Action               | Key                    |
|----------------------|------------------------|
| Move                 | WASD / Arrow Keys      |
| Jump (Land) / Swim Up (Sea) | Space           |
| Run                  | Left Shift             |
| Dive Down (Sea)      | Left Ctrl              |
| Primary Action       | Left Mouse Button      |
| Secondary Action     | Right Mouse Button     |
| Interact             | E                      |
| Use Tool             | F                      |
| Repair               | R                      |
| Pause / Menu         | Escape                 |
| Camera               | Mouse Movement         |

### Controller (Xbox / PlayStation)
| Action               | Xbox           | PlayStation    |
|----------------------|----------------|----------------|
| Move                 | Left Stick     | Left Stick     |
| Camera               | Right Stick    | Right Stick    |
| Run                  | L3             | L3             |
| Jump / Swim Up       | A              | X              |
| Dive Down            | B              | Circle         |
| Primary Action       | RT             | R2             |
| Secondary Action     | LT             | L2             |
| Repair               | Y              | Triangle       |
| Interact             | X              | Square         |
| Use Tool             | RB             | R1             |
| Pause / Menu         | Menu           | Options        |

---

## Visual & Art Direction

- **Visual Style**: Semi-realistic 3D, inspired by *Subnautica*
- **Camera**: Third-person perspective
- **Color Palettes**: Distinct per environment:
  - **Underwater General** — Blue-green, sunlit, clear
  - **Underwater Cave** — Dark, moody, flashlight-dependent
  - **Island** — Warm, tropical, natural greens & sandy tones
- **Lighting**: Dynamic per area (sunlit surface vs dark caves)
- **Effects**: Water waves, spray, rain, lightning, screen shake, particle effects on interactions

---

## Audio Design

### Music Tracks (Referenced in GDD)
- **Exploration**: "Omega" by Scott Buckley
- **Cave Diving**: "Thriller" by NastelBom
- **Combat**: "Epic3" by SenorMusica81

### Sound Effects by Level
| Level | Sound Effects |
|-------|---------------|
| L1    | Breaking wood, crashing ship, rushing water, captain shouts, thunder |
| L2    | Light ocean waves, soft wind, quiet ambient |
| L3    | Strong waves, rain |
| L4    | Birds, wind through trees, water, animal sounds |
| L4.2  | Echoes, dripping water, low ambient noise |
| L5    | Strong winds, rough waves, pirate ship sounds, distant combat |

---

## Development Conventions

### File Naming
- Scenes: `L{number}_{DescriptiveName}.unity` (e.g., `L1_BrokenBoat.unity`)
- Map transitions: `L{from}ToL{to}_Map.unity`
- Scripts: PascalCase class names matching filenames

### Folder Organization
- New prefabs go in the appropriate `Prefabs/` subfolder
- Scripts must be placed in the correct `Scripts/` subfolder by domain
- Third-party assets go in `ThirdParty/` or at the asset root if from Unity Asset Store
- Empty directories use `.gitkeep` to preserve folder structure in Git

### Unity Preferences
- Scenes must be added to **Build Settings** in the correct order (see `EditorBuildSettings.asset`)
- Audio mixer uses exposed parameters named `"MusicVolume"` and `"SFXVolume"`
- Volume persistence uses `PlayerPrefs` with keys `"MusicVolume"` and `"SFXVolume"`

---

## Quick Reference: Key File Paths

| What                    | Path                                               |
|-------------------------|----------------------------------------------------|
| Unity Project Root      | `Delmon Diver/`                                    |
| Solution File           | `Delmon Diver/Delmon Diver.sln`                    |
| All Scenes              | `Delmon Diver/Assets/Scenes/`                      |
| All Scripts             | `Delmon Diver/Assets/Scripts/`                     |
| Main Menu Manager       | `Delmon Diver/Assets/Scripts/Managers/MainMenuManager.cs` |
| Audio Scripts           | `Delmon Diver/Assets/Scripts/Audio/`               |
| Progression Stubs       | `Delmon Diver/Assets/Scripts/Progression/`          |
| UI Scripts              | `Delmon Diver/Assets/Scripts/UI/`                  |
| Package Manifest        | `Delmon Diver/Packages/manifest.json`              |
| Build Settings          | `Delmon Diver/ProjectSettings/EditorBuildSettings.asset` |
| Input Actions           | `Delmon Diver/Assets/InputSystem_Actions.inputactions` |
| Audio Mixer             | `Delmon Diver/Assets/Audio/Master.mixer`           |

---

## Important Notes for AI Agents

1. **Currently on `testShip` branch** — This is the active working branch with character, movement, and ship environment work. The `main` branch only has the initial project setup.

2. **Many systems are stubs** — `LevelProgression`, `SaveLoadManager`, `XPManager`, `Player/`, `Enemies/`, `Systems/` are scaffolded but empty. Check which branch has relevant WIP work before implementing.

3. **Check feature branches before implementing** — Work may already exist on a personal branch:
   - LLM/AI Parrot → `LLM_Parrot` branch
   - Water/Buoyancy → `the_ship_level1` branch
   - Settings/Audio → `Mohamed2` branch
   - Rewards → `Abdulla` branch

4. **Fungus plugin is read-only** — Do not modify files in `Assets/Fungus/`. Fungus flowcharts are used for Level 1 narrative.

5. **Unity 6 (6000.x)** — This project uses the latest Unity 6 version. APIs may differ from Unity 2022/2023 tutorials.

6. **URP rendering** — All materials and shaders must be URP-compatible.

7. **The game is a university project** — Keep code clean and well-documented. The team is learning, so prefer clear patterns over overly complex abstractions.

8. **Cultural sensitivity** — The game represents Bahraini maritime heritage. Respect the cultural context when implementing dialogue, lore, or character interactions.
