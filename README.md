# Delmon Diver
## Trello 
- [Trello - SCRUM](https://trello.com/b/YFdQKg9N/s6g3)
- [Trello - Game concept](https://trello.com/b/L5mdAE1y/s6g3-game-concept)

## File Structer
```
.
├── Build/                   # game executable file
├── Delmon Diver/            # unity file
└── Project File/            # note
```

**Unity File Structer**
```
Delmon Diver/
├── Assets/
│   ├── Animations/          # animation Clips & Controllers
│   ├── Audio/               # music and SFX
│   ├── Fonts/               # font to use for UI
│   ├── Materials/           # shaders and surface definitions
│   ├── Prefabs/             # reusable GameObjects (Boats, Characters, Creatures)
│   ├── Scenes/              # game levels
│   ├── ScriptableObjects/   # data containers for XP, Items, and Balancing
│   ├── Scripts/             # C# logic
│   │   ├── AI/              # LLM behaviors
│   │   ├── Enemies/         
│   │   ├── Managers/        # game, audio, and UI singletons
│   │   ├── Player/          
│   │   ├── Progression/     # XPManager, LevelProgression, and SaveLoad System
│   │   └── Systems/         # inventory, crafting, and boat controls
│   ├── Shaders/             
│   ├── Sprites/             # 2D textures and UI icons, raw *.png
│   ├── UI Toolkit/          # UXML and USS file
│   └── Visual Effects/     
└── ThirdParty/              # external Asset Store packages
```
## IDE Setup
### Rider
1. open Unity: `Edit > Preferences`
2. go to `External Tools`
3. set External Script Editor to **JetBrains Rider**
4. check these boxes: 
   - embedded packages
   - local packages
   - registry packages
   - git packages

5. open Rider: go to `Settings` > Plugins
6. installed `Unity Support`

7. if scripts aren't loading, close Rider and unity. re-open unity and open C# Project.

### VS Code
1. open Unity: `Edit > Preferences`
2. go to `External Tools`
3. set External Script Editor to **Visual Studio Code**
4. check these boxes: 
   - embedded packages
   - local packages
   - registry packages
   - git packages

5. open VS code: go to `Extension` 
6. installed `Unity` it automatically installs the `C# Dev Kit `

7. if scripts aren't loading, close Rider and unity. re-open unity and open C# Project.