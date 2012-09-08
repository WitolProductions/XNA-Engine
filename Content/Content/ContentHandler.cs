//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: ContentHandler.cs Version: 1.0 Last Edited: 8/21/2012
// ------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Content.ContentHolders;
using Content.ContentTypes;
using Content.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Content
{
    public class ContentHandler : DrawableGameComponent
    {
        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// Content Manager to manage all our Content
        /// </summary>
        public static CustomContentManager Content { get; set; }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public ContentHandler(Game game, IServiceProvider services, string content) : base(game)
        {
            //Setup our base variables for this class
            Content = new CustomContentManager(services, content);
            Textures.Initialize();
        }

        #endregion

        #region XNA Methods

        public override void Initialize()
        {
            //Attempt to get the list of content our game has
            Files.FilesList = Content.Load<Dictionary<string, string>>(Content.RootDirectory);

            //return if our File List is null
            if (Files.FilesList == null) return;

            foreach(var f in Files.FilesList)
            {
                switch(f.Value)
                {
                    case "Texture2D":
                        {
                            var content = new GameTexture2D {Loaded = false, Path = f.Key};
                            Textures.Add(content);
                            Unload<Texture2D>(content);
                        } break;
                    case "SpriteSheet":
                        {
                            var content = Content.Load<SpriteSheet>(f.Key);
                            if (content != null)
                            {
                                content.Path = f.Key;
                                content.Loaded = true;
                                content.UnloadTimer = Constants.UnloadTimer;
                                Textures.Add(content);
                                Unload<SpriteSheet>(content);
                            }
                        } break;
                    case "SpriteFont":
                        {
                            var font = new Font {Loaded = false, Path = f.Key};
                            Textures.Add(font);
                            var loadedFont = Load<SpriteFont>(font.Path) as SpriteFont;
                            if (loadedFont != null)
                            {
                                font.Loaded = true;
                                font.SpriteFont = loadedFont;
                            }
                        } break;
                }
            }
        }  
        

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            Textures.Unload();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Textures.Update(gameTime);
        }

        #endregion

        #region Private Methods
        
        #endregion

        #region Public Methods

        static public object Load<T>(string objectName)
        {
            switch (typeof(T).Name)
            {
                case "Texture2D":
                    {
                        #region Texture Check

                        //Attempt to get our texture
                        var texture = Textures.GetGameTexture(objectName);

                        //If the texture returned was not null...
                        if (texture != null)
                        {
                            //If texture is already loaded simply refresh the UnloadTimer and return it
                            if (texture.Loaded)
                            {
                                texture.UnloadTimer = Constants.UnloadTimer;
                                return texture.Texture;
                            }

                            //Else reload our object and setup its dispose timer and return the texture
                            texture.Texture = Content.ReloadObject<Texture2D>(texture.Path);
                            texture.Loaded = true;
                            texture.UnloadTimer = Constants.UnloadTimer;

                            return texture.Texture;
                        }

                        #endregion


                        return null;
                    }
                case "SpriteSheet":
                    {
                        #region Sprite Sheet Check

                        //Get our Sprite Sheet
                        var spriteSheet = Textures.GetSpriteSheet(objectName);

                        if (spriteSheet != null)
                        {
                            //If Sprite Sheet is already loaded simply refresh the UnloadTimer and return it
                            if (spriteSheet.Loaded)
                            {
                                spriteSheet.UnloadTimer = Constants.UnloadTimer;
                                return spriteSheet.Texture;
                            }

                            //Else reload our object and setup its dispose timer and return the texture
                            spriteSheet.Texture = Content.ReloadObject<SpriteSheet>(spriteSheet.Path).Texture;
                            spriteSheet.Loaded = true;
                            spriteSheet.UnloadTimer = Constants.UnloadTimer;

                            return spriteSheet.Texture;
                        }

                        #endregion

                        return null;
                    }
                case "SpriteFont":
                    {
                        #region SpriteFont Check

                        //Get our Font
                        var font = Textures.GetFont(objectName);

                        if (font != null)
                        {
                            //If font is already loaded in than simplu return it
                            if (font.Loaded)
                                return font.SpriteFont;
                            

                            //Else load our font in
                            font.SpriteFont = Content.Load<SpriteFont>(font.Path);
                            return font.SpriteFont;
                        }
                        
                        #endregion

                        return null;
                    }
            }


            return null;
        }

        public static void Unload<T>(object content)
        {
            switch (typeof (T).Name)
            {
                case "Texture2D":
                    {
                        ((GameTexture2D) content).Dispose();
                    } break;
                case "SpriteSheet":
                    {
                        ((SpriteSheet) content).Dispose();
                    } break;
                case "SpriteFont":
                    {
                        //No need to dispose this, not even sure if its possible
                    } break;
            }
        }

        #endregion
    }
}

