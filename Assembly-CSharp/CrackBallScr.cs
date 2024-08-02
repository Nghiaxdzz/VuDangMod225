using System;

// Token: 0x0200001B RID: 27
public class CrackBallScr : mScreen
{
	// Token: 0x060001A0 RID: 416 RVA: 0x00031EF8 File Offset: 0x000300F8
	public CrackBallScr()
	{
		CrackBallScr.xSkill = new int[2];
		CrackBallScr.xSkill[0] = 16;
		CrackBallScr.ySkill = GameCanvas.h - 41;
		CrackBallScr.xSkill[1] = GameCanvas.w - 40;
		Image img = GameCanvas.loadImage("/e/e_1.png");
		CrackBallScr.fraImgKame = new FrameImage(img, 30, 30);
		Image img2 = GameCanvas.loadImage("/e/e_0.png");
		CrackBallScr.fraImgKame_1 = new FrameImage(img2, 68, 65);
		Image img3 = GameCanvas.loadImage("/e/e_2.png");
		CrackBallScr.fraImgKame_2 = new FrameImage(img3, 66, 70);
		CrackBallScr.imgReplay = GameCanvas.loadImage("/e/nut2.png");
		CrackBallScr.imgX = GameCanvas.loadImage("/e/nut3.png");
		this.wP = 230;
		this.xP = GameCanvas.hw - this.wP / 2;
		this.hP = 40;
		this.yP = -this.hP;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x00032010 File Offset: 0x00030210
	public static CrackBallScr gI()
	{
		bool flag = CrackBallScr.instance == null;
		if (flag)
		{
			CrackBallScr.instance = new CrackBallScr();
		}
		return CrackBallScr.instance;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00032040 File Offset: 0x00030240
	public void SetCrackBallScr(short[] idImage, byte typePrice, int price, short idTicket)
	{
		bool flag = idImage != null && idImage.Length != 0;
		if (flag)
		{
			VuDang.isPaintCrackBall = true;
			this.yTo = global::Char.myCharz().cy - 10;
			this.setAuraItem();
			this.listBall = new BallInfo[idImage.Length];
			for (int i = 0; i < this.listBall.Length; i++)
			{
				this.listBall[i] = new BallInfo();
				this.listBall[i].idImg = (int)idImage[i];
				this.listBall[i].count = i * 25;
				this.listBall[i].yTo = -999;
				this.listBall[i].vx = Res.random(2, 5);
				this.listBall[i].dir = Res.random(-1, 2);
				this.listBall[i].SetChar();
			}
			this.isCanSkill = false;
			this.isKame = false;
			this.isSendSv = false;
			this.timeStart = GameCanvas.timeNow + (long)Res.random(1000, 2000);
			this.step = 0;
			this.indexSelect = -1;
			this.indexSkillSelect = -1;
			this.typePrice = typePrice;
			this.price = price;
			this.cost = 0;
			global::Char.myCharz().moveTo(470, 408, 1);
			global::Char.myCharz().cdir = 2;
			global::Char.myCharz().statusMe = 1;
			this.countFr = 0;
			this.countKame = 0;
			this.frame = 0;
			this.vp = 0;
			this.yP = -this.hP;
			this.idTicket = idTicket;
			this.numTicket = 0;
			this.checkNumTicket();
			this.switchToMe();
			SoundMn.gI().hoisinh();
		}
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x000321FC File Offset: 0x000303FC
	private void setAuraItem()
	{
		this.rO = GameCanvas.hh / 3 + 10;
		bool flag = this.rO > 50;
		if (flag)
		{
			this.rO = 50;
		}
		this.xO = 360;
		GameScr.cmx = GameScr.cmxLim / 2;
		this.yO = GameScr.cmy + GameCanvas.hh / 3 + 30;
		this.iDot = 175;
		this.angle = 0;
		this.iAngle = 360 / this.iDot;
		this.xArg = new int[this.iDot];
		this.yArg = new int[this.iDot];
		this.xDot = new int[this.iDot];
		this.yDot = new int[this.iDot];
		this.setDotPosition();
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x000322D0 File Offset: 0x000304D0
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

	// Token: 0x060001A5 RID: 421 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void perform(int idAction, object p)
	{
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00032488 File Offset: 0x00030688
	public override void update()
	{
		try
		{
			this.cost = this.price * (int)this.checkNum();
			this.checkNumTicket();
			GameScr.gI().update();
			bool flag = this.timeStart - GameCanvas.timeNow > 0L;
			if (flag)
			{
				for (int i = 0; i < this.listBall.Length; i++)
				{
					this.listBall[i].count += 2;
					bool flag2 = this.listBall[i].count >= this.iDot;
					if (flag2)
					{
						this.listBall[i].count = 0;
					}
					this.listBall[i].x = this.xDot[this.listBall[i].count];
					this.listBall[i].y = this.yDot[this.listBall[i].count];
				}
			}
			else
			{
				bool flag3 = this.step == 0;
				if (flag3)
				{
					this.step = 1;
				}
				bool flag4 = this.step == 1;
				if (flag4)
				{
					for (int j = 0; j < this.listBall.Length; j++)
					{
						bool flag5 = this.listBall[j].yTo == -999 || this.listBall[j].isDone;
						if (!flag5)
						{
							bool flag6 = this.listBall[j].y < this.listBall[j].yTo;
							if (flag6)
							{
								bool flag7 = this.listBall[j].vy < 0;
								if (flag7)
								{
									this.listBall[j].vy = 0;
								}
								bool flag8 = this.listBall[j].y + this.listBall[j].vy > this.listBall[j].yTo;
								if (flag8)
								{
									this.listBall[j].y = this.listBall[j].yTo;
								}
								else
								{
									this.listBall[j].y += this.listBall[j].vy;
								}
								this.listBall[j].vy++;
							}
							else
							{
								bool flag9 = this.listBall[j].vy > 0;
								if (flag9)
								{
									this.listBall[j].vy = 0;
								}
								this.listBall[j].y += this.listBall[j].vy;
								this.listBall[j].vy--;
							}
							bool flag10 = this.listBall[j].y == this.listBall[j].yTo;
							if (flag10)
							{
								Effect me = new Effect(19, this.listBall[j].x - 5, this.listBall[j].y + 25, 2, 1, -1);
								EffecMn.addEff(me);
								SoundMn.gI().charFall();
								this.listBall[j].isDone = true;
								bool flag11 = !this.isCanSkill;
								if (flag11)
								{
									this.isCanSkill = true;
								}
							}
						}
					}
				}
				bool flag12 = this.step == 2;
				if (flag12)
				{
					for (int k = 0; k < this.listBall.Length; k++)
					{
						bool isDone = this.listBall[k].isDone;
						if (!isDone)
						{
							bool flag13 = this.listBall[k].y > -10;
							if (flag13)
							{
								bool flag14 = this.listBall[k].vy > 0;
								if (flag14)
								{
									this.listBall[k].vy = 0;
								}
								this.listBall[k].y += this.listBall[k].vy;
								this.listBall[k].vy--;
								this.listBall[k].x += this.listBall[k].vx * this.listBall[k].dir;
								this.listBall[k].vx -= 3;
							}
							bool flag15 = this.listBall[k].y == -10;
							if (flag15)
							{
								this.listBall[k].isPaint = false;
							}
						}
					}
					this.countFr++;
					bool flag16 = this.countFr > this.fr.Length - 1;
					if (flag16)
					{
						this.countFr = this.fr.Length - 1;
						this.isKame = true;
						SoundMn.gI().newKame();
						bool flag17 = !this.isSendSv && this.timeKame - GameCanvas.timeNow < 0L;
						if (flag17)
						{
							Service.gI().SendCrackBall(2, this.checkTicket() + this.checkNum());
							this.isSendSv = true;
						}
					}
					global::Char.myCharz().cf = (int)this.fr[this.countFr];
					this.countKame++;
					bool flag18 = this.countKame > 5;
					if (flag18)
					{
						this.countKame = 0;
					}
					this.frame = (int)this.nFrame[this.countKame];
				}
				bool flag19 = this.step == 3;
				if (flag19)
				{
					bool flag20 = this.countKame <= 5;
					if (flag20)
					{
						this.countKame = 5;
					}
					this.countKame++;
					bool flag21 = this.countKame > this.nFrame.Length - 1;
					if (flag21)
					{
						this.countKame = this.nFrame.Length - 1;
						this.step = 4;
						this.isKame = false;
						int num = 0;
						for (int l = 0; l < this.listBall.Length; l++)
						{
							bool flag22 = this.listBall[l].isDone && !this.listBall[l].isSetImg;
							if (flag22)
							{
								this.listBall[l].idImg = (int)this.idItem[num];
								this.listBall[l].isSetImg = true;
								num++;
							}
						}
					}
					this.frame = (int)this.nFrame[this.countKame];
				}
				bool flag23 = this.step == 4;
				if (flag23)
				{
					for (int m = 0; m < this.listBall.Length; m++)
					{
						bool isPaint = this.listBall[m].isPaint;
						if (isPaint)
						{
							this.listBall[m].xTo = global::Char.myCharz().cx;
						}
					}
					this.step = 5;
				}
				bool flag24 = this.step != 5;
				if (!flag24)
				{
					this.vp++;
					bool flag25 = this.yP < GameCanvas.hh / 3;
					if (flag25)
					{
						bool flag26 = this.yP + this.vp > GameCanvas.hh / 3;
						if (flag26)
						{
							this.yP = GameCanvas.hh / 3;
						}
						else
						{
							this.yP += this.vp;
						}
					}
					for (int n = 0; n < this.listBall.Length; n++)
					{
						bool flag27 = !this.listBall[n].isPaint;
						if (!flag27)
						{
							bool flag28 = this.listBall[n].x < this.listBall[n].xTo;
							if (flag28)
							{
								bool flag29 = this.listBall[n].vx < 0;
								if (flag29)
								{
									this.listBall[n].vx = 0;
								}
								bool flag30 = this.listBall[n].x + this.listBall[n].vx > this.listBall[n].xTo;
								if (flag30)
								{
									this.listBall[n].x = this.listBall[n].xTo;
								}
								else
								{
									this.listBall[n].x += this.listBall[n].vx;
								}
								this.listBall[n].vx++;
							}
							else
							{
								bool flag31 = this.listBall[n].vx > 0;
								if (flag31)
								{
									this.listBall[n].vx = 0;
								}
								this.listBall[n].x += this.listBall[n].vx;
								this.listBall[n].vx--;
							}
							bool flag32 = this.listBall[n].x == this.listBall[n].xTo;
							if (flag32)
							{
								this.listBall[n].isPaint = false;
							}
						}
					}
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00032DB8 File Offset: 0x00030FB8
	public override void updateKey()
	{
		bool isLock = InfoDlg.isLock;
		if (!isLock)
		{
			bool flag = GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu;
			if (flag)
			{
				this.updateKeyTouchControl();
			}
			for (int i = 1; i < 8; i++)
			{
				bool flag2 = GameCanvas.keyPressed[i];
				if (flag2)
				{
					GameCanvas.keyPressed[i] = false;
					this.doClickBall(i - 1);
				}
			}
			bool flag3 = GameCanvas.keyPressed[12];
			if (flag3)
			{
				GameCanvas.keyPressed[12] = false;
				this.doClickSkill(0);
			}
			bool flag4 = GameCanvas.keyPressed[13];
			if (flag4)
			{
				GameCanvas.keyPressed[13] = false;
				this.doClickSkill(1);
			}
			GameCanvas.clearKeyPressed();
		}
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00032E80 File Offset: 0x00031080
	private void updateKeyTouchControl()
	{
		bool flag = this.step == 1 && GameCanvas.isPointerClick;
		if (flag)
		{
			for (int i = 0; i < this.listBall.Length; i++)
			{
				bool flag2 = GameCanvas.isPointerHoldIn(this.listBall[i].x - 20 - GameScr.cmx, this.listBall[i].y - 10 - GameScr.cmy, 30, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
				if (flag2)
				{
					this.doClickBall(i);
				}
			}
		}
		bool flag3 = !GameCanvas.isPointerClick;
		if (!flag3)
		{
			for (int j = 0; j < CrackBallScr.xSkill.Length; j++)
			{
				bool flag4 = GameCanvas.isPointerHoldIn(CrackBallScr.xSkill[j], CrackBallScr.ySkill, 36, 36) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
				if (flag4)
				{
					this.doClickSkill(j);
				}
			}
		}
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x00032F7C File Offset: 0x0003117C
	private void doClickBall(int index)
	{
		bool flag = !this.listBall[index].isDone;
		if (flag)
		{
			SoundMn.gI().getItem();
			long num = (this.typePrice != 0) ? ((long)global::Char.myCharz().checkLuong()) : global::Char.myCharz().xu;
			bool flag2 = (int)this.checkTicket() >= this.numTicket && num < (long)(this.cost + this.price);
			if (flag2)
			{
				string s = mResources.not_enough_money_1 + " " + ((this.typePrice != 0) ? mResources.LUONG : mResources.XU);
				GameScr.info1.addInfo(s, 0);
			}
			else
			{
				this.indexSelect = index;
				this.listBall[this.indexSelect].yTo = this.yTo + Res.random(-3, 3);
			}
		}
	}

	// Token: 0x060001AA RID: 426 RVA: 0x00033054 File Offset: 0x00031254
	private void doClickSkill(int index)
	{
		bool flag = this.indexSkillSelect != index;
		if (flag)
		{
			this.indexSkillSelect = index;
		}
		else
		{
			bool flag2 = index == 0;
			if (flag2)
			{
				bool flag3 = this.step < 2;
				if (flag3)
				{
					bool flag4 = this.checkTicket() + this.checkNum() > 0;
					if (flag4)
					{
						this.step = 2;
						SoundMn.gI().gong();
						global::Char.myCharz().setSkillPaint(GameScr.sks[13], 0);
						this.timeKame = GameCanvas.timeNow + (long)Res.random(2000, 3000);
					}
				}
				else
				{
					bool flag5 = this.yP == GameCanvas.hh / 3;
					if (flag5)
					{
						Service.gI().SendCrackBall(this.typePrice, 0);
					}
				}
			}
			else
			{
				GameScr.gI().isRongThanXuatHien = false;
				GameScr.gI().switchToMe();
			}
		}
	}

	// Token: 0x060001AB RID: 427 RVA: 0x0003313C File Offset: 0x0003133C
	public override void paint(mGraphics g)
	{
		try
		{
			GameScr.gI().paint(g);
			g.translate(-GameScr.cmx, -GameScr.cmy);
			g.translate(0, GameCanvas.transY);
			for (int i = 0; i < this.listBall.Length; i++)
			{
				bool flag = this.listBall[i].isPaint && this.listBall[i].y > this.listBall[i].yTo - 20;
				if (flag)
				{
					g.drawImage(TileMap.bong, this.listBall[i].x, this.listBall[i].yTo + 7, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
			for (int j = 0; j < this.listBall.Length; j++)
			{
				bool isPaint = this.listBall[j].isPaint;
				if (isPaint)
				{
					SmallImage.drawSmallImage(g, this.listBall[j].idImg, this.listBall[j].x, this.listBall[j].y, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
			bool flag2 = this.isKame;
			if (flag2)
			{
				bool flag3 = CrackBallScr.fraImgKame != null;
				if (flag3)
				{
					int num = global::Char.myCharz().cx - CrackBallScr.fraImgKame.frameWidth - 28;
					for (int k = 0; k < GameCanvas.w / CrackBallScr.fraImgKame.frameWidth + 1; k++)
					{
						CrackBallScr.fraImgKame.drawFrame(this.frame, num - k * (CrackBallScr.fraImgKame.frameWidth - 1), global::Char.myCharz().cy - CrackBallScr.fraImgKame.frameHeight / 2 - 12 + 2, 0, 0, g);
					}
				}
				bool flag4 = CrackBallScr.fraImgKame_1 != null;
				if (flag4)
				{
					int num2 = global::Char.myCharz().cx - CrackBallScr.fraImgKame_1.frameWidth - 10;
					CrackBallScr.fraImgKame_1.drawFrame(this.frame, num2 - 5, global::Char.myCharz().cy - CrackBallScr.fraImgKame_1.frameHeight / 2 - 12, 0, 0, g);
				}
			}
			GameScr.resetTranslate(g);
			int num3 = 240;
			int num4 = GameCanvas.w - num3;
			int num5 = 15;
			g.setColor(13524492);
			g.fillRect(num4, num5 - 15, num3, 15);
			g.drawImage(Panel.imgXu, num4 + 11, num5 - 7, 3);
			g.drawImage(Panel.imgLuong, num4 + 90, num5 - 8, 3);
			mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().xuStr + string.Empty, num4 + 24, num5 - 13, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().luongStr + string.Empty, num4 + 100, num5 - 13, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(Panel.imgLuongKhoa, num4 + 150, num5 - 7, 3);
			mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().luongKhoaStr + string.Empty, num4 + 160, num5 - 13, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(Panel.imgTicket, num4 + 200, num5 - 7, 3);
			mFont.tahoma_7_yellow.drawString(g, this.numTicket + string.Empty, num4 + 210, num5 - 13, mFont.LEFT, mFont.tahoma_7_grey);
			bool flag5 = this.step < 4;
			if (flag5)
			{
				int num6 = num3 / 2 + 20;
				int num7 = GameCanvas.w - num6;
				g.setColor(11837316);
				g.fillRect(num7, num5, num6, 15);
				bool flag6 = this.typePrice == 0;
				if (flag6)
				{
					g.drawImage(Panel.imgXu, num7 + 21, num5 + 8, 3);
				}
				else
				{
					g.drawImage(Panel.imgLuongKhoa, num7 + 21, num5 + 7, 3);
					g.drawImage(Panel.imgLuong, num7 + 18, num5 + 7, 3);
				}
				mFont.tahoma_7_red.drawString(g, " -" + this.cost, num7 + 30, num5 + 2, mFont.LEFT, mFont.tahoma_7_grey);
				g.drawImage(Panel.imgTicket, num7 + 80, num5 + 7, 3);
				mFont.tahoma_7_red.drawString(g, " -" + this.checkTicket(), num7 + 90, num5 + 2, mFont.LEFT, mFont.tahoma_7_grey);
			}
			g.drawImage(GameScr.imgSkill, CrackBallScr.xSkill[0], CrackBallScr.ySkill, 0);
			bool flag7 = this.indexSkillSelect == 0;
			if (flag7)
			{
				g.drawImage(GameScr.imgSkill2, CrackBallScr.xSkill[0], CrackBallScr.ySkill, 0);
			}
			bool flag8 = this.step < 3;
			if (flag8)
			{
				SmallImage.drawSmallImage(g, 540, CrackBallScr.xSkill[0] + 14, CrackBallScr.ySkill + 14, 0, StaticObj.VCENTER_HCENTER);
			}
			else
			{
				g.drawImage(CrackBallScr.imgReplay, CrackBallScr.xSkill[0] + 14 - 10, CrackBallScr.ySkill + 14 - 10, 0);
			}
			g.drawImage(GameScr.imgSkill, CrackBallScr.xSkill[1], CrackBallScr.ySkill, 0);
			bool flag9 = this.indexSkillSelect == 1;
			if (flag9)
			{
				g.drawImage(GameScr.imgSkill2, CrackBallScr.xSkill[1], CrackBallScr.ySkill, 0);
			}
			g.drawImage(CrackBallScr.imgX, CrackBallScr.xSkill[1] + 14 - 10, CrackBallScr.ySkill + 14 - 10, 0);
			bool flag10 = this.step > 3;
			if (flag10)
			{
				GameCanvas.paintz.paintFrameSimple(this.xP, this.yP, this.wP, this.hP, g);
				int num8 = GameCanvas.hw - this.idItem.Length * 30 / 2;
				for (int l = 0; l < this.idItem.Length; l++)
				{
					SmallImage.drawSmallImage(g, (int)this.idItem[l], num8 + 5 + l * 30, this.yP + 10, 0, 0);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060001AC RID: 428 RVA: 0x00003EAF File Offset: 0x000020AF
	public void DoneCrackBallScr(short[] idImage)
	{
		this.step = 3;
		this.idItem = idImage;
	}

	// Token: 0x060001AD RID: 429 RVA: 0x00003EC0 File Offset: 0x000020C0
	public override void switchToMe()
	{
		GameScr.isPaintOther = true;
		GameScr.gI().isRongThanXuatHien = true;
		base.switchToMe();
	}

	// Token: 0x060001AE RID: 430 RVA: 0x0003379C File Offset: 0x0003199C
	private byte checkTicket()
	{
		byte b = 0;
		for (int i = 0; i < this.listBall.Length; i++)
		{
			bool isDone = this.listBall[i].isDone;
			if (isDone)
			{
				b += 1;
			}
		}
		bool flag = (int)b > this.numTicket;
		if (flag)
		{
			b = (byte)this.numTicket;
		}
		return b;
	}

	// Token: 0x060001AF RID: 431 RVA: 0x00033800 File Offset: 0x00031A00
	private byte checkNum()
	{
		byte b = 0;
		for (int i = 0; i < this.listBall.Length; i++)
		{
			bool isDone = this.listBall[i].isDone;
			if (isDone)
			{
				b += 1;
			}
		}
		b -= this.checkTicket();
		bool flag = b <= 0;
		if (flag)
		{
			b = 0;
		}
		return b;
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00033864 File Offset: 0x00031A64
	private void checkNumTicket()
	{
		for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
		{
			bool flag = global::Char.myCharz().arrItemBag[i] != null && global::Char.myCharz().arrItemBag[i].template.id == this.idTicket;
			if (flag)
			{
				this.numTicket = global::Char.myCharz().arrItemBag[i].quantity;
				break;
			}
		}
	}

	// Token: 0x0400043A RID: 1082
	public static CrackBallScr instance;

	// Token: 0x0400043B RID: 1083
	private BallInfo[] listBall;

	// Token: 0x0400043C RID: 1084
	private byte step;

	// Token: 0x0400043D RID: 1085
	private byte typePrice;

	// Token: 0x0400043E RID: 1086
	private int rO;

	// Token: 0x0400043F RID: 1087
	private int xO;

	// Token: 0x04000440 RID: 1088
	private int yO;

	// Token: 0x04000441 RID: 1089
	private int angle;

	// Token: 0x04000442 RID: 1090
	private int iAngle;

	// Token: 0x04000443 RID: 1091
	private int iDot;

	// Token: 0x04000444 RID: 1092
	private int yTo;

	// Token: 0x04000445 RID: 1093
	private int indexSelect;

	// Token: 0x04000446 RID: 1094
	private int indexSkillSelect;

	// Token: 0x04000447 RID: 1095
	private int numTicket;

	// Token: 0x04000448 RID: 1096
	private int xP;

	// Token: 0x04000449 RID: 1097
	private int yP;

	// Token: 0x0400044A RID: 1098
	private int wP;

	// Token: 0x0400044B RID: 1099
	private int hP;

	// Token: 0x0400044C RID: 1100
	private int price;

	// Token: 0x0400044D RID: 1101
	private int cost;

	// Token: 0x0400044E RID: 1102
	private int countFr;

	// Token: 0x0400044F RID: 1103
	private int countKame;

	// Token: 0x04000450 RID: 1104
	private int frame;

	// Token: 0x04000451 RID: 1105
	private int vp;

	// Token: 0x04000452 RID: 1106
	private int[] xArg;

	// Token: 0x04000453 RID: 1107
	private int[] yArg;

	// Token: 0x04000454 RID: 1108
	private int[] xDot;

	// Token: 0x04000455 RID: 1109
	private int[] yDot;

	// Token: 0x04000456 RID: 1110
	private short[] idItem;

	// Token: 0x04000457 RID: 1111
	private long timeStart;

	// Token: 0x04000458 RID: 1112
	private long timeKame;

	// Token: 0x04000459 RID: 1113
	private bool isKame;

	// Token: 0x0400045A RID: 1114
	private bool isCanSkill;

	// Token: 0x0400045B RID: 1115
	private bool isSendSv;

	// Token: 0x0400045C RID: 1116
	private short idTicket;

	// Token: 0x0400045D RID: 1117
	private static int ySkill;

	// Token: 0x0400045E RID: 1118
	private static int[] xSkill;

	// Token: 0x0400045F RID: 1119
	private static FrameImage fraImgKame;

	// Token: 0x04000460 RID: 1120
	private static FrameImage fraImgKame_1;

	// Token: 0x04000461 RID: 1121
	private static FrameImage fraImgKame_2;

	// Token: 0x04000462 RID: 1122
	private static Image imgX;

	// Token: 0x04000463 RID: 1123
	private static Image imgReplay;

	// Token: 0x04000464 RID: 1124
	private byte[] fr = new byte[]
	{
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		19,
		20
	};

	// Token: 0x04000465 RID: 1125
	private byte[] nFrame = new byte[]
	{
		0,
		0,
		0,
		1,
		1,
		1,
		2,
		2,
		2,
		3,
		3,
		3
	};
}
