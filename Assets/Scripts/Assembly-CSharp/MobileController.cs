using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class MobileController : MonoBehaviour
{
	// Token: 0x06000052 RID: 82 RVA: 0x000036A9 File Offset: 0x00001AA9
	private void Start()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000053 RID: 83 RVA: 0x000036B7 File Offset: 0x00001AB7
	private void Update()
	{
		if (InputTypeManager.usingTouch)
		{
			if (!this.active)
			{
				this.ActivateMobileControls();
			}
		}
		else if (this.active)
		{
			this.DeactivateMobileControls();
		}
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000036EA File Offset: 0x00001AEA
	private void ActivateMobileControls()
	{
		this.simpleControls.SetActive(true);
		this.active = true;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000036FF File Offset: 0x00001AFF
	private void DeactivateMobileControls()
	{
		this.proControls.SetActive(false);
		this.simpleControls.SetActive(false);
		this.active = false;
	}

	// Token: 0x04000069 RID: 105
	public GameObject simpleControls;

	// Token: 0x0400006A RID: 106
	public GameObject proControls;

	// Token: 0x0400006B RID: 107
	private bool active;
}
