// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: StringCreator.cs Version: 1.0 Last Edited: 6/26/2012
// ------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Input.Global;
using Microsoft.Xna.Framework;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Input.Input
{
    public class StringCreator
    {
        #region Fields

        /// <summary>
        /// Timer that holds how long it was since our key was held down
        /// </summary>
        double _repeatUpdateTimer = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Determines if this class is enabled
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// A string value holding the text currently being typed
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Max length of text we can have
        /// </summary>
        int MaxLength { get; set; }

        #endregion

        #region XNA Methods

        /// <summary>
        /// Update the String Creator
        /// </summary>
        /// <param name="gameTime">Our Game Time</param>
        /// <param name="keysPressed">A Collection of Keys currently in Pressed State</param>
        /// <param name="keysDown">A Collection of keys currently in the Down state</param>
        public void Update(GameTime gameTime, ICollection keysPressed, List<Keys> keysDown)
        {
            //Return if not enabled
            if (!Enabled) return;
            //If our Text is to large we need to return, unless the Backspace key is pressed
            if (Text.Length >= MaxLength) 
                if (!keysDown.Contains(Keys.Back)) return;

            //Create a temp text variable and set it to empty
            var text = string.Empty;



            if (keysPressed.Count != 0) //If we have just pressed any keys add them in first
            {
                //Add all characters to our Text
                text += keysPressed.Cast<Keys>().Aggregate(text, (current, k) => current + HandleKey(k, keysDown));
                //Reset our repeat timer, has to be done here as well to prevent keys from instantly writting next update if else is ran
                _repeatUpdateTimer = 0;
            }
            else //Else we are only holding keys down
            {
                //Add our game time since last update to our repeat timer
                _repeatUpdateTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

                //And if we have reached our repeat standard then write the key
                if (_repeatUpdateTimer > Constant.KeyboardRepeatStandard)
                {
                    text += keysDown.Aggregate(text, (current, k) => current + HandleKey(k, keysDown));
                    //Reset our repeat timer
                    _repeatUpdateTimer = 0;
                }
            }

            if (MaxLength != -1)
            {
                if ((Text.Length + text.Length) >= MaxLength)
                { //If we can safly add our text to our Text do it
                    Text += text;
                    return;
                }

                var i = 0;

                while ((Text.Length + text.Length) <= MaxLength)
                {//Else loop adding one letter at a time until you need to stop
                    if (text.Length == i) break;
                    Text += text.ToCharArray()[i];
                    i++;
                }
            }
            else
                Text += text;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Start reading keys
        /// </summary>
        /// <param name="text">Text sent will be added into our StringCreator's text</param>
        /// <param name="length">Max lenth our text can be</param>
        public void Start (string text, int length)
        {
            if (Enabled) Enabled = false;

            Text = text;
            Enabled = true;
            MaxLength = length;
        }

        /// <summary>
        /// Stops reading keys
        /// </summary>
        public void Stop()
        {
            Enabled = false;
            MaxLength = -1;
        }

        #endregion

        #region Handle Key

        /// <summary>
        /// Handles transfering a key press and a collection of keys held down into a single string value
        /// </summary>
        /// <param name="k"></param>
        /// <param name="keysDown"></param>
        /// <returns></returns>
        string HandleKey(Keys k, ICollection<Keys> keysDown)
        {
            //Create an empty text temp variable
            var text = string.Empty;
            
            #region Special Keys

            //Handle any special key, those that are not a-z or 0-9
            switch (k)
            {
                case Keys.Back:
                    if (!string.IsNullOrWhiteSpace(Text))
                        Text = Text.Remove(Text.Length - 1);
                    break;
                case Keys.Space:
                    text += " ";
                    break;
                case Keys.OemBackslash:
                    if (keysDown.Contains(Keys.LeftShift) || keysDown.Contains(Keys.RightShift))
                        text += "|";
                    else
                        text += @"\";
                    break;
                case Keys.OemTilde:
                    if (keysDown.Contains(Keys.LeftShift) || keysDown.Contains(Keys.RightShift))
                        text += "~";
                    else
                        text += "`";
                    break;
                case Keys.OemMinus:
                    if (keysDown.Contains(Keys.LeftShift) || keysDown.Contains(Keys.RightShift))
                        text += "_";
                    else
                        text += "-";
                    break;
                case Keys.OemPlus:
                    if (keysDown.Contains(Keys.LeftShift) || keysDown.Contains(Keys.RightShift))
                        text += "+";
                    else
                        text += "=";
                    break;
                case Keys.OemOpenBrackets:
                    if (keysDown.Contains(Keys.LeftShift) || keysDown.Contains(Keys.RightShift))
                        text += "{";
                    else
                        text += "[";
                    break;
                case Keys.OemCloseBrackets:
                    if (keysDown.Contains(Keys.LeftShift) || keysDown.Contains(Keys.RightShift))
                        text += "}";
                    else
                        text += "]";
                    break;
                case Keys.OemQuotes:
                    if (keysDown.Contains(Keys.LeftShift) || keysDown.Contains(Keys.RightShift))
                        text += '"';
                    else
                        text += "'";
                    break;
                case Keys.OemSemicolon:
                    if (keysDown.Contains(Keys.LeftShift) || keysDown.Contains(Keys.RightShift))
                        text += ":";
                    else
                        text += ";";
                    break;
                case Keys.OemQuestion:
                    if (keysDown.Contains(Keys.LeftShift) || keysDown.Contains(Keys.RightShift))
                        text += "?";
                    else
                        text += "/";
                    break;
                case Keys.OemPeriod:
                    if (keysDown.Contains(Keys.LeftShift) || keysDown.Contains(Keys.RightShift))
                        text += ">";
                    else
                        text += ".";
                    break;
                case Keys.OemComma:
                    if (keysDown.Contains(Keys.LeftShift) || keysDown.Contains(Keys.RightShift))
                        text += "<";
                    else
                        text += ",";
                    break;
                case Keys.Multiply:
                    text += "*";
                    break;
                case Keys.Decimal:
                    text += ".";
                    break;
                case Keys.Subtract:
                    text += "-";
                    break;
                case Keys.Divide:
                    text += "/";
                    break;
                case Keys.Add:
                    text += "+";
                    break;
                default:
                    if ((k >= Keys.NumPad0 && k <= Keys.NumPad9) && InputHandler.IsNumLock())
                    {
                        switch (k)
                        {
                            case Keys.NumPad0:
                                text += 0;
                                break;
                            case Keys.NumPad1:
                                text += 1;
                                break;
                            case Keys.NumPad2:
                                text += 2;
                                break;
                            case Keys.NumPad3:
                                text += 3;
                                break;
                            case Keys.NumPad4:
                                text += 4;
                                break;
                            case Keys.NumPad5:
                                text += 5;
                                break;
                            case Keys.NumPad6:
                                text += 6;
                                break;
                            case Keys.NumPad7:
                                text += 7;
                                break;
                            case Keys.NumPad8:
                                text += 8;
                                break;
                            case Keys.NumPad9:
                                text += 9;
                                break;
                        }
                    }

                        #endregion

                        #region Numbers
                        //Handles any Numbers and the characters they can make
                    else if (k >= Keys.D0 && k <= Keys.D9)
                    {
                        var num = ((k - Keys.D0)).ToString();

                        if (keysDown.Contains(Keys.LeftShift) || keysDown.Contains(Keys.RightShift))
                        {
                            switch (k)
                            {
                                case Keys.D1:
                                    num = "!";
                                    break;
                                case Keys.D2:
                                    num = "@";
                                    break;
                                case Keys.D3:
                                    num = "#";
                                    break;
                                case Keys.D4:
                                    num = "$";
                                    break;
                                case Keys.D5:
                                    num = "%";
                                    break;
                                case Keys.D6:
                                    num = "^";
                                    break;
                                case Keys.D7:
                                    num = "&";
                                    break;
                                case Keys.D8:
                                    num = "*";
                                    break;
                                case Keys.D9:
                                    num = "(";
                                    break;
                                case Keys.D0:
                                    num = ")";
                                    break;
                            }
                        }

                        text += num;
                    }

                        #endregion

                        #region Normal Text

                    else if (k >= Keys.A && k <= Keys.Z)
                    {
                        var t = string.Empty;

                        #region Special Cases

                        if (keysDown.Contains(Keys.LeftControl) || keysDown.Contains(Keys.RightControl))
                        {
                            //If we are holding Control we want to do a special key
                            switch (k)
                            {
                                case Keys.C:
                                    {
                                        //TODO: Design a Selection Method to allow Copying text
                                    }
                                    break;
                                case Keys.V:
                                    t = Clipboard.GetText(); //Paste from Clipboard
                                    break;
                            }
                        }
                        else
                        {
                            //Else we are not a special Key so just write our String
                            t = k.ToString();
                        }

                        #endregion

                        if (InputHandler.IsCapsLock())
                        {
                            //If Caps Lock is On
                            if (keysDown.Contains(Keys.LeftShift) || keysDown.Contains(Keys.RightShift))
                                //If Left or Right shift IS held down
                                t = t.ToLower(); //Change text to lower
                        }
                        else
                        {
                            //Else Caps Lock Is Off
                            if (!keysDown.Contains(Keys.LeftShift) && !keysDown.Contains(Keys.RightShift))
                                //If Left or Right shift is NOT held down
                                t = t.ToLower(); //Change text to lower
                        }
                        //Set our value to text
                        text += t;
                    }
                    break;
            }

            #endregion
            
            //Return our string
            return text;
        }

        #endregion
    }
}
