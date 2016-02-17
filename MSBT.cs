using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MsbtEditor
{
	public enum EncodingByte : byte
	{
		UTF8 = 0x00,
		Unicode = 0x01
	}

	public class Header
	{
		public byte[] Identifier; // MsgStdBn
		public byte[] ByteOrderMark;
		public UInt16 Unknown1; // Always 0x0000
		public EncodingByte EncodingByte;
		public byte Unknown2; // Always 0x03
		public UInt16 NumberOfSections;
		public UInt16 Unknown3; // Always 0x0000
		public UInt32 FileSize;
		public byte[] Unknown4; // Always 0x0000 0000 0000 0000 0000

		public UInt32 FileSizeOffset;
	}

	public class LBL1
	{
		public byte[] Identifier; // LBL1
		public UInt32 SectionSize; // Begins after Unknown1
		public byte[] Unknown1; // Always 0x0000 0000
		public byte[] Unknown2;
		public byte[] Unknown3; // Large collection of unknown values

		public List<Entry> Labels;
	}

	public class NLI1
	{
		public byte[] Identifier; // NLI1
		public UInt32 SectionSize; // Begins after Unknown1
		public byte[] Unknown1; // Always 0x0000 0000
		public byte[] Unknown2; // Tons of unknown data
	}

	public class ATR1
	{
		public byte[] Identifier; // ATR1
		public UInt32 SectionSize; // Begins after Unknown1
		public byte[] Unknown1; // Always 0x0000 0000
		public UInt32 NumberOfAttributes;
		public byte[] Unknown2;

		public List<UInt32> Attributes;
	}

	public class TSY1
	{
		public byte[] Identifier; // TSY1
		public UInt32 SectionSize; // Begins after Unknown1
		public byte[] Unknown1; // Always 0x0000 0000
		public byte[] Unknown2;
	}

	public class TXT2
	{
		public byte[] Identifier; // TXT2
		public UInt32 SectionSize; // Begins after Unknown1
		public byte[] Unknown1; // Always 0x0000 0000
		public UInt32 NumberOfStrings;

		public List<Entry> OriginalEntries;
		public List<Entry> Entries;
	}

	public class Entry
	{
		public UInt32 Length;
		public List<Value> Values = new List<Value>();
		public byte[] Value;
		public Int32 Index;
		public Encoding FileEncoding = Encoding.Unicode;

		public override string ToString()
		{
			return (Length > 0 ? Encoding.ASCII.GetString(Value) : (Index + 1).ToString());
		}

		public string Preview()
		{
			string result = string.Empty;
			foreach (Value value in Values)
				result += FileEncoding.GetString(value.Data).Replace("\n", "\r\n");
			return result;
		}
	}

	public class Value
	{
		public byte[] Data;
		public bool Editable = true;
		public bool NullTerminated = true;
	}

	public class InvalidMSBTException : Exception
	{
		public InvalidMSBTException(string message) : base(message) { }
	}

	public class MSBT
	{
		public FileInfo File { get; set; }
		public bool HasLabels { get; set; }

		public Header Header = new Header();
		public LBL1 LBL1 = new LBL1();
		public NLI1 NLI1 = new NLI1();
		public ATR1 ATR1 = new ATR1();
		public TSY1 TSY1 = new TSY1();
		public TXT2 TXT2 = new TXT2();
		public List<string> SectionOrder = new List<string>();
		public Encoding FileEncoding = Encoding.Unicode;

		private byte paddingChar = 0xAB;

		public MSBT(string filename)
		{
			File = new FileInfo(filename);

			if (File.Exists && filename.Length > 0)
			{
				FileStream fs = System.IO.File.Open(File.FullName, FileMode.Open, FileAccess.Read, FileShare.None);
				BinaryReaderX br = new BinaryReaderX(fs);

				// Initialize Members
				LBL1.Labels = new List<Entry>();
				ATR1.Attributes = new List<UInt32>();
				TXT2.OriginalEntries = new List<Entry>();
				TXT2.Entries = new List<Entry>();

				// Header
				Header.Identifier = br.ReadBytes(8);
				if (Encoding.ASCII.GetString(Header.Identifier) != "MsgStdBn")
					throw new InvalidMSBTException("The file provided is not a valid MSBT file.");
				Header.ByteOrderMark = br.ReadBytes(2);

				// Byte Order
				br.ByteOrder = Header.ByteOrderMark[0] > Header.ByteOrderMark[1] ? ByteOrder.LittleEndian : ByteOrder.BigEndian;

				Header.Unknown1 = br.ReadUInt16();
				Header.EncodingByte = (EncodingByte)br.ReadByte();
				FileEncoding = (Header.EncodingByte == EncodingByte.UTF8 ? Encoding.UTF8 : Encoding.Unicode);
				Header.Unknown2 = br.ReadByte();
				Header.NumberOfSections = br.ReadUInt16();
				Header.Unknown3 = br.ReadUInt16();
				Header.FileSizeOffset = (UInt32)br.BaseStream.Position;
				Header.FileSize = br.ReadUInt32();
				Header.Unknown4 = br.ReadBytes(10);

				if (Header.FileSize != br.BaseStream.Length)
					throw new InvalidMSBTException("The file provided is not a valid MSBT file.");

				SectionOrder.Clear();
				for (int i = 0; i < Header.NumberOfSections; i++)
				{
					// Section Detection
					if (br.PeekString() == "LBL1")
					{
						ReadLBL1(br);
						SectionOrder.Add("LBL1");
					}
					else if (br.PeekString() == "NLI1")
					{
						ReadNLI1(br);
						SectionOrder.Add("NLI1");
					}
					else if (br.PeekString() == "ATR1")
					{
						ReadATR1(br);
						SectionOrder.Add("ATR1");
					}
					else if (br.PeekString() == "TSY1")
					{
						ReadTSY1(br);
						SectionOrder.Add("TSY1");
					}
					else if (br.PeekString() == "TXT2")
					{
						ReadTXT2(br);
						SectionOrder.Add("TXT2");
					}
				}

				br.Close();
			}
		}

		private void ReadLBL1(BinaryReaderX br)
		{
			// TODO: Continue reverse engineering the LBL1 section because the magic value below shouldn't be the end game
			long offset = br.BaseStream.Position;
			LBL1.Identifier = br.ReadBytes(4);
			LBL1.SectionSize = br.ReadUInt32();
			LBL1.Unknown1 = br.ReadBytes(8);
			LBL1.Unknown2 = br.ReadBytes(8);
			uint startOfLabels = 0x35C; // Magic LBL1 label start position
			LBL1.Unknown3 = br.ReadBytes((int)(startOfLabels - br.BaseStream.Position));

			while (br.BaseStream.Position < (offset + LBL1.Identifier.Length + sizeof(UInt32) + LBL1.Unknown1.Length + LBL1.SectionSize))
			{
				Entry lbl = new Entry();
				lbl.Length = Convert.ToUInt32(br.ReadByte());
				lbl.Value = br.ReadBytes((int)lbl.Length);
				lbl.Index = br.ReadInt32();
				lbl.FileEncoding = FileEncoding;
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
			ATR1.NumberOfAttributes = br.ReadUInt32();
			ATR1.Unknown2 = br.ReadBytes((int)ATR1.SectionSize - sizeof(UInt32)); // Read in the rest of the section at once since we don't know what it's for

			PaddingSeek(br);
		}

		private void ReadTSY1(BinaryReaderX br)
		{
			TSY1.Identifier = br.ReadBytes(4);
			TSY1.SectionSize = br.ReadUInt32();
			TSY1.Unknown1 = br.ReadBytes(8);
			TSY1.Unknown2 = br.ReadBytes((int)TSY1.SectionSize); // Read in the entire section at once since we don't know what it's for

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
					if (br.BaseStream.Position >= nextOffset || br.BaseStream.Position >= Header.FileSize)
					{
						eos = true;
						br.BaseStream.Seek(nextOffset, SeekOrigin.Begin);
					}
					else
					{
						if (Header.EncodingByte == EncodingByte.UTF8)
						{
							byte unichar = br.ReadByte();

							if (unichar != 0x0)
								result.Add(unichar);
							else
							{
								Value val = new Value();
								val.Data = result.ToArray();

								if (result.Count == 0)
									val.Editable = false;

								entry.Values.Add(val);
								result.Clear();
							}
						}
						else
						{
							byte[] unichar = br.ReadBytes(2);

							if (Header.ByteOrderMark[0] == 0xFE)
								Array.Reverse(unichar);

							if (unichar[0] != 0x0 || unichar[1] != 0x0)
								result.AddRange(unichar);
							else
							{
								Value val = new Value();
								val.Data = result.ToArray();

								if (result.Count == 0)
									val.Editable = false;

								entry.Values.Add(val);
								result.Clear();
							}
						}
					}
				}

				// Strange extended string without null termination
				if (result.Count > 1)
				{
					Value finalVal = new Value();
					finalVal.Data = result.ToArray();
					finalVal.Editable = false;
					finalVal.NullTerminated = false;
					entry.Values.Add(finalVal);
				}

				entry.Index = i;
				entry.FileEncoding = FileEncoding;
				TXT2.OriginalEntries.Add(entry);

				// Duplicate entries for editing
				Entry entryEdited = new Entry();
				foreach (Value value in entry.Values)
				{
					Value val = new Value();
					val.Data = value.Data;
					val.Editable = value.Editable;
					val.NullTerminated = value.NullTerminated;
					entryEdited.Values.Add(val);
				}
				entryEdited.Value = entry.Value;
				entryEdited.Index = entry.Index;
				entryEdited.FileEncoding = FileEncoding;
				TXT2.Entries.Add(entryEdited);
			}

			PaddingSeek(br);
		}

		private void PaddingSeek(BinaryReaderX br)
		{
			long remainder = br.BaseStream.Position % 16;
			if (remainder > 0)
			{
				paddingChar = br.ReadByte();
				br.BaseStream.Seek(-1, SeekOrigin.Current);
				br.BaseStream.Seek(16 - remainder, SeekOrigin.Current);
			}
		}

		public bool Save()
		{
			bool result = false;

			try
			{
				FileStream fs = System.IO.File.Create(File.FullName);
				BinaryWriterX bw = new BinaryWriterX(fs);

				// Byte Order
				bw.ByteOrder = Header.ByteOrderMark[0] > Header.ByteOrderMark[1] ? ByteOrder.LittleEndian : ByteOrder.BigEndian;

				// Header
				bw.Write(Header.Identifier);
				bw.Write(Header.ByteOrderMark);
				bw.Write(Header.Unknown1);
				bw.Write((byte)Header.EncodingByte);
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
					else if (section == "TSY1")
						WriteTSY1(bw);
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
					bw.Write(lbl.Index);
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
				bw.Write(ATR1.NumberOfAttributes);
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

		private bool WriteTSY1(BinaryWriterX bw)
		{
			bool result = false;

			try
			{
				bw.Write(TSY1.Identifier);
				bw.Write(TSY1.SectionSize);
				bw.Write(TSY1.Unknown1);
				bw.Write(TSY1.Unknown2);

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
					foreach (Value value in TXT2.Entries[i].Values)
					{
						newSize += (UInt32)(value.Data.Length + (value.NullTerminated ? (Header.EncodingByte == EncodingByte.UTF8 ? 1 : 2) : 0));
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
					foreach (Value value in TXT2.Entries[i].Values)
						runningTotal += (UInt32)(value.Data.Length + (value.NullTerminated ? (Header.EncodingByte == EncodingByte.UTF8 ? 1 : 2) : 0));
				}
				for (int i = 0; i < TXT2.NumberOfStrings; i++)
					bw.Write(offsets[i]);
				for (int i = 0; i < TXT2.NumberOfStrings; i++)
				{
					for (int j = 0; j < TXT2.Entries[i].Values.Count; j++)
						TXT2.OriginalEntries[i].Values[j] = TXT2.Entries[i].Values[j];

					if (Header.EncodingByte == EncodingByte.UTF8)
					{
						foreach (Value value in TXT2.Entries[i].Values)
						{
							bw.Write(value.Data);

							if (value.NullTerminated)
							{
								bw.Write('\0');
							}
						}
					}
					else
					{
						foreach (Value value in TXT2.Entries[i].Values)
						{
							if (Header.ByteOrderMark[0] == 0xFF)
								bw.Write(value.Data);
							else
								for (int j = 0; j < value.Data.Length; j += 2)
								{
									bw.Write(value.Data[j + 1]);
									bw.Write(value.Data[j]);
								}
							if (value.NullTerminated)
							{
								bw.Write('\0');
								bw.Write('\0');
							}
						}
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