using System;

// Token: 0x02000047 RID: 71
public class InfoPhuBan
{
	// Token: 0x060003B9 RID: 953 RVA: 0x00052F34 File Offset: 0x00051134
	public InfoPhuBan(int type_PB, short idmapPaint, string nameTeam1, string nameTeam2, int maxPoint, short timeSecond)
	{
		this.type_PB = type_PB;
		this.idmapPaint = idmapPaint;
		this.nameTeam1 = nameTeam1;
		this.nameTeam2 = nameTeam2;
		this.timeSecond = timeSecond;
		this.timeStart = GameCanvas.timeNow;
		this.maxPoint = maxPoint;
		bool flag = this.maxPoint <= 0;
		if (flag)
		{
			this.maxPoint = 1;
		}
		this.pointTeam1 = 0;
		this.pointTeam2 = 0;
		this.owner = 0;
		this.color_1 = 4;
		this.color_2 = 6;
	}

	// Token: 0x060003BA RID: 954 RVA: 0x0000499A File Offset: 0x00002B9A
	public void updateTime(int type_PB, short timeSecond)
	{
		this.type_PB = type_PB;
		this.timeSecond = timeSecond;
		this.timeStart = GameCanvas.timeNow;
	}

	// Token: 0x060003BB RID: 955 RVA: 0x000049B6 File Offset: 0x00002BB6
	public void updatePoint(int type_PB, int pointTeam1, int pointTeam2)
	{
		this.type_PB = type_PB;
		this.pointTeam1 = pointTeam1;
		this.pointTeam2 = pointTeam2;
	}

	// Token: 0x060003BC RID: 956 RVA: 0x000049CE File Offset: 0x00002BCE
	public void updateLife(int type_PB, int lifeTeam1, int lifeTeam2)
	{
		this.type_PB = type_PB;
		this.lifeTeam1 = lifeTeam1;
		this.lifeTeam2 = lifeTeam2;
	}

	// Token: 0x0400085E RID: 2142
	public int type_PB;

	// Token: 0x0400085F RID: 2143
	public int maxPoint;

	// Token: 0x04000860 RID: 2144
	public int pointTeam1;

	// Token: 0x04000861 RID: 2145
	public int pointTeam2;

	// Token: 0x04000862 RID: 2146
	public int color_1;

	// Token: 0x04000863 RID: 2147
	public int color_2;

	// Token: 0x04000864 RID: 2148
	public int maxLife = 1;

	// Token: 0x04000865 RID: 2149
	public int lifeTeam1;

	// Token: 0x04000866 RID: 2150
	public int lifeTeam2;

	// Token: 0x04000867 RID: 2151
	public string nameTeam1;

	// Token: 0x04000868 RID: 2152
	public string nameTeam2;

	// Token: 0x04000869 RID: 2153
	public short idmapPaint;

	// Token: 0x0400086A RID: 2154
	public short timeSecond;

	// Token: 0x0400086B RID: 2155
	public short timepaintSecond;

	// Token: 0x0400086C RID: 2156
	public short maxtimeSecond = 1;

	// Token: 0x0400086D RID: 2157
	public byte owner;

	// Token: 0x0400086E RID: 2158
	public long timeStart;

	// Token: 0x0400086F RID: 2159
	public MyVector vecInfo = new MyVector("vecInfo chientruong");
}
