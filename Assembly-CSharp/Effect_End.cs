using System;

// Token: 0x0200002D RID: 45
public class Effect_End
{
	// Token: 0x06000224 RID: 548 RVA: 0x00037CD8 File Offset: 0x00035ED8
	public Effect_End(int type, int subtype, int x, int y, int levelPaint, int dir)
	{
		this.f = 0;
		this.typeEffect = type;
		this.typeSub = subtype;
		this.x = x;
		this.y = y;
		this.levelPaint = levelPaint;
		this.dir = dir;
		this.dir_nguoc = ((dir == 0) ? 2 : 0);
		this.time = mSystem.currentTimeMillis();
		this.isRemove = (this.isAddSub = false);
		this.create_Effect();
	}

	// Token: 0x06000225 RID: 549 RVA: 0x00037D78 File Offset: 0x00035F78
	public static Image getImage(int id)
	{
		bool flag = id < 0;
		Image result;
		if (flag)
		{
			result = null;
		}
		else
		{
			string path = "/e/e_" + id + ".png";
			Image image = null;
			try
			{
				image = mSystem.loadImage(path);
				result = image;
			}
			catch (Exception)
			{
				result = image;
			}
		}
		return result;
	}

	// Token: 0x06000226 RID: 550 RVA: 0x00037DD0 File Offset: 0x00035FD0
	public static void setSoundSkill_END(int x, int y, int typeEffect)
	{
		try
		{
			int num = -1;
			int num2 = Res.random(3);
			bool flag = num >= 0;
			if (flag)
			{
				SoundMn.playSound(x, y, num, SoundMn.volume);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000227 RID: 551 RVA: 0x00037E1C File Offset: 0x0003601C
	public void create_Effect()
	{
		Effect_End.setSoundSkill_END(this.x, this.y, this.typeEffect);
		int num = this.typeEffect;
		if (num > 2)
		{
			if (num == 3)
			{
				this.set_FireWork();
			}
		}
		else
		{
			this.set_End_String(this.typeEffect);
		}
	}

	// Token: 0x06000228 RID: 552 RVA: 0x00037E70 File Offset: 0x00036070
	public void update()
	{
		this.f++;
		int num = this.typeEffect;
		if (num > 2)
		{
			if (num == 3)
			{
				this.upd_FireWork();
			}
		}
		else
		{
			this.upd_End_String();
		}
	}

	// Token: 0x06000229 RID: 553 RVA: 0x00037EB4 File Offset: 0x000360B4
	public void paint(mGraphics g)
	{
		try
		{
			bool flag = !this.isRemove && this.f >= 0;
			if (flag)
			{
				int num = this.typeEffect;
				if (num > 2)
				{
					if (num == 3)
					{
						this.pnt_FireWork(g);
					}
				}
				else
				{
					this.pnt_End_String(g);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600022A RID: 554 RVA: 0x0000412E File Offset: 0x0000232E
	public void removeEff()
	{
		this.isRemove = true;
	}

	// Token: 0x0600022B RID: 555 RVA: 0x00037F20 File Offset: 0x00036120
	private void set_End_String(int typeEffect)
	{
		switch (typeEffect)
		{
		case 0:
			this.fraImgEff = new FrameImage(4);
			break;
		case 1:
			this.fraImgEff = new FrameImage(5);
			break;
		case 2:
			this.fraImgEff = new FrameImage(6);
			break;
		}
		this.fRemove = 100;
		this.dy_throw = GameCanvas.h / 3 + 10;
		this.vy = 10;
		this.y1000 = 0;
		this.isAddSub = false;
	}

	// Token: 0x0600022C RID: 556 RVA: 0x00037F9C File Offset: 0x0003619C
	private void upd_End_String()
	{
		this.x = GameCanvas.hw;
		this.y = this.y1000;
		bool flag = this.f > this.fRemove;
		if (flag)
		{
			this.removeEff();
		}
		this.vy++;
		bool flag2 = this.vy > 15;
		if (flag2)
		{
			this.vy = 15;
		}
		bool flag3 = this.y1000 + this.vy < this.dy_throw;
		if (flag3)
		{
			this.y1000 += this.vy;
		}
		else
		{
			this.y1000 = this.dy_throw;
			bool flag4 = !this.isAddSub;
			if (flag4)
			{
				this.isAddSub = true;
				bool flag5 = this.typeSub != -1;
				if (flag5)
				{
					GameScr.addEffectEnd(this.typeSub, 0, this.x, this.y, this.levelPaint, 0);
				}
			}
		}
	}

	// Token: 0x0600022D RID: 557 RVA: 0x00038088 File Offset: 0x00036288
	private void pnt_End_String(mGraphics g)
	{
		bool flag = this.fraImgEff != null;
		if (flag)
		{
			this.fraImgEff.drawFrame(this.f / 5 % this.fraImgEff.nFrame, this.x, this.y, 0, 33, g);
		}
	}

	// Token: 0x0600022E RID: 558 RVA: 0x000380D8 File Offset: 0x000362D8
	private void set_FireWork()
	{
		int num = Res.random(3, 5);
		this.fRemove = 90;
		for (int i = 0; i < num; i++)
		{
			Point point = new Point();
			point.x = this.x + Res.random_Am_0(4);
			point.y = this.y + Res.random_Am_0(5);
			bool flag = this.typeSub == 0;
			if (flag)
			{
				point.fRe = Res.random(10);
				int num2 = 1;
				bool flag2 = i % 2 == 0;
				if (flag2)
				{
					num2 = -1;
				}
				point.x = this.x + Res.random((int)(Effect_End.arrInfoEff[5][0] / 2)) * num2;
				point.y = this.y - Res.random((int)(Effect_End.arrInfoEff[5][1] / 2));
				point.fraImgEff = new FrameImage(7);
			}
			this.VecEffEnd.addElement(point);
		}
	}

	// Token: 0x0600022F RID: 559 RVA: 0x000381C4 File Offset: 0x000363C4
	private void upd_FireWork()
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Point point = (Point)this.VecEffEnd.elementAt(i);
			point.update();
			bool flag = point.f == point.fRe;
			if (flag)
			{
				SoundMn.playSound(point.x, point.y, SoundMn.FIREWORK, SoundMn.volume);
			}
			bool flag2 = point.f - point.fRe <= point.fraImgEff.nFrame * 3 - 1;
			if (!flag2)
			{
				point.f = 0;
				bool flag3 = this.typeSub == 0;
				if (flag3)
				{
					point.fRe = Res.random(10);
					int num = 1;
					bool flag4 = i % 2 == 0;
					if (flag4)
					{
						num = -1;
					}
					point.x = this.x + Res.random((int)(Effect_End.arrInfoEff[5][0] / 2)) * num;
					point.y = this.y - Res.random((int)(Effect_End.arrInfoEff[5][1] / 2));
				}
			}
		}
		bool flag5 = this.f >= this.fRemove;
		if (flag5)
		{
			this.removeEff();
		}
	}

	// Token: 0x06000230 RID: 560 RVA: 0x00038300 File Offset: 0x00036500
	private void pnt_FireWork(mGraphics g)
	{
		for (int i = 0; i < this.VecEffEnd.size(); i++)
		{
			Point point = (Point)this.VecEffEnd.elementAt(i);
			bool flag = point.f - point.fRe > -1 && point.fraImgEff != null;
			if (flag)
			{
				point.fraImgEff.drawFrame((point.f - point.fRe) / 3 % point.fraImgEff.nFrame, point.x, point.y, 0, 3, g);
			}
		}
	}

	// Token: 0x04000504 RID: 1284
	public const short End_String_Lose = 0;

	// Token: 0x04000505 RID: 1285
	public const short End_String_Win = 1;

	// Token: 0x04000506 RID: 1286
	public const short End_String_Draw = 2;

	// Token: 0x04000507 RID: 1287
	public const short End_FireWork = 3;

	// Token: 0x04000508 RID: 1288
	public const sbyte Lvlpaint_All = -1;

	// Token: 0x04000509 RID: 1289
	public const sbyte Lvlpaint_Front = 0;

	// Token: 0x0400050A RID: 1290
	public const sbyte Lvlpaint_Mid = 1;

	// Token: 0x0400050B RID: 1291
	public const sbyte Lvlpaint_Behind = 2;

	// Token: 0x0400050C RID: 1292
	private MyVector VecEffEnd = new MyVector("EffectEnd VecEffEnd");

	// Token: 0x0400050D RID: 1293
	public byte[] nFrame = new byte[10];

	// Token: 0x0400050E RID: 1294
	public int id = -1;

	// Token: 0x0400050F RID: 1295
	public int typeEffect;

	// Token: 0x04000510 RID: 1296
	public int typeSub;

	// Token: 0x04000511 RID: 1297
	public FrameImage fraImgEff;

	// Token: 0x04000512 RID: 1298
	public FrameImage fraImgSubEff;

	// Token: 0x04000513 RID: 1299
	public int fRemove;

	// Token: 0x04000514 RID: 1300
	public int fMove;

	// Token: 0x04000515 RID: 1301
	public int x;

	// Token: 0x04000516 RID: 1302
	public int y;

	// Token: 0x04000517 RID: 1303
	public int dir;

	// Token: 0x04000518 RID: 1304
	public int dir_nguoc;

	// Token: 0x04000519 RID: 1305
	public int levelPaint;

	// Token: 0x0400051A RID: 1306
	public int f;

	// Token: 0x0400051B RID: 1307
	public int vx;

	// Token: 0x0400051C RID: 1308
	public int vy;

	// Token: 0x0400051D RID: 1309
	public int x1000;

	// Token: 0x0400051E RID: 1310
	public int y1000;

	// Token: 0x0400051F RID: 1311
	public int vx1000;

	// Token: 0x04000520 RID: 1312
	public int vy1000;

	// Token: 0x04000521 RID: 1313
	public int dy_throw;

	// Token: 0x04000522 RID: 1314
	public long time;

	// Token: 0x04000523 RID: 1315
	public bool isRemove;

	// Token: 0x04000524 RID: 1316
	public bool isAddSub;

	// Token: 0x04000525 RID: 1317
	public static short[][] arrInfoEff = new short[][]
	{
		new short[]
		{
			68,
			264,
			4
		},
		new short[]
		{
			30,
			120,
			4
		},
		new short[]
		{
			66,
			280,
			4
		},
		new short[]
		{
			0,
			0,
			1
		},
		new short[]
		{
			111,
			68,
			2
		},
		new short[]
		{
			90,
			68,
			2
		},
		new short[]
		{
			125,
			68,
			2
		},
		new short[]
		{
			47,
			282,
			6
		},
		new short[2]
	};
}
