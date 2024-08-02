using System;

// Token: 0x02000015 RID: 21
public class ClanMessage : IActionListener
{
	// Token: 0x06000164 RID: 356 RVA: 0x0001EBD8 File Offset: 0x0001CDD8
	public static void addMessage(ClanMessage cm, int index, bool upToTop)
	{
		for (int i = 0; i < ClanMessage.vMessage.size(); i++)
		{
			ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(i);
			bool flag = clanMessage.id == cm.id;
			if (flag)
			{
				ClanMessage.vMessage.removeElement(clanMessage);
				bool flag2 = !upToTop;
				if (flag2)
				{
					ClanMessage.vMessage.insertElementAt(cm, i);
				}
				else
				{
					ClanMessage.vMessage.insertElementAt(cm, 0);
				}
				return;
			}
			bool flag3 = clanMessage.maxCap != 0 && clanMessage.recieve == clanMessage.maxCap;
			if (flag3)
			{
				ClanMessage.vMessage.removeElement(clanMessage);
			}
		}
		bool flag4 = index == -1;
		if (flag4)
		{
			ClanMessage.vMessage.addElement(cm);
		}
		else
		{
			ClanMessage.vMessage.insertElementAt(cm, 0);
		}
		bool flag5 = ClanMessage.vMessage.size() > 20;
		if (flag5)
		{
			ClanMessage.vMessage.removeElementAt(ClanMessage.vMessage.size() - 1);
			return;
		}
	}

	// Token: 0x06000165 RID: 357 RVA: 0x0001ECEC File Offset: 0x0001CEEC
	public void paint(mGraphics g, int x, int y)
	{
		mFont mFont = mFont.tahoma_7b_dark;
		bool flag = this.role == 0;
		if (flag)
		{
			mFont = mFont.tahoma_7b_red;
		}
		else
		{
			bool flag2 = this.role == 1;
			if (flag2)
			{
				mFont = mFont.tahoma_7b_green;
			}
			else
			{
				bool flag3 = this.role == 2;
				if (flag3)
				{
					mFont = mFont.tahoma_7b_green2;
				}
			}
		}
		bool flag4 = this.type == 0;
		if (flag4)
		{
			mFont.drawString(g, this.playerName, x + 3, y + 1, 0);
			bool flag5 = this.color == 0;
			if (flag5)
			{
				mFont.tahoma_7_grey.drawString(g, this.chat[0] + ((this.chat.Length <= 1) ? string.Empty : "..."), x + 3, y + 11, 0);
			}
			else
			{
				mFont.tahoma_7_red.drawString(g, this.chat[0] + ((this.chat.Length <= 1) ? string.Empty : "..."), x + 3, y + 11, 0);
			}
			mFont.tahoma_7_grey.drawString(g, NinjaUtil.getTimeAgo(this.timeAgo) + " " + mResources.ago, x + GameCanvas.panel.wScroll - 3, y + 1, mFont.RIGHT);
		}
		bool flag6 = this.type == 1;
		if (flag6)
		{
			mFont.drawString(g, string.Concat(new object[]
			{
				this.playerName,
				" (",
				this.recieve,
				"/",
				this.maxCap,
				")"
			}), x + 3, y + 1, 0);
			mFont.tahoma_7_blue.drawString(g, string.Concat(new string[]
			{
				mResources.request_pea,
				" ",
				NinjaUtil.getTimeAgo(this.timeAgo),
				" ",
				mResources.ago
			}), x + 3, y + 11, 0);
		}
		bool flag7 = this.type == 2;
		if (flag7)
		{
			mFont.drawString(g, this.playerName, x + 3, y + 1, 0);
			mFont.tahoma_7_blue.drawString(g, mResources.request_join_clan, x + 3, y + 11, 0);
		}
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void perform(int idAction, object p)
	{
	}

	// Token: 0x06000167 RID: 359 RVA: 0x0001EF24 File Offset: 0x0001D124
	public void update()
	{
		bool flag = this.time != 0L;
		if (flag)
		{
			this.timeAgo = (int)(mSystem.currentTimeMillis() / 1000L - this.time);
		}
	}

	// Token: 0x040002DD RID: 733
	public int id;

	// Token: 0x040002DE RID: 734
	public int type;

	// Token: 0x040002DF RID: 735
	public int playerId;

	// Token: 0x040002E0 RID: 736
	public string playerName;

	// Token: 0x040002E1 RID: 737
	public long time;

	// Token: 0x040002E2 RID: 738
	public int headId;

	// Token: 0x040002E3 RID: 739
	public string[] chat;

	// Token: 0x040002E4 RID: 740
	public sbyte color;

	// Token: 0x040002E5 RID: 741
	public sbyte role;

	// Token: 0x040002E6 RID: 742
	private int timeAgo;

	// Token: 0x040002E7 RID: 743
	public int recieve;

	// Token: 0x040002E8 RID: 744
	public int maxCap;

	// Token: 0x040002E9 RID: 745
	public string[] option;

	// Token: 0x040002EA RID: 746
	public static MyVector vMessage = new MyVector();
}
