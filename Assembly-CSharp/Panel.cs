using System;
using AssemblyCSharp.Mod.Xmap;
using Assets.src.g;
using UnityEngine;

// Token: 0x02000083 RID: 131
public class Panel : IActionListener, IChatable
{
	// Token: 0x06000664 RID: 1636 RVA: 0x00068C80 File Offset: 0x00066E80
	public Panel()
	{
		this.init();
		this.cmdClose = new Command(string.Empty, this, 1003, null);
		this.cmdClose.img = GameCanvas.loadImage("/mainImage/myTexture2dbtX.png");
		this.cmdClose.cmdClosePanel = true;
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x00069050 File Offset: 0x00067250
	public static void loadBg()
	{
		Panel.imgMap = GameCanvas.loadImage("/img/map" + TileMap.planetID + ".png");
		Panel.imgBantay = GameCanvas.loadImage("/mainImage/myTexture2dbantay.png");
		Panel.imgX = GameCanvas.loadImage("/mainImage/myTexture2dbtX.png");
		Panel.imgXu = GameCanvas.loadImage("/mainImage/myTexture2dimgMoney.png");
		Panel.imgLuong = GameCanvas.loadImage("/mainImage/myTexture2dimgDiamond.png");
		Panel.imgLuongKhoa = GameCanvas.loadImage("/mainImage/luongkhoa.png");
		Panel.imgUp = GameCanvas.loadImage("/mainImage/myTexture2dup.png");
		Panel.imgDown = GameCanvas.loadImage("/mainImage/myTexture2ddown.png");
		Panel.imgStar = GameCanvas.loadImage("/mainImage/star.png");
		Panel.imgMaxStar = GameCanvas.loadImage("/mainImage/starE.png");
		Panel.imgStar8 = GameCanvas.loadImage("/mainImage/star8.png");
		Panel.imgNew = GameCanvas.loadImage("/mainImage/new.png");
		Panel.imgTicket = GameCanvas.loadImage("/mainImage/ticket12.png");
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x00069138 File Offset: 0x00067338
	public void init()
	{
		this.pX = GameCanvas.pxLast + this.cmxMap;
		this.pY = GameCanvas.pyLast + this.cmyMap;
		this.lastTabIndex = new int[this.tabName.Length];
		for (int i = 0; i < this.lastTabIndex.Length; i++)
		{
			this.lastTabIndex[i] = -1;
		}
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x000691A0 File Offset: 0x000673A0
	public int getXMap()
	{
		for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
		{
			bool flag = TileMap.mapID == Panel.mapId[(int)TileMap.planetID][i];
			if (flag)
			{
				return Panel.mapX[(int)TileMap.planetID][i];
			}
		}
		return -1;
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x000691FC File Offset: 0x000673FC
	public int getYMap()
	{
		for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
		{
			bool flag = TileMap.mapID == Panel.mapId[(int)TileMap.planetID][i];
			if (flag)
			{
				return Panel.mapY[(int)TileMap.planetID][i];
			}
		}
		return -1;
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x00069258 File Offset: 0x00067458
	public int getXMapTask()
	{
		bool flag = global::Char.myCharz().taskMaint == null;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
			{
				bool flag2 = GameScr.mapTasks[global::Char.myCharz().taskMaint.index] == Panel.mapId[(int)TileMap.planetID][i];
				if (flag2)
				{
					return Panel.mapX[(int)TileMap.planetID][i];
				}
			}
			result = -1;
		}
		return result;
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x000692DC File Offset: 0x000674DC
	public int getYMapTask()
	{
		bool flag = global::Char.myCharz().taskMaint == null;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
			{
				bool flag2 = GameScr.mapTasks[global::Char.myCharz().taskMaint.index] == Panel.mapId[(int)TileMap.planetID][i];
				if (flag2)
				{
					return Panel.mapY[(int)TileMap.planetID][i];
				}
			}
			result = -1;
		}
		return result;
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x00069360 File Offset: 0x00067560
	private void setType(int position)
	{
		this.typeShop = -1;
		this.W = Panel.WIDTH_PANEL;
		this.H = GameCanvas.h;
		this.X = 0;
		this.Y = 0;
		this.ITEM_HEIGHT = 24;
		this.position = position;
		if (position != 0)
		{
			if (position == 1)
			{
				this.wScroll = this.W - 4;
				this.xScroll = GameCanvas.w - this.wScroll;
				this.yScroll = 80;
				this.hScroll = this.H - 96;
				this.X = this.xScroll - 2;
				this.cmx = -(GameCanvas.w + this.W);
				this.cmtoX = GameCanvas.w - this.W;
			}
		}
		else
		{
			this.xScroll = 2;
			this.yScroll = 80;
			this.wScroll = this.W - 4;
			this.hScroll = this.H - 96;
			this.cmx = this.wScroll;
			this.cmtoX = 0;
			this.X = 0;
		}
		this.TAB_W = this.W / 5 - 1;
		this.currentTabIndex = 0;
		this.currentTabName = this.tabName[this.type];
		bool flag = this.currentTabName.Length < 5;
		if (flag)
		{
			this.TAB_W += 5;
		}
		this.startTabPos = this.xScroll + this.wScroll / 2 - this.currentTabName.Length * this.TAB_W / 2;
		this.lastSelect = new int[this.currentTabName.Length];
		this.cmyLast = new int[this.currentTabName.Length];
		for (int i = 0; i < this.currentTabName.Length; i++)
		{
			this.lastSelect[i] = (GameCanvas.isTouch ? -1 : 0);
		}
		bool flag2 = this.lastTabIndex[this.type] != -1;
		if (flag2)
		{
			this.currentTabIndex = this.lastTabIndex[this.type];
		}
		bool flag3 = this.currentTabIndex < 0;
		if (flag3)
		{
			this.currentTabIndex = 0;
		}
		bool flag4 = this.currentTabIndex > this.currentTabName.Length - 1;
		if (flag4)
		{
			this.currentTabIndex = this.currentTabName.Length - 1;
		}
		this.scroll = null;
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x000695A8 File Offset: 0x000677A8
	public void setTypeMapTrans()
	{
		this.type = 14;
		this.setType(0);
		this.setTabMapTrans();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x000056DF File Offset: 0x000038DF
	public void setTypeInfomatioin()
	{
		this.type = 6;
		this.cmx = this.wScroll;
		this.cmtoX = 0;
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x000695E0 File Offset: 0x000677E0
	public void setTypeMap()
	{
		bool flag = !GameScr.gI().isMapFize() && Panel.isPaintMap;
		if (flag)
		{
			bool flag2 = Hint.isOnTask(2, 0);
			if (flag2)
			{
				Hint.isViewMap = true;
				GameScr.info1.addInfo(mResources.go_to_quest, 0);
			}
			this.type = 4;
			this.currentTabName = this.tabName[this.type];
			this.startTabPos = this.xScroll + this.wScroll / 2 - this.currentTabName.Length * this.TAB_W / 2;
			this.cmx = (this.cmtoX = 0);
			this.setTabMap();
		}
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x00069684 File Offset: 0x00067884
	public void setTypeArchivement()
	{
		this.currentListLength = global::Char.myCharz().arrArchive.Length;
		this.setType(0);
		this.type = 9;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = 0);
		}
		this.selected = (GameCanvas.isTouch ? -1 : 0);
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x000056FC File Offset: 0x000038FC
	public void setTypeKiGuiOnly()
	{
		this.type = 17;
		this.setType(1);
		this.setTabKiGui();
		this.typeShop = 2;
		this.currentTabIndex = 0;
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x00069754 File Offset: 0x00067954
	public void setTabKiGui()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = global::Char.myCharz().arrItemShop[4].Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = (GameCanvas.isTouch ? -1 : 0);
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x00005724 File Offset: 0x00003924
	public void setTypeBodyOnly()
	{
		this.type = 7;
		this.setType(1);
		this.setTabInventory(true);
		this.currentTabIndex = 0;
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x00069824 File Offset: 0x00067A24
	public void addChatMessage(InfoItem info)
	{
		this.logChat.insertElementAt(info, 0);
		bool flag = this.logChat.size() > 20;
		if (flag)
		{
			this.logChat.removeElementAt(this.logChat.size() - 1);
		}
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x00005745 File Offset: 0x00003945
	public void addPlayerMenu(Command pm)
	{
		this.vPlayerMenu.addElement(pm);
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x00069870 File Offset: 0x00067A70
	public void setTabPlayerMenu()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = this.vPlayerMenu.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = (GameCanvas.isTouch ? -1 : 0);
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x00005755 File Offset: 0x00003955
	public void setTypeFlag()
	{
		this.type = 18;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? -1 : 0);
		this.setTabFlag();
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x0006993C File Offset: 0x00067B3C
	public void setTabFlag()
	{
		this.currentListLength = this.vFlag.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		bool flag4 = this.selected > this.currentListLength - 1;
		if (flag4)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x00005788 File Offset: 0x00003988
	public void setTypePlayerMenu(global::Char c)
	{
		this.type = 10;
		this.setType(0);
		this.setTabPlayerMenu();
		this.charMenu = c;
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x000057A9 File Offset: 0x000039A9
	public void setTypeFriend()
	{
		this.type = 11;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? -1 : 0);
		this.setTabFriend();
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x000057DC File Offset: 0x000039DC
	public void setTypeEnemy()
	{
		this.type = 16;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? -1 : 0);
		this.setTabEnemy();
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x0000580F File Offset: 0x00003A0F
	public void setTypeTop(sbyte t)
	{
		this.type = 15;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? -1 : 0);
		this.setTabTop();
		this.isThachDau = (t != 0);
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x00069A28 File Offset: 0x00067C28
	public void setTabTop()
	{
		this.currentListLength = this.vTop.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		bool flag4 = this.selected > this.currentListLength - 1;
		if (flag4)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x00069B14 File Offset: 0x00067D14
	public void setTabFriend()
	{
		this.currentListLength = this.vFriend.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		bool flag4 = this.selected > this.currentListLength - 1;
		if (flag4)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x00069C00 File Offset: 0x00067E00
	public void setTabEnemy()
	{
		this.currentListLength = this.vEnemy.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		bool flag4 = this.selected > this.currentListLength - 1;
		if (flag4)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0000584F File Offset: 0x00003A4F
	public void setTypeMessage()
	{
		this.type = 8;
		this.setType(0);
		this.setTabMessage();
		this.currentTabIndex = 0;
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x0000584F File Offset: 0x00003A4F
	public void setTypeLockInventory()
	{
		this.type = 8;
		this.setType(0);
		this.setTabMessage();
		this.currentTabIndex = 0;
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x0000586F File Offset: 0x00003A6F
	public void setTypeShop(int typeShop)
	{
		this.type = 1;
		this.setType(0);
		this.setTabShop();
		this.currentTabIndex = 0;
		this.typeShop = typeShop;
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x00069CEC File Offset: 0x00067EEC
	public void setTypeBox()
	{
		this.type = 2;
		bool flag = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
		if (flag)
		{
			Panel.boxTabName = new string[][]
			{
				mResources.chestt
			};
		}
		else
		{
			Panel.boxTabName = new string[][]
			{
				mResources.chestt,
				mResources.inventory
			};
		}
		this.tabName[2] = Panel.boxTabName;
		this.setType(0);
		bool flag2 = this.currentTabIndex == 0;
		if (flag2)
		{
			this.setTabBox();
		}
		bool flag3 = this.currentTabIndex == 1;
		if (flag3)
		{
			this.setTabInventory(true);
		}
		bool flag4 = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
		if (flag4)
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
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x00069DE0 File Offset: 0x00067FE0
	public void setTypeCombine()
	{
		this.type = 12;
		bool flag = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
		if (flag)
		{
			Panel.boxCombine = new string[][]
			{
				mResources.combine
			};
		}
		else
		{
			Panel.boxCombine = new string[][]
			{
				mResources.combine,
				mResources.inventory
			};
		}
		this.tabName[this.type] = Panel.boxCombine;
		this.setType(0);
		bool flag2 = this.currentTabIndex == 0;
		if (flag2)
		{
			this.setTabCombine();
		}
		bool flag3 = this.currentTabIndex == 1;
		if (flag3)
		{
			this.setTabInventory(true);
		}
		bool flag4 = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
		if (flag4)
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
		this.combineSuccess = -1;
		this.isDoneCombine = true;
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x00069EE8 File Offset: 0x000680E8
	public void setTabCombine()
	{
		this.currentListLength = this.vItemCombine.size() + 1;
		this.ITEM_HEIGHT = 24;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 9;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = (GameCanvas.isTouch ? -1 : 0);
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x00069FB8 File Offset: 0x000681B8
	public void setTypeAuto()
	{
		this.type = 22;
		this.setType(0);
		this.setTabAuto();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x00069FF0 File Offset: 0x000681F0
	private void setTabAuto()
	{
		this.currentListLength = Panel.strAuto.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? -1 : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x0006A0B8 File Offset: 0x000682B8
	public void setTypePetMain()
	{
		this.type = 21;
		bool flag = GameCanvas.panel2 != null;
		if (flag)
		{
			Panel.boxPet = mResources.petMainTab2;
		}
		else
		{
			Panel.boxPet = mResources.petMainTab;
		}
		this.tabName[21] = Panel.boxPet;
		bool flag2 = global::Char.myCharz().cgender == 1;
		if (flag2)
		{
			this.strStatus = new string[]
			{
				mResources.follow,
				mResources.defend,
				mResources.attack,
				mResources.gohome,
				mResources.fusion,
				mResources.fusionForever
			};
		}
		else
		{
			this.strStatus = new string[]
			{
				mResources.follow,
				mResources.defend,
				mResources.attack,
				mResources.gohome,
				mResources.fusion
			};
		}
		this.setType(2);
		bool flag3 = this.currentTabIndex == 0;
		if (flag3)
		{
			this.setTabPetInventory();
		}
		bool flag4 = this.currentTabIndex == 1;
		if (flag4)
		{
			this.setTabPetStatus();
		}
		bool flag5 = this.currentTabIndex == 2;
		if (flag5)
		{
			this.setTabInventory(true);
		}
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0006A1D8 File Offset: 0x000683D8
	public void setTypeMain()
	{
		this.type = 0;
		this.setType(0);
		bool flag = this.currentTabIndex == 1;
		if (flag)
		{
			this.setTabInventory(true);
		}
		bool flag2 = this.currentTabIndex == 2;
		if (flag2)
		{
			this.setTabSkill();
		}
		bool flag3 = this.currentTabIndex == 3;
		if (flag3)
		{
			bool flag4 = this.mainTabName.Length == 4;
			if (flag4)
			{
				this.setTabTool();
			}
			else
			{
				this.setTabClans();
			}
		}
		bool flag5 = this.currentTabIndex == 4;
		if (flag5)
		{
			this.setTabTool();
		}
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0006A26C File Offset: 0x0006846C
	public void setTypeZone()
	{
		this.type = 3;
		this.setType(0);
		this.setTabZone();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0006A2A0 File Offset: 0x000684A0
	public void addItemDetail(Item item)
	{
		this.cp = new ChatPopup();
		string text = string.Empty;
		string text2 = string.Empty;
		bool flag = (int)item.template.gender != global::Char.myCharz().cgender;
		if (flag)
		{
			bool flag2 = item.template.gender == 0;
			if (flag2)
			{
				text2 = text2 + "\n|7|1|" + mResources.from_earth;
			}
			else
			{
				bool flag3 = item.template.gender == 1;
				if (flag3)
				{
					text2 = text2 + "\n|7|1|" + mResources.from_namec;
				}
				else
				{
					bool flag4 = item.template.gender == 2;
					if (flag4)
					{
						text2 = text2 + "\n|7|1|" + mResources.from_sayda;
					}
				}
			}
		}
		string text3 = string.Empty;
		bool flag5 = item.itemOption != null;
		if (flag5)
		{
			for (int i = 0; i < item.itemOption.Length; i++)
			{
				bool flag6 = item.itemOption[i].optionTemplate.id == 72;
				if (flag6)
				{
					text3 = " [+" + item.itemOption[i].param + "]";
				}
			}
		}
		bool flag7 = false;
		bool flag8 = item.itemOption != null;
		if (flag8)
		{
			for (int j = 0; j < item.itemOption.Length; j++)
			{
				bool flag9 = item.itemOption[j].optionTemplate.id == 41;
				if (flag9)
				{
					flag7 = true;
					bool flag10 = item.itemOption[j].param == 1;
					if (flag10)
					{
						text2 = string.Concat(new object[]
						{
							text2,
							"|0|1|[",
							item.template.id,
							"] ",
							item.template.name,
							text3
						});
					}
					bool flag11 = item.itemOption[j].param == 2;
					if (flag11)
					{
						text2 = string.Concat(new object[]
						{
							text2,
							"|2|1|[",
							item.template.id,
							"] ",
							item.template.name,
							text3
						});
					}
					bool flag12 = item.itemOption[j].param == 3;
					if (flag12)
					{
						text2 = string.Concat(new object[]
						{
							text2,
							"|8|1|[",
							item.template.id,
							"] ",
							item.template.name,
							text3
						});
					}
					bool flag13 = item.itemOption[j].param == 4;
					if (flag13)
					{
						text2 = string.Concat(new object[]
						{
							text2,
							"|7|1|[",
							item.template.id,
							"] ",
							item.template.name,
							text3
						});
					}
				}
			}
		}
		bool flag14 = !flag7;
		if (flag14)
		{
			text2 = string.Concat(new object[]
			{
				text2,
				"|0|1|[",
				item.template.id,
				"] ",
				item.template.name,
				text3
			});
		}
		bool flag15 = item.itemOption != null;
		if (flag15)
		{
			for (int k = 0; k < item.itemOption.Length; k++)
			{
				bool flag16 = item.itemOption[k].optionTemplate.name.StartsWith("$");
				if (flag16)
				{
					text = item.itemOption[k].getOptiongColor();
					bool flag17 = item.itemOption[k].param == 1;
					if (flag17)
					{
						text2 = text2 + "\n|1|1|" + text;
					}
					bool flag18 = item.itemOption[k].param == 0;
					if (flag18)
					{
						text2 = text2 + "\n|0|1|" + text;
					}
				}
				else
				{
					text = item.itemOption[k].getOptionString();
					bool flag19 = !text.Equals(string.Empty) && item.itemOption[k].optionTemplate.id != 72;
					if (flag19)
					{
						bool flag20 = item.itemOption[k].optionTemplate.id == 102;
						if (flag20)
						{
							this.cp.starSlot = (sbyte)item.itemOption[k].param;
							Res.outz("STAR SLOT= " + this.cp.starSlot);
						}
						else
						{
							bool flag21 = item.itemOption[k].optionTemplate.id == 107;
							if (flag21)
							{
								this.cp.maxStarSlot = (sbyte)item.itemOption[k].param;
								Res.outz("STAR SLOT= " + this.cp.maxStarSlot);
							}
							else
							{
								text2 = text2 + "\n|1|1|" + text;
							}
						}
					}
				}
			}
		}
		bool flag22 = this.currItem.template.strRequire > 1;
		if (flag22)
		{
			string str = mResources.pow_request + ": " + this.currItem.template.strRequire;
			bool flag23 = (long)this.currItem.template.strRequire > global::Char.myCharz().cPower;
			if (flag23)
			{
				text2 = text2 + "\n|3|1|" + str;
				string text4 = text2;
				text2 = string.Concat(new object[]
				{
					text4,
					"\n|3|1|",
					mResources.your_pow,
					": ",
					global::Char.myCharz().cPower
				});
			}
			else
			{
				text2 = text2 + "\n|6|1|" + str;
			}
		}
		else
		{
			text2 += "\n|6|1|";
		}
		this.currItem.compare = this.getCompare(this.currItem);
		text2 += "\n--";
		text2 = text2 + "\n|6|" + item.template.description;
		bool flag24 = !item.reason.Equals(string.Empty);
		if (flag24)
		{
			bool flag25 = !item.template.description.Equals(string.Empty);
			if (flag25)
			{
				text2 += "\n--";
			}
			text2 = text2 + "\n|2|" + item.reason;
		}
		bool flag26 = this.cp.maxStarSlot > 0;
		if (flag26)
		{
			text2 += "\n\n";
		}
		this.popUpDetailInit(this.cp, text2);
		this.idIcon = (int)item.template.iconID;
		this.partID = null;
		this.charInfo = null;
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0006A988 File Offset: 0x00068B88
	public void popUpDetailInit(ChatPopup cp, string chat)
	{
		cp.isClip = false;
		cp.sayWidth = 180;
		cp.cx = 3 + this.X - ((this.X != 0) ? (Res.abs(cp.sayWidth - this.W) + 8) : 0);
		cp.says = mFont.tahoma_7_red.splitFontArray(chat, cp.sayWidth - 10);
		cp.delay = 10000000;
		cp.c = null;
		cp.sayRun = 7;
		cp.ch = 15 - cp.sayRun + cp.says.Length * 12 + 10;
		bool flag = cp.ch > GameCanvas.h - 80;
		if (flag)
		{
			cp.ch = GameCanvas.h - 80;
			cp.lim = cp.says.Length * 12 - cp.ch + 17;
			bool flag2 = cp.lim < 0;
			if (flag2)
			{
				cp.lim = 0;
			}
			ChatPopup.cmyText = 0;
			cp.isClip = true;
		}
		cp.cy = GameCanvas.menu.menuY - cp.ch;
		while (cp.cy < 10)
		{
			cp.cy++;
			GameCanvas.menu.menuY++;
		}
		cp.mH = 0;
		cp.strY = 10;
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0006AAE4 File Offset: 0x00068CE4
	public void popUpDetailInitArray(ChatPopup cp, string[] chat)
	{
		cp.sayWidth = 160;
		cp.cx = 3 + this.X;
		cp.says = chat;
		cp.delay = 10000000;
		cp.c = null;
		cp.sayRun = 7;
		cp.ch = 15 - cp.sayRun + cp.says.Length * 12 + 10;
		cp.cy = GameCanvas.menu.menuY - cp.ch;
		cp.mH = 0;
		cp.strY = 10;
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x0006AB70 File Offset: 0x00068D70
	public void addMessageDetail(ClanMessage cm)
	{
		this.cp = new ChatPopup();
		string text = "|0|" + cm.playerName;
		text = text + "\n|1|" + Member.getRole((int)cm.role);
		for (int i = 0; i < this.myMember.size(); i++)
		{
			Member member = (Member)this.myMember.elementAt(i);
			bool flag = cm.playerId == member.ID;
			if (flag)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"\n|5|",
					mResources.clan_capsuledonate,
					": ",
					member.clanPoint
				});
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"\n|5|",
					mResources.clan_capsuleself,
					": ",
					member.curClanPoint
				});
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"\n|4|",
					mResources.give_pea,
					": ",
					member.donate,
					mResources.time
				});
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"\n|4|",
					mResources.receive_pea,
					": ",
					member.receive_donate,
					mResources.time
				});
				this.partID = new int[]
				{
					(int)member.head,
					(int)member.leg,
					(int)member.body
				};
				break;
			}
		}
		text += "\n--";
		for (int j = 0; j < cm.chat.Length; j++)
		{
			text = text + "\n" + cm.chat[j];
		}
		bool flag2 = cm.type == 1;
		if (flag2)
		{
			string text3 = text;
			text = string.Concat(new object[]
			{
				text3,
				"\n|6|",
				mResources.received,
				" ",
				cm.recieve,
				"/",
				cm.maxCap
			});
		}
		this.popUpDetailInit(this.cp, text);
		this.charInfo = null;
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x0006ADD4 File Offset: 0x00068FD4
	public void addThachDauDetail(TopInfo t)
	{
		string text = "|0|1|" + t.name;
		text = text + "\n|1|Top " + t.rank;
		text = text + "\n|1|" + t.info;
		text = text + "\n|2|" + t.info2;
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.partID = new int[]
		{
			t.headID,
			(int)t.leg,
			(int)t.body
		};
		this.currItem = null;
		this.charInfo = null;
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0006AE7C File Offset: 0x0006907C
	public void addClanMemberDetail(Member m)
	{
		string text = "|0|1|" + m.name;
		string str = "\n|2|1|";
		bool flag = m.role == 0;
		if (flag)
		{
			str = "\n|7|1|";
		}
		bool flag2 = m.role == 1;
		if (flag2)
		{
			str = "\n|1|1|";
		}
		bool flag3 = m.role == 2;
		if (flag3)
		{
			str = "\n|0|1|";
		}
		text = text + str + Member.getRole((int)m.role);
		string text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"\n|2|1|",
			mResources.power,
			": ",
			m.powerPoint
		});
		text += "\n--";
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n|5|",
			mResources.clan_capsuledonate,
			": ",
			m.clanPoint
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n|5|",
			mResources.clan_capsuleself,
			": ",
			m.curClanPoint
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n|4|",
			mResources.give_pea,
			": ",
			m.donate,
			mResources.time
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n|4|",
			mResources.receive_pea,
			": ",
			m.receive_donate,
			mResources.time
		});
		text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"\n|6|",
			mResources.join_date,
			": ",
			m.joinTime
		});
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.partID = new int[]
		{
			(int)m.head,
			(int)m.leg,
			(int)m.body
		};
		this.currItem = null;
		this.charInfo = null;
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x0006B0A8 File Offset: 0x000692A8
	public void addClanDetail(Clan cl)
	{
		string text = "|0|" + cl.name;
		string[] array = mFont.tahoma_7_green.splitFontArray(cl.slogan, this.wScroll - 60);
		for (int i = 0; i < array.Length; i++)
		{
			text = text + "\n|2|" + array[i];
		}
		text += "\n--";
		string text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"\n|7|",
			mResources.clan_leader,
			": ",
			cl.leaderName
		});
		text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"\n|1|",
			mResources.power_point,
			": ",
			cl.powerPoint
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n|4|",
			mResources.member,
			": ",
			cl.currMember,
			"/",
			cl.maxMember
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n|4|",
			mResources.level,
			": ",
			cl.level
		});
		text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"\n|4|",
			mResources.clan_birthday,
			": ",
			NinjaUtil.getDate(cl.date)
		});
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.idIcon = (int)ClanImage.getClanImage((sbyte)cl.imgID).idImage[0];
		this.currItem = null;
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x0006B270 File Offset: 0x00069470
	public void addSkillDetail(SkillTemplate tp, Skill skill, Skill nextSkill)
	{
		string text = "|0|" + tp.name;
		for (int i = 0; i < tp.description.Length; i++)
		{
			text = text + "\n|4|" + tp.description[i];
		}
		text += "\n--";
		bool flag = skill != null;
		if (flag)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\n|2|",
				mResources.cap_do,
				": ",
				skill.point
			});
			text = text + "\n|5|" + NinjaUtil.replace(tp.damInfo, "#", skill.damage + string.Empty);
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\n|5|",
				mResources.KI_consume,
				skill.manaUse,
				(tp.manaUseType != 1) ? string.Empty : "%"
			});
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\n|5|",
				mResources.speed,
				": ",
				skill.coolDown,
				mResources.milisecond
			});
			text += "\n--";
			bool flag2 = skill.point == tp.maxPoint;
			if (flag2)
			{
				text = text + "\n|0|" + mResources.max_level_reach;
			}
			else
			{
				text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"\n|1|",
					mResources.next_level_require,
					Res.formatNumber(nextSkill.powRequire),
					" ",
					mResources.potential
				});
				text = text + "\n|4|" + NinjaUtil.replace(tp.damInfo, "#", nextSkill.damage + string.Empty);
			}
		}
		else
		{
			text = text + "\n|2|" + mResources.not_learn;
			string text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				"\n|1|",
				mResources.learn_require,
				Res.formatNumber(nextSkill.powRequire),
				" ",
				mResources.potential
			});
			text = text + "\n|4|" + NinjaUtil.replace(tp.damInfo, "#", nextSkill.damage + string.Empty);
			text3 = text;
			text = string.Concat(new object[]
			{
				text3,
				"\n|4|",
				mResources.KI_consume,
				nextSkill.manaUse,
				(tp.manaUseType != 1) ? string.Empty : "%"
			});
			text3 = text;
			text = string.Concat(new object[]
			{
				text3,
				"\n|4|",
				mResources.speed,
				": ",
				nextSkill.coolDown,
				mResources.milisecond
			});
		}
		this.currItem = null;
		this.partID = null;
		this.charInfo = null;
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.idIcon = 0;
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x0006B5C4 File Offset: 0x000697C4
	public void show()
	{
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.cmdClose.x = 156;
			this.cmdClose.y = 3;
		}
		else
		{
			this.cmdClose.x = GameCanvas.w - 19;
			this.cmdClose.y = GameCanvas.h - 19;
		}
		this.cmdClose.isPlaySoundButton = false;
		ChatPopup.currChatPopup = null;
		InfoDlg.hide();
		this.timeShow = 20;
		this.isShow = true;
		this.isClose = false;
		SoundMn.gI().panelOpen();
		bool flag = this.isTypeShop();
		if (flag)
		{
			global::Char.myCharz().setPartOld();
		}
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x0006B674 File Offset: 0x00069874
	public void chatTFUpdateKey()
	{
		bool flag = this.chatTField != null && this.chatTField.isShow;
		if (flag)
		{
			bool flag2 = this.chatTField.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.chatTField.left)) && this.chatTField.left != null;
			if (flag2)
			{
				this.chatTField.left.performAction();
			}
			bool flag3 = this.chatTField.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.chatTField.right)) && this.chatTField.right != null;
			if (flag3)
			{
				this.chatTField.right.performAction();
			}
			bool flag4 = this.chatTField.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.chatTField.center)) && this.chatTField.center != null;
			if (flag4)
			{
				this.chatTField.center.performAction();
			}
			bool flag5 = this.chatTField.isShow && GameCanvas.keyAsciiPress != 0;
			if (flag5)
			{
				this.chatTField.keyPressed(GameCanvas.keyAsciiPress);
				GameCanvas.keyAsciiPress = 0;
			}
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
		}
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x0006B7E4 File Offset: 0x000699E4
	public void updateKey()
	{
		bool flag = (this.chatTField != null && this.chatTField.isShow) || !GameCanvas.panel.isDoneCombine || InfoDlg.isShow;
		if (!flag)
		{
			bool flag2 = this.tabIcon != null && this.tabIcon.isShow;
			if (flag2)
			{
				this.tabIcon.updateKey();
			}
			else
			{
				bool flag3 = this.isClose || !this.isShow;
				if (!flag3)
				{
					bool flag4 = this.cmdClose.isPointerPressInside();
					if (flag4)
					{
						this.cmdClose.performAction();
					}
					else
					{
						bool flag5 = GameCanvas.keyPressed[13];
						if (flag5)
						{
							bool flag6 = this.type != 4;
							if (flag6)
							{
								this.hide();
								return;
							}
							this.setTypeMain();
							this.cmx = (this.cmtoX = 0);
						}
						bool flag7 = GameCanvas.keyPressed[12] || GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25];
						if (flag7)
						{
							bool flag8 = this.left.idAction > 0;
							if (flag8)
							{
								this.perform(this.left.idAction, this.left.p);
							}
							else
							{
								this.waitToPerform = 2;
							}
						}
						bool flag9 = this.Equals(GameCanvas.panel) && GameCanvas.panel2 == null && GameCanvas.isPointerJustRelease && !GameCanvas.isPointer(this.X, this.Y, this.W, this.H) && !this.pointerIsDowning;
						if (flag9)
						{
							this.hide();
						}
						else
						{
							bool flag10 = !this.isClanOption;
							if (flag10)
							{
								this.updateKeyInTabBar();
							}
							switch (this.type)
							{
							case 0:
							{
								bool flag11 = this.currentTabIndex == 0;
								if (flag11)
								{
									this.updateKeyQuest();
									GameCanvas.clearKeyPressed();
									return;
								}
								bool flag12 = this.currentTabIndex == 1;
								if (flag12)
								{
									this.updateKeyInventory();
								}
								bool flag13 = this.currentTabIndex == 2;
								if (flag13)
								{
									this.updateKeySkill();
								}
								bool flag14 = this.currentTabIndex == 3;
								if (flag14)
								{
									bool flag15 = this.mainTabName.Length == 4;
									if (flag15)
									{
										this.updateKeyTool();
									}
									else
									{
										this.updateKeyClans();
									}
								}
								bool flag16 = this.currentTabIndex == 4;
								if (flag16)
								{
									this.updateKeyTool();
								}
								break;
							}
							case 1:
							case 17:
							case 25:
							{
								bool flag17 = this.currentTabIndex < this.currentTabName.Length - ((GameCanvas.panel2 == null) ? 1 : 0) && this.type != 17;
								if (flag17)
								{
									this.updateKeyScrollView();
								}
								else
								{
									bool flag18 = this.typeShop == 0;
									if (flag18)
									{
										this.updateKeyInventory();
									}
									else
									{
										this.updateKeyScrollView();
									}
								}
								break;
							}
							case 2:
								this.updateKeyInventory();
								break;
							case 3:
								this.updateKeyScrollView();
								break;
							case 4:
								this.updateKeyMap();
								GameCanvas.clearKeyPressed();
								return;
							case 7:
								this.updateKeyInventory();
								break;
							case 8:
								this.updateKeyScrollView();
								break;
							case 9:
								this.updateKeyScrollView();
								break;
							case 10:
								this.updateKeyScrollView();
								break;
							case 11:
							case 16:
								this.updateKeyScrollView();
								break;
							case 12:
								this.updateKeyCombine();
								break;
							case 13:
								this.updateKeyGiaoDich();
								break;
							case 14:
								this.updateKeyScrollView();
								break;
							case 15:
								this.updateKeyScrollView();
								break;
							case 18:
								this.updateKeyScrollView();
								break;
							case 19:
								this.updateKeyOption();
								break;
							case 20:
								this.updateKeyOption();
								break;
							case 21:
							{
								bool flag19 = this.currentTabIndex == 0;
								if (flag19)
								{
									this.updateKeyScrollView();
								}
								bool flag20 = this.currentTabIndex == 1;
								if (flag20)
								{
									this.updateKeyPetStatus();
								}
								bool flag21 = this.currentTabIndex == 2;
								if (flag21)
								{
									this.updateKeyScrollView();
								}
								break;
							}
							case 22:
								this.updateKeyAuto();
								break;
							case 23:
							case 24:
								this.updateKeyScrollView();
								break;
							}
							GameCanvas.clearKeyHold();
							for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
							{
								GameCanvas.keyPressed[i] = false;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x00003A0D File Offset: 0x00001C0D
	private void updateKeyAuto()
	{
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x00005896 File Offset: 0x00003A96
	private void updateKeyPetStatus()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x00003A0D File Offset: 0x00001C0D
	private void updateKeyPetSkill()
	{
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x00005896 File Offset: 0x00003A96
	private void keyGiaodich()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x0006BC5C File Offset: 0x00069E5C
	private void updateKeyGiaoDich()
	{
		bool flag = this.currentTabIndex == 0;
		if (flag)
		{
			bool flag2 = this.Equals(GameCanvas.panel);
			if (flag2)
			{
				this.updateKeyInventory();
			}
			bool flag3 = this.Equals(GameCanvas.panel2);
			if (flag3)
			{
				this.keyGiaodich();
			}
		}
		bool flag4 = this.currentTabIndex == 1 || this.currentTabIndex == 2;
		if (flag4)
		{
			this.keyGiaodich();
		}
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x00005896 File Offset: 0x00003A96
	private void updateKeyTool()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x00005896 File Offset: 0x00003A96
	private void updateKeySkill()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x00005896 File Offset: 0x00003A96
	private void updateKeyClanIcon()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x0006BCCC File Offset: 0x00069ECC
	public void setTabGiaoDich(bool isMe)
	{
		this.currentListLength = ((!isMe) ? (this.vFriendGD.size() + 3) : (this.vMyGD.size() + 3));
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? -1 : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x0006BDAC File Offset: 0x00069FAC
	public void setTypeGiaoDich(global::Char cGD)
	{
		this.type = 13;
		this.tabName[this.type] = Panel.boxGD;
		this.isAccept = false;
		this.isLock = false;
		this.isFriendLock = false;
		this.vMyGD.removeAllElements();
		this.vFriendGD.removeAllElements();
		this.moneyGD = 0;
		this.friendMoneyGD = 0;
		bool flag = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
		if (flag)
		{
			GameCanvas.panel2 = new Panel();
			GameCanvas.panel2.type = 13;
			GameCanvas.panel2.tabName[this.type] = new string[][]
			{
				mResources.item_receive
			};
			GameCanvas.panel2.setType(1);
			GameCanvas.panel2.setTabGiaoDich(false);
			GameCanvas.panel.tabName[this.type] = new string[][]
			{
				mResources.inventory,
				mResources.item_give
			};
			GameCanvas.panel2.show();
			GameCanvas.panel2.charMenu = cGD;
		}
		bool flag2 = this.Equals(GameCanvas.panel);
		if (flag2)
		{
			this.setType(0);
		}
		bool flag3 = this.currentTabIndex == 0;
		if (flag3)
		{
			this.setTabInventory(true);
		}
		bool flag4 = this.currentTabIndex == 1;
		if (flag4)
		{
			this.setTabGiaoDich(true);
		}
		bool flag5 = this.currentTabIndex == 2;
		if (flag5)
		{
			this.setTabGiaoDich(false);
		}
		this.charMenu = cGD;
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x0006BF18 File Offset: 0x0006A118
	private void paintGiaoDich(mGraphics g, bool isMe)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		MyVector myVector = (!isMe) ? this.vFriendGD : this.vMyGD;
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll + 36;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 36;
			int num4 = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll;
			int num6 = this.yScroll + i * this.ITEM_HEIGHT;
			int num7 = 34;
			int num8 = this.ITEM_HEIGHT - 1;
			bool flag = num2 - this.cmy > this.yScroll + this.hScroll || num2 - this.cmy < this.yScroll - this.ITEM_HEIGHT;
			if (!flag)
			{
				bool flag2 = i == this.currentListLength - 1;
				if (flag2)
				{
					bool flag3 = !isMe;
					if (!flag3)
					{
						g.setColor(15196114);
						g.fillRect(num5, num2, this.wScroll, num4);
						bool flag4 = !this.isLock;
						if (flag4)
						{
							bool flag5 = !this.isFriendLock;
							if (flag5)
							{
								mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.not_lock_trade, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
							}
							else
							{
								mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.locked_trade, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
							}
						}
						else
						{
							bool flag6 = this.isFriendLock;
							if (flag6)
							{
								g.setColor(15196114);
								g.fillRect(num5, num2, this.wScroll, num4);
								g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
								((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.done, this.xScroll + this.wScroll - 22, num2 + 7, 2);
								mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.locked_trade, this.xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
							}
							else
							{
								mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.not_lock_trade, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
							}
						}
					}
				}
				else
				{
					bool flag7 = i == this.currentListLength - 2;
					if (flag7)
					{
						if (isMe)
						{
							g.setColor(15196114);
							g.fillRect(num5, num2, this.wScroll, num4);
							bool flag8 = !this.isAccept;
							if (flag8)
							{
								bool flag9 = !this.isLock;
								if (flag9)
								{
									g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
									((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.mlock, this.xScroll + this.wScroll - 22, num2 + 7, 2);
									mFont.tahoma_7_grey.drawString(g, mResources.you + mResources.not_lock_trade, this.xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
								}
								else
								{
									g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
									((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.CANCEL, this.xScroll + this.wScroll - 22, num2 + 7, 2);
									mFont.tahoma_7_grey.drawString(g, mResources.you + mResources.locked_trade, this.xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
								}
							}
						}
						else
						{
							bool flag10 = !this.isFriendLock;
							if (flag10)
							{
								mFont.tahoma_7b_dark.drawString(g, mResources.not_lock_trade_upper, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
							}
							else
							{
								mFont.tahoma_7b_dark.drawString(g, mResources.locked_trade_upper, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
							}
						}
					}
					else
					{
						bool flag11 = i == this.currentListLength - 3;
						if (flag11)
						{
							bool flag12 = this.isLock;
							if (flag12)
							{
								g.setColor(13748667);
							}
							else
							{
								g.setColor((i != this.selected) ? 15196114 : 16383818);
							}
							g.fillRect(num, num2, num3, num4);
							bool flag13 = this.isLock;
							if (flag13)
							{
								g.setColor(13748667);
							}
							else
							{
								g.setColor((i != this.selected) ? 9993045 : 7300181);
							}
							g.fillRect(num5, num6, num7, num8);
							g.drawImage(Panel.imgXu, num5 + num7 / 2, num6 + num8 / 2, 3);
							mFont.tahoma_7_green2.drawString(g, NinjaUtil.getMoneys((long)((!isMe) ? this.friendMoneyGD : this.moneyGD)) + " " + mResources.XU, num + 5, num2 + 11, 0);
							mFont.tahoma_7_green.drawString(g, mResources.money_trade, num + 5, num2, 0);
						}
						else
						{
							bool flag14 = myVector.size() == 0;
							if (flag14)
							{
								return;
							}
							bool flag15 = this.isLock;
							if (flag15)
							{
								g.setColor(13748667);
							}
							else
							{
								g.setColor((i != this.selected) ? 15196114 : 16383818);
							}
							g.fillRect(num, num2, num3, num4);
							bool flag16 = this.isLock;
							if (flag16)
							{
								g.setColor(13748667);
							}
							else
							{
								g.setColor((i != this.selected) ? 9993045 : 9541120);
							}
							Item item = (Item)myVector.elementAt(i);
							bool flag17 = item != null;
							if (flag17)
							{
								for (int j = 0; j < item.itemOption.Length; j++)
								{
									bool flag18 = item.itemOption[j].optionTemplate.id != 72 || item.itemOption[j].param <= 0;
									if (!flag18)
									{
										sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[j].param);
										int color_ItemBg = Panel.GetColor_ItemBg((int)color_Item_Upgrade);
										bool flag19 = color_ItemBg != -1;
										if (flag19)
										{
											bool flag20 = this.isLock;
											if (flag20)
											{
												g.setColor(13748667);
											}
											else
											{
												g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
											}
										}
									}
								}
							}
							g.fillRect(num5, num6, num7, num8);
							bool flag21 = item == null;
							if (!flag21)
							{
								string text = string.Empty;
								mFont mFont = mFont.tahoma_7_green2;
								bool flag22 = item.itemOption != null;
								if (flag22)
								{
									for (int k = 0; k < item.itemOption.Length; k++)
									{
										bool flag23 = item.itemOption[k].optionTemplate.id == 72;
										if (flag23)
										{
											text = " [+" + item.itemOption[k].param + "]";
										}
										bool flag24 = item.itemOption[k].optionTemplate.id == 41;
										if (flag24)
										{
											bool flag25 = item.itemOption[k].param == 1;
											if (flag25)
											{
												mFont = Panel.GetFont(0);
											}
											else
											{
												bool flag26 = item.itemOption[k].param == 2;
												if (flag26)
												{
													mFont = Panel.GetFont(2);
												}
												else
												{
													bool flag27 = item.itemOption[k].param == 3;
													if (flag27)
													{
														mFont = Panel.GetFont(8);
													}
													else
													{
														bool flag28 = item.itemOption[k].param == 4;
														if (flag28)
														{
															mFont = Panel.GetFont(7);
														}
													}
												}
											}
										}
									}
								}
								mFont.drawString(g, string.Concat(new object[]
								{
									"[",
									item.template.id,
									"] ",
									item.template.name,
									text
								}), num + 5, num2 + 1, 0);
								string text2 = string.Empty;
								bool flag29 = item.itemOption != null;
								if (flag29)
								{
									bool flag30 = item.itemOption.Length != 0 && item.itemOption[0] != null;
									if (flag30)
									{
										text2 += item.itemOption[0].getOptionString();
									}
									mFont mFont2 = mFont.tahoma_7_blue;
									bool flag31 = item.compare < 0 && item.template.type != 5;
									if (flag31)
									{
										mFont2 = mFont.tahoma_7_red;
									}
									bool flag32 = item.itemOption.Length > 1;
									if (flag32)
									{
										for (int l = 1; l < item.itemOption.Length; l++)
										{
											bool flag33 = item.itemOption[l] != null && item.itemOption[l].optionTemplate.id != 102 && item.itemOption[l].optionTemplate.id != 107;
											if (flag33)
											{
												text2 = text2 + "," + item.itemOption[l].getOptionString();
											}
										}
									}
									mFont2.drawString(g, text2, num + 5, num2 + 11, mFont.LEFT);
								}
								SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
								bool flag34 = item.itemOption != null;
								if (flag34)
								{
									for (int m = 0; m < item.itemOption.Length; m++)
									{
										this.paintOptItem(g, item.itemOption[m].optionTemplate.id, item.itemOption[m].param, num5, num6, num7, num8);
									}
									for (int n = 0; n < item.itemOption.Length; n++)
									{
										this.paintOptSlotItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8);
									}
								}
								bool flag35 = item.quantity > 1;
								if (flag35)
								{
									mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
								}
							}
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0006CAD8 File Offset: 0x0006ACD8
	private void updateKeyMap()
	{
		bool flag = GameCanvas.keyHold[(!Main.isPC) ? 2 : 21];
		if (flag)
		{
			this.yMove -= 5;
			this.cmyMap = this.yMove - (this.yScroll + this.hScroll / 2);
			bool flag2 = this.yMove < this.yScroll;
			if (flag2)
			{
				this.yMove = this.yScroll;
			}
		}
		bool flag3 = GameCanvas.keyHold[(!Main.isPC) ? 8 : 22];
		if (flag3)
		{
			this.yMove += 5;
			this.cmyMap = this.yMove - (this.yScroll + this.hScroll / 2);
			bool flag4 = this.yMove > this.yScroll + 200;
			if (flag4)
			{
				this.yMove = this.yScroll + 200;
			}
		}
		bool flag5 = GameCanvas.keyHold[(!Main.isPC) ? 4 : 23];
		if (flag5)
		{
			this.xMove -= 5;
			this.cmxMap = this.xMove - this.wScroll / 2;
			bool flag6 = this.xMove < 16;
			if (flag6)
			{
				this.xMove = 16;
			}
		}
		bool flag7 = GameCanvas.keyHold[(!Main.isPC) ? 6 : 24];
		if (flag7)
		{
			this.xMove += 5;
			this.cmxMap = this.xMove - this.wScroll / 2;
			bool flag8 = this.xMove > 250;
			if (flag8)
			{
				this.xMove = 250;
			}
		}
		bool isPointerDown = GameCanvas.isPointerDown;
		if (isPointerDown)
		{
			this.pointerIsDowning = true;
			bool flag9 = !this.trans;
			if (flag9)
			{
				this.pa1 = this.cmxMap;
				this.pa2 = this.cmyMap;
				this.trans = true;
			}
			this.cmxMap = this.pa1 + (GameCanvas.pxLast - GameCanvas.px);
			this.cmyMap = this.pa2 + (GameCanvas.pyLast - GameCanvas.py);
		}
		bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
		if (isPointerJustRelease)
		{
			this.trans = false;
			GameCanvas.pxLast = GameCanvas.px;
			GameCanvas.pyLast = GameCanvas.py;
			this.pX = GameCanvas.pxLast + this.cmxMap;
			this.pY = GameCanvas.pyLast + this.cmyMap;
		}
		bool isPointerClick = GameCanvas.isPointerClick;
		if (isPointerClick)
		{
			this.pointerIsDowning = false;
		}
		bool flag10 = this.cmxMap < 0;
		if (flag10)
		{
			this.cmxMap = 0;
		}
		bool flag11 = this.cmxMap > this.cmxMapLim;
		if (flag11)
		{
			this.cmxMap = this.cmxMapLim;
		}
		bool flag12 = this.cmyMap < 0;
		if (flag12)
		{
			this.cmyMap = 0;
		}
		bool flag13 = this.cmyMap > this.cmyMapLim;
		if (flag13)
		{
			this.cmyMap = this.cmyMapLim;
		}
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x0006CDB4 File Offset: 0x0006AFB4
	private void updateKeyCombine()
	{
		bool flag = this.currentTabIndex == 0;
		if (flag)
		{
			this.updateKeyScrollView();
			this.keyTouchCombine = -1;
			bool flag2 = this.selected == this.vItemCombine.size() && GameCanvas.isPointerClick;
			if (flag2)
			{
				GameCanvas.isPointerClick = false;
				this.keyTouchCombine = 1;
			}
		}
		bool flag3 = this.currentTabIndex == 1;
		if (flag3)
		{
			this.updateKeyScrollView();
		}
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x0006CE24 File Offset: 0x0006B024
	private void updateKeyQuest()
	{
		bool flag = GameCanvas.keyHold[(!Main.isPC) ? 2 : 21];
		if (flag)
		{
			this.cmyQuest -= 5;
		}
		bool flag2 = GameCanvas.keyHold[(!Main.isPC) ? 8 : 22];
		if (flag2)
		{
			this.cmyQuest += 5;
		}
		bool flag3 = this.cmyQuest < 0;
		if (flag3)
		{
			this.cmyQuest = 0;
		}
		int num = this.indexRowMax * 12 - (this.hScroll - 60);
		bool flag4 = num < 0;
		if (flag4)
		{
			num = 0;
		}
		bool flag5 = this.cmyQuest > num;
		if (flag5)
		{
			this.cmyQuest = num;
		}
		bool flag6 = this.scroll != null;
		if (flag6)
		{
			bool flag7 = !GameCanvas.isTouch;
			if (flag7)
			{
				this.scroll.cmy = this.cmyQuest;
			}
			this.scroll.updateKey();
		}
		int num2 = this.xScroll + this.wScroll / 2 - 35;
		int num3 = (GameCanvas.h <= 300) ? 15 : 20;
		int num4 = this.yScroll + this.hScroll - num3 - 15;
		int px = GameCanvas.px;
		int py = GameCanvas.py;
		this.keyTouchMapButton = -1;
		bool flag8 = Panel.isPaintMap && !GameScr.gI().isMapDocNhan() && px >= num2 && px <= num2 + 70 && py >= num4 && py <= num4 + 30 && (this.scroll == null || !this.scroll.pointerIsDowning);
		if (flag8)
		{
			this.keyTouchMapButton = 1;
			bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
			if (isPointerJustRelease)
			{
				SoundMn.gI().buttonClick();
				this.waitToPerform = 2;
				GameCanvas.clearAllPointerEvent();
			}
		}
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x0006CFE0 File Offset: 0x0006B1E0
	private void getCurrClanOtion()
	{
		this.isClanOption = false;
		bool flag = this.type != 0 || this.mainTabName.Length != 5 || this.currentTabIndex != 3;
		if (!flag)
		{
			this.isClanOption = false;
			bool flag2 = this.selected == 0;
			if (flag2)
			{
				this.currClanOption = new int[this.clansOption.Length];
				for (int i = 0; i < this.currClanOption.Length; i++)
				{
					this.currClanOption[i] = i;
				}
				bool flag3 = !this.isViewMember;
				if (flag3)
				{
					this.isClanOption = true;
				}
			}
			else
			{
				bool flag4 = this.selected != 1 && !this.isSearchClan && this.selected > 0;
				if (flag4)
				{
					this.currClanOption = new int[1];
					for (int j = 0; j < this.currClanOption.Length; j++)
					{
						this.currClanOption[j] = j;
					}
					this.isClanOption = true;
				}
			}
		}
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x0006D0E8 File Offset: 0x0006B2E8
	private void updateKeyClansOption()
	{
		bool flag = this.currClanOption == null;
		if (!flag)
		{
			bool flag2 = GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23];
			if (flag2)
			{
				this.currMess = this.getCurrMessage();
				this.cSelected--;
				bool flag3 = this.selected == 0 && this.cSelected < 0;
				if (flag3)
				{
					this.cSelected = this.currClanOption.Length - 1;
				}
				bool flag4 = this.selected > 1 && this.isMessage && this.currMess.option != null && this.cSelected < 0;
				if (flag4)
				{
					this.cSelected = this.currMess.option.Length - 1;
				}
			}
			else
			{
				bool flag5 = GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24];
				if (flag5)
				{
					this.currMess = this.getCurrMessage();
					this.cSelected++;
					bool flag6 = this.selected == 0 && this.cSelected > this.currClanOption.Length - 1;
					if (flag6)
					{
						this.cSelected = 0;
					}
					bool flag7 = this.selected > 1 && this.isMessage && this.currMess.option != null && this.cSelected > this.currMess.option.Length - 1;
					if (flag7)
					{
						this.cSelected = 0;
					}
				}
			}
		}
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x000058A0 File Offset: 0x00003AA0
	private void updateKeyClans()
	{
		this.updateKeyScrollView();
		this.updateKeyClansOption();
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x0006D25C File Offset: 0x0006B45C
	private void checkOptionSelect()
	{
		bool flag = this.type != 0 || this.currentTabIndex != 3 || this.mainTabName.Length != 5 || this.selected == -1;
		if (!flag)
		{
			int num = 0;
			bool flag2 = this.selected == 0;
			if (flag2)
			{
				num = this.xScroll + this.wScroll / 2 - this.clansOption.Length * this.TAB_W / 2;
				this.cSelected = (GameCanvas.px - num) / this.TAB_W;
			}
			else
			{
				this.currMess = this.getCurrMessage();
				bool flag3 = this.currMess != null && this.currMess.option != null;
				if (flag3)
				{
					num = this.xScroll + this.wScroll - 2 - this.currMess.option.Length * 40;
					this.cSelected = (GameCanvas.px - num) / 40;
				}
			}
			bool flag4 = GameCanvas.px < num;
			if (flag4)
			{
				this.cSelected = -1;
			}
		}
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x0006D35C File Offset: 0x0006B55C
	public void updateScroolMouse(int a)
	{
		bool flag = false;
		bool flag2 = GameCanvas.pxMouse > this.wScroll;
		if (!flag2)
		{
			bool flag3 = this.indexMouse == -1;
			if (flag3)
			{
				this.indexMouse = this.selected;
			}
			bool flag4 = a > 0;
			if (flag4)
			{
				this.indexMouse -= a;
				flag = true;
			}
			else
			{
				bool flag5 = a < 0;
				if (flag5)
				{
					this.indexMouse += -a;
					flag = true;
				}
			}
			bool flag6 = this.indexMouse < 0;
			if (flag6)
			{
				this.indexMouse = 0;
			}
			bool flag7 = flag;
			if (flag7)
			{
				this.cmtoY = this.indexMouse * 12;
				bool flag8 = this.cmtoY > this.cmyLim;
				if (flag8)
				{
					this.cmtoY = this.cmyLim;
				}
				bool flag9 = this.cmtoY < 0;
				if (flag9)
				{
					this.cmtoY = 0;
				}
			}
		}
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x0006D444 File Offset: 0x0006B644
	private void updateKeyScrollView()
	{
		bool flag = this.currentListLength <= 0;
		if (!flag)
		{
			bool flag2 = false;
			bool flag3 = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21];
			if (flag3)
			{
				flag2 = true;
				this.selected--;
				bool flag4 = this.type == 24;
				if (flag4)
				{
					this.selected -= 2;
					bool flag5 = this.selected < 0;
					if (flag5)
					{
						this.selected = 0;
					}
				}
				else
				{
					bool flag6 = this.selected < 0;
					if (flag6)
					{
						bool flag7 = this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.currentTabIndex <= 3 && this.maxPageShop[this.currentTabIndex] > 1;
						if (flag7)
						{
							InfoDlg.showWait();
							bool flag8 = this.currPageShop[this.currentTabIndex] <= 0;
							if (flag8)
							{
								Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.maxPageShop[this.currentTabIndex] - 1, -1);
							}
							else
							{
								Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] - 1, -1);
							}
							return;
						}
						this.selected = this.currentListLength - 1;
						bool flag9 = this.isClanOption;
						if (flag9)
						{
							this.selected = -1;
						}
						bool flag10 = this.size_tab > 0;
						if (flag10)
						{
							this.selected = -1;
						}
					}
				}
				this.lastSelect[this.currentTabIndex] = this.selected;
				this.cSelected = 0;
				this.getCurrClanOtion();
			}
			else
			{
				bool flag11 = GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22];
				if (flag11)
				{
					flag2 = true;
					this.selected++;
					bool flag12 = this.type == 24;
					if (flag12)
					{
						this.selected += 2;
						bool flag13 = this.selected > this.currentListLength - 1;
						if (flag13)
						{
							this.selected = this.currentListLength - 1;
						}
					}
					else
					{
						bool flag14 = this.selected > this.currentListLength - 1;
						if (flag14)
						{
							bool flag15 = this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.currentTabIndex <= 3 && this.maxPageShop[this.currentTabIndex] > 1;
							if (flag15)
							{
								InfoDlg.showWait();
								bool flag16 = this.currPageShop[this.currentTabIndex] >= this.maxPageShop[this.currentTabIndex] - 1;
								if (flag16)
								{
									Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, 0, -1);
								}
								else
								{
									Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] + 1, -1);
								}
								return;
							}
							this.selected = 0;
						}
					}
					this.lastSelect[this.currentTabIndex] = this.selected;
					this.cSelected = 0;
					this.getCurrClanOtion();
				}
			}
			bool flag17 = flag2;
			if (flag17)
			{
				this.cmtoY = this.selected * this.ITEM_HEIGHT - this.hScroll / 2;
				bool flag18 = this.cmtoY > this.cmyLim;
				if (flag18)
				{
					this.cmtoY = this.cmyLim;
				}
				bool flag19 = this.cmtoY < 0;
				if (flag19)
				{
					this.cmtoY = 0;
				}
				bool flag20 = this.selected == this.currentListLength || this.selected == 0;
				if (flag20)
				{
					this.cmy = this.cmtoY;
				}
			}
			bool isPointerDown = GameCanvas.isPointerDown;
			if (isPointerDown)
			{
				this.justRelease = false;
				bool flag21 = !this.pointerIsDowning && GameCanvas.isPointer(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
				if (flag21)
				{
					for (int i = 0; i < this.pointerDownLastX.Length; i++)
					{
						this.pointerDownLastX[0] = GameCanvas.py;
					}
					this.pointerDownFirstX = GameCanvas.py;
					this.pointerIsDowning = true;
					this.isDownWhenRunning = (this.cmRun != 0);
					this.cmRun = 0;
				}
				else
				{
					bool flag22 = this.pointerIsDowning;
					if (flag22)
					{
						this.pointerDownTime++;
						bool flag23 = this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.py && !this.isDownWhenRunning;
						if (flag23)
						{
							this.pointerDownFirstX = -1000;
							this.selected = (this.cmtoY + GameCanvas.py - this.yScroll) / this.ITEM_HEIGHT;
							bool flag24 = this.selected >= this.currentListLength;
							if (flag24)
							{
								this.selected = -1;
							}
							this.checkOptionSelect();
						}
						else
						{
							this.indexMouse = -1;
						}
						int num = GameCanvas.py - this.pointerDownLastX[0];
						bool flag25 = num != 0 && this.selected != -1;
						if (flag25)
						{
							this.selected = -1;
							this.cSelected = -1;
						}
						for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
						{
							this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
						}
						this.pointerDownLastX[0] = GameCanvas.py;
						this.cmtoY -= num;
						bool flag26 = this.cmtoY < 0;
						if (flag26)
						{
							this.cmtoY = 0;
						}
						bool flag27 = this.cmtoY > this.cmyLim;
						if (flag27)
						{
							this.cmtoY = this.cmyLim;
						}
						bool flag28 = this.cmy < 0 || this.cmy > this.cmyLim;
						if (flag28)
						{
							num /= 2;
						}
						this.cmy -= num;
						bool flag29 = this.cmy < -(GameCanvas.h / 3);
						if (flag29)
						{
							this.wantUpdateList = true;
						}
						else
						{
							this.wantUpdateList = false;
						}
					}
				}
			}
			bool flag30 = !GameCanvas.isPointerJustRelease || !this.pointerIsDowning;
			if (!flag30)
			{
				this.justRelease = true;
				int i2 = GameCanvas.py - this.pointerDownLastX[0];
				GameCanvas.isPointerJustRelease = false;
				bool flag31 = Res.abs(i2) < 20 && Res.abs(GameCanvas.py - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning;
				if (flag31)
				{
					this.cmRun = 0;
					this.cmtoY = this.cmy;
					this.pointerDownFirstX = -1000;
					this.selected = (this.cmtoY + GameCanvas.py - this.yScroll) / this.ITEM_HEIGHT;
					bool flag32 = this.selected >= this.currentListLength;
					if (flag32)
					{
						this.selected = -1;
					}
					this.checkOptionSelect();
					this.pointerDownTime = 0;
					this.waitToPerform = 10;
					SoundMn.gI().panelClick();
				}
				else
				{
					bool flag33 = this.selected != -1 && this.pointerDownTime > 5;
					if (flag33)
					{
						this.pointerDownTime = 0;
						this.waitToPerform = 1;
					}
					else
					{
						bool flag34 = this.selected == -1 && !this.isDownWhenRunning;
						if (flag34)
						{
							bool flag35 = this.cmy < 0;
							if (flag35)
							{
								this.cmtoY = 0;
							}
							else
							{
								bool flag36 = this.cmy > this.cmyLim;
								if (flag36)
								{
									this.cmtoY = this.cmyLim;
								}
								else
								{
									int num2 = GameCanvas.py - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
									num2 = ((num2 > 10) ? 10 : ((num2 < -10) ? -10 : 0));
									this.cmRun = -num2 * 100;
								}
							}
						}
					}
				}
				this.pointerIsDowning = false;
				this.pointerDownTime = 0;
				GameCanvas.isPointerJustRelease = false;
			}
		}
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x0006DC44 File Offset: 0x0006BE44
	public string subArray(string[] str)
	{
		return null;
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x0006DC58 File Offset: 0x0006BE58
	private void updateKeyInTabBar()
	{
		bool flag = (this.scroll != null && this.scroll.pointerIsDowning) || this.pointerIsDowning;
		if (!flag)
		{
			int num = this.currentTabIndex;
			bool flag2 = !this.IsTabOption();
			if (flag2)
			{
				bool flag3 = GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24];
				if (flag3)
				{
					this.currentTabIndex++;
					bool flag4 = this.currentTabIndex >= this.currentTabName.Length;
					if (flag4)
					{
						bool flag5 = GameCanvas.panel2 != null;
						if (flag5)
						{
							this.currentTabIndex = this.currentTabName.Length - 1;
							GameCanvas.isFocusPanel2 = true;
						}
						else
						{
							this.currentTabIndex = 0;
						}
					}
					this.selected = this.lastSelect[this.currentTabIndex];
					this.lastTabIndex[this.type] = this.currentTabIndex;
				}
				bool flag6 = GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23];
				if (flag6)
				{
					this.currentTabIndex--;
					bool flag7 = this.currentTabIndex < 0;
					if (flag7)
					{
						this.currentTabIndex = this.currentTabName.Length - 1;
					}
					bool isFocusPanel = GameCanvas.isFocusPanel2;
					if (isFocusPanel)
					{
						GameCanvas.isFocusPanel2 = false;
					}
					this.selected = this.lastSelect[this.currentTabIndex];
					this.lastTabIndex[this.type] = this.currentTabIndex;
				}
			}
			this.keyTouchTab = -1;
			for (int i = 0; i < this.currentTabName.Length; i++)
			{
				bool flag8 = !GameCanvas.isPointer(this.startTabPos + i * this.TAB_W, 52, this.TAB_W - 1, 25);
				if (!flag8)
				{
					this.keyTouchTab = i;
					bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
					if (isPointerJustRelease)
					{
						this.currentTabIndex = i;
						this.lastTabIndex[this.type] = i;
						GameCanvas.isPointerJustRelease = false;
						this.selected = this.lastSelect[this.currentTabIndex];
						bool flag9 = num == this.currentTabIndex && this.cmRun == 0;
						if (flag9)
						{
							this.cmtoY = 0;
							this.selected = (GameCanvas.isTouch ? -1 : 0);
						}
						break;
					}
				}
			}
			bool flag10 = num == this.currentTabIndex;
			if (!flag10)
			{
				this.size_tab = 0;
				SoundMn.gI().panelClick();
				int num2 = this.type;
				if (num2 <= 12)
				{
					switch (num2)
					{
					case 0:
					{
						bool flag11 = this.currentTabIndex == 0;
						if (flag11)
						{
							this.setTabTask();
						}
						bool flag12 = this.currentTabIndex == 1;
						if (flag12)
						{
							this.setTabInventory(true);
						}
						bool flag13 = this.currentTabIndex == 2;
						if (flag13)
						{
							this.setTabSkill();
						}
						bool flag14 = this.currentTabIndex == 3;
						if (flag14)
						{
							bool flag15 = this.mainTabName.Length > 4;
							if (flag15)
							{
								this.setTabClans();
							}
							else
							{
								this.setTabTool();
							}
						}
						bool flag16 = this.currentTabIndex == 4;
						if (flag16)
						{
							this.setTabTool();
						}
						break;
					}
					case 1:
						this.setTabShop();
						break;
					case 2:
					{
						bool flag17 = this.currentTabIndex == 0;
						if (flag17)
						{
							this.setTabBox();
						}
						bool flag18 = this.currentTabIndex == 1;
						if (flag18)
						{
							this.setTabInventory(true);
						}
						break;
					}
					case 3:
						this.setTabZone();
						break;
					default:
						if (num2 == 12)
						{
							bool flag19 = this.currentTabIndex == 0;
							if (flag19)
							{
								this.setTabCombine();
							}
							bool flag20 = this.currentTabIndex == 1;
							if (flag20)
							{
								this.setTabInventory(true);
							}
						}
						break;
					}
				}
				else if (num2 != 13)
				{
					if (num2 != 21)
					{
						if (num2 == 25)
						{
							this.setTabSpeacialSkill();
						}
					}
					else
					{
						bool flag21 = this.currentTabIndex == 0;
						if (flag21)
						{
							this.setTabPetInventory();
						}
						bool flag22 = this.currentTabIndex == 1;
						if (flag22)
						{
							this.setTabPetStatus();
						}
						bool flag23 = this.currentTabIndex == 2;
						if (flag23)
						{
							this.setTabInventory(true);
						}
					}
				}
				else
				{
					bool flag24 = this.currentTabIndex == 0;
					if (flag24)
					{
						bool flag25 = this.Equals(GameCanvas.panel);
						if (flag25)
						{
							this.setTabInventory(true);
						}
						else
						{
							bool flag26 = this.Equals(GameCanvas.panel2);
							if (flag26)
							{
								this.setTabGiaoDich(false);
							}
						}
					}
					bool flag27 = this.currentTabIndex == 1;
					if (flag27)
					{
						this.setTabGiaoDich(true);
					}
					bool flag28 = this.currentTabIndex == 2;
					if (flag28)
					{
						this.setTabGiaoDich(false);
					}
				}
				this.selected = this.lastSelect[this.currentTabIndex];
			}
		}
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x0006E11C File Offset: 0x0006C31C
	private void setTabPetStatus()
	{
		this.currentListLength = this.strStatus.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? -1 : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x00003A0D File Offset: 0x00001C0D
	private void setTabPetSkill()
	{
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x0006E1E8 File Offset: 0x0006C3E8
	private void setTabTool()
	{
		SoundMn.gI().getSoundOption();
		this.currentListLength = Panel.strTool.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? -1 : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x0006E2BC File Offset: 0x0006C4BC
	public void initTabClans()
	{
		bool flag = this.isSearchClan;
		if (flag)
		{
			this.currentListLength = ((this.clans != null) ? (this.clans.Length + 2) : 2);
			this.clanInfo = mResources.clan_list;
		}
		else
		{
			bool flag2 = this.isViewMember;
			if (flag2)
			{
				this.clanReport = string.Empty;
				this.currentListLength = ((this.member != null) ? this.member.size() : this.myMember.size()) + 2;
				this.clanInfo = mResources.member + " " + ((this.currClan == null) ? global::Char.myCharz().clan.name : this.currClan.name);
			}
			else
			{
				bool flag3 = this.isMessage;
				if (flag3)
				{
					this.currentListLength = ClanMessage.vMessage.size() + 2;
					this.clanInfo = mResources.msg;
					this.clanReport = string.Empty;
				}
			}
		}
		bool flag4 = global::Char.myCharz().clan == null;
		if (flag4)
		{
			this.clansOption = new string[][]
			{
				mResources.findClan,
				mResources.createClan
			};
		}
		else
		{
			bool flag5 = !this.isViewMember;
			if (flag5)
			{
				bool flag6 = this.myMember.size() > 1;
				if (flag6)
				{
					this.clansOption = new string[][]
					{
						mResources.chatClan,
						mResources.request_pea2,
						mResources.memberr
					};
				}
				else
				{
					this.clansOption = new string[][]
					{
						mResources.memberr
					};
				}
			}
			else
			{
				bool flag7 = global::Char.myCharz().role > 0;
				if (flag7)
				{
					this.clansOption = new string[][]
					{
						mResources.msgg,
						mResources.leaveClan
					};
				}
				else
				{
					bool flag8 = this.myMember.size() > 1;
					if (flag8)
					{
						this.clansOption = new string[][]
						{
							mResources.msgg,
							mResources.leaveClan,
							mResources.khau_hieuu,
							mResources.bieu_tuongg
						};
					}
					else
					{
						this.clansOption = new string[][]
						{
							mResources.msgg,
							mResources.khau_hieuu,
							mResources.bieu_tuongg
						};
					}
				}
			}
		}
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag9 = this.cmyLim < 0;
		if (flag9)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag10 = this.cmy < 0;
		if (flag10)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag11 = this.cmy > this.cmyLim;
		if (flag11)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x0006E588 File Offset: 0x0006C788
	public void setTabClans()
	{
		GameScr.isNewClanMessage = false;
		this.ITEM_HEIGHT = 24;
		bool flag = this.lastSelect != null && this.lastSelect[3] == 0;
		if (flag)
		{
			this.lastSelect[3] = -1;
		}
		this.currentListLength = 2;
		bool flag2 = global::Char.myCharz().clan != null;
		if (flag2)
		{
			this.isMessage = true;
			this.isViewMember = false;
			this.isSearchClan = false;
		}
		else
		{
			this.isMessage = false;
			this.isViewMember = false;
			this.isSearchClan = true;
		}
		bool flag3 = global::Char.myCharz().clan != null;
		if (flag3)
		{
			this.currentListLength = ClanMessage.vMessage.size() + 2;
		}
		this.initTabClans();
		this.cSelected = -1;
		bool flag4 = this.chatTField == null;
		if (flag4)
		{
			this.chatTField = new ChatTextField();
			this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			this.chatTField.initChatTextField();
			this.chatTField.parentScreen = GameCanvas.panel;
		}
		bool flag5 = global::Char.myCharz().clan == null;
		if (flag5)
		{
			this.clanReport = mResources.findingClan;
			Service.gI().searchClan(string.Empty);
		}
		this.selected = this.lastSelect[this.currentTabIndex];
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.selected = -1;
		}
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x0006E6FC File Offset: 0x0006C8FC
	public void initLogMessage()
	{
		this.currentListLength = this.logChat.size() + 1;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x000058B1 File Offset: 0x00003AB1
	private void setTabMessage()
	{
		this.ITEM_HEIGHT = 24;
		this.initLogMessage();
		this.selected = (GameCanvas.isTouch ? -1 : 0);
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x0006E7C4 File Offset: 0x0006C9C4
	public void setTabShop()
	{
		this.ITEM_HEIGHT = 24;
		bool flag = this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null && this.typeShop != 2;
		if (flag)
		{
			this.currentListLength = this.checkCurrentListLength(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length);
		}
		else
		{
			this.currentListLength = global::Char.myCharz().arrItemShop[this.currentTabIndex].Length;
		}
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag2 = this.cmyLim < 0;
		if (flag2)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag3 = this.cmy < 0;
		if (flag3)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag4 = this.cmy > this.cmyLim;
		if (flag4)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = (GameCanvas.isTouch ? -1 : 0);
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x0006E8F4 File Offset: 0x0006CAF4
	private void setTabSkill()
	{
		this.ITEM_HEIGHT = 30;
		this.currentListLength = global::Char.myCharz().nClass.skillTemplates.Length + 6;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = this.cmyLim;
		}
		this.selected = (GameCanvas.isTouch ? -1 : 0);
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x0006E9C0 File Offset: 0x0006CBC0
	private void setTabMapTrans()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = this.mapNames.Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = 0);
		this.selected = (GameCanvas.isTouch ? -1 : 0);
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0006EA20 File Offset: 0x0006CC20
	private void setTabZone()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = GameScr.gI().zones.Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = 0);
		this.selected = (GameCanvas.isTouch ? -1 : 0);
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0006EA84 File Offset: 0x0006CC84
	private void setTabBox()
	{
		this.currentListLength = this.checkCurrentListLength(global::Char.myCharz().arrItemBox.Length);
		this.ITEM_HEIGHT = 24;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 9;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = (GameCanvas.isTouch ? -1 : 0);
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x0006EB58 File Offset: 0x0006CD58
	private void setTabPetInventory()
	{
		this.ITEM_HEIGHT = 30;
		Item[] arrItemBody = global::Char.myPetz().arrItemBody;
		Skill[] arrPetSkill = global::Char.myPetz().arrPetSkill;
		this.currentListLength = arrItemBody.Length + arrPetSkill.Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = 0);
		}
		this.selected = (GameCanvas.isTouch ? -1 : 0);
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x0006EC38 File Offset: 0x0006CE38
	private void setTabInventory(bool resetSelect)
	{
		this.currentListLength = this.checkCurrentListLength(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length);
		this.ITEM_HEIGHT = 24;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (resetSelect)
		{
			this.selected = (GameCanvas.isTouch ? -1 : 0);
		}
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x0006ED1C File Offset: 0x0006CF1C
	private void setTabMap()
	{
		bool flag = !Panel.isPaintMap;
		if (!flag)
		{
			bool flag2 = TileMap.lastPlanetId != TileMap.planetID;
			if (flag2)
			{
				Res.outz("LOAD TAM HINH");
				Panel.imgMap = GameCanvas.loadImageRMS("/img/map" + TileMap.planetID + ".png");
				TileMap.lastPlanetId = TileMap.planetID;
			}
			this.cmxMap = this.getXMap() - this.wScroll / 2;
			this.cmyMap = this.getYMap() + this.yScroll - (this.yScroll + this.hScroll / 2);
			this.pa1 = this.cmxMap;
			this.pa2 = this.cmyMap;
			this.cmxMapLim = 250 - this.wScroll;
			this.cmyMapLim = 220 - this.hScroll;
			bool flag3 = this.cmxMapLim < 0;
			if (flag3)
			{
				this.cmxMapLim = 0;
			}
			bool flag4 = this.cmyMapLim < 0;
			if (flag4)
			{
				this.cmyMapLim = 0;
			}
			for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
			{
				bool flag5 = TileMap.mapID == Panel.mapId[(int)TileMap.planetID][i];
				if (flag5)
				{
					this.xMove = Panel.mapX[(int)TileMap.planetID][i] + this.xScroll;
					this.yMove = Panel.mapY[(int)TileMap.planetID][i] + this.yScroll + 5;
					break;
				}
			}
			this.xMap = this.getXMap() + this.xScroll;
			this.yMap = this.getYMap() + this.yScroll;
			this.xMapTask = this.getXMapTask() + this.xScroll;
			this.yMapTask = this.getYMapTask() + this.yScroll;
			Resources.UnloadUnusedAssets();
			GC.Collect();
		}
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x000058D4 File Offset: 0x00003AD4
	private void setTabTask()
	{
		this.cmyQuest = 0;
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x0006EEFC File Offset: 0x0006D0FC
	public void moveCamera()
	{
		bool flag = this.timeShow > 0;
		if (flag)
		{
			this.timeShow--;
		}
		bool flag2 = this.justRelease && this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.maxPageShop[this.currentTabIndex] > 1;
		if (flag2)
		{
			bool flag3 = this.cmy < -50;
			if (flag3)
			{
				InfoDlg.showWait();
				this.justRelease = false;
				bool flag4 = this.currPageShop[this.currentTabIndex] <= 0;
				if (flag4)
				{
					Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.maxPageShop[this.currentTabIndex] - 1, -1);
				}
				else
				{
					Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] - 1, -1);
				}
			}
			else
			{
				bool flag5 = this.cmy > this.cmyLim + 50;
				if (flag5)
				{
					this.justRelease = false;
					InfoDlg.showWait();
					bool flag6 = this.currPageShop[this.currentTabIndex] >= this.maxPageShop[this.currentTabIndex] - 1;
					if (flag6)
					{
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, 0, -1);
					}
					else
					{
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] + 1, -1);
					}
				}
			}
		}
		bool flag7 = this.cmx != this.cmtoX && !this.pointerIsDowning;
		if (flag7)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 3;
			this.cmdx &= 15;
		}
		bool flag8 = global::Math.abs(this.cmtoX - this.cmx) < 10;
		if (flag8)
		{
			this.cmx = this.cmtoX;
		}
		bool flag9 = this.isClose;
		if (flag9)
		{
			this.isClose = false;
			this.cmtoX = this.wScroll;
		}
		bool flag10 = this.cmtoX >= this.wScroll - 10 && this.cmx >= this.wScroll - 10 && this.position == 0;
		if (flag10)
		{
			this.isShow = false;
			this.cleanCombine();
			bool flag11 = this.isChangeZone;
			if (flag11)
			{
				this.isChangeZone = false;
				bool flag12 = global::Char.myCharz().cHP > 0 && global::Char.myCharz().statusMe != 14;
				if (flag12)
				{
					InfoDlg.showWait();
					bool flag13 = this.type == 3;
					if (flag13)
					{
						Service.gI().requestChangeZone(this.selected, -1);
					}
					else
					{
						bool flag14 = this.type == 14;
						if (flag14)
						{
							Pk9rXmap.SelectMapTrans(this.selected);
						}
					}
				}
			}
			bool flag15 = this.isSelectPlayerMenu;
			if (flag15)
			{
				this.isSelectPlayerMenu = false;
				Command command = (Command)this.vPlayerMenu.elementAt(this.selected);
				command.performAction();
			}
			this.vPlayerMenu.removeAllElements();
			this.charMenu = null;
		}
		bool flag16 = this.cmRun != 0 && !this.pointerIsDowning;
		if (flag16)
		{
			this.cmtoY += this.cmRun / 100;
			bool flag17 = this.cmtoY < 0;
			if (flag17)
			{
				this.cmtoY = 0;
			}
			else
			{
				bool flag18 = this.cmtoY > this.cmyLim;
				if (flag18)
				{
					this.cmtoY = this.cmyLim;
				}
				else
				{
					this.cmy = this.cmtoY;
				}
			}
			this.cmRun = this.cmRun * 9 / 10;
			bool flag19 = this.cmRun < 100 && this.cmRun > -100;
			if (flag19)
			{
				this.cmRun = 0;
			}
		}
		bool flag20 = this.cmy != this.cmtoY && !this.pointerIsDowning;
		if (flag20)
		{
			this.cmvy = this.cmtoY - this.cmy << 2;
			this.cmdy += this.cmvy;
			this.cmy += this.cmdy >> 4;
			this.cmdy &= 15;
		}
		this.cmyLast[this.currentTabIndex] = this.cmy;
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x0006F380 File Offset: 0x0006D580
	public void paintDetail(mGraphics g)
	{
		bool flag = this.cp == null || this.cp.says == null;
		if (!flag)
		{
			this.cp.paint(g);
			int num = this.cp.cx + 13;
			int num2 = this.cp.cy + 11;
			bool flag2 = this.type == 15;
			if (flag2)
			{
				num += 5;
				num2 += 26;
			}
			bool flag3 = this.type == 0 && this.currentTabIndex == 3;
			if (flag3)
			{
				bool flag4 = this.isSearchClan;
				if (flag4)
				{
					num -= 5;
				}
				else
				{
					bool flag5 = this.partID != null || this.charInfo != null;
					if (flag5)
					{
						num = this.cp.cx + 21;
						num2 = this.cp.cy + 40;
					}
				}
			}
			bool flag6 = this.partID != null;
			if (flag6)
			{
				Part part = GameScr.parts[this.partID[0]];
				Part part2 = GameScr.parts[this.partID[1]];
				Part part3 = GameScr.parts[this.partID[2]];
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + global::Char.CharInfo[0][0][1] + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 - global::Char.CharInfo[0][0][2] + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
				SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[0][1][0]].id, num + global::Char.CharInfo[0][1][1] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dx, num2 - global::Char.CharInfo[0][1][2] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dy, 0, 0);
				SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[0][2][0]].id, num + global::Char.CharInfo[0][2][1] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dx, num2 - global::Char.CharInfo[0][2][2] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dy, 0, 0);
			}
			else
			{
				bool flag7 = this.charInfo != null;
				if (flag7)
				{
					this.charInfo.paintCharBody(g, num + 5, num2 + 25, 1, 0, true);
				}
				else
				{
					bool flag8 = this.idIcon != -1;
					if (flag8)
					{
						SmallImage.drawSmallImage(g, this.idIcon, num, num2, 0, 3);
					}
				}
			}
			bool flag9 = this.currItem != null && this.currItem.template.type != 5;
			if (flag9)
			{
				bool flag10 = this.currItem.compare > 0;
				if (flag10)
				{
					g.drawImage(Panel.imgUp, num - 7, num2 + 13, 3);
					mFont.tahoma_7b_green.drawString(g, Res.abs(this.currItem.compare) + string.Empty, num + 1, num2 + 8, 0);
				}
				else
				{
					bool flag11 = this.currItem.compare < 0 && this.currItem.compare != -1;
					if (flag11)
					{
						g.drawImage(Panel.imgDown, num - 7, num2 + 13, 3);
						mFont.tahoma_7b_red.drawString(g, Res.abs(this.currItem.compare) + string.Empty, num + 1, num2 + 8, 0);
					}
				}
			}
		}
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x0006F738 File Offset: 0x0006D938
	public void paintTop(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		bool flag = this.currentListLength == 0;
		if (!flag)
		{
			int num = (this.cmy + this.hScroll) / 24 + 1;
			bool flag2 = num < this.hScroll / 24 + 1;
			if (flag2)
			{
				num = this.hScroll / 24 + 1;
			}
			bool flag3 = num > this.currentListLength;
			if (flag3)
			{
				num = this.currentListLength;
			}
			int num2 = this.cmy / 24;
			bool flag4 = num2 >= num;
			if (flag4)
			{
				num2 = num - 1;
			}
			bool flag5 = num2 < 0;
			if (flag5)
			{
				num2 = 0;
			}
			for (int i = num2; i < num; i++)
			{
				int num3 = this.xScroll;
				int num4 = this.yScroll + i * this.ITEM_HEIGHT;
				int num5 = 24;
				int h = this.ITEM_HEIGHT - 1;
				int num6 = this.xScroll + num5;
				int num7 = this.yScroll + i * this.ITEM_HEIGHT;
				int num8 = this.wScroll - num5;
				int num9 = this.ITEM_HEIGHT - 1;
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num6, num7, num8, num9);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num3, num4, num5, h);
				TopInfo topInfo = (TopInfo)this.vTop.elementAt(i);
				bool flag6 = topInfo.headICON != -1;
				if (flag6)
				{
					SmallImage.drawSmallImage(g, topInfo.headICON, num3, num4, 0, 0);
				}
				else
				{
					Part part = GameScr.parts[topInfo.headID];
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num4 + num9 - 1, 0, mGraphics.BOTTOM | mGraphics.LEFT);
				}
				g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
				bool flag7 = topInfo.pId != global::Char.myCharz().charID;
				if (flag7)
				{
					mFont.tahoma_7b_green.drawString(g, topInfo.name, num6 + 5, num7, 0);
				}
				else
				{
					mFont.tahoma_7b_red.drawString(g, topInfo.name, num6 + 5, num7, 0);
				}
				mFont.tahoma_7_blue.drawString(g, topInfo.info, num6 + num8 - 5, num7 + 11, 1);
				mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
				{
					mResources.rank,
					": ",
					topInfo.rank,
					string.Empty
				}), num6 + 5, num7 + 11, 0);
			}
			this.paintScrollArrow(g);
		}
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x0006FA60 File Offset: 0x0006DC60
	public void paint(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY() + mGraphics.addYWhenOpenKeyBoard);
		g.translate(-this.cmx, 0);
		g.translate(this.X, this.Y);
		bool flag = GameCanvas.panel.combineSuccess != -1;
		if (flag)
		{
			bool flag2 = this.Equals(GameCanvas.panel);
			if (flag2)
			{
				this.paintCombineEff(g);
			}
		}
		else
		{
			GameCanvas.paintz.paintFrameSimple(this.X, this.Y, this.W, this.H, g);
			this.paintTopInfo(g);
			this.paintBottomMoneyInfo(g);
			this.paintTab(g);
			switch (this.type)
			{
			case 0:
			{
				bool flag3 = this.currentTabIndex == 0;
				if (flag3)
				{
					this.paintTask(g);
				}
				bool flag4 = this.currentTabIndex == 1;
				if (flag4)
				{
					this.paintInventory(g);
				}
				bool flag5 = this.currentTabIndex == 2;
				if (flag5)
				{
					this.paintSkill(g);
				}
				bool flag6 = this.currentTabIndex == 3;
				if (flag6)
				{
					bool flag7 = this.mainTabName.Length == 4;
					if (flag7)
					{
						this.paintTools(g);
					}
					else
					{
						this.paintClans(g);
					}
				}
				bool flag8 = this.currentTabIndex == 4;
				if (flag8)
				{
					this.paintTools(g);
				}
				break;
			}
			case 1:
				this.paintShop(g);
				break;
			case 2:
			{
				bool flag9 = this.currentTabIndex == 0;
				if (flag9)
				{
					this.paintBox(g);
				}
				bool flag10 = this.currentTabIndex == 1;
				if (flag10)
				{
					this.paintInventory(g);
				}
				break;
			}
			case 3:
				this.paintZone(g);
				break;
			case 4:
				this.paintMap(g);
				break;
			case 7:
				this.paintInventory(g);
				break;
			case 8:
				this.paintLogChat(g);
				break;
			case 9:
				this.paintArchivement(g);
				break;
			case 10:
				this.paintPlayerMenu(g);
				break;
			case 11:
				this.paintFriend(g);
				break;
			case 12:
			{
				bool flag11 = this.currentTabIndex == 0;
				if (flag11)
				{
					this.paintCombine(g);
				}
				bool flag12 = this.currentTabIndex == 1;
				if (flag12)
				{
					this.paintInventory(g);
				}
				break;
			}
			case 13:
			{
				bool flag13 = this.currentTabIndex == 0;
				if (flag13)
				{
					bool flag14 = this.Equals(GameCanvas.panel);
					if (flag14)
					{
						this.paintInventory(g);
					}
					else
					{
						this.paintGiaoDich(g, false);
					}
				}
				bool flag15 = this.currentTabIndex == 1;
				if (flag15)
				{
					this.paintGiaoDich(g, true);
				}
				bool flag16 = this.currentTabIndex == 2;
				if (flag16)
				{
					this.paintGiaoDich(g, false);
				}
				break;
			}
			case 14:
				this.paintMapTrans(g);
				break;
			case 15:
				this.paintTop(g);
				break;
			case 16:
				this.paintEnemy(g);
				break;
			case 17:
				this.paintShop(g);
				break;
			case 18:
				this.paintFlagChange(g);
				break;
			case 19:
				this.paintOption(g);
				break;
			case 20:
				this.paintAccount(g);
				break;
			case 21:
			{
				bool flag17 = this.currentTabIndex == 0;
				if (flag17)
				{
					this.paintPetInventory(g);
				}
				bool flag18 = this.currentTabIndex == 1;
				if (flag18)
				{
					this.paintPetStatus(g);
				}
				bool flag19 = this.currentTabIndex == 2;
				if (flag19)
				{
					this.paintInventory(g);
				}
				break;
			}
			case 22:
				this.paintAuto(g);
				break;
			case 23:
				this.paintGameInfo(g);
				break;
			case 24:
				this.paintGameSubInfo(g);
				break;
			case 25:
				this.paintSpeacialSkill(g);
				break;
			}
			GameScr.resetTranslate(g);
			this.paintDetail(g);
			bool flag20 = this.cmx == this.cmtoX;
			if (flag20)
			{
				this.cmdClose.paint(g);
			}
			bool flag21 = this.tabIcon != null && this.tabIcon.isShow;
			if (flag21)
			{
				this.tabIcon.paint(g);
			}
		}
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x0006FEA8 File Offset: 0x0006E0A8
	private void paintShop(mGraphics g)
	{
		bool flag = this.type == 1 && this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null && this.typeShop != 2;
		if (flag)
		{
			this.paintInventory(g);
		}
		else
		{
			g.setColor(16711680);
			g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
			bool flag2 = this.typeShop == 2 && this.Equals(GameCanvas.panel);
			if (flag2)
			{
				bool flag3 = this.currentTabIndex <= 3 && GameCanvas.isTouch;
				if (flag3)
				{
					bool flag4 = this.cmy < -50;
					if (flag4)
					{
						GameCanvas.paintShukiren(this.xScroll + this.wScroll / 2, this.yScroll + 30, g);
					}
					else
					{
						bool flag5 = this.cmy < 0;
						if (flag5)
						{
							mFont.tahoma_7_grey.drawString(g, mResources.getDown, this.xScroll + this.wScroll / 2, this.yScroll + 15, 2);
						}
						else
						{
							bool flag6 = this.cmyLim >= 0;
							if (flag6)
							{
								bool flag7 = this.cmy > this.cmyLim + 50;
								if (flag7)
								{
									GameCanvas.paintShukiren(this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - 30, g);
								}
								else
								{
									bool flag8 = this.cmy > this.cmyLim;
									if (flag8)
									{
										mFont.tahoma_7_grey.drawString(g, mResources.getUp, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - 25, 2);
									}
								}
							}
						}
					}
				}
				bool flag9 = global::Char.myCharz().arrItemShop[this.currentTabIndex].Length == 0 && this.type != 17;
				if (flag9)
				{
					mFont.tahoma_7_grey.drawString(g, mResources.notYetSell, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - 10, 2);
					return;
				}
			}
			g.translate(0, -this.cmy);
			Item[] array = global::Char.myCharz().arrItemShop[this.currentTabIndex];
			bool flag10 = this.typeShop == 2 && (this.currentTabIndex == 4 || this.type == 17);
			if (flag10)
			{
				array = global::Char.myCharz().arrItemShop[4];
				bool flag11 = array.Length == 0;
				if (flag11)
				{
					mFont.tahoma_7_grey.drawString(g, mResources.notYetSell, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - 10, 2);
					return;
				}
			}
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				int num2 = this.xScroll + 26;
				int num3 = this.yScroll + i * this.ITEM_HEIGHT;
				int num4 = this.wScroll - 26;
				int h = this.ITEM_HEIGHT - 1;
				int num5 = this.xScroll;
				int num6 = this.yScroll + i * this.ITEM_HEIGHT;
				int num7 = 24;
				int num8 = this.ITEM_HEIGHT - 1;
				bool flag12 = num3 - this.cmy > this.yScroll + this.hScroll || num3 - this.cmy < this.yScroll - this.ITEM_HEIGHT;
				if (!flag12)
				{
					g.setColor((i != this.selected) ? 15196114 : 16383818);
					g.fillRect(num2, num3, num4, h);
					g.setColor((i != this.selected) ? 9993045 : 9541120);
					g.fillRect(num5, num6, num7, num8);
					Item item = array[i];
					bool flag13 = item != null;
					if (flag13)
					{
						string text = string.Empty;
						mFont mFont = mFont.tahoma_7_green2;
						bool flag14 = item.isMe != 0 && this.typeShop == 2 && this.currentTabIndex <= 3 && !this.Equals(GameCanvas.panel2);
						if (flag14)
						{
							mFont = mFont.tahoma_7b_green;
						}
						bool flag15 = item.itemOption != null;
						if (flag15)
						{
							for (int j = 0; j < item.itemOption.Length; j++)
							{
								bool flag16 = item.itemOption[j].optionTemplate.id == 72;
								if (flag16)
								{
									text = " [+" + item.itemOption[j].param + "]";
								}
								bool flag17 = item.itemOption[j].optionTemplate.id == 41;
								if (flag17)
								{
									bool flag18 = item.itemOption[j].param == 1;
									if (flag18)
									{
										mFont = Panel.GetFont(0);
									}
									else
									{
										bool flag19 = item.itemOption[j].param == 2;
										if (flag19)
										{
											mFont = Panel.GetFont(2);
										}
										else
										{
											bool flag20 = item.itemOption[j].param == 3;
											if (flag20)
											{
												mFont = Panel.GetFont(8);
											}
											else
											{
												bool flag21 = item.itemOption[j].param == 4;
												if (flag21)
												{
													mFont = Panel.GetFont(7);
												}
											}
										}
									}
								}
							}
						}
						mFont.drawString(g, string.Concat(new object[]
						{
							"[",
							item.template.id,
							"] ",
							item.template.name,
							text
						}), num2 + 5, num3 + 1, 0);
						string text2 = string.Empty;
						bool flag22 = item.itemOption != null && item.itemOption.Length >= 1;
						if (flag22)
						{
							bool flag23 = item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107;
							if (flag23)
							{
								text2 += item.itemOption[0].getOptionString();
							}
							mFont mFont2 = mFont.tahoma_7_blue;
							bool flag24 = item.compare < 0 && item.template.type != 5;
							if (flag24)
							{
								mFont2 = mFont.tahoma_7_red;
							}
							bool flag25 = this.typeShop == 2 && item.itemOption.Length > 1 && item.buyType != -1;
							if (flag25)
							{
								text2 += string.Empty;
							}
							bool flag26 = this.typeShop != 2 || (this.typeShop == 2 && item.buyType <= 1);
							if (flag26)
							{
								mFont2.drawString(g, text2, num2 + 5, num3 + 11, 0);
							}
						}
						bool flag27 = item.buySpec > 0;
						if (flag27)
						{
							SmallImage.drawSmallImage(g, (int)item.iconSpec, num2 + num4 - 7, num3 + 9, 0, 3);
							mFont.tahoma_7b_blue.drawString(g, Res.formatNumber((long)item.buySpec), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
						}
						bool flag28 = item.buyCoin != 0 || item.buyGold != 0;
						if (flag28)
						{
							bool flag29 = this.typeShop != 2 && item.powerRequire == 0L;
							if (flag29)
							{
								bool flag30 = item.buyCoin > 0 && item.buyGold > 0;
								if (flag30)
								{
									bool flag31 = item.buyCoin > 0;
									if (flag31)
									{
										g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 7, 3);
										mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber((long)item.buyCoin), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
									}
									bool flag32 = item.buyGold > 0;
									if (flag32)
									{
										g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7 + 11, 3);
										mFont.tahoma_7b_green.drawString(g, Res.formatNumber((long)item.buyGold), num2 + num4 - 15, num3 + 12, mFont.RIGHT);
									}
								}
								else
								{
									bool flag33 = item.buyCoin > 0;
									if (flag33)
									{
										g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 7, 3);
										mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber((long)item.buyCoin), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
									}
									bool flag34 = item.buyGold > 0;
									if (flag34)
									{
										g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7, 3);
										mFont.tahoma_7b_green.drawString(g, Res.formatNumber((long)item.buyGold), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
									}
								}
							}
							bool flag35 = this.typeShop == 2 && this.currentTabIndex <= 3 && !this.Equals(GameCanvas.panel2);
							if (flag35)
							{
								bool flag36 = item.buyCoin > 0 && item.buyGold > 0;
								if (flag36)
								{
									bool flag37 = item.buyCoin > 0;
									if (flag37)
									{
										g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 7, 3);
										mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
									}
									bool flag38 = item.buyGold > 0;
									if (flag38)
									{
										g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7 + 11, 3);
										mFont.tahoma_7b_green.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 12, mFont.RIGHT);
									}
								}
								else
								{
									bool flag39 = item.buyCoin > 0;
									if (flag39)
									{
										g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 7, 3);
										mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
									}
									bool flag40 = item.buyGold > 0;
									if (flag40)
									{
										g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7, 3);
										mFont.tahoma_7b_green.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
									}
								}
							}
						}
						SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
						bool flag41 = item.quantity > 1;
						if (flag41)
						{
							mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
						}
						bool flag42 = item.newItem && GameCanvas.gameTick % 10 > 5;
						if (flag42)
						{
							g.drawImage(Panel.imgNew, num5 + num7 / 2, num3 + 19, 3);
						}
					}
					bool flag43 = this.typeShop != 2 || (!this.Equals(GameCanvas.panel2) && this.currentTabIndex != 4) || item.buyType == 0;
					if (!flag43)
					{
						bool flag44 = item.buyType == 1;
						if (flag44)
						{
							mFont.tahoma_7_green.drawString(g, mResources.dangban, num2 + num4 - 5, num3 + 1, mFont.RIGHT);
							bool flag45 = item.buyCoin != -1;
							if (flag45)
							{
								g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 19, 3);
								mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 15, num3 + 13, mFont.RIGHT);
							}
							else
							{
								bool flag46 = item.buyGold != -1;
								if (flag46)
								{
									g.drawImage(Panel.imgLuongKhoa, num2 + num4 - 7, num3 + 17, 3);
									mFont.tahoma_7b_red.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 11, mFont.RIGHT);
								}
							}
						}
						else
						{
							bool flag47 = item.buyType == 2;
							if (flag47)
							{
								mFont.tahoma_7b_blue.drawString(g, mResources.daban, num2 + num4 - 5, num3 + 1, mFont.RIGHT);
								bool flag48 = item.buyCoin != -1;
								if (flag48)
								{
									g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 17, 3);
									mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 15, num3 + 11, mFont.RIGHT);
								}
								else
								{
									bool flag49 = item.buyGold != -1;
									if (flag49)
									{
										g.drawImage(Panel.imgLuongKhoa, num2 + num4 - 7, num3 + 17, 3);
										mFont.tahoma_7b_red.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 11, mFont.RIGHT);
									}
								}
							}
						}
					}
				}
			}
			this.paintScrollArrow(g);
		}
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x00003A0D File Offset: 0x00001C0D
	private void paintAuto(mGraphics g)
	{
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x00070C2C File Offset: 0x0006EE2C
	private void paintPetStatus(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < this.strStatus.Length; i++)
		{
			int x = this.xScroll;
			int num = this.yScroll + i * this.ITEM_HEIGHT;
			int num2 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			bool flag = num - this.cmy <= this.yScroll + this.hScroll && num - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
			if (flag)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(x, num, num2, h);
				mFont.tahoma_7b_dark.drawString(g, this.strStatus[i], this.xScroll + this.wScroll / 2, num + 6, mFont.CENTER);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x00003A0D File Offset: 0x00001C0D
	private void paintPetSkill()
	{
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x00070D4C File Offset: 0x0006EF4C
	private void paintPetInventory(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		Item[] arrItemBody = global::Char.myPetz().arrItemBody;
		Skill[] arrPetSkill = global::Char.myPetz().arrPetSkill;
		for (int i = 0; i < arrItemBody.Length + arrPetSkill.Length; i++)
		{
			bool flag = i < arrItemBody.Length;
			int num = i;
			int num2 = i - arrItemBody.Length;
			int num3 = this.xScroll + 36;
			int num4 = this.yScroll + i * this.ITEM_HEIGHT;
			int num5 = this.wScroll - 36;
			int h = this.ITEM_HEIGHT - 1;
			int num6 = this.xScroll;
			int num7 = this.yScroll + i * this.ITEM_HEIGHT;
			int num8 = 34;
			int num9 = this.ITEM_HEIGHT - 1;
			bool flag2 = num4 - this.cmy > this.yScroll + this.hScroll || num4 - this.cmy < this.yScroll - this.ITEM_HEIGHT;
			if (!flag2)
			{
				Item item = (!flag) ? null : arrItemBody[num];
				g.setColor((i == this.selected) ? 16383818 : ((!flag) ? 15723751 : 15196114));
				g.fillRect(num3, num4, num5, h);
				g.setColor((i == this.selected) ? 9541120 : ((!flag) ? 11837316 : 9993045));
				bool flag3 = item != null;
				if (flag3)
				{
					for (int j = 0; j < item.itemOption.Length; j++)
					{
						bool flag4 = item.itemOption[j].optionTemplate.id == 72 && item.itemOption[j].param > 0;
						if (flag4)
						{
							sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[j].param);
							int color_ItemBg = Panel.GetColor_ItemBg((int)color_Item_Upgrade);
							bool flag5 = color_ItemBg != -1;
							if (flag5)
							{
								g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
							}
						}
					}
				}
				g.fillRect(num6, num7, num8, num9);
				bool flag6 = item != null && item.isSelect && GameCanvas.panel.type == 12;
				if (flag6)
				{
					g.setColor((i != this.selected) ? 6047789 : 7040779);
					g.fillRect(num6, num7, num8, num9);
				}
				bool flag7 = item != null;
				if (flag7)
				{
					string text = string.Empty;
					mFont mFont = mFont.tahoma_7_green2;
					bool flag8 = item.itemOption != null;
					if (flag8)
					{
						for (int k = 0; k < item.itemOption.Length; k++)
						{
							bool flag9 = item.itemOption[k].optionTemplate.id == 72;
							if (flag9)
							{
								text = " [+" + item.itemOption[k].param + "]";
							}
							bool flag10 = item.itemOption[k].optionTemplate.id == 41;
							if (flag10)
							{
								bool flag11 = item.itemOption[k].param == 1;
								if (flag11)
								{
									mFont = Panel.GetFont(0);
								}
								else
								{
									bool flag12 = item.itemOption[k].param == 2;
									if (flag12)
									{
										mFont = Panel.GetFont(2);
									}
									else
									{
										bool flag13 = item.itemOption[k].param == 3;
										if (flag13)
										{
											mFont = Panel.GetFont(8);
										}
										else
										{
											bool flag14 = item.itemOption[k].param == 4;
											if (flag14)
											{
												mFont = Panel.GetFont(7);
											}
										}
									}
								}
							}
						}
					}
					mFont.drawString(g, string.Concat(new object[]
					{
						"[",
						item.template.id,
						"] ",
						item.template.name,
						text
					}), num3 + 5, num4 + 1, 0);
					mFont.tahoma_7b_red.drawString(g, VuDang.saoTrongBalo(item), num5 + 15, num7 + 1, 0);
					string text2 = string.Empty;
					bool flag15 = item.itemOption != null;
					if (flag15)
					{
						bool flag16 = item.itemOption.Length != 0 && item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107;
						if (flag16)
						{
							text2 += item.itemOption[0].getOptionString();
						}
						mFont mFont2 = mFont.tahoma_7_blue;
						bool flag17 = item.compare < 0 && item.template.type != 5;
						if (flag17)
						{
							mFont2 = mFont.tahoma_7_red;
						}
						bool flag18 = item.itemOption.Length > 1;
						if (flag18)
						{
							for (int l = 1; l < 2; l++)
							{
								bool flag19 = item.itemOption[l] != null && item.itemOption[l].optionTemplate.id != 102 && item.itemOption[l].optionTemplate.id != 107;
								if (flag19)
								{
									text2 = text2 + "," + item.itemOption[l].getOptionString();
								}
							}
						}
						mFont2.drawString(g, text2, num3 + 5, num4 + 11, mFont.LEFT);
					}
					SmallImage.drawSmallImage(g, (int)item.template.iconID, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
					bool flag20 = item.itemOption != null;
					if (flag20)
					{
						for (int m = 0; m < item.itemOption.Length; m++)
						{
							this.paintOptItem(g, item.itemOption[m].optionTemplate.id, item.itemOption[m].param, num6, num7, num8, num9);
						}
						for (int n = 0; n < item.itemOption.Length; n++)
						{
							this.paintOptSlotItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num6, num7, num8, num9);
						}
					}
					bool flag21 = item.quantity > 1;
					if (flag21)
					{
						mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num6 + num8, num7 + num9 - mFont.tahoma_7_yellow.getHeight(), 1);
					}
				}
				else
				{
					bool flag22 = !flag;
					if (flag22)
					{
						Skill skill = arrPetSkill[num2];
						g.drawImage(GameScr.imgSkill, num6 + num8 / 2, num7 + num9 / 2, 3);
						bool flag23 = skill.template != null;
						if (flag23)
						{
							mFont.tahoma_7_blue.drawString(g, skill.template.name, num3 + 5, num4 + 1, 0);
							mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
							{
								mResources.level,
								": ",
								skill.point,
								string.Empty
							}), num3 + 5, num4 + 11, 0);
							SmallImage.drawSmallImage(g, skill.template.iconId, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
						}
						else
						{
							mFont.tahoma_7_green2.drawString(g, skill.moreInfo, num3 + 5, num4 + 5, 0);
							SmallImage.drawSmallImage(g, GameScr.efs[98].arrEfInfo[0].idImg, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x00071570 File Offset: 0x0006F770
	private void paintScrollArrow(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		bool flag = (this.cmy > 24 && this.currentListLength > 0) || (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.maxPageShop[this.currentTabIndex] > 1);
		if (flag)
		{
			g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, this.xScroll + this.wScroll - 12, this.yScroll + 3, 0);
		}
		bool flag2 = (this.cmy < this.cmyLim && this.currentListLength > 0) || (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.maxPageShop[this.currentTabIndex] > 1);
		if (flag2)
		{
			g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.xScroll + this.wScroll - 12, this.yScroll + this.hScroll - 8, 0);
		}
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x00071680 File Offset: 0x0006F880
	private void paintTools(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.strTool.Length; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			bool flag = num2 - this.cmy > this.yScroll + this.hScroll || num2 - this.cmy < this.yScroll - this.ITEM_HEIGHT;
			if (!flag)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num, num2, num3, h);
				mFont.tahoma_7b_dark.drawString(g, Panel.strTool[i], this.xScroll + this.wScroll / 2, num2 + 6, mFont.CENTER);
				bool flag2 = !Panel.strTool[i].Equals(mResources.gameInfo);
				if (!flag2)
				{
					for (int j = 0; j < Panel.vGameInfo.size(); j++)
					{
						GameInfo gameInfo = (GameInfo)Panel.vGameInfo.elementAt(j);
						bool flag3 = !gameInfo.hasRead;
						if (flag3)
						{
							bool flag4 = GameCanvas.gameTick % 20 > 10;
							if (flag4)
							{
								g.drawImage(Panel.imgNew, num + 10, num2 + 10, 3);
							}
							break;
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x0007182C File Offset: 0x0006FA2C
	private void paintGameSubInfo(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.contenInfo.Length; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * 15;
			int num3 = this.wScroll - 1;
			int num4 = this.ITEM_HEIGHT - 1;
			bool flag = num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
			if (flag)
			{
				mFont.tahoma_7b_dark.drawString(g, Panel.contenInfo[i], this.xScroll + 5, num2 + 6, mFont.LEFT);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x00071918 File Offset: 0x0006FB18
	private void paintGameInfo(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.vGameInfo.size(); i++)
		{
			GameInfo gameInfo = (GameInfo)Panel.vGameInfo.elementAt(i);
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			bool flag = num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
			if (flag)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num, num2, num3, h);
				mFont.tahoma_7b_dark.drawString(g, gameInfo.main, this.xScroll + this.wScroll / 2, num2 + 6, mFont.CENTER);
				bool flag2 = !gameInfo.hasRead && GameCanvas.gameTick % 20 > 10;
				if (flag2)
				{
					g.drawImage(Panel.imgNew, num + 10, num2 + 10, 3);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x00071A80 File Offset: 0x0006FC80
	private void paintSkill(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		int num = global::Char.myCharz().nClass.skillTemplates.Length;
		for (int i = 0; i < num + 6; i++)
		{
			int num2 = this.xScroll + 30;
			int num3 = this.yScroll + i * this.ITEM_HEIGHT;
			int num4 = this.wScroll - 30;
			int h = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll;
			int num6 = this.yScroll + i * this.ITEM_HEIGHT;
			int num7 = this.ITEM_HEIGHT - 1;
			bool flag = num3 - this.cmy > this.yScroll + this.hScroll || num3 - this.cmy < this.yScroll - this.ITEM_HEIGHT;
			if (!flag)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				bool flag2 = i == 5;
				if (flag2)
				{
					g.setColor((i != this.selected) ? 16765060 : 16776068);
				}
				g.fillRect(num2, num3, num4, h);
				g.drawImage(GameScr.imgSkill, num5, num6, 0);
				bool flag3 = i == 0;
				if (flag3)
				{
					SmallImage.drawSmallImage(g, 567, num5 + 4, num6 + 4, 0, 0);
					string st = string.Concat(new string[]
					{
						mResources.HP,
						" ",
						mResources.root,
						": ",
						NinjaUtil.getMoneys((long)global::Char.myCharz().cHPGoc)
					});
					mFont.tahoma_7b_blue.drawString(g, st, num2 + 5, num3 + 3, 0);
					mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
					{
						NinjaUtil.getMoneys((long)(global::Char.myCharz().cHPGoc + 1000)),
						" ",
						mResources.potential,
						": ",
						mResources.increase,
						" ",
						global::Char.myCharz().hpFrom1000TiemNang
					}), num2 + 5, num3 + 15, 0);
				}
				bool flag4 = i == 1;
				if (flag4)
				{
					SmallImage.drawSmallImage(g, 569, num5 + 4, num6 + 4, 0, 0);
					string st2 = string.Concat(new string[]
					{
						mResources.KI,
						" ",
						mResources.root,
						": ",
						NinjaUtil.getMoneys((long)global::Char.myCharz().cMPGoc)
					});
					mFont.tahoma_7b_blue.drawString(g, st2, num2 + 5, num3 + 3, 0);
					mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
					{
						NinjaUtil.getMoneys((long)(global::Char.myCharz().cMPGoc + 1000)),
						" ",
						mResources.potential,
						": ",
						mResources.increase,
						" ",
						global::Char.myCharz().mpFrom1000TiemNang
					}), num2 + 5, num3 + 15, 0);
				}
				bool flag5 = i == 2;
				if (flag5)
				{
					SmallImage.drawSmallImage(g, 568, num5 + 4, num6 + 4, 0, 0);
					string st3 = string.Concat(new string[]
					{
						mResources.hit_point,
						" ",
						mResources.root,
						": ",
						NinjaUtil.getMoneys((long)global::Char.myCharz().cDamGoc)
					});
					mFont.tahoma_7b_blue.drawString(g, st3, num2 + 5, num3 + 3, 0);
					mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
					{
						NinjaUtil.getMoneys((long)(global::Char.myCharz().cDamGoc * 100)),
						" ",
						mResources.potential,
						": ",
						mResources.increase,
						" ",
						global::Char.myCharz().damFrom1000TiemNang
					}), num2 + 5, num3 + 15, 0);
				}
				bool flag6 = i == 3;
				if (flag6)
				{
					SmallImage.drawSmallImage(g, 721, num5 + 4, num6 + 4, 0, 0);
					string st4 = string.Concat(new string[]
					{
						mResources.armor,
						" ",
						mResources.root,
						": ",
						NinjaUtil.getMoneys((long)global::Char.myCharz().cDefGoc)
					});
					mFont.tahoma_7b_blue.drawString(g, st4, num2 + 5, num3 + 3, 0);
					mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
					{
						NinjaUtil.getMoneys((long)(500000 + global::Char.myCharz().cDefGoc * 100000)),
						" ",
						mResources.potential,
						": ",
						mResources.increase,
						" ",
						global::Char.myCharz().defFrom1000TiemNang
					}), num2 + 5, num3 + 15, 0);
				}
				bool flag7 = i == 4;
				if (flag7)
				{
					SmallImage.drawSmallImage(g, 719, num5 + 4, num6 + 4, 0, 0);
					string st5 = string.Concat(new object[]
					{
						mResources.critical,
						" ",
						mResources.root,
						": ",
						global::Char.myCharz().cCriticalGoc,
						"%"
					});
					long num8 = 50000000L;
					for (int j = 0; j < global::Char.myCharz().cCriticalGoc; j++)
					{
						num8 *= 5L;
					}
					mFont.tahoma_7b_blue.drawString(g, st5, num2 + 5, num3 + 3, 0);
					long m = num8;
					mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
					{
						NinjaUtil.getMoneys(m),
						" ",
						mResources.potential,
						": ",
						mResources.increase,
						" ",
						global::Char.myCharz().criticalFrom1000Tiemnang
					}), num2 + 5, num3 + 15, 0);
				}
				bool flag8 = i == 5;
				if (flag8)
				{
					bool flag9 = Panel.specialInfo != null;
					if (flag9)
					{
						SmallImage.drawSmallImage(g, (int)Panel.spearcialImage, num5 + 4, num6 + 4, 0, 0);
						string[] array = mFont.tahoma_7.splitFontArray(Panel.specialInfo, 120);
						for (int k = 0; k < array.Length; k++)
						{
							mFont.tahoma_7_green2.drawString(g, array[k], num2 + 5, num3 + 3 + k * 12, 0);
						}
					}
					else
					{
						mFont.tahoma_7_green2.drawString(g, string.Empty, num2 + 5, num3 + 9, 0);
					}
				}
				bool flag10 = i < 6;
				if (!flag10)
				{
					int num9 = i - 6;
					SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[num9];
					SmallImage.drawSmallImage(g, skillTemplate.iconId, num5 + 4, num6 + 4, 0, 0);
					Skill skill = global::Char.myCharz().getSkill(skillTemplate);
					bool flag11 = skill != null;
					if (flag11)
					{
						mFont.tahoma_7b_blue.drawString(g, skillTemplate.name, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_blue.drawString(g, mResources.level + ": " + skill.point, num2 + num4 - 5, num3 + 3, mFont.RIGHT);
						bool flag12 = skill.point == skillTemplate.maxPoint;
						if (flag12)
						{
							mFont.tahoma_7_green2.drawString(g, mResources.max_level_reach, num2 + 5, num3 + 15, 0);
						}
						else
						{
							Skill skill2 = skillTemplate.skills[skill.point];
							mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
							{
								mResources.level,
								" ",
								skill.point + 1,
								" ",
								mResources.need,
								" ",
								Res.formatNumber2(skill2.powRequire),
								" ",
								mResources.potential
							}), num2 + 5, num3 + 15, 0);
						}
					}
					else
					{
						Skill skill3 = skillTemplate.skills[0];
						mFont.tahoma_7b_green.drawString(g, skillTemplate.name, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
						{
							mResources.need_upper,
							" ",
							Res.formatNumber2(skill3.powRequire),
							" ",
							mResources.potential_to_learn
						}), num2 + 5, num3 + 15, 0);
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x00072340 File Offset: 0x00070540
	private void paintMapTrans(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < this.mapNames.Length; i++)
		{
			int num = this.xScroll + 36;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 36;
			int h = this.ITEM_HEIGHT - 1;
			int num4 = this.xScroll;
			int num5 = this.yScroll + i * this.ITEM_HEIGHT;
			int num6 = this.ITEM_HEIGHT - 1;
			bool flag = num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
			if (flag)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(this.xScroll, num2, this.wScroll, h);
				mFont.tahoma_7b_blue.drawString(g, this.mapNames[i], 5, num2 + 1, 0);
				mFont.tahoma_7_grey.drawString(g, this.planetNames[i], 5, num2 + 11, 0);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x000724A8 File Offset: 0x000706A8
	private void paintZone(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		int[] zones = GameScr.gI().zones;
		int[] pts = GameScr.gI().pts;
		for (int i = 0; i < pts.Length; i++)
		{
			int num = this.xScroll + 36;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 36;
			int h = this.ITEM_HEIGHT - 1;
			int num4 = this.xScroll;
			int y = this.yScroll + i * this.ITEM_HEIGHT;
			int num5 = 34;
			int h2 = this.ITEM_HEIGHT - 1;
			bool flag = num2 - this.cmy > this.yScroll + this.hScroll || num2 - this.cmy < this.yScroll - this.ITEM_HEIGHT;
			if (!flag)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num, num2, num3, h);
				g.setColor(this.zoneColor[pts[i]]);
				g.fillRect(num4, y, num5, h2);
				bool flag2 = zones[i] != -1;
				if (flag2)
				{
					bool flag3 = pts[i] != 1;
					if (flag3)
					{
						mFont.tahoma_7_yellow.drawString(g, zones[i] + string.Empty, num4 + num5 / 2, num2 + 6, mFont.CENTER);
					}
					else
					{
						mFont.tahoma_7_grey.drawString(g, zones[i] + string.Empty, num4 + num5 / 2, num2 + 6, mFont.CENTER);
					}
					mFont.tahoma_7_green2.drawString(g, GameScr.gI().numPlayer[i] + "/" + GameScr.gI().maxPlayer[i], num + 5, num2 + 6, 0);
				}
				bool flag4 = GameScr.gI().rankName1[i] != null;
				if (flag4)
				{
					mFont.tahoma_7_grey.drawString(g, string.Concat(new object[]
					{
						GameScr.gI().rankName1[i],
						"(Top ",
						GameScr.gI().rank1[i],
						")"
					}), num + num3 - 2, num2 + 1, mFont.RIGHT);
					mFont.tahoma_7_grey.drawString(g, string.Concat(new object[]
					{
						GameScr.gI().rankName2[i],
						"(Top ",
						GameScr.gI().rank2[i],
						")"
					}), num + num3 - 2, num2 + 11, mFont.RIGHT);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x0007279C File Offset: 0x0007099C
	private void paintSpeacialSkill(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		bool flag = this.currentListLength == 0;
		if (!flag)
		{
			int num = (this.cmy + this.hScroll) / 24 + 1;
			bool flag2 = num < this.hScroll / 24 + 1;
			if (flag2)
			{
				num = this.hScroll / 24 + 1;
			}
			bool flag3 = num > this.currentListLength;
			if (flag3)
			{
				num = this.currentListLength;
			}
			int num2 = this.cmy / 24;
			bool flag4 = num2 >= num;
			if (flag4)
			{
				num2 = num - 1;
			}
			bool flag5 = num2 < 0;
			if (flag5)
			{
				num2 = 0;
			}
			for (int i = num2; i < num; i++)
			{
				int num3 = this.xScroll;
				int num4 = this.yScroll + i * this.ITEM_HEIGHT;
				int num5 = 24;
				int num6 = this.ITEM_HEIGHT - 1;
				int num7 = this.xScroll + num5;
				int num8 = this.yScroll + i * this.ITEM_HEIGHT;
				int num9 = this.wScroll - num5;
				int h = this.ITEM_HEIGHT - 1;
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num7, num8, num9, h);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num3, num4, num5, num6);
				SmallImage.drawSmallImage(g, (int)global::Char.myCharz().imgSpeacialSkill[this.currentTabIndex][i], num3 + num5 / 2, num4 + num6 / 2, 0, 3);
				string[] array = mFont.tahoma_7_grey.splitFontArray(global::Char.myCharz().infoSpeacialSkill[this.currentTabIndex][i], 140);
				for (int j = 0; j < array.Length; j++)
				{
					mFont.tahoma_7_grey.drawString(g, array[j], num7 + 5, num8 + 1 + j * 11, 0);
				}
			}
			this.paintScrollArrow(g);
		}
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x000729CC File Offset: 0x00070BCC
	private void paintBox(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		try
		{
			Item[] arrItemBox = global::Char.myCharz().arrItemBox;
			this.currentListLength = this.checkCurrentListLength(arrItemBox.Length);
			int num = arrItemBox.Length / 20 + ((arrItemBox.Length % 20 > 0) ? 1 : 0);
			this.TAB_W_NEW = this.wScroll / num;
			for (int i = 0; i < this.currentListLength; i++)
			{
				int num2 = this.xScroll + 36;
				int num3 = this.yScroll + i * this.ITEM_HEIGHT;
				int num4 = this.wScroll - 36;
				int h = this.ITEM_HEIGHT - 1;
				int num5 = this.xScroll;
				int num6 = this.yScroll + i * this.ITEM_HEIGHT;
				int num7 = 34;
				int num8 = this.ITEM_HEIGHT - 1;
				bool flag = num3 - this.cmy > this.yScroll + this.hScroll || num3 - this.cmy < this.yScroll - this.ITEM_HEIGHT;
				if (!flag)
				{
					bool flag2 = i == 0;
					if (flag2)
					{
						for (int j = 0; j < num; j++)
						{
							int num9 = (j == this.newSelected && this.selected == 0) ? ((GameCanvas.gameTick % 10 < 7) ? -1 : 0) : 0;
							g.setColor((j != this.newSelected) ? 15723751 : 16383818);
							g.fillRect(this.xScroll + j * this.TAB_W_NEW, num3 + 9 + num9, this.TAB_W_NEW - 1, 14);
							mFont.tahoma_7_grey.drawString(g, string.Empty + j, this.xScroll + j * this.TAB_W_NEW + this.TAB_W_NEW / 2, this.yScroll + 11 + num9, mFont.CENTER);
						}
					}
					else
					{
						g.setColor((i != this.selected) ? 15196114 : 16383818);
						g.fillRect(num2, num3, num4, h);
						g.setColor((i != this.selected) ? 9993045 : 9541120);
						int inventorySelect_body = this.GetInventorySelect_body(i, this.newSelected);
						Item item = arrItemBox[inventorySelect_body];
						bool flag3 = item != null;
						if (flag3)
						{
							for (int k = 0; k < item.itemOption.Length; k++)
							{
								bool flag4 = item.itemOption[k].optionTemplate.id == 72 && item.itemOption[k].param > 0;
								if (flag4)
								{
									sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[k].param);
									int color_ItemBg = Panel.GetColor_ItemBg((int)color_Item_Upgrade);
									bool flag5 = color_ItemBg != -1;
									if (flag5)
									{
										g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
									}
								}
							}
						}
						g.fillRect(num5, num6, num7, num8);
						bool flag6 = item == null;
						if (!flag6)
						{
							string text = string.Empty;
							mFont mFont = mFont.tahoma_7_green2;
							bool flag7 = item.itemOption != null;
							if (flag7)
							{
								for (int l = 0; l < item.itemOption.Length; l++)
								{
									bool flag8 = item.itemOption[l].optionTemplate.id == 72;
									if (flag8)
									{
										text = " [+" + item.itemOption[l].getOptionString() + "]";
									}
									bool flag9 = item.itemOption[l].optionTemplate.id == 41;
									if (flag9)
									{
										bool flag10 = item.itemOption[l].param == 1;
										if (flag10)
										{
											mFont = Panel.GetFont(0);
										}
										else
										{
											bool flag11 = item.itemOption[l].param == 2;
											if (flag11)
											{
												mFont = Panel.GetFont(2);
											}
											else
											{
												bool flag12 = item.itemOption[l].param == 3;
												if (flag12)
												{
													mFont = Panel.GetFont(8);
												}
												else
												{
													bool flag13 = item.itemOption[l].param == 4;
													if (flag13)
													{
														mFont = Panel.GetFont(7);
													}
												}
											}
										}
									}
								}
							}
							mFont.drawString(g, string.Concat(new object[]
							{
								"[",
								item.template.id,
								"] ",
								item.template.name,
								text
							}), num2 + 5, num3 + 1, 0);
							mFont.tahoma_7b_red.drawString(g, VuDang.saoTrongBalo(item), num4 + 15, num6 + 1, 0);
							string text2 = string.Empty;
							bool flag14 = item.itemOption != null;
							if (flag14)
							{
								bool flag15 = item.itemOption.Length != 0 && item.itemOption[0] != null;
								if (flag15)
								{
									text2 += item.itemOption[0].getOptionString();
								}
								mFont mFont2 = mFont.tahoma_7_blue;
								bool flag16 = item.compare < 0 && item.template.type != 5;
								if (flag16)
								{
									mFont2 = mFont.tahoma_7_red;
								}
								bool flag17 = item.itemOption.Length > 1;
								if (flag17)
								{
									for (int m = 1; m < item.itemOption.Length; m++)
									{
										bool flag18 = item.itemOption[m] != null && item.itemOption[m].optionTemplate.id != 102 && item.itemOption[m].optionTemplate.id != 107;
										if (flag18)
										{
											text2 = text2 + "," + item.itemOption[m].getOptionString();
										}
									}
								}
								mFont2.drawString(g, text2, num2 + 5, num3 + 11, mFont.LEFT);
							}
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
							bool flag19 = item.itemOption != null;
							if (flag19)
							{
								for (int n = 0; n < item.itemOption.Length; n++)
								{
									this.paintOptItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8);
								}
								for (int num10 = 0; num10 < item.itemOption.Length; num10++)
								{
									this.paintOptSlotItem(g, item.itemOption[num10].optionTemplate.id, item.itemOption[num10].param, num5, num6, num7, num8);
								}
							}
							bool flag20 = item.quantity > 1;
							if (flag20)
							{
								mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
							}
						}
					}
				}
			}
		}
		catch (Exception)
		{
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x00073164 File Offset: 0x00071364
	public Member getCurrMember()
	{
		bool flag = this.selected < 2;
		Member result;
		if (flag)
		{
			result = null;
		}
		else
		{
			bool flag2 = this.selected > ((this.member == null) ? this.myMember.size() : this.member.size()) + 1;
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = ((this.member == null) ? ((Member)this.myMember.elementAt(this.selected - 2)) : ((Member)this.member.elementAt(this.selected - 2)));
			}
		}
		return result;
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x000731F8 File Offset: 0x000713F8
	public ClanMessage getCurrMessage()
	{
		bool flag = this.selected < 2;
		ClanMessage result;
		if (flag)
		{
			result = null;
		}
		else
		{
			bool flag2 = this.selected > ClanMessage.vMessage.size() + 1;
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = (ClanMessage)ClanMessage.vMessage.elementAt(this.selected - 2);
			}
		}
		return result;
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x00073250 File Offset: 0x00071450
	public Clan getCurrClan()
	{
		bool flag = this.selected < 2;
		Clan result;
		if (flag)
		{
			result = null;
		}
		else
		{
			bool flag2 = this.selected > this.clans.Length + 1;
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = this.clans[this.selected - 2];
			}
		}
		return result;
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x000732A0 File Offset: 0x000714A0
	private void paintLogChat(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		bool flag = this.logChat.size() == 0;
		if (flag)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_msg, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2 + 24, 2);
		}
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = 24;
			int h = this.ITEM_HEIGHT - 1;
			int num4 = this.xScroll + num3;
			int num5 = this.yScroll + i * this.ITEM_HEIGHT;
			int num6 = this.wScroll - num3;
			int num7 = this.ITEM_HEIGHT - 1;
			bool flag2 = i == 0;
			if (flag2)
			{
				g.setColor(15196114);
				g.fillRect(num, num5, this.wScroll, num7);
				g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num5 + 2, StaticObj.TOP_RIGHT);
				((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, (!this.isViewChatServer) ? mResources.on : mResources.off, this.xScroll + this.wScroll - 22, num5 + 7, 2);
				mFont.tahoma_7_grey.drawString(g, (!this.isViewChatServer) ? mResources.onPlease : mResources.offPlease, this.xScroll + 5, num5 + num7 / 2 - 4, mFont.LEFT);
			}
			else
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num4, num5, num6, num7);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num, num2, num3, h);
				InfoItem infoItem = (InfoItem)this.logChat.elementAt(i - 1);
				bool flag3 = infoItem.charInfo.headICON != -1;
				if (flag3)
				{
					SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
				}
				else
				{
					Part part = GameScr.parts[infoItem.charInfo.head];
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
				}
				g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
				mFont mFont = mFont.tahoma_7b_dark;
				mFont = mFont.tahoma_7b_green2;
				mFont.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
				bool flag4 = !infoItem.isChatServer;
				if (flag4)
				{
					mFont.tahoma_7_blue.drawString(g, Res.split(infoItem.s, "|", 0)[2], num4 + 5, num5 + 11, 0);
				}
				else
				{
					mFont.tahoma_7_red.drawString(g, Res.split(infoItem.s, "|", 0)[2], num4 + 5, num5 + 11, 0);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x00073658 File Offset: 0x00071858
	private void paintFlagChange(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll + 26;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 26;
			int h = this.ITEM_HEIGHT - 1;
			int num4 = this.xScroll;
			int num5 = this.yScroll + i * this.ITEM_HEIGHT;
			int num6 = 24;
			int num7 = this.ITEM_HEIGHT - 1;
			bool flag = num2 - this.cmy > this.yScroll + this.hScroll || num2 - this.cmy < this.yScroll - this.ITEM_HEIGHT;
			if (!flag)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num, num2, num3, h);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num4, num5, num6, num7);
				Item item = (Item)this.vFlag.elementAt(i);
				bool flag2 = item == null;
				if (!flag2)
				{
					mFont.tahoma_7_green2.drawString(g, string.Concat(new object[]
					{
						"[",
						item.template.id,
						"] ",
						item.template.name
					}), num + 5, num2 + 1, 0);
					string text = string.Empty;
					bool flag3 = item.itemOption != null && item.itemOption.Length >= 1;
					if (flag3)
					{
						bool flag4 = item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107;
						if (flag4)
						{
							text += item.itemOption[0].getOptionString();
						}
						mFont tahoma_7_blue = mFont.tahoma_7_blue;
						tahoma_7_blue.drawString(g, text, num + 5, num2 + 11, 0);
						SmallImage.drawSmallImage(g, (int)item.template.iconID, num4 + num6 / 2, num5 + num7 / 2, 0, 3);
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x000738E8 File Offset: 0x00071AE8
	private void paintEnemy(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		bool flag = this.currentListLength == 0;
		if (flag)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_enemy, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
		}
		else
		{
			for (int i = 0; i < this.currentListLength; i++)
			{
				int num = this.xScroll;
				int num2 = this.yScroll + i * this.ITEM_HEIGHT;
				int num3 = 24;
				int h = this.ITEM_HEIGHT - 1;
				int num4 = this.xScroll + num3;
				int num5 = this.yScroll + i * this.ITEM_HEIGHT;
				int num6 = this.wScroll - num3;
				int h2 = this.ITEM_HEIGHT - 1;
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num4, num5, num6, h2);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num, num2, num3, h);
				InfoItem infoItem = (InfoItem)this.vEnemy.elementAt(i);
				bool flag2 = infoItem.charInfo.headICON != -1;
				if (flag2)
				{
					SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
				}
				else
				{
					Part part = GameScr.parts[infoItem.charInfo.head];
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 + 3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
				}
				g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
				bool isOnline = infoItem.isOnline;
				if (isOnline)
				{
					mFont.tahoma_7b_green.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
					mFont.tahoma_7_blue.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
				}
				else
				{
					mFont.tahoma_7_grey.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
					mFont.tahoma_7_grey.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
				}
			}
			this.paintScrollArrow(g);
		}
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x00073BB4 File Offset: 0x00071DB4
	private void paintFriend(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		bool flag = this.currentListLength == 0;
		if (flag)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_friend, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
		}
		else
		{
			for (int i = 0; i < this.currentListLength; i++)
			{
				int num = this.xScroll;
				int num2 = this.yScroll + i * this.ITEM_HEIGHT;
				int num3 = 24;
				int h = this.ITEM_HEIGHT - 1;
				int num4 = this.xScroll + num3;
				int num5 = this.yScroll + i * this.ITEM_HEIGHT;
				int num6 = this.wScroll - num3;
				int h2 = this.ITEM_HEIGHT - 1;
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num4, num5, num6, h2);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num, num2, num3, h);
				InfoItem infoItem = (InfoItem)this.vFriend.elementAt(i);
				bool flag2 = infoItem.charInfo.headICON != -1;
				if (flag2)
				{
					SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
				}
				else
				{
					Part part = GameScr.parts[infoItem.charInfo.head];
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 + 3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
				}
				g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
				bool isOnline = infoItem.isOnline;
				if (isOnline)
				{
					mFont.tahoma_7b_green.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
					mFont.tahoma_7_blue.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
				}
				else
				{
					mFont.tahoma_7_grey.drawString(g, infoItem.charInfo.cName, num4 + 5, num5, 0);
					mFont.tahoma_7_grey.drawString(g, infoItem.s, num4 + 5, num5 + 11, 0);
				}
			}
			this.paintScrollArrow(g);
		}
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x00073E80 File Offset: 0x00072080
	public void paintPlayerMenu(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		int i = 0;
		while (i < this.vPlayerMenu.size())
		{
			int x = this.xScroll;
			int num = this.yScroll + i * this.ITEM_HEIGHT;
			int num2 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			bool flag = num - this.cmy <= this.yScroll + this.hScroll && num - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
			if (flag)
			{
				Command command = (Command)this.vPlayerMenu.elementAt(i);
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(x, num, num2, h);
				bool flag2 = command.caption2.Equals(string.Empty);
				if (flag2)
				{
					mFont.tahoma_7b_dark.drawString(g, command.caption, this.xScroll + this.wScroll / 2, num + 6, mFont.CENTER);
				}
				else
				{
					mFont.tahoma_7b_dark.drawString(g, command.caption, this.xScroll + this.wScroll / 2, num + 1, mFont.CENTER);
					mFont.tahoma_7b_dark.drawString(g, command.caption2, this.xScroll + this.wScroll / 2, num + 11, mFont.CENTER);
				}
			}
			IL_175:
			i++;
			continue;
			goto IL_175;
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x00074028 File Offset: 0x00072228
	private void paintClans(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(-this.cmx, -this.cmy);
		g.setColor(0);
		int num = this.xScroll + this.wScroll / 2 - this.clansOption.Length * this.TAB_W / 2;
		bool flag = this.currentListLength == 2;
		if (flag)
		{
			mFont.tahoma_7_green2.drawString(g, this.clanReport, this.xScroll + this.wScroll / 2, this.yScroll + 24 + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
			bool flag2 = this.isMessage && this.myMember.size() == 1;
			if (flag2)
			{
				for (int i = 0; i < mResources.clanEmpty.Length; i++)
				{
					mFont.tahoma_7b_dark.drawString(g, mResources.clanEmpty[i], this.xScroll + this.wScroll / 2, this.yScroll + 24 + this.hScroll / 2 - mResources.clanEmpty.Length * 12 / 2 + i * 12, mFont.CENTER);
				}
			}
		}
		for (int j = 0; j < this.currentListLength; j++)
		{
			int num2 = this.xScroll;
			int num3 = this.yScroll + j * this.ITEM_HEIGHT;
			int num4 = 24;
			int num5 = this.ITEM_HEIGHT - 1;
			int num6 = this.xScroll + num4;
			int num7 = this.yScroll + j * this.ITEM_HEIGHT;
			int num8 = this.wScroll - num4;
			int num9 = this.ITEM_HEIGHT - 1;
			bool flag3 = num7 - this.cmy > this.yScroll + this.hScroll || num7 - this.cmy < this.yScroll - this.ITEM_HEIGHT;
			if (!flag3)
			{
				int num10 = j;
				if (num10 != 0)
				{
					if (num10 != 1)
					{
						bool flag4 = this.isSearchClan;
						if (flag4)
						{
							bool flag5 = this.clans == null || this.clans.Length == 0;
							if (!flag5)
							{
								g.setColor((j != this.selected) ? 15196114 : 16383818);
								g.fillRect(num6, num7, num8, num9);
								g.setColor((j != this.selected) ? 9993045 : 9541120);
								g.fillRect(num2, num3, num4, num5);
								bool flag6 = ClanImage.isExistClanImage(this.clans[j - 2].imgID);
								if (flag6)
								{
									bool flag7 = ClanImage.getClanImage((sbyte)this.clans[j - 2].imgID).idImage != null;
									if (flag7)
									{
										SmallImage.drawSmallImage(g, (int)ClanImage.getClanImage((sbyte)this.clans[j - 2].imgID).idImage[0], num2 + num4 / 2, num3 + num5 / 2, 0, StaticObj.VCENTER_HCENTER);
									}
								}
								else
								{
									ClanImage clanImage = new ClanImage();
									clanImage.ID = this.clans[j - 2].imgID;
									bool flag8 = !ClanImage.isExistClanImage(clanImage.ID);
									if (flag8)
									{
										ClanImage.addClanImage(clanImage);
									}
								}
								mFont.tahoma_7b_green2.drawString(g, this.clans[j - 2].name, num6 + 5, num7, 0);
								g.setClip(num6, num7, num8 - 10, num9);
								mFont.tahoma_7_blue.drawString(g, this.clans[j - 2].slogan, num6 + 5, num7 + 11, 0);
								g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
								mFont.tahoma_7_green2.drawString(g, this.clans[j - 2].currMember + "/" + this.clans[j - 2].maxMember, num6 + num8 - 5, num7, mFont.RIGHT);
							}
						}
						else
						{
							bool flag9 = this.isViewMember;
							if (flag9)
							{
								g.setColor((j != this.selected) ? 15196114 : 16383818);
								g.fillRect(num6, num7, num8, num9);
								g.setColor((j != this.selected) ? 9993045 : 9541120);
								g.fillRect(num2, num3, num4, num5);
								Member member = (this.member == null) ? ((Member)this.myMember.elementAt(j - 2)) : ((Member)this.member.elementAt(j - 2));
								bool flag10 = member.headICON != -1;
								if (flag10)
								{
									SmallImage.drawSmallImage(g, (int)member.headICON, num2, num3, 0, 0);
								}
								else
								{
									Part part = GameScr.parts[(int)member.head];
									SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num2 + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num3 + 3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
								}
								g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
								mFont mFont = mFont.tahoma_7b_dark;
								bool flag11 = member.role == 0;
								if (flag11)
								{
									mFont = mFont.tahoma_7b_red;
								}
								else
								{
									bool flag12 = member.role == 1;
									if (flag12)
									{
										mFont = mFont.tahoma_7b_green;
									}
									else
									{
										bool flag13 = member.role == 2;
										if (flag13)
										{
											mFont = mFont.tahoma_7b_green2;
										}
									}
								}
								mFont.drawString(g, member.name, num6 + 5, num7, 0);
								mFont.tahoma_7_blue.drawString(g, mResources.power + ": " + member.powerPoint, num6 + 5, num7 + 11, 0);
								SmallImage.drawSmallImage(g, 7223, num6 + num8 - 7, num7 + 12, 0, 3);
								mFont.tahoma_7_blue.drawString(g, string.Empty + member.clanPoint, num6 + num8 - 15, num7 + 6, mFont.RIGHT);
							}
							else
							{
								bool flag14 = !this.isMessage || ClanMessage.vMessage.size() == 0;
								if (!flag14)
								{
									ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(j - 2);
									g.setColor((j != this.selected || clanMessage.option != null) ? 15196114 : 16383818);
									g.fillRect(num2, num3, num8 + num4, num9);
									clanMessage.paint(g, num2, num3);
									bool flag15 = clanMessage.option == null;
									if (!flag15)
									{
										int num11 = this.xScroll + this.wScroll - 2 - clanMessage.option.Length * 40;
										for (int k = 0; k < clanMessage.option.Length; k++)
										{
											bool flag16 = k == this.cSelected && j == this.selected;
											if (flag16)
											{
												g.drawImage(GameScr.imgLbtnFocus2, num11 + k * 40 + 20, num7 + num9 / 2, StaticObj.VCENTER_HCENTER);
												mFont.tahoma_7b_green2.drawString(g, clanMessage.option[k], num11 + k * 40 + 20, num7 + 6, mFont.CENTER);
											}
											else
											{
												g.drawImage(GameScr.imgLbtn2, num11 + k * 40 + 20, num7 + num9 / 2, StaticObj.VCENTER_HCENTER);
												mFont.tahoma_7b_dark.drawString(g, clanMessage.option[k], num11 + k * 40 + 20, num7 + 6, mFont.CENTER);
											}
										}
									}
								}
							}
						}
					}
					else
					{
						g.setColor((j != this.selected) ? 15196114 : 16383818);
						g.fillRect(this.xScroll, num7, this.wScroll, num9);
						bool flag17 = this.clanInfo != null;
						if (flag17)
						{
							mFont.tahoma_7b_dark.drawString(g, this.clanInfo, this.xScroll + this.wScroll / 2, num7 + 6, mFont.CENTER);
						}
					}
				}
				else
				{
					for (int l = 0; l < this.clansOption.Length; l++)
					{
						g.setColor((l != this.cSelected || j != this.selected) ? 15723751 : 16383818);
						g.fillRect(num + l * this.TAB_W, num7, this.TAB_W - 1, 23);
						for (int m = 0; m < this.clansOption[l].Length; m++)
						{
							mFont.tahoma_7_grey.drawString(g, this.clansOption[l][m], num + l * this.TAB_W + this.TAB_W / 2, this.yScroll + m * 11, mFont.CENTER);
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x0007497C File Offset: 0x00072B7C
	private void paintArchivement(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		bool flag = this.currentListLength == 0;
		if (flag)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_mission, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
		}
		else
		{
			bool flag2 = global::Char.myCharz().arrArchive == null || global::Char.myCharz().arrArchive.Length != this.currentListLength;
			if (!flag2)
			{
				for (int i = 0; i < this.currentListLength; i++)
				{
					int num = this.xScroll;
					int num2 = this.yScroll + i * this.ITEM_HEIGHT;
					int num3 = this.wScroll;
					int num4 = this.ITEM_HEIGHT - 1;
					Archivement archivement = global::Char.myCharz().arrArchive[i];
					g.setColor((i != this.selected || ((archivement.isRecieve || archivement.isFinish) && (!archivement.isRecieve || !archivement.isFinish))) ? 15196114 : 16383818);
					g.fillRect(num, num2, num3, num4);
					bool flag3 = archivement == null;
					if (!flag3)
					{
						bool flag4 = !archivement.isFinish;
						if (flag4)
						{
							mFont.tahoma_7.drawString(g, archivement.info1, num + 5, num2, 0);
							mFont.tahoma_7_green.drawString(g, archivement.money + " " + mResources.RUBY, num + num3 - 5, num2, mFont.RIGHT);
							mFont.tahoma_7_red.drawString(g, archivement.info2, num + 5, num2 + 11, 0);
						}
						else
						{
							bool flag5 = archivement.isFinish && !archivement.isRecieve;
							if (flag5)
							{
								mFont.tahoma_7.drawString(g, archivement.info1, num + 5, num2, 0);
								mFont.tahoma_7_blue.drawString(g, string.Concat(new object[]
								{
									mResources.reward_mission,
									archivement.money,
									" ",
									mResources.RUBY
								}), num + 5, num2 + 11, 0);
								bool flag6 = i == this.selected;
								if (flag6)
								{
									mFont.tahoma_7b_green2.drawString(g, mResources.receive_upper, num + num3 - 20, num2 + 6, mFont.CENTER);
									mFont.tahoma_7b_dark.drawString(g, mResources.receive_upper, num + num3 - 20, num2 + 6, mFont.CENTER);
								}
								else
								{
									g.drawImage(GameScr.imgLbtn2, num + num3 - 20, num2 + num4 / 2, StaticObj.VCENTER_HCENTER);
									mFont.tahoma_7b_dark.drawString(g, mResources.receive_upper, num + num3 - 20, num2 + 6, mFont.CENTER);
								}
							}
							else
							{
								bool flag7 = archivement.isFinish && archivement.isRecieve;
								if (flag7)
								{
									mFont.tahoma_7_green.drawString(g, archivement.info1, num + 5, num2, 0);
									mFont.tahoma_7_green.drawString(g, archivement.info2, num + 5, num2 + 11, 0);
								}
							}
						}
					}
				}
				this.paintScrollArrow(g);
			}
		}
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x00074CEC File Offset: 0x00072EEC
	private void paintCombine(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		bool flag = this.vItemCombine.size() == 0;
		if (flag)
		{
			bool flag2 = this.combineInfo != null;
			if (flag2)
			{
				for (int i = 0; i < this.combineInfo.Length; i++)
				{
					mFont.tahoma_7b_dark.drawString(g, this.combineInfo[i], this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - this.combineInfo.Length * 14 / 2 + i * 14 + 5, 2);
				}
			}
		}
		else
		{
			for (int j = 0; j < this.vItemCombine.size() + 1; j++)
			{
				int num = this.xScroll + 36;
				int num2 = this.yScroll + j * this.ITEM_HEIGHT;
				int num3 = this.wScroll - 36;
				int num4 = this.ITEM_HEIGHT - 1;
				int num5 = this.xScroll;
				int num6 = this.yScroll + j * this.ITEM_HEIGHT;
				int num7 = 34;
				int num8 = this.ITEM_HEIGHT - 1;
				bool flag3 = num2 - this.cmy > this.yScroll + this.hScroll || num2 - this.cmy < this.yScroll - this.ITEM_HEIGHT;
				if (!flag3)
				{
					bool flag4 = j == this.vItemCombine.size();
					if (flag4)
					{
						bool flag5 = this.vItemCombine.size() > 0;
						if (flag5)
						{
							bool flag6 = !GameCanvas.isTouch && j == this.selected;
							if (flag6)
							{
								g.setColor(16383818);
								g.fillRect(num5, num2, this.wScroll, num4 + 2);
							}
							bool flag7 = (j == this.selected && this.keyTouchCombine == 1) || (!GameCanvas.isTouch && j == this.selected);
							if (flag7)
							{
								g.drawImage(GameScr.imgLbtnFocus, this.xScroll + this.wScroll / 2, num2 + num4 / 2 + 1, StaticObj.VCENTER_HCENTER);
								mFont.tahoma_7b_green2.drawString(g, mResources.UPGRADE, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
							}
							else
							{
								g.drawImage(GameScr.imgLbtn, this.xScroll + this.wScroll / 2, num2 + num4 / 2 + 1, StaticObj.VCENTER_HCENTER);
								mFont.tahoma_7b_dark.drawString(g, mResources.UPGRADE, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
							}
						}
					}
					else
					{
						g.setColor((j != this.selected) ? 15196114 : 16383818);
						g.fillRect(num, num2, num3, num4);
						g.setColor((j != this.selected) ? 9993045 : 9541120);
						Item item = (Item)this.vItemCombine.elementAt(j);
						bool flag8 = item != null;
						if (flag8)
						{
							for (int k = 0; k < item.itemOption.Length; k++)
							{
								bool flag9 = item.itemOption[k].optionTemplate.id == 72 && item.itemOption[k].param > 0;
								if (flag9)
								{
									sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[k].param);
									int color_ItemBg = Panel.GetColor_ItemBg((int)color_Item_Upgrade);
									bool flag10 = color_ItemBg != -1;
									if (flag10)
									{
										g.setColor((j != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
									}
								}
							}
						}
						g.fillRect(num5, num6, num7, num8);
						bool flag11 = item == null;
						if (!flag11)
						{
							string text = string.Empty;
							mFont mFont = mFont.tahoma_7_green2;
							bool flag12 = item.itemOption != null;
							if (flag12)
							{
								for (int l = 0; l < item.itemOption.Length; l++)
								{
									bool flag13 = item.itemOption[l].optionTemplate.id == 72;
									if (flag13)
									{
										text = " [+" + item.itemOption[l].param + "]";
									}
									bool flag14 = item.itemOption[l].optionTemplate.id == 41;
									if (flag14)
									{
										bool flag15 = item.itemOption[l].param == 1;
										if (flag15)
										{
											mFont = Panel.GetFont(0);
										}
										else
										{
											bool flag16 = item.itemOption[l].param == 2;
											if (flag16)
											{
												mFont = Panel.GetFont(2);
											}
											else
											{
												bool flag17 = item.itemOption[l].param == 3;
												if (flag17)
												{
													mFont = Panel.GetFont(8);
												}
												else
												{
													bool flag18 = item.itemOption[l].param == 4;
													if (flag18)
													{
														mFont = Panel.GetFont(7);
													}
												}
											}
										}
									}
								}
							}
							mFont.drawString(g, string.Concat(new object[]
							{
								"[",
								item.template.id,
								"] ",
								item.template.name,
								text
							}), num + 5, num2 + 1, 0);
							string text2 = string.Empty;
							bool flag19 = item.itemOption != null;
							if (flag19)
							{
								bool flag20 = item.itemOption.Length != 0 && item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107;
								if (flag20)
								{
									text2 += item.itemOption[0].getOptionString();
								}
								mFont mFont2 = mFont.tahoma_7_blue;
								bool flag21 = item.compare < 0 && item.template.type != 5;
								if (flag21)
								{
									mFont2 = mFont.tahoma_7_red;
								}
								bool flag22 = item.itemOption.Length > 1;
								if (flag22)
								{
									for (int m = 1; m < item.itemOption.Length; m++)
									{
										bool flag23 = item.itemOption[m] != null && item.itemOption[m].optionTemplate.id != 102 && item.itemOption[m].optionTemplate.id != 107;
										if (flag23)
										{
											text2 = text2 + "," + item.itemOption[m].getOptionString();
										}
									}
								}
								mFont2.drawString(g, text2, num + 5, num2 + 11, mFont.LEFT);
							}
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
							bool flag24 = item.itemOption != null;
							if (flag24)
							{
								for (int n = 0; n < item.itemOption.Length; n++)
								{
									this.paintOptItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8);
								}
								for (int num9 = 0; num9 < item.itemOption.Length; num9++)
								{
									this.paintOptSlotItem(g, item.itemOption[num9].optionTemplate.id, item.itemOption[num9].param, num5, num6, num7, num8);
								}
							}
							bool flag25 = item.quantity > 1;
							if (flag25)
							{
								mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
							}
						}
					}
				}
			}
			this.paintScrollArrow(g);
		}
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x00075548 File Offset: 0x00073748
	private void paintInventory(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		try
		{
			Item[] arrItemBody = global::Char.myCharz().arrItemBody;
			Item[] arrItemBag = global::Char.myCharz().arrItemBag;
			this.currentListLength = this.checkCurrentListLength(arrItemBody.Length + arrItemBag.Length);
			int num = (arrItemBody.Length + arrItemBag.Length) / 20 + (((arrItemBody.Length + arrItemBag.Length) % 20 > 0) ? 1 : 0);
			this.TAB_W_NEW = this.wScroll / num;
			for (int i = 0; i < this.currentListLength; i++)
			{
				int num2 = this.xScroll + 36;
				int num3 = this.yScroll + i * this.ITEM_HEIGHT;
				int num4 = this.wScroll - 36;
				int h = this.ITEM_HEIGHT - 1;
				int num5 = this.xScroll;
				int num6 = this.yScroll + i * this.ITEM_HEIGHT;
				int num7 = 34;
				int num8 = this.ITEM_HEIGHT - 1;
				bool flag = num3 - this.cmy > this.yScroll + this.hScroll || num3 - this.cmy < this.yScroll - this.ITEM_HEIGHT;
				if (!flag)
				{
					bool flag2 = i == 0;
					if (flag2)
					{
						for (int j = 0; j < num; j++)
						{
							int num9 = (j == this.newSelected && this.selected == 0) ? ((GameCanvas.gameTick % 10 < 7) ? -1 : 0) : 0;
							g.setColor((j != this.newSelected) ? 15723751 : 16383818);
							g.fillRect(this.xScroll + j * this.TAB_W_NEW, num3 + 9 + num9, this.TAB_W_NEW - 1, 14);
							mFont.tahoma_7_grey.drawString(g, string.Empty + j, this.xScroll + j * this.TAB_W_NEW + this.TAB_W_NEW / 2, this.yScroll + 11 + num9, mFont.CENTER);
						}
					}
					else
					{
						bool inventorySelect_isbody = this.GetInventorySelect_isbody(i, this.newSelected, global::Char.myCharz().arrItemBody);
						int inventorySelect_body = this.GetInventorySelect_body(i, this.newSelected);
						int inventorySelect_bag = this.GetInventorySelect_bag(i, this.newSelected, global::Char.myCharz().arrItemBody);
						g.setColor((i == this.selected) ? 16383818 : ((!inventorySelect_isbody) ? 15723751 : 15196114));
						g.fillRect(num2, num3, num4, h);
						g.setColor((i == this.selected) ? 9541120 : ((!inventorySelect_isbody) ? 11837316 : 9993045));
						Item item = (!inventorySelect_isbody) ? arrItemBag[inventorySelect_bag] : arrItemBody[inventorySelect_body];
						bool flag3 = item != null;
						if (flag3)
						{
							for (int k = 0; k < item.itemOption.Length; k++)
							{
								bool flag4 = item.itemOption[k].optionTemplate.id == 72 && item.itemOption[k].param > 0;
								if (flag4)
								{
									sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[k].param);
									int color_ItemBg = Panel.GetColor_ItemBg((int)color_Item_Upgrade);
									bool flag5 = color_ItemBg != -1;
									if (flag5)
									{
										g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
									}
								}
							}
							foreach (VuDang.setDo1 setDo in VuDang.ListSet1)
							{
								bool flag6 = item.info == setDo.info && (int)item.template.type == setDo.type && (int)item.template.id == setDo.id && item.template.name == setDo.name && item.VuDangItemOption() == setDo.option && item.VuDangSoSao() == setDo.soSao;
								if (flag6)
								{
									g.setColor((i != this.selected) ? Panel.color1[2] : Panel.color2[2]);
								}
							}
							foreach (VuDang.setDo2 setDo2 in VuDang.ListSet2)
							{
								bool flag7 = item.info == setDo2.info && (int)item.template.type == setDo2.type && (int)item.template.id == setDo2.id && item.template.name == setDo2.name && item.VuDangItemOption() == setDo2.option && item.VuDangSoSao() == setDo2.soSao;
								if (flag7)
								{
									g.setColor((i != this.selected) ? Panel.color1[0] : Panel.color2[0]);
								}
							}
							foreach (VuDang.ItemAuto itemAuto in VuDang.listItemAuto)
							{
								bool flag8 = (int)item.template.id == itemAuto.id && (int)item.template.iconID == itemAuto.iconID;
								if (flag8)
								{
									g.setColor((i != this.selected) ? Panel.color1[1] : Panel.color2[1]);
								}
							}
						}
						g.fillRect(num5, num6, num7, num8);
						bool flag9 = item != null && item.isSelect && GameCanvas.panel.type == 12;
						if (flag9)
						{
							g.setColor((i != this.selected) ? 6047789 : 7040779);
							g.fillRect(num5, num6, num7, num8);
						}
						bool flag10 = item == null;
						if (!flag10)
						{
							string text = string.Empty;
							mFont mFont = mFont.tahoma_7_green2;
							bool flag11 = item.itemOption != null;
							if (flag11)
							{
								for (int l = 0; l < item.itemOption.Length; l++)
								{
									bool flag12 = item.itemOption[l].optionTemplate.id == 72;
									if (flag12)
									{
										text = " [+" + item.itemOption[l].param + "]";
									}
									bool flag13 = item.itemOption[l].optionTemplate.id == 41;
									if (flag13)
									{
										bool flag14 = item.itemOption[l].param == 1;
										if (flag14)
										{
											mFont = Panel.GetFont(0);
										}
										else
										{
											bool flag15 = item.itemOption[l].param == 2;
											if (flag15)
											{
												mFont = Panel.GetFont(2);
											}
											else
											{
												bool flag16 = item.itemOption[l].param == 3;
												if (flag16)
												{
													mFont = Panel.GetFont(8);
												}
												else
												{
													bool flag17 = item.itemOption[l].param == 4;
													if (flag17)
													{
														mFont = Panel.GetFont(7);
													}
												}
											}
										}
									}
								}
							}
							mFont.drawString(g, string.Concat(new object[]
							{
								"[",
								item.template.id,
								"] ",
								item.template.name,
								text
							}), num2 + 5, num3 + 1, 0);
							mFont.tahoma_7b_red.drawString(g, VuDang.saoTrongBalo(item), num4 + 15, num6 + 1, 0);
							string text2 = string.Empty;
							bool flag18 = item.itemOption != null;
							if (flag18)
							{
								bool flag19 = item.itemOption.Length != 0 && item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107;
								if (flag19)
								{
									text2 += item.itemOption[0].getOptionString();
								}
								mFont mFont2 = mFont.tahoma_7_blue;
								bool flag20 = item.compare < 0 && item.template.type != 5;
								if (flag20)
								{
									mFont2 = mFont.tahoma_7_red;
								}
								bool flag21 = item.itemOption.Length > 1;
								if (flag21)
								{
									for (int m = 1; m < 2; m++)
									{
										bool flag22 = item.itemOption[m] != null && item.itemOption[m].optionTemplate.id != 102 && item.itemOption[m].optionTemplate.id != 107;
										if (flag22)
										{
											text2 = text2 + "," + item.itemOption[m].getOptionString();
										}
									}
								}
								mFont2.drawString(g, text2, num2 + 5, num3 + 11, mFont.LEFT);
							}
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
							bool flag23 = item.itemOption != null;
							if (flag23)
							{
								for (int n = 0; n < item.itemOption.Length; n++)
								{
									this.paintOptItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8);
								}
								for (int num10 = 0; num10 < item.itemOption.Length; num10++)
								{
									this.paintOptSlotItem(g, item.itemOption[num10].optionTemplate.id, item.itemOption[num10].param, num5, num6, num7, num8);
								}
							}
							bool flag24 = item.quantity > 1;
							if (flag24)
							{
								mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity, num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
							}
						}
					}
				}
			}
		}
		catch (Exception)
		{
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x0007605C File Offset: 0x0007425C
	private void paintTab(mGraphics g)
	{
		bool flag = this.type == 23 || this.type == 24;
		if (flag)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.gameInfo, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
		}
		else
		{
			bool flag2 = this.type == 20;
			if (flag2)
			{
				g.setColor(13524492);
				g.fillRect(this.X + 1, 78, this.W - 2, 1);
				mFont.tahoma_7b_dark.drawString(g, mResources.account, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
			}
			else
			{
				bool flag3 = this.type == 22;
				if (flag3)
				{
					g.setColor(13524492);
					g.fillRect(this.X + 1, 78, this.W - 2, 1);
					mFont.tahoma_7b_dark.drawString(g, mResources.autoFunction, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
				}
				else
				{
					bool flag4 = this.type == 19;
					if (flag4)
					{
						g.setColor(13524492);
						g.fillRect(this.X + 1, 78, this.W - 2, 1);
						mFont.tahoma_7b_dark.drawString(g, mResources.option, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
					}
					else
					{
						bool flag5 = this.type == 18;
						if (flag5)
						{
							g.setColor(13524492);
							g.fillRect(this.X + 1, 78, this.W - 2, 1);
							mFont.tahoma_7b_dark.drawString(g, mResources.change_flag, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
						}
						else
						{
							bool flag6 = this.type == 13 && this.Equals(GameCanvas.panel2);
							if (flag6)
							{
								g.setColor(13524492);
								g.fillRect(this.X + 1, 78, this.W - 2, 1);
								mFont.tahoma_7b_dark.drawString(g, mResources.item_receive2, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
							}
							else
							{
								bool flag7 = this.type == 12 && GameCanvas.panel2 != null;
								if (flag7)
								{
									g.setColor(13524492);
									g.fillRect(this.X + 1, 78, this.W - 2, 1);
									mFont.tahoma_7b_dark.drawString(g, mResources.UPGRADE, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
								}
								else
								{
									bool flag8 = this.type == 11;
									if (flag8)
									{
										g.setColor(13524492);
										g.fillRect(this.X + 1, 78, this.W - 2, 1);
										mFont.tahoma_7b_dark.drawString(g, mResources.friend, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
									}
									else
									{
										bool flag9 = this.type == 16;
										if (flag9)
										{
											g.setColor(13524492);
											g.fillRect(this.X + 1, 78, this.W - 2, 1);
											mFont.tahoma_7b_dark.drawString(g, mResources.enemy, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
										}
										else
										{
											bool flag10 = this.type == 15;
											if (flag10)
											{
												g.setColor(13524492);
												g.fillRect(this.X + 1, 78, this.W - 2, 1);
												mFont.tahoma_7b_dark.drawString(g, this.topName, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
											}
											else
											{
												bool flag11 = this.type == 2 && GameCanvas.panel2 != null;
												if (flag11)
												{
													g.setColor(13524492);
													g.fillRect(this.X + 1, 78, this.W - 2, 1);
													mFont.tahoma_7b_dark.drawString(g, mResources.chest, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
												}
												else
												{
													bool flag12 = this.type == 9;
													if (flag12)
													{
														g.setColor(13524492);
														g.fillRect(this.X + 1, 78, this.W - 2, 1);
														mFont.tahoma_7b_dark.drawString(g, mResources.achievement_mission, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
													}
													else
													{
														bool flag13 = this.type == 3;
														if (flag13)
														{
															g.setColor(13524492);
															g.fillRect(this.X + 1, 78, this.W - 2, 1);
															mFont.tahoma_7b_dark.drawString(g, mResources.select_zone, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
														}
														else
														{
															bool flag14 = this.type == 14;
															if (flag14)
															{
																g.setColor(13524492);
																g.fillRect(this.X + 1, 78, this.W - 2, 1);
																mFont.tahoma_7b_dark.drawString(g, mResources.select_map, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
															}
															else
															{
																bool flag15 = this.type == 4;
																if (flag15)
																{
																	mFont.tahoma_7b_dark.drawString(g, mResources.map, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
																	g.setColor(13524492);
																	g.fillRect(this.X + 1, 78, this.W - 2, 1);
																}
																else
																{
																	bool flag16 = this.type == 7;
																	if (flag16)
																	{
																		mFont.tahoma_7b_dark.drawString(g, mResources.trangbi, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
																		g.setColor(13524492);
																		g.fillRect(this.X + 1, 78, this.W - 2, 1);
																	}
																	else
																	{
																		bool flag17 = this.type == 17;
																		if (flag17)
																		{
																			mFont.tahoma_7b_dark.drawString(g, mResources.kigui, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
																			g.setColor(13524492);
																			g.fillRect(this.X + 1, 78, this.W - 2, 1);
																		}
																		else
																		{
																			bool flag18 = this.type == 8;
																			if (flag18)
																			{
																				mFont.tahoma_7b_dark.drawString(g, mResources.msg, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
																				g.setColor(13524492);
																				g.fillRect(this.X + 1, 78, this.W - 2, 1);
																			}
																			else
																			{
																				bool flag19 = this.type == 10;
																				if (flag19)
																				{
																					mFont.tahoma_7b_dark.drawString(g, mResources.wat_do_u_want, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
																					g.setColor(13524492);
																					g.fillRect(this.X + 1, 78, this.W - 2, 1);
																				}
																				else
																				{
																					bool flag20 = this.currentTabIndex == 3 && this.mainTabName.Length != 4;
																					if (flag20)
																					{
																						g.translate(-this.cmx, 0);
																					}
																					for (int i = 0; i < this.currentTabName.Length; i++)
																					{
																						g.setColor((i != this.currentTabIndex) ? 16773296 : 6805896);
																						PopUp.paintPopUp(g, this.startTabPos + i * this.TAB_W, 52, this.TAB_W - 1, 25, (i == this.currentTabIndex) ? 1 : 0, true);
																						bool flag21 = i == this.keyTouchTab;
																						if (flag21)
																						{
																							g.drawImage(ItemMap.imageFlare, this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 62, 3);
																						}
																						mFont mFont = (i != this.currentTabIndex) ? mFont.tahoma_7_grey : mFont.tahoma_7_green2;
																						bool flag22 = !this.currentTabName[i][1].Equals(string.Empty);
																						if (flag22)
																						{
																							mFont.drawString(g, this.currentTabName[i][0], this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 53, mFont.CENTER);
																							mFont.drawString(g, this.currentTabName[i][1], this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 64, mFont.CENTER);
																						}
																						else
																						{
																							mFont.drawString(g, this.currentTabName[i][0], this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 59, mFont.CENTER);
																						}
																						bool flag23 = this.type == 0 && this.currentTabName.Length == 5 && GameScr.isNewClanMessage && GameCanvas.gameTick % 4 == 0;
																						if (flag23)
																						{
																							g.drawImage(ItemMap.imageFlare, this.startTabPos + 3 * this.TAB_W + this.TAB_W / 2, 77, mGraphics.BOTTOM | mGraphics.HCENTER);
																						}
																					}
																					g.setColor(13524492);
																					g.fillRect(1, 78, this.W - 2, 1);
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
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x00076A20 File Offset: 0x00074C20
	private void paintBottomMoneyInfo(mGraphics g)
	{
		bool flag = this.type != 13 || (this.currentTabIndex != 2 && !this.Equals(GameCanvas.panel2));
		if (flag)
		{
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			g.setColor(11837316);
			g.fillRect(this.X + 1, this.H - 15, this.W - 2, 14);
			g.setColor(13524492);
			g.fillRect(this.X + 1, this.H - 15, this.W - 2, 1);
			g.drawImage(Panel.imgXu, this.X + 11, this.H - 7, 3);
			g.drawImage(Panel.imgLuong, this.X + 75, this.H - 8, 3);
			mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().xuStr + string.Empty, this.X + 24, this.H - 13, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().luongStr + string.Empty, this.X + 85, this.H - 13, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(Panel.imgLuongKhoa, this.X + 130, this.H - 8, 3);
			mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().luongKhoaStr + string.Empty, this.X + 140, this.H - 13, mFont.LEFT, mFont.tahoma_7_grey);
		}
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00076BE0 File Offset: 0x00074DE0
	private void paintClanInfo(mGraphics g)
	{
		bool flag = global::Char.myCharz().clan == null;
		if (flag)
		{
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), 25, 50, 0, 33);
			mFont.tahoma_7b_white.drawString(g, mResources.not_join_clan, (this.wScroll - 50) / 2 + 50, 20, mFont.CENTER);
		}
		else
		{
			bool flag2 = !this.isViewMember;
			if (flag2)
			{
				Clan clan = global::Char.myCharz().clan;
				bool flag3 = clan != null;
				if (flag3)
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), 25, 50, 0, 33);
					mFont.tahoma_7b_white.drawString(g, clan.name, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
					mFont.tahoma_7_yellow.drawString(g, mResources.achievement_point + ": " + clan.powerPoint, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
					mFont.tahoma_7_yellow.drawString(g, mResources.clan_point + ": " + clan.clanPoint, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
					mFont.tahoma_7_yellow.drawString(g, mResources.level + ": " + clan.level, 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
					TextInfo.paint(g, clan.slogan, 60, 38, this.wScroll - 70, this.ITEM_HEIGHT, mFont.tahoma_7_yellow);
				}
			}
			else
			{
				Clan clan2 = (this.currClan == null) ? global::Char.myCharz().clan : this.currClan;
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), 25, 51, 0, 33);
				mFont.tahoma_7b_white.drawString(g, clan2.name, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
				mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
				{
					mResources.member,
					": ",
					clan2.currMember,
					"/",
					clan2.maxMember
				}), 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
				mFont.tahoma_7_yellow.drawString(g, mResources.clan_leader + ": " + clan2.leaderName, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
				TextInfo.paint(g, clan2.slogan, 60, 38, this.wScroll - 70, this.ITEM_HEIGHT, mFont.tahoma_7_yellow);
			}
		}
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x00076E70 File Offset: 0x00075070
	private void paintToolInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, mResources.dragon_ball + " " + GameMidlet.VERSION, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7_yellow.drawString(g, mResources.character + ": " + global::Char.myCharz().cName, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.account_server + " " + ServerListScreen.nameServer[ServerListScreen.ipSelect] + ":", 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, (!GameCanvas.loginScr.tfUser.getText().Equals(string.Empty)) ? GameCanvas.loginScr.tfUser.getText() : mResources.not_register_yet, 60, 39, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x00076F64 File Offset: 0x00075164
	private void paintGiaoDichInfo(mGraphics g)
	{
		mFont.tahoma_7_yellow.drawString(g, mResources.select_item, 60, 4, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.lock_trade, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.wait_opp_lock_trade, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.press_done, 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x000058DE File Offset: 0x00003ADE
	private void paintMyInfo(mGraphics g)
	{
		this.paintCharInfo(g, global::Char.myCharz());
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x00076FF0 File Offset: 0x000751F0
	private void paintPetInfo(mGraphics g)
	{
		mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(global::Char.myPetz().cPower), this.X + 60, 4, mFont.LEFT, mFont.tahoma_7_grey);
		bool flag = global::Char.myPetz().cPower > 0L;
		if (flag)
		{
			mFont.tahoma_7_yellow.drawString(g, (!global::Char.myPetz().me) ? global::Char.myPetz().currStrLevel : global::Char.myPetz().getStrLevel(), this.X + 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		}
		bool flag2 = global::Char.myPetz().cDamFull > 0;
		if (flag2)
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + " :" + global::Char.myPetz().cDamFull, this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		}
		bool flag3 = global::Char.myPetz().cMaxStamina > 0;
		if (flag3)
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.vitality, this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(GameScr.imgMPLost, this.X + 100, 41, 0);
			int num = global::Char.myPetz().cStamina * mGraphics.getImageWidth(GameScr.imgMP) / (int)global::Char.myPetz().cMaxStamina;
			g.setClip(100, this.X + 41, num, 20);
			g.drawImage(GameScr.imgMP, this.X + 100, 41, 0);
		}
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x0007719C File Offset: 0x0007539C
	private void paintCharInfo(mGraphics g, global::Char c)
	{
		mFont.tahoma_7b_white.drawString(g, ((GameScr.isNewMember == 1) ? "       " : string.Empty) + c.cName, this.X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		bool flag = GameScr.isNewMember == 1;
		if (flag)
		{
			SmallImage.drawSmallImage(g, 5427, this.X + 55, 4, 0, 0);
		}
		bool flag2 = c.cMaxStamina > 0;
		if (flag2)
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.vitality, this.X + 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(GameScr.imgMPLost, this.X + 95, 19, 0);
			int num = c.cStamina * mGraphics.getImageWidth(GameScr.imgMP) / (int)c.cMaxStamina;
			g.setClip(95, this.X + 19, num, 20);
			g.drawImage(GameScr.imgMP, this.X + 95, 19, 0);
		}
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		bool flag3 = c.cPower > 0L;
		if (flag3)
		{
			mFont.tahoma_7_yellow.drawString(g, (!c.me) ? c.currStrLevel : c.getStrLevel(), this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		}
		mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(c.cPower), this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x0007733C File Offset: 0x0007553C
	private void paintZoneInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, mResources.zone + " " + TileMap.zoneID, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7_yellow.drawString(g, TileMap.mapName, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7b_white.drawString(g, TileMap.zoneID + string.Empty, 25, 27, mFont.CENTER);
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x000773C4 File Offset: 0x000755C4
	public int getCompare(Item item)
	{
		bool flag = item == null;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			bool flag2 = item.isTypeBody();
			if (flag2)
			{
				bool flag3 = item.itemOption == null;
				if (flag3)
				{
					result = -1;
				}
				else
				{
					ItemOption itemOption = item.itemOption[0];
					bool flag4 = itemOption.optionTemplate.id == 22;
					if (flag4)
					{
						itemOption.optionTemplate = GameScr.gI().iOptionTemplates[6];
						itemOption.param *= 1000;
					}
					bool flag5 = itemOption.optionTemplate.id == 23;
					if (flag5)
					{
						itemOption.optionTemplate = GameScr.gI().iOptionTemplates[7];
						itemOption.param *= 1000;
					}
					Item item2 = null;
					for (int i = 0; i < global::Char.myCharz().arrItemBody.Length; i++)
					{
						Item item3 = global::Char.myCharz().arrItemBody[i];
						bool flag6 = itemOption.optionTemplate.id == 22;
						if (flag6)
						{
							itemOption.optionTemplate = GameScr.gI().iOptionTemplates[6];
							itemOption.param *= 1000;
						}
						bool flag7 = itemOption.optionTemplate.id == 23;
						if (flag7)
						{
							itemOption.optionTemplate = GameScr.gI().iOptionTemplates[7];
							itemOption.param *= 1000;
						}
						bool flag8 = item3 != null && item3.itemOption != null && item3.template.type == item.template.type;
						if (flag8)
						{
							item2 = item3;
							break;
						}
					}
					bool flag9 = item2 == null;
					if (flag9)
					{
						Res.outz("5");
						this.isUp = true;
						result = itemOption.param;
					}
					else
					{
						int num = (item2 == null || item2.itemOption == null) ? itemOption.param : (itemOption.param - item2.itemOption[0].param);
						bool flag10 = num < 0;
						if (flag10)
						{
							this.isUp = false;
						}
						else
						{
							this.isUp = true;
						}
						result = num;
					}
				}
			}
			else
			{
				result = 0;
			}
		}
		return result;
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x000775EC File Offset: 0x000757EC
	private void paintMapInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, mResources.MENUGENDER[(int)TileMap.planetID], 60, 4, mFont.LEFT);
		string str = string.Empty;
		bool flag = TileMap.mapID >= 135 && TileMap.mapID <= 138;
		if (flag)
		{
			str = " " + mResources.tang + TileMap.zoneID;
		}
		mFont.tahoma_7_yellow.drawString(g, TileMap.mapName + str, 60, 16, mFont.LEFT);
		mFont.tahoma_7b_white.drawString(g, mResources.quest_place + ": ", 60, 27, mFont.LEFT);
		bool flag2 = GameScr.getTaskMapId() >= 0 && GameScr.getTaskMapId() <= TileMap.mapNames.Length - 1;
		if (flag2)
		{
			mFont.tahoma_7_yellow.drawString(g, TileMap.mapNames[GameScr.getTaskMapId()], 60, 38, mFont.LEFT);
		}
		else
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.random, 60, 38, mFont.LEFT);
		}
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x00077704 File Offset: 0x00075904
	private void paintShopInfo(mGraphics g)
	{
		bool flag = this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null;
		if (flag)
		{
			this.paintMyInfo(g);
		}
		else
		{
			bool flag2 = this.selected < 0;
			if (flag2)
			{
				bool flag3 = this.typeShop != 2;
				if (flag3)
				{
					mFont.tahoma_7_white.drawString(g, mResources.say_hello, this.X + 60, 14, 0);
					mFont.tahoma_7_white.drawString(g, Panel.strWantToBuy, this.X + 60, 26, 0);
				}
				else
				{
					mFont.tahoma_7_white.drawString(g, mResources.say_hello, this.X + 60, 5, 0);
					mFont.tahoma_7_white.drawString(g, Panel.strWantToBuy, this.X + 60, 17, 0);
					mFont.tahoma_7_white.drawString(g, string.Concat(new object[]
					{
						mResources.page,
						" ",
						this.currPageShop[this.currentTabIndex] + 1,
						"/",
						this.maxPageShop[this.currentTabIndex]
					}), this.X + 60, 29, 0);
				}
			}
			else
			{
				bool flag4 = this.currentTabIndex < 0 || this.currentTabIndex > global::Char.myCharz().arrItemShop.Length - 1 || this.selected < 0 || this.selected > global::Char.myCharz().arrItemShop[this.currentTabIndex].Length - 1;
				if (!flag4)
				{
					Item item = global::Char.myCharz().arrItemShop[this.currentTabIndex][this.selected];
					bool flag5 = item != null;
					if (flag5)
					{
						bool flag6 = this.Equals(GameCanvas.panel) && this.currentTabIndex <= 3 && this.typeShop == 2;
						if (flag6)
						{
							mFont.tahoma_7b_white.drawString(g, string.Concat(new object[]
							{
								mResources.page,
								" ",
								this.currPageShop[this.currentTabIndex] + 1,
								"/",
								this.maxPageShop[this.currentTabIndex]
							}), this.X + 55, 4, 0);
						}
						mFont.tahoma_7b_white.drawString(g, string.Concat(new object[]
						{
							"[",
							item.template.id,
							"] ",
							item.template.name
						}), this.X + 55, 24, 0);
						string st = mResources.pow_request + " " + Res.formatNumber((long)item.template.strRequire);
						bool flag7 = (long)item.template.strRequire > global::Char.myCharz().cPower;
						if (flag7)
						{
							mFont.tahoma_7_yellow.drawString(g, st, this.X + 55, 35, 0);
						}
						else
						{
							mFont.tahoma_7_green.drawString(g, st, this.X + 55, 35, 0);
						}
					}
				}
			}
		}
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x00077A28 File Offset: 0x00075C28
	private void paintItemBoxInfo(mGraphics g)
	{
		string st = string.Concat(new object[]
		{
			mResources.used,
			": ",
			this.hasUse,
			"/",
			global::Char.myCharz().arrItemBox.Length,
			" ",
			mResources.place
		});
		mFont.tahoma_7b_white.drawString(g, mResources.chest, 60, 4, 0);
		mFont.tahoma_7_yellow.drawString(g, st, 60, 16, 0);
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x00077AB4 File Offset: 0x00075CB4
	private void paintSkillInfo(mGraphics g)
	{
		mFont.tahoma_7_white.drawString(g, "Top " + global::Char.myCharz().rank, this.X + 45 + (this.W - 50) / 2, 2, mFont.CENTER);
		mFont.tahoma_7_yellow.drawString(g, mResources.potential_point, this.X + 45 + (this.W - 50) / 2, 14, mFont.CENTER);
		mFont.tahoma_7_white.drawString(g, string.Empty + NinjaUtil.getMoneys(global::Char.myCharz().cTiemNang), this.X + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0) + 45 + (this.W - 50) / 2, 26, mFont.CENTER);
		mFont.tahoma_7_yellow.drawString(g, mResources.active_point + ": " + NinjaUtil.getMoneys(global::Char.myCharz().cNangdong), this.X + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0) + 45 + (this.W - 50) / 2, 38, mFont.CENTER);
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x00077BE8 File Offset: 0x00075DE8
	private void paintItemBodyBagInfo(mGraphics g)
	{
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
		{
			mResources.HP,
			": ",
			global::Char.myCharz().cHP,
			" / ",
			global::Char.myCharz().cHPFull
		}), this.X + 60, 2, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
		{
			mResources.KI,
			": ",
			global::Char.myCharz().cMP,
			" / ",
			global::Char.myCharz().cMPFull
		}), this.X + 60, 14, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + global::Char.myCharz().cDamFull, this.X + 60, 26, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
		{
			mResources.armor,
			": ",
			global::Char.myCharz().cDefull,
			", ",
			mResources.critical,
			": ",
			global::Char.myCharz().cCriticalFull,
			"%"
		}), this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00077D84 File Offset: 0x00075F84
	private void paintTopInfo(mGraphics g)
	{
		g.setClip(this.X + 1, this.Y, this.W - 2, this.yScroll - 2);
		g.setColor(9993045);
		g.fillRect(this.X, this.Y, this.W - 2, 50);
		switch (this.type)
		{
		case 0:
		{
			bool flag = this.currentTabIndex == 0;
			if (flag)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintMyInfo(g);
			}
			bool flag2 = this.currentTabIndex == 1;
			if (flag2)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintItemBodyBagInfo(g);
			}
			bool flag3 = this.currentTabIndex == 2;
			if (flag3)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintSkillInfo(g);
			}
			bool flag4 = this.currentTabIndex == 3;
			if (flag4)
			{
				bool flag5 = this.mainTabName.Length == 5;
				if (flag5)
				{
					this.paintClanInfo(g);
				}
				else
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintToolInfo(g);
				}
			}
			bool flag6 = this.currentTabIndex == 4;
			if (flag6)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintToolInfo(g);
			}
			break;
		}
		case 1:
		{
			bool flag7 = this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null;
			if (flag7)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			}
			else
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().npcFocus.avatar, this.X + 25, 50, 0, 33);
			}
			this.paintShopInfo(g);
			break;
		}
		case 2:
		{
			bool flag8 = this.currentTabIndex == 0;
			if (flag8)
			{
				SmallImage.drawSmallImage(g, 526, this.X + 25, 50, 0, 33);
				this.paintItemBoxInfo(g);
			}
			bool flag9 = this.currentTabIndex == 1;
			if (flag9)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintItemBodyBagInfo(g);
			}
			break;
		}
		case 3:
			SmallImage.drawSmallImage(g, 561, this.X + 25, 50, 0, 33);
			this.paintZoneInfo(g);
			break;
		case 4:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMapInfo(g);
			break;
		case 7:
		case 17:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			break;
		case 8:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			break;
		case 9:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			break;
		case 10:
		{
			bool flag10 = this.charMenu != null;
			if (flag10)
			{
				SmallImage.drawSmallImage(g, this.charMenu.avatarz(), this.X + 25, 50, 0, 33);
				this.paintCharInfo(g, this.charMenu);
			}
			break;
		}
		case 11:
		case 16:
		case 23:
		case 24:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			break;
		case 12:
		{
			bool flag11 = this.currentTabIndex == 0;
			if (flag11)
			{
				int id = 1410;
				for (int i = 0; i < GameScr.vNpc.size(); i++)
				{
					Npc npc = (Npc)GameScr.vNpc.elementAt(i);
					bool flag12 = npc.template.npcTemplateId == this.idNPC;
					if (flag12)
					{
						id = npc.avatar;
					}
				}
				SmallImage.drawSmallImage(g, id, this.X + 25, 50, 0, 33);
				this.paintCombineInfo(g);
			}
			bool flag13 = this.currentTabIndex == 1;
			if (flag13)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintMyInfo(g);
			}
			break;
		}
		case 13:
		{
			bool flag14 = this.currentTabIndex == 0 || this.currentTabIndex == 1;
			if (flag14)
			{
				bool flag15 = this.Equals(GameCanvas.panel);
				if (flag15)
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintGiaoDichInfo(g);
				}
				bool flag16 = this.Equals(GameCanvas.panel2) && this.charMenu != null;
				if (flag16)
				{
					SmallImage.drawSmallImage(g, this.charMenu.avatarz(), this.X + 25, 50, 0, 33);
					this.paintCharInfo(g, this.charMenu);
				}
			}
			bool flag17 = this.currentTabIndex == 2 && this.charMenu != null;
			if (flag17)
			{
				SmallImage.drawSmallImage(g, this.charMenu.avatarz(), this.X + 25, 50, 0, 33);
				this.paintCharInfo(g, this.charMenu);
			}
			break;
		}
		case 14:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMapInfo(g);
			break;
		case 15:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			break;
		case 18:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			break;
		case 19:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintToolInfo(g);
			break;
		case 20:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintToolInfo(g);
			break;
		case 21:
		{
			bool flag18 = this.currentTabIndex == 0;
			if (flag18)
			{
				SmallImage.drawSmallImage(g, global::Char.myPetz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintPetInfo(g);
			}
			bool flag19 = this.currentTabIndex == 1;
			if (flag19)
			{
				SmallImage.drawSmallImage(g, global::Char.myPetz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintPetStatusInfo(g);
			}
			bool flag20 = this.currentTabIndex == 2;
			if (flag20)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintItemBodyBagInfo(g);
			}
			break;
		}
		case 22:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintToolInfo(g);
			break;
		case 25:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			break;
		}
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x00078560 File Offset: 0x00076760
	private string getStatus(int status)
	{
		string result;
		switch (status)
		{
		case 0:
			result = mResources.follow;
			break;
		case 1:
			result = mResources.defend;
			break;
		case 2:
			result = mResources.attack;
			break;
		case 3:
			result = mResources.gohome;
			break;
		default:
			result = "aaa";
			break;
		}
		return result;
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x000785B4 File Offset: 0x000767B4
	private void paintPetStatusInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, string.Concat(new object[]
		{
			"HP: ",
			global::Char.myPetz().cHP,
			"/",
			global::Char.myPetz().cHPFull
		}), this.X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7b_white.drawString(g, string.Concat(new object[]
		{
			"MP: ",
			global::Char.myPetz().cMP,
			"/",
			global::Char.myPetz().cMPFull
		}), this.X + 60, 16, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new object[]
		{
			mResources.critical,
			": ",
			global::Char.myPetz().cCriticalFull,
			"   ",
			mResources.armor,
			": ",
			global::Char.myPetz().cDefull
		}), this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.status + " :" + this.strStatus[(int)global::Char.myPetz().petStatus], this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x0007873C File Offset: 0x0007693C
	private void paintCombineInfo(mGraphics g)
	{
		bool flag = this.combineTopInfo != null;
		if (flag)
		{
			for (int i = 0; i < this.combineTopInfo.Length; i++)
			{
				mFont.tahoma_7_white.drawString(g, this.combineTopInfo[i], this.X + 45 + (this.W - 50) / 2, 5 + i * 14, mFont.CENTER);
			}
		}
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x00003A0D File Offset: 0x00001C0D
	private void paintInfomation(mGraphics g)
	{
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x000787A8 File Offset: 0x000769A8
	public void paintMap(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(-this.cmxMap, -this.cmyMap);
		g.drawImage(Panel.imgMap, this.xScroll, this.yScroll, 0);
		int head = global::Char.myCharz().head;
		Part part = GameScr.parts[head];
		SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, this.xMap, this.yMap + 5, 0, 3);
		int align = mFont.CENTER;
		bool flag = this.xMap <= 40;
		if (flag)
		{
			align = mFont.LEFT;
		}
		bool flag2 = this.xMap >= 220;
		if (flag2)
		{
			align = mFont.RIGHT;
		}
		mFont.tahoma_7b_yellow.drawString(g, TileMap.mapName, this.xMap, this.yMap - 12, align, mFont.tahoma_7_grey);
		int num = -1;
		bool flag3 = GameScr.getTaskMapId() != -1;
		if (flag3)
		{
			for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
			{
				bool flag4 = Panel.mapId[(int)TileMap.planetID][i] == GameScr.getTaskMapId();
				if (flag4)
				{
					num = i;
					break;
				}
				num = 4;
			}
			bool flag5 = GameCanvas.gameTick % 4 > 0;
			if (flag5)
			{
				g.drawImage(ItemMap.imageFlare, this.xScroll + Panel.mapX[(int)TileMap.planetID][num], this.yScroll + Panel.mapY[(int)TileMap.planetID][num], 3);
			}
		}
		bool flag6 = !GameCanvas.isTouch;
		if (flag6)
		{
			g.drawImage(Panel.imgBantay, this.xMove, this.yMove, StaticObj.TOP_RIGHT);
			for (int j = 0; j < Panel.mapX[(int)TileMap.planetID].Length; j++)
			{
				int num2 = Panel.mapX[(int)TileMap.planetID][j] + this.xScroll;
				int num3 = Panel.mapY[(int)TileMap.planetID][j] + this.yScroll;
				bool flag7 = Res.inRect(num2 - 15, num3 - 15, 30, 30, this.xMove, this.yMove);
				if (flag7)
				{
					align = mFont.CENTER;
					bool flag8 = num2 <= 20;
					if (flag8)
					{
						align = mFont.LEFT;
					}
					bool flag9 = num2 >= 220;
					if (flag9)
					{
						align = mFont.RIGHT;
					}
					mFont.tahoma_7b_yellow.drawString(g, TileMap.mapNames[Panel.mapId[(int)TileMap.planetID][j]], num2, num3 - 12, align, mFont.tahoma_7_grey);
					break;
				}
			}
		}
		else
		{
			bool flag10 = !this.trans;
			if (flag10)
			{
				for (int k = 0; k < Panel.mapX[(int)TileMap.planetID].Length; k++)
				{
					int num4 = Panel.mapX[(int)TileMap.planetID][k] + this.xScroll;
					int num5 = Panel.mapY[(int)TileMap.planetID][k] + this.yScroll;
					bool flag11 = Res.inRect(num4 - 15, num5 - 15, 30, 30, this.pX, this.pY);
					if (flag11)
					{
						align = mFont.CENTER;
						bool flag12 = num4 <= 30;
						if (flag12)
						{
							align = mFont.LEFT;
						}
						bool flag13 = num4 >= 220;
						if (flag13)
						{
							align = mFont.RIGHT;
						}
						g.drawImage(Panel.imgBantay, num4, num5, StaticObj.TOP_RIGHT);
						mFont.tahoma_7b_yellow.drawString(g, TileMap.mapNames[Panel.mapId[(int)TileMap.planetID][k]], num4, num5 - 12, align, mFont.tahoma_7_grey);
						break;
					}
				}
			}
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		bool flag14 = num != -1;
		if (flag14)
		{
			bool flag15 = Panel.mapX[(int)TileMap.planetID][num] + this.xScroll < this.cmxMap;
			if (flag15)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 5, this.xScroll + 5, this.yScroll + this.hScroll / 2 - 4, 0);
			}
			bool flag16 = this.cmxMap + this.wScroll < Panel.mapX[(int)TileMap.planetID][num] + this.xScroll;
			if (flag16)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 6, this.xScroll + this.wScroll - 5, this.yScroll + this.hScroll / 2 - 4, StaticObj.TOP_RIGHT);
			}
			bool flag17 = Panel.mapY[(int)TileMap.planetID][num] < this.cmyMap;
			if (flag17)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, this.xScroll + this.wScroll / 2, this.yScroll + 5, StaticObj.TOP_CENTER);
			}
			bool flag18 = Panel.mapY[(int)TileMap.planetID][num] > this.cmyMap + this.hScroll;
			if (flag18)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - 5, StaticObj.BOTTOM_HCENTER);
			}
		}
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x00078CF4 File Offset: 0x00076EF4
	public void paintTask(mGraphics g)
	{
		int num = (GameCanvas.h <= 300) ? 15 : 20;
		bool flag = Panel.isPaintMap && !GameScr.gI().isMapDocNhan() && !GameScr.gI().isMapFize();
		if (flag)
		{
			g.drawImage((this.keyTouchMapButton != 1) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - num, 3);
			mFont.tahoma_7b_dark.drawString(g, mResources.map, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - (num + 5), mFont.CENTER);
		}
		this.xstart = this.xScroll + 5;
		this.ystart = this.yScroll + 14;
		this.yPaint = this.ystart;
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll - 35);
		bool flag2 = this.scroll != null;
		if (flag2)
		{
			bool flag3 = this.scroll.cmy > 0;
			if (flag3)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, this.xScroll + this.wScroll - 12, this.yScroll + 3, 0);
			}
			bool flag4 = this.scroll.cmy < this.scroll.cmyLim;
			if (flag4)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.xScroll + this.wScroll - 12, this.yScroll + this.hScroll - 45, 0);
			}
			g.translate(0, -this.scroll.cmy);
		}
		this.indexRowMax = 0;
		bool flag5 = this.indexMenu == 0;
		if (flag5)
		{
			bool flag6 = false;
			bool flag7 = global::Char.myCharz().taskMaint != null;
			if (flag7)
			{
				for (int i = 0; i < global::Char.myCharz().taskMaint.names.Length; i++)
				{
					mFont.tahoma_7_grey.drawString(g, global::Char.myCharz().taskMaint.names[i], this.xScroll + this.wScroll / 2, this.yPaint - 5 + i * 12, mFont.CENTER);
					this.indexRowMax++;
				}
				this.yPaint += (global::Char.myCharz().taskMaint.names.Length - 1) * 12;
				int num2 = 0;
				string text = string.Empty;
				for (int j = 0; j < global::Char.myCharz().taskMaint.subNames.Length; j++)
				{
					bool flag8 = global::Char.myCharz().taskMaint.subNames[j] != null;
					if (flag8)
					{
						num2 = j;
						text = "- " + global::Char.myCharz().taskMaint.subNames[j];
						bool flag9 = global::Char.myCharz().taskMaint.counts[j] != -1;
						if (flag9)
						{
							bool flag10 = global::Char.myCharz().taskMaint.index == j;
							if (flag10)
							{
								bool flag11 = global::Char.myCharz().taskMaint.counts[j] != 1;
								if (flag11)
								{
									string text2 = text;
									text = string.Concat(new object[]
									{
										text2,
										" (",
										global::Char.myCharz().taskMaint.count,
										"/",
										global::Char.myCharz().taskMaint.counts[j],
										")"
									});
								}
								bool flag12 = global::Char.myCharz().taskMaint.count == global::Char.myCharz().taskMaint.counts[j];
								if (flag12)
								{
									mFont.tahoma_7.drawString(g, text, this.xstart + 5, this.yPaint += 12, 0);
								}
								else
								{
									mFont mFont = mFont.tahoma_7_grey;
									bool flag13 = !flag6;
									if (flag13)
									{
										flag6 = true;
										mFont = mFont.tahoma_7_blue;
										mFont.drawString(g, text, this.xstart + 5 + ((mFont == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
									}
									else
									{
										mFont.drawString(g, "- ...", this.xstart + 5 + ((mFont == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
									}
								}
							}
							else
							{
								bool flag14 = global::Char.myCharz().taskMaint.index > j;
								if (flag14)
								{
									bool flag15 = global::Char.myCharz().taskMaint.counts[j] != 1;
									if (flag15)
									{
										string text3 = text;
										text = string.Concat(new object[]
										{
											text3,
											" (",
											global::Char.myCharz().taskMaint.counts[j],
											"/",
											global::Char.myCharz().taskMaint.counts[j],
											")"
										});
									}
									mFont.tahoma_7_white.drawString(g, text, this.xstart + 5, this.yPaint += 12, 0);
								}
								else
								{
									bool flag16 = global::Char.myCharz().taskMaint.counts[j] != 1;
									if (flag16)
									{
										text = text + " 0/" + global::Char.myCharz().taskMaint.counts[j];
									}
									mFont mFont2 = mFont.tahoma_7_grey;
									bool flag17 = !flag6;
									if (flag17)
									{
										flag6 = true;
										mFont2 = mFont.tahoma_7_blue;
										mFont2.drawString(g, text, this.xstart + 5 + ((mFont2 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
									}
									else
									{
										mFont2.drawString(g, "- ...", this.xstart + 5 + ((mFont2 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
									}
								}
							}
						}
						else
						{
							bool flag18 = global::Char.myCharz().taskMaint.index > j;
							if (flag18)
							{
								mFont.tahoma_7_white.drawString(g, text, this.xstart + 5, this.yPaint += 12, 0);
							}
							else
							{
								mFont mFont3 = mFont.tahoma_7_grey;
								bool flag19 = !flag6;
								if (flag19)
								{
									flag6 = true;
									mFont3 = mFont.tahoma_7_blue;
									mFont3.drawString(g, text, this.xstart + 5 + ((mFont3 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
								}
								else
								{
									mFont3.drawString(g, "- ...", this.xstart + 5 + ((mFont3 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
								}
							}
						}
						this.indexRowMax++;
					}
					else
					{
						bool flag20 = global::Char.myCharz().taskMaint.index <= j;
						if (flag20)
						{
							text = "- " + global::Char.myCharz().taskMaint.subNames[num2];
							mFont mFont4 = mFont.tahoma_7_grey;
							bool flag21 = !flag6;
							if (flag21)
							{
								flag6 = true;
								mFont4 = mFont.tahoma_7_blue;
							}
							mFont4.drawString(g, text, this.xstart + 5 + ((mFont4 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
						}
					}
				}
				this.yPaint += 5;
				for (int k = 0; k < global::Char.myCharz().taskMaint.details.Length; k++)
				{
					mFont.tahoma_7_green2.drawString(g, global::Char.myCharz().taskMaint.details[k], this.xstart + 5, this.yPaint += 12, 0);
					this.indexRowMax++;
				}
			}
			else
			{
				int taskMapId = GameScr.getTaskMapId();
				sbyte taskNpcId = GameScr.getTaskNpcId();
				string src = string.Empty;
				bool flag22 = taskMapId == -3 || taskNpcId == -3;
				if (flag22)
				{
					src = mResources.DES_TASK[3];
				}
				else
				{
					bool flag23 = global::Char.myCharz().taskMaint == null && global::Char.myCharz().ctaskId == 9 && global::Char.myCharz().nClass.classId == 0;
					if (flag23)
					{
						src = mResources.TASK_INPUT_CLASS;
					}
					else
					{
						bool flag24 = taskNpcId < 0 || taskMapId < 0;
						if (flag24)
						{
							return;
						}
						src = string.Concat(new string[]
						{
							mResources.DES_TASK[0],
							Npc.arrNpcTemplate[(int)taskNpcId].name,
							mResources.DES_TASK[1],
							TileMap.mapNames[taskMapId],
							mResources.DES_TASK[2]
						});
					}
				}
				string[] array = mFont.tahoma_7_white.splitFontArray(src, 150);
				for (int l = 0; l < array.Length; l++)
				{
					bool flag25 = l == 0;
					if (flag25)
					{
						mFont.tahoma_7_white.drawString(g, array[l], this.xstart + 5, this.yPaint = this.ystart, 0);
					}
					else
					{
						mFont.tahoma_7_white.drawString(g, array[l], this.xstart + 5, this.yPaint += 12, 0);
					}
				}
			}
		}
		else
		{
			bool flag26 = this.indexMenu == 1;
			if (flag26)
			{
				this.yPaint = this.ystart - 12;
				for (int m = 0; m < global::Char.myCharz().taskOrders.size(); m++)
				{
					TaskOrder taskOrder = (TaskOrder)global::Char.myCharz().taskOrders.elementAt(m);
					mFont.tahoma_7_white.drawString(g, taskOrder.name, this.xstart + 5, this.yPaint += 12, 0);
					bool flag27 = taskOrder.count == (int)taskOrder.maxCount;
					if (flag27)
					{
						mFont.tahoma_7_white.drawString(g, string.Concat(new object[]
						{
							(taskOrder.taskId != 0) ? mResources.KILLBOSS : mResources.KILL,
							" ",
							Mob.arrMobTemplate[taskOrder.killId].name,
							" (",
							taskOrder.count,
							"/",
							taskOrder.maxCount,
							")"
						}), this.xstart + 5, this.yPaint += 12, 0);
					}
					else
					{
						mFont.tahoma_7_blue.drawString(g, string.Concat(new object[]
						{
							(taskOrder.taskId != 0) ? mResources.KILLBOSS : mResources.KILL,
							" ",
							Mob.arrMobTemplate[taskOrder.killId].name,
							" (",
							taskOrder.count,
							"/",
							taskOrder.maxCount,
							")"
						}), this.xstart + 5, this.yPaint += 12, 0);
					}
					this.indexRowMax += 3;
					this.inforW = this.popupW - 25;
					this.paintMultiLine(g, mFont.tahoma_7_grey, taskOrder.description, this.xstart + 5, this.yPaint += 12, 0);
					this.yPaint += 12;
				}
			}
		}
		bool flag28 = this.scroll == null;
		if (flag28)
		{
			this.scroll = new Scroll();
			this.scroll.setStyle(this.indexRowMax, 12, this.xScroll, this.yScroll, this.wScroll, this.hScroll - num - 40, true, 1);
		}
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x00079A0C File Offset: 0x00077C0C
	public void paintMultiLine(mGraphics g, mFont f, string[] arr, string str, int x, int y, int align)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			string text = arr[i];
			bool flag = text.StartsWith("c");
			if (flag)
			{
				bool flag2 = text.StartsWith("c0");
				if (flag2)
				{
					text = text.Substring(2);
					f = mFont.tahoma_7b_dark;
				}
				else
				{
					bool flag3 = text.StartsWith("c1");
					if (flag3)
					{
						text = text.Substring(2);
						f = mFont.tahoma_7b_yellow;
					}
					else
					{
						bool flag4 = text.StartsWith("c2");
						if (flag4)
						{
							text = text.Substring(2);
							f = mFont.tahoma_7b_green;
						}
					}
				}
			}
			bool flag5 = i == 0;
			if (flag5)
			{
				f.drawString(g, text, x, y, align);
			}
			else
			{
				bool flag6 = i < this.indexRow + 30 && i > this.indexRow - 30;
				if (flag6)
				{
					f.drawString(g, text, x, y += 12, align);
				}
				else
				{
					y += 12;
				}
				this.yPaint += 12;
				this.indexRowMax++;
			}
		}
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x00079B30 File Offset: 0x00077D30
	public void paintMultiLine(mGraphics g, mFont f, string str, int x, int y, int align)
	{
		int num = (!GameCanvas.isTouch || GameCanvas.w < 320) ? 10 : 20;
		string[] array = f.splitFontArray(str, this.inforW - num);
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = i == 0;
			if (flag)
			{
				f.drawString(g, array[i], x, y, align);
			}
			else
			{
				bool flag2 = i < this.indexRow + 15 && i > this.indexRow - 15;
				if (flag2)
				{
					f.drawString(g, array[i], x, y += 12, align);
				}
				else
				{
					y += 12;
				}
				this.yPaint += 12;
				this.indexRowMax++;
			}
		}
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x00079C04 File Offset: 0x00077E04
	public void cleanCombine()
	{
		for (int i = 0; i < this.vItemCombine.size(); i++)
		{
			((Item)this.vItemCombine.elementAt(i)).isSelect = false;
		}
		this.vItemCombine.removeAllElements();
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x00079C54 File Offset: 0x00077E54
	public void hideNow()
	{
		bool flag = this.timeShow > 0;
		if (flag)
		{
			this.isClose = false;
		}
		else
		{
			bool flag2 = this.isTypeShop();
			if (flag2)
			{
				global::Char.myCharz().resetPartTemp();
			}
			bool flag3 = this.chatTField != null && this.type == 13 && this.chatTField.isShow;
			if (flag3)
			{
				this.chatTField = null;
			}
			bool flag4 = this.type == 13 && !this.isAccept;
			if (flag4)
			{
				Service.gI().giaodich(3, -1, -1, -1);
			}
			Res.outz("HIDE PANELLLLLLLLLLLLLLLLLLLLLL");
			SoundMn.gI().buttonClose();
			GameScr.isPaint = true;
			TileMap.lastPlanetId = -1;
			Panel.imgMap = null;
			mSystem.gcc();
			this.isClanOption = false;
			this.isClose = true;
			this.cleanCombine();
			Hint.clickNpc();
			GameCanvas.panel2 = null;
			GameCanvas.clearAllPointerEvent();
			GameCanvas.clearKeyPressed();
			this.pointerDownTime = (this.pointerDownFirstX = 0);
			this.pointerIsDowning = false;
			this.isShow = false;
			bool flag5 = (global::Char.myCharz().cHP <= 0 || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5) && global::Char.myCharz().meDead;
			if (flag5)
			{
				Command center = new Command(mResources.DIES[0], 11038, GameScr.gI());
				GameScr.gI().center = center;
				global::Char.myCharz().cHP = 0;
			}
		}
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x00079DD4 File Offset: 0x00077FD4
	public void hide()
	{
		bool flag = this.timeShow > 0;
		if (flag)
		{
			this.isClose = false;
		}
		else
		{
			bool flag2 = this.isTypeShop();
			if (flag2)
			{
				global::Char.myCharz().resetPartTemp();
			}
			bool flag3 = this.chatTField != null && this.type == 13 && this.chatTField.isShow;
			if (flag3)
			{
				this.chatTField = null;
			}
			bool flag4 = this.type == 13 && !this.isAccept;
			if (flag4)
			{
				Service.gI().giaodich(3, -1, -1, -1);
			}
			bool flag5 = this.type == 15;
			if (flag5)
			{
				Service.gI().sendThachDau(-1);
			}
			SoundMn.gI().buttonClose();
			GameScr.isPaint = true;
			TileMap.lastPlanetId = -1;
			bool flag6 = Panel.imgMap != null;
			if (flag6)
			{
				Panel.imgMap.texture = null;
				Panel.imgMap = null;
			}
			mSystem.gcc();
			this.isClanOption = false;
			bool flag7 = this.type != 4;
			if (flag7)
			{
				bool flag8 = this.type == 24;
				if (flag8)
				{
					this.setTypeGameInfo();
				}
				else
				{
					bool flag9 = this.type == 23;
					if (flag9)
					{
						this.setTypeMain();
					}
					else
					{
						bool flag10 = this.type == 3 || this.type == 14;
						if (flag10)
						{
							bool flag11 = this.isChangeZone;
							if (flag11)
							{
								this.isClose = true;
							}
							else
							{
								this.setTypeMain();
								this.cmx = (this.cmtoX = 0);
							}
						}
						else
						{
							bool flag12 = this.type == 18 || this.type == 19 || this.type == 20 || this.type == 21;
							if (flag12)
							{
								this.setTypeMain();
								this.cmx = (this.cmtoX = 0);
							}
							else
							{
								bool flag13 = this.type == 8 || this.type == 11 || this.type == 16;
								if (flag13)
								{
									this.setTypeAccount();
									this.cmx = (this.cmtoX = 0);
								}
								else
								{
									this.isClose = true;
								}
							}
						}
					}
				}
			}
			else
			{
				this.setTypeMain();
				this.cmx = (this.cmtoX = 0);
			}
			Hint.clickNpc();
			GameCanvas.panel2 = null;
			GameCanvas.clearAllPointerEvent();
			GameCanvas.clearKeyPressed();
			GameCanvas.isFocusPanel2 = false;
			this.pointerDownTime = (this.pointerDownFirstX = 0);
			this.pointerIsDowning = false;
			bool flag14 = (global::Char.myCharz().cHP <= 0 || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5) && global::Char.myCharz().meDead;
			if (flag14)
			{
				Command center = new Command(mResources.DIES[0], 11038, GameScr.gI());
				GameScr.gI().center = center;
				global::Char.myCharz().cHP = 0;
			}
		}
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x0007A0D0 File Offset: 0x000782D0
	public void update()
	{
		bool flag = this.chatTField != null && this.chatTField.isShow;
		if (flag)
		{
			this.chatTField.update();
		}
		else
		{
			bool flag2 = this.isKiguiXu;
			if (flag2)
			{
				this.delayKigui++;
				bool flag3 = this.delayKigui == 10;
				if (flag3)
				{
					this.delayKigui = 0;
					this.isKiguiXu = false;
					this.chatTField.tfChat.setText(string.Empty);
					this.chatTField.strChat = mResources.kiguiXuchat + " ";
					this.chatTField.tfChat.name = mResources.input_money;
					this.chatTField.to = string.Empty;
					this.chatTField.isShow = true;
					this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
					this.chatTField.tfChat.setMaxTextLenght(9);
					bool isTouch = GameCanvas.isTouch;
					if (isTouch)
					{
						this.chatTField.tfChat.doChangeToTextBox();
					}
					bool isWindowsPhone = Main.isWindowsPhone;
					if (isWindowsPhone)
					{
						this.chatTField.tfChat.strInfo = this.chatTField.strChat;
					}
					bool flag4 = !Main.isPC;
					if (flag4)
					{
						this.chatTField.startChat2(this, string.Empty);
					}
				}
			}
			else
			{
				bool flag5 = this.isKiguiLuong;
				if (flag5)
				{
					this.delayKigui++;
					bool flag6 = this.delayKigui == 10;
					if (flag6)
					{
						this.delayKigui = 0;
						this.isKiguiLuong = false;
						this.chatTField.tfChat.setText(string.Empty);
						this.chatTField.strChat = mResources.kiguiLuongchat + "  ";
						this.chatTField.tfChat.name = mResources.input_money;
						this.chatTField.to = string.Empty;
						this.chatTField.isShow = true;
						this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
						this.chatTField.tfChat.setMaxTextLenght(9);
						bool isTouch2 = GameCanvas.isTouch;
						if (isTouch2)
						{
							this.chatTField.tfChat.doChangeToTextBox();
						}
						bool isWindowsPhone2 = Main.isWindowsPhone;
						if (isWindowsPhone2)
						{
							this.chatTField.tfChat.strInfo = this.chatTField.strChat;
						}
						bool flag7 = !Main.isPC;
						if (flag7)
						{
							this.chatTField.startChat2(this, string.Empty);
						}
					}
				}
				else
				{
					bool flag8 = this.scroll != null;
					if (flag8)
					{
						this.scroll.updatecm();
					}
					bool flag9 = this.tabIcon != null && this.tabIcon.isShow;
					if (flag9)
					{
						this.tabIcon.update();
					}
					else
					{
						this.moveCamera();
						bool flag10 = this.waitToPerform > 0;
						if (flag10)
						{
							this.waitToPerform--;
							bool flag11 = this.waitToPerform == 0;
							if (flag11)
							{
								this.lastSelect[this.currentTabIndex] = this.selected;
								switch (this.type)
								{
								case 0:
									this.doFireMain();
									break;
								case 1:
								case 17:
									this.doFireShop();
									break;
								case 2:
									this.doFireBox();
									break;
								case 3:
									this.doFireZone();
									break;
								case 4:
									this.doFireMap();
									break;
								case 7:
								{
									bool flag12 = this.Equals(GameCanvas.panel2) && GameCanvas.panel.type == 2;
									if (flag12)
									{
										this.doFireBox();
										return;
									}
									this.doFireInventory();
									break;
								}
								case 8:
									this.doFireLogMessage();
									break;
								case 9:
									this.doFireArchivement();
									break;
								case 10:
									this.doFirePlayerMenu();
									break;
								case 11:
									this.doFireFriend();
									break;
								case 12:
									this.doFireCombine();
									break;
								case 13:
									this.doFireGiaoDich();
									break;
								case 14:
									this.doFireMapTrans();
									break;
								case 15:
									this.doFireTop();
									break;
								case 16:
									this.doFireEnemy();
									break;
								case 18:
									this.doFireChangeFlag();
									break;
								case 19:
									this.doFireOption();
									break;
								case 20:
									this.doFireAccount();
									break;
								case 21:
									this.doFirePetMain();
									break;
								case 22:
									this.doFireAuto();
									break;
								case 23:
									this.doFireGameInfo();
									break;
								case 25:
									this.doSpeacialSkill();
									break;
								}
							}
						}
						for (int i = 0; i < ClanMessage.vMessage.size(); i++)
						{
							((ClanMessage)ClanMessage.vMessage.elementAt(i)).update();
						}
						this.updateCombineEff();
					}
				}
			}
		}
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0007A5DC File Offset: 0x000787DC
	private void doSpeacialSkill()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command("Chọn nội tại", this, 8007, global::Char.myCharz().infoSpeacialSkill[this.currentTabIndex][this.selected]));
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0007A62C File Offset: 0x0007882C
	private void doFireGameInfo()
	{
		bool flag = this.selected != -1;
		if (flag)
		{
			this.infoSelect = this.selected;
			((GameInfo)Panel.vGameInfo.elementAt(this.infoSelect)).hasRead = true;
			Rms.saveRMSInt(((GameInfo)Panel.vGameInfo.elementAt(this.infoSelect)).id + string.Empty, 1);
			this.setTypeGameSubInfo();
		}
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x00003A0D File Offset: 0x00001C0D
	private void doFireAuto()
	{
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x0007A6AC File Offset: 0x000788AC
	private void doFirePetMain()
	{
		bool flag = this.currentTabIndex == 0;
		if (flag)
		{
			bool flag2 = this.selected == -1 || this.selected > global::Char.myPetz().arrItemBody.Length - 1;
			if (flag2)
			{
				return;
			}
			MyVector myVector = new MyVector(string.Empty);
			Item item = this.currItem = global::Char.myPetz().arrItemBody[this.selected];
			bool flag3 = this.currItem != null;
			if (flag3)
			{
				myVector.addElement(new Command(mResources.MOVEOUT, this, 2006, this.currItem));
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
		}
		bool flag4 = this.currentTabIndex == 1;
		if (flag4)
		{
			this.doFirePetStatus();
		}
		bool flag5 = this.currentTabIndex == 2;
		if (flag5)
		{
			this.doFireInventory();
		}
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0007A7C8 File Offset: 0x000789C8
	private void doFirePetStatus()
	{
		bool flag = this.selected == -1;
		if (!flag)
		{
			bool flag2 = this.selected == 5;
			if (flag2)
			{
				GameCanvas.startYesNoDlg(mResources.sure_fusion, new Command(mResources.YES, 888351), new Command(mResources.NO, 2001));
			}
			else
			{
				Service.gI().petStatus((sbyte)this.selected);
				bool flag3 = this.selected < 4;
				if (flag3)
				{
					VuDang.petStatus = (sbyte)this.selected;
					global::Char.myPetz().petStatus = (sbyte)this.selected;
				}
			}
		}
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x0007A860 File Offset: 0x00078A60
	private void doFireTop()
	{
		bool flag = this.selected >= -1;
		if (flag)
		{
			bool flag2 = this.isThachDau;
			if (flag2)
			{
				Service.gI().sendTop(this.topName, (sbyte)this.selected);
			}
			else
			{
				MyVector myVector = new MyVector(string.Empty);
				myVector.addElement(new Command(mResources.CHAR_ORDER[0], this, 9999, (TopInfo)this.vTop.elementAt(this.selected)));
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addThachDauDetail((TopInfo)this.vTop.elementAt(this.selected));
			}
		}
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x000058EE File Offset: 0x00003AEE
	private void doFireMapTrans()
	{
		this.doFireZone();
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0007A934 File Offset: 0x00078B34
	private void doFireGiaoDich()
	{
		bool flag = this.currentTabIndex == 0 && this.Equals(GameCanvas.panel);
		if (flag)
		{
			this.doFireInventory();
		}
		else
		{
			bool flag2 = (this.currentTabIndex == 0 && this.Equals(GameCanvas.panel2)) || this.currentTabIndex == 2;
			if (flag2)
			{
				bool flag3 = this.Equals(GameCanvas.panel2);
				if (flag3)
				{
					this.currItem = (Item)GameCanvas.panel2.vFriendGD.elementAt(this.selected);
				}
				else
				{
					this.currItem = (Item)GameCanvas.panel.vFriendGD.elementAt(this.selected);
				}
				Res.outz2("toi day select= " + this.selected);
				MyVector myVector = new MyVector();
				myVector.addElement(new Command(mResources.CLOSE, this, 8000, this.currItem));
				bool flag4 = this.currItem != null;
				if (flag4)
				{
					GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
					this.addItemDetail(this.currItem);
				}
				else
				{
					this.cp = null;
				}
			}
			bool flag5 = this.currentTabIndex == 1;
			if (flag5)
			{
				bool flag6 = this.selected == this.currentListLength - 3;
				if (flag6)
				{
					bool flag7 = this.isLock;
					if (flag7)
					{
						return;
					}
					this.putMoney();
				}
				else
				{
					bool flag8 = this.selected == this.currentListLength - 2;
					if (flag8)
					{
						bool flag9 = !this.isAccept;
						if (flag9)
						{
							this.isLock = !this.isLock;
							bool flag10 = this.isLock;
							if (flag10)
							{
								Service.gI().giaodich(5, -1, -1, -1);
							}
							else
							{
								this.hide();
								InfoDlg.showWait();
								Service.gI().giaodich(3, -1, -1, -1);
							}
						}
						else
						{
							this.isAccept = false;
						}
					}
					else
					{
						bool flag11 = this.selected == this.currentListLength - 1;
						if (flag11)
						{
							bool flag12 = this.isLock && !this.isAccept && this.isFriendLock;
							if (flag12)
							{
								GameCanvas.startYesNoDlg(mResources.do_u_sure_to_trade, new Command(mResources.YES, this, 7002, null), new Command(mResources.NO, this, 4005, null));
							}
						}
						else
						{
							bool flag13 = this.isLock;
							if (flag13)
							{
								return;
							}
							this.currItem = (Item)GameCanvas.panel.vMyGD.elementAt(this.selected);
							MyVector myVector2 = new MyVector();
							myVector2.addElement(new Command(mResources.CLOSE, this, 8000, this.currItem));
							bool flag14 = this.currItem != null;
							if (flag14)
							{
								GameCanvas.menu.startAt(myVector2, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
								this.addItemDetail(this.currItem);
							}
							else
							{
								this.cp = null;
							}
						}
					}
				}
			}
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				this.selected = -1;
			}
		}
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x0007AC80 File Offset: 0x00078E80
	private void doFireCombine()
	{
		bool flag = this.currentTabIndex == 0;
		if (flag)
		{
			bool flag2 = this.selected == -1 || this.vItemCombine.size() == 0;
			if (flag2)
			{
				return;
			}
			bool flag3 = this.selected == this.vItemCombine.size();
			if (flag3)
			{
				this.keyTouchCombine = -1;
				this.selected = (GameCanvas.isTouch ? -1 : 0);
				InfoDlg.showWait();
				Service.gI().combine(1, this.vItemCombine);
				return;
			}
			bool flag4 = this.selected > this.vItemCombine.size() - 1;
			if (flag4)
			{
				return;
			}
			this.currItem = (Item)GameCanvas.panel.vItemCombine.elementAt(this.selected);
			MyVector myVector = new MyVector();
			myVector.addElement(new Command(mResources.GETOUT, this, 6001, this.currItem));
			bool isDapDo = VuDang.isDapDo;
			if (isDapDo)
			{
				myVector.addElement(new Command("Chọn Số Sao Cần Đập", this, 8010, this.currItem));
			}
			bool flag5 = this.currItem != null;
			if (flag5)
			{
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
		}
		bool flag6 = this.currentTabIndex == 1;
		if (flag6)
		{
			this.doFireInventory();
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x0007AE10 File Offset: 0x00079010
	private void doFirePlayerMenu()
	{
		bool flag = this.selected != -1;
		if (flag)
		{
			this.isSelectPlayerMenu = true;
			this.hide();
		}
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x0007AE40 File Offset: 0x00079040
	private void doFireShop()
	{
		this.currItem = null;
		bool flag = this.selected < 0;
		if (!flag)
		{
			MyVector myVector = new MyVector();
			bool flag2 = this.currentTabIndex < this.currentTabName.Length - ((GameCanvas.panel2 == null) ? 1 : 0) && this.type != 17;
			if (flag2)
			{
				this.currItem = global::Char.myCharz().arrItemShop[this.currentTabIndex][this.selected];
				bool flag3 = this.currItem != null;
				if (flag3)
				{
					bool isBuySpec = this.currItem.isBuySpec;
					if (isBuySpec)
					{
						bool flag4 = this.currItem.buySpec > 0;
						if (flag4)
						{
							myVector.addElement(new Command(mResources.buy_with + "\n" + Res.formatNumber2((long)this.currItem.buySpec), this, 3005, this.currItem));
							myVector.addElement(new Command("Mua Nhiều\n" + Res.formatNumber2((long)this.currItem.buySpec) + "/1", Setup.gI(), 5, new string[]
							{
								"Nhập Số Item Cần Mua",
								"Chỉ Được Nhập Số"
							}));
						}
					}
					else
					{
						bool flag5 = this.typeShop == 4;
						if (flag5)
						{
							myVector.addElement(new Command(mResources.receive_upper, this, 30001, this.currItem));
							myVector.addElement(new Command(mResources.DELETE, this, 30002, this.currItem));
							myVector.addElement(new Command(mResources.receive_all, this, 30003, this.currItem));
						}
						else
						{
							bool flag6 = this.currItem.buyCoin == 0 && this.currItem.buyGold == 0;
							if (flag6)
							{
								bool flag7 = this.currItem.powerRequire != 0L;
								if (flag7)
								{
									myVector.addElement(new Command(string.Concat(new string[]
									{
										mResources.learn_with,
										"\n",
										Res.formatNumber(this.currItem.powerRequire),
										" \n",
										mResources.potential
									}), this, 3004, this.currItem));
								}
								else
								{
									myVector.addElement(new Command(mResources.receive_upper + "\n" + mResources.free, this, 3000, this.currItem));
								}
							}
							else
							{
								bool flag8 = this.typeShop == 8;
								if (flag8)
								{
									bool flag9 = this.currItem.buyCoin > 0;
									if (flag9)
									{
										myVector.addElement(new Command(string.Concat(new string[]
										{
											mResources.buy_with,
											"\n",
											Res.formatNumber2((long)this.currItem.buyCoin),
											"\n",
											mResources.XU
										}), this, 30001, this.currItem));
										myVector.addElement(new Command(string.Concat(new string[]
										{
											"Mua Nhiều\n",
											Res.formatNumber2((long)this.currItem.buyCoin),
											"\n",
											mResources.XU,
											"/1"
										}), Setup.gI(), 5, new string[]
										{
											"Nhập Số Item Cần Mua",
											"Chỉ Được Nhập Số"
										}));
									}
									bool flag10 = this.currItem.buyGold > 0;
									if (flag10)
									{
										myVector.addElement(new Command(string.Concat(new string[]
										{
											mResources.buy_with,
											"\n",
											Res.formatNumber2((long)this.currItem.buyGold),
											"\n",
											mResources.LUONG
										}), this, 30002, this.currItem));
										myVector.addElement(new Command(string.Concat(new string[]
										{
											"Mua Nhiều\n",
											Res.formatNumber2((long)this.currItem.buyGold),
											"\n",
											mResources.LUONG,
											"/1"
										}), Setup.gI(), 5, new string[]
										{
											"Nhập Số Item Cần Mua",
											"Chỉ Được Nhập Số"
										}));
									}
								}
								else
								{
									bool flag11 = this.typeShop != 2;
									if (flag11)
									{
										bool flag12 = this.currItem.buyCoin > 0;
										if (flag12)
										{
											myVector.addElement(new Command(string.Concat(new string[]
											{
												mResources.buy_with,
												"\n",
												Res.formatNumber2((long)this.currItem.buyCoin),
												"\n",
												mResources.XU
											}), this, 3000, this.currItem));
											myVector.addElement(new Command(string.Concat(new string[]
											{
												"Mua Nhiều\n",
												Res.formatNumber2((long)this.currItem.buyCoin),
												"\n",
												mResources.XU,
												"/1"
											}), Setup.gI(), 5, new string[]
											{
												"Nhập Số Item Cần Mua",
												"Chỉ Được Nhập Số"
											}));
										}
										bool flag13 = this.currItem.buyGold > 0;
										if (flag13)
										{
											myVector.addElement(new Command(string.Concat(new string[]
											{
												mResources.buy_with,
												"\n",
												Res.formatNumber2((long)this.currItem.buyGold),
												"\n",
												mResources.LUONG
											}), this, 3001, this.currItem));
											myVector.addElement(new Command(string.Concat(new string[]
											{
												"Mua Nhiều\n",
												Res.formatNumber2((long)this.currItem.buyGold),
												"\n",
												mResources.LUONG,
												"/1"
											}), Setup.gI(), 5, new string[]
											{
												"Nhập Số Item Cần Mua",
												"Chỉ Được Nhập Số"
											}));
										}
									}
									else
									{
										bool flag14 = this.currItem.buyCoin != -1;
										if (flag14)
										{
											myVector.addElement(new Command(string.Concat(new string[]
											{
												mResources.buy_with,
												"\n",
												Res.formatNumber2((long)this.currItem.buyCoin),
												"\n",
												mResources.XU
											}), this, 10016, this.currItem));
											myVector.addElement(new Command(string.Concat(new string[]
											{
												"Mua Nhiều\n",
												Res.formatNumber2((long)this.currItem.buyCoin),
												"\n",
												mResources.XU,
												"/1"
											}), Setup.gI(), 5, new string[]
											{
												"Nhập Số Item Cần Mua",
												"Chỉ Được Nhập Số"
											}));
										}
										bool flag15 = this.currItem.buyGold != -1;
										if (flag15)
										{
											myVector.addElement(new Command(string.Concat(new string[]
											{
												mResources.buy_with,
												"\n",
												Res.formatNumber2((long)this.currItem.buyGold),
												"\n",
												mResources.LUONG
											}), this, 10017, this.currItem));
											myVector.addElement(new Command(string.Concat(new string[]
											{
												"Mua Nhiều\n",
												Res.formatNumber2((long)this.currItem.buyGold),
												"\n",
												mResources.LUONG,
												"/1"
											}), Setup.gI(), 5, new string[]
											{
												"Nhập Số Item Cần Mua",
												"Chỉ Được Nhập Số"
											}));
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
				bool flag16 = this.typeShop == 0;
				if (flag16)
				{
					bool flag17 = this.selected == 0;
					if (flag17)
					{
						this.setNewSelected(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length, false);
					}
					else
					{
						this.currItem = null;
						bool flag18 = !this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
						if (flag18)
						{
							Item item = global::Char.myCharz().arrItemBag[this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody)];
							bool flag19 = item != null;
							if (flag19)
							{
								this.currItem = item;
							}
						}
						else
						{
							Item item2 = global::Char.myCharz().arrItemBody[this.GetInventorySelect_body(this.selected, this.newSelected)];
							bool flag20 = item2 != null;
							if (flag20)
							{
								this.currItem = item2;
							}
						}
						bool flag21 = this.currItem != null;
						if (flag21)
						{
							myVector.addElement(new Command(mResources.SALE, this, 3002, this.currItem));
						}
					}
				}
				else
				{
					bool flag22 = this.type == 17;
					if (flag22)
					{
						this.currItem = global::Char.myCharz().arrItemShop[4][this.selected];
					}
					else
					{
						this.currItem = global::Char.myCharz().arrItemShop[this.currentTabIndex][this.selected];
					}
					bool flag23 = this.currItem.buyType == 0;
					if (flag23)
					{
						bool flag24 = this.currItem.isHaveOption(87);
						if (flag24)
						{
							myVector.addElement(new Command(mResources.kiguiLuong, this, 10013, this.currItem));
						}
						else
						{
							myVector.addElement(new Command(mResources.kiguiXu, this, 10012, this.currItem));
						}
					}
					else
					{
						bool flag25 = this.currItem.buyType == 1;
						if (flag25)
						{
							myVector.addElement(new Command(mResources.huykigui, this, 10014, this.currItem));
							myVector.addElement(new Command(mResources.upTop, this, 10018, this.currItem));
						}
						else
						{
							bool flag26 = this.currItem.buyType == 2;
							if (flag26)
							{
								myVector.addElement(new Command(mResources.nhantien, this, 10015, this.currItem));
							}
						}
					}
				}
			}
			bool flag27 = this.currItem != null;
			if (flag27)
			{
				global::Char.myCharz().setPartTemp(this.currItem.headTemp, this.currItem.bodyTemp, this.currItem.legTemp, this.currItem.bagTemp);
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
		}
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x0007B900 File Offset: 0x00079B00
	private void doFireArchivement()
	{
		bool flag = this.selected >= 0 && global::Char.myCharz().arrArchive[this.selected].isFinish && !global::Char.myCharz().arrArchive[this.selected].isRecieve;
		if (flag)
		{
			bool flag2 = !GameCanvas.isTouch;
			if (flag2)
			{
				Service.gI().getArchivemnt(this.selected);
			}
			else
			{
				bool flag3 = GameCanvas.px > this.xScroll + this.wScroll - 40;
				if (flag3)
				{
					Service.gI().getArchivemnt(this.selected);
				}
			}
		}
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x0007B9A0 File Offset: 0x00079BA0
	private void doFireInventory()
	{
		Res.outz("fire inventory");
		bool flag = global::Char.myCharz().statusMe == 14;
		if (flag)
		{
			GameCanvas.startOKDlg(mResources.can_not_do_when_die);
		}
		else
		{
			bool flag2 = this.selected == -1;
			if (!flag2)
			{
				bool flag3 = this.selected == 0;
				if (flag3)
				{
					this.setNewSelected(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length, false);
				}
				else
				{
					this.currItem = null;
					MyVector myVector = new MyVector();
					bool flag4 = !this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
					if (flag4)
					{
						Item item = global::Char.myCharz().arrItemBag[this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody)];
						bool flag5 = item != null;
						if (flag5)
						{
							this.currItem = item;
							bool flag6 = GameCanvas.panel.type == 12;
							if (flag6)
							{
								myVector.addElement(new Command(mResources.use_for_combine, this, 6000, this.currItem));
							}
							else
							{
								bool flag7 = GameCanvas.panel.type == 13;
								if (flag7)
								{
									myVector.addElement(new Command(mResources.use_for_trade, this, 7000, this.currItem));
								}
								else
								{
									bool flag8 = item.isTypeBody();
									if (flag8)
									{
										myVector.addElement(new Command(mResources.USE, this, 2000, this.currItem));
										bool havePet = global::Char.myCharz().havePet;
										if (havePet)
										{
											myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, this.currItem));
										}
										myVector.addElement(new Command("Thêm vào set 1", this, 3142, this.currItem));
										myVector.addElement(new Command("Thêm vào set 2", this, 3143, this.currItem));
									}
									else
									{
										myVector.addElement(new Command(mResources.USE, this, 2001, this.currItem));
									}
								}
							}
						}
					}
					else
					{
						Item item2 = global::Char.myCharz().arrItemBody[this.GetInventorySelect_body(this.selected, this.newSelected)];
						bool flag9 = item2 != null;
						if (flag9)
						{
							this.currItem = item2;
							myVector.addElement(new Command(mResources.GETOUT, this, 2002, this.currItem));
						}
					}
					bool flag10 = this.currItem != null;
					if (flag10)
					{
						global::Char.myCharz().setPartTemp(this.currItem.headTemp, this.currItem.bodyTemp, this.currItem.legTemp, this.currItem.bagTemp);
						bool flag11 = GameCanvas.panel.type != 12 && GameCanvas.panel.type != 13;
						if (flag11)
						{
							bool flag12 = this.position == 0;
							if (flag12)
							{
								myVector.addElement(new Command(mResources.MOVEOUT, this, 2003, this.currItem));
								bool flag13 = this.currItem.template.type == 29 || this.currItem.template.type == 33 || this.currItem.template.id == 380 || this.currItem.quantity >= 2;
								if (flag13)
								{
									myVector.addElement(new Command("Thêm vào ListItem", this, 8009, this.currItem));
								}
							}
							bool flag14 = this.position == 1;
							if (flag14)
							{
								myVector.addElement(new Command(mResources.SALE, this, 3002, this.currItem));
							}
						}
						GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
						this.addItemDetail(this.currItem);
					}
					else
					{
						this.cp = null;
					}
				}
			}
		}
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x0007BDB4 File Offset: 0x00079FB4
	private void doRada()
	{
		this.hide();
		bool flag = RadarScr.list == null || RadarScr.list.size() == 0;
		if (flag)
		{
			Service.gI().SendRada(0, -1);
			RadarScr.gI().switchToMe();
		}
		else
		{
			RadarScr.gI().switchToMe();
		}
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x0007BE10 File Offset: 0x0007A010
	private void doFireTool()
	{
		bool flag = this.selected < 0;
		if (!flag)
		{
			bool flag2 = SoundMn.IsDelAcc && this.selected == Panel.strTool.Length - 1;
			if (flag2)
			{
				Service.gI().sendDelAcc();
			}
			else
			{
				bool flag3 = !global::Char.myCharz().havePet;
				if (flag3)
				{
					switch (this.selected)
					{
					case 0:
						this.doRada();
						break;
					case 1:
						this.hide();
						Service.gI().openMenu(54);
						break;
					case 2:
						this.setTypeGameInfo();
						break;
					case 3:
						Service.gI().getFlag(0, -1);
						InfoDlg.showWait();
						break;
					case 4:
					{
						bool flag4 = global::Char.myCharz().statusMe == 14;
						if (flag4)
						{
							GameCanvas.startOKDlg(mResources.can_not_do_when_die);
						}
						else
						{
							Service.gI().openUIZone();
							GameCanvas.panel.setTypeZone();
							GameCanvas.panel.show();
						}
						break;
					}
					case 5:
					{
						GameCanvas.endDlg();
						bool flag5 = global::Char.myCharz().checkLuong() < 5;
						if (flag5)
						{
							GameCanvas.startOKDlg(mResources.not_enough_luong_world_channel);
						}
						else
						{
							bool flag6 = this.chatTField == null;
							if (flag6)
							{
								this.chatTField = new ChatTextField();
								this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
								this.chatTField.initChatTextField();
								this.chatTField.parentScreen = GameCanvas.panel;
							}
							this.chatTField.strChat = mResources.world_channel_5_luong;
							this.chatTField.tfChat.name = mResources.CHAT;
							this.chatTField.to = string.Empty;
							this.chatTField.isShow = true;
							this.chatTField.tfChat.isFocus = true;
							this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
							bool isWindowsPhone = Main.isWindowsPhone;
							if (isWindowsPhone)
							{
								this.chatTField.tfChat.strInfo = this.chatTField.strChat;
							}
							bool flag7 = !Main.isPC;
							if (flag7)
							{
								this.chatTField.startChat2(this, string.Empty);
							}
							else
							{
								bool isTouch = GameCanvas.isTouch;
								if (isTouch)
								{
									this.chatTField.tfChat.doChangeToTextBox();
								}
							}
						}
						break;
					}
					case 6:
						this.setTypeAccount();
						break;
					case 7:
						this.setTypeOption();
						break;
					case 8:
						GameCanvas.loginScr.backToRegister();
						break;
					case 9:
					{
						bool isLogin = GameCanvas.loginScr.isLogin2;
						if (isLogin)
						{
							SoundMn.gI().backToRegister();
						}
						break;
					}
					}
				}
				else
				{
					switch (this.selected)
					{
					case 0:
						this.doRada();
						break;
					case 1:
						this.hide();
						Service.gI().openMenu(54);
						break;
					case 2:
						this.setTypeGameInfo();
						break;
					case 3:
						this.doFirePet();
						break;
					case 4:
						Service.gI().getFlag(0, -1);
						InfoDlg.showWait();
						break;
					case 5:
					{
						bool flag8 = global::Char.myCharz().statusMe == 14;
						if (flag8)
						{
							GameCanvas.startOKDlg(mResources.can_not_do_when_die);
						}
						else
						{
							Service.gI().openUIZone();
							GameCanvas.panel.setTypeZone();
							GameCanvas.panel.show();
						}
						break;
					}
					case 6:
					{
						GameCanvas.endDlg();
						bool flag9 = global::Char.myCharz().checkLuong() < 5;
						if (flag9)
						{
							GameCanvas.startOKDlg(mResources.not_enough_luong_world_channel);
						}
						else
						{
							bool flag10 = this.chatTField == null;
							if (flag10)
							{
								this.chatTField = new ChatTextField();
								this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
								this.chatTField.initChatTextField();
								this.chatTField.parentScreen = GameCanvas.panel;
							}
							this.chatTField.strChat = mResources.world_channel_5_luong;
							this.chatTField.tfChat.name = mResources.CHAT;
							this.chatTField.to = string.Empty;
							this.chatTField.isShow = true;
							this.chatTField.tfChat.isFocus = true;
							this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
							bool isWindowsPhone2 = Main.isWindowsPhone;
							if (isWindowsPhone2)
							{
								this.chatTField.tfChat.strInfo = this.chatTField.strChat;
							}
							bool flag11 = !Main.isPC;
							if (flag11)
							{
								this.chatTField.startChat2(this, string.Empty);
							}
							else
							{
								bool isTouch2 = GameCanvas.isTouch;
								if (isTouch2)
								{
									this.chatTField.tfChat.doChangeToTextBox();
								}
							}
						}
						break;
					}
					case 7:
						this.setTypeAccount();
						break;
					case 8:
						this.setTypeOption();
						break;
					case 9:
						GameCanvas.loginScr.backToRegister();
						break;
					case 10:
					{
						bool isLogin2 = GameCanvas.loginScr.isLogin2;
						if (isLogin2)
						{
							SoundMn.gI().backToRegister();
						}
						break;
					}
					}
				}
			}
		}
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0007C364 File Offset: 0x0007A564
	private void setTypeGameSubInfo()
	{
		string content = ((GameInfo)Panel.vGameInfo.elementAt(this.infoSelect)).content;
		Panel.contenInfo = mFont.tahoma_7_grey.splitFontArray(content, this.wScroll - 40);
		this.currentListLength = Panel.contenInfo.Length;
		this.ITEM_HEIGHT = 16;
		this.selected = (GameCanvas.isTouch ? -1 : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.type = 24;
		this.setType(0);
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x0007C458 File Offset: 0x0007A658
	private void setTypeGameInfo()
	{
		this.currentListLength = Panel.vGameInfo.size();
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? -1 : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.type = 23;
		this.setType(0);
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0007C518 File Offset: 0x0007A718
	private void doFirePet()
	{
		InfoDlg.showWait();
		Service.gI().petInfo();
		this.timeShow = 20;
		bool flag = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
		if (flag)
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
			GameCanvas.panel.setTypePetMain();
			GameCanvas.panel.show();
		}
		else
		{
			GameCanvas.panel.tabName[21] = mResources.petMainTab;
			GameCanvas.panel.setTypePetMain();
			GameCanvas.panel.show();
		}
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0007C5D8 File Offset: 0x0007A7D8
	private void searchClan()
	{
		this.chatTField.strChat = mResources.input_clan_name;
		this.chatTField.tfChat.name = mResources.clan_name;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.isFocus = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		bool flag = !Main.isPC;
		if (flag)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x0007C694 File Offset: 0x0007A894
	private void chatClan()
	{
		this.chatTField.strChat = mResources.chat_clan;
		this.chatTField.tfChat.name = mResources.CHAT;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.isFocus = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		bool flag = !Main.isPC;
		if (flag)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x0007C750 File Offset: 0x0007A950
	public void creatClan()
	{
		this.chatTField.strChat = mResources.input_clan_name_to_create;
		this.chatTField.tfChat.name = mResources.input_clan_name;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		bool flag = !Main.isPC;
		if (flag)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x0007C7FC File Offset: 0x0007A9FC
	public void putMoney()
	{
		bool flag = this.chatTField == null;
		if (flag)
		{
			this.chatTField = new ChatTextField();
			this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			this.chatTField.initChatTextField();
			this.chatTField.parentScreen = GameCanvas.panel;
		}
		this.chatTField.strChat = mResources.input_money_to_trade;
		this.chatTField.tfChat.name = mResources.input_money;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
		this.chatTField.tfChat.setMaxTextLenght(9);
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.chatTField.tfChat.doChangeToTextBox();
		}
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		bool flag2 = !Main.isPC;
		if (flag2)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0007C934 File Offset: 0x0007AB34
	public void putQuantily()
	{
		bool flag = this.chatTField == null;
		if (flag)
		{
			this.chatTField = new ChatTextField();
			this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			this.chatTField.initChatTextField();
			this.chatTField.parentScreen = GameCanvas.panel;
		}
		this.chatTField.strChat = mResources.input_quantity_to_trade;
		this.chatTField.tfChat.name = mResources.input_quantity;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.chatTField.tfChat.doChangeToTextBox();
		}
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		bool flag2 = !Main.isPC;
		if (flag2)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x0007CA5C File Offset: 0x0007AC5C
	public void chagenSlogan()
	{
		this.chatTField.strChat = mResources.input_clan_slogan;
		this.chatTField.tfChat.name = mResources.input_clan_slogan;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.isFocus = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		bool flag = !Main.isPC;
		if (flag)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x0007CB18 File Offset: 0x0007AD18
	public void changeIcon()
	{
		bool flag = this.tabIcon == null;
		if (flag)
		{
			this.tabIcon = new TabClanIcon();
		}
		this.tabIcon.text = this.chatTField.tfChat.getText();
		this.tabIcon.show(false);
		this.chatTField.isShow = false;
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0007CB74 File Offset: 0x0007AD74
	private void addFriend(InfoItem info)
	{
		string text = "|0|1|" + info.charInfo.cName;
		text += "\n";
		text = ((!info.isOnline) ? (text + "|3|1|" + mResources.is_offline) : (text + "|4|1|" + mResources.is_online));
		text += "\n--";
		string text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"\n|5|",
			mResources.power,
			": ",
			info.s
		});
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.charInfo = info.charInfo;
		this.currItem = null;
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x0007CC3C File Offset: 0x0007AE3C
	private void doFireEnemy()
	{
		bool flag = this.selected >= 0 && this.vEnemy.size() != 0;
		if (flag)
		{
			MyVector myVector = new MyVector();
			this.currInfoItem = this.selected;
			myVector.addElement(new Command(mResources.REVENGE, this, 10000, (InfoItem)this.vEnemy.elementAt(this.currInfoItem)));
			myVector.addElement(new Command(mResources.DELETE, this, 10001, (InfoItem)this.vEnemy.elementAt(this.currInfoItem)));
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addFriend((InfoItem)this.vEnemy.elementAt(this.selected));
		}
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0007CD28 File Offset: 0x0007AF28
	private void doFireFriend()
	{
		bool flag = this.selected >= 0 && this.vFriend.size() != 0;
		if (flag)
		{
			MyVector myVector = new MyVector();
			this.currInfoItem = this.selected;
			myVector.addElement(new Command(mResources.CHAT, this, 8001, (InfoItem)this.vFriend.elementAt(this.currInfoItem)));
			myVector.addElement(new Command(mResources.DELETE, this, 8002, (InfoItem)this.vFriend.elementAt(this.currInfoItem)));
			myVector.addElement(new Command(mResources.den, this, 8004, (InfoItem)this.vFriend.elementAt(this.currInfoItem)));
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addFriend((InfoItem)this.vFriend.elementAt(this.selected));
		}
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x0007CE40 File Offset: 0x0007B040
	private void doFireChangeFlag()
	{
		bool flag = this.selected >= 0;
		if (flag)
		{
			MyVector myVector = new MyVector();
			this.currInfoItem = this.selected;
			myVector.addElement(new Command(mResources.change_flag, this, 10030, null));
			myVector.addElement(new Command(mResources.BACK, this, 10031, null));
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
		}
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x0007CED4 File Offset: 0x0007B0D4
	private void doFireLogMessage()
	{
		bool flag = this.selected == 0;
		if (flag)
		{
			this.isViewChatServer = !this.isViewChatServer;
			Rms.saveRMSInt("viewchat", this.isViewChatServer ? 1 : 0);
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				this.selected = -1;
			}
		}
		else
		{
			bool flag2 = this.selected >= 0 && this.logChat.size() != 0;
			if (flag2)
			{
				MyVector myVector = new MyVector();
				this.currInfoItem = this.selected - 1;
				myVector.addElement(new Command(mResources.CHAT, this, 8001, (InfoItem)this.logChat.elementAt(this.currInfoItem)));
				myVector.addElement(new Command(mResources.make_friend, this, 8003, (InfoItem)this.logChat.elementAt(this.currInfoItem)));
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addLogMessage((InfoItem)this.logChat.elementAt(this.selected - 1));
			}
		}
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x0007D010 File Offset: 0x0007B210
	private void doFireClanOption()
	{
		this.partID = null;
		this.charInfo = null;
		Res.outz("cSelect= " + this.cSelected);
		bool flag = this.selected < 0;
		if (flag)
		{
			this.cSelected = -1;
		}
		else
		{
			bool flag2 = global::Char.myCharz().clan == null;
			if (flag2)
			{
				bool flag3 = this.selected == 0;
				if (flag3)
				{
					bool flag4 = this.cSelected == 0;
					if (flag4)
					{
						this.searchClan();
					}
					else
					{
						bool flag5 = this.cSelected == 1;
						if (flag5)
						{
							InfoDlg.showWait();
							this.creatClan();
							Service.gI().getClan(1, -1, null);
						}
					}
				}
				else
				{
					bool flag6 = this.selected != -1;
					if (flag6)
					{
						bool flag7 = this.selected == 1;
						if (flag7)
						{
							bool flag8 = this.isSearchClan;
							if (flag8)
							{
								Service.gI().searchClan(string.Empty);
							}
							else
							{
								bool flag9 = this.isViewMember && this.currClan != null;
								if (flag9)
								{
									GameCanvas.startYesNoDlg(mResources.do_u_want_join_clan + this.currClan.name, new Command(mResources.YES, this, 4000, this.currClan), new Command(mResources.NO, this, 4005, this.currClan));
								}
							}
						}
						else
						{
							bool flag10 = this.isSearchClan;
							if (flag10)
							{
								this.currClan = this.getCurrClan();
								bool flag11 = this.currClan != null;
								if (flag11)
								{
									MyVector myVector = new MyVector();
									myVector.addElement(new Command(mResources.request_join_clan, this, 4000, this.currClan));
									myVector.addElement(new Command(mResources.view_clan_member, this, 4001, this.currClan));
									GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
									this.addClanDetail(this.getCurrClan());
								}
							}
							else
							{
								bool flag12 = this.isViewMember;
								if (flag12)
								{
									this.currMem = this.getCurrMember();
									bool flag13 = this.currMem != null;
									if (flag13)
									{
										MyVector myVector2 = new MyVector();
										myVector2.addElement(new Command(mResources.CLOSE, this, 8000, this.currClan));
										GameCanvas.menu.startAt(myVector2, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
										GameCanvas.menu.startAt(myVector2, 0, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
										this.addClanMemberDetail(this.currMem);
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag14 = this.selected == 0;
				if (flag14)
				{
					bool flag15 = this.isMessage;
					if (flag15)
					{
						bool flag16 = this.cSelected == 0;
						if (flag16)
						{
							bool flag17 = this.myMember.size() > 1;
							if (flag17)
							{
								this.chatClan();
							}
							else
							{
								this.member = null;
								this.isSearchClan = false;
								this.isViewMember = true;
								this.isMessage = false;
								this.currentListLength = this.myMember.size() + 2;
								this.initTabClans();
							}
						}
						bool flag18 = this.cSelected == 1;
						if (flag18)
						{
							Service.gI().clanMessage(1, null, -1);
						}
						bool flag19 = this.cSelected == 2;
						if (flag19)
						{
							this.member = null;
							this.isSearchClan = false;
							this.isViewMember = true;
							this.isMessage = false;
							this.currentListLength = this.myMember.size() + 2;
							this.initTabClans();
							this.getCurrClanOtion();
						}
					}
					else
					{
						bool flag20 = this.isViewMember;
						if (flag20)
						{
							bool flag21 = this.cSelected == 0;
							if (flag21)
							{
								this.isSearchClan = false;
								this.isViewMember = false;
								this.isMessage = true;
								this.currentListLength = ClanMessage.vMessage.size() + 2;
								this.initTabClans();
							}
							bool flag22 = this.cSelected == 1;
							if (flag22)
							{
								bool flag23 = this.myMember.size() > 1;
								if (flag23)
								{
									Service.gI().leaveClan();
								}
								else
								{
									this.chagenSlogan();
								}
							}
							bool flag24 = this.cSelected == 2;
							if (flag24)
							{
								bool flag25 = this.myMember.size() > 1;
								if (flag25)
								{
									this.chagenSlogan();
								}
								else
								{
									Service.gI().getClan(3, -1, null);
								}
							}
							bool flag26 = this.cSelected == 3;
							if (flag26)
							{
								Service.gI().getClan(3, -1, null);
							}
						}
					}
				}
				else
				{
					bool flag27 = this.selected == 1;
					if (flag27)
					{
						bool flag28 = this.isSearchClan;
						if (flag28)
						{
							Service.gI().searchClan(string.Empty);
						}
					}
					else
					{
						bool flag29 = this.isSearchClan;
						if (flag29)
						{
							this.currClan = this.getCurrClan();
							bool flag30 = this.currClan != null;
							if (flag30)
							{
								MyVector myVector3 = new MyVector();
								myVector3.addElement(new Command(mResources.view_clan_member, this, 4001, this.currClan));
								GameCanvas.menu.startAt(myVector3, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
								this.addClanDetail(this.getCurrClan());
							}
						}
						else
						{
							bool flag31 = this.isViewMember;
							if (flag31)
							{
								Res.outz("TOI DAY 1");
								this.currMem = this.getCurrMember();
								bool flag32 = this.currMem != null;
								if (flag32)
								{
									MyVector myVector4 = new MyVector();
									Res.outz("TOI DAY 2");
									bool flag33 = this.member != null;
									if (flag33)
									{
										myVector4.addElement(new Command(mResources.CLOSE, this, 8000, null));
										Res.outz("TOI DAY 3");
									}
									else
									{
										bool flag34 = this.myMember != null;
										if (flag34)
										{
											Res.outz("TOI DAY 4");
											Res.outz("my role= " + global::Char.myCharz().role);
											bool flag35 = global::Char.myCharz().charID == this.currMem.ID || global::Char.myCharz().role == 2;
											if (flag35)
											{
												myVector4.addElement(new Command(mResources.CLOSE, this, 8000, this.currMem));
											}
											bool flag36 = global::Char.myCharz().role < 2 && global::Char.myCharz().charID != this.currMem.ID;
											if (flag36)
											{
												Res.outz("TOI DAY");
												bool flag37 = this.currMem.role == 0 || this.currMem.role == 1;
												if (flag37)
												{
													myVector4.addElement(new Command(mResources.CLOSE, this, 8000, this.currMem));
												}
												bool flag38 = this.currMem.role == 2;
												if (flag38)
												{
													myVector4.addElement(new Command(mResources.create_clan_co_leader, this, 5002, this.currMem));
												}
												bool flag39 = global::Char.myCharz().role == 0;
												if (flag39)
												{
													myVector4.addElement(new Command(mResources.create_clan_leader, this, 5001, this.currMem));
													bool flag40 = this.currMem.role == 1;
													if (flag40)
													{
														myVector4.addElement(new Command(mResources.disable_clan_mastership, this, 5003, this.currMem));
													}
												}
											}
											bool flag41 = global::Char.myCharz().role < this.currMem.role;
											if (flag41)
											{
												myVector4.addElement(new Command(mResources.kick_clan_mem, this, 5004, this.currMem));
											}
										}
									}
									GameCanvas.menu.startAt(myVector4, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
									this.addClanMemberDetail(this.currMem);
								}
							}
							else
							{
								bool flag42 = this.isMessage;
								if (flag42)
								{
									this.currMess = this.getCurrMessage();
									bool flag43 = this.currMess != null;
									if (flag43)
									{
										bool flag44 = this.currMess.type == 0;
										if (flag44)
										{
											MyVector myVector5 = new MyVector();
											myVector5.addElement(new Command(mResources.CLOSE, this, 8000, this.currMess));
											GameCanvas.menu.startAt(myVector5, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
											this.addMessageDetail(this.currMess);
										}
										else
										{
											bool flag45 = this.currMess.type == 1;
											if (flag45)
											{
												bool flag46 = this.currMess.playerId != global::Char.myCharz().charID && this.cSelected != -1;
												if (flag46)
												{
													Service.gI().clanDonate(this.currMess.id);
												}
											}
											else
											{
												bool flag47 = this.currMess.type == 2 && this.currMess.option != null;
												if (flag47)
												{
													bool flag48 = this.cSelected == 0;
													if (flag48)
													{
														Service.gI().joinClan(this.currMess.id, 1);
													}
													else
													{
														bool flag49 = this.cSelected == 1;
														if (flag49)
														{
															Service.gI().joinClan(this.currMess.id, 0);
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
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				this.cSelected = -1;
				this.selected = -1;
			}
		}
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0007DA04 File Offset: 0x0007BC04
	private void doFireMain()
	{
		bool flag = this.currentTabIndex == 0;
		if (flag)
		{
			this.setTypeMap();
		}
		bool flag2 = this.currentTabIndex == 1;
		if (flag2)
		{
			this.doFireInventory();
		}
		bool flag3 = this.currentTabIndex == 2;
		if (flag3)
		{
			this.doFireSkill();
		}
		bool flag4 = this.currentTabIndex == 3;
		if (flag4)
		{
			bool flag5 = this.mainTabName.Length == 4;
			if (flag5)
			{
				this.doFireTool();
			}
			else
			{
				this.doFireClanOption();
			}
		}
		bool flag6 = this.currentTabIndex == 4;
		if (flag6)
		{
			this.doFireTool();
		}
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x0007DAA0 File Offset: 0x0007BCA0
	private void doFireSkill()
	{
		bool flag = this.selected < 0;
		if (!flag)
		{
			bool flag2 = global::Char.myCharz().statusMe == 14;
			if (flag2)
			{
				GameCanvas.startOKDlg(mResources.can_not_do_when_die);
			}
			else
			{
				bool flag3 = this.selected == 0 || this.selected == 1 || this.selected == 2 || this.selected == 3 || this.selected == 4 || this.selected == 5;
				if (flag3)
				{
					long cTiemNang = global::Char.myCharz().cTiemNang;
					int cHPGoc = global::Char.myCharz().cHPGoc;
					int cMPGoc = global::Char.myCharz().cMPGoc;
					int cDamGoc = global::Char.myCharz().cDamGoc;
					int cDefGoc = global::Char.myCharz().cDefGoc;
					int cCriticalGoc = global::Char.myCharz().cCriticalGoc;
					int num = 1000;
					bool flag4 = this.selected == 0;
					if (flag4)
					{
						bool flag5 = cTiemNang < (long)(global::Char.myCharz().cHPGoc + num);
						if (flag5)
						{
							GameCanvas.startOKDlg(string.Concat(new object[]
							{
								mResources.not_enough_potential_point1,
								global::Char.myCharz().cTiemNang,
								mResources.not_enough_potential_point2,
								global::Char.myCharz().cHPGoc + num
							}), false);
							return;
						}
						bool flag6 = cTiemNang > (long)cHPGoc && cTiemNang < (long)(10 * (2 * (cHPGoc + num) + 180) / 2);
						if (flag6)
						{
							GameCanvas.startYesNoDlg(string.Concat(new object[]
							{
								mResources.use_potential_point_for1,
								cHPGoc + num,
								mResources.use_potential_point_for2,
								global::Char.myCharz().hpFrom1000TiemNang,
								mResources.for_HP
							}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
							return;
						}
						bool flag7 = cTiemNang >= (long)(10 * (2 * (cHPGoc + num) + 180) / 2) && cTiemNang < (long)(100 * (2 * (cHPGoc + num) + 1980) / 2);
						if (flag7)
						{
							MyVector myVector = new MyVector();
							myVector.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								global::Char.myCharz().hpFrom1000TiemNang,
								mResources.HP,
								"\n-",
								cHPGoc + num
							}), this, 9000, null));
							myVector.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(10 * global::Char.myCharz().hpFrom1000TiemNang),
								mResources.HP,
								"\n-",
								10 * (2 * (cHPGoc + num) + 180) / 2
							}), this, 9006, null));
							GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addSkillDetail2(this.selected);
						}
						bool flag8 = cTiemNang >= (long)(100 * (2 * (cHPGoc + num) + 1980) / 2);
						if (flag8)
						{
							MyVector myVector2 = new MyVector();
							myVector2.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								global::Char.myCharz().hpFrom1000TiemNang,
								mResources.HP,
								"\n-",
								cHPGoc + num
							}), this, 9000, null));
							myVector2.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(10 * global::Char.myCharz().hpFrom1000TiemNang),
								mResources.HP,
								"\n-",
								10 * (2 * (cHPGoc + num) + 180) / 2
							}), this, 9006, null));
							myVector2.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(100 * global::Char.myCharz().hpFrom1000TiemNang),
								mResources.HP,
								"\n-",
								100 * (2 * (cHPGoc + num) + 1980) / 2
							}), this, 9007, null));
							GameCanvas.menu.startAt(myVector2, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addSkillDetail2(this.selected);
						}
					}
					bool flag9 = this.selected == 1;
					if (flag9)
					{
						bool flag10 = global::Char.myCharz().cTiemNang < (long)(global::Char.myCharz().cMPGoc + num);
						if (flag10)
						{
							GameCanvas.startOKDlg(string.Concat(new object[]
							{
								mResources.not_enough_potential_point1,
								global::Char.myCharz().cTiemNang,
								mResources.not_enough_potential_point2,
								global::Char.myCharz().cMPGoc + num
							}), false);
							return;
						}
						bool flag11 = cTiemNang > (long)cMPGoc && cTiemNang < (long)(10 * (2 * (cMPGoc + num) + 180) / 2);
						if (flag11)
						{
							GameCanvas.startYesNoDlg(string.Concat(new object[]
							{
								mResources.use_potential_point_for1,
								cMPGoc + num,
								mResources.use_potential_point_for2,
								global::Char.myCharz().mpFrom1000TiemNang,
								mResources.for_KI
							}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
							return;
						}
						bool flag12 = cTiemNang >= (long)(10 * (2 * (cMPGoc + num) + 180) / 2) && cTiemNang < (long)(100 * (2 * (cMPGoc + num) + 1980) / 2);
						if (flag12)
						{
							MyVector myVector3 = new MyVector();
							myVector3.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								global::Char.myCharz().mpFrom1000TiemNang,
								mResources.KI,
								"\n-",
								cHPGoc + num
							}), this, 9000, null));
							myVector3.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(10 * global::Char.myCharz().mpFrom1000TiemNang),
								mResources.KI,
								"\n-",
								10 * (2 * (cHPGoc + num) + 180) / 2
							}), this, 9006, null));
							GameCanvas.menu.startAt(myVector3, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addSkillDetail2(this.selected);
						}
						bool flag13 = cTiemNang >= (long)(100 * (2 * (cMPGoc + num) + 1980) / 2);
						if (flag13)
						{
							MyVector myVector4 = new MyVector();
							myVector4.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								global::Char.myCharz().mpFrom1000TiemNang,
								mResources.KI,
								"\n-",
								cMPGoc + num
							}), this, 9000, null));
							myVector4.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(10 * global::Char.myCharz().mpFrom1000TiemNang),
								mResources.KI,
								"\n-",
								10 * (2 * (cMPGoc + num) + 180) / 2
							}), this, 9006, null));
							myVector4.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(100 * global::Char.myCharz().mpFrom1000TiemNang),
								mResources.KI,
								"\n-",
								100 * (2 * (cMPGoc + num) + 1980) / 2
							}), this, 9007, null));
							GameCanvas.menu.startAt(myVector4, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addSkillDetail2(this.selected);
						}
					}
					bool flag14 = this.selected == 2;
					if (flag14)
					{
						bool flag15 = global::Char.myCharz().cTiemNang < (long)(global::Char.myCharz().cDamGoc * (int)global::Char.myCharz().expForOneAdd);
						if (flag15)
						{
							GameCanvas.startOKDlg(string.Concat(new object[]
							{
								mResources.not_enough_potential_point1,
								global::Char.myCharz().cTiemNang,
								mResources.not_enough_potential_point2,
								cDamGoc * 100
							}), false);
							return;
						}
						bool flag16 = cTiemNang > (long)cDamGoc && cTiemNang < (long)(10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd);
						if (flag16)
						{
							GameCanvas.startYesNoDlg(string.Concat(new object[]
							{
								mResources.use_potential_point_for1,
								cDamGoc * 100,
								mResources.use_potential_point_for2,
								global::Char.myCharz().damFrom1000TiemNang,
								mResources.for_hit_point
							}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
							return;
						}
						bool flag17 = cTiemNang >= (long)(10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd) && cTiemNang < (long)(100 * (2 * cDamGoc + 99) / 2 * (int)global::Char.myCharz().expForOneAdd);
						if (flag17)
						{
							MyVector myVector5 = new MyVector();
							myVector5.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								global::Char.myCharz().damFrom1000TiemNang,
								"\n",
								mResources.hit_point,
								"\n-",
								cDamGoc * 100
							}), this, 9000, null));
							myVector5.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(10 * global::Char.myCharz().damFrom1000TiemNang),
								"\n",
								mResources.hit_point,
								"\n-",
								10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd
							}), this, 9006, null));
							GameCanvas.menu.startAt(myVector5, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addSkillDetail2(this.selected);
						}
						bool flag18 = cTiemNang >= (long)(100 * (2 * cDamGoc + 99) / 2 * (int)global::Char.myCharz().expForOneAdd);
						if (flag18)
						{
							MyVector myVector6 = new MyVector();
							myVector6.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								global::Char.myCharz().damFrom1000TiemNang,
								"\n",
								mResources.hit_point,
								"\n-",
								cDamGoc * 100
							}), this, 9000, null));
							myVector6.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(10 * global::Char.myCharz().damFrom1000TiemNang),
								"\n",
								mResources.hit_point,
								"\n-",
								10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd
							}), this, 9006, null));
							myVector6.addElement(new Command(string.Concat(new object[]
							{
								mResources.increase_upper,
								"\n",
								(int)(100 * global::Char.myCharz().damFrom1000TiemNang),
								"\n",
								mResources.hit_point,
								"\n-",
								100 * (2 * cDamGoc + 99) / 2 * (int)global::Char.myCharz().expForOneAdd
							}), this, 9007, null));
							GameCanvas.menu.startAt(myVector6, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addSkillDetail2(this.selected);
						}
					}
					bool flag19 = this.selected == 3;
					if (flag19)
					{
						bool flag20 = global::Char.myCharz().cTiemNang < (long)(50000 + global::Char.myCharz().cDefGoc * 1000);
						if (flag20)
						{
							GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + NinjaUtil.getMoneys(global::Char.myCharz().cTiemNang) + mResources.not_enough_potential_point2 + NinjaUtil.getMoneys((long)(50000 + global::Char.myCharz().cDefGoc * 1000)), false);
						}
						else
						{
							int num2 = 2 * (cDefGoc + 5) / 2 * 100000;
							GameCanvas.startYesNoDlg(string.Concat(new object[]
							{
								mResources.use_potential_point_for1,
								NinjaUtil.getMoneys((long)num2),
								mResources.use_potential_point_for2,
								global::Char.myCharz().defFrom1000TiemNang,
								mResources.for_armor
							}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
						}
					}
					else
					{
						bool flag21 = this.selected == 4;
						if (flag21)
						{
							long num3 = 50000000L;
							for (int i = 0; i < global::Char.myCharz().cCriticalGoc; i++)
							{
								num3 *= 5L;
							}
							bool flag22 = global::Char.myCharz().cTiemNang < num3;
							if (flag22)
							{
								GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + NinjaUtil.getMoneys(global::Char.myCharz().cTiemNang) + mResources.not_enough_potential_point2 + NinjaUtil.getMoneys(num3), false);
							}
							else
							{
								GameCanvas.startYesNoDlg(string.Concat(new object[]
								{
									mResources.use_potential_point_for1,
									NinjaUtil.getMoneys(num3),
									mResources.use_potential_point_for2,
									global::Char.myCharz().criticalFrom1000Tiemnang,
									mResources.for_crit
								}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
							}
						}
						else
						{
							bool flag23 = this.selected == 5;
							if (flag23)
							{
								Service.gI().speacialSkill(0);
							}
						}
					}
				}
				else
				{
					int num4 = this.selected - 6;
					SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[num4];
					Skill skill = global::Char.myCharz().getSkill(skillTemplate);
					Skill skill2 = null;
					MyVector myVector7 = new MyVector();
					bool flag24 = skill != null;
					if (flag24)
					{
						bool flag25 = skill.point == skillTemplate.maxPoint;
						if (flag25)
						{
							myVector7.addElement(new Command(mResources.make_shortcut, this, 9003, skill.template));
							myVector7.addElement(new Command(mResources.CLOSE, 2));
						}
						else
						{
							skill2 = skillTemplate.skills[skill.point];
							myVector7.addElement(new Command(mResources.UPGRADE, this, 9002, skill2));
							myVector7.addElement(new Command(mResources.make_shortcut, this, 9003, skill.template));
						}
					}
					else
					{
						skill2 = skillTemplate.skills[0];
						myVector7.addElement(new Command(mResources.learn, this, 9004, skill2));
					}
					GameCanvas.menu.startAt(myVector7, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
					this.addSkillDetail(skillTemplate, skill, skill2);
				}
			}
		}
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x0007EB20 File Offset: 0x0007CD20
	private void addLogMessage(InfoItem info)
	{
		string text = "|0|1|" + info.charInfo.cName;
		text += "\n";
		text += "\n--";
		text = text + "\n|5|" + Res.split(info.s, "|", 0)[2];
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.charInfo = info.charInfo;
		this.currItem = null;
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x0007EBA8 File Offset: 0x0007CDA8
	private void addSkillDetail2(int type)
	{
		string text = string.Empty;
		int num = 0;
		bool flag = this.selected == 0;
		if (flag)
		{
			num = global::Char.myCharz().cHPGoc + 1000;
		}
		bool flag2 = this.selected == 1;
		if (flag2)
		{
			num = global::Char.myCharz().cMPGoc + 1000;
		}
		bool flag3 = this.selected == 2;
		if (flag3)
		{
			num = global::Char.myCharz().cDamGoc * (int)global::Char.myCharz().expForOneAdd;
		}
		string text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"|5|2|",
			mResources.USE,
			" ",
			num,
			" ",
			mResources.potential
		});
		bool flag4 = type == 0;
		if (flag4)
		{
			text = text + "\n|5|2|" + mResources.to_gain_20hp;
		}
		bool flag5 = type == 1;
		if (flag5)
		{
			text = text + "\n|5|2|" + mResources.to_gain_20mp;
		}
		bool flag6 = type == 2;
		if (flag6)
		{
			text = text + "\n|5|2|" + mResources.to_gain_1pow;
		}
		this.currItem = null;
		this.partID = null;
		this.charInfo = null;
		this.idIcon = -1;
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x00003A0D File Offset: 0x00001C0D
	private void doFireClanIcon()
	{
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x0007ECF8 File Offset: 0x0007CEF8
	private void doFireMap()
	{
		bool flag = Panel.imgMap != null;
		if (flag)
		{
			Panel.imgMap.texture = null;
			Panel.imgMap = null;
		}
		TileMap.lastPlanetId = -1;
		mSystem.gcc();
		SmallImage.loadBigRMS();
		this.setTypeMain();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x0007ED50 File Offset: 0x0007CF50
	private void doFireZone()
	{
		bool flag = this.selected != -1;
		if (flag)
		{
			Res.outz("FIRE ZONE");
			this.isChangeZone = true;
			GameCanvas.panel.hide();
		}
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x0007ED90 File Offset: 0x0007CF90
	public void updateRequest(int recieve, int maxCap)
	{
		this.cp.says[this.cp.says.Length - 1] = string.Concat(new object[]
		{
			mResources.received,
			" ",
			recieve,
			"/",
			maxCap
		});
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x0007EDF0 File Offset: 0x0007CFF0
	private void doFireBox()
	{
		bool flag = this.selected < 0;
		if (!flag)
		{
			this.currItem = null;
			MyVector myVector = new MyVector();
			bool flag2 = this.currentTabIndex == 0 && !this.Equals(GameCanvas.panel2);
			if (flag2)
			{
				bool flag3 = this.selected == 0;
				if (flag3)
				{
					this.setNewSelected(global::Char.myCharz().arrItemBox.Length, false);
				}
				else
				{
					sbyte b = (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected);
					Item item = global::Char.myCharz().arrItemBox[(int)b];
					bool flag4 = item != null;
					if (flag4)
					{
						bool flag5 = this.isBoxClan;
						if (flag5)
						{
							myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
							myVector.addElement(new Command(mResources.USE, this, 2010, item));
						}
						else
						{
							bool flag6 = item.isTypeBody();
							if (flag6)
							{
								myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
							}
							else
							{
								myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
							}
						}
						this.currItem = item;
					}
				}
			}
			bool flag7 = this.currentTabIndex == 1 || this.Equals(GameCanvas.panel2);
			if (flag7)
			{
				bool flag8 = this.selected == 0;
				if (flag8)
				{
					this.setNewSelected(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length, true);
				}
				else
				{
					Item[] arrItemBody = global::Char.myCharz().arrItemBody;
					bool flag9 = !this.GetInventorySelect_isbody(this.selected, this.newSelected, arrItemBody);
					if (flag9)
					{
						sbyte b2 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, arrItemBody);
						Item item2 = global::Char.myCharz().arrItemBag[(int)b2];
						bool flag10 = item2 != null;
						if (flag10)
						{
							myVector.addElement(new Command(mResources.move_to_chest, this, 1001, item2));
							bool flag11 = item2.isTypeBody();
							if (flag11)
							{
								myVector.addElement(new Command(mResources.USE, this, 2000, item2));
							}
							else
							{
								myVector.addElement(new Command(mResources.USE, this, 2001, item2));
							}
							this.currItem = item2;
						}
					}
					else
					{
						Item item3 = global::Char.myCharz().arrItemBody[this.GetInventorySelect_body(this.selected, this.newSelected)];
						bool flag12 = item3 != null;
						if (flag12)
						{
							myVector.addElement(new Command(mResources.move_to_chest2, this, 1002, item3));
							this.currItem = item3;
						}
					}
				}
			}
			bool flag13 = this.currItem != null;
			if (flag13)
			{
				global::Char.myCharz().setPartTemp(this.currItem.headTemp, this.currItem.bodyTemp, this.currItem.legTemp, this.currItem.bagTemp);
				bool flag14 = this.isBoxClan;
				if (flag14)
				{
					myVector.addElement(new Command(mResources.MOVEOUT, this, 2011, this.currItem));
				}
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
		}
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x0007F160 File Offset: 0x0007D360
	public void itemRequest(sbyte itemAction, string info, sbyte where, sbyte index)
	{
		GameCanvas.endDlg();
		ItemObject itemObject = new ItemObject();
		itemObject.type = (int)itemAction;
		itemObject.id = (int)index;
		itemObject.where = (int)where;
		GameCanvas.startYesNoDlg(info, new Command(mResources.YES, this, 2004, itemObject), new Command(mResources.NO, this, 4005, null));
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x0007F1BC File Offset: 0x0007D3BC
	public void saleRequest(sbyte type, string info, short id)
	{
		ItemObject itemObject = new ItemObject();
		itemObject.type = (int)type;
		itemObject.id = (int)id;
		GameCanvas.startYesNoDlg(info, new Command(mResources.YES, this, 3003, itemObject), new Command(mResources.NO, this, 4005, null));
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x0007F208 File Offset: 0x0007D408
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 9999;
		if (flag)
		{
			TopInfo topInfo = (TopInfo)p;
			Service.gI().sendThachDau(topInfo.pId);
		}
		bool flag2 = idAction == 170391;
		if (flag2)
		{
			Rms.clearAll();
			bool flag3 = mGraphics.zoomLevel > 1;
			if (flag3)
			{
				Rms.saveRMSInt("levelScreenKN", 1);
			}
			else
			{
				Rms.saveRMSInt("levelScreenKN", 0);
			}
			GameMidlet.instance.exit();
		}
		bool flag4 = idAction == 6001;
		if (flag4)
		{
			Item item = (Item)p;
			item.isSelect = false;
			GameCanvas.panel.vItemCombine.removeElement(item);
			bool flag5 = GameCanvas.panel.currentTabIndex == 0;
			if (flag5)
			{
				GameCanvas.panel.setTabCombine();
			}
			bool isDapDo = VuDang.isDapDo;
			if (isDapDo)
			{
				VuDang.doDeDap = null;
			}
		}
		bool flag6 = idAction == 6000;
		if (flag6)
		{
			Item item2 = (Item)p;
			for (int i = 0; i < GameCanvas.panel.vItemCombine.size(); i++)
			{
				Item item3 = (Item)GameCanvas.panel.vItemCombine.elementAt(i);
				bool flag7 = item3.template.id == item2.template.id;
				if (flag7)
				{
					GameCanvas.startOKDlg(mResources.already_has_item);
					return;
				}
			}
			item2.isSelect = true;
			GameCanvas.panel.vItemCombine.addElement(item2);
			bool flag8 = GameCanvas.panel.currentTabIndex == 0;
			if (flag8)
			{
				GameCanvas.panel.setTabCombine();
			}
			bool isDapDo2 = VuDang.isDapDo;
			if (isDapDo2)
			{
				VuDang.doDeDap = item2;
			}
		}
		bool flag9 = idAction == 7000;
		if (flag9)
		{
			bool flag10 = this.isLock;
			if (flag10)
			{
				GameCanvas.startOKDlg(mResources.unlock_item_to_trade);
				return;
			}
			Item item4 = (Item)p;
			for (int j = 0; j < GameCanvas.panel.vMyGD.size(); j++)
			{
				Item item5 = (Item)GameCanvas.panel.vMyGD.elementAt(j);
				bool flag11 = item5.indexUI == item4.indexUI;
				if (flag11)
				{
					GameCanvas.startOKDlg(mResources.already_has_item);
					return;
				}
			}
			bool flag12 = item4.quantity > 1;
			if (flag12)
			{
				this.putQuantily();
				return;
			}
			item4.isSelect = true;
			Item item6 = new Item();
			item6.template = item4.template;
			item6.itemOption = item4.itemOption;
			item6.indexUI = item4.indexUI;
			GameCanvas.panel.vMyGD.addElement(item6);
			Service.gI().giaodich(2, -1, (sbyte)item6.indexUI, item6.quantity);
		}
		bool flag13 = idAction == 7001;
		if (flag13)
		{
			Item item7 = (Item)p;
			item7.isSelect = false;
			GameCanvas.panel.vMyGD.removeElement(item7);
			bool flag14 = GameCanvas.panel.currentTabIndex == 1;
			if (flag14)
			{
				GameCanvas.panel.setTabGiaoDich(true);
			}
			Service.gI().giaodich(4, -1, (sbyte)item7.indexUI, -1);
		}
		bool flag15 = idAction == 7002;
		if (flag15)
		{
			this.isAccept = true;
			GameCanvas.endDlg();
			Service.gI().giaodich(7, -1, -1, -1);
			this.hide();
		}
		bool flag16 = idAction == 8003;
		if (flag16)
		{
			InfoItem infoItem = (InfoItem)p;
			Service.gI().friend(1, infoItem.charInfo.charID);
			bool flag17 = this.type != 8;
			if (flag17)
			{
			}
		}
		bool flag18 = idAction == 8002;
		if (flag18)
		{
			InfoItem infoItem2 = (InfoItem)p;
			Service.gI().friend(2, infoItem2.charInfo.charID);
		}
		bool flag19 = idAction == 8004;
		if (flag19)
		{
			InfoItem infoItem3 = (InfoItem)p;
			Service.gI().gotoPlayer(infoItem3.charInfo.charID);
		}
		bool flag20 = idAction == 8007;
		if (flag20)
		{
			string text = (string)p;
			VuDang.tennoitaicanmo = text.Substring(0, text.IndexOf('%') - 4);
			GameCanvas.panel.hide();
			this.VuDangChatTextField("Nhập Chỉ Số Nội Tại", "Chỉ Được Nhập Số (% Nội Tại Cần Mở)");
		}
		bool flag21 = idAction == 8009;
		if (flag21)
		{
			VuDang.AddItem(this.currItem);
		}
		bool flag22 = idAction == 8010;
		if (flag22)
		{
			this.VuDangChatTextField("Nhập Số Sao Cần Đập", "Chỉ Được Nhập Số");
		}
		bool flag23 = idAction == 3142;
		if (flag23)
		{
			VuDang.addSet1(this.currItem);
		}
		bool flag24 = idAction == 3143;
		if (flag24)
		{
			VuDang.addSet2(this.currItem);
		}
		bool flag25 = idAction == 8001;
		if (flag25)
		{
			Res.outz("chat player");
			InfoItem infoItem4 = (InfoItem)p;
			bool flag26 = this.chatTField == null;
			if (flag26)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = GameCanvas.panel;
			}
			this.chatTField.strChat = mResources.chat_player;
			this.chatTField.tfChat.name = mResources.chat_with + " " + infoItem4.charInfo.cName;
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.isFocus = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			bool isWindowsPhone = Main.isWindowsPhone;
			if (isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			bool flag27 = !Main.isPC;
			if (flag27)
			{
				this.chatTField.startChat2(this, string.Empty);
			}
		}
		bool flag28 = idAction == 1000;
		if (flag28)
		{
			Service.gI().getItem(Panel.BOX_BAG, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected));
		}
		bool flag29 = idAction == 1001;
		if (flag29)
		{
			sbyte id = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			Service.gI().getItem(Panel.BAG_BOX, id);
		}
		bool flag30 = idAction == 1003;
		if (flag30)
		{
			this.hide();
		}
		bool flag31 = idAction == 1002;
		if (flag31)
		{
			Service.gI().getItem(Panel.BODY_BOX, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected));
		}
		bool flag32 = idAction == 2011;
		if (flag32)
		{
			Service.gI().useItem(1, 2, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected), -1);
		}
		bool flag33 = idAction == 2010;
		if (flag33)
		{
			Service.gI().useItem(0, 2, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected), -1);
			Item item8 = (Item)p;
			bool flag34 = item8 != null && (item8.template.id == 193 || item8.template.id == 194);
			if (flag34)
			{
				GameCanvas.panel.hide();
			}
		}
		bool flag35 = idAction == 2000;
		if (flag35)
		{
			Item[] arrItemBody = global::Char.myCharz().arrItemBody;
			sbyte id2 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, arrItemBody);
			Service.gI().getItem(Panel.BAG_BODY, id2);
		}
		bool flag36 = idAction == 2001;
		if (flag36)
		{
			Res.outz("use item");
			Item item9 = (Item)p;
			bool inventorySelect_isbody = this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			sbyte index = inventorySelect_isbody ? ((sbyte)this.GetInventorySelect_body(this.selected, this.newSelected)) : ((sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody));
			Service.gI().useItem(0, (!inventorySelect_isbody) ? 1 : 0, index, -1);
			bool flag37 = item9.template.id == 193 || item9.template.id == 194;
			if (flag37)
			{
				GameCanvas.panel.hide();
			}
		}
		bool flag38 = idAction == 2002;
		if (flag38)
		{
			Service.gI().getItem(Panel.BODY_BAG, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected));
		}
		bool flag39 = idAction == 2003;
		if (flag39)
		{
			Res.outz("remove item");
			bool inventorySelect_isbody2 = this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			sbyte index2 = inventorySelect_isbody2 ? ((sbyte)this.GetInventorySelect_body(this.selected, this.newSelected)) : ((sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody));
			Service.gI().useItem(1, (!inventorySelect_isbody2) ? 1 : 0, index2, -1);
		}
		bool flag40 = idAction == 2004;
		if (flag40)
		{
			GameCanvas.endDlg();
			ItemObject itemObject = (ItemObject)p;
			sbyte where = (sbyte)itemObject.where;
			sbyte index3 = (sbyte)itemObject.id;
			Service.gI().useItem((itemObject.type != 0) ? 2 : 3, where, index3, -1);
		}
		bool flag41 = idAction == 2005;
		if (flag41)
		{
			sbyte id3 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			Service.gI().getItem(Panel.BAG_PET, id3);
		}
		bool flag42 = idAction == 2006;
		if (flag42)
		{
			Item[] arrItemBody2 = global::Char.myPetz().arrItemBody;
			sbyte id4 = (sbyte)this.selected;
			Service.gI().getItem(Panel.PET_BAG, id4);
		}
		bool flag43 = idAction == 30001;
		if (flag43)
		{
			Res.outz("nhan do");
			Service.gI().buyItem(0, this.selected, 0);
		}
		bool flag44 = idAction == 30002;
		if (flag44)
		{
			Res.outz("xoa do");
			Service.gI().buyItem(1, this.selected, 0);
		}
		bool flag45 = idAction == 30003;
		if (flag45)
		{
			Res.outz("nhan tat");
			Service.gI().buyItem(2, this.selected, 0);
		}
		bool flag46 = idAction == 3000;
		if (flag46)
		{
			Res.outz("mua do");
			Item item10 = (Item)p;
			Service.gI().buyItem(0, (int)item10.template.id, 0);
		}
		bool flag47 = idAction == 3001;
		if (flag47)
		{
			Item item11 = (Item)p;
			GameCanvas.msgdlg.pleasewait();
			Service.gI().buyItem(1, (int)item11.template.id, 0);
		}
		bool flag48 = idAction == 3002;
		if (flag48)
		{
			GameCanvas.endDlg();
			bool inventorySelect_isbody3 = this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			sbyte id5 = inventorySelect_isbody3 ? ((sbyte)this.GetInventorySelect_body(this.selected, this.newSelected)) : ((sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody));
			Service.gI().saleItem(0, (!inventorySelect_isbody3) ? 1 : 0, (short)id5);
		}
		bool flag49 = idAction == 3003;
		if (flag49)
		{
			GameCanvas.endDlg();
			ItemObject itemObject2 = (ItemObject)p;
			Service.gI().saleItem(1, (sbyte)itemObject2.type, (short)itemObject2.id);
		}
		bool flag50 = idAction == 3004;
		if (flag50)
		{
			Item item12 = (Item)p;
			Service.gI().buyItem(3, (int)item12.template.id, 0);
		}
		bool flag51 = idAction == 3005;
		if (flag51)
		{
			Res.outz("mua do");
			Item item13 = (Item)p;
			Service.gI().buyItem(3, (int)item13.template.id, 0);
		}
		bool flag52 = idAction == 4000;
		if (flag52)
		{
			Clan clan = (Clan)p;
			bool flag53 = clan != null;
			if (flag53)
			{
				GameCanvas.endDlg();
				Service.gI().clanMessage(2, null, clan.ID);
			}
		}
		bool flag54 = idAction == 4001;
		if (flag54)
		{
			Clan clan2 = (Clan)p;
			bool flag55 = clan2 != null;
			if (flag55)
			{
				InfoDlg.showWait();
				this.clanReport = mResources.PLEASEWAIT;
				Service.gI().clanMember(clan2.ID);
			}
		}
		bool flag56 = idAction == 4005;
		if (flag56)
		{
			GameCanvas.endDlg();
		}
		bool flag57 = idAction == 4007;
		if (flag57)
		{
			GameCanvas.endDlg();
		}
		bool flag58 = idAction == 4006;
		if (flag58)
		{
			ClanMessage clanMessage = (ClanMessage)p;
			Service.gI().clanDonate(clanMessage.id);
		}
		bool flag59 = idAction == 5001;
		if (flag59)
		{
			Member member = (Member)p;
			Service.gI().clanRemote(member.ID, 0);
		}
		bool flag60 = idAction == 5002;
		if (flag60)
		{
			Member member2 = (Member)p;
			Service.gI().clanRemote(member2.ID, 1);
		}
		bool flag61 = idAction == 5003;
		if (flag61)
		{
			Member member3 = (Member)p;
			Service.gI().clanRemote(member3.ID, 2);
		}
		bool flag62 = idAction == 5004;
		if (flag62)
		{
			Member member4 = (Member)p;
			Service.gI().clanRemote(member4.ID, -1);
		}
		bool flag63 = idAction == 9000;
		if (flag63)
		{
			Service.gI().upPotential(this.selected, 1);
			GameCanvas.endDlg();
			InfoDlg.showWait();
		}
		bool flag64 = idAction == 9006;
		if (flag64)
		{
			Service.gI().upPotential(this.selected, 10);
			GameCanvas.endDlg();
			InfoDlg.showWait();
		}
		bool flag65 = idAction == 9007;
		if (flag65)
		{
			Service.gI().upPotential(this.selected, 100);
			GameCanvas.endDlg();
			InfoDlg.showWait();
		}
		bool flag66 = idAction == 9002;
		if (flag66)
		{
			Skill skill = (Skill)p;
			GameCanvas.startOKDlg(string.Concat(new object[]
			{
				mResources.can_buy_from_Uron1,
				skill.powRequire,
				mResources.can_buy_from_Uron2,
				skill.moreInfo,
				mResources.can_buy_from_Uron3
			}));
		}
		bool flag67 = idAction == 9003;
		if (flag67)
		{
			bool flag68 = GameCanvas.isTouch && !Main.isPC;
			if (flag68)
			{
				GameScr.gI().doSetOnScreenSkill((SkillTemplate)p);
			}
			else
			{
				GameScr.gI().doSetKeySkill((SkillTemplate)p);
			}
		}
		bool flag69 = idAction == 9004;
		if (flag69)
		{
			Skill skill2 = (Skill)p;
			GameCanvas.startOKDlg(string.Concat(new object[]
			{
				mResources.can_buy_from_Uron1,
				skill2.powRequire,
				mResources.can_buy_from_Uron2,
				skill2.moreInfo,
				mResources.can_buy_from_Uron3
			}));
		}
		bool flag70 = idAction == 10000;
		if (flag70)
		{
			InfoItem infoItem5 = (InfoItem)p;
			Service.gI().enemy(1, infoItem5.charInfo.charID);
			GameCanvas.panel.hideNow();
		}
		bool flag71 = idAction == 10001;
		if (flag71)
		{
			InfoItem infoItem6 = (InfoItem)p;
			Service.gI().enemy(2, infoItem6.charInfo.charID);
			InfoDlg.showWait();
		}
		bool flag72 = idAction == 10021;
		if (flag72)
		{
		}
		bool flag73 = idAction == 10012;
		if (flag73)
		{
			bool flag74 = this.chatTField == null;
			if (flag74)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = ((GameCanvas.panel2 != null) ? GameCanvas.panel2 : GameCanvas.panel);
			}
			bool flag75 = this.currItem.quantity == 1;
			if (flag75)
			{
				this.chatTField.strChat = mResources.kiguiXuchat;
				this.chatTField.tfChat.name = mResources.input_money;
			}
			else
			{
				this.chatTField.strChat = mResources.input_quantity + " ";
				this.chatTField.tfChat.name = mResources.input_quantity;
			}
			this.chatTField.tfChat.setMaxTextLenght(9);
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				this.chatTField.tfChat.doChangeToTextBox();
			}
			bool isWindowsPhone2 = Main.isWindowsPhone;
			if (isWindowsPhone2)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			bool flag76 = !Main.isPC;
			if (flag76)
			{
				this.chatTField.startChat2(this, string.Empty);
			}
		}
		bool flag77 = idAction == 10013;
		if (flag77)
		{
			bool flag78 = this.chatTField == null;
			if (flag78)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = ((GameCanvas.panel2 != null) ? GameCanvas.panel2 : GameCanvas.panel);
			}
			bool flag79 = this.currItem.quantity == 1;
			if (flag79)
			{
				this.chatTField.strChat = mResources.kiguiLuongchat;
				this.chatTField.tfChat.name = mResources.input_money;
			}
			else
			{
				this.chatTField.strChat = mResources.input_quantity + "  ";
				this.chatTField.tfChat.name = mResources.input_quantity;
			}
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			bool isTouch2 = GameCanvas.isTouch;
			if (isTouch2)
			{
				this.chatTField.tfChat.doChangeToTextBox();
			}
			bool isWindowsPhone3 = Main.isWindowsPhone;
			if (isWindowsPhone3)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			bool flag80 = !Main.isPC;
			if (flag80)
			{
				this.chatTField.startChat2(this, string.Empty);
			}
		}
		bool flag81 = idAction == 10014;
		if (flag81)
		{
			Item item14 = (Item)p;
			Service.gI().kigui(1, item14.itemId, -1, -1, -1);
			InfoDlg.showWait();
		}
		bool flag82 = idAction == 10015;
		if (flag82)
		{
			Item item15 = (Item)p;
			Service.gI().kigui(2, item15.itemId, -1, -1, -1);
			InfoDlg.showWait();
		}
		bool flag83 = idAction == 10016;
		if (flag83)
		{
			Item item16 = (Item)p;
			Service.gI().kigui(3, item16.itemId, 0, item16.buyCoin, -1);
			InfoDlg.showWait();
		}
		bool flag84 = idAction == 10017;
		if (flag84)
		{
			Item item17 = (Item)p;
			Service.gI().kigui(3, item17.itemId, 1, item17.buyGold, -1);
			InfoDlg.showWait();
		}
		bool flag85 = idAction == 10018;
		if (flag85)
		{
			Item item18 = (Item)p;
			Service.gI().kigui(5, item18.itemId, -1, -1, -1);
			InfoDlg.showWait();
		}
		bool flag86 = idAction == 10019;
		if (flag86)
		{
			Session_ME.gI().close();
			Rms.saveRMSString("acc", string.Empty);
			Rms.saveRMSString("pass", string.Empty);
			GameCanvas.loginScr.tfPass.setText(string.Empty);
			GameCanvas.loginScr.tfUser.setText(string.Empty);
			GameCanvas.loginScr.isLogin2 = false;
			GameCanvas.loginScr.switchToMe();
			GameCanvas.endDlg();
			this.hide();
		}
		bool flag87 = idAction == 10020;
		if (flag87)
		{
			GameCanvas.endDlg();
		}
		bool flag88 = idAction == 10030;
		if (flag88)
		{
			Service.gI().getFlag(1, (sbyte)this.selected);
			GameCanvas.panel.hideNow();
		}
		bool flag89 = idAction == 10031;
		if (flag89)
		{
			Session_ME.gI().close();
		}
		bool flag90 = idAction == 11000;
		if (flag90)
		{
			Service.gI().kigui(0, this.currItem.itemId, 1, this.currItem.buyRuby, 1);
			GameCanvas.endDlg();
		}
		bool flag91 = idAction == 11001;
		if (flag91)
		{
			Service.gI().kigui(0, this.currItem.itemId, 1, this.currItem.buyRuby, this.currItem.quantilyToBuy);
			GameCanvas.endDlg();
		}
		bool flag92 = idAction == 11002;
		if (flag92)
		{
			this.chatTField.isShow = false;
			GameCanvas.endDlg();
		}
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x000807C0 File Offset: 0x0007E9C0
	public void onChatFromMe(string text, string to)
	{
		bool flag = this.chatTField.tfChat.getText() == null || this.chatTField.tfChat.getText().Equals(string.Empty) || text.Equals(string.Empty) || text == null;
		if (flag)
		{
			this.chatTField.isShow = false;
		}
		else
		{
			bool flag2 = this.chatTField.strChat.Equals(mResources.input_clan_name);
			if (flag2)
			{
				InfoDlg.showWait();
				this.chatTField.isShow = false;
				Service.gI().searchClan(text);
			}
			else
			{
				bool flag3 = this.chatTField.strChat.Equals(mResources.chat_clan);
				if (flag3)
				{
					InfoDlg.showWait();
					this.chatTField.isShow = false;
					Service.gI().clanMessage(0, text, -1);
				}
				else
				{
					bool flag4 = this.chatTField.strChat.Equals(mResources.input_clan_name_to_create);
					if (flag4)
					{
						bool flag5 = this.chatTField.tfChat.getText() == string.Empty;
						if (flag5)
						{
							GameScr.info1.addInfo(mResources.clan_name_blank, 0);
							return;
						}
						bool flag6 = this.tabIcon == null;
						if (flag6)
						{
							this.tabIcon = new TabClanIcon();
						}
						this.tabIcon.text = this.chatTField.tfChat.getText();
						this.tabIcon.show(false);
						this.chatTField.isShow = false;
					}
					else
					{
						bool flag7 = this.chatTField.strChat.Equals(mResources.input_clan_slogan);
						if (flag7)
						{
							bool flag8 = this.chatTField.tfChat.getText() == string.Empty;
							if (flag8)
							{
								GameScr.info1.addInfo(mResources.clan_slogan_blank, 0);
								return;
							}
							Service.gI().getClan(4, (sbyte)global::Char.myCharz().clan.imgID, this.chatTField.tfChat.getText());
							this.chatTField.isShow = false;
						}
						else
						{
							bool flag9 = this.chatTField.strChat.Equals(mResources.input_Inventory_Pass);
							if (flag9)
							{
								try
								{
									int lockInventory = int.Parse(this.chatTField.tfChat.getText());
									bool flag10 = this.chatTField.tfChat.getText().Length != 6 || this.chatTField.tfChat.getText().Equals(string.Empty);
									if (flag10)
									{
										GameCanvas.startOKDlg(mResources.input_Inventory_Pass_wrong);
										return;
									}
									Service.gI().setLockInventory(lockInventory);
									this.chatTField.isShow = false;
									this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
									this.hide();
								}
								catch (Exception)
								{
									GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
								}
							}
							else
							{
								bool flag11 = this.chatTField.strChat.Equals(mResources.world_channel_5_luong);
								if (flag11)
								{
									bool flag12 = !this.chatTField.tfChat.getText().Equals(string.Empty);
									if (flag12)
									{
										Service.gI().chatGlobal(this.chatTField.tfChat.getText());
										this.chatTField.isShow = false;
										this.hide();
									}
								}
								else
								{
									bool flag13 = this.chatTField.strChat.Equals(mResources.chat_player);
									if (flag13)
									{
										this.chatTField.isShow = false;
										InfoItem infoItem = null;
										bool flag14 = this.type == 8;
										if (flag14)
										{
											infoItem = (InfoItem)this.logChat.elementAt(this.currInfoItem);
										}
										else
										{
											bool flag15 = this.type == 11;
											if (flag15)
											{
												infoItem = (InfoItem)this.vFriend.elementAt(this.currInfoItem);
											}
										}
										bool flag16 = infoItem.charInfo.charID != global::Char.myCharz().charID;
										if (flag16)
										{
											Service.gI().chatPlayer(text, infoItem.charInfo.charID);
										}
									}
									else
									{
										bool flag17 = this.chatTField.strChat.Equals(mResources.input_quantity_to_trade);
										if (flag17)
										{
											int num = 0;
											try
											{
												num = int.Parse(this.chatTField.tfChat.getText());
											}
											catch (Exception)
											{
												GameCanvas.startOKDlg(mResources.input_quantity_wrong);
												this.chatTField.isShow = false;
												this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
												return;
											}
											bool flag18 = num <= 0 || num > this.currItem.quantity;
											if (flag18)
											{
												GameCanvas.startOKDlg(mResources.input_quantity_wrong);
												this.chatTField.isShow = false;
												this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
												return;
											}
											this.currItem.isSelect = true;
											Item item = new Item();
											item.template = this.currItem.template;
											item.quantity = num;
											item.indexUI = this.currItem.indexUI;
											item.itemOption = this.currItem.itemOption;
											GameCanvas.panel.vMyGD.addElement(item);
											Service.gI().giaodich(2, -1, (sbyte)item.indexUI, item.quantity);
											this.chatTField.isShow = false;
											this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
										}
										else
										{
											bool flag19 = this.chatTField.strChat == mResources.input_money_to_trade;
											if (flag19)
											{
												int num2 = 0;
												try
												{
													num2 = int.Parse(this.chatTField.tfChat.getText());
												}
												catch (Exception)
												{
													GameCanvas.startOKDlg(mResources.input_money_wrong);
													this.chatTField.isShow = false;
													this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
													return;
												}
												bool flag20 = (long)num2 > global::Char.myCharz().xu;
												if (flag20)
												{
													GameCanvas.startOKDlg(mResources.not_enough_money);
													this.chatTField.isShow = false;
													this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
												}
												else
												{
													this.moneyGD = num2;
													Service.gI().giaodich(2, -1, -1, num2);
													this.chatTField.isShow = false;
													this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
												}
											}
											else
											{
												bool flag21 = this.chatTField.strChat.Equals(mResources.kiguiXuchat);
												if (flag21)
												{
													Service.gI().kigui(0, this.currItem.itemId, 0, int.Parse(this.chatTField.tfChat.getText()), 1);
													this.chatTField.isShow = false;
												}
												else
												{
													bool flag22 = this.chatTField.strChat.Equals(mResources.kiguiXuchat + " ");
													if (flag22)
													{
														Service.gI().kigui(0, this.currItem.itemId, 0, int.Parse(this.chatTField.tfChat.getText()), this.currItem.quantilyToBuy);
														this.chatTField.isShow = false;
													}
													else
													{
														bool flag23 = this.chatTField.strChat.Equals(mResources.kiguiLuongchat);
														if (flag23)
														{
															this.doNotiRuby(0);
															this.chatTField.isShow = false;
														}
														else
														{
															bool flag24 = this.chatTField.strChat.Equals(mResources.kiguiLuongchat + "  ");
															if (flag24)
															{
																this.doNotiRuby(1);
																this.chatTField.isShow = false;
															}
															else
															{
																bool flag25 = this.chatTField.strChat.Equals(mResources.input_quantity + " ");
																if (flag25)
																{
																	this.currItem.quantilyToBuy = int.Parse(this.chatTField.tfChat.getText());
																	bool flag26 = this.currItem.quantilyToBuy > this.currItem.quantity;
																	if (flag26)
																	{
																		GameCanvas.startOKDlg(mResources.input_quantity_wrong);
																		return;
																	}
																	this.isKiguiXu = true;
																	this.chatTField.isShow = false;
																}
																else
																{
																	bool flag27 = this.chatTField.strChat.Equals(mResources.input_quantity + "  ");
																	if (flag27)
																	{
																		this.currItem.quantilyToBuy = int.Parse(this.chatTField.tfChat.getText());
																		bool flag28 = this.currItem.quantilyToBuy > this.currItem.quantity;
																		if (flag28)
																		{
																			GameCanvas.startOKDlg(mResources.input_quantity_wrong);
																			return;
																		}
																		this.isKiguiLuong = true;
																		this.chatTField.isShow = false;
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
				}
			}
		}
		bool flag29 = this.chatTField.strChat == "Nhập Số Sao Cần Đập";
		if (flag29)
		{
			VuDang.soSaoCanDap = int.Parse(text);
			GameScr.info1.addInfo(string.Concat(new object[]
			{
				"Đập ",
				VuDang.doDeDap.template.name,
				" Đến ",
				VuDang.soSaoCanDap,
				" Sao"
			}), 0);
			this.chatTField.isShow = false;
		}
		bool flag30 = this.chatTField.strChat == "Nhập Số Item Cần Mua";
		if (flag30)
		{
			VuDang.countBuyItem = int.Parse(text);
			GameScr.info1.addInfo(string.Concat(new object[]
			{
				"Auto Mua ",
				VuDang.countBuyItem,
				" Cái ",
				VuDang.itemBuy.template.name
			}), 0);
			this.chatTField.isShow = false;
		}
		bool flag31 = this.chatTField.strChat == "Nhập Chỉ Số Nội Tại";
		if (flag31)
		{
			VuDang.ntMin = int.Parse(text);
			GameScr.info1.addInfo("Dừng khi chỉ số của nội tại đã chọn >= " + VuDang.ntMin + "%", 0);
			this.chatTField.isShow = false;
		}
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x000058F8 File Offset: 0x00003AF8
	public void onCancelChat()
	{
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00081224 File Offset: 0x0007F424
	public void setCombineEff(int type)
	{
		this.typeCombine = type;
		this.rS = 90;
		bool flag = this.typeCombine == 0;
		if (flag)
		{
			this.iDotS = 5;
			this.angleS = (this.angleO = 90);
			this.time = 2;
			for (int i = 0; i < this.vItemCombine.size(); i++)
			{
				Item item = (Item)this.vItemCombine.elementAt(i);
				bool flag2 = item != null;
				if (flag2)
				{
					bool flag3 = item.template.type == 14;
					if (flag3)
					{
						this.iconID2 = item.template.iconID;
					}
					else
					{
						this.iconID1 = item.template.iconID;
					}
				}
			}
		}
		else
		{
			bool flag4 = this.typeCombine == 1;
			if (flag4)
			{
				this.iDotS = 2;
				this.angleS = (this.angleO = 0);
				this.time = 1;
				for (int j = 0; j < this.vItemCombine.size(); j++)
				{
					Item item2 = (Item)this.vItemCombine.elementAt(j);
					bool flag5 = item2 != null;
					if (flag5)
					{
						bool flag6 = j == 0;
						if (flag6)
						{
							this.iconID1 = item2.template.iconID;
						}
						else
						{
							this.iconID2 = item2.template.iconID;
						}
					}
				}
			}
			else
			{
				bool flag7 = this.typeCombine == 2;
				if (flag7)
				{
					this.iDotS = 7;
					this.angleS = (this.angleO = 25);
					this.time = 1;
					for (int k = 0; k < this.vItemCombine.size(); k++)
					{
						Item item3 = (Item)this.vItemCombine.elementAt(k);
						bool flag8 = item3 != null;
						if (flag8)
						{
							this.iconID1 = item3.template.iconID;
						}
					}
				}
				else
				{
					bool flag9 = this.typeCombine == 3;
					if (flag9)
					{
						this.xS = GameCanvas.hw;
						this.yS = GameCanvas.hh;
						this.iDotS = 1;
						this.angleS = (this.angleO = 1);
						this.time = 4;
						for (int l = 0; l < this.vItemCombine.size(); l++)
						{
							Item item4 = (Item)this.vItemCombine.elementAt(l);
							bool flag10 = item4 != null;
							if (flag10)
							{
								this.iconID1 = item4.template.iconID;
							}
						}
					}
					else
					{
						bool flag11 = this.typeCombine == 4;
						if (flag11)
						{
							this.iDotS = this.vItemCombine.size();
							this.iconID = new short[this.iDotS];
							this.angleS = (this.angleO = 25);
							this.time = 1;
							for (int m = 0; m < this.vItemCombine.size(); m++)
							{
								Item item5 = (Item)this.vItemCombine.elementAt(m);
								bool flag12 = item5 != null;
								if (flag12)
								{
									this.iconID[m] = item5.template.iconID;
								}
							}
						}
					}
				}
			}
		}
		this.speed = 1;
		this.isSpeedCombine = true;
		this.isDoneCombine = false;
		this.isCompleteEffCombine = false;
		this.iAngleS = 360 / this.iDotS;
		this.xArgS = new int[this.iDotS];
		this.yArgS = new int[this.iDotS];
		this.xDotS = new int[this.iDotS];
		this.yDotS = new int[this.iDotS];
		this.setDotStar();
		this.isPaintCombine = true;
		this.countUpdate = 10;
		this.countR = 30;
		this.countWait = 10;
		this.addTextCombineNPC(this.idNPC, mResources.combineSpell);
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x0008161C File Offset: 0x0007F81C
	private void updateCombineEff()
	{
		this.countUpdate--;
		bool flag = this.countUpdate < 0;
		if (flag)
		{
			this.countUpdate = 0;
		}
		this.countR--;
		bool flag2 = this.countR < 0;
		if (flag2)
		{
			this.countR = 0;
		}
		bool flag3 = this.countUpdate != 0;
		if (!flag3)
		{
			bool flag4 = !this.isCompleteEffCombine;
			if (flag4)
			{
				bool flag5 = this.time > 0;
				if (flag5)
				{
					bool flag6 = this.combineSuccess != -1;
					if (flag6)
					{
						bool flag7 = this.typeCombine == 3;
						if (flag7)
						{
							bool flag8 = GameCanvas.gameTick % 10 == 0;
							if (flag8)
							{
								Effect me = new Effect(21, this.xS - 10, this.yS + 25, 4, 1, 1);
								EffecMn.addEff(me);
								this.time--;
							}
						}
						else
						{
							bool flag9 = GameCanvas.gameTick % 2 == 0;
							if (flag9)
							{
								bool flag10 = this.isSpeedCombine;
								if (flag10)
								{
									bool flag11 = this.speed < 40;
									if (flag11)
									{
										this.speed += 2;
									}
								}
								else
								{
									bool flag12 = this.speed > 10;
									if (flag12)
									{
										this.speed -= 2;
									}
								}
							}
							bool flag13 = this.countR == 0;
							if (flag13)
							{
								bool flag14 = this.isSpeedCombine;
								if (flag14)
								{
									bool flag15 = this.rS > 0;
									if (flag15)
									{
										this.rS -= 5;
									}
									else
									{
										bool flag16 = GameCanvas.gameTick % 10 == 0;
										if (flag16)
										{
											this.isSpeedCombine = false;
											this.time--;
											this.countR = 5;
											this.countWait = 10;
										}
									}
								}
								else
								{
									bool flag17 = this.rS < 90;
									if (flag17)
									{
										this.rS += 5;
									}
									else
									{
										bool flag18 = GameCanvas.gameTick % 10 == 0;
										if (flag18)
										{
											this.isSpeedCombine = true;
											this.countR = 10;
										}
									}
								}
							}
							this.angleS = this.angleO;
							this.angleS -= this.speed;
							bool flag19 = this.angleS >= 360;
							if (flag19)
							{
								this.angleS -= 360;
							}
							bool flag20 = this.angleS < 0;
							if (flag20)
							{
								this.angleS = 360 + this.angleS;
							}
							this.angleO = this.angleS;
							this.setDotStar();
						}
					}
				}
				else
				{
					bool flag21 = GameCanvas.gameTick % 20 == 0;
					if (flag21)
					{
						this.isCompleteEffCombine = true;
					}
				}
				bool flag22 = GameCanvas.gameTick % 20 == 0;
				if (flag22)
				{
					bool flag23 = this.typeCombine != 3;
					if (flag23)
					{
						EffectPanel.addServerEffect(132, this.xS, this.yS, 2);
					}
					EffectPanel.addServerEffect(114, this.xS, this.yS + 20, 2);
				}
			}
			else
			{
				bool flag24 = !this.isCompleteEffCombine;
				if (!flag24)
				{
					bool flag25 = this.combineSuccess == 1;
					if (flag25)
					{
						bool flag26 = this.countWait == 10;
						if (flag26)
						{
							Effect me2 = new Effect(22, this.xS - 3, this.yS + 25, 4, 1, 1);
							EffecMn.addEff(me2);
						}
						this.countWait--;
						bool flag27 = this.countWait < 0;
						if (flag27)
						{
							this.countWait = 0;
						}
						bool flag28 = this.rS < 300;
						if (flag28)
						{
							this.rS = Res.abs(this.rS + 10);
							bool flag29 = this.rS == 20;
							if (flag29)
							{
								this.addTextCombineNPC(this.idNPC, mResources.combineFail);
							}
						}
						else
						{
							bool flag30 = GameCanvas.gameTick % 20 == 0;
							if (flag30)
							{
								bool flag31 = GameCanvas.w > 2 * Panel.WIDTH_PANEL;
								if (flag31)
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
								this.combineSuccess = -1;
								this.isDoneCombine = true;
								bool flag32 = this.typeCombine == 4;
								if (flag32)
								{
									GameCanvas.panel.hideNow();
								}
							}
						}
						this.setDotStar();
					}
					else
					{
						bool flag33 = this.combineSuccess != 0;
						if (!flag33)
						{
							bool flag34 = this.countWait == 10;
							if (flag34)
							{
								bool flag35 = this.typeCombine == 2;
								if (flag35)
								{
									Effect me3 = new Effect(20, this.xS - 3, this.yS + 15, 4, 2, 1);
									EffecMn.addEff(me3);
								}
								else
								{
									Effect me4 = new Effect(21, this.xS - 10, this.yS + 25, 4, 1, 1);
									EffecMn.addEff(me4);
								}
								this.addTextCombineNPC(this.idNPC, mResources.combineSuccess);
								this.isPaintCombine = false;
							}
							bool flag36 = this.isPaintCombine;
							if (!flag36)
							{
								this.countWait--;
								bool flag37 = this.countWait < -50;
								if (flag37)
								{
									this.countWait = -50;
									bool flag38 = this.typeCombine < 3 && GameCanvas.w > 2 * Panel.WIDTH_PANEL;
									if (flag38)
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
									this.combineSuccess = -1;
									this.isDoneCombine = true;
									bool flag39 = this.typeCombine == 4;
									if (flag39)
									{
										GameCanvas.panel.hideNow();
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00081C28 File Offset: 0x0007FE28
	public void paintCombineEff(mGraphics g)
	{
		GameScr.gI().paintBlackSky(g);
		this.paintCombineNPC(g);
		bool flag = this.typeCombine == 0;
		if (flag)
		{
			for (int i = 0; i < this.yArgS.Length; i++)
			{
				SmallImage.drawSmallImage(g, (int)this.iconID1, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				bool flag2 = this.isPaintCombine;
				if (flag2)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID2, this.xDotS[i], this.yDotS[i], 0, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
		}
		else
		{
			bool flag3 = this.typeCombine == 1;
			if (flag3)
			{
				bool flag4 = !this.isPaintCombine;
				if (flag4)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				}
				else
				{
					for (int j = 0; j < this.yArgS.Length; j++)
					{
						SmallImage.drawSmallImage(g, (int)this.iconID1, this.xDotS[0], this.yDotS[0], 0, mGraphics.VCENTER | mGraphics.HCENTER);
						SmallImage.drawSmallImage(g, (int)this.iconID2, this.xDotS[1], this.yDotS[1], 0, mGraphics.VCENTER | mGraphics.HCENTER);
					}
				}
			}
			else
			{
				bool flag5 = this.typeCombine == 2;
				if (flag5)
				{
					bool flag6 = !this.isPaintCombine;
					if (flag6)
					{
						SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
					}
					else
					{
						for (int k = 0; k < this.yArgS.Length; k++)
						{
							SmallImage.drawSmallImage(g, (int)this.iconID1, this.xDotS[k], this.yDotS[k], 0, mGraphics.VCENTER | mGraphics.HCENTER);
						}
					}
				}
				else
				{
					bool flag7 = this.typeCombine == 3;
					if (flag7)
					{
						bool flag8 = !this.isPaintCombine;
						if (flag8)
						{
							SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
						}
						else
						{
							SmallImage.drawSmallImage(g, (int)this.iconID1, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
						}
					}
					else
					{
						bool flag9 = this.typeCombine != 4;
						if (!flag9)
						{
							bool flag10 = !this.isPaintCombine;
							if (flag10)
							{
								bool flag11 = this.iconID3 != -1;
								if (flag11)
								{
									SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
								}
							}
							else
							{
								for (int l = 0; l < this.iconID.Length; l++)
								{
									SmallImage.drawSmallImage(g, (int)this.iconID[l], this.xDotS[l], this.yDotS[l], 0, mGraphics.VCENTER | mGraphics.HCENTER);
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x00081F5C File Offset: 0x0008015C
	private void setDotStar()
	{
		for (int i = 0; i < this.yArgS.Length; i++)
		{
			bool flag = this.angleS >= 360;
			if (flag)
			{
				this.angleS -= 360;
			}
			bool flag2 = this.angleS < 0;
			if (flag2)
			{
				this.angleS = 360 + this.angleS;
			}
			this.yArgS[i] = Res.abs(this.rS * Res.sin(this.angleS) / 1024);
			this.xArgS[i] = Res.abs(this.rS * Res.cos(this.angleS) / 1024);
			bool flag3 = this.angleS < 90;
			if (flag3)
			{
				this.xDotS[i] = this.xS + this.xArgS[i];
				this.yDotS[i] = this.yS - this.yArgS[i];
			}
			else
			{
				bool flag4 = this.angleS >= 90 && this.angleS < 180;
				if (flag4)
				{
					this.xDotS[i] = this.xS - this.xArgS[i];
					this.yDotS[i] = this.yS - this.yArgS[i];
				}
				else
				{
					bool flag5 = this.angleS >= 180 && this.angleS < 270;
					if (flag5)
					{
						this.xDotS[i] = this.xS - this.xArgS[i];
						this.yDotS[i] = this.yS + this.yArgS[i];
					}
					else
					{
						this.xDotS[i] = this.xS + this.xArgS[i];
						this.yDotS[i] = this.yS + this.yArgS[i];
					}
				}
			}
			this.angleS -= this.iAngleS;
		}
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x00082150 File Offset: 0x00080350
	public void paintCombineNPC(mGraphics g)
	{
		g.translate(-GameScr.cmx, -GameScr.cmy);
		bool flag = this.typeCombine < 3;
		if (flag)
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				bool flag2 = npc.template.npcTemplateId == this.idNPC;
				if (flag2)
				{
					npc.paint(g);
					bool flag3 = npc.chatInfo != null;
					if (flag3)
					{
						npc.chatInfo.paint(g, npc.cx, npc.cy - npc.ch - GameCanvas.transY, npc.cdir);
					}
				}
			}
		}
		GameCanvas.resetTrans(g);
		bool flag4 = GameCanvas.gameTick % 4 == 0;
		if (flag4)
		{
			g.drawImage(ItemMap.imageFlare, this.xS - 5, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
			g.drawImage(ItemMap.imageFlare, this.xS + 5, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
			g.drawImage(ItemMap.imageFlare, this.xS, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
		}
		for (int j = 0; j < Effect2.vEffect3.size(); j++)
		{
			Effect2 effect = (Effect2)Effect2.vEffect3.elementAt(j);
			effect.paint(g);
		}
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x000822E0 File Offset: 0x000804E0
	public void addTextCombineNPC(int idNPC, string text)
	{
		bool flag = this.typeCombine >= 3;
		if (!flag)
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				bool flag2 = npc.template.npcTemplateId == idNPC;
				if (flag2)
				{
					npc.addInfo(text);
				}
			}
		}
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x0008234C File Offset: 0x0008054C
	public void setTypeOption()
	{
		this.type = 19;
		this.setType(0);
		this.setTabOption();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x00082384 File Offset: 0x00080584
	private void setTabOption()
	{
		SoundMn.gI().getStrOption();
		this.currentListLength = Panel.strCauhinh.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? -1 : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x00082458 File Offset: 0x00080658
	private void paintOption(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.strCauhinh.Length; i++)
		{
			int x = this.xScroll;
			int num = this.yScroll + i * this.ITEM_HEIGHT;
			int num2 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			bool flag = num - this.cmy <= this.yScroll + this.hScroll && num - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
			if (flag)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(x, num, num2, h);
				mFont.tahoma_7b_dark.drawString(g, Panel.strCauhinh[i], this.xScroll + this.wScroll / 2, num + 6, mFont.CENTER);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x00082574 File Offset: 0x00080774
	private void doFireOption()
	{
		bool flag = this.selected < 0;
		if (!flag)
		{
			switch (this.selected)
			{
			case 0:
				SoundMn.gI().HatToolOption();
				break;
			case 1:
				SoundMn.gI().AuraToolOption();
				break;
			case 2:
				SoundMn.gI().soundToolOption();
				break;
			case 3:
			{
				bool isPC = Main.isPC;
				if (isPC)
				{
					GameCanvas.startYesNoDlg(mResources.changeSizeScreen, new Command(mResources.YES, this, 170391, null), new Command(mResources.NO, this, 4005, null));
				}
				else
				{
					bool flag2 = GameScr.isAnalog == 0;
					if (flag2)
					{
						GameScr.isAnalog = 1;
						Rms.saveRMSInt("analog", GameScr.isAnalog);
						GameScr.setSkillBarPosition();
					}
					else
					{
						GameScr.isAnalog = 0;
						Rms.saveRMSInt("analog", GameScr.isAnalog);
						GameScr.setSkillBarPosition();
					}
					SoundMn.gI().getStrOption();
				}
				break;
			}
			}
		}
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x00082678 File Offset: 0x00080878
	public void setTypeAccount()
	{
		this.type = 20;
		this.setType(0);
		this.setTabAccount();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x000826B0 File Offset: 0x000808B0
	private void setTabAccount()
	{
		bool iphoneVersionApp = Main.IphoneVersionApp;
		if (iphoneVersionApp)
		{
			Panel.strAccount = new string[]
			{
				mResources.inventory_Pass,
				mResources.friend,
				mResources.enemy,
				mResources.msg
			};
			bool canAutoPlay = GameScr.canAutoPlay;
			if (canAutoPlay)
			{
				Panel.strAccount = new string[]
				{
					mResources.inventory_Pass,
					mResources.friend,
					mResources.enemy,
					mResources.msg,
					mResources.autoFunction
				};
			}
		}
		else
		{
			Panel.strAccount = new string[]
			{
				mResources.inventory_Pass,
				mResources.friend,
				mResources.enemy,
				mResources.msg,
				mResources.charger
			};
			bool canAutoPlay2 = GameScr.canAutoPlay;
			if (canAutoPlay2)
			{
				Panel.strAccount = new string[]
				{
					mResources.inventory_Pass,
					mResources.friend,
					mResources.enemy,
					mResources.msg,
					mResources.charger,
					mResources.autoFunction
				};
			}
			bool flag = (mSystem.clientType == 2 || mSystem.clientType == 7) && mResources.language != 2;
			if (flag)
			{
				Panel.strAccount = new string[]
				{
					mResources.inventory_Pass,
					mResources.friend,
					mResources.enemy,
					mResources.msg,
					mResources.charger
				};
				bool canAutoPlay3 = GameScr.canAutoPlay;
				if (canAutoPlay3)
				{
					Panel.strAccount = new string[]
					{
						mResources.inventory_Pass,
						mResources.friend,
						mResources.enemy,
						mResources.msg,
						mResources.charger,
						mResources.autoFunction
					};
				}
			}
		}
		this.currentListLength = Panel.strAccount.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? -1 : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag2 = this.cmyLim < 0;
		if (flag2)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag3 = this.cmy < 0;
		if (flag3)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag4 = this.cmy > this.cmyLim;
		if (flag4)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x00082918 File Offset: 0x00080B18
	private void paintAccount(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.strAccount.Length; i++)
		{
			int x = this.xScroll;
			int num = this.yScroll + i * this.ITEM_HEIGHT;
			int num2 = this.wScroll - 1;
			int h = this.ITEM_HEIGHT - 1;
			bool flag = num - this.cmy <= this.yScroll + this.hScroll && num - this.cmy >= this.yScroll - this.ITEM_HEIGHT;
			if (flag)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(x, num, num2, h);
				mFont.tahoma_7b_dark.drawString(g, Panel.strAccount[i], this.xScroll + this.wScroll / 2, num + 6, mFont.CENTER);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x00082A34 File Offset: 0x00080C34
	private void doFireAccount()
	{
		bool flag = this.selected < 0;
		if (!flag)
		{
			switch (this.selected)
			{
			case 0:
			{
				GameCanvas.endDlg();
				bool flag2 = this.chatTField == null;
				if (flag2)
				{
					this.chatTField = new ChatTextField();
					this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
					this.chatTField.initChatTextField();
					this.chatTField.parentScreen = GameCanvas.panel;
				}
				this.chatTField.tfChat.setText(string.Empty);
				this.chatTField.strChat = mResources.input_Inventory_Pass;
				this.chatTField.tfChat.name = mResources.input_Inventory_Pass;
				this.chatTField.to = string.Empty;
				this.chatTField.isShow = true;
				this.chatTField.tfChat.isFocus = true;
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
				bool isTouch = GameCanvas.isTouch;
				if (isTouch)
				{
					this.chatTField.tfChat.doChangeToTextBox();
				}
				bool flag3 = !Main.isPC;
				if (flag3)
				{
					this.chatTField.startChat2(this, string.Empty);
				}
				bool isWindowsPhone = Main.isWindowsPhone;
				if (isWindowsPhone)
				{
					this.chatTField.tfChat.strInfo = this.chatTField.strChat;
				}
				break;
			}
			case 1:
				Service.gI().friend(0, -1);
				InfoDlg.showWait();
				break;
			case 2:
				Service.gI().enemy(0, -1);
				InfoDlg.showWait();
				break;
			case 3:
			{
				this.setTypeMessage();
				bool flag4 = this.chatTField == null;
				if (flag4)
				{
					this.chatTField = new ChatTextField();
					this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
					this.chatTField.initChatTextField();
					this.chatTField.parentScreen = GameCanvas.panel;
				}
				break;
			}
			case 4:
			{
				bool flag5 = mResources.language == 2;
				if (flag5)
				{
					string url = "http://dragonball.indonaga.com/coda/?username=" + GameCanvas.loginScr.tfUser.getText();
					this.hideNow();
					try
					{
						GameMidlet.instance.platformRequest(url);
					}
					catch (Exception ex)
					{
						ex.StackTrace.ToString();
					}
				}
				else
				{
					this.hideNow();
					bool flag6 = global::Char.myCharz().taskMaint.taskId <= 10;
					if (flag6)
					{
						GameCanvas.startOKDlg(mResources.finishBomong);
					}
					else
					{
						MoneyCharge.gI().switchToMe();
					}
				}
				break;
			}
			case 5:
				this.setTypeAuto();
				break;
			}
		}
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x00005896 File Offset: 0x00003A96
	private void updateKeyOption()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x00005911 File Offset: 0x00003B11
	public void setTypeSpeacialSkill()
	{
		this.type = 25;
		this.setType(0);
		this.setTabSpeacialSkill();
		this.currentTabIndex = 0;
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x00082D1C File Offset: 0x00080F1C
	private void setTabSpeacialSkill()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = global::Char.myCharz().infoSpeacialSkill[this.currentTabIndex].Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		bool flag = this.cmyLim < 0;
		if (flag)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		bool flag2 = this.cmy < 0;
		if (flag2)
		{
			this.cmy = (this.cmtoY = 0);
		}
		bool flag3 = this.cmy > this.cmyLim;
		if (flag3)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = (GameCanvas.isTouch ? -1 : 0);
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x00082DF0 File Offset: 0x00080FF0
	public bool isTypeShop()
	{
		return this.type == 1;
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x00082E18 File Offset: 0x00081018
	private void doNotiRuby(int type)
	{
		try
		{
			this.currItem.buyRuby = int.Parse(this.chatTField.tfChat.getText());
		}
		catch (Exception)
		{
			GameCanvas.startOKDlg(mResources.input_money_wrong);
			this.chatTField.isShow = false;
			return;
		}
		Command cmdYes = new Command(mResources.YES, this, (type != 0) ? 11001 : 11000, null);
		Command cmdNo = new Command(mResources.NO, this, 11002, null);
		GameCanvas.startYesNoDlg(mResources.notiRuby, cmdYes, cmdNo);
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x00082EB0 File Offset: 0x000810B0
	public static void paintUpgradeEffect(int x, int y, int wItem, int hItem, int nline, int cl, mGraphics g)
	{
		try
		{
			int num = (wItem << 1) + (hItem << 1);
			int num2 = num / nline;
			Panel.nsize = Panel.sizeUpgradeEff.Length;
			bool flag = nline > 4;
			if (flag)
			{
				Panel.nsize = 2;
			}
			for (int i = 0; i < nline; i++)
			{
				for (int j = 0; j < Panel.nsize; j++)
				{
					int wSize = (Panel.sizeUpgradeEff[j] <= 1) ? 1 : ((Panel.sizeUpgradeEff[j] >> 1) + 1);
					int x2 = x + Panel.upgradeEffectX(num2 * i, GameCanvas.gameTick - j * 4, wItem, hItem, wSize);
					int y2 = y + Panel.upgradeEffectY(num2 * i, GameCanvas.gameTick - j * 4, wItem, hItem, wSize);
					g.setColor(Panel.colorUpgradeEffect[cl][j]);
					g.fillRect(x2, y2, Panel.sizeUpgradeEff[j], Panel.sizeUpgradeEff[j]);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x00082FC0 File Offset: 0x000811C0
	private static int upgradeEffectX(int dk, int tick, int wItem, int hitem, int wSize)
	{
		int num = (tick + dk) % ((wItem << 1) + (hitem << 1));
		bool flag = 0 <= num && num < wItem;
		int result;
		if (flag)
		{
			result = num % wItem;
		}
		else
		{
			bool flag2 = wItem <= num && num < wItem + hitem;
			if (flag2)
			{
				result = wItem - wSize;
			}
			else
			{
				bool flag3 = wItem + hitem <= num && num < (wItem << 1) + hitem;
				if (flag3)
				{
					result = wItem - (num - hitem) % wItem - wSize;
				}
				else
				{
					result = 0;
				}
			}
		}
		return result;
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x00083034 File Offset: 0x00081234
	private static int upgradeEffectY(int dk, int tick, int wItem, int hitem, int wSize)
	{
		int num = (tick + dk) % ((wItem << 1) + (hitem << 1));
		bool flag = 0 <= num && num < wItem;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			bool flag2 = wItem <= num && num < wItem + hitem;
			if (flag2)
			{
				result = num % wItem;
			}
			else
			{
				bool flag3 = wItem + hitem <= num && num < (wItem << 1) + hitem;
				if (flag3)
				{
					result = hitem - wSize;
				}
				else
				{
					result = hitem - (num - (wItem << 1)) % hitem - wSize;
				}
			}
		}
		return result;
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x000830AC File Offset: 0x000812AC
	public static int GetColor_ItemBg(int id)
	{
		int result;
		switch (id)
		{
		case 1:
			result = 2786816;
			break;
		case 2:
			result = 7078041;
			break;
		case 3:
			result = 12537346;
			break;
		case 4:
			result = 1269146;
			break;
		case 5:
			result = 13279744;
			break;
		case 6:
			result = 11599872;
			break;
		default:
			result = -1;
			break;
		}
		return result;
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x00083114 File Offset: 0x00081314
	public static sbyte GetColor_Item_Upgrade(int lv)
	{
		bool flag = lv < 0;
		sbyte result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			switch (lv)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
				result = 0;
				break;
			case 9:
				result = 4;
				break;
			case 10:
				result = 1;
				break;
			case 11:
				result = 5;
				break;
			case 12:
				result = 3;
				break;
			case 13:
				result = 2;
				break;
			default:
				result = 6;
				break;
			}
		}
		return result;
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00083190 File Offset: 0x00081390
	public static mFont GetFont(int color)
	{
		mFont result = mFont.tahoma_7;
		switch (color)
		{
		case -1:
			result = mFont.tahoma_7;
			break;
		case 0:
			result = mFont.tahoma_7b_dark;
			break;
		case 1:
			result = mFont.tahoma_7b_green;
			break;
		case 2:
			result = mFont.tahoma_7b_blue;
			break;
		case 3:
			result = mFont.tahoma_7_red;
			break;
		case 4:
			result = mFont.tahoma_7_green;
			break;
		case 5:
			result = mFont.tahoma_7_blue;
			break;
		case 7:
			result = mFont.tahoma_7b_red;
			break;
		case 8:
			result = mFont.tahoma_7b_yellow;
			break;
		}
		return result;
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x00083228 File Offset: 0x00081428
	public void paintOptItem(mGraphics g, int idOpt, int param, int x, int y, int w, int h)
	{
		switch (idOpt)
		{
		case 34:
		{
			bool flag = this.imgo_0 != null;
			if (flag)
			{
				g.drawImage(this.imgo_0, x, y + h - this.imgo_0.getHeight());
			}
			else
			{
				this.imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
			}
			bool flag2 = this.imgo_1 != null;
			if (flag2)
			{
				g.drawImage(this.imgo_1, x, y + h - this.imgo_1.getHeight());
			}
			else
			{
				this.imgo_1 = mSystem.loadImage("/mainImage/o_1.png");
			}
			break;
		}
		case 35:
		{
			bool flag3 = this.imgo_0 != null;
			if (flag3)
			{
				g.drawImage(this.imgo_0, x, y + h - this.imgo_0.getHeight());
			}
			else
			{
				this.imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
			}
			bool flag4 = this.imgo_2 != null;
			if (flag4)
			{
				g.drawImage(this.imgo_2, x, y + h - this.imgo_2.getHeight());
			}
			else
			{
				this.imgo_2 = mSystem.loadImage("/mainImage/o_2.png");
			}
			break;
		}
		case 36:
		{
			bool flag5 = this.imgo_0 != null;
			if (flag5)
			{
				g.drawImage(this.imgo_0, x, y + h - this.imgo_0.getHeight());
			}
			else
			{
				this.imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
			}
			bool flag6 = this.imgo_3 != null;
			if (flag6)
			{
				g.drawImage(this.imgo_3, x, y + h - this.imgo_3.getHeight());
			}
			else
			{
				this.imgo_3 = mSystem.loadImage("/mainImage/o_3.png");
			}
			break;
		}
		}
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x000833F8 File Offset: 0x000815F8
	public void paintOptSlotItem(mGraphics g, int idOpt, int param, int x, int y, int w, int h)
	{
		bool flag = idOpt == 102 && param > ChatPopup.numSlot;
		if (flag)
		{
			sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(param);
			int nline = param - ChatPopup.numSlot;
			Panel.paintUpgradeEffect(x, y, w, h, nline, (int)color_Item_Upgrade, g);
		}
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0008343C File Offset: 0x0008163C
	public static mFont setTextColor(int id, int type)
	{
		bool flag = type == 0;
		mFont result;
		if (flag)
		{
			switch (id)
			{
			case 0:
				return mFont.bigNumber_While;
			case 1:
				return mFont.bigNumber_green;
			case 3:
				return mFont.bigNumber_orange;
			case 4:
				return mFont.bigNumber_blue;
			case 5:
				return mFont.bigNumber_yellow;
			case 6:
				return mFont.bigNumber_red;
			}
			result = mFont.bigNumber_While;
		}
		else
		{
			switch (id)
			{
			case 0:
				return mFont.tahoma_7b_white;
			case 1:
				return mFont.tahoma_7b_green;
			case 3:
				return mFont.tahoma_7b_yellowSmall2;
			case 4:
				return mFont.tahoma_7b_blue;
			case 5:
				return mFont.tahoma_7b_yellow;
			case 6:
				return mFont.tahoma_7b_red;
			case 7:
				return mFont.tahoma_7b_dark;
			}
			result = mFont.tahoma_7b_white;
		}
		return result;
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x00083528 File Offset: 0x00081728
	private bool GetInventorySelect_isbody(int select, int subSelect, Item[] arrItem)
	{
		int num = select - 1 + subSelect * 20;
		return subSelect == 0 && num < arrItem.Length;
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x00083550 File Offset: 0x00081750
	private int GetInventorySelect_body(int select, int subSelect)
	{
		return select - 1 + subSelect * 20;
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0008356C File Offset: 0x0008176C
	private int GetInventorySelect_bag(int select, int subSelect, Item[] arrItem)
	{
		int num = select - 1 + subSelect * 20;
		return num - arrItem.Length;
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x0008358C File Offset: 0x0008178C
	private void updateKeyInvenTab()
	{
		bool flag = this.selected < 0;
		if (!flag)
		{
			bool flag2 = GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23];
			if (flag2)
			{
				this.newSelected--;
				bool flag3 = this.newSelected < 0;
				if (flag3)
				{
					this.newSelected = 0;
					bool isFocusPanel = GameCanvas.isFocusPanel2;
					if (isFocusPanel)
					{
						GameCanvas.isFocusPanel2 = false;
						GameCanvas.panel.selected = 0;
					}
				}
			}
			else
			{
				bool flag4 = !GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24];
				if (!flag4)
				{
					this.newSelected++;
					bool flag5 = this.newSelected > (int)(this.size_tab - 1);
					if (flag5)
					{
						this.newSelected = (int)(this.size_tab - 1);
						bool flag6 = GameCanvas.panel2 != null;
						if (flag6)
						{
							GameCanvas.isFocusPanel2 = true;
							GameCanvas.panel2.selected = 0;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x00005932 File Offset: 0x00003B32
	private void updateKeyInventory()
	{
		this.updateKeyScrollView();
		this.updateKeyInvenTab();
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x0008367C File Offset: 0x0008187C
	private bool IsTabOption()
	{
		bool flag = this.size_tab > 0;
		if (flag)
		{
			bool flag2 = this.currentTabName.Length > 1;
			if (flag2)
			{
				bool flag3 = this.selected == 0;
				if (flag3)
				{
					return true;
				}
			}
			else
			{
				bool flag4 = this.selected >= 0;
				if (flag4)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x000836DC File Offset: 0x000818DC
	private int checkCurrentListLength(int arrLength)
	{
		int num = 20;
		int num2 = arrLength / 20 + ((arrLength % 20 > 0) ? 1 : 0);
		this.size_tab = (sbyte)num2;
		bool flag = this.newSelected > num2 - 1;
		if (flag)
		{
			this.newSelected = num2 - 1;
		}
		bool flag2 = arrLength % 20 > 0 && this.newSelected == num2 - 1;
		if (flag2)
		{
			num = arrLength % 20;
		}
		return num + 1;
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x0008374C File Offset: 0x0008194C
	private void setNewSelected(int arrLength, bool resetSelect)
	{
		int num = arrLength / 20 + ((arrLength % 20 > 0) ? 1 : 0);
		int num2 = this.xScroll;
		this.newSelected = (GameCanvas.px - num2) / this.TAB_W_NEW;
		bool flag = this.newSelected > num - 1;
		if (flag)
		{
			this.newSelected = num - 1;
		}
		bool flag2 = GameCanvas.px < num2;
		if (flag2)
		{
			this.newSelected = 0;
		}
		this.setTabInventory(resetSelect);
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x000837C0 File Offset: 0x000819C0
	public void VuDangChatTextField(string strChat, string strName)
	{
		bool flag = this.chatTField == null;
		if (flag)
		{
			this.chatTField = new ChatTextField();
			this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			this.chatTField.initChatTextField();
			this.chatTField.parentScreen = GameCanvas.panel;
		}
		this.chatTField.strChat = strChat;
		this.chatTField.tfChat.name = strName;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
		this.chatTField.tfChat.setMaxTextLenght(9);
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.chatTField.tfChat.doChangeToTextBox();
		}
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		bool flag2 = !Main.isPC;
		if (flag2)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x04000DD1 RID: 3537
	public bool isShow;

	// Token: 0x04000DD2 RID: 3538
	public int X;

	// Token: 0x04000DD3 RID: 3539
	public int Y;

	// Token: 0x04000DD4 RID: 3540
	public int W;

	// Token: 0x04000DD5 RID: 3541
	public int H;

	// Token: 0x04000DD6 RID: 3542
	public int ITEM_HEIGHT;

	// Token: 0x04000DD7 RID: 3543
	public int TAB_W;

	// Token: 0x04000DD8 RID: 3544
	public int TAB_W_NEW;

	// Token: 0x04000DD9 RID: 3545
	public int cmtoY;

	// Token: 0x04000DDA RID: 3546
	public int cmy;

	// Token: 0x04000DDB RID: 3547
	public int cmdy;

	// Token: 0x04000DDC RID: 3548
	public int cmvy;

	// Token: 0x04000DDD RID: 3549
	public int cmyLim;

	// Token: 0x04000DDE RID: 3550
	public int xc;

	// Token: 0x04000DDF RID: 3551
	public int[] cmyLast;

	// Token: 0x04000DE0 RID: 3552
	public int cmtoX;

	// Token: 0x04000DE1 RID: 3553
	public int cmx;

	// Token: 0x04000DE2 RID: 3554
	public int cmxLim;

	// Token: 0x04000DE3 RID: 3555
	public int cmxMap;

	// Token: 0x04000DE4 RID: 3556
	public int cmyMap;

	// Token: 0x04000DE5 RID: 3557
	public int cmxMapLim;

	// Token: 0x04000DE6 RID: 3558
	public int cmyMapLim;

	// Token: 0x04000DE7 RID: 3559
	public int cmyQuest;

	// Token: 0x04000DE8 RID: 3560
	public static Image imgBantay;

	// Token: 0x04000DE9 RID: 3561
	public static Image imgX;

	// Token: 0x04000DEA RID: 3562
	public static Image imgMap;

	// Token: 0x04000DEB RID: 3563
	public TabClanIcon tabIcon;

	// Token: 0x04000DEC RID: 3564
	public MyVector vItemCombine = new MyVector();

	// Token: 0x04000DED RID: 3565
	public int moneyGD;

	// Token: 0x04000DEE RID: 3566
	public int friendMoneyGD;

	// Token: 0x04000DEF RID: 3567
	public bool isLock;

	// Token: 0x04000DF0 RID: 3568
	public bool isFriendLock;

	// Token: 0x04000DF1 RID: 3569
	public bool isAccept;

	// Token: 0x04000DF2 RID: 3570
	public bool isFriendAccep;

	// Token: 0x04000DF3 RID: 3571
	public string topName;

	// Token: 0x04000DF4 RID: 3572
	public ChatTextField chatTField;

	// Token: 0x04000DF5 RID: 3573
	public static string specialInfo;

	// Token: 0x04000DF6 RID: 3574
	public static short spearcialImage;

	// Token: 0x04000DF7 RID: 3575
	public static Image imgStar;

	// Token: 0x04000DF8 RID: 3576
	public static Image imgMaxStar;

	// Token: 0x04000DF9 RID: 3577
	public static Image imgStar8;

	// Token: 0x04000DFA RID: 3578
	public static Image imgNew;

	// Token: 0x04000DFB RID: 3579
	public static Image imgXu;

	// Token: 0x04000DFC RID: 3580
	public static Image imgTicket;

	// Token: 0x04000DFD RID: 3581
	public static Image imgLuong;

	// Token: 0x04000DFE RID: 3582
	public static Image imgLuongKhoa;

	// Token: 0x04000DFF RID: 3583
	private static Image imgUp;

	// Token: 0x04000E00 RID: 3584
	private static Image imgDown;

	// Token: 0x04000E01 RID: 3585
	private int pa1;

	// Token: 0x04000E02 RID: 3586
	private int pa2;

	// Token: 0x04000E03 RID: 3587
	private bool trans;

	// Token: 0x04000E04 RID: 3588
	private int pX;

	// Token: 0x04000E05 RID: 3589
	private int pY;

	// Token: 0x04000E06 RID: 3590
	private Command left = new Command(mResources.SELECT, 0);

	// Token: 0x04000E07 RID: 3591
	public int type;

	// Token: 0x04000E08 RID: 3592
	public int currentTabIndex;

	// Token: 0x04000E09 RID: 3593
	public int startTabPos;

	// Token: 0x04000E0A RID: 3594
	public int[] lastTabIndex;

	// Token: 0x04000E0B RID: 3595
	public string[][] currentTabName;

	// Token: 0x04000E0C RID: 3596
	private int[] currClanOption;

	// Token: 0x04000E0D RID: 3597
	public int mainTabPos = 4;

	// Token: 0x04000E0E RID: 3598
	public int shopTabPos = 50;

	// Token: 0x04000E0F RID: 3599
	public int boxTabPos = 50;

	// Token: 0x04000E10 RID: 3600
	public string[][] mainTabName;

	// Token: 0x04000E11 RID: 3601
	public string[] mapNames;

	// Token: 0x04000E12 RID: 3602
	public string[] planetNames;

	// Token: 0x04000E13 RID: 3603
	public static string[] strTool = new string[]
	{
		mResources.gameInfo,
		mResources.change_flag,
		mResources.change_zone,
		mResources.chat_world,
		mResources.account,
		mResources.option,
		mResources.change_account
	};

	// Token: 0x04000E14 RID: 3604
	public static string[] strCauhinh = new string[]
	{
		(!GameCanvas.isPlaySound) ? mResources.turnOnSound : mResources.turnOffSound,
		mResources.increase_vga,
		mResources.analog,
		(mGraphics.zoomLevel <= 1) ? mResources.x2Screen : mResources.x1Screen
	};

	// Token: 0x04000E15 RID: 3605
	public static string[] strAccount = new string[]
	{
		mResources.inventory_Pass,
		mResources.friend,
		mResources.enemy,
		mResources.msg,
		mResources.charger
	};

	// Token: 0x04000E16 RID: 3606
	public static string[] strAuto = new string[]
	{
		mResources.useGem
	};

	// Token: 0x04000E17 RID: 3607
	public static int graphics = 0;

	// Token: 0x04000E18 RID: 3608
	public string[][] shopTabName;

	// Token: 0x04000E19 RID: 3609
	public int[] maxPageShop;

	// Token: 0x04000E1A RID: 3610
	public int[] currPageShop;

	// Token: 0x04000E1B RID: 3611
	private static string[][] boxTabName = new string[][]
	{
		mResources.chestt,
		mResources.inventory
	};

	// Token: 0x04000E1C RID: 3612
	private static string[][] boxCombine = new string[][]
	{
		mResources.combine,
		mResources.inventory
	};

	// Token: 0x04000E1D RID: 3613
	private static string[][] boxZone = new string[][]
	{
		mResources.zonee
	};

	// Token: 0x04000E1E RID: 3614
	private static string[][] boxMap = new string[][]
	{
		mResources.mapp
	};

	// Token: 0x04000E1F RID: 3615
	private static string[][] boxGD = new string[][]
	{
		mResources.inventory,
		mResources.item_give,
		mResources.item_receive
	};

	// Token: 0x04000E20 RID: 3616
	private static string[][] boxPet = mResources.petMainTab;

	// Token: 0x04000E21 RID: 3617
	public string[][][] tabName = new string[][][]
	{
		null,
		null,
		Panel.boxTabName,
		Panel.boxZone,
		Panel.boxMap,
		null,
		null,
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		Panel.boxCombine,
		Panel.boxGD,
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		Panel.boxPet,
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		},
		new string[][]
		{
			new string[]
			{
				string.Empty
			}
		}
	};

	// Token: 0x04000E22 RID: 3618
	private static sbyte BOX_BAG = 0;

	// Token: 0x04000E23 RID: 3619
	private static sbyte BAG_BOX = 1;

	// Token: 0x04000E24 RID: 3620
	private static sbyte BOX_BODY = 2;

	// Token: 0x04000E25 RID: 3621
	private static sbyte BODY_BOX = 3;

	// Token: 0x04000E26 RID: 3622
	private static sbyte BAG_BODY = 4;

	// Token: 0x04000E27 RID: 3623
	private static sbyte BODY_BAG = 5;

	// Token: 0x04000E28 RID: 3624
	private static sbyte BAG_PET = 6;

	// Token: 0x04000E29 RID: 3625
	private static sbyte PET_BAG = 7;

	// Token: 0x04000E2A RID: 3626
	public int hasUse;

	// Token: 0x04000E2B RID: 3627
	public int hasUseBag;

	// Token: 0x04000E2C RID: 3628
	public int currentListLength;

	// Token: 0x04000E2D RID: 3629
	private int[] lastSelect;

	// Token: 0x04000E2E RID: 3630
	public static int[] mapIdTraidat = new int[]
	{
		21,
		0,
		1,
		2,
		24,
		3,
		4,
		5,
		6,
		27,
		28,
		29,
		30,
		42,
		47,
		46
	};

	// Token: 0x04000E2F RID: 3631
	public static int[] mapXTraidat = new int[]
	{
		39,
		42,
		105,
		93,
		61,
		93,
		142,
		165,
		210,
		100,
		165,
		220,
		233,
		10,
		125,
		125
	};

	// Token: 0x04000E30 RID: 3632
	public static int[] mapYTraidat = new int[]
	{
		28,
		60,
		48,
		96,
		88,
		131,
		136,
		95,
		32,
		200,
		189,
		167,
		120,
		110,
		20,
		20
	};

	// Token: 0x04000E31 RID: 3633
	public static int[] mapIdNamek = new int[]
	{
		22,
		7,
		8,
		9,
		25,
		11,
		12,
		13,
		10,
		31,
		32,
		33,
		34,
		43
	};

	// Token: 0x04000E32 RID: 3634
	public static int[] mapXNamek = new int[]
	{
		55,
		30,
		93,
		80,
		24,
		149,
		219,
		220,
		233,
		170,
		148,
		195,
		148,
		10
	};

	// Token: 0x04000E33 RID: 3635
	public static int[] mapYNamek = new int[]
	{
		136,
		84,
		69,
		34,
		25,
		42,
		32,
		110,
		192,
		70,
		106,
		156,
		210,
		57
	};

	// Token: 0x04000E34 RID: 3636
	public static int[] mapIdSaya = new int[]
	{
		23,
		14,
		15,
		16,
		26,
		17,
		18,
		20,
		19,
		35,
		36,
		37,
		38,
		44
	};

	// Token: 0x04000E35 RID: 3637
	public static int[] mapXSaya = new int[]
	{
		90,
		95,
		144,
		234,
		231,
		122,
		176,
		158,
		205,
		54,
		105,
		159,
		231,
		27
	};

	// Token: 0x04000E36 RID: 3638
	public static int[] mapYSaya = new int[]
	{
		10,
		43,
		20,
		36,
		69,
		87,
		112,
		167,
		160,
		151,
		173,
		207,
		194,
		29
	};

	// Token: 0x04000E37 RID: 3639
	public static int[][] mapId = new int[][]
	{
		Panel.mapIdTraidat,
		Panel.mapIdNamek,
		Panel.mapIdSaya
	};

	// Token: 0x04000E38 RID: 3640
	public static int[][] mapX = new int[][]
	{
		Panel.mapXTraidat,
		Panel.mapXNamek,
		Panel.mapXSaya
	};

	// Token: 0x04000E39 RID: 3641
	public static int[][] mapY = new int[][]
	{
		Panel.mapYTraidat,
		Panel.mapYNamek,
		Panel.mapYSaya
	};

	// Token: 0x04000E3A RID: 3642
	public Item currItem;

	// Token: 0x04000E3B RID: 3643
	public Clan currClan;

	// Token: 0x04000E3C RID: 3644
	public ClanMessage currMess;

	// Token: 0x04000E3D RID: 3645
	public Member currMem;

	// Token: 0x04000E3E RID: 3646
	public Clan[] clans;

	// Token: 0x04000E3F RID: 3647
	public MyVector member;

	// Token: 0x04000E40 RID: 3648
	public MyVector myMember;

	// Token: 0x04000E41 RID: 3649
	public MyVector logChat = new MyVector();

	// Token: 0x04000E42 RID: 3650
	public MyVector vPlayerMenu = new MyVector();

	// Token: 0x04000E43 RID: 3651
	public MyVector vFriend = new MyVector();

	// Token: 0x04000E44 RID: 3652
	public MyVector vMyGD = new MyVector();

	// Token: 0x04000E45 RID: 3653
	public MyVector vFriendGD = new MyVector();

	// Token: 0x04000E46 RID: 3654
	public MyVector vTop = new MyVector();

	// Token: 0x04000E47 RID: 3655
	public MyVector vEnemy = new MyVector();

	// Token: 0x04000E48 RID: 3656
	public MyVector vFlag = new MyVector();

	// Token: 0x04000E49 RID: 3657
	public Command cmdClose;

	// Token: 0x04000E4A RID: 3658
	public static bool CanNapTien = false;

	// Token: 0x04000E4B RID: 3659
	public static int WIDTH_PANEL = 240;

	// Token: 0x04000E4C RID: 3660
	private int position;

	// Token: 0x04000E4D RID: 3661
	public global::Char charMenu;

	// Token: 0x04000E4E RID: 3662
	private bool isThachDau;

	// Token: 0x04000E4F RID: 3663
	public int typeShop = -1;

	// Token: 0x04000E50 RID: 3664
	public int xScroll;

	// Token: 0x04000E51 RID: 3665
	public int yScroll;

	// Token: 0x04000E52 RID: 3666
	public int wScroll;

	// Token: 0x04000E53 RID: 3667
	public int hScroll;

	// Token: 0x04000E54 RID: 3668
	public ChatPopup cp;

	// Token: 0x04000E55 RID: 3669
	public int idIcon;

	// Token: 0x04000E56 RID: 3670
	public int[] partID;

	// Token: 0x04000E57 RID: 3671
	private int timeShow;

	// Token: 0x04000E58 RID: 3672
	public bool isBoxClan;

	// Token: 0x04000E59 RID: 3673
	public int w;

	// Token: 0x04000E5A RID: 3674
	private int pa;

	// Token: 0x04000E5B RID: 3675
	public int selected;

	// Token: 0x04000E5C RID: 3676
	private int cSelected;

	// Token: 0x04000E5D RID: 3677
	private int newSelected;

	// Token: 0x04000E5E RID: 3678
	private bool isClanOption;

	// Token: 0x04000E5F RID: 3679
	public bool isSearchClan;

	// Token: 0x04000E60 RID: 3680
	public bool isMessage;

	// Token: 0x04000E61 RID: 3681
	public bool isViewMember;

	// Token: 0x04000E62 RID: 3682
	public const int TYPE_MAIN = 0;

	// Token: 0x04000E63 RID: 3683
	public const int TYPE_SHOP = 1;

	// Token: 0x04000E64 RID: 3684
	public const int TYPE_BOX = 2;

	// Token: 0x04000E65 RID: 3685
	public const int TYPE_ZONE = 3;

	// Token: 0x04000E66 RID: 3686
	public const int TYPE_MAP = 4;

	// Token: 0x04000E67 RID: 3687
	public const int TYPE_CLANS = 5;

	// Token: 0x04000E68 RID: 3688
	public const int TYPE_INFOMATION = 6;

	// Token: 0x04000E69 RID: 3689
	public const int TYPE_BODY = 7;

	// Token: 0x04000E6A RID: 3690
	public const int TYPE_MESS = 8;

	// Token: 0x04000E6B RID: 3691
	public const int TYPE_ARCHIVEMENT = 9;

	// Token: 0x04000E6C RID: 3692
	public const int PLAYER_MENU = 10;

	// Token: 0x04000E6D RID: 3693
	public const int TYPE_FRIEND = 11;

	// Token: 0x04000E6E RID: 3694
	public const int TYPE_COMBINE = 12;

	// Token: 0x04000E6F RID: 3695
	public const int TYPE_GIAODICH = 13;

	// Token: 0x04000E70 RID: 3696
	public const int TYPE_MAPTRANS = 14;

	// Token: 0x04000E71 RID: 3697
	public const int TYPE_TOP = 15;

	// Token: 0x04000E72 RID: 3698
	public const int TYPE_ENEMY = 16;

	// Token: 0x04000E73 RID: 3699
	public const int TYPE_KIGUI = 17;

	// Token: 0x04000E74 RID: 3700
	public const int TYPE_FLAG = 18;

	// Token: 0x04000E75 RID: 3701
	public const int TYPE_OPTION = 19;

	// Token: 0x04000E76 RID: 3702
	public const int TYPE_ACCOUNT = 20;

	// Token: 0x04000E77 RID: 3703
	public const int TYPE_PET_MAIN = 21;

	// Token: 0x04000E78 RID: 3704
	public const int TYPE_AUTO = 22;

	// Token: 0x04000E79 RID: 3705
	public const int TYPE_GAMEINFO = 23;

	// Token: 0x04000E7A RID: 3706
	public const int TYPE_GAMEINFOSUB = 24;

	// Token: 0x04000E7B RID: 3707
	public const int TYPE_SPEACIALSKILL = 25;

	// Token: 0x04000E7C RID: 3708
	private int pointerDownTime;

	// Token: 0x04000E7D RID: 3709
	private int pointerDownFirstX;

	// Token: 0x04000E7E RID: 3710
	private int[] pointerDownLastX = new int[3];

	// Token: 0x04000E7F RID: 3711
	private bool pointerIsDowning;

	// Token: 0x04000E80 RID: 3712
	private bool isDownWhenRunning;

	// Token: 0x04000E81 RID: 3713
	private bool wantUpdateList;

	// Token: 0x04000E82 RID: 3714
	private int waitToPerform;

	// Token: 0x04000E83 RID: 3715
	private int cmRun;

	// Token: 0x04000E84 RID: 3716
	private int keyTouchLock = -1;

	// Token: 0x04000E85 RID: 3717
	private int keyToundGD = -1;

	// Token: 0x04000E86 RID: 3718
	private int keyTouchCombine = -1;

	// Token: 0x04000E87 RID: 3719
	private int keyTouchMapButton = -1;

	// Token: 0x04000E88 RID: 3720
	public int indexMouse = -1;

	// Token: 0x04000E89 RID: 3721
	private bool justRelease;

	// Token: 0x04000E8A RID: 3722
	private int keyTouchTab = -1;

	// Token: 0x04000E8B RID: 3723
	public string[][] clansOption;

	// Token: 0x04000E8C RID: 3724
	public string clanInfo = string.Empty;

	// Token: 0x04000E8D RID: 3725
	public string clanReport = string.Empty;

	// Token: 0x04000E8E RID: 3726
	private bool isHaveClan;

	// Token: 0x04000E8F RID: 3727
	private Scroll scroll;

	// Token: 0x04000E90 RID: 3728
	private int cmvx;

	// Token: 0x04000E91 RID: 3729
	private int cmdx;

	// Token: 0x04000E92 RID: 3730
	private bool isSelectPlayerMenu;

	// Token: 0x04000E93 RID: 3731
	private string[] strStatus = new string[]
	{
		mResources.follow,
		mResources.defend,
		mResources.attack,
		mResources.gohome,
		mResources.fusion,
		mResources.fusionForever
	};

	// Token: 0x04000E94 RID: 3732
	private int currentButtonPress;

	// Token: 0x04000E95 RID: 3733
	private int[] zoneColor = new int[]
	{
		43520,
		14743570,
		14155776
	};

	// Token: 0x04000E96 RID: 3734
	public string[] combineInfo;

	// Token: 0x04000E97 RID: 3735
	public string[] combineTopInfo;

	// Token: 0x04000E98 RID: 3736
	public static int[] color1 = new int[]
	{
		2327248,
		8982199,
		16713222
	};

	// Token: 0x04000E99 RID: 3737
	public static int[] color2 = new int[]
	{
		4583423,
		16719103,
		16714764
	};

	// Token: 0x04000E9A RID: 3738
	private bool isUp;

	// Token: 0x04000E9B RID: 3739
	private int compare;

	// Token: 0x04000E9C RID: 3740
	public static string strWantToBuy = string.Empty;

	// Token: 0x04000E9D RID: 3741
	public int xstart;

	// Token: 0x04000E9E RID: 3742
	public int ystart;

	// Token: 0x04000E9F RID: 3743
	public int popupW = 140;

	// Token: 0x04000EA0 RID: 3744
	public int popupH = 160;

	// Token: 0x04000EA1 RID: 3745
	public int cmySK;

	// Token: 0x04000EA2 RID: 3746
	public int cmtoYSK;

	// Token: 0x04000EA3 RID: 3747
	public int cmdySK;

	// Token: 0x04000EA4 RID: 3748
	public int cmvySK;

	// Token: 0x04000EA5 RID: 3749
	public int cmyLimSK;

	// Token: 0x04000EA6 RID: 3750
	public int popupY;

	// Token: 0x04000EA7 RID: 3751
	public int popupX;

	// Token: 0x04000EA8 RID: 3752
	public int isborderIndex;

	// Token: 0x04000EA9 RID: 3753
	public int isselectedRow;

	// Token: 0x04000EAA RID: 3754
	public int indexSize = 28;

	// Token: 0x04000EAB RID: 3755
	public int indexTitle;

	// Token: 0x04000EAC RID: 3756
	public int indexSelect;

	// Token: 0x04000EAD RID: 3757
	public int indexRow = -1;

	// Token: 0x04000EAE RID: 3758
	public int indexRowMax;

	// Token: 0x04000EAF RID: 3759
	public int indexMenu;

	// Token: 0x04000EB0 RID: 3760
	public int columns = 6;

	// Token: 0x04000EB1 RID: 3761
	public int rows;

	// Token: 0x04000EB2 RID: 3762
	public int inforX;

	// Token: 0x04000EB3 RID: 3763
	public int inforY;

	// Token: 0x04000EB4 RID: 3764
	public int inforW;

	// Token: 0x04000EB5 RID: 3765
	public int inforH;

	// Token: 0x04000EB6 RID: 3766
	private int yPaint;

	// Token: 0x04000EB7 RID: 3767
	private int xMap;

	// Token: 0x04000EB8 RID: 3768
	private int yMap;

	// Token: 0x04000EB9 RID: 3769
	private int xMapTask;

	// Token: 0x04000EBA RID: 3770
	private int yMapTask;

	// Token: 0x04000EBB RID: 3771
	private int xMove;

	// Token: 0x04000EBC RID: 3772
	private int yMove;

	// Token: 0x04000EBD RID: 3773
	public static bool isPaintMap = true;

	// Token: 0x04000EBE RID: 3774
	public bool isClose;

	// Token: 0x04000EBF RID: 3775
	private int infoSelect;

	// Token: 0x04000EC0 RID: 3776
	public static MyVector vGameInfo = new MyVector(string.Empty);

	// Token: 0x04000EC1 RID: 3777
	public static string[] contenInfo;

	// Token: 0x04000EC2 RID: 3778
	public bool isViewChatServer;

	// Token: 0x04000EC3 RID: 3779
	private int currInfoItem;

	// Token: 0x04000EC4 RID: 3780
	public global::Char charInfo;

	// Token: 0x04000EC5 RID: 3781
	private bool isChangeZone;

	// Token: 0x04000EC6 RID: 3782
	private bool isKiguiXu;

	// Token: 0x04000EC7 RID: 3783
	private bool isKiguiLuong;

	// Token: 0x04000EC8 RID: 3784
	private int delayKigui;

	// Token: 0x04000EC9 RID: 3785
	public sbyte combineSuccess = -1;

	// Token: 0x04000ECA RID: 3786
	public int idNPC;

	// Token: 0x04000ECB RID: 3787
	public int xS;

	// Token: 0x04000ECC RID: 3788
	public int yS;

	// Token: 0x04000ECD RID: 3789
	private int rS;

	// Token: 0x04000ECE RID: 3790
	private int angleS;

	// Token: 0x04000ECF RID: 3791
	private int angleO;

	// Token: 0x04000ED0 RID: 3792
	private int iAngleS;

	// Token: 0x04000ED1 RID: 3793
	private int iDotS;

	// Token: 0x04000ED2 RID: 3794
	private int speed;

	// Token: 0x04000ED3 RID: 3795
	private int[] xArgS;

	// Token: 0x04000ED4 RID: 3796
	private int[] yArgS;

	// Token: 0x04000ED5 RID: 3797
	private int[] xDotS;

	// Token: 0x04000ED6 RID: 3798
	private int[] yDotS;

	// Token: 0x04000ED7 RID: 3799
	private int time;

	// Token: 0x04000ED8 RID: 3800
	private int typeCombine;

	// Token: 0x04000ED9 RID: 3801
	private int countUpdate;

	// Token: 0x04000EDA RID: 3802
	private int countR;

	// Token: 0x04000EDB RID: 3803
	private int countWait;

	// Token: 0x04000EDC RID: 3804
	private bool isSpeedCombine;

	// Token: 0x04000EDD RID: 3805
	private bool isCompleteEffCombine = true;

	// Token: 0x04000EDE RID: 3806
	private bool isPaintCombine;

	// Token: 0x04000EDF RID: 3807
	public bool isDoneCombine = true;

	// Token: 0x04000EE0 RID: 3808
	public short iconID1;

	// Token: 0x04000EE1 RID: 3809
	public short iconID2;

	// Token: 0x04000EE2 RID: 3810
	public short iconID3;

	// Token: 0x04000EE3 RID: 3811
	public short[] iconID;

	// Token: 0x04000EE4 RID: 3812
	public string[][] speacialTabName;

	// Token: 0x04000EE5 RID: 3813
	public static int[] sizeUpgradeEff = new int[]
	{
		2,
		1,
		1
	};

	// Token: 0x04000EE6 RID: 3814
	public static int nsize = 1;

	// Token: 0x04000EE7 RID: 3815
	public const sbyte COLOR_WHITE = 0;

	// Token: 0x04000EE8 RID: 3816
	public const sbyte COLOR_GREEN = 1;

	// Token: 0x04000EE9 RID: 3817
	public const sbyte COLOR_PURPLE = 2;

	// Token: 0x04000EEA RID: 3818
	public const sbyte COLOR_ORANGE = 3;

	// Token: 0x04000EEB RID: 3819
	public const sbyte COLOR_BLUE = 4;

	// Token: 0x04000EEC RID: 3820
	public const sbyte COLOR_YELLOW = 5;

	// Token: 0x04000EED RID: 3821
	public const sbyte COLOR_RED = 6;

	// Token: 0x04000EEE RID: 3822
	public const sbyte COLOR_BLACK = 7;

	// Token: 0x04000EEF RID: 3823
	public static int[][] colorUpgradeEffect = new int[][]
	{
		new int[]
		{
			16777215,
			15000805,
			13487823,
			11711155,
			9671828,
			7895160
		},
		new int[]
		{
			61952,
			58624,
			52224,
			45824,
			39168,
			32768
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
			16744192,
			15037184,
			13395456,
			11753728,
			10046464,
			8404992
		},
		new int[]
		{
			37119,
			33509,
			28108,
			24499,
			21145,
			17536
		},
		new int[]
		{
			16776192,
			15063040,
			12635136,
			11776256,
			10063872,
			8290304
		},
		new int[]
		{
			16711680,
			15007744,
			13369344,
			11730944,
			10027008,
			8388608
		}
	};

	// Token: 0x04000EF0 RID: 3824
	public const int color_item_white = 15987701;

	// Token: 0x04000EF1 RID: 3825
	public const int color_item_green = 2786816;

	// Token: 0x04000EF2 RID: 3826
	public const int color_item_purple = 7078041;

	// Token: 0x04000EF3 RID: 3827
	public const int color_item_orange = 12537346;

	// Token: 0x04000EF4 RID: 3828
	public const int color_item_blue = 1269146;

	// Token: 0x04000EF5 RID: 3829
	public const int color_item_yellow = 13279744;

	// Token: 0x04000EF6 RID: 3830
	public const int color_item_red = 11599872;

	// Token: 0x04000EF7 RID: 3831
	public const int color_item_black = 2039326;

	// Token: 0x04000EF8 RID: 3832
	private Image imgo_0;

	// Token: 0x04000EF9 RID: 3833
	private Image imgo_1;

	// Token: 0x04000EFA RID: 3834
	private Image imgo_2;

	// Token: 0x04000EFB RID: 3835
	private Image imgo_3;

	// Token: 0x04000EFC RID: 3836
	public const int numItem = 20;

	// Token: 0x04000EFD RID: 3837
	public const sbyte INVENTORY_TAB = 1;

	// Token: 0x04000EFE RID: 3838
	public sbyte size_tab;
}
