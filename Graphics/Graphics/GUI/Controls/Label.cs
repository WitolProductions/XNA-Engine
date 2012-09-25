// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Label.cs Version: 1.0 Last Edited: 9/13/2012
// ------------------------------------------------------------------------

using System;
using Graphics.GUI.Interfaces;
using Graphics.Misc;
using Microsoft.Xna.Framework;

namespace Graphics.GUI.Controls
{
    public class Label : ControlBase, IBackground, IText, IBorder
    {
        #region Fields

        string _text = null;

        #endregion

        #region Inherited Properties

        #region Implementation of IFont

        public string Text { get { return _text; } 
            set
            {
                _text = value;
                OnTextChanged(this, null);
            } }

        public Vector2 TextOffset { get; set; }

        public Enumerations.ContentAlignment TextAlign { get; set; }

        public string Font { get; set; }

        public Color FontColor { get; set; }

        public float FontAlpha { get; set; }

        #endregion

        #region Implementation of IEvents
        
        public event ControlEvent Clicked;
        public event ControlEvent Enter;
        public event ControlEvent Leave;
        public event ControlEvent Down;
        public event ControlEvent Disabled;
        public event ControlEvent TextChanged;
        public event ControlEvent TabStoped;

        #endregion

        #region Implementation of IBorder

        public string BorderNormalImage { get; set; }

        public string BorderHoverImage { get; set; }

        public string BorderClickedImage { get; set; }

        public string BorderDisabledImage { get; set; }

        public Color BorderNormalColor { get; set; }

        public Color BorderHoverColor { get; set; }

        public Color BorderClickedColor { get; set; }

        public Color BorderDisabledColor { get; set; }

        public float BorderNormalAlpha { get; set; }

        public float BorderHoverAlpha { get; set; }

        public float BorderClickedAlpha { get; set; }

        public float BorderDisabledAlpha { get; set; }

        public int BorderWidth { get; set; }

        #endregion

        #region Implementation of IBackground

        public string BackgroundNormalImage { get; set; }

        public string BackgroundHoverImage { get; set; }

        public string BackgroundClickedImage { get; set; }

        public string BackgroundDisabledImage { get; set; }

        public Color BackgroundNormalColor { get; set; }

        public Color BackgroundHoverColor { get; set; }

        public Color BackgroundClickedColor { get; set; }

        public Color BackgroundDisabledColor { get; set; }

        public float BackgroundNormalAlpha { get; set; }

        public float BackgroundHoverAlpha { get; set; }

        public float BackgroundClickedAlpha { get; set; }

        public float BackgroundDisabledAlpha { get; set; }

        #endregion

        #endregion

        #region Constructor

        public Label(Game game) : base(game)
        {
            
        }

        #endregion

        #region Methods

        #endregion

        #region XNA Methods

        public override void Update(GameTime gameTime)
        {
            GuiHandler.Update(this, gameTime);
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GuiHandler.Draw(this, gameTime);
            
            base.Draw(gameTime);
        }

        #endregion

        #region Events

        public void OnTextChanged(object sender, object eventArgs)
        {
            Size = GraphicsHandler.MesureString(Font, Text) + new Vector2(BorderWidth, BorderWidth);
        }

        public void OnDown(object sender, object eventArgs)
        {
            Text = "Down!";
        }

        public void OnTabStoped(object sender, object eventArgs)
        {
        }

        public void OnEnter(object sender, object eventArgs)
        {
            Text = "Entered!";
        }

        public void OnDisabled(object sender, object eventArgs)
        {
        }

        public void OnLeave(object sender, object eventArgs)
        {
            Text = "Left!";
        }

        public void OnClicked(object sender, object eventArgs)
        {
            Text = "Clicked!";
        }

        #endregion
    }
}
