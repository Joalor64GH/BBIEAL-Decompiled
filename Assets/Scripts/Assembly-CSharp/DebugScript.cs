using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class DebugScript : MonoBehaviour
{
	// Token: 0x06000025 RID: 37 RVA: 0x000028E6 File Offset: 0x00000CE6
	private void Start()
	{
		if (this.limitFramerate)
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = this.framerate;
		}
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002904 File Offset: 0x00000D04
	private void Update()
	{
	}

	// Token: 0x04000027 RID: 39
	public bool limitFramerate;

	// Token: 0x04000028 RID: 40
	public int framerate;
}
