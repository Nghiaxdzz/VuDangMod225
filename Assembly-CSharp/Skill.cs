using System;

// Token: 0x020000A0 RID: 160
public class Skill
{
	// Token: 0x060008D1 RID: 2257 RVA: 0x00091F28 File Offset: 0x00090128
	public string strTimeReplay()
	{
		bool flag = this.coolDown % 1000 == 0;
		string result;
		if (flag)
		{
			result = this.coolDown / 1000 + string.Empty;
		}
		else
		{
			int num = this.coolDown % 1000;
			result = this.coolDown / 1000 + "." + ((num % 100 != 0) ? (num / 10) : (num / 100));
		}
		return result;
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x00091FAC File Offset: 0x000901AC
	public void paint(int x, int y, mGraphics g)
	{
		SmallImage.drawSmallImage(g, this.template.iconId, x, y, 0, StaticObj.VCENTER_HCENTER);
		long num = mSystem.currentTimeMillis();
		long num2 = num - this.lastTimeUseThisSkill;
		bool flag = num2 < (long)this.coolDown;
		if (flag)
		{
			g.setColor(2721889, 0.7f);
			bool flag2 = this.paintCanNotUseSkill && GameCanvas.gameTick % 6 > 2;
			if (flag2)
			{
				g.setColor(876862);
			}
			int num3 = (int)(num2 * 20L / (long)this.coolDown);
			g.fillRect(x - 10, y - 10 + num3, 20, 20 - num3);
		}
		else
		{
			this.paintCanNotUseSkill = false;
		}
	}

	// Token: 0x04001076 RID: 4214
	public const sbyte ATT_STAND = 0;

	// Token: 0x04001077 RID: 4215
	public const sbyte ATT_FLY = 1;

	// Token: 0x04001078 RID: 4216
	public const sbyte SKILL_AUTO_USE = 0;

	// Token: 0x04001079 RID: 4217
	public const sbyte SKILL_CLICK_USE_ATTACK = 1;

	// Token: 0x0400107A RID: 4218
	public const sbyte SKILL_CLICK_USE_BUFF = 2;

	// Token: 0x0400107B RID: 4219
	public const sbyte SKILL_CLICK_NPC = 3;

	// Token: 0x0400107C RID: 4220
	public const sbyte SKILL_CLICK_LIVE = 4;

	// Token: 0x0400107D RID: 4221
	public SkillTemplate template;

	// Token: 0x0400107E RID: 4222
	public short skillId;

	// Token: 0x0400107F RID: 4223
	public int point;

	// Token: 0x04001080 RID: 4224
	public long powRequire;

	// Token: 0x04001081 RID: 4225
	public int coolDown;

	// Token: 0x04001082 RID: 4226
	public long lastTimeUseThisSkill;

	// Token: 0x04001083 RID: 4227
	public int dx;

	// Token: 0x04001084 RID: 4228
	public int dy;

	// Token: 0x04001085 RID: 4229
	public int maxFight;

	// Token: 0x04001086 RID: 4230
	public int manaUse;

	// Token: 0x04001087 RID: 4231
	public SkillOption[] options;

	// Token: 0x04001088 RID: 4232
	public bool paintCanNotUseSkill;

	// Token: 0x04001089 RID: 4233
	public short damage;

	// Token: 0x0400108A RID: 4234
	public string moreInfo;

	// Token: 0x0400108B RID: 4235
	public short price;
}
