using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000010 RID: 16
public class FirstPrizeScript : MonoBehaviour
{
	// Token: 0x06000033 RID: 51 RVA: 0x00002C17 File Offset: 0x00001017
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
		this.coolDown = 1f;
		this.Wander();
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002C38 File Offset: 0x00001038
	private void Update()
	{
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
		if (this.autoBrakeCool > 0f)
		{
			this.autoBrakeCool -= 1f * Time.deltaTime;
		}
		else
		{
			this.agent.autoBraking = true;
		}
		this.angleDiff = Mathf.DeltaAngle(base.transform.eulerAngles.y, Mathf.Atan2(this.agent.steeringTarget.x - base.transform.position.x, this.agent.steeringTarget.z - base.transform.position.z) * 57.29578f);
		if (this.crazyTime <= 0f)
		{
			if (Mathf.Abs(this.angleDiff) < 5f)
			{
				base.transform.LookAt(new Vector3(this.agent.steeringTarget.x, base.transform.position.y, this.agent.steeringTarget.z));
				this.agent.speed = this.currentSpeed;
			}
			else
			{
				base.transform.Rotate(new Vector3(0f, this.turnSpeed * Mathf.Sign(this.angleDiff) * Time.deltaTime, 0f));
				this.agent.speed = 0f;
			}
		}
		else
		{
			this.agent.speed = 0f;
			base.transform.Rotate(new Vector3(0f, 180f * Time.deltaTime, 0f));
			this.crazyTime -= Time.deltaTime;
		}
		this.motorAudio.pitch = (this.agent.velocity.magnitude + 1f) * Time.timeScale;
		//if (this.prevSpeed - this.agent.velocity.magnitude > 15f)
		//{
		//	this.audioDevice.PlayOneShot(this.audBang);
		//}
		//this.prevSpeed = this.agent.velocity.magnitude;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002E60 File Offset: 0x00001260
	private void FixedUpdate()
	{
		Vector3 direction = this.player.position - base.transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, direction, out raycastHit, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player")
		{
			if (!this.playerSeen && !this.audioDevice.isPlaying)
			{
				int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
				this.audioDevice.PlayOneShot(this.aud_Found[num]);
			}
			this.playerSeen = true;
			this.TargetPlayer();
			this.currentSpeed = this.runSpeed;
		}
		else
		{
			this.currentSpeed = this.normSpeed;
			if (this.playerSeen & this.coolDown <= 0f)
			{
				if (!this.audioDevice.isPlaying)
				{
					int num2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
					this.audioDevice.PlayOneShot(this.aud_Lost[num2]);
				}
				this.playerSeen = false;
				this.Wander();
			}
			else if (this.agent.velocity.magnitude <= 1f & this.coolDown <= 0f & (base.transform.position - this.agent.destination).magnitude < 5f)
			{
				this.Wander();
			}
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00003000 File Offset: 0x00001400
	private void Wander()
	{
		this.wanderer.GetNewTargetHallway();
		this.agent.SetDestination(this.wanderTarget.position);
		this.hugAnnounced = false;
		int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 9f));
		if (!this.audioDevice.isPlaying & num == 0 & this.coolDown <= 0f)
		{
			int num2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
			this.audioDevice.PlayOneShot(this.aud_Random[num2]);
		}
		this.coolDown = 1f;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x000030A7 File Offset: 0x000014A7
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position);
		this.coolDown = 0.5f;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000030CC File Offset: 0x000014CC
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			if (!this.audioDevice.isPlaying & !this.hugAnnounced)
			{
				int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
				this.audioDevice.PlayOneShot(this.aud_Hug[num]);
				this.hugAnnounced = true;
			}
			this.agent.autoBraking = false;
		}
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00003146 File Offset: 0x00001546
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			this.autoBrakeCool = 1f;
		}
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00003168 File Offset: 0x00001568
	public void GoCrazy()
	{
		this.crazyTime = 15f;
	}

	// Token: 0x04000033 RID: 51
	public float debug;

	// Token: 0x04000034 RID: 52
	public float turnSpeed;

	// Token: 0x04000035 RID: 53
	public float str;

	// Token: 0x04000036 RID: 54
	public float angleDiff;

	// Token: 0x04000037 RID: 55
	public float normSpeed;

	// Token: 0x04000038 RID: 56
	public float runSpeed;

	// Token: 0x04000039 RID: 57
	public float currentSpeed;

	// Token: 0x0400003A RID: 58
	public float acceleration;

	// Token: 0x0400003B RID: 59
	public float speed;

	// Token: 0x0400003C RID: 60
	public float autoBrakeCool;

	// Token: 0x0400003D RID: 61
	public float crazyTime;

	// Token: 0x0400003E RID: 62
	public Quaternion targetRotation;

	// Token: 0x0400003F RID: 63
	public float coolDown;

	// Token: 0x04000040 RID: 64
	private float prevSpeed;

	// Token: 0x04000041 RID: 65
	public bool playerSeen;

	// Token: 0x04000042 RID: 66
	public bool hugAnnounced;

	// Token: 0x04000043 RID: 67
	public AILocationSelectorScript wanderer;

	// Token: 0x04000044 RID: 68
	public Transform player;

	// Token: 0x04000045 RID: 69
	public Transform wanderTarget;

	// Token: 0x04000046 RID: 70
	public AudioClip[] aud_Found = new AudioClip[2];

	// Token: 0x04000047 RID: 71
	public AudioClip[] aud_Lost = new AudioClip[2];

	// Token: 0x04000048 RID: 72
	public AudioClip[] aud_Hug = new AudioClip[2];

	// Token: 0x04000049 RID: 73
	public AudioClip[] aud_Random = new AudioClip[2];

	// Token: 0x0400004A RID: 74
	public AudioClip audBang;

	// Token: 0x0400004B RID: 75
	public AudioSource audioDevice;

	// Token: 0x0400004C RID: 76
	public AudioSource motorAudio;

	// Token: 0x0400004D RID: 77
	private NavMeshAgent agent;
}
