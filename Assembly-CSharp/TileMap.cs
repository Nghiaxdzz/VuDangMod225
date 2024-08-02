using System;

// Token: 0x020000BC RID: 188
public class TileMap
{
	// Token: 0x060009A0 RID: 2464 RVA: 0x0009C068 File Offset: 0x0009A268
	public static void loadBg()
	{
		TileMap.bong = GameCanvas.loadImage("/mainImage/myTexture2dbong.png");
		bool flag = mGraphics.zoomLevel != 1 && !Main.isIpod && !Main.isIphone4;
		if (flag)
		{
			TileMap.imgLight = GameCanvas.loadImage("/bg/light.png");
		}
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x0009C0B4 File Offset: 0x0009A2B4
	public static bool isVoDaiMap()
	{
		return TileMap.mapID == 51 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 129 || TileMap.mapID == 130;
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x0009C110 File Offset: 0x0009A310
	public static bool isTrainingMap()
	{
		return TileMap.mapID == 39 || TileMap.mapID == 40 || TileMap.mapID == 41;
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0009C14C File Offset: 0x0009A34C
	public static bool mapPhuBang()
	{
		return GameScr.phuban_Info != null && TileMap.mapID == (int)GameScr.phuban_Info.idmapPaint;
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x0009C184 File Offset: 0x0009A384
	public static BgItem getBIById(int id)
	{
		for (int i = 0; i < TileMap.vItemBg.size(); i++)
		{
			BgItem bgItem = (BgItem)TileMap.vItemBg.elementAt(i);
			bool flag = bgItem.id == id;
			if (flag)
			{
				return bgItem;
			}
		}
		return null;
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x0009C1D8 File Offset: 0x0009A3D8
	public static bool isOfflineMap()
	{
		for (int i = 0; i < TileMap.offlineId.Length; i++)
		{
			bool flag = TileMap.mapID == TileMap.offlineId[i];
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x0009C21C File Offset: 0x0009A41C
	public static bool isHighterMap()
	{
		for (int i = 0; i < TileMap.offlineId.Length; i++)
		{
			bool flag = TileMap.mapID == TileMap.highterId[i];
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0009C260 File Offset: 0x0009A460
	public static bool isToOfflineMap()
	{
		for (int i = 0; i < TileMap.toOfflineId.Length; i++)
		{
			bool flag = TileMap.mapID == TileMap.toOfflineId[i];
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x00006430 File Offset: 0x00004630
	public static void freeTilemap()
	{
		TileMap.imgTile = null;
		mSystem.gcc();
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void loadTileCreatChar()
	{
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0009C2A4 File Offset: 0x0009A4A4
	public static bool isExistMoreOne(int id)
	{
		bool flag = id == 156 || id == 330 || id == 345 || id == 334;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = TileMap.mapID == 54 || TileMap.mapID == 55 || TileMap.mapID == 56 || TileMap.mapID == 57 || TileMap.mapID == 58 || TileMap.mapID == 59 || TileMap.mapID == 103;
			if (flag2)
			{
				result = false;
			}
			else
			{
				int num = 0;
				for (int i = 0; i < TileMap.vCurrItem.size(); i++)
				{
					BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(i);
					bool flag3 = bgItem.id == id;
					if (flag3)
					{
						num++;
					}
				}
				bool flag4 = num > 2;
				result = flag4;
			}
		}
		return result;
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0009C38C File Offset: 0x0009A58C
	public static void loadTileImage()
	{
		bool flag = TileMap.imgWaterfall == null;
		if (flag)
		{
			TileMap.imgWaterfall = GameCanvas.loadImageRMS("/tWater/wtf.png");
		}
		bool flag2 = TileMap.imgTopWaterfall == null;
		if (flag2)
		{
			TileMap.imgTopWaterfall = GameCanvas.loadImageRMS("/tWater/twtf.png");
		}
		bool flag3 = TileMap.imgWaterflow == null;
		if (flag3)
		{
			TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/wts.png");
		}
		bool flag4 = TileMap.imgWaterlowN == null;
		if (flag4)
		{
			TileMap.imgWaterlowN = GameCanvas.loadImageRMS("/tWater/wtsN.png");
		}
		bool flag5 = TileMap.imgWaterlowN2 == null;
		if (flag5)
		{
			TileMap.imgWaterlowN2 = GameCanvas.loadImageRMS("/tWater/wtsN2.png");
		}
		mSystem.gcc();
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x0009C434 File Offset: 0x0009A634
	public static void setTile(int index, int[] mapsArr, int type)
	{
		for (int i = 0; i < mapsArr.Length; i++)
		{
			bool flag = TileMap.maps[index] == mapsArr[i];
			if (flag)
			{
				TileMap.types[index] |= type;
				break;
			}
		}
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x0009C47C File Offset: 0x0009A67C
	public static void loadMap(int tileId)
	{
		TileMap.pxh = TileMap.tmh * (int)TileMap.size;
		TileMap.pxw = TileMap.tmw * (int)TileMap.size;
		Res.outz("load tile ID= " + TileMap.tileID);
		int num = tileId - 1;
		try
		{
			for (int i = 0; i < TileMap.tmw * TileMap.tmh; i++)
			{
				for (int j = 0; j < TileMap.tileType[num].Length; j++)
				{
					TileMap.setTile(i, TileMap.tileIndex[num][j], TileMap.tileType[num][j]);
				}
			}
		}
		catch (Exception)
		{
			Cout.println("Error Load Map");
			GameMidlet.instance.exit();
		}
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x0009C548 File Offset: 0x0009A748
	public static bool isInAirMap()
	{
		return TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48;
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x0009C584 File Offset: 0x0009A784
	public static bool isDoubleMap()
	{
		return TileMap.isMapDouble || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48 || TileMap.mapID == 51 || TileMap.mapID == 52 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 119 || TileMap.mapID == 120 || TileMap.mapID == 121 || TileMap.mapID == 125 || TileMap.mapID == 129 || TileMap.mapID == 130;
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x0009C658 File Offset: 0x0009A858
	public static void getTile()
	{
		bool flag = Main.typeClient == 3 || Main.typeClient == 5;
		if (flag)
		{
			bool flag2 = mGraphics.zoomLevel == 1;
			if (flag2)
			{
				TileMap.imgTile = new Image[1];
				TileMap.imgTile[0] = GameCanvas.loadImage("/t/" + TileMap.tileID + ".png");
			}
			else
			{
				TileMap.imgTile = new Image[100];
				for (int i = 0; i < TileMap.imgTile.Length; i++)
				{
					TileMap.imgTile[i] = GameCanvas.loadImage(string.Concat(new object[]
					{
						"/t/",
						TileMap.tileID,
						"/",
						i + 1,
						".png"
					}));
				}
			}
		}
		else
		{
			bool flag3 = mGraphics.zoomLevel == 1;
			if (flag3)
			{
				bool flag4 = TileMap.imgTile != null;
				if (flag4)
				{
					for (int j = 0; j < TileMap.imgTile.Length; j++)
					{
						bool flag5 = TileMap.imgTile[j] != null;
						if (flag5)
						{
							TileMap.imgTile[j].texture = null;
							TileMap.imgTile[j] = null;
						}
					}
					mSystem.gcc();
				}
				TileMap.imgTile = new Image[100];
				string path = string.Empty;
				for (int k = 0; k < TileMap.imgTile.Length; k++)
				{
					path = ((k >= 9) ? string.Concat(new object[]
					{
						"/t/",
						TileMap.tileID,
						"/t_",
						k + 1
					}) : string.Concat(new object[]
					{
						"/t/",
						TileMap.tileID,
						"/t_0",
						k + 1
					}));
					TileMap.imgTile[k] = GameCanvas.loadImage(path);
				}
			}
			else
			{
				Image image = GameCanvas.loadImageRMS("/t/" + TileMap.tileID + "$1.png");
				bool flag6 = image != null;
				if (flag6)
				{
					Rms.DeleteStorage(string.Concat(new object[]
					{
						"x",
						mGraphics.zoomLevel,
						"t",
						TileMap.tileID
					}));
					TileMap.imgTile = new Image[100];
					for (int l = 0; l < TileMap.imgTile.Length; l++)
					{
						TileMap.imgTile[l] = GameCanvas.loadImageRMS(string.Concat(new object[]
						{
							"/t/",
							TileMap.tileID,
							"$",
							l + 1,
							".png"
						}));
					}
				}
				else
				{
					image = GameCanvas.loadImageRMS("/t/" + TileMap.tileID + ".png");
					bool flag7 = image != null;
					if (flag7)
					{
						Rms.DeleteStorage("$");
						TileMap.imgTile = new Image[1];
						TileMap.imgTile[0] = image;
					}
				}
			}
		}
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x0009C98C File Offset: 0x0009AB8C
	public static void paintTile(mGraphics g, int frame, int indexX, int indexY)
	{
		bool flag = TileMap.imgTile != null;
		if (flag)
		{
			bool flag2 = TileMap.imgTile.Length == 1;
			if (flag2)
			{
				g.drawRegion(TileMap.imgTile[0], 0, frame * (int)TileMap.size, (int)TileMap.size, (int)TileMap.size, 0, indexX * (int)TileMap.size, indexY * (int)TileMap.size, 0);
			}
			else
			{
				g.drawImage(TileMap.imgTile[frame], indexX * (int)TileMap.size, indexY * (int)TileMap.size, 0);
			}
		}
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x0009CA0C File Offset: 0x0009AC0C
	public static void paintTile(mGraphics g, int frame, int x, int y, int w, int h)
	{
		bool flag = TileMap.imgTile != null;
		if (flag)
		{
			bool flag2 = TileMap.imgTile.Length == 1;
			if (flag2)
			{
				g.drawRegion(TileMap.imgTile[0], 0, frame * w, w, w, 0, x, y, 0);
			}
			else
			{
				g.drawImage(TileMap.imgTile[frame], x, y, 0);
			}
		}
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x0009CA68 File Offset: 0x0009AC68
	public static void paintTilemapLOW(mGraphics g)
	{
		for (int i = GameScr.gssx; i < GameScr.gssxe; i++)
		{
			for (int j = GameScr.gssy; j < GameScr.gssye; j++)
			{
				int num = TileMap.maps[j * TileMap.tmw + i] - 1;
				bool flag = num != -1;
				if (flag)
				{
					TileMap.paintTile(g, num, i, j);
				}
				bool flag2 = (TileMap.tileTypeAt(i, j) & 32) == 32;
				if (flag2)
				{
					g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
				}
				else
				{
					bool flag3 = (TileMap.tileTypeAt(i, j) & 64) == 64;
					if (flag3)
					{
						bool flag4 = (TileMap.tileTypeAt(i, j - 1) & 32) == 32;
						if (flag4)
						{
							g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
						}
						else
						{
							bool flag5 = (TileMap.tileTypeAt(i, j - 1) & 4096) == 4096;
							if (flag5)
							{
								TileMap.paintTile(g, 21, i, j);
							}
						}
						Image arg = (TileMap.tileID == 5) ? TileMap.imgWaterlowN : ((TileMap.tileID != 8) ? TileMap.imgWaterflow : TileMap.imgWaterlowN2);
						g.drawRegion(arg, 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
					}
				}
				bool flag6 = (TileMap.tileTypeAt(i, j) & 2048) == 2048;
				if (flag6)
				{
					bool flag7 = (TileMap.tileTypeAt(i, j - 1) & 32) == 32;
					if (flag7)
					{
						g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
					}
					else
					{
						bool flag8 = (TileMap.tileTypeAt(i, j - 1) & 4096) == 4096;
						if (flag8)
						{
							TileMap.paintTile(g, 21, i, j);
						}
					}
					TileMap.paintTile(g, TileMap.maps[j * TileMap.tmw + i] - 1, i, j);
				}
			}
		}
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x0009CCAC File Offset: 0x0009AEAC
	public static void paintTilemap(mGraphics g)
	{
		bool flag = VuDang.xoamap || global::Char.isLoadingMap;
		if (!flag)
		{
			GameScr.gI().paintBgItem(g, 1);
			for (int i = 0; i < GameScr.vItemMap.size(); i++)
			{
				((ItemMap)GameScr.vItemMap.elementAt(i)).paintAuraItemEff(g);
			}
			for (int j = GameScr.gssx; j < GameScr.gssxe; j++)
			{
				for (int k = GameScr.gssy; k < GameScr.gssye; k++)
				{
					bool flag2 = j == 0 || j == TileMap.tmw - 1;
					if (!flag2)
					{
						int num = TileMap.maps[k * TileMap.tmw + j] - 1;
						bool flag3 = (TileMap.tileTypeAt(j, k) & 256) == 256;
						if (!flag3)
						{
							bool flag4 = (TileMap.tileTypeAt(j, k) & 32) == 32;
							if (flag4)
							{
								g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 8 >> 1), 24, 24, 0, j * (int)TileMap.size, k * (int)TileMap.size, 0);
							}
							else
							{
								bool flag5 = (TileMap.tileTypeAt(j, k) & 128) == 128;
								if (flag5)
								{
									g.drawRegion(TileMap.imgTopWaterfall, 0, 24 * (GameCanvas.gameTick % 8 >> 1), 24, 24, 0, j * (int)TileMap.size, k * (int)TileMap.size, 0);
								}
								else
								{
									bool flag6 = TileMap.tileID == 13 && num != -1;
									if (!flag6)
									{
										bool flag7 = TileMap.tileID == 2 && (TileMap.tileTypeAt(j, k) & 512) == 512 && num != -1;
										if (flag7)
										{
											TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size, 24, 1);
											TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size + 1, 24, 24);
										}
										bool flag8 = TileMap.tileID == 3;
										if (flag8)
										{
										}
										bool flag9 = (TileMap.tileTypeAt(j, k) & 16) == 16;
										if (flag9)
										{
											TileMap.bx = j * (int)TileMap.size - GameScr.cmx;
											TileMap.dbx = TileMap.bx - GameScr.gW2;
											TileMap.dfx = (int)(TileMap.size - 2) * TileMap.dbx / (int)TileMap.size;
											TileMap.fx = TileMap.dfx + GameScr.gW2;
											TileMap.paintTile(g, num, TileMap.fx + GameScr.cmx, k * (int)TileMap.size, 24, 24);
										}
										else
										{
											bool flag10 = (TileMap.tileTypeAt(j, k) & 512) == 512;
											if (flag10)
											{
												bool flag11 = num != -1;
												if (flag11)
												{
													TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size, 24, 1);
													TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size + 1, 24, 24);
												}
											}
											else
											{
												bool flag12 = num != -1;
												if (flag12)
												{
													TileMap.paintTile(g, num, j, k);
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
			bool flag13 = GameScr.cmx < 24;
			if (flag13)
			{
				for (int l = GameScr.gssy; l < GameScr.gssye; l++)
				{
					int num2 = TileMap.maps[l * TileMap.tmw + 1] - 1;
					bool flag14 = num2 != -1;
					if (flag14)
					{
						TileMap.paintTile(g, num2, 0, l);
					}
				}
			}
			bool flag15 = GameScr.cmx <= GameScr.cmxLim;
			if (!flag15)
			{
				int num3 = TileMap.tmw - 2;
				for (int m = GameScr.gssy; m < GameScr.gssye; m++)
				{
					int num4 = TileMap.maps[m * TileMap.tmw + num3] - 1;
					bool flag16 = num4 != -1;
					if (flag16)
					{
						TileMap.paintTile(g, num4, num3 + 1, m);
					}
				}
			}
		}
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x0009D0D4 File Offset: 0x0009B2D4
	public static bool isWaterEff()
	{
		bool flag = TileMap.mapID == 54 || TileMap.mapID == 55 || TileMap.mapID == 56 || TileMap.mapID == 57 || TileMap.mapID == 138;
		return !flag;
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x0009D124 File Offset: 0x0009B324
	public static void paintOutTilemap(mGraphics g)
	{
		bool lowGraphic = GameCanvas.lowGraphic;
		if (!lowGraphic)
		{
			int num = 0;
			for (int i = GameScr.gssx; i < GameScr.gssxe; i++)
			{
				for (int j = GameScr.gssy; j < GameScr.gssye; j++)
				{
					num++;
					bool flag = (TileMap.tileTypeAt(i, j) & 64) != 64;
					if (!flag)
					{
						Image arg = (TileMap.tileID == 5) ? TileMap.imgWaterlowN : ((TileMap.tileID != 8) ? TileMap.imgWaterflow : TileMap.imgWaterlowN2);
						bool flag2 = !TileMap.isWaterEff();
						if (flag2)
						{
							g.drawRegion(arg, 0, 0, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 1, 0);
							g.drawRegion(arg, 0, 0, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 3, 0);
						}
						g.drawRegion(arg, 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 12, 0);
						bool flag3 = TileMap.yWater == 0 && TileMap.isWaterEff();
						if (flag3)
						{
							TileMap.yWater = j * (int)TileMap.size - 12;
							int color = 16777215;
							bool flag4 = GameCanvas.typeBg == 2;
							if (flag4)
							{
								color = 10871287;
							}
							else
							{
								bool flag5 = GameCanvas.typeBg == 4;
								if (flag5)
								{
									color = 8111470;
								}
								else
								{
									bool flag6 = GameCanvas.typeBg == 7;
									if (flag6)
									{
										color = 5693125;
									}
								}
							}
							BackgroudEffect.addWater(color, TileMap.yWater + 15);
						}
					}
				}
			}
			BackgroudEffect.paintWaterAll(g);
		}
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x0009D2DC File Offset: 0x0009B4DC
	public static void loadMapFromResource(int mapID)
	{
		DataInputStream dataInputStream = MyStream.readFile("/mymap/" + mapID);
		TileMap.tmw = (int)((ushort)dataInputStream.read());
		TileMap.tmh = (int)((ushort)dataInputStream.read());
		TileMap.maps = new int[dataInputStream.available()];
		for (int i = 0; i < TileMap.tmw * TileMap.tmh; i++)
		{
			TileMap.maps[i] = (int)((ushort)dataInputStream.read());
		}
		TileMap.types = new int[TileMap.maps.Length];
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x0009D368 File Offset: 0x0009B568
	public static int tileAt(int x, int y)
	{
		int result;
		try
		{
			result = TileMap.maps[y * TileMap.tmw + x];
		}
		catch (Exception)
		{
			result = 1000;
		}
		return result;
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x0009D3A4 File Offset: 0x0009B5A4
	public static int tileTypeAt(int x, int y)
	{
		int result;
		try
		{
			result = TileMap.types[y * TileMap.tmw + x];
		}
		catch (Exception)
		{
			result = 1000;
		}
		return result;
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x0009D3E0 File Offset: 0x0009B5E0
	public static int tileTypeAtPixel(int px, int py)
	{
		int result;
		try
		{
			result = TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size];
		}
		catch (Exception)
		{
			result = 1000;
		}
		return result;
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0009D428 File Offset: 0x0009B628
	public static bool tileTypeAt(int px, int py, int t)
	{
		bool result;
		try
		{
			result = ((TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] & t) == t);
		}
		catch (Exception)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0000643F File Offset: 0x0000463F
	public static void setTileTypeAtPixel(int px, int py, int t)
	{
		TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] |= t;
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x00006466 File Offset: 0x00004666
	public static void setTileTypeAt(int x, int y, int t)
	{
		TileMap.types[y * TileMap.tmw + x] = t;
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x00006479 File Offset: 0x00004679
	public static void killTileTypeAt(int px, int py, int t)
	{
		TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] &= ~t;
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0009D474 File Offset: 0x0009B674
	public static int tileYofPixel(int py)
	{
		return py / (int)TileMap.size * (int)TileMap.size;
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0009D474 File Offset: 0x0009B674
	public static int tileXofPixel(int px)
	{
		return px / (int)TileMap.size * (int)TileMap.size;
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0009D494 File Offset: 0x0009B694
	public static void loadMainTile()
	{
		bool flag = TileMap.lastTileID != TileMap.tileID;
		if (flag)
		{
			TileMap.getTile();
			TileMap.lastTileID = TileMap.tileID;
		}
	}

	// Token: 0x040011ED RID: 4589
	public const int T_EMPTY = 0;

	// Token: 0x040011EE RID: 4590
	public const int T_TOP = 2;

	// Token: 0x040011EF RID: 4591
	public const int T_LEFT = 4;

	// Token: 0x040011F0 RID: 4592
	public const int T_RIGHT = 8;

	// Token: 0x040011F1 RID: 4593
	public const int T_TREE = 16;

	// Token: 0x040011F2 RID: 4594
	public const int T_WATERFALL = 32;

	// Token: 0x040011F3 RID: 4595
	public const int T_WATERFLOW = 64;

	// Token: 0x040011F4 RID: 4596
	public const int T_TOPFALL = 128;

	// Token: 0x040011F5 RID: 4597
	public const int T_OUTSIDE = 256;

	// Token: 0x040011F6 RID: 4598
	public const int T_DOWN1PIXEL = 512;

	// Token: 0x040011F7 RID: 4599
	public const int T_BRIDGE = 1024;

	// Token: 0x040011F8 RID: 4600
	public const int T_UNDERWATER = 2048;

	// Token: 0x040011F9 RID: 4601
	public const int T_SOLIDGROUND = 4096;

	// Token: 0x040011FA RID: 4602
	public const int T_BOTTOM = 8192;

	// Token: 0x040011FB RID: 4603
	public const int T_DIE = 16384;

	// Token: 0x040011FC RID: 4604
	public const int T_HEBI = 32768;

	// Token: 0x040011FD RID: 4605
	public const int T_BANG = 65536;

	// Token: 0x040011FE RID: 4606
	public const int T_JUM8 = 131072;

	// Token: 0x040011FF RID: 4607
	public const int T_NT0 = 262144;

	// Token: 0x04001200 RID: 4608
	public const int T_NT1 = 524288;

	// Token: 0x04001201 RID: 4609
	public const int T_CENTER = 1;

	// Token: 0x04001202 RID: 4610
	public static int tmw;

	// Token: 0x04001203 RID: 4611
	public static int tmh;

	// Token: 0x04001204 RID: 4612
	public static int pxw;

	// Token: 0x04001205 RID: 4613
	public static int pxh;

	// Token: 0x04001206 RID: 4614
	public static int tileID;

	// Token: 0x04001207 RID: 4615
	public static int lastTileID = -1;

	// Token: 0x04001208 RID: 4616
	public static int[] maps;

	// Token: 0x04001209 RID: 4617
	public static int[] types;

	// Token: 0x0400120A RID: 4618
	public static Image[] imgTile;

	// Token: 0x0400120B RID: 4619
	public static Image imgTileSmall;

	// Token: 0x0400120C RID: 4620
	public static Image imgMiniMap;

	// Token: 0x0400120D RID: 4621
	public static Image imgWaterfall;

	// Token: 0x0400120E RID: 4622
	public static Image imgTopWaterfall;

	// Token: 0x0400120F RID: 4623
	public static Image imgWaterflow;

	// Token: 0x04001210 RID: 4624
	public static Image imgWaterlowN;

	// Token: 0x04001211 RID: 4625
	public static Image imgWaterlowN2;

	// Token: 0x04001212 RID: 4626
	public static Image imgWaterF;

	// Token: 0x04001213 RID: 4627
	public static Image imgLeaf;

	// Token: 0x04001214 RID: 4628
	public static sbyte size = 24;

	// Token: 0x04001215 RID: 4629
	private static int bx;

	// Token: 0x04001216 RID: 4630
	private static int dbx;

	// Token: 0x04001217 RID: 4631
	private static int fx;

	// Token: 0x04001218 RID: 4632
	private static int dfx;

	// Token: 0x04001219 RID: 4633
	public static string[] instruction;

	// Token: 0x0400121A RID: 4634
	public static int[] iX;

	// Token: 0x0400121B RID: 4635
	public static int[] iY;

	// Token: 0x0400121C RID: 4636
	public static int[] iW;

	// Token: 0x0400121D RID: 4637
	public static int iCount;

	// Token: 0x0400121E RID: 4638
	public static bool isMapDouble = false;

	// Token: 0x0400121F RID: 4639
	public static string mapName = string.Empty;

	// Token: 0x04001220 RID: 4640
	public static sbyte versionMap = 1;

	// Token: 0x04001221 RID: 4641
	public static int mapID;

	// Token: 0x04001222 RID: 4642
	public static int lastBgID = -1;

	// Token: 0x04001223 RID: 4643
	public static int zoneID;

	// Token: 0x04001224 RID: 4644
	public static int bgID;

	// Token: 0x04001225 RID: 4645
	public static int bgType;

	// Token: 0x04001226 RID: 4646
	public static int lastType = -1;

	// Token: 0x04001227 RID: 4647
	public static int typeMap;

	// Token: 0x04001228 RID: 4648
	public static sbyte planetID;

	// Token: 0x04001229 RID: 4649
	public static sbyte lastPlanetId = -1;

	// Token: 0x0400122A RID: 4650
	public static long timeTranMini;

	// Token: 0x0400122B RID: 4651
	public static MyVector vGo = new MyVector();

	// Token: 0x0400122C RID: 4652
	public static MyVector vItemBg = new MyVector();

	// Token: 0x0400122D RID: 4653
	public static MyVector vCurrItem = new MyVector();

	// Token: 0x0400122E RID: 4654
	public static string[] mapNames;

	// Token: 0x0400122F RID: 4655
	public static sbyte MAP_NORMAL = 0;

	// Token: 0x04001230 RID: 4656
	public static Image bong;

	// Token: 0x04001231 RID: 4657
	public const int TRAIDAT_DOINUI = 0;

	// Token: 0x04001232 RID: 4658
	public const int TRAIDAT_RUNG = 1;

	// Token: 0x04001233 RID: 4659
	public const int TRAIDAT_DAORUA = 2;

	// Token: 0x04001234 RID: 4660
	public const int TRAIDAT_DADO = 3;

	// Token: 0x04001235 RID: 4661
	public const int NAMEK_THUNGLUNG = 5;

	// Token: 0x04001236 RID: 4662
	public const int NAMEK_DOINUI = 4;

	// Token: 0x04001237 RID: 4663
	public const int NAMEK_RUNG = 6;

	// Token: 0x04001238 RID: 4664
	public const int NAMEK_DAO = 7;

	// Token: 0x04001239 RID: 4665
	public const int SAYAI_DOINUI = 8;

	// Token: 0x0400123A RID: 4666
	public const int SAYAI_RUNG = 9;

	// Token: 0x0400123B RID: 4667
	public const int SAYAI_CITY = 10;

	// Token: 0x0400123C RID: 4668
	public const int SAYAI_NIGHT = 11;

	// Token: 0x0400123D RID: 4669
	public const int KAMISAMA = 12;

	// Token: 0x0400123E RID: 4670
	public const int TIME_ROOM = 13;

	// Token: 0x0400123F RID: 4671
	public const int HELL = 15;

	// Token: 0x04001240 RID: 4672
	public const int BEERUS = 16;

	// Token: 0x04001241 RID: 4673
	public static Image[] bgItem = new Image[8];

	// Token: 0x04001242 RID: 4674
	public static MyVector vObject = new MyVector();

	// Token: 0x04001243 RID: 4675
	public static int[] offlineId = new int[]
	{
		21,
		22,
		23,
		39,
		40,
		41
	};

	// Token: 0x04001244 RID: 4676
	public static int[] highterId = new int[]
	{
		21,
		22,
		23,
		24,
		25,
		26
	};

	// Token: 0x04001245 RID: 4677
	public static int[] toOfflineId = new int[]
	{
		0,
		7,
		14
	};

	// Token: 0x04001246 RID: 4678
	public static int[][] tileType;

	// Token: 0x04001247 RID: 4679
	public static int[][][] tileIndex;

	// Token: 0x04001248 RID: 4680
	public static Image imgLight = GameCanvas.loadImage("/bg/light.png");

	// Token: 0x04001249 RID: 4681
	public static int sizeMiniMap = 2;

	// Token: 0x0400124A RID: 4682
	public static int gssx;

	// Token: 0x0400124B RID: 4683
	public static int gssxe;

	// Token: 0x0400124C RID: 4684
	public static int gssy;

	// Token: 0x0400124D RID: 4685
	public static int gssye;

	// Token: 0x0400124E RID: 4686
	public static int countx;

	// Token: 0x0400124F RID: 4687
	public static int county;

	// Token: 0x04001250 RID: 4688
	private static int[] colorMini = new int[]
	{
		5257738,
		8807192
	};

	// Token: 0x04001251 RID: 4689
	public static int yWater = 0;
}
