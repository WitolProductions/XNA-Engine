// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: EventHelper.cs Version: 1.0 Last Edited: 9/29/2012
// ------------------------------------------------------------------------

using System;
using System.Linq;
using Graphics.GUI.Controls;

namespace Graphics.GUI
{
    public static class EventHelper
    {        
        #region Delegates
        
        /// <summary>
        /// Control Event Delegate, handles passing information about an event that fires
        /// </summary>
        /// <param name="sender">Control event fired from</param>
        /// <param name="args">Information about even that fired</param>
        public delegate void ControlEvent(object sender, object args);
        
        #endregion

        #region Methods

        /// <summary>
        /// Fire off all handlers attached to an event using reflection
        /// </summary>
        /// <param name="control">Control the event belongs too</param>
        /// <param name="eventName">Events Name</param>
        /// <param name="eventArgs">Event Args to send during the events Invoking</param>
        public static void FireEvent(object control, string eventName, object eventArgs)
        {
            if (control.GetType().GetEvents().Where(f => f.Name == eventName).Count() > 0)
            {
                var fi = control.GetType().GetField(eventName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                if (fi == null)
                    throw new Exception("Could not get Field: " + eventName + " from inside " + ((ControlBase)control).Name);
                
                var delegates = (MulticastDelegate) fi.GetValue(control);
                //If Delegates returns null it means the event is currently unset
                if (delegates != null)
                    foreach (var dlg in delegates.GetInvocationList()) //Fire off all events attached to the passed Event name attached to the passed control
                        dlg.Method.Invoke(dlg.Target, new[] {control, eventArgs});
                
            }
        }

        /// <summary>
        /// Sets an event to a method passed
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="eventName">Name of Event to Set</param>
        /// <param name="classType">Type of the class where the Method exists in</param>
        /// <param name="methodName">Name of Method to Set Event to</param>
        public static void AddEvent(object control, string eventName, Type classType, string methodName)
        {
            //Only Try and add if Event and Method is found to exist
            if (control.GetType().GetEvents().Where(e => e.Name == eventName).Count() > 0)
                if (classType.GetMethods().Where(m => m.Name == methodName).Count() > 0)
                {
                    control.GetType().GetEvent(eventName).AddEventHandler(control, Delegate.CreateDelegate(typeof(EventHelper.ControlEvent), control, classType.GetMethod(methodName)));
                    return;
                }

            //Throw exception if we reach here because event or method doesn't exist
            var c = (ControlBase)control;
            throw new Exception("Reflection failed. Could not attach Method '" + methodName + "' to Event '" + eventName + "' in Control '" + c.Name + "'");
        }

        #endregion
    }
}
