// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: ITyping.cs Version: 1.0 Last Edited: 9/18/2012
// ------------------------------------------------------------------------

using Graphics.GUI.Controls;
using Microsoft.Xna.Framework;

namespace Graphics.GUI.Interfaces
{
    public interface ITyping : IFont
    {
        #region Properties

        /// <summary>
        /// Gets and Sets if our Control is currently accepting input from the StringCreator
        /// </summary>
        bool IsTyping { get; set; }

        #endregion
    }


    /// <summary>
    /// Handles Drawing and Updating our Typing Interface
    /// </summary>
    public static class Typing
    {
        /// <summary>
        /// Updater our ITyping Interface in conjunction with the passed control
        /// </summary>
        public static void Update(object controlBase, GameTime gameTime)
        {
            var control = (ControlBase)controlBase;

        }
    }
}
