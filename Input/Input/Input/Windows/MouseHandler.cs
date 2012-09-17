// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: MouseHandler.cs Version: 1.0 Last Edited: 6/26/2012
// ------------------------------------------------------------------------

using System;
using Input.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Input
{
    public partial class InputHandler
    {
        #region Fields

#if WINDOWS
        /// <summary>
        /// List of Mouse buttons currently pressed and how many times they have been pressed
        /// </summary>
        static readonly byte[] MouseButtonsCurrentlyDown = new byte[Enum.GetValues(typeof(Enumeration.MouseButtons)).Length];
#endif
        
        /// <summary>
        /// Timer to reset our Mouse Clicks
        /// </summary>
        static double _updateTimer;

        #endregion

        #region Properties

        /// <summary>
        /// Our Mouse State
        /// </summary>
        public static MouseState MouseState { get; set; }
        
        /// <summary>
        /// Our Last Mouse State
        /// </summary>
        public static MouseState LastMouseState { get; set; }
        
        #endregion

        #region Methods

        #region Mouse Positioning Methods

        /// <summary>
        /// Gets our Current Mouse Position as a Vector2
        /// </summary>
        public static Vector2 MousePositionAsVector2 { get { return new Vector2(MouseState.X, MouseState.Y); } }

        /// <summary>
        /// Gets our Last Mouse Position as a Vector2
        /// </summary>
        public static Vector2 LastMousePositionAsVector2 { get { return new Vector2(LastMouseState.X, LastMouseState.Y); } }

        /// <summary>
        /// Gets our Current Mouse Position as a Point
        /// </summary>
        public static Point MousePositionAsPoint { get { return new Point(MouseState.X, MouseState.Y);}}

        /// <summary>
        /// Gets our Last Mouse Position as a Point
        /// </summary>
        public static Point LastMousePositionAsPoint { get { return new Point(LastMouseState.X, MouseState.Y); } }
        
        /// <summary>
        /// Checks if the mouse is present inside a rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static bool MousePresent(Rectangle rectangle)
        {
            return rectangle.Contains(MousePositionAsPoint);
        }

        /// <summary>
        /// Checks if the last mouse was present inside a rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static bool LastMousePresent(Rectangle rectangle)
        {
            return rectangle.Contains(LastMousePositionAsPoint);
        }

        #endregion

        #region Clicking Methods

#if WINDOWS
        /// <summary>
        /// Checks if the mouse button was pressed more than once, equaling a double click
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool MouseButtonDoubleClicked(Enumeration.MouseButtons button)
        {
            switch (button)
            {
                case Enumeration.MouseButtons.Left:
                    return MouseButtonsCurrentlyDown[(int)Enumeration.MouseButtons.Left] >= 2;
                case Enumeration.MouseButtons.Right:
                    return MouseButtonsCurrentlyDown[(int)Enumeration.MouseButtons.Right] >= 2;
            }

            return false;
        }

        /// <summary>
        /// Checks if the mouse was only clicked once
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool MouseButtonSingleClicked(Enumeration.MouseButtons button)
        {
            switch (button)
            {
                case Enumeration.MouseButtons.Left:
                    return MouseButtonsCurrentlyDown[(int)Enumeration.MouseButtons.Left] == 1;
                case Enumeration.MouseButtons.Right:
                    return MouseButtonsCurrentlyDown[(int) Enumeration.MouseButtons.Right] == 1;
            }

            return false;
        }
        
#endif
        #endregion

        #region General Mouse Methods

        /// <summary>
        /// Checks if the mouse was just released
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool MouseButtonReleased(Enumeration.MouseButtons button)
        {
            switch(button)
            {
                case Enumeration.MouseButtons.Left:
                    return MouseState.LeftButton == ButtonState.Released &&
                           LastMouseState.LeftButton == ButtonState.Pressed;
                case Enumeration.MouseButtons.Right:
                    return MouseState.RightButton == ButtonState.Released &&
                           LastMouseState.RightButton == ButtonState.Pressed;
                case Enumeration.MouseButtons.Middle:
                    return MouseState.MiddleButton == ButtonState.Released &&
                           LastMouseState.MiddleButton == ButtonState.Pressed;
                case Enumeration.MouseButtons.XButton1:
                    return MouseState.MiddleButton == ButtonState.Released &&
                           LastMouseState.XButton1 == ButtonState.Pressed;
                case Enumeration.MouseButtons.XButton2:
                    return MouseState.MiddleButton == ButtonState.Released &&
                           LastMouseState.XButton2 == ButtonState.Pressed;
            }

            return false;
        }

        /// <summary>
        /// Checks if the passed mouse button is being held down
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool MouseButtonDown(Enumeration.MouseButtons button)
        {
            switch (button)
            {
                case Enumeration.MouseButtons.Left:
                    return MouseState.LeftButton == ButtonState.Pressed;
                case Enumeration.MouseButtons.Right:
                        return MouseState.RightButton == ButtonState.Pressed;
                case Enumeration.MouseButtons.Middle:
                    return MouseState.MiddleButton == ButtonState.Pressed;
                case Enumeration.MouseButtons.XButton1:
                    return MouseState.XButton1 == ButtonState.Pressed;
                case Enumeration.MouseButtons.XButton2:
                    return MouseState.XButton2 == ButtonState.Pressed;
            }

            return false;
        }

        /// <summary>
        /// Checks if the passed mouse button is up
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool MouseButtonUp(Enumeration.MouseButtons button)
        {
            switch (button)
            {
                case Enumeration.MouseButtons.Left:
                    return MouseState.LeftButton == ButtonState.Released;
                case Enumeration.MouseButtons.Right:
                    return MouseState.RightButton == ButtonState.Released;
                case Enumeration.MouseButtons.Middle:
                    return MouseState.MiddleButton == ButtonState.Released;
                case Enumeration.MouseButtons.XButton1:
                    return MouseState.XButton1 == ButtonState.Released;
                case Enumeration.MouseButtons.XButton2:
                    return MouseState.XButton2 == ButtonState.Released;
            }

            return false;
        }

        /// <summary>
        /// Checks if the passed mouse button was just pressed down
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool MouseButtonPressed(Enumeration.MouseButtons button)
        {
            switch (button)
            {
                case Enumeration.MouseButtons.Left:
                    return MouseState.LeftButton == ButtonState.Pressed &&
                           LastMouseState.LeftButton == ButtonState.Released;
                case Enumeration.MouseButtons.Right:
                    return MouseState.RightButton == ButtonState.Pressed &&
                           LastMouseState.RightButton == ButtonState.Released;
                case Enumeration.MouseButtons.Middle:
                    return MouseState.MiddleButton == ButtonState.Pressed &&
                           LastMouseState.MiddleButton == ButtonState.Released;
                case Enumeration.MouseButtons.XButton1:
                    return MouseState.XButton1 == ButtonState.Pressed &&
                           LastMouseState.XButton1 == ButtonState.Released;
                case Enumeration.MouseButtons.XButton2:
                    return MouseState.XButton1 == ButtonState.Pressed &&
                           LastMouseState.XButton1 == ButtonState.Released;
            }

            return false;
        }       
        
        /// <summary>
        /// Check if the scroll wheel was scrolled up
        /// </summary>
        /// <returns></returns>
        public static bool MouseScrollUp()
        {
            return MouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue;
        }

        /// <summary>
        /// Check if the scroll wheel was scrolled down
        /// </summary>
        /// <returns></returns>
        public static bool MouseScrollDown()
        {
            return MouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue;
        }

        /// <summary>
        /// Check if the passed mouse key is currently in the passed mouse state
        /// </summary>
        /// <param name="mouseKeyState"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool MouseCheck(Enumeration.KeyState mouseKeyState, Enumeration.MouseButtons key)
        {
            switch (mouseKeyState)
            {
                case Enumeration.KeyState.KeyUp:
                    return MouseButtonUp(key);
                case Enumeration.KeyState.KeyDown:
                    return MouseButtonDown(key);
                case Enumeration.KeyState.KeyReleased:
                    return MouseButtonReleased(key);
                case Enumeration.KeyState.KeyPressed:
                    return MouseButtonPressed(key);
            }

            return false;
        }

        #endregion

        #region Mass Mouse Methods

        /// <summary>
        /// Gets a list of all mouse buttons currently held down
        /// </summary>
        /// <returns></returns>
        public static Enumeration.MouseButtons[] GetMouseButtonsDown()
        {
            var mb = new Enumeration.MouseButtons[0];
            var i = 0;

            if (MouseButtonDown(Enumeration.MouseButtons.Left))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.Left;
                i++;
            }
            if (MouseButtonDown(Enumeration.MouseButtons.Middle))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.Middle;
                i++;
            }
            if (MouseButtonDown(Enumeration.MouseButtons.Right))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.Right;
                i++;
            }
            if (MouseButtonDown(Enumeration.MouseButtons.XButton1))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.XButton1;
                i++;
            }
            if (MouseButtonDown(Enumeration.MouseButtons.XButton2))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.XButton2;
            }

            return mb;
        }

        /// <summary>
        /// Gets a list of all mouse buttons currently pressed
        /// </summary>
        /// <returns></returns>
        public static Enumeration.MouseButtons[] GetMouseButtonsPressed()
        {
            var mb = new Enumeration.MouseButtons[0];
            var i = 0;

            if (MouseButtonPressed(Enumeration.MouseButtons.Left))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.Left;
                i++;
            }
            if (MouseButtonPressed(Enumeration.MouseButtons.Middle))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.Middle;
                i++;
            }
            if (MouseButtonPressed(Enumeration.MouseButtons.Right))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.Right;
                i++;
            }
            if (MouseButtonPressed(Enumeration.MouseButtons.XButton1))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.XButton1;
                i++;
            }
            if (MouseButtonPressed(Enumeration.MouseButtons.XButton2))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.XButton2;
            }

            return mb;
        }

        /// <summary>
        /// Gets a list of all mouse buttons currently released
        /// </summary>
        /// <returns></returns>
        public static Enumeration.MouseButtons[] GetMouseButtonsReleased()
        {
            var mb = new Enumeration.MouseButtons[0];
            var i = 0;

            if (MouseButtonReleased(Enumeration.MouseButtons.Left))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.Left;
                i++;
            }
            if (MouseButtonReleased(Enumeration.MouseButtons.Middle))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.Middle;
                i++;
            }
            if (MouseButtonReleased(Enumeration.MouseButtons.Right))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.Right;
                i++;
            }
            if (MouseButtonReleased(Enumeration.MouseButtons.XButton1))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.XButton1;
                i++;
            }
            if (MouseButtonReleased(Enumeration.MouseButtons.XButton2))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.XButton2;
            }

            return mb;
        }

        /// <summary>
        /// Gets a list of all mouse buttons currently up
        /// </summary>
        /// <returns></returns>
        public static Enumeration.MouseButtons[] GetMouseButtonsUp()
        {
            var mb = new Enumeration.MouseButtons[0];
            var i = 0;

            if (MouseButtonUp(Enumeration.MouseButtons.Left))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.Left;
                i++;
            }
            if (MouseButtonUp(Enumeration.MouseButtons.Middle))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.Middle;
                i++;
            }
            if (MouseButtonUp(Enumeration.MouseButtons.Right))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.Right;
                i++;
            }
            if (MouseButtonUp(Enumeration.MouseButtons.XButton1))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.XButton1;
                i++;
            }
            if (MouseButtonUp(Enumeration.MouseButtons.XButton2))
            {
                Array.Resize(ref mb, i + 1);
                mb[i] = Enumeration.MouseButtons.XButton2;
            }

            return mb;
        }

        #endregion

        #region Area Checks
        
        /// <summary>
        /// Determines if the area was clicked by the passed mouse button
        /// </summary>
        /// <param name="area"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool Clicked(Rectangle area, Enumeration.MouseButtons button)
        {
            return MousePresent(area) && MouseButtonReleased(button);
        }

        /// <summary>
        /// Determines if the passed mouse button was released in the passed area
        /// </summary>
        /// <param name="area"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool Released(Rectangle area, Enumeration.MouseButtons button)
        {
            return MousePresent(area) && MouseButtonReleased(button);
        }

        /// <summary>
        /// Determines if the passed mouse button was pressed in the passed area
        /// </summary>
        /// <param name="area"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool Pressed(Rectangle area, Enumeration.MouseButtons button)
        {
            return MousePresent(area) && MouseButtonPressed(button);
        }
        
        #endregion

        #endregion

        #region Xna Methods

        /// <summary>
        /// Update the Mouse and any components used by it
        /// </summary>
        /// <param name="gameTime"></param>
        static void UpdateMouseButtons(GameTime gameTime)
        {
#if WINDOWS
            _updateTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_updateTimer >= Constant.MouseDoubleClickStandard)
            {//If we need to reset our Double Click do so
                for (var i = 0; i < MouseButtonsCurrentlyDown.Length; i++)
                    MouseButtonsCurrentlyDown[i] = 0;

                _updateTimer = 0;
            }

            var mbDown = GetMouseButtonsReleased();
            //If Pressed has happening this update add to our mouse buttons to help detect double clicks
            foreach (var mb in mbDown)
                switch (mb)
                {
                    case Enumeration.MouseButtons.Left:
                        MouseButtonsCurrentlyDown[(int)Enumeration.MouseButtons.Left]++;
                        break;
                    case Enumeration.MouseButtons.Right:
                        MouseButtonsCurrentlyDown[(int)Enumeration.MouseButtons.Right]++;
                        break;
                }

#endif
        }

        #endregion
    }
}
