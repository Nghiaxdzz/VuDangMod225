using System;

// Token: 0x02000044 RID: 68
public class InfoDlg
{
	// Token: 0x060003A6 RID: 934 RVA: 0x00052010 File Offset: 0x00050210
	public static void show(string title, string subtitle, int delay)
	{
		bool flag = title != null;
		if (flag)
		{
			InfoDlg.isShow = true;
			InfoDlg.title = title;
			InfoDlg.subtitke = subtitle;
			InfoDlg.delay = delay;
		}
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x000048B3 File Offset: 0x00002AB3
	public static void showWait()
	{
		InfoDlg.show(mResources.PLEASEWAIT, null, 1000);
		InfoDlg.isLock = true;
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x000048CD File Offset: 0x00002ACD
	public static void showWait(string str)
	{
		InfoDlg.show(str, null, 700);
		InfoDlg.isLock = true;
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x00052040 File Offset: 0x00050240
	public static void paint(mGraphics g)
	{
		bool flag = InfoDlg.isShow && (!InfoDlg.isLock || InfoDlg.delay <= 4990) && !GameScr.isPaintAlert;
		if (flag)
		{
			int num = 10;
			GameCanvas.paintz.paintPopUp(GameCanvas.hw - 75, num, 150, 55, g);
			bool flag2 = InfoDlg.isLock;
			if (flag2)
			{
				GameCanvas.paintShukiren(GameCanvas.hw - mFont.tahoma_8b.getWidth(InfoDlg.title) / 2 - 10, num + 28, g);
				mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw + 5, num + 21, 2);
			}
			else
			{
				bool flag3 = InfoDlg.subtitke != null;
				if (flag3)
				{
					mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw, num + 13, 2);
					mFont.tahoma_7_green2.drawString(g, InfoDlg.subtitke, GameCanvas.hw, num + 30, 2);
				}
				else
				{
					mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw, num + 21, 2);
				}
			}
		}
	}

	// Token: 0x060003AA RID: 938 RVA: 0x00052150 File Offset: 0x00050350
	public static void update()
	{
		bool flag = InfoDlg.delay > 0;
		if (flag)
		{
			InfoDlg.delay--;
			bool flag2 = InfoDlg.delay == 0;
			if (flag2)
			{
				InfoDlg.hide();
			}
		}
	}

	// Token: 0x060003AB RID: 939 RVA: 0x000048E3 File Offset: 0x00002AE3
	public static void hide()
	{
		InfoDlg.title = string.Empty;
		InfoDlg.subtitke = null;
		InfoDlg.isLock = false;
		InfoDlg.delay = 0;
		InfoDlg.isShow = false;
	}

	// Token: 0x04000838 RID: 2104
	public static bool isShow;

	// Token: 0x04000839 RID: 2105
	private static string title;

	// Token: 0x0400083A RID: 2106
	private static string subtitke;

	// Token: 0x0400083B RID: 2107
	public static int delay;

	// Token: 0x0400083C RID: 2108
	public static bool isLock;
}
