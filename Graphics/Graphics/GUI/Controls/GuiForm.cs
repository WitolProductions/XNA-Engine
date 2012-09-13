using Graphics.GUI.Interfaces;
using Microsoft.Xna.Framework;

namespace Graphics.GUI.Controls
{
    public class GuiForm : ControlBase
    {
        #region Fields

        #endregion

        #region Properties

        #region IFont

        public string Font { get; set; }

        public Color FontColor { get; set; }

        #endregion

        #endregion

        #region Constructor

        public GuiForm(Game game) : base(game)
        {
        }

        #endregion

        #region Methods

        #endregion

        #region XNA Methods

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
