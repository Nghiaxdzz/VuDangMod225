using System;

// Token: 0x02000054 RID: 84
public class ItemOption
{
	// Token: 0x06000439 RID: 1081 RVA: 0x00004B68 File Offset: 0x00002D68
	public ItemOption()
	{
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x000552B4 File Offset: 0x000534B4
	public ItemOption(int optionTemplateId, int param)
	{
		bool flag = optionTemplateId == 22;
		if (flag)
		{
			optionTemplateId = 6;
			param *= 1000;
		}
		bool flag2 = optionTemplateId == 23;
		if (flag2)
		{
			optionTemplateId = 7;
			param *= 1000;
		}
		this.param = param;
		this.optionTemplate = GameScr.gI().iOptionTemplates[optionTemplateId];
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x00055310 File Offset: 0x00053510
	public string getOptionString()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "#", this.param + string.Empty);
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x0005534C File Offset: 0x0005354C
	public string getOptionName()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "+#", string.Empty);
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x00055378 File Offset: 0x00053578
	public string getOptiongColor()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "$", string.Empty);
	}

	// Token: 0x04000932 RID: 2354
	public int param;

	// Token: 0x04000933 RID: 2355
	public sbyte active;

	// Token: 0x04000934 RID: 2356
	public sbyte activeCard;

	// Token: 0x04000935 RID: 2357
	public ItemOptionTemplate optionTemplate;
}
