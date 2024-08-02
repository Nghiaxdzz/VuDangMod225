using System;

namespace Assets.src.g
{
	// Token: 0x020000D8 RID: 216
	public class RegisterScreen : mScreen, IActionListener
	{
		// Token: 0x06000ADF RID: 2783 RVA: 0x000A8488 File Offset: 0x000A6688
		public RegisterScreen(sbyte haveName)
		{
			this.yLog = 130;
			TileMap.bgID = (int)((sbyte)(mSystem.currentTimeMillis() % 9L));
			bool flag = TileMap.bgID == 5 || TileMap.bgID == 6;
			if (flag)
			{
				TileMap.bgID = 4;
			}
			GameScr.loadCamera(true, -1, -1);
			GameScr.cmx = 100;
			GameScr.cmy = 200;
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
			this.tfSodt = new TField();
			this.tfSodt.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfSodt.width = 220;
			this.tfSodt.height = mScreen.ITEM_HEIGHT + 2;
			this.tfSodt.name = "Số điện thoại/ địa chỉ email";
			bool flag4 = haveName == 1;
			if (flag4)
			{
				this.tfSodt.setText("01234567890");
			}
			this.tfUser = new TField();
			this.tfUser.width = 220;
			this.tfUser.height = mScreen.ITEM_HEIGHT + 2;
			this.tfUser.isFocus = true;
			this.tfUser.name = "Họ và tên";
			bool flag5 = haveName == 1;
			if (flag5)
			{
				this.tfUser.setText("Nguyễn Văn A");
			}
			this.tfUser.setIputType(TField.INPUT_TYPE_ANY);
			this.tfNgay = new TField();
			this.tfNgay.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfNgay.width = 70;
			this.tfNgay.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNgay.name = "Ngày sinh";
			bool flag6 = haveName == 1;
			if (flag6)
			{
				this.tfNgay.setText("01");
			}
			this.tfThang = new TField();
			this.tfThang.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfThang.width = 70;
			this.tfThang.height = mScreen.ITEM_HEIGHT + 2;
			this.tfThang.name = "Tháng sinh";
			bool flag7 = haveName == 1;
			if (flag7)
			{
				this.tfThang.setText("01");
			}
			this.tfNam = new TField();
			this.tfNam.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfNam.width = 70;
			this.tfNam.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNam.name = "Năm sinh";
			bool flag8 = haveName == 1;
			if (flag8)
			{
				this.tfNam.setText("1990");
			}
			this.tfDiachi = new TField();
			this.tfDiachi.setIputType(TField.INPUT_TYPE_ANY);
			this.tfDiachi.width = 220;
			this.tfDiachi.height = mScreen.ITEM_HEIGHT + 2;
			this.tfDiachi.name = "Địa chỉ đăng ký thường trú";
			bool flag9 = haveName == 1;
			if (flag9)
			{
				this.tfDiachi.setText("123 đường số 1, Quận 1, TP.HCM");
			}
			this.tfCMND = new TField();
			this.tfCMND.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfCMND.width = 220;
			this.tfCMND.height = mScreen.ITEM_HEIGHT + 2;
			this.tfCMND.name = "Số Chứng minh nhân dân hoặc số hộ chiếu";
			bool flag10 = haveName == 1;
			if (flag10)
			{
				this.tfCMND.setText("123456789");
			}
			this.tfNgayCap = new TField();
			this.tfNgayCap.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.tfNgayCap.width = 220;
			this.tfNgayCap.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNgayCap.name = "Ngày cấp";
			bool flag11 = haveName == 1;
			if (flag11)
			{
				this.tfNgayCap.setText("01/01/2005");
			}
			this.tfNoiCap = new TField();
			this.tfNoiCap.setIputType(TField.INPUT_TYPE_ANY);
			this.tfNoiCap.width = 220;
			this.tfNoiCap.height = mScreen.ITEM_HEIGHT + 2;
			this.tfNoiCap.name = "Nơi cấp";
			bool flag12 = haveName == 1;
			if (flag12)
			{
				this.tfNoiCap.setText("TP.HCM");
			}
			this.yt += 35;
			this.isCheck = true;
			this.focus = 0;
			this.cmdLogin = new Command((GameCanvas.w <= 200) ? mResources.login2 : mResources.login, GameCanvas.instance, 888393, null);
			this.cmdCheck = new Command(mResources.remember, this, 2001, null);
			this.cmdRes = new Command(mResources.register, this, 2002, null);
			this.cmdBackFromRegister = new Command(mResources.CANCEL, this, 10021, null);
			this.left = (this.cmdMenu = new Command(mResources.MENU, this, 2003, null));
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				this.cmdLogin.x = GameCanvas.w / 2 - 100;
				this.cmdMenu.x = GameCanvas.w / 2 - mScreen.cmdW - 8;
				bool flag13 = GameCanvas.h >= 200;
				if (flag13)
				{
					this.cmdLogin.y = GameCanvas.h / 2 - 40;
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
			int num2 = 4;
			int num3 = num2 * 32 + 23 + 33;
			bool flag14 = num3 >= GameCanvas.w;
			if (flag14)
			{
				num2--;
				num3 = num2 * 32 + 23 + 33;
			}
			this.xLog = GameCanvas.w / 2 - num3 / 2;
			this.yLog = 5;
			this.lY = ((GameCanvas.w < 200) ? (this.tfUser.y - 30) : (this.yLog - 30));
			this.tfUser.x = this.xLog + 10;
			this.tfUser.y = this.yLog + 20;
			this.cmdOK = new Command(mResources.OK, this, 2008, null);
			this.cmdOK.x = 260;
			this.cmdOK.y = GameCanvas.h - 60;
			this.cmdFogetPass = new Command("Thoát", this, 1003, null);
			this.cmdFogetPass.x = 260;
			this.cmdFogetPass.y = GameCanvas.h - 30;
			bool flag15 = GameCanvas.w < 250;
			if (flag15)
			{
				this.cmdOK.x = GameCanvas.w / 2 - 80;
				this.cmdFogetPass.x = GameCanvas.w / 2 + 10;
				this.cmdFogetPass.y = (this.cmdOK.y = GameCanvas.h - 25);
			}
			this.center = this.cmdOK;
			this.left = this.cmdFogetPass;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x000A8CD8 File Offset: 0x000A6ED8
		public new void switchToMe()
		{
			Res.outz("Res switch");
			SoundMn.gI().stopAll();
			this.focus = 0;
			this.tfUser.isFocus = true;
			this.tfNgay.isFocus = false;
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				this.tfUser.isFocus = false;
				this.focus = -1;
			}
			base.switchToMe();
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x000A8D40 File Offset: 0x000A6F40
		protected void doMenu()
		{
			MyVector myVector = new MyVector("vMenu Login");
			myVector.addElement(new Command(mResources.registerNewAcc, this, 2004, null));
			bool flag = !this.isLogin2;
			if (flag)
			{
				myVector.addElement(new Command(mResources.selectServer, this, 1004, null));
			}
			myVector.addElement(new Command(mResources.forgetPass, this, 1003, null));
			myVector.addElement(new Command(mResources.website, this, 1005, null));
			int num = Rms.loadRMSInt("lowGraphic");
			bool flag2 = num == 1;
			if (flag2)
			{
				myVector.addElement(new Command(mResources.increase_vga, this, 10041, null));
			}
			else
			{
				myVector.addElement(new Command(mResources.decrease_vga, this, 10042, null));
			}
			myVector.addElement(new Command(mResources.EXIT, GameCanvas.instance, 8885, null));
			GameCanvas.menu.startAt(myVector, 0);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x000A8E3C File Offset: 0x000A703C
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
				bool flag2 = this.tfNgay.getText().Equals(string.Empty);
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
								this.tfNgay.getText()
							}), new Command(mResources.ACCEPT, this, 4000, null), null, new Command(mResources.NO, GameCanvas.instance, 8882, null));
						}
						GameCanvas.currentDialog = GameCanvas.msgdlg;
					}
				}
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00003A0D File Offset: 0x00001C0D
		protected void doRegister(string user)
		{
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x000A90D8 File Offset: 0x000A72D8
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

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000A9138 File Offset: 0x000A7338
		protected void doSelectServer()
		{
			MyVector myVector = new MyVector("vServer");
			bool flag = RegisterScreen.isLocal;
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

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00004B98 File Offset: 0x00002D98
		protected void saveIndexServer(int index)
		{
			Rms.saveRMSInt("indServer", index);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00056668 File Offset: 0x00054868
		protected int loadIndexServer()
		{
			return Rms.loadRMSInt("indServer");
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00003A0D File Offset: 0x00001C0D
		public void doLogin()
		{
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00003A0D File Offset: 0x00001C0D
		public void savePass()
		{
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000A91F4 File Offset: 0x000A73F4
		public override void update()
		{
			this.tfUser.update();
			this.tfNgay.update();
			this.tfThang.update();
			this.tfNam.update();
			this.tfDiachi.update();
			this.tfCMND.update();
			this.tfNoiCap.update();
			this.tfSodt.update();
			this.tfNgayCap.update();
			for (int i = 0; i < Effect2.vEffect2.size(); i++)
			{
				Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
				effect.update();
			}
			bool flag = RegisterScreen.isUpdateAll && !RegisterScreen.isUpdateData && !RegisterScreen.isUpdateItem && !RegisterScreen.isUpdateMap && !RegisterScreen.isUpdateSkill;
			if (flag)
			{
				RegisterScreen.isUpdateAll = false;
				mSystem.gcc();
				Service.gI().finishUpdate();
			}
			GameScr.cmx++;
			bool flag2 = GameScr.cmx > GameCanvas.w * 3 + 100;
			if (flag2)
			{
				GameScr.cmx = 100;
			}
			bool flag3 = ChatPopup.currChatPopup != null;
			if (!flag3)
			{
				GameCanvas.debug("LGU1", 0);
				GameCanvas.debug("LGU2", 0);
				GameCanvas.debug("LGU3", 0);
				this.updateLogo();
				GameCanvas.debug("LGU4", 0);
				GameCanvas.debug("LGU5", 0);
				bool flag4 = this.g >= 0;
				if (flag4)
				{
					this.ylogo += this.dir * this.g;
					this.g += this.dir * this.v;
					bool flag5 = this.g <= 0;
					if (flag5)
					{
						this.dir *= -1;
					}
					bool flag6 = this.ylogo > 0;
					if (flag6)
					{
						this.dir *= -1;
						this.g -= 2 * this.v;
					}
				}
				GameCanvas.debug("LGU6", 0);
				bool flag7 = this.tipid >= 0 && GameCanvas.gameTick % 100 == 0;
				if (flag7)
				{
					this.doChangeTip();
				}
				bool isTouch = GameCanvas.isTouch;
				if (isTouch)
				{
					bool flag8 = this.isRes;
					if (flag8)
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
					bool flag9 = this.isRes;
					if (flag9)
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
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000A94D0 File Offset: 0x000A76D0
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

		// Token: 0x06000AEC RID: 2796 RVA: 0x000A9544 File Offset: 0x000A7744
		public void updateLogo()
		{
			bool flag = this.defYL != this.yL;
			if (flag)
			{
				this.yL += this.defYL - this.yL >> 1;
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000A9588 File Offset: 0x000A7788
		public override void keyPress(int keyCode)
		{
			bool isFocus = this.tfUser.isFocus;
			if (isFocus)
			{
				this.tfUser.keyPressed(keyCode);
			}
			else
			{
				bool isFocus2 = this.tfNgay.isFocus;
				if (isFocus2)
				{
					this.tfNgay.keyPressed(keyCode);
				}
				else
				{
					bool isFocus3 = this.tfThang.isFocus;
					if (isFocus3)
					{
						this.tfThang.keyPressed(keyCode);
					}
					else
					{
						bool isFocus4 = this.tfNam.isFocus;
						if (isFocus4)
						{
							this.tfNam.keyPressed(keyCode);
						}
						else
						{
							bool isFocus5 = this.tfDiachi.isFocus;
							if (isFocus5)
							{
								this.tfDiachi.keyPressed(keyCode);
							}
							else
							{
								bool isFocus6 = this.tfCMND.isFocus;
								if (isFocus6)
								{
									this.tfCMND.keyPressed(keyCode);
								}
								else
								{
									bool isFocus7 = this.tfNoiCap.isFocus;
									if (isFocus7)
									{
										this.tfNoiCap.keyPressed(keyCode);
									}
									else
									{
										bool isFocus8 = this.tfSodt.isFocus;
										if (isFocus8)
										{
											this.tfSodt.keyPressed(keyCode);
										}
										else
										{
											bool isFocus9 = this.tfNgayCap.isFocus;
											if (isFocus9)
											{
												this.tfNgayCap.keyPressed(keyCode);
											}
										}
									}
								}
							}
						}
					}
				}
			}
			base.keyPress(keyCode);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00004BA7 File Offset: 0x00002DA7
		public override void unLoad()
		{
			base.unLoad();
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000A96D8 File Offset: 0x000A78D8
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
			bool flag2 = ChatPopup.currChatPopup != null || ChatPopup.serverChatPopUp != null;
			if (!flag2)
			{
				bool flag3 = GameCanvas.currentDialog == null;
				if (flag3)
				{
					this.xLog = 5;
					int num2 = 233;
					bool flag4 = GameCanvas.w < 260;
					if (flag4)
					{
						this.xLog = (GameCanvas.w - 240) / 2;
					}
					this.yLog = (GameCanvas.h - num2) / 2;
					int num3 = (GameCanvas.w < 200) ? 160 : 180;
					PopUp.paintPopUp(g, this.xLog, this.yLog, 240, num2, -1, true);
					bool flag5 = GameCanvas.h > 160 && RegisterScreen.imgTitle != null;
					if (flag5)
					{
						g.drawImage(RegisterScreen.imgTitle, GameCanvas.hw, num, 3);
					}
					GameCanvas.debug("PLG4", 1);
					int num4 = 4;
					int num5 = num4 * 32 + 23 + 33;
					bool flag6 = num5 >= GameCanvas.w;
					if (flag6)
					{
						num4--;
						num5 = num4 * 32 + 23 + 33;
					}
					this.tfSodt.x = this.xLog + 10;
					this.tfSodt.y = this.yLog + 15;
					this.tfUser.x = this.tfSodt.x;
					this.tfUser.y = this.tfSodt.y + 30;
					this.tfNgay.x = this.xLog + 10;
					this.tfNgay.y = this.tfUser.y + 30;
					this.tfThang.x = this.tfNgay.x + 75;
					this.tfThang.y = this.tfNgay.y;
					this.tfNam.x = this.tfThang.x + 75;
					this.tfNam.y = this.tfThang.y;
					this.tfDiachi.x = this.tfUser.x;
					this.tfDiachi.y = this.tfNgay.y + 30;
					this.tfCMND.x = this.tfUser.x;
					this.tfCMND.y = this.tfDiachi.y + 30;
					this.tfNgayCap.x = this.tfUser.x;
					this.tfNgayCap.y = this.tfCMND.y + 30;
					this.tfNoiCap.x = this.tfUser.x;
					this.tfNoiCap.y = this.tfNgayCap.y + 30;
					this.tfUser.paint(g);
					this.tfNgay.paint(g);
					this.tfThang.paint(g);
					this.tfNam.paint(g);
					this.tfDiachi.paint(g);
					this.tfCMND.paint(g);
					this.tfNgayCap.paint(g);
					this.tfNoiCap.paint(g);
					this.tfSodt.paint(g);
					bool flag7 = GameCanvas.w >= 176;
					if (!flag7)
					{
						mFont.tahoma_7b_green2.drawString(g, mResources.acc + ":", this.tfUser.x - 35, this.tfUser.y + 7, 0);
						mFont.tahoma_7b_green2.drawString(g, mResources.pwd + ":", this.tfNgay.x - 35, this.tfNgay.y + 7, 0);
						mFont.tahoma_7b_green2.drawString(g, mResources.server + ": " + RegisterScreen.serverName, GameCanvas.w / 2, this.tfNgay.y + 32, 2);
						bool flag8 = this.isRes;
						if (flag8)
						{
						}
					}
				}
				string version = GameMidlet.VERSION;
				g.setColor(GameCanvas.skyColor);
				g.fillRect(GameCanvas.w - 40, 4, 36, 11);
				mFont.tahoma_7_grey.drawString(g, version, GameCanvas.w - 22, 4, mFont.CENTER);
				GameCanvas.resetTrans(g);
				bool flag9 = GameCanvas.currentDialog == null;
				if (flag9)
				{
					bool flag10 = GameCanvas.w > 250;
					if (flag10)
					{
						mFont.tahoma_7b_white.drawString(g, "Dưới 18 tuổi", 260, 10, 0, mFont.tahoma_7b_dark);
						mFont.tahoma_7b_white.drawString(g, "chỉ có thể chơi", 260, 25, 0, mFont.tahoma_7b_dark);
						mFont.tahoma_7b_white.drawString(g, "180p 1 ngày", 260, 40, 0, mFont.tahoma_7b_dark);
					}
					else
					{
						mFont.tahoma_7b_white.drawString(g, "Dưới 18 tuổi chỉ có thể chơi", GameCanvas.w / 2, 5, 2, mFont.tahoma_7b_dark);
						mFont.tahoma_7b_white.drawString(g, "180p 1 ngày", GameCanvas.w / 2, 15, 2, mFont.tahoma_7b_dark);
					}
				}
				base.paint(g);
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000A9C50 File Offset: 0x000A7E50
		private void turnOffFocus()
		{
			this.tfUser.isFocus = false;
			this.tfNgay.isFocus = false;
			this.tfThang.isFocus = false;
			this.tfNam.isFocus = false;
			this.tfDiachi.isFocus = false;
			this.tfCMND.isFocus = false;
			this.tfNgayCap.isFocus = false;
			this.tfNoiCap.isFocus = false;
			this.tfSodt.isFocus = false;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x000A9CCC File Offset: 0x000A7ECC
		private void processFocus()
		{
			this.turnOffFocus();
			switch (this.focus)
			{
			case 0:
				this.tfUser.isFocus = true;
				break;
			case 1:
				this.tfNgay.isFocus = true;
				break;
			case 2:
				this.tfThang.isFocus = true;
				break;
			case 3:
				this.tfNam.isFocus = true;
				break;
			case 4:
				this.tfDiachi.isFocus = true;
				break;
			case 5:
				this.tfCMND.isFocus = true;
				break;
			case 6:
				this.tfNgayCap.isFocus = true;
				break;
			case 7:
				this.tfNoiCap.isFocus = true;
				break;
			case 8:
				this.tfSodt.isFocus = true;
				break;
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000A9D94 File Offset: 0x000A7F94
		public override void updateKey()
		{
			bool flag = RegisterScreen.isContinueToLogin;
			if (!flag)
			{
				bool flag2 = !GameCanvas.isTouch;
				if (flag2)
				{
					bool isFocus = this.tfUser.isFocus;
					if (isFocus)
					{
						this.right = this.tfUser.cmdClear;
					}
					else
					{
						bool isFocus2 = this.tfNgay.isFocus;
						if (isFocus2)
						{
							this.right = this.tfNgay.cmdClear;
						}
						else
						{
							bool isFocus3 = this.tfThang.isFocus;
							if (isFocus3)
							{
								this.right = this.tfThang.cmdClear;
							}
							else
							{
								bool isFocus4 = this.tfNam.isFocus;
								if (isFocus4)
								{
									this.right = this.tfNam.cmdClear;
								}
								else
								{
									bool isFocus5 = this.tfDiachi.isFocus;
									if (isFocus5)
									{
										this.right = this.tfDiachi.cmdClear;
									}
									else
									{
										bool isFocus6 = this.tfCMND.isFocus;
										if (isFocus6)
										{
											this.right = this.tfCMND.cmdClear;
										}
										else
										{
											bool isFocus7 = this.tfNgayCap.isFocus;
											if (isFocus7)
											{
												this.right = this.tfNgayCap.cmdClear;
											}
											else
											{
												bool isFocus8 = this.tfNoiCap.isFocus;
												if (isFocus8)
												{
													this.right = this.tfNoiCap.cmdClear;
												}
												else
												{
													bool isFocus9 = this.tfSodt.isFocus;
													if (isFocus9)
													{
														this.right = this.tfSodt.cmdClear;
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
				bool flag3 = GameCanvas.keyPressed[21];
				if (flag3)
				{
					this.focus--;
					bool flag4 = this.focus < 0;
					if (flag4)
					{
						this.focus = 8;
					}
					this.processFocus();
				}
				else
				{
					bool flag5 = GameCanvas.keyPressed[22];
					if (flag5)
					{
						this.focus++;
						bool flag6 = this.focus > 8;
						if (flag6)
						{
							this.focus = 0;
						}
						this.processFocus();
					}
				}
				bool flag7 = GameCanvas.keyPressed[21] || GameCanvas.keyPressed[22];
				if (flag7)
				{
					GameCanvas.clearKeyPressed();
					bool flag8 = !this.isLogin2 || this.isRes;
					if (flag8)
					{
						bool flag9 = this.focus == 1;
						if (flag9)
						{
							this.tfUser.isFocus = false;
							this.tfNgay.isFocus = true;
						}
						else
						{
							bool flag10 = this.focus == 0;
							if (flag10)
							{
								this.tfUser.isFocus = true;
								this.tfNgay.isFocus = false;
							}
							else
							{
								this.tfUser.isFocus = false;
								this.tfNgay.isFocus = false;
							}
						}
					}
				}
				bool isTouch = GameCanvas.isTouch;
				if (isTouch)
				{
					bool flag11 = this.isRes;
					if (flag11)
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
					bool flag12 = this.isRes;
					if (flag12)
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
				bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
				if (isPointerJustRelease)
				{
					bool flag13 = GameCanvas.isPointerHoldIn(this.tfUser.x, this.tfUser.y, this.tfUser.width, this.tfUser.height);
					if (flag13)
					{
						this.focus = 0;
						this.processFocus();
					}
					else
					{
						bool flag14 = GameCanvas.isPointerHoldIn(this.tfNgay.x, this.tfNgay.y, this.tfNgay.width, this.tfNgay.height);
						if (flag14)
						{
							this.focus = 1;
							this.processFocus();
						}
						else
						{
							bool flag15 = GameCanvas.isPointerHoldIn(this.tfThang.x, this.tfThang.y, this.tfThang.width, this.tfThang.height);
							if (flag15)
							{
								this.focus = 2;
								this.processFocus();
							}
							else
							{
								bool flag16 = GameCanvas.isPointerHoldIn(this.tfNam.x, this.tfNam.y, this.tfNam.width, this.tfNam.height);
								if (flag16)
								{
									this.focus = 3;
									this.processFocus();
								}
								else
								{
									bool flag17 = GameCanvas.isPointerHoldIn(this.tfDiachi.x, this.tfDiachi.y, this.tfDiachi.width, this.tfDiachi.height);
									if (flag17)
									{
										this.focus = 4;
										this.processFocus();
									}
									else
									{
										bool flag18 = GameCanvas.isPointerHoldIn(this.tfCMND.x, this.tfCMND.y, this.tfCMND.width, this.tfCMND.height);
										if (flag18)
										{
											this.focus = 5;
											this.processFocus();
										}
										else
										{
											bool flag19 = GameCanvas.isPointerHoldIn(this.tfNgayCap.x, this.tfNgayCap.y, this.tfNgayCap.width, this.tfNgayCap.height);
											if (flag19)
											{
												this.focus = 6;
												this.processFocus();
											}
											else
											{
												bool flag20 = GameCanvas.isPointerHoldIn(this.tfNoiCap.x, this.tfNoiCap.y, this.tfNoiCap.width, this.tfNoiCap.height);
												if (flag20)
												{
													this.focus = 7;
													this.processFocus();
												}
												else
												{
													bool flag21 = GameCanvas.isPointerHoldIn(this.tfSodt.x, this.tfSodt.y, this.tfSodt.width, this.tfSodt.height);
													if (flag21)
													{
														this.focus = 8;
														this.processFocus();
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
				base.updateKey();
				GameCanvas.clearKeyPressed();
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x000069C8 File Offset: 0x00004BC8
		public void resetLogo()
		{
			this.yL = -50;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000AA3A0 File Offset: 0x000A85A0
		public void perform(int idAction, object p)
		{
			if (idAction <= 2008)
			{
				switch (idAction)
				{
				case 1000:
					try
					{
						GameMidlet.instance.platformRequest((string)p);
					}
					catch (Exception ex)
					{
						ex.StackTrace.ToString();
					}
					GameCanvas.endDlg();
					break;
				case 1001:
					GameCanvas.endDlg();
					this.isRes = false;
					break;
				case 1002:
					break;
				case 1003:
					Session_ME.gI().close();
					GameCanvas.serverScreen.switchToMe();
					break;
				case 1004:
					ServerListScreen.doUpdateServer();
					GameCanvas.serverScreen.switchToMe();
					break;
				case 1005:
					try
					{
						GameMidlet.instance.platformRequest("http://ngocrongonline.com");
					}
					catch (Exception ex2)
					{
						ex2.StackTrace.ToString();
					}
					break;
				default:
					switch (idAction)
					{
					case 2001:
					{
						bool flag = this.isCheck;
						if (flag)
						{
							this.isCheck = false;
						}
						else
						{
							this.isCheck = true;
						}
						break;
					}
					case 2002:
						this.doRegister();
						break;
					case 2003:
						this.doMenu();
						break;
					case 2004:
						this.actRegister();
						break;
					case 2008:
					{
						bool flag2 = this.tfNgay.getText().Equals(string.Empty) || this.tfThang.getText().Equals(string.Empty) || this.tfNam.getText().Equals(string.Empty) || this.tfDiachi.getText().Equals(string.Empty) || this.tfCMND.getText().Equals(string.Empty) || this.tfNgayCap.getText().Equals(string.Empty) || this.tfNoiCap.getText().Equals(string.Empty) || this.tfSodt.getText().Equals(string.Empty) || this.tfUser.getText().Equals(string.Empty);
						if (flag2)
						{
							GameCanvas.startOKDlg("Vui lòng điền đầy đủ thông tin");
						}
						else
						{
							GameCanvas.startOKDlg(mResources.PLEASEWAIT);
							Service.gI().charInfo(this.tfNgay.getText(), this.tfThang.getText(), this.tfNam.getText(), this.tfDiachi.getText(), this.tfCMND.getText(), this.tfNgayCap.getText(), this.tfNoiCap.getText(), this.tfSodt.getText(), this.tfUser.getText());
						}
						break;
					}
					}
					break;
				}
			}
			else if (idAction != 4000)
			{
				if (idAction == 10021)
				{
					this.actRegisterLeft();
				}
			}
			else
			{
				this.doRegister(this.tfUser.getText());
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000AA6B8 File Offset: 0x000A88B8
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
				this.tfNgay.isFocus = false;
				this.tfUser.isFocus = true;
				this.left = this.cmdMenu;
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000069D3 File Offset: 0x00004BD3
		public void actRegister()
		{
			GameCanvas.endDlg();
			GameCanvas.startOKDlg(mResources.regNote);
			this.isRes = true;
			this.tfNgay.isFocus = false;
			this.tfUser.isFocus = true;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000AA708 File Offset: 0x000A8908
		public void backToRegister()
		{
			bool flag = GameCanvas.loginScr.isLogin2;
			if (flag)
			{
				GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, GameCanvas.panel, 10019, null), new Command(mResources.NO, GameCanvas.panel, 10020, null));
			}
			else
			{
				GameCanvas.instance.doResetToLoginScr(GameCanvas.loginScr);
				Session_ME.gI().close();
			}
		}

		// Token: 0x040013D7 RID: 5079
		public TField tfUser;

		// Token: 0x040013D8 RID: 5080
		public TField tfNgay;

		// Token: 0x040013D9 RID: 5081
		public TField tfThang;

		// Token: 0x040013DA RID: 5082
		public TField tfNam;

		// Token: 0x040013DB RID: 5083
		public TField tfDiachi;

		// Token: 0x040013DC RID: 5084
		public TField tfCMND;

		// Token: 0x040013DD RID: 5085
		public TField tfNgayCap;

		// Token: 0x040013DE RID: 5086
		public TField tfNoiCap;

		// Token: 0x040013DF RID: 5087
		public TField tfSodt;

		// Token: 0x040013E0 RID: 5088
		public static bool isContinueToLogin = false;

		// Token: 0x040013E1 RID: 5089
		private int focus;

		// Token: 0x040013E2 RID: 5090
		private int wC;

		// Token: 0x040013E3 RID: 5091
		private int yL;

		// Token: 0x040013E4 RID: 5092
		private int defYL;

		// Token: 0x040013E5 RID: 5093
		public bool isCheck;

		// Token: 0x040013E6 RID: 5094
		public bool isRes;

		// Token: 0x040013E7 RID: 5095
		private Command cmdLogin;

		// Token: 0x040013E8 RID: 5096
		private Command cmdCheck;

		// Token: 0x040013E9 RID: 5097
		private Command cmdFogetPass;

		// Token: 0x040013EA RID: 5098
		private Command cmdRes;

		// Token: 0x040013EB RID: 5099
		private Command cmdMenu;

		// Token: 0x040013EC RID: 5100
		private Command cmdBackFromRegister;

		// Token: 0x040013ED RID: 5101
		public string listFAQ = string.Empty;

		// Token: 0x040013EE RID: 5102
		public string titleFAQ;

		// Token: 0x040013EF RID: 5103
		public string subtitleFAQ;

		// Token: 0x040013F0 RID: 5104
		private string numSupport = string.Empty;

		// Token: 0x040013F1 RID: 5105
		private string strUser;

		// Token: 0x040013F2 RID: 5106
		private string strPass;

		// Token: 0x040013F3 RID: 5107
		public static bool isLocal = false;

		// Token: 0x040013F4 RID: 5108
		public static bool isUpdateAll;

		// Token: 0x040013F5 RID: 5109
		public static bool isUpdateData;

		// Token: 0x040013F6 RID: 5110
		public static bool isUpdateMap;

		// Token: 0x040013F7 RID: 5111
		public static bool isUpdateSkill;

		// Token: 0x040013F8 RID: 5112
		public static bool isUpdateItem;

		// Token: 0x040013F9 RID: 5113
		public static string serverName;

		// Token: 0x040013FA RID: 5114
		public static Image imgTitle;

		// Token: 0x040013FB RID: 5115
		public int plX;

		// Token: 0x040013FC RID: 5116
		public int plY;

		// Token: 0x040013FD RID: 5117
		public int lY;

		// Token: 0x040013FE RID: 5118
		public int lX;

		// Token: 0x040013FF RID: 5119
		public int logoDes;

		// Token: 0x04001400 RID: 5120
		public int lineX;

		// Token: 0x04001401 RID: 5121
		public int lineY;

		// Token: 0x04001402 RID: 5122
		public static int[] bgId = new int[]
		{
			0,
			8,
			2,
			6,
			9
		};

		// Token: 0x04001403 RID: 5123
		public static bool isTryGetIPFromWap;

		// Token: 0x04001404 RID: 5124
		public static short timeLogin;

		// Token: 0x04001405 RID: 5125
		public static long lastTimeLogin;

		// Token: 0x04001406 RID: 5126
		public static long currTimeLogin;

		// Token: 0x04001407 RID: 5127
		private int yt;

		// Token: 0x04001408 RID: 5128
		private Command cmdSelect;

		// Token: 0x04001409 RID: 5129
		private Command cmdOK;

		// Token: 0x0400140A RID: 5130
		private int xLog;

		// Token: 0x0400140B RID: 5131
		private int yLog;

		// Token: 0x0400140C RID: 5132
		private int xP;

		// Token: 0x0400140D RID: 5133
		private int yP;

		// Token: 0x0400140E RID: 5134
		private int wP;

		// Token: 0x0400140F RID: 5135
		private int hP;

		// Token: 0x04001410 RID: 5136
		private string passRe = string.Empty;

		// Token: 0x04001411 RID: 5137
		public bool isFAQ;

		// Token: 0x04001412 RID: 5138
		private int tipid = -1;

		// Token: 0x04001413 RID: 5139
		public bool isLogin2;

		// Token: 0x04001414 RID: 5140
		private int v = 2;

		// Token: 0x04001415 RID: 5141
		private int g;

		// Token: 0x04001416 RID: 5142
		private int ylogo = -40;

		// Token: 0x04001417 RID: 5143
		private int dir = 1;

		// Token: 0x04001418 RID: 5144
		public static bool isLoggingIn;
	}
}
