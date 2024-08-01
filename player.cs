using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MistsOfThelema
{
    public static class player
    {
        public readonly static Keys Up = Keys.W;
        public readonly static Keys Down = Keys.S;  
        public readonly static Keys Left = Keys.A;
        public readonly static Keys Right = Keys.D;

        public readonly static Keys Interact = Keys.F;

        public static bool IsUp = false;
        public static bool IsDown = false;
        public static bool IsLeft = false;
        public static bool IsRight = false;

        public static bool IsInteracting = false;
    }
}
