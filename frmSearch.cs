using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MsbtEditor.Properties;

namespace MsbtEditor
{
	public partial class frmSearch : Form
	{
		public MSBT Msbt { get; set; }
		public Entry Return { get; set; }

		public frmSearch()
		{
			InitializeComponent();
			this.Icon = Resources.find;
		}

		private void frmSearch_Load(object sender, EventArgs e)
		{
			Return = null;
			chkMatchCase.Checked = Settings.Default.MatchCase;
		}

		private void btnFindText_Click(object sender, EventArgs e)
		{
			lstResults.Items.Clear();

			if (txtFindText.Text.Trim() != string.Empty)
			{
				for (int i = 0; i < Msbt.TXT2.NumberOfStrings; i++)
				{
					if (Msbt.HasLabels)
					{
						Entry entry = Msbt.LBL1.Labels[i];

						if (chkMatchCase.Checked)
						{
							if (Msbt.TXT2.Entries[entry.ID].Preview().Contains(txtFindText.Text))
								lstResults.Items.Add(entry);
						}
						else
						{
							if (Msbt.TXT2.Entries[entry.ID].Preview().ToLower().Contains(txtFindText.Text.ToLower()))
								lstResults.Items.Add(entry);
						}
					}
					else
					{
						Entry entry = Msbt.TXT2.Entries[i];

						if (chkMatchCase.Checked)
						{
							if (entry.Preview().Contains(txtFindText.Text))
								lstResults.Items.Add(entry);
						}
						else
						{
							if (entry.Preview().ToLower().Contains(txtFindText.Text.ToLower()))
								lstResults.Items.Add(entry);
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
			this.Hide();
		}

		private void chkMatchCase_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.MatchCase = chkMatchCase.Checked;
			Settings.Default.Save();
			Settings.Default.Reload();
		}

		private void lstResults_DoubleClick(object sender, EventArgs e)
		{
			if (lstResults.Items.Count > 0 && lstResults.SelectedIndex >= 0)
			{
				Return = (Entry)lstResults.SelectedItem;
				this.Hide();
			}
		}
	}
}