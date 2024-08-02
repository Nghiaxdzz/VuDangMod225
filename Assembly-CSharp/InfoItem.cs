using System;

// Token: 0x02000045 RID: 69
public class InfoItem
{
	// Token: 0x060003AD RID: 941 RVA: 0x00004908 File Offset: 0x00002B08
	public InfoItem(string s)
	{
		this.f = mFont.tahoma_7_green2;
		this.s = s;
		this.speed = 20;
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00004934 File Offset: 0x00002B34
	public InfoItem(string s, mFont f, int speed)
	{
		this.f = f;
		this.s = s;
		this.speed = speed;
	}

	// Token: 0x0400083D RID: 2109
	public string s;

	// Token: 0x0400083E RID: 2110
	private mFont f;

	// Token: 0x0400083F RID: 2111
	public int speed = 70;

	// Token: 0x04000840 RID: 2112
	public global::Char charInfo;

	// Token: 0x04000841 RID: 2113
	public bool isChatServer;

	// Token: 0x04000842 RID: 2114
	public bool isOnline;

	// Token: 0x04000843 RID: 2115
	public int timeCount;

	// Token: 0x04000844 RID: 2116
	public int maxTime;

	// Token: 0x04000845 RID: 2117
	public long last;

	// Token: 0x04000846 RID: 2118
	public long curr;
}
