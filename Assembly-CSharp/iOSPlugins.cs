using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x0200004B RID: 75
public class iOSPlugins
{
	// Token: 0x060003D3 RID: 979
	[DllImport("__Internal")]
	private static extern void _SMSsend(string tophone, string withtext, int n);

	// Token: 0x060003D4 RID: 980
	[DllImport("__Internal")]
	private static extern int _unpause();

	// Token: 0x060003D5 RID: 981
	[DllImport("__Internal")]
	private static extern int _checkRotation();

	// Token: 0x060003D6 RID: 982
	[DllImport("__Internal")]
	private static extern int _back();

	// Token: 0x060003D7 RID: 983
	[DllImport("__Internal")]
	private static extern int _Send();

	// Token: 0x060003D8 RID: 984
	[DllImport("__Internal")]
	private static extern void _purchaseItem(string itemID, string userName, string gameID);

	// Token: 0x060003D9 RID: 985 RVA: 0x00053850 File Offset: 0x00051A50
	public static int Check()
	{
		bool flag = Application.platform == RuntimePlatform.IPhonePlayer;
		int result;
		if (flag)
		{
			result = iOSPlugins.checkCanSendSMS();
		}
		else
		{
			iOSPlugins.devide = iPhoneSettings.generation.ToString();
			string a = string.Empty + iOSPlugins.devide[2].ToString();
			bool flag2 = a == "h" && iOSPlugins.devide.Length > 6;
			if (flag2)
			{
				iOSPlugins.Myname = SystemInfo.operatingSystem.ToString();
				string a2 = string.Empty + iOSPlugins.Myname[10].ToString();
				bool flag3 = a2 != "2" && a2 != "3";
				if (flag3)
				{
					result = 0;
				}
				else
				{
					result = 1;
				}
			}
			else
			{
				Cout.println(iOSPlugins.devide + "  loai");
				bool flag4 = iOSPlugins.devide == "Unknown" && ScaleGUI.WIDTH * ScaleGUI.HEIGHT < 786432f;
				if (flag4)
				{
					result = 0;
				}
				else
				{
					result = -1;
				}
			}
		}
		return result;
	}

	// Token: 0x060003DA RID: 986 RVA: 0x00053974 File Offset: 0x00051B74
	public static int checkCanSendSMS()
	{
		bool flag = iPhoneSettings.generation == iPhoneGeneration.iPhone3GS || iPhoneSettings.generation == iPhoneGeneration.iPhone4 || iPhoneSettings.generation == iPhoneGeneration.iPhone4S || iPhoneSettings.generation == iPhoneGeneration.iPhone5;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			result = -1;
		}
		return result;
	}

	// Token: 0x060003DB RID: 987 RVA: 0x000539B8 File Offset: 0x00051BB8
	public static void SMSsend(string phonenumber, string bodytext, int n)
	{
		bool flag = Application.platform > RuntimePlatform.OSXEditor;
		if (flag)
		{
			iOSPlugins._SMSsend(phonenumber, bodytext, n);
		}
	}

	// Token: 0x060003DC RID: 988 RVA: 0x000539E0 File Offset: 0x00051BE0
	public static void back()
	{
		bool flag = Application.platform > RuntimePlatform.OSXEditor;
		if (flag)
		{
			iOSPlugins._back();
		}
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00053A04 File Offset: 0x00051C04
	public static void Send()
	{
		bool flag = Application.platform > RuntimePlatform.OSXEditor;
		if (flag)
		{
			iOSPlugins._Send();
		}
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00053A28 File Offset: 0x00051C28
	public static int unpause()
	{
		bool flag = Application.platform > RuntimePlatform.OSXEditor;
		int result;
		if (flag)
		{
			result = iOSPlugins._unpause();
		}
		else
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00053A50 File Offset: 0x00051C50
	public static int checkRotation()
	{
		bool flag = Application.platform > RuntimePlatform.OSXEditor;
		int result;
		if (flag)
		{
			result = iOSPlugins._checkRotation();
		}
		else
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x00053A78 File Offset: 0x00051C78
	public static void purchaseItem(string itemID, string userName, string gameID)
	{
		bool flag = Application.platform > RuntimePlatform.OSXEditor;
		if (flag)
		{
			iOSPlugins._purchaseItem(itemID, userName, gameID);
		}
	}

	// Token: 0x04000888 RID: 2184
	public static string devide;

	// Token: 0x04000889 RID: 2185
	public static string Myname;
}
