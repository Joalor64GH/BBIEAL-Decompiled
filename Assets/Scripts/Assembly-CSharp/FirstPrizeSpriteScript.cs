using System;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class FirstPrizeSpriteScript : MonoBehaviour
{
	// Token: 0x060009B9 RID: 2489 RVA: 0x00024DF1 File Offset: 0x000231F1
	private void Start()
	{
		this.sprite = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x00024E00 File Offset: 0x00023200
	private void Update()
	{
		this.angleF = Mathf.Atan2(this.cam.position.z - this.body.position.z, this.cam.position.x - this.body.position.x) * 57.29578f;
		if (this.angleF < 0f)
		{
			this.angleF += 360f;
		}
		this.debug = this.body.eulerAngles.y;
		this.angleF += this.body.eulerAngles.y;
		this.angle = Mathf.RoundToInt(this.angleF / 22.5f);
		while (this.angle < 0 || this.angle >= 16)
		{
			this.angle += (int)(-16f * Mathf.Sign((float)this.angle));
		}
		this.sprite.sprite = this.sprites[this.angle];
	}

	// Token: 0x040006B1 RID: 1713
	public float debug;

	// Token: 0x040006B2 RID: 1714
	public int angle;

	// Token: 0x040006B3 RID: 1715
	public float angleF;

	// Token: 0x040006B4 RID: 1716
	private SpriteRenderer sprite;

	// Token: 0x040006B5 RID: 1717
	public Transform cam;

	// Token: 0x040006B6 RID: 1718
	public Transform body;

	// Token: 0x040006B7 RID: 1719
	public Sprite[] sprites = new Sprite[16];
}
