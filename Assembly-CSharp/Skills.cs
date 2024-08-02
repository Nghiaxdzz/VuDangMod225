using System;

// Token: 0x020000A5 RID: 165
public class Skills
{
	// Token: 0x060008D9 RID: 2265 RVA: 0x00005E8D File Offset: 0x0000408D
	public static void add(Skill skill)
	{
		Skills.skills.put(skill.skillId, skill);
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x000920B8 File Offset: 0x000902B8
	public static Skill get(short skillId)
	{
		return (Skill)Skills.skills.get(skillId);
	}

	// Token: 0x040010A3 RID: 4259
	public static MyHashTable skills = new MyHashTable();
}
