// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Graphics.cs Version: 1.0 Last Edited: 9/4/2012
// ------------------------------------------------------------------------

using System;
using System.Reflection;
using System.Text;
using Content.ContentHolders;
using Content.ContentTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics
{
    public class GraphicsHandler
    {
        #region fields

        /// <summary>
        /// Grants access to our Graphics Device 
        /// </summary>
        static GraphicsDevice _graphicsDevice = null;

        /// <summary>
        /// Grants access to the Sprite Batch
        /// </summary>
        static SpriteBatch _spriteBatch = null;

        static bool _spriteBatchActive = false;

        #endregion
        
        #region Initialization

        public static void Initialize(Game game)
        {
            _graphicsDevice = game.GraphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
        }

        #endregion

        #region Special Methods

        public static void Clear(Color color)
        {
            if (_graphicsDevice == null) throw new Exception("Graphics Device cannot be null - Clear(Color)");
            _graphicsDevice.Clear(color);
        }

        public static void Begin()
        {
            if (_spriteBatchActive) return; //Do not allow attempts at running Begin multiple times
            _spriteBatch.Begin();
            _spriteBatchActive = true;
        }

        /// <summary>
        /// End our Sprite Batch and draw all images to screen
        /// </summary>
        public static void End()
        {
            if (!_spriteBatchActive) return; //Only try and end if we were running our Sprite Batch to begin with
            _spriteBatch.End();
            _spriteBatchActive = false;
        }

        /// <summary>
        /// Major Method that is used for every single draw call made
        /// </summary>
        /// <param name="texture">Texture being Drawn</param>
        /// <param name="destination">Destination to draw to, if Width/Height are 0 than it only has X and Y values</param>
        /// <param name="source">Source information on the texture we are drawing</param>
        /// <param name="color">Color</param>
        /// <param name="rotation">Rotation used in our drawing</param>
        /// <param name="origin">Origin used in our drawing</param>
        /// <param name="scalef">Scale value listed as a float</param>
        /// <param name="scalev">Scale value listed as a Vector2</param>
        /// <param name="spriteEffects">Sprite Effects</param>
        /// <param name="layerDepth">Layer Depth</param>
        static void RealDraw(Texture2D texture, Rectangle destination, Rectangle? source, Color color, float? rotation, Vector2? origin, float? scalef, Vector2? scalev, SpriteEffects? spriteEffects, float? layerDepth)
        {
            if (!_spriteBatchActive) return;
            if (texture == null) return; //If texture is null return
            //If our Destinations Width or Height is equal to 0 than we know we are using an entire rectangle
            if (destination.Width == 0 || destination.Height == 0)
            {
                if (!source.HasValue)
                    _spriteBatch.Draw(texture, new Vector2(destination.X, destination.Y), color);
                else
                {
                    //If we have values for all things sent than use them
                    if (rotation.HasValue && origin.HasValue && spriteEffects.HasValue && layerDepth.HasValue)
                    {
                        //Determine if we have Vector2 or float scale
                        if (!scalev.HasValue && scalef.HasValue)
                            _spriteBatch.Draw(texture, new Vector2(destination.X, destination.Y), source, color, (float)rotation, (Vector2)origin, (float)scalef, (SpriteEffects)spriteEffects, (float)layerDepth);
                        else if (scalev.HasValue && !scalef.HasValue)
                            _spriteBatch.Draw(texture, new Vector2(destination.X, destination.Y), source, color, (float)rotation, (Vector2)origin, (Vector2)scalev, (SpriteEffects)spriteEffects, (float)layerDepth);
                    }
                    else //Else follow with the remaining path
                        _spriteBatch.Draw(texture, new Vector2(destination.X, destination.Y), source, color);
                }
            }
            else //Else we know that destination values where sent and must be used
            {
                if (!source.HasValue)
                    _spriteBatch.Draw(texture, destination, color);
                else
                {
                    //If we have values for everything sent than draw using them
                    if (rotation.HasValue && origin.HasValue && spriteEffects.HasValue && layerDepth.HasValue)
                        _spriteBatch.Draw(texture, destination, source, color, (float)rotation, (Vector2)origin, (SpriteEffects)spriteEffects, (float)layerDepth);
                    else //Else follow the remaining path
                        _spriteBatch.Draw(texture, destination, source, color);
                }
            }
        }

        /// <summary>
        /// Major method that is used for every single draw call drawing text
        /// </summary>
        /// <param name="font">Font texture used</param>
        /// <param name="text">Text to be displayed</param>
        /// <param name="position">Position of text placment</param>
        /// <param name="color">Color of text</param>
        /// <param name="rotation">Rotation used on text</param>
        /// <param name="origin">Origin of text</param>
        /// <param name="scalef">Scale value listed as a Float</param>
        /// <param name="scalev">Scale value listed as a Vector2</param>
        /// <param name="spriteEffects">Sprite Effects</param>
        /// <param name="layerDepth">Layer Depth</param>
        static void RealSpriteFontDraw(SpriteFont font, StringBuilder text, Vector2 position, Color color, float? rotation, Vector2? origin, float? scalef, Vector2? scalev, SpriteEffects? spriteEffects, float? layerDepth)
        {
            if (!_spriteBatchActive) return;
            if (font == null) return; //If font is null return
            if (String.IsNullOrEmpty(text.ToString())) return; //Dont try drawing null text
            
            if (rotation.HasValue && origin.HasValue && (scalef.HasValue || scalev.HasValue) && spriteEffects.HasValue && layerDepth.HasValue)
            {//If all our nullable values are present then check which scale was passed to see which method to use
                if (scalef.HasValue)
                    _spriteBatch.DrawString(font, text, position, color, (float)rotation, (Vector2)origin, (float)scalef, (SpriteEffects)spriteEffects, (float)layerDepth);
                else
                    _spriteBatch.DrawString(font, text, position, color, (float)rotation, (Vector2)origin, (Vector2)scalev, (SpriteEffects)spriteEffects, (float)layerDepth);
            }
            else
                _spriteBatch.DrawString(font, text, position, color);
        }

        /// <summary>
        /// Get a Texture from a name and type
        /// </summary>
        /// <param name="name">Textures name</param>
        /// <param name="type">Type of content</param>
        /// <returns></returns>
        static Texture2D GetTexture(string name, MemberInfo type)
        {
            Texture2D texture;

            switch (type.Name)
            {
                case "Texture2D":
                    {
                        if ((texture = Textures.Texture<Texture2D>(name)) != null)
                            return texture;
                    } break;
                case "SpriteSheet":
                    {
                        if ((texture = Textures.Texture<SpriteSheet>(name)) != null)
                            return texture;
                    } break;
            }
            //Error was met
            throw new Exception("Cannot find texture: " + name + " of type " + type.Name); 
        }

        /// <summary>
        /// Get a textures source rectangles from a name and type
        /// </summary>
        /// <param name="name">Name of the texture</param>
        /// <param name="type">Type of content</param>
        /// <returns></returns>
        static Rectangle? GetSource(string name, MemberInfo type)
        {
            switch (type.Name)
            {
                case "Texture2D":
                    {
                        return Textures.SourceRectangle<Texture2D>(name);
                    }
                    
                case "SpriteSheet":
                    {
                        return Textures.SourceRectangle<SpriteSheet>(name);
                    }
            }
            //Error was met
            throw new Exception("Cannot find source: " + name + " of type " + type.Name); 
        }

        /// <summary>
        /// Get a font based on the name passed
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static SpriteFont GetFont(string name)
        {
            SpriteFont font;
            
            if ((font = Textures.SpriteFont(name)) != null)
                return font;
            
            //Error was met
            throw new Exception("Cannot find Font: " + name);
        }

        #endregion

        #region Draw Methods With Source
        
        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="name">Texture name</param>
        /// <param name="type">Texture type</param>
        /// <param name="destination">Destination</param>
        /// <param name="source">Source</param>
        /// <param name="color">Color</param>
        public static void Draw(string name, Type type, Vector2 destination, Rectangle source, Color color)
        {
            RealDraw(GetTexture(name, type), new Rectangle((int)destination.X, (int)destination.Y, 0, 0), source, color, null, null, null, null, null, null);
        }

        #endregion

        #region Draw Methods Without Source

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="name">Texture name</param>
        /// <param name="type">Texture type</param>
        public static void Draw(string name, Type type)
        {
            RealDraw(GetTexture(name, type), new Rectangle(0, 0, 0, 0), GetSource(name, type), Color.White, null, null, null, null, null, null);
        }
        
        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="name">Texture name</param>
        /// <param name="type">Texture type</param>
        /// <param name="destination">Destination</param>
        /// <param name="color">Color</param>
        public static void Draw(string name, Type type, Vector2 destination, Color color)
        {
            RealDraw(GetTexture(name, type), new Rectangle((int)destination.X, (int)destination.Y, 0, 0), GetSource(name, type), color, null, null, null, null, null, null);
        }

        #endregion

        #region Sprite Font Draw Calls

        /// <summary>
        /// Draw String
        /// </summary>
        /// <param name="name">Sprite Font name</param>
        /// <param name="text">Text to draw</param>
        /// <param name="position">Destination</param>
        /// <param name="color">Color</param>
        public static void DrawString(string name, string text, Vector2 position, Color color)
        {
            RealSpriteFontDraw(GetFont(name), new StringBuilder(text), position, color, null, null, null, null, null, null);
        }

        #endregion
    }
}

