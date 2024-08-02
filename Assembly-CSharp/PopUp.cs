using System;

// Token: 0x0200008A RID: 138
public class PopUp
{
	// Token: 0x0600075D RID: 1885 RVA: 0x000846C0 File Offset: 0x000828C0
	public PopUp(string info, int x, int y)
	{
		this.sayWidth = 100;
		bool flag = info.Length < 10;
		if (flag)
		{
			this.sayWidth = 60;
		}
		bool flag2 = GameCanvas.w == 128;
		if (flag2)
		{
			this.sayWidth = 128;
		}
		this.says = mFont.tahoma_7b_dark.splitFontArray(info, this.sayWidth - 10);
		this.sayRun = 7;
		this.cx = x - this.sayWidth / 2 - 1;
		this.cy = y - 15 + this.sayRun - this.says.Length * 12 - 10;
		this.cw = this.sayWidth + 2;
		this.ch = (this.says.Length + 1) * 12 + 1;
		while (this.cw % 10 != 0)
		{
			this.cw++;
		}
		while (this.ch % 10 != 0)
		{
			this.ch++;
		}
		bool flag3 = x >= 0 && x <= 24;
		if (flag3)
		{
			this.cx += this.cw / 2 + 30;
		}
		bool flag4 = x <= TileMap.tmw * 24 && x >= TileMap.tmw * 24 - 24;
		if (flag4)
		{
			this.cx -= this.cw / 2 + 6;
		}
		while (this.cx <= 30)
		{
			this.cx += 2;
		}
		while (this.cx + this.cw >= TileMap.tmw * 24 - 30)
		{
			this.cx -= 2;
		}
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x0008489C File Offset: 0x00082A9C
	public static void loadBg()
	{
		bool flag = PopUp.goc == null;
		if (flag)
		{
			PopUp.goc = GameCanvas.loadImage("/mainImage/myTexture2dbd3.png");
		}
		bool flag2 = PopUp.imgPopUp == null;
		if (flag2)
		{
			PopUp.imgPopUp = GameCanvas.loadImage("/mainImage/myTexture2dimgPopup.png");
		}
		bool flag3 = PopUp.imgPopUp2 == null;
		if (flag3)
		{
			PopUp.imgPopUp2 = GameCanvas.loadImage("/mainImage/myTexture2dimgPopup2.png");
		}
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x00084904 File Offset: 0x00082B04
	public void updateXYWH(string[] info, int x, int y)
	{
		this.sayWidth = 0;
		for (int i = 0; i < info.Length; i++)
		{
			bool flag = this.sayWidth < mFont.tahoma_7b_dark.getWidth(info[i]);
			if (flag)
			{
				this.sayWidth = mFont.tahoma_7b_dark.getWidth(info[i]);
			}
		}
		this.sayWidth += 20;
		this.says = info;
		this.sayRun = 7;
		this.cx = x - this.sayWidth / 2 - 1;
		this.cy = y - 15 + this.sayRun - this.says.Length * 12 - 10;
		this.cw = this.sayWidth + 2;
		this.ch = (this.says.Length + 1) * 12 + 1;
		while (this.cw % 10 != 0)
		{
			this.cw++;
		}
		while (this.ch % 10 != 0)
		{
			this.ch++;
		}
		bool flag2 = x >= 0 && x <= 24;
		if (flag2)
		{
			this.cx += this.cw / 2 + 30;
		}
		bool flag3 = x <= TileMap.tmw * 24 && x >= TileMap.tmw * 24 - 24;
		if (flag3)
		{
			this.cx -= this.cw / 2 + 6;
		}
		while (this.cx <= 30)
		{
			this.cx += 2;
		}
		while (this.cx + this.cw >= TileMap.tmw * 24 - 30)
		{
			this.cx -= 2;
		}
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x00005A5E File Offset: 0x00003C5E
	public static void addPopUp(int x, int y, string info)
	{
		PopUp.vPopups.addElement(new PopUp(info, x, y));
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x00005A74 File Offset: 0x00003C74
	public static void addPopUp(PopUp p)
	{
		PopUp.vPopups.addElement(p);
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00005A83 File Offset: 0x00003C83
	public static void removePopUp(PopUp p)
	{
		PopUp.vPopups.removeElement(p);
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x00084ADC File Offset: 0x00082CDC
	public void paintClipPopUp(mGraphics g, int x, int y, int w, int h, int color, bool isFocus)
	{
		bool flag = color == 1;
		if (flag)
		{
			g.fillRect(x, y, w, h, 16777215, 90);
		}
		else
		{
			g.fillRect(x, y, w, h, 0, 77);
		}
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x00084B20 File Offset: 0x00082D20
	public static void paintPopUp(mGraphics g, int x, int y, int w, int h, int color, bool isButton)
	{
		bool flag = !isButton;
		if (flag)
		{
			g.setColor(0);
			g.fillRect(x + 6, y, w - 14 + 1, h);
			g.fillRect(x, y + 6, w, h - 12 + 1);
			g.setColor(color);
			g.fillRect(x + 6, y + 1, w - 12, h - 2);
			g.fillRect(x + 1, y + 6, w - 2, h - 12);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 0, x, y, 0);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 2, x + w - 7, y, 0);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 1, x, y + h - 6, 0);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 3, x + w - 7, y + h - 6, 0);
		}
		else
		{
			Image arg = (color != 1) ? PopUp.imgPopUp : PopUp.imgPopUp2;
			g.drawRegion(arg, 0, 0, 10, 10, 0, x, y, 0);
			g.drawRegion(arg, 0, 20, 10, 10, 0, x + w - 10, y, 0);
			g.drawRegion(arg, 0, 50, 10, 10, 0, x, y + h - 10, 0);
			g.drawRegion(arg, 0, 70, 10, 10, 0, x + w - 10, y + h - 10, 0);
			int num = ((w - 20) % 10 != 0) ? ((w - 20) / 10 + 1) : ((w - 20) / 10);
			int num2 = ((h - 20) % 10 != 0) ? ((h - 20) / 10 + 1) : ((h - 20) / 10);
			for (int i = 0; i < num; i++)
			{
				g.drawRegion(arg, 0, 10, 10, 10, 0, x + 10 + i * 10, y, 0);
			}
			for (int j = 0; j < num2; j++)
			{
				g.drawRegion(arg, 0, 30, 10, 10, 0, x, y + 10 + j * 10, 0);
			}
			for (int k = 0; k < num; k++)
			{
				g.drawRegion(arg, 0, 60, 10, 10, 0, x + 10 + k * 10, y + h - 10, 0);
			}
			for (int l = 0; l < num2; l++)
			{
				g.drawRegion(arg, 0, 40, 10, 10, 0, x + w - 10, y + 10 + l * 10, 0);
			}
			g.setColor((color != 1) ? 16770503 : 12052656);
			g.fillRect(x + 10, y + 10, w - 20, h - 20);
		}
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x00084DBC File Offset: 0x00082FBC
	public void paint(mGraphics g)
	{
		bool flag = this.isPaint && this.says != null && ChatPopup.currChatPopup == null && !this.isHide;
		if (flag)
		{
			this.paintClipPopUp(g, this.cx, this.cy - GameCanvas.transY, this.cw, this.ch, (this.timeDelay != 0) ? 1 : 0, true);
			for (int i = 0; i < this.says.Length; i++)
			{
				((this.timeDelay != 0) ? mFont.tahoma_7b_green2 : mFont.tahoma_7b_white).drawString(g, this.says[i], this.cx + this.cw / 2, this.cy + (this.ch / 2 - this.says.Length * 12 / 2) + i * 12 - GameCanvas.transY, 2);
			}
		}
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x00084EA0 File Offset: 0x000830A0
	private void update()
	{
		bool flag = global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId == 0;
		if (flag)
		{
			bool flag2 = this.cx + this.cw >= GameScr.cmx && this.cx <= GameCanvas.w + GameScr.cmx && this.cy + this.ch >= GameScr.cmy && this.cy <= GameCanvas.h + GameScr.cmy;
			if (flag2)
			{
				this.isHide = false;
			}
			else
			{
				this.isHide = true;
			}
		}
		bool flag3 = global::Char.myCharz().taskMaint == null || (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId != 0);
		if (flag3)
		{
			bool flag4 = this.cx + this.cw / 2 >= global::Char.myCharz().cx - 100 && this.cx + this.cw / 2 <= global::Char.myCharz().cx + 100 && this.cy + this.ch >= GameScr.cmy && this.cy <= GameCanvas.h + GameScr.cmy;
			if (flag4)
			{
				this.isHide = false;
			}
			else
			{
				this.isHide = true;
			}
		}
		bool flag5 = this.timeDelay > 0;
		if (flag5)
		{
			this.timeDelay--;
			bool flag6 = this.timeDelay == 0 && this.command != null;
			if (flag6)
			{
				this.command.performAction();
			}
		}
		bool flag7 = !this.isWayPoint;
		if (!flag7)
		{
			bool flag8 = global::Char.myCharz().taskMaint != null;
			if (flag8)
			{
				bool flag9 = global::Char.myCharz().taskMaint.taskId == 0;
				if (flag9)
				{
					bool flag10 = global::Char.myCharz().taskMaint.index == 0;
					if (flag10)
					{
						this.isPaint = false;
					}
					bool flag11 = global::Char.myCharz().taskMaint.index == 1;
					if (flag11)
					{
						this.isPaint = true;
					}
					bool flag12 = global::Char.myCharz().taskMaint.index > 1 && global::Char.myCharz().taskMaint.index < 6;
					if (flag12)
					{
						this.isPaint = false;
					}
				}
				else
				{
					bool flag13 = !this.isPaint;
					if (flag13)
					{
						this.tDelay++;
						bool flag14 = this.tDelay == 50;
						if (flag14)
						{
							this.isPaint = true;
						}
					}
				}
			}
			else
			{
				bool flag15 = !this.isPaint;
				if (flag15)
				{
					Hint.isPaint = false;
					this.tDelay++;
					bool flag16 = this.tDelay == 50;
					if (flag16)
					{
						this.isPaint = true;
						Hint.isPaint = true;
					}
				}
			}
		}
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x00005A92 File Offset: 0x00003C92
	public void doClick(int timeDelay)
	{
		this.timeDelay = timeDelay;
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x00085184 File Offset: 0x00083384
	public static void paintAll(mGraphics g)
	{
		for (int i = 0; i < PopUp.vPopups.size(); i++)
		{
			((PopUp)PopUp.vPopups.elementAt(i)).paint(g);
		}
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x000851C4 File Offset: 0x000833C4
	public static void updateAll()
	{
		for (int i = 0; i < PopUp.vPopups.size(); i++)
		{
			((PopUp)PopUp.vPopups.elementAt(i)).update();
		}
	}

	// Token: 0x04000F59 RID: 3929
	public static MyVector vPopups = new MyVector();

	// Token: 0x04000F5A RID: 3930
	public int sayWidth;

	// Token: 0x04000F5B RID: 3931
	public int sayRun;

	// Token: 0x04000F5C RID: 3932
	public string[] says;

	// Token: 0x04000F5D RID: 3933
	public int cx;

	// Token: 0x04000F5E RID: 3934
	public int cy;

	// Token: 0x04000F5F RID: 3935
	public int cw;

	// Token: 0x04000F60 RID: 3936
	public int ch;

	// Token: 0x04000F61 RID: 3937
	public static int f;

	// Token: 0x04000F62 RID: 3938
	public static int tF;

	// Token: 0x04000F63 RID: 3939
	public static int dir;

	// Token: 0x04000F64 RID: 3940
	public bool isWayPoint;

	// Token: 0x04000F65 RID: 3941
	public int tDelay;

	// Token: 0x04000F66 RID: 3942
	private int timeDelay;

	// Token: 0x04000F67 RID: 3943
	public Command command;

	// Token: 0x04000F68 RID: 3944
	public bool isPaint = true;

	// Token: 0x04000F69 RID: 3945
	public bool isHide;

	// Token: 0x04000F6A RID: 3946
	public static Image goc;

	// Token: 0x04000F6B RID: 3947
	public static Image imgPopUp;

	// Token: 0x04000F6C RID: 3948
	public static Image imgPopUp2;

	// Token: 0x04000F6D RID: 3949
	public Image imgFocus;

	// Token: 0x04000F6E RID: 3950
	public Image imgUnFocus;
}
