using System;
using System.Collections.Generic;

namespace AssemblyCSharp.Mod.Xmap
{
	// Token: 0x020000CA RID: 202
	public struct GroupMap
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x000066B6 File Offset: 0x000048B6
		public GroupMap(string nameGroup, List<int> idMaps)
		{
			this.NameGroup = nameGroup;
			this.IdMaps = idMaps;
		}

		// Token: 0x0400133C RID: 4924
		public string NameGroup;

		// Token: 0x0400133D RID: 4925
		public List<int> IdMaps;
	}
}
