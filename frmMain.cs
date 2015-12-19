using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
		private int _subIndex = 0;

		public frmMain(string[] args)
		{
			InitializeComponent();

			if (args.Length > 0 && File.Exists(args[0]))
				OpenFile(args[0]);
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			slbAddress.Text = string.Empty;
			slbStringCount.Text = string.Empty;
			UpdateForm();
		}

		private void frmMain_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}

		private void frmMain_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			ConfirmOpenFile(files[0]);
		}

		#region Menu and Toolbar

		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
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

		private void compressToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog load = new OpenFileDialog();
			if (load.ShowDialog() != DialogResult.OK) return;
			YATA.dsdecmp.Compress(load.FileName, load.FileName + ".lz");
			MessageBox.Show("Done");
		}

		private void decompressToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog load = new OpenFileDialog();
			if (load.ShowDialog() != DialogResult.OK) return;
			YATA.dsdecmp.Decompress(load.FileName, load.FileName + ".bin");
			MessageBox.Show("Done");
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
			{
				dr = MessageBox.Show("You have unsaved changes in " + FileName() + ". Save changes before opening another file?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
			}

			switch (dr)
			{
				case DialogResult.Yes:
					dr = SaveFile();
					if (dr == DialogResult.OK) OpenFile(filename);
					break;
				case DialogResult.No:
					OpenFile(filename);
					break;
				case DialogResult.Cancel:
					break;
			}
		}

		private void OpenFile(string filename = "")
		{
			ofdOpenFile.InitialDirectory = Settings.Default.InitialDirectory;
			DialogResult dr = DialogResult.OK;

			if (filename != "")
			{
				_msbt = new MSBT(filename);
				_fileOpen = true;
				_hasChanges = false;
				LoadFile();
				UpdateForm();
				Settings.Default.InitialDirectory = new DirectoryInfo(filename).FullName;
				Settings.Default.Save();
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
						Settings.Default.InitialDirectory = new DirectoryInfo(ofdOpenFile.FileName).FullName;
						Settings.Default.Save();
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

			if (_msbt.LBL1.Labels.Count > 0)
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
				Settings.Default.InitialDirectory = new DirectoryInfo(sfdSaveEntity.FileName).FullName;
				Settings.Default.Save();
			}

			if (dr == DialogResult.OK)
			{
				_msbt.Save();
				_msbt = new MSBT(_msbt.File.FullName); // Reload to refresh Original Values
				_hasChanges = false;
				UpdateForm();
				lstStrings_SelectedIndexChanged(null, null);
			}

			return dr;
		}

		private void lstStrings_SelectedIndexChanged(object sender, EventArgs e)
		{
			Entry entry = (Entry)lstStrings.SelectedItem;

			_subIndex = 0;
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
			_subIndex = lstSubStrings.SelectedIndex;
			UpdateTextView();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Tab || keyData == (Keys.Tab | Keys.Shift))
			{
				if (txtEdit.Focused)
				{
					if (keyData == (Keys.Tab | Keys.Shift))
						lstStrings.SelectedIndex -= (lstStrings.SelectedIndex - 1 >= 0 ? 1 : (lstStrings.Items.Count - 1) * -1);
					else
						lstStrings.SelectedIndex += (lstStrings.SelectedIndex + 1 < lstStrings.Items.Count ? 1 : (lstStrings.Items.Count - 1) * -1);
					return true;
				}
				else
					return base.ProcessCmdKey(ref msg, keyData);
			}
			else
				return base.ProcessCmdKey(ref msg, keyData);
		}

		private void txtEdit_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.A)
			{
				txtEdit.SelectAll();
				e.SuppressKeyPress = true;
			}
		}

		private void txtEdit_KeyUp(object sender, KeyEventArgs e)
		{
			string result = txtEdit.Text;

			Entry entry = (Entry)lstStrings.SelectedItem;
			_msbt.TXT2.Entries[entry.ID].Values[_subIndex] = Encoding.Unicode.GetBytes(result.Replace("\r\n", "\n"));

			UpdateHexView();
		}

		private void UpdateTextView()
		{
			Entry entry = (Entry)lstStrings.SelectedItem;

			txtEdit.Text = Encoding.Unicode.GetString(_msbt.TXT2.Entries[entry.ID].Values[_subIndex]).Replace("\n", "\r\n");
			txtOriginal.Text = Encoding.Unicode.GetString(_msbt.TXT2.OriginalEntries[entry.ID].Values[_subIndex]).Replace("\n", "\r\n");

			string result = string.Empty;
			foreach (byte[] value in _msbt.TXT2.Entries[entry.ID].Values)
				result += Encoding.Unicode.GetString(value).Replace("\n", "\r\n");
			txtConcatenated.Text = result;

			UpdateHexView();

			// TODO: show string info
		}

		private void UpdateHexView()
		{
			DynamicFileByteProvider dfbp = null;

			try
			{
				Entry entry = (Entry)lstStrings.SelectedItem;
				MemoryStream strm = new MemoryStream(_msbt.TXT2.Entries[entry.ID].Values[_subIndex]);

				dfbp = new DynamicFileByteProvider(strm);
				dfbp.Changed += new EventHandler(byteProvider_Changed);
			}
			catch (Exception)
			{ }

			hbxHexView.ByteProvider = dfbp;
		}

		protected void byteProvider_Changed(object sender, EventArgs e)
		{
			DynamicFileByteProvider dfbp = (DynamicFileByteProvider)sender;

			Entry entry = (Entry)lstStrings.SelectedItem;
			List<byte> bytes = new List<byte>();
			for (int i = 0; i < (int)dfbp.Length; i++)
				bytes.Add(dfbp.ReadByte(i));
			_msbt.TXT2.Entries[entry.ID].Values[_subIndex] = bytes.ToArray();

			txtEdit.Text = Encoding.Unicode.GetString(_msbt.TXT2.Entries[entry.ID].Values[_subIndex]);
		}

		// Utilities
		private void UpdateForm()
		{
			this.Text  = Settings.Default.ApplicationName + FileName() + (_hasChanges ? "*" : "");

			saveToolStripMenuItem.Enabled = _fileOpen;
			saveAsToolStripMenuItem.Enabled = _fileOpen;

			lstStrings.Enabled = _fileOpen;
			lstSubStrings.Enabled = _fileOpen;
			txtEdit.Enabled = _fileOpen;
			txtOriginal.Enabled = _fileOpen;
			txtConcatenated.Enabled = _fileOpen;
			hbxHexView.Enabled = _fileOpen;
		}

		private string FileName()
		{
			return _msbt == null || _msbt.File == null ? "" : " - " + _msbt.File.Name;
		}
	}
}