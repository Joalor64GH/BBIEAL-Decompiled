using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C4 RID: 196
public class MouseSliderScript : MonoBehaviour
{
	// Token: 0x06000992 RID: 2450 RVA: 0x0002422C File Offset: 0x0002262C
	private void Start()
	{
		if (PlayerPrefs.GetFloat("MouseSensitivity") < 100f)
		{
			PlayerPrefs.SetFloat("MouseSensitivity", 200f);
		}
		slider.value = PlayerPrefs.GetFloat("MouseSensitivity");
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x00024266 File Offset: 0x00022666
	private void Update()
	{
		PlayerPrefs.SetFloat("MouseSensitivity", slider.value);
	}

	// Token: 0x04000673 RID: 1651
	public Slider slider;
}
