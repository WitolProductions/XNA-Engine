// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: KeyboardHandler.cs Version: 1.0 Last Edited: 6/26/2012
// ------------------------------------------------------------------------

#if WINDOWS || WINDOWS_PHONE

using System.Collections.Generic;
using System.Linq;
using Input.Misc;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

#if WINDOWS
using System.Windows.Forms;
#endif


namespace Input
{
    public partial class InputHandler
    {
        #region Properties

        /// <summary>
        /// Current Keyboard State
        /// </summary>
        public static KeyboardState KeyboardState { get; set; }

        /// <summary>
        /// Last Keyboard State
        /// </summary>
        public static KeyboardState LastKeyboardState { get; set; }

        #endregion

        #region Constructors

        #endregion

        #region Methods
        
        /// <summary>
        /// Check if a key was released this state that was down last
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyReleased(Keys key)
        {
            return KeyboardState.IsKeyUp(key) && LastKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Check if a key was down last state and up this one
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key) && LastKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Check if a key is being held down
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyDown(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Check if a key is up
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyUp(Keys key)
        {
            return KeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Return a full list of all keys currently held down
        /// </summary>
        /// <returns></returns>
        public static List<Keys> GetKeysDown()
        {
            return (from object k in (typeof(Keys)).GetFields() where KeyDown((Keys)k) select (Keys)k).ToList();
        }

        /// <summary>
        /// Return a full list of all keys pressed since last state
        /// </summary>
        /// <returns></returns>
        public static Keys[] GetKeysPressed()
        {
            return (from object k in (typeof (Keys)).GetFields() where KeyPressed((Keys) k) select (Keys) k).ToArray();
        }

        /// <summary>
        /// Returns if Caps Lock is on or off
        /// </summary>
        /// <returns></returns>
        public static bool IsCapsLock()
        {
#if WINDOWS
            return Control.IsKeyLocked((System.Windows.Forms.Keys) Keys.CapsLock);
#elif WINDOWS_PHONE
            return false;
#endif
        }

        /// <summary>
        /// Returns if Num Lock is on or off
        /// </summary>
        /// <returns></returns>
        public static bool IsNumLock()
        {
#if WINDOWS
            return Control.IsKeyLocked((System.Windows.Forms.Keys)Keys.NumLock);
#elif WINDOWS_PHONE
            return false;
#endif
        }

        /// <summary>
        /// Returns if Scroll Lock is on or off
        /// </summary>
        /// <returns></returns>
        public static bool IsScrollLock()
        {
#if WINDOWS
            return Control.IsKeyLocked((System.Windows.Forms.Keys)Keys.Scroll);
#elif WINDOWS_PHONE
            return false;
#endif
        }

        /// <summary>
        /// Checks if the passed Key equals the passed State 
        /// </summary>
        /// <param name="keyboardKeyState"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyCheck(Enumeration.KeyState keyboardKeyState, Keys key)
        {
            switch (keyboardKeyState)
            {
                case Enumeration.KeyState.KeyUp:
                    return KeyUp(key);
                case Enumeration.KeyState.KeyDown:
                    return KeyDown(key);
                case Enumeration.KeyState.KeyReleased:
                    return KeyReleased(key);
                case Enumeration.KeyState.KeyPressed:
                    return KeyPressed(key);
            }

            return false;
        }

        #endregion

    }
}
#endif