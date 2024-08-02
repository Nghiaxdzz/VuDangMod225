using System;

// Token: 0x02000071 RID: 113
public class mScreen
{
	// Token: 0x06000566 RID: 1382 RVA: 0x000630B8 File Offset: 0x000612B8
	public virtual void switchToMe()
	{
		GameCanvas.clearKeyPressed();
		GameCanvas.clearKeyHold();
		bool flag = GameCanvas.currentScreen != null;
		if (flag)
		{
			GameCanvas.currentScreen.unLoad();
		}
		GameCanvas.currentScreen = this;
		Cout.LogError3("cur Screen: " + GameCanvas.currentScreen);
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x00003A0D File Offset: 0x00001C0D
	public virtual void unLoad()
	{
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void initPos()
	{
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x00003A0D File Offset: 0x00001C0D
	public virtual void keyPress(int keyCode)
	{
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x00003A0D File Offset: 0x00001C0D
	public virtual void update()
	{
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x00063108 File Offset: 0x00061308
	public virtual void updateKey()
	{
		bool flag = GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center);
		if (flag)
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			bool flag2 = this.center != null;
			if (flag2)
			{
				this.center.performAction();
			}
		}
		bool flag3 = GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.left);
		if (flag3)
		{
			GameCanvas.keyPressed[12] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			bool isShow = ChatTextField.gI().isShow;
			if (isShow)
			{
				bool flag4 = ChatTextField.gI().left != null;
				if (flag4)
				{
					ChatTextField.gI().left.performAction();
				}
			}
			else
			{
				bool flag5 = this.left != null;
				if (flag5)
				{
					this.left.performAction();
				}
			}
		}
		bool flag6 = !GameCanvas.keyPressed[13] && !mScreen.getCmdPointerLast(GameCanvas.currentScreen.right);
		if (!flag6)
		{
			GameCanvas.keyPressed[13] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			bool isShow2 = ChatTextField.gI().isShow;
			if (isShow2)
			{
				bool flag7 = ChatTextField.gI().right != null;
				if (flag7)
				{
					ChatTextField.gI().right.performAction();
				}
			}
			else
			{
				bool flag8 = this.right != null;
				if (flag8)
				{
					this.right.performAction();
				}
			}
		}
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x0006329C File Offset: 0x0006149C
	public static bool getCmdPointerLast(Command cmd)
	{
		bool flag = cmd == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = cmd.x >= 0 && cmd.y != 0;
			if (flag2)
			{
				result = cmd.isPointerPressInside();
			}
			else
			{
				bool flag3 = GameCanvas.currentDialog != null;
				if (flag3)
				{
					bool flag4 = GameCanvas.currentDialog.center != null && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW >> 1, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10);
					if (flag4)
					{
						mScreen.keyTouch = 1;
						bool flag5 = cmd == GameCanvas.currentDialog.center && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
						if (flag5)
						{
							return true;
						}
					}
					bool flag6 = GameCanvas.currentDialog.left != null && GameCanvas.isPointerHoldIn(0, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10);
					if (flag6)
					{
						mScreen.keyTouch = 0;
						bool flag7 = cmd == GameCanvas.currentDialog.left && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
						if (flag7)
						{
							return true;
						}
					}
					bool flag8 = GameCanvas.currentDialog.right != null && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10);
					if (flag8)
					{
						mScreen.keyTouch = 2;
						bool flag9 = (cmd == GameCanvas.currentDialog.right || cmd == ChatTextField.gI().right) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
						if (flag9)
						{
							return true;
						}
					}
				}
				else
				{
					bool flag10 = cmd == GameCanvas.currentScreen.left && GameCanvas.isPointerHoldIn(0, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10);
					if (flag10)
					{
						mScreen.keyTouch = 0;
						bool flag11 = GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
						if (flag11)
						{
							return true;
						}
					}
					bool flag12 = cmd == GameCanvas.currentScreen.right && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10);
					if (flag12)
					{
						mScreen.keyTouch = 2;
						bool flag13 = GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
						if (flag13)
						{
							return true;
						}
					}
					bool flag14 = (cmd == GameCanvas.currentScreen.center || ChatPopup.currChatPopup != null) && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW >> 1, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10);
					if (flag14)
					{
						mScreen.keyTouch = 1;
						bool flag15 = GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease;
						if (flag15)
						{
							return true;
						}
					}
				}
				result = false;
			}
		}
		return result;
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x00063580 File Offset: 0x00061780
	public virtual void paint(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h + 1);
		bool flag = (!ChatTextField.gI().isShow || !Main.isPC) && GameCanvas.currentDialog == null && !GameCanvas.menu.showMenu;
		if (flag)
		{
			GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
		}
	}

	// Token: 0x04000D31 RID: 3377
	public Command left;

	// Token: 0x04000D32 RID: 3378
	public Command center;

	// Token: 0x04000D33 RID: 3379
	public Command right;

	// Token: 0x04000D34 RID: 3380
	public Command cmdClose;

	// Token: 0x04000D35 RID: 3381
	public static int ITEM_HEIGHT;

	// Token: 0x04000D36 RID: 3382
	public static int yOpenKeyBoard = 100;

	// Token: 0x04000D37 RID: 3383
	public static int cmdW = 68;

	// Token: 0x04000D38 RID: 3384
	public static int cmdH = 26;

	// Token: 0x04000D39 RID: 3385
	public static int keyTouch = -1;

	// Token: 0x04000D3A RID: 3386
	public static int keyMouse = -1;
}
