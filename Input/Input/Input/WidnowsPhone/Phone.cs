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
#if WINDOWS_PHONE

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

#if WINDOWS_PHONE
using Microsoft.Devices.Sensors;
#endif

namespace Input
{
    public partial class InputHandler
    {
        #region Fields

        #endregion
        
        #region Properties

        public static Motion Motion { get; set; }
        public static bool MotionEnabled { get; set;  }

        public static float Yaw { get; set; }
        public static float Pitch { get; set; }
        public static float Roll { get; set; }

        public static Vector3 Acceleration { get; set; }


        #endregion

        #region Sensor Methods

        /// <summary>
        /// Enables our Motion sensor if the device supports it
        /// </summary>
        public static void EnableMotion()
        {
            if (!Motion.IsSupported)
            {
            }
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
        }

        /// <summary>
        /// Disable our Motion Sensor
        /// </summary>
        public static void DisableMotion()
        {
            if (Motion == null) return;
            Motion.Stop();
            Motion = null;
            MotionEnabled = false;
        }

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
            //If our Keyboard is already shown do not worry about showing it again
            if (!Guide.IsVisible)
                Guide.BeginShowKeyboardInput(player, title, description, defualt, endCallback, null);
        }

        /// <summary>
        /// Ends the showing of our Keyboard and returns the string it just recieved from the user
        /// </summary>
        /// <param name="ar">An Asynchronous Result recieved from originally showing our Keyboard</param>
        /// <returns></returns>
        public static string EndShowKeyboard(IAsyncResult ar)
        {
            return Guide.EndShowKeyboardInput(ar);
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

    }
}

#endif