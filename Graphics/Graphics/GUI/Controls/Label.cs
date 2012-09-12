using Graphics.GUI.Interfaces;
using Microsoft.Xna.Framework;

namespace Graphics.GUI.Controls
{
    public class Label : ControlBase, IString
    {
        #region Fields

        #endregion

        #region Properties

        #region IFont

        public string Text { get; set; }
        
        public string Font { get; set; }

        public Color FontColor { get; set; }
        
        #endregion

        #endregion

        #region Constructor

        public Label(Game game) : base(game)
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
            GuiRenderer.Render(this);


            base.Draw(gameTime);
        }

        #endregion


    }
}
