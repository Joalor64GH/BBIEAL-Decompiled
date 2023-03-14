using System;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public class CraftersTriggerScript : MonoBehaviour
{
	// Token: 0x06000937 RID: 2359 RVA: 0x000210EB File Offset: 0x0001F4EB
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.crafters.GiveLocation(this.goTarget.position, false);
		}
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x00021119 File Offset: 0x0001F519
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			this.crafters.GiveLocation(this.fleeTarget.position, true);
		}
	}

	// Token: 0x040005C0 RID: 1472
	public Transform goTarget;

	// Token: 0x040005C1 RID: 1473
	public Transform fleeTarget;

	// Token: 0x040005C2 RID: 1474
	public CraftersScript crafters;
}
