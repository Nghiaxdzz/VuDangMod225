using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AssemblyCSharp.Mod.Xmap
{
	// Token: 0x020000D0 RID: 208
	public class XmapData
	{
		// Token: 0x06000A73 RID: 2675 RVA: 0x000067EA File Offset: 0x000049EA
		private XmapData()
		{
			this.GroupMaps = new List<GroupMap>();
			this.MyLinkMaps = null;
			this.IsLoading = false;
			this.IsLoadingCapsule = false;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000A5440 File Offset: 0x000A3640
		public static XmapData Instance()
		{
			bool flag = XmapData._Instance == null;
			if (flag)
			{
				XmapData._Instance = new XmapData();
			}
			return XmapData._Instance;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00006814 File Offset: 0x00004A14
		public void LoadLinkMaps()
		{
			this.IsLoading = true;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000A5470 File Offset: 0x000A3670
		public void Update()
		{
			bool isLoadingCapsule = this.IsLoadingCapsule;
			if (isLoadingCapsule)
			{
				bool flag = !this.IsWaitInfoMapTrans();
				if (flag)
				{
					this.LoadLinkMapCapsule();
					this.IsLoadingCapsule = false;
					this.IsLoading = false;
				}
			}
			else
			{
				this.LoadLinkMapBase();
				bool flag2 = XmapData.CanUseCapsuleVip();
				if (flag2)
				{
					XmapController.UseCapsuleVip();
					this.IsLoadingCapsule = true;
				}
				else
				{
					bool flag3 = XmapData.CanUseCapsuleNormal();
					if (flag3)
					{
						XmapController.UseCapsuleNormal();
						this.IsLoadingCapsule = true;
					}
					else
					{
						this.IsLoading = false;
					}
				}
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x000A54F0 File Offset: 0x000A36F0
		public void LoadGroupMapsFromFile(string path)
		{
			this.GroupMaps.Clear();
			try
			{
				StreamReader streamReader = new StreamReader(path);
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					text = text.Trim();
					bool flag = !text.StartsWith("#") && !text.Equals("");
					if (flag)
					{
						List<int> idMaps = Array.ConvertAll<string, int>(streamReader.ReadLine().Trim().Split(new char[]
						{
							' '
						}), (string s) => int.Parse(s)).ToList<int>();
						this.GroupMaps.Add(new GroupMap(text, idMaps));
					}
				}
			}
			catch (Exception ex)
			{
				GameScr.info1.addInfo(ex.Message, 0);
			}
			this.RemoveMapsHomeInGroupMaps();
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x000A55E4 File Offset: 0x000A37E4
		private void RemoveMapsHomeInGroupMaps()
		{
			int cgender = global::Char.myCharz().cgender;
			foreach (GroupMap groupMap in this.GroupMaps)
			{
				bool flag = cgender != 0;
				if (flag)
				{
					bool flag2 = cgender != 1;
					if (flag2)
					{
						groupMap.IdMaps.Remove(21);
						groupMap.IdMaps.Remove(22);
					}
					else
					{
						groupMap.IdMaps.Remove(21);
						groupMap.IdMaps.Remove(23);
					}
				}
				else
				{
					groupMap.IdMaps.Remove(22);
					groupMap.IdMaps.Remove(23);
				}
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000A56B8 File Offset: 0x000A38B8
		private void LoadLinkMapCapsule()
		{
			this.AddKeyLinkMaps(TileMap.mapID);
			string[] mapNames = GameCanvas.panel.mapNames;
			for (int i = 0; i < mapNames.Length; i++)
			{
				int idMapFromName = XmapData.GetIdMapFromName(mapNames[i]);
				bool flag = idMapFromName != -1;
				if (flag)
				{
					int[] info = new int[]
					{
						i
					};
					this.MyLinkMaps[TileMap.mapID].Add(new MapNext(idMapFromName, TypeMapNext.Capsule, info));
				}
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0000681E File Offset: 0x00004A1E
		private void LoadLinkMapBase()
		{
			this.MyLinkMaps = new Dictionary<int, List<MapNext>>();
			this.LoadLinkMapsFromFile("Dragonboy_vn_v225_Data\\TextData\\LinkMapsXmap.txt");
			this.LoadLinkMapsAutoWaypointFromFile("Dragonboy_vn_v225_Data\\TextData\\AutoLinkMapsWaypoint.txt");
			this.LoadLinkMapsHome();
			this.LoadLinkMapSieuThi();
			this.LoadLinkMapToCold();
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000A5734 File Offset: 0x000A3934
		private void LoadLinkMapsFromFile(string path)
		{
			try
			{
				StreamReader streamReader = new StreamReader(path);
				string arg = string.Empty;
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					text = text.Trim();
					bool flag = !text.StartsWith("#") && !text.Equals("");
					if (flag)
					{
						int[] array = Array.ConvertAll<string, int>(text.Split(new char[]
						{
							' '
						}), (string s) => int.Parse(s));
						for (int i = 0; i < array.Length; i++)
						{
							arg = arg + array[i] + " ";
						}
						int num = array.Length - 3;
						int[] array2 = new int[num];
						Array.Copy(array, 3, array2, 0, num);
						this.LoadLinkMap(array[0], array[1], (TypeMapNext)array[2], array2);
					}
				}
			}
			catch (Exception ex)
			{
				GameScr.info1.addInfo(ex.Message, 0);
			}
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x000A5864 File Offset: 0x000A3A64
		private void LoadLinkMapsAutoWaypointFromFile(string path)
		{
			try
			{
				StreamReader streamReader = new StreamReader(path);
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					text = text.Trim();
					bool flag = !text.StartsWith("#") && !text.Equals("");
					if (flag)
					{
						int[] array = Array.ConvertAll<string, int>(text.Split(new char[]
						{
							' '
						}), (string s) => int.Parse(s));
						for (int i = 0; i < array.Length; i++)
						{
							bool flag2 = i != 0;
							if (flag2)
							{
								this.LoadLinkMap(array[i], array[i - 1], TypeMapNext.AutoWaypoint, null);
							}
							bool flag3 = i != array.Length - 1;
							if (flag3)
							{
								this.LoadLinkMap(array[i], array[i + 1], TypeMapNext.AutoWaypoint, null);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				GameScr.info1.addInfo(ex.Message, 0);
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x000A5988 File Offset: 0x000A3B88
		private void LoadLinkMapsHome()
		{
			int cgender = global::Char.myCharz().cgender;
			int num = 21 + cgender;
			int num2 = 7 * cgender;
			this.LoadLinkMap(num2, num, TypeMapNext.AutoWaypoint, null);
			this.LoadLinkMap(num, num2, TypeMapNext.AutoWaypoint, null);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x000A59C0 File Offset: 0x000A3BC0
		private void LoadLinkMapSieuThi()
		{
			int cgender = global::Char.myCharz().cgender;
			int idMapNext = 24 + cgender;
			int[] array = new int[2];
			array[0] = 10;
			int[] info = array;
			this.LoadLinkMap(84, idMapNext, TypeMapNext.NpcMenu, info);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x000A59F8 File Offset: 0x000A3BF8
		private void LoadLinkMapToCold()
		{
			bool flag = global::Char.myCharz().taskMaint.taskId > 30;
			if (flag)
			{
				int[] array = new int[2];
				array[0] = 12;
				int[] info = array;
				this.LoadLinkMap(19, 109, TypeMapNext.NpcMenu, info);
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x000A5A3C File Offset: 0x000A3C3C
		public List<MapNext> GetMapNexts(int idMap)
		{
			bool flag = this.CanGetMapNexts(idMap);
			List<MapNext> result;
			if (flag)
			{
				result = this.MyLinkMaps[idMap];
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x000A5A6C File Offset: 0x000A3C6C
		public bool CanGetMapNexts(int idMap)
		{
			return this.MyLinkMaps.ContainsKey(idMap);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x000A5A8C File Offset: 0x000A3C8C
		private void LoadLinkMap(int idMapStart, int idMapNext, TypeMapNext type, int[] info)
		{
			this.AddKeyLinkMaps(idMapStart);
			MapNext item = new MapNext(idMapNext, type, info);
			this.MyLinkMaps[idMapStart].Add(item);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x000A5AC0 File Offset: 0x000A3CC0
		private void AddKeyLinkMaps(int idMap)
		{
			bool flag = !this.MyLinkMaps.ContainsKey(idMap);
			if (flag)
			{
				this.MyLinkMaps.Add(idMap, new List<MapNext>());
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x000A5AF8 File Offset: 0x000A3CF8
		private bool IsWaitInfoMapTrans()
		{
			return !Pk9rXmap.IsShowPanelMapTrans;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000A5B14 File Offset: 0x000A3D14
		public static int GetIdMapFromPanelXmap(string mapName)
		{
			return int.Parse(mapName.Split(new char[]
			{
				':'
			})[0]);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000A5B40 File Offset: 0x000A3D40
		public static Waypoint FindWaypoint(int idMap)
		{
			int i = 0;
			while (i < TileMap.vGo.size())
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
				bool flag = XmapController.IdMapEnd == 124 && TileMap.mapID == 123;
				if (flag)
				{
					for (int j = 0; j < TileMap.vGo.size(); j++)
					{
						Waypoint result = (Waypoint)TileMap.vGo.elementAt(j);
						bool flag2 = j == TileMap.vGo.size() - 1;
						if (flag2)
						{
							return result;
						}
					}
				}
				bool flag3 = XmapData.GetTextPopup(waypoint.popup).Trim().ToLower().Equals(XmapController.get_map_names(idMap).Trim().ToLower());
				if (!flag3)
				{
					i++;
					continue;
				}
				return waypoint;
			}
			return null;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x000A5C24 File Offset: 0x000A3E24
		public static int GetPosWaypointX(Waypoint waypoint)
		{
			bool flag = waypoint.maxX < 60;
			int result;
			if (flag)
			{
				result = 15;
			}
			else
			{
				bool flag2 = (int)waypoint.minX > TileMap.pxw - 60;
				if (flag2)
				{
					result = TileMap.pxw - 15;
				}
				else
				{
					result = (int)(waypoint.minX + 30);
				}
			}
			return result;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000A5C74 File Offset: 0x000A3E74
		public static int GetPosWaypointY(Waypoint waypoint)
		{
			return (int)waypoint.maxY;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x000A5C8C File Offset: 0x000A3E8C
		public static bool IsMyCharDie()
		{
			return global::Char.myCharz().statusMe == 14 || global::Char.myCharz().cHP <= 0;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x000A5CC0 File Offset: 0x000A3EC0
		public static bool CanNextMap()
		{
			return !global::Char.isLoadingMap && !global::Char.ischangingMap && !Controller.isStopReadMessage;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000A5CEC File Offset: 0x000A3EEC
		private static int GetIdMapFromName(string mapName)
		{
			int cgender = global::Char.myCharz().cgender;
			bool flag = mapName.Equals("Về nhà");
			int result;
			if (flag)
			{
				result = 21 + cgender;
			}
			else
			{
				bool flag2 = mapName.Equals("Trạm tàu vũ trụ");
				if (flag2)
				{
					result = 24 + cgender;
				}
				else
				{
					bool flag3 = mapName.Contains("Về chỗ cũ: ");
					if (flag3)
					{
						mapName = mapName.Replace("Về chỗ cũ: ", "");
						bool flag4 = XmapController.get_map_names(Pk9rXmap.IdMapCapsuleReturn).Equals(mapName);
						if (flag4)
						{
							return Pk9rXmap.IdMapCapsuleReturn;
						}
						bool flag5 = mapName.Equals("Rừng đá");
						if (flag5)
						{
							return -1;
						}
					}
					for (int i = 0; i < TileMap.mapNames.Length; i++)
					{
						bool flag6 = mapName.Equals(XmapController.get_map_names(i));
						if (flag6)
						{
							return i;
						}
					}
					result = -1;
				}
			}
			return result;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x000A5DD0 File Offset: 0x000A3FD0
		public static string GetTextPopup(PopUp popUp)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < popUp.says.Length; i++)
			{
				stringBuilder.Append(popUp.says[i]);
				stringBuilder.Append(" ");
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x000A5E28 File Offset: 0x000A4028
		private static bool CanUseCapsuleNormal()
		{
			return !XmapData.IsMyCharDie() && Pk9rXmap.IsUseCapsuleNormal && XmapData.HasItemCapsuleNormal();
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x000A5E50 File Offset: 0x000A4050
		private static bool HasItemCapsuleNormal()
		{
			Item[] arrItemBag = global::Char.myCharz().arrItemBag;
			for (int i = 0; i < arrItemBag.Length; i++)
			{
				bool flag = arrItemBag[i] != null && arrItemBag[i].template.id == 193;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x000A5EAC File Offset: 0x000A40AC
		private static bool CanUseCapsuleVip()
		{
			return !XmapData.IsMyCharDie() && Pk9rXmap.IsUseCapsuleVip && XmapData.HasItemCapsuleVip();
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x000A5ED4 File Offset: 0x000A40D4
		private static bool HasItemCapsuleVip()
		{
			Item[] arrItemBag = global::Char.myCharz().arrItemBag;
			for (int i = 0; i < arrItemBag.Length; i++)
			{
				bool flag = arrItemBag[i] != null && arrItemBag[i].template.id == 194;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400135D RID: 4957
		private const int ID_MAP_HOME_BASE = 21;

		// Token: 0x0400135E RID: 4958
		private const int ID_MAP_TTVT_BASE = 24;

		// Token: 0x0400135F RID: 4959
		private const int ID_ITEM_CAPSUAL_VIP = 194;

		// Token: 0x04001360 RID: 4960
		private const int ID_ITEM_CAPSUAL_NORMAL = 193;

		// Token: 0x04001361 RID: 4961
		private const int ID_MAP_TPVGT = 19;

		// Token: 0x04001362 RID: 4962
		private const int ID_MAP_TO_COLD = 109;

		// Token: 0x04001363 RID: 4963
		public List<GroupMap> GroupMaps;

		// Token: 0x04001364 RID: 4964
		public Dictionary<int, List<MapNext>> MyLinkMaps;

		// Token: 0x04001365 RID: 4965
		public bool IsLoading;

		// Token: 0x04001366 RID: 4966
		private bool IsLoadingCapsule;

		// Token: 0x04001367 RID: 4967
		private static XmapData _Instance;
	}
}
