using System;

// Token: 0x02000063 RID: 99
public class Member
{
	// Token: 0x06000499 RID: 1177 RVA: 0x00058980 File Offset: 0x00056B80
	public static string getRole(int r)
	{
		string result;
		switch (r)
		{
		case 0:
			result = mResources.clan_leader;
			break;
		case 1:
			result = mResources.clan_coleader;
			break;
		case 2:
			result = mResources.member;
			break;
		default:
			result = string.Empty;
			break;
		}
		return result;
	}

	// Token: 0x04000A0B RID: 2571
	public int ID;

	// Token: 0x04000A0C RID: 2572
	public short head;

	// Token: 0x04000A0D RID: 2573
	public short headICON = -1;

	// Token: 0x04000A0E RID: 2574
	public short leg;

	// Token: 0x04000A0F RID: 2575
	public short body;

	// Token: 0x04000A10 RID: 2576
	public string name;

	// Token: 0x04000A11 RID: 2577
	public sbyte role;

	// Token: 0x04000A12 RID: 2578
	public string powerPoint;

	// Token: 0x04000A13 RID: 2579
	public int donate;

	// Token: 0x04000A14 RID: 2580
	public int receive_donate;

	// Token: 0x04000A15 RID: 2581
	public int curClanPoint;

	// Token: 0x04000A16 RID: 2582
	public int clanPoint;

	// Token: 0x04000A17 RID: 2583
	public int lastRequest;

	// Token: 0x04000A18 RID: 2584
	public string joinTime;
}
