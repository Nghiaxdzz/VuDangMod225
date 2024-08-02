using System;
using System.Text;
using UnityEngine;

// Token: 0x02000073 RID: 115
public class mSystem
{
	// Token: 0x06000577 RID: 1399 RVA: 0x000051AB File Offset: 0x000033AB
	public static void resetCurInapp()
	{
		mSystem.curINAPP = 0;
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void LogCMD(string st)
	{
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x00063944 File Offset: 0x00061B44
	public static string getTimeCountDown(long timeStart, int secondCount, bool isOnlySecond, bool isShortText)
	{
		string text = string.Empty;
		long num = (timeStart + (long)(secondCount * 1000) - mSystem.currentTimeMillis()) / 1000L;
		bool flag = num <= 0L;
		string result;
		if (flag)
		{
			result = string.Empty;
		}
		else
		{
			long num2 = 0L;
			long num3 = 0L;
			long num4 = num / 60L;
			long num5 = num;
			if (isOnlySecond)
			{
				result = num5 + string.Empty;
			}
			else
			{
				bool flag2 = num >= 86400L;
				if (flag2)
				{
					num2 = num / 86400L;
					num3 = num % 86400L / 3600L;
				}
				else
				{
					bool flag3 = num >= 3600L;
					if (flag3)
					{
						num3 = num / 3600L;
						num4 = num % 3600L / 60L;
					}
					else
					{
						bool flag4 = num >= 60L;
						if (flag4)
						{
							num4 = num / 60L;
							num5 = num % 60L;
						}
						else
						{
							num5 = num;
						}
					}
				}
				if (isShortText)
				{
					bool flag5 = num2 > 0L;
					if (flag5)
					{
						return num2 + "d";
					}
					bool flag6 = num3 > 0L;
					if (flag6)
					{
						return num3 + "h";
					}
					bool flag7 = num4 > 0L;
					if (flag7)
					{
						return num4 + "m";
					}
					bool flag8 = num5 > 0L;
					if (flag8)
					{
						return num5 + "s";
					}
				}
				bool flag9 = num2 > 0L;
				if (flag9)
				{
					bool flag10 = num2 >= 10L;
					if (flag10)
					{
						text = ((num3 < 1L) ? (num2 + "d") : ((num3 >= 10L) ? string.Concat(new object[]
						{
							num2,
							"d",
							num3,
							"h"
						}) : string.Concat(new object[]
						{
							num2,
							"d0",
							num3,
							"h"
						})));
					}
					else
					{
						bool flag11 = num2 < 10L;
						if (flag11)
						{
							text = ((num3 < 1L) ? (num2 + "d") : ((num3 >= 10L) ? string.Concat(new object[]
							{
								num2,
								"d",
								num3,
								"h"
							}) : string.Concat(new object[]
							{
								num2,
								"d0",
								num3,
								"h"
							})));
						}
					}
				}
				else
				{
					bool flag12 = num3 > 0L;
					if (flag12)
					{
						bool flag13 = num3 >= 10L;
						if (flag13)
						{
							text = ((num4 < 1L) ? (num3 + "h") : ((num4 >= 10L) ? string.Concat(new object[]
							{
								num3,
								"h",
								num4,
								"m"
							}) : string.Concat(new object[]
							{
								num3,
								"h0",
								num4,
								"m"
							})));
						}
						else
						{
							bool flag14 = num3 < 10L;
							if (flag14)
							{
								text = ((num4 < 1L) ? (num3 + "h") : ((num4 >= 10L) ? string.Concat(new object[]
								{
									num3,
									"h",
									num4,
									"m"
								}) : string.Concat(new object[]
								{
									num3,
									"h0",
									num4,
									"m"
								})));
							}
						}
					}
					else
					{
						bool flag15 = num4 > 0L;
						if (flag15)
						{
							bool flag16 = num4 >= 10L;
							if (flag16)
							{
								bool flag17 = num5 >= 10L;
								if (flag17)
								{
									text = string.Concat(new object[]
									{
										num4,
										"m",
										num5,
										string.Empty
									});
								}
								else
								{
									bool flag18 = num5 < 10L;
									if (flag18)
									{
										text = string.Concat(new object[]
										{
											num4,
											"m0",
											num5,
											string.Empty
										});
									}
								}
							}
							else
							{
								bool flag19 = num4 < 10L;
								if (flag19)
								{
									bool flag20 = num5 >= 10L;
									if (flag20)
									{
										text = string.Concat(new object[]
										{
											num4,
											"m",
											num5,
											string.Empty
										});
									}
									else
									{
										bool flag21 = num5 < 10L;
										if (flag21)
										{
											text = string.Concat(new object[]
											{
												num4,
												"m0",
												num5,
												string.Empty
											});
										}
									}
								}
							}
						}
						else
						{
							text = ((num5 >= 10L) ? (num5 + string.Empty) : ("0" + num5 + string.Empty));
						}
					}
				}
				result = text;
			}
		}
		return result;
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x00063EC4 File Offset: 0x000620C4
	public static string numberTostring2(int aa)
	{
		string result;
		try
		{
			string text = string.Empty;
			string str = string.Empty;
			string text2 = aa + string.Empty;
			bool flag = text2.Equals(string.Empty);
			if (flag)
			{
				result = text;
			}
			else
			{
				bool flag2 = text2[0] == '-';
				if (flag2)
				{
					str = "-";
					text2 = text2.Substring(1);
				}
				for (int i = text2.Length - 1; i >= 0; i--)
				{
					text = (((text2.Length - 1 - i) % 3 != 0 || text2.Length - 1 - i <= 0) ? (text2[i].ToString() + text) : (text2[i].ToString() + "." + text));
				}
				result = str + text;
			}
		}
		catch (Exception)
		{
			result = aa + string.Empty;
		}
		return result;
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x00063FD4 File Offset: 0x000621D4
	public static string numberTostring(long number)
	{
		string text = string.Empty + number;
		bool flag = false;
		try
		{
			string text2 = string.Empty;
			bool flag2 = number < 0L;
			if (flag2)
			{
				flag = true;
				number = -number;
				text = string.Empty + number;
			}
			bool flag3 = number >= 1000000000L;
			int length;
			if (flag3)
			{
				text2 = "b";
				number /= 1000000000L;
				length = (string.Empty + number).Length;
			}
			else
			{
				bool flag4 = number >= 1000000L;
				if (flag4)
				{
					text2 = "m";
					number /= 1000000L;
					length = (string.Empty + number).Length;
				}
				else
				{
					bool flag5 = number < 1000L;
					if (flag5)
					{
						bool flag6 = flag;
						if (flag6)
						{
							return "-" + text;
						}
						return text;
					}
					else
					{
						text2 = "k";
						number /= 1000L;
						length = (string.Empty + number).Length;
					}
				}
			}
			int num = int.Parse(text.Substring(length, 2));
			text = ((num == 0) ? (text.Substring(0, length) + text2) : ((num % 10 != 0) ? (text.Substring(0, length) + "," + text.Substring(length, 2) + text2) : (text.Substring(0, length) + "," + text.Substring(length, 1) + text2)));
		}
		catch (Exception)
		{
		}
		bool flag7 = flag;
		string result;
		if (flag7)
		{
			result = "-" + text;
		}
		else
		{
			result = text;
		}
		return result;
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x000051B4 File Offset: 0x000033B4
	public static void callHotlinePC()
	{
		Application.OpenURL("http://ngocrongonline.com/");
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void callHotlineJava()
	{
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void callHotlineIphone()
	{
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void callHotlineWindowsPhone()
	{
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void closeBanner()
	{
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void showBanner()
	{
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void createAdmob()
	{
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void checkAdComlete()
	{
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x000051C2 File Offset: 0x000033C2
	public static void paintPopUp2(mGraphics g, int x, int y, int w, int h)
	{
		g.fillRect(x, y, w + 10, h, 0, 90);
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x000051D7 File Offset: 0x000033D7
	public static void arraycopy(sbyte[] scr, int scrPos, sbyte[] dest, int destPos, int lenght)
	{
		Array.Copy(scr, scrPos, dest, destPos, lenght);
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x0006419C File Offset: 0x0006239C
	public static void arrayReplace(sbyte[] scr, int scrPos, ref sbyte[] dest, int destPos, int lenght)
	{
		bool flag = scr != null && dest != null && scrPos + lenght <= scr.Length;
		if (flag)
		{
			sbyte[] array = new sbyte[dest.Length + lenght];
			for (int i = 0; i < destPos; i++)
			{
				array[i] = dest[i];
			}
			for (int j = destPos; j < destPos + lenght; j++)
			{
				array[j] = scr[scrPos + j - destPos];
			}
			for (int k = destPos + lenght; k < array.Length; k++)
			{
				array[k] = dest[destPos + k - lenght];
			}
		}
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00064240 File Offset: 0x00062440
	public static long currentTimeMillis()
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (DateTime.UtcNow.Ticks - dateTime.Ticks) / 10000L;
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x000051E6 File Offset: 0x000033E6
	public static void freeData()
	{
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00064280 File Offset: 0x00062480
	public static sbyte[] convertToSbyte(byte[] scr)
	{
		sbyte[] array = new sbyte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (sbyte)scr[i];
		}
		return array;
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x000642B8 File Offset: 0x000624B8
	public static sbyte[] convertToSbyte(string scr)
	{
		ASCIIEncoding asciiencoding = new ASCIIEncoding();
		byte[] bytes = asciiencoding.GetBytes(scr);
		return mSystem.convertToSbyte(bytes);
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00050938 File Offset: 0x0004EB38
	public static byte[] convetToByte(sbyte[] scr)
	{
		byte[] array = new byte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			bool flag = scr[i] > 0;
			if (flag)
			{
				array[i] = (byte)scr[i];
			}
			else
			{
				array[i] = (byte)((int)scr[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x000642E0 File Offset: 0x000624E0
	public static char[] ToCharArray(sbyte[] scr)
	{
		char[] array = new char[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (char)scr[i];
		}
		return array;
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00064318 File Offset: 0x00062518
	public static int currentHour()
	{
		return DateTime.Now.Hour;
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x000051F5 File Offset: 0x000033F5
	public static void println(object str)
	{
		Debug.Log(str);
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x000051E6 File Offset: 0x000033E6
	public static void gcc()
	{
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00064338 File Offset: 0x00062538
	public static mSystem gI()
	{
		bool flag = mSystem.instance == null;
		if (flag)
		{
			mSystem.instance = new mSystem();
		}
		return mSystem.instance;
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x000051FF File Offset: 0x000033FF
	public static void onConnectOK()
	{
		Controller.isConnectOK = true;
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x00005208 File Offset: 0x00003408
	public static void onConnectionFail()
	{
		Controller.isConnectionFail = true;
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00005211 File Offset: 0x00003411
	public static void onDisconnected()
	{
		Controller.isDisconnected = true;
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void exitWP()
	{
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x00064368 File Offset: 0x00062568
	public static void paintFlyText(mGraphics g)
	{
		for (int i = 0; i < 5; i++)
		{
			bool flag = GameScr.flyTextState[i] != -1 && GameCanvas.isPaint(GameScr.flyTextX[i], GameScr.flyTextY[i]);
			if (flag)
			{
				bool flag2 = GameScr.flyTextColor[i] == mFont.RED;
				if (flag2)
				{
					mFont.bigNumber_red.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
				}
				else
				{
					bool flag3 = GameScr.flyTextColor[i] == mFont.YELLOW;
					if (flag3)
					{
						mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
					}
					else
					{
						bool flag4 = GameScr.flyTextColor[i] == mFont.GREEN;
						if (flag4)
						{
							mFont.bigNumber_green.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
						}
						else
						{
							bool flag5 = GameScr.flyTextColor[i] == mFont.FATAL;
							if (flag5)
							{
								mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
							}
							else
							{
								bool flag6 = GameScr.flyTextColor[i] == mFont.FATAL_ME;
								if (flag6)
								{
									mFont.bigNumber_green.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
								}
								else
								{
									bool flag7 = GameScr.flyTextColor[i] == mFont.MISS;
									if (flag7)
									{
										mFont.bigNumber_While.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.tahoma_7_grey);
									}
									else
									{
										bool flag8 = GameScr.flyTextColor[i] == mFont.ORANGE;
										if (flag8)
										{
											mFont.bigNumber_orange.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
										}
										else
										{
											bool flag9 = GameScr.flyTextColor[i] == mFont.ADDMONEY;
											if (flag9)
											{
												mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
											}
											else
											{
												bool flag10 = GameScr.flyTextColor[i] == mFont.MISS_ME;
												if (flag10)
												{
													mFont.bigNumber_While.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
												}
												else
												{
													bool flag11 = GameScr.flyTextColor[i] == mFont.HP;
													if (flag11)
													{
														mFont.bigNumber_red.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
													}
													else
													{
														bool flag12 = GameScr.flyTextColor[i] == mFont.MP;
														if (flag12)
														{
															mFont.bigNumber_blue.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
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

	// Token: 0x06000596 RID: 1430 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void endKey()
	{
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x0006469C File Offset: 0x0006289C
	public static FrameImage getFraImage(string nameImg)
	{
		FrameImage result = null;
		MainImage mainImage = null;
		bool flag = mainImage == null;
		if (flag)
		{
			mainImage = ImgByName.getImagePath(nameImg, ImgByName.hashImagePath);
		}
		bool flag2 = mainImage.img != null;
		if (flag2)
		{
			int num = mainImage.img.getHeight() / (int)mainImage.nFrame;
			bool flag3 = num < 1;
			if (flag3)
			{
				num = 1;
			}
			result = new FrameImage(mainImage.img, mainImage.img.getWidth(), num);
		}
		return result;
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x00064718 File Offset: 0x00062918
	public static Image loadImage(string path)
	{
		return GameCanvas.loadImage(path);
	}

	// Token: 0x04000D40 RID: 3392
	public static bool isTest;

	// Token: 0x04000D41 RID: 3393
	public static string strAdmob;

	// Token: 0x04000D42 RID: 3394
	public static bool loadAdOk;

	// Token: 0x04000D43 RID: 3395
	public static string publicID;

	// Token: 0x04000D44 RID: 3396
	public static string android_pack;

	// Token: 0x04000D45 RID: 3397
	public static int clientType = 4;

	// Token: 0x04000D46 RID: 3398
	public static sbyte LANGUAGE;

	// Token: 0x04000D47 RID: 3399
	public static sbyte curINAPP;

	// Token: 0x04000D48 RID: 3400
	public static sbyte maxINAPP = 5;

	// Token: 0x04000D49 RID: 3401
	public const int JAVA = 1;

	// Token: 0x04000D4A RID: 3402
	public const int ANDROID = 2;

	// Token: 0x04000D4B RID: 3403
	public const int IP_JB = 3;

	// Token: 0x04000D4C RID: 3404
	public const int PC = 4;

	// Token: 0x04000D4D RID: 3405
	public const int IP_APPSTORE = 5;

	// Token: 0x04000D4E RID: 3406
	public const int WINDOWS_PHONE = 6;

	// Token: 0x04000D4F RID: 3407
	public const int GOOGLE_PLAY = 7;

	// Token: 0x04000D50 RID: 3408
	public static mSystem instance;
}
