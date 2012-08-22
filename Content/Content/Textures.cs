// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Textures.cs Version: 1.0 Last Edited: 8/21/2012
// ------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Content.Content;
using Content.ContentTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Content
{
    public static class Textures
    {
        #region Fields

        /// <summary>
        /// List of our SpriteSheets
        /// </summary>
        static List<SpriteSheet> _spriteSheets = null;

        /// <summary>
        /// List of our Textures
        /// </summary>
        static List<GameTexture> _textures = null;
        
        #endregion

        #region XNA Methods

        public static void Initialize()
        {
            _spriteSheets = new List<SpriteSheet>();
            _textures = new List<GameTexture>();
        }

        public static void Update(GameTime gameTime)
        {
            foreach (var spriteSheet in _spriteSheets.Where(spriteSheet => spriteSheet.Loaded))
            {
                spriteSheet.UnloadTimer = (int) (spriteSheet.UnloadTimer - gameTime.ElapsedGameTime.TotalMilliseconds);

                //Continue if we do not need to unload this texture
                if (spriteSheet.UnloadTimer > 0) continue;
                spriteSheet.Loaded = false;
                ContentHandler.Unload<SpriteSheet>(spriteSheet);
            }
        }

        #endregion

        #region Methods


        #endregion
        
        #region Sprite Sheet Methods

        //Add the passed sprite sheet
        public static void Add(SpriteSheet spriteSheet)
        {
            Remove(spriteSheet);
            
            //Add our sheet
            _spriteSheets.Add(spriteSheet);
        }

        /// <summary>
        /// Remove our Sprite Sheet
        /// </summary>
        /// <param name="spriteSheet"></param>
        static void Remove(SpriteSheet spriteSheet)
        {
            //Remove our sheet if it exists
            if (_spriteSheets.Contains(spriteSheet))
                _spriteSheets.Remove(spriteSheet);
        }
        
        /// <summary>
        /// Looks up the location of the specified sprite within the big texture.
        /// </summary>
        public static Rectangle SourceRectangle(string spriteName)
        {
            var spriteIndex = GetIndex(spriteName);
            var spriteSheet = GetSpriteSheet(spriteName);

            return spriteSheet.SpriteRectangles[spriteIndex];
        }

        /// <summary>
        /// Looks up the numeric index of the specified sprite. This is useful when
        /// implementing animation by cycling through a series of related sprites.
        /// </summary>
        static int GetIndex(string spriteName)
        {
            int index;
            var spriteSheet = GetSpriteSheet(spriteName);
            
            if (!spriteSheet.SpriteNames.TryGetValue(spriteName, out index))
                throw new KeyNotFoundException("SpriteSheet does not contain a sprite named" + spriteName);
            
            return index;
        }
        
        #endregion

        #region Generic Methods

        public static Texture2D Texture(string textureName)
        {
            return (Texture2D) ContentHandler.Load<Texture2D>(textureName);
        }

        /// <summary>
        /// Get a SpriteSheet based on Texture Name passed
        /// </summary>
        /// <param name="textureName"></param>
        /// <returns></returns>
        public static SpriteSheet GetSpriteSheet(string textureName)
        {
            return _spriteSheets.Where(t => t.SpriteNames.ContainsKey(textureName)).FirstOrDefault();
        }

        /// <summary>
        /// Get a Texture based on Texture Name passed
        /// </summary>
        /// <param name="textureName"></param>
        /// <returns></returns>
        public static GameTexture GetGameTexture(string textureName)
        {
            return _textures.Where(t => t.Path == textureName).Select(t => t).FirstOrDefault();
        }

        #endregion

    }
}
