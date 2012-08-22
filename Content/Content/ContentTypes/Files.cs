// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Files.cs Version: 1.0 Last Edited: 8/21/2012
// ------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace Content.ContentTypes
{
    public static class Files
    {
        [ContentSerializer]
        public static Dictionary<string, string> FilesList = null;
    }
}
