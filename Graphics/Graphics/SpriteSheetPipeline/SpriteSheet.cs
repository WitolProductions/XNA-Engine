// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// Source Code from Microsoft: http://create.msdn.com/en-US/education/catalog/sample/sprite_sheet using some modifications.
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: SpriteSheet.cs Version: 1.0 Last Edited: 7/27/2012
// ------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics.SpriteSheetPipeline
{
    /// <summary>
    /// A sprite sheet contains many individual sprite images, packed into different
    /// areas of a single larger texture, along with information describing where in
    /// that texture each sprite is located. Sprite sheets can make your game drawing
    /// more efficient, because they reduce the number of times the graphics hardware
    /// needs to switch from one texture to another.
    /// </summary>
    public class SpriteSheet
    {
        #region Fields

        // Single texture contains many separate sprite images.
        [ContentSerializer]
        Texture2D texture = null;

        // Remember where in the texture each sprite has been placed.
        [ContentSerializer]
        List<Rectangle> spriteRectangles = null;

        // Store the original sprite filenames, so we can look up sprites by name.
        [ContentSerializer]
        Dictionary<string, int> spriteNames = null;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the single large texture used by this sprite sheet.
        /// </summary>
        [ContentSerializerIgnore]
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        /// <summary>
        /// Gets and Sets list of Sprite names
        /// </summary>
        [ContentSerializerIgnore]
        public Dictionary<string, int> SpriteNames { get { return spriteNames; } set { spriteNames = value; } }

        /// <summary>
        /// Gets and Sets list of our Sprites Source Rectangles
        /// </summary>
        [ContentSerializerIgnore]
        public List<Rectangle> SpriteRectangles { get { return spriteRectangles; } set { spriteRectangles = value; } }

        /// <summary>
        /// Gets and Sets if the Texture is loaded or not
        /// </summary>
        [ContentSerializerIgnore]
        public bool Loaded { get; set; }

        /// <summary>
        /// Gets and Sets our Unload Timer which allows this class to automatically unload uneeded data
        /// </summary>
        [ContentSerializerIgnore]
        public int UnloadTimer { get; set; }

        /// <summary>
        /// Gets and Sets the path of this Sprite Sheet
        /// </summary>
        [ContentSerializerIgnore]
        public string Path { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Looks up the location of the specified sprite within the big texture.
        /// </summary>
        public Rectangle SourceRectangle(string spriteName)
        {
            var spriteIndex = GetIndex(spriteName);

            return spriteRectangles[spriteIndex];
        }
        
        /// <summary>
        /// Looks up the location of the specified sprite within the big texture.
        /// </summary>
        public Rectangle SourceRectangle(int spriteIndex)
        {
            if ((spriteIndex < 0) || (spriteIndex >= spriteRectangles.Count))
                throw new ArgumentOutOfRangeException("spriteIndex");

            return spriteRectangles[spriteIndex];
        }
        
        /// <summary>
        /// Looks up the numeric index of the specified sprite. This is useful when
        /// implementing animation by cycling through a series of related sprites.
        /// </summary>
        public int GetIndex(string spriteName)
        {
            int index;

            if (!spriteNames.TryGetValue(spriteName, out index))
            {
                const string error = "SpriteSheet does not contain a sprite named '{0}'.";

                throw new KeyNotFoundException(string.Format(error, spriteName));
            }

            return index;
        }

        #endregion
    }
}
