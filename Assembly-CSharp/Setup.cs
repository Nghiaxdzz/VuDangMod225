using System;

// Token: 0x0200009E RID: 158
internal class Setup : mScreen, IActionListener, IChatable
{
	// Token: 0x060008C8 RID: 2248 RVA: 0x00091B78 File Offset: 0x0008FD78
	public static Setup gI()
	{
		bool flag = Setup.instance == null;
		Setup result;
		if (flag)
		{
			result = (Setup.instance = new Setup());
		}
		else
		{
			result = Setup.instance;
		}
		return result;
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x00091BAC File Offset: 0x0008FDAC
	public void onChatFromMe(string text, string to)
	{
		string strChat = ChatTextField.gI().strChat;
		ChatTextField.gI().isShow = false;
		ChatTextField.gI().tfChat.setIputType(0);
		ChatTextField.gI().parentScreen = GameScr.gI();
		ChatTextField.gI().strChat = "Chat ";
		ChatTextField.gI().tfChat.name = "chat ";
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00091C14 File Offset: 0x0008FE14
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 1:
		{
			string[] array = (string[])p;
			ChatTextField.gI().VuDangStartChat(Setup.gI(), array[0], array[1]);
			break;
		}
		case 2:
		{
			string[] array2 = (string[])p;
			ChatTextField.gI().VuDangStartChat(Setup.gI(), array2[0], array2[1]);
			break;
		}
		case 3:
		{
			string[] array3 = (string[])p;
			ChatTextField.gI().VuDangStartChat(Setup.gI(), array3[0], array3[1]);
			break;
		}
		case 4:
		{
			string[] array4 = (string[])p;
			ChatTextField.gI().VuDangStartChat(Setup.gI(), array4[0], array4[1]);
			break;
		}
		case 5:
		{
			VuDang.itemBuy = GameCanvas.panel.currItem;
			string[] array5 = (string[])p;
			GameCanvas.panel.VuDangChatTextField(array5[0], array5[1]);
			break;
		}
		case 6:
		{
			string[] array6 = (string[])p;
			ChatTextField.gI().VuDangStartChat(Setup.gI(), array6[0], array6[1]);
			break;
		}
		case 7:
		{
			string[] array7 = (string[])p;
			GameCanvas.panel.VuDangChatTextField(array7[0], array7[1]);
			break;
		}
		}
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00005E59 File Offset: 0x00004059
	public void onCancelChat()
	{
		ChatTextField.gI().center = null;
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x00005E67 File Offset: 0x00004067
	public override void paint(mGraphics g)
	{
		ChatTextField.gI().paint(g);
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x00005E76 File Offset: 0x00004076
	public override void update()
	{
		ChatTextField.gI().update();
	}

	// Token: 0x04001071 RID: 4209
	public static Setup instance;
}
