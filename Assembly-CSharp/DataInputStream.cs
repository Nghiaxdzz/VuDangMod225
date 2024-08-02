using System;
using System.Threading;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class DataInputStream
{
	// Token: 0x060001BF RID: 447 RVA: 0x000352D0 File Offset: 0x000334D0
	public DataInputStream(string filename)
	{
		TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
		this.r = new myReader(ArrayCast.cast(textAsset.bytes));
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x00003EEA File Offset: 0x000020EA
	public DataInputStream(sbyte[] data)
	{
		this.r = new myReader(data);
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x00035314 File Offset: 0x00033514
	public static void update()
	{
		bool flag = DataInputStream.status == 2;
		if (flag)
		{
			DataInputStream.status = 1;
			DataInputStream.istemp = DataInputStream.__getResourceAsStream(DataInputStream.filenametemp);
			DataInputStream.status = 0;
		}
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0003534C File Offset: 0x0003354C
	public static DataInputStream getResourceAsStream(string filename)
	{
		return DataInputStream.__getResourceAsStream(filename);
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x00035364 File Offset: 0x00033564
	private static DataInputStream _getResourceAsStream(string filename)
	{
		bool flag = DataInputStream.status != 0;
		if (flag)
		{
			for (int i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = DataInputStream.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = DataInputStream.status != 0;
			if (flag3)
			{
				Debug.LogError("CANNOT GET INPUTSTREAM " + filename + " WHEN GETTING " + DataInputStream.filenametemp);
				return null;
			}
		}
		DataInputStream.istemp = null;
		DataInputStream.filenametemp = filename;
		DataInputStream.status = 2;
		int j;
		for (j = 0; j < 500; j++)
		{
			Thread.Sleep(5);
			bool flag4 = DataInputStream.status == 0;
			if (flag4)
			{
				break;
			}
		}
		bool flag5 = j == 500;
		DataInputStream result;
		if (flag5)
		{
			Debug.LogError("TOO LONG FOR CREATE INPUTSTREAM " + filename);
			DataInputStream.status = 0;
			result = null;
		}
		else
		{
			result = DataInputStream.istemp;
		}
		return result;
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x00035458 File Offset: 0x00033658
	private static DataInputStream __getResourceAsStream(string filename)
	{
		DataInputStream result;
		try
		{
			result = new DataInputStream(filename);
		}
		catch (Exception)
		{
			result = null;
		}
		return result;
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x00035488 File Offset: 0x00033688
	public short readShort()
	{
		return this.r.readShort();
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x000354A8 File Offset: 0x000336A8
	public int readInt()
	{
		return this.r.readInt();
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x000354C8 File Offset: 0x000336C8
	public int read()
	{
		return (int)this.r.readUnsignedByte();
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00003F00 File Offset: 0x00002100
	public void read(ref sbyte[] data)
	{
		this.r.read(ref data);
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x00003F10 File Offset: 0x00002110
	public void close()
	{
		this.r.Close();
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00003F10 File Offset: 0x00002110
	public void Close()
	{
		this.r.Close();
	}

	// Token: 0x060001CB RID: 459 RVA: 0x000354E8 File Offset: 0x000336E8
	public string readUTF()
	{
		return this.r.readUTF();
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00035508 File Offset: 0x00033708
	public sbyte readByte()
	{
		return this.r.readByte();
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00035528 File Offset: 0x00033728
	public long readLong()
	{
		return this.r.readLong();
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00035548 File Offset: 0x00033748
	public bool readBoolean()
	{
		return this.r.readBoolean();
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00035568 File Offset: 0x00033768
	public int readUnsignedByte()
	{
		return (int)((byte)this.r.readByte());
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x00035588 File Offset: 0x00033788
	public int readUnsignedShort()
	{
		return (int)this.r.readUnsignedShort();
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x00003F00 File Offset: 0x00002100
	public void readFully(ref sbyte[] data)
	{
		this.r.read(ref data);
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x000355A8 File Offset: 0x000337A8
	public int available()
	{
		return this.r.available();
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x00003F1F File Offset: 0x0000211F
	internal void read(ref sbyte[] byteData, int p, int size)
	{
		throw new NotImplementedException();
	}

	// Token: 0x04000484 RID: 1156
	public myReader r;

	// Token: 0x04000485 RID: 1157
	private const int INTERVAL = 5;

	// Token: 0x04000486 RID: 1158
	private const int MAXTIME = 500;

	// Token: 0x04000487 RID: 1159
	public static DataInputStream istemp;

	// Token: 0x04000488 RID: 1160
	private static int status;

	// Token: 0x04000489 RID: 1161
	private static string filenametemp;
}
