using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MsbtEditor.BG4
{
	public class FileEntry : IComparable<FileEntry>
	{
		public UInt32 Offset;
		public UInt32 Size;
		public UInt32 Unknown1;
		public UInt16 NameIndex;
		public bool Compressed;

		public int CompareTo(FileEntry rhs)
		{
			return NameIndex.CompareTo(rhs.NameIndex);
		}
	}

	public class InvalidBG4Exception : Exception
	{
		public InvalidBG4Exception(string message) : base(message) { }
	}

	public class BG4
	{
		public static string Extract(string filename, string path, bool overwrite = true)
		{
			string result = "Files successfully extracted.";

			if (File.Exists(filename) && new FileInfo(filename).Length > 0)
			{
				FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
				BinaryReaderX br = new BinaryReaderX(fs);

				try
				{
					string magic = Encoding.ASCII.GetString(br.ReadBytes(4));

					if (magic != "BG4\0")
						throw new InvalidBG4Exception("The file provided is not a valid BG4 archive.");

					ushort Constant1 = br.ReadUInt16();
					ushort NumberOfHeaders = br.ReadUInt16();
					uint SizeOfHeaders = br.ReadUInt32();
					ushort NumberOfHeadersDerived = br.ReadUInt16();
					ushort NumberOfHeadersMultiplier = br.ReadUInt16();

					// Read in file headers
					List<FileEntry> entries = new List<FileEntry>();
					FileEntry entry = new FileEntry();

					for (int i = 0; i < NumberOfHeaders; i++)
					{
						uint offsetTemp = br.ReadUInt32();
						if ((offsetTemp & 0x80000000) == 0x80000000) entry.Compressed = true;
						entry.Offset = entry.Compressed ? (offsetTemp ^ 0x80000000) : offsetTemp;
						entry.Size = br.ReadUInt32();
						entry.Unknown1 = br.ReadUInt32();
						entry.NameIndex = br.ReadUInt16();

						if (entry.Unknown1 != 0xFFFFFFFF)
							entries.Add(entry);

						entry = new FileEntry();
					}

					// Sort the file entries into NameIndex order
					entries.Sort();

					// Read in file names
					bool eofn = false;
					List<string> filenames = new List<string>();

					while (!eofn)
					{
						if (br.PeekString(2) == Encoding.ASCII.GetString(new byte[] { 0xFF, 0xFF }))
							eofn = true;
						else
						{
							bool eos = false;
							string name = string.Empty;

							while (!eos)
							{
								byte chr = br.ReadByte();

								if (chr == 0x00)
									eos = true;
								else
									name += (char)chr;
							}

							if (name != "(invalid)")
								filenames.Add(name);
						}
					}

					// Extract!
					for (int i = 0; i < entries.Count; i++)
					{
						FileEntry fe = entries[i];
						string extension = (!Regex.IsMatch(filenames[i], @"\..*?$") ? ".bin" : string.Empty);

						if (extension != string.Empty)
						{
							br.BaseStream.Seek(fe.Offset, SeekOrigin.Begin);
							magic = Encoding.ASCII.GetString(br.ReadBytes(8));

							if (magic.StartsWith("MsgStdBn"))
								extension = ".msbt";
							else if (magic.StartsWith("BCH"))
								extension = ".bch";
							else if (magic.StartsWith("PTX"))
								extension = ".ptx";

							// TODO: Add more known magic/extension pairs
						}

						Debug.Print("[" + fe.Offset.ToString("X8") + "] " + fe.NameIndex + " (" + fe.Unknown1 + ") " + filenames[i] + extension);

						FileInfo fi = new FileInfo(filename);
						FileStream fsr = new FileStream(Path.Combine(path, filenames[i] + extension), FileMode.Create, FileAccess.Write, FileShare.None);
						BinaryWriterX bw = new BinaryWriterX(fsr);

						br.BaseStream.Seek(fe.Offset, SeekOrigin.Begin);
						bw.Write(br.ReadBytes((int)fe.Size));
						bw.Close();
					}

					result = NumberOfHeaders + " header(s) were scanned and " + entries.Count + " files were successfully extracted!";
				}
				catch (InvalidBG4Exception ibex)
				{
					result = ibex.Message;
				}
				catch (Exception ex)
				{
					result = ex.Message;
				}
				finally
				{
					br.Close();
				}
			}

			return result;
		}

		public static void Pack(string filename, string path)
		{
			throw new NotImplementedException();
		}
	}
}