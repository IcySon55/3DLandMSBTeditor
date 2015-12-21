using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MsbtEditor
{
	public enum ByteOrder
	{
		LittleEndian = 0,
		BigEndian = 1
	}

	class BinaryReaderX : BinaryReader
	{
		public ByteOrder ByteOrder { get; set; }

		public BinaryReaderX(Stream input, ByteOrder byteOrder = ByteOrder.LittleEndian)
			: base(input)
		{
			ByteOrder = byteOrder;
		}

		public BinaryReaderX(Stream input, Encoding encoding, ByteOrder byteOrder = ByteOrder.LittleEndian)
			: base(input, encoding)
		{
			ByteOrder = byteOrder;
		}

		public override short ReadInt16()
		{
			if (ByteOrder == ByteOrder.LittleEndian)
				return base.ReadInt16();
			else
				return BitConverter.ToInt16(base.ReadBytes(2).Reverse().ToArray(), 0);
		}

		public override int ReadInt32()
		{
			if (ByteOrder == ByteOrder.LittleEndian)
				return base.ReadInt32();
			else
				return BitConverter.ToInt16(base.ReadBytes(4).Reverse().ToArray(), 0);
		}

		public override long ReadInt64()
		{
			if (ByteOrder == ByteOrder.LittleEndian)
				return base.ReadInt64();
			else
				return BitConverter.ToInt16(base.ReadBytes(8).Reverse().ToArray(), 0);
		}

		public override ushort ReadUInt16()
		{
			if (ByteOrder == ByteOrder.LittleEndian)
				return base.ReadUInt16();
			else
				return BitConverter.ToUInt16(base.ReadBytes(2).Reverse().ToArray(), 0);
		}

		public override uint ReadUInt32()
		{
			if (ByteOrder == ByteOrder.LittleEndian)
				return base.ReadUInt32();
			else
				return BitConverter.ToUInt16(base.ReadBytes(4).Reverse().ToArray(), 0);
		}

		public override ulong ReadUInt64()
		{
			if (ByteOrder == ByteOrder.LittleEndian)
				return base.ReadUInt64();
			else
				return BitConverter.ToUInt16(base.ReadBytes(8).Reverse().ToArray(), 0);
		}
	}

	class BinaryWriterX : BinaryWriter
	{
		public ByteOrder ByteOrder { get; set; }

		public BinaryWriterX(Stream input, ByteOrder byteOrder = ByteOrder.LittleEndian)
			: base(input)
		{
			ByteOrder = byteOrder;
		}

		public BinaryWriterX(Stream input, Encoding encoding, ByteOrder byteOrder = ByteOrder.LittleEndian)
			: base(input, encoding)
		{
			ByteOrder = byteOrder;
		}

		public override void Write(short value)
		{
			if (ByteOrder == ByteOrder.LittleEndian)
				base.Write(value);
			else
				base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
		}

		public override void Write(int value)
		{
			if (ByteOrder == ByteOrder.LittleEndian)
				base.Write(value);
			else
				base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
		}

		public override void Write(long value)
		{
			if (ByteOrder == ByteOrder.LittleEndian)
				base.Write(value);
			else
				base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
		}

		public override void Write(ushort value)
		{
			if (ByteOrder == ByteOrder.LittleEndian)
				base.Write(value);
			else
				base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
		}

		public override void Write(uint value)
		{
			if (ByteOrder == ByteOrder.LittleEndian)
				base.Write(value);
			else
				base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
		}

		public override void Write(ulong value)
		{
			if (ByteOrder == ByteOrder.LittleEndian)
				base.Write(value);
			else
				base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
		}
	}
}