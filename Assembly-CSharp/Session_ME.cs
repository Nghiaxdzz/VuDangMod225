using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

// Token: 0x02000098 RID: 152
public class Session_ME : ISession
{
	// Token: 0x06000890 RID: 2192 RVA: 0x00005CE4 File Offset: 0x00003EE4
	public Session_ME()
	{
		Debug.Log("init Session_ME");
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x00005CF9 File Offset: 0x00003EF9
	public void clearSendingMessage()
	{
		Session_ME.sender.sendingMessage.Clear();
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x000902D0 File Offset: 0x0008E4D0
	public static Session_ME gI()
	{
		bool flag = Session_ME.instance == null;
		if (flag)
		{
			Session_ME.instance = new Session_ME();
		}
		return Session_ME.instance;
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x00090300 File Offset: 0x0008E500
	public bool isConnected()
	{
		return Session_ME.connected;
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x00005D0C File Offset: 0x00003F0C
	public void setHandler(IMessageHandler msgHandler)
	{
		Session_ME.messageHandler = msgHandler;
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x00090318 File Offset: 0x0008E518
	public void connect(string host, int port)
	{
		bool flag = !Session_ME.connected && !Session_ME.connecting;
		if (flag)
		{
			bool flag2 = Session_ME.isMainSession;
			if (flag2)
			{
				ServerListScreen.testConnect = -1;
			}
			this.host = host;
			this.port = port;
			Session_ME.getKeyComplete = false;
			Session_ME.sc = null;
			Debug.Log("connecting...!");
			Debug.Log("host: " + host);
			Debug.Log("port: " + port);
			Session_ME.initThread = new Thread(new ThreadStart(this.NetworkInit));
			Session_ME.initThread.Start();
		}
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x000903C0 File Offset: 0x0008E5C0
	private void NetworkInit()
	{
		Session_ME.isCancel = false;
		Session_ME.connecting = true;
		Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
		Session_ME.connected = true;
		try
		{
			this.doConnect(this.host, this.port);
			Session_ME.messageHandler.onConnectOK(Session_ME.isMainSession);
		}
		catch (Exception)
		{
			bool flag = Session_ME.messageHandler != null;
			if (flag)
			{
				this.close();
				Session_ME.messageHandler.onConnectionFail(Session_ME.isMainSession);
			}
		}
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00090450 File Offset: 0x0008E650
	public void doConnect(string host, int port)
	{
		Session_ME.sc = new TcpClient();
		Session_ME.sc.Connect(host, port);
		Session_ME.dataStream = Session_ME.sc.GetStream();
		Session_ME.dis = new BinaryReader(Session_ME.dataStream, new UTF8Encoding());
		Session_ME.dos = new BinaryWriter(Session_ME.dataStream, new UTF8Encoding());
		new Thread(new ThreadStart(Session_ME.sender.run)).Start();
		Session_ME.MessageCollector @object = new Session_ME.MessageCollector();
		Cout.LogError("new -----");
		Session_ME.collectorThread = new Thread(new ThreadStart(@object.run));
		Session_ME.collectorThread.Start();
		Session_ME.timeConnected = Session_ME.currentTimeMillis();
		Session_ME.connecting = false;
		Session_ME.doSendMessage(new Message(-27));
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x00005D15 File Offset: 0x00003F15
	public void sendMessage(Message message)
	{
		Session_ME.count++;
		Res.outz("SEND MSG: " + message.command);
		Session_ME.sender.AddMessage(message);
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x00090518 File Offset: 0x0008E718
	private static void doSendMessage(Message m)
	{
		sbyte[] data = m.getData();
		try
		{
			bool flag = Session_ME.getKeyComplete;
			if (flag)
			{
				sbyte value = Session_ME.writeKey(m.command);
				Session_ME.dos.Write(value);
			}
			else
			{
				Session_ME.dos.Write(m.command);
			}
			bool flag2 = data != null;
			if (flag2)
			{
				int num = data.Length;
				bool flag3 = Session_ME.getKeyComplete;
				if (flag3)
				{
					int num2 = (int)Session_ME.writeKey((sbyte)(num >> 8));
					Session_ME.dos.Write((sbyte)num2);
					int num3 = (int)Session_ME.writeKey((sbyte)(num & 255));
					Session_ME.dos.Write((sbyte)num3);
				}
				else
				{
					Session_ME.dos.Write((ushort)num);
				}
				bool flag4 = Session_ME.getKeyComplete;
				if (flag4)
				{
					for (int i = 0; i < data.Length; i++)
					{
						sbyte value2 = Session_ME.writeKey(data[i]);
						Session_ME.dos.Write(value2);
					}
				}
				Session_ME.sendByteCount += 5 + data.Length;
			}
			else
			{
				bool flag5 = Session_ME.getKeyComplete;
				if (flag5)
				{
					int num4 = 0;
					int num5 = (int)Session_ME.writeKey((sbyte)(num4 >> 8));
					Session_ME.dos.Write((sbyte)num5);
					int num6 = (int)Session_ME.writeKey((sbyte)(num4 & 255));
					Session_ME.dos.Write((sbyte)num6);
				}
				else
				{
					Session_ME.dos.Write(0);
				}
				Session_ME.sendByteCount += 5;
			}
			Session_ME.dos.Flush();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.StackTrace);
		}
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x000906C4 File Offset: 0x0008E8C4
	public static sbyte readKey(sbyte b)
	{
		sbyte[] array = Session_ME.key;
		sbyte b2 = Session_ME.curR;
		Session_ME.curR = b2 + 1;
		sbyte result = (sbyte)(((int)array[(int)b2] & 255) ^ ((int)b & 255));
		bool flag = (int)Session_ME.curR >= Session_ME.key.Length;
		if (flag)
		{
			Session_ME.curR %= (sbyte)Session_ME.key.Length;
		}
		return result;
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x0009072C File Offset: 0x0008E92C
	public static sbyte writeKey(sbyte b)
	{
		sbyte[] array = Session_ME.key;
		sbyte b2 = Session_ME.curW;
		Session_ME.curW = b2 + 1;
		sbyte result = (sbyte)(((int)array[(int)b2] & 255) ^ ((int)b & 255));
		bool flag = (int)Session_ME.curW >= Session_ME.key.Length;
		if (flag)
		{
			Session_ME.curW %= (sbyte)Session_ME.key.Length;
		}
		return result;
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x00090794 File Offset: 0x0008E994
	public static void onRecieveMsg(Message msg)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		if (flag)
		{
			Session_ME.messageHandler.onMessage(msg);
		}
		else
		{
			Session_ME.recieveMsg.addElement(msg);
		}
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x000907D8 File Offset: 0x0008E9D8
	public static void update()
	{
		while (Session_ME.recieveMsg.size() > 0)
		{
			Message message = (Message)Session_ME.recieveMsg.elementAt(0);
			bool isStopReadMessage = Controller.isStopReadMessage;
			if (isStopReadMessage)
			{
				break;
			}
			bool flag = message == null;
			if (flag)
			{
				Session_ME.recieveMsg.removeElementAt(0);
				break;
			}
			Session_ME.messageHandler.onMessage(message);
			Session_ME.recieveMsg.removeElementAt(0);
		}
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x00005D4B File Offset: 0x00003F4B
	public void close()
	{
		Session_ME.cleanNetwork();
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x00090848 File Offset: 0x0008EA48
	private static void cleanNetwork()
	{
		Session_ME.key = null;
		Session_ME.curR = 0;
		Session_ME.curW = 0;
		try
		{
			Session_ME.connected = false;
			Session_ME.connecting = false;
			bool flag = Session_ME.sc != null;
			if (flag)
			{
				Session_ME.sc.Close();
				Session_ME.sc = null;
			}
			bool flag2 = Session_ME.dataStream != null;
			if (flag2)
			{
				Session_ME.dataStream.Close();
				Session_ME.dataStream = null;
			}
			bool flag3 = Session_ME.dos != null;
			if (flag3)
			{
				Session_ME.dos.Close();
				Session_ME.dos = null;
			}
			bool flag4 = Session_ME.dis != null;
			if (flag4)
			{
				Session_ME.dis.Close();
				Session_ME.dis = null;
			}
			Session_ME.sendThread = null;
			Session_ME.collectorThread = null;
			bool flag5 = Session_ME.isMainSession;
			if (flag5)
			{
				ServerListScreen.testConnect = 0;
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x00090928 File Offset: 0x0008EB28
	public static int currentTimeMillis()
	{
		return Environment.TickCount;
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x00050910 File Offset: 0x0004EB10
	public static byte convertSbyteToByte(sbyte var)
	{
		bool flag = var > 0;
		byte result;
		if (flag)
		{
			result = (byte)var;
		}
		else
		{
			result = (byte)((int)var + 256);
		}
		return result;
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x00050938 File Offset: 0x0004EB38
	public static byte[] convertSbyteToByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			bool flag = var[i] > 0;
			if (flag)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)((int)var[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x0400103A RID: 4154
	protected static Session_ME instance = new Session_ME();

	// Token: 0x0400103B RID: 4155
	private static NetworkStream dataStream;

	// Token: 0x0400103C RID: 4156
	private static BinaryReader dis;

	// Token: 0x0400103D RID: 4157
	private static BinaryWriter dos;

	// Token: 0x0400103E RID: 4158
	public static IMessageHandler messageHandler;

	// Token: 0x0400103F RID: 4159
	public static bool isMainSession = true;

	// Token: 0x04001040 RID: 4160
	private static TcpClient sc;

	// Token: 0x04001041 RID: 4161
	public static bool connected;

	// Token: 0x04001042 RID: 4162
	public static bool connecting;

	// Token: 0x04001043 RID: 4163
	private static Session_ME.Sender sender = new Session_ME.Sender();

	// Token: 0x04001044 RID: 4164
	public static Thread initThread;

	// Token: 0x04001045 RID: 4165
	public static Thread collectorThread;

	// Token: 0x04001046 RID: 4166
	public static Thread sendThread;

	// Token: 0x04001047 RID: 4167
	public static int sendByteCount;

	// Token: 0x04001048 RID: 4168
	public static int recvByteCount;

	// Token: 0x04001049 RID: 4169
	private static bool getKeyComplete;

	// Token: 0x0400104A RID: 4170
	public static sbyte[] key = null;

	// Token: 0x0400104B RID: 4171
	private static sbyte curR;

	// Token: 0x0400104C RID: 4172
	private static sbyte curW;

	// Token: 0x0400104D RID: 4173
	private static int timeConnected;

	// Token: 0x0400104E RID: 4174
	private long lastTimeConn;

	// Token: 0x0400104F RID: 4175
	public static string strRecvByteCount = string.Empty;

	// Token: 0x04001050 RID: 4176
	public static bool isCancel;

	// Token: 0x04001051 RID: 4177
	private string host;

	// Token: 0x04001052 RID: 4178
	private int port;

	// Token: 0x04001053 RID: 4179
	public static int count;

	// Token: 0x04001054 RID: 4180
	public static MyVector recieveMsg = new MyVector();

	// Token: 0x02000099 RID: 153
	public class Sender
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x00005D8A File Offset: 0x00003F8A
		public Sender()
		{
			this.sendingMessage = new List<Message>();
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00005D9F File Offset: 0x00003F9F
		public void AddMessage(Message message)
		{
			this.sendingMessage.Add(message);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00090940 File Offset: 0x0008EB40
		public void run()
		{
			while (Session_ME.connected)
			{
				try
				{
					bool getKeyComplete = Session_ME.getKeyComplete;
					if (getKeyComplete)
					{
						while (this.sendingMessage.Count > 0)
						{
							Message m = this.sendingMessage[0];
							Session_ME.doSendMessage(m);
							this.sendingMessage.RemoveAt(0);
						}
					}
					try
					{
						Thread.Sleep(5);
					}
					catch (Exception ex)
					{
						Cout.LogError(ex.ToString());
					}
				}
				catch (Exception)
				{
					Res.outz("error send message! ");
				}
			}
		}

		// Token: 0x04001055 RID: 4181
		public List<Message> sendingMessage;
	}

	// Token: 0x0200009A RID: 154
	private class MessageCollector
	{
		// Token: 0x060008A7 RID: 2215 RVA: 0x000909EC File Offset: 0x0008EBEC
		public void run()
		{
			try
			{
				while (Session_ME.connected)
				{
					Message message = this.readMessage();
					bool flag = message == null;
					if (flag)
					{
						break;
					}
					try
					{
						bool flag2 = message.command == -27;
						if (flag2)
						{
							this.getKey(message);
						}
						else
						{
							Session_ME.onRecieveMsg(message);
						}
					}
					catch (Exception)
					{
						Cout.println("LOI NHAN  MESS THU 1");
					}
					try
					{
						Thread.Sleep(5);
					}
					catch (Exception)
					{
						Cout.println("LOI NHAN  MESS THU 2");
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Log("error read message!");
				Debug.Log(ex.Message.ToString());
			}
			bool flag3 = !Session_ME.connected;
			if (!flag3)
			{
				bool flag4 = Session_ME.messageHandler != null;
				if (flag4)
				{
					bool flag5 = Session_ME.currentTimeMillis() - Session_ME.timeConnected > 500;
					if (flag5)
					{
						Session_ME.messageHandler.onDisconnected(Session_ME.isMainSession);
					}
					else
					{
						Session_ME.messageHandler.onConnectionFail(Session_ME.isMainSession);
					}
				}
				bool flag6 = Session_ME.sc != null;
				if (flag6)
				{
					Session_ME.cleanNetwork();
				}
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00090B34 File Offset: 0x0008ED34
		private void getKey(Message message)
		{
			try
			{
				sbyte b = message.reader().readSByte();
				Session_ME.key = new sbyte[(int)b];
				for (int i = 0; i < (int)b; i++)
				{
					Session_ME.key[i] = message.reader().readSByte();
				}
				for (int j = 0; j < Session_ME.key.Length - 1; j++)
				{
					ref sbyte ptr = ref Session_ME.key[j + 1];
					ptr ^= Session_ME.key[j];
				}
				Session_ME.getKeyComplete = true;
				GameMidlet.IP2 = message.reader().readUTF();
				GameMidlet.PORT2 = message.reader().readInt();
				GameMidlet.isConnect2 = (message.reader().readByte() != 0);
				bool flag = Session_ME.isMainSession && GameMidlet.isConnect2;
				if (flag)
				{
					GameCanvas.connect2();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00090C2C File Offset: 0x0008EE2C
		private Message readMessage2(sbyte cmd)
		{
			int num = (int)Session_ME.readKey(Session_ME.dis.ReadSByte()) + 128;
			int num2 = (int)Session_ME.readKey(Session_ME.dis.ReadSByte()) + 128;
			int num3 = (int)Session_ME.readKey(Session_ME.dis.ReadSByte()) + 128;
			int num4 = (num3 * 256 + num2) * 256 + num;
			Cout.LogError("SIZE = " + num4);
			sbyte[] array = new sbyte[num4];
			byte[] src = Session_ME.dis.ReadBytes(num4);
			Buffer.BlockCopy(src, 0, array, 0, num4);
			Session_ME.recvByteCount += 5 + num4;
			int num5 = Session_ME.recvByteCount + Session_ME.sendByteCount;
			Session_ME.strRecvByteCount = string.Concat(new object[]
			{
				num5 / 1024,
				".",
				num5 % 1024 / 102,
				"Kb"
			});
			bool getKeyComplete = Session_ME.getKeyComplete;
			if (getKeyComplete)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Session_ME.readKey(array[i]);
				}
			}
			return new Message(cmd, array);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00090D6C File Offset: 0x0008EF6C
		private Message readMessage()
		{
			try
			{
				sbyte b = Session_ME.dis.ReadSByte();
				bool getKeyComplete = Session_ME.getKeyComplete;
				if (getKeyComplete)
				{
					b = Session_ME.readKey(b);
				}
				bool flag = b == -32 || b == -66 || b == 11 || b == -67 || b == -74 || b == -87 || b == 66;
				if (flag)
				{
					return this.readMessage2(b);
				}
				bool getKeyComplete2 = Session_ME.getKeyComplete;
				int num;
				if (getKeyComplete2)
				{
					sbyte b2 = Session_ME.dis.ReadSByte();
					sbyte b3 = Session_ME.dis.ReadSByte();
					num = (((int)Session_ME.readKey(b2) & 255) << 8 | ((int)Session_ME.readKey(b3) & 255));
				}
				else
				{
					sbyte b4 = Session_ME.dis.ReadSByte();
					sbyte b5 = Session_ME.dis.ReadSByte();
					num = (((int)b4 & 65280) | ((int)b5 & 255));
				}
				sbyte[] array = new sbyte[num];
				byte[] src = Session_ME.dis.ReadBytes(num);
				Buffer.BlockCopy(src, 0, array, 0, num);
				Session_ME.recvByteCount += 5 + num;
				int num2 = Session_ME.recvByteCount + Session_ME.sendByteCount;
				Session_ME.strRecvByteCount = string.Concat(new object[]
				{
					num2 / 1024,
					".",
					num2 % 1024 / 102,
					"Kb"
				});
				bool getKeyComplete3 = Session_ME.getKeyComplete;
				if (getKeyComplete3)
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Session_ME.readKey(array[i]);
					}
				}
				return new Message(b, array);
			}
			catch (Exception ex)
			{
				Debug.Log(ex.StackTrace.ToString());
			}
			return null;
		}
	}
}
