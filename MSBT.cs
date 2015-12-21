using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;

namespace MsbtEditor
{
	class Header
	{
		public byte[] Identifier; // MsgStdBn
		public byte[] ByteOrderMark;
		public UInt16 Unknown1; // Always 0x0000
		public UInt16 Unknown2; // Always 0x0103
		public UInt16 NumberOfSections;
		public UInt16 Unknown3; // Always 0x0000
		public UInt32 FileSizeOffset;
		public UInt32 FileSize;
		public byte[] Unknown4; // Always 0x0000 0000 0000 0000 0000
	}

	class LBL1
	{
		public byte[] Identifier; // LBL1
		public UInt32 SectionSize; // Begins after Unknown1
		public byte[] Unknown1; // Always 0x0000 0000
		public byte[] Unknown2;
		public byte[] Unknown3; // Large collection of unknown values

		public List<Entry> Labels;
	}

	class NLI1
	{
		public byte[] Identifier; // NLI1
		public UInt32 SectionSize; // Begins after Unknown1
		public byte[] Unknown1; // Always 0x0000 0000
		public byte[] Unknown2; // Tons of unknown data
	}

	class ATR1
	{
		public byte[] Identifier; // ATR1
		public UInt32 SectionSize; // Begins after Unknown1
		public byte[] Unknown1; // Always 0x0000 0000
		public byte[] Unknown2;
	}

	class TXT2
	{
		public byte[] Identifier; // TXT2
		public UInt32 SectionSize; // Begins after Unknown1
		public byte[] Unknown1; // Always 0x0000 0000
		public UInt32 NumberOfStrings;

		public List<Entry> OriginalEntries;
		public List<Entry> Entries;
	}

	class Entry
	{
		public UInt32 Length;
		public List<byte[]> Values = new List<byte[]>();
		public byte[] Value;
		public Int32 ID;

		public override string ToString()
		{
			return (Length > 0 ? Encoding.ASCII.GetString(Value) : (ID + 1).ToString());
		}
	}

	class MSBT
	{
		public FileInfo File { get; set; }
		public bool HasLabels { get; set; }

		public Header Header = new Header();
		public LBL1 LBL1 = new LBL1();
		public NLI1 NLI1 = new NLI1();
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
				BinaryReaderX br = new BinaryReaderX(fs);

				// Initialize Members
				LBL1.Labels = new List<Entry>();
				TXT2.OriginalEntries = new List<Entry>();
				TXT2.Entries = new List<Entry>();

				// Header
				Header.Identifier = br.ReadBytes(8);
				if (Encoding.ASCII.GetString(Header.Identifier) != "MsgStdBn")
					throw new Exception("File is not a valid MSBT.");
				Header.ByteOrderMark = br.ReadBytes(2);

				// Byte Order
				br.ByteOrder = Header.ByteOrderMark[0] == 0xFF ? ByteOrder.LittleEndian : ByteOrder.BigEndian;

				Header.Unknown1 = br.ReadUInt16();
				Header.Unknown2 = br.ReadUInt16();
				Header.NumberOfSections = br.ReadUInt16();
				Header.Unknown3 = br.ReadUInt16();
				Header.FileSizeOffset = (UInt32)br.BaseStream.Position;
				Header.FileSize = br.ReadUInt32();
				Header.Unknown4 = br.ReadBytes(10);

				SectionOrder.Clear();
				for (int i = 0; i < Header.NumberOfSections; i++)
				{
					// Section Detection
					if (PeekString(br) == "LBL1")
					{
						ReadLBL1(br);
						SectionOrder.Add("LBL1");
					}
					else if (PeekString(br) == "NLI1")
					{
						ReadNLI1(br);
						SectionOrder.Add("NLI1");
					}
					else if (PeekString(br) == "ATR1")
					{
						ReadATR1(br);
						SectionOrder.Add("ATR1");
					}
					else if (PeekString(br) == "TXT2")
					{
						ReadTXT2(br);
						SectionOrder.Add("TXT2");
					}
				}

				br.Close();
			}
		}

		private string PeekString(BinaryReaderX br, int length = 4)
		{
			List<byte> bytes = new List<byte>();
			long startOffset = br.BaseStream.Position;

			for (int i = 0; i < length; i++)
				bytes.Add(br.ReadByte());

			br.BaseStream.Seek(startOffset, SeekOrigin.Begin);

			return Encoding.ASCII.GetString(bytes.ToArray());
		}

		private void ReadLBL1(BinaryReaderX br)
		{
			long offset = br.BaseStream.Position;
			LBL1.Identifier = br.ReadBytes(4);
			LBL1.SectionSize = br.ReadUInt32();
			LBL1.Unknown1 = br.ReadBytes(8);
			LBL1.Unknown2 = br.ReadBytes(8);
			uint startOfLabels = br.ReadUInt32() + (uint)offset + (uint)LBL1.Unknown1.Length + (uint)LBL1.Unknown2.Length;
			br.BaseStream.Seek(-sizeof(UInt32), SeekOrigin.Current);
			LBL1.Unknown3 = br.ReadBytes((int)startOfLabels - (int)br.BaseStream.Position);

			while (br.BaseStream.Position < (offset + LBL1.Identifier.Length + sizeof(UInt32) + LBL1.Unknown1.Length + LBL1.SectionSize))
			{
				Entry lbl = new Entry();
				lbl.Length = Convert.ToUInt32(br.ReadByte());
				lbl.Value = br.ReadBytes((int)lbl.Length);
				lbl.ID = br.ReadInt32();
				LBL1.Labels.Add(lbl);
			}

			HasLabels = LBL1.Labels.Count > 0;

			PaddingSeek(br);
		}

		private void ReadNLI1(BinaryReaderX br)
		{
			NLI1.Identifier = br.ReadBytes(4);
			NLI1.SectionSize = br.ReadUInt32();
			NLI1.Unknown1 = br.ReadBytes(8);
			NLI1.Unknown2 = br.ReadBytes((int)NLI1.SectionSize); // Read in the entire section at once since we don't know what it's for

			PaddingSeek(br);
		}

		private void ReadATR1(BinaryReaderX br)
		{
			ATR1.Identifier = br.ReadBytes(4);
			ATR1.SectionSize = br.ReadUInt32();
			ATR1.Unknown1 = br.ReadBytes(8);
			ATR1.Unknown2 = br.ReadBytes((int)ATR1.SectionSize); // Read in the entire section at once since we don't know what it's for

			PaddingSeek(br);
		}

		private void ReadTXT2(BinaryReaderX br)
		{
			TXT2.Identifier = br.ReadBytes(4);
			TXT2.SectionSize = br.ReadUInt32();
			TXT2.Unknown1 = br.ReadBytes(8);
			long startOfStrings = br.BaseStream.Position;
			TXT2.NumberOfStrings = br.ReadUInt32();

			List<UInt32> offsets = new List<UInt32>();
			for (int i = 0; i < TXT2.NumberOfStrings; i++)
				offsets.Add(br.ReadUInt32());
			for (int i = 0; i < TXT2.NumberOfStrings; i++)
			{
				Entry entry = new Entry();
				bool eos = false;
				UInt32 nextOffset = (i + 1 < offsets.Count) ? ((UInt32)startOfStrings + offsets[i + 1]) : ((UInt32)startOfStrings + TXT2.SectionSize);

				br.BaseStream.Seek(startOfStrings + offsets[i], SeekOrigin.Begin);

				List<byte> result = new List<byte>();
				while (!eos)
				{
					if (br.BaseStream.Position == nextOffset)
						eos = true;
					else
					{
						byte[] unichar = br.ReadBytes(2);

						if (Header.ByteOrderMark[0] == 0xFE)
							Array.Reverse(unichar);

						if (unichar[0] != 0x0 || unichar[1] != 0x0)
							result.AddRange(unichar);
						else
						{
							entry.Values.Add(result.ToArray());
							result.Clear();
						}
					}
				}
				entry.ID = i;
				TXT2.OriginalEntries.Add(entry);

				Entry entryEdited = new Entry();
				foreach (byte[] value in entry.Values)
					entryEdited.Values.Add(value);
				entryEdited.Value = entry.Value;
				entryEdited.ID = entry.ID;
				TXT2.Entries.Add(entryEdited);
			}

			PaddingSeek(br);
		}

		private void PaddingSeek(BinaryReaderX br)
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
				BinaryWriterX bw = new BinaryWriterX(fs);

				// Byte Order
				bw.ByteOrder = Header.ByteOrderMark[0] == 0xFF ? ByteOrder.LittleEndian : ByteOrder.BigEndian;

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
						WriteLBL1(bw);
					else if (section == "NLI1")
						WriteNLI1(bw);
					else if (section == "ATR1")
						WriteATR1(bw);
					else if (section == "TXT2")
						WriteTXT2(bw);
				}

				// Update FileSize
				long fileSize = bw.BaseStream.Position;
				bw.BaseStream.Seek(Header.FileSizeOffset, SeekOrigin.Begin);
				bw.Write((UInt32)fileSize);

				bw.Close();
			}
			catch (Exception)
			{ }

			return result;
		}

		private bool WriteLBL1(BinaryWriterX bw)
		{
			bool result = false;

			try
			{
				// Calculate Section Size
				UInt32 newSize = (UInt32)(LBL1.Unknown2.Length + LBL1.Unknown3.Length);

				foreach (Entry lbl in LBL1.Labels)
				{
					newSize += (UInt32)(sizeof(byte) + lbl.Value.Length + sizeof(UInt32));
				}

				bw.Write(LBL1.Identifier);
				bw.Write(newSize);
				bw.Write(LBL1.Unknown1);
				bw.Write(LBL1.Unknown2);
				bw.Write(LBL1.Unknown3);

				foreach (Entry lbl in LBL1.Labels)
				{
					bw.Write(Convert.ToByte(lbl.Length));
					bw.Write(lbl.Value);
					bw.Write(lbl.ID);
				}

				PaddingWrite(bw);

				result = true;
			}
			catch(Exception)
			{
				result = false;
			}

			return result;
		}

		private bool WriteNLI1(BinaryWriterX bw)
		{
			bool result = false;

			try
			{
				bw.Write(NLI1.Identifier);
				bw.Write(NLI1.SectionSize);
				bw.Write(NLI1.Unknown1);
				bw.Write(NLI1.Unknown2);

				PaddingWrite(bw);

				result = true;
			}
			catch (Exception)
			{
				result = false;
			}

			return result;
		}

		private bool WriteATR1(BinaryWriterX bw)
		{
			bool result = false;

			try
			{
				bw.Write(ATR1.Identifier);
				bw.Write(ATR1.SectionSize);
				bw.Write(ATR1.Unknown1);
				bw.Write(ATR1.Unknown2);

				PaddingWrite(bw);

				result = true;
			}
			catch (Exception)
			{
				result = false;
			}

			return result;
		}

		private bool WriteTXT2(BinaryWriterX bw)
		{
			bool result = false;

			try
			{
				// Calculate Section Size
				UInt32 newSize = (UInt32)(TXT2.NumberOfStrings * sizeof(UInt32) + sizeof(UInt32));

				for (int i = 0; i < TXT2.NumberOfStrings; i++)
				{
					foreach (byte[] value in TXT2.Entries[i].Values)
					{
						newSize += (UInt32)(value.Length + 2);
					}
				}

				bw.Write(TXT2.Identifier);
				bw.Write(newSize);
				bw.Write(TXT2.Unknown1);
				long startOfStrings = bw.BaseStream.Position;
				bw.Write(TXT2.NumberOfStrings);

				List<UInt32> offsets = new List<UInt32>();
				UInt32 offsetsLength = TXT2.NumberOfStrings * sizeof(UInt32) + sizeof(UInt32);
				UInt32 runningTotal = 0;
				for (int i = 0; i < TXT2.NumberOfStrings; i++)
				{
					offsets.Add(offsetsLength + runningTotal);
					foreach (byte[] value in TXT2.Entries[i].Values)
						runningTotal += ((UInt32)value.Length) + 2;
				}
				for (int i = 0; i < TXT2.NumberOfStrings; i++)
					bw.Write(offsets[i]);
				for (int i = 0; i < TXT2.NumberOfStrings; i++)
				{
					foreach (byte[] value in TXT2.Entries[i].Values)
					{
						if (Header.ByteOrderMark[0] == 0xFF)
							bw.Write(value);
						else
							for (int j = 0; j < value.Length; j += 2)
							{
								bw.Write(value[j + 1]);
								bw.Write(value[j]);
							}
						bw.Write('\0');
						bw.Write('\0');
					}
				}

				PaddingWrite(bw);

				result = true;
			}
			catch (Exception)
			{
				result = false;
			}

			return result;
		}

		private void PaddingWrite(BinaryWriterX bw)
		{
			long remainder = bw.BaseStream.Position % 16;
			if (remainder > 0)
				for (int i = 0; i < 16 - remainder; i++)
					bw.Write(paddingChar);
		}
	}
}