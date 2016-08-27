using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MsbtEditor.UMSBT
{
	public class FileEntry : IComparable<FileEntry>
	{
		public UInt32 Offset;
		public UInt32 Size;
		public UInt32 Index;

		public int CompareTo(FileEntry rhs)
		{
			return Index.CompareTo(rhs.Index);
		}
	}

	public class InvalidUMSBTException : Exception
	{
		public InvalidUMSBTException(string message) : base(message) { }
	}

	public class UMSBT
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
					// Read in file headers
					List<FileEntry> entries = new List<FileEntry>();
					FileEntry entry = new FileEntry();
					uint index = 0;

					while (1 == 1 && br.BaseStream.Position < br.BaseStream.Length)
					{
						entry.Offset = br.ReadUInt32();
						entry.Size = br.ReadUInt32();
						entry.Index = index;

						if (entry.Offset == 0 && entry.Size == 0)
							break;
						else
							entries.Add(entry);

						entry = new FileEntry();
						index++;
					}

					// Extract!
					for (int i = 0; i < entries.Count; i++)
					{
						FileEntry fe = entries[i];
						string extension = ".msbt";
						string outFile = Path.Combine(path, fe.Index.ToString("00000000") + extension);

						Debug.Print("[" + fe.Offset.ToString("X8") + "] " + fe.Index + extension);

						if (!File.Exists(outFile) || (File.Exists(outFile) && overwrite))
						{
							FileStream fsr = new FileStream(outFile, FileMode.Create, FileAccess.Write, FileShare.None);
							BinaryWriterX bw = new BinaryWriterX(fsr);

							br.BaseStream.Seek(fe.Offset, SeekOrigin.Begin);
							bw.Write(br.ReadBytes((int)fe.Size));
							bw.Close();
						}
					}

					result = entries.Count + " files were found and " + entries.Count + " files were successfully extracted!";
				}
				catch (InvalidUMSBTException ibex)
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

		public static string Pack(string filename, string path)
		{
			string result = "Files successfully packed.";

			if (Directory.Exists(path))
			{
				FileStream fs = System.IO.File.Create(filename);
				BinaryWriterX bw = new BinaryWriterX(fs);

				try
				{
					string[] files = Directory.GetFiles(path, "*.msbt");

					UInt32 offsetsLength = ((UInt32)files.Length + 1) * (sizeof(UInt32) * 2);
					UInt32 runningTotal = 0;

					foreach (string file in files)
					{
						FileInfo fi = new FileInfo(file);

						UInt32 offset = offsetsLength + runningTotal;
						UInt32 size = (UInt32)fi.Length;
						runningTotal += (UInt32)fi.Length;

						bw.Write(offset);
						bw.Write(size);
					}

					bw.Write(0x00000000);
					bw.Write(0x00000000);

					foreach (string file in files)
					{
						FileStream fs2 = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
						BinaryReaderX br = new BinaryReaderX(fs2);

						bw.Write(br.ReadBytes((Int32)fs2.Length));
						br.Close();
					}
				}
				catch (Exception ex)
				{
					result = ex.Message;
				}
				finally
				{
					bw.Close();
				}
			}

			return result;
		}
	}
}