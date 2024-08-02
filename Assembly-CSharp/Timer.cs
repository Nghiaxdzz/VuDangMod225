using System;

// Token: 0x020000BD RID: 189
public class Timer
{
	// Token: 0x060009C4 RID: 2500 RVA: 0x000064A1 File Offset: 0x000046A1
	public static void setTimer(IActionListener actionListener, int action, long timeEllapse)
	{
		Timer.timeListener = actionListener;
		Timer.idAction = action;
		Timer.timeExecute = mSystem.currentTimeMillis() + timeEllapse;
		Timer.isON = true;
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x0009D5BC File Offset: 0x0009B7BC
	public static void update()
	{
		long num = mSystem.currentTimeMillis();
		bool flag = !Timer.isON || num <= Timer.timeExecute;
		if (!flag)
		{
			Timer.isON = false;
			try
			{
				bool flag2 = Timer.idAction > 0;
				if (flag2)
				{
					GameScr.gI().actionPerform(Timer.idAction, null);
				}
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x04001252 RID: 4690
	public static IActionListener timeListener;

	// Token: 0x04001253 RID: 4691
	public static int idAction;

	// Token: 0x04001254 RID: 4692
	public static long timeExecute;

	// Token: 0x04001255 RID: 4693
	public static bool isON;
}
