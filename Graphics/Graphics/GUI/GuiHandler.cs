using Graphics.GUI.Controls;
using Graphics.Misc;
using Input;
using Microsoft.Xna.Framework;

namespace Graphics.GUI
{
    public static class GuiHandler
    {
        #region Update

        /// <summary>
        /// Update a control if it needs to be updated
        /// </summary>
        /// <param name="control"></param>
        /// <param name="gameTime"></param>
        public static void Update(ControlBase control, GameTime gameTime)
        {
            var controlBase = control;
            foreach (var i in control.GetType().GetInterfaces())
            {
                switch (i.Name)
                {
                    case "IEvents":
                        {
                            if (controlBase.Disabled)
                            {//Set to Disabled if needed
                                if (controlBase.State != Enumerationcs.ControlState.Disabled)
                                {
                                    controlBase.State = Enumerationcs.ControlState.Disabled;
                                    FireEvent(control, "OnInputChanged");
                                }
                            }
#if WINDOWS || XBOX
                            

#elif WINDOWS_PHONE
                            else if (InputHandler.AreaTouched(controlBase.Bounds))
                            {
                                if (controlBase.State != Enumerationcs.ControlState.Clicked)
                                {
                                    controlBase.State = Enumerationcs.ControlState.Clicked;
                                    FireEvent(control, "OnInputChanged");
                                }
                            }
#endif
                            else
                            {
                                if (controlBase.State != Enumerationcs.ControlState.Normal)
                                {
                                    controlBase.State = Enumerationcs.ControlState.Normal;
                                    FireEvent(control, "OnInputChanged");
                                }
                            }
                        }
                        break;
                    default: //If anything else simply break because its not needed
                        break;
                }
            }
        }

        #endregion

        #region Draw

        public static void Draw(ControlBase control, GameTime gameTime)
        {
            GraphicsHandler.Begin();
            var controlBase = control;
            foreach(var i in control.GetType().GetInterfaces())
            {
                switch (i.Name)
                {
                    case "IString":
                        {
                            var font = GetPropertyValue(control, "Font") as string;
                            var text = GetPropertyValue(control, "Text") as string;
                            var location = controlBase.Location;
                            var color = GetPropertyValue(control, "FontColor") is Color ? (Color) GetPropertyValue(control, "FontColor") : Color.Transparent;
                            GraphicsHandler.DrawString(font, text, location, color);
                        }
                        break;
                    case "IBackground":
                        {
                            var color = Color.Transparent;
                            if (controlBase.State == Enumerationcs.ControlState.Normal)
                                color = GetPropertyValue(control, "BackgroundNormalColor") is Color ? (Color)GetPropertyValue(control, "BackgroundNormalColor") : Color.Transparent;
                            else if (controlBase.State == Enumerationcs.ControlState.Clicked)
                                color = GetPropertyValue(control, "BackgroundClickedColor") is Color ? (Color)GetPropertyValue(control, "BackgroundClickedColor") : Color.Transparent;
                            GraphicsHandler.DrawFillRectangle(controlBase.Bounds, color);
                        }
                        break;
                    default: //If anything else simply break because its not needed
                        break;
                }
            }

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
        static object GetPropertyValue(object control, string name)
        {
            return control.GetType().GetProperty(name).GetValue(control, null);
        }

        /// <summary>
        /// Fires the event passed in the control passed
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="name">Name of Event to fire</param>
        static void FireEvent(object control, string name)
        {
            control.GetType().GetEvent(name).AddEventHandler(control, null);
        }

        #endregion
    }
}
