using System;

// Token: 0x02000088 RID: 136
public class PlayerInfo
{
	// Token: 0x06000750 RID: 1872 RVA: 0x00084564 File Offset: 0x00082764
	public string getName()
	{
		return this.name;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x00005979 File Offset: 0x00003B79
	public void setMoney(int m)
	{
		this.xu = m;
		this.strMoney = GameCanvas.getMoneys(this.xu);
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x0008457C File Offset: 0x0008277C
	public void setName(string name)
	{
		this.name = name;
		bool flag = name.Length > 9;
		if (flag)
		{
			this.showName = name.Substring(0, 8);
		}
		else
		{
			this.showName = name;
		}
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void paint(mGraphics g, int x, int y)
	{
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x000845BC File Offset: 0x000827BC
	public int getExp()
	{
		return this.exp;
	}

	// Token: 0x04000F16 RID: 3862
	public string name;

	// Token: 0x04000F17 RID: 3863
	public string showName;

	// Token: 0x04000F18 RID: 3864
	public string status;

	// Token: 0x04000F19 RID: 3865
	public int IDDB;

	// Token: 0x04000F1A RID: 3866
	private int exp;

	// Token: 0x04000F1B RID: 3867
	public bool isReady;

	// Token: 0x04000F1C RID: 3868
	public int xu;

	// Token: 0x04000F1D RID: 3869
	public int gold;

	// Token: 0x04000F1E RID: 3870
	public string strMoney = string.Empty;

	// Token: 0x04000F1F RID: 3871
	public sbyte finishPosition;

	// Token: 0x04000F20 RID: 3872
	public bool isMaster;

	// Token: 0x04000F21 RID: 3873
	public static Image[] imgStart;

	// Token: 0x04000F22 RID: 3874
	public sbyte[] indexLv;

	// Token: 0x04000F23 RID: 3875
	public int onlineTime;
}
