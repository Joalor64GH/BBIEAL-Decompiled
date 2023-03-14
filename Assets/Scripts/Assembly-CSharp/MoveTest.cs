using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class MoveTest : MonoBehaviour
{
	// Token: 0x0600005C RID: 92 RVA: 0x00003764 File Offset: 0x00001B64
	private void Start()
	{
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00003766 File Offset: 0x00001B66
	private void Update()
	{
		base.transform.position = base.transform.position + new Vector3(0.1f, 0f, 0f);
	}
}
