using System;

// Token: 0x02000080 RID: 128
public class Npc : global::Char
{
	// Token: 0x06000630 RID: 1584 RVA: 0x00066D98 File Offset: 0x00064F98
	public Npc(int npcId, int status, int cx, int cy, int templateId, int avatar)
	{
		this.isShadown = true;
		this.npcId = npcId;
		this.avatar = avatar;
		this.cx = cx;
		this.cy = cy;
		this.xSd = cx;
		this.ySd = cy;
		this.statusMe = status;
		bool flag = npcId != -1;
		if (flag)
		{
			this.template = Npc.arrNpcTemplate[templateId];
		}
		bool flag2 = templateId == 23 || templateId == 42;
		if (flag2)
		{
			this.ch = 45;
		}
		bool flag3 = templateId == 51;
		if (flag3)
		{
			this.isShadown = false;
			this.duaHauIndex = status;
		}
		bool flag4 = this.template != null;
		if (flag4)
		{
			bool flag5 = this.template.name == null;
			if (flag5)
			{
				this.template.name = string.Empty;
			}
			this.template.name = Res.changeString(this.template.name);
		}
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x00066E94 File Offset: 0x00065094
	public void setStatus(sbyte s, int sc)
	{
		this.duaHauIndex = (int)s;
		this.last = (this.cur = mSystem.currentTimeMillis());
		this.seconds = sc;
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x00066EC4 File Offset: 0x000650C4
	public static void clearEffTask()
	{
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			npc.effTask = null;
			npc.indexEffTask = -1;
		}
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x00066F0C File Offset: 0x0006510C
	public override void update()
	{
		bool flag = this.template.npcTemplateId == 51;
		if (flag)
		{
			this.cur = mSystem.currentTimeMillis();
			bool flag2 = this.cur - this.last >= 1000L;
			if (flag2)
			{
				this.seconds--;
				this.last = this.cur;
				bool flag3 = this.seconds < 0;
				if (flag3)
				{
					this.seconds = 0;
				}
			}
		}
		bool isShadown = this.isShadown;
		if (isShadown)
		{
			base.updateShadown();
		}
		bool flag4 = this.effTask == null;
		if (flag4)
		{
			sbyte[] array = new sbyte[]
			{
				-1,
				9,
				9,
				10,
				10,
				11,
				11
			};
			bool flag5 = global::Char.myCharz().ctaskId >= 9 && global::Char.myCharz().ctaskId <= 10 && global::Char.myCharz().nClass.classId > 0 && (int)array[global::Char.myCharz().nClass.classId] == this.template.npcTemplateId;
			if (flag5)
			{
				bool flag6 = global::Char.myCharz().taskMaint == null;
				if (flag6)
				{
					this.effTask = GameScr.efs[57];
					this.indexEffTask = 0;
				}
				else
				{
					bool flag7 = global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.index + 1 == global::Char.myCharz().taskMaint.subNames.Length;
					if (flag7)
					{
						this.effTask = GameScr.efs[62];
						this.indexEffTask = 0;
					}
				}
			}
			else
			{
				sbyte taskNpcId = GameScr.getTaskNpcId();
				bool flag8 = global::Char.myCharz().taskMaint == null && (int)taskNpcId == this.template.npcTemplateId;
				if (flag8)
				{
					this.indexEffTask = 0;
				}
				else
				{
					bool flag9 = global::Char.myCharz().taskMaint != null && (int)taskNpcId == this.template.npcTemplateId;
					if (flag9)
					{
						bool flag10 = global::Char.myCharz().taskMaint.index + 1 == global::Char.myCharz().taskMaint.subNames.Length;
						if (flag10)
						{
							this.effTask = GameScr.efs[98];
						}
						else
						{
							this.effTask = GameScr.efs[98];
						}
						this.indexEffTask = 0;
					}
				}
			}
		}
		base.update();
		bool flag11 = TileMap.mapID != 51;
		if (!flag11)
		{
			bool flag12 = this.cx > global::Char.myCharz().cx;
			if (flag12)
			{
				this.cdir = -1;
			}
			else
			{
				this.cdir = 1;
			}
			bool flag13 = this.template.npcTemplateId % 2 == 0;
			if (flag13)
			{
				bool flag14 = this.cf == 1;
				if (flag14)
				{
					this.cf = 0;
				}
				else
				{
					this.cf = 1;
				}
			}
		}
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x000671D4 File Offset: 0x000653D4
	public void paintHead(mGraphics g, int xStart, int yStart)
	{
		Part part = GameScr.parts[this.template.headId];
		bool flag = this.cdir == 1;
		if (flag)
		{
			SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, GameCanvas.w - 31 - g.getTranslateX(), 2 - g.getTranslateY(), 0, 0);
		}
		else
		{
			SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, GameCanvas.w - 31 - g.getTranslateX(), 2 - g.getTranslateY(), 2, 24);
		}
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x00067280 File Offset: 0x00065480
	public override void paint(mGraphics g)
	{
		bool flag = global::Char.isLoadingMap || this.isHide || !base.isPaint() || this.statusMe == 15;
		if (!flag)
		{
			bool flag2 = this.cTypePk != 0;
			if (flag2)
			{
				base.paint(g);
			}
			else
			{
				bool flag3 = this.template == null;
				if (!flag3)
				{
					bool flag4 = this.template.npcTemplateId != 4 && this.template.npcTemplateId != 51 && this.template.npcTemplateId != 50;
					if (flag4)
					{
						g.drawImage(TileMap.bong, this.cx, this.cy, 3);
					}
					bool flag5 = this.template.npcTemplateId == 3;
					if (flag5)
					{
						SmallImage.drawSmallImage(g, 265, this.cx, this.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
						bool flag6 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this) && ChatPopup.currChatPopup == null;
						if (flag6)
						{
							g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch + 4, mGraphics.BOTTOM | mGraphics.HCENTER);
						}
						this.dyEff = 60;
					}
					else
					{
						bool flag7 = this.template.npcTemplateId != 4;
						if (flag7)
						{
							bool flag8 = this.template.npcTemplateId == 50 || this.template.npcTemplateId == 51;
							if (flag8)
							{
								bool flag9 = this.duahau != null;
								if (flag9)
								{
									bool flag10 = this.template.npcTemplateId == 50 && Npc.mabuEff;
									if (flag10)
									{
										Npc.tMabuEff++;
										bool flag11 = GameCanvas.gameTick % 3 == 0;
										if (flag11)
										{
											Effect me = new Effect(19, this.cx + Res.random(-50, 50), this.cy, 2, 1, -1);
											EffecMn.addEff(me);
										}
										bool flag12 = GameCanvas.gameTick % 15 == 0;
										if (flag12)
										{
											Effect me2 = new Effect(18, this.cx + Res.random(-5, 5), this.cy + Res.random(-90, 0), 2, 1, -1);
											EffecMn.addEff(me2);
										}
										bool flag13 = Npc.tMabuEff == 100;
										if (flag13)
										{
											GameScr.gI().activeSuperPower(this.cx, this.cy);
										}
										bool flag14 = Npc.tMabuEff == 110;
										if (flag14)
										{
											Npc.mabuEff = false;
											this.template.npcTemplateId = 4;
										}
									}
									int num = 0;
									bool flag15 = SmallImage.imgNew[this.duahau[this.duaHauIndex]] != null && SmallImage.imgNew[this.duahau[this.duaHauIndex]].img != null;
									if (flag15)
									{
										num = mGraphics.getImageHeight(SmallImage.imgNew[this.duahau[this.duaHauIndex]].img);
									}
									SmallImage.drawSmallImage(g, this.duahau[this.duaHauIndex], this.cx + Res.random(-1, 1), this.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
									bool flag16 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
									if (flag16)
									{
										bool flag17 = ChatPopup.currChatPopup == null;
										if (flag17)
										{
											g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch - 9 + 16 - num, mGraphics.BOTTOM | mGraphics.HCENTER);
										}
										mFont.tahoma_7b_white.drawString(g, NinjaUtil.getTime(this.seconds), this.cx, this.cy - this.ch - 16 - mFont.tahoma_7.getHeight() - 20 - num + 16, mFont.CENTER, mFont.tahoma_7b_dark);
									}
									else
									{
										mFont.tahoma_7b_white.drawString(g, NinjaUtil.getTime(this.seconds), this.cx, this.cy - this.ch - 8 - mFont.tahoma_7.getHeight() - 20 - num + 16, mFont.CENTER, mFont.tahoma_7b_dark);
									}
								}
							}
							else
							{
								bool flag18 = this.template.npcTemplateId == 6;
								if (flag18)
								{
									SmallImage.drawSmallImage(g, 545, this.cx, this.cy + 5, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
									bool flag19 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this) && ChatPopup.currChatPopup == null;
									if (flag19)
									{
										g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch - 9, mGraphics.BOTTOM | mGraphics.HCENTER);
									}
									mFont.tahoma_7b_white.drawString(g, TileMap.zoneID + string.Empty, this.cx, this.cy - this.ch + 19 - mFont.tahoma_7.getHeight(), mFont.CENTER);
								}
								else
								{
									int headId = this.template.headId;
									int legId = this.template.legId;
									int bodyId = this.template.bodyId;
									Part part = GameScr.parts[headId];
									Part part2 = GameScr.parts[legId];
									Part part3 = GameScr.parts[bodyId];
									bool flag20 = this.cdir == 1;
									if (flag20)
									{
										SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, this.cx + global::Char.CharInfo[this.cf][0][1] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dx, this.cy - global::Char.CharInfo[this.cf][0][2] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dy, 0, 0);
										SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].id, this.cx + global::Char.CharInfo[this.cf][1][1] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dx, this.cy - global::Char.CharInfo[this.cf][1][2] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dy, 0, 0);
										SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].id, this.cx + global::Char.CharInfo[this.cf][2][1] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dx, this.cy - global::Char.CharInfo[this.cf][2][2] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dy, 0, 0);
									}
									else
									{
										SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[this.cf][0][0]].id, this.cx - global::Char.CharInfo[this.cf][0][1] - (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dx, this.cy - global::Char.CharInfo[this.cf][0][2] + (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dy, 2, 24);
										SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].id, this.cx - global::Char.CharInfo[this.cf][1][1] - (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dx, this.cy - global::Char.CharInfo[this.cf][1][2] + (int)part2.pi[global::Char.CharInfo[this.cf][1][0]].dy, 2, 24);
										SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].id, this.cx - global::Char.CharInfo[this.cf][2][1] - (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dx, this.cy - global::Char.CharInfo[this.cf][2][2] + (int)part3.pi[global::Char.CharInfo[this.cf][2][0]].dy, 2, 24);
									}
									bool flag21 = TileMap.mapID != 51;
									if (flag21)
									{
										int num2 = 15;
										bool flag22 = this.template.npcTemplateId == 47;
										if (flag22)
										{
											num2 = 47;
										}
										bool flag23 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
										if (flag23)
										{
											bool flag24 = ChatPopup.currChatPopup == null;
											if (flag24)
											{
												g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch - (num2 - 8), mGraphics.BOTTOM | mGraphics.HCENTER);
											}
										}
										else
										{
											bool flag25 = this.template.npcTemplateId == 47;
											if (flag25)
											{
											}
										}
									}
									this.dyEff = 65;
								}
							}
						}
					}
					bool flag26 = this.indexEffTask < 0 || this.effTask == null || this.cTypePk != 0;
					if (!flag26)
					{
						SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx, this.cy + this.effTask.arrEfInfo[this.indexEffTask].dy - this.dyEff, 0, mGraphics.VCENTER | mGraphics.HCENTER);
						bool flag27 = GameCanvas.gameTick % 2 == 0;
						if (flag27)
						{
							this.indexEffTask++;
							bool flag28 = this.indexEffTask >= this.effTask.arrEfInfo.Length;
							if (flag28)
							{
								this.indexEffTask = 0;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x00067D40 File Offset: 0x00065F40
	public new void paintName(mGraphics g)
	{
		bool flag = global::Char.isLoadingMap || this.isHide || !base.isPaint() || this.statusMe == 15 || this.template == null;
		if (!flag)
		{
			bool flag2 = this.template.npcTemplateId == 3;
			if (flag2)
			{
				bool flag3 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
				if (flag3)
				{
					mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - 5, mFont.CENTER, mFont.tahoma_7_grey);
				}
				else
				{
					mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - 3 - mFont.tahoma_7.getHeight(), mFont.CENTER, mFont.tahoma_7_grey);
				}
				this.dyEff = 60;
			}
			else
			{
				bool flag4 = this.template.npcTemplateId == 4;
				if (!flag4)
				{
					bool flag5 = this.template.npcTemplateId == 50 || this.template.npcTemplateId == 51;
					if (flag5)
					{
						bool flag6 = this.duahau != null;
						if (flag6)
						{
							int num = 0;
							bool flag7 = SmallImage.imgNew[this.duahau[this.duaHauIndex]] != null && SmallImage.imgNew[this.duahau[this.duaHauIndex]].img != null;
							if (flag7)
							{
								num = mGraphics.getImageHeight(SmallImage.imgNew[this.duahau[this.duaHauIndex]].img);
							}
							bool flag8 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
							if (flag8)
							{
								mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - num, mFont.CENTER, mFont.tahoma_7_grey);
							}
							else
							{
								mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - 8 - mFont.tahoma_7.getHeight() - num + 16, mFont.CENTER, mFont.tahoma_7_grey);
							}
						}
					}
					else
					{
						bool flag9 = this.template.npcTemplateId == 6;
						if (flag9)
						{
							bool flag10 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
							if (flag10)
							{
								mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - 16, mFont.CENTER, mFont.tahoma_7_grey);
							}
							else
							{
								mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - 8 - mFont.tahoma_7.getHeight(), mFont.CENTER, mFont.tahoma_7_grey);
							}
						}
						else
						{
							bool flag11 = TileMap.mapID != 51;
							if (flag11)
							{
								int num2 = 15;
								bool flag12 = this.template.npcTemplateId == 47;
								if (flag12)
								{
									num2 = 47;
								}
								bool flag13 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
								if (flag13)
								{
									bool flag14 = TileMap.mapID != 113;
									if (flag14)
									{
										mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - num2, mFont.CENTER, mFont.tahoma_7_grey);
									}
								}
								else
								{
									num2 = 8;
									bool flag15 = this.template.npcTemplateId == 47;
									if (flag15)
									{
										num2 = 40;
									}
									bool flag16 = TileMap.mapID != 113;
									if (flag16)
									{
										mFont.tahoma_7_yellow.drawString(g, this.template.name, this.cx, this.cy - this.ch - num2 - mFont.tahoma_7.getHeight(), mFont.CENTER, mFont.tahoma_7_grey);
									}
								}
							}
							this.dyEff = 65;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x00005463 File Offset: 0x00003663
	public new void hide()
	{
		this.statusMe = 15;
		global::Char.chatPopup = null;
	}

	// Token: 0x04000D9B RID: 3483
	public const sbyte BINH_KHI = 0;

	// Token: 0x04000D9C RID: 3484
	public const sbyte PHONG_CU = 1;

	// Token: 0x04000D9D RID: 3485
	public const sbyte TRANG_SUC = 2;

	// Token: 0x04000D9E RID: 3486
	public const sbyte DUOC_PHAM = 3;

	// Token: 0x04000D9F RID: 3487
	public const sbyte TAP_HOA = 4;

	// Token: 0x04000DA0 RID: 3488
	public const sbyte THU_KHO = 5;

	// Token: 0x04000DA1 RID: 3489
	public const sbyte DA_LUYEN = 6;

	// Token: 0x04000DA2 RID: 3490
	public NpcTemplate template;

	// Token: 0x04000DA3 RID: 3491
	public int npcId;

	// Token: 0x04000DA4 RID: 3492
	public bool isFocus = true;

	// Token: 0x04000DA5 RID: 3493
	public static NpcTemplate[] arrNpcTemplate;

	// Token: 0x04000DA6 RID: 3494
	public int sys;

	// Token: 0x04000DA7 RID: 3495
	public bool isHide;

	// Token: 0x04000DA8 RID: 3496
	private int duaHauIndex;

	// Token: 0x04000DA9 RID: 3497
	private int dyEff;

	// Token: 0x04000DAA RID: 3498
	public static bool mabuEff;

	// Token: 0x04000DAB RID: 3499
	public static int tMabuEff;

	// Token: 0x04000DAC RID: 3500
	private static int[] shock_x = new int[]
	{
		1,
		-1,
		1,
		-1
	};

	// Token: 0x04000DAD RID: 3501
	private static int[] shock_y = new int[]
	{
		1,
		-1,
		-1,
		1
	};

	// Token: 0x04000DAE RID: 3502
	public static int shock_scr;

	// Token: 0x04000DAF RID: 3503
	public int[] duahau;

	// Token: 0x04000DB0 RID: 3504
	public new int seconds;

	// Token: 0x04000DB1 RID: 3505
	public new long last;

	// Token: 0x04000DB2 RID: 3506
	public new long cur;

	// Token: 0x04000DB3 RID: 3507
	public int idItem;
}
