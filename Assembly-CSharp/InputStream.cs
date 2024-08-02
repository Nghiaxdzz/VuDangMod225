using System;

// Token: 0x0200004A RID: 74
public class InputStream : myReader
{
	// Token: 0x060003D0 RID: 976 RVA: 0x00004A8A File Offset: 0x00002C8A
	public InputStream()
	{
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00004A94 File Offset: 0x00002C94
	public InputStream(sbyte[] data)
	{
		this.buffer = data;
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00004AA5 File Offset: 0x00002CA5
	public InputStream(string filename) : base(filename)
	{
	}
}
