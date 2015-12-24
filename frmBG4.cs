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
	public partial class frmBG4 : Form
	{
		private FileInfo File = null;
		public string FileName = string.Empty;

		public frmBG4()
		{
			InitializeComponent();
			this.Icon = Properties.Resources.msbteditor;
		}

		private void frmBG4_Load(object sender, EventArgs e)
		{
			//FolderBrowserDialog fbd = new FolderBrowserDialog();
			//fbd.Description = "Select the destination directory to extarct the files into.";
			//fbd.SelectedPath = Settings.Default.InitialDirectory;

			//if (fbd.ShowDialog() == DialogResult.OK)
			//{
			//    string directory = fbd.SelectedPath;

			//    if (Directory.Exists(directory))
			//    {
			//        bool overwrite = true;

			//        if (new DirectoryInfo(directory).GetFiles().Length > 0)
			//            overwrite = MessageBox.Show("Is it OK to overwrite files in the destination directory?", "Overwrite?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes ? true : false;

			//        string result = BG4.Extract(FileName, directory, overwrite);

			//        MessageBox.Show(result, "BG4 Extraction Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//    }
			//}



		}
	}
}