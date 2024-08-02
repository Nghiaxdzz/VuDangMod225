using System;
using System.Collections;

// Token: 0x02000075 RID: 117
public class MyHashTable
{
	// Token: 0x0600059E RID: 1438 RVA: 0x00064730 File Offset: 0x00062930
	public object get(object k)
	{
		return this.h[k];
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0000526E File Offset: 0x0000346E
	public void clear()
	{
		this.h.Clear();
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x00064750 File Offset: 0x00062950
	public IDictionaryEnumerator GetEnumerator()
	{
		return this.h.GetEnumerator();
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x00064770 File Offset: 0x00062970
	public int size()
	{
		return this.h.Count;
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x00064790 File Offset: 0x00062990
	public void put(object k, object v)
	{
		bool flag = this.h.ContainsKey(k);
		if (flag)
		{
			this.h.Remove(k);
		}
		this.h.Add(k, v);
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0000527D File Offset: 0x0000347D
	public void remove(object k)
	{
		this.h.Remove(k);
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x0000527D File Offset: 0x0000347D
	public void Remove(string key)
	{
		this.h.Remove(key);
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x000647CC File Offset: 0x000629CC
	public bool containsKey(object key)
	{
		return this.h.ContainsKey(key);
	}

	// Token: 0x04000D54 RID: 3412
	public Hashtable h = new Hashtable();
}
