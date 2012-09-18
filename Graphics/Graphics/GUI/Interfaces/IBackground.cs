// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: IBackground.cs Version: 1.0 Last Edited: 9/13/2012
// ------------------------------------------------------------------------

using Graphics.GUI.Controls;
using Graphics.Misc;
using Microsoft.Xna.Framework;

namespace Graphics.GUI.Interfaces
{
    /// <summary>
    /// Provides support for a control to contain a background Image and/or Color, requires IEvents in order to function properly
    /// </summary>
    public interface IBackground : IEvents
    {
        #region Properties

        /// <summary>
        /// Background Images Name when Normal
        /// </summary>
        string BackgroundNormalImage { get; set; }

        /// <summary>
        /// Background Images Name when Hovering
        /// </summary>
        string BackgroundHoverImage { get; set; }

        /// <summary>
        /// Background Images Name when Clicked
        /// </summary>
        string BackgroundClickedImage { get; set; }

        /// <summary>
        /// Background Images Name when Disabled
        /// </summary>
        string BackgroundDisabledImage { get; set; }

        /// <summary>
        /// Color of background when Normal
        /// </summary>
        Color BackgroundNormalColor { get; set; }

        /// <summary>
        /// Color of background while Hovering
        /// </summary>
        Color BackgroundHoverColor { get; set; }

        /// <summary>
        /// Color of background while Clicked
        /// </summary>
        Color BackgroundClickedColor { get; set; }

        /// <summary>
        /// Color of background while Disabled
        /// </summary>
        Color BackgroundDisabledColor { get; set; }

        /// <summary>
        /// Alpha used on the Color of the background when Normal
        /// </summary>
        float BackgroundNormalAlpha { get; set; }

        /// <summary>
        /// Alpha used on the Color of the background when Hovering
        /// </summary>
        float BackgroundHoverAlpha { get; set; }

        /// <summary>
        /// Alpha used on the Color of the background when Clicked
        /// </summary>
        float BackgroundClickedAlpha { get; set; }

        /// <summary>
        /// Alpha used on the Color of the background when Disabled
        /// </summary>
        float BackgroundDisabledAlpha { get; set; }
        
        #endregion
    }

    /// <summary>
    /// Handles Drawing and Updating our Background Interface
    /// </summary>
    public static class Background
    {
        /// <summary>
        /// Updater our IBackground Interface in conjunction with the passed control
        /// </summary>
        public static void Update(object controlBase, GameTime gameTime)
        {
            var control = (ControlBase)controlBase;

        }

        /// <summary>
        /// Draw our IBackground Interface in conjunction with the passed control
        /// </summary>
        public static void Draw(object controlBase, GameTime gameTime)
        {
            var control = (ControlBase)controlBase;

            var color = Color.Transparent;
            if (control.State == Enumerationcs.ControlState.Normal)
                color = GuiHandler.GetPropertyValue(control, "BackgroundNormalColor") is Color ? (Color)GuiHandler.GetPropertyValue(control, "BackgroundNormalColor") : Color.Transparent;
            else if (control.State == Enumerationcs.ControlState.Clicked)
                color = GuiHandler.GetPropertyValue(control, "BackgroundClickedColor") is Color ? (Color)GuiHandler.GetPropertyValue(control, "BackgroundClickedColor") : Color.Transparent;
            GraphicsHandler.DrawFillRectangle(control.Bounds, color);

        }
    }
}
