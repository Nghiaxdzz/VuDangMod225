using System;

// Token: 0x02000065 RID: 101
public class Message
{
	// Token: 0x060004A8 RID: 1192 RVA: 0x00004DD0 File Offset: 0x00002FD0
	public Message(int command)
	{
		this.command = (sbyte)command;
		this.dos = new myWriter();
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x00004DED File Offset: 0x00002FED
	public Message()
	{
		this.dos = new myWriter();
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x00004E02 File Offset: 0x00003002
	public Message(sbyte command)
	{
		this.command = command;
		this.dos = new myWriter();
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x00004E1E File Offset: 0x0000301E
	public Message(sbyte command, sbyte[] data)
	{
		this.command = command;
		this.dis = new myReader(data);
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x00059CDC File Offset: 0x00057EDC
	public sbyte[] getData()
	{
		return this.dos.getData();
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x00059CFC File Offset: 0x00057EFC
	public myReader reader()
	{
		return this.dis;
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00059D14 File Offset: 0x00057F14
	public myWriter writer()
	{
		return this.dos;
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00059D2C File Offset: 0x00057F2C
	public int readInt3Byte()
	{
		return this.dis.readInt();
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void cleanup()
	{
	}

	// Token: 0x04000A3F RID: 2623
	public sbyte command;

	// Token: 0x04000A40 RID: 2624
	private myReader dis;

	// Token: 0x04000A41 RID: 2625
	private myWriter dos;
}
