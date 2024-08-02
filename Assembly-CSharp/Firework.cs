using System;

// Token: 0x0200002F RID: 47
public class Firework
{
	// Token: 0x06000235 RID: 565 RVA: 0x00038454 File Offset: 0x00036654
	public Firework(int x0, int y0, int v, int angle, int cl)
	{
		this.y0 = y0;
		this.x0 = x0;
		this.a = 1f;
		this.v = v;
		this.angle = angle;
		this.w = GameCanvas.w;
		this.h = GameCanvas.h;
		this.last = this.time();
		for (int i = 0; i < 2; i++)
		{
			this.arr_x[i] = x0;
			this.arr_y[i] = y0;
		}
		this.cl = cl;
	}

	// Token: 0x06000236 RID: 566 RVA: 0x00038518 File Offset: 0x00036718
	public void preDraw()
	{
		bool flag = this.time() - this.last >= this.delay;
		if (flag)
		{
			this.t++;
			this.last = this.time();
			this.arr_x[1] = this.arr_x[0];
			this.arr_y[1] = this.arr_y[0];
			this.arr_x[0] = this.x;
			this.arr_y[0] = this.y;
			this.x = Res.cos((int)((double)this.angle * 3.141592653589793 / 180.0)) * this.v * this.t + this.x0;
			this.y = (int)((float)(this.v * Res.sin((int)((double)this.angle * 3.141592653589793 / 180.0)) * this.t) - this.a * (float)this.t * (float)this.t / 2f) + this.y0;
		}
	}

	// Token: 0x06000237 RID: 567 RVA: 0x00038634 File Offset: 0x00036834
	public void paint(mGraphics g)
	{
		this.Drawline(g, this.w - this.x, this.h - this.y, this.cl);
		for (int i = 0; i < 2; i++)
		{
			this.Drawline(g, this.w - this.arr_x[i], this.h - this.arr_y[i], this.cl);
		}
		bool flag = this.act;
		if (flag)
		{
			this.preDraw();
		}
	}

	// Token: 0x06000238 RID: 568 RVA: 0x000386BC File Offset: 0x000368BC
	public long time()
	{
		return mSystem.currentTimeMillis();
	}

	// Token: 0x06000239 RID: 569 RVA: 0x000041A4 File Offset: 0x000023A4
	public void Drawline(mGraphics g, int x, int y, int color)
	{
		g.setColor(color);
		g.fillRect(x, y, 1, 2);
	}

	// Token: 0x0400052D RID: 1325
	public int w;

	// Token: 0x0400052E RID: 1326
	public int h;

	// Token: 0x0400052F RID: 1327
	public int v;

	// Token: 0x04000530 RID: 1328
	public int x0;

	// Token: 0x04000531 RID: 1329
	public int x;

	// Token: 0x04000532 RID: 1330
	public int y;

	// Token: 0x04000533 RID: 1331
	public int y0;

	// Token: 0x04000534 RID: 1332
	public int angle;

	// Token: 0x04000535 RID: 1333
	public int t;

	// Token: 0x04000536 RID: 1334
	public int cl = 16711680;

	// Token: 0x04000537 RID: 1335
	private float a;

	// Token: 0x04000538 RID: 1336
	private long last;

	// Token: 0x04000539 RID: 1337
	private long delay = 150L;

	// Token: 0x0400053A RID: 1338
	private bool act = true;

	// Token: 0x0400053B RID: 1339
	private int[] arr_x = new int[2];

	// Token: 0x0400053C RID: 1340
	private int[] arr_y = new int[2];
}
