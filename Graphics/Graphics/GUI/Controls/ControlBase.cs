using Graphics.ScreenManager;
using Microsoft.Xna.Framework;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Graphics.GUI.Controls
{
    public abstract class ControlBase : DrawableGameComponent
    {
        #region Fields

        /// <summary>
        /// Reference to our Game
        /// </summary>
        protected Game GameRef = null;

        /// <summary>
        /// Background Color of our Control
        /// </summary>
        public Color BackColor = Color.Transparent;
        
        /// <summary>
        /// Location of our Control X than Y
        /// </summary>
        public Vector2 Location = new Vector2(0, 0);
        
        /// <summary>
        /// Size of our Control Width than Height
        /// </summary>
        public Vector2 Size = new Vector2(0, 0);
        
        /// <summary>
        /// Unique name of a Control
        /// </summary>
        public string Name = null;
        
        #endregion

        #region Properties
        
        /// <summary>
        /// Bounds of our Object
        /// </summary>
        protected Rectangle Bounds { get { return new Rectangle((int)Location.X, (int)Location.Y, (int)Size.X, (int)Size.Y); } }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Base to our Control
        /// </summary>
        /// <param name="game"></param>
        protected ControlBase(Game game) : base(game)
        {
            GameRef = game;
            DrawOrder = ScreenHandler.DrawLevel + 200;
        }

        #endregion

        #region Abstract Methods



        #endregion
    }
}
