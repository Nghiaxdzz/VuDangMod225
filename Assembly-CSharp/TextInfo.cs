using System;

// Token: 0x020000BA RID: 186
public class TextInfo
{
	// Token: 0x06000976 RID: 2422 RVA: 0x000063EF File Offset: 0x000045EF
	public static void reset()
	{
		TextInfo.dx = 0;
		TextInfo.tx = 0;
		TextInfo.isBack = false;
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x00099FC4 File Offset: 0x000981C4
	public static void paint(mGraphics g, string str, int x, int y, int w, int h, mFont f)
	{
		bool flag = TextInfo.wStr != f.getWidth(str) || !TextInfo.laststring.Equals(str);
		if (flag)
		{
			TextInfo.laststring = str;
			TextInfo.dx = 0;
			TextInfo.wStr = f.getWidth(str);
			TextInfo.isBack = false;
			TextInfo.tx = 0;
		}
		g.setClip(x, y, w, h);
		bool flag2 = TextInfo.wStr > w;
		if (flag2)
		{
			f.drawString(g, str, x - TextInfo.dx, y, 0);
		}
		else
		{
			f.drawString(g, str, x + w / 2, y, 2);
		}
		GameCanvas.resetTrans(g);
		bool flag3 = TextInfo.wStr <= w;
		if (!flag3)
		{
			bool flag4 = !TextInfo.isBack;
			if (flag4)
			{
				TextInfo.tx++;
				bool flag5 = TextInfo.tx > 50;
				if (flag5)
				{
					TextInfo.dx++;
					bool flag6 = TextInfo.dx >= TextInfo.wStr;
					if (flag6)
					{
						TextInfo.tx = 0;
						TextInfo.dx = -w + 30;
						TextInfo.isBack = true;
					}
				}
			}
			else
			{
				bool flag7 = TextInfo.dx < 0;
				if (flag7)
				{
					int num = w + TextInfo.dx >> 1;
					TextInfo.dx += num;
				}
				bool flag8 = TextInfo.dx > 0;
				if (flag8)
				{
					TextInfo.dx = 0;
				}
				bool flag9 = TextInfo.dx == 0;
				if (flag9)
				{
					TextInfo.tx++;
					bool flag10 = TextInfo.tx == 50;
					if (flag10)
					{
						TextInfo.tx = 0;
						TextInfo.isBack = false;
					}
				}
			}
		}
	}

	// Token: 0x0400119D RID: 4509
	public static int dx;

	// Token: 0x0400119E RID: 4510
	public static int tx;

	// Token: 0x0400119F RID: 4511
	public static int wStr;

	// Token: 0x040011A0 RID: 4512
	public static bool isBack;

	// Token: 0x040011A1 RID: 4513
	public static string laststring = string.Empty;
}
