using System;

// Token: 0x02000087 RID: 135
public class PlayerDart
{
	// Token: 0x0600074B RID: 1867 RVA: 0x00083D4C File Offset: 0x00081F4C
	public PlayerDart(global::Char charBelong, int dartType, SkillPaint sp, int x, int y)
	{
		this.skillPaint = sp;
		this.charBelong = charBelong;
		this.info = GameScr.darts[dartType];
		this.va = this.info.va;
		this.x = x;
		this.y = y;
		bool flag = charBelong.mobFocus == null;
		object obj;
		if (flag)
		{
			IMapObject charFocus = charBelong.charFocus;
			obj = charFocus;
		}
		else
		{
			obj = charBelong.mobFocus;
		}
		IMapObject mapObject = (IMapObject)obj;
		this.setAngle(Res.angle(mapObject.getX() - x, mapObject.getY() - y));
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x00005943 File Offset: 0x00003B43
	public void setAngle(int angle)
	{
		this.angle = angle;
		this.vx = this.va * Res.cos(angle) >> 10;
		this.vy = this.va * Res.sin(angle) >> 10;
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x00083DF8 File Offset: 0x00081FF8
	public void update()
	{
		bool flag = !this.isActive;
		if (!flag)
		{
			bool flag2 = this.charBelong.mobFocus == null && this.charBelong.charFocus == null;
			if (flag2)
			{
				this.endMe();
			}
			else
			{
				bool flag3 = this.charBelong.mobFocus == null;
				object obj;
				if (flag3)
				{
					IMapObject charFocus = this.charBelong.charFocus;
					obj = charFocus;
				}
				else
				{
					obj = this.charBelong.mobFocus;
				}
				IMapObject mapObject = (IMapObject)obj;
				for (int i = 0; i < (int)this.info.nUpdate; i++)
				{
					bool flag4 = this.info.tail.Length != 0;
					if (flag4)
					{
						this.darts.addElement(new SmallDart(this.x, this.y));
					}
					int num = (this.charBelong.getX() <= mapObject.getX()) ? -10 : 10;
					this.dx = mapObject.getX() + num - this.x;
					this.dy = mapObject.getY() - mapObject.getH() / 2 - this.y;
					this.life++;
					bool flag5 = Res.abs(this.dx) < 20 && Res.abs(this.dy) < 20;
					if (flag5)
					{
						bool flag6 = this.charBelong.charFocus != null && this.charBelong.charFocus.me;
						if (flag6)
						{
							this.charBelong.charFocus.doInjure(this.charBelong.charFocus.damHP, 0, this.charBelong.charFocus.isCrit, this.charBelong.charFocus.isMob);
						}
						this.endMe();
						return;
					}
					int num2 = Res.angle(this.dx, this.dy);
					bool flag7 = global::Math.abs(num2 - this.angle) < 90 || this.dx * this.dx + this.dy * this.dy > 4096;
					if (flag7)
					{
						bool flag8 = global::Math.abs(num2 - this.angle) < 15;
						if (flag8)
						{
							this.angle = num2;
						}
						else
						{
							bool flag9 = (num2 - this.angle >= 0 && num2 - this.angle < 180) || num2 - this.angle < -180;
							if (flag9)
							{
								this.angle = Res.fixangle(this.angle + 15);
							}
							else
							{
								this.angle = Res.fixangle(this.angle - 15);
							}
						}
					}
					bool flag10 = !this.isSpeedUp && this.va < 8192;
					if (flag10)
					{
						this.va += 1024;
					}
					this.vx = this.va * Res.cos(this.angle) >> 10;
					this.vy = this.va * Res.sin(this.angle) >> 10;
					this.dx += this.vx;
					int num3 = this.dx >> 10;
					this.x += num3;
					this.dx &= 1023;
					this.dy += this.vy;
					int num4 = this.dy >> 10;
					this.y += num4;
					this.dy &= 1023;
				}
				for (int j = 0; j < this.darts.size(); j++)
				{
					SmallDart smallDart = (SmallDart)this.darts.elementAt(j);
					smallDart.index++;
					bool flag11 = smallDart.index >= this.info.tail.Length;
					if (flag11)
					{
						this.darts.removeElementAt(j);
					}
				}
			}
		}
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x00084210 File Offset: 0x00082410
	private void endMe()
	{
		bool flag = !this.charBelong.isUseSkillAfterCharge && this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w;
		if (flag)
		{
			SoundMn.gI().explode_1();
		}
		this.charBelong.setAttack();
		bool me = this.charBelong.me;
		if (me)
		{
			this.charBelong.saveLoadPreviousSkill();
		}
		bool isUseSkillAfterCharge = this.charBelong.isUseSkillAfterCharge;
		if (isUseSkillAfterCharge)
		{
			this.charBelong.isUseSkillAfterCharge = false;
			bool flag2 = this.charBelong.isLockMove && this.charBelong.me && this.charBelong.statusMe != 14 && this.charBelong.statusMe != 5;
			if (flag2)
			{
				this.charBelong.isLockMove = false;
			}
			GameScr.gI().activeSuperPower(this.x, this.y);
		}
		this.charBelong.dart = null;
		this.charBelong.isCreateDark = false;
		this.charBelong.skillPaint = null;
		this.charBelong.skillPaintRandomPaint = null;
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x00084340 File Offset: 0x00082540
	public void paint(mGraphics g)
	{
		bool flag = !this.isActive;
		if (!flag)
		{
			int num = MonsterDart.findDirIndexFromAngle(360 - this.angle);
			int num2 = (int)MonsterDart.FRAME[num];
			int transform = MonsterDart.TRANSFORM[num];
			for (int i = this.darts.size() / 2; i < this.darts.size(); i++)
			{
				SmallDart smallDart = (SmallDart)this.darts.elementAt(i);
				SmallImage.drawSmallImage(g, (int)this.info.tailBorder[smallDart.index], smallDart.x, smallDart.y, 0, 3);
			}
			int num3 = GameCanvas.gameTick % this.info.headBorder.Length;
			SmallImage.drawSmallImage(g, (int)this.info.headBorder[num3][num2], this.x, this.y, transform, 3);
			for (int j = 0; j < this.darts.size(); j++)
			{
				SmallDart smallDart2 = (SmallDart)this.darts.elementAt(j);
				SmallImage.drawSmallImage(g, (int)this.info.tail[smallDart2.index], smallDart2.x, smallDart2.y, 0, 3);
			}
			SmallImage.drawSmallImage(g, (int)this.info.head[num3][num2], this.x, this.y, transform, 3);
			for (int k = 0; k < this.darts.size(); k++)
			{
				SmallDart smallDart3 = (SmallDart)this.darts.elementAt(k);
				bool flag2 = Res.abs(MonsterDart.r.nextInt(100)) < (int)this.info.xdPercent;
				if (flag2)
				{
					SmallImage.drawSmallImage(g, (int)((GameCanvas.gameTick % 2 != 0) ? this.info.xd2[smallDart3.index] : this.info.xd1[smallDart3.index]), smallDart3.x, smallDart3.y, 0, 3);
				}
			}
			g.setColor(16711680);
		}
	}

	// Token: 0x04000F06 RID: 3846
	public global::Char charBelong;

	// Token: 0x04000F07 RID: 3847
	public DartInfo info;

	// Token: 0x04000F08 RID: 3848
	public MyVector darts = new MyVector();

	// Token: 0x04000F09 RID: 3849
	public int angle;

	// Token: 0x04000F0A RID: 3850
	public int vx;

	// Token: 0x04000F0B RID: 3851
	public int vy;

	// Token: 0x04000F0C RID: 3852
	public int va;

	// Token: 0x04000F0D RID: 3853
	public int x;

	// Token: 0x04000F0E RID: 3854
	public int y;

	// Token: 0x04000F0F RID: 3855
	public int z;

	// Token: 0x04000F10 RID: 3856
	private int life;

	// Token: 0x04000F11 RID: 3857
	private int dx;

	// Token: 0x04000F12 RID: 3858
	private int dy;

	// Token: 0x04000F13 RID: 3859
	public bool isActive = true;

	// Token: 0x04000F14 RID: 3860
	public bool isSpeedUp;

	// Token: 0x04000F15 RID: 3861
	public SkillPaint skillPaint;
}
