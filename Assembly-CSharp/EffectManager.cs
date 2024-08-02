using System;

// Token: 0x02000029 RID: 41
public class EffectManager : MyVector
{
	// Token: 0x06000211 RID: 529 RVA: 0x00037908 File Offset: 0x00035B08
	public void updateAll()
	{
		for (int i = base.size() - 1; i >= 0; i--)
		{
			Effect_End effect_End = (Effect_End)base.elementAt(i);
			bool flag = effect_End != null;
			if (flag)
			{
				effect_End.update();
				bool isRemove = effect_End.isRemove;
				if (isRemove)
				{
					base.removeElementAt(i);
				}
			}
		}
	}

	// Token: 0x06000212 RID: 530 RVA: 0x00004087 File Offset: 0x00002287
	public static void update()
	{
		EffectManager.hiEffects.updateAll();
		EffectManager.midEffects.updateAll();
		EffectManager.lowEffects.updateAll();
	}

	// Token: 0x06000213 RID: 531 RVA: 0x00037968 File Offset: 0x00035B68
	public void paintAll(mGraphics g)
	{
		for (int i = 0; i < base.size(); i++)
		{
			Effect_End effect_End = (Effect_End)base.elementAt(i);
			bool flag = effect_End != null && !effect_End.isRemove;
			if (flag)
			{
				((Effect_End)base.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x06000214 RID: 532 RVA: 0x000379C4 File Offset: 0x00035BC4
	public void removeAll()
	{
		for (int i = base.size() - 1; i >= 0; i--)
		{
			Effect_End effect_End = (Effect_End)base.elementAt(i);
			bool flag = effect_End != null;
			if (flag)
			{
				effect_End.isRemove = true;
				base.removeElementAt(i);
			}
		}
	}

	// Token: 0x06000215 RID: 533 RVA: 0x000040AB File Offset: 0x000022AB
	public static void addHiEffect(Effect_End eff)
	{
		EffectManager.hiEffects.addElement(eff);
	}

	// Token: 0x06000216 RID: 534 RVA: 0x000040BA File Offset: 0x000022BA
	public static void removeHiEffect(Effect_End eff)
	{
		EffectManager.hiEffects.removeElement(eff);
	}

	// Token: 0x06000217 RID: 535 RVA: 0x000040C9 File Offset: 0x000022C9
	public static void addMidEffects(Effect_End eff)
	{
		EffectManager.midEffects.addElement(eff);
	}

	// Token: 0x06000218 RID: 536 RVA: 0x000040D8 File Offset: 0x000022D8
	public static void removeMidEffects(Effect_End eff)
	{
		EffectManager.midEffects.removeElement(eff);
	}

	// Token: 0x06000219 RID: 537 RVA: 0x000040E7 File Offset: 0x000022E7
	public static void addLowEffect(Effect_End eff)
	{
		EffectManager.lowEffects.addElement(eff);
	}

	// Token: 0x0600021A RID: 538 RVA: 0x000040F6 File Offset: 0x000022F6
	public static void removeLowEffect(Effect_End eff)
	{
		EffectManager.lowEffects.removeElement(eff);
	}

	// Token: 0x040004ED RID: 1261
	public static EffectManager lowEffects = new EffectManager();

	// Token: 0x040004EE RID: 1262
	public static EffectManager midEffects = new EffectManager();

	// Token: 0x040004EF RID: 1263
	public static EffectManager hiEffects = new EffectManager();
}
