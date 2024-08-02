using System;

// Token: 0x02000033 RID: 51
public class FrameImage
{
	// Token: 0x06000245 RID: 581 RVA: 0x00038B9C File Offset: 0x00036D9C
	public FrameImage(int ID)
	{
		this.Id = ID;
		Image image = Effect_End.getImage(ID);
		bool flag = image != null;
		if (flag)
		{
			this.imgFrame = image;
			this.frameWidth = (int)Effect_End.arrInfoEff[ID][0];
			this.frameHeight = (int)(Effect_End.arrInfoEff[ID][1] / Effect_End.arrInfoEff[ID][2]);
			this.nFrame = (int)Effect_End.arrInfoEff[ID][2];
		}
	}

	// Token: 0x06000246 RID: 582 RVA: 0x00038C10 File Offset: 0x00036E10
	public FrameImage(Image img, int width, int height)
	{
		bool flag = img != null;
		if (flag)
		{
			this.imgFrame = img;
			this.frameWidth = width;
			this.frameHeight = height;
			this.nFrame = img.getHeight() / height;
			bool flag2 = this.nFrame < 1;
			if (flag2)
			{
				this.nFrame = 1;
			}
		}
	}

	// Token: 0x06000247 RID: 583 RVA: 0x00038C70 File Offset: 0x00036E70
	public FrameImage(Image img, int numW, int numH, int numNull)
	{
		bool flag = img != null;
		if (flag)
		{
			this.imgFrame = img;
			this.numWidth = numW;
			this.numHeight = numH;
			this.frameWidth = this.imgFrame.getWidth() / numW;
			this.frameHeight = this.imgFrame.getHeight() / numH;
			this.nFrame = numW * numH - numNull;
		}
	}

	// Token: 0x06000248 RID: 584 RVA: 0x00038CE0 File Offset: 0x00036EE0
	public void drawFrame(int idx, int x, int y, int trans, int anchor, mGraphics g)
	{
		try
		{
			bool flag = this.imgFrame != null;
			if (flag)
			{
				bool flag2 = idx > this.nFrame;
				if (flag2)
				{
					idx = this.nFrame;
				}
				int num = idx * this.frameHeight;
				bool flag3 = num > this.frameHeight * (this.nFrame - 1) || num < 0;
				if (flag3)
				{
					num = this.frameHeight * (this.nFrame - 1);
				}
				g.drawRegion(this.imgFrame, 0, idx * this.frameHeight, this.frameWidth, this.frameHeight, trans, x, y, anchor);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0400055A RID: 1370
	public int frameWidth;

	// Token: 0x0400055B RID: 1371
	public int frameHeight;

	// Token: 0x0400055C RID: 1372
	public int nFrame;

	// Token: 0x0400055D RID: 1373
	public Image imgFrame;

	// Token: 0x0400055E RID: 1374
	public int Id = -1;

	// Token: 0x0400055F RID: 1375
	public int numWidth;

	// Token: 0x04000560 RID: 1376
	public int numHeight;
}
