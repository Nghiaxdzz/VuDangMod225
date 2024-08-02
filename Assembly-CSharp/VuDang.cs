using System;
using System.Collections.Generic;
using System.Threading;
using AssemblyCSharp.Mod.Xmap;
using UnityEngine;

// Token: 0x020000C2 RID: 194
internal class VuDang
{
	// Token: 0x060009D2 RID: 2514 RVA: 0x0009DC10 File Offset: 0x0009BE10
	public static void GotoNpc(int npcID)
	{
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			if (npc.template.npcTemplateId == npcID && global::Math.abs(npc.cx - global::Char.myCharz().cx) >= 50)
			{
				VuDang.GotoXY(npc.cx, npc.cy - 1);
				global::Char.myCharz().focusManualTo(npc);
				return;
			}
		}
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x000064DC File Offset: 0x000046DC
	public static void GotoXY(int x, int y)
	{
		global::Char.myCharz().cx = x;
		global::Char.myCharz().cy = y;
		Service.gI().charMove();
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x0009DC8C File Offset: 0x0009BE8C
	public static int FindIndexItem(int idItem)
	{
		for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
		{
			if (global::Char.myCharz().arrItemBag[i] != null && (int)global::Char.myCharz().arrItemBag[i].template.id == idItem)
			{
				return global::Char.myCharz().arrItemBag[i].indexUI;
			}
		}
		return -1;
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x0009DCEC File Offset: 0x0009BEEC
	public static void PhimTat(int key)
	{
		if (key != 0 && GameScr.gI().mobCapcha == null && TField.isQwerty && key == 120)
		{
			GameScr.gI().onChatFromMe("xmp", "xmp");
			return;
		}
		if (GameCanvas.keyAsciiPress == 98)
		{
			for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
			{
				if (global::Char.myCharz().arrItemBag[i] != null && global::Char.myCharz().arrItemBag[i].template.id == 921)
				{
					Service.gI().useItem(0, 1, (sbyte)i, -1);
					Service.gI().petStatus(VuDang.petStatus);
					return;
				}
			}
			for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
			{
				if (global::Char.myCharz().arrItemBag[j] != null && global::Char.myCharz().arrItemBag[j].template.id == 454)
				{
					Service.gI().useItem(0, 1, (sbyte)j, -1);
					Service.gI().petStatus(VuDang.petStatus);
					return;
				}
			}
			GameScr.info1.addInfo("Đéo có bông tai", 0);
		}
		if (GameCanvas.keyAsciiPress == 99)
		{
			for (int k = 0; k < global::Char.myCharz().arrItemBag.Length; k++)
			{
				if (global::Char.myCharz().arrItemBag[k] != null && global::Char.myCharz().arrItemBag[k].template.id == 193)
				{
					Service.gI().useItem(0, 1, (sbyte)k, -1);
					Service.gI().petStatus(VuDang.petStatus);
					return;
				}
			}
			for (int l = 0; l < global::Char.myCharz().arrItemBag.Length; l++)
			{
				if (global::Char.myCharz().arrItemBag[l] != null && global::Char.myCharz().arrItemBag[l].template.id == 194)
				{
					Service.gI().useItem(0, 1, (sbyte)l, -1);
					Service.gI().petStatus(VuDang.petStatus);
					return;
				}
			}
			GameScr.info1.addInfo("Đéo có capsun", 0);
			return;
		}
		if (GameCanvas.keyAsciiPress == 102)
		{
			Service.gI().openUIZone();
			GameCanvas.panel.setTypeZone();
			GameCanvas.panel.show();
		}
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x0009DF10 File Offset: 0x0009C110
	public static void update()
	{
		global::Char.myCharz().cspeed = VuDang.tocdochay;
		VuDang.AutoTTNL();
		VuDang.AutoHoiSinh();
		VuDang.xd();
		VuDang.cd();
		VuDang.UseSkillAuto();
		VuDang.autoFocusBoss();
		VuDang.KSBoss();
		VuDang.KSBossBangSkill5();
		VuDang.BuyItems();
		VuDang.khoaViTri();
		VuDang.gmt();
		VuDang.AutoBT();
		VuDang.AutoCTG();
		VuDang.AutoNhatXa();
		if (VuDang.isAutoAnNho && global::Char.myCharz().cStamina <= 5 && mSystem.currentTimeMillis() - VuDang.currAnNho >= 1000L)
		{
			VuDang.AnNho();
			VuDang.currAnNho = mSystem.currentTimeMillis();
		}
		if (VuDang.isCheckLag)
		{
			VuDang.CheckLag();
		}
		if (VuDang.isKOK && mSystem.currentTimeMillis() - VuDang.currKOK >= 500L)
		{
			VuDang.currKOK = mSystem.currentTimeMillis();
			if (global::Char.myCharz().isCharge || global::Char.myCharz().isFlyAndCharge || global::Char.myCharz().isStandAndCharge || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().cHP <= 0)
			{
				return;
			}
			global::Char.myCharz().cy--;
			Service.gI().charMove();
			global::Char.myCharz().cy++;
			Service.gI().charMove();
		}
		if (VuDang.isAutoCo && mSystem.currentTimeMillis() - VuDang.currAutoFlag >= 500L)
		{
			if (!VuDang.FlagInMap() && global::Char.myCharz().cFlag == 0)
			{
				Service.gI().getFlag(1, 8);
			}
			if (VuDang.FlagInMap() && global::Char.myCharz().cFlag == 8)
			{
				Service.gI().getFlag(1, 0);
			}
			VuDang.currAutoFlag = mSystem.currentTimeMillis();
		}
		if (VuDang.isAutoNeBoss && mSystem.currentTimeMillis() - VuDang.currNeBoss >= 5000L)
		{
			VuDang.NeBoss();
			VuDang.currNeBoss = mSystem.currentTimeMillis();
		}
		if (VuDang.thudau && mSystem.currentTimeMillis() - VuDang.currThuDau >= 500L)
		{
			VuDang.td();
			VuDang.currThuDau = mSystem.currentTimeMillis();
		}
		if (VuDang.aDauDeTu && mSystem.currentTimeMillis() - VuDang.currDauDeTu >= 1000L)
		{
			if ((VuDang.csHPDeTu > global::Char.myPetz().cHP || VuDang.csKIDeTu > global::Char.myPetz().cMP) && global::Char.myPetz().cHP > 0 && VuDang.myPetInMap() != null)
			{
				GameScr.gI().doUseHP();
			}
			VuDang.currDauDeTu = mSystem.currentTimeMillis();
		}
		if (VuDang.aBuffDau && mSystem.currentTimeMillis() - VuDang.currBuffDau >= 1000L)
		{
			if ((VuDang.csHP > global::Char.myCharz().cHP || VuDang.csKI > global::Char.myCharz().cMP) && global::Char.myCharz().cHP > 0)
			{
				GameScr.gI().doUseHP();
			}
			VuDang.currBuffDau = mSystem.currentTimeMillis();
		}
		if (VuDang.isAutoVeKhu && mSystem.currentTimeMillis() - VuDang.currVeKhuCu >= 20000L)
		{
			VuDang.currVeKhuCu = mSystem.currentTimeMillis();
			VuDang.khuVeLai = TileMap.zoneID;
		}
		if ((VuDang.isThuongDeThuong || VuDang.isThuongDeVip) && mSystem.currentTimeMillis() - VuDang.currThuongDe >= 1000L)
		{
			VuDang.quayThuongDe();
			VuDang.currThuongDe = mSystem.currentTimeMillis();
		}
		if (VuDang.doBoss && mSystem.currentTimeMillis() - VuDang.currDoBoss >= 1000L)
		{
			VuDang.DoBoss();
			VuDang.currDoBoss = mSystem.currentTimeMillis();
		}
		if (VuDang.isNoitai)
		{
			VuDang.ntNow = int.Parse(VuDang.CutString(Panel.specialInfo.Substring(0, Panel.specialInfo.IndexOf('%')).LastIndexOf(' ') + 1, Panel.specialInfo.IndexOf('%'), Panel.specialInfo));
		}
		if (VuDang.hoiSinhNgoc && mSystem.currentTimeMillis() - VuDang.currHoiSinh >= 1000L)
		{
			VuDang.HoiSinhTheoNgocChiDinh();
			VuDang.currHoiSinh = mSystem.currentTimeMillis();
		}
		if (VuDang.achat && mSystem.currentTimeMillis() - VuDang.currAutoChat >= 2000L)
		{
			VuDang.AutoChat();
			VuDang.currAutoChat = mSystem.currentTimeMillis();
		}
		if (mSystem.currentTimeMillis() - VuDang.currUpdateKhu >= 1000L)
		{
			VuDang.currUpdateKhu = mSystem.currentTimeMillis();
			VuDang.useItem();
			if (VuDang.isUpdateKhu)
			{
				Service.gI().openUIZone();
			}
			if (global::Char.myCharz().havePet)
			{
				Service.gI().petInfo();
			}
		}
		if (VuDang.isAK)
		{
			VuDang.Ak();
		}
		if (VuDang.isDapDo && VuDang.doDeDap != null)
		{
			VuDang.saoHienTai = VuDang.soSao(VuDang.findItemBagWithIndexUI(VuDang.doDeDap.indexUI));
		}
		else
		{
			VuDang.saoHienTai = -1;
		}
		if (VuDang.isPKM && !VuDang.isGMT && (global::Char.myCharz().charFocus == null || (global::Char.myCharz().charFocus != null && !global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus))))
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char != null && global::Char.myCharz().isMeCanAttackOtherPlayer(@char) && !@char.isPet && !@char.isMiniPet && !@char.cName.StartsWith("$") && !@char.cName.StartsWith("#") && @char.charID >= 0)
				{
					global::Char.myCharz().focusManualTo(@char);
					return;
				}
			}
		}
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x0009E434 File Offset: 0x0009C634
	public static void Paint(mGraphics g)
	{
		int num = 10;
		int num2 = 200;
		mFont.tahoma_7.drawString(g, "Time : " + DateTime.Now, num, GameCanvas.h - num2, mFont.LEFT);
		num2 -= 10;
		mFont.tahoma_7.drawString(g, string.Concat(new object[]
		{
			"Map : ",
			XmapController.get_map_names(TileMap.mapID),
			" [",
			TileMap.mapID,
			"]  - Khu : ",
			TileMap.zoneID
		}), num, GameCanvas.h - num2, mFont.LEFT);
		num2 -= 10;
		mFont.tahoma_7.drawString(g, string.Concat(new object[]
		{
			"Tọa độ X : ",
			global::Char.myCharz().cx,
			" - Y : ",
			global::Char.myCharz().cy
		}), num, GameCanvas.h - num2, mFont.LEFT);
		num2 -= 10;
		mFont.tahoma_7b_yellowSmall2.drawString(g, NinjaUtil.getMoneys((long)global::Char.myCharz().cHP), 90, 5, mFont.LEFT);
		mFont.tahoma_7b_yellowSmall2.drawString(g, NinjaUtil.getMoneys((long)global::Char.myCharz().cMP), 90, 17, mFont.LEFT);
		if (VuDang.isThongTinSuPhu)
		{
			mFont.tahoma_7b_red.drawString(g, "Sư Phụ :", num, GameCanvas.h - 170, mFont.LEFT);
			mFont.tahoma_7b_yellowSmall2.drawString(g, "Sức Mạnh : " + NinjaUtil.getMoneys(global::Char.myCharz().cPower), num, GameCanvas.h - 160, mFont.LEFT);
			mFont.tahoma_7b_yellowSmall2.drawString(g, "Tiềm Năng : " + NinjaUtil.getMoneys(global::Char.myCharz().cTiemNang), num, GameCanvas.h - 150, mFont.LEFT);
			mFont.tahoma_7b_yellowSmall2.drawString(g, string.Concat(new object[]
			{
				"Sức Đánh : ",
				NinjaUtil.getMoneys((long)global::Char.myCharz().cDamFull),
				"  Giáp : ",
				global::Char.myCharz().cDefull
			}), num, GameCanvas.h - 140, mFont.LEFT);
			num += GameCanvas.w / 4;
		}
		if (VuDang.isThongTinDeTu)
		{
			mFont.tahoma_7b_red.drawString(g, "Đệ Tử :", num, GameCanvas.h - 170, mFont.LEFT);
			mFont.tahoma_7b_yellowSmall2.drawString(g, "Sức Mạnh : " + NinjaUtil.getMoneys(global::Char.myPetz().cPower), num, GameCanvas.h - 160, mFont.LEFT);
			mFont.tahoma_7b_yellowSmall2.drawString(g, "Tiềm Năng : " + NinjaUtil.getMoneys(global::Char.myPetz().cTiemNang), num, GameCanvas.h - 150, mFont.LEFT);
			mFont.tahoma_7b_yellowSmall2.drawString(g, string.Concat(new object[]
			{
				"Sức Đánh : ",
				NinjaUtil.getMoneys((long)global::Char.myPetz().cDamFull),
				"  Giáp : ",
				global::Char.myPetz().cDefull
			}), num, GameCanvas.h - 140, mFont.LEFT);
			mFont.tahoma_7b_yellowSmall2.drawString(g, "HP : " + NinjaUtil.getMoneys((long)global::Char.myPetz().cHP), num, GameCanvas.h - 130, mFont.LEFT);
			mFont.tahoma_7b_yellowSmall2.drawString(g, "MP : " + NinjaUtil.getMoneys((long)global::Char.myPetz().cMP), num, GameCanvas.h - 120, mFont.LEFT);
			num += GameCanvas.w / 4;
		}
		if (VuDang.lineboss)
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char != null && @char != null && @char.cTypePk == 5)
				{
					if (global::Char.myCharz().charFocus == @char)
					{
						g.setColor(Color.green);
						g.drawLine(global::Char.myCharz().cx - GameScr.cmx, global::Char.myCharz().cy - GameScr.cmy, @char.cx - GameScr.cmx, @char.cy - GameScr.cmy);
					}
					else
					{
						g.setColor(Color.red);
						g.drawLine(global::Char.myCharz().cx - GameScr.cmx, global::Char.myCharz().cy - GameScr.cmy, @char.cx - GameScr.cmx, @char.cy - GameScr.cmy);
					}
				}
			}
		}
		if (VuDang.thongBaoBoss)
		{
			int num3 = 35;
			for (int j = 0; j < VuDang.bossVip.size(); j++)
			{
				g.setColor(2721889, 0.5f);
				g.fillRect(GameCanvas.w - 23, num3 + 2, 25, 9);
				((ShowBoss)VuDang.bossVip.elementAt(j)).paintBoss(g, GameCanvas.w - 2, num3, mFont.RIGHT);
				num3 += 10;
			}
		}
		if (VuDang.trangThai)
		{
			for (int k = 0; k < GameScr.vCharInMap.size(); k++)
			{
				int num4 = 72;
				global::Char char2 = (global::Char)GameScr.vCharInMap.elementAt(k);
				if (char2 != null && char2 == global::Char.myCharz().charFocus)
				{
					mFont.tahoma_7b_red.drawString(g, string.Concat(new object[]
					{
						char2.cName,
						" [",
						NinjaUtil.getMoneys((long)char2.cHP),
						" / ",
						NinjaUtil.getMoneys((long)char2.cHPFull),
						"] [",
						VuDang.hanhTinhNhanVat(char2),
						"]"
					}), GameCanvas.w / 2, num4, mFont.CENTER);
					num4 += 10;
					if (char2.protectEff)
					{
						mFont.tahoma_7b_red.drawString(g, "Đang khiên: " + char2.timeProtectEff.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
						num4 += 10;
					}
					if (char2.isMonkey == 1)
					{
						mFont.tahoma_7b_red.drawString(g, "Đang biến khỉ: " + char2.timeMonkey.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
						num4 += 10;
					}
					if (char2.sleepEff)
					{
						mFont.tahoma_7b_red.drawString(g, "Bị thôi miên: " + char2.timeSleep.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
						num4 += 10;
					}
					if (char2.holdEffID != 0)
					{
						mFont.tahoma_7b_red.drawString(g, "Bị trói: " + char2.timeBiTroi.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
						num4 += 10;
					}
					if (char2.isFreez)
					{
						mFont.tahoma_7b_red.drawString(g, "Bị TDHS: " + char2.freezSeconds.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
						num4 += 10;
					}
					if (char2.blindEff)
					{
						mFont.tahoma_7b_red.drawString(g, "Bị choáng: " + char2.timeBlind.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
						num4 += 10;
					}
					if (char2.timeHuytSao > 0)
					{
						mFont.tahoma_7b_red.drawString(g, "Có huýt sáo: " + char2.timeHuytSao.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
						num4 += 10;
					}
					if (char2.isNRD)
					{
						mFont.tahoma_7b_red.drawString(g, "Ôm NRD còn: " + char2.timeNRD.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
						num4 += 10;
					}
				}
			}
		}
		if (VuDang.isBossM)
		{
			mFont.tahoma_7b_unfocus.drawString(g, "Boss trong khu:", GameCanvas.w / 2, 72, mFont.CENTER);
			int num5 = 82;
			for (int l = 0; l < GameScr.vCharInMap.size(); l++)
			{
				global::Char char3 = (global::Char)GameScr.vCharInMap.elementAt(l);
				if (char3 != null && (char3.cTypePk == 5 || (char3.charID < 0 && char3.charID > -1000 && char3.charID != -114)) && !char3.isMiniPet)
				{
					mFont.tahoma_7b_red.drawString(g, string.Concat(new object[]
					{
						l,
						" - ",
						char3.isPet ? "$" : "",
						char3.isMiniPet ? "#" : "",
						char3.cName,
						"[ ",
						NinjaUtil.getMoneys((long)char3.cHP).ToString(),
						" / ",
						NinjaUtil.getMoneys((long)char3.cHPFull).ToString(),
						" ] [ ",
						VuDang.hanhTinhNhanVat(char3),
						" ]"
					}), GameCanvas.w / 2, num5, mFont.CENTER);
					num5 += 10;
				}
			}
		}
		if (VuDang.isPKM)
		{
			mFont.tahoma_7b_unfocus.drawString(g, "Địch trong khu:", GameCanvas.w / 2, 72, mFont.CENTER);
			int num6 = 82;
			global::Char char4 = null;
			for (int m = 0; m < GameScr.vCharInMap.size(); m++)
			{
				global::Char char5 = (global::Char)GameScr.vCharInMap.elementAt(m);
				if (char5 != null && global::Char.myCharz().isMeCanAttackOtherPlayer(char5))
				{
					if (global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus == char5)
					{
						char4 = char5;
					}
					g.setColor(Color.red);
					g.drawLine(global::Char.myCharz().cx - GameScr.cmx, global::Char.myCharz().cy - GameScr.cmy, char5.cx - GameScr.cmx, char5.cy - GameScr.cmy);
					mFont.tahoma_7b_red.drawString(g, string.Concat(new object[]
					{
						m,
						" - ",
						char5.isPet ? "$" : "",
						char5.isMiniPet ? "#" : "",
						char5.cName,
						"[",
						NinjaUtil.getMoneys((long)char5.cHP).ToString(),
						" / ",
						NinjaUtil.getMoneys((long)char5.cHPFull).ToString(),
						" ] [ ",
						VuDang.hanhTinhNhanVat(char5),
						" ]"
					}), GameCanvas.w / 2, num6, mFont.CENTER);
					num6 += 10;
				}
			}
			if (char4 != null)
			{
				g.setColor(Color.green);
				g.drawLine(global::Char.myCharz().cx - GameScr.cmx, global::Char.myCharz().cy - GameScr.cmy, char4.cx - GameScr.cmx, char4.cy - GameScr.cmy);
			}
		}
		if (VuDang.isDapDo)
		{
			mFont.tahoma_7b_red.drawString(g, (VuDang.doDeDap != null) ? VuDang.doDeDap.template.name : "Chưa Có", GameCanvas.w / 2, 72, mFont.CENTER);
			mFont.tahoma_7b_red.drawString(g, (VuDang.doDeDap != null) ? ("Số Sao : " + VuDang.saoHienTai.ToString()) : "Số Sao : -1", GameCanvas.w / 2, 82, mFont.CENTER);
			mFont.tahoma_7b_red.drawString(g, "Số Sao Cần Đập : " + VuDang.soSaoCanDap + " Sao", GameCanvas.w / 2, 92, mFont.CENTER);
		}
		if (VuDang.isDapDo || VuDang.isThuongDeThuong || VuDang.isThuongDeVip)
		{
			mFont.tahoma_7b_red.drawString(g, "Ngọc Xanh : " + NinjaUtil.getMoneys((long)global::Char.myCharz().luong) + " Ngọc Hồng : " + NinjaUtil.getMoneys((long)global::Char.myCharz().luongKhoa), GameCanvas.w / 2, 102, mFont.CENTER);
			mFont.tahoma_7b_red.drawString(g, string.Concat(new object[]
			{
				"Vàng : ",
				NinjaUtil.getMoneys(global::Char.myCharz().xu),
				" Thỏi Vàng : ",
				VuDang.thoiVang()
			}), GameCanvas.w / 2, 112, mFont.CENTER);
		}
		if (VuDang.nvat)
		{
			int num7 = 95;
			for (int n = 0; n < VuDang.chars.Length; n++)
			{
				VuDang.chars[n] = null;
			}
			for (int num8 = 0; num8 < GameScr.vCharInMap.size(); num8++)
			{
				global::Char char6 = (global::Char)GameScr.vCharInMap.elementAt(num8);
				if (char6 != null)
				{
					g.fillRect(GameCanvas.w - 155, num7, 150, 10, 2721889, 90);
					if (char6 == global::Char.myCharz().charFocus && char6.cTypePk != 5)
					{
						mFont.tahoma_7b_red.drawString(g, string.Concat(new object[]
						{
							num8,
							".",
							char6.cName,
							"[ ",
							NinjaUtil.getMoneys((long)char6.cHP).ToString(),
							" ] [ ",
							VuDang.hanhTinhNhanVat(char6),
							" ]"
						}), GameCanvas.w - 150, num7, mFont.LEFT);
					}
					else if (char6 == global::Char.myCharz().charFocus && char6.cTypePk == 5)
					{
						mFont.tahoma_7b_red.drawString(g, string.Concat(new object[]
						{
							num8,
							".",
							char6.cName,
							"[ ",
							NinjaUtil.getMoneys((long)char6.cHP).ToString(),
							" ] [ ",
							VuDang.hanhTinhNhanVat(char6),
							" ]"
						}), GameCanvas.w - 150, num7, mFont.LEFT);
					}
					else if (char6.cTypePk == 5 || (char6.charID < 0 && char6.charID > -1000 && char6.charID != -114))
					{
						mFont.tahoma_7b_yellowSmall.drawString(g, string.Concat(new object[]
						{
							num8,
							".",
							char6.cName,
							"[ ",
							NinjaUtil.getMoneys((long)char6.cHP).ToString(),
							" ] [ ",
							VuDang.hanhTinhNhanVat(char6),
							" ]"
						}), GameCanvas.w - 150, num7, mFont.LEFT);
					}
					else if (char6.clanID == global::Char.myCharz().clanID)
					{
						mFont.tahoma_7_blue1.drawString(g, string.Concat(new object[]
						{
							num8,
							".",
							char6.cName,
							"[ ",
							NinjaUtil.getMoneys((long)char6.cHP).ToString(),
							" ] [ ",
							VuDang.hanhTinhNhanVat(char6),
							" ]"
						}), GameCanvas.w - 150, num7, mFont.LEFT);
					}
					else
					{
						mFont.tahoma_7.drawString(g, string.Concat(new object[]
						{
							num8,
							".",
							char6.cName,
							"[ ",
							NinjaUtil.getMoneys((long)char6.cHP).ToString(),
							" ] [ ",
							VuDang.hanhTinhNhanVat(char6),
							" ]"
						}), GameCanvas.w - 150, num7, mFont.LEFT);
					}
					VuDang.chars[num8] = char6;
					num7 += 10;
				}
			}
		}
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x0009F488 File Offset: 0x0009D688
	public static void Ak()
	{
		if (!global::Char.myCharz().stone && !global::Char.isLoadingMap && !global::Char.myCharz().meDead && global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5 && global::Char.myCharz().myskill.template.type != 3 && global::Char.myCharz().myskill.template.id != 10 && global::Char.myCharz().myskill.template.id != 11 && !global::Char.myCharz().myskill.paintCanNotUseSkill)
		{
			int skill = VuDang.getSkill();
			if (mSystem.currentTimeMillis() - VuDang.currTimeAK[skill] > VuDang.getTimeSkill(global::Char.myCharz().myskill))
			{
				if (GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus) && (double)Res.abs(global::Char.myCharz().mobFocus.xFirst - global::Char.myCharz().cx) < (double)global::Char.myCharz().myskill.dx * 1.5)
				{
					global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
					VuDang.AkMob();
					VuDang.currTimeAK[skill] = mSystem.currentTimeMillis();
					return;
				}
				if (global::Char.myCharz().charFocus != null && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus) && (double)Res.abs(global::Char.myCharz().charFocus.cx - global::Char.myCharz().cx) < (double)global::Char.myCharz().myskill.dx * 1.5)
				{
					global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
					VuDang.AkChar();
					VuDang.currTimeAK[skill] = mSystem.currentTimeMillis();
				}
			}
		}
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x0009F664 File Offset: 0x0009D864
	public static void AkChar()
	{
		try
		{
			MyVector myVector = new MyVector();
			myVector.addElement(global::Char.myCharz().charFocus);
			Service.gI().sendPlayerAttack(new MyVector(), myVector, 2);
			global::Char.myCharz().cMP -= global::Char.myCharz().myskill.manaUse;
		}
		catch
		{
		}
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x0009F6D0 File Offset: 0x0009D8D0
	public static void AkMob()
	{
		try
		{
			MyVector myVector = new MyVector();
			myVector.addElement(global::Char.myCharz().mobFocus);
			Service.gI().sendPlayerAttack(myVector, new MyVector(), 1);
			global::Char.myCharz().cMP -= global::Char.myCharz().myskill.manaUse;
		}
		catch
		{
		}
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x0009F73C File Offset: 0x0009D93C
	private static int getSkill()
	{
		for (int i = 0; i < GameScr.keySkill.Length; i++)
		{
			if (GameScr.keySkill[i] == global::Char.myCharz().myskill)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0009F774 File Offset: 0x0009D974
	public static long getTimeSkill(Skill s)
	{
		long result;
		if (s.template.id == 29 || s.template.id == 22 || s.template.id == 7 || s.template.id == 18 || s.template.id == 23)
		{
			result = (long)s.coolDown + 500L;
		}
		else
		{
			long num = (long)((double)s.coolDown * 1.2);
			if (num < 406L)
			{
				result = 406L;
			}
			else
			{
				result = num;
			}
		}
		return result;
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x000064FE File Offset: 0x000046FE
	public static void LoadGame()
	{
		global::Char.myCharz().cspeed = VuDang.tocdochay;
		VuDang.petStatus = 3;
		VuDang.listSkillsAuto.Clear();
		VuDang.listItemAuto.Clear();
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x0009F804 File Offset: 0x0009DA04
	public static void AutoDapDo()
	{
		while (VuDang.isDapDo)
		{
			if (Input.GetKey("q"))
			{
				GameScr.info1.addInfo("Đồ để đập đã reset hãy add đồ", 0);
				VuDang.soSaoCanDap = -1;
				VuDang.doDeDap = null;
			}
			if (TileMap.mapID != 5 && !Pk9rXmap.IsXmapRunning)
			{
				XmapController.StartRunToMapId(5);
			}
			while (TileMap.mapID != 5)
			{
				Thread.Sleep(100);
			}
			if (VuDang.saoHienTai >= VuDang.soSaoCanDap && VuDang.doDeDap != null && VuDang.saoHienTai >= 0 && VuDang.soSaoCanDap > 0)
			{
				Sound.start(1f, Sound.l1);
				GameScr.info1.addInfo("Đồ Cần Đập Đã Đạt Số Sao Yêu Cầu", 0);
				VuDang.soSaoCanDap = -1;
				VuDang.doDeDap = null;
			}
			if (global::Char.myCharz().xu > 200000000L)
			{
				long xu = global::Char.myCharz().xu;
				VuDang.GotoNpc(21);
				if (VuDang.doDeDap != null && VuDang.soSaoCanDap > 0)
				{
					while (!GameCanvas.menu.showMenu)
					{
						Service.gI().combine(1, GameCanvas.panel.vItemCombine);
						Thread.Sleep(500);
					}
					Service.gI().confirmMenu(21, 0);
					GameCanvas.menu.doCloseMenu();
					GameCanvas.panel.currItem = null;
					GameCanvas.panel.chatTField.isShow = false;
				}
			}
			else if (VuDang.doDeDap != null)
			{
				VuDang.BanVang();
			}
			Thread.Sleep(100);
		}
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x0009F968 File Offset: 0x0009DB68
	public static void BanVang()
	{
		VuDang.dangBanVang = true;
		if (TileMap.mapID != 5)
		{
			XmapController.StartRunToMapId(5);
			Thread.Sleep(1000);
		}
		while (TileMap.mapID != 5)
		{
			Thread.Sleep(500);
		}
		if (Input.GetKey("q"))
		{
			GameScr.info1.addInfo("Dừng bán vàng", 0);
			VuDang.dangBanVang = false;
			return;
		}
		while (global::Char.myCharz().xu <= 1500000000L && !Input.GetKey("q"))
		{
			if (VuDang.thoiVang() <= 0)
			{
				GameScr.info1.addInfo("Hết vàng", 0);
				if (VuDang.isDapDo)
				{
					VuDang.isDapDo = false;
					GameScr.info1.addInfo("Đập đồ đã tắt do bạn quá nghèo :v", 0);
				}
				VuDang.dangBanVang = false;
				return;
			}
			Service.gI().saleItem(1, 1, (short)VuDang.FindIndexItem(457));
			Thread.Sleep(500);
			Thread.Sleep(500);
		}
		GameScr.info1.addInfo("Đã bán xong", 0);
		Thread.Sleep(500);
		VuDang.dangBanVang = false;
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x0009FA74 File Offset: 0x0009DC74
	public static int soSao(Item item)
	{
		for (int i = 0; i < item.itemOption.Length; i++)
		{
			if (item.itemOption[i].optionTemplate.id == 107)
			{
				return item.itemOption[i].param;
			}
		}
		return 0;
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x0009FABC File Offset: 0x0009DCBC
	public static int thoiVang()
	{
		int num = 0;
		for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
		{
			Item item = global::Char.myCharz().arrItemBag[i];
			if (item != null && item.template.id == 457)
			{
				num += item.quantity;
			}
		}
		return num;
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x0009FB10 File Offset: 0x0009DD10
	public static string saoTrongBalo(Item item)
	{
		string result;
		if ((item != null && item.template.type <= 5) || item.template.type == 32)
		{
			result = VuDang.soSao(item) + " sao";
		}
		else
		{
			result = "";
		}
		return result;
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x0009FB5C File Offset: 0x0009DD5C
	public static Item findItemBagWithIndexUI(int index)
	{
		foreach (Item item in global::Char.myCharz().arrItemBag)
		{
			if (item != null && item.indexUI == index)
			{
				return item;
			}
		}
		return null;
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x0009FB98 File Offset: 0x0009DD98
	public static void VaoKhu(object okhu)
	{
		try
		{
			VuDang.isVaoKhu = true;
			int zoneID = TileMap.zoneID;
			int mapID = TileMap.mapID;
			int num = (int)okhu;
			if (VuDang.isVaoKhu)
			{
				GameScr.info1.addInfo("Vào Khu: " + num, 0);
			}
			while (TileMap.zoneID == zoneID && TileMap.mapID == mapID && TileMap.zoneID != num)
			{
				if (Input.GetKey("q"))
				{
					GameScr.info1.addInfo("Đã dừng auto vào khu", 0);
					VuDang.isVaoKhu = false;
					return;
				}
				if (!VuDang.isVaoKhu)
				{
					return;
				}
				if (GameScr.gI().numPlayer[num] < GameScr.gI().maxPlayer[num])
				{
					Service.gI().requestChangeZone(num, -1);
				}
				Thread.Sleep(1);
			}
			VuDang.isVaoKhu = false;
		}
		catch (Exception)
		{
			VuDang.isVaoKhu = false;
		}
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x0009FC74 File Offset: 0x0009DE74
	public static string hanhTinhNhanVat(global::Char @char)
	{
		string result;
		if (@char.cTypePk == 5)
		{
			result = "BOSS";
		}
		else if (@char.cgender == 0)
		{
			result = "TD";
		}
		else if (@char.cgender == 1)
		{
			result = "NM";
		}
		else if (@char.cgender == 2)
		{
			result = "XD";
		}
		else
		{
			result = "";
		}
		return result;
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x00006529 File Offset: 0x00004729
	public static void AutoChat()
	{
		if (!string.IsNullOrEmpty(VuDang.textAutoChat))
		{
			Service.gI().chat(VuDang.textAutoChat);
			return;
		}
		GameScr.info1.addInfo("Chưa cài nội dung auto chat", 0);
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x0009FCCC File Offset: 0x0009DECC
	public static void AutoCTG()
	{
		if (VuDang.isAutoCTG && mSystem.currentTimeMillis() - VuDang.currAutoCTG >= 5000L)
		{
			VuDang.currAutoCTG = mSystem.currentTimeMillis();
			if (!string.IsNullOrEmpty(VuDang.textAutoChatTG))
			{
				Service.gI().chatGlobal(VuDang.textAutoChatTG);
				return;
			}
			GameScr.info1.addInfo("Chưa cài nội dung auto chat thế giới", 0);
		}
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x0009FD2C File Offset: 0x0009DF2C
	public static void AutoNhatXa()
	{
		if (VuDang.isAutoNhatXa && mSystem.currentTimeMillis() - VuDang.currNhatXa >= 2000L)
		{
			VuDang.currNhatXa = mSystem.currentTimeMillis();
			for (int i = 0; i < GameScr.vItemMap.size(); i++)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
				if (itemMap != null && itemMap.itemMapID == global::Char.myCharz().charID)
				{
					VuDang.GotoXY(itemMap.x, itemMap.y);
					Service.gI().pickItem(itemMap.itemMapID);
					VuDang.GotoXY(VuDang.xNhatXa, VuDang.yNhatXa);
					return;
				}
				if (itemMap != null)
				{
					VuDang.GotoXY(itemMap.x, itemMap.y);
					Service.gI().pickItem(itemMap.itemMapID);
					VuDang.GotoXY(VuDang.xNhatXa, VuDang.yNhatXa);
					return;
				}
			}
		}
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x0009FE0C File Offset: 0x0009E00C
	public static void BongTai()
	{
		for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
		{
			if (global::Char.myCharz().arrItemBag[i] != null && global::Char.myCharz().arrItemBag[i].template.id == 921)
			{
				Service.gI().useItem(0, 1, (sbyte)i, -1);
				Service.gI().petStatus(VuDang.petStatus);
				return;
			}
		}
		for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
		{
			if (global::Char.myCharz().arrItemBag[j] != null && global::Char.myCharz().arrItemBag[j].template.id == 454)
			{
				Service.gI().useItem(0, 1, (sbyte)j, -1);
				Service.gI().petStatus(VuDang.petStatus);
				return;
			}
		}
		GameScr.info1.addInfo("Đéo có bông tai", 0);
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x0009FEEC File Offset: 0x0009E0EC
	public static void AutoBT()
	{
		if (VuDang.isAutoBT && mSystem.currentTimeMillis() - VuDang.currAutoBT >= (long)(VuDang.timeBT * 1000))
		{
			VuDang.currAutoBT = mSystem.currentTimeMillis();
			VuDang.BongTai();
			if (global::Char.myCharz().isNhapThe)
			{
				VuDang.BongTai();
			}
		}
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x00006557 File Offset: 0x00004757
	public static void gmt()
	{
		if (VuDang.isGMT && GameScr.findCharInMap(VuDang.charMT.charID) != null)
		{
			global::Char.myCharz().focusManualTo(GameScr.findCharInMap(VuDang.charMT.charID));
		}
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x0009FF3C File Offset: 0x0009E13C
	public static Color GetColor()
	{
		string[] array = VuDang.backgroundColor.Split(new char[]
		{
			' '
		});
		return new Color(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0000658A File Offset: 0x0000478A
	public static bool MapNRD()
	{
		return TileMap.mapID >= 85 && TileMap.mapID <= 91;
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x0009FF7C File Offset: 0x0009E17C
	public static int GetIDMap(string mapName)
	{
		int result = -1;
		for (int i = 0; i < VuDang.MapNames.Length; i++)
		{
			if (VuDang.MapNames[i].Trim().ToLower().Equals(mapName.Trim().ToLower()))
			{
				result = i;
			}
		}
		return result;
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x0009FFC4 File Offset: 0x0009E1C4
	public static int MapID(string a)
	{
		for (int i = 0; i < TileMap.mapNames.Length; i++)
		{
			if (TileMap.mapNames[i].Trim().ToLower() == a.Trim().ToLower())
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x000A000C File Offset: 0x0009E20C
	public static void khoaViTri()
	{
		if (VuDang.isKhoaViTri && mSystem.currentTimeMillis() - VuDang.currKhoaViTri >= 600L && global::Char.myCharz().statusMe != 14 && global::Char.myCharz().cHP > 0)
		{
			VuDang.currKhoaViTri = mSystem.currentTimeMillis();
			global::Char.myCharz().cx = VuDang.ghimX;
			global::Char.myCharz().cy = VuDang.ghimY;
			Service.gI().charMove();
		}
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x000A0080 File Offset: 0x0009E280
	public static sbyte typeBuyItem()
	{
		sbyte result;
		if (GameCanvas.panel.currItem.buyCoin > 0)
		{
			result = 0;
		}
		else if (GameCanvas.panel.currItem.buyGold > 0)
		{
			result = 1;
		}
		else if (GameCanvas.panel.currItem.buySpec > 0)
		{
			result = 3;
		}
		else
		{
			result = -1;
		}
		return result;
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x000A00D4 File Offset: 0x0009E2D4
	public static void BuyItems()
	{
		if (VuDang.itemBuy != null && VuDang.countBuyItem > 0 && mSystem.currentTimeMillis() - VuDang.currBuyItem >= 500L)
		{
			VuDang.currBuyItem = mSystem.currentTimeMillis();
			if (Input.GetKey("q"))
			{
				VuDang.countBuyItem = 0;
				VuDang.itemBuy = null;
				GameScr.info1.addInfo("Dừng Mua Item", 0);
				return;
			}
			Service.gI().buyItem(VuDang.typeBuyItem(), (int)VuDang.itemBuy.template.id, 0);
			VuDang.countBuyItem--;
			if (VuDang.countBuyItem == 0)
			{
				GameScr.info1.addInfo("Đã Mua Xong !", 0);
				VuDang.itemBuy = null;
			}
		}
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x000A0184 File Offset: 0x0009E384
	public static void KSBoss()
	{
		if (VuDang.isKSBoss)
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (VuDang.HPKSBoss == 0)
				{
					VuDang.isKSBoss = false;
					GameScr.info1.addInfo("HP Boss = 0 thì ks sao ba =))", 0);
					GameScr.info1.addInfo("KS Boss đã tắt", 0);
					return;
				}
				if (@char != null && @char.charID < 0 && @char.cTypePk == 5 && !@char.cName.StartsWith("Đ") && @char.cHP <= VuDang.HPKSBoss && @char.cHP > 0)
				{
					VuDang.GetSkillByIconID(539);
					if (global::Math.abs(@char.cx - global::Char.myCharz().cx) >= 25)
					{
						VuDang.GotoXY(@char.cx, @char.cy - 1);
					}
					global::Char.myCharz().focusManualTo(@char);
					VuDang.Ak();
				}
			}
		}
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x000A0280 File Offset: 0x0009E480
	private static sbyte ulti()
	{
		sbyte result;
		if (global::Char.myCharz().cgender == 0)
		{
			result = 10;
		}
		else if (global::Char.myCharz().cgender == 1)
		{
			result = 11;
		}
		else
		{
			result = 14;
		}
		return result;
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x000A02B4 File Offset: 0x0009E4B4
	public static void KSBossBangSkill5()
	{
		if (VuDang.isKSBossBangSkill5)
		{
			global::Char @char = VuDang.BossInMap();
			if ((!global::Char.myCharz().isStandAndCharge && !global::Char.myCharz().isCharge && !global::Char.myCharz().isFlyAndCharge && VuDang.GetCoolDownSkill(VuDang.GetSkillByID(VuDang.ulti())) <= 0) || VuDang.GetSkillByID(VuDang.ulti()) != null || @char != null)
			{
				if (VuDang.HPKSBoss == 0)
				{
					VuDang.isKSBossBangSkill5 = false;
					GameScr.info1.addInfo("HP Boss = 0 thì ks sao ba =))", 0);
					GameScr.info1.addInfo("KS Boss đã tắt", 0);
					return;
				}
				if ((@char.cHP <= VuDang.HPKSBoss || (VuDang.DameToBoss() + global::Char.myCharz().cHPFull) / 2 >= @char.cHP) && VuDang.GetSkillByID(VuDang.ulti()) != null)
				{
					if (global::Math.abs(@char.cx - global::Char.myCharz().cx) >= 500)
					{
						VuDang.GotoXY(@char.cx, @char.cy - 1);
					}
					global::Char.myCharz().focusManualTo(@char);
					VuDang.UseSkill(VuDang.GetSkillByID(VuDang.ulti()));
				}
			}
		}
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x000A03C4 File Offset: 0x0009E5C4
	public static global::Char BossInMap()
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char.cTypePk == 5 && @char != null && @char.cHP > 0 && global::Char.myCharz().isMeCanAttackOtherPlayer(@char))
			{
				return @char;
			}
		}
		return null;
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x000A041C File Offset: 0x0009E61C
	private static int DameToBoss()
	{
		int num = -global::Char.myCharz().cHPFull;
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char != null && @char.isStandAndCharge && global::Char.myCharz().cgender == 2 && VuDang.BossInMap() != null)
			{
				if (num < 0)
				{
					num = 0;
				}
				num += @char.cHPFull;
			}
		}
		return num;
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x000065A3 File Offset: 0x000047A3
	public static void UseSkill(Skill sk)
	{
		if (global::Char.myCharz().myskill != sk)
		{
			GameScr.gI().doSelectSkill(sk, true);
			GameScr.gI().doSelectSkill(sk, true);
			return;
		}
		GameScr.gI().doSelectSkill(sk, true);
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x000A048C File Offset: 0x0009E68C
	public static Skill GetSkillByID(sbyte idSkill)
	{
		for (int i = 0; i < GameScr.keySkill.Length; i++)
		{
			if (GameScr.keySkill[i] != null && GameScr.keySkill[i].template.id == idSkill)
			{
				return GameScr.keySkill[i];
			}
		}
		return null;
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x000A04D4 File Offset: 0x0009E6D4
	public static Skill GetSkillByIconID(int iconID)
	{
		for (int i = 0; i < GameScr.keySkill.Length; i++)
		{
			if (GameScr.keySkill[i] != null && GameScr.keySkill[i].template.iconId == iconID)
			{
				return GameScr.keySkill[i];
			}
		}
		return null;
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x000065D7 File Offset: 0x000047D7
	public static int GetCoolDownSkill(Skill skill)
	{
		return (int)((long)skill.coolDown - mSystem.currentTimeMillis() + skill.lastTimeUseThisSkill);
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x000A051C File Offset: 0x0009E71C
	public static void HoiSinhTheoNgocChiDinh()
	{
		if (VuDang.ngocHienTai == 0 || global::Char.myCharz().luongKhoa + global::Char.myCharz().luong == 0 || VuDang.ngocDuocDungDeHoiSinh == 0)
		{
			GameScr.info1.addInfo("Không còn ngọc để hồi sinh hoặc chưa set up số ngọc được phép sử dụng hoặc số ngọc được hồi sinh đã dùng hết", 0);
			GameScr.info1.addInfo("Đã tắt tự hồi sinh với số ngọc chỉ định", 0);
			VuDang.hoiSinhNgoc = false;
		}
		if (VuDang.ngocDuocDungDeHoiSinh > 0 && global::Char.myCharz().cHP <= 0 && global::Char.myCharz().statusMe == 14)
		{
			Service.gI().wakeUpFromDead();
			VuDang.ngocDuocDungDeHoiSinh--;
		}
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x000A05B0 File Offset: 0x0009E7B0
	public static void AutoNoiTai()
	{
		while (VuDang.isNoitai)
		{
			if (VuDang.ntMin == 0)
			{
				GameScr.info1.addInfo("Chưa set up chỉ số nội tại", 0);
				VuDang.isNoitai = false;
				return;
			}
			if (Panel.specialInfo.Contains(VuDang.tennoitaicanmo) && VuDang.ntMin <= VuDang.ntNow)
			{
				GameScr.info1.addInfo("Đã Ra Nội Tại Cần Mở", 0);
				VuDang.isNoitai = false;
				return;
			}
			if (Input.GetKey("q"))
			{
				GameScr.info1.addInfo("Auto Mở Nội Tại Đã Tắt", 0);
				VuDang.isNoitai = false;
				return;
			}
			Service.gI().speacialSkill(0);
			Service.gI().confirmMenu(5, 2);
			Service.gI().confirmMenu(5, 0);
			Thread.Sleep(700);
		}
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x000A0670 File Offset: 0x0009E870
	public static string CutString(int start, int end, string s)
	{
		string text = "";
		for (int i = start; i < end; i++)
		{
			text += s[i].ToString();
		}
		return text;
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x000A06A8 File Offset: 0x0009E8A8
	public static void DoBoss()
	{
		if (string.IsNullOrEmpty(VuDang.bossCanDo))
		{
			GameScr.info1.addInfo("Chưa nhập boss cần tìm", 0);
			VuDang.zoneMacDinh = 0;
			VuDang.doBoss = false;
			return;
		}
		if (Input.GetKey("q"))
		{
			GameScr.info1.addInfo("Đã tắt auto dò boss", 0);
			VuDang.doBoss = false;
			return;
		}
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char != null && @char.cName.ToLower().Contains(VuDang.bossCanDo.ToLower()) && @char.cTypePk == 5)
			{
				Sound.start(1f, Sound.l1);
				GameScr.info1.addInfo("Đã tìm thấy boss", 0);
				VuDang.zoneMacDinh = 0;
				VuDang.doBoss = false;
				return;
			}
		}
		Service.gI().requestChangeZone(VuDang.zoneMacDinh, -1);
		if (!global::Char.isLoadingMap && TileMap.zoneID == VuDang.zoneMacDinh)
		{
			VuDang.zoneMacDinh++;
		}
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x000A07AC File Offset: 0x0009E9AC
	public static int getX(sbyte type)
	{
		int i = 0;
		while (i < TileMap.vGo.size())
		{
			Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
			int result;
			if (waypoint.maxX < 60 && type == 0)
			{
				result = 15;
			}
			else if ((int)waypoint.minX <= TileMap.pxw - 60 && waypoint.maxX >= 60 && type == 1)
			{
				result = (int)((waypoint.minX + waypoint.maxX) / 2);
			}
			else
			{
				if ((int)waypoint.minX <= TileMap.pxw - 60 || type != 2)
				{
					if (type == 3)
					{
						if (waypoint.maxX < 60)
						{
							return 15;
						}
						if ((int)waypoint.minX > TileMap.pxw - 60)
						{
							return TileMap.pxw - 15;
						}
					}
					i++;
					continue;
				}
				result = TileMap.pxw - 15;
			}
			return result;
		}
		return 0;
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x000A0874 File Offset: 0x0009EA74
	public static int getY(sbyte type)
	{
		int i = 0;
		while (i < TileMap.vGo.size())
		{
			Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
			int maxY;
			if (waypoint.maxX < 60 && type == 0)
			{
				maxY = (int)waypoint.maxY;
			}
			else if ((int)waypoint.minX <= TileMap.pxw - 60 && waypoint.maxX >= 60 && type == 1)
			{
				maxY = (int)waypoint.maxY;
			}
			else
			{
				if ((int)waypoint.minX <= TileMap.pxw - 60 || type != 2)
				{
					if (type == 3 && (int)waypoint.maxY != global::Char.myCharz().cy)
					{
						if (waypoint.maxX < 60)
						{
							return (int)waypoint.maxY;
						}
						if ((int)waypoint.minX > TileMap.pxw - 60)
						{
							return (int)waypoint.maxY;
						}
					}
					i++;
					continue;
				}
				maxY = (int)waypoint.maxY;
			}
			return maxY;
		}
		return 0;
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x000A094C File Offset: 0x0009EB4C
	public static void autoFocusBoss()
	{
		if (VuDang.focusBoss && mSystem.currentTimeMillis() - VuDang.currFocusBoss >= 500L)
		{
			VuDang.currFocusBoss = mSystem.currentTimeMillis();
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (@char != null && @char.cTypePk == 5 && !@char.cName.StartsWith("Đ"))
				{
					global::Char.myCharz().focusManualTo(@char);
					return;
				}
			}
		}
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x000A09D0 File Offset: 0x0009EBD0
	public static void quayThuongDe()
	{
		if (!VuDang.dangBanVang && !VuDang.dangNhanAll && !VuDang.dangMoMenu)
		{
			if (!VuDang.isPaintCrackBall && TileMap.mapID == 45)
			{
				new Thread(new ThreadStart(VuDang.OpenMenuThuongDe)).Start();
				return;
			}
			if (TileMap.mapID != 45 && !Pk9rXmap.IsXmapRunning)
			{
				XmapController.StartRunToMapId(45);
				return;
			}
			if (TileMap.mapID == 45)
			{
				if (Input.GetKey("q"))
				{
					GameScr.info1.addInfo("Auto đã tắt", 0);
					VuDang.isThuongDeVip = false;
					VuDang.isThuongDeThuong = false;
					return;
				}
				if (global::Char.myCharz().xu <= 175000000L && VuDang.isThuongDeThuong)
				{
					new Thread(new ThreadStart(VuDang.BanVang)).Start();
					return;
				}
				VuDang.GotoNpc(19);
				Service.gI().openMenu(19);
				Service.gI().SendCrackBall(2, 7);
			}
		}
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x000A0ABC File Offset: 0x0009ECBC
	public static void NhanAllThuongDe()
	{
		VuDang.dangNhanAll = true;
		Service.gI().openMenu(19);
		Service.gI().confirmMenu(19, 1);
		Service.gI().confirmMenu(19, 3);
		Service.gI().buyItem(2, 0, 0);
		VuDang.dangNhanAll = false;
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x000A0B08 File Offset: 0x0009ED08
	private static void OpenMenuThuongDe()
	{
		VuDang.dangMoMenu = true;
		Service.gI().openMenu(19);
		Service.gI().confirmMenu(19, 1);
		if (VuDang.isThuongDeThuong)
		{
			Service.gI().confirmMenu(19, 1);
		}
		if (VuDang.isThuongDeVip)
		{
			Service.gI().confirmMenu(19, 2);
		}
		VuDang.dangMoMenu = false;
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x000A0B64 File Offset: 0x0009ED64
	public static void addSet1(Item item)
	{
		foreach (VuDang.setDo1 setDo in VuDang.ListSet1)
		{
			if (setDo.type == (int)item.template.type && setDo.info == item.info && setDo.id == (int)item.template.id && setDo.name == item.template.name && setDo.option == item.VuDangItemOption() && setDo.soSao == item.VuDangSoSao())
			{
				VuDang.ListSet1.Remove(setDo);
				GameCanvas.startOKDlg("Đã xóa " + item.template.name + " khỏi set 1");
			}
		}
		VuDang.ListSet1.Add(new VuDang.setDo1((int)item.template.type, item.info, (int)item.template.id, item.template.name, item.VuDangItemOption(), item.VuDangSoSao()));
		GameCanvas.startOKDlg("Đã thêm " + item.template.name + " vào set 1");
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x000A0CB8 File Offset: 0x0009EEB8
	public static void macSet1()
	{
		foreach (VuDang.setDo1 setDo in VuDang.ListSet1)
		{
			Item[] arrItemBag = global::Char.myCharz().arrItemBag;
			try
			{
				for (int i = 0; i < arrItemBag.Length; i++)
				{
					if ((int)arrItemBag[i].template.type == setDo.type && arrItemBag[i].info == setDo.info && (int)arrItemBag[i].template.id == setDo.id && arrItemBag[i].template.name == setDo.name && arrItemBag[i].VuDangItemOption() == setDo.option && arrItemBag[i].VuDangSoSao() == setDo.soSao)
					{
						Service.gI().getItem(4, (sbyte)i);
						Thread.Sleep(500);
					}
				}
			}
			catch
			{
			}
		}
		GameScr.info1.addInfo("Đã mặc set 1", 0);
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x000A0DE4 File Offset: 0x0009EFE4
	public static void addSet2(Item item)
	{
		foreach (VuDang.setDo2 setDo in VuDang.ListSet2)
		{
			if (setDo.name == item.template.name && setDo.type == (int)item.template.type && setDo.info == item.info && setDo.id == (int)item.template.id && setDo.option == item.VuDangItemOption() && setDo.soSao == item.VuDangSoSao())
			{
				VuDang.ListSet2.Remove(setDo);
				GameCanvas.startOKDlg("Đã xóa " + item.template.name + " khỏi set 2");
			}
		}
		VuDang.ListSet2.Add(new VuDang.setDo2((int)item.template.type, item.info, (int)item.template.id, item.template.name, item.VuDangItemOption(), item.VuDangSoSao()));
		GameCanvas.startOKDlg("Đã thêm " + item.template.name + " vào set 2");
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x000A0F38 File Offset: 0x0009F138
	public static void macSet2()
	{
		foreach (VuDang.setDo2 setDo in VuDang.ListSet2)
		{
			Item[] arrItemBag = global::Char.myCharz().arrItemBag;
			try
			{
				for (int i = 0; i < arrItemBag.Length; i++)
				{
					if ((int)arrItemBag[i].template.type == setDo.type && arrItemBag[i].info == setDo.info && (int)arrItemBag[i].template.id == setDo.id && arrItemBag[i].template.name == setDo.name && arrItemBag[i].VuDangItemOption() == setDo.option && arrItemBag[i].VuDangSoSao() == setDo.soSao)
					{
						Service.gI().getItem(4, (sbyte)i);
						Thread.Sleep(500);
					}
				}
			}
			catch
			{
			}
		}
		GameScr.info1.addInfo("Đã mặc set 2", 0);
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x000A1064 File Offset: 0x0009F264
	public static void AddItem(Item item)
	{
		foreach (VuDang.ItemAuto itemAuto in VuDang.listItemAuto)
		{
			if (itemAuto.iconID == (int)item.template.iconID && itemAuto.id == (int)item.template.id)
			{
				VuDang.listItemAuto.Remove(itemAuto);
				GameCanvas.startOKDlg("Đã xóa " + item.template.name + " khỏi list item auto");
				return;
			}
		}
		VuDang.listItemAuto.Add(new VuDang.ItemAuto((int)item.template.iconID, (int)item.template.id));
		GameCanvas.startOKDlg("Đã thêm " + item.template.name + " vào list item auto");
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x000A1148 File Offset: 0x0009F348
	public static void useItem()
	{
		if (VuDang.listItemAuto.Count > 0)
		{
			for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
			{
				Item item = global::Char.myCharz().arrItemBag[i];
				foreach (VuDang.ItemAuto itemAuto in VuDang.listItemAuto)
				{
					if (item != null && (int)item.template.iconID == itemAuto.iconID && (int)item.template.id == itemAuto.id && !ItemTime.isExistItem((int)item.template.iconID))
					{
						Service.gI().useItem(0, 1, (sbyte)VuDang.FindIndexItem((int)item.template.id), -1);
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x000A1228 File Offset: 0x0009F428
	public static void AutoLogin()
	{
		VuDang.dangLogin = true;
		Thread.Sleep(1000);
		GameCanvas.startOKDlg("Vui Lòng Đợi 25 Giây...");
		Thread.Sleep(23000);
		while (ServerListScreen.testConnect != 2)
		{
			GameCanvas.serverScreen.switchToMe();
			Thread.Sleep(1000);
		}
		if (GameCanvas.loginScr == null)
		{
			GameCanvas.loginScr = new LoginScr();
		}
		Thread.Sleep(1000);
		GameCanvas.loginScr.switchToMe();
		GameCanvas.loginScr.doLogin();
		VuDang.dangLogin = false;
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x000A12AC File Offset: 0x0009F4AC
	public static void VeKhu()
	{
		if (VuDang.isAutoVeKhu)
		{
			int num = -1;
			try
			{
				num = VuDang.khuVeLai;
				goto IL_20;
			}
			catch
			{
				goto IL_20;
			}
			IL_14:
			Service.gI().requestChangeZone(num, -1);
			IL_20:
			if (num >= 0 && TileMap.zoneID != num)
			{
				goto IL_14;
			}
		}
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x000A12F8 File Offset: 0x0009F4F8
	public static void AutoTTNL()
	{
		if (VuDang.isAutoTTNL && mSystem.currentTimeMillis() - VuDang.currAutoTTNL >= 1000L)
		{
			VuDang.currAutoTTNL = mSystem.currentTimeMillis();
			if (global::Char.myCharz().cgender != 2)
			{
				GameScr.info1.addInfo("Hành tinh acc này không phải xayda", 0);
				GameScr.info1.addInfo("Auto ttnl đang tắt", 0);
				VuDang.isAutoTTNL = false;
				return;
			}
			if (!global::Char.myCharz().isStandAndCharge && !global::Char.myCharz().isCharge && !global::Char.myCharz().isFlyAndCharge && !global::Char.myCharz().stone && global::Char.myCharz().holdEffID == 0 && !global::Char.myCharz().blindEff && !global::Char.myCharz().sleepEff && global::Char.myCharz().cHP > 0 && global::Char.myCharz().statusMe != 14)
			{
				if ((global::Char.myCharz().cHP < global::Char.myCharz().cHPFull || global::Char.myCharz().cMP < global::Char.myCharz().cMPFull) && VuDang.GetCoolDownSkill(VuDang.GetSkillByIconID(720)) <= 0)
				{
					VuDang.UseSkill(VuDang.GetSkillByIconID(720));
					return;
				}
				if (global::Char.myCharz().myskill != VuDang.GetSkillByIconID(539))
				{
					GameScr.gI().doSelectSkill(VuDang.GetSkillByIconID(539), true);
				}
			}
		}
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x000A1464 File Offset: 0x0009F664
	public static global::Char myPetInMap()
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char != null && @char.charID == -global::Char.myCharz().charID)
			{
				return @char;
			}
		}
		return null;
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x000A14B0 File Offset: 0x0009F6B0
	private static void UseSkillAuto()
	{
		if (Input.GetKey("q") && VuDang.listSkillsAuto.Count > 0)
		{
			VuDang.listSkillsAuto.Clear();
			GameScr.info1.addInfo("Đã reset skill auto", 0);
			return;
		}
		foreach (Skill skill in VuDang.listSkillsAuto)
		{
			if (global::Char.myCharz().isStandAndCharge || global::Char.myCharz().isCharge || global::Char.myCharz().isFlyAndCharge || global::Char.myCharz().stone)
			{
				break;
			}
			if (global::Char.myCharz().holdEffID != 0)
			{
				break;
			}
			if (global::Char.myCharz().blindEff)
			{
				break;
			}
			if (global::Char.myCharz().sleepEff)
			{
				break;
			}
			if (global::Char.myCharz().cHP <= 0)
			{
				break;
			}
			if (global::Char.myCharz().statusMe == 14)
			{
				break;
			}
			if (TileMap.mapID == global::Char.myCharz().cgender + 21)
			{
				break;
			}
			if ((VuDang.GetCoolDownSkill(skill) <= 0 && skill.template.type == 3) || skill.lastTimeUseThisSkill == 0L)
			{
				VuDang.UseSkill(skill);
			}
		}
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x000A15FC File Offset: 0x0009F7FC
	public static void AddRemoveSkill(int indexSkill)
	{
		if (VuDang.listSkillsAuto.Contains(GameScr.keySkill[indexSkill]))
		{
			VuDang.listSkillsAuto.Remove(GameScr.keySkill[indexSkill]);
			GameScr.info1.addInfo("Đã xóa " + GameScr.keySkill[indexSkill].template.name + " khỏi list skill auto", 0);
			GameScr.VuDangMenuMod();
			return;
		}
		VuDang.listSkillsAuto.Add(GameScr.keySkill[indexSkill]);
		GameScr.info1.addInfo("Đã thêm " + GameScr.keySkill[indexSkill].template.name + " vào list skill auto", 0);
		GameScr.VuDangMenuMod();
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x000065EE File Offset: 0x000047EE
	public static void xd()
	{
		if (VuDang.xindau && mSystem.currentTimeMillis() - VuDang.currXinDau >= 30000L)
		{
			VuDang.currXinDau = mSystem.currentTimeMillis();
			Service.gI().clanMessage(1, "", -1);
		}
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x000A16A0 File Offset: 0x0009F8A0
	public static void td()
	{
		if (TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23)
		{
			int num = 0;
			for (int i = 0; i < global::Char.myCharz().arrItemBox.Length; i++)
			{
				Item item = global::Char.myCharz().arrItemBox[i];
				if (item != null && item.template.type == 6)
				{
					num += item.quantity;
				}
			}
			if (num < 20 && GameCanvas.gameTick % 200 == 0)
			{
				for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
				{
					Item item2 = global::Char.myCharz().arrItemBag[j];
					if (item2 != null && item2.template.type == 6)
					{
						Service.gI().getItem(1, (sbyte)j);
					}
				}
			}
			if ((GameScr.gI().magicTree.currPeas > 0 && GameScr.hpPotion < 10) || (num < 20 && GameCanvas.gameTick % 100 == 0))
			{
				Service.gI().openMenu(4);
				Service.gI().confirmMenu(4, 0);
			}
		}
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x000A17A4 File Offset: 0x0009F9A4
	public static void cd()
	{
		if (VuDang.chodau && mSystem.currentTimeMillis() - VuDang.currChoDau >= 500L)
		{
			VuDang.currChoDau = mSystem.currentTimeMillis();
			for (int i = 0; i < ClanMessage.vMessage.size(); i++)
			{
				ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(i);
				if (clanMessage.maxCap != 0 && clanMessage.playerName != global::Char.myCharz().cName && clanMessage.recieve != clanMessage.maxCap)
				{
					Service.gI().clanDonate(clanMessage.id);
					return;
				}
			}
		}
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x000A183C File Offset: 0x0009FA3C
	public static void NeBoss()
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			char c = char.Parse(@char.cName.Substring(0, 1));
			if (@char != null && c >= 'A' && c <= 'Z' && !@char.cName.StartsWith("Đệ tử") && @char.cTypePk == 5 && !@char.isMiniPet)
			{
				Service.gI().requestChangeZone(Res.random(0, GameScr.gI().zones.Length), -1);
				return;
			}
		}
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x000A18D4 File Offset: 0x0009FAD4
	public static bool FlagInMap()
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char != null && @char.cFlag != 0 && @char.charID > 0 && ((global::Math.abs(@char.cx - global::Char.myCharz().cx) <= 500 && global::Math.abs(@char.cy - global::Char.myCharz().cy) <= 500) || (global::Math.abs(@char.cx - VuDang.myPetInMap().cx) <= 500 && global::Math.abs(@char.cy - VuDang.myPetInMap().cy) <= 500)))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x000A199C File Offset: 0x0009FB9C
	public static void CheckLag()
	{
		if (mSystem.currentTimeMillis() - VuDang.lastTimeCheckLag >= 1000L && GameCanvas.currentScreen == GameScr.instance)
		{
			VuDang.currentCheckLag--;
			if (VuDang.currentCheckLag <= 0)
			{
				Session_ME.gI().close();
				Session_ME2.gI().close();
				GameCanvas.gI().onDisconnected();
				VuDang.currentCheckLag = 30;
			}
			VuDang.lastTimeCheckLag = mSystem.currentTimeMillis();
		}
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x000A1A0C File Offset: 0x0009FC0C
	private static void AutoHoiSinh()
	{
		if (VuDang.autoHoiSinh && (global::Char.myCharz().cHP <= 0 || global::Char.myCharz().meDead || global::Char.myCharz().statusMe == 14) && mSystem.currentTimeMillis() - VuDang.currAutoHoiSinh >= 300L)
		{
			VuDang.currAutoHoiSinh = mSystem.currentTimeMillis();
			Service.gI().wakeUpFromDead();
		}
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x000A1A70 File Offset: 0x0009FC70
	public static void AnNho()
	{
		try
		{
			for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
			{
				if (global::Char.myCharz().arrItemBag[i] != null && global::Char.myCharz().arrItemBag[i].template.id == 212)
				{
					Service.gI().useItem(0, 1, (sbyte)i, -1);
					break;
				}
			}
		}
		catch
		{
			try
			{
				for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
				{
					if (global::Char.myCharz().arrItemBag[j] != null && global::Char.myCharz().arrItemBag[j].template.id == 211)
					{
						Service.gI().useItem(0, 1, (sbyte)j, -1);
						break;
					}
				}
			}
			catch
			{
				GameScr.info1.addInfo("Không có nho trong balo", 0);
			}
		}
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x000A1B5C File Offset: 0x0009FD5C
	public static void GoBack()
	{
		Thread.Sleep(5000);
		if (!GameScr.gI().magicTree.isUpdate && GameScr.gI().magicTree.currPeas > 0 && TileMap.mapID == global::Char.myCharz().cgender + 21)
		{
			Service.gI().magicTree(1);
			Thread.Sleep(500);
			GameCanvas.gI().keyPressedz(-5);
			Thread.Sleep(1000);
		}
		for (int i = 0; i < GameScr.vItemMap.size(); i++)
		{
			ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
			global::Char.myCharz().cx = itemMap.x;
			Service.gI().charMove();
			Thread.Sleep(1000);
			Service.gI().pickItem(itemMap.itemMapID);
			Thread.Sleep(1000);
		}
		XmapController.StartRunToMapId(VuDang.IdmapGB);
		while (TileMap.mapID != VuDang.IdmapGB)
		{
			Thread.Sleep(100);
		}
		new Thread(new ThreadStart(VuDang.DoiLaiKhu)).Start();
		GameScr.isAutoPlay = true;
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x000A1C74 File Offset: 0x0009FE74
	public static void DoiLaiKhu()
	{
		while (TileMap.zoneID != VuDang.ZoneGB)
		{
			Thread.Sleep(1000);
			Service.gI().requestChangeZone(VuDang.ZoneGB, -1);
		}
		Thread.Sleep(2000);
		VuDang.GotoXY(VuDang.xGB, VuDang.yGB);
		GameScr.isAutoPlay = true;
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x000A1E0C File Offset: 0x000A000C
	public static void Capsun()
	{
		for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
		{
			if (global::Char.myCharz().arrItemBag[i] != null && global::Char.myCharz().arrItemBag[i].template.id == 193)
			{
				Service.gI().useItem(0, 1, (sbyte)i, -1);
				Service.gI().petStatus(VuDang.petStatus);
				return;
			}
		}
		for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
		{
			if (global::Char.myCharz().arrItemBag[j] != null && global::Char.myCharz().arrItemBag[j].template.id == 194)
			{
				Service.gI().useItem(0, 1, (sbyte)j, -1);
				Service.gI().petStatus(VuDang.petStatus);
				return;
			}
		}
		GameScr.info1.addInfo("Đéo có capsun", 0);
	}

	// Token: 0x0400127A RID: 4730
	private static string ListMap = "Làng Aru,Đồi hoa cúc,Thung lũng tre,Rừng nấm,Rừng xương,Đảo Kamê,Đông Karin,Làng Mori,Đồi nấm tím,Thị trấn Moori,Thung lũng Namếc,Thung lũng Maima,Vực maima,Đảo Guru,Làng Kakarot,Đồi hoang,Làng Plant,Rừng nguyên sinh,Rừng thông Xayda,Thành phố Vegeta,Vách núi đen,Nhà Gôhan,Nhà Moori,Nhà Broly,Trạm tàu vũ trụ,Trạm tàu vũ trụ,Trạm tàu vũ trụ,Rừng Bamboo,Rừng dương xỉ,Nam Kamê,Đảo Bulông,Núi hoa vàng,Núi hoa tím,Nam Guru,Đông Nam Guru,Rừng cọ,Rừng đá,Thung lũng đen,Bờ vực đen,Vách núi Aru,Vách núi Moori,Vực Plant,Vách núi Aru,Vách núi Moori,Vách núi Kakarot,Thần điện,Tháp Karin,Rừng Karin,Hành tinh Kaio,Phòng tập thời gian,Thánh địa Kaio,Đấu trường,Đại hội võ thuật,Tường thành 1,Tầng 3,Tầng 1,Tầng 2,Tầng 4,Tường thành 2,Tường thành 3,Trại độc nhãn 1,Trại độc nhãn 2,Trại độc nhãn 3,Trại lính Fide,Núi dây leo,Núi cây quỷ,Trại qủy già,Vực chết,Thung lũng Nappa,Vực cấm,Núi Appule,Căn cứ Raspberry,Thung lũng Raspberry,Thung lũng chết,Đồi cây Fide,Khe núi tử thần,Núi đá,Rừng đá,Lãnh  địa Fize,Núi khỉ đỏ,Núi khỉ vàng,Hang quỷ chim,Núi khỉ đen,Hang khỉ đen,Siêu Thị,Hành tinh M-2,Hành tinh Polaris,Hành tinh Cretaceous,Hành tinh Monmaasu,Hành tinh Rudeeze,Hành tinh Gelbo,Hành tinh Tigere,Thành phố phía đông,Thành phố phía nam,Đảo Balê,95,Cao nguyên,Thành phố phía bắc,Ngọn núi phía bắc,Thung lũng phía bắc,Thị trấn Ginder,101,Nhà Bunma,Võ đài Xên bọ hung,Sân sau siêu thị,Cánh đồng tuyết,Rừng tuyết,Núi tuyết,Dòng sông băng,Rừng băng,Hang băng,Đông Nam Karin,Võ đài Hạt Mít,Đại hội võ thuật,Cổng phi thuyền,Phòng chờ,Thánh địa Kaio,Cửa Ải 1,Cửa Ải 2,Cửa Ải 3,Phòng chỉ huy,Đấu trường,Ngũ Hành Sơn,Ngũ Hành Sơn,Ngũ Hành Sơn,Võ đài Bang,Thành phố Santa,Cổng phi thuyền,Bụng Mabư,Đại hội võ thuật,Đại hội võ thuật Vũ Trụ,Hành Tinh Yardart,Hành Tinh Yardart 2,Hành Tinh Yardart 3,Đại hội võ thuật Vũ Trụ 6-7,Động hải tặc,Hang Bạch Tuộc,Động kho báu,Cảng hải tặc,Hành tinh Potaufeu,Hang động Potaufeu,Con đường rắn độc,Con đường rắn độc,Con đường rắn độc,Hoang mạc,Võ Đài Siêu Cấp,Tây Karin,Sa mạc,Lâu đài Lychee,Thành phố Santa,Lôi Đài,Hành tinh bóng tối,Vùng đất băng giá,Lãnh địa bang hội,Hành tinh Bill,Hành tinh ngục tù,Tây thánh địa,Đông thánh Địa,Bắc thánh địa,Nam thánh Địa,Khu hang động,Bìa rừng nguyên thủy,Rừng nguyên thủy,Làng Plant nguyên thủy,Tranh ngọc Namếc";

	// Token: 0x0400127B RID: 4731
	public static string[] MapNames = VuDang.ListMap.Split(new char[]
	{
		','
	});

	// Token: 0x0400127C RID: 4732
	public static bool dichChuyenPem = true;

	// Token: 0x0400127D RID: 4733
	public static bool isAK;

	// Token: 0x0400127E RID: 4734
	private static long currUpdateKhu;

	// Token: 0x0400127F RID: 4735
	public static long[] currTimeAK = new long[8];

	// Token: 0x04001280 RID: 4736
	public static bool isNhanVang;

	// Token: 0x04001281 RID: 4737
	public static bool isDapDo;

	// Token: 0x04001282 RID: 4738
	public static Item doDeDap;

	// Token: 0x04001283 RID: 4739
	public static int soSaoCanDap = -1;

	// Token: 0x04001284 RID: 4740
	private static int saoHienTai = -1;

	// Token: 0x04001285 RID: 4741
	private static bool dangBanVang;

	// Token: 0x04001286 RID: 4742
	public static bool isVaoKhu;

	// Token: 0x04001287 RID: 4743
	public static global::Char[] chars = new global::Char[50];

	// Token: 0x04001288 RID: 4744
	public static bool nvat;

	// Token: 0x04001289 RID: 4745
	public static long currChangeZone;

	// Token: 0x0400128A RID: 4746
	public static bool achat;

	// Token: 0x0400128B RID: 4747
	private static long currAutoChat;

	// Token: 0x0400128C RID: 4748
	public static string textAutoChat = string.Empty;

	// Token: 0x0400128D RID: 4749
	public static bool isAutoCTG;

	// Token: 0x0400128E RID: 4750
	private static long currAutoCTG;

	// Token: 0x0400128F RID: 4751
	public static string textAutoChatTG = string.Empty;

	// Token: 0x04001290 RID: 4752
	public static bool isAutoNhatXa;

	// Token: 0x04001291 RID: 4753
	public static int xNhatXa;

	// Token: 0x04001292 RID: 4754
	public static int yNhatXa;

	// Token: 0x04001293 RID: 4755
	private static long currNhatXa;

	// Token: 0x04001294 RID: 4756
	private static long currAutoBT;

	// Token: 0x04001295 RID: 4757
	public static sbyte petStatus;

	// Token: 0x04001296 RID: 4758
	public static bool isAutoBT;

	// Token: 0x04001297 RID: 4759
	public static int timeBT = 1;

	// Token: 0x04001298 RID: 4760
	public static bool isGMT;

	// Token: 0x04001299 RID: 4761
	public static global::Char charMT;

	// Token: 0x0400129A RID: 4762
	public static bool xoamap;

	// Token: 0x0400129B RID: 4763
	public static bool XoaBackground = false;

	// Token: 0x0400129C RID: 4764
	private static string backgroundColor = "0.6 0.8 0.9";

	// Token: 0x0400129D RID: 4765
	public static bool giamDungLuong;

	// Token: 0x0400129E RID: 4766
	public static bool isPKM;

	// Token: 0x0400129F RID: 4767
	public static bool isBossM;

	// Token: 0x040012A0 RID: 4768
	public static bool trangThai = false;

	// Token: 0x040012A1 RID: 4769
	public static bool thongBaoBoss;

	// Token: 0x040012A2 RID: 4770
	public static MyVector bossVip = new MyVector();

	// Token: 0x040012A3 RID: 4771
	public static bool lineboss;

	// Token: 0x040012A4 RID: 4772
	public static int dichLenXuongTraiPhai;

	// Token: 0x040012A5 RID: 4773
	public static int ghimX;

	// Token: 0x040012A6 RID: 4774
	public static int ghimY;

	// Token: 0x040012A7 RID: 4775
	public static bool isKhoaViTri;

	// Token: 0x040012A8 RID: 4776
	private static long currKhoaViTri;

	// Token: 0x040012A9 RID: 4777
	public static bool xoaHieuUngHopThe;

	// Token: 0x040012AA RID: 4778
	public static bool xoaTauBay;

	// Token: 0x040012AB RID: 4779
	public static bool isThongTinDeTu;

	// Token: 0x040012AC RID: 4780
	public static bool isThongTinSuPhu;

	// Token: 0x040012AD RID: 4781
	public static int countBuyItem;

	// Token: 0x040012AE RID: 4782
	public static Item itemBuy = null;

	// Token: 0x040012AF RID: 4783
	private static long currBuyItem = 0L;

	// Token: 0x040012B0 RID: 4784
	public static bool isKSBoss;

	// Token: 0x040012B1 RID: 4785
	public static int HPKSBoss = 0;

	// Token: 0x040012B2 RID: 4786
	public static bool isKSBossBangSkill5;

	// Token: 0x040012B3 RID: 4787
	private static long currHoiSinh;

	// Token: 0x040012B4 RID: 4788
	public static bool hoiSinhNgoc;

	// Token: 0x040012B5 RID: 4789
	public static int ngocDuocDungDeHoiSinh;

	// Token: 0x040012B6 RID: 4790
	public static int ngocHienTai = 0;

	// Token: 0x040012B7 RID: 4791
	public static string tennoitaicanmo;

	// Token: 0x040012B8 RID: 4792
	public static bool isNoitai;

	// Token: 0x040012B9 RID: 4793
	public static int ntMin;

	// Token: 0x040012BA RID: 4794
	public static int ntNow;

	// Token: 0x040012BB RID: 4795
	public static bool doBoss;

	// Token: 0x040012BC RID: 4796
	public static int zoneMacDinh;

	// Token: 0x040012BD RID: 4797
	public static string bossCanDo;

	// Token: 0x040012BE RID: 4798
	private static long currDoBoss;

	// Token: 0x040012BF RID: 4799
	public static bool isUpdateKhu = false;

	// Token: 0x040012C0 RID: 4800
	private static long currFocusBoss;

	// Token: 0x040012C1 RID: 4801
	public static bool focusBoss;

	// Token: 0x040012C2 RID: 4802
	public static bool isThuongDeThuong;

	// Token: 0x040012C3 RID: 4803
	public static bool isThuongDeVip;

	// Token: 0x040012C4 RID: 4804
	private static long currThuongDe;

	// Token: 0x040012C5 RID: 4805
	public static bool isPaintCrackBall;

	// Token: 0x040012C6 RID: 4806
	public static bool dangNhanAll;

	// Token: 0x040012C7 RID: 4807
	private static bool dangMoMenu;

	// Token: 0x040012C8 RID: 4808
	public static bool khoamap;

	// Token: 0x040012C9 RID: 4809
	public static bool khoakhu;

	// Token: 0x040012CA RID: 4810
	public static long canChangeZone;

	// Token: 0x040012CB RID: 4811
	public static List<VuDang.setDo1> ListSet1 = new List<VuDang.setDo1>();

	// Token: 0x040012CC RID: 4812
	public static List<VuDang.setDo2> ListSet2 = new List<VuDang.setDo2>();

	// Token: 0x040012CD RID: 4813
	public static List<VuDang.ItemAuto> listItemAuto = new List<VuDang.ItemAuto>();

	// Token: 0x040012CE RID: 4814
	public static bool dangLogin;

	// Token: 0x040012CF RID: 4815
	public static bool isAutoLogin;

	// Token: 0x040012D0 RID: 4816
	public static bool isAutoVeKhu;

	// Token: 0x040012D1 RID: 4817
	private static long currVeKhuCu;

	// Token: 0x040012D2 RID: 4818
	public static int khuVeLai;

	// Token: 0x040012D3 RID: 4819
	public static int csHP;

	// Token: 0x040012D4 RID: 4820
	public static int csKI;

	// Token: 0x040012D5 RID: 4821
	public static bool aBuffDau;

	// Token: 0x040012D6 RID: 4822
	private static long currBuffDau;

	// Token: 0x040012D7 RID: 4823
	public static bool aDauDeTu;

	// Token: 0x040012D8 RID: 4824
	private static long currDauDeTu;

	// Token: 0x040012D9 RID: 4825
	public static int csHPDeTu;

	// Token: 0x040012DA RID: 4826
	public static int csKIDeTu;

	// Token: 0x040012DB RID: 4827
	public static bool isAutoTTNL;

	// Token: 0x040012DC RID: 4828
	private static long currAutoTTNL;

	// Token: 0x040012DD RID: 4829
	public static string dau = "Đậu";

	// Token: 0x040012DE RID: 4830
	public static string doHoa = "Đồ Họa";

	// Token: 0x040012DF RID: 4831
	public static string hoTroUpDe = "Up đệ";

	// Token: 0x040012E0 RID: 4832
	public static string upYardrat = "Up Yardrat";

	// Token: 0x040012E1 RID: 4833
	public static string hoTroSanBoss = "Boss";

	// Token: 0x040012E2 RID: 4834
	public static string chucNangKhac = "Khác";

	// Token: 0x040012E3 RID: 4835
	public static string tdLT = "TĐLT";

	// Token: 0x040012E4 RID: 4836
	public static string autoSkill = "Auto Skill";

	// Token: 0x040012E5 RID: 4837
	public static List<Skill> listSkillsAuto = new List<Skill>();

	// Token: 0x040012E6 RID: 4838
	public static bool xindau;

	// Token: 0x040012E7 RID: 4839
	public static bool thudau;

	// Token: 0x040012E8 RID: 4840
	private static long currThuDau;

	// Token: 0x040012E9 RID: 4841
	public static bool chodau;

	// Token: 0x040012EA RID: 4842
	private static long currXinDau;

	// Token: 0x040012EB RID: 4843
	private static long currChoDau;

	// Token: 0x040012EC RID: 4844
	public static bool isAutoNeBoss;

	// Token: 0x040012ED RID: 4845
	private static long currNeBoss;

	// Token: 0x040012EE RID: 4846
	private static long currAutoFlag;

	// Token: 0x040012EF RID: 4847
	public static bool isAutoCo;

	// Token: 0x040012F0 RID: 4848
	public static bool isKOK;

	// Token: 0x040012F1 RID: 4849
	private static long currKOK;

	// Token: 0x040012F2 RID: 4850
	private static long lastTimeCheckLag;

	// Token: 0x040012F3 RID: 4851
	public static int currentCheckLag = 30;

	// Token: 0x040012F4 RID: 4852
	public static bool isCheckLag = false;

	// Token: 0x040012F5 RID: 4853
	public static bool autoHoiSinh;

	// Token: 0x040012F6 RID: 4854
	private static long currAutoHoiSinh;

	// Token: 0x040012F7 RID: 4855
	public static bool isAutoAnNho;

	// Token: 0x040012F8 RID: 4856
	private static long currAnNho;

	// Token: 0x040012F9 RID: 4857
	public static int xGB;

	// Token: 0x040012FA RID: 4858
	public static int yGB;

	// Token: 0x040012FB RID: 4859
	public static bool IsGoBack;

	// Token: 0x040012FC RID: 4860
	public static int IdmapGB;

	// Token: 0x040012FD RID: 4861
	public static int ZoneGB;

	// Token: 0x040012FE RID: 4862
	public static bool canUpdate = false;

	// Token: 0x040012FF RID: 4863
	public static int tocdochay = 6;

	// Token: 0x020000C3 RID: 195
	public struct setDo1
	{
		// Token: 0x06000A1F RID: 2591 RVA: 0x00006625 File Offset: 0x00004825
		public setDo1(int type, string info, int id, string name, string option, int soSao)
		{
			this.type = type;
			this.info = info;
			this.id = id;
			this.name = name;
			this.option = option;
			this.soSao = soSao;
		}

		// Token: 0x04001300 RID: 4864
		public string info;

		// Token: 0x04001301 RID: 4865
		public int type;

		// Token: 0x04001302 RID: 4866
		public int id;

		// Token: 0x04001303 RID: 4867
		public string name;

		// Token: 0x04001304 RID: 4868
		public string option;

		// Token: 0x04001305 RID: 4869
		public int soSao;
	}

	// Token: 0x020000C4 RID: 196
	public struct setDo2
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x00006654 File Offset: 0x00004854
		public setDo2(int type, string info, int id, string name, string option, int soSao)
		{
			this.type = type;
			this.info = info;
			this.id = id;
			this.name = name;
			this.option = option;
			this.soSao = soSao;
		}

		// Token: 0x04001306 RID: 4870
		public string info;

		// Token: 0x04001307 RID: 4871
		public int type;

		// Token: 0x04001308 RID: 4872
		public int id;

		// Token: 0x04001309 RID: 4873
		public string name;

		// Token: 0x0400130A RID: 4874
		public string option;

		// Token: 0x0400130B RID: 4875
		public int soSao;
	}

	// Token: 0x020000C5 RID: 197
	public struct ItemAuto
	{
		// Token: 0x06000A21 RID: 2593 RVA: 0x00006683 File Offset: 0x00004883
		public ItemAuto(int iconID, int id)
		{
			this.iconID = iconID;
			this.id = id;
		}

		// Token: 0x0400130C RID: 4876
		public int iconID;

		// Token: 0x0400130D RID: 4877
		public int id;
	}
}
