using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Be.Windows.Forms;
using System.Runtime.InteropServices;

namespace _3DlandMSBTeditor
{
	public partial class Form1 : Form
	{
		List<List<byte>> Strings = new List<List<byte>>();
		List<string> Names = new List<string>();
		List<int> addressList = new List<int>();
		List<int> CorrectStringID = new List<int>();
		int ExtraEmptyData = 0;
		string LoadedFile = "";
		int editingIndex = 0;
		bool Found_LBL1;
		int txt2addr = -1;

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool AllocConsole();

		public Form1(string[] args)
		{
			InitializeComponent();
			AllocConsole();
			if (args.Length > 0) loadFile(args[0]);
		}

		void loadFile(string file)
		{
			Strings.Clear();
			addressList.Clear();
			lstStrings.Items.Clear();
			Names.Clear();
			addressList.Clear();
			editingIndex = 0;
			ExtraEmptyData = 0;
			CorrectStringID.Clear();
			txtEdit.Enabled = true;
			lstStrings.Enabled = true;
			byte[] byteBuffer = File.ReadAllBytes(file);
			string byteBufferAsString = System.Text.Encoding.ASCII.GetString(byteBuffer);
			if (!byteBufferAsString.StartsWith("Msg"))
			{
				MessageBox.Show("Not a valid MSBT file !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Strings.Clear();
				addressList.Clear();
				lstStrings.Items.Clear();
				Names.Clear();
				addressList.Clear();
				editingIndex = 0;
				ExtraEmptyData = 0;
				CorrectStringID.Clear();
				txtEdit.Enabled = false;
				lstStrings.Enabled = false;
				return;
			}
			int base_offset = byteBufferAsString.IndexOf("TXT2");
			txt2addr = base_offset;
			#region ReadStrings
			base_offset += 4;
			BinaryReader bin = new BinaryReader(File.Open(file, FileMode.Open));
			bin.BaseStream.Position = base_offset;
			int SectionSize = bin.ReadInt32(); //0x4
			bin.BaseStream.Position += 8;
			long countpos = bin.BaseStream.Position;
			int count = bin.ReadInt32();
			for (int i = 0; i < count; i++)
			{
				addressList.Add(bin.ReadInt32());
			}
			for (int i = 0; i < count; i++)
			{
				bin.BaseStream.Position = countpos + addressList[i];
				Debug.Print("POS: " + (countpos + addressList[i]).ToString());
				List<byte> String = new List<byte>();
				bool FindNULL = false;
				while (!FindNULL)
				{
					try
					{
						byte[] temp = bin.ReadBytes(2);
						Debug.Print(temp[0].ToString() + " " + temp[1].ToString());
						if (IsNull(temp)) FindNULL = true; else String.AddRange(temp);
					}
					catch
					{
						FindNULL = true;
					}
				}
				Strings.Add(String);
			}
			for (int i = 0; i < 16; i++)
			{
				if ((bin.BaseStream.Position + 1) <= bin.BaseStream.Length && bin.ReadByte() == 0xAB) ExtraEmptyData++; else break;
			}
			slbStringCount.Text = "Strings in the file: " + count;
			LoadedFile = file;
			#endregion
			#region ReadNames
			base_offset = byteBufferAsString.IndexOf("LBL1");
			if (base_offset != -1)
			{
				Found_LBL1 = true;
				lstStrings.Sorted = true;
				bin.BaseStream.Position = base_offset + 4;
				int end = bin.ReadInt32();
				bin.BaseStream.Position += 16;
				bin.BaseStream.Position = bin.ReadUInt32() + base_offset + 16;
				for (int i = 0; i < count; i++)
				{
					Names.Add(Encoding.UTF8.GetString(bin.ReadBytes(Convert.ToInt32(bin.ReadByte()))));
					CorrectStringID.Add(bin.ReadInt32());
				}
				lstStrings.Items.AddRange(Names.ToArray());
			}
			else
			{
				MessageBox.Show("This file doesn't have the LBL1 section, strings names are missing");
				Found_LBL1 = false;
				lstStrings.Sorted = false;
				List<string> list = new List<string>();
				for (int i = 0; i < count; i++)
				{
					list.Add(i.ToString());
				}
				lstStrings.Items.AddRange(list.ToArray());
			}
			#endregion
			bin.Close();
			lstStrings.SelectedItem = 0;
		}

		void WriteFile(string file = "")
		{
			if (file != LoadedFile && file != "")
				File.Copy(LoadedFile, file);
			else
				file = LoadedFile;

			byte[] byteBuffer = File.ReadAllBytes(file);
			string byteBufferAsString = System.Text.Encoding.ASCII.GetString(byteBuffer);
			int base_offset = byteBufferAsString.IndexOf("TXT2");
			base_offset += 4;
			BinaryWriter bin = new BinaryWriter(File.Open(file, FileMode.Open));
			bin.BaseStream.Position = base_offset;
			long SectionSizePos = bin.BaseStream.Position;
			bin.Write(new byte[4]); //0x4
			bin.Write(new byte[8]);
			long CountPos = bin.BaseStream.Position;
			bin.Write(Strings.Count); //COUNT
			long AddressPos = bin.BaseStream.Position;
			for (int i = 0; i < Strings.Count; i++) bin.Write(new byte[4]);
			List<int> Pos = new List<int>();
			for (int i = 0; i < Strings.Count; i++)
			{
				Pos.Add((Int32)(bin.BaseStream.Position - CountPos));
				bin.Write(Strings[i].ToArray());
				bin.Write(new byte[2]);
			}
			bin.BaseStream.Position = AddressPos;
			for (int i = 0; i < Strings.Count; i++)
			{
				bin.Write(Pos[i]);
			}
			bin.BaseStream.Position = SectionSizePos;
			bin.Write(bin.BaseStream.Length - CountPos - ExtraEmptyData);
			bin.BaseStream.Position = 18;
			bin.Write(bin.BaseStream.Length);
			bin.Flush();
			bin.Close();
		}

		private bool IsNull(byte[] input)
		{
			if (input.Length > 1)
			{
				int ZeroCount = 0;
				for (int i = 0; i < input.Length; i++)
				{
					if (input[i] == 0x00) ZeroCount++;
				}
				if (ZeroCount > 1) return true; else return false;
			}
			else return true;
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog load = new OpenFileDialog();
			if (load.ShowDialog() != DialogResult.OK) return;
			loadFile(load.FileName);
		}
		bool CanChange = false;

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			CanChange = false;
			if (Found_LBL1)
			{
				editingIndex = CorrectStringID[Names.FindIndex(x => x == lstStrings.SelectedItem.ToString())];
			}
			else
			{
				editingIndex = lstStrings.SelectedIndex;
			}
			txtEdit.Text = FromBytesToText(Strings[editingIndex].ToArray());
			UpdateDECview();
			slbAddress.Text = "Selected string address: " + valueConverter(txt2addr + 16 + addressList[editingIndex]);
			CanChange = true;
		}

		void UpdateDECview()
		{
			DynamicFileByteProvider dynamicFileByteProvider = null;

			try
			{
				MemoryStream strm;
				byte[] bytes = new byte[Strings[editingIndex].Count];

				for (int i = 0; i < Strings[editingIndex].Count; i++) bytes[i] = Strings[editingIndex][i];
				strm = new MemoryStream(bytes);

				// try to open in write mode
				dynamicFileByteProvider = new DynamicFileByteProvider(strm);
				//dynamicFileByteProvider.Changed += new EventHandler(byteProvider_Changed);
				//dynamicFileByteProvider.LengthChanged += new EventHandler(byteProvider_LengthChanged);
			}
			catch (IOException) // write mode failed
			{
				try
				{
					//// try to open in read-only mode
					//dynamicFileByteProvider = new DynamicFileByteProvider(fileName, true);
					//if (Program.ShowQuestion(strings.OpenReadonly) == DialogResult.No)
					//{
					//   dynamicFileByteProvider.Dispose();
					//   return;
					//}
				}
				catch (IOException) // read-only also failed
				{
					// file cannot be opened
					//Program.ShowError(strings.OpenFailed);
					return;
				}
			}

			hbxHexView.ByteProvider = dynamicFileByteProvider;

			//string data = "";
			//for (int i = 0; i < Strings[editingIndex].Count; i++) data = data + " " + valueConverter(Strings[editingIndex][i]);
			//hbxHexView.Text = data;
		}

		private string FromBytesToText(byte[] bytes)
		{
			string res = "";
			#region FIND0A
			List<int> positions = new List<int>();
			for (int i = 0; i < bytes.Length; i++)
			{
				if (bytes[i] == 10) positions.Add(i);
			}
			#endregion
			res = System.Text.Encoding.Unicode.GetString(bytes);
			for (int i = 0; i < positions.Count; i++)
			{
				res = res.Insert(positions[i] / 2 + 2 * i, Environment.NewLine);
			}
			return res;
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WriteFile();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			if (sfd.ShowDialog() != DialogResult.OK) return;
			WriteFile(sfd.FileName);
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			if (CanChange)
			{
				Strings[editingIndex].Clear();
				string sav = txtEdit.Text.Trim();
				#region TESTCODE
				/*
					 List<int> indexes = new List<int>();
					 string save = sav;
					 for (int index = 0; ; index += Environment.NewLine.Length)
					 {
						  index = sav.IndexOf(Environment.NewLine, index);
						  if (index == -1) break;
						  indexes.Add(index);
						  save = save.Remove(index - indexes.Count , 1);
					 }
					 Debug.Print(save);
					 Debug.Print("COUNT: "+indexes.Count.ToString());
					 for (int i = 0; i < indexes.Count; i++) Debug.Print(i.ToString() + ": " + indexes[i].ToString());
					 List<Byte> array = new List<Byte>();
					 array.AddRange(System.Text.Encoding.Unicode.GetBytes(save));
					 string arr = "";
					 for (int i = 0; i < array.Count; i++) arr = arr + " " + array[i].ToString();
					 Debug.Print(arr);

					 for (int i = 0; i < indexes.Count; i++) array[indexes[i] * 2 - i*2] = 10;

					 Strings[listBox1.editingIndex].AddRange(array);
					 arr = "";
					 for (int i = 0; i < array.Count; i++) arr = arr + " " + array[i].ToString();
					 Debug.Print(arr);*/
				#endregion
				for (int i = 0; i < sav.Length; i++)
				{
					int res = GetBytes(sav[i]);
					if (res != -1)
					{
						Strings[editingIndex].Add((byte)res);
						Strings[editingIndex].Add(0);
					}
				}
				string arpr = "";
				for (int i = 0; i < Strings[editingIndex].Count; i++) arpr = arpr + " " + Strings[editingIndex][i].ToString();
				Debug.Print(arpr);
				UpdateDECview();
			}
		}

		int newlinecount = 0;
		int GetBytes(char str)
		{
			byte[] bytes = new byte[1];
			System.Buffer.BlockCopy(new char[1] { str }, 0, bytes, 0, bytes.Length);
			if (bytes[0] == 13) bytes[0] = 10;
			if (bytes[0] == 10)
			{
				if (newlinecount != 0) { newlinecount = 0; return -1; }
				newlinecount++;
			}
			return bytes[0];
		}

		private void version01ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Version 0.3\r\nBy Exelix11");
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

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void Form_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}

		private void Form_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			loadFile(files[0]);
		}

		int valueConverter(string HexNum)
		{
			return int.Parse(HexNum, System.Globalization.NumberStyles.HexNumber);
		}

		string valueConverter(int IntNum)
		{
			return IntNum.ToString("X");
		}

		private void tESTToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.InitialDirectory = Directory.GetCurrentDirectory();
			if (ofd.ShowDialog() != DialogResult.OK) return;

			txtEdit.Enabled = true;
			try
			{
				MSBT msbt = new MSBT(ofd.FileName);

				txtEdit.Text = "Magic: " + new String(msbt.Header.Identifier) + "\r\nSections: " + msbt.Header.NumberOfSections + "\r\nFileSize: " + msbt.Header.FileSize + " bytes";

			}
			catch (Exception ex)
			{
				txtEdit.Text = ex.ToString();
			}
		}
	}
}