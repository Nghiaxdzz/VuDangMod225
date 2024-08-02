using System;

// Token: 0x0200004F RID: 79
public class ipKeyboard
{
	// Token: 0x06000407 RID: 1031 RVA: 0x00053AB8 File Offset: 0x00051CB8
	public static void openKeyBoard(string caption, int type, string text, Command action)
	{
		ipKeyboard.act = action;
		TouchScreenKeyboardType t = (type == 0 || type == 2) ? TouchScreenKeyboardType.ASCIICapable : TouchScreenKeyboardType.NumberPad;
		TouchScreenKeyboard.hideInput = false;
		ipKeyboard.tk = TouchScreenKeyboard.Open(text, t, false, false, type == 2, false, caption);
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x00053AF4 File Offset: 0x00051CF4
	public static void update()
	{
		try
		{
			bool flag = ipKeyboard.tk != null && ipKeyboard.tk.done;
			if (flag)
			{
				bool flag2 = ipKeyboard.act != null;
				if (flag2)
				{
					ipKeyboard.act.perform(ipKeyboard.tk.text);
				}
				ipKeyboard.tk.text = string.Empty;
				ipKeyboard.tk = null;
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x040008A4 RID: 2212
	private static TouchScreenKeyboard tk;

	// Token: 0x040008A5 RID: 2213
	public static int TEXT;

	// Token: 0x040008A6 RID: 2214
	public static int NUMBERIC = 1;

	// Token: 0x040008A7 RID: 2215
	public static int PASS = 2;

	// Token: 0x040008A8 RID: 2216
	private static Command act;
}
