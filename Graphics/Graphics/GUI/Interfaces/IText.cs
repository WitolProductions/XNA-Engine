// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: IText.cs Version: 1.0 Last Edited: 9/18/2012
// ------------------------------------------------------------------------

using System.Linq;
using Graphics.GUI.Controls;
using Graphics.Misc;
using Microsoft.Xna.Framework;

namespace Graphics.GUI.Interfaces
{
    /// <summary>
    /// IText implements an Interface that allows writting a single line of text onto a Control, requires a valid IFont in order to work
    /// </summary>
    interface IText : IFont
    {
        #region Properties

        /// <summary>
        /// Text to be Written 
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Temp text used to detect a change in Text
        /// </summary>
        string TempText { get; set; }

        /// <summary>
        /// Text offset, set automatically to ensure we don't draw text on a border
        /// </summary>
        Vector2 TextOffset { get; set; }

        /// <summary>
        /// Alignment of Text relitive to Control
        /// </summary>
        Enumerations.ContentAlignment TextAlign { get; set; }
        
        #endregion

        #region Events

        /// <summary>
        /// Text was changed
        /// </summary>
        event EventHelper.ControlEvent TextChanged;

        #endregion

        #region Abstract Events

        /// <summary>
        /// On Text Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void OnTextChanged(object sender, object eventArgs);

        #endregion
    }

    /// <summary>
    /// Handles Drawing and Updating our IText Interface
    /// </summary>
    public static class Text
    {
        /// <summary>
        /// Initializes our Control with base information needed
        /// </summary>
        /// <param name="controlBase"></param>
        public static void Initialize(object controlBase)
        {
            EventHelper.AddEvent(controlBase, "TextChanged", typeof(IText), "OnTextChanged");
            ReflectionHelper.SetPropertyValue(controlBase, "TextAlign", Enumerations.ContentAlignment.MiddleLeft);
        }

        /// <summary>
        /// Update our IText Interface in conjunction with the passed control
        /// </summary>
        public static void Update(object controlBase, GameTime gameTime)
        {
            var control = (ControlBase)controlBase;

            var tempText = ReflectionHelper.GetPropertyValue(control, "TempText") as string;
            var text = ReflectionHelper.GetPropertyValue(control, "Text") as string;

            if (text != null) 
                if (!text.Equals(tempText))
                {
                    ReflectionHelper.SetPropertyValue(control, "TempText", text);
                    EventHelper.FireEvent(controlBase, "TextChanged", null);
                }
        }

        /// <summary>
        /// Draw our IText Interface in conjunction with the passed control
        /// </summary>
        public static void Draw(object controlBase, GameTime gameTime)
        {
            //Gather information on Font, Color, Text, etc. and then draw it
            var control = (ControlBase)controlBase;
            var font = ReflectionHelper.GetPropertyValue(control, "Font") as string;
            var text = ReflectionHelper.GetPropertyValue(control, "Text") as string;
            var fontSize = GraphicsHandler.MesureString((string)ReflectionHelper.GetPropertyValue(controlBase, "Font"), (string)ReflectionHelper.GetPropertyValue(controlBase, "Text"));

            var location = GetPosition((ControlBase) controlBase,
                                       (Enumerations.ContentAlignment) ReflectionHelper.GetPropertyValue(controlBase, "TextAlign"),
                                       new Rectangle((int) control.Location.X, (int) control.Location.Y, (int) fontSize.X, (int) fontSize.Y));

            var color = ReflectionHelper.GetPropertyValue(control, "FontColor") is Color ?
                (Color)ReflectionHelper.GetPropertyValue(control, "FontColor") * (float)ReflectionHelper.GetPropertyValue(control, "FontAlpha") : Color.Transparent;


            GraphicsHandler.DrawString(font, text, location, color);
        }


        static Vector2 GetPosition(ControlBase control, Enumerations.ContentAlignment align, Rectangle area)
        {
            var borderWidth = 0;

            //If our Control uses the Border Interface than we need to 
            if (control.GetType().GetInterfaces().Where(e => e.Name == "IBorder").Count() > 0)
                borderWidth = (int)ReflectionHelper.GetPropertyValue(control, "BorderWidth");

            var width = control.Size.X - borderWidth;
            var height = control.Size.Y - borderWidth;

            var x = 0;
            var y = 0;

            switch(align)
            {
                case Enumerations.ContentAlignment.BottomCenter:

                    break;
                case Enumerations.ContentAlignment.BottomLeft:
                    x = (int) control.Location.X;
                    y = (int)(control.Location.Y + height - area.Height);
                    break;
                case Enumerations.ContentAlignment.BottomRight:
                    break;
                case Enumerations.ContentAlignment.MiddleCenter:
                    break;
                case Enumerations.ContentAlignment.MiddleLeft:
                    x = (int) control.Location.X;
                    y = (int) (control.Location.Y + height/2 - area.Height/2);
                    break;
                case Enumerations.ContentAlignment.MiddleRight:
                    break;
                case Enumerations.ContentAlignment.TopCenter:
                    break;
                case Enumerations.ContentAlignment.TopLeft:
                    x = (int) control.Location.X;
                    y = (int) control.Location.Y;
                    break;
                case Enumerations.ContentAlignment.TopRight:
                    break;
            }
            //If the Interface Border is detected add border width into our calculations
            if (control.GetType().GetInterfaces().Where(e => e.Name == "IBorder").Count() > 0)
            {
                x += borderWidth;
                y += borderWidth;
            }

            return new Vector2(x, y);
        }
    }
}
