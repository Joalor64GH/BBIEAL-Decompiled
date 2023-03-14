using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class EndlessNotebookScript : MonoBehaviour
{
	// Token: 0x0600002E RID: 46 RVA: 0x00002A8E File Offset: 0x00000E8E
	private void Start()
	{
		this.gc = GameObject.Find("Game Controller").GetComponent<GameControllerScript>(); //Find the game controller object
		this.player = GameObject.Find("Player").GetComponent<Transform>(); //Find the player object
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002ACC File Offset: 0x00000ECC
	private void Update()
	{
		if (Input.GetMouseButtonDown(0)) //If left clicked
		{
			Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit) && (raycastHit.transform.tag == "Notebook" & Vector3.Distance(this.player.position, base.transform.position) < this.openingDistance)) //If it is a notebook
			{
				base.gameObject.SetActive(false); //Disable the object being clicked
				this.gc.CollectNotebook(); //Collect the notebook
				this.learningGame.SetActive(true); //Activate the learning game
			}
		}
	}

	// Token: 0x0400002D RID: 45
	public float openingDistance;

	// Token: 0x0400002E RID: 46
	public GameControllerScript gc;

	// Token: 0x0400002F RID: 47
	public Transform player;

	// Token: 0x04000030 RID: 48
	public GameObject learningGame;

}
