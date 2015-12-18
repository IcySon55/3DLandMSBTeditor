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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.mnuMain = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lZCompressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.compressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.decompressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lstStrings = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.hbxHexView = new Be.Windows.Forms.HexBox();
			this.stsMain = new System.Windows.Forms.StatusStrip();
			this.slbAddress = new System.Windows.Forms.ToolStripStatusLabel();
			this.slbStringCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.txtOriginal = new System.Windows.Forms.TextBox();
			this.txtEdit = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.sfdSaveEntity = new System.Windows.Forms.SaveFileDialog();
			this.mnuMain.SuspendLayout();
			this.stsMain.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mnuMain
			// 
			this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.lZCompressionToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.mnuMain.Location = new System.Drawing.Point(0, 0);
			this.mnuMain.Name = "mnuMain";
			this.mnuMain.Size = new System.Drawing.Size(854, 24);
			this.mnuMain.TabIndex = 0;
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// loadToolStripMenuItem
			// 
			this.loadToolStripMenuItem.Image = global::MsbtEditor.Properties.Resources.menu_open;
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.loadToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.loadToolStripMenuItem.Text = "&Open";
			this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
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
			// lZCompressionToolStripMenuItem
			// 
			this.lZCompressionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compressToolStripMenuItem,
            this.decompressToolStripMenuItem});
			this.lZCompressionToolStripMenuItem.Name = "lZCompressionToolStripMenuItem";
			this.lZCompressionToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
			this.lZCompressionToolStripMenuItem.Text = "LZ compression";
			// 
			// compressToolStripMenuItem
			// 
			this.compressToolStripMenuItem.Image = global::MsbtEditor.Properties.Resources.tab_class;
			this.compressToolStripMenuItem.Name = "compressToolStripMenuItem";
			this.compressToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.compressToolStripMenuItem.Text = "Compress";
			this.compressToolStripMenuItem.Click += new System.EventHandler(this.compressToolStripMenuItem_Click);
			// 
			// decompressToolStripMenuItem
			// 
			this.decompressToolStripMenuItem.Image = global::MsbtEditor.Properties.Resources.tab_class;
			this.decompressToolStripMenuItem.Name = "decompressToolStripMenuItem";
			this.decompressToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.decompressToolStripMenuItem.Text = "Decompress";
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
			this.lstStrings.Size = new System.Drawing.Size(267, 453);
			this.lstStrings.Sorted = true;
			this.lstStrings.TabIndex = 1;
			this.lstStrings.SelectedIndexChanged += new System.EventHandler(this.lstStrings_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 28);
			this.label1.Margin = new System.Windows.Forms.Padding(4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Strings:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(288, 32);
			this.label2.Margin = new System.Windows.Forms.Padding(4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(28, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Edit:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(288, 314);
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
			this.hbxHexView.Location = new System.Drawing.Point(291, 335);
			this.hbxHexView.Margin = new System.Windows.Forms.Padding(4);
			this.hbxHexView.Name = "hbxHexView";
			this.hbxHexView.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
			this.hbxHexView.Size = new System.Drawing.Size(551, 167);
			this.hbxHexView.StringViewVisible = true;
			this.hbxHexView.TabIndex = 10;
			this.hbxHexView.UseFixedBytesPerLine = true;
			this.hbxHexView.VScrollBarVisible = true;
			// 
			// stsMain
			// 
			this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slbAddress,
            this.slbStringCount});
			this.stsMain.Location = new System.Drawing.Point(0, 514);
			this.stsMain.Name = "stsMain";
			this.stsMain.Size = new System.Drawing.Size(854, 22);
			this.stsMain.TabIndex = 11;
			// 
			// slbAddress
			// 
			this.slbAddress.Name = "slbAddress";
			this.slbAddress.Size = new System.Drawing.Size(419, 17);
			this.slbAddress.Spring = true;
			this.slbAddress.Text = "Address";
			this.slbAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// slbStringCount
			// 
			this.slbStringCount.Name = "slbStringCount";
			this.slbStringCount.Size = new System.Drawing.Size(419, 17);
			this.slbStringCount.Spring = true;
			this.slbStringCount.Text = "Count";
			this.slbStringCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.txtOriginal, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtEdit, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(291, 49);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(551, 258);
			this.tableLayoutPanel1.TabIndex = 12;
			// 
			// txtOriginal
			// 
			this.txtOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOriginal.Enabled = false;
			this.txtOriginal.Location = new System.Drawing.Point(280, 0);
			this.txtOriginal.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.txtOriginal.Multiline = true;
			this.txtOriginal.Name = "txtOriginal";
			this.txtOriginal.ReadOnly = true;
			this.txtOriginal.Size = new System.Drawing.Size(271, 258);
			this.txtOriginal.TabIndex = 5;
			// 
			// txtEdit
			// 
			this.txtEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtEdit.Enabled = false;
			this.txtEdit.Location = new System.Drawing.Point(0, 0);
			this.txtEdit.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
			this.txtEdit.Multiline = true;
			this.txtEdit.Name = "txtEdit";
			this.txtEdit.Size = new System.Drawing.Size(270, 258);
			this.txtEdit.TabIndex = 4;
			this.txtEdit.TextChanged += new System.EventHandler(this.txtEdit_TextChanged);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Location = new System.Drawing.Point(563, 32);
			this.label4.Margin = new System.Windows.Forms.Padding(4);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "Original:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// frmMain
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(854, 536);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.stsMain);
			this.Controls.Add(this.hbxHexView);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstStrings);
			this.Controls.Add(this.mnuMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mnuMain;
			this.MinimumSize = new System.Drawing.Size(870, 574);
			this.Name = "frmMain";
			this.Text = "MSBT Editor";
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
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		  private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lZCompressionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compressToolStripMenuItem;
		  private System.Windows.Forms.ToolStripMenuItem decompressToolStripMenuItem;
        private System.Windows.Forms.ListBox lstStrings;
        private System.Windows.Forms.Label label1;
		  private System.Windows.Forms.Label label2;
		  private System.Windows.Forms.Label label3;
        private Be.Windows.Forms.HexBox hbxHexView;
		  private System.Windows.Forms.StatusStrip stsMain;
		  private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		  private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		  private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		  private System.Windows.Forms.ToolStripStatusLabel slbAddress;
		  private System.Windows.Forms.ToolStripStatusLabel slbStringCount;
		  private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		  private System.Windows.Forms.TextBox txtOriginal;
		  private System.Windows.Forms.TextBox txtEdit;
		  private System.Windows.Forms.Label label4;
		  private System.Windows.Forms.OpenFileDialog ofdOpenFile;
		  private System.Windows.Forms.SaveFileDialog sfdSaveEntity;
    }
}

