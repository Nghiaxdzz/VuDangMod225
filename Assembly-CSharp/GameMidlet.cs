using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class GameMidlet
{
	// Token: 0x0600029D RID: 669 RVA: 0x00004355 File Offset: 0x00002555
	public GameMidlet()
	{
		this.initGame();
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0003EF88 File Offset: 0x0003D188
	public void initGame()
	{
		GameMidlet.instance = this;
		MotherCanvas.instance = new MotherCanvas();
		Session_ME.gI().setHandler(Controller.gI());
		Session_ME2.gI().setHandler(Controller.gI());
		Session_ME2.isMainSession = false;
		GameMidlet.instance = this;
		GameMidlet.gameCanvas = new GameCanvas();
		GameMidlet.gameCanvas.start();
		SplashScr.loadImg();
		SplashScr.loadSplashScr();
		GameCanvas.currentScreen = new SplashScr();
	}

	// Token: 0x0600029F RID: 671 RVA: 0x0003F000 File Offset: 0x0003D200
	public void exit()
	{
		bool flag = Main.typeClient == 6;
		if (flag)
		{
			mSystem.exitWP();
		}
		else
		{
			GameCanvas.bRun = false;
			mSystem.gcc();
			this.notifyDestroyed();
		}
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x00004366 File Offset: 0x00002566
	public static void sendSMS(string data, string to, Command successAction, Command failAction)
	{
		Cout.println("SEND SMS");
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x00004374 File Offset: 0x00002574
	public static void flatForm(string url)
	{
		Cout.LogWarning("PLATFORM REQUEST: " + url);
		Application.OpenURL(url);
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x0000438F File Offset: 0x0000258F
	public void notifyDestroyed()
	{
		Main.exit();
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x00004398 File Offset: 0x00002598
	public void platformRequest(string url)
	{
		Cout.LogWarning("PLATFORM REQUEST: " + url);
		Application.OpenURL(url);
	}

	// Token: 0x0400060B RID: 1547
	public static string IP = "112.213.94.23";

	// Token: 0x0400060C RID: 1548
	public static int PORT = 14445;

	// Token: 0x0400060D RID: 1549
	public static string IP2;

	// Token: 0x0400060E RID: 1550
	public static int PORT2;

	// Token: 0x0400060F RID: 1551
	public static sbyte PROVIDER;

	// Token: 0x04000610 RID: 1552
	public static string VERSION = "2.2.5";

	// Token: 0x04000611 RID: 1553
	public static GameCanvas gameCanvas;

	// Token: 0x04000612 RID: 1554
	public static GameMidlet instance;

	// Token: 0x04000613 RID: 1555
	public static bool isConnect2;

	// Token: 0x04000614 RID: 1556
	public static bool isBackWindowsPhone;
}
