// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// Source Code from Microsoft: http://create.msdn.com/en-US/education/catalog/sample/sprite_sheet using some modifications.
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: SpriteSheetContent.cs Version: 1.0 Last Edited: 7/27/2012
// ------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace ContentPipeline.TexturePipeline
{
    /// <summary>
    /// Build-time type used to hold the output data from the SpriteSheetProcessor.
    /// This is serialized into XNB format, then at runtime, the ContentManager
    /// loads the data into a SpriteSheet object.
    /// </summary>
    [ContentSerializerRuntimeType("Content.ContentTypes.SpriteSheet, Content")]
    public class SpriteSheetContent
    {
        // Single texture contains many separate sprite images.
        public Texture2DContent Texture = new Texture2DContent();

        // Remember where in the texture each sprite has been placed.
        public List<Rectangle> SpriteRectangles = new List<Rectangle>();

        // Store the original sprite filenames, so we can look up sprites by name.
        public Dictionary<string, int> SpriteNames = new Dictionary<string, int>();
    }
}
