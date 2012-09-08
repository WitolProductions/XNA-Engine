// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Font.cs Version: 1.0 Last Edited: 9/8/2012
// ------------------------------------------------------------------------

using Microsoft.Xna.Framework.Graphics;

namespace Content.ContentTypes
{
    public class Font
    {
        /// <summary>
        /// Our Sprite Font
        /// </summary>
        public SpriteFont SpriteFont;

        /// <summary>
        /// Path to our Font file
        /// </summary>
        public string Path = null;

        /// <summary>
        /// Gets and Sets if our SpriteFont has been loaded
        /// </summary>
        public bool Loaded = false;
    }
}
