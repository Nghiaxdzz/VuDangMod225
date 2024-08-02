using System;

// Token: 0x02000094 RID: 148
public class ServerListScreen : mScreen, IActionListener
{
	// Token: 0x060007D9 RID: 2009 RVA: 0x000890F0 File Offset: 0x000872F0
	public ServerListScreen()
	{
		int num = 4;
		if (num * 32 + 23 + 33 >= GameCanvas.w)
		{
			num--;
		}
		this.initCommand();
		if (!GameCanvas.isTouch)
		{
			ServerListScreen.selected = 0;
			this.processInput();
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
		if (this.cmdCallHotline == null)
		{
			this.cmdCallHotline = new Command("Gọi hotline", this, 13, null);
			this.cmdCallHotline.x = GameCanvas.w - 75;
			if (mSystem.clientType == 1 && !GameCanvas.isTouch)
			{
				this.cmdCallHotline.y = GameCanvas.h - 20;
			}
			else
			{
				int num2 = 2;
				this.cmdCallHotline.y = num2 + 6;
			}
		}
		ServerListScreen.cmdUpdateServer = new Command();
		ServerListScreen.cmdUpdateServer.actionChat = delegate(string str)
		{
			string text = str;
			string text2 = str;
			if (text == null)
			{
				text = ServerListScreen.linkDefault;
				return;
			}
			if (text == null && text2 != null)
			{
				if (text2.Equals(string.Empty) || text2.Length < 20)
				{
					text2 = ServerListScreen.linkDefault;
				}
				ServerListScreen.getServerList(text2);
			}
			if (text != null && text2 == null)
			{
				if (text.Equals(string.Empty) || text.Length < 20)
				{
					text = ServerListScreen.linkDefault;
				}
				ServerListScreen.getServerList(text);
			}
			if (text != null && text2 != null)
			{
				if (text.Length > text2.Length)
				{
					ServerListScreen.getServerList(text);
					return;
				}
				ServerListScreen.getServerList(text2);
			}
		};
		this.setLinkDefault(mSystem.LANGUAGE);
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x000891F4 File Offset: 0x000873F4
	public static void createDeleteRMS()
	{
		if (ServerListScreen.cmdDeleteRMS == null)
		{
			if (GameCanvas.serverScreen == null)
			{
				GameCanvas.serverScreen = new ServerListScreen();
			}
			ServerListScreen.cmdDeleteRMS = new Command(string.Empty, GameCanvas.serverScreen, 14, null);
			ServerListScreen.cmdDeleteRMS.x = GameCanvas.w - 78;
			ServerListScreen.cmdDeleteRMS.y = GameCanvas.h - 26;
		}
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x00089254 File Offset: 0x00087454
	private void initCommand()
	{
		this.nCmdPlay = 0;
		string text = Rms.loadRMSString("acc");
		if (text == null)
		{
			if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect) != null)
			{
				this.nCmdPlay = 1;
			}
		}
		else if (text.Equals(string.Empty))
		{
			if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect) != null)
			{
				this.nCmdPlay = 1;
			}
		}
		else
		{
			this.nCmdPlay = 1;
		}
		this.cmd = new Command[4 + this.nCmdPlay];
		int num = GameCanvas.hh - 15 * this.cmd.Length + 28;
		for (int i = 0; i < this.cmd.Length; i++)
		{
			switch (i)
			{
			case 0:
				this.cmd[0] = new Command(string.Empty, this, 3, null);
				if (text == null)
				{
					this.cmd[0].caption = mResources.playNew;
					if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect) != null)
					{
						this.cmd[0].caption = mResources.choitiep;
					}
				}
				else if (text.Equals(string.Empty))
				{
					this.cmd[0].caption = mResources.playNew;
					if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect) != null)
					{
						this.cmd[0].caption = mResources.choitiep;
					}
				}
				else
				{
					this.cmd[0].caption = mResources.playAcc + ": " + text;
					if (this.cmd[0].caption.Length > 23)
					{
						this.cmd[0].caption = this.cmd[0].caption.Substring(0, 23);
						Command command = this.cmd[0];
						command.caption += "...";
					}
				}
				break;
			case 1:
				if (this.nCmdPlay == 1)
				{
					this.cmd[1] = new Command(string.Empty, this, 10100, null);
					this.cmd[1].caption = mResources.playNew;
				}
				else
				{
					this.cmd[1] = new Command(mResources.change_account, this, 7, null);
				}
				break;
			case 2:
				if (this.nCmdPlay == 1)
				{
					this.cmd[2] = new Command(mResources.change_account, this, 7, null);
				}
				else
				{
					this.cmd[2] = new Command(string.Empty, this, 17, null);
				}
				break;
			case 3:
				if (this.nCmdPlay == 1)
				{
					this.cmd[3] = new Command(string.Empty, this, 17, null);
				}
				else
				{
					this.cmd[3] = new Command(mResources.option, this, 8, null);
				}
				break;
			case 4:
				this.cmd[4] = new Command(mResources.option, this, 8, null);
				break;
			}
			this.cmd[i].y = num;
			this.cmd[i].setType();
			this.cmd[i].x = (GameCanvas.w - this.cmd[i].w) / 2;
			num += 30;
		}
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x00005C3C File Offset: 0x00003E3C
	public static void doUpdateServer()
	{
		if (ServerListScreen.cmdUpdateServer == null && GameCanvas.serverScreen == null)
		{
			GameCanvas.serverScreen = new ServerListScreen();
		}
		Net.connectHTTP2(ServerListScreen.linkDefault, ServerListScreen.cmdUpdateServer);
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x0008957C File Offset: 0x0008777C
	public static void getServerList(string str)
	{
		ServerListScreen.lengthServer = new int[3];
		string[] array = Res.split(str.Trim(), ",", 0);
		Res.outz("tem leng= " + array.Length);
		mResources.loadLanguague(sbyte.Parse(array[array.Length - 2]));
		ServerListScreen.nameServer = new string[array.Length - 2];
		ServerListScreen.address = new string[array.Length - 2];
		ServerListScreen.port = new short[array.Length - 2];
		ServerListScreen.language = new sbyte[array.Length - 2];
		ServerListScreen.hasConnected = new bool[2];
		for (int i = 0; i < array.Length - 2; i++)
		{
			string[] array2 = Res.split(array[i].Trim(), ":", 0);
			ServerListScreen.nameServer[i] = array2[0];
			ServerListScreen.address[i] = array2[1];
			ServerListScreen.port[i] = short.Parse(array2[2]);
			ServerListScreen.language[i] = sbyte.Parse(array2[3].Trim());
			ServerListScreen.lengthServer[(int)ServerListScreen.language[i]]++;
		}
		ServerListScreen.serverPriority = sbyte.Parse(array[array.Length - 1]);
		ServerListScreen.saveIP();
		GameCanvas.endDlg();
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x000896A4 File Offset: 0x000878A4
	public override void paint(mGraphics g)
	{
		if (!ServerListScreen.loadScreen)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			if (ServerListScreen.bigOk)
			{
			}
		}
		else
		{
			GameCanvas.paintBGGameScr(g);
		}
		int num = 2;
		mFont.tahoma_7_white.drawString(g, string.Concat(new object[]
		{
			"v",
			GameMidlet.VERSION,
			"(",
			mGraphics.zoomLevel,
			")"
		}), GameCanvas.w - 2, num + 15, 1, mFont.tahoma_7_grey);
		if (!ServerListScreen.isGetData || ServerListScreen.loadScreen)
		{
			if (mSystem.clientType == 1 && !GameCanvas.isTouch)
			{
				mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
			}
			else
			{
				mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, num, 1, mFont.tahoma_7_grey);
			}
		}
		else
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, num, 1, mFont.tahoma_7_grey);
		}
		int w = GameCanvas.w;
		if (ServerListScreen.cmdDeleteRMS != null)
		{
			mFont.tahoma_7_white.drawString(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
		}
		if (GameCanvas.currentDialog == null)
		{
			if (!ServerListScreen.loadScreen)
			{
				if (!ServerListScreen.bigOk)
				{
					g.drawImage(LoginScr.imgTitle, GameCanvas.hw, GameCanvas.hh - 32, 3);
					if (!ServerListScreen.isGetData)
					{
						mFont.tahoma_7b_white.drawString(g, mResources.taidulieudechoi, GameCanvas.hw, GameCanvas.hh + 24, 2);
						if (ServerListScreen.cmdDownload != null)
						{
							ServerListScreen.cmdDownload.paint(g);
						}
					}
					else
					{
						if (ServerListScreen.cmdDownload != null)
						{
							ServerListScreen.cmdDownload.paint(g);
						}
						mFont.tahoma_7b_white.drawString(g, mResources.downloading_data + ServerListScreen.percent + "%", GameCanvas.w / 2, GameCanvas.hh + 24, 2);
						GameScr.paintOngMauPercent(GameScr.frBarPow20, GameScr.frBarPow21, GameScr.frBarPow22, (float)(GameCanvas.w / 2 - 50), (float)(GameCanvas.hh + 45), 100, 100f, g);
						GameScr.paintOngMauPercent(GameScr.frBarPow0, GameScr.frBarPow1, GameScr.frBarPow2, (float)(GameCanvas.w / 2 - 50), (float)(GameCanvas.hh + 45), 100, (float)ServerListScreen.percent, g);
					}
				}
			}
			else
			{
				int num2 = GameCanvas.hh - 15 * this.cmd.Length - 15;
				if (num2 < 25)
				{
					num2 = 25;
				}
				if (LoginScr.imgTitle != null)
				{
					g.drawImage(LoginScr.imgTitle, GameCanvas.hw, num2, 3);
				}
				for (int i = 0; i < this.cmd.Length; i++)
				{
					this.cmd[i].paint(g);
				}
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				if (ServerListScreen.testConnect == -1)
				{
					if (GameCanvas.gameTick % 20 > 10)
					{
						g.drawRegion(GameScr.imgRoomStat, 0, 14, 7, 7, 0, (GameCanvas.w - mFont.tahoma_7b_dark.getWidth(this.cmd[2 + this.nCmdPlay].caption) >> 1) - 10, this.cmd[2 + this.nCmdPlay].y + 10, 0);
					}
				}
				else
				{
					g.drawRegion(GameScr.imgRoomStat, 0, ServerListScreen.testConnect * 7, 7, 7, 0, (GameCanvas.w - mFont.tahoma_7b_dark.getWidth(this.cmd[2 + this.nCmdPlay].caption) >> 1) - 10, this.cmd[2 + this.nCmdPlay].y + 9, 0);
				}
			}
		}
		base.paint(g);
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x00089A40 File Offset: 0x00087C40
	public void selectServer()
	{
		ServerListScreen.flagServer = 30;
		GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
		if (Session_ME.gI().isConnected())
		{
			Session_ME.gI().close();
		}
		GameMidlet.IP = ServerListScreen.address[ServerListScreen.ipSelect];
		GameMidlet.PORT = (int)ServerListScreen.port[ServerListScreen.ipSelect];
		if (ServerListScreen.language[ServerListScreen.ipSelect] != mResources.language)
		{
			mResources.loadLanguague(ServerListScreen.language[ServerListScreen.ipSelect]);
		}
		LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		this.initCommand();
		GameCanvas.connect();
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x00089AD4 File Offset: 0x00087CD4
	public override void update()
	{
		if (ServerListScreen.waitToLogin)
		{
			ServerListScreen.tWaitToLogin++;
			if (ServerListScreen.tWaitToLogin == 50)
			{
				GameCanvas.serverScreen.selectServer();
			}
			if (ServerListScreen.tWaitToLogin == 100)
			{
				if (GameCanvas.loginScr == null)
				{
					GameCanvas.loginScr = new LoginScr();
				}
				GameCanvas.loginScr.doLogin();
				Service.gI().finishUpdate();
				ServerListScreen.waitToLogin = false;
			}
		}
		if (ServerListScreen.flagServer > 0)
		{
			ServerListScreen.flagServer--;
			if (ServerListScreen.flagServer == 0)
			{
				GameCanvas.endDlg();
			}
		}
		for (int i = 0; i < this.cmd.Length; i++)
		{
			if (i == ServerListScreen.selected)
			{
				this.cmd[i].isFocus = true;
			}
			else
			{
				this.cmd[i].isFocus = false;
			}
		}
		GameScr.cmx++;
		if (!ServerListScreen.loadScreen && (ServerListScreen.bigOk || ServerListScreen.percent == 100))
		{
			ServerListScreen.cmdDownload = null;
		}
		base.update();
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x00005C65 File Offset: 0x00003E65
	private void processInput()
	{
		if (ServerListScreen.loadScreen)
		{
			this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
			return;
		}
		this.center = ServerListScreen.cmdDownload;
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x00005C9D File Offset: 0x00003E9D
	public static void updateDeleteData()
	{
		if (ServerListScreen.cmdDeleteRMS != null && ServerListScreen.cmdDeleteRMS.isPointerPressInside())
		{
			ServerListScreen.cmdDeleteRMS.performAction();
		}
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x00089BC4 File Offset: 0x00087DC4
	public override void updateKey()
	{
		if (GameCanvas.isTouch)
		{
			ServerListScreen.updateDeleteData();
			if (this.cmdCallHotline != null && this.cmdCallHotline.isPointerPressInside())
			{
				this.cmdCallHotline.performAction();
			}
			if (!ServerListScreen.loadScreen)
			{
				if (ServerListScreen.cmdDownload != null && ServerListScreen.cmdDownload.isPointerPressInside())
				{
					ServerListScreen.cmdDownload.performAction();
				}
				base.updateKey();
				return;
			}
			for (int i = 0; i < this.cmd.Length; i++)
			{
				if (this.cmd[i] != null && this.cmd[i].isPointerPressInside())
				{
					this.cmd[i].performAction();
				}
			}
		}
		else if (ServerListScreen.loadScreen)
		{
			if (GameCanvas.keyPressed[8])
			{
				int num = (mGraphics.zoomLevel <= 1) ? 4 : 2;
				GameCanvas.keyPressed[8] = false;
				ServerListScreen.selected++;
				if (ServerListScreen.selected > num)
				{
					ServerListScreen.selected = 0;
				}
				this.processInput();
			}
			if (GameCanvas.keyPressed[2])
			{
				int num2 = (mGraphics.zoomLevel <= 1) ? 4 : 2;
				GameCanvas.keyPressed[2] = false;
				ServerListScreen.selected--;
				if (ServerListScreen.selected < 0)
				{
					ServerListScreen.selected = num2;
				}
				this.processInput();
			}
		}
		if (!ServerListScreen.isWait)
		{
			base.updateKey();
		}
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x00089CFC File Offset: 0x00087EFC
	public static void saveIP()
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeByte(mResources.language);
			dataOutputStream.writeByte((sbyte)ServerListScreen.nameServer.Length);
			for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
			{
				dataOutputStream.writeUTF(ServerListScreen.nameServer[i]);
				dataOutputStream.writeUTF(ServerListScreen.address[i]);
				dataOutputStream.writeShort(ServerListScreen.port[i]);
				dataOutputStream.writeByte(ServerListScreen.language[i]);
			}
			dataOutputStream.writeByte(ServerListScreen.serverPriority);
			Rms.saveRMS("NRlink2", dataOutputStream.toByteArray());
			dataOutputStream.close();
			SplashScr.loadIP();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x00089DAC File Offset: 0x00087FAC
	public static bool allServerConnected()
	{
		for (int i = 0; i < 2; i++)
		{
			if (!ServerListScreen.hasConnected[i])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x00089DD4 File Offset: 0x00087FD4
	public static void loadIP()
	{
		sbyte[] array = Rms.loadRMS("NRlink2");
		if (array == null)
		{
			ServerListScreen.getServerList(ServerListScreen.linkDefault);
			return;
		}
		DataInputStream dataInputStream = new DataInputStream(array);
		if (dataInputStream != null)
		{
			try
			{
				ServerListScreen.lengthServer = new int[3];
				mResources.loadLanguague(dataInputStream.readByte());
				sbyte b = dataInputStream.readByte();
				ServerListScreen.nameServer = new string[(int)b];
				ServerListScreen.address = new string[(int)b];
				ServerListScreen.port = new short[(int)b];
				ServerListScreen.language = new sbyte[(int)b];
				for (int i = 0; i < (int)b; i++)
				{
					ServerListScreen.nameServer[i] = dataInputStream.readUTF();
					ServerListScreen.address[i] = dataInputStream.readUTF();
					ServerListScreen.port[i] = dataInputStream.readShort();
					ServerListScreen.language[i] = dataInputStream.readByte();
					ServerListScreen.lengthServer[(int)ServerListScreen.language[i]]++;
				}
				ServerListScreen.serverPriority = dataInputStream.readByte();
				dataInputStream.close();
				SplashScr.loadIP();
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x00089ED4 File Offset: 0x000880D4
	public override void switchToMe()
	{
		GameCanvas.connect();
		GameScr.cmy = 0;
		GameScr.cmx = 0;
		this.initCommand();
		ServerListScreen.isWait = false;
		GameCanvas.loginScr = null;
		string text = Rms.loadRMSString("ResVersion");
		if (((text == null || !(text != string.Empty)) ? -1 : int.Parse(text)) > 0)
		{
			ServerListScreen.loadScreen = true;
			GameCanvas.loadBG(0);
		}
		ServerListScreen.bigOk = true;
		this.cmd[2 + this.nCmdPlay].caption = mResources.server + ": " + ServerListScreen.nameServer[ServerListScreen.ipSelect];
		this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
		this.cmd[1 + this.nCmdPlay].caption = mResources.change_account;
		if (this.cmd.Length == 4 + this.nCmdPlay)
		{
			this.cmd[3 + this.nCmdPlay].caption = mResources.option;
		}
		mSystem.resetCurInapp();
		base.switchToMe();
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x00089FE0 File Offset: 0x000881E0
	public void switchToMe2()
	{
		GameScr.cmy = 0;
		GameScr.cmx = 0;
		this.initCommand();
		ServerListScreen.isWait = false;
		GameCanvas.loginScr = null;
		string text = Rms.loadRMSString("ResVersion");
		if (((text == null || !(text != string.Empty)) ? -1 : int.Parse(text)) > 0)
		{
			ServerListScreen.loadScreen = true;
			GameCanvas.loadBG(0);
		}
		ServerListScreen.bigOk = true;
		this.cmd[2 + this.nCmdPlay].caption = mResources.server + ": " + ServerListScreen.nameServer[ServerListScreen.ipSelect];
		this.center = new Command(string.Empty, this, this.cmd[ServerListScreen.selected].idAction, null);
		this.cmd[1 + this.nCmdPlay].caption = mResources.change_account;
		if (this.cmd.Length == 4 + this.nCmdPlay)
		{
			this.cmd[3 + this.nCmdPlay].caption = mResources.option;
		}
		mSystem.resetCurInapp();
		base.switchToMe();
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x00004426 File Offset: 0x00002626
	public void connectOk()
	{
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x0008A0E4 File Offset: 0x000882E4
	public void cancel()
	{
		if (GameCanvas.serverScreen == null)
		{
			GameCanvas.serverScreen = new ServerListScreen();
		}
		ServerListScreen.demPercent = 0;
		ServerListScreen.percent = 0;
		ServerListScreen.stopDownload = true;
		GameCanvas.serverScreen.show2();
		ServerListScreen.isGetData = false;
		ServerListScreen.cmdDownload.isFocus = true;
		this.center = new Command(string.Empty, this, 2, null);
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x0008A144 File Offset: 0x00088344
	public void perform(int idAction, object p)
	{
		Res.outz("perform " + idAction);
		if (idAction == 1000)
		{
			GameCanvas.connect();
		}
		if (idAction == 1 || idAction == 4)
		{
			this.cancel();
		}
		if (idAction == 2)
		{
			ServerListScreen.stopDownload = false;
			ServerListScreen.cmdDownload = new Command(mResources.huy, this, 4, null);
			ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
			ServerListScreen.cmdDownload.y = GameCanvas.hh + 65;
			this.right = null;
			if (!GameCanvas.isTouch)
			{
				ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
				ServerListScreen.cmdDownload.y = GameCanvas.h - mScreen.cmdH - 1;
			}
			this.center = new Command(string.Empty, this, 4, null);
			if (!ServerListScreen.isGetData)
			{
				Service.gI().getResource(1, null);
				if (!GameCanvas.isTouch)
				{
					ServerListScreen.cmdDownload.isFocus = true;
					this.center = new Command(string.Empty, this, 4, null);
				}
				ServerListScreen.isGetData = true;
			}
		}
		if (idAction == 3)
		{
			Res.outz("toi day");
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
			bool flag = Rms.loadRMSString("acc") != null && !Rms.loadRMSString("acc").Equals(string.Empty);
			bool flag2 = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect) != null && !Rms.loadRMSString("userAo" + ServerListScreen.ipSelect).Equals(string.Empty);
			if (!flag && !flag2)
			{
				GameCanvas.connect();
				string text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect);
				if (text == null || text.Equals(string.Empty))
				{
					Service.gI().login2(string.Empty);
				}
				else
				{
					GameCanvas.loginScr.isLogin2 = true;
					GameCanvas.connect();
					Service.gI().setClientType();
					Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
				}
				if (Session_ME.connected)
				{
					GameCanvas.startWaitDlg();
				}
				else
				{
					GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
				}
			}
			else
			{
				GameCanvas.loginScr.doLogin();
			}
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		}
		if (idAction == 10100)
		{
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
			GameCanvas.connect();
			Service.gI().login2(string.Empty);
			Res.outz("tao user ao");
			GameCanvas.startWaitDlg();
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		}
		if (idAction == 5)
		{
			ServerListScreen.doUpdateServer();
			if (ServerListScreen.nameServer.Length == 1)
			{
				return;
			}
			MyVector myVector = new MyVector(string.Empty);
			for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
			{
				myVector.addElement(new Command(ServerListScreen.nameServer[i], this, 6, null));
			}
			GameCanvas.menu.startAt(myVector, 0);
			if (!GameCanvas.isTouch)
			{
				GameCanvas.menu.menuSelectedItem = ServerListScreen.ipSelect;
			}
		}
		if (idAction == 6)
		{
			ServerListScreen.ipSelect = GameCanvas.menu.menuSelectedItem;
			this.selectServer();
		}
		if (idAction == 7)
		{
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
		}
		if (idAction == 8)
		{
			bool flag3 = Rms.loadRMSInt("lowGraphic") == 1;
			MyVector myVector2 = new MyVector("cau hinh");
			myVector2.addElement(new Command(mResources.cauhinhthap, this, 9, null));
			myVector2.addElement(new Command(mResources.cauhinhcao, this, 10, null));
			GameCanvas.menu.startAt(myVector2, 0);
			if (flag3)
			{
				GameCanvas.menu.menuSelectedItem = 0;
			}
			else
			{
				GameCanvas.menu.menuSelectedItem = 1;
			}
		}
		if (idAction == 9)
		{
			Rms.saveRMSInt("lowGraphic", 1);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 10)
		{
			Rms.saveRMSInt("lowGraphic", 0);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 11)
		{
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
			string text2 = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect);
			if (text2 == null || text2.Equals(string.Empty))
			{
				Service.gI().login2(string.Empty);
			}
			else
			{
				GameCanvas.loginScr.isLogin2 = true;
				GameCanvas.connect();
				Service.gI().setClientType();
				Service.gI().login(text2, string.Empty, GameMidlet.VERSION, 1);
			}
			GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
			Res.outz("tao user ao");
		}
		if (idAction == 12)
		{
			GameMidlet.instance.exit();
		}
		if (idAction == 13 && (!ServerListScreen.isGetData || ServerListScreen.loadScreen))
		{
			switch (mSystem.clientType)
			{
			case 1:
				mSystem.callHotlineJava();
				break;
			case 3:
			case 5:
				mSystem.callHotlineIphone();
				break;
			case 4:
				mSystem.callHotlinePC();
				break;
			case 6:
				mSystem.callHotlineWindowsPhone();
				break;
			}
		}
		if (idAction == 14)
		{
			Command cmdYes = new Command(mResources.YES, GameCanvas.serverScreen, 15, null);
			Command cmdNo = new Command(mResources.NO, GameCanvas.serverScreen, 16, null);
			GameCanvas.startYesNoDlg(mResources.deletaDataNote, cmdYes, cmdNo);
		}
		if (idAction == 15)
		{
			Rms.clearAll();
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 16)
		{
			InfoDlg.hide();
			GameCanvas.currentDialog = null;
		}
		if (idAction == 17)
		{
			if (GameCanvas.serverScr == null)
			{
				GameCanvas.serverScr = new ServerScr();
			}
			GameCanvas.serverScr.switchToMe();
		}
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x0008A6D8 File Offset: 0x000888D8
	public void init()
	{
		if (!ServerListScreen.loadScreen)
		{
			ServerListScreen.cmdDownload = new Command(mResources.taidulieu, this, 2, null);
			ServerListScreen.cmdDownload.isFocus = true;
			ServerListScreen.cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
			ServerListScreen.cmdDownload.y = GameCanvas.hh + 45;
			if (ServerListScreen.cmdDownload.y > GameCanvas.h - 26)
			{
				ServerListScreen.cmdDownload.y = GameCanvas.h - 26;
			}
		}
		if (!GameCanvas.isTouch)
		{
			ServerListScreen.selected = 0;
			this.processInput();
		}
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x0008A770 File Offset: 0x00088970
	public void show2()
	{
		GameScr.cmx = 0;
		GameScr.cmy = 0;
		this.initCommand();
		ServerListScreen.loadScreen = false;
		ServerListScreen.percent = 0;
		ServerListScreen.bigOk = false;
		ServerListScreen.isGetData = false;
		ServerListScreen.p = 0;
		ServerListScreen.demPercent = 0;
		ServerListScreen.strWait = mResources.PLEASEWAIT;
		this.init();
		base.switchToMe();
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x0008A7CC File Offset: 0x000889CC
	public void setLinkDefault(sbyte language)
	{
		if (language == 2)
		{
			if (mSystem.clientType == 1)
			{
				ServerListScreen.linkDefault = ServerListScreen.javaIn;
				return;
			}
			ServerListScreen.linkDefault = ServerListScreen.smartPhoneIn;
			return;
		}
		else if (language == 1)
		{
			ServerListScreen.linkDefault = ServerListScreen.javaE;
			if (mSystem.clientType == 1)
			{
				ServerListScreen.linkDefault = ServerListScreen.javaE;
				return;
			}
			ServerListScreen.linkDefault = ServerListScreen.smartPhoneE;
			return;
		}
		else
		{
			ServerListScreen.linkDefault = ServerListScreen.javaVN;
			if (mSystem.clientType == 1)
			{
				ServerListScreen.linkDefault = ServerListScreen.javaVN;
				return;
			}
			ServerListScreen.linkDefault = ServerListScreen.smartPhoneVN;
			return;
		}
	}

	// Token: 0x04000FF9 RID: 4089
	public static string[] nameServer;

	// Token: 0x04000FFA RID: 4090
	public static string[] address;

	// Token: 0x04000FFB RID: 4091
	public static sbyte serverPriority;

	// Token: 0x04000FFC RID: 4092
	public static bool[] hasConnected;

	// Token: 0x04000FFD RID: 4093
	public static short[] port;

	// Token: 0x04000FFE RID: 4094
	public static int selected;

	// Token: 0x04000FFF RID: 4095
	public static bool isWait;

	// Token: 0x04001000 RID: 4096
	public static Command cmdUpdateServer;

	// Token: 0x04001001 RID: 4097
	public static sbyte[] language;

	// Token: 0x04001002 RID: 4098
	private Command[] cmd;

	// Token: 0x04001003 RID: 4099
	private Command cmdCallHotline;

	// Token: 0x04001004 RID: 4100
	private int nCmdPlay;

	// Token: 0x04001005 RID: 4101
	public static Command cmdDeleteRMS;

	// Token: 0x04001006 RID: 4102
	private int lY;

	// Token: 0x04001007 RID: 4103
	public static string smartPhoneVN = "DARLING:103.179.173.205:14445:0,0,0";

	// Token: 0x04001008 RID: 4104
	public static string javaVN = "DARLING:103.179.173.205:14445:0,0,0";

	// Token: 0x04001009 RID: 4105
	public static string smartPhoneIn = "Naga:dragon.indonaga.com:14446:2,2,0";

	// Token: 0x0400100A RID: 4106
	public static string javaIn = "Naga:54.179.255.27:14446:2,2,0";

	// Token: 0x0400100B RID: 4107
	public static string smartPhoneE = "Universe 1:54.179.255.27:14445:1,1,0";

	// Token: 0x0400100C RID: 4108
	public static string javaE = "Universe 1:54.179.255.27:14445:1,1,0";

	// Token: 0x0400100D RID: 4109
	public static string linkGetHost = "http://sv1.ngocrongonline.com/game/ngocrong031_t.php";

	// Token: 0x0400100E RID: 4110
	public static string linkDefault = ServerListScreen.javaVN;

	// Token: 0x0400100F RID: 4111
	public const sbyte languageVersion = 2;

	// Token: 0x04001010 RID: 4112
	public new int keyTouch = -1;

	// Token: 0x04001011 RID: 4113
	private int tam;

	// Token: 0x04001012 RID: 4114
	public static bool stopDownload;

	// Token: 0x04001013 RID: 4115
	public static string linkweb = "https://www.youtube.com/channel/UClrZSuj2uqbcr6KNsHaqsZw";

	// Token: 0x04001014 RID: 4116
	public static bool waitToLogin;

	// Token: 0x04001015 RID: 4117
	public static int tWaitToLogin;

	// Token: 0x04001016 RID: 4118
	public static int[] lengthServer = new int[3];

	// Token: 0x04001017 RID: 4119
	public static int ipSelect;

	// Token: 0x04001018 RID: 4120
	public static int flagServer;

	// Token: 0x04001019 RID: 4121
	public static bool bigOk;

	// Token: 0x0400101A RID: 4122
	public static int percent;

	// Token: 0x0400101B RID: 4123
	public static string strWait;

	// Token: 0x0400101C RID: 4124
	public static int nBig;

	// Token: 0x0400101D RID: 4125
	public static int nBg;

	// Token: 0x0400101E RID: 4126
	public static int demPercent;

	// Token: 0x0400101F RID: 4127
	public static int maxBg;

	// Token: 0x04001020 RID: 4128
	public static bool isGetData = false;

	// Token: 0x04001021 RID: 4129
	public static Command cmdDownload;

	// Token: 0x04001022 RID: 4130
	private Command cmdStart;

	// Token: 0x04001023 RID: 4131
	public string dataSize;

	// Token: 0x04001024 RID: 4132
	public static int p;

	// Token: 0x04001025 RID: 4133
	public static int testConnect = -1;

	// Token: 0x04001026 RID: 4134
	public static bool loadScreen;
}
