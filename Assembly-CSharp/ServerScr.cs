using System;

// Token: 0x02000096 RID: 150
public class ServerScr : mScreen, IActionListener
{
	// Token: 0x060007F3 RID: 2035 RVA: 0x0008A960 File Offset: 0x00088B60
	public ServerScr()
	{
		TileMap.bgID = (int)((byte)(mSystem.currentTimeMillis() % 9L));
		bool flag = TileMap.bgID == 5 || TileMap.bgID == 6;
		if (flag)
		{
			TileMap.bgID = 4;
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x0008A9C0 File Offset: 0x00088BC0
	public override void switchToMe()
	{
		SoundMn.gI().stopAll();
		base.switchToMe();
		this.vecServer = new Command[ServerListScreen.nameServer.Length];
		for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
		{
			this.vecServer[i] = new Command(ServerListScreen.nameServer[i], this, 100 + i, null);
		}
		this.mainSelect = ServerListScreen.ipSelect;
		this.w2c = 5;
		this.wc = 76;
		this.hc = mScreen.cmdH;
		this.numw = 2;
		bool flag = GameCanvas.w > 3 * (this.wc + this.w2c);
		if (flag)
		{
			this.numw = 3;
		}
		this.numh = this.vecServer.Length / this.numw + ((this.vecServer.Length % this.numw != 0) ? 1 : 0);
		for (int j = 0; j < this.vecServer.Length; j++)
		{
			bool flag2 = this.vecServer[j] != null;
			if (flag2)
			{
				int num = GameCanvas.hw - this.numw * (this.wc + this.w2c) / 2;
				int x = num + j % this.numw * (this.wc + this.w2c);
				int num2 = GameCanvas.hh - this.numh * (this.hc + this.w2c) / 2;
				int y = num2 + j / this.numw * (this.hc + this.w2c);
				this.vecServer[j].x = x;
				this.vecServer[j].y = y;
			}
		}
		bool flag3 = !GameCanvas.isTouch;
		if (flag3)
		{
			this.cmdCheck = new Command(mResources.SELECT, this, 99, null);
			this.center = this.cmdCheck;
		}
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x0008AB98 File Offset: 0x00088D98
	public override void update()
	{
		GameScr.cmx++;
		bool flag = GameScr.cmx > GameCanvas.w * 3 + 100;
		if (flag)
		{
			GameScr.cmx = 100;
		}
		for (int i = 0; i < this.vecServer.Length; i++)
		{
			bool flag2 = !GameCanvas.isTouch;
			if (flag2)
			{
				bool flag3 = i == this.mainSelect;
				if (flag3)
				{
					bool flag4 = GameCanvas.gameTick % 10 < 4;
					if (flag4)
					{
						this.vecServer[i].isFocus = true;
					}
					else
					{
						this.vecServer[i].isFocus = false;
					}
				}
				else
				{
					this.vecServer[i].isFocus = false;
				}
			}
			else
			{
				bool flag5 = this.vecServer[i] != null && this.vecServer[i].isPointerPressInside();
				if (flag5)
				{
					this.vecServer[i].performAction();
				}
			}
		}
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x0008AC88 File Offset: 0x00088E88
	public override void paint(mGraphics g)
	{
		GameCanvas.paintBGGameScr(g);
		for (int i = 0; i < this.vecServer.Length; i++)
		{
			bool flag = this.vecServer[i] != null;
			if (flag)
			{
				this.vecServer[i].paint(g);
			}
		}
		base.paint(g);
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x0008ACE0 File Offset: 0x00088EE0
	public override void updateKey()
	{
		base.updateKey();
		int num = this.mainSelect % this.numw;
		int num2 = this.mainSelect / this.numw;
		bool flag = GameCanvas.keyPressed[4];
		if (flag)
		{
			bool flag2 = num > 0;
			if (flag2)
			{
				this.mainSelect--;
			}
			GameCanvas.keyPressed[4] = false;
		}
		else
		{
			bool flag3 = GameCanvas.keyPressed[6];
			if (flag3)
			{
				bool flag4 = num < this.numw - 1;
				if (flag4)
				{
					this.mainSelect++;
				}
				GameCanvas.keyPressed[6] = false;
			}
			else
			{
				bool flag5 = GameCanvas.keyPressed[2];
				if (flag5)
				{
					bool flag6 = num2 > 0;
					if (flag6)
					{
						this.mainSelect -= this.numw;
					}
					GameCanvas.keyPressed[2] = false;
				}
				else
				{
					bool flag7 = GameCanvas.keyPressed[8];
					if (flag7)
					{
						bool flag8 = num2 < this.numh - 1;
						if (flag8)
						{
							this.mainSelect += this.numw;
						}
						GameCanvas.keyPressed[8] = false;
					}
				}
			}
		}
		bool flag9 = this.mainSelect < 0;
		if (flag9)
		{
			this.mainSelect = 0;
		}
		bool flag10 = this.mainSelect >= this.vecServer.Length;
		if (flag10)
		{
			this.mainSelect = this.vecServer.Length - 1;
		}
		bool flag11 = GameCanvas.keyPressed[5];
		if (flag11)
		{
			this.vecServer[num].performAction();
			GameCanvas.keyPressed[5] = false;
		}
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x0008AE60 File Offset: 0x00089060
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 99;
		if (flag)
		{
			ServerListScreen.ipSelect = this.mainSelect;
			GameCanvas.serverScreen.selectServer();
			GameCanvas.serverScreen.switchToMe();
		}
		else
		{
			ServerListScreen.ipSelect = idAction - 100;
			GameCanvas.serverScreen.selectServer();
			GameCanvas.serverScreen.switchToMe();
		}
	}

	// Token: 0x04001029 RID: 4137
	private int mainSelect;

	// Token: 0x0400102A RID: 4138
	private Command[] vecServer;

	// Token: 0x0400102B RID: 4139
	private Command cmdCheck;

	// Token: 0x0400102C RID: 4140
	public const int icmd = 100;

	// Token: 0x0400102D RID: 4141
	private int wc;

	// Token: 0x0400102E RID: 4142
	private int hc;

	// Token: 0x0400102F RID: 4143
	private int w2c;

	// Token: 0x04001030 RID: 4144
	private int numw;

	// Token: 0x04001031 RID: 4145
	private int numh;
}
