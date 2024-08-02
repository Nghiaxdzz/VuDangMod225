using System;

// Token: 0x02000048 RID: 72
public class Info_RadaScr
{
	// Token: 0x060003BD RID: 957 RVA: 0x00052FDC File Offset: 0x000511DC
	public void SetInfo(int id, int no, int idIcon, sbyte rank, sbyte typeMonster, short templateId, string name, string info, global::Char charInfo, ItemOption[] itemOption)
	{
		this.id = id;
		this.no = no;
		this.idIcon = idIcon;
		this.rank = rank;
		this.typeMonster = typeMonster;
		bool flag = templateId != -1;
		if (flag)
		{
			this.mobInfo = new Mob();
			this.mobInfo.templateId = (int)templateId;
		}
		this.name = name;
		this.info = info;
		this.charInfo = charInfo;
		this.itemOption = itemOption;
		this.addItemDetail();
	}

	// Token: 0x060003BE RID: 958 RVA: 0x000049E6 File Offset: 0x00002BE6
	public void SetAmount(sbyte amount, sbyte max_amount)
	{
		this.amount = amount;
		this.max_amount = max_amount;
	}

	// Token: 0x060003BF RID: 959 RVA: 0x000049F7 File Offset: 0x00002BF7
	public void SetLevel(sbyte level)
	{
		this.level = level;
		this.addItemDetail();
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x00004A08 File Offset: 0x00002C08
	public void SetUse(sbyte isUse)
	{
		this.isUse = isUse;
		this.addItemDetail();
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x0005305C File Offset: 0x0005125C
	public static global::Char SetCharInfo(int head, int body, int leg, int bag)
	{
		return new global::Char
		{
			head = head,
			body = body,
			leg = leg,
			bag = bag
		};
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00053094 File Offset: 0x00051294
	public static Info_RadaScr GetInfo(MyVector vec, int id)
	{
		bool flag = vec != null;
		if (flag)
		{
			for (int i = 0; i < vec.size(); i++)
			{
				Info_RadaScr info_RadaScr = (Info_RadaScr)vec.elementAt(i);
				bool flag2 = info_RadaScr != null && info_RadaScr.id == id;
				if (flag2)
				{
					return info_RadaScr;
				}
			}
		}
		return null;
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x000530F4 File Offset: 0x000512F4
	public void paintInfo(mGraphics g, int x, int y)
	{
		this.count++;
		bool flag = this.count > this.f.Length - 1;
		if (flag)
		{
			this.count = 0;
		}
		bool flag2 = this.typeMonster == 0;
		if (flag2)
		{
			bool flag3 = Mob.arrMobTemplate[this.mobInfo.templateId] != null;
			if (flag3)
			{
				bool flag4 = Mob.arrMobTemplate[this.mobInfo.templateId].data != null;
				if (flag4)
				{
					Mob.arrMobTemplate[this.mobInfo.templateId].data.paintFrame(g, this.f[this.count], x, y, 0, 0);
				}
				else
				{
					bool flag5 = this.timeRequest - GameCanvas.timeNow < 0L;
					if (flag5)
					{
						this.timeRequest = GameCanvas.timeNow + 1500L;
						this.mobInfo.getData();
					}
				}
			}
		}
		else
		{
			bool flag6 = this.charInfo != null;
			if (flag6)
			{
				this.charInfo.paintCharBody(g, x, y, 1, this.f[this.count], true);
			}
		}
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00053214 File Offset: 0x00051414
	public void addItemDetail()
	{
		this.cp = new ChatPopup();
		string text = string.Empty;
		string text2 = string.Empty;
		text2 = text2 + "\n|6|" + this.info;
		text2 += "\n--";
		bool flag = this.itemOption != null;
		if (flag)
		{
			int num = 0;
			bool flag2 = true;
			while (flag2)
			{
				int num2 = 0;
				for (int i = 0; i < this.itemOption.Length; i++)
				{
					text = this.itemOption[i].getOptionString();
					bool flag3 = !text.Equals(string.Empty) && num == (int)this.itemOption[i].activeCard;
					if (flag3)
					{
						num2++;
						break;
					}
				}
				bool flag4 = num2 == 0;
				if (flag4)
				{
					break;
				}
				bool flag5 = num == 0;
				if (flag5)
				{
					text2 = text2 + "\n|6|2|--" + mResources.unlock + "--";
				}
				else
				{
					string text3 = text2;
					text2 = string.Concat(new object[]
					{
						text3,
						"\n|6|2|--",
						mResources.equip,
						" Lv.",
						num,
						"--"
					});
				}
				for (int j = 0; j < this.itemOption.Length; j++)
				{
					text = this.itemOption[j].getOptionString();
					bool flag6 = text.Equals(string.Empty) || num != (int)this.itemOption[j].activeCard;
					if (!flag6)
					{
						string text4 = "1";
						bool flag7 = this.level == 0;
						if (flag7)
						{
							text4 = "2";
						}
						else
						{
							bool flag8 = this.itemOption[j].activeCard != 0;
							if (flag8)
							{
								bool flag9 = this.isUse == 0;
								if (flag9)
								{
									text4 = "2";
								}
								else
								{
									bool flag10 = this.level < this.itemOption[j].activeCard;
									if (flag10)
									{
										text4 = "2";
									}
								}
							}
						}
						string text5 = text2;
						text2 = string.Concat(new string[]
						{
							text5,
							"\n|",
							text4,
							"|1|",
							text
						});
					}
				}
				bool flag11 = num2 != 0;
				if (flag11)
				{
					num++;
				}
			}
		}
		this.popUpDetailInit(this.cp, text2);
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00053484 File Offset: 0x00051684
	public void popUpDetailInit(ChatPopup cp, string chat)
	{
		cp.sayWidth = RadarScr.wText;
		cp.cx = RadarScr.xText;
		cp.says = mFont.tahoma_7.splitFontArray(chat, cp.sayWidth - 8);
		cp.delay = 10000000;
		cp.c = null;
		cp.ch = cp.says.Length * 12;
		cp.cy = RadarScr.yText;
		cp.strY = 10;
		cp.lim = cp.ch - RadarScr.hText;
		bool flag = cp.lim < 0;
		if (flag)
		{
			cp.lim = 0;
		}
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x00053520 File Offset: 0x00051720
	public void SetEff()
	{
		bool flag = this.amount == this.max_amount && this.eff.size() == 0;
		if (flag)
		{
			int num = Res.random(1, 5);
			for (int i = 0; i < num; i++)
			{
				Position position = new Position();
				position.x = Res.random(5, 25);
				position.y = Res.random(5, 25);
				position.v = i * Res.random(0, 8);
				position.w = 0;
				position.anchor = -1;
				this.eff.addElement(position);
			}
		}
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x000535BC File Offset: 0x000517BC
	public void paintEff(mGraphics g, int x, int y)
	{
		this.SetEff();
		for (int i = 0; i < this.eff.size(); i++)
		{
			Position position = (Position)this.eff.elementAt(i);
			bool flag = position == null;
			if (!flag)
			{
				bool flag2 = position.w < position.v;
				if (flag2)
				{
					position.w++;
				}
				bool flag3 = position.w >= position.v;
				if (flag3)
				{
					position.anchor = GameCanvas.gameTick / 3 % (RadarScr.fraEff.nFrame + 1);
					bool flag4 = position.anchor >= RadarScr.fraEff.nFrame;
					if (flag4)
					{
						this.eff.removeElementAt(i);
						i--;
					}
					else
					{
						RadarScr.fraEff.drawFrame(position.anchor, x + position.x, y + position.y, 0, 3, g);
					}
				}
			}
		}
	}

	// Token: 0x04000870 RID: 2160
	public const sbyte TYPE_MONSTER = 0;

	// Token: 0x04000871 RID: 2161
	public const sbyte TYPE_CHARPART = 1;

	// Token: 0x04000872 RID: 2162
	public sbyte rank;

	// Token: 0x04000873 RID: 2163
	public sbyte amount;

	// Token: 0x04000874 RID: 2164
	public sbyte max_amount;

	// Token: 0x04000875 RID: 2165
	public sbyte typeMonster;

	// Token: 0x04000876 RID: 2166
	public int id;

	// Token: 0x04000877 RID: 2167
	public int no;

	// Token: 0x04000878 RID: 2168
	public int idIcon;

	// Token: 0x04000879 RID: 2169
	public string name;

	// Token: 0x0400087A RID: 2170
	public string info;

	// Token: 0x0400087B RID: 2171
	public sbyte level;

	// Token: 0x0400087C RID: 2172
	public sbyte isUse;

	// Token: 0x0400087D RID: 2173
	public global::Char charInfo;

	// Token: 0x0400087E RID: 2174
	public Mob mobInfo;

	// Token: 0x0400087F RID: 2175
	public ItemOption[] itemOption;

	// Token: 0x04000880 RID: 2176
	private int[] f = new int[]
	{
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		1
	};

	// Token: 0x04000881 RID: 2177
	private int count;

	// Token: 0x04000882 RID: 2178
	private long timeRequest;

	// Token: 0x04000883 RID: 2179
	public ChatPopup cp;

	// Token: 0x04000884 RID: 2180
	public MyVector eff = new MyVector(string.Empty);
}
