using System;
using UnityEngine;

// Token: 0x020000D0 RID: 208
public class PlayerMovement : MonoBehaviour
{
	// Token: 0x060009CC RID: 2508 RVA: 0x00025A36 File Offset: 0x00023E36
	private void Awake()
	{
		this.mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x00025A59 File Offset: 0x00023E59
	private void Start()
	{
		this.stamina = this.staminaMax;
		Time.timeScale = 1f;
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x00025A71 File Offset: 0x00023E71
	private void Update()
	{
		this.running = Input.GetButton("Run");
		this.MouseMove();
		this.PlayerMove();
		this.StaminaUpdate();
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x00025A9C File Offset: 0x00023E9C
	private void MouseMove()
	{
		Quaternion rotation = base.transform.rotation;
		rotation.eulerAngles += new Vector3(0f, Input.GetAxis("Mouse X") * this.mouseSensitivity * Time.deltaTime * Time.timeScale, 0f);
		base.transform.rotation = rotation;
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x00025B08 File Offset: 0x00023F08
	private void PlayerMove()
	{
		float d = this.walkSpeed;
		if (this.stamina > 0f & this.running)
		{
			d = this.runSpeed;
		}
		Vector3 a = base.transform.right * Input.GetAxis("Strafe");
		Vector3 b = base.transform.forward * Input.GetAxis("Forward");
		this.sensitivity = Mathf.Clamp((a + b).magnitude, 0f, 1f);
		this.cc.Move((a + b).normalized * d * this.sensitivity * Time.deltaTime);
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x00025BDC File Offset: 0x00023FDC
	public void StaminaUpdate()
	{
		if (this.cc.velocity.magnitude > this.cc.minMoveDistance)
		{
			if (this.running)
			{
				this.stamina = Mathf.Max(this.stamina - this.staminaDrop * Time.deltaTime, 0f);
			}
		}
		else if (this.stamina < this.staminaMax)
		{
			this.stamina += this.staminaRise * Time.deltaTime;
		}
	}

	// Token: 0x040006DF RID: 1759
	public CharacterController cc;

	// Token: 0x040006E0 RID: 1760
	public float walkSpeed;

	// Token: 0x040006E1 RID: 1761
	public float runSpeed;

	// Token: 0x040006E2 RID: 1762
	public float stamina;

	// Token: 0x040006E3 RID: 1763
	public float staminaDrop;

	// Token: 0x040006E4 RID: 1764
	public float staminaRise;

	// Token: 0x040006E5 RID: 1765
	public float staminaMax;

	// Token: 0x040006E6 RID: 1766
	private float sensitivity;

	// Token: 0x040006E7 RID: 1767
	private float mouseSensitivity;

	// Token: 0x040006E8 RID: 1768
	private bool running;
}
