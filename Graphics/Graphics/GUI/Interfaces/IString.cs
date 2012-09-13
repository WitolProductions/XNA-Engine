namespace Graphics.GUI.Interfaces
{
    /// <summary>
    /// IString implements an Interface that allows writting a single line of text onto a Control, requires a valid IFont in order to work
    /// </summary>
    interface IString : IFont, IEvents
    {
        #region Properties

        /// <summary>
        /// Text to be Written 
        /// </summary>
        string Text { get; set; }

        #endregion
    }
}
