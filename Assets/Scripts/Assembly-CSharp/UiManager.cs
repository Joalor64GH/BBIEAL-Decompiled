using System;
using MaterialKit;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DB RID: 219
public class UiManager : MonoBehaviour
{
	// Token: 0x06000A05 RID: 2565 RVA: 0x00026C34 File Offset: 0x00025034
	private void Start()
	{
		int @int = PlayerPrefs.GetInt("UiSize");
		int int2 = PlayerPrefs.GetInt("UiHeight");
		if (@int == 1)
		{
			this.normScaler.referenceResolution = new Vector2(640f, 480f);
		}
		else if (@int == 2)
		{
			this.normScaler.referenceResolution = new Vector2(800f, 600f);
		}
		else if (@int == 3)
		{
			this.normScaler.referenceResolution = new Vector2(900f, 720f);
		}
		else if (@int == 4)
		{
			this.normScaler.referenceResolution = new Vector2(1024f, 720f);
		}
		if (int2 == 1)
		{
			foreach (RectTransform rectTransform in this.transforms)
			{
				rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y + (float)(Screen.height / 8), rectTransform.position.z);
			}
		}
		else if (int2 == 2)
		{
			foreach (RectTransform rectTransform2 in this.transforms)
			{
				rectTransform2.position = new Vector3(rectTransform2.position.x, rectTransform2.position.y + (float)(Screen.height / 4), rectTransform2.position.z);
			}
		}
	}

	// Token: 0x04000735 RID: 1845
	public CanvasScaler normScaler;

	// Token: 0x04000736 RID: 1846
	public DpCanvasScaler dpiScaler;

	// Token: 0x04000737 RID: 1847
	public RectTransform[] transforms;
}
