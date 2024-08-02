using System;

// Token: 0x02000024 RID: 36
public class EffectChar
{
	// Token: 0x060001FE RID: 510 RVA: 0x00004023 File Offset: 0x00002223
	public EffectChar(sbyte templateId, int timeStart, int timeLenght, short param)
	{
		this.template = EffectChar.effTemplates[(int)templateId];
		this.timeStart = timeStart;
		this.timeLenght = timeLenght / 1000;
		this.param = param;
	}

	// Token: 0x040004D2 RID: 1234
	public static EffectTemplate[] effTemplates;

	// Token: 0x040004D3 RID: 1235
	public static sbyte EFF_ME;

	// Token: 0x040004D4 RID: 1236
	public static sbyte EFF_FRIEND = 1;

	// Token: 0x040004D5 RID: 1237
	public int timeStart;

	// Token: 0x040004D6 RID: 1238
	public int timeLenght;

	// Token: 0x040004D7 RID: 1239
	public short param;

	// Token: 0x040004D8 RID: 1240
	public EffectTemplate template;
}
