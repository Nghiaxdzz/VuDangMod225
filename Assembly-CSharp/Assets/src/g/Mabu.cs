using System;

namespace Assets.src.g
{
	// Token: 0x020000D6 RID: 214
	internal class Mabu : global::Char
	{
		// Token: 0x06000AD0 RID: 2768 RVA: 0x0000693F File Offset: 0x00004B3F
		public Mabu()
		{
			this.getData1();
			this.getData2();
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000A7A2C File Offset: 0x000A5C2C
		public void eat(int id)
		{
			this.effEat = new Effect(105, this.cx, this.cy + 20, 2, 1, -1);
			EffecMn.addEff(this.effEat);
			bool flag = id == global::Char.myCharz().charID;
			if (flag)
			{
				this.focus = global::Char.myCharz();
			}
			else
			{
				this.focus = GameScr.findCharInMap(id);
			}
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000A7A94 File Offset: 0x000A5C94
		public new void checkFrameTick(int[] array)
		{
			bool flag = this.skillID == 0;
			if (flag)
			{
				bool flag2 = this.tick == 11;
				if (flag2)
				{
					this.addFoot = true;
					Effect me = new Effect(19, this.cx, this.cy + 20, 2, 1, -1);
					EffecMn.addEff(me);
				}
				bool flag3 = this.tick >= array.Length - 1;
				if (flag3)
				{
					this.skillID = 2;
					return;
				}
			}
			bool flag4 = this.skillID == 1 && this.tick == array.Length - 1;
			if (flag4)
			{
				this.skillID = 3;
				this.cy -= 15;
			}
			else
			{
				this.tick++;
				bool flag5 = this.tick > array.Length - 1;
				if (flag5)
				{
					this.tick = 0;
				}
				this.frame = array[this.tick];
			}
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x000A7B78 File Offset: 0x000A5D78
		public void getData1()
		{
			Mabu.data1 = null;
			Mabu.data1 = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				102,
				"/data"
			});
			try
			{
				Mabu.data1.readData2(patch);
				Mabu.data1.img = GameCanvas.loadImage("/effectdata/" + 102 + "/img.png");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x000A7C1C File Offset: 0x000A5E1C
		public void setSkill(sbyte id, short x, short y, global::Char[] charHit, int[] damageHit)
		{
			this.skillID = id;
			this.xTo = (int)x;
			this.yTo = (int)y;
			this.lastDir = this.cdir;
			this.cdir = ((this.xTo > this.cx) ? 1 : -1);
			this.charAttack = charHit;
			this.damageAttack = damageHit;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000A7C74 File Offset: 0x000A5E74
		public void getData2()
		{
			Mabu.data2 = null;
			Mabu.data2 = new EffectData();
			string patch = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				103,
				"/data"
			});
			try
			{
				Mabu.data2.readData2(patch);
				Mabu.data2.img = GameCanvas.loadImage("/effectdata/" + 103 + "/img.png");
				Res.outz("read xong data");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000A7D24 File Offset: 0x000A5F24
		public override void update()
		{
			bool flag = this.focus != null;
			if (flag)
			{
				bool flag2 = this.effEat.t >= 30;
				if (flag2)
				{
					this.effEat.x += (this.cx - this.effEat.x) / 4;
					this.effEat.y += (this.cy - this.effEat.y) / 4;
					this.focus.cx = this.effEat.x;
					this.focus.cy = this.effEat.y;
					this.focus.isMabuHold = true;
				}
				else
				{
					this.effEat.trans = ((this.effEat.x > this.focus.cx) ? 1 : 0);
					this.effEat.x += (this.focus.cx - this.effEat.x) / 3;
					this.effEat.y += (this.focus.cy - this.effEat.y) / 3;
				}
			}
			bool flag3 = this.skillID != -1;
			if (flag3)
			{
				bool flag4 = this.skillID == 0 && this.addFoot && GameCanvas.gameTick % 2 == 0;
				if (flag4)
				{
					this.dx += ((this.xTo <= this.cx) ? -30 : 30);
					EffecMn.addEff(new Effect(103, this.cx + this.dx, this.cy + 20, 2, 1, -1)
					{
						trans = ((this.xTo <= this.cx) ? 1 : 0)
					});
					bool flag5 = (this.cdir == 1 && this.cx + this.dx >= this.xTo) || (this.cdir == -1 && this.cx + this.dx <= this.xTo);
					if (flag5)
					{
						this.addFoot = false;
						this.skillID = -1;
						this.dx = 0;
						this.tick = 0;
						this.cdir = this.lastDir;
						for (int i = 0; i < this.charAttack.Length; i++)
						{
							this.charAttack[i].doInjure(this.damageAttack[i], 0, false, false);
						}
					}
				}
				bool flag6 = this.skillID != 3;
				if (!flag6)
				{
					this.xTo = this.charAttack[this.pIndex].cx;
					this.yTo = this.charAttack[this.pIndex].cy;
					this.cx += (this.xTo - this.cx) / 3;
					this.cy += (this.yTo - this.cy) / 3;
					bool flag7 = GameCanvas.gameTick % 5 == 0;
					if (flag7)
					{
						Effect me = new Effect(19, this.cx, this.cy, 2, 1, -1);
						EffecMn.addEff(me);
					}
					bool flag8 = Res.abs(this.cx - this.xTo) <= 20 && Res.abs(this.cy - this.yTo) <= 20;
					if (flag8)
					{
						this.cx = this.xTo;
						this.cy = this.yTo;
						this.charAttack[this.pIndex].doInjure(this.damageAttack[this.pIndex], 0, false, false);
						this.pIndex++;
						bool flag9 = this.pIndex == this.charAttack.Length;
						if (flag9)
						{
							this.skillID = -1;
							this.pIndex = 0;
						}
					}
				}
			}
			else
			{
				base.update();
			}
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x000A8118 File Offset: 0x000A6318
		public override void paint(mGraphics g)
		{
			bool flag = this.skillID != -1;
			if (flag)
			{
				base.paintShadow(g);
				g.translate(0, GameCanvas.transY);
				this.checkFrameTick(Mabu.skills[(int)this.skillID]);
				bool flag2 = this.skillID == 0 || this.skillID == 1;
				if (flag2)
				{
					Mabu.data1.paintFrame(g, this.frame, this.cx, this.cy + this.fy, (this.cdir != 1) ? 1 : 0, 2);
				}
				else
				{
					Mabu.data2.paintFrame(g, this.frame, this.cx, this.cy + this.fy, (this.cdir != 1) ? 1 : 0, 2);
				}
				g.translate(0, -GameCanvas.transY);
			}
			else
			{
				base.paint(g);
			}
		}

		// Token: 0x040013AD RID: 5037
		public static EffectData data1;

		// Token: 0x040013AE RID: 5038
		public static EffectData data2;

		// Token: 0x040013AF RID: 5039
		private new int tick;

		// Token: 0x040013B0 RID: 5040
		private int lastDir;

		// Token: 0x040013B1 RID: 5041
		private bool addFoot;

		// Token: 0x040013B2 RID: 5042
		private Effect effEat;

		// Token: 0x040013B3 RID: 5043
		private new global::Char focus;

		// Token: 0x040013B4 RID: 5044
		public int xTo;

		// Token: 0x040013B5 RID: 5045
		public int yTo;

		// Token: 0x040013B6 RID: 5046
		public bool haftBody;

		// Token: 0x040013B7 RID: 5047
		public bool change;

		// Token: 0x040013B8 RID: 5048
		private global::Char[] charAttack;

		// Token: 0x040013B9 RID: 5049
		private int[] damageAttack;

		// Token: 0x040013BA RID: 5050
		private int dx;

		// Token: 0x040013BB RID: 5051
		public static int[] skill1 = new int[]
		{
			0,
			0,
			1,
			1,
			2,
			2,
			3,
			3,
			4,
			4,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5
		};

		// Token: 0x040013BC RID: 5052
		public static int[] skill2 = new int[]
		{
			0,
			0,
			6,
			6,
			7,
			7,
			8,
			8,
			9,
			9,
			9,
			9,
			9,
			10,
			10
		};

		// Token: 0x040013BD RID: 5053
		public static int[] skill3 = new int[]
		{
			0,
			0,
			1,
			1,
			2,
			2,
			3,
			3,
			4,
			4,
			5,
			5,
			6,
			6,
			7,
			7,
			8,
			8,
			9,
			9,
			10,
			10,
			11,
			11,
			12,
			12
		};

		// Token: 0x040013BE RID: 5054
		public static int[] skill4 = new int[]
		{
			13,
			13,
			14,
			14,
			15,
			15,
			16,
			16
		};

		// Token: 0x040013BF RID: 5055
		public static int[][] skills = new int[][]
		{
			Mabu.skill1,
			Mabu.skill2,
			Mabu.skill3,
			Mabu.skill4
		};

		// Token: 0x040013C0 RID: 5056
		public sbyte skillID = -1;

		// Token: 0x040013C1 RID: 5057
		private int frame;

		// Token: 0x040013C2 RID: 5058
		private int pIndex;
	}
}
