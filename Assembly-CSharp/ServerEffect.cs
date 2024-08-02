using System;

// Token: 0x02000093 RID: 147
public class ServerEffect : Effect2
{
	// Token: 0x060007CF RID: 1999 RVA: 0x00088C90 File Offset: 0x00086E90
	public static void addServerEffect(int id, int cx, int cy, int loopCount)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.x = cx;
		serverEffect.y = cy;
		serverEffect.loopCount = (short)loopCount;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x00088CD8 File Offset: 0x00086ED8
	public static void addServerEffect(int id, int cx, int cy, int loopCount, int trans)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.x = cx;
		serverEffect.y = cy;
		serverEffect.loopCount = (short)loopCount;
		serverEffect.trans = trans;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x00088D28 File Offset: 0x00086F28
	public static void addServerEffect(int id, Mob m, int loopCount)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.m = m;
		serverEffect.loopCount = (short)loopCount;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x00088D68 File Offset: 0x00086F68
	public static void addServerEffect(int id, global::Char c, int loopCount)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.c = c;
		serverEffect.loopCount = (short)loopCount;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x00088DA8 File Offset: 0x00086FA8
	public static void addServerEffect(int id, global::Char c, int loopCount, int trans)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.c = c;
		serverEffect.loopCount = (short)loopCount;
		serverEffect.trans = trans;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x00088DF0 File Offset: 0x00086FF0
	public static void addServerEffectWithTime(int id, int cx, int cy, int timeLengthInSecond)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.x = cx;
		serverEffect.y = cy;
		serverEffect.endTime = mSystem.currentTimeMillis() + (long)(timeLengthInSecond * 1000);
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x00088E44 File Offset: 0x00087044
	public static void addServerEffectWithTime(int id, global::Char c, int timeLengthInSecond)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.c = c;
		serverEffect.endTime = mSystem.currentTimeMillis() + (long)(timeLengthInSecond * 1000);
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x00088E90 File Offset: 0x00087090
	public override void paint(mGraphics g)
	{
		bool flag = mGraphics.zoomLevel == 1;
		if (flag)
		{
			GameScr.countEff++;
		}
		bool flag2 = GameScr.countEff < 8;
		if (flag2)
		{
			bool flag3 = this.c != null;
			if (flag3)
			{
				this.x = this.c.cx;
				this.y = this.c.cy + GameCanvas.transY;
			}
			bool flag4 = this.m != null;
			if (flag4)
			{
				this.x = this.m.x;
				this.y = this.m.y + GameCanvas.transY;
			}
			int num = this.x + this.dx0 + this.eff.arrEfInfo[this.i0].dx;
			int num2 = this.y + this.dy0 + this.eff.arrEfInfo[this.i0].dy;
			bool flag5 = GameCanvas.isPaint(num, num2);
			if (flag5)
			{
				SmallImage.drawSmallImage(g, this.eff.arrEfInfo[this.i0].idImg, num, num2, this.trans, mGraphics.VCENTER | mGraphics.HCENTER);
			}
		}
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x00088FC8 File Offset: 0x000871C8
	public override void update()
	{
		bool flag = this.endTime != 0L;
		if (flag)
		{
			this.i0++;
			bool flag2 = this.i0 >= this.eff.arrEfInfo.Length;
			if (flag2)
			{
				this.i0 = 0;
			}
			bool flag3 = mSystem.currentTimeMillis() - this.endTime > 0L;
			if (flag3)
			{
				Effect2.vEffect2.removeElement(this);
			}
		}
		else
		{
			this.i0++;
			bool flag4 = this.i0 >= this.eff.arrEfInfo.Length;
			if (flag4)
			{
				this.loopCount -= 1;
				bool flag5 = this.loopCount <= 0;
				if (flag5)
				{
					Effect2.vEffect2.removeElement(this);
				}
				else
				{
					this.i0 = 0;
				}
			}
		}
		bool flag6 = GameCanvas.gameTick % 11 == 0 && this.c != null && this.c != global::Char.myCharz() && !GameScr.vCharInMap.contains(this.c);
		if (flag6)
		{
			Effect2.vEffect2.removeElement(this);
		}
	}

	// Token: 0x04000FEE RID: 4078
	public EffectCharPaint eff;

	// Token: 0x04000FEF RID: 4079
	private int i0;

	// Token: 0x04000FF0 RID: 4080
	private int dx0;

	// Token: 0x04000FF1 RID: 4081
	private int dy0;

	// Token: 0x04000FF2 RID: 4082
	private int x;

	// Token: 0x04000FF3 RID: 4083
	private int y;

	// Token: 0x04000FF4 RID: 4084
	private global::Char c;

	// Token: 0x04000FF5 RID: 4085
	private Mob m;

	// Token: 0x04000FF6 RID: 4086
	private short loopCount;

	// Token: 0x04000FF7 RID: 4087
	private long endTime;

	// Token: 0x04000FF8 RID: 4088
	private int trans;
}
