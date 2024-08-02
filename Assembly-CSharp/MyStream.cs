using System;

// Token: 0x02000079 RID: 121
public class MyStream
{
	// Token: 0x060005C9 RID: 1481 RVA: 0x000651AC File Offset: 0x000633AC
	public static DataInputStream readFile(string path)
	{
		path = Main.res + path;
		DataInputStream result;
		try
		{
			result = DataInputStream.getResourceAsStream(path);
		}
		catch (Exception)
		{
			result = null;
		}
		return result;
	}
}
