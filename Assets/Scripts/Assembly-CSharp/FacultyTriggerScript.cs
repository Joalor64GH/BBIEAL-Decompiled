using System;
using UnityEngine;

// Token: 0x020000CB RID: 203
public class FacultyTriggerScript : MonoBehaviour
{
	// Token: 0x060009B5 RID: 2485 RVA: 0x00024DA0 File Offset: 0x000231A0
	private void Start()
	{
		this.hitBox = base.GetComponent<BoxCollider>();
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x00024DAE File Offset: 0x000231AE
	private void Update()
	{
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x00024DB0 File Offset: 0x000231B0
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player")) //If it is a player
		{
			this.ps.ResetGuilt("faculty", 1f); //Make the player guilty of entering school faculty for 1 second
		}
	}

	// Token: 0x040006AF RID: 1711
	public PlayerScript ps;

	// Token: 0x040006B0 RID: 1712
	private BoxCollider hitBox;
}
