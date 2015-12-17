namespace _3DlandMSBTeditor
{
    partial class Form1
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
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
			this.version01ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lstStrings = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.hbxHexView = new Be.Windows.Forms.HexBox();
			this.stsMain = new System.Windows.Forms.StatusStrip();
			this.slbAddress = new System.Windows.Forms.ToolStripStatusLabel();
			this.slbStringCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.txtEdit = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tESTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.stsMain.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.lZCompressionToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.tESTToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(844, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
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
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.loadToolStripMenuItem.Text = "&Open";
			this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveAsToolStripMenuItem.Text = "S&ave as...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
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
			this.compressToolStripMenuItem.Name = "compressToolStripMenuItem";
			this.compressToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.compressToolStripMenuItem.Text = "Compress";
			this.compressToolStripMenuItem.Click += new System.EventHandler(this.compressToolStripMenuItem_Click);
			// 
			// decompressToolStripMenuItem
			// 
			this.decompressToolStripMenuItem.Name = "decompressToolStripMenuItem";
			this.decompressToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.decompressToolStripMenuItem.Text = "Decompress";
			this.decompressToolStripMenuItem.Click += new System.EventHandler(this.decompressToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.version01ToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// version01ToolStripMenuItem
			// 
			this.version01ToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
			this.version01ToolStripMenuItem.Name = "version01ToolStripMenuItem";
			this.version01ToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.version01ToolStripMenuItem.Text = "&About";
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
			this.lstStrings.Size = new System.Drawing.Size(231, 451);
			this.lstStrings.Sorted = true;
			this.lstStrings.TabIndex = 1;
			this.lstStrings.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
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
			this.label2.Location = new System.Drawing.Point(252, 28);
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
			this.label3.Location = new System.Drawing.Point(252, 300);
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
			this.hbxHexView.Location = new System.Drawing.Point(255, 321);
			this.hbxHexView.Margin = new System.Windows.Forms.Padding(4);
			this.hbxHexView.Name = "hbxHexView";
			this.hbxHexView.ReadOnly = true;
			this.hbxHexView.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
			this.hbxHexView.Size = new System.Drawing.Size(576, 179);
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
			this.stsMain.Size = new System.Drawing.Size(844, 22);
			this.stsMain.TabIndex = 11;
			// 
			// slbAddress
			// 
			this.slbAddress.Name = "slbAddress";
			this.slbAddress.Size = new System.Drawing.Size(414, 17);
			this.slbAddress.Spring = true;
			this.slbAddress.Text = "Address";
			this.slbAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// slbStringCount
			// 
			this.slbStringCount.Name = "slbStringCount";
			this.slbStringCount.Size = new System.Drawing.Size(414, 17);
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
			this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtEdit, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(255, 49);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(576, 244);
			this.tableLayoutPanel1.TabIndex = 12;
			// 
			// txtEdit
			// 
			this.txtEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtEdit.Enabled = false;
			this.txtEdit.Location = new System.Drawing.Point(0, 0);
			this.txtEdit.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
			this.txtEdit.Multiline = true;
			this.txtEdit.Name = "txtEdit";
			this.txtEdit.Size = new System.Drawing.Size(283, 244);
			this.txtEdit.TabIndex = 4;
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(293, 0);
			this.textBox1.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(283, 244);
			this.textBox1.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(545, 32);
			this.label4.Margin = new System.Windows.Forms.Padding(4);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "Original:";
			// 
			// tESTToolStripMenuItem
			// 
			this.tESTToolStripMenuItem.Name = "tESTToolStripMenuItem";
			this.tESTToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
			this.tESTToolStripMenuItem.Text = "TEST";
			this.tESTToolStripMenuItem.Click += new System.EventHandler(this.tESTToolStripMenuItem_Click_1);
			// 
			// Form1
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(844, 536);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.stsMain);
			this.Controls.Add(this.hbxHexView);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstStrings);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(860, 574);
			this.Name = "Form1";
			this.Text = "MSBT Editor";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.stsMain.ResumeLayout(false);
			this.stsMain.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
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
		  private System.Windows.Forms.ToolStripMenuItem version01ToolStripMenuItem;
		  private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		  private System.Windows.Forms.ToolStripStatusLabel slbAddress;
		  private System.Windows.Forms.ToolStripStatusLabel slbStringCount;
		  private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		  private System.Windows.Forms.TextBox textBox1;
		  private System.Windows.Forms.TextBox txtEdit;
		  private System.Windows.Forms.Label label4;
		  private System.Windows.Forms.ToolStripMenuItem tESTToolStripMenuItem;
    }
}

