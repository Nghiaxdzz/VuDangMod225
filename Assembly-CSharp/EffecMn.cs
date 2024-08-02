using System;

// Token: 0x02000021 RID: 33
public class EffecMn
{
	// Token: 0x060001E2 RID: 482 RVA: 0x00003FAA File Offset: 0x000021AA
	public static void addEff(Effect me)
	{
		EffecMn.vEff.addElement(me);
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x000358B8 File Offset: 0x00033AB8
	public static void removeEff(int id)
	{
		bool flag = EffecMn.getEffById(id) != null;
		if (flag)
		{
			EffecMn.vEff.removeElement(EffecMn.getEffById(id));
		}
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x000358E8 File Offset: 0x00033AE8
	public static Effect getEffById(int id)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			Effect effect = (Effect)EffecMn.vEff.elementAt(i);
			bool flag = effect.effId == id;
			if (flag)
			{
				return effect;
			}
		}
		return null;
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x0003593C File Offset: 0x00033B3C
	public static void paintBackGroundUnderLayer(mGraphics g, int x, int y, int layer)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			bool flag = ((Effect)EffecMn.vEff.elementAt(i)).layer == -layer;
			if (flag)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paintUnderBackground(g, x, y);
			}
		}
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x000359A0 File Offset: 0x00033BA0
	public static void paintLayer1(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			bool flag = ((Effect)EffecMn.vEff.elementAt(i)).layer == 1;
			if (flag)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x00035A00 File Offset: 0x00033C00
	public static void paintLayer2(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			bool flag = ((Effect)EffecMn.vEff.elementAt(i)).layer == 2;
			if (flag)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x00035A60 File Offset: 0x00033C60
	public static void paintLayer3(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			bool flag = ((Effect)EffecMn.vEff.elementAt(i)).layer == 3;
			if (flag)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x00035AC0 File Offset: 0x00033CC0
	public static void paintLayer4(mGraphics g)
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			bool flag = ((Effect)EffecMn.vEff.elementAt(i)).layer == 4;
			if (flag)
			{
				((Effect)EffecMn.vEff.elementAt(i)).paint(g);
			}
		}
	}

	// Token: 0x060001EA RID: 490 RVA: 0x00035B20 File Offset: 0x00033D20
	public static void update()
	{
		for (int i = 0; i < EffecMn.vEff.size(); i++)
		{
			((Effect)EffecMn.vEff.elementAt(i)).update();
		}
	}

	// Token: 0x0400048F RID: 1167
	public static MyVector vEff = new MyVector();
}
