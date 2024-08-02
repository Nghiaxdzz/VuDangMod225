using System;

// Token: 0x02000091 RID: 145
public class Scroll
{
	// Token: 0x060007C4 RID: 1988 RVA: 0x00087EEC File Offset: 0x000860EC
	public void clear()
	{
		this.cmtoX = 0;
		this.cmtoY = 0;
		this.cmx = 0;
		this.cmy = 0;
		this.cmvx = 0;
		this.cmvy = 0;
		this.cmdx = 0;
		this.cmdy = 0;
		this.cmxLim = 0;
		this.cmyLim = 0;
		this.width = 0;
		this.height = 0;
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x00087F50 File Offset: 0x00086150
	public ScrollResult updateKey()
	{
		bool flag = this.styleUPDOWN;
		ScrollResult result;
		if (flag)
		{
			result = this.updateKeyScrollUpDown(false);
		}
		else
		{
			result = this.updateKeyScrollLeftRight();
		}
		return result;
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x00087F80 File Offset: 0x00086180
	public ScrollResult updateKey(bool isGetSelectNow)
	{
		bool flag = this.styleUPDOWN;
		ScrollResult result;
		if (flag)
		{
			result = this.updateKeyScrollUpDown(isGetSelectNow);
		}
		else
		{
			result = this.updateKeyScrollLeftRight();
		}
		return result;
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x00087FB0 File Offset: 0x000861B0
	private ScrollResult updateKeyScrollUpDown(bool isGetNow)
	{
		int num = this.xPos;
		int num2 = this.yPos;
		int w = this.width;
		int h = this.height;
		bool isPointerDown = GameCanvas.isPointerDown;
		if (isPointerDown)
		{
			bool flag = !this.pointerIsDowning && GameCanvas.isPointer(num, num2, w, h);
			if (flag)
			{
				for (int i = 0; i < this.pointerDownLastX.Length; i++)
				{
					this.pointerDownLastX[0] = GameCanvas.py;
				}
				this.pointerDownFirstX = GameCanvas.py;
				this.pointerIsDowning = true;
				bool flag2 = !isGetNow;
				if (flag2)
				{
					this.selectedItem = -1;
				}
				this.isDownWhenRunning = (this.cmRun != 0);
				this.cmRun = 0;
			}
			else
			{
				bool flag3 = this.pointerIsDowning;
				if (flag3)
				{
					this.pointerDownTime++;
					bool flag4 = this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.py && !this.isDownWhenRunning;
					if (flag4)
					{
						this.pointerDownFirstX = -1000;
						bool flag5 = this.ITEM_PER_LINE > 1;
						if (flag5)
						{
							int num3 = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
							int num4 = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
							this.selectedItem = num3 * this.ITEM_PER_LINE + num4;
						}
						else
						{
							this.selectedItem = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
						}
					}
					int num5 = GameCanvas.py - this.pointerDownLastX[0];
					bool flag6 = !isGetNow;
					if (flag6)
					{
						bool flag7 = num5 != 0 && this.selectedItem != -1;
						if (flag7)
						{
							this.selectedItem = -1;
						}
					}
					else
					{
						this.selectedItem = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
					}
					for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
					{
						this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
					}
					this.pointerDownLastX[0] = GameCanvas.py;
					this.cmtoY -= num5;
					bool flag8 = this.cmtoY < 0;
					if (flag8)
					{
						this.cmtoY = 0;
					}
					bool flag9 = this.cmtoY > this.cmyLim;
					if (flag9)
					{
						this.cmtoY = this.cmyLim;
					}
					bool flag10 = this.cmy < 0 || this.cmy > this.cmyLim;
					if (flag10)
					{
						num5 /= 2;
					}
					this.cmy -= num5;
				}
			}
		}
		bool isFinish = false;
		bool flag11 = GameCanvas.isPointerJustRelease && this.pointerIsDowning;
		if (flag11)
		{
			int i2 = GameCanvas.py - this.pointerDownLastX[0];
			GameCanvas.isPointerJustRelease = false;
			bool flag12 = Res.abs(i2) < 20 && Res.abs(GameCanvas.py - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning;
			if (flag12)
			{
				this.cmRun = 0;
				this.cmtoY = this.cmy;
				this.pointerDownFirstX = -1000;
				bool flag13 = this.ITEM_PER_LINE > 1;
				if (flag13)
				{
					int num6 = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
					int num7 = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
					this.selectedItem = num6 * this.ITEM_PER_LINE + num7;
				}
				else
				{
					this.selectedItem = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
				}
				this.pointerDownTime = 0;
				isFinish = true;
			}
			else
			{
				bool flag14 = this.selectedItem != -1 && this.pointerDownTime > 5;
				if (flag14)
				{
					this.pointerDownTime = 0;
					isFinish = true;
				}
				else
				{
					bool flag15 = (this.selectedItem == -1 && !this.isDownWhenRunning) || (isGetNow && this.selectedItem != -1 && !this.isDownWhenRunning);
					if (flag15)
					{
						bool flag16 = this.cmy < 0;
						if (flag16)
						{
							this.cmtoY = 0;
						}
						else
						{
							bool flag17 = this.cmy > this.cmyLim;
							if (flag17)
							{
								this.cmtoY = this.cmyLim;
							}
							else
							{
								int num8 = GameCanvas.py - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
								num8 = ((num8 > 10) ? 10 : ((num8 < -10) ? -10 : 0));
								this.cmRun = -num8 * 100;
							}
						}
					}
				}
			}
			this.pointerIsDowning = false;
			this.pointerDownTime = 0;
			GameCanvas.isPointerJustRelease = false;
		}
		return new ScrollResult
		{
			selected = this.selectedItem,
			isFinish = isFinish,
			isDowning = this.pointerIsDowning
		};
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x000884A0 File Offset: 0x000866A0
	private ScrollResult updateKeyScrollLeftRight()
	{
		int num = this.xPos;
		int y = this.yPos;
		int w = this.width;
		int h = this.height;
		bool isPointerDown = GameCanvas.isPointerDown;
		if (isPointerDown)
		{
			bool flag = !this.pointerIsDowning && GameCanvas.isPointer(num, y, w, h);
			if (flag)
			{
				for (int i = 0; i < this.pointerDownLastX.Length; i++)
				{
					this.pointerDownLastX[0] = GameCanvas.px;
				}
				this.pointerDownFirstX = GameCanvas.px;
				this.pointerIsDowning = true;
				this.selectedItem = -1;
				this.isDownWhenRunning = (this.cmRun != 0);
				this.cmRun = 0;
			}
			else
			{
				bool flag2 = this.pointerIsDowning;
				if (flag2)
				{
					this.pointerDownTime++;
					bool flag3 = this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.px && !this.isDownWhenRunning;
					if (flag3)
					{
						this.pointerDownFirstX = -1000;
						this.selectedItem = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
					}
					int num2 = GameCanvas.px - this.pointerDownLastX[0];
					bool flag4 = num2 != 0 && this.selectedItem != -1;
					if (flag4)
					{
						this.selectedItem = -1;
					}
					for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
					{
						this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
					}
					this.pointerDownLastX[0] = GameCanvas.px;
					this.cmtoX -= num2;
					bool flag5 = this.cmtoX < 0;
					if (flag5)
					{
						this.cmtoX = 0;
					}
					bool flag6 = this.cmtoX > this.cmxLim;
					if (flag6)
					{
						this.cmtoX = this.cmxLim;
					}
					bool flag7 = this.cmx < 0 || this.cmx > this.cmxLim;
					if (flag7)
					{
						num2 /= 2;
					}
					this.cmx -= num2;
				}
			}
		}
		bool isFinish = false;
		bool flag8 = GameCanvas.isPointerJustRelease && this.pointerIsDowning;
		if (flag8)
		{
			int i2 = GameCanvas.px - this.pointerDownLastX[0];
			GameCanvas.isPointerJustRelease = false;
			bool flag9 = Res.abs(i2) < 20 && Res.abs(GameCanvas.px - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning;
			if (flag9)
			{
				this.cmRun = 0;
				this.cmtoX = this.cmx;
				this.pointerDownFirstX = -1000;
				this.selectedItem = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
				this.pointerDownTime = 0;
				isFinish = true;
			}
			else
			{
				bool flag10 = this.selectedItem != -1 && this.pointerDownTime > 5;
				if (flag10)
				{
					this.pointerDownTime = 0;
					isFinish = true;
				}
				else
				{
					bool flag11 = this.selectedItem == -1 && !this.isDownWhenRunning;
					if (flag11)
					{
						bool flag12 = this.cmx < 0;
						if (flag12)
						{
							this.cmtoX = 0;
						}
						else
						{
							bool flag13 = this.cmx > this.cmxLim;
							if (flag13)
							{
								this.cmtoX = this.cmxLim;
							}
							else
							{
								int num3 = GameCanvas.px - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
								num3 = ((num3 > 10) ? 10 : ((num3 < -10) ? -10 : 0));
								this.cmRun = -num3 * 100;
							}
						}
					}
				}
			}
			this.pointerIsDowning = false;
			this.pointerDownTime = 0;
			GameCanvas.isPointerJustRelease = false;
		}
		return new ScrollResult
		{
			selected = this.selectedItem,
			isFinish = isFinish,
			isDowning = this.pointerIsDowning
		};
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x00088894 File Offset: 0x00086A94
	public void updatecm()
	{
		bool flag = this.cmRun != 0 && !this.pointerIsDowning;
		if (flag)
		{
			bool flag2 = this.styleUPDOWN;
			if (flag2)
			{
				this.cmtoY += this.cmRun / 100;
				bool flag3 = this.cmtoY < 0;
				if (flag3)
				{
					this.cmtoY = 0;
				}
				else
				{
					bool flag4 = this.cmtoY > this.cmyLim;
					if (flag4)
					{
						this.cmtoY = this.cmyLim;
					}
					else
					{
						this.cmy = this.cmtoY;
					}
				}
			}
			else
			{
				this.cmtoX += this.cmRun / 100;
				bool flag5 = this.cmtoX < 0;
				if (flag5)
				{
					this.cmtoX = 0;
				}
				else
				{
					bool flag6 = this.cmtoX > this.cmxLim;
					if (flag6)
					{
						this.cmtoX = this.cmxLim;
					}
					else
					{
						this.cmx = this.cmtoX;
					}
				}
			}
			this.cmRun = this.cmRun * 9 / 10;
			bool flag7 = this.cmRun < 100 && this.cmRun > -100;
			if (flag7)
			{
				this.cmRun = 0;
			}
		}
		bool flag8 = this.cmx != this.cmtoX && !this.pointerIsDowning;
		if (flag8)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 4;
			this.cmdx &= 15;
		}
		bool flag9 = this.cmy != this.cmtoY && !this.pointerIsDowning;
		if (flag9)
		{
			this.cmvy = this.cmtoY - this.cmy << 2;
			this.cmdy += this.cmvy;
			this.cmy += this.cmdy >> 4;
			this.cmdy &= 15;
		}
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x00088AA4 File Offset: 0x00086CA4
	public void setStyle(int nItem, int ITEM_SIZE, int xPos, int yPos, int width, int height, bool styleUPDOWN, int ITEM_PER_LINE)
	{
		this.xPos = xPos;
		this.yPos = yPos;
		this.ITEM_SIZE = ITEM_SIZE;
		this.nITEM = nItem;
		this.width = width;
		this.height = height;
		this.styleUPDOWN = styleUPDOWN;
		this.ITEM_PER_LINE = ITEM_PER_LINE;
		Res.outz(string.Concat(new object[]
		{
			"nItem= ",
			nItem,
			" ITEMSIZE= ",
			ITEM_SIZE,
			" heghit= ",
			height
		}));
		if (styleUPDOWN)
		{
			int num = nItem / ITEM_PER_LINE;
			bool flag = nItem % ITEM_PER_LINE != 0;
			if (flag)
			{
				num++;
			}
			this.cmyLim = num * ITEM_SIZE - height;
		}
		else
		{
			this.cmxLim = ITEM_PER_LINE * ITEM_SIZE - width;
		}
		bool flag2 = this.cmyLim < 0;
		if (flag2)
		{
			this.cmyLim = 0;
		}
		bool flag3 = this.cmxLim < 0;
		if (flag3)
		{
			this.cmxLim = 0;
		}
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x00088BA0 File Offset: 0x00086DA0
	public void moveTo(int to)
	{
		bool flag = this.styleUPDOWN;
		if (flag)
		{
			to -= (this.height - this.ITEM_SIZE) / 2;
			this.cmtoY = to;
			bool flag2 = this.cmtoY < 0;
			if (flag2)
			{
				this.cmtoY = 0;
			}
			bool flag3 = this.cmtoY > this.cmyLim;
			if (flag3)
			{
				this.cmtoY = this.cmyLim;
			}
		}
		else
		{
			to -= (this.width - this.ITEM_SIZE) / 2;
			this.cmtoX = to;
			bool flag4 = this.cmtoX < 0;
			if (flag4)
			{
				this.cmtoX = 0;
			}
			bool flag5 = this.cmtoX > this.cmxLim;
			if (flag5)
			{
				this.cmtoX = this.cmxLim;
			}
		}
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00088C60 File Offset: 0x00086E60
	public static Scroll gIz()
	{
		bool flag = Scroll.gI == null;
		if (flag)
		{
			Scroll.gI = new Scroll();
		}
		return Scroll.gI;
	}

	// Token: 0x04000FD1 RID: 4049
	public int cmtoX;

	// Token: 0x04000FD2 RID: 4050
	public int cmtoY;

	// Token: 0x04000FD3 RID: 4051
	public int cmx;

	// Token: 0x04000FD4 RID: 4052
	public int cmy;

	// Token: 0x04000FD5 RID: 4053
	public int cmvx;

	// Token: 0x04000FD6 RID: 4054
	public int cmvy;

	// Token: 0x04000FD7 RID: 4055
	public int cmdx;

	// Token: 0x04000FD8 RID: 4056
	public int cmdy;

	// Token: 0x04000FD9 RID: 4057
	public int xPos;

	// Token: 0x04000FDA RID: 4058
	public int yPos;

	// Token: 0x04000FDB RID: 4059
	public int width;

	// Token: 0x04000FDC RID: 4060
	public int height;

	// Token: 0x04000FDD RID: 4061
	public int cmxLim;

	// Token: 0x04000FDE RID: 4062
	public int cmyLim;

	// Token: 0x04000FDF RID: 4063
	public static Scroll gI;

	// Token: 0x04000FE0 RID: 4064
	private int pointerDownTime;

	// Token: 0x04000FE1 RID: 4065
	private int pointerDownFirstX;

	// Token: 0x04000FE2 RID: 4066
	private int[] pointerDownLastX = new int[3];

	// Token: 0x04000FE3 RID: 4067
	public bool pointerIsDowning;

	// Token: 0x04000FE4 RID: 4068
	public bool isDownWhenRunning;

	// Token: 0x04000FE5 RID: 4069
	private int cmRun;

	// Token: 0x04000FE6 RID: 4070
	public int selectedItem;

	// Token: 0x04000FE7 RID: 4071
	public int ITEM_SIZE;

	// Token: 0x04000FE8 RID: 4072
	public int nITEM;

	// Token: 0x04000FE9 RID: 4073
	public int ITEM_PER_LINE;

	// Token: 0x04000FEA RID: 4074
	public bool styleUPDOWN = true;
}
