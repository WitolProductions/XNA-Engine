using System;
using Microsoft.Xna.Framework;

namespace Graphics.ScreenManager
{
    public abstract class ScreenBase : DrawableGameComponent
    {
        #region Field

        #endregion

        #region Properties

        #endregion

        #region Constructor

        protected ScreenBase(Game game) : base(game)
        {
        }

        #endregion

        #region Methods

        internal protected void ScreenChange(object sender, EventArgs e)
        {
            //If this screen is the current screen show it, else hide it
            if (ScreenHandler.CurrentScreen == this)
                Show();
            else
                Hide();
        }

        void Show()
        {
            Visible = true;
            Enabled = true;

            OnShow();
        }

        void Hide()
        {
            Visible = false;
            Enabled = false;
        }

        protected abstract void OnShow();

        public new abstract void LoadContent();

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
