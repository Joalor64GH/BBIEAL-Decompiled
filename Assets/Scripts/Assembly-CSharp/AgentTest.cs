using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000C7 RID: 199
public class AgentTest : MonoBehaviour
{
	// Token: 0x06000999 RID: 2457 RVA: 0x0002431D File Offset: 0x0002271D
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>(); // Define the AI Agent
		this.Wander(); //Start wandering
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x00024331 File Offset: 0x00022731
	private void Update()
	{
		if (this.coolDown > 0f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x0002435C File Offset: 0x0002275C
	private void FixedUpdate()
	{
		Vector3 direction = this.player.position - base.transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, direction, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player") //Check if its the player
		{
			this.db = true;
			this.TargetPlayer(); //Head towards the player
		}
		else
		{
			this.db = false;
			if (this.agent.velocity.magnitude <= 1f & this.coolDown <= 0f)
			{
				this.Wander(); //Just wander
			}
		}
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x0002440D File Offset: 0x0002280D
	private void Wander()
	{
		this.wanderer.GetNewTarget(); //Get a new target on the map
		this.agent.SetDestination(this.wanderTarget.position); //Set its destination to position of the wanderTarget
		this.coolDown = 1f;
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x0002443C File Offset: 0x0002283C
	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position); //Set it's destination to the player
		this.coolDown = 1f;
	}

	// Token: 0x04000676 RID: 1654
	public bool db;

	// Token: 0x04000677 RID: 1655
	public Transform player;

	// Token: 0x04000678 RID: 1656
	public Transform wanderTarget;

	// Token: 0x04000679 RID: 1657
	public AILocationSelectorScript wanderer;

	// Token: 0x0400067A RID: 1658
	public float coolDown;

	// Token: 0x0400067B RID: 1659
	private NavMeshAgent agent;
}
