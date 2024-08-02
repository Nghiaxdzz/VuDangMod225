using System;
using System.IO;
using System.Threading;
using AssemblyCSharp.Mod.PickMob;
using AssemblyCSharp.Mod.Xmap;
using Assets.src.g;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class GameScr : mScreen, IChatable
{
	// Token: 0x060002AC RID: 684 RVA: 0x0003F98C File Offset: 0x0003DB8C
	public GameScr()
	{
		if (GameCanvas.w == 128 || GameCanvas.h <= 208)
		{
			GameScr.indexSize = 20;
		}
		this.cmdback = new Command(string.Empty, 11021);
		this.cmdMenu = new Command("menu", 11000);
		this.cmdFocus = new Command(string.Empty, 11001);
		this.cmdMenu.img = GameScr.imgMenu;
		this.cmdMenu.w = mGraphics.getImageWidth(this.cmdMenu.img) + 20;
		this.cmdMenu.isPlaySoundButton = false;
		this.cmdFocus.img = GameScr.imgFocus;
		if (GameCanvas.isTouch)
		{
			this.cmdMenu.x = 0;
			this.cmdMenu.y = 50;
			this.cmdFocus = null;
		}
		else
		{
			this.cmdMenu.x = 0;
			this.cmdMenu.y = GameScr.gH - 30;
			this.cmdFocus.x = GameScr.gW - 32;
			this.cmdFocus.y = GameScr.gH - 32;
		}
		this.right = this.cmdFocus;
		GameScr.isPaintRada = 1;
		if (GameCanvas.isTouch)
		{
			GameScr.isHaveSelectSkill = true;
		}
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0003FB54 File Offset: 0x0003DD54
	public static void loadBg()
	{
		GameScr.fra_PVE_Bar_0 = new FrameImage(mSystem.loadImage("/mainImage/i_pve_bar_0.png"), 6, 15);
		GameScr.fra_PVE_Bar_1 = new FrameImage(mSystem.loadImage("/mainImage/i_pve_bar_1.png"), 38, 21);
		GameScr.imgVS = mSystem.loadImage("/mainImage/i_vs.png");
		GameScr.imgBall = mSystem.loadImage("/mainImage/i_charlife.png");
		GameScr.imgHP_NEW = mSystem.loadImage("/mainImage/i_hp.png");
		GameScr.imgKhung = mSystem.loadImage("/mainImage/i_khung.png");
		GameScr.imgLbtn = GameCanvas.loadImage("/mainImage/myTexture2dbtnl.png");
		GameScr.imgLbtnFocus = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf.png");
		GameScr.imgLbtn2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnl2.png");
		GameScr.imgLbtnFocus2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf2.png");
		GameScr.imgPanel = GameCanvas.loadImage("/mainImage/myTexture2dpanel.png");
		GameScr.imgPanel2 = GameCanvas.loadImage("/mainImage/panel2.png");
		GameScr.imgHP = GameCanvas.loadImage("/mainImage/myTexture2dHP.png");
		GameScr.imgSP = GameCanvas.loadImage("/mainImage/SP.png");
		GameScr.imgHPLost = GameCanvas.loadImage("/mainImage/myTexture2dhpLost.png");
		GameScr.imgMPLost = GameCanvas.loadImage("/mainImage/myTexture2dmpLost.png");
		GameScr.imgMP = GameCanvas.loadImage("/mainImage/myTexture2dMP.png");
		GameScr.imgSkill = GameCanvas.loadImage("/mainImage/myTexture2dskill.png");
		GameScr.imgSkill2 = GameCanvas.loadImage("/mainImage/myTexture2dskill2.png");
		GameScr.imgMenu = GameCanvas.loadImage("/mainImage/myTexture2dmenu.png");
		GameScr.imgFocus = GameCanvas.loadImage("/mainImage/myTexture2dfocus.png");
		GameScr.imgChatPC = GameCanvas.loadImage("/pc/chat.png");
		GameScr.imgChatsPC2 = GameCanvas.loadImage("/pc/chat2.png");
		if (GameCanvas.isTouch)
		{
			GameScr.imgArrow = GameCanvas.loadImage("/mainImage/myTexture2darrow.png");
			GameScr.imgArrow2 = GameCanvas.loadImage("/mainImage/myTexture2darrow2.png");
			GameScr.imgChat = GameCanvas.loadImage("/mainImage/myTexture2dchat.png");
			GameScr.imgChat2 = GameCanvas.loadImage("/mainImage/myTexture2dchat2.png");
			GameScr.imgFocus2 = GameCanvas.loadImage("/mainImage/myTexture2dfocus2.png");
			GameScr.imgHP1 = GameCanvas.loadImage("/mainImage/myTexture2dPea0.png");
			GameScr.imgHP2 = GameCanvas.loadImage("/mainImage/myTexture2dPea1.png");
			GameScr.imgAnalog1 = GameCanvas.loadImage("/mainImage/myTexture2danalog1.png");
			GameScr.imgAnalog2 = GameCanvas.loadImage("/mainImage/myTexture2danalog2.png");
			GameScr.imgHP3 = GameCanvas.loadImage("/mainImage/myTexture2dPea2.png");
			GameScr.imgHP4 = GameCanvas.loadImage("/mainImage/myTexture2dPea3.png");
			GameScr.imgFire0 = GameCanvas.loadImage("/mainImage/myTexture2dfirebtn0.png");
			GameScr.imgFire1 = GameCanvas.loadImage("/mainImage/myTexture2dfirebtn1.png");
		}
		GameScr.flyTextX = new int[5];
		GameScr.flyTextY = new int[5];
		GameScr.flyTextDx = new int[5];
		GameScr.flyTextDy = new int[5];
		GameScr.flyTextState = new int[5];
		GameScr.flyTextString = new string[5];
		GameScr.flyTextYTo = new int[5];
		GameScr.flyTime = new int[5];
		GameScr.flyTextColor = new int[8];
		for (int i = 0; i < 5; i++)
		{
			GameScr.flyTextState[i] = -1;
		}
		sbyte[] array = Rms.loadRMS("NRdataVersion");
		sbyte[] array2 = Rms.loadRMS("NRmapVersion");
		sbyte[] array3 = Rms.loadRMS("NRskillVersion");
		sbyte[] array4 = Rms.loadRMS("NRitemVersion");
		if (array != null)
		{
			GameScr.vcData = array[0];
		}
		if (array2 != null)
		{
			GameScr.vcMap = array2[0];
		}
		if (array3 != null)
		{
			GameScr.vcSkill = array3[0];
		}
		if (array4 != null)
		{
			GameScr.vcItem = array4[0];
		}
		GameScr.imgNut = GameCanvas.loadImage("/mainImage/myTexture2dnut.png");
		GameScr.imgNutF = GameCanvas.loadImage("/mainImage/myTexture2dnutF.png");
		MobCapcha.init();
		GameScr.isAnalog = ((Rms.loadRMSInt("analog") == 1) ? 1 : 0);
		GameScr.gamePad = new GamePad();
		GameScr.arrow = GameCanvas.loadImage("/mainImage/myTexture2darrow3.png");
		GameScr.imgTrans = GameCanvas.loadImage("/bg/trans.png");
		GameScr.imgRoomStat = GameCanvas.loadImage("/mainImage/myTexture2dstat.png");
		GameScr.frBarPow0 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor20.png");
		GameScr.frBarPow1 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor21.png");
		GameScr.frBarPow2 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor22.png");
		GameScr.frBarPow20 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor00.png");
		GameScr.frBarPow21 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor01.png");
		GameScr.frBarPow22 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor02.png");
	}

	// Token: 0x060002AE RID: 686 RVA: 0x000043DC File Offset: 0x000025DC
	public void initSelectChar()
	{
		this.readPart();
		SmallImage.init();
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0003FF38 File Offset: 0x0003E138
	public static void paintOngMauPercent(Image img0, Image img1, Image img2, float x, float y, int size, float pixelPercent, mGraphics g)
	{
		int clipX = g.getClipX();
		int clipY = g.getClipY();
		int clipWidth = g.getClipWidth();
		int clipHeight = g.getClipHeight();
		g.setClip((int)x, (int)y, (int)pixelPercent, 13);
		int num = size / 15 - 2;
		for (int i = 0; i < num; i++)
		{
			g.drawImage(img1, x + (float)((i + 1) * 15), y, 0);
		}
		g.drawImage(img0, x, y, 0);
		g.drawImage(img1, x + (float)size - 30f, y, 0);
		g.drawImage(img2, x + (float)size - 15f, y, 0);
		g.setClip(clipX, clipY, clipWidth, clipHeight);
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x000043E9 File Offset: 0x000025E9
	public void initTraining()
	{
		if (CreateCharScr.isCreateChar)
		{
			CreateCharScr.isCreateChar = false;
			this.right = null;
		}
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x000043FF File Offset: 0x000025FF
	public bool isMapDocNhan()
	{
		return TileMap.mapID >= 53 && TileMap.mapID <= 62;
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x00004418 File Offset: 0x00002618
	public bool isMapFize()
	{
		return TileMap.mapID >= 63;
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0003FFE8 File Offset: 0x0003E1E8
	public override void switchToMe()
	{
		GameScr.vChatVip.removeAllElements();
		ServerListScreen.isWait = false;
		if (BackgroudEffect.isHaveRain())
		{
			SoundMn.gI().rain();
		}
		LoginScr.isContinueToLogin = false;
		global::Char.isLoadingMap = false;
		if (!GameScr.isPaintOther)
		{
			Service.gI().finishLoadMap();
		}
		if (TileMap.isTrainingMap())
		{
			this.initTraining();
		}
		GameScr.info1.isUpdate = true;
		GameScr.info2.isUpdate = true;
		this.resetButton();
		GameScr.isLoadAllData = true;
		GameScr.isPaintOther = false;
		base.switchToMe();
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x00040070 File Offset: 0x0003E270
	public static int getMaxExp(int level)
	{
		int num = 0;
		for (int i = 0; i <= level; i++)
		{
			num += (int)GameScr.exps[i];
		}
		return num;
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x00040098 File Offset: 0x0003E298
	public static void resetAllvector()
	{
		GameScr.vCharInMap.removeAllElements();
		Teleport.vTeleport.removeAllElements();
		GameScr.vItemMap.removeAllElements();
		Effect2.vEffect2.removeAllElements();
		Effect2.vAnimateEffect.removeAllElements();
		Effect2.vEffect2Outside.removeAllElements();
		Effect2.vEffectFeet.removeAllElements();
		Effect2.vEffect3.removeAllElements();
		GameScr.vMobAttack.removeAllElements();
		GameScr.vMob.removeAllElements();
		GameScr.vNpc.removeAllElements();
		global::Char.myCharz().vMovePoints.removeAllElements();
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x00004426 File Offset: 0x00002626
	public void loadSkillShortcut()
	{
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x00040124 File Offset: 0x0003E324
	public void onOSkill(sbyte[] oSkillID)
	{
		Cout.println("GET onScreenSkill!");
		GameScr.onScreenSkill = new Skill[10];
		if (oSkillID == null)
		{
			this.loadDefaultonScreenSkill();
			return;
		}
		for (int i = 0; i < oSkillID.Length; i++)
		{
			for (int j = 0; j < global::Char.myCharz().vSkillFight.size(); j++)
			{
				Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(j);
				if (skill.template.id == oSkillID[i])
				{
					GameScr.onScreenSkill[i] = skill;
					break;
				}
			}
		}
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x000401AC File Offset: 0x0003E3AC
	public void onKSkill(sbyte[] kSkillID)
	{
		Cout.println("GET KEYSKILL!");
		GameScr.keySkill = new Skill[10];
		if (kSkillID == null)
		{
			this.loadDefaultKeySkill();
			return;
		}
		for (int i = 0; i < kSkillID.Length; i++)
		{
			for (int j = 0; j < global::Char.myCharz().vSkillFight.size(); j++)
			{
				Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(j);
				if (skill.template.id == kSkillID[i])
				{
					GameScr.keySkill[i] = skill;
					break;
				}
			}
		}
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x00040234 File Offset: 0x0003E434
	public void onCSkill(sbyte[] cSkillID)
	{
		Cout.println("GET CURRENTSKILL!");
		if (cSkillID == null || cSkillID.Length == 0)
		{
			if (global::Char.myCharz().vSkillFight.size() > 0)
			{
				global::Char.myCharz().myskill = (Skill)global::Char.myCharz().vSkillFight.elementAt(0);
			}
		}
		else
		{
			for (int i = 0; i < global::Char.myCharz().vSkillFight.size(); i++)
			{
				Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(i);
				if (skill.template.id == cSkillID[0])
				{
					global::Char.myCharz().myskill = skill;
					break;
				}
			}
		}
		if (global::Char.myCharz().myskill != null)
		{
			Service.gI().selectSkill((int)global::Char.myCharz().myskill.template.id);
			this.saveRMSCurrentSkill(global::Char.myCharz().myskill.template.id);
		}
	}

	// Token: 0x060002BA RID: 698 RVA: 0x00040318 File Offset: 0x0003E518
	private void loadDefaultonScreenSkill()
	{
		Cout.println("LOAD DEFAULT ONmScreen SKILL");
		int num = 0;
		while (num < GameScr.onScreenSkill.Length && num < global::Char.myCharz().vSkillFight.size())
		{
			Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(num);
			GameScr.onScreenSkill[num] = skill;
			num++;
		}
		this.saveonScreenSkillToRMS();
	}

	// Token: 0x060002BB RID: 699 RVA: 0x00040378 File Offset: 0x0003E578
	private void loadDefaultKeySkill()
	{
		Cout.println("LOAD DEFAULT KEY SKILL");
		int num = 0;
		while (num < GameScr.keySkill.Length && num < global::Char.myCharz().vSkillFight.size())
		{
			Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(num);
			GameScr.keySkill[num] = skill;
			num++;
		}
		this.saveKeySkillToRMS();
	}

	// Token: 0x060002BC RID: 700 RVA: 0x000403D8 File Offset: 0x0003E5D8
	public void doSetOnScreenSkill(SkillTemplate skillTemplate)
	{
		Skill skill = global::Char.myCharz().getSkill(skillTemplate);
		MyVector myVector = new MyVector();
		for (int i = 0; i < 10; i++)
		{
			MyVector myVector2 = myVector;
			object p = new object[]
			{
				skill,
				i + string.Empty
			};
			myVector2.addElement(new Command(mResources.into_place + (i + 1), 11120, p));
		}
		GameCanvas.menu.startAt(myVector, 0);
	}

	// Token: 0x060002BD RID: 701 RVA: 0x00040458 File Offset: 0x0003E658
	public void doSetKeySkill(SkillTemplate skillTemplate)
	{
		Cout.println("DO SET KEY SKILL");
		Skill skill = global::Char.myCharz().getSkill(skillTemplate);
		string[] array = (!TField.isQwerty) ? mResources.key_skill : mResources.key_skill_qwerty;
		MyVector myVector = new MyVector();
		for (int i = 0; i < 10; i++)
		{
			MyVector myVector2 = myVector;
			object p = new object[]
			{
				skill,
				i + string.Empty
			};
			myVector2.addElement(new Command(array[i], 11121, p));
		}
		GameCanvas.menu.startAt(myVector, 0);
	}

	// Token: 0x060002BE RID: 702 RVA: 0x000404E8 File Offset: 0x0003E6E8
	public void saveonScreenSkillToRMS()
	{
		sbyte[] array = new sbyte[GameScr.onScreenSkill.Length];
		for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
		{
			if (GameScr.onScreenSkill[i] == null)
			{
				array[i] = -1;
			}
			else
			{
				array[i] = GameScr.onScreenSkill[i].template.id;
			}
		}
		Service.gI().changeOnKeyScr(array);
	}

	// Token: 0x060002BF RID: 703 RVA: 0x00040544 File Offset: 0x0003E744
	public void saveKeySkillToRMS()
	{
		sbyte[] array = new sbyte[GameScr.keySkill.Length];
		for (int i = 0; i < GameScr.keySkill.Length; i++)
		{
			if (GameScr.keySkill[i] == null)
			{
				array[i] = -1;
			}
			else
			{
				array[i] = GameScr.keySkill[i].template.id;
			}
		}
		Service.gI().changeOnKeyScr(array);
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x00004426 File Offset: 0x00002626
	public void saveRMSCurrentSkill(sbyte id)
	{
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x000405A0 File Offset: 0x0003E7A0
	public void addSkillShortcut(Skill skill)
	{
		Cout.println("ADD SKILL SHORTCUT TO SKILL " + skill.template.id);
		for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
		{
			if (GameScr.onScreenSkill[i] == null)
			{
				GameScr.onScreenSkill[i] = skill;
				break;
			}
		}
		for (int j = 0; j < GameScr.keySkill.Length; j++)
		{
			if (GameScr.keySkill[j] == null)
			{
				GameScr.keySkill[j] = skill;
				break;
			}
		}
		if (global::Char.myCharz().myskill == null)
		{
			global::Char.myCharz().myskill = skill;
		}
		this.saveKeySkillToRMS();
		this.saveonScreenSkillToRMS();
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x0004063C File Offset: 0x0003E83C
	public bool isBagFull()
	{
		for (int i = global::Char.myCharz().arrItemBag.Length - 1; i >= 0; i--)
		{
			if (global::Char.myCharz().arrItemBag[i] == null)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x00004428 File Offset: 0x00002628
	public void createConfirm(string[] menu, Npc npc)
	{
		this.resetButton();
		this.isLockKey = true;
		this.left = new Command(menu[0], 130011, npc);
		this.right = new Command(menu[1], 130012, npc);
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x00040674 File Offset: 0x0003E874
	public void createMenu(string[] menu, Npc npc)
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < menu.Length; i++)
		{
			myVector.addElement(new Command(menu[i], 11057, npc));
		}
		GameCanvas.menu.startAt(myVector, 2);
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x000406B8 File Offset: 0x0003E8B8
	public void readPart()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_part"));
			int num = (int)dataInputStream.readShort();
			GameScr.parts = new Part[num];
			for (int i = 0; i < num; i++)
			{
				int type = (int)dataInputStream.readByte();
				GameScr.parts[i] = new Part(type);
				for (int j = 0; j < GameScr.parts[i].pi.Length; j++)
				{
					GameScr.parts[i].pi[j] = new PartImage();
					GameScr.parts[i].pi[j].id = dataInputStream.readShort();
					GameScr.parts[i].pi[j].dx = dataInputStream.readByte();
					GameScr.parts[i].pi[j].dy = dataInputStream.readByte();
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("LOI TAI readPart " + ex.ToString());
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Cout.LogError("LOI TAI readPart 2" + ex2.ToString());
			}
		}
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x000407F8 File Offset: 0x0003E9F8
	public void readEfect()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_effect"));
			int num = (int)dataInputStream.readShort();
			GameScr.efs = new EffectCharPaint[num];
			for (int i = 0; i < num; i++)
			{
				GameScr.efs[i] = new EffectCharPaint();
				GameScr.efs[i].idEf = (int)dataInputStream.readShort();
				GameScr.efs[i].arrEfInfo = new EffectInfoPaint[(int)dataInputStream.readByte()];
				for (int j = 0; j < GameScr.efs[i].arrEfInfo.Length; j++)
				{
					GameScr.efs[i].arrEfInfo[j] = new EffectInfoPaint();
					GameScr.efs[i].arrEfInfo[j].idImg = (int)dataInputStream.readShort();
					GameScr.efs[i].arrEfInfo[j].dx = (int)dataInputStream.readByte();
					GameScr.efs[i].arrEfInfo[j].dy = (int)dataInputStream.readByte();
				}
			}
		}
		catch (Exception)
		{
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex)
			{
				Cout.LogError("Loi ham Eff: " + ex.ToString());
			}
		}
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x00040938 File Offset: 0x0003EB38
	public void readArrow()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_arrow"));
			int num = (int)dataInputStream.readShort();
			GameScr.arrs = new Arrowpaint[num];
			for (int i = 0; i < num; i++)
			{
				GameScr.arrs[i] = new Arrowpaint();
				GameScr.arrs[i].id = (int)dataInputStream.readShort();
				GameScr.arrs[i].imgId[0] = (int)dataInputStream.readShort();
				GameScr.arrs[i].imgId[1] = (int)dataInputStream.readShort();
				GameScr.arrs[i].imgId[2] = (int)dataInputStream.readShort();
			}
		}
		catch (Exception)
		{
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex)
			{
				Cout.LogError("Loi ham readArrow: " + ex.ToString());
			}
		}
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x00040A20 File Offset: 0x0003EC20
	public void readDart()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_dart"));
			int num = (int)dataInputStream.readShort();
			GameScr.darts = new DartInfo[num];
			for (int i = 0; i < num; i++)
			{
				GameScr.darts[i] = new DartInfo();
				GameScr.darts[i].id = dataInputStream.readShort();
				GameScr.darts[i].nUpdate = dataInputStream.readShort();
				GameScr.darts[i].va = (int)(dataInputStream.readShort() * 256);
				GameScr.darts[i].xdPercent = dataInputStream.readShort();
				int num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].tail = new short[num2];
				for (int j = 0; j < num2; j++)
				{
					GameScr.darts[i].tail[j] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].tailBorder = new short[num2];
				for (int k = 0; k < num2; k++)
				{
					GameScr.darts[i].tailBorder[k] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].xd1 = new short[num2];
				for (int l = 0; l < num2; l++)
				{
					GameScr.darts[i].xd1[l] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].xd2 = new short[num2];
				for (int m = 0; m < num2; m++)
				{
					GameScr.darts[i].xd2[m] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].head = new short[num2][];
				for (int n = 0; n < num2; n++)
				{
					short num3 = dataInputStream.readShort();
					GameScr.darts[i].head[n] = new short[(int)num3];
					for (int num4 = 0; num4 < (int)num3; num4++)
					{
						GameScr.darts[i].head[n][num4] = dataInputStream.readShort();
					}
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].headBorder = new short[num2][];
				for (int num5 = 0; num5 < num2; num5++)
				{
					short num6 = dataInputStream.readShort();
					GameScr.darts[i].headBorder[num5] = new short[(int)num6];
					for (int num7 = 0; num7 < (int)num6; num7++)
					{
						GameScr.darts[i].headBorder[num5][num7] = dataInputStream.readShort();
					}
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham ReadDart: " + ex.ToString());
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Cout.LogError("Loi ham reaaDart: " + ex2.ToString());
			}
		}
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x00040D24 File Offset: 0x0003EF24
	public void readSkill()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_skill"));
			int num = (int)dataInputStream.readShort();
			GameScr.sks = new SkillPaint[Skills.skills.size()];
			for (int i = 0; i < num; i++)
			{
				short num2 = dataInputStream.readShort();
				Res.outz("skill id= " + num2);
				if (num2 == 1111)
				{
					num2 = (short)(num - 1);
				}
				GameScr.sks[(int)num2] = new SkillPaint();
				GameScr.sks[(int)num2].id = (int)num2;
				GameScr.sks[(int)num2].effectHappenOnMob = (int)dataInputStream.readShort();
				if (GameScr.sks[(int)num2].effectHappenOnMob <= 0)
				{
					GameScr.sks[(int)num2].effectHappenOnMob = 80;
				}
				GameScr.sks[(int)num2].numEff = (int)dataInputStream.readByte();
				GameScr.sks[(int)num2].skillStand = new SkillInfoPaint[(int)dataInputStream.readByte()];
				for (int j = 0; j < GameScr.sks[(int)num2].skillStand.Length; j++)
				{
					GameScr.sks[(int)num2].skillStand[j] = new SkillInfoPaint();
					GameScr.sks[(int)num2].skillStand[j].status = (int)dataInputStream.readByte();
					GameScr.sks[(int)num2].skillStand[j].effS0Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].e0dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].e0dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].effS1Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].e1dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].e1dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].effS2Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].e2dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].e2dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].arrowId = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].adx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].ady = (int)dataInputStream.readShort();
				}
				GameScr.sks[(int)num2].skillfly = new SkillInfoPaint[(int)dataInputStream.readByte()];
				for (int k = 0; k < GameScr.sks[(int)num2].skillfly.Length; k++)
				{
					GameScr.sks[(int)num2].skillfly[k] = new SkillInfoPaint();
					GameScr.sks[(int)num2].skillfly[k].status = (int)dataInputStream.readByte();
					GameScr.sks[(int)num2].skillfly[k].effS0Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].e0dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].e0dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].effS1Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].e1dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].e1dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].effS2Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].e2dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].e2dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].arrowId = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].adx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].ady = (int)dataInputStream.readShort();
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham readSkill: " + ex.ToString());
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Cout.LogError("Loi ham readskill: " + ex2.ToString());
			}
		}
	}

	// Token: 0x060002CA RID: 714 RVA: 0x0000445F File Offset: 0x0000265F
	public static GameScr gI()
	{
		if (GameScr.instance == null)
		{
			GameScr.instance = new GameScr();
		}
		return GameScr.instance;
	}

	// Token: 0x060002CB RID: 715 RVA: 0x00004477 File Offset: 0x00002677
	public static void clearGameScr()
	{
		GameScr.instance = null;
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0000447F File Offset: 0x0000267F
	public void loadGameScr()
	{
		GameScr.loadSplash();
		Res.init();
		this.loadInforBar();
	}

	// Token: 0x060002CD RID: 717 RVA: 0x000411C0 File Offset: 0x0003F3C0
	public void doMenuInforMe()
	{
		GameScr.scrMain.clear();
		GameScr.scrInfo.clear();
		GameScr.isViewNext = false;
		this.cmdBag = new Command(mResources.MENUME[0], 1100011);
		this.cmdSkill = new Command(mResources.MENUME[1], 1100012);
		this.cmdTiemnang = new Command(mResources.MENUME[2], 1100013);
		this.cmdInfo = new Command(mResources.MENUME[3], 1100014);
		this.cmdtrangbi = new Command(mResources.MENUME[4], 1100015);
		MyVector myVector = new MyVector();
		myVector.addElement(this.cmdBag);
		myVector.addElement(this.cmdSkill);
		myVector.addElement(this.cmdTiemnang);
		myVector.addElement(this.cmdInfo);
		myVector.addElement(this.cmdtrangbi);
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x060002CE RID: 718 RVA: 0x000412A8 File Offset: 0x0003F4A8
	public void doMenusynthesis()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(mResources.SYNTHESIS[0], 110002));
		myVector.addElement(new Command(mResources.SYNTHESIS[1], 1100032));
		myVector.addElement(new Command(mResources.SYNTHESIS[2], 1100033));
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0004130C File Offset: 0x0003F50C
	public static void loadCamera(bool fullmScreen, int cx, int cy)
	{
		GameScr.gW = GameCanvas.w;
		GameScr.cmdBarH = 39;
		GameScr.gH = GameCanvas.h;
		GameScr.cmdBarW = GameScr.gW;
		GameScr.cmdBarX = 0;
		GameScr.cmdBarY = GameCanvas.h - Paint.hTab - GameScr.cmdBarH;
		GameScr.girlHPBarY = 0;
		GameScr.csPadMaxH = GameCanvas.h / 6;
		if (GameScr.csPadMaxH < 48)
		{
			GameScr.csPadMaxH = 48;
		}
		GameScr.gW2 = GameScr.gW >> 1;
		GameScr.gH2 = GameScr.gH >> 1;
		GameScr.gW3 = GameScr.gW / 3;
		GameScr.gH3 = GameScr.gH / 3;
		GameScr.gW23 = GameScr.gH - 120;
		GameScr.gH23 = GameScr.gH * 2 / 3;
		GameScr.gW34 = 3 * GameScr.gW / 4;
		GameScr.gH34 = 3 * GameScr.gH / 4;
		GameScr.gW6 = GameScr.gW / 6;
		GameScr.gH6 = GameScr.gH / 6;
		GameScr.gssw = GameScr.gW / (int)TileMap.size + 2;
		GameScr.gssh = GameScr.gH / (int)TileMap.size + 2;
		if (GameScr.gW % 24 != 0)
		{
			GameScr.gssw++;
		}
		GameScr.cmxLim = (TileMap.tmw - 1) * (int)TileMap.size - GameScr.gW;
		GameScr.cmyLim = (TileMap.tmh - 1) * (int)TileMap.size - GameScr.gH;
		if (cx == -1 && cy == -1)
		{
			GameScr.cmx = (GameScr.cmtoX = global::Char.myCharz().cx - GameScr.gW2 + GameScr.gW6 * global::Char.myCharz().cdir);
			GameScr.cmy = (GameScr.cmtoY = global::Char.myCharz().cy - GameScr.gH23);
		}
		else
		{
			GameScr.cmx = (GameScr.cmtoX = cx - GameScr.gW23 + GameScr.gW6 * global::Char.myCharz().cdir);
			GameScr.cmy = (GameScr.cmtoY = cy - GameScr.gH23);
		}
		GameScr.firstY = GameScr.cmy;
		if (GameScr.cmx < 24)
		{
			GameScr.cmx = (GameScr.cmtoX = 24);
		}
		if (GameScr.cmx > GameScr.cmxLim)
		{
			GameScr.cmx = (GameScr.cmtoX = GameScr.cmxLim);
		}
		if (GameScr.cmy < 0)
		{
			GameScr.cmy = (GameScr.cmtoY = 0);
		}
		if (GameScr.cmy > GameScr.cmyLim)
		{
			GameScr.cmy = (GameScr.cmtoY = GameScr.cmyLim);
		}
		GameScr.gssx = GameScr.cmx / (int)TileMap.size - 1;
		if (GameScr.gssx < 0)
		{
			GameScr.gssx = 0;
		}
		GameScr.gssy = GameScr.cmy / (int)TileMap.size;
		GameScr.gssxe = GameScr.gssx + GameScr.gssw;
		GameScr.gssye = GameScr.gssy + GameScr.gssh;
		if (GameScr.gssy < 0)
		{
			GameScr.gssy = 0;
		}
		if (GameScr.gssye > TileMap.tmh - 1)
		{
			GameScr.gssye = TileMap.tmh - 1;
		}
		TileMap.countx = (GameScr.gssxe - GameScr.gssx) * 4;
		if (TileMap.countx > TileMap.tmw)
		{
			TileMap.countx = TileMap.tmw;
		}
		TileMap.county = (GameScr.gssye - GameScr.gssy) * 4;
		if (TileMap.county > TileMap.tmh)
		{
			TileMap.county = TileMap.tmh;
		}
		TileMap.gssx = (global::Char.myCharz().cx - 2 * GameScr.gW) / (int)TileMap.size;
		if (TileMap.gssx < 0)
		{
			TileMap.gssx = 0;
		}
		TileMap.gssxe = TileMap.gssx + TileMap.countx;
		if (TileMap.gssxe > TileMap.tmw)
		{
			TileMap.gssxe = TileMap.tmw;
		}
		TileMap.gssy = (global::Char.myCharz().cy - 2 * GameScr.gH) / (int)TileMap.size;
		if (TileMap.gssy < 0)
		{
			TileMap.gssy = 0;
		}
		TileMap.gssye = TileMap.gssy + TileMap.county;
		if (TileMap.gssye > TileMap.tmh)
		{
			TileMap.gssye = TileMap.tmh;
		}
		ChatTextField.gI().parentScreen = GameScr.instance;
		ChatTextField.gI().tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
		ChatTextField.gI().initChatTextField();
		if (GameCanvas.isTouch)
		{
			GameScr.yTouchBar = GameScr.gH - 88;
			GameScr.xC = GameScr.gW - 40;
			GameScr.yC = 2;
			if (GameCanvas.w <= 240)
			{
				GameScr.xC = GameScr.gW - 35;
				GameScr.yC = 5;
			}
			GameScr.xF = GameScr.gW - 55;
			GameScr.yF = GameScr.yTouchBar + 35;
			GameScr.xTG = GameScr.gW - 37;
			GameScr.yTG = GameScr.yTouchBar - 1;
			if (GameCanvas.w >= 450)
			{
				GameScr.yTG -= 12;
				GameScr.yHP -= 7;
				GameScr.xF -= 10;
				GameScr.yF -= 5;
				GameScr.xTG -= 10;
			}
		}
		GameScr.setSkillBarPosition();
		GameScr.disXC = ((GameCanvas.w <= 200) ? 30 : 40);
		if (Rms.loadRMSInt("viewchat") == -1)
		{
			GameCanvas.panel.isViewChatServer = true;
			return;
		}
		GameCanvas.panel.isViewChatServer = (Rms.loadRMSInt("viewchat") == 1);
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x00041814 File Offset: 0x0003FA14
	public static void setSkillBarPosition()
	{
		Skill[] array = (!GameCanvas.isTouch) ? GameScr.keySkill : GameScr.onScreenSkill;
		GameScr.xS = new int[array.Length];
		GameScr.yS = new int[array.Length];
		if (GameCanvas.isTouchControlSmallScreen && GameScr.isUseTouch)
		{
			GameScr.xSkill = 23;
			GameScr.ySkill = 52;
			GameScr.padSkill = 5;
			for (int i = 0; i < GameScr.xS.Length; i++)
			{
				GameScr.xS[i] = i * (25 + GameScr.padSkill);
				GameScr.yS[i] = GameScr.ySkill;
				if (GameScr.xS.Length > 5 && i >= GameScr.xS.Length / 2)
				{
					GameScr.xS[i] = (i - GameScr.xS.Length / 2) * (25 + GameScr.padSkill);
					GameScr.yS[i] = GameScr.ySkill - 32;
				}
			}
			GameScr.xHP = array.Length * (25 + GameScr.padSkill);
			GameScr.yHP = GameScr.ySkill;
		}
		else
		{
			GameScr.wSkill = 30;
			if (GameCanvas.w <= 320)
			{
				GameScr.ySkill = GameScr.gH - GameScr.wSkill - 6;
				GameScr.xSkill = GameScr.gW2 - array.Length * GameScr.wSkill / 2 - 25;
			}
			else
			{
				GameScr.wSkill = 40;
				GameScr.xSkill = 10;
				GameScr.ySkill = GameCanvas.h - GameScr.wSkill + 7;
			}
			for (int j = 0; j < GameScr.xS.Length; j++)
			{
				GameScr.xS[j] = j * GameScr.wSkill;
				GameScr.yS[j] = GameScr.ySkill;
				if (GameScr.xS.Length > 5 && j >= GameScr.xS.Length / 2)
				{
					GameScr.xS[j] = (j - GameScr.xS.Length / 2) * GameScr.wSkill;
					GameScr.yS[j] = GameScr.ySkill - 32;
				}
			}
			GameScr.xHP = array.Length * GameScr.wSkill;
			GameScr.yHP = GameScr.ySkill;
		}
		if (GameCanvas.isTouch)
		{
			GameScr.xSkill = 17;
			GameScr.ySkill = GameCanvas.h - 40;
			if (GameScr.gamePad.isSmallGamePad && GameScr.isAnalog == 1)
			{
				GameScr.xHP = array.Length * GameScr.wSkill;
				GameScr.yHP = GameScr.ySkill;
			}
			else
			{
				GameScr.xHP = GameCanvas.w - 45;
				GameScr.yHP = GameCanvas.h - 45;
			}
			GameScr.setTouchBtn();
			for (int k = 0; k < GameScr.xS.Length; k++)
			{
				GameScr.xS[k] = k * GameScr.wSkill;
				GameScr.yS[k] = GameScr.ySkill;
				if (GameScr.xS.Length > 5 && k >= GameScr.xS.Length / 2)
				{
					GameScr.xS[k] = (k - GameScr.xS.Length / 2) * GameScr.wSkill;
					GameScr.yS[k] = GameScr.ySkill - 32;
				}
			}
		}
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x00041ABC File Offset: 0x0003FCBC
	private static void updateCamera()
	{
		if (!GameScr.isPaintOther)
		{
			if (GameScr.cmx != GameScr.cmtoX || GameScr.cmy != GameScr.cmtoY)
			{
				GameScr.cmvx = GameScr.cmtoX - GameScr.cmx << 2;
				GameScr.cmvy = GameScr.cmtoY - GameScr.cmy << 2;
				GameScr.cmdx += GameScr.cmvx;
				GameScr.cmx += GameScr.cmdx >> 4;
				GameScr.cmdx &= 15;
				GameScr.cmdy += GameScr.cmvy;
				GameScr.cmy += GameScr.cmdy >> 4;
				GameScr.cmdy &= 15;
				if (GameScr.cmx < 24)
				{
					GameScr.cmx = 24;
				}
				if (GameScr.cmx > GameScr.cmxLim)
				{
					GameScr.cmx = GameScr.cmxLim;
				}
				if (GameScr.cmy < 0)
				{
					GameScr.cmy = 0;
				}
				if (GameScr.cmy > GameScr.cmyLim)
				{
					GameScr.cmy = GameScr.cmyLim;
				}
			}
			GameScr.gssx = GameScr.cmx / (int)TileMap.size - 1;
			if (GameScr.gssx < 0)
			{
				GameScr.gssx = 0;
			}
			GameScr.gssy = GameScr.cmy / (int)TileMap.size;
			GameScr.gssxe = GameScr.gssx + GameScr.gssw;
			GameScr.gssye = GameScr.gssy + GameScr.gssh;
			if (GameScr.gssy < 0)
			{
				GameScr.gssy = 0;
			}
			if (GameScr.gssye > TileMap.tmh - 1)
			{
				GameScr.gssye = TileMap.tmh - 1;
			}
			TileMap.gssx = (global::Char.myCharz().cx - 2 * GameScr.gW) / (int)TileMap.size;
			if (TileMap.gssx < 0)
			{
				TileMap.gssx = 0;
			}
			TileMap.gssxe = TileMap.gssx + TileMap.countx;
			if (TileMap.gssxe > TileMap.tmw)
			{
				TileMap.gssxe = TileMap.tmw;
				TileMap.gssx = TileMap.gssxe - TileMap.countx;
			}
			TileMap.gssy = (global::Char.myCharz().cy - 2 * GameScr.gH) / (int)TileMap.size;
			if (TileMap.gssy < 0)
			{
				TileMap.gssy = 0;
			}
			TileMap.gssye = TileMap.gssy + TileMap.county;
			if (TileMap.gssye > TileMap.tmh)
			{
				TileMap.gssye = TileMap.tmh;
				TileMap.gssy = TileMap.gssye - TileMap.county;
			}
			GameScr.scrMain.updatecm();
			GameScr.scrInfo.updatecm();
		}
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x00041D08 File Offset: 0x0003FF08
	public bool testAct()
	{
		for (sbyte b = 2; b < 9; b += 2)
		{
			if (GameCanvas.keyHold[(int)b])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x00041D30 File Offset: 0x0003FF30
	public void clanInvite(string strInvite, int clanID, int code)
	{
		ClanObject clanObject = new ClanObject();
		clanObject.code = code;
		clanObject.clanID = clanID;
		this.startYesNoPopUp(strInvite, new Command(mResources.YES, 12002, clanObject), new Command(mResources.NO, 12003, clanObject));
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x00041D78 File Offset: 0x0003FF78
	public void playerMenu(global::Char c)
	{
		this.auto = 0;
		GameCanvas.clearKeyHold();
		if (global::Char.myCharz().charFocus.charID >= 0 && global::Char.myCharz().charID >= 0)
		{
			MyVector vPlayerMenu = GameCanvas.panel.vPlayerMenu;
			if (vPlayerMenu.size() <= 0)
			{
				if (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId > 1)
				{
					vPlayerMenu.addElement(new Command(mResources.make_friend, 11112, global::Char.myCharz().charFocus));
					vPlayerMenu.addElement(new Command(mResources.trade, 11113, global::Char.myCharz().charFocus));
				}
				if (global::Char.myCharz().clan != null && global::Char.myCharz().role < 2 && global::Char.myCharz().charFocus.clanID == -1)
				{
					vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[4], 110391));
				}
				if (global::Char.myCharz().charFocus.statusMe != 14 && global::Char.myCharz().charFocus.statusMe != 5)
				{
					if (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId >= 14)
					{
						vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[0], 2003));
					}
				}
				else
				{
					int type = global::Char.myCharz().myskill.template.type;
				}
				if (global::Char.myCharz().clan != null && global::Char.myCharz().clan.ID == global::Char.myCharz().charFocus.clanID && global::Char.myCharz().charFocus.statusMe != 14 && global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId >= 14)
				{
					vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[1], 2004));
				}
				int num = global::Char.myCharz().nClass.skillTemplates.Length;
				for (int i = 0; i < num; i++)
				{
					SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[i];
					Skill skill = global::Char.myCharz().getSkill(skillTemplate);
					if (skill != null && skillTemplate.isBuffToPlayer() && skill.point >= 1)
					{
						vPlayerMenu.addElement(new Command(skillTemplate.name, 12004, skill));
					}
				}
			}
		}
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x00041FC0 File Offset: 0x000401C0
	public bool isAttack()
	{
		bool result;
		if (this.checkClickToBotton(global::Char.myCharz().charFocus))
		{
			result = false;
		}
		else if (this.checkClickToBotton(global::Char.myCharz().mobFocus))
		{
			result = false;
		}
		else if (this.checkClickToBotton(global::Char.myCharz().npcFocus))
		{
			result = false;
		}
		else if (ChatTextField.gI().isShow)
		{
			result = false;
		}
		else if (InfoDlg.isLock || global::Char.myCharz().isLockAttack || global::Char.isLockKey)
		{
			result = false;
		}
		else if (global::Char.myCharz().myskill != null && global::Char.myCharz().myskill.template.id == 6 && global::Char.myCharz().itemFocus != null)
		{
			this.pickItem();
			result = false;
		}
		else if (global::Char.myCharz().myskill != null && global::Char.myCharz().myskill.template.type == 2 && global::Char.myCharz().npcFocus == null && global::Char.myCharz().myskill.template.id != 6)
		{
			result = this.checkSkillValid();
		}
		else if (global::Char.myCharz().skillPaint != null || (global::Char.myCharz().mobFocus == null && global::Char.myCharz().npcFocus == null && global::Char.myCharz().charFocus == null && global::Char.myCharz().itemFocus == null))
		{
			result = false;
		}
		else if (global::Char.myCharz().mobFocus != null)
		{
			if (global::Char.myCharz().mobFocus.isBigBoss() && global::Char.myCharz().mobFocus.status == 4)
			{
				global::Char.myCharz().mobFocus = null;
				global::Char.myCharz().currentMovePoint = null;
			}
			GameScr.isAutoPlay = true;
			if (!this.isMeCanAttackMob(global::Char.myCharz().mobFocus))
			{
				Res.outz("can not attack");
				result = false;
			}
			else if (this.mobCapcha != null)
			{
				result = false;
			}
			else if (global::Char.myCharz().myskill == null)
			{
				result = false;
			}
			else if (global::Char.myCharz().isSelectingSkillUseAlone())
			{
				result = false;
			}
			else if (global::Char.myCharz().mobFocus.status == 1 || global::Char.myCharz().mobFocus.status == 0 || global::Char.myCharz().myskill.template.type == 4)
			{
				result = false;
			}
			else if (!this.checkSkillValid())
			{
				result = false;
			}
			else
			{
				if (global::Char.myCharz().cx < global::Char.myCharz().mobFocus.getX())
				{
					global::Char.myCharz().cdir = 1;
				}
				else
				{
					global::Char.myCharz().cdir = -1;
				}
				int num = global::Math.abs(global::Char.myCharz().cx - global::Char.myCharz().mobFocus.getX());
				int num2 = global::Math.abs(global::Char.myCharz().cy - global::Char.myCharz().mobFocus.getY());
				global::Char.myCharz().cvx = 0;
				if (num <= global::Char.myCharz().myskill.dx && num2 <= global::Char.myCharz().myskill.dy)
				{
					if (global::Char.myCharz().myskill.template.id == 20)
					{
						result = true;
					}
					else if (num2 > num && Res.abs(global::Char.myCharz().cy - global::Char.myCharz().mobFocus.getY()) > 30 && global::Char.myCharz().mobFocus.getTemplate().type == 4)
					{
						global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().cx + global::Char.myCharz().cdir, global::Char.myCharz().mobFocus.getY());
						global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
						GameCanvas.clearKeyHold();
						GameCanvas.clearKeyPressed();
						result = false;
					}
					else
					{
						int num3 = 20;
						bool flag = false;
						if (global::Char.myCharz().mobFocus is BigBoss || global::Char.myCharz().mobFocus is BigBoss2)
						{
							flag = true;
						}
						if (global::Char.myCharz().myskill.dx > 100)
						{
							num3 = 60;
							if (num < 20)
							{
								global::Char.myCharz().createShadow(global::Char.myCharz().cx, global::Char.myCharz().cy, 10);
							}
						}
						bool flag2 = false;
						if ((TileMap.tileTypeAtPixel(global::Char.myCharz().cx, global::Char.myCharz().cy + 3) & 2) == 2)
						{
							int num4 = (global::Char.myCharz().cx > global::Char.myCharz().mobFocus.getX()) ? 1 : -1;
							if ((TileMap.tileTypeAtPixel(global::Char.myCharz().mobFocus.getX() + num3 * num4, global::Char.myCharz().cy + 3) & 2) != 2)
							{
								flag2 = true;
							}
						}
						if (num <= num3 && !flag2)
						{
							if (num >= 30)
							{
								int num5 = (global::Char.myCharz().cx <= global::Char.myCharz().mobFocus.getX()) ? (-num3) : num3;
								global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().cx + num5, global::Char.myCharz().cy);
								global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
								GameCanvas.clearKeyHold();
								GameCanvas.clearKeyPressed();
								return false;
							}
							if (global::Char.myCharz().cx > global::Char.myCharz().mobFocus.getX())
							{
								global::Char.myCharz().cx = global::Char.myCharz().mobFocus.getX() + num3 + (flag ? 30 : 0);
								global::Char.myCharz().cdir = -1;
							}
							else
							{
								global::Char.myCharz().cx = global::Char.myCharz().mobFocus.getX() - num3 - (flag ? 30 : 0);
								global::Char.myCharz().cdir = 1;
							}
							Service.gI().charMove();
						}
						GameCanvas.clearKeyHold();
						GameCanvas.clearKeyPressed();
						result = true;
					}
				}
				else
				{
					bool flag3 = false;
					if (global::Char.myCharz().mobFocus is BigBoss || global::Char.myCharz().mobFocus is BigBoss2)
					{
						flag3 = true;
					}
					int num6 = (global::Char.myCharz().myskill.dx - ((!flag3) ? 20 : 50)) * ((global::Char.myCharz().cx > global::Char.myCharz().mobFocus.getX()) ? 1 : -1);
					if (num <= global::Char.myCharz().myskill.dx)
					{
						num6 = 0;
					}
					global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().mobFocus.getX() + num6, global::Char.myCharz().mobFocus.getY());
					global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					result = false;
				}
			}
		}
		else if (global::Char.myCharz().npcFocus != null)
		{
			if (global::Char.myCharz().npcFocus.isHide)
			{
				result = false;
			}
			else
			{
				if (global::Char.myCharz().cx < global::Char.myCharz().npcFocus.cx)
				{
					global::Char.myCharz().cdir = 1;
				}
				else
				{
					global::Char.myCharz().cdir = -1;
				}
				if (global::Char.myCharz().cx < global::Char.myCharz().npcFocus.cx)
				{
					global::Char.myCharz().npcFocus.cdir = -1;
				}
				else
				{
					global::Char.myCharz().npcFocus.cdir = 1;
				}
				int num7 = global::Math.abs(global::Char.myCharz().cx - global::Char.myCharz().npcFocus.cx);
				if (global::Math.abs(global::Char.myCharz().cy - global::Char.myCharz().npcFocus.cy) > 40)
				{
					global::Char.myCharz().cy = global::Char.myCharz().npcFocus.cy - 40;
				}
				if (num7 < 60)
				{
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					if (this.tMenuDelay == 0)
					{
						if (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId == 0)
						{
							if (global::Char.myCharz().taskMaint.index < 4 && global::Char.myCharz().npcFocus.template.npcTemplateId == 4)
							{
								return false;
							}
							if (global::Char.myCharz().taskMaint.index < 3 && global::Char.myCharz().npcFocus.template.npcTemplateId == 3)
							{
								return false;
							}
						}
						this.tMenuDelay = 50;
						InfoDlg.showWait();
						Service.gI().charMove();
						Service.gI().openMenu(global::Char.myCharz().npcFocus.template.npcTemplateId);
					}
				}
				else
				{
					int num8 = (20 + Res.r.nextInt(20)) * ((global::Char.myCharz().cx > global::Char.myCharz().npcFocus.cx) ? 1 : -1);
					global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().npcFocus.cx + num8, global::Char.myCharz().cy);
					global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
				}
				result = false;
			}
		}
		else if (global::Char.myCharz().charFocus != null)
		{
			if (this.mobCapcha != null)
			{
				result = false;
			}
			else
			{
				if (global::Char.myCharz().cx < global::Char.myCharz().charFocus.cx)
				{
					global::Char.myCharz().cdir = 1;
				}
				else
				{
					global::Char.myCharz().cdir = -1;
				}
				int num9 = global::Math.abs(global::Char.myCharz().cx - global::Char.myCharz().charFocus.cx);
				int num10 = global::Math.abs(global::Char.myCharz().cy - global::Char.myCharz().charFocus.cy);
				if (global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus) || global::Char.myCharz().isSelectingSkillBuffToPlayer())
				{
					if (global::Char.myCharz().myskill == null)
					{
						result = false;
					}
					else if (!this.checkSkillValid())
					{
						result = false;
					}
					else
					{
						if (global::Char.myCharz().cx < global::Char.myCharz().charFocus.cx)
						{
							global::Char.myCharz().cdir = 1;
						}
						else
						{
							global::Char.myCharz().cdir = -1;
						}
						global::Char.myCharz().cvx = 0;
						if (num9 <= global::Char.myCharz().myskill.dx && num10 <= global::Char.myCharz().myskill.dy)
						{
							if (global::Char.myCharz().myskill.template.id == 20)
							{
								result = true;
							}
							else
							{
								int num11 = 20;
								if (global::Char.myCharz().myskill.dx > 60)
								{
									num11 = 60;
									if (num9 < 20)
									{
										global::Char.myCharz().createShadow(global::Char.myCharz().cx, global::Char.myCharz().cy, 10);
									}
								}
								bool flag4 = false;
								if ((TileMap.tileTypeAtPixel(global::Char.myCharz().cx, global::Char.myCharz().cy + 3) & 2) == 2)
								{
									int num12 = (global::Char.myCharz().cx > global::Char.myCharz().charFocus.cx) ? 1 : -1;
									if ((TileMap.tileTypeAtPixel(global::Char.myCharz().charFocus.cx + num11 * num12, global::Char.myCharz().cy + 3) & 2) != 2)
									{
										flag4 = true;
									}
								}
								if (num9 <= num11 && !flag4)
								{
									if (global::Char.myCharz().cx > global::Char.myCharz().charFocus.cx)
									{
										global::Char.myCharz().cx = global::Char.myCharz().charFocus.cx + num11;
										global::Char.myCharz().cdir = -1;
									}
									else
									{
										global::Char.myCharz().cx = global::Char.myCharz().charFocus.cx - num11;
										global::Char.myCharz().cdir = 1;
									}
									Service.gI().charMove();
								}
								GameCanvas.clearKeyHold();
								GameCanvas.clearKeyPressed();
								result = true;
							}
						}
						else
						{
							int num13 = (global::Char.myCharz().myskill.dx - 20) * ((global::Char.myCharz().cx > global::Char.myCharz().charFocus.cx) ? 1 : -1);
							if (num9 <= global::Char.myCharz().myskill.dx)
							{
								num13 = 0;
							}
							global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().charFocus.cx + num13, global::Char.myCharz().charFocus.cy);
							global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
							GameCanvas.clearKeyHold();
							GameCanvas.clearKeyPressed();
							result = false;
						}
					}
				}
				else
				{
					if (num9 < 60 && num10 < 40)
					{
						this.playerMenu(global::Char.myCharz().charFocus);
						if (!GameCanvas.isTouch && global::Char.myCharz().charFocus.charID >= 0 && TileMap.mapID != 51 && TileMap.mapID != 52 && this.popUpYesNo == null)
						{
							GameCanvas.panel.setTypePlayerMenu(global::Char.myCharz().charFocus);
							GameCanvas.panel.show();
							Service.gI().getPlayerMenu(global::Char.myCharz().charFocus.charID);
							Service.gI().messagePlayerMenu(global::Char.myCharz().charFocus.charID);
						}
					}
					else
					{
						int num14 = (20 + Res.r.nextInt(20)) * ((global::Char.myCharz().cx > global::Char.myCharz().charFocus.cx) ? 1 : -1);
						global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().charFocus.cx + num14, global::Char.myCharz().charFocus.cy);
						global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
						GameCanvas.clearKeyHold();
						GameCanvas.clearKeyPressed();
					}
					result = false;
				}
			}
		}
		else if (global::Char.myCharz().itemFocus != null)
		{
			this.pickItem();
			result = false;
		}
		else
		{
			result = true;
		}
		return result;
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x00042D14 File Offset: 0x00040F14
	public bool isMeCanAttackMob(Mob m)
	{
		bool result;
		if (m == null)
		{
			result = false;
		}
		else if (global::Char.myCharz().cTypePk == 5)
		{
			result = true;
		}
		else if (global::Char.myCharz().isAttacPlayerStatus() && !m.isMobMe)
		{
			result = false;
		}
		else if (global::Char.myCharz().mobMe != null && m.Equals(global::Char.myCharz().mobMe))
		{
			result = false;
		}
		else
		{
			global::Char @char = GameScr.findCharInMap(m.mobId);
			result = (@char == null || @char.cTypePk == 5 || global::Char.myCharz().isMeCanAttackOtherPlayer(@char));
		}
		return result;
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00042D9C File Offset: 0x00040F9C
	private bool checkSkillValid()
	{
		bool result;
		if (global::Char.myCharz().myskill != null && ((global::Char.myCharz().myskill.template.manaUseType != 1 && global::Char.myCharz().cMP < global::Char.myCharz().myskill.manaUse) || (global::Char.myCharz().myskill.template.manaUseType == 1 && global::Char.myCharz().cMP < global::Char.myCharz().cMPFull * global::Char.myCharz().myskill.manaUse / 100)))
		{
			GameScr.info1.addInfo(mResources.NOT_ENOUGH_MP, 0);
			this.auto = 0;
			result = false;
		}
		else if (global::Char.myCharz().myskill == null || (global::Char.myCharz().myskill.template.maxPoint > 0 && global::Char.myCharz().myskill.point == 0))
		{
			GameCanvas.startOKDlg(mResources.SKILL_FAIL);
			result = false;
		}
		else
		{
			result = true;
		}
		return result;
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x00042E8C File Offset: 0x0004108C
	private bool checkSkillValid2()
	{
		return (global::Char.myCharz().myskill == null || ((global::Char.myCharz().myskill.template.manaUseType == 1 || global::Char.myCharz().cMP >= global::Char.myCharz().myskill.manaUse) && (global::Char.myCharz().myskill.template.manaUseType != 1 || global::Char.myCharz().cMP >= global::Char.myCharz().cMPFull * global::Char.myCharz().myskill.manaUse / 100))) && global::Char.myCharz().myskill != null && (global::Char.myCharz().myskill.template.maxPoint <= 0 || global::Char.myCharz().myskill.point != 0);
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x00042F50 File Offset: 0x00041150
	public void resetButton()
	{
		GameCanvas.menu.showMenu = false;
		ChatTextField.gI().close();
		ChatTextField.gI().center = null;
		this.isLockKey = false;
		this.typeTrade = 0;
		GameScr.indexMenu = 0;
		GameScr.indexSelect = 0;
		this.indexItemUse = -1;
		GameScr.indexRow = -1;
		GameScr.indexRowMax = 0;
		GameScr.indexTitle = 0;
		this.typeTrade = (this.typeTradeOrder = 0);
		mSystem.endKey();
		if (global::Char.myCharz().cHP <= 0 || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5)
		{
			if (global::Char.myCharz().meDead && VuDang.IsGoBack && VuDang.IdmapGB != -1)
			{
				GameScr.isAutoPlay = false;
				Service.gI().returnTownFromDead();
				new Thread(new ThreadStart(VuDang.GoBack)).Start();
			}
			else
			{
				this.cmdDead = new Command(mResources.DIES[0], 11038);
				this.center = this.cmdDead;
				global::Char.myCharz().cHP = 0;
			}
			GameScr.isHaveSelectSkill = false;
		}
		else
		{
			GameScr.isHaveSelectSkill = true;
		}
		GameScr.scrMain.clear();
	}

	// Token: 0x060002DA RID: 730 RVA: 0x00004491 File Offset: 0x00002691
	public override void keyPress(int keyCode)
	{
		base.keyPress(keyCode);
	}

	// Token: 0x060002DB RID: 731 RVA: 0x00043078 File Offset: 0x00041278
	public override void updateKey()
	{
		if (!Controller.isStopReadMessage && !global::Char.myCharz().isTeleport && !InfoDlg.isLock)
		{
			if (GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu)
			{
				this.updateKeyTouchControl();
			}
			this.checkAuto();
			GameCanvas.debug("F2", 0);
			if (ChatPopup.currChatPopup != null)
			{
				Command cmdNextLine = ChatPopup.currChatPopup.cmdNextLine;
				if ((GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(cmdNextLine)) && cmdNextLine != null)
				{
					GameCanvas.isPointerJustRelease = false;
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
					mScreen.keyTouch = -1;
					if (cmdNextLine != null)
					{
						cmdNextLine.performAction();
					}
				}
			}
			else if (!ChatTextField.gI().isShow)
			{
				if ((GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.left)) && this.left != null)
				{
					GameCanvas.isPointerJustRelease = false;
					GameCanvas.isPointerClick = false;
					GameCanvas.keyPressed[12] = false;
					mScreen.keyTouch = -1;
					if (this.left != null)
					{
						this.left.performAction();
					}
				}
				if ((GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.right)) && this.right != null)
				{
					GameCanvas.isPointerJustRelease = false;
					GameCanvas.isPointerClick = false;
					GameCanvas.keyPressed[13] = false;
					mScreen.keyTouch = -1;
					if (this.right != null)
					{
						this.right.performAction();
					}
				}
				if ((GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center)) && this.center != null)
				{
					GameCanvas.isPointerJustRelease = false;
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
					mScreen.keyTouch = -1;
					if (this.center != null)
					{
						this.center.performAction();
					}
				}
			}
			else
			{
				if (ChatTextField.gI().left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(ChatTextField.gI().left)) && ChatTextField.gI().left != null)
				{
					ChatTextField.gI().left.performAction();
				}
				if (ChatTextField.gI().right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(ChatTextField.gI().right)) && ChatTextField.gI().right != null)
				{
					ChatTextField.gI().right.performAction();
				}
				if (ChatTextField.gI().center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(ChatTextField.gI().center)) && ChatTextField.gI().center != null)
				{
					ChatTextField.gI().center.performAction();
				}
			}
			GameCanvas.debug("F6", 0);
			this.updateKeyAlert();
			GameCanvas.debug("F7", 0);
			if (global::Char.myCharz().currentMovePoint != null)
			{
				for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
				{
					if (GameCanvas.keyPressed[i])
					{
						global::Char.myCharz().currentMovePoint = null;
						break;
					}
				}
			}
			GameCanvas.debug("F8", 0);
			if (ChatTextField.gI().isShow && GameCanvas.keyAsciiPress != 0)
			{
				ChatTextField.gI().keyPressed(GameCanvas.keyAsciiPress);
				GameCanvas.keyAsciiPress = 0;
				return;
			}
			if (this.isLockKey)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return;
			}
			if (!GameCanvas.menu.showMenu && !this.isOpenUI() && !global::Char.isLockKey)
			{
				if (GameCanvas.keyPressed[10])
				{
					GameCanvas.keyPressed[10] = false;
					this.doUseHP();
					GameCanvas.clearKeyPressed();
				}
				if (GameCanvas.keyPressed[11] && this.mobCapcha == null)
				{
					if (this.popUpYesNo != null)
					{
						this.popUpYesNo.cmdYes.performAction();
					}
					else if (GameScr.info2.info.info != null && GameScr.info2.info.info.charInfo != null)
					{
						GameCanvas.panel.setTypeMessage();
						GameCanvas.panel.show();
					}
					GameCanvas.keyPressed[11] = false;
					GameCanvas.clearKeyPressed();
				}
				if (GameCanvas.keyAsciiPress != 0 && TField.isQwerty && GameCanvas.keyAsciiPress == 32)
				{
					this.doUseHP();
					GameCanvas.keyAsciiPress = 0;
					GameCanvas.clearKeyPressed();
				}
				if (GameCanvas.keyAsciiPress != 0 && this.mobCapcha == null && TField.isQwerty && GameCanvas.keyAsciiPress == 121)
				{
					if (this.popUpYesNo != null)
					{
						this.popUpYesNo.cmdYes.performAction();
						GameCanvas.keyAsciiPress = 0;
						GameCanvas.clearKeyPressed();
					}
					else if (GameScr.info2.info.info != null && GameScr.info2.info.info.charInfo != null)
					{
						GameCanvas.panel.setTypeMessage();
						GameCanvas.panel.show();
						GameCanvas.keyAsciiPress = 0;
						GameCanvas.clearKeyPressed();
					}
				}
				if (GameCanvas.keyPressed[10] && this.mobCapcha == null)
				{
					GameCanvas.keyPressed[10] = false;
					GameScr.info2.doClick(10);
					GameCanvas.clearKeyPressed();
				}
				this.checkDrag();
				if (!global::Char.myCharz().isFlyAndCharge)
				{
					this.checkClick();
				}
				if (global::Char.myCharz().cmdMenu != null && global::Char.myCharz().cmdMenu.isPointerPressInside())
				{
					global::Char.myCharz().cmdMenu.performAction();
				}
				if (global::Char.myCharz().skillPaint == null)
				{
					if (GameCanvas.keyAsciiPress != 0)
					{
						if (this.mobCapcha == null)
						{
							if (TField.isQwerty)
							{
								if (GameCanvas.keyPressed[1])
								{
									if (GameScr.keySkill[0] != null)
									{
										this.doSelectSkill(GameScr.keySkill[0], true);
									}
								}
								else if (GameCanvas.keyPressed[2])
								{
									if (GameScr.keySkill[1] != null)
									{
										this.doSelectSkill(GameScr.keySkill[1], true);
									}
								}
								else if (GameCanvas.keyPressed[3])
								{
									if (GameScr.keySkill[2] != null)
									{
										this.doSelectSkill(GameScr.keySkill[2], true);
									}
								}
								else if (GameCanvas.keyPressed[4])
								{
									if (GameScr.keySkill[3] != null)
									{
										this.doSelectSkill(GameScr.keySkill[3], true);
									}
								}
								else if (GameCanvas.keyPressed[5])
								{
									if (GameScr.keySkill[4] != null)
									{
										this.doSelectSkill(GameScr.keySkill[4], true);
									}
								}
								else if (GameCanvas.keyPressed[6])
								{
									if (GameScr.keySkill[5] != null)
									{
										this.doSelectSkill(GameScr.keySkill[5], true);
									}
								}
								else if (GameCanvas.keyPressed[7])
								{
									if (GameScr.keySkill[6] != null)
									{
										this.doSelectSkill(GameScr.keySkill[6], true);
									}
								}
								else if (GameCanvas.keyPressed[8])
								{
									if (GameScr.keySkill[7] != null)
									{
										this.doSelectSkill(GameScr.keySkill[7], true);
									}
								}
								else if (GameCanvas.keyPressed[9])
								{
									if (GameScr.keySkill[8] != null)
									{
										this.doSelectSkill(GameScr.keySkill[8], true);
									}
								}
								else if (GameCanvas.keyPressed[0])
								{
									if (GameScr.keySkill[9] != null)
									{
										this.doSelectSkill(GameScr.keySkill[9], true);
									}
								}
								else if (GameCanvas.keyAsciiPress == 114)
								{
									ChatTextField.gI().startChat(this, string.Empty);
								}
							}
							else if (!GameCanvas.isMoveNumberPad)
							{
								ChatTextField.gI().startChat(GameCanvas.keyAsciiPress, this, string.Empty);
							}
							else if (GameCanvas.keyAsciiPress == 55)
							{
								if (GameScr.keySkill[0] != null)
								{
									this.doSelectSkill(GameScr.keySkill[0], true);
								}
							}
							else if (GameCanvas.keyAsciiPress == 56)
							{
								if (GameScr.keySkill[1] != null)
								{
									this.doSelectSkill(GameScr.keySkill[1], true);
								}
							}
							else if (GameCanvas.keyAsciiPress == 57)
							{
								if (GameScr.keySkill[(!Main.isPC) ? 2 : 21] != null)
								{
									this.doSelectSkill(GameScr.keySkill[2], true);
								}
							}
							else if (GameCanvas.keyAsciiPress == 48)
							{
								ChatTextField.gI().startChat(this, string.Empty);
							}
						}
						else
						{
							char[] array = this.keyInput.ToCharArray();
							MyVector myVector = new MyVector();
							for (int j = 0; j < array.Length; j++)
							{
								myVector.addElement(array[j].ToString() + string.Empty);
							}
							myVector.removeElementAt(0);
							string text = ((char)GameCanvas.keyAsciiPress).ToString() + string.Empty;
							if (text.Equals(string.Empty) || text == null || text.Equals("\n"))
							{
								text = "-";
							}
							myVector.insertElementAt(text, myVector.size());
							this.keyInput = string.Empty;
							for (int k = 0; k < myVector.size(); k++)
							{
								this.keyInput += ((string)myVector.elementAt(k)).ToUpper();
							}
							Service.gI().mobCapcha((char)GameCanvas.keyAsciiPress);
						}
						VuDang.PhimTat(GameCanvas.keyAsciiPress);
						GameCanvas.keyAsciiPress = 0;
					}
					if (global::Char.myCharz().statusMe == 1)
					{
						GameCanvas.debug("F10", 0);
						if (!this.doSeleckSkillFlag)
						{
							if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
							{
								GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
								this.doFire(false, false);
							}
							else if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
							{
								if (!global::Char.myCharz().isLockMove)
								{
									this.setCharJump(0);
								}
							}
							else if (GameCanvas.keyHold[1] && this.mobCapcha == null)
							{
								if (!Main.isPC)
								{
									global::Char.myCharz().cdir = -1;
									if (!global::Char.myCharz().isLockMove)
									{
										this.setCharJump(-4);
									}
								}
							}
							else if (GameCanvas.keyHold[(!Main.isPC) ? 5 : 25] && this.mobCapcha == null)
							{
								if (!Main.isPC)
								{
									global::Char.myCharz().cdir = 1;
									if (!global::Char.myCharz().isLockMove)
									{
										this.setCharJump(4);
									}
								}
							}
							else if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
							{
								GameScr.isAutoPlay = false;
								global::Char.myCharz().isAttack = false;
								if (global::Char.myCharz().cdir == 1)
								{
									global::Char.myCharz().cdir = -1;
								}
								else if (!global::Char.myCharz().isLockMove)
								{
									if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0)
									{
										Service.gI().charMove();
									}
									global::Char.myCharz().statusMe = 2;
									global::Char.myCharz().cvx = -global::Char.myCharz().cspeed;
								}
								global::Char.myCharz().holder = false;
							}
							else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
							{
								GameScr.isAutoPlay = false;
								global::Char.myCharz().isAttack = false;
								if (global::Char.myCharz().cdir == -1)
								{
									global::Char.myCharz().cdir = 1;
								}
								else if (!global::Char.myCharz().isLockMove)
								{
									if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0)
									{
										Service.gI().charMove();
									}
									global::Char.myCharz().statusMe = 2;
									global::Char.myCharz().cvx = global::Char.myCharz().cspeed;
								}
								global::Char.myCharz().holder = false;
							}
						}
					}
					else if (global::Char.myCharz().statusMe == 2)
					{
						GameCanvas.debug("F11", 0);
						if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
						{
							GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
							this.doFire(false, true);
						}
						else if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
						{
							if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0)
							{
								Service.gI().charMove();
							}
							global::Char.myCharz().cvy = -10;
							global::Char.myCharz().statusMe = 3;
							global::Char.myCharz().cp1 = 0;
						}
						else if (GameCanvas.keyHold[1] && this.mobCapcha == null)
						{
							if (Main.isPC)
							{
								if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0)
								{
									Service.gI().charMove();
								}
								global::Char.myCharz().cdir = -1;
								global::Char.myCharz().cvy = -10;
								global::Char.myCharz().cvx = -4;
								global::Char.myCharz().statusMe = 3;
								global::Char.myCharz().cp1 = 0;
							}
						}
						else if (GameCanvas.keyHold[3] && this.mobCapcha == null)
						{
							if (!Main.isPC)
							{
								if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0)
								{
									Service.gI().charMove();
								}
								global::Char.myCharz().cdir = 1;
								global::Char.myCharz().cvy = -10;
								global::Char.myCharz().cvx = 4;
								global::Char.myCharz().statusMe = 3;
								global::Char.myCharz().cp1 = 0;
							}
						}
						else if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
						{
							GameScr.isAutoPlay = false;
							if (global::Char.myCharz().cdir == 1)
							{
								global::Char.myCharz().cdir = -1;
							}
							else
							{
								global::Char.myCharz().cvx = -global::Char.myCharz().cspeed + global::Char.myCharz().cBonusSpeed;
							}
						}
						else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
						{
							GameScr.isAutoPlay = false;
							if (global::Char.myCharz().cdir == -1)
							{
								global::Char.myCharz().cdir = 1;
							}
							else
							{
								global::Char.myCharz().cvx = global::Char.myCharz().cspeed + global::Char.myCharz().cBonusSpeed;
							}
						}
					}
					else if (global::Char.myCharz().statusMe == 3)
					{
						GameScr.isAutoPlay = false;
						GameCanvas.debug("F12", 0);
						if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
						{
							GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
							this.doFire(false, true);
						}
						if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] || (GameCanvas.keyHold[1] && this.mobCapcha == null))
						{
							if (global::Char.myCharz().cdir == 1)
							{
								global::Char.myCharz().cdir = -1;
							}
							else
							{
								global::Char.myCharz().cvx = -global::Char.myCharz().cspeed;
							}
						}
						else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] || (GameCanvas.keyHold[3] && this.mobCapcha == null))
						{
							if (global::Char.myCharz().cdir == -1)
							{
								global::Char.myCharz().cdir = 1;
							}
							else
							{
								global::Char.myCharz().cvx = global::Char.myCharz().cspeed;
							}
						}
						if ((GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] || ((GameCanvas.keyHold[1] || GameCanvas.keyHold[3]) && this.mobCapcha == null)) && global::Char.myCharz().canFly && global::Char.myCharz().cMP > 0 && global::Char.myCharz().cp1 < 8 && global::Char.myCharz().cvy > -4)
						{
							global::Char.myCharz().cp1++;
							global::Char.myCharz().cvy = -7;
						}
					}
					else if (global::Char.myCharz().statusMe == 4)
					{
						GameCanvas.debug("F13", 0);
						if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
						{
							GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
							this.doFire(false, true);
						}
						if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] && global::Char.myCharz().cMP > 0 && global::Char.myCharz().canFly)
						{
							GameScr.isAutoPlay = false;
							if ((global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0) && (Res.abs(global::Char.myCharz().cx - global::Char.myCharz().cxSend) > 96 || Res.abs(global::Char.myCharz().cy - global::Char.myCharz().cySend) > 24))
							{
								Service.gI().charMove();
							}
							global::Char.myCharz().cvy = -10;
							global::Char.myCharz().statusMe = 3;
							global::Char.myCharz().cp1 = 0;
						}
						if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
						{
							GameScr.isAutoPlay = false;
							if (global::Char.myCharz().cdir == 1)
							{
								global::Char.myCharz().cdir = -1;
							}
							else
							{
								global::Char.myCharz().cp1++;
								global::Char.myCharz().cvx = -global::Char.myCharz().cspeed;
								if (global::Char.myCharz().cp1 > 5 && global::Char.myCharz().cvy > 6)
								{
									global::Char.myCharz().statusMe = 10;
									global::Char.myCharz().cp1 = 0;
									global::Char.myCharz().cvy = 0;
								}
							}
						}
						else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
						{
							GameScr.isAutoPlay = false;
							if (global::Char.myCharz().cdir == -1)
							{
								global::Char.myCharz().cdir = 1;
							}
							else
							{
								global::Char.myCharz().cp1++;
								global::Char.myCharz().cvx = global::Char.myCharz().cspeed;
								if (global::Char.myCharz().cp1 > 5 && global::Char.myCharz().cvy > 6)
								{
									global::Char.myCharz().statusMe = 10;
									global::Char.myCharz().cp1 = 0;
									global::Char.myCharz().cvy = 0;
								}
							}
						}
					}
					else if (global::Char.myCharz().statusMe == 10)
					{
						GameCanvas.debug("F14", 0);
						if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
						{
							GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
							this.doFire(false, true);
						}
						if (global::Char.myCharz().canFly && global::Char.myCharz().cMP > 0)
						{
							if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
							{
								GameScr.isAutoPlay = false;
								if ((global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0) && (Res.abs(global::Char.myCharz().cx - global::Char.myCharz().cxSend) > 96 || Res.abs(global::Char.myCharz().cy - global::Char.myCharz().cySend) > 24))
								{
									Service.gI().charMove();
								}
								global::Char.myCharz().cvy = -10;
								global::Char.myCharz().statusMe = 3;
								global::Char.myCharz().cp1 = 0;
							}
							else if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
							{
								GameScr.isAutoPlay = false;
								if (global::Char.myCharz().cdir == 1)
								{
									global::Char.myCharz().cdir = -1;
								}
								else
								{
									global::Char.myCharz().cvx = -(global::Char.myCharz().cspeed + 1);
								}
							}
							else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
							{
								if (global::Char.myCharz().cdir == -1)
								{
									global::Char.myCharz().cdir = 1;
								}
								else
								{
									global::Char.myCharz().cvx = global::Char.myCharz().cspeed + 1;
								}
							}
						}
					}
					else if (global::Char.myCharz().statusMe == 7)
					{
						GameCanvas.debug("F15", 0);
						if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
						{
							GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
						}
						if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
						{
							GameScr.isAutoPlay = false;
							if (global::Char.myCharz().cdir == 1)
							{
								global::Char.myCharz().cdir = -1;
							}
							else
							{
								global::Char.myCharz().cvx = -global::Char.myCharz().cspeed + 2;
							}
						}
						else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
						{
							GameScr.isAutoPlay = false;
							if (global::Char.myCharz().cdir == -1)
							{
								global::Char.myCharz().cdir = 1;
							}
							else
							{
								global::Char.myCharz().cvx = global::Char.myCharz().cspeed - 2;
							}
						}
					}
					GameCanvas.debug("F17", 0);
					if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] && GameCanvas.keyAsciiPress != 56)
					{
						GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
						global::Char.myCharz().delayFall = 0;
					}
					if (GameCanvas.keyPressed[10])
					{
						GameCanvas.keyPressed[10] = false;
						this.doUseHP();
					}
					GameCanvas.debug("F20", 0);
					GameCanvas.clearKeyPressed();
					GameCanvas.debug("F23", 0);
					this.doSeleckSkillFlag = false;
				}
			}
		}
	}

	// Token: 0x060002DC RID: 732 RVA: 0x0000449A File Offset: 0x0000269A
	public bool isVsMap()
	{
		return true;
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0004455C File Offset: 0x0004275C
	private void checkDrag()
	{
		if (GameScr.isAnalog != 1 && !GameScr.gamePad.disableCheckDrag())
		{
			global::Char.myCharz().cmtoChar = true;
			if (!GameScr.isUseTouch)
			{
				if (GameCanvas.isPointerJustDown)
				{
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = true;
					this.ptDownTime = 0;
					this.ptLastDownX = (this.ptFirstDownX = GameCanvas.px);
					this.ptLastDownY = (this.ptFirstDownY = GameCanvas.py);
				}
				if (this.isPointerDowning)
				{
					int num = GameCanvas.px - this.ptLastDownX;
					int num2 = GameCanvas.py - this.ptLastDownY;
					if (!this.isChangingCameraMode && (Res.abs(GameCanvas.px - this.ptFirstDownX) > 15 || Res.abs(GameCanvas.py - this.ptFirstDownY) > 15))
					{
						this.isChangingCameraMode = true;
					}
					this.ptLastDownX = GameCanvas.px;
					this.ptLastDownY = GameCanvas.py;
					this.ptDownTime++;
					if (this.isChangingCameraMode)
					{
						global::Char.myCharz().cmtoChar = false;
						GameScr.cmx -= num;
						GameScr.cmy -= num2;
						if (GameScr.cmx < 24)
						{
							int num3 = (24 - GameScr.cmx) / 3;
							if (num3 != 0)
							{
								GameScr.cmx += num - num / num3;
							}
						}
						if (GameScr.cmx < (this.isVsMap() ? 24 : 0))
						{
							GameScr.cmx = (this.isVsMap() ? 24 : 0);
						}
						if (GameScr.cmx > GameScr.cmxLim)
						{
							int num4 = (GameScr.cmx - GameScr.cmxLim) / 3;
							if (num4 != 0)
							{
								GameScr.cmx += num - num / num4;
							}
						}
						if (GameScr.cmx > GameScr.cmxLim + ((!this.isVsMap()) ? 24 : 0))
						{
							GameScr.cmx = GameScr.cmxLim + ((!this.isVsMap()) ? 24 : 0);
						}
						if (GameScr.cmy < 0)
						{
							int num5 = -GameScr.cmy / 3;
							if (num5 != 0)
							{
								GameScr.cmy += num2 - num2 / num5;
							}
						}
						if (GameScr.cmy < -((!this.isVsMap()) ? 24 : 0))
						{
							GameScr.cmy = -((!this.isVsMap()) ? 24 : 0);
						}
						if (GameScr.cmy > GameScr.cmyLim)
						{
							GameScr.cmy = GameScr.cmyLim;
						}
						GameScr.cmtoX = GameScr.cmx;
						GameScr.cmtoY = GameScr.cmy;
					}
				}
				if (this.isPointerDowning && GameCanvas.isPointerJustRelease)
				{
					this.isPointerDowning = false;
					this.isChangingCameraMode = false;
					if (Res.abs(GameCanvas.px - this.ptFirstDownX) > 15 || Res.abs(GameCanvas.py - this.ptFirstDownY) > 15)
					{
						GameCanvas.isPointerJustRelease = false;
					}
				}
			}
		}
	}

	// Token: 0x060002DE RID: 734 RVA: 0x00044800 File Offset: 0x00042A00
	private void checkClick()
	{
		if (!this.isCharging())
		{
			if (this.popUpYesNo != null && this.popUpYesNo.cmdYes != null && this.popUpYesNo.cmdYes.isPointerPressInside())
			{
				this.popUpYesNo.cmdYes.performAction();
				return;
			}
			if (!this.checkClickToCapcha())
			{
				long num = mSystem.currentTimeMillis();
				if (this.lastSingleClick != 0L && num - this.lastSingleClick > 300L)
				{
					this.lastSingleClick = 0L;
					GameCanvas.isPointerJustDown = false;
					if (!this.disableSingleClick)
					{
						this.checkSingleClick();
						GameCanvas.isPointerJustRelease = false;
					}
				}
				if (GameCanvas.isPointerJustRelease)
				{
					this.disableSingleClick = this.checkSingleClickEarly();
					if (num - this.lastSingleClick < 300L)
					{
						this.lastSingleClick = 0L;
						this.checkDoubleClick();
					}
					else
					{
						this.lastSingleClick = num;
						this.lastClickCMX = GameScr.cmx;
						this.lastClickCMY = GameScr.cmy;
					}
					GameCanvas.isPointerJustRelease = false;
				}
			}
		}
	}

	// Token: 0x060002DF RID: 735 RVA: 0x000448F4 File Offset: 0x00042AF4
	private IMapObject findClickToItem(int px, int py)
	{
		IMapObject mapObject = null;
		int num = 0;
		int num2 = 30;
		MyVector[] array = new MyVector[]
		{
			GameScr.vMob,
			GameScr.vNpc,
			GameScr.vItemMap,
			GameScr.vCharInMap
		};
		for (int i = 0; i < array.Length; i++)
		{
			for (int j = 0; j < array[i].size(); j++)
			{
				IMapObject mapObject2 = (IMapObject)array[i].elementAt(j);
				if (!mapObject2.isInvisible())
				{
					if (mapObject2 is Mob)
					{
						Mob mob = (Mob)mapObject2;
						if (mob.isMobMe && mob.Equals(global::Char.myCharz().mobMe))
						{
							goto IL_118;
						}
					}
					int x = mapObject2.getX();
					int y = mapObject2.getY();
					int w = mapObject2.getW();
					int h = mapObject2.getH();
					if (this.inRectangle(px, py, x - w / 2 - num2, y - h - num2, w + num2 * 2, h + num2 * 2))
					{
						if (mapObject == null)
						{
							mapObject = mapObject2;
							num = Res.abs(px - x) + Res.abs(py - y);
							if (i == 1)
							{
								return mapObject;
							}
						}
						else
						{
							int num3 = Res.abs(px - x) + Res.abs(py - y);
							if (num3 < num)
							{
								mapObject = mapObject2;
								num = num3;
							}
						}
					}
				}
				IL_118:;
			}
		}
		return mapObject;
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x0000449D File Offset: 0x0000269D
	private bool inRectangle(int xClick, int yClick, int x, int y, int w, int h)
	{
		return xClick >= x && xClick <= x + w && yClick >= y && yClick <= y + h;
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x00044A40 File Offset: 0x00042C40
	private bool checkSingleClickEarly()
	{
		int num = GameCanvas.px + GameScr.cmx;
		int num2 = GameCanvas.py + GameScr.cmy;
		global::Char.myCharz().cancelAttack();
		IMapObject mapObject = this.findClickToItem(num, num2);
		bool result;
		if (mapObject != null)
		{
			if (global::Char.myCharz().isAttacPlayerStatus() && global::Char.myCharz().charFocus != null && !mapObject.Equals(global::Char.myCharz().charFocus) && !mapObject.Equals(global::Char.myCharz().charFocus.mobMe) && mapObject is global::Char)
			{
				global::Char @char = (global::Char)mapObject;
				if (@char.cTypePk != 5 && !@char.isAttacPlayerStatus())
				{
					this.checkClickMoveTo(num, num2);
					return false;
				}
			}
			if ((global::Char.myCharz().mobFocus == mapObject || global::Char.myCharz().itemFocus == mapObject) && !Main.isPC)
			{
				this.doDoubleClickToObj(mapObject);
				result = true;
			}
			else if (TileMap.mapID == 51 && mapObject.Equals(global::Char.myCharz().npcFocus))
			{
				this.checkClickMoveTo(num, num2);
				result = false;
			}
			else if (global::Char.myCharz().skillPaint != null || global::Char.myCharz().arr != null || global::Char.myCharz().dart != null || global::Char.myCharz().skillInfoPaint() != null)
			{
				result = false;
			}
			else
			{
				global::Char.myCharz().focusManualTo(mapObject);
				mapObject.stopMoving();
				result = false;
			}
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x00044B8C File Offset: 0x00042D8C
	private void checkDoubleClick()
	{
		int num = GameCanvas.px + this.lastClickCMX;
		int num2 = GameCanvas.py + this.lastClickCMY;
		int cy = global::Char.myCharz().cy;
		if (!this.isLockKey)
		{
			IMapObject mapObject = this.findClickToItem(num, num2);
			if (mapObject != null)
			{
				if (mapObject is Mob && !this.isMeCanAttackMob((Mob)mapObject))
				{
					this.checkClickMoveTo(num, num2);
					return;
				}
				if (!this.checkClickToBotton(mapObject) && (mapObject.Equals(global::Char.myCharz().npcFocus) || this.mobCapcha == null))
				{
					if (global::Char.myCharz().isAttacPlayerStatus() && global::Char.myCharz().charFocus != null && !mapObject.Equals(global::Char.myCharz().charFocus) && !mapObject.Equals(global::Char.myCharz().charFocus.mobMe) && mapObject is global::Char)
					{
						global::Char @char = (global::Char)mapObject;
						if (@char.cTypePk != 5 && !@char.isAttacPlayerStatus())
						{
							this.checkClickMoveTo(num, num2);
							return;
						}
					}
					if (TileMap.mapID == 51 && mapObject.Equals(global::Char.myCharz().npcFocus))
					{
						this.checkClickMoveTo(num, num2);
						return;
					}
					this.doDoubleClickToObj(mapObject);
					return;
				}
			}
			else if (!this.checkClickToPopup(num, num2) && !this.checkClipTopChatPopUp(num, num2) && !Main.isPC)
			{
				this.checkClickMoveTo(num, num2);
			}
		}
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x00044CDC File Offset: 0x00042EDC
	private bool checkClickToBotton(IMapObject Object)
	{
		bool result;
		if (Object == null)
		{
			result = false;
		}
		else
		{
			int i = Object.getY();
			int num = global::Char.myCharz().cy;
			if (i < num)
			{
				while (i < num)
				{
					num -= 5;
					if (TileMap.tileTypeAt(global::Char.myCharz().cx, num, 8192))
					{
						this.auto = 0;
						global::Char.myCharz().cancelAttack();
						global::Char.myCharz().currentMovePoint = null;
						return true;
					}
				}
			}
			result = false;
		}
		return result;
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x00044D48 File Offset: 0x00042F48
	public void doDoubleClickToObj(IMapObject obj)
	{
		if ((obj.Equals(global::Char.myCharz().npcFocus) || this.mobCapcha == null) && !this.checkClickToBotton(obj))
		{
			this.checkEffToObj(obj);
			global::Char.myCharz().cancelAttack();
			global::Char.myCharz().currentMovePoint = null;
			global::Char.myCharz().cvx = (global::Char.myCharz().cvy = 0);
			obj.stopMoving();
			this.auto = 10;
			this.doFire(false, true);
			this.clickToX = obj.getX();
			this.clickToY = obj.getY();
			this.clickOnTileTop = false;
			this.clickMoving = true;
			this.clickMovingRed = true;
			this.clickMovingTimeOut = 20;
			this.clickMovingP1 = 30;
		}
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00044E08 File Offset: 0x00043008
	private void checkSingleClick()
	{
		int xClick = GameCanvas.px + this.lastClickCMX;
		int yClick = GameCanvas.py + this.lastClickCMY;
		if (!this.isLockKey && !this.checkClickToPopup(xClick, yClick) && !this.checkClipTopChatPopUp(xClick, yClick))
		{
			this.checkClickMoveTo(xClick, yClick);
		}
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00044E54 File Offset: 0x00043054
	private bool checkClipTopChatPopUp(int xClick, int yClick)
	{
		bool result;
		if (this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null)
		{
			result = false;
		}
		else
		{
			if (GameScr.info2.info.info != null && GameScr.info2.info.info.charInfo != null)
			{
				int x = Res.abs(GameScr.info2.cmx) + GameScr.info2.info.X - 40;
				int y = Res.abs(GameScr.info2.cmy) + GameScr.info2.info.Y;
				if (this.inRectangle(xClick - GameScr.cmx, yClick - GameScr.cmy, x, y, 200, GameScr.info2.info.H))
				{
					GameScr.info2.doClick(10);
					return true;
				}
			}
			result = false;
		}
		return result;
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x00044F2C File Offset: 0x0004312C
	private bool checkClickToPopup(int xClick, int yClick)
	{
		for (int i = 0; i < PopUp.vPopups.size(); i++)
		{
			PopUp popUp = (PopUp)PopUp.vPopups.elementAt(i);
			if (this.inRectangle(xClick, yClick, popUp.cx, popUp.cy, popUp.cw, popUp.ch))
			{
				bool result;
				if (popUp.cy <= 24 && TileMap.isInAirMap() && global::Char.myCharz().cTypePk != 0)
				{
					result = false;
				}
				else
				{
					if (!popUp.isPaint)
					{
						goto IL_6C;
					}
					popUp.doClick(10);
					result = true;
				}
				return result;
			}
			IL_6C:;
		}
		return false;
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x00044FB8 File Offset: 0x000431B8
	private void checkClickMoveTo(int xClick, int yClick)
	{
		if (!GameScr.gamePad.disableClickMove())
		{
			global::Char.myCharz().cancelAttack();
			if (xClick < TileMap.pxw && xClick > TileMap.pxw - 32)
			{
				global::Char.myCharz().currentMovePoint = new MovePoint(TileMap.pxw, yClick);
				return;
			}
			if (xClick < 32 && xClick > 0)
			{
				global::Char.myCharz().currentMovePoint = new MovePoint(0, yClick);
				return;
			}
			if (xClick < TileMap.pxw && xClick > TileMap.pxw - 48)
			{
				global::Char.myCharz().currentMovePoint = new MovePoint(TileMap.pxw, yClick);
				return;
			}
			if (xClick < 48 && xClick > 0)
			{
				global::Char.myCharz().currentMovePoint = new MovePoint(0, yClick);
				return;
			}
			this.clickToX = xClick;
			this.clickToY = yClick;
			this.clickOnTileTop = false;
			global::Char.myCharz().delayFall = 0;
			int num = (!global::Char.myCharz().canFly || global::Char.myCharz().cMP <= 0) ? 1000 : 0;
			if (this.clickToY <= global::Char.myCharz().cy || Res.abs(this.clickToX - global::Char.myCharz().cx) >= 12)
			{
				int num2 = 0;
				while (num2 < 60 + num && this.clickToY + num2 < TileMap.pxh - 24)
				{
					if (TileMap.tileTypeAt(this.clickToX, this.clickToY + num2, 2))
					{
						this.clickToY = TileMap.tileYofPixel(this.clickToY + num2);
						this.clickOnTileTop = true;
						break;
					}
					num2 += 24;
				}
				for (int i = 0; i < 40 + num; i += 24)
				{
					if (TileMap.tileTypeAt(this.clickToX, this.clickToY - i, 2))
					{
						this.clickToY = TileMap.tileYofPixel(this.clickToY - i);
						this.clickOnTileTop = true;
						break;
					}
				}
				this.clickMoving = true;
				this.clickMovingRed = false;
				this.clickMovingP1 = ((!this.clickOnTileTop) ? 30 : ((yClick >= this.clickToY) ? this.clickToY : yClick));
				global::Char.myCharz().delayFall = 0;
				if (!this.clickOnTileTop && this.clickToY < global::Char.myCharz().cy - 50)
				{
					global::Char.myCharz().delayFall = 20;
				}
				this.clickMovingTimeOut = 30;
				this.auto = 0;
				if (global::Char.myCharz().holder)
				{
					global::Char.myCharz().removeHoleEff();
				}
				global::Char.myCharz().currentMovePoint = new MovePoint(this.clickToX, this.clickToY);
				global::Char.myCharz().cdir = ((global::Char.myCharz().cx - global::Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : -1);
				global::Char.myCharz().endMovePointCommand = null;
				GameScr.isAutoPlay = false;
			}
		}
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x00045250 File Offset: 0x00043450
	private void checkAuto()
	{
		long num = mSystem.currentTimeMillis();
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] || GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] || GameCanvas.keyPressed[1] || GameCanvas.keyPressed[3])
		{
			this.auto = 0;
			GameScr.isAutoPlay = false;
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] && !this.isPaintPopup())
		{
			if (this.auto == 0)
			{
				if (num - this.lastFire < 800L && this.checkSkillValid2() && (global::Char.myCharz().mobFocus != null || (global::Char.myCharz().charFocus != null && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus))))
				{
					Res.outz("toi day");
					this.auto = 10;
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				}
			}
			else
			{
				this.auto = 0;
				GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = false);
			}
			this.lastFire = num;
		}
		if (GameCanvas.gameTick % 5 == 0 && this.auto > 0 && global::Char.myCharz().currentMovePoint == null)
		{
			if (global::Char.myCharz().myskill != null && (global::Char.myCharz().myskill.template.isUseAlone() || global::Char.myCharz().myskill.paintCanNotUseSkill))
			{
				return;
			}
			if ((global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.status != 1 && global::Char.myCharz().mobFocus.status != 0 && global::Char.myCharz().charFocus == null) || (global::Char.myCharz().charFocus != null && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus)))
			{
				if (global::Char.myCharz().myskill.paintCanNotUseSkill)
				{
					return;
				}
				this.doFire(false, true);
			}
		}
		if (this.auto > 1)
		{
			this.auto--;
		}
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0004547C File Offset: 0x0004367C
	public void doUseHP()
	{
		if (!global::Char.myCharz().stone && !global::Char.myCharz().blindEff && global::Char.myCharz().holdEffID <= 0)
		{
			long num = mSystem.currentTimeMillis();
			if (num - this.lastUsePotion >= 10000L)
			{
				if (!global::Char.myCharz().doUsePotion())
				{
					GameScr.info1.addInfo(mResources.HP_EMPTY, 0);
					return;
				}
				ServerEffect.addServerEffect(11, global::Char.myCharz(), 5);
				ServerEffect.addServerEffect(104, global::Char.myCharz(), 4);
				this.lastUsePotion = num;
				SoundMn.gI().eatPeans();
			}
		}
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0004550C File Offset: 0x0004370C
	public void activeSuperPower(int x, int y)
	{
		if (!this.isSuperPower)
		{
			SoundMn.gI().bigeExlode();
			this.isSuperPower = true;
			this.tPower = 0;
			this.dxPower = 0;
			this.xPower = x - GameScr.cmx;
			this.yPower = y - GameScr.cmy;
		}
	}

	// Token: 0x060002EC RID: 748 RVA: 0x000044BC File Offset: 0x000026BC
	public void activeRongThanEff(bool isMe)
	{
		this.activeRongThan = true;
		this.isUseFreez = true;
		this.isMeCallRongThan = true;
		if (isMe)
		{
			EffecMn.addEff(new Effect(20, global::Char.myCharz().cx, global::Char.myCharz().cy - 77, 2, 8, 1));
		}
	}

	// Token: 0x060002ED RID: 749 RVA: 0x000044FC File Offset: 0x000026FC
	public void hideRongThanEff()
	{
		this.activeRongThan = false;
		this.isUseFreez = true;
		this.isMeCallRongThan = false;
	}

	// Token: 0x060002EE RID: 750 RVA: 0x00004513 File Offset: 0x00002713
	public void doiMauTroi()
	{
		this.isRongThanXuatHien = true;
		this.mautroi = mGraphics.blendColor(0.4f, 0, GameCanvas.colorTop[GameCanvas.colorTop.Length - 1]);
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0004555C File Offset: 0x0004375C
	public void callRongThan(int x, int y)
	{
		Res.outz(string.Concat(new object[]
		{
			"VE RONG THAN O VI TRI x= ",
			x,
			" y=",
			y
		}));
		this.doiMauTroi();
		EffecMn.addEff(new Effect((!this.isRongNamek) ? 17 : 25, x, y - 77, 2, -1, 1));
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0000453C File Offset: 0x0000273C
	public void hideRongThan()
	{
		this.isRongThanXuatHien = false;
		EffecMn.removeEff(17);
		if (this.isRongNamek)
		{
			this.isRongNamek = false;
			EffecMn.removeEff(25);
		}
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x000455C4 File Offset: 0x000437C4
	private void autoPlay()
	{
		if (!Pk9rPickMob.IsTanSat)
		{
			if (this.timeSkill > 0)
			{
				this.timeSkill--;
			}
			if (GameScr.canAutoPlay && !GameScr.isChangeZone && global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5 && !global::Char.myCharz().isCharge && !global::Char.myCharz().isFlyAndCharge && !global::Char.myCharz().isUseChargeSkill())
			{
				bool flag = false;
				for (int i = 0; i < GameScr.vMob.size(); i++)
				{
					Mob mob = (Mob)GameScr.vMob.elementAt(i);
					if (mob.status != 0 && mob.status != 1)
					{
						flag = true;
					}
				}
				if (flag)
				{
					bool flag2 = false;
					for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
					{
						Item item = global::Char.myCharz().arrItemBag[j];
						if (item != null && item.template.type == 6)
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2 && GameCanvas.gameTick % 150 == 0)
					{
						Service.gI().requestPean();
					}
					if (global::Char.myCharz().cHP <= global::Char.myCharz().cHPFull * 20 / 100 || global::Char.myCharz().cMP <= global::Char.myCharz().cMPFull * 20 / 100)
					{
						this.doUseHP();
					}
					if (global::Char.myCharz().mobFocus == null || (global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.isMobMe))
					{
						for (int k = 0; k < GameScr.vMob.size(); k++)
						{
							Mob mob2 = (Mob)GameScr.vMob.elementAt(k);
							if (mob2.status != 0 && mob2.status != 1 && mob2.hp > 0 && !mob2.isMobMe)
							{
								global::Char.myCharz().cx = mob2.x;
								global::Char.myCharz().cy = mob2.y;
								global::Char.myCharz().mobFocus = mob2;
								Service.gI().charMove();
								Res.outz("focus 1 con bossssssssssssssssssssssssssssssssssssssssssssssssss");
								break;
							}
						}
					}
					else if (global::Char.myCharz().mobFocus.hp <= 0 || global::Char.myCharz().mobFocus.status == 1 || global::Char.myCharz().mobFocus.status == 0)
					{
						global::Char.myCharz().mobFocus = null;
					}
					if (global::Char.myCharz().mobFocus != null && this.timeSkill == 0 && (global::Char.myCharz().skillInfoPaint() == null || global::Char.myCharz().indexSkill >= global::Char.myCharz().skillInfoPaint().Length || global::Char.myCharz().dart == null || global::Char.myCharz().arr == null))
					{
						Skill skill = null;
						if (GameCanvas.isTouch)
						{
							for (int l = 0; l < GameScr.onScreenSkill.Length; l++)
							{
								if (GameScr.onScreenSkill[l] != null && !GameScr.onScreenSkill[l].paintCanNotUseSkill && GameScr.onScreenSkill[l].template.id != 10 && GameScr.onScreenSkill[l].template.id != 11 && GameScr.onScreenSkill[l].template.id != 14 && GameScr.onScreenSkill[l].template.id != 23 && GameScr.onScreenSkill[l].template.id != 7 && global::Char.myCharz().skillInfoPaint() == null)
								{
									int num = (GameScr.onScreenSkill[l].template.manaUseType == 2) ? 1 : ((GameScr.onScreenSkill[l].template.manaUseType == 1) ? (GameScr.onScreenSkill[l].manaUse * global::Char.myCharz().cMPFull / 100) : GameScr.onScreenSkill[l].manaUse);
									if (global::Char.myCharz().cMP >= num)
									{
										if (skill == null)
										{
											skill = GameScr.onScreenSkill[l];
										}
										else if (skill.coolDown < GameScr.onScreenSkill[l].coolDown)
										{
											skill = GameScr.onScreenSkill[l];
										}
									}
								}
							}
							if (skill != null)
							{
								this.doSelectSkill(skill, true);
								this.doDoubleClickToObj(global::Char.myCharz().mobFocus);
								return;
							}
						}
						else
						{
							for (int m = 0; m < GameScr.keySkill.Length; m++)
							{
								if (GameScr.keySkill[m] != null && !GameScr.keySkill[m].paintCanNotUseSkill && GameScr.keySkill[m].template.id != 10 && GameScr.keySkill[m].template.id != 11 && GameScr.keySkill[m].template.id != 14 && GameScr.keySkill[m].template.id != 23 && GameScr.keySkill[m].template.id != 7 && global::Char.myCharz().skillInfoPaint() == null)
								{
									int num2 = (GameScr.keySkill[m].template.manaUseType == 2) ? 1 : ((GameScr.keySkill[m].template.manaUseType == 1) ? (GameScr.keySkill[m].manaUse * global::Char.myCharz().cMPFull / 100) : GameScr.keySkill[m].manaUse);
									if (global::Char.myCharz().cMP >= num2)
									{
										if (skill == null)
										{
											skill = GameScr.keySkill[m];
										}
										else if (skill.coolDown < GameScr.keySkill[m].coolDown)
										{
											skill = GameScr.keySkill[m];
										}
									}
								}
							}
							if (skill != null)
							{
								this.doSelectSkill(skill, true);
								this.doDoubleClickToObj(global::Char.myCharz().mobFocus);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x00045B9C File Offset: 0x00043D9C
	private void doFire(bool isFireByShortCut, bool skipWaypoint)
	{
		GameScr.tam++;
		Waypoint waypoint = global::Char.myCharz().isInEnterOfflinePoint();
		Waypoint waypoint2 = global::Char.myCharz().isInEnterOnlinePoint();
		if (!skipWaypoint && waypoint != null && (global::Char.myCharz().mobFocus == null || (global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.templateId == 0)))
		{
			waypoint.popup.command.performAction();
			return;
		}
		if (!skipWaypoint && waypoint2 != null && (global::Char.myCharz().mobFocus == null || (global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.templateId == 0)))
		{
			waypoint2.popup.command.performAction();
			return;
		}
		if ((TileMap.mapID != 51 || global::Char.myCharz().npcFocus == null) && global::Char.myCharz().statusMe != 14)
		{
			global::Char.myCharz().cvx = (global::Char.myCharz().cvy = 0);
			if (global::Char.myCharz().isSelectingSkillUseAlone() && global::Char.myCharz().focusToAttack())
			{
				if (this.checkSkillValid())
				{
					global::Char.myCharz().currentFireByShortcut = isFireByShortCut;
					global::Char.myCharz().useSkillNotFocus();
				}
			}
			else if (this.isAttack())
			{
				if (global::Char.myCharz().isUseChargeSkill() && global::Char.myCharz().focusToAttack())
				{
					if (this.checkSkillValid())
					{
						global::Char.myCharz().currentFireByShortcut = isFireByShortCut;
						global::Char.myCharz().sendUseChargeSkill();
					}
					else
					{
						global::Char.myCharz().stopUseChargeSkill();
					}
				}
				else
				{
					bool flag = TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2);
					global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], (!flag) ? 1 : 0);
					if (flag)
					{
						global::Char.myCharz().delayFall = 20;
					}
					global::Char.myCharz().currentFireByShortcut = isFireByShortCut;
				}
			}
			if (global::Char.myCharz().isSelectingSkillBuffToPlayer())
			{
				this.auto = 0;
			}
		}
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x00045D88 File Offset: 0x00043F88
	private void askToPick()
	{
		Npc npc = new Npc(5, 0, -100, 100, 5, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
		string nhatvatpham = mResources.nhatvatpham;
		string[] menu = new string[]
		{
			mResources.YES,
			mResources.NO
		};
		npc.idItem = 673;
		GameScr.gI().createMenu(menu, npc);
		ChatPopup.addChatPopupWithIcon(nhatvatpham, 100000, npc, 5820);
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x00045E00 File Offset: 0x00044000
	private void pickItem()
	{
		if (global::Char.myCharz().itemFocus != null)
		{
			if (global::Char.myCharz().cx < global::Char.myCharz().itemFocus.x)
			{
				global::Char.myCharz().cdir = 1;
			}
			else
			{
				global::Char.myCharz().cdir = -1;
			}
			int num = global::Math.abs(global::Char.myCharz().cx - global::Char.myCharz().itemFocus.x);
			int num2 = global::Math.abs(global::Char.myCharz().cy - global::Char.myCharz().itemFocus.y);
			if (num <= 40 && num2 < 40)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				if (global::Char.myCharz().itemFocus.template.id != 673)
				{
					Service.gI().pickItem(global::Char.myCharz().itemFocus.itemMapID);
					return;
				}
				this.askToPick();
				return;
			}
			else
			{
				global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().itemFocus.x, global::Char.myCharz().itemFocus.y);
				global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
			}
		}
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00045F2C File Offset: 0x0004412C
	public bool isCharging()
	{
		return global::Char.myCharz().isFlyAndCharge || global::Char.myCharz().isUseSkillAfterCharge || global::Char.myCharz().isStandAndCharge || global::Char.myCharz().isWaitMonkey || this.isSuperPower || global::Char.myCharz().isFreez;
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x00045F80 File Offset: 0x00044180
	public void doSelectSkill(Skill skill, bool isShortcut)
	{
		if (!global::Char.myCharz().isCreateDark && !this.isCharging() && global::Char.myCharz().taskMaint.taskId > 1)
		{
			global::Char.myCharz().myskill = skill;
			if (this.lastSkill != skill && this.lastSkill != null)
			{
				Service.gI().selectSkill((int)skill.template.id);
				this.saveRMSCurrentSkill(skill.template.id);
				this.resetButton();
				this.lastSkill = skill;
				this.selectedIndexSkill = -1;
				GameScr.gI().auto = 0;
				return;
			}
			if (global::Char.myCharz().isSelectingSkillUseAlone())
			{
				Res.outz("use skill not focus");
				this.doUseSkillNotFocus(skill);
				this.lastSkill = skill;
				return;
			}
			this.selectedIndexSkill = -1;
			if (skill != null)
			{
				Res.outz("only select skill");
				if (this.lastSkill != skill)
				{
					Service.gI().selectSkill((int)skill.template.id);
					this.saveRMSCurrentSkill(skill.template.id);
					this.resetButton();
				}
				if (global::Char.myCharz().charFocus != null || !global::Char.myCharz().isSelectingSkillBuffToPlayer())
				{
					if (global::Char.myCharz().focusToAttack())
					{
						this.doFire(isShortcut, true);
						this.doSeleckSkillFlag = true;
					}
					this.lastSkill = skill;
				}
			}
		}
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x000460C8 File Offset: 0x000442C8
	public void doUseSkill(Skill skill, bool isShortcut)
	{
		if ((TileMap.mapID != 112 && TileMap.mapID != 113) || global::Char.myCharz().cTypePk != 0)
		{
			if (global::Char.myCharz().isSelectingSkillUseAlone())
			{
				Res.outz("HERE");
				this.doUseSkillNotFocus(skill);
				return;
			}
			this.selectedIndexSkill = -1;
			if (skill != null)
			{
				Service.gI().selectSkill((int)skill.template.id);
				this.saveRMSCurrentSkill(skill.template.id);
				this.resetButton();
				global::Char.myCharz().myskill = skill;
				this.doFire(isShortcut, true);
			}
		}
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x0004615C File Offset: 0x0004435C
	public void doUseSkillNotFocus(Skill skill)
	{
		if (((TileMap.mapID != 112 && TileMap.mapID != 113) || global::Char.myCharz().cTypePk != 0) && this.checkSkillValid())
		{
			this.selectedIndexSkill = -1;
			if (skill != null)
			{
				Service.gI().selectSkill((int)skill.template.id);
				this.saveRMSCurrentSkill(skill.template.id);
				this.resetButton();
				global::Char.myCharz().myskill = skill;
				global::Char.myCharz().useSkillNotFocus();
				global::Char.myCharz().currentFireByShortcut = true;
				this.auto = 0;
			}
		}
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x000461EC File Offset: 0x000443EC
	public void sortSkill()
	{
		for (int i = 0; i < global::Char.myCharz().vSkillFight.size() - 1; i++)
		{
			Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(i);
			for (int j = i + 1; j < global::Char.myCharz().vSkillFight.size(); j++)
			{
				Skill skill2 = (Skill)global::Char.myCharz().vSkillFight.elementAt(j);
				if (skill2.template.id < skill.template.id)
				{
					Skill skill3 = skill2;
					skill2 = skill;
					skill = skill3;
					global::Char.myCharz().vSkillFight.setElementAt(skill, i);
					global::Char.myCharz().vSkillFight.setElementAt(skill2, j);
				}
			}
		}
	}

	// Token: 0x060002FA RID: 762 RVA: 0x000462A4 File Offset: 0x000444A4
	public void updateKeyTouchCapcha()
	{
		if (!this.isNotPaintTouchControl())
		{
			for (int i = 0; i < this.strCapcha.Length; i++)
			{
				this.keyCapcha[i] = -1;
				if (GameCanvas.isTouchControl)
				{
					int num = (GameCanvas.w - this.strCapcha.Length * GameScr.disXC) / 2;
					int w = this.strCapcha.Length * GameScr.disXC;
					int y = GameCanvas.h - 40;
					int h = GameScr.disXC;
					if (GameCanvas.isPointerHoldIn(num, y, w, h))
					{
						int num2 = (GameCanvas.px - num) / GameScr.disXC;
						if (i == num2)
						{
							this.keyCapcha[i] = 1;
						}
						if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease && i == num2)
						{
							char[] array = this.keyInput.ToCharArray();
							MyVector myVector = new MyVector();
							for (int j = 0; j < array.Length; j++)
							{
								myVector.addElement(array[j].ToString() + string.Empty);
							}
							myVector.removeElementAt(0);
							myVector.insertElementAt(this.strCapcha[i].ToString() + string.Empty, myVector.size());
							this.keyInput = string.Empty;
							for (int k = 0; k < myVector.size(); k++)
							{
								this.keyInput += ((string)myVector.elementAt(k)).ToUpper();
							}
							Service.gI().mobCapcha(this.strCapcha[i]);
						}
					}
				}
			}
		}
	}

	// Token: 0x060002FB RID: 763 RVA: 0x00046448 File Offset: 0x00044648
	public bool checkClickToCapcha()
	{
		bool result;
		if (this.mobCapcha == null)
		{
			result = false;
		}
		else
		{
			int x = (GameCanvas.w - 5 * GameScr.disXC) / 2;
			int w = 5 * GameScr.disXC;
			int y = GameCanvas.h - 40;
			int h = GameScr.disXC;
			result = GameCanvas.isPointerHoldIn(x, y, w, h);
		}
		return result;
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00046494 File Offset: 0x00044694
	public void checkMouseChat()
	{
		if (GameCanvas.isMouseFocus(GameScr.xC, GameScr.yC, 34, 34))
		{
			if (!TileMap.isOfflineMap())
			{
				mScreen.keyMouse = 15;
				return;
			}
		}
		else if (GameCanvas.isMouseFocus(GameScr.xHP, GameScr.yHP, 40, 40))
		{
			if (global::Char.myCharz().statusMe != 14)
			{
				mScreen.keyMouse = 10;
				return;
			}
		}
		else if (GameCanvas.isMouseFocus(GameScr.xF, GameScr.yF, 40, 40))
		{
			if (global::Char.myCharz().statusMe != 14)
			{
				mScreen.keyMouse = 5;
				return;
			}
		}
		else
		{
			if (this.cmdMenu != null && GameCanvas.isMouseFocus(this.cmdMenu.x, this.cmdMenu.y, this.cmdMenu.w / 2, this.cmdMenu.h))
			{
				mScreen.keyMouse = 1;
				return;
			}
			mScreen.keyMouse = -1;
		}
	}

	// Token: 0x060002FD RID: 765 RVA: 0x00046568 File Offset: 0x00044768
	private void updateKeyTouchControl()
	{
		if (!this.isNotPaintTouchControl())
		{
			mScreen.keyTouch = -1;
			if (GameCanvas.isTouchControl)
			{
				if (GameCanvas.isPointerHoldIn(0, 0, 60, 50) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					if (global::Char.myCharz().cmdMenu != null)
					{
						global::Char.myCharz().cmdMenu.performAction();
					}
					global::Char.myCharz().currentMovePoint = null;
					GameCanvas.clearAllPointerEvent();
					this.flareFindFocus = true;
					this.flareTime = 5;
					return;
				}
				if (Main.isPC)
				{
					this.checkMouseChat();
				}
				if (!TileMap.isOfflineMap() && GameCanvas.isPointerHoldIn(GameScr.xC, GameScr.yC, 34, 34))
				{
					mScreen.keyTouch = 15;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						ChatTextField.gI().startChat(this, string.Empty);
						SoundMn.gI().buttonClick();
						global::Char.myCharz().currentMovePoint = null;
						GameCanvas.clearAllPointerEvent();
						return;
					}
				}
				if (global::Char.myCharz().cmdMenu != null && GameCanvas.isPointerHoldIn(global::Char.myCharz().cmdMenu.x - 17, global::Char.myCharz().cmdMenu.y - 17, 34, 34))
				{
					mScreen.keyTouch = 20;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						GameCanvas.clearAllPointerEvent();
						global::Char.myCharz().cmdMenu.performAction();
						return;
					}
				}
				this.updateGamePad();
				if (((GameScr.isAnalog != 0) ? GameCanvas.isPointerHoldIn(GameScr.xHP, GameScr.yHP, 34, 34) : GameCanvas.isPointerHoldIn(GameScr.xHP, GameScr.yHP, 40, 40)) && global::Char.myCharz().statusMe != 14 && this.mobCapcha == null)
				{
					mScreen.keyTouch = 10;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						GameCanvas.keyPressed[10] = true;
						GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
					}
				}
			}
			if (this.mobCapcha != null)
			{
				this.updateKeyTouchCapcha();
			}
			else if (GameScr.isHaveSelectSkill)
			{
				if (this.isCharging())
				{
					return;
				}
				this.keyTouchSkill = -1;
				if (VuDang.nvat)
				{
					int num = 95;
					for (int i = 0; i < VuDang.chars.Length; i++)
					{
						if (VuDang.chars[i] != null)
						{
							if (GameCanvas.isPointerHoldIn(GameCanvas.w - 155, num, 150, 10))
							{
								VuDang.GotoXY(VuDang.chars[i].cx, VuDang.chars[i].cy);
								global::Char.myCharz().focusManualTo(VuDang.chars[i]);
								global::Char.myCharz().currentMovePoint = null;
								GameCanvas.clearAllPointerEvent();
							}
							num += 10;
						}
					}
				}
				if (VuDang.thongBaoBoss)
				{
					for (int j = 0; j < VuDang.bossVip.size(); j++)
					{
						if (GameCanvas.isPointerHoldIn(GameCanvas.w - 23, 35 + 10 * j, 28, 9))
						{
							GameCanvas.isPointerJustDown = false;
							this.isPointerDowning = false;
							if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
							{
								if (!Pk9rXmap.IsXmapRunning)
								{
									XmapController.StartRunToMapId(VuDang.GetIDMap(((ShowBoss)VuDang.bossVip.elementAt(j)).mapName));
								}
								else
								{
									XmapController.FinishXmap();
								}
								global::Char.myCharz().currentMovePoint = null;
								GameCanvas.clearAllPointerEvent();
								return;
							}
						}
					}
				}
				bool flag = false;
				if (GameScr.onScreenSkill.Length > 5 && (GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12, GameScr.yS[0] - GameScr.wSkill / 2 + 12, 5 * GameScr.wSkill, GameScr.wSkill) || GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[5] - GameScr.wSkill / 2 + 12, GameScr.yS[5] - GameScr.wSkill / 2 + 12, 5 * GameScr.wSkill, GameScr.wSkill)))
				{
					flag = true;
				}
				if (flag || GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12, GameScr.yS[0] - GameScr.wSkill / 2 + 12, 5 * GameScr.wSkill, GameScr.wSkill) || (!GameCanvas.isTouchControl && GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12, GameScr.yS[0] - GameScr.wSkill / 2 + 12, GameScr.wSkill, GameScr.onScreenSkill.Length * GameScr.wSkill)))
				{
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					int num2 = (GameCanvas.pxLast - (GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12)) / GameScr.wSkill;
					if (flag && GameCanvas.pyLast < GameScr.yS[0])
					{
						num2 += 5;
					}
					this.keyTouchSkill = num2;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
						this.selectedIndexSkill = num2;
						if (GameScr.indexSelect < 0)
						{
							GameScr.indexSelect = 0;
						}
						if (!Main.isPC)
						{
							if (this.selectedIndexSkill > GameScr.onScreenSkill.Length - 1)
							{
								this.selectedIndexSkill = GameScr.onScreenSkill.Length - 1;
							}
						}
						else if (this.selectedIndexSkill > GameScr.keySkill.Length - 1)
						{
							this.selectedIndexSkill = GameScr.keySkill.Length - 1;
						}
						Skill skill = Main.isPC ? GameScr.keySkill[this.selectedIndexSkill] : GameScr.onScreenSkill[this.selectedIndexSkill];
						if (skill != null)
						{
							this.doSelectSkill(skill, true);
						}
					}
				}
			}
			if (GameCanvas.isPointerJustRelease)
			{
				if (GameCanvas.keyHold[1] || (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] || GameCanvas.keyHold[3]) || GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] || GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
				{
					GameCanvas.isPointerJustRelease = false;
				}
				GameCanvas.keyHold[1] = false;
				GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = false;
				GameCanvas.keyHold[3] = false;
				GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = false;
				GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = false;
			}
		}
	}

	// Token: 0x060002FE RID: 766 RVA: 0x00004562 File Offset: 0x00002762
	public void setCharJumpAtt()
	{
		global::Char.myCharz().cvy = -10;
		global::Char.myCharz().statusMe = 3;
		global::Char.myCharz().cp1 = 0;
	}

	// Token: 0x060002FF RID: 767 RVA: 0x00046B64 File Offset: 0x00044D64
	public void setCharJump(int cvx)
	{
		if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0)
		{
			Service.gI().charMove();
		}
		global::Char.myCharz().cvy = -10;
		global::Char.myCharz().cvx = cvx;
		global::Char.myCharz().statusMe = 3;
		global::Char.myCharz().cp1 = 0;
	}

	// Token: 0x06000300 RID: 768 RVA: 0x00046BD8 File Offset: 0x00044DD8
	public void updateOpen()
	{
		if (this.isstarOpen)
		{
			if (this.moveUp > -3)
			{
				this.moveUp -= 4;
			}
			else
			{
				this.moveUp = -2;
			}
			if (this.moveDow < GameCanvas.h + 3)
			{
				this.moveDow += 4;
			}
			else
			{
				this.moveDow = GameCanvas.h + 2;
			}
			if (this.moveUp <= -2 && this.moveDow >= GameCanvas.h + 2)
			{
				this.isstarOpen = false;
			}
		}
	}

	// Token: 0x06000301 RID: 769 RVA: 0x00004426 File Offset: 0x00002626
	public void initCreateCommand()
	{
	}

	// Token: 0x06000302 RID: 770 RVA: 0x00004426 File Offset: 0x00002626
	public void checkCharFocus()
	{
	}

	// Token: 0x06000303 RID: 771 RVA: 0x00046C5C File Offset: 0x00044E5C
	public void updateXoSo()
	{
		if (this.tShow != 0)
		{
			GameScr.currXS = mSystem.currentTimeMillis();
			if (GameScr.currXS - GameScr.lastXS > 1000L)
			{
				GameScr.lastXS = mSystem.currentTimeMillis();
				GameScr.secondXS++;
			}
			if (GameScr.secondXS > 20)
			{
				for (int i = 0; i < this.winnumber.Length; i++)
				{
					this.randomNumber[i] = this.winnumber[i];
				}
				this.tShow--;
				if (this.tShow == 0)
				{
					this.yourNumber = string.Empty;
					GameScr.info1.addInfo(this.strFinish, 0);
					GameScr.secondXS = 0;
					return;
				}
			}
			else if (this.moveIndex > this.winnumber.Length - 1)
			{
				this.tShow--;
				if (this.tShow == 0)
				{
					this.yourNumber = string.Empty;
					GameScr.info1.addInfo(this.strFinish, 0);
					return;
				}
			}
			else
			{
				if (this.moveIndex < this.randomNumber.Length)
				{
					if (this.tMove[this.moveIndex] == 15)
					{
						if (this.randomNumber[this.moveIndex] == this.winnumber[this.moveIndex] - 1)
						{
							this.delayMove[this.moveIndex] = 10;
						}
						if (this.randomNumber[this.moveIndex] == this.winnumber[this.moveIndex])
						{
							this.tMove[this.moveIndex] = -1;
							this.moveIndex++;
						}
					}
					else if (GameCanvas.gameTick % 5 == 0)
					{
						this.tMove[this.moveIndex]++;
					}
				}
				for (int j = 0; j < this.winnumber.Length; j++)
				{
					if (this.tMove[j] != -1)
					{
						this.moveCount[j]++;
						if (this.moveCount[j] > this.tMove[j] + this.delayMove[j])
						{
							this.moveCount[j] = 0;
							this.randomNumber[j]++;
							if (this.randomNumber[j] >= 10)
							{
								this.randomNumber[j] = 0;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000304 RID: 772 RVA: 0x00046E7C File Offset: 0x0004507C
	public override void update()
	{
		if (!Pk9rXmap.IsXmapRunning)
		{
			Pk9rPickMob.Update();
		}
		if (GameCanvas.keyPressed[16])
		{
			GameCanvas.keyPressed[16] = false;
			global::Char.myCharz().findNextFocusByKey();
		}
		if (GameCanvas.keyPressed[13] && !GameCanvas.panel.isShow)
		{
			GameCanvas.keyPressed[13] = false;
			global::Char.myCharz().findNextFocusByKey();
		}
		if (GameCanvas.keyPressed[17])
		{
			GameCanvas.keyPressed[17] = false;
			global::Char.myCharz().searchItem();
			if (global::Char.myCharz().itemFocus != null)
			{
				this.pickItem();
			}
		}
		if (GameCanvas.gameTick % 100 == 0 && TileMap.mapID == 137)
		{
			GameScr.shock_scr = 30;
		}
		if (GameScr.isAutoPlay && GameCanvas.gameTick % 20 == 0)
		{
			this.autoPlay();
		}
		this.updateXoSo();
		mSystem.checkAdComlete();
		SmallImage.update();
		try
		{
			if (LoginScr.isContinueToLogin)
			{
				LoginScr.isContinueToLogin = false;
			}
			if (GameScr.tickMove == 1)
			{
				GameScr.lastTick = mSystem.currentTimeMillis();
			}
			if (GameScr.tickMove == 100)
			{
				GameScr.tickMove = 0;
				GameScr.currTick = mSystem.currentTimeMillis();
				int second = (int)(GameScr.currTick - GameScr.lastTick) / 1000;
				Service.gI().checkMMove(second);
			}
			if (GameScr.lockTick > 0)
			{
				GameScr.lockTick--;
				if (GameScr.lockTick == 0)
				{
					Controller.isStopReadMessage = false;
				}
			}
			this.checkCharFocus();
			GameCanvas.debug("E1", 0);
			GameScr.updateCamera();
			GameCanvas.debug("E2", 0);
			ChatTextField.gI().update();
			GameCanvas.debug("E3", 0);
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				((global::Char)GameScr.vCharInMap.elementAt(i)).update();
			}
			for (int j = 0; j < Teleport.vTeleport.size(); j++)
			{
				((Teleport)Teleport.vTeleport.elementAt(j)).update();
			}
			global::Char.myCharz().update();
			int statusMe = global::Char.myCharz().statusMe;
			if (this.popUpYesNo != null)
			{
				this.popUpYesNo.update();
			}
			EffecMn.update();
			GameCanvas.debug("E5x", 0);
			for (int k = 0; k < GameScr.vMob.size(); k++)
			{
				((Mob)GameScr.vMob.elementAt(k)).update();
			}
			GameCanvas.debug("E6", 0);
			for (int l = 0; l < GameScr.vNpc.size(); l++)
			{
				((Npc)GameScr.vNpc.elementAt(l)).update();
			}
			this.nSkill = GameScr.onScreenSkill.Length;
			for (int m = GameScr.onScreenSkill.Length - 1; m >= 0; m--)
			{
				if (GameScr.onScreenSkill[m] != null)
				{
					this.nSkill = m + 1;
					break;
				}
				this.nSkill--;
			}
			if (this.nSkill == 1 && GameCanvas.isTouch)
			{
				GameScr.xSkill = -200;
			}
			else if (GameScr.xSkill < 0)
			{
				GameScr.setSkillBarPosition();
			}
			GameCanvas.debug("E7", 0);
			GameCanvas.gI().updateDust();
			GameCanvas.debug("E8", 0);
			GameScr.updateFlyText();
			PopUp.updateAll();
			GameScr.updateSplash();
			this.updateSS();
			GameCanvas.updateBG();
			GameCanvas.debug("E9", 0);
			this.updateClickToArrow();
			GameCanvas.debug("E10", 0);
			for (int n = 0; n < GameScr.vItemMap.size(); n++)
			{
				((ItemMap)GameScr.vItemMap.elementAt(n)).update();
			}
			GameCanvas.debug("E11", 0);
			GameCanvas.debug("E13", 0);
			for (int num = Effect2.vRemoveEffect2.size() - 1; num >= 0; num--)
			{
				Effect2.vEffect2.removeElement(Effect2.vRemoveEffect2.elementAt(num));
				Effect2.vRemoveEffect2.removeElementAt(num);
			}
			for (int num2 = 0; num2 < Effect2.vEffect2.size(); num2++)
			{
				((Effect2)Effect2.vEffect2.elementAt(num2)).update();
			}
			for (int num3 = 0; num3 < Effect2.vEffect2Outside.size(); num3++)
			{
				((Effect2)Effect2.vEffect2Outside.elementAt(num3)).update();
			}
			for (int num4 = 0; num4 < Effect2.vAnimateEffect.size(); num4++)
			{
				((Effect2)Effect2.vAnimateEffect.elementAt(num4)).update();
			}
			for (int num5 = 0; num5 < Effect2.vEffectFeet.size(); num5++)
			{
				((Effect2)Effect2.vEffectFeet.elementAt(num5)).update();
			}
			for (int num6 = 0; num6 < Effect2.vEffect3.size(); num6++)
			{
				((Effect2)Effect2.vEffect3.elementAt(num6)).update();
			}
			BackgroudEffect.updateEff();
			GameScr.info1.update();
			GameScr.info2.update();
			GameCanvas.debug("E15", 0);
			if (GameScr.currentCharViewInfo != null && !GameScr.currentCharViewInfo.Equals(global::Char.myCharz()))
			{
				GameScr.currentCharViewInfo.update();
			}
			this.runArrow++;
			if (this.runArrow > 3)
			{
				this.runArrow = 0;
			}
			if (this.isInjureHp)
			{
				this.twHp++;
				if (this.twHp == 20)
				{
					this.twHp = 0;
					this.isInjureHp = false;
				}
			}
			else if (this.dHP > global::Char.myCharz().cHP)
			{
				int num7 = this.dHP - global::Char.myCharz().cHP >> 1;
				if (num7 < 1)
				{
					num7 = 1;
				}
				this.dHP -= num7;
			}
			else
			{
				this.dHP = global::Char.myCharz().cHP;
			}
			if (this.isInjureMp)
			{
				this.twMp++;
				if (this.twMp == 20)
				{
					this.twMp = 0;
					this.isInjureMp = false;
				}
			}
			else if (this.dMP > global::Char.myCharz().cMP)
			{
				int num8 = this.dMP - global::Char.myCharz().cMP >> 1;
				if (num8 < 1)
				{
					num8 = 1;
				}
				this.dMP -= num8;
			}
			else
			{
				this.dMP = global::Char.myCharz().cMP;
			}
			if (this.tMenuDelay > 0)
			{
				this.tMenuDelay--;
			}
			if (this.isRongThanMenu())
			{
				int num9 = 100;
				while (this.yR - num9 < GameScr.cmy)
				{
					GameScr.cmy--;
				}
			}
			for (int num10 = 0; num10 < global::Char.vItemTime.size(); num10++)
			{
				((ItemTime)global::Char.vItemTime.elementAt(num10)).update();
			}
			for (int num11 = 0; num11 < GameScr.textTime.size(); num11++)
			{
				((ItemTime)GameScr.textTime.elementAt(num11)).update();
			}
			this.updateChatVip();
		}
		catch (Exception)
		{
		}
		if (GameCanvas.gameTick % 4000 == 1000)
		{
			GameScr.checkRemoveImage();
		}
		EffectManager.update();
	}

	// Token: 0x06000305 RID: 773 RVA: 0x00004426 File Offset: 0x00002626
	public void updateKeyChatPopUp()
	{
	}

	// Token: 0x06000306 RID: 774 RVA: 0x00004586 File Offset: 0x00002786
	public bool isRongThanMenu()
	{
		return this.isMeCallRongThan;
	}

	// Token: 0x06000307 RID: 775 RVA: 0x00047568 File Offset: 0x00045768
	public void paintEffect(mGraphics g)
	{
		for (int i = 0; i < Effect2.vEffect2.size(); i++)
		{
			Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
			if (!(effect is ChatPopup))
			{
				effect.paint(g);
			}
		}
		if (!GameCanvas.lowGraphic)
		{
			for (int j = 0; j < Effect2.vAnimateEffect.size(); j++)
			{
				((Effect2)Effect2.vAnimateEffect.elementAt(j)).paint(g);
			}
		}
		for (int k = 0; k < Effect2.vEffect2Outside.size(); k++)
		{
			((Effect2)Effect2.vEffect2Outside.elementAt(k)).paint(g);
		}
	}

	// Token: 0x06000308 RID: 776 RVA: 0x00047608 File Offset: 0x00045808
	public void paintBgItem(mGraphics g, int layer)
	{
		if (!VuDang.xoamap)
		{
			for (int i = 0; i < TileMap.vCurrItem.size(); i++)
			{
				BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(i);
				if (bgItem.idImage != -1 && (int)bgItem.layer == layer)
				{
					bgItem.paint(g);
				}
			}
		}
		if (TileMap.mapID == 48 && layer == 3 && GameCanvas.bgW != null && GameCanvas.bgW[0] != 0)
		{
			for (int j = 0; j < TileMap.pxw / GameCanvas.bgW[0] + 1; j++)
			{
				g.drawImage(GameCanvas.imgBG[0], j * GameCanvas.bgW[0], TileMap.pxh - GameCanvas.bgH[0] - 70, 0);
			}
		}
	}

	// Token: 0x06000309 RID: 777 RVA: 0x0000458E File Offset: 0x0000278E
	public void paintBlackSky(mGraphics g)
	{
		if (!GameCanvas.lowGraphic)
		{
			g.fillTrans(GameScr.imgTrans, 0, 0, GameCanvas.w, GameCanvas.h);
		}
	}

	// Token: 0x0600030A RID: 778 RVA: 0x000476BC File Offset: 0x000458BC
	public void paintCapcha(mGraphics g)
	{
		MobCapcha.paint(g, global::Char.myCharz().cx, global::Char.myCharz().cy);
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		if (!GameCanvas.menu.showMenu && !GameCanvas.panel.isShow && ChatPopup.currChatPopup == null && GameCanvas.isTouch)
		{
			for (int i = 0; i < this.strCapcha.Length; i++)
			{
				int x = (GameCanvas.w - this.strCapcha.Length * GameScr.disXC) / 2 + i * GameScr.disXC + GameScr.disXC / 2;
				if (this.keyCapcha[i] == -1)
				{
					g.drawImage(GameScr.imgNut, x, GameCanvas.h - 25, 3);
					mFont.tahoma_7b_dark.drawString(g, this.strCapcha[i].ToString() + string.Empty, x, GameCanvas.h - 30, 2);
				}
				else
				{
					g.drawImage(GameScr.imgNutF, x, GameCanvas.h - 25, 3);
					mFont.tahoma_7b_green2.drawString(g, this.strCapcha[i].ToString() + string.Empty, x, GameCanvas.h - 30, 2);
				}
			}
		}
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0004780C File Offset: 0x00045A0C
	public override void paint(mGraphics g)
	{
		if (VuDang.giamDungLuong)
		{
			Thread.Sleep(1);
		}
		GameScr.countEff = 0;
		if (GameScr.isPaint)
		{
			GameCanvas.debug("PA1", 1);
			if (this.isUseFreez && ChatPopup.currChatPopup == null)
			{
				this.dem++;
				if ((this.dem < 30 && this.dem >= 0 && GameCanvas.gameTick % 4 == 0) || (this.dem >= 30 && this.dem <= 50 && GameCanvas.gameTick % 3 == 0) || this.dem > 50)
				{
					if (this.dem <= 50)
					{
						return;
					}
					if (this.isUseFreez)
					{
						this.isUseFreez = false;
						this.dem = 0;
						if (this.activeRongThan)
						{
							this.callRongThan(this.xR, this.yR);
						}
						else
						{
							this.hideRongThan();
						}
					}
					if (!VuDang.giamDungLuong)
					{
						this.paintInfoBar(g);
						g.translate(-GameScr.cmx, -GameScr.cmy);
						g.translate(0, GameCanvas.transY);
						this.paintSelectedSkill(g);
					}
					global::Char.myCharz().paint(g);
					mSystem.paintFlyText(g);
					GameScr.resetTranslate(g);
					return;
				}
			}
			GameCanvas.debug("PA2", 1);
			GameCanvas.paintBGGameScr(g);
			if ((this.isRongThanXuatHien || this.isFireWorks) && TileMap.bgID != 3 && !VuDang.giamDungLuong)
			{
				this.paintBlackSky(g);
			}
			GameCanvas.debug("PA3", 1);
			if (GameScr.shock_scr > 0)
			{
				g.translate(-GameScr.cmx + GameScr.shock_x[GameScr.shock_scr % GameScr.shock_x.Length], -GameScr.cmy + GameScr.shock_y[GameScr.shock_scr % GameScr.shock_y.Length]);
				GameScr.shock_scr--;
			}
			else
			{
				g.translate(-GameScr.cmx, -GameScr.cmy);
			}
			if (this.isSuperPower)
			{
				int tx = (GameCanvas.gameTick % 3 != 0) ? -3 : 3;
				g.translate(tx, 0);
			}
			if (!VuDang.giamDungLuong)
			{
				BackgroudEffect.paintBehindTileAll(g);
				EffecMn.paintLayer1(g);
				TileMap.paintTilemap(g);
				TileMap.paintOutTilemap(g);
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
					if (@char.isMabuHold && TileMap.mapID == 128)
					{
						@char.paintHeadWithXY(g, @char.cx, @char.cy, 0);
					}
				}
				if (global::Char.myCharz().isMabuHold && TileMap.mapID == 128)
				{
					global::Char.myCharz().paintHeadWithXY(g, global::Char.myCharz().cx, global::Char.myCharz().cy, 0);
				}
				this.paintBgItem(g, 2);
				if (global::Char.myCharz().cmdMenu != null && GameCanvas.isTouch)
				{
					if (mScreen.keyTouch == 20)
					{
						g.drawImage(GameScr.imgChat2, global::Char.myCharz().cmdMenu.x + GameScr.cmx, global::Char.myCharz().cmdMenu.y + GameScr.cmy, mGraphics.HCENTER | mGraphics.VCENTER);
					}
					else
					{
						g.drawImage(GameScr.imgChat, global::Char.myCharz().cmdMenu.x + GameScr.cmx, global::Char.myCharz().cmdMenu.y + GameScr.cmy, mGraphics.HCENTER | mGraphics.VCENTER);
					}
				}
			}
			GameCanvas.debug("PA4", 1);
			GameCanvas.debug("PA5", 1);
			if (!VuDang.giamDungLuong)
			{
				BackgroudEffect.paintBackAll(g);
				EffectManager.lowEffects.paintAll(g);
				for (int j = 0; j < Effect2.vEffectFeet.size(); j++)
				{
					((Effect2)Effect2.vEffectFeet.elementAt(j)).paint(g);
				}
				for (int k = 0; k < Teleport.vTeleport.size(); k++)
				{
					((Teleport)Teleport.vTeleport.elementAt(k)).paintHole(g);
				}
				for (int l = 0; l < GameScr.vNpc.size(); l++)
				{
					Npc npc = (Npc)GameScr.vNpc.elementAt(l);
					if (npc.cHP > 0)
					{
						npc.paintShadow(g);
					}
				}
			}
			for (int m = 0; m < GameScr.vNpc.size(); m++)
			{
				((Npc)GameScr.vNpc.elementAt(m)).paint(g);
			}
			g.translate(0, GameCanvas.transY);
			GameCanvas.debug("PA7", 1);
			GameCanvas.debug("PA8", 1);
			for (int n = 0; n < GameScr.vCharInMap.size(); n++)
			{
				global::Char char2 = null;
				try
				{
					char2 = (global::Char)GameScr.vCharInMap.elementAt(n);
				}
				catch (Exception ex)
				{
					Cout.LogError("Loi ham paint char gamesc: " + ex.ToString());
				}
				if (char2 != null && (!GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop()) && char2.isShadown)
				{
					char2.paintShadow(g);
				}
			}
			global::Char.myCharz().paintShadow(g);
			EffecMn.paintLayer2(g);
			for (int num = 0; num < GameScr.vMob.size(); num++)
			{
				((Mob)GameScr.vMob.elementAt(num)).paint(g);
			}
			for (int num2 = 0; num2 < Teleport.vTeleport.size(); num2++)
			{
				((Teleport)Teleport.vTeleport.elementAt(num2)).paint(g);
			}
			for (int num3 = 0; num3 < GameScr.vCharInMap.size(); num3++)
			{
				global::Char char3 = null;
				try
				{
					char3 = (global::Char)GameScr.vCharInMap.elementAt(num3);
				}
				catch (Exception)
				{
				}
				if (char3 != null && (!GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop()))
				{
					char3.paint(g);
				}
			}
			global::Char.myCharz().paint(g);
			if (global::Char.myCharz().skillPaint != null && global::Char.myCharz().skillInfoPaint() != null && global::Char.myCharz().indexSkill < global::Char.myCharz().skillInfoPaint().Length)
			{
				global::Char.myCharz().paintCharWithSkill(g);
				global::Char.myCharz().paintMount2(g);
			}
			for (int num4 = 0; num4 < GameScr.vCharInMap.size(); num4++)
			{
				global::Char char4 = null;
				try
				{
					char4 = (global::Char)GameScr.vCharInMap.elementAt(num4);
				}
				catch (Exception ex2)
				{
					Cout.LogError("Loi ham paint char gamescr: " + ex2.ToString());
				}
				if (char4 != null && (!GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop()) && char4.skillPaint != null && char4.skillInfoPaint() != null && char4.indexSkill < char4.skillInfoPaint().Length)
				{
					char4.paintCharWithSkill(g);
					char4.paintMount2(g);
				}
			}
			for (int num5 = 0; num5 < GameScr.vItemMap.size(); num5++)
			{
				((ItemMap)GameScr.vItemMap.elementAt(num5)).paint(g);
			}
			g.translate(0, -GameCanvas.transY);
			GameCanvas.debug("PA9", 1);
			GameScr.paintSplash(g);
			GameCanvas.debug("PA10", 1);
			GameCanvas.debug("PA11", 1);
			GameCanvas.debug("PA13", 1);
			if (!VuDang.giamDungLuong)
			{
				this.paintEffect(g);
				EffectManager.midEffects.paintAll(g);
				this.paintBgItem(g, 3);
				for (int num6 = 0; num6 < GameScr.vNpc.size(); num6++)
				{
					((Npc)GameScr.vNpc.elementAt(num6)).paintName(g);
				}
				EffecMn.paintLayer3(g);
				for (int num7 = 0; num7 < GameScr.vNpc.size(); num7++)
				{
					Npc npc2 = (Npc)GameScr.vNpc.elementAt(num7);
					if (npc2.chatInfo != null && npc2 != null)
					{
						npc2.chatInfo.paint(g, npc2.cx, npc2.cy - npc2.ch - GameCanvas.transY, npc2.cdir);
					}
				}
				BackgroudEffect.paintFrontAll(g);
				if (!VuDang.xoamap)
				{
					for (int num8 = 0; num8 < TileMap.vCurrItem.size(); num8++)
					{
						BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(num8);
						if (bgItem.idImage != -1 && bgItem.layer > 3)
						{
							bgItem.paint(g);
						}
					}
				}
				PopUp.paintAll(g);
				if (TileMap.mapID == 120)
				{
					if (this.percentMabu != 100)
					{
						int w = (int)this.percentMabu * mGraphics.getImageWidth(GameScr.imgHPLost) / 100;
						sbyte b = this.percentMabu;
						g.drawImage(GameScr.imgHPLost, TileMap.pxw / 2 - mGraphics.getImageWidth(GameScr.imgHPLost) / 2, 220, 0);
						g.setClip(TileMap.pxw / 2 - mGraphics.getImageWidth(GameScr.imgHPLost) / 2, 220, w, 10);
						g.drawImage(GameScr.imgHP, TileMap.pxw / 2 - mGraphics.getImageWidth(GameScr.imgHPLost) / 2, 220, 0);
						g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
					}
					if (this.mabuEff)
					{
						this.tMabuEff++;
						if (GameCanvas.gameTick % 3 == 0)
						{
							EffecMn.addEff(new Effect(19, Res.random(TileMap.pxw / 2 - 50, TileMap.pxw / 2 + 50), 340, 2, 1, -1));
						}
						if (GameCanvas.gameTick % 15 == 0)
						{
							EffecMn.addEff(new Effect(18, Res.random(TileMap.pxw / 2 - 5, TileMap.pxw / 2 + 5), Res.random(300, 320), 2, 1, -1));
						}
						if (this.tMabuEff == 100)
						{
							this.activeSuperPower(TileMap.pxw / 2, 300);
						}
						if (this.tMabuEff == 110)
						{
							this.tMabuEff = 0;
							this.mabuEff = false;
						}
					}
				}
				BackgroudEffect.paintFog(g);
				bool flag = true;
				for (int num9 = 0; num9 < BackgroudEffect.vBgEffect.size(); num9++)
				{
					if (((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(num9)).typeEff == 0)
					{
						flag = false;
						break;
					}
				}
				if (mGraphics.zoomLevel <= 1 || Main.isIpod || Main.isIphone4)
				{
					flag = false;
				}
				if (flag && !this.isRongThanXuatHien)
				{
					int num10 = TileMap.pxw / (mGraphics.getImageWidth(TileMap.imgLight) + 50);
					if (num10 <= 0)
					{
						num10 = 1;
					}
					if (TileMap.tileID != 28)
					{
						for (int num11 = 0; num11 < num10; num11++)
						{
							int num12 = 100 + num11 * (mGraphics.getImageWidth(TileMap.imgLight) + 50) - GameScr.cmx / 2;
							int num13 = -20;
							int imageWidth = mGraphics.getImageWidth(TileMap.imgLight);
							if (num12 + imageWidth >= GameScr.cmx && num12 <= GameScr.cmx + GameCanvas.w && num13 + mGraphics.getImageHeight(TileMap.imgLight) >= GameScr.cmy && num13 <= GameScr.cmy + GameCanvas.h)
							{
								g.drawImage(TileMap.imgLight, 100 + num11 * (mGraphics.getImageWidth(TileMap.imgLight) + 50) - GameScr.cmx / 2, num13, 0);
							}
						}
					}
				}
			}
			for (int num14 = 0; num14 < GameScr.vCharInMap.size(); num14++)
			{
				global::Char char5 = null;
				try
				{
					char5 = (global::Char)GameScr.vCharInMap.elementAt(num14);
				}
				catch (Exception)
				{
				}
				if (char5 != null && char5.chatInfo != null)
				{
					char5.chatInfo.paint(g, char5.cx, char5.cy - char5.ch, char5.cdir);
				}
			}
			if (global::Char.myCharz().chatInfo != null)
			{
				global::Char.myCharz().chatInfo.paint(g, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, global::Char.myCharz().cdir);
			}
			mSystem.paintFlyText(g);
			GameCanvas.debug("PA14", 1);
			GameCanvas.debug("PA15", 1);
			GameCanvas.debug("PA16", 1);
			this.paintArrowPointToNPC(g);
			GameCanvas.debug("PA17", 1);
			if (!GameScr.isPaintOther && GameScr.isPaintRada == 1 && !GameCanvas.panel.isShow)
			{
				this.paintInfoBar(g);
			}
			GameScr.resetTranslate(g);
			g.drawImage(GameScr.logo, GameCanvas.w / 3, 1, 0);
			VuDang.Paint(g);
			if (!GameScr.isPaintOther)
			{
				if (GameCanvas.open3Hour && !VuDang.giamDungLuong)
				{
					if (GameCanvas.w > 250)
					{
						g.drawImage(GameCanvas.img12, 160, 6, 0);
						mFont.tahoma_7_white.drawString(g, "Dành cho người chơi trên 12 tuổi.", 180, 2, 0);
						mFont.tahoma_7_white.drawString(g, "Chơi quá 180 phút mỗi ngày ", 180, 12, 0);
						mFont.tahoma_7_white.drawString(g, "sẽ hại sức khỏe.", 180, 22, 0);
					}
					else
					{
						g.drawImage(GameCanvas.img12, 5, GameCanvas.h - 67, 0);
						mFont.tahoma_7_white.drawString(g, "Dành cho người chơi trên 12 tuổi.", 25, GameCanvas.h - 70, 0);
						mFont.tahoma_7_white.drawString(g, "Chơi quá 180 phút mỗi ngày sẽ hại sức khỏe.", 25, GameCanvas.h - 60, 0);
					}
				}
				GameCanvas.debug("PA21", 1);
				GameCanvas.debug("PA18", 1);
				if (!VuDang.giamDungLuong)
				{
					g.translate(-g.getTranslateX(), -g.getTranslateY());
					if ((TileMap.mapID == 128 || TileMap.mapID == 127) && GameScr.mabuPercent != 0)
					{
						int num15 = 30;
						int num16 = 200;
						g.setColor(0);
						g.fillRect(num15 - 27, num16 - 112, 54, 8);
						g.setColor(16711680);
						g.setClip(num15 - 25, num16 - 110, (int)GameScr.mabuPercent, 4);
						g.fillRect(num15 - 25, num16 - 110, 50, 4);
						g.setClip(0, 0, 3000, 3000);
						mFont.tahoma_7b_white.drawString(g, "Mabu", num15, num16 - 112 + 10, 2, mFont.tahoma_7b_dark);
					}
				}
				if (global::Char.myCharz().isFusion)
				{
					global::Char.myCharz().tFusion++;
					if (GameCanvas.gameTick % 3 == 0)
					{
						g.setColor(16777215);
						g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					}
					if (global::Char.myCharz().tFusion >= 100)
					{
						global::Char.myCharz().fusionComplete();
					}
				}
				for (int num17 = 0; num17 < GameScr.vCharInMap.size(); num17++)
				{
					global::Char char6 = null;
					try
					{
						char6 = (global::Char)GameScr.vCharInMap.elementAt(num17);
					}
					catch (Exception)
					{
					}
					if (char6 != null && char6.isFusion && global::Char.isCharInScreen(char6))
					{
						char6.tFusion++;
						if (GameCanvas.gameTick % 3 == 0)
						{
							g.setColor(16777215);
							g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
						}
						if (char6.tFusion >= 100)
						{
							char6.fusionComplete();
						}
					}
				}
				GameCanvas.paintz.paintTabSoft(g);
				GameCanvas.debug("PA19", 1);
				GameCanvas.debug("PA20", 1);
				GameScr.resetTranslate(g);
				this.paintSelectedSkill(g);
				GameCanvas.debug("PA22", 1);
				GameScr.resetTranslate(g);
				if (GameCanvas.isTouch && GameCanvas.isTouchControl)
				{
					this.paintTouchControl(g);
				}
				GameScr.resetTranslate(g);
				this.paintChatVip(g);
				if (!GameCanvas.panel.isShow && GameCanvas.currentDialog == null && ChatPopup.currChatPopup == null && ChatPopup.serverChatPopUp == null && GameCanvas.currentScreen.Equals(GameScr.instance))
				{
					base.paint(g);
					if (mScreen.keyMouse == 1 && this.cmdMenu != null)
					{
						g.drawImage(ItemMap.imageFlare, this.cmdMenu.x + 7, this.cmdMenu.y + 15, 3);
					}
				}
				GameScr.resetTranslate(g);
				int num18 = 100 + ((global::Char.vItemTime.size() != 0) ? (GameScr.textTime.size() * 12) : 0);
				if (global::Char.myCharz().clan != null)
				{
					int num19 = 0;
					int num20 = 0;
					int num21 = (GameCanvas.h - 100 - 60) / 12;
					for (int num22 = 0; num22 < GameScr.vCharInMap.size(); num22++)
					{
						global::Char char7 = (global::Char)GameScr.vCharInMap.elementAt(num22);
						if (char7.clanID != -1 && char7.clanID == global::Char.myCharz().clan.ID)
						{
							if (char7.isOutX() && char7.cx < global::Char.myCharz().cx)
							{
								int num23 = num21;
								if (global::Char.vItemTime.size() != 0)
								{
									num23 -= GameScr.textTime.size();
								}
								if (num19 <= num23)
								{
									mFont.tahoma_7_green.drawString(g, char7.cName, 20, num18 - 12 + num19 * 12, mFont.LEFT, mFont.tahoma_7_grey);
									char7.paintHp(g, 10, num18 + num19 * 12 - 5);
									num19++;
								}
							}
							else if (char7.isOutX() && char7.cx > global::Char.myCharz().cx && num20 <= num21)
							{
								mFont.tahoma_7_green.drawString(g, char7.cName, GameCanvas.w - 25, num18 - 12 + num20 * 12, mFont.RIGHT, mFont.tahoma_7_grey);
								char7.paintHp(g, GameCanvas.w - 15, num18 + num20 * 12 - 5);
								num20++;
							}
						}
					}
				}
				ChatTextField.gI().paint(g);
				if (GameScr.isNewClanMessage && !GameCanvas.panel.isShow && GameCanvas.gameTick % 4 == 0)
				{
					g.drawImage(ItemMap.imageFlare, this.cmdMenu.x + 15, this.cmdMenu.y + 30, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				if (this.isSuperPower)
				{
					this.dxPower += 5;
					if (this.tPower >= 0)
					{
						this.tPower += this.dxPower;
					}
					Res.outz("x power= " + this.xPower);
					if (this.tPower < 0)
					{
						this.tPower--;
						if (this.tPower == -20)
						{
							this.isSuperPower = false;
							this.tPower = 0;
							this.dxPower = 0;
						}
					}
					else if ((this.xPower - this.tPower > 0 || this.tPower < TileMap.pxw) && this.tPower > 0)
					{
						g.setColor(16777215);
						if (!GameCanvas.lowGraphic)
						{
							g.fillArg(0, 0, GameCanvas.w, GameCanvas.h, 0, 0);
						}
						else
						{
							g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
						}
					}
					else
					{
						this.tPower = -1;
					}
				}
				for (int num24 = 0; num24 < global::Char.vItemTime.size(); num24++)
				{
					((ItemTime)global::Char.vItemTime.elementAt(num24)).paint(g, this.cmdMenu.x + 32 + num24 * 24, 55);
				}
				for (int num25 = 0; num25 < GameScr.textTime.size(); num25++)
				{
					((ItemTime)GameScr.textTime.elementAt(num25)).paintText(g, this.cmdMenu.x + ((global::Char.vItemTime.size() == 0) ? 25 : 5), ((global::Char.vItemTime.size() == 0) ? 45 : 90) + num25 * 12);
				}
				this.paintXoSo(g);
				if (mResources.language == 1)
				{
					long second = mSystem.currentTimeMillis() + GameScr.deltaTime;
					mFont.tahoma_7b_white.drawString(g, NinjaUtil.getDate2(second), 10, GameCanvas.h - 65, 0, mFont.tahoma_7b_dark);
				}
				if (!this.yourNumber.Equals(string.Empty))
				{
					for (int num26 = 0; num26 < this.strPaint.Length; num26++)
					{
						mFont.tahoma_7b_white.drawString(g, this.strPaint[num26], 5, 85 + num26 * 18, 0, mFont.tahoma_7b_dark);
					}
				}
			}
			int num27 = 0;
			int num28 = GameCanvas.hw;
			if (num28 > 200)
			{
				num28 = 200;
			}
			this.paintPhuBanBar(g, num27 + GameCanvas.w / 2, 0, num28);
			EffectManager.hiEffects.paintAll(g);
		}
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00048C20 File Offset: 0x00046E20
	private void paintXoSo(mGraphics g)
	{
		if (this.tShow != 0)
		{
			string text = string.Empty;
			for (int i = 0; i < this.winnumber.Length; i++)
			{
				text = text + this.randomNumber[i] + " ";
			}
			PopUp.paintPopUp(g, 20, 45, 95, 35, 16777215, false);
			mFont.tahoma_7b_dark.drawString(g, mResources.kquaVongQuay, 68, 50, 2);
			mFont.tahoma_7b_dark.drawString(g, text + string.Empty, 68, 65, 2);
		}
	}

	// Token: 0x0600030D RID: 781 RVA: 0x00048CAC File Offset: 0x00046EAC
	private void checkEffToObj(IMapObject obj)
	{
		if (obj != null && this.tDoubleDelay <= 0)
		{
			this.tDoubleDelay = 10;
			int x = obj.getX();
			int num = Res.abs(global::Char.myCharz().cx - x);
			int num2 = (num <= 80) ? 1 : ((num > 80 && num <= 200) ? 2 : ((num <= 200 || num > 400) ? 4 : 3));
			Res.outz("nLoop= " + num2);
			if (obj.Equals(global::Char.myCharz().mobFocus) || (obj.Equals(global::Char.myCharz().charFocus) && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus)))
			{
				ServerEffect.addServerEffect(135, obj.getX(), obj.getY(), num2);
				return;
			}
			if (obj.Equals(global::Char.myCharz().npcFocus) || obj.Equals(global::Char.myCharz().itemFocus) || obj.Equals(global::Char.myCharz().charFocus))
			{
				ServerEffect.addServerEffect(136, obj.getX(), obj.getY(), num2);
			}
		}
	}

	// Token: 0x0600030E RID: 782 RVA: 0x00048DD0 File Offset: 0x00046FD0
	private void updateClickToArrow()
	{
		if (this.tDoubleDelay > 0)
		{
			this.tDoubleDelay--;
		}
		if (this.clickMoving)
		{
			this.clickMoving = false;
			IMapObject mapObject = this.findClickToItem(this.clickToX, this.clickToY);
			if (mapObject == null || (mapObject != null && mapObject.Equals(global::Char.myCharz().npcFocus) && TileMap.mapID == 51))
			{
				ServerEffect.addServerEffect(134, this.clickToX, this.clickToY + GameCanvas.transY / 2, 3);
			}
		}
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00048E58 File Offset: 0x00047058
	private void paintWaypointArrow(mGraphics g)
	{
		int num = 10;
		Task taskMaint = global::Char.myCharz().taskMaint;
		if (taskMaint == null || taskMaint.taskId != 0 || ((taskMaint.index == 1 || taskMaint.index >= 6) && taskMaint.index != 0))
		{
			for (int i = 0; i < TileMap.vGo.size(); i++)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
				if (waypoint.minY == 0 || (int)waypoint.maxY >= TileMap.pxh - 24)
				{
					if ((int)waypoint.maxY <= TileMap.pxh / 2)
					{
						int x = (int)(waypoint.minX + (waypoint.maxX - waypoint.minX) / 2);
						int y = (int)(waypoint.minY + (waypoint.maxY - waypoint.minY) / 2) + this.runArrow;
						if (GameCanvas.isTouch)
						{
							y = (int)(waypoint.maxY + (waypoint.maxY - waypoint.minY)) + this.runArrow + num;
						}
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 6, x, y, StaticObj.VCENTER_HCENTER);
					}
					else if ((int)waypoint.minY >= TileMap.pxh / 2)
					{
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, (int)(waypoint.minX + (waypoint.maxX - waypoint.minX) / 2), (int)(waypoint.minY - 12) - this.runArrow, StaticObj.VCENTER_HCENTER);
					}
				}
				else if (waypoint.minX >= 0 && waypoint.minX < 24)
				{
					if (!GameCanvas.isTouch)
					{
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 2, (int)(waypoint.maxX + 12) + this.runArrow, (int)(waypoint.maxY - 12), StaticObj.VCENTER_HCENTER);
					}
					else
					{
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 2, (int)(waypoint.maxX + 12) + this.runArrow, (int)(waypoint.maxY - 32), StaticObj.VCENTER_HCENTER);
					}
				}
				else if ((int)waypoint.minX <= TileMap.tmw * 24 && (int)waypoint.minX >= TileMap.tmw * 24 - 48)
				{
					if (!GameCanvas.isTouch)
					{
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 0, (int)(waypoint.minX - 12) - this.runArrow, (int)(waypoint.maxY - 12), StaticObj.VCENTER_HCENTER);
					}
					else
					{
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 0, (int)(waypoint.minX - 12) - this.runArrow, (int)(waypoint.maxY - 32), StaticObj.VCENTER_HCENTER);
					}
				}
				else
				{
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, (int)(waypoint.minX + (waypoint.maxX - waypoint.minX) / 2), (int)(waypoint.maxY - 48) - this.runArrow, StaticObj.VCENTER_HCENTER);
				}
			}
		}
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00049114 File Offset: 0x00047314
	public static Npc findNPCInMap(short id)
	{
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			if (npc.template.npcTemplateId == (int)id)
			{
				return npc;
			}
		}
		return null;
	}

	// Token: 0x06000311 RID: 785 RVA: 0x00049158 File Offset: 0x00047358
	public static global::Char findCharInMap(int charId)
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char.charID == charId)
			{
				return @char;
			}
		}
		return null;
	}

	// Token: 0x06000312 RID: 786 RVA: 0x000045AE File Offset: 0x000027AE
	public static Mob findMobInMap(sbyte mobIndex)
	{
		return (Mob)GameScr.vMob.elementAt((int)mobIndex);
	}

	// Token: 0x06000313 RID: 787 RVA: 0x00049198 File Offset: 0x00047398
	public static Mob findMobInMap(int mobId)
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob.mobId == mobId)
			{
				return mob;
			}
		}
		return null;
	}

	// Token: 0x06000314 RID: 788 RVA: 0x000491D8 File Offset: 0x000473D8
	public static Npc getNpcTask()
	{
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			if (npc.template.npcTemplateId == (int)GameScr.getTaskNpcId())
			{
				return npc;
			}
		}
		return null;
	}

	// Token: 0x06000315 RID: 789 RVA: 0x00049220 File Offset: 0x00047420
	private void paintArrowPointToNPC(mGraphics g)
	{
		try
		{
			if (ChatPopup.currChatPopup == null)
			{
				int taskNpcId = (int)GameScr.getTaskNpcId();
				if (taskNpcId != -1)
				{
					Npc npc = null;
					for (int i = 0; i < GameScr.vNpc.size(); i++)
					{
						Npc npc2 = (Npc)GameScr.vNpc.elementAt(i);
						if (npc2.template.npcTemplateId == taskNpcId)
						{
							if (npc == null)
							{
								npc = npc2;
							}
							else if (Res.abs(npc2.cx - global::Char.myCharz().cx) < Res.abs(npc.cx - global::Char.myCharz().cx))
							{
								npc = npc2;
							}
						}
					}
					if (npc != null && npc.statusMe != 15 && (npc.cx <= GameScr.cmx || npc.cx >= GameScr.cmx + GameScr.gW || npc.cy <= GameScr.cmy || npc.cy >= GameScr.cmy + GameScr.gH) && GameCanvas.gameTick % 10 >= 5)
					{
						int num = npc.cx - global::Char.myCharz().cx;
						int num2 = npc.cy - global::Char.myCharz().cy;
						int x = 0;
						int y = 0;
						int arg = 0;
						if (num > 0 && num2 >= 0)
						{
							if (Res.abs(num) >= Res.abs(num2))
							{
								x = GameScr.gW - 10;
								y = GameScr.gH / 2 + 30;
								if (GameCanvas.isTouch)
								{
									y = GameScr.gH / 2 + 10;
								}
								arg = 0;
							}
							else
							{
								x = GameScr.gW / 2;
								y = GameScr.gH - 10;
								arg = 5;
							}
						}
						else if (num >= 0 && num2 < 0)
						{
							if (Res.abs(num) >= Res.abs(num2))
							{
								x = GameScr.gW - 10;
								y = GameScr.gH / 2 + 30;
								if (GameCanvas.isTouch)
								{
									y = GameScr.gH / 2 + 10;
								}
								arg = 0;
							}
							else
							{
								x = GameScr.gW / 2;
								y = 10;
								arg = 6;
							}
						}
						if (num < 0 && num2 >= 0)
						{
							if (Res.abs(num) >= Res.abs(num2))
							{
								x = 10;
								y = GameScr.gH / 2 + 30;
								if (GameCanvas.isTouch)
								{
									y = GameScr.gH / 2 + 10;
								}
								arg = 3;
							}
							else
							{
								x = GameScr.gW / 2;
								y = GameScr.gH - 10;
								arg = 5;
							}
						}
						else if (num <= 0 && num2 < 0)
						{
							if (Res.abs(num) >= Res.abs(num2))
							{
								x = 10;
								y = GameScr.gH / 2 + 30;
								if (GameCanvas.isTouch)
								{
									y = GameScr.gH / 2 + 10;
								}
								arg = 3;
							}
							else
							{
								x = GameScr.gW / 2;
								y = 10;
								arg = 6;
							}
						}
						GameScr.resetTranslate(g);
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, arg, x, y, StaticObj.VCENTER_HCENTER);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham arrow to npc: " + ex.ToString());
		}
	}

	// Token: 0x06000316 RID: 790 RVA: 0x000045C0 File Offset: 0x000027C0
	public static void resetTranslate(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, -200, GameCanvas.w, 200 + GameCanvas.h);
	}

	// Token: 0x06000317 RID: 791 RVA: 0x00049504 File Offset: 0x00047704
	private void paintTouchControl(mGraphics g)
	{
		if (!this.isNotPaintTouchControl())
		{
			GameScr.resetTranslate(g);
			if (!TileMap.isOfflineMap() && !this.isVS())
			{
				if (mScreen.keyTouch == 15 || mScreen.keyMouse == 15)
				{
					g.drawImage((!Main.isPC) ? GameScr.imgChat2 : GameScr.imgChatsPC2, GameScr.xC + 17, GameScr.yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
				}
				else
				{
					g.drawImage((!Main.isPC) ? GameScr.imgChat : GameScr.imgChatPC, GameScr.xC + 17, GameScr.yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
				}
			}
			bool flag = GameScr.isUseTouch;
		}
	}

	// Token: 0x06000318 RID: 792 RVA: 0x000495C8 File Offset: 0x000477C8
	public void paintImageBarRight(mGraphics g, global::Char c)
	{
		int num = c.cHP * GameScr.hpBarW / c.cHPFull;
		int num2 = c.cMP * GameScr.mpBarW;
		int num3 = this.dHP * GameScr.hpBarW / c.cHPFull;
		int num4 = this.dMP * GameScr.mpBarW;
		g.setClip(GameCanvas.w / 2 + 58 - mGraphics.getImageWidth(GameScr.imgPanel), 0, 95, 100);
		g.drawRegion(GameScr.imgPanel, 0, 0, mGraphics.getImageWidth(GameScr.imgPanel), mGraphics.getImageHeight(GameScr.imgPanel), 2, GameCanvas.w / 2 + 60, 0, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(GameCanvas.w / 2 + 60 - 83 - GameScr.hpBarW + GameScr.hpBarW - num3, 5, num3, 10);
		g.drawImage(GameScr.imgHPLost, GameCanvas.w / 2 + 60 - 83, 5, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.setClip(GameCanvas.w / 2 + 60 - 83 - GameScr.hpBarW + GameScr.hpBarW - num, 5, num, 10);
		g.drawImage(GameScr.imgHP, GameCanvas.w / 2 + 60 - 83, 5, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.setClip(GameCanvas.w / 2 + 60 - 83 - GameScr.mpBarW + GameScr.hpBarW - num4, 20, num4, 6);
		g.drawImage(GameScr.imgMPLost, GameCanvas.w / 2 + 60 - 83, 20, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.setClip(GameCanvas.w / 2 + 60 - 83 - GameScr.mpBarW + GameScr.hpBarW - num2, 20, num2, 6);
		g.drawImage(GameScr.imgMP, GameCanvas.w / 2 + 60 - 83, 20, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x06000319 RID: 793 RVA: 0x000497E0 File Offset: 0x000479E0
	private void paintImageBar(mGraphics g, bool isLeft, global::Char c)
	{
		if (c != null)
		{
			int num;
			int num2;
			int num3;
			int num4;
			if (c.charID == global::Char.myCharz().charID)
			{
				num = this.dHP * GameScr.hpBarW / c.cHPFull;
				num2 = this.dMP * GameScr.mpBarW / c.cMPFull;
				num3 = c.cHP * GameScr.hpBarW / c.cHPFull;
				num4 = c.cMP * GameScr.mpBarW / c.cMPFull;
			}
			else
			{
				num = c.dHP * GameScr.hpBarW / c.cHPFull;
				num2 = c.perCentMp * GameScr.mpBarW / 100;
				num3 = c.cHP * GameScr.hpBarW / c.cHPFull;
				num4 = c.perCentMp * GameScr.mpBarW / 100;
			}
			if (global::Char.myCharz().secondPower > 0)
			{
				int w = (int)global::Char.myCharz().powerPoint * GameScr.spBarW / (int)global::Char.myCharz().maxPowerPoint;
				g.drawImage(GameScr.imgPanel2, 58, 29, 0);
				g.setClip(83, 31, w, 10);
				g.drawImage(GameScr.imgSP, 83, 31, 0);
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				mFont.tahoma_7_white.drawString(g, string.Concat(new object[]
				{
					global::Char.myCharz().strInfo,
					":",
					global::Char.myCharz().powerPoint,
					"/",
					global::Char.myCharz().maxPowerPoint
				}), 115, 29, 2);
			}
			if (c.charID != global::Char.myCharz().charID)
			{
				g.setClip(mGraphics.getImageWidth(GameScr.imgPanel) - 95, 0, 95, 100);
			}
			g.drawImage(GameScr.imgPanel, 0, 0, 0);
			if (isLeft)
			{
				g.setClip(83, 5, num, 10);
			}
			else
			{
				g.setClip(83 + GameScr.hpBarW - num, 5, num, 10);
			}
			g.drawImage(GameScr.imgHPLost, 83, 5, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (isLeft)
			{
				g.setClip(83, 5, num3, 10);
			}
			else
			{
				g.setClip(83 + GameScr.hpBarW - num3, 5, num3, 10);
			}
			g.drawImage(GameScr.imgHP, 83, 5, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (isLeft)
			{
				g.setClip(83, 20, num2, 6);
			}
			else
			{
				g.setClip(83 + GameScr.mpBarW - num2, 20, num2, 6);
			}
			g.drawImage(GameScr.imgMPLost, 83, 20, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (isLeft)
			{
				g.setClip(83, 20, num2, 6);
			}
			else
			{
				g.setClip(83 + GameScr.mpBarW - num4, 20, num4, 6);
			}
			g.drawImage(GameScr.imgMP, 83, 20, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (global::Char.myCharz().cMP == 0 && GameCanvas.gameTick % 10 > 5)
			{
				g.setClip(83, 20, 2, 6);
				g.drawImage(GameScr.imgMPLost, 83, 20, 0);
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			}
		}
	}

	// Token: 0x0600031A RID: 794 RVA: 0x00004426 File Offset: 0x00002626
	public void getInjure()
	{
	}

	// Token: 0x0600031B RID: 795 RVA: 0x00049B00 File Offset: 0x00047D00
	public void starVS()
	{
		this.curr = (this.last = mSystem.currentTimeMillis());
		this.secondVS = 180;
	}

	// Token: 0x0600031C RID: 796 RVA: 0x00049B2C File Offset: 0x00047D2C
	private global::Char findCharVS1()
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char.cTypePk != 0)
			{
				return @char;
			}
		}
		return null;
	}

	// Token: 0x0600031D RID: 797 RVA: 0x00049B6C File Offset: 0x00047D6C
	private global::Char findCharVS2()
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char.cTypePk != 0 && @char != this.findCharVS1())
			{
				return @char;
			}
		}
		return null;
	}

	// Token: 0x0600031E RID: 798 RVA: 0x00049BB4 File Offset: 0x00047DB4
	private void paintInfoBar(mGraphics g)
	{
		GameScr.resetTranslate(g);
		if (TileMap.mapID == 130 && this.findCharVS1() != null && this.findCharVS2() != null)
		{
			g.translate(GameCanvas.w / 2 - 62, 0);
			this.paintImageBar(g, true, this.findCharVS1());
			g.translate(-(GameCanvas.w / 2 - 65), 0);
			this.paintImageBarRight(g, this.findCharVS2());
			this.findCharVS1().paintHeadWithXY(g, 15, 20, 0);
			this.findCharVS2().paintHeadWithXY(g, GameCanvas.w - 15, 20, 2);
		}
		else if (this.isVS() && global::Char.myCharz().charFocus != null)
		{
			g.translate(GameCanvas.w / 2 - 62, 0);
			this.paintImageBar(g, true, global::Char.myCharz().charFocus);
			g.translate(-(GameCanvas.w / 2 - 65), 0);
			this.paintImageBarRight(g, global::Char.myCharz());
			global::Char.myCharz().paintHeadWithXY(g, 15, 20, 0);
			global::Char.myCharz().charFocus.paintHeadWithXY(g, GameCanvas.w - 15, 20, 2);
		}
		else if (GameScr.ispaintPhubangBar() && GameScr.isSmallScr())
		{
			GameScr.paintHPBar_NEW(g, 1, 1, global::Char.myCharz());
		}
		else
		{
			this.paintImageBar(g, true, global::Char.myCharz());
			if (global::Char.myCharz().isInEnterOfflinePoint() != null || global::Char.myCharz().isInEnterOnlinePoint() != null)
			{
				mFont.tahoma_7_green2.drawString(g, mResources.enter, this.imgScrW / 2, 8 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
			}
			else if (global::Char.myCharz().mobFocus != null)
			{
				if (global::Char.myCharz().mobFocus.getTemplate() != null)
				{
					mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().mobFocus.getTemplate().name, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				}
				if (global::Char.myCharz().mobFocus.templateId != 0)
				{
					mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys((long)global::Char.myCharz().mobFocus.hp) + string.Empty, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				}
			}
			else if (global::Char.myCharz().npcFocus != null)
			{
				mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().npcFocus.template.name, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				if (global::Char.myCharz().npcFocus.template.npcTemplateId == 4)
				{
					mFont.tahoma_7b_green2.drawString(g, GameScr.gI().magicTree.currPeas + "/" + GameScr.gI().magicTree.maxPeas, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				}
			}
			else if (global::Char.myCharz().charFocus != null)
			{
				mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().charFocus.cName, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys((long)global::Char.myCharz().charFocus.cHP) + string.Empty, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
			}
			else
			{
				mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().cName, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys(global::Char.myCharz().cPower) + string.Empty, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
			}
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		if (this.isVS() && this.secondVS > 0)
		{
			this.curr = mSystem.currentTimeMillis();
			if (this.curr - this.last >= 1000L)
			{
				this.last = mSystem.currentTimeMillis();
				this.secondVS--;
			}
			mFont.tahoma_7b_white.drawString(g, this.secondVS + string.Empty, GameCanvas.w / 2, 13, 2, mFont.tahoma_7b_dark);
		}
		if (this.flareFindFocus)
		{
			g.drawImage(ItemMap.imageFlare, 40, 35, mGraphics.BOTTOM | mGraphics.HCENTER);
			this.flareTime--;
			if (this.flareTime < 0)
			{
				this.flareTime = 0;
				this.flareFindFocus = false;
			}
		}
	}

	// Token: 0x0600031F RID: 799 RVA: 0x000045F2 File Offset: 0x000027F2
	public bool isVS()
	{
		return TileMap.isVoDaiMap() && (global::Char.myCharz().cTypePk != 0 || (TileMap.mapID == 130 && this.findCharVS1() != null && this.findCharVS2() != null));
	}

	// Token: 0x06000320 RID: 800 RVA: 0x0004A054 File Offset: 0x00048254
	private void paintSelectedSkill(mGraphics g)
	{
		if (this.mobCapcha != null)
		{
			this.paintCapcha(g);
			return;
		}
		if (GameCanvas.currentDialog == null && ChatPopup.currChatPopup == null && !GameCanvas.menu.showMenu && !this.isPaintPopup() && !GameCanvas.panel.isShow && global::Char.myCharz().taskMaint.taskId != 0 && !ChatTextField.gI().isShow && GameCanvas.currentScreen != MoneyCharge.instance)
		{
			long num = mSystem.currentTimeMillis() - this.lastUsePotion;
			int num2 = 0;
			if (num < 10000L)
			{
				num2 = (int)(num * 20L / 10000L);
			}
			if (!GameCanvas.isTouch)
			{
				g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgSkill : GameScr.imgSkill2, GameScr.xSkill + GameScr.xHP - 1, GameScr.yHP - 1, 0);
				SmallImage.drawSmallImage(g, 542, GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3, 0, 0);
				mFont.number_gray.drawString(g, string.Empty + GameScr.hpPotion, GameScr.xSkill + GameScr.xHP + 22, GameScr.yHP + 15, 1);
				if (num < 10000L)
				{
					g.setColor(2721889);
					num2 = (int)(num * 20L / 10000L);
					g.fillRect(GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3 + num2, 20, 20 - num2);
				}
			}
			else if (global::Char.myCharz().statusMe != 14)
			{
				if (GameScr.gamePad.isSmallGamePad)
				{
					if (GameScr.isAnalog != 1)
					{
						g.setColor(9670800);
						g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10, 22, 20);
						g.setColor(16777215);
						g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 + ((num2 != 0) ? (20 - num2) : 0), 22, (num2 == 0) ? 20 : num2);
						g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgHP1 : GameScr.imgHP2, GameScr.xHP, GameScr.yHP, 0);
						mFont.tahoma_7_red.drawString(g, string.Empty + GameScr.hpPotion, GameScr.xHP + 20, GameScr.yHP + 15, 2);
					}
					else if (GameScr.isAnalog == 1)
					{
						g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgSkill : GameScr.imgSkill2, GameScr.xSkill + GameScr.xHP - 1, GameScr.yHP - 1, 0);
						SmallImage.drawSmallImage(g, 542, GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3, 0, 0);
						mFont.number_gray.drawString(g, string.Empty + GameScr.hpPotion, GameScr.xSkill + GameScr.xHP + 22, GameScr.yHP + 13, 1);
						if (num < 10000L)
						{
							g.setColor(2721889);
							num2 = (int)(num * 20L / 10000L);
							g.fillRect(GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3 + num2, 20, 20 - num2);
						}
					}
				}
				else if (!GameScr.gamePad.isSmallGamePad)
				{
					if (GameScr.isAnalog != 1)
					{
						g.setColor(9670800);
						g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10, 22, 20);
						g.setColor(16777215);
						g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 + ((num2 != 0) ? (20 - num2) : 0), 22, (num2 == 0) ? 20 : num2);
						g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgHP1 : GameScr.imgHP2, GameScr.xHP, GameScr.yHP, 0);
						mFont.tahoma_7_red.drawString(g, string.Empty + GameScr.hpPotion, GameScr.xHP + 20, GameScr.yHP + 15, 2);
					}
					else
					{
						g.setColor(9670800);
						g.fillRect(GameScr.xHP + 10, GameScr.yHP + 10, 20, 18);
						g.setColor(16777215);
						g.fillRect(GameScr.xHP + 10, GameScr.yHP + 10 + ((num2 != 0) ? (20 - num2) : 0), 20, (num2 == 0) ? 18 : num2);
						g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgHP3 : GameScr.imgHP4, GameScr.xHP + 20, GameScr.yHP + 20, mGraphics.HCENTER | mGraphics.VCENTER);
						mFont.tahoma_7_red.drawString(g, string.Empty + GameScr.hpPotion, GameScr.xHP + 20, GameScr.yHP + 15, 2);
					}
				}
			}
			if (GameScr.isHaveSelectSkill)
			{
				Skill[] array = Main.isPC ? GameScr.keySkill : GameScr.onScreenSkill;
				int keyTouch = mScreen.keyTouch;
				if (!GameCanvas.isTouch)
				{
					g.setColor(11152401);
					g.fillRect(GameScr.xSkill + GameScr.xHP + 2, GameScr.yHP - 10, 20, 10);
					mFont.tahoma_7_white.drawString(g, "*", GameScr.xSkill + GameScr.xHP + 12, GameScr.yHP - 8, mFont.CENTER);
				}
				int num3 = (!Main.isPC) ? this.nSkill : array.Length;
				for (int i = 0; i < num3; i++)
				{
					if (Main.isPC)
					{
						string[] array3;
						if (!TField.isQwerty)
						{
							string[] array2 = new string[5];
							array2[0] = "7";
							array2[1] = "8";
							array2[2] = "9";
							array2[3] = "10";
							array3 = array2;
							array2[4] = "11";
						}
						else
						{
							string[] array4 = new string[10];
							array4[0] = "1";
							array4[1] = "2";
							array4[2] = "3";
							array4[3] = "4";
							array4[4] = "5";
							array4[5] = "6";
							array4[6] = "7";
							array4[7] = "8";
							array4[8] = "9";
							array3 = array4;
							array4[9] = "0";
						}
						string[] array5 = array3;
						int num4 = -13;
						if (num3 > 5 && i < 5)
						{
							num4 = 27;
						}
						mFont.tahoma_7b_dark.drawString(g, array5[i], GameScr.xSkill + GameScr.xS[i] + 14, GameScr.yS[i] + num4, mFont.CENTER);
						mFont.tahoma_7b_white.drawString(g, array5[i], GameScr.xSkill + GameScr.xS[i] + 14, GameScr.yS[i] + num4 + 1, mFont.CENTER);
					}
					Skill skill = array[i];
					if (skill != global::Char.myCharz().myskill)
					{
						g.drawImage(GameScr.imgSkill, GameScr.xSkill + GameScr.xS[i] - 1, GameScr.yS[i] - 1, 0);
					}
					if (skill != null)
					{
						if (skill == global::Char.myCharz().myskill)
						{
							g.drawImage(GameScr.imgSkill2, GameScr.xSkill + GameScr.xS[i] - 1, GameScr.yS[i] - 1, 0);
							if (GameCanvas.isTouch && !Main.isPC)
							{
								g.drawRegion(Mob.imgHP, 0, 12, 9, 6, 0, GameScr.xSkill + GameScr.xS[i] + 8, GameScr.yS[i] - 7, 0);
							}
						}
						skill.paint(GameScr.xSkill + GameScr.xS[i] + 13, GameScr.yS[i] + 13, g);
						if ((i == this.selectedIndexSkill && !this.isPaintUI() && GameCanvas.gameTick % 10 > 5) || i == this.keyTouchSkill)
						{
							g.drawImage(ItemMap.imageFlare, GameScr.xSkill + GameScr.xS[i] + 13, GameScr.yS[i] + 14, 3);
						}
					}
				}
			}
			this.paintGamePad(g);
		}
	}

	// Token: 0x06000321 RID: 801 RVA: 0x0004A81C File Offset: 0x00048A1C
	public void paintOpen(mGraphics g)
	{
		if (this.isstarOpen)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.fillRect(0, 0, GameCanvas.w, this.moveUp);
			g.setColor(10275899);
			g.fillRect(0, this.moveUp - 1, GameCanvas.w, 1);
			g.fillRect(0, this.moveDow + 1, GameCanvas.w, 1);
		}
	}

	// Token: 0x06000322 RID: 802 RVA: 0x0004A890 File Offset: 0x00048A90
	public static void startFlyText(string flyString, int x, int y, int dx, int dy, int color)
	{
		int num = -1;
		for (int i = 0; i < 5; i++)
		{
			if (GameScr.flyTextState[i] == -1)
			{
				num = i;
				break;
			}
		}
		if (num != -1)
		{
			GameScr.flyTextColor[num] = color;
			GameScr.flyTextString[num] = flyString;
			GameScr.flyTextX[num] = x;
			GameScr.flyTextY[num] = y;
			GameScr.flyTextDx[num] = dx;
			GameScr.flyTextDy[num] = ((dy >= 0) ? 5 : -5);
			GameScr.flyTextState[num] = 0;
			GameScr.flyTime[num] = 0;
			GameScr.flyTextYTo[num] = 10;
			for (int j = 0; j < 5; j++)
			{
				if (GameScr.flyTextState[j] != -1 && num != j && GameScr.flyTextDy[num] < 0 && Res.abs(GameScr.flyTextX[num] - GameScr.flyTextX[j]) <= 20 && GameScr.flyTextYTo[num] == GameScr.flyTextYTo[j])
				{
					GameScr.flyTextYTo[num] += 10;
				}
			}
		}
	}

	// Token: 0x06000323 RID: 803 RVA: 0x0004A970 File Offset: 0x00048B70
	public static void updateFlyText()
	{
		for (int i = 0; i < 5; i++)
		{
			if (GameScr.flyTextState[i] != -1)
			{
				if (GameScr.flyTextState[i] > GameScr.flyTextYTo[i])
				{
					GameScr.flyTime[i]++;
					if (GameScr.flyTime[i] == 25)
					{
						GameScr.flyTime[i] = 0;
						GameScr.flyTextState[i] = -1;
						GameScr.flyTextYTo[i] = 0;
						GameScr.flyTextDx[i] = 0;
						GameScr.flyTextX[i] = 0;
					}
				}
				else
				{
					GameScr.flyTextState[i] += Res.abs(GameScr.flyTextDy[i]);
					GameScr.flyTextX[i] += GameScr.flyTextDx[i];
					GameScr.flyTextY[i] += GameScr.flyTextDy[i];
				}
			}
		}
	}

	// Token: 0x06000324 RID: 804 RVA: 0x0004AA38 File Offset: 0x00048C38
	public static void loadSplash()
	{
		if (GameScr.imgSplash == null)
		{
			GameScr.imgSplash = new Image[3];
			for (int i = 0; i < 3; i++)
			{
				GameScr.imgSplash[i] = GameCanvas.loadImage("/e/sp" + i + ".png");
			}
		}
		GameScr.splashX = new int[2];
		GameScr.splashY = new int[2];
		GameScr.splashState = new int[2];
		GameScr.splashF = new int[2];
		GameScr.splashDir = new int[2];
		GameScr.splashState[0] = (GameScr.splashState[1] = -1);
	}

	// Token: 0x06000325 RID: 805 RVA: 0x0004AAD0 File Offset: 0x00048CD0
	public static bool startSplash(int x, int y, int dir)
	{
		int num = (GameScr.splashState[0] != -1) ? 1 : 0;
		bool result;
		if (GameScr.splashState[num] != -1)
		{
			result = false;
		}
		else
		{
			GameScr.splashState[num] = 0;
			GameScr.splashDir[num] = dir;
			GameScr.splashX[num] = x;
			GameScr.splashY[num] = y;
			result = true;
		}
		return result;
	}

	// Token: 0x06000326 RID: 806 RVA: 0x0004AB20 File Offset: 0x00048D20
	public static void updateSplash()
	{
		for (int i = 0; i < 2; i++)
		{
			if (GameScr.splashState[i] != -1)
			{
				GameScr.splashState[i]++;
				GameScr.splashX[i] += GameScr.splashDir[i] << 2;
				GameScr.splashY[i]--;
				if (GameScr.splashState[i] >= 6)
				{
					GameScr.splashState[i] = -1;
				}
				else
				{
					GameScr.splashF[i] = (GameScr.splashState[i] >> 1) % 3;
				}
			}
		}
	}

	// Token: 0x06000327 RID: 807 RVA: 0x0004ABA4 File Offset: 0x00048DA4
	public static void paintSplash(mGraphics g)
	{
		for (int i = 0; i < 2; i++)
		{
			if (GameScr.splashState[i] != -1)
			{
				if (GameScr.splashDir[i] == 1)
				{
					g.drawImage(GameScr.imgSplash[GameScr.splashF[i]], GameScr.splashX[i], GameScr.splashY[i], 3);
				}
				else
				{
					g.drawRegion(GameScr.imgSplash[GameScr.splashF[i]], 0, 0, mGraphics.getImageWidth(GameScr.imgSplash[GameScr.splashF[i]]), mGraphics.getImageHeight(GameScr.imgSplash[GameScr.splashF[i]]), 2, GameScr.splashX[i], GameScr.splashY[i], 3);
				}
			}
		}
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0000462A File Offset: 0x0000282A
	private void loadInforBar()
	{
		this.imgScrW = 84;
		GameScr.hpBarW = 66;
		GameScr.mpBarW = 59;
		GameScr.hpBarX = 52;
		GameScr.hpBarY = 10;
		GameScr.spBarW = 61;
		GameScr.expBarW = GameScr.gW - 61;
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0004AC44 File Offset: 0x00048E44
	public void updateSS()
	{
		if (GameScr.indexMenu != -1)
		{
			if (GameScr.cmySK != GameScr.cmtoYSK)
			{
				GameScr.cmvySK = GameScr.cmtoYSK - GameScr.cmySK << 2;
				GameScr.cmdySK += GameScr.cmvySK;
				GameScr.cmySK += GameScr.cmdySK >> 4;
				GameScr.cmdySK &= 15;
			}
			if (global::Math.abs(GameScr.cmtoYSK - GameScr.cmySK) < 15 && GameScr.cmySK < 0)
			{
				GameScr.cmtoYSK = 0;
			}
			if (global::Math.abs(GameScr.cmtoYSK - GameScr.cmySK) < 15 && GameScr.cmySK > GameScr.cmyLimSK)
			{
				GameScr.cmtoYSK = GameScr.cmyLimSK;
			}
		}
	}

	// Token: 0x0600032A RID: 810 RVA: 0x0004ACF8 File Offset: 0x00048EF8
	public void updateKeyAlert()
	{
		if (GameScr.isPaintAlert && GameCanvas.currentDialog == null)
		{
			bool flag = false;
			if (GameCanvas.keyPressed[Key.NUM8])
			{
				GameScr.indexRow++;
				if (GameScr.indexRow >= this.texts.size())
				{
					GameScr.indexRow = 0;
				}
				flag = true;
			}
			else if (GameCanvas.keyPressed[Key.NUM2])
			{
				GameScr.indexRow--;
				if (GameScr.indexRow < 0)
				{
					GameScr.indexRow = this.texts.size() - 1;
				}
				flag = true;
			}
			if (flag)
			{
				GameScr.scrMain.moveTo(GameScr.indexRow * GameScr.scrMain.ITEM_SIZE);
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
			}
			if (GameCanvas.isTouch)
			{
				ScrollResult scrollResult = GameScr.scrMain.updateKey();
				if (scrollResult.isDowning || scrollResult.isFinish)
				{
					GameScr.indexRow = scrollResult.selected;
					flag = true;
				}
			}
			if (flag && GameScr.indexRow >= 0 && GameScr.indexRow < this.texts.size())
			{
				string text = (string)this.texts.elementAt(GameScr.indexRow);
				this.fnick = null;
				this.alertURL = null;
				this.center = null;
				ChatTextField.gI().center = null;
				int num;
				if ((num = text.IndexOf("http://")) >= 0)
				{
					Cout.println("currentLine: " + text);
					this.alertURL = text.Substring(num);
					this.center = new Command(mResources.open_link, 12000);
					if (!GameCanvas.isTouch)
					{
						ChatTextField.gI().center = new Command(mResources.open_link, null, 12000, null);
						return;
					}
				}
				else if (text.IndexOf("@") >= 0)
				{
					string text2 = text.Substring(2);
					text2 = text2.Trim();
					num = text2.IndexOf("@");
					string text3 = text2.Substring(num);
					int num2 = text3.IndexOf(" ");
					num2 = ((num2 > 0) ? (num2 + num) : (num + text3.Length));
					this.fnick = text2.Substring(num + 1, num2);
					if (!this.fnick.Equals(string.Empty) && !this.fnick.Equals(global::Char.myCharz().cName))
					{
						this.center = new Command(mResources.SELECT, 12009, this.fnick);
						if (!GameCanvas.isTouch)
						{
							ChatTextField.gI().center = new Command(mResources.SELECT, null, 12009, this.fnick);
							return;
						}
					}
					else
					{
						this.fnick = null;
						this.center = null;
					}
				}
			}
		}
	}

	// Token: 0x0600032B RID: 811 RVA: 0x0004AF8C File Offset: 0x0004918C
	public bool isPaintPopup()
	{
		return GameScr.isPaintItemInfo || GameScr.isPaintInfoMe || GameScr.isPaintStore || GameScr.isPaintWeapon || GameScr.isPaintNonNam || GameScr.isPaintNonNu || GameScr.isPaintAoNam || GameScr.isPaintAoNu || GameScr.isPaintGangTayNam || GameScr.isPaintGangTayNu || GameScr.isPaintQuanNam || GameScr.isPaintQuanNu || GameScr.isPaintGiayNam || GameScr.isPaintGiayNu || GameScr.isPaintLien || GameScr.isPaintNhan || GameScr.isPaintNgocBoi || GameScr.isPaintPhu || GameScr.isPaintStack || GameScr.isPaintStackLock || GameScr.isPaintGrocery || GameScr.isPaintGroceryLock || GameScr.isPaintUpGrade || GameScr.isPaintConvert || GameScr.isPaintSplit || GameScr.isPaintUpPearl || GameScr.isPaintBox || GameScr.isPaintTrade || GameScr.isPaintAlert || GameScr.isPaintZone || GameScr.isPaintTeam || GameScr.isPaintClan || GameScr.isPaintFindTeam || GameScr.isPaintTask || GameScr.isPaintFriend || GameScr.isPaintEnemies || GameScr.isPaintCharInMap || GameScr.isPaintMessage;
	}

	// Token: 0x0600032C RID: 812 RVA: 0x0004B0DC File Offset: 0x000492DC
	public bool isNotPaintTouchControl()
	{
		return (!GameCanvas.isTouchControl && GameCanvas.currentScreen == GameScr.gI()) || !GameCanvas.isTouch || ChatTextField.gI().isShow || InfoDlg.isShow || GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || GameCanvas.panel.isShow || this.isPaintPopup();
	}

	// Token: 0x0600032D RID: 813 RVA: 0x0004B144 File Offset: 0x00049344
	public bool isPaintUI()
	{
		return GameScr.isPaintStore || GameScr.isPaintWeapon || GameScr.isPaintNonNam || GameScr.isPaintNonNu || GameScr.isPaintAoNam || GameScr.isPaintAoNu || GameScr.isPaintGangTayNam || GameScr.isPaintGangTayNu || GameScr.isPaintQuanNam || GameScr.isPaintQuanNu || GameScr.isPaintGiayNam || GameScr.isPaintGiayNu || GameScr.isPaintLien || GameScr.isPaintNhan || GameScr.isPaintNgocBoi || GameScr.isPaintPhu || GameScr.isPaintStack || GameScr.isPaintStackLock || GameScr.isPaintGrocery || GameScr.isPaintGroceryLock || GameScr.isPaintUpGrade || GameScr.isPaintConvert || GameScr.isPaintSplit || GameScr.isPaintUpPearl || GameScr.isPaintBox || GameScr.isPaintTrade;
	}

	// Token: 0x0600032E RID: 814 RVA: 0x0004B21C File Offset: 0x0004941C
	public bool isOpenUI()
	{
		return GameScr.isPaintItemInfo || GameScr.isPaintInfoMe || GameScr.isPaintStore || GameScr.isPaintNonNam || GameScr.isPaintNonNu || GameScr.isPaintAoNam || GameScr.isPaintAoNu || GameScr.isPaintGangTayNam || GameScr.isPaintGangTayNu || GameScr.isPaintQuanNam || GameScr.isPaintQuanNu || GameScr.isPaintGiayNam || GameScr.isPaintGiayNu || GameScr.isPaintLien || GameScr.isPaintNhan || GameScr.isPaintNgocBoi || GameScr.isPaintPhu || GameScr.isPaintWeapon || GameScr.isPaintStack || GameScr.isPaintStackLock || GameScr.isPaintGrocery || GameScr.isPaintGroceryLock || GameScr.isPaintUpGrade || GameScr.isPaintConvert || GameScr.isPaintUpPearl || GameScr.isPaintBox || GameScr.isPaintSplit || GameScr.isPaintTrade;
	}

	// Token: 0x0600032F RID: 815 RVA: 0x0004B308 File Offset: 0x00049508
	public static void setPopupSize(int w, int h)
	{
		if (GameCanvas.w == 128 || GameCanvas.h <= 208)
		{
			w = 126;
			h = 160;
		}
		GameScr.indexTitle = 0;
		GameScr.popupW = w;
		GameScr.popupH = h;
		GameScr.popupX = GameScr.gW2 - w / 2;
		GameScr.popupY = GameScr.gH2 - h / 2;
		if (GameCanvas.isTouch && !GameScr.isPaintZone && !GameScr.isPaintTeam && !GameScr.isPaintClan && !GameScr.isPaintCharInMap && !GameScr.isPaintFindTeam && !GameScr.isPaintFriend && !GameScr.isPaintEnemies && !GameScr.isPaintTask && !GameScr.isPaintMessage)
		{
			if (GameCanvas.h <= 240)
			{
				GameScr.popupY -= 10;
			}
			if (GameCanvas.isTouch && !GameCanvas.isTouchControlSmallScreen && GameCanvas.currentScreen is GameScr)
			{
				GameScr.popupW = 310;
				GameScr.popupX = GameScr.gW / 2 - GameScr.popupW / 2;
				if (GameScr.isPaintInfoMe && GameScr.indexMenu > 0)
				{
					GameScr.popupW = w;
					GameScr.popupX = GameScr.gW2 - w / 2;
				}
			}
		}
		if (GameScr.popupY < -10)
		{
			GameScr.popupY = -10;
		}
		if (GameCanvas.h > 208 && GameScr.popupY < 0)
		{
			GameScr.popupY = 0;
		}
		if (GameCanvas.h == 208 && GameScr.popupY < 10)
		{
			GameScr.popupY = 10;
		}
	}

	// Token: 0x06000330 RID: 816 RVA: 0x00004664 File Offset: 0x00002864
	public static void loadImg()
	{
		TileMap.loadTileImage();
	}

	// Token: 0x06000331 RID: 817 RVA: 0x0004B480 File Offset: 0x00049680
	public void paintTitle(mGraphics g, string title, bool arrow)
	{
		int num = GameScr.gW / 2;
		g.setColor(Paint.COLORDARK);
		g.fillRoundRect(num - mFont.tahoma_8b.getWidth(title) / 2 - 12, GameScr.popupY + 4, mFont.tahoma_8b.getWidth(title) + 22, 24, 6, 6);
		if ((GameScr.indexTitle == 0 || GameCanvas.isTouch) && arrow)
		{
			SmallImage.drawSmallImage(g, 989, num - mFont.tahoma_8b.getWidth(title) / 2 - 15 - 7 - ((GameCanvas.gameTick % 8 <= 3) ? 2 : 0), GameScr.popupY + 16, 2, StaticObj.VCENTER_HCENTER);
			SmallImage.drawSmallImage(g, 989, num + mFont.tahoma_8b.getWidth(title) / 2 + 15 + 5 + ((GameCanvas.gameTick % 8 <= 3) ? 2 : 0), GameScr.popupY + 16, 0, StaticObj.VCENTER_HCENTER);
		}
		if (GameScr.indexTitle == 0)
		{
			g.setColor(Paint.COLORFOCUS);
		}
		else
		{
			g.setColor(Paint.COLORBORDER);
		}
		g.drawRoundRect(num - mFont.tahoma_8b.getWidth(title) / 2 - 12, GameScr.popupY + 4, mFont.tahoma_8b.getWidth(title) + 22, 24, 6, 6);
		mFont.tahoma_8b.drawString(g, title, num, GameScr.popupY + 9, 2);
	}

	// Token: 0x06000332 RID: 818 RVA: 0x0004B5C4 File Offset: 0x000497C4
	public static int getTaskMapId()
	{
		int result;
		if (global::Char.myCharz().taskMaint == null)
		{
			result = -1;
		}
		else
		{
			result = GameScr.mapTasks[global::Char.myCharz().taskMaint.index];
		}
		return result;
	}

	// Token: 0x06000333 RID: 819 RVA: 0x0004B5F8 File Offset: 0x000497F8
	public static sbyte getTaskNpcId()
	{
		sbyte result = 0;
		if (global::Char.myCharz().taskMaint == null)
		{
			result = -1;
		}
		else if (global::Char.myCharz().taskMaint.index <= GameScr.tasks.Length - 1)
		{
			result = (sbyte)GameScr.tasks[global::Char.myCharz().taskMaint.index];
		}
		return result;
	}

	// Token: 0x06000334 RID: 820 RVA: 0x00004426 File Offset: 0x00002626
	public void refreshTeam()
	{
	}

	// Token: 0x06000335 RID: 821 RVA: 0x0004B64C File Offset: 0x0004984C
	public void onChatFromMe(string text, string to)
	{
		if (text == "menu")
		{
			GameScr.VuDangMenuMod();
			text = "";
		}
		if (text == "ht")
		{
			VuDang.BongTai();
			text = "";
		}
		if (text == "attnl")
		{
			VuDang.isAutoTTNL = !VuDang.isAutoTTNL;
			GameScr.info1.addInfo("Auto TTNL: " + (VuDang.isAutoTTNL ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "abfdt")
		{
			VuDang.aDauDeTu = !VuDang.aDauDeTu;
			GameScr.info1.addInfo("Auto buff đậu theo chỉ số đệ tử: " + (VuDang.aDauDeTu ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text.StartsWith("bhpdt "))
		{
			VuDang.csHPDeTu = int.Parse(text.Split(new char[]
			{
				' '
			})[1]);
			GameScr.info1.addInfo("HP buff đậu đệ tử: " + NinjaUtil.getMoneys((long)VuDang.csHPDeTu), 0);
			text = "";
		}
		if (text.StartsWith("bkidt "))
		{
			VuDang.csKIDeTu = int.Parse(text.Split(new char[]
			{
				' '
			})[1]);
			GameScr.info1.addInfo("KI buff đậu đệ tử: " + NinjaUtil.getMoneys((long)VuDang.csKIDeTu), 0);
			text = "";
		}
		if (text == "abf")
		{
			VuDang.aBuffDau = !VuDang.aBuffDau;
			GameScr.info1.addInfo("Auto buff đậu theo chỉ số: " + (VuDang.aBuffDau ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text.StartsWith("bhp "))
		{
			VuDang.csHP = int.Parse(text.Split(new char[]
			{
				' '
			})[1]);
			GameScr.info1.addInfo("HP buff đậu: " + NinjaUtil.getMoneys((long)VuDang.csHP), 0);
			text = "";
		}
		if (text.StartsWith("bki "))
		{
			VuDang.csKI = int.Parse(text.Split(new char[]
			{
				' '
			})[1]);
			GameScr.info1.addInfo("KI buff đậu: " + NinjaUtil.getMoneys((long)VuDang.csKI), 0);
			text = "";
		}
		if (text == "akhu")
		{
			VuDang.isAutoVeKhu = !VuDang.isAutoVeKhu;
			GameScr.info1.addInfo((VuDang.isAutoVeKhu ? "Auto về khu cũ khi Login: Bật" : "Auto về khu cũ khi Login: Tắt") ?? "", 0);
			text = "";
		}
		if (text == "alogin")
		{
			VuDang.isAutoLogin = !VuDang.isAutoLogin;
			GameScr.info1.addInfo("Auto Login: " + (VuDang.isAutoLogin ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "set")
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Mặc set 1", 999901));
			myVector.addElement(new Command("Mặc set 2", 999902));
			GameCanvas.menu.startAt(myVector, 3);
			text = "";
		}
		if (text == "kk")
		{
			VuDang.khoakhu = !VuDang.khoakhu;
			GameScr.info1.addInfo("Khóa chuyển khu: " + (VuDang.khoakhu ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "kmap")
		{
			VuDang.khoamap = !VuDang.khoamap;
			GameScr.info1.addInfo("Khóa map: " + (VuDang.khoamap ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "tdthuong")
		{
			VuDang.isPaintCrackBall = false;
			VuDang.isThuongDeThuong = !VuDang.isThuongDeThuong;
			GameScr.info1.addInfo("Auto Quay Thượng Đế Thường : " + (VuDang.isThuongDeThuong ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "tdvip")
		{
			VuDang.isPaintCrackBall = false;
			VuDang.isThuongDeVip = !VuDang.isThuongDeVip;
			GameScr.info1.addInfo("Auto Quay Thượng Đế Vip : " + (VuDang.isThuongDeVip ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "tgt")
		{
			VuDang.isUpdateKhu = !VuDang.isUpdateKhu;
			GameScr.info1.addInfo("Update khu theo thời gian thực: " + (VuDang.isUpdateKhu ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "fcb")
		{
			VuDang.focusBoss = !VuDang.focusBoss;
			GameScr.info1.addInfo("Auto chỉ vào boss: " + (VuDang.focusBoss ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "nmt")
		{
			if (VuDang.getX(0) > 0 && VuDang.getY(0) > 0)
			{
				VuDang.GotoXY(VuDang.getX(0), VuDang.getY(0));
			}
			else
			{
				VuDang.GotoXY(30, PickMobController.GetYsd(30));
			}
			text = "";
		}
		if (text == "nmp")
		{
			if (VuDang.getX(2) > 0 && VuDang.getY(2) > 0)
			{
				VuDang.GotoXY(VuDang.getX(2), VuDang.getY(2));
			}
			else
			{
				VuDang.GotoXY(TileMap.pxw - 30, PickMobController.GetYsd(TileMap.pxw - 30));
			}
			text = "";
		}
		if (text == "nmg")
		{
			if (VuDang.getX(1) > 0 && VuDang.getY(1) > 0)
			{
				VuDang.GotoXY(VuDang.getX(1), VuDang.getY(1));
				Service.gI().getMapOffline();
				Service.gI().requestChangeMap();
			}
			else
			{
				VuDang.GotoXY(TileMap.pxw / 2, PickMobController.GetYsd(TileMap.pxw / 2));
			}
			text = "";
		}
		if (text == "nmtr")
		{
			if (VuDang.getX(3) > 0 && VuDang.getY(3) > 0)
			{
				VuDang.GotoXY(VuDang.getX(3), VuDang.getY(3));
			}
			text = "";
		}
		if (text.StartsWith("do "))
		{
			VuDang.bossCanDo = text.Replace("do ", "");
			GameScr.info1.addInfo("Boss cần dò: " + VuDang.bossCanDo, 0);
			text = "";
		}
		if (text.StartsWith("dk "))
		{
			VuDang.zoneMacDinh = int.Parse(text.Replace("dk ", ""));
			GameScr.info1.addInfo("Dò boss từ khu " + VuDang.zoneMacDinh, 0);
			text = "";
		}
		if (text == "clrz")
		{
			VuDang.zoneMacDinh = 0;
			GameScr.info1.addInfo("Reset khu dò boss xuống", 0);
			text = "";
		}
		if (text == "doall")
		{
			VuDang.doBoss = !VuDang.doBoss;
			GameScr.info1.addInfo("Dò boss: " + (VuDang.doBoss ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "mont")
		{
			VuDang.isNoitai = !VuDang.isNoitai;
			new Thread(new ThreadStart(VuDang.AutoNoiTai)).Start();
			GameScr.info1.addInfo("Auto mở nội tại : " + (VuDang.isNoitai ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "ahs")
		{
			VuDang.hoiSinhNgoc = !VuDang.hoiSinhNgoc;
			GameScr.info1.addInfo((VuDang.hoiSinhNgoc ? "Auto hồi sinh bằng số ngọc được chỉ định: Bật" : "Auto hồi sinh bằng số ngọc được chỉ định: Tắt") ?? "", 0);
			text = "";
		}
		if (text.StartsWith("ngochs "))
		{
			VuDang.ngocHienTai = global::Char.myCharz().luongKhoa + global::Char.myCharz().luong;
			VuDang.ngocDuocDungDeHoiSinh = int.Parse(text.Replace("ngochs ", ""));
			GameScr.info1.addInfo("Ngọc được sử dụng để hồi sinh là " + VuDang.ngocDuocDungDeHoiSinh, 0);
			text = "";
		}
		if (text == "ksbs5")
		{
			VuDang.isKSBoss = false;
			VuDang.isKSBossBangSkill5 = !VuDang.isKSBossBangSkill5;
			GameScr.info1.addInfo((VuDang.isKSBossBangSkill5 ? "KS Boss Bằng Skill 5: Bật" : "KS Boss Bằng Skill 5: Tắt") ?? "", 0);
			text = "";
		}
		if (text == "ksb")
		{
			VuDang.isKSBossBangSkill5 = false;
			VuDang.isKSBoss = !VuDang.isKSBoss;
			GameScr.info1.addInfo((VuDang.isKSBoss ? "KS Boss bằng đấm thường: Bật" : "KS Boss bằng đấm thường: Tắt") ?? "", 0);
			text = "";
		}
		if (text.StartsWith("hpboss "))
		{
			VuDang.HPKSBoss = int.Parse(text.Replace("hpboss ", ""));
			GameScr.info1.addInfo("HP Boss khi đạt " + NinjaUtil.getMoneys((long)VuDang.HPKSBoss) + " sẽ oánh bỏ con mẹ boss", 0);
			text = "";
		}
		if (text == "ttsp")
		{
			VuDang.isThongTinSuPhu = !VuDang.isThongTinSuPhu;
			GameScr.info1.addInfo("Thông Tin Sư Phụ: " + (VuDang.isThongTinSuPhu ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "ttdt")
		{
			VuDang.isThongTinDeTu = !VuDang.isThongTinDeTu;
			GameScr.info1.addInfo("Thông Tin Đệ Tử: " + (VuDang.isThongTinDeTu ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "xtb")
		{
			VuDang.xoaTauBay = !VuDang.xoaTauBay;
			GameScr.info1.addInfo("Xóa tàu bay: " + (VuDang.xoaTauBay ? "Tắt" : "Bật"), 0);
			text = "";
		}
		if (text == "xht")
		{
			VuDang.xoaHieuUngHopThe = !VuDang.xoaHieuUngHopThe;
			GameScr.info1.addInfo("Hiệu ứng hợp thể: " + (VuDang.xoaHieuUngHopThe ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "kvt")
		{
			VuDang.ghimX = global::Char.myCharz().cx;
			VuDang.ghimY = global::Char.myCharz().cy;
			VuDang.isKhoaViTri = !VuDang.isKhoaViTri;
			GameScr.info1.addInfo("Khóa vị trí: " + (VuDang.isKhoaViTri ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text.StartsWith("l "))
		{
			VuDang.dichLenXuongTraiPhai = int.Parse(text.Replace("l ", ""));
			global::Char.myCharz().cx -= VuDang.dichLenXuongTraiPhai;
			Service.gI().charMove();
			GameScr.info1.addInfo("Dịch trái " + VuDang.dichLenXuongTraiPhai, 0);
			text = "";
		}
		if (text.StartsWith("r "))
		{
			VuDang.dichLenXuongTraiPhai = int.Parse(text.Replace("r ", ""));
			global::Char.myCharz().cx += VuDang.dichLenXuongTraiPhai;
			Service.gI().charMove();
			GameScr.info1.addInfo("Dịch phải " + VuDang.dichLenXuongTraiPhai, 0);
			text = "";
		}
		if (text.StartsWith("u "))
		{
			VuDang.dichLenXuongTraiPhai = int.Parse(text.Replace("u ", ""));
			global::Char.myCharz().cy -= VuDang.dichLenXuongTraiPhai;
			Service.gI().charMove();
			GameScr.info1.addInfo("Khinh công " + VuDang.dichLenXuongTraiPhai, 0);
			text = "";
		}
		if (text.StartsWith("d "))
		{
			VuDang.dichLenXuongTraiPhai = int.Parse(text.Replace("d ", ""));
			global::Char.myCharz().cy += VuDang.dichLenXuongTraiPhai;
			Service.gI().charMove();
			GameScr.info1.addInfo("Đi vào lòng đất " + VuDang.dichLenXuongTraiPhai, 0);
			text = "";
		}
		if (text == "line")
		{
			VuDang.lineboss = !VuDang.lineboss;
			GameScr.info1.addInfo("Đường kẻ tới boss: " + (VuDang.lineboss ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "sb")
		{
			VuDang.thongBaoBoss = !VuDang.thongBaoBoss;
			GameScr.info1.addInfo("Thông báo boss: " + (VuDang.thongBaoBoss ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "ttnv")
		{
			VuDang.isBossM = false;
			VuDang.isPKM = false;
			VuDang.trangThai = !VuDang.trangThai;
			GameScr.info1.addInfo("Trạng thái nhân vật đang trỏ: " + (VuDang.trangThai ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "bossm")
		{
			VuDang.isBossM = !VuDang.isBossM;
			VuDang.isPKM = false;
			VuDang.trangThai = false;
			GameScr.info1.addInfo("Boss trong khu: " + (VuDang.isBossM ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "pkm")
		{
			VuDang.isPKM = !VuDang.isPKM;
			VuDang.isBossM = false;
			VuDang.trangThai = false;
			GameScr.info1.addInfo("Bọn đấm nhau được trong khu: " + (VuDang.isPKM ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "gdl")
		{
			VuDang.giamDungLuong = !VuDang.giamDungLuong;
			GameScr.info1.addInfo("Giảm dung lượng: " + (VuDang.giamDungLuong ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "xbg")
		{
			VuDang.XoaBackground = !VuDang.XoaBackground;
			GameScr.info1.addInfo("Xóa background: " + (VuDang.XoaBackground ? "Tắt" : "Bật"), 0);
			text = "";
		}
		if (text == "xoamap")
		{
			VuDang.xoamap = !VuDang.xoamap;
			GameScr.info1.addInfo("Xóa map: " + (VuDang.xoamap ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "gmt")
		{
			VuDang.isGMT = false;
			text = "";
		}
		if (text.StartsWith("gmt "))
		{
			int num = int.Parse(text.Remove(0, 4));
			if (num < GameScr.vCharInMap.size())
			{
				VuDang.isGMT = true;
				VuDang.charMT = (global::Char)GameScr.vCharInMap.elementAt(num);
			}
			text = "";
		}
		if (text == "abt")
		{
			VuDang.isAutoBT = !VuDang.isAutoBT;
			GameScr.info1.addInfo("Auto bông tai: " + (VuDang.isAutoBT ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text.StartsWith("bt "))
		{
			VuDang.timeBT = int.Parse(text.Replace("bt ", ""));
			GameScr.info1.addInfo("Delay auto bông tai: " + VuDang.timeBT + "s", 0);
			text = "";
		}
		if (text == "anz")
		{
			VuDang.isAutoNhatXa = !VuDang.isAutoNhatXa;
			if (VuDang.isAutoNhatXa)
			{
				VuDang.xNhatXa = global::Char.myCharz().cx;
				VuDang.yNhatXa = global::Char.myCharz().cy;
				GameScr.info1.addInfo(string.Concat(new object[]
				{
					"Tọa Độ : ",
					global::Char.myCharz().cx,
					"|",
					global::Char.myCharz().cy
				}), 0);
			}
			GameScr.info1.addInfo("Auto Nhặt Xa : " + (VuDang.isAutoNhatXa ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "ak")
		{
			VuDang.isAK = !VuDang.isAK;
			GameScr.info1.addInfo("Auto đánh: " + (VuDang.isAK ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text.StartsWith("ndc "))
		{
			VuDang.textAutoChat = text.Replace("ndc ", "");
			GameScr.info1.addInfo("Nội dung auto chat : " + VuDang.textAutoChat, 0);
			text = "";
		}
		if (text.StartsWith("ndctg "))
		{
			VuDang.textAutoChatTG = text.Replace("ndc ", "");
			GameScr.info1.addInfo("Nội dung auto chat thế giới : " + VuDang.textAutoChatTG, 0);
			text = "";
		}
		if (text == "atchattg")
		{
			VuDang.isAutoCTG = !VuDang.isAutoCTG;
			GameScr.info1.addInfo("Auto Chat Thế Giới: " + (VuDang.isAutoCTG ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text == "atc")
		{
			VuDang.achat = !VuDang.achat;
			GameScr.info1.addInfo("Auto chat : " + (VuDang.achat ? "Bật" : "Tắt"), 0);
			text = string.Empty;
		}
		if (text.StartsWith("go "))
		{
			int num2 = int.Parse(text.Remove(0, 3));
			if (num2 < GameScr.vCharInMap.size())
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(num2);
				VuDang.GotoXY(@char.cx, @char.cy);
				global::Char.myCharz().focusManualTo(@char);
			}
			text = "";
		}
		if (text == "showhp")
		{
			VuDang.nvat = !VuDang.nvat;
			GameScr.info1.addInfo("Thông tin người chơi trong map: " + (VuDang.nvat ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text.StartsWith("kx "))
		{
			new Thread(new ParameterizedThreadStart(VuDang.VaoKhu)).Start(int.Parse(text.Remove(0, 3)));
			text = "";
		}
		if (text.StartsWith("k "))
		{
			Service.gI().requestChangeZone(int.Parse(text.Replace("k ", "")), -1);
			text = "";
		}
		if (text.StartsWith("tdc "))
		{
			VuDang.tocdochay = int.Parse(text.Replace("tdc ", ""));
			GameScr.info1.addInfo("Tốc độ phóng: " + VuDang.tocdochay, 0);
			text = "";
		}
		if (text == "dapdo")
		{
			VuDang.isDapDo = !VuDang.isDapDo;
			new Thread(new ThreadStart(VuDang.AutoDapDo)).Start();
			GameScr.info1.addInfo("Đập đồ: " + (VuDang.isDapDo ? "Bật" : "Tắt"), 0);
			text = "";
		}
		if (text.StartsWith("speed "))
		{
			Time.timeScale = float.Parse(text.Remove(0, 6));
			GameScr.info1.addInfo("Tốc Độ Game: " + Time.timeScale, 0);
			text = "";
		}
		Res.outz("CHAT");
		if (!GameScr.isPaintMessage || GameCanvas.isTouch)
		{
			ChatTextField.gI().isShow = false;
		}
		if (to.Equals(mResources.chat_player))
		{
			if (GameScr.info2.playerID != global::Char.myCharz().charID)
			{
				Service.gI().chatPlayer(text, GameScr.info2.playerID);
				return;
			}
		}
		else if (!text.Equals(string.Empty))
		{
			Service.gI().chat(text);
		}
	}

	// Token: 0x06000336 RID: 822 RVA: 0x0000466B File Offset: 0x0000286B
	public void onCancelChat()
	{
		if (GameScr.isPaintMessage)
		{
			GameScr.isPaintMessage = false;
			ChatTextField.gI().center = null;
		}
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0004CAC4 File Offset: 0x0004ACC4
	public void openWeb(string strLeft, string strRight, string url, string title, string str)
	{
		GameScr.isPaintAlert = true;
		this.isLockKey = true;
		GameScr.indexRow = 0;
		GameScr.setPopupSize(175, 200);
		this.textsTitle = title;
		this.texts = mFont.tahoma_7.splitFontVector(str, GameScr.popupW - 30);
		this.center = null;
		this.left = new Command(strLeft, 11068, url);
		this.right = new Command(strRight, 11069);
	}

	// Token: 0x06000338 RID: 824 RVA: 0x0004CB40 File Offset: 0x0004AD40
	public void sendSms(string strLeft, string strRight, short port, string syntax, string title, string str)
	{
		GameScr.isPaintAlert = true;
		this.isLockKey = true;
		GameScr.indexRow = 0;
		GameScr.setPopupSize(175, 200);
		this.textsTitle = title;
		this.texts = mFont.tahoma_7.splitFontVector(str, GameScr.popupW - 30);
		this.center = null;
		MyVector myVector = new MyVector();
		myVector.addElement(string.Empty + port);
		myVector.addElement(syntax);
		this.left = new Command(strLeft, 11074);
		this.right = new Command(strRight, 11075);
	}

	// Token: 0x06000339 RID: 825 RVA: 0x00004685 File Offset: 0x00002885
	public void actMenu()
	{
		GameCanvas.panel.setTypeMain();
		GameCanvas.panel.show();
	}

	// Token: 0x0600033A RID: 826 RVA: 0x0004CBDC File Offset: 0x0004ADDC
	public void openUIZone(Message message)
	{
		InfoDlg.hide();
		try
		{
			this.zones = new int[(int)message.reader().readByte()];
			this.pts = new int[this.zones.Length];
			this.numPlayer = new int[this.zones.Length];
			this.maxPlayer = new int[this.zones.Length];
			this.rank1 = new int[this.zones.Length];
			this.rankName1 = new string[this.zones.Length];
			this.rank2 = new int[this.zones.Length];
			this.rankName2 = new string[this.zones.Length];
			for (int i = 0; i < this.zones.Length; i++)
			{
				this.zones[i] = (int)message.reader().readByte();
				this.pts[i] = (int)message.reader().readByte();
				this.numPlayer[i] = (int)message.reader().readByte();
				this.maxPlayer[i] = (int)message.reader().readByte();
				if (message.reader().readByte() == 1)
				{
					this.rankName1[i] = message.reader().readUTF();
					this.rank1[i] = message.reader().readInt();
					this.rankName2[i] = message.reader().readUTF();
					this.rank2[i] = message.reader().readInt();
				}
			}
			VuDang.currentCheckLag = 30;
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham OPEN UIZONE " + ex.ToString());
		}
	}

	// Token: 0x0600033B RID: 827 RVA: 0x0000469B File Offset: 0x0000289B
	public void showViewInfo()
	{
		GameScr.indexMenu = 3;
		GameScr.isPaintInfoMe = true;
		GameScr.setPopupSize(175, 200);
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0004CD88 File Offset: 0x0004AF88
	private void actDead()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(mResources.DIES[1], 110381));
		myVector.addElement(new Command(mResources.DIES[2], 110382));
		myVector.addElement(new Command(mResources.DIES[3], 110383));
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x0600033D RID: 829 RVA: 0x000046B8 File Offset: 0x000028B8
	public void startYesNoPopUp(string info, Command cmdYes, Command cmdNo)
	{
		this.popUpYesNo = new PopUpYesNo();
		this.popUpYesNo.setPopUp(info, cmdYes, cmdNo);
	}

	// Token: 0x0600033E RID: 830 RVA: 0x0004CDEC File Offset: 0x0004AFEC
	public void player_vs_player(int playerId, int xu, string info, sbyte typePK)
	{
		global::Char @char = GameScr.findCharInMap(playerId);
		if (@char != null)
		{
			if (typePK == 3)
			{
				this.startYesNoPopUp(info, new Command(mResources.OK, 2000, @char), new Command(mResources.CLOSE, 2009, @char));
			}
			if (typePK == 4)
			{
				this.startYesNoPopUp(info, new Command(mResources.OK, 2005, @char), new Command(mResources.CLOSE, 2009, @char));
			}
		}
	}

	// Token: 0x0600033F RID: 831 RVA: 0x0004CE5C File Offset: 0x0004B05C
	public void giaodich(int playerID)
	{
		global::Char @char = GameScr.findCharInMap(playerID);
		if (@char != null)
		{
			this.startYesNoPopUp(@char.cName + mResources.want_to_trade, new Command(mResources.YES, 11114, @char), new Command(mResources.NO, 2009, @char));
		}
	}

	// Token: 0x06000340 RID: 832 RVA: 0x0004CEAC File Offset: 0x0004B0AC
	public void getFlagImage(int charID, sbyte cflag)
	{
		if (GameScr.vFlag.size() == 0)
		{
			Service.gI().getFlag(2, cflag);
			Res.outz("getFlag1");
			return;
		}
		if (charID == global::Char.myCharz().charID)
		{
			Res.outz("my cflag: isme");
			if (global::Char.myCharz().isGetFlagImage(cflag))
			{
				Res.outz("my cflag: true");
				for (int i = 0; i < GameScr.vFlag.size(); i++)
				{
					PKFlag pkflag = (PKFlag)GameScr.vFlag.elementAt(i);
					if (pkflag != null && pkflag.cflag == cflag)
					{
						Res.outz("my cflag: cflag==");
						global::Char.myCharz().flagImage = pkflag.IDimageFlag;
					}
				}
				return;
			}
			if (!global::Char.myCharz().isGetFlagImage(cflag))
			{
				Res.outz("my cflag: false");
				Service.gI().getFlag(2, cflag);
				return;
			}
		}
		else
		{
			Res.outz("my cflag: not me");
			if (GameScr.findCharInMap(charID) != null)
			{
				if (GameScr.findCharInMap(charID).isGetFlagImage(cflag))
				{
					Res.outz("my cflag: true");
					for (int j = 0; j < GameScr.vFlag.size(); j++)
					{
						PKFlag pkflag2 = (PKFlag)GameScr.vFlag.elementAt(j);
						if (pkflag2 != null && pkflag2.cflag == cflag)
						{
							Res.outz("my cflag: cflag==");
							GameScr.findCharInMap(charID).flagImage = pkflag2.IDimageFlag;
						}
					}
					return;
				}
				if (!GameScr.findCharInMap(charID).isGetFlagImage(cflag))
				{
					Res.outz("my cflag: false");
					Service.gI().getFlag(2, cflag);
				}
			}
		}
	}

	// Token: 0x06000341 RID: 833 RVA: 0x0004D020 File Offset: 0x0004B220
	public void actionPerform(int idAction, object p)
	{
		Cout.println("PERFORM WITH ID = " + idAction);
		if (idAction == 999901)
		{
			new Thread(new ThreadStart(VuDang.macSet1)).Start();
		}
		if (idAction == 999902)
		{
			new Thread(new ThreadStart(VuDang.macSet2)).Start();
		}
		if (idAction == 3)
		{
			VuDang.thudau = !VuDang.thudau;
			GameScr.info1.addInfo("Thu Đậu: " + (VuDang.thudau ? "Bật" : "Tắt"), 0);
			GameScr.VuDangMenuMod();
		}
		if (idAction == 4)
		{
			VuDang.xindau = !VuDang.xindau;
			GameScr.info1.addInfo("Xin Đậu: " + (VuDang.xindau ? "Bật" : "Tắt"), 0);
			GameScr.VuDangMenuMod();
		}
		if (idAction == 5)
		{
			VuDang.chodau = !VuDang.chodau;
			new Thread(new ThreadStart(VuDang.cd)).Start();
			GameScr.info1.addInfo("Cho Đậu: " + (VuDang.chodau ? "Bật" : "Tắt"), 0);
			GameScr.VuDangMenuMod();
		}
		if (idAction == 6)
		{
			VuDang.nvat = !VuDang.nvat;
			GameScr.info1.addInfo("Thông tin người chơi trong map: " + (VuDang.nvat ? "Bật" : "Tắt"), 0);
			GameScr.VuDangMenuMod();
		}
		if (idAction == 7)
		{
			VuDang.XoaBackground = !VuDang.XoaBackground;
			GameScr.info1.addInfo("Xóa background: " + (VuDang.XoaBackground ? "Bật" : "Tắt"), 0);
			GameScr.VuDangMenuMod();
		}
		if (idAction == 9)
		{
			VuDang.xoamap = !VuDang.xoamap;
			GameScr.info1.addInfo("Xóa map: " + (VuDang.xoamap ? "Bật" : "Tắt"), 0);
			GameScr.VuDangMenuMod();
		}
		if (idAction == 11)
		{
			VuDang.giamDungLuong = !VuDang.giamDungLuong;
			GameScr.info1.addInfo("Giảm dung lượng: " + (VuDang.giamDungLuong ? "Bật" : "Tắt"), 0);
			GameScr.VuDangMenuMod();
		}
		if (idAction == 12)
		{
			VuDang.isAutoNeBoss = !VuDang.isAutoNeBoss;
			GameScr.info1.addInfo("Auto né boss: " + (VuDang.isAutoNeBoss ? "Bật" : "Tắt"), 0);
			GameScr.VuDangMenuMod();
		}
		if (idAction == 13)
		{
			VuDang.isAutoCo = !VuDang.isAutoCo;
			GameScr.info1.addInfo("Auto cờ né súc vật: " + (VuDang.isAutoCo ? "Bật" : "Tắt"), 0);
			GameScr.VuDangMenuMod();
		}
		switch (idAction)
		{
		case 14:
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command(VuDang.thudau ? "Auto thu đậu: Bật" : "Auto thu đậu: Tắt", 3));
			myVector.addElement(new Command(VuDang.chodau ? "Auto cho đậu: Bật" : "Auto cho đậu: Tắt", 5));
			myVector.addElement(new Command(VuDang.xindau ? "Auto xin đậu: Bật" : "Auto xin đậu: Tắt", 4));
			GameCanvas.menu.startAt(myVector, 4);
			return;
		}
		case 15:
		{
			MyVector myVector2 = new MyVector();
			myVector2.addElement(new Command(VuDang.XoaBackground ? "Xóa Background: Bật" : "Xóa Background: Tắt", 7));
			myVector2.addElement(new Command(VuDang.xoamap ? "Xóa địa hình: Bật" : "Xóa địa hình: Tắt", 9));
			myVector2.addElement(new Command(VuDang.giamDungLuong ? "Giảm dung lượng: Bật" : "Giảm dung lượng: Tắt", 11));
			GameCanvas.menu.startAt(myVector2, 4);
			return;
		}
		case 16:
		{
			MyVector myVector3 = new MyVector();
			myVector3.addElement(new Command(VuDang.isKOK ? "Up đệ kok: Bật" : "Up đệ kok: Tắt", 19));
			myVector3.addElement(new Command(VuDang.isThongTinSuPhu ? "TT sư phụ: Bật" : "TT sư phụ: Tắt", 92));
			myVector3.addElement(new Command(VuDang.isThongTinDeTu ? "TT đệ tử: Bật" : "TT đệ tử: Tắt", 93));
			myVector3.addElement(new Command(VuDang.isAutoNeBoss ? "Né boss: Bật" : "Né boss: Tắt", 12));
			myVector3.addElement(new Command(VuDang.isAutoCo ? "Auto cờ: Bật" : "Auto cờ: Tắt", 13));
			myVector3.addElement(new Command(VuDang.isCheckLag ? "Checklag: Bật" : "Checklag: Tắt", 75));
			GameCanvas.menu.startAt(myVector3, 4);
			return;
		}
		default:
			if (idAction == 19)
			{
				VuDang.isKOK = !VuDang.isKOK;
				GameScr.info1.addInfo("Auto up đệ kok: " + (VuDang.isKOK ? "Bật" : "Tắt"), 0);
				GameScr.VuDangMenuMod();
			}
			if (idAction == 20)
			{
				VuDang.isBossM = !VuDang.isBossM;
				VuDang.isPKM = false;
				VuDang.trangThai = false;
				GameScr.info1.addInfo("Boss trong khu: " + (VuDang.isBossM ? "Bật" : "Tắt"), 0);
				GameScr.VuDangMenuMod();
			}
			if (idAction == 23)
			{
				VuDang.autoHoiSinh = !VuDang.autoHoiSinh;
				GameScr.info1.addInfo("Auto hồi sinh bằng ngọc: " + (VuDang.autoHoiSinh ? "Bật" : "Tắt"), 0);
				GameScr.VuDangMenuMod();
			}
			if (idAction == 25)
			{
				VuDang.isAutoBT = !VuDang.isAutoBT;
				GameScr.info1.addInfo("Auto bông tai: " + (VuDang.isAutoBT ? "Bật" : "Tắt"), 0);
				GameScr.VuDangMenuMod();
			}
			if (idAction == 26)
			{
				VuDang.khoamap = !VuDang.khoamap;
				GameScr.info1.addInfo("Khóa map: " + (VuDang.khoamap ? "Bật" : "Tắt"), 0);
				GameScr.VuDangMenuMod();
			}
			if (idAction == 27)
			{
				VuDang.khoakhu = !VuDang.khoakhu;
				GameScr.info1.addInfo("Khóa chuyển khu: " + (VuDang.khoakhu ? "Bật" : "Tắt"), 0);
				GameScr.VuDangMenuMod();
			}
			if (idAction == 28)
			{
				VuDang.isBossM = false;
				VuDang.isPKM = false;
				VuDang.trangThai = !VuDang.trangThai;
				GameScr.info1.addInfo("Trạng thái nhân vật đang trỏ: " + (VuDang.trangThai ? "Bật" : "Tắt"), 0);
				GameScr.VuDangMenuMod();
			}
			if (idAction == 29)
			{
				VuDang.isAK = !VuDang.isAK;
				GameScr.info1.addInfo("Tự động chém: " + (VuDang.isAK ? "Bật" : "Tắt"), 0);
				GameScr.VuDangMenuMod();
			}
			switch (idAction)
			{
			case 31:
			{
				MyVector myVector4 = new MyVector();
				myVector4.addElement(new Command(VuDang.isAutoBT ? "Auto bông tai: Bật" : "Auto bông tai: Tắt", 25));
				myVector4.addElement(new Command(VuDang.isAK ? "Tự động đánh: Bật" : "Tự động đánh: Tắt", 29));
				myVector4.addElement(new Command(VuDang.isAutoTTNL ? "Auto TTNL: Bật" : "Auto TTNL: Tắt", 37));
				myVector4.addElement(new Command(VuDang.isKhoaViTri ? "Khóa vị trí: Bật" : "Khóa vị trí: Tắt", 64));
				myVector4.addElement(new Command(VuDang.isAutoAnNho ? "Auto ăn nho: Bật" : "Auto ăn nho: Tắt", 41));
				myVector4.addElement(new Command(VuDang.isAutoNhatXa ? "Auto nhặt xa: Bật" : "Auto nhặt xa: Tắt", 91));
				GameCanvas.menu.startAt(myVector4, 4);
				return;
			}
			case 32:
			{
				MyVector myVector5 = new MyVector();
				myVector5.addElement(new Command(VuDang.isBossM ? "Boss trong khu: Bật" : "Boss trong khu: Tắt", 20));
				myVector5.addElement(new Command(VuDang.isPKM ? "Được đánh trong khu: Bật" : "Được đánh trong khu: Tắt", 50));
				myVector5.addElement(new Command(VuDang.trangThai ? "Soi Time Skill: Bật" : "Soi Time Skill: Tắt", 28));
				myVector5.addElement(new Command(VuDang.thongBaoBoss ? "Thông báo boss: Bật" : "Thông báo boss: Tắt", 46));
				myVector5.addElement(new Command(VuDang.lineboss ? "Đường kẻ tới boss: Bật" : "Đường kẻ tới boss: Tắt", 47));
				myVector5.addElement(new Command(VuDang.focusBoss ? "Focus boss: Bật" : "Focus boss: Tắt", 52));
				myVector5.addElement(new Command(VuDang.khoamap ? "Khóa chuyển map: Bật" : "Khóa chuyển map: Tắt", 26));
				myVector5.addElement(new Command(VuDang.khoakhu ? "Khóa chuyển khu: Bật" : "Khóa chuyển khu: Tắt", 27));
				GameCanvas.menu.startAt(myVector5, 4);
				return;
			}
			case 33:
			{
				MyVector myVector6 = new MyVector();
				myVector6.addElement(new Command(VuDang.nvat ? "TT nvật: Bật" : "TT nvật: Tắt", 6));
				myVector6.addElement(new Command(VuDang.isAutoLogin ? "Auto login: Bật" : "Auto login: Tắt", 36));
				myVector6.addElement(new Command(VuDang.isAutoVeKhu ? "Auto khu: Bật" : "Auto khu: Tắt", 39));
				myVector6.addElement(new Command(VuDang.IsGoBack ? "Goback tọa độ: Bật" : "Goback tọa độ: Tắt", 38));
				myVector6.addElement(new Command(VuDang.autoHoiSinh ? "Auto HS ngọc: Bật" : "Auto HS ngọc: Tắt", 23));
				myVector6.addElement(new Command(VuDang.hoiSinhNgoc ? "Auto HS ngọc 2: Bật" : "Auto HS ngọc 2: Tắt", 63));
				myVector6.addElement(new Command(Application.runInBackground ? "Đóng băng cpu: Tắt" : "Đóng băng cpu: Bật", 24));
				GameCanvas.menu.startAt(myVector6, 4);
				return;
			}
			default:
				if (idAction == 36)
				{
					VuDang.isAutoLogin = !VuDang.isAutoLogin;
					GameScr.info1.addInfo("Auto login: " + (VuDang.isAutoLogin ? "Bật" : "Tắt"), 0);
					GameScr.VuDangMenuMod();
				}
				if (idAction == 37)
				{
					VuDang.isAutoTTNL = !VuDang.isAutoTTNL;
					GameScr.info1.addInfo("Auto TTNL: " + (VuDang.isAutoTTNL ? "Bật" : "Tắt"), 0);
					GameScr.VuDangMenuMod();
				}
				if (idAction == 38)
				{
					VuDang.IdmapGB = TileMap.mapID;
					VuDang.ZoneGB = TileMap.zoneID;
					VuDang.xGB = global::Char.myCharz().cx;
					VuDang.yGB = global::Char.myCharz().cy;
					VuDang.IsGoBack = !VuDang.IsGoBack;
					if (VuDang.IsGoBack)
					{
						GameScr.info1.addInfo(string.Concat(new object[]
						{
							"Map Goback: ",
							TileMap.mapName,
							" | Khu: ",
							TileMap.zoneID
						}), 0);
						GameScr.info1.addInfo(string.Concat(new object[]
						{
							"Tọa độ X: ",
							VuDang.xGB,
							" | Y: ",
							VuDang.yGB
						}), 0);
						if (global::Char.myCharz().cHP <= 0 || global::Char.myCharz().statusMe == 14)
						{
							Service.gI().returnTownFromDead();
							new Thread(new ThreadStart(VuDang.GoBack)).Start();
						}
					}
					GameScr.info1.addInfo("Goback tọa độ: " + (VuDang.IsGoBack ? "Bật" : "Tắt"), 0);
					GameScr.VuDangMenuMod();
				}
				if (idAction == 39)
				{
					VuDang.isAutoVeKhu = !VuDang.isAutoVeKhu;
					GameScr.info1.addInfo((VuDang.isAutoVeKhu ? "Auto về khu cũ khi Login: Bật" : "Auto về khu cũ khi Login: Tắt") ?? "", 0);
					GameScr.VuDangMenuMod();
				}
				if (idAction == 41)
				{
					VuDang.isAutoAnNho = !VuDang.isAutoAnNho;
					GameScr.info1.addInfo("Auto ăn nho: " + (VuDang.isAutoAnNho ? "Bật" : "Tắt"), 0);
					GameScr.VuDangMenuMod();
				}
				if (idAction == 43)
				{
					Pk9rPickMob.IsNeSieuQuai = !Pk9rPickMob.IsNeSieuQuai;
					GameScr.info1.addInfo("Tàn sát né siêu quái: " + (Pk9rPickMob.IsNeSieuQuai ? "Bật" : "Tắt"), 0);
					GameScr.VuDangMenuMod();
				}
				if (idAction == 44)
				{
					Pk9rPickMob.IsTanSat = !Pk9rPickMob.IsTanSat;
					GameScr.info1.addInfo("Tự động đánh quái: " + (Pk9rPickMob.IsTanSat ? "Bật" : "Tắt"), 0);
					GameScr.VuDangMenuMod();
				}
				if (idAction != 45)
				{
					if (idAction == 46)
					{
						VuDang.thongBaoBoss = !VuDang.thongBaoBoss;
						GameScr.info1.addInfo("Thông báo boss: " + (VuDang.thongBaoBoss ? "Bật" : "Tắt"), 0);
						GameScr.VuDangMenuMod();
					}
					if (idAction == 47)
					{
						VuDang.lineboss = !VuDang.lineboss;
						GameScr.info1.addInfo("Đường kẻ tới Boss: " + (VuDang.lineboss ? "Bật" : "Tắt"), 0);
						GameScr.VuDangMenuMod();
					}
					if (idAction == 50)
					{
						VuDang.isPKM = !VuDang.isPKM;
						VuDang.isBossM = false;
						VuDang.trangThai = false;
						GameScr.info1.addInfo("Bọn được đấm nhau trong khu: " + (VuDang.isPKM ? "Bật" : "Tắt"), 0);
						GameScr.VuDangMenuMod();
					}
					if (idAction == 52)
					{
						VuDang.focusBoss = !VuDang.focusBoss;
						GameScr.info1.addInfo("Auto chỉ vào boss: " + (VuDang.focusBoss ? "Bật" : "Tắt"), 0);
						GameScr.VuDangMenuMod();
					}
					if (idAction == 63)
					{
						VuDang.ngocHienTai = global::Char.myCharz().luongKhoa + global::Char.myCharz().luong;
						VuDang.hoiSinhNgoc = !VuDang.hoiSinhNgoc;
						GameScr.info1.addInfo((VuDang.hoiSinhNgoc ? "Auto hồi sinh bằng số ngọc được chỉ định: Bật" : "Auto hồi sinh bằng số ngọc được chỉ định: Tắt") ?? "", 0);
						GameScr.VuDangMenuMod();
					}
					if (idAction == 64)
					{
						VuDang.ghimX = global::Char.myCharz().cx;
						VuDang.ghimY = global::Char.myCharz().cy;
						VuDang.isKhoaViTri = !VuDang.isKhoaViTri;
						GameScr.info1.addInfo("Khóa vị trí: " + (VuDang.isKhoaViTri ? "Bật" : "Tắt"), 0);
						GameScr.VuDangMenuMod();
					}
					if (idAction == 75)
					{
						VuDang.isCheckLag = !VuDang.isCheckLag;
						GameScr.info1.addInfo("Checklag: " + (VuDang.isCheckLag ? "Bật" : "Tắt"), 0);
						GameScr.VuDangMenuMod();
					}
					if (idAction != 76)
					{
						if (idAction != 80)
						{
							switch (idAction)
							{
							case 89:
							{
								MyVector myVector7 = new MyVector();
								for (int i = 0; i < GameScr.keySkill.Length; i++)
								{
									myVector7.addElement(new Command((GameScr.keySkill[i] == null) ? "Chưa Gán Skill" : (GameScr.keySkill[i].template.name + " [" + (VuDang.listSkillsAuto.Contains(GameScr.keySkill[i]) ? "Xóa" : "Thêm") + "]"), 90));
								}
								GameCanvas.menu.startAt(myVector7, 4);
								break;
							}
							case 90:
								VuDang.AddRemoveSkill(GameCanvas.menu.menuSelectedItem);
								break;
							case 91:
								VuDang.isAutoNhatXa = !VuDang.isAutoNhatXa;
								if (VuDang.isAutoNhatXa)
								{
									VuDang.xNhatXa = global::Char.myCharz().cx;
									VuDang.yNhatXa = global::Char.myCharz().cy;
									GameScr.info1.addInfo(string.Concat(new object[]
									{
										"Tọa Độ : ",
										global::Char.myCharz().cx,
										"|",
										global::Char.myCharz().cy
									}), 0);
								}
								GameScr.info1.addInfo("Auto Nhặt Xa : " + (VuDang.isAutoNhatXa ? "Bật" : "Tắt"), 0);
								GameScr.VuDangMenuMod();
								break;
							case 92:
								VuDang.isThongTinSuPhu = !VuDang.isThongTinSuPhu;
								GameScr.info1.addInfo("Thông Tin Sư Phụ: " + (VuDang.isThongTinSuPhu ? "Bật" : "Tắt"), 0);
								GameScr.VuDangMenuMod();
								break;
							case 93:
								VuDang.isThongTinDeTu = !VuDang.isThongTinDeTu;
								GameScr.info1.addInfo("Thông Tin Đệ Tử: " + (VuDang.isThongTinDeTu ? "Bật" : "Tắt"), 0);
								GameScr.VuDangMenuMod();
								break;
							}
						}
						else
						{
							VuDang.dichChuyenPem = !VuDang.dichChuyenPem;
							GameScr.info1.addInfo("Tele quái: " + (VuDang.dichChuyenPem ? "Bật" : "Tắt"), 0);
							GameScr.VuDangMenuMod();
						}
					}
					else
					{
						Pk9rPickMob.IsVuotDiaHinh = !Pk9rPickMob.IsVuotDiaHinh;
						GameScr.info1.addInfo("Vượt địa hình: " + (Pk9rPickMob.IsVuotDiaHinh ? "Bật" : "Tắt"), 0);
						GameScr.VuDangMenuMod();
					}
					if (idAction <= 11059)
					{
						if (idAction <= 8002)
						{
							if (idAction <= 2)
							{
								if (idAction == 1)
								{
									GameCanvas.endDlg();
									return;
								}
								if (idAction == 2)
								{
									GameCanvas.menu.showMenu = false;
									return;
								}
							}
							else
							{
								switch (idAction)
								{
								case 2000:
									this.popUpYesNo = null;
									GameCanvas.endDlg();
									if ((global::Char)p == null)
									{
										Service.gI().player_vs_player(1, 3, -1);
										return;
									}
									Service.gI().player_vs_player(1, 3, ((global::Char)p).charID);
									Service.gI().charMove();
									return;
								case 2001:
									GameCanvas.endDlg();
									return;
								case 2002:
								case 2008:
									break;
								case 2003:
									GameCanvas.endDlg();
									InfoDlg.showWait();
									Service.gI().player_vs_player(0, 3, global::Char.myCharz().charFocus.charID);
									return;
								case 2004:
									GameCanvas.endDlg();
									Service.gI().player_vs_player(0, 4, global::Char.myCharz().charFocus.charID);
									return;
								case 2005:
									GameCanvas.endDlg();
									this.popUpYesNo = null;
									if ((global::Char)p == null)
									{
										Service.gI().player_vs_player(1, 4, -1);
										return;
									}
									Service.gI().player_vs_player(1, 4, ((global::Char)p).charID);
									return;
								case 2006:
									GameCanvas.endDlg();
									Service.gI().player_vs_player(2, 4, global::Char.myCharz().charFocus.charID);
									return;
								case 2007:
									GameCanvas.endDlg();
									GameMidlet.instance.exit();
									return;
								case 2009:
									this.popUpYesNo = null;
									return;
								default:
									if (idAction == 8002)
									{
										this.doFire(false, true);
										GameCanvas.clearKeyHold();
										GameCanvas.clearKeyPressed();
										return;
									}
									break;
								}
							}
						}
						else if (idAction <= 11038)
						{
							switch (idAction)
							{
							case 11000:
								this.actMenu();
								return;
							case 11001:
								global::Char.myCharz().findNextFocusByKey();
								return;
							case 11002:
								GameCanvas.panel.hide();
								return;
							default:
								if (idAction == 11038)
								{
									this.actDead();
									return;
								}
								break;
							}
						}
						else if (idAction != 11057)
						{
							if (idAction == 11059)
							{
								Skill skill = GameScr.onScreenSkill[this.selectedIndexSkill];
								this.doUseSkill(skill, false);
								this.center = null;
								return;
							}
						}
						else
						{
							Effect2.vEffect2Outside.removeAllElements();
							Effect2.vEffect2.removeAllElements();
							Npc npc = (Npc)p;
							if (npc.idItem == 0)
							{
								Service.gI().confirmMenu((short)npc.template.npcTemplateId, (sbyte)GameCanvas.menu.menuSelectedItem);
								return;
							}
							if (GameCanvas.menu.menuSelectedItem == 0)
							{
								Service.gI().pickItem(npc.idItem);
								return;
							}
						}
					}
					else if (idAction <= 110001)
					{
						if (idAction <= 11121)
						{
							if (idAction != 11067)
							{
								switch (idAction)
								{
								case 11111:
									if (global::Char.myCharz().charFocus != null)
									{
										InfoDlg.showWait();
										if (GameCanvas.panel.vPlayerMenu.size() <= 0)
										{
											this.playerMenu(global::Char.myCharz().charFocus);
										}
										GameCanvas.panel.setTypePlayerMenu(global::Char.myCharz().charFocus);
										GameCanvas.panel.show();
										Service.gI().getPlayerMenu(global::Char.myCharz().charFocus.charID);
										Service.gI().messagePlayerMenu(global::Char.myCharz().charFocus.charID);
										return;
									}
									break;
								case 11112:
								{
									global::Char @char = (global::Char)p;
									Service.gI().friend(1, @char.charID);
									return;
								}
								case 11113:
								{
									global::Char char2 = (global::Char)p;
									if (char2 != null)
									{
										Service.gI().giaodich(0, char2.charID, -1, -1);
										return;
									}
									break;
								}
								case 11114:
								{
									this.popUpYesNo = null;
									GameCanvas.endDlg();
									global::Char char3 = (global::Char)p;
									if (char3 != null)
									{
										Service.gI().giaodich(1, char3.charID, -1, -1);
										return;
									}
									break;
								}
								case 11115:
									if (global::Char.myCharz().charFocus != null)
									{
										InfoDlg.showWait();
										Service.gI().playerMenuAction(global::Char.myCharz().charFocus.charID, (short)global::Char.myCharz().charFocus.menuSelect);
										return;
									}
									break;
								case 11116:
								case 11117:
								case 11118:
								case 11119:
									break;
								case 11120:
								{
									object[] array = (object[])p;
									Skill skill2 = (Skill)array[0];
									int num = int.Parse((string)array[1]);
									for (int j = 0; j < GameScr.onScreenSkill.Length; j++)
									{
										if (GameScr.onScreenSkill[j] == skill2)
										{
											GameScr.onScreenSkill[j] = null;
										}
									}
									GameScr.onScreenSkill[num] = skill2;
									this.saveonScreenSkillToRMS();
									return;
								}
								case 11121:
								{
									object[] array2 = (object[])p;
									Skill skill3 = (Skill)array2[0];
									int num2 = int.Parse((string)array2[1]);
									for (int k = 0; k < GameScr.keySkill.Length; k++)
									{
										if (GameScr.keySkill[k] == skill3)
										{
											GameScr.keySkill[k] = null;
										}
									}
									GameScr.keySkill[num2] = skill3;
									this.saveKeySkillToRMS();
									return;
								}
								default:
									return;
								}
							}
							else
							{
								if (TileMap.zoneID != GameScr.indexSelect)
								{
									Service.gI().requestChangeZone(GameScr.indexSelect, this.indexItemUse);
									InfoDlg.showWait();
									return;
								}
								GameScr.info1.addInfo(mResources.ZONE_HERE, 0);
								return;
							}
						}
						else
						{
							switch (idAction)
							{
							case 12000:
								Service.gI().getClan(1, -1, null);
								return;
							case 12001:
								GameCanvas.endDlg();
								return;
							case 12002:
							{
								GameCanvas.endDlg();
								ClanObject clanObject = (ClanObject)p;
								Service.gI().clanInvite(1, -1, clanObject.clanID, clanObject.code);
								this.popUpYesNo = null;
								return;
							}
							case 12003:
							{
								ClanObject clanObject2 = (ClanObject)p;
								GameCanvas.endDlg();
								Service.gI().clanInvite(2, -1, clanObject2.clanID, clanObject2.code);
								this.popUpYesNo = null;
								return;
							}
							case 12004:
							{
								Skill skill4 = (Skill)p;
								this.doUseSkill(skill4, true);
								global::Char.myCharz().saveLoadPreviousSkill();
								return;
							}
							default:
								if (idAction == 110001)
								{
									GameCanvas.panel.setTypeMain();
									GameCanvas.panel.show();
									return;
								}
								break;
							}
						}
					}
					else if (idAction <= 110382)
					{
						if (idAction == 110004)
						{
							GameCanvas.menu.showMenu = false;
							return;
						}
						if (idAction == 110382)
						{
							Service.gI().returnTownFromDead();
							return;
						}
					}
					else
					{
						if (idAction == 110383)
						{
							Service.gI().wakeUpFromDead();
							return;
						}
						if (idAction == 110391)
						{
							Service.gI().clanInvite(0, global::Char.myCharz().charFocus.charID, -1, -1);
							return;
						}
						if (idAction == 888351)
						{
							Service.gI().petStatus(5);
							GameCanvas.endDlg();
							return;
						}
					}
				}
				else
				{
					MyVector myVector8 = new MyVector();
					myVector8.addElement(new Command(Pk9rPickMob.IsNeSieuQuai ? "Né siêu quái: Bật" : "Né siêu quái: Tắt", 43));
					myVector8.addElement(new Command(Pk9rPickMob.IsVuotDiaHinh ? "Vượt địa hình: Bật" : "Vượt địa hình: Tắt", 76));
					myVector8.addElement(new Command(VuDang.dichChuyenPem ? "Tele quái: Bật" : "Tele quái: Tắt", 80));
					myVector8.addElement(new Command(Pk9rPickMob.IsTanSat ? "TĐLT: Bật" : "TĐLT: Tắt", 44));
					GameCanvas.menu.startAt(myVector8, 4);
				}
				return;
			}
			break;
		}
	}

	// Token: 0x06000342 RID: 834 RVA: 0x0004E7B8 File Offset: 0x0004C9B8
	private static void setTouchBtn()
	{
		if (GameScr.isAnalog != 0)
		{
			GameScr.xTG = (GameScr.xF = GameCanvas.w - 45);
			if (GameScr.gamePad.isLargeGamePad)
			{
				GameScr.xSkill = GameScr.gamePad.wZone + 20;
				GameScr.wSkill = 35;
				GameScr.xHP = GameScr.xF - 45;
			}
			else if (GameScr.gamePad.isMediumGamePad)
			{
				GameScr.xHP = GameScr.xF - 45;
			}
			GameScr.yF = GameCanvas.h - 45;
			GameScr.yTG = GameScr.yF - 45;
		}
	}

	// Token: 0x06000343 RID: 835 RVA: 0x0004E848 File Offset: 0x0004CA48
	private void updateGamePad()
	{
		if (GameScr.isAnalog != 0 && global::Char.myCharz().statusMe != 14)
		{
			if (GameCanvas.isPointerHoldIn(GameScr.xF, GameScr.yF, 40, 40))
			{
				mScreen.keyTouch = 5;
				if (GameCanvas.isPointerJustRelease)
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = true;
					GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
				}
			}
			GameScr.gamePad.update();
			if (GameCanvas.isPointerHoldIn(GameScr.xTG, GameScr.yTG, 34, 34))
			{
				mScreen.keyTouch = 13;
				GameCanvas.isPointerJustDown = false;
				this.isPointerDowning = false;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					global::Char.myCharz().findNextFocusByKey();
					GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
				}
			}
		}
	}

	// Token: 0x06000344 RID: 836 RVA: 0x0004E914 File Offset: 0x0004CB14
	private void paintGamePad(mGraphics g)
	{
		if (GameScr.isAnalog != 0 && global::Char.myCharz().statusMe != 14)
		{
			g.drawImage((mScreen.keyTouch != 5 && mScreen.keyMouse != 5) ? GameScr.imgFire0 : GameScr.imgFire1, GameScr.xF + 20, GameScr.yF + 20, mGraphics.HCENTER | mGraphics.VCENTER);
			GameScr.gamePad.paint(g);
			g.drawImage((mScreen.keyTouch != 13) ? GameScr.imgFocus : GameScr.imgFocus2, GameScr.xTG + 20, GameScr.yTG + 20, mGraphics.HCENTER | mGraphics.VCENTER);
		}
	}

	// Token: 0x06000345 RID: 837 RVA: 0x0004E9B8 File Offset: 0x0004CBB8
	public void showWinNumber(string num, string finish)
	{
		this.winnumber = new int[num.Length];
		this.randomNumber = new int[num.Length];
		this.tMove = new int[num.Length];
		this.moveCount = new int[num.Length];
		this.delayMove = new int[num.Length];
		for (int i = 0; i < num.Length; i++)
		{
			this.winnumber[i] = (int)((short)num[i]);
			this.randomNumber[i] = Res.random(0, 11);
			this.tMove[i] = 1;
			this.delayMove[i] = 0;
		}
		this.tShow = 100;
		this.moveIndex = 0;
		this.strFinish = finish;
		GameScr.lastXS = (GameScr.currXS = mSystem.currentTimeMillis());
	}

	// Token: 0x06000346 RID: 838 RVA: 0x0004EA84 File Offset: 0x0004CC84
	public void chatVip(string chatVip)
	{
		if (!this.startChat)
		{
			this.currChatWidth = mFont.tahoma_7b_yellowSmall.getWidth(chatVip);
			this.xChatVip = GameCanvas.w;
			this.startChat = true;
		}
		if (chatVip.StartsWith("!"))
		{
			chatVip = chatVip.Substring(1, chatVip.Length);
			this.isFireWorks = true;
		}
		GameScr.vChatVip.addElement(chatVip);
		if (chatVip.Trim().ToLower().Contains("boss"))
		{
			VuDang.bossVip.addElement(new ShowBoss(chatVip));
			if (VuDang.bossVip.size() > 5)
			{
				VuDang.bossVip.removeElementAt(0);
			}
		}
	}

	// Token: 0x06000347 RID: 839 RVA: 0x000046D3 File Offset: 0x000028D3
	public void clearChatVip()
	{
		GameScr.vChatVip.removeAllElements();
		this.xChatVip = GameCanvas.w;
		this.startChat = false;
	}

	// Token: 0x06000348 RID: 840 RVA: 0x0004EB2C File Offset: 0x0004CD2C
	public void paintChatVip(mGraphics g)
	{
		if (GameScr.vChatVip.size() != 0 && GameScr.isPaintChatVip)
		{
			g.setClip(0, GameCanvas.h - 13, GameCanvas.w, 15);
			g.fillRect(0, GameCanvas.h - 13, GameCanvas.w, 15, 0, 90);
			string st = (string)GameScr.vChatVip.elementAt(0);
			mFont.tahoma_7b_yellow.drawString(g, st, this.xChatVip, GameCanvas.h - 13, 0, mFont.tahoma_7b_dark);
		}
	}

	// Token: 0x06000349 RID: 841 RVA: 0x0004EBAC File Offset: 0x0004CDAC
	public void updateChatVip()
	{
		if (this.startChat)
		{
			this.xChatVip -= 2;
			if (this.xChatVip < -this.currChatWidth)
			{
				this.xChatVip = GameCanvas.w;
				GameScr.vChatVip.removeElementAt(0);
				if (GameScr.vChatVip.size() == 0)
				{
					this.isFireWorks = false;
					this.startChat = false;
					return;
				}
				this.currChatWidth = mFont.tahoma_7b_white.getWidth((string)GameScr.vChatVip.elementAt(0));
			}
		}
	}

	// Token: 0x0600034A RID: 842 RVA: 0x000046F1 File Offset: 0x000028F1
	public void showYourNumber(string strNum)
	{
		this.yourNumber = strNum;
		this.strPaint = mFont.tahoma_7.splitFontArray(this.yourNumber, 500);
	}

	// Token: 0x0600034B RID: 843 RVA: 0x00004715 File Offset: 0x00002915
	public static void checkRemoveImage()
	{
		ImgByName.checkDelHash(ImgByName.hashImagePath, 10, false);
	}

	// Token: 0x0600034C RID: 844 RVA: 0x0004EC30 File Offset: 0x0004CE30
	public static void StartServerPopUp(string strMsg)
	{
		GameCanvas.endDlg();
		int avatar = 1139;
		ChatPopup.addBigMessage(strMsg, 100000, new Npc(-1, 0, 0, 0, 0, 0)
		{
			avatar = avatar
		});
		ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
		ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 35;
		ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
	}

	// Token: 0x0600034D RID: 845 RVA: 0x00004724 File Offset: 0x00002924
	public static bool ispaintPhubangBar()
	{
		return TileMap.mapPhuBang() && GameScr.phuban_Info.type_PB == 0;
	}

	// Token: 0x0600034E RID: 846 RVA: 0x0004ECB4 File Offset: 0x0004CEB4
	public void paintPhuBanBar(mGraphics g, int x, int y, int w)
	{
		if (GameScr.phuban_Info != null && !GameScr.isPaintOther && GameScr.isPaintRada == 1 && !GameCanvas.panel.isShow && GameScr.ispaintPhubangBar())
		{
			if (w < GameScr.fra_PVE_Bar_1.frameWidth + GameScr.fra_PVE_Bar_0.frameWidth * 4)
			{
				w = GameScr.fra_PVE_Bar_1.frameWidth + GameScr.fra_PVE_Bar_0.frameWidth * 4;
			}
			if (x > GameCanvas.w - w / 2)
			{
				x = GameCanvas.w - w / 2;
			}
			if (x < mGraphics.getImageWidth(GameScr.imgKhung) + w / 2 + 10)
			{
				x = mGraphics.getImageWidth(GameScr.imgKhung) + w / 2 + 10;
			}
			int frameHeight = GameScr.fra_PVE_Bar_0.frameHeight;
			int num = y + frameHeight + GameScr.imgBall.getHeight() / 2 + 2;
			int frameWidth = GameScr.fra_PVE_Bar_1.frameWidth;
			int num2 = w / 2 - frameWidth / 2;
			int num3 = x - w / 2;
			int num4 = x + frameWidth / 2;
			int y2 = y + 3;
			int num5 = num2 - GameScr.fra_PVE_Bar_0.frameWidth;
			int num6 = num5 / GameScr.fra_PVE_Bar_0.frameWidth;
			if (num5 % GameScr.fra_PVE_Bar_0.frameWidth > 0)
			{
				num6++;
			}
			for (int i = 0; i < num6; i++)
			{
				if (i < num6 - 1)
				{
					GameScr.fra_PVE_Bar_0.drawFrame(1, num3 + GameScr.fra_PVE_Bar_0.frameWidth + i * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
				}
				else
				{
					GameScr.fra_PVE_Bar_0.drawFrame(1, num3 + num5, y2, 0, 0, g);
				}
				if (i < num6 - 1)
				{
					GameScr.fra_PVE_Bar_0.drawFrame(1, num4 + i * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
				}
				else
				{
					GameScr.fra_PVE_Bar_0.drawFrame(1, num4 + num5 - GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
				}
			}
			GameScr.fra_PVE_Bar_0.drawFrame(0, num3, y2, 2, 0, g);
			GameScr.fra_PVE_Bar_0.drawFrame(0, num4 + num5, y2, 0, 0, g);
			if (GameScr.phuban_Info.pointTeam1 > 0)
			{
				int idx = 2;
				int idx2 = 3;
				if (GameScr.phuban_Info.color_1 == 4)
				{
					idx = 4;
					idx2 = 5;
				}
				int num7 = GameScr.phuban_Info.pointTeam1 * num2 / GameScr.phuban_Info.maxPoint;
				if (num7 < 0)
				{
					num7 = 0;
				}
				if (num7 > num2)
				{
					num7 = num2;
				}
				g.setClip(num3 + num2 - num7, y2, num7, frameHeight);
				for (int j = 0; j < num6; j++)
				{
					if (j < num6 - 1)
					{
						GameScr.fra_PVE_Bar_0.drawFrame(idx2, num3 + GameScr.fra_PVE_Bar_0.frameWidth + j * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
					}
					else
					{
						GameScr.fra_PVE_Bar_0.drawFrame(idx2, num3 + num5, y2, 0, 0, g);
					}
				}
				GameScr.fra_PVE_Bar_0.drawFrame(idx, num3, y2, 2, 0, g);
				GameCanvas.resetTrans(g);
			}
			if (GameScr.phuban_Info.pointTeam2 > 0)
			{
				int idx3 = 2;
				int idx4 = 3;
				if (GameScr.phuban_Info.color_2 == 4)
				{
					idx3 = 4;
					idx4 = 5;
				}
				int num8 = GameScr.phuban_Info.pointTeam2 * num2 / GameScr.phuban_Info.maxPoint;
				if (num8 < 0)
				{
					num8 = 0;
				}
				if (num8 > num2)
				{
					num8 = num2;
				}
				g.setClip(num4, y2, num8, frameHeight);
				for (int k = 0; k < num6; k++)
				{
					if (k < num6 - 1)
					{
						GameScr.fra_PVE_Bar_0.drawFrame(idx4, num4 + k * GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
					}
					else
					{
						GameScr.fra_PVE_Bar_0.drawFrame(idx4, num4 + num5 - GameScr.fra_PVE_Bar_0.frameWidth, y2, 0, 0, g);
					}
				}
				GameScr.fra_PVE_Bar_0.drawFrame(idx3, num4 + num5, y2, 0, 0, g);
				GameCanvas.resetTrans(g);
			}
			GameScr.fra_PVE_Bar_1.drawFrame(0, x - frameWidth / 2, y, 0, 0, g);
			string timeCountDown = mSystem.getTimeCountDown(GameScr.phuban_Info.timeStart, (int)GameScr.phuban_Info.timeSecond, true, false);
			mFont.tahoma_7b_yellow.drawString(g, timeCountDown, x + 1, y + GameScr.fra_PVE_Bar_1.frameHeight / 2 - mFont.tahoma_7b_green2.getHeight() / 2, 2);
			Panel.setTextColor(GameScr.phuban_Info.color_1, 1).drawString(g, GameScr.phuban_Info.nameTeam1, x - 5, num + 5, 1);
			Panel.setTextColor(GameScr.phuban_Info.color_2, 1).drawString(g, GameScr.phuban_Info.nameTeam2, x + 5, num + 5, 0);
			if (GameScr.phuban_Info.type_PB != 0)
			{
				int y3 = y + frameHeight / 2 - 2;
				mFont.bigNumber_While.drawString(g, string.Empty + GameScr.phuban_Info.pointTeam1, num3 + num2 / 2, y3, 2);
				mFont.bigNumber_While.drawString(g, string.Empty + GameScr.phuban_Info.pointTeam2, num4 + num2 / 2, y3, 2);
			}
			g.drawImage(GameScr.imgVS, x, y + GameScr.fra_PVE_Bar_1.frameHeight + 2, 3);
			if (GameScr.phuban_Info.type_PB == 0)
			{
				GameScr.paintChienTruong_Life(g, GameScr.phuban_Info.maxLife, GameScr.phuban_Info.color_1, GameScr.phuban_Info.lifeTeam1, x - 13, GameScr.phuban_Info.color_2, GameScr.phuban_Info.lifeTeam2, x + 13, num);
			}
		}
	}

	// Token: 0x0600034F RID: 847 RVA: 0x0004F1F0 File Offset: 0x0004D3F0
	public static void paintChienTruong_Life(mGraphics g, int maxLife, int cl1, int lifeTeam1, int x1, int cl2, int lifeTeam2, int x2, int y)
	{
		if (GameScr.imgBall != null)
		{
			int num = GameScr.imgBall.getHeight() / 2;
			for (int i = 0; i < maxLife; i++)
			{
				int num2 = 0;
				if (i < lifeTeam1)
				{
					num2 = 1;
				}
				g.drawRegion(GameScr.imgBall, 0, num2 * num, GameScr.imgBall.getWidth(), num, 0, x1 - i * (num + 1), y, mGraphics.VCENTER | mGraphics.HCENTER);
			}
			for (int j = 0; j < maxLife; j++)
			{
				int num3 = 0;
				if (j < lifeTeam2)
				{
					num3 = 1;
				}
				g.drawRegion(GameScr.imgBall, 0, num3 * num, GameScr.imgBall.getWidth(), num, 0, x2 + j * (num + 1), y, mGraphics.VCENTER | mGraphics.HCENTER);
			}
		}
	}

	// Token: 0x06000350 RID: 848 RVA: 0x0004F2A0 File Offset: 0x0004D4A0
	public static void paintHPBar_NEW(mGraphics g, int x, int y, global::Char c)
	{
		g.drawImage(GameScr.imgKhung, x, y, 0);
		int x2 = x + 3;
		int num = y + 19;
		int width = GameScr.imgHP_NEW.getWidth();
		int num2 = GameScr.imgHP_NEW.getHeight() / 2;
		int num3 = c.cHP * width / c.cHPFull;
		if (num3 <= 0)
		{
			num3 = 1;
		}
		else if (num3 > width)
		{
			num3 = width;
		}
		g.drawRegion(GameScr.imgHP_NEW, 0, num2, num3, num2, 0, x2, num, 0);
		int num4 = c.cMP * width / c.cMPFull;
		if (num4 <= 0)
		{
			num4 = 1;
		}
		else if (num4 > width)
		{
			num4 = width;
		}
		g.drawRegion(GameScr.imgHP_NEW, 0, 0, num4, num2, 0, x2, num + 6, 0);
		int x3 = x + GameScr.imgKhung.getWidth() / 2 + 1;
		int y2 = num + 13;
		mFont.tahoma_7_green2.drawString(g, c.cName, x3, y + 4, 2);
		if (c.mobFocus != null)
		{
			if (c.mobFocus.getTemplate() != null)
			{
				mFont.tahoma_7_green2.drawString(g, c.mobFocus.getTemplate().name, x3, y2, 2);
				return;
			}
		}
		else
		{
			if (c.npcFocus != null)
			{
				mFont.tahoma_7_green2.drawString(g, c.npcFocus.template.name, x3, y2, 2);
				return;
			}
			if (c.charFocus != null)
			{
				mFont.tahoma_7_green2.drawString(g, c.charFocus.cName, x3, y2, 2);
			}
		}
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0000473C File Offset: 0x0000293C
	public static void addEffectEnd(int type, int subtype, int x, int y, int levelPaint, int dir)
	{
		GameScr.addEffect2Vector(new Effect_End(type, subtype, x, y, levelPaint, dir));
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00004750 File Offset: 0x00002950
	public static void addEffect2Vector(Effect_End eff)
	{
		if (eff.levelPaint == 0)
		{
			EffectManager.addHiEffect(eff);
			return;
		}
		if (eff.levelPaint == 1)
		{
			EffectManager.addMidEffects(eff);
			return;
		}
		EffectManager.addLowEffect(eff);
	}

	// Token: 0x06000353 RID: 851 RVA: 0x00004777 File Offset: 0x00002977
	public static bool setIsInScreen(int x, int y, int wOne, int hOne)
	{
		return x >= GameScr.cmx - wOne && x <= GameScr.cmx + GameCanvas.w + wOne && y >= GameScr.cmy - hOne && y <= GameScr.cmy + GameCanvas.h + hOne * 3 / 2;
	}

	// Token: 0x06000354 RID: 852 RVA: 0x000047B6 File Offset: 0x000029B6
	public static bool isSmallScr()
	{
		return GameCanvas.w <= 320;
	}

	// Token: 0x06000355 RID: 853 RVA: 0x0004F400 File Offset: 0x0004D600
	public static void VuDangMenuMod()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(VuDang.dau, 14));
		myVector.addElement(new Command(VuDang.autoSkill, 89));
		myVector.addElement(new Command(VuDang.doHoa, 15));
		myVector.addElement(new Command(VuDang.hoTroUpDe, 16));
		myVector.addElement(new Command(VuDang.upYardrat, 31));
		myVector.addElement(new Command(VuDang.hoTroSanBoss, 32));
		myVector.addElement(new Command(VuDang.tdLT, 45));
		myVector.addElement(new Command(VuDang.chucNangKhac, 33));
		GameCanvas.menu.startAt(myVector, 4);
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0004F4B0 File Offset: 0x0004D6B0
	static GameScr()
	{
		GameScr.isPaintOther = false;
		GameScr.textTime = new MyVector(string.Empty);
		GameScr.isLoadAllData = false;
		GameScr.vClan = new MyVector();
		GameScr.vPtMap = new MyVector();
		GameScr.vFriend = new MyVector();
		GameScr.vEnemies = new MyVector();
		GameScr.vCharInMap = new MyVector();
		GameScr.vItemMap = new MyVector();
		GameScr.vMobAttack = new MyVector();
		GameScr.vSet = new MyVector();
		GameScr.vMob = new MyVector();
		GameScr.vNpc = new MyVector();
		GameScr.vFlag = new MyVector();
		GameScr.indexSize = 28;
		GameScr.indexTitle = 0;
		GameScr.indexSelect = 0;
		GameScr.indexRow = -1;
		GameScr.indexMenu = 0;
		GameScr.scrInfo = new Scroll();
		GameScr.scrMain = new Scroll();
		GameScr.vItemUpGrade = new MyVector();
		GameScr.isViewClanMemOnline = false;
		GameScr.isViewClanInvite = true;
		GameScr.titleInputText = string.Empty;
		GameScr.isPaintAlert = false;
		GameScr.isPaintTask = false;
		GameScr.isPaintTeam = false;
		GameScr.isPaintFindTeam = false;
		GameScr.isPaintFriend = false;
		GameScr.isPaintEnemies = false;
		GameScr.isPaintItemInfo = false;
		GameScr.isHaveSelectSkill = false;
		GameScr.isPaintSkill = false;
		GameScr.isPaintInfoMe = false;
		GameScr.isPaintStore = false;
		GameScr.isPaintNonNam = false;
		GameScr.isPaintNonNu = false;
		GameScr.isPaintAoNam = false;
		GameScr.isPaintAoNu = false;
		GameScr.isPaintGangTayNam = false;
		GameScr.isPaintGangTayNu = false;
		GameScr.isPaintQuanNam = false;
		GameScr.isPaintQuanNu = false;
		GameScr.isPaintGiayNam = false;
		GameScr.isPaintGiayNu = false;
		GameScr.isPaintLien = false;
		GameScr.isPaintNhan = false;
		GameScr.isPaintNgocBoi = false;
		GameScr.isPaintPhu = false;
		GameScr.isPaintWeapon = false;
		GameScr.isPaintStack = false;
		GameScr.isPaintStackLock = false;
		GameScr.isPaintGrocery = false;
		GameScr.isPaintGroceryLock = false;
		GameScr.isPaintUpGrade = false;
		GameScr.isPaintConvert = false;
		GameScr.isPaintUpGradeGold = false;
		GameScr.isPaintUpPearl = false;
		GameScr.isPaintBox = false;
		GameScr.isPaintSplit = false;
		GameScr.isPaintCharInMap = false;
		GameScr.isPaintTrade = false;
		GameScr.isPaintZone = false;
		GameScr.isPaintMessage = false;
		GameScr.isPaintClan = false;
		GameScr.isRequestMember = false;
		GameScr.typeViewInfo = 0;
		GameScr.typeActive = 0;
		GameScr.info1 = new InfoMe();
		GameScr.info2 = new InfoMe();
		GameScr.gamePad = new GamePad();
		GameScr.isAnalog = 0;
		GameScr.keySkill = new Skill[10];
		GameScr.onScreenSkill = new Skill[10];
		GameScr.tam = 0;
		GameScr.isPaint = true;
		GameScr.shock_x = new int[]
		{
			3,
			-3,
			3,
			-3
		};
		GameScr.shock_y = new int[]
		{
			3,
			-3,
			-3,
			3
		};
		GameScr.popupW = 140;
		GameScr.popupH = 160;
		GameScr.columns = 6;
		GameScr.indexEff = 0;
		GameScr.notPaint = false;
		GameScr.isPing = false;
		GameScr.INFO = 0;
		GameScr.STORE = 1;
		GameScr.ZONE = 2;
		GameScr.UPGRADE = 3;
		GameScr.vChatVip = new MyVector();
	}

	// Token: 0x0400062C RID: 1580
	public static bool isPaintOther;

	// Token: 0x0400062D RID: 1581
	public static MyVector textTime;

	// Token: 0x0400062E RID: 1582
	public static bool isLoadAllData;

	// Token: 0x0400062F RID: 1583
	public static GameScr instance;

	// Token: 0x04000630 RID: 1584
	public static int gW;

	// Token: 0x04000631 RID: 1585
	public static int gH;

	// Token: 0x04000632 RID: 1586
	public static int gW2;

	// Token: 0x04000633 RID: 1587
	public static int gssw;

	// Token: 0x04000634 RID: 1588
	public static int gssh;

	// Token: 0x04000635 RID: 1589
	public static int gH34;

	// Token: 0x04000636 RID: 1590
	public static int gW3;

	// Token: 0x04000637 RID: 1591
	public static int gH3;

	// Token: 0x04000638 RID: 1592
	public static int gH23;

	// Token: 0x04000639 RID: 1593
	public static int gW23;

	// Token: 0x0400063A RID: 1594
	public static int gH2;

	// Token: 0x0400063B RID: 1595
	public static int csPadMaxH;

	// Token: 0x0400063C RID: 1596
	public static int cmdBarH;

	// Token: 0x0400063D RID: 1597
	public static int gW34;

	// Token: 0x0400063E RID: 1598
	public static int gW6;

	// Token: 0x0400063F RID: 1599
	public static int gH6;

	// Token: 0x04000640 RID: 1600
	public static int cmx;

	// Token: 0x04000641 RID: 1601
	public static int cmy;

	// Token: 0x04000642 RID: 1602
	public static int cmdx;

	// Token: 0x04000643 RID: 1603
	public static int cmdy;

	// Token: 0x04000644 RID: 1604
	public static int cmvx;

	// Token: 0x04000645 RID: 1605
	public static int cmvy;

	// Token: 0x04000646 RID: 1606
	public static int cmtoX;

	// Token: 0x04000647 RID: 1607
	public static int cmtoY;

	// Token: 0x04000648 RID: 1608
	public static int cmxLim;

	// Token: 0x04000649 RID: 1609
	public static int cmyLim;

	// Token: 0x0400064A RID: 1610
	public static int gssx;

	// Token: 0x0400064B RID: 1611
	public static int gssy;

	// Token: 0x0400064C RID: 1612
	public static int gssxe;

	// Token: 0x0400064D RID: 1613
	public static int gssye;

	// Token: 0x0400064E RID: 1614
	public Command cmdback;

	// Token: 0x0400064F RID: 1615
	public Command cmdBag;

	// Token: 0x04000650 RID: 1616
	public Command cmdSkill;

	// Token: 0x04000651 RID: 1617
	public Command cmdTiemnang;

	// Token: 0x04000652 RID: 1618
	public Command cmdtrangbi;

	// Token: 0x04000653 RID: 1619
	public Command cmdInfo;

	// Token: 0x04000654 RID: 1620
	public Command cmdFocus;

	// Token: 0x04000655 RID: 1621
	public Command cmdFire;

	// Token: 0x04000656 RID: 1622
	public static int d;

	// Token: 0x04000657 RID: 1623
	public static int hpPotion;

	// Token: 0x04000658 RID: 1624
	public static SkillPaint[] sks;

	// Token: 0x04000659 RID: 1625
	public static Arrowpaint[] arrs;

	// Token: 0x0400065A RID: 1626
	public static DartInfo[] darts;

	// Token: 0x0400065B RID: 1627
	public static Part[] parts;

	// Token: 0x0400065C RID: 1628
	public static EffectCharPaint[] efs;

	// Token: 0x0400065D RID: 1629
	public static int lockTick;

	// Token: 0x0400065E RID: 1630
	private int moveUp;

	// Token: 0x0400065F RID: 1631
	private int moveDow;

	// Token: 0x04000660 RID: 1632
	private int idTypeTask;

	// Token: 0x04000661 RID: 1633
	private bool isstarOpen;

	// Token: 0x04000662 RID: 1634
	private bool isChangeSkill;

	// Token: 0x04000663 RID: 1635
	public static MyVector vClan;

	// Token: 0x04000664 RID: 1636
	public static MyVector vPtMap;

	// Token: 0x04000665 RID: 1637
	public static MyVector vFriend;

	// Token: 0x04000666 RID: 1638
	public static MyVector vEnemies;

	// Token: 0x04000667 RID: 1639
	public static MyVector vCharInMap;

	// Token: 0x04000668 RID: 1640
	public static MyVector vItemMap;

	// Token: 0x04000669 RID: 1641
	public static MyVector vMobAttack;

	// Token: 0x0400066A RID: 1642
	public static MyVector vSet;

	// Token: 0x0400066B RID: 1643
	public static MyVector vMob;

	// Token: 0x0400066C RID: 1644
	public static MyVector vNpc;

	// Token: 0x0400066D RID: 1645
	public static MyVector vFlag;

	// Token: 0x0400066E RID: 1646
	public static NClass[] nClasss;

	// Token: 0x0400066F RID: 1647
	public static int indexSize;

	// Token: 0x04000670 RID: 1648
	public static int indexTitle;

	// Token: 0x04000671 RID: 1649
	public static int indexSelect;

	// Token: 0x04000672 RID: 1650
	public static int indexRow;

	// Token: 0x04000673 RID: 1651
	public static int indexRowMax;

	// Token: 0x04000674 RID: 1652
	public static int indexMenu;

	// Token: 0x04000675 RID: 1653
	public Item itemFocus;

	// Token: 0x04000676 RID: 1654
	public ItemOptionTemplate[] iOptionTemplates;

	// Token: 0x04000677 RID: 1655
	public SkillOptionTemplate[] sOptionTemplates;

	// Token: 0x04000678 RID: 1656
	private static Scroll scrInfo;

	// Token: 0x04000679 RID: 1657
	public static Scroll scrMain;

	// Token: 0x0400067A RID: 1658
	public static MyVector vItemUpGrade;

	// Token: 0x0400067B RID: 1659
	public static bool isTypeXu;

	// Token: 0x0400067C RID: 1660
	public static bool isViewNext;

	// Token: 0x0400067D RID: 1661
	public static bool isViewClanMemOnline;

	// Token: 0x0400067E RID: 1662
	public static bool isViewClanInvite;

	// Token: 0x0400067F RID: 1663
	public static bool isChop;

	// Token: 0x04000680 RID: 1664
	public static string titleInputText;

	// Token: 0x04000681 RID: 1665
	public static int tickMove;

	// Token: 0x04000682 RID: 1666
	public static bool isPaintAlert;

	// Token: 0x04000683 RID: 1667
	public static bool isPaintTask;

	// Token: 0x04000684 RID: 1668
	public static bool isPaintTeam;

	// Token: 0x04000685 RID: 1669
	public static bool isPaintFindTeam;

	// Token: 0x04000686 RID: 1670
	public static bool isPaintFriend;

	// Token: 0x04000687 RID: 1671
	public static bool isPaintEnemies;

	// Token: 0x04000688 RID: 1672
	public static bool isPaintItemInfo;

	// Token: 0x04000689 RID: 1673
	public static bool isHaveSelectSkill;

	// Token: 0x0400068A RID: 1674
	public static bool isPaintSkill;

	// Token: 0x0400068B RID: 1675
	public static bool isPaintInfoMe;

	// Token: 0x0400068C RID: 1676
	public static bool isPaintStore;

	// Token: 0x0400068D RID: 1677
	public static bool isPaintNonNam;

	// Token: 0x0400068E RID: 1678
	public static bool isPaintNonNu;

	// Token: 0x0400068F RID: 1679
	public static bool isPaintAoNam;

	// Token: 0x04000690 RID: 1680
	public static bool isPaintAoNu;

	// Token: 0x04000691 RID: 1681
	public static bool isPaintGangTayNam;

	// Token: 0x04000692 RID: 1682
	public static bool isPaintGangTayNu;

	// Token: 0x04000693 RID: 1683
	public static bool isPaintQuanNam;

	// Token: 0x04000694 RID: 1684
	public static bool isPaintQuanNu;

	// Token: 0x04000695 RID: 1685
	public static bool isPaintGiayNam;

	// Token: 0x04000696 RID: 1686
	public static bool isPaintGiayNu;

	// Token: 0x04000697 RID: 1687
	public static bool isPaintLien;

	// Token: 0x04000698 RID: 1688
	public static bool isPaintNhan;

	// Token: 0x04000699 RID: 1689
	public static bool isPaintNgocBoi;

	// Token: 0x0400069A RID: 1690
	public static bool isPaintPhu;

	// Token: 0x0400069B RID: 1691
	public static bool isPaintWeapon;

	// Token: 0x0400069C RID: 1692
	public static bool isPaintStack;

	// Token: 0x0400069D RID: 1693
	public static bool isPaintStackLock;

	// Token: 0x0400069E RID: 1694
	public static bool isPaintGrocery;

	// Token: 0x0400069F RID: 1695
	public static bool isPaintGroceryLock;

	// Token: 0x040006A0 RID: 1696
	public static bool isPaintUpGrade;

	// Token: 0x040006A1 RID: 1697
	public static bool isPaintConvert;

	// Token: 0x040006A2 RID: 1698
	public static bool isPaintUpGradeGold;

	// Token: 0x040006A3 RID: 1699
	public static bool isPaintUpPearl;

	// Token: 0x040006A4 RID: 1700
	public static bool isPaintBox;

	// Token: 0x040006A5 RID: 1701
	public static bool isPaintSplit;

	// Token: 0x040006A6 RID: 1702
	public static bool isPaintCharInMap;

	// Token: 0x040006A7 RID: 1703
	public static bool isPaintTrade;

	// Token: 0x040006A8 RID: 1704
	public static bool isPaintZone;

	// Token: 0x040006A9 RID: 1705
	public static bool isPaintMessage;

	// Token: 0x040006AA RID: 1706
	public static bool isPaintClan;

	// Token: 0x040006AB RID: 1707
	public static bool isRequestMember;

	// Token: 0x040006AC RID: 1708
	public static global::Char currentCharViewInfo;

	// Token: 0x040006AD RID: 1709
	public static long[] exps;

	// Token: 0x040006AE RID: 1710
	public static int[] crystals;

	// Token: 0x040006AF RID: 1711
	public static int[] upClothe;

	// Token: 0x040006B0 RID: 1712
	public static int[] upAdorn;

	// Token: 0x040006B1 RID: 1713
	public static int[] upWeapon;

	// Token: 0x040006B2 RID: 1714
	public static int[] coinUpCrystals;

	// Token: 0x040006B3 RID: 1715
	public static int[] coinUpClothes;

	// Token: 0x040006B4 RID: 1716
	public static int[] coinUpAdorns;

	// Token: 0x040006B5 RID: 1717
	public static int[] coinUpWeapons;

	// Token: 0x040006B6 RID: 1718
	public static int[] maxPercents;

	// Token: 0x040006B7 RID: 1719
	public static int[] goldUps;

	// Token: 0x040006B8 RID: 1720
	public int tMenuDelay;

	// Token: 0x040006B9 RID: 1721
	public int zoneCol = 6;

	// Token: 0x040006BA RID: 1722
	public int[] zones;

	// Token: 0x040006BB RID: 1723
	public int[] pts;

	// Token: 0x040006BC RID: 1724
	public int[] numPlayer;

	// Token: 0x040006BD RID: 1725
	public int[] maxPlayer;

	// Token: 0x040006BE RID: 1726
	public int[] rank1;

	// Token: 0x040006BF RID: 1727
	public int[] rank2;

	// Token: 0x040006C0 RID: 1728
	public string[] rankName1;

	// Token: 0x040006C1 RID: 1729
	public string[] rankName2;

	// Token: 0x040006C2 RID: 1730
	public int typeTrade;

	// Token: 0x040006C3 RID: 1731
	public int typeTradeOrder;

	// Token: 0x040006C4 RID: 1732
	public int coinTrade;

	// Token: 0x040006C5 RID: 1733
	public int coinTradeOrder;

	// Token: 0x040006C6 RID: 1734
	public int timeTrade;

	// Token: 0x040006C7 RID: 1735
	public int indexItemUse = -1;

	// Token: 0x040006C8 RID: 1736
	public int cLastFocusID = -1;

	// Token: 0x040006C9 RID: 1737
	public int cPreFocusID = -1;

	// Token: 0x040006CA RID: 1738
	public bool isLockKey;

	// Token: 0x040006CB RID: 1739
	public static int[] tasks;

	// Token: 0x040006CC RID: 1740
	public static int[] mapTasks;

	// Token: 0x040006CD RID: 1741
	public static Image imgRoomStat;

	// Token: 0x040006CE RID: 1742
	public static Image frBarPow0;

	// Token: 0x040006CF RID: 1743
	public static Image frBarPow1;

	// Token: 0x040006D0 RID: 1744
	public static Image frBarPow2;

	// Token: 0x040006D1 RID: 1745
	public static Image frBarPow20;

	// Token: 0x040006D2 RID: 1746
	public static Image frBarPow21;

	// Token: 0x040006D3 RID: 1747
	public static Image frBarPow22;

	// Token: 0x040006D4 RID: 1748
	public MyVector texts;

	// Token: 0x040006D5 RID: 1749
	public string textsTitle;

	// Token: 0x040006D6 RID: 1750
	public static sbyte vcData;

	// Token: 0x040006D7 RID: 1751
	public static sbyte vcMap;

	// Token: 0x040006D8 RID: 1752
	public static sbyte vcSkill;

	// Token: 0x040006D9 RID: 1753
	public static sbyte vcItem;

	// Token: 0x040006DA RID: 1754
	public static sbyte vsData;

	// Token: 0x040006DB RID: 1755
	public static sbyte vsMap;

	// Token: 0x040006DC RID: 1756
	public static sbyte vsSkill;

	// Token: 0x040006DD RID: 1757
	public static sbyte vsItem;

	// Token: 0x040006DE RID: 1758
	public static sbyte vcTask;

	// Token: 0x040006DF RID: 1759
	public static Image imgArrow;

	// Token: 0x040006E0 RID: 1760
	public static Image imgArrow2;

	// Token: 0x040006E1 RID: 1761
	public static Image imgChat;

	// Token: 0x040006E2 RID: 1762
	public static Image imgChat2;

	// Token: 0x040006E3 RID: 1763
	public static Image imgMenu;

	// Token: 0x040006E4 RID: 1764
	public static Image imgFocus;

	// Token: 0x040006E5 RID: 1765
	public static Image imgFocus2;

	// Token: 0x040006E6 RID: 1766
	public static Image imgSkill;

	// Token: 0x040006E7 RID: 1767
	public static Image imgSkill2;

	// Token: 0x040006E8 RID: 1768
	public static Image imgHP1;

	// Token: 0x040006E9 RID: 1769
	public static Image imgHP2;

	// Token: 0x040006EA RID: 1770
	public static Image imgHP3;

	// Token: 0x040006EB RID: 1771
	public static Image imgHP4;

	// Token: 0x040006EC RID: 1772
	public static Image imgFire0;

	// Token: 0x040006ED RID: 1773
	public static Image imgFire1;

	// Token: 0x040006EE RID: 1774
	public static Image imgLbtn;

	// Token: 0x040006EF RID: 1775
	public static Image imgLbtnFocus;

	// Token: 0x040006F0 RID: 1776
	public static Image imgLbtn2;

	// Token: 0x040006F1 RID: 1777
	public static Image imgLbtnFocus2;

	// Token: 0x040006F2 RID: 1778
	public static Image imgAnalog1;

	// Token: 0x040006F3 RID: 1779
	public static Image imgAnalog2;

	// Token: 0x040006F4 RID: 1780
	public string tradeName = string.Empty;

	// Token: 0x040006F5 RID: 1781
	public string tradeItemName = string.Empty;

	// Token: 0x040006F6 RID: 1782
	public int timeLengthMap;

	// Token: 0x040006F7 RID: 1783
	public int timeStartMap;

	// Token: 0x040006F8 RID: 1784
	public static sbyte typeViewInfo;

	// Token: 0x040006F9 RID: 1785
	public static sbyte typeActive;

	// Token: 0x040006FA RID: 1786
	public static InfoMe info1;

	// Token: 0x040006FB RID: 1787
	public static InfoMe info2;

	// Token: 0x040006FC RID: 1788
	public static Image imgPanel;

	// Token: 0x040006FD RID: 1789
	public static Image imgPanel2;

	// Token: 0x040006FE RID: 1790
	public static Image imgHP;

	// Token: 0x040006FF RID: 1791
	public static Image imgMP;

	// Token: 0x04000700 RID: 1792
	public static Image imgSP;

	// Token: 0x04000701 RID: 1793
	public static Image imgHPLost;

	// Token: 0x04000702 RID: 1794
	public static Image imgMPLost;

	// Token: 0x04000703 RID: 1795
	public Mob mobCapcha;

	// Token: 0x04000704 RID: 1796
	public MagicTree magicTree;

	// Token: 0x04000705 RID: 1797
	public static int countEff;

	// Token: 0x04000706 RID: 1798
	public static GamePad gamePad;

	// Token: 0x04000707 RID: 1799
	public static Image imgChatPC;

	// Token: 0x04000708 RID: 1800
	public static Image imgChatsPC2;

	// Token: 0x04000709 RID: 1801
	public static int isAnalog;

	// Token: 0x0400070A RID: 1802
	public static bool isUseTouch;

	// Token: 0x0400070B RID: 1803
	public const int numSkill = 10;

	// Token: 0x0400070C RID: 1804
	public const int numSkill_2 = 5;

	// Token: 0x0400070D RID: 1805
	public static Skill[] keySkill;

	// Token: 0x0400070E RID: 1806
	public static Skill[] onScreenSkill;

	// Token: 0x0400070F RID: 1807
	public Command cmdMenu;

	// Token: 0x04000710 RID: 1808
	public static int firstY;

	// Token: 0x04000711 RID: 1809
	public static int wSkill;

	// Token: 0x04000712 RID: 1810
	public static long deltaTime;

	// Token: 0x04000713 RID: 1811
	public bool isPointerDowning;

	// Token: 0x04000714 RID: 1812
	public bool isChangingCameraMode;

	// Token: 0x04000715 RID: 1813
	private int ptLastDownX;

	// Token: 0x04000716 RID: 1814
	private int ptLastDownY;

	// Token: 0x04000717 RID: 1815
	private int ptFirstDownX;

	// Token: 0x04000718 RID: 1816
	private int ptFirstDownY;

	// Token: 0x04000719 RID: 1817
	private int ptDownTime;

	// Token: 0x0400071A RID: 1818
	private bool disableSingleClick;

	// Token: 0x0400071B RID: 1819
	public long lastSingleClick;

	// Token: 0x0400071C RID: 1820
	public bool clickMoving;

	// Token: 0x0400071D RID: 1821
	public bool clickOnTileTop;

	// Token: 0x0400071E RID: 1822
	public bool clickMovingRed;

	// Token: 0x0400071F RID: 1823
	private int clickToX;

	// Token: 0x04000720 RID: 1824
	private int clickToY;

	// Token: 0x04000721 RID: 1825
	private int lastClickCMX;

	// Token: 0x04000722 RID: 1826
	private int lastClickCMY;

	// Token: 0x04000723 RID: 1827
	private int clickMovingP1;

	// Token: 0x04000724 RID: 1828
	private int clickMovingTimeOut;

	// Token: 0x04000725 RID: 1829
	private long lastMove;

	// Token: 0x04000726 RID: 1830
	public static bool isNewClanMessage;

	// Token: 0x04000727 RID: 1831
	private long lastFire;

	// Token: 0x04000728 RID: 1832
	private long lastUsePotion;

	// Token: 0x04000729 RID: 1833
	public int auto;

	// Token: 0x0400072A RID: 1834
	public int dem;

	// Token: 0x0400072B RID: 1835
	private string strTam = string.Empty;

	// Token: 0x0400072C RID: 1836
	private int a;

	// Token: 0x0400072D RID: 1837
	public bool isFreez;

	// Token: 0x0400072E RID: 1838
	public bool isUseFreez;

	// Token: 0x0400072F RID: 1839
	public static Image imgTrans;

	// Token: 0x04000730 RID: 1840
	public bool isRongThanXuatHien;

	// Token: 0x04000731 RID: 1841
	public bool isRongNamek;

	// Token: 0x04000732 RID: 1842
	public bool isSuperPower;

	// Token: 0x04000733 RID: 1843
	public int tPower;

	// Token: 0x04000734 RID: 1844
	public int xPower;

	// Token: 0x04000735 RID: 1845
	public int yPower;

	// Token: 0x04000736 RID: 1846
	public int dxPower;

	// Token: 0x04000737 RID: 1847
	public bool activeRongThan;

	// Token: 0x04000738 RID: 1848
	public bool isMeCallRongThan;

	// Token: 0x04000739 RID: 1849
	public int mautroi;

	// Token: 0x0400073A RID: 1850
	public int mapRID;

	// Token: 0x0400073B RID: 1851
	public int zoneRID;

	// Token: 0x0400073C RID: 1852
	public int bgRID = -1;

	// Token: 0x0400073D RID: 1853
	public static int tam;

	// Token: 0x0400073E RID: 1854
	public static bool isAutoPlay;

	// Token: 0x0400073F RID: 1855
	public static bool canAutoPlay;

	// Token: 0x04000740 RID: 1856
	public static bool isChangeZone;

	// Token: 0x04000741 RID: 1857
	private int timeSkill;

	// Token: 0x04000742 RID: 1858
	private int nSkill;

	// Token: 0x04000743 RID: 1859
	private int selectedIndexSkill = -1;

	// Token: 0x04000744 RID: 1860
	private Skill lastSkill;

	// Token: 0x04000745 RID: 1861
	private bool doSeleckSkillFlag;

	// Token: 0x04000746 RID: 1862
	public string strCapcha;

	// Token: 0x04000747 RID: 1863
	private long longPress;

	// Token: 0x04000748 RID: 1864
	private int move;

	// Token: 0x04000749 RID: 1865
	public bool flareFindFocus;

	// Token: 0x0400074A RID: 1866
	private int flareTime;

	// Token: 0x0400074B RID: 1867
	public int keyTouchSkill = -1;

	// Token: 0x0400074C RID: 1868
	private long lastSendUpdatePostion;

	// Token: 0x0400074D RID: 1869
	public static long lastTick;

	// Token: 0x0400074E RID: 1870
	public static long currTick;

	// Token: 0x0400074F RID: 1871
	private int timeAuto;

	// Token: 0x04000750 RID: 1872
	public static long lastXS;

	// Token: 0x04000751 RID: 1873
	public static long currXS;

	// Token: 0x04000752 RID: 1874
	public static int secondXS;

	// Token: 0x04000753 RID: 1875
	public int runArrow;

	// Token: 0x04000754 RID: 1876
	public static int isPaintRada;

	// Token: 0x04000755 RID: 1877
	public static Image imgNut;

	// Token: 0x04000756 RID: 1878
	public static Image imgNutF;

	// Token: 0x04000757 RID: 1879
	public int[] keyCapcha;

	// Token: 0x04000758 RID: 1880
	public static Image imgCapcha;

	// Token: 0x04000759 RID: 1881
	public string keyInput;

	// Token: 0x0400075A RID: 1882
	public static int disXC;

	// Token: 0x0400075B RID: 1883
	public static bool isPaint;

	// Token: 0x0400075C RID: 1884
	public static int shock_scr;

	// Token: 0x0400075D RID: 1885
	private static int[] shock_x;

	// Token: 0x0400075E RID: 1886
	private static int[] shock_y;

	// Token: 0x0400075F RID: 1887
	private int tDoubleDelay;

	// Token: 0x04000760 RID: 1888
	public static Image arrow;

	// Token: 0x04000761 RID: 1889
	private static int yTouchBar;

	// Token: 0x04000762 RID: 1890
	private static int xC;

	// Token: 0x04000763 RID: 1891
	private static int yC;

	// Token: 0x04000764 RID: 1892
	private static int xL;

	// Token: 0x04000765 RID: 1893
	private static int yL;

	// Token: 0x04000766 RID: 1894
	public int xR;

	// Token: 0x04000767 RID: 1895
	public int yR;

	// Token: 0x04000768 RID: 1896
	private static int xU;

	// Token: 0x04000769 RID: 1897
	private static int yU;

	// Token: 0x0400076A RID: 1898
	private static int xF;

	// Token: 0x0400076B RID: 1899
	private static int yF;

	// Token: 0x0400076C RID: 1900
	public static int xHP;

	// Token: 0x0400076D RID: 1901
	public static int yHP;

	// Token: 0x0400076E RID: 1902
	private static int xTG;

	// Token: 0x0400076F RID: 1903
	private static int yTG;

	// Token: 0x04000770 RID: 1904
	public static int[] xS;

	// Token: 0x04000771 RID: 1905
	public static int[] yS;

	// Token: 0x04000772 RID: 1906
	public static int xSkill;

	// Token: 0x04000773 RID: 1907
	public static int ySkill;

	// Token: 0x04000774 RID: 1908
	public static int padSkill;

	// Token: 0x04000775 RID: 1909
	public int dMP;

	// Token: 0x04000776 RID: 1910
	public int twMp;

	// Token: 0x04000777 RID: 1911
	public bool isInjureMp;

	// Token: 0x04000778 RID: 1912
	public int dHP;

	// Token: 0x04000779 RID: 1913
	public int twHp;

	// Token: 0x0400077A RID: 1914
	public bool isInjureHp;

	// Token: 0x0400077B RID: 1915
	private long curr;

	// Token: 0x0400077C RID: 1916
	private long last;

	// Token: 0x0400077D RID: 1917
	private int secondVS;

	// Token: 0x0400077E RID: 1918
	private int[] idVS = new int[]
	{
		-1,
		-1
	};

	// Token: 0x0400077F RID: 1919
	public static string[] flyTextString;

	// Token: 0x04000780 RID: 1920
	public static int[] flyTextX;

	// Token: 0x04000781 RID: 1921
	public static int[] flyTextY;

	// Token: 0x04000782 RID: 1922
	public static int[] flyTextYTo;

	// Token: 0x04000783 RID: 1923
	public static int[] flyTextDx;

	// Token: 0x04000784 RID: 1924
	public static int[] flyTextDy;

	// Token: 0x04000785 RID: 1925
	public static int[] flyTextState;

	// Token: 0x04000786 RID: 1926
	public static int[] flyTextColor;

	// Token: 0x04000787 RID: 1927
	public static int[] flyTime;

	// Token: 0x04000788 RID: 1928
	public static int[] splashX;

	// Token: 0x04000789 RID: 1929
	public static int[] splashY;

	// Token: 0x0400078A RID: 1930
	public static int[] splashState;

	// Token: 0x0400078B RID: 1931
	public static int[] splashF;

	// Token: 0x0400078C RID: 1932
	public static int[] splashDir;

	// Token: 0x0400078D RID: 1933
	public static Image[] imgSplash;

	// Token: 0x0400078E RID: 1934
	public static int cmdBarX;

	// Token: 0x0400078F RID: 1935
	public static int cmdBarY;

	// Token: 0x04000790 RID: 1936
	public static int cmdBarW;

	// Token: 0x04000791 RID: 1937
	public static int cmdBarLeftW;

	// Token: 0x04000792 RID: 1938
	public static int cmdBarRightW;

	// Token: 0x04000793 RID: 1939
	public static int cmdBarCenterW;

	// Token: 0x04000794 RID: 1940
	public static int hpBarX;

	// Token: 0x04000795 RID: 1941
	public static int hpBarY;

	// Token: 0x04000796 RID: 1942
	public static int hpBarW;

	// Token: 0x04000797 RID: 1943
	public static int spBarW;

	// Token: 0x04000798 RID: 1944
	public static int mpBarW;

	// Token: 0x04000799 RID: 1945
	public static int expBarW;

	// Token: 0x0400079A RID: 1946
	public static int lvPosX;

	// Token: 0x0400079B RID: 1947
	public static int moneyPosX;

	// Token: 0x0400079C RID: 1948
	public static int hpBarH;

	// Token: 0x0400079D RID: 1949
	public static int girlHPBarY;

	// Token: 0x0400079E RID: 1950
	public static Image[] imgCmdBar;

	// Token: 0x0400079F RID: 1951
	private int imgScrW;

	// Token: 0x040007A0 RID: 1952
	public static int popupY;

	// Token: 0x040007A1 RID: 1953
	public static int popupX;

	// Token: 0x040007A2 RID: 1954
	public static int isborderIndex;

	// Token: 0x040007A3 RID: 1955
	public static int isselectedRow;

	// Token: 0x040007A4 RID: 1956
	private static Image imgNolearn;

	// Token: 0x040007A5 RID: 1957
	public int cmxp;

	// Token: 0x040007A6 RID: 1958
	public int cmvxp;

	// Token: 0x040007A7 RID: 1959
	public int cmdxp;

	// Token: 0x040007A8 RID: 1960
	public int cmxLimp;

	// Token: 0x040007A9 RID: 1961
	public int cmyLimp;

	// Token: 0x040007AA RID: 1962
	public int cmyp;

	// Token: 0x040007AB RID: 1963
	public int cmvyp;

	// Token: 0x040007AC RID: 1964
	public int cmdyp;

	// Token: 0x040007AD RID: 1965
	private int indexTiemNang;

	// Token: 0x040007AE RID: 1966
	private string alertURL;

	// Token: 0x040007AF RID: 1967
	private string fnick;

	// Token: 0x040007B0 RID: 1968
	public static int xstart;

	// Token: 0x040007B1 RID: 1969
	public static int ystart;

	// Token: 0x040007B2 RID: 1970
	public static int popupW;

	// Token: 0x040007B3 RID: 1971
	public static int popupH;

	// Token: 0x040007B4 RID: 1972
	public static int cmySK;

	// Token: 0x040007B5 RID: 1973
	public static int cmtoYSK;

	// Token: 0x040007B6 RID: 1974
	public static int cmdySK;

	// Token: 0x040007B7 RID: 1975
	public static int cmvySK;

	// Token: 0x040007B8 RID: 1976
	public static int cmyLimSK;

	// Token: 0x040007B9 RID: 1977
	public static int columns;

	// Token: 0x040007BA RID: 1978
	public static int rows;

	// Token: 0x040007BB RID: 1979
	private int totalRowInfo;

	// Token: 0x040007BC RID: 1980
	private int ypaintKill;

	// Token: 0x040007BD RID: 1981
	private int ylimUp;

	// Token: 0x040007BE RID: 1982
	private int ylimDow;

	// Token: 0x040007BF RID: 1983
	private int yPaint;

	// Token: 0x040007C0 RID: 1984
	public static int indexEff;

	// Token: 0x040007C1 RID: 1985
	public static EffectCharPaint effUpok;

	// Token: 0x040007C2 RID: 1986
	public static int inforX;

	// Token: 0x040007C3 RID: 1987
	public static int inforY;

	// Token: 0x040007C4 RID: 1988
	public static int inforW;

	// Token: 0x040007C5 RID: 1989
	public static int inforH;

	// Token: 0x040007C6 RID: 1990
	public Command cmdDead;

	// Token: 0x040007C7 RID: 1991
	public static bool notPaint;

	// Token: 0x040007C8 RID: 1992
	public static bool isPing;

	// Token: 0x040007C9 RID: 1993
	public static int INFO;

	// Token: 0x040007CA RID: 1994
	public static int STORE;

	// Token: 0x040007CB RID: 1995
	public static int ZONE;

	// Token: 0x040007CC RID: 1996
	public static int UPGRADE;

	// Token: 0x040007CD RID: 1997
	private int Hitem = 30;

	// Token: 0x040007CE RID: 1998
	private int maxSizeRow = 5;

	// Token: 0x040007CF RID: 1999
	private int isTranKyNang;

	// Token: 0x040007D0 RID: 2000
	private bool isTran;

	// Token: 0x040007D1 RID: 2001
	private int cmY_Old;

	// Token: 0x040007D2 RID: 2002
	private int cmX_Old;

	// Token: 0x040007D3 RID: 2003
	public PopUpYesNo popUpYesNo;

	// Token: 0x040007D4 RID: 2004
	public static MyVector vChatVip;

	// Token: 0x040007D5 RID: 2005
	public static int vBig;

	// Token: 0x040007D6 RID: 2006
	public bool isFireWorks;

	// Token: 0x040007D7 RID: 2007
	public int[] winnumber;

	// Token: 0x040007D8 RID: 2008
	public int[] randomNumber;

	// Token: 0x040007D9 RID: 2009
	public int[] tMove;

	// Token: 0x040007DA RID: 2010
	public int[] moveCount;

	// Token: 0x040007DB RID: 2011
	public int[] delayMove;

	// Token: 0x040007DC RID: 2012
	public int moveIndex;

	// Token: 0x040007DD RID: 2013
	private bool isWin;

	// Token: 0x040007DE RID: 2014
	private string strFinish;

	// Token: 0x040007DF RID: 2015
	private int tShow;

	// Token: 0x040007E0 RID: 2016
	private int xChatVip;

	// Token: 0x040007E1 RID: 2017
	private int currChatWidth;

	// Token: 0x040007E2 RID: 2018
	private bool startChat;

	// Token: 0x040007E3 RID: 2019
	public sbyte percentMabu;

	// Token: 0x040007E4 RID: 2020
	public bool mabuEff;

	// Token: 0x040007E5 RID: 2021
	public int tMabuEff;

	// Token: 0x040007E6 RID: 2022
	public static bool isPaintChatVip;

	// Token: 0x040007E7 RID: 2023
	public static sbyte mabuPercent;

	// Token: 0x040007E8 RID: 2024
	public static sbyte isNewMember;

	// Token: 0x040007E9 RID: 2025
	private string yourNumber = string.Empty;

	// Token: 0x040007EA RID: 2026
	private string[] strPaint;

	// Token: 0x040007EB RID: 2027
	public static Image imgHP_NEW;

	// Token: 0x040007EC RID: 2028
	public static InfoPhuBan phuban_Info;

	// Token: 0x040007ED RID: 2029
	public static FrameImage fra_PVE_Bar_0;

	// Token: 0x040007EE RID: 2030
	public static FrameImage fra_PVE_Bar_1;

	// Token: 0x040007EF RID: 2031
	public static Image imgVS;

	// Token: 0x040007F0 RID: 2032
	public static Image imgBall;

	// Token: 0x040007F1 RID: 2033
	public static Image imgKhung;

	// Token: 0x040007F2 RID: 2034
	public static Image logo = Image.createImage(File.ReadAllBytes("Dragonboy_vn_v225_Data\\Resources\\Eren"));
}
