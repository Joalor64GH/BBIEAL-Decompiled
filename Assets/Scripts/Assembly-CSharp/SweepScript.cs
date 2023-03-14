using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000D7 RID: 215
public class SweepScript : MonoBehaviour
{
	// Token: 0x060009ED RID: 2541 RVA: 0x000267DC File Offset: 0x00024BDC
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
		this.audioDevice = base.GetComponent<AudioSource>();
		this.origin = base.transform.position;
		this.waitTime = UnityEngine.Random.Range(120f, 180f);
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x0002681C File Offset: 0x00024C1C
	private void Update()
	{
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
		if (this.waitTime > 0f)
		{
			this.waitTime -= Time.deltaTime;
		}
		else if (!this.active)
		{
			this.active = true;
			this.wanders = 0;
			this.Wander(); // Start wandering
			this.audioDevice.PlayOneShot(this.aud_Intro); // "Looks like its sweeping time!"
		}
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x000268A8 File Offset: 0x00024CA8
	private void FixedUpdate()
	{
		if ((double)this.agent.velocity.magnitude <= 0.1 & this.coolDown <= 0f & this.wanders < 5 & this.active) // If Gotta Sweep has roamed around the school 5 times
		{
			this.Wander(); // Wander
		}
		else if (this.wanders >= 5)
		{
			this.GoHome(); // Go back to the closet
		}
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x0002691E File Offset: 0x00024D1E
	private void Wander()
	{
		this.wanderer.GetNewTargetHallway();
		this.agent.SetDestination(this.wanderTarget.position);
		this.coolDown = 1f;
		this.wanders++;
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x0002695B File Offset: 0x00024D5B
	private void GoHome()
	{
		this.agent.SetDestination(this.origin);
		this.waitTime = UnityEngine.Random.Range(120f, 180f);
		this.wanders = 0;
		this.active = false;
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x00026992 File Offset: 0x00024D92
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "NPC" || other.tag == "Player")
		{
			this.audioDevice.PlayOneShot(this.aud_Sweep);
		}
	}

	// Token: 0x0400071B RID: 1819
	public Transform wanderTarget;

	// Token: 0x0400071C RID: 1820
	public AILocationSelectorScript wanderer;

	// Token: 0x0400071D RID: 1821
	public float coolDown;

	// Token: 0x0400071E RID: 1822
	public float waitTime;

	// Token: 0x0400071F RID: 1823
	public int wanders;

	// Token: 0x04000720 RID: 1824
	public bool active;

	// Token: 0x04000721 RID: 1825
	private Vector3 origin;

	// Token: 0x04000722 RID: 1826
	public AudioClip aud_Sweep;

	// Token: 0x04000723 RID: 1827
	public AudioClip aud_Intro;

	// Token: 0x04000724 RID: 1828
	private NavMeshAgent agent;

	// Token: 0x04000725 RID: 1829
	private AudioSource audioDevice;
}
