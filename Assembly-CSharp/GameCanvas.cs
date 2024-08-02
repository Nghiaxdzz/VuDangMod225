using System;
using System.Threading;
using Assets.src.g;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class GameCanvas : IActionListener
{
	// Token: 0x0600024B RID: 587 RVA: 0x00038D90 File Offset: 0x00036F90
	public GameCanvas()
	{
		int num = Rms.loadRMSInt("languageVersion");
		if (num != -1)
		{
			if (num != 2)
			{
				Main.main.doClearRMS();
				Rms.saveRMSInt("languageVersion", 2);
			}
		}
		else
		{
			Rms.saveRMSInt("languageVersion", 2);
		}
		GameCanvas.clearOldData = Rms.loadRMSInt(GameMidlet.VERSION);
		bool flag = GameCanvas.clearOldData != 1;
		if (flag)
		{
			Main.main.doClearRMS();
			Rms.saveRMSInt(GameMidlet.VERSION, 1);
		}
		this.initGame();
	}

	// Token: 0x0600024C RID: 588 RVA: 0x00038E38 File Offset: 0x00037038
	public static string getPlatformName()
	{
		return "Pc platform xxx";
	}

	// Token: 0x0600024D RID: 589 RVA: 0x00038E50 File Offset: 0x00037050
	public void initGame()
	{
		MotherCanvas.instance.setChildCanvas(this);
		GameCanvas.w = MotherCanvas.instance.getWidthz();
		GameCanvas.h = MotherCanvas.instance.getHeightz();
		GameCanvas.hw = GameCanvas.w / 2;
		GameCanvas.hh = GameCanvas.h / 2;
		GameCanvas.isTouch = true;
		bool flag = GameCanvas.w >= 240;
		if (flag)
		{
			GameCanvas.isTouchControl = true;
		}
		bool flag2 = GameCanvas.w < 320;
		if (flag2)
		{
			GameCanvas.isTouchControlSmallScreen = true;
		}
		bool flag3 = GameCanvas.w >= 320;
		if (flag3)
		{
			GameCanvas.isTouchControlLargeScreen = true;
		}
		GameCanvas.msgdlg = new MsgDlg();
		bool flag4 = GameCanvas.h <= 160;
		if (flag4)
		{
			Paint.hTab = 15;
			mScreen.cmdH = 17;
		}
		GameScr.d = ((GameCanvas.w <= GameCanvas.h) ? GameCanvas.h : GameCanvas.w) + 20;
		GameCanvas.instance = this;
		mFont.init();
		mScreen.ITEM_HEIGHT = mFont.tahoma_8b.getHeight() + 8;
		this.initPaint();
		this.loadDust();
		this.loadWaterSplash();
		GameCanvas.panel = new Panel();
		GameCanvas.imgShuriken = GameCanvas.loadImage("/mainImage/myTexture2df.png");
		int num = Rms.loadRMSInt("clienttype");
		bool flag5 = num != -1;
		if (flag5)
		{
			bool flag6 = num > 7;
			if (flag6)
			{
				Rms.saveRMSInt("clienttype", mSystem.clientType);
			}
			else
			{
				mSystem.clientType = num;
			}
		}
		bool flag7 = mSystem.clientType == 7 && (Rms.loadRMSString("fake") == null || Rms.loadRMSString("fake") == string.Empty);
		if (flag7)
		{
			GameCanvas.imgShuriken = GameCanvas.loadImage("/mainImage/wait.png");
		}
		GameCanvas.imgClear = GameCanvas.loadImage("/mainImage/myTexture2der.png");
		GameCanvas.img12 = GameCanvas.loadImage("/mainImage/12+.png");
		GameCanvas.debugUpdate = new MyVector();
		GameCanvas.debugPaint = new MyVector();
		GameCanvas.debugSession = new MyVector();
		for (int i = 0; i < 3; i++)
		{
			GameCanvas.imgBorder[i] = GameCanvas.loadImage("/mainImage/myTexture2dbd" + i + ".png");
		}
		GameCanvas.borderConnerW = mGraphics.getImageWidth(GameCanvas.imgBorder[0]);
		GameCanvas.borderConnerH = mGraphics.getImageHeight(GameCanvas.imgBorder[0]);
		GameCanvas.borderCenterW = mGraphics.getImageWidth(GameCanvas.imgBorder[1]);
		GameCanvas.borderCenterH = mGraphics.getImageHeight(GameCanvas.imgBorder[1]);
		Panel.graphics = Rms.loadRMSInt("lowGraphic");
		GameCanvas.lowGraphic = (Rms.loadRMSInt("lowGraphic") == 1);
		GameScr.isPaintChatVip = (Rms.loadRMSInt("serverchat") != 1);
		global::Char.isPaintAura = (Rms.loadRMSInt("isPaintAura") == 1);
		Res.init();
		SmallImage.loadBigImage();
		Panel.WIDTH_PANEL = 176;
		bool flag8 = Panel.WIDTH_PANEL > GameCanvas.w;
		if (flag8)
		{
			Panel.WIDTH_PANEL = GameCanvas.w;
		}
		InfoMe.gI().loadCharId();
		Command.btn0left = GameCanvas.loadImage("/mainImage/btn0left.png");
		Command.btn0mid = GameCanvas.loadImage("/mainImage/btn0mid.png");
		Command.btn0right = GameCanvas.loadImage("/mainImage/btn0right.png");
		Command.btn1left = GameCanvas.loadImage("/mainImage/btn1left.png");
		Command.btn1mid = GameCanvas.loadImage("/mainImage/btn1mid.png");
		Command.btn1right = GameCanvas.loadImage("/mainImage/btn1right.png");
		GameCanvas.serverScreen = new ServerListScreen();
		GameCanvas.img12 = GameCanvas.loadImage("/mainImage/12+.png");
		for (int j = 0; j < 7; j++)
		{
			GameCanvas.imgBlue[j] = GameCanvas.loadImage("/effectdata/blue/" + j + ".png");
			GameCanvas.imgViolet[j] = GameCanvas.loadImage("/effectdata/violet/" + j + ".png");
		}
		ServerListScreen.createDeleteRMS();
		GameCanvas.serverScr = new ServerScr();
	}

	// Token: 0x0600024E RID: 590 RVA: 0x00039240 File Offset: 0x00037440
	public static GameCanvas gI()
	{
		return GameCanvas.instance;
	}

	// Token: 0x0600024F RID: 591 RVA: 0x000041EB File Offset: 0x000023EB
	public void initPaint()
	{
		GameCanvas.paintz = new Paint();
	}

	// Token: 0x06000250 RID: 592 RVA: 0x000041F8 File Offset: 0x000023F8
	public static void closeKeyBoard()
	{
		mGraphics.addYWhenOpenKeyBoard = 0;
		GameCanvas.timeOpenKeyBoard = 0;
		Main.closeKeyBoard();
	}

	// Token: 0x06000251 RID: 593 RVA: 0x00039258 File Offset: 0x00037458
	public void update()
	{
		bool flag = GameCanvas.gameTick % 5 == 0;
		if (flag)
		{
			GameCanvas.timeNow = mSystem.currentTimeMillis();
		}
		Res.updateOnScreenDebug();
		try
		{
			bool visible = global::TouchScreenKeyboard.visible;
			if (visible)
			{
				GameCanvas.timeOpenKeyBoard++;
				bool flag2 = GameCanvas.timeOpenKeyBoard > ((!Main.isWindowsPhone) ? 10 : 5);
				if (flag2)
				{
					mGraphics.addYWhenOpenKeyBoard = 94;
				}
			}
			else
			{
				mGraphics.addYWhenOpenKeyBoard = 0;
				GameCanvas.timeOpenKeyBoard = 0;
			}
			GameCanvas.debugUpdate.removeAllElements();
			long num = mSystem.currentTimeMillis();
			bool flag3 = num - GameCanvas.timeTickEff1 >= 780L && !GameCanvas.isEff1;
			if (flag3)
			{
				GameCanvas.timeTickEff1 = num;
				GameCanvas.isEff1 = true;
			}
			else
			{
				GameCanvas.isEff1 = false;
			}
			bool flag4 = num - GameCanvas.timeTickEff2 >= 7800L && !GameCanvas.isEff2;
			if (flag4)
			{
				GameCanvas.timeTickEff2 = num;
				GameCanvas.isEff2 = true;
			}
			else
			{
				GameCanvas.isEff2 = false;
			}
			bool flag5 = GameCanvas.taskTick > 0;
			if (flag5)
			{
				GameCanvas.taskTick--;
			}
			GameCanvas.gameTick++;
			bool flag6 = GameCanvas.gameTick > 10000;
			if (flag6)
			{
				bool flag7 = mSystem.currentTimeMillis() - GameCanvas.lastTimePress > 20000L && GameCanvas.currentScreen == GameCanvas.loginScr;
				if (flag7)
				{
					bool isAutoLogin = VuDang.isAutoLogin;
					if (isAutoLogin)
					{
						new Thread(new ThreadStart(VuDang.AutoLogin)).Start();
					}
					else
					{
						GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
					}
				}
				GameCanvas.gameTick = 0;
			}
			bool flag8 = GameCanvas.currentScreen != null;
			if (flag8)
			{
				bool flag9 = ChatPopup.serverChatPopUp != null;
				if (flag9)
				{
					ChatPopup.serverChatPopUp.update();
					ChatPopup.serverChatPopUp.updateKey();
				}
				else
				{
					bool flag10 = ChatPopup.currChatPopup != null;
					if (flag10)
					{
						ChatPopup.currChatPopup.update();
						ChatPopup.currChatPopup.updateKey();
					}
					else
					{
						bool flag11 = GameCanvas.currentDialog != null;
						if (flag11)
						{
							GameCanvas.debug("B", 0);
							GameCanvas.currentDialog.update();
						}
						else
						{
							bool showMenu = GameCanvas.menu.showMenu;
							if (showMenu)
							{
								GameCanvas.debug("C", 0);
								GameCanvas.menu.updateMenu();
								GameCanvas.debug("D", 0);
								GameCanvas.menu.updateMenuKey();
							}
							else
							{
								bool isShow = GameCanvas.panel.isShow;
								if (isShow)
								{
									GameCanvas.panel.update();
									bool flag12 = GameCanvas.isPointer(GameCanvas.panel.X, GameCanvas.panel.Y, GameCanvas.panel.W, GameCanvas.panel.H);
									if (flag12)
									{
										GameCanvas.isFocusPanel2 = false;
									}
									bool flag13 = GameCanvas.panel2 != null && GameCanvas.panel2.isShow;
									if (flag13)
									{
										GameCanvas.panel2.update();
										bool flag14 = GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H);
										if (flag14)
										{
											GameCanvas.isFocusPanel2 = true;
										}
									}
									bool flag15 = GameCanvas.panel2 != null;
									if (flag15)
									{
										bool flag16 = GameCanvas.isFocusPanel2;
										if (flag16)
										{
											GameCanvas.panel2.updateKey();
										}
										else
										{
											GameCanvas.panel.updateKey();
										}
									}
									else
									{
										GameCanvas.panel.updateKey();
									}
									bool flag17 = GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow;
									if (flag17)
									{
										GameCanvas.panel.chatTFUpdateKey();
									}
									else
									{
										bool flag18 = GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow;
										if (flag18)
										{
											GameCanvas.panel2.chatTFUpdateKey();
										}
										else
										{
											bool flag19 = (GameCanvas.isPointer(GameCanvas.panel.X, GameCanvas.panel.Y, GameCanvas.panel.W, GameCanvas.panel.H) && GameCanvas.panel2 != null) || GameCanvas.panel2 == null;
											if (flag19)
											{
												GameCanvas.panel.updateKey();
											}
											else
											{
												bool flag20 = GameCanvas.panel2 != null && GameCanvas.panel2.isShow && GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H);
												if (flag20)
												{
													GameCanvas.panel2.updateKey();
												}
											}
										}
									}
									bool flag21 = GameCanvas.isPointer(GameCanvas.panel.X + GameCanvas.panel.W, GameCanvas.panel.Y, GameCanvas.w - GameCanvas.panel.W * 2, GameCanvas.panel.H) && GameCanvas.isPointerJustRelease && GameCanvas.panel.isDoneCombine;
									if (flag21)
									{
										GameCanvas.panel.hide();
									}
								}
							}
						}
					}
				}
				GameCanvas.debug("E", 0);
				bool flag22 = !GameCanvas.isLoading;
				if (flag22)
				{
					GameCanvas.currentScreen.update();
				}
				GameCanvas.debug("F", 0);
				bool flag23 = !GameCanvas.panel.isShow && ChatPopup.serverChatPopUp == null;
				if (flag23)
				{
					GameCanvas.currentScreen.updateKey();
				}
				Hint.update();
				SoundMn.gI().update();
			}
			GameCanvas.debug("Ix", 0);
			global::Timer.update();
			GameCanvas.debug("Hx", 0);
			InfoDlg.update();
			GameCanvas.debug("G", 0);
			bool flag24 = this.resetToLoginScr;
			if (flag24)
			{
				this.resetToLoginScr = false;
				this.doResetToLoginScr(GameCanvas.serverScreen);
			}
			GameCanvas.debug("Zzz", 0);
			bool isConnectOK = Controller.isConnectOK;
			if (isConnectOK)
			{
				bool isMain = Controller.isMain;
				if (isMain)
				{
					GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
					GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
					Cout.println("Connect ok");
					ServerListScreen.testConnect = 2;
					Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
					Rms.saveIP(GameMidlet.IP + ":" + GameMidlet.PORT);
					Service.gI().setClientType();
					Service.gI().androidPack();
				}
				else
				{
					Service.gI().setClientType2();
					Service.gI().androidPack2();
				}
				Controller.isConnectOK = false;
			}
			bool isDisconnected = Controller.isDisconnected;
			if (isDisconnected)
			{
				Debug.Log("disconnect");
				bool flag25 = !Controller.isMain;
				if (flag25)
				{
					bool flag26 = GameCanvas.currentScreen == GameCanvas.serverScreen && !Service.reciveFromMainSession;
					if (flag26)
					{
						GameCanvas.serverScreen.cancel();
					}
					bool flag27 = GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession;
					if (flag27)
					{
						this.onDisconnected();
					}
				}
				else
				{
					this.onDisconnected();
				}
				Controller.isDisconnected = false;
			}
			bool isConnectionFail = Controller.isConnectionFail;
			if (isConnectionFail)
			{
				Debug.Log("connect fail");
				bool flag28 = !Controller.isMain;
				if (flag28)
				{
					bool flag29 = GameCanvas.currentScreen == GameCanvas.serverScreen && ServerListScreen.isGetData && !Service.reciveFromMainSession;
					if (flag29)
					{
						GameCanvas.serverScreen.cancel();
					}
					bool flag30 = GameCanvas.currentScreen == GameCanvas.loginScr && !Service.reciveFromMainSession;
					if (flag30)
					{
						this.onConnectionFail();
					}
				}
				else
				{
					this.onConnectionFail();
				}
				Controller.isConnectionFail = false;
			}
			bool flag31 = Main.isResume;
			if (flag31)
			{
				Main.isResume = false;
				bool flag32 = GameCanvas.currentDialog != null && GameCanvas.currentDialog.left != null && GameCanvas.currentDialog.left.actionListener != null;
				if (flag32)
				{
					GameCanvas.currentDialog.left.performAction();
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000252 RID: 594 RVA: 0x00039A60 File Offset: 0x00037C60
	public void onDisconnected()
	{
		bool isConnectionFail = Controller.isConnectionFail;
		if (isConnectionFail)
		{
			Controller.isConnectionFail = false;
		}
		GameCanvas.isResume = true;
		Session_ME.gI().clearSendingMessage();
		Session_ME2.gI().clearSendingMessage();
		bool isLoadingData = Controller.isLoadingData;
		if (isLoadingData)
		{
			GameCanvas.instance.resetToLoginScrz();
			GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
			Controller.isDisconnected = false;
		}
		else
		{
			global::Char.isLoadingMap = false;
			bool isMain = Controller.isMain;
			if (isMain)
			{
				ServerListScreen.testConnect = 0;
			}
			GameCanvas.instance.resetToLoginScrz();
			bool flag = Main.typeClient == 6;
			if (flag)
			{
				bool flag2 = GameCanvas.currentScreen != GameCanvas.serverScreen && GameCanvas.currentScreen != GameCanvas.loginScr;
				if (flag2)
				{
					bool isAutoLogin = VuDang.isAutoLogin;
					if (isAutoLogin)
					{
						new Thread(new ThreadStart(VuDang.AutoLogin)).Start();
					}
					else
					{
						GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
					}
				}
			}
			else
			{
				bool isAutoLogin2 = VuDang.isAutoLogin;
				if (isAutoLogin2)
				{
					new Thread(new ThreadStart(VuDang.AutoLogin)).Start();
				}
				else
				{
					GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
				}
			}
			mSystem.endKey();
		}
	}

	// Token: 0x06000253 RID: 595 RVA: 0x00039B8C File Offset: 0x00037D8C
	public void onConnectionFail()
	{
		bool flag = GameCanvas.currentScreen.Equals(SplashScr.instance);
		if (flag)
		{
			bool flag2 = ServerListScreen.hasConnected != null;
			if (flag2)
			{
				bool flag3 = !ServerListScreen.hasConnected[0];
				if (flag3)
				{
					ServerListScreen.hasConnected[0] = true;
					ServerListScreen.ipSelect = 0;
					GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
					Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
					GameCanvas.connect();
				}
				else
				{
					bool flag4 = !ServerListScreen.hasConnected[2];
					if (flag4)
					{
						ServerListScreen.hasConnected[2] = true;
						ServerListScreen.ipSelect = 2;
						GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
						Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
						GameCanvas.connect();
					}
					else
					{
						GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
					}
				}
			}
			else
			{
				GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
			}
		}
		else
		{
			Session_ME.gI().clearSendingMessage();
			Session_ME2.gI().clearSendingMessage();
			ServerListScreen.isWait = false;
			bool isLoadingData = Controller.isLoadingData;
			if (isLoadingData)
			{
				GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
				Controller.isConnectionFail = false;
			}
			else
			{
				GameCanvas.isResume = true;
				LoginScr.isContinueToLogin = false;
				bool flag5 = GameCanvas.loginScr != null;
				if (flag5)
				{
					GameCanvas.instance.resetToLoginScrz();
				}
				else
				{
					GameCanvas.loginScr = new LoginScr();
				}
				LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
				bool flag6 = GameCanvas.currentScreen != GameCanvas.serverScreen;
				if (flag6)
				{
					GameCanvas.startOK(mResources.lost_connection + LoginScr.serverName, 888395, null);
				}
				else
				{
					GameCanvas.endDlg();
				}
				global::Char.isLoadingMap = false;
				bool isMain = Controller.isMain;
				if (isMain)
				{
					ServerListScreen.testConnect = 0;
				}
				mSystem.endKey();
			}
		}
	}

	// Token: 0x06000254 RID: 596 RVA: 0x00039D60 File Offset: 0x00037F60
	public static bool isWaiting()
	{
		return InfoDlg.isShow || (GameCanvas.msgdlg != null && GameCanvas.msgdlg.info.Equals(mResources.PLEASEWAIT)) || global::Char.isLoadingMap || LoginScr.isContinueToLogin;
	}

	// Token: 0x06000255 RID: 597 RVA: 0x00039DB0 File Offset: 0x00037FB0
	public static void connect()
	{
		bool flag = !Session_ME.gI().isConnected();
		if (flag)
		{
			Session_ME.gI().connect(GameMidlet.IP, GameMidlet.PORT);
		}
	}

	// Token: 0x06000256 RID: 598 RVA: 0x00039DE8 File Offset: 0x00037FE8
	public static void connect2()
	{
		bool flag = !Session_ME2.gI().isConnected();
		if (flag)
		{
			Res.outz(string.Concat(new object[]
			{
				"IP2= ",
				GameMidlet.IP2,
				" PORT 2= ",
				GameMidlet.PORT2
			}));
			Session_ME2.gI().connect(GameMidlet.IP2, GameMidlet.PORT2);
		}
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0000420D File Offset: 0x0000240D
	public static void resetTrans(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x06000258 RID: 600 RVA: 0x00039E54 File Offset: 0x00038054
	public void initGameCanvas()
	{
		GameCanvas.debug("SP2i1", 0);
		GameCanvas.w = MotherCanvas.instance.getWidthz();
		GameCanvas.h = MotherCanvas.instance.getHeightz();
		GameCanvas.debug("SP2i2", 0);
		GameCanvas.hw = GameCanvas.w / 2;
		GameCanvas.hh = GameCanvas.h / 2;
		GameCanvas.wd3 = GameCanvas.w / 3;
		GameCanvas.hd3 = GameCanvas.h / 3;
		GameCanvas.w2d3 = 2 * GameCanvas.w / 3;
		GameCanvas.h2d3 = 2 * GameCanvas.h / 3;
		GameCanvas.w3d4 = 3 * GameCanvas.w / 4;
		GameCanvas.h3d4 = 3 * GameCanvas.h / 4;
		GameCanvas.wd6 = GameCanvas.w / 6;
		GameCanvas.hd6 = GameCanvas.h / 6;
		GameCanvas.debug("SP2i3", 0);
		mScreen.initPos();
		GameCanvas.debug("SP2i4", 0);
		GameCanvas.debug("SP2i5", 0);
		GameCanvas.inputDlg = new InputDlg();
		GameCanvas.debug("SP2i6", 0);
		GameCanvas.listPoint = new MyVector();
		GameCanvas.debug("SP2i7", 0);
	}

	// Token: 0x06000259 RID: 601 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void start()
	{
	}

	// Token: 0x0600025A RID: 602 RVA: 0x00039F70 File Offset: 0x00038170
	public int getWidth()
	{
		return (int)ScaleGUI.WIDTH;
	}

	// Token: 0x0600025B RID: 603 RVA: 0x00039F88 File Offset: 0x00038188
	public int getHeight()
	{
		return (int)ScaleGUI.HEIGHT;
	}

	// Token: 0x0600025C RID: 604 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void debug(string s, int type)
	{
	}

	// Token: 0x0600025D RID: 605 RVA: 0x00039FA0 File Offset: 0x000381A0
	public void doResetToLoginScr(mScreen screen)
	{
		try
		{
			SoundMn.gI().stopAll();
			LoginScr.isContinueToLogin = false;
			TileMap.lastType = (TileMap.bgType = 0);
			global::Char.clearMyChar();
			GameScr.clearGameScr();
			GameScr.resetAllvector();
			InfoDlg.hide();
			GameScr.info1.hide();
			GameScr.info2.hide();
			GameScr.info2.cmdChat = null;
			Hint.isShow = false;
			ChatPopup.currChatPopup = null;
			Controller.isStopReadMessage = false;
			GameScr.loadCamera(true, -1, -1);
			GameScr.cmx = 100;
			GameCanvas.panel.currentTabIndex = 0;
			GameCanvas.panel.selected = (GameCanvas.isTouch ? -1 : 0);
			GameCanvas.panel.init();
			GameCanvas.panel2 = null;
			GameScr.isPaint = true;
			ClanMessage.vMessage.removeAllElements();
			GameScr.textTime.removeAllElements();
			GameScr.vClan.removeAllElements();
			GameScr.vFriend.removeAllElements();
			GameScr.vEnemies.removeAllElements();
			TileMap.vCurrItem.removeAllElements();
			BackgroudEffect.vBgEffect.removeAllElements();
			EffecMn.vEff.removeAllElements();
			Effect.newEff.removeAllElements();
			GameCanvas.menu.showMenu = false;
			GameCanvas.panel.vItemCombine.removeAllElements();
			GameCanvas.panel.isShow = false;
			bool flag = GameCanvas.panel.tabIcon != null;
			if (flag)
			{
				GameCanvas.panel.tabIcon.isShow = false;
			}
			bool flag2 = mGraphics.zoomLevel == 1;
			if (flag2)
			{
				SmallImage.clearHastable();
			}
			Session_ME.gI().close();
			Session_ME2.gI().close();
			screen.switchToMe();
		}
		catch (Exception ex)
		{
			Cout.println("Loi tai doResetToLoginScr " + ex.ToString());
		}
	}

	// Token: 0x0600025E RID: 606 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void showErrorForm(int type, string moreInfo)
	{
	}

	// Token: 0x0600025F RID: 607 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void paintCloud(mGraphics g)
	{
	}

	// Token: 0x06000260 RID: 608 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void updateBG()
	{
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0003A178 File Offset: 0x00038378
	public static void fillRect(mGraphics g, int color, int x, int y, int w, int h, int detalY)
	{
		g.setColor(color);
		int cmy = GameScr.cmy;
		bool flag = cmy > GameCanvas.h;
		if (flag)
		{
			cmy = GameCanvas.h;
		}
		g.fillRect(x, y - ((detalY != 0) ? (cmy >> detalY) : 0), w, h + ((detalY != 0) ? (cmy >> detalY) : 0));
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0003A1D4 File Offset: 0x000383D4
	public static void paintBackgroundtLayer(mGraphics g, int layer, int deltaY, int color1, int color2)
	{
		try
		{
			int num = layer - 1;
			bool flag = num == GameCanvas.imgBG.Length - 1 && (GameScr.gI().isRongThanXuatHien || GameScr.gI().isFireWorks);
			if (flag)
			{
				g.setColor(GameScr.gI().mautroi);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				bool flag2 = GameCanvas.typeBg == 2 || GameCanvas.typeBg == 4 || GameCanvas.typeBg == 7;
				if (flag2)
				{
					GameCanvas.drawSun1(g);
					GameCanvas.drawSun2(g);
				}
				bool flag3 = GameScr.gI().isFireWorks && !GameCanvas.lowGraphic;
				if (flag3)
				{
					FireWorkEff.paint(g);
				}
			}
			else
			{
				bool flag4 = GameCanvas.imgBG == null || GameCanvas.imgBG[num] == null;
				if (!flag4)
				{
					bool flag5 = GameCanvas.moveX[num] != 0;
					if (flag5)
					{
						GameCanvas.moveX[num] += GameCanvas.moveXSpeed[num];
					}
					int cmy = GameScr.cmy;
					bool flag6 = cmy > GameCanvas.h;
					if (flag6)
					{
						cmy = GameCanvas.h;
					}
					bool flag7 = GameCanvas.layerSpeed[num] != 0;
					if (flag7)
					{
						for (int i = -((GameScr.cmx + GameCanvas.moveX[num] >> GameCanvas.layerSpeed[num]) % GameCanvas.bgW[num]); i < GameScr.gW; i += GameCanvas.bgW[num])
						{
							g.drawImage(GameCanvas.imgBG[num], i, GameCanvas.yb[num] - ((deltaY > 0) ? (cmy >> deltaY) : 0), 0);
						}
					}
					else
					{
						for (int j = 0; j < GameScr.gW; j += GameCanvas.bgW[num])
						{
							g.drawImage(GameCanvas.imgBG[num], j, GameCanvas.yb[num] - ((deltaY > 0) ? (cmy >> deltaY) : 0), 0);
						}
					}
					bool flag8 = color1 != -1;
					if (flag8)
					{
						bool flag9 = num == GameCanvas.nBg - 1;
						if (flag9)
						{
							GameCanvas.fillRect(g, color1, 0, -(cmy >> deltaY), GameScr.gW, GameCanvas.yb[num], deltaY);
						}
						else
						{
							GameCanvas.fillRect(g, color1, 0, GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1], GameScr.gW, GameCanvas.yb[num] - (GameCanvas.yb[num - 1] + GameCanvas.bgH[num - 1]), deltaY);
						}
					}
					bool flag10 = color2 != -1;
					if (flag10)
					{
						bool flag11 = num == 0;
						if (flag11)
						{
							GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameScr.gH - (GameCanvas.yb[num] + GameCanvas.bgH[num]), deltaY);
						}
						else
						{
							GameCanvas.fillRect(g, color2, 0, GameCanvas.yb[num] + GameCanvas.bgH[num], GameScr.gW, GameCanvas.yb[num - 1] - (GameCanvas.yb[num] + GameCanvas.bgH[num]) + 80, deltaY);
						}
					}
					bool flag12 = GameCanvas.currentScreen == GameScr.instance;
					if (flag12)
					{
						bool flag13 = layer == 1 && GameCanvas.typeBg == 11;
						if (flag13)
						{
							g.drawImage(GameCanvas.imgSun2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 400, GameCanvas.yb[0] + 30 - (cmy >> 2), StaticObj.BOTTOM_HCENTER);
						}
						bool flag14 = layer == 1 && GameCanvas.typeBg == 13;
						if (flag14)
						{
							g.drawImage(GameCanvas.imgBG[1], -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 200, GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
							g.drawRegion(GameCanvas.imgBG[1], 0, 0, GameCanvas.bgW[1], GameCanvas.bgH[1], 2, -(GameScr.cmx >> GameCanvas.layerSpeed[0]) + 200 + GameCanvas.bgW[1], GameCanvas.yb[0] - (cmy >> 3) + 30, 0);
						}
						bool flag15 = layer == 3 && TileMap.mapID == 1;
						if (flag15)
						{
							for (int k = 0; k < TileMap.pxh / mGraphics.getImageHeight(GameCanvas.imgCaycot); k++)
							{
								g.drawImage(GameCanvas.imgCaycot, -(GameScr.cmx >> GameCanvas.layerSpeed[2]) + 300, k * mGraphics.getImageHeight(GameCanvas.imgCaycot) - (cmy >> 3), 0);
							}
						}
					}
					int x = -(GameScr.cmx + GameCanvas.moveX[num] >> GameCanvas.layerSpeed[num]);
					EffecMn.paintBackGroundUnderLayer(g, x, GameCanvas.yb[num] + GameCanvas.bgH[num] - (cmy >> deltaY), num);
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham paint bground: " + ex.ToString());
		}
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0003A6C8 File Offset: 0x000388C8
	public static void drawSun1(mGraphics g)
	{
		bool flag = GameCanvas.imgSun != null;
		if (flag)
		{
			g.drawImage(GameCanvas.imgSun, GameCanvas.sunX, GameCanvas.sunY, 0);
		}
		bool flag2 = !GameCanvas.isBoltEff;
		if (!flag2)
		{
			bool flag3 = GameCanvas.gameTick % 200 == 0;
			if (flag3)
			{
				GameCanvas.boltActive = true;
			}
			bool flag4 = GameCanvas.boltActive;
			if (flag4)
			{
				GameCanvas.tBolt++;
				bool flag5 = GameCanvas.tBolt == 10;
				if (flag5)
				{
					GameCanvas.tBolt = 0;
					GameCanvas.boltActive = false;
				}
				bool flag6 = GameCanvas.tBolt % 2 == 0;
				if (flag6)
				{
					g.setColor(16777215);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				}
			}
		}
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0003A78C File Offset: 0x0003898C
	public static void drawSun2(mGraphics g)
	{
		bool flag = GameCanvas.imgSun2 != null;
		if (flag)
		{
			g.drawImage(GameCanvas.imgSun2, GameCanvas.sunX2, GameCanvas.sunY2, 0);
		}
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0003A7C0 File Offset: 0x000389C0
	public static bool isHDVersion()
	{
		return mGraphics.zoomLevel > 1;
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0003A7E4 File Offset: 0x000389E4
	public static void paintBGGameScr(mGraphics g)
	{
		bool flag = !GameCanvas.isLoadBGok;
		if (flag)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		}
		bool isLoadingMap = global::Char.isLoadingMap;
		if (!isLoadingMap)
		{
			int gW = GameScr.gW;
			int gH = GameScr.gH;
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			bool xoaBackground = VuDang.XoaBackground;
			if (xoaBackground)
			{
				g.setColor(VuDang.GetColor());
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			}
			else
			{
				try
				{
					bool flag2 = GameCanvas.paintBG;
					if (flag2)
					{
						bool flag3 = GameCanvas.currentScreen != GameScr.gI();
						if (!flag3)
						{
							bool flag4 = TileMap.mapID == 137 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 120 || TileMap.isMapDouble;
							if (flag4)
							{
								g.setColor(0);
								g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
								return;
							}
							bool flag5 = TileMap.mapID != 138;
							if (!flag5)
							{
								g.setColor(6776679);
								g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
								return;
							}
						}
						bool flag6 = GameCanvas.typeBg == 0;
						if (flag6)
						{
							GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
							GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
							GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
							GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
						}
						else
						{
							bool flag7 = GameCanvas.typeBg == 1;
							if (flag7)
							{
								GameCanvas.paintBackgroundtLayer(g, 4, 6, -1, -1);
								GameCanvas.paintBackgroundtLayer(g, 3, 3, -1, -1);
								GameCanvas.fillRect(g, GameCanvas.colorTop[2], 0, -(GameScr.cmy >> 5), gW, GameCanvas.yb[2], 5);
								GameCanvas.fillRect(g, GameCanvas.colorBotton[2], 0, GameCanvas.yb[2] + GameCanvas.bgH[2] - (GameScr.cmy >> 3), gW, 70, 3);
								GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, -1);
								GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
							}
							else
							{
								bool flag8 = GameCanvas.typeBg == 2;
								if (flag8)
								{
									GameCanvas.paintBackgroundtLayer(g, 5, 10, GameCanvas.colorTop[4], GameCanvas.colorBotton[4]);
									GameCanvas.paintBackgroundtLayer(g, 4, 8, -1, GameCanvas.colorTop[2]);
									GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, GameCanvas.colorBotton[2]);
									GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
									GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
									GameCanvas.paintCloud(g);
								}
								else
								{
									bool flag9 = GameCanvas.typeBg == 3;
									if (flag9)
									{
										int num = GameScr.cmy - (325 - GameScr.gH23);
										g.translate(0, -num);
										GameCanvas.fillRect(g, (!GameScr.gI().isRongThanXuatHien && !GameScr.gI().isFireWorks) ? GameCanvas.colorTop[2] : GameScr.gI().mautroi, 0, num - (GameScr.cmy >> 3), gW, GameCanvas.yb[2] - num + (GameScr.cmy >> 3) + 100, 2);
										GameCanvas.paintBackgroundtLayer(g, 3, 2, -1, GameCanvas.colorBotton[2]);
										GameCanvas.paintBackgroundtLayer(g, 2, 0, -1, -1);
										GameCanvas.paintBackgroundtLayer(g, 1, 0, -1, GameCanvas.colorBotton[0]);
										g.translate(0, -g.getTranslateY());
									}
									else
									{
										bool flag10 = GameCanvas.typeBg == 4;
										if (flag10)
										{
											GameCanvas.paintBackgroundtLayer(g, 4, 7, GameCanvas.colorTop[3], -1);
											GameCanvas.paintBackgroundtLayer(g, 3, 3, -1, (!GameCanvas.isHDVersion()) ? GameCanvas.colorTop[1] : GameCanvas.colorBotton[2]);
											GameCanvas.paintBackgroundtLayer(g, 2, 2, GameCanvas.colorTop[1], GameCanvas.colorBotton[1]);
											GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
										}
										else
										{
											bool flag11 = GameCanvas.typeBg == 5;
											if (flag11)
											{
												GameCanvas.paintBackgroundtLayer(g, 4, 15, GameCanvas.colorTop[3], -1);
												GameCanvas.drawSun1(g);
												g.translate(100, 10);
												GameCanvas.drawSun1(g);
												g.translate(-100, -10);
												GameCanvas.drawSun2(g);
												GameCanvas.paintBackgroundtLayer(g, 3, 10, -1, -1);
												GameCanvas.paintBackgroundtLayer(g, 2, 6, -1, -1);
												GameCanvas.paintBackgroundtLayer(g, 1, 4, -1, -1);
												g.translate(0, 27);
												GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, -1);
												g.translate(0, 20);
												GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
												g.translate(-g.getTranslateX(), -g.getTranslateY());
											}
											else
											{
												bool flag12 = GameCanvas.typeBg == 6;
												if (flag12)
												{
													GameCanvas.paintBackgroundtLayer(g, 5, 10, GameCanvas.colorTop[4], GameCanvas.colorBotton[4]);
													GameCanvas.drawSun1(g);
													GameCanvas.drawSun2(g);
													g.translate(60, 40);
													GameCanvas.drawSun2(g);
													g.translate(-60, -40);
													GameCanvas.paintBackgroundtLayer(g, 4, 7, -1, GameCanvas.colorBotton[3]);
													BackgroudEffect.paintFarAll(g);
													GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, -1);
													GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
													GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
												}
												else
												{
													bool flag13 = GameCanvas.typeBg == 7;
													if (flag13)
													{
														GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
														GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, -1);
														GameCanvas.paintBackgroundtLayer(g, 2, 4, -1, -1);
														GameCanvas.paintBackgroundtLayer(g, 1, 3, -1, GameCanvas.colorBotton[0]);
													}
													else
													{
														bool flag14 = GameCanvas.typeBg == 8;
														if (flag14)
														{
															GameCanvas.paintBackgroundtLayer(g, 4, 8, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
															GameCanvas.drawSun1(g);
															GameCanvas.drawSun2(g);
															GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
															GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
															bool flag15 = ((TileMap.mapID < 92 || TileMap.mapID > 96) && TileMap.mapID != 51 && TileMap.mapID != 52) || GameCanvas.currentScreen == GameCanvas.loginScr;
															if (flag15)
															{
																GameCanvas.paintBackgroundtLayer(g, 1, 1, -1, GameCanvas.colorBotton[0]);
															}
														}
														else
														{
															bool flag16 = GameCanvas.typeBg == 9;
															if (flag16)
															{
																GameCanvas.paintBackgroundtLayer(g, 4, 8, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
																GameCanvas.drawSun1(g);
																GameCanvas.drawSun2(g);
																g.translate(-80, 20);
																GameCanvas.drawSun2(g);
																g.translate(80, -20);
																BackgroudEffect.paintFarAll(g);
																GameCanvas.paintBackgroundtLayer(g, 3, 5, -1, -1);
																GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, -1);
																GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
															}
															else
															{
																bool flag17 = GameCanvas.typeBg == 10;
																if (flag17)
																{
																	int num2 = GameScr.cmy - (380 - GameScr.gH23);
																	g.translate(0, -num2);
																	GameCanvas.fillRect(g, (!GameScr.gI().isRongThanXuatHien) ? GameCanvas.colorTop[1] : GameScr.gI().mautroi, 0, num2 - (GameScr.cmy >> 2), gW, GameCanvas.yb[1] - num2 + (GameScr.cmy >> 2) + 100, 2);
																	GameCanvas.paintBackgroundtLayer(g, 2, 2, -1, GameCanvas.colorBotton[1]);
																	GameCanvas.drawSun1(g);
																	GameCanvas.drawSun2(g);
																	GameCanvas.paintBackgroundtLayer(g, 1, 0, -1, -1);
																	g.translate(0, -g.getTranslateY());
																}
																else
																{
																	bool flag18 = GameCanvas.typeBg == 11;
																	if (flag18)
																	{
																		GameCanvas.paintBackgroundtLayer(g, 3, 6, GameCanvas.colorTop[2], GameCanvas.colorBotton[2]);
																		GameCanvas.drawSun1(g);
																		GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
																		GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
																	}
																	else
																	{
																		bool flag19 = GameCanvas.typeBg == 12;
																		if (flag19)
																		{
																			g.setColor(9161471);
																			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
																			GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, 14417919);
																			GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, 14417919);
																			GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, 14417919);
																			GameCanvas.paintCloud(g);
																		}
																		else
																		{
																			bool flag20 = GameCanvas.typeBg == 13;
																			if (flag20)
																			{
																				g.setColor(15268088);
																				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
																				GameCanvas.paintBackgroundtLayer(g, 1, 5, -1, 15268088);
																			}
																			else
																			{
																				bool flag21 = GameCanvas.typeBg == 15;
																				if (flag21)
																				{
																					g.setColor(2631752);
																					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
																					GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
																					GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
																				}
																				else
																				{
																					bool flag22 = GameCanvas.typeBg == 16;
																					if (flag22)
																					{
																						GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
																						for (int i = 0; i < GameCanvas.imgSunSpec.Length; i++)
																						{
																							g.drawImage(GameCanvas.imgSunSpec[i], GameCanvas.cloudX[i], GameCanvas.cloudY[i], 33);
																						}
																						GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
																						GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
																						GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
																					}
																					else
																					{
																						GameCanvas.fillRect(g, GameCanvas.colorBotton[3], 0, GameCanvas.yb[3] + GameCanvas.bgH[3], GameScr.gW, GameCanvas.yb[2] + GameCanvas.bgH[2], 6);
																						GameCanvas.paintBackgroundtLayer(g, 4, 6, GameCanvas.colorTop[3], GameCanvas.colorBotton[3]);
																						GameCanvas.drawSun1(g);
																						GameCanvas.paintBackgroundtLayer(g, 3, 4, -1, GameCanvas.colorBotton[2]);
																						GameCanvas.paintBackgroundtLayer(g, 2, 3, -1, GameCanvas.colorBotton[1]);
																						GameCanvas.paintBackgroundtLayer(g, 1, 2, -1, GameCanvas.colorBotton[0]);
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					else
					{
						g.setColor(2315859);
						g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
						bool flag23 = GameCanvas.tam != null;
						if (flag23)
						{
							for (int j = -((GameScr.cmx >> 2) % mGraphics.getImageWidth(GameCanvas.tam)); j < GameScr.gW; j += mGraphics.getImageWidth(GameCanvas.tam))
							{
								g.drawImage(GameCanvas.tam, j, (GameScr.cmy >> 3) + GameCanvas.h / 2 - 50, 0);
							}
						}
						g.setColor(5084791);
						g.fillRect(0, (GameScr.cmy >> 3) + GameCanvas.h / 2 - 50 + mGraphics.getImageHeight(GameCanvas.tam), gW, GameCanvas.h);
					}
				}
				catch (Exception)
				{
					g.setColor(0);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				}
			}
		}
	}

	// Token: 0x06000267 RID: 615 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void resetBg()
	{
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0003B304 File Offset: 0x00039504
	public static void getYBackground(int typeBg)
	{
		try
		{
			int gH = GameScr.gH23;
			switch (typeBg)
			{
			case 0:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 70;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 20;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 30;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 50;
				goto IL_5FE;
			case 1:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 120;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 40;
				GameCanvas.yb[2] = GameCanvas.yb[1] - 90;
				GameCanvas.yb[3] = GameCanvas.yb[2] - 25;
				goto IL_5FE;
			case 2:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 150;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 60;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] - 10;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4];
				goto IL_5FE;
			case 3:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 10;
				GameCanvas.yb[1] = GameCanvas.yb[0] + 80;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
				goto IL_5FE;
			case 4:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 130;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1];
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 20;
				GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 80;
				goto IL_5FE;
			case 5:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 40;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 10;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 15;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 50;
				goto IL_5FE;
			case 6:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 30;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 10;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 15;
				GameCanvas.yb[4] = GameCanvas.yb[3] - GameCanvas.bgH[4] + 15;
				goto IL_5FE;
			case 7:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 20;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 15;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 20;
				GameCanvas.yb[3] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 10;
				goto IL_5FE;
			case 8:
			{
				GameCanvas.yb[0] = gH - 103 + 150;
				bool flag = TileMap.mapID == 103;
				if (flag)
				{
					GameCanvas.yb[0] -= 100;
				}
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 40;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 10;
				goto IL_5FE;
			}
			case 9:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 100;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 22;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3];
				goto IL_5FE;
			case 10:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] - 45;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] - 10;
				goto IL_5FE;
			case 11:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 60;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 5;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] - 15;
				goto IL_5FE;
			case 12:
				GameCanvas.yb[0] = gH + 40;
				GameCanvas.yb[1] = GameCanvas.yb[0] - 40;
				GameCanvas.yb[2] = GameCanvas.yb[1] - 40;
				goto IL_5FE;
			case 13:
				GameCanvas.yb[0] = gH - 80;
				GameCanvas.yb[1] = GameCanvas.yb[0];
				goto IL_5FE;
			case 15:
				GameCanvas.yb[0] = gH - 20;
				GameCanvas.yb[1] = GameCanvas.yb[0] - 80;
				goto IL_5FE;
			case 16:
				GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 75;
				GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 50;
				GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
				GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 90;
				goto IL_5FE;
			}
			GameCanvas.yb[0] = gH - GameCanvas.bgH[0] + 75;
			GameCanvas.yb[1] = GameCanvas.yb[0] - GameCanvas.bgH[1] + 50;
			GameCanvas.yb[2] = GameCanvas.yb[1] - GameCanvas.bgH[2] + 50;
			GameCanvas.yb[3] = GameCanvas.yb[2] - GameCanvas.bgH[3] + 90;
			IL_5FE:;
		}
		catch (Exception)
		{
			int gH2 = GameScr.gH23;
			for (int i = 0; i < GameCanvas.yb.Length; i++)
			{
				GameCanvas.yb[i] = 1;
			}
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0003B960 File Offset: 0x00039B60
	public static void loadBG(int typeBG)
	{
		try
		{
			GameCanvas.isLoadBGok = true;
			bool flag = GameCanvas.typeBg == 12;
			if (flag)
			{
				BackgroudEffect.yfog = TileMap.pxh - 100;
			}
			else
			{
				BackgroudEffect.yfog = TileMap.pxh - 160;
			}
			BackgroudEffect.clearImage();
			GameCanvas.randomRaintEff(typeBG);
			bool flag2 = (TileMap.lastBgID == typeBG && TileMap.lastType == TileMap.bgType) || typeBG == -1;
			if (!flag2)
			{
				GameCanvas.transY = 12;
				TileMap.lastBgID = (int)((sbyte)typeBG);
				TileMap.lastType = (int)((sbyte)TileMap.bgType);
				GameCanvas.layerSpeed = new int[]
				{
					1,
					2,
					3,
					7,
					8
				};
				GameCanvas.moveX = new int[5];
				GameCanvas.moveXSpeed = new int[5];
				GameCanvas.typeBg = typeBG;
				GameCanvas.isBoltEff = false;
				GameScr.firstY = GameScr.cmy;
				GameCanvas.imgBG = null;
				GameCanvas.imgCloud = null;
				GameCanvas.imgSun = null;
				GameCanvas.imgCaycot = null;
				GameScr.firstY = -1;
				switch (GameCanvas.typeBg)
				{
				case 0:
				{
					GameCanvas.imgCaycot = GameCanvas.loadImageRMS("/bg/caycot.png");
					GameCanvas.layerSpeed = new int[]
					{
						1,
						3,
						5,
						7
					};
					GameCanvas.nBg = 4;
					bool flag3 = TileMap.bgType == 2;
					if (flag3)
					{
						GameCanvas.transY = 8;
					}
					goto IL_2FD;
				}
				case 1:
					GameCanvas.transY = 7;
					GameCanvas.nBg = 4;
					goto IL_2FD;
				case 2:
				{
					int[] array = new int[5];
					array[2] = 1;
					GameCanvas.moveX = array;
					int[] array2 = new int[5];
					array2[2] = 2;
					GameCanvas.moveXSpeed = array2;
					GameCanvas.nBg = 5;
					goto IL_2FD;
				}
				case 3:
					GameCanvas.nBg = 3;
					goto IL_2FD;
				case 4:
				{
					BackgroudEffect.addEffect(3);
					int[] array3 = new int[5];
					array3[1] = 1;
					GameCanvas.moveX = array3;
					int[] array4 = new int[5];
					array4[1] = 1;
					GameCanvas.moveXSpeed = array4;
					GameCanvas.nBg = 4;
					goto IL_2FD;
				}
				case 5:
					GameCanvas.nBg = 4;
					goto IL_2FD;
				case 6:
				{
					int[] array5 = new int[5];
					array5[0] = 1;
					GameCanvas.moveX = array5;
					int[] array6 = new int[5];
					array6[0] = 2;
					GameCanvas.moveXSpeed = array6;
					GameCanvas.nBg = 5;
					goto IL_2FD;
				}
				case 7:
					GameCanvas.nBg = 4;
					goto IL_2FD;
				case 8:
					GameCanvas.transY = 8;
					GameCanvas.nBg = 4;
					goto IL_2FD;
				case 9:
					BackgroudEffect.addEffect(9);
					GameCanvas.nBg = 4;
					goto IL_2FD;
				case 10:
					GameCanvas.nBg = 2;
					goto IL_2FD;
				case 11:
					GameCanvas.transY = 7;
					GameCanvas.layerSpeed[2] = 0;
					GameCanvas.nBg = 3;
					goto IL_2FD;
				case 12:
				{
					int[] array7 = new int[5];
					array7[0] = 1;
					array7[1] = 1;
					GameCanvas.moveX = array7;
					int[] array8 = new int[5];
					array8[0] = 2;
					array8[1] = 1;
					GameCanvas.moveXSpeed = array8;
					GameCanvas.nBg = 3;
					goto IL_2FD;
				}
				case 13:
					GameCanvas.nBg = 2;
					goto IL_2FD;
				case 15:
					Res.outz("HELL");
					GameCanvas.nBg = 2;
					goto IL_2FD;
				case 16:
					GameCanvas.layerSpeed = new int[]
					{
						1,
						3,
						5,
						7
					};
					GameCanvas.nBg = 4;
					goto IL_2FD;
				}
				GameCanvas.layerSpeed = new int[]
				{
					1,
					3,
					5,
					7
				};
				GameCanvas.nBg = 4;
				IL_2FD:
				bool flag4 = typeBG <= 16;
				if (flag4)
				{
					GameCanvas.skyColor = StaticObj.SKYCOLOR[GameCanvas.typeBg];
				}
				else
				{
					try
					{
						string path = string.Concat(new object[]
						{
							"/bg/b",
							GameCanvas.typeBg,
							3,
							".png"
						});
						bool flag5 = TileMap.bgType != 0;
						if (flag5)
						{
							path = string.Concat(new object[]
							{
								"/bg/b",
								GameCanvas.typeBg,
								3,
								"-",
								TileMap.bgType,
								".png"
							});
						}
						int[] array9 = new int[1];
						Image image = GameCanvas.loadImageRMS(path);
						image.getRGB(ref array9, 0, 1, mGraphics.getRealImageWidth(image) / 2, 0, 1, 1);
						GameCanvas.skyColor = array9[0];
					}
					catch (Exception)
					{
						GameCanvas.skyColor = StaticObj.SKYCOLOR[StaticObj.SKYCOLOR.Length - 1];
					}
				}
				GameCanvas.colorTop = new int[StaticObj.SKYCOLOR.Length];
				GameCanvas.colorBotton = new int[StaticObj.SKYCOLOR.Length];
				for (int i = 0; i < StaticObj.SKYCOLOR.Length; i++)
				{
					GameCanvas.colorTop[i] = StaticObj.SKYCOLOR[i];
					GameCanvas.colorBotton[i] = StaticObj.SKYCOLOR[i];
				}
				bool flag6 = GameCanvas.lowGraphic;
				if (flag6)
				{
					GameCanvas.tam = GameCanvas.loadImageRMS("/bg/b63.png");
				}
				else
				{
					GameCanvas.imgBG = new Image[GameCanvas.nBg];
					GameCanvas.bgW = new int[GameCanvas.nBg];
					GameCanvas.bgH = new int[GameCanvas.nBg];
					GameCanvas.colorBotton = new int[GameCanvas.nBg];
					GameCanvas.colorTop = new int[GameCanvas.nBg];
					bool flag7 = TileMap.bgType == 100;
					if (flag7)
					{
						GameCanvas.imgBG[0] = GameCanvas.loadImageRMS("/bg/b100.png");
						GameCanvas.imgBG[1] = GameCanvas.loadImageRMS("/bg/b100.png");
						GameCanvas.imgBG[2] = GameCanvas.loadImageRMS("/bg/b82-1.png");
						GameCanvas.imgBG[3] = GameCanvas.loadImageRMS("/bg/b93.png");
						for (int j = 0; j < GameCanvas.nBg; j++)
						{
							bool flag8 = GameCanvas.imgBG[j] != null;
							if (flag8)
							{
								int[] array10 = new int[1];
								GameCanvas.imgBG[j].getRGB(ref array10, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[j]) / 2, 0, 1, 1);
								GameCanvas.colorTop[j] = array10[0];
								array10 = new int[1];
								GameCanvas.imgBG[j].getRGB(ref array10, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[j]) / 2, mGraphics.getRealImageHeight(GameCanvas.imgBG[j]) - 1, 1, 1);
								GameCanvas.colorBotton[j] = array10[0];
								GameCanvas.bgW[j] = mGraphics.getImageWidth(GameCanvas.imgBG[j]);
								GameCanvas.bgH[j] = mGraphics.getImageHeight(GameCanvas.imgBG[j]);
							}
							else
							{
								bool flag9 = GameCanvas.nBg > 1;
								if (flag9)
								{
									GameCanvas.imgBG[j] = GameCanvas.loadImageRMS("/bg/b" + GameCanvas.typeBg + "0.png");
									GameCanvas.bgW[j] = mGraphics.getImageWidth(GameCanvas.imgBG[j]);
									GameCanvas.bgH[j] = mGraphics.getImageHeight(GameCanvas.imgBG[j]);
								}
							}
						}
					}
					else
					{
						for (int k = 0; k < GameCanvas.nBg; k++)
						{
							string path2 = string.Concat(new object[]
							{
								"/bg/b",
								GameCanvas.typeBg,
								k,
								".png"
							});
							bool flag10 = TileMap.bgType != 0;
							if (flag10)
							{
								path2 = string.Concat(new object[]
								{
									"/bg/b",
									GameCanvas.typeBg,
									k,
									"-",
									TileMap.bgType,
									".png"
								});
							}
							GameCanvas.imgBG[k] = GameCanvas.loadImageRMS(path2);
							bool flag11 = GameCanvas.imgBG[k] != null;
							if (flag11)
							{
								int[] array11 = new int[1];
								GameCanvas.imgBG[k].getRGB(ref array11, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[k]) / 2, 0, 1, 1);
								GameCanvas.colorTop[k] = array11[0];
								array11 = new int[1];
								GameCanvas.imgBG[k].getRGB(ref array11, 0, 1, mGraphics.getRealImageWidth(GameCanvas.imgBG[k]) / 2, mGraphics.getRealImageHeight(GameCanvas.imgBG[k]) - 1, 1, 1);
								GameCanvas.colorBotton[k] = array11[0];
								GameCanvas.bgW[k] = mGraphics.getImageWidth(GameCanvas.imgBG[k]);
								GameCanvas.bgH[k] = mGraphics.getImageHeight(GameCanvas.imgBG[k]);
							}
							else
							{
								bool flag12 = GameCanvas.nBg > 1;
								if (flag12)
								{
									GameCanvas.imgBG[k] = GameCanvas.loadImageRMS("/bg/b" + GameCanvas.typeBg + "0.png");
									GameCanvas.bgW[k] = mGraphics.getImageWidth(GameCanvas.imgBG[k]);
									GameCanvas.bgH[k] = mGraphics.getImageHeight(GameCanvas.imgBG[k]);
								}
							}
						}
					}
					GameCanvas.getYBackground(GameCanvas.typeBg);
					GameCanvas.cloudX = new int[]
					{
						GameScr.gW / 2 - 40,
						GameScr.gW / 2 + 40,
						GameScr.gW / 2 - 100,
						GameScr.gW / 2 - 80,
						GameScr.gW / 2 - 120
					};
					GameCanvas.cloudY = new int[]
					{
						130,
						100,
						150,
						140,
						80
					};
					GameCanvas.imgSunSpec = null;
					bool flag13 = GameCanvas.typeBg != 0;
					if (flag13)
					{
						bool flag14 = GameCanvas.typeBg == 2;
						if (flag14)
						{
							GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun0.png");
							GameCanvas.sunX = GameScr.gW / 2 + 50;
							GameCanvas.sunY = GameCanvas.yb[4] - 40;
						}
						else
						{
							bool flag15 = GameCanvas.typeBg == 4;
							if (flag15)
							{
								GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun2.png");
								GameCanvas.sunX = GameScr.gW / 2 + 30;
								GameCanvas.sunY = GameCanvas.yb[3];
							}
							else
							{
								bool flag16 = GameCanvas.typeBg == 7;
								if (flag16)
								{
									GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun3" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
									GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun4" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
									GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
									GameCanvas.sunY = GameCanvas.yb[3] - 80;
									GameCanvas.sunX2 = GameCanvas.sunX - 100;
									GameCanvas.sunY2 = GameCanvas.yb[3] - 30;
								}
								else
								{
									bool flag17 = GameCanvas.typeBg == 6;
									if (flag17)
									{
										GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun5" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
										GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun6" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
										GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
										GameCanvas.sunY = GameCanvas.yb[4];
										GameCanvas.sunX2 = GameCanvas.sunX - 100;
										GameCanvas.sunY2 = GameCanvas.yb[4] + 20;
									}
									else
									{
										bool flag18 = typeBG == 5;
										if (flag18)
										{
											GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun8" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
											GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun7" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
											GameCanvas.sunX = GameScr.gW / 2 - 50;
											GameCanvas.sunY = GameCanvas.yb[3] + 20;
											GameCanvas.sunX2 = GameScr.gW / 2 + 20;
											GameCanvas.sunY2 = GameCanvas.yb[3] - 30;
										}
										else
										{
											bool flag19 = GameCanvas.typeBg == 8 && TileMap.mapID < 90;
											if (flag19)
											{
												GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun9" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
												GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun10" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
												GameCanvas.sunX = GameScr.gW / 2 - 30;
												GameCanvas.sunY = GameCanvas.yb[3] + 60;
												GameCanvas.sunX2 = GameScr.gW / 2 + 20;
												GameCanvas.sunY2 = GameCanvas.yb[3] + 10;
											}
											else
											{
												switch (typeBG)
												{
												case 9:
													GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun11" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
													GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun12" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
													GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
													GameCanvas.sunY = GameCanvas.yb[4] + 20;
													GameCanvas.sunX2 = GameCanvas.sunX - 80;
													GameCanvas.sunY2 = GameCanvas.yb[4] + 40;
													goto IL_1054;
												case 10:
													GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun13" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
													GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/sun14" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
													GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
													GameCanvas.sunY = GameCanvas.yb[1] - 30;
													GameCanvas.sunX2 = GameCanvas.sunX - 80;
													GameCanvas.sunY2 = GameCanvas.yb[1];
													goto IL_1054;
												case 11:
													GameCanvas.imgSun = GameCanvas.loadImageRMS("/bg/sun15" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
													GameCanvas.imgSun2 = GameCanvas.loadImageRMS("/bg/b113" + ((TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty) + ".png");
													GameCanvas.sunX = GameScr.gW / 2 - 30;
													GameCanvas.sunY = GameCanvas.yb[2] - 30;
													goto IL_1054;
												case 12:
													GameCanvas.cloudY = new int[]
													{
														200,
														170,
														220,
														150,
														250
													};
													goto IL_1054;
												case 16:
													GameCanvas.cloudX = new int[]
													{
														90,
														170,
														250,
														320,
														400,
														450,
														500
													};
													GameCanvas.cloudY = new int[]
													{
														GameCanvas.yb[2] + 5,
														GameCanvas.yb[2] - 20,
														GameCanvas.yb[2] - 50,
														GameCanvas.yb[2] - 30,
														GameCanvas.yb[2] - 50,
														GameCanvas.yb[2],
														GameCanvas.yb[2] - 40
													};
													GameCanvas.imgSunSpec = new Image[7];
													for (int l = 0; l < GameCanvas.imgSunSpec.Length; l++)
													{
														int num = 161;
														bool flag20 = l == 0 || l == 2 || l == 3 || l == 2 || l == 6;
														if (flag20)
														{
															num = 160;
														}
														GameCanvas.imgSunSpec[l] = GameCanvas.loadImageRMS("/bg/sun" + num + ".png");
													}
													goto IL_1054;
												}
												GameCanvas.imgCloud = null;
												GameCanvas.imgSun = null;
												GameCanvas.imgSun2 = null;
												GameCanvas.imgSun = GameCanvas.loadImageRMS(string.Concat(new object[]
												{
													"/bg/sun",
													typeBG,
													(TileMap.bgType != 0) ? ("-" + TileMap.bgType) : string.Empty,
													".png"
												}));
												GameCanvas.sunX = GameScr.gW - GameScr.gW / 3;
												GameCanvas.sunY = GameCanvas.yb[2] - 30;
												IL_1054:;
											}
										}
									}
								}
							}
						}
					}
					GameCanvas.paintBG = false;
					bool flag21 = !GameCanvas.paintBG;
					if (flag21)
					{
						GameCanvas.paintBG = true;
					}
				}
			}
		}
		catch (Exception)
		{
			GameCanvas.isLoadBGok = false;
		}
	}

	// Token: 0x0600026A RID: 618 RVA: 0x0003CA24 File Offset: 0x0003AC24
	private static void randomRaintEff(int typeBG)
	{
		for (int i = 0; i < GameCanvas.bgRain.Length; i++)
		{
			bool flag = typeBG == GameCanvas.bgRain[i] && Res.random(0, 2) == 0;
			if (flag)
			{
				BackgroudEffect.addEffect(0);
				break;
			}
		}
	}

	// Token: 0x0600026B RID: 619 RVA: 0x0003CA70 File Offset: 0x0003AC70
	public void keyPressedz(int keyCode)
	{
		GameCanvas.lastTimePress = mSystem.currentTimeMillis();
		bool flag = (keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 122) || keyCode == 10 || keyCode == 8 || keyCode == 13 || keyCode == 32 || keyCode == 31;
		if (flag)
		{
			GameCanvas.keyAsciiPress = keyCode;
		}
		this.mapKeyPress(keyCode);
	}

	// Token: 0x0600026C RID: 620 RVA: 0x0003CACC File Offset: 0x0003ACCC
	public void mapKeyPress(int keyCode)
	{
		bool flag = GameCanvas.currentDialog != null;
		if (flag)
		{
			GameCanvas.currentDialog.keyPress(keyCode);
			GameCanvas.keyAsciiPress = 0;
		}
		else
		{
			GameCanvas.currentScreen.keyPress(keyCode);
			if (keyCode <= -22)
			{
				if (keyCode <= -38)
				{
					if (keyCode == -39)
					{
						goto IL_177;
					}
					if (keyCode != -38)
					{
						return;
					}
				}
				else
				{
					if (keyCode == -26)
					{
						GameCanvas.keyHold[16] = true;
						GameCanvas.keyPressed[16] = true;
						return;
					}
					if (keyCode != -22)
					{
						return;
					}
					goto IL_3FB;
				}
			}
			else
			{
				if (keyCode <= -1)
				{
					if (keyCode != -21)
					{
						switch (keyCode)
						{
						case -8:
							GameCanvas.keyHold[14] = true;
							GameCanvas.keyPressed[14] = true;
							return;
						case -7:
							goto IL_3FB;
						case -6:
							break;
						case -5:
							goto IL_271;
						case -4:
						{
							bool flag2 = (GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack;
							if (flag2)
							{
								GameCanvas.clearKeyHold();
								GameCanvas.clearKeyPressed();
							}
							else
							{
								GameCanvas.keyHold[24] = true;
								GameCanvas.keyPressed[24] = true;
							}
							return;
						}
						case -3:
						{
							bool flag3 = (GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack;
							if (flag3)
							{
								GameCanvas.clearKeyHold();
								GameCanvas.clearKeyPressed();
							}
							else
							{
								GameCanvas.keyHold[23] = true;
								GameCanvas.keyPressed[23] = true;
							}
							return;
						}
						case -2:
							goto IL_177;
						case -1:
							goto IL_125;
						default:
							return;
						}
					}
					GameCanvas.keyHold[12] = true;
					GameCanvas.keyPressed[12] = true;
					return;
				}
				if (keyCode != 10)
				{
					switch (keyCode)
					{
					case 35:
						GameCanvas.keyHold[11] = true;
						GameCanvas.keyPressed[11] = true;
						return;
					case 36:
					case 37:
					case 38:
					case 39:
					case 40:
					case 41:
					case 43:
					case 44:
					case 45:
					case 46:
					case 47:
						return;
					case 42:
						GameCanvas.keyHold[10] = true;
						GameCanvas.keyPressed[10] = true;
						return;
					case 48:
						GameCanvas.keyHold[0] = true;
						GameCanvas.keyPressed[0] = true;
						return;
					case 49:
					{
						bool flag4 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
						if (flag4)
						{
							GameCanvas.keyHold[1] = true;
							GameCanvas.keyPressed[1] = true;
						}
						return;
					}
					case 50:
					{
						bool flag5 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
						if (flag5)
						{
							GameCanvas.keyHold[2] = true;
							GameCanvas.keyPressed[2] = true;
						}
						return;
					}
					case 51:
					{
						bool flag6 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
						if (flag6)
						{
							GameCanvas.keyHold[3] = true;
							GameCanvas.keyPressed[3] = true;
						}
						return;
					}
					case 52:
					{
						bool flag7 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
						if (flag7)
						{
							GameCanvas.keyHold[4] = true;
							GameCanvas.keyPressed[4] = true;
						}
						return;
					}
					case 53:
					{
						bool flag8 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
						if (flag8)
						{
							GameCanvas.keyHold[5] = true;
							GameCanvas.keyPressed[5] = true;
						}
						return;
					}
					case 54:
					{
						bool flag9 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
						if (flag9)
						{
							GameCanvas.keyHold[6] = true;
							GameCanvas.keyPressed[6] = true;
						}
						return;
					}
					case 55:
						GameCanvas.keyHold[7] = true;
						GameCanvas.keyPressed[7] = true;
						return;
					case 56:
					{
						bool flag10 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
						if (flag10)
						{
							GameCanvas.keyHold[8] = true;
							GameCanvas.keyPressed[8] = true;
						}
						return;
					}
					case 57:
						GameCanvas.keyHold[9] = true;
						GameCanvas.keyPressed[9] = true;
						return;
					default:
						if (keyCode != 113)
						{
							return;
						}
						GameCanvas.keyHold[17] = true;
						GameCanvas.keyPressed[17] = true;
						return;
					}
				}
				IL_271:
				bool flag11 = (GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack;
				if (flag11)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					return;
				}
				GameCanvas.keyHold[25] = true;
				GameCanvas.keyPressed[25] = true;
				GameCanvas.keyHold[15] = true;
				GameCanvas.keyPressed[15] = true;
				return;
			}
			IL_125:
			bool flag12 = (GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack;
			if (flag12)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
			}
			else
			{
				GameCanvas.keyHold[21] = true;
				GameCanvas.keyPressed[21] = true;
			}
			return;
			IL_177:
			bool flag13 = (GameCanvas.currentScreen is GameScr || GameCanvas.currentScreen is CrackBallScr) && global::Char.myCharz().isAttack;
			if (flag13)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
			}
			else
			{
				GameCanvas.keyHold[22] = true;
				GameCanvas.keyPressed[22] = true;
			}
			return;
			IL_3FB:
			GameCanvas.keyHold[13] = true;
			GameCanvas.keyPressed[13] = true;
		}
	}

	// Token: 0x0600026D RID: 621 RVA: 0x00004238 File Offset: 0x00002438
	public void keyReleasedz(int keyCode)
	{
		GameCanvas.keyAsciiPress = 0;
		this.mapKeyRelease(keyCode);
	}

	// Token: 0x0600026E RID: 622 RVA: 0x0003D0B0 File Offset: 0x0003B2B0
	public void mapKeyRelease(int keyCode)
	{
		if (keyCode > -22)
		{
			if (keyCode <= -1)
			{
				if (keyCode != -21)
				{
					switch (keyCode)
					{
					case -8:
						GameCanvas.keyHold[14] = false;
						return;
					case -7:
						goto IL_276;
					case -6:
						break;
					case -5:
						goto IL_12D;
					case -4:
						GameCanvas.keyHold[24] = false;
						return;
					case -3:
						GameCanvas.keyHold[23] = false;
						return;
					case -2:
						goto IL_103;
					case -1:
						goto IL_F5;
					default:
						return;
					}
				}
				GameCanvas.keyHold[12] = false;
				GameCanvas.keyReleased[12] = true;
				return;
			}
			if (keyCode != 10)
			{
				switch (keyCode)
				{
				case 35:
					GameCanvas.keyHold[11] = false;
					GameCanvas.keyReleased[11] = true;
					return;
				case 36:
				case 37:
				case 38:
				case 39:
				case 40:
				case 41:
				case 43:
				case 44:
				case 45:
				case 46:
				case 47:
					return;
				case 42:
					GameCanvas.keyHold[10] = false;
					GameCanvas.keyReleased[10] = true;
					return;
				case 48:
					GameCanvas.keyHold[0] = false;
					GameCanvas.keyReleased[0] = true;
					return;
				case 49:
				{
					bool flag = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
					if (flag)
					{
						GameCanvas.keyHold[1] = false;
						GameCanvas.keyReleased[1] = true;
					}
					return;
				}
				case 50:
				{
					bool flag2 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
					if (flag2)
					{
						GameCanvas.keyHold[2] = false;
						GameCanvas.keyReleased[2] = true;
					}
					return;
				}
				case 51:
				{
					bool flag3 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
					if (flag3)
					{
						GameCanvas.keyHold[3] = false;
						GameCanvas.keyReleased[3] = true;
					}
					return;
				}
				case 52:
				{
					bool flag4 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
					if (flag4)
					{
						GameCanvas.keyHold[4] = false;
						GameCanvas.keyReleased[4] = true;
					}
					return;
				}
				case 53:
				{
					bool flag5 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
					if (flag5)
					{
						GameCanvas.keyHold[5] = false;
						GameCanvas.keyReleased[5] = true;
					}
					return;
				}
				case 54:
				{
					bool flag6 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
					if (flag6)
					{
						GameCanvas.keyHold[6] = false;
						GameCanvas.keyReleased[6] = true;
					}
					return;
				}
				case 55:
					GameCanvas.keyHold[7] = false;
					GameCanvas.keyReleased[7] = true;
					return;
				case 56:
				{
					bool flag7 = GameCanvas.currentScreen == CrackBallScr.instance || (GameCanvas.currentScreen == GameScr.instance && GameCanvas.isMoveNumberPad && !ChatTextField.gI().isShow);
					if (flag7)
					{
						GameCanvas.keyHold[8] = false;
						GameCanvas.keyReleased[8] = true;
					}
					return;
				}
				case 57:
					GameCanvas.keyHold[9] = false;
					GameCanvas.keyReleased[9] = true;
					return;
				default:
					if (keyCode != 113)
					{
						return;
					}
					GameCanvas.keyHold[17] = false;
					GameCanvas.keyReleased[17] = true;
					return;
				}
			}
			IL_12D:
			GameCanvas.keyHold[25] = false;
			GameCanvas.keyReleased[25] = true;
			GameCanvas.keyHold[15] = true;
			GameCanvas.keyPressed[15] = true;
			return;
		}
		if (keyCode <= -38)
		{
			if (keyCode == -39)
			{
				goto IL_103;
			}
			if (keyCode != -38)
			{
				return;
			}
		}
		else
		{
			if (keyCode == -26)
			{
				GameCanvas.keyHold[16] = false;
				return;
			}
			if (keyCode != -22)
			{
				return;
			}
			goto IL_276;
		}
		IL_F5:
		GameCanvas.keyHold[21] = false;
		return;
		IL_103:
		GameCanvas.keyHold[22] = false;
		return;
		IL_276:
		GameCanvas.keyHold[13] = false;
		GameCanvas.keyReleased[13] = true;
	}

	// Token: 0x0600026F RID: 623 RVA: 0x00004249 File Offset: 0x00002449
	public void pointerMouse(int x, int y)
	{
		GameCanvas.pxMouse = x;
		GameCanvas.pyMouse = y;
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0003D4F8 File Offset: 0x0003B6F8
	public void scrollMouse(int a)
	{
		GameCanvas.pXYScrollMouse = a;
		bool flag = GameCanvas.panel != null && GameCanvas.panel.isShow;
		if (flag)
		{
			GameCanvas.panel.updateScroolMouse(a);
		}
	}

	// Token: 0x06000271 RID: 625 RVA: 0x0003D534 File Offset: 0x0003B734
	public void pointerDragged(int x, int y)
	{
		bool flag = Res.abs(x - GameCanvas.pxLast) >= 10 || Res.abs(y - GameCanvas.pyLast) >= 10;
		if (flag)
		{
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerDown = true;
			GameCanvas.isPointerMove = true;
		}
		GameCanvas.px = x;
		GameCanvas.py = y;
		GameCanvas.curPos++;
		bool flag2 = GameCanvas.curPos > 3;
		if (flag2)
		{
			GameCanvas.curPos = 0;
		}
		GameCanvas.arrPos[GameCanvas.curPos] = new Position(x, y);
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0003D5C0 File Offset: 0x0003B7C0
	public static bool isHoldPress()
	{
		return mSystem.currentTimeMillis() - GameCanvas.lastTimePress >= 800L;
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0003D5F4 File Offset: 0x0003B7F4
	public void pointerPressed(int x, int y)
	{
		GameCanvas.isPointerJustRelease = false;
		GameCanvas.isPointerJustDown = true;
		GameCanvas.isPointerDown = true;
		GameCanvas.isPointerClick = true;
		GameCanvas.isPointerMove = false;
		GameCanvas.lastTimePress = mSystem.currentTimeMillis();
		GameCanvas.pxFirst = x;
		GameCanvas.pyFirst = y;
		GameCanvas.pxLast = x;
		GameCanvas.pyLast = y;
		GameCanvas.px = x;
		GameCanvas.py = y;
	}

	// Token: 0x06000274 RID: 628 RVA: 0x00004258 File Offset: 0x00002458
	public void pointerReleased(int x, int y)
	{
		GameCanvas.isPointerDown = false;
		GameCanvas.isPointerJustRelease = true;
		GameCanvas.isPointerMove = false;
		mScreen.keyTouch = -1;
		GameCanvas.px = x;
		GameCanvas.py = y;
	}

	// Token: 0x06000275 RID: 629 RVA: 0x0003D650 File Offset: 0x0003B850
	public static bool isPointerHoldIn(int x, int y, int w, int h)
	{
		bool flag = !GameCanvas.isPointerDown && !GameCanvas.isPointerJustRelease;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h;
			result = flag2;
		}
		return result;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0003D6B4 File Offset: 0x0003B8B4
	public static bool isMouseFocus(int x, int y, int w, int h)
	{
		return GameCanvas.pxMouse >= x && GameCanvas.pxMouse <= x + w && GameCanvas.pyMouse >= y && GameCanvas.pyMouse <= y + h;
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0003D6FC File Offset: 0x0003B8FC
	public static void clearKeyPressed()
	{
		for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
		{
			GameCanvas.keyPressed[i] = false;
		}
		GameCanvas.isPointerJustRelease = false;
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0003D730 File Offset: 0x0003B930
	public static void clearKeyHold()
	{
		for (int i = 0; i < GameCanvas.keyHold.Length; i++)
		{
			GameCanvas.keyHold[i] = false;
		}
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0003D760 File Offset: 0x0003B960
	public static void checkBackButton()
	{
		bool flag = ChatPopup.serverChatPopUp == null && ChatPopup.currChatPopup == null;
		if (flag)
		{
			GameCanvas.startYesNoDlg(mResources.DOYOUWANTEXIT, new Command(mResources.YES, GameCanvas.instance, 8885, null), new Command(mResources.NO, GameCanvas.instance, 8882, null));
		}
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0003D7BC File Offset: 0x0003B9BC
	public void paintChangeMap(mGraphics g)
	{
		GameCanvas.resetTrans(g);
		g.setColor(0);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(LoginScr.imgTitle, GameCanvas.w / 2, GameCanvas.h / 2 - 24, StaticObj.BOTTOM_HCENTER);
		GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.h / 2 + 24, g);
		mFont.tahoma_7b_white.drawString(g, mResources.PLEASEWAIT + ((LoginScr.timeLogin <= 0) ? string.Empty : (" " + LoginScr.timeLogin + "s")), GameCanvas.w / 2, GameCanvas.h / 2, 2);
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0003D874 File Offset: 0x0003BA74
	public void paint(mGraphics gx)
	{
		try
		{
			GameCanvas.debugPaint.removeAllElements();
			GameCanvas.debug("PA", 1);
			bool flag = GameCanvas.currentScreen != null;
			if (flag)
			{
				GameCanvas.currentScreen.paint(this.g);
			}
			GameCanvas.debug("PB", 1);
			this.g.translate(-this.g.getTranslateX(), -this.g.getTranslateY());
			this.g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			bool isShow = GameCanvas.panel.isShow;
			if (isShow)
			{
				GameCanvas.panel.paint(this.g);
				bool flag2 = GameCanvas.panel2 != null && GameCanvas.panel2.isShow;
				if (flag2)
				{
					GameCanvas.panel2.paint(this.g);
				}
				bool flag3 = GameCanvas.panel.chatTField != null && GameCanvas.panel.chatTField.isShow;
				if (flag3)
				{
					GameCanvas.panel.chatTField.paint(this.g);
				}
				bool flag4 = GameCanvas.panel2 != null && GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow;
				if (flag4)
				{
					GameCanvas.panel2.chatTField.paint(this.g);
				}
			}
			Res.paintOnScreenDebug(this.g);
			InfoDlg.paint(this.g);
			bool flag5 = GameCanvas.currentDialog != null;
			if (flag5)
			{
				GameCanvas.debug("PC", 1);
				GameCanvas.currentDialog.paint(this.g);
			}
			else
			{
				bool showMenu = GameCanvas.menu.showMenu;
				if (showMenu)
				{
					GameCanvas.debug("PD", 1);
					GameCanvas.menu.paintMenu(this.g);
				}
			}
			GameScr.info1.paint(this.g);
			GameScr.info2.paint(this.g);
			bool flag6 = GameScr.gI().popUpYesNo != null;
			if (flag6)
			{
				GameScr.gI().popUpYesNo.paint(this.g);
			}
			bool flag7 = ChatPopup.currChatPopup != null;
			if (flag7)
			{
				ChatPopup.currChatPopup.paint(this.g);
			}
			Hint.paint(this.g);
			bool flag8 = ChatPopup.serverChatPopUp != null;
			if (flag8)
			{
				ChatPopup.serverChatPopUp.paint(this.g);
			}
			for (int i = 0; i < Effect2.vEffect2.size(); i++)
			{
				Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
				bool flag9 = effect is ChatPopup && !effect.Equals(ChatPopup.currChatPopup) && !effect.Equals(ChatPopup.serverChatPopUp);
				if (flag9)
				{
					effect.paint(this.g);
				}
			}
			bool flag10 = global::Char.isLoadingMap || LoginScr.isContinueToLogin || ServerListScreen.waitToLogin || ServerListScreen.isWait;
			if (flag10)
			{
				this.paintChangeMap(this.g);
			}
			GameCanvas.debug("PE", 1);
			GameCanvas.resetTrans(this.g);
			EffecMn.paintLayer4(this.g);
			bool flag11 = mResources.language == 0 && GameCanvas.open3Hour && !GameCanvas.isLoading;
			if (flag11)
			{
				bool flag12 = GameCanvas.currentScreen == GameCanvas.loginScr || GameCanvas.currentScreen == GameCanvas.serverScreen || GameCanvas.currentScreen == GameCanvas.serverScr;
				if (flag12)
				{
					this.g.drawImage(GameCanvas.img12, 5, 5, 0);
				}
				bool flag13 = GameCanvas.currentScreen == CreateCharScr.instance;
				if (flag13)
				{
					this.g.drawImage(GameCanvas.img12, 5, 20, 0);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600027C RID: 636 RVA: 0x0003DC58 File Offset: 0x0003BE58
	public static void endDlg()
	{
		bool flag = GameCanvas.inputDlg != null;
		if (flag)
		{
			GameCanvas.inputDlg.tfInput.setMaxTextLenght(500);
		}
		GameCanvas.currentDialog = null;
		InfoDlg.hide();
	}

	// Token: 0x0600027D RID: 637 RVA: 0x0003DC98 File Offset: 0x0003BE98
	public static void startOKDlg(string info)
	{
		bool flag = (info.ToLower().Contains("giây") || info.ToLower().Contains("đủ") || info.ToLower().Contains("không") || info.ToLower().Contains("không thể đến") || info.ToLower().Contains("cần")) && !VuDang.dangLogin && GameCanvas.currentScreen == GameScr.instance;
		if (flag)
		{
			GameScr.info1.addInfo(info, 0);
			VuDang.isVaoKhu = false;
			VuDang.currentCheckLag = 30;
		}
		else
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, 8882, null), null);
			GameCanvas.currentDialog = GameCanvas.msgdlg;
		}
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0003DD6C File Offset: 0x0003BF6C
	public static void startWaitDlg(string info)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
		GameCanvas.currentDialog = GameCanvas.msgdlg;
		GameCanvas.msgdlg.isWait = true;
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0003DDB8 File Offset: 0x0003BFB8
	public static void startOKDlg(string info, bool isError)
	{
		bool flag = info.Trim().ToLower().Contains("không thể đổi khu vực");
		if (!flag)
		{
			GameCanvas.closeKeyBoard();
			GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.CANCEL, GameCanvas.instance, 8882, null), null);
			GameCanvas.currentDialog = GameCanvas.msgdlg;
			GameCanvas.msgdlg.isWait = true;
		}
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0000427F File Offset: 0x0000247F
	public static void startWaitDlg()
	{
		GameCanvas.closeKeyBoard();
		global::Char.isLoadingMap = true;
	}

	// Token: 0x06000281 RID: 641 RVA: 0x0000428E File Offset: 0x0000248E
	public void openWeb(string strLeft, string strRight, string url, string str)
	{
		GameCanvas.msgdlg.setInfo(str, new Command(strLeft, this, 8881, url), null, new Command(strRight, this, 8882, null));
		GameCanvas.currentDialog = GameCanvas.msgdlg;
	}

	// Token: 0x06000282 RID: 642 RVA: 0x000042C3 File Offset: 0x000024C3
	public static void startOK(string info, int actionID, object p)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, null, new Command(mResources.OK, GameCanvas.instance, actionID, p), null);
		GameCanvas.msgdlg.show();
	}

	// Token: 0x06000283 RID: 643 RVA: 0x0003DE20 File Offset: 0x0003C020
	public static void startYesNoDlg(string info, int iYes, object pYes, int iNo, object pNo)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, new Command(mResources.YES, GameCanvas.instance, iYes, pYes), new Command(string.Empty, GameCanvas.instance, iYes, pYes), new Command(mResources.NO, GameCanvas.instance, iNo, pNo));
		GameCanvas.msgdlg.show();
	}

	// Token: 0x06000284 RID: 644 RVA: 0x000042F6 File Offset: 0x000024F6
	public static void startYesNoDlg(string info, Command cmdYes, Command cmdNo)
	{
		GameCanvas.closeKeyBoard();
		GameCanvas.msgdlg.setInfo(info, cmdYes, null, cmdNo);
		GameCanvas.msgdlg.show();
	}

	// Token: 0x06000285 RID: 645 RVA: 0x0003DE80 File Offset: 0x0003C080
	public static string getMoneys(int m)
	{
		string text = string.Empty;
		int num = m / 1000 + 1;
		for (int i = 0; i < num; i++)
		{
			bool flag = m >= 1000;
			if (!flag)
			{
				text = m + text;
				break;
			}
			int num2 = m % 1000;
			text = ((num2 != 0) ? ((num2 >= 10) ? ((num2 >= 100) ? ("." + num2 + text) : (".0" + num2 + text)) : (".00" + num2 + text)) : (".000" + text));
			m /= 1000;
		}
		return text;
	}

	// Token: 0x06000286 RID: 646 RVA: 0x0003DF4C File Offset: 0x0003C14C
	public static int getX(int start, int w)
	{
		return (GameCanvas.px - start) / w;
	}

	// Token: 0x06000287 RID: 647 RVA: 0x0003DF68 File Offset: 0x0003C168
	public static int getY(int start, int w)
	{
		return (GameCanvas.py - start) / w;
	}

	// Token: 0x06000288 RID: 648 RVA: 0x00003A0D File Offset: 0x00001C0D
	protected void sizeChanged(int w, int h)
	{
	}

	// Token: 0x06000289 RID: 649 RVA: 0x0003DF84 File Offset: 0x0003C184
	public static bool isGetResourceFromServer()
	{
		return true;
	}

	// Token: 0x0600028A RID: 650 RVA: 0x0003DF98 File Offset: 0x0003C198
	public static Image loadImageRMS(string path)
	{
		path = string.Concat(new object[]
		{
			Main.res,
			"/x",
			mGraphics.zoomLevel,
			path
		});
		path = GameCanvas.cutPng(path);
		Image image = null;
		Image result;
		try
		{
			image = Image.createImage(path);
			result = image;
		}
		catch (Exception ex)
		{
			try
			{
				string[] array = Res.split(path, "/", 0);
				string filename = "x" + mGraphics.zoomLevel + array[array.Length - 1];
				sbyte[] array2 = Rms.loadRMS(filename);
				bool flag = array2 != null;
				if (flag)
				{
					image = Image.createImage(array2, 0, array2.Length);
					result = image;
				}
				else
				{
					result = image;
				}
			}
			catch (Exception)
			{
				Cout.LogError("Loi ham khong tim thay a: " + ex.ToString());
				result = image;
			}
		}
		return result;
	}

	// Token: 0x0600028B RID: 651 RVA: 0x0003E080 File Offset: 0x0003C280
	public static Image loadImage(string path)
	{
		path = string.Concat(new object[]
		{
			Main.res,
			"/x",
			mGraphics.zoomLevel,
			path
		});
		path = GameCanvas.cutPng(path);
		Image image = null;
		Image result;
		try
		{
			image = Image.createImage(path);
			result = image;
		}
		catch (Exception)
		{
			result = image;
		}
		return result;
	}

	// Token: 0x0600028C RID: 652 RVA: 0x0003E0EC File Offset: 0x0003C2EC
	public static string cutPng(string str)
	{
		string result = str;
		bool flag = str.Contains(".png");
		if (flag)
		{
			result = str.Replace(".png", string.Empty);
		}
		return result;
	}

	// Token: 0x0600028D RID: 653 RVA: 0x0003E124 File Offset: 0x0003C324
	public static int random(int a, int b)
	{
		return a + GameCanvas.r.nextInt(b - a);
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0003E148 File Offset: 0x0003C348
	public bool startDust(int dir, int x, int y)
	{
		bool flag = GameCanvas.lowGraphic;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			int num = (dir != 1) ? 1 : 0;
			bool flag2 = this.dustState[num] != -1;
			if (flag2)
			{
				result = false;
			}
			else
			{
				this.dustState[num] = 0;
				this.dustX[num] = x;
				this.dustY[num] = y;
				result = true;
			}
		}
		return result;
	}

	// Token: 0x0600028F RID: 655 RVA: 0x0003E1A4 File Offset: 0x0003C3A4
	public void loadWaterSplash()
	{
		bool flag = !GameCanvas.lowGraphic;
		if (flag)
		{
			GameCanvas.imgWS = new Image[3];
			for (int i = 0; i < 3; i++)
			{
				GameCanvas.imgWS[i] = GameCanvas.loadImage("/e/w" + i + ".png");
			}
			GameCanvas.wsX = new int[2];
			GameCanvas.wsY = new int[2];
			GameCanvas.wsState = new int[2];
			GameCanvas.wsF = new int[2];
			GameCanvas.wsState[0] = (GameCanvas.wsState[1] = -1);
		}
	}

	// Token: 0x06000290 RID: 656 RVA: 0x0003E23C File Offset: 0x0003C43C
	public bool startWaterSplash(int x, int y)
	{
		bool flag = GameCanvas.lowGraphic;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			int num = (GameCanvas.wsState[0] != -1) ? 1 : 0;
			bool flag2 = GameCanvas.wsState[num] != -1;
			if (flag2)
			{
				result = false;
			}
			else
			{
				GameCanvas.wsState[num] = 0;
				GameCanvas.wsX[num] = x;
				GameCanvas.wsY[num] = y;
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0003E29C File Offset: 0x0003C49C
	public void updateWaterSplash()
	{
		bool flag = GameCanvas.lowGraphic;
		if (!flag)
		{
			for (int i = 0; i < 2; i++)
			{
				bool flag2 = GameCanvas.wsState[i] == -1;
				if (!flag2)
				{
					GameCanvas.wsY[i]--;
					bool flag3 = GameCanvas.gameTick % 2 == 0;
					if (flag3)
					{
						GameCanvas.wsState[i]++;
						bool flag4 = GameCanvas.wsState[i] > 2;
						if (flag4)
						{
							GameCanvas.wsState[i] = -1;
						}
						else
						{
							GameCanvas.wsF[i] = GameCanvas.wsState[i];
						}
					}
				}
			}
		}
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0003E33C File Offset: 0x0003C53C
	public void updateDust()
	{
		bool flag = GameCanvas.lowGraphic;
		if (!flag)
		{
			for (int i = 0; i < 2; i++)
			{
				bool flag2 = this.dustState[i] != -1;
				if (flag2)
				{
					this.dustState[i]++;
					bool flag3 = this.dustState[i] >= 5;
					if (flag3)
					{
						this.dustState[i] = -1;
					}
					bool flag4 = i == 0;
					if (flag4)
					{
						this.dustX[i]--;
					}
					else
					{
						this.dustX[i]++;
					}
					this.dustY[i]--;
				}
			}
		}
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0003E3F8 File Offset: 0x0003C5F8
	public static bool isPaint(int x, int y)
	{
		bool flag = x < GameScr.cmx;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = x > GameScr.cmx + GameScr.gW;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = y < GameScr.cmy;
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = y > GameScr.cmy + GameScr.gH + 30;
					result = !flag4;
				}
			}
		}
		return result;
	}

	// Token: 0x06000294 RID: 660 RVA: 0x0003E460 File Offset: 0x0003C660
	public void paintDust(mGraphics g)
	{
		bool flag = GameCanvas.lowGraphic;
		if (!flag)
		{
			for (int i = 0; i < 2; i++)
			{
				bool flag2 = this.dustState[i] != -1 && GameCanvas.isPaint(this.dustX[i], this.dustY[i]);
				if (flag2)
				{
					g.drawImage(GameCanvas.imgDust[i][this.dustState[i]], this.dustX[i], this.dustY[i], 3);
				}
			}
		}
	}

	// Token: 0x06000295 RID: 661 RVA: 0x0003E4E0 File Offset: 0x0003C6E0
	public void loadDust()
	{
		bool flag = GameCanvas.lowGraphic;
		if (!flag)
		{
			bool flag2 = GameCanvas.imgDust == null;
			if (flag2)
			{
				GameCanvas.imgDust = new Image[2][];
				for (int i = 0; i < GameCanvas.imgDust.Length; i++)
				{
					GameCanvas.imgDust[i] = new Image[5];
				}
				for (int j = 0; j < 2; j++)
				{
					for (int k = 0; k < 5; k++)
					{
						GameCanvas.imgDust[j][k] = GameCanvas.loadImage(string.Concat(new object[]
						{
							"/e/d",
							j,
							k,
							".png"
						}));
					}
				}
			}
			this.dustX = new int[2];
			this.dustY = new int[2];
			this.dustState = new int[2];
			this.dustState[0] = (this.dustState[1] = -1);
		}
	}

	// Token: 0x06000296 RID: 662 RVA: 0x0003E5E8 File Offset: 0x0003C7E8
	public static void paintShukiren(int x, int y, mGraphics g)
	{
		g.drawRegion(GameCanvas.imgShuriken, 0, Main.f * 16, 16, 16, 0, x, y, mGraphics.HCENTER | mGraphics.VCENTER);
	}

	// Token: 0x06000297 RID: 663 RVA: 0x00004319 File Offset: 0x00002519
	public void resetToLoginScrz()
	{
		this.resetToLoginScr = true;
	}

	// Token: 0x06000298 RID: 664 RVA: 0x0003D650 File Offset: 0x0003B850
	public static bool isPointer(int x, int y, int w, int h)
	{
		bool flag = !GameCanvas.isPointerDown && !GameCanvas.isPointerJustRelease;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = GameCanvas.px >= x && GameCanvas.px <= x + w && GameCanvas.py >= y && GameCanvas.py <= y + h;
			result = flag2;
		}
		return result;
	}

	// Token: 0x06000299 RID: 665 RVA: 0x0003E620 File Offset: 0x0003C820
	public void perform(int idAction, object p)
	{
		if (idAction <= 9999)
		{
			if (idAction <= 8889)
			{
				if (idAction != 999)
				{
					switch (idAction)
					{
					case 8881:
					{
						string url = (string)p;
						try
						{
							GameMidlet.instance.platformRequest(url);
						}
						catch (Exception)
						{
						}
						GameCanvas.currentDialog = null;
						break;
					}
					case 8882:
						InfoDlg.hide();
						GameCanvas.currentDialog = null;
						break;
					case 8884:
						GameCanvas.endDlg();
						GameCanvas.loginScr.switchToMe();
						break;
					case 8885:
						GameMidlet.instance.exit();
						break;
					case 8886:
					{
						GameCanvas.endDlg();
						string name = (string)p;
						Service.gI().addFriend(name);
						break;
					}
					case 8887:
					{
						GameCanvas.endDlg();
						int charId = (int)p;
						Service.gI().addPartyAccept(charId);
						break;
					}
					case 8888:
					{
						int charId2 = (int)p;
						Service.gI().addPartyCancel(charId2);
						GameCanvas.endDlg();
						break;
					}
					case 8889:
					{
						string str = (string)p;
						GameCanvas.endDlg();
						Service.gI().acceptPleaseParty(str);
						break;
					}
					}
				}
				else
				{
					mSystem.closeBanner();
					GameCanvas.endDlg();
				}
			}
			else if (idAction != 9000)
			{
				if (idAction == 9999)
				{
					GameCanvas.endDlg();
					GameCanvas.connect();
					Service.gI().setClientType();
					bool flag = GameCanvas.loginScr == null;
					if (flag)
					{
						GameCanvas.loginScr = new LoginScr();
					}
					GameCanvas.loginScr.doLogin();
				}
			}
			else
			{
				GameCanvas.endDlg();
				SplashScr.imgLogo = null;
				SmallImage.loadBigRMS();
				mSystem.gcc();
				ServerListScreen.bigOk = true;
				ServerListScreen.loadScreen = true;
				GameScr.gI().loadGameScr();
				bool flag2 = GameCanvas.currentScreen != GameCanvas.loginScr;
				if (flag2)
				{
					GameCanvas.serverScreen.switchToMe2();
				}
			}
		}
		else if (idAction <= 101023)
		{
			switch (idAction)
			{
			case 88810:
			{
				int playerMapId = (int)p;
				GameCanvas.endDlg();
				Service.gI().acceptInviteTrade(playerMapId);
				break;
			}
			case 88811:
				GameCanvas.endDlg();
				Service.gI().cancelInviteTrade();
				break;
			case 88812:
			case 88813:
			case 88815:
			case 88816:
			case 88830:
			case 88831:
			case 88832:
			case 88833:
			case 88834:
			case 88835:
			case 88838:
				break;
			case 88814:
			{
				Item[] items = (Item[])p;
				GameCanvas.endDlg();
				Service.gI().crystalCollectLock(items);
				break;
			}
			case 88817:
				ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
				Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
				break;
			case 88818:
			{
				short menuId = (short)p;
				Service.gI().textBoxId(menuId, GameCanvas.inputDlg.tfInput.getText());
				GameCanvas.endDlg();
				break;
			}
			case 88819:
			{
				short menuId2 = (short)p;
				Service.gI().menuId(menuId2);
				break;
			}
			case 88820:
			{
				string[] array = (string[])p;
				bool flag3 = global::Char.myCharz().npcFocus == null;
				if (!flag3)
				{
					int menuSelectedItem = GameCanvas.menu.menuSelectedItem;
					bool flag4 = array.Length > 1;
					if (flag4)
					{
						MyVector myVector = new MyVector();
						for (int i = 0; i < array.Length - 1; i++)
						{
							myVector.addElement(new Command(array[i + 1], GameCanvas.instance, 88821, menuSelectedItem));
						}
						GameCanvas.menu.startAt(myVector, 3);
					}
					else
					{
						ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
						Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, menuSelectedItem, 0);
					}
				}
				break;
			}
			case 88821:
			{
				int menuId3 = (int)p;
				ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
				Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, menuId3, GameCanvas.menu.menuSelectedItem);
				break;
			}
			case 88822:
				ChatPopup.addChatPopup(string.Empty, 1, global::Char.myCharz().npcFocus);
				Service.gI().menu(global::Char.myCharz().npcFocus.template.npcTemplateId, GameCanvas.menu.menuSelectedItem, 0);
				break;
			case 88823:
				GameCanvas.startOKDlg(mResources.SENTMSG);
				break;
			case 88824:
				GameCanvas.startOKDlg(mResources.NOSENDMSG);
				break;
			case 88825:
				GameCanvas.startOKDlg(mResources.sendMsgSuccess, false);
				break;
			case 88826:
				GameCanvas.startOKDlg(mResources.cannotSendMsg, false);
				break;
			case 88827:
				GameCanvas.startOKDlg(mResources.sendGuessMsgSuccess);
				break;
			case 88828:
				GameCanvas.startOKDlg(mResources.sendMsgFail);
				break;
			case 88829:
			{
				string text = GameCanvas.inputDlg.tfInput.getText();
				bool flag5 = !text.Equals(string.Empty);
				if (flag5)
				{
					Service.gI().changeName(text, (int)p);
					InfoDlg.showWait();
				}
				break;
			}
			case 88836:
				GameCanvas.inputDlg.tfInput.setMaxTextLenght(6);
				GameCanvas.inputDlg.show(mResources.INPUT_PRIVATE_PASS, new Command(mResources.ACCEPT, GameCanvas.instance, 888361, null), TField.INPUT_TYPE_NUMERIC);
				break;
			case 88837:
			{
				string text2 = GameCanvas.inputDlg.tfInput.getText();
				GameCanvas.endDlg();
				try
				{
					Service.gI().openLockAccProtect(int.Parse(text2.Trim()));
				}
				catch (Exception ex)
				{
					Cout.println("Loi tai 88837 " + ex.ToString());
				}
				break;
			}
			case 88839:
			{
				string text3 = GameCanvas.inputDlg.tfInput.getText();
				GameCanvas.endDlg();
				bool flag6 = text3.Length < 6 || text3.Equals(string.Empty);
				if (flag6)
				{
					GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_1);
				}
				else
				{
					try
					{
						GameCanvas.startYesNoDlg(mResources.cancelAccountProtection, 888391, text3, 8882, null);
					}
					catch (Exception)
					{
						GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
					}
				}
				break;
			}
			default:
				if (idAction == 101023)
				{
					Main.numberQuit = 0;
				}
				break;
			}
		}
		else if (idAction != 888361)
		{
			switch (idAction)
			{
			case 888391:
			{
				string s = (string)p;
				GameCanvas.endDlg();
				Service.gI().clearAccProtect(int.Parse(s));
				break;
			}
			case 888392:
				Service.gI().menu(4, GameCanvas.menu.menuSelectedItem, 0);
				break;
			case 888393:
			{
				bool flag7 = GameCanvas.loginScr == null;
				if (flag7)
				{
					GameCanvas.loginScr = new LoginScr();
				}
				GameCanvas.loginScr.doLogin();
				Main.closeKeyBoard();
				break;
			}
			case 888394:
				GameCanvas.endDlg();
				break;
			case 888395:
				GameCanvas.endDlg();
				break;
			case 888396:
				GameCanvas.endDlg();
				break;
			case 888397:
			{
				string text4 = (string)p;
				break;
			}
			}
		}
		else
		{
			string text5 = GameCanvas.inputDlg.tfInput.getText();
			GameCanvas.endDlg();
			bool flag8 = text5.Length < 6 || text5.Equals(string.Empty);
			if (flag8)
			{
				GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_1);
			}
			else
			{
				try
				{
					Service.gI().activeAccProtect(int.Parse(text5));
				}
				catch (Exception ex2)
				{
					GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
					Cout.println("Loi tai 888361 Gamescavas " + ex2.ToString());
				}
			}
		}
	}

	// Token: 0x0600029A RID: 666 RVA: 0x00004323 File Offset: 0x00002523
	public static void clearAllPointerEvent()
	{
		GameCanvas.isPointerClick = false;
		GameCanvas.isPointerDown = false;
		GameCanvas.isPointerJustDown = false;
		GameCanvas.isPointerJustRelease = false;
		GameScr.gI().lastSingleClick = 0L;
		GameScr.gI().isPointerDowning = false;
	}

	// Token: 0x0600029B RID: 667 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void backToRegister()
	{
	}

	// Token: 0x04000563 RID: 1379
	public static long timeNow = 0L;

	// Token: 0x04000564 RID: 1380
	public static bool open3Hour;

	// Token: 0x04000565 RID: 1381
	public static bool lowGraphic = false;

	// Token: 0x04000566 RID: 1382
	public static bool isMoveNumberPad = true;

	// Token: 0x04000567 RID: 1383
	public static bool isLoading;

	// Token: 0x04000568 RID: 1384
	public static bool isTouch = false;

	// Token: 0x04000569 RID: 1385
	public static bool isTouchControl;

	// Token: 0x0400056A RID: 1386
	public static bool isTouchControlSmallScreen;

	// Token: 0x0400056B RID: 1387
	public static bool isTouchControlLargeScreen;

	// Token: 0x0400056C RID: 1388
	public static bool isConnectFail;

	// Token: 0x0400056D RID: 1389
	public static GameCanvas instance;

	// Token: 0x0400056E RID: 1390
	public static bool bRun;

	// Token: 0x0400056F RID: 1391
	public static bool[] keyPressed = new bool[30];

	// Token: 0x04000570 RID: 1392
	public static bool[] keyReleased = new bool[30];

	// Token: 0x04000571 RID: 1393
	public static bool[] keyHold = new bool[30];

	// Token: 0x04000572 RID: 1394
	public static bool isPointerDown;

	// Token: 0x04000573 RID: 1395
	public static bool isPointerClick;

	// Token: 0x04000574 RID: 1396
	public static bool isPointerJustRelease;

	// Token: 0x04000575 RID: 1397
	public static bool isPointerMove;

	// Token: 0x04000576 RID: 1398
	public static int px;

	// Token: 0x04000577 RID: 1399
	public static int py;

	// Token: 0x04000578 RID: 1400
	public static int pxFirst;

	// Token: 0x04000579 RID: 1401
	public static int pyFirst;

	// Token: 0x0400057A RID: 1402
	public static int pxLast;

	// Token: 0x0400057B RID: 1403
	public static int pyLast;

	// Token: 0x0400057C RID: 1404
	public static int pxMouse;

	// Token: 0x0400057D RID: 1405
	public static int pyMouse;

	// Token: 0x0400057E RID: 1406
	public static Position[] arrPos = new Position[4];

	// Token: 0x0400057F RID: 1407
	public static int gameTick;

	// Token: 0x04000580 RID: 1408
	public static int taskTick;

	// Token: 0x04000581 RID: 1409
	public static bool isEff1;

	// Token: 0x04000582 RID: 1410
	public static bool isEff2;

	// Token: 0x04000583 RID: 1411
	public static long timeTickEff1;

	// Token: 0x04000584 RID: 1412
	public static long timeTickEff2;

	// Token: 0x04000585 RID: 1413
	public static int w;

	// Token: 0x04000586 RID: 1414
	public static int h;

	// Token: 0x04000587 RID: 1415
	public static int hw;

	// Token: 0x04000588 RID: 1416
	public static int hh;

	// Token: 0x04000589 RID: 1417
	public static int wd3;

	// Token: 0x0400058A RID: 1418
	public static int hd3;

	// Token: 0x0400058B RID: 1419
	public static int w2d3;

	// Token: 0x0400058C RID: 1420
	public static int h2d3;

	// Token: 0x0400058D RID: 1421
	public static int w3d4;

	// Token: 0x0400058E RID: 1422
	public static int h3d4;

	// Token: 0x0400058F RID: 1423
	public static int wd6;

	// Token: 0x04000590 RID: 1424
	public static int hd6;

	// Token: 0x04000591 RID: 1425
	public static mScreen currentScreen;

	// Token: 0x04000592 RID: 1426
	public static Menu menu = new Menu();

	// Token: 0x04000593 RID: 1427
	public static Panel panel;

	// Token: 0x04000594 RID: 1428
	public static Panel panel2;

	// Token: 0x04000595 RID: 1429
	public static LoginScr loginScr;

	// Token: 0x04000596 RID: 1430
	public static RegisterScreen registerScr;

	// Token: 0x04000597 RID: 1431
	public static Dialog currentDialog;

	// Token: 0x04000598 RID: 1432
	public static MsgDlg msgdlg;

	// Token: 0x04000599 RID: 1433
	public static InputDlg inputDlg;

	// Token: 0x0400059A RID: 1434
	public static MyVector currentPopup = new MyVector();

	// Token: 0x0400059B RID: 1435
	public static int requestLoseCount;

	// Token: 0x0400059C RID: 1436
	public static MyVector listPoint;

	// Token: 0x0400059D RID: 1437
	public static Paint paintz;

	// Token: 0x0400059E RID: 1438
	public static bool isGetResFromServer;

	// Token: 0x0400059F RID: 1439
	public static Image[] imgBG;

	// Token: 0x040005A0 RID: 1440
	public static int skyColor;

	// Token: 0x040005A1 RID: 1441
	public static int curPos = 0;

	// Token: 0x040005A2 RID: 1442
	public static int[] bgW;

	// Token: 0x040005A3 RID: 1443
	public static int[] bgH;

	// Token: 0x040005A4 RID: 1444
	public static int planet = 0;

	// Token: 0x040005A5 RID: 1445
	private mGraphics g = new mGraphics();

	// Token: 0x040005A6 RID: 1446
	public static Image img12;

	// Token: 0x040005A7 RID: 1447
	public static Image[] imgBlue = new Image[7];

	// Token: 0x040005A8 RID: 1448
	public static Image[] imgViolet = new Image[7];

	// Token: 0x040005A9 RID: 1449
	public static bool isPlaySound = true;

	// Token: 0x040005AA RID: 1450
	private static int clearOldData;

	// Token: 0x040005AB RID: 1451
	public static int timeOpenKeyBoard;

	// Token: 0x040005AC RID: 1452
	public static bool isFocusPanel2;

	// Token: 0x040005AD RID: 1453
	public bool isPaintCarret;

	// Token: 0x040005AE RID: 1454
	public static MyVector debugUpdate;

	// Token: 0x040005AF RID: 1455
	public static MyVector debugPaint;

	// Token: 0x040005B0 RID: 1456
	public static MyVector debugSession;

	// Token: 0x040005B1 RID: 1457
	private static bool isShowErrorForm = false;

	// Token: 0x040005B2 RID: 1458
	public static bool paintBG;

	// Token: 0x040005B3 RID: 1459
	public static int gsskyHeight;

	// Token: 0x040005B4 RID: 1460
	public static int gsgreenField1Y;

	// Token: 0x040005B5 RID: 1461
	public static int gsgreenField2Y;

	// Token: 0x040005B6 RID: 1462
	public static int gshouseY;

	// Token: 0x040005B7 RID: 1463
	public static int gsmountainY;

	// Token: 0x040005B8 RID: 1464
	public static int bgLayer0y;

	// Token: 0x040005B9 RID: 1465
	public static int bgLayer1y;

	// Token: 0x040005BA RID: 1466
	public static Image imgCloud;

	// Token: 0x040005BB RID: 1467
	public static Image imgSun;

	// Token: 0x040005BC RID: 1468
	public static Image imgSun2;

	// Token: 0x040005BD RID: 1469
	public static Image imgClear;

	// Token: 0x040005BE RID: 1470
	public static Image[] imgBorder = new Image[3];

	// Token: 0x040005BF RID: 1471
	public static Image[] imgSunSpec = new Image[3];

	// Token: 0x040005C0 RID: 1472
	public static int borderConnerW;

	// Token: 0x040005C1 RID: 1473
	public static int borderConnerH;

	// Token: 0x040005C2 RID: 1474
	public static int borderCenterW;

	// Token: 0x040005C3 RID: 1475
	public static int borderCenterH;

	// Token: 0x040005C4 RID: 1476
	public static int[] cloudX;

	// Token: 0x040005C5 RID: 1477
	public static int[] cloudY;

	// Token: 0x040005C6 RID: 1478
	public static int sunX;

	// Token: 0x040005C7 RID: 1479
	public static int sunY;

	// Token: 0x040005C8 RID: 1480
	public static int sunX2;

	// Token: 0x040005C9 RID: 1481
	public static int sunY2;

	// Token: 0x040005CA RID: 1482
	public static int[] layerSpeed;

	// Token: 0x040005CB RID: 1483
	public static int[] moveX;

	// Token: 0x040005CC RID: 1484
	public static int[] moveXSpeed;

	// Token: 0x040005CD RID: 1485
	public static bool isBoltEff;

	// Token: 0x040005CE RID: 1486
	public static bool boltActive;

	// Token: 0x040005CF RID: 1487
	public static int tBolt;

	// Token: 0x040005D0 RID: 1488
	public static int typeBg = -1;

	// Token: 0x040005D1 RID: 1489
	public static int transY;

	// Token: 0x040005D2 RID: 1490
	public static int[] yb = new int[5];

	// Token: 0x040005D3 RID: 1491
	public static int[] colorTop;

	// Token: 0x040005D4 RID: 1492
	public static int[] colorBotton;

	// Token: 0x040005D5 RID: 1493
	public static int yb1;

	// Token: 0x040005D6 RID: 1494
	public static int yb2;

	// Token: 0x040005D7 RID: 1495
	public static int yb3;

	// Token: 0x040005D8 RID: 1496
	public static int nBg = 0;

	// Token: 0x040005D9 RID: 1497
	public static int lastBg = -1;

	// Token: 0x040005DA RID: 1498
	public static int[] bgRain = new int[]
	{
		1,
		4,
		11
	};

	// Token: 0x040005DB RID: 1499
	public static int[] bgRainFont = new int[]
	{
		-1
	};

	// Token: 0x040005DC RID: 1500
	public static Image imgCaycot;

	// Token: 0x040005DD RID: 1501
	public static Image tam;

	// Token: 0x040005DE RID: 1502
	public static int typeBackGround = -1;

	// Token: 0x040005DF RID: 1503
	public static int saveIDBg = -10;

	// Token: 0x040005E0 RID: 1504
	public static bool isLoadBGok;

	// Token: 0x040005E1 RID: 1505
	private static long lastTimePress = 0L;

	// Token: 0x040005E2 RID: 1506
	public static int keyAsciiPress;

	// Token: 0x040005E3 RID: 1507
	public static int pXYScrollMouse;

	// Token: 0x040005E4 RID: 1508
	private static Image imgSignal;

	// Token: 0x040005E5 RID: 1509
	public static MyVector flyTexts = new MyVector();

	// Token: 0x040005E6 RID: 1510
	public int longTime;

	// Token: 0x040005E7 RID: 1511
	public static bool isPointerJustDown = false;

	// Token: 0x040005E8 RID: 1512
	private int count = 1;

	// Token: 0x040005E9 RID: 1513
	public static bool csWait;

	// Token: 0x040005EA RID: 1514
	public static MyRandom r = new MyRandom();

	// Token: 0x040005EB RID: 1515
	public static bool isBlackScreen;

	// Token: 0x040005EC RID: 1516
	public static int[] bgSpeed;

	// Token: 0x040005ED RID: 1517
	public static int cmdBarX;

	// Token: 0x040005EE RID: 1518
	public static int cmdBarY;

	// Token: 0x040005EF RID: 1519
	public static int cmdBarW;

	// Token: 0x040005F0 RID: 1520
	public static int cmdBarH;

	// Token: 0x040005F1 RID: 1521
	public static int cmdBarLeftW;

	// Token: 0x040005F2 RID: 1522
	public static int cmdBarRightW;

	// Token: 0x040005F3 RID: 1523
	public static int cmdBarCenterW;

	// Token: 0x040005F4 RID: 1524
	public static int hpBarX;

	// Token: 0x040005F5 RID: 1525
	public static int hpBarY;

	// Token: 0x040005F6 RID: 1526
	public static int hpBarW;

	// Token: 0x040005F7 RID: 1527
	public static int expBarW;

	// Token: 0x040005F8 RID: 1528
	public static int lvPosX;

	// Token: 0x040005F9 RID: 1529
	public static int moneyPosX;

	// Token: 0x040005FA RID: 1530
	public static int hpBarH;

	// Token: 0x040005FB RID: 1531
	public static int girlHPBarY;

	// Token: 0x040005FC RID: 1532
	public int timeOut;

	// Token: 0x040005FD RID: 1533
	public int[] dustX;

	// Token: 0x040005FE RID: 1534
	public int[] dustY;

	// Token: 0x040005FF RID: 1535
	public int[] dustState;

	// Token: 0x04000600 RID: 1536
	public static int[] wsX;

	// Token: 0x04000601 RID: 1537
	public static int[] wsY;

	// Token: 0x04000602 RID: 1538
	public static int[] wsState;

	// Token: 0x04000603 RID: 1539
	public static int[] wsF;

	// Token: 0x04000604 RID: 1540
	public static Image[] imgWS;

	// Token: 0x04000605 RID: 1541
	public static Image imgShuriken;

	// Token: 0x04000606 RID: 1542
	public static Image[][] imgDust;

	// Token: 0x04000607 RID: 1543
	public static bool isResume;

	// Token: 0x04000608 RID: 1544
	public static ServerListScreen serverScreen;

	// Token: 0x04000609 RID: 1545
	public static ServerScr serverScr;

	// Token: 0x0400060A RID: 1546
	public bool resetToLoginScr;
}
