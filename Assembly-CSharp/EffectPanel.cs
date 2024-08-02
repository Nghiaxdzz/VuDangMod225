using System;

// Token: 0x0200002B RID: 43
public class EffectPanel : Effect2
{
	// Token: 0x0600021F RID: 543 RVA: 0x00037A40 File Offset: 0x00035C40
	public static void addServerEffect(int id, int cx, int cy, int loopCount)
	{
		EffectPanel effectPanel = new EffectPanel();
		effectPanel.eff = GameScr.efs[id - 1];
		effectPanel.x = cx;
		effectPanel.y = cy;
		effectPanel.loopCount = (short)loopCount;
		Effect2.vEffect3.addElement(effectPanel);
	}

	// Token: 0x06000220 RID: 544 RVA: 0x00037A88 File Offset: 0x00035C88
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
			SmallImage.drawSmallImage(g, this.eff.arrEfInfo[this.i0].idImg, num, num2, this.trans, mGraphics.VCENTER | mGraphics.HCENTER);
		}
	}

	// Token: 0x06000221 RID: 545 RVA: 0x00037BB0 File Offset: 0x00035DB0
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
				Effect2.vEffect3.removeElement(this);
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
					Effect2.vEffect3.removeElement(this);
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
			Effect2.vEffect3.removeElement(this);
		}
	}

	// Token: 0x040004F5 RID: 1269
	public EffectCharPaint eff;

	// Token: 0x040004F6 RID: 1270
	private int i0;

	// Token: 0x040004F7 RID: 1271
	private int dx0;

	// Token: 0x040004F8 RID: 1272
	private int dy0;

	// Token: 0x040004F9 RID: 1273
	private int x;

	// Token: 0x040004FA RID: 1274
	private int y;

	// Token: 0x040004FB RID: 1275
	private global::Char c;

	// Token: 0x040004FC RID: 1276
	private Mob m;

	// Token: 0x040004FD RID: 1277
	private short loopCount;

	// Token: 0x040004FE RID: 1278
	private long endTime;

	// Token: 0x040004FF RID: 1279
	private int trans;
}
