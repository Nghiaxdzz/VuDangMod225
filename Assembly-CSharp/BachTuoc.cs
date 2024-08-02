using System;

// Token: 0x02000009 RID: 9
public class BachTuoc : Mob, IMapObject
{
	// Token: 0x0600001E RID: 30 RVA: 0x0000767C File Offset: 0x0000587C
	public BachTuoc(int id, short px, short py, int templateID, int hp, int maxHp, int s)
	{
		this.mobId = id;
		this.xFirst = (this.x = (int)(px + 20));
		this.y = (int)py;
		this.yFirst = (int)py;
		this.xTo = this.x;
		this.yTo = this.y;
		this.maxHp = maxHp;
		this.hp = hp;
		this.templateId = templateID;
		this.getDataB();
		this.status = 2;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00007794 File Offset: 0x00005994
	public void getDataB()
	{
		BachTuoc.data = null;
		BachTuoc.data = new EffectData();
		string patch = string.Concat(new object[]
		{
			"/x",
			mGraphics.zoomLevel,
			"/effectdata/",
			108,
			"/data"
		});
		try
		{
			BachTuoc.data.readData2(patch);
			BachTuoc.data.img = GameCanvas.loadImage("/effectdata/" + 108 + "/img.png");
		}
		catch (Exception)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.w = BachTuoc.data.width;
		this.h = BachTuoc.data.height;
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000039F2 File Offset: 0x00001BF2
	public new void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00003A03 File Offset: 0x00001C03
	public new void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00007868 File Offset: 0x00005A68
	public new static bool isExistNewMob(string id)
	{
		for (int i = 0; i < Mob.newMob.size(); i++)
		{
			string text = (string)Mob.newMob.elementAt(i);
			bool flag = text.Equals(id);
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x000078BC File Offset: 0x00005ABC
	public new void checkFrameTick(int[] array)
	{
		this.tick++;
		bool flag = this.tick > array.Length - 1;
		if (flag)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00007900 File Offset: 0x00005B00
	private void updateShadown()
	{
		int size = (int)TileMap.size;
		this.xSd = this.x;
		this.wCount = 0;
		bool flag = this.ySd <= 0 || TileMap.tileTypeAt(this.xSd, this.ySd, 2);
		if (!flag)
		{
			bool flag2 = TileMap.tileTypeAt(this.xSd / size, this.ySd / size) == 0;
			if (flag2)
			{
				this.isOutMap = true;
			}
			else
			{
				bool flag3 = TileMap.tileTypeAt(this.xSd / size, this.ySd / size) != 0 && !TileMap.tileTypeAt(this.xSd, this.ySd, 2);
				if (flag3)
				{
					this.xSd = this.x;
					this.ySd = this.y;
					this.isOutMap = false;
				}
			}
			while (this.isOutMap && this.wCount < 10)
			{
				this.wCount++;
				this.ySd += 24;
				bool flag4 = TileMap.tileTypeAt(this.xSd, this.ySd, 2);
				if (flag4)
				{
					bool flag5 = this.ySd % 24 != 0;
					if (flag5)
					{
						this.ySd -= this.ySd % 24;
					}
					break;
				}
			}
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00007A4C File Offset: 0x00005C4C
	private void paintShadow(mGraphics g)
	{
		int size = (int)TileMap.size;
		g.drawImage(BachTuoc.shadowBig, this.xSd, this.yFirst, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00003A0D File Offset: 0x00001C0D
	public new void updateSuperEff()
	{
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00007AA4 File Offset: 0x00005CA4
	public override void update()
	{
		bool flag = !this.isUpdate();
		if (!flag)
		{
			this.updateShadown();
			switch (this.status)
			{
			case 0:
			case 1:
				this.updateDead();
				break;
			case 2:
				this.updateMobStandWait();
				break;
			case 3:
				this.updateMobAttack();
				break;
			case 5:
				this.timeStatus = 0;
				this.updateMobWalk();
				break;
			case 6:
			{
				this.timeStatus = 0;
				this.p1++;
				this.y += this.p1;
				bool flag2 = this.y >= this.yFirst;
				if (flag2)
				{
					this.y = this.yFirst;
					this.p1 = 0;
					this.status = 5;
				}
				break;
			}
			case 7:
				this.updateInjure();
				break;
			}
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00007B94 File Offset: 0x00005D94
	private void updateDead()
	{
		this.checkFrameTick(this.stand);
		bool flag = GameCanvas.gameTick % 5 == 0;
		if (flag)
		{
			ServerEffect.addServerEffect(167, Res.random(this.x - this.getW() / 2, this.x + this.getW() / 2), Res.random(this.getY() + this.getH() / 2, this.getY() + this.getH()), 1);
		}
		bool flag2 = this.x != this.xTo || this.y != this.yTo;
		if (flag2)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00003A0D File Offset: 0x00001C0D
	public new void setInjure()
	{
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00007C70 File Offset: 0x00005E70
	public new void setAttack(global::Char cFocus)
	{
		this.isBusyAttackSomeOne = true;
		this.mobToAttack = null;
		this.cFocus = cFocus;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((cFocus.cx > this.x) ? 1 : -1);
		int cx = cFocus.cx;
		int cy = cFocus.cy;
		bool flag = Res.abs(cx - this.x) < this.w * 2 && Res.abs(cy - this.y) < this.h * 2;
		if (flag)
		{
			bool flag2 = this.x < cx;
			if (flag2)
			{
				this.x = cx - this.w;
			}
			else
			{
				this.x = cx + this.w;
			}
			this.p3 = 0;
		}
		else
		{
			this.p3 = 1;
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00007D50 File Offset: 0x00005F50
	private bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00003A0D File Offset: 0x00001C0D
	private void updateInjure()
	{
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00007D98 File Offset: 0x00005F98
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		bool flag = this.x != this.xTo || this.y != this.yTo;
		if (flag)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00003A10 File Offset: 0x00001C10
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00003A21 File Offset: 0x00001C21
	public void setAttack(global::Char[] cAttack, int[] dame, sbyte type)
	{
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.status = 3;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00007E14 File Offset: 0x00006014
	public new void updateMobAttack()
	{
		bool flag = this.type == 3;
		if (flag)
		{
			bool flag2 = this.tick == this.attack1.Length - 1;
			if (flag2)
			{
				this.status = 2;
			}
			this.dir = ((this.x < this.charAttack[0].cx) ? 1 : -1);
			this.checkFrameTick(this.attack1);
			this.x += (this.charAttack[0].cx - this.x) / 4;
			this.y += (this.charAttack[0].cy - this.y) / 4;
			this.xTo = this.x;
			bool flag3 = this.tick == 8;
			if (flag3)
			{
				for (int i = 0; i < this.charAttack.Length; i++)
				{
					this.charAttack[i].doInjure(this.dameHP[i], 0, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[i].cx, this.charAttack[i].cy, 1);
				}
			}
		}
		bool flag4 = this.type != 4;
		if (!flag4)
		{
			bool flag5 = this.tick == this.attack2.Length - 1;
			if (flag5)
			{
				this.status = 2;
			}
			this.dir = ((this.x < this.charAttack[0].cx) ? 1 : -1);
			this.checkFrameTick(this.attack2);
			bool flag6 = this.tick == 8;
			if (flag6)
			{
				for (int j = 0; j < this.charAttack.Length; j++)
				{
					this.charAttack[j].doInjure(this.dameHP[j], 0, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[j].cx, this.charAttack[j].cy, 1);
				}
			}
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x0000800C File Offset: 0x0000620C
	public new void updateMobWalk()
	{
		this.checkFrameTick(this.movee);
		this.x += ((this.x >= this.xTo) ? -2 : 2);
		this.y = this.yTo;
		this.dir = ((this.x < this.xTo) ? 1 : -1);
		bool flag = Res.abs(this.x - this.xTo) <= 1;
		if (flag)
		{
			this.x = this.xTo;
			this.status = 2;
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x0000809C File Offset: 0x0000629C
	public new bool isPaint()
	{
		bool flag = this.x < GameScr.cmx;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = this.x > GameScr.cmx + GameScr.gW;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = this.y < GameScr.cmy;
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = this.y > GameScr.cmy + GameScr.gH + 30;
					if (flag4)
					{
						result = false;
					}
					else
					{
						bool flag5 = this.status == 0;
						result = !flag5;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x0000812C File Offset: 0x0000632C
	public new bool isUpdate()
	{
		bool flag = this.status == 0;
		return !flag;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00008154 File Offset: 0x00006354
	public new bool checkIsBoss()
	{
		return this.isBoss || this.levelBoss > 0;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00008184 File Offset: 0x00006384
	public override void paint(mGraphics g)
	{
		bool flag = BachTuoc.data == null;
		if (!flag)
		{
			bool flag2 = this.isShadown && this.status != 0;
			if (flag2)
			{
				this.paintShadow(g);
			}
			g.translate(0, GameCanvas.transY);
			BachTuoc.data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
			g.translate(0, -GameCanvas.transY);
			int num = (int)((long)this.hp * 50L / (long)this.maxHp);
			bool flag3 = num != 0;
			if (flag3)
			{
				g.setColor(0);
				g.fillRect(this.x - 27, this.y - 82, 54, 8);
				g.setColor(16711680);
				g.setClip(this.x - 25, this.y - 80, num, 4);
				g.fillRect(this.x - 25, this.y - 80, 50, 4);
				g.setClip(0, 0, 3000, 3000);
			}
			bool flag4 = this.shock;
			if (flag4)
			{
				this.tShock++;
				Effect me = new Effect((this.type != 2) ? 22 : 19, this.x + this.tShock * 50, this.y + 25, 2, 1, -1);
				EffecMn.addEff(me);
				Effect me2 = new Effect((this.type != 2) ? 22 : 19, this.x - this.tShock * 50, this.y + 25, 2, 1, -1);
				EffecMn.addEff(me2);
				bool flag5 = this.tShock == 50;
				if (flag5)
				{
					this.tShock = 0;
					this.shock = false;
				}
			}
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00008360 File Offset: 0x00006560
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00003A40 File Offset: 0x00001C40
	public new void startDie()
	{
		this.hp = 0;
		this.injureThenDie = true;
		this.hp = 0;
		this.status = 1;
		this.p1 = -3;
		this.p2 = -this.dir;
		this.p3 = 0;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00008378 File Offset: 0x00006578
	public new void attackOtherMob(Mob mobToAttack)
	{
		this.mobToAttack = mobToAttack;
		this.isBusyAttackSomeOne = true;
		this.cFocus = null;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((mobToAttack.x > this.x) ? 1 : -1);
		int x = mobToAttack.x;
		int y = mobToAttack.y;
		bool flag = Res.abs(x - this.x) < this.w * 2 && Res.abs(y - this.y) < this.h * 2;
		if (flag)
		{
			bool flag2 = this.x < x;
			if (flag2)
			{
				this.x = x - this.w;
			}
			else
			{
				this.x = x + this.w;
			}
			this.p3 = 0;
		}
		else
		{
			this.p3 = 1;
		}
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00008458 File Offset: 0x00006658
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00008470 File Offset: 0x00006670
	public new int getY()
	{
		return this.y - 40;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0000848C File Offset: 0x0000668C
	public new int getH()
	{
		return 40;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x0000848C File Offset: 0x0000668C
	public new int getW()
	{
		return 40;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x000084A0 File Offset: 0x000066A0
	public new void stopMoving()
	{
		bool flag = this.status == 5;
		if (flag)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000084E8 File Offset: 0x000066E8
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00008510 File Offset: 0x00006710
	public new void removeHoldEff()
	{
		bool flag = this.holdEffID != 0;
		if (flag)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003A7B File Offset: 0x00001C7B
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00003A85 File Offset: 0x00001C85
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00003A8F File Offset: 0x00001C8F
	public new void move(short xMoveTo)
	{
		this.xTo = (int)xMoveTo;
		this.status = 5;
	}

	// Token: 0x04000023 RID: 35
	public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

	// Token: 0x04000024 RID: 36
	public static EffectData data;

	// Token: 0x04000025 RID: 37
	public int xTo;

	// Token: 0x04000026 RID: 38
	public int yTo;

	// Token: 0x04000027 RID: 39
	public bool haftBody;

	// Token: 0x04000028 RID: 40
	public bool change;

	// Token: 0x04000029 RID: 41
	private Mob mob1;

	// Token: 0x0400002A RID: 42
	public new int xSd;

	// Token: 0x0400002B RID: 43
	public new int ySd;

	// Token: 0x0400002C RID: 44
	private bool isOutMap;

	// Token: 0x0400002D RID: 45
	private int wCount;

	// Token: 0x0400002E RID: 46
	public new bool isShadown = true;

	// Token: 0x0400002F RID: 47
	private int tick;

	// Token: 0x04000030 RID: 48
	private int frame;

	// Token: 0x04000031 RID: 49
	public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x04000032 RID: 50
	private bool wy;

	// Token: 0x04000033 RID: 51
	private int wt;

	// Token: 0x04000034 RID: 52
	private int fy;

	// Token: 0x04000035 RID: 53
	private int ty;

	// Token: 0x04000036 RID: 54
	public new int typeSuperEff;

	// Token: 0x04000037 RID: 55
	private global::Char focus;

	// Token: 0x04000038 RID: 56
	private bool flyUp;

	// Token: 0x04000039 RID: 57
	private bool flyDown;

	// Token: 0x0400003A RID: 58
	private int dy;

	// Token: 0x0400003B RID: 59
	public bool changePos;

	// Token: 0x0400003C RID: 60
	private int tShock;

	// Token: 0x0400003D RID: 61
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x0400003E RID: 62
	private int tA;

	// Token: 0x0400003F RID: 63
	private global::Char[] charAttack;

	// Token: 0x04000040 RID: 64
	private int[] dameHP;

	// Token: 0x04000041 RID: 65
	private sbyte type;

	// Token: 0x04000042 RID: 66
	public new int[] stand = new int[]
	{
		0,
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		1,
		1
	};

	// Token: 0x04000043 RID: 67
	public int[] movee = new int[]
	{
		0,
		0,
		0,
		2,
		2,
		2,
		3,
		3,
		3,
		4,
		4,
		4
	};

	// Token: 0x04000044 RID: 68
	public new int[] attack1 = new int[]
	{
		0,
		0,
		0,
		4,
		4,
		4,
		5,
		5,
		5,
		6,
		6,
		6
	};

	// Token: 0x04000045 RID: 69
	public new int[] attack2 = new int[]
	{
		0,
		0,
		0,
		7,
		7,
		7,
		8,
		8,
		8,
		9,
		9,
		9,
		10,
		10,
		10,
		11,
		11
	};

	// Token: 0x04000046 RID: 70
	public new int[] hurt = new int[]
	{
		1,
		1,
		7,
		7
	};

	// Token: 0x04000047 RID: 71
	private bool shock;

	// Token: 0x04000048 RID: 72
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04000049 RID: 73
	public new global::Char injureBy;

	// Token: 0x0400004A RID: 74
	public new bool injureThenDie;

	// Token: 0x0400004B RID: 75
	public new Mob mobToAttack;

	// Token: 0x0400004C RID: 76
	public new int forceWait;

	// Token: 0x0400004D RID: 77
	public new bool blindEff;

	// Token: 0x0400004E RID: 78
	public new bool sleepEff;
}
