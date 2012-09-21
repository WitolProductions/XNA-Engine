// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: IFont.cs Version: 1.0 Last Edited: 9/18/2012
// ------------------------------------------------------------------------

using Graphics.GUI.Controls;
using Microsoft.Xna.Framework;

namespace Graphics.GUI.Interfaces
{
    /// <summary>
    /// Provides font support for controls
    /// </summary>
    public interface IFont
    {
        #region Properties

        /// <summary>
        /// Font's Name
        /// </summary>
        string Font { get; set; }

        /// <summary>
        /// Color of font
        /// </summary>
        Color FontColor { get; set; }

        /// <summary>
        /// Alpha of Font
        /// </summary>
        float FontAlpha { get; set; }

        #endregion
    }

    /// <summary>
    /// Handles Updating our Font Interface
    /// </summary>
    public static class Font
    {
        /// <summary>
        /// Initializes our Control with base information needed
        /// </summary>
        /// <param name="controlBase"></param>
        public static void Initialize(object controlBase)
        {
            GuiHandler.SetPropertyValue(controlBase, "FontAlpha", 1f);
            //GuiHandler.SetPropertyValue(controlBase, "Font", "Defualt");  TODO: Define a defualt font thats always available
        }

        /// <summary>
        /// Update our IFont Interface in conjunction with the passed control
        /// </summary>
        public static void Update(object controlBase, GameTime gameTime)
        {
            var control = (ControlBase)controlBase;

        }
    }
}
