using System;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class SwingingDoorScript : MonoBehaviour
{
	// Token: 0x06000945 RID: 2373 RVA: 0x0002147A File Offset: 0x0001F87A
	private void Start()
	{
		this.myAudio = base.GetComponent<AudioSource>();
		this.bDoorLocked = true;
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x00021490 File Offset: 0x0001F890
	private void Update()
	{
		if (!this.requirementMet & this.gc.notebooks >= 2)
		{
			this.requirementMet = true;
			this.UnlockDoor();
		}
		if (this.openTime > 0f)
		{
			this.openTime -= 1f * Time.deltaTime;
		}
		if (this.lockTime > 0f)
		{
			this.lockTime -= Time.deltaTime;
		}
		else if (this.bDoorLocked & this.requirementMet)
		{
			this.UnlockDoor();
		}
		if (this.openTime <= 0f & this.bDoorOpen & !this.bDoorLocked)
		{
			this.bDoorOpen = false;
			this.inside.material = this.closed;
			this.outside.material = this.closed;
		}
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x0002158C File Offset: 0x0001F98C
	private void OnTriggerStay(Collider other)
	{
		if (!this.bDoorLocked)
		{
			this.bDoorOpen = true;
			this.inside.material = this.open;
			this.outside.material = this.open;
			this.openTime = 2f;
		}
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x000215D8 File Offset: 0x0001F9D8
	private void OnTriggerEnter(Collider other)
	{
		if (!(this.gc.notebooks < 2 & other.tag == "Player"))
		{
			if (!this.bDoorLocked)
			{
				this.myAudio.PlayOneShot(this.doorOpen, 1f);
				if (other.tag == "Player" && this.baldi.isActiveAndEnabled)
				{
					this.baldi.Hear(base.transform.position, 1f);
				}
			}
		}
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x00021670 File Offset: 0x0001FA70
	public void LockDoor(float time)
	{
		this.barrier.enabled = true;
		this.obstacle.SetActive(true);
		this.bDoorLocked = true;
		this.lockTime = time;
		this.inside.material = this.locked;
		this.outside.material = this.locked;
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x000216C8 File Offset: 0x0001FAC8
	private void UnlockDoor()
	{
		this.barrier.enabled = false;
		this.obstacle.SetActive(false);
		this.bDoorLocked = false;
		this.inside.material = this.closed;
		this.outside.material = this.closed;
	}

	// Token: 0x040005D9 RID: 1497
	public GameControllerScript gc;

	// Token: 0x040005DA RID: 1498
	public BaldiScript baldi;

	// Token: 0x040005DB RID: 1499
	public MeshCollider barrier;

	// Token: 0x040005DC RID: 1500
	public GameObject obstacle;

	// Token: 0x040005DD RID: 1501
	public MeshCollider trigger;

	// Token: 0x040005DE RID: 1502
	public MeshRenderer inside;

	// Token: 0x040005DF RID: 1503
	public MeshRenderer outside;

	// Token: 0x040005E0 RID: 1504
	public Material closed;

	// Token: 0x040005E1 RID: 1505
	public Material open;

	// Token: 0x040005E2 RID: 1506
	public Material locked;

	// Token: 0x040005E3 RID: 1507
	public AudioClip doorOpen;

	// Token: 0x040005E4 RID: 1508
	public AudioClip baldiDoor;

	// Token: 0x040005E5 RID: 1509
	private float openTime;

	// Token: 0x040005E6 RID: 1510
	private float lockTime;

	// Token: 0x040005E7 RID: 1511
	public bool bDoorOpen;

	// Token: 0x040005E8 RID: 1512
	public bool bDoorLocked;

	// Token: 0x040005E9 RID: 1513
	private bool requirementMet;

	// Token: 0x040005EA RID: 1514
	private AudioSource myAudio;
}
