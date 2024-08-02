using System;

// Token: 0x02000050 RID: 80
public interface ISession
{
	// Token: 0x0600040B RID: 1035
	bool isConnected();

	// Token: 0x0600040C RID: 1036
	void setHandler(IMessageHandler messageHandler);

	// Token: 0x0600040D RID: 1037
	void connect(string host, int port);

	// Token: 0x0600040E RID: 1038
	void sendMessage(Message message);

	// Token: 0x0600040F RID: 1039
	void close();
}
