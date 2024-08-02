using System;

// Token: 0x020000BF RID: 191
public class TouchScreenKeyboard
{
	// Token: 0x060009C8 RID: 2504 RVA: 0x0009D62C File Offset: 0x0009B82C
	public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType t, bool b1, bool b2, bool type, bool b3, string caption)
	{
		return null;
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void Clear()
	{
	}

	// Token: 0x0400125F RID: 4703
	public static bool hideInput;

	// Token: 0x04001260 RID: 4704
	public static bool visible;

	// Token: 0x04001261 RID: 4705
	public bool done;

	// Token: 0x04001262 RID: 4706
	public bool active;

	// Token: 0x04001263 RID: 4707
	public string text;
}
