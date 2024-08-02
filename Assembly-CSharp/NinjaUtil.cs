using System;

// Token: 0x0200007F RID: 127
public class NinjaUtil
{
	// Token: 0x06000622 RID: 1570 RVA: 0x00004A81 File Offset: 0x00002C81
	public static void onLoadMapComplete()
	{
		GameCanvas.endDlg();
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x00005455 File Offset: 0x00003655
	public void onLoading()
	{
		GameCanvas.startWaitDlg(mResources.downloading_data);
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x000666C8 File Offset: 0x000648C8
	public static int randomNumber(int max)
	{
		MyRandom myRandom = new MyRandom();
		return myRandom.nextInt(max);
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x000666E8 File Offset: 0x000648E8
	public static sbyte[] readByteArray(Message msg)
	{
		try
		{
			int num = msg.reader().readInt();
			sbyte[] result = new sbyte[num];
			msg.reader().read(ref result);
			return result;
		}
		catch (Exception)
		{
			Cout.LogError("LOI DOC readByteArray NINJAUTIL");
		}
		return null;
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x00066744 File Offset: 0x00064944
	public static sbyte[] readByteArray(myReader dos)
	{
		try
		{
			int num = dos.readInt();
			sbyte[] result = new sbyte[num];
			dos.read(ref result);
			return result;
		}
		catch (Exception)
		{
			Cout.LogError("LOI DOC readByteArray dos  NINJAUTIL");
		}
		return null;
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x00066794 File Offset: 0x00064994
	public static string replace(string text, string regex, string replacement)
	{
		return text.Replace(regex, replacement);
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x000667B0 File Offset: 0x000649B0
	public static string numberTostring(string number)
	{
		string text = string.Empty;
		string str = string.Empty;
		bool flag = number.Equals(string.Empty);
		string result;
		if (flag)
		{
			result = text;
		}
		else
		{
			bool flag2 = number[0] == '-';
			if (flag2)
			{
				str = "-";
				number = number.Substring(1);
			}
			for (int i = number.Length - 1; i >= 0; i--)
			{
				text = (((number.Length - 1 - i) % 3 != 0 || number.Length - 1 - i <= 0) ? (number[i].ToString() + text) : (number[i].ToString() + "." + text));
			}
			result = str + text;
		}
		return result;
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x00066884 File Offset: 0x00064A84
	public static string getDate(int second)
	{
		long num = (long)second * 1000L;
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Add(new TimeSpan(num * 10000L)).ToUniversalTime();
		int hour = dateTime.Hour;
		int minute = dateTime.Minute;
		int day = dateTime.Day;
		int month = dateTime.Month;
		int year = dateTime.Year;
		return string.Concat(new object[]
		{
			day,
			"/",
			month,
			"/",
			year,
			" ",
			hour,
			"h"
		});
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x00066950 File Offset: 0x00064B50
	public static string getDate2(long second)
	{
		long num = second + 25200000L;
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Add(new TimeSpan(num * 10000L)).ToUniversalTime();
		int hour = dateTime.Hour;
		int minute = dateTime.Minute;
		return string.Concat(new object[]
		{
			hour,
			"h",
			minute,
			"m"
		});
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x000669DC File Offset: 0x00064BDC
	public static string getTime(int timeRemainS)
	{
		int num = 0;
		bool flag = timeRemainS > 60;
		if (flag)
		{
			num = timeRemainS / 60;
			timeRemainS %= 60;
		}
		int num2 = 0;
		bool flag2 = num > 60;
		if (flag2)
		{
			num2 = num / 60;
			num %= 60;
		}
		int num3 = 0;
		bool flag3 = num2 > 24;
		if (flag3)
		{
			num3 = num2 / 24;
			num2 %= 24;
		}
		string text = string.Empty;
		bool flag4 = num3 > 0;
		string result;
		if (flag4)
		{
			text += num3;
			text += "d";
			result = text + num2 + "h";
		}
		else
		{
			bool flag5 = num2 > 0;
			if (flag5)
			{
				text += num2;
				text += "h";
				result = text + num + "'";
			}
			else
			{
				text = ((num <= 9) ? (text + "0" + num) : (text + num));
				text += ":";
				bool flag6 = timeRemainS > 9;
				if (flag6)
				{
					result = text + timeRemainS;
				}
				else
				{
					result = text + "0" + timeRemainS;
				}
			}
		}
		return result;
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x00066B18 File Offset: 0x00064D18
	public static string getMoneys(long m)
	{
		string text = string.Empty;
		long num = m / 1000L + 1L;
		int num2 = 0;
		while ((long)num2 < num)
		{
			bool flag = m >= 1000L;
			if (!flag)
			{
				text = m + text;
				break;
			}
			long num3 = m % 1000L;
			text = ((num3 != 0L) ? ((num3 >= 10L) ? ((num3 >= 100L) ? ("." + num3 + text) : (".0" + num3 + text)) : (".00" + num3 + text)) : (".000" + text));
			m /= 1000L;
			num2++;
		}
		return text;
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x00066BEC File Offset: 0x00064DEC
	public static string getTimeAgo(int timeRemainS)
	{
		int num = 0;
		bool flag = timeRemainS > 60;
		if (flag)
		{
			num = timeRemainS / 60;
			timeRemainS %= 60;
		}
		int num2 = 0;
		bool flag2 = num > 60;
		if (flag2)
		{
			num2 = num / 60;
			num %= 60;
		}
		int num3 = 0;
		bool flag3 = num2 > 24;
		if (flag3)
		{
			num3 = num2 / 24;
			num2 %= 24;
		}
		string text = string.Empty;
		bool flag4 = num3 > 0;
		string result;
		if (flag4)
		{
			text += num3;
			text += "d";
			result = text + num2 + "h";
		}
		else
		{
			bool flag5 = num2 > 0;
			if (flag5)
			{
				text += num2;
				text += "h";
				result = text + num + "'";
			}
			else
			{
				bool flag6 = num == 0;
				if (flag6)
				{
					num = 1;
				}
				text += num;
				result = text + "ph";
			}
		}
		return result;
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x00066CEC File Offset: 0x00064EEC
	public static string[] split(string original, string separator)
	{
		MyVector myVector = new MyVector();
		for (int i = original.IndexOf(separator); i >= 0; i = original.IndexOf(separator))
		{
			myVector.addElement(original.Substring(0, i));
			original = original.Substring(i + separator.Length);
		}
		myVector.addElement(original);
		string[] array = new string[myVector.size()];
		bool flag = myVector.size() > 0;
		if (flag)
		{
			for (int j = 0; j < myVector.size(); j++)
			{
				array[j] = (string)myVector.elementAt(j);
			}
		}
		return array;
	}
}
