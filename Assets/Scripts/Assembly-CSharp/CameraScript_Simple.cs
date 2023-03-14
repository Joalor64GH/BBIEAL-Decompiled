using System;
//using Rewired;
using UnityEngine;

// Token: 0x020000B6 RID: 182
public class CameraScript_Simple : MonoBehaviour
{
	// Token: 0x06000931 RID: 2353 RVA: 0x00020FA9 File Offset: 0x0001F3A9
	private void Start()
	{
		//this.playerInput = ReInput.players.GetPlayer(0);
		this.offset = base.transform.position - this.player.transform.position;
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x00020FE4 File Offset: 0x0001F3E4
	private void LateUpdate()
	{
		base.transform.position = this.player.transform.position + this.offset;
		base.transform.rotation = this.player.transform.rotation * Quaternion.Euler(0f, (float)this.lookBehind, 0f);
	}

	// Token: 0x040005BB RID: 1467
	public GameObject player;

	// Token: 0x040005BC RID: 1468
	private int lookBehind;

	// Token: 0x040005BD RID: 1469
	private Vector3 offset;

	// Token: 0x040005BE RID: 1470
	//private Player playerInput;
}
