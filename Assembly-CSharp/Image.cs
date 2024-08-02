using System;
using System.Threading;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class Image
{
	// Token: 0x06000367 RID: 871 RVA: 0x000507F8 File Offset: 0x0004E9F8
	public static Image createEmptyImage()
	{
		return Image.__createEmptyImage();
	}

	// Token: 0x06000368 RID: 872 RVA: 0x00050810 File Offset: 0x0004EA10
	public static Image createImage(string filename)
	{
		return Image.__createImage(filename);
	}

	// Token: 0x06000369 RID: 873 RVA: 0x00050828 File Offset: 0x0004EA28
	public static Image createImage(byte[] imageData)
	{
		return Image.__createImage(imageData);
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00050840 File Offset: 0x0004EA40
	public static Image createImage(Image src, int x, int y, int w, int h, int transform)
	{
		return Image.__createImage(src, x, y, w, h, transform);
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00050860 File Offset: 0x0004EA60
	public static Image createImage(int w, int h)
	{
		return Image.__createImage(w, h);
	}

	// Token: 0x0600036C RID: 876 RVA: 0x0005087C File Offset: 0x0004EA7C
	public static Image createImage(Image img)
	{
		Image image = Image.createImage(img.w, img.h);
		image.texture = img.texture;
		image.texture.Apply();
		return image;
	}

	// Token: 0x0600036D RID: 877 RVA: 0x000508BC File Offset: 0x0004EABC
	public static Image createImage(sbyte[] imageData, int offset, int lenght)
	{
		bool flag = offset + lenght > imageData.Length;
		Image result;
		if (flag)
		{
			result = null;
		}
		else
		{
			byte[] array = new byte[lenght];
			for (int i = 0; i < lenght; i++)
			{
				array[i] = Image.convertSbyteToByte(imageData[i + offset]);
			}
			result = Image.createImage(array);
		}
		return result;
	}

	// Token: 0x0600036E RID: 878 RVA: 0x00050910 File Offset: 0x0004EB10
	public static byte convertSbyteToByte(sbyte var)
	{
		bool flag = var > 0;
		byte result;
		if (flag)
		{
			result = (byte)var;
		}
		else
		{
			result = (byte)((int)var + 256);
		}
		return result;
	}

	// Token: 0x0600036F RID: 879 RVA: 0x00050938 File Offset: 0x0004EB38
	public static byte[] convertArrSbyteToArrByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			bool flag = var[i] > 0;
			if (flag)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)((int)var[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x06000370 RID: 880 RVA: 0x00050990 File Offset: 0x0004EB90
	public static Image createRGBImage(int[] rbg, int w, int h, bool bl)
	{
		Image image = Image.createImage(w, h);
		Color[] array = new Color[rbg.Length];
		for (int i = 0; i < array.Length; i++)
		{
			ref Color ptr = ref array[i];
			ptr = Image.setColorFromRBG(rbg[i]);
		}
		image.texture.SetPixels(0, 0, w, h, array);
		image.texture.Apply();
		return image;
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00050A00 File Offset: 0x0004EC00
	public static Color setColorFromRBG(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float b = (float)num / 256f;
		float g = (float)num2 / 256f;
		float r = (float)num3 / 256f;
		return new Color(r, g, b);
	}

	// Token: 0x06000372 RID: 882 RVA: 0x00050A58 File Offset: 0x0004EC58
	public static void update()
	{
		bool flag = Image.status == 2;
		if (flag)
		{
			Image.status = 1;
			Image.imgTemp = Image.__createEmptyImage();
			Image.status = 0;
		}
		else
		{
			bool flag2 = Image.status == 3;
			if (flag2)
			{
				Image.status = 1;
				Image.imgTemp = Image.__createImage(Image.filenametemp);
				Image.status = 0;
			}
			else
			{
				bool flag3 = Image.status == 4;
				if (flag3)
				{
					Image.status = 1;
					Image.imgTemp = Image.__createImage(Image.datatemp);
					Image.status = 0;
				}
				else
				{
					bool flag4 = Image.status == 5;
					if (flag4)
					{
						Image.status = 1;
						Image.imgTemp = Image.__createImage(Image.imgSrcTemp, Image.xtemp, Image.ytemp, Image.wtemp, Image.htemp, Image.transformtemp);
						Image.status = 0;
					}
					else
					{
						bool flag5 = Image.status == 6;
						if (flag5)
						{
							Image.status = 1;
							Image.imgTemp = Image.__createImage(Image.wtemp, Image.htemp);
							Image.status = 0;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000373 RID: 883 RVA: 0x00050B5C File Offset: 0x0004ED5C
	private static Image _createEmptyImage()
	{
		bool flag = Image.status != 0;
		Image result;
		if (flag)
		{
			Cout.LogError("CANNOT CREATE EMPTY IMAGE WHEN CREATING OTHER IMAGE");
			result = null;
		}
		else
		{
			Image.imgTemp = null;
			Image.status = 2;
			int i;
			for (i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Image.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 500;
			if (flag3)
			{
				Cout.LogError("TOO LONG FOR CREATE EMPTY IMAGE");
				Image.status = 0;
			}
			result = Image.imgTemp;
		}
		return result;
	}

	// Token: 0x06000374 RID: 884 RVA: 0x00050BEC File Offset: 0x0004EDEC
	private static Image _createImage(string filename)
	{
		bool flag = Image.status != 0;
		Image result;
		if (flag)
		{
			Cout.LogError("CANNOT CREATE IMAGE " + filename + " WHEN CREATING OTHER IMAGE");
			result = null;
		}
		else
		{
			Image.imgTemp = null;
			Image.filenametemp = filename;
			Image.status = 3;
			int i;
			for (i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Image.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 500;
			if (flag3)
			{
				Cout.LogError("TOO LONG FOR CREATE IMAGE " + filename);
				Image.status = 0;
			}
			result = Image.imgTemp;
		}
		return result;
	}

	// Token: 0x06000375 RID: 885 RVA: 0x00050C94 File Offset: 0x0004EE94
	private static Image _createImage(byte[] imageData)
	{
		bool flag = Image.status != 0;
		Image result;
		if (flag)
		{
			Cout.LogError("CANNOT CREATE IMAGE(FromArray) WHEN CREATING OTHER IMAGE");
			result = null;
		}
		else
		{
			Image.imgTemp = null;
			Image.datatemp = imageData;
			Image.status = 4;
			int i;
			for (i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Image.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 500;
			if (flag3)
			{
				Cout.LogError("TOO LONG FOR CREATE IMAGE(FromArray)");
				Image.status = 0;
			}
			result = Image.imgTemp;
		}
		return result;
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00050D28 File Offset: 0x0004EF28
	private static Image _createImage(Image src, int x, int y, int w, int h, int transform)
	{
		bool flag = Image.status != 0;
		Image result;
		if (flag)
		{
			Cout.LogError("CANNOT CREATE IMAGE(FromSrcPart) WHEN CREATING OTHER IMAGE");
			result = null;
		}
		else
		{
			Image.imgTemp = null;
			Image.imgSrcTemp = src;
			Image.xtemp = x;
			Image.ytemp = y;
			Image.wtemp = w;
			Image.htemp = h;
			Image.transformtemp = transform;
			Image.status = 5;
			int i;
			for (i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Image.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 500;
			if (flag3)
			{
				Cout.LogError("TOO LONG FOR CREATE IMAGE(FromSrcPart)");
				Image.status = 0;
			}
			result = Image.imgTemp;
		}
		return result;
	}

	// Token: 0x06000377 RID: 887 RVA: 0x00050DE0 File Offset: 0x0004EFE0
	private static Image _createImage(int w, int h)
	{
		bool flag = Image.status != 0;
		Image result;
		if (flag)
		{
			Cout.LogError("CANNOT CREATE IMAGE(w,h) WHEN CREATING OTHER IMAGE");
			result = null;
		}
		else
		{
			Image.imgTemp = null;
			Image.wtemp = w;
			Image.htemp = h;
			Image.status = 6;
			int i;
			for (i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Image.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 500;
			if (flag3)
			{
				Cout.LogError("TOO LONG FOR CREATE IMAGE(w,h)");
				Image.status = 0;
			}
			result = Image.imgTemp;
		}
		return result;
	}

	// Token: 0x06000378 RID: 888 RVA: 0x00050E7C File Offset: 0x0004F07C
	public static byte[] loadData(string filename)
	{
		Image image = new Image();
		TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
		bool flag = textAsset == null || textAsset.bytes == null || textAsset.bytes.Length == 0;
		if (flag)
		{
			throw new Exception("NULL POINTER EXCEPTION AT Image __createImage " + filename);
		}
		sbyte[] array = ArrayCast.cast(textAsset.bytes);
		Debug.LogError("CHIEU DAI MANG BYTE IMAGE CREAT = " + array.Length);
		return textAsset.bytes;
	}

	// Token: 0x06000379 RID: 889 RVA: 0x00050F0C File Offset: 0x0004F10C
	private static Image __createImage(string filename)
	{
		Image image = new Image();
		Texture2D x = Resources.Load(filename) as Texture2D;
		bool flag = x == null;
		if (flag)
		{
			throw new Exception("NULL POINTER EXCEPTION AT Image __createImage " + filename);
		}
		image.texture = x;
		image.w = image.texture.width;
		image.h = image.texture.height;
		Image.setTextureQuality(image);
		return image;
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00050F80 File Offset: 0x0004F180
	private static Image __createImage(byte[] imageData)
	{
		bool flag = imageData == null || imageData.Length == 0;
		Image result;
		if (flag)
		{
			Cout.LogError("Create Image from byte array fail");
			result = null;
		}
		else
		{
			Image image = new Image();
			try
			{
				image.texture.LoadImage(imageData);
				image.w = image.texture.width;
				image.h = image.texture.height;
				Image.setTextureQuality(image);
				result = image;
			}
			catch (Exception)
			{
				Cout.LogError("CREAT IMAGE FROM ARRAY FAIL \n" + Environment.StackTrace);
				result = image;
			}
		}
		return result;
	}

	// Token: 0x0600037B RID: 891 RVA: 0x0005101C File Offset: 0x0004F21C
	private static Image __createImage(Image src, int x, int y, int w, int h, int transform)
	{
		Image image = new Image();
		image.texture = new Texture2D(w, h);
		y = src.texture.height - y - h;
		for (int i = 0; i < w; i++)
		{
			for (int j = 0; j < h; j++)
			{
				int num = i;
				bool flag = transform == 2;
				if (flag)
				{
					num = w - i;
				}
				int num2 = j;
				image.texture.SetPixel(i, j, src.texture.GetPixel(x + num, y + num2));
			}
		}
		image.texture.Apply();
		image.w = image.texture.width;
		image.h = image.texture.height;
		Image.setTextureQuality(image);
		return image;
	}

	// Token: 0x0600037C RID: 892 RVA: 0x000510EC File Offset: 0x0004F2EC
	private static Image __createEmptyImage()
	{
		return new Image();
	}

	// Token: 0x0600037D RID: 893 RVA: 0x00051104 File Offset: 0x0004F304
	public static Image __createImage(int w, int h)
	{
		Image image = new Image();
		image.texture = new Texture2D(w, h, TextureFormat.RGBA32, false);
		Image.setTextureQuality(image);
		image.w = w;
		image.h = h;
		image.texture.Apply();
		return image;
	}

	// Token: 0x0600037E RID: 894 RVA: 0x00051150 File Offset: 0x0004F350
	public static int getImageWidth(Image image)
	{
		return image.getWidth();
	}

	// Token: 0x0600037F RID: 895 RVA: 0x00051168 File Offset: 0x0004F368
	public static int getImageHeight(Image image)
	{
		return image.getHeight();
	}

	// Token: 0x06000380 RID: 896 RVA: 0x00051180 File Offset: 0x0004F380
	public int getWidth()
	{
		return this.w / mGraphics.zoomLevel;
	}

	// Token: 0x06000381 RID: 897 RVA: 0x000511A0 File Offset: 0x0004F3A0
	public int getHeight()
	{
		return this.h / mGraphics.zoomLevel;
	}

	// Token: 0x06000382 RID: 898 RVA: 0x000047D7 File Offset: 0x000029D7
	private static void setTextureQuality(Image img)
	{
		Image.setTextureQuality(img.texture);
	}

	// Token: 0x06000383 RID: 899 RVA: 0x000047E6 File Offset: 0x000029E6
	public static void setTextureQuality(Texture2D texture)
	{
		texture.anisoLevel = 0;
		texture.filterMode = FilterMode.Point;
		texture.mipMapBias = 0f;
		texture.wrapMode = TextureWrapMode.Clamp;
	}

	// Token: 0x06000384 RID: 900 RVA: 0x000511C0 File Offset: 0x0004F3C0
	public Color[] getColor()
	{
		return this.texture.GetPixels();
	}

	// Token: 0x06000385 RID: 901 RVA: 0x000511E0 File Offset: 0x0004F3E0
	public int getRealImageWidth()
	{
		return this.w;
	}

	// Token: 0x06000386 RID: 902 RVA: 0x000511F8 File Offset: 0x0004F3F8
	public int getRealImageHeight()
	{
		return this.h;
	}

	// Token: 0x06000387 RID: 903 RVA: 0x00051210 File Offset: 0x0004F410
	public void getRGB(ref int[] data, int x1, int x2, int x, int y, int w, int h)
	{
		Color[] pixels = this.texture.GetPixels(x, this.h - 1 - y, w, h);
		for (int i = 0; i < pixels.Length; i++)
		{
			data[i] = mGraphics.getIntByColor(pixels[i]);
		}
	}

	// Token: 0x04000803 RID: 2051
	private const int INTERVAL = 5;

	// Token: 0x04000804 RID: 2052
	private const int MAXTIME = 500;

	// Token: 0x04000805 RID: 2053
	public Texture2D texture = new Texture2D(1, 1);

	// Token: 0x04000806 RID: 2054
	public static Image imgTemp;

	// Token: 0x04000807 RID: 2055
	public static string filenametemp;

	// Token: 0x04000808 RID: 2056
	public static byte[] datatemp;

	// Token: 0x04000809 RID: 2057
	public static Image imgSrcTemp;

	// Token: 0x0400080A RID: 2058
	public static int xtemp;

	// Token: 0x0400080B RID: 2059
	public static int ytemp;

	// Token: 0x0400080C RID: 2060
	public static int wtemp;

	// Token: 0x0400080D RID: 2061
	public static int htemp;

	// Token: 0x0400080E RID: 2062
	public static int transformtemp;

	// Token: 0x0400080F RID: 2063
	public int w;

	// Token: 0x04000810 RID: 2064
	public int h;

	// Token: 0x04000811 RID: 2065
	public static int status;

	// Token: 0x04000812 RID: 2066
	public Color colorBlend = Color.black;
}
