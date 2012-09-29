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

using System.Linq;
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
        event EventHelper.ControlEvent Clicked;

        /// <summary>
        /// Occurs when a control is entered either with the mouse o touch
        /// </summary>
        event EventHelper.ControlEvent Enter;
        
        /// <summary>
        /// Occurs when a control is left either with the mouse or touch
        /// </summary>
        event EventHelper.ControlEvent Leave;

        /// <summary>
        /// Occurs when a control is held down via mouse or touch
        /// </summary>
        event EventHelper.ControlEvent Down;

        /// <summary>
        /// Occurs when a control enters into a disabled state
        /// </summary>
        event EventHelper.ControlEvent Disabled;
        
        /// <summary>
        /// The attched Control was stopped on
        /// </summary>
        event EventHelper.ControlEvent TabStoped;

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
        void OnTabStoped(object sender, object eventArgs);

        #endregion
    }

    
    
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
            EventHelper.AddEvent(controlBase, "Clicked", typeof(IEvents), "OnClicked");
            EventHelper.AddEvent(controlBase, "Leave", typeof(IEvents), "OnLeave");
            EventHelper.AddEvent(controlBase, "Enter", typeof(IEvents), "OnEnter");
            EventHelper.AddEvent(controlBase, "Down", typeof(IEvents), "OnDown");
            EventHelper.AddEvent(controlBase, "Disabled", typeof(IEvents), "OnDisabled");
            EventHelper.AddEvent(controlBase, "TabStoped", typeof(IEvents), "OnTabStoped");
        }

        /// <summary>
        /// Update our IEvent Interface in conjunction with the passed control
        /// </summary>
        public static void Update(object controlBase, GameTime gameTime)
        {
            var control = (ControlBase)controlBase;
            var bounds = control.Bounds;

            //If the Interface Border is detected add border width into our calculations
            if (control.GetType().GetInterfaces().Where(e => e.Name == "IBorder").Count() > 0)
            {
                var borderWidth = (int)ReflectionHelper.GetPropertyValue(control, "BorderWidth");
                bounds = new Rectangle((int)control.Location.X + borderWidth, (int)control.Location.Y + borderWidth, (int)control.Size.X - borderWidth, (int)control.Size.Y - borderWidth);
            }


            #region Handle Switching States

            if (!control.Enabled)
            {//Set to Disabled if needed
                if (control.State != Enumerations.ControlState.Disabled)
                {
                    control.State = Enumerations.ControlState.Disabled;
                    EventHelper.FireEvent(controlBase, "Disabled", null);
                }
            }
            else if (InputHandler.Clicked(bounds))
            {//Fire click event if we complete the click
                EventHelper.FireEvent(control, "Clicked", null);
            }
            else if (InputHandler.Down(bounds))
            {//Simulate a click if we are holding it down
                if (control.State != Enumerations.ControlState.Down)
                {
                    control.State = Enumerations.ControlState.Down;
                    EventHelper.FireEvent(controlBase, "Down", null);
                }
            }
            else if (InputHandler.Hover(bounds))
            {//Else maybe we are just hovering
                if (control.State != Enumerations.ControlState.Hover)
                {
                    control.State = Enumerations.ControlState.Hover;
                    EventHelper.FireEvent(controlBase, "Enter", null);
                }
            }
            else
            {//Else we are doing nothing
                if (control.State != Enumerations.ControlState.Normal)
                {
                    control.State = Enumerations.ControlState.Normal;
                    EventHelper.FireEvent(controlBase, "Leave", null);
                }
            }

            #endregion
        }
    }
}
