using System;

// Token: 0x02000089 RID: 137
public class Point
{
	// Token: 0x06000756 RID: 1878 RVA: 0x000059A8 File Offset: 0x00003BA8
	public Point()
	{
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x000059B9 File Offset: 0x00003BB9
	public Point(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x000059D8 File Offset: 0x00003BD8
	public Point(int x, int y, int goc)
	{
		this.x = x;
		this.y = y;
		this.goc = goc;
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x000059FE File Offset: 0x00003BFE
	public void update()
	{
		this.f++;
		this.x += this.vx;
		this.y += this.vy;
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x00005A35 File Offset: 0x00003C35
	public void update_not_f()
	{
		this.x += this.vx;
		this.y += this.vy;
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x000845D4 File Offset: 0x000827D4
	public void paint(mGraphics g)
	{
		bool flag = !this.isRemove;
		if (flag)
		{
			int num = 0;
			bool flag2 = this.isSmall && this.f >= this.fSmall;
			if (flag2)
			{
				num = 1;
			}
			Point.FraEffInMap[this.color].drawFrame(this.frame / 2 + num, this.x, this.y, this.dis, 3, g);
		}
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00084648 File Offset: 0x00082848
	public void updateInMap()
	{
		this.f++;
		bool flag = this.maxframe > 1;
		if (flag)
		{
			this.frame++;
			bool flag2 = this.frame / 2 >= this.maxframe;
			if (flag2)
			{
				this.frame = 0;
			}
		}
		bool flag3 = this.f >= this.fRe;
		if (flag3)
		{
			this.isRemove = true;
		}
	}

	// Token: 0x04000F24 RID: 3876
	public byte type;

	// Token: 0x04000F25 RID: 3877
	public int x;

	// Token: 0x04000F26 RID: 3878
	public int y;

	// Token: 0x04000F27 RID: 3879
	public int g;

	// Token: 0x04000F28 RID: 3880
	public int v;

	// Token: 0x04000F29 RID: 3881
	public int vMax;

	// Token: 0x04000F2A RID: 3882
	public int w;

	// Token: 0x04000F2B RID: 3883
	public int h;

	// Token: 0x04000F2C RID: 3884
	public int color;

	// Token: 0x04000F2D RID: 3885
	public int limitY;

	// Token: 0x04000F2E RID: 3886
	public int vx;

	// Token: 0x04000F2F RID: 3887
	public int vy;

	// Token: 0x04000F30 RID: 3888
	public int x2;

	// Token: 0x04000F31 RID: 3889
	public int y2;

	// Token: 0x04000F32 RID: 3890
	public int toX;

	// Token: 0x04000F33 RID: 3891
	public int toY;

	// Token: 0x04000F34 RID: 3892
	public int dis;

	// Token: 0x04000F35 RID: 3893
	public int f;

	// Token: 0x04000F36 RID: 3894
	public int ftam;

	// Token: 0x04000F37 RID: 3895
	public int fRe;

	// Token: 0x04000F38 RID: 3896
	public int frame;

	// Token: 0x04000F39 RID: 3897
	public int maxframe;

	// Token: 0x04000F3A RID: 3898
	public int fSmall;

	// Token: 0x04000F3B RID: 3899
	public int goc;

	// Token: 0x04000F3C RID: 3900
	public int gocT_Arc;

	// Token: 0x04000F3D RID: 3901
	public int idir;

	// Token: 0x04000F3E RID: 3902
	public int dirThrow;

	// Token: 0x04000F3F RID: 3903
	public int dir;

	// Token: 0x04000F40 RID: 3904
	public int dir_nguoc;

	// Token: 0x04000F41 RID: 3905
	public int idSkill;

	// Token: 0x04000F42 RID: 3906
	public int id;

	// Token: 0x04000F43 RID: 3907
	public int levelPaint;

	// Token: 0x04000F44 RID: 3908
	public int num_per_frame = 1;

	// Token: 0x04000F45 RID: 3909
	public int life;

	// Token: 0x04000F46 RID: 3910
	public int goc_Arc;

	// Token: 0x04000F47 RID: 3911
	public int vx1000;

	// Token: 0x04000F48 RID: 3912
	public int vy1000;

	// Token: 0x04000F49 RID: 3913
	public int va;

	// Token: 0x04000F4A RID: 3914
	public int x1000;

	// Token: 0x04000F4B RID: 3915
	public int y1000;

	// Token: 0x04000F4C RID: 3916
	public int vX1000;

	// Token: 0x04000F4D RID: 3917
	public int vY1000;

	// Token: 0x04000F4E RID: 3918
	public long time;

	// Token: 0x04000F4F RID: 3919
	public long timecount;

	// Token: 0x04000F50 RID: 3920
	public MyVector vecEffPoint;

	// Token: 0x04000F51 RID: 3921
	public string name;

	// Token: 0x04000F52 RID: 3922
	public string info;

	// Token: 0x04000F53 RID: 3923
	public bool isRemove;

	// Token: 0x04000F54 RID: 3924
	public bool isSmall;

	// Token: 0x04000F55 RID: 3925
	public bool isPaint;

	// Token: 0x04000F56 RID: 3926
	public bool isChange;

	// Token: 0x04000F57 RID: 3927
	public static FrameImage[] FraEffInMap;

	// Token: 0x04000F58 RID: 3928
	public FrameImage fraImgEff;
}
