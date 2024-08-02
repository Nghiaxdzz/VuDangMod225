using System;
using AssemblyCSharp.Mod.Xmap;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class Controller : IMessageHandler
{
	// Token: 0x06000179 RID: 377 RVA: 0x0001F638 File Offset: 0x0001D838
	public static Controller gI()
	{
		bool flag = Controller.me == null;
		if (flag)
		{
			Controller.me = new Controller();
		}
		return Controller.me;
	}

	// Token: 0x0600017A RID: 378 RVA: 0x0001F668 File Offset: 0x0001D868
	public static Controller gI2()
	{
		bool flag = Controller.me2 == null;
		if (flag)
		{
			Controller.me2 = new Controller();
		}
		return Controller.me2;
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00003E76 File Offset: 0x00002076
	public void onConnectOK(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onConnectOK();
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00003E85 File Offset: 0x00002085
	public void onConnectionFail(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onConnectionFail();
	}

	// Token: 0x0600017D RID: 381 RVA: 0x00003E94 File Offset: 0x00002094
	public void onDisconnected(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onDisconnected();
	}

	// Token: 0x0600017E RID: 382 RVA: 0x0001F698 File Offset: 0x0001D898
	public void requestItemPlayer(Message msg)
	{
		try
		{
			int num = (int)msg.reader().readUnsignedByte();
			Item item = GameScr.currentCharViewInfo.arrItemBody[num];
			item.saleCoinLock = msg.reader().readInt();
			item.sys = (int)msg.reader().readByte();
			item.options = new MyVector();
			try
			{
				for (;;)
				{
					item.options.addElement(new ItemOption((int)msg.reader().readUnsignedByte(), (int)msg.reader().readUnsignedShort()));
				}
			}
			catch (Exception ex)
			{
				Cout.println("Loi tairequestItemPlayer 1" + ex.ToString());
			}
		}
		catch (Exception ex2)
		{
			Cout.println("Loi tairequestItemPlayer 2" + ex2.ToString());
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x0001F774 File Offset: 0x0001D974
	public void onMessage(Message msg)
	{
		GameCanvas.debugSession.removeAllElements();
		GameCanvas.debug("SA1", 2);
		try
		{
			mSystem.LogCMD(">>>cmd= " + msg.command);
			global::Char @char = null;
			MyVector myVector = new MyVector();
			int i = 0;
			Controller2.readMessage(msg);
			sbyte command = msg.command;
			switch (command)
			{
			case -112:
			{
				sbyte b = msg.reader().readByte();
				bool flag = b == 0;
				if (flag)
				{
					sbyte mobIndex = msg.reader().readByte();
					GameScr.findMobInMap(mobIndex).clearBody();
				}
				bool flag2 = b == 1;
				if (flag2)
				{
					sbyte mobIndex2 = msg.reader().readByte();
					GameScr.findMobInMap(mobIndex2).setBody(msg.reader().readShort());
				}
				break;
			}
			case -111:
			case -110:
			case -109:
			case -108:
			case -106:
			case -105:
			case -104:
			case -103:
			case -102:
			case -101:
			case -100:
			case -89:
			case -78:
			case -75:
			case -73:
			case -72:
			case -71:
			case -58:
			case -56:
			case -55:
			case -54:
			case -49:
			case -48:
			case -40:
			case -39:
			case -38:
			case -33:
			case -27:
			case -23:
			case -17:
			case -16:
			case -15:
			case -13:
			case -12:
			case -11:
			case -10:
			case -9:
			case -8:
			case -7:
			case -6:
			case -5:
			case -3:
			case -2:
			case -1:
			case 0:
			case 3:
			case 4:
			case 5:
			case 8:
			case 9:
			case 10:
			case 12:
			case 13:
			case 14:
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 21:
			case 22:
			case 23:
			case 25:
			case 26:
			case 28:
			case 30:
			case 31:
			case 34:
			case 35:
			case 36:
			case 37:
			case 42:
			case 44:
			case 45:
			case 48:
			case 49:
			case 51:
			case 52:
			case 53:
			case 55:
			case 59:
			case 60:
			case 61:
			case 67:
			case 70:
			case 71:
			case 72:
			case 73:
			case 74:
			case 75:
			case 76:
			case 77:
			case 78:
			case 79:
			case 80:
			case 89:
			case 91:
			case 93:
				break;
			case -107:
			{
				sbyte b2 = msg.reader().readByte();
				bool flag3 = b2 == 0;
				if (flag3)
				{
					global::Char.myCharz().havePet = false;
				}
				bool flag4 = b2 == 1;
				if (flag4)
				{
					global::Char.myCharz().havePet = true;
				}
				bool flag5 = b2 != 2;
				if (!flag5)
				{
					InfoDlg.hide();
					global::Char.myPetz().head = (int)msg.reader().readShort();
					global::Char.myPetz().setDefaultPart();
					int num = (int)msg.reader().readUnsignedByte();
					Res.outz("num body = " + num);
					global::Char.myPetz().arrItemBody = new Item[num];
					int num5;
					for (int j = 0; j < num; j = num5 + 1)
					{
						short num2 = msg.reader().readShort();
						Res.outz("template id= " + num2);
						bool flag6 = num2 == -1;
						if (!flag6)
						{
							Res.outz("1");
							global::Char.myPetz().arrItemBody[j] = new Item();
							global::Char.myPetz().arrItemBody[j].template = ItemTemplates.get(num2);
							int type = (int)global::Char.myPetz().arrItemBody[j].template.type;
							global::Char.myPetz().arrItemBody[j].quantity = msg.reader().readInt();
							Res.outz("3");
							global::Char.myPetz().arrItemBody[j].info = msg.reader().readUTF();
							global::Char.myPetz().arrItemBody[j].content = msg.reader().readUTF();
							int num3 = (int)msg.reader().readUnsignedByte();
							Res.outz("option size= " + num3);
							bool flag7 = num3 != 0;
							if (flag7)
							{
								global::Char.myPetz().arrItemBody[j].itemOption = new ItemOption[num3];
								for (int k = 0; k < global::Char.myPetz().arrItemBody[j].itemOption.Length; k = num5 + 1)
								{
									int num4 = (int)msg.reader().readUnsignedByte();
									int param = (int)msg.reader().readUnsignedShort();
									bool flag8 = num4 != -1;
									if (flag8)
									{
										global::Char.myPetz().arrItemBody[j].itemOption[k] = new ItemOption(num4, param);
									}
									num5 = k;
								}
							}
							int num6 = type;
							if (num6 != 0)
							{
								if (num6 == 1)
								{
									global::Char.myPetz().leg = (int)global::Char.myPetz().arrItemBody[j].template.part;
								}
							}
							else
							{
								global::Char.myPetz().body = (int)global::Char.myPetz().arrItemBody[j].template.part;
							}
						}
						num5 = j;
					}
					global::Char.myPetz().cHP = msg.readInt3Byte();
					global::Char.myPetz().cHPFull = msg.readInt3Byte();
					global::Char.myPetz().cMP = msg.readInt3Byte();
					global::Char.myPetz().cMPFull = msg.readInt3Byte();
					global::Char.myPetz().cDamFull = msg.readInt3Byte();
					global::Char.myPetz().cName = msg.reader().readUTF();
					global::Char.myPetz().currStrLevel = msg.reader().readUTF();
					global::Char.myPetz().cPower = msg.reader().readLong();
					global::Char.myPetz().cTiemNang = msg.reader().readLong();
					global::Char.myPetz().petStatus = msg.reader().readByte();
					global::Char.myPetz().cStamina = (int)msg.reader().readShort();
					global::Char.myPetz().cMaxStamina = msg.reader().readShort();
					global::Char.myPetz().cCriticalFull = (int)msg.reader().readByte();
					global::Char.myPetz().cDefull = (int)msg.reader().readShort();
					global::Char.myPetz().arrPetSkill = new Skill[(int)msg.reader().readByte()];
					Res.outz("SKILLENT = " + global::Char.myPetz().arrPetSkill);
					for (int l = 0; l < global::Char.myPetz().arrPetSkill.Length; l = num5 + 1)
					{
						short num7 = msg.reader().readShort();
						bool flag9 = num7 != -1;
						if (flag9)
						{
							global::Char.myPetz().arrPetSkill[l] = Skills.get(num7);
						}
						else
						{
							global::Char.myPetz().arrPetSkill[l] = new Skill();
							global::Char.myPetz().arrPetSkill[l].template = null;
							global::Char.myPetz().arrPetSkill[l].moreInfo = msg.reader().readUTF();
						}
						num5 = l;
					}
				}
				break;
			}
			case -99:
			{
				InfoDlg.hide();
				sbyte b3 = msg.reader().readByte();
				bool flag10 = b3 == 0;
				if (flag10)
				{
					GameCanvas.panel.vEnemy.removeAllElements();
					int num8 = (int)msg.reader().readUnsignedByte();
					int num5;
					for (int m = 0; m < num8; m = num5 + 1)
					{
						global::Char char2 = new global::Char();
						char2.charID = msg.reader().readInt();
						char2.head = (int)msg.reader().readShort();
						char2.headICON = (int)msg.reader().readShort();
						char2.body = (int)msg.reader().readShort();
						char2.leg = (int)msg.reader().readShort();
						char2.bag = (int)msg.reader().readShort();
						char2.cName = msg.reader().readUTF();
						InfoItem infoItem = new InfoItem(msg.reader().readUTF());
						bool isOnline = msg.reader().readBoolean();
						infoItem.charInfo = char2;
						infoItem.isOnline = isOnline;
						Res.outz("isonline = " + isOnline.ToString());
						GameCanvas.panel.vEnemy.addElement(infoItem);
						num5 = m;
					}
					GameCanvas.panel.setTypeEnemy();
					GameCanvas.panel.show();
				}
				break;
			}
			case -98:
			{
				sbyte b4 = msg.reader().readByte();
				GameCanvas.menu.showMenu = false;
				bool flag11 = b4 == 0;
				if (flag11)
				{
					GameCanvas.startYesNoDlg(msg.reader().readUTF(), new Command(mResources.YES, GameCanvas.instance, 888397, msg.reader().readUTF()), new Command(mResources.NO, GameCanvas.instance, 888396, null));
				}
				break;
			}
			case -97:
				global::Char.myCharz().cNangdong = (long)msg.reader().readInt();
				break;
			case -96:
			{
				sbyte typeTop = msg.reader().readByte();
				GameCanvas.panel.vTop.removeAllElements();
				string topName = msg.reader().readUTF();
				sbyte b5 = msg.reader().readByte();
				int num5;
				for (int n = 0; n < (int)b5; n = num5 + 1)
				{
					int rank = msg.reader().readInt();
					int pId = msg.reader().readInt();
					short headID = msg.reader().readShort();
					short headICON = msg.reader().readShort();
					short body = msg.reader().readShort();
					short leg = msg.reader().readShort();
					string name = msg.reader().readUTF();
					string info = msg.reader().readUTF();
					TopInfo topInfo = new TopInfo();
					topInfo.rank = rank;
					topInfo.headID = (int)headID;
					topInfo.headICON = (int)headICON;
					topInfo.body = body;
					topInfo.leg = leg;
					topInfo.name = name;
					topInfo.info = info;
					topInfo.info2 = msg.reader().readUTF();
					topInfo.pId = pId;
					GameCanvas.panel.vTop.addElement(topInfo);
					num5 = n;
				}
				GameCanvas.panel.topName = topName;
				GameCanvas.panel.setTypeTop(typeTop);
				GameCanvas.panel.show();
				break;
			}
			case -95:
			{
				sbyte b6 = msg.reader().readByte();
				Res.outz("type= " + b6);
				bool flag12 = b6 == 0;
				if (flag12)
				{
					int num9 = msg.reader().readInt();
					short templateId = msg.reader().readShort();
					int num10 = msg.readInt3Byte();
					SoundMn.gI().explode_1();
					bool flag13 = num9 == global::Char.myCharz().charID;
					if (flag13)
					{
						global::Char.myCharz().mobMe = new Mob(num9, false, false, false, false, false, (int)templateId, 1, num10, 0, num10, (short)(global::Char.myCharz().cx + ((global::Char.myCharz().cdir != 1) ? -40 : 40)), (short)global::Char.myCharz().cy, 4, 0);
						global::Char.myCharz().mobMe.isMobMe = true;
						EffecMn.addEff(new Effect(18, global::Char.myCharz().mobMe.x, global::Char.myCharz().mobMe.y, 2, 10, -1));
						global::Char.myCharz().tMobMeBorn = 30;
						GameScr.vMob.addElement(global::Char.myCharz().mobMe);
					}
					else
					{
						@char = GameScr.findCharInMap(num9);
						bool flag14 = @char != null;
						if (flag14)
						{
							@char.mobMe = new Mob(num9, false, false, false, false, false, (int)templateId, 1, num10, 0, num10, (short)@char.cx, (short)@char.cy, 4, 0)
							{
								isMobMe = true
							};
							GameScr.vMob.addElement(@char.mobMe);
						}
						else
						{
							Mob mob = GameScr.findMobInMap(num9);
							bool flag15 = mob == null;
							if (flag15)
							{
								mob = new Mob(num9, false, false, false, false, false, (int)templateId, 1, num10, 0, num10, -100, -100, 4, 0);
								mob.isMobMe = true;
								GameScr.vMob.addElement(mob);
							}
						}
					}
				}
				bool flag16 = b6 == 1;
				if (flag16)
				{
					int num11 = msg.reader().readInt();
					int mobId = (int)msg.reader().readByte();
					Res.outz("mod attack id= " + num11);
					bool flag17 = num11 == global::Char.myCharz().charID;
					if (flag17)
					{
						bool flag18 = GameScr.findMobInMap(mobId) != null;
						if (flag18)
						{
							global::Char.myCharz().mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
						}
					}
					else
					{
						@char = GameScr.findCharInMap(num11);
						bool flag19 = @char != null && GameScr.findMobInMap(mobId) != null;
						if (flag19)
						{
							@char.mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
						}
					}
				}
				bool flag20 = b6 == 2;
				if (flag20)
				{
					int num12 = msg.reader().readInt();
					int num13 = msg.reader().readInt();
					int num14 = msg.readInt3Byte();
					int cHPNew = msg.readInt3Byte();
					bool flag21 = num12 == global::Char.myCharz().charID;
					if (flag21)
					{
						Res.outz("mob dame= " + num14);
						@char = GameScr.findCharInMap(num13);
						bool flag22 = @char != null;
						if (flag22)
						{
							@char.cHPNew = cHPNew;
							bool isBusyAttackSomeOne = global::Char.myCharz().mobMe.isBusyAttackSomeOne;
							if (isBusyAttackSomeOne)
							{
								@char.doInjure(num14, 0, false, true);
							}
							else
							{
								global::Char.myCharz().mobMe.dame = num14;
								global::Char.myCharz().mobMe.setAttack(@char);
							}
						}
					}
					else
					{
						Mob mob2 = GameScr.findMobInMap(num12);
						bool flag23 = mob2 != null;
						if (flag23)
						{
							bool flag24 = num13 == global::Char.myCharz().charID;
							if (flag24)
							{
								global::Char.myCharz().cHPNew = cHPNew;
								bool isBusyAttackSomeOne2 = mob2.isBusyAttackSomeOne;
								if (isBusyAttackSomeOne2)
								{
									global::Char.myCharz().doInjure(num14, 0, false, true);
								}
								else
								{
									mob2.dame = num14;
									mob2.setAttack(global::Char.myCharz());
								}
							}
							else
							{
								@char = GameScr.findCharInMap(num13);
								bool flag25 = @char != null;
								if (flag25)
								{
									@char.cHPNew = cHPNew;
									bool isBusyAttackSomeOne3 = mob2.isBusyAttackSomeOne;
									if (isBusyAttackSomeOne3)
									{
										@char.doInjure(num14, 0, false, true);
									}
									else
									{
										mob2.dame = num14;
										mob2.setAttack(@char);
									}
								}
							}
						}
					}
				}
				bool flag26 = b6 == 3;
				if (flag26)
				{
					int num15 = msg.reader().readInt();
					int mobId2 = msg.reader().readInt();
					int hp = msg.readInt3Byte();
					int num16 = msg.readInt3Byte();
					@char = null;
					@char = ((global::Char.myCharz().charID != num15) ? GameScr.findCharInMap(num15) : global::Char.myCharz());
					bool flag27 = @char != null;
					if (flag27)
					{
						Mob mob2 = GameScr.findMobInMap(mobId2);
						bool flag28 = @char.mobMe != null;
						if (flag28)
						{
							@char.mobMe.attackOtherMob(mob2);
						}
						bool flag29 = mob2 != null;
						if (flag29)
						{
							mob2.hp = hp;
							bool flag30 = num16 == 0;
							if (flag30)
							{
								mob2.x = mob2.xFirst;
								mob2.y = mob2.yFirst;
								GameScr.startFlyText(mResources.miss, mob2.x, mob2.y - mob2.h, 0, -2, mFont.MISS);
							}
							else
							{
								GameScr.startFlyText("-" + num16, mob2.x, mob2.y - mob2.h, 0, -2, mFont.ORANGE);
							}
						}
					}
				}
				bool flag31 = b6 == 4;
				if (flag31)
				{
				}
				bool flag32 = b6 == 5;
				if (flag32)
				{
					int num17 = msg.reader().readInt();
					sbyte b7 = msg.reader().readByte();
					int mobId3 = msg.reader().readInt();
					int num18 = msg.readInt3Byte();
					int hp2 = msg.readInt3Byte();
					@char = null;
					@char = ((num17 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num17) : global::Char.myCharz());
					bool flag33 = @char == null;
					if (flag33)
					{
						return;
					}
					bool flag34 = (TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2;
					if (flag34)
					{
						@char.setSkillPaint(GameScr.sks[(int)b7], 0);
					}
					else
					{
						@char.setSkillPaint(GameScr.sks[(int)b7], 1);
					}
					Mob mob3 = GameScr.findMobInMap(mobId3);
					bool flag35 = @char.cx <= mob3.x;
					if (flag35)
					{
						@char.cdir = 1;
					}
					else
					{
						@char.cdir = -1;
					}
					@char.mobFocus = mob3;
					mob3.hp = hp2;
					GameCanvas.debug("SA83v2", 2);
					bool flag36 = num18 == 0;
					if (flag36)
					{
						mob3.x = mob3.xFirst;
						mob3.y = mob3.yFirst;
						GameScr.startFlyText(mResources.miss, mob3.x, mob3.y - mob3.h, 0, -2, mFont.MISS);
					}
					else
					{
						GameScr.startFlyText("-" + num18, mob3.x, mob3.y - mob3.h, 0, -2, mFont.ORANGE);
					}
				}
				bool flag37 = b6 == 6;
				if (flag37)
				{
					int num19 = msg.reader().readInt();
					bool flag38 = num19 == global::Char.myCharz().charID;
					if (flag38)
					{
						global::Char.myCharz().mobMe.startDie();
					}
					else
					{
						global::Char char3 = GameScr.findCharInMap(num19);
						if (char3 != null)
						{
							char3.mobMe.startDie();
						}
					}
				}
				bool flag39 = b6 != 7;
				if (!flag39)
				{
					int num20 = msg.reader().readInt();
					bool flag40 = num20 == global::Char.myCharz().charID;
					if (flag40)
					{
						global::Char.myCharz().mobMe = null;
						int num5;
						for (int num21 = 0; num21 < GameScr.vMob.size(); num21 = num5 + 1)
						{
							bool flag41 = ((Mob)GameScr.vMob.elementAt(num21)).mobId == num20;
							if (flag41)
							{
								GameScr.vMob.removeElementAt(num21);
							}
							num5 = num21;
						}
					}
					else
					{
						@char = GameScr.findCharInMap(num20);
						int num5;
						for (int num22 = 0; num22 < GameScr.vMob.size(); num22 = num5 + 1)
						{
							bool flag42 = ((Mob)GameScr.vMob.elementAt(num22)).mobId == num20;
							if (flag42)
							{
								GameScr.vMob.removeElementAt(num22);
							}
							num5 = num22;
						}
						bool flag43 = @char != null;
						if (flag43)
						{
							@char.mobMe = null;
						}
					}
				}
				break;
			}
			case -94:
				while (msg.reader().available() > 0)
				{
					short num23 = msg.reader().readShort();
					int num24 = msg.reader().readInt();
					int num5;
					for (int num25 = 0; num25 < global::Char.myCharz().vSkill.size(); num25 = num5 + 1)
					{
						Skill skill = (Skill)global::Char.myCharz().vSkill.elementAt(num25);
						bool flag44 = skill != null && skill.skillId == num23;
						if (flag44)
						{
							bool flag45 = num24 < skill.coolDown;
							if (flag45)
							{
								skill.lastTimeUseThisSkill = mSystem.currentTimeMillis() - (long)(skill.coolDown - num24);
							}
							Res.outz(string.Concat(new object[]
							{
								"1 chieu id= ",
								skill.template.id,
								" cooldown= ",
								num24,
								"curr cool down= ",
								skill.coolDown
							}));
						}
						num5 = num25;
					}
				}
				break;
			case -93:
			{
				short num26 = msg.reader().readShort();
				BgItem.newSmallVersion = new sbyte[(int)num26];
				int num5;
				for (int num27 = 0; num27 < (int)num26; num27 = num5 + 1)
				{
					BgItem.newSmallVersion[num27] = msg.reader().readByte();
					num5 = num27;
				}
				break;
			}
			case -92:
				Main.typeClient = (int)msg.reader().readByte();
				Rms.clearAll();
				Rms.saveRMSInt("clienttype", Main.typeClient);
				Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
				GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
				break;
			case -91:
			{
				sbyte b8 = msg.reader().readByte();
				GameCanvas.panel.mapNames = new string[(int)b8];
				GameCanvas.panel.planetNames = new string[(int)b8];
				int num5;
				for (int num28 = 0; num28 < (int)b8; num28 = num5 + 1)
				{
					GameCanvas.panel.mapNames[num28] = msg.reader().readUTF();
					GameCanvas.panel.planetNames[num28] = msg.reader().readUTF();
					num5 = num28;
				}
				GameCanvas.panel.setTypeMapTrans();
				GameCanvas.panel.show();
				Pk9rXmap.ShowPanelMapTrans();
				break;
			}
			case -90:
			{
				sbyte b9 = msg.reader().readByte();
				Res.outz("type = " + b9);
				int num29 = msg.reader().readInt();
				bool flag46 = b9 != -1;
				if (flag46)
				{
					short num30 = msg.reader().readShort();
					short num31 = msg.reader().readShort();
					short num32 = msg.reader().readShort();
					sbyte b10 = msg.reader().readByte();
					Res.outz("is Monkey = " + b10);
					bool flag47 = global::Char.myCharz().charID == num29;
					if (flag47)
					{
						global::Char.myCharz().isMask = true;
						global::Char.myCharz().isMonkey = b10;
						bool flag48 = global::Char.myCharz().isMonkey != 0;
						if (flag48)
						{
							global::Char.myCharz().isWaitMonkey = false;
							global::Char.myCharz().isLockMove = false;
						}
					}
					else
					{
						bool flag49 = GameScr.findCharInMap(num29) != null;
						if (flag49)
						{
							GameScr.findCharInMap(num29).isMask = true;
							GameScr.findCharInMap(num29).isMonkey = b10;
						}
					}
					bool flag50 = num30 != -1;
					if (flag50)
					{
						bool flag51 = num29 == global::Char.myCharz().charID;
						if (flag51)
						{
							global::Char.myCharz().head = (int)num30;
						}
						else
						{
							bool flag52 = GameScr.findCharInMap(num29) != null;
							if (flag52)
							{
								GameScr.findCharInMap(num29).head = (int)num30;
							}
						}
					}
					bool flag53 = num31 != -1;
					if (flag53)
					{
						bool flag54 = num29 == global::Char.myCharz().charID;
						if (flag54)
						{
							global::Char.myCharz().body = (int)num31;
						}
						else
						{
							bool flag55 = GameScr.findCharInMap(num29) != null;
							if (flag55)
							{
								GameScr.findCharInMap(num29).body = (int)num31;
							}
						}
					}
					bool flag56 = num32 != -1;
					if (flag56)
					{
						bool flag57 = num29 == global::Char.myCharz().charID;
						if (flag57)
						{
							global::Char.myCharz().leg = (int)num32;
						}
						else
						{
							bool flag58 = GameScr.findCharInMap(num29) != null;
							if (flag58)
							{
								GameScr.findCharInMap(num29).leg = (int)num32;
							}
						}
					}
				}
				bool flag59 = b9 == -1;
				if (flag59)
				{
					bool flag60 = global::Char.myCharz().charID == num29;
					if (flag60)
					{
						global::Char.myCharz().isMask = false;
						global::Char.myCharz().isMonkey = 0;
					}
					else
					{
						bool flag61 = GameScr.findCharInMap(num29) != null;
						if (flag61)
						{
							GameScr.findCharInMap(num29).isMask = false;
							GameScr.findCharInMap(num29).isMonkey = 0;
						}
					}
				}
				break;
			}
			case -88:
				GameCanvas.endDlg();
				GameCanvas.serverScreen.switchToMe();
				break;
			case -87:
			{
				Res.outz("GET UPDATE_DATA " + msg.reader().available() + " bytes");
				msg.reader().mark(100000);
				this.createData(msg.reader(), true);
				msg.reader().reset();
				sbyte[] array = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref array);
				sbyte[] data = new sbyte[]
				{
					GameScr.vcData
				};
				Rms.saveRMS("NRdataVersion", data);
				LoginScr.isUpdateData = false;
				bool flag62 = GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem;
				if (flag62)
				{
					Res.outz(string.Concat(new object[]
					{
						GameScr.vsData,
						",",
						GameScr.vsMap,
						",",
						GameScr.vsSkill,
						",",
						GameScr.vsItem
					}));
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
					return;
				}
				break;
			}
			case -86:
			{
				sbyte b11 = msg.reader().readByte();
				Res.outz("server gui ve giao dich action = " + b11);
				bool flag63 = b11 == 0;
				if (flag63)
				{
					int playerID = msg.reader().readInt();
					GameScr.gI().giaodich(playerID);
				}
				bool flag64 = b11 == 1;
				if (flag64)
				{
					int num33 = msg.reader().readInt();
					global::Char char4 = GameScr.findCharInMap(num33);
					bool flag65 = char4 == null;
					if (flag65)
					{
						return;
					}
					GameCanvas.panel.setTypeGiaoDich(char4);
					GameCanvas.panel.show();
					Service.gI().getPlayerMenu(num33);
				}
				bool flag66 = b11 == 2;
				if (flag66)
				{
					sbyte b12 = msg.reader().readByte();
					int num5;
					for (int num34 = 0; num34 < GameCanvas.panel.vMyGD.size(); num34 = num5 + 1)
					{
						Item item = (Item)GameCanvas.panel.vMyGD.elementAt(num34);
						bool flag67 = item.indexUI == (int)b12;
						if (flag67)
						{
							GameCanvas.panel.vMyGD.removeElement(item);
							break;
						}
						num5 = num34;
					}
				}
				bool flag68 = b11 == 5;
				if (flag68)
				{
				}
				bool flag69 = b11 == 6;
				if (flag69)
				{
					GameCanvas.panel.isFriendLock = true;
					bool flag70 = GameCanvas.panel2 != null;
					if (flag70)
					{
						GameCanvas.panel2.isFriendLock = true;
					}
					GameCanvas.panel.vFriendGD.removeAllElements();
					bool flag71 = GameCanvas.panel2 != null;
					if (flag71)
					{
						GameCanvas.panel2.vFriendGD.removeAllElements();
					}
					int friendMoneyGD = msg.reader().readInt();
					sbyte b13 = msg.reader().readByte();
					Res.outz("item size = " + b13);
					int num5;
					for (int num35 = 0; num35 < (int)b13; num35 = num5 + 1)
					{
						Item item2 = new Item();
						item2.template = ItemTemplates.get(msg.reader().readShort());
						item2.quantity = msg.reader().readInt();
						int num36 = (int)msg.reader().readUnsignedByte();
						bool flag72 = num36 != 0;
						if (flag72)
						{
							item2.itemOption = new ItemOption[num36];
							for (int num37 = 0; num37 < item2.itemOption.Length; num37 = num5 + 1)
							{
								int num38 = (int)msg.reader().readUnsignedByte();
								int param2 = (int)msg.reader().readUnsignedShort();
								bool flag73 = num38 != -1;
								if (flag73)
								{
									item2.itemOption[num37] = new ItemOption(num38, param2);
									item2.compare = GameCanvas.panel.getCompare(item2);
								}
								num5 = num37;
							}
						}
						bool flag74 = GameCanvas.panel2 != null;
						if (flag74)
						{
							GameCanvas.panel2.vFriendGD.addElement(item2);
						}
						else
						{
							GameCanvas.panel.vFriendGD.addElement(item2);
						}
						num5 = num35;
					}
					bool flag75 = GameCanvas.panel2 != null;
					if (flag75)
					{
						GameCanvas.panel2.setTabGiaoDich(false);
						GameCanvas.panel2.friendMoneyGD = friendMoneyGD;
					}
					else
					{
						GameCanvas.panel.friendMoneyGD = friendMoneyGD;
						bool flag76 = GameCanvas.panel.currentTabIndex == 2;
						if (flag76)
						{
							GameCanvas.panel.setTabGiaoDich(false);
						}
					}
				}
				bool flag77 = b11 == 7;
				if (flag77)
				{
					InfoDlg.hide();
					bool isShow = GameCanvas.panel.isShow;
					if (isShow)
					{
						GameCanvas.panel.hide();
					}
				}
				break;
			}
			case -85:
			{
				Res.outz("CAP CHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
				sbyte b14 = msg.reader().readByte();
				bool flag78 = b14 == 0;
				if (flag78)
				{
					int num39 = (int)msg.reader().readUnsignedShort();
					Res.outz("lent =" + num39);
					sbyte[] imageData = new sbyte[num39];
					msg.reader().read(ref imageData, 0, num39);
					GameScr.imgCapcha = Image.createImage(imageData, 0, num39);
					GameScr.gI().keyInput = "-----";
					GameScr.gI().strCapcha = msg.reader().readUTF();
					GameScr.gI().keyCapcha = new int[GameScr.gI().strCapcha.Length];
					GameScr.gI().mobCapcha = new Mob();
					GameScr.gI().right = null;
				}
				bool flag79 = b14 == 1;
				if (flag79)
				{
					MobCapcha.isAttack = true;
				}
				bool flag80 = b14 == 2;
				if (flag80)
				{
					MobCapcha.explode = true;
					GameScr.gI().right = GameScr.gI().cmdFocus;
				}
				break;
			}
			case -84:
			{
				int index = (int)msg.reader().readUnsignedByte();
				Mob mob4 = null;
				try
				{
					mob4 = (Mob)GameScr.vMob.elementAt(index);
				}
				catch (Exception)
				{
				}
				bool flag81 = mob4 != null;
				if (flag81)
				{
					mob4.maxHp = msg.reader().readInt();
				}
				break;
			}
			case -83:
			{
				sbyte b15 = msg.reader().readByte();
				bool flag82 = b15 == 0;
				if (flag82)
				{
					int num40 = (int)msg.reader().readShort();
					int bgRID = (int)msg.reader().readShort();
					int num41 = (int)msg.reader().readUnsignedByte();
					int num42 = msg.reader().readInt();
					string text = msg.reader().readUTF();
					int num43 = (int)msg.reader().readShort();
					int num44 = (int)msg.reader().readShort();
					sbyte b16 = msg.reader().readByte();
					bool flag83 = b16 == 1;
					if (flag83)
					{
						GameScr.gI().isRongNamek = true;
					}
					else
					{
						GameScr.gI().isRongNamek = false;
					}
					GameScr.gI().xR = num43;
					GameScr.gI().yR = num44;
					Res.outz(string.Concat(new object[]
					{
						"xR= ",
						num43,
						" yR= ",
						num44,
						" +++++++++++++++++++++++++++++++++++++++"
					}));
					bool flag84 = global::Char.myCharz().charID == num42;
					if (flag84)
					{
						GameCanvas.panel.hideNow();
						GameScr.gI().activeRongThanEff(true);
					}
					else
					{
						bool flag85 = TileMap.mapID == num40 && TileMap.zoneID == num41;
						if (flag85)
						{
							GameScr.gI().activeRongThanEff(false);
						}
						else
						{
							bool flag86 = mGraphics.zoomLevel > 1;
							if (flag86)
							{
								GameScr.gI().doiMauTroi();
							}
						}
					}
					GameScr.gI().mapRID = num40;
					GameScr.gI().bgRID = bgRID;
					GameScr.gI().zoneRID = num41;
				}
				bool flag87 = b15 == 1;
				if (flag87)
				{
					Res.outz(string.Concat(new object[]
					{
						"map RID = ",
						GameScr.gI().mapRID,
						" zone RID= ",
						GameScr.gI().zoneRID
					}));
					Res.outz(string.Concat(new object[]
					{
						"map ID = ",
						TileMap.mapID,
						" zone ID= ",
						TileMap.zoneID
					}));
					bool flag88 = TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID;
					if (flag88)
					{
						GameScr.gI().hideRongThanEff();
					}
					else
					{
						GameScr.gI().isRongThanXuatHien = false;
						bool isRongNamek = GameScr.gI().isRongNamek;
						if (isRongNamek)
						{
							GameScr.gI().isRongNamek = false;
						}
					}
				}
				bool flag89 = b15 != 2;
				if (flag89)
				{
				}
				break;
			}
			case -82:
			{
				sbyte b17 = msg.reader().readByte();
				TileMap.tileIndex = new int[(int)b17][][];
				TileMap.tileType = new int[(int)b17][];
				int num5;
				for (int num45 = 0; num45 < (int)b17; num45 = num5 + 1)
				{
					sbyte b18 = msg.reader().readByte();
					TileMap.tileType[num45] = new int[(int)b18];
					TileMap.tileIndex[num45] = new int[(int)b18][];
					for (int num46 = 0; num46 < (int)b18; num46 = num5 + 1)
					{
						TileMap.tileType[num45][num46] = msg.reader().readInt();
						sbyte b19 = msg.reader().readByte();
						TileMap.tileIndex[num45][num46] = new int[(int)b19];
						for (int num47 = 0; num47 < (int)b19; num47 = num5 + 1)
						{
							TileMap.tileIndex[num45][num46][num47] = (int)msg.reader().readByte();
							num5 = num47;
						}
						num5 = num46;
					}
					num5 = num45;
				}
				break;
			}
			case -81:
			{
				sbyte b20 = msg.reader().readByte();
				bool flag90 = b20 == 0;
				if (flag90)
				{
					string src = msg.reader().readUTF();
					string src2 = msg.reader().readUTF();
					GameCanvas.panel.setTypeCombine();
					GameCanvas.panel.combineInfo = mFont.tahoma_7b_blue.splitFontArray(src, Panel.WIDTH_PANEL);
					GameCanvas.panel.combineTopInfo = mFont.tahoma_7.splitFontArray(src2, Panel.WIDTH_PANEL);
					GameCanvas.panel.show();
				}
				bool flag91 = b20 == 1;
				int num5;
				if (flag91)
				{
					GameCanvas.panel.vItemCombine.removeAllElements();
					sbyte b21 = msg.reader().readByte();
					for (int num48 = 0; num48 < (int)b21; num48 = num5 + 1)
					{
						sbyte b22 = msg.reader().readByte();
						for (int num49 = 0; num49 < global::Char.myCharz().arrItemBag.Length; num49 = num5 + 1)
						{
							Item item3 = global::Char.myCharz().arrItemBag[num49];
							bool flag92 = item3 != null && item3.indexUI == (int)b22;
							if (flag92)
							{
								item3.isSelect = true;
								GameCanvas.panel.vItemCombine.addElement(item3);
							}
							num5 = num49;
						}
						num5 = num48;
					}
					bool isShow2 = GameCanvas.panel.isShow;
					if (isShow2)
					{
						GameCanvas.panel.setTabCombine();
					}
				}
				bool flag93 = b20 == 2;
				if (flag93)
				{
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(0);
				}
				bool flag94 = b20 == 3;
				if (flag94)
				{
					GameCanvas.panel.combineSuccess = 1;
					GameCanvas.panel.setCombineEff(0);
				}
				bool flag95 = b20 == 4;
				if (flag95)
				{
					short iconID = msg.reader().readShort();
					GameCanvas.panel.iconID3 = iconID;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(1);
				}
				bool flag96 = b20 == 5;
				if (flag96)
				{
					short iconID2 = msg.reader().readShort();
					GameCanvas.panel.iconID3 = iconID2;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(2);
				}
				bool flag97 = b20 == 6;
				if (flag97)
				{
					short iconID3 = msg.reader().readShort();
					short iconID4 = msg.reader().readShort();
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(3);
					GameCanvas.panel.iconID1 = iconID3;
					GameCanvas.panel.iconID3 = iconID4;
				}
				bool flag98 = b20 == 7;
				if (flag98)
				{
					short iconID5 = msg.reader().readShort();
					GameCanvas.panel.iconID3 = iconID5;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(4);
				}
				bool flag99 = b20 == 8;
				if (flag99)
				{
					GameCanvas.panel.iconID3 = -1;
					GameCanvas.panel.combineSuccess = 1;
					GameCanvas.panel.setCombineEff(4);
				}
				short num50 = 21;
				try
				{
					num50 = msg.reader().readShort();
				}
				catch (Exception)
				{
				}
				for (int num51 = 0; num51 < GameScr.vNpc.size(); num51 = num5 + 1)
				{
					Npc npc = (Npc)GameScr.vNpc.elementAt(num51);
					bool flag100 = npc.template.npcTemplateId == (int)num50;
					if (flag100)
					{
						GameCanvas.panel.xS = npc.cx - GameScr.cmx;
						GameCanvas.panel.yS = npc.cy - GameScr.cmy;
						GameCanvas.panel.idNPC = (int)num50;
						break;
					}
					num5 = num51;
				}
				break;
			}
			case -80:
			{
				sbyte b23 = msg.reader().readByte();
				InfoDlg.hide();
				bool flag101 = b23 == 0;
				if (flag101)
				{
					GameCanvas.panel.vFriend.removeAllElements();
					int num52 = (int)msg.reader().readUnsignedByte();
					int num5;
					for (int num53 = 0; num53 < num52; num53 = num5 + 1)
					{
						global::Char char5 = new global::Char();
						char5.charID = msg.reader().readInt();
						char5.head = (int)msg.reader().readShort();
						char5.headICON = (int)msg.reader().readShort();
						char5.body = (int)msg.reader().readShort();
						char5.leg = (int)msg.reader().readShort();
						char5.bag = (int)msg.reader().readUnsignedByte();
						char5.cName = msg.reader().readUTF();
						bool isOnline2 = msg.reader().readBoolean();
						InfoItem infoItem2 = new InfoItem(mResources.power + ": " + msg.reader().readUTF());
						infoItem2.charInfo = char5;
						infoItem2.isOnline = isOnline2;
						GameCanvas.panel.vFriend.addElement(infoItem2);
						num5 = num53;
					}
					GameCanvas.panel.setTypeFriend();
					GameCanvas.panel.show();
				}
				bool flag102 = b23 == 3;
				if (flag102)
				{
					MyVector vFriend = GameCanvas.panel.vFriend;
					int num54 = msg.reader().readInt();
					Res.outz("online offline id=" + num54);
					int num5;
					for (int num55 = 0; num55 < vFriend.size(); num55 = num5 + 1)
					{
						InfoItem infoItem3 = (InfoItem)vFriend.elementAt(num55);
						bool flag103 = infoItem3.charInfo != null && infoItem3.charInfo.charID == num54;
						if (flag103)
						{
							Res.outz("online= " + infoItem3.isOnline.ToString());
							infoItem3.isOnline = msg.reader().readBoolean();
							break;
						}
						num5 = num55;
					}
				}
				bool flag104 = b23 != 2;
				if (!flag104)
				{
					MyVector vFriend2 = GameCanvas.panel.vFriend;
					int num56 = msg.reader().readInt();
					int num5;
					for (int num57 = 0; num57 < vFriend2.size(); num57 = num5 + 1)
					{
						InfoItem infoItem4 = (InfoItem)vFriend2.elementAt(num57);
						bool flag105 = infoItem4.charInfo != null && infoItem4.charInfo.charID == num56;
						if (flag105)
						{
							vFriend2.removeElement(infoItem4);
							break;
						}
						num5 = num57;
					}
					bool isShow3 = GameCanvas.panel.isShow;
					if (isShow3)
					{
						GameCanvas.panel.setTabFriend();
					}
				}
				break;
			}
			case -79:
			{
				InfoDlg.hide();
				int num58 = msg.reader().readInt();
				global::Char charMenu = GameCanvas.panel.charMenu;
				bool flag106 = charMenu == null;
				if (flag106)
				{
					return;
				}
				charMenu.cPower = msg.reader().readLong();
				charMenu.currStrLevel = msg.reader().readUTF();
				break;
			}
			case -77:
			{
				short num59 = msg.reader().readShort();
				SmallImage.newSmallVersion = new sbyte[(int)num59];
				SmallImage.maxSmall = num59;
				SmallImage.imgNew = new Small[(int)num59];
				int num5;
				for (int num60 = 0; num60 < (int)num59; num60 = num5 + 1)
				{
					SmallImage.newSmallVersion[num60] = msg.reader().readByte();
					num5 = num60;
				}
				break;
			}
			case -76:
			{
				sbyte b24 = msg.reader().readByte();
				bool flag107 = b24 == 0;
				if (flag107)
				{
					sbyte b25 = msg.reader().readByte();
					bool flag108 = b25 <= 0;
					if (flag108)
					{
						return;
					}
					global::Char.myCharz().arrArchive = new Archivement[(int)b25];
					int num5;
					for (int num61 = 0; num61 < (int)b25; num61 = num5 + 1)
					{
						global::Char.myCharz().arrArchive[num61] = new Archivement();
						global::Char.myCharz().arrArchive[num61].info1 = num61 + 1 + ". " + msg.reader().readUTF();
						global::Char.myCharz().arrArchive[num61].info2 = msg.reader().readUTF();
						global::Char.myCharz().arrArchive[num61].money = (int)msg.reader().readShort();
						global::Char.myCharz().arrArchive[num61].isFinish = msg.reader().readBoolean();
						global::Char.myCharz().arrArchive[num61].isRecieve = msg.reader().readBoolean();
						num5 = num61;
					}
					GameCanvas.panel.setTypeArchivement();
					GameCanvas.panel.show();
				}
				else
				{
					bool flag109 = b24 == 1;
					if (flag109)
					{
						int num62 = (int)msg.reader().readUnsignedByte();
						bool flag110 = global::Char.myCharz().arrArchive[num62] != null;
						if (flag110)
						{
							global::Char.myCharz().arrArchive[num62].isRecieve = true;
						}
					}
				}
				break;
			}
			case -74:
			{
				bool stopDownload = ServerListScreen.stopDownload;
				if (stopDownload)
				{
					return;
				}
				bool flag111 = !GameCanvas.isGetResourceFromServer();
				if (flag111)
				{
					Service.gI().getResource(3, null);
					SmallImage.loadBigRMS();
					SplashScr.imgLogo = null;
					bool flag112 = Rms.loadRMSString("acc") != null || Rms.loadRMSString("userAo" + ServerListScreen.ipSelect) != null;
					if (flag112)
					{
						LoginScr.isContinueToLogin = true;
					}
					GameCanvas.loginScr = new LoginScr();
					GameCanvas.loginScr.switchToMe();
					return;
				}
				bool flag113 = true;
				sbyte b26 = msg.reader().readByte();
				Res.outz("action = " + b26);
				bool flag114 = b26 == 0;
				if (flag114)
				{
					int num63 = msg.reader().readInt();
					string text2 = Rms.loadRMSString("ResVersion");
					int num64 = (text2 == null || !(text2 != string.Empty)) ? -1 : int.Parse(text2);
					bool flag115 = num64 == -1 || num64 != num63;
					if (flag115)
					{
						ServerListScreen.loadScreen = false;
						GameCanvas.serverScreen.show2();
					}
					else
					{
						Res.outz("login ngay");
						SmallImage.loadBigRMS();
						SplashScr.imgLogo = null;
						ServerListScreen.loadScreen = true;
						bool flag116 = GameCanvas.currentScreen != GameCanvas.loginScr;
						if (flag116)
						{
							GameCanvas.serverScreen.switchToMe();
						}
					}
				}
				bool flag117 = b26 == 1;
				if (flag117)
				{
					ServerListScreen.strWait = mResources.downloading_data;
					short num65 = (short)(ServerListScreen.nBig = (int)msg.reader().readShort());
					Service.gI().getResource(2, null);
				}
				bool flag118 = b26 == 2;
				if (flag118)
				{
					try
					{
						Controller.isLoadingData = true;
						GameCanvas.endDlg();
						int num5 = ServerListScreen.demPercent;
						ServerListScreen.demPercent = num5 + 1;
						ServerListScreen.percent = ServerListScreen.demPercent * 100 / ServerListScreen.nBig;
						string original = msg.reader().readUTF();
						string[] array2 = Res.split(original, "/", 0);
						string filename = "x" + mGraphics.zoomLevel + array2[array2.Length - 1];
						int num66 = msg.reader().readInt();
						sbyte[] data2 = new sbyte[num66];
						msg.reader().read(ref data2, 0, num66);
						Rms.saveRMS(filename, data2);
					}
					catch (Exception)
					{
						GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
					}
				}
				bool flag119 = b26 == 3 && flag113;
				if (flag119)
				{
					Controller.isLoadingData = false;
					int num67 = msg.reader().readInt();
					Res.outz("last version= " + num67);
					Rms.saveRMSString("ResVersion", num67 + string.Empty);
					Service.gI().getResource(3, null);
					GameCanvas.endDlg();
					SplashScr.imgLogo = null;
					SmallImage.loadBigRMS();
					mSystem.gcc();
					ServerListScreen.bigOk = true;
					ServerListScreen.loadScreen = true;
					GameScr.gI().loadGameScr();
					bool flag120 = GameCanvas.currentScreen != GameCanvas.loginScr;
					if (flag120)
					{
						GameCanvas.serverScreen.switchToMe();
					}
				}
				break;
			}
			case -70:
			{
				Res.outz("BIG MESSAGE .......................................");
				GameCanvas.endDlg();
				int avatar = (int)msg.reader().readShort();
				string chat = msg.reader().readUTF();
				ChatPopup.addBigMessage(chat, 100000, new Npc(-1, 0, 0, 0, 0, 0)
				{
					avatar = avatar
				});
				sbyte b27 = msg.reader().readByte();
				bool flag121 = b27 == 0;
				if (flag121)
				{
					ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
					ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 35;
					ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
				}
				bool flag122 = b27 == 1;
				if (flag122)
				{
					string p = msg.reader().readUTF();
					string caption = msg.reader().readUTF();
					ChatPopup.serverChatPopUp.cmdMsg1 = new Command(caption, ChatPopup.serverChatPopUp, 1000, p);
					ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 75;
					ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
					ChatPopup.serverChatPopUp.cmdMsg2 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
					ChatPopup.serverChatPopUp.cmdMsg2.x = GameCanvas.w / 2 + 11;
					ChatPopup.serverChatPopUp.cmdMsg2.y = GameCanvas.h - 35;
				}
				break;
			}
			case -69:
				global::Char.myCharz().cMaxStamina = msg.reader().readShort();
				break;
			case -68:
				global::Char.myCharz().cStamina = (int)msg.reader().readShort();
				break;
			case -67:
			{
				Res.outz("RECIEVE ICON");
				this.demCount += 1f;
				int num68 = msg.reader().readInt();
				sbyte[] array3 = null;
				try
				{
					array3 = NinjaUtil.readByteArray(msg);
					Res.outz("request hinh icon = " + num68);
					bool flag123 = num68 == 3896;
					if (flag123)
					{
						Res.outz("SIZE CHECK= " + array3.Length);
					}
					SmallImage.imgNew[num68].img = this.createImage(array3);
				}
				catch (Exception)
				{
					array3 = null;
					SmallImage.imgNew[num68].img = Image.createRGBImage(new int[1], 1, 1, true);
				}
				bool flag124 = array3 != null && mGraphics.zoomLevel > 1;
				if (flag124)
				{
					Rms.saveRMS(mGraphics.zoomLevel + "Small" + num68, array3);
				}
				break;
			}
			case -66:
			{
				short id = msg.reader().readShort();
				sbyte[] data3 = NinjaUtil.readByteArray(msg);
				EffectData effDataById = Effect.getEffDataById((int)id);
				sbyte b28 = msg.reader().readSByte();
				bool flag125 = b28 == 0;
				if (flag125)
				{
					effDataById.readData(data3);
				}
				else
				{
					effDataById.readDataNewBoss(data3, b28);
				}
				sbyte[] array4 = NinjaUtil.readByteArray(msg);
				effDataById.img = Image.createImage(array4, 0, array4.Length);
				break;
			}
			case -65:
			{
				Res.outz("TELEPORT ...................................................");
				InfoDlg.hide();
				int num69 = msg.reader().readInt();
				sbyte b29 = msg.reader().readByte();
				bool flag126 = b29 == 0;
				if (!flag126)
				{
					bool flag127 = global::Char.myCharz().charID == num69;
					if (flag127)
					{
						Controller.isStopReadMessage = true;
						GameScr.lockTick = 500;
						GameScr.gI().center = null;
						bool flag128 = b29 == 0 || b29 == 1 || b29 == 3;
						if (flag128)
						{
							Teleport p2 = new Teleport(global::Char.myCharz().cx, global::Char.myCharz().cy, global::Char.myCharz().head, global::Char.myCharz().cdir, 0, true, (b29 != 1) ? ((int)b29) : global::Char.myCharz().cgender);
							Teleport.addTeleport(p2);
						}
						bool flag129 = b29 == 2;
						if (flag129)
						{
							GameScr.lockTick = 50;
							global::Char.myCharz().hide();
						}
					}
					else
					{
						global::Char char6 = GameScr.findCharInMap(num69);
						bool flag130 = (b29 == 0 || b29 == 1 || b29 == 3) && char6 != null;
						if (flag130)
						{
							char6.isUsePlane = true;
							Teleport.addTeleport(new Teleport(char6.cx, char6.cy, char6.head, char6.cdir, 0, false, (b29 != 1) ? ((int)b29) : char6.cgender)
							{
								id = num69
							});
						}
						bool flag131 = b29 == 2;
						if (flag131)
						{
							char6.hide();
						}
					}
				}
				break;
			}
			case -64:
			{
				int num70 = msg.reader().readInt();
				int bag = (int)msg.reader().readUnsignedByte();
				bool flag132 = num70 == global::Char.myCharz().charID;
				if (flag132)
				{
					global::Char.myCharz().bag = bag;
				}
				else
				{
					bool flag133 = GameScr.findCharInMap(num70) != null;
					if (flag133)
					{
						GameScr.findCharInMap(num70).bag = bag;
					}
				}
				break;
			}
			case -63:
			{
				Res.outz("GET BAG");
				int num71 = (int)msg.reader().readUnsignedByte();
				sbyte b30 = msg.reader().readByte();
				ClanImage clanImage = new ClanImage();
				clanImage.ID = num71;
				bool flag134 = b30 > 0;
				if (flag134)
				{
					clanImage.idImage = new short[(int)b30];
					int num5;
					for (int num72 = 0; num72 < (int)b30; num72 = num5 + 1)
					{
						clanImage.idImage[num72] = msg.reader().readShort();
						Res.outz(string.Concat(new object[]
						{
							"ID=  ",
							num71,
							" frame= ",
							clanImage.idImage[num72]
						}));
						num5 = num72;
					}
					ClanImage.idImages.put(num71 + string.Empty, clanImage);
				}
				break;
			}
			case -62:
			{
				int num73 = (int)msg.reader().readUnsignedByte();
				sbyte b31 = msg.reader().readByte();
				bool flag135 = b31 <= 0;
				if (!flag135)
				{
					ClanImage clanImage2 = ClanImage.getClanImage((sbyte)num73);
					bool flag136 = clanImage2 == null;
					if (!flag136)
					{
						clanImage2.idImage = new short[(int)b31];
						int num5;
						for (int num74 = 0; num74 < (int)b31; num74 = num5 + 1)
						{
							clanImage2.idImage[num74] = msg.reader().readShort();
							bool flag137 = clanImage2.idImage[num74] > 0;
							if (flag137)
							{
								SmallImage.vKeys.addElement(clanImage2.idImage[num74] + string.Empty);
							}
							num5 = num74;
						}
					}
				}
				break;
			}
			case -61:
			{
				int num75 = msg.reader().readInt();
				bool flag138 = num75 != global::Char.myCharz().charID;
				if (flag138)
				{
					bool flag139 = GameScr.findCharInMap(num75) != null;
					if (flag139)
					{
						GameScr.findCharInMap(num75).clanID = msg.reader().readInt();
						bool flag140 = GameScr.findCharInMap(num75).clanID == -2;
						if (flag140)
						{
							GameScr.findCharInMap(num75).isCopy = true;
						}
					}
				}
				else
				{
					bool flag141 = global::Char.myCharz().clan != null;
					if (flag141)
					{
						global::Char.myCharz().clan.ID = msg.reader().readInt();
					}
				}
				break;
			}
			case -60:
			{
				GameCanvas.debug("SA7666", 2);
				int num76 = msg.reader().readInt();
				int num77 = -1;
				bool flag142 = num76 != global::Char.myCharz().charID;
				if (flag142)
				{
					global::Char char7 = GameScr.findCharInMap(num76);
					bool flag143 = char7 == null;
					if (flag143)
					{
						return;
					}
					bool flag144 = char7.currentMovePoint != null;
					if (flag144)
					{
						char7.createShadow(char7.cx, char7.cy, 10);
						char7.cx = char7.currentMovePoint.xEnd;
						char7.cy = char7.currentMovePoint.yEnd;
					}
					int num78 = (int)msg.reader().readUnsignedByte();
					Res.outz("player skill ID= " + num78);
					bool flag145 = (TileMap.tileTypeAtPixel(char7.cx, char7.cy) & 2) == 2;
					if (flag145)
					{
						char7.setSkillPaint(GameScr.sks[num78], 0);
					}
					else
					{
						char7.setSkillPaint(GameScr.sks[num78], 1);
					}
					sbyte b32 = msg.reader().readByte();
					Res.outz("nAttack = " + b32);
					global::Char[] array5 = new global::Char[(int)b32];
					int num5;
					for (i = 0; i < array5.Length; i = num5 + 1)
					{
						num77 = msg.reader().readInt();
						bool flag146 = num77 == global::Char.myCharz().charID;
						global::Char char8;
						if (flag146)
						{
							char8 = global::Char.myCharz();
							bool flag147 = !GameScr.isChangeZone && GameScr.isAutoPlay && GameScr.canAutoPlay;
							if (flag147)
							{
								Service.gI().requestChangeZone(-1, -1);
								GameScr.isChangeZone = true;
							}
						}
						else
						{
							char8 = GameScr.findCharInMap(num77);
						}
						array5[i] = char8;
						bool flag148 = i == 0;
						if (flag148)
						{
							bool flag149 = char7.cx <= char8.cx;
							if (flag149)
							{
								char7.cdir = 1;
							}
							else
							{
								char7.cdir = -1;
							}
						}
						num5 = i;
					}
					bool flag150 = i > 0;
					if (flag150)
					{
						char7.attChars = new global::Char[i];
						for (i = 0; i < char7.attChars.Length; i = num5 + 1)
						{
							char7.attChars[i] = array5[i];
							num5 = i;
						}
						char7.mobFocus = null;
						char7.charFocus = char7.attChars[0];
					}
				}
				else
				{
					sbyte b33 = msg.reader().readByte();
					sbyte b34 = msg.reader().readByte();
					num77 = msg.reader().readInt();
				}
				try
				{
					sbyte b35 = msg.reader().readByte();
					Res.outz("isRead continue = " + b35);
					bool flag151 = b35 != 1;
					if (!flag151)
					{
						sbyte b36 = msg.reader().readByte();
						Res.outz("type skill = " + b36);
						bool flag152 = num77 == global::Char.myCharz().charID;
						if (flag152)
						{
							@char = global::Char.myCharz();
							int num79 = msg.readInt3Byte();
							Res.outz("dame hit = " + num79);
							@char.isDie = msg.reader().readBoolean();
							bool isDie = @char.isDie;
							if (isDie)
							{
								global::Char.isLockKey = true;
							}
							Res.outz("isDie=" + @char.isDie.ToString() + "---------------------------------------");
							int num80 = 0;
							bool isCrit = @char.isCrit = msg.reader().readBoolean();
							@char.isMob = false;
							num79 = (@char.damHP = num79 + num80);
							bool flag153 = b36 == 0;
							if (flag153)
							{
								@char.doInjure(num79, 0, isCrit, false);
							}
						}
						else
						{
							@char = GameScr.findCharInMap(num77);
							bool flag154 = @char == null;
							if (flag154)
							{
								return;
							}
							int num81 = msg.readInt3Byte();
							Res.outz("dame hit= " + num81);
							@char.isDie = msg.reader().readBoolean();
							Res.outz("isDie=" + @char.isDie.ToString() + "---------------------------------------");
							int num82 = 0;
							bool isCrit2 = @char.isCrit = msg.reader().readBoolean();
							@char.isMob = false;
							num81 = (@char.damHP = num81 + num82);
							bool flag155 = b36 == 0;
							if (flag155)
							{
								@char.doInjure(num81, 0, isCrit2, false);
							}
						}
					}
				}
				catch (Exception)
				{
				}
				break;
			}
			case -59:
			{
				sbyte typePK = msg.reader().readByte();
				GameScr.gI().player_vs_player(msg.reader().readInt(), msg.reader().readInt(), msg.reader().readUTF(), typePK);
				break;
			}
			case -57:
			{
				string strInvite = msg.reader().readUTF();
				int clanID = msg.reader().readInt();
				int code = msg.reader().readInt();
				GameScr.gI().clanInvite(strInvite, clanID, code);
				break;
			}
			case -53:
			{
				Res.outz("MY CLAN INFO");
				InfoDlg.hide();
				bool flag156 = false;
				int num83 = msg.reader().readInt();
				Res.outz("clanId= " + num83);
				bool flag157 = num83 == -1;
				if (flag157)
				{
					global::Char.myCharz().clan = null;
					ClanMessage.vMessage.removeAllElements();
					bool flag158 = GameCanvas.panel.member != null;
					if (flag158)
					{
						GameCanvas.panel.member.removeAllElements();
					}
					bool flag159 = GameCanvas.panel.myMember != null;
					if (flag159)
					{
						GameCanvas.panel.myMember.removeAllElements();
					}
					bool flag160 = GameCanvas.currentScreen == GameScr.gI();
					if (flag160)
					{
						GameCanvas.panel.setTabClans();
					}
					return;
				}
				GameCanvas.panel.tabIcon = null;
				bool flag161 = global::Char.myCharz().clan == null;
				if (flag161)
				{
					global::Char.myCharz().clan = new Clan();
				}
				global::Char.myCharz().clan.ID = num83;
				global::Char.myCharz().clan.name = msg.reader().readUTF();
				global::Char.myCharz().clan.slogan = msg.reader().readUTF();
				global::Char.myCharz().clan.imgID = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().clan.powerPoint = msg.reader().readUTF();
				global::Char.myCharz().clan.leaderName = msg.reader().readUTF();
				global::Char.myCharz().clan.currMember = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().clan.maxMember = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().role = msg.reader().readByte();
				global::Char.myCharz().clan.clanPoint = msg.reader().readInt();
				global::Char.myCharz().clan.level = (int)msg.reader().readByte();
				GameCanvas.panel.myMember = new MyVector();
				int num5;
				for (int num84 = 0; num84 < global::Char.myCharz().clan.currMember; num84 = num5 + 1)
				{
					Member member = new Member();
					member.ID = msg.reader().readInt();
					member.head = msg.reader().readShort();
					member.headICON = msg.reader().readShort();
					member.leg = msg.reader().readShort();
					member.body = msg.reader().readShort();
					member.name = msg.reader().readUTF();
					member.role = msg.reader().readByte();
					member.powerPoint = msg.reader().readUTF();
					member.donate = msg.reader().readInt();
					member.receive_donate = msg.reader().readInt();
					member.clanPoint = msg.reader().readInt();
					member.curClanPoint = msg.reader().readInt();
					member.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					GameCanvas.panel.myMember.addElement(member);
					num5 = num84;
				}
				int num85 = (int)msg.reader().readUnsignedByte();
				for (int num86 = 0; num86 < num85; num86 = num5 + 1)
				{
					this.readClanMsg(msg, -1);
					num5 = num86;
				}
				bool flag162 = GameCanvas.panel.isSearchClan || GameCanvas.panel.isViewMember || GameCanvas.panel.isMessage;
				if (flag162)
				{
					GameCanvas.panel.setTabClans();
				}
				bool flag163 = flag156;
				if (flag163)
				{
					GameCanvas.panel.setTabClans();
				}
				break;
			}
			case -52:
			{
				sbyte b37 = msg.reader().readByte();
				bool flag164 = b37 == 0;
				if (flag164)
				{
					Member member2 = new Member();
					member2.ID = msg.reader().readInt();
					member2.head = msg.reader().readShort();
					member2.headICON = msg.reader().readShort();
					member2.leg = msg.reader().readShort();
					member2.body = msg.reader().readShort();
					member2.name = msg.reader().readUTF();
					member2.role = msg.reader().readByte();
					member2.powerPoint = msg.reader().readUTF();
					member2.donate = msg.reader().readInt();
					member2.receive_donate = msg.reader().readInt();
					member2.clanPoint = msg.reader().readInt();
					member2.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					bool flag165 = GameCanvas.panel.myMember == null;
					if (flag165)
					{
						GameCanvas.panel.myMember = new MyVector();
					}
					GameCanvas.panel.myMember.addElement(member2);
					GameCanvas.panel.initTabClans();
				}
				bool flag166 = b37 == 1;
				if (flag166)
				{
					GameCanvas.panel.myMember.removeElementAt((int)msg.reader().readByte());
					Panel panel = GameCanvas.panel;
					Panel panel2 = panel;
					int num5 = panel.currentListLength;
					panel2.currentListLength = num5 - 1;
					GameCanvas.panel.initTabClans();
				}
				bool flag167 = b37 != 2;
				if (!flag167)
				{
					Member member3 = new Member();
					member3.ID = msg.reader().readInt();
					member3.head = msg.reader().readShort();
					member3.headICON = msg.reader().readShort();
					member3.leg = msg.reader().readShort();
					member3.body = msg.reader().readShort();
					member3.name = msg.reader().readUTF();
					member3.role = msg.reader().readByte();
					member3.powerPoint = msg.reader().readUTF();
					member3.donate = msg.reader().readInt();
					member3.receive_donate = msg.reader().readInt();
					member3.clanPoint = msg.reader().readInt();
					member3.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					int num5;
					for (int num87 = 0; num87 < GameCanvas.panel.myMember.size(); num87 = num5 + 1)
					{
						Member member4 = (Member)GameCanvas.panel.myMember.elementAt(num87);
						bool flag168 = member4.ID == member3.ID;
						if (flag168)
						{
							bool flag169 = global::Char.myCharz().charID == member3.ID;
							if (flag169)
							{
								global::Char.myCharz().role = member3.role;
							}
							Member o = member3;
							GameCanvas.panel.myMember.removeElement(member4);
							GameCanvas.panel.myMember.insertElementAt(o, num87);
							return;
						}
						num5 = num87;
					}
				}
				break;
			}
			case -51:
			{
				InfoDlg.hide();
				this.readClanMsg(msg, 0);
				bool flag170 = GameCanvas.panel.isMessage && GameCanvas.panel.type == 5;
				if (flag170)
				{
					GameCanvas.panel.initTabClans();
				}
				break;
			}
			case -50:
			{
				InfoDlg.hide();
				GameCanvas.panel.member = new MyVector();
				sbyte b38 = msg.reader().readByte();
				int num5;
				for (int num88 = 0; num88 < (int)b38; num88 = num5 + 1)
				{
					Member member5 = new Member();
					member5.ID = msg.reader().readInt();
					member5.head = msg.reader().readShort();
					member5.headICON = msg.reader().readShort();
					member5.leg = msg.reader().readShort();
					member5.body = msg.reader().readShort();
					member5.name = msg.reader().readUTF();
					member5.role = msg.reader().readByte();
					member5.powerPoint = msg.reader().readUTF();
					member5.donate = msg.reader().readInt();
					member5.receive_donate = msg.reader().readInt();
					member5.clanPoint = msg.reader().readInt();
					member5.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					GameCanvas.panel.member.addElement(member5);
					num5 = num88;
				}
				GameCanvas.panel.isViewMember = true;
				GameCanvas.panel.isSearchClan = false;
				GameCanvas.panel.isMessage = false;
				GameCanvas.panel.currentListLength = GameCanvas.panel.member.size() + 2;
				GameCanvas.panel.initTabClans();
				break;
			}
			case -47:
			{
				InfoDlg.hide();
				sbyte b39 = msg.reader().readByte();
				Res.outz("clan = " + b39);
				bool flag171 = b39 == 0;
				if (flag171)
				{
					GameCanvas.panel.clanReport = mResources.cannot_find_clan;
					GameCanvas.panel.clans = null;
				}
				else
				{
					GameCanvas.panel.clans = new Clan[(int)b39];
					Res.outz("clan search lent= " + GameCanvas.panel.clans.Length);
					int num5;
					for (int num89 = 0; num89 < GameCanvas.panel.clans.Length; num89 = num5 + 1)
					{
						GameCanvas.panel.clans[num89] = new Clan();
						GameCanvas.panel.clans[num89].ID = msg.reader().readInt();
						GameCanvas.panel.clans[num89].name = msg.reader().readUTF();
						GameCanvas.panel.clans[num89].slogan = msg.reader().readUTF();
						GameCanvas.panel.clans[num89].imgID = (int)msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num89].powerPoint = msg.reader().readUTF();
						GameCanvas.panel.clans[num89].leaderName = msg.reader().readUTF();
						GameCanvas.panel.clans[num89].currMember = (int)msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num89].maxMember = (int)msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num89].date = msg.reader().readInt();
						num5 = num89;
					}
				}
				GameCanvas.panel.isSearchClan = true;
				GameCanvas.panel.isViewMember = false;
				GameCanvas.panel.isMessage = false;
				bool isSearchClan = GameCanvas.panel.isSearchClan;
				if (isSearchClan)
				{
					GameCanvas.panel.initTabClans();
				}
				break;
			}
			case -46:
			{
				InfoDlg.hide();
				sbyte b40 = msg.reader().readByte();
				bool flag172 = b40 == 1 || b40 == 3;
				if (flag172)
				{
					GameCanvas.endDlg();
					ClanImage.vClanImage.removeAllElements();
					int num90 = (int)msg.reader().readUnsignedByte();
					int num5;
					for (int num91 = 0; num91 < num90; num91 = num5 + 1)
					{
						ClanImage clanImage3 = new ClanImage();
						clanImage3.ID = (int)msg.reader().readUnsignedByte();
						clanImage3.name = msg.reader().readUTF();
						clanImage3.xu = msg.reader().readInt();
						clanImage3.luong = msg.reader().readInt();
						bool flag173 = !ClanImage.isExistClanImage(clanImage3.ID);
						if (flag173)
						{
							ClanImage.addClanImage(clanImage3);
						}
						else
						{
							ClanImage.getClanImage((sbyte)clanImage3.ID).name = clanImage3.name;
							ClanImage.getClanImage((sbyte)clanImage3.ID).xu = clanImage3.xu;
							ClanImage.getClanImage((sbyte)clanImage3.ID).luong = clanImage3.luong;
						}
						num5 = num91;
					}
					bool flag174 = global::Char.myCharz().clan != null;
					if (flag174)
					{
						GameCanvas.panel.changeIcon();
					}
				}
				bool flag175 = b40 == 4;
				if (flag175)
				{
					global::Char.myCharz().clan.imgID = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().clan.slogan = msg.reader().readUTF();
				}
				break;
			}
			case -45:
			{
				sbyte b41 = msg.reader().readByte();
				int num92 = msg.reader().readInt();
				short num93 = msg.reader().readShort();
				Res.outz(string.Concat(new object[]
				{
					"skill type= ",
					b41,
					"   player use= ",
					num92
				}));
				bool flag176 = b41 == 0;
				if (flag176)
				{
					Res.outz("id use= " + num92);
					bool flag177 = global::Char.myCharz().charID != num92;
					if (flag177)
					{
						@char = GameScr.findCharInMap(num92);
						bool flag178 = (TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2;
						if (flag178)
						{
							@char.setSkillPaint(GameScr.sks[(int)num93], 0);
						}
						else
						{
							@char.setSkillPaint(GameScr.sks[(int)num93], 1);
							@char.delayFall = 20;
						}
					}
					else
					{
						global::Char.myCharz().saveLoadPreviousSkill();
						Res.outz("LOAD LAST SKILL");
					}
					sbyte b42 = msg.reader().readByte();
					Res.outz("npc size= " + b42);
					int num5;
					for (int num94 = 0; num94 < (int)b42; num94 = num5 + 1)
					{
						sbyte b43 = msg.reader().readByte();
						sbyte seconds = msg.reader().readByte();
						Res.outz("index= " + b43);
						bool flag179 = num93 >= 42 && num93 <= 48;
						if (flag179)
						{
							((Mob)GameScr.vMob.elementAt((int)b43)).isFreez = true;
							((Mob)GameScr.vMob.elementAt((int)b43)).seconds = (int)seconds;
							((Mob)GameScr.vMob.elementAt((int)b43)).last = (((Mob)GameScr.vMob.elementAt((int)b43)).cur = mSystem.currentTimeMillis());
						}
						num5 = num94;
					}
					sbyte b44 = msg.reader().readByte();
					for (int num95 = 0; num95 < (int)b44; num95 = num5 + 1)
					{
						int num96 = msg.reader().readInt();
						sbyte b45 = msg.reader().readByte();
						Res.outz(string.Concat(new object[]
						{
							"player ID= ",
							num96,
							" my ID= ",
							global::Char.myCharz().charID
						}));
						bool flag180 = num93 < 42 || num93 > 48;
						if (!flag180)
						{
							bool flag181 = num96 == global::Char.myCharz().charID;
							if (flag181)
							{
								bool flag182 = !global::Char.myCharz().isFlyAndCharge && !global::Char.myCharz().isStandAndCharge;
								if (flag182)
								{
									GameScr.gI().isFreez = true;
									global::Char.myCharz().isFreez = true;
									global::Char.myCharz().freezSeconds = (int)b45;
									global::Char.myCharz().lastFreez = (global::Char.myCharz().currFreez = mSystem.currentTimeMillis());
									global::Char.myCharz().isLockMove = true;
								}
							}
							else
							{
								@char = GameScr.findCharInMap(num96);
								bool flag183 = @char != null && !@char.isFlyAndCharge && !@char.isStandAndCharge;
								if (flag183)
								{
									@char.isFreez = true;
									@char.seconds = (int)b45;
									@char.freezSeconds = (int)b45;
									@char.lastFreez = (GameScr.findCharInMap(num96).currFreez = mSystem.currentTimeMillis());
								}
							}
						}
						num5 = num95;
					}
				}
				bool flag184 = b41 == 1 && num92 != global::Char.myCharz().charID;
				if (flag184)
				{
					GameScr.findCharInMap(num92).isCharge = true;
				}
				bool flag185 = b41 == 3;
				if (flag185)
				{
					bool flag186 = num92 == global::Char.myCharz().charID;
					if (flag186)
					{
						global::Char.myCharz().isCharge = false;
						SoundMn.gI().taitaoPause();
						global::Char.myCharz().saveLoadPreviousSkill();
					}
					else
					{
						GameScr.findCharInMap(num92).isCharge = false;
					}
				}
				bool flag187 = b41 == 4;
				if (flag187)
				{
					bool flag188 = num92 == global::Char.myCharz().charID;
					if (flag188)
					{
						global::Char.myCharz().seconds = (int)(msg.reader().readShort() - 1000);
						global::Char.myCharz().last = mSystem.currentTimeMillis();
						Res.outz(string.Concat(new object[]
						{
							"second= ",
							global::Char.myCharz().seconds,
							" last= ",
							global::Char.myCharz().last
						}));
					}
					else
					{
						bool flag189 = GameScr.findCharInMap(num92) != null;
						if (flag189)
						{
							int cgender = GameScr.findCharInMap(num92).cgender;
							if (cgender != 0)
							{
								if (cgender == 1)
								{
									GameScr.findCharInMap(num92).useChargeSkill(true);
								}
							}
							else
							{
								GameScr.findCharInMap(num92).useChargeSkill(false);
							}
							GameScr.findCharInMap(num92).skillTemplateId = (int)num93;
							GameScr.findCharInMap(num92).isUseSkillAfterCharge = true;
							GameScr.findCharInMap(num92).seconds = (int)msg.reader().readShort();
							GameScr.findCharInMap(num92).last = mSystem.currentTimeMillis();
						}
					}
				}
				bool flag190 = b41 == 5;
				if (flag190)
				{
					bool flag191 = num92 == global::Char.myCharz().charID;
					if (flag191)
					{
						global::Char.myCharz().stopUseChargeSkill();
					}
					else
					{
						bool flag192 = GameScr.findCharInMap(num92) != null;
						if (flag192)
						{
							GameScr.findCharInMap(num92).stopUseChargeSkill();
						}
					}
				}
				bool flag193 = b41 == 6;
				if (flag193)
				{
					bool flag194 = num92 == global::Char.myCharz().charID;
					if (flag194)
					{
						global::Char.myCharz().setAutoSkillPaint(GameScr.sks[(int)num93], 0);
					}
					else
					{
						bool flag195 = GameScr.findCharInMap(num92) != null;
						if (flag195)
						{
							GameScr.findCharInMap(num92).setAutoSkillPaint(GameScr.sks[(int)num93], 0);
							SoundMn.gI().gong();
						}
					}
				}
				bool flag196 = b41 == 7;
				if (flag196)
				{
					bool flag197 = num92 == global::Char.myCharz().charID;
					if (flag197)
					{
						global::Char.myCharz().seconds = (int)msg.reader().readShort();
						Res.outz("second = " + global::Char.myCharz().seconds);
						global::Char.myCharz().last = mSystem.currentTimeMillis();
					}
					else
					{
						bool flag198 = GameScr.findCharInMap(num92) != null;
						if (flag198)
						{
							GameScr.findCharInMap(num92).useChargeSkill(true);
							GameScr.findCharInMap(num92).seconds = (int)msg.reader().readShort();
							GameScr.findCharInMap(num92).last = mSystem.currentTimeMillis();
							SoundMn.gI().gong();
						}
					}
				}
				bool flag199 = b41 == 8 && num92 != global::Char.myCharz().charID && GameScr.findCharInMap(num92) != null;
				if (flag199)
				{
					GameScr.findCharInMap(num92).setAutoSkillPaint(GameScr.sks[(int)num93], 0);
				}
				break;
			}
			case -44:
			{
				bool flag200 = false;
				bool flag201 = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
				if (flag201)
				{
					flag200 = true;
				}
				sbyte b46 = msg.reader().readByte();
				int num97 = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().arrItemShop = new Item[num97][];
				GameCanvas.panel.shopTabName = new string[num97 + ((!flag200) ? 1 : 0)][];
				int num5;
				for (int num98 = 0; num98 < GameCanvas.panel.shopTabName.Length; num98 = num5 + 1)
				{
					GameCanvas.panel.shopTabName[num98] = new string[2];
					num5 = num98;
				}
				bool flag202 = b46 == 2;
				if (flag202)
				{
					GameCanvas.panel.maxPageShop = new int[num97];
					GameCanvas.panel.currPageShop = new int[num97];
				}
				bool flag203 = !flag200;
				if (flag203)
				{
					GameCanvas.panel.shopTabName[num97] = mResources.inventory;
				}
				for (int num99 = 0; num99 < num97; num99 = num5 + 1)
				{
					string[] array6 = Res.split(msg.reader().readUTF(), "\n", 0);
					bool flag204 = b46 == 2;
					if (flag204)
					{
						GameCanvas.panel.maxPageShop[num99] = (int)msg.reader().readUnsignedByte();
					}
					bool flag205 = array6.Length == 2;
					if (flag205)
					{
						GameCanvas.panel.shopTabName[num99] = array6;
					}
					bool flag206 = array6.Length == 1;
					if (flag206)
					{
						GameCanvas.panel.shopTabName[num99][0] = array6[0];
						GameCanvas.panel.shopTabName[num99][1] = string.Empty;
					}
					int num100 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemShop[num99] = new Item[num100];
					Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
					bool flag207 = b46 == 1;
					if (flag207)
					{
						Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy2;
					}
					for (int num101 = 0; num101 < num100; num101 = num5 + 1)
					{
						short num102 = msg.reader().readShort();
						bool flag208 = num102 == -1;
						if (!flag208)
						{
							global::Char.myCharz().arrItemShop[num99][num101] = new Item();
							global::Char.myCharz().arrItemShop[num99][num101].template = ItemTemplates.get(num102);
							Res.outz(string.Concat(new object[]
							{
								"name ",
								num99,
								" = ",
								global::Char.myCharz().arrItemShop[num99][num101].template.name,
								" id templat= ",
								global::Char.myCharz().arrItemShop[num99][num101].template.id
							}));
							bool flag209 = b46 == 8;
							if (flag209)
							{
								global::Char.myCharz().arrItemShop[num99][num101].buyCoin = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num99][num101].buyGold = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num99][num101].quantity = msg.reader().readInt();
							}
							else
							{
								bool flag210 = b46 == 4;
								if (flag210)
								{
									global::Char.myCharz().arrItemShop[num99][num101].reason = msg.reader().readUTF();
								}
								else
								{
									bool flag211 = b46 == 0;
									if (flag211)
									{
										global::Char.myCharz().arrItemShop[num99][num101].buyCoin = msg.reader().readInt();
										global::Char.myCharz().arrItemShop[num99][num101].buyGold = msg.reader().readInt();
									}
									else
									{
										bool flag212 = b46 == 1;
										if (flag212)
										{
											global::Char.myCharz().arrItemShop[num99][num101].powerRequire = msg.reader().readLong();
										}
										else
										{
											bool flag213 = b46 == 2;
											if (flag213)
											{
												global::Char.myCharz().arrItemShop[num99][num101].itemId = (int)msg.reader().readShort();
												global::Char.myCharz().arrItemShop[num99][num101].buyCoin = msg.reader().readInt();
												global::Char.myCharz().arrItemShop[num99][num101].buyGold = msg.reader().readInt();
												global::Char.myCharz().arrItemShop[num99][num101].buyType = msg.reader().readByte();
												global::Char.myCharz().arrItemShop[num99][num101].quantity = msg.reader().readInt();
												global::Char.myCharz().arrItemShop[num99][num101].isMe = msg.reader().readByte();
											}
											else
											{
												bool flag214 = b46 == 3;
												if (flag214)
												{
													global::Char.myCharz().arrItemShop[num99][num101].isBuySpec = true;
													global::Char.myCharz().arrItemShop[num99][num101].iconSpec = msg.reader().readShort();
													global::Char.myCharz().arrItemShop[num99][num101].buySpec = msg.reader().readInt();
												}
											}
										}
									}
								}
							}
							int num103 = (int)msg.reader().readUnsignedByte();
							bool flag215 = num103 != 0;
							if (flag215)
							{
								global::Char.myCharz().arrItemShop[num99][num101].itemOption = new ItemOption[num103];
								for (int num104 = 0; num104 < global::Char.myCharz().arrItemShop[num99][num101].itemOption.Length; num104 = num5 + 1)
								{
									int num105 = (int)msg.reader().readUnsignedByte();
									int param3 = (int)msg.reader().readUnsignedShort();
									bool flag216 = num105 != -1;
									if (flag216)
									{
										global::Char.myCharz().arrItemShop[num99][num101].itemOption[num104] = new ItemOption(num105, param3);
										global::Char.myCharz().arrItemShop[num99][num101].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemShop[num99][num101]);
									}
									num5 = num104;
								}
							}
							sbyte newItem = msg.reader().readByte();
							global::Char.myCharz().arrItemShop[num99][num101].newItem = (newItem != 0);
							sbyte b47 = msg.reader().readByte();
							bool flag217 = b47 == 1;
							if (flag217)
							{
								int headTemp = (int)msg.reader().readShort();
								int bodyTemp = (int)msg.reader().readShort();
								int legTemp = (int)msg.reader().readShort();
								int bagTemp = (int)msg.reader().readShort();
								global::Char.myCharz().arrItemShop[num99][num101].setPartTemp(headTemp, bodyTemp, legTemp, bagTemp);
							}
						}
						num5 = num101;
					}
					num5 = num99;
				}
				bool flag218 = flag200;
				if (flag218)
				{
					bool flag219 = b46 != 2;
					if (flag219)
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.tabName[7] = new string[][]
						{
							new string[]
							{
								string.Empty
							}
						};
						GameCanvas.panel2.setTypeBodyOnly();
						GameCanvas.panel2.show();
					}
					else
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.setTypeKiGuiOnly();
						GameCanvas.panel2.show();
					}
				}
				GameCanvas.panel.tabName[1] = GameCanvas.panel.shopTabName;
				bool flag220 = b46 == 2;
				if (flag220)
				{
					string[][] array7 = GameCanvas.panel.tabName[1];
					bool flag221 = flag200;
					if (flag221)
					{
						GameCanvas.panel.tabName[1] = new string[][]
						{
							array7[0],
							array7[1],
							array7[2],
							array7[3]
						};
					}
					else
					{
						GameCanvas.panel.tabName[1] = new string[][]
						{
							array7[0],
							array7[1],
							array7[2],
							array7[3],
							array7[4]
						};
					}
				}
				GameCanvas.panel.setTypeShop((int)b46);
				GameCanvas.panel.show();
				break;
			}
			case -43:
			{
				sbyte itemAction = msg.reader().readByte();
				sbyte where = msg.reader().readByte();
				sbyte index2 = msg.reader().readByte();
				string info2 = msg.reader().readUTF();
				GameCanvas.panel.itemRequest(itemAction, info2, where, index2);
				break;
			}
			case -42:
				global::Char.myCharz().cHPGoc = msg.readInt3Byte();
				global::Char.myCharz().cMPGoc = msg.readInt3Byte();
				global::Char.myCharz().cDamGoc = msg.reader().readInt();
				global::Char.myCharz().cHPFull = msg.readInt3Byte();
				global::Char.myCharz().cMPFull = msg.readInt3Byte();
				global::Char.myCharz().cHP = msg.readInt3Byte();
				global::Char.myCharz().cMP = msg.readInt3Byte();
				global::Char.myCharz().cspeed = (int)msg.reader().readByte();
				global::Char.myCharz().hpFrom1000TiemNang = msg.reader().readByte();
				global::Char.myCharz().mpFrom1000TiemNang = msg.reader().readByte();
				global::Char.myCharz().damFrom1000TiemNang = msg.reader().readByte();
				global::Char.myCharz().cDamFull = msg.reader().readInt();
				global::Char.myCharz().cDefull = msg.reader().readInt();
				global::Char.myCharz().cCriticalFull = (int)msg.reader().readByte();
				global::Char.myCharz().cTiemNang = msg.reader().readLong();
				global::Char.myCharz().expForOneAdd = msg.reader().readShort();
				global::Char.myCharz().cDefGoc = (int)msg.reader().readShort();
				global::Char.myCharz().cCriticalGoc = (int)msg.reader().readByte();
				InfoDlg.hide();
				break;
			case -41:
			{
				sbyte b48 = msg.reader().readByte();
				global::Char.myCharz().strLevel = new string[(int)b48];
				int num5;
				for (int num106 = 0; num106 < (int)b48; num106 = num5 + 1)
				{
					string text3 = msg.reader().readUTF();
					global::Char.myCharz().strLevel[num106] = text3;
					num5 = num106;
				}
				Res.outz("---   xong  level caption cmd : " + msg.command);
				break;
			}
			case -37:
			{
				sbyte b49 = msg.reader().readByte();
				Res.outz("cAction= " + b49);
				bool flag222 = b49 != 0;
				if (!flag222)
				{
					global::Char.myCharz().head = (int)msg.reader().readShort();
					global::Char.myCharz().setDefaultPart();
					int num107 = (int)msg.reader().readUnsignedByte();
					Res.outz("num body = " + num107);
					global::Char.myCharz().arrItemBody = new Item[num107];
					int num5;
					for (int num108 = 0; num108 < num107; num108 = num5 + 1)
					{
						short num109 = msg.reader().readShort();
						bool flag223 = num109 == -1;
						if (!flag223)
						{
							global::Char.myCharz().arrItemBody[num108] = new Item();
							global::Char.myCharz().arrItemBody[num108].template = ItemTemplates.get(num109);
							int type2 = (int)global::Char.myCharz().arrItemBody[num108].template.type;
							global::Char.myCharz().arrItemBody[num108].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBody[num108].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBody[num108].content = msg.reader().readUTF();
							int num110 = (int)msg.reader().readUnsignedByte();
							bool flag224 = num110 != 0;
							if (flag224)
							{
								global::Char.myCharz().arrItemBody[num108].itemOption = new ItemOption[num110];
								for (int num111 = 0; num111 < global::Char.myCharz().arrItemBody[num108].itemOption.Length; num111 = num5 + 1)
								{
									int num112 = (int)msg.reader().readUnsignedByte();
									int param4 = (int)msg.reader().readUnsignedShort();
									bool flag225 = num112 != -1;
									if (flag225)
									{
										global::Char.myCharz().arrItemBody[num108].itemOption[num111] = new ItemOption(num112, param4);
									}
									num5 = num111;
								}
							}
							int num113 = type2;
							if (num113 != 0)
							{
								if (num113 == 1)
								{
									global::Char.myCharz().leg = (int)global::Char.myCharz().arrItemBody[num108].template.part;
								}
							}
							else
							{
								global::Char.myCharz().body = (int)global::Char.myCharz().arrItemBody[num108].template.part;
							}
						}
						num5 = num108;
					}
				}
				break;
			}
			case -36:
			{
				sbyte b50 = msg.reader().readByte();
				Res.outz("cAction= " + b50);
				bool flag226 = b50 == 0;
				if (flag226)
				{
					int num114 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemBag = new Item[num114];
					GameScr.hpPotion = 0;
					Res.outz("numC=" + num114);
					int num5;
					for (int num115 = 0; num115 < num114; num115 = num5 + 1)
					{
						short num116 = msg.reader().readShort();
						bool flag227 = num116 == -1;
						if (!flag227)
						{
							global::Char.myCharz().arrItemBag[num115] = new Item();
							global::Char.myCharz().arrItemBag[num115].template = ItemTemplates.get(num116);
							global::Char.myCharz().arrItemBag[num115].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBag[num115].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBag[num115].content = msg.reader().readUTF();
							global::Char.myCharz().arrItemBag[num115].indexUI = num115;
							int num117 = (int)msg.reader().readUnsignedByte();
							bool flag228 = num117 != 0;
							if (flag228)
							{
								global::Char.myCharz().arrItemBag[num115].itemOption = new ItemOption[num117];
								for (int num118 = 0; num118 < global::Char.myCharz().arrItemBag[num115].itemOption.Length; num118 = num5 + 1)
								{
									int num119 = (int)msg.reader().readUnsignedByte();
									int param5 = (int)msg.reader().readUnsignedShort();
									bool flag229 = num119 != -1;
									if (flag229)
									{
										global::Char.myCharz().arrItemBag[num115].itemOption[num118] = new ItemOption(num119, param5);
									}
									num5 = num118;
								}
								global::Char.myCharz().arrItemBag[num115].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemBag[num115]);
							}
							bool flag230 = global::Char.myCharz().arrItemBag[num115].template.type == 11;
							if (flag230)
							{
							}
							bool flag231 = global::Char.myCharz().arrItemBag[num115].template.type == 6;
							if (flag231)
							{
								GameScr.hpPotion += global::Char.myCharz().arrItemBag[num115].quantity;
							}
						}
						num5 = num115;
					}
				}
				bool flag232 = b50 == 2;
				if (flag232)
				{
					sbyte b51 = msg.reader().readByte();
					int quantity = msg.reader().readInt();
					int quantity2 = global::Char.myCharz().arrItemBag[(int)b51].quantity;
					global::Char.myCharz().arrItemBag[(int)b51].quantity = quantity;
					bool flag233 = global::Char.myCharz().arrItemBag[(int)b51].quantity < quantity2 && global::Char.myCharz().arrItemBag[(int)b51].template.type == 6;
					if (flag233)
					{
						GameScr.hpPotion -= quantity2 - global::Char.myCharz().arrItemBag[(int)b51].quantity;
					}
					bool flag234 = global::Char.myCharz().arrItemBag[(int)b51].quantity == 0;
					if (flag234)
					{
						global::Char.myCharz().arrItemBag[(int)b51] = null;
					}
				}
				break;
			}
			case -35:
			{
				sbyte b52 = msg.reader().readByte();
				Res.outz("cAction= " + b52);
				bool flag235 = b52 == 0;
				if (flag235)
				{
					int num120 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemBox = new Item[num120];
					GameCanvas.panel.hasUse = 0;
					int num5;
					for (int num121 = 0; num121 < num120; num121 = num5 + 1)
					{
						short num122 = msg.reader().readShort();
						bool flag236 = num122 == -1;
						if (!flag236)
						{
							global::Char.myCharz().arrItemBox[num121] = new Item();
							global::Char.myCharz().arrItemBox[num121].template = ItemTemplates.get(num122);
							global::Char.myCharz().arrItemBox[num121].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBox[num121].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBox[num121].content = msg.reader().readUTF();
							int num123 = (int)msg.reader().readUnsignedByte();
							bool flag237 = num123 != 0;
							if (flag237)
							{
								global::Char.myCharz().arrItemBox[num121].itemOption = new ItemOption[num123];
								for (int num124 = 0; num124 < global::Char.myCharz().arrItemBox[num121].itemOption.Length; num124 = num5 + 1)
								{
									int num125 = (int)msg.reader().readUnsignedByte();
									int param6 = (int)msg.reader().readUnsignedShort();
									bool flag238 = num125 != -1;
									if (flag238)
									{
										global::Char.myCharz().arrItemBox[num121].itemOption[num124] = new ItemOption(num125, param6);
									}
									num5 = num124;
								}
							}
							Panel panel = GameCanvas.panel;
							Panel panel3 = panel;
							num5 = panel.hasUse;
							panel3.hasUse = num5 + 1;
						}
						num5 = num121;
					}
				}
				bool flag239 = b52 == 1;
				if (flag239)
				{
					bool isBoxClan = false;
					try
					{
						sbyte b53 = msg.reader().readByte();
						bool flag240 = b53 == 1;
						if (flag240)
						{
							isBoxClan = true;
						}
					}
					catch (Exception)
					{
					}
					GameCanvas.panel.setTypeBox();
					GameCanvas.panel.isBoxClan = isBoxClan;
					GameCanvas.panel.show();
				}
				bool flag241 = b52 == 2;
				if (flag241)
				{
					sbyte b54 = msg.reader().readByte();
					int quantity3 = msg.reader().readInt();
					global::Char.myCharz().arrItemBox[(int)b54].quantity = quantity3;
					bool flag242 = global::Char.myCharz().arrItemBox[(int)b54].quantity == 0;
					if (flag242)
					{
						global::Char.myCharz().arrItemBox[(int)b54] = null;
					}
				}
				break;
			}
			case -34:
			{
				sbyte b55 = msg.reader().readByte();
				Res.outz("act= " + b55);
				bool flag243 = b55 == 0 && GameScr.gI().magicTree != null;
				if (flag243)
				{
					Res.outz("toi duoc day");
					MagicTree magicTree = GameScr.gI().magicTree;
					magicTree.id = (int)msg.reader().readShort();
					magicTree.name = msg.reader().readUTF();
					magicTree.name = Res.changeString(magicTree.name);
					magicTree.x = (int)msg.reader().readShort();
					magicTree.y = (int)msg.reader().readShort();
					magicTree.level = (int)msg.reader().readByte();
					magicTree.currPeas = (int)msg.reader().readShort();
					magicTree.maxPeas = (int)msg.reader().readShort();
					Res.outz("curr Peas= " + magicTree.currPeas);
					magicTree.strInfo = msg.reader().readUTF();
					magicTree.seconds = msg.reader().readInt();
					magicTree.timeToRecieve = magicTree.seconds;
					sbyte b56 = msg.reader().readByte();
					magicTree.peaPostionX = new int[(int)b56];
					magicTree.peaPostionY = new int[(int)b56];
					int num5;
					for (int num126 = 0; num126 < (int)b56; num126 = num5 + 1)
					{
						magicTree.peaPostionX[num126] = (int)msg.reader().readByte();
						magicTree.peaPostionY[num126] = (int)msg.reader().readByte();
						num5 = num126;
					}
					magicTree.isUpdate = msg.reader().readBool();
					magicTree.last = (magicTree.cur = mSystem.currentTimeMillis());
					GameScr.gI().magicTree.isUpdateTree = true;
				}
				bool flag244 = b55 == 1;
				if (flag244)
				{
					myVector = new MyVector();
					try
					{
						while (msg.reader().available() > 0)
						{
							string caption2 = msg.reader().readUTF();
							myVector.addElement(new Command(caption2, GameCanvas.instance, 888392, null));
						}
					}
					catch (Exception ex)
					{
						Cout.println("Loi MAGIC_TREE " + ex.ToString());
					}
					GameCanvas.menu.startAt(myVector, 3);
				}
				bool flag245 = b55 == 2;
				if (flag245)
				{
					GameScr.gI().magicTree.remainPeas = (int)msg.reader().readShort();
					GameScr.gI().magicTree.seconds = msg.reader().readInt();
					GameScr.gI().magicTree.last = (GameScr.gI().magicTree.cur = mSystem.currentTimeMillis());
					GameScr.gI().magicTree.isUpdateTree = true;
					GameScr.gI().magicTree.isPeasEffect = true;
				}
				break;
			}
			case -32:
			{
				short num127 = msg.reader().readShort();
				int num128 = msg.reader().readInt();
				sbyte[] array8 = null;
				Image image = null;
				try
				{
					array8 = new sbyte[num128];
					int num5;
					for (int num129 = 0; num129 < num128; num129 = num5 + 1)
					{
						array8[num129] = msg.reader().readByte();
						num5 = num129;
					}
					image = Image.createImage(array8, 0, num128);
					BgItem.imgNew.put(num127 + string.Empty, image);
				}
				catch (Exception)
				{
					array8 = null;
					BgItem.imgNew.put(num127 + string.Empty, Image.createRGBImage(new int[1], 1, 1, true));
				}
				bool flag246 = array8 != null;
				if (flag246)
				{
					bool flag247 = mGraphics.zoomLevel > 1;
					if (flag247)
					{
						Rms.saveRMS(mGraphics.zoomLevel + "bgItem" + num127, array8);
					}
					BgItemMn.blendcurrBg(num127, image);
				}
				break;
			}
			case -31:
			{
				TileMap.vItemBg.removeAllElements();
				short num130 = msg.reader().readShort();
				Cout.LogError2("nItem= " + num130);
				int num5;
				for (int num131 = 0; num131 < (int)num130; num131 = num5 + 1)
				{
					BgItem bgItem = new BgItem();
					bgItem.id = num131;
					bgItem.idImage = msg.reader().readShort();
					bgItem.layer = msg.reader().readByte();
					bgItem.dx = (int)msg.reader().readShort();
					bgItem.dy = (int)msg.reader().readShort();
					sbyte b57 = msg.reader().readByte();
					bgItem.tileX = new int[(int)b57];
					bgItem.tileY = new int[(int)b57];
					for (int num132 = 0; num132 < (int)b57; num132 = num5 + 1)
					{
						bgItem.tileX[num131] = (int)msg.reader().readByte();
						bgItem.tileY[num131] = (int)msg.reader().readByte();
						num5 = num132;
					}
					TileMap.vItemBg.addElement(bgItem);
					num5 = num131;
				}
				break;
			}
			case -30:
				this.messageSubCommand(msg);
				break;
			case -29:
				this.messageNotLogin(msg);
				break;
			case -28:
				this.messageNotMap(msg);
				break;
			case -26:
			{
				ServerListScreen.testConnect = 2;
				GameCanvas.debug("SA2", 2);
				GameCanvas.startOKDlg(msg.reader().readUTF());
				InfoDlg.hide();
				LoginScr.isContinueToLogin = false;
				global::Char.isLoadingMap = false;
				bool flag248 = GameCanvas.currentScreen == GameCanvas.loginScr;
				if (flag248)
				{
					GameCanvas.serverScreen.switchToMe();
				}
				break;
			}
			case -25:
				GameCanvas.debug("SA3", 2);
				GameScr.info1.addInfo(msg.reader().readUTF(), 0);
				break;
			case -24:
				global::Char.isLoadingMap = true;
				Cout.println("GET MAP INFO");
				GameScr.gI().magicTree = null;
				GameCanvas.isLoading = true;
				GameCanvas.debug("SA75", 2);
				GameScr.resetAllvector();
				GameCanvas.endDlg();
				TileMap.vGo.removeAllElements();
				PopUp.vPopups.removeAllElements();
				mSystem.gcc();
				TileMap.mapID = (int)msg.reader().readUnsignedByte();
				TileMap.planetID = msg.reader().readByte();
				TileMap.tileID = (int)msg.reader().readByte();
				TileMap.bgID = (int)msg.reader().readByte();
				Cout.println(string.Concat(new object[]
				{
					"load planet from server: ",
					TileMap.planetID,
					"bgType= ",
					TileMap.bgType,
					"............................."
				}));
				TileMap.typeMap = (int)msg.reader().readByte();
				TileMap.mapName = msg.reader().readUTF();
				TileMap.zoneID = (int)msg.reader().readByte();
				GameCanvas.debug("SA75x1", 2);
				try
				{
					TileMap.loadMapFromResource(TileMap.mapID);
				}
				catch (Exception)
				{
					Service.gI().requestMaptemplate(TileMap.mapID);
					this.messWait = msg;
					return;
				}
				this.loadInfoMap(msg);
				try
				{
					TileMap.isMapDouble = (msg.reader().readByte() != 0);
				}
				catch (Exception)
				{
				}
				GameScr.cmx = GameScr.cmtoX;
				GameScr.cmy = GameScr.cmtoY;
				break;
			case -22:
				GameCanvas.debug("SA65", 2);
				global::Char.isLockKey = true;
				global::Char.ischangingMap = true;
				GameScr.gI().timeStartMap = 0;
				GameScr.gI().timeLengthMap = 0;
				global::Char.myCharz().mobFocus = null;
				global::Char.myCharz().npcFocus = null;
				global::Char.myCharz().charFocus = null;
				global::Char.myCharz().itemFocus = null;
				global::Char.myCharz().focus.removeAllElements();
				global::Char.myCharz().testCharId = -9999;
				global::Char.myCharz().killCharId = -9999;
				GameCanvas.resetBg();
				GameScr.gI().resetButton();
				GameScr.gI().center = null;
				break;
			case -21:
			{
				GameCanvas.debug("SA60", 2);
				short num133 = msg.reader().readShort();
				int num5;
				for (int num134 = 0; num134 < GameScr.vItemMap.size(); num134 = num5 + 1)
				{
					bool flag249 = ((ItemMap)GameScr.vItemMap.elementAt(num134)).itemMapID == (int)num133;
					if (flag249)
					{
						GameScr.vItemMap.removeElementAt(num134);
						break;
					}
					num5 = num134;
				}
				break;
			}
			case -20:
			{
				GameCanvas.debug("SA61", 2);
				global::Char.myCharz().itemFocus = null;
				short num135 = msg.reader().readShort();
				int num5;
				for (int num136 = 0; num136 < GameScr.vItemMap.size(); num136 = num5 + 1)
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(num136);
					bool flag250 = itemMap.itemMapID != (int)num135;
					if (!flag250)
					{
						itemMap.setPoint(global::Char.myCharz().cx, global::Char.myCharz().cy - 10);
						string text4 = msg.reader().readUTF();
						i = 0;
						try
						{
							i = (int)msg.reader().readShort();
							bool flag251 = itemMap.template.type == 9;
							if (flag251)
							{
								i = (int)msg.reader().readShort();
								global::Char char9 = global::Char.myCharz();
								char9.xu += (long)i;
								global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
							}
							else
							{
								bool flag252 = itemMap.template.type == 10;
								if (flag252)
								{
									i = (int)msg.reader().readShort();
									global::Char char9 = global::Char.myCharz();
									char9.luong += i;
									global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
								}
								else
								{
									bool flag253 = itemMap.template.type == 34;
									if (flag253)
									{
										i = (int)msg.reader().readShort();
										global::Char char9 = global::Char.myCharz();
										char9.luongKhoa += i;
										global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
									}
								}
							}
						}
						catch (Exception)
						{
						}
						bool flag254 = text4.Equals(string.Empty);
						if (flag254)
						{
							bool flag255 = itemMap.template.type == 9;
							if (flag255)
							{
								GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.YELLOW);
								SoundMn.gI().getItem();
							}
							else
							{
								bool flag256 = itemMap.template.type == 10;
								if (flag256)
								{
									GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.GREEN);
									SoundMn.gI().getItem();
								}
								else
								{
									bool flag257 = itemMap.template.type == 34;
									if (flag257)
									{
										GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.RED);
										SoundMn.gI().getItem();
									}
									else
									{
										GameScr.info1.addInfo(mResources.you_receive + " " + ((i <= 0) ? string.Empty : (i + " ")) + itemMap.template.name, 0);
										SoundMn.gI().getItem();
									}
								}
							}
							bool flag258 = i > 0 && global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 4683;
							if (flag258)
							{
								ServerEffect.addServerEffect(55, global::Char.myCharz().petFollow.cmx, global::Char.myCharz().petFollow.cmy, 1);
								ServerEffect.addServerEffect(55, global::Char.myCharz().cx, global::Char.myCharz().cy, 1);
							}
						}
						else
						{
							bool flag259 = text4.Length == 1;
							if (flag259)
							{
								Cout.LogError3("strInf.Length =1:  " + text4);
							}
							else
							{
								GameScr.info1.addInfo(text4, 0);
							}
						}
						break;
					}
					num5 = num136;
				}
				break;
			}
			case -19:
			{
				GameCanvas.debug("SA62", 2);
				short num137 = msg.reader().readShort();
				@char = GameScr.findCharInMap(msg.reader().readInt());
				int num138 = 0;
				while (num138 < GameScr.vItemMap.size())
				{
					ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(num138);
					bool flag260 = itemMap2.itemMapID != (int)num137;
					if (flag260)
					{
						int num5 = num138;
						num138 = num5 + 1;
					}
					else
					{
						bool flag261 = @char == null;
						if (flag261)
						{
							return;
						}
						itemMap2.setPoint(@char.cx, @char.cy - 10);
						bool flag262 = itemMap2.x < @char.cx;
						if (flag262)
						{
							@char.cdir = -1;
						}
						else
						{
							bool flag263 = itemMap2.x > @char.cx;
							if (flag263)
							{
								@char.cdir = 1;
							}
						}
						break;
					}
				}
				break;
			}
			case -18:
			{
				GameCanvas.debug("SA63", 2);
				int num139 = (int)msg.reader().readByte();
				GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), global::Char.myCharz().arrItemBag[num139].template.id, global::Char.myCharz().cx, global::Char.myCharz().cy, (int)msg.reader().readShort(), (int)msg.reader().readShort()));
				global::Char.myCharz().arrItemBag[num139] = null;
				break;
			}
			case -14:
			{
				GameCanvas.debug("SA64", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				bool flag264 = @char == null;
				if (flag264)
				{
					return;
				}
				GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), msg.reader().readShort(), @char.cx, @char.cy, (int)msg.reader().readShort(), (int)msg.reader().readShort()));
				break;
			}
			case -4:
			{
				GameCanvas.debug("SA76", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				bool flag265 = @char == null;
				if (flag265)
				{
					return;
				}
				GameCanvas.debug("SA76v1", 2);
				bool flag266 = (TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2;
				if (flag266)
				{
					@char.setSkillPaint(GameScr.sks[(int)msg.reader().readUnsignedByte()], 0);
				}
				else
				{
					@char.setSkillPaint(GameScr.sks[(int)msg.reader().readUnsignedByte()], 1);
				}
				GameCanvas.debug("SA76v2", 2);
				@char.attMobs = new Mob[(int)msg.reader().readByte()];
				int num5;
				for (int num140 = 0; num140 < @char.attMobs.Length; num140 = num5 + 1)
				{
					Mob mob5 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readByte());
					@char.attMobs[num140] = mob5;
					bool flag267 = num140 == 0;
					if (flag267)
					{
						bool flag268 = @char.cx <= mob5.x;
						if (flag268)
						{
							@char.cdir = 1;
						}
						else
						{
							@char.cdir = -1;
						}
					}
					num5 = num140;
				}
				GameCanvas.debug("SA76v3", 2);
				@char.charFocus = null;
				@char.mobFocus = @char.attMobs[0];
				global::Char[] array9 = new global::Char[10];
				i = 0;
				try
				{
					for (i = 0; i < array9.Length; i = num5 + 1)
					{
						int num141 = msg.reader().readInt();
						global::Char char10 = array9[i] = ((num141 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num141) : global::Char.myCharz());
						bool flag269 = i == 0;
						if (flag269)
						{
							bool flag270 = @char.cx <= char10.cx;
							if (flag270)
							{
								@char.cdir = 1;
							}
							else
							{
								@char.cdir = -1;
							}
						}
						num5 = i;
					}
				}
				catch (Exception ex2)
				{
					Cout.println("Loi PLAYER_ATTACK_N_P " + ex2.ToString());
				}
				GameCanvas.debug("SA76v4", 2);
				bool flag271 = i > 0;
				if (flag271)
				{
					@char.attChars = new global::Char[i];
					for (i = 0; i < @char.attChars.Length; i = num5 + 1)
					{
						@char.attChars[i] = array9[i];
						num5 = i;
					}
					@char.charFocus = @char.attChars[0];
					@char.mobFocus = null;
				}
				GameCanvas.debug("SA76v5", 2);
				break;
			}
			case 1:
			{
				bool flag272 = msg.reader().readBool();
				Res.outz("isRes= " + flag272.ToString());
				bool flag273 = !flag272;
				if (flag273)
				{
					GameCanvas.startOKDlg(msg.reader().readUTF());
				}
				else
				{
					GameCanvas.loginScr.isLogin2 = false;
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, string.Empty);
					GameCanvas.endDlg();
					GameCanvas.loginScr.doLogin();
				}
				break;
			}
			case 2:
			{
				global::Char.isLoadingMap = true;
				LoginScr.isLoggingIn = false;
				bool flag274 = !GameScr.isLoadAllData;
				if (flag274)
				{
					GameScr.gI().initSelectChar();
				}
				BgItem.clearHashTable();
				GameCanvas.endDlg();
				CreateCharScr.isCreateChar = true;
				CreateCharScr.gI().switchToMe();
				break;
			}
			case 6:
				GameCanvas.debug("SA70", 2);
				global::Char.myCharz().xu = msg.reader().readLong();
				global::Char.myCharz().luong = msg.reader().readInt();
				global::Char.myCharz().luongKhoa = msg.reader().readInt();
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
				global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
				GameCanvas.endDlg();
				break;
			case 7:
			{
				sbyte type3 = msg.reader().readByte();
				short id2 = msg.reader().readShort();
				string info3 = msg.reader().readUTF();
				GameCanvas.panel.saleRequest(type3, info3, id2);
				break;
			}
			case 11:
			{
				GameCanvas.debug("SA9", 2);
				int num142 = (int)msg.reader().readByte();
				sbyte b58 = msg.reader().readByte();
				bool flag275 = b58 != 0;
				if (flag275)
				{
					Mob.arrMobTemplate[num142].data.readDataNewBoss(NinjaUtil.readByteArray(msg), b58);
				}
				else
				{
					Mob.arrMobTemplate[num142].data.readData(NinjaUtil.readByteArray(msg));
				}
				int num5;
				for (int num143 = 0; num143 < GameScr.vMob.size(); num143 = num5 + 1)
				{
					Mob mob2 = (Mob)GameScr.vMob.elementAt(num143);
					bool flag276 = mob2.templateId == num142;
					if (flag276)
					{
						mob2.w = Mob.arrMobTemplate[num142].data.width;
						mob2.h = Mob.arrMobTemplate[num142].data.height;
					}
					num5 = num143;
				}
				sbyte[] array10 = NinjaUtil.readByteArray(msg);
				Image img = Image.createImage(array10, 0, array10.Length);
				Mob.arrMobTemplate[num142].data.img = img;
				int num144 = (int)msg.reader().readByte();
				Mob.arrMobTemplate[num142].data.typeData = num144;
				bool flag277 = num144 == 1 || num144 == 2;
				if (flag277)
				{
					this.readFrameBoss(msg, num142);
				}
				break;
			}
			case 20:
				this.phuban_Info(msg);
				break;
			case 24:
				this.read_opt(msg);
				break;
			case 27:
			{
				myVector = new MyVector();
				string text5 = msg.reader().readUTF();
				int num145 = (int)msg.reader().readByte();
				int num5;
				for (int num146 = 0; num146 < num145; num146 = num5 + 1)
				{
					string caption3 = msg.reader().readUTF();
					short num147 = msg.reader().readShort();
					myVector.addElement(new Command(caption3, GameCanvas.instance, 88819, num147));
					num5 = num146;
				}
				GameCanvas.menu.startWithoutCloseButton(myVector, 3);
				break;
			}
			case 29:
				GameCanvas.debug("SA58", 2);
				GameScr.gI().openUIZone(msg);
				break;
			case 32:
			{
				GameCanvas.debug("SA68", 2);
				int num148 = (int)msg.reader().readShort();
				int num5;
				for (int num149 = 0; num149 < GameScr.vNpc.size(); num149 = num5 + 1)
				{
					Npc npc2 = (Npc)GameScr.vNpc.elementAt(num149);
					bool flag278 = npc2.template.npcTemplateId == num148 && npc2.Equals(global::Char.myCharz().npcFocus);
					if (flag278)
					{
						string chat2 = msg.reader().readUTF();
						string[] array11 = new string[(int)msg.reader().readByte()];
						for (int num150 = 0; num150 < array11.Length; num150 = num5 + 1)
						{
							array11[num150] = msg.reader().readUTF();
							num5 = num150;
						}
						GameScr.gI().createMenu(array11, npc2);
						ChatPopup.addChatPopup(chat2, 100000, npc2);
						return;
					}
					num5 = num149;
				}
				Npc npc3 = new Npc(num148, 0, -100, 100, num148, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
				Res.outz((global::Char.myCharz().npcFocus == null) ? "null" : "!null");
				string chat3 = msg.reader().readUTF();
				string[] array12 = new string[(int)msg.reader().readByte()];
				for (int num151 = 0; num151 < array12.Length; num151 = num5 + 1)
				{
					array12[num151] = msg.reader().readUTF();
					num5 = num151;
				}
				try
				{
					short num152 = (short)(npc3.avatar = (int)msg.reader().readShort());
				}
				catch (Exception)
				{
				}
				Res.outz((global::Char.myCharz().npcFocus == null) ? "null" : "!null");
				GameScr.gI().createMenu(array12, npc3);
				ChatPopup.addChatPopup(chat3, 100000, npc3);
				break;
			}
			case 33:
			{
				GameCanvas.debug("SA51", 2);
				InfoDlg.hide();
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				myVector = new MyVector();
				try
				{
					for (;;)
					{
						string caption4 = msg.reader().readUTF();
						myVector.addElement(new Command(caption4, GameCanvas.instance, 88822, null));
					}
				}
				catch (Exception ex3)
				{
					Cout.println("Loi OPEN_UI_MENU " + ex3.ToString());
				}
				bool flag279 = global::Char.myCharz().npcFocus == null;
				if (flag279)
				{
					return;
				}
				int num5;
				for (int num153 = 0; num153 < global::Char.myCharz().npcFocus.template.menu.Length; num153 = num5 + 1)
				{
					string[] array13 = global::Char.myCharz().npcFocus.template.menu[num153];
					myVector.addElement(new Command(array13[0], GameCanvas.instance, 88820, array13));
					num5 = num153;
				}
				GameCanvas.menu.startAt(myVector, 3);
				break;
			}
			case 38:
			{
				GameCanvas.debug("SA67", 2);
				InfoDlg.hide();
				int num154 = (int)msg.reader().readShort();
				Res.outz("OPEN_UI_SAY ID= " + num154);
				string text6 = msg.reader().readUTF();
				text6 = Res.changeString(text6);
				int num5;
				for (int num155 = 0; num155 < GameScr.vNpc.size(); num155 = num5 + 1)
				{
					Npc npc4 = (Npc)GameScr.vNpc.elementAt(num155);
					Res.outz("npc id= " + npc4.template.npcTemplateId);
					bool flag280 = npc4.template.npcTemplateId == num154;
					if (flag280)
					{
						ChatPopup.addChatPopupMultiLine(text6, 100000, npc4);
						GameCanvas.panel.hideNow();
						return;
					}
					num5 = num155;
				}
				Npc npc5 = new Npc(num154, 0, 0, 0, num154, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
				bool flag281 = npc5.template.npcTemplateId == 5;
				if (flag281)
				{
					npc5.charID = 5;
				}
				try
				{
					npc5.avatar = (int)msg.reader().readShort();
				}
				catch (Exception)
				{
				}
				ChatPopup.addChatPopupMultiLine(text6, 100000, npc5);
				GameCanvas.panel.hideNow();
				break;
			}
			case 39:
			{
				GameCanvas.debug("SA49", 2);
				GameScr.gI().typeTradeOrder = 2;
				bool flag282 = GameScr.gI().typeTrade >= 2 && GameScr.gI().typeTradeOrder >= 2;
				if (flag282)
				{
					InfoDlg.showWait();
				}
				break;
			}
			case 40:
			{
				GameCanvas.debug("SA52", 2);
				GameCanvas.taskTick = 150;
				short taskId = msg.reader().readShort();
				sbyte index3 = msg.reader().readByte();
				string text7 = msg.reader().readUTF();
				text7 = Res.changeString(text7);
				string text8 = msg.reader().readUTF();
				text8 = Res.changeString(text8);
				string[] array14 = new string[(int)msg.reader().readByte()];
				string[] array15 = new string[array14.Length];
				GameScr.tasks = new int[array14.Length];
				GameScr.mapTasks = new int[array14.Length];
				short[] array16 = new short[array14.Length];
				short count = -1;
				int num5;
				for (int num156 = 0; num156 < array14.Length; num156 = num5 + 1)
				{
					string text9 = msg.reader().readUTF();
					text9 = Res.changeString(text9);
					GameScr.tasks[num156] = (int)msg.reader().readByte();
					GameScr.mapTasks[num156] = (int)msg.reader().readShort();
					string text10 = msg.reader().readUTF();
					text10 = Res.changeString(text10);
					array16[num156] = -1;
					bool flag283 = !text9.Equals(string.Empty);
					if (flag283)
					{
						array14[num156] = text9;
						array15[num156] = text10;
					}
					num5 = num156;
				}
				try
				{
					count = msg.reader().readShort();
					for (int num157 = 0; num157 < array14.Length; num157 = num5 + 1)
					{
						array16[num157] = msg.reader().readShort();
						num5 = num157;
					}
				}
				catch (Exception ex4)
				{
					Cout.println("Loi TASK_GET " + ex4.ToString());
				}
				global::Char.myCharz().taskMaint = new Task(taskId, index3, text7, text8, array14, array16, count, array15);
				bool flag284 = global::Char.myCharz().npcFocus != null;
				if (flag284)
				{
					Npc.clearEffTask();
				}
				global::Char.taskAction(false);
				break;
			}
			case 41:
			{
				GameCanvas.debug("SA53", 2);
				GameCanvas.taskTick = 100;
				Res.outz("TASK NEXT");
				Task taskMaint = global::Char.myCharz().taskMaint;
				Task task = taskMaint;
				int num5 = taskMaint.index;
				task.index = num5 + 1;
				global::Char.myCharz().taskMaint.count = 0;
				Npc.clearEffTask();
				global::Char.taskAction(true);
				break;
			}
			case 43:
			{
				GameCanvas.taskTick = 50;
				GameCanvas.debug("SA55", 2);
				global::Char.myCharz().taskMaint.count = msg.reader().readShort();
				bool flag285 = global::Char.myCharz().npcFocus != null;
				if (flag285)
				{
					Npc.clearEffTask();
				}
				break;
			}
			case 46:
				GameCanvas.debug("SA5", 2);
				Cout.LogWarning("Controler RESET_POINT  " + global::Char.ischangingMap.ToString());
				global::Char.isLockKey = false;
				global::Char.myCharz().setResetPoint((int)msg.reader().readShort(), (int)msg.reader().readShort());
				break;
			case 47:
				GameCanvas.debug("SA4", 2);
				GameScr.gI().resetButton();
				break;
			case 50:
			{
				sbyte b59 = msg.reader().readByte();
				Panel.vGameInfo.removeAllElements();
				int num5;
				for (int num158 = 0; num158 < (int)b59; num158 = num5 + 1)
				{
					GameInfo gameInfo = new GameInfo();
					gameInfo.id = msg.reader().readShort();
					gameInfo.main = msg.reader().readUTF();
					gameInfo.content = msg.reader().readUTF();
					Panel.vGameInfo.addElement(gameInfo);
					bool flag286 = gameInfo.hasRead = ((Rms.loadRMSInt(gameInfo.id + string.Empty) != -1) ? true : false);
					num5 = num158;
				}
				break;
			}
			case 54:
			{
				@char = GameScr.findCharInMap(msg.reader().readInt());
				bool flag287 = @char == null;
				if (flag287)
				{
					return;
				}
				int num159 = (int)msg.reader().readUnsignedByte();
				bool flag288 = (TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2;
				if (flag288)
				{
					@char.setSkillPaint(GameScr.sks[num159], 0);
				}
				else
				{
					@char.setSkillPaint(GameScr.sks[num159], 1);
				}
				GameCanvas.debug("SA769991v2", 2);
				Mob[] array17 = new Mob[10];
				i = 0;
				try
				{
					GameCanvas.debug("SA769991v3", 2);
					int num5;
					for (i = 0; i < array17.Length; i = num5 + 1)
					{
						GameCanvas.debug("SA769991v4-num" + i, 2);
						Mob mob6 = array17[i] = (Mob)GameScr.vMob.elementAt((int)msg.reader().readByte());
						bool flag289 = i == 0;
						if (flag289)
						{
							bool flag290 = @char.cx <= mob6.x;
							if (flag290)
							{
								@char.cdir = 1;
							}
							else
							{
								@char.cdir = -1;
							}
						}
						GameCanvas.debug("SA769991v5-num" + i, 2);
						num5 = i;
					}
				}
				catch (Exception ex5)
				{
					Cout.println("Loi PLAYER_ATTACK_NPC " + ex5.ToString());
				}
				GameCanvas.debug("SA769992", 2);
				bool flag291 = i > 0;
				if (flag291)
				{
					@char.attMobs = new Mob[i];
					int num5;
					for (i = 0; i < @char.attMobs.Length; i = num5 + 1)
					{
						@char.attMobs[i] = array17[i];
						num5 = i;
					}
					@char.charFocus = null;
					@char.mobFocus = @char.attMobs[0];
				}
				break;
			}
			case 56:
			{
				GameCanvas.debug("SXX6", 2);
				@char = null;
				int num160 = msg.reader().readInt();
				bool flag292 = num160 == global::Char.myCharz().charID;
				if (flag292)
				{
					bool flag293 = false;
					@char = global::Char.myCharz();
					@char.cHP = msg.readInt3Byte();
					int num161 = msg.readInt3Byte();
					Res.outz("dame hit = " + num161);
					bool flag294 = num161 != 0;
					if (flag294)
					{
						@char.doInjure();
					}
					int num162 = 0;
					try
					{
						flag293 = msg.reader().readBoolean();
						sbyte b60 = msg.reader().readByte();
						bool flag295 = b60 != -1;
						if (flag295)
						{
							Res.outz("hit eff= " + b60);
							EffecMn.addEff(new Effect((int)b60, @char.cx, @char.cy, 3, 1, -1));
						}
					}
					catch (Exception)
					{
					}
					num161 += num162;
					bool flag296 = global::Char.myCharz().cTypePk != 4;
					if (flag296)
					{
						bool flag297 = num161 == 0;
						if (flag297)
						{
							GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS_ME);
						}
						else
						{
							GameScr.startFlyText("-" + num161, @char.cx, @char.cy - @char.ch, 0, -3, flag293 ? mFont.FATAL : mFont.RED);
						}
					}
				}
				else
				{
					@char = GameScr.findCharInMap(num160);
					bool flag298 = @char == null;
					if (flag298)
					{
						return;
					}
					@char.cHP = msg.readInt3Byte();
					bool flag299 = false;
					int num163 = msg.readInt3Byte();
					bool flag300 = num163 != 0;
					if (flag300)
					{
						@char.doInjure();
					}
					int num164 = 0;
					try
					{
						flag299 = msg.reader().readBoolean();
						sbyte b61 = msg.reader().readByte();
						bool flag301 = b61 != -1;
						if (flag301)
						{
							Res.outz("hit eff= " + b61);
							EffecMn.addEff(new Effect((int)b61, @char.cx, @char.cy, 3, 1, -1));
						}
					}
					catch (Exception)
					{
					}
					num163 += num164;
					bool flag302 = @char.cTypePk != 4;
					if (flag302)
					{
						bool flag303 = num163 == 0;
						if (flag303)
						{
							GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS);
						}
						else
						{
							GameScr.startFlyText("-" + num163, @char.cx, @char.cy - @char.ch, 0, -3, flag299 ? mFont.FATAL : mFont.ORANGE);
						}
					}
				}
				break;
			}
			case 57:
			{
				GameCanvas.debug("SZ6", 2);
				MyVector myVector2 = new MyVector();
				myVector2.addElement(new Command(msg.reader().readUTF(), GameCanvas.instance, 88817, null));
				GameCanvas.menu.startAt(myVector2, 3);
				break;
			}
			case 58:
			{
				GameCanvas.debug("SZ7", 2);
				int num165 = msg.reader().readInt();
				global::Char char11 = (num165 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num165) : global::Char.myCharz();
				char11.moveFast = new short[3];
				char11.moveFast[0] = 0;
				short num166 = msg.reader().readShort();
				short num167 = msg.reader().readShort();
				char11.moveFast[1] = num166;
				char11.moveFast[2] = num167;
				try
				{
					num165 = msg.reader().readInt();
					global::Char char12 = (num165 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num165) : global::Char.myCharz();
					char12.cx = (int)num166;
					char12.cy = (int)num167;
				}
				catch (Exception ex6)
				{
					Cout.println("Loi MOVE_FAST " + ex6.ToString());
				}
				break;
			}
			case 62:
			{
				GameCanvas.debug("SZ3", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				bool flag304 = @char != null;
				if (flag304)
				{
					@char.killCharId = global::Char.myCharz().charID;
					global::Char.myCharz().npcFocus = null;
					global::Char.myCharz().mobFocus = null;
					global::Char.myCharz().itemFocus = null;
					global::Char.myCharz().charFocus = @char;
					global::Char.isManualFocus = true;
					GameScr.info1.addInfo(@char.cName + mResources.CUU_SAT, 0);
				}
				break;
			}
			case 63:
				GameCanvas.debug("SZ4", 2);
				global::Char.myCharz().killCharId = msg.reader().readInt();
				global::Char.myCharz().npcFocus = null;
				global::Char.myCharz().mobFocus = null;
				global::Char.myCharz().itemFocus = null;
				global::Char.myCharz().charFocus = GameScr.findCharInMap(global::Char.myCharz().killCharId);
				global::Char.isManualFocus = true;
				break;
			case 64:
				GameCanvas.debug("SZ5", 2);
				@char = global::Char.myCharz();
				try
				{
					@char = GameScr.findCharInMap(msg.reader().readInt());
				}
				catch (Exception ex7)
				{
					Cout.println("Loi CLEAR_CUU_SAT " + ex7.ToString());
				}
				@char.killCharId = -9999;
				break;
			case 65:
			{
				sbyte id3 = msg.reader().readSByte();
				string text11 = msg.reader().readUTF();
				short num168 = msg.reader().readShort();
				bool flag305 = ItemTime.isExistMessage((int)id3);
				if (flag305)
				{
					bool flag306 = num168 != 0;
					if (flag306)
					{
						ItemTime.getMessageById((int)id3).initTimeText(id3, text11, (int)num168);
					}
					else
					{
						GameScr.textTime.removeElement(ItemTime.getMessageById((int)id3));
					}
				}
				else
				{
					ItemTime itemTime = new ItemTime();
					itemTime.initTimeText(id3, text11, (int)num168);
					GameScr.textTime.addElement(itemTime);
				}
				break;
			}
			case 66:
				this.readGetImgByName(msg);
				break;
			case 68:
			{
				Res.outz("ADD ITEM TO MAP --------------------------------------");
				GameCanvas.debug("SA6333", 2);
				short itemMapID = msg.reader().readShort();
				short itemTemplateID = msg.reader().readShort();
				int x = (int)msg.reader().readShort();
				int y = (int)msg.reader().readShort();
				int num169 = msg.reader().readInt();
				short r = 0;
				bool flag307 = num169 == -2;
				if (flag307)
				{
					r = msg.reader().readShort();
				}
				ItemMap o2 = new ItemMap(num169, itemMapID, itemTemplateID, x, y, r);
				GameScr.vItemMap.addElement(o2);
				break;
			}
			case 69:
				SoundMn.IsDelAcc = (msg.reader().readByte() != 0);
				break;
			case 81:
			{
				GameCanvas.debug("SXX4", 2);
				Mob mob7 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob7.isDisable = msg.reader().readBool();
				break;
			}
			case 82:
			{
				GameCanvas.debug("SXX5", 2);
				Mob mob8 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob8.isDontMove = msg.reader().readBool();
				break;
			}
			case 83:
			{
				GameCanvas.debug("SXX8", 2);
				int num170 = msg.reader().readInt();
				@char = ((num170 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num170) : global::Char.myCharz());
				bool flag308 = @char == null;
				if (flag308)
				{
					return;
				}
				Mob mobToAttack = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				bool flag309 = @char.mobMe != null;
				if (flag309)
				{
					@char.mobMe.attackOtherMob(mobToAttack);
				}
				break;
			}
			case 84:
			{
				int num171 = msg.reader().readInt();
				bool flag310 = num171 == global::Char.myCharz().charID;
				if (flag310)
				{
					@char = global::Char.myCharz();
				}
				else
				{
					@char = GameScr.findCharInMap(num171);
					bool flag311 = @char == null;
					if (flag311)
					{
						return;
					}
				}
				@char.cHP = @char.cHPFull;
				@char.cMP = @char.cMPFull;
				@char.cx = (int)msg.reader().readShort();
				@char.cy = (int)msg.reader().readShort();
				@char.liveFromDead();
				break;
			}
			case 85:
			{
				GameCanvas.debug("SXX5", 2);
				Mob mob9 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob9.isFire = msg.reader().readBool();
				break;
			}
			case 86:
			{
				GameCanvas.debug("SXX5", 2);
				Mob mob10 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob10.isIce = msg.reader().readBool();
				bool flag312 = !mob10.isIce;
				if (flag312)
				{
					ServerEffect.addServerEffect(77, mob10.x, mob10.y - 9, 1);
				}
				break;
			}
			case 87:
			{
				GameCanvas.debug("SXX5", 2);
				Mob mob11 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob11.isWind = msg.reader().readBool();
				break;
			}
			case 88:
			{
				string info4 = msg.reader().readUTF();
				short num172 = msg.reader().readShort();
				GameCanvas.inputDlg.show(info4, new Command(mResources.ACCEPT, GameCanvas.instance, 88818, num172), TField.INPUT_TYPE_ANY);
				break;
			}
			case 90:
				GameCanvas.debug("SA577", 2);
				this.requestItemPlayer(msg);
				break;
			case 92:
			{
				bool flag313 = GameCanvas.currentScreen == GameScr.instance;
				if (flag313)
				{
					GameCanvas.endDlg();
				}
				string text12 = msg.reader().readUTF();
				string text13 = msg.reader().readUTF();
				text13 = Res.changeString(text13);
				string text14 = string.Empty;
				global::Char char13 = null;
				sbyte b62 = 0;
				bool flag314 = !text12.Equals(string.Empty);
				if (flag314)
				{
					char13 = new global::Char();
					char13.charID = msg.reader().readInt();
					char13.head = (int)msg.reader().readShort();
					char13.headICON = (int)msg.reader().readShort();
					char13.body = (int)msg.reader().readShort();
					char13.bag = (int)msg.reader().readShort();
					char13.leg = (int)msg.reader().readShort();
					b62 = msg.reader().readByte();
					char13.cName = text12;
				}
				text14 += text13;
				InfoDlg.hide();
				bool flag315 = text12.Equals(string.Empty);
				if (flag315)
				{
					GameScr.info1.addInfo(text14, 0);
				}
				else
				{
					GameScr.info2.addInfoWithChar(text14, char13, b62 == 0);
					bool flag316 = GameCanvas.panel.isShow && GameCanvas.panel.type == 8;
					if (flag316)
					{
						GameCanvas.panel.initLogMessage();
					}
				}
				break;
			}
			case 94:
				GameCanvas.debug("SA3", 2);
				GameScr.info1.addInfo(msg.reader().readUTF(), 0);
				break;
			default:
				if (command == 112)
				{
					sbyte b63 = msg.reader().readByte();
					Res.outz("spec type= " + b63);
					bool flag317 = b63 == 0;
					if (flag317)
					{
						Panel.spearcialImage = msg.reader().readShort();
						Panel.specialInfo = msg.reader().readUTF();
					}
					else
					{
						bool flag318 = b63 != 1;
						if (!flag318)
						{
							sbyte b64 = msg.reader().readByte();
							global::Char.myCharz().infoSpeacialSkill = new string[(int)b64][];
							global::Char.myCharz().imgSpeacialSkill = new short[(int)b64][];
							GameCanvas.panel.speacialTabName = new string[(int)b64][];
							int num5;
							for (int num173 = 0; num173 < (int)b64; num173 = num5 + 1)
							{
								GameCanvas.panel.speacialTabName[num173] = new string[2];
								string[] array18 = Res.split(msg.reader().readUTF(), "\n", 0);
								bool flag319 = array18.Length == 2;
								if (flag319)
								{
									GameCanvas.panel.speacialTabName[num173] = array18;
								}
								bool flag320 = array18.Length == 1;
								if (flag320)
								{
									GameCanvas.panel.speacialTabName[num173][0] = array18[0];
									GameCanvas.panel.speacialTabName[num173][1] = string.Empty;
								}
								int num174 = (int)msg.reader().readByte();
								global::Char.myCharz().infoSpeacialSkill[num173] = new string[num174];
								global::Char.myCharz().imgSpeacialSkill[num173] = new short[num174];
								for (int num175 = 0; num175 < num174; num175 = num5 + 1)
								{
									global::Char.myCharz().imgSpeacialSkill[num173][num175] = msg.reader().readShort();
									global::Char.myCharz().infoSpeacialSkill[num173][num175] = msg.reader().readUTF();
									num5 = num175;
								}
								num5 = num173;
							}
							GameCanvas.panel.tabName[25] = GameCanvas.panel.speacialTabName;
							GameCanvas.panel.setTypeSpeacialSkill();
							GameCanvas.panel.show();
						}
					}
				}
				break;
			}
			sbyte command2 = msg.command;
			if (command2 <= 19)
			{
				if (command2 <= -73)
				{
					if (command2 != -75)
					{
						if (command2 == -73)
						{
							sbyte b65 = msg.reader().readByte();
							int num5;
							for (int num176 = 0; num176 < GameScr.vNpc.size(); num176 = num5 + 1)
							{
								Npc npc6 = (Npc)GameScr.vNpc.elementAt(num176);
								bool flag321 = npc6.template.npcTemplateId == (int)b65;
								if (flag321)
								{
									sbyte b66 = msg.reader().readByte();
									bool flag322 = b66 == 0;
									if (flag322)
									{
										npc6.isHide = true;
									}
									else
									{
										npc6.isHide = false;
									}
									break;
								}
								num5 = num176;
							}
						}
					}
					else
					{
						Mob mob12 = null;
						try
						{
							mob12 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						}
						catch (Exception)
						{
						}
						bool flag323 = mob12 != null;
						if (flag323)
						{
							mob12.levelBoss = msg.reader().readByte();
							bool flag324 = mob12.levelBoss > 0;
							if (flag324)
							{
								mob12.typeSuperEff = Res.random(0, 3);
							}
						}
					}
				}
				else
				{
					switch (command2)
					{
					case -17:
						GameCanvas.debug("SA88", 2);
						global::Char.myCharz().meDead = true;
						global::Char.myCharz().cPk = msg.reader().readByte();
						global::Char.myCharz().startDie(msg.reader().readShort(), msg.reader().readShort());
						try
						{
							global::Char.myCharz().cPower = msg.reader().readLong();
							global::Char.myCharz().applyCharLevelPercent();
						}
						catch (Exception)
						{
							Cout.println("Loi tai ME_DIE " + msg.command);
						}
						global::Char.myCharz().countKill = 0;
						break;
					case -16:
					{
						GameCanvas.debug("SA90", 2);
						bool flag325 = global::Char.myCharz().wdx != 0 || global::Char.myCharz().wdy != 0;
						if (flag325)
						{
							global::Char.myCharz().cx = (int)global::Char.myCharz().wdx;
							global::Char.myCharz().cy = (int)global::Char.myCharz().wdy;
							global::Char.myCharz().wdx = (global::Char.myCharz().wdy = 0);
						}
						global::Char.myCharz().liveFromDead();
						global::Char.myCharz().isLockMove = false;
						global::Char.myCharz().meDead = false;
						break;
					}
					case -15:
					case -14:
					case -4:
						break;
					case -13:
					{
						GameCanvas.debug("SA82", 2);
						int num177 = (int)msg.reader().readUnsignedByte();
						bool flag326 = num177 > GameScr.vMob.size() - 1 || num177 < 0;
						if (flag326)
						{
							return;
						}
						Mob mob13 = (Mob)GameScr.vMob.elementAt(num177);
						mob13.sys = (int)msg.reader().readByte();
						mob13.levelBoss = msg.reader().readByte();
						bool flag327 = mob13.levelBoss != 0;
						if (flag327)
						{
							mob13.typeSuperEff = Res.random(0, 3);
						}
						mob13.x = mob13.xFirst;
						mob13.y = mob13.yFirst;
						mob13.status = 5;
						mob13.injureThenDie = false;
						mob13.hp = msg.reader().readInt();
						mob13.maxHp = mob13.hp;
						ServerEffect.addServerEffect(60, mob13.x, mob13.y, 1);
						break;
					}
					case -12:
					{
						Res.outz("SERVER SEND MOB DIE");
						GameCanvas.debug("SA85", 2);
						Mob mob14 = null;
						try
						{
							mob14 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						}
						catch (Exception)
						{
							Cout.println("LOi tai NPC_DIE cmd " + msg.command);
						}
						bool flag328 = mob14 == null || mob14.status == 0 || mob14.status == 0;
						if (!flag328)
						{
							mob14.startDie();
							try
							{
								int num178 = msg.readInt3Byte();
								bool flag329 = msg.reader().readBool();
								if (flag329)
								{
									GameScr.startFlyText("-" + num178, mob14.x, mob14.y - mob14.h, 0, -2, mFont.FATAL);
								}
								else
								{
									GameScr.startFlyText("-" + num178, mob14.x, mob14.y - mob14.h, 0, -2, mFont.ORANGE);
								}
								sbyte b67 = msg.reader().readByte();
								int num5;
								for (int num179 = 0; num179 < (int)b67; num179 = num5 + 1)
								{
									ItemMap itemMap3 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob14.x, mob14.y, (int)msg.reader().readShort(), (int)msg.reader().readShort());
									int num180 = itemMap3.playerId = msg.reader().readInt();
									Res.outz(string.Concat(new object[]
									{
										"playerid= ",
										num180,
										" my id= ",
										global::Char.myCharz().charID
									}));
									GameScr.vItemMap.addElement(itemMap3);
									bool flag330 = Res.abs(itemMap3.y - global::Char.myCharz().cy) < 24 && Res.abs(itemMap3.x - global::Char.myCharz().cx) < 24;
									if (flag330)
									{
										global::Char.myCharz().charFocus = null;
									}
									num5 = num179;
								}
							}
							catch (Exception ex8)
							{
								Cout.println(string.Concat(new object[]
								{
									"LOi tai NPC_DIE ",
									ex8.ToString(),
									" cmd ",
									msg.command
								}));
							}
						}
						break;
					}
					case -11:
					{
						GameCanvas.debug("SA86", 2);
						Mob mob15 = null;
						try
						{
							int index4 = (int)msg.reader().readUnsignedByte();
							mob15 = (Mob)GameScr.vMob.elementAt(index4);
						}
						catch (Exception)
						{
							Cout.println("Loi tai NPC_ATTACK_ME " + msg.command);
						}
						bool flag331 = mob15 != null;
						if (flag331)
						{
							global::Char.myCharz().isDie = false;
							global::Char.isLockKey = false;
							int num181 = msg.readInt3Byte();
							int num182;
							try
							{
								num182 = msg.readInt3Byte();
							}
							catch (Exception)
							{
								num182 = 0;
							}
							bool isBusyAttackSomeOne4 = mob15.isBusyAttackSomeOne;
							if (isBusyAttackSomeOne4)
							{
								global::Char.myCharz().doInjure(num181, num182, false, true);
							}
							else
							{
								mob15.dame = num181;
								mob15.dameMp = num182;
								mob15.setAttack(global::Char.myCharz());
							}
						}
						break;
					}
					case -10:
					{
						GameCanvas.debug("SA87", 2);
						Mob mob16 = null;
						try
						{
							mob16 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						}
						catch (Exception)
						{
						}
						GameCanvas.debug("SA87x1", 2);
						bool flag332 = mob16 != null;
						if (flag332)
						{
							GameCanvas.debug("SA87x2", 2);
							@char = GameScr.findCharInMap(msg.reader().readInt());
							bool flag333 = @char == null;
							if (flag333)
							{
								return;
							}
							GameCanvas.debug("SA87x3", 2);
							int num183 = msg.readInt3Byte();
							mob16.dame = @char.cHP - num183;
							@char.cHPNew = num183;
							GameCanvas.debug("SA87x4", 2);
							try
							{
								@char.cMP = msg.readInt3Byte();
							}
							catch (Exception)
							{
							}
							GameCanvas.debug("SA87x5", 2);
							bool isBusyAttackSomeOne5 = mob16.isBusyAttackSomeOne;
							if (isBusyAttackSomeOne5)
							{
								@char.doInjure(mob16.dame, 0, false, true);
							}
							else
							{
								mob16.setAttack(@char);
							}
							GameCanvas.debug("SA87x6", 2);
						}
						break;
					}
					case -9:
					{
						GameCanvas.debug("SA83", 2);
						Mob mob17 = null;
						try
						{
							mob17 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						}
						catch (Exception)
						{
						}
						GameCanvas.debug("SA83v1", 2);
						bool flag334 = mob17 != null;
						if (flag334)
						{
							mob17.hp = msg.readInt3Byte();
							int num184 = msg.readInt3Byte();
							bool flag335 = num184 == 1;
							if (flag335)
							{
								return;
							}
							bool flag336 = false;
							try
							{
								flag336 = msg.reader().readBoolean();
							}
							catch (Exception)
							{
							}
							sbyte b68 = msg.reader().readByte();
							bool flag337 = b68 != -1;
							if (flag337)
							{
								EffecMn.addEff(new Effect((int)b68, mob17.x, mob17.getY(), 3, 1, -1));
							}
							GameCanvas.debug("SA83v2", 2);
							bool flag338 = flag336;
							if (flag338)
							{
								GameScr.startFlyText("-" + num184, mob17.x, mob17.getY() - mob17.getH(), 0, -2, mFont.FATAL);
							}
							else
							{
								bool flag339 = num184 == 0;
								if (flag339)
								{
									mob17.x = mob17.xFirst;
									mob17.y = mob17.yFirst;
									GameScr.startFlyText(mResources.miss, mob17.x, mob17.getY() - mob17.getH(), 0, -2, mFont.MISS);
								}
								else
								{
									GameScr.startFlyText("-" + num184, mob17.x, mob17.getY() - mob17.getH(), 0, -2, mFont.ORANGE);
								}
							}
						}
						GameCanvas.debug("SA83v3", 2);
						break;
					}
					case -8:
					{
						GameCanvas.debug("SA89", 2);
						@char = GameScr.findCharInMap(msg.reader().readInt());
						bool flag340 = @char == null;
						if (flag340)
						{
							return;
						}
						@char.cPk = msg.reader().readByte();
						@char.waitToDie(msg.reader().readShort(), msg.reader().readShort());
						break;
					}
					case -7:
					{
						GameCanvas.debug("SA80", 2);
						int num185 = msg.reader().readInt();
						Cout.println("RECEVED MOVE OF " + num185);
						int num5;
						for (int num186 = 0; num186 < GameScr.vCharInMap.size(); num186 = num5 + 1)
						{
							global::Char char14 = null;
							try
							{
								char14 = (global::Char)GameScr.vCharInMap.elementAt(num186);
							}
							catch (Exception ex9)
							{
								Cout.println("Loi PLAYER_MOVE " + ex9.ToString());
							}
							bool flag341 = char14 == null;
							if (flag341)
							{
								break;
							}
							bool flag342 = char14.charID == num185;
							if (flag342)
							{
								GameCanvas.debug("SA8x2y" + num186, 2);
								char14.moveTo((int)msg.reader().readShort(), (int)msg.reader().readShort(), 0);
								char14.lastUpdateTime = mSystem.currentTimeMillis();
								break;
							}
							num5 = num186;
						}
						GameCanvas.debug("SA80x3", 2);
						break;
					}
					case -6:
					{
						GameCanvas.debug("SA81", 2);
						int num187 = msg.reader().readInt();
						int num5;
						for (int num188 = 0; num188 < GameScr.vCharInMap.size(); num188 = num5 + 1)
						{
							global::Char char15 = (global::Char)GameScr.vCharInMap.elementAt(num188);
							bool flag343 = char15 != null && char15.charID == num187;
							if (flag343)
							{
								bool flag344 = !char15.isInvisiblez && !char15.isUsePlane;
								if (flag344)
								{
									ServerEffect.addServerEffect(60, char15.cx, char15.cy, 1);
								}
								bool flag345 = !char15.isUsePlane;
								if (flag345)
								{
									GameScr.vCharInMap.removeElementAt(num188);
								}
								return;
							}
							num5 = num188;
						}
						break;
					}
					case -5:
					{
						GameCanvas.debug("SA79", 2);
						int charID = msg.reader().readInt();
						int num189 = msg.reader().readInt();
						bool flag346 = num189 != -100;
						global::Char char16;
						if (flag346)
						{
							char16 = new global::Char();
							char16.charID = charID;
							char16.clanID = num189;
						}
						else
						{
							char16 = new Mabu();
							char16.charID = charID;
							char16.clanID = num189;
						}
						bool flag347 = char16.clanID == -2;
						if (flag347)
						{
							char16.isCopy = true;
						}
						bool flag348 = this.readCharInfo(char16, msg);
						if (flag348)
						{
							sbyte b69 = msg.reader().readByte();
							bool flag349 = char16.cy <= 10 && b69 != 0 && b69 != 2;
							if (flag349)
							{
								Res.outz(string.Concat(new object[]
								{
									"nhân vật bay trên trời xuống x= ",
									char16.cx,
									" y= ",
									char16.cy
								}));
								Teleport teleport = new Teleport(char16.cx, char16.cy, char16.head, char16.cdir, 1, false, (b69 != 1) ? ((int)b69) : char16.cgender);
								teleport.id = char16.charID;
								char16.isTeleport = true;
								Teleport.addTeleport(teleport);
							}
							bool flag350 = b69 == 2;
							if (flag350)
							{
								char16.show();
							}
							int num5;
							for (int num190 = 0; num190 < GameScr.vMob.size(); num190 = num5 + 1)
							{
								Mob mob18 = (Mob)GameScr.vMob.elementAt(num190);
								bool flag351 = mob18 != null && mob18.isMobMe && mob18.mobId == char16.charID;
								if (flag351)
								{
									Res.outz("co 1 con quai");
									char16.mobMe = mob18;
									char16.mobMe.x = char16.cx;
									char16.mobMe.y = char16.cy - 40;
									break;
								}
								num5 = num190;
							}
							bool flag352 = GameScr.findCharInMap(char16.charID) == null;
							if (flag352)
							{
								GameScr.vCharInMap.addElement(char16);
							}
							char16.isMonkey = msg.reader().readByte();
							short num191 = msg.reader().readShort();
							Res.outz("mount id= " + num191 + "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
							bool flag353 = num191 != -1;
							if (flag353)
							{
								char16.isHaveMount = true;
								short num192 = num191;
								if (num192 <= 351)
								{
									if (num192 - 346 <= 2)
									{
										char16.isMountVip = false;
										goto IL_B979;
									}
									if (num192 - 349 <= 2)
									{
										char16.isMountVip = true;
										goto IL_B979;
									}
								}
								else
								{
									if (num192 == 396)
									{
										char16.isEventMount = true;
										goto IL_B979;
									}
									if (num192 == 532)
									{
										char16.isSpeacialMount = true;
										goto IL_B979;
									}
								}
								bool flag354 = num191 >= global::Char.ID_NEW_MOUNT;
								if (flag354)
								{
									char16.idMount = num191;
								}
								IL_B979:;
							}
							else
							{
								char16.isHaveMount = false;
							}
						}
						sbyte b70 = msg.reader().readByte();
						Res.outz("addplayer:   " + b70);
						char16.cFlag = b70;
						char16.isNhapThe = (msg.reader().readByte() == 1);
						try
						{
							char16.idAuraEff = msg.reader().readShort();
							char16.idEff_Set_Item = (short)msg.reader().readSByte();
							char16.idHat = msg.reader().readShort();
						}
						catch (Exception)
						{
						}
						GameScr.gI().getFlagImage(char16.charID, char16.cFlag);
						break;
					}
					case -3:
					{
						GameCanvas.debug("SA78", 2);
						sbyte b71 = msg.reader().readByte();
						int num193 = msg.reader().readInt();
						bool flag355 = b71 == 0;
						if (flag355)
						{
							global::Char char9 = global::Char.myCharz();
							char9.cPower += (long)num193;
						}
						bool flag356 = b71 == 1;
						if (flag356)
						{
							global::Char char9 = global::Char.myCharz();
							char9.cTiemNang += (long)num193;
						}
						bool flag357 = b71 == 2;
						if (flag357)
						{
							global::Char char9 = global::Char.myCharz();
							char9.cPower += (long)num193;
							char9 = global::Char.myCharz();
							char9.cTiemNang += (long)num193;
						}
						global::Char.myCharz().applyCharLevelPercent();
						bool flag358 = global::Char.myCharz().cTypePk != 3;
						if (flag358)
						{
							GameScr.startFlyText(((num193 <= 0) ? string.Empty : "+") + num193, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -4, mFont.GREEN);
							bool flag359 = num193 > 0 && global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5002;
							if (flag359)
							{
								ServerEffect.addServerEffect(55, global::Char.myCharz().petFollow.cmx, global::Char.myCharz().petFollow.cmy, 1);
								ServerEffect.addServerEffect(55, global::Char.myCharz().cx, global::Char.myCharz().cy, 1);
							}
						}
						break;
					}
					case -2:
					{
						GameCanvas.debug("SA77", 22);
						int num194 = msg.reader().readInt();
						global::Char char9 = global::Char.myCharz();
						char9.yen += num194;
						GameScr.startFlyText((num194 <= 0) ? (string.Empty + num194) : ("+" + num194), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
						break;
					}
					case -1:
					{
						GameCanvas.debug("SA77", 222);
						int num195 = msg.reader().readInt();
						global::Char char9 = global::Char.myCharz();
						char9.xu += (long)num195;
						global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
						char9 = global::Char.myCharz();
						char9.yen -= num195;
						GameScr.startFlyText("+" + num195, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
						break;
					}
					default:
						if (command2 != 18)
						{
							if (command2 == 19)
							{
								global::Char.myCharz().countKill = (int)msg.reader().readUnsignedShort();
								global::Char.myCharz().countKillMax = (int)msg.reader().readUnsignedShort();
							}
						}
						else
						{
							sbyte b72 = msg.reader().readByte();
							int num5;
							for (int num196 = 0; num196 < (int)b72; num196 = num5 + 1)
							{
								int charId = msg.reader().readInt();
								int cx = (int)msg.reader().readShort();
								int cy = (int)msg.reader().readShort();
								int cHPShow = msg.readInt3Byte();
								global::Char char17 = GameScr.findCharInMap(charId);
								bool flag360 = char17 != null;
								if (flag360)
								{
									char17.cx = cx;
									char17.cy = cy;
									char17.cHP = (char17.cHPShow = cHPShow);
									char17.lastUpdateTime = mSystem.currentTimeMillis();
								}
								num5 = num196;
							}
						}
						break;
					}
				}
			}
			else if (command2 <= 45)
			{
				if (command2 != 44)
				{
					if (command2 == 45)
					{
						GameCanvas.debug("SA84", 2);
						Mob mob19 = null;
						try
						{
							mob19 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						}
						catch (Exception ex10)
						{
							Cout.println("Loi tai NPC_MISS  " + ex10.ToString());
						}
						bool flag361 = mob19 != null;
						if (flag361)
						{
							mob19.hp = msg.reader().readInt();
							GameScr.startFlyText(mResources.miss, mob19.x, mob19.y - mob19.h, 0, -2, mFont.MISS);
						}
					}
				}
				else
				{
					GameCanvas.debug("SA91", 2);
					int num197 = msg.reader().readInt();
					string text15 = msg.reader().readUTF();
					Res.outz(string.Concat(new object[]
					{
						"user id= ",
						num197,
						" text= ",
						text15
					}));
					@char = ((global::Char.myCharz().charID != num197) ? GameScr.findCharInMap(num197) : global::Char.myCharz());
					bool flag362 = @char == null;
					if (flag362)
					{
						return;
					}
					@char.addInfo(text15);
				}
			}
			else if (command2 != 66)
			{
				if (command2 != 74)
				{
					switch (command2)
					{
					case 95:
					{
						GameCanvas.debug("SA77", 22);
						int num198 = msg.reader().readInt();
						global::Char char9 = global::Char.myCharz();
						char9.xu += (long)num198;
						global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
						GameScr.startFlyText((num198 <= 0) ? (string.Empty + num198) : ("+" + num198), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
						break;
					}
					case 96:
						GameCanvas.debug("SA77a", 22);
						global::Char.myCharz().taskOrders.addElement(new TaskOrder(msg.reader().readByte(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readByte()));
						break;
					case 97:
					{
						sbyte b73 = msg.reader().readByte();
						int num5;
						for (int num199 = 0; num199 < global::Char.myCharz().taskOrders.size(); num199 = num5 + 1)
						{
							TaskOrder taskOrder = (TaskOrder)global::Char.myCharz().taskOrders.elementAt(num199);
							bool flag363 = taskOrder.taskId == (int)b73;
							if (flag363)
							{
								taskOrder.count = (int)msg.reader().readShort();
								break;
							}
							num5 = num199;
						}
						break;
					}
					}
				}
				else
				{
					GameCanvas.debug("SA85", 2);
					Mob mob20 = null;
					try
					{
						mob20 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
					}
					catch (Exception)
					{
						Cout.println("Loi tai NPC CHANGE " + msg.command);
					}
					bool flag364 = mob20 != null && mob20.status != 0 && mob20.status != 0;
					if (flag364)
					{
						mob20.status = 0;
						ServerEffect.addServerEffect(60, mob20.x, mob20.y, 1);
						ItemMap itemMap4 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob20.x, mob20.y, (int)msg.reader().readShort(), (int)msg.reader().readShort());
						GameScr.vItemMap.addElement(itemMap4);
						bool flag365 = Res.abs(itemMap4.y - global::Char.myCharz().cy) < 24 && Res.abs(itemMap4.x - global::Char.myCharz().cx) < 24;
						if (flag365)
						{
							global::Char.myCharz().charFocus = null;
						}
					}
				}
			}
			else
			{
				Res.outz("ME DIE XP DOWN NOT IMPLEMENT YET!!!!!!!!!!!!!!!!!!!!!!!!!!");
			}
			GameCanvas.debug("SA92", 2);
		}
		catch (Exception)
		{
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0002C7E8 File Offset: 0x0002A9E8
	private void createItem(myReader d)
	{
		GameScr.vcItem = d.readByte();
		ItemTemplates.itemTemplates.clear();
		GameScr.gI().iOptionTemplates = new ItemOptionTemplate[(int)d.readUnsignedByte()];
		for (int i = 0; i < GameScr.gI().iOptionTemplates.Length; i++)
		{
			GameScr.gI().iOptionTemplates[i] = new ItemOptionTemplate();
			GameScr.gI().iOptionTemplates[i].id = i;
			GameScr.gI().iOptionTemplates[i].name = d.readUTF();
			GameScr.gI().iOptionTemplates[i].type = (int)d.readByte();
		}
		int num = (int)d.readShort();
		for (int j = 0; j < num; j++)
		{
			ItemTemplate it = new ItemTemplate((short)j, d.readByte(), d.readByte(), d.readUTF(), d.readUTF(), d.readByte(), d.readInt(), d.readShort(), d.readShort(), d.readBool());
			ItemTemplates.add(it);
		}
	}

	// Token: 0x06000181 RID: 385 RVA: 0x0002C8F4 File Offset: 0x0002AAF4
	private void createSkill(myReader d)
	{
		GameScr.vcSkill = d.readByte();
		GameScr.gI().sOptionTemplates = new SkillOptionTemplate[(int)d.readByte()];
		for (int i = 0; i < GameScr.gI().sOptionTemplates.Length; i++)
		{
			GameScr.gI().sOptionTemplates[i] = new SkillOptionTemplate();
			GameScr.gI().sOptionTemplates[i].id = i;
			GameScr.gI().sOptionTemplates[i].name = d.readUTF();
		}
		GameScr.nClasss = new NClass[(int)d.readByte()];
		for (int j = 0; j < GameScr.nClasss.Length; j++)
		{
			GameScr.nClasss[j] = new NClass();
			GameScr.nClasss[j].classId = j;
			GameScr.nClasss[j].name = d.readUTF();
			GameScr.nClasss[j].skillTemplates = new SkillTemplate[(int)d.readByte()];
			for (int k = 0; k < GameScr.nClasss[j].skillTemplates.Length; k++)
			{
				GameScr.nClasss[j].skillTemplates[k] = new SkillTemplate();
				GameScr.nClasss[j].skillTemplates[k].id = d.readByte();
				GameScr.nClasss[j].skillTemplates[k].name = d.readUTF();
				GameScr.nClasss[j].skillTemplates[k].maxPoint = (int)d.readByte();
				GameScr.nClasss[j].skillTemplates[k].manaUseType = (int)d.readByte();
				GameScr.nClasss[j].skillTemplates[k].type = (int)d.readByte();
				GameScr.nClasss[j].skillTemplates[k].iconId = (int)d.readShort();
				GameScr.nClasss[j].skillTemplates[k].damInfo = d.readUTF();
				int lineWidth = 130;
				bool flag = GameCanvas.w == 128 || GameCanvas.h <= 208;
				if (flag)
				{
					lineWidth = 100;
				}
				GameScr.nClasss[j].skillTemplates[k].description = mFont.tahoma_7_green2.splitFontArray(d.readUTF(), lineWidth);
				GameScr.nClasss[j].skillTemplates[k].skills = new Skill[(int)d.readByte()];
				for (int l = 0; l < GameScr.nClasss[j].skillTemplates[k].skills.Length; l++)
				{
					GameScr.nClasss[j].skillTemplates[k].skills[l] = new Skill();
					GameScr.nClasss[j].skillTemplates[k].skills[l].skillId = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].template = GameScr.nClasss[j].skillTemplates[k];
					GameScr.nClasss[j].skillTemplates[k].skills[l].point = (int)d.readByte();
					GameScr.nClasss[j].skillTemplates[k].skills[l].powRequire = d.readLong();
					GameScr.nClasss[j].skillTemplates[k].skills[l].manaUse = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].coolDown = d.readInt();
					GameScr.nClasss[j].skillTemplates[k].skills[l].dx = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].dy = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].maxFight = (int)d.readByte();
					GameScr.nClasss[j].skillTemplates[k].skills[l].damage = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].price = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].moreInfo = d.readUTF();
					Skills.add(GameScr.nClasss[j].skillTemplates[k].skills[l]);
				}
			}
		}
	}

	// Token: 0x06000182 RID: 386 RVA: 0x0002CD48 File Offset: 0x0002AF48
	private void createMap(myReader d)
	{
		GameScr.vcMap = d.readByte();
		TileMap.mapNames = new string[(int)d.readUnsignedByte()];
		for (int i = 0; i < TileMap.mapNames.Length; i++)
		{
			TileMap.mapNames[i] = d.readUTF();
		}
		Npc.arrNpcTemplate = new NpcTemplate[(int)d.readByte()];
		sbyte b = 0;
		while ((int)b < Npc.arrNpcTemplate.Length)
		{
			Npc.arrNpcTemplate[(int)b] = new NpcTemplate();
			Npc.arrNpcTemplate[(int)b].npcTemplateId = (int)b;
			Npc.arrNpcTemplate[(int)b].name = d.readUTF();
			Npc.arrNpcTemplate[(int)b].headId = (int)d.readShort();
			Npc.arrNpcTemplate[(int)b].bodyId = (int)d.readShort();
			Npc.arrNpcTemplate[(int)b].legId = (int)d.readShort();
			Npc.arrNpcTemplate[(int)b].menu = new string[(int)d.readByte()][];
			for (int j = 0; j < Npc.arrNpcTemplate[(int)b].menu.Length; j++)
			{
				Npc.arrNpcTemplate[(int)b].menu[j] = new string[(int)d.readByte()];
				for (int k = 0; k < Npc.arrNpcTemplate[(int)b].menu[j].Length; k++)
				{
					Npc.arrNpcTemplate[(int)b].menu[j][k] = d.readUTF();
				}
			}
			b += 1;
		}
		Mob.arrMobTemplate = new MobTemplate[(int)d.readByte()];
		sbyte b2 = 0;
		while ((int)b2 < Mob.arrMobTemplate.Length)
		{
			Mob.arrMobTemplate[(int)b2] = new MobTemplate();
			Mob.arrMobTemplate[(int)b2].mobTemplateId = b2;
			Mob.arrMobTemplate[(int)b2].type = d.readByte();
			Mob.arrMobTemplate[(int)b2].name = d.readUTF();
			Mob.arrMobTemplate[(int)b2].hp = d.readInt();
			Mob.arrMobTemplate[(int)b2].rangeMove = d.readByte();
			Mob.arrMobTemplate[(int)b2].speed = d.readByte();
			Mob.arrMobTemplate[(int)b2].dartType = d.readByte();
			b2 += 1;
		}
	}

	// Token: 0x06000183 RID: 387 RVA: 0x0002CF7C File Offset: 0x0002B17C
	private void createData(myReader d, bool isSaveRMS)
	{
		GameScr.vcData = d.readByte();
		if (isSaveRMS)
		{
			Rms.saveRMS("NR_dart", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_arrow", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_effect", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_image", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_part", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_skill", NinjaUtil.readByteArray(d));
			Rms.DeleteStorage("NRdata");
		}
	}

	// Token: 0x06000184 RID: 388 RVA: 0x0002D010 File Offset: 0x0002B210
	private Image createImage(sbyte[] arr)
	{
		try
		{
			return Image.createImage(arr, 0, arr.Length);
		}
		catch (Exception)
		{
		}
		return null;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0002D048 File Offset: 0x0002B248
	public int[] arrayByte2Int(sbyte[] b)
	{
		int[] array = new int[b.Length];
		for (int i = 0; i < b.Length; i++)
		{
			int num = (int)b[i];
			bool flag = num < 0;
			if (flag)
			{
				num += 256;
			}
			array[i] = num;
		}
		return array;
	}

	// Token: 0x06000186 RID: 390 RVA: 0x0002D098 File Offset: 0x0002B298
	public void readClanMsg(Message msg, int index)
	{
		try
		{
			ClanMessage clanMessage = new ClanMessage();
			sbyte b = msg.reader().readByte();
			clanMessage.type = (int)b;
			clanMessage.id = msg.reader().readInt();
			clanMessage.playerId = msg.reader().readInt();
			clanMessage.playerName = msg.reader().readUTF();
			clanMessage.role = msg.reader().readByte();
			clanMessage.time = (long)(msg.reader().readInt() + 1000000000);
			bool flag = false;
			GameScr.isNewClanMessage = false;
			bool flag2 = b == 0;
			if (flag2)
			{
				string text = msg.reader().readUTF();
				GameScr.isNewClanMessage = true;
				bool flag3 = mFont.tahoma_7.getWidth(text) > Panel.WIDTH_PANEL - 60;
				if (flag3)
				{
					clanMessage.chat = mFont.tahoma_7.splitFontArray(text, Panel.WIDTH_PANEL - 10);
				}
				else
				{
					clanMessage.chat = new string[1];
					clanMessage.chat[0] = text;
				}
				clanMessage.color = msg.reader().readByte();
			}
			else
			{
				bool flag4 = b == 1;
				if (flag4)
				{
					clanMessage.recieve = (int)msg.reader().readByte();
					clanMessage.maxCap = (int)msg.reader().readByte();
					flag = (msg.reader().readByte() == 1);
					bool flag5 = flag;
					if (flag5)
					{
						GameScr.isNewClanMessage = true;
					}
					bool flag6 = clanMessage.playerId != global::Char.myCharz().charID;
					if (flag6)
					{
						bool flag7 = clanMessage.recieve < clanMessage.maxCap;
						if (flag7)
						{
							clanMessage.option = new string[]
							{
								mResources.donate
							};
						}
						else
						{
							clanMessage.option = null;
						}
					}
					bool flag8 = GameCanvas.panel.cp != null;
					if (flag8)
					{
						GameCanvas.panel.updateRequest(clanMessage.recieve, clanMessage.maxCap);
					}
				}
				else
				{
					bool flag9 = b == 2 && global::Char.myCharz().role == 0;
					if (flag9)
					{
						GameScr.isNewClanMessage = true;
						clanMessage.option = new string[]
						{
							mResources.CANCEL,
							mResources.receive
						};
					}
				}
			}
			bool flag10 = GameCanvas.currentScreen != GameScr.instance;
			if (flag10)
			{
				GameScr.isNewClanMessage = false;
			}
			else
			{
				bool flag11 = GameCanvas.panel.isShow && GameCanvas.panel.type == 0 && GameCanvas.panel.currentTabIndex == 3;
				if (flag11)
				{
					GameScr.isNewClanMessage = false;
				}
			}
			ClanMessage.addMessage(clanMessage, index, flag);
		}
		catch (Exception)
		{
			Cout.println("LOI TAI CMD -= " + msg.command);
		}
	}

	// Token: 0x06000187 RID: 391 RVA: 0x0002D358 File Offset: 0x0002B558
	public void loadCurrMap(sbyte teleport3)
	{
		Res.outz("is loading map = " + global::Char.isLoadingMap.ToString());
		GameScr.gI().auto = 0;
		GameScr.isChangeZone = false;
		CreateCharScr.instance = null;
		GameScr.info1.isUpdate = false;
		GameScr.info2.isUpdate = false;
		GameScr.lockTick = 0;
		GameCanvas.panel.isShow = false;
		SoundMn.gI().stopAll();
		bool flag = !GameScr.isLoadAllData && !CreateCharScr.isCreateChar;
		if (flag)
		{
			GameScr.gI().initSelectChar();
		}
		GameScr.loadCamera(false, (teleport3 != 1) ? -1 : global::Char.myCharz().cx, (teleport3 == 0) ? -1 : 0);
		TileMap.loadMainTile();
		TileMap.loadMap(TileMap.tileID);
		Res.outz("LOAD GAMESCR 2");
		global::Char.myCharz().cvx = 0;
		global::Char.myCharz().statusMe = 4;
		global::Char.myCharz().currentMovePoint = null;
		global::Char.myCharz().mobFocus = null;
		global::Char.myCharz().charFocus = null;
		global::Char.myCharz().npcFocus = null;
		global::Char.myCharz().itemFocus = null;
		global::Char.myCharz().skillPaint = null;
		global::Char.myCharz().setMabuHold(false);
		global::Char.myCharz().skillPaintRandomPaint = null;
		GameCanvas.clearAllPointerEvent();
		bool flag2 = global::Char.myCharz().cy >= TileMap.pxh - 100;
		if (flag2)
		{
			global::Char.myCharz().isFlyUp = true;
			global::Char.myCharz().cx += Res.abs(Res.random(0, 80));
			Service.gI().charMove();
		}
		GameScr.gI().loadGameScr();
		GameCanvas.loadBG(TileMap.bgID);
		global::Char.isLockKey = false;
		Res.outz("cy= " + global::Char.myCharz().cy + "---------------------------------------------");
		for (int i = 0; i < global::Char.myCharz().vEff.size(); i++)
		{
			EffectChar effectChar = (EffectChar)global::Char.myCharz().vEff.elementAt(i);
			bool flag3 = effectChar.template.type == 10;
			if (flag3)
			{
				global::Char.isLockKey = true;
				break;
			}
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
		GameScr.gI().dHP = global::Char.myCharz().cHP;
		GameScr.gI().dMP = global::Char.myCharz().cMP;
		global::Char.ischangingMap = false;
		GameScr.gI().switchToMe();
		bool flag4 = global::Char.myCharz().cy <= 10 && teleport3 != 0 && teleport3 != 2;
		if (flag4)
		{
			Teleport p = new Teleport(global::Char.myCharz().cx, global::Char.myCharz().cy, global::Char.myCharz().head, global::Char.myCharz().cdir, 1, true, (teleport3 != 1) ? ((int)teleport3) : global::Char.myCharz().cgender);
			Teleport.addTeleport(p);
			global::Char.myCharz().isTeleport = true;
		}
		bool flag5 = teleport3 == 2;
		if (flag5)
		{
			global::Char.myCharz().show();
		}
		bool isRongThanXuatHien = GameScr.gI().isRongThanXuatHien;
		if (isRongThanXuatHien)
		{
			bool flag6 = TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID;
			if (flag6)
			{
				GameScr.gI().callRongThan(GameScr.gI().xR, GameScr.gI().yR);
			}
			bool flag7 = mGraphics.zoomLevel > 1;
			if (flag7)
			{
				GameScr.gI().doiMauTroi();
			}
		}
		InfoDlg.hide();
		InfoDlg.show(TileMap.mapName, mResources.zone + " " + TileMap.zoneID, 30);
		GameCanvas.endDlg();
		GameCanvas.isLoading = false;
		Hint.clickMob();
		Hint.clickNpc();
		GameCanvas.debug("SA75x9", 2);
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0002D720 File Offset: 0x0002B920
	public void loadInfoMap(Message msg)
	{
		try
		{
			bool flag = mGraphics.zoomLevel == 1;
			if (flag)
			{
				SmallImage.clearHastable();
			}
			global::Char.myCharz().cx = (global::Char.myCharz().cxSend = (global::Char.myCharz().cxFocus = (int)msg.reader().readShort()));
			global::Char.myCharz().cy = (global::Char.myCharz().cySend = (global::Char.myCharz().cyFocus = (int)msg.reader().readShort()));
			global::Char.myCharz().xSd = global::Char.myCharz().cx;
			global::Char.myCharz().ySd = global::Char.myCharz().cy;
			Res.outz(string.Concat(new object[]
			{
				"head= ",
				global::Char.myCharz().head,
				" body= ",
				global::Char.myCharz().body,
				" left= ",
				global::Char.myCharz().leg,
				" x= ",
				global::Char.myCharz().cx,
				" y= ",
				global::Char.myCharz().cy,
				" chung toc= ",
				global::Char.myCharz().cgender
			}));
			bool flag2 = global::Char.myCharz().cx >= 0 && global::Char.myCharz().cx <= 100;
			if (flag2)
			{
				global::Char.myCharz().cdir = 1;
			}
			else
			{
				bool flag3 = global::Char.myCharz().cx >= TileMap.tmw - 100 && global::Char.myCharz().cx <= TileMap.tmw;
				if (flag3)
				{
					global::Char.myCharz().cdir = -1;
				}
			}
			GameCanvas.debug("SA75x4", 2);
			int num = (int)msg.reader().readByte();
			Res.outz("vGo size= " + num);
			bool flag4 = !GameScr.info1.isDone;
			if (flag4)
			{
				GameScr.info1.cmx = global::Char.myCharz().cx - GameScr.cmx;
				GameScr.info1.cmy = global::Char.myCharz().cy - GameScr.cmy;
			}
			for (int i = 0; i < num; i++)
			{
				Waypoint waypoint = new Waypoint(msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readUTF());
				bool flag5 = (TileMap.mapID != 21 && TileMap.mapID != 22 && TileMap.mapID != 23) || waypoint.minX < 0 || waypoint.minX <= 24;
				if (flag5)
				{
				}
			}
			Resources.UnloadUnusedAssets();
			GC.Collect();
			GameCanvas.debug("SA75x5", 2);
			num = (int)msg.reader().readByte();
			Mob.newMob.removeAllElements();
			sbyte b = 0;
			while ((int)b < num)
			{
				Mob mob = new Mob((int)b, msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), (int)msg.reader().readByte(), (int)msg.reader().readByte(), msg.reader().readInt(), msg.reader().readByte(), msg.reader().readInt(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readByte(), msg.reader().readByte());
				mob.xSd = mob.x;
				mob.ySd = mob.y;
				mob.isBoss = msg.reader().readBoolean();
				bool flag6 = Mob.arrMobTemplate[mob.templateId].type != 0;
				if (flag6)
				{
					bool flag7 = b % 3 == 0;
					if (flag7)
					{
						mob.dir = -1;
					}
					else
					{
						mob.dir = 1;
					}
					mob.x += (int)(10 - b % 20);
				}
				mob.isMobMe = false;
				BigBoss bigBoss = null;
				BachTuoc bachTuoc = null;
				BigBoss2 bigBoss2 = null;
				NewBoss newBoss = null;
				bool flag8 = mob.templateId == 70;
				if (flag8)
				{
					bigBoss = new BigBoss((int)b, (short)mob.x, (short)mob.y, 70, mob.hp, mob.maxHp, mob.sys);
				}
				bool flag9 = mob.templateId == 71;
				if (flag9)
				{
					bachTuoc = new BachTuoc((int)b, (short)mob.x, (short)mob.y, 71, mob.hp, mob.maxHp, mob.sys);
				}
				bool flag10 = mob.templateId == 72;
				if (flag10)
				{
					bigBoss2 = new BigBoss2((int)b, (short)mob.x, (short)mob.y, 72, mob.hp, mob.maxHp, 3);
				}
				bool isBoss = mob.isBoss;
				if (isBoss)
				{
					newBoss = new NewBoss((int)b, (short)mob.x, (short)mob.y, mob.templateId, mob.hp, mob.maxHp, mob.sys);
				}
				bool flag11 = newBoss != null;
				if (flag11)
				{
					GameScr.vMob.addElement(newBoss);
				}
				else
				{
					bool flag12 = bigBoss != null;
					if (flag12)
					{
						GameScr.vMob.addElement(bigBoss);
					}
					else
					{
						bool flag13 = bachTuoc != null;
						if (flag13)
						{
							GameScr.vMob.addElement(bachTuoc);
						}
						else
						{
							bool flag14 = bigBoss2 != null;
							if (flag14)
							{
								GameScr.vMob.addElement(bigBoss2);
							}
							else
							{
								GameScr.vMob.addElement(mob);
							}
						}
					}
				}
				b += 1;
			}
			for (int j = 0; j < Mob.lastMob.size(); j++)
			{
				string text = (string)Mob.lastMob.elementAt(j);
				bool flag15 = !Mob.isExistNewMob(text);
				if (flag15)
				{
					Mob.arrMobTemplate[int.Parse(text)].data = null;
					Mob.lastMob.removeElementAt(j);
					j--;
				}
			}
			bool flag16 = global::Char.myCharz().mobMe != null && GameScr.findMobInMap(global::Char.myCharz().mobMe.mobId) == null;
			if (flag16)
			{
				global::Char.myCharz().mobMe.getData();
				global::Char.myCharz().mobMe.x = global::Char.myCharz().cx;
				global::Char.myCharz().mobMe.y = global::Char.myCharz().cy - 40;
				GameScr.vMob.addElement(global::Char.myCharz().mobMe);
			}
			num = (int)msg.reader().readByte();
			byte b2 = 0;
			while ((int)b2 < num)
			{
				b2 += 1;
			}
			GameCanvas.debug("SA75x6", 2);
			num = (int)msg.reader().readByte();
			Res.outz("NPC size= " + num);
			for (int k = 0; k < num; k++)
			{
				sbyte status = msg.reader().readByte();
				short cx = msg.reader().readShort();
				short num2 = msg.reader().readShort();
				sbyte b3 = msg.reader().readByte();
				short num3 = msg.reader().readShort();
				bool flag17 = b3 != 6 && ((global::Char.myCharz().taskMaint.taskId >= 7 && (global::Char.myCharz().taskMaint.taskId != 7 || global::Char.myCharz().taskMaint.index > 1)) || (b3 != 7 && b3 != 8 && b3 != 9)) && (global::Char.myCharz().taskMaint.taskId >= 6 || b3 != 16);
				if (flag17)
				{
					bool flag18 = b3 == 4;
					if (flag18)
					{
						GameScr.gI().magicTree = new MagicTree(k, (int)status, (int)cx, (int)num2, (int)b3, (int)num3);
						Service.gI().magicTree(2);
						GameScr.vNpc.addElement(GameScr.gI().magicTree);
					}
					else
					{
						Npc o = new Npc(k, (int)status, (int)cx, (int)(num2 + 3), (int)b3, (int)num3);
						GameScr.vNpc.addElement(o);
					}
				}
			}
			GameCanvas.debug("SA75x7", 2);
			num = (int)msg.reader().readByte();
			Res.outz("item size = " + num);
			for (int l = 0; l < num; l++)
			{
				short itemMapID = msg.reader().readShort();
				short itemTemplateID = msg.reader().readShort();
				int x = (int)msg.reader().readShort();
				int y = (int)msg.reader().readShort();
				int num4 = msg.reader().readInt();
				short r = 0;
				bool flag19 = num4 == -2;
				if (flag19)
				{
					r = msg.reader().readShort();
				}
				ItemMap itemMap = new ItemMap(num4, itemMapID, itemTemplateID, x, y, r);
				bool flag20 = false;
				for (int m = 0; m < GameScr.vItemMap.size(); m++)
				{
					ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(m);
					bool flag21 = itemMap2.itemMapID == itemMap.itemMapID;
					if (flag21)
					{
						flag20 = true;
						break;
					}
				}
				bool flag22 = !flag20;
				if (flag22)
				{
					GameScr.vItemMap.addElement(itemMap);
				}
			}
			TileMap.vCurrItem.removeAllElements();
			bool flag23 = mGraphics.zoomLevel == 1;
			if (flag23)
			{
				BgItem.clearHashTable();
			}
			BgItem.vKeysNew.removeAllElements();
			bool flag24 = !GameCanvas.lowGraphic || (GameCanvas.lowGraphic && TileMap.isVoDaiMap()) || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 47 || TileMap.mapID == 48;
			if (flag24)
			{
				short num5 = msg.reader().readShort();
				Res.outz("nItem= " + num5);
				for (int n = 0; n < (int)num5; n++)
				{
					short id = msg.reader().readShort();
					short num6 = msg.reader().readShort();
					short num7 = msg.reader().readShort();
					bool flag25 = TileMap.getBIById((int)id) == null;
					if (!flag25)
					{
						BgItem bibyId = TileMap.getBIById((int)id);
						BgItem bgItem = new BgItem();
						bgItem.id = (int)id;
						bgItem.idImage = bibyId.idImage;
						bgItem.dx = bibyId.dx;
						bgItem.dy = bibyId.dy;
						bgItem.x = (int)(num6 * (short)TileMap.size);
						bgItem.y = (int)(num7 * (short)TileMap.size);
						bgItem.layer = bibyId.layer;
						bool flag26 = TileMap.isExistMoreOne(bgItem.id);
						if (flag26)
						{
							bgItem.trans = ((n % 2 != 0) ? 2 : 0);
							bool flag27 = TileMap.mapID == 45;
							if (flag27)
							{
								bgItem.trans = 0;
							}
						}
						bool flag28 = !BgItem.imgNew.containsKey(bgItem.idImage + string.Empty);
						if (flag28)
						{
							bool flag29 = mGraphics.zoomLevel == 1;
							if (flag29)
							{
								Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
								bool flag30 = image == null;
								if (flag30)
								{
									image = Image.createRGBImage(new int[1], 1, 1, true);
									Service.gI().getBgTemplate(bgItem.idImage);
								}
								BgItem.imgNew.put(bgItem.idImage + string.Empty, image);
							}
							else
							{
								bool flag31 = false;
								sbyte[] array = Rms.loadRMS(mGraphics.zoomLevel + "bgItem" + bgItem.idImage);
								bool flag32 = array != null;
								if (flag32)
								{
									bool flag33 = BgItem.newSmallVersion != null;
									if (flag33)
									{
										Res.outz(string.Concat(new object[]
										{
											"Small  last= ",
											array.Length % 127,
											"new Version= ",
											BgItem.newSmallVersion[(int)bgItem.idImage]
										}));
										bool flag34 = array.Length % 127 != (int)BgItem.newSmallVersion[(int)bgItem.idImage];
										if (flag34)
										{
											flag31 = true;
										}
									}
									bool flag35 = !flag31;
									if (flag35)
									{
										Image image = Image.createImage(array, 0, array.Length);
										bool flag36 = image != null;
										if (flag36)
										{
											BgItem.imgNew.put(bgItem.idImage + string.Empty, image);
										}
										else
										{
											flag31 = true;
										}
									}
								}
								else
								{
									flag31 = true;
								}
								bool flag37 = flag31;
								if (flag37)
								{
									Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
									bool flag38 = image == null;
									if (flag38)
									{
										image = Image.createRGBImage(new int[1], 1, 1, true);
										Service.gI().getBgTemplate(bgItem.idImage);
									}
									BgItem.imgNew.put(bgItem.idImage + string.Empty, image);
								}
							}
							BgItem.vKeysLast.addElement(bgItem.idImage + string.Empty);
						}
						bool flag39 = !BgItem.isExistKeyNews(bgItem.idImage + string.Empty);
						if (flag39)
						{
							BgItem.vKeysNew.addElement(bgItem.idImage + string.Empty);
						}
						bgItem.changeColor();
						TileMap.vCurrItem.addElement(bgItem);
					}
				}
				for (int num8 = 0; num8 < BgItem.vKeysLast.size(); num8++)
				{
					string text2 = (string)BgItem.vKeysLast.elementAt(num8);
					bool flag40 = !BgItem.isExistKeyNews(text2);
					if (flag40)
					{
						BgItem.imgNew.remove(text2);
						bool flag41 = BgItem.imgNew.containsKey(text2 + "blend" + 1);
						if (flag41)
						{
							BgItem.imgNew.remove(text2 + "blend" + 1);
						}
						bool flag42 = BgItem.imgNew.containsKey(text2 + "blend" + 3);
						if (flag42)
						{
							BgItem.imgNew.remove(text2 + "blend" + 3);
						}
						BgItem.vKeysLast.removeElementAt(num8);
						num8--;
					}
				}
				BackgroudEffect.isFog = false;
				BackgroudEffect.nCloud = 0;
				EffecMn.vEff.removeAllElements();
				BackgroudEffect.vBgEffect.removeAllElements();
				Effect.newEff.removeAllElements();
				short num9 = msg.reader().readShort();
				for (int num10 = 0; num10 < (int)num9; num10++)
				{
					string key = msg.reader().readUTF();
					string value = msg.reader().readUTF();
					this.keyValueAction(key, value);
				}
				for (int num11 = 0; num11 < Effect.lastEff.size(); num11++)
				{
					string text3 = (string)Effect.lastEff.elementAt(num11);
					bool flag43 = !Effect.isExistNewEff(text3);
					if (flag43)
					{
						Effect.removeEffData(int.Parse(text3));
						Effect.lastEff.removeElementAt(num11);
						num11--;
					}
				}
			}
			else
			{
				short num12 = msg.reader().readShort();
				for (int num13 = 0; num13 < (int)num12; num13++)
				{
					short num14 = msg.reader().readShort();
					short num15 = msg.reader().readShort();
					short num16 = msg.reader().readShort();
				}
				short num17 = msg.reader().readShort();
				for (int num18 = 0; num18 < (int)num17; num18++)
				{
					string text4 = msg.reader().readUTF();
					string text5 = msg.reader().readUTF();
				}
			}
			TileMap.bgType = (int)msg.reader().readByte();
			sbyte teleport = msg.reader().readByte();
			this.loadCurrMap(teleport);
			global::Char.isLoadingMap = false;
			GameCanvas.debug("SA75x8", 2);
			Resources.UnloadUnusedAssets();
			GC.Collect();
			Cout.LogError("----------DA CHAY XONG LOAD INFO MAP");
			VuDang.isVaoKhu = false;
			VuDang.canUpdate = true;
		}
		catch (Exception ex)
		{
			Pk9rXmap.FixBlackScreen();
			Cout.LogError("LOI TAI LOADMAP INFO " + ex.ToString());
		}
	}

	// Token: 0x06000189 RID: 393 RVA: 0x0002E83C File Offset: 0x0002CA3C
	public void keyValueAction(string key, string value)
	{
		bool flag = key.Equals("eff");
		if (flag)
		{
			bool flag2 = Panel.graphics > 0;
			if (!flag2)
			{
				string[] array = Res.split(value, ".", 0);
				int id = int.Parse(array[0]);
				int layer = int.Parse(array[1]);
				int x = int.Parse(array[2]);
				int y = int.Parse(array[3]);
				bool flag3 = array.Length <= 4;
				int loop;
				int loopCount;
				if (flag3)
				{
					loop = -1;
					loopCount = 1;
				}
				else
				{
					loop = int.Parse(array[4]);
					loopCount = int.Parse(array[5]);
				}
				Effect effect = new Effect(id, x, y, layer, loop, loopCount);
				bool flag4 = array.Length > 6;
				if (flag4)
				{
					effect.typeEff = int.Parse(array[6]);
					bool flag5 = array.Length > 7;
					if (flag5)
					{
						effect.indexFrom = int.Parse(array[7]);
						effect.indexTo = int.Parse(array[8]);
					}
				}
				EffecMn.addEff(effect);
			}
		}
		else
		{
			bool flag6 = key.Equals("beff") && Panel.graphics <= 1;
			if (flag6)
			{
				BackgroudEffect.addEffect(int.Parse(value));
			}
		}
	}

	// Token: 0x0600018A RID: 394 RVA: 0x0002E968 File Offset: 0x0002CB68
	public void messageNotMap(Message msg)
	{
		GameCanvas.debug("SA6", 2);
		try
		{
			sbyte b = msg.reader().readByte();
			mSystem.LogCMD("---messageNotMap : " + b);
			sbyte b2 = b;
			switch (b2)
			{
			case 4:
			{
				GameCanvas.debug("SA8", 2);
				GameCanvas.loginScr.savePass();
				GameScr.isAutoPlay = false;
				GameScr.canAutoPlay = false;
				LoginScr.isUpdateAll = true;
				LoginScr.isUpdateData = true;
				LoginScr.isUpdateMap = true;
				LoginScr.isUpdateSkill = true;
				LoginScr.isUpdateItem = true;
				GameScr.vsData = msg.reader().readByte();
				GameScr.vsMap = msg.reader().readByte();
				GameScr.vsSkill = msg.reader().readByte();
				GameScr.vsItem = msg.reader().readByte();
				sbyte b3 = msg.reader().readByte();
				bool isLogin = GameCanvas.loginScr.isLogin2;
				if (isLogin)
				{
					Rms.saveRMSString("acc", string.Empty);
					Rms.saveRMSString("pass", string.Empty);
				}
				else
				{
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, string.Empty);
				}
				bool flag = GameScr.vsData != GameScr.vcData;
				if (flag)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateData();
				}
				else
				{
					try
					{
						LoginScr.isUpdateData = false;
					}
					catch (Exception)
					{
						GameScr.vcData = -1;
						Service.gI().updateData();
					}
				}
				bool flag2 = GameScr.vsMap != GameScr.vcMap;
				if (flag2)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateMap();
				}
				else
				{
					try
					{
						bool flag3 = !GameScr.isLoadAllData;
						if (flag3)
						{
							DataInputStream dataInputStream = new DataInputStream(Rms.loadRMS("NRmap"));
							this.createMap(dataInputStream.r);
						}
						LoginScr.isUpdateMap = false;
					}
					catch (Exception)
					{
						GameScr.vcMap = -1;
						Service.gI().updateMap();
					}
				}
				bool flag4 = GameScr.vsSkill != GameScr.vcSkill;
				if (flag4)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateSkill();
				}
				else
				{
					try
					{
						bool flag5 = !GameScr.isLoadAllData;
						if (flag5)
						{
							DataInputStream dataInputStream2 = new DataInputStream(Rms.loadRMS("NRskill"));
							this.createSkill(dataInputStream2.r);
						}
						LoginScr.isUpdateSkill = false;
					}
					catch (Exception)
					{
						GameScr.vcSkill = -1;
						Service.gI().updateSkill();
					}
				}
				bool flag6 = GameScr.vsItem != GameScr.vcItem;
				if (flag6)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateItem();
				}
				else
				{
					try
					{
						DataInputStream dataInputStream3 = new DataInputStream(Rms.loadRMS("NRitem0"));
						this.loadItemNew(dataInputStream3.r, 0, false);
						DataInputStream dataInputStream4 = new DataInputStream(Rms.loadRMS("NRitem1"));
						this.loadItemNew(dataInputStream4.r, 1, false);
						DataInputStream dataInputStream5 = new DataInputStream(Rms.loadRMS("NRitem2"));
						this.loadItemNew(dataInputStream5.r, 2, false);
						DataInputStream dataInputStream6 = new DataInputStream(Rms.loadRMS("NRitem100"));
						this.loadItemNew(dataInputStream6.r, 100, false);
						LoginScr.isUpdateItem = false;
					}
					catch (Exception)
					{
						GameScr.vcItem = -1;
						Service.gI().updateItem();
					}
				}
				bool flag7 = GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem;
				if (flag7)
				{
					bool flag8 = !GameScr.isLoadAllData;
					if (flag8)
					{
						GameScr.gI().readDart();
						GameScr.gI().readEfect();
						GameScr.gI().readArrow();
						GameScr.gI().readSkill();
					}
					Service.gI().clientOk();
				}
				sbyte b4 = msg.reader().readByte();
				Res.outz("CAPTION LENT= " + b4);
				GameScr.exps = new long[(int)b4];
				for (int i = 0; i < GameScr.exps.Length; i++)
				{
					GameScr.exps[i] = msg.reader().readLong();
				}
				break;
			}
			case 5:
			case 11:
			case 13:
			case 14:
			case 15:
			case 19:
				break;
			case 6:
			{
				Res.outz("GET UPDATE_MAP " + msg.reader().available() + " bytes");
				msg.reader().mark(100000);
				this.createMap(msg.reader());
				msg.reader().reset();
				sbyte[] data = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref data);
				Rms.saveRMS("NRmap", data);
				sbyte[] data2 = new sbyte[]
				{
					GameScr.vcMap
				};
				Rms.saveRMS("NRmapVersion", data2);
				LoginScr.isUpdateMap = false;
				bool flag9 = GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem;
				if (flag9)
				{
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
				}
				break;
			}
			case 7:
			{
				Res.outz("GET UPDATE_SKILL " + msg.reader().available() + " bytes");
				msg.reader().mark(100000);
				this.createSkill(msg.reader());
				msg.reader().reset();
				sbyte[] data3 = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref data3);
				Rms.saveRMS("NRskill", data3);
				sbyte[] data4 = new sbyte[]
				{
					GameScr.vcSkill
				};
				Rms.saveRMS("NRskillVersion", data4);
				LoginScr.isUpdateSkill = false;
				bool flag10 = GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem;
				if (flag10)
				{
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
				}
				break;
			}
			case 8:
				Res.outz("GET UPDATE_ITEM " + msg.reader().available() + " bytes");
				this.createItemNew(msg.reader());
				break;
			case 9:
				GameCanvas.debug("SA11", 2);
				break;
			case 10:
				try
				{
					global::Char.isLoadingMap = true;
					Res.outz("REQUEST MAP TEMPLATE");
					GameCanvas.isLoading = true;
					TileMap.maps = null;
					TileMap.types = null;
					mSystem.gcc();
					GameCanvas.debug("SA99", 2);
					TileMap.tmw = (int)msg.reader().readByte();
					TileMap.tmh = (int)msg.reader().readByte();
					TileMap.maps = new int[TileMap.tmw * TileMap.tmh];
					Res.outz("   M apsize= " + TileMap.tmw * TileMap.tmh);
					for (int j = 0; j < TileMap.maps.Length; j++)
					{
						int num = (int)msg.reader().readByte();
						bool flag11 = num < 0;
						if (flag11)
						{
							num += 256;
						}
						TileMap.maps[j] = (int)((ushort)num);
					}
					TileMap.types = new int[TileMap.maps.Length];
					msg = this.messWait;
					this.loadInfoMap(msg);
					try
					{
						TileMap.isMapDouble = (msg.reader().readByte() != 0);
					}
					catch (Exception)
					{
					}
				}
				catch (Exception ex)
				{
					Cout.LogError("LOI TAI CASE REQUEST_MAPTEMPLATE " + ex.ToString());
				}
				msg.cleanup();
				this.messWait.cleanup();
				msg = (this.messWait = null);
				break;
			case 12:
				GameCanvas.debug("SA10", 2);
				break;
			case 16:
				MoneyCharge.gI().switchToMe();
				break;
			case 17:
				GameCanvas.debug("SYB123", 2);
				global::Char.myCharz().clearTask();
				break;
			case 18:
			{
				GameCanvas.isLoading = false;
				GameCanvas.endDlg();
				int num2 = msg.reader().readInt();
				GameCanvas.inputDlg.show(mResources.changeNameChar, new Command(mResources.OK, GameCanvas.instance, 88829, num2), TField.INPUT_TYPE_ANY);
				break;
			}
			case 20:
				global::Char.myCharz().cPk = msg.reader().readByte();
				GameScr.info1.addInfo(mResources.PK_NOW + " " + global::Char.myCharz().cPk, 0);
				break;
			default:
				if (b2 != 35)
				{
					if (b2 == 36)
					{
						GameScr.typeActive = msg.reader().readByte();
						Res.outz("load Me Active: " + GameScr.typeActive);
					}
				}
				else
				{
					GameCanvas.endDlg();
					GameScr.gI().resetButton();
					GameScr.info1.addInfo(msg.reader().readUTF(), 0);
				}
				break;
			}
		}
		catch (Exception)
		{
			Cout.LogError("LOI TAI messageNotMap + " + msg.command);
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x0600018B RID: 395 RVA: 0x0002F3E0 File Offset: 0x0002D5E0
	public void messageNotLogin(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			mSystem.LogCMD("---messageNotLogin : " + b);
			bool flag = b == 2;
			if (flag)
			{
				string text = msg.reader().readUTF();
				bool isTest = mSystem.isTest;
				if (isTest)
				{
					text = "88:192.168.1.88:20000:0,53:112.213.85.53:20000:0," + text;
				}
				bool flag2 = mSystem.clientType == 1;
				if (flag2)
				{
					ServerListScreen.linkDefault = text;
				}
				else
				{
					ServerListScreen.linkDefault = text;
				}
				ServerListScreen.getServerList(ServerListScreen.linkDefault);
				try
				{
					sbyte b2 = msg.reader().readByte();
					Panel.CanNapTien = (b2 == 1);
				}
				catch (Exception)
				{
				}
			}
		}
		catch (Exception)
		{
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0002F4D0 File Offset: 0x0002D6D0
	public void messageSubCommand(Message msg)
	{
		try
		{
			GameCanvas.debug("SA12", 2);
			sbyte b = msg.reader().readByte();
			mSystem.LogCMD("---messageSubCommand : " + b);
			sbyte b2 = b;
			switch (b2)
			{
			case 0:
			{
				GameCanvas.debug("SA21", 2);
				RadarScr.list = new MyVector();
				Teleport.vTeleport.removeAllElements();
				GameScr.vCharInMap.removeAllElements();
				GameScr.vItemMap.removeAllElements();
				global::Char.vItemTime.removeAllElements();
				GameScr.loadImg();
				GameScr.currentCharViewInfo = global::Char.myCharz();
				global::Char.myCharz().charID = msg.reader().readInt();
				global::Char.myCharz().ctaskId = (int)msg.reader().readByte();
				global::Char.myCharz().cgender = (int)msg.reader().readByte();
				global::Char.myCharz().head = (int)msg.reader().readShort();
				global::Char.myCharz().cName = msg.reader().readUTF();
				global::Char.myCharz().cPk = msg.reader().readByte();
				global::Char.myCharz().cTypePk = msg.reader().readByte();
				global::Char.myCharz().cPower = msg.reader().readLong();
				global::Char.myCharz().applyCharLevelPercent();
				global::Char.myCharz().eff5BuffHp = (int)msg.reader().readShort();
				global::Char.myCharz().eff5BuffMp = (int)msg.reader().readShort();
				global::Char.myCharz().nClass = GameScr.nClasss[(int)msg.reader().readByte()];
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				GameScr.gI().dHP = global::Char.myCharz().cHP;
				GameScr.gI().dMP = global::Char.myCharz().cMP;
				sbyte b3 = msg.reader().readByte();
				for (sbyte b4 = 0; b4 < b3; b4 += 1)
				{
					Skill skill = Skills.get(msg.reader().readShort());
					this.useSkill(skill);
				}
				GameScr.gI().sortSkill();
				GameScr.gI().loadSkillShortcut();
				global::Char.myCharz().xu = msg.reader().readLong();
				global::Char.myCharz().luongKhoa = msg.reader().readInt();
				global::Char.myCharz().luong = msg.reader().readInt();
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
				global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
				global::Char.myCharz().arrItemBody = new Item[(int)msg.reader().readByte()];
				try
				{
					global::Char.myCharz().setDefaultPart();
					for (int i = 0; i < global::Char.myCharz().arrItemBody.Length; i++)
					{
						short num = msg.reader().readShort();
						bool flag = num == -1;
						if (!flag)
						{
							ItemTemplate itemTemplate = ItemTemplates.get(num);
							int type = (int)itemTemplate.type;
							global::Char.myCharz().arrItemBody[i] = new Item();
							global::Char.myCharz().arrItemBody[i].template = itemTemplate;
							global::Char.myCharz().arrItemBody[i].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBody[i].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBody[i].content = msg.reader().readUTF();
							int num2 = (int)msg.reader().readUnsignedByte();
							bool flag2 = num2 != 0;
							if (flag2)
							{
								global::Char.myCharz().arrItemBody[i].itemOption = new ItemOption[num2];
								for (int j = 0; j < global::Char.myCharz().arrItemBody[i].itemOption.Length; j++)
								{
									int num3 = (int)msg.reader().readUnsignedByte();
									int param = (int)msg.reader().readUnsignedShort();
									bool flag3 = num3 != -1;
									if (flag3)
									{
										global::Char.myCharz().arrItemBody[i].itemOption[j] = new ItemOption(num3, param);
									}
								}
							}
							int num4 = type;
							if (num4 != 0)
							{
								if (num4 == 1)
								{
									global::Char.myCharz().leg = (int)global::Char.myCharz().arrItemBody[i].template.part;
									Res.outz("toi day =======================================" + global::Char.myCharz().leg);
								}
							}
							else
							{
								Res.outz("toi day =======================================" + global::Char.myCharz().body);
								global::Char.myCharz().body = (int)global::Char.myCharz().arrItemBody[i].template.part;
							}
						}
					}
				}
				catch (Exception)
				{
				}
				global::Char.myCharz().arrItemBag = new Item[(int)msg.reader().readByte()];
				GameScr.hpPotion = 0;
				for (int k = 0; k < global::Char.myCharz().arrItemBag.Length; k++)
				{
					short num5 = msg.reader().readShort();
					bool flag4 = num5 == -1;
					if (!flag4)
					{
						global::Char.myCharz().arrItemBag[k] = new Item();
						global::Char.myCharz().arrItemBag[k].template = ItemTemplates.get(num5);
						global::Char.myCharz().arrItemBag[k].quantity = msg.reader().readInt();
						global::Char.myCharz().arrItemBag[k].info = msg.reader().readUTF();
						global::Char.myCharz().arrItemBag[k].content = msg.reader().readUTF();
						global::Char.myCharz().arrItemBag[k].indexUI = k;
						sbyte b5 = msg.reader().readByte();
						bool flag5 = b5 != 0;
						if (flag5)
						{
							global::Char.myCharz().arrItemBag[k].itemOption = new ItemOption[(int)b5];
							for (int l = 0; l < global::Char.myCharz().arrItemBag[k].itemOption.Length; l++)
							{
								int num6 = (int)msg.reader().readUnsignedByte();
								int param2 = (int)msg.reader().readUnsignedShort();
								bool flag6 = num6 != -1;
								if (flag6)
								{
									global::Char.myCharz().arrItemBag[k].itemOption[l] = new ItemOption(num6, param2);
									global::Char.myCharz().arrItemBag[k].getCompare();
								}
							}
						}
						bool flag7 = global::Char.myCharz().arrItemBag[k].template.type == 6;
						if (flag7)
						{
							GameScr.hpPotion += global::Char.myCharz().arrItemBag[k].quantity;
						}
					}
				}
				global::Char.myCharz().arrItemBox = new Item[(int)msg.reader().readByte()];
				GameCanvas.panel.hasUse = 0;
				for (int m = 0; m < global::Char.myCharz().arrItemBox.Length; m++)
				{
					short num7 = msg.reader().readShort();
					bool flag8 = num7 == -1;
					if (!flag8)
					{
						global::Char.myCharz().arrItemBox[m] = new Item();
						global::Char.myCharz().arrItemBox[m].template = ItemTemplates.get(num7);
						global::Char.myCharz().arrItemBox[m].quantity = msg.reader().readInt();
						global::Char.myCharz().arrItemBox[m].info = msg.reader().readUTF();
						global::Char.myCharz().arrItemBox[m].content = msg.reader().readUTF();
						global::Char.myCharz().arrItemBox[m].itemOption = new ItemOption[(int)msg.reader().readByte()];
						for (int n = 0; n < global::Char.myCharz().arrItemBox[m].itemOption.Length; n++)
						{
							int num8 = (int)msg.reader().readUnsignedByte();
							int param3 = (int)msg.reader().readUnsignedShort();
							bool flag9 = num8 != -1;
							if (flag9)
							{
								global::Char.myCharz().arrItemBox[m].itemOption[n] = new ItemOption(num8, param3);
								global::Char.myCharz().arrItemBox[m].getCompare();
							}
						}
						GameCanvas.panel.hasUse++;
					}
				}
				global::Char.myCharz().statusMe = 4;
				int num9 = Rms.loadRMSInt(global::Char.myCharz().cName + "vci");
				bool flag10 = num9 < 1;
				if (flag10)
				{
					GameScr.isViewClanInvite = false;
				}
				else
				{
					GameScr.isViewClanInvite = true;
				}
				short num10 = msg.reader().readShort();
				global::Char.idHead = new short[(int)num10];
				global::Char.idAvatar = new short[(int)num10];
				for (int num11 = 0; num11 < (int)num10; num11++)
				{
					global::Char.idHead[num11] = msg.reader().readShort();
					global::Char.idAvatar[num11] = msg.reader().readShort();
				}
				for (int num12 = 0; num12 < GameScr.info1.charId.Length; num12++)
				{
					GameScr.info1.charId[num12] = new int[3];
				}
				GameScr.info1.charId[global::Char.myCharz().cgender][0] = (int)msg.reader().readShort();
				GameScr.info1.charId[global::Char.myCharz().cgender][1] = (int)msg.reader().readShort();
				GameScr.info1.charId[global::Char.myCharz().cgender][2] = (int)msg.reader().readShort();
				global::Char.myCharz().isNhapThe = (msg.reader().readByte() == 1);
				Res.outz("NHAP THE= " + global::Char.myCharz().isNhapThe.ToString());
				GameScr.deltaTime = mSystem.currentTimeMillis() - (long)msg.reader().readInt() * 1000L;
				GameScr.isNewMember = msg.reader().readByte();
				Service.gI().updateCaption((sbyte)global::Char.myCharz().cgender);
				Service.gI().androidPack();
				try
				{
					global::Char.myCharz().idAuraEff = msg.reader().readShort();
					global::Char.myCharz().idEff_Set_Item = (short)msg.reader().readSByte();
					global::Char.myCharz().idHat = msg.reader().readShort();
				}
				catch (Exception)
				{
				}
				break;
			}
			case 1:
				GameCanvas.debug("SA13", 2);
				global::Char.myCharz().nClass = GameScr.nClasss[(int)msg.reader().readByte()];
				global::Char.myCharz().cTiemNang = msg.reader().readLong();
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				global::Char.myCharz().myskill = null;
				break;
			case 2:
			{
				GameCanvas.debug("SA14", 2);
				bool flag11 = global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5;
				if (flag11)
				{
					global::Char.myCharz().cHP = global::Char.myCharz().cHPFull;
					global::Char.myCharz().cMP = global::Char.myCharz().cMPFull;
					Cout.LogError2(" ME_LOAD_SKILL");
				}
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				sbyte b6 = msg.reader().readByte();
				for (sbyte b7 = 0; b7 < b6; b7 += 1)
				{
					short skillId = msg.reader().readShort();
					Skill skill2 = Skills.get(skillId);
					this.useSkill(skill2);
				}
				GameScr.gI().sortSkill();
				bool isPaintInfoMe = GameScr.isPaintInfoMe;
				if (isPaintInfoMe)
				{
					GameScr.indexRow = -1;
					GameScr.gI().left = (GameScr.gI().center = null);
				}
				break;
			}
			case 3:
			case 16:
			case 17:
			case 18:
			case 20:
			case 22:
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
			case 30:
			case 31:
			case 32:
			case 33:
			case 34:
				break;
			case 4:
				GameCanvas.debug("SA23", 2);
				global::Char.myCharz().xu = msg.reader().readLong();
				global::Char.myCharz().luong = msg.reader().readInt();
				global::Char.myCharz().cHP = msg.readInt3Byte();
				global::Char.myCharz().cMP = msg.readInt3Byte();
				global::Char.myCharz().luongKhoa = msg.reader().readInt();
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
				global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
				break;
			case 5:
			{
				GameCanvas.debug("SA24", 2);
				int cHP = global::Char.myCharz().cHP;
				global::Char.myCharz().cHP = msg.readInt3Byte();
				bool flag12 = global::Char.myCharz().cHP > cHP && global::Char.myCharz().cTypePk != 4;
				if (flag12)
				{
					GameScr.startFlyText(string.Concat(new object[]
					{
						"+",
						global::Char.myCharz().cHP - cHP,
						" ",
						mResources.HP
					}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 20, 0, -1, mFont.HP);
					SoundMn.gI().HP_MPup();
					bool flag13 = global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5003;
					if (flag13)
					{
						MonsterDart.addMonsterDart(global::Char.myCharz().petFollow.cmx + ((global::Char.myCharz().petFollow.dir != 1) ? -10 : 10), global::Char.myCharz().petFollow.cmy + 10, true, -1, -1, global::Char.myCharz(), 29);
					}
				}
				bool flag14 = global::Char.myCharz().cHP < cHP;
				if (flag14)
				{
					GameScr.startFlyText(string.Concat(new object[]
					{
						"-",
						cHP - global::Char.myCharz().cHP,
						" ",
						mResources.HP
					}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 20, 0, -1, mFont.HP);
				}
				GameScr.gI().dHP = global::Char.myCharz().cHP;
				bool isPaintInfoMe2 = GameScr.isPaintInfoMe;
				if (isPaintInfoMe2)
				{
				}
				break;
			}
			case 6:
			{
				GameCanvas.debug("SA25", 2);
				bool flag15 = global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5;
				if (!flag15)
				{
					int cMP = global::Char.myCharz().cMP;
					global::Char.myCharz().cMP = msg.readInt3Byte();
					bool flag16 = global::Char.myCharz().cMP > cMP;
					if (flag16)
					{
						GameScr.startFlyText(string.Concat(new object[]
						{
							"+",
							global::Char.myCharz().cMP - cMP,
							" ",
							mResources.KI
						}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 23, 0, -2, mFont.MP);
						SoundMn.gI().HP_MPup();
						bool flag17 = global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5001;
						if (flag17)
						{
							MonsterDart.addMonsterDart(global::Char.myCharz().petFollow.cmx + ((global::Char.myCharz().petFollow.dir != 1) ? -10 : 10), global::Char.myCharz().petFollow.cmy + 10, true, -1, -1, global::Char.myCharz(), 29);
						}
					}
					bool flag18 = global::Char.myCharz().cMP < cMP;
					if (flag18)
					{
						GameScr.startFlyText(string.Concat(new object[]
						{
							"-",
							cMP - global::Char.myCharz().cMP,
							" ",
							mResources.KI
						}), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 23, 0, -2, mFont.MP);
					}
					Res.outz("curr MP= " + global::Char.myCharz().cMP);
					GameScr.gI().dMP = global::Char.myCharz().cMP;
					bool isPaintInfoMe3 = GameScr.isPaintInfoMe;
					if (isPaintInfoMe3)
					{
					}
				}
				break;
			}
			case 7:
			{
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				bool flag19 = @char != null;
				if (flag19)
				{
					@char.clanID = msg.reader().readInt();
					bool flag20 = @char.clanID == -2;
					if (flag20)
					{
						@char.isCopy = true;
					}
					this.readCharInfo(@char, msg);
					try
					{
						@char.idAuraEff = msg.reader().readShort();
						@char.idEff_Set_Item = (short)msg.reader().readSByte();
						@char.idHat = msg.reader().readShort();
					}
					catch (Exception)
					{
					}
				}
				break;
			}
			case 8:
			{
				GameCanvas.debug("SA26", 2);
				global::Char char2 = GameScr.findCharInMap(msg.reader().readInt());
				bool flag21 = char2 != null;
				if (flag21)
				{
					char2.cspeed = (int)msg.reader().readByte();
				}
				break;
			}
			case 9:
			{
				GameCanvas.debug("SA27", 2);
				global::Char char3 = GameScr.findCharInMap(msg.reader().readInt());
				bool flag22 = char3 != null;
				if (flag22)
				{
					char3.cHP = msg.readInt3Byte();
					char3.cHPFull = msg.readInt3Byte();
				}
				break;
			}
			case 10:
			{
				GameCanvas.debug("SA28", 2);
				global::Char char4 = GameScr.findCharInMap(msg.reader().readInt());
				bool flag23 = char4 != null;
				if (flag23)
				{
					char4.cHP = msg.readInt3Byte();
					char4.cHPFull = msg.readInt3Byte();
					char4.eff5BuffHp = (int)msg.reader().readShort();
					char4.eff5BuffMp = (int)msg.reader().readShort();
					char4.wp = (int)msg.reader().readShort();
					bool flag24 = char4.wp == -1;
					if (flag24)
					{
						char4.setDefaultWeapon();
					}
				}
				break;
			}
			case 11:
			{
				GameCanvas.debug("SA29", 2);
				global::Char char5 = GameScr.findCharInMap(msg.reader().readInt());
				bool flag25 = char5 != null;
				if (flag25)
				{
					char5.cHP = msg.readInt3Byte();
					char5.cHPFull = msg.readInt3Byte();
					char5.eff5BuffHp = (int)msg.reader().readShort();
					char5.eff5BuffMp = (int)msg.reader().readShort();
					char5.body = (int)msg.reader().readShort();
					bool flag26 = char5.body == -1;
					if (flag26)
					{
						char5.setDefaultBody();
					}
				}
				break;
			}
			case 12:
			{
				GameCanvas.debug("SA30", 2);
				global::Char char6 = GameScr.findCharInMap(msg.reader().readInt());
				bool flag27 = char6 != null;
				if (flag27)
				{
					char6.cHP = msg.readInt3Byte();
					char6.cHPFull = msg.readInt3Byte();
					char6.eff5BuffHp = (int)msg.reader().readShort();
					char6.eff5BuffMp = (int)msg.reader().readShort();
					char6.leg = (int)msg.reader().readShort();
					bool flag28 = char6.leg == -1;
					if (flag28)
					{
						char6.setDefaultLeg();
					}
				}
				break;
			}
			case 13:
			{
				GameCanvas.debug("SA31", 2);
				global::Char char7 = GameScr.findCharInMap(msg.reader().readInt());
				bool flag29 = char7 != null;
				if (flag29)
				{
					char7.cHP = msg.readInt3Byte();
					char7.cHPFull = msg.readInt3Byte();
					char7.eff5BuffHp = (int)msg.reader().readShort();
					char7.eff5BuffMp = (int)msg.reader().readShort();
				}
				break;
			}
			case 14:
			{
				GameCanvas.debug("SA32", 2);
				global::Char char8 = GameScr.findCharInMap(msg.reader().readInt());
				bool flag30 = char8 != null;
				if (flag30)
				{
					char8.cHP = msg.readInt3Byte();
					sbyte b8 = msg.reader().readByte();
					Res.outz("player load hp type= " + b8);
					bool flag31 = b8 == 1;
					if (flag31)
					{
						ServerEffect.addServerEffect(11, char8, 5);
						ServerEffect.addServerEffect(104, char8, 4);
					}
					try
					{
						char8.cHPFull = msg.readInt3Byte();
					}
					catch (Exception)
					{
					}
				}
				break;
			}
			case 15:
			{
				GameCanvas.debug("SA33", 2);
				global::Char char9 = GameScr.findCharInMap(msg.reader().readInt());
				bool flag32 = char9 != null;
				if (flag32)
				{
					char9.cHP = msg.readInt3Byte();
					char9.cHPFull = msg.readInt3Byte();
					char9.cx = (int)msg.reader().readShort();
					char9.cy = (int)msg.reader().readShort();
					char9.statusMe = 1;
					char9.cp3 = 3;
					ServerEffect.addServerEffect(109, char9, 2);
				}
				break;
			}
			case 19:
				GameCanvas.debug("SA17", 2);
				global::Char.myCharz().boxSort();
				break;
			case 21:
			{
				GameCanvas.debug("SA19", 2);
				int num13 = msg.reader().readInt();
				global::Char.myCharz().xuInBox -= num13;
				global::Char.myCharz().xu += (long)num13;
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				break;
			}
			case 23:
			{
				short num14 = msg.reader().readShort();
				Skill skill3 = Skills.get(num14);
				this.useSkill(skill3);
				bool flag33 = num14 != 0 && num14 != 14 && num14 != 28;
				if (flag33)
				{
					GameScr.info1.addInfo(mResources.LEARN_SKILL + " " + skill3.template.name, 0);
				}
				break;
			}
			case 35:
			{
				GameCanvas.debug("SY3", 2);
				int num15 = msg.reader().readInt();
				Res.outz("CID = " + num15);
				bool flag34 = TileMap.mapID == 130;
				if (flag34)
				{
					GameScr.gI().starVS();
				}
				bool flag35 = num15 == global::Char.myCharz().charID;
				if (flag35)
				{
					global::Char.myCharz().cTypePk = msg.reader().readByte();
					bool flag36 = GameScr.gI().isVS() && global::Char.myCharz().cTypePk != 0;
					if (flag36)
					{
						GameScr.gI().starVS();
					}
					Res.outz("type pk= " + global::Char.myCharz().cTypePk);
					global::Char.myCharz().npcFocus = null;
					bool flag37 = !GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus);
					if (flag37)
					{
						global::Char.myCharz().mobFocus = null;
					}
					global::Char.myCharz().itemFocus = null;
				}
				else
				{
					global::Char char10 = GameScr.findCharInMap(num15);
					bool flag38 = char10 != null;
					if (flag38)
					{
						Res.outz("type pk= " + char10.cTypePk);
						char10.cTypePk = msg.reader().readByte();
						bool flag39 = char10.isAttacPlayerStatus();
						if (flag39)
						{
							global::Char.myCharz().charFocus = char10;
						}
					}
				}
				for (int num16 = 0; num16 < GameScr.vCharInMap.size(); num16++)
				{
					global::Char char11 = GameScr.findCharInMap(num16);
					bool flag40 = char11 != null && char11.cTypePk != 0 && char11.cTypePk == global::Char.myCharz().cTypePk;
					if (flag40)
					{
						bool flag41 = !global::Char.myCharz().mobFocus.isMobMe;
						if (flag41)
						{
							global::Char.myCharz().mobFocus = null;
						}
						global::Char.myCharz().npcFocus = null;
						global::Char.myCharz().itemFocus = null;
						break;
					}
				}
				Res.outz("update type pk= ");
				break;
			}
			default:
				switch (b2)
				{
				case 61:
				{
					string text = msg.reader().readUTF();
					sbyte[] array = new sbyte[msg.reader().readInt()];
					msg.reader().read(ref array);
					bool flag42 = array.Length == 0;
					if (flag42)
					{
						array = null;
					}
					bool flag43 = text.Equals("KSkill");
					if (flag43)
					{
						GameScr.gI().onKSkill(array);
					}
					else
					{
						bool flag44 = text.Equals("OSkill");
						if (flag44)
						{
							GameScr.gI().onOSkill(array);
						}
						else
						{
							bool flag45 = text.Equals("CSkill");
							if (flag45)
							{
								GameScr.gI().onCSkill(array);
							}
						}
					}
					break;
				}
				case 62:
				{
					Res.outz("ME UPDATE SKILL");
					short skillId2 = msg.reader().readShort();
					Skill skill4 = Skills.get(skillId2);
					for (int num17 = 0; num17 < global::Char.myCharz().vSkill.size(); num17++)
					{
						Skill skill5 = (Skill)global::Char.myCharz().vSkill.elementAt(num17);
						bool flag46 = skill5.template.id == skill4.template.id;
						if (flag46)
						{
							global::Char.myCharz().vSkill.setElementAt(skill4, num17);
							break;
						}
					}
					for (int num18 = 0; num18 < global::Char.myCharz().vSkillFight.size(); num18++)
					{
						Skill skill6 = (Skill)global::Char.myCharz().vSkillFight.elementAt(num18);
						bool flag47 = skill6.template.id == skill4.template.id;
						if (flag47)
						{
							global::Char.myCharz().vSkillFight.setElementAt(skill4, num18);
							break;
						}
					}
					for (int num19 = 0; num19 < GameScr.onScreenSkill.Length; num19++)
					{
						bool flag48 = GameScr.onScreenSkill[num19] != null && GameScr.onScreenSkill[num19].template.id == skill4.template.id;
						if (flag48)
						{
							GameScr.onScreenSkill[num19] = skill4;
							break;
						}
					}
					for (int num20 = 0; num20 < GameScr.keySkill.Length; num20++)
					{
						bool flag49 = GameScr.keySkill[num20] != null && GameScr.keySkill[num20].template.id == skill4.template.id;
						if (flag49)
						{
							GameScr.keySkill[num20] = skill4;
							break;
						}
					}
					bool flag50 = global::Char.myCharz().myskill.template.id == skill4.template.id;
					if (flag50)
					{
						global::Char.myCharz().myskill = skill4;
					}
					GameScr.info1.addInfo(string.Concat(new object[]
					{
						mResources.hasJustUpgrade1,
						skill4.template.name,
						mResources.hasJustUpgrade2,
						skill4.point
					}), 0);
					break;
				}
				case 63:
				{
					sbyte b9 = msg.reader().readByte();
					bool flag51 = b9 > 0;
					if (flag51)
					{
						InfoDlg.showWait();
						MyVector vPlayerMenu = GameCanvas.panel.vPlayerMenu;
						for (int num21 = 0; num21 < (int)b9; num21++)
						{
							string caption = msg.reader().readUTF();
							string caption2 = msg.reader().readUTF();
							short menuSelect = msg.reader().readShort();
							global::Char.myCharz().charFocus.menuSelect = (int)menuSelect;
							vPlayerMenu.addElement(new Command(caption, 11115, global::Char.myCharz().charFocus)
							{
								caption2 = caption2
							});
						}
						InfoDlg.hide();
						GameCanvas.panel.setTabPlayerMenu();
					}
					break;
				}
				}
				break;
			}
		}
		catch (Exception ex)
		{
			Cout.println("Loi tai Sub : " + ex.ToString());
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00031268 File Offset: 0x0002F468
	private void useSkill(Skill skill)
	{
		bool flag = global::Char.myCharz().myskill == null;
		if (flag)
		{
			global::Char.myCharz().myskill = skill;
		}
		else
		{
			bool flag2 = skill.template.Equals(global::Char.myCharz().myskill.template);
			if (flag2)
			{
				global::Char.myCharz().myskill = skill;
			}
		}
		global::Char.myCharz().vSkill.addElement(skill);
		bool flag3 = (skill.template.type == 1 || skill.template.type == 4 || skill.template.type == 2 || skill.template.type == 3) && (skill.template.maxPoint == 0 || (skill.template.maxPoint > 0 && skill.point > 0));
		if (flag3)
		{
			bool flag4 = (int)skill.template.id == global::Char.myCharz().skillTemplateId;
			if (flag4)
			{
				Service.gI().selectSkill(global::Char.myCharz().skillTemplateId);
			}
			global::Char.myCharz().vSkillFight.addElement(skill);
		}
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00031380 File Offset: 0x0002F580
	public bool readCharInfo(global::Char c, Message msg)
	{
		try
		{
			c.clevel = (int)msg.reader().readByte();
			c.isInvisiblez = msg.reader().readBoolean();
			c.cTypePk = msg.reader().readByte();
			Res.outz(string.Concat(new object[]
			{
				"ADD TYPE PK= ",
				c.cTypePk,
				" to player ",
				c.charID,
				" @@ ",
				c.cName
			}));
			c.nClass = GameScr.nClasss[(int)msg.reader().readByte()];
			c.cgender = (int)msg.reader().readByte();
			c.head = (int)msg.reader().readShort();
			c.cName = msg.reader().readUTF();
			c.cHP = msg.readInt3Byte();
			c.dHP = c.cHP;
			bool flag = c.cHP == 0;
			if (flag)
			{
				c.statusMe = 14;
			}
			c.cHPFull = msg.readInt3Byte();
			bool flag2 = c.cy >= TileMap.pxh - 100;
			if (flag2)
			{
				c.isFlyUp = true;
			}
			c.body = (int)msg.reader().readShort();
			c.leg = (int)msg.reader().readShort();
			c.bag = (int)msg.reader().readUnsignedByte();
			Res.outz(string.Concat(new object[]
			{
				" body= ",
				c.body,
				" leg= ",
				c.leg,
				" bag=",
				c.bag,
				"BAG ==",
				c.bag,
				"*********************************"
			}));
			c.isShadown = true;
			sbyte b = msg.reader().readByte();
			bool flag3 = c.wp == -1;
			if (flag3)
			{
				c.setDefaultWeapon();
			}
			bool flag4 = c.body == -1;
			if (flag4)
			{
				c.setDefaultBody();
			}
			bool flag5 = c.leg == -1;
			if (flag5)
			{
				c.setDefaultLeg();
			}
			c.cx = (int)msg.reader().readShort();
			c.cy = (int)msg.reader().readShort();
			c.xSd = c.cx;
			c.ySd = c.cy;
			c.eff5BuffHp = (int)msg.reader().readShort();
			c.eff5BuffMp = (int)msg.reader().readShort();
			int num = (int)msg.reader().readByte();
			for (int i = 0; i < num; i++)
			{
				EffectChar effectChar = new EffectChar(msg.reader().readByte(), msg.reader().readInt(), msg.reader().readInt(), msg.reader().readShort());
				c.vEff.addElement(effectChar);
				bool flag6 = effectChar.template.type == 12 || effectChar.template.type == 11;
				if (flag6)
				{
					c.isInvisiblez = true;
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		return false;
	}

	// Token: 0x0600018F RID: 399 RVA: 0x000316E8 File Offset: 0x0002F8E8
	private void readGetImgByName(Message msg)
	{
		try
		{
			string text = msg.reader().readUTF();
			sbyte nFrame = msg.reader().readByte();
			sbyte[] array = NinjaUtil.readByteArray(msg);
			Image img = this.createImage(array);
			ImgByName.SetImage(text, img, nFrame);
			bool flag = array != null;
			if (flag)
			{
				ImgByName.saveRMS(text, nFrame, array);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00031758 File Offset: 0x0002F958
	private void createItemNew(myReader d)
	{
		try
		{
			this.loadItemNew(d, -1, true);
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000191 RID: 401 RVA: 0x0003178C File Offset: 0x0002F98C
	private void loadItemNew(myReader d, sbyte type, bool isSave)
	{
		try
		{
			d.mark(100000);
			GameScr.vcItem = d.readByte();
			type = d.readByte();
			bool flag = type == 0;
			if (flag)
			{
				GameScr.gI().iOptionTemplates = new ItemOptionTemplate[(int)d.readUnsignedByte()];
				for (int i = 0; i < GameScr.gI().iOptionTemplates.Length; i++)
				{
					GameScr.gI().iOptionTemplates[i] = new ItemOptionTemplate();
					GameScr.gI().iOptionTemplates[i].id = i;
					GameScr.gI().iOptionTemplates[i].name = d.readUTF();
					GameScr.gI().iOptionTemplates[i].type = (int)d.readByte();
				}
				if (isSave)
				{
					d.reset();
					sbyte[] data = new sbyte[d.available()];
					d.readFully(ref data);
					Rms.saveRMS("NRitem0", data);
				}
			}
			else
			{
				bool flag2 = type == 1;
				if (flag2)
				{
					ItemTemplates.itemTemplates.clear();
					int num = (int)d.readShort();
					for (int j = 0; j < num; j++)
					{
						ItemTemplate it = new ItemTemplate((short)j, d.readByte(), d.readByte(), d.readUTF(), d.readUTF(), d.readByte(), d.readInt(), d.readShort(), d.readShort(), d.readBoolean());
						ItemTemplates.add(it);
					}
					if (isSave)
					{
						d.reset();
						sbyte[] data2 = new sbyte[d.available()];
						d.readFully(ref data2);
						Rms.saveRMS("NRitem1", data2);
					}
				}
				else
				{
					bool flag3 = type == 2;
					if (flag3)
					{
						int num2 = (int)d.readShort();
						int num3 = (int)d.readShort();
						for (int k = num2; k < num3; k++)
						{
							ItemTemplate it2 = new ItemTemplate((short)k, d.readByte(), d.readByte(), d.readUTF(), d.readUTF(), d.readByte(), d.readInt(), d.readShort(), d.readShort(), d.readBoolean());
							ItemTemplates.add(it2);
						}
						if (isSave)
						{
							d.reset();
							sbyte[] data3 = new sbyte[d.available()];
							d.readFully(ref data3);
							Rms.saveRMS("NRitem2", data3);
							sbyte[] data4 = new sbyte[]
							{
								GameScr.vcItem
							};
							Rms.saveRMS("NRitemVersion", data4);
							LoginScr.isUpdateItem = false;
							bool flag4 = GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem;
							if (flag4)
							{
								GameScr.gI().readDart();
								GameScr.gI().readEfect();
								GameScr.gI().readArrow();
								GameScr.gI().readSkill();
								Service.gI().clientOk();
							}
						}
					}
					else
					{
						bool flag5 = type == 100;
						if (flag5)
						{
							global::Char.Arr_Head_2Fr = this.readArrHead(d);
							if (isSave)
							{
								d.reset();
								sbyte[] data5 = new sbyte[d.available()];
								d.readFully(ref data5);
								Rms.saveRMS("NRitem100", data5);
							}
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			ex.ToString();
		}
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00031B04 File Offset: 0x0002FD04
	private void readFrameBoss(Message msg, int mobTemplateId)
	{
		try
		{
			int num = (int)msg.reader().readByte();
			int[][] array = new int[num][];
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)msg.reader().readByte();
				array[i] = new int[num2];
				for (int j = 0; j < num2; j++)
				{
					array[i][j] = (int)msg.reader().readByte();
				}
			}
			Controller.frameHT_NEWBOSS.put(mobTemplateId + string.Empty, array);
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000193 RID: 403 RVA: 0x00031BAC File Offset: 0x0002FDAC
	private int[][] readArrHead(myReader d)
	{
		int[][] array = new int[][]
		{
			new int[]
			{
				542,
				543
			}
		};
		int[][] result;
		try
		{
			int num = (int)d.readShort();
			array = new int[num][];
			for (int i = 0; i < array.Length; i++)
			{
				int num2 = (int)d.readByte();
				array[i] = new int[num2];
				for (int j = 0; j < num2; j++)
				{
					array[i][j] = (int)d.readShort();
				}
			}
			result = array;
		}
		catch (Exception)
		{
			result = array;
		}
		return result;
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00031C54 File Offset: 0x0002FE54
	public void phuban_Info(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			bool flag = b == 0;
			if (flag)
			{
				this.readPhuBan_CHIENTRUONGNAMEK(msg, (int)b);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000195 RID: 405 RVA: 0x00031C9C File Offset: 0x0002FE9C
	private void readPhuBan_CHIENTRUONGNAMEK(Message msg, int type_PB)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			bool flag = b == 0;
			if (flag)
			{
				short idmapPaint = msg.reader().readShort();
				string nameTeam = msg.reader().readUTF();
				string nameTeam2 = msg.reader().readUTF();
				int maxPoint = msg.reader().readInt();
				short timeSecond = msg.reader().readShort();
				int maxLife = (int)msg.reader().readByte();
				GameScr.phuban_Info = new InfoPhuBan(type_PB, idmapPaint, nameTeam, nameTeam2, maxPoint, timeSecond);
				GameScr.phuban_Info.maxLife = maxLife;
				GameScr.phuban_Info.updateLife(type_PB, 0, 0);
			}
			else
			{
				bool flag2 = b == 1;
				if (flag2)
				{
					int pointTeam = msg.reader().readInt();
					int pointTeam2 = msg.reader().readInt();
					bool flag3 = GameScr.phuban_Info != null;
					if (flag3)
					{
						GameScr.phuban_Info.updatePoint(type_PB, pointTeam, pointTeam2);
					}
				}
				else
				{
					bool flag4 = b == 2;
					if (flag4)
					{
						sbyte b2 = msg.reader().readByte();
						short type = 0;
						bool flag5 = b2 == 1;
						if (flag5)
						{
							type = 1;
						}
						else
						{
							bool flag6 = b2 == 2;
							if (flag6)
							{
								type = 2;
							}
						}
						short subtype = -1;
						GameScr.phuban_Info = null;
						GameScr.addEffectEnd((int)type, (int)subtype, GameCanvas.hw, GameCanvas.hh, 0, 0);
					}
					else
					{
						bool flag7 = b == 5;
						if (flag7)
						{
							short timeSecond2 = msg.reader().readShort();
							bool flag8 = GameScr.phuban_Info != null;
							if (flag8)
							{
								GameScr.phuban_Info.updateTime(type_PB, timeSecond2);
							}
						}
						else
						{
							bool flag9 = b == 4;
							if (flag9)
							{
								int lifeTeam = (int)msg.reader().readByte();
								int lifeTeam2 = (int)msg.reader().readByte();
								bool flag10 = GameScr.phuban_Info != null;
								if (flag10)
								{
									GameScr.phuban_Info.updateLife(type_PB, lifeTeam, lifeTeam2);
								}
							}
						}
					}
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000196 RID: 406 RVA: 0x00031E98 File Offset: 0x00030098
	public void read_opt(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			bool flag = b == 0;
			if (flag)
			{
				short idHat = msg.reader().readShort();
				global::Char.myCharz().idHat = idHat;
				SoundMn.gI().getStrOption();
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x04000426 RID: 1062
	protected static Controller me;

	// Token: 0x04000427 RID: 1063
	protected static Controller me2;

	// Token: 0x04000428 RID: 1064
	public Message messWait;

	// Token: 0x04000429 RID: 1065
	public static bool isLoadingData;

	// Token: 0x0400042A RID: 1066
	public static bool isConnectOK;

	// Token: 0x0400042B RID: 1067
	public static bool isConnectionFail;

	// Token: 0x0400042C RID: 1068
	public static bool isDisconnected;

	// Token: 0x0400042D RID: 1069
	public static bool isMain;

	// Token: 0x0400042E RID: 1070
	private float demCount;

	// Token: 0x0400042F RID: 1071
	private int move;

	// Token: 0x04000430 RID: 1072
	private int total;

	// Token: 0x04000431 RID: 1073
	public static bool isStopReadMessage;

	// Token: 0x04000432 RID: 1074
	public static MyHashTable frameHT_NEWBOSS = new MyHashTable();

	// Token: 0x04000433 RID: 1075
	public const sbyte PHUBAN_TYPE_CHIENTRUONGNAMEK = 0;

	// Token: 0x04000434 RID: 1076
	public const sbyte PHUBAN_START = 0;

	// Token: 0x04000435 RID: 1077
	public const sbyte PHUBAN_UPDATE_POINT = 1;

	// Token: 0x04000436 RID: 1078
	public const sbyte PHUBAN_END = 2;

	// Token: 0x04000437 RID: 1079
	public const sbyte PHUBAN_LIFE = 4;

	// Token: 0x04000438 RID: 1080
	public const sbyte PHUBAN_INFO = 5;
}
