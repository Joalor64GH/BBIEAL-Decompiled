using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class AlarmClockScript : MonoBehaviour
{
	// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000458
	private void Start()
	{
		this.timeLeft = 30f;
		this.lifeSpan = 35f;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002070 File Offset: 0x00000470
	private void Update()
	{
		if (this.timeLeft >= 0f) //If the time is greater then 0
		{
			this.timeLeft -= Time.deltaTime; //Decrease the time variable
		}
		else if (!this.rang) // If it has not been rang
		{
			this.Alarm(); // Start the alarm function
		}
		if (this.lifeSpan >= 0f) //If the time left in the lifespan is greater then 0
		{
			this.lifeSpan -= Time.deltaTime; //Decrease the time variable
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject, 0f); //Otherwise, if time is less then 0, destroy the alarm clock
		}
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000020EC File Offset: 0x000004EC
	private void Alarm()
	{
		this.rang = true;
		if (this.baldi.isActiveAndEnabled) this.baldi.Hear(base.transform.position, 8f); //Baldi is told to go to this location, with a priority of 10(above most sounds)
		this.audioDevice.clip = this.ring;
		this.audioDevice.loop = false; // Tells the audio not to loop
		this.audioDevice.Play(); //Play the audio
	}

	// Token: 0x04000001 RID: 1
	public float timeLeft;

	// Token: 0x04000002 RID: 2
	private float lifeSpan;

	// Token: 0x04000003 RID: 3
	private bool rang;

	// Token: 0x04000004 RID: 4
	public BaldiScript baldi;

	// Token: 0x04000005 RID: 5
	public AudioClip ring;

	// Token: 0x04000006 RID: 6
	public AudioSource audioDevice;
}
