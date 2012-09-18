// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: IIndex.cs Version: 1.0 Last Edited: 9/18/2012
// ------------------------------------------------------------------------

using Graphics.GUI.Controls;
using Microsoft.Xna.Framework;

namespace Graphics.GUI.Interfaces
{
    /// <summary>
    /// IIndex provides controls with the ability to have a Selected Index
    /// </summary>
    public interface IIndex
    {
        #region Properties

        /// <summary>
        /// Gets and Sets our Selected Index
        /// </summary>
        int SelectedIndex { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Selected Index Changed
        /// </summary>
        event ControlEvent SelectedIndexChanged;

        #endregion

        #region Abstract Events

        void OnSelectedIndexChanged(object sender, object eventArgs);

        #endregion
    }

    /// <summary>
    /// Handles Updating our IIndex Interface
    /// </summary>
    public static class Index
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
