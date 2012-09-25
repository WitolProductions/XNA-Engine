// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Game.cs Version: 1.0 Last Edited: 9/4/2012
// ------------------------------------------------------------------------

using Content;
using Graphics;
using Graphics.ScreenManager;
using Input;
using Input.Input.Actions;
using Microsoft.Xna.Framework;

#if WINDOWS
using System.Windows.Forms;
#endif

namespace Engine
{
    public abstract class TheGame : Game
    {
        #region Fields

        /// <summary>
        /// Grants access to the Graphics Device Manager
        /// </summary>
        public static GraphicsDeviceManager GraphicsDeviceManager = null;

        /// <summary>
        /// Shortcut to our Content Manager
        /// </summary>
        public new static CustomContentManager Content = null;

        /// <summary>
        /// Shortcut to our Graphics Handler
        /// </summary>
        public static GraphicsHandler Graphics = null;

        /// <summary>
        /// Shortcut to our Screen Handler
        /// </summary>
        public static ScreenHandler Screen = null;

        /// <summary>
        /// Shortcut to our Content Handler
        /// </summary>
        public static ContentHandler ContentHandler = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize the Game Window
        /// </summary>
        protected TheGame()
        {
            //Setup our games form  
            GraphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1024,
                PreferredBackBufferHeight = 768,
                IsFullScreen = false,
                PreferMultiSampling = true
            };
            //Ensure mouse shows up
            IsMouseVisible = true;
        }

        #endregion

        #region XNA Methods

        /// <summary>
        /// Initialize our Game Data
        /// </summary>
        protected override void Initialize()
        {
            ContentHandler = new ContentHandler(this, Services, "Content");

            Components.Add(new InputHandler(this));
            Components.Add(ContentHandler);
            Components.Add(new ScreenHandler(this));

            Content = ContentHandler.Content;
            GraphicsHandler.Initialize(this);

            ActionHandler.Load();

            base.Initialize();
        }

        /// <summary>
        /// Load our Game Content
        /// </summary>
        protected override void LoadContent()
        {
            
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Update our game
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        #endregion
    }
}
