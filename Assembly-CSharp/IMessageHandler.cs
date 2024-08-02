using System;

// Token: 0x02000041 RID: 65
public interface IMessageHandler
{
	// Token: 0x06000390 RID: 912
	void onMessage(Message message);

	// Token: 0x06000391 RID: 913
	void onConnectionFail(bool isMain);

	// Token: 0x06000392 RID: 914
	void onDisconnected(bool isMain);

	// Token: 0x06000393 RID: 915
	void onConnectOK(bool isMain);
}
