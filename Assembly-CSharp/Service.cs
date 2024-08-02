using System;
using AssemblyCSharp.Mod.PickMob;
using AssemblyCSharp.Mod.Xmap;
using Assets.src.g;

// Token: 0x02000097 RID: 151
public class Service
{
	// Token: 0x060007F9 RID: 2041 RVA: 0x0008AEC0 File Offset: 0x000890C0
	public static Service gI()
	{
		bool flag = Service.instance == null;
		if (flag)
		{
			Service.instance = new Service();
		}
		return Service.instance;
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x0008AEF0 File Offset: 0x000890F0
	public void gotoPlayer(int id)
	{
		Message message = null;
		try
		{
			message = new Message(18);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x0008AF60 File Offset: 0x00089160
	public void androidPack()
	{
		bool flag = mSystem.android_pack == null;
		if (!flag)
		{
			Message message = null;
			try
			{
				message = new Message(126);
				message.writer().writeUTF(mSystem.android_pack);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x0008AFE4 File Offset: 0x000891E4
	public void charInfo(string day, string month, string year, string address, string cmnd, string dayCmnd, string noiCapCmnd, string sdt, string name)
	{
		Message message = null;
		try
		{
			message = new Message(42);
			message.writer().writeUTF(day);
			message.writer().writeUTF(month);
			message.writer().writeUTF(year);
			message.writer().writeUTF(address);
			message.writer().writeUTF(cmnd);
			message.writer().writeUTF(dayCmnd);
			message.writer().writeUTF(noiCapCmnd);
			message.writer().writeUTF(sdt);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x0008B0C4 File Offset: 0x000892C4
	public void androidPack2()
	{
		bool flag = mSystem.android_pack == null;
		if (!flag)
		{
			Message message = null;
			try
			{
				message = new Message(126);
				message.writer().writeUTF(mSystem.android_pack);
				bool flag2 = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
				if (flag2)
				{
					this.session = Session_ME2.gI();
				}
				else
				{
					this.session = Session_ME.gI();
				}
				this.session.sendMessage(message);
				this.session = Session_ME.gI();
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x0008B18C File Offset: 0x0008938C
	public void checkAd(sbyte status)
	{
		Message message = null;
		try
		{
			message = new Message(-44);
			message.writer().writeByte(status);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x0008B1FC File Offset: 0x000893FC
	public void combine(sbyte action, MyVector id)
	{
		Res.outz("combine");
		Message message = null;
		try
		{
			message = new Message(-81);
			message.writer().writeByte(action);
			bool flag = action == 1;
			if (flag)
			{
				message.writer().writeByte(id.size());
				for (int i = 0; i < id.size(); i++)
				{
					message.writer().writeByte(((Item)id.elementAt(i)).indexUI);
					Res.outz("gui id " + ((Item)id.elementAt(i)).indexUI);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x0008B2E0 File Offset: 0x000894E0
	public void giaodich(sbyte action, int playerID, sbyte index, int num)
	{
		Res.outz2("giao dich action = " + action);
		Message message = null;
		try
		{
			message = new Message(-86);
			message.writer().writeByte(action);
			bool flag = action == 0 || action == 1;
			if (flag)
			{
				Res.outz2(">>>> len playerID =" + playerID);
				message.writer().writeInt(playerID);
			}
			bool flag2 = action == 2;
			if (flag2)
			{
				Res.outz2(string.Concat(new object[]
				{
					"gui len index =",
					index,
					" num= ",
					num
				}));
				message.writer().writeByte(index);
				message.writer().writeInt(num);
			}
			bool flag3 = action == 4;
			if (flag3)
			{
				Res.outz2(">>>> len index =" + index);
				message.writer().writeByte(index);
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x0008B414 File Offset: 0x00089614
	public void sendClientInput(TField[] t)
	{
		Message message = null;
		try
		{
			Res.outz(" gui input ");
			message = new Message(-125);
			Res.outz("byte lent = " + t.Length);
			message.writer().writeByte(t.Length);
			for (int i = 0; i < t.Length; i++)
			{
				message.writer().writeUTF(t[i].getText());
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x0008B4C4 File Offset: 0x000896C4
	public void speacialSkill(sbyte index)
	{
		Message message = null;
		try
		{
			message = new Message(112);
			message.writer().writeByte(index);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x0008B534 File Offset: 0x00089734
	public void test(short x, short y)
	{
		Res.outz(string.Concat(new object[]
		{
			"gui x= ",
			x,
			" y= ",
			y
		}));
		Message message = null;
		try
		{
			message = new Message(0);
			message.writer().writeShort(x);
			message.writer().writeShort(y);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x0008B5E4 File Offset: 0x000897E4
	public void test2()
	{
		Res.outz("gui test1");
		Message message = null;
		try
		{
			message = new Message(1);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void testJoint()
	{
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x0008B650 File Offset: 0x00089850
	public void mobCapcha(char ch)
	{
		Res.outz("cap char c= " + ch.ToString());
		Message message = null;
		try
		{
			message = new Message(-85);
			message.writer().writeChar(ch);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x0008B6CC File Offset: 0x000898CC
	public void friend(sbyte action, int playerId)
	{
		Res.outz("add friend");
		Message message = null;
		try
		{
			message = new Message(-80);
			message.writer().writeByte(action);
			bool flag = playerId != -1;
			if (flag)
			{
				message.writer().writeInt(playerId);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x0008B76C File Offset: 0x0008996C
	public void getArchivemnt(int index)
	{
		Res.outz("get ngoc");
		Message message = null;
		try
		{
			message = new Message(-76);
			message.writer().writeByte(index);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x0008B7F4 File Offset: 0x000899F4
	public void getPlayerMenu(int playerID)
	{
		Message message = null;
		try
		{
			message = new Message(-79);
			message.writer().writeInt(playerID);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x0008B858 File Offset: 0x00089A58
	public void clanImage(sbyte id)
	{
		Message message = null;
		try
		{
			message = new Message(-62);
			message.writer().writeByte(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x0008B8D4 File Offset: 0x00089AD4
	public void skill_not_focus(sbyte status)
	{
		Message message = null;
		try
		{
			message = new Message(-45);
			message.writer().writeByte(status);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x0008B950 File Offset: 0x00089B50
	public void clanDonate(int id)
	{
		Message message = null;
		try
		{
			message = new Message(-54);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x0008B9CC File Offset: 0x00089BCC
	public void clanMessage(int type, string text, int clanID)
	{
		Message message = null;
		try
		{
			message = new Message(-51);
			message.writer().writeByte(type);
			bool flag = type == 0;
			if (flag)
			{
				message.writer().writeUTF(text);
			}
			bool flag2 = type == 2;
			if (flag2)
			{
				message.writer().writeInt(clanID);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x0008BA74 File Offset: 0x00089C74
	public void useItem(sbyte type, sbyte where, sbyte index, short template)
	{
		Cout.println("USE ITEM! " + type);
		bool flag = global::Char.myCharz().statusMe == 14;
		if (!flag)
		{
			Message message = null;
			try
			{
				message = new Message(-43);
				message.writer().writeByte(type);
				message.writer().writeByte(where);
				message.writer().writeByte(index);
				bool flag2 = index == -1;
				if (flag2)
				{
					message.writer().writeShort(template);
				}
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x0008BB34 File Offset: 0x00089D34
	public void joinClan(int id, sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-49);
			message.writer().writeInt(id);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x0008BBBC File Offset: 0x00089DBC
	public void clanMember(int id)
	{
		Message message = null;
		try
		{
			message = new Message(-50);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x0008BC38 File Offset: 0x00089E38
	public void searchClan(string text)
	{
		Message message = null;
		try
		{
			message = new Message(-47);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x0008BCB4 File Offset: 0x00089EB4
	public void requestClan(short id)
	{
		Message message = null;
		try
		{
			message = new Message(-53);
			message.writer().writeShort(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x0008BD30 File Offset: 0x00089F30
	public void clanRemote(int id, sbyte role)
	{
		Message message = null;
		try
		{
			message = new Message(-56);
			message.writer().writeInt(id);
			message.writer().writeByte(role);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x0008BDB8 File Offset: 0x00089FB8
	public void leaveClan()
	{
		Message message = null;
		try
		{
			message = new Message(-55);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x0008BE28 File Offset: 0x0008A028
	public void clanInvite(sbyte action, int playerID, int clanID, int code)
	{
		Message message = null;
		try
		{
			message = new Message(-57);
			message.writer().writeByte(action);
			bool flag = action == 0;
			if (flag)
			{
				message.writer().writeInt(playerID);
			}
			bool flag2 = action == 1 || action == 2;
			if (flag2)
			{
				message.writer().writeInt(clanID);
				message.writer().writeInt(code);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x0008BEE8 File Offset: 0x0008A0E8
	public void getClan(sbyte action, sbyte id, string text)
	{
		Message message = null;
		try
		{
			message = new Message(-46);
			message.writer().writeByte(action);
			bool flag = action == 2 || action == 4;
			if (flag)
			{
				message.writer().writeByte(id);
				message.writer().writeUTF(text);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x0008BF90 File Offset: 0x0008A190
	public void updateCaption(sbyte gender)
	{
		Message message = null;
		try
		{
			message = new Message(-41);
			message.writer().writeByte(gender);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x0008C00C File Offset: 0x0008A20C
	public void getItem(sbyte type, sbyte id)
	{
		Message message = null;
		try
		{
			message = new Message(-40);
			message.writer().writeByte(type);
			message.writer().writeByte(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x0008C094 File Offset: 0x0008A294
	public void getTask(int npcTemplateId, int menuId, int optionId)
	{
		Message message = null;
		try
		{
			message = new Message(40);
			message.writer().writeByte(npcTemplateId);
			message.writer().writeByte(menuId);
			bool flag = optionId >= 0;
			if (flag)
			{
				message.writer().writeByte(optionId);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x0008C138 File Offset: 0x0008A338
	public Message messageNotLogin(sbyte command)
	{
		Message message = new Message(-29);
		message.writer().writeByte(command);
		return message;
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x0008C160 File Offset: 0x0008A360
	public Message messageNotMap(sbyte command)
	{
		Message message = new Message(-28);
		message.writer().writeByte(command);
		return message;
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x0008C188 File Offset: 0x0008A388
	public static Message messageSubCommand(sbyte command)
	{
		Message message = new Message(-30);
		message.writer().writeByte(command);
		return message;
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x0008C1B0 File Offset: 0x0008A3B0
	public void setClientType()
	{
		bool flag = Rms.loadRMSInt("clienttype") != -1;
		if (flag)
		{
			Main.typeClient = Rms.loadRMSInt("clienttype");
		}
		try
		{
			Message message = this.messageNotLogin(2);
			message.writer().writeByte(Main.typeClient);
			message.writer().writeByte(mGraphics.zoomLevel);
			message.writer().writeBoolean(false);
			message.writer().writeInt(GameCanvas.w);
			message.writer().writeInt(GameCanvas.h);
			message.writer().writeBoolean(TField.isQwerty);
			message.writer().writeBoolean(GameCanvas.isTouch);
			message.writer().writeUTF(GameCanvas.getPlatformName() + "|" + GameMidlet.VERSION);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x0008C2C4 File Offset: 0x0008A4C4
	public void setClientType2()
	{
		Res.outz("SET CLIENT TYPE");
		bool flag = Rms.loadRMSInt("clienttype") != -1;
		if (flag)
		{
			mSystem.clientType = Rms.loadRMSInt("clienttype");
		}
		try
		{
			Res.outz("setType");
			Message message = this.messageNotLogin(2);
			message.writer().writeByte(mSystem.clientType);
			message.writer().writeByte(mGraphics.zoomLevel);
			Res.outz("gui zoomlevel = " + mGraphics.zoomLevel);
			message.writer().writeBoolean(false);
			message.writer().writeInt(GameCanvas.w);
			message.writer().writeInt(GameCanvas.h);
			message.writer().writeBoolean(TField.isQwerty);
			message.writer().writeBoolean(GameCanvas.isTouch);
			message.writer().writeUTF(GameCanvas.getPlatformName() + "|" + GameMidlet.VERSION);
			this.session = Session_ME2.gI();
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
			message.cleanup();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x0008C414 File Offset: 0x0008A614
	public void sendCheckController()
	{
		Message message = null;
		try
		{
			message = new Message(-120);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			Service.curCheckController = mSystem.currentTimeMillis();
			message.cleanup();
		}
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x0008C474 File Offset: 0x0008A674
	public void sendCheckMap()
	{
		Message message = null;
		try
		{
			message = new Message(-121);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			Service.curCheckMap = mSystem.currentTimeMillis();
			message.cleanup();
		}
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x0008C4D4 File Offset: 0x0008A6D4
	public void login(string username, string pass, string version, sbyte type)
	{
		try
		{
			Message message = this.messageNotLogin(0);
			message.writer().writeUTF(username);
			message.writer().writeUTF(pass);
			message.writer().writeUTF(version);
			message.writer().writeByte(type);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x0008C564 File Offset: 0x0008A764
	public void requestRegister(string username, string pass, string usernameAo, string passAo, string version)
	{
		try
		{
			Message message = this.messageNotLogin(1);
			message.writer().writeUTF(username);
			message.writer().writeUTF(pass);
			bool flag = usernameAo != null && !usernameAo.Equals(string.Empty);
			if (flag)
			{
				message.writer().writeUTF(usernameAo);
				message.writer().writeUTF("a");
			}
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x0008C610 File Offset: 0x0008A810
	public void requestChangeMap()
	{
		Message message = new Message(-23);
		this.session.sendMessage(message);
		message.cleanup();
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x0008C63C File Offset: 0x0008A83C
	public void magicTree(sbyte type)
	{
		Message message = new Message(-34);
		try
		{
			message.writer().writeByte(type);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x0008C68C File Offset: 0x0008A88C
	public void requestChangeZone(int zoneId, int indexUI)
	{
		bool flag = !VuDang.khoakhu && mSystem.currentTimeMillis() - VuDang.canChangeZone >= 500L;
		if (flag)
		{
			VuDang.canChangeZone = mSystem.currentTimeMillis();
			Message message = new Message(21);
			try
			{
				message.writer().writeByte(zoneId);
				this.session.sendMessage(message);
				message.cleanup();
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x0008C70C File Offset: 0x0008A90C
	public void checkMMove(int second)
	{
		Message message = new Message(-78);
		try
		{
			message.writer().writeInt(second);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x0008C75C File Offset: 0x0008A95C
	public void charMove()
	{
		int num = global::Char.myCharz().cx - global::Char.myCharz().cxSend;
		int num2 = global::Char.myCharz().cy - global::Char.myCharz().cySend;
		bool flag = global::Char.ischangingMap || (num == 0 && num2 == 0) || Controller.isStopReadMessage || global::Char.myCharz().isTeleport || global::Char.myCharz().cy <= 0 || global::Char.myCharz().telePortSkill;
		if (!flag)
		{
			try
			{
				Message message = new Message(-7);
				global::Char.myCharz().cxSend = global::Char.myCharz().cx;
				global::Char.myCharz().cySend = global::Char.myCharz().cy;
				global::Char.myCharz().cdirSend = global::Char.myCharz().cdir;
				global::Char.myCharz().cactFirst = global::Char.myCharz().statusMe;
				bool flag2 = TileMap.tileTypeAt(global::Char.myCharz().cx / (int)TileMap.size, global::Char.myCharz().cy / (int)TileMap.size) == 0;
				if (flag2)
				{
					message.writer().writeByte(1);
					bool canFly = global::Char.myCharz().canFly;
					if (canFly)
					{
						bool flag3 = !global::Char.myCharz().isHaveMount;
						if (flag3)
						{
							global::Char.myCharz().cMP -= global::Char.myCharz().cMPGoc / 100 * ((global::Char.myCharz().isMonkey != 1) ? 1 : 2);
						}
						bool flag4 = global::Char.myCharz().cMP < 0;
						if (flag4)
						{
							global::Char.myCharz().cMP = 0;
						}
						GameScr.gI().isInjureMp = true;
						GameScr.gI().twMp = 0;
					}
				}
				else
				{
					message.writer().writeByte(0);
				}
				message.writer().writeShort(global::Char.myCharz().cx);
				bool flag5 = num2 != 0;
				if (flag5)
				{
					message.writer().writeShort(global::Char.myCharz().cy);
				}
				this.session.sendMessage(message);
				GameScr.tickMove++;
				message.cleanup();
			}
			catch (Exception ex)
			{
				Cout.LogError("LOI CHAR MOVE " + ex.ToString());
			}
		}
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x0008C9A8 File Offset: 0x0008ABA8
	public void selectCharToPlay(string charname)
	{
		Message message = new Message(-28);
		try
		{
			message.writer().writeByte(1);
			message.writer().writeUTF(charname);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		this.session.sendMessage(message);
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void selectZone(sbyte sub, int value)
	{
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x0008CA18 File Offset: 0x0008AC18
	public void createChar(string name, int gender, int hair)
	{
		Message message = new Message(-28);
		try
		{
			message.writer().writeByte(2);
			message.writer().writeUTF(name);
			message.writer().writeByte(gender);
			message.writer().writeByte(hair);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		this.session.sendMessage(message);
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x0008CAA0 File Offset: 0x0008ACA0
	public void requestModTemplate(int modTemplateId)
	{
		Message message = null;
		try
		{
			message = new Message(11);
			message.writer().writeByte(modTemplateId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x0008CB1C File Offset: 0x0008AD1C
	public void requestNpcTemplate(int npcTemplateId)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(12);
			message.writer().writeByte(npcTemplateId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x0008CB98 File Offset: 0x0008AD98
	public void requestSkill(int skillId)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(9);
			message.writer().writeShort(skillId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x0008CC14 File Offset: 0x0008AE14
	public void requestItemInfo(int typeUI, int indexUI)
	{
		Message message = null;
		try
		{
			message = new Message(35);
			message.writer().writeByte(typeUI);
			message.writer().writeByte(indexUI);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x0008CC9C File Offset: 0x0008AE9C
	public void requestItemPlayer(int charId, int indexUI)
	{
		Message message = null;
		try
		{
			message = new Message(90);
			message.writer().writeInt(charId);
			message.writer().writeByte(indexUI);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x0008CD24 File Offset: 0x0008AF24
	public void upSkill(int skillTemplateId, int point)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(17);
			message.writer().writeShort(skillTemplateId);
			message.writer().writeByte(point);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x0008CDAC File Offset: 0x0008AFAC
	public void saleItem(sbyte action, sbyte type, short id)
	{
		Message message = null;
		try
		{
			message = new Message(7);
			message.writer().writeByte(action);
			message.writer().writeByte(type);
			message.writer().writeShort(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x0008CE40 File Offset: 0x0008B040
	public void buyItem(sbyte type, int id, int quantity)
	{
		Message message = null;
		try
		{
			message = new Message(6);
			message.writer().writeByte(type);
			message.writer().writeShort(id);
			bool flag = quantity > 1;
			if (flag)
			{
				message.writer().writeShort(quantity);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x0008CEE0 File Offset: 0x0008B0E0
	public void selectSkill(int skillTemplateId)
	{
		Cout.println(global::Char.myCharz().cName + " SELECT SKILL " + skillTemplateId);
		Message message = null;
		try
		{
			message = new Message(34);
			message.writer().writeShort(skillTemplateId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x0008CF7C File Offset: 0x0008B17C
	public void getEffData(short id)
	{
		Message message = null;
		try
		{
			message = new Message(-66);
			message.writer().writeShort(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0008CFF8 File Offset: 0x0008B1F8
	public void openUIZone()
	{
		Message message = null;
		try
		{
			message = new Message(29);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0008D068 File Offset: 0x0008B268
	public void confirmMenu(short npcID, sbyte select)
	{
		Res.outz("confirme menu" + select);
		Message message = null;
		try
		{
			message = new Message(32);
			message.writer().writeShort(npcID);
			message.writer().writeByte(select);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x0008D108 File Offset: 0x0008B308
	public void openMenu(int npcId)
	{
		Message message = null;
		try
		{
			message = new Message(33);
			message.writer().writeShort(npcId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x0008D184 File Offset: 0x0008B384
	public void menu(int npcId, int menuId, int optionId)
	{
		Cout.println("menuid: " + menuId);
		Message message = null;
		try
		{
			message = new Message(22);
			message.writer().writeByte(npcId);
			message.writer().writeByte(menuId);
			message.writer().writeByte(optionId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x0008D230 File Offset: 0x0008B430
	public void menuId(short menuId)
	{
		Message message = null;
		try
		{
			message = new Message(27);
			message.writer().writeShort(menuId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x0008D2AC File Offset: 0x0008B4AC
	public void textBoxId(short menuId, string str)
	{
		Message message = null;
		try
		{
			message = new Message(88);
			message.writer().writeShort(menuId);
			message.writer().writeUTF(str);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x0008D334 File Offset: 0x0008B534
	public void requestItem(int typeUI)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(22);
			message.writer().writeByte(typeUI);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x0008D3B0 File Offset: 0x0008B5B0
	public void boxSort()
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(19);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x0008D420 File Offset: 0x0008B620
	public void boxCoinOut(int coinOut)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(21);
			message.writer().writeInt(coinOut);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x0008D49C File Offset: 0x0008B69C
	public void upgradeItem(Item item, Item[] items, bool isGold)
	{
		GameCanvas.msgdlg.pleasewait();
		Message message = null;
		try
		{
			message = new Message(14);
			message.writer().writeBoolean(isGold);
			message.writer().writeByte(item.indexUI);
			for (int i = 0; i < items.Length; i++)
			{
				bool flag = items[i] != null;
				if (flag)
				{
					message.writer().writeByte(items[i].indexUI);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x0008D56C File Offset: 0x0008B76C
	public void crystalCollectLock(Item[] items)
	{
		GameCanvas.msgdlg.pleasewait();
		Message message = null;
		try
		{
			message = new Message(13);
			for (int i = 0; i < items.Length; i++)
			{
				bool flag = items[i] != null;
				if (flag)
				{
					message.writer().writeByte(items[i].indexUI);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x0008D61C File Offset: 0x0008B81C
	public void acceptInviteTrade(int playerMapId)
	{
		Message message = null;
		try
		{
			message = new Message(37);
			message.writer().writeInt(playerMapId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x0008D698 File Offset: 0x0008B898
	public void cancelInviteTrade()
	{
		Message message = null;
		try
		{
			message = new Message(50);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x0008D708 File Offset: 0x0008B908
	public void tradeAccept()
	{
		Message message = null;
		try
		{
			message = new Message(39);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x0008D778 File Offset: 0x0008B978
	public void tradeItemLock(int coin, Item[] items)
	{
		Message message = null;
		try
		{
			message = new Message(38);
			message.writer().writeInt(coin);
			int num = 0;
			for (int i = 0; i < items.Length; i++)
			{
				bool flag = items[i] != null;
				if (flag)
				{
					num++;
				}
			}
			message.writer().writeByte(num);
			for (int j = 0; j < items.Length; j++)
			{
				bool flag2 = items[j] != null;
				if (flag2)
				{
					message.writer().writeByte(items[j].indexUI);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x0008D868 File Offset: 0x0008BA68
	public void sendPlayerAttack(MyVector vMob, MyVector vChar, int type)
	{
		try
		{
			Message message = null;
			bool flag = type == 0;
			if (!flag)
			{
				bool flag2 = vMob.size() > 0 && vChar.size() > 0;
				if (flag2)
				{
					if (type != 1)
					{
						if (type == 2)
						{
							message = new Message(67);
						}
					}
					else
					{
						message = new Message(-4);
					}
					message.writer().writeByte(vMob.size());
					for (int i = 0; i < vMob.size(); i++)
					{
						Mob mob = (Mob)vMob.elementAt(i);
						message.writer().writeByte(mob.mobId);
					}
					for (int j = 0; j < vChar.size(); j++)
					{
						global::Char @char = (global::Char)vChar.elementAt(j);
						bool flag3 = @char != null;
						if (flag3)
						{
							message.writer().writeInt(@char.charID);
						}
						else
						{
							message.writer().writeInt(-1);
						}
					}
				}
				else
				{
					bool flag4 = vMob.size() > 0;
					if (flag4)
					{
						message = new Message(54);
						for (int k = 0; k < vMob.size(); k++)
						{
							Mob mob2 = (Mob)vMob.elementAt(k);
							bool flag5 = !mob2.isMobMe;
							if (flag5)
							{
								message.writer().writeByte(mob2.mobId);
							}
							else
							{
								message.writer().writeByte(-1);
								message.writer().writeInt(mob2.mobId);
							}
						}
					}
					else
					{
						bool flag6 = vChar.size() > 0;
						if (flag6)
						{
							message = new Message(-60);
							for (int l = 0; l < vChar.size(); l++)
							{
								global::Char char2 = (global::Char)vChar.elementAt(l);
								message.writer().writeInt(char2.charID);
							}
						}
					}
				}
				bool flag7 = message != null;
				if (flag7)
				{
					this.session.sendMessage(message);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x0008DA9C File Offset: 0x0008BC9C
	public void pickItem(int itemMapId)
	{
		Message message = null;
		try
		{
			message = new Message(-20);
			message.writer().writeShort(itemMapId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x0008DB18 File Offset: 0x0008BD18
	public void throwItem(int index)
	{
		Message message = null;
		try
		{
			message = new Message(-18);
			message.writer().writeByte(index);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x0008DB94 File Offset: 0x0008BD94
	public void returnTownFromDead()
	{
		Message message = null;
		try
		{
			message = new Message(-15);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x0008DC04 File Offset: 0x0008BE04
	public void wakeUpFromDead()
	{
		Message message = null;
		try
		{
			message = new Message(-16);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x0008DC74 File Offset: 0x0008BE74
	public void chat(string text)
	{
		bool flag = Pk9rXmap.Chat(text);
		if (!flag)
		{
			bool flag2 = Pk9rPickMob.Chat(text);
			if (!flag2)
			{
				Message message = null;
				try
				{
					message = new Message(44);
					message.writer().writeUTF(text);
					this.session.sendMessage(message);
				}
				catch (Exception ex)
				{
					Cout.println(ex.Message + ex.StackTrace);
				}
				finally
				{
					message.cleanup();
				}
			}
		}
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x0008DD08 File Offset: 0x0008BF08
	public void updateData()
	{
		Message message = null;
		try
		{
			message = new Message(-87);
			bool flag = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x0008DDB8 File Offset: 0x0008BFB8
	public void updateMap()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(6);
			bool flag = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x0008DE68 File Offset: 0x0008C068
	public void updateSkill()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(7);
			bool flag = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x0008DF0C File Offset: 0x0008C10C
	public void updateItem()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(8);
			bool flag = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x0008DFB0 File Offset: 0x0008C1B0
	public void clientOk()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(13);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x0008E020 File Offset: 0x0008C220
	public void tradeInvite(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(36);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0008E09C File Offset: 0x0008C29C
	public void addFriend(string name)
	{
		Message message = null;
		try
		{
			message = new Message(53);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x0008E118 File Offset: 0x0008C318
	public void addPartyAccept(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(76);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x0008E194 File Offset: 0x0008C394
	public void addPartyCancel(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(77);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x0008E210 File Offset: 0x0008C410
	public void testInvite(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(59);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x0008E28C File Offset: 0x0008C48C
	public void addCuuSat(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(62);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0008E308 File Offset: 0x0008C508
	public void addParty(string name)
	{
		Message message = null;
		try
		{
			message = new Message(75);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x0008E384 File Offset: 0x0008C584
	public void player_vs_player(sbyte action, sbyte type, int playerId)
	{
		Message message = null;
		try
		{
			message = new Message(-59);
			message.writer().writeByte(action);
			message.writer().writeByte(type);
			message.writer().writeInt(playerId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0008E418 File Offset: 0x0008C618
	public void requestMaptemplate(int maptemplateId)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(10);
			message.writer().writeByte(maptemplateId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x0008E494 File Offset: 0x0008C694
	public void outParty()
	{
		Message message = null;
		try
		{
			message = new Message(79);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x0008E504 File Offset: 0x0008C704
	public void requestPlayerInfo(MyVector chars)
	{
		Message message = null;
		try
		{
			message = new Message(18);
			message.writer().writeByte(chars.size());
			for (int i = 0; i < chars.size(); i++)
			{
				global::Char @char = (global::Char)chars.elementAt(i);
				message.writer().writeInt(@char.charID);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x0008E5BC File Offset: 0x0008C7BC
	public void pleaseInputParty(string str)
	{
		Message message = null;
		try
		{
			message = new Message(16);
			message.writer().writeUTF(str);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x0008E638 File Offset: 0x0008C838
	public void acceptPleaseParty(string str)
	{
		Message message = null;
		try
		{
			message = new Message(17);
			message.writer().writeUTF(str);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x0008E6B4 File Offset: 0x0008C8B4
	public void chatPlayer(string text, int id)
	{
		Res.outz("chat player text = " + text);
		Message message = null;
		try
		{
			message = new Message(-72);
			message.writer().writeInt(id);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x0008E74C File Offset: 0x0008C94C
	public void chatGlobal(string text)
	{
		Message message = null;
		try
		{
			message = new Message(-71);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x0008E7C8 File Offset: 0x0008C9C8
	public void chatPrivate(string to, string text)
	{
		Message message = null;
		try
		{
			message = new Message(91);
			message.writer().writeUTF(to);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x0008E850 File Offset: 0x0008CA50
	public void sendCardInfo(string NAP, string PIN)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(16);
			message.writer().writeUTF(NAP);
			message.writer().writeUTF(PIN);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x0008E8D8 File Offset: 0x0008CAD8
	public void saveRms(string key, sbyte[] data)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(60);
			message.writer().writeUTF(key);
			message.writer().writeInt(data.Length);
			message.writer().write(data);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x0008E970 File Offset: 0x0008CB70
	public void loadRMS(string key)
	{
		Cout.println("REQUEST RMS");
		Message message = null;
		try
		{
			message = Service.messageSubCommand(61);
			message.writer().writeUTF(key);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x0008E9F8 File Offset: 0x0008CBF8
	public void clearTask()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(17);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x0008EA68 File Offset: 0x0008CC68
	public void changeName(string name, int id)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(18);
			message.writer().writeInt(id);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x0008EAF0 File Offset: 0x0008CCF0
	public void requestIcon(int id)
	{
		GameCanvas.connect();
		Message message = null;
		try
		{
			Res.outz("REQUEST ICON " + id);
			message = new Message(-67);
			message.writer().writeInt(id);
			bool flag = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x0008EBC8 File Offset: 0x0008CDC8
	public void doConvertUpgrade(int index1, int index2, int index3)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(33);
			message.writer().writeByte(index1);
			message.writer().writeByte(index2);
			message.writer().writeByte(index3);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0008EC60 File Offset: 0x0008CE60
	public void inviteClanDun(string name)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(34);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x0008ECDC File Offset: 0x0008CEDC
	public void inputNumSplit(int indexItem, int numSplit)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(40);
			message.writer().writeByte(indexItem);
			message.writer().writeInt(numSplit);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x0008ED64 File Offset: 0x0008CF64
	public void activeAccProtect(int pass)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(37);
			message.writer().writeInt(pass);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0008EDE0 File Offset: 0x0008CFE0
	public void clearAccProtect(int pass)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(41);
			message.writer().writeInt(pass);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x0008EE5C File Offset: 0x0008D05C
	public void updateActive(int passOld, int passNew)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(38);
			message.writer().writeInt(passOld);
			message.writer().writeInt(passNew);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x0008EEE4 File Offset: 0x0008D0E4
	public void openLockAccProtect(int pass2)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(39);
			message.writer().writeInt(pass2);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x0008EF60 File Offset: 0x0008D160
	public void getBgTemplate(short id)
	{
		Message message = null;
		try
		{
			message = new Message(-32);
			message.writer().writeShort(id);
			bool flag = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x0008F01C File Offset: 0x0008D21C
	public void getMapOffline()
	{
		Message message = null;
		try
		{
			message = new Message(-33);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x0008F08C File Offset: 0x0008D28C
	public void finishUpdate()
	{
		Message message = null;
		try
		{
			message = new Message(-38);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x0008F0FC File Offset: 0x0008D2FC
	public void finishLoadMap()
	{
		Message message = null;
		try
		{
			message = new Message(-39);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x0008F16C File Offset: 0x0008D36C
	public void getChest(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-35);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x0008F1E8 File Offset: 0x0008D3E8
	public void requestBagImage(sbyte ID)
	{
		Message message = null;
		try
		{
			message = new Message(-63);
			message.writer().writeByte(ID);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x0008F264 File Offset: 0x0008D464
	public void getBag(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-36);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0008F2E0 File Offset: 0x0008D4E0
	public void getBody(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-37);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0008F35C File Offset: 0x0008D55C
	public void login2(string user)
	{
		Res.outz("Login 2");
		Message message = null;
		try
		{
			message = new Message(-101);
			message.writer().writeUTF(user);
			message.writer().writeByte(1);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x0008F3D8 File Offset: 0x0008D5D8
	public void getMagicTree(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-34);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x0008F454 File Offset: 0x0008D654
	public void upPotential(int typePotential, int num)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(16);
			message.writer().writeByte(typePotential);
			message.writer().writeShort(num);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0008F4DC File Offset: 0x0008D6DC
	public void getResource(sbyte action, MyVector vResourceIndex)
	{
		Res.outz("request resource action= " + action);
		Message message = null;
		try
		{
			message = new Message(-74);
			message.writer().writeByte(action);
			bool flag = action == 2 && vResourceIndex != null;
			if (flag)
			{
				message.writer().writeShort(vResourceIndex.size());
				for (int i = 0; i < vResourceIndex.size(); i++)
				{
					message.writer().writeShort(short.Parse((string)vResourceIndex.elementAt(i)));
				}
			}
			bool flag2 = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag2)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				Service.reciveFromMainSession = true;
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0008F610 File Offset: 0x0008D810
	public void requestMapSelect(int selected)
	{
		Res.outz("request magic tree");
		Message message = null;
		try
		{
			message = new Message(-91);
			message.writer().writeByte(selected);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x0008F680 File Offset: 0x0008D880
	public void petInfo()
	{
		Message message = null;
		try
		{
			message = new Message(-107);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x0008F6D8 File Offset: 0x0008D8D8
	public void sendTop(string topName, sbyte selected)
	{
		Message message = null;
		try
		{
			message = new Message(-96);
			message.writer().writeUTF(topName);
			message.writer().writeByte(selected);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x0008F748 File Offset: 0x0008D948
	public void enemy(sbyte b, int charID)
	{
		Message message = null;
		Res.outz("add enemy");
		try
		{
			message = new Message(-99);
			message.writer().writeByte(b);
			bool flag = b == 1 || b == 2;
			if (flag)
			{
				message.writer().writeInt(charID);
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x0008F7D4 File Offset: 0x0008D9D4
	public void kigui(sbyte action, int itemId, sbyte moneyType, int money, int quaintly)
	{
		Message message = null;
		try
		{
			Res.outz("ki gui action= " + action);
			message = new Message(-100);
			message.writer().writeByte(action);
			bool flag = action == 0;
			if (flag)
			{
				message.writer().writeShort(itemId);
				message.writer().writeByte(moneyType);
				message.writer().writeInt(money);
				message.writer().writeInt(quaintly);
			}
			bool flag2 = action == 1 || action == 2;
			if (flag2)
			{
				message.writer().writeShort(itemId);
			}
			bool flag3 = action == 3;
			if (flag3)
			{
				message.writer().writeShort(itemId);
				message.writer().writeByte(moneyType);
				message.writer().writeInt(money);
			}
			bool flag4 = action == 4;
			if (flag4)
			{
				message.writer().writeByte(moneyType);
				message.writer().writeByte(money);
				Res.outz(string.Concat(new object[]
				{
					"currTab= ",
					moneyType,
					" page= ",
					money
				}));
			}
			bool flag5 = action == 5;
			if (flag5)
			{
				message.writer().writeShort(itemId);
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x0008F96C File Offset: 0x0008DB6C
	public void getFlag(sbyte action, sbyte flagType)
	{
		Message message = null;
		try
		{
			message = new Message(-103);
			message.writer().writeByte(action);
			Res.outz(string.Concat(new object[]
			{
				"------------service--  ",
				action,
				"   ",
				flagType
			}));
			bool flag = action != 0;
			if (flag)
			{
				message.writer().writeByte(flagType);
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0008FA1C File Offset: 0x0008DC1C
	public void setLockInventory(int pass)
	{
		Message message = null;
		try
		{
			Res.outz("------------setLockInventory:     " + pass);
			message = new Message(-104);
			message.writer().writeInt(pass);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x0008FA98 File Offset: 0x0008DC98
	public void petStatus(sbyte status)
	{
		Message message = null;
		try
		{
			message = new Message(-108);
			message.writer().writeByte(status);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x0008FAFC File Offset: 0x0008DCFC
	public void transportNow()
	{
		Message message = null;
		try
		{
			Res.outz("------------transportNow  ");
			message = new Message(-105);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x0008FB60 File Offset: 0x0008DD60
	public void funsion(sbyte type)
	{
		Message message = null;
		try
		{
			Res.outz("FUNSION");
			message = new Message(125);
			message.writer().writeByte(type);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0008FBDC File Offset: 0x0008DDDC
	public void imageSource(MyVector vID)
	{
		Message message = null;
		try
		{
			Res.outz("IMAGE SOURCE size= " + vID.size());
			message = new Message(-111);
			message.writer().writeShort(vID.size());
			bool flag = vID.size() > 0;
			if (flag)
			{
				for (int i = 0; i < vID.size(); i++)
				{
					Res.outz("gui len str " + ((ImageSource)vID.elementAt(i)).id);
					message.writer().writeUTF(((ImageSource)vID.elementAt(i)).id);
				}
			}
			bool flag2 = Session_ME2.gI().isConnected() && !Session_ME2.connecting;
			if (flag2)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
				Service.reciveFromMainSession = true;
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x0008FD34 File Offset: 0x0008DF34
	public void getQuayso()
	{
		Message message = null;
		try
		{
			message = new Message(-126);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x0008FD98 File Offset: 0x0008DF98
	public void sendServerData(sbyte action, int id, sbyte[] data)
	{
		Message message = null;
		try
		{
			Res.outz("SERVER DATA");
			message = new Message(-110);
			message.writer().writeByte(action);
			bool flag = action == 1;
			if (flag)
			{
				message.writer().writeInt(id);
				bool flag2 = data != null;
				if (flag2)
				{
					int num = data.Length;
					message.writer().writeShort(num);
					message.writer().write(ref data, 0, num);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x0008FE48 File Offset: 0x0008E048
	public void changeOnKeyScr(sbyte[] skill)
	{
		Message message = null;
		try
		{
			message = new Message(-113);
			for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
			{
				message.writer().writeByte(skill[i]);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x0008FED4 File Offset: 0x0008E0D4
	public void requestPean()
	{
		Message message = null;
		try
		{
			message = new Message(-114);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x0008FF38 File Offset: 0x0008E138
	public void sendThachDau(int id)
	{
		Res.outz("GUI THACH DAU");
		Message message = null;
		try
		{
			message = new Message(-118);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x0008FFB4 File Offset: 0x0008E1B4
	public void messagePlayerMenu(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(-30);
			message.writer().writeByte(63);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x00090034 File Offset: 0x0008E234
	public void playerMenuAction(int charId, short select)
	{
		Message message = null;
		try
		{
			message = new Message(-30);
			message.writer().writeByte(64);
			message.writer().writeInt(charId);
			message.writer().writeShort(select);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x000900C0 File Offset: 0x0008E2C0
	public void getImgByName(string nameImg)
	{
		Message message = null;
		try
		{
			message = new Message(66);
			message.writer().writeUTF(nameImg);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00090124 File Offset: 0x0008E324
	public void SendCrackBall(byte type, byte soluong)
	{
		Message message = new Message(-127);
		try
		{
			message.writer().writeByte((int)type);
			bool flag = soluong > 0;
			if (flag)
			{
				message.writer().writeByte((int)soluong);
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x0009019C File Offset: 0x0008E39C
	public void SendRada(int i, int id)
	{
		Message message = new Message(sbyte.MaxValue);
		try
		{
			message.writer().writeByte(i);
			bool flag = id != -1;
			if (flag)
			{
				message.writer().writeShort(id);
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x00090218 File Offset: 0x0008E418
	public void sendOptHat()
	{
		Message message = new Message(24);
		try
		{
			message.writer().writeByte(0);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x0009027C File Offset: 0x0008E47C
	public void sendDelAcc()
	{
		Message message = new Message(69);
		try
		{
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x04001032 RID: 4146
	private ISession session = Session_ME.gI();

	// Token: 0x04001033 RID: 4147
	protected static Service instance;

	// Token: 0x04001034 RID: 4148
	public static long curCheckController;

	// Token: 0x04001035 RID: 4149
	public static long curCheckMap;

	// Token: 0x04001036 RID: 4150
	public static long logController;

	// Token: 0x04001037 RID: 4151
	public static long logMap;

	// Token: 0x04001038 RID: 4152
	public int demGui;

	// Token: 0x04001039 RID: 4153
	public static bool reciveFromMainSession;
}
