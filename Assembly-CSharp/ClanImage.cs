using System;

// Token: 0x02000013 RID: 19
public class ClanImage
{
	// Token: 0x0600015E RID: 350 RVA: 0x00003DA4 File Offset: 0x00001FA4
	public static void addClanImage(ClanImage cm)
	{
		Service.gI().clanImage((sbyte)cm.ID);
		ClanImage.vClanImage.addElement(cm);
	}

	// Token: 0x0600015F RID: 351 RVA: 0x0001EB30 File Offset: 0x0001CD30
	public static ClanImage getClanImage(sbyte ID)
	{
		for (int i = 0; i < ClanImage.vClanImage.size(); i++)
		{
			ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(i);
			bool flag = clanImage.ID == (int)ID;
			if (flag)
			{
				return clanImage;
			}
		}
		return null;
	}

	// Token: 0x06000160 RID: 352 RVA: 0x0001EB84 File Offset: 0x0001CD84
	public static bool isExistClanImage(int ID)
	{
		for (int i = 0; i < ClanImage.vClanImage.size(); i++)
		{
			ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(i);
			bool flag = clanImage.ID == ID;
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040002D5 RID: 725
	public int ID;

	// Token: 0x040002D6 RID: 726
	public string name;

	// Token: 0x040002D7 RID: 727
	public short[] idImage;

	// Token: 0x040002D8 RID: 728
	public int xu;

	// Token: 0x040002D9 RID: 729
	public int luong;

	// Token: 0x040002DA RID: 730
	public static MyVector vClanImage = new MyVector();

	// Token: 0x040002DB RID: 731
	public static MyHashTable idImages = new MyHashTable();
}
