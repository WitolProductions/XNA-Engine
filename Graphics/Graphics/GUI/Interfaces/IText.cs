// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: IText.cs Version: 1.0 Last Edited: 9/18/2012
// ------------------------------------------------------------------------

using Graphics.GUI.Controls;
using Microsoft.Xna.Framework;

namespace Graphics.GUI.Interfaces
{
    /// <summary>
    /// IText implements an Interface that allows writting a single line of text onto a Control, requires a valid IFont in order to work
    /// </summary>
    interface IText : IFont
    {
        #region Properties

        /// <summary>
        /// Text to be Written 
        /// </summary>
        string Text { get; set; }
        
        #endregion

        #region Events

        /// <summary>
        /// Text was changed
        /// </summary>
        event ControlEvent TextChanged;

        #endregion

        #region Abstract Events

        /// <summary>
        /// On Text Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void OnTextChanged(object sender, object eventArgs);

        #endregion
    }

    /// <summary>
    /// Handles Drawing and Updating our Text Interface
    /// </summary>
    public static class Text
    {
        /// <summary>
        /// Updater our IText Interface in conjunction with the passed control
        /// </summary>
        public static void Update(object controlBase, GameTime gameTime)
        {
            var control = (ControlBase)controlBase;

        }

        /// <summary>
        /// Draw our IText Interface in conjunction with the passed control
        /// </summary>
        public static void Draw(object controlBase, GameTime gameTime)
        {
            //Gather information on Font, Color, Text, etc. and then draw it
            var control = (ControlBase)controlBase;
            var font = GuiHandler.GetPropertyValue(control, "Font") as string;
            var text = GuiHandler.GetPropertyValue(control, "Text") as string;
            var location = control.Location;
            var color = GuiHandler.GetPropertyValue(control, "FontColor") is Color ? (Color)GuiHandler.GetPropertyValue(control, "FontColor") : Color.Transparent;
            GraphicsHandler.DrawString(font, text, location, color);
        }
    }
}
