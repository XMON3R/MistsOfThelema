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


        public static int X { get; set; } = 50; 
        public static int Y { get; set; } = 50; 
        public static int Speed { get; set; } = 5;

        public static void KeyDown(Keys key)
        {
            if (key == Up) IsUp = true;
            if (key == Down) IsDown = true;
            if (key == Left) IsLeft = true;
            if (key == Right) IsRight = true;
            if (key == Interact) IsInteracting = true;
        }

        public static void KeyUp(Keys key)
        {
            if (key == Up) IsUp = false;
            if (key == Down) IsDown = false;
            if (key == Left) IsLeft = false;
            if (key == Right) IsRight = false;
            if (key == Interact) IsInteracting = false;
        }

        public static void UpdatePosition()
        {
            if (IsUp) Y -= Speed;
            if (IsDown) Y += Speed;
            if (IsLeft) X -= Speed;
            if (IsRight) X += Speed;
        }

        public static void Reset()
        {
            IsUp = IsDown = IsLeft = IsRight = IsInteracting = false;
        }
    }
}

