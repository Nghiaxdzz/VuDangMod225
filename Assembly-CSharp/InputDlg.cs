using System;

// Token: 0x02000049 RID: 73
public class InputDlg : Dialog
{
	// Token: 0x060003C9 RID: 969 RVA: 0x000536C0 File Offset: 0x000518C0
	public InputDlg()
	{
		this.padLeft = 40;
		bool flag = GameCanvas.w <= 176;
		if (flag)
		{
			this.padLeft = 10;
		}
		this.tfInput = new TField();
		this.tfInput.x = this.padLeft + 10;
		this.tfInput.y = GameCanvas.h - mScreen.ITEM_HEIGHT - 43;
		this.tfInput.width = GameCanvas.w - 2 * (this.padLeft + 10);
		this.tfInput.height = mScreen.ITEM_HEIGHT + 2;
		this.tfInput.isFocus = true;
		this.right = this.tfInput.cmdClear;
	}

	// Token: 0x060003CA RID: 970 RVA: 0x00053780 File Offset: 0x00051980
	public void show(string info, Command ok, int type)
	{
		this.tfInput.setText(string.Empty);
		this.tfInput.setIputType(type);
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - this.padLeft * 2);
		this.left = new Command(mResources.CLOSE, GameCanvas.gI(), 8882, null);
		this.center = ok;
		this.show();
	}

	// Token: 0x060003CB RID: 971 RVA: 0x000537F4 File Offset: 0x000519F4
	public override void paint(mGraphics g)
	{
		GameCanvas.paintz.paintInputDlg(g, this.padLeft, GameCanvas.h - 77 - mScreen.cmdH, GameCanvas.w - this.padLeft * 2, 69, this.info);
		this.tfInput.paint(g);
		base.paint(g);
	}

	// Token: 0x060003CC RID: 972 RVA: 0x00004A4A File Offset: 0x00002C4A
	public override void keyPress(int keyCode)
	{
		this.tfInput.keyPressed(keyCode);
		base.keyPress(keyCode);
	}

	// Token: 0x060003CD RID: 973 RVA: 0x00004A62 File Offset: 0x00002C62
	public override void update()
	{
		this.tfInput.update();
		base.update();
	}

	// Token: 0x060003CE RID: 974 RVA: 0x00004A78 File Offset: 0x00002C78
	public override void show()
	{
		GameCanvas.currentDialog = this;
	}

	// Token: 0x060003CF RID: 975 RVA: 0x00004A81 File Offset: 0x00002C81
	public void hide()
	{
		GameCanvas.endDlg();
	}

	// Token: 0x04000885 RID: 2181
	protected string[] info;

	// Token: 0x04000886 RID: 2182
	public TField tfInput;

	// Token: 0x04000887 RID: 2183
	private int padLeft;
}
