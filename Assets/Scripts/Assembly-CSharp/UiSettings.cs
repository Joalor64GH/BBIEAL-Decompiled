using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DC RID: 220
public class UiSettings : MonoBehaviour
{
	// Token: 0x06000A07 RID: 2567 RVA: 0x00026DD8 File Offset: 0x000251D8
	public void UpdateState()
	{
		if (this.sAuto.isOn)
		{
			PlayerPrefs.SetInt("UiSize", 0);
		}
		else if (this.sXLarge.isOn)
		{
			PlayerPrefs.SetInt("UiSize", 1);
		}
		else if (this.sLarge.isOn)
		{
			PlayerPrefs.SetInt("UiSize", 2);
		}
		else if (this.sMed.isOn)
		{
			PlayerPrefs.SetInt("UiSize", 3);
		}
		else if (this.sSmall.isOn)
		{
			PlayerPrefs.SetInt("UiSize", 4);
		}
		if (this.hLow.isOn)
		{
			PlayerPrefs.SetInt("UiHeight", 0);
		}
		else if (this.hMed.isOn)
		{
			PlayerPrefs.SetInt("UiHeight", 1);
		}
		else if (this.hHigh.isOn)
		{
			PlayerPrefs.SetInt("UiHeight", 2);
		}
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x00026EDC File Offset: 0x000252DC
	public void RestoreState()
	{
		this.size = PlayerPrefs.GetInt("UiSize");
		this.height = PlayerPrefs.GetInt("UiHeight");
		if (this.size == 0)
		{
			this.sAuto.isOn = true;
		}
		else if (this.size == 1)
		{
			this.sXLarge.isOn = true;
		}
		else if (this.size == 2)
		{
			this.sLarge.isOn = true;
		}
		else if (this.size == 3)
		{
			this.sMed.isOn = true;
		}
		else if (this.size == 4)
		{
			this.sSmall.isOn = true;
		}
		if (this.height == 0)
		{
			this.hLow.isOn = true;
		}
		else if (this.height == 1)
		{
			this.hMed.isOn = true;
		}
		else if (this.height == 2)
		{
			this.hHigh.isOn = true;
		}
	}

	// Token: 0x04000738 RID: 1848
	public Toggle sAuto;

	// Token: 0x04000739 RID: 1849
	public Toggle sXLarge;

	// Token: 0x0400073A RID: 1850
	public Toggle sLarge;

	// Token: 0x0400073B RID: 1851
	public Toggle sMed;

	// Token: 0x0400073C RID: 1852
	public Toggle sSmall;

	// Token: 0x0400073D RID: 1853
	public Toggle hLow;

	// Token: 0x0400073E RID: 1854
	public Toggle hMed;

	// Token: 0x0400073F RID: 1855
	public Toggle hHigh;

	// Token: 0x04000740 RID: 1856
	private int size;

	// Token: 0x04000741 RID: 1857
	private int height;
}
