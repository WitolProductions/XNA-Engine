// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Validation.cs Version: 1.0 Last Edited: 8/6/2012
// ------------------------------------------------------------------------

using System;
using System.Linq;
using System.Windows.Forms;
using Input.Global;
using Microsoft.Xna.Framework.Input;

namespace ControlSchemeCreator
{
    public static class Validation
    {

        /// <summary>
        /// Validates that no Action Names are doubled
        /// </summary>
        /// <param name="dataGrid"></param>
        public static void CheckKeyName(DataGridView dataGrid)
        {
            //Create a list of all names already on the grid

            var names = new string[ dataGrid.Rows.Count-1];
            for (var i = 0; i < dataGrid.Rows.Count-1; i++)
            {
                if (dataGrid.Rows[i].IsNewRow) continue;
                //Get the names
                names[i] = (string) (dataGrid.Rows[i].Cells["ActionName"].Value);
            }

            for (var i = 0; i < dataGrid.Rows.Count; i++)
            {
                if (dataGrid.Rows[i].IsNewRow) continue;
                var i1 = i; //Prevents an issue with using it in Where below

                dataGrid.Rows[i].ErrorText =
                    names.Where(an => an == (string) dataGrid.Rows[i1].Cells["ActionName"].Value).Count() >= 2
                        ? "Cannot have duplicate Action Names"
                        : string.Empty;
            }
        }

        /// <summary>
        /// Validates that Modififers are correctly specified
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="?"></param>
        public static string CheckModifiers(DataGridView dataGrid, int i)
        {
            //Get a list of possible names that will work with out Enumerations
            var keyboardMods = Enum.GetNames(typeof (Enumeration.KeyboardModiferKeys));
            var mouseMods = Enum.GetNames(typeof(Enumeration.MouseButtons));
            var controllerMods = Enum.GetNames(typeof(Buttons));

            //Create a variable for return error text
            var errorText = string.Empty;
            
            //Check each type of modifier and add to our error if an error exists
            if ((string)dataGrid["KeyboardModifiers", i].Value != null)
            {
                var keyboardModifiers = ((string)dataGrid["KeyboardModifiers", i].Value).Replace(" ", "").Split(',');
                errorText += keyboardModifiers.Where(km => !keyboardMods.Contains(km)).Aggregate(errorText, (current, km) => current + (km + " does not belong to Keyboard Modififers;"));
            }

            if ((string)dataGrid["MouseModifiers", i].Value != null)
            {
                var mouseModifiers = ((string)dataGrid["MouseModifiers", i].Value).Replace(" ", "").Split(',');
                errorText += mouseModifiers.Where(km => !mouseMods.Contains(km)).Aggregate(errorText, (current, km) => current + (km + " does not belong to Mouse Modififers;"));
            }
            
            if ((string)dataGrid["ControllerModifiers", i].Value != null)
            {
                var controllerModifiers = ((string)dataGrid["ControllerModifiers", i].Value).Replace(" ", "").Split(',');
                errorText += controllerModifiers.Where(km => !controllerMods.Contains(km)).Aggregate(errorText, (current, km) => current + (km + " does not belong to Controller Modififers;"));
            }

            if (errorText != string.Empty)
                errorText += " Use a comma (',') seperated list of possible buttons.";

            //Send our Text back
            return errorText;
        }
    }
}

