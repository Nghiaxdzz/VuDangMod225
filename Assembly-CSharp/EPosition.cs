using System;

// Token: 0x0200002E RID: 46
public class EPosition
{
	// Token: 0x06000232 RID: 562 RVA: 0x00004138 File Offset: 0x00002338
	public EPosition(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000233 RID: 563 RVA: 0x0000415E File Offset: 0x0000235E
	public EPosition(int x, int y, int fol)
	{
		this.x = x;
		this.y = y;
		this.follow = (sbyte)fol;
	}

	// Token: 0x06000234 RID: 564 RVA: 0x0000418C File Offset: 0x0000238C
	public EPosition()
	{
	}

	// Token: 0x04000526 RID: 1318
	public int x;

	// Token: 0x04000527 RID: 1319
	public int y;

	// Token: 0x04000528 RID: 1320
	public int anchor;

	// Token: 0x04000529 RID: 1321
	public sbyte follow;

	// Token: 0x0400052A RID: 1322
	public sbyte count;

	// Token: 0x0400052B RID: 1323
	public sbyte dir = 1;

	// Token: 0x0400052C RID: 1324
	public short index = -1;
}
