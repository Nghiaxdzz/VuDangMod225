using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

// Token: 0x0200009B RID: 155
public class Session_ME2 : ISession
{
	// Token: 0x060008AC RID: 2220 RVA: 0x00005CE4 File Offset: 0x00003EE4
	public Session_ME2()
	{
		Debug.Log("init Session_ME");
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x00005DAF File Offset: 0x00003FAF
	public void clearSendingMessage()
	{
		Session_ME2.sender.sendingMessage.Clear();
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x00090F44 File Offset: 0x0008F144
	public static Session_ME2 gI()
	{
		bool flag = Session_ME2.instance == null;
		if (flag)
		{
			Session_ME2.instance = new Session_ME2();
		}
		return Session_ME2.instance;
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x00090F74 File Offset: 0x0008F174
	public bool isConnected()
	{
		return Session_ME2.connected;
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x00005DC2 File Offset: 0x00003FC2
	public void setHandler(IMessageHandler msgHandler)
	{
		Session_ME2.messageHandler = msgHandler;
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x00090F8C File Offset: 0x0008F18C
	public void connect(string host, int port)
	{
		bool flag = !Session_ME2.connected && !Session_ME2.connecting;
		if (flag)
		{
			this.host = host;
			this.port = port;
			Session_ME2.getKeyComplete = false;
			Session_ME2.sc = null;
			Debug.Log("connecting...!");
			Debug.Log("host: " + host);
			Debug.Log("port: " + port);
			Session_ME2.initThread = new Thread(new ThreadStart(this.NetworkInit));
			Session_ME2.initThread.Start();
		}
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x00091020 File Offset: 0x0008F220
	private void NetworkInit()
	{
		Session_ME2.isCancel = false;
		Session_ME2.connecting = true;
		Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
		Session_ME2.connected = true;
		try
		{
			this.doConnect(this.host, this.port);
			Session_ME2.messageHandler.onConnectOK(Session_ME2.isMainSession);
		}
		catch (Exception)
		{
			bool flag = Session_ME2.messageHandler != null;
			if (flag)
			{
				this.close();
				Session_ME2.messageHandler.onConnectionFail(Session_ME2.isMainSession);
			}
		}
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x000910B0 File Offset: 0x0008F2B0
	public void doConnect(string host, int port)
	{
		Session_ME2.sc = new TcpClient();
		Session_ME2.sc.Connect(host, port);
		Session_ME2.dataStream = Session_ME2.sc.GetStream();
		Session_ME2.dis = new BinaryReader(Session_ME2.dataStream, new UTF8Encoding());
		Session_ME2.dos = new BinaryWriter(Session_ME2.dataStream, new UTF8Encoding());
		new Thread(new ThreadStart(Session_ME2.sender.run)).Start();
		Session_ME2.MessageCollector @object = new Session_ME2.MessageCollector();
		Cout.LogError("new -----");
		Session_ME2.collectorThread = new Thread(new ThreadStart(@object.run));
		Session_ME2.collectorThread.Start();
		Session_ME2.timeConnected = Session_ME2.currentTimeMillis();
		Session_ME2.connecting = false;
		Session_ME2.doSendMessage(new Message(-27));
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x00005DCB File Offset: 0x00003FCB
	public void sendMessage(Message message)
	{
		Res.outz("SEND MSG: " + message.command);
		Session_ME2.sender.AddMessage(message);
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x00091178 File Offset: 0x0008F378
	private static void doSendMessage(Message m)
	{
		sbyte[] data = m.getData();
		try
		{
			bool flag = Session_ME2.getKeyComplete;
			if (flag)
			{
				sbyte value = Session_ME2.writeKey(m.command);
				Session_ME2.dos.Write(value);
			}
			else
			{
				Session_ME2.dos.Write(m.command);
			}
			bool flag2 = data != null;
			if (flag2)
			{
				int num = data.Length;
				bool flag3 = Session_ME2.getKeyComplete;
				if (flag3)
				{
					int num2 = (int)Session_ME2.writeKey((sbyte)(num >> 8));
					Session_ME2.dos.Write((sbyte)num2);
					int num3 = (int)Session_ME2.writeKey((sbyte)(num & 255));
					Session_ME2.dos.Write((sbyte)num3);
				}
				else
				{
					Session_ME2.dos.Write((ushort)num);
				}
				bool flag4 = Session_ME2.getKeyComplete;
				if (flag4)
				{
					for (int i = 0; i < data.Length; i++)
					{
						sbyte value2 = Session_ME2.writeKey(data[i]);
						Session_ME2.dos.Write(value2);
					}
				}
				Session_ME2.sendByteCount += 5 + data.Length;
			}
			else
			{
				bool flag5 = Session_ME2.getKeyComplete;
				if (flag5)
				{
					int num4 = 0;
					int num5 = (int)Session_ME2.writeKey((sbyte)(num4 >> 8));
					Session_ME2.dos.Write((sbyte)num5);
					int num6 = (int)Session_ME2.writeKey((sbyte)(num4 & 255));
					Session_ME2.dos.Write((sbyte)num6);
				}
				else
				{
					Session_ME2.dos.Write(0);
				}
				Session_ME2.sendByteCount += 5;
			}
			Session_ME2.dos.Flush();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.StackTrace);
		}
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x00091324 File Offset: 0x0008F524
	public static sbyte readKey(sbyte b)
	{
		sbyte[] array = Session_ME2.key;
		sbyte b2 = Session_ME2.curR;
		Session_ME2.curR = b2 + 1;
		sbyte result = (sbyte)(((int)array[(int)b2] & 255) ^ ((int)b & 255));
		bool flag = (int)Session_ME2.curR >= Session_ME2.key.Length;
		if (flag)
		{
			Session_ME2.curR %= (sbyte)Session_ME2.key.Length;
		}
		return result;
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0009138C File Offset: 0x0008F58C
	public static sbyte writeKey(sbyte b)
	{
		sbyte[] array = Session_ME2.key;
		sbyte b2 = Session_ME2.curW;
		Session_ME2.curW = b2 + 1;
		sbyte result = (sbyte)(((int)array[(int)b2] & 255) ^ ((int)b & 255));
		bool flag = (int)Session_ME2.curW >= Session_ME2.key.Length;
		if (flag)
		{
			Session_ME2.curW %= (sbyte)Session_ME2.key.Length;
		}
		return result;
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x000913F4 File Offset: 0x0008F5F4
	public static void onRecieveMsg(Message msg)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		if (flag)
		{
			Session_ME2.messageHandler.onMessage(msg);
		}
		else
		{
			Session_ME2.recieveMsg.addElement(msg);
		}
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x00091438 File Offset: 0x0008F638
	public static void update()
	{
		while (Session_ME2.recieveMsg.size() > 0)
		{
			Message message = (Message)Session_ME2.recieveMsg.elementAt(0);
			bool isStopReadMessage = Controller.isStopReadMessage;
			if (isStopReadMessage)
			{
				break;
			}
			bool flag = message == null;
			if (flag)
			{
				Session_ME2.recieveMsg.removeElementAt(0);
				break;
			}
			Session_ME2.messageHandler.onMessage(message);
			Session_ME2.recieveMsg.removeElementAt(0);
		}
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x00005DF5 File Offset: 0x00003FF5
	public void close()
	{
		Session_ME2.cleanNetwork();
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x000914A8 File Offset: 0x0008F6A8
	private static void cleanNetwork()
	{
		Session_ME2.key = null;
		Session_ME2.curR = 0;
		Session_ME2.curW = 0;
		try
		{
			Session_ME2.connected = false;
			Session_ME2.connecting = false;
			bool flag = Session_ME2.sc != null;
			if (flag)
			{
				Session_ME2.sc.Close();
				Session_ME2.sc = null;
			}
			bool flag2 = Session_ME2.dataStream != null;
			if (flag2)
			{
				Session_ME2.dataStream.Close();
				Session_ME2.dataStream = null;
			}
			bool flag3 = Session_ME2.dos != null;
			if (flag3)
			{
				Session_ME2.dos.Close();
				Session_ME2.dos = null;
			}
			bool flag4 = Session_ME2.dis != null;
			if (flag4)
			{
				Session_ME2.dis.Close();
				Session_ME2.dis = null;
			}
			Session_ME2.sendThread = null;
			Session_ME2.collectorThread = null;
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x00090928 File Offset: 0x0008EB28
	public static int currentTimeMillis()
	{
		return Environment.TickCount;
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x00050910 File Offset: 0x0004EB10
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

	// Token: 0x060008BE RID: 2238 RVA: 0x00050938 File Offset: 0x0004EB38
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

	// Token: 0x04001056 RID: 4182
	protected static Session_ME2 instance = new Session_ME2();

	// Token: 0x04001057 RID: 4183
	private static NetworkStream dataStream;

	// Token: 0x04001058 RID: 4184
	private static BinaryReader dis;

	// Token: 0x04001059 RID: 4185
	private static BinaryWriter dos;

	// Token: 0x0400105A RID: 4186
	public static IMessageHandler messageHandler;

	// Token: 0x0400105B RID: 4187
	public static bool isMainSession = true;

	// Token: 0x0400105C RID: 4188
	private static TcpClient sc;

	// Token: 0x0400105D RID: 4189
	public static bool connected;

	// Token: 0x0400105E RID: 4190
	public static bool connecting;

	// Token: 0x0400105F RID: 4191
	private static Session_ME2.Sender sender = new Session_ME2.Sender();

	// Token: 0x04001060 RID: 4192
	public static Thread initThread;

	// Token: 0x04001061 RID: 4193
	public static Thread collectorThread;

	// Token: 0x04001062 RID: 4194
	public static Thread sendThread;

	// Token: 0x04001063 RID: 4195
	public static int sendByteCount;

	// Token: 0x04001064 RID: 4196
	public static int recvByteCount;

	// Token: 0x04001065 RID: 4197
	private static bool getKeyComplete;

	// Token: 0x04001066 RID: 4198
	public static sbyte[] key = null;

	// Token: 0x04001067 RID: 4199
	private static sbyte curR;

	// Token: 0x04001068 RID: 4200
	private static sbyte curW;

	// Token: 0x04001069 RID: 4201
	private static int timeConnected;

	// Token: 0x0400106A RID: 4202
	private long lastTimeConn;

	// Token: 0x0400106B RID: 4203
	public static string strRecvByteCount = string.Empty;

	// Token: 0x0400106C RID: 4204
	public static bool isCancel;

	// Token: 0x0400106D RID: 4205
	private string host;

	// Token: 0x0400106E RID: 4206
	private int port;

	// Token: 0x0400106F RID: 4207
	public static MyVector recieveMsg = new MyVector();

	// Token: 0x0200009C RID: 156
	public class Sender
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x00005E34 File Offset: 0x00004034
		public Sender()
		{
			this.sendingMessage = new List<Message>();
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00005E49 File Offset: 0x00004049
		public void AddMessage(Message message)
		{
			this.sendingMessage.Add(message);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00091578 File Offset: 0x0008F778
		public void run()
		{
			while (Session_ME2.connected)
			{
				try
				{
					bool getKeyComplete = Session_ME2.getKeyComplete;
					if (getKeyComplete)
					{
						while (this.sendingMessage.Count > 0)
						{
							Message m = this.sendingMessage[0];
							Session_ME2.doSendMessage(m);
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

		// Token: 0x04001070 RID: 4208
		public List<Message> sendingMessage;
	}

	// Token: 0x0200009D RID: 157
	private class MessageCollector
	{
		// Token: 0x060008C3 RID: 2243 RVA: 0x00091624 File Offset: 0x0008F824
		public void run()
		{
			try
			{
				while (Session_ME2.connected)
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
							Session_ME2.onRecieveMsg(message);
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
			bool flag3 = !Session_ME2.connected;
			if (!flag3)
			{
				bool flag4 = Session_ME2.messageHandler != null;
				if (flag4)
				{
					bool flag5 = Session_ME2.currentTimeMillis() - Session_ME2.timeConnected > 500;
					if (flag5)
					{
						Session_ME2.messageHandler.onDisconnected(Session_ME2.isMainSession);
					}
					else
					{
						Session_ME2.messageHandler.onConnectionFail(Session_ME2.isMainSession);
					}
				}
				bool flag6 = Session_ME2.sc != null;
				if (flag6)
				{
					Session_ME2.cleanNetwork();
				}
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0009176C File Offset: 0x0008F96C
		private void getKey(Message message)
		{
			try
			{
				sbyte b = message.reader().readSByte();
				Session_ME2.key = new sbyte[(int)b];
				for (int i = 0; i < (int)b; i++)
				{
					Session_ME2.key[i] = message.reader().readSByte();
				}
				for (int j = 0; j < Session_ME2.key.Length - 1; j++)
				{
					ref sbyte ptr = ref Session_ME2.key[j + 1];
					ptr ^= Session_ME2.key[j];
				}
				Session_ME2.getKeyComplete = true;
				GameMidlet.IP2 = message.reader().readUTF();
				GameMidlet.PORT2 = message.reader().readInt();
				GameMidlet.isConnect2 = (message.reader().readByte() != 0);
				bool flag = Session_ME2.isMainSession && GameMidlet.isConnect2;
				if (flag)
				{
					GameCanvas.connect2();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00091864 File Offset: 0x0008FA64
		private Message readMessage2(sbyte cmd)
		{
			int num = (int)Session_ME2.readKey(Session_ME2.dis.ReadSByte()) + 128;
			int num2 = (int)Session_ME2.readKey(Session_ME2.dis.ReadSByte()) + 128;
			int num3 = (int)Session_ME2.readKey(Session_ME2.dis.ReadSByte()) + 128;
			int num4 = (num3 * 256 + num2) * 256 + num;
			Cout.LogError("SIZE = " + num4);
			sbyte[] array = new sbyte[num4];
			byte[] src = Session_ME2.dis.ReadBytes(num4);
			Buffer.BlockCopy(src, 0, array, 0, num4);
			Session_ME2.recvByteCount += 5 + num4;
			int num5 = Session_ME2.recvByteCount + Session_ME2.sendByteCount;
			Session_ME2.strRecvByteCount = string.Concat(new object[]
			{
				num5 / 1024,
				".",
				num5 % 1024 / 102,
				"Kb"
			});
			bool getKeyComplete = Session_ME2.getKeyComplete;
			if (getKeyComplete)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Session_ME2.readKey(array[i]);
				}
			}
			return new Message(cmd, array);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x000919A4 File Offset: 0x0008FBA4
		private Message readMessage()
		{
			try
			{
				sbyte b = Session_ME2.dis.ReadSByte();
				bool getKeyComplete = Session_ME2.getKeyComplete;
				if (getKeyComplete)
				{
					b = Session_ME2.readKey(b);
				}
				bool flag = b == -32 || b == -66 || b == 11 || b == -67 || b == -74 || b == -87;
				if (flag)
				{
					return this.readMessage2(b);
				}
				bool getKeyComplete2 = Session_ME2.getKeyComplete;
				int num;
				if (getKeyComplete2)
				{
					sbyte b2 = Session_ME2.dis.ReadSByte();
					sbyte b3 = Session_ME2.dis.ReadSByte();
					num = (((int)Session_ME2.readKey(b2) & 255) << 8 | ((int)Session_ME2.readKey(b3) & 255));
				}
				else
				{
					sbyte b4 = Session_ME2.dis.ReadSByte();
					sbyte b5 = Session_ME2.dis.ReadSByte();
					num = (((int)b4 & 65280) | ((int)b5 & 255));
				}
				sbyte[] array = new sbyte[num];
				byte[] src = Session_ME2.dis.ReadBytes(num);
				Buffer.BlockCopy(src, 0, array, 0, num);
				Session_ME2.recvByteCount += 5 + num;
				int num2 = Session_ME2.recvByteCount + Session_ME2.sendByteCount;
				Session_ME2.strRecvByteCount = string.Concat(new object[]
				{
					num2 / 1024,
					".",
					num2 % 1024 / 102,
					"Kb"
				});
				bool getKeyComplete3 = Session_ME2.getKeyComplete;
				if (getKeyComplete3)
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Session_ME2.readKey(array[i]);
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
