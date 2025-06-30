C# Programming Documentation for `Mists of Thelema`
===================================================

**Author:** Šimon Jůza **Date:** 2024

Table of Contents
-----------------

1. [Introduction](#1-introduction)  
2. [System Requirements](#2-system-requirements)  
3. [Installation Guide](#3-installation-guide)  
4. [Program and Mechanics](#4-program-and-mechanics)  
    - [4.1 Project Structure](#41-project-structure)  
    - [4.2 How to Run the Game](#42-how-to-run-the-game)  
    - [4.3 Development Process and Feature Description](#43-development-process-and-feature-description)  
    - [4.4 Key Classes and Modules](#44-key-classes-and-modules)  
    - [4.5 Alternative Solutions and Approaches](#45-alternative-solutions-and-approaches)  
        - [4.5.1 Libraries Used](#451-libraries-used)  
    - [4.6 Input and Output Data Representation](#46-input-and-output-data-representation)  
        - [4.6.1 Input and Output Data](#461-input-and-output-data)  
    - [4.7 Error Handling](#47-error-handling)  
    - [4.8 Testing](#48-testing)  
    - [4.9 Work Progress](#49-work-progress)  
    - [4.10 Possible Extensions](#410-possible-extensions)  
5. [Contribution Guidelines](#5-contribution-guidelines)  
6. [Licensing](#6-licensing)  
7. [Conclusion](#7-conclusion)  

1\. Introduction
----------------

The **Mists of Thelema** project is a video game adventure with interactive and choice-based elements, developed in C# using the Windows Forms graphical user interface. The player explores various scenarios and responds to situations by using items from their inventory and making decisions. The game is designed to be executable on Windows computers with the standard .NET Framework.

2\. System Requirements
-----------------------

To run or develop `Mists of Thelema`, you will need:

-   **Operating System:** Windows 7 or newer (Windows 10/11 recommended).

-   **.NET Framework:** .NET Framework 4.7.2 or later (or compatible .NET runtime if compiling with .NET Core/.NET 5+).

-   **Processor:** Any modern x86 or x64 compatible processor.

-   **RAM:** 4 GB or more.

-   **Disk Space:** Approximately 50 MB for the game files.

-   **Development Environment (for contributors):** Visual Studio 2019 or newer (with .NET desktop development workload installed).

3\. Installation Guide
----------------------

To get a local copy of the project up and running, follow these steps:

1.  **Clone the repository:**

    ```
    git clone https://github.com/your-username/MistsOfThelema.git
    cd MistsOfThelema

    ```

    (Replace `https://github.com/your-username/MistsOfThelema.git` with the actual repository URL if different).

2.  **Open in Visual Studio:** Open the `MistsOfThelema.sln` solution file in Visual Studio.

3.  **Restore NuGet Packages:** Visual Studio should automatically prompt you to restore any missing NuGet packages. If not, right-click on the solution in Solution Explorer and select "Restore NuGet Packages."

4.  **Build the Project:** Build the solution by going to `Build > Build Solution` in the Visual Studio menu, or by pressing `Ctrl+Shift+B`.

4\. Program and Mechanics
-------------------------

The game is structured into different scenes, each represented by a `Form` derived from the WinForms library. The `Program.cs` entry point first opens the `TitleScreen` scene. This form displays the game's logo, an introductory text, and interactive buttons that allow the player to start the main game scene, referred to as "FirstDay". 

**FirstDay + NextDay** is the core gameplay loop. It includes:

-   **Collision detection** between the player character and various game objects (NPCs, houses).

-   **Keyboard input handling** for player movement and interactions (e.g., pressing 'E' to interact).

-   **Player movement boundaries** to prevent the character from leaving the playable area.

-   **Item pickups** for Day 2.

This scene makes use of several custom-created classes to simplify the program's structure and make future extensions easier.

**Transitioning to Scene 2 (`EndOfDay`)**

From FirstDay or NextDay, the player transitions to Scene 2, the `EndOfDay` form. This transition occurs when the player interacts with a specific `Houses` object named "Your House".

In **Scene 2**, a random event is triggered using the `Random` class from the `System` library. These events are also described in an accompanying JSON file, and several functions evaluate these files to determine what should happen. The game currently ends as a soon as not json file with the next day number exists.

### 4.1 Project Structure

The project is organized into the following key directories:

-   **`MistsOfThelema/`**: The root directory of the main project.

    -   **`Assets/`**: Contains custom UI controls and game entities like `CPlayer.cs`, `Npc.cs`, `Houses.cs`, and `ItemPickup.cs`.

    -   **`Functionality/`**: Holds core game logic and utility classes such as `DialogLoader.cs`, `GameManager.cs`, `InvItem.cs`, and `Core.cs`.

    -   **`Resources/`**: Stores game assets like images (`.png`, `.jpg`) and dialogue/scenario JSON files.

    -   **`Scenes/`**: Contains the different `Form` classes representing game scenes, e.g., `TitleScreen.cs`, `FirstDay.cs`, `NextDay.cs`, and `EndOfDay.cs`.

    -   **`Docs/`**: This documentation and user documentation.

    -   **`Program.cs`**: The application's entry point.

    -   **`.csproj`**: The project file.

    -   **`.sln`**: The solution file.

### 4.2 How to Run the Game

After building the project in Visual Studio:

1.  **From Visual Studio:** Press `F5` or click the "Start" button (green triangle) to run the game in debug mode.

2.  **From Executable:** Navigate to the `bin/Debug/` (or `bin/Release/`) folder within your project directory and double-click `MistsOfThelema.exe`.

### 4.3 Development Process and Feature Description

My work on the game began with the creation of individual scenes and pixel art textures for the game objects and backgrounds, which I created in the online application **Piskel**. I then focused on player movement and restricting movement within defined "boundaries" to prevent the player from moving outside the playable area. Subsequently, I worked on the layout of individual elements, including layering, display, and obscuring.

From a functional perspective, I would highlight the loading and processing of JSON files via the `DialogLoader` class, as well as the display of dialogues and interaction with NPCs in FirstDay / NextDay scene.

### 4.4 Key Classes and Modules

Here's a more detailed look at some of the custom classes:

-   **`DialogLoader.cs`**:

    -   **Purpose:** Manages the loading and retrieval of game dialogues and scenario texts.

    -   **Functionality:** Can parse both simple `.txt` files (though JSON is preferred for structured data) and complex JSON structures for dialogues and end-of-day scenarios. It provides methods to fetch specific dialogue nodes based on NPC name and node ID.

    -   **Key Methods:**  `LoadDialogsFromJsonAsync()`, `GetDialogNode()`.

-   **`cPlayer.cs`**:

    -   **Purpose:** Represents the player character.

    -   **Functionality:** Handles player movement, collision detection, inventory management, and health points (HP). It encapsulates player-specific logic and data.

    -   **Key Properties:**  `Inventory`, `HP`.

    -   **Key Methods:**  `GetBounds()`, `AddItem()`.

-   **`Npc.cs`**:

    -   **Purpose:** Represents non-player characters in the game world.

    -   **Functionality:** Inherits from `PictureBox` and includes properties for interaction, such as `InstanceName`. Designed to be interactable by the player.

-   **`Houses.cs`**:

    -   **Purpose:** Represents interactive house objects on the map.

    -   **Functionality:** Similar to `Npc.cs`, it's a visual control that can be interacted with, specifically used for the "Your House" object to trigger the end-of-day sequence.

-   **`InvItem.cs`**:

    -   **Purpose:** Base class for all inventory items.

    -   **Functionality:** Defines common properties for items (e.g., `Name`, `Description`, `Value`). Implements `IInteractable` to allow items to be picked up.

-   **`Core.cs`**:

    -   **Purpose:** Contains core game utilities and interfaces.

    -   **Functionality:** Defines the `IInteractable` interface, which standardizes interaction behavior across different game objects, ensuring consistent handling of player interactions.

    -   **Key Properties:**  `Core.KeyUp`, `Core.KeyDown`, `Core.KeyLeft`, `Core.KeyRight`, `Core.Interact`, `Core.Inventory` (keyboard mappings).

-   **`GameManager.cs`**:

    -   **Purpose:** Manages global game state variables.

    -   **Functionality:** Holds static properties like `CurrentDay` to track the game's progression.

### 4.5 Possible Alternative Solutions and Approaches

The game could have been effectively created in the Unity game engine. However, I did not choose this approach because I had already developed several smaller applications and games in WinForms. Therefore, I decided that I would focus more on programming here than on learning a new platform.

### 4.5.1 Libraries Used

1.  **System**: Fundamental classes for data types and operations.

2.  **System.Drawing**: Graphical objects, colors, fonts.

3.  **System.Windows.Forms**: Creation and management of the Windows Forms user interface.

4.  **System.Collections.Generic**: Generic collections such as `List` and `Dictionary`.

5.  **System.Text**: Text and encoding manipulation.

6.  **System.Text.Json**: Working with JSON data.

### 4.6 Input and Output Data Representation

### 4.6.1 Input and Output Data

The **input data** for the game Mists of Thelema is represented by keyboard presses, specifically WASD keys (movement), E (interaction), I (inventory), and left mouse button clicks.

The **application's output** is the visual aspect of the game itself. In the case of an extension with game saving and decisions, the program's output could also include a "save" file.

### 4.7 Error Handling

The application includes basic error handling, primarily for:

-   **Dialog Loading:** If JSON dialogue files fail to load, a `MessageBox` is displayed, and the application exits gracefully to prevent runtime errors.

-   **Item Placement:** Attempts to place random items on the map include a maximum number of attempts and a warning `MessageBox` if a suitable non-colliding spot cannot be found.

-   **Thread Safety:**  `InvokeRequired` checks are used when updating UI elements from non-UI threads (e.g., after asynchronous dialog loading) to prevent cross-thread operation exceptions.

Further robust error logging and more specific exception handling could be implemented in future iterations.

### 4.8 Testing

During development, testing was primarily manual, focusing on:

-   **Functional Testing:** Verifying that player movement, interactions, dialogue flows, and scenario outcomes work as intended.

-   **Collision Detection:** Ensuring player and item collisions are accurate.

-   **UI Responsiveness:** Checking that the UI updates correctly and timers behave as expected.

No automated unit or integration tests were implemented for this version.

### 4.9 Work Progress

The game itself underwent many transformations, where I encountered both limitations and, conversely, very helpful WinForms tools such as the wide range of usable labels, "textboxes," etc. The biggest development was the transition from text files to JSON files for representing dialogues or scenarios at the end of the day.

### 4.10 Possible Extensions (What Was Not Completed)

In the next version of the game, I intend to focus on a multi-day game progression with extended consequences and new dialogues. Also, using items in dialogues with NPCs, buying items from an NPC with the ID "Shopkeeper," or exchanging items. There is also the possibility of expanding the game area to include a forest, where it would be possible to kill animals and monsters from which items would drop.

5\. Contribution Guidelines
---------------------------

Contributions are welcome! If you wish to contribute to `Mists of Thelema`, please follow these guidelines:

1.  **Fork the repository.**

2.  **Create a new branch** for your feature or bug fix: `git checkout -b feature/your-feature-name` or `bugfix/issue-description`.

3.  **Make your changes** and ensure they adhere to the existing coding style.

4.  **Write clear, concise commit messages.**

5.  **Test your changes** thoroughly.

6.  **Submit a Pull Request** to the `main` branch, describing your changes and their purpose.

6\. Conclusion
--------------

The creation of this application itself taught me a lot, and I believe I better understood many concepts from lectures and exercises. In practice, I was able to test the usefulness of many constructs on a larger program, for which I am truly grateful. At the same time, I decided that I would continue to work on the game even outside the scope of this subject.
