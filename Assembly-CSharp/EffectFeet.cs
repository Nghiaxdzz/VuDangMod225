using System;

// Token: 0x02000027 RID: 39
public class EffectFeet : Effect2
{
	// Token: 0x0600020B RID: 523 RVA: 0x000376E0 File Offset: 0x000358E0
	public static void addFeet(int cx, int cy, int ctrans, int timeLengthInSecond, bool isCF)
	{
		EffectFeet effectFeet = new EffectFeet();
		effectFeet.x = cx;
		effectFeet.y = cy;
		effectFeet.trans = ctrans;
		effectFeet.isF = isCF;
		effectFeet.endTime = mSystem.currentTimeMillis() + (long)(timeLengthInSecond * 1000);
		Effect2.vEffectFeet.addElement(effectFeet);
	}

	// Token: 0x0600020C RID: 524 RVA: 0x00037734 File Offset: 0x00035934
	public override void update()
	{
		bool flag = mSystem.currentTimeMillis() - this.endTime > 0L;
		if (flag)
		{
			Effect2.vEffectFeet.removeElement(this);
		}
	}

	// Token: 0x0600020D RID: 525 RVA: 0x00037764 File Offset: 0x00035964
	public override void paint(mGraphics g)
	{
		int size = (int)TileMap.size;
		bool flag = TileMap.tileTypeAt(this.x + size / 2, this.y + 1, 4);
		if (flag)
		{
			g.setClip(this.x / size * size, (this.y - 30) / size * size, size, 100);
		}
		else
		{
			bool flag2 = TileMap.tileTypeAt((this.x - size / 2) / size, (this.y + 1) / size) == 0;
			if (flag2)
			{
				g.setClip(this.x / size * size, (this.y - 30) / size * size, 100, 100);
			}
			else
			{
				bool flag3 = TileMap.tileTypeAt((this.x + size / 2) / size, (this.y + 1) / size) == 0;
				if (flag3)
				{
					g.setClip(this.x / size * size, (this.y - 30) / size * size, size, 100);
				}
				else
				{
					bool flag4 = TileMap.tileTypeAt(this.x - size / 2, this.y + 1, 8);
					if (flag4)
					{
						g.setClip(this.x / 24 * size, (this.y - 30) / size * size, size, 100);
					}
				}
			}
		}
		g.drawRegion((!this.isF) ? EffectFeet.imgFeet3 : EffectFeet.imgFeet1, 0, 0, EffectFeet.imgFeet1.getWidth(), EffectFeet.imgFeet1.getHeight(), this.trans, this.x, this.y, mGraphics.BOTTOM | mGraphics.HCENTER);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x040004E3 RID: 1251
	private int x;

	// Token: 0x040004E4 RID: 1252
	private int y;

	// Token: 0x040004E5 RID: 1253
	private int trans;

	// Token: 0x040004E6 RID: 1254
	private long endTime;

	// Token: 0x040004E7 RID: 1255
	private bool isF;

	// Token: 0x040004E8 RID: 1256
	public static Image imgFeet1 = GameCanvas.loadImage("/mainImage/myTexture2dmove-1.png");

	// Token: 0x040004E9 RID: 1257
	public static Image imgFeet3 = GameCanvas.loadImage("/mainImage/myTexture2dmove-3.png");
}
