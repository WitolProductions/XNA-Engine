// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: IPictureBox.cs Version: 1.0 Last Edited: 9/18/2012
// ------------------------------------------------------------------------

using System.Collections.Generic;
using Graphics.GUI.Controls;
using Microsoft.Xna.Framework;

namespace Graphics.GUI.Interfaces
{   
    /// <summary>
    /// IPicture implements an Interface that allows usable Textures inside of our controls
    /// </summary>
    public interface IPicture
    {
        #region Properties

        List<Image> Pictures { get; set; }

        #endregion
    }

    /// <summary>
    /// Handles Drawing and Updating our IPicture Interface
    /// </summary>
    public static class Picture
    {
        /// <summary>
        /// Initializes our Control with base information needed
        /// </summary>
        /// <param name="controlBase"></param>
        public static void Initialize(object controlBase)
        {
        }

        /// <summary>
        /// Update our IPicture Interface in conjunction with the passed control
        /// </summary>
        public static void Update(object controlBase, GameTime gameTime)
        {
            var control = (ControlBase)controlBase;

        }

        /// <summary>
        /// Draw our IPicture Interface in conjunction with the passed control
        /// </summary>
        public static void Draw(object controlBase, GameTime gameTime)
        {
   
        }
    }

    /// <summary>
    /// Simple struct that holds a Texture Name and two rectangles instructing it how to draw
    /// </summary>
    public struct Image
    {
        /// <summary>
        /// Name of Texture being drawn
        /// </summary>
        public string TextureName { get; set; }

        /// <summary>
        /// Destination of texture being drawn
        /// </summary>
        public Rectangle Destination { get; set; }

        public Image(string textureName, Rectangle destination)
            : this()
        {
            TextureName = textureName;
            Destination = destination;
        }
    }
}
