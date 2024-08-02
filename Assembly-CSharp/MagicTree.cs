using System;

// Token: 0x0200005E RID: 94
public class MagicTree : Npc, IActionListener
{
	// Token: 0x06000478 RID: 1144 RVA: 0x00057AA4 File Offset: 0x00055CA4
	public MagicTree(int npcId, int status, int cx, int cy, int templateId, int iconId) : base(npcId, status, cx, cy, templateId, iconId)
	{
		this.p = new PopUp(string.Empty, 0, 0);
		this.p.command = new Command(null, this, 1, null);
		PopUp.addPopUp(this.p);
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x00057AF4 File Offset: 0x00055CF4
	public override void paint(mGraphics g)
	{
		bool flag = this.id == 0;
		if (!flag)
		{
			SmallImage.drawSmallImage(g, this.id, this.cx, this.cy, 0, StaticObj.BOTTOM_HCENTER);
			bool flag2 = global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this);
			if (flag2)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 1, mGraphics.BOTTOM | mGraphics.HCENTER);
				bool flag3 = this.name != null;
				if (flag3)
				{
					mFont.tahoma_7b_white.drawString(g, this.name, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 20, mFont.CENTER, mFont.tahoma_7_grey);
				}
			}
			else
			{
				bool flag4 = this.name != null;
				if (flag4)
				{
					mFont.tahoma_7b_white.drawString(g, this.name, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 17, mFont.CENTER, mFont.tahoma_7_grey);
				}
			}
			try
			{
				for (int i = 0; i < this.currPeas; i++)
				{
					g.drawImage(MagicTree.pea, this.cx + this.peaPostionX[i] - SmallImage.smallImg[this.id][3] / 2, this.cy + this.peaPostionY[i] - SmallImage.smallImg[this.id][4], 0);
				}
			}
			catch (Exception)
			{
			}
			bool flag5 = this.indexEffTask < 0 || this.effTask == null || this.cTypePk != 0;
			if (!flag5)
			{
				SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx + SmallImage.smallImg[this.id][3] / 2 + 5, this.cy - 15 + this.effTask.arrEfInfo[this.indexEffTask].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				bool flag6 = GameCanvas.gameTick % 2 == 0;
				if (flag6)
				{
					this.indexEffTask++;
					bool flag7 = this.indexEffTask >= this.effTask.arrEfInfo.Length;
					if (flag7)
					{
						this.indexEffTask = 0;
					}
				}
			}
		}
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x00057D9C File Offset: 0x00055F9C
	public override void update()
	{
		this.p.isPaint = MagicTree.isPaint;
		this.cur = mSystem.currentTimeMillis();
		bool flag = this.cur - this.last >= 1000L;
		if (flag)
		{
			this.seconds--;
			this.last = this.cur;
			bool flag2 = this.seconds < 0;
			if (flag2)
			{
				this.seconds = 0;
			}
		}
		bool flag3 = !this.isUpdate;
		if (flag3)
		{
			bool flag4 = this.currPeas < this.maxPeas && this.seconds == 0;
			if (flag4)
			{
				this.waitToUpdate = true;
			}
		}
		else
		{
			bool flag5 = this.seconds == 0;
			if (flag5)
			{
				this.isUpdate = false;
				this.waitToUpdate = true;
			}
		}
		bool flag6 = this.waitToUpdate;
		if (flag6)
		{
			this.delay++;
			bool flag7 = this.delay == 20;
			if (flag7)
			{
				this.delay = 0;
				this.waitToUpdate = false;
				Service.gI().getMagicTree(2);
			}
		}
		this.num = ((this.peaPostionX != null) ? (this.peaPostionX.Length * this.currPeas / this.maxPeas) : 0);
		bool flag8 = this.isUpdateTree;
		if (flag8)
		{
			this.isUpdateTree = false;
			bool flag9 = (this.seconds >= 0 && this.currPeas < this.maxPeas) || (this.seconds >= 0 && this.isUpdate) || this.isPeasEffect;
			if (flag9)
			{
				this.p.updateXYWH(new string[]
				{
					this.isUpdate ? mResources.UPGRADING : (this.currPeas + "/" + this.maxPeas),
					NinjaUtil.getTime(this.seconds)
				}, this.cx, this.cy - 20 - SmallImage.smallImg[this.id][4]);
			}
			else
			{
				bool flag10 = this.currPeas == this.maxPeas && !this.isUpdate;
				if (flag10)
				{
					this.p.updateXYWH(new string[]
					{
						mResources.can_harvest,
						this.currPeas + "/" + this.maxPeas
					}, this.cx, this.cy - 20 - SmallImage.smallImg[this.id][4]);
				}
			}
		}
		bool flag11 = (this.seconds >= 0 && this.currPeas < this.maxPeas) || (this.seconds >= 0 && this.isUpdate);
		if (flag11)
		{
			this.p.says[this.p.says.Length - 1] = NinjaUtil.getTime(this.seconds);
		}
		bool flag12 = this.isPeasEffect;
		if (flag12)
		{
			this.p.isPaint = false;
			ServerEffect.addServerEffect(98, this.cx + this.peaPostionX[this.currPeas - 1] - SmallImage.smallImg[this.id][3] / 2, this.cy + this.peaPostionY[this.currPeas - 1] - SmallImage.smallImg[this.id][4], 1);
			this.currPeas--;
			bool flag13 = GameCanvas.gameTick % 2 == 0;
			if (flag13)
			{
				SoundMn.gI().HP_MPup();
			}
			bool flag14 = this.currPeas == this.remainPeas;
			if (flag14)
			{
				this.p.isPaint = true;
				this.isUpdateTree = true;
				this.isPeasEffect = false;
			}
		}
		base.update();
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x00058148 File Offset: 0x00056348
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 1;
		if (flag)
		{
			Service.gI().magicTree(1);
		}
	}

	// Token: 0x040009BA RID: 2490
	public static Image imgMagicTree;

	// Token: 0x040009BB RID: 2491
	public static Image pea = GameCanvas.loadImage("/mainImage/myTexture2dhatdau.png");

	// Token: 0x040009BC RID: 2492
	public int id;

	// Token: 0x040009BD RID: 2493
	public int level;

	// Token: 0x040009BE RID: 2494
	public int x;

	// Token: 0x040009BF RID: 2495
	public int y;

	// Token: 0x040009C0 RID: 2496
	public int currPeas;

	// Token: 0x040009C1 RID: 2497
	public int remainPeas;

	// Token: 0x040009C2 RID: 2498
	public int maxPeas;

	// Token: 0x040009C3 RID: 2499
	public new string strInfo;

	// Token: 0x040009C4 RID: 2500
	public string name;

	// Token: 0x040009C5 RID: 2501
	public int timeToRecieve;

	// Token: 0x040009C6 RID: 2502
	public bool isUpdate;

	// Token: 0x040009C7 RID: 2503
	public int[] peaPostionX;

	// Token: 0x040009C8 RID: 2504
	public int[] peaPostionY;

	// Token: 0x040009C9 RID: 2505
	private int num;

	// Token: 0x040009CA RID: 2506
	public PopUp p;

	// Token: 0x040009CB RID: 2507
	public bool isUpdateTree;

	// Token: 0x040009CC RID: 2508
	public new static bool isPaint = true;

	// Token: 0x040009CD RID: 2509
	public bool isPeasEffect;

	// Token: 0x040009CE RID: 2510
	public new int seconds;

	// Token: 0x040009CF RID: 2511
	public new long last;

	// Token: 0x040009D0 RID: 2512
	public new long cur;

	// Token: 0x040009D1 RID: 2513
	private int wPopUp;

	// Token: 0x040009D2 RID: 2514
	private bool waitToUpdate;

	// Token: 0x040009D3 RID: 2515
	private int delay;
}
