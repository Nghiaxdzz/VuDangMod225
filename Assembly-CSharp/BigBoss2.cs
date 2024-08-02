using System;

// Token: 0x0200000E RID: 14
public class BigBoss2 : Mob, IMapObject
{
	// Token: 0x0600006E RID: 110 RVA: 0x0000B128 File Offset: 0x00009328
	public BigBoss2(int id, short px, short py, int templateID, int hp, int maxHp, int s)
	{
		bool flag = BigBoss2.shadowBig == null;
		if (flag)
		{
			BigBoss2.shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");
		}
		this.mobId = id;
		this.xTo = (this.x = (int)(px + 20));
		this.y = (int)py;
		this.yTo = (int)py;
		this.yFirst = (int)py;
		this.hp = hp;
		this.maxHp = maxHp;
		this.templateId = templateID;
		this.getDataB();
		this.status = 2;
	}

	// Token: 0x0600006F RID: 111 RVA: 0x0000B294 File Offset: 0x00009494
	public void getDataB()
	{
		BigBoss2.data = null;
		BigBoss2.data = new EffectData();
		string patch = string.Concat(new object[]
		{
			"/x",
			mGraphics.zoomLevel,
			"/effectdata/",
			109,
			"/data"
		});
		try
		{
			BigBoss2.data.readData2(patch);
			BigBoss2.data.img = GameCanvas.loadImage("/effectdata/" + 109 + "/img.png");
		}
		catch (Exception)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.w = BigBoss2.data.width;
		this.h = BigBoss2.data.height;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x000039F2 File Offset: 0x00001BF2
	public new void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00003A03 File Offset: 0x00001C03
	public new void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00007868 File Offset: 0x00005A68
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

	// Token: 0x06000073 RID: 115 RVA: 0x0000B368 File Offset: 0x00009568
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

	// Token: 0x06000074 RID: 116 RVA: 0x0000B3AC File Offset: 0x000095AC
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

	// Token: 0x06000075 RID: 117 RVA: 0x0000B4F8 File Offset: 0x000096F8
	private void paintShadow(mGraphics g)
	{
		int size = (int)TileMap.size;
		g.drawImage(BigBoss2.shadowBig, this.xSd, this.yFirst, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00003A0D File Offset: 0x00001C0D
	public new void updateSuperEff()
	{
	}

	// Token: 0x06000077 RID: 119 RVA: 0x0000B550 File Offset: 0x00009750
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
			case 4:
				this.timeStatus = 0;
				this.updateMobFly();
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

	// Token: 0x06000078 RID: 120 RVA: 0x0000B650 File Offset: 0x00009850
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

	// Token: 0x06000079 RID: 121 RVA: 0x0000B72C File Offset: 0x0000992C
	private void updateMobFly()
	{
		bool flag = this.flyUp;
		if (flag)
		{
			this.dy++;
			this.y -= this.dy;
			this.checkFrameTick(this.fly);
			bool flag2 = this.y <= -500;
			if (flag2)
			{
				this.flyUp = false;
				this.flyDown = true;
				this.dy = 0;
			}
		}
		bool flag3 = this.flyDown;
		if (flag3)
		{
			this.x = this.xTo;
			this.dy += 2;
			this.y += this.dy;
			this.checkFrameTick(this.hitground);
			bool flag4 = this.y > this.yFirst;
			if (flag4)
			{
				this.y = this.yFirst;
				this.flyDown = false;
				this.dy = 0;
				this.status = 2;
				GameScr.shock_scr = 10;
				this.shock = true;
			}
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00003A0D File Offset: 0x00001C0D
	public new void setInjure()
	{
	}

	// Token: 0x0600007B RID: 123 RVA: 0x0000B828 File Offset: 0x00009A28
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

	// Token: 0x0600007C RID: 124 RVA: 0x00007D50 File Offset: 0x00005F50
	private bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00003A0D File Offset: 0x00001C0D
	private void updateInjure()
	{
	}

	// Token: 0x0600007E RID: 126 RVA: 0x0000B908 File Offset: 0x00009B08
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

	// Token: 0x0600007F RID: 127 RVA: 0x00003B3E File Offset: 0x00001D3E
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00003B4F File Offset: 0x00001D4F
	public void setAttack(global::Char[] cAttack, int[] dame, sbyte type)
	{
		this.status = 3;
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.tick = 0;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x0000B984 File Offset: 0x00009B84
	public new void updateMobAttack()
	{
		bool flag = this.type == 0;
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
		bool flag4 = this.type == 1;
		if (flag4)
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
					MonsterDart.addMonsterDart(this.x + ((this.dir != 1) ? -45 : 45), this.y - 25, true, this.dameHP[j], 0, this.charAttack[j], 24);
				}
			}
		}
		bool flag7 = this.type != 2;
		if (!flag7)
		{
			bool flag8 = this.tick == this.fly.Length - 1;
			if (flag8)
			{
				this.status = 2;
			}
			this.dir = ((this.x < this.charAttack[0].cx) ? 1 : -1);
			this.checkFrameTick(this.fly);
			this.x += (this.charAttack[0].cx - this.x) / 4;
			this.xTo = this.x;
			this.yTo = this.y;
			bool flag9 = this.tick == 12;
			if (flag9)
			{
				for (int k = 0; k < this.charAttack.Length; k++)
				{
					this.charAttack[k].doInjure(this.dameHP[k], 0, false, false);
					ServerEffect.addServerEffect(102, this.charAttack[k].cx, this.charAttack[k].cy, 1);
				}
			}
		}
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00003A0D File Offset: 0x00001C0D
	public new void updateMobWalk()
	{
	}

	// Token: 0x06000083 RID: 131 RVA: 0x0000809C File Offset: 0x0000629C
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

	// Token: 0x06000084 RID: 132 RVA: 0x0000812C File Offset: 0x0000632C
	public new bool isUpdate()
	{
		bool flag = this.status == 0;
		return !flag;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00008154 File Offset: 0x00006354
	public new bool checkIsBoss()
	{
		return this.isBoss || this.levelBoss > 0;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x0000BC84 File Offset: 0x00009E84
	public override void paint(mGraphics g)
	{
		bool flag = BigBoss2.data == null;
		if (!flag)
		{
			bool flag2 = this.isShadown && this.status != 0;
			if (flag2)
			{
				this.paintShadow(g);
			}
			g.translate(0, GameCanvas.transY);
			BigBoss2.data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
			g.translate(0, -GameCanvas.transY);
			int num = (int)((long)this.hp * 50L / (long)this.maxHp);
			bool flag3 = num != 0;
			if (flag3)
			{
				g.setColor(0);
				g.fillRect(this.x - 27, this.y - 112, 54, 8);
				g.setColor(16711680);
				g.setClip(this.x - 25, this.y - 110, num, 4);
				g.fillRect(this.x - 25, this.y - 110, 50, 4);
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

	// Token: 0x06000087 RID: 135 RVA: 0x00008360 File Offset: 0x00006560
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00003B75 File Offset: 0x00001D75
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

	// Token: 0x06000089 RID: 137 RVA: 0x0000BE60 File Offset: 0x0000A060
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

	// Token: 0x0600008A RID: 138 RVA: 0x00008458 File Offset: 0x00006658
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x0600008B RID: 139 RVA: 0x0000BF40 File Offset: 0x0000A140
	public new int getY()
	{
		return this.y - 50;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x0000848C File Offset: 0x0000668C
	public new int getH()
	{
		return 40;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x0000BF5C File Offset: 0x0000A15C
	public new int getW()
	{
		return 50;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x0000BF70 File Offset: 0x0000A170
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

	// Token: 0x0600008F RID: 143 RVA: 0x000084E8 File Offset: 0x000066E8
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00008510 File Offset: 0x00006710
	public new void removeHoldEff()
	{
		bool flag = this.holdEffID != 0;
		if (flag)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00003BB0 File Offset: 0x00001DB0
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00003BBA File Offset: 0x00001DBA
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x040000B2 RID: 178
	public static Image shadowBig;

	// Token: 0x040000B3 RID: 179
	public static EffectData data;

	// Token: 0x040000B4 RID: 180
	public int xTo;

	// Token: 0x040000B5 RID: 181
	public int yTo;

	// Token: 0x040000B6 RID: 182
	public bool haftBody;

	// Token: 0x040000B7 RID: 183
	public bool change;

	// Token: 0x040000B8 RID: 184
	private Mob mob1;

	// Token: 0x040000B9 RID: 185
	public new int xSd;

	// Token: 0x040000BA RID: 186
	public new int ySd;

	// Token: 0x040000BB RID: 187
	private bool isOutMap;

	// Token: 0x040000BC RID: 188
	private int wCount;

	// Token: 0x040000BD RID: 189
	public new bool isShadown = true;

	// Token: 0x040000BE RID: 190
	private int tick;

	// Token: 0x040000BF RID: 191
	private int frame;

	// Token: 0x040000C0 RID: 192
	public new static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x040000C1 RID: 193
	private bool wy;

	// Token: 0x040000C2 RID: 194
	private int wt;

	// Token: 0x040000C3 RID: 195
	private int fy;

	// Token: 0x040000C4 RID: 196
	private int ty;

	// Token: 0x040000C5 RID: 197
	public new int typeSuperEff;

	// Token: 0x040000C6 RID: 198
	private global::Char focus;

	// Token: 0x040000C7 RID: 199
	private int timeDead;

	// Token: 0x040000C8 RID: 200
	private bool flyUp;

	// Token: 0x040000C9 RID: 201
	private bool flyDown;

	// Token: 0x040000CA RID: 202
	private int dy;

	// Token: 0x040000CB RID: 203
	public bool changePos;

	// Token: 0x040000CC RID: 204
	private int tShock;

	// Token: 0x040000CD RID: 205
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x040000CE RID: 206
	private int tA;

	// Token: 0x040000CF RID: 207
	private global::Char[] charAttack;

	// Token: 0x040000D0 RID: 208
	private int[] dameHP;

	// Token: 0x040000D1 RID: 209
	private sbyte type;

	// Token: 0x040000D2 RID: 210
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

	// Token: 0x040000D3 RID: 211
	public new int[] move = new int[]
	{
		1,
		1,
		1,
		1,
		2,
		2,
		2,
		2,
		3,
		3,
		3,
		3,
		2,
		2,
		2
	};

	// Token: 0x040000D4 RID: 212
	public new int[] moveFast = new int[]
	{
		1,
		1,
		2,
		2,
		3,
		3,
		2
	};

	// Token: 0x040000D5 RID: 213
	public new int[] attack1 = new int[]
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
		9
	};

	// Token: 0x040000D6 RID: 214
	public new int[] attack2 = new int[]
	{
		0,
		0,
		0,
		10,
		10,
		10,
		11,
		11,
		11,
		12,
		12,
		12
	};

	// Token: 0x040000D7 RID: 215
	public int[] attack3 = new int[]
	{
		0,
		0,
		1,
		1,
		4,
		4,
		6,
		6,
		8,
		8,
		25,
		25,
		26,
		26,
		28,
		28,
		30,
		30,
		32,
		32,
		2,
		2,
		1,
		1
	};

	// Token: 0x040000D8 RID: 216
	public int[] fly = new int[]
	{
		4,
		4,
		4,
		5,
		5,
		5,
		6,
		6,
		6,
		6,
		6,
		6,
		3,
		3,
		3,
		2,
		2,
		2,
		1,
		1,
		1
	};

	// Token: 0x040000D9 RID: 217
	public int[] hitground = new int[]
	{
		6,
		6,
		6,
		3,
		3,
		3,
		2,
		2,
		2,
		1,
		1,
		1
	};

	// Token: 0x040000DA RID: 218
	private bool shock;

	// Token: 0x040000DB RID: 219
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x040000DC RID: 220
	public new global::Char injureBy;

	// Token: 0x040000DD RID: 221
	public new bool injureThenDie;

	// Token: 0x040000DE RID: 222
	public new Mob mobToAttack;

	// Token: 0x040000DF RID: 223
	public new int forceWait;

	// Token: 0x040000E0 RID: 224
	public new bool blindEff;

	// Token: 0x040000E1 RID: 225
	public new bool sleepEff;
}
