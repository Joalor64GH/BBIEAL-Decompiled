using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class DefaultSettingsScript : MonoBehaviour
{
	// Token: 0x06000028 RID: 40 RVA: 0x0000290E File Offset: 0x00000D0E
	private void Start()
	{
		if (!PlayerPrefs.HasKey("OptionsSet"))
		{
			this.options.SetActive(true);
			base.StartCoroutine(this.CloseOptions());
			this.canvas.enabled = false;
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002944 File Offset: 0x00000D44
	public IEnumerator CloseOptions()
	{
		yield return new WaitForEndOfFrame();
		this.canvas.enabled = true;
		this.options.SetActive(false);
		yield break;
	}

	// Token: 0x04000029 RID: 41
	public GameObject options;

	// Token: 0x0400002A RID: 42
	public Canvas canvas;
}
