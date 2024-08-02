using System;

// Token: 0x02000006 RID: 6
public class ArrayCast
{
	// Token: 0x0600000E RID: 14 RVA: 0x00006A50 File Offset: 0x00004C50
	public static sbyte[] cast(byte[] data)
	{
		sbyte[] array = new sbyte[data.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (sbyte)data[i];
		}
		return array;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00006A88 File Offset: 0x00004C88
	public static byte[] cast(sbyte[] data)
	{
		byte[] array = new byte[data.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (byte)data[i];
		}
		return array;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00006AC0 File Offset: 0x00004CC0
	public static char[] ToCharArray(sbyte[] data)
	{
		char[] array = new char[data.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (char)data[i];
		}
		return array;
	}
}
