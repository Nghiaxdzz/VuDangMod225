using System;

// Token: 0x0200008E RID: 142
public class Res
{
	// Token: 0x06000789 RID: 1929 RVA: 0x00086D70 File Offset: 0x00084F70
	public static void init()
	{
		Res.cosz = new short[91];
		Res.tanz = new int[91];
		for (int i = 0; i <= 90; i++)
		{
			Res.cosz[i] = Res.sinz[90 - i];
			bool flag = Res.cosz[i] == 0;
			if (flag)
			{
				Res.tanz[i] = int.MaxValue;
			}
			else
			{
				Res.tanz[i] = ((int)Res.sinz[i] << 10) / (int)Res.cosz[i];
			}
		}
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x00086DF8 File Offset: 0x00084FF8
	public static int sin(int a)
	{
		a = Res.fixangle(a);
		bool flag = a >= 0 && a < 90;
		int result;
		if (flag)
		{
			result = (int)Res.sinz[a];
		}
		else
		{
			bool flag2 = a >= 90 && a < 180;
			if (flag2)
			{
				result = (int)Res.sinz[180 - a];
			}
			else
			{
				bool flag3 = a >= 180 && a < 270;
				if (flag3)
				{
					result = (int)(-(int)Res.sinz[a - 180]);
				}
				else
				{
					result = (int)(-(int)Res.sinz[360 - a]);
				}
			}
		}
		return result;
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x00086E8C File Offset: 0x0008508C
	public static int cos(int a)
	{
		a = Res.fixangle(a);
		bool flag = a >= 0 && a < 90;
		int result;
		if (flag)
		{
			result = (int)Res.cosz[a];
		}
		else
		{
			bool flag2 = a >= 90 && a < 180;
			if (flag2)
			{
				result = (int)(-(int)Res.cosz[180 - a]);
			}
			else
			{
				bool flag3 = a >= 180 && a < 270;
				if (flag3)
				{
					result = (int)(-(int)Res.cosz[a - 180]);
				}
				else
				{
					result = (int)Res.cosz[360 - a];
				}
			}
		}
		return result;
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x00086F20 File Offset: 0x00085120
	public static int tan(int a)
	{
		a = Res.fixangle(a);
		bool flag = a >= 0 && a < 90;
		int result;
		if (flag)
		{
			result = Res.tanz[a];
		}
		else
		{
			bool flag2 = a >= 90 && a < 180;
			if (flag2)
			{
				result = -Res.tanz[180 - a];
			}
			else
			{
				bool flag3 = a >= 180 && a < 270;
				if (flag3)
				{
					result = Res.tanz[a - 180];
				}
				else
				{
					result = -Res.tanz[360 - a];
				}
			}
		}
		return result;
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x00086FB4 File Offset: 0x000851B4
	public static int atan(int a)
	{
		for (int i = 0; i <= 90; i++)
		{
			bool flag = Res.tanz[i] >= a;
			if (flag)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x00086FF4 File Offset: 0x000851F4
	public static int angle(int dx, int dy)
	{
		bool flag = dx != 0;
		int num;
		if (flag)
		{
			int a = global::Math.abs((dy << 10) / dx);
			num = Res.atan(a);
			bool flag2 = dy >= 0 && dx < 0;
			if (flag2)
			{
				num = 180 - num;
			}
			bool flag3 = dy < 0 && dx < 0;
			if (flag3)
			{
				num = 180 + num;
			}
			bool flag4 = dy < 0 && dx >= 0;
			if (flag4)
			{
				num = 360 - num;
			}
		}
		else
		{
			num = ((dy <= 0) ? 270 : 90);
		}
		return num;
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x0008708C File Offset: 0x0008528C
	public static int fixangle(int angle)
	{
		bool flag = angle >= 360;
		if (flag)
		{
			angle -= 360;
		}
		bool flag2 = angle < 0;
		if (flag2)
		{
			angle += 360;
		}
		return angle;
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void outz(string s)
	{
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void outz2(string s)
	{
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void onScreenDebug(string s)
	{
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void paintOnScreenDebug(mGraphics g)
	{
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void updateOnScreenDebug()
	{
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x000870CC File Offset: 0x000852CC
	public static string changeString(string str)
	{
		return str;
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00066794 File Offset: 0x00064994
	public static string replace(string _text, string _searchStr, string _replacementStr)
	{
		return _text.Replace(_searchStr, _replacementStr);
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x000870E0 File Offset: 0x000852E0
	public static int xetVX(int goc, int d)
	{
		return Res.cos(Res.fixangle(goc)) * d >> 10;
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x00087104 File Offset: 0x00085304
	public static int xetVY(int goc, int d)
	{
		return Res.sin(Res.fixangle(goc)) * d >> 10;
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x00087128 File Offset: 0x00085328
	public static int random(int a, int b)
	{
		bool flag = a == b;
		int result;
		if (flag)
		{
			result = a;
		}
		else
		{
			result = a + Res.r.nextInt(b - a);
		}
		return result;
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x00087158 File Offset: 0x00085358
	public static int random(int a)
	{
		return Res.r.nextInt(a);
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x00087178 File Offset: 0x00085378
	public static int random_Am_0(int a)
	{
		int num;
		for (num = 0; num == 0; num = Res.r.nextInt() % a)
		{
		}
		return num;
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x000871A8 File Offset: 0x000853A8
	public static int s2tick(int currentTimeMillis)
	{
		int num = currentTimeMillis * 16 / 1000;
		bool flag = currentTimeMillis * 16 % 1000 >= 5;
		if (flag)
		{
			num++;
		}
		return num;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x000871E4 File Offset: 0x000853E4
	public static int distance(int x1, int y1, int x2, int y2)
	{
		return Res.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x0008720C File Offset: 0x0008540C
	public static int sqrt(int a)
	{
		bool flag = a <= 0;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			int num = (a + 1) / 2;
			int num2;
			do
			{
				num2 = num;
				num = num / 2 + a / (2 * num);
			}
			while (global::Math.abs(num2 - num) > 1);
			result = num;
		}
		return result;
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x00087158 File Offset: 0x00085358
	public static int rnd(int a)
	{
		return Res.r.nextInt(a);
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x00011F50 File Offset: 0x00010150
	public static int abs(int i)
	{
		return (i <= 0) ? (-i) : i;
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x00087254 File Offset: 0x00085454
	public static bool inRect(int x1, int y1, int width, int height, int x2, int y2)
	{
		return x2 >= x1 && x2 <= x1 + width && y2 >= y1 && y2 <= y1 + height;
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x00087284 File Offset: 0x00085484
	public static string[] split(string original, string separator, int count)
	{
		int num = original.IndexOf(separator);
		bool flag = num >= 0;
		string[] array;
		if (flag)
		{
			array = Res.split(original.Substring(num + separator.Length), separator, count + 1);
		}
		else
		{
			array = new string[count + 1];
			num = original.Length;
		}
		array[count] = original.Substring(0, num);
		return array;
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x000872E4 File Offset: 0x000854E4
	public static string formatNumber(long number)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		text = string.Empty;
		bool flag = number >= 1000000000L;
		string result;
		if (flag)
		{
			text2 = mResources.billion;
			long num = number % 1000000000L / 100000000L;
			number /= 1000000000L;
			text = number + string.Empty;
			bool flag2 = num > 0L;
			if (flag2)
			{
				string text3 = text;
				result = string.Concat(new object[]
				{
					text3,
					",",
					num,
					text2
				});
			}
			else
			{
				result = text + text2;
			}
		}
		else
		{
			bool flag3 = number >= 1000000L;
			if (flag3)
			{
				text2 = mResources.million;
				long num2 = number % 1000000L / 100000L;
				number /= 1000000L;
				text = number + string.Empty;
				bool flag4 = num2 > 0L;
				if (flag4)
				{
					string text4 = text;
					result = string.Concat(new object[]
					{
						text4,
						",",
						num2,
						text2
					});
				}
				else
				{
					result = text + text2;
				}
			}
			else
			{
				result = number + string.Empty;
			}
		}
		return result;
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x00087430 File Offset: 0x00085630
	public static string formatNumber2(long number)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		text = string.Empty;
		bool flag = number >= 1000000000L;
		string result;
		if (flag)
		{
			text2 = mResources.billion;
			long num = number % 1000000000L / 10000000L;
			number /= 1000000000L;
			text = number + string.Empty;
			bool flag2 = num >= 10L;
			if (flag2)
			{
				bool flag3 = num % 10L == 0L;
				if (flag3)
				{
					num /= 10L;
				}
				string text3 = text;
				result = string.Concat(new object[]
				{
					text3,
					",",
					num,
					text2
				});
			}
			else
			{
				bool flag4 = num > 0L;
				if (flag4)
				{
					string text4 = text;
					result = string.Concat(new object[]
					{
						text4,
						",0",
						num,
						text2
					});
				}
				else
				{
					result = text + text2;
				}
			}
		}
		else
		{
			bool flag5 = number >= 1000000L;
			if (flag5)
			{
				text2 = mResources.million;
				long num2 = number % 1000000L / 10000L;
				number /= 1000000L;
				text = number + string.Empty;
				bool flag6 = num2 >= 10L;
				if (flag6)
				{
					bool flag7 = num2 % 10L == 0L;
					if (flag7)
					{
						num2 /= 10L;
					}
					string text5 = text;
					result = string.Concat(new object[]
					{
						text5,
						",",
						num2,
						text2
					});
				}
				else
				{
					bool flag8 = num2 > 0L;
					if (flag8)
					{
						string text6 = text;
						result = string.Concat(new object[]
						{
							text6,
							",0",
							num2,
							text2
						});
					}
					else
					{
						result = text + text2;
					}
				}
			}
			else
			{
				bool flag9 = number >= 10000L;
				if (flag9)
				{
					text2 = "k";
					long num3 = number % 1000L / 10L;
					number /= 1000L;
					text = number + string.Empty;
					bool flag10 = num3 >= 10L;
					if (flag10)
					{
						bool flag11 = num3 % 10L == 0L;
						if (flag11)
						{
							num3 /= 10L;
						}
						string text7 = text;
						result = string.Concat(new object[]
						{
							text7,
							",",
							num3,
							text2
						});
					}
					else
					{
						bool flag12 = num3 > 0L;
						if (flag12)
						{
							string text8 = text;
							result = string.Concat(new object[]
							{
								text8,
								",0",
								num3,
								text2
							});
						}
						else
						{
							result = text + text2;
						}
					}
				}
				else
				{
					result = number + string.Empty;
				}
			}
		}
		return result;
	}

	// Token: 0x04000FC0 RID: 4032
	private static short[] sinz = new short[]
	{
		0,
		18,
		36,
		54,
		71,
		89,
		107,
		125,
		143,
		160,
		178,
		195,
		213,
		230,
		248,
		265,
		282,
		299,
		316,
		333,
		350,
		367,
		384,
		400,
		416,
		433,
		449,
		465,
		481,
		496,
		512,
		527,
		543,
		558,
		573,
		587,
		602,
		616,
		630,
		644,
		658,
		672,
		685,
		698,
		711,
		724,
		737,
		749,
		761,
		773,
		784,
		796,
		807,
		818,
		828,
		839,
		849,
		859,
		868,
		878,
		887,
		896,
		904,
		912,
		920,
		928,
		935,
		943,
		949,
		956,
		962,
		968,
		974,
		979,
		984,
		989,
		994,
		998,
		1002,
		1005,
		1008,
		1011,
		1014,
		1016,
		1018,
		1020,
		1022,
		1023,
		1023,
		1024,
		1024
	};

	// Token: 0x04000FC1 RID: 4033
	private static short[] cosz;

	// Token: 0x04000FC2 RID: 4034
	private static int[] tanz;

	// Token: 0x04000FC3 RID: 4035
	public static int count;

	// Token: 0x04000FC4 RID: 4036
	public static bool isIcon;

	// Token: 0x04000FC5 RID: 4037
	public static bool isBig;

	// Token: 0x04000FC6 RID: 4038
	public static MyVector debug = new MyVector();

	// Token: 0x04000FC7 RID: 4039
	public static MyRandom r = new MyRandom();
}
