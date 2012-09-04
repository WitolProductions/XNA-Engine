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
using Input;
using Input.Input.Actions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public abstract class TheGame : Game
    {
        #region Fields

        /// <summary>
        /// Grants access to the Graphics Device Manager
        /// </summary>
        public static GraphicsDeviceManager GraphicsDeviceManager;

        /// <summary>
        /// Grants access to the Sprite Batch
        /// </summary>
        public SpriteBatch SpriteBatch;

        public new static CustomContentManager Content = ContentHandler.Content;
        

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize the Game Window
        /// </summary>
        protected TheGame()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1024,
                PreferredBackBufferHeight = 768,
                IsFullScreen = false,
                PreferMultiSampling = true
            };

            IsMouseVisible = true;
        }

        #endregion

        #region XNA Methods

        /// <summary>
        /// Initialize our Game Data
        /// </summary>
        protected override void Initialize()
        {
            Components.Add(new InputHandler(this));
            Components.Add(new ContentHandler(this, Services, "Content"));

            Content = ContentHandler.Content;
            SpriteBatch = new SpriteBatch(GraphicsDevice);

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
