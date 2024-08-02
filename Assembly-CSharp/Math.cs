using System;

// Token: 0x02000062 RID: 98
public class Math
{
	// Token: 0x06000494 RID: 1172 RVA: 0x00011F50 File Offset: 0x00010150
	public static int abs(int i)
	{
		return (i <= 0) ? (-i) : i;
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x0005891C File Offset: 0x00056B1C
	public static int min(int x, int y)
	{
		return (x >= y) ? y : x;
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x00058938 File Offset: 0x00056B38
	public static int max(int x, int y)
	{
		return (x <= y) ? y : x;
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x00058954 File Offset: 0x00056B54
	public static int pow(int data, int x)
	{
		int num = 1;
		for (int i = 0; i < x; i++)
		{
			num *= data;
		}
		return num;
	}

	// Token: 0x04000A0A RID: 2570
	public const double PI = 3.141592653589793;
}
