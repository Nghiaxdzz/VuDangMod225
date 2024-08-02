using System;

// Token: 0x02000040 RID: 64
public interface IMapObject
{
	// Token: 0x0600038A RID: 906
	int getX();

	// Token: 0x0600038B RID: 907
	int getY();

	// Token: 0x0600038C RID: 908
	int getW();

	// Token: 0x0600038D RID: 909
	int getH();

	// Token: 0x0600038E RID: 910
	void stopMoving();

	// Token: 0x0600038F RID: 911
	bool isInvisible();
}
