using System;
using AssemblyCSharp.Mod.PickMob;
using Assets.src.g;

// Token: 0x02000069 RID: 105
public class Mob : IMapObject
{
	// Token: 0x0600050A RID: 1290 RVA: 0x0005D9C0 File Offset: 0x0005BBC0
	public Mob()
	{
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x0005DB2C File Offset: 0x0005BD2C
	public Mob(int mobId, bool isDisable, bool isDontMove, bool isFire, bool isIce, bool isWind, int templateId, int sys, int hp, sbyte level, int maxp, short pointx, short pointy, sbyte status, sbyte levelBoss)
	{
		this.isDisable = isDisable;
		this.isDontMove = isDontMove;
		this.isFire = isFire;
		this.isIce = isIce;
		this.isWind = isWind;
		this.sys = sys;
		this.mobId = mobId;
		this.templateId = templateId;
		this.hp = hp;
		this.level = level;
		this.pointx = pointx;
		this.x = (int)pointx;
		this.xFirst = (int)pointx;
		this.pointy = pointy;
		this.y = (int)pointy;
		this.yFirst = (int)pointy;
		this.status = (int)status;
		bool flag = templateId != 70;
		if (flag)
		{
			this.checkData();
			this.getData();
		}
		bool flag2 = !Mob.isExistNewMob(templateId + string.Empty);
		if (flag2)
		{
			Mob.newMob.addElement(templateId + string.Empty);
		}
		this.maxHp = maxp;
		this.levelBoss = levelBoss;
		this.isDie = false;
		this.xSd = (int)pointx;
		this.ySd = (int)pointy;
		bool flag3 = this.isNewModStand();
		if (flag3)
		{
			this.stand = new int[]
			{
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
				2,
				2,
				2,
				2,
				2,
				2,
				2
			};
			this.move = new int[]
			{
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
				2,
				2,
				2,
				2,
				2,
				2,
				2
			};
			this.moveFast = new int[]
			{
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
				2,
				2,
				2,
				2,
				2,
				2,
				2
			};
			this.attack1 = new int[]
			{
				3,
				3,
				3,
				3,
				4,
				4,
				4,
				4,
				5,
				5,
				5,
				5
			};
			this.attack2 = new int[]
			{
				3,
				3,
				3,
				3,
				4,
				4,
				4,
				4,
				5,
				5,
				5,
				5
			};
		}
		else
		{
			bool flag4 = this.isNewMod();
			if (flag4)
			{
				this.stand = new int[]
				{
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					1,
					1,
					1,
					1
				};
				this.move = new int[]
				{
					1,
					1,
					1,
					1,
					2,
					2,
					2,
					2,
					1,
					1,
					1,
					1,
					3,
					3,
					3,
					3
				};
				this.moveFast = new int[]
				{
					1,
					1,
					2,
					2,
					1,
					1,
					3,
					3
				};
				this.attack1 = new int[]
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
					6
				};
				this.attack2 = new int[]
				{
					7,
					7,
					7,
					8,
					8,
					8,
					9,
					9,
					9,
					9,
					9
				};
			}
			else
			{
				bool flag5 = this.isSpecial();
				if (flag5)
				{
					this.stand = new int[]
					{
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						1,
						1,
						1,
						1
					};
					this.move = new int[]
					{
						2,
						2,
						3,
						3,
						2,
						2,
						4,
						4,
						2,
						2,
						3,
						3,
						2,
						2,
						4,
						4
					};
					this.moveFast = new int[]
					{
						2,
						2,
						3,
						3,
						2,
						2,
						4,
						4
					};
					this.attack1 = new int[]
					{
						5,
						6,
						7,
						8,
						9,
						10,
						11,
						12
					};
					this.attack2 = new int[]
					{
						5,
						12,
						13,
						14
					};
				}
				else
				{
					this.stand = new int[]
					{
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						0,
						1,
						1,
						1,
						1
					};
					this.move = new int[]
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
					this.moveFast = new int[]
					{
						1,
						1,
						2,
						2,
						3,
						3,
						2
					};
					this.attack1 = new int[]
					{
						4,
						5,
						6
					};
					this.attack2 = new int[]
					{
						7,
						8,
						9
					};
				}
			}
		}
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x0005DFB0 File Offset: 0x0005C1B0
	public bool isBigBoss()
	{
		return this is BachTuoc || this is BigBoss2 || this is BigBoss || this is NewBoss;
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x0005DFE8 File Offset: 0x0005C1E8
	public void getData()
	{
		bool flag = Mob.arrMobTemplate[this.templateId].data == null;
		if (flag)
		{
			Mob.arrMobTemplate[this.templateId].data = new EffectData();
			string text = "/Mob/" + this.templateId;
			DataInputStream dataInputStream = MyStream.readFile(text);
			bool flag2 = dataInputStream != null;
			if (flag2)
			{
				Mob.arrMobTemplate[this.templateId].data.readData(text + "/data");
				Mob.arrMobTemplate[this.templateId].data.img = GameCanvas.loadImage(text + "/img.png");
			}
			else
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			Mob.lastMob.addElement(this.templateId + string.Empty);
		}
		else
		{
			this.w = Mob.arrMobTemplate[this.templateId].data.width;
			this.h = Mob.arrMobTemplate[this.templateId].data.height;
		}
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x000039F2 File Offset: 0x00001BF2
	public void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00003A03 File Offset: 0x00001C03
	public void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00007868 File Offset: 0x00005A68
	public static bool isExistNewMob(string id)
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

	// Token: 0x06000511 RID: 1297 RVA: 0x0005E10C File Offset: 0x0005C30C
	public void checkData()
	{
		int num = 0;
		for (int i = 0; i < Mob.arrMobTemplate.Length; i++)
		{
			bool flag = Mob.arrMobTemplate[i].data != null;
			if (flag)
			{
				num++;
			}
		}
		bool flag2 = num < 10;
		if (!flag2)
		{
			for (int j = 0; j < Mob.arrMobTemplate.Length; j++)
			{
				bool flag3 = Mob.arrMobTemplate[j].data != null && num > 5;
				if (flag3)
				{
					Mob.arrMobTemplate[j].data = null;
				}
			}
		}
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x0005E1A8 File Offset: 0x0005C3A8
	public void checkFrameTick(int[] array)
	{
		bool flag = this.tick > array.Length - 1;
		if (flag)
		{
			this.tick = 0;
		}
		this.frame = array[this.tick];
		this.tick++;
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x0005E1EC File Offset: 0x0005C3EC
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

	// Token: 0x06000514 RID: 1300 RVA: 0x0005E338 File Offset: 0x0005C538
	private void paintShadow(mGraphics g)
	{
		int size = (int)TileMap.size;
		bool flag = TileMap.tileTypeAt(this.xSd + size / 2, this.ySd + 1, 4);
		if (flag)
		{
			g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, size, 100);
		}
		else
		{
			bool flag2 = TileMap.tileTypeAt((this.xSd - size / 2) / size, (this.ySd + 1) / size) == 0;
			if (flag2)
			{
				g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, 100, 100);
			}
			else
			{
				bool flag3 = TileMap.tileTypeAt((this.xSd + size / 2) / size, (this.ySd + 1) / size) == 0;
				if (flag3)
				{
					g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, size, 100);
				}
				else
				{
					bool flag4 = TileMap.tileTypeAt(this.xSd - size / 2, this.ySd + 1, 8);
					if (flag4)
					{
						g.setClip(this.xSd / 24 * size, (this.ySd - 30) / size * size, size, 100);
					}
				}
			}
		}
		g.drawImage(TileMap.bong, this.xSd, this.ySd, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x0005E4A8 File Offset: 0x0005C6A8
	public void updateSuperEff()
	{
		bool flag = this.typeSuperEff == 0 && GameCanvas.gameTick % 25 == 0;
		if (flag)
		{
			ServerEffect.addServerEffect(114, this, 1);
		}
		bool flag2 = this.typeSuperEff == 1 && GameCanvas.gameTick % 4 == 0;
		if (flag2)
		{
			ServerEffect.addServerEffect(132, this, 1);
		}
		bool flag3 = this.typeSuperEff == 2 && GameCanvas.gameTick % 7 == 0;
		if (flag3)
		{
			ServerEffect.addServerEffect(131, this, 1);
		}
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x0005E530 File Offset: 0x0005C730
	public virtual void update()
	{
		Pk9rPickMob.UpdateCountDieMob(this);
		this.GetFrame();
		bool flag = this.blindEff && GameCanvas.gameTick % 5 == 0;
		if (flag)
		{
			ServerEffect.addServerEffect(113, this.x, this.y, 1);
		}
		bool flag2 = this.sleepEff && GameCanvas.gameTick % 10 == 0;
		if (flag2)
		{
			EffecMn.addEff(new Effect(41, this.x, this.y, 3, 1, 1));
		}
		bool flag3 = !GameCanvas.lowGraphic && this.status != 1 && this.status != 0 && !GameCanvas.lowGraphic && GameCanvas.gameTick % (15 + this.mobId * 2) == 0;
		if (flag3)
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				bool flag4 = @char != null && @char.isFlyAndCharge && @char.cf == 32;
				if (flag4)
				{
					global::Char char2 = new global::Char();
					char2.cx = @char.cx;
					char2.cy = @char.cy - @char.ch;
					bool flag5 = @char.cgender == 0;
					if (flag5)
					{
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), -100, -100, char2, 25);
					}
				}
			}
			bool flag6 = global::Char.myCharz().isFlyAndCharge && global::Char.myCharz().cf == 32;
			if (flag6)
			{
				global::Char char3 = new global::Char();
				char3.cx = global::Char.myCharz().cx;
				char3.cy = global::Char.myCharz().cy - global::Char.myCharz().ch;
				bool flag7 = global::Char.myCharz().cgender == 0;
				if (flag7)
				{
					MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), -100, -100, char3, 25);
				}
			}
		}
		bool flag8 = this.holdEffID != 0 && GameCanvas.gameTick % 5 == 0;
		if (flag8)
		{
			EffecMn.addEff(new Effect(this.holdEffID, this.x, this.y + 24, 3, 5, 1));
		}
		bool flag9 = this.isFreez;
		if (flag9)
		{
			bool flag10 = GameCanvas.gameTick % 5 == 0;
			if (flag10)
			{
				ServerEffect.addServerEffect(113, this.x, this.y, 1);
			}
			long num = mSystem.currentTimeMillis();
			bool flag11 = num - this.last >= 1000L;
			if (flag11)
			{
				this.seconds--;
				this.last = num;
				bool flag12 = this.seconds < 0;
				if (flag12)
				{
					this.isFreez = false;
					this.seconds = 0;
				}
			}
			bool flag13 = this.isTypeNewMod();
			if (flag13)
			{
				this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
			}
			else
			{
				bool flag14 = this.isNewModStand();
				if (flag14)
				{
					this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
				}
				else
				{
					bool flag15 = this.isNewMod();
					if (flag15)
					{
						bool flag16 = GameCanvas.gameTick % 20 > 5;
						if (flag16)
						{
							this.frame = 11;
						}
						else
						{
							this.frame = 10;
						}
					}
					else
					{
						bool flag17 = this.isSpecial();
						if (flag17)
						{
							bool flag18 = GameCanvas.gameTick % 20 > 5;
							if (flag18)
							{
								this.frame = 1;
							}
							else
							{
								this.frame = 15;
							}
						}
						else
						{
							bool flag19 = GameCanvas.gameTick % 20 > 5;
							if (flag19)
							{
								this.frame = 11;
							}
							else
							{
								this.frame = 10;
							}
						}
					}
				}
			}
		}
		bool flag20 = !this.isUpdate();
		if (!flag20)
		{
			bool flag21 = this.isShadown;
			if (flag21)
			{
				this.updateShadown();
			}
			bool flag22 = this.vMobMove == null && Mob.arrMobTemplate[this.templateId].rangeMove != 0;
			if (!flag22)
			{
				bool flag23 = this.status != 3 && this.isBusyAttackSomeOne;
				if (flag23)
				{
					bool flag24 = this.cFocus != null;
					if (flag24)
					{
						this.cFocus.doInjure(this.dame, this.dameMp, false, true);
					}
					else
					{
						bool flag25 = this.mobToAttack != null;
						if (flag25)
						{
							this.mobToAttack.setInjure();
						}
					}
					this.isBusyAttackSomeOne = false;
				}
				bool flag26 = this.levelBoss > 0;
				if (flag26)
				{
					this.updateSuperEff();
				}
				switch (this.status)
				{
				case 1:
				{
					this.isDisable = false;
					this.isDontMove = false;
					this.isFire = false;
					this.isIce = false;
					this.isWind = false;
					this.y += this.p1;
					bool flag27 = GameCanvas.gameTick % 2 == 0;
					if (flag27)
					{
						bool flag28 = this.p2 > 1;
						if (flag28)
						{
							this.p2--;
						}
						else
						{
							bool flag29 = this.p2 < -1;
							if (flag29)
							{
								this.p2++;
							}
						}
					}
					this.x += this.p2;
					bool flag30 = this.isTypeNewMod();
					if (flag30)
					{
						this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
					}
					else
					{
						bool flag31 = this.isNewModStand();
						if (flag31)
						{
							this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
						}
						else
						{
							bool flag32 = this.isNewMod();
							if (flag32)
							{
								this.frame = 11;
							}
							else
							{
								bool flag33 = this.isSpecial();
								if (flag33)
								{
									this.frame = 15;
								}
								else
								{
									this.frame = 11;
								}
							}
						}
					}
					bool flag34 = this.isDie;
					if (flag34)
					{
						this.isDie = false;
						bool flag35 = this.isMobMe;
						if (flag35)
						{
							for (int j = 0; j < GameScr.vMob.size(); j++)
							{
								bool flag36 = ((Mob)GameScr.vMob.elementAt(j)).mobId == this.mobId;
								if (flag36)
								{
									GameScr.vMob.removeElementAt(j);
								}
							}
						}
						this.p1 = 0;
						this.p2 = 0;
						this.x = (this.y = 0);
						this.hp = this.getTemplate().hp;
						this.status = 0;
						this.timeStatus = 0;
					}
					else
					{
						bool flag37 = (TileMap.tileTypeAtPixel(this.x, this.y) & 2) == 2;
						if (flag37)
						{
							this.p1 = ((this.p1 <= 4) ? (-this.p1) : -4);
							bool flag38 = this.p3 == 0;
							if (flag38)
							{
								this.p3 = 16;
							}
						}
						else
						{
							this.p1++;
						}
						bool flag39 = this.p3 > 0;
						if (flag39)
						{
							this.p3--;
							bool flag40 = this.p3 == 0;
							if (flag40)
							{
								this.isDie = true;
							}
						}
					}
					break;
				}
				case 2:
				{
					bool flag41 = this.holdEffID == 0 && !this.isFreez && !this.blindEff && !this.sleepEff;
					if (flag41)
					{
						this.timeStatus = 0;
						this.updateMobStandWait();
					}
					break;
				}
				case 3:
				{
					bool flag42 = this.holdEffID == 0 && !this.blindEff && !this.sleepEff && !this.isFreez;
					if (flag42)
					{
						this.updateMobAttack();
					}
					break;
				}
				case 4:
				{
					bool flag43 = this.holdEffID == 0 && !this.blindEff && !this.sleepEff && !this.isFreez;
					if (flag43)
					{
						this.timeStatus = 0;
						this.p1++;
						bool flag44 = this.p1 > 40 + this.mobId % 5;
						if (flag44)
						{
							this.y -= 2;
							this.status = 5;
							this.p1 = 0;
						}
					}
					break;
				}
				case 5:
				{
					bool flag45 = this.holdEffID != 0 || this.blindEff || this.sleepEff;
					if (!flag45)
					{
						bool flag46 = this.isFreez;
						if (flag46)
						{
							bool flag47 = Mob.arrMobTemplate[this.templateId].type == 4;
							if (flag47)
							{
								this.ty++;
								this.wt++;
								this.fy += ((!this.wy) ? 1 : -1);
								bool flag48 = this.wt == 10;
								if (flag48)
								{
									this.wt = 0;
									this.wy = !this.wy;
								}
							}
						}
						else
						{
							this.timeStatus = 0;
							this.updateMobWalk();
						}
					}
					break;
				}
				case 6:
				{
					this.timeStatus = 0;
					this.p1++;
					this.y += this.p1;
					bool flag49 = this.y >= this.yFirst;
					if (flag49)
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
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x0005EEC0 File Offset: 0x0005D0C0
	public void setInjure()
	{
		bool flag = this.hp > 0 && this.status != 3;
		if (flag)
		{
			this.timeStatus = 4;
			this.status = 7;
			bool flag2 = this.getTemplate().type != 0 && Res.abs(this.x - this.xFirst) < 30;
			if (flag2)
			{
				this.x -= 10 * this.dir;
			}
		}
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x0005EF3C File Offset: 0x0005D13C
	public static BigBoss getBigBoss()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			bool flag = mob is BigBoss;
			if (flag)
			{
				return (BigBoss)mob;
			}
		}
		return null;
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x0005EF94 File Offset: 0x0005D194
	public static BigBoss2 getBigBoss2()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			bool flag = mob is BigBoss2;
			if (flag)
			{
				return (BigBoss2)mob;
			}
		}
		return null;
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x0005EFEC File Offset: 0x0005D1EC
	public static BachTuoc getBachTuoc()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			bool flag = mob is BachTuoc;
			if (flag)
			{
				return (BachTuoc)mob;
			}
		}
		return null;
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x0005F044 File Offset: 0x0005D244
	public static NewBoss getNewBoss(sbyte idBoss)
	{
		Mob mob = (Mob)GameScr.vMob.elementAt((int)idBoss);
		bool flag = mob is NewBoss;
		NewBoss result;
		if (flag)
		{
			result = (NewBoss)mob;
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x0005F080 File Offset: 0x0005D280
	public static void removeBigBoss()
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			bool flag = mob is BigBoss;
			if (flag)
			{
				GameScr.vMob.removeElement(mob);
				break;
			}
		}
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x0005F0D8 File Offset: 0x0005D2D8
	public void setAttack(global::Char cFocus)
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

	// Token: 0x0600051E RID: 1310 RVA: 0x00007D50 File Offset: 0x00005F50
	private bool isSpecial()
	{
		return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x0005F1B8 File Offset: 0x0005D3B8
	private bool isNewMod()
	{
		return this.templateId >= 73 && !this.isNewModStand();
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x0005F1EC File Offset: 0x0005D3EC
	private bool isNewModStand()
	{
		return this.templateId == 76;
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x0005F214 File Offset: 0x0005D414
	private void updateInjure()
	{
		bool flag = !this.isBusyAttackSomeOne && GameCanvas.gameTick % 4 == 0;
		if (flag)
		{
			bool flag2 = this.isTypeNewMod();
			if (flag2)
			{
				this.frame = this.hurt[GameCanvas.gameTick % this.hurt.Length];
			}
			else
			{
				bool flag3 = this.isNewModStand();
				if (flag3)
				{
					this.frame = this.attack1[GameCanvas.gameTick % this.attack1.Length];
				}
				else
				{
					bool flag4 = this.isNewMod();
					if (flag4)
					{
						bool flag5 = this.frame != 10;
						if (flag5)
						{
							this.frame = 10;
						}
						else
						{
							this.frame = 11;
						}
					}
					else
					{
						bool flag6 = this.isSpecial();
						if (flag6)
						{
							bool flag7 = this.frame != 1;
							if (flag7)
							{
								this.frame = 1;
							}
							else
							{
								this.frame = 15;
							}
						}
						else
						{
							bool flag8 = this.frame != 10;
							if (flag8)
							{
								this.frame = 10;
							}
							else
							{
								this.frame = 11;
							}
						}
					}
				}
			}
		}
		this.timeStatus--;
		bool flag9 = this.timeStatus <= 0 && (this.isTypeNewMod() || this.isNewModStand() || (this.isNewMod() && this.frame == 11) || (this.isSpecial() && this.frame == 15) || (this.templateId < 58 && this.frame == 11));
		if (flag9)
		{
			bool flag10 = (this.injureBy != null && this.injureThenDie) || this.hp == 0;
			if (flag10)
			{
				this.status = 1;
				this.p2 = this.injureBy.cdir << 1;
				this.p1 = -3;
				this.p3 = 0;
			}
			else
			{
				this.status = 5;
				bool flag11 = this.injureBy != null;
				if (flag11)
				{
					this.dir = -this.injureBy.cdir;
					bool flag12 = Res.abs(this.x - this.injureBy.cx) < 24;
					if (flag12)
					{
						this.status = 2;
					}
				}
				this.p1 = (this.p2 = (this.p3 = 0));
				this.timeStatus = 0;
			}
			this.injureBy = null;
		}
		else
		{
			bool flag13 = Mob.arrMobTemplate[this.templateId].type != 0 && this.injureBy != null;
			if (flag13)
			{
				int num = -this.injureBy.cdir << 1;
				bool flag14 = this.x > this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove && this.x < this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove;
				if (flag14)
				{
					this.x -= num;
				}
			}
		}
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x0005F508 File Offset: 0x0005D708
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.stand);
		sbyte type = Mob.arrMobTemplate[this.templateId].type;
		if (type > 3)
		{
			if (type - 4 <= 1)
			{
				this.p1++;
				bool flag = this.p1 > this.mobId % 3 && (this.cFocus == null || Res.abs(this.cFocus.cx - this.x) > 80) && (this.mobToAttack == null || Res.abs(this.mobToAttack.x - this.x) > 80);
				if (flag)
				{
					this.status = 5;
				}
			}
		}
		else
		{
			this.p1++;
			bool flag2 = this.p1 > 10 + this.mobId % 10 && (this.cFocus == null || Res.abs(this.cFocus.cx - this.x) > 80) && (this.mobToAttack == null || Res.abs(this.mobToAttack.x - this.x) > 80);
			if (flag2)
			{
				this.status = 5;
			}
		}
		bool flag3 = this.cFocus != null && GameCanvas.gameTick % (10 + this.p1 % 20) == 0;
		if (flag3)
		{
			bool flag4 = this.cFocus.cx > this.x;
			if (flag4)
			{
				this.dir = 1;
			}
			else
			{
				this.dir = -1;
			}
		}
		else
		{
			bool flag5 = this.mobToAttack != null && GameCanvas.gameTick % (10 + this.p1 % 20) == 0;
			if (flag5)
			{
				bool flag6 = this.mobToAttack.x > this.x;
				if (flag6)
				{
					this.dir = 1;
				}
				else
				{
					this.dir = -1;
				}
			}
		}
		bool flag7 = this.forceWait > 0;
		if (flag7)
		{
			this.forceWait--;
			this.status = 2;
		}
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x0005F710 File Offset: 0x0005D910
	public void updateMobAttack()
	{
		int[] array = (this.p3 != 0) ? this.attack2 : this.attack1;
		bool flag = this.tick < array.Length;
		if (flag)
		{
			this.checkFrameTick(array);
			bool flag2 = this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w && this.p3 == 0 && GameCanvas.gameTick % 2 == 0;
			if (flag2)
			{
				SoundMn.gI().charPunch(false, 0.05f);
			}
		}
		bool flag3 = this.p1 == 0;
		if (flag3)
		{
			int num = (this.cFocus == null) ? this.mobToAttack.x : this.cFocus.cx;
			int num2 = (this.cFocus == null) ? this.mobToAttack.y : this.cFocus.cy;
			bool flag4 = !this.isNewMod();
			if (flag4)
			{
				bool flag5 = this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove;
				if (flag5)
				{
					this.p1 = 1;
				}
				bool flag6 = this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove;
				if (flag6)
				{
					this.p1 = 1;
				}
			}
			bool flag7 = (Mob.arrMobTemplate[this.templateId].type == 4 || Mob.arrMobTemplate[this.templateId].type == 5) && !this.isDontMove;
			if (flag7)
			{
				this.y += (num2 - this.y) / 20;
			}
			this.p2++;
			bool flag8 = this.p2 > array.Length - 1 || this.p1 == 1;
			if (flag8)
			{
				this.p1 = 1;
				bool flag9 = this.p3 == 0;
				if (flag9)
				{
					bool flag10 = this.cFocus != null;
					if (flag10)
					{
						this.cFocus.doInjure(this.dame, this.dameMp, false, true);
					}
					else
					{
						this.mobToAttack.setInjure();
					}
					this.isBusyAttackSomeOne = false;
				}
				else
				{
					bool flag11 = this.cFocus != null;
					if (flag11)
					{
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), this.dame, this.dameMp, this.cFocus, (int)this.getTemplate().dartType);
					}
					else
					{
						global::Char @char = new global::Char();
						@char.cx = this.mobToAttack.x;
						@char.cy = this.mobToAttack.y;
						@char.charID = -100;
						MonsterDart.addMonsterDart(this.x + this.dir * this.w, this.y, this.checkIsBoss(), this.dame, this.dameMp, @char, (int)this.getTemplate().dartType);
					}
					this.isBusyAttackSomeOne = false;
				}
			}
			this.dir = ((this.x < num) ? 1 : -1);
		}
		else
		{
			bool flag12 = this.p1 == 1;
			if (flag12)
			{
				bool flag13 = Mob.arrMobTemplate[this.templateId].type != 0 && !this.isDontMove && !this.isIce && !this.isWind;
				if (flag13)
				{
					this.x += (this.xFirst - this.x) / 4;
					this.y += (this.yFirst - this.y) / 4;
				}
				bool flag14 = Res.abs(this.xFirst - this.x) < 5 && Res.abs(this.yFirst - this.y) < 5 && this.tick == array.Length;
				if (flag14)
				{
					this.status = 2;
					this.p1 = 0;
					this.p2 = 0;
					this.tick = 0;
				}
			}
		}
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x0005FB18 File Offset: 0x0005DD18
	public void updateMobWalk()
	{
		int num = 0;
		try
		{
			bool flag = this.injureThenDie;
			if (flag)
			{
				this.status = 1;
				this.p2 = this.injureBy.cdir << 3;
				this.p1 = -5;
				this.p3 = 0;
			}
			num = 1;
			bool flag2 = this.isIce;
			if (!flag2)
			{
				bool flag3 = this.isDontMove || this.isWind;
				if (flag3)
				{
					this.checkFrameTick(this.stand);
				}
				else
				{
					switch (Mob.arrMobTemplate[this.templateId].type)
					{
					case 0:
					{
						bool flag4 = this.isNewModStand();
						if (flag4)
						{
							this.frame = this.stand[GameCanvas.gameTick % this.stand.Length];
						}
						else
						{
							this.frame = 0;
						}
						num = 2;
						break;
					}
					case 1:
					case 2:
					case 3:
					{
						num = 3;
						sbyte b = Mob.arrMobTemplate[this.templateId].speed;
						bool flag5 = b == 1;
						if (flag5)
						{
							bool flag6 = GameCanvas.gameTick % 2 == 1;
							if (flag6)
							{
								break;
							}
						}
						else
						{
							bool flag7 = b > 2;
							if (flag7)
							{
								b += (sbyte)(this.mobId % 2);
							}
							else
							{
								bool flag8 = GameCanvas.gameTick % 2 == 1;
								if (flag8)
								{
									b -= 1;
								}
							}
						}
						this.x += (int)b * this.dir;
						bool flag9 = this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove;
						if (flag9)
						{
							this.dir = -1;
						}
						else
						{
							bool flag10 = this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove;
							if (flag10)
							{
								this.dir = 1;
							}
						}
						bool flag11 = Res.abs(this.x - global::Char.myCharz().cx) < 40 && Res.abs(this.x - this.xFirst) < (int)Mob.arrMobTemplate[this.templateId].rangeMove;
						if (flag11)
						{
							this.dir = ((this.x <= global::Char.myCharz().cx) ? 1 : -1);
							bool flag12 = Res.abs(this.x - global::Char.myCharz().cx) < 20;
							if (flag12)
							{
								this.x -= this.dir * 10;
							}
							this.status = 2;
							this.forceWait = 20;
						}
						this.checkFrameTick((this.w <= 30) ? this.moveFast : this.move);
						break;
					}
					case 4:
					{
						num = 4;
						sbyte b2 = Mob.arrMobTemplate[this.templateId].speed;
						b2 += (sbyte)(this.mobId % 2);
						this.x += (int)b2 * this.dir;
						bool flag13 = GameCanvas.gameTick % 10 > 2;
						if (flag13)
						{
							this.y += (int)b2 * this.dirV;
						}
						b2 += (sbyte)((GameCanvas.gameTick + this.mobId) % 2);
						bool flag14 = this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove;
						if (flag14)
						{
							this.dir = -1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						else
						{
							bool flag15 = this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove;
							if (flag15)
							{
								this.dir = 1;
								this.status = 2;
								this.forceWait = GameCanvas.gameTick % 20 + 20;
								this.p1 = 0;
							}
						}
						bool flag16 = this.y > this.yFirst + 24;
						if (flag16)
						{
							this.dirV = -1;
						}
						else
						{
							bool flag17 = this.y < this.yFirst - (20 + GameCanvas.gameTick % 10);
							if (flag17)
							{
								this.dirV = 1;
							}
						}
						this.checkFrameTick(this.move);
						break;
					}
					case 5:
					{
						num = 5;
						sbyte b3 = Mob.arrMobTemplate[this.templateId].speed;
						b3 += (sbyte)(this.mobId % 2);
						this.x += (int)b3 * this.dir;
						b3 += (sbyte)((GameCanvas.gameTick + this.mobId) % 2);
						bool flag18 = GameCanvas.gameTick % 10 > 2;
						if (flag18)
						{
							this.y += (int)b3 * this.dirV;
						}
						bool flag19 = this.x > this.xFirst + (int)Mob.arrMobTemplate[this.templateId].rangeMove;
						if (flag19)
						{
							this.dir = -1;
							this.status = 2;
							this.forceWait = GameCanvas.gameTick % 20 + 20;
							this.p1 = 0;
						}
						else
						{
							bool flag20 = this.x < this.xFirst - (int)Mob.arrMobTemplate[this.templateId].rangeMove;
							if (flag20)
							{
								this.dir = 1;
								this.status = 2;
								this.forceWait = GameCanvas.gameTick % 20 + 20;
								this.p1 = 0;
							}
						}
						bool flag21 = this.y > this.yFirst + 24;
						if (flag21)
						{
							this.dirV = -1;
						}
						else
						{
							bool flag22 = this.y < this.yFirst - (20 + GameCanvas.gameTick % 10);
							if (flag22)
							{
								this.dirV = 1;
							}
						}
						bool flag23 = TileMap.tileTypeAt(this.x, this.y, 2);
						if (flag23)
						{
							bool flag24 = GameCanvas.gameTick % 10 > 5;
							if (flag24)
							{
								this.y = TileMap.tileYofPixel(this.y);
								this.status = 4;
								this.p1 = 0;
								this.dirV = -1;
							}
							else
							{
								this.dirV = -1;
							}
						}
						break;
					}
					}
				}
			}
		}
		catch (Exception)
		{
			Cout.println("lineee: " + num);
		}
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x00060134 File Offset: 0x0005E334
	public MobTemplate getTemplate()
	{
		return Mob.arrMobTemplate[this.templateId];
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x00060154 File Offset: 0x0005E354
	public bool isPaint()
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
						bool flag5 = Mob.arrMobTemplate[this.templateId] == null;
						if (flag5)
						{
							result = false;
						}
						else
						{
							bool flag6 = Mob.arrMobTemplate[this.templateId].data == null;
							if (flag6)
							{
								result = false;
							}
							else
							{
								bool flag7 = Mob.arrMobTemplate[this.templateId].data.img == null;
								if (flag7)
								{
									result = false;
								}
								else
								{
									bool flag8 = this.status == 0;
									result = !flag8;
								}
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x0006024C File Offset: 0x0005E44C
	public bool isUpdate()
	{
		bool flag = Mob.arrMobTemplate[this.templateId] == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = Mob.arrMobTemplate[this.templateId].data == null;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = this.status == 0;
				result = !flag3;
			}
		}
		return result;
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x00008154 File Offset: 0x00006354
	public bool checkIsBoss()
	{
		return this.isBoss || this.levelBoss > 0;
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x000602A8 File Offset: 0x0005E4A8
	public virtual void paint(mGraphics g)
	{
		bool flag = this.isShadown && this.status != 0;
		if (flag)
		{
			this.paintShadow(g);
		}
		bool flag2 = !this.isPaint() || (this.status == 1 && this.p3 > 0 && GameCanvas.gameTick % 3 == 0);
		if (!flag2)
		{
			g.translate(0, GameCanvas.transY);
			bool flag3 = !this.changBody;
			if (flag3)
			{
				Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.smallBody, this.x, this.y + this.fy - 14, 0, 3);
			}
			g.translate(0, -GameCanvas.transY);
			bool flag4 = global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.Equals(this) && this.status != 1;
			if (flag4)
			{
				int num = (int)((long)this.hp * 100L / (long)this.maxHp) / 10 - 1;
				bool flag5 = num < 0;
				if (flag5)
				{
					num = 0;
				}
				bool flag6 = num > 9;
				if (flag6)
				{
					num = 9;
				}
				g.drawRegion(Mob.imgHP, 0, 6 * (9 - num), 9, 6, 0, this.x, this.y - this.h - 10, 3);
			}
		}
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x00008360 File Offset: 0x00006560
	public int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x00060440 File Offset: 0x0005E640
	public void startDie()
	{
		Pk9rPickMob.MobStartDie(this);
		this.hp = 0;
		this.injureThenDie = true;
		this.hp = 0;
		this.status = 1;
		Res.outz("MOB DIEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEe");
		this.p1 = -3;
		this.p2 = -this.dir;
		this.p3 = 0;
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x00060498 File Offset: 0x0005E698
	public void attackOtherMob(Mob mobToAttack)
	{
		this.mobToAttack = mobToAttack;
		this.isBusyAttackSomeOne = true;
		this.cFocus = null;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
		this.dir = ((mobToAttack.x > this.x) ? 1 : -1);
		int num = mobToAttack.x;
		int num2 = mobToAttack.y;
		bool flag = Res.abs(num - this.x) < this.w * 2 && Res.abs(num2 - this.y) < this.h * 2;
		if (flag)
		{
			bool flag2 = this.x < num;
			if (flag2)
			{
				this.x = num - this.w;
			}
			else
			{
				this.x = num + this.w;
			}
			this.p3 = 0;
		}
		else
		{
			this.p3 = 1;
		}
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x00008458 File Offset: 0x00006658
	public int getX()
	{
		return this.x;
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x00060578 File Offset: 0x0005E778
	public int getY()
	{
		return this.y;
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x00060590 File Offset: 0x0005E790
	public int getH()
	{
		return this.h;
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x000605A8 File Offset: 0x0005E7A8
	public int getW()
	{
		return this.w;
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x000605C0 File Offset: 0x0005E7C0
	public void stopMoving()
	{
		bool flag = this.status == 5;
		if (flag)
		{
			this.status = 2;
			this.p1 = (this.p2 = (this.p3 = 0));
			this.forceWait = 50;
		}
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x000084E8 File Offset: 0x000066E8
	public bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x00008510 File Offset: 0x00006710
	public void removeHoldEff()
	{
		bool flag = this.holdEffID != 0;
		if (flag)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x00004F5E File Offset: 0x0000315E
	public void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x00004F68 File Offset: 0x00003168
	public void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x00060608 File Offset: 0x0005E808
	public void GetFrame()
	{
		bool flag = this.isGetFr && this.isTypeNewMod() && Mob.arrMobTemplate[this.templateId].data != null;
		if (flag)
		{
			this.frameArr = (int[][])Controller.frameHT_NEWBOSS.get(this.templateId + string.Empty);
			this.stand = this.frameArr[0];
			this.move = this.frameArr[1];
			this.moveFast = this.frameArr[2];
			this.attack1 = this.frameArr[3];
			this.attack2 = this.frameArr[4];
			this.hurt = this.frameArr[5];
			this.isGetFr = false;
		}
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x000606CC File Offset: 0x0005E8CC
	private bool isTypeNewMod()
	{
		return Mob.arrMobTemplate[this.templateId].data != null && Mob.arrMobTemplate[this.templateId].data.typeData == 2;
	}

	// Token: 0x04000AC2 RID: 2754
	public int countDie = 0;

	// Token: 0x04000AC3 RID: 2755
	public long timeLastDie = 0L;

	// Token: 0x04000AC4 RID: 2756
	public const sbyte TYPE_DUNG = 0;

	// Token: 0x04000AC5 RID: 2757
	public const sbyte TYPE_DI = 1;

	// Token: 0x04000AC6 RID: 2758
	public const sbyte TYPE_NHAY = 2;

	// Token: 0x04000AC7 RID: 2759
	public const sbyte TYPE_LET = 3;

	// Token: 0x04000AC8 RID: 2760
	public const sbyte TYPE_BAY = 4;

	// Token: 0x04000AC9 RID: 2761
	public const sbyte TYPE_BAY_DAU = 5;

	// Token: 0x04000ACA RID: 2762
	public static MobTemplate[] arrMobTemplate;

	// Token: 0x04000ACB RID: 2763
	public const sbyte MA_INHELL = 0;

	// Token: 0x04000ACC RID: 2764
	public const sbyte MA_DEADFLY = 1;

	// Token: 0x04000ACD RID: 2765
	public const sbyte MA_STANDWAIT = 2;

	// Token: 0x04000ACE RID: 2766
	public const sbyte MA_ATTACK = 3;

	// Token: 0x04000ACF RID: 2767
	public const sbyte MA_STANDFLY = 4;

	// Token: 0x04000AD0 RID: 2768
	public const sbyte MA_WALK = 5;

	// Token: 0x04000AD1 RID: 2769
	public const sbyte MA_FALL = 6;

	// Token: 0x04000AD2 RID: 2770
	public const sbyte MA_INJURE = 7;

	// Token: 0x04000AD3 RID: 2771
	public bool changBody;

	// Token: 0x04000AD4 RID: 2772
	public short smallBody;

	// Token: 0x04000AD5 RID: 2773
	public bool isHintFocus;

	// Token: 0x04000AD6 RID: 2774
	public string flystring;

	// Token: 0x04000AD7 RID: 2775
	public int flyx;

	// Token: 0x04000AD8 RID: 2776
	public int flyy;

	// Token: 0x04000AD9 RID: 2777
	public int flyIndex;

	// Token: 0x04000ADA RID: 2778
	public bool isFreez;

	// Token: 0x04000ADB RID: 2779
	public int seconds;

	// Token: 0x04000ADC RID: 2780
	public long last;

	// Token: 0x04000ADD RID: 2781
	public long cur;

	// Token: 0x04000ADE RID: 2782
	public int holdEffID;

	// Token: 0x04000ADF RID: 2783
	public int hp;

	// Token: 0x04000AE0 RID: 2784
	public int maxHp;

	// Token: 0x04000AE1 RID: 2785
	public int x;

	// Token: 0x04000AE2 RID: 2786
	public int y;

	// Token: 0x04000AE3 RID: 2787
	public int dir = 1;

	// Token: 0x04000AE4 RID: 2788
	public int dirV = 1;

	// Token: 0x04000AE5 RID: 2789
	public int status;

	// Token: 0x04000AE6 RID: 2790
	public int p1;

	// Token: 0x04000AE7 RID: 2791
	public int p2;

	// Token: 0x04000AE8 RID: 2792
	public int p3;

	// Token: 0x04000AE9 RID: 2793
	public int xFirst;

	// Token: 0x04000AEA RID: 2794
	public int yFirst;

	// Token: 0x04000AEB RID: 2795
	public int vy;

	// Token: 0x04000AEC RID: 2796
	public int exp;

	// Token: 0x04000AED RID: 2797
	public int w;

	// Token: 0x04000AEE RID: 2798
	public int h;

	// Token: 0x04000AEF RID: 2799
	public int hpInjure;

	// Token: 0x04000AF0 RID: 2800
	public int charIndex;

	// Token: 0x04000AF1 RID: 2801
	public int timeStatus;

	// Token: 0x04000AF2 RID: 2802
	public int mobId;

	// Token: 0x04000AF3 RID: 2803
	public bool isx;

	// Token: 0x04000AF4 RID: 2804
	public bool isy;

	// Token: 0x04000AF5 RID: 2805
	public bool isDisable;

	// Token: 0x04000AF6 RID: 2806
	public bool isDontMove;

	// Token: 0x04000AF7 RID: 2807
	public bool isFire;

	// Token: 0x04000AF8 RID: 2808
	public bool isIce;

	// Token: 0x04000AF9 RID: 2809
	public bool isWind;

	// Token: 0x04000AFA RID: 2810
	public bool isDie;

	// Token: 0x04000AFB RID: 2811
	public MyVector vMobMove = new MyVector();

	// Token: 0x04000AFC RID: 2812
	public bool isGo;

	// Token: 0x04000AFD RID: 2813
	public string mobName;

	// Token: 0x04000AFE RID: 2814
	public int templateId;

	// Token: 0x04000AFF RID: 2815
	public short pointx;

	// Token: 0x04000B00 RID: 2816
	public short pointy;

	// Token: 0x04000B01 RID: 2817
	public global::Char cFocus;

	// Token: 0x04000B02 RID: 2818
	public int dame;

	// Token: 0x04000B03 RID: 2819
	public int dameMp;

	// Token: 0x04000B04 RID: 2820
	public int sys;

	// Token: 0x04000B05 RID: 2821
	public sbyte levelBoss;

	// Token: 0x04000B06 RID: 2822
	public sbyte level;

	// Token: 0x04000B07 RID: 2823
	public bool isBoss;

	// Token: 0x04000B08 RID: 2824
	public bool isMobMe;

	// Token: 0x04000B09 RID: 2825
	public static MyVector lastMob = new MyVector();

	// Token: 0x04000B0A RID: 2826
	public static MyVector newMob = new MyVector();

	// Token: 0x04000B0B RID: 2827
	public int xSd;

	// Token: 0x04000B0C RID: 2828
	public int ySd;

	// Token: 0x04000B0D RID: 2829
	private bool isOutMap;

	// Token: 0x04000B0E RID: 2830
	private int wCount;

	// Token: 0x04000B0F RID: 2831
	public bool isShadown = true;

	// Token: 0x04000B10 RID: 2832
	private int tick;

	// Token: 0x04000B11 RID: 2833
	private int frame;

	// Token: 0x04000B12 RID: 2834
	public static Image imgHP = GameCanvas.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x04000B13 RID: 2835
	private bool wy;

	// Token: 0x04000B14 RID: 2836
	private int wt;

	// Token: 0x04000B15 RID: 2837
	private int fy;

	// Token: 0x04000B16 RID: 2838
	private int ty;

	// Token: 0x04000B17 RID: 2839
	public int typeSuperEff;

	// Token: 0x04000B18 RID: 2840
	public bool isBusyAttackSomeOne = true;

	// Token: 0x04000B19 RID: 2841
	public int[] stand = new int[]
	{
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1
	};

	// Token: 0x04000B1A RID: 2842
	public int[] move = new int[]
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

	// Token: 0x04000B1B RID: 2843
	public int[] moveFast = new int[]
	{
		1,
		1,
		2,
		2,
		3,
		3,
		2
	};

	// Token: 0x04000B1C RID: 2844
	public int[] attack1 = new int[]
	{
		4,
		5,
		6
	};

	// Token: 0x04000B1D RID: 2845
	public int[] attack2 = new int[]
	{
		7,
		8,
		9
	};

	// Token: 0x04000B1E RID: 2846
	public int[] hurt = new int[1];

	// Token: 0x04000B1F RID: 2847
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04000B20 RID: 2848
	public global::Char injureBy;

	// Token: 0x04000B21 RID: 2849
	public bool injureThenDie;

	// Token: 0x04000B22 RID: 2850
	public Mob mobToAttack;

	// Token: 0x04000B23 RID: 2851
	public int forceWait;

	// Token: 0x04000B24 RID: 2852
	public bool blindEff;

	// Token: 0x04000B25 RID: 2853
	public bool sleepEff;

	// Token: 0x04000B26 RID: 2854
	private int[][] frameArr = new int[][]
	{
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		},
		new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1
		}
	};

	// Token: 0x04000B27 RID: 2855
	private bool isGetFr = true;
}
