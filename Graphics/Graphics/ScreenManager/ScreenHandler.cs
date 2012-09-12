// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: ScreenHandler.cs Version: 1.0 Last Edited: 9/8/2012
// ------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Graphics.Misc;
using Microsoft.Xna.Framework;

namespace Graphics.ScreenManager
{
    public class ScreenHandler : DrawableGameComponent
    {
        #region Events

        public event EventHandler OnScreenChange;

        #endregion

        #region Fields

        List<ScreenBase> _screens = null;

        readonly Stack<ScreenBase> _gameScreens = new Stack<ScreenBase>();

        #endregion

        #region Properties
        
        /// <summary>
        /// Singleton Acccessor
        /// </summary>
        static ScreenHandler Instance { get; set; }

        /// <summary>
        /// The level that this game component draws too
        /// </summary>
        public static int DrawLevel { get { return Instance.DrawOrder; } }

        /// <summary>
        /// When you try to get this Property you recieve the top level screen
        /// </summary>
        public static ScreenBase CurrentScreen
        {
            get { return Instance._gameScreens.Peek(); }
        }
        
        #endregion

        #region Constructor

        public ScreenHandler(Game game) : base(game)
        {
            Instance = this;

            //Set our current draw order
            Instance.DrawOrder = Constants.StartDrawOrder;

            //Setup our Screens
            Instance._screens = new List<ScreenBase>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a screen for later use
        /// </summary>
        /// <param name="newScreen"></param>
        public static void Add(ScreenBase newScreen)
        {
            //Ask it to load its content
            newScreen.LoadContent();
            //Add screen into our screen manager
            Instance._screens.Add(newScreen);
        }

        /// <summary>
        /// Pop Screen will remove the top level screen from our stack
        /// </summary>
        private static void PopScreen()
        {
            //Get our top level screen
            var screen = Instance._gameScreens.Peek();
            //Hide our screen
            screen.Hide();
            //Unsubscribe this screen from our On Change method so it doesn't trigger anymore
            Instance.OnScreenChange -= screen.ScreenChange;
            //Remove our screen from our components
            Instance.Game.Components.Remove(screen);
            //Pop off the screen from our stack
            Instance._gameScreens.Pop();
        }

        private static void AddScreenToStack(ScreenBase screen)
        {
            //Push our added screen to top of stack
            Instance._gameScreens.Push(screen);
            //Add it into our Game Components list
            Instance.Game.Components.Add(screen);
            //Attach an event for On Screen Change
            Instance.OnScreenChange += screen.ScreenChange;
        }

        /// <summary>
        /// Change will cause the screen to change from one to the one passed
        /// </summary>
        /// <param name="name"></param>
        public static void Change(string name)
        {
            var screen = GetScreen(name);

            if (screen == null) throw new Exception("Could not locate Screen: " + name);

            //Remove all screens from our stack
            while (Instance._gameScreens.Count > 0)
                PopScreen();

            //Alter the draw order a bit
            screen.DrawOrder = Constants.StartDrawOrder;
            Instance.DrawOrder = Constants.StartDrawOrder;

            AddScreenToStack(screen);

            //Fire off the On Change Event
            if (Instance.OnScreenChange != null)
                Instance.OnScreenChange(Instance, null);
        }
        
        /// <summary>
        /// Gets a Screen based on the name passed
        /// </summary>
        /// <param name="name">Name of Screen to fetch</param>
        static ScreenBase GetScreen(string name)
        {            
            return Instance._screens.Where(s => s.Name == name).FirstOrDefault();
        }

        #endregion
    }
}
