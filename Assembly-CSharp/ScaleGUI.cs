using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000090 RID: 144
public class ScaleGUI
{
	// Token: 0x060007BD RID: 1981 RVA: 0x00087D10 File Offset: 0x00085F10
	public static void initScaleGUI()
	{
		Cout.println(string.Concat(new object[]
		{
			"Init Scale GUI: Screen.w=",
			Screen.width,
			" Screen.h=",
			Screen.height
		}));
		ScaleGUI.WIDTH = (float)Screen.width;
		ScaleGUI.HEIGHT = (float)Screen.height;
		ScaleGUI.scaleScreen = false;
		bool flag = Screen.width <= 1200;
		if (flag)
		{
		}
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x00087D8C File Offset: 0x00085F8C
	public static void BeginGUI()
	{
		bool flag = ScaleGUI.scaleScreen;
		if (flag)
		{
			ScaleGUI.stack.Add(GUI.matrix);
			Matrix4x4 rhs = default(Matrix4x4);
			float num = (float)Screen.width;
			float num2 = (float)Screen.height;
			float num3 = num / num2;
			Vector3 zero = Vector3.zero;
			float d = (num3 >= ScaleGUI.WIDTH / ScaleGUI.HEIGHT) ? ((float)Screen.height / ScaleGUI.HEIGHT) : ((float)Screen.width / ScaleGUI.WIDTH);
			rhs.SetTRS(zero, Quaternion.identity, Vector3.one * d);
			GUI.matrix *= rhs;
		}
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x00087E38 File Offset: 0x00086038
	public static void EndGUI()
	{
		bool flag = ScaleGUI.scaleScreen;
		if (flag)
		{
			GUI.matrix = ScaleGUI.stack[ScaleGUI.stack.Count - 1];
			ScaleGUI.stack.RemoveAt(ScaleGUI.stack.Count - 1);
		}
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x00087E84 File Offset: 0x00086084
	public static float scaleX(float x)
	{
		bool flag = !ScaleGUI.scaleScreen;
		float result;
		if (flag)
		{
			result = x;
		}
		else
		{
			x = x * ScaleGUI.WIDTH / (float)Screen.width;
			result = x;
		}
		return result;
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x00087EB8 File Offset: 0x000860B8
	public static float scaleY(float y)
	{
		bool flag = !ScaleGUI.scaleScreen;
		float result;
		if (flag)
		{
			result = y;
		}
		else
		{
			y = y * ScaleGUI.HEIGHT / (float)Screen.height;
			result = y;
		}
		return result;
	}

	// Token: 0x04000FCD RID: 4045
	public static bool scaleScreen;

	// Token: 0x04000FCE RID: 4046
	public static float WIDTH;

	// Token: 0x04000FCF RID: 4047
	public static float HEIGHT;

	// Token: 0x04000FD0 RID: 4048
	private static List<Matrix4x4> stack = new List<Matrix4x4>();
}
