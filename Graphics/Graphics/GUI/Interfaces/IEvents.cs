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

using Graphics.GUI.Controls;
using Graphics.Misc;
using Input;
using Microsoft.Xna.Framework;

namespace Graphics.GUI.Interfaces
{
    /// <summary>
    /// Grants a control access to responding to events that appear in all controls
    /// </summary>
    public interface IEvents
    {
        #region Events
        
        /// <summary>
        /// Occurs when a control is clicked via mouse or touch
        /// </summary>
        event ControlEvent Clicked;

        /// <summary>
        /// Occurs when a control is entered either with the mouse o touch
        /// </summary>
        event ControlEvent Enter;
        
        /// <summary>
        /// Occurs when a control is left either with the mouse or touch
        /// </summary>
        event ControlEvent Leave;

        /// <summary>
        /// Occurs when a control is held down via mouse or touch
        /// </summary>
        event ControlEvent Down;

        /// <summary>
        /// Occurs when a control enters into a disabled state
        /// </summary>
        event ControlEvent Disabled;
        
        /// <summary>
        /// The attched Control was stopped on
        /// </summary>
        event ControlEvent TabStop;

        #endregion

        #region Abstract Events

        /// <summary>
        /// Fires On Disabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void OnDisabled(object sender, object eventArgs);

        /// <summary>
        /// Fires On Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void OnLeave(object sender, object eventArgs);

        /// <summary>
        /// Fires On Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void OnEnter(object sender, object eventArgs);

        /// <summary>
        /// Fires On Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void OnClicked(object sender, object eventArgs);

        /// <summary>
        /// Fires On Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void OnDown(object sender, object eventArgs);

        /// <summary>
        /// On Tab Stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void OnTabStop(object sender, object eventArgs);

        #endregion
    }

    /// <summary>
    /// Control Event Delegate, handles passing information about an event that fires
    /// </summary>
    /// <param name="sender">Control event fired from</param>
    /// <param name="args">Information about even that fired</param>
    public delegate void ControlEvent(object sender, object args);

    /// <summary>
    /// Handles Updating our Event Interface
    /// </summary>
    public static class Events
    {
        /// <summary>
        /// Initializes our Control with base information needed
        /// </summary>
        /// <param name="controlBase"></param>
        public static void Initialize(object controlBase)
        {
            GuiHandler.AddEvent(controlBase, "Clicked", "OnClicked");
            GuiHandler.AddEvent(controlBase, "Leave", "OnLeave");
            GuiHandler.AddEvent(controlBase, "Enter", "OnEnter");
            GuiHandler.AddEvent(controlBase, "Down", "OnDown");
            GuiHandler.AddEvent(controlBase, "Disabled", "OnDisabled");
            GuiHandler.AddEvent(controlBase, "TabStop", "OnTabStop");
        }

        /// <summary>
        /// Update our IEvent Interface in conjunction with the passed control
        /// </summary>
        public static void Update(object controlBase, GameTime gameTime)
        {
            var control = (ControlBase)controlBase;

            #region Handle Switching States

            if (!control.Enabled)
            {//Set to Disabled if needed
                if (control.State != Enumerationcs.ControlState.Disabled)
                {
                    control.State = Enumerationcs.ControlState.Disabled;
                    GuiHandler.FireEvent(controlBase, "OnDisabled", null);
                }
            }
            else if (InputHandler.Clicked(control.Bounds))
            {//Fire click event if we complete the click
                GuiHandler.FireEvent(controlBase, "OnClicked", null);
            }
            else if (InputHandler.Down(control.Bounds))
            {//Simulate a click if we are holding it down
                if (control.State != Enumerationcs.ControlState.Down)
                {
                    control.State = Enumerationcs.ControlState.Down;
                    GuiHandler.FireEvent(controlBase, "OnDown", null);
                }
            }
            else if (InputHandler.Hover(control.Bounds))
            {//Else maybe we are just hovering
                if (control.State != Enumerationcs.ControlState.Hover)
                {
                    control.State = Enumerationcs.ControlState.Hover;
                    GuiHandler.FireEvent(controlBase, "OnEnter", null);
                }
            }
            else
            {//Else we are doing nothing
                if (control.State != Enumerationcs.ControlState.Normal)
                {
                    control.State = Enumerationcs.ControlState.Normal;
                    GuiHandler.FireEvent(controlBase, "OnLeave", null);
                }
            }

            #endregion
        }
    }
}
