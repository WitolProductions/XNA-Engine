// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: GameTexture2D.cs Version: 1.0 Last Edited: 8/31/2012
// ------------------------------------------------------------------------

using Microsoft.Xna.Framework.Graphics;

namespace Content.ContentTypes
{
    public class GameTexture2D : IDisposableObject
    {
        /// <summary>
        /// Our Texture
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Path to our Texture
        /// </summary>
        public string Path { get; set; }

        #region Interface

        public bool Loaded { get; set; }

        public double UnloadTimer { get; set; }

        public void Dispose()
        {
            UnloadTimer = 0;
            Loaded = false;

            if (Texture != null) //If texture is not already null
                if (!Texture.IsDisposed) //If texture is not already disposed dispose of it
                    Texture.Dispose();
        }

        #endregion
    }
}
