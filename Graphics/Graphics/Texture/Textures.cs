using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphics.SpriteSheetPipeline;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics.Texture
{
    public static class Textures
    {
        #region Fields

        // List of our SpriteSheets
        static List<SpriteSheet> _spriteSheets = null;
        
        #endregion

        #region XNA Methods

        public static void Initialize()
        {
            _spriteSheets = new List<SpriteSheet>();
        }

        public static void Update(GameTime gameTime)
        {
            foreach(var spriteSheet in _spriteSheets)
            {
                if (!spriteSheet.Loaded) continue;
                spriteSheet.UnloadTimer = (int) (spriteSheet.UnloadTimer - gameTime.ElapsedGameTime.TotalMilliseconds);

                //Continue if we do not need to unload this texture
                if (spriteSheet.UnloadTimer > 0) continue;
                spriteSheet.Loaded = false;
                Unload<SpriteSheet>(spriteSheet);
            }
        }

        public static void UnloadContent()
        {
            
        }

        #endregion

        #region Methods
        
        static void Unload<T>(SpriteSheet spriteSheet)
        {
            switch (typeof(T).Name)
            {
                case "Texture2D":
                    {
                        //Dispose of our Texture
                        GraphicsHandler.Content.DisposeObject(spriteSheet.Path);
                        spriteSheet.SpriteRectangles = null;
                    } break;
            }
        }

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
        public static void Remove(SpriteSheet spriteSheet)
        {
            //Remove our sheet if it exists
            if (_spriteSheets.Contains(spriteSheet))
                _spriteSheets.Remove(spriteSheet);
        }
        
        /// <summary>
        /// Get Texture based on name sent
        /// </summary>
        /// <param name="spriteName"></param>
        /// <returns></returns>
        public static Texture2D Texture(string spriteName)
        {
            var sprite = GetSpriteSheet(spriteName);
            
            return sprite.Texture;
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
        public static int GetIndex(string spriteName)
        {
            int index;
            var spriteSheet = GetSpriteSheet(spriteName);
            
            if (!spriteSheet.SpriteNames.TryGetValue(spriteName, out index))
                throw new KeyNotFoundException("SpriteSheet does not contain a sprite named" + spriteName);
            
            return index;
        }

        /// <summary>
        /// Looks up our passed texture name in our SpriteSheets
        /// </summary>
        /// <param name="spriteName"></param>
        public static SpriteSheet GetSpriteSheet(string spriteName)
        {
            //If our Sprite Sheet has the texture in it we found the sprite sheet
            foreach (var t in _spriteSheets.Where(t => t.SpriteNames.ContainsKey(spriteName)))
                return t;
            
            throw new KeyNotFoundException("SpriteSheet could not be found with texture " + spriteName);
        }

        #endregion

    }
}
