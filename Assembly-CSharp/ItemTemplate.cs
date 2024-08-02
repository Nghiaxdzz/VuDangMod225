using System;

// Token: 0x02000056 RID: 86
public class ItemTemplate
{
	// Token: 0x0600043F RID: 1087 RVA: 0x000553A4 File Offset: 0x000535A4
	public ItemTemplate(short templateID, sbyte type, sbyte gender, string name, string description, sbyte level, int strRequire, short iconID, short part, bool isUpToUp)
	{
		this.id = templateID;
		this.type = type;
		this.gender = gender;
		this.name = name;
		this.name = Res.changeString(this.name);
		this.description = description;
		this.description = Res.changeString(this.description);
		this.level = level;
		this.strRequire = strRequire;
		this.iconID = iconID;
		this.part = part;
		this.isUpToUp = isUpToUp;
	}

	// Token: 0x04000939 RID: 2361
	public short id;

	// Token: 0x0400093A RID: 2362
	public sbyte type;

	// Token: 0x0400093B RID: 2363
	public sbyte gender;

	// Token: 0x0400093C RID: 2364
	public string name;

	// Token: 0x0400093D RID: 2365
	public string[] subName;

	// Token: 0x0400093E RID: 2366
	public string description;

	// Token: 0x0400093F RID: 2367
	public sbyte level;

	// Token: 0x04000940 RID: 2368
	public short iconID;

	// Token: 0x04000941 RID: 2369
	public short part;

	// Token: 0x04000942 RID: 2370
	public bool isUpToUp;

	// Token: 0x04000943 RID: 2371
	public int w;

	// Token: 0x04000944 RID: 2372
	public int h;

	// Token: 0x04000945 RID: 2373
	public int strRequire;
}
