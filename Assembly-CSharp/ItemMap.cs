using System;

// Token: 0x02000052 RID: 82
public class ItemMap : IMapObject
{
	// Token: 0x06000427 RID: 1063 RVA: 0x00054888 File Offset: 0x00052A88
	public ItemMap(short itemMapID, short itemTemplateID, int x, int y, int xEnd, int yEnd)
	{
		this.itemMapID = (int)itemMapID;
		this.template = ItemTemplates.get(itemTemplateID);
		this.x = xEnd;
		this.y = y;
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.vx = xEnd - x >> 2;
		this.vy = 5;
		Res.outz(string.Concat(new object[]
		{
			"playerid=  ",
			this.playerId,
			" myid= ",
			global::Char.myCharz().charID
		}));
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x0005492C File Offset: 0x00052B2C
	public ItemMap(int playerId, short itemMapID, short itemTemplateID, int x, int y, short r)
	{
		Res.outz(string.Concat(new object[]
		{
			"item map item= ",
			itemMapID,
			" template= ",
			itemTemplateID,
			" x= ",
			x,
			" y= ",
			y
		}));
		this.itemMapID = (int)itemMapID;
		this.template = ItemTemplates.get(itemTemplateID);
		Res.outz(string.Concat(new object[]
		{
			"playerid=  ",
			playerId,
			" myid= ",
			global::Char.myCharz().charID
		}));
		this.xEnd = x;
		this.x = x;
		this.yEnd = y;
		this.y = y;
		this.status = 1;
		this.playerId = playerId;
		bool flag = this.isAuraItem();
		if (flag)
		{
			this.rO = (int)r;
			this.setAuraItem();
		}
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x00004AF2 File Offset: 0x00002CF2
	public void setPoint(int xEnd, int yEnd)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.vx = xEnd - this.x >> 2;
		this.vy = yEnd - this.y >> 2;
		this.status = 2;
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x00054A3C File Offset: 0x00052C3C
	public void update()
	{
		bool flag = this.status == 2 && this.x == this.xEnd && this.y == this.yEnd;
		if (flag)
		{
			GameScr.vItemMap.removeElement(this);
			bool flag2 = global::Char.myCharz().itemFocus != null && global::Char.myCharz().itemFocus.Equals(this);
			if (flag2)
			{
				global::Char.myCharz().itemFocus = null;
			}
		}
		else
		{
			bool flag3 = this.status > 0;
			if (flag3)
			{
				bool flag4 = this.vx == 0;
				if (flag4)
				{
					this.x = this.xEnd;
				}
				bool flag5 = this.vy == 0;
				if (flag5)
				{
					this.y = this.yEnd;
				}
				bool flag6 = this.x != this.xEnd;
				if (flag6)
				{
					this.x += this.vx;
					bool flag7 = (this.vx > 0 && this.x > this.xEnd) || (this.vx < 0 && this.x < this.xEnd);
					if (flag7)
					{
						this.x = this.xEnd;
					}
				}
				bool flag8 = this.y != this.yEnd;
				if (flag8)
				{
					this.y += this.vy;
					bool flag9 = (this.vy > 0 && this.y > this.yEnd) || (this.vy < 0 && this.y < this.yEnd);
					if (flag9)
					{
						this.y = this.yEnd;
					}
				}
			}
			else
			{
				this.status -= 4;
				bool flag10 = this.status < -12;
				if (flag10)
				{
					this.y -= 12;
					this.status = 1;
				}
			}
			bool flag11 = this.isAuraItem();
			if (flag11)
			{
				this.updateAuraItemEff();
			}
		}
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x00054C3C File Offset: 0x00052E3C
	public void paint(mGraphics g)
	{
		bool flag = this.isAuraItem();
		if (flag)
		{
			g.drawImage(TileMap.bong, this.x + 3, this.y, mGraphics.VCENTER | mGraphics.HCENTER);
			bool flag2 = this.status <= 0;
			if (flag2)
			{
				bool flag3 = this.countAura < 10;
				if (flag3)
				{
					g.drawImage(ItemMap.imageAuraItem1, this.x, this.y + (int)this.status + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				else
				{
					g.drawImage(ItemMap.imageAuraItem2, this.x, this.y + (int)this.status + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
			else
			{
				bool flag4 = this.countAura < 10;
				if (flag4)
				{
					g.drawImage(ItemMap.imageAuraItem1, this.x, this.y + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				else
				{
					g.drawImage(ItemMap.imageAuraItem2, this.x, this.y + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
		}
		else
		{
			bool flag5 = !this.isAuraItem();
			if (flag5)
			{
				bool flag6 = GameCanvas.gameTick % 4 == 0;
				if (flag6)
				{
					g.drawImage(ItemMap.imageFlare, this.x, this.y + (int)this.status + 13, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				bool flag7 = this.status <= 0;
				if (flag7)
				{
					SmallImage.drawSmallImage(g, (int)this.template.iconID, this.x, this.y + (int)this.status + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				else
				{
					SmallImage.drawSmallImage(g, (int)this.template.iconID, this.x, this.y + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				bool flag8 = global::Char.myCharz().itemFocus != null && global::Char.myCharz().itemFocus.Equals(this) && this.status != 2;
				if (flag8)
				{
					g.drawRegion(Mob.imgHP, 0, 24, 9, 6, 0, this.x, this.y - 17, 3);
				}
			}
		}
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x00054E84 File Offset: 0x00053084
	private bool isAuraItem()
	{
		return this.template.type == 22;
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x00054EB4 File Offset: 0x000530B4
	private void setAuraItem()
	{
		this.xO = this.x;
		this.yO = this.y;
		this.iDot = 120;
		this.angle = 0;
		bool flag = !GameCanvas.lowGraphic;
		if (flag)
		{
			this.iAngle = 360 / this.iDot;
			this.xArg = new int[this.iDot];
			this.yArg = new int[this.iDot];
			this.xDot = new int[this.iDot];
			this.yDot = new int[this.iDot];
			this.setDotPosition();
		}
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x00054F54 File Offset: 0x00053154
	private void updateAuraItemEff()
	{
		this.count++;
		this.countAura++;
		bool flag = this.countAura >= 40;
		if (flag)
		{
			this.countAura = 0;
		}
		bool flag2 = this.count >= this.iDot;
		if (flag2)
		{
			this.count = 0;
		}
		bool flag3 = this.count % 10 == 0 && !GameCanvas.lowGraphic;
		if (flag3)
		{
			ServerEffect.addServerEffect(114, this.x - 5, this.y - 30, 1);
		}
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x00054FEC File Offset: 0x000531EC
	public void paintAuraItemEff(mGraphics g)
	{
		bool flag = GameCanvas.lowGraphic || !this.isAuraItem();
		if (!flag)
		{
			for (int i = 0; i < this.yArg.Length; i++)
			{
				bool flag2 = this.count == i;
				if (flag2)
				{
					bool flag3 = this.countAura <= 20;
					if (flag3)
					{
						g.drawImage(ItemMap.imageAuraItem3, this.xDot[i], this.yDot[i] + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else
					{
						SmallImage.drawSmallImage(g, (int)this.template.iconID, this.xDot[i], this.yDot[i] + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
				}
			}
		}
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x000550B8 File Offset: 0x000532B8
	private void setDotPosition()
	{
		bool lowGraphic = GameCanvas.lowGraphic;
		if (!lowGraphic)
		{
			for (int i = 0; i < this.yArg.Length; i++)
			{
				this.yArg[i] = Res.abs(this.rO * Res.sin(this.angle) / 1024);
				this.xArg[i] = Res.abs(this.rO * Res.cos(this.angle) / 1024);
				bool flag = this.angle < 90;
				if (flag)
				{
					this.xDot[i] = this.xO + this.xArg[i];
					this.yDot[i] = this.yO - this.yArg[i];
				}
				else
				{
					bool flag2 = this.angle >= 90 && this.angle < 180;
					if (flag2)
					{
						this.xDot[i] = this.xO - this.xArg[i];
						this.yDot[i] = this.yO - this.yArg[i];
					}
					else
					{
						bool flag3 = this.angle >= 180 && this.angle < 270;
						if (flag3)
						{
							this.xDot[i] = this.xO - this.xArg[i];
							this.yDot[i] = this.yO + this.yArg[i];
						}
						else
						{
							this.xDot[i] = this.xO + this.xArg[i];
							this.yDot[i] = this.yO + this.yArg[i];
						}
					}
				}
				this.angle += this.iAngle;
			}
		}
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x00055270 File Offset: 0x00053470
	public int getX()
	{
		return this.x;
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x00055288 File Offset: 0x00053488
	public int getY()
	{
		return this.y;
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x000552A0 File Offset: 0x000534A0
	public int getH()
	{
		return 20;
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x000552A0 File Offset: 0x000534A0
	public int getW()
	{
		return 20;
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void stopMoving()
	{
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x0001A69C File Offset: 0x0001889C
	public bool isInvisible()
	{
		return false;
	}

	// Token: 0x0400090E RID: 2318
	public int countAutoPick = 0;

	// Token: 0x0400090F RID: 2319
	public int x;

	// Token: 0x04000910 RID: 2320
	public int y;

	// Token: 0x04000911 RID: 2321
	public int xEnd;

	// Token: 0x04000912 RID: 2322
	public int yEnd;

	// Token: 0x04000913 RID: 2323
	public int f;

	// Token: 0x04000914 RID: 2324
	public int vx;

	// Token: 0x04000915 RID: 2325
	public int vy;

	// Token: 0x04000916 RID: 2326
	public int playerId;

	// Token: 0x04000917 RID: 2327
	public int itemMapID;

	// Token: 0x04000918 RID: 2328
	public int IdCharMove;

	// Token: 0x04000919 RID: 2329
	public ItemTemplate template;

	// Token: 0x0400091A RID: 2330
	public sbyte status;

	// Token: 0x0400091B RID: 2331
	public bool isHintFocus;

	// Token: 0x0400091C RID: 2332
	public int rO;

	// Token: 0x0400091D RID: 2333
	public int xO;

	// Token: 0x0400091E RID: 2334
	public int yO;

	// Token: 0x0400091F RID: 2335
	public int angle;

	// Token: 0x04000920 RID: 2336
	public int iAngle;

	// Token: 0x04000921 RID: 2337
	public int iDot;

	// Token: 0x04000922 RID: 2338
	public int[] xArg;

	// Token: 0x04000923 RID: 2339
	public int[] yArg;

	// Token: 0x04000924 RID: 2340
	public int[] xDot;

	// Token: 0x04000925 RID: 2341
	public int[] yDot;

	// Token: 0x04000926 RID: 2342
	public int count;

	// Token: 0x04000927 RID: 2343
	public int countAura;

	// Token: 0x04000928 RID: 2344
	public static Image imageFlare = GameCanvas.loadImage("/mainImage/myTexture2dflare.png");

	// Token: 0x04000929 RID: 2345
	public static Image imageAuraItem1 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura1.png");

	// Token: 0x0400092A RID: 2346
	public static Image imageAuraItem2 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura2.png");

	// Token: 0x0400092B RID: 2347
	public static Image imageAuraItem3 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura3.png");
}
