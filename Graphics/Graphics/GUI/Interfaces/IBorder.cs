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

using Microsoft.Xna.Framework;

namespace Graphics.GUI.Interfaces
{
    /// <summary>
    /// Provides support for a control to contain a border Image and/or Color, requires IEvents in order to function properly
    /// </summary>
    interface IBorder : IEvents
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

        #endregion
    }
}
