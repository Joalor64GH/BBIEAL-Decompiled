using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000BF RID: 191
public class ExitTriggerScript : MonoBehaviour
{
	// Token: 0x06000962 RID: 2402 RVA: 0x000219A0 File Offset: 0x0001FDA0
	private void OnTriggerEnter(Collider other)
	{
		if (this.gc.notebooks >= 7 & other.tag == "Player")
		{
			if (this.gc.failedNotebooks >= 7) //If the player got all the problems wrong on all the 7 notebooks
			{
				SceneManager.LoadScene("Secret"); //Go to the secret ending
			}
			else
			{
				SceneManager.LoadScene("Results"); //Go to the win screen
			}
		}
	}

	// Token: 0x040005F6 RID: 1526
	public GameControllerScript gc;
}
