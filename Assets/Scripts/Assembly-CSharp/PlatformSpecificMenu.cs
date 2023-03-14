using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class PlatformSpecificMenu : MonoBehaviour
{
	// Token: 0x06000066 RID: 102 RVA: 0x00003953 File Offset: 0x00001D53
	private void Start()
	{
		this.pC.SetActive(true);
	}

	// Token: 0x04000073 RID: 115
	public GameObject pC;

	// Token: 0x04000074 RID: 116
	public GameObject mobile;
}
