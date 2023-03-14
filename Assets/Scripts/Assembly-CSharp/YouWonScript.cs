using System;
using UnityEngine;

// Token: 0x020000D5 RID: 213
public class YouWonScript : MonoBehaviour
{
	// Token: 0x060009E8 RID: 2536 RVA: 0x0002678A File Offset: 0x00024B8A
	private void Start()
	{
		this.delay = 10f;
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x00026797 File Offset: 0x00024B97
	private void Update()
	{
		this.delay -= Time.deltaTime;
		if (this.delay <= 0f)
		{
			Application.Quit();
		}
	}

	// Token: 0x0400071A RID: 1818
	private float delay;
}
