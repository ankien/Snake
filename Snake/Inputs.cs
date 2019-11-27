using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Snake
{
    class Inputs
    {
        // Load list of available Keyboard buttons
        private static Hashtable keyTable = new Hashtable();

        // Perform a check to see if a particular button is pressed.
        public static bool KeyPressed(Keys key)
        {
            if(keyTable[key] == null)
            {
                return false;
            }

            return (bool) keyTable[key];
        }

        // Detect if a keyboard button is pressed
        public static void ChangeState(Keys key, bool state)
        {
            keyTable[key] = state;
        }
    }
}
