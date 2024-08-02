using System;

// Token: 0x0200006A RID: 106
public class MobCapcha
{
	// Token: 0x06000539 RID: 1337 RVA: 0x00004F97 File Offset: 0x00003197
	public static void init()
	{
		MobCapcha.imgMob = GameCanvas.loadImage("/mainImage/myTexture2dmobCapcha.png");
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x00060718 File Offset: 0x0005E918
	public static void paint(mGraphics g, int x, int y)
	{
		bool flag = !MobCapcha.isAttack;
		if (flag)
		{
			bool flag2 = GameCanvas.gameTick % 3 == 0;
			if (flag2)
			{
				bool flag3 = global::Char.myCharz().cdir == 1;
				if (flag3)
				{
					MobCapcha.cmtoX = x - 20 - GameScr.cmx;
				}
				bool flag4 = global::Char.myCharz().cdir == -1;
				if (flag4)
				{
					MobCapcha.cmtoX = x + 20 - GameScr.cmx;
				}
			}
			MobCapcha.cmtoY = global::Char.myCharz().cy - 40 - GameScr.cmy;
		}
		else
		{
			MobCapcha.delay++;
			bool flag5 = MobCapcha.delay == 5;
			if (flag5)
			{
				MobCapcha.isAttack = false;
				MobCapcha.delay = 0;
			}
			MobCapcha.cmtoX = x - GameScr.cmx;
			MobCapcha.cmtoY = y - GameScr.cmy;
		}
		bool flag6 = MobCapcha.cmx > x - GameScr.cmx;
		if (flag6)
		{
			MobCapcha.dir = -1;
		}
		else
		{
			MobCapcha.dir = 1;
		}
		g.drawImage(GameScr.imgCapcha, MobCapcha.cmx, MobCapcha.cmy - 40, 3);
		PopUp.paintPopUp(g, MobCapcha.cmx - 25, MobCapcha.cmy - 70, 50, 20, 16777215, false);
		mFont.tahoma_7b_dark.drawString(g, GameScr.gI().keyInput, MobCapcha.cmx, MobCapcha.cmy - 65, 2);
		bool flag7 = MobCapcha.isCreateMob;
		if (flag7)
		{
			MobCapcha.isCreateMob = false;
			EffecMn.addEff(new Effect(18, MobCapcha.cmx + GameScr.cmx, MobCapcha.cmy + GameScr.cmy, 2, 10, -1));
		}
		bool flag8 = MobCapcha.explode;
		if (flag8)
		{
			MobCapcha.explode = false;
			EffecMn.addEff(new Effect(18, MobCapcha.cmx + GameScr.cmx, MobCapcha.cmy + GameScr.cmy, 2, 10, -1));
			GameScr.gI().mobCapcha = null;
			MobCapcha.cmtoX = -GameScr.cmx;
			MobCapcha.cmtoY = -GameScr.cmy;
		}
		g.drawRegion(MobCapcha.imgMob, 0, MobCapcha.f * 40, 40, 40, (MobCapcha.dir != 1) ? 2 : 0, MobCapcha.cmx, MobCapcha.cmy + 3 + ((GameCanvas.gameTick % 10 > 5) ? 1 : 0), 3);
		MobCapcha.moveCamera();
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00060948 File Offset: 0x0005EB48
	public static void moveCamera()
	{
		bool flag = MobCapcha.cmy != MobCapcha.cmtoY;
		if (flag)
		{
			MobCapcha.cmvy = MobCapcha.cmtoY - MobCapcha.cmy << 2;
			MobCapcha.cmdy += MobCapcha.cmvy;
			MobCapcha.cmy += MobCapcha.cmdy >> 4;
			MobCapcha.cmdy &= 15;
		}
		bool flag2 = MobCapcha.cmx != MobCapcha.cmtoX;
		if (flag2)
		{
			MobCapcha.cmvx = MobCapcha.cmtoX - MobCapcha.cmx << 2;
			MobCapcha.cmdx += MobCapcha.cmvx;
			MobCapcha.cmx += MobCapcha.cmdx >> 4;
			MobCapcha.cmdx &= 15;
		}
		MobCapcha.tF++;
		bool flag3 = MobCapcha.tF == 5;
		if (flag3)
		{
			MobCapcha.tF = 0;
			MobCapcha.f++;
			bool flag4 = MobCapcha.f > 2;
			if (flag4)
			{
				MobCapcha.f = 0;
			}
		}
	}

	// Token: 0x04000B28 RID: 2856
	public static Image imgMob;

	// Token: 0x04000B29 RID: 2857
	public static int cmtoY;

	// Token: 0x04000B2A RID: 2858
	public static int cmy;

	// Token: 0x04000B2B RID: 2859
	public static int cmdy;

	// Token: 0x04000B2C RID: 2860
	public static int cmvy;

	// Token: 0x04000B2D RID: 2861
	public static int cmyLim;

	// Token: 0x04000B2E RID: 2862
	public static int cmtoX;

	// Token: 0x04000B2F RID: 2863
	public static int cmx;

	// Token: 0x04000B30 RID: 2864
	public static int cmdx;

	// Token: 0x04000B31 RID: 2865
	public static int cmvx;

	// Token: 0x04000B32 RID: 2866
	public static int cmxLim;

	// Token: 0x04000B33 RID: 2867
	public static bool explode;

	// Token: 0x04000B34 RID: 2868
	public static int delay;

	// Token: 0x04000B35 RID: 2869
	public static bool isCreateMob;

	// Token: 0x04000B36 RID: 2870
	public static int tF;

	// Token: 0x04000B37 RID: 2871
	public static int f;

	// Token: 0x04000B38 RID: 2872
	public static int dir;

	// Token: 0x04000B39 RID: 2873
	public static bool isAttack;
}
