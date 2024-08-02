using System;

// Token: 0x02000072 RID: 114
public class MsgDlg : Dialog
{
	// Token: 0x06000570 RID: 1392 RVA: 0x00063608 File Offset: 0x00061808
	public MsgDlg()
	{
		this.padLeft = 35;
		bool flag = GameCanvas.w <= 176;
		if (flag)
		{
			this.padLeft = 10;
		}
		bool flag2 = GameCanvas.w > 320;
		if (flag2)
		{
			this.padLeft = 80;
		}
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x00005170 File Offset: 0x00003370
	public void pleasewait()
	{
		this.setInfo(mResources.PLEASEWAIT, null, null, null);
		GameCanvas.currentDialog = this;
		this.time = mSystem.currentTimeMillis() + 5000L;
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x0000519A File Offset: 0x0000339A
	public override void show()
	{
		GameCanvas.currentDialog = this;
		this.time = -1L;
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x00063664 File Offset: 0x00061864
	public void setInfo(string info)
	{
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
		this.h = 80;
		bool flag = this.info.Length >= 5;
		if (flag)
		{
			this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
		}
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x000636CC File Offset: 0x000618CC
	public void setInfo(string info, Command left, Command center, Command right)
	{
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
		this.left = left;
		this.center = center;
		this.right = right;
		this.h = 80;
		bool flag = this.info.Length >= 5;
		if (flag)
		{
			this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
		}
		bool isTouch = GameCanvas.isTouch;
		if (isTouch)
		{
			bool flag2 = left != null;
			if (flag2)
			{
				this.left.x = GameCanvas.w / 2 - 68 - 5;
				this.left.y = GameCanvas.h - 50;
			}
			bool flag3 = right != null;
			if (flag3)
			{
				this.right.x = GameCanvas.w / 2 + 5;
				this.right.y = GameCanvas.h - 50;
			}
			bool flag4 = center != null;
			if (flag4)
			{
				this.center.x = GameCanvas.w / 2 - 35;
				this.center.y = GameCanvas.h - 50;
			}
		}
		this.isWait = false;
		this.time = -1L;
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x00063804 File Offset: 0x00061A04
	public override void paint(mGraphics g)
	{
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		bool flag = !LoginScr.isContinueToLogin;
		if (flag)
		{
			int num = GameCanvas.h - this.h - 38;
			int w = GameCanvas.w - this.padLeft * 2;
			GameCanvas.paintz.paintPopUp(this.padLeft, num, w, this.h, g);
			int num2 = num + (this.h - this.info.Length * mFont.tahoma_8b.getHeight()) / 2 - 2;
			bool flag2 = this.isWait;
			if (flag2)
			{
				num2 += 8;
				GameCanvas.paintShukiren(GameCanvas.hw, num2 - 12, g);
			}
			int i = 0;
			int num3 = num2;
			while (i < this.info.Length)
			{
				mFont.tahoma_7b_dark.drawString(g, this.info[i], GameCanvas.hw, num3, 2);
				i++;
				num3 += mFont.tahoma_8b.getHeight();
			}
			base.paint(g);
		}
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x00063908 File Offset: 0x00061B08
	public override void update()
	{
		base.update();
		bool flag = this.time != -1L && mSystem.currentTimeMillis() > this.time;
		if (flag)
		{
			GameCanvas.endDlg();
		}
	}

	// Token: 0x04000D3B RID: 3387
	public string[] info;

	// Token: 0x04000D3C RID: 3388
	public bool isWait;

	// Token: 0x04000D3D RID: 3389
	private int h;

	// Token: 0x04000D3E RID: 3390
	private int padLeft;

	// Token: 0x04000D3F RID: 3391
	private long time = -1L;
}
