using System;

// Token: 0x020000B6 RID: 182
public class Task
{
	// Token: 0x0600096D RID: 2413 RVA: 0x000991BC File Offset: 0x000973BC
	public Task(short taskId, sbyte index, string name, string detail, string[] subNames, short[] counts, short count, string[] contentInfo)
	{
		this.taskId = taskId;
		this.index = (int)index;
		this.names = mFont.tahoma_7b_green2.splitFontArray(name, Panel.WIDTH_PANEL - 20);
		this.details = mFont.tahoma_7.splitFontArray(detail, Panel.WIDTH_PANEL - 20);
		this.subNames = subNames;
		this.counts = counts;
		this.count = count;
		this.contentInfo = contentInfo;
	}

	// Token: 0x04001171 RID: 4465
	public int index;

	// Token: 0x04001172 RID: 4466
	public int max;

	// Token: 0x04001173 RID: 4467
	public short[] counts;

	// Token: 0x04001174 RID: 4468
	public short taskId;

	// Token: 0x04001175 RID: 4469
	public string[] names;

	// Token: 0x04001176 RID: 4470
	public string[] details;

	// Token: 0x04001177 RID: 4471
	public string[] subNames;

	// Token: 0x04001178 RID: 4472
	public string[] contentInfo;

	// Token: 0x04001179 RID: 4473
	public short count;
}
