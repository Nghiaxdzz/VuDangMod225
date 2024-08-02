using System;
using System.Text;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class myReader
{
	// Token: 0x060005AE RID: 1454 RVA: 0x00004B68 File Offset: 0x00002D68
	public myReader()
	{
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x000052B6 File Offset: 0x000034B6
	public myReader(sbyte[] data)
	{
		this.buffer = data;
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x00064DAC File Offset: 0x00062FAC
	public myReader(string filename)
	{
		TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
		this.buffer = mSystem.convertToSbyte(textAsset.bytes);
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00064DE8 File Offset: 0x00062FE8
	public sbyte readSByte()
	{
		bool flag = this.posRead < this.buffer.Length;
		if (flag)
		{
			sbyte[] array = this.buffer;
			int num = this.posRead;
			this.posRead = num + 1;
			return array[num];
		}
		this.posRead = this.buffer.Length;
		throw new Exception(" loi doc sbyte eof ");
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x00064E40 File Offset: 0x00063040
	public sbyte readsbyte()
	{
		return this.readSByte();
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x00064E40 File Offset: 0x00063040
	public sbyte readByte()
	{
		return this.readSByte();
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x000052C7 File Offset: 0x000034C7
	public void mark(int readlimit)
	{
		this.posMark = this.posRead;
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x000052D6 File Offset: 0x000034D6
	public void reset()
	{
		this.posRead = this.posMark;
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x00064E58 File Offset: 0x00063058
	public byte readUnsignedByte()
	{
		return myReader.convertSbyteToByte(this.readSByte());
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x00064E78 File Offset: 0x00063078
	public short readShort()
	{
		short num = 0;
		for (int i = 0; i < 2; i++)
		{
			num = (short)(num << 8);
			short num2 = num;
			short num3 = 255;
			sbyte[] array = this.buffer;
			int num4 = this.posRead;
			this.posRead = num4 + 1;
			num = (num2 | (num3 & array[num4]));
		}
		return num;
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x00064ECC File Offset: 0x000630CC
	public ushort readUnsignedShort()
	{
		ushort num = 0;
		for (int i = 0; i < 2; i++)
		{
			num = (ushort)(num << 8);
			ushort num2 = num;
			ushort num3 = 255;
			sbyte[] array = this.buffer;
			int num4 = this.posRead;
			this.posRead = num4 + 1;
			num = (num2 | (num3 & array[num4]));
		}
		return num;
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x00064F20 File Offset: 0x00063120
	public int readInt()
	{
		int num = 0;
		for (int i = 0; i < 4; i++)
		{
			num <<= 8;
			int num2 = num;
			int num3 = 255;
			sbyte[] array = this.buffer;
			int num4 = this.posRead;
			this.posRead = num4 + 1;
			num = (num2 | (num3 & array[num4]));
		}
		return num;
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x00064F70 File Offset: 0x00063170
	public long readLong()
	{
		long num = 0L;
		for (int i = 0; i < 8; i++)
		{
			num <<= 8;
			long num2 = num;
			long num3 = 255L;
			sbyte[] array = this.buffer;
			int num4 = this.posRead;
			this.posRead = num4 + 1;
			num = (num2 | (num3 & array[num4]));
		}
		return num;
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x00064FC0 File Offset: 0x000631C0
	public bool readBool()
	{
		return this.readSByte() > 0;
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x00064FC0 File Offset: 0x000631C0
	public bool readBoolean()
	{
		return this.readSByte() > 0;
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x00064FE0 File Offset: 0x000631E0
	public string readString()
	{
		short num = this.readShort();
		byte[] array = new byte[(int)num];
		for (int i = 0; i < (int)num; i++)
		{
			array[i] = myReader.convertSbyteToByte(this.readSByte());
		}
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		return utf8Encoding.GetString(array);
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x00064FE0 File Offset: 0x000631E0
	public string readStringUTF()
	{
		short num = this.readShort();
		byte[] array = new byte[(int)num];
		for (int i = 0; i < (int)num; i++)
		{
			array[i] = myReader.convertSbyteToByte(this.readSByte());
		}
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		return utf8Encoding.GetString(array);
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x00065034 File Offset: 0x00063234
	public string readUTF()
	{
		return this.readStringUTF();
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x0006504C File Offset: 0x0006324C
	public int read()
	{
		bool flag = this.posRead < this.buffer.Length;
		int result;
		if (flag)
		{
			result = (int)this.readSByte();
		}
		else
		{
			result = -1;
		}
		return result;
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x00065080 File Offset: 0x00063280
	public int read(ref sbyte[] data)
	{
		bool flag = data == null;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			int num = 0;
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = this.readSByte();
				bool flag2 = this.posRead > this.buffer.Length;
				if (flag2)
				{
					return -1;
				}
				num++;
			}
			result = num;
		}
		return result;
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x000650E4 File Offset: 0x000632E4
	public void readFully(ref sbyte[] data)
	{
		bool flag = data != null && data.Length + this.posRead <= this.buffer.Length;
		if (flag)
		{
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = this.readSByte();
			}
		}
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x00065138 File Offset: 0x00063338
	public int available()
	{
		return this.buffer.Length - this.posRead;
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x00050910 File Offset: 0x0004EB10
	public static byte convertSbyteToByte(sbyte var)
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

	// Token: 0x060005C5 RID: 1477 RVA: 0x00050938 File Offset: 0x0004EB38
	public static byte[] convertSbyteToByte(sbyte[] var)
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

	// Token: 0x060005C6 RID: 1478 RVA: 0x000052E5 File Offset: 0x000034E5
	public void Close()
	{
		this.buffer = null;
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x000052E5 File Offset: 0x000034E5
	public void close()
	{
		this.buffer = null;
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x0006515C File Offset: 0x0006335C
	public void read(ref sbyte[] data, int arg1, int arg2)
	{
		bool flag = data == null;
		if (!flag)
		{
			for (int i = 0; i < arg2; i++)
			{
				data[i + arg1] = this.readSByte();
				bool flag2 = this.posRead > this.buffer.Length;
				if (flag2)
				{
					break;
				}
			}
		}
	}

	// Token: 0x04000D57 RID: 3415
	public sbyte[] buffer;

	// Token: 0x04000D58 RID: 3416
	private int posRead;

	// Token: 0x04000D59 RID: 3417
	private int posMark;

	// Token: 0x04000D5A RID: 3418
	private static string fileName;

	// Token: 0x04000D5B RID: 3419
	private static int status;
}
