using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MistsOfThelema
{
    /// <summary>
    /// Defines an interface for any object in the game world that the player can interact with.
    /// This includes NPCs, houses, and other items.
    /// </summary>
    public interface IInteractable
    {
        // Gets the name of the interactable object instance.
        string InstanceName { get; }
        // Returns the bounding rectangle of the object for collision detection.
        Rectangle GetBounds();
    }

    /// <summary>
    /// A static class that holds core game variables, including key bindings and movement states.
    /// </summary>
    internal class Core
    {
        // Publicly accessible key bindings for player movement.
        public readonly static Keys KeyUp = Keys.W;
        public readonly static Keys KeyDown = Keys.S;
        public readonly static Keys KeyLeft = Keys.A;
        public readonly static Keys KeyRight = Keys.D;

        // Publicly accessible key bindings for player actions.
        public readonly static Keys Interact = Keys.E;
        public readonly static Keys Inventory = Keys.I;

        //public readonly static Keys Help = Keys.H;

        // Static flags to track which movement keys are currently pressed.
        public static bool IsUp = false;
        public static bool IsDown = false;
        public static bool IsLeft = false;
        public static bool IsRight = false;

        // Static flags to track if action keys are currently pressed.
        public static bool IsInteracting = false;
        public static bool IsInventory = false;
        //public static bool IsHelp = false;
    }
}