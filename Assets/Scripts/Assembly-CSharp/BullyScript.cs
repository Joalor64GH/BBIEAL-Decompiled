using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class BullyScript : MonoBehaviour
{
	// Token: 0x06000014 RID: 20 RVA: 0x00002396 File Offset: 0x00000796
	private void Start()
	{
		this.audioDevice = base.GetComponent<AudioSource>(); //Get the Audio Source
		this.waitTime = UnityEngine.Random.Range(60f, 120f); //Set the amount of time before the bully appears again
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000023BC File Offset: 0x000007BC
	private void Update()
	{
		if (this.waitTime > 0f) //Decrease the waittime
		{
			this.waitTime -= Time.deltaTime;
		}
		else if (!this.active)
		{
			this.Activate(); //Activate the Bully
		}
		if (this.active) //If the Bully is on the map
		{
			this.activeTime += Time.deltaTime; //Increase active time
			if (this.activeTime >= 180f & (base.transform.position - this.player.position).magnitude >= 120f) //If the bully has been in the map for a long time and the player is far away
			{
				this.Reset(); //Reset the bully
			}
		}
		if (this.guilt > 0f)
		{
			this.guilt -= Time.deltaTime; //Decrease Bully's guilt
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x0000248C File Offset: 0x0000088C
	private void FixedUpdate()
	{
		Vector3 direction = this.player.position - base.transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + new Vector3(0f, 4f, 0f), direction, out raycastHit, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & (base.transform.position - this.player.position).magnitude <= 30f & this.active)
		{
			if (!this.spoken) // If the bully hasn't already spoken
			{
				int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f)); //Get a random number between 0 and 1
				this.audioDevice.PlayOneShot(this.aud_Taunts[num]); //Say a line in an index using num
				this.spoken = true; //Sets spoken to true, preventing the bully from talking again
			}
			this.guilt = 10f; //Makes the bully guilty for "Bullying in the halls"
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002588 File Offset: 0x00000988
	private void Activate()
	{
		this.wanderer.GetNewTargetHallway(); //Get a hallway position
		base.transform.position = this.wanderTarget.position + new Vector3(0f, 5f, 0f); // Go to the wanderTarget + 5 on the Y axis
		while ((base.transform.position - this.player.position).magnitude < 20f) // While the Bully is close to the player
		{
			this.wanderer.GetNewTargetHallway(); //Get a new target
			base.transform.position = this.wanderTarget.position + new Vector3(0f, 5f, 0f);// Go to the wanderTarget + 5 on the Y axis
        } //This is here to prevent the bully from spawning ontop iof the player
		this.active = true; //Set the bully to active
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002644 File Offset: 0x00000A44
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Player") // If touching the player
		{
			if (this.gc.item[0] == 0 & this.gc.item[1] == 0 & this.gc.item[2] == 0) // If the player has no items
			{
				this.audioDevice.PlayOneShot(this.aud_Denied); // "What, no items? No Items? No passsssss"
			}
			else
			{
				int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 2f)); //Get a random item slot
				while (this.gc.item[num] == 0) //If the selected slot is empty
				{
					num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 2f)); // Choose another slot
				}
				this.gc.LoseItem(num); // Remove the item selected
				int num2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
				this.audioDevice.PlayOneShot(this.aud_Thanks[num2]);
				this.Reset();
			}
		}
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002733 File Offset: 0x00000B33
	private void OnTriggerStay(Collider other)
	{
		if (other.transform.name == "Principal of the Thing" & this.guilt > 0f) //If touching the principal and the bully is guilty
		{
			this.Reset(); //Reset the bully
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002764 File Offset: 0x00000B64
	private void Reset()
	{
		base.transform.position = base.transform.position - new Vector3(0f, 20f, 0f); // Go to X: 0, Y: 20, Z: 20
		this.waitTime = UnityEngine.Random.Range(60f, 120f); //Set the amount of time before the bully appears again
		this.active = false; //Set active to false
		this.activeTime = 0f; //Reset active time
		this.spoken = false; //Reset spoken
	}

	// Token: 0x04000012 RID: 18
	public Transform player;

	// Token: 0x04000013 RID: 19
	public GameControllerScript gc;

	// Token: 0x04000014 RID: 20
	public Renderer bullyRenderer;

	// Token: 0x04000015 RID: 21
	public Transform wanderTarget;

	// Token: 0x04000016 RID: 22
	public AILocationSelectorScript wanderer;

	// Token: 0x04000017 RID: 23
	public float waitTime;

	// Token: 0x04000018 RID: 24
	public float activeTime;

	// Token: 0x04000019 RID: 25
	public float guilt;

	// Token: 0x0400001A RID: 26
	public bool active;

	// Token: 0x0400001B RID: 27
	public bool spoken;

	// Token: 0x0400001C RID: 28
	private AudioSource audioDevice;

	// Token: 0x0400001D RID: 29
	public AudioClip[] aud_Taunts = new AudioClip[2];

	// Token: 0x0400001E RID: 30
	public AudioClip[] aud_Thanks = new AudioClip[2];

	// Token: 0x0400001F RID: 31
	public AudioClip aud_Denied;
}
