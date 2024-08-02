using System;

// Token: 0x02000023 RID: 35
public abstract class Effect2
{
	// Token: 0x060001FA RID: 506 RVA: 0x00003A0D File Offset: 0x00001C0D
	public virtual void update()
	{
	}

	// Token: 0x060001FB RID: 507 RVA: 0x00003A0D File Offset: 0x00001C0D
	public virtual void paint(mGraphics g)
	{
	}

	// Token: 0x040004CC RID: 1228
	public static MyVector vEffect3 = new MyVector();

	// Token: 0x040004CD RID: 1229
	public static MyVector vEffect2 = new MyVector();

	// Token: 0x040004CE RID: 1230
	public static MyVector vRemoveEffect2 = new MyVector();

	// Token: 0x040004CF RID: 1231
	public static MyVector vEffect2Outside = new MyVector();

	// Token: 0x040004D0 RID: 1232
	public static MyVector vAnimateEffect = new MyVector();

	// Token: 0x040004D1 RID: 1233
	public static MyVector vEffectFeet = new MyVector();
}
