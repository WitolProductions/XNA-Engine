using Microsoft.Xna.Framework;

namespace Graphics.GUI.Interfaces
{
    /// <summary>
    /// Provides font for support for controls
    /// </summary>
    public interface IFont
    {
        #region Properties

        /// <summary>
        /// Font's Name
        /// </summary>
        string Font { get; set; }

        /// <summary>
        /// Color of font
        /// </summary>
        Color FontColor { get; set; }

        #endregion
    }
}
