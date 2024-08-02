using System;
using System.Threading;
using AssemblyCSharp.Mod.Xmap;

// Token: 0x02000046 RID: 70
public class InfoMe
{
	// Token: 0x060003AF RID: 943 RVA: 0x0005218C File Offset: 0x0005038C
	public InfoMe()
	{
		for (int i = 0; i < this.charId.Length; i++)
		{
			this.charId[i] = new int[3];
		}
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x000521E8 File Offset: 0x000503E8
	public static InfoMe gI()
	{
		bool flag = InfoMe.me == null;
		if (flag)
		{
			InfoMe.me = new InfoMe();
		}
		return InfoMe.me;
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x00052218 File Offset: 0x00050418
	public void loadCharId()
	{
		for (int i = 0; i < this.charId.Length; i++)
		{
			this.charId[i] = new int[3];
		}
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00052250 File Offset: 0x00050450
	public void paint(mGraphics g)
	{
		bool flag = (this.Equals(GameScr.info2) && GameScr.gI().isVS()) || (this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null) || !GameScr.isPaint || (GameCanvas.currentScreen != GameScr.gI() && GameCanvas.currentScreen != CrackBallScr.gI()) || ChatPopup.serverChatPopUp != null || !this.isUpdate || global::Char.ischangingMap || (GameCanvas.panel.isShow && this.Equals(GameScr.info2));
		if (!flag)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			bool flag2 = this.info != null;
			if (flag2)
			{
				this.info.paint(g, this.cmx, this.cmy, this.dir);
				bool flag3 = this.info.info == null || this.info.info.charInfo == null || this.cmdChat != null || !GameCanvas.isTouch;
				if (flag3)
				{
				}
				bool flag4 = this.info.info != null && this.info.info.charInfo != null && this.cmdChat == null;
				if (flag4)
				{
				}
			}
			bool flag5 = this.info.info != null && this.info.info.charInfo == null && this.charId != null;
			if (flag5)
			{
				SmallImage.drawSmallImage(g, this.charId[global::Char.myCharz().cgender][this.f], this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 > 5) ? 1 : 0), (this.dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
		}
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x0000495B File Offset: 0x00002B5B
	public void hide()
	{
		this.info.hide();
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00052448 File Offset: 0x00050648
	public void moveCamera()
	{
		bool flag = this.cmy != this.cmtoY;
		if (flag)
		{
			this.cmvy = this.cmtoY - this.cmy << 2;
			this.cmdy += this.cmvy;
			this.cmy += this.cmdy >> 4;
			this.cmdy &= 15;
		}
		bool flag2 = this.cmx != this.cmtoX;
		if (flag2)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 4;
			this.cmdx &= 15;
		}
		this.tF++;
		bool flag3 = this.tF == 5;
		if (flag3)
		{
			this.tF = 0;
			bool flag4 = this.f == 0;
			if (flag4)
			{
				this.f = 1;
			}
			else
			{
				this.f = 0;
			}
		}
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x0000496A File Offset: 0x00002B6A
	public void doClick(int t)
	{
		this.timeDelay = t;
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00052564 File Offset: 0x00050764
	public void update()
	{
		bool flag = this.info != null && this.info.infoWaitToShow != null && this.info.infoWaitToShow.size() == 0 && this.cmy != -40;
		if (flag)
		{
			this.info.timeW--;
			bool flag2 = this.info.timeW <= 0;
			if (flag2)
			{
				this.cmy = -40;
				this.info.time = 0;
				this.info.infoWaitToShow.removeAllElements();
				this.info.says = null;
				this.info.timeW = 200;
			}
		}
		bool flag3 = (this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null) || !this.isUpdate;
		if (!flag3)
		{
			this.moveCamera();
			bool flag4 = this.info == null || (this.info != null && this.info.info == null);
			if (!flag4)
			{
				bool flag5 = !this.isDone;
				if (flag5)
				{
					bool flag6 = this.timeDelay > 0;
					if (flag6)
					{
						this.timeDelay--;
						bool flag7 = this.timeDelay == 0;
						if (flag7)
						{
							GameCanvas.panel.setTypeMessage();
							GameCanvas.panel.show();
						}
					}
					bool flag8 = GameCanvas.gameTick % 3 == 0;
					if (flag8)
					{
						bool flag9 = global::Char.myCharz().cdir == 1;
						if (flag9)
						{
							this.cmtoX = global::Char.myCharz().cx - 20 - GameScr.cmx;
						}
						bool flag10 = global::Char.myCharz().cdir == -1;
						if (flag10)
						{
							this.cmtoX = global::Char.myCharz().cx + 20 - GameScr.cmx;
						}
						bool flag11 = this.cmtoX <= 24;
						if (flag11)
						{
							this.cmtoX += this.info.sayWidth / 2;
						}
						bool flag12 = this.cmtoX >= GameCanvas.w - 24;
						if (flag12)
						{
							this.cmtoX -= this.info.sayWidth / 2;
						}
						this.cmtoY = global::Char.myCharz().cy - 40 - GameScr.cmy;
						bool flag13 = this.info.says != null && this.cmtoY < (this.info.says.Length + 1) * 12 + 10;
						if (flag13)
						{
							this.cmtoY = (this.info.says.Length + 1) * 12 + 10;
						}
						bool flag14 = this.info.info.charInfo != null;
						if (flag14)
						{
							bool flag15 = GameCanvas.w - 50 > 155 + this.info.W;
							if (flag15)
							{
								this.cmtoX = GameCanvas.w - 60 - this.info.W / 2;
								this.cmtoY = this.info.H + 10;
							}
							else
							{
								this.cmtoX = GameCanvas.w - 20 - this.info.W / 2;
								this.cmtoY = 45 + this.info.H;
								bool flag16 = GameCanvas.w > GameCanvas.h || GameCanvas.w < 220;
								if (flag16)
								{
									this.cmtoX = GameCanvas.w - 20 - this.info.W / 2;
									this.cmtoY = this.info.H + 10;
								}
							}
						}
					}
					bool flag17 = this.cmx > global::Char.myCharz().cx - GameScr.cmx;
					if (flag17)
					{
						this.dir = -1;
					}
					else
					{
						this.dir = 1;
					}
				}
				bool flag18 = this.info.info == null;
				if (!flag18)
				{
					bool flag19 = this.info.infoWaitToShow.size() > 1;
					if (flag19)
					{
						bool flag20 = this.info.info.timeCount == 0;
						if (flag20)
						{
							this.info.time++;
							bool flag21 = this.info.time >= this.info.info.speed;
							if (flag21)
							{
								this.info.time = 0;
								this.info.infoWaitToShow.removeElementAt(0);
								InfoItem infoItem = (InfoItem)this.info.infoWaitToShow.firstElement();
								this.info.info = infoItem;
								this.info.getInfo();
							}
						}
						else
						{
							this.info.info.curr = mSystem.currentTimeMillis();
							bool flag22 = this.info.info.curr - this.info.info.last >= 100L;
							if (flag22)
							{
								this.info.info.last = mSystem.currentTimeMillis();
								this.info.info.timeCount--;
							}
							bool flag23 = this.info.info.timeCount == 0;
							if (flag23)
							{
								this.info.infoWaitToShow.removeElementAt(0);
								bool flag24 = this.info.infoWaitToShow.size() != 0;
								if (flag24)
								{
									InfoItem infoItem2 = (InfoItem)this.info.infoWaitToShow.firstElement();
									this.info.info = infoItem2;
									this.info.getInfo();
								}
							}
						}
					}
					else
					{
						bool flag25 = this.info.infoWaitToShow.size() != 1;
						if (!flag25)
						{
							bool flag26 = this.info.info.timeCount == 0;
							if (flag26)
							{
								this.info.time++;
								bool flag27 = this.info.time >= this.info.info.speed;
								if (flag27)
								{
									this.isDone = true;
								}
								bool flag28 = this.info.time == this.info.info.speed;
								if (flag28)
								{
									this.cmtoY = -40;
									this.cmtoX = global::Char.myCharz().cx - GameScr.cmx + ((global::Char.myCharz().cdir != 1) ? 20 : -20);
								}
								bool flag29 = this.info.time >= this.info.info.speed + 20;
								if (flag29)
								{
									this.info.time = 0;
									this.info.infoWaitToShow.removeAllElements();
									this.info.says = null;
									this.info.timeW = 200;
								}
							}
							else
							{
								this.info.info.curr = mSystem.currentTimeMillis();
								bool flag30 = this.info.info.curr - this.info.info.last >= 100L;
								if (flag30)
								{
									this.info.info.last = mSystem.currentTimeMillis();
									this.info.info.timeCount--;
								}
								bool flag31 = this.info.info.timeCount == 0;
								if (flag31)
								{
									this.isDone = true;
									this.cmtoY = -40;
									this.cmtoX = global::Char.myCharz().cx - GameScr.cmx + ((global::Char.myCharz().cdir != 1) ? 20 : -20);
									this.info.time = 0;
									this.info.infoWaitToShow.removeAllElements();
									this.info.says = null;
									this.cmdChat = null;
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x00004974 File Offset: 0x00002B74
	public void addInfoWithChar(string s, global::Char c, bool isChatServer)
	{
		this.playerID = c.charID;
		this.info.addInfo(s, 3, c, isChatServer);
		this.isDone = false;
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x00052D54 File Offset: 0x00050F54
	public void addInfo(string s, int Type)
	{
		Pk9rXmap.Info(s);
		bool flag = s.Trim().ToLower().Contains("nhiệm vụ của bạn") || s.ToLower().Contains("chào mừng các bạn") || s.ToLower().Contains("xin chào các") || s.Contains(global::Char.myCharz().taskMaint.subNames[global::Char.myCharz().taskMaint.index]);
		if (flag)
		{
			new Thread(new ThreadStart(VuDang.VeKhu)).Start();
			bool flag2 = ItemTime.isExistItem(4387);
			if (flag2)
			{
				GameScr.isAutoPlay = true;
			}
		}
		bool flag3 = (VuDang.isThuongDeThuong || VuDang.isThuongDeVip) && s.ToLower().Contains("đầy");
		if (flag3)
		{
			new Thread(new ThreadStart(VuDang.NhanAllThuongDe)).Start();
		}
		s = Res.changeString(s);
		bool flag4 = this.info.infoWaitToShow.size() > 0 && s.Equals(((InfoItem)this.info.infoWaitToShow.lastElement()).s);
		if (!flag4)
		{
			bool flag5 = this.info.infoWaitToShow.size() > 10;
			if (flag5)
			{
				for (int i = 0; i < 5; i++)
				{
					this.info.infoWaitToShow.removeElementAt(0);
				}
			}
			global::Char cInfo = null;
			this.info.addInfo(s, Type, cInfo, false);
			bool flag6 = this.info.infoWaitToShow.size() == 1;
			if (flag6)
			{
				this.cmy = 0;
				this.cmx = global::Char.myCharz().cx - GameScr.cmx + ((global::Char.myCharz().cdir != 1) ? 20 : -20);
			}
			this.isDone = false;
		}
	}

	// Token: 0x04000847 RID: 2119
	public static InfoMe me;

	// Token: 0x04000848 RID: 2120
	public int[][] charId = new int[3][];

	// Token: 0x04000849 RID: 2121
	public Info info = new Info();

	// Token: 0x0400084A RID: 2122
	public int dir;

	// Token: 0x0400084B RID: 2123
	public int f;

	// Token: 0x0400084C RID: 2124
	public int tF;

	// Token: 0x0400084D RID: 2125
	public int cmtoY;

	// Token: 0x0400084E RID: 2126
	public int cmy;

	// Token: 0x0400084F RID: 2127
	public int cmdy;

	// Token: 0x04000850 RID: 2128
	public int cmvy;

	// Token: 0x04000851 RID: 2129
	public int cmyLim;

	// Token: 0x04000852 RID: 2130
	public int cmtoX;

	// Token: 0x04000853 RID: 2131
	public int cmx;

	// Token: 0x04000854 RID: 2132
	public int cmdx;

	// Token: 0x04000855 RID: 2133
	public int cmvx;

	// Token: 0x04000856 RID: 2134
	public int cmxLim;

	// Token: 0x04000857 RID: 2135
	public bool isDone;

	// Token: 0x04000858 RID: 2136
	public bool isUpdate = true;

	// Token: 0x04000859 RID: 2137
	public int timeDelay;

	// Token: 0x0400085A RID: 2138
	public int playerID;

	// Token: 0x0400085B RID: 2139
	public int timeCount;

	// Token: 0x0400085C RID: 2140
	public Command cmdChat;

	// Token: 0x0400085D RID: 2141
	public bool isShow;
}
