// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: ControlBase.cs Version: 1.0 Last Edited: 9/13/2012
// ------------------------------------------------------------------------

using Graphics.Misc;
using Graphics.ScreenManager;
using Microsoft.Xna.Framework;

namespace Graphics.GUI.Controls
{
    public abstract class ControlBase : DrawableGameComponent
    {
        #region Fields

        /// <summary>
        /// Unique name of a Control
        /// </summary>
        public string Name = null;

        /// <summary>
        /// Location of our Control X than Y
        /// </summary>
        public Vector2 Location = new Vector2(0, 0);
        
        /// <summary>
        /// Size of our Control Width than Height
        /// </summary>
        public Vector2 Size = new Vector2(0, 0);
        
        /// <summary>
        /// Controls Current State
        /// </summary>
        public Enumerationcs.ControlState State = Enumerationcs.ControlState.Normal;

        /// <summary>
        /// Gets and Sets if this control will run the TabStop event when it is focused
        /// </summary>
        public bool TabStop = false;
        
        #endregion

        #region Properties
        
        /// <summary>
        /// Bounds of our Object
        /// </summary>
        public Rectangle Bounds { get { return new Rectangle((int)Location.X, (int)Location.Y, (int)Size.X, (int)Size.Y); } }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Base to our Control
        /// </summary>
        /// <param name="game"></param>
        protected ControlBase(Game game) : base(game)
        {
            GuiHandler.Initialize(this); //Automatically Initializes ALL interfaces attached to any method that inherits from this one
            DrawOrder = ScreenHandler.DrawLevel + 200;
        }

        #endregion

        #region Abstract Methods



        #endregion
    }
}
