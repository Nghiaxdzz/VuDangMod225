using System;

namespace AssemblyCSharp.Mod.Xmap
{
	// Token: 0x020000CB RID: 203
	public struct MapNext
	{
		// Token: 0x06000A43 RID: 2627 RVA: 0x000066C7 File Offset: 0x000048C7
		public MapNext(int mapID, TypeMapNext type, int[] info)
		{
			this.MapID = mapID;
			this.Type = type;
			this.Info = info;
		}

		// Token: 0x0400133E RID: 4926
		public int MapID;

		// Token: 0x0400133F RID: 4927
		public TypeMapNext Type;

		// Token: 0x04001340 RID: 4928
		public int[] Info;
	}
}
