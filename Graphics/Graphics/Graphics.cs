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

using Microsoft.Xna.Framework;

namespace Graphics
{
    public class Graphics
    {
        public static void Clear(Color color)
        {
            Engine.Engine.Game.GraphicsDevice.Clear(color);
        }
    }
}
