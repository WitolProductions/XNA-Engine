// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: IPanel.cs Version: 1.0 Last Edited: 9/18/2012
// ------------------------------------------------------------------------

using System.Collections.Generic;
using Graphics.GUI.Controls;
using Microsoft.Xna.Framework;

namespace Graphics.GUI.Interfaces
{
    public interface IPanel : IBackground, IBorder
    {
        #region Properties
        
        /// <summary>
        /// Gets and Sets our Controls
        /// </summary>
        List<ControlBase> Controls { get; set; }

        #endregion

        #region Events
        
        /// <summary>
        /// Control was added to the control
        /// </summary>
        event EventHelper.ControlEvent ControlAdded;

        /// <summary>
        /// Control was removed from the control
        /// </summary>
        event EventHelper.ControlEvent ControlRemoved;
        
        #endregion

        #region Abstract Events

        /// <summary>
        /// On Control Added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void OnControlAdded(object sender, object eventArgs);

        /// <summary>
        /// On Control Removed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void OnControlRemoved(object sender, object eventArgs);

        #endregion
    }

    /// <summary>
    /// Handles Drawing and Updating our Panel Interface
    /// </summary>
    public static class Panel
    {
        /// <summary>
        /// Initializes our Control with base information needed
        /// </summary>
        /// <param name="controlBase"></param>
        public static void Initialize(object controlBase)
        {
        }

        /// <summary>
        /// Update our IPanel Interface in conjunction with the passed control
        /// </summary>
        public static void Update(object controlBase, GameTime gameTime)
        {
            var control = (ControlBase)controlBase;

        }

        /// <summary>
        /// Draw our IPanel Interface in conjunction with the passed control
        /// </summary>
        public static void Draw(object controlBase, GameTime gameTime)
        {
            var control = (ControlBase)controlBase;

        }
    }
}
