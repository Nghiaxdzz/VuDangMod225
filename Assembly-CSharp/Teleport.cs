using System;
using AssemblyCSharp.Mod.Xmap;

// Token: 0x020000B9 RID: 185
public class Teleport
{
	// Token: 0x06000970 RID: 2416 RVA: 0x00099234 File Offset: 0x00097434
	public Teleport(int x, int y, int headId, int dir, int type, bool isMe, int planet)
	{
		this.x = x;
		this.y = 5;
		this.y2 = y;
		this.headId = headId;
		this.type = type;
		this.isMe = isMe;
		this.dir = dir;
		this.planet = planet;
		this.tPrepare = 0;
		int i = 0;
		while (i < 100)
		{
			i++;
			this.y2 += 12;
			bool flag = TileMap.tileTypeAt(x, this.y2, 2);
			if (flag)
			{
				bool flag2 = this.y2 % 24 != 0;
				if (flag2)
				{
					this.y2 -= this.y2 % 24;
				}
				break;
			}
		}
		this.isDown = true;
		bool flag3 = this.planet > 2;
		if (flag3)
		{
			this.y2 += 4;
			bool flag4 = Teleport.maybay[3] == null;
			if (flag4)
			{
				Teleport.maybay[3] = GameCanvas.loadImage("/mainImage/myTexture2dmaybay4a.png");
			}
			bool flag5 = Teleport.maybay[4] == null;
			if (flag5)
			{
				Teleport.maybay[4] = GameCanvas.loadImage("/mainImage/myTexture2dmaybay4b.png");
			}
			bool flag6 = Teleport.hole == null;
			if (flag6)
			{
				Teleport.hole = GameCanvas.loadImage("/mainImage/hole.png");
			}
		}
		else
		{
			bool flag7 = Teleport.maybay[planet] == null;
			if (flag7)
			{
				Teleport.maybay[planet] = GameCanvas.loadImage("/mainImage/myTexture2dmaybay" + (planet + 1) + ".png");
			}
		}
		bool flag8 = x > GameScr.cmx && x < GameScr.cmx + GameCanvas.w && this.y2 > 100 && !SoundMn.gI().isPlayAirShip() && !SoundMn.gI().isPlayRain();
		if (flag8)
		{
			this.createShip = true;
			SoundMn.gI().airShip();
		}
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x000063C9 File Offset: 0x000045C9
	public static void addTeleport(Teleport p)
	{
		Teleport.vTeleport.addElement(p);
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x0009940C File Offset: 0x0009760C
	public void paintHole(mGraphics g)
	{
		bool flag = this.planet > 2 && this.tHole;
		if (flag)
		{
			g.drawImage(Teleport.hole, this.x, this.y2 + 20, StaticObj.BOTTOM_HCENTER);
		}
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x00099454 File Offset: 0x00097654
	public void paint(mGraphics g)
	{
		bool flag = global::Char.isLoadingMap || this.x < GameScr.cmx || this.x > GameScr.cmx + GameCanvas.w;
		if (!flag)
		{
			Part part = GameScr.parts[this.headId];
			int num = 0;
			int num2 = 0;
			bool flag2 = this.planet == 0;
			if (flag2)
			{
				num = 15;
				num2 = 40;
			}
			bool flag3 = this.planet == 1;
			if (flag3)
			{
				num = 7;
				num2 = 55;
			}
			bool flag4 = this.planet == 2;
			if (flag4)
			{
				num = 18;
				num2 = 52;
			}
			bool flag5 = this.painHead && this.planet < 3;
			if (flag5)
			{
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, this.x + ((this.dir != 1) ? (-num) : num), this.y - num2, (this.dir != 1) ? 2 : 0, StaticObj.TOP_CENTER);
			}
			bool flag6 = this.planet < 3;
			if (flag6)
			{
				g.drawRegion(Teleport.maybay[this.planet], 0, 0, mGraphics.getImageWidth(Teleport.maybay[this.planet]), mGraphics.getImageHeight(Teleport.maybay[this.planet]), (this.dir == 1) ? 2 : 0, this.x, this.y, StaticObj.BOTTOM_HCENTER);
			}
			else
			{
				bool flag7 = this.isDown;
				if (flag7)
				{
					bool flag8 = this.tPrepare > 10;
					if (flag8)
					{
						g.drawRegion(Teleport.maybay[4], 0, 0, mGraphics.getImageWidth(Teleport.maybay[4]), mGraphics.getImageHeight(Teleport.maybay[4]), (this.dir == 1) ? 2 : 0, (this.dir != 1) ? (this.x + 11) : (this.x - 11), this.y + 2, StaticObj.BOTTOM_HCENTER);
					}
					else
					{
						g.drawRegion(Teleport.maybay[3], 0, 0, mGraphics.getImageWidth(Teleport.maybay[3]), mGraphics.getImageHeight(Teleport.maybay[3]), (this.dir == 1) ? 2 : 0, this.x, this.y, StaticObj.BOTTOM_HCENTER);
					}
				}
				else
				{
					bool flag9 = this.tPrepare < 20;
					if (flag9)
					{
						g.drawRegion(Teleport.maybay[4], 0, 0, mGraphics.getImageWidth(Teleport.maybay[4]), mGraphics.getImageHeight(Teleport.maybay[4]), (this.dir == 1) ? 2 : 0, (this.dir != 1) ? (this.x + 11) : (this.x - 11), this.y + 2, StaticObj.BOTTOM_HCENTER);
					}
					else
					{
						g.drawRegion(Teleport.maybay[3], 0, 0, mGraphics.getImageWidth(Teleport.maybay[3]), mGraphics.getImageHeight(Teleport.maybay[3]), (this.dir == 1) ? 2 : 0, this.x, this.y, StaticObj.BOTTOM_HCENTER);
					}
				}
			}
		}
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x00099758 File Offset: 0x00097958
	public void update()
	{
		bool flag = !VuDang.xoaTauBay;
		if (flag)
		{
			bool flag2 = Pk9rXmap.XoaTauBay(this);
			if (flag2)
			{
				return;
			}
		}
		bool flag3 = this.planet > 2 && this.paintFire && this.y != -80;
		if (flag3)
		{
			bool flag4 = this.isDown && this.tPrepare == 0;
			if (flag4)
			{
				bool flag5 = GameCanvas.gameTick % 3 == 0;
				if (flag5)
				{
					ServerEffect.addServerEffect(1, this.x, this.y, 1, 0);
				}
			}
			else
			{
				bool flag6 = this.isUp && GameCanvas.gameTick % 3 == 0;
				if (flag6)
				{
					ServerEffect.addServerEffect(1, this.x, this.y + 16, 1, 1);
				}
			}
		}
		this.tFire++;
		bool flag7 = this.tFire > 3;
		if (flag7)
		{
			this.tFire = 0;
		}
		bool flag8 = this.isDown;
		if (flag8)
		{
			this.paintFire = true;
			this.painHead = (this.type != 0);
			bool flag9 = this.planet < 3;
			if (flag9)
			{
				int num = this.y2 - this.y >> 3;
				bool flag10 = num < 1;
				if (flag10)
				{
					num = 1;
					this.paintFire = false;
				}
				this.y += num;
			}
			else
			{
				bool flag11 = GameCanvas.gameTick % 2 == 0;
				if (flag11)
				{
					this.vy++;
				}
				bool flag12 = this.y2 - this.y < this.vy;
				if (flag12)
				{
					this.y = this.y2;
					this.paintFire = false;
				}
				else
				{
					this.y += this.vy;
				}
			}
			bool flag13 = this.isMe && this.type == 1 && global::Char.myCharz().isTeleport;
			if (flag13)
			{
				global::Char.myCharz().cx = this.x;
				global::Char.myCharz().cy = this.y - 30;
				global::Char.myCharz().statusMe = 4;
				GameScr.cmtoX = this.x - GameScr.gW2;
				GameScr.cmtoY = this.y - GameScr.gH23;
				GameScr.info1.isUpdate = false;
			}
			bool flag14 = GameScr.findCharInMap(this.id) != null && !this.isMe && this.type == 1 && GameScr.findCharInMap(this.id).isTeleport;
			if (flag14)
			{
				GameScr.findCharInMap(this.id).cx = this.x;
				GameScr.findCharInMap(this.id).cy = this.y - 30;
				GameScr.findCharInMap(this.id).statusMe = 4;
			}
			bool flag15 = Res.abs(this.y - this.y2) < 50 && TileMap.tileTypeAt(this.x, this.y, 2);
			if (flag15)
			{
				this.tHole = true;
				bool flag16 = this.planet < 3;
				if (flag16)
				{
					SoundMn.gI().pauseAirShip();
					bool flag17 = this.y % 24 != 0;
					if (flag17)
					{
						this.y -= this.y % 24;
					}
					this.tPrepare++;
					bool flag18 = this.tPrepare > 10;
					if (flag18)
					{
						this.tPrepare = 0;
						this.isDown = false;
						this.isUp = true;
						this.paintFire = false;
					}
					bool flag19 = this.type == 1;
					if (flag19)
					{
						bool flag20 = this.isMe;
						if (flag20)
						{
							global::Char.myCharz().isTeleport = false;
						}
						else
						{
							bool flag21 = GameScr.findCharInMap(this.id) != null;
							if (flag21)
							{
								GameScr.findCharInMap(this.id).isTeleport = false;
							}
						}
						this.painHead = false;
					}
				}
				else
				{
					this.y = this.y2;
					bool flag22 = !this.isShock;
					if (flag22)
					{
						ServerEffect.addServerEffect(92, this.x + 4, this.y + 14, 1, 0);
						GameScr.shock_scr = 10;
						this.isShock = true;
					}
					this.tPrepare++;
					bool flag23 = this.tPrepare > 30;
					if (flag23)
					{
						this.tPrepare = 0;
						this.isDown = false;
						this.isUp = true;
						this.paintFire = false;
					}
					bool flag24 = this.type == 1;
					if (flag24)
					{
						bool flag25 = this.isMe;
						if (flag25)
						{
							global::Char.myCharz().isTeleport = false;
						}
						else
						{
							bool flag26 = GameScr.findCharInMap(this.id) != null;
							if (flag26)
							{
								GameScr.findCharInMap(this.id).isTeleport = false;
							}
						}
						this.painHead = false;
					}
				}
			}
		}
		else
		{
			bool flag27 = this.isUp;
			if (flag27)
			{
				this.tPrepare++;
				bool flag28 = this.tPrepare > 30;
				if (flag28)
				{
					int num2 = this.y2 + 24 - this.y >> 3;
					bool flag29 = num2 > 30;
					if (flag29)
					{
						num2 = 30;
					}
					this.y -= num2;
					this.paintFire = true;
				}
				else
				{
					bool flag30 = this.tPrepare == 14 && this.createShip;
					if (flag30)
					{
						SoundMn.gI().resumeAirShip();
					}
					bool flag31 = this.tPrepare > 0 && this.type == 0;
					if (flag31)
					{
						bool flag32 = this.isMe;
						if (flag32)
						{
							global::Char.myCharz().isTeleport = false;
							bool flag33 = global::Char.myCharz().statusMe != 14;
							if (flag33)
							{
								global::Char.myCharz().statusMe = 3;
							}
							global::Char.myCharz().cvy = -3;
						}
						else
						{
							bool flag34 = GameScr.findCharInMap(this.id) != null;
							if (flag34)
							{
								GameScr.findCharInMap(this.id).isTeleport = false;
								bool flag35 = GameScr.findCharInMap(this.id).statusMe != 14;
								if (flag35)
								{
									GameScr.findCharInMap(this.id).statusMe = 3;
								}
								GameScr.findCharInMap(this.id).cvy = -3;
							}
						}
						this.painHead = false;
					}
					bool flag36 = this.tPrepare > 12 && this.type == 0;
					if (flag36)
					{
						bool flag37 = this.isMe;
						if (flag37)
						{
							global::Char.myCharz().isTeleport = true;
						}
						else
						{
							bool flag38 = GameScr.findCharInMap(this.id) != null;
							if (flag38)
							{
								GameScr.findCharInMap(this.id).cx = this.x;
								GameScr.findCharInMap(this.id).cy = this.y;
								GameScr.findCharInMap(this.id).isTeleport = true;
							}
						}
						this.painHead = true;
					}
				}
				bool flag39 = this.isMe;
				if (flag39)
				{
					bool flag40 = this.type == 0;
					if (flag40)
					{
						GameScr.cmtoX = this.x - GameScr.gW2;
						GameScr.cmtoY = this.y - GameScr.gH23;
					}
					bool flag41 = this.type == 1;
					if (flag41)
					{
						GameScr.info1.isUpdate = true;
					}
				}
				bool flag42 = this.y <= -80;
				if (flag42)
				{
					bool flag43 = this.isMe && this.type == 0;
					if (flag43)
					{
						Controller.isStopReadMessage = false;
						global::Char.ischangingMap = true;
					}
					bool flag44 = !this.isMe && GameScr.findCharInMap(this.id) != null && this.type == 0;
					if (flag44)
					{
						GameScr.vCharInMap.removeElement(GameScr.findCharInMap(this.id));
					}
					bool flag45 = this.planet < 3;
					if (flag45)
					{
						Teleport.vTeleport.removeElement(this);
					}
					else
					{
						this.y = -80;
						this.tDelayHole++;
						bool flag46 = this.tDelayHole > 80;
						if (flag46)
						{
							this.tDelayHole = 0;
							Teleport.vTeleport.removeElement(this);
						}
					}
				}
			}
		}
		bool flag47 = this.paintFire && this.planet < 3 && Res.abs(this.y - this.y2) <= 50 && GameCanvas.gameTick % 5 == 0;
		if (flag47)
		{
			Effect me = new Effect(19, this.x, this.y2 + 20, 2, 1, -1);
			EffecMn.addEff(me);
		}
	}

	// Token: 0x04001186 RID: 4486
	public static MyVector vTeleport = new MyVector();

	// Token: 0x04001187 RID: 4487
	public int x;

	// Token: 0x04001188 RID: 4488
	public int y;

	// Token: 0x04001189 RID: 4489
	public int headId;

	// Token: 0x0400118A RID: 4490
	public int type;

	// Token: 0x0400118B RID: 4491
	public bool isMe;

	// Token: 0x0400118C RID: 4492
	public int y2;

	// Token: 0x0400118D RID: 4493
	public int id;

	// Token: 0x0400118E RID: 4494
	public int dir;

	// Token: 0x0400118F RID: 4495
	public int planet;

	// Token: 0x04001190 RID: 4496
	public static Image[] maybay = new Image[5];

	// Token: 0x04001191 RID: 4497
	public static Image hole;

	// Token: 0x04001192 RID: 4498
	public bool isUp;

	// Token: 0x04001193 RID: 4499
	public bool isDown;

	// Token: 0x04001194 RID: 4500
	private bool createShip;

	// Token: 0x04001195 RID: 4501
	public bool paintFire;

	// Token: 0x04001196 RID: 4502
	private bool painHead;

	// Token: 0x04001197 RID: 4503
	private int tPrepare;

	// Token: 0x04001198 RID: 4504
	private int vy = 1;

	// Token: 0x04001199 RID: 4505
	private int tFire;

	// Token: 0x0400119A RID: 4506
	private int tDelayHole;

	// Token: 0x0400119B RID: 4507
	private bool tHole;

	// Token: 0x0400119C RID: 4508
	private bool isShock;
}
