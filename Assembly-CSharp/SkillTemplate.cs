using System;

// Token: 0x020000A6 RID: 166
public class SkillTemplate
{
	// Token: 0x060008DD RID: 2269 RVA: 0x000920E0 File Offset: 0x000902E0
	public bool isBuffToPlayer()
	{
		return this.type == 2;
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00092108 File Offset: 0x00090308
	public bool isUseAlone()
	{
		return this.type == 3;
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x00092130 File Offset: 0x00090330
	public bool isAttackSkill()
	{
		return this.type == 1;
	}

	// Token: 0x040010A4 RID: 4260
	public sbyte id;

	// Token: 0x040010A5 RID: 4261
	public int classId;

	// Token: 0x040010A6 RID: 4262
	public string name;

	// Token: 0x040010A7 RID: 4263
	public int maxPoint;

	// Token: 0x040010A8 RID: 4264
	public int manaUseType;

	// Token: 0x040010A9 RID: 4265
	public int type;

	// Token: 0x040010AA RID: 4266
	public int iconId;

	// Token: 0x040010AB RID: 4267
	public string[] description;

	// Token: 0x040010AC RID: 4268
	public Skill[] skills;

	// Token: 0x040010AD RID: 4269
	public string damInfo;
}
