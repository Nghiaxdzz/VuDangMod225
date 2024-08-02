using System;

// Token: 0x0200006F RID: 111
public class MovePoint
{
	// Token: 0x0600055F RID: 1375 RVA: 0x00005104 File Offset: 0x00003304
	public MovePoint(int xEnd, int yEnd, int act, int dir)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.dir = dir;
		this.status = act;
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0000512B File Offset: 0x0000332B
	public MovePoint(int xEnd, int yEnd)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
	}

	// Token: 0x04000B6F RID: 2927
	public int xEnd;

	// Token: 0x04000B70 RID: 2928
	public int yEnd;

	// Token: 0x04000B71 RID: 2929
	public int dir;

	// Token: 0x04000B72 RID: 2930
	public int cvx;

	// Token: 0x04000B73 RID: 2931
	public int cvy;

	// Token: 0x04000B74 RID: 2932
	public int status;
}
