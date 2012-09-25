// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Cursor.cs Version: 1.0 Last Edited: 9/22/2012
// ------------------------------------------------------------------------

using System.IO;
using Microsoft.Xna.Framework.Content;

#if WINDOWS
using System;
using System.Windows.Forms;
using Content.Misc;
#endif

namespace Content.ContentTypes
{
    public class GameCursor
    {
        #region Fields

#if WINDOWS
        /// <summary>
        /// Cursor Object 
        /// </summary>
        [ContentSerializerIgnore]
        Cursor cursor = null;
#endif

        #endregion

        #region Properties

        /// <summary>
        /// Gets and Sets if our Cursor has been loaded
        /// </summary>
        [ContentSerializerIgnore]
        public bool Loaded { get; set; }

        /// <summary>
        /// Gets and Sets our Cursors Path
        /// </summary>
        [ContentSerializerIgnore]
        public string Path { get; set; }

#if WINDOWS
        /// <summary>
        /// Gets our Cursor
        /// </summary>
        [ContentSerializerIgnore]
        public Cursor Cursor { get { return cursor; } set { cursor = value; } }

#endif
        #endregion
    }

#if WINDOWS

    public class GameCursorReader : ContentTypeReader<GameCursor>
    {
        #region Overrides
        
        /// <summary>
        /// Read in our Cursor Data
        /// </summary>
        /// <param name="input"></param>
        /// <param name="existingInstance"></param>
        /// <returns></returns>
        protected override GameCursor Read(ContentReader input, GameCursor existingInstance)
        {
#if WINDOWS
            if (existingInstance == null)
            {
                var extension = input.ReadString();
                var count = input.ReadInt32();
                var data = input.ReadBytes(count);

                var path = Path.GetTempFileName();
                File.WriteAllBytes(path, data);
                
                var handle = IntPtr.Zero;
                switch (extension)
                {
                    case ".png":
                    case ".jpg":
                        handle = NativeMethods.LoadCursor(path, 0, 0);
                        break;
                    case ".ani":
                    case ".cur":
                        handle = NativeMethods.LoadCursor(path);
                        break;
                }

                File.Delete(path);
                
                if (handle == IntPtr.Zero)
                    throw new Exception("Cannot create handle for Icon: " + input.AssetName);
                
                var gameCursor = new GameCursor {Cursor = new Cursor(handle), Loaded = true};


                return gameCursor;
            }
#endif
            return existingInstance;
        }

        #endregion
    }

#endif
}
