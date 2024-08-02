using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x0200007B RID: 123
public class myWriter
{
	// Token: 0x060005DB RID: 1499 RVA: 0x00065390 File Offset: 0x00063590
	public void writeSByte(sbyte value)
	{
		this.checkLenght(0);
		sbyte[] array = this.buffer;
		int num = this.posWrite;
		this.posWrite = num + 1;
		array[num] = value;
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x000653C0 File Offset: 0x000635C0
	public void writeSByteUncheck(sbyte value)
	{
		sbyte[] array = this.buffer;
		int num = this.posWrite;
		this.posWrite = num + 1;
		array[num] = value;
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x00005355 File Offset: 0x00003555
	public void writeByte(sbyte value)
	{
		this.writeSByte(value);
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x00005360 File Offset: 0x00003560
	public void writeByte(int value)
	{
		this.writeSByte((sbyte)value);
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x0000536C File Offset: 0x0000356C
	public void writeChar(char value)
	{
		this.writeSByte(0);
		this.writeSByte((sbyte)value);
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x00005360 File Offset: 0x00003560
	public void writeUnsignedByte(byte value)
	{
		this.writeSByte((sbyte)value);
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x000653E8 File Offset: 0x000635E8
	public void writeUnsignedByte(byte[] value)
	{
		this.checkLenght(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			this.writeSByteUncheck((sbyte)value[i]);
		}
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x00065420 File Offset: 0x00063620
	public void writeSByte(sbyte[] value)
	{
		this.checkLenght(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			this.writeSByteUncheck(value[i]);
		}
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x00065458 File Offset: 0x00063658
	public void writeShort(short value)
	{
		this.checkLenght(2);
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x00065494 File Offset: 0x00063694
	public void writeShort(int value)
	{
		this.checkLenght(2);
		short num = (short)value;
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(num >> i * 8));
		}
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x00065458 File Offset: 0x00063658
	public void writeUnsignedShort(ushort value)
	{
		this.checkLenght(2);
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x000654D4 File Offset: 0x000636D4
	public void writeInt(int value)
	{
		this.checkLenght(4);
		for (int i = 3; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x00065510 File Offset: 0x00063710
	public void writeLong(long value)
	{
		this.checkLenght(8);
		for (int i = 7; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x00005380 File Offset: 0x00003580
	public void writeBoolean(bool value)
	{
		this.writeSByte(value ? 1 : 0);
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x00005380 File Offset: 0x00003580
	public void writeBool(bool value)
	{
		this.writeSByte(value ? 1 : 0);
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x0006554C File Offset: 0x0006374C
	public void writeString(string value)
	{
		char[] array = value.ToCharArray();
		this.writeShort((short)array.Length);
		this.checkLenght(array.Length);
		for (int i = 0; i < array.Length; i++)
		{
			this.writeSByteUncheck((sbyte)array[i]);
		}
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x00065598 File Offset: 0x00063798
	public void writeUTF(string value)
	{
		Encoding unicode = Encoding.Unicode;
		Encoding encoding = Encoding.GetEncoding(65001);
		byte[] bytes = unicode.GetBytes(value);
		byte[] array = Encoding.Convert(unicode, encoding, bytes);
		this.writeShort((short)array.Length);
		this.checkLenght(array.Length);
		foreach (sbyte value2 in array)
		{
			this.writeSByteUncheck(value2);
		}
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x00005355 File Offset: 0x00003555
	public void write(sbyte value)
	{
		this.writeSByte(value);
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x00065608 File Offset: 0x00063808
	public void write(ref sbyte[] data, int arg1, int arg2)
	{
		bool flag = data == null;
		if (!flag)
		{
			for (int i = 0; i < arg2; i++)
			{
				this.writeSByte(data[i + arg1]);
				bool flag2 = this.posWrite > this.buffer.Length;
				if (flag2)
				{
					break;
				}
			}
		}
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x00005392 File Offset: 0x00003592
	public void write(sbyte[] value)
	{
		this.writeSByte(value);
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x00065658 File Offset: 0x00063858
	public sbyte[] getData()
	{
		bool flag = this.posWrite <= 0;
		sbyte[] result;
		if (flag)
		{
			result = null;
		}
		else
		{
			sbyte[] array = new sbyte[this.posWrite];
			for (int i = 0; i < this.posWrite; i++)
			{
				array[i] = this.buffer[i];
			}
			result = array;
		}
		return result;
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x000656B0 File Offset: 0x000638B0
	public void checkLenght(int ltemp)
	{
		bool flag = this.posWrite + ltemp > this.lenght;
		if (flag)
		{
			sbyte[] array = new sbyte[this.lenght + 1024 + ltemp];
			for (int i = 0; i < this.lenght; i++)
			{
				array[i] = this.buffer[i];
			}
			this.buffer = null;
			this.buffer = array;
			this.lenght += 1024 + ltemp;
		}
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x0006572C File Offset: 0x0006392C
	private static void convertString(string[] args)
	{
		string path = args[0];
		string path2 = args[1];
		using (StreamReader streamReader = new StreamReader(path, Encoding.Unicode))
		{
			using (StreamWriter streamWriter = new StreamWriter(path2, false, Encoding.UTF8))
			{
				myWriter.CopyContents(streamReader, streamWriter);
			}
		}
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x000657A0 File Offset: 0x000639A0
	private static void CopyContents(TextReader input, TextWriter output)
	{
		char[] array = new char[8192];
		int count;
		while ((count = input.Read(array, 0, array.Length)) != 0)
		{
			output.Write(array, 0, count);
		}
		output.Flush();
		string message = output.ToString();
		Debug.Log(message);
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x000657F0 File Offset: 0x000639F0
	public byte convertSbyteToByte(sbyte var)
	{
		bool flag = var > 0;
		byte result;
		if (flag)
		{
			result = (byte)var;
		}
		else
		{
			result = (byte)((int)var + 256);
		}
		return result;
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x00065818 File Offset: 0x00063A18
	public byte[] convertSbyteToByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			bool flag = var[i] > 0;
			if (flag)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)((int)var[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x0000539D File Offset: 0x0000359D
	public void Close()
	{
		this.buffer = null;
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0000539D File Offset: 0x0000359D
	public void close()
	{
		this.buffer = null;
	}

	// Token: 0x04000D5D RID: 3421
	public sbyte[] buffer = new sbyte[2048];

	// Token: 0x04000D5E RID: 3422
	private int posWrite;

	// Token: 0x04000D5F RID: 3423
	private int lenght = 2048;
}
