using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class DebugFirstPrizeScript : MonoBehaviour
{
	// Token: 0x06000022 RID: 34 RVA: 0x0000286E File Offset: 0x00000C6E
	private void Start()
	{
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002870 File Offset: 0x00000C70
	private void Update()
	{
		base.transform.position = this.first.position + new Vector3((float)Mathf.RoundToInt(this.first.forward.x), 0f, (float)Mathf.RoundToInt(this.first.forward.z)) * 3f;
	}

	// Token: 0x04000025 RID: 37
	public Transform player;

	// Token: 0x04000026 RID: 38
	public Transform first;
}
