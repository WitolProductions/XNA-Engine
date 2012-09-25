// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: NativeMethods.cs Version: 1.0 Last Edited: 9/25/2012
// ------------------------------------------------------------------------

#if WINDOWS
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Content.Misc
{
    public static class NativeMethods
    {
        #region Create Icon from CUR or ANI file

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr LoadImage(IntPtr instance, string fileName, uint type, int width, int height, uint load);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        /// <summary>
        /// Loads a .Cur or .Ani file as a Cursor
        /// </summary>
        /// <param name="fileName">Path to file</param>
        /// <returns></returns>
        internal static IntPtr LoadCursor(string fileName)
        {
            return LoadImage(IntPtr.Zero, fileName, 2, 0, 0, 0x0010);
        }

        #endregion

        #region Create Icon from Bitmap

        [DllImport("user32.dll")]
        static extern IntPtr CreateIconIndirect(ref IconInfo icon);
        
        /// <summary>
        /// Structure used to store Icon Information
        /// </summary>
        struct IconInfo
        {
            public bool FIcon;
            public int XHotspot;
            public int YHotspot;
            public IntPtr HbmMask;
            public IntPtr HbmColor;
        }

        /// <summary>
        /// Loads our Cursor from an Image
        /// </summary>
        /// <param name="path">Path of Image</param>
        /// <param name="xHotSpot">X HotSpot Location</param>
        /// <param name="yHotSpot">Y HotSpot Location</param>
        /// <returns></returns>
        internal static IntPtr LoadCursor(string path, int xHotSpot, int yHotSpot)
        {
            var image = CreateNonIndexedImage(path);
            var bitmapsIntPtr = new Bitmap(image).GetHicon(); //Get Bitmaps handle
            var tmp = new IconInfo();
            GetIconInfo(bitmapsIntPtr, ref tmp); //Get Icon info and pass it back
            tmp.XHotspot = xHotSpot; //Create HotSpots for our Icon
            tmp.YHotspot = yHotSpot;
            tmp.FIcon = false; 
            return CreateIconIndirect(ref tmp); //Return the handle for our Icon
        }

        /// <summary>
        /// Returns an image that is not bound by a file handle
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static Image CreateNonIndexedImage(string path)
        {
            using (var sourceImage = Image.FromFile(path))
            {
                //Get a copy of our image info
                var targetImage = new Bitmap(sourceImage.Width, sourceImage.Height,
                  PixelFormat.Format32bppArgb);
                //Copy our image
                using (var canvas = Graphics.FromImage(targetImage))
                    canvas.DrawImageUnscaled(sourceImage, 0, 0);
                //Return it
                return targetImage;
            }
        } 
        
        #endregion
    }
}     
#endif