using System;

// Token: 0x0200000B RID: 11
public class BallInfo
{
	// Token: 0x0600005E RID: 94 RVA: 0x0000A37C File Offset: 0x0000857C
	public void SetChar()
	{
		this.cFocus = new global::Char();
		this.cFocus.charID = Res.random(-999, -800);
		this.cFocus.head = -1;
		this.cFocus.body = -1;
		this.cFocus.leg = -1;
		this.cFocus.bag = -1;
		this.cFocus.cName = string.Empty;
		this.cFocus.cHP = (this.cFocus.cHPFull = 20);
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00003AF3 File Offset: 0x00001CF3
	public void UpdChar()
	{
		this.cFocus.cx = this.x;
		this.cFocus.cy = this.y;
	}

	// Token: 0x04000091 RID: 145
	public int x;

	// Token: 0x04000092 RID: 146
	public int y;

	// Token: 0x04000093 RID: 147
	public int xTo = -999;

	// Token: 0x04000094 RID: 148
	public int yTo = -999;

	// Token: 0x04000095 RID: 149
	public int count;

	// Token: 0x04000096 RID: 150
	public int vy;

	// Token: 0x04000097 RID: 151
	public int vx;

	// Token: 0x04000098 RID: 152
	public int dir;

	// Token: 0x04000099 RID: 153
	public int idImg;

	// Token: 0x0400009A RID: 154
	public bool isPaint = true;

	// Token: 0x0400009B RID: 155
	public bool isDone;

	// Token: 0x0400009C RID: 156
	public bool isSetImg;

	// Token: 0x0400009D RID: 157
	public global::Char cFocus;
}
