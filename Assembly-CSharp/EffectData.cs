using System;

// Token: 0x02000026 RID: 38
public class EffectData
{
	// Token: 0x06000201 RID: 513 RVA: 0x00036944 File Offset: 0x00034B44
	public ImageInfo getImageInfo(sbyte id)
	{
		for (int i = 0; i < this.imgInfo.Length; i++)
		{
			bool flag = this.imgInfo[i].ID == (int)id;
			if (flag)
			{
				return this.imgInfo[i];
			}
		}
		return null;
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00036990 File Offset: 0x00034B90
	public void readData(string patch)
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = MyStream.readFile(patch);
		}
		catch (Exception)
		{
			return;
		}
		this.readData(dataInputStream.r);
	}

	// Token: 0x06000203 RID: 515 RVA: 0x000369CC File Offset: 0x00034BCC
	public void readData2(string patch)
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = MyStream.readFile(patch);
		}
		catch (Exception)
		{
			return;
		}
		this.readEffect(dataInputStream.r);
	}

	// Token: 0x06000204 RID: 516 RVA: 0x00036A08 File Offset: 0x00034C08
	public void readEffect(myReader msg)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		try
		{
			sbyte b = msg.readByte();
			Res.outz("size IMG==========" + b);
			this.imgInfo = new ImageInfo[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				this.imgInfo[i] = new ImageInfo();
				this.imgInfo[i].ID = (int)msg.readByte();
				this.imgInfo[i].x0 = (int)msg.readUnsignedByte();
				this.imgInfo[i].y0 = (int)msg.readUnsignedByte();
				this.imgInfo[i].w = (int)msg.readUnsignedByte();
				this.imgInfo[i].h = (int)msg.readUnsignedByte();
			}
			short num5 = msg.readShort();
			this.frame = new Frame[(int)num5];
			for (int j = 0; j < this.frame.Length; j++)
			{
				this.frame[j] = new Frame();
				sbyte b2 = msg.readByte();
				this.frame[j].dx = new short[(int)b2];
				this.frame[j].dy = new short[(int)b2];
				this.frame[j].idImg = new sbyte[(int)b2];
				for (int k = 0; k < (int)b2; k++)
				{
					this.frame[j].dx[k] = msg.readShort();
					this.frame[j].dy[k] = msg.readShort();
					this.frame[j].idImg[k] = msg.readByte();
					bool flag = j == 0;
					if (flag)
					{
						bool flag2 = num > (int)this.frame[j].dx[k];
						if (flag2)
						{
							num = (int)this.frame[j].dx[k];
						}
						bool flag3 = num2 > (int)this.frame[j].dy[k];
						if (flag3)
						{
							num2 = (int)this.frame[j].dy[k];
						}
						bool flag4 = num3 < (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						if (flag4)
						{
							num3 = (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						}
						bool flag5 = num4 < (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
						if (flag5)
						{
							num4 = (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
						}
						this.width = num3 - num;
						this.height = num4 - num2;
					}
				}
			}
			this.arrFrame = new short[(int)msg.readShort()];
			for (int l = 0; l < this.arrFrame.Length; l++)
			{
				this.arrFrame[l] = msg.readShort();
			}
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
			Res.outz("1");
		}
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00036D9C File Offset: 0x00034F9C
	public void readData(myReader iss)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		try
		{
			sbyte b = iss.readByte();
			this.imgInfo = new ImageInfo[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				this.imgInfo[i] = new ImageInfo();
				this.imgInfo[i].ID = (int)iss.readByte();
				this.imgInfo[i].x0 = (int)iss.readByte();
				this.imgInfo[i].y0 = (int)iss.readByte();
				this.imgInfo[i].w = (int)iss.readByte();
				this.imgInfo[i].h = (int)iss.readByte();
			}
			short num5 = iss.readShort();
			this.frame = new Frame[(int)num5];
			for (int j = 0; j < (int)num5; j++)
			{
				this.frame[j] = new Frame();
				sbyte b2 = iss.readByte();
				this.frame[j].dx = new short[(int)b2];
				this.frame[j].dy = new short[(int)b2];
				this.frame[j].idImg = new sbyte[(int)b2];
				for (int k = 0; k < (int)b2; k++)
				{
					this.frame[j].dx[k] = iss.readShort();
					this.frame[j].dy[k] = iss.readShort();
					this.frame[j].idImg[k] = iss.readByte();
					bool flag = j == 0;
					if (flag)
					{
						bool flag2 = num > (int)this.frame[j].dx[k];
						if (flag2)
						{
							num = (int)this.frame[j].dx[k];
						}
						bool flag3 = num2 > (int)this.frame[j].dy[k];
						if (flag3)
						{
							num2 = (int)this.frame[j].dy[k];
						}
						bool flag4 = num3 < (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						if (flag4)
						{
							num3 = (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						}
						bool flag5 = num4 < (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
						if (flag5)
						{
							num4 = (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
						}
						this.width = num3 - num;
						this.height = num4 - num2;
					}
				}
			}
			short num6 = iss.readShort();
			this.arrFrame = new short[(int)num6];
			for (int l = 0; l < (int)num6; l++)
			{
				this.arrFrame[l] = iss.readShort();
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("LOI TAI readData cua EffectDAta" + ex.ToString());
		}
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00037110 File Offset: 0x00035310
	public void readData(sbyte[] data)
	{
		myReader iss = new myReader(data);
		this.readData(iss);
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00037130 File Offset: 0x00035330
	public void readDataNewBoss(sbyte[] data, sbyte typeread)
	{
		myReader msg = new myReader(data);
		this.readMobNew(msg, typeread);
	}

	// Token: 0x06000208 RID: 520 RVA: 0x00037150 File Offset: 0x00035350
	public void paintFrame(mGraphics g, int f, int x, int y, int trans, int layer)
	{
		bool flag = this.frame == null || this.frame.Length == 0;
		if (!flag)
		{
			Frame frame = this.frame[f];
			for (int i = 0; i < frame.dx.Length; i++)
			{
				ImageInfo imageInfo = this.getImageInfo(frame.idImg[i]);
				try
				{
					bool flag2 = trans == -1;
					if (flag2)
					{
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 0, x + (int)frame.dx[i], y + (int)frame.dy[i], 0);
					}
					bool flag3 = trans == 0;
					if (flag3)
					{
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 0, x + (int)frame.dx[i], y + (int)frame.dy[i] - ((layer < 4 && layer > 0) ? GameCanvas.transY : 0), 0);
					}
					bool flag4 = trans == 1;
					if (flag4)
					{
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 2, x - (int)frame.dx[i], y + (int)frame.dy[i] - ((layer < 4 && layer > 0) ? GameCanvas.transY : 0), StaticObj.TOP_RIGHT);
					}
					bool flag5 = trans == 2;
					if (flag5)
					{
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 7, x - (int)frame.dx[i], y + (int)frame.dy[i] - ((layer < 4 && layer > 0) ? GameCanvas.transY : 0), StaticObj.VCENTER_HCENTER);
					}
				}
				catch (Exception)
				{
				}
			}
		}
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00037340 File Offset: 0x00035540
	public void readMobNew(myReader msg, sbyte typeread)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		try
		{
			sbyte b = msg.readByte();
			this.imgInfo = new ImageInfo[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				this.imgInfo[i] = new ImageInfo();
				this.imgInfo[i].ID = (int)msg.readByte();
				bool flag = typeread == 1;
				if (flag)
				{
					this.imgInfo[i].x0 = (int)msg.readUnsignedByte();
					this.imgInfo[i].y0 = (int)msg.readUnsignedByte();
				}
				else
				{
					this.imgInfo[i].x0 = (int)msg.readShort();
					this.imgInfo[i].y0 = (int)msg.readShort();
				}
				this.imgInfo[i].w = (int)msg.readUnsignedByte();
				this.imgInfo[i].h = (int)msg.readUnsignedByte();
			}
			short num5 = msg.readShort();
			this.frame = new Frame[(int)num5];
			for (int j = 0; j < this.frame.Length; j++)
			{
				this.frame[j] = new Frame();
				sbyte b2 = msg.readByte();
				this.frame[j].dx = new short[(int)b2];
				this.frame[j].dy = new short[(int)b2];
				this.frame[j].idImg = new sbyte[(int)b2];
				for (int k = 0; k < (int)b2; k++)
				{
					this.frame[j].dx[k] = msg.readShort();
					this.frame[j].dy[k] = msg.readShort();
					this.frame[j].idImg[k] = msg.readByte();
					bool flag2 = j == 0;
					if (flag2)
					{
						bool flag3 = num > (int)this.frame[j].dx[k];
						if (flag3)
						{
							num = (int)this.frame[j].dx[k];
						}
						bool flag4 = num2 > (int)this.frame[j].dy[k];
						if (flag4)
						{
							num2 = (int)this.frame[j].dy[k];
						}
						bool flag5 = num3 < (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						if (flag5)
						{
							num3 = (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
						}
						bool flag6 = num4 < (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
						if (flag6)
						{
							num4 = (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
						}
						this.width = num3 - num;
						this.height = num4 - num2;
					}
				}
			}
			this.arrFrame = new short[(int)msg.readShort()];
			for (int l = 0; l < this.arrFrame.Length; l++)
			{
				this.arrFrame[l] = msg.readShort();
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x040004DB RID: 1243
	public Image img;

	// Token: 0x040004DC RID: 1244
	public ImageInfo[] imgInfo;

	// Token: 0x040004DD RID: 1245
	public Frame[] frame;

	// Token: 0x040004DE RID: 1246
	public short[] arrFrame;

	// Token: 0x040004DF RID: 1247
	public int ID;

	// Token: 0x040004E0 RID: 1248
	public int typeData;

	// Token: 0x040004E1 RID: 1249
	public int width;

	// Token: 0x040004E2 RID: 1250
	public int height;
}
