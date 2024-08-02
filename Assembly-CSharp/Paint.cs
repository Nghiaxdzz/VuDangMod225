using System;

// Token: 0x02000082 RID: 130
public class Paint
{
	// Token: 0x0600063A RID: 1594 RVA: 0x000681C4 File Offset: 0x000663C4
	public static void loadbg()
	{
		for (int i = 0; i < Paint.goc.Length; i++)
		{
			Paint.goc[i] = GameCanvas.loadImage("/mainImage/myTexture2dgoc" + (i + 1) + ".png");
		}
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x00068210 File Offset: 0x00066410
	public void paintDefaultBg(mGraphics g)
	{
		g.setColor(8916494);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(Paint.imgBg, GameCanvas.w / 2, GameCanvas.h / 2 - Paint.hTab / 2 - 1, 3);
		g.drawImage(Paint.imgLT, 0, 0, 0);
		g.drawImage(Paint.imgRT, GameCanvas.w, 0, mGraphics.TOP | mGraphics.RIGHT);
		g.drawImage(Paint.imgLB, 0, GameCanvas.h - Paint.hTab - 2, mGraphics.BOTTOM | mGraphics.LEFT);
		g.drawImage(Paint.imgRB, GameCanvas.w, GameCanvas.h - Paint.hTab - 2, mGraphics.BOTTOM | mGraphics.RIGHT);
		g.setColor(16774843);
		g.drawRect(0, 0, GameCanvas.w, 0);
		g.drawRect(0, GameCanvas.h - Paint.hTab - 2, GameCanvas.w, 0);
		g.drawRect(0, 0, 0, GameCanvas.h - Paint.hTab);
		g.drawRect(GameCanvas.w - 1, 0, 0, GameCanvas.h - Paint.hTab);
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x000054A2 File Offset: 0x000036A2
	public void paintfillDefaultBg(mGraphics g)
	{
		g.setColor(205314);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void repaintCircleBg()
	{
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void paintSolidBg(mGraphics g)
	{
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x000054C4 File Offset: 0x000036C4
	public void paintDefaultPopup(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(8411138);
		g.fillRect(x, y, w, h);
		g.setColor(13606712);
		g.drawRect(x, y, w, h);
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x000054F9 File Offset: 0x000036F9
	public void paintWhitePopup(mGraphics g, int y, int x, int width, int height)
	{
		g.setColor(16776363);
		g.fillRect(x, y, width, height);
		g.setColor(0);
		g.drawRect(x - 1, y - 1, width + 1, height + 1);
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x00068344 File Offset: 0x00066544
	public void paintDefaultPopupH(mGraphics g, int h)
	{
		g.setColor(14279153);
		g.fillRect(8, GameCanvas.h - (h + 37), GameCanvas.w - 16, h + 4);
		g.setColor(4682453);
		g.fillRect(10, GameCanvas.h - (h + 35), GameCanvas.w - 20, h);
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x000683A4 File Offset: 0x000665A4
	public void paintCmdBar(mGraphics g, Command left, Command center, Command right)
	{
		mFont mFont = (!GameCanvas.isTouch) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_dark;
		int num = 3;
		bool flag = left != null;
		if (flag)
		{
			Paint.lenCaption = mFont.getWidth(left.caption);
			bool flag2 = Paint.lenCaption > 0;
			if (flag2)
			{
				bool flag3 = left.x >= 0 && left.y > 0;
				if (flag3)
				{
					left.paint(g);
				}
				else
				{
					g.drawImage((mScreen.keyTouch != 0) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, 1, GameCanvas.h - mScreen.cmdH - 1, 0);
					mFont.drawString(g, left.caption, 35, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
				}
			}
		}
		bool flag4 = center != null;
		if (flag4)
		{
			Paint.lenCaption = mFont.getWidth(center.caption);
			bool flag5 = Paint.lenCaption > 0;
			if (flag5)
			{
				bool flag6 = center.x > 0 && center.y > 0;
				if (flag6)
				{
					center.paint(g);
				}
				else
				{
					g.drawImage((mScreen.keyTouch != 1) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, GameCanvas.hw - 35, GameCanvas.h - mScreen.cmdH - 1, 0);
					mFont.drawString(g, center.caption, GameCanvas.hw, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
				}
			}
		}
		bool flag7 = right == null;
		if (!flag7)
		{
			Paint.lenCaption = mFont.getWidth(right.caption);
			bool flag8 = Paint.lenCaption > 0;
			if (flag8)
			{
				bool flag9 = right.x > 0 && right.y > 0;
				if (flag9)
				{
					right.paint(g);
				}
				else
				{
					g.drawImage((mScreen.keyTouch != 2) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, GameCanvas.w - 71, GameCanvas.h - mScreen.cmdH - 1, 0);
					mFont.drawString(g, right.caption, GameCanvas.w - 35, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
				}
			}
		}
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void paintTabSoft(mGraphics g)
	{
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x00005532 File Offset: 0x00003732
	public void paintSelect(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(16774843);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0000554E File Offset: 0x0000374E
	public void paintLogo(mGraphics g, int x, int y)
	{
		g.drawImage(Paint.imgLogo, x, y, 3);
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void paintHotline(mGraphics g, string number)
	{
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x000685CC File Offset: 0x000667CC
	public void paintBackMenu(mGraphics g, int x, int y, int w, int h, bool iss)
	{
		if (iss)
		{
			g.setColor(16646144);
			g.fillRoundRect(x, y, w, h, 10, 10);
			g.setColor(16770612);
		}
		else
		{
			g.setColor(16775097);
			g.fillRoundRect(x, y, w, h, 10, 10);
			g.setColor(16775097);
		}
		g.fillRoundRect(x + 3, y + 3, w - 6, h - 6, 10, 10);
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void paintMsgBG(mGraphics g, int x, int y, int w, int h, string title, string subTitle, string check)
	{
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void paintDefaultScrList(mGraphics g, string title, string subTitle, string check)
	{
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x00068654 File Offset: 0x00066854
	public void paintCheck(mGraphics g, int x, int y, int index)
	{
		g.drawImage(Paint.imgTick[1], x, y, 3);
		bool flag = index == 1;
		if (flag)
		{
			g.drawImage(Paint.imgTick[0], x + 1, y - 3, 3);
		}
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x00005560 File Offset: 0x00003760
	public void paintImgMsg(mGraphics g, int x, int y, int index)
	{
		g.drawImage(Paint.imgMsg[index], x, y, 0);
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x00005575 File Offset: 0x00003775
	public void paintTitleBoard(mGraphics g, int roomId)
	{
		this.paintDefaultBg(g);
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x00068694 File Offset: 0x00066894
	public void paintCheckPass(mGraphics g, int x, int y, bool check, bool focus)
	{
		if (focus)
		{
			g.drawRegion(Paint.imgCheck, 0, ((!check) ? 1 : 3) * 18, 20, 18, 0, x, y, 0);
		}
		else
		{
			g.drawRegion(Paint.imgCheck, 0, (check ? 2 : 0) * 18, 20, 18, 0, x, y, 0);
		}
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x000686F0 File Offset: 0x000668F0
	public void paintInputDlg(mGraphics g, int x, int y, int w, int h, string[] str)
	{
		this.paintFrame(x, y, w, h, g);
		int num = y + 20 - mFont.tahoma_8b.getHeight();
		int i = 0;
		int num2 = num;
		while (i < str.Length)
		{
			mFont.tahoma_8b.drawString(g, str[i], x + w / 2, num2, 2);
			i++;
			num2 += mFont.tahoma_8b.getHeight();
		}
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void paintIconMainMenu(mGraphics g, int x, int y, bool iss, bool isSe, int i, int wStr)
	{
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x00005580 File Offset: 0x00003780
	public void paintLineRoom(mGraphics g, int x, int y, int xTo, int yTo)
	{
		g.setColor(16774843);
		g.drawLine(x, y, xTo, yTo);
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x00068758 File Offset: 0x00066958
	public void paintCellContaint(mGraphics g, int x, int y, int w, int h, bool iss)
	{
		if (iss)
		{
			g.setColor(13132288);
			g.fillRect(x + 2, y + 2, w - 3, w - 3);
		}
		g.setColor(3502080);
		g.drawRect(x, y, w, w);
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x0000559C File Offset: 0x0000379C
	public void paintScroll(mGraphics g, int x, int y, int h)
	{
		g.setColor(3847752);
		g.fillRect(x, y, 4, h);
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x000687A8 File Offset: 0x000669A8
	public int[] getColorMsg()
	{
		return this.color;
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x000055B7 File Offset: 0x000037B7
	public void paintLogo(mGraphics g)
	{
		g.setColor(8916494);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(Paint.imgLogo, GameCanvas.h >> 1, GameCanvas.w >> 1, 3);
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x000687C0 File Offset: 0x000669C0
	public void paintTextLogin(mGraphics g, bool isRes)
	{
		int num = 0;
		bool flag = !isRes && GameCanvas.h <= 240;
		if (flag)
		{
			num = 15;
		}
		mFont.tahoma_7b_green2.drawString(g, mResources.LOGINLABELS[0], GameCanvas.hw, GameCanvas.hh + 60 - num, 2);
		mFont.tahoma_7b_green2.drawString(g, mResources.LOGINLABELS[1], GameCanvas.hw, GameCanvas.hh + 73 - num, 2);
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x000055F4 File Offset: 0x000037F4
	public void paintSellectBoard(mGraphics g, int x, int y, int w, int h)
	{
		g.drawImage(Paint.imgSelectBoard, x - 7, y, 0);
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x00068834 File Offset: 0x00066A34
	public int isRegisterUsingWAP()
	{
		return 0;
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x00068848 File Offset: 0x00066A48
	public string getCard()
	{
		return "/vmg/card.on";
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x00005608 File Offset: 0x00003808
	public void paintSellectedShop(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(16777215);
		g.drawRect(x, y, 40, 40);
		g.drawRect(x + 1, y + 1, 38, 38);
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x00068860 File Offset: 0x00066A60
	public string getUrlUpdateGame()
	{
		return string.Concat(new object[]
		{
			"http://wap.teamobi.com?info=checkupdate&game=3&version=",
			GameMidlet.VERSION,
			"&provider=",
			GameMidlet.PROVIDER
		});
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void doSelect(int focus)
	{
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x000688A4 File Offset: 0x00066AA4
	public void paintPopUp(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(9340251);
		g.drawRect(x + 18, y, (w - 36) / 2 - 32, h);
		g.drawRect(x + 18 + (w - 36) / 2 + 32, y, (w - 36) / 2 - 22, h);
		g.drawRect(x, y + 8, w, h - 17);
		g.setColor(Paint.COLORBACKGROUND);
		g.fillRect(x + 18, y + 3, (w - 36) / 2 - 32, h - 4);
		g.fillRect(x + 18 + (w - 36) / 2 + 31, y + 3, (w - 38) / 2 - 22, h - 4);
		g.fillRect(x + 1, y + 6, w - 1, h - 11);
		g.setColor(14667919);
		g.fillRect(x + 18, y + 1, (w - 36) / 2 - 32, 2);
		g.fillRect(x + 18 + (w - 36) / 2 + 32, y + 1, (w - 36) / 2 - 12, 2);
		g.fillRect(x + 18, y + h - 2, (w - 36) / 2 - 31, 2);
		g.fillRect(x + 18 + (w - 36) / 2 + 32, y + h - 2, (w - 36) / 2 - 31, 2);
		g.fillRect(x + 1, y + 11, 2, h - 18);
		g.fillRect(x + w - 2, y + 11, 2, h - 18);
		g.drawImage(Paint.goc[0], x - 3, y - 2, mGraphics.TOP | mGraphics.LEFT);
		g.drawImage(Paint.goc[2], x + w + 3, y - 2, StaticObj.TOP_RIGHT);
		g.drawImage(Paint.goc[1], x - 3, y + h + 3, StaticObj.BOTTOM_LEFT);
		g.drawImage(Paint.goc[3], x + w + 4, y + h + 2, StaticObj.BOTTOM_RIGHT);
		g.drawImage(Paint.goc[4], x + w / 2, y, StaticObj.TOP_CENTER);
		g.drawImage(Paint.goc[5], x + w / 2, y + h + 1, StaticObj.BOTTOM_HCENTER);
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x00068AD8 File Offset: 0x00066CD8
	public void paintFrame(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(13524492);
		g.drawRect(x + 6, y, w - 12, h);
		g.drawRect(x, y + 6, w, h - 12);
		g.drawRect(x + 7, y + 1, w - 14, h - 2);
		g.drawRect(x + 1, y + 7, w - 2, h - 14);
		g.setColor(14338484);
		g.fillRect(x + 8, y + 2, w - 16, h - 3);
		g.fillRect(x + 2, y + 8, w - 3, h - 14);
		g.drawImage(GameCanvas.imgBorder[2], x, y, mGraphics.TOP | mGraphics.LEFT);
		g.drawRegion(GameCanvas.imgBorder[2], 0, 0, 16, 16, 2, x + w + 1, y, StaticObj.TOP_RIGHT);
		g.drawRegion(GameCanvas.imgBorder[2], 0, 0, 16, 16, 1, x, y + h + 1, StaticObj.BOTTOM_LEFT);
		g.drawRegion(GameCanvas.imgBorder[2], 0, 0, 16, 16, 3, x + w + 1, y + h + 1, StaticObj.BOTTOM_RIGHT);
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x00005635 File Offset: 0x00003835
	public void paintFrameSimple(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(6702080);
		g.fillRect(x, y, w, h);
		g.setColor(14338484);
		g.fillRect(x + 1, y + 1, w - 2, h - 2);
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x00005674 File Offset: 0x00003874
	public void paintFrameBorder(int x, int y, int w, int h, mGraphics g)
	{
		this.paintFrame(x, y, w, h, g);
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x00005685 File Offset: 0x00003885
	public void paintFrameInside(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(Paint.COLORBACKGROUND);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x000056A2 File Offset: 0x000038A2
	public void paintFrameInsideSelected(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(Paint.COLORLIGHT);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x04000DBA RID: 3514
	public static int COLORBACKGROUND = 15787715;

	// Token: 0x04000DBB RID: 3515
	public static int COLORLIGHT = 16383818;

	// Token: 0x04000DBC RID: 3516
	public static int COLORDARK = 3937280;

	// Token: 0x04000DBD RID: 3517
	public static int COLORBORDER = 15224576;

	// Token: 0x04000DBE RID: 3518
	public static int COLORFOCUS = 16777215;

	// Token: 0x04000DBF RID: 3519
	public static Image imgBg;

	// Token: 0x04000DC0 RID: 3520
	public static Image imgLogo;

	// Token: 0x04000DC1 RID: 3521
	public static Image imgLB;

	// Token: 0x04000DC2 RID: 3522
	public static Image imgLT;

	// Token: 0x04000DC3 RID: 3523
	public static Image imgRB;

	// Token: 0x04000DC4 RID: 3524
	public static Image imgRT;

	// Token: 0x04000DC5 RID: 3525
	public static Image imgChuong;

	// Token: 0x04000DC6 RID: 3526
	public static Image imgSelectBoard;

	// Token: 0x04000DC7 RID: 3527
	public static Image imgtoiSmall;

	// Token: 0x04000DC8 RID: 3528
	public static Image imgTayTren;

	// Token: 0x04000DC9 RID: 3529
	public static Image imgTayDuoi;

	// Token: 0x04000DCA RID: 3530
	public static Image[] imgTick = new Image[2];

	// Token: 0x04000DCB RID: 3531
	public static Image[] imgMsg = new Image[2];

	// Token: 0x04000DCC RID: 3532
	public static Image[] goc = new Image[6];

	// Token: 0x04000DCD RID: 3533
	public static int hTab = 24;

	// Token: 0x04000DCE RID: 3534
	public static int lenCaption = 0;

	// Token: 0x04000DCF RID: 3535
	public int[] color = new int[]
	{
		15970400,
		13479911,
		2250052,
		16374659,
		15906669,
		12931125,
		3108954
	};

	// Token: 0x04000DD0 RID: 3536
	public static Image imgCheck = GameCanvas.loadImage("/mainImage/myTexture2dcheck.png");
}
