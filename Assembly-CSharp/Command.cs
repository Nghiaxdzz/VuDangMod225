using System;

// Token: 0x02000018 RID: 24
public class Command
{
	// Token: 0x0600016C RID: 364 RVA: 0x0001EF5C File Offset: 0x0001D15C
	public Command(string caption, IActionListener actionListener, int action, object p, int x, int y)
	{
		this.caption = caption;
		this.idAction = action;
		this.actionListener = actionListener;
		this.p = p;
		this.x = x;
		this.y = y;
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00003DE7 File Offset: 0x00001FE7
	public Command()
	{
	}

	// Token: 0x0600016E RID: 366 RVA: 0x0001EFC8 File Offset: 0x0001D1C8
	public Command(string caption, IActionListener actionListener, int action, object p)
	{
		this.caption = caption;
		this.idAction = action;
		this.actionListener = actionListener;
		this.p = p;
	}

	// Token: 0x0600016F RID: 367 RVA: 0x0001F024 File Offset: 0x0001D224
	public Command(string caption, int action, object p)
	{
		this.caption = caption;
		this.idAction = action;
		this.p = p;
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00003E19 File Offset: 0x00002019
	public Command(string caption, int action)
	{
		this.caption = caption;
		this.idAction = action;
	}

	// Token: 0x06000171 RID: 369 RVA: 0x0001F078 File Offset: 0x0001D278
	public Command(string caption, int action, int x, int y)
	{
		this.caption = caption;
		this.idAction = action;
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000172 RID: 370 RVA: 0x0001F0D4 File Offset: 0x0001D2D4
	public void perform(string str)
	{
		bool flag = this.actionChat != null;
		if (flag)
		{
			this.actionChat(str);
		}
	}

	// Token: 0x06000173 RID: 371 RVA: 0x0001F100 File Offset: 0x0001D300
	public void performAction()
	{
		GameCanvas.clearAllPointerEvent();
		bool flag = this.isPlaySoundButton && ((this.caption != null && !this.caption.Equals(string.Empty) && !this.caption.Equals(mResources.saying)) || this.img != null);
		if (flag)
		{
			SoundMn.gI().buttonClick();
		}
		bool flag2 = this.idAction > 0;
		if (flag2)
		{
			bool flag3 = this.actionListener != null;
			if (flag3)
			{
				this.actionListener.perform(this.idAction, this.p);
			}
			else
			{
				GameScr.gI().actionPerform(this.idAction, this.p);
			}
		}
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00003E59 File Offset: 0x00002059
	public void setType()
	{
		this.type = 1;
		this.w = 160;
		this.hw = 80;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x0001F1BC File Offset: 0x0001D3BC
	public void paint(mGraphics g)
	{
		bool flag = this.img != null;
		if (flag)
		{
			g.drawImage(this.img, this.x, this.y + mGraphics.addYWhenOpenKeyBoard, 0);
			bool flag2 = this.isFocus;
			if (flag2)
			{
				bool flag3 = this.imgFocus == null;
				if (flag3)
				{
					bool flag4 = this.cmdClosePanel;
					if (flag4)
					{
						g.drawImage(ItemMap.imageFlare, this.x + 8, this.y + mGraphics.addYWhenOpenKeyBoard + 8, 3);
					}
					else
					{
						g.drawImage(ItemMap.imageFlare, this.x - (this.img.Equals(GameScr.imgMenu) ? 10 : 0), this.y + mGraphics.addYWhenOpenKeyBoard, 0);
					}
				}
				else
				{
					g.drawImage(this.imgFocus, this.x, this.y + mGraphics.addYWhenOpenKeyBoard, 0);
				}
			}
			bool flag5 = this.caption != "menu" && this.caption != null;
			if (flag5)
			{
				bool flag6 = !this.isFocus;
				if (flag6)
				{
					mFont.tahoma_7b_dark.drawString(g, this.caption, this.x + mGraphics.getImageWidth(this.img) / 2, this.y + mGraphics.getImageHeight(this.img) / 2 - 5, 2);
				}
				else
				{
					mFont.tahoma_7b_green2.drawString(g, this.caption, this.x + mGraphics.getImageWidth(this.img) / 2, this.y + mGraphics.getImageHeight(this.img) / 2 - 5, 2);
				}
			}
		}
		else
		{
			bool flag7 = this.caption != string.Empty;
			if (flag7)
			{
				bool flag8 = this.type == 1;
				if (flag8)
				{
					bool flag9 = !this.isFocus;
					if (flag9)
					{
						Command.paintOngMau(Command.btn0left, Command.btn0mid, Command.btn0right, this.x, this.y, 160, g);
					}
					else
					{
						Command.paintOngMau(Command.btn1left, Command.btn1mid, Command.btn1right, this.x, this.y, 160, g);
					}
				}
				else
				{
					bool flag10 = !this.isFocus;
					if (flag10)
					{
						Command.paintOngMau(Command.btn0left, Command.btn0mid, Command.btn0right, this.x, this.y, 76, g);
					}
					else
					{
						Command.paintOngMau(Command.btn1left, Command.btn1mid, Command.btn1right, this.x, this.y, 76, g);
					}
				}
			}
			int num = (this.type != 1) ? (this.x + 38) : (this.x + this.hw);
			bool flag11 = !this.isFocus;
			if (flag11)
			{
				mFont.tahoma_7b_dark.drawString(g, this.caption, num, this.y + 7, 2);
			}
			else
			{
				mFont.tahoma_7b_green2.drawString(g, this.caption, num, this.y + 7, 2);
			}
		}
	}

	// Token: 0x06000176 RID: 374 RVA: 0x0001F4C4 File Offset: 0x0001D6C4
	public static void paintOngMau(Image img0, Image img1, Image img2, int x, int y, int size, mGraphics g)
	{
		for (int i = 10; i <= size - 20; i += 10)
		{
			g.drawImage(img1, x + i, y, 0);
		}
		int num = size % 10;
		bool flag = num > 0;
		if (flag)
		{
			g.drawRegion(img1, 0, 0, num, 24, 0, x + size - 10 - num, y, 0);
		}
		g.drawImage(img0, x, y, 0);
		g.drawImage(img2, x + size - 10, y, 0);
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0001F548 File Offset: 0x0001D748
	public bool isPointerPressInside()
	{
		this.isFocus = false;
		bool flag = GameCanvas.isPointerHoldIn(this.x, this.y, this.w, this.h);
		if (flag)
		{
			bool isPointerDown = GameCanvas.isPointerDown;
			if (isPointerDown)
			{
				this.isFocus = true;
			}
			bool flag2 = GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick;
			if (flag2)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000178 RID: 376 RVA: 0x0001F5B0 File Offset: 0x0001D7B0
	public bool isPointerPressInsideCamera(int cmx, int cmy)
	{
		this.isFocus = false;
		bool flag = GameCanvas.isPointerHoldIn(this.x - cmx, this.y - cmy, this.w, this.h);
		if (flag)
		{
			Res.outz("w= " + this.w);
			bool isPointerDown = GameCanvas.isPointerDown;
			if (isPointerDown)
			{
				this.isFocus = true;
			}
			bool flag2 = GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick;
			if (flag2)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0400040D RID: 1037
	public ActionChat actionChat;

	// Token: 0x0400040E RID: 1038
	public string caption;

	// Token: 0x0400040F RID: 1039
	public string[] subCaption;

	// Token: 0x04000410 RID: 1040
	public IActionListener actionListener;

	// Token: 0x04000411 RID: 1041
	public int idAction;

	// Token: 0x04000412 RID: 1042
	public bool isPlaySoundButton = true;

	// Token: 0x04000413 RID: 1043
	public Image img;

	// Token: 0x04000414 RID: 1044
	public Image imgFocus;

	// Token: 0x04000415 RID: 1045
	public int x;

	// Token: 0x04000416 RID: 1046
	public int y;

	// Token: 0x04000417 RID: 1047
	public int w = mScreen.cmdW;

	// Token: 0x04000418 RID: 1048
	public int h = mScreen.cmdH;

	// Token: 0x04000419 RID: 1049
	public int hw;

	// Token: 0x0400041A RID: 1050
	private int lenCaption;

	// Token: 0x0400041B RID: 1051
	public bool isFocus;

	// Token: 0x0400041C RID: 1052
	public object p;

	// Token: 0x0400041D RID: 1053
	public int type;

	// Token: 0x0400041E RID: 1054
	public string caption2 = string.Empty;

	// Token: 0x0400041F RID: 1055
	public static Image btn0left;

	// Token: 0x04000420 RID: 1056
	public static Image btn0mid;

	// Token: 0x04000421 RID: 1057
	public static Image btn0right;

	// Token: 0x04000422 RID: 1058
	public static Image btn1left;

	// Token: 0x04000423 RID: 1059
	public static Image btn1mid;

	// Token: 0x04000424 RID: 1060
	public static Image btn1right;

	// Token: 0x04000425 RID: 1061
	public bool cmdClosePanel;
}
