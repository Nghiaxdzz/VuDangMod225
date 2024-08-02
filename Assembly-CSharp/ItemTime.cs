using System;

// Token: 0x02000058 RID: 88
public class ItemTime
{
	// Token: 0x06000446 RID: 1094 RVA: 0x00004B68 File Offset: 0x00002D68
	public ItemTime()
	{
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x00055490 File Offset: 0x00053690
	public ItemTime(short idIcon, int s)
	{
		this.idIcon = idIcon;
		this.minute = s / 60;
		this.second = s % 60;
		this.curr = (this.last = mSystem.currentTimeMillis());
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x000554D4 File Offset: 0x000536D4
	public void initTimeText(sbyte id, string text, int time)
	{
		bool flag = time == -1;
		if (flag)
		{
			this.dontClear = true;
		}
		else
		{
			this.dontClear = false;
		}
		this.isText = true;
		this.minute = time / 60;
		this.second = time % 60;
		this.idIcon = (short)id;
		this.curr = (this.last = mSystem.currentTimeMillis());
		this.text = text;
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x0005553C File Offset: 0x0005373C
	public void initTime(int time, bool isText)
	{
		this.minute = time / 60;
		this.second = time % 60;
		this.curr = (this.last = mSystem.currentTimeMillis());
		this.isText = isText;
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x0005557C File Offset: 0x0005377C
	public static bool isExistItem(int id)
	{
		for (int i = 0; i < global::Char.vItemTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)global::Char.vItemTime.elementAt(i);
			bool flag = (int)itemTime.idIcon == id;
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x000555D0 File Offset: 0x000537D0
	public static ItemTime getMessageById(int id)
	{
		for (int i = 0; i < GameScr.textTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)GameScr.textTime.elementAt(i);
			bool flag = (int)itemTime.idIcon == id;
			if (flag)
			{
				return itemTime;
			}
		}
		return null;
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x00055624 File Offset: 0x00053824
	public static bool isExistMessage(int id)
	{
		for (int i = 0; i < GameScr.textTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)GameScr.textTime.elementAt(i);
			bool flag = (int)itemTime.idIcon == id;
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x00055678 File Offset: 0x00053878
	public static ItemTime getItemById(int id)
	{
		for (int i = 0; i < global::Char.vItemTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)global::Char.vItemTime.elementAt(i);
			bool flag = (int)itemTime.idIcon == id;
			if (flag)
			{
				return itemTime;
			}
		}
		return null;
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x000556CC File Offset: 0x000538CC
	public void initTime(int time)
	{
		this.minute = time / 60;
		this.second = time % 60;
		this.curr = (this.last = mSystem.currentTimeMillis());
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x00055704 File Offset: 0x00053904
	public void paint(mGraphics g, int x, int y)
	{
		SmallImage.drawSmallImage(g, (int)this.idIcon, x, y, 0, 3);
		string st = string.Empty;
		st = this.minute + "'";
		bool flag = this.minute == 0;
		if (flag)
		{
			st = this.second + "s";
		}
		mFont.tahoma_7b_white.drawString(g, st, x, y + 15, 2, mFont.tahoma_7b_dark);
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x0005577C File Offset: 0x0005397C
	public void paintText(mGraphics g, int x, int y)
	{
		string str = string.Empty;
		str = this.minute + "'";
		bool flag = this.minute < 1;
		if (flag)
		{
			str = this.second + "s";
		}
		bool flag2 = this.minute < 0;
		if (flag2)
		{
			str = string.Empty;
		}
		bool flag3 = this.dontClear;
		if (flag3)
		{
			str = string.Empty;
		}
		mFont.tahoma_7b_white.drawString(g, this.text + " " + str, x, y, mFont.LEFT, mFont.tahoma_7b_dark);
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x0005581C File Offset: 0x00053A1C
	public void update()
	{
		this.curr = mSystem.currentTimeMillis();
		bool flag = this.curr - this.last >= 1000L;
		if (flag)
		{
			this.last = mSystem.currentTimeMillis();
			this.second--;
			bool flag2 = this.second <= 0;
			if (flag2)
			{
				this.second = 60;
				this.minute--;
			}
		}
		bool flag3 = this.minute < 0 && !this.isText;
		if (flag3)
		{
			global::Char.vItemTime.removeElement(this);
		}
		bool flag4 = this.minute < 0 && this.isText && !this.dontClear;
		if (flag4)
		{
			GameScr.textTime.removeElement(this);
		}
	}

	// Token: 0x04000947 RID: 2375
	public short idIcon;

	// Token: 0x04000948 RID: 2376
	public int second;

	// Token: 0x04000949 RID: 2377
	public int minute;

	// Token: 0x0400094A RID: 2378
	private long curr;

	// Token: 0x0400094B RID: 2379
	private long last;

	// Token: 0x0400094C RID: 2380
	private bool isText;

	// Token: 0x0400094D RID: 2381
	private bool dontClear;

	// Token: 0x0400094E RID: 2382
	private string text;
}
