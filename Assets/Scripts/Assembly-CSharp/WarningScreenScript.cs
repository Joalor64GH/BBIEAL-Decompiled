using System;
//using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000D4 RID: 212
public class WarningScreenScript : MonoBehaviour
{
	// Token: 0x060009E5 RID: 2533 RVA: 0x00026753 File Offset: 0x00024B53
	private void Start()
	{
		//this.player = ReInput.players.GetPlayer(0);
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x00026766 File Offset: 0x00024B66
	private void Update()
	{
		if (Input.anyKeyDown)
		{
			SceneManager.LoadScene("MainMenu");
		}
	}

	// Token: 0x04000719 RID: 1817
	//public Player player;
}
