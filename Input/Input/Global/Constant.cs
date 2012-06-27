// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Constant.cs Version: 1.0 Last Edited: 6/26/2012
// ------------------------------------------------------------------------

namespace Input.Global
{
    static class Constant
    {       
        /// <summary>
        /// How quickly a key trys to repeat when its held down
        /// </summary>
        public const int KeyboardRepeatStandard = 170;
        
        /// <summary>
        /// How quickly one must click again to count as a second click
        /// </summary>
        public const int MouseDoubleClickStandard = 500;
        
        /// <summary>
        /// Our defualt Control Scheme files name
        /// </summary>
        public const string ControlScheme = "ControlScheme.xml";
    }
}
