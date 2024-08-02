using System;

namespace Assets.src.g
{
	// Token: 0x020000D2 RID: 210
	public class BigBoss : Mob, IMapObject
	{
		// Token: 0x06000A96 RID: 2710 RVA: 0x000A5F30 File Offset: 0x000A4130
		public BigBoss(int id, short px, short py, int templateID, int hp, int maxhp, int s)
		{
			this.xFirst = (this.x = (int)(px + 20));
			this.y = (int)py;
			this.yFirst = (int)py;
			this.mobId = id;
			this.hp = hp;
			this.maxHp = maxhp;
			this.templateId = templateID;
			bool flag = s == 0;
			if (flag)
			{
				this.getDataB();
			}
			bool flag2 = s == 1;
			if (flag2)
			{
				this.getDataB2();
			}
			bool flag3 = s == 2;
			if (flag3)
			{
				this.getDataB2();
				this.haftBody = true;
			}
			this.status = 2;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000A60F4 File Offset: 0x000A42F4
		public void getDataB2()
		{
			BigBoss.data = null;
			BigBoss.data = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				100,
				"/data"
			});
			try
			{
				BigBoss.data.readData2(patch);
				BigBoss.data.img = GameCanvas.loadImage("/effectdata/" + 100 + "/img.png");
			}
			catch (Exception)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.status = 2;
			this.w = BigBoss.data.width;
			this.h = BigBoss.data.height;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000A61D0 File Offset: 0x000A43D0
		public void getDataB()
		{
			BigBoss.data = null;
			BigBoss.data = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				101,
				"/data"
			});
			try
			{
				BigBoss.data.readData2(patch);
				BigBoss.data.img = GameCanvas.loadImage("/effectdata/" + 101 + "/img.png");
				Res.outz("read xong data");
			}
			catch (Exception)
			{
				Service.gI().requestModTemplate(this.templateId);
			}
			this.w = BigBoss.data.width;
			this.h = BigBoss.data.height;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000039F2 File Offset: 0x00001BF2
		public new void setBody(short id)
		{
			this.changBody = true;
			this.smallBody = id;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00003A03 File Offset: 0x00001C03
		public new void clearBody()
		{
			this.changBody = false;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00007868 File Offset: 0x00005A68
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

		// Token: 0x06000A9C RID: 2716 RVA: 0x000A62B0 File Offset: 0x000A44B0
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

		// Token: 0x06000A9D RID: 2717 RVA: 0x000A62F4 File Offset: 0x000A44F4
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

		// Token: 0x06000A9E RID: 2718 RVA: 0x000A6440 File Offset: 0x000A4640
		private void paintShadow(mGraphics g)
		{
			g.drawImage(BigBoss.shadowBig, this.xSd, this.yFirst, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00003A0D File Offset: 0x00001C0D
		public new void updateSuperEff()
		{
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000A6490 File Offset: 0x000A4690
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

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000A6590 File Offset: 0x000A4790
		private void updateDead()
		{
			this.checkFrameTick((!this.haftBody) ? this.stand : this.stand_1);
			bool flag = GameCanvas.gameTick % 5 == 0;
			if (flag)
			{
				ServerEffect.addServerEffect(167, Res.random(this.x - this.getW() / 2, this.x + this.getW() / 2), Res.random(this.getY() + this.getH() / 2, this.getY() + this.getH()), 1);
			}
			bool flag2 = this.x != this.xFirst || this.y != this.yFirst;
			if (flag2)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x000A667C File Offset: 0x000A487C
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

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00003A0D File Offset: 0x00001C0D
		public new void setInjure()
		{
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000A6778 File Offset: 0x000A4978
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

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00007D50 File Offset: 0x00005F50
		private bool isSpecial()
		{
			return (this.templateId >= 58 && this.templateId <= 65) || this.templateId == 67 || this.templateId == 68;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00003A0D File Offset: 0x00001C0D
		private void updateInjure()
		{
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000A6858 File Offset: 0x000A4A58
		private void updateMobStandWait()
		{
			this.checkFrameTick((!this.haftBody) ? this.stand : this.stand_1);
			bool flag = this.x != this.xFirst || this.y != this.yFirst;
			if (flag)
			{
				this.x += (this.xFirst - this.x) / 4;
				this.y += (this.yFirst - this.y) / 4;
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0000686D File Offset: 0x00004A6D
		public void setFly()
		{
			this.status = 4;
			this.flyUp = true;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000A68E4 File Offset: 0x000A4AE4
		public void setAttack(global::Char[] cAttack, int[] dame, sbyte type)
		{
			this.charAttack = cAttack;
			this.dameHP = dame;
			this.type = type;
			this.tick = 0;
			bool flag = type < 3;
			if (flag)
			{
				this.status = 3;
			}
			bool flag2 = type == 3;
			if (flag2)
			{
				this.flyUp = true;
				this.status = 4;
			}
			bool flag3 = type == 4;
			if (flag3)
			{
				for (int i = 0; i < this.charAttack.Length; i++)
				{
					this.charAttack[i].doInjure(this.dameHP[i], 0, false, false);
				}
			}
			bool flag4 = type == 7;
			if (flag4)
			{
				this.status = 3;
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000A6988 File Offset: 0x000A4B88
		public new void updateMobAttack()
		{
			bool flag = this.type == 7;
			if (flag)
			{
				bool flag2 = this.tick > 8;
				if (flag2)
				{
					this.tick = 8;
				}
				this.checkFrameTick(this.attack1);
				bool flag3 = GameCanvas.gameTick % 4 == 0;
				if (flag3)
				{
					ServerEffect.addServerEffect(70, this.x + ((this.dir != 1) ? -15 : 15), this.y - 40, 1);
				}
			}
			bool flag4 = this.type == 0;
			if (flag4)
			{
				bool flag5 = this.tick == this.attack1.Length - 1;
				if (flag5)
				{
					this.status = 2;
				}
				this.dir = ((this.x < this.charAttack[0].cx) ? 1 : -1);
				this.checkFrameTick(this.attack1);
				bool flag6 = this.tick == 8;
				if (flag6)
				{
					for (int i = 0; i < this.charAttack.Length; i++)
					{
						MonsterDart.addMonsterDart(this.x + ((this.dir != 1) ? -45 : 45), this.y - 30, true, this.dameHP[i], 0, this.charAttack[i], 24);
					}
				}
			}
			bool flag7 = this.type == 1;
			if (flag7)
			{
				bool flag8 = this.tick == ((!this.haftBody) ? (this.attack2.Length - 1) : (this.attack2_1.Length - 1));
				if (flag8)
				{
					this.status = 2;
				}
				this.dir = ((this.x < this.charAttack[0].cx) ? 1 : -1);
				this.checkFrameTick((!this.haftBody) ? this.attack2 : this.attack2_1);
				this.x += (this.charAttack[0].cx - this.x) / 4;
				this.y += (this.charAttack[0].cy - this.y) / 4;
				bool flag9 = this.tick == 18;
				if (flag9)
				{
					for (int j = 0; j < this.charAttack.Length; j++)
					{
						this.charAttack[j].doInjure(this.dameHP[j], 0, false, false);
						ServerEffect.addServerEffect(102, this.charAttack[j].cx, this.charAttack[j].cy, 1);
					}
				}
			}
			bool flag10 = this.type == 8;
			if (flag10)
			{
			}
			bool flag11 = this.type != 2;
			if (!flag11)
			{
				bool flag12 = this.tick == ((!this.haftBody) ? (this.attack3.Length - 1) : (this.attack3_1.Length - 1));
				if (flag12)
				{
					this.status = 2;
				}
				this.dir = ((this.x < this.charAttack[0].cx) ? 1 : -1);
				this.checkFrameTick((!this.haftBody) ? this.attack3 : this.attack3_1);
				bool flag13 = this.tick == 13;
				if (flag13)
				{
					GameScr.shock_scr = 10;
					this.shock = true;
					for (int k = 0; k < this.charAttack.Length; k++)
					{
						this.charAttack[k].doInjure(this.dameHP[k], 0, false, false);
					}
				}
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00003A0D File Offset: 0x00001C0D
		public new void updateMobWalk()
		{
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0000809C File Offset: 0x0000629C
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

		// Token: 0x06000AAD RID: 2733 RVA: 0x0000812C File Offset: 0x0000632C
		public new bool isUpdate()
		{
			bool flag = this.status == 0;
			return !flag;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00008154 File Offset: 0x00006354
		public new bool checkIsBoss()
		{
			return this.isBoss || this.levelBoss > 0;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000A6CFC File Offset: 0x000A4EFC
		public override void paint(mGraphics g)
		{
			bool flag = BigBoss.data == null;
			if (!flag)
			{
				bool flag2 = this.isShadown && this.status != 0;
				if (flag2)
				{
					this.paintShadow(g);
				}
				g.translate(0, GameCanvas.transY);
				BigBoss.data.paintFrame(g, this.frame, this.x, this.y + this.fy, (this.dir != 1) ? 1 : 0, 2);
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
					Res.outz("type= " + this.type);
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

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00008360 File Offset: 0x00006560
		public new int getHPColor()
		{
			return 16711680;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0000687E File Offset: 0x00004A7E
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

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000A6EF4 File Offset: 0x000A50F4
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

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00008458 File Offset: 0x00006658
		public new int getX()
		{
			return this.x;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000A6FD4 File Offset: 0x000A51D4
		public new int getY()
		{
			return (!this.haftBody) ? (this.y - 60) : (this.y - 20);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0000848C File Offset: 0x0000668C
		public new int getH()
		{
			return 40;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000A7004 File Offset: 0x000A5204
		public new int getW()
		{
			return 60;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000A7018 File Offset: 0x000A5218
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

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000084E8 File Offset: 0x000066E8
		public new bool isInvisible()
		{
			return this.status == 0 || this.status == 1;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00008510 File Offset: 0x00006710
		public new void removeHoldEff()
		{
			bool flag = this.holdEffID != 0;
			if (flag)
			{
				this.holdEffID = 0;
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x000068B9 File Offset: 0x00004AB9
		public new void removeBlindEff()
		{
			this.blindEff = false;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000068C3 File Offset: 0x00004AC3
		public new void removeSleepEff()
		{
			this.sleepEff = false;
		}

		// Token: 0x0400136C RID: 4972
		public static Image shadowBig = GameCanvas.loadImage("/mainImage/shadowBig.png");

		// Token: 0x0400136D RID: 4973
		public static EffectData data;

		// Token: 0x0400136E RID: 4974
		public int xTo;

		// Token: 0x0400136F RID: 4975
		public int yTo;

		// Token: 0x04001370 RID: 4976
		public bool haftBody;

		// Token: 0x04001371 RID: 4977
		public bool change;

		// Token: 0x04001372 RID: 4978
		public new int xSd;

		// Token: 0x04001373 RID: 4979
		public new int ySd;

		// Token: 0x04001374 RID: 4980
		private bool isOutMap;

		// Token: 0x04001375 RID: 4981
		private int wCount;

		// Token: 0x04001376 RID: 4982
		public new bool isShadown = true;

		// Token: 0x04001377 RID: 4983
		private int tick;

		// Token: 0x04001378 RID: 4984
		private int frame;

		// Token: 0x04001379 RID: 4985
		private bool wy;

		// Token: 0x0400137A RID: 4986
		private int wt;

		// Token: 0x0400137B RID: 4987
		private int fy;

		// Token: 0x0400137C RID: 4988
		private int ty;

		// Token: 0x0400137D RID: 4989
		public new int typeSuperEff;

		// Token: 0x0400137E RID: 4990
		private global::Char focus;

		// Token: 0x0400137F RID: 4991
		private bool flyUp;

		// Token: 0x04001380 RID: 4992
		private bool flyDown;

		// Token: 0x04001381 RID: 4993
		private int dy;

		// Token: 0x04001382 RID: 4994
		public bool changePos;

		// Token: 0x04001383 RID: 4995
		private int tShock;

		// Token: 0x04001384 RID: 4996
		public new bool isBusyAttackSomeOne = true;

		// Token: 0x04001385 RID: 4997
		private int tA;

		// Token: 0x04001386 RID: 4998
		private global::Char[] charAttack;

		// Token: 0x04001387 RID: 4999
		private int[] dameHP;

		// Token: 0x04001388 RID: 5000
		private sbyte type;

		// Token: 0x04001389 RID: 5001
		public new int[] stand = new int[]
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

		// Token: 0x0400138A RID: 5002
		public int[] stand_1 = new int[]
		{
			37,
			37,
			37,
			38,
			38,
			38,
			39,
			39,
			40,
			40,
			40,
			39,
			39,
			39,
			38,
			38,
			38
		};

		// Token: 0x0400138B RID: 5003
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

		// Token: 0x0400138C RID: 5004
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

		// Token: 0x0400138D RID: 5005
		public new int[] attack1 = new int[]
		{
			0,
			0,
			34,
			34,
			35,
			35,
			36,
			36,
			2,
			2,
			1,
			1
		};

		// Token: 0x0400138E RID: 5006
		public new int[] attack2 = new int[]
		{
			0,
			0,
			0,
			4,
			4,
			6,
			6,
			9,
			9,
			10,
			10,
			13,
			13,
			15,
			15,
			17,
			17,
			19,
			19,
			21,
			21,
			23,
			23
		};

		// Token: 0x0400138F RID: 5007
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

		// Token: 0x04001390 RID: 5008
		public int[] attack2_1 = new int[]
		{
			37,
			37,
			5,
			5,
			7,
			7,
			11,
			11,
			14,
			14,
			16,
			16,
			18,
			18,
			20,
			20,
			22,
			22,
			24,
			24
		};

		// Token: 0x04001391 RID: 5009
		public int[] attack3_1 = new int[]
		{
			37,
			37,
			37,
			38,
			38,
			5,
			5,
			7,
			7,
			11,
			11,
			27,
			27,
			29,
			29,
			31,
			31,
			33,
			33,
			38,
			38
		};

		// Token: 0x04001392 RID: 5010
		public int[] fly = new int[]
		{
			8,
			8,
			9,
			9,
			10,
			10,
			12,
			12
		};

		// Token: 0x04001393 RID: 5011
		public int[] hitground = new int[]
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

		// Token: 0x04001394 RID: 5012
		private bool shock;

		// Token: 0x04001395 RID: 5013
		private sbyte[] cou = new sbyte[]
		{
			-1,
			1
		};

		// Token: 0x04001396 RID: 5014
		public new global::Char injureBy;

		// Token: 0x04001397 RID: 5015
		public new bool injureThenDie;

		// Token: 0x04001398 RID: 5016
		public new Mob mobToAttack;

		// Token: 0x04001399 RID: 5017
		public new int forceWait;

		// Token: 0x0400139A RID: 5018
		public new bool blindEff;

		// Token: 0x0400139B RID: 5019
		public new bool sleepEff;
	}
}
