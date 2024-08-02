using System;

// Token: 0x0200003C RID: 60
public interface IChatable
{
	// Token: 0x06000364 RID: 868
	void onChatFromMe(string text, string to);

	// Token: 0x06000365 RID: 869
	void onCancelChat();
}
