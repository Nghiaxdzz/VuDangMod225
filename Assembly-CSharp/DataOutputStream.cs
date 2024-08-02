using System;

// Token: 0x0200001F RID: 31
public class DataOutputStream
{
	// Token: 0x060001D4 RID: 468 RVA: 0x00003F27 File Offset: 0x00002127
	public void writeShort(short i)
	{
		this.w.writeShort(i);
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00003F37 File Offset: 0x00002137
	public void writeInt(int i)
	{
		this.w.writeInt(i);
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00003F47 File Offset: 0x00002147
	public void write(sbyte[] data)
	{
		this.w.writeSByte(data);
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x000355C8 File Offset: 0x000337C8
	public sbyte[] toByteArray()
	{
		return this.w.getData();
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x00003F57 File Offset: 0x00002157
	public void close()
	{
		this.w.Close();
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x00003F66 File Offset: 0x00002166
	public void writeByte(sbyte b)
	{
		this.w.writeByte(b);
	}

	// Token: 0x060001DA RID: 474 RVA: 0x00003F76 File Offset: 0x00002176
	public void writeUTF(string name)
	{
		this.w.writeUTF(name);
	}

	// Token: 0x060001DB RID: 475 RVA: 0x00003F86 File Offset: 0x00002186
	public void writeBoolean(bool b)
	{
		this.w.writeBoolean(b);
	}

	// Token: 0x0400048A RID: 1162
	private myWriter w = new myWriter();
}
