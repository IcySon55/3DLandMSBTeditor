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
		public MSBT msbt { get; set; }
		public IEntry Return { get; set; }

		public frmSearch()
		{
			InitializeComponent();
			this.Icon = Resources.find;
		}

		private void frmSearch_Load(object sender, EventArgs e)
		{
			Return = null;
			chkMatchCase.Checked = Settings.Default.FindMatchCase;
		}

		private void btnFindText_Click(object sender, EventArgs e)
		{
			lstResults.Items.Clear();

			if (txtFindText.Text.Trim() != string.Empty)
			{
				for (int i = 0; i < msbt.TXT2.NumberOfStrings; i++)
				{
					IEntry ent = null;

					if (msbt.HasLabels)
						ent = msbt.LBL1.Labels[i];
					else
						ent = msbt.TXT2.Strings[i];

					if (chkMatchCase.Checked)
					{
						if (msbt.FileEncoding.GetString(ent.Value).Contains(txtFindText.Text))
							lstResults.Items.Add(ent);
					}
					else
					{
						if (msbt.FileEncoding.GetString(ent.Value).ToLower().Contains(txtFindText.Text.ToLower()))
							lstResults.Items.Add(ent);
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
			Settings.Default.FindMatchCase = chkMatchCase.Checked;
			Settings.Default.Save();
			Settings.Default.Reload();
		}

		private void lstResults_DoubleClick(object sender, EventArgs e)
		{
			if (lstResults.Items.Count > 0 && lstResults.SelectedIndex >= 0)
			{
				Return = (IEntry)lstResults.SelectedItem;
				this.Close();
			}
		}
	}
}