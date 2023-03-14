using System;
using UnityEngine;

// Token: 0x020000B3 RID: 179
public class Billboard : MonoBehaviour
{
	// Token: 0x06000927 RID: 2343 RVA: 0x00020BD5 File Offset: 0x0001EFD5
	private void Start()
	{
		this.m_Camera = Camera.main;
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x00020BE2 File Offset: 0x0001EFE2
	private void LateUpdate()
	{
		base.transform.LookAt(base.transform.position + this.m_Camera.transform.rotation * Vector3.forward); // Look towards the player
	}

	// Token: 0x040005AC RID: 1452
	private Camera m_Camera;
}
