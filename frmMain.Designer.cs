namespace MsbtEditor
{
    partial class frmMain
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
			this.mnuMain = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.BG4ExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.compressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.decompressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lstStrings = new System.Windows.Forms.ListBox();
			this.lblStrings = new System.Windows.Forms.Label();
			this.lblEdit = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.hbxHexView = new Be.Windows.Forms.HexBox();
			this.stsMain = new System.Windows.Forms.StatusStrip();
			this.slbAddress = new System.Windows.Forms.ToolStripStatusLabel();
			this.slbActions = new System.Windows.Forms.ToolStripStatusLabel();
			this.slbStringCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.txtOriginal = new System.Windows.Forms.TextBox();
			this.txtEdit = new System.Windows.Forms.TextBox();
			this.lstSubStrings = new System.Windows.Forms.ListBox();
			this.lblOriginal = new System.Windows.Forms.Label();
			this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.sfdSaveEntity = new System.Windows.Forms.SaveFileDialog();
			this.lblSubStrings = new System.Windows.Forms.Label();
			this.txtConcatenated = new System.Windows.Forms.TextBox();
			this.mnuMain.SuspendLayout();
			this.stsMain.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mnuMain
			// 
			this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.mnuMain.Location = new System.Drawing.Point(0, 0);
			this.mnuMain.Name = "mnuMain";
			this.mnuMain.Size = new System.Drawing.Size(854, 24);
			this.mnuMain.TabIndex = 0;
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = global::MsbtEditor.Properties.Resources.menu_open;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = global::MsbtEditor.Properties.Resources.menu_save;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Image = global::MsbtEditor.Properties.Resources.menu_save_as;
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.saveAsToolStripMenuItem.Text = "S&ave as...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Image = global::MsbtEditor.Properties.Resources.menu_exit;
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BG4ExplorerToolStripMenuItem,
            this.compressToolStripMenuItem,
            this.decompressToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// BG4ExplorerToolStripMenuItem
			// 
			this.BG4ExplorerToolStripMenuItem.Image = global::MsbtEditor.Properties.Resources.menu_export;
			this.BG4ExplorerToolStripMenuItem.Name = "BG4ExplorerToolStripMenuItem";
			this.BG4ExplorerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
			this.BG4ExplorerToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.BG4ExplorerToolStripMenuItem.Text = "BG4 &Extract";
			this.BG4ExplorerToolStripMenuItem.Click += new System.EventHandler(this.BG4ExplorerToolStripMenuItem_Click);
			// 
			// compressToolStripMenuItem
			// 
			this.compressToolStripMenuItem.Image = global::MsbtEditor.Properties.Resources.tab_class;
			this.compressToolStripMenuItem.Name = "compressToolStripMenuItem";
			this.compressToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
			this.compressToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.compressToolStripMenuItem.Text = "LZ11 &Compress";
			this.compressToolStripMenuItem.Click += new System.EventHandler(this.compressToolStripMenuItem_Click);
			// 
			// decompressToolStripMenuItem
			// 
			this.decompressToolStripMenuItem.Image = global::MsbtEditor.Properties.Resources.tab_class;
			this.decompressToolStripMenuItem.Name = "decompressToolStripMenuItem";
			this.decompressToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
			this.decompressToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.decompressToolStripMenuItem.Text = "LZ11 &Decompress";
			this.decompressToolStripMenuItem.Click += new System.EventHandler(this.decompressToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
			this.aboutToolStripMenuItem.Image = global::MsbtEditor.Properties.Resources.menu_about;
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// lstStrings
			// 
			this.lstStrings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstStrings.Enabled = false;
			this.lstStrings.FormattingEnabled = true;
			this.lstStrings.IntegralHeight = false;
			this.lstStrings.Location = new System.Drawing.Point(13, 49);
			this.lstStrings.Margin = new System.Windows.Forms.Padding(4);
			this.lstStrings.Name = "lstStrings";
			this.lstStrings.Size = new System.Drawing.Size(271, 454);
			this.lstStrings.Sorted = true;
			this.lstStrings.TabIndex = 1;
			this.lstStrings.SelectedIndexChanged += new System.EventHandler(this.lstStrings_SelectedIndexChanged);
			// 
			// lblStrings
			// 
			this.lblStrings.AutoSize = true;
			this.lblStrings.Location = new System.Drawing.Point(13, 32);
			this.lblStrings.Margin = new System.Windows.Forms.Padding(4);
			this.lblStrings.Name = "lblStrings";
			this.lblStrings.Size = new System.Drawing.Size(42, 13);
			this.lblStrings.TabIndex = 4;
			this.lblStrings.Text = "Strings:";
			// 
			// lblEdit
			// 
			this.lblEdit.AutoSize = true;
			this.lblEdit.Location = new System.Drawing.Point(341, 32);
			this.lblEdit.Margin = new System.Windows.Forms.Padding(4);
			this.lblEdit.Name = "lblEdit";
			this.lblEdit.Size = new System.Drawing.Size(28, 13);
			this.lblEdit.TabIndex = 5;
			this.lblEdit.Text = "Edit:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(289, 316);
			this.label3.Margin = new System.Windows.Forms.Padding(4);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(55, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Hex View:";
			// 
			// hbxHexView
			// 
			this.hbxHexView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.hbxHexView.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.hbxHexView.Location = new System.Drawing.Point(292, 337);
			this.hbxHexView.Margin = new System.Windows.Forms.Padding(4);
			this.hbxHexView.Name = "hbxHexView";
			this.hbxHexView.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
			this.hbxHexView.Size = new System.Drawing.Size(550, 166);
			this.hbxHexView.StringViewVisible = true;
			this.hbxHexView.TabIndex = 6;
			this.hbxHexView.UseFixedBytesPerLine = true;
			this.hbxHexView.VScrollBarVisible = true;
			this.hbxHexView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hbxSelectAll_KeyDown);
			// 
			// stsMain
			// 
			this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slbAddress,
            this.slbActions,
            this.slbStringCount});
			this.stsMain.Location = new System.Drawing.Point(0, 516);
			this.stsMain.Name = "stsMain";
			this.stsMain.Size = new System.Drawing.Size(854, 22);
			this.stsMain.TabIndex = 11;
			// 
			// slbAddress
			// 
			this.slbAddress.Name = "slbAddress";
			this.slbAddress.Size = new System.Drawing.Size(279, 17);
			this.slbAddress.Spring = true;
			this.slbAddress.Text = "Address";
			this.slbAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// slbActions
			// 
			this.slbActions.Name = "slbActions";
			this.slbActions.Size = new System.Drawing.Size(279, 17);
			this.slbActions.Spring = true;
			this.slbActions.Text = "Actions";
			// 
			// slbStringCount
			// 
			this.slbStringCount.Name = "slbStringCount";
			this.slbStringCount.Size = new System.Drawing.Size(279, 17);
			this.slbStringCount.Spring = true;
			this.slbStringCount.Text = "Count";
			this.slbStringCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.txtOriginal, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtEdit, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.lstSubStrings, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(291, 49);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(551, 157);
			this.tableLayoutPanel1.TabIndex = 12;
			// 
			// txtOriginal
			// 
			this.txtOriginal.BackColor = System.Drawing.SystemColors.Window;
			this.txtOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOriginal.Enabled = false;
			this.txtOriginal.Location = new System.Drawing.Point(303, 0);
			this.txtOriginal.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
			this.txtOriginal.Multiline = true;
			this.txtOriginal.Name = "txtOriginal";
			this.txtOriginal.ReadOnly = true;
			this.txtOriginal.Size = new System.Drawing.Size(248, 157);
			this.txtOriginal.TabIndex = 4;
			this.txtOriginal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSelectAll_KeyDown);
			// 
			// txtEdit
			// 
			this.txtEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtEdit.Enabled = false;
			this.txtEdit.Location = new System.Drawing.Point(52, 0);
			this.txtEdit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.txtEdit.Multiline = true;
			this.txtEdit.Name = "txtEdit";
			this.txtEdit.Size = new System.Drawing.Size(243, 157);
			this.txtEdit.TabIndex = 3;
			this.txtEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSelectAll_KeyDown);
			this.txtEdit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEdit_KeyUp);
			// 
			// lstSubStrings
			// 
			this.lstSubStrings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstSubStrings.FormattingEnabled = true;
			this.lstSubStrings.IntegralHeight = false;
			this.lstSubStrings.Location = new System.Drawing.Point(0, 0);
			this.lstSubStrings.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
			this.lstSubStrings.Name = "lstSubStrings";
			this.lstSubStrings.Size = new System.Drawing.Size(44, 157);
			this.lstSubStrings.TabIndex = 2;
			this.lstSubStrings.SelectedIndexChanged += new System.EventHandler(this.lstSubStrings_SelectedIndexChanged);
			// 
			// lblOriginal
			// 
			this.lblOriginal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblOriginal.Location = new System.Drawing.Point(592, 32);
			this.lblOriginal.Margin = new System.Windows.Forms.Padding(4);
			this.lblOriginal.Name = "lblOriginal";
			this.lblOriginal.Size = new System.Drawing.Size(48, 13);
			this.lblOriginal.TabIndex = 13;
			this.lblOriginal.Text = "Original:";
			this.lblOriginal.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// ofdOpenFile
			// 
			this.ofdOpenFile.Filter = "MSBT Files (*.msbt)|*.msbt|All Files (*.*)|*.*";
			// 
			// sfdSaveEntity
			// 
			this.sfdSaveEntity.Filter = "MSBT Files (*.msbt)|*.msbt";
			// 
			// lblSubStrings
			// 
			this.lblSubStrings.AutoSize = true;
			this.lblSubStrings.Location = new System.Drawing.Point(288, 32);
			this.lblSubStrings.Margin = new System.Windows.Forms.Padding(4);
			this.lblSubStrings.Name = "lblSubStrings";
			this.lblSubStrings.Size = new System.Drawing.Size(29, 13);
			this.lblSubStrings.TabIndex = 14;
			this.lblSubStrings.Text = "Sub:";
			// 
			// txtConcatenated
			// 
			this.txtConcatenated.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtConcatenated.BackColor = System.Drawing.SystemColors.Window;
			this.txtConcatenated.Enabled = false;
			this.txtConcatenated.Location = new System.Drawing.Point(292, 213);
			this.txtConcatenated.Margin = new System.Windows.Forms.Padding(4);
			this.txtConcatenated.Multiline = true;
			this.txtConcatenated.Name = "txtConcatenated";
			this.txtConcatenated.ReadOnly = true;
			this.txtConcatenated.Size = new System.Drawing.Size(549, 95);
			this.txtConcatenated.TabIndex = 5;
			this.txtConcatenated.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSelectAll_KeyDown);
			// 
			// frmMain
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(854, 538);
			this.Controls.Add(this.txtConcatenated);
			this.Controls.Add(this.lblSubStrings);
			this.Controls.Add(this.lblOriginal);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.stsMain);
			this.Controls.Add(this.hbxHexView);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblEdit);
			this.Controls.Add(this.lblStrings);
			this.Controls.Add(this.lstStrings);
			this.Controls.Add(this.mnuMain);
			this.MainMenuStrip = this.mnuMain;
			this.MinimumSize = new System.Drawing.Size(870, 576);
			this.Name = "frmMain";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmMain_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmMain_DragEnter);
			this.mnuMain.ResumeLayout(false);
			this.mnuMain.PerformLayout();
			this.stsMain.ResumeLayout(false);
			this.stsMain.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ListBox lstStrings;
        private System.Windows.Forms.Label lblStrings;
		  private System.Windows.Forms.Label lblEdit;
		  private System.Windows.Forms.Label label3;
        private Be.Windows.Forms.HexBox hbxHexView;
		  private System.Windows.Forms.StatusStrip stsMain;
		  private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		  private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		  private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		  private System.Windows.Forms.ToolStripStatusLabel slbAddress;
		  private System.Windows.Forms.ToolStripStatusLabel slbStringCount;
		  private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		  private System.Windows.Forms.TextBox txtEdit;
		  private System.Windows.Forms.Label lblOriginal;
		  private System.Windows.Forms.OpenFileDialog ofdOpenFile;
		  private System.Windows.Forms.SaveFileDialog sfdSaveEntity;
		  private System.Windows.Forms.TextBox txtOriginal;
		  private System.Windows.Forms.ListBox lstSubStrings;
		  private System.Windows.Forms.Label lblSubStrings;
		  private System.Windows.Forms.TextBox txtConcatenated;
		  private System.Windows.Forms.ToolStripStatusLabel slbActions;
		  private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		  private System.Windows.Forms.ToolStripMenuItem BG4ExplorerToolStripMenuItem;
		  private System.Windows.Forms.ToolStripMenuItem compressToolStripMenuItem;
		  private System.Windows.Forms.ToolStripMenuItem decompressToolStripMenuItem;
    }
}

