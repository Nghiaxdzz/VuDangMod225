using System;

// Token: 0x0200006D RID: 109
public class MonsterDart : Effect2
{
	// Token: 0x06000548 RID: 1352 RVA: 0x000611EC File Offset: 0x0005F3EC
	public MonsterDart(int x, int y, bool isBoss, int dame, int dameMp, global::Char c, int dartType)
	{
		this.info = GameScr.darts[dartType];
		this.x = x;
		this.y = y;
		this.isBoss = isBoss;
		this.dame = dame;
		this.dameMp = dameMp;
		this.c = c;
		this.va = this.info.va;
		this.setAngle(Res.angle(c.cx - x, c.cy - y));
		bool flag = x >= GameScr.cmx && x <= GameScr.cmx + GameCanvas.w;
		if (flag)
		{
			SoundMn.gI().mobKame(dartType);
		}
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x000612A8 File Offset: 0x0005F4A8
	public MonsterDart(int x, int y, bool isBoss, int dame, int dameMp, int xTo, int yTo, int dartType)
	{
		this.info = GameScr.darts[dartType];
		this.x = x;
		this.y = y;
		this.isBoss = isBoss;
		this.dame = dame;
		this.dameMp = dameMp;
		this.xTo = xTo;
		this.yTo = yTo;
		this.va = this.info.va;
		this.setAngle(Res.angle(xTo - x, yTo - y));
		bool flag = x >= GameScr.cmx && x <= GameScr.cmx + GameCanvas.w;
		if (flag)
		{
			SoundMn.gI().mobKame(dartType);
		}
		this.c = null;
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00004FC3 File Offset: 0x000031C3
	public void setAngle(int angle)
	{
		this.angle = angle;
		this.vx = this.va * Res.cos(angle) >> 10;
		this.vy = this.va * Res.sin(angle) >> 10;
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00004FF9 File Offset: 0x000031F9
	public static void addMonsterDart(int x, int y, bool isBoss, int dame, int dameMp, global::Char c, int dartType)
	{
		Effect2.vEffect2.addElement(new MonsterDart(x, y, isBoss, dame, dameMp, c, dartType));
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x00061368 File Offset: 0x0005F568
	public static void addMonsterDart(int x, int y, bool isBoss, int dame, int dameMp, int xTo, int yTo, int dartType)
	{
		Effect2.vEffect2.addElement(new MonsterDart(x, y, isBoss, dame, dameMp, xTo, yTo, dartType));
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x00061394 File Offset: 0x0005F594
	public override void update()
	{
		for (int i = 0; i < (int)this.info.nUpdate; i++)
		{
			bool flag = this.info.tail.Length != 0;
			if (flag)
			{
				this.darts.addElement(new SmallDart(this.x, this.y));
			}
			this.dx = ((this.c == null) ? this.xTo : this.c.cx) - this.x;
			this.dy = ((this.c == null) ? this.yTo : this.c.cy) - 10 - this.y;
			int num = 60;
			bool flag2 = TileMap.mapID == 0;
			if (flag2)
			{
				num = 600;
			}
			this.life++;
			bool flag3 = (this.c != null && (this.c.statusMe == 5 || this.c.statusMe == 14)) || this.c == null;
			if (flag3)
			{
				this.x += (((this.c == null) ? this.xTo : this.c.cx) - this.x) / 2;
				this.y += (((this.c == null) ? this.yTo : this.c.cy) - this.y) / 2;
			}
			bool flag4 = (Res.abs(this.dx) < 16 && Res.abs(this.dy) < 16) || this.life > num;
			if (flag4)
			{
				bool flag5 = this.c != null && this.c.charID >= 0 && this.dameMp != -1;
				if (flag5)
				{
					bool flag6 = this.dameMp != -100;
					if (flag6)
					{
						this.c.doInjure(this.dame, this.dameMp, false, true);
					}
					else
					{
						ServerEffect.addServerEffect(80, this.c, 1);
					}
				}
				Effect2.vEffect2.removeElement(this);
				bool flag7 = this.dameMp != -100;
				if (flag7)
				{
					ServerEffect.addServerEffect(81, this.c, 1);
					bool flag8 = this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w;
					if (flag8)
					{
						SoundMn.gI().explode_2();
					}
				}
			}
			int num2 = Res.angle(this.dx, this.dy);
			bool flag9 = global::Math.abs(num2 - this.angle) < 90 || this.dx * this.dx + this.dy * this.dy > 4096;
			if (flag9)
			{
				bool flag10 = global::Math.abs(num2 - this.angle) < 15;
				if (flag10)
				{
					this.angle = num2;
				}
				else
				{
					bool flag11 = (num2 - this.angle >= 0 && num2 - this.angle < 180) || num2 - this.angle < -180;
					if (flag11)
					{
						this.angle = Res.fixangle(this.angle + 15);
					}
					else
					{
						this.angle = Res.fixangle(this.angle - 15);
					}
				}
			}
			bool flag12 = !this.isSpeedUp && this.va < 8192;
			if (flag12)
			{
				this.va += 1024;
			}
			this.vx = this.va * Res.cos(this.angle) >> 10;
			this.vy = this.va * Res.sin(this.angle) >> 10;
			this.dx += this.vx;
			int num3 = this.dx >> 10;
			this.x += num3;
			this.dx &= 1023;
			this.dy += this.vy;
			int num4 = this.dy >> 10;
			this.y += num4;
			this.dy &= 1023;
		}
		for (int j = 0; j < this.darts.size(); j++)
		{
			SmallDart smallDart = (SmallDart)this.darts.elementAt(j);
			smallDart.index++;
			bool flag13 = smallDart.index >= this.info.tail.Length;
			if (flag13)
			{
				this.darts.removeElementAt(j);
			}
		}
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x0006184C File Offset: 0x0005FA4C
	public static int findDirIndexFromAngle(int angle)
	{
		for (int i = 0; i < MonsterDart.ARROWINDEX.Length - 1; i++)
		{
			bool flag = angle >= MonsterDart.ARROWINDEX[i] && angle <= MonsterDart.ARROWINDEX[i + 1];
			if (flag)
			{
				bool flag2 = i >= 16;
				int result;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					result = i;
				}
				return result;
			}
		}
		return 0;
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x000618B4 File Offset: 0x0005FAB4
	public override void paint(mGraphics g)
	{
		int num = MonsterDart.findDirIndexFromAngle(360 - this.angle);
		int num2 = (int)MonsterDart.FRAME[num];
		int transform = MonsterDart.TRANSFORM[num];
		for (int i = this.darts.size() / 2; i < this.darts.size(); i++)
		{
			SmallDart smallDart = (SmallDart)this.darts.elementAt(i);
			SmallImage.drawSmallImage(g, (int)this.info.tailBorder[smallDart.index], smallDart.x, smallDart.y, 0, 3);
		}
		int num3 = GameCanvas.gameTick % this.info.headBorder.Length;
		SmallImage.drawSmallImage(g, (int)this.info.headBorder[num3][num2], this.x, this.y, transform, 3);
		for (int j = 0; j < this.darts.size(); j++)
		{
			SmallDart smallDart2 = (SmallDart)this.darts.elementAt(j);
			SmallImage.drawSmallImage(g, (int)this.info.tail[smallDart2.index], smallDart2.x, smallDart2.y, 0, 3);
		}
		SmallImage.drawSmallImage(g, (int)this.info.head[num3][num2], this.x, this.y, transform, 3);
		for (int k = 0; k < this.darts.size(); k++)
		{
			SmallDart smallDart3 = (SmallDart)this.darts.elementAt(k);
			bool flag = Res.abs(MonsterDart.r.nextInt(100)) < (int)this.info.xdPercent;
			if (flag)
			{
				SmallImage.drawSmallImage(g, (int)((GameCanvas.gameTick % 2 != 0) ? this.info.xd2[smallDart3.index] : this.info.xd1[smallDart3.index]), smallDart3.x, smallDart3.y, 0, 3);
			}
		}
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x00005016 File Offset: 0x00003216
	public static void addMonsterDart(int x2, int y2, bool checkIsBoss, int dame2, int dameMp2, Mob mobToAttack, sbyte dartType)
	{
		MonsterDart.addMonsterDart(x2, y2, checkIsBoss, dame2, dameMp2, mobToAttack.x, mobToAttack.y, (int)dartType);
	}

	// Token: 0x04000B4F RID: 2895
	public int va;

	// Token: 0x04000B50 RID: 2896
	private DartInfo info;

	// Token: 0x04000B51 RID: 2897
	public static MyRandom r = new MyRandom();

	// Token: 0x04000B52 RID: 2898
	public int angle;

	// Token: 0x04000B53 RID: 2899
	public int vx;

	// Token: 0x04000B54 RID: 2900
	public int vy;

	// Token: 0x04000B55 RID: 2901
	public int x;

	// Token: 0x04000B56 RID: 2902
	public int y;

	// Token: 0x04000B57 RID: 2903
	public int z;

	// Token: 0x04000B58 RID: 2904
	public int xTo;

	// Token: 0x04000B59 RID: 2905
	public int yTo;

	// Token: 0x04000B5A RID: 2906
	private int life;

	// Token: 0x04000B5B RID: 2907
	public bool isSpeedUp;

	// Token: 0x04000B5C RID: 2908
	public int dame;

	// Token: 0x04000B5D RID: 2909
	public int dameMp;

	// Token: 0x04000B5E RID: 2910
	public global::Char c;

	// Token: 0x04000B5F RID: 2911
	public bool isBoss;

	// Token: 0x04000B60 RID: 2912
	public MyVector darts = new MyVector();

	// Token: 0x04000B61 RID: 2913
	private int dx;

	// Token: 0x04000B62 RID: 2914
	private int dy;

	// Token: 0x04000B63 RID: 2915
	public static int[] ARROWINDEX = new int[]
	{
		0,
		15,
		37,
		52,
		75,
		105,
		127,
		142,
		165,
		195,
		217,
		232,
		255,
		285,
		307,
		322,
		345,
		370
	};

	// Token: 0x04000B64 RID: 2916
	public static int[] TRANSFORM = new int[]
	{
		0,
		0,
		0,
		7,
		6,
		6,
		6,
		2,
		2,
		3,
		3,
		4,
		5,
		5,
		5,
		1
	};

	// Token: 0x04000B65 RID: 2917
	public static sbyte[] FRAME = new sbyte[]
	{
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0,
		1,
		2,
		1,
		0
	};
}
