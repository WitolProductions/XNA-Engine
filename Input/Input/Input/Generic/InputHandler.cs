// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: InputHandler.cs Version: 1.1 Last Edited: 6/26/2012
// ------------------------------------------------------------------------

using System;
using Input.Input.Actions;
using Microsoft.Xna.Framework;

#if WINDOWS || XBOX
using Microsoft.Xna.Framework.Input;
#endif

#if WINDOWS_PHONE
using System.Linq;
using Microsoft.Xna.Framework.Input.Touch;
#endif

namespace Input
{
    public partial class InputHandler : GameComponent
    {
        #region Fields

        #endregion

        #region Properties

#if WINDOWS
        public static StringCreator StringCreator { get; set; }
#endif
        public static ActionHandler ActionHandler { get; set; }

        #endregion

        #region Constructors

        //Constructor for our Input Handler
        public InputHandler(Game game): base(game)
        {
            //Setup our ActionHandler
            ActionHandler = new ActionHandler();

#if WINDOWS_PHONE
            ActionHandler.WindowsPhoneEnabled = true;
#elif WINDOWS
            ActionHandler.KeyboardEnabled = true;
            ActionHandler.MouseEnabled = true;
            ActionHandler.ControllerEnabled = true;
#elif XBOX
            ActionHandler.ControllerEnabled = true;
#endif
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update all our controller states
        /// </summary>
        void SetStates()
        {

#if XBOX || WINDOWS
            #region XBOX and Windows Stuff

            if (ControllerEnabled)
            {
                LastGamePadStates = (GamePadState[]) GamePadStates.Clone();
                //Update all our Controllers
                for (var index = 0; index < GamePadStates.Length; index++)
                {
                    GamePadStates[index] = GamePad.GetState((PlayerIndex)index);

                    if (!GamePadStates[index].IsConnected)
                        ControllerDisconnect(index);
                }
            }

            #endregion
#endif

#if WINDOWS 
            #region Windows Stuff
            
            LastMouseState = MouseState;
            MouseState = Mouse.GetState();

            #endregion
#endif

#if WINDOWS || WIDNOWS_PHONE
            #region Windows and Windows Phone Stuff
            
            //Update our Keyboard/hardware keyboards
            LastKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();

            #endregion
#endif

#if WINDOWS_PHONE
            #region Windows Phone Stuff
            
            //Update our Tourch screen information if its enabled
            if (TouchEnabled)
                Touch = TouchPanel.GetState();

            #endregion
#endif
        }

        /// <summary>
        /// Enable the use of Controllers
        /// </summary>
        public static void EnableControllers()
        {
#if WINDOWS || XBOX
            GamePadStates = new GamePadState[NumPads];
            LastGamePadStates = new GamePadState[NumPads];
            ControllerEnabled = true;
#endif
        }

        /// <summary>
        /// Disable the use of Controllers
        /// </summary>
        public static void DisableControllers()
        {
#if WINDOWS || XBOX
            GamePadStates = null;
            LastGamePadStates = null;
            ControllerEnabled = false;
#endif
        }

        #endregion

        #region XNA Methods

        public override void Initialize()
        {
#if WINDOWS || XBOX
            #region Xbox and Windows Stuff
            
            if (ControllerEnabled)
                GamePadStates = new GamePadState[NumPads];
            
            #endregion
#endif


#if WINDOWS
            #region Windows Stuff

            StringCreator = new StringCreator();

            #endregion
#endif

#if WINDOWS_PHONE
            #region Windows Phone Stuff

            if (MotionEnabled)
                EnableMotion();
            if (TouchEnabled)
                EnableTouch();

            #endregion
#endif
            
            SetStates();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //Set all our Controller states 
            SetStates();
#if WINDOWS
            #region Windows Stuff

            //Update our Mouse Buttons
            UpdateMouseButtons(gameTime);

            if (StringCreator.Enabled)
                StringCreator.Update(gameTime, GetKeysPressed(), GetKeysDown());

            #endregion
#endif
            base.Update(gameTime);
        }
        
        #endregion

        #region Generic Methods

        /// <summary>
        /// Returns if the area passed is currently being hovered over by the mouse
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static bool Hover(Rectangle area)
        {
#if WINDOWS || XBOX
            return MousePresent(area);
#elif WINDOWS_PHONE
            return Touch.IsConnected && Touch.Where(tl => tl.State == TouchLocationState.Moved).Any(tl => area.Contains((int)tl.Position.X, (int)tl.Position.Y));
#endif
        }

        /// <summary>
        /// Returns a bool determing if an area was just clicked
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static bool Clicked(Rectangle area)
        {
#if WINDOWS_PHONE
            if (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();

                return area.Contains((int)gesture.Position.X, (int)gesture.Position.Y) && gesture.GestureType == GestureType.Tap;
            }

            return false;
#elif WINDOWS || XBOX
            return MousePresent(area) && GetMouseButtonsReleased().Length > 0;
#endif
        }

        /// <summary>
        /// Returns a bool determining if an area is held down currently
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static bool Down(Rectangle area)
        {
#if WINDOWS_PHONE
            return Hover(area);
#elif WINDOWS || XBOX
            return MousePresent(area) && (GetMouseButtonsDown().Length > 0 || GetMouseButtonsPressed().Length > 0);
#endif
        }

        #endregion
    }
}
