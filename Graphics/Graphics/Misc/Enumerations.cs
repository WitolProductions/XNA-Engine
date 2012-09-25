// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Enumerations.cs Version: 1.0 Last Edited: 9/13/2012
// ------------------------------------------------------------------------

namespace Graphics.Misc
{
    public static class Enumerations
    {
        public enum ControlState
        {
            Normal = 0,
            Hover = 1,
            Clicked = 2,
            Disabled = 3,
            Down = 4
        }

        public enum ContentAlignment
        {
            TopLeft = 0,
            TopCenter = 1,
            TopRight = 2,
            MiddleLeft = 3, //Defualt
            MiddleCenter = 4, 
            MiddleRight = 5,
            BottomLeft = 6,
            BottomCenter = 7,
            BottomRight =8
        }
    }
}
