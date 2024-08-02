using System;
using System.Net.NetworkInformation;
using System.Threading;
using AssemblyCSharp.Mod.Xmap;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class Main : MonoBehaviour
{
	// Token: 0x0600047D RID: 1149 RVA: 0x0005816C File Offset: 0x0005636C
	private void Start()
	{
		Time.timeScale = 2f;
		VuDang.LoadGame();
		if (!Main.started)
		{
			if (Thread.CurrentThread.Name != "Main")
			{
				Thread.CurrentThread.Name = "Main";
			}
			Main.mainThreadName = Thread.CurrentThread.Name;
			Main.isPC = true;
			Main.started = true;
			if (Main.isPC)
			{
				this.level = Rms.loadRMSInt("levelScreenKN");
				if (this.level == 1)
				{
					Screen.SetResolution(720, 320, false);
					return;
				}
				Screen.SetResolution(1024, 600, false);
			}
		}
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x00004C2B File Offset: 0x00002E2B
	private void SetInit()
	{
		base.enabled = true;
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x00004C34 File Offset: 0x00002E34
	private void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown)
		{
			Time.timeScale = 0f;
			return;
		}
		Time.timeScale = 1f;
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00058218 File Offset: 0x00056418
	private void OnGUI()
	{
		if (this.count >= 10)
		{
			if (this.fps == 0)
			{
				this.timefps = mSystem.currentTimeMillis();
			}
			else if (mSystem.currentTimeMillis() - this.timefps > 1000L)
			{
				this.max = this.fps;
				this.fps = 0;
				this.timefps = mSystem.currentTimeMillis();
			}
			this.fps++;
			this.checkInput();
			Session_ME.update();
			Session_ME2.update();
			if (Event.current.type.Equals(EventType.Repaint) && this.paintCount <= this.updateCount)
			{
				GameMidlet.gameCanvas.paint(Main.g);
				this.paintCount++;
				Main.g.reset();
			}
		}
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x00058300 File Offset: 0x00056500
	public void setsizeChange()
	{
		if (!this.isRun)
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			Application.runInBackground = true;
			Application.targetFrameRate = 35;
			base.useGUILayout = false;
			Main.isCompactDevice = Main.detectCompactDevice();
			if (Main.main == null)
			{
				Main.main = this;
			}
			this.isRun = true;
			ScaleGUI.initScaleGUI();
			if (Main.isPC)
			{
				Main.IMEI = SystemInfo.deviceUniqueIdentifier;
			}
			else
			{
				Main.IMEI = this.GetMacAddress();
			}
			Main.isPC = true;
			if (Main.isPC)
			{
				Screen.fullScreen = false;
			}
			if (Main.isWindowsPhone)
			{
				Main.typeClient = 6;
			}
			if (Main.isPC)
			{
				Main.typeClient = 4;
			}
			if (Main.IphoneVersionApp)
			{
				Main.typeClient = 5;
			}
			if (iPhoneSettings.generation == iPhoneGeneration.iPodTouch4Gen)
			{
				Main.isIpod = true;
			}
			if (iPhoneSettings.generation == iPhoneGeneration.iPhone4)
			{
				Main.isIphone4 = true;
			}
			Main.g = new mGraphics();
			Main.midlet = new GameMidlet();
			TileMap.loadBg();
			Paint.loadbg();
			PopUp.loadBg();
			GameScr.loadBg();
			InfoMe.gI().loadCharId();
			Panel.loadBg();
			Menu.loadBg();
			Key.mapKeyPC();
			SoundMn.gI().loadSound(TileMap.mapID);
			Main.g.CreateLineMaterial();
		}
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x00004426 File Offset: 0x00002626
	public static void setBackupIcloud(string path)
	{
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00058434 File Offset: 0x00056634
	public string GetMacAddress()
	{
		string empty = string.Empty;
		NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
		for (int i = 0; i < allNetworkInterfaces.Length; i++)
		{
			PhysicalAddress physicalAddress = allNetworkInterfaces[i].GetPhysicalAddress();
			if (physicalAddress.ToString() != string.Empty)
			{
				return physicalAddress.ToString();
			}
		}
		return string.Empty;
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00058484 File Offset: 0x00056684
	public void doClearRMS()
	{
		if (Main.isPC && Rms.loadRMSInt("lastZoomlevel") != mGraphics.zoomLevel)
		{
			Rms.clearAll();
			Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
			Rms.saveRMSInt("levelScreenKN", this.level);
		}
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x00004C51 File Offset: 0x00002E51
	public static void closeKeyBoard()
	{
		if (global::TouchScreenKeyboard.visible)
		{
			TField.kb.active = false;
			TField.kb = null;
		}
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x000584D4 File Offset: 0x000566D4
	private void FixedUpdate()
	{
		if (VuDang.giamDungLuong)
		{
			Thread.Sleep(12);
		}
		Rms.update();
		this.count++;
		if (this.count >= 10)
		{
			if (this.up == 0)
			{
				this.timeup = mSystem.currentTimeMillis();
			}
			else if (mSystem.currentTimeMillis() - this.timeup > 1000L)
			{
				this.upmax = this.up;
				this.up = 0;
				this.timeup = mSystem.currentTimeMillis();
			}
			this.up++;
			this.setsizeChange();
			this.updateCount++;
			ipKeyboard.update();
			GameMidlet.gameCanvas.update();
			Image.update();
			DataInputStream.update();
			SMS.update();
			Net.update();
			if (VuDang.canUpdate)
			{
				Pk9rXmap.Update();
				VuDang.update();
			}
			Main.f++;
			if (Main.f > 8)
			{
				Main.f = 0;
			}
			if (!Main.isPC)
			{
				int num = 1 / Main.a;
			}
		}
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x00004426 File Offset: 0x00002626
	private void Update()
	{
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x000585E4 File Offset: 0x000567E4
	private void checkInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePosition = Input.mousePosition;
			GameMidlet.gameCanvas.pointerPressed((int)(mousePosition.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
			this.lastMousePos.x = mousePosition.x / (float)mGraphics.zoomLevel;
			this.lastMousePos.y = mousePosition.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
		}
		if (Input.GetMouseButton(0))
		{
			Vector3 mousePosition2 = Input.mousePosition;
			GameMidlet.gameCanvas.pointerDragged((int)(mousePosition2.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition2.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
			this.lastMousePos.x = mousePosition2.x / (float)mGraphics.zoomLevel;
			this.lastMousePos.y = mousePosition2.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
		}
		if (Input.GetMouseButtonUp(0))
		{
			Vector3 mousePosition3 = Input.mousePosition;
			this.lastMousePos.x = mousePosition3.x / (float)mGraphics.zoomLevel;
			this.lastMousePos.y = mousePosition3.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
			GameMidlet.gameCanvas.pointerReleased((int)(mousePosition3.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition3.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
		}
		if (Input.anyKeyDown && Event.current.type == EventType.KeyDown)
		{
			int num = MyKeyMap.map(Event.current.keyCode);
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				KeyCode keyCode = Event.current.keyCode;
				if (keyCode != KeyCode.Minus)
				{
					if (keyCode == KeyCode.Alpha2)
					{
						num = 64;
					}
				}
				else
				{
					num = 95;
				}
			}
			if (num != 0)
			{
				GameMidlet.gameCanvas.keyPressedz(num);
			}
		}
		if (Event.current.type == EventType.KeyUp)
		{
			int num2 = MyKeyMap.map(Event.current.keyCode);
			if (num2 != 0)
			{
				GameMidlet.gameCanvas.keyReleasedz(num2);
			}
		}
		if (Main.isPC)
		{
			GameMidlet.gameCanvas.scrollMouse((int)(Input.GetAxis("Mouse ScrollWheel") * 10f));
			int x = (int)Input.mousePosition.x;
			float y = Input.mousePosition.y;
			int x2 = x / mGraphics.zoomLevel;
			int y2 = (Screen.height - (int)y) / mGraphics.zoomLevel;
			GameMidlet.gameCanvas.pointerMouse(x2, y2);
		}
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x00004C6B File Offset: 0x00002E6B
	private void OnApplicationQuit()
	{
		Debug.LogWarning("APP QUIT");
		GameCanvas.bRun = false;
		Session_ME.gI().close();
		Session_ME2.gI().close();
		if (Main.isPC)
		{
			Application.Quit();
		}
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x00058864 File Offset: 0x00056A64
	private void OnApplicationPause(bool paused)
	{
		Main.isResume = false;
		if (paused)
		{
			if (GameCanvas.isWaiting())
			{
				Main.isQuitApp = true;
			}
		}
		else
		{
			Main.isResume = true;
		}
		if (global::TouchScreenKeyboard.visible)
		{
			TField.kb.active = false;
			TField.kb = null;
		}
		if (Main.isQuitApp)
		{
			Application.Quit();
		}
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x00004C9D File Offset: 0x00002E9D
	public static void exit()
	{
		if (Main.isPC)
		{
			Main.main.OnApplicationQuit();
			return;
		}
		Main.a = 0;
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x00004CB7 File Offset: 0x00002EB7
	public static bool detectCompactDevice()
	{
		return iPhoneSettings.generation != iPhoneGeneration.iPhone && iPhoneSettings.generation != iPhoneGeneration.iPhone3G && iPhoneSettings.generation != iPhoneGeneration.iPodTouch1Gen && iPhoneSettings.generation != iPhoneGeneration.iPodTouch2Gen;
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x00004CDF File Offset: 0x00002EDF
	public static bool checkCanSendSMS()
	{
		return iPhoneSettings.generation == iPhoneGeneration.iPhone3GS || iPhoneSettings.generation == iPhoneGeneration.iPhone4 || iPhoneSettings.generation > iPhoneGeneration.iPodTouch4Gen;
	}

	// Token: 0x040009D4 RID: 2516
	public static Main main;

	// Token: 0x040009D5 RID: 2517
	public static mGraphics g;

	// Token: 0x040009D6 RID: 2518
	public static GameMidlet midlet;

	// Token: 0x040009D7 RID: 2519
	public static string res = "res";

	// Token: 0x040009D8 RID: 2520
	public static string mainThreadName;

	// Token: 0x040009D9 RID: 2521
	public static bool started;

	// Token: 0x040009DA RID: 2522
	public static bool isIpod;

	// Token: 0x040009DB RID: 2523
	public static bool isIphone4;

	// Token: 0x040009DC RID: 2524
	public static bool isPC;

	// Token: 0x040009DD RID: 2525
	public static bool isWindowsPhone;

	// Token: 0x040009DE RID: 2526
	public static bool isIPhone;

	// Token: 0x040009DF RID: 2527
	public static bool IphoneVersionApp;

	// Token: 0x040009E0 RID: 2528
	public static string IMEI;

	// Token: 0x040009E1 RID: 2529
	public static int versionIp;

	// Token: 0x040009E2 RID: 2530
	public static int numberQuit = 1;

	// Token: 0x040009E3 RID: 2531
	public static int typeClient = 4;

	// Token: 0x040009E4 RID: 2532
	public const sbyte PC_VERSION = 4;

	// Token: 0x040009E5 RID: 2533
	public const sbyte IP_APPSTORE = 5;

	// Token: 0x040009E6 RID: 2534
	public const sbyte WINDOWSPHONE = 6;

	// Token: 0x040009E7 RID: 2535
	private int level;

	// Token: 0x040009E8 RID: 2536
	public const sbyte IP_JB = 3;

	// Token: 0x040009E9 RID: 2537
	private int updateCount;

	// Token: 0x040009EA RID: 2538
	private int paintCount;

	// Token: 0x040009EB RID: 2539
	private int count;

	// Token: 0x040009EC RID: 2540
	private int fps;

	// Token: 0x040009ED RID: 2541
	private int max;

	// Token: 0x040009EE RID: 2542
	private int up;

	// Token: 0x040009EF RID: 2543
	private int upmax;

	// Token: 0x040009F0 RID: 2544
	private long timefps;

	// Token: 0x040009F1 RID: 2545
	private long timeup;

	// Token: 0x040009F2 RID: 2546
	private bool isRun;

	// Token: 0x040009F3 RID: 2547
	public static int waitTick;

	// Token: 0x040009F4 RID: 2548
	public static int f;

	// Token: 0x040009F5 RID: 2549
	public static bool isResume;

	// Token: 0x040009F6 RID: 2550
	public static bool isMiniApp = true;

	// Token: 0x040009F7 RID: 2551
	public static bool isQuitApp;

	// Token: 0x040009F8 RID: 2552
	private Vector2 lastMousePos;

	// Token: 0x040009F9 RID: 2553
	public static int a = 1;

	// Token: 0x040009FA RID: 2554
	public static bool isCompactDevice = true;
}
