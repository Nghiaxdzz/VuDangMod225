using System;

// Token: 0x02000031 RID: 49
public class FireWorkMn
{
	// Token: 0x06000242 RID: 578 RVA: 0x00038A68 File Offset: 0x00036C68
	public FireWorkMn(int x, int y, int goc, int n)
	{
		this.x = x;
		this.y = y;
		this.goc = goc;
		this.n = n;
		for (int i = 0; i < n; i++)
		{
			this.fw.addElement(new Firework(x, y, global::Math.abs(this.rd.nextInt() % 8) + 3, i * goc, this.color[global::Math.abs(this.rd.nextInt() % this.color.Length)]));
		}
	}

	// Token: 0x06000243 RID: 579 RVA: 0x00038B38 File Offset: 0x00036D38
	public void paint(mGraphics g)
	{
		for (int i = 0; i < this.fw.size(); i++)
		{
			Firework firework = (Firework)this.fw.elementAt(i);
			bool flag = firework.y < -200;
			if (flag)
			{
				this.fw.removeElementAt(i);
			}
			firework.paint(g);
		}
	}

	// Token: 0x04000550 RID: 1360
	private int x;

	// Token: 0x04000551 RID: 1361
	private int y;

	// Token: 0x04000552 RID: 1362
	private int goc = 1;

	// Token: 0x04000553 RID: 1363
	private int n = 360;

	// Token: 0x04000554 RID: 1364
	private MyRandom rd = new MyRandom();

	// Token: 0x04000555 RID: 1365
	private MyVector fw = new MyVector();

	// Token: 0x04000556 RID: 1366
	private int[] color = new int[]
	{
		16711680,
		16776960,
		65280,
		16777215,
		255,
		65535,
		15790320,
		12632256
	};
}
