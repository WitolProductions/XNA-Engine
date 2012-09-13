// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: IEvents.cs Version: 1.0 Last Edited: 9/13/2012
// ------------------------------------------------------------------------

namespace Graphics.GUI.Interfaces
{
    /// <summary>
    /// Grants a control full access to responding to events
    /// </summary>
    public interface IEvents
    {
        #region Events

        /// <summary>
        /// Input Event Handler
        /// </summary>
        event ControlEvent OnInputChanged;
        
        /// <summary>
        /// Text was changed
        /// </summary>
        event ControlEvent OnTextChanged;

        #endregion
    }

    /// <summary>
    /// Control Event Delegate, handles passing information about an event that fires
    /// </summary>
    /// <param name="sender">Control event fired from</param>
    /// <param name="args">Information about even that fired</param>
    public delegate void ControlEvent(object sender, ControlEvent args);
}
