using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace _3DlandMSBTeditor
{
	struct Header
	{
		public char[] Identifier; // MsgStdBn
		public UInt16 ByteOrderMark;
		public UInt16 Unknown1; // Always 0x0000
		public UInt16 Unknown2; // Always 0x0103
		public UInt16 NumberOfSections;
		public UInt16 Unknown3; // Always 0x0000
		public UInt32 FileSize;
		public byte[] Unknown4; // Always 0x0000 0000 0000 0000 0000
	}

	struct LBL1
	{
		public long Offset;
		public char[] Identifier; // LBL1
		public UInt32 SectionSize; // Begins after Unknown1
		public byte[] Unknown1; // Always 0x0000 0000
		public byte[] Unknown2;
		public byte[] Unknown3; // Large collection of unknown values
		public uint Padding; // AB bytes to pad to multiples of 16

		public new List<Label> Labels;
	}

	struct Label
	{
		public UInt32 Length;
		public char[] Value;
		public UInt32 ID;
	}

	struct ATR1
	{
		public long Offset;
		public char[] Identifier; // ATR1
		public UInt32 SectionSize; // Begins after Unknown1
		public byte[] Unknown1; // Always 0x0000 0000
		public byte[] Unknown2;
		public uint Padding; // AB bytes to pad to multiples of 16
	}

	struct TXT2
	{
		public long Offset;
		public char[] Identifier; // TXT2
		public UInt32 NumberOfStrings;

		public uint Padding; // AB bytes to pad to multiples of 16
	}

	class MSBT
	{
		public Header Header = new Header();
		public LBL1 LBL1 = new LBL1();
		public ATR1 ATR1 = new ATR1();
		public TXT2 TXT2 = new TXT2();
		public List<string> SectionOrder = new List<string>();

		public MSBT(string filename)
		{
			if (File.Exists(filename))
			{
				FileStream file = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None);
				BinaryReader br = new BinaryReader(file);

				// Header
				Header.Identifier = br.ReadChars(8);
				Header.ByteOrderMark = br.ReadUInt16();
				Header.Unknown1 = br.ReadUInt16();
				Header.Unknown2 = br.ReadUInt16();
				Header.NumberOfSections = br.ReadUInt16();
				Header.Unknown3 = br.ReadUInt16();
				Header.FileSize = br.ReadUInt32();
				Header.Unknown4 = br.ReadBytes(10);

				for (int i = 0; i < Header.NumberOfSections; i++)
				{
					// Section Detection
					if (br.PeekChar() == 'L')
					{
						ReadLBL1(ref br);
						SectionOrder.Add("LBL1");
					}
					else if (br.PeekChar() == 'A')
					{
						ReadATR1(ref br);
						SectionOrder.Add("ATR1");
					}
					else if (br.PeekChar() == 'T')
					{
						ReadTXT2(ref br);
						SectionOrder.Add("TXT2");
					}
				}

				br.Close();
			}
		}

		private void ReadLBL1(ref BinaryReader br)
		{
			LBL1.Offset = br.BaseStream.Position;
			LBL1.Identifier = br.ReadChars(4);
			LBL1.SectionSize = br.ReadUInt32();
			LBL1.Unknown1 = br.ReadBytes(8);
			LBL1.Unknown2 = br.ReadBytes(8);
			uint startOfLabels = br.ReadUInt32() + (uint)LBL1.Offset + (uint)LBL1.Unknown1.Length + (uint)LBL1.Unknown2.Length;
			LBL1.Unknown3 = br.ReadBytes((int)startOfLabels - (int)br.BaseStream.Position);

			LBL1.Labels = new List<Label>();
			while (br.BaseStream.Position < (LBL1.Offset + LBL1.Identifier.Length + sizeof(UInt32) + LBL1.Unknown1.Length + LBL1.SectionSize))
			{
				Label lbl = new Label();

				lbl.Length = Convert.ToUInt32(br.ReadByte());
				lbl.Value = br.ReadChars((int)lbl.Length);
				lbl.ID = br.ReadUInt32();

				LBL1.Labels.Add(lbl);
				Console.WriteLine(new String(lbl.Value));
			}
		}

		private void ReadATR1(ref BinaryReader br)
		{
			ATR1.Offset = br.BaseStream.Position;
			ATR1.Identifier = br.ReadChars(4);
			ATR1.SectionSize = br.ReadUInt32();
			ATR1.Unknown1 = br.ReadBytes(8);
			ATR1.Unknown2 = br.ReadBytes(8);
			ATR1.Padding
		}

		private void ReadTXT2(ref BinaryReader br)
		{

		}

	}
}