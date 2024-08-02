using System;
using System.Threading;
using UnityEngine;

// Token: 0x020000A9 RID: 169
public class SMS
{
	// Token: 0x060008EF RID: 2287 RVA: 0x00092894 File Offset: 0x00090A94
	public static int send(string content, string to)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		int result;
		if (flag)
		{
			result = SMS.__send(content, to);
		}
		else
		{
			result = SMS._send(content, to);
		}
		return result;
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x000928D0 File Offset: 0x00090AD0
	private static int _send(string content, string to)
	{
		bool flag = SMS.status != 0;
		if (flag)
		{
			for (int i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = SMS.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = SMS.status != 0;
			if (flag3)
			{
				Cout.LogError("CANNOT SEND SMS " + content + " WHEN SENDING " + SMS._content);
				return -1;
			}
		}
		SMS._content = content;
		SMS._to = to;
		SMS._result = -1;
		SMS.status = 2;
		int j;
		for (j = 0; j < 500; j++)
		{
			Thread.Sleep(5);
			bool flag4 = SMS.status == 0;
			if (flag4)
			{
				break;
			}
		}
		bool flag5 = j == 500;
		if (flag5)
		{
			Debug.LogError("TOO LONG FOR SEND SMS " + content);
			SMS.status = 0;
		}
		else
		{
			Debug.Log(string.Concat(new object[]
			{
				"Send SMS ",
				content,
				" done in ",
				j * 5,
				"ms"
			}));
		}
		return SMS._result;
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x00092A04 File Offset: 0x00090C04
	private static int __send(string content, string to)
	{
		int num = iOSPlugins.Check();
		Cout.println("vao sms ko " + num);
		bool flag = num >= 0;
		if (flag)
		{
			SMS.f = true;
			SMS.sendEnable = true;
			iOSPlugins.SMSsend(to, content, num);
			Screen.orientation = ScreenOrientation.AutoRotation;
		}
		return num;
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x00092A5C File Offset: 0x00090C5C
	public static void update()
	{
		float num = Time.time;
		bool flag = num - (float)SMS.time > 1f;
		if (flag)
		{
			SMS.time++;
		}
		bool flag2 = SMS.f;
		if (flag2)
		{
			SMS.OnSMS();
		}
		bool flag3 = SMS.status == 2;
		if (flag3)
		{
			SMS.status = 1;
			try
			{
				SMS._result = SMS.__send(SMS._content, SMS._to);
			}
			catch (Exception)
			{
				Debug.Log("CANNOT SEND SMS");
			}
			SMS.status = 0;
		}
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x00092AF4 File Offset: 0x00090CF4
	private static void OnSMS()
	{
		bool flag = SMS.sendEnable;
		if (flag)
		{
			bool flag2 = iOSPlugins.checkRotation() == 1;
			if (flag2)
			{
				Screen.orientation = ScreenOrientation.LandscapeLeft;
			}
			else
			{
				bool flag3 = iOSPlugins.checkRotation() == -1;
				if (flag3)
				{
					Screen.orientation = ScreenOrientation.Portrait;
				}
				else
				{
					bool flag4 = iOSPlugins.checkRotation() == 0;
					if (flag4)
					{
						Screen.orientation = ScreenOrientation.AutoRotation;
					}
					else
					{
						bool flag5 = iOSPlugins.checkRotation() == 2;
						if (flag5)
						{
							Screen.orientation = ScreenOrientation.LandscapeRight;
						}
						else
						{
							bool flag6 = iOSPlugins.checkRotation() == 3;
							if (flag6)
							{
								Screen.orientation = ScreenOrientation.PortraitUpsideDown;
							}
						}
					}
				}
			}
			bool flag7 = SMS.time0 < 5;
			if (flag7)
			{
				SMS.time0++;
			}
			else
			{
				iOSPlugins.Send();
				SMS.sendEnable = false;
				SMS.time0 = 0;
			}
		}
		bool flag8 = iOSPlugins.unpause() == 1;
		if (flag8)
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			bool flag9 = SMS.time0 < 5;
			if (flag9)
			{
				SMS.time0++;
			}
			else
			{
				SMS.f = false;
				iOSPlugins.back();
				SMS.time0 = 0;
			}
		}
	}

	// Token: 0x040010BA RID: 4282
	private const int INTERVAL = 5;

	// Token: 0x040010BB RID: 4283
	private const int MAXTIME = 500;

	// Token: 0x040010BC RID: 4284
	private static int status;

	// Token: 0x040010BD RID: 4285
	private static int _result;

	// Token: 0x040010BE RID: 4286
	private static string _to;

	// Token: 0x040010BF RID: 4287
	private static string _content;

	// Token: 0x040010C0 RID: 4288
	private static bool f;

	// Token: 0x040010C1 RID: 4289
	private static int time;

	// Token: 0x040010C2 RID: 4290
	public static bool sendEnable;

	// Token: 0x040010C3 RID: 4291
	private static int time0;
}
