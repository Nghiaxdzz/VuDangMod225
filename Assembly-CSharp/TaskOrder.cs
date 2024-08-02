using System;

// Token: 0x020000B7 RID: 183
public class TaskOrder
{
	// Token: 0x0600096E RID: 2414 RVA: 0x0000638A File Offset: 0x0000458A
	public TaskOrder(sbyte taskId, short count, short maxCount, string name, string description, sbyte killId, sbyte mapId)
	{
		this.count = (int)count;
		this.maxCount = maxCount;
		this.taskId = (int)taskId;
		this.name = name;
		this.description = description;
		this.killId = (int)killId;
		this.mapId = (int)mapId;
	}

	// Token: 0x0400117A RID: 4474
	public const sbyte TASK_DAY = 0;

	// Token: 0x0400117B RID: 4475
	public const sbyte TASK_BOSS = 1;

	// Token: 0x0400117C RID: 4476
	public int taskId;

	// Token: 0x0400117D RID: 4477
	public int count;

	// Token: 0x0400117E RID: 4478
	public short maxCount;

	// Token: 0x0400117F RID: 4479
	public string name;

	// Token: 0x04001180 RID: 4480
	public string description;

	// Token: 0x04001181 RID: 4481
	public int killId;

	// Token: 0x04001182 RID: 4482
	public int mapId;
}
