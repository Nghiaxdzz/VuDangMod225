using System;

// Token: 0x0200002A RID: 42
public class EffectPaint
{
	// Token: 0x0600021D RID: 541 RVA: 0x00037A14 File Offset: 0x00035C14
	public int getImgId()
	{
		return this.effCharPaint.arrEfInfo[this.index].idImg;
	}

	// Token: 0x040004F0 RID: 1264
	public int index;

	// Token: 0x040004F1 RID: 1265
	public Mob eMob;

	// Token: 0x040004F2 RID: 1266
	public global::Char eChar;

	// Token: 0x040004F3 RID: 1267
	public EffectCharPaint effCharPaint;

	// Token: 0x040004F4 RID: 1268
	public bool isFly;
}
