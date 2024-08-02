using System;

// Token: 0x02000059 RID: 89
public class Key
{
	// Token: 0x06000452 RID: 1106 RVA: 0x000558EC File Offset: 0x00053AEC
	public static void mapKeyPC()
	{
		bool isPC = Main.isPC;
		if (isPC)
		{
			Key.UP = 15;
			Key.DOWN = 16;
			Key.LEFT = 17;
			Key.RIGHT = 18;
		}
	}

	// Token: 0x0400094F RID: 2383
	public static int NUM0;

	// Token: 0x04000950 RID: 2384
	public static int NUM1 = 1;

	// Token: 0x04000951 RID: 2385
	public static int NUM2 = 2;

	// Token: 0x04000952 RID: 2386
	public static int NUM3 = 3;

	// Token: 0x04000953 RID: 2387
	public static int NUM4 = 4;

	// Token: 0x04000954 RID: 2388
	public static int NUM5 = 5;

	// Token: 0x04000955 RID: 2389
	public static int NUM6 = 6;

	// Token: 0x04000956 RID: 2390
	public static int NUM7 = 7;

	// Token: 0x04000957 RID: 2391
	public static int NUM8 = 8;

	// Token: 0x04000958 RID: 2392
	public static int NUM9 = 9;

	// Token: 0x04000959 RID: 2393
	public static int STAR = 10;

	// Token: 0x0400095A RID: 2394
	public static int BOUND = 11;

	// Token: 0x0400095B RID: 2395
	public static int UP = 12;

	// Token: 0x0400095C RID: 2396
	public static int DOWN = 13;

	// Token: 0x0400095D RID: 2397
	public static int LEFT = 14;

	// Token: 0x0400095E RID: 2398
	public static int RIGHT = 15;

	// Token: 0x0400095F RID: 2399
	public static int FIRE = 16;

	// Token: 0x04000960 RID: 2400
	public static int LEFT_SOFTKEY = 17;

	// Token: 0x04000961 RID: 2401
	public static int RIGHT_SOFTKEY = 18;

	// Token: 0x04000962 RID: 2402
	public static int CLEAR = 19;

	// Token: 0x04000963 RID: 2403
	public static int BACK = 20;
}
