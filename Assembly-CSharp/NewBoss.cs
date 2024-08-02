using System;

// Token: 0x0200007E RID: 126
public class NewBoss : Mob, IMapObject
{
	// Token: 0x060005FD RID: 1533 RVA: 0x00065954 File Offset: 0x00063B54
	public NewBoss(int id, short px, short py, int templateID, int hp, int maxHp, int s)
	{
		this.mobId = id;
		this.x = (this.xFirst = (int)(px + 20));
		this.yFirst = (int)py;
		this.y = (int)py;
		this.xTo = this.x;
		this.yTo = this.y;
		this.maxHp = maxHp;
		this.hp = hp;
		this.templateId = templateID;
		bool flag = Mob.arrMobTemplate[this.templateId].data == null;
		if (flag)
		{
			Service.gI().requestModTemplate(this.templateId);
		}
		this.status = 2;
		this.frameArr = null;
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x000039F2 File Offset: 0x00001BF2
	public new void setBody(short id)
	{
		this.changBody = true;
		this.smallBody = id;
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x00003A03 File Offset: 0x00001C03
	public new void clearBody()
	{
		this.changBody = false;
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x00007868 File Offset: 0x00005A68
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

	// Token: 0x06000601 RID: 1537 RVA: 0x00065B88 File Offset: 0x00063D88
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

	// Token: 0x06000602 RID: 1538 RVA: 0x00065BCC File Offset: 0x00063DCC
	public void updateShadown()
	{
		int i = 0;
		this.xSd = this.x;
		bool flag = TileMap.tileTypeAt(this.x, this.y, 2);
		if (flag)
		{
			this.ySd = this.y;
		}
		else
		{
			this.ySd = this.y;
			while (i < 30)
			{
				i++;
				this.ySd += 24;
				bool flag2 = TileMap.tileTypeAt(this.xSd, this.ySd, 2);
				if (flag2)
				{
					bool flag3 = this.ySd % 24 != 0;
					if (flag3)
					{
						this.ySd -= this.ySd % 24;
					}
					break;
				}
			}
		}
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x00065C7C File Offset: 0x00063E7C
	private void paintShadow(mGraphics g)
	{
		int size = (int)TileMap.size;
		bool flag = (TileMap.mapID < 114 || TileMap.mapID > 120) && TileMap.mapID != 127 && TileMap.mapID != 128;
		if (flag)
		{
			bool flag2 = TileMap.tileTypeAt(this.xSd + size / 2, this.ySd + 1, 4);
			if (flag2)
			{
				g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, size, 100);
			}
			else
			{
				bool flag3 = TileMap.tileTypeAt((this.xSd - size / 2) / size, (this.ySd + 1) / size) == 0;
				if (flag3)
				{
					g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, 100, 100);
				}
				else
				{
					bool flag4 = TileMap.tileTypeAt((this.xSd + size / 2) / size, (this.ySd + 1) / size) == 0;
					if (flag4)
					{
						g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, size, 100);
					}
					else
					{
						bool flag5 = TileMap.tileTypeAt(this.xSd - size / 2, this.ySd + 1, 8);
						if (flag5)
						{
							g.setClip(this.xSd / 24 * size, (this.ySd - 30) / size * size, size, 100);
						}
					}
				}
			}
		}
		g.drawImage(NewBoss.shadowBig, this.xSd, this.ySd - 5, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x00003A0D File Offset: 0x00001C0D
	public new void updateSuperEff()
	{
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x00065E24 File Offset: 0x00064024
	public override void update()
	{
		bool flag = this.frameArr == null && Mob.arrMobTemplate[this.templateId].data != null;
		if (flag)
		{
			this.GetFrame();
		}
		bool flag2 = this.frameArr == null || !this.isUpdate();
		if (!flag2)
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
				bool flag3 = this.y >= this.yFirst;
				if (flag3)
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

	// Token: 0x06000606 RID: 1542 RVA: 0x00065F54 File Offset: 0x00064154
	private void updateDead()
	{
		this.tick++;
		bool flag = this.tick > this.frameArr[13].Length - 1;
		if (flag)
		{
			this.tick = this.frameArr[13].Length - 1;
		}
		this.frame = this.frameArr[13][this.tick];
		bool flag2 = this.x != this.xTo || this.y != this.yTo;
		if (flag2)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x00003A0D File Offset: 0x00001C0D
	private void updateMobFly()
	{
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x00066014 File Offset: 0x00064214
	public new void setAttack(global::Char cFocus)
	{
		this.isBusyAttackSomeOne = true;
		this.mobToAttack = null;
		this.cFocus = cFocus;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
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

	// Token: 0x06000609 RID: 1545 RVA: 0x00003A0D File Offset: 0x00001C0D
	private void updateInjure()
	{
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x000660DC File Offset: 0x000642DC
	private void updateMobStandWait()
	{
		this.checkFrameTick(this.frameArr[0]);
		bool flag = this.x != this.xTo || this.y != this.yTo;
		if (flag)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x000053CB File Offset: 0x000035CB
	public void setFly()
	{
		this.status = 4;
		this.flyUp = true;
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0006615C File Offset: 0x0006435C
	public void setAttack(global::Char[] cAttack, int[] dame, sbyte type, sbyte dir)
	{
		this.charAttack = cAttack;
		this.dameHP = dame;
		this.type = type;
		this.dir = (int)dir;
		this.status = 3;
		bool flag = this.x != this.xTo || this.y != this.yTo;
		if (flag)
		{
			this.x += (this.xTo - this.x) / 4;
			this.y += (this.yTo - this.y) / 4;
		}
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x000661F0 File Offset: 0x000643F0
	public new void updateMobAttack()
	{
		bool flag = this.tick == this.frameArr[(int)(this.type + 1)].Length - 1;
		if (flag)
		{
			this.status = 2;
		}
		this.checkFrameTick(this.frameArr[(int)(this.type + 1)]);
		bool flag2 = this.tick == this.frameArr[15][(int)(this.type - 1)];
		if (flag2)
		{
			for (int i = 0; i < this.charAttack.Length; i++)
			{
				this.charAttack[i].doInjure(this.dameHP[i], 0, false, false);
				ServerEffect.addServerEffect(this.frameArr[16][(int)(this.type - 1)], this.charAttack[i].cx, this.charAttack[i].cy, 1);
			}
		}
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x000662C4 File Offset: 0x000644C4
	public new void updateMobWalk()
	{
		this.checkFrameTick(this.frameArr[1]);
		sbyte speed = Mob.arrMobTemplate[this.templateId].speed;
		int num = (int)speed;
		bool flag = Res.abs(this.x - this.xTo) < (int)speed;
		if (flag)
		{
			num = Res.abs(this.x - this.xTo);
		}
		this.x += ((this.x >= this.xTo) ? (-num) : num);
		this.y = this.yTo;
		bool flag2 = this.x < this.xTo;
		if (flag2)
		{
			this.dir = 1;
		}
		else
		{
			bool flag3 = this.x > this.xTo;
			if (flag3)
			{
				this.dir = -1;
			}
		}
		bool flag4 = Res.abs(this.x - this.xTo) <= 1;
		if (flag4)
		{
			this.x = this.xTo;
			this.status = 2;
		}
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x0000809C File Offset: 0x0000629C
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

	// Token: 0x06000610 RID: 1552 RVA: 0x0000812C File Offset: 0x0000632C
	public new bool isUpdate()
	{
		bool flag = this.status == 0;
		return !flag;
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x000663BC File Offset: 0x000645BC
	public override void paint(mGraphics g)
	{
		bool flag = Mob.arrMobTemplate[this.templateId].data != null;
		if (flag)
		{
			bool flag2 = this.isShadown;
			if (flag2)
			{
				this.paintShadow(g);
			}
			g.translate(0, GameCanvas.transY);
			Mob.arrMobTemplate[this.templateId].data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
			g.translate(0, -GameCanvas.transY);
			int num = (int)((long)this.hp * 50L / (long)this.maxHp);
			bool flag3 = num != 0;
			if (flag3)
			{
				int num2 = this.y - this.h - 5;
				g.setColor(0);
				g.fillRect(this.x - 27, num2 - 2, 54, 8);
				g.setColor(16711680);
				g.fillRect(this.x - 25, num2, num, 4);
			}
		}
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x00008360 File Offset: 0x00006560
	public new int getHPColor()
	{
		return 16711680;
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x000053DC File Offset: 0x000035DC
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

	// Token: 0x06000614 RID: 1556 RVA: 0x000664C4 File Offset: 0x000646C4
	public new void attackOtherMob(Mob mobToAttack)
	{
		this.mobToAttack = mobToAttack;
		this.isBusyAttackSomeOne = true;
		this.cFocus = null;
		this.p1 = 0;
		this.p2 = 0;
		this.status = 3;
		this.tick = 0;
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

	// Token: 0x06000615 RID: 1557 RVA: 0x00008458 File Offset: 0x00006658
	public new int getX()
	{
		return this.x;
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x00060578 File Offset: 0x0005E778
	public new int getY()
	{
		return this.y;
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x00060590 File Offset: 0x0005E790
	public new int getH()
	{
		return this.h;
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x000605A8 File Offset: 0x0005E7A8
	public new int getW()
	{
		return this.w;
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0006658C File Offset: 0x0006478C
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

	// Token: 0x0600061A RID: 1562 RVA: 0x000084E8 File Offset: 0x000066E8
	public new bool isInvisible()
	{
		return this.status == 0 || this.status == 1;
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x00008510 File Offset: 0x00006710
	public new void removeHoldEff()
	{
		bool flag = this.holdEffID != 0;
		if (flag)
		{
			this.holdEffID = 0;
		}
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x00005417 File Offset: 0x00003617
	public new void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x00005421 File Offset: 0x00003621
	public new void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x000665D4 File Offset: 0x000647D4
	public new void move(short xMoveTo, short yMoveTo)
	{
		bool flag = yMoveTo != -1;
		if (flag)
		{
			bool flag2 = Res.distance(this.x, this.y, this.xTo, this.yTo) > 100;
			if (flag2)
			{
				this.x = (int)xMoveTo;
				this.y = (int)yMoveTo;
				this.status = 2;
			}
			else
			{
				this.xTo = (int)xMoveTo;
				this.yTo = (int)yMoveTo;
				this.status = 5;
			}
		}
		else
		{
			this.xTo = (int)xMoveTo;
			this.status = 5;
		}
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x00066658 File Offset: 0x00064858
	public new void GetFrame()
	{
		this.frameArr = (int[][])Controller.frameHT_NEWBOSS.get(this.templateId + string.Empty);
		this.w = Mob.arrMobTemplate[this.templateId].data.width;
		this.h = Mob.arrMobTemplate[this.templateId].data.height;
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x0000542B File Offset: 0x0000362B
	public void setDie()
	{
		this.status = 0;
	}

	// Token: 0x04000D65 RID: 3429
	public static Image shadowBig = mSystem.loadImage("/mainImage/shadowBig.png");

	// Token: 0x04000D66 RID: 3430
	public int xTo;

	// Token: 0x04000D67 RID: 3431
	public int yTo;

	// Token: 0x04000D68 RID: 3432
	public bool haftBody;

	// Token: 0x04000D69 RID: 3433
	public bool change;

	// Token: 0x04000D6A RID: 3434
	public new int xSd;

	// Token: 0x04000D6B RID: 3435
	public new int ySd;

	// Token: 0x04000D6C RID: 3436
	private int wCount;

	// Token: 0x04000D6D RID: 3437
	public new bool isShadown = true;

	// Token: 0x04000D6E RID: 3438
	private int tick;

	// Token: 0x04000D6F RID: 3439
	private int frame;

	// Token: 0x04000D70 RID: 3440
	public new static Image imgHP = mSystem.loadImage("/mainImage/myTexture2dmobHP.png");

	// Token: 0x04000D71 RID: 3441
	private bool wy;

	// Token: 0x04000D72 RID: 3442
	private int wt;

	// Token: 0x04000D73 RID: 3443
	private int fy;

	// Token: 0x04000D74 RID: 3444
	private int ty;

	// Token: 0x04000D75 RID: 3445
	public new int typeSuperEff;

	// Token: 0x04000D76 RID: 3446
	private global::Char focus;

	// Token: 0x04000D77 RID: 3447
	private bool flyUp;

	// Token: 0x04000D78 RID: 3448
	private bool flyDown;

	// Token: 0x04000D79 RID: 3449
	private int dy;

	// Token: 0x04000D7A RID: 3450
	public bool changePos;

	// Token: 0x04000D7B RID: 3451
	private int tShock;

	// Token: 0x04000D7C RID: 3452
	public new bool isBusyAttackSomeOne = true;

	// Token: 0x04000D7D RID: 3453
	private int tA;

	// Token: 0x04000D7E RID: 3454
	private global::Char[] charAttack;

	// Token: 0x04000D7F RID: 3455
	private int[] dameHP;

	// Token: 0x04000D80 RID: 3456
	private sbyte type;

	// Token: 0x04000D81 RID: 3457
	private int ff;

	// Token: 0x04000D82 RID: 3458
	private sbyte[] cou = new sbyte[]
	{
		-1,
		1
	};

	// Token: 0x04000D83 RID: 3459
	public new global::Char injureBy;

	// Token: 0x04000D84 RID: 3460
	public new bool injureThenDie;

	// Token: 0x04000D85 RID: 3461
	public new Mob mobToAttack;

	// Token: 0x04000D86 RID: 3462
	public new int forceWait;

	// Token: 0x04000D87 RID: 3463
	public new bool blindEff;

	// Token: 0x04000D88 RID: 3464
	public new bool sleepEff;

	// Token: 0x04000D89 RID: 3465
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

	// Token: 0x04000D8A RID: 3466
	public new const sbyte stand = 0;

	// Token: 0x04000D8B RID: 3467
	public const sbyte moveFra = 1;

	// Token: 0x04000D8C RID: 3468
	public new const sbyte attack1 = 2;

	// Token: 0x04000D8D RID: 3469
	public new const sbyte attack2 = 3;

	// Token: 0x04000D8E RID: 3470
	public const sbyte attack3 = 4;

	// Token: 0x04000D8F RID: 3471
	public const sbyte attack4 = 5;

	// Token: 0x04000D90 RID: 3472
	public const sbyte attack5 = 6;

	// Token: 0x04000D91 RID: 3473
	public const sbyte attack6 = 7;

	// Token: 0x04000D92 RID: 3474
	public const sbyte attack7 = 8;

	// Token: 0x04000D93 RID: 3475
	public const sbyte attack8 = 9;

	// Token: 0x04000D94 RID: 3476
	public const sbyte attack9 = 10;

	// Token: 0x04000D95 RID: 3477
	public const sbyte attack10 = 11;

	// Token: 0x04000D96 RID: 3478
	public new const sbyte hurt = 12;

	// Token: 0x04000D97 RID: 3479
	public const sbyte die = 13;

	// Token: 0x04000D98 RID: 3480
	public const sbyte fly = 14;

	// Token: 0x04000D99 RID: 3481
	public const sbyte adddame = 15;

	// Token: 0x04000D9A RID: 3482
	public const sbyte typeEff = 16;
}
