// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Phone.cs Version: 1.0 Last Edited: 6/29/2012
// ------------------------------------------------------------------------


using System;
using Microsoft.Xna.Framework;

#if WINDOWS_PHONE
using System.Collections.Generic;
using Microsoft.Xna.Framework.GamerServices;
using System.Linq;
using System.Windows;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework.Input.Touch;
#endif

namespace Input
{
    public partial class InputHandler
    {
#region Fields

        

        #endregion
        
#region Properties
#if WINDOWS_PHONE
        public static TouchCollection Touch { get; set; }
        public static int MaxTouchCount { get; set; }
        public static bool TouchEnabled { get; set; }

        public static Motion Motion { get; set; }
        public static bool MotionEnabled { get; set;  }

        public static float Yaw { get; set; }
        public static float Pitch { get; set; }
        public static float Roll { get; set; }

        public static Vector3 Acceleration { get; set; }
#endif

        #endregion

        #region Motion Sensor Methods
        
        /// <summary>
        /// Enables our Motion sensor if the device supports it
        /// </summary>
        public static void EnableMotion()
        {
#if WINDOWS_PHONE
            if (!Motion.IsSupported)
                Motion.Stop();
            else
            {
                Motion = new Motion
                             {
                                 TimeBetweenUpdates = TimeSpan.FromMilliseconds(30)
                             };
                Motion.CurrentValueChanged += MotionCurrentValueChanged;

                try
                {
                    Motion.Start();
                    MotionEnabled = true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
#endif
        }

        /// <summary>
        /// Disable our Motion Sensor
        /// </summary>
        public static void DisableMotion()
        {
#if WINDOWS_PHONE
            if (Motion == null) return;
            Motion.Stop();
            Motion = null;
            MotionEnabled = false;
#endif
        }

        #endregion

        #region Touch Screen Methods

        /// <summary>
        /// Enables all touch capabilities that our device supports
        /// </summary>
        public static void EnableTouch()
        {
#if WINDOWS_PHONE
            var capabilities = TouchPanel.GetCapabilities();
            if (capabilities.IsConnected)
            {
                //Set our Max touch counts to the devices maximum capabilities
                MaxTouchCount = capabilities.MaximumTouchCount;
                //Enable the Tap Gesture
                TouchPanel.EnabledGestures = GestureType.Tap;

                TouchEnabled = true;
            }
            else
            {
                DisableTouch();
            }
#endif
        }

        /// <summary>
        /// Disable all Touch capabilities on our device
        /// </summary>
        public static void DisableTouch()
        {
#if WINDOWS_PHONE
            Touch.Clear();
            MaxTouchCount = 0;
            TouchEnabled = false;
#endif
        }

        #endregion

        #region Software Keyboard Methods

        /// <summary>
        /// Shows the software Keyboard to the player
        /// </summary>
        /// <param name="player">Index of the Player, usually PlayerIndex.One</param>
        /// <param name="title">Title of the Keyboard</param>
        /// <param name="description">Description of text to be typed in</param>
        /// <param name="defualt">Defualt text used if none is typed, if no text is desired defualt use string.Empty</param>
        /// <param name="endCallback">Method that will be called when user closes the software keyboard, text will be sent here</param>
        public static void BeginShowKeyboard(PlayerIndex player, string title, string description, string defualt, AsyncCallback endCallback)
        {
#if WINDOWS_PHONE
            //If our Keyboard is already shown do not worry about showing it again
            if (!Guide.IsVisible)
                Guide.BeginShowKeyboardInput(player, title, description, defualt, endCallback, null);
#endif
        }

        /// <summary>
        /// Ends the showing of our Keyboard and returns the string it just recieved from the user
        /// </summary>
        /// <param name="ar">An Asynchronous Result recieved from originally showing our Keyboard</param>
        /// <returns></returns>
        public static string EndShowKeyboard(IAsyncResult ar)
        {
#if WINDOWS_PHONE
            return Guide.EndShowKeyboardInput(ar);
#endif
            return string.Empty;
        }

        #region Touch Sensor Methods


        /// <summary>
        /// Returns a bool determining if an area was pressed
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static bool Pressed(Rectangle area)
        {
#if WINDOWS_PHONE
            return Touch.IsConnected && Touch.Where(tl => tl.State == TouchLocationState.Pressed).Any(tl => area.Contains((int)tl.Position.X, (int)tl.Position.Y));
#endif
            return false;
        }

        /// <summary>
        /// Returns a bool determining if an area was released
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static bool Released(Rectangle area)
        {
#if WINDOWS_PHONE
            return Touch.IsConnected && Touch.Where(tl => tl.State == TouchLocationState.Released).Any(tl => area.Contains((int)tl.Position.X, (int)tl.Position.Y));
#endif
            return false;
        }

        #endregion

        #endregion

#if WINDOWS_PHONE
        #region Windows Phone Specific Methods

        /// <summary>
        /// Used to update our Reading data
        /// </summary>
        /// <param name="e"></param>
        static void CurrentValueChanged(MotionReading e)
        {
            //Get Yaw, Pitch, and Roll
            Yaw = MathHelper.ToDegrees(e.Attitude.Yaw);
            Pitch = MathHelper.ToDegrees(e.Attitude.Pitch);
            Roll = MathHelper.ToDegrees(e.Attitude.Roll);
            //Get the Acceleration on all 3 vectors
            Acceleration = new Vector3(e.DeviceAcceleration.X, e.DeviceAcceleration.Y, e.DeviceAcceleration.Z);
        }
        
        #region MessageBox Methods

        /// <summary>
        /// Very simple implementation of showing a Message Box using minimal information
        /// </summary>
        /// <param name="title">Title of Message Box</param>
        /// <param name="message">Message inside the Message Box</param>
        /// <param name="icon">Icon used for the Message Box</param>
        public static void ShowMessageBox(string title, string message, MessageBoxIcon icon)
        {
            Guide.BeginShowMessageBox(title, message, new List<string> {"OK"}, 0, icon, null, null);
        }

        #endregion

        #region Events

        /// <summary>
        /// Event fires when our Motion values change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void MotionCurrentValueChanged(object sender, SensorReadingEventArgs<MotionReading> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => CurrentValueChanged(e.SensorReading));
        }

        #endregion

        #endregion

#endif

    }
}

