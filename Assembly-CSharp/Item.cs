using System;

// Token: 0x02000051 RID: 81
public class Item
{
	// Token: 0x06000410 RID: 1040 RVA: 0x00004ABE File Offset: 0x00002CBE
	public void getCompare()
	{
		this.compare = GameCanvas.panel.getCompare(this);
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00053B70 File Offset: 0x00051D70
	public string getPrice()
	{
		string text = string.Empty;
		bool flag = this.buyCoin <= 0 && this.buyGold <= 0;
		string result;
		if (flag)
		{
			result = null;
		}
		else
		{
			bool flag2 = this.buyCoin > 0 && this.buyGold <= 0;
			if (flag2)
			{
				text = this.buyCoin + mResources.XU;
			}
			else
			{
				bool flag3 = this.buyGold > 0 && this.buyCoin <= 0;
				if (flag3)
				{
					text = this.buyGold + mResources.LUONG;
				}
				else
				{
					bool flag4 = this.buyCoin > 0 && this.buyGold > 0;
					if (flag4)
					{
						text = string.Concat(new object[]
						{
							this.buyCoin,
							mResources.XU,
							"/",
							this.buyGold,
							mResources.LUONG
						});
					}
				}
			}
			result = text;
		}
		return result;
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x00053C7C File Offset: 0x00051E7C
	public void paintUpgradeEffect(int x, int y, int upgrade, mGraphics g)
	{
		int num = GameScr.indexSize - 2;
		int num2 = 0;
		int num3 = (upgrade >= 4) ? ((upgrade < 8) ? 1 : ((upgrade < 12) ? 2 : ((upgrade > 14) ? 4 : 3))) : 0;
		for (int i = num2; i < this.size.Length; i++)
		{
			int num4 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick - i * 4);
			int num5 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick - i * 4);
			g.setColor(this.colorBorder[num3][i]);
			g.fillRect(num4 - this.size[i] / 2, num5 - this.size[i] / 2, this.size[i], this.size[i]);
		}
		bool flag = upgrade == 4 || upgrade == 8;
		if (flag)
		{
			for (int j = num2; j < this.size.Length; j++)
			{
				int num6 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick - num * 2 - j * 4);
				int num7 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick - num * 2 - j * 4);
				g.setColor(this.colorBorder[num3 - 1][j]);
				g.fillRect(num6 - this.size[j] / 2, num7 - this.size[j] / 2, this.size[j], this.size[j]);
			}
		}
		bool flag2 = upgrade != 1 && upgrade != 4 && upgrade != 8;
		if (flag2)
		{
			for (int k = num2; k < this.size.Length; k++)
			{
				int num8 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick - num * 2 - k * 4);
				int num9 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick - num * 2 - k * 4);
				g.setColor(this.colorBorder[num3][k]);
				g.fillRect(num8 - this.size[k] / 2, num9 - this.size[k] / 2, this.size[k], this.size[k]);
			}
		}
		bool flag3 = upgrade != 1 && upgrade != 4 && upgrade != 8 && upgrade != 12 && upgrade != 2 && upgrade != 5 && upgrade != 9;
		if (flag3)
		{
			for (int l = num2; l < this.size.Length; l++)
			{
				int num10 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick - num - l * 4);
				int num11 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick - num - l * 4);
				g.setColor(this.colorBorder[num3][l]);
				g.fillRect(num10 - this.size[l] / 2, num11 - this.size[l] / 2, this.size[l], this.size[l]);
			}
		}
		bool flag4 = upgrade != 1 && upgrade != 4 && upgrade != 8 && upgrade != 12 && upgrade != 2 && upgrade != 5 && upgrade != 9 && upgrade != 13 && upgrade != 3 && upgrade != 6 && upgrade != 10 && upgrade != 15;
		if (flag4)
		{
			for (int m = num2; m < this.size.Length; m++)
			{
				int num12 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick - num * 3 - m * 4);
				int num13 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick - num * 3 - m * 4);
				g.setColor(this.colorBorder[num3][m]);
				g.fillRect(num12 - this.size[m] / 2, num13 - this.size[m] / 2, this.size[m], this.size[m]);
			}
		}
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00054084 File Offset: 0x00052284
	private int upgradeEffectY(int tick)
	{
		int num = GameScr.indexSize - 2;
		int num2 = tick % (4 * num);
		bool flag = 0 <= num2 && num2 < num;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool flag2 = num <= num2 && num2 < num * 2;
			if (flag2)
			{
				result = num2 % num;
			}
			else
			{
				bool flag3 = num * 2 <= num2 && num2 < num * 3;
				if (flag3)
				{
					result = num;
				}
				else
				{
					result = num - num2 % num;
				}
			}
		}
		return result;
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x000540F4 File Offset: 0x000522F4
	private int upgradeEffectX(int tick)
	{
		int num = GameScr.indexSize - 2;
		int num2 = tick % (4 * num);
		bool flag = 0 <= num2 && num2 < num;
		int result;
		if (flag)
		{
			result = num2 % num;
		}
		else
		{
			bool flag2 = num <= num2 && num2 < num * 2;
			if (flag2)
			{
				result = num;
			}
			else
			{
				bool flag3 = num * 2 <= num2 && num2 < num * 3;
				if (flag3)
				{
					result = num - num2 % num;
				}
				else
				{
					result = 0;
				}
			}
		}
		return result;
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00054164 File Offset: 0x00052364
	public bool isHaveOption(int id)
	{
		for (int i = 0; i < this.itemOption.Length; i++)
		{
			ItemOption itemOption = this.itemOption[i];
			bool flag = itemOption != null && itemOption.optionTemplate.id == id;
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x000541B8 File Offset: 0x000523B8
	public Item clone()
	{
		Item item = new Item();
		item.template = this.template;
		bool flag = this.options != null;
		if (flag)
		{
			item.options = new MyVector();
			for (int i = 0; i < this.options.size(); i++)
			{
				ItemOption itemOption = new ItemOption();
				itemOption.optionTemplate = ((ItemOption)this.options.elementAt(i)).optionTemplate;
				itemOption.param = ((ItemOption)this.options.elementAt(i)).param;
				item.options.addElement(itemOption);
			}
		}
		item.itemId = this.itemId;
		item.playerId = this.playerId;
		item.indexUI = this.indexUI;
		item.quantity = this.quantity;
		item.isLock = this.isLock;
		item.sys = this.sys;
		item.upgrade = this.upgrade;
		item.buyCoin = this.buyCoin;
		item.buyCoinLock = this.buyCoinLock;
		item.buyGold = this.buyGold;
		item.buyGoldLock = this.buyGoldLock;
		item.saleCoinLock = this.saleCoinLock;
		item.typeUI = this.typeUI;
		item.isExpires = this.isExpires;
		return item;
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x0005430C File Offset: 0x0005250C
	public bool isTypeBody()
	{
		return (0 <= this.template.type && this.template.type < 6) || this.template.type == 32 || this.template.type == 35 || this.template.type == 11 || this.template.type == 23;
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x00054384 File Offset: 0x00052584
	public string getLockstring()
	{
		return (!this.isLock) ? mResources.NOLOCK : mResources.LOCKED;
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x000543AC File Offset: 0x000525AC
	public string getUpgradestring()
	{
		bool flag = this.template.level < 10 || this.template.type >= 10;
		string result;
		if (flag)
		{
			result = mResources.NOTUPGRADE;
		}
		else
		{
			bool flag2 = this.upgrade == 0;
			if (flag2)
			{
				result = mResources.NOUPGRADE;
			}
			else
			{
				result = null;
			}
		}
		return result;
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x00054408 File Offset: 0x00052608
	public bool isTypeUIMe()
	{
		return this.typeUI == 5 || this.typeUI == 3 || this.typeUI == 4;
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x00054444 File Offset: 0x00052644
	public bool isTypeUIShopView()
	{
		bool flag = this.isTypeUIShop();
		bool result;
		if (flag)
		{
			result = true;
		}
		else
		{
			bool flag2 = this.isTypeUIStore() || this.isTypeUIBook() || this.isTypeUIFashion();
			result = flag2;
		}
		return result;
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x00054488 File Offset: 0x00052688
	public bool isTypeUIShop()
	{
		return this.typeUI == 20 || this.typeUI == 21 || this.typeUI == 22 || this.typeUI == 23 || this.typeUI == 24 || this.typeUI == 25 || this.typeUI == 26 || this.typeUI == 27 || this.typeUI == 28 || this.typeUI == 29 || this.typeUI == 16 || this.typeUI == 17 || this.typeUI == 18 || this.typeUI == 19 || this.typeUI == 2 || this.typeUI == 6 || this.typeUI == 8;
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x0005455C File Offset: 0x0005275C
	public bool isTypeUIShopLock()
	{
		return this.typeUI == 7 || this.typeUI == 9;
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x00054590 File Offset: 0x00052790
	public bool isTypeUIStore()
	{
		return this.typeUI == 14;
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x000545B8 File Offset: 0x000527B8
	public bool isTypeUIBook()
	{
		return this.typeUI == 15;
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x000545E0 File Offset: 0x000527E0
	public bool isTypeUIFashion()
	{
		return this.typeUI == 32;
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x00054608 File Offset: 0x00052808
	public bool isUpMax()
	{
		return this.getUpMax() == this.upgrade;
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x00054634 File Offset: 0x00052834
	public int getUpMax()
	{
		bool flag = this.template.level >= 1 && this.template.level < 20;
		int result;
		if (flag)
		{
			result = 4;
		}
		else
		{
			bool flag2 = this.template.level >= 20 && this.template.level < 40;
			if (flag2)
			{
				result = 8;
			}
			else
			{
				bool flag3 = this.template.level >= 40 && this.template.level < 50;
				if (flag3)
				{
					result = 12;
				}
				else
				{
					bool flag4 = this.template.level >= 50 && this.template.level < 60;
					if (flag4)
					{
						result = 14;
					}
					else
					{
						result = 16;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x00004AD2 File Offset: 0x00002CD2
	public void setPartTemp(int headTemp, int bodyTemp, int legTemp, int bagTemp)
	{
		this.headTemp = headTemp;
		this.bodyTemp = bodyTemp;
		this.legTemp = legTemp;
		this.bagTemp = bagTemp;
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x000546F8 File Offset: 0x000528F8
	public string VuDangItemOption()
	{
		string text = "";
		for (int i = 0; i < this.itemOption.Length; i++)
		{
			text += this.itemOption[i].getOptionName();
		}
		return text;
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x00054740 File Offset: 0x00052940
	public int VuDangSoSao()
	{
		for (int i = 0; i < this.itemOption.Length; i++)
		{
			bool flag = this.itemOption[i].optionTemplate.id == 107;
			if (flag)
			{
				return this.itemOption[i].param;
			}
		}
		return 0;
	}

	// Token: 0x040008A9 RID: 2217
	public const int OPT_STAR = 34;

	// Token: 0x040008AA RID: 2218
	public const int OPT_MOON = 35;

	// Token: 0x040008AB RID: 2219
	public const int OPT_SUN = 36;

	// Token: 0x040008AC RID: 2220
	public const int OPT_COLORNAME = 41;

	// Token: 0x040008AD RID: 2221
	public const int OPT_LVITEM = 72;

	// Token: 0x040008AE RID: 2222
	public const int OPT_STARSLOT = 102;

	// Token: 0x040008AF RID: 2223
	public const int OPT_MAXSTARSLOT = 107;

	// Token: 0x040008B0 RID: 2224
	public const int TYPE_BODY_MIN = 0;

	// Token: 0x040008B1 RID: 2225
	public const int TYPE_BODY_MAX = 6;

	// Token: 0x040008B2 RID: 2226
	public const int TYPE_AO = 0;

	// Token: 0x040008B3 RID: 2227
	public const int TYPE_QUAN = 1;

	// Token: 0x040008B4 RID: 2228
	public const int TYPE_GANGTAY = 2;

	// Token: 0x040008B5 RID: 2229
	public const int TYPE_GIAY = 3;

	// Token: 0x040008B6 RID: 2230
	public const int TYPE_RADA = 4;

	// Token: 0x040008B7 RID: 2231
	public const int TYPE_HAIR = 5;

	// Token: 0x040008B8 RID: 2232
	public const int TYPE_DAUTHAN = 6;

	// Token: 0x040008B9 RID: 2233
	public const int TYPE_NGOCRONG = 12;

	// Token: 0x040008BA RID: 2234
	public const int TYPE_SACH = 7;

	// Token: 0x040008BB RID: 2235
	public const int TYPE_NHIEMVU = 8;

	// Token: 0x040008BC RID: 2236
	public const int TYPE_GOLD = 9;

	// Token: 0x040008BD RID: 2237
	public const int TYPE_DIAMOND = 10;

	// Token: 0x040008BE RID: 2238
	public const int TYPE_BALO = 11;

	// Token: 0x040008BF RID: 2239
	public const int TYPE_MOUNT = 23;

	// Token: 0x040008C0 RID: 2240
	public const int TYPE_MOUNT_VIP = 24;

	// Token: 0x040008C1 RID: 2241
	public const int TYPE_DIAMOND_LOCK = 34;

	// Token: 0x040008C2 RID: 2242
	public const int TYPE_TRAINSUIT = 32;

	// Token: 0x040008C3 RID: 2243
	public const int TYPE_HAT = 35;

	// Token: 0x040008C4 RID: 2244
	public const sbyte UI_WEAPON = 2;

	// Token: 0x040008C5 RID: 2245
	public const sbyte UI_BAG = 3;

	// Token: 0x040008C6 RID: 2246
	public const sbyte UI_BOX = 4;

	// Token: 0x040008C7 RID: 2247
	public const sbyte UI_BODY = 5;

	// Token: 0x040008C8 RID: 2248
	public const sbyte UI_STACK = 6;

	// Token: 0x040008C9 RID: 2249
	public const sbyte UI_STACK_LOCK = 7;

	// Token: 0x040008CA RID: 2250
	public const sbyte UI_GROCERY = 8;

	// Token: 0x040008CB RID: 2251
	public const sbyte UI_GROCERY_LOCK = 9;

	// Token: 0x040008CC RID: 2252
	public const sbyte UI_UPGRADE = 10;

	// Token: 0x040008CD RID: 2253
	public const sbyte UI_UPPEARL = 11;

	// Token: 0x040008CE RID: 2254
	public const sbyte UI_UPPEARL_LOCK = 12;

	// Token: 0x040008CF RID: 2255
	public const sbyte UI_SPLIT = 13;

	// Token: 0x040008D0 RID: 2256
	public const sbyte UI_STORE = 14;

	// Token: 0x040008D1 RID: 2257
	public const sbyte UI_BOOK = 15;

	// Token: 0x040008D2 RID: 2258
	public const sbyte UI_LIEN = 16;

	// Token: 0x040008D3 RID: 2259
	public const sbyte UI_NHAN = 17;

	// Token: 0x040008D4 RID: 2260
	public const sbyte UI_NGOCBOI = 18;

	// Token: 0x040008D5 RID: 2261
	public const sbyte UI_PHU = 19;

	// Token: 0x040008D6 RID: 2262
	public const sbyte UI_NONNAM = 20;

	// Token: 0x040008D7 RID: 2263
	public const sbyte UI_NONNU = 21;

	// Token: 0x040008D8 RID: 2264
	public const sbyte UI_AONAM = 22;

	// Token: 0x040008D9 RID: 2265
	public const sbyte UI_AONU = 23;

	// Token: 0x040008DA RID: 2266
	public const sbyte UI_GANGTAYNAM = 24;

	// Token: 0x040008DB RID: 2267
	public const sbyte UI_GANGTAYNU = 25;

	// Token: 0x040008DC RID: 2268
	public const sbyte UI_QUANNAM = 26;

	// Token: 0x040008DD RID: 2269
	public const sbyte UI_QUANNU = 27;

	// Token: 0x040008DE RID: 2270
	public const sbyte UI_GIAYNAM = 28;

	// Token: 0x040008DF RID: 2271
	public const sbyte UI_GIAYNU = 29;

	// Token: 0x040008E0 RID: 2272
	public const sbyte UI_TRADE = 30;

	// Token: 0x040008E1 RID: 2273
	public const sbyte UI_UPGRADE_GOLD = 31;

	// Token: 0x040008E2 RID: 2274
	public const sbyte UI_FASHION = 32;

	// Token: 0x040008E3 RID: 2275
	public const sbyte UI_CONVERT = 33;

	// Token: 0x040008E4 RID: 2276
	public ItemOption[] itemOption;

	// Token: 0x040008E5 RID: 2277
	public ItemTemplate template;

	// Token: 0x040008E6 RID: 2278
	public MyVector options;

	// Token: 0x040008E7 RID: 2279
	public int itemId;

	// Token: 0x040008E8 RID: 2280
	public int playerId;

	// Token: 0x040008E9 RID: 2281
	public bool isSelect;

	// Token: 0x040008EA RID: 2282
	public int indexUI;

	// Token: 0x040008EB RID: 2283
	public int quantity;

	// Token: 0x040008EC RID: 2284
	public int quantilyToBuy;

	// Token: 0x040008ED RID: 2285
	public long powerRequire;

	// Token: 0x040008EE RID: 2286
	public bool isLock;

	// Token: 0x040008EF RID: 2287
	public int sys;

	// Token: 0x040008F0 RID: 2288
	public int upgrade;

	// Token: 0x040008F1 RID: 2289
	public int buyCoin;

	// Token: 0x040008F2 RID: 2290
	public int buyCoinLock;

	// Token: 0x040008F3 RID: 2291
	public int buyGold;

	// Token: 0x040008F4 RID: 2292
	public int buyGoldLock;

	// Token: 0x040008F5 RID: 2293
	public int saleCoinLock;

	// Token: 0x040008F6 RID: 2294
	public int buySpec;

	// Token: 0x040008F7 RID: 2295
	public int buyRuby;

	// Token: 0x040008F8 RID: 2296
	public short iconSpec = -1;

	// Token: 0x040008F9 RID: 2297
	public sbyte buyType = -1;

	// Token: 0x040008FA RID: 2298
	public int typeUI;

	// Token: 0x040008FB RID: 2299
	public bool isExpires;

	// Token: 0x040008FC RID: 2300
	public bool isBuySpec;

	// Token: 0x040008FD RID: 2301
	public EffectCharPaint eff;

	// Token: 0x040008FE RID: 2302
	public int indexEff;

	// Token: 0x040008FF RID: 2303
	public Image img;

	// Token: 0x04000900 RID: 2304
	public string info;

	// Token: 0x04000901 RID: 2305
	public string content;

	// Token: 0x04000902 RID: 2306
	public string reason = string.Empty;

	// Token: 0x04000903 RID: 2307
	public int compare;

	// Token: 0x04000904 RID: 2308
	public sbyte isMe;

	// Token: 0x04000905 RID: 2309
	public bool newItem;

	// Token: 0x04000906 RID: 2310
	public int headTemp = -1;

	// Token: 0x04000907 RID: 2311
	public int bodyTemp = -1;

	// Token: 0x04000908 RID: 2312
	public int legTemp = -1;

	// Token: 0x04000909 RID: 2313
	public int bagTemp = -1;

	// Token: 0x0400090A RID: 2314
	public int wpTemp = -1;

	// Token: 0x0400090B RID: 2315
	private int[] color = new int[]
	{
		0,
		0,
		0,
		0,
		600841,
		600841,
		667658,
		667658,
		3346944,
		3346688,
		4199680,
		5052928,
		3276851,
		3932211,
		4587571,
		5046280,
		6684682,
		3359744
	};

	// Token: 0x0400090C RID: 2316
	private int[][] colorBorder = new int[][]
	{
		new int[]
		{
			18687,
			16869,
			15052,
			13235,
			11161,
			9344
		},
		new int[]
		{
			45824,
			39168,
			32768,
			26112,
			19712,
			13056
		},
		new int[]
		{
			16744192,
			15037184,
			13395456,
			11753728,
			10046464,
			8404992
		},
		new int[]
		{
			13500671,
			12058853,
			10682572,
			9371827,
			7995545,
			6684800
		},
		new int[]
		{
			16711705,
			15007767,
			13369364,
			11730962,
			10027023,
			8388621
		}
	};

	// Token: 0x0400090D RID: 2317
	private int[] size = new int[]
	{
		2,
		1,
		1,
		1,
		1,
		1
	};
}
