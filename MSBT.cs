using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MsbtEditor
{
	class Header
	{
		public byte[] Identifier; // MsgStdBn
		public UInt16 ByteOrderMark;
		public UInt16 Unknown1; // Always 0x0000
		public UInt16 Unknown2; // Always 0x0103
		public UInt16 NumberOfSections;
		public UInt16 Unknown3; // Always 0x0000
		public UInt32 FileSize;
		public byte[] Unknown4; // Always 0x0000 0000 0000 0000 0000
	}

	class LBL1
	{
		public long Offset;
		public byte[] Identifier; // LBL1
		public UInt32 SectionSize; // Begins after Unknown1
		public byte[] Unknown1; // Always 0x0000 0000
		public byte[] Unknown2;
		public byte[] Unknown3; // Large collection of unknown values

		public List<Label> Labels;
	}

	class Label
	{
		public UInt32 Length;
		public byte[] Value;
		public Int32 ID;

		public override string ToString()
		{
			return Encoding.ASCII.GetString(Value);
		}
	}

	class ATR1
	{
		public long Offset;
		public byte[] Identifier; // ATR1
		public UInt32 SectionSize; // Begins after Unknown1
		public byte[] Unknown1; // Always 0x0000 0000
		public byte[] Unknown2;
	}

	class TXT2
	{
		public long Offset;
		public byte[] Identifier; // TXT2
		public UInt32 SectionSize; // Begins after Unknown1
		public byte[] Unknown1; // Always 0x0000 0000
		public UInt32 NumberOfStrings;

		public List<Value> OriginalValues;
		public List<Value> Values;
	}

	class Value
	{
		public byte[] Text;
		public Int32 ID;

		public override string ToString()
		{
			return ID.ToString();
		}
	}

	class MSBT
	{
		public FileInfo File { get; set; }

		public Header Header = new Header();
		public LBL1 LBL1 = new LBL1();
		public ATR1 ATR1 = new ATR1();
		public TXT2 TXT2 = new TXT2();
		public List<string> SectionOrder = new List<string>();

		byte paddingChar = 0xAB;

		public MSBT(string filename)
		{
			File = new FileInfo(filename);

			if (File.Exists)
			{
				FileStream fs = System.IO.File.Open(File.FullName, FileMode.Open, FileAccess.Read, FileShare.None);
				BinaryReader br = new BinaryReader(fs);

				// Header
				Header.Identifier = br.ReadBytes(8);
				if (Encoding.ASCII.GetString(Header.Identifier) != "MsgStdBn")
					throw new Exception("File is not a valid MSBT.");
				Header.ByteOrderMark = br.ReadUInt16();
				Header.Unknown1 = br.ReadUInt16();
				Header.Unknown2 = br.ReadUInt16();
				Header.NumberOfSections = br.ReadUInt16();
				Header.Unknown3 = br.ReadUInt16();
				Header.FileSize = br.ReadUInt32();
				Header.Unknown4 = br.ReadBytes(10);

				SectionOrder.Clear();
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
			LBL1.Identifier = br.ReadBytes(4);
			LBL1.SectionSize = br.ReadUInt32();
			LBL1.Unknown1 = br.ReadBytes(8);
			LBL1.Unknown2 = br.ReadBytes(8);
			uint startOfLabels = br.ReadUInt32() + (uint)LBL1.Offset + (uint)LBL1.Unknown1.Length + (uint)LBL1.Unknown2.Length;
			br.BaseStream.Seek(-sizeof(UInt32), SeekOrigin.Current);
			LBL1.Unknown3 = br.ReadBytes((int)startOfLabels - (int)br.BaseStream.Position);

			LBL1.Labels = new List<Label>();
			while (br.BaseStream.Position < (LBL1.Offset + LBL1.Identifier.Length + sizeof(UInt32) + LBL1.Unknown1.Length + LBL1.SectionSize))
			{
				Label lbl = new Label();
				lbl.Length = Convert.ToUInt32(br.ReadByte());
				lbl.Value = br.ReadBytes((int)lbl.Length);
				lbl.ID = br.ReadInt32();
				LBL1.Labels.Add(lbl);
			}

			PaddingSeek(ref br);
		}

		private void ReadATR1(ref BinaryReader br)
		{
			ATR1.Offset = br.BaseStream.Position;
			ATR1.Identifier = br.ReadBytes(4);
			ATR1.SectionSize = br.ReadUInt32();
			ATR1.Unknown1 = br.ReadBytes(8);
			ATR1.Unknown2 = br.ReadBytes(8);

			PaddingSeek(ref br);
		}

		private void ReadTXT2(ref BinaryReader br)
		{
			TXT2.Offset = br.BaseStream.Position;
			TXT2.Identifier = br.ReadBytes(4);
			TXT2.SectionSize = br.ReadUInt32();
			TXT2.Unknown1 = br.ReadBytes(8);
			long startOfStrings = br.BaseStream.Position;
			TXT2.NumberOfStrings = br.ReadUInt32();

			TXT2.OriginalValues = new List<Value>();
			TXT2.Values = new List<Value>();

			List<UInt32> offsets = new List<UInt32>();
			for (int i = 0; i < TXT2.NumberOfStrings; i++)
				offsets.Add(br.ReadUInt32());
			for (int i = 0; i < TXT2.NumberOfStrings; i++)
			{
				Value value = new Value();
				bool eos = false;
				UInt32 next = offsets[0];

				br.BaseStream.Seek(startOfStrings + offsets[i], SeekOrigin.Begin);

				if (i + 1 >= offsets.Count)
				{
					List<byte> result = new List<byte>();
					while (!eos)
					{
						byte unichar = br.ReadByte();

						if (unichar == 0xAB || br.BaseStream.Position == br.BaseStream.Length)
							eos = true;
						else
							result.Add(unichar);
					}
					value.Text = result.ToArray();
				}
				else
					value.Text = br.ReadBytes((int)((startOfStrings + offsets[i + 1]) - (startOfStrings + offsets[i])));
				value.ID = i;
				TXT2.OriginalValues.Add(value);

				Value valueEdited = new Value();
				valueEdited.Text = value.Text;
				TXT2.Values.Add(valueEdited);
			}

			PaddingSeek(ref br);
		}

		private void PaddingSeek(ref BinaryReader br)
		{
			long remainder = br.BaseStream.Position % 16;
			if (remainder > 0)
				br.BaseStream.Seek(16 - remainder, SeekOrigin.Current);
		}

		public bool Save()
		{
			bool result = false;

			try
			{
				FileStream fs = System.IO.File.Create(File.FullName);
				BinaryWriter bw = new BinaryWriter(fs);

				// Header
				bw.Write(Header.Identifier);
				bw.Write(Header.ByteOrderMark);
				bw.Write(Header.Unknown1);
				bw.Write(Header.Unknown2);
				bw.Write(Header.NumberOfSections);
				bw.Write(Header.Unknown3);
				bw.Write(Header.FileSize);
				bw.Write(Header.Unknown4);

				foreach (string section in SectionOrder)
				{
					if (section == "LBL1")
						WriteLBL1(ref bw);
					else if (section == "ATR1")
						WriteATR1(ref bw);
					else if (section == "TXT2")
						WriteTXT2(ref bw);
				}

				bw.Close();
			}
			catch (Exception)
			{ }

			return result;
		}

		private bool WriteLBL1(ref BinaryWriter bw)
		{
			bool result = false;

			try
			{
				bw.Write(LBL1.Identifier);
				bw.Write(LBL1.SectionSize);
				bw.Write(LBL1.Unknown1);
				bw.Write(LBL1.Unknown2);
				bw.Write(LBL1.Unknown3);

				foreach (Label lbl in LBL1.Labels)
				{
					bw.Write(Convert.ToByte(lbl.Length));
					bw.Write(lbl.Value);
					bw.Write(lbl.ID);
				}

				PaddingWrite(ref bw);

				result = true;
			}
			catch(Exception)
			{
				result = false;
			}

			return result;
		}

		private bool WriteATR1(ref BinaryWriter bw)
		{
			bool result = false;

			try
			{
				bw.Write(ATR1.Identifier);
				bw.Write(ATR1.SectionSize);
				bw.Write(ATR1.Unknown1);
				bw.Write(ATR1.Unknown2);

				PaddingWrite(ref bw);

				result = true;
			}
			catch (Exception)
			{
				result = false;
			}

			return result;
		}

		private bool WriteTXT2(ref BinaryWriter bw)
		{
			bool result = false;

			try
			{
				bw.Write(TXT2.Identifier);
				bw.Write(TXT2.SectionSize);
				bw.Write(TXT2.Unknown1);
				long startOfStrings = bw.BaseStream.Position;
				bw.Write(TXT2.NumberOfStrings);

				long cursor = bw.BaseStream.Position;

				List<UInt32> offsets = new List<UInt32>();
				UInt32 offsetsLength = TXT2.NumberOfStrings * sizeof(UInt32) + sizeof(UInt32);
				UInt32 runningTotal = 0;
				for (int i = 0; i < TXT2.NumberOfStrings; i++)
				{
					offsets.Add(offsetsLength + runningTotal);
					runningTotal += (UInt32)(TXT2.Values[i].Text.Length);
					if (TXT2.Values[i].Text[TXT2.Values[i].Text.Length - 2] != '\0' && TXT2.Values[i].Text[TXT2.Values[i].Text.Length - 1] != '\0')
						runningTotal += 2;
				}
				for (int i = 0; i < TXT2.NumberOfStrings; i++)
					bw.Write(offsets[i]);
				for (int i = 0; i < TXT2.NumberOfStrings; i++)
				{
					bw.Write(TXT2.Values[i].Text);
					if (TXT2.Values[i].Text[TXT2.Values[i].Text.Length - 2] != '\0' && TXT2.Values[i].Text[TXT2.Values[i].Text.Length - 1] != '\0')
					{
						bw.Write('\0');
						bw.Write('\0');
					}
				}

				PaddingWrite(ref bw);

				result = true;
			}
			catch (Exception)
			{
				result = false;
			}

			return result;
		}

		private void PaddingWrite(ref BinaryWriter bw)
		{
			long remainder = bw.BaseStream.Position % 16;
			if (remainder > 0)
				for (int i = 0; i < 16 - remainder; i++)
					bw.Write(paddingChar);
		}
	}
}