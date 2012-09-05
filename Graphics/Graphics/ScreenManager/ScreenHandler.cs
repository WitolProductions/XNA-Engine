using System;
using System.Collections.Generic;
using Graphics.Misc;
using Microsoft.Xna.Framework;

namespace Graphics.ScreenManager
{
    public class ScreenHandler : DrawableGameComponent
    {

        #region Events

        public event EventHandler OnStateChange;

        #endregion

        #region Fields

        readonly Stack<ScreenBase> _gameStates = new Stack<ScreenBase>();

        #endregion

        #region Properties

        public static ScreenHandler Instance { get; set; }

        public static ScreenBase CurrentScreen
        {
            get { return Instance._gameStates.Peek(); }
            set { Instance.ChangeScreen(value); }
        }

        List<ScreenBase> _screens = new List<ScreenBase>();
        
        #endregion

        #region Constructor

        public ScreenHandler(Game game) : base(game)
        {
            Instance = this;

            //Set our current draw order
            Instance.DrawOrder = Constants.StartDrawOrder;

            //If for some reason _screens isn't already initialized
            if (Instance._screens == null)
                Instance._screens = new List<ScreenBase>();
        }

        #endregion

        #region Methods

        public void PopScreen()
        {
            if (Instance._gameStates.Count <= 0) return;
            RemoveState();
            Instance.DrawOrder -= Constants.DrawOrderInc;

            if (Instance.OnStateChange != null)
                Instance.OnStateChange(this, null);
        }

        private static void RemoveState()
        {
            var state = Instance._gameStates.Peek();

            Instance.OnStateChange -= state.ScreenChange;
            Instance.Game.Components.Remove(state);
            Instance._gameStates.Pop();
        }

        public void PushScreen(ScreenBase newScreen)
        {
            Instance.DrawOrder += Constants.DrawOrderInc;
            newScreen.DrawOrder = Instance.DrawOrder;

            AddScreen(newScreen);

            if (Instance.OnStateChange != null)
                Instance.OnStateChange(this, null);
        }

        public static void AddScreen(ScreenBase newScreen)
        {
            Instance._gameStates.Push(newScreen);

            Instance.Game.Components.Add(newScreen);

            newScreen.LoadContent();

            Instance.OnStateChange += newScreen.ScreenChange;
        }

        public void ChangeScreen(ScreenBase newScreen)
        {
            while (Instance._gameStates.Count > 0)
                RemoveState();

            newScreen.DrawOrder = Constants.StartDrawOrder;
            Instance.DrawOrder = Constants.StartDrawOrder;

            AddScreen(newScreen);

            if (Instance.OnStateChange != null)
                Instance.OnStateChange(this, null);
        }
        
        #endregion

        #region XNA Commands

        #endregion
    }
}
