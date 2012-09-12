namespace Graphics.GUI.Interfaces
{
    /// <summary>
    /// IString implements an Interface that allows writting text onto a Control, requires IFont in order to work
    /// </summary>
    interface IString : IFont
    {
        #region Fields

        /// <summary>
        /// Text to be Written 
        /// </summary>
        string Text { get; set; }

        #endregion
    }
}
