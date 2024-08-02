using System;
using System.Threading;
using UnityEngine;

// Token: 0x020000AA RID: 170
public class Sound
{
	// Token: 0x060008F5 RID: 2293 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void setActivity(SoundMn.AssetManager ac)
	{
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x00092C00 File Offset: 0x00090E00
	public static void stop()
	{
		for (int i = 0; i < Sound.player.Length; i++)
		{
			bool flag = Sound.player[i] != null;
			if (flag)
			{
				Sound.player[i].GetComponent<AudioSource>().Pause();
			}
		}
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x0001A69C File Offset: 0x0001889C
	public static bool isPlaying()
	{
		return false;
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x00092C4C File Offset: 0x00090E4C
	public static void init()
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "Audio Player";
		gameObject.transform.position = Vector3.zero;
		gameObject.AddComponent<AudioListener>();
		Sound.SoundBGLoop = gameObject.AddComponent<AudioSource>();
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x00092C90 File Offset: 0x00090E90
	public static void init(int[] musicID, int[] sID)
	{
		bool flag = Sound.player == null && Sound.music == null;
		if (flag)
		{
			Sound.init();
			Sound.l1 = musicID.Length;
			Sound.player = new GameObject[musicID.Length + sID.Length];
			Sound.music = new AudioClip[musicID.Length + sID.Length];
			for (int i = 0; i < Sound.player.Length; i++)
			{
				string text = (i >= Sound.l1) ? ("/sound/" + (i - Sound.l1)) : ("/music/" + i);
				Sound.getAssetSoundFile(text, i);
			}
		}
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00005F26 File Offset: 0x00004126
	public static void playSound(int id, float volume)
	{
		Sound.play(id + Sound.l1, volume);
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x00005F37 File Offset: 0x00004137
	public static void playSound1(int id, float volume)
	{
		Sound.play(id, volume);
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x00092D3C File Offset: 0x00090F3C
	public static void getAssetSoundFile(string fileName, int pos)
	{
		Sound.stop(pos);
		string filename = string.Empty;
		filename = Main.res + fileName;
		Sound.load(filename, pos);
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x00092D6C File Offset: 0x00090F6C
	public static void stopAllz()
	{
		for (int i = 0; i < Sound.music.Length; i++)
		{
			Sound.stop(i);
		}
		for (int j = 0; j < Sound.l1; j++)
		{
			Sound.sTopSoundBG(j);
		}
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x00092DB8 File Offset: 0x00090FB8
	public static void stopAllBg()
	{
		for (int i = 0; i < Sound.music.Length; i++)
		{
			Sound.stop(i);
		}
		Sound.sTopSoundBG(0);
		Sound.sTopSoundRun();
		Sound.stopSoundNatural(0);
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00003A0D File Offset: 0x00001C0D
	public static void update()
	{
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x00092DFC File Offset: 0x00090FFC
	public static void stopMusic(int x)
	{
		bool isPlaySound = GameCanvas.isPlaySound;
		if (isPlaySound)
		{
			Sound.stop(x);
		}
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x00092E1C File Offset: 0x0009101C
	public static void play(int id, float volume)
	{
		bool flag = !Sound.isNotPlay && GameCanvas.isPlaySound;
		if (flag)
		{
			Sound.start(volume, id);
		}
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x00092E48 File Offset: 0x00091048
	public static void playSoundRun(int id, float volume)
	{
		bool flag = GameCanvas.isPlaySound && !(Sound.SoundRun == null);
		if (flag)
		{
			Sound.SoundRun.GetComponent<AudioSource>().loop = true;
			Sound.SoundRun.GetComponent<AudioSource>().clip = Sound.music[id];
			Sound.SoundRun.GetComponent<AudioSource>().volume = volume;
			Sound.SoundRun.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x00005F42 File Offset: 0x00004142
	public static void sTopSoundRun()
	{
		Sound.SoundRun.GetComponent<AudioSource>().Stop();
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x00092EC0 File Offset: 0x000910C0
	public static bool isPlayingSound()
	{
		bool flag = Sound.SoundRun == null;
		return !flag && Sound.SoundRun.GetComponent<AudioSource>().isPlaying;
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00092EF8 File Offset: 0x000910F8
	public static void playSoundNatural(int id, float volume, bool isLoop)
	{
		bool flag = GameCanvas.isPlaySound && !(Sound.SoundBGLoop == null);
		if (flag)
		{
			Sound.SoundWater.GetComponent<AudioSource>().loop = isLoop;
			Sound.SoundWater.GetComponent<AudioSource>().clip = Sound.music[id];
			Sound.SoundWater.GetComponent<AudioSource>().volume = volume;
			Sound.SoundWater.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00005F55 File Offset: 0x00004155
	public static void stopSoundNatural(int id)
	{
		Sound.SoundWater.GetComponent<AudioSource>().Stop();
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00092F70 File Offset: 0x00091170
	public static bool isPlayingSoundatural(int id)
	{
		bool flag = Sound.SoundWater == null;
		return !flag && Sound.SoundWater.GetComponent<AudioSource>().isPlaying;
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x00092FA8 File Offset: 0x000911A8
	public static void playMus(int type, float vl, bool loop)
	{
		bool flag = !Sound.isNotPlay;
		if (flag)
		{
			vl -= 0.3f;
			bool flag2 = vl <= 0f;
			if (flag2)
			{
				vl = 0.01f;
			}
			Sound.playSoundBGLoop(type, vl);
		}
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x00092FF0 File Offset: 0x000911F0
	public static void playSoundBGLoop(int id, float volume)
	{
		bool isPlaySound = GameCanvas.isPlaySound;
		if (isPlaySound)
		{
			bool flag = id == SoundMn.AIR_SHIP;
			if (flag)
			{
				Sound.playSound1(id, volume + 0.2f);
			}
			else
			{
				bool flag2 = !(Sound.SoundBGLoop == null) && !Sound.isPlayingSoundBG(id);
				if (flag2)
				{
					Sound.SoundBGLoop.GetComponent<AudioSource>().loop = true;
					Sound.SoundBGLoop.GetComponent<AudioSource>().clip = Sound.music[id];
					Sound.SoundBGLoop.GetComponent<AudioSource>().volume = volume;
					Sound.SoundBGLoop.GetComponent<AudioSource>().Play();
				}
			}
		}
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x00005F68 File Offset: 0x00004168
	public static void sTopSoundBG(int id)
	{
		Sound.SoundBGLoop.GetComponent<AudioSource>().Stop();
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x00093094 File Offset: 0x00091294
	public static bool isPlayingSoundBG(int id)
	{
		bool flag = Sound.SoundBGLoop == null;
		return !flag && Sound.SoundBGLoop.GetComponent<AudioSource>().isPlaying;
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x000930CC File Offset: 0x000912CC
	public static void load(string filename, int pos)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		if (flag)
		{
			Sound.__load(filename, pos);
		}
		else
		{
			Sound._load(filename, pos);
		}
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x00093108 File Offset: 0x00091308
	private static void _load(string filename, int pos)
	{
		bool flag = Sound.status != 0;
		if (flag)
		{
			Cout.LogError("CANNOT LOAD AUDIO " + filename + " WHEN LOADING " + Sound.filenametemp);
		}
		else
		{
			Sound.filenametemp = filename;
			Sound.postem = pos;
			Sound.status = 2;
			int i;
			for (i = 0; i < 100; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Sound.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 100;
			if (flag3)
			{
				Cout.LogError("TOO LONG FOR LOAD AUDIO " + filename);
			}
			else
			{
				Cout.Log(string.Concat(new object[]
				{
					"Load Audio ",
					filename,
					" done in ",
					i * 5,
					"ms"
				}));
			}
		}
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x00005F7B File Offset: 0x0000417B
	private static void __load(string filename, int pos)
	{
		Sound.music[pos] = (AudioClip)Resources.Load(filename, typeof(AudioClip));
		GameObject.Find("Main Camera").AddComponent<AudioSource>();
		Sound.player[pos] = GameObject.Find("Main Camera");
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x000931D8 File Offset: 0x000913D8
	public static void start(float volume, int pos)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		if (flag)
		{
			Sound.__start(volume, pos);
		}
		else
		{
			Sound._start(volume, pos);
		}
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x00093214 File Offset: 0x00091414
	public static void _start(float volume, int pos)
	{
		bool flag = Sound.status != 0;
		if (flag)
		{
			Debug.LogError("CANNOT START AUDIO WHEN STARTING");
		}
		else
		{
			Sound.volumetem = volume;
			Sound.postem = pos;
			Sound.status = 3;
			int i;
			for (i = 0; i < 100; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Sound.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 100;
			if (flag3)
			{
				Debug.LogError("TOO LONG FOR START AUDIO");
			}
			else
			{
				Debug.Log("Start Audio done in " + i * 5 + "ms");
			}
		}
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x000932B0 File Offset: 0x000914B0
	public static void __start(float volume, int pos)
	{
		bool flag = !(Sound.player[pos] == null);
		if (flag)
		{
			Sound.player[pos].GetComponent<AudioSource>().PlayOneShot(Sound.music[pos], volume);
		}
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x000932F0 File Offset: 0x000914F0
	public static void stop(int pos)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		if (flag)
		{
			Sound.__stop(pos);
		}
		else
		{
			Sound._stop(pos);
		}
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x0009332C File Offset: 0x0009152C
	public static void _stop(int pos)
	{
		bool flag = Sound.status != 0;
		if (flag)
		{
			Debug.LogError("CANNOT STOP AUDIO WHEN STOPPING");
		}
		else
		{
			Sound.postem = pos;
			Sound.status = 4;
			int i;
			for (i = 0; i < 100; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Sound.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 100;
			if (flag3)
			{
				Debug.LogError("TOO LONG FOR STOP AUDIO");
			}
			else
			{
				Debug.Log("Stop Audio done in " + i * 5 + "ms");
			}
		}
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x000933C4 File Offset: 0x000915C4
	public static void __stop(int pos)
	{
		bool flag = Sound.player[pos] != null;
		if (flag)
		{
			Sound.player[pos].GetComponent<AudioSource>().Stop();
		}
	}

	// Token: 0x040010C4 RID: 4292
	private const int INTERVAL = 5;

	// Token: 0x040010C5 RID: 4293
	private const int MAXTIME = 100;

	// Token: 0x040010C6 RID: 4294
	public static int status;

	// Token: 0x040010C7 RID: 4295
	public static int postem;

	// Token: 0x040010C8 RID: 4296
	public static int timestart;

	// Token: 0x040010C9 RID: 4297
	private static string filenametemp;

	// Token: 0x040010CA RID: 4298
	private static float volumetem;

	// Token: 0x040010CB RID: 4299
	public static bool isSound = true;

	// Token: 0x040010CC RID: 4300
	public static bool isNotPlay;

	// Token: 0x040010CD RID: 4301
	public static bool stopAll;

	// Token: 0x040010CE RID: 4302
	public static AudioSource SoundWater;

	// Token: 0x040010CF RID: 4303
	public static AudioSource SoundRun;

	// Token: 0x040010D0 RID: 4304
	public static AudioSource SoundBGLoop;

	// Token: 0x040010D1 RID: 4305
	public static AudioClip[] music;

	// Token: 0x040010D2 RID: 4306
	public static GameObject[] player;

	// Token: 0x040010D3 RID: 4307
	public static string[] fileName = new string[]
	{
		"0",
		"1",
		"2",
		"3",
		"4",
		"5",
		"6",
		"7",
		"8",
		"9",
		"10",
		"11",
		"12",
		"13",
		"14",
		"15",
		"16",
		"17",
		"18",
		"19",
		"29",
		"21",
		"22",
		"23",
		"24",
		"25",
		"26",
		"27",
		"28",
		"29",
		"30",
		"31",
		"32",
		"33"
	};

	// Token: 0x040010D4 RID: 4308
	public static sbyte MLogin = 0;

	// Token: 0x040010D5 RID: 4309
	public static sbyte MBClick = 1;

	// Token: 0x040010D6 RID: 4310
	public static sbyte MTone = 2;

	// Token: 0x040010D7 RID: 4311
	public static sbyte MSanzu = 3;

	// Token: 0x040010D8 RID: 4312
	public static sbyte MChakumi = 4;

	// Token: 0x040010D9 RID: 4313
	public static sbyte MChai = 5;

	// Token: 0x040010DA RID: 4314
	public static sbyte MOshin = 6;

	// Token: 0x040010DB RID: 4315
	public static sbyte MEchigo = 7;

	// Token: 0x040010DC RID: 4316
	public static sbyte MKojin = 8;

	// Token: 0x040010DD RID: 4317
	public static sbyte MHaruna = 9;

	// Token: 0x040010DE RID: 4318
	public static sbyte MHirosaki = 10;

	// Token: 0x040010DF RID: 4319
	public static sbyte MOokaza = 11;

	// Token: 0x040010E0 RID: 4320
	public static sbyte MGiotuyet = 12;

	// Token: 0x040010E1 RID: 4321
	public static sbyte MHangdong = 13;

	// Token: 0x040010E2 RID: 4322
	public static sbyte MDeKeu = 14;

	// Token: 0x040010E3 RID: 4323
	public static sbyte MChimKeu = 15;

	// Token: 0x040010E4 RID: 4324
	public static sbyte MBuocChan = 16;

	// Token: 0x040010E5 RID: 4325
	public static sbyte MNuocChay = 17;

	// Token: 0x040010E6 RID: 4326
	public static sbyte MBomMau = 18;

	// Token: 0x040010E7 RID: 4327
	public static sbyte MKiemGo = 19;

	// Token: 0x040010E8 RID: 4328
	public static sbyte MKiem = 20;

	// Token: 0x040010E9 RID: 4329
	public static sbyte MTieu = 21;

	// Token: 0x040010EA RID: 4330
	public static sbyte MKunai = 22;

	// Token: 0x040010EB RID: 4331
	public static sbyte MCung = 23;

	// Token: 0x040010EC RID: 4332
	public static sbyte MDao = 24;

	// Token: 0x040010ED RID: 4333
	public static sbyte MQuat = 25;

	// Token: 0x040010EE RID: 4334
	public static sbyte MCung2 = 26;

	// Token: 0x040010EF RID: 4335
	public static sbyte MTieu2 = 27;

	// Token: 0x040010F0 RID: 4336
	public static sbyte MTieu3 = 28;

	// Token: 0x040010F1 RID: 4337
	public static sbyte MKiem2 = 29;

	// Token: 0x040010F2 RID: 4338
	public static sbyte MKiem3 = 30;

	// Token: 0x040010F3 RID: 4339
	public static sbyte MDao2 = 31;

	// Token: 0x040010F4 RID: 4340
	public static sbyte MDao3 = 32;

	// Token: 0x040010F5 RID: 4341
	public static sbyte MCung3 = 33;

	// Token: 0x040010F6 RID: 4342
	public static int l1;
}
