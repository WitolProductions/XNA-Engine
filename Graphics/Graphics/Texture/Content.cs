using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Graphics.SpriteSheetPipeline;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics.Texture
{
    public class GraphicsHandler : DrawableGameComponent
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
        public GraphicsHandler(Game game, IServiceProvider services, string content) : base(game)
        {
            //Setup our base variables for this class
            Content = new CustomContentManager(services, content);
        }

        #endregion

        #region XNA Methods

        public override void Initialize()
        {
            //Initialize our Textures class
            Textures.Initialize();
            
            //Initialize our Content
            InitializeContent("Content");
        }  
        

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Textures.Update(gameTime);
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Initialize all XNB files that are SpriteSheet files inside the directory passed
        /// </summary>
        /// <param name="fileName"></param>
        static void InitializeContent(string fileName)
        {
            if (!File.Exists(fileName)) return; //Return if Directory passed does not exist


            //SpriteSheet ss = null;
            
            //foreach(var content in ss.SpriteNames)
            //{
            //    SpriteSheet spriteSheet = null;

            //    try
            //    {
            //        spriteSheet = Content.Load<SpriteSheet>(content.Key);
            //    }
            //    catch
            //    { } //Unless there is another way to detect what kind of XNB this file belongs to then we need to ingore it when it errros
                        
            //    //Ignore files that don't load into our variable
            //    if (spriteSheet == null) continue;
            //    spriteSheet.Path = content.Key;
            //    spriteSheet.Loaded = true;
            //    spriteSheet.UnloadTimer = Constants.UnloadTimer;
            //    Textures.Add(spriteSheet);
            //}
        }

        #endregion

        #region Public Methods

        static public object Load<T>(string spriteName)
        {
            switch (typeof(T).Name)
            {
                case "Texture2D":
                    //Get our Sprite Sheet
                    var spriteSheet = Textures.GetSpriteSheet(spriteName);
                    //Reload our SpriteSheet and set our Texture to variable to return it
                    spriteSheet.Texture = Content.ReloadObject<SpriteSheet>(spriteSheet.Path).Texture;
                    //Set that our texture is now loaded in and set an unload timer
                    spriteSheet.UnloadTimer = Constants.UnloadTimer;;
                    spriteSheet.Loaded = true;
                    //Finally return our Texture
                    return spriteSheet.Texture;
            }


            return null;
        }

        #endregion

    }
}
