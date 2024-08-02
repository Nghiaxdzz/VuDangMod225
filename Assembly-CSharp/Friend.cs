using System;

// Token: 0x02000034 RID: 52
public class Friend
{
	// Token: 0x06000249 RID: 585 RVA: 0x000041BB File Offset: 0x000023BB
	public Friend(string friendName, sbyte type)
	{
		this.friendName = friendName;
		this.type = type;
	}

	// Token: 0x0600024A RID: 586 RVA: 0x000041D3 File Offset: 0x000023D3
	public Friend(string friendName)
	{
		this.friendName = friendName;
		this.type = 2;
	}

	// Token: 0x04000561 RID: 1377
	public string friendName;

	// Token: 0x04000562 RID: 1378
	public sbyte type;
}
