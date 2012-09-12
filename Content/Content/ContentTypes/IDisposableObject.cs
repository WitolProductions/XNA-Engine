// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: IDisposableObject.cs Version: 1.0 Last Edited: 8/21/2012
// ------------------------------------------------------------------------

namespace Content.ContentTypes
{
    public interface IDisposableObject
    {
        #region Fields

        /// <summary>
        /// Gets and Sets if this object is currently loaded
        /// </summary>
        bool Loaded { get; set; }

        /// <summary>
        /// Gets and Sets the timer values for automatically unloading this object
        /// </summary>
        double UnloadTimer { get; set; }

        /// <summary>
        /// Dispose of our Class
        /// </summary>
        void Dispose();

        #endregion
    }
}
