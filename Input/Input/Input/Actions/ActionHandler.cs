// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: ActionHandler.cs Version: 1.1 Last Edited: 6/26/2012
// ------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Input.Global;

#if WINDOWS

using System.Windows.Forms;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;

#endif

namespace Input.Input.Actions
{
    public class ActionHandler
    {
        #region Fields

        /// <summary>
        /// A Dictionary of our Actions
        /// </summary>
        public Dictionary<string, Action> Actions = new Dictionary<string, Action>();

        public bool ControllerEnabled = false;
        public bool KeyboardEnabled = false;
        public bool MouseEnabled = false;
        public bool WindowsPhoneEnabled = false;

        #endregion
        
        #region Constructor

        /// <summary>
        /// Empty Constructor for Serialization
        /// </summary>
        public ActionHandler()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if Key exists
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        bool Exists(string test)
        {
            return Actions.ContainsKey(test);
        }

        /// <summary>
        /// Checks if an action is running while also ensuring it exists
        /// </summary>
        /// <param name="name">Name of Action</param>
        /// <returns></returns>
        public bool IsActionRunning(string name)
        {
            return Exists(name) && Actions[name].IsActionRunning();
        }

        /// <summary>
        /// Add an Action to our Actions List if it doesn't already exist
        /// </summary>
        /// <param name="name">Name of our Action</param>
        /// <param name="action">Action needed to perform</param>
        public void Add(string name, Action action)
        {
            //If our Action already exists remove it in place of the new one
            if (Exists(name))
                Actions.Remove(name);

            //Add our Action
            Actions.Add(name, action);
        }

        #endregion

        #region Saving and Loading

        /// <summary>
        /// Save our Control Scheme
        /// </summary>
        public void Save()
        {  
#if WINDOWS
            using (var writer = XmlWriter.Create(Constant.ControlScheme, new XmlWriterSettings { Indent = true }))
                IntermediateSerializer.Serialize(writer, this, null);
#elif XBOX
#elif WINDOWSPHONE
#endif
        }

        /// <summary>
        /// Save our Control Scheme
        /// </summary>
        public void Save(string path)
        {
#if WINDOWS
            using (var writer = XmlWriter.Create(path, new XmlWriterSettings { Indent = true }))
                IntermediateSerializer.Serialize(writer, this, null);
#elif XBOX
#elif WINDOWSPHONE
#endif
        }


        /// <summary>
        /// Load our Control Scheme
        /// </summary>
        public static void Load()
        {

            #region Windows

#if WINDOWS
            if (!File.Exists(Constant.ControlScheme)) return;

            try
            {//Try and load our XML file in
                using (var reader = XmlReader.Create(Constant.ControlScheme))
                    InputHandler.ActionHandler = IntermediateSerializer.Deserialize<ActionHandler>(reader, "InputLibrary.Input.Actions.ActionHandler");
                
            }
            catch (Exception)
            {
                //Return if we met with an error
                MessageBox.Show("Error Loading Control Scheme.");
                return;
            }

            if (InputHandler.ActionHandler.ControllerEnabled)
                InputHandler.EnableControllers();
#endif
            #endregion
            
            #region XBOX

#if XBOX


#endif

            #endregion

            #region WINDOWS PHONE

#if WINDOWSPHONE


#endif

            #endregion
        }

        /// <summary>
        /// Load our Control Scheme
        /// </summary>
        public static void Load(string path)
        {

            #region Windows

#if WINDOWS
            if (!File.Exists(path)) return;

            try
            {//Try and load our XML file in
                using (var reader = XmlReader.Create(path))
                    InputHandler.ActionHandler = IntermediateSerializer.Deserialize<ActionHandler>(reader, "InputLibrary.Input.Actions.ActionHandler");
                
            }
            catch (Exception)
            {
                //Return if we met with an error
                MessageBox.Show("Error Loading Control Scheme.");
                return;
            }

            if (InputHandler.ActionHandler.ControllerEnabled)
                InputHandler.EnableControllers();
#endif
            #endregion

            #region XBOX

#if XBOX


#endif

            #endregion

            #region WINDOWS PHONE

#if WINDOWSPHONE


#endif

            #endregion
        }

        #endregion

    }
}
