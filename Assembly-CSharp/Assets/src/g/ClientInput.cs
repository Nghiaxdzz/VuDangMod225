using System;

namespace Assets.src.g
{
	// Token: 0x020000D3 RID: 211
	public class ClientInput : mScreen, IActionListener
	{
		// Token: 0x06000ABD RID: 2749 RVA: 0x000A7060 File Offset: 0x000A5260
		private void init(string t)
		{
			this.w = GameCanvas.w - 20;
			bool flag = this.w > 320;
			if (flag)
			{
				this.w = 320;
			}
			Res.outz("title= " + t);
			this.strPaint = mFont.tahoma_7b_dark.splitFontArray(t, this.w - 20);
			this.x = (GameCanvas.w - this.w) / 2;
			this.tf = new TField[this.nTf];
			this.h = this.tf.Length * 35 + (this.strPaint.Length - 1) * 20 + 40;
			this.y = GameCanvas.h - this.h - 40 - (this.strPaint.Length - 1) * 20;
			for (int i = 0; i < this.tf.Length; i++)
			{
				this.tf[i] = new TField();
				this.tf[i].name = string.Empty;
				this.tf[i].x = this.x + 10;
				this.tf[i].y = this.y + 35 + (this.strPaint.Length - 1) * 20 + i * 35;
				this.tf[i].width = this.w - 20;
				this.tf[i].height = mScreen.ITEM_HEIGHT + 2;
				bool isTouch = GameCanvas.isTouch;
				if (isTouch)
				{
					this.tf[0].isFocus = false;
				}
				else
				{
					this.tf[0].isFocus = true;
				}
				bool flag2 = !GameCanvas.isTouch;
				if (flag2)
				{
					this.right = this.tf[0].cmdClear;
				}
			}
			this.left = new Command(mResources.CLOSE, this, 1, null);
			this.center = new Command(mResources.OK, this, 2, null);
			bool isTouch2 = GameCanvas.isTouch;
			if (isTouch2)
			{
				this.center.x = GameCanvas.w / 2 + 18;
				this.left.x = GameCanvas.w / 2 - 85;
				this.center.y = (this.left.y = this.y + this.h + 5);
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x000A72AC File Offset: 0x000A54AC
		public static ClientInput gI()
		{
			bool flag = ClientInput.instance == null;
			if (flag)
			{
				ClientInput.instance = new ClientInput();
			}
			return ClientInput.instance;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x000068DE File Offset: 0x00004ADE
		public override void switchToMe()
		{
			this.focus = 0;
			base.switchToMe();
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000068EF File Offset: 0x00004AEF
		public void setInput(int type, string title)
		{
			this.nTf = type;
			this.init(title);
			this.switchToMe();
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000A72DC File Offset: 0x000A54DC
		public override void paint(mGraphics g)
		{
			GameScr.gI().paint(g);
			PopUp.paintPopUp(g, this.x, this.y, this.w, this.h, -1, true);
			for (int i = 0; i < this.strPaint.Length; i++)
			{
				mFont.tahoma_7b_green2.drawString(g, this.strPaint[i], GameCanvas.w / 2, this.y + 15 + i * 20, mFont.CENTER);
			}
			for (int j = 0; j < this.tf.Length; j++)
			{
				this.tf[j].paint(g);
			}
			base.paint(g);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000A7390 File Offset: 0x000A5590
		public override void update()
		{
			GameScr.gI().update();
			for (int i = 0; i < this.tf.Length; i++)
			{
				this.tf[i].update();
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000A73D0 File Offset: 0x000A55D0
		public override void keyPress(int keyCode)
		{
			for (int i = 0; i < this.tf.Length; i++)
			{
				bool isFocus = this.tf[i].isFocus;
				if (isFocus)
				{
					this.tf[i].keyPressed(keyCode);
					break;
				}
			}
			base.keyPress(keyCode);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000A7424 File Offset: 0x000A5624
		public override void updateKey()
		{
			bool flag = GameCanvas.keyPressed[2];
			if (flag)
			{
				this.focus--;
				bool flag2 = this.focus < 0;
				if (flag2)
				{
					this.focus = this.tf.Length - 1;
				}
			}
			else
			{
				bool flag3 = GameCanvas.keyPressed[8];
				if (flag3)
				{
					this.focus++;
					bool flag4 = this.focus > this.tf.Length - 1;
					if (flag4)
					{
						this.focus = 0;
					}
				}
			}
			bool flag5 = GameCanvas.keyPressed[2] || GameCanvas.keyPressed[8];
			if (flag5)
			{
				GameCanvas.clearKeyPressed();
				for (int i = 0; i < this.tf.Length; i++)
				{
					bool flag6 = this.focus == i;
					if (flag6)
					{
						this.tf[i].isFocus = true;
						bool flag7 = !GameCanvas.isTouch;
						if (flag7)
						{
							this.right = this.tf[i].cmdClear;
						}
					}
					else
					{
						this.tf[i].isFocus = false;
					}
					bool flag8 = GameCanvas.isPointerJustRelease && GameCanvas.isPointerHoldIn(this.tf[i].x, this.tf[i].y, this.tf[i].width, this.tf[i].height);
					if (flag8)
					{
						this.focus = i;
						break;
					}
				}
			}
			base.updateKey();
			GameCanvas.clearKeyPressed();
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00006908 File Offset: 0x00004B08
		public void clearScreen()
		{
			ClientInput.instance = null;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x000A75B0 File Offset: 0x000A57B0
		public void perform(int idAction, object p)
		{
			bool flag = idAction == 1;
			if (flag)
			{
				GameScr.instance.switchToMe();
				this.clearScreen();
			}
			bool flag2 = idAction != 2;
			if (!flag2)
			{
				for (int i = 0; i < this.tf.Length; i++)
				{
					bool flag3 = this.tf[i].getText() == null || this.tf[i].getText().Equals(string.Empty);
					if (flag3)
					{
						GameCanvas.startOKDlg(mResources.vuilongnhapduthongtin);
						return;
					}
				}
				Service.gI().sendClientInput(this.tf);
				GameScr.instance.switchToMe();
			}
		}

		// Token: 0x0400139C RID: 5020
		public static ClientInput instance;

		// Token: 0x0400139D RID: 5021
		public TField[] tf;

		// Token: 0x0400139E RID: 5022
		private int x;

		// Token: 0x0400139F RID: 5023
		private int y;

		// Token: 0x040013A0 RID: 5024
		private int w;

		// Token: 0x040013A1 RID: 5025
		private int h;

		// Token: 0x040013A2 RID: 5026
		private string[] strPaint;

		// Token: 0x040013A3 RID: 5027
		private int focus;

		// Token: 0x040013A4 RID: 5028
		private int nTf;
	}
}
