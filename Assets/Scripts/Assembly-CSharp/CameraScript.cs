using System;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public class CameraScript : MonoBehaviour
{
	// Token: 0x0600092D RID: 2349 RVA: 0x00020CC4 File Offset: 0x0001F0C4
	private void Start()
	{
		this.offset = base.transform.position - this.player.transform.position; //Defines the offset
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x00020D00 File Offset: 0x0001F100
	private void Update()
	{
		if (this.ps.jumpRope) //If the player is jump roping
		{
			this.velocity -= this.gravity * Time.deltaTime; //Decrease the velocity using gravity
			this.jumpHeight += this.velocity * Time.deltaTime; //Increase the jump height based on the velocity
			if (this.jumpHeight <= 0f) //When the player is on the floor, prevent the player from falling through.
			{
				this.jumpHeight = 0f;
				if (Input.GetKeyDown(KeyCode.Space))
				{
					this.velocity = this.initVelocity; //Start the jump
				}
			}
			this.jumpHeightV3 = new Vector3(0f, this.jumpHeight, 0f); //Turn the float into a vector
		}
		else if (Input.GetButton("Look Behind"))
		{
			this.lookBehind = 180; //Look behind you
		}
		else
		{
			this.lookBehind = 0; //Don't look behind you
		}
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x00020DD8 File Offset: 0x0001F1D8
	private void LateUpdate()
	{
		base.transform.position = this.player.transform.position + this.offset; //Teleport to the player, then move based on the offset vector(if all other statements fail)
		if (!this.ps.gameOver & !this.ps.jumpRope)
		{
			base.transform.position = this.player.transform.position + this.offset; //Teleport to the player, then move based on the offset vector
			base.transform.rotation = this.player.transform.rotation * Quaternion.Euler(0f, (float)this.lookBehind, 0f); //Rotate based on player direction + lookbehind
		}
		else if (this.ps.gameOver)
		{
			base.transform.position = this.baldi.transform.position + this.baldi.transform.forward * 2f + new Vector3(0f, 5f, 0f); //Puts the camera in front of Baldi
			base.transform.LookAt(new Vector3(this.baldi.position.x, this.baldi.position.y + 5f, this.baldi.position.z)); //Makes the player look at baldi with an offset so the camera doesn't look at the feet
		}
		else if (this.ps.jumpRope)
		{
			base.transform.position = this.player.transform.position + this.offset + this.jumpHeightV3; //Apply the jump rope vector onto the normal offset
			base.transform.rotation = this.player.transform.rotation; //Rotate based on player direction
		}
	}

	// Token: 0x040005B0 RID: 1456
	public GameObject player;

	// Token: 0x040005B1 RID: 1457
	public PlayerScript ps;

	// Token: 0x040005B2 RID: 1458
	public Transform baldi;

	// Token: 0x040005B3 RID: 1459
	public float initVelocity;

	// Token: 0x040005B4 RID: 1460
	public float velocity;

	// Token: 0x040005B5 RID: 1461
	public float gravity;

	// Token: 0x040005B6 RID: 1462
	private int lookBehind;

	// Token: 0x040005B7 RID: 1463
	public Vector3 offset;

	// Token: 0x040005B8 RID: 1464
	public float jumpHeight;

	// Token: 0x040005B9 RID: 1465
	public Vector3 jumpHeightV3;

}
