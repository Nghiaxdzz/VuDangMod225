using System;

// Token: 0x02000010 RID: 16
public class ChatPopup : Effect2, IActionListener
{
	// Token: 0x0600013F RID: 319 RVA: 0x0001C6AC File Offset: 0x0001A8AC
	public static void addNextPopUpMultiLine(string strNext, Npc next)
	{
		ChatPopup.nextMultiChatPopUp = strNext;
		ChatPopup.nextChar = next;
		bool flag = ChatPopup.currChatPopup == null;
		if (flag)
		{
			ChatPopup.addChatPopupMultiLine(ChatPopup.nextMultiChatPopUp, 100000, ChatPopup.nextChar);
			ChatPopup.nextMultiChatPopUp = null;
			ChatPopup.nextChar = null;
		}
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0001C6F8 File Offset: 0x0001A8F8
	public static void addBigMessage(string chat, int howLong, Npc c)
	{
		string[] array = new string[]
		{
			chat
		};
		bool flag = c.charID != 5 && GameScr.info1.isDone;
		if (flag)
		{
			GameScr.info1.isUpdate = false;
		}
		global::Char.isLockKey = true;
		ChatPopup.serverChatPopUp = ChatPopup.addChatPopup(array[0], howLong, c);
		ChatPopup.serverChatPopUp.strY = 5;
		ChatPopup.serverChatPopUp.cx = GameCanvas.w / 2 - ChatPopup.serverChatPopUp.sayWidth / 2 - 1;
		ChatPopup.serverChatPopUp.cy = GameCanvas.h - 20 - ChatPopup.serverChatPopUp.ch;
		ChatPopup.serverChatPopUp.currentLine = 0;
		ChatPopup.serverChatPopUp.lines = array;
		ChatPopup.scr = new Scroll();
		int nItem = ChatPopup.serverChatPopUp.says.Length;
		ChatPopup.scr.setStyle(nItem, 12, ChatPopup.serverChatPopUp.cx, ChatPopup.serverChatPopUp.cy - ChatPopup.serverChatPopUp.strY + 12, ChatPopup.serverChatPopUp.sayWidth + 2, ChatPopup.serverChatPopUp.ch - 25, true, 1);
		SoundMn.gI().openDialog();
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0001C818 File Offset: 0x0001AA18
	public static void addChatPopupMultiLine(string chat, int howLong, Npc c)
	{
		string[] array = Res.split(chat, "\n", 0);
		global::Char.isLockKey = true;
		ChatPopup.currChatPopup = ChatPopup.addChatPopup(array[0], howLong, c);
		ChatPopup.currChatPopup.currentLine = 0;
		ChatPopup.currChatPopup.lines = array;
		string caption = mResources.CONTINUE;
		bool flag = array.Length == 1;
		if (flag)
		{
			caption = mResources.CLOSE;
		}
		ChatPopup.currChatPopup.cmdNextLine = new Command(caption, ChatPopup.currChatPopup, 8000, null);
		ChatPopup.currChatPopup.cmdNextLine.x = GameCanvas.w / 2 - 35;
		ChatPopup.currChatPopup.cmdNextLine.y = GameCanvas.h - 35;
		SoundMn.gI().openDialog();
	}

	// Token: 0x06000142 RID: 322 RVA: 0x0001C8CC File Offset: 0x0001AACC
	public static ChatPopup addChatPopupWithIcon(string chat, int howLong, Npc c, int idIcon)
	{
		ChatPopup.performDelay = 10;
		ChatPopup chatPopup = new ChatPopup();
		chatPopup.sayWidth = GameCanvas.w - 30 - (GameCanvas.menu.showMenu ? GameCanvas.menu.menuX : 0);
		bool flag = chatPopup.sayWidth > 320;
		if (flag)
		{
			chatPopup.sayWidth = 320;
		}
		bool flag2 = chat.Length < 10;
		if (flag2)
		{
			chatPopup.sayWidth = 64;
		}
		bool flag3 = GameCanvas.w == 128;
		if (flag3)
		{
			chatPopup.sayWidth = 128;
		}
		chatPopup.says = mFont.tahoma_7_red.splitFontArray(chat, chatPopup.sayWidth - 10);
		chatPopup.delay = howLong;
		chatPopup.c = c;
		chatPopup.iconID = idIcon;
		global::Char.chatPopup = chatPopup;
		chatPopup.ch = 15 - chatPopup.sayRun + chatPopup.says.Length * 12 + 10;
		bool flag4 = chatPopup.ch > GameCanvas.h - 80;
		if (flag4)
		{
			chatPopup.ch = GameCanvas.h - 80;
		}
		chatPopup.mH = 10;
		bool showMenu = GameCanvas.menu.showMenu;
		if (showMenu)
		{
			chatPopup.mH = 0;
		}
		Effect2.vEffect2.addElement(chatPopup);
		ChatPopup.isHavePetNpc = false;
		bool flag5 = c != null && c.charID == 5;
		if (flag5)
		{
			ChatPopup.isHavePetNpc = true;
			GameScr.info1.addInfo(string.Empty, 1);
		}
		ChatPopup.curr = (ChatPopup.last = mSystem.currentTimeMillis());
		chatPopup.ch += 15;
		return chatPopup;
	}

	// Token: 0x06000143 RID: 323 RVA: 0x0001CA64 File Offset: 0x0001AC64
	public static ChatPopup addChatPopup(string chat, int howLong, Npc c)
	{
		ChatPopup.performDelay = 10;
		ChatPopup chatPopup = new ChatPopup();
		chatPopup.sayWidth = GameCanvas.w - 30 - (GameCanvas.menu.showMenu ? GameCanvas.menu.menuX : 0);
		bool flag = chatPopup.sayWidth > 320;
		if (flag)
		{
			chatPopup.sayWidth = 320;
		}
		bool flag2 = chat.Length < 10;
		if (flag2)
		{
			chatPopup.sayWidth = 64;
		}
		bool flag3 = GameCanvas.w == 128;
		if (flag3)
		{
			chatPopup.sayWidth = 128;
		}
		chatPopup.says = mFont.tahoma_7_red.splitFontArray(chat, chatPopup.sayWidth - 10);
		chatPopup.delay = howLong;
		chatPopup.c = c;
		global::Char.chatPopup = chatPopup;
		chatPopup.ch = 15 - chatPopup.sayRun + chatPopup.says.Length * 12 + 10;
		bool flag4 = chatPopup.ch > GameCanvas.h - 80;
		if (flag4)
		{
			chatPopup.ch = GameCanvas.h - 80;
		}
		chatPopup.mH = 10;
		bool showMenu = GameCanvas.menu.showMenu;
		if (showMenu)
		{
			chatPopup.mH = 0;
		}
		Effect2.vEffect2.addElement(chatPopup);
		ChatPopup.isHavePetNpc = false;
		bool flag5 = c != null && c.charID == 5;
		if (flag5)
		{
			ChatPopup.isHavePetNpc = true;
			GameScr.info1.addInfo(string.Empty, 1);
		}
		ChatPopup.curr = (ChatPopup.last = mSystem.currentTimeMillis());
		return chatPopup;
	}

	// Token: 0x06000144 RID: 324 RVA: 0x0001CBE4 File Offset: 0x0001ADE4
	public override void update()
	{
		bool flag = ChatPopup.scr != null;
		if (flag)
		{
			GameScr.info1.isUpdate = false;
			ChatPopup.scr.updatecm();
		}
		else
		{
			GameScr.info1.isUpdate = true;
		}
		bool showMenu = GameCanvas.menu.showMenu;
		if (showMenu)
		{
			this.strY = 0;
			this.cx = GameCanvas.w / 2 - this.sayWidth / 2 - 1;
			this.cy = GameCanvas.menu.menuY - this.ch;
		}
		else
		{
			this.strY = 0;
			bool flag2 = GameScr.gI().right != null || GameScr.gI().left != null || GameScr.gI().center != null || this.cmdNextLine != null || this.cmdMsg1 != null;
			if (flag2)
			{
				this.strY = 5;
				this.cx = GameCanvas.w / 2 - this.sayWidth / 2 - 1;
				this.cy = GameCanvas.h - 20 - this.ch;
			}
			else
			{
				this.cx = GameCanvas.w / 2 - this.sayWidth / 2 - 1;
				this.cy = GameCanvas.h - 5 - this.ch;
			}
		}
		bool flag3 = this.delay > 0;
		if (flag3)
		{
			this.delay--;
		}
		bool flag4 = ChatPopup.performDelay > 0;
		if (flag4)
		{
			ChatPopup.performDelay--;
		}
		bool flag5 = this.sayRun > 1;
		if (flag5)
		{
			this.sayRun--;
		}
		bool flag6 = (this.c != null && global::Char.chatPopup != null && global::Char.chatPopup != this) || (this.c != null && global::Char.chatPopup == null) || this.delay == 0;
		if (flag6)
		{
			Effect2.vEffect2Outside.removeElement(this);
			Effect2.vEffect2.removeElement(this);
		}
	}

	// Token: 0x06000145 RID: 325 RVA: 0x0001CDC8 File Offset: 0x0001AFC8
	public override void paint(mGraphics g)
	{
		bool flag = GameScr.gI().activeRongThan && GameScr.gI().isUseFreez;
		if (!flag)
		{
			GameCanvas.resetTrans(g);
			int num = this.cx;
			int num2 = this.cy;
			int num3 = this.sayWidth + 2;
			int num4 = this.ch;
			bool flag2 = (num <= 0 || num2 <= 0) && !GameCanvas.panel.isShow;
			if (!flag2)
			{
				PopUp.paintPopUp(g, num, num2, num3, num4, 16777215, false);
				bool flag3 = this.c != null;
				if (flag3)
				{
					SmallImage.drawSmallImage(g, this.c.avatar, this.cx + 14, this.cy, 0, StaticObj.BOTTOM_LEFT);
				}
				bool flag4 = this.iconID != 0;
				if (flag4)
				{
					SmallImage.drawSmallImage(g, this.iconID, this.cx + num3 / 2, this.cy + this.ch - 15, 0, StaticObj.VCENTER_HCENTER);
				}
				bool flag5 = ChatPopup.scr != null;
				if (flag5)
				{
					g.setClip(num, num2, num3, num4 - 16);
					g.translate(0, -ChatPopup.scr.cmy);
				}
				int tx = 0;
				int ty = 0;
				bool flag6 = this.isClip;
				if (flag6)
				{
					tx = g.getTranslateX();
					ty = g.getTranslateY();
					g.setClip(num, num2 + 1, num3, num4 - 17);
					g.translate(0, -ChatPopup.cmyText);
				}
				int num5 = -1;
				for (int i = 0; i < this.says.Length; i++)
				{
					bool flag7 = this.says[i].StartsWith("--");
					if (flag7)
					{
						g.setColor(0);
						g.fillRect(num + 10, this.cy + this.sayRun + i * 12 + 6, num3 - 20, 1);
					}
					else
					{
						mFont mFont = mFont.tahoma_7;
						int num6 = 2;
						string st = this.says[i];
						bool flag8 = this.says[i].StartsWith("|");
						int num7;
						if (flag8)
						{
							string[] array = Res.split(this.says[i], "|", 0);
							bool flag9 = array.Length == 3;
							if (flag9)
							{
								st = array[2];
							}
							bool flag10 = array.Length == 4;
							if (flag10)
							{
								st = array[3];
								num6 = int.Parse(array[2]);
							}
							num7 = int.Parse(array[1]);
							num5 = num7;
						}
						else
						{
							num7 = num5;
						}
						switch (num7)
						{
						case -1:
							mFont = mFont.tahoma_7;
							break;
						case 0:
							mFont = mFont.tahoma_7b_dark;
							break;
						case 1:
							mFont = mFont.tahoma_7b_green;
							break;
						case 2:
							mFont = mFont.tahoma_7b_blue;
							break;
						case 3:
							mFont = mFont.tahoma_7_red;
							break;
						case 4:
							mFont = mFont.tahoma_7_green;
							break;
						case 5:
							mFont = mFont.tahoma_7_blue;
							break;
						case 7:
							mFont = mFont.tahoma_7b_red;
							break;
						case 8:
							mFont = mFont.tahoma_7b_yellow;
							break;
						}
						bool flag11 = this.says[i].StartsWith("<");
						if (flag11)
						{
							string[] array2 = Res.split(this.says[i], "<", 0);
							string[] array3 = Res.split(array2[1], ">", 1);
							bool flag12 = this.second == 0;
							if (flag12)
							{
								this.second = int.Parse(array3[1]);
							}
							else
							{
								ChatPopup.curr = mSystem.currentTimeMillis();
								bool flag13 = ChatPopup.curr - ChatPopup.last >= 1000L;
								if (flag13)
								{
									ChatPopup.last = ChatPopup.curr;
									this.second--;
								}
							}
							st = this.second + " " + array3[2];
							mFont.drawString(g, st, this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY + 12, num6);
						}
						else
						{
							bool flag14 = num6 == 2;
							if (flag14)
							{
								mFont.drawString(g, st, this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY + 12, num6);
							}
							bool flag15 = num6 == 1;
							if (flag15)
							{
								mFont.drawString(g, st, this.cx + this.sayWidth - 5, this.cy + this.sayRun + i * 12 - this.strY + 12, num6);
							}
						}
					}
				}
				bool flag16 = this.isClip;
				if (flag16)
				{
					GameCanvas.resetTrans(g);
					g.translate(tx, ty);
				}
				bool flag17 = this.maxStarSlot > 4;
				if (flag17)
				{
					this.nMaxslot_tren = (int)((this.maxStarSlot + 1) / 2);
					this.nMaxslot_duoi = (int)this.maxStarSlot - this.nMaxslot_tren;
					for (int j = 0; j < this.nMaxslot_tren; j++)
					{
						g.drawImage(Panel.imgMaxStar, num + num3 / 2 - this.nMaxslot_tren * 20 / 2 + j * 20 + mGraphics.getImageWidth(Panel.imgMaxStar), num2 + num4 - 17, 3);
					}
					for (int k = 0; k < this.nMaxslot_duoi; k++)
					{
						g.drawImage(Panel.imgMaxStar, num + num3 / 2 - this.nMaxslot_duoi * 20 / 2 + k * 20 + mGraphics.getImageWidth(Panel.imgMaxStar), num2 + num4 - 8, 3);
					}
					bool flag18 = this.starSlot > 0;
					if (flag18)
					{
						this.imgStar = Panel.imgStar;
						bool flag19 = (int)this.starSlot >= this.nMaxslot_tren;
						if (flag19)
						{
							this.nslot_duoi = (int)this.starSlot - this.nMaxslot_tren;
							for (int l = 0; l < this.nMaxslot_tren; l++)
							{
								g.drawImage(this.imgStar, num + num3 / 2 - this.nMaxslot_tren * 20 / 2 + l * 20 + mGraphics.getImageWidth(this.imgStar), num2 + num4 - 17, 3);
							}
							for (int m = 0; m < this.nslot_duoi; m++)
							{
								bool flag20 = m + this.nMaxslot_tren >= ChatPopup.numSlot;
								if (flag20)
								{
									this.imgStar = Panel.imgStar8;
								}
								g.drawImage(this.imgStar, num + num3 / 2 - this.nMaxslot_duoi * 20 / 2 + m * 20 + mGraphics.getImageWidth(this.imgStar), num2 + num4 - 8, 3);
							}
						}
						else
						{
							for (int n = 0; n < (int)this.starSlot; n++)
							{
								g.drawImage(this.imgStar, num + num3 / 2 - this.nMaxslot_tren * 20 / 2 + n * 20 + mGraphics.getImageWidth(this.imgStar), num2 + num4 - 17, 3);
							}
						}
					}
				}
				else
				{
					for (int num8 = 0; num8 < (int)this.maxStarSlot; num8++)
					{
						g.drawImage(Panel.imgMaxStar, num + num3 / 2 - (int)(this.maxStarSlot * 20 / 2) + num8 * 20 + mGraphics.getImageWidth(Panel.imgMaxStar), num2 + num4 - 13, 3);
					}
					bool flag21 = this.starSlot > 0;
					if (flag21)
					{
						for (int num9 = 0; num9 < (int)this.starSlot; num9++)
						{
							g.drawImage(Panel.imgStar, num + num3 / 2 - (int)(this.maxStarSlot * 20 / 2) + num9 * 20 + mGraphics.getImageWidth(Panel.imgStar), num2 + num4 - 13, 3);
						}
					}
				}
				this.paintCmd(g);
			}
		}
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0001D590 File Offset: 0x0001B790
	public void paintRada(mGraphics g, int cmyText)
	{
		int num = this.cx;
		int num2 = this.cy;
		int num3 = this.sayWidth;
		int num4 = this.ch;
		int translateX = g.getTranslateX();
		int translateY = g.getTranslateY();
		g.translate(0, -cmyText);
		bool flag = (num <= 0 || num2 <= 0) && !GameCanvas.panel.isShow;
		if (!flag)
		{
			int num5 = -1;
			for (int i = 0; i < this.says.Length; i++)
			{
				bool flag2 = this.says[i].StartsWith("--");
				if (flag2)
				{
					g.setColor(16777215);
					g.fillRect(num + 10, this.cy + this.sayRun + i * 12 - 6, num3 - 20, 1);
				}
				else
				{
					mFont mFont = mFont.tahoma_7_white;
					int num6 = 2;
					string st = this.says[i];
					bool flag3 = this.says[i].StartsWith("|");
					int num7;
					if (flag3)
					{
						string[] array = Res.split(this.says[i], "|", 0);
						bool flag4 = array.Length == 3;
						if (flag4)
						{
							st = array[2];
						}
						bool flag5 = array.Length == 4;
						if (flag5)
						{
							st = array[3];
							num6 = int.Parse(array[2]);
						}
						num7 = int.Parse(array[1]);
						num5 = num7;
					}
					else
					{
						num7 = num5;
					}
					switch (num7)
					{
					case -1:
						mFont = mFont.tahoma_7_white;
						break;
					case 0:
						mFont = mFont.tahoma_7b_white;
						break;
					case 1:
						mFont = mFont.tahoma_7b_green;
						break;
					case 2:
						mFont = mFont.tahoma_7b_red;
						break;
					}
					bool flag6 = this.says[i].StartsWith("<");
					if (flag6)
					{
						string[] array2 = Res.split(this.says[i], "<", 0);
						string[] array3 = Res.split(array2[1], ">", 1);
						bool flag7 = this.second == 0;
						if (flag7)
						{
							this.second = int.Parse(array3[1]);
						}
						else
						{
							ChatPopup.curr = mSystem.currentTimeMillis();
							bool flag8 = ChatPopup.curr - ChatPopup.last >= 1000L;
							if (flag8)
							{
								ChatPopup.last = ChatPopup.curr;
								this.second--;
							}
						}
						st = this.second + " " + array3[2];
						mFont.drawString(g, st, this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY, num6);
					}
					else
					{
						bool flag9 = num6 == 2;
						if (flag9)
						{
							mFont.drawString(g, st, this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY, num6);
						}
						bool flag10 = num6 == 1;
						if (flag10)
						{
							mFont.drawString(g, st, this.cx + this.sayWidth - 5, this.cy + this.sayRun + i * 12 - this.strY, num6);
						}
					}
				}
			}
			GameCanvas.resetTrans(g);
			g.translate(translateX, translateY);
		}
	}

	// Token: 0x06000147 RID: 327 RVA: 0x0001D8E8 File Offset: 0x0001BAE8
	private void doKeyText(int type)
	{
		ChatPopup.cmyText += 12 * type;
		bool flag = ChatPopup.cmyText < 0;
		if (flag)
		{
			ChatPopup.cmyText = 0;
		}
		bool flag2 = ChatPopup.cmyText > this.lim;
		if (flag2)
		{
			ChatPopup.cmyText = this.lim;
		}
	}

	// Token: 0x06000148 RID: 328 RVA: 0x0001D938 File Offset: 0x0001BB38
	public void updateKey()
	{
		bool flag = this.isClip;
		if (flag)
		{
			bool flag2 = GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22];
			if (flag2)
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
				this.doKeyText(1);
			}
			bool flag3 = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21];
			if (flag3)
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = false;
				this.doKeyText(-1);
			}
			bool flag4 = GameCanvas.isPointerHoldIn(this.cx, 0, this.sayWidth + 2, this.ch);
			if (flag4)
			{
				bool isPointerMove = GameCanvas.isPointerMove;
				if (isPointerMove)
				{
					bool flag5 = this.pyy == 0;
					if (flag5)
					{
						this.pyy = GameCanvas.py;
					}
					this.pxx = this.pyy - GameCanvas.py;
					bool flag6 = this.pxx != 0;
					if (flag6)
					{
						ChatPopup.cmyText += this.pxx;
						this.pyy = GameCanvas.py;
					}
					bool flag7 = ChatPopup.cmyText < 0;
					if (flag7)
					{
						ChatPopup.cmyText = 0;
					}
					bool flag8 = ChatPopup.cmyText > this.lim;
					if (flag8)
					{
						ChatPopup.cmyText = this.lim;
					}
				}
				else
				{
					this.pyy = 0;
					this.pyy = 0;
				}
			}
		}
		bool flag9 = ChatPopup.scr != null;
		if (flag9)
		{
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				ChatPopup.scr.updateKey();
			}
			bool flag10 = GameCanvas.keyHold[(!Main.isPC) ? 2 : 21];
			if (flag10)
			{
				ChatPopup.scr.cmtoY -= 12;
				bool flag11 = ChatPopup.scr.cmtoY < 0;
				if (flag11)
				{
					ChatPopup.scr.cmtoY = 0;
				}
			}
			bool flag12 = GameCanvas.keyHold[(!Main.isPC) ? 8 : 22];
			if (flag12)
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
				ChatPopup.scr.cmtoY += 12;
				bool flag13 = ChatPopup.scr.cmtoY > ChatPopup.scr.cmyLim;
				if (flag13)
				{
					ChatPopup.scr.cmtoY = ChatPopup.scr.cmyLim;
				}
			}
		}
		bool flag14 = GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center);
		if (flag14)
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			mScreen.keyTouch = -1;
			bool flag15 = this.cmdNextLine != null;
			if (flag15)
			{
				this.cmdNextLine.performAction();
			}
			else
			{
				bool flag16 = this.cmdMsg1 != null;
				if (flag16)
				{
					this.cmdMsg1.performAction();
				}
				else
				{
					bool flag17 = this.cmdMsg2 != null;
					if (flag17)
					{
						this.cmdMsg2.performAction();
					}
				}
			}
		}
		bool flag18 = ChatPopup.scr == null || !ChatPopup.scr.pointerIsDowning;
		if (flag18)
		{
			bool flag19 = this.cmdMsg1 != null && (GameCanvas.keyPressed[12] || GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.cmdMsg1));
			if (flag19)
			{
				GameCanvas.keyPressed[12] = false;
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				GameCanvas.isPointerClick = false;
				GameCanvas.isPointerJustRelease = false;
				this.cmdMsg1.performAction();
				mScreen.keyTouch = -1;
			}
			bool flag20 = this.cmdMsg2 != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.cmdMsg2));
			if (flag20)
			{
				GameCanvas.keyPressed[13] = false;
				GameCanvas.isPointerClick = false;
				GameCanvas.isPointerJustRelease = false;
				this.cmdMsg2.performAction();
				mScreen.keyTouch = -1;
			}
		}
	}

	// Token: 0x06000149 RID: 329 RVA: 0x0001DD0C File Offset: 0x0001BF0C
	public void paintCmd(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		GameCanvas.paintz.paintTabSoft(g);
		bool flag = this.cmdNextLine != null;
		if (flag)
		{
			GameCanvas.paintz.paintCmdBar(g, null, this.cmdNextLine, null);
		}
		bool flag2 = this.cmdMsg1 != null;
		if (flag2)
		{
			GameCanvas.paintz.paintCmdBar(g, this.cmdMsg1, null, this.cmdMsg2);
		}
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0001DD9C File Offset: 0x0001BF9C
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 1000;
		if (flag)
		{
			try
			{
				GameMidlet.instance.platformRequest((string)p);
			}
			catch (Exception)
			{
			}
			bool flag2 = !Main.isPC;
			if (flag2)
			{
				GameMidlet.instance.notifyDestroyed();
			}
			else
			{
				idAction = 1001;
			}
			GameCanvas.endDlg();
		}
		bool flag3 = idAction == 1001;
		if (flag3)
		{
			ChatPopup.scr = null;
			global::Char.chatPopup = null;
			ChatPopup.serverChatPopUp = null;
			GameScr.info1.isUpdate = true;
			global::Char.isLockKey = false;
			bool flag4 = ChatPopup.isHavePetNpc;
			if (flag4)
			{
				GameScr.info1.info.time = 0;
				GameScr.info1.info.info.speed = 10;
			}
		}
		bool flag5 = idAction != 8000 || ChatPopup.performDelay > 0;
		if (!flag5)
		{
			int num = ChatPopup.currChatPopup.currentLine;
			num++;
			bool flag6 = num >= ChatPopup.currChatPopup.lines.Length;
			if (flag6)
			{
				global::Char.chatPopup = null;
				ChatPopup.currChatPopup = null;
				GameScr.info1.isUpdate = true;
				global::Char.isLockKey = false;
				bool flag7 = ChatPopup.nextMultiChatPopUp != null;
				if (flag7)
				{
					ChatPopup.addChatPopupMultiLine(ChatPopup.nextMultiChatPopUp, 100000, ChatPopup.nextChar);
					ChatPopup.nextMultiChatPopUp = null;
					ChatPopup.nextChar = null;
				}
				else
				{
					bool flag8 = !ChatPopup.isHavePetNpc;
					if (!flag8)
					{
						GameScr.info1.info.time = 0;
						for (int i = 0; i < GameScr.info1.info.infoWaitToShow.size(); i++)
						{
							bool flag9 = ((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed == 10000000;
							if (flag9)
							{
								((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed = 10;
							}
						}
					}
				}
			}
			else
			{
				ChatPopup chatPopup = ChatPopup.addChatPopup(ChatPopup.currChatPopup.lines[num], ChatPopup.currChatPopup.delay, ChatPopup.currChatPopup.c);
				chatPopup.currentLine = num;
				chatPopup.lines = ChatPopup.currChatPopup.lines;
				chatPopup.cmdNextLine = ChatPopup.currChatPopup.cmdNextLine;
				ChatPopup.currChatPopup = chatPopup;
			}
		}
	}

	// Token: 0x04000289 RID: 649
	public int sayWidth = 100;

	// Token: 0x0400028A RID: 650
	public int delay;

	// Token: 0x0400028B RID: 651
	public int sayRun;

	// Token: 0x0400028C RID: 652
	public string[] says;

	// Token: 0x0400028D RID: 653
	public int cx;

	// Token: 0x0400028E RID: 654
	public int cy;

	// Token: 0x0400028F RID: 655
	public int ch;

	// Token: 0x04000290 RID: 656
	public int cmx;

	// Token: 0x04000291 RID: 657
	public int cmy;

	// Token: 0x04000292 RID: 658
	public int lim;

	// Token: 0x04000293 RID: 659
	public Npc c;

	// Token: 0x04000294 RID: 660
	private bool outSide;

	// Token: 0x04000295 RID: 661
	public static long curr;

	// Token: 0x04000296 RID: 662
	public static long last;

	// Token: 0x04000297 RID: 663
	private int currentLine;

	// Token: 0x04000298 RID: 664
	private string[] lines;

	// Token: 0x04000299 RID: 665
	public Command cmdNextLine;

	// Token: 0x0400029A RID: 666
	public Command cmdMsg1;

	// Token: 0x0400029B RID: 667
	public Command cmdMsg2;

	// Token: 0x0400029C RID: 668
	public static ChatPopup currChatPopup;

	// Token: 0x0400029D RID: 669
	public static ChatPopup serverChatPopUp;

	// Token: 0x0400029E RID: 670
	public static string nextMultiChatPopUp;

	// Token: 0x0400029F RID: 671
	public static Npc nextChar;

	// Token: 0x040002A0 RID: 672
	public bool isShopDetail;

	// Token: 0x040002A1 RID: 673
	public sbyte starSlot;

	// Token: 0x040002A2 RID: 674
	public sbyte maxStarSlot;

	// Token: 0x040002A3 RID: 675
	public static Scroll scr;

	// Token: 0x040002A4 RID: 676
	public static bool isHavePetNpc;

	// Token: 0x040002A5 RID: 677
	public int mH;

	// Token: 0x040002A6 RID: 678
	public static int performDelay;

	// Token: 0x040002A7 RID: 679
	public int dx;

	// Token: 0x040002A8 RID: 680
	public int dy;

	// Token: 0x040002A9 RID: 681
	public int second;

	// Token: 0x040002AA RID: 682
	public static int numSlot = 7;

	// Token: 0x040002AB RID: 683
	private int nMaxslot_duoi;

	// Token: 0x040002AC RID: 684
	private int nMaxslot_tren;

	// Token: 0x040002AD RID: 685
	private int nslot_duoi;

	// Token: 0x040002AE RID: 686
	private Image imgStar;

	// Token: 0x040002AF RID: 687
	public int strY;

	// Token: 0x040002B0 RID: 688
	private int iconID;

	// Token: 0x040002B1 RID: 689
	public bool isClip;

	// Token: 0x040002B2 RID: 690
	public static int cmyText;

	// Token: 0x040002B3 RID: 691
	private int pxx;

	// Token: 0x040002B4 RID: 692
	private int pyy;
}
