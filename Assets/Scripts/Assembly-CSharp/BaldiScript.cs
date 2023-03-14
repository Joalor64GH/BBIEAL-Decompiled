using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000C9 RID: 201
public class BaldiScript : MonoBehaviour
{
	// Token: 0x060009A3 RID: 2467 RVA: 0x00024564 File Offset: 0x00022964
	private void Start()
	{
		this.baldiAudio = base.GetComponent<AudioSource>(); //Get The Baldi Audio Source(Used mostly for the slap sound)
		this.agent = base.GetComponent<NavMeshAgent>(); //Get the Nav Mesh Agent
		this.timeToMove = this.baseTime; //Sets timeToMove to baseTime
		this.Wander(); //Start wandering
		if (PlayerPrefs.GetInt("Rumble") == 1)
		{
			this.rumble = true;
		}
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x000245C4 File Offset: 0x000229C4
	private void Update()
	{
		if (this.timeToMove > 0f) //If timeToMove is greater then 0, decrease it
		{
			this.timeToMove -= 1f * Time.deltaTime;
		}
		else
		{
			this.Move(); //Start moving
		}
		if (this.coolDown > 0f) //If coolDown is greater then 0, decrease it
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
		if (this.baldiTempAnger > 0f) //Slowly decrease Baldi's temporary anger over time.
		{
			this.baldiTempAnger -= 0.02f * Time.deltaTime;
		}
		else
		{
			this.baldiTempAnger = 0f; //Cap its lowest value at 0
		}
		if (this.antiHearingTime > 0f) //Decrease antiHearingTime, then when it runs out stop the effects of the antiHearing tape
		{
			this.antiHearingTime -= Time.deltaTime;
		}
		else
		{
			this.antiHearing = false;
		}
		if (this.endless) //Only activate if the player is playing on endless mode
		{
			if (this.timeToAnger > 0f) //Decrease the timeToAnger
			{
				this.timeToAnger -= 1f * Time.deltaTime;
			}
			else
			{
				this.timeToAnger = this.angerFrequency; //Set timeToAnger to angerFrequency
				this.GetAngry(this.angerRate); //Get angry based on angerRate
				this.angerRate += this.angerRateRate; //Increase angerRate for next time
			}
		}
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x000246F8 File Offset: 0x00022AF8
	private void FixedUpdate()
	{
		if (this.moveFrames > 0f) //Move for a certain amount of frames, and then stop moving.(Ruler slapping)
		{
			this.moveFrames -= 1f;
			this.agent.speed = this.speed;
		}
		else
		{
			this.agent.speed = 0f;
		}
		Vector3 direction = this.player.position - base.transform.position; 
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out raycastHit, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player") //Create a raycast, if the raycast hits the player, Baldi can see the player
		{
			this.db = true;
			this.TargetPlayer(); //Start attacking the player
		}
		else
		{
			this.db = false;
		}
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x000247D0 File Offset: 0x00022BD0
	private void Wander()
	{
		this.wanderer.GetNewTarget(); //Get a new location
		this.agent.SetDestination(this.wanderTarget.position); //Head towards the position of the wanderTarget object
		this.coolDown = 1f; //Set the cooldown
		this.currentPriority = 0f;
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0002480A File Offset: 0x00022C0A
	public void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position); //Target the player
		this.coolDown = 1f;
		this.currentPriority = 0f;
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x0002483C File Offset: 0x00022C3C
	private void Move()
	{
		if (base.transform.position == this.previous & this.coolDown < 0f) // If Baldi reached his destination, start wandering
		{
			this.Wander();
		}
		this.moveFrames = 10f;
		this.timeToMove = this.baldiWait - this.baldiTempAnger;
		this.previous = base.transform.position; // Set previous to Baldi's current location
		this.baldiAudio.PlayOneShot(this.slap); //Play the slap sound
		this.baldiAnimator.SetTrigger("slap"); // Play the slap animation
		if (this.rumble)
		{
			float num = Vector3.Distance(base.transform.position, this.player.position);
			if (num < this.vibrationDistance)
			{
				float motorLevel = 1f - num / this.vibrationDistance;
			}
		}
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x00024930 File Offset: 0x00022D30
	public void GetAngry(float value)
	{
		this.baldiAnger += value; // Increase Baldi's anger by the value provided
		if (this.baldiAnger < 0.5f) //Cap Baldi anger at a minimum of 0.5
		{
			this.baldiAnger = 0.5f;
		}
		this.baldiWait = -3f * this.baldiAnger / (this.baldiAnger + 2f / this.baldiSpeedScale) + 3f; //Some formula I don't understand.
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x00024992 File Offset: 0x00022D92
	public void GetTempAngry(float value)
	{
		this.baldiTempAnger += value; //Increase Baldi's Temporary Anger
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x000249A2 File Offset: 0x00022DA2
	public void Hear(Vector3 soundLocation, float priority)
	{
		if (!this.antiHearing && priority >= this.currentPriority) //If anti-hearing is not active and the priority is greater then the priority of the current sound
		{
			this.agent.SetDestination(soundLocation); //Go to that sound
			this.currentPriority = priority; //Set the current priority to the priority
		}
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x000249CF File Offset: 0x00022DCF
	public void ActivateAntiHearing(float t)
	{
		this.Wander(); //Start wandering
		this.antiHearing = true; //Set the antihearing variable to true for other scripts
		this.antiHearingTime = t; //Set the time the tape's effect on baldi will last
	}

	// Token: 0x0400067F RID: 1663
	public bool db;

	// Token: 0x04000680 RID: 1664
	public float baseTime;

	// Token: 0x04000681 RID: 1665
	public float speed;

	// Token: 0x04000682 RID: 1666
	public float timeToMove;

	// Token: 0x04000683 RID: 1667
	public float baldiAnger;

	// Token: 0x04000684 RID: 1668
	public float baldiTempAnger;

	// Token: 0x04000685 RID: 1669
	public float baldiWait;

	// Token: 0x04000686 RID: 1670
	public float baldiSpeedScale;

	// Token: 0x04000687 RID: 1671
	private float moveFrames;

	// Token: 0x04000688 RID: 1672
	private float currentPriority;

	// Token: 0x04000689 RID: 1673
	public bool antiHearing;

	// Token: 0x0400068A RID: 1674
	public float antiHearingTime;

	// Token: 0x0400068B RID: 1675
	public float vibrationDistance;

	// Token: 0x0400068C RID: 1676
	public float angerRate;

	// Token: 0x0400068D RID: 1677
	public float angerRateRate;

	// Token: 0x0400068E RID: 1678
	public float angerFrequency;

	// Token: 0x0400068F RID: 1679
	public float timeToAnger;

	// Token: 0x04000690 RID: 1680
	public bool endless;

	// Token: 0x04000691 RID: 1681
	public Transform player;

	// Token: 0x04000692 RID: 1682
	public Transform wanderTarget;

	// Token: 0x04000693 RID: 1683
	public AILocationSelectorScript wanderer;

	// Token: 0x04000694 RID: 1684
	private AudioSource baldiAudio;

	// Token: 0x04000695 RID: 1685
	public AudioClip slap;

	// Token: 0x04000696 RID: 1686
	public AudioClip[] speech = new AudioClip[3];

	// Token: 0x04000697 RID: 1687
	public Animator baldiAnimator;

	// Token: 0x04000698 RID: 1688
	public float coolDown;

	// Token: 0x04000699 RID: 1689
	private Vector3 previous;

	// Token: 0x0400069A RID: 1690
	private bool rumble;

	// Token: 0x0400069B RID: 1691
	private NavMeshAgent agent;

}
