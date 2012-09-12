using Microsoft.Xna.Framework;

namespace Graphics.GUI
{
    public static class GuiRenderer
    {

        public static void Render(object control)
        {
            GraphicsHandler.Begin();
            foreach(var i in control.GetType().GetInterfaces())
            {
                switch (i.Name)
                {
                    case "IString":
                        {
                            var font = GetPropertyValue(control, "Font") as string;
                            var text = GetPropertyValue(control, "Text") as string;
                            var location = GetFieldValue(control, "Location") is Vector2 ? (Vector2) GetFieldValue(control, "Location") : new Vector2();
                            var color = GetPropertyValue(control, "FontColor") is Color ? (Color) GetPropertyValue(control, "FontColor") : new Color();
                            GraphicsHandler.DrawString(font, text, location, color);
                        }
                        break;
                }
            }

            GraphicsHandler.End();
        }

        /// <summary>
        /// Gets a Property Value from a passed object based on name sent
        /// </summary>
        /// <param name="control"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static object GetPropertyValue(object control, string name)
        {
            return control.GetType().GetProperty(name).GetValue(control, null);
        }

        /// <summary>
        /// Gets a Field Value from a passed object based on name sent
        /// </summary>
        /// <param name="control"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static object GetFieldValue(object control, string name)
        {
            return control.GetType().GetField(name).GetValue(control);
        }
    }
}
