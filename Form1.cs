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

        public Form1(string[] args)
        {
            InitializeComponent();
            if (args.Length > 0) loadFile(args[0]);
        }

        void loadFile(string file)
        {
            Strings.Clear();
            addressList.Clear();
            listBox1.Items.Clear();
            Names.Clear();
            addressList.Clear();
            editingIndex = 0;
            ExtraEmptyData = 0;
            CorrectStringID.Clear();
            textBox1.Enabled = true;
            listBox1.Enabled = true;
            byte[] byteBuffer = File.ReadAllBytes(file);
            string byteBufferAsString = System.Text.Encoding.UTF8.GetString(byteBuffer);
            if (!byteBufferAsString.StartsWith("Msg"))
            {
                MessageBox.Show("Not a valid MSBT file !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Strings.Clear();
                addressList.Clear();
                listBox1.Items.Clear();
                Names.Clear();
                addressList.Clear();
                editingIndex = 0;
                ExtraEmptyData = 0;
                CorrectStringID.Clear();
                textBox1.Enabled = false;
                listBox1.Enabled = false;
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
                bin.BaseStream.Position = countpos  + addressList[i] ;
                Debug.Print("POS: " + (countpos + addressList[i] ).ToString());
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
            label4.Text = "Strings in the file: " + count.ToString();
            LoadedFile = file;
            #endregion
            #region ReadNames
            base_offset = byteBufferAsString.IndexOf("LBL1");
            if (base_offset != -1)
            {
                Found_LBL1 = true;
                listBox1.Sorted = true;
                bin.BaseStream.Position = base_offset + 4;
                int end = bin.ReadInt32();
                bin.BaseStream.Position += 16;
                bin.BaseStream.Position = bin.ReadUInt32() + base_offset + 16;
                for (int i = 0; i < count; i++)
                {
                    Names.Add(Encoding.UTF8.GetString(bin.ReadBytes(Convert.ToInt32(bin.ReadByte()))));
                    CorrectStringID.Add(bin.ReadInt32());
                }
                listBox1.Items.AddRange(Names.ToArray());
            }
            else
            {
                MessageBox.Show("This file doesn't have the LBL1 section, strings names are missing");
                Found_LBL1 = false;
                listBox1.Sorted = false;
                List<string> list = new List<string>();
                for (int i = 0; i < count; i++)
                {
                    list.Add(i.ToString());
                }
                listBox1.Items.AddRange(list.ToArray());
            }
            #endregion
            bin.Close();
            listBox1.SelectedItem = 0;
        }

        void WriteFile(string file)
        {
            if (file != LoadedFile) File.Copy(LoadedFile, file);
            byte[] byteBuffer = File.ReadAllBytes(file);
            string byteBufferAsString = System.Text.Encoding.UTF8.GetString(byteBuffer);
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
            for (int i = 0; i < Strings.Count;i ++) bin.Write(new byte[4]);
            List<int> Pos = new List<int>();
            for (int i = 0; i < Strings.Count; i++)
            {
                Pos.Add((Int32)(bin.BaseStream.Position - CountPos ));
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
                editingIndex = CorrectStringID[Names.FindIndex(x => x == listBox1.SelectedItem.ToString())];
            }
            else
            {
                editingIndex = listBox1.SelectedIndex;
            }
            textBox1.Text = FromBytesToText(Strings[editingIndex].ToArray());
            UpdateDECview();
            lbl_addr.Text = "Selected string address: " + valueConverter(txt2addr + 16 + addressList[editingIndex]);
            CanChange = true;
        }
        
        void UpdateDECview()
        {
            string data = "";
            for (int i = 0; i < Strings[editingIndex].Count; i++) data = data + " " + valueConverter(Strings[editingIndex][i]);
            DecEditTextbox.Text = data;
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
                res = res.Insert(positions[i]/2 + 2*i, Environment.NewLine);
            }
            return res;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog Save = new SaveFileDialog();
            if (Save.ShowDialog() != DialogResult.OK) return;
            WriteFile(Save.FileName);
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (CanChange)
            {
                Strings[editingIndex].Clear();
                string sav = textBox1.Text.Trim();
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
            if (bytes[0] == 13) bytes[0]= 10;
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
    }
}
