using System;

// Token: 0x0200000C RID: 12
public class BgItem
{
	// Token: 0x06000061 RID: 97 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void clearHashTable()
	{
	}

	// Token: 0x06000062 RID: 98 RVA: 0x0000A40C File Offset: 0x0000860C
	public static bool isExistKeyNews(string keyNew)
	{
		for (int i = 0; i < BgItem.vKeysNew.size(); i++)
		{
			string text = (string)BgItem.vKeysNew.elementAt(i);
			bool flag = text.Equals(keyNew);
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x0000A460 File Offset: 0x00008660
	public static bool isExistKeyLast(string keyLast)
	{
		for (int i = 0; i < BgItem.vKeysLast.size(); i++)
		{
			string text = (string)BgItem.vKeysLast.elementAt(i);
			bool flag = text.Equals(keyLast);
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x0000A4B4 File Offset: 0x000086B4
	public bool isNotBlend()
	{
		bool flag = mGraphics.zoomLevel == 1;
		bool result;
		if (flag)
		{
			result = true;
		}
		else
		{
			bool flag2 = TileMap.isInAirMap();
			if (flag2)
			{
				result = true;
			}
			else
			{
				for (int i = 0; i < BgItem.idNotBlend.Length; i++)
				{
					bool flag3 = (int)this.idImage == BgItem.idNotBlend[i];
					if (flag3)
					{
						return true;
					}
				}
				result = false;
			}
		}
		return result;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x0000A51C File Offset: 0x0000871C
	public bool isMiniBg()
	{
		for (int i = 0; i < BgItem.isMiniBgz.Length; i++)
		{
			bool flag = (int)this.idImage == BgItem.isMiniBgz[i];
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000066 RID: 102 RVA: 0x0000A560 File Offset: 0x00008760
	public void changeColor()
	{
		bool flag = this.isNotBlend() || this.layer == 2 || this.layer == 4 || BgItem.imgNew.containsKey(this.idImage + "blend" + this.layer);
		if (!flag)
		{
			Image image = (Image)BgItem.imgNew.get(this.idImage + string.Empty);
			bool flag2 = image != null && image.getRealImageWidth() > 4;
			if (flag2)
			{
				sbyte[] array = Rms.loadRMS(string.Concat(new object[]
				{
					"x",
					mGraphics.zoomLevel,
					"blend",
					this.idImage,
					"layer",
					this.layer
				}));
				bool flag3 = array == null;
				if (flag3)
				{
					BgItem.imgNew.put(this.idImage + "blend" + this.layer, BgItemMn.blendImage(image, (int)this.layer, (int)this.idImage));
				}
				else
				{
					Image v = Image.createImage(array, 0, array.Length);
					BgItem.imgNew.put(this.idImage + "blend" + this.layer, v);
				}
			}
		}
	}

	// Token: 0x06000067 RID: 103 RVA: 0x0000A6D4 File Offset: 0x000088D4
	public void paint(mGraphics g)
	{
		bool flag = global::Char.isLoadingMap || (this.idImage == 279 && GameScr.gI().tMabuEff >= 110);
		if (!flag)
		{
			int cmx = GameScr.cmx;
			int cmy = GameScr.cmy;
			Image image = (this.layer == 2 || this.layer == 4) ? ((Image)BgItem.imgNew.get(this.idImage + string.Empty)) : (this.isNotBlend() ? ((Image)BgItem.imgNew.get(this.idImage + string.Empty)) : ((Image)BgItem.imgNew.get(this.idImage + "blend" + this.layer)));
			bool flag2 = image == null || this.idImage == 96;
			if (!flag2)
			{
				bool flag3 = this.layer == 4;
				if (flag3)
				{
					this.transX = -cmx / 2 + 100;
				}
				bool flag4 = this.idImage == 28 && this.layer == 3;
				if (flag4)
				{
					this.transX = -cmx / 3 + 200;
				}
				bool flag5 = (this.idImage == 67 || this.idImage == 68 || this.idImage == 69 || this.idImage == 70) && this.layer == 3;
				if (flag5)
				{
					this.transX = -cmx / 3 + 200;
				}
				bool flag6 = this.isMiniBg() && this.layer < 4;
				if (flag6)
				{
					this.transX = -(cmx >> 4) + 50;
					this.transY = (cmy >> 5) - 15;
				}
				int num = this.x + this.dx + this.transX;
				int num2 = this.y + this.dy + this.transY;
				bool flag7 = this.x + this.dx + image.getWidth() + this.transX >= cmx && this.x + this.dx + this.transX <= cmx + GameCanvas.w && this.y + this.dy + this.transY + image.getHeight() >= cmy && this.y + this.dy + this.transY <= cmy + GameCanvas.h;
				if (flag7)
				{
					g.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), this.trans, this.x + this.dx + this.transX, this.y + this.dy + this.transY, 0);
					bool flag8 = this.idImage == 11 && TileMap.mapID != 122;
					if (flag8)
					{
						g.setClip(num, num2 + 24, 48, 14);
						for (int i = 0; i < 2; i++)
						{
							g.drawRegion(TileMap.imgWaterflow, 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, num + i * 24, num2 + 24, 0);
						}
						g.setClip(GameScr.cmx, GameScr.cmy, GameScr.gW, GameScr.gH);
					}
				}
				bool flag9 = TileMap.isDoubleMap() && this.idImage > 137 && this.idImage != 156 && this.idImage != 159 && this.idImage != 157 && this.idImage != 165 && this.idImage != 167 && this.idImage != 168 && this.idImage != 169 && this.idImage != 170 && this.idImage != 238 && TileMap.pxw - (this.x + this.dx + this.transX) >= cmx && TileMap.pxw - (this.x + this.dx + this.transX + image.getWidth()) <= cmx + GameCanvas.w && this.y + this.dy + this.transY + image.getHeight() >= cmy && this.y + this.dy + this.transY <= cmy + GameCanvas.h && (this.idImage < 241 || this.idImage >= 266);
				if (flag9)
				{
					g.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 2, TileMap.pxw - (this.x + this.dx + this.transX), this.y + this.dy + this.transY, StaticObj.TOP_RIGHT);
				}
			}
		}
	}

	// Token: 0x0400009E RID: 158
	public int id;

	// Token: 0x0400009F RID: 159
	public int trans;

	// Token: 0x040000A0 RID: 160
	public short idImage;

	// Token: 0x040000A1 RID: 161
	public int x;

	// Token: 0x040000A2 RID: 162
	public int y;

	// Token: 0x040000A3 RID: 163
	public int dx;

	// Token: 0x040000A4 RID: 164
	public int dy;

	// Token: 0x040000A5 RID: 165
	public sbyte layer;

	// Token: 0x040000A6 RID: 166
	public int nTilenotMove;

	// Token: 0x040000A7 RID: 167
	public int[] tileX;

	// Token: 0x040000A8 RID: 168
	public int[] tileY;

	// Token: 0x040000A9 RID: 169
	public static MyHashTable imgNew = new MyHashTable();

	// Token: 0x040000AA RID: 170
	public static MyVector vKeysNew = new MyVector();

	// Token: 0x040000AB RID: 171
	public static MyVector vKeysLast = new MyVector();

	// Token: 0x040000AC RID: 172
	private bool isBlur;

	// Token: 0x040000AD RID: 173
	public int transX;

	// Token: 0x040000AE RID: 174
	public int transY;

	// Token: 0x040000AF RID: 175
	public static int[] idNotBlend = new int[]
	{
		79,
		80,
		81,
		82,
		83,
		84,
		85,
		86,
		87,
		88,
		89,
		90,
		91,
		92,
		95,
		144,
		99,
		100,
		101,
		102,
		103,
		104,
		105,
		106,
		107,
		108,
		109,
		110,
		111,
		112,
		113,
		114,
		115,
		117,
		118,
		119,
		120,
		121,
		122,
		123,
		124,
		125,
		126,
		127,
		132,
		133,
		134,
		139,
		140,
		141,
		142,
		143,
		144,
		145,
		146,
		147,
		171,
		121,
		122,
		229,
		218
	};

	// Token: 0x040000B0 RID: 176
	public static int[] isMiniBgz = new int[]
	{
		79,
		80,
		81,
		85,
		86,
		90,
		91,
		92,
		99,
		100,
		101,
		102,
		103,
		104,
		105,
		106,
		107,
		108
	};

	// Token: 0x040000B1 RID: 177
	public static sbyte[] newSmallVersion;
}
