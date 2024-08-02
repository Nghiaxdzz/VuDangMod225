using System;

// Token: 0x02000060 RID: 96
public class MainImage
{
	// Token: 0x06000490 RID: 1168 RVA: 0x00004D2E File Offset: 0x00002F2E
	public MainImage()
	{
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x00004D52 File Offset: 0x00002F52
	public MainImage(Image im, sbyte nFrame)
	{
		this.img = im;
		this.count = 0L;
		this.nFrame = nFrame;
	}

	// Token: 0x040009FB RID: 2555
	public Image img;

	// Token: 0x040009FC RID: 2556
	public long count = -1L;

	// Token: 0x040009FD RID: 2557
	public int timeImageNull;

	// Token: 0x040009FE RID: 2558
	public int idImage;

	// Token: 0x040009FF RID: 2559
	public long timerequest;

	// Token: 0x04000A00 RID: 2560
	public sbyte nFrame = 1;

	// Token: 0x04000A01 RID: 2561
	public long timeUse = mSystem.currentTimeMillis();
}
