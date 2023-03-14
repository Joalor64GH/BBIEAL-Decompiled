using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class DarkDoorScript : MonoBehaviour
{
	// Token: 0x06000020 RID: 32 RVA: 0x000027FC File Offset: 0x00000BFC
	private void Update()
	{
		if (this.door.bDoorOpen)
		{
			this.mesh.material = this.lightDoo60;
		}
		else if (this.door.bDoorLocked)
		{
			this.mesh.material = this.lightDooLock;
		}
		else
		{
			this.mesh.material = this.lightDoo0;
		}
	}

	// Token: 0x04000020 RID: 32
	public SwingingDoorScript door;

	// Token: 0x04000021 RID: 33
	public Material lightDoo0;

	// Token: 0x04000022 RID: 34
	public Material lightDoo60;

	// Token: 0x04000023 RID: 35
	public Material lightDooLock;

	// Token: 0x04000024 RID: 36
	public MeshRenderer mesh;
}
