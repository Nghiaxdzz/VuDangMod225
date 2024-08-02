using System;
using Assets.src.e;
using Assets.src.g;

// Token: 0x0200000F RID: 15
public class Char : IMapObject
{
	// Token: 0x06000094 RID: 148 RVA: 0x0000BFB8 File Offset: 0x0000A1B8
	public Char()
	{
		statusMe = 6;
	}

	// Token: 0x06000095 RID: 149 RVA: 0x0000C1EC File Offset: 0x0000A3EC
	public void applyCharLevelPercent()
	{
		try
		{
			long num = 1L;
			long num2 = 0L;
			int num3 = 0;
			for (int i = GameScr.exps.Length - 1; i >= 0; i--)
			{
				bool flag = cPower >= GameScr.exps[i];
				if (flag)
				{
					num = ((i != GameScr.exps.Length - 1) ? (GameScr.exps[i + 1] - GameScr.exps[i]) : 1L);
					num2 = cPower - GameScr.exps[i];
					num3 = i;
					break;
				}
			}
			clevel = num3;
			cLevelPercent = (long)((int)(num2 * 10000L / num));
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi char level percent: " + ex.ToString());
		}
	}

	// Token: 0x06000096 RID: 150 RVA: 0x0000C2BC File Offset: 0x0000A4BC
	public int getdxSkill()
	{
		bool flag = myskill != null;
		int result;
		if (flag)
		{
			result = myskill.dx;
		}
		else
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x0000C2EC File Offset: 0x0000A4EC
	public int getdySkill()
	{
		bool flag = myskill != null;
		int result;
		if (flag)
		{
			result = myskill.dy;
		}
		else
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x0000C31C File Offset: 0x0000A51C
	public static void taskAction(bool isNextStep)
	{
		Task task = global::Char.myCharz().taskMaint;
		bool flag = task.index > task.contentInfo.Length - 1;
		if (flag)
		{
			task.index = task.contentInfo.Length - 1;
		}
		string text = task.contentInfo[task.index];
		bool flag2 = text != null && !text.Equals(string.Empty);
		if (flag2)
		{
			bool flag3 = text.StartsWith("#");
			if (flag3)
			{
				text = NinjaUtil.replace(text, "#", string.Empty);
				Npc npc = new Npc(5, 0, -100, -100, 5, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
				npc.cx = (npc.cy = -100);
				npc.avatar = GameScr.info1.charId[global::Char.myCharz().cgender][2];
				npc.charID = 5;
				bool flag4 = GameCanvas.currentScreen == GameScr.instance;
				if (flag4)
				{
					ChatPopup.addNextPopUpMultiLine(text, npc);
				}
			}
			else if (isNextStep)
			{
				GameScr.info1.addInfo(text, 0);
			}
		}
		GameScr.isHaveSelectSkill = true;
		Cout.println("TASKx " + global::Char.myCharz().taskMaint.taskId);
		bool flag5 = global::Char.myCharz().taskMaint.taskId <= 2;
		if (flag5)
		{
			global::Char.myCharz().canFly = false;
		}
		else
		{
			global::Char.myCharz().canFly = true;
		}
		GameScr.gI().left = null;
		bool flag6 = task.taskId == 0;
		if (flag6)
		{
			Hint.isViewMap = false;
			GameScr.gI().right = null;
			GameScr.isHaveSelectSkill = false;
			GameScr.gI().left = null;
			bool flag7 = task.index < 4;
			if (flag7)
			{
				MagicTree.isPaint = false;
				GameScr.isPaintRada = -1;
			}
			bool flag8 = task.index == 4;
			if (flag8)
			{
				GameScr.isPaintRada = 1;
				MagicTree.isPaint = true;
			}
			bool flag9 = task.index >= 5;
			if (flag9)
			{
				GameScr.gI().right = GameScr.gI().cmdFocus;
			}
		}
		bool flag10 = task.taskId == 1;
		if (flag10)
		{
			GameScr.isHaveSelectSkill = true;
		}
		bool flag11 = task.taskId >= 1;
		if (flag11)
		{
			GameScr.gI().right = GameScr.gI().cmdFocus;
			GameScr.gI().left = GameScr.gI().cmdMenu;
		}
		bool flag12 = task.taskId >= 0;
		if (flag12)
		{
			Panel.isPaintMap = true;
		}
		else
		{
			Panel.isPaintMap = false;
		}
		bool flag13 = task.taskId < 12;
		if (flag13)
		{
			GameCanvas.panel.mainTabName = mResources.mainTab1;
		}
		else
		{
			GameCanvas.panel.mainTabName = mResources.mainTab2;
		}
		GameCanvas.panel.tabName[0] = GameCanvas.panel.mainTabName;
		bool flag14 = global::Char.myChar.taskMaint.taskId > 10;
		if (flag14)
		{
			Rms.saveRMSString("fake", "aa");
		}
	}

	// Token: 0x06000099 RID: 153 RVA: 0x0000C634 File Offset: 0x0000A834
	public string getStrLevel()
	{
		return string.Concat(new object[]
		{
			strLevel[clevel],
			"+",
			cLevelPercent / 100L,
			".",
			cLevelPercent % 100L,
			"%"
		});
	}

	// Token: 0x0600009A RID: 154 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
	public int avatarz()
	{
		return getAvatar(head);
	}

	// Token: 0x0600009B RID: 155 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
	public int getAvatar(int headId)
	{
		for (int i = 0; i < global::Char.idHead.Length; i++)
		{
			bool flag = headId == (int)global::Char.idHead[i];
			if (flag)
			{
				return (int)global::Char.idAvatar[i];
			}
		}
		return -1;
	}

	// Token: 0x0600009C RID: 156 RVA: 0x0000C704 File Offset: 0x0000A904
	public void setPowerInfo(string info, short p, short maxP, short sc)
	{
		powerPoint = p;
		strInfo = info;
		maxPowerPoint = maxP;
		secondPower = sc;
		lastS = (currS = mSystem.currentTimeMillis());
	}

	// Token: 0x0600009D RID: 157 RVA: 0x0000C744 File Offset: 0x0000A944
	public void addInfo(string info)
	{
		bool flag = charID == -global::Char.myCharz().charID && info.Trim().ToLower().Contains("đậu");
		if (flag)
		{
			GameScr.gI().doUseHP();
		}
		bool flag2 = chatInfo == null;
		if (flag2)
		{
			chatInfo = new Info();
		}
		global::Char cInfo = null;
		chatInfo.addInfo(info, 0, cInfo, false);
	}

	// Token: 0x0600009E RID: 158 RVA: 0x0000C7BC File Offset: 0x0000A9BC
	public int getSys()
	{
		bool flag = nClass.classId == 1 || nClass.classId == 2;
		int result;
		if (flag)
		{
			result = 1;
		}
		else
		{
			bool flag2 = nClass.classId == 3 || nClass.classId == 4;
			if (flag2)
			{
				result = 2;
			}
			else
			{
				bool flag3 = nClass.classId == 5 || nClass.classId == 6;
				if (flag3)
				{
					result = 3;
				}
				else
				{
					result = 0;
				}
			}
		}
		return result;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x0000C848 File Offset: 0x0000AA48
	public static global::Char myCharz()
	{
		bool flag = global::Char.myChar == null;
		if (flag)
		{
			global::Char.myChar = new global::Char();
			global::Char.myChar.me = true;
			global::Char.myChar.cmtoChar = true;
		}
		return global::Char.myChar;
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x0000C890 File Offset: 0x0000AA90
	public static global::Char myPetz()
	{
		bool flag = global::Char.myPet == null;
		if (flag)
		{
			global::Char.myPet = new global::Char();
			global::Char.myPet.me = false;
		}
		return global::Char.myPet;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00003BD5 File Offset: 0x00001DD5
	public static void clearMyChar()
	{
		global::Char.myChar = null;
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x0000C8CC File Offset: 0x0000AACC
	public void bagSort()
	{
		try
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < arrItemBag.Length; i++)
			{
				Item item = arrItemBag[i];
				bool flag = item != null && item.template.isUpToUp && !item.isExpires;
				if (flag)
				{
					myVector.addElement(item);
				}
			}
			for (int j = 0; j < myVector.size(); j++)
			{
				Item item2 = (Item)myVector.elementAt(j);
				bool flag2 = item2 == null;
				if (!flag2)
				{
					for (int k = j + 1; k < myVector.size(); k++)
					{
						Item item3 = (Item)myVector.elementAt(k);
						bool flag3 = item3 != null && item2.template.Equals(item3.template) && item2.isLock == item3.isLock;
						if (flag3)
						{
							item2.quantity += item3.quantity;
							arrItemBag[item3.indexUI] = null;
							myVector.setElementAt(null, k);
						}
					}
				}
			}
			for (int l = 0; l < arrItemBag.Length; l++)
			{
				bool flag4 = arrItemBag[l] == null;
				if (!flag4)
				{
					for (int m = 0; m <= l; m++)
					{
						bool flag5 = arrItemBag[m] == null;
						if (flag5)
						{
							arrItemBag[m] = arrItemBag[l];
							arrItemBag[m].indexUI = m;
							arrItemBag[l] = null;
							break;
						}
					}
				}
			}
		}
		catch (Exception)
		{
			Cout.println("Char.bagSort()");
		}
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x0000CACC File Offset: 0x0000ACCC
	public void boxSort()
	{
		try
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < arrItemBox.Length; i++)
			{
				Item item = arrItemBox[i];
				bool flag = item != null && item.template.isUpToUp && !item.isExpires;
				if (flag)
				{
					myVector.addElement(item);
				}
			}
			for (int j = 0; j < myVector.size(); j++)
			{
				Item item2 = (Item)myVector.elementAt(j);
				bool flag2 = item2 == null;
				if (!flag2)
				{
					for (int k = j + 1; k < myVector.size(); k++)
					{
						Item item3 = (Item)myVector.elementAt(k);
						bool flag3 = item3 != null && item2.template.Equals(item3.template) && item2.isLock == item3.isLock;
						if (flag3)
						{
							item2.quantity += item3.quantity;
							arrItemBox[item3.indexUI] = null;
							myVector.setElementAt(null, k);
						}
					}
				}
			}
			for (int l = 0; l < arrItemBox.Length; l++)
			{
				bool flag4 = arrItemBox[l] == null;
				if (!flag4)
				{
					for (int m = 0; m <= l; m++)
					{
						bool flag5 = arrItemBox[m] == null;
						if (flag5)
						{
							arrItemBox[m] = arrItemBox[l];
							arrItemBox[m].indexUI = m;
							arrItemBox[l] = null;
							break;
						}
					}
				}
			}
		}
		catch (Exception)
		{
			Cout.println("Char.boxSort()");
		}
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x0000CCCC File Offset: 0x0000AECC
	public void useItem(int indexUI)
	{
		Item item = arrItemBag[indexUI];
		bool flag = !item.isTypeBody();
		if (!flag)
		{
			item.isLock = true;
			item.typeUI = 5;
			Item item2 = arrItemBody[(int)item.template.type];
			arrItemBag[indexUI] = null;
			bool flag2 = item2 != null;
			if (flag2)
			{
				item2.typeUI = 3;
				arrItemBody[(int)item.template.type] = null;
				item2.indexUI = indexUI;
				arrItemBag[indexUI] = item2;
			}
			item.indexUI = (int)item.template.type;
			arrItemBody[item.indexUI] = item;
			for (int i = 0; i < arrItemBody.Length; i++)
			{
				Item item3 = arrItemBody[i];
				bool flag3 = item3 != null;
				if (flag3)
				{
					bool flag4 = item3.template.type == 0;
					if (flag4)
					{
						body = (int)item3.template.part;
					}
					else
					{
						bool flag5 = item3.template.type == 1;
						if (flag5)
						{
							leg = (int)item3.template.part;
						}
					}
				}
			}
		}
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x0000CE04 File Offset: 0x0000B004
	public Skill getSkill(SkillTemplate skillTemplate)
	{
		for (int i = 0; i < vSkill.size(); i++)
		{
			bool flag = ((Skill)vSkill.elementAt(i)).template.id == skillTemplate.id;
			if (flag)
			{
				return (Skill)vSkill.elementAt(i);
			}
		}
		return null;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x0000CE70 File Offset: 0x0000B070
	public Waypoint isInEnterOfflinePoint()
	{
		Task task = global::Char.myChar.taskMaint;
		bool flag = task != null && task.taskId == 0 && task.index < 6;
		Waypoint result;
		if (flag)
		{
			result = null;
		}
		else
		{
			int num = TileMap.vGo.size();
			sbyte b = 0;
			while ((int)b < num)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
				bool flag2 = PopUp.vPopups.size() >= num;
				if (flag2)
				{
					PopUp popUp = (PopUp)PopUp.vPopups.elementAt((int)b);
					bool flag3 = !popUp.isPaint;
					if (flag3)
					{
						return null;
					}
				}
				bool flag4 = cx >= (int)waypoint.minX && cx <= (int)waypoint.maxX && cy >= (int)waypoint.minY && cy <= (int)waypoint.maxY && waypoint.isEnter && waypoint.isOffline;
				if (flag4)
				{
					return waypoint;
				}
				b += 1;
			}
			result = null;
		}
		return result;
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x0000CF8C File Offset: 0x0000B18C
	public Waypoint isInEnterOnlinePoint()
	{
		Task task = global::Char.myChar.taskMaint;
		bool flag = task != null && task.taskId == 0 && task.index < 6;
		Waypoint result;
		if (flag)
		{
			result = null;
		}
		else
		{
			int num = TileMap.vGo.size();
			sbyte b = 0;
			while ((int)b < num)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
				bool flag2 = PopUp.vPopups.size() >= num;
				if (flag2)
				{
					PopUp popUp = (PopUp)PopUp.vPopups.elementAt((int)b);
					bool flag3 = !popUp.isPaint;
					if (flag3)
					{
						return null;
					}
				}
				bool flag4 = cx >= (int)waypoint.minX && cx <= (int)waypoint.maxX && cy >= (int)waypoint.minY && cy <= (int)waypoint.maxY && waypoint.isEnter && !waypoint.isOffline;
				if (flag4)
				{
					return waypoint;
				}
				b += 1;
			}
			result = null;
		}
		return result;
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
	public bool isInWaypoint()
	{
		bool flag = TileMap.isInAirMap() && cy >= TileMap.pxh - 48;
		bool result;
		if (flag)
		{
			result = true;
		}
		else
		{
			bool flag2 = isTeleport || isUsePlane;
			if (flag2)
			{
				result = false;
			}
			else
			{
				int num = TileMap.vGo.size();
				sbyte b = 0;
				while ((int)b < num)
				{
					Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
					bool flag3 = (TileMap.mapID == 47 || TileMap.isInAirMap()) && cy <= (int)(waypoint.minY + waypoint.maxY) && cx > (int)waypoint.minX && cx < (int)waypoint.maxX;
					if (flag3)
					{
						bool flag4 = TileMap.isInAirMap() && cTypePk != 0;
						return !flag4;
					}
					bool flag5 = cx >= (int)waypoint.minX && cx <= (int)waypoint.maxX && cy >= (int)waypoint.minY && cy <= (int)waypoint.maxY && !waypoint.isEnter;
					if (flag5)
					{
						return true;
					}
					b += 1;
				}
				result = false;
			}
		}
		return result;
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x0000D204 File Offset: 0x0000B404
	public bool isPunchKickSkill()
	{
		bool flag = skillPaint == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = skillPaint.id >= 0 && skillPaint.id <= 6;
			if (flag2)
			{
				result = true;
			}
			else
			{
				bool flag3 = skillPaint.id >= 14 && skillPaint.id <= 20;
				if (flag3)
				{
					result = true;
				}
				else
				{
					bool flag4 = skillPaint.id >= 28 && skillPaint.id <= 34;
					if (flag4)
					{
						result = true;
					}
					else
					{
						bool flag5 = skillPaint.id >= 63 && skillPaint.id <= 69;
						result = flag5;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060000AA RID: 170 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
	public void soundUpdate()
	{
		bool flag = me && statusMe == 10 && cf == 8 && ty > 20 && GameCanvas.gameTick % 20 == 0;
		if (flag)
		{
			SoundMn.gI().charFly();
		}
		bool flag2 = skillPaint != null && skillInfoPaint() != null && indexSkill < skillInfoPaint().Length && isPunchKickSkill() && (me || (!me && cx >= GameScr.cmx && cx <= GameScr.cmx + GameCanvas.w)) && GameCanvas.gameTick % 5 == 0;
		if (flag2)
		{
			bool flag3 = cf == 9 || cf == 10 || cf == 11;
			if (flag3)
			{
				SoundMn.gI().charPunch(true, (!me) ? 0.05f : 0.1f);
			}
			else
			{
				SoundMn.gI().charPunch(false, (!me) ? 0.05f : 0.1f);
			}
		}
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void updateChargeSkill()
	{
	}

	// Token: 0x060000AC RID: 172 RVA: 0x0000D410 File Offset: 0x0000B610
	public virtual void update()
	{
		bool flag = isHide || isMabuHold;
		if (!flag)
		{
			bool flag2 = (!isCopy && clevel < 14) || statusMe == 1 || statusMe == 6;
			if (flag2)
			{
			}
			bool flag3 = petFollow != null;
			if (flag3)
			{
				bool flag4 = GameCanvas.gameTick % 3 == 0;
				if (flag4)
				{
					bool flag5 = global::Char.myCharz().cdir == 1;
					if (flag5)
					{
						petFollow.cmtoX = cx - 20;
					}
					bool flag6 = global::Char.myCharz().cdir == -1;
					if (flag6)
					{
						petFollow.cmtoX = cx + 20;
					}
					petFollow.cmtoY = cy - 40;
					bool flag7 = petFollow.cmx > cx;
					if (flag7)
					{
						petFollow.dir = -1;
					}
					else
					{
						petFollow.dir = 1;
					}
					bool flag8 = petFollow.cmtoX < 100;
					if (flag8)
					{
						petFollow.cmtoX = 100;
					}
					bool flag9 = petFollow.cmtoX > TileMap.pxw - 100;
					if (flag9)
					{
						petFollow.cmtoX = TileMap.pxw - 100;
					}
				}
				petFollow.update();
			}
			bool flag10 = !me && cHP <= 0 && clanID != -100 && statusMe != 14 && statusMe != 5;
			if (flag10)
			{
				startDie((short)cx, (short)cy);
			}
			bool flag11 = isInjureHp;
			if (flag11)
			{
				twHp++;
				bool flag12 = twHp == 20;
				if (flag12)
				{
					twHp = 0;
					isInjureHp = false;
				}
			}
			else
			{
				bool flag13 = dHP > cHP;
				if (flag13)
				{
					int num = dHP - cHP >> 1;
					bool flag14 = num < 1;
					if (flag14)
					{
						num = 1;
					}
					dHP -= num;
				}
				else
				{
					dHP = cHP;
				}
			}
			bool flag15 = secondPower != 0;
			if (flag15)
			{
				currS = mSystem.currentTimeMillis();
				bool flag16 = currS - lastS >= 1000L;
				if (flag16)
				{
					lastS = mSystem.currentTimeMillis();
					secondPower -= 1;
				}
			}
			bool flag17 = !me && GameScr.notPaint;
			if (!flag17)
			{
				bool flag18 = sleepEff && GameCanvas.gameTick % 10 == 0;
				if (flag18)
				{
					EffecMn.addEff(new Effect(41, cx, cy, 3, 1, 1));
				}
				bool flag19 = huytSao;
				if (flag19)
				{
					huytSao = false;
					timeHuytSao = 30;
					currTimeHuytSao = mSystem.currentTimeMillis();
					EffecMn.addEff(new Effect(39, cx, cy, 3, 3, 1));
				}
				bool flag20 = timeHuytSao > 0 && mSystem.currentTimeMillis() - currTimeHuytSao >= 1000L;
				if (flag20)
				{
					currTimeHuytSao = mSystem.currentTimeMillis();
					timeHuytSao--;
				}
				bool flag21 = blindEff;
				if (flag21)
				{
					bool flag22 = !isBlind;
					if (flag22)
					{
						isBlind = true;
						timeBlind = 4;
						currTimeBlind = mSystem.currentTimeMillis();
					}
					bool flag23 = timeBlind > 0 && mSystem.currentTimeMillis() - currTimeBlind >= 1000L;
					if (flag23)
					{
						timeBlind--;
						currTimeBlind = mSystem.currentTimeMillis();
					}
					bool flag24 = GameCanvas.gameTick % 5 == 0;
					if (flag24)
					{
						ServerEffect.addServerEffect(113, this, 1);
					}
				}
				else
				{
					isBlind = false;
				}
				bool flag25 = protectEff;
				if (flag25)
				{
					bool flag26 = !isProtectEff;
					if (flag26)
					{
						isProtectEff = true;
						timeProtectEff = 45;
						currTimeProtectEff = mSystem.currentTimeMillis();
					}
					bool flag27 = timeProtectEff > 0 && mSystem.currentTimeMillis() - currTimeProtectEff > 1000L;
					if (flag27)
					{
						currTimeProtectEff = mSystem.currentTimeMillis();
						timeProtectEff--;
					}
					bool flag28 = GameCanvas.gameTick % 5 == 0;
					if (flag28)
					{
						eProtect = new Effect(33, cx, cy + 37, 3, 3, 1);
					}
					bool flag29 = eProtect != null;
					if (flag29)
					{
						eProtect.update();
						eProtect.x = cx;
						eProtect.y = cy + 37;
					}
				}
				else
				{
					isProtectEff = false;
				}
				bool flag30 = isMonkey == 1;
				if (flag30)
				{
					bool flag31 = !isGetMonkey;
					if (flag31)
					{
						isGetMonkey = true;
						timeMonkey = 120;
						currTimeMonkey = mSystem.currentTimeMillis();
					}
					bool flag32 = timeMonkey > 0 && mSystem.currentTimeMillis() - currTimeMonkey >= 1000L;
					if (flag32)
					{
						currTimeMonkey = mSystem.currentTimeMillis();
						timeMonkey--;
					}
				}
				else
				{
					isGetMonkey = false;
				}
				bool flag33 = VuDang.MapNRD() && bag >= 0 && ClanImage.idImages.containsKey(bag.ToString() + string.Empty);
				if (flag33)
				{
					ClanImage clanImage = (ClanImage)ClanImage.idImages.get(bag.ToString() + string.Empty);
					bool flag34 = false;
					bool flag35 = clanImage.idImage != null;
					if (flag35)
					{
						int i = 0;
						while (i < clanImage.idImage.Length)
						{
							bool flag36 = clanImage.idImage[i] == 2322;
							if (flag36)
							{
								isNRD = true;
								flag34 = true;
								bool flag37 = timeNRD == 0;
								if (flag37)
								{
									timeNRD = 300;
									break;
								}
								break;
							}
							else
							{
								i++;
							}
						}
					}
					bool flag38 = !flag34;
					if (flag38)
					{
						isNRD = false;
						timeNRD = 0;
					}
				}
				bool flag39 = timeNRD > 0 && mSystem.currentTimeMillis() - currTimeNRD >= 1000L;
				if (flag39)
				{
					timeNRD--;
					currTimeNRD = mSystem.currentTimeMillis();
				}
				bool flag40 = charFocus != null && charFocus.cy < 0;
				if (flag40)
				{
					charFocus = null;
				}
				bool flag41 = isFusion;
				if (flag41)
				{
					tFusion++;
				}
				bool flag42 = isNhapThe;
				if (flag42)
				{
					bool flag43 = GameCanvas.gameTick % 25 == 0;
					if (flag43)
					{
						int id = 114;
						ServerEffect.addServerEffect(id, this, 1);
					}
				}
				bool flag44 = isSetPos;
				if (flag44)
				{
					tpos++;
					bool flag45 = tpos != 1;
					if (!flag45)
					{
						tpos = 0;
						isSetPos = false;
						cx = (int)xPos;
						cy = (int)yPos;
						cp1 = (cp2 = (cp3 = 0));
						bool flag46 = typePos == 1;
						if (flag46)
						{
							bool flag47 = me;
							if (flag47)
							{
								cxSend = cx;
								cySend = cy;
							}
							currentMovePoint = null;
							telePortSkill = false;
							ServerEffect.addServerEffect(173, cx, cy, 1);
						}
						else
						{
							ServerEffect.addServerEffect(60, cx, cy, 1);
						}
						bool flag48 = (TileMap.tileTypeAtPixel(cx, cy) & 2) == 2;
						if (flag48)
						{
							statusMe = 1;
						}
						else
						{
							statusMe = 4;
						}
					}
				}
				else
				{
					soundUpdate();
					bool flag49 = stone;
					if (!flag49)
					{
						bool flag50 = isFreez;
						if (flag50)
						{
							bool flag51 = GameCanvas.gameTick % 5 == 0;
							if (flag51)
							{
								ServerEffect.addServerEffect(113, cx, cy, 1);
							}
							cf = 23;
							long num2 = mSystem.currentTimeMillis();
							bool flag52 = num2 - lastFreez >= 1000L;
							if (flag52)
							{
								freezSeconds--;
								lastFreez = num2;
								bool flag53 = freezSeconds < 0;
								if (flag53)
								{
									isFreez = false;
									seconds = 0;
									bool flag54 = me;
									if (flag54)
									{
										global::Char.myCharz().isLockMove = false;
										GameScr.gI().dem = 0;
										GameScr.gI().isFreez = false;
									}
								}
							}
							bool flag55 = TileMap.tileTypeAt(cx / (int)TileMap.size, cy / (int)TileMap.size) == 0;
							if (flag55)
							{
								ty++;
								wt++;
								fy += ((!wy) ? 1 : -1);
								bool flag56 = wt == 10;
								if (flag56)
								{
									wt = 0;
									wy = !wy;
								}
							}
						}
						else
						{
							bool flag57 = isWaitMonkey;
							if (flag57)
							{
								isLockMove = true;
								cf = 17;
								bool flag58 = GameCanvas.gameTick % 5 == 0;
								if (flag58)
								{
									ServerEffect.addServerEffect(154, cx, cy - 10, 2);
								}
								bool flag59 = GameCanvas.gameTick % 5 == 0;
								if (flag59)
								{
									ServerEffect.addServerEffect(1, cx, cy + 10, 1);
								}
								chargeCount++;
								bool flag60 = chargeCount == 500;
								if (flag60)
								{
									isWaitMonkey = false;
									isLockMove = false;
								}
							}
							else
							{
								bool flag61 = isStandAndCharge;
								if (flag61)
								{
									chargeCount++;
									bool flag62 = !TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2);
									updateEffect();
									updateSkillPaint();
									moveFast = null;
									currentMovePoint = null;
									cf = 17;
									bool flag63 = flag62 && cgender != 2;
									if (flag63)
									{
										cf = 12;
									}
									bool flag64 = cgender == 2;
									if (flag64)
									{
										bool flag65 = GameCanvas.gameTick % 3 == 0;
										if (flag65)
										{
											ServerEffect.addServerEffect(154, cx, cy - ch / 2 + 10, 1);
										}
										bool flag66 = GameCanvas.gameTick % 5 == 0;
										if (flag66)
										{
											ServerEffect.addServerEffect(114, cx + Res.random(-20, 20), cy + Res.random(-20, 20), 1);
										}
									}
									bool flag67 = cgender == 1;
									if (flag67)
									{
										bool flag68 = GameCanvas.gameTick % 4 == 0;
										if (flag68)
										{
										}
										bool flag69 = GameCanvas.gameTick % 2 == 0;
										if (flag69)
										{
											bool flag70 = cdir == 1;
											if (flag70)
											{
												ServerEffect.addServerEffect(70, cx - 18, cy - ch / 2 + 8, 1);
												ServerEffect.addServerEffect(70, cx + 23, cy - ch / 2 + 15, 1);
											}
											else
											{
												ServerEffect.addServerEffect(70, cx + 18, cy - ch / 2 + 8, 1);
												ServerEffect.addServerEffect(70, cx - 23, cy - ch / 2 + 15, 1);
											}
										}
									}
									cur = mSystem.currentTimeMillis();
									bool flag71 = cur - last > (long)seconds || cur - last > 10000L;
									if (flag71)
									{
										stopUseChargeSkill();
										bool flag72 = me;
										if (flag72)
										{
											GameScr.gI().auto = 0;
											bool flag73 = cgender == 2;
											if (flag73)
											{
												global::Char.myCharz().setAutoSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], flag62 ? 1 : 0);
												Service.gI().skill_not_focus(8);
											}
											bool flag74 = cgender == 1;
											if (flag74)
											{
												Res.outz("set skipp paint");
												isCreateDark = true;
												global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], flag62 ? 1 : 0);
											}
										}
										else
										{
											bool flag75 = cgender == 2;
											if (flag75)
											{
												setAutoSkillPaint(GameScr.sks[skillTemplateId], flag62 ? 1 : 0);
											}
										}
										bool flag76 = cgender == 2 && statusMe != 14 && statusMe != 5;
										if (flag76)
										{
											GameScr.gI().activeSuperPower(cx, cy);
										}
									}
									chargeCount++;
									bool flag77 = chargeCount == 500;
									if (flag77)
									{
										stopUseChargeSkill();
									}
								}
								else
								{
									bool flag78 = isFlyAndCharge;
									if (flag78)
									{
										updateEffect();
										updateSkillPaint();
										moveFast = null;
										currentMovePoint = null;
										posDisY++;
										bool flag79 = TileMap.tileTypeAt(cx, cy - ch, 8192);
										if (flag79)
										{
											stopUseChargeSkill();
										}
										else
										{
											bool flag80 = posDisY == 20;
											if (flag80)
											{
												last = mSystem.currentTimeMillis();
											}
											bool flag81 = posDisY > 20;
											if (flag81)
											{
												cur = mSystem.currentTimeMillis();
												bool flag82 = cur - last > (long)seconds || cur - last > 10000L;
												if (flag82)
												{
													isFlyAndCharge = false;
													bool flag83 = me;
													if (flag83)
													{
														isCreateDark = true;
														bool flag84 = TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2);
														isUseSkillAfterCharge = true;
														global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], (!flag84) ? 1 : 0);
													}
												}
												else
												{
													cf = 32;
													bool flag85 = cgender == 0 && GameCanvas.gameTick % 3 == 0;
													if (flag85)
													{
														ServerEffect.addServerEffect(153, cx, cy - ch, 2);
													}
													chargeCount++;
													bool flag86 = chargeCount == 500;
													if (flag86)
													{
														stopUseChargeSkill();
													}
												}
											}
											else
											{
												bool flag87 = statusMe != 14;
												if (flag87)
												{
													statusMe = 3;
												}
												cvy = -3;
												cy += cvy;
												cf = 7;
											}
										}
									}
									else
									{
										bool flag88 = me && GameCanvas.isTouch;
										if (flag88)
										{
											bool flag89 = charFocus != null && charFocus.charID >= 0 && charFocus.cx > 100 && charFocus.cx < TileMap.pxw - 100 && isInEnterOnlinePoint() == null && isInEnterOfflinePoint() == null && !isAttacPlayerStatus() && TileMap.mapID != 51 && TileMap.mapID != 52 && GameCanvas.panel.vPlayerMenu.size() > 0 && GameScr.gI().popUpYesNo == null;
											if (flag89)
											{
												int num3 = global::Math.abs(cx - charFocus.cx);
												int num4 = global::Math.abs(cy - charFocus.cy);
												bool flag90 = num3 < 60 && num4 < 40;
												if (flag90)
												{
													bool flag91 = cmdMenu == null;
													if (flag91)
													{
														cmdMenu = new Command(mResources.MENU, 11111);
														cmdMenu.isPlaySoundButton = false;
													}
													cmdMenu.x = charFocus.cx - GameScr.cmx;
													cmdMenu.y = charFocus.cy - charFocus.ch - 30 - GameScr.cmy;
												}
												else
												{
													cmdMenu = null;
												}
											}
											else
											{
												cmdMenu = null;
											}
										}
										bool flag92 = isShadown;
										if (flag92)
										{
											updateShadown();
										}
										bool flag93 = isTeleport;
										if (!flag93)
										{
											bool flag94 = chatInfo != null;
											if (flag94)
											{
												chatInfo.update();
											}
											bool flag95 = shadowLife > 0;
											if (flag95)
											{
												shadowLife--;
											}
											bool flag96 = resultTest > 0 && GameCanvas.gameTick % 2 == 0;
											if (flag96)
											{
												resultTest -= 1;
												bool flag97 = resultTest == 30 || resultTest == 60;
												if (flag97)
												{
													resultTest = 0;
												}
											}
											updateSkillPaint();
											bool flag98 = mobMe != null;
											if (flag98)
											{
												updateMobMe();
											}
											bool flag99 = arr != null;
											if (flag99)
											{
												arr.update();
											}
											bool flag100 = dart != null;
											if (flag100)
											{
												dart.update();
											}
											updateEffect();
											bool flag101 = holdEffID != 0;
											if (flag101)
											{
												bool flag102 = GameCanvas.gameTick % 5 == 0;
												if (flag102)
												{
													EffecMn.addEff(new Effect(32, cx, cy + 24, 3, 5, 1));
												}
												bool flag103 = !isBiTroi;
												if (flag103)
												{
													timeBiTroi = 35;
													isBiTroi = true;
													currBiTroi = mSystem.currentTimeMillis();
												}
												bool flag104 = timeBiTroi > 0 && mSystem.currentTimeMillis() - currBiTroi >= 1000L;
												if (flag104)
												{
													timeBiTroi--;
													currBiTroi = mSystem.currentTimeMillis();
												}
											}
											else
											{
												isBiTroi = false;
												bool flag105 = blindEff || sleepEff;
												if (!flag105)
												{
													bool flag106 = holder;
													if (flag106)
													{
														bool flag107 = charHold != null && (charHold.statusMe == 14 || charHold.statusMe == 5);
														if (flag107)
														{
															removeHoleEff();
														}
														bool flag108 = mobHold != null && mobHold.status == 1;
														if (flag108)
														{
															removeHoleEff();
														}
														bool flag109 = me && statusMe == 2 && currentMovePoint != null;
														if (flag109)
														{
															holder = false;
															charHold = null;
															mobHold = null;
														}
														bool flag110 = TileMap.tileTypeAt(cx, cy, 2);
														if (flag110)
														{
															cf = 16;
														}
														else
														{
															cf = 31;
														}
													}
													else
													{
														bool flag111 = cHP > 0;
														if (flag111)
														{
															for (int j = 0; j < vEff.size(); j++)
															{
																EffectChar effectChar = (EffectChar)vEff.elementAt(j);
																bool flag112 = effectChar.template.type == 0 || effectChar.template.type == 12;
																if (flag112)
																{
																	bool isEff = GameCanvas.isEff1;
																	if (isEff)
																	{
																		cHP += (int)effectChar.param;
																		cMP += (int)effectChar.param;
																	}
																}
																else
																{
																	bool flag113 = effectChar.template.type == 4 || effectChar.template.type == 17;
																	if (flag113)
																	{
																		bool isEff2 = GameCanvas.isEff1;
																		if (isEff2)
																		{
																			cHP += (int)effectChar.param;
																		}
																	}
																	else
																	{
																		bool flag114 = effectChar.template.type == 13 && GameCanvas.isEff1;
																		if (flag114)
																		{
																			cHP -= cHPFull * 3 / 100;
																			bool flag115 = cHP < 1;
																			if (flag115)
																			{
																				cHP = 1;
																			}
																		}
																	}
																}
															}
															bool flag116 = eff5BuffHp > 0 && GameCanvas.isEff2;
															if (flag116)
															{
																cHP += eff5BuffHp;
															}
															bool flag117 = eff5BuffMp > 0 && GameCanvas.isEff2;
															if (flag117)
															{
																cMP += eff5BuffMp;
															}
															bool flag118 = cHP > cHPFull;
															if (flag118)
															{
																cHP = cHPFull;
															}
															bool flag119 = cMP > cMPFull;
															if (flag119)
															{
																cMP = cMPFull;
															}
														}
														bool flag120 = cmtoChar;
														if (flag120)
														{
															GameScr.cmtoX = cx - GameScr.gW2;
															GameScr.cmtoY = cy - GameScr.gH23;
															bool flag121 = !GameCanvas.isTouchControl;
															if (flag121)
															{
																GameScr.cmtoX += GameScr.gW6 * cdir;
															}
														}
														tick = (tick + 1) % 100;
														bool flag122 = me;
														if (flag122)
														{
															bool flag123 = charFocus != null && !GameScr.vCharInMap.contains(charFocus);
															if (flag123)
															{
																charFocus = null;
															}
															bool flag124 = cx < 10;
															if (flag124)
															{
																cvx = 0;
																cx = 10;
															}
															else
															{
																bool flag125 = cx > TileMap.pxw - 10;
																if (flag125)
																{
																	cx = TileMap.pxw - 10;
																	cvx = 0;
																}
															}
															bool flag126 = me && !global::Char.ischangingMap && isInWaypoint() && !VuDang.khoamap;
															if (flag126)
															{
																Service.gI().charMove();
																bool flag127 = TileMap.isTrainingMap();
																if (flag127)
																{
																	Service.gI().getMapOffline();
																	global::Char.ischangingMap = true;
																}
																else
																{
																	Service.gI().requestChangeMap();
																}
																global::Char.isLockKey = true;
																global::Char.ischangingMap = true;
																GameCanvas.clearKeyHold();
																GameCanvas.clearKeyPressed();
																InfoDlg.showWait();
																return;
															}
															bool flag128 = statusMe != 4 && Res.abs(cx - cxSend) + Res.abs(cy - cySend) >= 70 && cy - cySend <= 0 && me;
															if (flag128)
															{
																Service.gI().charMove();
															}
															bool flag129 = isLockMove;
															if (flag129)
															{
																currentMovePoint = null;
															}
															bool flag130 = currentMovePoint != null;
															if (flag130)
															{
																bool flag131 = global::Char.abs(cx - currentMovePoint.xEnd) <= 16 && global::Char.abs(cy - currentMovePoint.yEnd) <= 16;
																if (flag131)
																{
																	cx = (currentMovePoint.xEnd + cx) / 2;
																	cy = currentMovePoint.yEnd;
																	currentMovePoint = null;
																	GameScr.instance.clickMoving = false;
																	checkPerformEndMovePointAction();
																	cvx = (cvy = 0);
																	bool flag132 = (TileMap.tileTypeAtPixel(cx, cy) & 2) == 2;
																	if (flag132)
																	{
																		statusMe = 1;
																	}
																	else
																	{
																		setCharFallFromJump();
																	}
																	Service.gI().charMove();
																}
																else
																{
																	cdir = ((currentMovePoint.xEnd > cx) ? 1 : -1);
																	bool flag133 = TileMap.tileTypeAt(cx, cy, 2);
																	if (flag133)
																	{
																		statusMe = 2;
																		bool flag134 = currentMovePoint != null;
																		if (flag134)
																		{
																			cvx = cspeed * cdir;
																			cvy = 0;
																		}
																		bool flag135 = global::Char.abs(cx - currentMovePoint.xEnd) <= 10;
																		if (flag135)
																		{
																			bool flag136 = currentMovePoint.yEnd > cy;
																			if (flag136)
																			{
																				currentMovePoint = null;
																				GameScr.instance.clickMoving = false;
																				statusMe = 1;
																				cvx = (cvy = 0);
																				checkPerformEndMovePointAction();
																			}
																			else
																			{
																				SoundMn.gI().charJump();
																				cx = currentMovePoint.xEnd;
																				statusMe = 10;
																				cvy = -5;
																				cvx = 0;
																			}
																		}
																		bool flag137 = cdir == 1;
																		if (flag137)
																		{
																			bool flag138 = TileMap.tileTypeAt(cx + chw, cy - chh, 4);
																			if (flag138)
																			{
																				cvx = cspeed * cdir;
																				statusMe = 10;
																				cvy = -5;
																			}
																		}
																		else
																		{
																			bool flag139 = TileMap.tileTypeAt(cx - chw - 1, cy - chh, 8);
																			if (flag139)
																			{
																				cvx = cspeed * cdir;
																				statusMe = 10;
																				cvy = -5;
																			}
																		}
																	}
																	else
																	{
																		bool flag140 = currentMovePoint.yEnd < cy + 10;
																		if (flag140)
																		{
																			statusMe = 10;
																			cvy = -5;
																			bool flag141 = global::Char.abs(cy - currentMovePoint.yEnd) <= 10;
																			if (flag141)
																			{
																				cy = currentMovePoint.yEnd;
																				cvy = 0;
																			}
																			bool flag142 = global::Char.abs(cx - currentMovePoint.xEnd) <= 10;
																			if (flag142)
																			{
																				cvx = 0;
																			}
																			else
																			{
																				cvx = cspeed * cdir;
																			}
																		}
																		else
																		{
																			bool flag143 = TileMap.tileTypeAt(cx, cy, 2);
																			if (flag143)
																			{
																				currentMovePoint = null;
																				GameScr.instance.clickMoving = false;
																				statusMe = 1;
																				cvx = (cvy = 0);
																				checkPerformEndMovePointAction();
																			}
																			else
																			{
																				bool flag144 = statusMe == 10 || statusMe == 2;
																				if (flag144)
																				{
																					cvy = 0;
																				}
																				statusMe = 4;
																			}
																		}
																		bool flag145 = currentMovePoint.yEnd > cy;
																		if (flag145)
																		{
																			bool flag146 = cdir == 1;
																			if (flag146)
																			{
																				bool flag147 = TileMap.tileTypeAt(cx + chw, cy - chh, 4);
																				if (flag147)
																				{
																					cvx = (cvy = 0);
																					statusMe = 4;
																					currentMovePoint = null;
																					GameScr.instance.clickMoving = false;
																					checkPerformEndMovePointAction();
																				}
																			}
																			else
																			{
																				bool flag148 = TileMap.tileTypeAt(cx - chw - 1, cy - chh, 8);
																				if (flag148)
																				{
																					cvx = (cvy = 0);
																					statusMe = 4;
																					currentMovePoint = null;
																					GameScr.instance.clickMoving = false;
																					checkPerformEndMovePointAction();
																				}
																			}
																		}
																	}
																}
															}
															searchFocus();
														}
														else
														{
															checkHideCharName();
															bool flag149 = statusMe == 1 || statusMe == 6;
															if (flag149)
															{
																bool flag150 = false;
																bool flag151 = currentMovePoint != null;
																if (flag151)
																{
																	bool flag152 = global::Char.abs(currentMovePoint.xEnd - cx) < 17 && global::Char.abs(currentMovePoint.yEnd - cy) < 25;
																	if (flag152)
																	{
																		cx = currentMovePoint.xEnd;
																		cy = currentMovePoint.yEnd;
																		currentMovePoint = null;
																		bool flag153 = (TileMap.tileTypeAtPixel(cx, cy) & 2) == 2;
																		if (flag153)
																		{
																			statusMe = 1;
																			cp3 = 0;
																			GameCanvas.gI().startDust(-1, cx - -8, cy);
																			GameCanvas.gI().startDust(1, cx - 8, cy);
																		}
																		else
																		{
																			statusMe = 4;
																			cvy = 0;
																			cp1 = 0;
																		}
																		flag150 = true;
																	}
																	else
																	{
																		bool flag154 = (statusBeforeNothing == 10 || cf == 8) && vMovePoints.size() > 0;
																		if (flag154)
																		{
																			flag150 = true;
																		}
																		else
																		{
																			bool flag155 = cy == currentMovePoint.yEnd;
																			if (flag155)
																			{
																				bool flag156 = cx != currentMovePoint.xEnd;
																				if (flag156)
																				{
																					cx = (cx + currentMovePoint.xEnd) / 2;
																					cf = GameCanvas.gameTick % 5 + 2;
																				}
																			}
																			else
																			{
																				bool flag157 = cy < currentMovePoint.yEnd;
																				if (flag157)
																				{
																					cf = 12;
																					cx = (cx + currentMovePoint.xEnd) / 2;
																					bool flag158 = cvy < 0;
																					if (flag158)
																					{
																						cvy = 0;
																					}
																					cy += cvy;
																					bool flag159 = (TileMap.tileTypeAtPixel(cx, cy) & 2) == 2;
																					if (flag159)
																					{
																						GameCanvas.gI().startDust(-1, cx - -8, cy);
																						GameCanvas.gI().startDust(1, cx - 8, cy);
																					}
																					cvy++;
																					bool flag160 = cvy > 16;
																					if (flag160)
																					{
																						cy = (cy + currentMovePoint.yEnd) / 2;
																					}
																				}
																				else
																				{
																					cf = 7;
																					cx = (cx + currentMovePoint.xEnd) / 2;
																					cy = (cy + currentMovePoint.yEnd) / 2;
																				}
																			}
																		}
																	}
																}
																else
																{
																	flag150 = true;
																}
																bool flag161 = flag150 && vMovePoints.size() > 0;
																if (flag161)
																{
																	currentMovePoint = (MovePoint)vMovePoints.firstElement();
																	vMovePoints.removeElementAt(0);
																	bool flag162 = currentMovePoint.status == 2;
																	if (flag162)
																	{
																		bool flag163 = (TileMap.tileTypeAtPixel(cx, cy + 12) & 2) != 2;
																		if (flag163)
																		{
																			statusMe = 10;
																			cp1 = 0;
																			cp2 = 0;
																			cvx = -(cx - currentMovePoint.xEnd) / 10;
																			cvy = -(cy - currentMovePoint.yEnd) / 10;
																			bool flag164 = cx - currentMovePoint.xEnd > 0;
																			if (flag164)
																			{
																				cdir = -1;
																			}
																			else
																			{
																				bool flag165 = cx - currentMovePoint.xEnd < 0;
																				if (flag165)
																				{
																					cdir = 1;
																				}
																			}
																		}
																		else
																		{
																			statusMe = 2;
																			bool flag166 = cx - currentMovePoint.xEnd > 0;
																			if (flag166)
																			{
																				cdir = -1;
																			}
																			else
																			{
																				bool flag167 = cx - currentMovePoint.xEnd < 0;
																				if (flag167)
																				{
																					cdir = 1;
																				}
																			}
																			cvx = cspeed * cdir;
																			cvy = 0;
																		}
																	}
																	else
																	{
																		bool flag168 = currentMovePoint.status == 3;
																		if (flag168)
																		{
																			bool flag169 = (TileMap.tileTypeAtPixel(cx, cy + 23) & 2) != 2;
																			if (flag169)
																			{
																				statusMe = 10;
																				cp1 = 0;
																				cp2 = 0;
																				cvx = -(cx - currentMovePoint.xEnd) / 10;
																				cvy = -(cy - currentMovePoint.yEnd) / 10;
																				bool flag170 = cx - currentMovePoint.xEnd > 0;
																				if (flag170)
																				{
																					cdir = -1;
																				}
																				else
																				{
																					bool flag171 = cx - currentMovePoint.xEnd < 0;
																					if (flag171)
																					{
																						cdir = 1;
																					}
																				}
																			}
																			else
																			{
																				statusMe = 3;
																				GameCanvas.gI().startDust(-1, cx - -8, cy);
																				GameCanvas.gI().startDust(1, cx - 8, cy);
																				bool flag172 = cx - currentMovePoint.xEnd > 0;
																				if (flag172)
																				{
																					cdir = -1;
																				}
																				else
																				{
																					bool flag173 = cx - currentMovePoint.xEnd < 0;
																					if (flag173)
																					{
																						cdir = 1;
																					}
																				}
																				cvx = global::Char.abs(cx - currentMovePoint.xEnd) / 10 * cdir;
																				cvy = -10;
																			}
																		}
																		else
																		{
																			bool flag174 = currentMovePoint.status == 4;
																			if (flag174)
																			{
																				statusMe = 4;
																				bool flag175 = cx - currentMovePoint.xEnd > 0;
																				if (flag175)
																				{
																					cdir = -1;
																				}
																				else
																				{
																					bool flag176 = cx - currentMovePoint.xEnd < 0;
																					if (flag176)
																					{
																						cdir = 1;
																					}
																				}
																				cvx = global::Char.abs(cx - currentMovePoint.xEnd) / 9 * cdir;
																				cvy = 0;
																			}
																			else
																			{
																				cx = currentMovePoint.xEnd;
																				cy = currentMovePoint.yEnd;
																				currentMovePoint = null;
																			}
																		}
																	}
																}
															}
														}
														switch (statusMe)
														{
														case 1:
															updateCharStand();
															break;
														case 2:
															updateCharRun();
															break;
														case 3:
															updateCharJump();
															break;
														case 4:
															updateCharFall();
															break;
														case 5:
															updateCharDeadFly();
															break;
														case 6:
														{
															bool flag177 = isInjure <= 0;
															if (flag177)
															{
																cf = 0;
															}
															else
															{
																bool flag178 = statusBeforeNothing == 10;
																if (flag178)
																{
																	cx += cvx;
																}
																else
																{
																	bool flag179 = cf <= 1;
																	if (flag179)
																	{
																		cp1++;
																		bool flag180 = cp1 > 6;
																		if (flag180)
																		{
																			cf = 0;
																		}
																		else
																		{
																			cf = 1;
																		}
																		bool flag181 = cp1 > 10;
																		if (flag181)
																		{
																			cp1 = 0;
																		}
																	}
																}
															}
															bool flag182 = cf != 7 && cf != 12 && (TileMap.tileTypeAtPixel(cx, cy + 1) & 2) != 2;
															if (flag182)
															{
																cvx = 0;
																cvy = 0;
																statusMe = 4;
																cf = 7;
															}
															bool flag183 = me;
															if (!flag183)
															{
																cp3++;
																bool flag184 = cp3 > 10;
																if (flag184)
																{
																	bool flag185 = (TileMap.tileTypeAtPixel(cx, cy + 1) & 2) != 2;
																	if (flag185)
																	{
																		cy += 5;
																	}
																	else
																	{
																		cf = 0;
																	}
																}
																bool flag186 = cp3 > 50;
																if (flag186)
																{
																	cp3 = 0;
																	currentMovePoint = null;
																}
															}
															break;
														}
														case 9:
															updateCharAutoJump();
															break;
														case 10:
															updateCharFly();
															break;
														case 12:
															updateSkillStand();
															break;
														case 13:
															updateSkillFall();
															break;
														case 14:
														{
															cp1++;
															bool flag187 = cp1 > 30;
															if (flag187)
															{
																cp1 = 0;
															}
															bool flag188 = cp1 % 15 < 5;
															if (flag188)
															{
																cf = 0;
															}
															else
															{
																cf = 1;
															}
															break;
														}
														case 16:
															updateResetPoint();
															break;
														}
														bool flag189 = isInjure > 0;
														if (flag189)
														{
															cf = 23;
															isInjure -= 1;
														}
														bool flag190 = wdx != 0 || wdy != 0;
														if (flag190)
														{
															startDie(wdx, wdy);
															wdx = 0;
															wdy = 0;
														}
														bool flag191 = moveFast != null;
														if (flag191)
														{
															bool flag192 = moveFast[0] == 0;
															if (flag192)
															{
																short[] array = moveFast;
																int num5 = 0;
																array[num5] += 1;
																ServerEffect.addServerEffect(60, this, 1);
															}
															else
															{
																bool flag193 = moveFast[0] < 10;
																if (flag193)
																{
																	short[] array2 = moveFast;
																	int num6 = 0;
																	array2[num6] += 1;
																}
																else
																{
																	cx = (int)moveFast[1];
																	cy = (int)moveFast[2];
																	moveFast = null;
																	ServerEffect.addServerEffect(60, this, 1);
																	bool flag194 = me;
																	if (flag194)
																	{
																		bool flag195 = (TileMap.tileTypeAtPixel(cx, cy) & 2) != 2;
																		if (flag195)
																		{
																			statusMe = 4;
																			global::Char.myCharz().setAutoSkillPaint(GameScr.sks[38], 1);
																		}
																		else
																		{
																			Service.gI().charMove();
																			global::Char.myCharz().setAutoSkillPaint(GameScr.sks[38], 0);
																		}
																	}
																}
															}
														}
														bool flag196 = statusMe != 10;
														if (flag196)
														{
															fy = 0;
														}
														bool flag197 = isCharge;
														if (flag197)
														{
															cf = 17;
															bool flag198 = GameCanvas.gameTick % 4 == 0;
															if (flag198)
															{
																ServerEffect.addServerEffect(1, cx, cy + GameCanvas.transY, 1);
															}
															bool flag199 = me;
															if (flag199)
															{
																long num7 = mSystem.currentTimeMillis();
																bool flag200 = num7 - last >= 1000L;
																if (flag200)
																{
																	Res.outz("%= " + myskill.damage);
																	last = num7;
																	cHP += cHPFull * (int)myskill.damage / 100;
																	cMP += cMPFull * (int)myskill.damage / 100;
																	bool flag201 = cHP < cHPFull;
																	if (flag201)
																	{
																		GameScr.startFlyText(string.Concat(new object[]
																		{
																			"+",
																			cHPFull * (int)myskill.damage / 100,
																			" ",
																			mResources.HP
																		}), cx, cy - ch - 20, 0, -1, mFont.HP);
																	}
																	bool flag202 = cMP < cMPFull;
																	if (flag202)
																	{
																		GameScr.startFlyText(string.Concat(new object[]
																		{
																			"+",
																			cMPFull * (int)myskill.damage / 100,
																			" ",
																			mResources.KI
																		}), cx, cy - ch - 20, 0, -2, mFont.MP);
																	}
																	Service.gI().skill_not_focus(2);
																}
															}
														}
														bool flag203 = isFlyUp;
														if (flag203)
														{
															bool flag204 = me;
															if (flag204)
															{
																global::Char.isLockKey = true;
																statusMe = 3;
																cvy = -8;
																bool flag205 = cy <= TileMap.pxh - 240;
																if (flag205)
																{
																	isFlyUp = false;
																	global::Char.isLockKey = false;
																	statusMe = 4;
																}
															}
															else
															{
																statusMe = 3;
																cvy = -8;
																bool flag206 = cy <= TileMap.pxh - 240;
																if (flag206)
																{
																	cvy = 0;
																	isFlyUp = false;
																	cvy = 0;
																	statusMe = 1;
																}
															}
														}
														updateMount();
														updEffChar();
														updateEye();
														updateFHead();
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060000AD RID: 173 RVA: 0x0000FF20 File Offset: 0x0000E120
	private void updateEffect()
	{
		bool flag = effPaints != null;
		if (flag)
		{
			for (int i = 0; i < effPaints.Length; i++)
			{
				bool flag2 = effPaints[i] == null;
				if (!flag2)
				{
					bool flag3 = effPaints[i].eMob != null;
					if (flag3)
					{
						bool flag4 = !effPaints[i].isFly;
						if (flag4)
						{
							effPaints[i].eMob.setInjure();
							effPaints[i].eMob.injureBy = this;
							bool flag5 = me;
							if (flag5)
							{
								effPaints[i].eMob.hpInjure = global::Char.myCharz().cDamFull / 2 - global::Char.myCharz().cDamFull * NinjaUtil.randomNumber(11) / 100;
							}
							int num = effPaints[i].eMob.h >> 1;
							bool flag6 = effPaints[i].eMob.isBigBoss();
							if (flag6)
							{
								num = effPaints[i].eMob.getY() + 20;
							}
							GameScr.startSplash(effPaints[i].eMob.x, effPaints[i].eMob.y - num, cdir);
							effPaints[i].isFly = true;
						}
					}
					else
					{
						bool flag7 = effPaints[i].eChar != null && !effPaints[i].isFly;
						if (flag7)
						{
							bool flag8 = effPaints[i].eChar.charID >= 0;
							if (flag8)
							{
								effPaints[i].eChar.doInjure();
							}
							GameScr.startSplash(effPaints[i].eChar.cx, effPaints[i].eChar.cy - (effPaints[i].eChar.ch >> 1), cdir);
							effPaints[i].isFly = true;
						}
					}
					effPaints[i].index++;
					bool flag9 = effPaints[i].index >= effPaints[i].effCharPaint.arrEfInfo.Length;
					if (flag9)
					{
						effPaints[i] = null;
					}
				}
			}
		}
		bool flag10 = indexEff >= 0 && eff != null && GameCanvas.gameTick % 2 == 0;
		if (flag10)
		{
			indexEff++;
			bool flag11 = indexEff >= eff.arrEfInfo.Length;
			if (flag11)
			{
				indexEff = -1;
				eff = null;
			}
		}
		bool flag12 = indexEffTask >= 0 && effTask != null && GameCanvas.gameTick % 2 == 0;
		if (flag12)
		{
			indexEffTask++;
			bool flag13 = indexEffTask >= effTask.arrEfInfo.Length;
			if (flag13)
			{
				indexEffTask = -1;
				effTask = null;
			}
		}
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00010264 File Offset: 0x0000E464
	private void checkPerformEndMovePointAction()
	{
		bool flag = endMovePointCommand != null;
		if (flag)
		{
			Command command = endMovePointCommand;
			endMovePointCommand = null;
			command.performAction();
		}
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00010298 File Offset: 0x0000E498
	private void checkHideCharName()
	{
		bool flag = GameCanvas.gameTick % 20 != 0 || charID < 0;
		if (!flag)
		{
			paintName = true;
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = null;
				try
				{
					@char = (global::Char)GameScr.vCharInMap.elementAt(i);
				}
				catch (Exception)
				{
				}
				bool flag2 = @char != null && !@char.Equals(this) && ((@char.cy == cy && Res.abs(@char.cx - cx) < 35) || (cy - @char.cy < 32 && cy - @char.cy > 0 && Res.abs(@char.cx - cx) < 24));
				if (flag2)
				{
					paintName = false;
				}
			}
			for (int j = 0; j < GameScr.vNpc.size(); j++)
			{
				Npc npc = null;
				try
				{
					npc = (Npc)GameScr.vNpc.elementAt(j);
				}
				catch (Exception)
				{
				}
				bool flag3 = npc != null && npc.cy == cy && Res.abs(npc.cx - cx) < 24;
				if (flag3)
				{
					paintName = false;
				}
			}
		}
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00010424 File Offset: 0x0000E624
	private void updateMobMe()
	{
		bool flag = tMobMeBorn != 0;
		if (flag)
		{
			tMobMeBorn--;
		}
		bool flag2 = tMobMeBorn == 0;
		if (flag2)
		{
			mobMe.xFirst = ((cdir != 1) ? (cx + 30) : (cx - 30));
			mobMe.yFirst = cy - 60;
			int num = mobMe.xFirst - mobMe.x;
			int num2 = mobMe.yFirst - mobMe.y;
			mobMe.x += num / 4;
			mobMe.y += num2 / 4;
			mobMe.dir = cdir;
		}
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00010508 File Offset: 0x0000E708
	private void updateSkillPaint()
	{
		bool flag = statusMe == 14 || statusMe == 5;
		if (!flag)
		{
			bool flag2 = skillPaint != null && ((charFocus != null && isMeCanAttackOtherPlayer(charFocus) && charFocus.statusMe == 14) || (mobFocus != null && mobFocus.status == 0));
			if (flag2)
			{
				bool flag3 = !me;
				if (flag3)
				{
					bool flag4 = (TileMap.tileTypeAtPixel(cx, cy) & 2) == 2;
					if (flag4)
					{
						statusMe = 1;
					}
					else
					{
						statusMe = 6;
					}
					cp3 = 0;
				}
				indexSkill = 0;
				skillPaint = null;
				skillPaintRandomPaint = null;
				eff0 = (eff1 = (eff2 = null));
				i0 = (i1 = (i2 = 0));
				mobFocus = null;
				charFocus = null;
				effPaints = null;
				currentMovePoint = null;
				arr = null;
				bool flag5 = (TileMap.tileTypeAtPixel(cx, cy) & 2) != 2;
				if (flag5)
				{
					delayFall = 5;
				}
			}
			bool flag6 = skillPaint != null && arr == null && skillInfoPaint() != null && indexSkill >= skillInfoPaint().Length;
			if (flag6)
			{
				bool flag7 = !me;
				if (flag7)
				{
					bool flag8 = (TileMap.tileTypeAtPixel(cx, cy) & 2) == 2;
					if (flag8)
					{
						statusMe = 1;
					}
					else
					{
						statusMe = 6;
					}
					cp3 = 0;
				}
				indexSkill = 0;
				Res.outz("remove 2");
				skillPaint = null;
				skillPaintRandomPaint = null;
				eff0 = (eff1 = (eff2 = null));
				i0 = (i1 = (i2 = 0));
				arr = null;
				bool flag9 = (TileMap.tileTypeAtPixel(cx, cy) & 2) != 2;
				if (flag9)
				{
					delayFall = 5;
				}
			}
			SkillInfoPaint[] array = skillInfoPaint();
			bool flag10 = array == null || indexSkill < 0 || indexSkill > array.Length - 1;
			if (!flag10)
			{
				bool flag11 = array[indexSkill].effS0Id != 0;
				if (flag11)
				{
					eff0 = GameScr.efs[array[indexSkill].effS0Id - 1];
					i0 = (dx0 = (dy0 = 0));
				}
				bool flag12 = array[indexSkill].effS1Id != 0;
				if (flag12)
				{
					eff1 = GameScr.efs[array[indexSkill].effS1Id - 1];
					i1 = (dx1 = (dy1 = 0));
				}
				bool flag13 = array[indexSkill].effS2Id != 0;
				if (flag13)
				{
					eff2 = GameScr.efs[array[indexSkill].effS2Id - 1];
					i2 = (dx2 = (dy2 = 0));
				}
				SkillInfoPaint[] array2 = array;
				int num = indexSkill;
				bool flag14 = array2 != null && array2[num] != null && num >= 0 && num <= array2.Length - 1 && array2[num].arrowId != 0;
				if (flag14)
				{
					int arrowId = array2[num].arrowId;
					bool flag15 = arrowId >= 100;
					if (flag15)
					{
						bool flag16 = mobFocus == null;
						object obj;
						if (flag16)
						{
							IMapObject mapObject = charFocus;
							obj = mapObject;
						}
						else
						{
							obj = mobFocus;
						}
						IMapObject mapObject2 = (IMapObject)obj;
						bool flag17 = mapObject2 != null;
						if (flag17)
						{
							int num2 = Res.abs(mapObject2.getX() - cx);
							int num3 = Res.abs(mapObject2.getY() - cy);
							bool flag18 = num2 > 4 * num3;
							int num4;
							if (flag18)
							{
								num4 = 0;
							}
							else
							{
								num4 = ((mapObject2.getY() >= cy) ? 3 : -3);
								bool flag19 = mapObject2 is BigBoss;
								if (flag19)
								{
									BigBoss bigBoss = (BigBoss)mapObject2;
									bool haftBody = bigBoss.haftBody;
									if (haftBody)
									{
										num4 = -20;
									}
								}
							}
							dart = new PlayerDart(this, arrowId - 100, skillPaintRandomPaint, cx + (array2[num].adx - 10) * cdir, cy + array2[num].ady + num4);
							bool flag20 = myskill != null;
							if (flag20)
							{
								bool flag21 = myskill.template.id == 1;
								if (flag21)
								{
									SoundMn.gI().traidatKame();
								}
								else
								{
									bool flag22 = myskill.template.id == 3;
									if (flag22)
									{
										SoundMn.gI().namekKame();
									}
									else
									{
										bool flag23 = myskill.template.id == 5;
										if (flag23)
										{
											SoundMn.gI().xaydaKame();
										}
										else
										{
											bool flag24 = myskill.template.id == 11;
											if (flag24)
											{
												SoundMn.gI().nameLazer();
											}
										}
									}
								}
							}
						}
						else
						{
							bool flag25 = isFlyAndCharge || isUseSkillAfterCharge;
							if (flag25)
							{
								stopUseChargeSkill();
							}
						}
					}
					else
					{
						Res.outz("g");
						arr = new Arrow(this, GameScr.arrs[arrowId - 1]);
						arr.life = 10;
						arr.ax = cx + array2[num].adx;
						arr.ay = cy + array2[num].ady;
					}
				}
				bool flag26 = (mobFocus != null || (!me && charFocus != null) || (me && charFocus != null && (isMeCanAttackOtherPlayer(charFocus) || isSelectingSkillBuffToPlayer()) && arr == null && dart == null)) && indexSkill == array.Length - 1;
				if (flag26)
				{
					setAttack();
					bool flag27 = me && myskill.template.isAttackSkill();
					if (flag27)
					{
						saveLoadPreviousSkill();
					}
				}
				bool flag28 = me;
				if (!flag28)
				{
					IMapObject mapObject3 = null;
					bool flag29 = mobFocus != null;
					if (flag29)
					{
						mapObject3 = mobFocus;
					}
					else
					{
						bool flag30 = charFocus != null;
						if (flag30)
						{
							mapObject3 = charFocus;
						}
					}
					bool flag31 = mapObject3 == null;
					if (!flag31)
					{
						bool flag32 = Res.abs(mapObject3.getX() - cx) < 10;
						if (flag32)
						{
							bool flag33 = mapObject3.getX() > cx;
							if (flag33)
							{
								cx -= 10;
							}
							else
							{
								cx += 10;
							}
						}
						bool flag34 = mapObject3.getX() > cx;
						if (flag34)
						{
							cdir = 1;
						}
						else
						{
							cdir = -1;
						}
					}
				}
			}
		}
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void saveLoadPreviousSkill()
	{
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x00010CA0 File Offset: 0x0000EEA0
	public void setResetPoint(int x, int y)
	{
		InfoDlg.hide();
		currentMovePoint = null;
		int num = cx - x;
		bool flag = cy - y == 0;
		if (flag)
		{
			cx = x;
			global::Char.ischangingMap = false;
			global::Char.isLockKey = false;
		}
		else
		{
			statusMe = 16;
			cp2 = x;
			cp3 = y;
			cp1 = 0;
			global::Char.myCharz().cxSend = x;
			global::Char.myCharz().cySend = y;
		}
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00010D1C File Offset: 0x0000EF1C
	private void updateCharDeadFly()
	{
		isFreez = false;
		bool flag = isCharge;
		if (flag)
		{
			isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		cp1++;
		cx += (cp2 - cx) / 4;
		bool flag2 = cp1 > 7;
		if (flag2)
		{
			cy += (cp3 - cy) / 4;
		}
		else
		{
			cy += cp1 - 10;
		}
		bool flag3 = Res.abs(cp2 - cx) < 4 && Res.abs(cp3 - cy) < 10;
		if (flag3)
		{
			cx = cp2;
			cy = cp3;
			statusMe = 14;
			bool flag4 = me;
			if (flag4)
			{
				GameScr.gI().resetButton();
				Service.gI().charMove();
			}
		}
		cf = 23;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00010E48 File Offset: 0x0000F048
	private void updateResetPoint()
	{
		InfoDlg.hide();
		GameCanvas.clearAllPointerEvent();
		currentMovePoint = null;
		cp1++;
		cx += (cp2 - cx) / 4;
		bool flag = cp1 > 7;
		if (flag)
		{
			cy += (cp3 - cy) / 4;
		}
		else
		{
			cy += cp1 - 10;
		}
		bool flag2 = Res.abs(cp2 - cx) < 4 && Res.abs(cp3 - cy) < 10;
		if (flag2)
		{
			cx = cp2;
			cy = cp3;
			statusMe = 1;
			cp3 = 0;
			global::Char.ischangingMap = false;
			Service.gI().charMove();
		}
		cf = 23;
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void updateSkillFall()
	{
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00010F4C File Offset: 0x0000F14C
	public void updateSkillStand()
	{
		ty = 0;
		cp1++;
		bool flag = cdir == 1;
		if (flag)
		{
			bool flag2 = (TileMap.tileTypeAtPixel(cx + chw, cy - chh) & 4) == 4;
			if (flag2)
			{
				cvx = 0;
			}
		}
		else
		{
			bool flag3 = (TileMap.tileTypeAtPixel(cx - chw, cy - chh) & 8) == 8;
			if (flag3)
			{
				cvx = 0;
			}
		}
		bool flag4 = cy > ch && TileMap.tileTypeAt(cx, cy - ch + 24, 8192);
		if (flag4)
		{
			bool flag5 = !TileMap.tileTypeAt(cx, cy, 2);
			if (flag5)
			{
				statusMe = 4;
				cp1 = 0;
				cp2 = 0;
				cvy = 1;
			}
			else
			{
				cy = TileMap.tileYofPixel(cy);
			}
		}
		cx += cvx;
		cy += cvy;
		bool flag6 = cy < 0;
		if (flag6)
		{
			cy = (cvy = 0);
		}
		bool flag7 = cvy == 0;
		if (flag7)
		{
			bool flag8 = (TileMap.tileTypeAtPixel(cx, cy) & 2) != 2;
			if (flag8)
			{
				statusMe = 4;
				cvx = (cspeed >> 1) * cdir;
				cp1 = (cp2 = 0);
			}
		}
		else
		{
			bool flag9 = cvy < 0;
			if (flag9)
			{
				cvy++;
				bool flag10 = cvy == 0;
				if (flag10)
				{
					cvy = 1;
				}
			}
			else
			{
				bool flag11 = cvy < 20 && cp1 % 5 == 0;
				if (flag11)
				{
					cvy++;
				}
				bool flag12 = cvy > 3;
				if (flag12)
				{
					cvy = 3;
				}
				bool flag13 = (TileMap.tileTypeAtPixel(cx, cy + 3) & 2) == 2 && cy <= TileMap.tileXofPixel(cy + 3);
				if (flag13)
				{
					cvx = (cvy = 0);
					cy = TileMap.tileXofPixel(cy + 3);
				}
			}
		}
		bool flag14 = cvx > 0;
		if (flag14)
		{
			cvx--;
		}
		else
		{
			bool flag15 = cvx < 0;
			if (flag15)
			{
				cvx++;
			}
		}
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x0001123C File Offset: 0x0000F43C
	public void updateCharAutoJump()
	{
		isFreez = false;
		bool flag = isCharge;
		if (flag)
		{
			isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		cx += cvx * cdir;
		cy += cvyJump;
		cvyJump++;
		bool flag2 = cp1 == 0;
		if (flag2)
		{
			cf = 7;
		}
		else
		{
			cf = 23;
		}
		bool flag3 = cvyJump == -3;
		if (flag3)
		{
			cf = 8;
		}
		else
		{
			bool flag4 = cvyJump == -2;
			if (flag4)
			{
				cf = 9;
			}
			else
			{
				bool flag5 = cvyJump == -1;
				if (flag5)
				{
					cf = 10;
				}
				else
				{
					bool flag6 = cvyJump == 0;
					if (flag6)
					{
						cf = 11;
					}
				}
			}
		}
		bool flag7 = cvyJump == 0;
		if (flag7)
		{
			statusMe = 6;
			cp3 = 0;
			((MovePoint)vMovePoints.firstElement()).status = 4;
			isJump = true;
			cp1 = 0;
			cvy = 1;
		}
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x0001138C File Offset: 0x0000F58C
	public int getVx(int size, int dx, int dy)
	{
		bool flag = dy > 0 && !TileMap.tileTypeAt(cx, cy, 2);
		if (flag)
		{
			bool flag2 = dx - dy <= 10;
			if (flag2)
			{
				return 5;
			}
			bool flag3 = dx - dy <= 30;
			if (flag3)
			{
				return 6;
			}
			bool flag4 = dx - dy <= 50;
			if (flag4)
			{
				return 7;
			}
			bool flag5 = dx - dy <= 70;
			if (flag5)
			{
				return 8;
			}
		}
		bool flag6 = dx <= 30;
		int result;
		if (flag6)
		{
			result = 4;
		}
		else
		{
			bool flag7 = dx <= 160;
			if (flag7)
			{
				result = 5;
			}
			else
			{
				bool flag8 = dx <= 270;
				if (flag8)
				{
					result = 6;
				}
				else
				{
					bool flag9 = dx <= 320;
					if (flag9)
					{
						result = 7;
					}
					else
					{
						result = 8;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00003BDE File Offset: 0x00001DDE
	public void hide()
	{
		isHide = true;
		EffecMn.addEff(new Effect(107, cx, cy + 25, 3, 15, 1));
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00003C08 File Offset: 0x00001E08
	public void show()
	{
		isHide = false;
		EffecMn.addEff(new Effect(107, cx, cy + 25, 3, 10, 1));
	}

	// Token: 0x060000BC RID: 188 RVA: 0x0001146C File Offset: 0x0000F66C
	public int getVy(int size, int dx, int dy)
	{
		bool flag = dy <= 10;
		int result;
		if (flag)
		{
			result = 5;
		}
		else
		{
			bool flag2 = dy <= 20;
			if (flag2)
			{
				result = 6;
			}
			else
			{
				bool flag3 = dy <= 30;
				if (flag3)
				{
					result = 7;
				}
				else
				{
					bool flag4 = dy <= 40;
					if (flag4)
					{
						result = 8;
					}
					else
					{
						bool flag5 = dy <= 50;
						if (flag5)
						{
							result = 9;
						}
						else
						{
							result = 10;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x000114DC File Offset: 0x0000F6DC
	public int returnAct(int xFirst, int yFirst, int xEnd, int yEnd)
	{
		int num = xEnd - xFirst;
		int num2 = yEnd - yFirst;
		bool flag = num == 0 && num2 == 0;
		int result;
		if (flag)
		{
			result = 1;
		}
		else
		{
			bool flag2 = num2 == 0 && yFirst % 24 == 0 && TileMap.tileTypeAt(xFirst, yFirst, 2);
			if (flag2)
			{
				result = 2;
			}
			else
			{
				bool flag3 = num2 > 0 && (yFirst % 24 != 0 || !TileMap.tileTypeAt(xFirst, yFirst, 2));
				if (flag3)
				{
					result = 4;
				}
				else
				{
					cvy = -10;
					cp1 = 0;
					cdir = ((num > 0) ? 1 : -1);
					bool flag4 = num <= 5;
					if (flag4)
					{
						cvx = 0;
					}
					else
					{
						bool flag5 = num <= 10;
						if (flag5)
						{
							cvx = 3;
						}
						else
						{
							cvx = 5;
						}
					}
					result = 9;
				}
			}
		}
		return result;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x000115B0 File Offset: 0x0000F7B0
	public void setAutoJump()
	{
		int num = ((MovePoint)vMovePoints.firstElement()).xEnd - cx;
		cvyJump = -10;
		cp1 = 0;
		cdir = ((num > 0) ? 1 : -1);
		bool flag = num <= 6;
		if (flag)
		{
			cvx = 0;
		}
		else
		{
			bool flag2 = num <= 20;
			if (flag2)
			{
				cvx = 3;
			}
			else
			{
				cvx = 5;
			}
		}
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00011630 File Offset: 0x0000F830
	public void updateCharStand()
	{
		isSoundJump = false;
		isAttack = false;
		isAttFly = false;
		cvx = 0;
		cvy = 0;
		cp1++;
		bool flag = cp1 > 30;
		if (flag)
		{
			cp1 = 0;
		}
		bool flag2 = cp1 % 15 < 5;
		if (flag2)
		{
			cf = 0;
		}
		else
		{
			cf = 1;
		}
		updateCharInBridge();
		bool flag3 = !me;
		if (flag3)
		{
			cp3++;
			bool flag4 = cp3 > 50;
			if (flag4)
			{
				cp3 = 0;
				currentMovePoint = null;
			}
		}
		updateSuperEff();
		bool flag5 = !me || GameScr.vCharInMap.size() == 0 || TileMap.mapID != 50;
		if (!flag5)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(0);
			bool flag6 = !@char.changePos;
			if (flag6)
			{
				bool flag7 = @char.statusMe != 2;
				if (flag7)
				{
					@char.moveTo(cx - 45, cy, 0);
				}
				@char.lastUpdateTime = mSystem.currentTimeMillis();
				bool flag8 = Res.abs(cx - 45 - @char.cx) <= 10;
				if (flag8)
				{
					@char.changePos = true;
				}
			}
			else
			{
				bool flag9 = @char.statusMe != 2;
				if (flag9)
				{
					@char.moveTo(cx + 45, cy, 0);
				}
				@char.lastUpdateTime = mSystem.currentTimeMillis();
				bool flag10 = Res.abs(cx + 45 - @char.cx) <= 10;
				if (flag10)
				{
					@char.changePos = false;
				}
			}
			bool flag11 = GameCanvas.gameTick % 100 == 0;
			if (flag11)
			{
				@char.addInfo("Cắc cùm cum");
			}
		}
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x0001182C File Offset: 0x0000FA2C
	public void updateSuperEff()
	{
		bool flag = GameCanvas.panel.isShow || isCopy || isFusion || isSetPos || isPet || isMiniPet || isMonkey == 1;
		if (!flag)
		{
			bool flag2 = me;
			if (flag2)
			{
				bool flag3 = global::Char.isPaintAura && idAuraEff > -1;
				if (flag3)
				{
					return;
				}
			}
			else
			{
				bool flag4 = idAuraEff > -1;
				if (flag4)
				{
					return;
				}
			}
			ty++;
			bool flag5 = clevel >= 14;
			if (!flag5)
			{
				bool flag6 = clevel >= 9 && !GameCanvas.lowGraphic && (ty == 40 || ty == 50);
				if (flag6)
				{
					GameCanvas.gI().startDust(-1, cx - -8, cy);
					GameCanvas.gI().startDust(1, cx - 8, cy);
					addDustEff(1);
				}
				bool flag7 = ty <= 50 || clevel < 9;
				if (!flag7)
				{
					bool flag8 = cgender == 0;
					if (flag8)
					{
						bool flag9 = GameCanvas.gameTick % 25 == 0;
						if (flag9)
						{
							int id = 114;
							ServerEffect.addServerEffect(id, this, 1);
						}
						bool flag10 = clevel >= 13 && GameCanvas.gameTick % 4 == 0;
						if (flag10)
						{
							int id = 132;
							ServerEffect.addServerEffect(id, this, 1);
						}
					}
					bool flag11 = cgender == 1;
					if (flag11)
					{
						bool flag12 = GameCanvas.gameTick % 4 == 0;
						if (flag12)
						{
							int id = 132;
							ServerEffect.addServerEffect(id, this, 1);
						}
						bool flag13 = clevel >= 13 && GameCanvas.gameTick % 7 == 0;
						if (flag13)
						{
							int id = 131;
							ServerEffect.addServerEffect(id, this, 1);
						}
					}
					bool flag14 = cgender == 2;
					if (flag14)
					{
						bool flag15 = GameCanvas.gameTick % 7 == 0;
						if (flag15)
						{
							int id = 131;
							ServerEffect.addServerEffect(id, this, 1);
						}
						bool flag16 = clevel >= 13 && GameCanvas.gameTick % 25 == 0;
						if (flag16)
						{
							int id = 114;
							ServerEffect.addServerEffect(id, this, 1);
						}
					}
				}
			}
		}
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00011A9C File Offset: 0x0000FC9C
	public float getSoundVolumn()
	{
		bool flag = me;
		float result;
		if (flag)
		{
			result = 0.1f;
		}
		else
		{
			int num = Res.abs(global::Char.myChar.cx - cx);
			bool flag2 = num >= 0 && num <= 50;
			if (flag2)
			{
				result = 0.1f;
			}
			else
			{
				result = 0.05f;
			}
		}
		return result;
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00011AFC File Offset: 0x0000FCFC
	public void updateCharRun()
	{
		int num = (isMonkey != 1 || me) ? 1 : 2;
		bool flag = cx >= GameScr.cmx && cx <= GameScr.cmx + GameCanvas.w;
		if (flag)
		{
			bool flag2 = isMonkey == 0;
			if (flag2)
			{
				SoundMn.gI().charRun(getSoundVolumn());
			}
			else
			{
				SoundMn.gI().monkeyRun(getSoundVolumn());
			}
		}
		ty = 0;
		isFreez = false;
		bool flag3 = isCharge;
		if (flag3)
		{
			isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		int num2 = 0;
		bool flag4 = !me && currentMovePoint != null;
		if (flag4)
		{
			num2 = global::Char.abs(cx - currentMovePoint.xEnd);
		}
		cp1++;
		bool flag5 = cp1 >= 10;
		if (flag5)
		{
			cp1 = 0;
			cBonusSpeed = 0;
		}
		cf = (cp1 >> 1) + 2;
		bool flag6 = (TileMap.tileTypeAtPixel(cx, cy - 1) & 64) == 64;
		if (flag6)
		{
			cx += cvx * num >> 1;
		}
		else
		{
			cx += cvx * num;
		}
		bool flag7 = cdir == 1;
		if (flag7)
		{
			bool flag8 = TileMap.tileTypeAt(cx + chw, cy - chh, 4);
			if (flag8)
			{
				bool flag9 = me;
				if (flag9)
				{
					cvx = 0;
					cx = TileMap.tileXofPixel(cx + chw) - chw;
				}
				else
				{
					stop();
				}
			}
		}
		else
		{
			bool flag10 = TileMap.tileTypeAt(cx - chw - 1, cy - chh, 8);
			if (flag10)
			{
				bool flag11 = me;
				if (flag11)
				{
					cvx = 0;
					cx = TileMap.tileXofPixel(cx - chw - 1) + (int)TileMap.size + chw;
				}
				else
				{
					stop();
				}
			}
		}
		bool flag12 = me;
		if (flag12)
		{
			bool flag13 = cvx > 0;
			if (flag13)
			{
				cvx--;
			}
			else
			{
				bool flag14 = cvx < 0;
				if (flag14)
				{
					cvx++;
				}
				else
				{
					bool flag15 = cx - cxSend != 0 && me;
					if (flag15)
					{
						Service.gI().charMove();
					}
					statusMe = 1;
					cBonusSpeed = 0;
				}
			}
		}
		bool flag16 = (TileMap.tileTypeAtPixel(cx, cy) & 2) != 2;
		if (flag16)
		{
			bool flag17 = me;
			if (flag17)
			{
				bool flag18 = cx - cxSend != 0 || cy - cySend != 0;
				if (flag18)
				{
					Service.gI().charMove();
				}
				cf = 7;
				statusMe = 4;
				delayFall = 0;
				cvx = 3 * cdir;
				cp2 = 0;
			}
			else
			{
				stop();
			}
		}
		bool flag19 = !me && currentMovePoint != null;
		if (flag19)
		{
			int num3 = global::Char.abs(cx - currentMovePoint.xEnd);
			bool flag20 = num3 > num2;
			if (flag20)
			{
				stop();
			}
		}
		GameCanvas.gI().startDust(cdir, cx - (cdir << 3), cy);
		updateCharInBridge();
		addDustEff(2);
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00011F14 File Offset: 0x00010114
	private void stop()
	{
		statusMe = 6;
		cp3 = 0;
		cvx = 0;
		cvy = 0;
		cp1 = (cp2 = 0);
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00011F50 File Offset: 0x00010150
	public static int abs(int i)
	{
		return (i <= 0) ? (-i) : i;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00011F6C File Offset: 0x0001016C
	public void updateCharJump()
	{
		setMountIsStart();
		ty = 0;
		isFreez = false;
		bool flag = isCharge;
		if (flag)
		{
			isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		addDustEff(3);
		cx += cvx;
		cy += cvy;
		bool flag2 = cy < 0;
		if (flag2)
		{
			cy = 0;
			cvy = -1;
		}
		cvy++;
		bool flag3 = cvy > 0;
		if (flag3)
		{
			cvy = 0;
		}
		bool flag4 = !me && currentMovePoint != null;
		if (flag4)
		{
			int num = currentMovePoint.xEnd - cx;
			bool flag5 = num > 0;
			if (flag5)
			{
				bool flag6 = cvx > num;
				if (flag6)
				{
					cvx = num;
				}
				bool flag7 = cvx < 0;
				if (flag7)
				{
					cvx = num;
				}
			}
			else
			{
				bool flag8 = num < 0;
				if (flag8)
				{
					bool flag9 = cvx < num;
					if (flag9)
					{
						cvx = num;
					}
					bool flag10 = cvx > 0;
					if (flag10)
					{
						cvx = num;
					}
				}
				else
				{
					cvx = num;
				}
			}
		}
		bool flag11 = cdir == 1;
		if (flag11)
		{
			bool flag12 = (TileMap.tileTypeAtPixel(cx + chw, cy - 1) & 4) == 4 && cx <= TileMap.tileXofPixel(cx + chw) + 12;
			if (flag12)
			{
				cx = TileMap.tileXofPixel(cx + chw) - chw;
				cvx = 0;
			}
		}
		else
		{
			bool flag13 = (TileMap.tileTypeAtPixel(cx - chw, cy - 1) & 8) == 8 && cx >= TileMap.tileXofPixel(cx - chw) + 12;
			if (flag13)
			{
				cx = TileMap.tileXofPixel(cx + 24 - chw) + chw;
				cvx = 0;
			}
		}
		bool flag14 = cvy == 0;
		if (flag14)
		{
			bool flag15 = !isAttFly;
			if (flag15)
			{
				bool flag16 = me;
				if (flag16)
				{
					setCharFallFromJump();
				}
				else
				{
					stop();
				}
			}
			else
			{
				setCharFallFromJump();
			}
		}
		bool flag17 = me && !global::Char.ischangingMap && isInWaypoint() && !VuDang.khoamap;
		if (flag17)
		{
			Service.gI().charMove();
			bool flag18 = TileMap.isTrainingMap();
			if (flag18)
			{
				global::Char.ischangingMap = true;
				Service.gI().getMapOffline();
			}
			else
			{
				Service.gI().requestChangeMap();
			}
			global::Char.isLockKey = true;
			global::Char.ischangingMap = true;
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			InfoDlg.showWait();
		}
		else
		{
			bool flag19 = statusMe != 16 && (TileMap.tileTypeAt(cx, cy - ch + 24, 8192) || cy < 0);
			if (flag19)
			{
				statusMe = 4;
				cp1 = 0;
				cp2 = 0;
				cvy = 1;
				delayFall = 0;
				bool flag20 = cy < 0;
				if (flag20)
				{
					cy = 0;
				}
				cy = TileMap.tileYofPixel(cy + 25);
				GameCanvas.clearKeyHold();
			}
			bool flag21 = cp3 < 0;
			if (flag21)
			{
				cp3++;
			}
			cf = 7;
			bool flag22 = !me && currentMovePoint != null && cy < currentMovePoint.yEnd;
			if (flag22)
			{
				stop();
			}
		}
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x000123A4 File Offset: 0x000105A4
	public bool checkInRangeJump(int x1, int xw1, int xmob, int y1, int yh1, int ymob)
	{
		bool flag = xmob > xw1 || xmob < x1 || ymob > y1 || ymob < yh1;
		return !flag;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x000123D8 File Offset: 0x000105D8
	public void setCharFallFromJump()
	{
		cyStartFall = cy;
		cp1 = 0;
		cp2 = 0;
		statusMe = 10;
		cvx = cdir << 2;
		cvy = 0;
		cy = TileMap.tileYofPixel(cy) + 12;
		bool flag = me && (cx - cxSend != 0 || cy - cySend != 0) && (Res.abs(global::Char.myCharz().cx - global::Char.myCharz().cxSend) > 96 || Res.abs(global::Char.myCharz().cy - global::Char.myCharz().cySend) > 24);
		if (flag)
		{
			Service.gI().charMove();
		}
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x000124AC File Offset: 0x000106AC
	public void updateCharFall()
	{
		bool flag = holder;
		if (!flag)
		{
			ty = 0;
			bool flag2 = cy + 4 >= TileMap.pxh;
			if (flag2)
			{
				statusMe = 1;
				bool flag3 = me;
				if (flag3)
				{
					SoundMn.gI().charFall();
				}
				cvx = (cvy = 0);
				cp3 = 0;
			}
			else
			{
				bool flag4 = cy % 24 == 0 && (TileMap.tileTypeAtPixel(cx, cy) & 2) == 2;
				if (flag4)
				{
					delayFall = 0;
					bool flag5 = me;
					if (flag5)
					{
						bool flag6 = cy - cySend > 0;
						if (flag6)
						{
							Service.gI().charMove();
						}
						else
						{
							bool flag7 = cx - cxSend != 0 || cy - cySend < 0;
							if (flag7)
							{
								Service.gI().charMove();
							}
						}
						cvx = (cvy = 0);
						cp1 = (cp2 = 0);
						statusMe = 1;
						cp3 = 0;
						return;
					}
					stop();
					cf = 0;
					GameCanvas.gI().startDust(-1, cx - -8, cy);
					GameCanvas.gI().startDust(1, cx - 8, cy);
					addDustEff(1);
				}
				bool flag8 = delayFall > 0;
				if (flag8)
				{
					delayFall--;
					bool flag9 = delayFall % 10 > 5;
					if (flag9)
					{
						cy++;
					}
					else
					{
						cy--;
					}
				}
				else
				{
					bool flag10 = cvy < -4;
					if (flag10)
					{
						cf = 7;
					}
					else
					{
						cf = 12;
					}
					cx += cvx;
					bool flag11 = !me && currentMovePoint != null;
					if (flag11)
					{
						int num = currentMovePoint.xEnd - cx;
						bool flag12 = num > 0;
						if (flag12)
						{
							bool flag13 = cvx > num;
							if (flag13)
							{
								cvx = num;
							}
							bool flag14 = cvx < 0;
							if (flag14)
							{
								cvx = num;
							}
						}
						else
						{
							bool flag15 = num < 0;
							if (flag15)
							{
								bool flag16 = cvx < num;
								if (flag16)
								{
									cvx = num;
								}
								bool flag17 = cvx > 0;
								if (flag17)
								{
									cvx = num;
								}
							}
							else
							{
								cvx = num;
							}
						}
					}
					cvy++;
					bool flag18 = cvy > 8;
					if (flag18)
					{
						cvy = 8;
					}
					bool flag19 = skillPaintRandomPaint == null;
					if (flag19)
					{
						cy += cvy;
					}
					bool flag20 = cdir == 1;
					if (flag20)
					{
						bool flag21 = (TileMap.tileTypeAtPixel(cx + chw, cy - 1) & 4) == 4 && cx <= TileMap.tileXofPixel(cx + chw) + 12;
						if (flag21)
						{
							cx = TileMap.tileXofPixel(cx + chw) - chw;
							cvx = 0;
						}
					}
					else
					{
						bool flag22 = (TileMap.tileTypeAtPixel(cx - chw, cy - 1) & 8) == 8 && cx >= TileMap.tileXofPixel(cx - chw) + 12;
						if (flag22)
						{
							cx = TileMap.tileXofPixel(cx + 24 - chw) + chw;
							cvx = 0;
						}
					}
					bool flag23 = cvy > 3 && (cyStartFall == 0 || cyStartFall <= TileMap.tileYofPixel(cy + 3)) && (TileMap.tileTypeAtPixel(cx, cy + 3) & 2) == 2;
					if (flag23)
					{
						bool flag24 = me;
						if (flag24)
						{
							cyStartFall = 0;
							cvx = (cvy = 0);
							cp1 = (cp2 = 0);
							cy = TileMap.tileXofPixel(cy + 3);
							statusMe = 1;
							bool flag25 = me;
							if (flag25)
							{
								SoundMn.gI().charFall();
							}
							cp3 = 0;
							GameCanvas.gI().startDust(-1, cx - -8, cy);
							GameCanvas.gI().startDust(1, cx - 8, cy);
							addDustEff(1);
							bool flag26 = cy - cySend > 0;
							if (flag26)
							{
								bool flag27 = me;
								if (flag27)
								{
									Service.gI().charMove();
								}
							}
							else
							{
								bool flag28 = (cx - cxSend != 0 || cy - cySend < 0) && me;
								if (flag28)
								{
									Service.gI().charMove();
								}
							}
						}
						else
						{
							stop();
							cy = TileMap.tileXofPixel(cy + 3);
							cf = 0;
							GameCanvas.gI().startDust(-1, cx - -8, cy);
							GameCanvas.gI().startDust(1, cx - 8, cy);
							addDustEff(1);
						}
					}
					else
					{
						cf = 12;
						bool flag29 = me;
						if (flag29)
						{
							bool flag30 = !isAttack;
							if (flag30)
							{
							}
						}
						else
						{
							bool flag31 = (TileMap.tileTypeAtPixel(cx, cy + 1) & 2) == 2;
							if (flag31)
							{
								cf = 0;
							}
							bool flag32 = currentMovePoint != null && cy > currentMovePoint.yEnd;
							if (flag32)
							{
								stop();
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00012B18 File Offset: 0x00010D18
	public void updateCharFly()
	{
		int num = (isMonkey != 1 || me) ? 1 : 2;
		setMountIsStart();
		bool flag = statusMe != 16 && (TileMap.tileTypeAt(cx, cy - ch + 24, 8192) || cy < 0);
		if (flag)
		{
			bool flag2 = cy - ch < 0;
			if (flag2)
			{
				cy = ch;
			}
			cf = 7;
			statusMe = 4;
			cvx = 0;
			cp2 = 0;
			currentMovePoint = null;
		}
		else
		{
			int num2 = cy;
			cp1++;
			bool flag3 = cp1 >= 9;
			if (flag3)
			{
				cp1 = 0;
				bool flag4 = !me;
				if (flag4)
				{
					cvx = (cvy = 0);
				}
				cBonusSpeed = 0;
			}
			cf = 8;
			bool flag5 = Res.abs(cvx) <= 4 && me;
			if (flag5)
			{
				bool flag6 = currentMovePoint != null;
				if (flag6)
				{
					int num3 = global::Char.abs(cx - currentMovePoint.xEnd);
					int num4 = global::Char.abs(cy - currentMovePoint.yEnd);
					bool flag7 = num3 > num4 * 10;
					if (flag7)
					{
						cf = 8;
					}
					else
					{
						bool flag8 = num3 > num4 && num3 > 48 && num4 > 32;
						if (flag8)
						{
							cf = 8;
						}
						else
						{
							cf = 7;
						}
					}
				}
				else
				{
					bool flag9 = cvy < 0;
					if (flag9)
					{
						cvy = 0;
					}
					bool flag10 = cvy > 16;
					if (flag10)
					{
						cvy = 16;
					}
					cf = 7;
				}
			}
			bool flag11 = !me;
			if (flag11)
			{
				bool flag12 = global::Char.abs(cvx) < 2;
				if (flag12)
				{
					cvx = (cdir << 1) * num;
				}
				bool flag13 = cvy != 0;
				if (flag13)
				{
					cf = 7;
				}
				bool flag14 = global::Char.abs(cvx) <= 2;
				if (flag14)
				{
					cp2++;
					bool flag15 = cp2 > 32;
					if (flag15)
					{
						statusMe = 4;
						cvx = 0;
						cvy = 0;
					}
				}
			}
			bool flag16 = cdir == 1;
			if (flag16)
			{
				bool flag17 = TileMap.tileTypeAt(cx + chw, cy - 1, 4);
				if (flag17)
				{
					cvx = 0;
					cx = TileMap.tileXofPixel(cx + chw) - chw;
					bool flag18 = cvy == 0;
					if (flag18)
					{
						currentMovePoint = null;
					}
				}
			}
			else
			{
				bool flag19 = TileMap.tileTypeAt(cx - chw - 1, cy - 1, 8);
				if (flag19)
				{
					cvx = 0;
					cx = TileMap.tileXofPixel(cx - chw - 1) + (int)TileMap.size + chw;
					bool flag20 = cvy == 0;
					if (flag20)
					{
						currentMovePoint = null;
					}
				}
			}
			cx += cvx * num;
			cy += cvy * num;
			bool flag21 = !isMount && num2 - cy == 0;
			if (flag21)
			{
				ty++;
				wt++;
				fy += ((!wy) ? 1 : -1);
				bool flag22 = wt == 10;
				if (flag22)
				{
					wt = 0;
					wy = !wy;
				}
				bool flag23 = ty > 20;
				if (flag23)
				{
					delayFall = 10;
					bool flag24 = GameCanvas.gameTick % 3 == 0;
					if (flag24)
					{
						ServerEffect.addServerEffect(111, cx + ((cdir != 1) ? 27 : -17), cy + fy + 13, 1, (cdir != 1) ? 2 : 0);
					}
				}
			}
			bool flag25 = !me;
			if (!flag25)
			{
				bool flag26 = cvx > 0;
				if (flag26)
				{
					cvx--;
				}
				else
				{
					bool flag27 = cvx < 0;
					if (flag27)
					{
						cvx++;
					}
					else
					{
						bool flag28 = cvy == 0;
						if (flag28)
						{
							statusMe = 4;
							checkDelayFallIfTooHigh();
							Service.gI().charMove();
						}
					}
				}
				bool flag29 = (TileMap.tileTypeAtPixel(cx, cy + 20) & 2) == 2 || (TileMap.tileTypeAtPixel(cx, cy + 40) & 2) == 2;
				if (flag29)
				{
					bool flag30 = cvy == 0;
					if (flag30)
					{
						delayFall = 0;
					}
					cyStartFall = 0;
					cvx = (cvy = 0);
					cp1 = (cp2 = 0);
					statusMe = 4;
					addDustEff(3);
				}
				bool flag31 = global::Char.abs(cx - cxSend) > 96 || global::Char.abs(cy - cySend) > 24;
				if (flag31)
				{
					Service.gI().charMove();
				}
			}
		}
	}

	// Token: 0x060000CA RID: 202 RVA: 0x000130FC File Offset: 0x000112FC
	public void setMount(int cid, int ctrans, int cgender)
	{
		idcharMount = cid;
		transMount = ctrans;
		genderMount = cgender;
		speedMount = 30;
		bool flag = transMount < 0;
		if (flag)
		{
			transMount = 0;
			xMount = GameScr.cmx + GameCanvas.w + 50;
			dxMount = -19;
		}
		else
		{
			bool flag2 = transMount == 1;
			if (flag2)
			{
				transMount = 2;
				xMount = GameScr.cmx - 100;
				dxMount = -33;
			}
		}
		dyMount = -17;
		yMount = cy;
		frameMount = 0;
		frameNewMount = 0;
		isMount = false;
		isEndMount = false;
	}

	// Token: 0x060000CB RID: 203 RVA: 0x000131B8 File Offset: 0x000113B8
	public void updateMount()
	{
		frameMount++;
		bool flag = frameMount > FrameMount.Length - 1;
		if (flag)
		{
			frameMount = 0;
		}
		frameNewMount++;
		bool flag2 = frameNewMount > 1000;
		if (flag2)
		{
			frameNewMount = 0;
		}
		bool flag3 = isStartMount && !isMount;
		if (flag3)
		{
			yMount = cy;
			bool flag4 = transMount == 0;
			if (flag4)
			{
				bool flag5 = xMount - cx >= speedMount;
				if (flag5)
				{
					xMount -= speedMount;
				}
				else
				{
					xMount = cx;
					isMount = true;
					isEndMount = false;
				}
			}
			else
			{
				bool flag6 = transMount == 2;
				if (flag6)
				{
					bool flag7 = cx - xMount >= speedMount;
					if (flag7)
					{
						xMount += speedMount;
					}
					else
					{
						xMount = cx;
						isMount = true;
						isEndMount = false;
					}
				}
			}
		}
		else
		{
			bool flag8 = isMount;
			if (flag8)
			{
				bool flag9 = statusMe == 14 || ySd - cy < 24;
				if (flag9)
				{
					setMountIsEnd();
				}
				bool flag10 = cp1 % 15 < 5;
				if (flag10)
				{
					cf = 0;
				}
				else
				{
					cf = 1;
				}
				transMount = cdir;
				updateSuperEff();
				bool flag11 = transMount < 0;
				if (flag11)
				{
					transMount = 0;
					dxMount = -19;
				}
				else
				{
					bool flag12 = transMount == 1;
					if (flag12)
					{
						transMount = 2;
						dxMount = -31;
						bool flag13 = isEventMount;
						if (flag13)
						{
							dxMount = -38;
						}
					}
				}
				bool flag14 = skillInfoPaint() != null;
				if (flag14)
				{
					dyMount = -15;
				}
				else
				{
					dyMount = -17;
				}
				yMount = cy;
				xMount = cx;
			}
			else
			{
				bool flag15 = isEndMount;
				if (flag15)
				{
					bool flag16 = transMount == 0;
					if (flag16)
					{
						bool flag17 = xMount > GameScr.cmx - 100;
						if (flag17)
						{
							xMount -= 20;
						}
						else
						{
							isStartMount = false;
							isMount = false;
							isEndMount = false;
						}
					}
					else
					{
						bool flag18 = transMount == 2;
						if (flag18)
						{
							bool flag19 = xMount < GameScr.cmx + GameCanvas.w + 50;
							if (flag19)
							{
								xMount += 20;
							}
							else
							{
								isStartMount = false;
								isMount = false;
								isEndMount = false;
							}
						}
					}
				}
				else
				{
					bool flag20 = !isStartMount || !isMount || !isEndMount;
					if (flag20)
					{
						xMount = GameScr.cmx - 100;
						yMount = GameScr.cmy - 100;
					}
				}
			}
		}
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00013518 File Offset: 0x00011718
	public void getMountData()
	{
		bool flag = Mob.arrMobTemplate[50].data == null;
		if (flag)
		{
			Mob.arrMobTemplate[50].data = new EffectData();
			string text = "/Mob/" + 50;
			DataInputStream dataInputStream = MyStream.readFile(text);
			bool flag2 = dataInputStream != null;
			if (flag2)
			{
				Mob.arrMobTemplate[50].data.readData(text + "/data");
				Mob.arrMobTemplate[50].data.img = GameCanvas.loadImage(text + "/img.png");
			}
			else
			{
				Service.gI().requestModTemplate(50);
			}
			Mob.lastMob.addElement(50 + string.Empty);
		}
	}

	// Token: 0x060000CD RID: 205 RVA: 0x000135E4 File Offset: 0x000117E4
	public void checkFrameTick(int[] array)
	{
		t++;
		bool flag = t > array.Length - 1;
		if (flag)
		{
			t = 0;
		}
		fM = array[t];
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00013628 File Offset: 0x00011828
	public void paintMount1(mGraphics g)
	{
		bool flag = xMount <= GameScr.cmx || xMount >= GameScr.cmx + GameCanvas.w;
		if (!flag)
		{
			bool flag2 = me;
			if (flag2)
			{
				bool flag3 = !isEndMount && !isStartMount && !isMount;
				if (!flag3)
				{
					bool flag4 = idMount >= global::Char.ID_NEW_MOUNT;
					if (flag4)
					{
						string nameImg = strMount + (int)(idMount - global::Char.ID_NEW_MOUNT) + "_0";
						FrameImage fraImage = mSystem.getFraImage(nameImg);
						if (fraImage != null)
						{
							fraImage.drawFrame(frameNewMount / 2 % fraImage.nFrame, xMount, yMount + fy, transMount, 3, g);
						}
					}
					else
					{
						bool flag5 = isSpeacialMount;
						if (!flag5)
						{
							bool flag6 = isEventMount;
							if (flag6)
							{
								g.drawRegion(global::Char.imgEventMountWing, 0, (int)(FrameMount[frameMount] * 60), 60, 60, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
							}
							else
							{
								bool flag7 = genderMount == 2;
								if (flag7)
								{
									bool flag8 = !isMountVip;
									if (flag8)
									{
										g.drawRegion(global::Char.imgMount_XD, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
									}
									else
									{
										g.drawRegion(global::Char.imgMount_XD_VIP, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
									}
								}
								else
								{
									bool flag9 = genderMount == 1;
									if (flag9)
									{
										bool flag10 = !isMountVip;
										if (flag10)
										{
											g.drawRegion(global::Char.imgMount_NM, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
										}
										else
										{
											g.drawRegion(global::Char.imgMount_NM_VIP, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag11 = me;
				if (!flag11)
				{
					bool flag12 = idMount >= global::Char.ID_NEW_MOUNT;
					if (flag12)
					{
						string nameImg2 = strMount + (int)(idMount - global::Char.ID_NEW_MOUNT) + "_0";
						FrameImage fraImage2 = mSystem.getFraImage(nameImg2);
						if (fraImage2 != null)
						{
							fraImage2.drawFrame(frameNewMount / 2 % fraImage2.nFrame, xMount, yMount + fy, transMount, 3, g);
						}
					}
					else
					{
						bool flag13 = isSpeacialMount;
						if (!flag13)
						{
							bool flag14 = isEventMount;
							if (flag14)
							{
								g.drawRegion(global::Char.imgEventMountWing, 0, (int)(FrameMount[frameMount] * 60), 60, 60, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
							}
							else
							{
								bool flag15 = !isMount;
								if (!flag15)
								{
									bool flag16 = genderMount == 2;
									if (flag16)
									{
										bool flag17 = !isMountVip;
										if (flag17)
										{
											g.drawRegion(global::Char.imgMount_XD, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
										}
										else
										{
											g.drawRegion(global::Char.imgMount_XD_VIP, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
										}
									}
									else
									{
										bool flag18 = genderMount == 1;
										if (flag18)
										{
											bool flag19 = !isMountVip;
											if (flag19)
											{
												g.drawRegion(global::Char.imgMount_NM, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
											}
											else
											{
												g.drawRegion(global::Char.imgMount_NM_VIP, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00013BA4 File Offset: 0x00011DA4
	public void paintMount2(mGraphics g)
	{
		bool flag = xMount <= GameScr.cmx || xMount >= GameScr.cmx + GameCanvas.w;
		if (!flag)
		{
			bool flag2 = me;
			if (flag2)
			{
				bool flag3 = !isEndMount && !isStartMount && !isMount;
				if (!flag3)
				{
					bool flag4 = idMount >= global::Char.ID_NEW_MOUNT;
					if (flag4)
					{
						string nameImg = strMount + (int)(idMount - global::Char.ID_NEW_MOUNT) + "_1";
						FrameImage fraImage = mSystem.getFraImage(nameImg);
						if (fraImage != null)
						{
							fraImage.drawFrame(frameNewMount / 2 % fraImage.nFrame, xMount, yMount + fy, transMount, 3, g);
						}
					}
					else
					{
						bool flag5 = isSpeacialMount;
						if (flag5)
						{
							checkFrameTick(move);
							bool flag6 = Mob.arrMobTemplate[50] != null && Mob.arrMobTemplate[50].data != null;
							if (flag6)
							{
								Mob.arrMobTemplate[50].data.paintFrame(g, fM, xMount + ((cdir != 1) ? 8 : -8), yMount + 35, (cdir != 1) ? 1 : 0, 0);
							}
							else
							{
								getMountData();
							}
						}
						else
						{
							bool flag7 = isEventMount;
							if (flag7)
							{
								g.drawRegion(global::Char.imgEventMount, 0, (int)(FrameMount[frameMount] * 60), 60, 60, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
							}
							else
							{
								bool flag8 = genderMount == 0;
								if (flag8)
								{
									bool flag9 = !isMountVip;
									if (flag9)
									{
										g.drawRegion(global::Char.imgMount_TD, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
									}
									else
									{
										g.drawRegion(global::Char.imgMount_TD_VIP, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
									}
								}
								else
								{
									bool flag10 = genderMount == 1;
									if (flag10)
									{
										bool flag11 = !isMountVip;
										if (flag11)
										{
											g.drawRegion(global::Char.imgMount_NM_1, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
										}
										else
										{
											g.drawRegion(global::Char.imgMount_NM_1_VIP, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag12 = me;
				if (!flag12)
				{
					bool flag13 = idMount >= global::Char.ID_NEW_MOUNT;
					if (flag13)
					{
						string nameImg2 = strMount + (int)(idMount - global::Char.ID_NEW_MOUNT) + "_1";
						FrameImage fraImage2 = mSystem.getFraImage(nameImg2);
						if (fraImage2 != null)
						{
							fraImage2.drawFrame(frameNewMount / 2 % fraImage2.nFrame, xMount, yMount + fy, transMount, 3, g);
						}
					}
					else
					{
						bool flag14 = isSpeacialMount;
						if (flag14)
						{
							checkFrameTick(move);
							bool flag15 = Mob.arrMobTemplate[50] != null && Mob.arrMobTemplate[50].data != null;
							if (flag15)
							{
								Mob.arrMobTemplate[50].data.paintFrame(g, fM, xMount + ((cdir != 1) ? 8 : -8), yMount + 35, (cdir != 1) ? 1 : 0, 0);
							}
							else
							{
								getMountData();
							}
						}
						else
						{
							bool flag16 = isEventMount;
							if (flag16)
							{
								g.drawRegion(global::Char.imgEventMount, 0, (int)(FrameMount[frameMount] * 60), 60, 60, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
							}
							bool flag17 = !isMount;
							if (!flag17)
							{
								bool flag18 = genderMount == 0;
								if (flag18)
								{
									bool flag19 = !isMountVip;
									if (flag19)
									{
										g.drawRegion(global::Char.imgMount_TD, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
									}
									else
									{
										g.drawRegion(global::Char.imgMount_TD_VIP, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
									}
								}
								else
								{
									bool flag20 = genderMount == 1;
									if (flag20)
									{
										bool flag21 = !isMountVip;
										if (flag21)
										{
											g.drawRegion(global::Char.imgMount_NM_1, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
										}
										else
										{
											g.drawRegion(global::Char.imgMount_NM_1_VIP, 0, (int)(FrameMount[frameMount] * 40), 50, 40, transMount, xMount + dxMount, yMount + dyMount + fy, 0);
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00014220 File Offset: 0x00012420
	public void setMountIsStart()
	{
		bool flag = me;
		if (flag)
		{
			isHaveMount = checkHaveMount();
			bool flag2 = TileMap.isVoDaiMap();
			if (flag2)
			{
				isHaveMount = false;
			}
		}
		bool flag3 = isHaveMount;
		if (flag3)
		{
			bool flag4 = ySd - cy <= 20;
			if (flag4)
			{
				xChar = cx;
			}
			bool flag5 = xdis < 100;
			if (flag5)
			{
				xdis = Res.abs(xChar - cx);
			}
			bool flag6 = xdis >= 70 && ySd - cy > 30 && !isStartMount && !isEndMount;
			if (flag6)
			{
				setMount(charID, cdir, cgender);
				isStartMount = true;
			}
		}
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00014310 File Offset: 0x00012510
	public void setMountIsEnd()
	{
		bool flag = ySd - cy < 24 && !isEndMount;
		if (flag)
		{
			isStartMount = false;
			isMount = false;
			isEndMount = true;
			xdis = 0;
		}
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00014360 File Offset: 0x00012560
	public bool checkHaveMount()
	{
		bool result = false;
		short num = -1;
		Item[] array = arrItemBody;
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = array[i] != null && (array[i].template.type == 24 || array[i].template.type == 23);
			if (flag)
			{
				num = ((array[i].template.part < 0) ? array[i].template.id : (global::Char.ID_NEW_MOUNT + array[i].template.part));
				result = true;
				break;
			}
		}
		isMountVip = false;
		isSpeacialMount = false;
		isEventMount = false;
		idMount = -1;
		short num2 = num;
		if (num2 - 349 > 2)
		{
			if (num2 != 396)
			{
				if (num2 != 532)
				{
					bool flag2 = num >= global::Char.ID_NEW_MOUNT;
					if (flag2)
					{
						idMount = num;
					}
				}
				else
				{
					isSpeacialMount = true;
				}
			}
			else
			{
				isEventMount = true;
			}
		}
		else
		{
			isMountVip = true;
		}
		return result;
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x0001447C File Offset: 0x0001267C
	private void checkDelayFallIfTooHigh()
	{
		bool flag = true;
		for (int i = 0; i < 150; i += 24)
		{
			bool flag2 = (TileMap.tileTypeAtPixel(cx, cy + i) & 2) == 2 || cy + i > TileMap.tmh * (int)TileMap.size - 24;
			if (flag2)
			{
				flag = false;
				break;
			}
		}
		bool flag3 = flag;
		if (flag3)
		{
			delayFall = 40;
		}
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00003C32 File Offset: 0x00001E32
	public void setDefaultPart()
	{
		setDefaultWeapon();
		setDefaultBody();
		setDefaultLeg();
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x000144F0 File Offset: 0x000126F0
	public void setDefaultWeapon()
	{
		bool flag = cgender == 0;
		if (flag)
		{
			wp = 0;
		}
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00014514 File Offset: 0x00012714
	public void setDefaultBody()
	{
		bool flag = cgender == 0;
		if (flag)
		{
			body = 57;
		}
		else
		{
			bool flag2 = cgender == 1;
			if (flag2)
			{
				body = 59;
			}
			else
			{
				bool flag3 = cgender == 2;
				if (flag3)
				{
					body = 57;
				}
			}
		}
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x0001456C File Offset: 0x0001276C
	public void setDefaultLeg()
	{
		bool flag = cgender == 0;
		if (flag)
		{
			leg = 58;
		}
		else
		{
			bool flag2 = cgender == 1;
			if (flag2)
			{
				leg = 60;
			}
			else
			{
				bool flag3 = cgender == 2;
				if (flag3)
				{
					leg = 58;
				}
			}
		}
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x000145C4 File Offset: 0x000127C4
	public bool isSelectingSkillUseAlone()
	{
		return myskill != null && myskill.template.isUseAlone();
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x000145F4 File Offset: 0x000127F4
	public bool isSelectingSkillBuffToPlayer()
	{
		return myskill != null && myskill.template.isBuffToPlayer();
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00014624 File Offset: 0x00012824
	public bool isUseChargeSkill()
	{
		return !isUseSkillAfterCharge && myskill != null && (myskill.template.id == 10 || myskill.template.id == 11);
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00014674 File Offset: 0x00012874
	public void setSkillPaint(SkillPaint skillPaint, int sType)
	{
		hasSendAttack = false;
		bool flag = stone || (me && myskill.template.id == 9 && cHP <= cHPFull / 10);
		if (!flag)
		{
			bool flag2 = me;
			if (flag2)
			{
				bool flag3 = mobFocus == null && charFocus == null;
				if (flag3)
				{
					stopUseChargeSkill();
				}
				bool flag4 = mobFocus != null && (mobFocus.status == 1 || mobFocus.status == 0);
				if (flag4)
				{
					stopUseChargeSkill();
				}
				bool flag5 = charFocus != null && (charFocus.statusMe == 14 || charFocus.statusMe == 5);
				if (flag5)
				{
					stopUseChargeSkill();
				}
				bool flag6 = (myskill.template.id == 23 && ((charFocus != null && charFocus.holdEffID != 0) || (mobFocus != null && mobFocus.holdEffID != 0) || holdEffID != 0)) || sleepEff || blindEff;
				if (flag6)
				{
					return;
				}
			}
			Res.outz("skill id= " + skillPaint.id);
			bool flag7 = (me && dart != null) || TileMap.isOfflineMap();
			if (!flag7)
			{
				long num = mSystem.currentTimeMillis();
				bool flag8 = me;
				if (flag8)
				{
					bool flag9 = isSelectingSkillBuffToPlayer() && charFocus == null;
					if (flag9)
					{
						return;
					}
					bool flag10 = num - myskill.lastTimeUseThisSkill < (long)myskill.coolDown;
					if (flag10)
					{
						myskill.paintCanNotUseSkill = true;
						return;
					}
					myskill.lastTimeUseThisSkill = num;
					bool flag11 = myskill.template.manaUseType == 2;
					if (flag11)
					{
						cMP = 1;
					}
					else
					{
						bool flag12 = myskill.template.manaUseType != 1;
						if (flag12)
						{
							cMP -= myskill.manaUse;
						}
						else
						{
							cMP -= myskill.manaUse * cMPFull / 100;
						}
					}
					global::Char.myCharz().cStamina--;
					GameScr.gI().isInjureMp = true;
					GameScr.gI().twMp = 0;
					bool flag13 = cMP < 0;
					if (flag13)
					{
						cMP = 0;
					}
				}
				bool flag14 = me;
				if (flag14)
				{
					bool flag15 = myskill.template.id == 7;
					if (flag15)
					{
						SoundMn.gI().hoisinh();
					}
					bool flag16 = myskill.template.id == 6;
					if (flag16)
					{
						Service.gI().skill_not_focus(0);
						GameScr.gI().isUseFreez = true;
						SoundMn.gI().thaiduonghasan();
					}
					bool flag17 = myskill.template.id == 8;
					if (flag17)
					{
						bool flag18 = !isCharge;
						if (flag18)
						{
							SoundMn.gI().taitaoPause();
							Service.gI().skill_not_focus(1);
							isCharge = true;
							last = (cur = mSystem.currentTimeMillis());
						}
						else
						{
							Service.gI().skill_not_focus(3);
							isCharge = false;
							SoundMn.gI().taitaoPause();
						}
					}
					bool flag19 = myskill.template.id == 13;
					if (flag19)
					{
						bool flag20 = isMonkey != 0;
						if (flag20)
						{
							GameScr.gI().auto = 0;
						}
						else
						{
							bool flag21 = !isCreateDark;
							if (flag21)
							{
								SoundMn.gI().gong();
								Service.gI().skill_not_focus(6);
								chargeCount = 0;
								isWaitMonkey = true;
							}
						}
						return;
					}
					bool flag22 = myskill.template.id == 14;
					if (flag22)
					{
						SoundMn.gI().gong();
						Service.gI().skill_not_focus(7);
						useChargeSkill(true);
					}
					bool flag23 = myskill.template.id == 21;
					if (flag23)
					{
						Service.gI().skill_not_focus(10);
						return;
					}
					bool flag24 = myskill.template.id == 12;
					if (flag24)
					{
						Service.gI().skill_not_focus(8);
					}
					bool flag25 = myskill.template.id == 19;
					if (flag25)
					{
						Service.gI().skill_not_focus(9);
						return;
					}
				}
				bool flag26 = isMonkey == 1 && skillPaint.id >= 35 && skillPaint.id <= 41;
				if (flag26)
				{
					skillPaint = GameScr.sks[106];
				}
				bool flag27 = skillPaint.id >= 128 && skillPaint.id <= 134;
				if (flag27)
				{
					skillPaint = GameScr.sks[skillPaint.id - 65];
					bool flag28 = charFocus != null;
					if (flag28)
					{
						cx = charFocus.cx;
						cy = charFocus.cy;
						currentMovePoint = null;
					}
					bool flag29 = mobFocus != null;
					if (flag29)
					{
						cx = mobFocus.x;
						cy = mobFocus.y;
						currentMovePoint = null;
					}
					ServerEffect.addServerEffect(60, cx, cy, 1);
					telePortSkill = true;
				}
				bool flag30 = skillPaint.id >= 107 && skillPaint.id <= 113;
				if (flag30)
				{
					skillPaint = GameScr.sks[skillPaint.id - 44];
					EffecMn.addEff(new Effect(23, cx, cy + ch / 2, 3, 2, 1));
				}
				setAutoSkillPaint(skillPaint, sType);
			}
		}
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00014CD8 File Offset: 0x00012ED8
	public void useSkillNotFocus()
	{
		GameScr.gI().auto = 0;
		global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], (!TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2)) ? 1 : 0);
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00014D34 File Offset: 0x00012F34
	public void sendUseChargeSkill()
	{
		bool flag = me && (isFreez || isUsePlane);
		if (flag)
		{
			GameScr.gI().auto = 0;
		}
		else
		{
			long num = mSystem.currentTimeMillis();
			bool flag2 = me && num - myskill.lastTimeUseThisSkill < (long)myskill.coolDown;
			if (flag2)
			{
				myskill.paintCanNotUseSkill = true;
			}
			else
			{
				bool flag3 = myskill.template.id == 10;
				if (flag3)
				{
					useChargeSkill(false);
				}
				bool flag4 = myskill.template.id == 11;
				if (flag4)
				{
					useChargeSkill(true);
				}
			}
		}
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00014DF8 File Offset: 0x00012FF8
	public void stopUseChargeSkill()
	{
		isFlyAndCharge = false;
		isStandAndCharge = false;
		isUseSkillAfterCharge = false;
		isCreateDark = false;
		bool flag = me && statusMe != 14 && statusMe != 5;
		if (flag)
		{
			isLockMove = false;
		}
		GameScr.gI().auto = 0;
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00014E5C File Offset: 0x0001305C
	public void useChargeSkill(bool isGround)
	{
		bool flag = isCreateDark;
		if (!flag)
		{
			GameScr.gI().auto = 0;
			if (isGround)
			{
				bool flag2 = isStandAndCharge;
				if (!flag2)
				{
					chargeCount = 0;
					seconds = 50000;
					posDisY = 0;
					last = mSystem.currentTimeMillis();
					bool flag3 = me;
					if (flag3)
					{
						isLockMove = true;
						bool flag4 = cgender == 1;
						if (flag4)
						{
							Service.gI().skill_not_focus(4);
						}
					}
					bool flag5 = cgender == 1;
					if (flag5)
					{
						SoundMn.gI().gongName();
					}
					isStandAndCharge = true;
				}
			}
			else
			{
				bool flag6 = !isFlyAndCharge;
				if (flag6)
				{
					bool flag7 = me;
					if (flag7)
					{
						GameScr.gI().auto = 0;
						isLockMove = true;
						Service.gI().skill_not_focus(4);
					}
					isUseSkillAfterCharge = false;
					chargeCount = 0;
					isFlyAndCharge = true;
					posDisY = 0;
					seconds = 50000;
					isFlying = TileMap.tileTypeAt(cx, cy, 2);
				}
			}
		}
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00014F94 File Offset: 0x00013194
	public void setAutoSkillPaint(SkillPaint skillPaint, int sType)
	{
		skillPaint = skillPaint;
		Res.outz("set auto skill " + ((skillPaint == null) ? "null" : "!null"));
		bool flag = skillPaint.id >= 0 && skillPaint.id <= 6;
		if (flag)
		{
			int num = Res.random(0, skillPaint.id + 4) - 1;
			bool flag2 = num < 0;
			if (flag2)
			{
				num = 0;
			}
			bool flag3 = num > 6;
			if (flag3)
			{
				num = 6;
			}
			skillPaintRandomPaint = GameScr.sks[num];
		}
		else
		{
			bool flag4 = skillPaint.id >= 14 && skillPaint.id <= 20;
			if (flag4)
			{
				int num2 = Res.random(0, skillPaint.id - 14 + 4) - 1;
				bool flag5 = num2 < 0;
				if (flag5)
				{
					num2 = 0;
				}
				bool flag6 = num2 > 6;
				if (flag6)
				{
					num2 = 6;
				}
				skillPaintRandomPaint = GameScr.sks[num2 + 14];
			}
			else
			{
				bool flag7 = skillPaint.id >= 28 && skillPaint.id <= 34;
				if (flag7)
				{
					int num3 = Res.random(0, ((isMonkey != 1) ? skillPaint.id : 105) - ((isMonkey != 1) ? 28 : 105) + 4) - 1;
					bool flag8 = num3 < 0;
					if (flag8)
					{
						num3 = 0;
					}
					bool flag9 = num3 > 6;
					if (flag9)
					{
						num3 = 6;
					}
					bool flag10 = isMonkey == 1;
					if (flag10)
					{
						num3 = 0;
					}
					skillPaintRandomPaint = GameScr.sks[num3 + ((isMonkey != 1) ? 28 : 105)];
				}
				else
				{
					bool flag11 = skillPaint.id >= 63 && skillPaint.id <= 69;
					if (flag11)
					{
						int num4 = Res.random(0, skillPaint.id - 63 + 4) - 1;
						bool flag12 = num4 < 0;
						if (flag12)
						{
							num4 = 0;
						}
						bool flag13 = num4 > 6;
						if (flag13)
						{
							num4 = 6;
						}
						skillPaintRandomPaint = GameScr.sks[num4 + 63];
					}
					else
					{
						bool flag14 = skillPaint.id >= 107 && skillPaint.id <= 109;
						if (flag14)
						{
							int num5 = Res.random(0, skillPaint.id - 107 + 4) - 1;
							bool flag15 = num5 < 0;
							if (flag15)
							{
								num5 = 0;
							}
							bool flag16 = num5 > 6;
							if (flag16)
							{
								num5 = 6;
							}
							skillPaintRandomPaint = GameScr.sks[num5 + 107];
						}
						else
						{
							skillPaintRandomPaint = skillPaint;
						}
					}
				}
			}
		}
		sType = sType;
		indexSkill = 0;
		i0 = (i1 = (i2 = (dx0 = (dx1 = (dx2 = (dy0 = (dy1 = (dy2 = 0))))))));
		eff0 = null;
		eff1 = null;
		eff2 = null;
		cvy = 0;
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x000152A0 File Offset: 0x000134A0
	public SkillInfoPaint[] skillInfoPaint()
	{
		bool flag = skillPaint == null;
		SkillInfoPaint[] result;
		if (flag)
		{
			result = null;
		}
		else
		{
			bool flag2 = skillPaintRandomPaint == null;
			if (flag2)
			{
				result = null;
			}
			else
			{
				bool flag3 = sType == 0;
				if (flag3)
				{
					result = skillPaintRandomPaint.skillStand;
				}
				else
				{
					result = skillPaintRandomPaint.skillfly;
				}
			}
		}
		return result;
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00015300 File Offset: 0x00013500
	public void setAttack()
	{
		bool flag = me;
		if (flag)
		{
			SkillPaint skillPaint = skillPaintRandomPaint;
			bool flag2 = dart != null;
			if (flag2)
			{
				skillPaint = dart.skillPaint;
			}
			bool flag3 = skillPaint == null;
			if (!flag3)
			{
				MyVector myVector = new MyVector();
				MyVector myVector2 = new MyVector();
				bool flag4 = charFocus != null;
				if (flag4)
				{
					myVector2.addElement(charFocus);
				}
				else
				{
					bool flag5 = mobFocus != null;
					if (flag5)
					{
						myVector.addElement(mobFocus);
					}
				}
				effPaints = new EffectPaint[myVector.size() + myVector2.size()];
				for (int i = 0; i < myVector.size(); i++)
				{
					effPaints[i] = new EffectPaint();
					effPaints[i].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
					bool flag6 = !isSelectingSkillUseAlone();
					if (flag6)
					{
						effPaints[i].eMob = (Mob)myVector.elementAt(i);
					}
				}
				for (int j = 0; j < myVector2.size(); j++)
				{
					effPaints[j + myVector.size()] = new EffectPaint();
					effPaints[j + myVector.size()].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
					effPaints[j + myVector.size()].eChar = (global::Char)myVector2.elementAt(j);
				}
				int type = 0;
				bool flag7 = mobFocus != null;
				if (flag7)
				{
					type = 1;
				}
				else
				{
					bool flag8 = charFocus != null;
					if (flag8)
					{
						type = 2;
					}
				}
				bool flag9 = myVector.size() == 0 && myVector2.size() == 0;
				if (flag9)
				{
					stopUseChargeSkill();
				}
				bool flag10 = me && !isSelectingSkillUseAlone() && !hasSendAttack;
				if (flag10)
				{
					Service.gI().sendPlayerAttack(myVector, myVector2, type);
					hasSendAttack = true;
				}
			}
		}
		else
		{
			SkillPaint skillPaint2 = skillPaintRandomPaint;
			bool flag11 = dart != null;
			if (flag11)
			{
				skillPaint2 = dart.skillPaint;
			}
			bool flag12 = skillPaint2 == null;
			if (!flag12)
			{
				bool flag13 = attMobs != null;
				if (flag13)
				{
					effPaints = new EffectPaint[attMobs.Length];
					for (int k = 0; k < attMobs.Length; k++)
					{
						effPaints[k] = new EffectPaint();
						effPaints[k].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
						effPaints[k].eMob = attMobs[k];
					}
					attMobs = null;
				}
				else
				{
					bool flag14 = attChars != null;
					if (flag14)
					{
						effPaints = new EffectPaint[attChars.Length];
						for (int l = 0; l < attChars.Length; l++)
						{
							effPaints[l] = new EffectPaint();
							effPaints[l].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
							effPaints[l].eChar = attChars[l];
						}
						attChars = null;
					}
				}
			}
		}
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x00015684 File Offset: 0x00013884
	public bool isOutX()
	{
		bool flag = cx < GameScr.cmx;
		bool result;
		if (flag)
		{
			result = true;
		}
		else
		{
			bool flag2 = cx > GameScr.cmx + GameScr.gW;
			result = flag2;
		}
		return result;
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x000156CC File Offset: 0x000138CC
	public bool isPaint()
	{
		bool flag = cy < GameScr.cmy;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = cy > GameScr.cmy + GameScr.gH + 30;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = isOutX();
				if (flag3)
				{
					result = false;
				}
				else
				{
					bool flag4 = isSetPos;
					if (flag4)
					{
						result = false;
					}
					else
					{
						bool flag5 = isFusion;
						result = !flag5;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00003C4A File Offset: 0x00001E4A
	public void createShadow(int x, int y, int life)
	{
		shadowX = x;
		shadowY = y;
		shadowLife = life;
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00003C62 File Offset: 0x00001E62
	public void setMabuHold(bool m)
	{
		isMabuHold = m;
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00015748 File Offset: 0x00013948
	public virtual void paint(mGraphics g)
	{
		bool flag = isHide;
		if (!flag)
		{
			bool flag2 = isMabuHold;
			if (flag2)
			{
				bool flag3 = cmtoChar;
				if (flag3)
				{
					GameScr.cmtoX = cx - GameScr.gW2;
					GameScr.cmtoY = cy - GameScr.gH23;
					bool flag4 = !GameCanvas.isTouchControl;
					if (flag4)
					{
						GameScr.cmtoX += GameScr.gW6 * cdir;
					}
				}
			}
			else
			{
				bool flag5 = !isPaint() || (!me && GameScr.notPaint);
				if (!flag5)
				{
					bool flag6 = petFollow != null;
					if (flag6)
					{
						petFollow.paint(g);
					}
					paintMount1(g);
					bool flag7 = (TileMap.isInAirMap() && cy >= TileMap.pxh - 48) || isTeleport;
					if (!flag7)
					{
						bool flag8 = holder && GameCanvas.gameTick % 2 == 0;
						if (flag8)
						{
							g.setColor(16185600);
							bool flag9 = charHold != null;
							if (flag9)
							{
								g.drawLine(cx, cy - ch / 2, charHold.cx, charHold.cy - charHold.ch / 2);
							}
							bool flag10 = mobHold != null;
							if (flag10)
							{
								g.drawLine(cx, cy - ch / 2, mobHold.x, mobHold.y - mobHold.h / 2);
							}
						}
						paintSuperEffBehind(g);
						paintAuraBehind(g);
						paintEffBehind(g);
						paintEff_Lvup_behind(g);
						bool flag11 = shadowLife > 0;
						if (flag11)
						{
							bool flag12 = GameCanvas.gameTick % 2 == 0;
							if (flag12)
							{
								paintCharBody(g, shadowX, shadowY, cdir, 25, true);
							}
							else
							{
								bool flag13 = shadowLife > 5;
								if (flag13)
								{
									paintCharBody(g, shadowX, shadowY, cdir, 7, true);
								}
							}
						}
						bool flag14 = !isPaint() && skillPaint != null && (skillPaint.id < 70 || skillPaint.id > 76) && (skillPaint.id < 77 || skillPaint.id > 83);
						if (flag14)
						{
							bool flag15 = skillPaint != null;
							if (flag15)
							{
								indexSkill = skillInfoPaint().Length;
								skillPaint = null;
							}
							effPaints = null;
							eff = null;
							effTask = null;
							indexEff = -1;
							indexEffTask = -1;
						}
						else
						{
							bool flag16 = statusMe != 15 && (moveFast == null || moveFast[0] <= 0);
							if (flag16)
							{
								paintCharName_HP_MP_Overhead(g);
								bool flag17 = skillPaint == null || skillInfoPaint() == null || indexSkill >= skillInfoPaint().Length;
								if (flag17)
								{
									paintCharWithoutSkill(g);
								}
								bool flag18 = arr != null;
								if (flag18)
								{
									arr.paint(g);
								}
								bool flag19 = dart != null;
								if (flag19)
								{
									dart.paint(g);
								}
								paintEffect(g);
								bool flag20 = mobMe != null;
								if (flag20)
								{
								}
								paintMount2(g);
								paintEff_Lvup_front(g);
								paintSuperEffFront(g);
								paintAuraFront(g);
								paintEffFront(g);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00015B38 File Offset: 0x00013D38
	private void paintSuperEffBehind(mGraphics g)
	{
		bool flag = me;
		if (flag)
		{
			bool flag2 = global::Char.isPaintAura && idAuraEff > -1;
			if (flag2)
			{
				return;
			}
		}
		else
		{
			bool flag3 = idAuraEff > -1;
			if (flag3)
			{
				return;
			}
		}
		bool flag4 = (statusMe != 1 && statusMe != 6) || GameCanvas.panel.isShow || mSystem.currentTimeMillis() - timeBlue <= 0L || isCopy || clevel < 16;
		if (!flag4)
		{
			int num = 7598;
			int num2 = 4;
			bool flag5 = clevel >= 19;
			if (flag5)
			{
				num = 7676;
			}
			bool flag6 = clevel >= 22;
			if (flag6)
			{
				num = 7677;
			}
			bool flag7 = clevel >= 25;
			if (flag7)
			{
				num = 7678;
			}
			bool flag8 = num != -1;
			if (flag8)
			{
				Small small = SmallImage.imgNew[num];
				bool flag9 = small == null;
				if (flag9)
				{
					SmallImage.createImage(num);
				}
				else
				{
					int y = GameCanvas.gameTick / 4 % num2 * (mGraphics.getImageHeight(small.img) / num2);
					g.drawRegion(small.img, 0, y, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img) / num2, 0, cx, cy + 2, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
		}
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00015CBC File Offset: 0x00013EBC
	private void paintSuperEffFront(mGraphics g)
	{
		bool flag = me;
		if (flag)
		{
			bool flag2 = global::Char.isPaintAura && idAuraEff > -1;
			if (flag2)
			{
				return;
			}
		}
		else
		{
			bool flag3 = idAuraEff > -1;
			if (flag3)
			{
				return;
			}
		}
		bool flag4 = statusMe == 1 || statusMe == 6;
		if (flag4)
		{
			bool flag5 = GameCanvas.panel.isShow || mSystem.currentTimeMillis() - timeBlue <= 0L;
			if (!flag5)
			{
				bool flag6 = isCopy;
				if (flag6)
				{
					bool flag7 = GameCanvas.gameTick % 2 == 0;
					if (flag7)
					{
						tBlue++;
					}
					bool flag8 = tBlue > 6;
					if (flag8)
					{
						tBlue = 0;
					}
					g.drawImage(GameCanvas.imgViolet[tBlue], cx, cy + 9, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				else
				{
					bool flag9 = clevel >= 14 && !GameCanvas.lowGraphic;
					if (flag9)
					{
						bool flag10 = false;
						bool flag11 = mSystem.currentTimeMillis() - timeBlue > -1000L && IsAddDust1;
						if (flag11)
						{
							flag10 = true;
							IsAddDust1 = false;
						}
						bool flag12 = mSystem.currentTimeMillis() - timeBlue > -500L && IsAddDust2;
						if (flag12)
						{
							flag10 = true;
							IsAddDust2 = false;
						}
						bool flag13 = flag10;
						if (flag13)
						{
							GameCanvas.gI().startDust(-1, cx - -8, cy);
							GameCanvas.gI().startDust(1, cx - 8, cy);
							addDustEff(1);
						}
					}
					bool flag14 = clevel == 14;
					if (flag14)
					{
						bool flag15 = GameCanvas.gameTick % 2 == 0;
						if (flag15)
						{
							tBlue++;
						}
						bool flag16 = tBlue > 6;
						if (flag16)
						{
							tBlue = 0;
						}
						g.drawImage(GameCanvas.imgBlue[tBlue], cx, cy + 9, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else
					{
						bool flag17 = clevel == 15;
						if (flag17)
						{
							bool flag18 = GameCanvas.gameTick % 2 == 0;
							if (flag18)
							{
								tBlue++;
							}
							bool flag19 = tBlue > 6;
							if (flag19)
							{
								tBlue = 0;
							}
							g.drawImage(GameCanvas.imgViolet[tBlue], cx, cy + 9, mGraphics.BOTTOM | mGraphics.HCENTER);
						}
						else
						{
							bool flag20 = clevel < 16;
							if (!flag20)
							{
								int num = -1;
								int num2 = 4;
								bool flag21 = clevel >= 16 && clevel < 22;
								if (flag21)
								{
									num = 7599;
									num2 = 4;
								}
								bool flag22 = num != -1;
								if (flag22)
								{
									Small small = SmallImage.imgNew[num];
									bool flag23 = small == null;
									if (flag23)
									{
										SmallImage.createImage(num);
									}
									else
									{
										int y = GameCanvas.gameTick / 4 % num2 * (mGraphics.getImageHeight(small.img) / num2);
										g.drawRegion(small.img, 0, y, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img) / num2, 0, cx, cy + 2, mGraphics.BOTTOM | mGraphics.HCENTER);
									}
								}
							}
						}
					}
				}
			}
		}
		else
		{
			timeBlue = mSystem.currentTimeMillis() + 1500L;
			IsAddDust1 = true;
			IsAddDust2 = true;
		}
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00016090 File Offset: 0x00014290
	private void paintEffect(mGraphics g)
	{
		bool flag = effPaints != null;
		if (flag)
		{
			for (int i = 0; i < effPaints.Length; i++)
			{
				bool flag2 = effPaints[i] == null;
				if (!flag2)
				{
					bool flag3 = effPaints[i].eMob != null;
					if (flag3)
					{
						int y = effPaints[i].eMob.y;
						bool flag4 = effPaints[i].eMob is BigBoss;
						if (flag4)
						{
							y = effPaints[i].eMob.y - 60;
						}
						bool flag5 = effPaints[i].eMob is BigBoss2;
						if (flag5)
						{
							y = effPaints[i].eMob.y - 50;
						}
						bool flag6 = effPaints[i].eMob is BachTuoc;
						if (flag6)
						{
							y = effPaints[i].eMob.y - 40;
						}
						SmallImage.drawSmallImage(g, effPaints[i].getImgId(), effPaints[i].eMob.x, y, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else
					{
						bool flag7 = effPaints[i].eChar != null;
						if (flag7)
						{
							SmallImage.drawSmallImage(g, effPaints[i].getImgId(), effPaints[i].eChar.cx, effPaints[i].eChar.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
						}
					}
				}
			}
		}
		bool flag8 = indexEff >= 0 && eff != null;
		if (flag8)
		{
			SmallImage.drawSmallImage(g, eff.arrEfInfo[indexEff].idImg, cx + eff.arrEfInfo[indexEff].dx, cy + eff.arrEfInfo[indexEff].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
		}
		bool flag9 = indexEffTask >= 0 && effTask != null;
		if (flag9)
		{
			SmallImage.drawSmallImage(g, effTask.arrEfInfo[indexEffTask].idImg, cx + effTask.arrEfInfo[indexEffTask].dx, cy + effTask.arrEfInfo[indexEffTask].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
		}
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00003A0D File Offset: 0x00001C0D
	private void paintArrowAttack(mGraphics g)
	{
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00016348 File Offset: 0x00014548
	public void paintHp(mGraphics g, int x, int y)
	{
		int num = cHP * 100 / cHPFull / 10 - 1;
		bool flag = num < 0;
		if (flag)
		{
			num = 0;
		}
		bool flag2 = num > 9;
		if (flag2)
		{
			num = 9;
		}
		g.drawRegion(Mob.imgHP, 0, 6 * (9 - num), 9, 6, 0, x, y, 3);
	}

	// Token: 0x060000ED RID: 237 RVA: 0x000163A0 File Offset: 0x000145A0
	public int getClassColor()
	{
		int result = 9145227;
		bool flag = nClass.classId == 1 || nClass.classId == 2;
		if (flag)
		{
			result = 16711680;
		}
		else
		{
			bool flag2 = nClass.classId == 3 || nClass.classId == 4;
			if (flag2)
			{
				result = 33023;
			}
			else
			{
				bool flag3 = nClass.classId == 5 || nClass.classId == 6;
				if (flag3)
				{
					result = 7443811;
				}
			}
		}
		return result;
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00016440 File Offset: 0x00014640
	public void paintNameInSameParty(mGraphics g)
	{
		bool flag = cTypePk != 3 && cTypePk != 5 && isPaint();
		if (flag)
		{
			bool flag2 = global::Char.myCharz().charFocus == null || !global::Char.myCharz().charFocus.Equals(this);
			if (flag2)
			{
				mFont.tahoma_7_yellow.drawString(g, cName, cx, cy - ch - mFont.tahoma_7_green.getHeight() - 5, mFont.CENTER, mFont.tahoma_7_grey);
			}
			else
			{
				bool flag3 = global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.Equals(this);
				if (flag3)
				{
					mFont.tahoma_7_yellow.drawString(g, cName, cx, cy - ch - mFont.tahoma_7_green.getHeight() - 10, mFont.CENTER, mFont.tahoma_7_grey);
				}
			}
		}
	}

	// Token: 0x060000EF RID: 239 RVA: 0x0001653C File Offset: 0x0001473C
	private void paintCharName_HP_MP_Overhead(mGraphics g)
	{
		Part part = GameScr.parts[getFHead(head)];
		int num = global::Char.CharInfo[cf][0][2] - (int)part.pi[global::Char.CharInfo[cf][0][0]].dy + 5;
		bool flag = (isInvisiblez && !me) || (!me && TileMap.mapID == 113 && cy >= 360) || me;
		if (!flag)
		{
			bool flag2 = global::Char.myChar.clan != null && clanID == global::Char.myChar.clan.ID;
			bool flag3 = cTypePk == 3 || cTypePk == 5;
			bool flag4 = cTypePk == 4;
			bool flag5 = cName.StartsWith("$");
			if (flag5)
			{
				cName = cName.Substring(1);
				isPet = true;
			}
			bool flag6 = cName.StartsWith("#");
			if (flag6)
			{
				cName = cName.Substring(1);
				isMiniPet = true;
			}
			bool flag7 = global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.Equals(this);
			if (flag7)
			{
				num += 5;
				paintHp(g, cx, cy - num + 3);
			}
			num += mFont.tahoma_7_white.getHeight();
			mFont mFont = mFont.tahoma_7_whiteSmall;
			bool flag8 = isPet || isMiniPet;
			if (flag8)
			{
				mFont = mFont.tahoma_7_blue1Small;
			}
			else
			{
				bool flag9 = flag3;
				if (flag9)
				{
					mFont = mFont.nameFontRed;
				}
				else
				{
					bool flag10 = flag4;
					if (flag10)
					{
						mFont = mFont.nameFontYellow;
					}
					else
					{
						bool flag11 = flag2;
						if (flag11)
						{
							mFont = mFont.nameFontGreen;
						}
					}
				}
			}
			bool flag12 = (paintName || flag3 || flag4) && !flag2;
			if (flag12)
			{
				bool flag13 = mSystem.clientType == 1;
				if (flag13)
				{
					mFont.drawString(g, cName, cx, cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
				}
				else
				{
					mFont.drawString(g, cName, cx, cy - num, mFont.CENTER);
				}
				num += mFont.tahoma_7.getHeight();
			}
			bool flag14 = flag2;
			if (flag14)
			{
				bool flag15 = global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.Equals(this);
				if (flag15)
				{
					mFont.drawString(g, cName, cx, cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
				}
				else
				{
					bool flag16 = charFocus == null;
					if (flag16)
					{
						mFont.drawString(g, cName, cx - 10, cy - num + 3, mFont.LEFT, mFont.tahoma_7_grey);
						paintHp(g, cx - 16, cy - num + 10);
					}
				}
			}
		}
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x00016864 File Offset: 0x00014A64
	public void paintShadow(mGraphics g)
	{
		bool flag = isMabuHold || head == 377 || leg == 471 || isTeleport || isFlyUp;
		if (!flag)
		{
			int size = (int)TileMap.size;
			bool flag2 = (TileMap.mapID < 114 || TileMap.mapID > 120) && TileMap.mapID != 127 && TileMap.mapID != 128;
			if (flag2)
			{
				bool flag3 = TileMap.tileTypeAt(xSd + size / 2, ySd + 1, 4);
				if (flag3)
				{
					g.setClip(xSd / size * size, (ySd - 30) / size * size, size, 100);
				}
				else
				{
					bool flag4 = TileMap.tileTypeAt((xSd - size / 2) / size, (ySd + 1) / size) == 0;
					if (flag4)
					{
						g.setClip(xSd / size * size, (ySd - 30) / size * size, 100, 100);
					}
					else
					{
						bool flag5 = TileMap.tileTypeAt((xSd + size / 2) / size, (ySd + 1) / size) == 0;
						if (flag5)
						{
							g.setClip(xSd / size * size, (ySd - 30) / size * size, size, 100);
						}
						else
						{
							bool flag6 = TileMap.tileTypeAt(xSd - size / 2, ySd + 1, 8);
							if (flag6)
							{
								g.setClip(xSd / 24 * size, (ySd - 30) / size * size, size, 100);
							}
						}
					}
				}
			}
			g.drawImage(TileMap.bong, xSd, ySd, 3);
			g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
		}
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00016A4C File Offset: 0x00014C4C
	public void updateShadown()
	{
		int i = 0;
		xSd = cx;
		bool flag = TileMap.tileTypeAt(cx, cy, 2);
		if (flag)
		{
			ySd = cy;
		}
		else
		{
			ySd = cy;
			while (i < 30)
			{
				i++;
				ySd += 24;
				bool flag2 = TileMap.tileTypeAt(xSd, ySd, 2);
				if (flag2)
				{
					bool flag3 = ySd % 24 != 0;
					if (flag3)
					{
						ySd -= ySd % 24;
					}
					break;
				}
			}
		}
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00016AFC File Offset: 0x00014CFC
	private void paintCharWithoutSkill(mGraphics g)
	{
		try
		{
			bool flag = isInvisiblez;
			if (flag)
			{
				bool flag2 = me;
				if (flag2)
				{
					bool flag3 = GameCanvas.gameTick % 50 == 48 || GameCanvas.gameTick % 50 == 90;
					if (flag3)
					{
						SmallImage.drawSmallImage(g, 1196, cx, cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
					}
					else
					{
						SmallImage.drawSmallImage(g, 1195, cx, cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
					}
				}
			}
			else
			{
				paintCharBody(g, cx, cy + fy, cdir, cf, true);
			}
			bool flag4 = isLockAttack;
			if (flag4)
			{
				SmallImage.drawSmallImage(g, 290, cx, cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi paint char without skill: " + ex.ToString());
		}
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00016C28 File Offset: 0x00014E28
	public void paintBag(mGraphics g, short[] id, int x, int y, int dir, bool isPaintChar)
	{
		int num = 0;
		int num2 = 0;
		bool flag = statusMe == 6;
		if (flag)
		{
			num = 8;
			num2 = 17;
		}
		bool flag2 = statusMe == 1;
		if (flag2)
		{
			bool flag3 = cp1 % 15 < 5;
			if (flag3)
			{
				num = 8;
				num2 = 17;
			}
			else
			{
				num = 8;
				num2 = 18;
			}
		}
		bool flag4 = statusMe == 2;
		if (flag4)
		{
			bool flag5 = cf <= 3;
			if (flag5)
			{
				num = 7;
				num2 = 17;
			}
			else
			{
				num = 7;
				num2 = 18;
			}
		}
		bool flag6 = statusMe == 3 || statusMe == 9;
		if (flag6)
		{
			num = 5;
			num2 = 20;
		}
		bool flag7 = statusMe == 4;
		if (flag7)
		{
			bool flag8 = cf == 8;
			if (flag8)
			{
				num = 5;
				num2 = 16;
			}
			else
			{
				num = 5;
				num2 = 20;
			}
		}
		bool flag9 = statusMe == 10;
		if (flag9)
		{
			Res.outz("cf= " + cf);
			bool flag10 = cf == 8;
			if (flag10)
			{
				num = 0;
				num2 = 23;
			}
			else
			{
				num = 5;
				num2 = 22;
			}
		}
		bool flag11 = isInjure > 0;
		if (flag11)
		{
			num = 5;
			num2 = 18;
		}
		bool flag12 = skillPaint != null && skillInfoPaint() != null && indexSkill < skillInfoPaint().Length;
		if (flag12)
		{
			num = -1;
			num2 = 17;
		}
		fBag++;
		bool flag13 = fBag > 10000;
		if (flag13)
		{
			fBag = 0;
		}
		sbyte b = (sbyte)(fBag / 4 % id.Length);
		bool flag14 = !isPaintChar;
		if (flag14)
		{
			bool flag15 = id.Length == 2;
			if (flag15)
			{
				b = 1;
			}
			bool flag16 = id.Length == 3;
			if (flag16)
			{
				bool flag17 = id[2] >= 0;
				if (flag17)
				{
					b = 2;
					bool flag18 = GameCanvas.gameTick % 10 > 5;
					if (flag18)
					{
						b = 1;
					}
				}
				else
				{
					b = 1;
				}
			}
		}
		else
		{
			bool flag19 = id.Length > 1 && (b == 0 || b == 1) && statusMe != 1 && statusMe != 6;
			if (flag19)
			{
				fBag = 0;
				b = 0;
				bool flag20 = GameCanvas.gameTick % 10 > 5;
				if (flag20)
				{
					b = 1;
				}
			}
		}
		SmallImage.drawSmallImage(g, (int)id[(int)b], x + ((dir != 1) ? num : (-num)), y - num2, (dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00016EA0 File Offset: 0x000150A0
	public bool isCharBodyImageID(int id)
	{
		Part part = GameScr.parts[head];
		Part part2 = GameScr.parts[leg];
		Part part3 = GameScr.parts[body];
		int i = 0;
		while (i < global::Char.CharInfo.Length)
		{
			bool flag = id == (int)part.pi[global::Char.CharInfo[i][0][0]].id;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = id == (int)part2.pi[global::Char.CharInfo[i][1][0]].id;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = id == (int)part3.pi[global::Char.CharInfo[i][2][0]].id;
					if (!flag3)
					{
						i++;
						continue;
					}
					result = true;
				}
			}
			return result;
		}
		return false;
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x00016F6C File Offset: 0x0001516C
	public void paintHead(mGraphics g, int cx, int cy, int look)
	{
		Part part = GameScr.parts[head];
		SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, cx, cy, (look != 0) ? 2 : 0, mGraphics.RIGHT | mGraphics.VCENTER);
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00016FBC File Offset: 0x000151BC
	public void paintHeadWithXY(mGraphics g, int x, int y, int look)
	{
		Part part = GameScr.parts[head];
		SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, x + global::Char.CharInfo[0][0][1] + (int)part.pi[global::Char.CharInfo[0][0][0]].dx - 3, y + 3, look, mGraphics.LEFT | mGraphics.BOTTOM);
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x0001702C File Offset: 0x0001522C
	public void paintCharBody(mGraphics g, int cx, int cy, int cdir, int cf, bool isPaintBag)
	{
		Part part = GameScr.parts[head];
		Part part2 = GameScr.parts[leg];
		Part part3 = GameScr.parts[body];
		bool flag = bag >= 0 && statusMe != 14 && isMonkey == 0;
		if (flag)
		{
			bool flag2 = !ClanImage.idImages.containsKey(bag + string.Empty);
			if (flag2)
			{
				ClanImage.idImages.put(bag + string.Empty, new ClanImage());
				Service.gI().requestBagImage((sbyte)bag);
			}
			else
			{
				ClanImage clanImage = (ClanImage)ClanImage.idImages.get(bag + string.Empty);
				bool flag3 = clanImage.idImage != null && isPaintBag;
				if (flag3)
				{
					paintBag(g, clanImage.idImage, cx, cy, cdir, true);
				}
			}
		}
		int num = 2;
		int anchor = 24;
		int anchor2 = StaticObj.TOP_RIGHT;
		int num2 = -1;
		bool flag4 = cdir == 1;
		if (flag4)
		{
			num = 0;
			anchor = 0;
			anchor2 = 0;
			num2 = 1;
		}
		bool flag5 = statusMe == 14;
		if (flag5)
		{
			bool flag6 = GameCanvas.gameTick % 4 > 0;
			if (flag6)
			{
				g.drawImage(ItemMap.imageFlare, cx, cy - ch - 11, mGraphics.HCENTER | mGraphics.VCENTER);
			}
			int num3 = 0;
			bool flag7 = head == 89 || head == 457 || head == 460 || head == 461 || head == 462 || head == 463 || head == 464 || head == 465 || head == 466;
			if (flag7)
			{
				num3 = 15;
			}
			SmallImage.drawSmallImage(g, 834, cx, cy - global::Char.CharInfo[cf][2][2] + (int)part3.pi[global::Char.CharInfo[cf][2][0]].dy - 2 + num3, num, StaticObj.TOP_CENTER);
			SmallImage.drawSmallImage(g, 79, cx, cy - ch - 8, 0, mGraphics.HCENTER | mGraphics.BOTTOM);
			paintHat_behind(g, cf, cy - global::Char.CharInfo[cf][2][2] + (int)part3.pi[global::Char.CharInfo[cf][2][0]].dy);
			bool flag8 = isHead_2Fr(head);
			if (flag8)
			{
				Part part4 = GameScr.parts[getFHead(head)];
				SmallImage.drawSmallImage(g, (int)part4.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)part4.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)part4.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
			}
			paintHat_front(g, cf, cy - global::Char.CharInfo[cf][2][2] + (int)part3.pi[global::Char.CharInfo[cf][2][0]].dy);
			paintRedEye(g, cx + (global::Char.CharInfo[cf][0][1] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
		}
		else
		{
			paintHat_behind(g, cf, cy - global::Char.CharInfo[cf][2][2] + (int)part3.pi[global::Char.CharInfo[cf][2][0]].dy);
			bool flag9 = isHead_2Fr(head);
			if (flag9)
			{
				Part part5 = GameScr.parts[getFHead(head)];
				SmallImage.drawSmallImage(g, (int)part5.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)part5.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)part5.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
			}
			paintHat_front(g, cf, cy - global::Char.CharInfo[cf][2][2] + (int)part3.pi[global::Char.CharInfo[cf][2][0]].dy);
			SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[cf][1][0]].id, cx + (global::Char.CharInfo[cf][1][1] + (int)part2.pi[global::Char.CharInfo[cf][1][0]].dx) * num2, cy - global::Char.CharInfo[cf][1][2] + (int)part2.pi[global::Char.CharInfo[cf][1][0]].dy, num, anchor);
			SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[cf][2][0]].id, cx + (global::Char.CharInfo[cf][2][1] + (int)part3.pi[global::Char.CharInfo[cf][2][0]].dx) * num2, cy - global::Char.CharInfo[cf][2][2] + (int)part3.pi[global::Char.CharInfo[cf][2][0]].dy, num, anchor);
			paintRedEye(g, cx + (global::Char.CharInfo[cf][0][1] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dx) * num2, cy - global::Char.CharInfo[cf][0][2] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dy, num, anchor);
		}
		ch = ((isMonkey != 1 && !isFusion) ? (global::Char.CharInfo[0][0][2] + (int)part.pi[global::Char.CharInfo[0][0][0]].dy + 10) : 60);
		bool flag10 = statusMe == 1 && charID > 0 && !isMask && !isUseChargeSkill() && !isWaitMonkey && skillPaint == null && cf != 23 && bag < 0 && ((GameCanvas.gameTick + charID) % 30 == 0 || isFreez);
		if (flag10)
		{
			g.drawImage((cgender != 1) ? global::Char.eyeTraiDat : global::Char.eyeNamek, cx + -((cgender != 1) ? 2 : 2) * num2, cy - 32 + ((cgender != 1) ? 11 : 10) - cf, anchor2);
		}
		bool flag11 = eProtect != null;
		if (flag11)
		{
			eProtect.paint(g);
		}
		paintPKFlag(g);
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x00017844 File Offset: 0x00015A44
	public void paintCharWithSkill(mGraphics g)
	{
		ty = 0;
		SkillInfoPaint[] array = skillInfoPaint();
		cf = array[indexSkill].status;
		paintCharWithoutSkill(g);
		bool flag = cdir == 1;
		if (flag)
		{
			bool flag2 = eff0 != null;
			if (flag2)
			{
				bool flag3 = dx0 == 0;
				if (flag3)
				{
					dx0 = array[indexSkill].e0dx;
				}
				bool flag4 = dy0 == 0;
				if (flag4)
				{
					dy0 = array[indexSkill].e0dy;
				}
				SmallImage.drawSmallImage(g, eff0.arrEfInfo[i0].idImg, cx + dx0 + eff0.arrEfInfo[i0].dx, cy + dy0 + eff0.arrEfInfo[i0].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				i0++;
				bool flag5 = i0 >= eff0.arrEfInfo.Length;
				if (flag5)
				{
					eff0 = null;
					i0 = (dx0 = (dy0 = 0));
				}
			}
			bool flag6 = eff1 != null;
			if (flag6)
			{
				bool flag7 = dx1 == 0;
				if (flag7)
				{
					dx1 = array[indexSkill].e1dx;
				}
				bool flag8 = dy1 == 0;
				if (flag8)
				{
					dy1 = array[indexSkill].e1dy;
				}
				SmallImage.drawSmallImage(g, eff1.arrEfInfo[i1].idImg, cx + dx1 + eff1.arrEfInfo[i1].dx, cy + dy1 + eff1.arrEfInfo[i1].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				i1++;
				bool flag9 = i1 >= eff1.arrEfInfo.Length;
				if (flag9)
				{
					eff1 = null;
					i1 = (dx1 = (dy1 = 0));
				}
			}
			bool flag10 = eff2 != null;
			if (flag10)
			{
				bool flag11 = dx2 == 0;
				if (flag11)
				{
					dx2 = array[indexSkill].e2dx;
				}
				bool flag12 = dy2 == 0;
				if (flag12)
				{
					dy2 = array[indexSkill].e2dy;
				}
				SmallImage.drawSmallImage(g, eff2.arrEfInfo[i2].idImg, cx + dx2 + eff2.arrEfInfo[i2].dx, cy + dy2 + eff2.arrEfInfo[i2].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				i2++;
				bool flag13 = i2 >= eff2.arrEfInfo.Length;
				if (flag13)
				{
					eff2 = null;
					i2 = (dx2 = (dy2 = 0));
				}
			}
		}
		else
		{
			bool flag14 = eff0 != null;
			if (flag14)
			{
				bool flag15 = dx0 == 0;
				if (flag15)
				{
					dx0 = array[indexSkill].e0dx;
				}
				bool flag16 = dy0 == 0;
				if (flag16)
				{
					dy0 = array[indexSkill].e0dy;
				}
				SmallImage.drawSmallImage(g, eff0.arrEfInfo[i0].idImg, cx - dx0 - eff0.arrEfInfo[i0].dx, cy + dy0 + eff0.arrEfInfo[i0].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
				i0++;
				bool flag17 = i0 >= eff0.arrEfInfo.Length;
				if (flag17)
				{
					eff0 = null;
					i0 = 0;
					dx0 = 0;
					dy0 = 0;
				}
			}
			bool flag18 = eff1 != null;
			if (flag18)
			{
				bool flag19 = dx1 == 0;
				if (flag19)
				{
					dx1 = array[indexSkill].e1dx;
				}
				bool flag20 = dy1 == 0;
				if (flag20)
				{
					dy1 = array[indexSkill].e1dy;
				}
				SmallImage.drawSmallImage(g, eff1.arrEfInfo[i1].idImg, cx - dx1 - eff1.arrEfInfo[i1].dx, cy + dy1 + eff1.arrEfInfo[i1].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
				i1++;
				bool flag21 = i1 >= eff1.arrEfInfo.Length;
				if (flag21)
				{
					eff1 = null;
					i1 = 0;
					dx1 = 0;
					dy1 = 0;
				}
			}
			bool flag22 = eff2 != null;
			if (flag22)
			{
				bool flag23 = dx2 == 0;
				if (flag23)
				{
					dx2 = array[indexSkill].e2dx;
				}
				bool flag24 = dy2 == 0;
				if (flag24)
				{
					dy2 = array[indexSkill].e2dy;
				}
				SmallImage.drawSmallImage(g, eff2.arrEfInfo[i2].idImg, cx - dx2 - eff2.arrEfInfo[i2].dx, cy + dy2 + eff2.arrEfInfo[i2].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
				i2++;
				bool flag25 = i2 >= eff2.arrEfInfo.Length;
				if (flag25)
				{
					eff2 = null;
					i2 = 0;
					dx2 = 0;
					dy2 = 0;
				}
			}
		}
		indexSkill++;
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x00017F54 File Offset: 0x00016154
	public static int getIndexChar(int ID)
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			bool flag = @char.charID == ID;
			if (flag)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00017FA8 File Offset: 0x000161A8
	public void moveTo(int toX, int toY, int type)
	{
		bool flag = type == 1 || Res.abs(toX - cx) > 100 || Res.abs(toY - cy) > 300;
		if (flag)
		{
			createShadow(cx, cy, 10);
			cx = toX;
			cy = toY;
			vMovePoints.removeAllElements();
			statusMe = 6;
			cp3 = 0;
			currentMovePoint = null;
			cf = 25;
		}
		else
		{
			int dir = 0;
			int act = 0;
			int num = toX - cx;
			int num2 = toY - cy;
			bool flag2 = num == 0 && num2 == 0;
			if (flag2)
			{
				act = 1;
				cp3 = 0;
			}
			else
			{
				bool flag3 = num2 == 0;
				if (flag3)
				{
					act = 2;
					bool flag4 = num > 0;
					if (flag4)
					{
						dir = 1;
					}
					bool flag5 = num < 0;
					if (flag5)
					{
						dir = -1;
					}
				}
				else
				{
					bool flag6 = num2 != 0;
					if (flag6)
					{
						bool flag7 = num2 < 0;
						if (flag7)
						{
							act = 3;
						}
						bool flag8 = num2 > 0;
						if (flag8)
						{
							act = 4;
						}
						bool flag9 = num < 0;
						if (flag9)
						{
							dir = -1;
						}
						bool flag10 = num > 0;
						if (flag10)
						{
							dir = 1;
						}
					}
				}
			}
			vMovePoints.addElement(new MovePoint(toX, toY, act, dir));
			bool flag11 = statusMe != 6;
			if (flag11)
			{
				statusBeforeNothing = statusMe;
			}
			statusMe = 6;
			cp3 = 0;
		}
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00018120 File Offset: 0x00016320
	public static void getcharInjure(int cID, int dx, int dy, int HP)
	{
		global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(cID);
		bool flag = @char.vMovePoints.size() != 0;
		if (flag)
		{
			MovePoint movePoint = (MovePoint)@char.vMovePoints.lastElement();
			int xEnd = movePoint.xEnd + dx;
			int yEnd = movePoint.yEnd + dy;
			global::Char char2 = (global::Char)GameScr.vCharInMap.elementAt(cID);
			char2.cHP -= HP;
			bool flag2 = char2.cHP < 0;
			if (flag2)
			{
				char2.cHP = 0;
			}
			char2.cHPShow = ((global::Char)GameScr.vCharInMap.elementAt(cID)).cHP - HP;
			char2.statusMe = 6;
			char2.cp3 = 0;
			char2.vMovePoints.addElement(new MovePoint(xEnd, yEnd, 8, char2.cdir));
		}
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00018200 File Offset: 0x00016400
	public bool isMagicTree()
	{
		bool flag = GameScr.gI().magicTree != null;
		bool result;
		if (flag)
		{
			int x = GameScr.gI().magicTree.x;
			int y = GameScr.gI().magicTree.y;
			bool flag2 = cx > x - 30 && cx < x + 30 && cy > y - 30 && cy < y + 30;
			result = flag2;
		}
		else
		{
			result = false;
		}
		return result;
	}

	// Token: 0x060000FD RID: 253 RVA: 0x0001828C File Offset: 0x0001648C
	public void searchItem()
	{
		int[] array = new int[]
		{
			-1,
			-1,
			-1,
			-1
		};
		bool flag = itemFocus != null;
		if (!flag)
		{
			for (int i = 0; i < GameScr.vItemMap.size(); i++)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
				int num = global::Math.abs(global::Char.myCharz().cx - itemMap.x);
				int num2 = global::Math.abs(global::Char.myCharz().cy - itemMap.y);
				int num3 = (num <= num2) ? num2 : num;
				bool flag2 = num > 48 || num2 > 48 || (itemFocus != null && num3 >= array[3]);
				if (!flag2)
				{
					bool flag3 = GameScr.gI().auto != 0 && GameScr.gI().isBagFull();
					if (flag3)
					{
						bool flag4 = itemMap.template.type == 9;
						if (flag4)
						{
							itemFocus = itemMap;
							array[3] = num3;
						}
					}
					else
					{
						itemFocus = itemMap;
						array[3] = num3;
					}
				}
			}
		}
	}

	// Token: 0x060000FE RID: 254 RVA: 0x000183B4 File Offset: 0x000165B4
	public void searchFocus()
	{
		bool flag = global::Char.myCharz().skillPaint != null || global::Char.myCharz().arr != null || global::Char.myCharz().dart != null;
		if (flag)
		{
			timeFocusToMob = 200;
		}
		else
		{
			bool flag2 = timeFocusToMob > 0;
			if (flag2)
			{
				timeFocusToMob--;
			}
			else
			{
				bool flag3 = global::Char.isManualFocus && charFocus != null && (charFocus.statusMe == 15 || charFocus.isInvisiblez);
				if (flag3)
				{
					charFocus = null;
				}
				bool flag4 = GameCanvas.gameTick % 2 == 0 || isMeCanAttackOtherPlayer(charFocus);
				if (!flag4)
				{
					int num = 0;
					bool flag5 = nClass.classId == 0 || nClass.classId == 1 || nClass.classId == 3 || nClass.classId == 5;
					if (flag5)
					{
						num = 40;
					}
					int[] array = new int[]
					{
						-1,
						-1,
						-1,
						-1
					};
					int num2 = GameScr.cmx - 10;
					int num3 = GameScr.cmx + GameCanvas.w + 10;
					int num4 = GameScr.cmy;
					int num5 = GameScr.cmy + GameCanvas.h - GameScr.cmdBarH + 10;
					bool flag6 = global::Char.isManualFocus;
					if (flag6)
					{
						bool flag7 = (mobFocus != null && mobFocus.status != 1 && mobFocus.status != 0 && num2 <= mobFocus.x && mobFocus.x <= num3 && num4 <= mobFocus.y && mobFocus.y <= num5) || (npcFocus != null && num2 <= npcFocus.cx && npcFocus.cx <= num3 && num4 <= npcFocus.cy && npcFocus.cy <= num5) || (charFocus != null && num2 <= charFocus.cx && charFocus.cx <= num3 && num4 <= charFocus.cy && charFocus.cy <= num5) || (itemFocus != null && num2 <= itemFocus.x && itemFocus.x <= num3 && num4 <= itemFocus.y && itemFocus.y <= num5);
						if (flag7)
						{
							return;
						}
						global::Char.isManualFocus = false;
					}
					num2 = global::Char.myCharz().cx - 80;
					num3 = global::Char.myCharz().cx + 80;
					num4 = global::Char.myCharz().cy - 30;
					num5 = global::Char.myCharz().cy + 30;
					bool flag8 = npcFocus != null && npcFocus.template.npcTemplateId == 6;
					if (flag8)
					{
						num2 = global::Char.myCharz().cx - 20;
						num3 = global::Char.myCharz().cx + 20;
						num4 = global::Char.myCharz().cy - 10;
						num5 = global::Char.myCharz().cy + 10;
					}
					bool flag9 = npcFocus == null;
					if (flag9)
					{
						for (int i = 0; i < GameScr.vNpc.size(); i++)
						{
							Npc npc = (Npc)GameScr.vNpc.elementAt(i);
							bool flag10 = npc.statusMe != 15;
							if (flag10)
							{
								int num6 = global::Math.abs(global::Char.myCharz().cx - npc.cx);
								int num7 = global::Math.abs(global::Char.myCharz().cy - npc.cy);
								int num8 = (num6 <= num7) ? num7 : num6;
								num2 = global::Char.myCharz().cx - 80;
								num3 = global::Char.myCharz().cx + 80;
								num4 = global::Char.myCharz().cy - 30;
								num5 = global::Char.myCharz().cy + 30;
								bool flag11 = npc.template.npcTemplateId == 6;
								if (flag11)
								{
									num2 = global::Char.myCharz().cx - 20;
									num3 = global::Char.myCharz().cx + 20;
									num4 = global::Char.myCharz().cy - 10;
									num5 = global::Char.myCharz().cy + 10;
								}
								bool flag12 = num2 <= npc.cx && npc.cx <= num3 && num4 <= npc.cy && npc.cy <= num5 && (npcFocus == null || num8 < array[1]);
								if (flag12)
								{
									npcFocus = npc;
									array[1] = num8;
								}
							}
						}
					}
					else
					{
						bool flag13 = num2 <= npcFocus.cx && npcFocus.cx <= num3 && num4 <= npcFocus.cy && npcFocus.cy <= num5;
						if (flag13)
						{
							clearFocus(1);
							return;
						}
						deFocusNPC();
						for (int j = 0; j < GameScr.vNpc.size(); j++)
						{
							Npc npc2 = (Npc)GameScr.vNpc.elementAt(j);
							bool flag14 = npc2.statusMe != 15;
							if (flag14)
							{
								int num9 = global::Math.abs(global::Char.myCharz().cx - npc2.cx);
								int num10 = global::Math.abs(global::Char.myCharz().cy - npc2.cy);
								int num11 = (num9 <= num10) ? num10 : num9;
								num2 = global::Char.myCharz().cx - 80;
								num3 = global::Char.myCharz().cx + 80;
								num4 = global::Char.myCharz().cy - 30;
								num5 = global::Char.myCharz().cy + 30;
								bool flag15 = npc2.template.npcTemplateId == 6;
								if (flag15)
								{
									num2 = global::Char.myCharz().cx - 20;
									num3 = global::Char.myCharz().cx + 20;
									num4 = global::Char.myCharz().cy - 10;
									num5 = global::Char.myCharz().cy + 10;
								}
								bool flag16 = num2 <= npc2.cx && npc2.cx <= num3 && num4 <= npc2.cy && npc2.cy <= num5 && (npcFocus == null || num11 < array[1]);
								if (flag16)
								{
									npcFocus = npc2;
									array[1] = num11;
								}
							}
						}
					}
					bool flag17 = itemFocus == null;
					if (flag17)
					{
						for (int k = 0; k < GameScr.vItemMap.size(); k++)
						{
							ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(k);
							int num12 = global::Math.abs(global::Char.myCharz().cx - itemMap.x);
							int num13 = global::Math.abs(global::Char.myCharz().cy - itemMap.y);
							int num14 = (num12 <= num13) ? num13 : num12;
							bool flag18 = num12 > 48 || num13 > 48 || (itemFocus != null && num14 >= array[3]);
							if (!flag18)
							{
								bool flag19 = GameScr.gI().auto != 0 && GameScr.gI().isBagFull();
								if (flag19)
								{
									bool flag20 = itemMap.template.type == 9;
									if (flag20)
									{
										itemFocus = itemMap;
										array[3] = num14;
									}
								}
								else
								{
									itemFocus = itemMap;
									array[3] = num14;
								}
							}
						}
					}
					else
					{
						bool flag21 = num2 <= itemFocus.x && itemFocus.x <= num3 && num4 <= itemFocus.y && itemFocus.y <= num5;
						if (flag21)
						{
							clearFocus(3);
							return;
						}
						itemFocus = null;
						for (int l = 0; l < GameScr.vItemMap.size(); l++)
						{
							ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(l);
							int num15 = global::Math.abs(global::Char.myCharz().cx - itemMap2.x);
							int num16 = global::Math.abs(global::Char.myCharz().cy - itemMap2.y);
							int num17 = (num15 <= num16) ? num16 : num15;
							bool flag22 = num2 > itemMap2.x || itemMap2.x > num3 || num4 > itemMap2.y || itemMap2.y > num5 || (itemFocus != null && num17 >= array[3]);
							if (!flag22)
							{
								bool flag23 = GameScr.gI().auto != 0 && GameScr.gI().isBagFull();
								if (flag23)
								{
									bool flag24 = itemMap2.template.type == 9;
									if (flag24)
									{
										itemFocus = itemMap2;
										array[3] = num17;
									}
								}
								else
								{
									itemFocus = itemMap2;
									array[3] = num17;
								}
							}
						}
					}
					num2 = global::Char.myCharz().cx - global::Char.myCharz().getdxSkill() - 10;
					num3 = global::Char.myCharz().cx + global::Char.myCharz().getdxSkill() + 10;
					num4 = global::Char.myCharz().cy - global::Char.myCharz().getdySkill() - num - 20;
					num5 = global::Char.myCharz().cy + global::Char.myCharz().getdySkill() + 20;
					bool flag25 = num5 > global::Char.myCharz().cy + 30;
					if (flag25)
					{
						num5 = global::Char.myCharz().cy + 30;
					}
					bool flag26 = mobFocus == null;
					if (flag26)
					{
						for (int m = 0; m < GameScr.vMob.size(); m++)
						{
							Mob mob = (Mob)GameScr.vMob.elementAt(m);
							int num18 = global::Math.abs(global::Char.myCharz().cx - mob.x);
							int num19 = global::Math.abs(global::Char.myCharz().cy - mob.y);
							int num20 = (num18 <= num19) ? num19 : num18;
							bool flag27 = num2 <= mob.x && mob.x <= num3 && num4 <= mob.y && mob.y <= num5 && (mobFocus == null || num20 < array[0]);
							if (flag27)
							{
								mobFocus = mob;
								array[0] = num20;
							}
						}
					}
					else
					{
						bool flag28 = mobFocus.status != 1 && mobFocus.status != 0 && num2 <= mobFocus.x && mobFocus.x <= num3 && num4 <= mobFocus.y && mobFocus.y <= num5;
						if (flag28)
						{
							clearFocus(0);
							return;
						}
						mobFocus = null;
						for (int n = 0; n < GameScr.vMob.size(); n++)
						{
							Mob mob2 = (Mob)GameScr.vMob.elementAt(n);
							int num21 = global::Math.abs(global::Char.myCharz().cx - mob2.x);
							int num22 = global::Math.abs(global::Char.myCharz().cy - mob2.y);
							int num23 = (num21 <= num22) ? num22 : num21;
							bool flag29 = num2 <= mob2.x && mob2.x <= num3 && num4 <= mob2.y && mob2.y <= num5 && (mobFocus == null || num23 < array[0]);
							if (flag29)
							{
								mobFocus = mob2;
								array[0] = num23;
							}
						}
					}
					bool flag30 = charFocus == null;
					if (flag30)
					{
						for (int num24 = 0; num24 < GameScr.vCharInMap.size(); num24++)
						{
							global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(num24);
							bool flag31 = @char.statusMe != 15 && !@char.isInvisiblez && wdx == 0 && wdy == 0;
							if (flag31)
							{
								int num25 = global::Math.abs(global::Char.myCharz().cx - @char.cx);
								int num26 = global::Math.abs(global::Char.myCharz().cy - @char.cy);
								int num27 = (num25 <= num26) ? num26 : num25;
								bool flag32 = num2 <= @char.cx && @char.cx <= num3 && num4 <= @char.cy && @char.cy <= num5 && (charFocus == null || num27 < array[2]);
								if (flag32)
								{
									charFocus = @char;
									array[2] = num27;
								}
							}
						}
					}
					else
					{
						bool flag33 = num2 <= charFocus.cx && charFocus.cx <= num3 && num4 <= charFocus.cy && charFocus.cy <= num5 && charFocus.statusMe != 15 && !charFocus.isInvisiblez;
						if (flag33)
						{
							clearFocus(2);
							return;
						}
						for (int num28 = 0; num28 < GameScr.vCharInMap.size(); num28++)
						{
							global::Char char2 = (global::Char)GameScr.vCharInMap.elementAt(num28);
							bool flag34 = char2.statusMe != 15 && !char2.isInvisiblez && wdx == 0 && wdy == 0;
							if (flag34)
							{
								int num29 = global::Math.abs(global::Char.myCharz().cx - char2.cx);
								int num30 = global::Math.abs(global::Char.myCharz().cy - char2.cy);
								int num31 = (num29 <= num30) ? num30 : num29;
								bool flag35 = num2 <= char2.cx && char2.cx <= num3 && num4 <= char2.cy && char2.cy <= num5 && (charFocus == null || num31 < array[2]);
								if (flag35)
								{
									charFocus = char2;
									array[2] = num31;
								}
							}
						}
					}
					int num32 = -1;
					for (int num33 = 0; num33 < array.Length; num33++)
					{
						bool flag36 = num32 == -1;
						if (flag36)
						{
							bool flag37 = array[num33] != -1;
							if (flag37)
							{
								num32 = num33;
							}
						}
						else
						{
							bool flag38 = array[num33] < array[num32] && array[num33] != -1;
							if (flag38)
							{
								num32 = num33;
							}
						}
					}
					clearFocus(num32);
					bool flag39 = me && isAttacPlayerStatus();
					if (flag39)
					{
						bool flag40 = mobFocus != null && !mobFocus.isMobMe;
						if (flag40)
						{
							mobFocus = null;
						}
						npcFocus = null;
						itemFocus = null;
					}
				}
			}
		}
	}

	// Token: 0x060000FF RID: 255 RVA: 0x000192F4 File Offset: 0x000174F4
	public void clearFocus(int index)
	{
		switch (index)
		{
		case 0:
			deFocusNPC();
			charFocus = null;
			itemFocus = null;
			break;
		case 1:
			mobFocus = null;
			charFocus = null;
			itemFocus = null;
			break;
		case 2:
			mobFocus = null;
			deFocusNPC();
			itemFocus = null;
			break;
		case 3:
			mobFocus = null;
			deFocusNPC();
			charFocus = null;
			break;
		}
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00019378 File Offset: 0x00017578
	public static bool isCharInScreen(global::Char c)
	{
		int cmx = GameScr.cmx;
		int num = GameScr.cmx + GameCanvas.w;
		int num2 = GameScr.cmy + 10;
		int num3 = GameScr.cmy + GameScr.gH;
		return c.statusMe != 15 && !c.isInvisiblez && cmx <= c.cx && c.cx <= num && num2 <= c.cy && c.cy <= num3;
	}

	// Token: 0x06000101 RID: 257 RVA: 0x000193FC File Offset: 0x000175FC
	public bool isAttacPlayerStatus()
	{
		return cTypePk == 4 || cTypePk == 3;
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00019424 File Offset: 0x00017624
	public void setHoldChar(global::Char r)
	{
		bool flag = cx < r.cx;
		if (flag)
		{
			cdir = 1;
		}
		else
		{
			cdir = -1;
		}
		charHold = r;
		holder = true;
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00019468 File Offset: 0x00017668
	public void setHoldMob(Mob r)
	{
		bool flag = cx < r.x;
		if (flag)
		{
			cdir = 1;
		}
		else
		{
			cdir = -1;
		}
		mobHold = r;
		holder = true;
	}

	// Token: 0x06000104 RID: 260 RVA: 0x000194AC File Offset: 0x000176AC
	public void findNextFocusByKey()
	{
		Res.outz("focus size= " + focus.size());
		bool flag = (global::Char.myCharz().skillPaint != null || global::Char.myCharz().arr != null || global::Char.myCharz().dart != null || global::Char.myCharz().skillInfoPaint() != null) && focus.size() == 0;
		if (!flag)
		{
			focus.removeAllElements();
			int num = 0;
			int num2 = GameScr.cmx + 10;
			int num3 = GameScr.cmx + GameCanvas.w - 10;
			int num4 = GameScr.cmy + 10;
			int num5 = GameScr.cmy + GameScr.gH;
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				bool flag2 = @char.statusMe != 15 && !@char.isInvisiblez && (VuDang.isPKM || (num2 <= @char.cx && @char.cx <= num3 && num4 <= @char.cy && @char.cy <= num5)) && @char.charID != -114 && (TileMap.mapID != 129 || (TileMap.mapID == 129 && global::Char.myCharz().cy > 264)) && (!VuDang.isPKM || global::Char.myCharz().isMeCanAttackOtherPlayer(@char));
				if (flag2)
				{
					focus.addElement(@char);
					bool flag3 = charFocus != null && @char.Equals(charFocus);
					if (flag3)
					{
						num = focus.size();
					}
				}
			}
			bool flag4 = !VuDang.isPKM;
			if (flag4)
			{
				bool flag5 = me && isAttacPlayerStatus();
				if (flag5)
				{
					Res.outz("co the tan cong nguoi");
					for (int j = 0; j < GameScr.vMob.size(); j++)
					{
						Mob mob = (Mob)GameScr.vMob.elementAt(j);
						bool flag6 = !GameScr.gI().isMeCanAttackMob(mob);
						if (flag6)
						{
							Res.outz("khong the tan cong quai");
							mobFocus = null;
						}
						else
						{
							Res.outz("co the tan ong quai");
							focus.addElement(mob);
							bool flag7 = mobFocus != null;
							if (flag7)
							{
								num = focus.size();
							}
						}
					}
					npcFocus = null;
					itemFocus = null;
					bool flag8 = focus.size() > 0;
					if (flag8)
					{
						bool flag9 = num >= focus.size();
						if (flag9)
						{
							num = 0;
						}
						focusManualTo(focus.elementAt(num));
					}
					else
					{
						mobFocus = null;
						deFocusNPC();
						charFocus = null;
						itemFocus = null;
						global::Char.isManualFocus = false;
					}
					return;
				}
				for (int k = 0; k < GameScr.vItemMap.size(); k++)
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(k);
					bool flag10 = num2 <= itemMap.x && itemMap.x <= num3 && num4 <= itemMap.y && itemMap.y <= num5;
					if (flag10)
					{
						focus.addElement(itemMap);
						bool flag11 = itemFocus != null && itemMap.Equals(itemFocus);
						if (flag11)
						{
							num = focus.size();
						}
					}
				}
				for (int l = 0; l < GameScr.vMob.size(); l++)
				{
					Mob mob2 = (Mob)GameScr.vMob.elementAt(l);
					bool flag12 = mob2.status != 1 && mob2.status != 0 && num2 <= mob2.x && mob2.x <= num3 && num4 <= mob2.y && mob2.y <= num5;
					if (flag12)
					{
						focus.addElement(mob2);
						bool flag13 = mobFocus != null && mob2.Equals(mobFocus);
						if (flag13)
						{
							num = focus.size();
						}
					}
				}
				for (int m = 0; m < GameScr.vNpc.size(); m++)
				{
					Npc npc = (Npc)GameScr.vNpc.elementAt(m);
					bool flag14 = npc.statusMe != 15 && num2 <= npc.cx && npc.cx <= num3 && num4 <= npc.cy && npc.cy <= num5;
					if (flag14)
					{
						focus.addElement(npc);
						bool flag15 = npcFocus != null && npc.Equals(npcFocus);
						if (flag15)
						{
							num = focus.size();
						}
					}
				}
			}
			bool flag16 = focus.size() > 0;
			if (flag16)
			{
				bool flag17 = num >= focus.size();
				if (flag17)
				{
					num = 0;
				}
				focusManualTo(focus.elementAt(num));
			}
			else
			{
				mobFocus = null;
				deFocusNPC();
				charFocus = null;
				itemFocus = null;
				global::Char.isManualFocus = false;
			}
		}
	}

	// Token: 0x06000105 RID: 261 RVA: 0x00019A4C File Offset: 0x00017C4C
	public void deFocusNPC()
	{
		bool flag = me && npcFocus != null;
		if (flag)
		{
			bool flag2 = !GameCanvas.menu.showMenu;
			if (flag2)
			{
				global::Char.chatPopup = null;
			}
			npcFocus = null;
		}
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00019A94 File Offset: 0x00017C94
	public void updateCharInBridge()
	{
		bool flag = !GameCanvas.lowGraphic;
		if (flag)
		{
			bool flag2 = TileMap.tileTypeAt(cx, cy + 1, 1024);
			if (flag2)
			{
				TileMap.setTileTypeAtPixel(cx, cy + 1, 512);
				TileMap.setTileTypeAtPixel(cx, cy - 2, 512);
			}
			bool flag3 = TileMap.tileTypeAt(cx - (int)TileMap.size, cy + 1, 512);
			if (flag3)
			{
				TileMap.killTileTypeAt(cx - (int)TileMap.size, cy + 1, 512);
				TileMap.killTileTypeAt(cx - (int)TileMap.size, cy - 2, 512);
			}
			bool flag4 = TileMap.tileTypeAt(cx + (int)TileMap.size, cy + 1, 512);
			if (flag4)
			{
				TileMap.killTileTypeAt(cx + (int)TileMap.size, cy + 1, 512);
				TileMap.killTileTypeAt(cx + (int)TileMap.size, cy - 2, 512);
			}
		}
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00019BC8 File Offset: 0x00017DC8
	public static void sort(int[] data)
	{
		int num = 5;
		for (int i = 0; i < num - 1; i++)
		{
			for (int j = i + 1; j < num; j++)
			{
				bool flag = data[i] < data[j];
				if (flag)
				{
					int num2 = data[j];
					data[j] = data[i];
					data[i] = num2;
				}
			}
		}
	}

	// Token: 0x06000108 RID: 264 RVA: 0x00019C24 File Offset: 0x00017E24
	public static bool setInsc(int cmX, int cmWx, int x, int cmy, int cmyH, int y)
	{
		bool flag = x > cmWx || x < cmX || y > cmyH || y < cmy;
		return !flag;
	}

	// Token: 0x06000109 RID: 265 RVA: 0x00019C58 File Offset: 0x00017E58
	public void kickOption(Item item, int maxKick)
	{
		int num = 0;
		bool flag = item == null || item.options == null;
		if (!flag)
		{
			for (int i = 0; i < item.options.size(); i++)
			{
				ItemOption itemOption = (ItemOption)item.options.elementAt(i);
				itemOption.active = 0;
				bool flag2 = itemOption.optionTemplate.type == 2;
				if (flag2)
				{
					bool flag3 = num < maxKick;
					if (flag3)
					{
						itemOption.active = 1;
						num++;
					}
				}
				else
				{
					bool flag4 = itemOption.optionTemplate.type == 3 && item.upgrade >= 4;
					if (flag4)
					{
						itemOption.active = 1;
					}
					else
					{
						bool flag5 = itemOption.optionTemplate.type == 4 && item.upgrade >= 8;
						if (flag5)
						{
							itemOption.active = 1;
						}
						else
						{
							bool flag6 = itemOption.optionTemplate.type == 5 && item.upgrade >= 12;
							if (flag6)
							{
								itemOption.active = 1;
							}
							else
							{
								bool flag7 = itemOption.optionTemplate.type == 6 && item.upgrade >= 14;
								if (flag7)
								{
									itemOption.active = 1;
								}
								else
								{
									bool flag8 = itemOption.optionTemplate.type == 7 && item.upgrade >= 16;
									if (flag8)
									{
										itemOption.active = 1;
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00019DE0 File Offset: 0x00017FE0
	public void doInjure(int HPShow, int MPShow, bool isCrit, bool isMob)
	{
		isCrit = isCrit;
		isMob = isMob;
		Res.outz(string.Concat(new object[]
		{
			"CHP= ",
			cHP,
			" dame -= ",
			HPShow,
			" HP FULL= ",
			cHPFull
		}));
		cHP -= HPShow;
		cMP -= MPShow;
		GameScr.gI().isInjureHp = true;
		GameScr.gI().twHp = 0;
		GameScr.gI().isInjureMp = true;
		GameScr.gI().twMp = 0;
		bool flag = cHP < 0;
		if (flag)
		{
			cHP = 0;
		}
		bool flag2 = cMP < 0;
		if (flag2)
		{
			cMP = 0;
		}
		bool flag3 = isMob || (!isMob && cTypePk != 4 && damMP != -100);
		if (flag3)
		{
			bool flag4 = HPShow <= 0;
			if (flag4)
			{
				bool flag5 = me;
				if (flag5)
				{
					GameScr.startFlyText(mResources.miss, cx, cy - ch, 0, -2, mFont.MISS_ME);
				}
				else
				{
					GameScr.startFlyText(mResources.miss, cx, cy - ch, 0, -2, mFont.MISS);
				}
			}
			else
			{
				GameScr.startFlyText("-" + HPShow, cx, cy - ch, 0, -2, isCrit ? mFont.FATAL : mFont.RED);
			}
		}
		bool flag6 = HPShow > 0;
		if (flag6)
		{
			isInjure = 6;
		}
		ServerEffect.addServerEffect(80, this, 1);
		bool flag7 = isDie;
		if (flag7)
		{
			isDie = false;
			global::Char.isLockKey = false;
			startDie((short)xSd, (short)ySd);
		}
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00019FE4 File Offset: 0x000181E4
	public void doInjure()
	{
		GameScr.gI().isInjureHp = true;
		GameScr.gI().twHp = 0;
		GameScr.gI().isInjureMp = true;
		GameScr.gI().twMp = 0;
		isInjure = 6;
		ServerEffect.addServerEffect(8, this, 1);
		isInjureHp = true;
		twHp = 0;
	}

	// Token: 0x0600010C RID: 268 RVA: 0x0001A03C File Offset: 0x0001823C
	public void startDie(short toX, short toY)
	{
		isMonkey = 0;
		isWaitMonkey = false;
		bool flag = me && isDie;
		if (!flag)
		{
			bool flag2 = me;
			if (flag2)
			{
				isLockMove = true;
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
					@char.killCharId = -9999;
				}
				bool flag3 = GameCanvas.panel != null && GameCanvas.panel.cp != null;
				if (flag3)
				{
					GameCanvas.panel.cp = null;
				}
				bool flag4 = GameCanvas.panel2 != null && GameCanvas.panel2.cp != null;
				if (flag4)
				{
					GameCanvas.panel2.cp = null;
				}
			}
			statusMe = 5;
			cp2 = (int)toX;
			cp3 = (int)toY;
			cp1 = 0;
			cHP = 0;
			testCharId = -9999;
			killCharId = -9999;
			bool flag5 = me && myskill != null && myskill.template.id != 14;
			if (flag5)
			{
				stopUseChargeSkill();
			}
			cTypePk = 0;
		}
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00003C6C File Offset: 0x00001E6C
	public void waitToDie(short toX, short toY)
	{
		wdx = toX;
		wdy = toY;
	}

	// Token: 0x0600010E RID: 270 RVA: 0x0001A190 File Offset: 0x00018390
	public void liveFromDead()
	{
		cHP = cHPFull;
		cMP = cMPFull;
		statusMe = 1;
		cp1 = (cp2 = (cp3 = 0));
		ServerEffect.addServerEffect(109, this, 2);
		GameScr.gI().center = null;
		GameScr.isHaveSelectSkill = true;
	}

	// Token: 0x0600010F RID: 271 RVA: 0x0001A1F4 File Offset: 0x000183F4
	public bool doUsePotion()
	{
		bool flag = arrItemBag == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			for (int i = 0; i < arrItemBag.Length; i++)
			{
				bool flag2 = arrItemBag[i] != null && arrItemBag[i].template.type == 6;
				if (flag2)
				{
					Service.gI().useItem(0, 1, -1, arrItemBag[i].template.id);
					return true;
				}
			}
			result = false;
		}
		return result;
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0001A280 File Offset: 0x00018480
	public bool isLang()
	{
		return TileMap.mapID == 1 || TileMap.mapID == 27 || TileMap.mapID == 72 || TileMap.mapID == 10 || TileMap.mapID == 17 || TileMap.mapID == 22 || TileMap.mapID == 32 || TileMap.mapID == 38 || TileMap.mapID == 43 || TileMap.mapID == 48;
	}

	// Token: 0x06000111 RID: 273 RVA: 0x0001A2F8 File Offset: 0x000184F8
	public bool isMeCanAttackOtherPlayer(global::Char cAtt)
	{
		bool flag = cAtt == null || global::Char.myCharz().myskill == null || global::Char.myCharz().myskill.template.type == 2 || (global::Char.myCharz().myskill.template.type == 4 && cAtt.statusMe != 14 && cAtt.statusMe != 5);
		return !flag && (((cAtt.cTypePk == 3 && global::Char.myCharz().cTypePk == 3) || (global::Char.myCharz().cTypePk == 5 || cAtt.cTypePk == 5 || (global::Char.myCharz().cTypePk == 1 && cAtt.cTypePk == 1)) || (global::Char.myCharz().cTypePk == 4 && cAtt.cTypePk == 4) || (global::Char.myCharz().testCharId >= 0 && global::Char.myCharz().testCharId == cAtt.charID) || (global::Char.myCharz().killCharId >= 0 && global::Char.myCharz().killCharId == cAtt.charID && !isLang()) || (cAtt.killCharId >= 0 && cAtt.killCharId == global::Char.myCharz().charID && !isLang()) || (global::Char.myCharz().cFlag == 8 && cAtt.cFlag != 0) || (global::Char.myCharz().cFlag != 0 && cAtt.cFlag == 8) || (global::Char.myCharz().cFlag != cAtt.cFlag && global::Char.myCharz().cFlag != 0 && cAtt.cFlag != 0)) && cAtt.statusMe != 14) && cAtt.statusMe != 5;
	}

	// Token: 0x06000112 RID: 274 RVA: 0x0001A4AC File Offset: 0x000186AC
	public void clearTask()
	{
		global::Char.myCharz().taskMaint = null;
		for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
		{
			bool flag = global::Char.myCharz().arrItemBag[i] != null && global::Char.myCharz().arrItemBag[i].template.type == 8;
			if (flag)
			{
				global::Char.myCharz().arrItemBag[i] = null;
			}
		}
		Npc.clearEffTask();
	}

	// Token: 0x06000113 RID: 275 RVA: 0x0001A528 File Offset: 0x00018728
	public int getX()
	{
		return cx;
	}

	// Token: 0x06000114 RID: 276 RVA: 0x0001A540 File Offset: 0x00018740
	public int getY()
	{
		return cy;
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0001A558 File Offset: 0x00018758
	public int getH()
	{
		return 32;
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0001A56C File Offset: 0x0001876C
	public int getW()
	{
		return 24;
	}

	// Token: 0x06000117 RID: 279 RVA: 0x0001A580 File Offset: 0x00018780
	public void focusManualTo(object objectz)
	{
		bool flag = objectz is Mob;
		if (flag)
		{
			mobFocus = (Mob)objectz;
			deFocusNPC();
			charFocus = null;
			itemFocus = null;
		}
		else
		{
			bool flag2 = objectz is Npc;
			if (flag2)
			{
				global::Char.myCharz().mobFocus = null;
				global::Char.myCharz().deFocusNPC();
				global::Char.myCharz().npcFocus = (Npc)objectz;
				global::Char.myCharz().charFocus = null;
				global::Char.myCharz().itemFocus = null;
			}
			else
			{
				bool flag3 = objectz is global::Char;
				if (flag3)
				{
					global::Char.myCharz().mobFocus = null;
					global::Char.myCharz().deFocusNPC();
					global::Char.myCharz().charFocus = (global::Char)objectz;
					global::Char.myCharz().itemFocus = null;
				}
				else
				{
					bool flag4 = objectz is ItemMap;
					if (flag4)
					{
						global::Char.myCharz().mobFocus = null;
						global::Char.myCharz().deFocusNPC();
						global::Char.myCharz().charFocus = null;
						global::Char.myCharz().itemFocus = (ItemMap)objectz;
					}
				}
			}
		}
		global::Char.isManualFocus = true;
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void stopMoving()
	{
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void cancelAttack()
	{
	}

	// Token: 0x0600011A RID: 282 RVA: 0x0001A69C File Offset: 0x0001889C
	public bool isInvisible()
	{
		return false;
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0001A6B0 File Offset: 0x000188B0
	public bool focusToAttack()
	{
		return mobFocus != null || (charFocus != null && isMeCanAttackOtherPlayer(charFocus));
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0001A6E4 File Offset: 0x000188E4
	public void addDustEff(int type)
	{
		bool lowGraphic = GameCanvas.lowGraphic;
		if (!lowGraphic)
		{
			switch (type)
			{
			case 1:
			{
				bool flag = clevel >= 9;
				if (flag)
				{
					Effect effect = new Effect(19, cx - 5, cy + 20, 2, 1, -1);
					EffecMn.addEff(effect);
				}
				break;
			}
			case 2:
			{
				bool flag2 = (!me || isMonkey != 1) && isNhapThe && GameCanvas.gameTick % 5 == 0;
				if (flag2)
				{
					Effect effect2 = new Effect(22, cx - 5, cy + 35, 2, 1, -1);
					EffecMn.addEff(effect2);
				}
				break;
			}
			case 3:
			{
				bool flag3 = clevel >= 9 && ySd - cy <= 5;
				if (flag3)
				{
					Effect effect3 = new Effect(19, cx - 5, ySd + 20, 2, 1, -1);
					EffecMn.addEff(effect3);
				}
				break;
			}
			}
		}
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0001A7FC File Offset: 0x000189FC
	public bool isGetFlagImage(sbyte getFlag)
	{
		bool result = true;
		for (int i = 0; i < GameScr.vFlag.size(); i++)
		{
			PKFlag pkflag = (PKFlag)GameScr.vFlag.elementAt(i);
			bool flag = pkflag != null;
			if (flag)
			{
				bool flag2 = pkflag.cflag == getFlag;
				if (flag2)
				{
					return true;
				}
				result = false;
			}
		}
		return result;
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0001A864 File Offset: 0x00018A64
	private void paintPKFlag(mGraphics g)
	{
		bool flag = cdir == 1;
		if (flag)
		{
			bool flag2 = cFlag != 0 && cFlag != -1;
			if (flag2)
			{
				SmallImage.drawSmallImage(g, flagImage, cx - 10, cy - ch - ((!me) ? 30 : 30) + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), 2, 0);
			}
		}
		else
		{
			bool flag3 = cFlag != 0 && cFlag != -1;
			if (flag3)
			{
				SmallImage.drawSmallImage(g, flagImage, cx, cy - ch - ((!me) ? 30 : 30) + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), 0, 0);
			}
		}
	}

	// Token: 0x0600011F RID: 287 RVA: 0x0001A954 File Offset: 0x00018B54
	public void removeHoleEff()
	{
		bool flag = holder;
		if (flag)
		{
			holder = false;
			charHold = null;
			mobHold = null;
		}
		else
		{
			holdEffID = 0;
			charHold = null;
			mobHold = null;
		}
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00003C7D File Offset: 0x00001E7D
	public void removeProtectEff()
	{
		protectEff = false;
		eProtect = null;
	}

	// Token: 0x06000121 RID: 289 RVA: 0x00003C8E File Offset: 0x00001E8E
	public void removeBlindEff()
	{
		blindEff = false;
	}

	// Token: 0x06000122 RID: 290 RVA: 0x0001A99C File Offset: 0x00018B9C
	public void removeEffect()
	{
		bool flag = holdEffID != 0;
		if (flag)
		{
			holdEffID = 0;
		}
		bool flag2 = holder;
		if (flag2)
		{
			holder = false;
		}
		bool flag3 = protectEff;
		if (flag3)
		{
			protectEff = false;
		}
		eProtect = null;
		charHold = null;
		mobHold = null;
		blindEff = false;
		sleepEff = false;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0001AA0C File Offset: 0x00018C0C
	public void setPos(short xPos, short yPos, sbyte typePos)
	{
		isSetPos = true;
		xPos = xPos;
		yPos = yPos;
		typePos = typePos;
		tpos = 0;
		bool flag = me;
		if (flag)
		{
			bool flag2 = GameCanvas.panel != null;
			if (flag2)
			{
				GameCanvas.panel.hide();
			}
			bool flag3 = GameCanvas.panel2 != null;
			if (flag3)
			{
				GameCanvas.panel2.hide();
			}
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00003C98 File Offset: 0x00001E98
	public void removeHuytSao()
	{
		huytSao = false;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00003CA2 File Offset: 0x00001EA2
	public void fusionComplete()
	{
		isFusion = false;
		global::Char.isLockKey = false;
		tFusion = 0;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00003CB9 File Offset: 0x00001EB9
	public void setFusion(sbyte fusion)
	{
		tFusion = 0;
		if ((fusion == 4 || fusion == 5) && me)
		{
			Service.gI().funsion(fusion);
		}
		if (VuDang.xoaHieuUngHopThe)
		{
			bool flag = fusion == 6;
		}
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00003CEE File Offset: 0x00001EEE
	public void removeSleepEff()
	{
		sleepEff = false;
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00003CF8 File Offset: 0x00001EF8
	public void setPartOld()
	{
		headTemp = head;
		bodyTemp = body;
		legTemp = leg;
		bagTemp = bag;
	}

	// Token: 0x06000129 RID: 297 RVA: 0x0001AA7C File Offset: 0x00018C7C
	public void setPartTemp(int head, int body, int leg, int bag)
	{
		bool flag = head != -1;
		if (flag)
		{
			head = head;
		}
		bool flag2 = body != -1;
		if (flag2)
		{
			body = body;
		}
		bool flag3 = leg != -1;
		if (flag3)
		{
			leg = leg;
		}
		bool flag4 = bag != -1;
		if (flag4)
		{
			bag = bag;
		}
	}

	// Token: 0x0600012A RID: 298 RVA: 0x0001AADC File Offset: 0x00018CDC
	public void resetPartTemp()
	{
		bool flag = headTemp != -1;
		if (flag)
		{
			head = headTemp;
			headTemp = -1;
		}
		bool flag2 = bodyTemp != -1;
		if (flag2)
		{
			body = bodyTemp;
			bodyTemp = -1;
		}
		bool flag3 = legTemp != -1;
		if (flag3)
		{
			leg = legTemp;
			legTemp = -1;
		}
		bool flag4 = bagTemp != -1;
		if (flag4)
		{
			bag = bagTemp;
			bagTemp = -1;
		}
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0001AB80 File Offset: 0x00018D80
	public Effect getEffById(int id)
	{
		for (int i = 0; i < vEffChar.size(); i++)
		{
			Effect effect = (Effect)vEffChar.elementAt(i);
			bool flag = effect.effId == id;
			if (flag)
			{
				return effect;
			}
		}
		return null;
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00003D2B File Offset: 0x00001F2B
	public void addEffChar(Effect e)
	{
		removeEffChar(0, e.effId);
		vEffChar.addElement(e);
	}

	// Token: 0x0600012D RID: 301 RVA: 0x0001ABD8 File Offset: 0x00018DD8
	public void removeEffChar(int type, int id)
	{
		bool flag = type == -1;
		if (flag)
		{
			vEffChar.removeAllElements();
		}
		else
		{
			bool flag2 = getEffById(id) != null;
			if (flag2)
			{
				vEffChar.removeElement(getEffById(id));
			}
		}
	}

	// Token: 0x0600012E RID: 302 RVA: 0x0001AC24 File Offset: 0x00018E24
	public void paintEffBehind(mGraphics g)
	{
		for (int i = 0; i < vEffChar.size(); i++)
		{
			Effect effect = (Effect)vEffChar.elementAt(i);
			bool flag = effect.layer == 0;
			if (flag)
			{
				bool flag2 = true;
				bool flag3 = effect.isStand == 0;
				if (flag3)
				{
					flag2 = (statusMe == 1 || statusMe == 6);
				}
				bool flag4 = flag2;
				if (flag4)
				{
					effect.paint(g);
				}
			}
		}
	}

	// Token: 0x0600012F RID: 303 RVA: 0x0001ACAC File Offset: 0x00018EAC
	public void paintEffFront(mGraphics g)
	{
		for (int i = 0; i < vEffChar.size(); i++)
		{
			Effect effect = (Effect)vEffChar.elementAt(i);
			bool flag = effect.layer == 1;
			if (flag)
			{
				bool flag2 = true;
				bool flag3 = effect.isStand == 0;
				if (flag3)
				{
					flag2 = (statusMe == 1 || statusMe == 6);
				}
				bool flag4 = flag2;
				if (flag4)
				{
					effect.paint(g);
				}
			}
		}
	}

	// Token: 0x06000130 RID: 304 RVA: 0x0001AD34 File Offset: 0x00018F34
	public void updEffChar()
	{
		for (int i = 0; i < vEffChar.size(); i++)
		{
			((Effect)vEffChar.elementAt(i)).update();
		}
	}

	// Token: 0x06000131 RID: 305 RVA: 0x0001AD78 File Offset: 0x00018F78
	public int checkLuong()
	{
		return luong + luongKhoa;
	}

	// Token: 0x06000132 RID: 306 RVA: 0x0001AD98 File Offset: 0x00018F98
	public void updateEye()
	{
		bool flag = head != 934;
		if (!flag)
		{
			bool flag2 = GameCanvas.timeNow - timeAddChopmat > 0L;
			if (flag2)
			{
				fChopmat++;
				bool flag3 = fChopmat > frEye.Length - 1;
				if (flag3)
				{
					fChopmat = 0;
					timeAddChopmat = GameCanvas.timeNow + (long)Res.random(2000, 3500);
					frEye = frChopCham;
					bool flag4 = Res.random(2) == 0;
					if (flag4)
					{
						frEye = frChopNhanh;
					}
				}
			}
			else
			{
				fChopmat = 0;
			}
		}
	}

	// Token: 0x06000133 RID: 307 RVA: 0x0001AE54 File Offset: 0x00019054
	private void paintRedEye(mGraphics g, int xx, int yy, int trans, int anchor)
	{
		bool flag = head != 934 || (statusMe != 1 && statusMe != 6);
		if (!flag)
		{
			bool flag2 = global::Char.fraRedEye == null || global::Char.fraRedEye.imgFrame == null;
			if (flag2)
			{
				Image img = mSystem.loadImage("/redeye.png");
				global::Char.fraRedEye = new FrameImage(img, 14, 10);
			}
			else
			{
				bool flag3 = frEye[fChopmat] != -1;
				if (flag3)
				{
					int num = 8;
					int num2 = 15;
					bool flag4 = trans == 2;
					if (flag4)
					{
						num = -8;
					}
					global::Char.fraRedEye.drawFrame(frEye[fChopmat], xx + num, yy + num2, trans, anchor, g);
				}
			}
		}
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0001AF24 File Offset: 0x00019124
	public bool isHead_2Fr(int idHead)
	{
		for (int i = 0; i < global::Char.Arr_Head_2Fr.Length; i++)
		{
			bool flag = global::Char.Arr_Head_2Fr[i][0] == idHead;
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0001AF64 File Offset: 0x00019164
	private void updateFHead()
	{
		bool flag = isHead_2Fr(head);
		if (flag)
		{
			fHead++;
			bool flag2 = fHead > 10000;
			if (flag2)
			{
				fHead = 0;
			}
		}
		else
		{
			fHead = 0;
		}
	}

	// Token: 0x06000136 RID: 310 RVA: 0x0001AFB8 File Offset: 0x000191B8
	private int getFHead(int idHead)
	{
		for (int i = 0; i < global::Char.Arr_Head_2Fr.Length; i++)
		{
			bool flag = global::Char.Arr_Head_2Fr[i][0] == idHead;
			if (flag)
			{
				return global::Char.Arr_Head_2Fr[i][fHead / 4 % global::Char.Arr_Head_2Fr[i].Length];
			}
		}
		return idHead;
	}

	// Token: 0x06000137 RID: 311 RVA: 0x0001B014 File Offset: 0x00019214
	public void paintAuraBehind(mGraphics g)
	{
		bool flag = (!me || global::Char.isPaintAura) && idAuraEff > -1 && (statusMe == 1 || statusMe == 6) && !GameCanvas.panel.isShow && mSystem.currentTimeMillis() - timeBlue > 0L;
		if (flag)
		{
			string nameImg = strEffAura + idAuraEff + "_0";
			FrameImage fraImage = mSystem.getFraImage(nameImg);
			if (fraImage != null)
			{
				fraImage.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, cx, cy, (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
			}
		}
	}

	// Token: 0x06000138 RID: 312 RVA: 0x0001B0D4 File Offset: 0x000192D4
	public void paintAuraFront(mGraphics g)
	{
		bool flag = (me && !global::Char.isPaintAura) || idAuraEff <= -1;
		if (!flag)
		{
			bool flag2 = statusMe == 1 || statusMe == 6;
			if (flag2)
			{
				bool flag3 = !GameCanvas.panel.isShow && !GameCanvas.lowGraphic;
				if (flag3)
				{
					bool flag4 = false;
					bool flag5 = mSystem.currentTimeMillis() - timeBlue > -1000L && IsAddDust1;
					if (flag5)
					{
						flag4 = true;
						IsAddDust1 = false;
					}
					bool flag6 = mSystem.currentTimeMillis() - timeBlue > -500L && IsAddDust2;
					if (flag6)
					{
						flag4 = true;
						IsAddDust2 = false;
					}
					bool flag7 = flag4;
					if (flag7)
					{
						GameCanvas.gI().startDust(-1, cx - -8, cy);
						GameCanvas.gI().startDust(1, cx - 8, cy);
						addDustEff(1);
					}
					bool flag8 = mSystem.currentTimeMillis() - timeBlue > 0L;
					if (flag8)
					{
						string nameImg = strEffAura + idAuraEff + "_1";
						FrameImage fraImage = mSystem.getFraImage(nameImg);
						if (fraImage != null)
						{
							fraImage.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, cx, cy + 2, (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
						}
					}
				}
			}
			else
			{
				timeBlue = mSystem.currentTimeMillis() + 1500L;
				IsAddDust1 = true;
				IsAddDust2 = true;
			}
		}
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0001B294 File Offset: 0x00019494
	public void paintEff_Lvup_behind(mGraphics g)
	{
		bool flag = idEff_Set_Item != -1;
		if (flag)
		{
			bool flag2 = fraEff != null;
			if (flag2)
			{
				fraEff.drawFrame(GameCanvas.gameTick / 4 % fraEff.nFrame, cx, cy + 3, (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
			}
			else
			{
				fraEff = mSystem.getFraImage(strEff_Set_Item + idEff_Set_Item + "_0");
			}
		}
	}

	// Token: 0x0600013A RID: 314 RVA: 0x0001B338 File Offset: 0x00019538
	public void paintEff_Lvup_front(mGraphics g)
	{
		bool flag = idEff_Set_Item != -1;
		if (flag)
		{
			bool flag2 = fraEffSub != null;
			if (flag2)
			{
				fraEffSub.drawFrame(GameCanvas.gameTick / 4 % fraEffSub.nFrame, cx, cy + 8, (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
			}
			else
			{
				fraEffSub = mSystem.getFraImage(strEff_Set_Item + idEff_Set_Item + "_1");
			}
		}
	}

	// Token: 0x0600013B RID: 315 RVA: 0x0001B3DC File Offset: 0x000195DC
	public void paintHat_behind(mGraphics g, int cf, int yh)
	{
		try
		{
			bool flag = idHat == -1;
			if (!flag)
			{
				bool flag2 = isFrNgang(cf);
				if (flag2)
				{
					bool flag3 = fraHat_behind_2 != null;
					if (flag3)
					{
						fraHat_behind_2.drawFrame(GameCanvas.gameTick / 4 % fraHat_behind_2.nFrame, cx + global::Char.hatInfo[cf][0] * ((cdir == 1) ? 1 : -1), yh + global::Char.hatInfo[cf][1], (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
					else
					{
						fraHat_behind_2 = mSystem.getFraImage(strHat_behind + strNgang + idHat);
					}
				}
				else
				{
					bool flag4 = fraHat_behind != null;
					if (flag4)
					{
						fraHat_behind.drawFrame(GameCanvas.gameTick / 4 % fraHat_behind.nFrame, cx + global::Char.hatInfo[cf][0] * ((cdir == 1) ? 1 : -1), yh + global::Char.hatInfo[cf][1], (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
					else
					{
						fraHat_behind = mSystem.getFraImage(strHat_behind + idHat);
					}
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0001B568 File Offset: 0x00019768
	public void paintHat_front(mGraphics g, int cf, int yh)
	{
		try
		{
			bool flag = idHat == -1;
			if (!flag)
			{
				bool flag2 = isFrNgang(cf);
				if (flag2)
				{
					bool flag3 = fraHat_font_2 != null;
					if (flag3)
					{
						fraHat_font_2.drawFrame(GameCanvas.gameTick / 4 % fraHat_font_2.nFrame, cx + global::Char.hatInfo[cf][0] * ((cdir == 1) ? 1 : -1), yh + global::Char.hatInfo[cf][1], (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
					else
					{
						fraHat_font_2 = mSystem.getFraImage(strHat_font + strNgang + idHat);
					}
				}
				else
				{
					bool flag4 = fraHat_font != null;
					if (flag4)
					{
						fraHat_font.drawFrame(GameCanvas.gameTick / 4 % fraHat_font.nFrame, cx + global::Char.hatInfo[cf][0] * ((cdir == 1) ? 1 : -1), yh + global::Char.hatInfo[cf][1], (cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
					else
					{
						fraHat_font = mSystem.getFraImage(strHat_font + idHat);
					}
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600013D RID: 317 RVA: 0x0001B6F4 File Offset: 0x000198F4
	public bool isFrNgang(int fr)
	{
		return fr == 2 || fr == 3 || fr == 4 || fr == 5 || fr == 6 || fr == 9 || fr == 10 || fr == 13 || fr == 14 || fr == 15 || fr == 16 || fr == 26 || fr == 27 || fr == 28 || fr == 29;
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0001B75C File Offset: 0x0001995C
	static Char()
	{
		int[][] array = new int[32][];
		array[0] = new int[]
		{
			5,
			-7
		};
		array[1] = new int[]
		{
			5,
			-7
		};
		array[2] = new int[]
		{
			5,
			-8
		};
		array[3] = new int[]
		{
			5,
			-7
		};
		array[4] = new int[]
		{
			5,
			-6
		};
		array[5] = new int[]
		{
			5,
			-8
		};
		array[6] = new int[]
		{
			5,
			-7
		};
		int num = 7;
		int[] array2 = new int[2];
		array2[0] = 9;
		array[num] = array2;
		array[8] = new int[]
		{
			11,
			1
		};
		int num2 = 9;
		int[] array3 = new int[2];
		array3[0] = 4;
		array[num2] = array3;
		array[10] = new int[]
		{
			4,
			-1
		};
		array[11] = new int[]
		{
			4,
			8
		};
		array[12] = new int[]
		{
			6,
			5
		};
		array[13] = new int[]
		{
			6,
			-6
		};
		array[14] = new int[]
		{
			2,
			-5
		};
		array[15] = new int[]
		{
			7,
			-8
		};
		array[16] = new int[]
		{
			7,
			-6
		};
		int num3 = 17;
		int[] array4 = new int[2];
		array4[0] = 8;
		array[num3] = array4;
		array[18] = new int[]
		{
			7,
			5
		};
		array[19] = new int[]
		{
			9,
			-7
		};
		array[20] = new int[]
		{
			7,
			-3
		};
		array[21] = new int[]
		{
			2,
			8
		};
		array[22] = new int[]
		{
			4,
			5
		};
		array[23] = new int[]
		{
			10,
			-5
		};
		array[24] = new int[]
		{
			9,
			-5
		};
		array[25] = new int[]
		{
			9,
			-5
		};
		array[26] = new int[]
		{
			6,
			-6
		};
		array[27] = new int[]
		{
			2,
			-5
		};
		array[28] = new int[]
		{
			7,
			-8
		};
		array[29] = new int[]
		{
			7,
			-6
		};
		array[30] = new int[]
		{
			9,
			-7
		};
		array[31] = new int[]
		{
			7,
			-3
		};
		global::Char.hatInfo = array;
	}

	// Token: 0x040000E2 RID: 226
	private bool isBiTroi;

	// Token: 0x040000E3 RID: 227
	public int timeBiTroi;

	// Token: 0x040000E4 RID: 228
	private long currBiTroi;

	// Token: 0x040000E5 RID: 229
	public int timeHuytSao;

	// Token: 0x040000E6 RID: 230
	private long currTimeHuytSao;

	// Token: 0x040000E7 RID: 231
	private bool isBlind;

	// Token: 0x040000E8 RID: 232
	public int timeBlind;

	// Token: 0x040000E9 RID: 233
	private long currTimeBlind;

	// Token: 0x040000EA RID: 234
	private bool isProtectEff;

	// Token: 0x040000EB RID: 235
	public int timeProtectEff;

	// Token: 0x040000EC RID: 236
	private long currTimeProtectEff;

	// Token: 0x040000ED RID: 237
	private bool isSleep;

	// Token: 0x040000EE RID: 238
	public int timeSleep;

	// Token: 0x040000EF RID: 239
	private long currTimeSleep;

	// Token: 0x040000F0 RID: 240
	private bool isGetMonkey;

	// Token: 0x040000F1 RID: 241
	public int timeMonkey;

	// Token: 0x040000F2 RID: 242
	private long currTimeMonkey;

	// Token: 0x040000F3 RID: 243
	public bool isNRD;

	// Token: 0x040000F4 RID: 244
	public int timeNRD;

	// Token: 0x040000F5 RID: 245
	private long currTimeNRD;

	// Token: 0x040000F6 RID: 246
	public string xuStr;

	// Token: 0x040000F7 RID: 247
	public string luongStr;

	// Token: 0x040000F8 RID: 248
	public string luongKhoaStr;

	// Token: 0x040000F9 RID: 249
	public long lastUpdateTime;

	// Token: 0x040000FA RID: 250
	public bool meLive;

	// Token: 0x040000FB RID: 251
	public bool isMask;

	// Token: 0x040000FC RID: 252
	public bool isTeleport;

	// Token: 0x040000FD RID: 253
	public bool isUsePlane;

	// Token: 0x040000FE RID: 254
	public int shadowX;

	// Token: 0x040000FF RID: 255
	public int shadowY;

	// Token: 0x04000100 RID: 256
	public int shadowLife;

	// Token: 0x04000101 RID: 257
	public bool isNhapThe;

	// Token: 0x04000102 RID: 258
	public PetFollow petFollow;

	// Token: 0x04000103 RID: 259
	public int rank;

	// Token: 0x04000104 RID: 260
	public const sbyte A_STAND = 1;

	// Token: 0x04000105 RID: 261
	public const sbyte A_RUN = 2;

	// Token: 0x04000106 RID: 262
	public const sbyte A_JUMP = 3;

	// Token: 0x04000107 RID: 263
	public const sbyte A_FALL = 4;

	// Token: 0x04000108 RID: 264
	public const sbyte A_DEADFLY = 5;

	// Token: 0x04000109 RID: 265
	public const sbyte A_NOTHING = 6;

	// Token: 0x0400010A RID: 266
	public const sbyte A_ATTK = 7;

	// Token: 0x0400010B RID: 267
	public const sbyte A_INJURE = 8;

	// Token: 0x0400010C RID: 268
	public const sbyte A_AUTOJUMP = 9;

	// Token: 0x0400010D RID: 269
	public const sbyte A_FLY = 10;

	// Token: 0x0400010E RID: 270
	public const sbyte SKILL_STAND = 12;

	// Token: 0x0400010F RID: 271
	public const sbyte SKILL_FALL = 13;

	// Token: 0x04000110 RID: 272
	public const sbyte A_DEAD = 14;

	// Token: 0x04000111 RID: 273
	public const sbyte A_HIDE = 15;

	// Token: 0x04000112 RID: 274
	public const sbyte A_RESETPOINT = 16;

	// Token: 0x04000113 RID: 275
	public static ChatPopup chatPopup;

	// Token: 0x04000114 RID: 276
	public long cPower;

	// Token: 0x04000115 RID: 277
	public Info chatInfo;

	// Token: 0x04000116 RID: 278
	public sbyte petStatus;

	// Token: 0x04000117 RID: 279
	public int cx = 24;

	// Token: 0x04000118 RID: 280
	public int cy = 24;

	// Token: 0x04000119 RID: 281
	public int cvx;

	// Token: 0x0400011A RID: 282
	public int cvy;

	// Token: 0x0400011B RID: 283
	public int cp1;

	// Token: 0x0400011C RID: 284
	public int cp2;

	// Token: 0x0400011D RID: 285
	public int cp3;

	// Token: 0x0400011E RID: 286
	public int statusMe = 5;

	// Token: 0x0400011F RID: 287
	public int cdir = 1;

	// Token: 0x04000120 RID: 288
	public int charID;

	// Token: 0x04000121 RID: 289
	public int cgender;

	// Token: 0x04000122 RID: 290
	public int ctaskId;

	// Token: 0x04000123 RID: 291
	public int menuSelect;

	// Token: 0x04000124 RID: 292
	public int cBonusSpeed;

	// Token: 0x04000125 RID: 293
	public int cspeed = 4;

	// Token: 0x04000126 RID: 294
	public int ccurrentAttack;

	// Token: 0x04000127 RID: 295
	public int cDamFull;

	// Token: 0x04000128 RID: 296
	public int cDefull;

	// Token: 0x04000129 RID: 297
	public int cCriticalFull;

	// Token: 0x0400012A RID: 298
	public int clevel;

	// Token: 0x0400012B RID: 299
	public int cMP;

	// Token: 0x0400012C RID: 300
	public int cHP;

	// Token: 0x0400012D RID: 301
	public int cHPNew;

	// Token: 0x0400012E RID: 302
	public int cMaxEXP;

	// Token: 0x0400012F RID: 303
	public int cHPShow;

	// Token: 0x04000130 RID: 304
	public int xReload;

	// Token: 0x04000131 RID: 305
	public int yReload;

	// Token: 0x04000132 RID: 306
	public int cyStartFall;

	// Token: 0x04000133 RID: 307
	public int saveStatus;

	// Token: 0x04000134 RID: 308
	public int eff5BuffHp;

	// Token: 0x04000135 RID: 309
	public int eff5BuffMp;

	// Token: 0x04000136 RID: 310
	public int cHPFull;

	// Token: 0x04000137 RID: 311
	public int cMPFull;

	// Token: 0x04000138 RID: 312
	public int cdameDown;

	// Token: 0x04000139 RID: 313
	public int cStr;

	// Token: 0x0400013A RID: 314
	public long cLevelPercent;

	// Token: 0x0400013B RID: 315
	public long cTiemNang;

	// Token: 0x0400013C RID: 316
	public long cNangdong;

	// Token: 0x0400013D RID: 317
	public int damHP;

	// Token: 0x0400013E RID: 318
	public int damMP;

	// Token: 0x0400013F RID: 319
	public bool isMob;

	// Token: 0x04000140 RID: 320
	public bool isCrit;

	// Token: 0x04000141 RID: 321
	public bool isDie;

	// Token: 0x04000142 RID: 322
	public int pointUydanh;

	// Token: 0x04000143 RID: 323
	public int pointNon;

	// Token: 0x04000144 RID: 324
	public int pointVukhi;

	// Token: 0x04000145 RID: 325
	public int pointAo;

	// Token: 0x04000146 RID: 326
	public int pointLien;

	// Token: 0x04000147 RID: 327
	public int pointGangtay;

	// Token: 0x04000148 RID: 328
	public int pointNhan;

	// Token: 0x04000149 RID: 329
	public int pointQuan;

	// Token: 0x0400014A RID: 330
	public int pointNgocboi;

	// Token: 0x0400014B RID: 331
	public int pointGiay;

	// Token: 0x0400014C RID: 332
	public int pointPhu;

	// Token: 0x0400014D RID: 333
	public int countFinishDay;

	// Token: 0x0400014E RID: 334
	public int countLoopBoos;

	// Token: 0x0400014F RID: 335
	public int limitTiemnangso;

	// Token: 0x04000150 RID: 336
	public int limitKynangso;

	// Token: 0x04000151 RID: 337
	public short[] potential = new short[4];

	// Token: 0x04000152 RID: 338
	public string cName = string.Empty;

	// Token: 0x04000153 RID: 339
	public int clanID;

	// Token: 0x04000154 RID: 340
	public sbyte ctypeClan;

	// Token: 0x04000155 RID: 341
	public Clan clan;

	// Token: 0x04000156 RID: 342
	public sbyte role;

	// Token: 0x04000157 RID: 343
	public int cw = 22;

	// Token: 0x04000158 RID: 344
	public int ch = 32;

	// Token: 0x04000159 RID: 345
	public int chw = 11;

	// Token: 0x0400015A RID: 346
	public int chh = 16;

	// Token: 0x0400015B RID: 347
	public Command cmdMenu;

	// Token: 0x0400015C RID: 348
	public bool canFly = true;

	// Token: 0x0400015D RID: 349
	public bool cmtoChar;

	// Token: 0x0400015E RID: 350
	public bool me;

	// Token: 0x0400015F RID: 351
	public bool cFinishedAttack;

	// Token: 0x04000160 RID: 352
	public bool cchistlast;

	// Token: 0x04000161 RID: 353
	public bool isAttack;

	// Token: 0x04000162 RID: 354
	public bool isAttFly;

	// Token: 0x04000163 RID: 355
	public int cwpt;

	// Token: 0x04000164 RID: 356
	public int cwplv;

	// Token: 0x04000165 RID: 357
	public int cf;

	// Token: 0x04000166 RID: 358
	public int tick;

	// Token: 0x04000167 RID: 359
	public static bool fallAttack;

	// Token: 0x04000168 RID: 360
	public bool isJump;

	// Token: 0x04000169 RID: 361
	public bool autoFall;

	// Token: 0x0400016A RID: 362
	public bool attack = true;

	// Token: 0x0400016B RID: 363
	public long xu;

	// Token: 0x0400016C RID: 364
	public int xuInBox;

	// Token: 0x0400016D RID: 365
	public int yen;

	// Token: 0x0400016E RID: 366
	public int gold_lock;

	// Token: 0x0400016F RID: 367
	public int luong;

	// Token: 0x04000170 RID: 368
	public int luongKhoa;

	// Token: 0x04000171 RID: 369
	public NClass nClass;

	// Token: 0x04000172 RID: 370
	public Command endMovePointCommand;

	// Token: 0x04000173 RID: 371
	public MyVector vSkill = new MyVector();

	// Token: 0x04000174 RID: 372
	public MyVector vSkillFight = new MyVector();

	// Token: 0x04000175 RID: 373
	public MyVector vEff = new MyVector();

	// Token: 0x04000176 RID: 374
	public Skill myskill;

	// Token: 0x04000177 RID: 375
	public Task taskMaint;

	// Token: 0x04000178 RID: 376
	public bool paintName = true;

	// Token: 0x04000179 RID: 377
	public Archivement[] arrArchive;

	// Token: 0x0400017A RID: 378
	public Item[] arrItemBag;

	// Token: 0x0400017B RID: 379
	public Item[] arrItemBox;

	// Token: 0x0400017C RID: 380
	public Item[] arrItemBody;

	// Token: 0x0400017D RID: 381
	public Skill[] arrPetSkill;

	// Token: 0x0400017E RID: 382
	public Item[][] arrItemShop;

	// Token: 0x0400017F RID: 383
	public string[][] infoSpeacialSkill;

	// Token: 0x04000180 RID: 384
	public short[][] imgSpeacialSkill;

	// Token: 0x04000181 RID: 385
	public short cResFire;

	// Token: 0x04000182 RID: 386
	public short cResIce;

	// Token: 0x04000183 RID: 387
	public short cResWind;

	// Token: 0x04000184 RID: 388
	public short cMiss;

	// Token: 0x04000185 RID: 389
	public short cExactly;

	// Token: 0x04000186 RID: 390
	public short cFatal;

	// Token: 0x04000187 RID: 391
	public sbyte cPk;

	// Token: 0x04000188 RID: 392
	public sbyte cTypePk;

	// Token: 0x04000189 RID: 393
	public short cReactDame;

	// Token: 0x0400018A RID: 394
	public short sysUp;

	// Token: 0x0400018B RID: 395
	public short sysDown;

	// Token: 0x0400018C RID: 396
	public int avatar;

	// Token: 0x0400018D RID: 397
	public int skillTemplateId;

	// Token: 0x0400018E RID: 398
	public Mob mobFocus;

	// Token: 0x0400018F RID: 399
	public Mob mobMe;

	// Token: 0x04000190 RID: 400
	public int tMobMeBorn;

	// Token: 0x04000191 RID: 401
	public Npc npcFocus;

	// Token: 0x04000192 RID: 402
	public global::Char charFocus;

	// Token: 0x04000193 RID: 403
	public ItemMap itemFocus;

	// Token: 0x04000194 RID: 404
	public MyVector focus = new MyVector();

	// Token: 0x04000195 RID: 405
	public Mob[] attMobs;

	// Token: 0x04000196 RID: 406
	public global::Char[] attChars;

	// Token: 0x04000197 RID: 407
	public short[] moveFast;

	// Token: 0x04000198 RID: 408
	public int testCharId = -9999;

	// Token: 0x04000199 RID: 409
	public int killCharId = -9999;

	// Token: 0x0400019A RID: 410
	public sbyte resultTest;

	// Token: 0x0400019B RID: 411
	public int countKill;

	// Token: 0x0400019C RID: 412
	public int countKillMax;

	// Token: 0x0400019D RID: 413
	public bool isInvisiblez;

	// Token: 0x0400019E RID: 414
	public bool isShadown = true;

	// Token: 0x0400019F RID: 415
	public const sbyte PK_NORMAL = 0;

	// Token: 0x040001A0 RID: 416
	public const sbyte PK_PHE = 1;

	// Token: 0x040001A1 RID: 417
	public const sbyte PK_BANG = 2;

	// Token: 0x040001A2 RID: 418
	public const sbyte PK_THIDAU = 3;

	// Token: 0x040001A3 RID: 419
	public const sbyte PK_LUYENTAP = 4;

	// Token: 0x040001A4 RID: 420
	public const sbyte PK_TUDO = 5;

	// Token: 0x040001A5 RID: 421
	public MyVector taskOrders = new MyVector();

	// Token: 0x040001A6 RID: 422
	public int cStamina;

	// Token: 0x040001A7 RID: 423
	public static short[] idHead;

	// Token: 0x040001A8 RID: 424
	public static short[] idAvatar;

	// Token: 0x040001A9 RID: 425
	public int exp;

	// Token: 0x040001AA RID: 426
	public string[] strLevel;

	// Token: 0x040001AB RID: 427
	public string currStrLevel;

	// Token: 0x040001AC RID: 428
	public static Image eyeTraiDat = GameCanvas.loadImage("/mainImage/myTexture2dmat-trai-dat.png");

	// Token: 0x040001AD RID: 429
	public static Image eyeNamek = GameCanvas.loadImage("/mainImage/myTexture2dmat-namek.png");

	// Token: 0x040001AE RID: 430
	public bool isFreez;

	// Token: 0x040001AF RID: 431
	public bool isCharge;

	// Token: 0x040001B0 RID: 432
	public int seconds;

	// Token: 0x040001B1 RID: 433
	public int freezSeconds;

	// Token: 0x040001B2 RID: 434
	public long last;

	// Token: 0x040001B3 RID: 435
	public long cur;

	// Token: 0x040001B4 RID: 436
	public long lastFreez;

	// Token: 0x040001B5 RID: 437
	public long currFreez;

	// Token: 0x040001B6 RID: 438
	public bool isFlyUp;

	// Token: 0x040001B7 RID: 439
	public static MyVector vItemTime = new MyVector();

	// Token: 0x040001B8 RID: 440
	public static short ID_NEW_MOUNT = 30000;

	// Token: 0x040001B9 RID: 441
	public short idMount;

	// Token: 0x040001BA RID: 442
	public bool isHaveMount;

	// Token: 0x040001BB RID: 443
	public bool isMountVip;

	// Token: 0x040001BC RID: 444
	public bool isEventMount;

	// Token: 0x040001BD RID: 445
	public bool isSpeacialMount;

	// Token: 0x040001BE RID: 446
	public static Image imgMount_TD = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi10.png");

	// Token: 0x040001BF RID: 447
	public static Image imgMount_NM = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi20.png");

	// Token: 0x040001C0 RID: 448
	public static Image imgMount_NM_1 = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi21.png");

	// Token: 0x040001C1 RID: 449
	public static Image imgMount_XD = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi30.png");

	// Token: 0x040001C2 RID: 450
	public static Image imgMount_TD_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi11.png");

	// Token: 0x040001C3 RID: 451
	public static Image imgMount_NM_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi22.png");

	// Token: 0x040001C4 RID: 452
	public static Image imgMount_NM_1_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi23.png");

	// Token: 0x040001C5 RID: 453
	public static Image imgMount_XD_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi31.png");

	// Token: 0x040001C6 RID: 454
	public static Image imgEventMount = GameCanvas.loadImage("/mainImage/myTexture2drong.png");

	// Token: 0x040001C7 RID: 455
	public static Image imgEventMountWing = GameCanvas.loadImage("/mainImage/myTexture2dcanhrong.png");

	// Token: 0x040001C8 RID: 456
	public sbyte[] FrameMount = new sbyte[]
	{
		0,
		0,
		1,
		1,
		2,
		2,
		1,
		1
	};

	// Token: 0x040001C9 RID: 457
	public int frameMount;

	// Token: 0x040001CA RID: 458
	public int frameNewMount;

	// Token: 0x040001CB RID: 459
	public int transMount;

	// Token: 0x040001CC RID: 460
	public int genderMount;

	// Token: 0x040001CD RID: 461
	public int idcharMount;

	// Token: 0x040001CE RID: 462
	public int xMount;

	// Token: 0x040001CF RID: 463
	public int yMount;

	// Token: 0x040001D0 RID: 464
	public int dxMount;

	// Token: 0x040001D1 RID: 465
	public int dyMount;

	// Token: 0x040001D2 RID: 466
	public int xChar;

	// Token: 0x040001D3 RID: 467
	public int xdis;

	// Token: 0x040001D4 RID: 468
	public int speedMount;

	// Token: 0x040001D5 RID: 469
	public bool isStartMount;

	// Token: 0x040001D6 RID: 470
	public bool isMount;

	// Token: 0x040001D7 RID: 471
	public bool isEndMount;

	// Token: 0x040001D8 RID: 472
	public sbyte cFlag;

	// Token: 0x040001D9 RID: 473
	public int flagImage;

	// Token: 0x040001DA RID: 474
	public static int[][][] CharInfo = new int[][][]
	{
		new int[][]
		{
			new int[]
			{
				0,
				-13,
				34
			},
			new int[]
			{
				1,
				-8,
				10
			},
			new int[]
			{
				1,
				-9,
				16
			},
			new int[]
			{
				1,
				-9,
				45
			}
		},
		new int[][]
		{
			new int[]
			{
				0,
				-13,
				35
			},
			new int[]
			{
				1,
				-8,
				10
			},
			new int[]
			{
				1,
				-9,
				17
			},
			new int[]
			{
				1,
				-9,
				46
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				33
			},
			new int[]
			{
				2,
				-10,
				11
			},
			new int[]
			{
				2,
				-8,
				16
			},
			new int[]
			{
				1,
				-12,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				32
			},
			new int[]
			{
				3,
				-12,
				10
			},
			new int[]
			{
				3,
				-11,
				15
			},
			new int[]
			{
				1,
				-13,
				47
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				34
			},
			new int[]
			{
				4,
				-8,
				11
			},
			new int[]
			{
				4,
				-7,
				17
			},
			new int[]
			{
				1,
				-12,
				47
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				34
			},
			new int[]
			{
				5,
				-12,
				11
			},
			new int[]
			{
				5,
				-9,
				17
			},
			new int[]
			{
				1,
				-13,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-10,
				33
			},
			new int[]
			{
				6,
				-10,
				10
			},
			new int[]
			{
				6,
				-8,
				16
			},
			new int[]
			{
				1,
				-12,
				47
			}
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				36
			},
			new int[]
			{
				7,
				-5,
				17
			},
			new int[]
			{
				7,
				-11,
				25
			},
			new int[]
			{
				1,
				-8,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				0,
				-7,
				35
			},
			new int[]
			{
				0,
				-18,
				22
			},
			new int[]
			{
				7,
				-10,
				25
			},
			new int[]
			{
				1,
				-7,
				48
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-11,
				35
			},
			new int[]
			{
				10,
				-3,
				25
			},
			new int[]
			{
				12,
				-10,
				26
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-11,
				37
			},
			new int[]
			{
				11,
				-3,
				25
			},
			new int[]
			{
				12,
				-11,
				27
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-14,
				34
			},
			new int[]
			{
				12,
				-8,
				21
			},
			new int[]
			{
				9,
				-7,
				31
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-12,
				35
			},
			new int[]
			{
				8,
				-5,
				14
			},
			new int[]
			{
				8,
				-15,
				29
			},
			new int[]
			{
				1,
				-9,
				49
			}
		},
		new int[][]
		{
			new int[]
			{
				1,
				-9,
				34
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				10,
				-7,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-13,
				34
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				11,
				-10,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				2,
				-6,
				15
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				13,
				-12,
				16
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-10,
				31
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				7,
				-13,
				20
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-11,
				32
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				8,
				-15,
				26
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				33
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				14,
				-8,
				18
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-11,
				33
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				15,
				-6,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-16,
				31
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				9,
				-8,
				28
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-14,
				34
			},
			new int[]
			{
				1,
				-8,
				10
			},
			new int[]
			{
				8,
				-16,
				28
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-8,
				36
			},
			new int[]
			{
				7,
				-5,
				17
			},
			new int[]
			{
				0,
				-5,
				25
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				31
			},
			new int[]
			{
				9,
				-12,
				9
			},
			new int[]
			{
				0,
				-6,
				20
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				2,
				-9,
				36
			},
			new int[]
			{
				13,
				-5,
				17
			},
			new int[]
			{
				16,
				-11,
				25
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-9,
				34
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				10,
				-7,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-13,
				34
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				11,
				-10,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				2,
				-6,
				15
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				1,
				-8,
				32
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				13,
				-12,
				16
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-9,
				33
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				14,
				-8,
				18
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-11,
				33
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				15,
				-6,
				19
			},
			new int[3]
		},
		new int[][]
		{
			new int[]
			{
				0,
				-16,
				32
			},
			new int[]
			{
				8,
				-5,
				13
			},
			new int[]
			{
				9,
				-8,
				29
			},
			new int[3]
		}
	};

	// Token: 0x040001DB RID: 475
	public static int[] CHAR_WEAPONX = new int[]
	{
		-2,
		-6,
		22,
		21,
		19,
		22,
		10,
		-2,
		-2,
		5,
		19
	};

	// Token: 0x040001DC RID: 476
	public static int[] CHAR_WEAPONY = new int[]
	{
		9,
		22,
		25,
		17,
		26,
		37,
		36,
		49,
		50,
		52,
		36
	};

	// Token: 0x040001DD RID: 477
	private static global::Char myChar;

	// Token: 0x040001DE RID: 478
	private static global::Char myPet;

	// Token: 0x040001DF RID: 479
	public static int[] listAttack;

	// Token: 0x040001E0 RID: 480
	public static int[][] listIonC;

	// Token: 0x040001E1 RID: 481
	public int cvyJump;

	// Token: 0x040001E2 RID: 482
	private int indexUseSkill = -1;

	// Token: 0x040001E3 RID: 483
	public int cxSend;

	// Token: 0x040001E4 RID: 484
	public int cySend;

	// Token: 0x040001E5 RID: 485
	public int cdirSend = 1;

	// Token: 0x040001E6 RID: 486
	public int cxFocus;

	// Token: 0x040001E7 RID: 487
	public int cyFocus;

	// Token: 0x040001E8 RID: 488
	public int cactFirst = 5;

	// Token: 0x040001E9 RID: 489
	public MyVector vMovePoints = new MyVector();

	// Token: 0x040001EA RID: 490
	public static string[][] inforClass = new string[][]
	{
		new string[]
		{
			"1",
			"1",
			"chiêu 1",
			"0"
		},
		new string[]
		{
			"2",
			"2",
			"chiêu 2",
			"5"
		}
	};

	// Token: 0x040001EB RID: 491
	public static int[][] inforSkill = new int[][]
	{
		new int[]
		{
			1,
			0,
			1,
			1000,
			40,
			1,
			0,
			20,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			1,
			10,
			1000,
			100,
			1,
			0,
			40,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			2,
			11,
			800,
			100,
			1,
			0,
			45,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			3,
			12,
			600,
			100,
			1,
			0,
			50,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			2,
			4,
			13,
			500,
			100,
			1,
			0,
			55,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			1,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			2,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			3,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			4,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		},
		new int[]
		{
			3,
			5,
			14,
			500,
			100,
			1,
			0,
			60,
			0,
			0,
			0,
			0
		}
	};

	// Token: 0x040001EC RID: 492
	public static bool flag;

	// Token: 0x040001ED RID: 493
	public static bool ischangingMap;

	// Token: 0x040001EE RID: 494
	public static bool isLockKey;

	// Token: 0x040001EF RID: 495
	public static bool isLoadingMap;

	// Token: 0x040001F0 RID: 496
	public bool isLockMove;

	// Token: 0x040001F1 RID: 497
	public bool isLockAttack;

	// Token: 0x040001F2 RID: 498
	public string strInfo;

	// Token: 0x040001F3 RID: 499
	public short powerPoint;

	// Token: 0x040001F4 RID: 500
	public short maxPowerPoint;

	// Token: 0x040001F5 RID: 501
	public short secondPower;

	// Token: 0x040001F6 RID: 502
	public long lastS;

	// Token: 0x040001F7 RID: 503
	public long currS;

	// Token: 0x040001F8 RID: 504
	public bool havePet = true;

	// Token: 0x040001F9 RID: 505
	public MovePoint currentMovePoint;

	// Token: 0x040001FA RID: 506
	public int bom;

	// Token: 0x040001FB RID: 507
	public int delayFall;

	// Token: 0x040001FC RID: 508
	private bool isSoundJump;

	// Token: 0x040001FD RID: 509
	public int lastFrame;

	// Token: 0x040001FE RID: 510
	private Effect eProtect;

	// Token: 0x040001FF RID: 511
	private int twHp;

	// Token: 0x04000200 RID: 512
	public bool isInjureHp;

	// Token: 0x04000201 RID: 513
	public bool changePos;

	// Token: 0x04000202 RID: 514
	private bool isHide;

	// Token: 0x04000203 RID: 515
	private bool wy;

	// Token: 0x04000204 RID: 516
	public int wt;

	// Token: 0x04000205 RID: 517
	public int fy;

	// Token: 0x04000206 RID: 518
	public int ty;

	// Token: 0x04000207 RID: 519
	private int t;

	// Token: 0x04000208 RID: 520
	private int fM;

	// Token: 0x04000209 RID: 521
	public int[] move = new int[]
	{
		1,
		1,
		1,
		1,
		2,
		2,
		2,
		2,
		3,
		3,
		3,
		3,
		2,
		2,
		2
	};

	// Token: 0x0400020A RID: 522
	private string strMount = "mount_";

	// Token: 0x0400020B RID: 523
	public int headICON = -1;

	// Token: 0x0400020C RID: 524
	public int head;

	// Token: 0x0400020D RID: 525
	public int leg;

	// Token: 0x0400020E RID: 526
	public int body;

	// Token: 0x0400020F RID: 527
	public int bag;

	// Token: 0x04000210 RID: 528
	public int wp;

	// Token: 0x04000211 RID: 529
	public int indexEff = -1;

	// Token: 0x04000212 RID: 530
	public int indexEffTask = -1;

	// Token: 0x04000213 RID: 531
	public EffectCharPaint eff;

	// Token: 0x04000214 RID: 532
	public EffectCharPaint effTask;

	// Token: 0x04000215 RID: 533
	public int indexSkill;

	// Token: 0x04000216 RID: 534
	public int i0;

	// Token: 0x04000217 RID: 535
	public int i1;

	// Token: 0x04000218 RID: 536
	public int i2;

	// Token: 0x04000219 RID: 537
	public int dx0;

	// Token: 0x0400021A RID: 538
	public int dx1;

	// Token: 0x0400021B RID: 539
	public int dx2;

	// Token: 0x0400021C RID: 540
	public int dy0;

	// Token: 0x0400021D RID: 541
	public int dy1;

	// Token: 0x0400021E RID: 542
	public int dy2;

	// Token: 0x0400021F RID: 543
	public EffectCharPaint eff0;

	// Token: 0x04000220 RID: 544
	public EffectCharPaint eff1;

	// Token: 0x04000221 RID: 545
	public EffectCharPaint eff2;

	// Token: 0x04000222 RID: 546
	public Arrow arr;

	// Token: 0x04000223 RID: 547
	public PlayerDart dart;

	// Token: 0x04000224 RID: 548
	public bool isCreateDark;

	// Token: 0x04000225 RID: 549
	public SkillPaint skillPaint;

	// Token: 0x04000226 RID: 550
	public SkillPaint skillPaintRandomPaint;

	// Token: 0x04000227 RID: 551
	public EffectPaint[] effPaints;

	// Token: 0x04000228 RID: 552
	public int sType;

	// Token: 0x04000229 RID: 553
	public sbyte isInjure;

	// Token: 0x0400022A RID: 554
	public bool isUseSkillAfterCharge;

	// Token: 0x0400022B RID: 555
	public bool isFlyAndCharge;

	// Token: 0x0400022C RID: 556
	public bool isStandAndCharge;

	// Token: 0x0400022D RID: 557
	private bool isFlying;

	// Token: 0x0400022E RID: 558
	public int posDisY;

	// Token: 0x0400022F RID: 559
	private int chargeCount;

	// Token: 0x04000230 RID: 560
	private bool hasSendAttack;

	// Token: 0x04000231 RID: 561
	public bool isMabuHold;

	// Token: 0x04000232 RID: 562
	private long timeBlue;

	// Token: 0x04000233 RID: 563
	private int tBlue;

	// Token: 0x04000234 RID: 564
	private bool IsAddDust1;

	// Token: 0x04000235 RID: 565
	private bool IsAddDust2;

	// Token: 0x04000236 RID: 566
	public bool isPet;

	// Token: 0x04000237 RID: 567
	public bool isMiniPet;

	// Token: 0x04000238 RID: 568
	public int xSd;

	// Token: 0x04000239 RID: 569
	public int ySd;

	// Token: 0x0400023A RID: 570
	private bool isOutMap;

	// Token: 0x0400023B RID: 571
	private int fBag;

	// Token: 0x0400023C RID: 572
	private int statusBeforeNothing;

	// Token: 0x0400023D RID: 573
	private int timeFocusToMob;

	// Token: 0x0400023E RID: 574
	public static bool isManualFocus = false;

	// Token: 0x0400023F RID: 575
	private global::Char charHold;

	// Token: 0x04000240 RID: 576
	private Mob mobHold;

	// Token: 0x04000241 RID: 577
	private int nInjure;

	// Token: 0x04000242 RID: 578
	public short wdx;

	// Token: 0x04000243 RID: 579
	public short wdy;

	// Token: 0x04000244 RID: 580
	public bool isDirtyPostion;

	// Token: 0x04000245 RID: 581
	public Skill lastNormalSkill;

	// Token: 0x04000246 RID: 582
	public bool currentFireByShortcut;

	// Token: 0x04000247 RID: 583
	public int cDamGoc;

	// Token: 0x04000248 RID: 584
	public int cHPGoc;

	// Token: 0x04000249 RID: 585
	public int cMPGoc;

	// Token: 0x0400024A RID: 586
	public int cDefGoc;

	// Token: 0x0400024B RID: 587
	public int cCriticalGoc;

	// Token: 0x0400024C RID: 588
	public sbyte hpFrom1000TiemNang;

	// Token: 0x0400024D RID: 589
	public sbyte mpFrom1000TiemNang;

	// Token: 0x0400024E RID: 590
	public sbyte damFrom1000TiemNang;

	// Token: 0x0400024F RID: 591
	public sbyte defFrom1000TiemNang = 1;

	// Token: 0x04000250 RID: 592
	public sbyte criticalFrom1000Tiemnang = 1;

	// Token: 0x04000251 RID: 593
	public short cMaxStamina;

	// Token: 0x04000252 RID: 594
	public short expForOneAdd;

	// Token: 0x04000253 RID: 595
	public sbyte isMonkey;

	// Token: 0x04000254 RID: 596
	public bool isCopy;

	// Token: 0x04000255 RID: 597
	public bool isWaitMonkey;

	// Token: 0x04000256 RID: 598
	private bool isFeetEff;

	// Token: 0x04000257 RID: 599
	public bool meDead;

	// Token: 0x04000258 RID: 600
	public int holdEffID;

	// Token: 0x04000259 RID: 601
	public bool holder;

	// Token: 0x0400025A RID: 602
	public bool protectEff;

	// Token: 0x0400025B RID: 603
	private bool isSetPos;

	// Token: 0x0400025C RID: 604
	private int tpos;

	// Token: 0x0400025D RID: 605
	private short xPos;

	// Token: 0x0400025E RID: 606
	private short yPos;

	// Token: 0x0400025F RID: 607
	private sbyte typePos;

	// Token: 0x04000260 RID: 608
	private bool isMyFusion;

	// Token: 0x04000261 RID: 609
	public bool isFusion;

	// Token: 0x04000262 RID: 610
	public int tFusion;

	// Token: 0x04000263 RID: 611
	public bool huytSao;

	// Token: 0x04000264 RID: 612
	public bool blindEff;

	// Token: 0x04000265 RID: 613
	public bool telePortSkill;

	// Token: 0x04000266 RID: 614
	public bool sleepEff;

	// Token: 0x04000267 RID: 615
	public bool stone;

	// Token: 0x04000268 RID: 616
	public int perCentMp = 100;

	// Token: 0x04000269 RID: 617
	public int dHP;

	// Token: 0x0400026A RID: 618
	public int headTemp = -1;

	// Token: 0x0400026B RID: 619
	public int bodyTemp = -1;

	// Token: 0x0400026C RID: 620
	public int legTemp = -1;

	// Token: 0x0400026D RID: 621
	public int bagTemp = -1;

	// Token: 0x0400026E RID: 622
	public int wpTemp = -1;

	// Token: 0x0400026F RID: 623
	public MyVector vEffChar = new MyVector("vEff");

	// Token: 0x04000270 RID: 624
	public static FrameImage fraRedEye;

	// Token: 0x04000271 RID: 625
	private int fChopmat;

	// Token: 0x04000272 RID: 626
	private bool isAddChopMat;

	// Token: 0x04000273 RID: 627
	private long timeAddChopmat;

	// Token: 0x04000274 RID: 628
	private int[] frChopNhanh = new int[]
	{
		-1,
		-1,
		-1,
		-1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		-1,
		-1,
		-1,
		-1
	};

	// Token: 0x04000275 RID: 629
	private int[] frChopCham = new int[]
	{
		-1,
		-1,
		-1,
		-1,
		0,
		0,
		1,
		1,
		1,
		0,
		0,
		1,
		1,
		1,
		0,
		0,
		1,
		1,
		1,
		-1,
		-1,
		-1,
		-1
	};

	// Token: 0x04000276 RID: 630
	private int[] frEye = new int[]
	{
		-1,
		-1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		1,
		1,
		0,
		0,
		-1,
		-1
	};

	// Token: 0x04000277 RID: 631
	public static int[][] Arr_Head_2Fr = new int[][]
	{
		new int[]
		{
			542,
			543
		}
	};

	// Token: 0x04000278 RID: 632
	private int fHead;

	// Token: 0x04000279 RID: 633
	private string strEffAura = "aura_";

	// Token: 0x0400027A RID: 634
	public short idAuraEff = -1;

	// Token: 0x0400027B RID: 635
	public static bool isPaintAura = true;

	// Token: 0x0400027C RID: 636
	private FrameImage fraEff;

	// Token: 0x0400027D RID: 637
	private FrameImage fraEffSub;

	// Token: 0x0400027E RID: 638
	private string strEff_Set_Item = "set_eff_";

	// Token: 0x0400027F RID: 639
	public short idEff_Set_Item = -1;

	// Token: 0x04000280 RID: 640
	private FrameImage fraHat_behind;

	// Token: 0x04000281 RID: 641
	private FrameImage fraHat_font;

	// Token: 0x04000282 RID: 642
	private FrameImage fraHat_behind_2;

	// Token: 0x04000283 RID: 643
	private FrameImage fraHat_font_2;

	// Token: 0x04000284 RID: 644
	private string strHat_behind = "hat_sau_";

	// Token: 0x04000285 RID: 645
	private string strHat_font = "hat_truoc_";

	// Token: 0x04000286 RID: 646
	private string strNgang = "ngang_";

	// Token: 0x04000287 RID: 647
	public short idHat = -1;

	// Token: 0x04000288 RID: 648
	public static int[][] hatInfo;
}
