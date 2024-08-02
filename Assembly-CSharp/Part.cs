using System;

// Token: 0x02000084 RID: 132
public class Part
{
	// Token: 0x06000748 RID: 1864 RVA: 0x00083CD4 File Offset: 0x00081ED4
	public Part(int type)
	{
		this.type = type;
		bool flag = type == 0;
		if (flag)
		{
			this.pi = new PartImage[3];
		}
		bool flag2 = type == 1;
		if (flag2)
		{
			this.pi = new PartImage[17];
		}
		bool flag3 = type == 2;
		if (flag3)
		{
			this.pi = new PartImage[14];
		}
		bool flag4 = type == 3;
		if (flag4)
		{
			this.pi = new PartImage[2];
		}
	}

	// Token: 0x04000EFF RID: 3839
	public int type;

	// Token: 0x04000F00 RID: 3840
	public PartImage[] pi;
}
