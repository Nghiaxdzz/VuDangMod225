using System;
using System.Collections.Generic;

namespace AssemblyCSharp.Mod.Xmap
{
	// Token: 0x020000CE RID: 206
	public class XmapAlgorithm
	{
		// Token: 0x06000A50 RID: 2640 RVA: 0x000A48AC File Offset: 0x000A2AAC
		public static List<int> FindWay(int idMapStart, int idMapEnd)
		{
			List<int> wayPassedStart = XmapAlgorithm.GetWayPassedStart(idMapStart);
			return XmapAlgorithm.FindWay(idMapEnd, wayPassedStart);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x000A48CC File Offset: 0x000A2ACC
		private static List<int> FindWay(int idMapEnd, List<int> wayPassed)
		{
			int num = wayPassed[wayPassed.Count - 1];
			bool flag = num == idMapEnd;
			List<int> result;
			if (flag)
			{
				result = wayPassed;
			}
			else
			{
				bool flag2 = !XmapData.Instance().CanGetMapNexts(num);
				if (flag2)
				{
					result = null;
				}
				else
				{
					List<List<int>> list = new List<List<int>>();
					foreach (MapNext mapNext in XmapData.Instance().GetMapNexts(num))
					{
						List<int> list2 = null;
						bool flag3 = !wayPassed.Contains(mapNext.MapID);
						if (flag3)
						{
							List<int> wayPassedNext = XmapAlgorithm.GetWayPassedNext(wayPassed, mapNext.MapID);
							list2 = XmapAlgorithm.FindWay(idMapEnd, wayPassedNext);
						}
						bool flag4 = list2 != null;
						if (flag4)
						{
							list.Add(list2);
						}
					}
					result = XmapAlgorithm.GetBestWay(list);
				}
			}
			return result;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000A49BC File Offset: 0x000A2BBC
		private static List<int> GetBestWay(List<List<int>> ways)
		{
			bool flag = ways.Count == 0;
			List<int> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<int> list = ways[0];
				for (int i = 1; i < ways.Count; i++)
				{
					bool flag2 = XmapAlgorithm.IsWayBetter(ways[i], list);
					if (flag2)
					{
						list = ways[i];
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x000A4A20 File Offset: 0x000A2C20
		private static List<int> GetWayPassedStart(int idMapStart)
		{
			return new List<int>
			{
				idMapStart
			};
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x000A4A40 File Offset: 0x000A2C40
		private static List<int> GetWayPassedNext(List<int> wayPassed, int idMapNext)
		{
			return new List<int>(wayPassed)
			{
				idMapNext
			};
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000A4A60 File Offset: 0x000A2C60
		private static bool IsWayBetter(List<int> way1, List<int> way2)
		{
			bool flag = XmapAlgorithm.IsBadWay(way1);
			bool flag2 = XmapAlgorithm.IsBadWay(way2);
			return (!flag || flag2) && ((!flag && flag2) || way1.Count < way2.Count);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000A4AA4 File Offset: 0x000A2CA4
		private static bool IsBadWay(List<int> way)
		{
			return XmapAlgorithm.IsWayGoFutureAndBack(way);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000A4ABC File Offset: 0x000A2CBC
		private static bool IsWayGoFutureAndBack(List<int> way)
		{
			List<int> list = new List<int>
			{
				27,
				28,
				29
			};
			for (int i = 1; i < way.Count - 1; i++)
			{
				bool flag = way[i] == 102 && way[i + 1] == 24 && list.Contains(way[i - 1]);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}
	}
}
