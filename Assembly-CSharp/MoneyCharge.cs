using System;

// Token: 0x0200006C RID: 108
public class MoneyCharge : mScreen, IActionListener
{
	// Token: 0x0600053E RID: 1342 RVA: 0x00060A44 File Offset: 0x0005EC44
	public MoneyCharge()
	{
		this.w = GameCanvas.w - 20;
		bool flag = this.w > 320;
		if (flag)
		{
			this.w = 320;
		}
		this.strPaint = mFont.tahoma_7b_green2.splitFontArray(mResources.pay_card, this.w - 20);
		this.x = (GameCanvas.w - this.w) / 2;
		this.y = GameCanvas.h - 150 - (this.strPaint.Length - 1) * 20;
		this.h = 110 + (this.strPaint.Length - 1) * 20;
		this.yP = this.y;
		this.tfSerial = new TField();
		this.tfSerial.name = mResources.SERI_NUM;
		this.tfSerial.x = this.x + 10;
		this.tfSerial.y = this.y + 35 + (this.strPaint.Length - 1) * 20;
		this.yt = this.tfSerial.y;
		this.tfSerial.width = this.w - 20;
		this.tfSerial.height = mScreen.ITEM_HEIGHT + 2;
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.tfSerial.isFocus = false;
		}
		else
		{
			this.tfSerial.isFocus = true;
		}
		this.tfSerial.setIputType(TField.INPUT_TYPE_ANY);
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.tfSerial.showSubTextField = false;
		}
		bool isIPhone = Main.isIPhone;
		if (isIPhone)
		{
			this.tfSerial.isPaintMouse = false;
		}
		bool flag2 = !GameCanvas.isTouch;
		if (flag2)
		{
			this.right = this.tfSerial.cmdClear;
		}
		this.tfCode = new TField();
		this.tfCode.name = mResources.CARD_CODE;
		this.tfCode.x = this.x + 10;
		this.tfCode.y = this.tfSerial.y + 35;
		this.tfCode.width = this.w - 20;
		this.tfCode.height = mScreen.ITEM_HEIGHT + 2;
		this.tfCode.isFocus = false;
		this.tfCode.setIputType(TField.INPUT_TYPE_ANY);
		bool isWindowsPhone2 = Main.isWindowsPhone;
		if (isWindowsPhone2)
		{
			this.tfCode.showSubTextField = false;
		}
		bool isIPhone2 = Main.isIPhone;
		if (isIPhone2)
		{
			this.tfCode.isPaintMouse = false;
		}
		this.left = new Command(mResources.CLOSE, this, 1, null);
		this.center = new Command(mResources.pay_card2, this, 2, null);
		bool isTouch2 = GameCanvas.isTouch;
		if (isTouch2)
		{
			this.center.x = GameCanvas.w / 2 + 18;
			this.left.x = GameCanvas.w / 2 - 85;
			this.center.y = (this.left.y = this.y + this.h + 5);
		}
		this.freeAreaHeight = this.tfSerial.y - (4 * this.tfSerial.height - 10);
		this.yP = this.tfSerial.y;
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x00060D94 File Offset: 0x0005EF94
	public static MoneyCharge gI()
	{
		bool flag = MoneyCharge.instance == null;
		if (flag)
		{
			MoneyCharge.instance = new MoneyCharge();
		}
		return MoneyCharge.instance;
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x00004FA9 File Offset: 0x000031A9
	public override void switchToMe()
	{
		this.focus = 0;
		base.switchToMe();
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void updateTfWhenOpenKb()
	{
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00060DC4 File Offset: 0x0005EFC4
	public override void paint(mGraphics g)
	{
		GameScr.gI().paint(g);
		PopUp.paintPopUp(g, this.x, this.y, this.w, this.h, -1, true);
		for (int i = 0; i < this.strPaint.Length; i++)
		{
			mFont.tahoma_7b_green2.drawString(g, this.strPaint[i], GameCanvas.w / 2, this.y + 15 + i * 20, mFont.CENTER);
		}
		this.tfSerial.paint(g);
		this.tfCode.paint(g);
		base.paint(g);
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x00060E68 File Offset: 0x0005F068
	public override void update()
	{
		GameScr.gI().update();
		this.tfSerial.update();
		this.tfCode.update();
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.updateTfWhenOpenKb();
		}
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00060EAC File Offset: 0x0005F0AC
	public override void keyPress(int keyCode)
	{
		bool isFocus = this.tfSerial.isFocus;
		if (isFocus)
		{
			this.tfSerial.keyPressed(keyCode);
		}
		else
		{
			bool isFocus2 = this.tfCode.isFocus;
			if (isFocus2)
			{
				this.tfCode.keyPressed(keyCode);
			}
		}
		base.keyPress(keyCode);
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00060F00 File Offset: 0x0005F100
	public override void updateKey()
	{
		bool flag = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21];
		if (flag)
		{
			this.focus--;
			bool flag2 = this.focus < 0;
			if (flag2)
			{
				this.focus = 1;
			}
		}
		else
		{
			bool flag3 = GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22];
			if (flag3)
			{
				this.focus++;
				bool flag4 = this.focus > 1;
				if (flag4)
				{
					this.focus = 1;
				}
			}
		}
		bool flag5 = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22];
		if (flag5)
		{
			GameCanvas.clearKeyPressed();
			bool flag6 = this.focus == 1;
			if (flag6)
			{
				this.tfSerial.isFocus = false;
				this.tfCode.isFocus = true;
				bool flag7 = !GameCanvas.isTouch;
				if (flag7)
				{
					this.right = this.tfCode.cmdClear;
				}
			}
			else
			{
				bool flag8 = this.focus == 0;
				if (flag8)
				{
					this.tfSerial.isFocus = true;
					this.tfCode.isFocus = false;
					bool flag9 = !GameCanvas.isTouch;
					if (flag9)
					{
						this.right = this.tfSerial.cmdClear;
					}
				}
				else
				{
					this.tfSerial.isFocus = false;
					this.tfCode.isFocus = false;
				}
			}
		}
		bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
		if (isPointerJustRelease)
		{
			bool flag10 = GameCanvas.isPointerHoldIn(this.tfSerial.x, this.tfSerial.y, this.tfSerial.width, this.tfSerial.height);
			if (flag10)
			{
				this.focus = 0;
			}
			else
			{
				bool flag11 = GameCanvas.isPointerHoldIn(this.tfCode.x, this.tfCode.y, this.tfCode.width, this.tfCode.height);
				if (flag11)
				{
					this.focus = 1;
				}
			}
		}
		base.updateKey();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00004FBA File Offset: 0x000031BA
	public void clearScreen()
	{
		MoneyCharge.instance = null;
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00061114 File Offset: 0x0005F314
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 1;
		if (flag)
		{
			GameScr.instance.switchToMe();
			this.clearScreen();
		}
		bool flag2 = idAction == 2;
		if (flag2)
		{
			bool flag3 = this.tfSerial.getText() == null || this.tfSerial.getText().Equals(string.Empty);
			if (flag3)
			{
				GameCanvas.startOKDlg(mResources.serial_blank);
			}
			else
			{
				bool flag4 = this.tfCode.getText() == null || this.tfCode.getText().Equals(string.Empty);
				if (flag4)
				{
					GameCanvas.startOKDlg(mResources.card_code_blank);
				}
				else
				{
					Service.gI().sendCardInfo(this.tfSerial.getText(), this.tfCode.getText());
					GameScr.instance.switchToMe();
					this.clearScreen();
				}
			}
		}
	}

	// Token: 0x04000B42 RID: 2882
	public static MoneyCharge instance;

	// Token: 0x04000B43 RID: 2883
	public TField tfSerial;

	// Token: 0x04000B44 RID: 2884
	public TField tfCode;

	// Token: 0x04000B45 RID: 2885
	private int x;

	// Token: 0x04000B46 RID: 2886
	private int y;

	// Token: 0x04000B47 RID: 2887
	private int w;

	// Token: 0x04000B48 RID: 2888
	private int h;

	// Token: 0x04000B49 RID: 2889
	private string[] strPaint;

	// Token: 0x04000B4A RID: 2890
	private int focus;

	// Token: 0x04000B4B RID: 2891
	private int yt;

	// Token: 0x04000B4C RID: 2892
	private int freeAreaHeight;

	// Token: 0x04000B4D RID: 2893
	private int yy = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;

	// Token: 0x04000B4E RID: 2894
	private int yP;
}
