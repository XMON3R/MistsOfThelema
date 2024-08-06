using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MistsOfThelema
{
    public interface IInteractable
    {
        string InstanceName { get; }
        Rectangle GetBounds();
    }

    internal class Core
    {
        public readonly static Keys KeyUp = Keys.W;
        public readonly static Keys KeyDown = Keys.S;
        public readonly static Keys KeyLeft = Keys.A;
        public readonly static Keys KeyRight = Keys.D;

        public readonly static Keys Interact = Keys.E;
        public readonly static Keys Inventory = Keys.I;

        public readonly static Keys Help = Keys.H;

        public static bool IsUp = false;
        public static bool IsDown = false;
        public static bool IsLeft = false;
        public static bool IsRight = false;

        public static bool IsInteracting = false;
        public static bool IsInventory = false;
        public static bool IsHelp = false;  
    }
}
