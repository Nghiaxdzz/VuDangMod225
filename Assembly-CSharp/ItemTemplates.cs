using System;

// Token: 0x02000057 RID: 87
public class ItemTemplates
{
	// Token: 0x06000440 RID: 1088 RVA: 0x00004B72 File Offset: 0x00002D72
	public static void add(ItemTemplate it)
	{
		ItemTemplates.itemTemplates.put(it.id, it);
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x00055428 File Offset: 0x00053628
	public static ItemTemplate get(short id)
	{
		return (ItemTemplate)ItemTemplates.itemTemplates.get(id);
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x00055450 File Offset: 0x00053650
	public static short getPart(short itemTemplateID)
	{
		return ItemTemplates.get(itemTemplateID).part;
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x00055470 File Offset: 0x00053670
	public static short getIcon(short itemTemplateID)
	{
		return ItemTemplates.get(itemTemplateID).iconID;
	}

	// Token: 0x04000946 RID: 2374
	public static MyHashTable itemTemplates = new MyHashTable();
}
