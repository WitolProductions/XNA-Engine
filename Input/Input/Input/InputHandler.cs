// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: InputHandler.cs Version: 1.0 Last Edited: 6/26/2012
// ------------------------------------------------------------------------

using System;
using Input.Input.Actions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Input.Input
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

        public InputHandler(Game game): base(game)
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

            #region Generic Stuff

            ActionHandler = new ActionHandler();

            SetStates();

            #endregion
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
            //Update our Keyboard and mouse states
            LastKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();

            LastMouseState = MouseState;
            MouseState = Mouse.GetState();

            #endregion
#endif

            #region Generic Stuff



            #endregion
        }

        #endregion

        #region XNA Methods

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
    }
}
