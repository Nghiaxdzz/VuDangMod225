using System;

// Token: 0x02000011 RID: 17
public class ChatTextField : IActionListener
{
	// Token: 0x0600014D RID: 333 RVA: 0x0001E010 File Offset: 0x0001C210
	public void VuDangStartChat(IChatable parentScreen, string str, string name)
	{
		this.tfChat.setFocusWithKb(true);
		this.strChat = str;
		this.tfChat.name = name;
		this.parentScreen = parentScreen;
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		bool isIPhone = Main.isIPhone;
		if (isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		bool flag = GameCanvas.currentDialog == null;
		if (flag)
		{
			this.isShow = true;
			bool flag2 = !Main.isPC;
			if (flag2)
			{
				ipKeyboard.openKeyBoard(this.strChat, ipKeyboard.TEXT, string.Empty, this.cmdChat2);
				this.tfChat.setFocusWithKb(true);
			}
		}
		this.tfChat.setText(string.Empty);
		this.tfChat.clearAll();
		this.isPublic = false;
	}

	// Token: 0x0600014E RID: 334 RVA: 0x0001E0E4 File Offset: 0x0001C2E4
	public ChatTextField()
	{
		this.tfChat = new TField();
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		bool isIPhone = Main.isIPhone;
		if (isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		this.tfChat.name = "chat";
		bool isWindowsPhone2 = Main.isWindowsPhone;
		if (isWindowsPhone2)
		{
			this.tfChat.strInfo = this.tfChat.name;
		}
		this.tfChat.width = GameCanvas.w - 6;
		bool flag = Main.isPC && this.tfChat.width > 250;
		if (flag)
		{
			this.tfChat.width = 250;
		}
		this.tfChat.height = mScreen.ITEM_HEIGHT + 2;
		this.tfChat.x = GameCanvas.w / 2 - this.tfChat.width / 2;
		this.tfChat.isFocus = true;
		this.tfChat.setMaxTextLenght(80);
	}

	// Token: 0x0600014F RID: 335 RVA: 0x0001E200 File Offset: 0x0001C400
	public void initChatTextField()
	{
		this.left = new Command(mResources.OK, this, 8000, null, 1, GameCanvas.h - mScreen.cmdH + 1);
		this.right = new Command(mResources.DELETE, this, 8001, null, GameCanvas.w - 70, GameCanvas.h - mScreen.cmdH + 1);
		this.center = null;
		this.w = this.tfChat.width + 20;
		this.h = this.tfChat.height + 26;
		this.x = GameCanvas.w / 2 - this.w / 2;
		this.y = this.tfChat.y - 18;
		bool flag = Main.isPC && this.w > 320;
		if (flag)
		{
			this.w = 320;
		}
		this.left.x = this.x;
		this.right.x = this.x + this.w - 68;
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.tfChat.y -= 5;
			this.y -= 20;
			this.h += 30;
			this.left.x = GameCanvas.w / 2 - 68 - 5;
			this.right.x = GameCanvas.w / 2 + 5;
			this.left.y = GameCanvas.h - 30;
			this.right.y = GameCanvas.h - 30;
		}
		this.cmdChat = new Command();
		ActionChat actionChat = delegate(string str)
		{
			this.tfChat.justReturnFromTextBox = false;
			this.tfChat.setText(str);
			this.parentScreen.onChatFromMe(str, this.to);
			this.tfChat.setText(string.Empty);
			this.right.caption = mResources.CLOSE;
		};
		this.cmdChat.actionChat = actionChat;
		this.cmdChat2 = new Command();
		this.cmdChat2.actionChat = delegate(string str)
		{
			this.tfChat.justReturnFromTextBox = false;
			bool flag2 = this.parentScreen != null;
			if (flag2)
			{
				this.tfChat.setText(str);
				this.parentScreen.onChatFromMe(str, this.to);
				this.tfChat.setText(string.Empty);
				this.tfChat.clearKb();
				bool flag3 = this.right != null;
				if (flag3)
				{
					this.right.performAction();
				}
			}
			this.isShow = false;
		};
		this.yBegin = this.tfChat.y;
		this.yUp = GameCanvas.h / 2 - 2 * this.tfChat.height;
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		bool isIPhone = Main.isIPhone;
		if (isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void updateWhenKeyBoardVisible()
	{
	}

	// Token: 0x06000151 RID: 337 RVA: 0x0001E440 File Offset: 0x0001C640
	public void keyPressed(int keyCode)
	{
		bool flag = this.isShow;
		if (flag)
		{
			this.tfChat.keyPressed(keyCode);
		}
		bool flag2 = this.tfChat.getText().Equals(string.Empty);
		if (flag2)
		{
			this.right.caption = mResources.CLOSE;
		}
		else
		{
			this.right.caption = mResources.DELETE;
		}
	}

	// Token: 0x06000152 RID: 338 RVA: 0x0001E4A8 File Offset: 0x0001C6A8
	public static ChatTextField gI()
	{
		return (ChatTextField.instance != null) ? ChatTextField.instance : (ChatTextField.instance = new ChatTextField());
	}

	// Token: 0x06000153 RID: 339 RVA: 0x0001E4D4 File Offset: 0x0001C6D4
	public void startChat(int firstCharacter, IChatable parentScreen, string to)
	{
		this.right.caption = mResources.CLOSE;
		this.to = to;
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		bool isIPhone = Main.isIPhone;
		if (isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		this.tfChat.keyPressed(firstCharacter);
		bool flag = !this.tfChat.getText().Equals(string.Empty) && GameCanvas.currentDialog == null;
		if (flag)
		{
			this.parentScreen = parentScreen;
			this.isShow = true;
		}
	}

	// Token: 0x06000154 RID: 340 RVA: 0x0001E56C File Offset: 0x0001C76C
	public void startChat(IChatable parentScreen, string to)
	{
		this.right.caption = mResources.CLOSE;
		this.to = to;
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		bool isIPhone = Main.isIPhone;
		if (isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		bool flag = GameCanvas.currentDialog == null;
		if (flag)
		{
			this.isShow = true;
			this.tfChat.isFocus = true;
			bool flag2 = !Main.isPC;
			if (flag2)
			{
				ipKeyboard.openKeyBoard(this.strChat, ipKeyboard.TEXT, string.Empty, this.cmdChat);
				this.tfChat.setFocusWithKb(true);
			}
		}
		this.tfChat.setText(string.Empty);
		this.tfChat.clearAll();
		this.isPublic = false;
	}

	// Token: 0x06000155 RID: 341 RVA: 0x0001E63C File Offset: 0x0001C83C
	public void startChat2(IChatable parentScreen, string to)
	{
		this.tfChat.setFocusWithKb(true);
		this.to = to;
		this.parentScreen = parentScreen;
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		bool isIPhone = Main.isIPhone;
		if (isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		bool flag = GameCanvas.currentDialog == null;
		if (flag)
		{
			this.isShow = true;
			bool flag2 = !Main.isPC;
			if (flag2)
			{
				ipKeyboard.openKeyBoard(this.strChat, ipKeyboard.TEXT, string.Empty, this.cmdChat2);
				this.tfChat.setFocusWithKb(true);
			}
		}
		this.tfChat.setText(string.Empty);
		this.tfChat.clearAll();
		this.isPublic = false;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void updateKey()
	{
	}

	// Token: 0x06000157 RID: 343 RVA: 0x0001E704 File Offset: 0x0001C904
	public void update()
	{
		bool flag = !this.isShow;
		if (!flag)
		{
			this.tfChat.update();
			bool isWindowsPhone = Main.isWindowsPhone;
			if (isWindowsPhone)
			{
				this.updateWhenKeyBoardVisible();
			}
			bool justReturnFromTextBox = this.tfChat.justReturnFromTextBox;
			if (justReturnFromTextBox)
			{
				this.tfChat.justReturnFromTextBox = false;
				this.parentScreen.onChatFromMe(this.tfChat.getText(), this.to);
				this.tfChat.setText(string.Empty);
				this.right.caption = mResources.CLOSE;
			}
			bool flag2 = !Main.isPC;
			if (!flag2)
			{
				bool flag3 = GameCanvas.keyPressed[15];
				if (flag3)
				{
					bool flag4 = this.left != null && this.tfChat.getText() != string.Empty;
					if (flag4)
					{
						this.left.performAction();
					}
					GameCanvas.keyPressed[15] = false;
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				}
				bool flag5 = GameCanvas.keyPressed[14];
				if (flag5)
				{
					bool flag6 = this.right != null;
					if (flag6)
					{
						this.right.performAction();
					}
					GameCanvas.keyPressed[14] = false;
				}
			}
		}
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00003D62 File Offset: 0x00001F62
	public void close()
	{
		this.tfChat.setText(string.Empty);
		this.isShow = false;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x0001E848 File Offset: 0x0001CA48
	public void paint(mGraphics g)
	{
		bool flag = this.isShow && !Main.isIPhone;
		if (flag)
		{
			int num = (!Main.isWindowsPhone) ? (this.y - this.KC) : (this.tfChat.y - 5);
			int num2 = (!Main.isWindowsPhone) ? this.x : 0;
			int num3 = (!Main.isWindowsPhone) ? this.w : GameCanvas.w;
			PopUp.paintPopUp(g, num2, num, num3, this.h, -1, true);
			bool isPC = Main.isPC;
			if (isPC)
			{
				mFont.tahoma_7b_green2.drawString(g, this.strChat + this.to, this.tfChat.x, this.tfChat.y - ((!GameCanvas.isTouch) ? 12 : 17), 0);
				GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
			}
			this.tfChat.paint(g);
		}
	}

	// Token: 0x0600015A RID: 346 RVA: 0x0001E948 File Offset: 0x0001CB48
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 8000:
		{
			Cout.LogError("perform chat 8000");
			bool flag = this.parentScreen != null;
			if (flag)
			{
				long num = mSystem.currentTimeMillis();
				bool flag2 = num - this.lastChatTime >= 1000L;
				if (flag2)
				{
					this.lastChatTime = num;
					this.parentScreen.onChatFromMe(this.tfChat.getText(), this.to);
					this.tfChat.setText(string.Empty);
					this.right.caption = mResources.CLOSE;
					this.tfChat.clearKb();
				}
			}
			break;
		}
		case 8001:
		{
			Cout.LogError("perform chat 8001");
			bool flag3 = this.tfChat.getText().Equals(string.Empty);
			if (flag3)
			{
				this.isShow = false;
				this.parentScreen.onCancelChat();
			}
			this.tfChat.clear();
			break;
		}
		}
	}

	// Token: 0x040002B5 RID: 693
	private static ChatTextField instance;

	// Token: 0x040002B6 RID: 694
	public TField tfChat;

	// Token: 0x040002B7 RID: 695
	public bool isShow;

	// Token: 0x040002B8 RID: 696
	public IChatable parentScreen;

	// Token: 0x040002B9 RID: 697
	private long lastChatTime;

	// Token: 0x040002BA RID: 698
	public Command left;

	// Token: 0x040002BB RID: 699
	public Command cmdChat;

	// Token: 0x040002BC RID: 700
	public Command right;

	// Token: 0x040002BD RID: 701
	public Command center;

	// Token: 0x040002BE RID: 702
	private int x;

	// Token: 0x040002BF RID: 703
	private int y;

	// Token: 0x040002C0 RID: 704
	private int w;

	// Token: 0x040002C1 RID: 705
	private int h;

	// Token: 0x040002C2 RID: 706
	private bool isPublic;

	// Token: 0x040002C3 RID: 707
	public Command cmdChat2;

	// Token: 0x040002C4 RID: 708
	public int yBegin;

	// Token: 0x040002C5 RID: 709
	public int yUp;

	// Token: 0x040002C6 RID: 710
	public int KC;

	// Token: 0x040002C7 RID: 711
	public string to;

	// Token: 0x040002C8 RID: 712
	public string strChat = "Chat ";
}
