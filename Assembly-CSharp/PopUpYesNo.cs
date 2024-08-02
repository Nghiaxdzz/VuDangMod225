using System;

// Token: 0x0200008B RID: 139
public class PopUpYesNo : IActionListener
{
	// Token: 0x0600076B RID: 1899 RVA: 0x00085204 File Offset: 0x00083404
	public void setPopUp(string info, Command cmdYes, Command cmdNo)
	{
		this.info = new string[]
		{
			info
		};
		this.H = 29;
		this.cmdYes = cmdYes;
		this.cmdNo = cmdNo;
		this.cmdYes.img = (this.cmdNo.img = GameScr.imgNut);
		this.cmdYes.imgFocus = (this.cmdNo.imgFocus = GameScr.imgNutF);
		this.cmdYes.w = mGraphics.getImageWidth(cmdYes.img);
		this.cmdNo.w = mGraphics.getImageWidth(cmdYes.img);
		this.cmdYes.h = mGraphics.getImageHeight(cmdYes.img);
		this.cmdNo.h = mGraphics.getImageHeight(cmdYes.img);
		this.last = mSystem.currentTimeMillis();
		this.dem = this.info[0].Length / 3;
		bool flag = this.dem < 15;
		if (flag)
		{
			this.dem = 15;
		}
		TextInfo.reset();
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x0008530C File Offset: 0x0008350C
	public void paint(mGraphics g)
	{
		PopUp.paintPopUp(g, this.X, this.Y, this.W, this.H + ((!GameCanvas.isTouch) ? 10 : 0), 16777215, false);
		bool flag = this.info != null;
		if (flag)
		{
			TextInfo.paint(g, this.info[0], this.X + 5, this.Y + this.H / 2 - ((!GameCanvas.isTouch) ? 6 : 4), this.W - 10, this.H, mFont.tahoma_7);
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				this.cmdYes.paint(g);
				mFont.tahoma_7_yellow.drawString(g, this.dem + string.Empty, this.cmdYes.x + this.cmdYes.w / 2, this.cmdYes.y + this.cmdYes.h + 5, 2, mFont.tahoma_7_grey);
			}
			else
			{
				bool isQwerty = TField.isQwerty;
				if (isQwerty)
				{
					mFont.tahoma_7b_blue.drawString(g, mResources.do_accept_qwerty + this.dem + ")", this.X + this.W / 2, this.Y + this.H - 6, 2);
				}
				else
				{
					mFont.tahoma_7b_blue.drawString(g, mResources.do_accept + this.dem + ")", this.X + this.W / 2, this.Y + this.H - 6, 2);
				}
			}
		}
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x000854B4 File Offset: 0x000836B4
	public void update()
	{
		bool flag = this.info != null;
		if (flag)
		{
			this.X = GameCanvas.w - 5 - this.W;
			this.Y = 45;
			bool flag2 = GameCanvas.w - 50 > 155 + this.W;
			if (flag2)
			{
				this.X = GameCanvas.w - 55 - this.W;
				this.Y = 5;
			}
			this.cmdYes.x = this.X - 35;
			this.cmdYes.y = this.Y;
			this.curr = mSystem.currentTimeMillis();
			Res.outz("curr - last= " + (this.curr - this.last));
			bool flag3 = this.curr - this.last >= 1000L;
			if (flag3)
			{
				this.last = mSystem.currentTimeMillis();
				this.dem--;
			}
			bool flag4 = this.dem == 0;
			if (flag4)
			{
				GameScr.gI().popUpYesNo = null;
			}
		}
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void perform(int idAction, object p)
	{
	}

	// Token: 0x04000F6F RID: 3951
	public Command cmdYes;

	// Token: 0x04000F70 RID: 3952
	public Command cmdNo;

	// Token: 0x04000F71 RID: 3953
	public string[] info;

	// Token: 0x04000F72 RID: 3954
	private int X;

	// Token: 0x04000F73 RID: 3955
	private int Y;

	// Token: 0x04000F74 RID: 3956
	private int W = 120;

	// Token: 0x04000F75 RID: 3957
	private int H;

	// Token: 0x04000F76 RID: 3958
	private int dem;

	// Token: 0x04000F77 RID: 3959
	private long last;

	// Token: 0x04000F78 RID: 3960
	private long curr;
}
