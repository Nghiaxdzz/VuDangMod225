using System;

namespace Assets.src.e
{
	// Token: 0x020000DB RID: 219
	public class Small
	{
		// Token: 0x06000B00 RID: 2816 RVA: 0x00006A2A File Offset: 0x00004C2A
		public Small(Image img, int id)
		{
			this.img = img;
			this.id = id;
			this.timePaint = 0;
			this.timeUpdate = 0;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000AD6A4 File Offset: 0x000AB8A4
		public void paint(mGraphics g, int transform, int x, int y, int anchor)
		{
			g.drawRegion(this.img, 0, 0, mGraphics.getImageWidth(this.img), mGraphics.getImageHeight(this.img), transform, x, y, anchor);
			bool flag = GameCanvas.gameTick % 1000 == 0;
			if (flag)
			{
				this.timePaint++;
				this.timeUpdate = this.timePaint;
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000AD70C File Offset: 0x000AB90C
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor)
		{
			this.paint(g, transform, f, x, y, w, h, anchor, false);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000AD730 File Offset: 0x000AB930
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor, bool isClip)
		{
			bool flag = mGraphics.getImageWidth(this.img) != 1;
			if (flag)
			{
				g.drawRegion(this.img, 0, f * w, w, h, transform, x, y, anchor, isClip);
				bool flag2 = GameCanvas.gameTick % 1000 == 0;
				if (flag2)
				{
					this.timePaint++;
					this.timeUpdate = this.timePaint;
				}
			}
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000AD7A4 File Offset: 0x000AB9A4
		public void update()
		{
			this.timeUpdate++;
			bool flag = this.timeUpdate - this.timePaint > 1 && !global::Char.myCharz().isCharBodyImageID(this.id);
			if (flag)
			{
				SmallImage.imgNew[this.id] = null;
			}
		}

		// Token: 0x0400141C RID: 5148
		public Image img;

		// Token: 0x0400141D RID: 5149
		public int id;

		// Token: 0x0400141E RID: 5150
		public int timePaint;

		// Token: 0x0400141F RID: 5151
		public int timeUpdate;
	}
}
