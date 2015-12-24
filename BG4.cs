using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MsbtEditor
{
	class FileEntry : IComparable<FileEntry>
	{
		public UInt32 Offset;
		public UInt32 Size;
		public UInt32 Unknown1;
		public UInt16 NameIndex;

		public int CompareTo(FileEntry rhs)
		{
			return NameIndex.CompareTo(rhs.NameIndex);
		}
	}

	class InvalidBG4Exception : Exception
	{
		public InvalidBG4Exception(string message) : base(message) { }
	}

	class BG4
	{
		public static string Extract(string filename, string path, bool overwrite = true)
		{
			string result = "Files successfully extracted.";

			if (File.Exists(filename))
			{
				FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
				BinaryReaderX br = new BinaryReaderX(fs);

				try
				{
					string magic = Encoding.ASCII.GetString(br.ReadBytes(4));

					if (magic != "BG4\0")
						throw new InvalidBG4Exception("The file provided is not a valid BG4 archive.");

					// TODO: Decipher header values

					while (br.ReadUInt32() != 0xFFFFFFFF) { } // Jump to the end of the header
					br.ReadBytes(2); // 00 00

					// Loop through file details
					bool eoh = false;
					List<FileEntry> entries = new List<FileEntry>();
					FileEntry entry = new FileEntry();

					while (!eoh)
					{
						if (br.PeekString(10) == "(invalid)\0")
						{
							br.ReadBytes(10);
							eoh = true;
						}
						else
						{
							while (br.PeekString() != Encoding.ASCII.GetString(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }))
							{
								entry.Offset = br.ReadUInt32();

								if (entry.Offset > 0)
								{
									entry.Size = br.ReadUInt32();
									entry.Unknown1 = br.ReadUInt32();
									entry.NameIndex = br.ReadUInt16();
									entries.Add(entry);
									entry = new FileEntry();
								}

								// File entry closer
								if (br.PeekString() == Encoding.ASCII.GetString(new byte[] { 0x00, 0x00, 0x00, 0x80 }))
									br.ReadBytes(4); // 00 00 00 80
							}

							br.ReadBytes(4); // FF FF FF FF
							br.ReadBytes(2); // 00 00
						}
					}

					// Sort the file entries into NameIndex order
					entries.Sort();

					// Filenames
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

							filenames.Add(name);
						}
					}

					// Extract!
					for (int i = 0; i < entries.Count; i++)
					{
						FileEntry fe = entries[i];

						FileInfo fi = new FileInfo(filename);
						FileStream fsr = new FileStream(Path.Combine(path, filenames[i] + ".bin"), FileMode.Create, FileAccess.Write, FileShare.None);
						BinaryWriterX bw = new BinaryWriterX(fsr);

						br.BaseStream.Seek(fe.Offset, SeekOrigin.Begin);
						bw.Write(br.ReadBytes((int)fe.Size));
						bw.Close();
					}

					result = entries.Count + " files successfully extracted!";
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