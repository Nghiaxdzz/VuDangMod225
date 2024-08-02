using System;

// Token: 0x020000C6 RID: 198
public class Waypoint : IActionListener
{
	// Token: 0x06000A22 RID: 2594 RVA: 0x000A1EEC File Offset: 0x000A00EC
	public Waypoint(short minX, short minY, short maxX, short maxY, bool isEnter, bool isOffline, string name)
	{
		this.minX = minX;
		this.minY = minY;
		this.maxX = maxX;
		this.maxY = maxY;
		name = Res.changeString(name);
		this.isEnter = isEnter;
		this.isOffline = isOffline;
		bool flag = ((TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23) && this.minX >= 0 && this.minX <= 24) || (((TileMap.mapID == 0 && global::Char.myCharz().cgender != 0) || (TileMap.mapID == 7 && global::Char.myCharz().cgender != 1) || (TileMap.mapID == 14 && global::Char.myCharz().cgender != 2)) && isOffline);
		if (!flag)
		{
			bool flag2 = TileMap.isInAirMap() || TileMap.mapID == 47;
			if (flag2)
			{
				bool flag3 = minY <= 150 || !TileMap.isInAirMap();
				if (flag3)
				{
					this.popup = new PopUp(name, (int)(minX + (maxX - minX) / 2), (int)(maxY - ((minX <= 100) ? 48 : 24)));
					this.popup.command = new Command(null, this, 1, this);
					this.popup.isWayPoint = true;
					this.popup.isPaint = false;
					PopUp.addPopUp(this.popup);
					TileMap.vGo.addElement(this);
				}
			}
			else
			{
				bool flag4 = !isEnter && !isOffline;
				if (flag4)
				{
					this.popup = new PopUp(name, (int)minX, (int)(minY - 24));
					this.popup.command = new Command(null, this, 1, this);
					this.popup.isWayPoint = true;
					this.popup.isPaint = false;
					PopUp.addPopUp(this.popup);
				}
				else
				{
					bool flag5 = TileMap.isTrainingMap();
					if (flag5)
					{
						this.popup = new PopUp(name, (int)minX, (int)(minY - 16));
					}
					else
					{
						int x = (int)(minX + (maxX - minX) / 2);
						this.popup = new PopUp(name, x, (int)(minY - ((minY == 0) ? -32 : 16)));
					}
					this.popup.command = new Command(null, this, 2, this);
					this.popup.isWayPoint = true;
					this.popup.isPaint = false;
					PopUp.addPopUp(this.popup);
				}
				TileMap.vGo.addElement(this);
			}
		}
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x000A2148 File Offset: 0x000A0348
	public void perform(int idAction, object p)
	{
		if (idAction != 1)
		{
			if (idAction == 2)
			{
				GameScr.gI().auto = 0;
				bool flag = global::Char.myCharz().isInEnterOfflinePoint() != null;
				if (flag)
				{
					Service.gI().charMove();
					InfoDlg.showWait();
					Service.gI().getMapOffline();
					global::Char.ischangingMap = true;
				}
				else
				{
					bool flag2 = global::Char.myCharz().isInEnterOnlinePoint() != null;
					if (flag2)
					{
						Service.gI().charMove();
						Service.gI().requestChangeMap();
						global::Char.isLockKey = true;
						global::Char.ischangingMap = true;
						GameCanvas.clearKeyHold();
						GameCanvas.clearKeyPressed();
						InfoDlg.showWait();
					}
					else
					{
						int xEnd = (int)((this.minX + this.maxX) / 2);
						int yEnd = (int)this.maxY;
						global::Char.myCharz().currentMovePoint = new MovePoint(xEnd, yEnd);
						global::Char.myCharz().cdir = ((global::Char.myCharz().cx - global::Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : -1);
						global::Char.myCharz().endMovePointCommand = new Command(null, this, 2, null);
					}
				}
			}
		}
		else
		{
			int xEnd2 = (int)((this.minX + this.maxX) / 2);
			int yEnd2 = (int)this.maxY;
			bool flag3 = this.maxY > this.minY + 24;
			if (flag3)
			{
				yEnd2 = (int)((this.minY + this.maxY) / 2);
			}
			GameScr.gI().auto = 0;
			global::Char.myCharz().currentMovePoint = new MovePoint(xEnd2, yEnd2);
			global::Char.myCharz().cdir = ((global::Char.myCharz().cx - global::Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : -1);
			Service.gI().charMove();
		}
	}

	// Token: 0x0400130E RID: 4878
	public short minX;

	// Token: 0x0400130F RID: 4879
	public short minY;

	// Token: 0x04001310 RID: 4880
	public short maxX;

	// Token: 0x04001311 RID: 4881
	public short maxY;

	// Token: 0x04001312 RID: 4882
	public bool isEnter;

	// Token: 0x04001313 RID: 4883
	public bool isOffline;

	// Token: 0x04001314 RID: 4884
	public PopUp popup;
}
