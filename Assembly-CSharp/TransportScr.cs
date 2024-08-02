﻿using System;

// Token: 0x020000C1 RID: 193
public class TransportScr : mScreen, IActionListener
{
	// Token: 0x060009CB RID: 2507 RVA: 0x0009D640 File Offset: 0x0009B840
	public TransportScr()
	{
		this.posX = new int[this.n];
		this.posY = new int[this.n];
		for (int i = 0; i < this.n; i++)
		{
			this.posX[i] = Res.random(0, GameCanvas.w);
			this.posY[i] = i * (GameCanvas.h / this.n);
		}
		this.posX2 = new int[this.n];
		this.posY2 = new int[this.n];
		for (int j = 0; j < this.n; j++)
		{
			this.posX2[j] = Res.random(0, GameCanvas.w);
			this.posY2[j] = j * (GameCanvas.h / this.n);
		}
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x0009D728 File Offset: 0x0009B928
	public static TransportScr gI()
	{
		bool flag = TransportScr.instance == null;
		if (flag)
		{
			TransportScr.instance = new TransportScr();
		}
		return TransportScr.instance;
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0009D758 File Offset: 0x0009B958
	public override void switchToMe()
	{
		bool flag = TransportScr.ship == null;
		if (flag)
		{
			TransportScr.ship = GameCanvas.loadImage("/mainImage/myTexture2dfutherShip.png");
		}
		bool flag2 = TransportScr.taungam == null;
		if (flag2)
		{
			TransportScr.taungam = GameCanvas.loadImage("/mainImage/taungam.png");
		}
		this.isSpeed = false;
		this.transNow = false;
		bool flag3 = global::Char.myCharz().checkLuong() > 0 && this.type == 0;
		if (flag3)
		{
			this.center = new Command(mResources.faster, this, 1, null);
		}
		else
		{
			this.center = null;
		}
		this.currSpeed = 0;
		base.switchToMe();
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x0009D7FC File Offset: 0x0009B9FC
	public override void paint(mGraphics g)
	{
		g.setColor((this.type != 0) ? 3056895 : 0);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		for (int i = 0; i < this.n; i++)
		{
			g.setColor((this.type != 0) ? 11140863 : 14802654);
			g.fillRect(this.posX[i], this.posY[i], 10, 2);
		}
		bool flag = this.type == 0;
		if (flag)
		{
			g.drawRegion(TransportScr.ship, 0, 0, 72, 95, 7, this.cmx + this.currSpeed, GameCanvas.h / 2, 3);
		}
		bool flag2 = this.type == 1;
		if (flag2)
		{
			g.drawRegion(TransportScr.taungam, 0, 0, 144, 79, 2, this.cmx + this.currSpeed, GameCanvas.h / 2, 3);
		}
		for (int j = 0; j < this.n; j++)
		{
			g.setColor((this.type != 0) ? 7536127 : 14935011);
			g.fillRect(this.posX2[j], this.posY2[j], 18, 3);
		}
		base.paint(g);
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x0009D950 File Offset: 0x0009BB50
	public override void update()
	{
		bool flag = this.type == 0;
		if (flag)
		{
			bool flag2 = !this.isSpeed;
			if (flag2)
			{
				this.currSpeed = GameCanvas.w / 2 * (int)this.time / (int)this.maxTime;
			}
		}
		else
		{
			this.currSpeed += 2;
		}
		Controller.isStopReadMessage = false;
		this.cmx = (((GameCanvas.w / 2 + this.cmx) / 2 + this.cmx) / 2 + this.cmx) / 2;
		bool flag3 = this.type == 1;
		if (flag3)
		{
			this.cmx = 0;
		}
		for (int i = 0; i < this.n; i++)
		{
			this.posX[i] -= this.speed / 2;
			bool flag4 = this.posX[i] < -20;
			if (flag4)
			{
				this.posX[i] = GameCanvas.w;
			}
		}
		for (int j = 0; j < this.n; j++)
		{
			this.posX2[j] -= this.speed;
			bool flag5 = this.posX2[j] < -20;
			if (flag5)
			{
				this.posX2[j] = GameCanvas.w;
			}
		}
		bool flag6 = GameCanvas.gameTick % 3 == 0;
		if (flag6)
		{
			this.speed += ((!this.isSpeed) ? 1 : 2);
		}
		bool flag7 = this.speed > ((!this.isSpeed) ? 25 : 80);
		if (flag7)
		{
			this.speed = ((!this.isSpeed) ? 25 : 80);
		}
		this.curr = mSystem.currentTimeMillis();
		bool flag8 = this.curr - this.last >= 1000L;
		if (flag8)
		{
			this.time += 1;
			this.last = this.curr;
		}
		bool flag9 = this.isSpeed;
		if (flag9)
		{
			this.currSpeed += 3;
		}
		bool flag10 = this.currSpeed >= GameCanvas.w / 2 + 30 && !this.transNow;
		if (flag10)
		{
			this.transNow = true;
			Service.gI().transportNow();
		}
		base.update();
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x000064D2 File Offset: 0x000046D2
	public override void updateKey()
	{
		base.updateKey();
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x0009DB94 File Offset: 0x0009BD94
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 1;
		if (flag)
		{
			GameCanvas.startYesNoDlg(mResources.fasterQuestion, new Command(mResources.YES, this, 2, null), new Command(mResources.NO, this, 3, null));
		}
		bool flag2 = idAction == 2 && global::Char.myCharz().checkLuong() > 0;
		if (flag2)
		{
			this.isSpeed = true;
			GameCanvas.endDlg();
			this.center = null;
		}
		bool flag3 = idAction == 3;
		if (flag3)
		{
			GameCanvas.endDlg();
		}
	}

	// Token: 0x04001268 RID: 4712
	public static TransportScr instance;

	// Token: 0x04001269 RID: 4713
	public static Image ship;

	// Token: 0x0400126A RID: 4714
	public static Image taungam;

	// Token: 0x0400126B RID: 4715
	public sbyte type;

	// Token: 0x0400126C RID: 4716
	public int speed = 5;

	// Token: 0x0400126D RID: 4717
	public int[] posX;

	// Token: 0x0400126E RID: 4718
	public int[] posY;

	// Token: 0x0400126F RID: 4719
	public int[] posX2;

	// Token: 0x04001270 RID: 4720
	public int[] posY2;

	// Token: 0x04001271 RID: 4721
	private int cmx;

	// Token: 0x04001272 RID: 4722
	private int n = 20;

	// Token: 0x04001273 RID: 4723
	public short time;

	// Token: 0x04001274 RID: 4724
	public short maxTime;

	// Token: 0x04001275 RID: 4725
	public long last;

	// Token: 0x04001276 RID: 4726
	public long curr;

	// Token: 0x04001277 RID: 4727
	private bool isSpeed;

	// Token: 0x04001278 RID: 4728
	private bool transNow;

	// Token: 0x04001279 RID: 4729
	private int currSpeed;
}
