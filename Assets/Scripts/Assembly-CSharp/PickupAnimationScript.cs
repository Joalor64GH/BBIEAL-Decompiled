using System;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class PickupAnimationScript : MonoBehaviour
{
	// Token: 0x060009C6 RID: 2502 RVA: 0x000255A0 File Offset: 0x000239A0
	private void Start()
	{
		this.itemPosition = base.GetComponent<Transform>();
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x000255AE File Offset: 0x000239AE
	private void Update()
	{
		this.itemPosition.localPosition = new Vector3(0f, Mathf.Sin((float)Time.frameCount * 0.017453292f) / 2f + 1f, 0f);
	}

	// Token: 0x040006DA RID: 1754
	private Transform itemPosition;
}
