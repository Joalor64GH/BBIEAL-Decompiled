using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001B RID: 27
public class OptionsManager : MonoBehaviour
{
	// Token: 0x06000061 RID: 97 RVA: 0x000037B4 File Offset: 0x00001BB4
	private void Start()
	{
		if (PlayerPrefs.HasKey("OptionsSet"))
		{
			slider.value = PlayerPrefs.GetFloat("MouseSensitivity");
			if (PlayerPrefs.GetInt("Rumble") == 1)
			{
				rumble.isOn = true;
			}
			else
			{
				rumble.isOn = false;
			}
			if (PlayerPrefs.GetInt("AnalogMove") == 1)
			{
				analog.isOn = true;
			}
			else
			{
				analog.isOn = false;
			}
		}
		else
		{
			PlayerPrefs.SetInt("OptionsSet", 1);
		}
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00003850 File Offset: 0x00001C50
	private void Update()
	{
		PlayerPrefs.SetFloat("MouseSensitivity", slider.value);
		if (rumble.isOn)
		{
			PlayerPrefs.SetInt("Rumble", 1);
		}
		else
		{
			PlayerPrefs.SetInt("Rumble", 0);
		}
		if (analog.isOn)
		{
			PlayerPrefs.SetInt("AnalogMove", 1);
		}
		else
		{
			PlayerPrefs.SetInt("AnalogMove", 0);
		}
	}

	// Token: 0x0400006F RID: 111
	public Slider slider;

	// Token: 0x04000070 RID: 112
	public Toggle rumble;

	// Token: 0x04000071 RID: 113
	public Toggle analog;
}
