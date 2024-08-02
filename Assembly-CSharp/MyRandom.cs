using System;

// Token: 0x02000077 RID: 119
public class MyRandom
{
	// Token: 0x060005AA RID: 1450 RVA: 0x000052A1 File Offset: 0x000034A1
	public MyRandom()
	{
		this.r = new Random();
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x00064D4C File Offset: 0x00062F4C
	public int nextInt()
	{
		return this.r.Next();
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x00064D6C File Offset: 0x00062F6C
	public int nextInt(int a)
	{
		return this.r.Next(a);
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x00064D8C File Offset: 0x00062F8C
	public int nextInt(int a, int b)
	{
		return this.r.Next(a, b);
	}

	// Token: 0x04000D56 RID: 3414
	public Random r;
}
