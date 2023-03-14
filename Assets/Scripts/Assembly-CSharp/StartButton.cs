using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000D2 RID: 210
public class StartButton : MonoBehaviour
{
	// Token: 0x060009E3 RID: 2531 RVA: 0x00026710 File Offset: 0x00024B10
	public void StartGame()
	{
		if (this.currentMode == StartButton.Mode.Story)
		{
			PlayerPrefs.SetString("CurrentMode", "story");
		}
		else
		{
			PlayerPrefs.SetString("CurrentMode", "endless");
		}
		SceneManager.LoadSceneAsync("School");
	}

	// Token: 0x04000715 RID: 1813
	public StartButton.Mode currentMode;

	// Token: 0x020000D3 RID: 211
	public enum Mode
	{
		// Token: 0x04000717 RID: 1815
		Story,
		// Token: 0x04000718 RID: 1816
		Endless
	}
}
