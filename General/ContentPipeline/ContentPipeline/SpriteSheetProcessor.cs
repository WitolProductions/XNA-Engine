// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// Source Code from Microsoft: http://create.msdn.com/en-US/education/catalog/sample/sprite_sheet using some modifications.
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: SpriteSheetProcessor.cs Version: 1.0 Last Edited: 7/27/2012
// ------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace SpriteSheetPipeline
{
    /// <summary>
    /// Custom content processor takes an array of individual sprite filenames (which
    /// will typically be imported from an XML file), reads them all into memory,
    /// arranges them onto a single larger texture, and returns the resulting sprite
    /// sheet object.
    /// </summary>
    [ContentProcessor]
    public class SpriteSheetProcessor : ContentProcessor<string[], SpriteSheetContent>
    {
        /// <summary>
        /// Converts an array of sprite filenames into a sprite sheet object.
        /// </summary>
        public override SpriteSheetContent Process(string[] input, ContentProcessorContext context)
        {
            var spriteSheet = new SpriteSheetContent();
            var sourceSprites = new List<BitmapContent>();

            // Loop over each input sprite filename.
            foreach (var inputFilename in input)
            {
                // Store the name of this sprite.
                var spriteName = Path.GetFileNameWithoutExtension(inputFilename);

                if (spriteName != null)
                    if (!spriteSheet.SpriteNames.ContainsKey(spriteName))
                        spriteSheet.SpriteNames.Add(spriteName, sourceSprites.Count);

                // Load the sprite texture into memory.
                var textureReference = new ExternalReference<TextureContent>(inputFilename);

                var texture = context.BuildAndLoadAsset<TextureContent, TextureContent>(textureReference, "TextureProcessor");

                sourceSprites.Add(texture.Faces[0][0]);
            }

            // Pack all the sprites into a single large texture.
            var packedSprites = SpritePacker.PackSprites(sourceSprites, spriteSheet.SpriteRectangles, context);

            spriteSheet.Texture.Mipmaps.Add(packedSprites);

            return spriteSheet;
        }
    }
}
