using System;

// Token: 0x02000037 RID: 55
public class GamePad
{
	// Token: 0x060002A5 RID: 677 RVA: 0x0003F038 File Offset: 0x0003D238
	public GamePad()
	{
		this.R = 28;
		bool flag = GameCanvas.w < 300;
		if (flag)
		{
			this.isSmallGamePad = true;
			this.isMediumGamePad = false;
			this.isLargeGamePad = false;
		}
		bool flag2 = GameCanvas.w >= 300 && GameCanvas.w <= 380;
		if (flag2)
		{
			this.isSmallGamePad = false;
			this.isMediumGamePad = true;
			this.isLargeGamePad = false;
		}
		bool flag3 = GameCanvas.w > 380;
		if (flag3)
		{
			this.isSmallGamePad = false;
			this.isMediumGamePad = false;
			this.isLargeGamePad = true;
		}
		bool flag4 = !this.isLargeGamePad;
		if (flag4)
		{
			this.xZone = 0;
			this.wZone = GameCanvas.hw;
			this.yZone = GameCanvas.hh >> 1;
			this.hZone = GameCanvas.h - 80;
		}
		else
		{
			this.xZone = 0;
			this.wZone = GameCanvas.hw / 4 * 3 - 20;
			this.yZone = GameCanvas.hh >> 1;
			this.hZone = GameCanvas.h;
		}
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0003F150 File Offset: 0x0003D350
	public void update()
	{
		try
		{
			bool flag = GameScr.isAnalog == 0;
			if (!flag)
			{
				bool flag2 = GameCanvas.isPointerDown && !GameCanvas.isPointerJustRelease;
				if (flag2)
				{
					this.xTemp = GameCanvas.pxFirst;
					this.yTemp = GameCanvas.pyFirst;
					bool flag3 = this.xTemp < this.xZone || this.xTemp > this.wZone || this.yTemp < this.yZone || this.yTemp > this.hZone;
					if (!flag3)
					{
						bool flag4 = !this.isGamePad;
						if (flag4)
						{
							this.xC = (this.xM = this.xTemp);
							this.yC = (this.yM = this.yTemp);
						}
						this.isGamePad = true;
						this.deltaX = GameCanvas.px - this.xC;
						this.deltaY = GameCanvas.py - this.yC;
						this.delta = global::Math.pow(this.deltaX, 2) + global::Math.pow(this.deltaY, 2);
						this.d = Res.sqrt(this.delta);
						bool flag5 = global::Math.abs(this.deltaX) <= 4 && global::Math.abs(this.deltaY) <= 4;
						if (!flag5)
						{
							this.angle = Res.angle(this.deltaX, this.deltaY);
							bool flag6 = !GameCanvas.isPointerHoldIn(this.xC - this.R, this.yC - this.R, 2 * this.R, 2 * this.R);
							if (flag6)
							{
								bool flag7 = this.d != 0;
								if (flag7)
								{
									this.yM = this.deltaY * this.R / this.d;
									this.xM = this.deltaX * this.R / this.d;
									this.xM += this.xC;
									this.yM += this.yC;
									bool flag8 = !Res.inRect(this.xC - this.R, this.yC - this.R, 2 * this.R, 2 * this.R, this.xM, this.yM);
									if (flag8)
									{
										this.xM = this.xMLast;
										this.yM = this.yMLast;
									}
									else
									{
										this.xMLast = this.xM;
										this.yMLast = this.yM;
									}
								}
								else
								{
									this.xM = this.xMLast;
									this.yM = this.yMLast;
								}
							}
							else
							{
								this.xM = GameCanvas.px;
								this.yM = GameCanvas.py;
							}
							this.resetHold();
							bool flag9 = this.checkPointerMove(2);
							if (flag9)
							{
								bool flag10 = (this.angle <= 360 && this.angle >= 340) || (this.angle >= 0 && this.angle <= 20);
								if (flag10)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
								}
								else
								{
									bool flag11 = this.angle > 40 && this.angle < 70;
									if (flag11)
									{
										GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
										GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
									}
									else
									{
										bool flag12 = this.angle >= 70 && this.angle <= 110;
										if (flag12)
										{
											GameCanvas.keyHold[(!Main.isPC) ? 8 : 22] = true;
											GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = true;
										}
										else
										{
											bool flag13 = this.angle > 110 && this.angle < 120;
											if (flag13)
											{
												GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
												GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
											}
											else
											{
												bool flag14 = this.angle >= 120 && this.angle <= 200;
												if (flag14)
												{
													GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
													GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
												}
												else
												{
													bool flag15 = this.angle > 200 && this.angle < 250;
													if (flag15)
													{
														GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
														GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
														GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
														GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
													}
													else
													{
														bool flag16 = this.angle >= 250 && this.angle <= 290;
														if (flag16)
														{
															GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
															GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
														}
														else
														{
															bool flag17 = this.angle > 290 && this.angle < 340;
															if (flag17)
															{
																GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
																GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
																GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
																GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
															}
														}
													}
												}
											}
										}
									}
								}
							}
							else
							{
								this.resetHold();
							}
						}
					}
				}
				else
				{
					this.xM = (this.xC = 45);
					bool flag18 = !this.isLargeGamePad;
					if (flag18)
					{
						this.yM = (this.yC = GameCanvas.h - 90);
					}
					else
					{
						this.yM = (this.yC = GameCanvas.h - 45);
					}
					this.isGamePad = false;
					this.resetHold();
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0003F7A4 File Offset: 0x0003D9A4
	private bool checkPointerMove(int distance)
	{
		bool flag = GameScr.isAnalog == 0;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = global::Char.myCharz().statusMe == 3;
			if (flag2)
			{
				result = true;
			}
			else
			{
				try
				{
					for (int i = 2; i > 0; i--)
					{
						int i2 = GameCanvas.arrPos[i].x - GameCanvas.arrPos[i - 1].x;
						int i3 = GameCanvas.arrPos[i].y - GameCanvas.arrPos[i - 1].y;
						bool flag3 = Res.abs(i2) > distance && Res.abs(i3) > distance;
						if (flag3)
						{
							return false;
						}
					}
				}
				catch (Exception)
				{
				}
				result = true;
			}
		}
		return result;
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x000043D3 File Offset: 0x000025D3
	private void resetHold()
	{
		GameCanvas.clearKeyHold();
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0003F86C File Offset: 0x0003DA6C
	public void paint(mGraphics g)
	{
		bool flag = GameScr.isAnalog != 0;
		if (flag)
		{
			g.drawImage(GameScr.imgAnalog1, this.xC, this.yC, mGraphics.HCENTER | mGraphics.VCENTER);
			g.drawImage(GameScr.imgAnalog2, this.xM, this.yM, mGraphics.HCENTER | mGraphics.VCENTER);
		}
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0003F8D0 File Offset: 0x0003DAD0
	public bool disableCheckDrag()
	{
		bool flag = GameScr.isAnalog == 0;
		return !flag && this.isGamePad;
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0003F8FC File Offset: 0x0003DAFC
	public bool disableClickMove()
	{
		bool result;
		try
		{
			bool flag = GameScr.isAnalog == 0;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = (GameCanvas.px >= this.xZone && GameCanvas.px <= this.wZone && GameCanvas.py >= this.yZone && GameCanvas.py <= this.hZone) || GameCanvas.px >= GameCanvas.w - 50;
				if (flag2)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
		}
		catch (Exception)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x04000615 RID: 1557
	private int xC;

	// Token: 0x04000616 RID: 1558
	private int yC;

	// Token: 0x04000617 RID: 1559
	private int xM;

	// Token: 0x04000618 RID: 1560
	private int yM;

	// Token: 0x04000619 RID: 1561
	private int xMLast;

	// Token: 0x0400061A RID: 1562
	private int yMLast;

	// Token: 0x0400061B RID: 1563
	private int R;

	// Token: 0x0400061C RID: 1564
	private int r;

	// Token: 0x0400061D RID: 1565
	private int d;

	// Token: 0x0400061E RID: 1566
	private int xTemp;

	// Token: 0x0400061F RID: 1567
	private int yTemp;

	// Token: 0x04000620 RID: 1568
	private int deltaX;

	// Token: 0x04000621 RID: 1569
	private int deltaY;

	// Token: 0x04000622 RID: 1570
	private int delta;

	// Token: 0x04000623 RID: 1571
	private int angle;

	// Token: 0x04000624 RID: 1572
	public int xZone;

	// Token: 0x04000625 RID: 1573
	public int yZone;

	// Token: 0x04000626 RID: 1574
	public int wZone;

	// Token: 0x04000627 RID: 1575
	public int hZone;

	// Token: 0x04000628 RID: 1576
	private bool isGamePad;

	// Token: 0x04000629 RID: 1577
	public bool isSmallGamePad;

	// Token: 0x0400062A RID: 1578
	public bool isMediumGamePad;

	// Token: 0x0400062B RID: 1579
	public bool isLargeGamePad;
}
