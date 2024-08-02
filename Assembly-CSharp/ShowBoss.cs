using System;

// Token: 0x0200009F RID: 159
public class ShowBoss
{
	// Token: 0x060008CF RID: 2255 RVA: 0x00091D4C File Offset: 0x0008FF4C
	public ShowBoss(string a)
	{
		a = a.Replace(a.Substring(0, 5), "|");
		a = a.Replace(" vừa xuất hiện tại", "|");
		a = a.Replace(" khu vực", "|");
		string[] array = a.Split(new char[]
		{
			'|'
		});
		this.nameBoss = array[1].Trim();
		this.mapName = array[2].Trim();
		this.mapID = VuDang.GetIDMap(this.mapName);
		this.time = DateTime.Now;
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x00091DE4 File Offset: 0x0008FFE4
	public void paintBoss(mGraphics a, int b, int c, int d)
	{
		TimeSpan timeSpan = DateTime.Now.Subtract(this.time);
		int num = (int)timeSpan.TotalSeconds;
		mFont mFont = mFont.tahoma_7b_yellowSmall2;
		bool flag = TileMap.mapName.Trim().ToLower() == this.mapName.Trim().ToLower();
		if (flag)
		{
			mFont = mFont.tahoma_7_red;
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				bool flag2 = @char.cName == this.nameBoss;
				if (flag2)
				{
					mFont = mFont.tahoma_7b_red;
					break;
				}
			}
		}
		mFont.drawString(a, string.Concat(new object[]
		{
			this.nameBoss,
			" - ",
			this.mapName,
			" [",
			VuDang.GetIDMap(this.mapName),
			"] - ",
			(num < 60) ? (num + "s") : (timeSpan.Minutes + "p"),
			" trước <<<"
		}), b, c, d);
	}

	// Token: 0x04001072 RID: 4210
	public string nameBoss;

	// Token: 0x04001073 RID: 4211
	public string mapName;

	// Token: 0x04001074 RID: 4212
	public int mapID;

	// Token: 0x04001075 RID: 4213
	public DateTime time;
}
