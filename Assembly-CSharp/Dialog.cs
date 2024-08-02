using System;

// Token: 0x02000020 RID: 32
public abstract class Dialog
{
	// Token: 0x060001DD RID: 477 RVA: 0x000355E8 File Offset: 0x000337E8
	public virtual void paint(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		GameCanvas.paintz.paintTabSoft(g);
		GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
	}

	// Token: 0x060001DE RID: 478 RVA: 0x00035648 File Offset: 0x00033848
	public virtual void keyPress(int keyCode)
	{
		if (keyCode > -22)
		{
			if (keyCode != -21)
			{
				switch (keyCode)
				{
				case -7:
					goto IL_C2;
				case -6:
					goto IL_AE;
				case -5:
					break;
				case -4:
				case -3:
					return;
				case -2:
					goto IL_86;
				case -1:
					goto IL_5E;
				default:
					if (keyCode != 10)
					{
						return;
					}
					break;
				}
				GameCanvas.keyHold[(!Main.isPC) ? 5 : 25] = true;
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = true;
				return;
			}
			IL_AE:
			GameCanvas.keyHold[12] = true;
			GameCanvas.keyPressed[12] = true;
			return;
		}
		if (keyCode == -39)
		{
			goto IL_86;
		}
		if (keyCode != -38)
		{
			if (keyCode != -22)
			{
				return;
			}
			goto IL_C2;
		}
		IL_5E:
		GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
		GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
		return;
		IL_86:
		GameCanvas.keyHold[(!Main.isPC) ? 8 : 22] = true;
		GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = true;
		return;
		IL_C2:
		GameCanvas.keyHold[13] = true;
		GameCanvas.keyPressed[13] = true;
	}

	// Token: 0x060001DF RID: 479 RVA: 0x00035754 File Offset: 0x00033954
	public virtual void update()
	{
		bool flag = this.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.center));
		if (flag)
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			GameCanvas.isPointerClick = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			bool flag2 = this.center != null;
			if (flag2)
			{
				this.center.performAction();
			}
			mScreen.keyTouch = -1;
		}
		bool flag3 = this.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.left));
		if (flag3)
		{
			GameCanvas.keyPressed[12] = false;
			GameCanvas.isPointerClick = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			bool flag4 = this.left != null;
			if (flag4)
			{
				this.left.performAction();
			}
			mScreen.keyTouch = -1;
		}
		bool flag5 = this.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right));
		if (flag5)
		{
			GameCanvas.keyPressed[13] = false;
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerJustRelease = false;
			mScreen.keyTouch = -1;
			bool flag6 = this.right != null;
			if (flag6)
			{
				this.right.performAction();
			}
			mScreen.keyTouch = -1;
		}
		GameCanvas.clearKeyPressed();
		GameCanvas.clearKeyHold();
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x00003A0D File Offset: 0x00001C0D
	public virtual void show()
	{
	}

	// Token: 0x0400048B RID: 1163
	public Command left;

	// Token: 0x0400048C RID: 1164
	public Command center;

	// Token: 0x0400048D RID: 1165
	public Command right;

	// Token: 0x0400048E RID: 1166
	private int lenCaption;
}
