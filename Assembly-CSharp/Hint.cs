using System;

// Token: 0x02000039 RID: 57
public class Hint
{
	// Token: 0x06000357 RID: 855 RVA: 0x0004F778 File Offset: 0x0004D978
	public static bool isOnTask(int tastId, int index)
	{
		return global::Char.myCharz().taskMaint != null && (int)global::Char.myCharz().taskMaint.taskId == tastId && global::Char.myCharz().taskMaint.index == index;
	}

	// Token: 0x06000358 RID: 856 RVA: 0x0004F7C8 File Offset: 0x0004D9C8
	public static bool isPaintz()
	{
		bool flag = Hint.isOnTask(0, 3) && GameCanvas.panel.currentTabIndex == 0 && (GameCanvas.panel.cmy < 0 || GameCanvas.panel.cmy > 30);
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = Hint.isOnTask(2, 0) && GameCanvas.panel.isShow && GameCanvas.panel.currentTabIndex != 0;
			result = !flag2;
		}
		return result;
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0004F848 File Offset: 0x0004DA48
	public static void clickNpc()
	{
		bool flag = GameCanvas.panel.isShow;
		if (flag)
		{
			Hint.isPaint = false;
		}
		bool flag2 = GameScr.getNpcTask() != null;
		if (flag2)
		{
			Hint.x = GameScr.getNpcTask().cx;
			Hint.y = GameScr.getNpcTask().cy;
			Hint.trans = 0;
			Hint.isCamera = true;
			Hint.type = (GameCanvas.isTouch ? 1 : 0);
		}
	}

	// Token: 0x0600035A RID: 858 RVA: 0x0004F8B4 File Offset: 0x0004DAB4
	public static void nextMap(int index)
	{
		bool flag = !GameCanvas.panel.isShow && PopUp.vPopups.size() - 1 >= index;
		if (flag)
		{
			PopUp popUp = (PopUp)PopUp.vPopups.elementAt(index);
			Hint.x = popUp.cx + popUp.sayWidth / 2;
			Hint.y = popUp.cy + 30;
			bool flag2 = popUp.isHide || !popUp.isPaint;
			if (flag2)
			{
				Hint.isPaint = false;
			}
			else
			{
				Hint.isPaint = true;
			}
			Hint.type = 0;
			Hint.isCamera = true;
			Hint.trans = 0;
			bool flag3 = !GameCanvas.isTouch;
			if (flag3)
			{
				Hint.isPaint = false;
			}
		}
	}

	// Token: 0x0600035B RID: 859 RVA: 0x0004F970 File Offset: 0x0004DB70
	public static void clickMob()
	{
		Hint.type = 1;
		bool flag = GameCanvas.panel.isShow;
		if (flag)
		{
			Hint.isPaint = false;
		}
		bool flag2 = false;
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			bool isHintFocus = mob.isHintFocus;
			if (isHintFocus)
			{
				flag2 = true;
				break;
			}
		}
		for (int j = 0; j < GameScr.vMob.size(); j++)
		{
			Mob mob2 = (Mob)GameScr.vMob.elementAt(j);
			bool isHintFocus2 = mob2.isHintFocus;
			if (isHintFocus2)
			{
				Hint.x = mob2.x;
				Hint.y = mob2.y + 5;
				Hint.isCamera = true;
				bool flag3 = mob2.status == 0;
				if (flag3)
				{
					mob2.isHintFocus = false;
				}
				break;
			}
			bool flag4 = !flag2;
			if (flag4)
			{
				bool flag5 = mob2.status != 0;
				if (flag5)
				{
					mob2.isHintFocus = true;
					break;
				}
				mob2.isHintFocus = false;
			}
		}
	}

	// Token: 0x0600035C RID: 860 RVA: 0x0004FA90 File Offset: 0x0004DC90
	public static bool isHaveItem()
	{
		bool flag = GameCanvas.panel.isShow;
		if (flag)
		{
			Hint.isPaint = false;
		}
		for (int i = 0; i < GameScr.vItemMap.size(); i++)
		{
			ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
			bool flag2 = itemMap.playerId == global::Char.myCharz().charID && itemMap.template.id == 73;
			if (flag2)
			{
				Hint.type = 1;
				Hint.x = itemMap.x;
				Hint.y = itemMap.y + 5;
				Hint.isCamera = true;
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600035D RID: 861 RVA: 0x0004FB3C File Offset: 0x0004DD3C
	public static void paintArrowPointToHint(mGraphics g)
	{
		try
		{
			bool flag = !Hint.isPaintArrow || (Hint.x > GameScr.cmx && Hint.x < GameScr.cmx + GameScr.gW && Hint.y > GameScr.cmy && Hint.y < GameScr.cmy + GameScr.gH) || GameCanvas.gameTick % 10 < 5 || ChatPopup.currChatPopup != null || ChatPopup.serverChatPopUp != null || GameCanvas.panel.isShow || !Hint.isCamera;
			if (!flag)
			{
				int num = Hint.x - global::Char.myCharz().cx;
				int num2 = Hint.y - global::Char.myCharz().cy;
				int num3 = 0;
				int num4 = 0;
				int arg = 0;
				bool flag2 = num > 0 && num2 >= 0;
				if (flag2)
				{
					bool flag3 = Res.abs(num) >= Res.abs(num2);
					if (flag3)
					{
						num3 = GameScr.gW - 10;
						num4 = GameScr.gH / 2 + 30;
						bool isTouch = GameCanvas.isTouch;
						if (isTouch)
						{
							num4 = GameScr.gH / 2 + 10;
						}
						arg = 0;
					}
					else
					{
						num3 = GameScr.gW / 2;
						num4 = GameScr.gH - 10;
						arg = 5;
					}
				}
				else
				{
					bool flag4 = num >= 0 && num2 < 0;
					if (flag4)
					{
						bool flag5 = Res.abs(num) >= Res.abs(num2);
						if (flag5)
						{
							num3 = GameScr.gW - 10;
							num4 = GameScr.gH / 2 + 30;
							bool isTouch2 = GameCanvas.isTouch;
							if (isTouch2)
							{
								num4 = GameScr.gH / 2 + 10;
							}
							arg = 0;
						}
						else
						{
							num3 = GameScr.gW / 2;
							num4 = 10;
							arg = 6;
						}
					}
				}
				bool flag6 = num < 0 && num2 >= 0;
				if (flag6)
				{
					bool flag7 = Res.abs(num) >= Res.abs(num2);
					if (flag7)
					{
						num3 = 10;
						num4 = GameScr.gH / 2 + 30;
						bool isTouch3 = GameCanvas.isTouch;
						if (isTouch3)
						{
							num4 = GameScr.gH / 2 + 10;
						}
						arg = 3;
					}
					else
					{
						num3 = GameScr.gW / 2;
						num4 = GameScr.gH - 10;
						arg = 5;
					}
				}
				else
				{
					bool flag8 = num <= 0 && num2 < 0;
					if (flag8)
					{
						bool flag9 = Res.abs(num) >= Res.abs(num2);
						if (flag9)
						{
							num3 = 10;
							num4 = GameScr.gH / 2 + 30;
							bool isTouch4 = GameCanvas.isTouch;
							if (isTouch4)
							{
								num4 = GameScr.gH / 2 + 10;
							}
							arg = 3;
						}
						else
						{
							num3 = GameScr.gW / 2;
							num4 = 10;
							arg = 6;
						}
					}
				}
				GameScr.resetTranslate(g);
				g.drawRegion(GameScr.arrow, 0, 0, 13, 16, arg, num3, num4, StaticObj.VCENTER_HCENTER);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600035E RID: 862 RVA: 0x0004FDF8 File Offset: 0x0004DFF8
	public static void paint(mGraphics g)
	{
		bool flag = ChatPopup.serverChatPopUp != null || global::Char.myCharz().isUsePlane || global::Char.myCharz().isTeleport;
		if (!flag)
		{
			Hint.paintArrowPointToHint(g);
			bool flag2 = GameCanvas.menu.tDelay == 0 && Hint.isPaint && ChatPopup.scr == null && !global::Char.ischangingMap && GameCanvas.currentScreen == GameScr.gI() && (!GameCanvas.panel.isShow || GameCanvas.panel.cmx == 0);
			if (flag2)
			{
				bool flag3 = Hint.isCamera;
				if (flag3)
				{
					g.translate(-GameScr.cmx, -GameScr.cmy);
				}
				bool flag4 = Hint.trans == 0;
				if (flag4)
				{
					g.drawImage(Panel.imgBantay, Hint.x - 15, Hint.y, 0);
				}
				bool flag5 = Hint.trans == 1;
				if (flag5)
				{
					g.drawRegion(Panel.imgBantay, 0, 0, 14, 16, 2, Hint.x + 15, Hint.y, StaticObj.TOP_RIGHT);
				}
				bool flag6 = Hint.paintFlare;
				if (flag6)
				{
					g.drawImage(ItemMap.imageFlare, Hint.x, Hint.y, 3);
				}
			}
		}
	}

	// Token: 0x0600035F RID: 863 RVA: 0x0004FF2C File Offset: 0x0004E12C
	public static void hint()
	{
		bool flag = global::Char.myCharz().taskMaint != null && GameCanvas.currentScreen == GameScr.instance;
		if (flag)
		{
			int taskId = (int)global::Char.myCharz().taskMaint.taskId;
			int index = global::Char.myCharz().taskMaint.index;
			Hint.isCamera = false;
			Hint.trans = 0;
			Hint.type = 0;
			Hint.isPaint = true;
			Hint.isPaintArrow = true;
			bool flag2 = GameCanvas.menu.showMenu && taskId > 0;
			if (flag2)
			{
				Hint.isPaint = false;
			}
			switch (taskId)
			{
			case 0:
			{
				bool flag3 = ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14;
				if (flag3)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
				}
				else
				{
					bool flag4 = index == 0 && TileMap.vGo.size() != 0;
					if (flag4)
					{
						Hint.x = (int)(((Waypoint)TileMap.vGo.elementAt(0)).minX - 100);
						Hint.y = (int)(((Waypoint)TileMap.vGo.elementAt(0)).minY + 40);
						Hint.isCamera = true;
					}
					bool flag5 = index == 1;
					if (flag5)
					{
						Hint.nextMap(0);
					}
					bool flag6 = index == 2;
					if (flag6)
					{
						Hint.clickNpc();
					}
					bool flag7 = index == 3;
					if (flag7)
					{
						bool flag8 = !GameCanvas.panel.isShow;
						if (flag8)
						{
							Hint.clickNpc();
						}
						else
						{
							bool flag9 = GameCanvas.panel.currentTabIndex == 0;
							if (flag9)
							{
								bool flag10 = GameCanvas.panel.cp == null;
								if (flag10)
								{
									Hint.x = GameCanvas.panel.xScroll + GameCanvas.panel.wScroll / 2;
									Hint.y = GameCanvas.panel.yScroll + 20;
								}
								else
								{
									bool flag11 = GameCanvas.menu.tDelay != 0;
									if (flag11)
									{
										Hint.x = GameCanvas.panel.xScroll + 25;
										Hint.y = GameCanvas.panel.yScroll + 60;
									}
								}
							}
							else
							{
								bool flag12 = GameCanvas.panel.currentTabIndex == 1;
								if (flag12)
								{
									Hint.x = GameCanvas.panel.startTabPos + 10;
									Hint.y = 65;
								}
							}
						}
					}
					bool flag13 = index == 4;
					if (flag13)
					{
						bool flag14 = GameCanvas.panel.isShow;
						if (flag14)
						{
							Hint.x = GameCanvas.panel.cmdClose.x + 5;
							Hint.y = GameCanvas.panel.cmdClose.y + 5;
						}
						else
						{
							bool showMenu = GameCanvas.menu.showMenu;
							if (showMenu)
							{
								Hint.x = GameCanvas.w / 2;
								Hint.y = GameCanvas.h - 20;
							}
							else
							{
								Hint.clickNpc();
							}
						}
					}
					bool flag15 = index == 5;
					if (flag15)
					{
						Hint.clickNpc();
					}
				}
				break;
			}
			case 1:
			{
				bool flag16 = ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14;
				if (flag16)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
				}
				else
				{
					bool flag17 = index == 0;
					if (flag17)
					{
						bool flag18 = TileMap.isOfflineMap();
						if (flag18)
						{
							Hint.nextMap(0);
						}
						else
						{
							Hint.clickMob();
						}
					}
					bool flag19 = index == 1;
					if (flag19)
					{
						bool flag20 = !TileMap.isOfflineMap();
						if (flag20)
						{
							Hint.nextMap(1);
						}
						else
						{
							Hint.clickNpc();
						}
					}
				}
				break;
			}
			case 2:
			{
				bool flag21 = ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14;
				if (flag21)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
				}
				else
				{
					bool flag22 = index == 0;
					if (flag22)
					{
						bool flag23 = !TileMap.isOfflineMap();
						if (flag23)
						{
							Hint.isViewMap = true;
						}
						bool flag24 = !GameCanvas.panel.isShow;
						if (flag24)
						{
							bool flag25 = !Hint.isViewMap;
							if (flag25)
							{
								Hint.x = GameScr.gI().cmdMenu.x;
								Hint.y = GameScr.gI().cmdMenu.y + 13;
								Hint.trans = 1;
							}
							else
							{
								bool flag26 = GameScr.getTaskMapId() == TileMap.mapID;
								if (flag26)
								{
									bool flag27 = !Hint.isHaveItem();
									if (flag27)
									{
										Hint.clickMob();
									}
								}
								else
								{
									Hint.nextMap(0);
								}
								bool flag28 = Hint.isViewMap;
								if (flag28)
								{
									Hint.isCloseMap = true;
								}
							}
						}
						else
						{
							bool flag29 = !Hint.isViewMap;
							if (flag29)
							{
								bool flag30 = GameCanvas.panel.currentTabIndex == 0;
								if (flag30)
								{
									int num = (GameCanvas.h <= 300) ? 10 : 15;
									Hint.x = GameCanvas.panel.xScroll + GameCanvas.panel.wScroll / 2;
									Hint.y = GameCanvas.panel.yScroll + GameCanvas.panel.hScroll - num;
								}
								else
								{
									Hint.x = GameCanvas.panel.startTabPos + 10;
									Hint.y = 65;
								}
							}
							else
							{
								bool flag31 = !Hint.isCloseMap;
								if (flag31)
								{
									Hint.x = GameCanvas.panel.cmdClose.x + 5;
									Hint.y = GameCanvas.panel.cmdClose.y + 5;
								}
								else
								{
									Hint.isPaint = false;
								}
							}
						}
						bool flag32 = global::Char.myCharz().cMP <= 0;
						if (flag32)
						{
							Hint.x = GameScr.xHP + 5;
							Hint.y = GameScr.yHP + 13;
							Hint.isCamera = false;
						}
					}
					bool flag33 = index == 1;
					if (flag33)
					{
						Hint.isPaint = false;
						Hint.isPaintArrow = false;
					}
				}
				break;
			}
			default:
			{
				bool flag34 = global::Char.myCharz().taskMaint.taskId == 9 && global::Char.myCharz().taskMaint.index == 2;
				if (flag34)
				{
					for (int i = 0; i < PopUp.vPopups.size(); i++)
					{
						PopUp popUp = (PopUp)PopUp.vPopups.elementAt(i);
						bool flag35 = popUp.cy <= 24;
						if (flag35)
						{
							Hint.x = popUp.cx + popUp.sayWidth / 2;
							Hint.y = popUp.cy + 30;
							Hint.isCamera = true;
							Hint.isPaint = false;
							Hint.isPaintArrow = true;
							return;
						}
					}
				}
				Hint.isPaint = false;
				Hint.isPaintArrow = false;
				break;
			}
			}
		}
		else
		{
			Hint.isPaint = false;
			Hint.isPaintArrow = false;
		}
	}

	// Token: 0x06000360 RID: 864 RVA: 0x000505C4 File Offset: 0x0004E7C4
	public static void update()
	{
		Hint.hint();
		int num = (Hint.trans != 0) ? -2 : 2;
		bool flag = !Hint.activeClick;
		if (flag)
		{
			Hint.paintFlare = false;
			Hint.t++;
			bool flag2 = Hint.t == 50;
			if (flag2)
			{
				Hint.t = 0;
				Hint.activeClick = true;
			}
		}
		else
		{
			Hint.t++;
			bool flag3 = Hint.type == 0;
			if (flag3)
			{
				bool flag4 = Hint.t == 2;
				if (flag4)
				{
					Hint.x += 2 * num;
					Hint.y -= 4;
					Hint.paintFlare = true;
				}
				bool flag5 = Hint.t == 4;
				if (flag5)
				{
					Hint.x -= 2 * num;
					Hint.y += 4;
					Hint.activeClick = false;
					Hint.paintFlare = false;
					Hint.t = 0;
				}
				bool flag6 = Hint.t > 4;
				if (flag6)
				{
					Hint.activeClick = false;
				}
			}
			bool flag7 = Hint.type != 1;
			if (!flag7)
			{
				bool flag8 = Hint.t == 2;
				if (flag8)
				{
					bool isTouch = GameCanvas.isTouch;
					if (isTouch)
					{
						GameScr.startFlyText(mResources.press_twice, Hint.x, Hint.y + 10, 0, 20, mFont.MISS_ME);
					}
					Hint.paintFlare = true;
					Hint.x += 2 * num;
					Hint.y -= 4;
				}
				bool flag9 = Hint.t == 4;
				if (flag9)
				{
					Hint.paintFlare = false;
					Hint.x -= num;
					Hint.y += 2;
				}
				bool flag10 = Hint.t == 6;
				if (flag10)
				{
					Hint.paintFlare = true;
					Hint.x += num;
					Hint.y -= 2;
				}
				bool flag11 = Hint.t == 8;
				if (flag11)
				{
					Hint.paintFlare = false;
					Hint.x -= num;
					Hint.y += 2;
				}
				bool flag12 = Hint.t == 10;
				if (flag12)
				{
					Hint.x -= num;
					Hint.y += 2;
					Hint.activeClick = false;
					Hint.t = 0;
				}
			}
		}
	}

	// Token: 0x040007F3 RID: 2035
	public static int x;

	// Token: 0x040007F4 RID: 2036
	public static int y;

	// Token: 0x040007F5 RID: 2037
	public static int type;

	// Token: 0x040007F6 RID: 2038
	public static int t;

	// Token: 0x040007F7 RID: 2039
	public static int xF;

	// Token: 0x040007F8 RID: 2040
	public static int yF;

	// Token: 0x040007F9 RID: 2041
	public static bool isShow;

	// Token: 0x040007FA RID: 2042
	public static bool activeClick;

	// Token: 0x040007FB RID: 2043
	public static bool isViewMap;

	// Token: 0x040007FC RID: 2044
	public static bool isCloseMap;

	// Token: 0x040007FD RID: 2045
	public static bool isPaint;

	// Token: 0x040007FE RID: 2046
	public static bool isCamera;

	// Token: 0x040007FF RID: 2047
	public static int trans;

	// Token: 0x04000800 RID: 2048
	public static bool paintFlare;

	// Token: 0x04000801 RID: 2049
	public static bool isPaintArrow;

	// Token: 0x04000802 RID: 2050
	private int s = 2;
}
