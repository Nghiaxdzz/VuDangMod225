using System;

// Token: 0x020000B5 RID: 181
public class TabClanIcon : IActionListener
{
	// Token: 0x06000962 RID: 2402 RVA: 0x000987B4 File Offset: 0x000969B4
	public TabClanIcon()
	{
		this.left = new Command(mResources.SELECT, this, 1, null);
		this.right = new Command(mResources.CLOSE, this, 2, null);
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x0009880C File Offset: 0x00096A0C
	public void init()
	{
		bool flag = this.isGetName;
		if (flag)
		{
			this.w = 170;
			this.h = 118;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2;
		}
		else
		{
			this.w = 170;
			this.h = 170;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2;
			bool flag2 = GameCanvas.h < 240;
			if (flag2)
			{
				this.y -= 10;
			}
		}
		this.cmx = this.x;
		this.cmtoX = 0;
		bool flag3 = !this.isRequest;
		if (flag3)
		{
			this.nItem = ClanImage.vClanImage.size();
		}
		else
		{
			this.nItem = this.vItems.size();
		}
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			this.left.x = this.x;
			this.left.y = this.y + this.h + 5;
			this.right.x = this.x + this.w - 68;
			this.right.y = this.y + this.h + 5;
		}
		TabClanIcon.scrMain = new Scroll();
		TabClanIcon.scrMain.setStyle(this.nItem, this.WIDTH, this.x, this.y + this.disStart, this.w, this.h - this.disStart, true, 1);
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x000989C8 File Offset: 0x00096BC8
	public void show(bool isGetName)
	{
		bool flag = global::Char.myCharz().clan != null;
		if (flag)
		{
			this.isUpdate = true;
		}
		this.isShow = true;
		this.isGetName = isGetName;
		this.init();
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x0000634F File Offset: 0x0000454F
	public void showRequest(int msgID)
	{
		this.isShow = true;
		this.isRequest = true;
		this.msgID = msgID;
		this.init();
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x0000636E File Offset: 0x0000456E
	public void hide()
	{
		this.cmtoX = this.x + this.w;
		SmallImage.clearHastable();
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void paintPeans(mGraphics g)
	{
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x00098A08 File Offset: 0x00096C08
	public void paintIcon(mGraphics g)
	{
		g.translate(-this.cmx, 0);
		PopUp.paintPopUp(g, this.x, this.y - 17, this.w, this.h + 17, -1, true);
		mFont.tahoma_7b_dark.drawString(g, mResources.select_clan_icon, this.x + this.w / 2, this.y - 7, 2);
		bool flag = this.lastSelect >= 0 && this.lastSelect <= ClanImage.vClanImage.size() - 1;
		if (flag)
		{
			ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect);
			bool flag2 = clanImage.idImage != null;
			if (flag2)
			{
				global::Char.myCharz().paintBag(g, clanImage.idImage, GameCanvas.w / 2, this.y + 45, 1, false);
			}
		}
		global::Char.myCharz().paintCharBody(g, GameCanvas.w / 2, this.y + 45, 1, global::Char.myCharz().cf, false);
		g.setClip(this.x, this.y + this.disStart, this.w, this.h - this.disStart - 10);
		bool flag3 = TabClanIcon.scrMain != null;
		if (flag3)
		{
			g.translate(0, -TabClanIcon.scrMain.cmy);
		}
		for (int i = 0; i < this.nItem; i++)
		{
			int num = this.x + 10;
			int num2 = this.y + i * this.WIDTH + this.disStart;
			bool flag4 = num2 + this.WIDTH - ((TabClanIcon.scrMain != null) ? TabClanIcon.scrMain.cmy : 0) >= this.y + this.disStart && num2 - ((TabClanIcon.scrMain != null) ? TabClanIcon.scrMain.cmy : 0) <= this.y + this.disStart + this.h;
			if (flag4)
			{
				ClanImage clanImage2 = (ClanImage)ClanImage.vClanImage.elementAt(i);
				mFont mFont = mFont.tahoma_7_grey;
				bool flag5 = i == this.lastSelect;
				if (flag5)
				{
					mFont = mFont.tahoma_7_blue;
				}
				bool flag6 = clanImage2.name != null;
				if (flag6)
				{
					mFont.drawString(g, clanImage2.name, num + 20, num2, 0);
				}
				bool flag7 = clanImage2.xu > 0;
				if (flag7)
				{
					mFont.drawString(g, clanImage2.xu + " " + mResources.XU, num + this.w - 20, num2, mFont.RIGHT);
				}
				else
				{
					bool flag8 = clanImage2.luong > 0;
					if (flag8)
					{
						mFont.drawString(g, clanImage2.luong + " " + mResources.LUONG, num + this.w - 20, num2, mFont.RIGHT);
					}
				}
				bool flag9 = clanImage2.idImage != null;
				if (flag9)
				{
					SmallImage.drawSmallImage(g, (int)clanImage2.idImage[0], num, num2, 0, 0);
				}
			}
		}
		g.translate(0, -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x00098D6C File Offset: 0x00096F6C
	public void paint(mGraphics g)
	{
		bool flag = !this.isRequest;
		if (flag)
		{
			this.paintIcon(g);
		}
		else
		{
			this.paintPeans(g);
		}
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x00098DA0 File Offset: 0x00096FA0
	public void update()
	{
		bool flag = TabClanIcon.scrMain != null;
		if (flag)
		{
			TabClanIcon.scrMain.updatecm();
		}
		bool flag2 = this.cmx != this.cmtoX;
		if (flag2)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 3;
			this.cmdx &= 15;
		}
		bool flag3 = global::Math.abs(this.cmtoX - this.cmx) < 10;
		if (flag3)
		{
			this.cmx = this.cmtoX;
		}
		bool flag4 = this.cmx >= this.x + this.w - 10 && this.cmtoX >= this.x + this.w - 10;
		if (flag4)
		{
			this.isShow = false;
		}
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x00098E98 File Offset: 0x00097098
	public void updateKey()
	{
		bool flag = this.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.left));
		if (flag)
		{
			this.left.performAction();
		}
		bool flag2 = this.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right));
		if (flag2)
		{
			this.right.performAction();
		}
		bool flag3 = this.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.center));
		if (flag3)
		{
			this.center.performAction();
		}
		bool flag4 = !this.isGetName;
		if (flag4)
		{
			bool flag5 = TabClanIcon.scrMain == null;
			if (flag5)
			{
				return;
			}
			bool isTouch = GameCanvas.isTouch;
			if (isTouch)
			{
				TabClanIcon.scrMain.updateKey();
				this.select = TabClanIcon.scrMain.selectedItem;
			}
			bool flag6 = GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21];
			if (flag6)
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = false;
				this.select--;
				bool flag7 = this.select < 0;
				if (flag7)
				{
					this.select = this.nItem - 1;
				}
				TabClanIcon.scrMain.moveTo(this.select * TabClanIcon.scrMain.ITEM_SIZE);
			}
			bool flag8 = GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22];
			if (flag8)
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
				this.select++;
				bool flag9 = this.select > this.nItem - 1;
				if (flag9)
				{
					this.select = 0;
				}
				TabClanIcon.scrMain.moveTo(this.select * TabClanIcon.scrMain.ITEM_SIZE);
			}
			bool flag10 = this.select != -1;
			if (flag10)
			{
				this.lastSelect = this.select;
			}
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x000990B8 File Offset: 0x000972B8
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 2;
		if (flag)
		{
			this.hide();
		}
		bool flag2 = idAction != 1 || this.isGetName;
		if (!flag2)
		{
			bool flag3 = !this.isRequest;
			if (flag3)
			{
				bool flag4 = this.lastSelect >= 0;
				if (flag4)
				{
					this.hide();
					bool flag5 = global::Char.myCharz().clan == null;
					if (flag5)
					{
						Service.gI().getClan(2, (sbyte)((ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect)).ID, this.text);
					}
					else
					{
						Service.gI().getClan(4, (sbyte)((ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect)).ID, string.Empty);
					}
				}
			}
			else
			{
				bool flag6 = this.lastSelect >= 0;
				if (flag6)
				{
					Item item = (Item)this.vItems.elementAt(this.select);
				}
			}
		}
	}

	// Token: 0x04001158 RID: 4440
	private int x;

	// Token: 0x04001159 RID: 4441
	private int y;

	// Token: 0x0400115A RID: 4442
	private int w;

	// Token: 0x0400115B RID: 4443
	private int h;

	// Token: 0x0400115C RID: 4444
	private Command left;

	// Token: 0x0400115D RID: 4445
	private Command right;

	// Token: 0x0400115E RID: 4446
	private Command center;

	// Token: 0x0400115F RID: 4447
	private int WIDTH = 24;

	// Token: 0x04001160 RID: 4448
	public int nItem;

	// Token: 0x04001161 RID: 4449
	private int disStart = 50;

	// Token: 0x04001162 RID: 4450
	public static Scroll scrMain;

	// Token: 0x04001163 RID: 4451
	public int cmtoX;

	// Token: 0x04001164 RID: 4452
	public int cmx;

	// Token: 0x04001165 RID: 4453
	public int cmvx;

	// Token: 0x04001166 RID: 4454
	public int cmdx;

	// Token: 0x04001167 RID: 4455
	public bool isShow;

	// Token: 0x04001168 RID: 4456
	public bool isGetName;

	// Token: 0x04001169 RID: 4457
	public string text;

	// Token: 0x0400116A RID: 4458
	private bool isRequest;

	// Token: 0x0400116B RID: 4459
	private bool isUpdate;

	// Token: 0x0400116C RID: 4460
	public MyVector vItems = new MyVector();

	// Token: 0x0400116D RID: 4461
	private int msgID;

	// Token: 0x0400116E RID: 4462
	private int select;

	// Token: 0x0400116F RID: 4463
	private int lastSelect;

	// Token: 0x04001170 RID: 4464
	private ScrollResult sr;
}
