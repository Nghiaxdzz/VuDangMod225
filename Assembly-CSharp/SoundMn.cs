using System;

// Token: 0x020000AB RID: 171
public class SoundMn
{
	// Token: 0x06000917 RID: 2327 RVA: 0x00005FBB File Offset: 0x000041BB
	public static void init(SoundMn.AssetManager ac)
	{
		Sound.setActivity(ac);
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x00093628 File Offset: 0x00091828
	public static SoundMn gI()
	{
		bool flag = SoundMn.gIz == null;
		if (flag)
		{
			SoundMn.gIz = new SoundMn();
		}
		return SoundMn.gIz;
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x00093658 File Offset: 0x00091858
	public void loadSound(int mapID)
	{
		Sound.init(new int[]
		{
			SoundMn.AIR_SHIP,
			SoundMn.RAIN,
			SoundMn.TAITAONANGLUONG
		}, new int[]
		{
			SoundMn.GET_ITEM,
			SoundMn.MOVE,
			SoundMn.LOW_PUNCH,
			SoundMn.LOW_KICK,
			SoundMn.FLY,
			SoundMn.JUMP,
			SoundMn.PANEL_OPEN,
			SoundMn.BUTTON_CLOSE,
			SoundMn.BUTTON_CLICK,
			SoundMn.MEDIUM_PUNCH,
			SoundMn.MEDIUM_KICK,
			SoundMn.PANEL_OPEN,
			SoundMn.EAT_PEAN,
			SoundMn.OPEN_DIALOG,
			SoundMn.NORMAL_KAME,
			SoundMn.NAMEK_KAME,
			SoundMn.XAYDA_KAME,
			SoundMn.EXPLODE_1,
			SoundMn.EXPLODE_2,
			SoundMn.TRAIDAT_KAME,
			SoundMn.HP_UP,
			SoundMn.THAIDUONGHASAN,
			SoundMn.HOISINH,
			SoundMn.GONG,
			SoundMn.KHICHAY,
			SoundMn.BIG_EXPLODE,
			SoundMn.NAMEK_LAZER,
			SoundMn.NAMEK_CHARGE,
			SoundMn.RADAR_CLICK,
			SoundMn.RADAR_ITEM,
			SoundMn.FIREWORK
		});
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x000937A0 File Offset: 0x000919A0
	public void getSoundOption()
	{
		bool flag = GameCanvas.loginScr.isLogin2 && global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId >= 2;
		if (flag)
		{
			Panel.strTool = new string[]
			{
				mResources.radaCard,
				mResources.quayso,
				mResources.gameInfo,
				mResources.change_flag,
				mResources.change_zone,
				mResources.chat_world,
				mResources.account,
				mResources.option,
				mResources.change_account,
				mResources.REGISTOPROTECT
			};
			bool havePet = global::Char.myCharz().havePet;
			if (havePet)
			{
				Panel.strTool = new string[]
				{
					mResources.radaCard,
					mResources.quayso,
					mResources.gameInfo,
					mResources.pet,
					mResources.change_flag,
					mResources.change_zone,
					mResources.chat_world,
					mResources.account,
					mResources.option,
					mResources.change_account,
					mResources.REGISTOPROTECT
				};
			}
		}
		else
		{
			Panel.strTool = new string[]
			{
				mResources.radaCard,
				mResources.quayso,
				mResources.gameInfo,
				mResources.change_flag,
				mResources.change_zone,
				mResources.chat_world,
				mResources.account,
				mResources.option,
				mResources.change_account
			};
			bool havePet2 = global::Char.myCharz().havePet;
			if (havePet2)
			{
				Panel.strTool = new string[]
				{
					mResources.radaCard,
					mResources.quayso,
					mResources.gameInfo,
					mResources.pet,
					mResources.change_flag,
					mResources.change_zone,
					mResources.chat_world,
					mResources.account,
					mResources.option,
					mResources.change_account
				};
			}
		}
		bool isDelAcc = SoundMn.IsDelAcc;
		if (isDelAcc)
		{
			string[] array = new string[Panel.strTool.Length + 1];
			for (int i = 0; i < Panel.strTool.Length; i++)
			{
				array[i] = Panel.strTool[i];
			}
			array[Panel.strTool.Length] = mResources.delacc;
			Panel.strTool = array;
		}
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x000939E0 File Offset: 0x00091BE0
	public void getStrOption()
	{
		bool isPC = Main.isPC;
		if (isPC)
		{
			Panel.strCauhinh = new string[]
			{
				(global::Char.myCharz().idHat == -1) ? mResources.hat_on : mResources.hat_off,
				(!global::Char.isPaintAura) ? mResources.aura_on : mResources.aura_off,
				(!GameCanvas.isPlaySound) ? mResources.turnOnSound : mResources.turnOffSound,
				(mGraphics.zoomLevel <= 1) ? mResources.x2Screen : mResources.x1Screen
			};
		}
		else
		{
			Panel.strCauhinh = new string[]
			{
				(global::Char.myCharz().idHat == -1) ? mResources.hat_on : mResources.hat_off,
				(!global::Char.isPaintAura) ? mResources.aura_on : mResources.aura_off,
				(!GameCanvas.isPlaySound) ? mResources.turnOnSound : mResources.turnOffSound,
				(GameScr.isAnalog != 0) ? mResources.turnOffAnalog : mResources.turnOnAnalog
			};
		}
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x00005FC5 File Offset: 0x000041C5
	public void HP_MPup()
	{
		Sound.playSound(SoundMn.HP_UP, 0.5f);
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00093AD0 File Offset: 0x00091CD0
	public void charPunch(bool isKick, float volumn)
	{
		bool flag = !global::Char.myCharz().me;
		if (flag)
		{
			SoundMn.volume /= 2f;
		}
		bool flag2 = volumn <= 0f;
		if (flag2)
		{
			volumn = 0.01f;
		}
		int num = Res.random(0, 3);
		if (isKick)
		{
			Sound.playSound((num != 0) ? SoundMn.MEDIUM_KICK : SoundMn.LOW_KICK, 0.1f);
		}
		else
		{
			Sound.playSound((num != 0) ? SoundMn.MEDIUM_PUNCH : SoundMn.LOW_PUNCH, 0.1f);
		}
		this.poolCount++;
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x00005FD8 File Offset: 0x000041D8
	public void thaiduonghasan()
	{
		Sound.playSound(SoundMn.THAIDUONGHASAN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x00005FF9 File Offset: 0x000041F9
	public void rain()
	{
		Sound.playMus(SoundMn.RAIN, 0.3f, true);
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x0000600D File Offset: 0x0000420D
	public void gongName()
	{
		Sound.playSound(SoundMn.NAMEK_CHARGE, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x0000602E File Offset: 0x0000422E
	public void gong()
	{
		Sound.playSound(SoundMn.GONG, 0.2f);
		this.poolCount++;
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x0000604F File Offset: 0x0000424F
	public void getItem()
	{
		Sound.playSound(SoundMn.GET_ITEM, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x00093B70 File Offset: 0x00091D70
	public void soundToolOption()
	{
		GameCanvas.isPlaySound = !GameCanvas.isPlaySound;
		bool isPlaySound = GameCanvas.isPlaySound;
		if (isPlaySound)
		{
			SoundMn.gI().loadSound(TileMap.mapID);
			Rms.saveRMSInt("isPlaySound", 1);
		}
		else
		{
			SoundMn.gI().closeSound();
			Rms.saveRMSInt("isPlaySound", 0);
		}
		this.getStrOption();
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x00093BD4 File Offset: 0x00091DD4
	public void AuraToolOption()
	{
		bool isPaintAura = global::Char.isPaintAura;
		if (isPaintAura)
		{
			Rms.saveRMSInt("isPaintAura", 0);
			global::Char.isPaintAura = false;
		}
		else
		{
			Rms.saveRMSInt("isPaintAura", 1);
			global::Char.isPaintAura = true;
		}
		this.getStrOption();
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x00006070 File Offset: 0x00004270
	public void HatToolOption()
	{
		Service.gI().sendOptHat();
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void update()
	{
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x0000607E File Offset: 0x0000427E
	public void closeSound()
	{
		Sound.stopAll = true;
		this.stopAll();
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x00093C1C File Offset: 0x00091E1C
	public void openSound()
	{
		bool flag = Sound.music == null;
		if (flag)
		{
			this.loadSound(0);
		}
		Sound.stopAll = false;
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x0000608E File Offset: 0x0000428E
	public void bigeExlode()
	{
		Sound.playSound(SoundMn.BIG_EXPLODE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x000060AF File Offset: 0x000042AF
	public void explode_1()
	{
		Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x000060AF File Offset: 0x000042AF
	public void explode_2()
	{
		Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x000060D0 File Offset: 0x000042D0
	public void traidatKame()
	{
		Sound.playSound(SoundMn.TRAIDAT_KAME, 1f);
		this.poolCount++;
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x000060F1 File Offset: 0x000042F1
	public void namekKame()
	{
		Sound.playSound(SoundMn.NAMEK_KAME, 0.3f);
		this.poolCount++;
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x00006112 File Offset: 0x00004312
	public void nameLazer()
	{
		Sound.playSound(SoundMn.NAMEK_LAZER, 0.3f);
		this.poolCount++;
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x00006133 File Offset: 0x00004333
	public void xaydaKame()
	{
		Sound.playSound(SoundMn.XAYDA_KAME, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x00093C48 File Offset: 0x00091E48
	public void mobKame(int type)
	{
		int id = SoundMn.XAYDA_KAME;
		bool flag = type == 13;
		if (flag)
		{
			id = SoundMn.NORMAL_KAME;
		}
		Sound.playSound(id, 0.1f);
		this.poolCount++;
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x00093C88 File Offset: 0x00091E88
	public void charRun(float volumn)
	{
		bool flag = !global::Char.myCharz().me;
		if (flag)
		{
			SoundMn.volume /= 2f;
			bool flag2 = volumn <= 0f;
			if (flag2)
			{
				volumn = 0.01f;
			}
		}
		bool flag3 = GameCanvas.gameTick % 8 == 0;
		if (flag3)
		{
			Sound.playSound(SoundMn.MOVE, volumn);
			this.poolCount++;
		}
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x00093CFC File Offset: 0x00091EFC
	public void monkeyRun(float volumn)
	{
		bool flag = GameCanvas.gameTick % 8 == 0;
		if (flag)
		{
			Sound.playSound(SoundMn.KHICHAY, 0.2f);
			this.poolCount++;
		}
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x00006154 File Offset: 0x00004354
	public void charFall()
	{
		Sound.playSound(SoundMn.MOVE, 0.1f);
		this.poolCount++;
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x00006175 File Offset: 0x00004375
	public void charJump()
	{
		Sound.playSound(SoundMn.MOVE, 0.2f);
		this.poolCount++;
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x00006196 File Offset: 0x00004396
	public void panelOpen()
	{
		Sound.playSound(SoundMn.PANEL_OPEN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x000061B7 File Offset: 0x000043B7
	public void buttonClose()
	{
		Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x000061D8 File Offset: 0x000043D8
	public void buttonClick()
	{
		Sound.playSound(SoundMn.BUTTON_CLICK, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void stopMove()
	{
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x000061F9 File Offset: 0x000043F9
	public void charFly()
	{
		Sound.playSound(SoundMn.FLY, 0.2f);
		this.poolCount++;
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void stopFly()
	{
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x000061B7 File Offset: 0x000043B7
	public void openMenu()
	{
		Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x0000621A File Offset: 0x0000441A
	public void panelClick()
	{
		Sound.playSound(SoundMn.PANEL_CLICK, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x0000623B File Offset: 0x0000443B
	public void eatPeans()
	{
		Sound.playSound(SoundMn.EAT_PEAN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x0000625C File Offset: 0x0000445C
	public void openDialog()
	{
		Sound.playSound(SoundMn.OPEN_DIALOG, 0.5f);
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x0000626F File Offset: 0x0000446F
	public void hoisinh()
	{
		Sound.playSound(SoundMn.HOISINH, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x00006290 File Offset: 0x00004490
	public void taitao()
	{
		Sound.playMus(SoundMn.TAITAONANGLUONG, 0.5f, true);
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void taitaoPause()
	{
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x00093D38 File Offset: 0x00091F38
	public bool isPlayRain()
	{
		bool result;
		try
		{
			result = Sound.isPlayingSound();
		}
		catch (Exception)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x0001A69C File Offset: 0x0001889C
	public bool isPlayAirShip()
	{
		return false;
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x00093D68 File Offset: 0x00091F68
	public void airShip()
	{
		SoundMn.cout++;
		bool flag = SoundMn.cout % 2 == 0;
		if (flag)
		{
			Sound.playMus(SoundMn.AIR_SHIP, 0.3f, false);
		}
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void pauseAirShip()
	{
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void resumeAirShip()
	{
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x000062A4 File Offset: 0x000044A4
	public void stopAll()
	{
		Sound.stopAllz();
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x000062AD File Offset: 0x000044AD
	public void backToRegister()
	{
		Session_ME.gI().close();
		GameCanvas.panel.hide();
		GameCanvas.loginScr.actRegister();
		GameCanvas.loginScr.switchToMe();
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x00093DA4 File Offset: 0x00091FA4
	public void newKame()
	{
		this.poolCount++;
		bool flag = this.poolCount % 15 == 0;
		if (flag)
		{
			Sound.playSound(SoundMn.TRAIDAT_KAME, 0.5f);
		}
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x000062DC File Offset: 0x000044DC
	public void radarClick()
	{
		Sound.playSound(SoundMn.RADAR_CLICK, 0.5f);
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x000062EF File Offset: 0x000044EF
	public void radarItem()
	{
		Sound.playSound(SoundMn.RADAR_ITEM, 0.5f);
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x00006302 File Offset: 0x00004502
	public static void playSound(int x, int y, int id, float volume)
	{
		Sound.playSound(id, volume);
	}

	// Token: 0x040010F7 RID: 4343
	public static bool IsDelAcc;

	// Token: 0x040010F8 RID: 4344
	public static SoundMn gIz;

	// Token: 0x040010F9 RID: 4345
	public static bool isSound = true;

	// Token: 0x040010FA RID: 4346
	public static float volume = 0.5f;

	// Token: 0x040010FB RID: 4347
	private static int MAX_VOLUME = 10;

	// Token: 0x040010FC RID: 4348
	public static SoundMn.MediaPlayer[] music;

	// Token: 0x040010FD RID: 4349
	public static SoundMn.SoundPool[] sound;

	// Token: 0x040010FE RID: 4350
	public static int[] soundID;

	// Token: 0x040010FF RID: 4351
	public static int AIR_SHIP;

	// Token: 0x04001100 RID: 4352
	public static int RAIN = 1;

	// Token: 0x04001101 RID: 4353
	public static int TAITAONANGLUONG = 2;

	// Token: 0x04001102 RID: 4354
	public static int GET_ITEM;

	// Token: 0x04001103 RID: 4355
	public static int MOVE = 1;

	// Token: 0x04001104 RID: 4356
	public static int LOW_PUNCH = 2;

	// Token: 0x04001105 RID: 4357
	public static int LOW_KICK = 3;

	// Token: 0x04001106 RID: 4358
	public static int FLY = 4;

	// Token: 0x04001107 RID: 4359
	public static int JUMP = 5;

	// Token: 0x04001108 RID: 4360
	public static int PANEL_OPEN = 6;

	// Token: 0x04001109 RID: 4361
	public static int BUTTON_CLOSE = 7;

	// Token: 0x0400110A RID: 4362
	public static int BUTTON_CLICK = 8;

	// Token: 0x0400110B RID: 4363
	public static int MEDIUM_PUNCH = 9;

	// Token: 0x0400110C RID: 4364
	public static int MEDIUM_KICK = 10;

	// Token: 0x0400110D RID: 4365
	public static int PANEL_CLICK = 11;

	// Token: 0x0400110E RID: 4366
	public static int EAT_PEAN = 12;

	// Token: 0x0400110F RID: 4367
	public static int OPEN_DIALOG = 13;

	// Token: 0x04001110 RID: 4368
	public static int NORMAL_KAME = 14;

	// Token: 0x04001111 RID: 4369
	public static int NAMEK_KAME = 15;

	// Token: 0x04001112 RID: 4370
	public static int XAYDA_KAME = 16;

	// Token: 0x04001113 RID: 4371
	public static int EXPLODE_1 = 17;

	// Token: 0x04001114 RID: 4372
	public static int EXPLODE_2 = 18;

	// Token: 0x04001115 RID: 4373
	public static int TRAIDAT_KAME = 19;

	// Token: 0x04001116 RID: 4374
	public static int HP_UP = 20;

	// Token: 0x04001117 RID: 4375
	public static int THAIDUONGHASAN = 21;

	// Token: 0x04001118 RID: 4376
	public static int HOISINH = 22;

	// Token: 0x04001119 RID: 4377
	public static int GONG = 23;

	// Token: 0x0400111A RID: 4378
	public static int KHICHAY = 24;

	// Token: 0x0400111B RID: 4379
	public static int BIG_EXPLODE = 25;

	// Token: 0x0400111C RID: 4380
	public static int NAMEK_LAZER = 26;

	// Token: 0x0400111D RID: 4381
	public static int NAMEK_CHARGE = 27;

	// Token: 0x0400111E RID: 4382
	public static int RADAR_CLICK = 28;

	// Token: 0x0400111F RID: 4383
	public static int RADAR_ITEM = 29;

	// Token: 0x04001120 RID: 4384
	public static int FIREWORK = 30;

	// Token: 0x04001121 RID: 4385
	public bool freePool;

	// Token: 0x04001122 RID: 4386
	public int poolCount;

	// Token: 0x04001123 RID: 4387
	public static int cout = 1;

	// Token: 0x020000AC RID: 172
	public class MediaPlayer
	{
	}

	// Token: 0x020000AD RID: 173
	public class SoundPool
	{
	}

	// Token: 0x020000AE RID: 174
	public class AssetManager
	{
	}
}
