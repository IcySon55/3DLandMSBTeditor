using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MsbtEditor.Properties;
using Be.Windows.Forms;

namespace MsbtEditor
{
	public partial class frmMain : Form
	{
		private MSBT _msbt = null;
		private bool _fileOpen = false;
		private bool _hasChanges = false;

		public frmMain(string[] args)
		{
			InitializeComponent();
			this.Icon = Properties.Resources.msbteditor;

			if (args.Length > 0 && File.Exists(args[0]))
				OpenFile(args[0]);
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			Settings.Default.Upgrade();
			slbAddress.Text = string.Empty;
			slbActions.Text = string.Empty;
			slbStringCount.Text = string.Empty;
			UpdateForm();
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult dr = DialogResult.No;

			if (_fileOpen && _hasChanges)
				dr = MessageBox.Show("You have unsaved changes in " + FileName() + ". Save changes before exiting?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

			switch (dr)
			{
				case DialogResult.Yes:
					SaveFile();
					break;
				case DialogResult.Cancel:
					e.Cancel = true;
					break;
			}

			Settings.Default.Save();
		}

		private void frmMain_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}

		private void frmMain_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			if (files.Length > 0 && File.Exists(files[0]))
				ConfirmOpenFile(files[0]);
		}

		#region Menu and Toolbar

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ConfirmOpenFile();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFile();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFile(true);
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show(Settings.Default.ApplicationName + "\r\nCreated by IcySon55 using Exelix11's original as a base.", "About " + Settings.Default.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		#endregion

		private void ConfirmOpenFile(string filename = "")
		{
			DialogResult dr = DialogResult.No;

			if (_fileOpen && _hasChanges)
				dr = MessageBox.Show("You have unsaved changes in " + FileName() + ". Save changes before opening another file?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

			switch (dr)
			{
				case DialogResult.Yes:
					dr = SaveFile();
					if (dr == DialogResult.OK) OpenFile(filename);
					break;
				case DialogResult.No:
					OpenFile(filename);
					break;
			}
		}

		private void OpenFile(string filename = "")
		{
			ofdOpenFile.InitialDirectory = Settings.Default.InitialDirectory;
			DialogResult dr = DialogResult.OK;

			if (filename != string.Empty)
			{
				_msbt = new MSBT(filename);
				_fileOpen = true;
				_hasChanges = false;
				LoadFile();
				UpdateForm();
				Settings.Default.InitialDirectory = new FileInfo(filename).DirectoryName;
				Settings.Default.Save();
				Settings.Default.Reload();
			}
			else
			{
				dr = ofdOpenFile.ShowDialog();
				if (dr == DialogResult.OK)
				{
					try
					{
						_msbt = new MSBT(ofdOpenFile.FileName);
						_fileOpen = true;
						_hasChanges = false;
						LoadFile();
						Settings.Default.InitialDirectory = new FileInfo(ofdOpenFile.FileName).DirectoryName;
						Settings.Default.Save();
						Settings.Default.Reload();
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK);
						_fileOpen = false;
						_hasChanges = false;
					}
					UpdateForm();
				}
			}
		}

		private void LoadFile()
		{
			lstStrings.Items.Clear();
			lstSubStrings.Items.Clear();

			if (_msbt.HasLabels)
			{
				lstStrings.Sorted = true;
				for (int i = 0; i < _msbt.TXT2.NumberOfStrings; i++)
				{
					lstStrings.Items.Add(_msbt.LBL1.Labels[i]);
				}
			}
			else
			{
				lstStrings.Sorted = false;
				for (int i = 0; i < _msbt.TXT2.NumberOfStrings; i++)
				{
					lstStrings.Items.Add(_msbt.TXT2.Entries[i]);
				}
			}

			if (lstStrings.Items.Count > 0)
				lstStrings.SelectedIndex = 0;

			slbActions.Text = "Successfully opened " + FileName();
			slbStringCount.Text = "Strings in this file: " + _msbt.TXT2.NumberOfStrings;
		}

		private DialogResult SaveFile(bool saveAs = false)
		{
			DialogResult dr = DialogResult.OK;

			if (_msbt.File == null || saveAs)
			{
				sfdSaveEntity.InitialDirectory = Settings.Default.InitialDirectory;
				dr = sfdSaveEntity.ShowDialog();
			}

			if ((_msbt.File == null || saveAs) && dr == DialogResult.OK)
			{
				_msbt.File = new FileInfo(sfdSaveEntity.FileName);
				Settings.Default.InitialDirectory = new FileInfo(sfdSaveEntity.FileName).DirectoryName;
				Settings.Default.Save();
				Settings.Default.Reload();
			}

			if (dr == DialogResult.OK)
			{
				_msbt.Save();
				_hasChanges = false;
				UpdateTextView();
				UpdateOriginalText();
				UpdateTextPreview();
				UpdateHexView();
				UpdateForm();
			}

			slbActions.Text = "Successfully saved " + FileName();

			return dr;
		}

		private void lstStrings_SelectedIndexChanged(object sender, EventArgs e)
		{
			Entry entry = (Entry)lstStrings.SelectedItem;

			lstSubStrings.Items.Clear();
			for (int i = 0; i < _msbt.TXT2.Entries[entry.ID].Values.Count; i++)
			{
				Entry subEntry = new Entry();
				subEntry.ID = i;

				lstSubStrings.Items.Add(subEntry);

				if (lstSubStrings.Items.Count > 0)
					lstSubStrings.SelectedIndex = 0;
			}
		}

		private void lstSubStrings_SelectedIndexChanged(object sender, EventArgs e)
		{
			Entry entry = (Entry)lstStrings.SelectedItem;
			Value val = _msbt.TXT2.Entries[entry.ID].Values[lstSubStrings.SelectedIndex];

			txtEdit.Enabled = val.Editable;
			txtOriginal.Enabled = val.Editable;
			txtConcatenated.Enabled = val.Editable;
			hbxHexView.Enabled = val.Editable;

			UpdateTextView();
			UpdateOriginalText();
			UpdateTextPreview();
			UpdateHexView();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Tab || keyData == (Keys.Tab | Keys.Shift))
			{
				if (keyData == (Keys.Tab | Keys.Shift))
				{
					if (lstSubStrings.Items.Count > 1 && lstSubStrings.SelectedIndex - 1 >= 0)
						lstSubStrings.SelectedIndex -= 1;
					else
					{
						lstStrings.SelectedIndex -= (lstStrings.SelectedIndex - 1 >= 0 ? 1 : (lstStrings.Items.Count - 1) * -1);
						if (lstSubStrings.Items.Count > 1)
							lstSubStrings.SelectedIndex = lstSubStrings.Items.Count - 1;
					}
				}
				else
				{
					if (lstSubStrings.Items.Count > 1 && lstSubStrings.SelectedIndex + 1 < lstSubStrings.Items.Count)
						lstSubStrings.SelectedIndex += 1;
					else
					{
						lstStrings.SelectedIndex += (lstStrings.SelectedIndex + 1 < lstStrings.Items.Count ? 1 : (lstStrings.Items.Count - 1) * -1);
						if (lstSubStrings.Items.Count > 1)
							lstSubStrings.SelectedIndex = 0;
					}
				}

				return true;
			}
			else
				return base.ProcessCmdKey(ref msg, keyData);
		}

		private void txtSelectAll_KeyDown(object sender, KeyEventArgs e)
		{
			TextBox txtBox = (TextBox)sender;
			if (e.Control && e.KeyCode == Keys.A)
			{
				txtBox.SelectAll();
				e.SuppressKeyPress = true;
			}
		}

		private void hbxSelectAll_KeyDown(object sender, KeyEventArgs e)
		{
			HexBox hbxBox = (HexBox)sender;
			if (e.Control && e.KeyCode == Keys.A)
			{
				hbxBox.SelectAll();
				e.SuppressKeyPress = true;
			}
		}

		private void txtEdit_KeyUp(object sender, KeyEventArgs e)
		{
			string result = txtEdit.Text;

			Entry entry = (Entry)lstStrings.SelectedItem;
			_msbt.TXT2.Entries[entry.ID].Values[lstSubStrings.SelectedIndex].Data = Encoding.Unicode.GetBytes(result.Replace("\r\n", "\n"));

			if (txtEdit.Text != txtOriginal.Text)
				_hasChanges = true;

			UpdateTextPreview();
			UpdateHexView();
			UpdateForm();
		}

		private void UpdateTextView()
		{
			Entry entry = (Entry)lstStrings.SelectedItem;

			txtEdit.Text = Encoding.Unicode.GetString(_msbt.TXT2.Entries[entry.ID].Values[lstSubStrings.SelectedIndex].Data).Replace("\n", "\r\n");

			slbAddress.Text = "String: " + (entry.ID + 1) + "/" + (lstSubStrings.SelectedIndex + 1);
		}

		private void UpdateOriginalText()
		{
			Entry entry = (Entry)lstStrings.SelectedItem;

			txtOriginal.Text = Encoding.Unicode.GetString(_msbt.TXT2.OriginalEntries[entry.ID].Values[lstSubStrings.SelectedIndex].Data).Replace("\n", "\r\n");
		}

		private void UpdateTextPreview()
		{
			Entry entry = (Entry)lstStrings.SelectedItem;

			string result = string.Empty;
			foreach (Value value in _msbt.TXT2.Entries[entry.ID].Values)
				result += Encoding.Unicode.GetString(value.Data).Replace("\n", "\r\n");

			txtConcatenated.Text = result;
		}

		protected void byteProvider_Changed(object sender, EventArgs e)
		{
			DynamicFileByteProvider dfbp = (DynamicFileByteProvider)sender;

			Entry entry = (Entry)lstStrings.SelectedItem;
			List<byte> bytes = new List<byte>();
			for (int i = 0; i < (int)dfbp.Length; i++)
				bytes.Add(dfbp.ReadByte(i));
			_msbt.TXT2.Entries[entry.ID].Values[lstSubStrings.SelectedIndex].Data = bytes.ToArray();

			UpdateTextView();
			UpdateTextPreview();
			UpdateForm();

			if (txtEdit.Text != txtOriginal.Text)
				_hasChanges = true;
		}

		private void UpdateHexView()
		{
			DynamicFileByteProvider dfbp = null;

			try
			{
				Entry entry = (Entry)lstStrings.SelectedItem;
				MemoryStream strm = new MemoryStream(_msbt.TXT2.Entries[entry.ID].Values[lstSubStrings.SelectedIndex].Data);

				dfbp = new DynamicFileByteProvider(strm);
				dfbp.Changed += new EventHandler(byteProvider_Changed);
			}
			catch (Exception)
			{ }

			hbxHexView.ByteProvider = dfbp;
		}

		// Utilities
		private void UpdateForm()
		{
			this.Text = Settings.Default.ApplicationName + (FileName() != string.Empty ? " - " + FileName() : string.Empty) + (_hasChanges ? "*" : string.Empty);

			saveToolStripMenuItem.Enabled = _fileOpen;
			saveAsToolStripMenuItem.Enabled = _fileOpen;
			exportToolStripMenuItem.Enabled = _fileOpen;

			lstStrings.Enabled = _fileOpen;
			lstSubStrings.Enabled = _fileOpen;
			txtEdit.Enabled = _fileOpen;
			txtOriginal.Enabled = _fileOpen;
			txtConcatenated.Enabled = _fileOpen;
			hbxHexView.Enabled = _fileOpen;
		}

		private string FileName()
		{
			return _msbt == null || _msbt.File == null ? string.Empty : _msbt.File.Name;
		}

		// Tools
		private void exportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();

			List<string> row = new List<string>();
			if (_msbt.HasLabels)
			{
				sb.AppendLine("LabelOrder,Label,StringOrder,String");

				for (int i = 0; i < _msbt.TXT2.NumberOfStrings; i++)
				{
					Entry label = _msbt.LBL1.Labels[i];

					// Label
					row.Add((i + 1).ToString());
					row.Add(label.ToString());

					// Entry
					row.Add(label.ID.ToString());
					string result = string.Empty;
					foreach (Value value in _msbt.TXT2.Entries[label.ID].Values)
						result += Encoding.Unicode.GetString(value.Data).Replace("\n", "\r\n").Replace("\"", "\"\"");
					row.Add("\"" + result + "\"");

					sb.AppendLine(String.Join(",", row.ToArray()));
					row.Clear();
				}
			}
			else
			{
				sb.AppendLine("StringOrder,String");

				for (int i = 0; i < _msbt.TXT2.NumberOfStrings; i++)
				{
					Entry entry = _msbt.TXT2.Entries[i];

					// Entry
					row.Add((i + 1).ToString());
					string result = string.Empty;
					foreach (Value value in entry.Values)
						result += Encoding.Unicode.GetString(value.Data).Replace("\n", "\r\n").Replace("\"", "\"\"");
					row.Add("\"" + result + "\"");

					sb.AppendLine(String.Join(",", row.ToArray()));
					row.Clear();
				}
			}

			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Title = "Saving ";
			sfd.Filter = "Comma Separated Values (*.csv)|*.csv";
			sfd.InitialDirectory = Settings.Default.InitialDirectory;

			if (sfd.ShowDialog() == DialogResult.OK)
			{
				try
				{
					FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
					BinaryWriter bw = new BinaryWriter(fs);
					bw.Write(new byte[] { 0xEF, 0xBB, 0xBF });
					bw.Write(sb.ToString().ToCharArray());
					bw.Close();
				}
				catch (IOException ioex)
				{
					MessageBox.Show(ioex.Message, "File Access Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void BG4ExplorerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Select a BG4 Binary...";
			ofd.Filter = "BG4 Archive (*.dat)|*.dat|All Files (*.*)|*.*";
			ofd.InitialDirectory = Settings.Default.BG4OpenDirectory;

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				Settings.Default.BG4OpenDirectory = new FileInfo(ofd.FileName).DirectoryName;

				if (File.Exists(ofd.FileName))
				{
					FolderBrowserDialog fbd = new FolderBrowserDialog();
					fbd.Description = "Select the destination directory to extarct the files into.";
					fbd.SelectedPath = Settings.Default.BG4ExtractDirectory;

					if (fbd.ShowDialog() == DialogResult.OK)
					{
						Settings.Default.BG4ExtractDirectory = fbd.SelectedPath;

						if (Directory.Exists(fbd.SelectedPath))
						{
							bool overwrite = true;

							if (new DirectoryInfo(fbd.SelectedPath).GetFiles().Length > 0)
								overwrite = MessageBox.Show("Is it OK to overwrite files in the destination directory?", "Overwrite?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes ? true : false;

							string result = BG4.Extract(ofd.FileName, fbd.SelectedPath, overwrite);

							MessageBox.Show(result, "BG4 Extraction Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
					}
				}
			}

			Settings.Default.Save();
			Settings.Default.Reload();
		}

		private void compressToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog load = new OpenFileDialog();
				if (load.ShowDialog() != DialogResult.OK) return;
				YATA.dsdecmp.Compress(load.FileName, load.FileName + ".lz");
				MessageBox.Show("Done", "LZ11 Compress");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "LZ11 Compress");
			}
		}

		private void decompressToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog load = new OpenFileDialog();
				if (load.ShowDialog() != DialogResult.OK) return;
				YATA.dsdecmp.Decompress(load.FileName, load.FileName + ".bin");
				MessageBox.Show("Done", "LZ11 Decompress");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "LZ11 Decompress");
			}
		}
	}
}