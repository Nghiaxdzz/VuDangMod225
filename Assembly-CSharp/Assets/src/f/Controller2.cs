using System;
using Assets.src.g;

namespace Assets.src.f
{
	// Token: 0x020000D9 RID: 217
	internal class Controller2
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x000AA778 File Offset: 0x000A8978
		public static void readMessage(Message msg)
		{
			try
			{
				Res.outz("cmd=" + msg.command);
				sbyte command = msg.command;
				if (command <= 42)
				{
					switch (command)
					{
					case -128:
						Controller2.readInfoEffChar(msg);
						break;
					case -127:
						Controller2.readLuckyRound(msg);
						break;
					case -126:
					{
						sbyte b = msg.reader().readByte();
						Res.outz("type quay= " + b);
						bool flag = b == 1;
						if (flag)
						{
							sbyte b2 = msg.reader().readByte();
							string num = msg.reader().readUTF();
							string finish = msg.reader().readUTF();
							GameScr.gI().showWinNumber(num, finish);
						}
						bool flag2 = b == 0;
						if (flag2)
						{
							GameScr.gI().showYourNumber(msg.reader().readUTF());
						}
						break;
					}
					case -125:
					{
						ChatTextField.gI().isShow = false;
						string text = msg.reader().readUTF();
						Res.outz("titile= " + text);
						sbyte b3 = msg.reader().readByte();
						ClientInput.gI().setInput((int)b3, text);
						for (int i = 0; i < (int)b3; i++)
						{
							ClientInput.gI().tf[i].name = msg.reader().readUTF();
							sbyte b4 = msg.reader().readByte();
							bool flag3 = b4 == 0;
							if (flag3)
							{
								ClientInput.gI().tf[i].setIputType(TField.INPUT_TYPE_NUMERIC);
							}
							bool flag4 = b4 == 1;
							if (flag4)
							{
								ClientInput.gI().tf[i].setIputType(TField.INPUT_TYPE_ANY);
							}
							bool flag5 = b4 == 2;
							if (flag5)
							{
								ClientInput.gI().tf[i].setIputType(TField.INPUT_TYPE_PASSWORD);
							}
						}
						break;
					}
					case -124:
					{
						sbyte b5 = msg.reader().readByte();
						sbyte b6 = msg.reader().readByte();
						bool flag6 = b6 == 0;
						if (flag6)
						{
							bool flag7 = b5 == 2;
							if (flag7)
							{
								int num2 = msg.reader().readInt();
								bool flag8 = num2 == global::Char.myCharz().charID;
								if (flag8)
								{
									global::Char.myCharz().removeEffect();
								}
								else
								{
									bool flag9 = GameScr.findCharInMap(num2) != null;
									if (flag9)
									{
										GameScr.findCharInMap(num2).removeEffect();
									}
								}
							}
							int num3 = (int)msg.reader().readUnsignedByte();
							int num4 = msg.reader().readInt();
							bool flag10 = num3 == 32;
							if (flag10)
							{
								bool flag11 = b5 == 1;
								if (flag11)
								{
									int num5 = msg.reader().readInt();
									bool flag12 = num4 == global::Char.myCharz().charID;
									if (flag12)
									{
										global::Char.myCharz().holdEffID = num3;
										GameScr.findCharInMap(num5).setHoldChar(global::Char.myCharz());
									}
									else
									{
										bool flag13 = GameScr.findCharInMap(num4) != null && num5 != global::Char.myCharz().charID;
										if (flag13)
										{
											GameScr.findCharInMap(num4).holdEffID = num3;
											GameScr.findCharInMap(num5).setHoldChar(GameScr.findCharInMap(num4));
										}
										else
										{
											bool flag14 = GameScr.findCharInMap(num4) != null && num5 == global::Char.myCharz().charID;
											if (flag14)
											{
												GameScr.findCharInMap(num4).holdEffID = num3;
												global::Char.myCharz().setHoldChar(GameScr.findCharInMap(num4));
											}
										}
									}
								}
								else
								{
									bool flag15 = num4 == global::Char.myCharz().charID;
									if (flag15)
									{
										global::Char.myCharz().removeHoleEff();
									}
									else
									{
										bool flag16 = GameScr.findCharInMap(num4) != null;
										if (flag16)
										{
											GameScr.findCharInMap(num4).removeHoleEff();
										}
									}
								}
							}
							bool flag17 = num3 == 33;
							if (flag17)
							{
								bool flag18 = b5 == 1;
								if (flag18)
								{
									bool flag19 = num4 == global::Char.myCharz().charID;
									if (flag19)
									{
										global::Char.myCharz().protectEff = true;
									}
									else
									{
										bool flag20 = GameScr.findCharInMap(num4) != null;
										if (flag20)
										{
											GameScr.findCharInMap(num4).protectEff = true;
										}
									}
								}
								else
								{
									bool flag21 = num4 == global::Char.myCharz().charID;
									if (flag21)
									{
										global::Char.myCharz().removeProtectEff();
									}
									else
									{
										bool flag22 = GameScr.findCharInMap(num4) != null;
										if (flag22)
										{
											GameScr.findCharInMap(num4).removeProtectEff();
										}
									}
								}
							}
							bool flag23 = num3 == 39;
							if (flag23)
							{
								bool flag24 = b5 == 1;
								if (flag24)
								{
									bool flag25 = num4 == global::Char.myCharz().charID;
									if (flag25)
									{
										global::Char.myCharz().huytSao = true;
									}
									else
									{
										bool flag26 = GameScr.findCharInMap(num4) != null;
										if (flag26)
										{
											GameScr.findCharInMap(num4).huytSao = true;
										}
									}
								}
								else
								{
									bool flag27 = num4 == global::Char.myCharz().charID;
									if (flag27)
									{
										global::Char.myCharz().removeHuytSao();
									}
									else
									{
										bool flag28 = GameScr.findCharInMap(num4) != null;
										if (flag28)
										{
											GameScr.findCharInMap(num4).removeHuytSao();
										}
									}
								}
							}
							bool flag29 = num3 == 40;
							if (flag29)
							{
								bool flag30 = b5 == 1;
								if (flag30)
								{
									bool flag31 = num4 == global::Char.myCharz().charID;
									if (flag31)
									{
										global::Char.myCharz().blindEff = true;
									}
									else
									{
										bool flag32 = GameScr.findCharInMap(num4) != null;
										if (flag32)
										{
											GameScr.findCharInMap(num4).blindEff = true;
										}
									}
								}
								else
								{
									bool flag33 = num4 == global::Char.myCharz().charID;
									if (flag33)
									{
										global::Char.myCharz().removeBlindEff();
									}
									else
									{
										bool flag34 = GameScr.findCharInMap(num4) != null;
										if (flag34)
										{
											GameScr.findCharInMap(num4).removeBlindEff();
										}
									}
								}
							}
							bool flag35 = num3 == 41;
							if (flag35)
							{
								bool flag36 = b5 == 1;
								if (flag36)
								{
									bool flag37 = num4 == global::Char.myCharz().charID;
									if (flag37)
									{
										global::Char.myCharz().sleepEff = true;
									}
									else
									{
										bool flag38 = GameScr.findCharInMap(num4) != null;
										if (flag38)
										{
											GameScr.findCharInMap(num4).sleepEff = true;
										}
									}
								}
								else
								{
									bool flag39 = num4 == global::Char.myCharz().charID;
									if (flag39)
									{
										global::Char.myCharz().removeSleepEff();
									}
									else
									{
										bool flag40 = GameScr.findCharInMap(num4) != null;
										if (flag40)
										{
											GameScr.findCharInMap(num4).removeSleepEff();
										}
									}
								}
							}
							bool flag41 = num3 == 42;
							if (flag41)
							{
								bool flag42 = b5 == 1;
								if (flag42)
								{
									bool flag43 = num4 == global::Char.myCharz().charID;
									if (flag43)
									{
										global::Char.myCharz().stone = true;
									}
								}
								else
								{
									bool flag44 = num4 == global::Char.myCharz().charID;
									if (flag44)
									{
										global::Char.myCharz().stone = false;
									}
								}
							}
						}
						bool flag45 = b6 != 1;
						if (!flag45)
						{
							int num6 = (int)msg.reader().readUnsignedByte();
							sbyte b7 = msg.reader().readByte();
							Res.outz(string.Concat(new object[]
							{
								"modbHoldID= ",
								b7,
								" skillID= ",
								num6,
								"eff ID= ",
								b5
							}));
							bool flag46 = num6 == 32;
							if (flag46)
							{
								bool flag47 = b5 == 1;
								if (flag47)
								{
									int num7 = msg.reader().readInt();
									bool flag48 = num7 == global::Char.myCharz().charID;
									if (flag48)
									{
										GameScr.findMobInMap(b7).holdEffID = num6;
										global::Char.myCharz().setHoldMob(GameScr.findMobInMap(b7));
									}
									else
									{
										bool flag49 = GameScr.findCharInMap(num7) != null;
										if (flag49)
										{
											GameScr.findMobInMap(b7).holdEffID = num6;
											GameScr.findCharInMap(num7).setHoldMob(GameScr.findMobInMap(b7));
										}
									}
								}
								else
								{
									GameScr.findMobInMap(b7).removeHoldEff();
								}
							}
							bool flag50 = num6 == 40;
							if (flag50)
							{
								bool flag51 = b5 == 1;
								if (flag51)
								{
									GameScr.findMobInMap(b7).blindEff = true;
								}
								else
								{
									GameScr.findMobInMap(b7).removeBlindEff();
								}
							}
							bool flag52 = num6 == 41;
							if (flag52)
							{
								bool flag53 = b5 == 1;
								if (flag53)
								{
									GameScr.findMobInMap(b7).sleepEff = true;
								}
								else
								{
									GameScr.findMobInMap(b7).removeSleepEff();
								}
							}
						}
						break;
					}
					case -123:
					{
						int charId = msg.reader().readInt();
						bool flag54 = GameScr.findCharInMap(charId) != null;
						if (flag54)
						{
							GameScr.findCharInMap(charId).perCentMp = (int)msg.reader().readByte();
						}
						break;
					}
					case -122:
					{
						short id = msg.reader().readShort();
						Npc npc = GameScr.findNPCInMap(id);
						sbyte b8 = msg.reader().readByte();
						npc.duahau = new int[(int)b8];
						Res.outz("N DUA HAU= " + b8);
						for (int j = 0; j < (int)b8; j++)
						{
							npc.duahau[j] = (int)msg.reader().readShort();
						}
						npc.setStatus(msg.reader().readByte(), msg.reader().readInt());
						break;
					}
					case -121:
					{
						long num8 = mSystem.currentTimeMillis();
						Service.logMap = num8 - Service.curCheckMap;
						Service.gI().sendCheckMap();
						break;
					}
					case -120:
					{
						long num9 = mSystem.currentTimeMillis();
						Service.logController = num9 - Service.curCheckController;
						Service.gI().sendCheckController();
						break;
					}
					case -119:
						global::Char.myCharz().rank = msg.reader().readInt();
						break;
					case -118:
					case -114:
					case -112:
					case -109:
					case -108:
					case -107:
					case -104:
					case -99:
					case -98:
					case -97:
					case -96:
					case -95:
					case -94:
					case -93:
					case -92:
					case -91:
					case -90:
						break;
					case -117:
					{
						GameScr.gI().tMabuEff = 0;
						GameScr.gI().percentMabu = msg.reader().readByte();
						bool flag55 = GameScr.gI().percentMabu == 100;
						if (flag55)
						{
							GameScr.gI().mabuEff = true;
						}
						bool flag56 = GameScr.gI().percentMabu == 101;
						if (flag56)
						{
							Npc.mabuEff = true;
						}
						break;
					}
					case -116:
						GameScr.canAutoPlay = (msg.reader().readByte() == 1);
						break;
					case -115:
						global::Char.myCharz().setPowerInfo(msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort());
						break;
					case -113:
					{
						sbyte[] array = new sbyte[10];
						for (int k = 0; k < 10; k++)
						{
							array[k] = msg.reader().readByte();
							Res.outz("vlue i= " + array[k]);
						}
						GameScr.gI().onKSkill(array);
						GameScr.gI().onOSkill(array);
						GameScr.gI().onCSkill(array);
						break;
					}
					case -111:
					{
						short num10 = msg.reader().readShort();
						ImageSource.vSource = new MyVector();
						for (int l = 0; l < (int)num10; l++)
						{
							string id2 = msg.reader().readUTF();
							sbyte version = msg.reader().readByte();
							ImageSource.vSource.addElement(new ImageSource(id2, version));
						}
						ImageSource.checkRMS();
						ImageSource.saveRMS();
						break;
					}
					case -110:
					{
						sbyte b9 = msg.reader().readByte();
						bool flag57 = b9 == 1;
						if (flag57)
						{
							int num11 = msg.reader().readInt();
							sbyte[] array2 = Rms.loadRMS(num11 + string.Empty);
							bool flag58 = array2 == null;
							if (flag58)
							{
								Service.gI().sendServerData(1, -1, null);
							}
							else
							{
								Service.gI().sendServerData(1, num11, array2);
							}
						}
						bool flag59 = b9 == 0;
						if (flag59)
						{
							int num12 = msg.reader().readInt();
							short num13 = msg.reader().readShort();
							sbyte[] data = new sbyte[(int)num13];
							msg.reader().read(ref data, 0, (int)num13);
							Rms.saveRMS(num12 + string.Empty, data);
						}
						break;
					}
					case -106:
					{
						short num14 = msg.reader().readShort();
						int num15 = (int)msg.reader().readShort();
						bool flag60 = ItemTime.isExistItem((int)num14);
						if (flag60)
						{
							ItemTime.getItemById((int)num14).initTime(num15);
						}
						else
						{
							ItemTime o = new ItemTime(num14, num15);
							global::Char.vItemTime.addElement(o);
						}
						break;
					}
					case -105:
						TransportScr.gI().time = 0;
						TransportScr.gI().maxTime = msg.reader().readShort();
						TransportScr.gI().last = (TransportScr.gI().curr = mSystem.currentTimeMillis());
						TransportScr.gI().type = msg.reader().readByte();
						TransportScr.gI().switchToMe();
						break;
					case -103:
					{
						sbyte b10 = msg.reader().readByte();
						bool flag61 = b10 == 0;
						if (flag61)
						{
							GameCanvas.panel.vFlag.removeAllElements();
							sbyte b11 = msg.reader().readByte();
							for (int m = 0; m < (int)b11; m++)
							{
								Item item = new Item();
								short num16 = msg.reader().readShort();
								bool flag62 = num16 != -1;
								if (flag62)
								{
									item.template = ItemTemplates.get(num16);
									sbyte b12 = msg.reader().readByte();
									bool flag63 = b12 != -1;
									if (flag63)
									{
										item.itemOption = new ItemOption[(int)b12];
										for (int n = 0; n < item.itemOption.Length; n++)
										{
											int num17 = (int)msg.reader().readUnsignedByte();
											int param = (int)msg.reader().readUnsignedShort();
											bool flag64 = num17 != -1;
											if (flag64)
											{
												item.itemOption[n] = new ItemOption(num17, param);
											}
										}
									}
								}
								GameCanvas.panel.vFlag.addElement(item);
							}
							GameCanvas.panel.setTypeFlag();
							GameCanvas.panel.show();
						}
						else
						{
							bool flag65 = b10 == 1;
							if (flag65)
							{
								int num18 = msg.reader().readInt();
								sbyte b13 = msg.reader().readByte();
								Res.outz(string.Concat(new object[]
								{
									"---------------actionFlag1:  ",
									num18,
									" : ",
									b13
								}));
								bool flag66 = num18 == global::Char.myCharz().charID;
								if (flag66)
								{
									global::Char.myCharz().cFlag = b13;
								}
								else
								{
									bool flag67 = GameScr.findCharInMap(num18) != null;
									if (flag67)
									{
										GameScr.findCharInMap(num18).cFlag = b13;
									}
								}
								GameScr.gI().getFlagImage(num18, b13);
							}
							else
							{
								bool flag68 = b10 != 2;
								if (!flag68)
								{
									sbyte b14 = msg.reader().readByte();
									int num19 = (int)msg.reader().readShort();
									PKFlag pkflag = new PKFlag();
									pkflag.cflag = b14;
									pkflag.IDimageFlag = num19;
									GameScr.vFlag.addElement(pkflag);
									for (int num20 = 0; num20 < GameScr.vFlag.size(); num20++)
									{
										PKFlag pkflag2 = (PKFlag)GameScr.vFlag.elementAt(num20);
										Res.outz(string.Concat(new object[]
										{
											"i: ",
											num20,
											"  cflag: ",
											pkflag2.cflag,
											"   IDimageFlag: ",
											pkflag2.IDimageFlag
										}));
									}
									for (int num21 = 0; num21 < GameScr.vCharInMap.size(); num21++)
									{
										global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(num21);
										bool flag69 = @char != null && @char.cFlag == b14;
										if (flag69)
										{
											@char.flagImage = num19;
										}
									}
									bool flag70 = global::Char.myCharz().cFlag == b14;
									if (flag70)
									{
										global::Char.myCharz().flagImage = num19;
									}
								}
							}
						}
						break;
					}
					case -102:
					{
						sbyte b15 = msg.reader().readByte();
						bool flag71 = b15 != 0 && b15 == 1;
						if (flag71)
						{
							GameCanvas.loginScr.isLogin2 = false;
							Service.gI().login(Rms.loadRMSString("acc"), Rms.loadRMSString("pass"), GameMidlet.VERSION, 0);
							LoginScr.isLoggingIn = true;
						}
						break;
					}
					case -101:
					{
						GameCanvas.loginScr.isLogin2 = true;
						GameCanvas.connect();
						string text2 = msg.reader().readUTF();
						Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, text2);
						Service.gI().setClientType();
						Service.gI().login(text2, string.Empty, GameMidlet.VERSION, 1);
						break;
					}
					case -100:
					{
						InfoDlg.hide();
						bool flag72 = false;
						bool flag73 = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
						if (flag73)
						{
							flag72 = true;
						}
						sbyte b16 = msg.reader().readByte();
						Res.outz("t Indxe= " + b16);
						GameCanvas.panel.maxPageShop[(int)b16] = (int)msg.reader().readByte();
						GameCanvas.panel.currPageShop[(int)b16] = (int)msg.reader().readByte();
						Res.outz(string.Concat(new object[]
						{
							"max page= ",
							GameCanvas.panel.maxPageShop[(int)b16],
							" curr page= ",
							GameCanvas.panel.currPageShop[(int)b16]
						}));
						int num22 = (int)msg.reader().readUnsignedByte();
						global::Char.myCharz().arrItemShop[(int)b16] = new Item[num22];
						for (int num23 = 0; num23 < num22; num23++)
						{
							short num24 = msg.reader().readShort();
							bool flag74 = num24 == -1;
							if (!flag74)
							{
								Res.outz("template id= " + num24);
								global::Char.myCharz().arrItemShop[(int)b16][num23] = new Item();
								global::Char.myCharz().arrItemShop[(int)b16][num23].template = ItemTemplates.get(num24);
								global::Char.myCharz().arrItemShop[(int)b16][num23].itemId = (int)msg.reader().readShort();
								global::Char.myCharz().arrItemShop[(int)b16][num23].buyCoin = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[(int)b16][num23].buyGold = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[(int)b16][num23].buyType = msg.reader().readByte();
								global::Char.myCharz().arrItemShop[(int)b16][num23].quantity = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[(int)b16][num23].isMe = msg.reader().readByte();
								Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
								sbyte b17 = msg.reader().readByte();
								bool flag75 = b17 != -1;
								if (flag75)
								{
									global::Char.myCharz().arrItemShop[(int)b16][num23].itemOption = new ItemOption[(int)b17];
									for (int num25 = 0; num25 < global::Char.myCharz().arrItemShop[(int)b16][num23].itemOption.Length; num25++)
									{
										int num26 = (int)msg.reader().readUnsignedByte();
										int param2 = (int)msg.reader().readUnsignedShort();
										bool flag76 = num26 != -1;
										if (flag76)
										{
											global::Char.myCharz().arrItemShop[(int)b16][num23].itemOption[num25] = new ItemOption(num26, param2);
											global::Char.myCharz().arrItemShop[(int)b16][num23].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemShop[(int)b16][num23]);
										}
									}
								}
								sbyte b18 = msg.reader().readByte();
								bool flag77 = b18 == 1;
								if (flag77)
								{
									int headTemp = (int)msg.reader().readShort();
									int bodyTemp = (int)msg.reader().readShort();
									int legTemp = (int)msg.reader().readShort();
									int bagTemp = (int)msg.reader().readShort();
									global::Char.myCharz().arrItemShop[(int)b16][num23].setPartTemp(headTemp, bodyTemp, legTemp, bagTemp);
								}
							}
						}
						bool flag78 = flag72;
						if (flag78)
						{
							GameCanvas.panel2.setTabKiGui();
						}
						GameCanvas.panel.setTabShop();
						GameCanvas.panel.cmy = (GameCanvas.panel.cmtoY = 0);
						break;
					}
					case -89:
						GameCanvas.open3Hour = (msg.reader().readByte() == 1);
						break;
					default:
						if (command != 31)
						{
							if (command == 42)
							{
								GameCanvas.endDlg();
								LoginScr.isContinueToLogin = false;
								global::Char.isLoadingMap = false;
								sbyte haveName = msg.reader().readByte();
								bool flag79 = GameCanvas.registerScr == null;
								if (flag79)
								{
									GameCanvas.registerScr = new RegisterScreen(haveName);
								}
								GameCanvas.registerScr.switchToMe();
							}
						}
						else
						{
							int num27 = msg.reader().readInt();
							sbyte b19 = msg.reader().readByte();
							bool flag80 = b19 == 1;
							if (flag80)
							{
								short smallID = msg.reader().readShort();
								sbyte b20 = -1;
								int[] array3 = null;
								short wimg = 0;
								short himg = 0;
								try
								{
									b20 = msg.reader().readByte();
									bool flag81 = b20 > 0;
									if (flag81)
									{
										sbyte b21 = msg.reader().readByte();
										array3 = new int[(int)b21];
										for (int num28 = 0; num28 < (int)b21; num28++)
										{
											array3[num28] = (int)msg.reader().readByte();
										}
										wimg = msg.reader().readShort();
										himg = msg.reader().readShort();
									}
								}
								catch (Exception)
								{
								}
								bool flag82 = num27 == global::Char.myCharz().charID;
								if (flag82)
								{
									global::Char.myCharz().petFollow = new PetFollow();
									global::Char.myCharz().petFollow.smallID = smallID;
									bool flag83 = b20 > 0;
									if (flag83)
									{
										global::Char.myCharz().petFollow.SetImg((int)b20, array3, (int)wimg, (int)himg);
									}
								}
								else
								{
									global::Char char2 = GameScr.findCharInMap(num27);
									char2.petFollow = new PetFollow();
									char2.petFollow.smallID = smallID;
									bool flag84 = b20 > 0;
									if (flag84)
									{
										char2.petFollow.SetImg((int)b20, array3, (int)wimg, (int)himg);
									}
								}
							}
							else
							{
								bool flag85 = num27 == global::Char.myCharz().charID;
								if (flag85)
								{
									global::Char.myCharz().petFollow.remove();
									global::Char.myCharz().petFollow = null;
								}
								else
								{
									global::Char char3 = GameScr.findCharInMap(num27);
									char3.petFollow.remove();
									char3.petFollow = null;
								}
							}
						}
						break;
					}
				}
				else if (command <= 93)
				{
					switch (command)
					{
					case 48:
					{
						sbyte ipSelect = msg.reader().readByte();
						ServerListScreen.ipSelect = (int)ipSelect;
						GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
						Session_ME.gI().close();
						GameCanvas.endDlg();
						ServerListScreen.waitToLogin = true;
						break;
					}
					case 49:
					case 50:
						break;
					case 51:
					{
						int charId2 = msg.reader().readInt();
						Mabu mabu = (Mabu)GameScr.findCharInMap(charId2);
						sbyte id3 = msg.reader().readByte();
						short x = msg.reader().readShort();
						short y = msg.reader().readShort();
						sbyte b22 = msg.reader().readByte();
						global::Char[] array4 = new global::Char[(int)b22];
						int[] array5 = new int[(int)b22];
						for (int num29 = 0; num29 < (int)b22; num29++)
						{
							int num30 = msg.reader().readInt();
							Res.outz("char ID=" + num30);
							array4[num29] = null;
							bool flag86 = num30 != global::Char.myCharz().charID;
							if (flag86)
							{
								array4[num29] = GameScr.findCharInMap(num30);
							}
							else
							{
								array4[num29] = global::Char.myCharz();
							}
							array5[num29] = msg.reader().readInt();
						}
						mabu.setSkill(id3, x, y, array4, array5);
						break;
					}
					case 52:
					{
						sbyte b23 = msg.reader().readByte();
						bool flag87 = b23 == 1;
						if (flag87)
						{
							int num31 = msg.reader().readInt();
							bool flag88 = num31 == global::Char.myCharz().charID;
							if (flag88)
							{
								global::Char.myCharz().setMabuHold(true);
								global::Char.myCharz().cx = (int)msg.reader().readShort();
								global::Char.myCharz().cy = (int)msg.reader().readShort();
							}
							else
							{
								global::Char char4 = GameScr.findCharInMap(num31);
								bool flag89 = char4 != null;
								if (flag89)
								{
									char4.setMabuHold(true);
									char4.cx = (int)msg.reader().readShort();
									char4.cy = (int)msg.reader().readShort();
								}
							}
						}
						bool flag90 = b23 == 0;
						if (flag90)
						{
							int num32 = msg.reader().readInt();
							bool flag91 = num32 == global::Char.myCharz().charID;
							if (flag91)
							{
								global::Char.myCharz().setMabuHold(false);
							}
							else
							{
								global::Char char5 = GameScr.findCharInMap(num32);
								if (char5 != null)
								{
									char5.setMabuHold(false);
								}
							}
						}
						bool flag92 = b23 == 2;
						if (flag92)
						{
							int charId3 = msg.reader().readInt();
							int id4 = msg.reader().readInt();
							Mabu mabu2 = (Mabu)GameScr.findCharInMap(charId3);
							mabu2.eat(id4);
						}
						bool flag93 = b23 == 3;
						if (flag93)
						{
							GameScr.mabuPercent = msg.reader().readByte();
						}
						break;
					}
					default:
						if (command == 93)
						{
							string text3 = msg.reader().readUTF();
							text3 = Res.changeString(text3);
							GameScr.gI().chatVip(text3);
						}
						break;
					}
				}
				else
				{
					switch (command)
					{
					case 100:
					{
						sbyte b24 = msg.reader().readByte();
						sbyte b25 = msg.reader().readByte();
						Item item2 = null;
						bool flag94 = b24 == 0;
						if (flag94)
						{
							item2 = global::Char.myCharz().arrItemBody[(int)b25];
						}
						bool flag95 = b24 == 1;
						if (flag95)
						{
							item2 = global::Char.myCharz().arrItemBag[(int)b25];
						}
						short num33 = msg.reader().readShort();
						bool flag96 = num33 == -1;
						if (!flag96)
						{
							item2.template = ItemTemplates.get(num33);
							item2.quantity = msg.reader().readInt();
							item2.info = msg.reader().readUTF();
							item2.content = msg.reader().readUTF();
							sbyte b26 = msg.reader().readByte();
							bool flag97 = b26 == 0;
							if (!flag97)
							{
								item2.itemOption = new ItemOption[(int)b26];
								for (int num34 = 0; num34 < item2.itemOption.Length; num34++)
								{
									int num35 = (int)msg.reader().readUnsignedByte();
									Res.outz("id o= " + num35);
									int param3 = (int)msg.reader().readUnsignedShort();
									bool flag98 = num35 != -1;
									if (flag98)
									{
										item2.itemOption[num34] = new ItemOption(num35, param3);
									}
								}
							}
						}
						break;
					}
					case 101:
					{
						Res.outz("big boss--------------------------------------------------");
						BigBoss bigBoss = Mob.getBigBoss();
						bool flag99 = bigBoss == null;
						if (!flag99)
						{
							sbyte b27 = msg.reader().readByte();
							bool flag100 = b27 == 0 || b27 == 1 || b27 == 2 || b27 == 4 || b27 == 3;
							if (flag100)
							{
								bool flag101 = b27 == 3;
								if (flag101)
								{
									bigBoss.xTo = (bigBoss.xFirst = (int)msg.reader().readShort());
									bigBoss.yTo = (bigBoss.yFirst = (int)msg.reader().readShort());
									bigBoss.setFly();
								}
								else
								{
									sbyte b28 = msg.reader().readByte();
									Res.outz("CHUONG nChar= " + b28);
									global::Char[] array6 = new global::Char[(int)b28];
									int[] array7 = new int[(int)b28];
									for (int num36 = 0; num36 < (int)b28; num36++)
									{
										int num37 = msg.reader().readInt();
										Res.outz("char ID=" + num37);
										array6[num36] = null;
										bool flag102 = num37 != global::Char.myCharz().charID;
										if (flag102)
										{
											array6[num36] = GameScr.findCharInMap(num37);
										}
										else
										{
											array6[num36] = global::Char.myCharz();
										}
										array7[num36] = msg.reader().readInt();
									}
									bigBoss.setAttack(array6, array7, b27);
								}
							}
							bool flag103 = b27 == 5;
							if (flag103)
							{
								bigBoss.haftBody = true;
								bigBoss.status = 2;
							}
							bool flag104 = b27 == 6;
							if (flag104)
							{
								bigBoss.getDataB2();
								bigBoss.x = (int)msg.reader().readShort();
								bigBoss.y = (int)msg.reader().readShort();
							}
							bool flag105 = b27 == 7;
							if (flag105)
							{
								bigBoss.setAttack(null, null, b27);
							}
							bool flag106 = b27 == 8;
							if (flag106)
							{
								bigBoss.xTo = (bigBoss.xFirst = (int)msg.reader().readShort());
								bigBoss.yTo = (bigBoss.yFirst = (int)msg.reader().readShort());
								bigBoss.status = 2;
							}
							bool flag107 = b27 == 9;
							if (flag107)
							{
								bigBoss.x = (bigBoss.y = (bigBoss.xTo = (bigBoss.yTo = (bigBoss.xFirst = (bigBoss.yFirst = -1000)))));
							}
						}
						break;
					}
					case 102:
					{
						sbyte b29 = msg.reader().readByte();
						bool flag108 = b29 == 0 || b29 == 1 || b29 == 2 || b29 == 6;
						if (flag108)
						{
							BigBoss2 bigBoss2 = Mob.getBigBoss2();
							bool flag109 = bigBoss2 == null;
							if (flag109)
							{
								break;
							}
							bool flag110 = b29 == 6;
							if (flag110)
							{
								bigBoss2.x = (bigBoss2.y = (bigBoss2.xTo = (bigBoss2.yTo = (bigBoss2.xFirst = (bigBoss2.yFirst = -1000)))));
								break;
							}
							sbyte b30 = msg.reader().readByte();
							global::Char[] array8 = new global::Char[(int)b30];
							int[] array9 = new int[(int)b30];
							for (int num38 = 0; num38 < (int)b30; num38++)
							{
								int num39 = msg.reader().readInt();
								array8[num38] = null;
								bool flag111 = num39 != global::Char.myCharz().charID;
								if (flag111)
								{
									array8[num38] = GameScr.findCharInMap(num39);
								}
								else
								{
									array8[num38] = global::Char.myCharz();
								}
								array9[num38] = msg.reader().readInt();
							}
							bigBoss2.setAttack(array8, array9, b29);
						}
						bool flag112 = b29 == 3 || b29 == 4 || b29 == 5 || b29 == 7;
						if (flag112)
						{
							BachTuoc bachTuoc = Mob.getBachTuoc();
							bool flag113 = bachTuoc == null;
							if (flag113)
							{
								break;
							}
							bool flag114 = b29 == 7;
							if (flag114)
							{
								bachTuoc.x = (bachTuoc.y = (bachTuoc.xTo = (bachTuoc.yTo = (bachTuoc.xFirst = (bachTuoc.yFirst = -1000)))));
								break;
							}
							bool flag115 = b29 == 3 || b29 == 4;
							if (flag115)
							{
								sbyte b31 = msg.reader().readByte();
								global::Char[] array10 = new global::Char[(int)b31];
								int[] array11 = new int[(int)b31];
								for (int num40 = 0; num40 < (int)b31; num40++)
								{
									int num41 = msg.reader().readInt();
									array10[num40] = null;
									bool flag116 = num41 != global::Char.myCharz().charID;
									if (flag116)
									{
										array10[num40] = GameScr.findCharInMap(num41);
									}
									else
									{
										array10[num40] = global::Char.myCharz();
									}
									array11[num40] = msg.reader().readInt();
								}
								bachTuoc.setAttack(array10, array11, b29);
							}
							bool flag117 = b29 == 5;
							if (flag117)
							{
								short xMoveTo = msg.reader().readShort();
								bachTuoc.move(xMoveTo);
							}
						}
						bool flag118 = b29 > 9 && b29 < 30;
						if (flag118)
						{
							Controller2.readActionBoss(msg, (int)b29);
						}
						break;
					}
					default:
						switch (command)
						{
						case 113:
						{
							int loop = (int)msg.reader().readByte();
							int layer = (int)msg.reader().readByte();
							int id5 = (int)msg.reader().readUnsignedByte();
							short x2 = msg.reader().readShort();
							short y2 = msg.reader().readShort();
							short loopCount = msg.reader().readShort();
							EffecMn.addEff(new Effect(id5, (int)x2, (int)y2, layer, loop, (int)loopCount));
							break;
						}
						case 114:
							try
							{
								string text4 = msg.reader().readUTF();
								mSystem.curINAPP = msg.reader().readByte();
								mSystem.maxINAPP = msg.reader().readByte();
							}
							catch (Exception)
							{
							}
							break;
						case 121:
							mSystem.publicID = msg.reader().readUTF();
							mSystem.strAdmob = msg.reader().readUTF();
							Res.outz("SHOW AD public ID= " + mSystem.publicID);
							mSystem.createAdmob();
							break;
						case 122:
						{
							short num42 = msg.reader().readShort();
							Res.outz("second login = " + num42);
							LoginScr.timeLogin = num42;
							LoginScr.currTimeLogin = (LoginScr.lastTimeLogin = mSystem.currentTimeMillis());
							GameCanvas.endDlg();
							break;
						}
						case 123:
						{
							Res.outz("SET POSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSss");
							int num43 = msg.reader().readInt();
							short xPos = msg.reader().readShort();
							short yPos = msg.reader().readShort();
							sbyte b32 = msg.reader().readByte();
							global::Char char6 = null;
							bool flag119 = num43 == global::Char.myCharz().charID;
							if (flag119)
							{
								char6 = global::Char.myCharz();
							}
							else
							{
								bool flag120 = GameScr.findCharInMap(num43) != null;
								if (flag120)
								{
									char6 = GameScr.findCharInMap(num43);
								}
							}
							bool flag121 = char6 != null;
							if (flag121)
							{
								ServerEffect.addServerEffect((b32 != 0) ? 173 : 60, char6, 1);
								char6.setPos(xPos, yPos, b32);
							}
							break;
						}
						case 124:
						{
							short num44 = msg.reader().readShort();
							string text5 = msg.reader().readUTF();
							Res.outz(string.Concat(new object[]
							{
								"noi chuyen = ",
								text5,
								"npc ID= ",
								num44
							}));
							Npc npc2 = GameScr.findNPCInMap(num44);
							if (npc2 != null)
							{
								npc2.addInfo(text5);
							}
							break;
						}
						case 125:
						{
							sbyte fusion = msg.reader().readByte();
							int num45 = msg.reader().readInt();
							bool flag122 = num45 == global::Char.myCharz().charID;
							if (flag122)
							{
								global::Char.myCharz().setFusion(fusion);
							}
							else
							{
								bool flag123 = GameScr.findCharInMap(num45) != null;
								if (flag123)
								{
									GameScr.findCharInMap(num45).setFusion(fusion);
								}
							}
							break;
						}
						case 127:
							Controller2.readInfoRada(msg);
							break;
						}
						break;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000ACE30 File Offset: 0x000AB030
		private static void readLuckyRound(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				bool flag = b == 0;
				if (flag)
				{
					sbyte b2 = msg.reader().readByte();
					short[] array = new short[(int)b2];
					for (int i = 0; i < (int)b2; i++)
					{
						array[i] = msg.reader().readShort();
					}
					sbyte b3 = msg.reader().readByte();
					int price = msg.reader().readInt();
					short idTicket = msg.reader().readShort();
					CrackBallScr.gI().SetCrackBallScr(array, (byte)b3, price, idTicket);
				}
				else
				{
					bool flag2 = b == 1;
					if (flag2)
					{
						sbyte b4 = msg.reader().readByte();
						short[] array2 = new short[(int)b4];
						for (int j = 0; j < (int)b4; j++)
						{
							array2[j] = msg.reader().readShort();
						}
						CrackBallScr.gI().DoneCrackBallScr(array2);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x000ACF3C File Offset: 0x000AB13C
		private static void readInfoRada(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				bool flag = b == 0;
				if (flag)
				{
					RadarScr.gI();
					MyVector myVector = new MyVector(string.Empty);
					short num = msg.reader().readShort();
					int num2 = 0;
					for (int i = 0; i < (int)num; i++)
					{
						Info_RadaScr info_RadaScr = new Info_RadaScr();
						int id = (int)msg.reader().readShort();
						int no = i + 1;
						int idIcon = (int)msg.reader().readShort();
						sbyte rank = msg.reader().readByte();
						sbyte amount = msg.reader().readByte();
						sbyte max_amount = msg.reader().readByte();
						short templateId = -1;
						global::Char charInfo = null;
						sbyte b2 = msg.reader().readByte();
						bool flag2 = b2 == 0;
						if (flag2)
						{
							templateId = msg.reader().readShort();
						}
						else
						{
							int head = (int)msg.reader().readShort();
							int body = (int)msg.reader().readShort();
							int leg = (int)msg.reader().readShort();
							int bag = (int)msg.reader().readShort();
							charInfo = Info_RadaScr.SetCharInfo(head, body, leg, bag);
						}
						string name = msg.reader().readUTF();
						string info = msg.reader().readUTF();
						sbyte b3 = msg.reader().readByte();
						sbyte use = msg.reader().readByte();
						sbyte b4 = msg.reader().readByte();
						ItemOption[] array = null;
						bool flag3 = b4 != 0;
						if (flag3)
						{
							array = new ItemOption[(int)b4];
							for (int j = 0; j < array.Length; j++)
							{
								int num3 = (int)msg.reader().readUnsignedByte();
								int param = (int)msg.reader().readUnsignedShort();
								sbyte activeCard = msg.reader().readByte();
								bool flag4 = num3 != -1;
								if (flag4)
								{
									array[j] = new ItemOption(num3, param);
									array[j].activeCard = activeCard;
								}
							}
						}
						info_RadaScr.SetInfo(id, no, idIcon, rank, b2, templateId, name, info, charInfo, array);
						info_RadaScr.SetLevel(b3);
						info_RadaScr.SetUse(use);
						info_RadaScr.SetAmount(amount, max_amount);
						myVector.addElement(info_RadaScr);
						bool flag5 = b3 > 0;
						if (flag5)
						{
							num2++;
						}
					}
					RadarScr.gI().SetRadarScr(myVector, num2, (int)num);
					RadarScr.gI().switchToMe();
				}
				else
				{
					bool flag6 = b == 1;
					if (flag6)
					{
						int id2 = (int)msg.reader().readShort();
						sbyte use2 = msg.reader().readByte();
						bool flag7 = Info_RadaScr.GetInfo(RadarScr.list, id2) != null;
						if (flag7)
						{
							Info_RadaScr.GetInfo(RadarScr.list, id2).SetUse(use2);
						}
						RadarScr.SetListUse();
					}
					else
					{
						bool flag8 = b == 2;
						if (flag8)
						{
							int num4 = (int)msg.reader().readShort();
							sbyte level = msg.reader().readByte();
							int num5 = 0;
							for (int k = 0; k < RadarScr.list.size(); k++)
							{
								Info_RadaScr info_RadaScr2 = (Info_RadaScr)RadarScr.list.elementAt(k);
								bool flag9 = info_RadaScr2 != null;
								if (flag9)
								{
									bool flag10 = info_RadaScr2.id == num4;
									if (flag10)
									{
										info_RadaScr2.SetLevel(level);
									}
									bool flag11 = info_RadaScr2.level > 0;
									if (flag11)
									{
										num5++;
									}
								}
							}
							RadarScr.SetNum(num5, RadarScr.list.size());
							bool flag12 = Info_RadaScr.GetInfo(RadarScr.listUse, num4) != null;
							if (flag12)
							{
								Info_RadaScr.GetInfo(RadarScr.listUse, num4).SetLevel(level);
							}
						}
						else
						{
							bool flag13 = b == 3;
							if (flag13)
							{
								int id3 = (int)msg.reader().readShort();
								sbyte amount2 = msg.reader().readByte();
								sbyte max_amount2 = msg.reader().readByte();
								bool flag14 = Info_RadaScr.GetInfo(RadarScr.list, id3) != null;
								if (flag14)
								{
									Info_RadaScr.GetInfo(RadarScr.list, id3).SetAmount(amount2, max_amount2);
								}
								bool flag15 = Info_RadaScr.GetInfo(RadarScr.listUse, id3) != null;
								if (flag15)
								{
									Info_RadaScr.GetInfo(RadarScr.listUse, id3).SetAmount(amount2, max_amount2);
								}
							}
							else
							{
								bool flag16 = b == 4;
								if (flag16)
								{
									int num6 = msg.reader().readInt();
									short idAuraEff = msg.reader().readShort();
									global::Char @char = (num6 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num6) : global::Char.myCharz();
									bool flag17 = @char != null;
									if (flag17)
									{
										@char.idAuraEff = idAuraEff;
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

		// Token: 0x06000AFC RID: 2812 RVA: 0x000AD3F4 File Offset: 0x000AB5F4
		private static void readInfoEffChar(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				int num = msg.reader().readInt();
				global::Char @char = (num != global::Char.myCharz().charID) ? GameScr.findCharInMap(num) : global::Char.myCharz();
				bool flag = b == 0;
				if (flag)
				{
					int id = (int)msg.reader().readShort();
					int layer = (int)msg.reader().readByte();
					int loop = (int)msg.reader().readByte();
					short loopCount = msg.reader().readShort();
					sbyte isStand = msg.reader().readByte();
					if (@char != null)
					{
						@char.addEffChar(new Effect(id, @char, layer, loop, (int)loopCount, isStand));
					}
				}
				else
				{
					bool flag2 = b == 1;
					if (flag2)
					{
						int id2 = (int)msg.reader().readShort();
						if (@char != null)
						{
							@char.removeEffChar(0, id2);
						}
					}
					else
					{
						bool flag3 = b == 2;
						if (flag3)
						{
							if (@char != null)
							{
								@char.removeEffChar(-1, 0);
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000AD500 File Offset: 0x000AB700
		private static void readActionBoss(Message msg, int actionBoss)
		{
			try
			{
				sbyte idBoss = msg.reader().readByte();
				NewBoss newBoss = Mob.getNewBoss(idBoss);
				bool flag = newBoss == null;
				if (!flag)
				{
					bool flag2 = actionBoss == 10;
					if (flag2)
					{
						short xMoveTo = msg.reader().readShort();
						short yMoveTo = msg.reader().readShort();
						newBoss.move(xMoveTo, yMoveTo);
					}
					bool flag3 = actionBoss >= 11 && actionBoss <= 20;
					if (flag3)
					{
						sbyte b = msg.reader().readByte();
						global::Char[] array = new global::Char[(int)b];
						int[] array2 = new int[(int)b];
						for (int i = 0; i < (int)b; i++)
						{
							int num = msg.reader().readInt();
							array[i] = null;
							bool flag4 = num != global::Char.myCharz().charID;
							if (flag4)
							{
								array[i] = GameScr.findCharInMap(num);
							}
							else
							{
								array[i] = global::Char.myCharz();
							}
							array2[i] = msg.reader().readInt();
						}
						sbyte dir = msg.reader().readByte();
						newBoss.setAttack(array, array2, (sbyte)(actionBoss - 10), dir);
					}
					bool flag5 = actionBoss == 21;
					if (flag5)
					{
						newBoss.xTo = (int)msg.reader().readShort();
						newBoss.yTo = (int)msg.reader().readShort();
						newBoss.setFly();
					}
					bool flag6 = actionBoss == 22;
					if (flag6)
					{
					}
					bool flag7 = actionBoss == 23;
					if (flag7)
					{
						newBoss.setDie();
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
