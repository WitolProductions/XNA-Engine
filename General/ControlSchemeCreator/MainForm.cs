// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: MainForm.cs Version: 1.0 Last Edited: 7/24/2012
// ------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Input.Input.Actions;
using Input.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Action = Input.Input.Actions.Action;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace ControlSchemeCreator
{
    public partial class MainForm : Form
    {
        #region Properties

        string Path { get; set; }
        ActionHandler ActionHandler { get; set; }

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();

            ActionHandler = new ActionHandler();
            
            Path = String.Empty; //Set our save path to empty

            //Setup our Datagrid by databinding some columns and other things
            SetupDataGrid();
            
            //Set our Initial rows defualt values
            DataGridRowsAdded(null, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        #endregion

        #region Data Grid Methods

        /// <summary>
        /// Initial setup our DataGrid
        /// </summary>
        void SetupDataGrid()
        {
            //Load all our Enumerations into our DataGrid as defualts to use per column
            var keyState = Enum.GetValues(typeof(Enumeration.KeyState));

            //Setup our Key Data Sources as well as our Value Types
            KeyboardKey.DataSource = Enum.GetValues(typeof(Keys));
            KeyboardKey.ValueType = typeof (Keys);
            MouseKey.DataSource = Enum.GetValues(typeof(Enumeration.MouseButtons));
            MouseKey.ValueType = typeof (Enumeration.MouseButtons);
            ControllerKey.DataSource = Enum.GetValues(typeof(Buttons));
            ControllerKey.ValueType = typeof (Buttons);

            //Setup our Key States
            KeyboardKeyState.DataSource = keyState;
            KeyboardKeyState.ValueType = typeof (Enumeration.KeyState);
            MouseKeyState.DataSource = keyState;
            MouseKeyState.ValueType = typeof(Enumeration.KeyState);
            ControllerKeyState.DataSource = keyState;
            ControllerKeyState.ValueType = typeof(Enumeration.KeyState);
            ControllerPlayerIndex.DataSource = Enum.GetValues(typeof (PlayerIndex));
            ControllerPlayerIndex.ValueType = typeof (PlayerIndex);

            KeyboardModifiers.ValueType = typeof(string);
            MouseModifiers.ValueType = typeof(string);
            ControllerModifiers.ValueType = typeof (string);
        }

        /// <summary>
        /// Saves the data inside our Data Grid to our ActionHandler
        /// </summary>
        void SaveDataGrid()
        {
            ActionHandler.Actions.Clear();

            for (var i = 0; i < DataGrid.Rows.Count - 1; i++)
            {
                var action = new Action();

                var r = DataGrid.Rows[i];

                //If our ActionName is null or empty lets skip it
                if (string.IsNullOrEmpty((string)r.Cells["ActionName"].Value)) continue;
                
                #region Handle Keyboard Stuffs

                if ((bool) r.Cells["KeyboardEnabled"].Value && enableKeyboard.Checked)
                {
                    action.Key.KeyboardKey = (Keys)r.Cells["KeyboardKey"].Value;
                    action.Key.KeyboardKeyState = (Enumeration.KeyState)r.Cells["KeyboardKeyState"].Value;

                    //Next check determines if we should either null our Modifier value or if we should actually attempt to use it
                    if (String.IsNullOrEmpty(r.ErrorText) && !String.IsNullOrEmpty((string)r.Cells["KeyboardModifiers"].Value))
                    {
                        //Create a new modifier list
                        action.Key.KeyboardKeyModifiers = new List<Enumeration.KeyboardModiferKeys>();
                        //Create a reference list of all values in our enum
                        var keys = Enum.GetNames(typeof (Enumeration.KeyboardModiferKeys));
                        //Loop through each enum in our cell and ensure we can use them before adding them to our Action Handler
                        foreach (var s in ((string) r.Cells["KeyboardModifiers"].Value).Split(',').Where(s => Array.Exists(keys, k => k.ToLower().Contains(s)) && !String.IsNullOrEmpty(s)))
                            action.Key.KeyboardKeyModifiers.Add((Enumeration.KeyboardModiferKeys)Enum.Parse(typeof(Enumeration.KeyboardModiferKeys), s, true));
                    }
                    else
                        //Else null the modifier list
                        action.Key.KeyboardKeyModifiers = null;
                }
                else
                {
                    //Else all is null
                    action.Key.KeyboardKey = null;
                    action.Key.KeyboardKeyState = null;
                    action.Key.KeyboardKeyModifiers = null;
                }

                #endregion

                #region Handle Mouse Stuffs

                if (enableMouse.Checked && (bool)r.Cells["MouseEnabled"].Value)
                {
                    action.Key.MouseKey = (Enumeration.MouseButtons)r.Cells["MouseKey"].Value;
                    action.Key.MouseKeyState = (Enumeration.KeyState)r.Cells["MouseKeyState"].Value;

                    if (String.IsNullOrEmpty(r.ErrorText) && !String.IsNullOrEmpty((string)r.Cells["MouseModifiers"].Value))
                    {
                        action.Key.MouseKeyModifiers = new List<Enumeration.MouseButtons>();
                        var keys = Enum.GetNames(typeof(Enumeration.MouseButtons));

                        foreach (var s in ((string)r.Cells["MouseModifiers"].Value).Split(',').Where(s => Array.Exists(keys, k => k.ToLower().Contains(s)) && !String.IsNullOrEmpty(s)))
                            action.Key.MouseKeyModifiers.Add((Enumeration.MouseButtons)Enum.Parse(typeof(Enumeration.MouseButtons), s, true));
                    }
                    else
                        action.Key.MouseKeyModifiers = null;
                }
                else
                {
                    action.Key.MouseKey = null;
                    action.Key.MouseKeyState = null;
                    action.Key.MouseKeyModifiers = null;
                }

                #endregion

                #region Controller Stuffs

                if ((bool)r.Cells["ControllerEnabled"].Value && enableController.Checked)
                {
                    action.Key.ControllerButton = (Buttons)r.Cells["ControllerKey"].Value;
                    action.Key.ControllerButtonState = (Enumeration.KeyState)r.Cells["ControllerKeyState"].Value;

                    if (String.IsNullOrEmpty(r.ErrorText) && !String.IsNullOrEmpty((string)r.Cells["ControllerModifiers"].Value))
                    {
                        action.Key.ControllerButtonModifiers = new List<Buttons>();
                        var keys = Enum.GetNames(typeof(Buttons));

                        foreach (var s in ((string)r.Cells["ControllerModifiers"].Value).Split(',').Where(s => Array.Exists(keys, k => k.ToLower().Contains(s)) && !String.IsNullOrEmpty(s)))
                            action.Key.ControllerButtonModifiers.Add((Buttons)Enum.Parse(typeof(Buttons), s, true));
                    }
                    else
                        action.Key.ControllerButtonModifiers = null;
                }
                else
                {
                    action.Key.ControllerButton = null;
                    action.Key.ControllerButtonState = null;
                    action.Key.ControllerPlayerIndex = null;
                    action.Key.ControllerButtonModifiers = null;
                }

                #endregion

                #region Windows Phone Stuffs

                #endregion

                ActionHandler.Add((string) r.Cells["ActionName"].Value, action);
            }

            UpdateCellStates();
        }

        /// <summary>
        /// Load information in from our ActionHandler
        /// </summary>
        void LoadDataGrid()
        {
            var i = 0;
            foreach (var a in ActionHandler.Actions)
            {
                if (!DataGrid.Rows[i].IsNewRow)
                    DataGrid.Rows.Add();

                DataGrid.Rows[i].Cells["KeyboardKey"].Value = a.Value.Key.KeyboardKey;
                DataGrid.Rows[i].Cells["KeyboardKeyState"].Value = a.Value.Key.KeyboardKeyState;
                DataGrid.Rows[i].Cells["MouseKey"].Value = a.Value.Key.MouseKey;
                DataGrid.Rows[i].Cells["MouseKeyState"].Value = a.Value.Key.MouseKeyState;
                DataGrid.Rows[i].Cells["ControllerKey"].Value = a.Value.Key.ControllerButton;
                DataGrid.Rows[i].Cells["ControllerKeyState"].Value = a.Value.Key.ControllerButtonState;

                i++;
            }
        }

        #endregion

        #region Update Methods

        /// <summary>
        /// Updates all cells to either be enabled or disabled based on if the checkboxes controlling them are checked or not
        /// </summary>
        void UpdateCellStates()
        {
            for (var i = 0; i < DataGrid.Rows.Count; i++)
            {
                if (enableKeyboard.Checked)
                    AlterCells(new[] { "KeyboardKey", "KeyboardKeyState", "KeyboardModifiers" }, (bool)DataGrid.Rows[i].Cells["KeyboardEnabled"].Value, i);
                else
                    AlterCells(new[] { "KeyboardKey", "KeyboardKeyState", "KeyboardModifiers" }, false, i);

                if (enableController.Checked)
                    AlterCells(new[] { "ControllerKey", "ControllerKeyState", "ControllerPlayerIndex", "ControllerModifiers" }, (bool)DataGrid.Rows[i].Cells["ControllerEnabled"].Value, i);
                else
                    AlterCells(new[] { "ControllerKey", "ControllerKeyState", "ControllerPlayerIndex", "ControllerModifiers" }, false, i);
                
                if (enableMouse.Checked)
                    AlterCells(new[] { "MouseKey", "MouseKeyState", "MouseModifiers" }, (bool)DataGrid.Rows[i].Cells["MouseEnabled"].Value, i);
                else
                    AlterCells(new[] { "MouseKey", "MouseKeyState", "MouseModifiers" }, false, i);
            }
        }


        /// <summary>
        ///  Alter cells to either be enabled or disabled based on info passed
        /// </summary>        
        /// <param name="cells">List of strings that hold Cell names to disbale or enable</param>
        /// <param name="check">Value to check against if we should enable or disable. True: Enable, False: Disable</param>
        /// <param name="rowIndex">Row index to alter</param>
        void AlterCells(IEnumerable<string> cells, bool check, int rowIndex)
        {
            foreach(var c in cells)
            {
                if (DataGrid[c, rowIndex].ValueType == typeof(string))
                {
                    //If we are dealing with a text box
                    var cell = (DataGridViewTextBoxCell)DataGrid[c, rowIndex];
                    //Simply set the cell to readonly and it cant be altered
                    cell.ReadOnly = !check;
                }
                else
                {
                    //Else we are dealing with a Combo box
                    var cell = (DataGridViewComboBoxCell) DataGrid[c, rowIndex];
                    //Basically change the state of the combo box to now allow it to be used and set its value to readonly
                    cell.DisplayStyle = check
                                            ? DataGridViewComboBoxDisplayStyle.DropDownButton : 
                                            DataGridViewComboBoxDisplayStyle.Nothing;

                    cell.ReadOnly = !check;
                }
            }
        }

        #endregion

        #region Handle Saving and Loading

        /// <summary>
        /// Handle saving our Control Scheme
        /// </summary>
        void HandleSave(bool saveAs)
        {
            //As the data to be updated so we ensure we save everything
            SaveDataGrid();

            if (saveAs)
            {
                //If we try and Save As..
                saveFileDialog1.DefaultExt = ".xml";
                saveFileDialog1.Title = @"Choose where you want to save the Control Scheme too:";
                saveFileDialog1.Filter = @"Xml File (*.xml)|*.xml| All Files (*.*)|*.*";
                saveFileDialog1.ShowDialog();

                if (String.IsNullOrEmpty(saveFileDialog1.FileName))
                    if (!File.Exists(saveFileDialog1.FileName))
                        File.Create(saveFileDialog1.FileName).Close();

                ActionHandler.Save(saveFileDialog1.FileName);

                Path = saveFileDialog1.FileName;
            }
            else
            { 
                //If our Path is empty then do not attempt to save
                if (Path == string.Empty) return;
                //Else we try and Save
                ActionHandler.Save(Path);
            }
        }

        /// <summary>
        /// Handle loading our Control Scheme
        /// </summary>
        void HandleLoad()
        {
            try
            {
                openFileDialog1.DefaultExt = ".xml";
                openFileDialog1.Title = @"Choose the file you would like to attempt to open";
                openFileDialog1.Filter = @"Xml File (*.xml)|*.xml| All Files (*.*)|*.*";
                openFileDialog1.ShowDialog();
                
                //Unhook our Update event until we finish loading in data
                DataGrid.CellValueChanged -= DataGridCellValueChanged;
                
                ActionHandler.Load(openFileDialog1.FileName);
                Path = openFileDialog1.FileName;

                //Next load our first actions datagrid information
                LoadDataGrid();
                
                //Hook our even back in so we can continue to update our list
                DataGrid.CellValueChanged += DataGridCellValueChanged;
            }
            catch (Exception e)
            {//Display any issues that we have loading a file
                MessageBox.Show(@"Error loading file: " + e.Message);
            }
        }

        #endregion

        #region Events
        
        private void DataGridCellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //Handles checking validation for each row
            Validation.CheckKeyName(DataGrid);
            for (var i = 0; i < DataGrid.Rows.Count - 1; i++)
                if (!DataGrid.Rows[i].IsNewRow)
                    DataGrid.Rows[i].ErrorText = Validation.CheckModifiers(DataGrid, i);
        }

        private void DataGridRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //Unhook our Update event until we finish loading in data
            DataGrid.CellValueChanged -= DataGridCellValueChanged;

            //Set each row's defualt values
            DataGrid.Rows[e.RowIndex].Cells["KeyboardKey"].Value = Keys.None;
            DataGrid.Rows[e.RowIndex].Cells["KeyboardKeyState"].Value = Enumeration.KeyState.KeyUp;
            DataGrid.Rows[e.RowIndex].Cells["KeyboardEnabled"].Value = true;
            DataGrid.Rows[e.RowIndex].Cells["MouseKey"].Value = Enumeration.MouseButtons.Left;
            DataGrid.Rows[e.RowIndex].Cells["MouseKeyState"].Value = Enumeration.KeyState.KeyUp;
            DataGrid.Rows[e.RowIndex].Cells["MouseEnabled"].Value = true;
            DataGrid.Rows[e.RowIndex].Cells["ControllerKey"].Value = Buttons.DPadDown;
            DataGrid.Rows[e.RowIndex].Cells["ControllerKeyState"].Value = Enumeration.KeyState.KeyUp;
            DataGrid.Rows[e.RowIndex].Cells["ControllerEnabled"].Value = true;
            DataGrid.Rows[e.RowIndex].Cells["ControllerPlayerIndex"].Value = PlayerIndex.One;
            
            
            //Unhook our Update event until we finish loading in data
            DataGrid.CellValueChanged += DataGridCellValueChanged;
        }

        private void DataGridDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Errors are annoying and really are not needed, if an error appears its hanlded inside Validating
            e.Cancel = true;
        }

        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            HandleSave(false);
        }

        private void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
        {
            HandleSave(true);
        }

        private void LoadToolStripMenuItemClick(object sender, EventArgs e)
        {
            HandleLoad();
        }

        private void DataGridCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Update our ActionHandler with information in our DataGrid
            if (e.RowIndex != -1)
                SaveDataGrid();
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DataGridCurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DataGrid.IsCurrentCellDirty) //Determine if we need to commit our data back to our ActionHandler class
                DataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void EnableKeyboardCheckStateChanged(object sender, EventArgs e)
        {
            UpdateCellStates();
        }

        #endregion
    }
}
