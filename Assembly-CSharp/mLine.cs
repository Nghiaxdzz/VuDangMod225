using System;

// Token: 0x02000068 RID: 104
public class mLine
{
	// Token: 0x06000508 RID: 1288 RVA: 0x00004F2E File Offset: 0x0000312E
	public mLine(int x1, int y1, int x2, int y2, int cl)
	{
		this.x1 = x1;
		this.y1 = y1;
		this.x2 = x2;
		this.y2 = y2;
		this.setColor(cl);
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x0005D960 File Offset: 0x0005BB60
	public void setColor(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		this.b = (float)num / 256f;
		this.g = (float)num2 / 256f;
		this.r = (float)num3 / 256f;
		this.a = 255f;
	}

	// Token: 0x04000ABA RID: 2746
	public int x1;

	// Token: 0x04000ABB RID: 2747
	public int x2;

	// Token: 0x04000ABC RID: 2748
	public int y1;

	// Token: 0x04000ABD RID: 2749
	public int y2;

	// Token: 0x04000ABE RID: 2750
	public float r;

	// Token: 0x04000ABF RID: 2751
	public float b;

	// Token: 0x04000AC0 RID: 2752
	public float g;

	// Token: 0x04000AC1 RID: 2753
	public float a;
}
