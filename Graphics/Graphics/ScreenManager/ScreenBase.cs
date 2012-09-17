// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: ScreenBase.cs Version: 1.0 Last Edited: 9/8/2012
// ------------------------------------------------------------------------

using System;
using Microsoft.Xna.Framework;

namespace Graphics.ScreenManager
{
    public abstract class ScreenBase : DrawableGameComponent
    {
        #region Field

        #endregion

        #region Properties

        public readonly string Name = null;
        
        #endregion

        #region Constructor

        protected ScreenBase(Game game, string name) : base(game)
        {
            Name = name;
            Hide();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Event fires when screen is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ScreenChange(object sender, EventArgs e)
        {
            //If this screen is the current screen show it, else hide it
            if (ScreenHandler.CurrentScreen == this)
                Show();
            else
                Hide();
        }

        /// <summary>
        /// Show our Screen while also Enableing it
        /// </summary>
        void Show()
        {
            Visible = true;
            Enabled = true;

            LoadOnShow();
        }

        /// <summary>
        /// Hide our Screen while also Disableing it
        /// </summary>
        public void Hide()
        {
            Visible = false;
            Enabled = false;

            DisposeOnHide();
        }

        public new abstract void LoadContent();

        /// <summary>
        /// Method is intended to load anything needed when a screen shows itself
        /// </summary>
        public abstract void LoadOnShow();

        /// <summary>
        /// Method is intended to Dispose uneeded items when a screen hides to help improve performance
        /// </summary>
        public abstract void DisposeOnHide();

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        #endregion
    }
}
