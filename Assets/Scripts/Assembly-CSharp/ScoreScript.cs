using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public class ScoreScript : MonoBehaviour
{
	// Token: 0x06000919 RID: 2329 RVA: 0x00020A40 File Offset: 0x0001EE40
	private void Start()
	{
		if (PlayerPrefs.GetString("CurrentMode") == "endless")
		{
			this.scoreText.SetActive(true);
			this.text.text = "Score:\n" + PlayerPrefs.GetInt("CurrentBooks") + " Notebooks";
		}
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x00020A9B File Offset: 0x0001EE9B
	private void Update()
	{
	}

	// Token: 0x040005A5 RID: 1445
	public GameObject scoreText;

	// Token: 0x040005A6 RID: 1446
	public TMP_Text text;
}
