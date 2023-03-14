using System;
using UnityEngine;

// Token: 0x020000BD RID: 189
public class EntranceScript : MonoBehaviour
{
	// Token: 0x0600095D RID: 2397 RVA: 0x000218F8 File Offset: 0x0001FCF8
	public void Lower()
	{
		base.transform.position = base.transform.position - new Vector3(0f, 10f, 0f);
		if (this.gc.finaleMode)
		{
			this.wall.material = this.map;
		}
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x00021955 File Offset: 0x0001FD55
	public void Raise()
	{
		base.transform.position = base.transform.position + new Vector3(0f, 10f, 0f);
	}

	// Token: 0x040005F3 RID: 1523
	public GameControllerScript gc;

	// Token: 0x040005F4 RID: 1524
	public Material map;

	// Token: 0x040005F5 RID: 1525
	public MeshRenderer wall;
}
