using System;

namespace Assets.src.g
{
	// Token: 0x020000D7 RID: 215
	public class PetFollow
	{
		// Token: 0x06000AD9 RID: 2777 RVA: 0x0000695E File Offset: 0x00004B5E
		public PetFollow()
		{
			this.f = Res.random(0, 3);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000A8298 File Offset: 0x000A6498
		public void SetImg(int fimg, int[] frameNew, int wimg, int himg)
		{
			bool flag = fimg >= 1;
			if (flag)
			{
				this.fimg = fimg;
				this.frame = frameNew;
				this.wimg = wimg;
				this.himg = himg;
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000A82D0 File Offset: 0x000A64D0
		public void paint(mGraphics g)
		{
			int w = 32;
			int h = 32;
			int num = (GameCanvas.gameTick % 10 > 5) ? 1 : 0;
			bool flag = this.fimg > 0;
			if (flag)
			{
				w = this.wimg;
				h = this.himg;
				num = 0;
			}
			SmallImage.drawSmallImage(g, (int)this.smallID, this.f, this.cmx, this.cmy + 3 + num, w, h, (this.dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000A834C File Offset: 0x000A654C
		public void update()
		{
			this.moveCamera();
			bool flag = GameCanvas.gameTick % 3 == 0;
			if (flag)
			{
				this.f = this.frame[this.count];
				this.count++;
			}
			bool flag2 = this.count >= this.frame.Length;
			if (flag2)
			{
				this.count = 0;
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0000699E File Offset: 0x00004B9E
		public void remove()
		{
			ServerEffect.addServerEffect(60, this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 > 5) ? 1 : 0), 1);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000A83B4 File Offset: 0x000A65B4
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
		}

		// Token: 0x040013C3 RID: 5059
		public short smallID;

		// Token: 0x040013C4 RID: 5060
		public Info info = new Info();

		// Token: 0x040013C5 RID: 5061
		public int dir;

		// Token: 0x040013C6 RID: 5062
		public int f;

		// Token: 0x040013C7 RID: 5063
		public int tF;

		// Token: 0x040013C8 RID: 5064
		public int cmtoY;

		// Token: 0x040013C9 RID: 5065
		public int cmy;

		// Token: 0x040013CA RID: 5066
		public int cmdy;

		// Token: 0x040013CB RID: 5067
		public int cmvy;

		// Token: 0x040013CC RID: 5068
		public int cmyLim;

		// Token: 0x040013CD RID: 5069
		public int cmtoX;

		// Token: 0x040013CE RID: 5070
		public int cmx;

		// Token: 0x040013CF RID: 5071
		public int cmdx;

		// Token: 0x040013D0 RID: 5072
		public int cmvx;

		// Token: 0x040013D1 RID: 5073
		public int cmxLim;

		// Token: 0x040013D2 RID: 5074
		public int fimg = -1;

		// Token: 0x040013D3 RID: 5075
		public int wimg;

		// Token: 0x040013D4 RID: 5076
		public int himg;

		// Token: 0x040013D5 RID: 5077
		private int[] frame = new int[]
		{
			0,
			1,
			2,
			1
		};

		// Token: 0x040013D6 RID: 5078
		private int count;
	}
}
