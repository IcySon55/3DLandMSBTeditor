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
        string LoadedFile = "";
        public Form1()
        {
            InitializeComponent();
        }

        void loadFile(string file)
        {
            Strings.Clear();
            listBox1.Items.Clear();
            byte[] byteBuffer = File.ReadAllBytes(file);
            string byteBufferAsString = System.Text.Encoding.UTF8.GetString(byteBuffer);
            int base_offset = byteBufferAsString.IndexOf("TXT2");
            base_offset += 4;
            BinaryReader bin = new BinaryReader(File.Open(file, FileMode.Open));
            bin.BaseStream.Position = base_offset;
            int SectionSize = bin.ReadInt32(); //0x4
            bin.BaseStream.Position += 8;
            long countpos = bin.BaseStream.Position;
            int count = bin.ReadInt32();
            List<int> addressList = new List<int>();
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
                    try {
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
            for (int i = 0; i < Strings.Count; i++) listBox1.Items.Add(i);
            LoadedFile = file;
            bin.Close();
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
            bin.Write(bin.BaseStream.Length - CountPos);
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = System.Text.Encoding.Unicode.GetString(Strings[listBox1.SelectedIndex].ToArray());
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog Save = new SaveFileDialog();
            if (Save.ShowDialog() != DialogResult.OK) return;
            WriteFile(Save.FileName);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Strings[listBox1.SelectedIndex].Clear();
            Strings[listBox1.SelectedIndex].AddRange(System.Text.Encoding.Unicode.GetBytes(textBox1.Text));
        }

        private void version01ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version 0.1\r\nBy Exelix11");
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
    }
}
