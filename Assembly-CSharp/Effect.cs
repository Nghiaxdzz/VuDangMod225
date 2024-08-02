using System;

// Token: 0x02000022 RID: 34
public class Effect
{
	// Token: 0x060001ED RID: 493 RVA: 0x00035B60 File Offset: 0x00033D60
	public Effect()
	{
	}

	// Token: 0x060001EE RID: 494 RVA: 0x00035BF4 File Offset: 0x00033DF4
	public Effect(int id, global::Char c, int layer, int loop, int loopCount, sbyte isStand)
	{
		this.c = c;
		this.effId = id;
		this.layer = layer;
		this.loop = loop;
		this.tLoop = loopCount;
		this.isStand = (int)isStand;
		bool flag = Effect.getEffDataById(id) == null;
		if (flag)
		{
			EffectData effectData = new EffectData
			{
				ID = id
			};
			bool flag2 = id >= 42 && id <= 46;
			if (flag2)
			{
				id = 106;
			}
			string text = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				id,
				"/data"
			});
			DataInputStream dataInputStream = MyStream.readFile(text);
			bool flag3 = dataInputStream != null;
			if (flag3)
			{
				bool flag4 = id > 100 && id < 200;
				if (flag4)
				{
					effectData.readData2(text);
				}
				else
				{
					effectData.readData(text);
				}
				effectData.img = GameCanvas.loadImage("/effectdata/" + id + "/img.png");
			}
			else
			{
				Service.gI().getEffData((short)id);
			}
			Effect.addEffData(effectData);
		}
		this.indexFrom = -1;
		this.indexTo = -1;
		this.trans = -1;
		this.typeEff = 4;
	}

	// Token: 0x060001EF RID: 495 RVA: 0x00035DC0 File Offset: 0x00033FC0
	public Effect(int id, int x, int y, int layer, int loop, int loopCount)
	{
		this.x = x;
		this.y = y;
		this.effId = id;
		this.layer = layer;
		this.loop = loop;
		this.tLoop = loopCount;
		bool flag = Effect.getEffDataById(id) == null;
		if (flag)
		{
			EffectData effectData = new EffectData
			{
				ID = id
			};
			bool flag2 = id >= 42 && id <= 46;
			if (flag2)
			{
				id = 106;
			}
			string text = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				id,
				"/data"
			});
			DataInputStream dataInputStream = MyStream.readFile(text);
			bool flag3 = dataInputStream != null;
			if (flag3)
			{
				bool flag4 = id > 100 && id < 200;
				if (flag4)
				{
					effectData.readData2(text);
				}
				else
				{
					effectData.readData(text);
				}
				effectData.img = GameCanvas.loadImage("/effectdata/" + id + "/img.png");
			}
			else
			{
				Service.gI().getEffData((short)id);
			}
			Effect.addEffData(effectData);
			Effect.lastEff.addElement(this.effId + string.Empty);
		}
		this.indexFrom = -1;
		this.indexTo = -1;
		this.typeEff = 1;
		bool flag5 = !Effect.isExistNewEff(this.effId + string.Empty);
		if (flag5)
		{
			Effect.newEff.addElement(this.effId + string.Empty);
		}
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x00035FE8 File Offset: 0x000341E8
	public static void removeEffData(int id)
	{
		for (int i = 0; i < Effect.vEffData.size(); i++)
		{
			EffectData effectData = (EffectData)Effect.vEffData.elementAt(i);
			bool flag = effectData.ID == id;
			if (flag)
			{
				Effect.vEffData.removeElement(effectData);
				break;
			}
		}
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x00036040 File Offset: 0x00034240
	public static void addEffData(EffectData eff)
	{
		Effect.vEffData.addElement(eff);
		bool flag = TileMap.mapID != 130 && Effect.vEffData.size() > 10;
		if (flag)
		{
			for (int i = 0; i < 5; i++)
			{
				Effect.vEffData.removeElementAt(0);
			}
		}
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x0003609C File Offset: 0x0003429C
	public static EffectData getEffDataById(int id)
	{
		for (int i = 0; i < Effect.vEffData.size(); i++)
		{
			EffectData effectData = (EffectData)Effect.vEffData.elementAt(i);
			bool flag = effectData.ID == id;
			if (flag)
			{
				return effectData;
			}
		}
		return null;
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x000360F0 File Offset: 0x000342F0
	public static bool isExistNewEff(string id)
	{
		for (int i = 0; i < Effect.newEff.size(); i++)
		{
			string text = (string)Effect.newEff.elementAt(i);
			bool flag = text.Equals(id);
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x00036144 File Offset: 0x00034344
	public bool isPaintz()
	{
		bool flag = !this.isPaint;
		return !flag;
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0003616C File Offset: 0x0003436C
	public void paintUnderBackground(mGraphics g, int xLayer, int yLayer)
	{
		bool flag = this.isPaintz() && Effect.getEffDataById(this.effId).img != null;
		if (flag)
		{
			Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x + xLayer, this.y + yLayer, this.trans, this.layer);
		}
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x000361D4 File Offset: 0x000343D4
	public void getFrameKhangia()
	{
		bool flag = this.effId == 42;
		if (flag)
		{
			this.currFrame = this.khangia1[this.t];
		}
		bool flag2 = this.effId == 43;
		if (flag2)
		{
			this.currFrame = this.khangia2[this.t];
		}
		bool flag3 = this.effId == 44;
		if (flag3)
		{
			this.currFrame = this.khangia3[this.t];
		}
		bool flag4 = this.effId == 45;
		if (flag4)
		{
			this.currFrame = this.khangia4[this.t];
		}
		bool flag5 = this.effId == 46;
		if (flag5)
		{
			this.currFrame = this.khangia5[this.t];
		}
		this.t++;
		bool flag6 = this.t > this.khangia1.Length - 1;
		if (flag6)
		{
			this.t = 0;
		}
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x000362C4 File Offset: 0x000344C4
	public void paint(mGraphics g)
	{
		bool flag = this.isPaintz() && Effect.getEffDataById(this.effId) != null && Effect.getEffDataById(this.effId).img != null;
		if (flag)
		{
			Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x, this.y, this.trans, this.layer);
		}
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x00036334 File Offset: 0x00034534
	public void update()
	{
		try
		{
			bool flag = this.effId >= 42 && this.effId <= 46;
			if (flag)
			{
				this.getFrameKhangia();
			}
			else
			{
				bool flag2 = Effect.getEffDataById(this.effId) == null || Effect.getEffDataById(this.effId).img == null;
				if (!flag2)
				{
					bool flag3 = Effect.getEffDataById(this.effId).arrFrame != null;
					if (flag3)
					{
						bool flag4 = !this.isGetTime;
						if (flag4)
						{
							this.isGetTime = true;
							int num = Effect.getEffDataById(this.effId).arrFrame.Length - 1;
							bool flag5 = num > 0 && this.typeEff != 1;
							if (flag5)
							{
								this.t = Res.random(0, num);
							}
							bool flag6 = this.typeEff == 0;
							if (flag6)
							{
								this.t = Res.random(this.indexFrom, this.indexTo);
							}
						}
						switch (this.typeEff)
						{
						case 0:
						{
							bool flag7 = Res.inRect(this.x - 50, this.y - 50, 100, 100, global::Char.myCharz().cx, global::Char.myCharz().cy) && this.t > this.indexFrom && this.t < this.indexTo;
							if (flag7)
							{
								bool flag8 = this.t < this.indexTo;
								if (flag8)
								{
									this.t = this.indexTo;
								}
								this.isNearPlayer = true;
							}
							bool flag9 = !this.isNearPlayer;
							if (flag9)
							{
								this.t++;
								bool flag10 = this.t == this.indexTo;
								if (flag10)
								{
									this.t = this.indexFrom;
								}
							}
							else
							{
								bool flag11 = this.t < Effect.getEffDataById(this.effId).arrFrame.Length;
								if (flag11)
								{
									this.t++;
								}
							}
							break;
						}
						case 1:
						case 3:
						{
							bool flag12 = this.t < Effect.getEffDataById(this.effId).arrFrame.Length;
							if (flag12)
							{
								this.t++;
							}
							break;
						}
						case 2:
						{
							bool flag13 = this.t < Effect.getEffDataById(this.effId).arrFrame.Length;
							if (flag13)
							{
								this.t++;
							}
							this.tLoopCount++;
							bool flag14 = this.tLoopCount == this.tLoop;
							if (flag14)
							{
								this.tLoopCount = 0;
								this.trans = Res.random(0, 2);
							}
							break;
						}
						case 4:
						{
							this.x = this.c.cx;
							this.y = this.c.cy;
							bool flag15 = this.t < Effect.getEffDataById(this.effId).arrFrame.Length;
							if (flag15)
							{
								this.t++;
							}
							break;
						}
						}
						bool flag16 = this.t == Effect.getEffDataById(this.effId).arrFrame.Length / 2 && (this.effId == 62 || this.effId == 63 || this.effId == 64 || this.effId == 65);
						if (flag16)
						{
							SoundMn.playSound(this.x, this.y, SoundMn.FIREWORK, SoundMn.volume);
						}
						bool flag17 = this.t <= Effect.getEffDataById(this.effId).arrFrame.Length - 1;
						if (flag17)
						{
							this.currFrame = (int)Effect.getEffDataById(this.effId).arrFrame[this.t];
						}
					}
					bool flag18 = this.t >= Effect.getEffDataById(this.effId).arrFrame.Length - 1;
					if (flag18)
					{
						bool flag19 = this.typeEff == 0 || this.typeEff == 3;
						if (flag19)
						{
							this.isPaint = false;
						}
						bool flag20 = this.tLoop == -1;
						if (flag20)
						{
							EffecMn.vEff.removeElement(this);
						}
						bool flag21 = this.typeEff == 2;
						if (flag21)
						{
							this.t = 0;
						}
						else
						{
							bool flag22 = this.typeEff == 1 && this.loop == 1;
							if (flag22)
							{
								this.isPaint = false;
							}
							bool flag23 = this.typeEff == 4;
							if (flag23)
							{
								bool flag24 = this.loop == -1;
								if (flag24)
								{
									this.t = 0;
								}
								else
								{
									this.tLoopCount++;
									bool flag25 = this.tLoopCount == this.tLoop;
									if (flag25)
									{
										this.tLoopCount = 0;
										this.loop--;
										this.t = 0;
										bool flag26 = this.loop == 0;
										if (flag26)
										{
											this.c.removeEffChar(0, this.effId);
										}
									}
								}
							}
							else
							{
								this.isNearPlayer = false;
								bool flag27 = this.loop == -1;
								if (flag27)
								{
									this.tLoopCount++;
									bool flag28 = this.tLoopCount == this.tLoop;
									if (flag28)
									{
										this.tLoopCount = 0;
										this.t = 0;
										bool flag29 = this.tLoop > 1;
										if (flag29)
										{
											this.trans = Res.random(0, 2);
										}
									}
								}
								else
								{
									this.tLoopCount++;
									bool flag30 = this.tLoopCount == this.tLoop;
									if (flag30)
									{
										this.tLoopCount = 0;
										this.loop--;
										this.t = 0;
										bool flag31 = this.loop == 0;
										if (flag31)
										{
											EffecMn.vEff.removeElement(this);
										}
									}
								}
							}
						}
					}
					else
					{
						this.isPaint = true;
					}
				}
			}
		}
		catch (Exception)
		{
			EffecMn.vEff.removeElement(this);
		}
	}

	// Token: 0x04000490 RID: 1168
	public int effId;

	// Token: 0x04000491 RID: 1169
	public int typeEff;

	// Token: 0x04000492 RID: 1170
	public int indexFrom;

	// Token: 0x04000493 RID: 1171
	public int indexTo;

	// Token: 0x04000494 RID: 1172
	public bool isNearPlayer;

	// Token: 0x04000495 RID: 1173
	public const int NEAR_PLAYER = 0;

	// Token: 0x04000496 RID: 1174
	public const int LOOP_NORMAL = 1;

	// Token: 0x04000497 RID: 1175
	public const int LOOP_TRANS = 2;

	// Token: 0x04000498 RID: 1176
	public const int BACKGROUND = 3;

	// Token: 0x04000499 RID: 1177
	public const int CHAR = 4;

	// Token: 0x0400049A RID: 1178
	public const int FIRE_TD = 0;

	// Token: 0x0400049B RID: 1179
	public const int BIRD = 1;

	// Token: 0x0400049C RID: 1180
	public const int FIRE_NAMEK = 2;

	// Token: 0x0400049D RID: 1181
	public const int FIRE_SAYAI = 3;

	// Token: 0x0400049E RID: 1182
	public const int FROG = 5;

	// Token: 0x0400049F RID: 1183
	public const int CA = 4;

	// Token: 0x040004A0 RID: 1184
	public const int ECH = 6;

	// Token: 0x040004A1 RID: 1185
	public const int TACKE = 7;

	// Token: 0x040004A2 RID: 1186
	public const int RAN = 8;

	// Token: 0x040004A3 RID: 1187
	public const int KHI = 9;

	// Token: 0x040004A4 RID: 1188
	public const int GACON = 10;

	// Token: 0x040004A5 RID: 1189
	public const int DANONG = 11;

	// Token: 0x040004A6 RID: 1190
	public const int DANBUOM = 12;

	// Token: 0x040004A7 RID: 1191
	public const int QUA = 13;

	// Token: 0x040004A8 RID: 1192
	public const int THIENTHACH = 14;

	// Token: 0x040004A9 RID: 1193
	public const int CAVOI = 15;

	// Token: 0x040004AA RID: 1194
	public const int NAM = 16;

	// Token: 0x040004AB RID: 1195
	public const int RONGTHAN = 17;

	// Token: 0x040004AC RID: 1196
	public const int BUOMBAY = 26;

	// Token: 0x040004AD RID: 1197
	public const int KHUCGO = 27;

	// Token: 0x040004AE RID: 1198
	public const int DOIBAY = 28;

	// Token: 0x040004AF RID: 1199
	public const int CONMEO = 29;

	// Token: 0x040004B0 RID: 1200
	public const int LUATAT = 30;

	// Token: 0x040004B1 RID: 1201
	public const int ONGCONG = 31;

	// Token: 0x040004B2 RID: 1202
	public const int KHANGIA1 = 42;

	// Token: 0x040004B3 RID: 1203
	public const int KHANGIA2 = 43;

	// Token: 0x040004B4 RID: 1204
	public const int KHANGIA3 = 44;

	// Token: 0x040004B5 RID: 1205
	public const int KHANGIA4 = 45;

	// Token: 0x040004B6 RID: 1206
	public const int KHANGIA5 = 46;

	// Token: 0x040004B7 RID: 1207
	public global::Char c;

	// Token: 0x040004B8 RID: 1208
	public int t;

	// Token: 0x040004B9 RID: 1209
	public int currFrame;

	// Token: 0x040004BA RID: 1210
	public int x;

	// Token: 0x040004BB RID: 1211
	public int y;

	// Token: 0x040004BC RID: 1212
	public int loop;

	// Token: 0x040004BD RID: 1213
	public int tLoop;

	// Token: 0x040004BE RID: 1214
	public int tLoopCount;

	// Token: 0x040004BF RID: 1215
	private bool isPaint = true;

	// Token: 0x040004C0 RID: 1216
	public int layer;

	// Token: 0x040004C1 RID: 1217
	public int isStand;

	// Token: 0x040004C2 RID: 1218
	public static MyVector vEffData = new MyVector();

	// Token: 0x040004C3 RID: 1219
	public int trans;

	// Token: 0x040004C4 RID: 1220
	public static MyVector lastEff = new MyVector();

	// Token: 0x040004C5 RID: 1221
	public static MyVector newEff = new MyVector();

	// Token: 0x040004C6 RID: 1222
	private int[] khangia1 = new int[]
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

	// Token: 0x040004C7 RID: 1223
	private int[] khangia2 = new int[]
	{
		2,
		2,
		2,
		2,
		2,
		3,
		3,
		3,
		3,
		3
	};

	// Token: 0x040004C8 RID: 1224
	private int[] khangia3 = new int[]
	{
		4,
		4,
		4,
		4,
		4,
		5,
		5,
		5,
		5,
		5
	};

	// Token: 0x040004C9 RID: 1225
	private int[] khangia4 = new int[]
	{
		6,
		6,
		6,
		6,
		6,
		7,
		7,
		7,
		7,
		7
	};

	// Token: 0x040004CA RID: 1226
	private int[] khangia5 = new int[]
	{
		8,
		8,
		8,
		8,
		8,
		9,
		9,
		9,
		9,
		9
	};

	// Token: 0x040004CB RID: 1227
	private bool isGetTime;
}
