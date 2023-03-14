using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class TapePlayerScript : MonoBehaviour
{
	// Token: 0x060009F4 RID: 2548 RVA: 0x000269D7 File Offset: 0x00024DD7
	private void Start()
	{
		this.audioDevice = base.GetComponent<AudioSource>();
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x000269E8 File Offset: 0x00024DE8
	private void Update()
	{
		if (this.audioDevice.isPlaying & Time.timeScale == 0f)
		{
			this.audioDevice.Pause();
		}
		else if (Time.timeScale > 0f & this.baldi.antiHearingTime > 0f)
		{
			this.audioDevice.UnPause();
		}
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x00026A51 File Offset: 0x00024E51
	public void Play()
	{
		this.sprite.sprite = this.closedSprite;
		this.audioDevice.Play();
		if (this.baldi.isActiveAndEnabled) this.baldi.ActivateAntiHearing(30f);
	}

	// Token: 0x04000726 RID: 1830
	public Sprite closedSprite;

	// Token: 0x04000727 RID: 1831
	public SpriteRenderer sprite;

	// Token: 0x04000728 RID: 1832
	public BaldiScript baldi;

	// Token: 0x04000729 RID: 1833
	private AudioSource audioDevice;
}
