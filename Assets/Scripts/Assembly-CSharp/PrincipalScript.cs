using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000CD RID: 205
public class PrincipalScript : MonoBehaviour
{
	// Token: 0x060009BC RID: 2492 RVA: 0x00024F6D File Offset: 0x0002336D
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>(); //Get the agent
		this.audioQueue = base.GetComponent<AudioQueueScript>();
		this.audioDevice = base.GetComponent<AudioSource>();
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x00024F94 File Offset: 0x00023394
	private void Update()
	{
		if (this.seesRuleBreak)
		{
			this.timeSeenRuleBreak += 1f * Time.deltaTime;
			if ((double)this.timeSeenRuleBreak >= 0.5 & !this.angry) // If the principal sees the player break a rule for more then 1/2 of a second
			{
				this.angry = true;
				this.seesRuleBreak = false;
				this.timeSeenRuleBreak = 0f;
				this.TargetPlayer(); //Target the player
				this.CorrectPlayer();
			}
		}
		else
		{
			this.timeSeenRuleBreak = 0f;
		}
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x00025048 File Offset: 0x00023448
	private void FixedUpdate()
	{
		if (!this.angry) // If the principal isn't angry
		{
			this.aim = this.player.position - base.transform.position; // If he sees the player and the player has guilt
			if (Physics.Raycast(base.transform.position, this.aim, out this.hit, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & this.hit.transform.tag == "Player" & this.playerScript.guilt > 0f & !this.inOffice & !this.angry)
			{
				this.seesRuleBreak = true;
			}
			else
			{
				this.seesRuleBreak = false;
				if (this.agent.velocity.magnitude <= 1f & this.coolDown <= 0f)
				{
					this.Wander(); // Start wandering again
				}
			}
			this.aim = this.bully.position - base.transform.position;
			if (Physics.Raycast(base.transform.position, this.aim, out this.hit, float.PositiveInfinity, 769) & this.hit.transform.name == "Its a Bully" & this.bullyScript.guilt > 0f & !this.inOffice & !this.angry)
			{
				this.TargetBully();
			}
		}
		else
		{
			this.TargetPlayer(); // If the principal is angry, target the player
		}
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x000251DC File Offset: 0x000235DC
	private void Wander()
	{
		this.playerScript.principalBugFixer = 1;
		this.wanderer.GetNewTarget();
		this.agent.SetDestination(this.wanderTarget.position);
		if (this.agent.isStopped)
		{
			this.agent.isStopped = false;
		}
		this.coolDown = 1f;
		if (UnityEngine.Random.Range(0f, 10f) <= 1f)
		{
			this.quietAudioDevice.PlayOneShot(this.aud_Whistle);
		}
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x00025268 File Offset: 0x00023668
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position);
		this.coolDown = 1f;
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0002528C File Offset: 0x0002368C
	private void TargetBully()
	{
		if (!this.bullySeen)
		{
			this.agent.SetDestination(this.bully.position);
			this.audioQueue.QueueAudio(this.audNoBullying);
			this.bullySeen = true;
		}
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x000252C8 File Offset: 0x000236C8
	private void CorrectPlayer()
	{
		this.audioQueue.ClearAudioQueue();
		if (this.playerScript.guiltType == "faculty")
		{
			this.audioQueue.QueueAudio(this.audNoFaculty);
		}
		else if (this.playerScript.guiltType == "running")
		{
			this.audioQueue.QueueAudio(this.audNoRunning);
		}
		else if (this.playerScript.guiltType == "drink")
		{
			this.audioQueue.QueueAudio(this.audNoDrinking);
		}
		else if (this.playerScript.guiltType == "escape")
		{
			this.audioQueue.QueueAudio(this.audNoEscaping);
		}
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x0002539C File Offset: 0x0002379C
	private void OnTriggerStay(Collider other)
	{
		if (other.name == "Office Trigger")
		{
			this.inOffice = true;
		}
		if (other.tag == "Player" & this.angry & !this.inOffice)
		{
			this.inOffice = true;
			this.playerScript.principalBugFixer = 0;
			this.agent.Warp(new Vector3(10f, 0f, 170f)); //Teleport the principal to the office
			this.agent.isStopped = true; //Stop the principal from moving
			this.cc.enabled = false;
			other.transform.position = new Vector3(10f, 4f, 160f); // Teleport the player to the office
			other.transform.LookAt(new Vector3(base.transform.position.x, other.transform.position.y, base.transform.position.z)); // Get the plaer to look at the principal
			this.cc.enabled = true;
			this.audioQueue.QueueAudio(this.aud_Delay);
			this.audioQueue.QueueAudio(this.audTimes[this.detentions]); //Play the detention time sound
			this.audioQueue.QueueAudio(this.audDetention);
			int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 2f));
			this.audioQueue.QueueAudio(this.audScolds[num]); // Say one of the other lines
			this.officeDoor.LockDoor((float)this.lockTime[this.detentions]); // Lock the door
			if (this.baldiScript.isActiveAndEnabled) this.baldiScript.Hear(base.transform.position, 8f);
			this.coolDown = 5f;
			this.angry = false;
			this.detentions++;
			if (this.detentions > 4) // Prevent detention number from going above 4
			{
				this.detentions = 4;
			}
		}
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x0002555E File Offset: 0x0002395E
	private void OnTriggerExit(Collider other)
	{
		if (other.name == "Office Trigger")
		{
			this.inOffice = false;
		}
		if (other.name == "Its a Bully")
		{
			this.bullySeen = false;
		}
	}

	// Token: 0x040006B8 RID: 1720
	public bool seesRuleBreak;

	// Token: 0x040006B9 RID: 1721
	public Transform player;

	// Token: 0x040006BA RID: 1722
	public Transform bully;

	// Token: 0x040006BB RID: 1723
	public bool bullySeen;

	// Token: 0x040006BC RID: 1724
	public PlayerScript playerScript;

	// Token: 0x040006BD RID: 1725
	public BullyScript bullyScript;

	// Token: 0x040006BE RID: 1726
	public BaldiScript baldiScript;

	// Token: 0x040006BF RID: 1727
	public Transform wanderTarget;

	// Token: 0x040006C0 RID: 1728
	public AILocationSelectorScript wanderer;

	// Token: 0x040006C1 RID: 1729
	public DoorScript officeDoor;

	// Token: 0x040006C2 RID: 1730
	public float coolDown;

	// Token: 0x040006C3 RID: 1731
	public float timeSeenRuleBreak;

	// Token: 0x040006C4 RID: 1732
	public bool angry;

	// Token: 0x040006C5 RID: 1733
	public bool inOffice;

	// Token: 0x040006C6 RID: 1734
	private int detentions;

	// Token: 0x040006C7 RID: 1735
	private int[] lockTime = new int[]
	{
		15,
		30,
		45,
		60,
		99
	};

	// Token: 0x040006C8 RID: 1736
	public AudioClip[] audTimes = new AudioClip[5];

	// Token: 0x040006C9 RID: 1737
	public AudioClip[] audScolds = new AudioClip[3];

	// Token: 0x040006CA RID: 1738
	public AudioClip audDetention;

	// Token: 0x040006CB RID: 1739
	public AudioClip audNoDrinking;

	// Token: 0x040006CC RID: 1740
	public AudioClip audNoBullying;

	// Token: 0x040006CD RID: 1741
	public AudioClip audNoFaculty;

	// Token: 0x040006CE RID: 1742
	public AudioClip audNoLockers;

	// Token: 0x040006CF RID: 1743
	public AudioClip audNoRunning;

	// Token: 0x040006D0 RID: 1744
	public AudioClip audNoStabbing;

	// Token: 0x040006D1 RID: 1745
	public AudioClip audNoEscaping;

	// Token: 0x040006D2 RID: 1746
	public AudioClip aud_Whistle;

	// Token: 0x040006D3 RID: 1747
	public AudioClip aud_Delay;

	// Token: 0x040006D4 RID: 1748
	private NavMeshAgent agent;

	// Token: 0x040006D5 RID: 1749
	private AudioQueueScript audioQueue;

	// Token: 0x040006D6 RID: 1750
	private AudioSource audioDevice;

	// Token: 0x040006D7 RID: 1751
	public AudioSource quietAudioDevice;

	// Token: 0x040006D8 RID: 1752
	private RaycastHit hit;

	// Token: 0x040006D9 RID: 1753
	private Vector3 aim;

	public CharacterController cc;
}
