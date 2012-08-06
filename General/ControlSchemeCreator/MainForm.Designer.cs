namespace ControlSchemeCreator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DataGrid = new System.Windows.Forms.DataGridView();
            this.ActionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeyboardKey = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.KeyboardKeyState = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.KeyboardModifiers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeyboardEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MouseKey = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.MouseKeyState = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.MouseModifiers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MouseEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ControllerKey = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ControllerKeyState = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ControllerPlayerIndex = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ControllerModifiers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ControllerEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.enableMouse = new System.Windows.Forms.CheckBox();
            this.enableController = new System.Windows.Forms.CheckBox();
            this.enableWindowsPhone = new System.Windows.Forms.CheckBox();
            this.enableKeyboard = new System.Windows.Forms.CheckBox();
            this.exportAsBinaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGrid
            // 
            this.DataGrid.AllowUserToOrderColumns = true;
            this.DataGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ActionName,
            this.KeyboardKey,
            this.KeyboardKeyState,
            this.KeyboardModifiers,
            this.KeyboardEnabled,
            this.MouseKey,
            this.MouseKeyState,
            this.MouseModifiers,
            this.MouseEnabled,
            this.ControllerKey,
            this.ControllerKeyState,
            this.ControllerPlayerIndex,
            this.ControllerModifiers,
            this.ControllerEnabled});
            this.DataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGrid.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.DataGrid.Location = new System.Drawing.Point(0, 24);
            this.DataGrid.Name = "DataGrid";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGrid.Size = new System.Drawing.Size(1461, 674);
            this.DataGrid.TabIndex = 0;
            this.DataGrid.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridCellValidated);
            this.DataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridCellValueChanged);
            this.DataGrid.CurrentCellDirtyStateChanged += new System.EventHandler(this.DataGridCurrentCellDirtyStateChanged);
            this.DataGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridDataError);
            this.DataGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DataGridRowsAdded);
            // 
            // ActionName
            // 
            this.ActionName.HeaderText = "Action Name";
            this.ActionName.Name = "ActionName";
            // 
            // KeyboardKey
            // 
            this.KeyboardKey.HeaderText = "Keyboard Key";
            this.KeyboardKey.Name = "KeyboardKey";
            // 
            // KeyboardKeyState
            // 
            this.KeyboardKeyState.HeaderText = "Keyboard Key State";
            this.KeyboardKeyState.Name = "KeyboardKeyState";
            // 
            // KeyboardModifiers
            // 
            this.KeyboardModifiers.HeaderText = "Keyboard Modifiers";
            this.KeyboardModifiers.Name = "KeyboardModifiers";
            // 
            // KeyboardEnabled
            // 
            this.KeyboardEnabled.HeaderText = "Keyboard Enabled";
            this.KeyboardEnabled.Name = "KeyboardEnabled";
            // 
            // MouseKey
            // 
            this.MouseKey.HeaderText = "Mouse Key";
            this.MouseKey.Name = "MouseKey";
            // 
            // MouseKeyState
            // 
            this.MouseKeyState.HeaderText = "Mouse Key State";
            this.MouseKeyState.Name = "MouseKeyState";
            // 
            // MouseModifiers
            // 
            this.MouseModifiers.HeaderText = "Mouse Modifiers";
            this.MouseModifiers.Name = "MouseModifiers";
            // 
            // MouseEnabled
            // 
            this.MouseEnabled.HeaderText = "Mouse Enabled";
            this.MouseEnabled.Name = "MouseEnabled";
            // 
            // ControllerKey
            // 
            this.ControllerKey.HeaderText = "Controller Key";
            this.ControllerKey.Name = "ControllerKey";
            this.ControllerKey.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ControllerKey.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ControllerKeyState
            // 
            this.ControllerKeyState.HeaderText = "Controller Key State";
            this.ControllerKeyState.Name = "ControllerKeyState";
            // 
            // ControllerPlayerIndex
            // 
            this.ControllerPlayerIndex.HeaderText = "Controller Player Index";
            this.ControllerPlayerIndex.Name = "ControllerPlayerIndex";
            // 
            // ControllerModifiers
            // 
            this.ControllerModifiers.HeaderText = "Controller Modifiers";
            this.ControllerModifiers.Name = "ControllerModifiers";
            // 
            // ControllerEnabled
            // 
            this.ControllerEnabled.HeaderText = "Controller Enabled";
            this.ControllerEnabled.Name = "ControllerEnabled";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1461, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exportAsBinaryToolStripMenuItem,
            this.toolStripSeparator1,
            this.loadToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.saveAsToolStripMenuItem.Text = "Save As..";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(156, 6);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItemClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(156, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // enableMouse
            // 
            this.enableMouse.AutoSize = true;
            this.enableMouse.Checked = true;
            this.enableMouse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableMouse.Location = new System.Drawing.Point(159, 4);
            this.enableMouse.Name = "enableMouse";
            this.enableMouse.Size = new System.Drawing.Size(94, 17);
            this.enableMouse.TabIndex = 2;
            this.enableMouse.Text = "Enable Mouse";
            this.enableMouse.UseVisualStyleBackColor = true;
            this.enableMouse.CheckStateChanged += new System.EventHandler(this.EnableKeyboardCheckStateChanged);
            // 
            // enableController
            // 
            this.enableController.AutoSize = true;
            this.enableController.Checked = true;
            this.enableController.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableController.Location = new System.Drawing.Point(259, 4);
            this.enableController.Name = "enableController";
            this.enableController.Size = new System.Drawing.Size(106, 17);
            this.enableController.TabIndex = 3;
            this.enableController.Text = "Enable Controller";
            this.enableController.UseVisualStyleBackColor = true;
            this.enableController.CheckStateChanged += new System.EventHandler(this.EnableKeyboardCheckStateChanged);
            // 
            // enableWindowsPhone
            // 
            this.enableWindowsPhone.AutoSize = true;
            this.enableWindowsPhone.Checked = true;
            this.enableWindowsPhone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableWindowsPhone.Location = new System.Drawing.Point(371, 4);
            this.enableWindowsPhone.Name = "enableWindowsPhone";
            this.enableWindowsPhone.Size = new System.Drawing.Size(140, 17);
            this.enableWindowsPhone.TabIndex = 4;
            this.enableWindowsPhone.Text = "Enable Windows Phone";
            this.enableWindowsPhone.UseVisualStyleBackColor = true;
            this.enableWindowsPhone.CheckStateChanged += new System.EventHandler(this.EnableKeyboardCheckStateChanged);
            // 
            // enableKeyboard
            // 
            this.enableKeyboard.AutoSize = true;
            this.enableKeyboard.Checked = true;
            this.enableKeyboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableKeyboard.Location = new System.Drawing.Point(46, 4);
            this.enableKeyboard.Name = "enableKeyboard";
            this.enableKeyboard.Size = new System.Drawing.Size(107, 17);
            this.enableKeyboard.TabIndex = 5;
            this.enableKeyboard.Text = "Enable Keyboard";
            this.enableKeyboard.UseVisualStyleBackColor = true;
            this.enableKeyboard.CheckStateChanged += new System.EventHandler(this.EnableKeyboardCheckStateChanged);
            // 
            // exportAsBinaryToolStripMenuItem
            // 
            this.exportAsBinaryToolStripMenuItem.Name = "exportAsBinaryToolStripMenuItem";
            this.exportAsBinaryToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.exportAsBinaryToolStripMenuItem.Text = "Export As Binary";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1461, 698);
            this.Controls.Add(this.enableKeyboard);
            this.Controls.Add(this.enableWindowsPhone);
            this.Controls.Add(this.enableController);
            this.Controls.Add(this.enableMouse);
            this.Controls.Add(this.DataGrid);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Control Scheme Creator";
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGrid;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.CheckBox enableMouse;
        private System.Windows.Forms.CheckBox enableController;
        private System.Windows.Forms.CheckBox enableWindowsPhone;
        private System.Windows.Forms.CheckBox enableKeyboard;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActionName;
        private System.Windows.Forms.DataGridViewComboBoxColumn KeyboardKey;
        private System.Windows.Forms.DataGridViewComboBoxColumn KeyboardKeyState;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyboardModifiers;
        private System.Windows.Forms.DataGridViewCheckBoxColumn KeyboardEnabled;
        private System.Windows.Forms.DataGridViewComboBoxColumn MouseKey;
        private System.Windows.Forms.DataGridViewComboBoxColumn MouseKeyState;
        private System.Windows.Forms.DataGridViewTextBoxColumn MouseModifiers;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MouseEnabled;
        private System.Windows.Forms.DataGridViewComboBoxColumn ControllerKey;
        private System.Windows.Forms.DataGridViewComboBoxColumn ControllerKeyState;
        private System.Windows.Forms.DataGridViewComboBoxColumn ControllerPlayerIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn ControllerModifiers;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ControllerEnabled;
        private System.Windows.Forms.ToolStripMenuItem exportAsBinaryToolStripMenuItem;
    }
}

