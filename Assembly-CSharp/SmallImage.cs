using System;
using Assets.src.e;

// Token: 0x020000A8 RID: 168
public class SmallImage
{
	// Token: 0x060008E2 RID: 2274 RVA: 0x00005ECB File Offset: 0x000040CB
	public SmallImage()
	{
		this.readImage();
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x00092158 File Offset: 0x00090358
	public static void loadBigRMS()
	{
		bool flag = SmallImage.imgbig == null;
		if (flag)
		{
			SmallImage.imgbig = new Image[]
			{
				GameCanvas.loadImageRMS("/img/Big0.png"),
				GameCanvas.loadImageRMS("/img/Big1.png"),
				GameCanvas.loadImageRMS("/img/Big2.png"),
				GameCanvas.loadImageRMS("/img/Big3.png"),
				GameCanvas.loadImageRMS("/img/Big4.png")
			};
		}
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x00005EDC File Offset: 0x000040DC
	public static void freeBig()
	{
		SmallImage.imgbig = null;
		mSystem.gcc();
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00005EEB File Offset: 0x000040EB
	public static void loadBigImage()
	{
		SmallImage.imgEmpty = Image.createRGBImage(new int[1], 1, 1, true);
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x00005F01 File Offset: 0x00004101
	public static void init()
	{
		SmallImage.instance = null;
		SmallImage.instance = new SmallImage();
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void readData(byte[] data)
	{
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x000921C0 File Offset: 0x000903C0
	public void readImage()
	{
		int num = 0;
		try
		{
			DataInputStream dataInputStream = new DataInputStream(Rms.loadRMS("NR_image"));
			short num2 = dataInputStream.readShort();
			SmallImage.smallImg = new int[(int)num2][];
			for (int i = 0; i < SmallImage.smallImg.Length; i++)
			{
				SmallImage.smallImg[i] = new int[5];
			}
			for (int j = 0; j < (int)num2; j++)
			{
				num++;
				SmallImage.smallImg[j][0] = dataInputStream.readUnsignedByte();
				SmallImage.smallImg[j][1] = (int)dataInputStream.readShort();
				SmallImage.smallImg[j][2] = (int)dataInputStream.readShort();
				SmallImage.smallImg[j][3] = (int)dataInputStream.readShort();
				SmallImage.smallImg[j][4] = (int)dataInputStream.readShort();
			}
		}
		catch (Exception ex)
		{
			Cout.LogError3(string.Concat(new object[]
			{
				"Loi readImage: ",
				ex.ToString(),
				"i= ",
				num
			}));
		}
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void clearHastable()
	{
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x000922D4 File Offset: 0x000904D4
	public static void createImage(int id)
	{
		Res.outz(string.Concat(new object[]
		{
			"is request =",
			id,
			" zoom=",
			mGraphics.zoomLevel
		}));
		bool flag = mGraphics.zoomLevel == 1;
		if (flag)
		{
			Image image = GameCanvas.loadImage("/SmallImage/Small" + id + ".png");
			bool flag2 = image != null;
			if (flag2)
			{
				SmallImage.imgNew[id] = new Small(image, id);
			}
			else
			{
				SmallImage.imgNew[id] = new Small(SmallImage.imgEmpty, id);
				Service.gI().requestIcon(id);
			}
		}
		else
		{
			Image image2 = GameCanvas.loadImage("/SmallImage/Small" + id + ".png");
			bool flag3 = image2 != null;
			if (flag3)
			{
				SmallImage.imgNew[id] = new Small(image2, id);
			}
			else
			{
				bool flag4 = false;
				sbyte[] array = Rms.loadRMS(mGraphics.zoomLevel + "Small" + id);
				bool flag5 = array != null;
				if (flag5)
				{
					bool flag6 = SmallImage.newSmallVersion != null && array.Length % 127 != (int)SmallImage.newSmallVersion[id];
					if (flag6)
					{
						flag4 = true;
					}
					bool flag7 = !flag4;
					if (flag7)
					{
						Image image3 = Image.createImage(array, 0, array.Length);
						bool flag8 = image3 != null;
						if (flag8)
						{
							SmallImage.imgNew[id] = new Small(image3, id);
						}
						else
						{
							flag4 = true;
						}
					}
				}
				else
				{
					flag4 = true;
				}
				bool flag9 = flag4;
				if (flag9)
				{
					SmallImage.imgNew[id] = new Small(SmallImage.imgEmpty, id);
					Service.gI().requestIcon(id);
				}
			}
		}
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x0009247C File Offset: 0x0009067C
	public static void drawSmallImage(mGraphics g, int id, int x, int y, int transform, int anchor)
	{
		bool flag = SmallImage.imgbig == null;
		if (flag)
		{
			Small small = SmallImage.imgNew[id];
			bool flag2 = small == null;
			if (flag2)
			{
				SmallImage.createImage(id);
			}
			else
			{
				g.drawRegion(small, 0, 0, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img), transform, x, y, anchor);
			}
		}
		else
		{
			bool flag3 = SmallImage.smallImg != null;
			if (flag3)
			{
				bool flag4 = id >= SmallImage.smallImg.Length || SmallImage.smallImg[id][1] >= 256 || SmallImage.smallImg[id][3] >= 256 || SmallImage.smallImg[id][2] >= 256 || SmallImage.smallImg[id][4] >= 256;
				if (flag4)
				{
					Small small2 = SmallImage.imgNew[id];
					bool flag5 = small2 == null;
					if (flag5)
					{
						SmallImage.createImage(id);
					}
					else
					{
						small2.paint(g, transform, x, y, anchor);
					}
				}
				else
				{
					bool flag6 = SmallImage.imgbig[SmallImage.smallImg[id][0]] != null;
					if (flag6)
					{
						g.drawRegion(SmallImage.imgbig[SmallImage.smallImg[id][0]], SmallImage.smallImg[id][1], SmallImage.smallImg[id][2], SmallImage.smallImg[id][3], SmallImage.smallImg[id][4], transform, x, y, anchor);
					}
				}
			}
			else
			{
				bool flag7 = GameCanvas.currentScreen != GameScr.gI();
				if (flag7)
				{
					Small small3 = SmallImage.imgNew[id];
					bool flag8 = small3 == null;
					if (flag8)
					{
						SmallImage.createImage(id);
					}
					else
					{
						small3.paint(g, transform, x, y, anchor);
					}
				}
			}
		}
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x00092620 File Offset: 0x00090820
	public static void drawSmallImage(mGraphics g, int id, int f, int x, int y, int w, int h, int transform, int anchor)
	{
		bool flag = SmallImage.imgbig == null;
		if (flag)
		{
			Small small = SmallImage.imgNew[id];
			bool flag2 = small == null;
			if (flag2)
			{
				SmallImage.createImage(id);
			}
			else
			{
				g.drawRegion(small.img, 0, f * w, w, h, transform, x, y, anchor);
			}
		}
		else
		{
			bool flag3 = SmallImage.smallImg != null;
			if (flag3)
			{
				bool flag4 = id >= SmallImage.smallImg.Length || SmallImage.smallImg[id] == null || SmallImage.smallImg[id][1] >= 256 || SmallImage.smallImg[id][3] >= 256 || SmallImage.smallImg[id][2] >= 256 || SmallImage.smallImg[id][4] >= 256;
				if (flag4)
				{
					Small small2 = SmallImage.imgNew[id];
					bool flag5 = small2 == null;
					if (flag5)
					{
						SmallImage.createImage(id);
					}
					else
					{
						small2.paint(g, transform, f, x, y, w, h, anchor);
					}
				}
				else
				{
					bool flag6 = SmallImage.smallImg[id][0] != 4 && SmallImage.imgbig[SmallImage.smallImg[id][0]] != null;
					if (flag6)
					{
						g.drawRegion(SmallImage.imgbig[SmallImage.smallImg[id][0]], 0, f * w, w, h, transform, x, y, anchor);
					}
					else
					{
						Small small3 = SmallImage.imgNew[id];
						bool flag7 = small3 == null;
						if (flag7)
						{
							SmallImage.createImage(id);
						}
						else
						{
							small3.paint(g, transform, f, x, y, w, h, anchor);
						}
					}
				}
			}
			else
			{
				bool flag8 = GameCanvas.currentScreen != GameScr.gI();
				if (flag8)
				{
					Small small4 = SmallImage.imgNew[id];
					bool flag9 = small4 == null;
					if (flag9)
					{
						SmallImage.createImage(id);
					}
					else
					{
						small4.paint(g, transform, f, x, y, w, h, anchor);
					}
				}
			}
		}
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x00092800 File Offset: 0x00090A00
	public static void update()
	{
		int num = 0;
		bool flag = GameCanvas.gameTick % 1000 != 0;
		if (!flag)
		{
			for (int i = 0; i < SmallImage.imgNew.Length; i++)
			{
				bool flag2 = SmallImage.imgNew[i] != null;
				if (flag2)
				{
					num++;
					SmallImage.imgNew[i].update();
					SmallImage.smallCount++;
				}
			}
			bool flag3 = num > 200 && GameCanvas.lowGraphic;
			if (flag3)
			{
				SmallImage.imgNew = new Small[(int)SmallImage.maxSmall];
			}
		}
	}

	// Token: 0x040010B1 RID: 4273
	public static int[][] smallImg;

	// Token: 0x040010B2 RID: 4274
	public static SmallImage instance;

	// Token: 0x040010B3 RID: 4275
	public static Image[] imgbig;

	// Token: 0x040010B4 RID: 4276
	public static Small[] imgNew;

	// Token: 0x040010B5 RID: 4277
	public static MyVector vKeys = new MyVector();

	// Token: 0x040010B6 RID: 4278
	public static Image imgEmpty = null;

	// Token: 0x040010B7 RID: 4279
	public static sbyte[] newSmallVersion;

	// Token: 0x040010B8 RID: 4280
	public static int smallCount;

	// Token: 0x040010B9 RID: 4281
	public static short maxSmall;
}
