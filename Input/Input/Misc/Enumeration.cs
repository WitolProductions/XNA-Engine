// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Enumeration.cs Version: 1.1 Last Edited: 6/26/2012
// ------------------------------------------------------------------------

using Microsoft.Xna.Framework.Input;

namespace Input.Misc
{
    public class Enumeration
    {     
        /// <summary>
        /// State of a key
        /// </summary>
        public enum KeyState : byte
        {
            KeyUp,
            KeyDown,
            KeyReleased,
            KeyPressed
        }

        /// <summary>
        /// Modifier keys a keyboard has
        /// </summary>
        public enum KeyboardModiferKeys : byte
        {
            LeftControl = Keys.LeftControl,
            LeftShift = Keys.LeftShift,
            LeftAlt = Keys.LeftAlt,
            RightControl = Keys.RightControl,
            RightAlt = Keys.RightAlt,
            RightShift = Keys.RightShift,
            Caps = Keys.CapsLock,
            Num = Keys.NumLock,
            Scroll = Keys.Scroll
        }

        /// <summary>
        /// Mouse Buttons
        /// </summary>
        public enum MouseButtons : byte
        {
            Left, 
            Right, 
            Middle, 
            XButton1, 
            XButton2
        }
    }
}
