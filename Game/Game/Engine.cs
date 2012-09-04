// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Engine.cs Version: 1.0 Last Edited: 9/4/2012
// ------------------------------------------------------------------------

using System;

namespace Engine
{
    /// <summary>
    /// Main entrance into our Engine that can be ran using Engine.Run(InheritedTheGameClass);  
    /// </summary>
    public class Engine : IDisposable
    {
        #region Fields

        public static TheGame Game;

        #endregion
        
        #region Constructor

        /// <summary>
        /// Initiate anything that should be initiated at Engine run time
        /// </summary>
        Engine()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Run the game
        /// </summary>
        public static void Run(TheGame game)
        {
            //Run our game in an environment that ensures full shut down if we exit out of this statment
            using (Game = game)
                Game.Run();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (Game != null)
                Game.Dispose();
        }

        #endregion
    }
}
