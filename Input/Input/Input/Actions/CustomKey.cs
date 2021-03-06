﻿// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: CustomKey.cs Version: 1.1 Last Edited: 7/24/2012
// ------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Input.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Input.Input.Actions
{
    public class CustomKey
    {
        #region Properties
        
        #region Keyboard Properties

        /// <summary>
        /// Keyboard Key
        /// </summary>
        public Keys? KeyboardKey { get; set; }

        /// <summary>
        /// Modifiers applied to our Keyboard Key
        /// </summary>
        public List<Enumeration.KeyboardModiferKeys> KeyboardKeyModifiers { get; set; }

        /// <summary>
        /// State our Key should be in to perform action
        /// </summary>
        public Enumeration.KeyState? KeyboardKeyState { get; set; }

        #endregion

        #region Mouse Properties

        /// <summary>
        /// Mouse Key
        /// </summary>
        public Enumeration.MouseButtons? MouseKey { get; set; }

        /// <summary>
        /// Modifiers applied to our Mouse Key
        /// </summary>
        public List<Enumeration.MouseButtons> MouseKeyModifiers { get; set; }

        /// <summary>
        /// State our Key should be in to perform action
        /// </summary>
        public Enumeration.KeyState? MouseKeyState { get; set; }

        #endregion

        #region Constroller Properties
        
        /// <summary>
        /// Controller Key
        /// </summary>
        public Buttons? ControllerButton { get; set; }

        /// <summary>
        /// Modifiers applied to our Controller Key
        /// </summary>
        public List<Buttons> ControllerButtonModifiers { get; set; }
        
        /// <summary>
        /// State our Key should be in to perform action
        /// </summary>
        public Enumeration.KeyState? ControllerButtonState { get; set; }

        /// <summary>
        /// Index our Player is at for this Control to work
        /// </summary>
        public PlayerIndex? ControllerPlayerIndex { get; set; }

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for our Custom Keys, empty one is needed for XML serialization
        /// </summary>
        public CustomKey()
        {
            KeyboardKeyModifiers = new List<Enumeration.KeyboardModiferKeys>();
            MouseKeyModifiers = new List<Enumeration.MouseButtons>();
            ControllerButtonModifiers = new List<Buttons>();
            ControllerPlayerIndex = PlayerIndex.One;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if our Action is running
        /// </summary>
        /// <returns></returns>
        public bool IsActionRunning()
        {
            var keyboard = false;
            var mouse = false;
            var controller = false;


#if WINDOWS || WINDOWS_PHONE
            keyboard = IsKeyboardActionRunning();
#endif
            
#if WINDOWS
            mouse = IsMouseActionRunning();
#endif
            
#if WINDOWS || XBOX
            controller = IsControllerActionRunning();
#endif

            //Return true if any of our input devices are responding to the actions key press
            return keyboard || mouse || controller;
        }

        #region Private Methods

#if WINDOWS || WINDOWS_PHONE
        /// <summary>
        /// Quick check to see if our Keyboard is performing the action
        /// </summary>
        /// <returns></returns>
        bool IsKeyboardActionRunning()
        {
            #region Keyboard Stuff

            #region Keyboard Modifier Check

            if (KeyboardKeyModifiers != null && KeyboardKeyModifiers.Count != 0)
            {
                //Check our Modifiers to ensure they are held down, if not return because our action cannot possibly work
                foreach (var kkm in KeyboardKeyModifiers)
                {
                    switch (kkm)
                    {
                        case Enumeration.KeyboardModiferKeys.Caps:
                            if (!InputHandler.IsCapsLock())
                                return false;
                            break;
                        case Enumeration.KeyboardModiferKeys.Scroll:
                            if (!InputHandler.IsScrollLock())
                                return false;
                            break;
                        case Enumeration.KeyboardModiferKeys.Num:
                            if (!InputHandler.IsNumLock())
                                return false;
                            break;
                        default:
                            if (!InputHandler.KeyDown((Keys)kkm))
                                return false;
                            break;
                    }
                }
            }

            #endregion

            #region Keyboard Key Check

            //If our Keyboard key passed and the state passed are not fired return
            if (KeyboardKeyState != null)
            {
                if (KeyboardKey != null)
                {
                    if (!InputHandler.KeyCheck((Enumeration.KeyState)KeyboardKeyState, (Keys)KeyboardKey))
                        return false;
                }
                else
                    return false;
            }
            else
                return false;

            #endregion

            return true;

            #endregion
        }
#endif

#if WINDOWS
        /// <summary>
        /// Quick check to see if our Mouse is performing the action
        /// </summary>
        /// <returns></returns>
        bool IsMouseActionRunning()
        {
            #region Mouse Stuff
            
            #region Mouse Modifier Check

            if (MouseKeyModifiers != null && MouseKeyModifiers.Count != 0)
            {//Check our Modifiers to ensure they are held down, if not return because our action cannot possibly work
                if (MouseKeyModifiers.Any(mkm => !InputHandler.MouseButtonDown(mkm)))
                    return false;
            }

            #endregion

            #region Mouse Key Check

            //If our Mouse key passed and the state passed are not fired return
            if (MouseKeyState != null)
            {
                if (MouseKey != null)
                {
                    if (!InputHandler.MouseCheck((Enumeration.KeyState) MouseKeyState, (Enumeration.MouseButtons) MouseKey))
                        return false;
                }
                else
                    return false;
            }
            else
                return false;

            #endregion

            return true;

            #endregion
        }
#endif

#if WINDOWS || XBOX

        /// <summary>
        /// Quick check to see if our Controller is performing the action
        /// </summary>
        /// <returns></returns>
        bool IsControllerActionRunning()
        {
            #region Controller Stuff

            #region Controller Modifier Check

            if (ControllerButtonModifiers != null && ControllerButtonModifiers.Count != 0)
            {//Check our Modifiers to ensure they are held down, if not return because our action cannot possibly work
                if (ControllerButtonModifiers.Any(mkm => !InputHandler.ButtonDown(mkm, ControllerPlayerIndex)))
                    return false;
            }

            #endregion

            #region Controller Key Check

            if (ControllerButtonState != null)
            {
                if (ControllerButton != null)
                {
                    if (!InputHandler.ButtonCheck((Enumeration.KeyState)ControllerButtonState, (Buttons)ControllerButton, ControllerPlayerIndex))
                        return false;
                }
                else
                    return false;
            }
            else
                return false;

            #endregion

            return true;

            #endregion
        }
#endif

        #endregion

        #endregion
    }
}
