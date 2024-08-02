using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class MyAudioClip
{
	// Token: 0x0600059B RID: 1435 RVA: 0x00005228 File Offset: 0x00003428
	public MyAudioClip(string filename)
	{
		this.clip = (AudioClip)Resources.Load(filename);
		this.name = filename;
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x0000524A File Offset: 0x0000344A
	public void Play()
	{
		Main.main.GetComponent<AudioSource>().PlayOneShot(this.clip);
		this.timeStart = mSystem.currentTimeMillis();
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x0001A69C File Offset: 0x0001889C
	public bool isPlaying()
	{
		return false;
	}

	// Token: 0x04000D51 RID: 3409
	public string name;

	// Token: 0x04000D52 RID: 3410
	public AudioClip clip;

	// Token: 0x04000D53 RID: 3411
	public long timeStart;
}
