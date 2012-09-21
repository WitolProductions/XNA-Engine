// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: GuiHandler.cs Version: 1.0 Last Edited: 9/18/2012
// ------------------------------------------------------------------------

using System;
using System.Linq;
using Graphics.GUI.Controls;
using Graphics.GUI.Interfaces;
using Microsoft.Xna.Framework;

namespace Graphics.GUI
{
    public static class GuiHandler
    {
        #region Initialize

        /// <summary>
        /// Initializes the passed control
        /// </summary>
        /// <param name="control">Control</param>
        public static void Initialize(ControlBase control)
        {
            foreach (var i in control.GetType().GetInterfaces())
            {
                switch (i.Name)
                {
                    case "IEvents": Events.Initialize(control); break;
                    case "IText": Text.Initialize(control); break;
                    case "IIndex": Index.Initialize(control); break;
                    case "IChecked": Checked.Initialize(control); break;
                    case "IBorder": Border.Initialize(control); break;
                    case "IBackground": Background.Initialize(control); break;
                    case "IPanel": Panel.Initialize(control); break;
                    case "IPicture": Picture.Initialize(control); break;
                    case "IFont": Font.Initialize(control); break;
                    default: break; //If anything else simply break because its not needed
                }
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the passed control
        /// </summary>
        /// <param name="control"></param>
        /// <param name="gameTime"></param>
        public static void Update(ControlBase control, GameTime gameTime)
        {
            foreach (var i in control.GetType().GetInterfaces())
            {
                switch (i.Name)
                {
                    case "IEvents": Events.Update(control, gameTime); break;
                    case "IText": Text.Update(control, gameTime); break;
                    case "IIndex": Index.Update(control, gameTime); break;
                    case "IChecked": Checked.Update(control, gameTime); break;
                    case "IFont": Font.Update(control, gameTime); break;
                    case "ITyping": Typing.Update(control, gameTime); break;
                    case "IBorder": Border.Update(control, gameTime); break;
                    case "IBackground": Background.Update(control, gameTime); break;
                    case "IPanel": Panel.Update(control, gameTime); break;
                    case "IPicture": Picture.Update(control, gameTime); break;
                    default: break; //If anything else simply break because its not needed
                }
            }
        }

        #endregion

        #region Draw

        /// <summary>
        /// Draws the passed control
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="gameTime">GameTime</param>
        public static void Draw(ControlBase control, GameTime gameTime)
        {
            GraphicsHandler.Begin();

            //Get a list of Interfaces available and draw those that need to be drawn in the correct order
            var list = control.GetType().GetInterfaces();
            
            //Drawn First
            if (list.Contains(typeof(IBackground)))
                Background.Draw(control, gameTime);

            //Drawn Second
            if (list.Contains(typeof(IBorder)))
                Border.Draw(control, gameTime);

            //Drawn Third
            if (list.Contains(typeof(IPicture)))
                Picture.Draw(control, gameTime);

            //Drawn Last
            if (list.Contains(typeof(IText)))
                Text.Draw(control, gameTime);
           
            GraphicsHandler.End();
        }

        #endregion

        #region Easy Reflection Methods

        /// <summary>
        /// Gets a Property Value from a passed object based on name sent
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="name">Name of Property</param>
        /// <returns>Object</returns>
        public static object GetPropertyValue(object control, string name)
        {
            //Check if our Property exists if it doesn't throw exception
            if (control.GetType().GetProperties().Where(p => p.Name == name).Count() == 0)
            {
                var c = (ControlBase) control;
                throw new Exception("Reflection failed. Could not find Property '" + name + "' in Control '" + c.Name + "'");
            }
            //Return our Value
            return control.GetType().GetProperty(name).GetValue(control, null);
        }

        /// <summary>
        /// Sets a Property Value based on information passed
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="name">Name of Property</param>
        /// <param name="data">Data being set</param>
        public static void SetPropertyValue(object control, string name, object data)
        {
            //Check our property exists before setting 
            if (control.GetType().GetProperties().Where(p => p.Name == name).Count() > 0)
                control.GetType().GetProperty(name).SetValue(control, data, null);
            else
            {//Throw exception because Property doesn't exist
                var c = (ControlBase)control;
                throw new Exception("Reflection failed. Could not set Property '" + name + "' in Control '" + c.Name + "'");
            }
        }

        /// <summary>
        /// Fires the event passed in the control passed
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="name">Name of Event to fire</param>
        /// <param name="eventArgs">Event args to pass to event, usually can be null</param>
        public static void FireEvent(object control, string name, object eventArgs)
        {
            //Check our Event exists before invoking it
            if (control.GetType().GetMethods().Where(m => m.Name == name).Count() > 0)
                control.GetType().GetMethod(name).Invoke(control, new[] { control, eventArgs });
            else
            {//Throw exception because Event doesn't exist
                var c = (ControlBase)control;
                throw new Exception("Reflection failed. Could not find Event '" + name + "' in Control '" + c.Name + "'");
            }

        }

        /// <summary>
        /// Sets an event to a method passed
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="eventName">Name of Event to Set</param>
        /// <param name="methodName">Name of Method to Set Event to</param>
        public static void AddEvent(object control, string eventName, string methodName)
        {
            //Only Try and add if Event and Method is found to exist
            if (control.GetType().GetEvents().Where(e => e.Name == eventName).Count() > 0)
               if (control.GetType().GetMethods().Where(m => m.Name == methodName).Count() > 0)
               {
                   control.GetType().GetEvent(eventName).AddEventHandler(control, Delegate.CreateDelegate(typeof (ControlEvent), control, control.GetType(). GetMethod(methodName)));
                   return;
               }

            //Throw exception if we reach here because event or method doesn't exist
            var c = (ControlBase)control;
            throw new Exception("Reflection failed. Could not attach Method '" + methodName + "' to Event '" + eventName + "' in Control '" + c.Name + "'");
        }

        /// <summary>
        /// Removes an events delegate method
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="eventName">Name of Event to Remove</param>
        /// <param name="methodName">Name of Method to Remove Event from</param>
        public static void RemoveEvent(object control, string eventName, string methodName)
        {
            //Only Try and add if Event and Method is found to exist
            if (control.GetType().GetEvents().Where(e => e.Name == eventName).Count() > 0)
               if (control.GetType().GetMethods().Where(m => m.Name == methodName).Count() > 0)
               {
                   control.GetType().GetEvent(eventName).RemoveEventHandler(control, Delegate.CreateDelegate(typeof (ControlEvent), control, control.GetType(). GetMethod(methodName)));
                   return;
               }

            //Throw exception if we reach here because event or method doesn't exist
            var c = (ControlBase)control;
            throw new Exception("Reflection failed. Could not remove Method '" + methodName + "' from Event '" + eventName + "' in Control '" + c.Name + "'");
        }

        #endregion
    }
}
