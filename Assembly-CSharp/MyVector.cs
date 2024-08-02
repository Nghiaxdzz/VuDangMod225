using System;
using System.Collections;

// Token: 0x0200007A RID: 122
public class MyVector
{
	// Token: 0x060005CB RID: 1483 RVA: 0x000052EF File Offset: 0x000034EF
	public MyVector()
	{
		this.a = new ArrayList();
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x000052EF File Offset: 0x000034EF
	public MyVector(string s)
	{
		this.a = new ArrayList();
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x00005304 File Offset: 0x00003504
	public MyVector(ArrayList a)
	{
		this.a = a;
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x00005315 File Offset: 0x00003515
	public void addElement(object o)
	{
		this.a.Add(o);
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x000651E8 File Offset: 0x000633E8
	public bool contains(object o)
	{
		return this.a.Contains(o);
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x00065210 File Offset: 0x00063410
	public int size()
	{
		bool flag = this.a == null;
		int result;
		if (flag)
		{
			result = 0;
		}
		else
		{
			result = this.a.Count;
		}
		return result;
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x00065240 File Offset: 0x00063440
	public object elementAt(int index)
	{
		bool flag = index > -1 && index < this.a.Count;
		object result;
		if (flag)
		{
			result = this.a[index];
		}
		else
		{
			result = null;
		}
		return result;
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x0006527C File Offset: 0x0006347C
	public void set(int index, object obj)
	{
		bool flag = index > -1 && index < this.a.Count;
		if (flag)
		{
			this.a[index] = obj;
		}
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x000652B4 File Offset: 0x000634B4
	public void setElementAt(object obj, int index)
	{
		bool flag = index > -1 && index < this.a.Count;
		if (flag)
		{
			this.a[index] = obj;
		}
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x000652EC File Offset: 0x000634EC
	public int indexOf(object o)
	{
		return this.a.IndexOf(o);
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x0006530C File Offset: 0x0006350C
	public void removeElementAt(int index)
	{
		bool flag = index > -1 && index < this.a.Count;
		if (flag)
		{
			this.a.RemoveAt(index);
		}
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x00005325 File Offset: 0x00003525
	public void removeElement(object o)
	{
		this.a.Remove(o);
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x00005335 File Offset: 0x00003535
	public void removeAllElements()
	{
		this.a.Clear();
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x00005344 File Offset: 0x00003544
	public void insertElementAt(object o, int i)
	{
		this.a.Insert(i, o);
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x00065344 File Offset: 0x00063544
	public object firstElement()
	{
		return this.a[0];
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x00065364 File Offset: 0x00063564
	public object lastElement()
	{
		return this.a[this.a.Count - 1];
	}

	// Token: 0x04000D5C RID: 3420
	private ArrayList a;
}
