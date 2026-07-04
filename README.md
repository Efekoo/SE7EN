# SE7EN

A **2D isometric action-RPG** inspired by Dante's *Inferno*, built in Unity. Fight through layered levels of hell, defeat bosses, loot and craft items, complete quests, and progress deeper with each layer.

**Tech:** Unity 6 (6000.2.8f1) · C# · Universal Render Pipeline · Unity Input System · TextMeshPro

## Gameplay systems

- **Combat** — isometric melee combat with animation-driven attacks and enemy AI
- **Boss encounters** — bosses with reveal mechanics, dedicated health UI, and music transitions
- **Inventory & hotbar** — item pickup, storage, and quick-use slots
- **Loot & crafting** — item drops from enemies, crafting from collected materials
- **Shops & quests** — vendors and a quest-tracking system driving progression
- **Level progression** — layered levels with traps, moving/collapsing platforms, and scene transitions

## Project structure

```
SE7EN/                       # Unity project root
└── Assets/
    ├── Scripts/             # Gameplay systems (character, bosses, platforms, UI, items)
    ├── Character/           # Player art and animations
    ├── Objects/             # Interactable and environment objects
    ├── Scenes/              # Menu and level scenes
    └── UI/                  # HUD, menus, boss health bar
```

## Opening the project

1. Install **Unity 6000.2.8f1** via Unity Hub
2. Open the `SE7EN/` folder as a project
3. Load the main menu scene from `Assets/Scenes` and press Play
