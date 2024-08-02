using System;

// Token: 0x0200006E RID: 110
public class MotherCanvas
{
	// Token: 0x06000552 RID: 1362 RVA: 0x00005035 File Offset: 0x00003235
	public MotherCanvas()
	{
		this.checkZoomLevel(this.getWidth(), this.getHeight());
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x00061B14 File Offset: 0x0005FD14
	public void checkZoomLevel(int w, int h)
	{
		bool isWindowsPhone = Main.isWindowsPhone;
		if (isWindowsPhone)
		{
			mGraphics.zoomLevel = 2;
			bool flag = w * h >= 2073600;
			if (flag)
			{
				mGraphics.zoomLevel = 4;
			}
			else
			{
				bool flag2 = w * h > 384000;
				if (flag2)
				{
					mGraphics.zoomLevel = 3;
				}
			}
		}
		else
		{
			bool flag3 = !Main.isPC;
			if (flag3)
			{
				bool isIpod = Main.isIpod;
				if (isIpod)
				{
					mGraphics.zoomLevel = 2;
				}
				else
				{
					bool flag4 = w * h >= 2073600;
					if (flag4)
					{
						mGraphics.zoomLevel = 4;
					}
					else
					{
						bool flag5 = w * h >= 691200;
						if (flag5)
						{
							mGraphics.zoomLevel = 3;
						}
						else
						{
							bool flag6 = w * h > 153600;
							if (flag6)
							{
								mGraphics.zoomLevel = 2;
							}
						}
					}
				}
			}
			else
			{
				mGraphics.zoomLevel = 2;
				bool flag7 = w * h < 480000;
				if (flag7)
				{
					mGraphics.zoomLevel = 1;
				}
			}
		}
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x00039F70 File Offset: 0x00038170
	public int getWidth()
	{
		return (int)ScaleGUI.WIDTH;
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x00039F88 File Offset: 0x00038188
	public int getHeight()
	{
		return (int)ScaleGUI.HEIGHT;
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x00005061 File Offset: 0x00003261
	public void setChildCanvas(GameCanvas tCanvas)
	{
		this.tCanvas = tCanvas;
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x0000506B File Offset: 0x0000326B
	protected void paint(mGraphics g)
	{
		this.tCanvas.paint(g);
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x0000507B File Offset: 0x0000327B
	protected void keyPressed(int keyCode)
	{
		this.tCanvas.keyPressedz(keyCode);
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x0000508B File Offset: 0x0000328B
	protected void keyReleased(int keyCode)
	{
		this.tCanvas.keyReleasedz(keyCode);
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x0000509B File Offset: 0x0000329B
	protected void pointerDragged(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerDragged(x, y);
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x000050BE File Offset: 0x000032BE
	protected void pointerPressed(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerPressed(x, y);
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x000050E1 File Offset: 0x000032E1
	protected void pointerReleased(int x, int y)
	{
		x /= mGraphics.zoomLevel;
		y /= mGraphics.zoomLevel;
		this.tCanvas.pointerReleased(x, y);
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x00061C00 File Offset: 0x0005FE00
	public int getWidthz()
	{
		int width = this.getWidth();
		return width / mGraphics.zoomLevel + width % mGraphics.zoomLevel;
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x00061C28 File Offset: 0x0005FE28
	public int getHeightz()
	{
		int height = this.getHeight();
		return height / mGraphics.zoomLevel + height % mGraphics.zoomLevel;
	}

	// Token: 0x04000B66 RID: 2918
	public static MotherCanvas instance;

	// Token: 0x04000B67 RID: 2919
	public GameCanvas tCanvas;

	// Token: 0x04000B68 RID: 2920
	public int zoomLevel = 1;

	// Token: 0x04000B69 RID: 2921
	public Image imgCache;

	// Token: 0x04000B6A RID: 2922
	private int[] imgRGBCache;

	// Token: 0x04000B6B RID: 2923
	private int newWidth;

	// Token: 0x04000B6C RID: 2924
	private int newHeight;

	// Token: 0x04000B6D RID: 2925
	private int[] output;

	// Token: 0x04000B6E RID: 2926
	private int OUTPUTSIZE = 20;
}
