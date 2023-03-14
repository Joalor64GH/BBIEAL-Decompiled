using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class QuarterSpawnScript : MonoBehaviour
{
	// Token: 0x0600006F RID: 111 RVA: 0x00003D0A File Offset: 0x0000210A
	private void Start()
	{
		this.wanderer.QuarterExclusive();
		base.transform.position = this.location.position + Vector3.up * 4f;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00003D41 File Offset: 0x00002141
	private void Update()
	{
	}

	// Token: 0x0400008C RID: 140
	public AILocationSelectorScript wanderer;

	// Token: 0x0400008D RID: 141
	public Transform location;
}
