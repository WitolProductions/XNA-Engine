// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: ControlerHandler.cs Version: 1.0 Last Edited: 6/26/2012
// ------------------------------------------------------------------------
#if WINDOWS || XBOX

using System;
using Input.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Input
{
    public partial class InputHandler
    {
        #region Fields

        /// <summary>
        /// On Controller Disconnect
        /// </summary>
        /// <param name="o">Returns the InputHandler class</param>
        /// <param name="e">Which Controller Disconnected</param>
        public delegate void ControllerDisconnectedHandler(object o, int e);
        
        
        public static event ControllerDisconnectedHandler OnControllerDisconnect;

        const int NumPads = 4;

        #endregion
        
        #region Properties

        public static GamePadState[] GamePadStates { get; set; }
        public static GamePadState[] LastGamePadStates { get; set; }

        public static bool ControllerEnabled { get; set; }

        #endregion

        #region Methods



        /// <summary>
        /// Gets a GamePad
        /// </summary>
        /// <param name="index">Index to get the gamepad at</param>
        /// <returns></returns>
        public static GamePadState GamePadState(PlayerIndex index)
        {
            return GamePadStates[(int) index];
        }

        /// <summary>
        /// Gets a GamePad from our last update
        /// </summary>
        /// <param name="index">Index to get the gamepad at</param>
        /// <returns></returns>
        public static GamePadState LastGamePadState(PlayerIndex index)
        {
            return LastGamePadStates[(int)index];
        }

        #endregion

        #region Button Methods

        /// <summary>
        /// Gets if Button was Released
        /// </summary>
        /// <param name="button">Button to Check</param>
        /// <param name="index">GamePad index to check</param>
        /// <returns></returns>
        public static bool ButtonReleased(Buttons button, PlayerIndex index)
        {
            if (!ControllerEnabled) return false; //Return false if Controller is disabled
            return GamePadStates[(int)index].IsButtonUp(button) &&
                LastGamePadStates[(int)index].IsButtonDown(button);
        }

        /// <summary>
        /// Gets if Button was Pressed
        /// </summary>
        /// <param name="button">Button to Check</param>
        /// <param name="index">GamePad index to check</param>
        /// <returns></returns>
        public static bool ButtonPressed(Buttons button, PlayerIndex index)
        {
            if (!ControllerEnabled) return false; //Return false if Controller is disabled
            return GamePadStates[(int) index].IsButtonDown(button) &&
                   LastGamePadStates[(int) index].IsButtonUp(button);
        }

        /// <summary>
        /// Gets if Button is Down
        /// </summary>
        /// <param name="button">Button to Check</param>
        /// <param name="index">GamePad index to check</param>
        /// <returns></returns>
        public static bool ButtonDown(Buttons button, PlayerIndex index)
        {
            return ControllerEnabled && GamePadStates[(int)index].IsButtonDown(button);
        }

        /// <summary>
        /// Gets if Button is Up
        /// </summary>
        /// <param name="button">Button to Check</param>
        /// <param name="index">GamePad index to check</param>
        /// <returns></returns>
        public static bool ButtonUp(Buttons button, PlayerIndex index)
        {
            return ControllerEnabled && GamePadStates[(int) index].IsButtonUp(button);
        }

        /// <summary>
        /// Checks if the passed Button equals the passed State at that player index
        /// </summary>
        /// <param name="buttonState"></param>
        /// <param name="button"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool ButtonCheck(Enumeration.KeyState buttonState, Buttons button, PlayerIndex index)
        {
            switch(buttonState)
            {
                case Enumeration.KeyState.KeyUp:
                    return ButtonUp(button, index);
                case Enumeration.KeyState.KeyDown:
                    return ButtonDown(button, index);
                case Enumeration.KeyState.KeyReleased:
                    return ButtonReleased(button, index);
                case Enumeration.KeyState.KeyPressed:
                    return ButtonPressed(button, index);
            }

            return false;
        }

        #endregion

        #region Connected methods

        /// <summary>
        /// Check if our Controller is actually Connected
        /// </summary>
        /// <param name="index">Index of controller to check</param>
        /// <returns></returns>
        public static bool IsConnected(PlayerIndex index)
        {
            return ControllerEnabled && GamePadStates[(int) index].IsConnected;
        }

        #endregion

        #region Vibration Methods

        /// <summary>
        /// Have our Controller start Vibrating
        /// </summary>
        /// <param name="index">Controler to Vibrate</param>
        /// <param name="left">How much the Left should vibrate</param>
        /// <param name="right">How much the Right should vibrate</param>
        public static void Vibrate(PlayerIndex index, float left, float right)
        {
            left = MathHelper.Clamp(left, 0.0f, 1.0f);
            right = MathHelper.Clamp(right, 0.0f, 1.0f);

            GamePad.SetVibration(index, left, right);
        }

        /// <summary>
        /// Stop a controller from vibrating
        /// </summary>
        /// <param name="index">Index of Controller to stop</param>
        public static void StopVibration(PlayerIndex index)
        {
            GamePad.SetVibration(index, 0f, 0f);
        }

        #endregion

        #region Thumbstick and DPad Methods

        /// <summary>
        /// Gets position of our Left Thumb Pad
        /// </summary>
        /// <param name="index">Index of Controller to check</param>
        /// <returns></returns>
        public static Vector2 LeftThumb(PlayerIndex index)
        {
            if (!ControllerEnabled) return Vector2.Zero; ; //Return a Zero vector if not enabled
            return GamePadStates[(int) index].ThumbSticks.Left;
        }

        /// <summary>
        /// Gets position of our Last Left Thumb Pad
        /// </summary>
        /// <param name="index">Index of Controller to check</param>
        /// <returns></returns>
        public static Vector2 LastLeftThumb(PlayerIndex index)
        {
            if (!ControllerEnabled) return Vector2.Zero; ; //Return a Zero vector if not enabled
            return LastGamePadStates[(int)index].ThumbSticks.Left;
        }

        /// <summary>
        /// Gets position of our Right Thumb Pad
        /// </summary>
        /// <param name="index">Index of Controller to check</param>
        /// <returns></returns>
        public static Vector2 RightThumb(PlayerIndex index)
        {
            if (!ControllerEnabled) return Vector2.Zero; ; //Return a Zero vector if not enabled
            return GamePadStates[(int) index].ThumbSticks.Right;
        }

        /// <summary>
        /// Gets position of our Last Right Thumb Pad
        /// </summary>
        /// <param name="index">Index of Controller to check</param>
        /// <returns></returns>
        public static Vector2 LastRightThumb(PlayerIndex index)
        {
            if (!ControllerEnabled) return Vector2.Zero; ; //Return a Zero vector if not enabled
            return LastGamePadStates[(int)index].ThumbSticks.Right;
        }

        /// <summary>
        /// Gets the direction the depad is pressed in
        /// </summary>
        /// <param name="index">Index of Controller to check</param>
        /// <returns></returns>
        public static GamePadDPad DPad(PlayerIndex index)
        {
            return !ControllerEnabled ? new GamePadDPad(ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released) : GamePadStates[(int) index].DPad;
        }

        /// <summary>
        /// Gets the direction the last depad is pressed in
        /// </summary>
        /// <param name="index">Index of Controller to check</param>
        /// <returns></returns>
        public static GamePadDPad LastDPad(PlayerIndex index)
        {
            return !ControllerEnabled ? new GamePadDPad(ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released) : LastGamePadStates[(int)index].DPad;
        }

        #endregion

        #region Trigger Methods

        /// <summary>
        /// Gets both left and right triggers
        /// </summary>
        /// <param name="index">Index of Controller to check</param>
        /// <returns></returns>
        public static GamePadTriggers Triggers(PlayerIndex index)
        {
            return !ControllerEnabled ? new GamePadTriggers(0f, 0f) : GamePadStates[(int) index].Triggers;
        }

        /// <summary>
        /// Gets both left and right last triggers
        /// </summary>
        /// <param name="index">Index of Controller to check</param>
        /// <returns></returns>
        public static GamePadTriggers LastTriggers(PlayerIndex index)
        {
            return !ControllerEnabled ? new GamePadTriggers(0f, 0f) : LastGamePadStates[(int) index].Triggers;
        }

        /// <summary>
        /// Gets the left triggers value
        /// </summary>
        /// <param name="index">Index of Controller to check</param>
        /// <returns></returns>
        public static float LeftTrigger(PlayerIndex index)
        {
            return !ControllerEnabled ? 0f : GamePadStates[(int) index].Triggers.Left;
        }

        /// <summary>
        /// Gets the last left triggers value
        /// </summary>
        /// <param name="index">Index of Controller to check</param>
        /// <returns></returns>
        public static float LastLeftTrigger(PlayerIndex index)
        {
            return !ControllerEnabled ? 0f : LastGamePadStates[(int)index].Triggers.Left;
        }

        /// <summary>
        /// Gets the right triggers value
        /// </summary>
        /// <param name="index">Index of Controller to check</param>
        /// <returns></returns>
        public static float RightTrigger(PlayerIndex index)
        {
            return !ControllerEnabled ? 0f : GamePadStates[(int)index].Triggers.Right;
        }

        /// <summary>
        /// Gets the last right triggers value
        /// </summary>
        /// <param name="index">Index of Controller to check</param>
        /// <returns></returns>
        public static float LastRightTrigger(PlayerIndex index)
        {
            return !ControllerEnabled ? 0f : LastGamePadStates[(int)index].Triggers.Right;
        }

        #endregion

        #region Events

        /// <summary>
        /// Event to watch for when a controller disconnects
        /// </summary>
        /// <param name="e"></param>
        private void ControllerDisconnect(int e)
        {
            if (OnControllerDisconnect != null)
                OnControllerDisconnect(this, e);
        }

        #endregion

    }
}

#endif