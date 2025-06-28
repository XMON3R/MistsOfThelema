using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MistsOfThelema
{
    /// <summary>
    /// A static class to manage global game-related variables and states.
    /// This ensures that critical game data is accessible from anywhere in the application.
    /// </summary>
    public static class GameManager
    {
        /// <summary>
        /// Gets or sets the current in-game day. The game starts on Day 1.
        /// This property can be accessed and modified globally to track the progression of time in the game.
        /// </summary>
        public static int CurrentDay { get; set; } = 1; // Start on Day 1
    }
}