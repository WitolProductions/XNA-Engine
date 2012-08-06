// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// Source Code from Microsoft: http://create.msdn.com/en-US/education/catalog/sample/sprite_sheet using some modifications.
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: SpritePacker.cs Version: 1.0 Last Edited: 7/27/2012
// ------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace SpriteSheetPipeline
{
    /// <summary>
    /// Helper for arranging many small sprites into a single larger sheet.
    /// </summary>
    public static class SpritePacker
    {    
        #region Classes

        /// <summary>
        /// Internal helper class keeps track of a sprite while it is being arranged.
        /// </summary>
        class ArrangedSprite
        {
            public int Index;

            public int X;
            public int Y;

            public int Width;
            public int Height;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Packs a list of sprites into a single big texture,
        /// recording where each one was stored.
        /// </summary>
        public static BitmapContent PackSprites(IList<BitmapContent> sourceSprites, ICollection<Rectangle> outputSprites, ContentProcessorContext context)
        {
            if (sourceSprites.Count == 0)
                throw new InvalidContentException("There are no sprites to arrange");

            // Build up a list of all the sprites needing to be arranged.
            var sprites = sourceSprites.Select((t, i) => new ArrangedSprite { Width = t.Width + 2, Height = t.Height + 2, Index = i }).ToList();

            // Sort so the largest sprites get arranged first.
            sprites.Sort(CompareSpriteSizes);

            // Work out how big the output bitmap should be.
            var outputWidth = GuessOutputWidth(sprites);
            var outputHeight = 0;
            var totalSpriteSize = 0;

            // Choose positions for each sprite, one at a time.
            for (var i = 0; i < sprites.Count; i++)
            {
                PositionSprite(sprites, i, outputWidth);

                outputHeight = Math.Max(outputHeight, sprites[i].Y + sprites[i].Height);

                totalSpriteSize += sprites[i].Width * sprites[i].Height;
            }

            // Sort the sprites back into index order.
            sprites.Sort(CompareSpriteIndices);

            context.Logger.LogImportantMessage("Packed {0} sprites into a {1}x{2} sheet, {3}% efficiency", sprites.Count,
                                                outputWidth, outputHeight, totalSpriteSize * 100 / outputWidth / outputHeight);

            return CopySpritesToOutput(sprites, sourceSprites, outputSprites, outputWidth, outputHeight);
        }

        /// <summary>
        /// Once the arranging is complete, copies the bitmap data for each
        /// sprite to its chosen position in the single larger output bitmap.
        /// </summary>
        static BitmapContent CopySpritesToOutput(IEnumerable<ArrangedSprite> sprites, IList<BitmapContent> sourceSprites, ICollection<Rectangle> outputSprites, int width, int height)
        {
            BitmapContent output = new PixelBitmapContent<Color>(width, height);

            foreach (var sprite in sprites)
            {
                var source = sourceSprites[sprite.Index];

                var x = sprite.X;
                var y = sprite.Y;

                var w = source.Width;
                var h = source.Height;

                // Copy the main sprite data to the output sheet.
                BitmapContent.Copy(source, new Rectangle(0, 0, w, h), output, new Rectangle(x + 1, y + 1, w, h));

                // Copy a border strip from each edge of the sprite, creating a one pixel padding area to avoid filtering problems if the sprite is scaled or rotated.
                BitmapContent.Copy(source, new Rectangle(0, 0, 1, h), output, new Rectangle(x, y + 1, 1, h));

                BitmapContent.Copy(source, new Rectangle(w - 1, 0, 1, h), output, new Rectangle(x + w + 1, y + 1, 1, h));

                BitmapContent.Copy(source, new Rectangle(0, 0, w, 1), output, new Rectangle(x + 1, y, w, 1));

                BitmapContent.Copy(source, new Rectangle(0, h - 1, w, 1), output, new Rectangle(x + 1, y + h + 1, w, 1));

                // Copy a single pixel from each corner of the sprite,
                // filling in the corners of the one pixel padding area.
                BitmapContent.Copy(source, new Rectangle(0, 0, 1, 1), output, new Rectangle(x, y, 1, 1));

                BitmapContent.Copy(source, new Rectangle(w - 1, 0, 1, 1), output, new Rectangle(x + w + 1, y, 1, 1));

                BitmapContent.Copy(source, new Rectangle(0, h - 1, 1, 1), output, new Rectangle(x, y + h + 1, 1, 1));

                BitmapContent.Copy(source, new Rectangle(w - 1, h - 1, 1, 1), output, new Rectangle(x + w + 1, y + h + 1, 1, 1));

                // Remember where we placed this sprite.
                outputSprites.Add(new Rectangle(x + 1, y + 1, w, h));
            }

            return output;
        }

        /// <summary>
        /// Works out where to position a single sprite.
        /// </summary>
        static void PositionSprite(IList<ArrangedSprite> sprites, int index, int outputWidth)
        {
            var x = 0;
            var y = 0;

            while (true)
            {
                // Is this position free for us to use?
                var intersects = FindIntersectingSprite(sprites, index, x, y);

                if (intersects < 0)
                {
                    sprites[index].X = x;
                    sprites[index].Y = y;

                    return;
                }

                // Skip past the existing sprite that we collided with.
                x = sprites[intersects].X + sprites[intersects].Width;

                // If we ran out of room to move to the right, try the next line down instead.
                if (x + sprites[index].Width <= outputWidth) continue;
                x = 0;
                y++;
            }
        }
        
        /// <summary>
        /// Checks if a proposed sprite position collides with anything
        /// that we already arranged.
        /// </summary>
        static int FindIntersectingSprite(IList<ArrangedSprite> sprites, int index, int x, int y)
        {
            var w = sprites[index].Width;
            var h = sprites[index].Height;

            for (var i = 0; i < index; i++)
            {
                if (sprites[i].X >= x + w)
                    continue;

                if (sprites[i].X + sprites[i].Width <= x)
                    continue;

                if (sprites[i].Y >= y + h)
                    continue;

                if (sprites[i].Y + sprites[i].Height <= y)
                    continue;

                return i;
            }

            return -1;
        }
        
        /// <summary>
        /// Comparison function for sorting sprites by size.
        /// </summary>
        static int CompareSpriteSizes(ArrangedSprite a, ArrangedSprite b)
        {
            var aSize = a.Height * 1024 + a.Width;
            var bSize = b.Height * 1024 + b.Width;

            return bSize.CompareTo(aSize);
        }

        /// <summary>
        /// Comparison function for sorting sprites by their original indices.
        /// </summary>
        static int CompareSpriteIndices(ArrangedSprite a, ArrangedSprite b)
        {
            return a.Index.CompareTo(b.Index);
        }
        
        /// <summary>
        /// Heuristic guesses what might be a good output width for a list of sprites.
        /// </summary>
        static int GuessOutputWidth(ICollection<ArrangedSprite> sprites)
        {
            // Gather the widths of all our sprites into a temporary list.
            var widths = sprites.Select(sprite => sprite.Width).ToList();

            // Sort the widths into ascending order.
            widths.Sort();

            // Extract the maximum and median widths.
            var maxWidth = widths[widths.Count - 1];
            var medianWidth = widths[widths.Count / 2];

            // Heuristic assumes an NxN grid of median sized sprites.
            var width = medianWidth * (int)Math.Round(Math.Sqrt(sprites.Count));

            // Make sure we never choose anything smaller than our largest sprite.
            return Math.Max(width, maxWidth);
        }

        #endregion
    }      
}
