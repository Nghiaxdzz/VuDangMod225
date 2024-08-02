using System;
using UnityEngine;

// Token: 0x0200007D RID: 125
internal class Net
{
	// Token: 0x060005F9 RID: 1529 RVA: 0x00065870 File Offset: 0x00063A70
	public static void update()
	{
		bool flag = Net.www != null && Net.www.isDone;
		if (flag)
		{
			string str = string.Empty;
			bool flag2 = Net.www.error == null || Net.www.error.Equals(string.Empty);
			if (flag2)
			{
				str = Net.www.text;
			}
			Net.www = null;
			bool flag3 = Net.h != null;
			if (flag3)
			{
				Net.h.perform(str);
			}
		}
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x000658F4 File Offset: 0x00063AF4
	public static void connectHTTP(string link, Command h)
	{
		bool flag = Net.www != null;
		if (flag)
		{
			Cout.LogError("GET HTTP BUSY");
		}
		Net.www = new WWW(link);
		Net.h = h;
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x0006592C File Offset: 0x00063B2C
	public static void connectHTTP2(string link, Command h)
	{
		Net.h = h;
		bool flag = link != null;
		if (flag)
		{
			h.perform(link);
		}
	}

	// Token: 0x04000D63 RID: 3427
	public static WWW www;

	// Token: 0x04000D64 RID: 3428
	public static Command h;
}
