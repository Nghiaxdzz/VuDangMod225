using System;

// Token: 0x020000A2 RID: 162
public class SkillOption
{
	// Token: 0x060008D5 RID: 2261 RVA: 0x00092060 File Offset: 0x00090260
	public string getOptionString()
	{
		bool flag = this.optionString == null;
		if (flag)
		{
			this.optionString = NinjaUtil.replace(this.optionTemplate.name, "#", string.Empty + this.param);
		}
		return this.optionString;
	}

	// Token: 0x04001099 RID: 4249
	public int param;

	// Token: 0x0400109A RID: 4250
	public SkillOptionTemplate optionTemplate;

	// Token: 0x0400109B RID: 4251
	public string optionString;
}
