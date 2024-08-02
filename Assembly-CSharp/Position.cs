using System;

// Token: 0x0200008C RID: 140
public class Position
{
	// Token: 0x06000770 RID: 1904 RVA: 0x00005AB9 File Offset: 0x00003CB9
	public Position()
	{
		this.x = 0;
		this.y = 0;
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x00005AD1 File Offset: 0x00003CD1
	public Position(int x, int y, int anchor)
	{
		this.x = x;
		this.y = y;
		this.anchor = anchor;
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x00005AF0 File Offset: 0x00003CF0
	public Position(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x00005B08 File Offset: 0x00003D08
	public void setPosTo(int xT, int yT)
	{
		this.xTo = (short)xT;
		this.yTo = (short)yT;
		this.distant = (short)Res.distance(this.x, this.y, (int)this.xTo, (int)this.yTo);
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x000855CC File Offset: 0x000837CC
	public int translate()
	{
		bool flag = this.x == (int)this.xTo && this.y == (int)this.yTo;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			bool flag2 = global::Math.abs(((int)this.xTo - this.x) / 2) <= 1 && global::Math.abs(((int)this.yTo - this.y) / 2) <= 1;
			if (flag2)
			{
				this.x = (int)this.xTo;
				this.y = (int)this.yTo;
				result = 0;
			}
			else
			{
				bool flag3 = this.x != (int)this.xTo;
				if (flag3)
				{
					this.x += ((int)this.xTo - this.x) / 2;
				}
				bool flag4 = this.y != (int)this.yTo;
				if (flag4)
				{
					this.y += ((int)this.yTo - this.y) / 2;
				}
				bool flag5 = Res.distance(this.x, this.y, (int)this.xTo, (int)this.yTo) <= (int)(this.distant / 5);
				if (flag5)
				{
					result = 2;
				}
				else
				{
					result = 1;
				}
			}
		}
		return result;
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x00005B3F File Offset: 0x00003D3F
	public void update()
	{
		this.layer.update();
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x00005B4E File Offset: 0x00003D4E
	public void paint(mGraphics g)
	{
		this.layer.paint(g, this.x, this.y);
	}

	// Token: 0x04000F79 RID: 3961
	public int x;

	// Token: 0x04000F7A RID: 3962
	public int y;

	// Token: 0x04000F7B RID: 3963
	public int anchor;

	// Token: 0x04000F7C RID: 3964
	public int g;

	// Token: 0x04000F7D RID: 3965
	public int v;

	// Token: 0x04000F7E RID: 3966
	public int w;

	// Token: 0x04000F7F RID: 3967
	public int h;

	// Token: 0x04000F80 RID: 3968
	public int color;

	// Token: 0x04000F81 RID: 3969
	public int limitY;

	// Token: 0x04000F82 RID: 3970
	public Layer layer;

	// Token: 0x04000F83 RID: 3971
	public short yTo;

	// Token: 0x04000F84 RID: 3972
	public short xTo;

	// Token: 0x04000F85 RID: 3973
	public short distant;
}
