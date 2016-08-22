using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MsbtEditor.Properties;
using System.IO;

namespace MsbtEditor
{
	public partial class frmSearchDirectory : Form
	{
		public class SearchResult
		{
			public string Filename { get; set; }
			public IEntry Entry { get; set; }
			public int Index { get; set; }

			public override string ToString()
			{
				return new FileInfo(Filename).Name + " - " + Entry.ToString();
			}
		}

		public SearchResult Return { get; set; }

		public frmSearchDirectory()
		{
			InitializeComponent();
			this.Icon = Resources.search;
		}

		private void frmSearchDirectory_Load(object sender, EventArgs e)
		{
			Return = null;
			txtSearchDirectory.Text = Settings.Default.SearchDirectory;
			chkMatchCase.Checked = Settings.Default.SearchMatchCase;
			chkSearchSubfolders.Checked = Settings.Default.SearchSubfolders;
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.Description = "Select the directory to search through.";
			fbd.SelectedPath = Settings.Default.SearchDirectory;

			if (fbd.ShowDialog() == DialogResult.OK)
			{
				Settings.Default.SearchDirectory = fbd.SelectedPath;
				txtSearchDirectory.Text = fbd.SelectedPath;
			}

			Settings.Default.Save();
			Settings.Default.Reload();
		}

		private void btnFindText_Click(object sender, EventArgs e)
		{
			lstResults.Items.Clear();

			if (txtSearchDirectory.Text.Trim() != string.Empty && Directory.Exists(txtSearchDirectory.Text.Trim()))
			{
				if (txtFindText.Text.Trim() != string.Empty)
				{
					string[] files = Directory.GetFiles(txtSearchDirectory.Text.Trim(), "*.msbt", (chkSearchSubfolders.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
					ListBox lstTemp = new ListBox();

					foreach (string file in files)
					{
						MSBT msbt = null;

						try
						{
							msbt = new MSBT(file);
						}
						catch(InvalidMSBTException imex)
						{
							continue;
						}

						if (msbt.HasLabels)
							lstTemp.Sorted = true;
						else
							lstTemp.Sorted = false;

						lstTemp.Items.Clear();
						for (int i = 0; i < msbt.TXT2.NumberOfStrings; i++)
						{
							if (msbt.HasLabels)
								lstTemp.Items.Add(msbt.LBL1.Labels[i]);
							else
								lstTemp.Items.Add(msbt.TXT2.Strings[i]);
						}

						// Find the strings
						for (int i = 0; i < msbt.TXT2.NumberOfStrings; i++)
						{
							IEntry ent = null;

							if (msbt.HasLabels)
								ent = msbt.LBL1.Labels[i];
							else
								ent = msbt.TXT2.Strings[i];

							if (lstTemp.Items.Contains(ent))
								lstTemp.SelectedItem = ent;

							if (chkMatchCase.Checked)
							{
								if (msbt.FileEncoding.GetString(ent.Value).Contains(txtFindText.Text))
								{
									SearchResult result = new SearchResult();
									result.Filename = file;
									result.Entry = ent;
									result.Index = lstTemp.SelectedIndex;
									lstResults.Items.Add(result);
								}
							}
							else
							{
								if (msbt.FileEncoding.GetString(ent.Value).ToLower().Contains(txtFindText.Text.ToLower()))
								{
									SearchResult result = new SearchResult();
									result.Filename = file;
									result.Entry = ent;
									result.Index = lstTemp.SelectedIndex;
									lstResults.Items.Add(result);
								}
							}
						}
					}
				}
			}

			if (lstResults.Items.Count == 0)
			{
				MessageBox.Show("Could not find \"" + txtFindText.Text + "\".", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void chkMatchCase_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.SearchMatchCase = chkMatchCase.Checked;
			Settings.Default.Save();
			Settings.Default.Reload();
		}

		private void chkSearchSubfolders_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.SearchSubfolders = chkSearchSubfolders.Checked;
			Settings.Default.Save();
			Settings.Default.Reload();
		}

		private void lstResults_DoubleClick(object sender, EventArgs e)
		{
			if (lstResults.Items.Count > 0 && lstResults.SelectedIndex >= 0)
			{
				Return = (SearchResult)lstResults.SelectedItem;
				this.Close();
			}
		}
	}
}