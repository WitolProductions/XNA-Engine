using Microsoft.Xna.Framework;

namespace Graphics.GUI.Interfaces
{
    public interface IFont
    {
        #region Fields

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
