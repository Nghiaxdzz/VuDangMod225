using System;
using UnityEngine;

// Token: 0x0200008D RID: 141
public class RadarScr : mScreen
{
	// Token: 0x06000777 RID: 1911 RVA: 0x00085700 File Offset: 0x00083900
	public RadarScr()
	{
		RadarScr.TYPE_UI = true;
		Image img = mSystem.loadImage("/radar/17.png");
		Image img2 = mSystem.loadImage("/radar/3.png");
		Image img3 = mSystem.loadImage("/radar/23.png");
		RadarScr.fraImgFocus = new FrameImage(img, 28, 28);
		RadarScr.fraImgFocusNone = new FrameImage(img2, 30, 30);
		RadarScr.fraEff = new FrameImage(img3, 11, 11);
		RadarScr.imgUI = mSystem.loadImage("/radar/0.png");
		RadarScr.imgArrow_Left = mSystem.loadImage("/radar/1.png");
		RadarScr.imgArrow_Right = mSystem.loadImage("/radar/2.png");
		RadarScr.imgUIText = mSystem.loadImage("/radar/17.png");
		RadarScr.imgArrow_Down = mSystem.loadImage("/radar/4.png");
		RadarScr.imgLock = mSystem.loadImage("/radar/5.png");
		RadarScr.imgUse_0 = mSystem.loadImage("/radar/6.png");
		RadarScr.imgRank = new Image[7];
		for (int i = 0; i < 7; i++)
		{
			RadarScr.imgRank[i] = mSystem.loadImage("/radar/" + (i + 7) + ".png");
		}
		RadarScr.imgUse = mSystem.loadImage("/radar/14.png");
		RadarScr.imgBack = mSystem.loadImage("/radar/15.png");
		RadarScr.imgChange = mSystem.loadImage("/radar/16.png");
		RadarScr.imgUIText = mSystem.loadImage("/radar/18.png");
		RadarScr.imgBar_1 = mSystem.loadImage("/radar/19.png");
		RadarScr.imgPro_0 = mSystem.loadImage("/radar/20.png");
		RadarScr.imgPro_1 = mSystem.loadImage("/radar/21.png");
		RadarScr.imgBar_0 = mSystem.loadImage("/radar/22.png");
		RadarScr.wUi = 200;
		RadarScr.hUi = 219;
		RadarScr.xUi = GameCanvas.hw - (RadarScr.wUi + 40) / 2;
		RadarScr.yUi = GameCanvas.hh - RadarScr.hUi / 2;
		RadarScr.xText = RadarScr.xUi + RadarScr.wUi - 81;
		RadarScr.yText = RadarScr.yUi + 29;
		RadarScr.wText = 120;
		RadarScr.hText = 80;
		RadarScr.xyArrow = new int[][]
		{
			new int[]
			{
				RadarScr.xUi + 34,
				RadarScr.yUi + RadarScr.hUi - 42
			},
			new int[]
			{
				RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgArrow_Down.getWidth() / 2,
				RadarScr.yUi + RadarScr.hUi / 2 + 33
			},
			new int[]
			{
				RadarScr.xUi + RadarScr.wUi - 41,
				RadarScr.yUi + RadarScr.hUi - 42
			}
		};
		RadarScr.xyItem = new int[][]
		{
			new int[]
			{
				RadarScr.xUi + 25,
				RadarScr.yUi + RadarScr.hUi - 82
			},
			new int[]
			{
				RadarScr.xUi + 57,
				RadarScr.yUi + RadarScr.hUi - 62
			},
			new int[]
			{
				RadarScr.xUi + RadarScr.wUi / 2 - 14,
				RadarScr.yUi + RadarScr.hUi - 102
			},
			new int[]
			{
				RadarScr.xUi + RadarScr.wUi - 57 - 28,
				RadarScr.yUi + RadarScr.hUi - 62
			},
			new int[]
			{
				RadarScr.xUi + RadarScr.wUi - 25 - 28,
				RadarScr.yUi + RadarScr.hUi - 82
			}
		};
		this.dxArrow = new int[2];
		this.dyArrow = 0;
		RadarScr.xMon = RadarScr.xUi + 73;
		RadarScr.yMon = RadarScr.yUi + RadarScr.hUi / 2 + 5;
		RadarScr.yCmd = RadarScr.yUi + RadarScr.hUi - 22;
		RadarScr.xCmd = new int[]
		{
			RadarScr.xUi + RadarScr.wUi / 2 - 8 - 80,
			RadarScr.xUi + RadarScr.wUi / 2 - 8,
			RadarScr.xUi + RadarScr.wUi / 2 - 8 + 80
		};
		RadarScr.dxCmd = new int[3];
		this.yClip = RadarScr.yText + 10 + 70;
		this.hClip = 0;
		RadarScr.list = new MyVector();
		RadarScr.listUse = new MyVector();
		this.page = 1;
		this.maxpage = 2;
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x00085B3C File Offset: 0x00083D3C
	public static RadarScr gI()
	{
		bool flag = RadarScr.instance == null;
		if (flag)
		{
			RadarScr.instance = new RadarScr();
		}
		return RadarScr.instance;
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x00085B6C File Offset: 0x00083D6C
	public void SetRadarScr(MyVector list, int num, int numMax)
	{
		RadarScr.list = list;
		RadarScr.SetNum(num, numMax);
		this.page = 1;
		this.indexFocus = 2;
		this.listIndex();
		RadarScr.TYPE_UI = true;
		RadarScr.SetListUse();
		bool type_UI = RadarScr.TYPE_UI;
		if (type_UI)
		{
			this.maxpage = list.size() / 5 + ((list.size() % 5 > 0) ? 1 : 0);
		}
		else
		{
			this.maxpage = RadarScr.listUse.size() / 5 + ((RadarScr.listUse.size() % 5 > 0) ? 1 : 0);
		}
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x00005B6A File Offset: 0x00003D6A
	public static void SetNum(int num, int numMax)
	{
		RadarScr.num = num;
		RadarScr.numMax = numMax;
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x00085BFC File Offset: 0x00083DFC
	public static void SetListUse()
	{
		RadarScr.listUse = new MyVector(string.Empty);
		for (int i = 0; i < RadarScr.list.size(); i++)
		{
			Info_RadaScr info_RadaScr = (Info_RadaScr)RadarScr.list.elementAt(i);
			bool flag = info_RadaScr != null && info_RadaScr.isUse == 1;
			if (flag)
			{
				RadarScr.listUse.addElement(info_RadaScr);
			}
		}
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x00085C68 File Offset: 0x00083E68
	public void listIndex()
	{
		MyVector myVector = RadarScr.listUse;
		bool type_UI = RadarScr.TYPE_UI;
		if (type_UI)
		{
			myVector = RadarScr.list;
		}
		int num = (this.page - 1) * 5;
		int num2 = num + 5;
		for (int i = num; i < num2; i++)
		{
			bool flag = i >= myVector.size();
			if (flag)
			{
				RadarScr.index[i - num] = -1;
			}
			else
			{
				Info_RadaScr info_RadaScr = (Info_RadaScr)myVector.elementAt(i);
				bool flag2 = info_RadaScr != null;
				if (flag2)
				{
					RadarScr.index[i - num] = info_RadaScr.id;
				}
			}
		}
		RadarScr.cmyText = 0;
		RadarScr.hText = 0;
		SoundMn.gI().radarItem();
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x00085D1C File Offset: 0x00083F1C
	public override void update()
	{
		try
		{
			bool flag = RadarScr.hText < 80;
			if (flag)
			{
				RadarScr.hText += 4;
				bool flag2 = RadarScr.hText > 80;
				if (flag2)
				{
					RadarScr.hText = 80;
				}
			}
			this.focus_card = Info_RadaScr.GetInfo(RadarScr.listUse, RadarScr.index[this.indexFocus]);
			bool type_UI = RadarScr.TYPE_UI;
			if (type_UI)
			{
				this.focus_card = Info_RadaScr.GetInfo(RadarScr.list, RadarScr.index[this.indexFocus]);
			}
			GameScr.gI().update();
			bool flag3 = GameCanvas.gameTick % 10 < 6;
			if (flag3)
			{
				bool flag4 = GameCanvas.gameTick % 2 == 0;
				if (flag4)
				{
					this.dyArrow--;
				}
			}
			else
			{
				this.dyArrow = 0;
			}
			bool flag5 = this.focus_card != null;
			if (flag5)
			{
				int num = (int)(this.focus_card.amount * 100 / this.focus_card.max_amount);
				this.hClip = num * RadarScr.imgBar_1.getHeight() / 100;
				int num2 = RadarScr.num * 100 / RadarScr.list.size();
				this.wClip = num2 * RadarScr.imgPro_1.getWidth() / 100;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("-upd-radaScr-null: " + ex.ToString());
		}
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x00085E90 File Offset: 0x00084090
	public override void updateKey()
	{
		bool flag = !InfoDlg.isLock;
		if (flag)
		{
			bool flag2 = GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu;
			if (flag2)
			{
				this.updateKeyTouchControl();
			}
			bool flag3 = GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22];
			if (flag3)
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
				this.doKeyText(1);
			}
			bool flag4 = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21];
			if (flag4)
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = false;
				this.doKeyText(-1);
			}
			bool flag5 = GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23];
			if (flag5)
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = false;
				this.doKeyItem(1);
			}
			bool flag6 = GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24];
			if (flag6)
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = false;
				this.doKeyItem(0);
			}
			bool flag7 = GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25];
			if (flag7)
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				this.doClickUse(1);
			}
			bool flag8 = GameCanvas.keyPressed[13];
			if (flag8)
			{
				this.doClickUse(2);
			}
			bool flag9 = GameCanvas.keyPressed[12];
			if (flag9)
			{
				GameCanvas.keyPressed[12] = false;
				this.doClickUse(0);
			}
			GameCanvas.clearKeyPressed();
		}
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x00086024 File Offset: 0x00084224
	private void doChangeUI()
	{
		RadarScr.TYPE_UI = !RadarScr.TYPE_UI;
		this.page = 1;
		this.indexFocus = 0;
		bool type_UI = RadarScr.TYPE_UI;
		if (type_UI)
		{
			this.maxpage = RadarScr.list.size() / 5 + ((RadarScr.list.size() % 5 > 0) ? 1 : 0);
		}
		else
		{
			this.maxpage = RadarScr.listUse.size() / 5 + ((RadarScr.listUse.size() % 5 > 0) ? 1 : 0);
		}
		this.listIndex();
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x000860B0 File Offset: 0x000842B0
	private void updateKeyTouchControl()
	{
		bool isPointerClick = GameCanvas.isPointerClick;
		if (isPointerClick)
		{
			for (int i = 0; i < 5; i++)
			{
				bool flag = GameCanvas.isPointerHoldIn(RadarScr.xyItem[i][0], RadarScr.xyItem[i][1], 30, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease && i != this.indexFocus;
				if (flag)
				{
					this.doClickItem(i);
				}
			}
			bool flag2 = GameCanvas.isPointerHoldIn(RadarScr.xyArrow[0][0] - 5, RadarScr.xyArrow[0][1] - 5, 20, 20);
			if (flag2)
			{
				bool isPointerDown = GameCanvas.isPointerDown;
				if (isPointerDown)
				{
					this.dxArrow[0] = 1;
				}
				bool flag3 = GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
				if (flag3)
				{
					this.doClickArrow(0);
					this.dxArrow[0] = 0;
				}
			}
			bool flag4 = GameCanvas.isPointerHoldIn(RadarScr.xyArrow[2][0] - 5, RadarScr.xyArrow[2][1] - 5, 20, 20);
			if (flag4)
			{
				bool isPointerDown2 = GameCanvas.isPointerDown;
				if (isPointerDown2)
				{
					this.dxArrow[1] = 1;
				}
				bool flag5 = GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
				if (flag5)
				{
					this.doClickArrow(1);
					this.dxArrow[1] = 0;
				}
			}
			for (int j = 0; j < RadarScr.xCmd.Length; j++)
			{
				bool flag6 = GameCanvas.isPointerHoldIn(RadarScr.xCmd[j] - 5, RadarScr.yCmd - 5, 20, 20);
				if (flag6)
				{
					bool isPointerDown3 = GameCanvas.isPointerDown;
					if (isPointerDown3)
					{
						RadarScr.dxCmd[j] = 1;
					}
					bool flag7 = GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
					if (flag7)
					{
						this.doClickUse(j);
						RadarScr.dxCmd[j] = 0;
					}
				}
			}
		}
		else
		{
			RadarScr.dxCmd[0] = 0;
			RadarScr.dxCmd[1] = 0;
			RadarScr.dxCmd[2] = 0;
			this.dxArrow[0] = 0;
			this.dxArrow[1] = 0;
		}
		bool flag8 = !GameCanvas.isPointerHoldIn(RadarScr.xText, 0, RadarScr.wText, RadarScr.yText + RadarScr.hText);
		if (!flag8)
		{
			bool isPointerMove = GameCanvas.isPointerMove;
			if (isPointerMove)
			{
				bool flag9 = this.pyy == 0;
				if (flag9)
				{
					this.pyy = GameCanvas.py;
				}
				this.pxx = this.pyy - GameCanvas.py;
				bool flag10 = this.pxx != 0;
				if (flag10)
				{
					RadarScr.cmyText += this.pxx;
					this.pyy = GameCanvas.py;
				}
				bool flag11 = RadarScr.cmyText < 0;
				if (flag11)
				{
					RadarScr.cmyText = 0;
				}
				bool flag12 = RadarScr.cmyText > this.focus_card.cp.lim;
				if (flag12)
				{
					RadarScr.cmyText = this.focus_card.cp.lim;
				}
			}
			else
			{
				this.pyy = 0;
				this.pyy = 0;
			}
		}
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x00086390 File Offset: 0x00084590
	private void doClickUse(int i)
	{
		switch (i)
		{
		case 0:
			this.doChangeUI();
			break;
		case 1:
		{
			bool flag = this.focus_card != null;
			if (flag)
			{
				Service.gI().SendRada(1, this.focus_card.id);
			}
			break;
		}
		case 2:
			GameScr.gI().switchToMe();
			break;
		}
		SoundMn.gI().radarClick();
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x00086400 File Offset: 0x00084600
	private void doClickArrow(int dir)
	{
		bool type_UI = RadarScr.TYPE_UI;
		if (type_UI)
		{
			this.maxpage = RadarScr.list.size() / 5 + ((RadarScr.list.size() % 5 > 0) ? 1 : 0);
		}
		else
		{
			this.maxpage = RadarScr.listUse.size() / 5 + ((RadarScr.listUse.size() % 5 > 0) ? 1 : 0);
		}
		int num = this.page;
		bool flag = dir == 0;
		if (flag)
		{
			bool flag2 = this.page == 1;
			if (flag2)
			{
				return;
			}
			num--;
			bool flag3 = num < 1;
			if (flag3)
			{
				num = 1;
			}
		}
		else
		{
			bool flag4 = this.page == this.maxpage;
			if (flag4)
			{
				return;
			}
			num++;
			bool flag5 = num > this.maxpage;
			if (flag5)
			{
				num = this.maxpage;
			}
		}
		bool flag6 = num != this.page;
		if (flag6)
		{
			this.page = num;
			this.listIndex();
		}
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x00005B79 File Offset: 0x00003D79
	private void doClickItem(int focus)
	{
		this.indexFocus = focus;
		this.listIndex();
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x000864F8 File Offset: 0x000846F8
	private void doKeyText(int type)
	{
		RadarScr.cmyText += 12 * type;
		bool flag = RadarScr.cmyText < 0;
		if (flag)
		{
			RadarScr.cmyText = 0;
		}
		bool flag2 = RadarScr.cmyText > this.focus_card.cp.lim;
		if (flag2)
		{
			RadarScr.cmyText = this.focus_card.cp.lim;
		}
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x0008655C File Offset: 0x0008475C
	private void doKeyItem(int type)
	{
		int num = this.indexFocus;
		int num2 = this.page;
		num = ((type != 0) ? (num - 1) : (num + 1));
		bool flag = num >= RadarScr.index.Length;
		if (flag)
		{
			bool flag2 = this.page < this.maxpage;
			if (flag2)
			{
				num = 0;
				num2++;
			}
			else
			{
				num = RadarScr.index.Length - 1;
			}
		}
		bool flag3 = num < 0;
		if (flag3)
		{
			bool flag4 = this.page > 1;
			if (flag4)
			{
				num = RadarScr.index.Length - 1;
				num2--;
			}
			else
			{
				num = 0;
			}
		}
		bool flag5 = num != this.indexFocus;
		if (flag5)
		{
			this.indexFocus = num;
			RadarScr.cmyText = 0;
			RadarScr.hText = 0;
		}
		bool flag6 = num2 != this.page;
		if (flag6)
		{
			this.page = num2;
			this.listIndex();
		}
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x0008663C File Offset: 0x0008483C
	public override void paint(mGraphics g)
	{
		try
		{
			GameScr.gI().paint(g);
			g.translate(-GameScr.cmx, -GameScr.cmy);
			g.translate(0, GameCanvas.transY);
			GameScr.resetTranslate(g);
			g.drawImage(RadarScr.imgUI, RadarScr.xUi, RadarScr.yUi, 0);
			g.drawImage(RadarScr.imgPro_0, RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgPro_0.getWidth() / 2, RadarScr.yUi - RadarScr.imgPro_0.getHeight() / 2 - 2, 0);
			g.setClip(RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgPro_0.getWidth() / 2 + 13, RadarScr.yUi - RadarScr.imgPro_0.getHeight() / 2 + 3, this.wClip, RadarScr.imgPro_0.getHeight());
			g.drawImage(RadarScr.imgPro_1, RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgPro_0.getWidth() / 2 + 13, RadarScr.yUi - RadarScr.imgPro_0.getHeight() / 2 + 3, 0);
			GameScr.resetTranslate(g);
			g.drawImage(RadarScr.imgChange, RadarScr.xCmd[0], RadarScr.yCmd + RadarScr.dxCmd[0], 0);
			g.drawImage(RadarScr.imgUse_0, RadarScr.xCmd[1], RadarScr.yCmd + RadarScr.dxCmd[1], 0);
			g.drawImage(RadarScr.imgBack, RadarScr.xCmd[2], RadarScr.yCmd + RadarScr.dxCmd[2], 0);
			bool type_UI = RadarScr.TYPE_UI;
			if (type_UI)
			{
				g.drawRegion(RadarScr.imgUse, 0, 0, 17, 17, 0, RadarScr.xCmd[1], RadarScr.yCmd + RadarScr.dxCmd[1], 0);
			}
			else
			{
				g.drawRegion(RadarScr.imgUse, 0, 0, 17, 17, 1, RadarScr.xCmd[1], RadarScr.yCmd + RadarScr.dxCmd[1], 0);
			}
			bool flag = this.focus_card != null;
			if (flag)
			{
				g.setClip(RadarScr.xUi + 30, RadarScr.yUi + 13, RadarScr.wUi - 60, RadarScr.hUi / 2);
				this.focus_card.paintInfo(g, RadarScr.xMon, RadarScr.yMon);
				GameScr.resetTranslate(g);
				mFont.tahoma_7b_yellow.drawString(g, ((this.focus_card.level <= 0) ? " " : ("Lv." + this.focus_card.level + " ")) + this.focus_card.name, RadarScr.xUi + RadarScr.wUi / 2, RadarScr.yUi + 15, 2);
				mFont.tahoma_7_white.drawString(g, "no." + this.focus_card.no, RadarScr.xUi + 30, RadarScr.yText - 2, 0);
				g.drawImage(RadarScr.imgBar_0, RadarScr.xUi + 36, RadarScr.yText + 10, 0);
				g.setClip(RadarScr.xUi + 36, this.yClip - this.hClip, 7, this.hClip);
				g.drawImage(RadarScr.imgBar_1, RadarScr.xUi + 36, RadarScr.yText + 10, 0);
				GameScr.resetTranslate(g);
				g.drawImage(RadarScr.imgRank[(int)this.focus_card.rank], RadarScr.xUi + 39 - 5 + 14, RadarScr.yText + 12, 0);
			}
			g.setClip(RadarScr.xText, RadarScr.yText, RadarScr.wText + 5, RadarScr.hText + 8);
			bool flag2 = this.focus_card != null;
			if (flag2)
			{
				g.drawImage(RadarScr.imgUIText, RadarScr.xText, RadarScr.yText, 0);
			}
			GameScr.resetTranslate(g);
			g.setClip(RadarScr.xText, RadarScr.yText + 1, RadarScr.wText, RadarScr.hText + 5);
			bool flag3 = this.focus_card == null || this.focus_card.cp == null;
			if (!flag3)
			{
				bool flag4 = this.focus_card.cp.says == null;
				if (flag4)
				{
					return;
				}
				this.focus_card.cp.paintRada(g, RadarScr.cmyText);
			}
			GameScr.resetTranslate(g);
			bool flag5 = (!RadarScr.TYPE_UI && RadarScr.listUse.size() > 5) || RadarScr.TYPE_UI;
			if (flag5)
			{
				bool flag6 = this.page > 1;
				if (flag6)
				{
					g.drawImage(RadarScr.imgArrow_Left, RadarScr.xyArrow[0][0], RadarScr.xyArrow[0][1] + this.dxArrow[0], 0);
				}
				bool flag7 = this.page < this.maxpage;
				if (flag7)
				{
					g.drawImage(RadarScr.imgArrow_Right, RadarScr.xyArrow[2][0], RadarScr.xyArrow[2][1] + this.dxArrow[1], 0);
				}
			}
			for (int i = 0; i < RadarScr.index.Length; i++)
			{
				int num = 0;
				int num2 = 0;
				int idx = 0;
				bool flag8 = i == this.indexFocus;
				if (flag8)
				{
					num = this.dyArrow;
					num2 = -10;
					idx = 1;
					g.drawImage(RadarScr.imgArrow_Down, RadarScr.xyItem[i][0] + 10, RadarScr.xyItem[i][1] + this.dyArrow + 29 + num2, 0);
				}
				Info_RadaScr info = Info_RadaScr.GetInfo(RadarScr.listUse, RadarScr.index[i]);
				bool type_UI2 = RadarScr.TYPE_UI;
				if (type_UI2)
				{
					info = Info_RadaScr.GetInfo(RadarScr.list, RadarScr.index[i]);
				}
				bool flag9 = info != null;
				if (flag9)
				{
					RadarScr.fraImgFocus.drawFrame((int)info.rank, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0, 0, g);
					SmallImage.drawSmallImage(g, info.idIcon, RadarScr.xyItem[i][0] + 14, RadarScr.xyItem[i][1] + 14 + num + num2, 0, StaticObj.VCENTER_HCENTER);
					info.paintEff(g, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2);
					bool flag10 = info.level == 0;
					if (flag10)
					{
						g.drawImage(RadarScr.imgLock, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0);
					}
					bool flag11 = i == this.indexFocus;
					if (flag11)
					{
						RadarScr.fraImgFocus.drawFrame(7, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0, 0, g);
					}
					bool flag12 = info.isUse == 1;
					if (flag12)
					{
						RadarScr.fraImgFocus.drawFrame(8, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0, 0, g);
					}
				}
				else
				{
					RadarScr.fraImgFocusNone.drawFrame(idx, RadarScr.xyItem[i][0] - 1, RadarScr.xyItem[i][1] - 1 + num + num2, 0, 0, g);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("-pnt-radaScr-null: " + ex.ToString());
		}
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x00005B8A File Offset: 0x00003D8A
	public override void switchToMe()
	{
		GameScr.isPaintOther = true;
		base.switchToMe();
	}

	// Token: 0x04000F86 RID: 3974
	public const sbyte SUBCMD_ALL = 0;

	// Token: 0x04000F87 RID: 3975
	public const sbyte SUBCMD_USE = 1;

	// Token: 0x04000F88 RID: 3976
	public const sbyte SUBCMD_LEVEL = 2;

	// Token: 0x04000F89 RID: 3977
	public const sbyte SUBCMD_AMOUNT = 3;

	// Token: 0x04000F8A RID: 3978
	public const sbyte SUBCMD_AURA = 4;

	// Token: 0x04000F8B RID: 3979
	public static RadarScr instance;

	// Token: 0x04000F8C RID: 3980
	public static bool TYPE_UI;

	// Token: 0x04000F8D RID: 3981
	public static FrameImage fraImgFocus;

	// Token: 0x04000F8E RID: 3982
	public static FrameImage fraImgFocusNone;

	// Token: 0x04000F8F RID: 3983
	public static FrameImage fraEff;

	// Token: 0x04000F90 RID: 3984
	private static Image imgUI;

	// Token: 0x04000F91 RID: 3985
	private static Image imgUIText;

	// Token: 0x04000F92 RID: 3986
	private static Image imgArrow_Left;

	// Token: 0x04000F93 RID: 3987
	private static Image imgArrow_Right;

	// Token: 0x04000F94 RID: 3988
	private static Image imgArrow_Down;

	// Token: 0x04000F95 RID: 3989
	private static Image imgLock;

	// Token: 0x04000F96 RID: 3990
	private static Image imgUse_0;

	// Token: 0x04000F97 RID: 3991
	private static Image imgUse;

	// Token: 0x04000F98 RID: 3992
	private static Image imgBack;

	// Token: 0x04000F99 RID: 3993
	private static Image imgChange;

	// Token: 0x04000F9A RID: 3994
	private static Image imgBar_0;

	// Token: 0x04000F9B RID: 3995
	private static Image imgBar_1;

	// Token: 0x04000F9C RID: 3996
	private static Image imgPro_0;

	// Token: 0x04000F9D RID: 3997
	private static Image imgPro_1;

	// Token: 0x04000F9E RID: 3998
	private static Image[] imgRank;

	// Token: 0x04000F9F RID: 3999
	public static int xUi;

	// Token: 0x04000FA0 RID: 4000
	public static int yUi;

	// Token: 0x04000FA1 RID: 4001
	public static int wUi;

	// Token: 0x04000FA2 RID: 4002
	public static int hUi;

	// Token: 0x04000FA3 RID: 4003
	public static int xMon;

	// Token: 0x04000FA4 RID: 4004
	public static int yMon;

	// Token: 0x04000FA5 RID: 4005
	public static int xText;

	// Token: 0x04000FA6 RID: 4006
	public static int yText;

	// Token: 0x04000FA7 RID: 4007
	public static int wText;

	// Token: 0x04000FA8 RID: 4008
	public static int cmyText;

	// Token: 0x04000FA9 RID: 4009
	public static int hText;

	// Token: 0x04000FAA RID: 4010
	public static int yCmd;

	// Token: 0x04000FAB RID: 4011
	public static int[] xCmd = new int[0];

	// Token: 0x04000FAC RID: 4012
	public static int[] dxCmd = new int[0];

	// Token: 0x04000FAD RID: 4013
	private static int[][] xyArrow;

	// Token: 0x04000FAE RID: 4014
	private static int[][] xyItem;

	// Token: 0x04000FAF RID: 4015
	private static int[] index = new int[]
	{
		-2,
		-1,
		0,
		1,
		2
	};

	// Token: 0x04000FB0 RID: 4016
	private int dyArrow;

	// Token: 0x04000FB1 RID: 4017
	private int[] dxArrow;

	// Token: 0x04000FB2 RID: 4018
	private int page;

	// Token: 0x04000FB3 RID: 4019
	private int maxpage;

	// Token: 0x04000FB4 RID: 4020
	private int indexFocus;

	// Token: 0x04000FB5 RID: 4021
	public static MyVector list;

	// Token: 0x04000FB6 RID: 4022
	public static MyVector listUse;

	// Token: 0x04000FB7 RID: 4023
	private static int num;

	// Token: 0x04000FB8 RID: 4024
	private static int numMax;

	// Token: 0x04000FB9 RID: 4025
	private Info_RadaScr focus_card;

	// Token: 0x04000FBA RID: 4026
	private int pxx;

	// Token: 0x04000FBB RID: 4027
	private int pyy;

	// Token: 0x04000FBC RID: 4028
	private int xClip;

	// Token: 0x04000FBD RID: 4029
	private int wClip;

	// Token: 0x04000FBE RID: 4030
	private int yClip;

	// Token: 0x04000FBF RID: 4031
	private int hClip;
}
