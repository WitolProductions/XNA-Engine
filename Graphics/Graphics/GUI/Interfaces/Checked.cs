// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Checked.cs Version: 1.0 Last Edited: 9/18/2012
// ------------------------------------------------------------------------

using Graphics.GUI.Controls;
using Microsoft.Xna.Framework;

namespace Graphics.GUI.Interfaces
{
    /// <summary>
    /// IChecked interface provides an option for determining if a control is currently checked or not
    /// </summary>
    public interface IChecked
    {
        #region Properties

        bool Checked { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Check State Changed
        /// </summary>
        event ControlEvent CheckStateChanged;
        
        #endregion

        #region Abstract Events

        void OnCheckStateChanged(object sender, object eventArgs);

        #endregion
    }

    /// <summary>
    /// Handles Updating our IChecked Interface
    /// </summary>
    public static class Checked
    {
        /// <summary>
        /// Initializes our Control with base information needed
        /// </summary>
        /// <param name="controlBase"></param>
        public static void Initialize(object controlBase)
        {
        }

        /// <summary>
        /// Update our IChecked Interface in conjunction with the passed control
        /// </summary>
        public static void Update(object controlBase, GameTime gameTime)
        {
            var control = (ControlBase)controlBase;

        }
    }
}
