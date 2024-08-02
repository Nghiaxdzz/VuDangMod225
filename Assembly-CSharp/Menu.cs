using System;

// Token: 0x02000064 RID: 100
public class Menu
{
	// Token: 0x0600049B RID: 1179 RVA: 0x00004D9C File Offset: 0x00002F9C
	public static void loadBg()
	{
		Menu.imgMenu1 = GameCanvas.loadImage("/mainImage/myTexture2dbtMenu1.png");
		Menu.imgMenu2 = GameCanvas.loadImage("/mainImage/myTexture2dbtMenu2.png");
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x00004DBD File Offset: 0x00002FBD
	public void startWithoutCloseButton(MyVector menuItems, int pos)
	{
		this.startAt(menuItems, pos);
		this.disableClose = true;
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x000589C8 File Offset: 0x00056BC8
	public void startAt(MyVector menuItems, int x, int y)
	{
		this.startAt(menuItems, 0);
		this.menuX = x;
		this.menuY = y;
		while (this.menuY + this.menuH > GameCanvas.h)
		{
			this.menuY -= 2;
		}
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00058A18 File Offset: 0x00056C18
	public void startAt(MyVector menuItems, int pos)
	{
		bool flag = this.showMenu;
		if (!flag)
		{
			this.isClose = false;
			this.touch = false;
			this.close = false;
			this.tDelay = 0;
			bool flag2 = menuItems.size() == 1;
			if (flag2)
			{
				this.menuSelectedItem = 0;
				Command command = (Command)menuItems.elementAt(0);
				bool flag3 = command != null && command.caption.Equals(mResources.saying);
				if (flag3)
				{
					command.performAction();
					this.showMenu = false;
					InfoDlg.showWait();
					return;
				}
			}
			SoundMn.gI().openMenu();
			this.isNotClose = new bool[menuItems.size()];
			for (int i = 0; i < this.isNotClose.Length; i++)
			{
				this.isNotClose[i] = false;
			}
			this.disableClose = false;
			ChatPopup.currChatPopup = null;
			Effect2.vEffect2.removeAllElements();
			Effect2.vEffect2Outside.removeAllElements();
			InfoDlg.hide();
			bool flag4 = menuItems.size() != 0;
			if (flag4)
			{
				this.menuItems = menuItems;
				this.menuW = 60;
				this.menuH = 60;
				for (int j = 0; j < menuItems.size(); j++)
				{
					Command command2 = (Command)menuItems.elementAt(j);
					command2.isPlaySoundButton = false;
					int width = mFont.tahoma_7_yellow.getWidth(command2.caption);
					command2.subCaption = mFont.tahoma_7_yellow.splitFontArray(command2.caption, this.menuW - 10);
					Res.outz("c caption= " + command2.caption);
				}
				Menu.menuTemY = new int[menuItems.size()];
				this.menuX = (GameCanvas.w - menuItems.size() * this.menuW) / 2;
				bool flag5 = this.menuX < 1;
				if (flag5)
				{
					this.menuX = 1;
				}
				this.menuY = GameCanvas.h - this.menuH - (Paint.hTab + 1) - 1;
				bool isTouch = GameCanvas.isTouch;
				if (isTouch)
				{
					this.menuY -= 3;
				}
				this.menuY += 27;
				for (int k = 0; k < Menu.menuTemY.Length; k++)
				{
					Menu.menuTemY[k] = GameCanvas.h;
				}
				this.showMenu = true;
				this.menuSelectedItem = 0;
				Menu.cmxLim = this.menuItems.size() * this.menuW - GameCanvas.w;
				bool flag6 = Menu.cmxLim < 0;
				if (flag6)
				{
					Menu.cmxLim = 0;
				}
				Menu.cmtoX = 0;
				Menu.cmx = 0;
				Menu.xc = 50;
				this.w = menuItems.size() * this.menuW - 1;
				bool flag7 = this.w > GameCanvas.w - 2;
				if (flag7)
				{
					this.w = GameCanvas.w - 2;
				}
				bool flag8 = GameCanvas.isTouch && !Main.isPC;
				if (flag8)
				{
					this.menuSelectedItem = -1;
				}
			}
		}
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00058D24 File Offset: 0x00056F24
	public bool isScrolling()
	{
		return (!this.isClose && Menu.menuTemY[Menu.menuTemY.Length - 1] > this.menuY) || (this.isClose && Menu.menuTemY[Menu.menuTemY.Length - 1] < GameCanvas.h);
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x00058D84 File Offset: 0x00056F84
	public void updateMenuKey()
	{
		bool flag = (GameScr.gI().activeRongThan && GameScr.gI().isUseFreez) || !this.showMenu || this.isScrolling();
		if (!flag)
		{
			bool flag2 = false;
			bool flag3 = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23];
			if (flag3)
			{
				flag2 = true;
				this.menuSelectedItem--;
				bool flag4 = this.menuSelectedItem < 0;
				if (flag4)
				{
					this.menuSelectedItem = this.menuItems.size() - 1;
				}
			}
			else
			{
				bool flag5 = GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24];
				if (flag5)
				{
					flag2 = true;
					this.menuSelectedItem++;
					bool flag6 = this.menuSelectedItem > this.menuItems.size() - 1;
					if (flag6)
					{
						this.menuSelectedItem = 0;
					}
				}
				else
				{
					bool flag7 = GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25];
					if (flag7)
					{
						bool flag8 = this.center != null;
						if (flag8)
						{
							bool flag9 = this.center.idAction > 0;
							if (flag9)
							{
								bool flag10 = this.center.actionListener == GameScr.gI();
								if (flag10)
								{
									GameScr.gI().actionPerform(this.center.idAction, this.center.p);
								}
								else
								{
									this.perform(this.center.idAction, this.center.p);
								}
							}
						}
						else
						{
							this.waitToPerform = 2;
						}
					}
					else
					{
						bool flag11 = GameCanvas.keyPressed[12] && !GameScr.gI().isRongThanMenu();
						if (flag11)
						{
							bool flag12 = this.isScrolling();
							if (flag12)
							{
								return;
							}
							bool flag13 = this.left.idAction > 0;
							if (flag13)
							{
								this.perform(this.left.idAction, this.left.p);
							}
							else
							{
								this.waitToPerform = 2;
							}
							SoundMn.gI().buttonClose();
						}
						else
						{
							bool flag14 = !GameScr.gI().isRongThanMenu() && !this.disableClose && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right));
							if (flag14)
							{
								bool flag15 = this.isScrolling();
								if (flag15)
								{
									return;
								}
								bool flag16 = !this.close;
								if (flag16)
								{
									this.close = true;
								}
								this.isClose = true;
								SoundMn.gI().buttonClose();
							}
						}
					}
				}
			}
			bool flag17 = flag2;
			if (flag17)
			{
				Menu.cmtoX = this.menuSelectedItem * this.menuW + this.menuW - GameCanvas.w / 2;
				bool flag18 = Menu.cmtoX > Menu.cmxLim;
				if (flag18)
				{
					Menu.cmtoX = Menu.cmxLim;
				}
				bool flag19 = Menu.cmtoX < 0;
				if (flag19)
				{
					Menu.cmtoX = 0;
				}
				bool flag20 = this.menuSelectedItem == this.menuItems.size() - 1 || this.menuSelectedItem == 0;
				if (flag20)
				{
					Menu.cmx = Menu.cmtoX;
				}
			}
			bool flag21 = true;
			bool flag22 = GameCanvas.panel.cp != null && GameCanvas.panel.cp.isClip;
			if (flag22)
			{
				bool flag23 = !GameCanvas.isPointerHoldIn(GameCanvas.panel.cp.cx, 0, GameCanvas.panel.cp.sayWidth + 2, GameCanvas.panel.cp.ch);
				if (flag23)
				{
					flag21 = true;
				}
				else
				{
					flag21 = false;
					GameCanvas.panel.cp.updateKey();
				}
			}
			bool flag24 = !this.disableClose && GameCanvas.isPointerJustRelease && !GameCanvas.isPointer(this.menuX, this.menuY, this.w, this.menuH) && !this.pointerIsDowning && !GameScr.gI().isRongThanMenu() && flag21;
			if (flag24)
			{
				bool flag25 = !this.isScrolling();
				if (flag25)
				{
					this.pointerDownTime = (this.pointerDownFirstX = 0);
					this.pointerIsDowning = false;
					GameCanvas.clearAllPointerEvent();
					Res.outz("menu select= " + this.menuSelectedItem);
					this.isClose = true;
					this.close = true;
					SoundMn.gI().buttonClose();
				}
			}
			else
			{
				bool isPointerDown = GameCanvas.isPointerDown;
				if (isPointerDown)
				{
					bool flag26 = !this.pointerIsDowning && GameCanvas.isPointer(this.menuX, this.menuY, this.w, this.menuH);
					if (flag26)
					{
						for (int i = 0; i < this.pointerDownLastX.Length; i++)
						{
							this.pointerDownLastX[0] = GameCanvas.px;
						}
						this.pointerDownFirstX = GameCanvas.px;
						this.pointerIsDowning = true;
						this.isDownWhenRunning = (this.cmRun != 0);
						this.cmRun = 0;
					}
					else
					{
						bool flag27 = this.pointerIsDowning;
						if (flag27)
						{
							this.pointerDownTime++;
							bool flag28 = this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.px && !this.isDownWhenRunning;
							if (flag28)
							{
								this.pointerDownFirstX = -1000;
								this.menuSelectedItem = (Menu.cmtoX + GameCanvas.px - this.menuX) / this.menuW;
							}
							int num = GameCanvas.px - this.pointerDownLastX[0];
							bool flag29 = num != 0 && this.menuSelectedItem != -1;
							if (flag29)
							{
								this.menuSelectedItem = -1;
							}
							for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
							{
								this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
							}
							this.pointerDownLastX[0] = GameCanvas.px;
							Menu.cmtoX -= num;
							bool flag30 = Menu.cmtoX < 0;
							if (flag30)
							{
								Menu.cmtoX = 0;
							}
							bool flag31 = Menu.cmtoX > Menu.cmxLim;
							if (flag31)
							{
								Menu.cmtoX = Menu.cmxLim;
							}
							bool flag32 = Menu.cmx < 0 || Menu.cmx > Menu.cmxLim;
							if (flag32)
							{
								num /= 2;
							}
							Menu.cmx -= num;
							bool flag33 = Menu.cmx < -(GameCanvas.h / 3);
							if (flag33)
							{
								this.wantUpdateList = true;
							}
							else
							{
								this.wantUpdateList = false;
							}
						}
					}
				}
				bool flag34 = GameCanvas.isPointerJustRelease && this.pointerIsDowning;
				if (flag34)
				{
					int i2 = GameCanvas.px - this.pointerDownLastX[0];
					GameCanvas.isPointerJustRelease = false;
					bool flag35 = Res.abs(i2) < 20 && Res.abs(GameCanvas.px - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning;
					if (flag35)
					{
						this.cmRun = 0;
						Menu.cmtoX = Menu.cmx;
						this.pointerDownFirstX = -1000;
						this.menuSelectedItem = (Menu.cmtoX + GameCanvas.px - this.menuX) / this.menuW;
						this.pointerDownTime = 0;
						this.waitToPerform = 10;
					}
					else
					{
						bool flag36 = this.menuSelectedItem != -1 && this.pointerDownTime > 5;
						if (flag36)
						{
							this.pointerDownTime = 0;
							this.waitToPerform = 1;
						}
						else
						{
							bool flag37 = this.menuSelectedItem == -1 && !this.isDownWhenRunning;
							if (flag37)
							{
								bool flag38 = Menu.cmx < 0;
								if (flag38)
								{
									Menu.cmtoX = 0;
								}
								else
								{
									bool flag39 = Menu.cmx > Menu.cmxLim;
									if (flag39)
									{
										Menu.cmtoX = Menu.cmxLim;
									}
									else
									{
										int num2 = GameCanvas.px - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
										num2 = ((num2 > 10) ? 10 : ((num2 < -10) ? -10 : 0));
										this.cmRun = -num2 * 100;
									}
								}
							}
						}
					}
					this.pointerIsDowning = false;
					this.pointerDownTime = 0;
					GameCanvas.isPointerJustRelease = false;
				}
				GameCanvas.clearKeyPressed();
				GameCanvas.clearKeyHold();
			}
		}
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x000595D8 File Offset: 0x000577D8
	public void moveCamera()
	{
		bool flag = this.cmRun != 0 && !this.pointerIsDowning;
		if (flag)
		{
			Menu.cmtoX += this.cmRun / 100;
			bool flag2 = Menu.cmtoX < 0;
			if (flag2)
			{
				Menu.cmtoX = 0;
			}
			else
			{
				bool flag3 = Menu.cmtoX > Menu.cmxLim;
				if (flag3)
				{
					Menu.cmtoX = Menu.cmxLim;
				}
				else
				{
					Menu.cmx = Menu.cmtoX;
				}
			}
			this.cmRun = this.cmRun * 9 / 10;
			bool flag4 = this.cmRun < 100 && this.cmRun > -100;
			if (flag4)
			{
				this.cmRun = 0;
			}
		}
		bool flag5 = Menu.cmx != Menu.cmtoX && !this.pointerIsDowning;
		if (flag5)
		{
			this.cmvx = Menu.cmtoX - Menu.cmx << 2;
			this.cmdx += this.cmvx;
			Menu.cmx += this.cmdx >> 4;
			this.cmdx &= 15;
		}
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x000596F8 File Offset: 0x000578F8
	public void paintMenu(mGraphics g)
	{
		bool flag = GameScr.gI().activeRongThan && GameScr.gI().isUseFreez;
		if (!flag)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			g.translate(-Menu.cmx, 0);
			for (int i = 0; i < this.menuItems.size(); i++)
			{
				bool flag2 = i == this.menuSelectedItem;
				if (flag2)
				{
					g.drawImage(Menu.imgMenu2, this.menuX + i * this.menuW + 1, Menu.menuTemY[i], 0);
				}
				else
				{
					g.drawImage(Menu.imgMenu1, this.menuX + i * this.menuW + 1, Menu.menuTemY[i], 0);
				}
				string[] array = ((Command)this.menuItems.elementAt(i)).subCaption;
				bool flag3 = array == null;
				if (flag3)
				{
					array = new string[]
					{
						((Command)this.menuItems.elementAt(i)).caption
					};
				}
				int num = Menu.menuTemY[i] + (this.menuH - array.Length * 14) / 2 + 1;
				for (int j = 0; j < array.Length; j++)
				{
					bool flag4 = i == this.menuSelectedItem;
					if (flag4)
					{
						mFont.tahoma_7b_green2.drawString(g, array[j], this.menuX + i * this.menuW + this.menuW / 2, num + j * 14, 2);
					}
					else
					{
						mFont.tahoma_7b_dark.drawString(g, array[j], this.menuX + i * this.menuW + this.menuW / 2, num + j * 14, 2);
					}
				}
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
		}
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x000598E8 File Offset: 0x00057AE8
	public void doCloseMenu()
	{
		Res.outz("CLOSE MENU");
		this.isClose = false;
		this.showMenu = false;
		InfoDlg.hide();
		bool flag = this.close;
		if (flag)
		{
			GameCanvas.panel.cp = null;
			global::Char.chatPopup = null;
			bool flag2 = GameCanvas.panel2 != null && GameCanvas.panel2.cp != null;
			if (flag2)
			{
				GameCanvas.panel2.cp = null;
			}
		}
		else
		{
			bool flag3 = !this.touch;
			if (!flag3)
			{
				GameCanvas.panel.cp = null;
				bool flag4 = GameCanvas.panel2 != null && GameCanvas.panel2.cp != null;
				if (flag4)
				{
					GameCanvas.panel2.cp = null;
				}
				bool flag5 = this.menuSelectedItem >= 0;
				if (flag5)
				{
					Command command = (Command)this.menuItems.elementAt(this.menuSelectedItem);
					bool flag6 = command != null;
					if (flag6)
					{
						SoundMn.gI().buttonClose();
						command.performAction();
					}
				}
			}
		}
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x000599F4 File Offset: 0x00057BF4
	public void performSelect()
	{
		InfoDlg.hide();
		bool flag = this.menuSelectedItem >= 0;
		if (flag)
		{
			Command command = (Command)this.menuItems.elementAt(this.menuSelectedItem);
			if (command != null)
			{
				command.performAction();
			}
		}
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x00059A3C File Offset: 0x00057C3C
	public void updateMenu()
	{
		this.moveCamera();
		bool flag = !this.isClose;
		if (flag)
		{
			this.tDelay++;
			for (int i = 0; i < Menu.menuTemY.Length; i++)
			{
				bool flag2 = Menu.menuTemY[i] > this.menuY;
				if (flag2)
				{
					int num = Menu.menuTemY[i] - this.menuY >> 1;
					bool flag3 = num < 1;
					if (flag3)
					{
						num = 1;
					}
					bool flag4 = this.tDelay > i;
					if (flag4)
					{
						Menu.menuTemY[i] -= num;
					}
				}
			}
			bool flag5 = Menu.menuTemY[Menu.menuTemY.Length - 1] <= this.menuY;
			if (flag5)
			{
				this.tDelay = 0;
			}
		}
		else
		{
			this.tDelay++;
			for (int j = 0; j < Menu.menuTemY.Length; j++)
			{
				bool flag6 = Menu.menuTemY[j] < GameCanvas.h;
				if (flag6)
				{
					int num2 = (GameCanvas.h - Menu.menuTemY[j] >> 1) + 2;
					bool flag7 = num2 < 1;
					if (flag7)
					{
						num2 = 1;
					}
					bool flag8 = this.tDelay > j;
					if (flag8)
					{
						Menu.menuTemY[j] += num2;
					}
				}
			}
			bool flag9 = Menu.menuTemY[Menu.menuTemY.Length - 1] >= GameCanvas.h;
			if (flag9)
			{
				this.tDelay = 0;
				this.doCloseMenu();
			}
		}
		bool flag10 = Menu.xc != 0;
		if (flag10)
		{
			Menu.xc >>= 1;
			bool flag11 = Menu.xc < 0;
			if (flag11)
			{
				Menu.xc = 0;
			}
		}
		bool flag12 = this.isScrolling() || this.waitToPerform <= 0;
		if (!flag12)
		{
			this.waitToPerform--;
			bool flag13 = this.waitToPerform == 0;
			if (flag13)
			{
				bool flag14 = this.menuSelectedItem >= 0 && !this.isNotClose[this.menuSelectedItem];
				if (flag14)
				{
					this.isClose = true;
					this.touch = true;
					GameCanvas.panel.cp = null;
				}
				else
				{
					this.performSelect();
				}
			}
		}
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void perform(int idAction, object p)
	{
	}

	// Token: 0x04000A19 RID: 2585
	public bool showMenu;

	// Token: 0x04000A1A RID: 2586
	public MyVector menuItems;

	// Token: 0x04000A1B RID: 2587
	public int menuSelectedItem;

	// Token: 0x04000A1C RID: 2588
	public int menuX;

	// Token: 0x04000A1D RID: 2589
	public int menuY;

	// Token: 0x04000A1E RID: 2590
	public int menuW;

	// Token: 0x04000A1F RID: 2591
	public int menuH;

	// Token: 0x04000A20 RID: 2592
	public static int[] menuTemY;

	// Token: 0x04000A21 RID: 2593
	public static int cmtoX;

	// Token: 0x04000A22 RID: 2594
	public static int cmx;

	// Token: 0x04000A23 RID: 2595
	public static int cmdy;

	// Token: 0x04000A24 RID: 2596
	public static int cmvy;

	// Token: 0x04000A25 RID: 2597
	public static int cmxLim;

	// Token: 0x04000A26 RID: 2598
	public static int xc;

	// Token: 0x04000A27 RID: 2599
	private Command left = new Command(mResources.SELECT, 0);

	// Token: 0x04000A28 RID: 2600
	private Command right = new Command(mResources.CLOSE, 0, GameCanvas.w - 71, GameCanvas.h - mScreen.cmdH + 1);

	// Token: 0x04000A29 RID: 2601
	private Command center;

	// Token: 0x04000A2A RID: 2602
	public static Image imgMenu1;

	// Token: 0x04000A2B RID: 2603
	public static Image imgMenu2;

	// Token: 0x04000A2C RID: 2604
	private bool disableClose;

	// Token: 0x04000A2D RID: 2605
	public int tDelay;

	// Token: 0x04000A2E RID: 2606
	public int w;

	// Token: 0x04000A2F RID: 2607
	private int pa;

	// Token: 0x04000A30 RID: 2608
	private bool trans;

	// Token: 0x04000A31 RID: 2609
	private int pointerDownTime;

	// Token: 0x04000A32 RID: 2610
	private int pointerDownFirstX;

	// Token: 0x04000A33 RID: 2611
	private int[] pointerDownLastX = new int[3];

	// Token: 0x04000A34 RID: 2612
	private bool pointerIsDowning;

	// Token: 0x04000A35 RID: 2613
	private bool isDownWhenRunning;

	// Token: 0x04000A36 RID: 2614
	private bool wantUpdateList;

	// Token: 0x04000A37 RID: 2615
	private int waitToPerform;

	// Token: 0x04000A38 RID: 2616
	private int cmRun;

	// Token: 0x04000A39 RID: 2617
	private bool touch;

	// Token: 0x04000A3A RID: 2618
	private bool close;

	// Token: 0x04000A3B RID: 2619
	private int cmvx;

	// Token: 0x04000A3C RID: 2620
	private int cmdx;

	// Token: 0x04000A3D RID: 2621
	private bool isClose;

	// Token: 0x04000A3E RID: 2622
	public bool[] isNotClose;
}
