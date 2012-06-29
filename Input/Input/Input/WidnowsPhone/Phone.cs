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

        static Accelerometer _accelerometer;

        #endregion

        #region Properties

        public static bool AccelerometerEnabled;
        public static Vector3 AccelerometerReading = Vector3.Zero;

        #endregion

        #region Methods

        /// <summary>
        /// Enable the Accelerometer if this device supports it
        /// </summary>
        public static void EnableAccelerometer()
        {
            if (!Accelerometer.IsSupported) return;
            //Set our objects instance
            _accelerometer = new Accelerometer();
            //Setup our Event handler
            _accelerometer.CurrentValueChanged += AccelerometerCurrentValueChanged;
            //Start the Accelerometer
            _accelerometer.Start();
            //Set that our Accelerometer is enabled
            AccelerometerEnabled = true;
        }

        /// <summary>
        /// Disable the Accelerometer
        /// </summary>
        public static void DisableAccelerometer()
        {
            //Null our instance of Accelerometer
            _accelerometer = null;
            //Zero our Accelerometer Reading
            AccelerometerReading = Vector3.Zero;
            //Set that our Accelerometer is disabled
            AccelerometerEnabled = false;
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
        /// Accelerometer Reading Event
        /// </summary>
        /// <param name="sender">Object sending the data</param>
        /// <param name="e">Event data</param>
        static void AccelerometerCurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            AccelerometerReading = e.SensorReading.Acceleration;
        }

        #endregion

    }
}

#endif