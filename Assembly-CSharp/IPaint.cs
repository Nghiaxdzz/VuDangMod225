using System;

// Token: 0x0200004C RID: 76
public abstract class IPaint
{
	// Token: 0x060003E2 RID: 994
	public abstract void paintDefaultBg(mGraphics g);

	// Token: 0x060003E3 RID: 995
	public abstract void paintfillDefaultBg(mGraphics g);

	// Token: 0x060003E4 RID: 996
	public abstract void repaintCircleBg();

	// Token: 0x060003E5 RID: 997
	public abstract void paintSolidBg(mGraphics g);

	// Token: 0x060003E6 RID: 998
	public abstract void paintDefaultPopup(mGraphics g, int x, int y, int w, int h);

	// Token: 0x060003E7 RID: 999
	public abstract void paintWhitePopup(mGraphics g, int y, int x, int width, int height);

	// Token: 0x060003E8 RID: 1000
	public abstract void paintDefaultPopupH(mGraphics g, int h);

	// Token: 0x060003E9 RID: 1001
	public abstract void paintCmdBar(mGraphics g, Command left, Command center, Command right);

	// Token: 0x060003EA RID: 1002
	public abstract void paintSelect(mGraphics g, int x, int y, int w, int h);

	// Token: 0x060003EB RID: 1003
	public abstract void paintLogo(mGraphics g, int x, int y);

	// Token: 0x060003EC RID: 1004
	public abstract void paintHotline(mGraphics g, string num);

	// Token: 0x060003ED RID: 1005
	public abstract void paintInputTf(mGraphics g, bool iss, int x, int y, int w, int h, int xText, int yText, string text);

	// Token: 0x060003EE RID: 1006
	public abstract void paintTabSoft(mGraphics g);

	// Token: 0x060003EF RID: 1007
	public abstract void paintBackMenu(mGraphics g, int x, int y, int w, int h, bool iss);

	// Token: 0x060003F0 RID: 1008
	public abstract void paintMsgBG(mGraphics g, int x, int y, int w, int h, string title, string subTitle, string check);

	// Token: 0x060003F1 RID: 1009
	public abstract void paintDefaultScrLisst(mGraphics g, string title, string subTitle, string check);

	// Token: 0x060003F2 RID: 1010
	public abstract void paintCheck(mGraphics g, int x, int y, int index);

	// Token: 0x060003F3 RID: 1011
	public abstract void paintImgMsg(mGraphics g, int x, int y, int index);

	// Token: 0x060003F4 RID: 1012
	public abstract void paintTitleBoard(mGraphics g, int roomID);

	// Token: 0x060003F5 RID: 1013
	public abstract void paintCheckPass(mGraphics g, int x, int y, bool check, bool focus);

	// Token: 0x060003F6 RID: 1014
	public abstract void paintInputDlg(mGraphics g, int x, int y, int w, int h, string[] str);

	// Token: 0x060003F7 RID: 1015
	public abstract void paintIconMainMenu(mGraphics g, int x, int y, bool iss, bool issSe, int i, int wStr);

	// Token: 0x060003F8 RID: 1016
	public abstract void paintLineRoom(mGraphics g, int x, int y, int xTo, int yTo);

	// Token: 0x060003F9 RID: 1017
	public abstract void paintCellContaint(mGraphics g, int x, int y, int w, int h, bool iss);

	// Token: 0x060003FA RID: 1018
	public abstract void paintScroll(mGraphics g, int x, int y, int h);

	// Token: 0x060003FB RID: 1019
	public abstract int[] getColorMsg();

	// Token: 0x060003FC RID: 1020
	public abstract void paintLogo(mGraphics g);

	// Token: 0x060003FD RID: 1021
	public abstract void paintTextLogin(mGraphics g, bool issRes);

	// Token: 0x060003FE RID: 1022
	public abstract void paintSellectBoard(mGraphics g, int x, int y, int w, int h);

	// Token: 0x060003FF RID: 1023
	public abstract int issRegissterUsingWAP();

	// Token: 0x06000400 RID: 1024
	public abstract string getCard();

	// Token: 0x06000401 RID: 1025
	public abstract void paintSellectedShop(mGraphics g, int x, int y, int w, int h);

	// Token: 0x06000402 RID: 1026
	public abstract string getUrlUpdateGame();

	// Token: 0x06000403 RID: 1027 RVA: 0x00053AA0 File Offset: 0x00051CA0
	public string getFAQLink()
	{
		return "http://wap.teamobi.com/faqs.php?provider=";
	}

	// Token: 0x06000404 RID: 1028
	public abstract void doSelect(int focus);
}
