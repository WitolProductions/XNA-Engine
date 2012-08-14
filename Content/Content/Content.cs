using System;
using System.IO;
using Content.Types;
using Microsoft.Xna.Framework;

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
        }

        #endregion

        #region XNA Methods

        public override void Initialize()
        {
            //Initialize our Textures class
            Textures.Initialize();
            
            //Initialize our Root Directory
            InitializeFolder(Content.RootDirectory);

            //Now initialize all folders in our Root Directory
            foreach (var d in Directory.GetDirectories(Content.RootDirectory))
                InitializeFolder(d);

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
        /// <param name="folderName"></param>
        static void InitializeFolder(string folderName)
        {
            if (!Directory.Exists(folderName)) return; //Return if Directory passed does not exist

            foreach(var f in Directory.GetFiles(folderName))
            {
                var fileInfo = new FileInfo(f);

                if (!fileInfo.Exists) continue; //No idea why this should happen but incase it does...
                if (fileInfo.Extension != ".xnb") continue; //Found odd file we don't need
                SpriteSheet spriteSheet = null;

                var filePath = folderName.Replace(Content.RootDirectory + "\\", ""); //Remove "Content\\"
                filePath = filePath.Replace(Content.RootDirectory, ""); //Remove only "Content"
                if (filePath != "")
                    filePath += "\\";
                filePath += Path.GetFileNameWithoutExtension(fileInfo.FullName); //Add the files name to create a real path

                try
                {
                    spriteSheet = Content.Load<SpriteSheet>(filePath);
                }
                catch
                { } //Unless there is another way to detect what kind of XNB this file belongs to then we need to ingore it when it errros
                        
                //Ignore files that don't load into our variable
                if (spriteSheet == null) continue;
                spriteSheet.Path = filePath;
                spriteSheet.Loaded = true;
                spriteSheet.UnloadTimer = Constants.UnloadTimer;
                Textures.Add(spriteSheet);
            }
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
