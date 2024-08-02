using System;

// Token: 0x02000043 RID: 67
public class Info : IActionListener
{
	// Token: 0x0600039B RID: 923 RVA: 0x0000485A File Offset: 0x00002A5A
	public void hide()
	{
		this.says = null;
		this.infoWaitToShow.removeAllElements();
	}

	// Token: 0x0600039C RID: 924 RVA: 0x000514E8 File Offset: 0x0004F6E8
	public void paint(mGraphics g, int x, int y, int dir)
	{
		bool flag = this.infoWaitToShow.size() == 0;
		if (!flag)
		{
			g.translate(x, y);
			bool flag2 = this.says != null && this.says.Length != 0 && this.type != 1;
			if (flag2)
			{
				bool flag3 = this.outSide;
				if (flag3)
				{
					this.cx -= GameScr.cmx;
					this.cy -= GameScr.cmy;
					this.cy += 35;
				}
				int num = (mGraphics.zoomLevel != 1) ? 10 : 0;
				bool flag4 = this.info.charInfo == null;
				if (flag4)
				{
					PopUp.paintPopUp(g, this.X, this.Y, this.W, this.H, 16777215, false);
				}
				else
				{
					mSystem.paintPopUp2(g, this.X - 23, this.Y - num / 2, this.W + 15, this.H + ((!GameCanvas.isTouch) ? 14 : 0) + num);
				}
				bool flag5 = this.info.charInfo == null;
				if (flag5)
				{
					g.drawRegion(Info.gocnhon, 0, 0, 9, 8, (dir != 1) ? 2 : 0, this.cx - 3 + ((dir != 1) ? 20 : -15), this.cy - this.ch - 20 + this.sayRun + 2, mGraphics.TOP | mGraphics.HCENTER);
				}
				int num2 = -1;
				for (int i = 0; i < this.says.Length; i++)
				{
					mFont mFont = mFont.tahoma_7;
					string text = this.says[i];
					bool flag6 = this.says[i].StartsWith("|");
					int num4;
					if (flag6)
					{
						string[] array = Res.split(this.says[i], "|", 0);
						bool flag7 = array.Length == 3;
						if (flag7)
						{
							text = array[2];
						}
						bool flag8 = array.Length == 4;
						if (flag8)
						{
							text = array[3];
							int num3 = int.Parse(array[2]);
						}
						num4 = int.Parse(array[1]);
						num2 = num4;
					}
					else
					{
						num4 = num2;
					}
					switch (num4)
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
					}
					bool flag9 = this.info.charInfo == null;
					if (flag9)
					{
						mFont.drawString(g, text, this.cx, this.cy - this.ch - 15 + this.sayRun + i * 12 - this.says.Length * 12 - 9, 2);
					}
					else
					{
						int num5 = this.X - 23;
						int num6 = this.Y - num / 2;
						int num7 = (mSystem.clientType != 1) ? (this.W + 25) : (this.W + 28);
						int num8 = this.H + ((!GameCanvas.isTouch) ? 14 : 0) + num;
						g.setColor(4465169);
						g.fillRect(num5, num6 + num8, num7, 2);
						int num9 = this.info.timeCount * num7 / this.info.maxTime;
						bool flag10 = num9 < 0;
						if (flag10)
						{
							num9 = 0;
						}
						g.setColor(43758);
						g.fillRect(num5, num6 + num8, num9, 2);
						bool flag11 = this.info.timeCount == 0;
						if (flag11)
						{
							return;
						}
						this.info.charInfo.paintHead(g, this.X + 5, this.Y + this.H / 2, 0);
						bool flag12 = mGraphics.zoomLevel == 1;
						if (flag12)
						{
							((!this.info.isChatServer) ? mFont.tahoma_7b_greenSmall : mFont.tahoma_7b_yellowSmall2).drawString(g, this.info.charInfo.cName, this.X + 12, this.Y + 3, 0);
						}
						else
						{
							((!this.info.isChatServer) ? mFont.tahoma_7b_greenSmall : mFont.tahoma_7b_yellowSmall2).drawString(g, this.info.charInfo.cName, this.X + 12, this.Y - 3, 0);
						}
						bool flag13 = !GameCanvas.isTouch;
						if (flag13)
						{
							bool flag14 = !TField.isQwerty;
							if (flag14)
							{
								mFont.tahoma_7b_green2Small.drawString(g, "Nhấn # để chat", this.X + this.W / 2 + 10, this.Y + this.H, mFont.CENTER);
							}
							else
							{
								mFont.tahoma_7b_green2Small.drawString(g, "Nhấn Y để chat", this.X + this.W / 2 + 10, this.Y + this.H, mFont.CENTER);
							}
						}
						bool flag15 = mGraphics.zoomLevel == 1;
						if (flag15)
						{
							TextInfo.paint(g, text, this.X + 14, this.Y + this.H / 2 + 2, this.W - 16, this.H, mFont.tahoma_7_whiteSmall);
						}
						else
						{
							string[] array2 = mFont.tahoma_7_whiteSmall.splitFontArray(text, 120);
							for (int j = 0; j < array2.Length; j++)
							{
								mFont.tahoma_7_whiteSmall.drawString(g, array2[j], this.X + 12, this.Y + 12 + j * 12 - 3, 0);
							}
							GameCanvas.resetTrans(g);
						}
					}
				}
				bool flag16 = this.info.charInfo == null;
				if (flag16)
				{
				}
			}
			g.translate(-x, -y);
		}
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00051AE4 File Offset: 0x0004FCE4
	public void update()
	{
		bool flag = this.infoWaitToShow.size() == 0 || this.info.timeCount != 0;
		if (!flag)
		{
			this.time++;
			bool flag2 = this.time >= this.info.speed;
			if (flag2)
			{
				this.time = 0;
				this.infoWaitToShow.removeElementAt(0);
				bool flag3 = this.infoWaitToShow.size() != 0;
				if (flag3)
				{
					InfoItem infoItem = this.info = (InfoItem)this.infoWaitToShow.firstElement();
					this.getInfo();
				}
			}
		}
	}

	// Token: 0x0600039E RID: 926 RVA: 0x00051B8C File Offset: 0x0004FD8C
	public void getInfo()
	{
		this.sayWidth = 100;
		bool flag = GameCanvas.w == 128;
		if (flag)
		{
			this.sayWidth = 128;
		}
		bool flag2 = this.info.charInfo != null;
		int num;
		if (flag2)
		{
			this.says = new string[]
			{
				this.info.s
			};
			bool flag3 = mGraphics.zoomLevel == 1;
			if (flag3)
			{
				num = this.says.Length;
			}
			else
			{
				string[] array = mFont.tahoma_7_whiteSmall.splitFontArray(this.info.s, 120);
				num = array.Length;
			}
		}
		else
		{
			this.says = mFont.tahoma_7.splitFontArray(this.info.s, this.sayWidth - 10);
			num = this.says.Length;
		}
		this.sayRun = 7;
		this.X = this.cx - this.sayWidth / 2 - 1;
		this.Y = this.cy - this.ch - 15 + this.sayRun - num * 12 - 15;
		this.W = this.sayWidth + 2 + ((this.info.charInfo != null) ? 30 : 0);
		this.H = (num + 1) * 12 + 1 + ((this.info.charInfo != null) ? 5 : 0);
	}

	// Token: 0x0600039F RID: 927 RVA: 0x00051CDC File Offset: 0x0004FEDC
	public void addInfo(string s, int Type, global::Char cInfo, bool isChatServer)
	{
		this.type = Type;
		bool flag = GameCanvas.w == 128;
		if (flag)
		{
			this.limLeft = 1;
		}
		bool flag2 = this.infoWaitToShow.size() > 10;
		if (flag2)
		{
			this.infoWaitToShow.removeElementAt(0);
		}
		bool flag3 = this.infoWaitToShow.size() > 0 && s.Equals(((InfoItem)this.infoWaitToShow.lastElement()).s);
		if (flag3)
		{
			Res.outz("return");
		}
		else
		{
			InfoItem infoItem = new InfoItem(s);
			bool flag4 = this.type == 0;
			if (flag4)
			{
				infoItem.speed = s.Length;
			}
			bool flag5 = infoItem.speed < 70;
			if (flag5)
			{
				infoItem.speed = 70;
			}
			bool flag6 = this.type == 1;
			if (flag6)
			{
				infoItem.speed = 10000000;
			}
			bool flag7 = this.type == 3;
			if (flag7)
			{
				infoItem.speed = 300;
				infoItem.last = mSystem.currentTimeMillis();
				infoItem.timeCount = s.Length * 10 / 4;
				bool flag8 = infoItem.timeCount < 150;
				if (flag8)
				{
					infoItem.timeCount = 150;
				}
				infoItem.maxTime = infoItem.timeCount;
			}
			bool flag9 = cInfo != null;
			if (flag9)
			{
				infoItem.charInfo = cInfo;
				infoItem.isChatServer = isChatServer;
				GameCanvas.panel.addChatMessage(infoItem);
				bool flag10 = GameCanvas.isTouch && GameCanvas.panel.isViewChatServer;
				if (flag10)
				{
					GameScr.info2.cmdChat = new Command(mResources.CHAT, this, 1000, infoItem);
				}
			}
			bool flag11 = (cInfo != null && GameCanvas.panel.isViewChatServer) || cInfo == null;
			if (flag11)
			{
				this.infoWaitToShow.addElement(infoItem);
			}
			bool flag12 = this.infoWaitToShow.size() == 1;
			if (flag12)
			{
				this.info = (InfoItem)this.infoWaitToShow.firstElement();
				this.getInfo();
			}
			bool flag13 = GameCanvas.isTouch && cInfo != null && GameCanvas.panel.isViewChatServer && GameCanvas.w - 50 > 155 + this.W;
			if (flag13)
			{
				GameScr.info2.cmdChat.x = GameCanvas.w - this.W - 50;
				GameScr.info2.cmdChat.y = 35;
			}
		}
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00051F50 File Offset: 0x00050150
	public void addInfo(string s, int speed, mFont f)
	{
		bool flag = GameCanvas.w == 128;
		if (flag)
		{
			this.limLeft = 1;
		}
		bool flag2 = this.infoWaitToShow.size() > 10;
		if (flag2)
		{
			this.infoWaitToShow.removeElementAt(0);
		}
		this.infoWaitToShow.addElement(new InfoItem(s, f, speed));
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00051FB0 File Offset: 0x000501B0
	public bool isEmpty()
	{
		return this.p1 == 5 && this.infoWaitToShow.size() == 0;
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x00051FDC File Offset: 0x000501DC
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 1000;
		if (flag)
		{
			ChatTextField.gI().startChat(GameScr.gI(), mResources.chat_player);
		}
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void onCancelChat()
	{
	}

	// Token: 0x0400081B RID: 2075
	public MyVector infoWaitToShow = new MyVector();

	// Token: 0x0400081C RID: 2076
	public InfoItem info;

	// Token: 0x0400081D RID: 2077
	public int p1 = 5;

	// Token: 0x0400081E RID: 2078
	public int p2;

	// Token: 0x0400081F RID: 2079
	public int p3;

	// Token: 0x04000820 RID: 2080
	public int x;

	// Token: 0x04000821 RID: 2081
	public int strWidth;

	// Token: 0x04000822 RID: 2082
	public int limLeft = 2;

	// Token: 0x04000823 RID: 2083
	public int hI = 20;

	// Token: 0x04000824 RID: 2084
	public int xChar;

	// Token: 0x04000825 RID: 2085
	public int yChar;

	// Token: 0x04000826 RID: 2086
	public int sayWidth = 100;

	// Token: 0x04000827 RID: 2087
	public int sayRun;

	// Token: 0x04000828 RID: 2088
	public string[] says;

	// Token: 0x04000829 RID: 2089
	public int cx;

	// Token: 0x0400082A RID: 2090
	public int cy;

	// Token: 0x0400082B RID: 2091
	public int ch;

	// Token: 0x0400082C RID: 2092
	public bool outSide;

	// Token: 0x0400082D RID: 2093
	public int f;

	// Token: 0x0400082E RID: 2094
	public int tF;

	// Token: 0x0400082F RID: 2095
	public Image img;

	// Token: 0x04000830 RID: 2096
	public static Image gocnhon = GameCanvas.loadImage("/mainImage/myTexture2dgocnhon.png");

	// Token: 0x04000831 RID: 2097
	public int time;

	// Token: 0x04000832 RID: 2098
	public int timeW;

	// Token: 0x04000833 RID: 2099
	public int type;

	// Token: 0x04000834 RID: 2100
	public int X;

	// Token: 0x04000835 RID: 2101
	public int Y;

	// Token: 0x04000836 RID: 2102
	public int W;

	// Token: 0x04000837 RID: 2103
	public int H;
}
