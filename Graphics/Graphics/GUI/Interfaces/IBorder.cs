// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: IBorder.cs Version: 1.0 Last Edited: 9/13/2012
// ------------------------------------------------------------------------

using Graphics.GUI.Controls;
using Graphics.Misc;
using Microsoft.Xna.Framework;

namespace Graphics.GUI.Interfaces
{
    /// <summary>
    /// Provides support for a control to contain a border Image and/or Color, requires IEvents in order to function properly
    /// </summary>
    public interface IBorder : IEvents
    {
        #region Properties
        
        /// <summary>
        /// Border Images Name when Normal
        /// </summary>
        string BorderNormalImage { get; set; }

        /// <summary>
        /// Border Images Name when Hovering
        /// </summary>
        string BorderHoverImage { get; set; }

        /// <summary>
        /// Border Images Name when Clicked
        /// </summary>
        string BorderClickedImage { get; set; }

        /// <summary>
        /// Border Images Name when Disabled
        /// </summary>
        string BorderDisabledImage { get; set; }

        /// <summary>
        /// Color of Border when Normal
        /// </summary>
        Color BorderNormalColor { get; set; }

        /// <summary>
        /// Color of Border while Hovering
        /// </summary>
        Color BorderHoverColor { get; set; }

        /// <summary>
        /// Color of Border while Clicked
        /// </summary>
        Color BorderClickedColor { get; set; }

        /// <summary>
        /// Color of Border while Disabled
        /// </summary>
        Color BorderDisabledColor { get; set; }

        /// <summary>
        /// Alpha used on the Color of the Border when Normal
        /// </summary>
        float BorderNormalAlpha { get; set; }

        /// <summary>
        /// Alpha used on the Color of the Border when Hovering
        /// </summary>
        float BorderHoverAlpha { get; set; }

        /// <summary>
        /// Alpha used on the Color of the Border when Clicked
        /// </summary>
        float BorderClickedAlpha { get; set; }

        /// <summary>
        /// Alpha used on the Color of the Border when Disabled
        /// </summary>
        float BorderDisabledAlpha { get; set; }

        /// <summary>
        /// Border Width used when drawing a border of Colors
        /// </summary>
        int BorderWidth { get; set; }

        #endregion
    }
    
    /// <summary>
    /// Handles Drawing and Updating our Border Interface
    /// </summary>
    public static class Border
    {
        /// <summary>
        /// Initializes our Control with base information needed
        /// </summary>
        /// <param name="controlBase"></param>
        public static void Initialize(object controlBase)
        {
            GuiHandler.SetPropertyValue(controlBase, "BorderNormalAlpha", 1f);
            GuiHandler.SetPropertyValue(controlBase, "BorderClickedAlpha", 1f);
            GuiHandler.SetPropertyValue(controlBase, "BorderHoverAlpha", 1f);
            GuiHandler.SetPropertyValue(controlBase, "BorderDisabledAlpha", 1f);
            GuiHandler.SetPropertyValue(controlBase, "BorderWidth", 1);
        }

        /// <summary>
        /// Update our IBorder Interface in conjunction with the passed control
        /// </summary>
        public static void Update(object controlBase, GameTime gameTime)
        {
            var control = (ControlBase)controlBase;

        }

        /// <summary>
        /// Draw our IBorder Interface in conjunction with the passed control
        /// </summary>
        public static void Draw(object controlBase, GameTime gameTime)
        {
            var control = (ControlBase)controlBase;

            #region Control States - Border Color

            var color = Color.Transparent;
            switch (control.State)
            {
                case Enumerations.ControlState.Normal:
                    color = GuiHandler.GetPropertyValue(control, "BorderNormalColor") is Color ? (Color)GuiHandler.GetPropertyValue(control, "BorderNormalColor") * (float)GuiHandler.GetPropertyValue(control, "BorderNormalAlpha")
                        : Color.Transparent;
                    break;
                case Enumerations.ControlState.Clicked:
                    color = GuiHandler.GetPropertyValue(control, "BorderClickedColor") is Color ? (Color)GuiHandler.GetPropertyValue(control, "BorderClickedColor") * (float)GuiHandler.GetPropertyValue(control, "BorderClickedAlpha")
                        : Color.Transparent;
                    break;
                case Enumerations.ControlState.Hover:
                    color = GuiHandler.GetPropertyValue(control, "BorderHoverColor") is Color ? (Color)GuiHandler.GetPropertyValue(control, "BorderHoverColor") * (float)GuiHandler.GetPropertyValue(control, "BorderHoverAlpha")
                        : Color.Transparent;
                    break;
                case Enumerations.ControlState.Disabled:
                    color = GuiHandler.GetPropertyValue(control, "BorderDisabledColor") is Color ? (Color)GuiHandler.GetPropertyValue(control, "BorderDisabledColor") * (float)GuiHandler.GetPropertyValue(control, "BorderDisabledAlpha")
                        : Color.Transparent;
                    break;
            }

            #endregion


            GraphicsHandler.DrawRectangle(control.Bounds, color, (int) GuiHandler.GetPropertyValue(control, "BorderWidth"));


        }
    }
}
