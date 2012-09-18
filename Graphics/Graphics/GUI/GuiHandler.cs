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

using System.Linq;
using Graphics.GUI.Controls;
using Graphics.GUI.Interfaces;
using Microsoft.Xna.Framework;

namespace Graphics.GUI
{
    public static class GuiHandler
    {
        #region Initialize

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
            return control.GetType().GetProperty(name).GetValue(control, null);
        }

        public static void SetPropertyValue(object control, string name, object data)
        {
            control.GetType().GetProperty(name).SetValue(control, data, null);
        }

        /// <summary>
        /// Fires the event passed in the control passed
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="name">Name of Event to fire</param>
        /// <param name="eventArgs">Event args to pass to event, usually can be null</param>
        public static void FireEvent(object control, string name, object eventArgs)
        {
            control.GetType().GetMethod(name).Invoke(control, new[] { control, eventArgs });
        }

        #endregion
    }
}
