using System;

// Token: 0x0200005C RID: 92
public class LoginScr : mScreen, IActionListener
{
	// Token: 0x0600045A RID: 1114 RVA: 0x000559B8 File Offset: 0x00053BB8
	public LoginScr()
	{
		this.yLog = GameCanvas.hh - 30;
		TileMap.bgID = (int)((sbyte)(mSystem.currentTimeMillis() % 9L));
		bool flag = TileMap.bgID == 5 || TileMap.bgID == 6;
		if (flag)
		{
			TileMap.bgID = 4;
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
		Main.closeKeyBoard();
		bool flag2 = GameCanvas.h > 200;
		if (flag2)
		{
			this.defYL = GameCanvas.hh - 80;
		}
		else
		{
			this.defYL = GameCanvas.hh - 65;
		}
		this.resetLogo();
		int num = this.wC = ((GameCanvas.w < 200) ? 140 : 160);
		this.yt = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;
		bool flag3 = GameCanvas.h <= 160;
		if (flag3)
		{
			this.yt = 20;
		}
		this.tfUser = new TField();
		this.tfUser.y = GameCanvas.hh - mScreen.ITEM_HEIGHT - 9;
		this.tfUser.width = this.wC;
		this.tfUser.height = mScreen.ITEM_HEIGHT + 2;
		this.tfUser.isFocus = true;
		this.tfUser.setIputType(TField.INPUT_TYPE_ANY);
		this.tfUser.name = ((mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
		this.tfPass = new TField();
		this.tfPass.y = GameCanvas.hh - 4;
		this.tfPass.setIputType(TField.INPUT_TYPE_PASSWORD);
		this.tfPass.width = this.wC;
		this.tfPass.height = mScreen.ITEM_HEIGHT + 2;
		this.yt += 35;
		this.isCheck = true;
		int num2 = Rms.loadRMSInt("check");
		if (num2 != 1)
		{
			if (num2 == 2)
			{
				this.isCheck = false;
			}
		}
		else
		{
			this.isCheck = true;
		}
		this.tfUser.setText(Rms.loadRMSString("acc"));
		this.tfPass.setText(Rms.loadRMSString("pass"));
		bool flag4 = this.cmdCallHotline == null;
		if (flag4)
		{
			this.cmdCallHotline = new Command("Gọi hotline", this, 13, null);
			this.cmdCallHotline.x = GameCanvas.w - 75;
			bool flag5 = mSystem.clientType == 1 && !GameCanvas.isTouch;
			if (flag5)
			{
				this.cmdCallHotline.y = GameCanvas.h - 20;
			}
			else
			{
				int num3 = 2;
				this.cmdCallHotline.y = num3 + 6;
			}
		}
		this.focus = 0;
		this.cmdLogin = new Command((GameCanvas.w <= 200) ? mResources.login2 : mResources.login, GameCanvas.instance, 888393, null);
		this.cmdCheck = new Command(mResources.remember, this, 2001, null);
		this.cmdRes = new Command(mResources.register, this, 2002, null);
		this.cmdBackFromRegister = new Command(mResources.CANCEL, this, 10021, null);
		this.left = (this.cmdMenu = new Command(mResources.MENU, this, 2003, null));
		this.freeAreaHeight = this.tfUser.y - 2 * this.tfUser.height;
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.cmdLogin.x = GameCanvas.w / 2 + 8;
			this.cmdMenu.x = GameCanvas.w / 2 - mScreen.cmdW - 8;
			bool flag6 = GameCanvas.h >= 200;
			if (flag6)
			{
				this.cmdLogin.y = this.yLog + 110;
				this.cmdMenu.y = this.yLog + 110;
			}
			this.cmdBackFromRegister.x = GameCanvas.w / 2 + 3;
			this.cmdBackFromRegister.y = this.yLog + 110;
			this.cmdRes.x = GameCanvas.w / 2 - 84;
			this.cmdRes.y = this.cmdMenu.y;
		}
		this.wP = 170;
		this.hP = ((!this.isRes) ? 100 : 110);
		this.xP = GameCanvas.hw - this.wP / 2;
		this.yP = this.tfUser.y - 15;
		int num4 = 4;
		int num5 = num4 * 32 + 23 + 33;
		bool flag7 = num5 >= GameCanvas.w;
		if (flag7)
		{
			num4--;
			num5 = num4 * 32 + 23 + 33;
		}
		this.xLog = GameCanvas.w / 2 - num5 / 2;
		this.yLog = GameCanvas.hh - 30;
		this.lY = ((GameCanvas.w < 200) ? (this.tfUser.y - 30) : (this.yLog - 30));
		this.tfUser.x = this.xLog + 10;
		this.tfUser.y = this.yLog + 20;
		this.cmdOK = new Command(mResources.OK, this, 2008, null);
		this.cmdOK.x = GameCanvas.w / 2 - 84;
		this.cmdOK.y = this.cmdLogin.y;
		this.cmdFogetPass = new Command(mResources.forgetPass, this, 1003, null);
		this.cmdFogetPass.x = GameCanvas.w / 2 + 3;
		this.cmdFogetPass.y = this.cmdLogin.y;
		this.center = this.cmdOK;
		this.left = this.cmdFogetPass;
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x00055FF4 File Offset: 0x000541F4
	public static void getServerLink()
	{
		try
		{
			bool flag = LoginScr.isTryGetIPFromWap;
			if (!flag)
			{
				Command command = new Command();
				ActionChat actionChat = command.actionChat = delegate(string str)
				{
					try
					{
						bool flag2 = str != null && !(str == string.Empty);
						if (flag2)
						{
							Rms.saveIP(str);
							bool flag3 = str.Contains(":");
							if (flag3)
							{
								int num = str.IndexOf(":");
								string text = str.Substring(0, num);
								string s = str.Substring(num + 1);
								GameMidlet.IP = text;
								GameMidlet.PORT = int.Parse(s);
								Session_ME.gI().connect(text, int.Parse(s));
								LoginScr.isTryGetIPFromWap = true;
							}
						}
					}
					catch (Exception)
					{
					}
				};
				Net.connectHTTP(ServerListScreen.linkGetHost, command);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x00056064 File Offset: 0x00054264
	public override void switchToMe()
	{
		this.isRegistering = false;
		SoundMn.gI().stopAll();
		this.tfUser.isFocus = true;
		this.tfPass.isFocus = false;
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.tfUser.isFocus = false;
		}
		GameCanvas.loadBG(0);
		base.switchToMe();
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x000560C4 File Offset: 0x000542C4
	public void setUserPass()
	{
		string text = Rms.loadRMSString("acc");
		bool flag = text != null && !text.Equals(string.Empty);
		if (flag)
		{
			this.tfUser.setText(text);
		}
		string text2 = Rms.loadRMSString("pass");
		bool flag2 = text2 != null && !text2.Equals(string.Empty);
		if (flag2)
		{
			this.tfPass.setText(text2);
		}
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void updateTfWhenOpenKb()
	{
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x00056138 File Offset: 0x00054338
	protected void doMenu()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(mResources.registerNewAcc, this, 2004, null));
		bool flag = !this.isLogin2;
		if (flag)
		{
			myVector.addElement(new Command(mResources.selectServer, this, 1004, null));
		}
		myVector.addElement(new Command(mResources.forgetPass, this, 1003, null));
		myVector.addElement(new Command(mResources.website, this, 1005, null));
		bool isPC = Main.isPC;
		if (isPC)
		{
			myVector.addElement(new Command(mResources.EXIT, GameCanvas.instance, 8885, null));
		}
		GameCanvas.menu.startAt(myVector, 0);
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x000561F0 File Offset: 0x000543F0
	protected void doRegister()
	{
		bool flag = this.tfUser.getText().Equals(string.Empty);
		if (flag)
		{
			GameCanvas.startOKDlg(mResources.userBlank);
		}
		else
		{
			char[] array = this.tfUser.getText().ToCharArray();
			bool flag2 = this.tfPass.getText().Equals(string.Empty);
			if (flag2)
			{
				GameCanvas.startOKDlg(mResources.passwordBlank);
			}
			else
			{
				bool flag3 = this.tfUser.getText().Length < 5;
				if (flag3)
				{
					GameCanvas.startOKDlg(mResources.accTooShort);
				}
				else
				{
					int num = 0;
					string text = null;
					bool flag4 = mResources.language == 2;
					if (flag4)
					{
						bool flag5 = this.tfUser.getText().IndexOf("@") == -1 || this.tfUser.getText().IndexOf(".") == -1;
						if (flag5)
						{
							text = mResources.emailInvalid;
						}
						num = 0;
					}
					else
					{
						try
						{
							long num2 = long.Parse(this.tfUser.getText());
							bool flag6 = this.tfUser.getText().Length < 8 || this.tfUser.getText().Length > 12 || (!this.tfUser.getText().StartsWith("0") && !this.tfUser.getText().StartsWith("84"));
							if (flag6)
							{
								text = mResources.phoneInvalid;
							}
							num = 1;
						}
						catch (Exception)
						{
							bool flag7 = this.tfUser.getText().IndexOf("@") == -1 || this.tfUser.getText().IndexOf(".") == -1;
							if (flag7)
							{
								text = mResources.emailInvalid;
							}
							num = 0;
						}
					}
					bool flag8 = text != null;
					if (flag8)
					{
						GameCanvas.startOKDlg(text);
					}
					else
					{
						GameCanvas.msgdlg.setInfo(string.Concat(new string[]
						{
							mResources.plsCheckAcc,
							(num != 1) ? (mResources.email + ": ") : (mResources.phone + ": "),
							this.tfUser.getText(),
							"\n",
							mResources.password,
							": ",
							this.tfPass.getText()
						}), new Command(mResources.ACCEPT, this, 4000, null), null, new Command(mResources.NO, GameCanvas.instance, 8882, null));
					}
					GameCanvas.currentDialog = GameCanvas.msgdlg;
				}
			}
		}
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x0005648C File Offset: 0x0005468C
	protected void doRegister(string user)
	{
		this.isFAQ = false;
		GameCanvas.startWaitDlg(mResources.CONNECTING);
		GameCanvas.connect();
		GameCanvas.startWaitDlg(mResources.REGISTERING);
		this.passRe = this.tfPass.getText();
		Service.gI().requestRegister(user, this.tfPass.getText(), Rms.loadRMSString("userAo" + ServerListScreen.ipSelect), Rms.loadRMSString("passAo" + ServerListScreen.ipSelect), GameMidlet.VERSION);
		Rms.saveRMSString("acc", user);
		Rms.saveRMSString("pass", this.tfPass.getText());
		this.t = 20;
		this.isRegistering = true;
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x00056550 File Offset: 0x00054750
	public void doViewFAQ()
	{
		bool flag = !this.listFAQ.Equals(string.Empty) || !this.listFAQ.Equals(string.Empty);
		if (flag)
		{
		}
		bool flag2 = !Session_ME.connected;
		if (flag2)
		{
			this.isFAQ = true;
			GameCanvas.connect();
		}
		GameCanvas.startWaitDlg();
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x000565B0 File Offset: 0x000547B0
	protected void doSelectServer()
	{
		MyVector myVector = new MyVector();
		bool flag = LoginScr.isLocal;
		if (flag)
		{
			myVector.addElement(new Command("Server LOCAL", this, 20004, null));
		}
		myVector.addElement(new Command("Server Bokken", this, 20001, null));
		myVector.addElement(new Command("Server Shuriken", this, 20002, null));
		myVector.addElement(new Command("Server Tessen (mới)", this, 20003, null));
		GameCanvas.menu.startAt(myVector, 0);
		bool flag2 = this.loadIndexServer() != -1 && !GameCanvas.isTouch;
		if (flag2)
		{
			GameCanvas.menu.menuSelectedItem = this.loadIndexServer();
		}
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x00004B98 File Offset: 0x00002D98
	protected void saveIndexServer(int index)
	{
		Rms.saveRMSInt("indServer", index);
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x00056668 File Offset: 0x00054868
	protected int loadIndexServer()
	{
		return Rms.loadRMSInt("indServer");
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x00056684 File Offset: 0x00054884
	public void doLogin()
	{
		string text = Rms.loadRMSString("acc");
		string text2 = Rms.loadRMSString("pass");
		bool flag = text != null && !text.Equals(string.Empty);
		if (flag)
		{
			this.isLogin2 = false;
		}
		else
		{
			bool flag2 = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect) != null && !Rms.loadRMSString("userAo" + ServerListScreen.ipSelect).Equals(string.Empty);
			if (flag2)
			{
				this.isLogin2 = true;
			}
			else
			{
				this.isLogin2 = false;
			}
		}
		bool flag3 = (text == null || text.Equals(string.Empty)) && this.isLogin2;
		if (flag3)
		{
			text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect);
			text2 = "a";
		}
		bool flag4 = text == null || text2 == null || GameMidlet.VERSION == null || text.Equals(string.Empty);
		if (!flag4)
		{
			bool flag5 = text2.Equals(string.Empty);
			if (flag5)
			{
				this.focus = 1;
				this.tfUser.isFocus = false;
				this.tfPass.isFocus = true;
				bool flag6 = !GameCanvas.isTouch;
				if (flag6)
				{
					this.right = this.tfPass.cmdClear;
				}
			}
			else
			{
				GameCanvas.connect();
				Res.outz(string.Concat(new object[]
				{
					"ccccccc ",
					text,
					" ",
					text2,
					" ",
					GameMidlet.VERSION,
					" ",
					this.isLogin2 ? 1 : 0
				}));
				Service.gI().login(text, text2, GameMidlet.VERSION, this.isLogin2 ? 1 : 0);
				bool connected = Session_ME.connected;
				if (connected)
				{
					GameCanvas.startWaitDlg();
				}
				else
				{
					GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
				}
				this.focus = 0;
				bool flag7 = !this.isLogin2;
				if (flag7)
				{
					this.actRegisterLeft();
				}
			}
		}
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x000568A4 File Offset: 0x00054AA4
	public void savePass()
	{
		bool flag = this.isCheck;
		if (flag)
		{
			Rms.saveRMSInt("check", 1);
			Rms.saveRMSString("acc", this.tfUser.getText().ToLower().Trim());
			Rms.saveRMSString("pass", this.tfPass.getText().ToLower().Trim());
		}
		else
		{
			Rms.saveRMSInt("check", 2);
			Rms.saveRMSString("acc", string.Empty);
			Rms.saveRMSString("pass", string.Empty);
		}
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x0005693C File Offset: 0x00054B3C
	public override void update()
	{
		bool flag = Main.isWindowsPhone && this.isRegistering;
		if (flag)
		{
			bool flag2 = this.t < 0;
			if (flag2)
			{
				GameCanvas.endDlg();
				Session_ME.gI().close();
				GameCanvas.serverScreen.switchToMe();
				this.isRegistering = false;
			}
			else
			{
				this.t--;
			}
		}
		bool flag3 = LoginScr.timeLogin > 0;
		if (flag3)
		{
			GameCanvas.startWaitDlg();
			LoginScr.currTimeLogin = mSystem.currentTimeMillis();
			bool flag4 = LoginScr.currTimeLogin - LoginScr.lastTimeLogin >= 1000L;
			if (flag4)
			{
				LoginScr.timeLogin -= 1;
				bool flag5 = LoginScr.timeLogin == 0;
				if (flag5)
				{
					Session_ME.gI().close();
					GameCanvas.loginScr.doLogin();
				}
				LoginScr.lastTimeLogin = LoginScr.currTimeLogin;
			}
		}
		bool flag6 = this.isLogin2 && !this.isRes;
		if (flag6)
		{
			this.tfUser.name = ((mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.isPaintCarret = false;
			this.tfPass.isPaintCarret = false;
			this.tfUser.update();
			this.tfPass.update();
		}
		else
		{
			this.tfUser.name = ((mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
			this.tfPass.name = mResources.password;
			this.tfUser.update();
			this.tfPass.update();
		}
		bool visible = TouchScreenKeyboard.visible;
		if (visible)
		{
			mGraphics.addYWhenOpenKeyBoard = 50;
		}
		for (int i = 0; i < Effect2.vEffect2.size(); i++)
		{
			Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
			effect.update();
		}
		bool flag7 = LoginScr.isUpdateAll && !LoginScr.isUpdateData && !LoginScr.isUpdateItem && !LoginScr.isUpdateMap && !LoginScr.isUpdateSkill;
		if (flag7)
		{
			LoginScr.isUpdateAll = false;
			mSystem.gcc();
			Service.gI().finishUpdate();
		}
		GameScr.cmx++;
		bool flag8 = GameScr.cmx > GameCanvas.w * 3 + 100;
		if (flag8)
		{
			GameScr.cmx = 100;
		}
		bool flag9 = ChatPopup.currChatPopup != null;
		if (!flag9)
		{
			GameCanvas.debug("LGU1", 0);
			GameCanvas.debug("LGU2", 0);
			GameCanvas.debug("LGU3", 0);
			this.updateLogo();
			GameCanvas.debug("LGU4", 0);
			GameCanvas.debug("LGU5", 0);
			bool flag10 = this.g >= 0;
			if (flag10)
			{
				this.ylogo += this.dir * this.g;
				this.g += this.dir * this.v;
				bool flag11 = this.g <= 0;
				if (flag11)
				{
					this.dir *= -1;
				}
				bool flag12 = this.ylogo > 0;
				if (flag12)
				{
					this.dir *= -1;
					this.g -= 2 * this.v;
				}
			}
			GameCanvas.debug("LGU6", 0);
			bool flag13 = this.tipid >= 0 && GameCanvas.gameTick % 100 == 0;
			if (flag13)
			{
				this.doChangeTip();
			}
			bool flag14 = this.isLogin2 && !this.isRes;
			if (flag14)
			{
				this.tfUser.isPaintCarret = false;
				this.tfPass.isPaintCarret = false;
				this.tfUser.update();
				this.tfPass.update();
			}
			else
			{
				this.tfUser.name = ((mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
				this.tfPass.name = mResources.password;
				this.tfUser.update();
				this.tfPass.update();
			}
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				bool flag15 = this.isRes;
				if (flag15)
				{
					this.center = this.cmdRes;
					this.left = this.cmdBackFromRegister;
				}
				else
				{
					this.center = this.cmdOK;
					this.left = this.cmdFogetPass;
				}
			}
			else
			{
				bool flag16 = this.isRes;
				if (flag16)
				{
					this.center = this.cmdRes;
					this.left = this.cmdBackFromRegister;
				}
				else
				{
					this.center = this.cmdOK;
					this.left = this.cmdFogetPass;
				}
			}
			bool flag17 = !Main.isPC && !TouchScreenKeyboard.visible && !Main.isMiniApp && !Main.isWindowsPhone;
			if (flag17)
			{
				string text = this.tfUser.getText().ToLower().Trim();
				string text2 = this.tfPass.getText().ToLower().Trim();
				bool flag18 = !text.Equals(string.Empty) && !text2.Equals(string.Empty);
				if (flag18)
				{
					this.doLogin();
				}
				Main.isMiniApp = true;
			}
			this.updateTfWhenOpenKb();
		}
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x00056EC8 File Offset: 0x000550C8
	private void doChangeTip()
	{
		this.tipid++;
		bool flag = this.tipid >= mResources.tips.Length;
		if (flag)
		{
			this.tipid = 0;
		}
		bool flag2 = GameCanvas.currentDialog == GameCanvas.msgdlg && GameCanvas.msgdlg.isWait;
		if (flag2)
		{
			GameCanvas.msgdlg.setInfo(mResources.tips[this.tipid]);
		}
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x00056F3C File Offset: 0x0005513C
	public void updateLogo()
	{
		bool flag = this.defYL != this.yL;
		if (flag)
		{
			this.yL += this.defYL - this.yL >> 1;
		}
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00056F80 File Offset: 0x00055180
	public override void keyPress(int keyCode)
	{
		bool isFocus = this.tfUser.isFocus;
		if (isFocus)
		{
			this.tfUser.keyPressed(keyCode);
		}
		else
		{
			bool isFocus2 = this.tfPass.isFocus;
			if (isFocus2)
			{
				this.tfPass.keyPressed(keyCode);
			}
		}
		base.keyPress(keyCode);
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x00004BA7 File Offset: 0x00002DA7
	public override void unLoad()
	{
		base.unLoad();
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x00056FD4 File Offset: 0x000551D4
	public override void paint(mGraphics g)
	{
		GameCanvas.debug("PLG1", 1);
		GameCanvas.paintBGGameScr(g);
		GameCanvas.debug("PLG2", 2);
		int num = this.tfUser.y - 50;
		bool flag = GameCanvas.h <= 220;
		if (flag)
		{
			num += 5;
		}
		mFont.tahoma_7_white.drawString(g, "v" + GameMidlet.VERSION, GameCanvas.w - 2, 17, 1, mFont.tahoma_7_grey);
		bool flag2 = mSystem.clientType == 1 && !GameCanvas.isTouch;
		if (flag2)
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
		}
		else
		{
			mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, 2, 1, mFont.tahoma_7_grey);
		}
		bool flag3 = ChatPopup.currChatPopup != null || ChatPopup.serverChatPopUp != null;
		if (!flag3)
		{
			bool flag4 = GameCanvas.currentDialog == null;
			if (flag4)
			{
				int h = 105;
				int w = (GameCanvas.w < 200) ? 160 : 180;
				PopUp.paintPopUp(g, this.xLog, this.yLog - 10, w, h, -1, true);
				bool flag5 = GameCanvas.h > 160 && LoginScr.imgTitle != null;
				if (flag5)
				{
					g.drawImage(LoginScr.imgTitle, GameCanvas.hw, num, 3);
				}
				GameCanvas.debug("PLG4", 1);
				int num2 = 4;
				int num3 = num2 * 32 + 23 + 33;
				bool flag6 = num3 >= GameCanvas.w;
				if (flag6)
				{
					num2--;
					num3 = num2 * 32 + 23 + 33;
				}
				this.xLog = GameCanvas.w / 2 - num3 / 2;
				this.tfUser.x = this.xLog + 10;
				this.tfUser.y = this.yLog + 20;
				this.tfPass.x = this.xLog + 10;
				this.tfPass.y = this.yLog + 55;
				this.tfUser.paint(g);
				this.tfPass.paint(g);
				bool flag7 = GameCanvas.w >= 176;
				if (!flag7)
				{
					mFont.tahoma_7b_green2.drawString(g, mResources.acc + ":", this.tfUser.x - 35, this.tfUser.y + 7, 0);
					mFont.tahoma_7b_green2.drawString(g, mResources.pwd + ":", this.tfPass.x - 35, this.tfPass.y + 7, 0);
					mFont.tahoma_7b_green2.drawString(g, mResources.server + ":" + LoginScr.serverName, GameCanvas.w / 2, this.tfPass.y + 32, 2);
				}
			}
			base.paint(g);
		}
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x000572E4 File Offset: 0x000554E4
	public override void updateKey()
	{
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			bool flag = this.cmdCallHotline != null && this.cmdCallHotline.isPointerPressInside();
			if (flag)
			{
				this.cmdCallHotline.performAction();
			}
		}
		else
		{
			bool flag2 = mSystem.clientType == 1 && GameCanvas.keyPressed[13];
			if (flag2)
			{
				GameCanvas.keyPressed[13] = false;
				this.cmdCallHotline.performAction();
			}
		}
		bool flag3 = LoginScr.isContinueToLogin;
		if (!flag3)
		{
			bool flag4 = !GameCanvas.isTouch;
			if (flag4)
			{
				bool isFocus = this.tfUser.isFocus;
				if (isFocus)
				{
					this.right = this.tfUser.cmdClear;
				}
				else
				{
					this.right = this.tfPass.cmdClear;
				}
			}
			bool flag5 = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21];
			if (flag5)
			{
				this.focus--;
				bool flag6 = this.focus < 0;
				if (flag6)
				{
					this.focus = 1;
				}
			}
			else
			{
				bool flag7 = GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[16];
				if (flag7)
				{
					this.focus++;
					bool flag8 = this.focus > 1;
					if (flag8)
					{
						this.focus = 0;
					}
				}
			}
			bool flag9 = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] || GameCanvas.keyPressed[16];
			if (flag9)
			{
				GameCanvas.clearKeyPressed();
				bool flag10 = !this.isLogin2 || this.isRes;
				if (flag10)
				{
					bool flag11 = this.focus == 1;
					if (flag11)
					{
						this.tfUser.isFocus = false;
						this.tfPass.isFocus = true;
					}
					else
					{
						bool flag12 = this.focus == 0;
						if (flag12)
						{
							this.tfUser.isFocus = true;
							this.tfPass.isFocus = false;
						}
						else
						{
							this.tfUser.isFocus = false;
							this.tfPass.isFocus = false;
						}
					}
				}
			}
			bool isTouch2 = GameCanvas.isTouch;
			if (isTouch2)
			{
				bool flag13 = this.isRes;
				if (flag13)
				{
					this.center = this.cmdRes;
					this.left = this.cmdBackFromRegister;
				}
				else
				{
					this.center = this.cmdOK;
					this.left = this.cmdFogetPass;
				}
			}
			else
			{
				bool flag14 = this.isRes;
				if (flag14)
				{
					this.center = this.cmdRes;
					this.left = this.cmdBackFromRegister;
				}
				else
				{
					this.center = this.cmdOK;
					this.left = this.cmdFogetPass;
				}
			}
			bool flag15 = GameCanvas.isPointerJustRelease && (!this.isLogin2 || this.isRes);
			if (flag15)
			{
				bool flag16 = GameCanvas.isPointerHoldIn(this.tfUser.x, this.tfUser.y, this.tfUser.width, this.tfUser.height);
				if (flag16)
				{
					this.focus = 0;
				}
				else
				{
					bool flag17 = GameCanvas.isPointerHoldIn(this.tfPass.x, this.tfPass.y, this.tfPass.width, this.tfPass.height);
					if (flag17)
					{
						this.focus = 1;
					}
				}
			}
			bool flag18 = Main.isPC && GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] && this.right != null;
			if (flag18)
			{
				this.right.performAction();
			}
			base.updateKey();
			GameCanvas.clearKeyPressed();
		}
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x00004BB1 File Offset: 0x00002DB1
	public void resetLogo()
	{
		this.yL = -50;
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0005768C File Offset: 0x0005588C
	public void perform(int idAction, object p)
	{
		if (idAction <= 2008)
		{
			if (idAction != 13)
			{
				switch (idAction)
				{
				case 1000:
					try
					{
						GameMidlet.instance.platformRequest((string)p);
					}
					catch (Exception)
					{
					}
					GameCanvas.endDlg();
					return;
				case 1001:
					GameCanvas.endDlg();
					this.isRes = false;
					return;
				case 1002:
				{
					GameCanvas.startWaitDlg();
					string text = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect);
					if (text == null || text.Equals(string.Empty))
					{
						Service.gI().login2(string.Empty);
						return;
					}
					GameCanvas.loginScr.isLogin2 = true;
					GameCanvas.connect();
					Service.gI().setClientType();
					Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
					return;
				}
				case 1003:
					GameCanvas.startOKDlg(mResources.goToWebForPassword);
					return;
				case 1004:
					ServerListScreen.doUpdateServer();
					GameCanvas.serverScreen.switchToMe();
					return;
				case 1005:
					try
					{
						GameMidlet.instance.platformRequest("https://www.youtube.com/channel/UClrZSuj2uqbcr6KNsHaqsZw");
						return;
					}
					catch (Exception)
					{
						return;
					}
					break;
				}
				switch (idAction)
				{
				case 2001:
					if (this.isCheck)
					{
						this.isCheck = false;
						return;
					}
					this.isCheck = true;
					return;
				case 2002:
					this.doRegister();
					return;
				case 2003:
					this.doMenu();
					return;
				case 2004:
					this.actRegister();
					return;
				case 2005:
				case 2006:
				case 2007:
					break;
				case 2008:
					Rms.saveRMSString("acc", this.tfUser.getText().Trim());
					Rms.saveRMSString("pass", this.tfPass.getText().Trim());
					if (ServerListScreen.loadScreen)
					{
						GameCanvas.serverScreen.switchToMe();
						return;
					}
					GameCanvas.serverScreen.show2();
					return;
				default:
					return;
				}
			}
			else
			{
				switch (mSystem.clientType)
				{
				case 1:
					mSystem.callHotlineJava();
					return;
				case 2:
					break;
				case 3:
				case 5:
					mSystem.callHotlineIphone();
					return;
				case 4:
					mSystem.callHotlinePC();
					return;
				case 6:
					mSystem.callHotlineWindowsPhone();
					return;
				default:
					return;
				}
			}
		}
		else if (idAction <= 10021)
		{
			if (idAction == 4000)
			{
				this.doRegister(this.tfUser.getText());
				return;
			}
			if (idAction == 10021)
			{
				this.actRegisterLeft();
				return;
			}
		}
		else if (idAction != 10041)
		{
			if (idAction == 10042)
			{
				Rms.saveRMSInt("lowGraphic", 1);
				GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
				return;
			}
		}
		else
		{
			Rms.saveRMSInt("lowGraphic", 0);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00057924 File Offset: 0x00055B24
	public void actRegisterLeft()
	{
		bool flag = this.isLogin2;
		if (flag)
		{
			this.doLogin();
		}
		else
		{
			this.isRes = false;
			this.tfPass.isFocus = false;
			this.tfUser.isFocus = true;
			this.left = this.cmdMenu;
		}
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x00004BBC File Offset: 0x00002DBC
	public void actRegister()
	{
		GameCanvas.endDlg();
		this.isRes = true;
		this.tfPass.isFocus = false;
		this.tfUser.isFocus = true;
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x00057974 File Offset: 0x00055B74
	public void backToRegister()
	{
		bool flag = GameCanvas.loginScr.isLogin2;
		if (flag)
		{
			GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, GameCanvas.panel, 10019, null), new Command(mResources.NO, GameCanvas.panel, 10020, null));
		}
		else
		{
			bool isWindowsPhone = Main.isWindowsPhone;
			if (isWindowsPhone)
			{
				GameMidlet.isBackWindowsPhone = true;
			}
			GameCanvas.instance.resetToLoginScr = false;
			GameCanvas.instance.doResetToLoginScr(GameCanvas.loginScr);
			Session_ME.gI().close();
		}
	}

	// Token: 0x04000979 RID: 2425
	public TField tfUser;

	// Token: 0x0400097A RID: 2426
	public TField tfPass;

	// Token: 0x0400097B RID: 2427
	public static bool isContinueToLogin = false;

	// Token: 0x0400097C RID: 2428
	private int focus;

	// Token: 0x0400097D RID: 2429
	private int wC;

	// Token: 0x0400097E RID: 2430
	private int yL;

	// Token: 0x0400097F RID: 2431
	private int defYL;

	// Token: 0x04000980 RID: 2432
	public bool isCheck;

	// Token: 0x04000981 RID: 2433
	public bool isRes;

	// Token: 0x04000982 RID: 2434
	public Command cmdLogin;

	// Token: 0x04000983 RID: 2435
	public Command cmdCheck;

	// Token: 0x04000984 RID: 2436
	public Command cmdFogetPass;

	// Token: 0x04000985 RID: 2437
	public Command cmdRes;

	// Token: 0x04000986 RID: 2438
	public Command cmdMenu;

	// Token: 0x04000987 RID: 2439
	public Command cmdBackFromRegister;

	// Token: 0x04000988 RID: 2440
	public string listFAQ = string.Empty;

	// Token: 0x04000989 RID: 2441
	public string titleFAQ;

	// Token: 0x0400098A RID: 2442
	public string subtitleFAQ;

	// Token: 0x0400098B RID: 2443
	private string numSupport = string.Empty;

	// Token: 0x0400098C RID: 2444
	public static bool isLocal = false;

	// Token: 0x0400098D RID: 2445
	public static bool isUpdateAll;

	// Token: 0x0400098E RID: 2446
	public static bool isUpdateData;

	// Token: 0x0400098F RID: 2447
	public static bool isUpdateMap;

	// Token: 0x04000990 RID: 2448
	public static bool isUpdateSkill;

	// Token: 0x04000991 RID: 2449
	public static bool isUpdateItem;

	// Token: 0x04000992 RID: 2450
	public static string serverName;

	// Token: 0x04000993 RID: 2451
	public static Image imgTitle;

	// Token: 0x04000994 RID: 2452
	public int plX;

	// Token: 0x04000995 RID: 2453
	public int plY;

	// Token: 0x04000996 RID: 2454
	public int lY;

	// Token: 0x04000997 RID: 2455
	public int lX;

	// Token: 0x04000998 RID: 2456
	public int logoDes;

	// Token: 0x04000999 RID: 2457
	public int lineX;

	// Token: 0x0400099A RID: 2458
	public int lineY;

	// Token: 0x0400099B RID: 2459
	public static int[] bgId = new int[]
	{
		0,
		8,
		2,
		6,
		9
	};

	// Token: 0x0400099C RID: 2460
	public static bool isTryGetIPFromWap;

	// Token: 0x0400099D RID: 2461
	public static short timeLogin;

	// Token: 0x0400099E RID: 2462
	public static long lastTimeLogin;

	// Token: 0x0400099F RID: 2463
	public static long currTimeLogin;

	// Token: 0x040009A0 RID: 2464
	private int yt;

	// Token: 0x040009A1 RID: 2465
	private Command cmdSelect;

	// Token: 0x040009A2 RID: 2466
	private Command cmdOK;

	// Token: 0x040009A3 RID: 2467
	private int xLog;

	// Token: 0x040009A4 RID: 2468
	private int yLog;

	// Token: 0x040009A5 RID: 2469
	public static GameMidlet m;

	// Token: 0x040009A6 RID: 2470
	private int yy = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;

	// Token: 0x040009A7 RID: 2471
	private int freeAreaHeight;

	// Token: 0x040009A8 RID: 2472
	private int xP;

	// Token: 0x040009A9 RID: 2473
	private int yP;

	// Token: 0x040009AA RID: 2474
	private int wP;

	// Token: 0x040009AB RID: 2475
	private int hP;

	// Token: 0x040009AC RID: 2476
	private int t = 20;

	// Token: 0x040009AD RID: 2477
	private bool isRegistering;

	// Token: 0x040009AE RID: 2478
	private string passRe = string.Empty;

	// Token: 0x040009AF RID: 2479
	public bool isFAQ;

	// Token: 0x040009B0 RID: 2480
	private int tipid = -1;

	// Token: 0x040009B1 RID: 2481
	public bool isLogin2;

	// Token: 0x040009B2 RID: 2482
	private int v = 2;

	// Token: 0x040009B3 RID: 2483
	private int g;

	// Token: 0x040009B4 RID: 2484
	private int ylogo = -40;

	// Token: 0x040009B5 RID: 2485
	private int dir = 1;

	// Token: 0x040009B6 RID: 2486
	private Command cmdCallHotline;

	// Token: 0x040009B7 RID: 2487
	public static bool isLoggingIn;
}
