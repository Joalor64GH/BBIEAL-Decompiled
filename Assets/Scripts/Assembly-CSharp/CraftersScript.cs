using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000CA RID: 202
public class CraftersScript : MonoBehaviour
{
	// Token: 0x060009AE RID: 2478 RVA: 0x000249ED File Offset: 0x00022DED
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>(); // Defines the nav mesh agent
		this.audioDevice = base.GetComponent<AudioSource>(); //Gets the audio source
		this.sprite.SetActive(false); // Set arts and crafters sprite to be invisible
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x00024A08 File Offset: 0x00022E08
	private void Update()
	{
		if (this.forceShowTime > 0f)
		{
			this.forceShowTime -= Time.deltaTime;
		}
		if (this.gettingAngry) //If arts is getting agry
		{
			this.anger += Time.deltaTime; // Increase anger
			if (this.anger >= 1f & !this.angry) //If anger is greater then 1 and arts isn't angry
			{
				this.angry = true; // Get angry
				this.audioDevice.PlayOneShot(this.aud_Intro); // Do the woooosoh sound
				this.spriteImage.sprite = this.angrySprite; // Switch to the angry sprite
			}
		}
		else if (this.anger > 0f) // If anger is greater then 0, decrease.
		{
			this.anger -= Time.deltaTime;
		}
		if (!this.angry) // If not angry
		{
			if (((base.transform.position - this.agent.destination).magnitude <= 20f & (base.transform.position - this.player.position).magnitude >= 60f) || this.forceShowTime > 0f) //If close to the player and force showtime is less then 0
			{
				this.sprite.SetActive(true); // Become visible
			}
			else
			{
				this.sprite.SetActive(false); // Become invisible
			}
		}
		else
		{
			this.agent.speed = this.agent.speed + 60f * Time.deltaTime; // Increase the speed
			this.TargetPlayer(); // Target the player
			if (!this.audioDevice.isPlaying) //If the sound is not already playing
			{
				this.audioDevice.PlayOneShot(this.aud_Loop); //Play the full wooooosh sound
			}
		}
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x00024BAC File Offset: 0x00022FAC
	private void FixedUpdate()
	{
		if (this.gc.notebooks >= 7) // If the player has more then 7 notebooks
		{
			Vector3 direction = this.player.position - base.transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out raycastHit, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & this.craftersRenderer.isVisible & this.sprite.activeSelf) // If Arts is Visible, and active and sees the player
			{
				this.gettingAngry = true; // Start to get angry
			}
			else
			{
				this.gettingAngry = false; // Stop getting angry
			}
		}
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x00024C65 File Offset: 0x00023065
	public void GiveLocation(Vector3 location, bool flee)
	{
		if (!this.angry && this.agent.isActiveAndEnabled)
		{
			this.agent.SetDestination(location);
			if (flee)
			{
				this.forceShowTime = 3f; // Make arts appear in 3 seconds
			}
		}
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x00024CA0 File Offset: 0x000230A0
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position); // Set destination to the player
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x00024CBC File Offset: 0x000230BC
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" & this.angry) // If arts is angry and is touching the player
		{
			this.cc.enabled = false;
			this.player.position = new Vector3(5f, this.player.position.y, 80f); // Teleport the player to X: 5, their current Y position, Z: 80
			this.baldiAgent.Warp(new Vector3(5f, this.baldi.position.y, 125f)); // Teleport Baldi to X: 5, baldi's Y, Z: 125
			this.player.LookAt(new Vector3(this.baldi.position.x, this.player.position.y, this.baldi.position.z)); // Make the player look at baldi
			this.cc.enabled = true;
			this.gc.DespawnCrafters(); // Despawn Arts And Crafters
		}
	}

	// Token: 0x0400069D RID: 1693
	public bool db;

	// Token: 0x0400069E RID: 1694
	public bool angry;

	// Token: 0x0400069F RID: 1695
	public bool gettingAngry;

	// Token: 0x040006A0 RID: 1696
	public float anger;

	// Token: 0x040006A1 RID: 1697
	private float forceShowTime;

	// Token: 0x040006A2 RID: 1698
	public Transform player;

	public CharacterController cc;

	// Token: 0x040006A3 RID: 1699
	public Transform playerCamera;

	// Token: 0x040006A4 RID: 1700
	public Transform baldi;

	// Token: 0x040006A5 RID: 1701
	public NavMeshAgent baldiAgent;

	// Token: 0x040006A6 RID: 1702
	public GameObject sprite;

	// Token: 0x040006A7 RID: 1703
	public GameControllerScript gc;

	// Token: 0x040006A8 RID: 1704
	[SerializeField]
	private NavMeshAgent agent;

	// Token: 0x040006A9 RID: 1705
	public Renderer craftersRenderer;

	// Token: 0x040006AA RID: 1706
	public SpriteRenderer spriteImage;

	// Token: 0x040006AB RID: 1707
	public Sprite angrySprite;

	// Token: 0x040006AC RID: 1708
	private AudioSource audioDevice;

	// Token: 0x040006AD RID: 1709
	public AudioClip aud_Intro;

	// Token: 0x040006AE RID: 1710
	public AudioClip aud_Loop;
}
