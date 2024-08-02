using System;

// Token: 0x0200001C RID: 28
public class CreateCharScr : mScreen, IActionListener
{
	// Token: 0x060001B1 RID: 433 RVA: 0x000338DC File Offset: 0x00031ADC
	public CreateCharScr()
	{
		try
		{
			bool flag = !GameCanvas.lowGraphic;
			if (flag)
			{
				CreateCharScr.loadMapFromResource(new sbyte[]
				{
					39,
					40,
					41
				});
			}
			this.loadMapTableFromResource(new sbyte[]
			{
				39,
				40,
				41
			});
		}
		catch (Exception ex)
		{
			Cout.LogError("Tao char loi " + ex.ToString());
		}
		bool flag2 = GameCanvas.w <= 200;
		if (flag2)
		{
			GameScr.setPopupSize(128, 100);
			GameScr.popupX = (GameCanvas.w - 128) / 2;
			GameScr.popupY = 10;
			this.cy += 15;
			this.dy -= 15;
		}
		CreateCharScr.indexGender = 1;
		CreateCharScr.tAddName = new TField();
		CreateCharScr.tAddName.width = GameCanvas.loginScr.tfUser.width;
		bool flag3 = GameCanvas.w < 200;
		if (flag3)
		{
			CreateCharScr.tAddName.width = 60;
		}
		CreateCharScr.tAddName.height = mScreen.ITEM_HEIGHT + 2;
		bool flag4 = GameCanvas.w < 200;
		if (flag4)
		{
			CreateCharScr.tAddName.x = GameScr.popupX + 45;
			CreateCharScr.tAddName.y = GameScr.popupY + 12;
		}
		else
		{
			CreateCharScr.tAddName.x = GameCanvas.w / 2 - CreateCharScr.tAddName.width / 2;
			CreateCharScr.tAddName.y = 35;
		}
		bool flag5 = !GameCanvas.isTouch;
		if (flag5)
		{
			CreateCharScr.tAddName.isFocus = true;
		}
		CreateCharScr.tAddName.setIputType(TField.INPUT_TYPE_ANY);
		CreateCharScr.tAddName.showSubTextField = false;
		CreateCharScr.tAddName.strInfo = mResources.char_name;
		bool flag6 = CreateCharScr.tAddName.getText().Equals("@");
		if (flag6)
		{
			CreateCharScr.tAddName.setText(GameCanvas.loginScr.tfUser.getText().Substring(0, GameCanvas.loginScr.tfUser.getText().IndexOf("@")));
		}
		CreateCharScr.tAddName.name = mResources.char_name;
		CreateCharScr.indexGender = 1;
		CreateCharScr.indexHair = 0;
		this.center = new Command(mResources.NEWCHAR, this, 8000, null);
		this.left = new Command(mResources.BACK, this, 8001, null);
		bool flag7 = !GameCanvas.isTouch;
		if (flag7)
		{
			this.right = CreateCharScr.tAddName.cmdClear;
		}
		this.yBegin = CreateCharScr.tAddName.y;
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x00033BBC File Offset: 0x00031DBC
	public static CreateCharScr gI()
	{
		bool flag = CreateCharScr.instance == null;
		if (flag)
		{
			CreateCharScr.instance = new CreateCharScr();
		}
		return CreateCharScr.instance;
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void init()
	{
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x00033BEC File Offset: 0x00031DEC
	public static void loadMapFromResource(sbyte[] mapID)
	{
		Res.outz("newwwwwwwwww =============");
		for (int i = 0; i < mapID.Length; i++)
		{
			DataInputStream dataInputStream = MyStream.readFile("/mymap/" + mapID[i]);
			MapTemplate.tmw[i] = (int)((ushort)dataInputStream.read());
			MapTemplate.tmh[i] = (int)((ushort)dataInputStream.read());
			Cout.LogError(string.Concat(new object[]
			{
				"Thong TIn : ",
				MapTemplate.tmw[i],
				"::",
				MapTemplate.tmh[i]
			}));
			MapTemplate.maps[i] = new int[dataInputStream.available()];
			Cout.LogError("lent= " + MapTemplate.maps[i].Length);
			for (int j = 0; j < MapTemplate.tmw[i] * MapTemplate.tmh[i]; j++)
			{
				MapTemplate.maps[i][j] = dataInputStream.read();
			}
			MapTemplate.types[i] = new int[MapTemplate.maps[i].Length];
		}
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00033D0C File Offset: 0x00031F0C
	public void loadMapTableFromResource(sbyte[] mapID)
	{
		bool lowGraphic = GameCanvas.lowGraphic;
		if (!lowGraphic)
		{
			DataInputStream dataInputStream = null;
			try
			{
				for (int i = 0; i < mapID.Length; i++)
				{
					dataInputStream = MyStream.readFile("/mymap/mapTable" + mapID[i]);
					Cout.LogError("mapTable : " + mapID[i]);
					short num = dataInputStream.readShort();
					MapTemplate.vCurrItem[i] = new MyVector();
					Res.outz("nItem= " + num);
					for (int j = 0; j < (int)num; j++)
					{
						short id = dataInputStream.readShort();
						short num2 = dataInputStream.readShort();
						short num3 = dataInputStream.readShort();
						bool flag = TileMap.getBIById((int)id) != null;
						if (flag)
						{
							BgItem bibyId = TileMap.getBIById((int)id);
							BgItem bgItem = new BgItem();
							bgItem.id = (int)id;
							bgItem.idImage = bibyId.idImage;
							bgItem.dx = bibyId.dx;
							bgItem.dy = bibyId.dy;
							bgItem.x = (int)(num2 * (short)TileMap.size);
							bgItem.y = (int)(num3 * (short)TileMap.size);
							bgItem.layer = bibyId.layer;
							MapTemplate.vCurrItem[i].addElement(bgItem);
							bool flag2 = !BgItem.imgNew.containsKey(bgItem.idImage + string.Empty);
							if (flag2)
							{
								try
								{
									Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
									bool flag3 = image == null;
									if (flag3)
									{
										BgItem.imgNew.put(bgItem.idImage + string.Empty, Image.createRGBImage(new int[1], 1, 1, true));
										Service.gI().getBgTemplate(bgItem.idImage);
									}
									else
									{
										BgItem.imgNew.put(bgItem.idImage + string.Empty, image);
									}
								}
								catch (Exception)
								{
									Image image2 = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
									bool flag4 = image2 == null;
									if (flag4)
									{
										image2 = Image.createRGBImage(new int[1], 1, 1, true);
										Service.gI().getBgTemplate(bgItem.idImage);
									}
									BgItem.imgNew.put(bgItem.idImage + string.Empty, image2);
								}
								BgItem.vKeysLast.addElement(bgItem.idImage + string.Empty);
							}
							bool flag5 = !BgItem.isExistKeyNews(bgItem.idImage + string.Empty);
							if (flag5)
							{
								BgItem.vKeysNew.addElement(bgItem.idImage + string.Empty);
							}
							bgItem.changeColor();
						}
						else
						{
							Res.outz("item null");
						}
					}
				}
			}
			catch (Exception ex)
			{
				Cout.println("LOI TAI loadMapTableFromResource" + ex.ToString());
			}
		}
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x00034080 File Offset: 0x00032280
	public override void switchToMe()
	{
		LoginScr.isContinueToLogin = false;
		GameCanvas.menu.showMenu = false;
		GameCanvas.endDlg();
		base.switchToMe();
		CreateCharScr.indexGender = Res.random(0, 3);
		CreateCharScr.indexHair = Res.random(0, 3);
		this.doChangeMap();
		global::Char.isLoadingMap = false;
		CreateCharScr.tAddName.setFocusWithKb(true);
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x000340E0 File Offset: 0x000322E0
	public void doChangeMap()
	{
		TileMap.maps = new int[MapTemplate.maps[CreateCharScr.indexGender].Length];
		for (int i = 0; i < MapTemplate.maps[CreateCharScr.indexGender].Length; i++)
		{
			TileMap.maps[i] = MapTemplate.maps[CreateCharScr.indexGender][i];
		}
		TileMap.types = MapTemplate.types[CreateCharScr.indexGender];
		TileMap.pxh = MapTemplate.pxh[CreateCharScr.indexGender];
		TileMap.pxw = MapTemplate.pxw[CreateCharScr.indexGender];
		TileMap.tileID = MapTemplate.pxw[CreateCharScr.indexGender];
		TileMap.tmw = MapTemplate.tmw[CreateCharScr.indexGender];
		TileMap.tmh = MapTemplate.tmh[CreateCharScr.indexGender];
		TileMap.tileID = this.bgID[CreateCharScr.indexGender] + 1;
		TileMap.loadMainTile();
		TileMap.loadTileCreatChar();
		GameCanvas.loadBG(this.bgID[CreateCharScr.indexGender]);
		GameScr.loadCamera(false, this.cx, this.cy);
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00003EDB File Offset: 0x000020DB
	public override void keyPress(int keyCode)
	{
		CreateCharScr.tAddName.keyPressed(keyCode);
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x000341DC File Offset: 0x000323DC
	public override void update()
	{
		this.cp1++;
		bool flag = this.cp1 > 30;
		if (flag)
		{
			this.cp1 = 0;
		}
		bool flag2 = this.cp1 % 15 < 5;
		if (flag2)
		{
			this.cf = 0;
		}
		else
		{
			this.cf = 1;
		}
		CreateCharScr.tAddName.update();
		bool flag3 = CreateCharScr.selected != 0;
		if (flag3)
		{
			CreateCharScr.tAddName.isFocus = false;
		}
	}

	// Token: 0x060001BA RID: 442 RVA: 0x00034258 File Offset: 0x00032458
	public override void updateKey()
	{
		bool flag = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21];
		if (flag)
		{
			CreateCharScr.selected--;
			bool flag2 = CreateCharScr.selected < 0;
			if (flag2)
			{
				CreateCharScr.selected = mResources.MENUNEWCHAR.Length - 1;
			}
		}
		bool flag3 = GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22];
		if (flag3)
		{
			CreateCharScr.selected++;
			bool flag4 = CreateCharScr.selected >= mResources.MENUNEWCHAR.Length;
			if (flag4)
			{
				CreateCharScr.selected = 0;
			}
		}
		bool flag5 = CreateCharScr.selected == 0;
		if (flag5)
		{
			bool flag6 = !GameCanvas.isTouch;
			if (flag6)
			{
				this.right = CreateCharScr.tAddName.cmdClear;
			}
			CreateCharScr.tAddName.update();
		}
		bool flag7 = CreateCharScr.selected == 1;
		if (flag7)
		{
			bool flag8 = GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23];
			if (flag8)
			{
				CreateCharScr.indexGender--;
				bool flag9 = CreateCharScr.indexGender < 0;
				if (flag9)
				{
					CreateCharScr.indexGender = mResources.MENUGENDER.Length - 1;
				}
				this.doChangeMap();
			}
			bool flag10 = GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24];
			if (flag10)
			{
				CreateCharScr.indexGender++;
				bool flag11 = CreateCharScr.indexGender > mResources.MENUGENDER.Length - 1;
				if (flag11)
				{
					CreateCharScr.indexGender = 0;
				}
				this.doChangeMap();
			}
			this.right = null;
		}
		bool flag12 = CreateCharScr.selected == 2;
		if (flag12)
		{
			bool flag13 = GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23];
			if (flag13)
			{
				CreateCharScr.indexHair--;
				bool flag14 = CreateCharScr.indexHair < 0;
				if (flag14)
				{
					CreateCharScr.indexHair = mResources.hairStyleName[0].Length - 1;
				}
			}
			bool flag15 = GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24];
			if (flag15)
			{
				CreateCharScr.indexHair++;
				bool flag16 = CreateCharScr.indexHair > mResources.hairStyleName[0].Length - 1;
				if (flag16)
				{
					CreateCharScr.indexHair = 0;
				}
			}
			this.right = null;
		}
		bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
		if (isPointerJustRelease)
		{
			int num = 110;
			int num2 = 60;
			int num3 = 78;
			bool flag17 = GameCanvas.w > GameCanvas.h;
			if (flag17)
			{
				num = 100;
				num2 = 40;
			}
			bool flag18 = GameCanvas.isPointerHoldIn(GameCanvas.w / 2 - 3 * num3 / 2, 15, num3 * 3, 80);
			if (flag18)
			{
				CreateCharScr.selected = 0;
				CreateCharScr.tAddName.isFocus = true;
			}
			bool flag19 = GameCanvas.isPointerHoldIn(GameCanvas.w / 2 - 3 * num3 / 2, num - 30, num3 * 3, num2 + 5);
			if (flag19)
			{
				CreateCharScr.selected = 1;
				int num4 = CreateCharScr.indexGender;
				CreateCharScr.indexGender = (GameCanvas.px - (GameCanvas.w / 2 - 3 * num3 / 2)) / num3;
				bool flag20 = CreateCharScr.indexGender < 0;
				if (flag20)
				{
					CreateCharScr.indexGender = 0;
				}
				bool flag21 = CreateCharScr.indexGender > mResources.MENUGENDER.Length - 1;
				if (flag21)
				{
					CreateCharScr.indexGender = mResources.MENUGENDER.Length - 1;
				}
				bool flag22 = num4 != CreateCharScr.indexGender;
				if (flag22)
				{
					this.doChangeMap();
				}
			}
			bool flag23 = GameCanvas.isPointerHoldIn(GameCanvas.w / 2 - 3 * num3 / 2, num - 30 + num2 + 5, num3 * 3, 65);
			if (flag23)
			{
				CreateCharScr.selected = 2;
				int num5 = CreateCharScr.indexHair;
				CreateCharScr.indexHair = (GameCanvas.px - (GameCanvas.w / 2 - 3 * num3 / 2)) / num3;
				bool flag24 = CreateCharScr.indexHair < 0;
				if (flag24)
				{
					CreateCharScr.indexHair = 0;
				}
				bool flag25 = CreateCharScr.indexHair > mResources.hairStyleName[0].Length - 1;
				if (flag25)
				{
					CreateCharScr.indexHair = mResources.hairStyleName[0].Length - 1;
				}
				bool flag26 = num5 != CreateCharScr.selected;
				if (flag26)
				{
					this.doChangeMap();
				}
			}
		}
		bool flag27 = !TouchScreenKeyboard.visible;
		if (flag27)
		{
			base.updateKey();
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x060001BB RID: 443 RVA: 0x00034664 File Offset: 0x00032864
	public override void paint(mGraphics g)
	{
		bool isLoadingMap = global::Char.isLoadingMap;
		if (!isLoadingMap)
		{
			GameCanvas.paintBGGameScr(g);
			g.translate(-GameScr.cmx, -GameScr.cmy);
			bool flag = !GameCanvas.lowGraphic;
			if (flag)
			{
				for (int i = 0; i < MapTemplate.vCurrItem[CreateCharScr.indexGender].size(); i++)
				{
					BgItem bgItem = (BgItem)MapTemplate.vCurrItem[CreateCharScr.indexGender].elementAt(i);
					bool flag2 = bgItem.idImage != -1 && bgItem.layer == 1;
					if (flag2)
					{
						bgItem.paint(g);
					}
				}
			}
			TileMap.paintTilemap(g);
			int num = 30;
			bool flag3 = GameCanvas.w == 128;
			if (flag3)
			{
				num = 20;
			}
			int num2 = CreateCharScr.hairID[CreateCharScr.indexGender][CreateCharScr.indexHair];
			int num3 = CreateCharScr.defaultLeg[CreateCharScr.indexGender];
			int num4 = CreateCharScr.defaultBody[CreateCharScr.indexGender];
			g.drawImage(TileMap.bong, this.cx, this.cy + this.dy, 3);
			Part part = GameScr.parts[num2];
			Part part2 = GameScr.parts[num3];
			Part part3 = GameScr.parts[num4];
			SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, this.cx + global::Char.CharInfo[this.cf][0][1] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dx, this.cy - global::Char.CharInfo[this.cf][0][2] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dy + this.dy, 0, 0);
			SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].id, this.cx + global::Char.CharInfo[this.cf][1][1] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dx, this.cy - global::Char.CharInfo[this.cf][1][2] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dy + this.dy, 0, 0);
			SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].id, this.cx + global::Char.CharInfo[this.cf][2][1] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dx, this.cy - global::Char.CharInfo[this.cf][2][2] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dy + this.dy, 0, 0);
			bool flag4 = !GameCanvas.lowGraphic;
			if (flag4)
			{
				for (int j = 0; j < MapTemplate.vCurrItem[CreateCharScr.indexGender].size(); j++)
				{
					BgItem bgItem2 = (BgItem)MapTemplate.vCurrItem[CreateCharScr.indexGender].elementAt(j);
					bool flag5 = bgItem2.idImage != -1 && bgItem2.layer == 3;
					if (flag5)
					{
						bgItem2.paint(g);
					}
				}
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			bool flag6 = GameCanvas.w < 200;
			if (flag6)
			{
				GameCanvas.paintz.paintFrame(GameScr.popupX, GameScr.popupY, GameScr.popupW, GameScr.popupH, g);
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, GameCanvas.w / 2 + global::Char.CharInfo[0][0][1] + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, GameScr.popupY + 30 + 3 * num - global::Char.CharInfo[0][0][2] + (int)part.pi[global::Char.CharInfo[0][0][0]].dy + this.dy, 0, 0);
				SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[0][1][0]].id, GameCanvas.w / 2 + global::Char.CharInfo[0][1][1] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dx, GameScr.popupY + 30 + 3 * num - global::Char.CharInfo[0][1][2] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dy + this.dy, 0, 0);
				SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[0][2][0]].id, GameCanvas.w / 2 + global::Char.CharInfo[0][2][1] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dx, GameScr.popupY + 30 + 3 * num - global::Char.CharInfo[0][2][2] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dy + this.dy, 0, 0);
				for (int k = 0; k < mResources.MENUNEWCHAR.Length; k++)
				{
					bool flag7 = CreateCharScr.selected == k;
					if (flag7)
					{
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 2, GameScr.popupX + 10 + ((GameCanvas.gameTick % 7 > 3) ? 1 : 0), GameScr.popupY + 35 + k * num, StaticObj.VCENTER_HCENTER);
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 0, GameScr.popupX + GameScr.popupW - 10 - ((GameCanvas.gameTick % 7 > 3) ? 1 : 0), GameScr.popupY + 35 + k * num, StaticObj.VCENTER_HCENTER);
					}
					mFont.tahoma_7b_dark.drawString(g, mResources.MENUNEWCHAR[k], GameScr.popupX + 20, GameScr.popupY + 30 + k * num, 0);
				}
				mFont.tahoma_7b_dark.drawString(g, mResources.MENUGENDER[CreateCharScr.indexGender], GameScr.popupX + 70, GameScr.popupY + 30 + num, mFont.LEFT);
				mFont.tahoma_7b_dark.drawString(g, mResources.hairStyleName[CreateCharScr.indexGender][CreateCharScr.indexHair], GameScr.popupX + 55, GameScr.popupY + 30 + 2 * num, mFont.LEFT);
				CreateCharScr.tAddName.paint(g);
			}
			else
			{
				bool flag8 = !Main.isPC;
				if (flag8)
				{
					bool flag9 = mGraphics.addYWhenOpenKeyBoard != 0;
					if (flag9)
					{
						this.yButton = 110;
						this.disY = 60;
						bool flag10 = GameCanvas.w > GameCanvas.h;
						if (flag10)
						{
							this.yButton = GameScr.popupY + 30 + 3 * num + (int)part3.pi[global::Char.CharInfo[0][2][0]].dy + this.dy - 15;
							this.disY = 35;
						}
					}
					else
					{
						this.yButton = 110;
						this.disY = 60;
						bool flag11 = GameCanvas.w > GameCanvas.h;
						if (flag11)
						{
							this.yButton = 100;
							this.disY = 45;
						}
					}
					CreateCharScr.tAddName.y = this.yButton - CreateCharScr.tAddName.height - this.disY + 5;
				}
				else
				{
					this.yButton = 110;
					this.disY = 60;
					bool flag12 = GameCanvas.w > GameCanvas.h;
					if (flag12)
					{
						this.yButton = 100;
						this.disY = 45;
					}
					CreateCharScr.tAddName.y = this.yBegin;
				}
				for (int l = 0; l < 3; l++)
				{
					int num5 = 78;
					bool flag13 = l != CreateCharScr.indexGender;
					if (flag13)
					{
						g.drawImage(GameScr.imgLbtn, GameCanvas.w / 2 - num5 + l * num5, this.yButton, 3);
					}
					else
					{
						bool flag14 = CreateCharScr.selected == 1;
						if (flag14)
						{
							g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, GameCanvas.w / 2 - num5 + l * num5, this.yButton - 20 + ((GameCanvas.gameTick % 7 > 3) ? 1 : 0), StaticObj.VCENTER_HCENTER);
						}
						g.drawImage(GameScr.imgLbtnFocus, GameCanvas.w / 2 - num5 + l * num5, this.yButton, 3);
					}
					mFont.tahoma_7b_dark.drawString(g, mResources.MENUGENDER[l], GameCanvas.w / 2 - num5 + l * num5, this.yButton - 5, mFont.CENTER);
				}
				for (int m = 0; m < 3; m++)
				{
					int num6 = 78;
					bool flag15 = m != CreateCharScr.indexHair;
					if (flag15)
					{
						g.drawImage(GameScr.imgLbtn, GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY, 3);
					}
					else
					{
						bool flag16 = CreateCharScr.selected == 2;
						if (flag16)
						{
							g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY - 20 + ((GameCanvas.gameTick % 7 > 3) ? 1 : 0), StaticObj.VCENTER_HCENTER);
						}
						g.drawImage(GameScr.imgLbtnFocus, GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY, 3);
					}
					mFont.tahoma_7b_dark.drawString(g, mResources.hairStyleName[CreateCharScr.indexGender][m], GameCanvas.w / 2 - num6 + m * num6, this.yButton + this.disY - 5, mFont.CENTER);
				}
				CreateCharScr.tAddName.paint(g);
			}
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			mFont.tahoma_7b_white.drawString(g, mResources.server + " " + LoginScr.serverName, 5, 5, 0, mFont.tahoma_7b_dark);
			bool flag17 = !TouchScreenKeyboard.visible;
			if (flag17)
			{
				base.paint(g);
			}
		}
	}

	// Token: 0x060001BC RID: 444 RVA: 0x000350B8 File Offset: 0x000332B8
	public void perform(int idAction, object p)
	{
		if (idAction <= 8001)
		{
			if (idAction != 8000)
			{
				if (idAction == 8001)
				{
					bool isLogin = GameCanvas.loginScr.isLogin2;
					if (isLogin)
					{
						GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, this, 10019, null), new Command(mResources.NO, this, 10020, null));
					}
					else
					{
						bool isWindowsPhone = Main.isWindowsPhone;
						if (isWindowsPhone)
						{
							GameMidlet.isBackWindowsPhone = true;
						}
						Session_ME.gI().close();
						GameCanvas.serverScreen.switchToMe();
					}
				}
			}
			else
			{
				bool flag = CreateCharScr.tAddName.getText().Equals(string.Empty);
				if (flag)
				{
					GameCanvas.startOKDlg(mResources.char_name_blank);
				}
				else
				{
					bool flag2 = CreateCharScr.tAddName.getText().Length < 5;
					if (flag2)
					{
						GameCanvas.startOKDlg(mResources.char_name_short);
					}
					else
					{
						bool flag3 = CreateCharScr.tAddName.getText().Length > 15;
						if (flag3)
						{
							GameCanvas.startOKDlg(mResources.char_name_long);
						}
						else
						{
							InfoDlg.showWait();
							Service.gI().createChar(CreateCharScr.tAddName.getText(), CreateCharScr.indexGender, CreateCharScr.hairID[CreateCharScr.indexGender][CreateCharScr.indexHair]);
						}
					}
				}
			}
		}
		else if (idAction != 10019)
		{
			if (idAction == 10020)
			{
				GameCanvas.endDlg();
			}
		}
		else
		{
			Session_ME.gI().close();
			GameCanvas.endDlg();
			GameCanvas.serverScreen.switchToMe();
		}
	}

	// Token: 0x04000466 RID: 1126
	public static CreateCharScr instance;

	// Token: 0x04000467 RID: 1127
	private PopUp p;

	// Token: 0x04000468 RID: 1128
	public static bool isCreateChar = false;

	// Token: 0x04000469 RID: 1129
	public static TField tAddName;

	// Token: 0x0400046A RID: 1130
	public static int indexGender;

	// Token: 0x0400046B RID: 1131
	public static int indexHair;

	// Token: 0x0400046C RID: 1132
	public static int selected;

	// Token: 0x0400046D RID: 1133
	public static int[][] hairID = new int[][]
	{
		new int[]
		{
			64,
			30,
			31
		},
		new int[]
		{
			9,
			29,
			32
		},
		new int[]
		{
			6,
			27,
			28
		}
	};

	// Token: 0x0400046E RID: 1134
	public static int[] defaultLeg = new int[]
	{
		2,
		13,
		8
	};

	// Token: 0x0400046F RID: 1135
	public static int[] defaultBody = new int[]
	{
		1,
		12,
		7
	};

	// Token: 0x04000470 RID: 1136
	private int yButton;

	// Token: 0x04000471 RID: 1137
	private int disY;

	// Token: 0x04000472 RID: 1138
	private int[] bgID = new int[]
	{
		0,
		4,
		8
	};

	// Token: 0x04000473 RID: 1139
	public int yBegin;

	// Token: 0x04000474 RID: 1140
	private int curIndex;

	// Token: 0x04000475 RID: 1141
	private int cx = 168;

	// Token: 0x04000476 RID: 1142
	private int cy = 350;

	// Token: 0x04000477 RID: 1143
	private int dy = 45;

	// Token: 0x04000478 RID: 1144
	private int cp1;

	// Token: 0x04000479 RID: 1145
	private int cf;
}
