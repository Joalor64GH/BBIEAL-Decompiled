using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000011 RID: 17
public class HuggingScript : MonoBehaviour
{
	// Token: 0x0600003C RID: 60 RVA: 0x0000317D File Offset: 0x0000157D
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600003D RID: 61 RVA: 0x0000318B File Offset: 0x0000158B
	private void Update()
	{
		if (this.failSave > 0f)
		{
			this.failSave -= Time.deltaTime;
		}
		else
		{
			this.inBsoda = false;
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000031BB File Offset: 0x000015BB
	private void FixedUpdate()
	{
		if (this.inBsoda)
		{
			this.rb.velocity = this.otherVelocity;
		}
	}

	// Token: 0x0600003F RID: 63 RVA: 0x000031DC File Offset: 0x000015DC
	private void OnTriggerStay(Collider other)
	{
		if (other.transform.name == "1st Prize")
		{
			this.inBsoda = true;
			this.otherVelocity = this.rb.velocity * 0.1f + other.GetComponent<NavMeshAgent>().velocity;
			this.failSave = 1f;
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003240 File Offset: 0x00001640
	private void OnTriggerExit()
	{
		this.inBsoda = false;
	}

	// Token: 0x0400004E RID: 78
	private Rigidbody rb;

	// Token: 0x0400004F RID: 79
	private Vector3 otherVelocity;

	// Token: 0x04000050 RID: 80
	public bool inBsoda;

	// Token: 0x04000051 RID: 81
	private float failSave;
}
