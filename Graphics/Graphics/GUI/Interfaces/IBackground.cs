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

using System.Linq;
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
        /// Initializes our Control with base information needed
        /// </summary>
        /// <param name="controlBase"></param>
        public static void Initialize(object controlBase)
        {
            GuiHandler.SetPropertyValue(controlBase, "BackgroundNormalAlpha", 1f);
            GuiHandler.SetPropertyValue(controlBase, "BackgroundClickedAlpha", 1f);
            GuiHandler.SetPropertyValue(controlBase, "BackgroundHoverAlpha", 1f);
            GuiHandler.SetPropertyValue(controlBase, "BackgroundDisabledAlpha", 1f);
        }
        
        /// <summary>
        /// Update our IBackground Interface in conjunction with the passed control
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

            #region Control States - Background Color

            var color = Color.Transparent;
            switch (control.State)
            {
                case Enumerations.ControlState.Normal:
                    color = GuiHandler.GetPropertyValue(control, "BackgroundNormalColor") is Color ? (Color)GuiHandler.GetPropertyValue(control, "BackgroundNormalColor") * (float) GuiHandler.GetPropertyValue(control, "BackgroundNormalAlpha")
                        : Color.Transparent;
                    break;
                case Enumerations.ControlState.Clicked:
                    color = GuiHandler.GetPropertyValue(control, "BackgroundClickedColor") is Color ? (Color)GuiHandler.GetPropertyValue(control, "BackgroundClickedColor") * (float)GuiHandler.GetPropertyValue(control, "BackgroundClickedAlpha") 
                        : Color.Transparent;
                    break;
                case Enumerations.ControlState.Hover:
                    color = GuiHandler.GetPropertyValue(control, "BackgroundHoverColor") is Color ? (Color)GuiHandler.GetPropertyValue(control, "BackgroundHoverColor") * (float)GuiHandler.GetPropertyValue(control, "BackgroundHoverAlpha") 
                        : Color.Transparent;
                    break;
                case Enumerations.ControlState.Disabled:
                    color = GuiHandler.GetPropertyValue(control, "BackgroundDisabledColor") is Color ? (Color)GuiHandler.GetPropertyValue(control, "BackgroundDisabledColor") * (float)GuiHandler.GetPropertyValue(control, "BackgroundDisabledAlpha") 
                        : Color.Transparent;
                    break;
            }

            #endregion

            var bounds = control.Bounds;

            //If border is used with this control we need to alter the bounds a bit
            if (control.GetType().GetInterfaces().Where(e => e.Name == "IBorder").Count() > 0)
            {
                var borderWidth = (int)GuiHandler.GetPropertyValue(control, "BorderWidth");
                var x = (int) control.Location.X + borderWidth;
                var y = (int) control.Location.Y + borderWidth;
                var width =  (int)control.Size.X - borderWidth;
                var height = (int)control.Size.Y - borderWidth;

                bounds = new Rectangle(x, y, width, height);
            }

            GraphicsHandler.DrawFillRectangle(bounds, color);

        }
    }
}
