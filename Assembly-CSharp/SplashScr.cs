using System;

// Token: 0x020000AF RID: 175
public class SplashScr : mScreen
{
	// Token: 0x06000952 RID: 2386 RVA: 0x0000630D File Offset: 0x0000450D
	public SplashScr()
	{
		SplashScr.instance = this;
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x0000631D File Offset: 0x0000451D
	public static void loadSplashScr()
	{
		SplashScr.splashScrStat = 0;
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x00093EE4 File Offset: 0x000920E4
	public override void update()
	{
		bool flag = SplashScr.splashScrStat == 30 && !this.isCheckConnect;
		if (flag)
		{
			this.isCheckConnect = true;
			bool flag2 = Rms.loadRMSInt("isPlaySound") != -1;
			if (flag2)
			{
				GameCanvas.isPlaySound = (Rms.loadRMSInt("isPlaySound") == 1);
			}
			bool isPlaySound = GameCanvas.isPlaySound;
			if (isPlaySound)
			{
				SoundMn.gI().loadSound(TileMap.mapID);
			}
			SoundMn.gI().getStrOption();
			ServerListScreen.loadIP();
		}
		SplashScr.splashScrStat++;
		ServerListScreen.updateDeleteData();
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x00093F80 File Offset: 0x00092180
	public static void loadIP()
	{
		bool flag = Rms.loadRMSInt("svselect") == -1;
		if (flag)
		{
			int num = 0;
			bool flag2 = mResources.language > 0;
			if (flag2)
			{
				for (int i = 0; i < (int)mResources.language; i++)
				{
					num += ServerListScreen.lengthServer[i];
				}
			}
			bool flag3 = ServerListScreen.serverPriority == -1;
			if (flag3)
			{
				ServerListScreen.ipSelect = num + Res.random(0, ServerListScreen.lengthServer[(int)mResources.language]);
			}
			else
			{
				ServerListScreen.ipSelect = (int)ServerListScreen.serverPriority;
			}
			Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
			GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
			GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
			mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
			GameCanvas.connect();
		}
		else
		{
			ServerListScreen.ipSelect = Rms.loadRMSInt("svselect");
			bool flag4 = ServerListScreen.ipSelect > ServerListScreen.nameServer.Length - 1;
			if (flag4)
			{
				ServerListScreen.ipSelect = (int)ServerListScreen.serverPriority;
				Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
			}
			GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
			GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
			mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
			GameCanvas.connect();
		}
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x000940F4 File Offset: 0x000922F4
	public override void paint(mGraphics g)
	{
		bool flag = SplashScr.imgLogo != null && SplashScr.splashScrStat < 30;
		if (flag)
		{
			g.setColor(16777215);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			g.drawImage(SplashScr.imgLogo, GameCanvas.w / 2, GameCanvas.h / 2, 3);
		}
		bool flag2 = SplashScr.nData != -1;
		if (flag2)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			g.drawImage(LoginScr.imgTitle, GameCanvas.w / 2, GameCanvas.h / 2 - 24, StaticObj.BOTTOM_HCENTER);
			GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.h / 2 + 24, g);
			mFont.tahoma_7b_white.drawString(g, mResources.downloading_data + SplashScr.nData * 100 / SplashScr.maxData + "%", GameCanvas.w / 2, GameCanvas.h / 2, 2);
		}
		else
		{
			bool flag3 = SplashScr.splashScrStat >= 30;
			if (flag3)
			{
				g.setColor(0);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.hh, g);
				bool flag4 = ServerListScreen.cmdDeleteRMS != null;
				if (flag4)
				{
					mFont.tahoma_7_white.drawString(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
				}
			}
		}
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x00006326 File Offset: 0x00004526
	public static void loadImg()
	{
		SplashScr.imgLogo = GameCanvas.loadImage("/gamelogo.png");
	}

	// Token: 0x04001124 RID: 4388
	public static int splashScrStat;

	// Token: 0x04001125 RID: 4389
	private bool isCheckConnect;

	// Token: 0x04001126 RID: 4390
	private bool isSwitchToLogin;

	// Token: 0x04001127 RID: 4391
	public static int nData = -1;

	// Token: 0x04001128 RID: 4392
	public static int maxData = -1;

	// Token: 0x04001129 RID: 4393
	public static SplashScr instance;

	// Token: 0x0400112A RID: 4394
	public static Image imgLogo;
}
