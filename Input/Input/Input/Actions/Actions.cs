// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Actions.cs Version: 1.0 Last Edited: 6/26/2012
// ------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Input.Input.Actions
{
    public class Action
    {  
        #region Fields

        /// <summary>
        /// Keyboard Keys
        /// </summary>
        public List<CustomKey> Keys;

        #endregion
        
        #region Constructor

        /// <summary>
        /// Constructor for our Custom Keys, its empty for Serialization
        /// </summary>
        public Action()
        {
            Keys = new List<CustomKey>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if all actions are currently running
        /// </summary>
        /// <returns></returns>
        public bool IsActionRunning()
        {
            return Keys.Count != 0 && Keys.All(k => k.IsActionRunning());
        }

        #endregion
    }
}
