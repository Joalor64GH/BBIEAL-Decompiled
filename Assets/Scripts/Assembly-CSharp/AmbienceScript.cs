using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class AmbienceScript : MonoBehaviour
{
	// Token: 0x06000006 RID: 6 RVA: 0x0000214B File Offset: 0x0000054B
	private void Start()
	{
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002150 File Offset: 0x00000550
	public void PlayAudio()
	{
		int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 49f)); 
		if (!this.audioDevice.isPlaying & num == 0) // If it is not currently playing an audio device, and num is equal to 0 (1/50 chance)
		{
			base.transform.position = this.aiLocation.position; // Go to the location of the AILocation object
			int num2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, (float)(this.sounds.Length - 1))); // Choose a random number for playing a sound
			this.audioDevice.PlayOneShot(this.sounds[num2]); // Play the sound
		}
	}

	// Token: 0x04000007 RID: 7
	public Transform aiLocation;

	// Token: 0x04000008 RID: 8
	public AudioClip[] sounds;

	// Token: 0x04000009 RID: 9
	public AudioSource audioDevice;
}
