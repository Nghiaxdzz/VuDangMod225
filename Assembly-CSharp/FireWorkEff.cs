using System;

// Token: 0x02000030 RID: 48
public class FireWorkEff
{
	// Token: 0x0600023A RID: 570 RVA: 0x000386D4 File Offset: 0x000368D4
	public static void preDraw()
	{
		bool flag = FireWorkEff.st;
		if (flag)
		{
			FireWorkEff.animate();
		}
		bool flag2 = FireWorkEff.t > 32 && FireWorkEff.st;
		if (flag2)
		{
			FireWorkEff.st = false;
			FireWorkEff.mg.removeAllElements();
			FireWorkEff.mg.addElement(new FireWorkMn(Res.random(50, GameCanvas.w - 50), Res.random(GameCanvas.h - 100, GameCanvas.h), 5, 72));
		}
	}

	// Token: 0x0600023B RID: 571 RVA: 0x00038750 File Offset: 0x00036950
	public static void paint(mGraphics g)
	{
		FireWorkEff.preDraw();
		g.setColor(0);
		g.fillRect(0, 0, FireWorkEff.w, FireWorkEff.h);
		g.setColor(16711680);
		for (int i = 0; i < FireWorkEff.mg.size(); i++)
		{
			((FireWorkMn)FireWorkEff.mg.elementAt(i)).paint(g);
		}
		bool flag = !FireWorkEff.st;
		if (flag)
		{
			FireWorkEff.keyPressed(-(global::Math.abs(FireWorkEff.r.nextInt() % 3) + 5));
		}
	}

	// Token: 0x0600023C RID: 572 RVA: 0x000387E8 File Offset: 0x000369E8
	public static void keyPressed(int k)
	{
		bool flag = k == -5 && !FireWorkEff.st;
		if (flag)
		{
			FireWorkEff.x0 = FireWorkEff.w / 2;
			FireWorkEff.ag = 80;
			FireWorkEff.st = true;
			FireWorkEff.add();
		}
		else
		{
			bool flag2 = k == -7 && !FireWorkEff.st;
			if (flag2)
			{
				FireWorkEff.ag = 60;
				FireWorkEff.x0 = 0;
				FireWorkEff.st = true;
				FireWorkEff.add();
			}
			else
			{
				bool flag3 = k == -6 && !FireWorkEff.st;
				if (flag3)
				{
					FireWorkEff.ag = 120;
					FireWorkEff.x0 = FireWorkEff.w;
					FireWorkEff.st = true;
					FireWorkEff.add();
				}
			}
		}
	}

	// Token: 0x0600023D RID: 573 RVA: 0x00038894 File Offset: 0x00036A94
	public static void add()
	{
		FireWorkEff.y0 = 0;
		FireWorkEff.v = 16;
		FireWorkEff.t = 0;
		FireWorkEff.a = 0f;
		for (int i = 0; i < 3; i++)
		{
			FireWorkEff.mang_y[i] = 0;
			FireWorkEff.mang_x[i] = FireWorkEff.x0;
		}
		FireWorkEff.st = true;
	}

	// Token: 0x0600023E RID: 574 RVA: 0x000388EC File Offset: 0x00036AEC
	public static void animate()
	{
		FireWorkEff.mang_y[2] = FireWorkEff.mang_y[1];
		FireWorkEff.mang_x[2] = FireWorkEff.mang_x[1];
		FireWorkEff.mang_y[1] = FireWorkEff.mang_y[0];
		FireWorkEff.mang_x[1] = FireWorkEff.mang_x[0];
		FireWorkEff.mang_y[0] = FireWorkEff.y;
		FireWorkEff.mang_x[0] = FireWorkEff.x;
		FireWorkEff.x = Res.cos((int)((double)FireWorkEff.ag * 3.141592653589793 / 180.0)) * FireWorkEff.v * FireWorkEff.t + FireWorkEff.x0;
		FireWorkEff.y = (int)((float)(FireWorkEff.v * Res.sin((int)((double)FireWorkEff.ag * 3.141592653589793 / 180.0)) * FireWorkEff.t) - FireWorkEff.a * (float)FireWorkEff.t * (float)FireWorkEff.t / 2f) + FireWorkEff.y0;
		bool flag = FireWorkEff.time() - FireWorkEff.last >= FireWorkEff.delay;
		if (flag)
		{
			FireWorkEff.t++;
			FireWorkEff.last = FireWorkEff.time();
		}
	}

	// Token: 0x0600023F RID: 575 RVA: 0x000386BC File Offset: 0x000368BC
	public static long time()
	{
		return mSystem.currentTimeMillis();
	}

	// Token: 0x0400053D RID: 1341
	private static int w;

	// Token: 0x0400053E RID: 1342
	private static int h;

	// Token: 0x0400053F RID: 1343
	private static MyRandom r = new MyRandom();

	// Token: 0x04000540 RID: 1344
	private static MyVector mg = new MyVector();

	// Token: 0x04000541 RID: 1345
	private static int f = 17;

	// Token: 0x04000542 RID: 1346
	private static int x;

	// Token: 0x04000543 RID: 1347
	private static int y;

	// Token: 0x04000544 RID: 1348
	private static int ag;

	// Token: 0x04000545 RID: 1349
	private static int x0;

	// Token: 0x04000546 RID: 1350
	private static int y0;

	// Token: 0x04000547 RID: 1351
	private static int t;

	// Token: 0x04000548 RID: 1352
	private static int v;

	// Token: 0x04000549 RID: 1353
	private static int ymax = 269;

	// Token: 0x0400054A RID: 1354
	private static float a;

	// Token: 0x0400054B RID: 1355
	private static int[] mang_x = new int[3];

	// Token: 0x0400054C RID: 1356
	private static int[] mang_y = new int[3];

	// Token: 0x0400054D RID: 1357
	private static bool st = false;

	// Token: 0x0400054E RID: 1358
	private static long last = 0L;

	// Token: 0x0400054F RID: 1359
	private static long delay = 150L;
}
