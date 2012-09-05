﻿// -----------------------------------------------------------------------
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

using System;
using System.Collections.Generic;
using System.Linq;
using Content.ContentTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Content.ContentHolders
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
        static List<GameTexture2D> _textures = null;
        
        #endregion

        #region XNA Methods

        public static void Initialize()
        {
            _spriteSheets = new List<SpriteSheet>();
            _textures = new List<GameTexture2D>();
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

        #region Texture Methods

        /// <summary>
        /// Add the passed texture
        /// </summary>
        /// <param name="gameTexture"></param>
        public static void Add(GameTexture2D gameTexture)
        {
            Remove(gameTexture);

            //Add our Texture
            _textures.Add(gameTexture);
        }

        /// <summary>
        /// Remove our Texture
        /// </summary>
        /// <param name="gameTexture"></param>
        public static void Remove(GameTexture2D gameTexture)
        {
            //Only remove if it exists
            if (_textures.Contains(gameTexture))
                _textures.Remove(gameTexture);
        }

        /// <summary>
        /// Get a Texture based on Texture Name passed
        /// </summary>
        /// <param name="textureName"></param>
        /// <returns></returns>
        public static GameTexture2D GetGameTexture(string textureName)
        {
            return _textures.Where(t => t.Path == textureName).Select(t => t).FirstOrDefault();
        }

        #endregion

        #region Sprite Sheet Methods

        /// <summary>
        /// Add the passed sprite sheet
        /// </summary>
        /// <param name="spriteSheet"></param>
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
        /// Looks up the numeric index of the specified sprite. This is useful when
        /// implementing animation by cycling through a series of related sprites.
        /// </summary>
        static int GetIndex(string spriteName)
        {
            int index;
            var spriteSheet = GetSpriteSheet(spriteName);
            
            if (!spriteSheet.SpriteNames.TryGetValue(spriteName, out index))
                throw new KeyNotFoundException("SpriteSheet does not contain a sprite named " + spriteName);
            
            return index;
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

        #endregion

        #region Generic Methods

        /// <summary>
        /// Looks up the location of the specified sprite within the big texture.
        /// </summary>
        public static Rectangle SourceRectangle<T>(string textureName)
        {
            switch (typeof(T).Name)
            {
                case "Texture2D":
                    {
                        if (ContentHandler.Load<Texture2D>(textureName) != null)
                            return GetGameTexture(textureName).Texture.Bounds;

                        throw new Exception("Cannot find source: " + textureName + " of type " + typeof(T).Name); 
                    }
                case "SpriteSheet":
                    {
                        var spriteIndex = GetIndex(textureName);
                        var spriteSheet = GetSpriteSheet(textureName);

                        return spriteSheet.SpriteRectangles[spriteIndex];
                    }
            }

            return new Rectangle();
        }

        /// <summary>
        /// Returns a texture based on name sent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textureName"></param>
        /// <returns></returns>
        public static Texture2D Texture<T>(string textureName)
        {
            return ContentHandler.Load<T>(textureName) as Texture2D;
        }

        public static void Unload()
        {
            foreach(var t in _textures)
                t.Dispose();

            foreach(var ss in _spriteSheets)
                ss.Dispose();
            
            _textures.Clear();
            _spriteSheets.Clear();
        }
        
        #endregion

    }
}
