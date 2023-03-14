using System;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class NearExitTriggerScript : MonoBehaviour
{
	// Token: 0x06000995 RID: 2453 RVA: 0x00024288 File Offset: 0x00022688
	private void OnTriggerEnter(Collider other)
	{
		if (this.gc.exitsReached < 3 & this.gc.finaleMode & other.tag == "Player")
		{
			this.gc.ExitReached();
			this.es.Lower();
			if (this.gc.baldiScrpt.isActiveAndEnabled) this.gc.baldiScrpt.Hear(base.transform.position, 8f);
		}
	}

	// Token: 0x04000674 RID: 1652
	public GameControllerScript gc;

	// Token: 0x04000675 RID: 1653
	public EntranceScript es;
}
