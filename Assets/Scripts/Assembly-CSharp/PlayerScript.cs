using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Token: 0x020000D1 RID: 209
public class PlayerScript : MonoBehaviour
{
	// Token: 0x060009D3 RID: 2515 RVA: 0x00025C74 File Offset: 0x00024074
	private void Start()
	{
		//Yeah your on your own for this one
		if (PlayerPrefs.GetInt("AnalogMove") == 1)
		{
			this.sensitivityActive = true;
		}
		this.height = base.transform.position.y;
		this.stamina = this.maxStamina;
		this.playerRotation = base.transform.rotation;
		this.mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
		this.principalBugFixer = 1;
		this.flipaturn = 1f;
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x00025D04 File Offset: 0x00024104
	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, this.height, base.transform.position.z);
		this.MouseMove();
		this.PlayerMove();
		this.StaminaCheck();
		this.GuiltCheck();
		if (this.cc.velocity.magnitude > 0f)
		{
			this.gc.LockMouse();
		}
		if (this.jumpRope & (base.transform.position - this.frozenPosition).magnitude >= 1f) // If the player moves, deactivate the jumprope minigame
		{
			this.DeactivateJumpRope();
		}
		if (this.sweepingFailsave > 0f)
		{
			this.sweepingFailsave -= Time.deltaTime;
		}
		else
		{
			this.sweeping = false;
			this.hugging = false;
		}
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x00025E00 File Offset: 0x00024200
	private void MouseMove()
	{
		this.playerRotation.eulerAngles = new Vector3(this.playerRotation.eulerAngles.x, this.playerRotation.eulerAngles.y, this.fliparoo);
		this.playerRotation.eulerAngles = this.playerRotation.eulerAngles + Vector3.up * Input.GetAxis("Mouse X") * this.mouseSensitivity * Time.timeScale * this.flipaturn;
		base.transform.rotation = this.playerRotation;
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x00025EAC File Offset: 0x000242AC
	private void PlayerMove()
	{
		Vector3 vector = new Vector3(0f, 0f, 0f);
		Vector3 vector2 = new Vector3(0f, 0f, 0f);
		vector = base.transform.forward * Input.GetAxis("Forward");
		vector2 = base.transform.right * Input.GetAxis("Strafe");
		if (this.stamina > 0f)
		{
			if (Input.GetButton("Run"))
			{
				this.playerSpeed = this.runSpeed;
				this.sensitivity = 1f;
				if (this.cc.velocity.magnitude > 0.1f & !this.hugging & !this.sweeping)
				{
					this.ResetGuilt("running", 0.1f);
				}
			}
			else
			{
				this.playerSpeed = this.walkSpeed;
				if (this.sensitivityActive)
				{
					this.sensitivity = Mathf.Clamp((vector2 + vector).magnitude, 0f, 1f);
				}
				else
				{
					this.sensitivity = 1f;
				}
			}
		}
		else
		{
			this.playerSpeed = this.walkSpeed;
			if (this.sensitivityActive)
			{
				this.sensitivity = Mathf.Clamp((vector2 + vector).magnitude, 0f, 1f);
			}
			else
			{
				this.sensitivity = 1f;
			}
		}
		this.playerSpeed *= Time.deltaTime;
		this.moveDirection = (vector + vector2).normalized * this.playerSpeed * this.sensitivity;
		if (!(!this.jumpRope & !this.sweeping & !this.hugging))
		{
			if (this.sweeping && !this.bootsActive)
			{
				this.moveDirection = this.gottaSweep.velocity * Time.deltaTime + this.moveDirection * 0.3f;
			}
			else if (this.hugging && !this.bootsActive)
			{
				this.moveDirection = (this.firstPrize.velocity * 1.2f * Time.deltaTime + (new Vector3(this.firstPrizeTransform.position.x, this.height, this.firstPrizeTransform.position.z) + new Vector3((float)Mathf.RoundToInt(this.firstPrizeTransform.forward.x), 0f, (float)Mathf.RoundToInt(this.firstPrizeTransform.forward.z)) * 3f - base.transform.position)) * (float)this.principalBugFixer;
			}
			else if (this.jumpRope)
			{
				this.moveDirection = new Vector3(0f, 0f, 0f);
			}
		}
		this.cc.Move(this.moveDirection);
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x00026210 File Offset: 0x00024610
	private void StaminaCheck()
	{
		if (this.cc.velocity.magnitude > 0.1f)
		{
			if (Input.GetButton("Run") & this.stamina > 0f)
			{
				this.stamina -= this.staminaRate * Time.deltaTime;
			}
			if (this.stamina < 0f & this.stamina > -5f)
			{
				this.stamina = -5f;
			}
		}
		else if (this.stamina < this.maxStamina)
		{
			this.stamina += this.staminaRate * Time.deltaTime;
		}
		this.staminaBar.value = this.stamina / this.maxStamina * 100f;
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x000262F0 File Offset: 0x000246F0
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.name == "Baldi" & !this.gc.debugMode)
		{
			this.gameOver = true;
			RenderSettings.skybox = this.blackSky; //Sets the skybox black
			base.StartCoroutine(this.KeepTheHudOff()); //Hides the Hud
		}
		else if (other.transform.name == "Playtime" & !this.jumpRope & this.playtime.playCool <= 0f)
		{
			this.ActivateJumpRope();
		}
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x0002638C File Offset: 0x0002478C
	public IEnumerator KeepTheHudOff()
	{
		while (this.gameOver)
		{
			this.hud.enabled = false;
			this.mobile1.enabled = false;
			this.mobile2.enabled = false;
			this.jumpRopeScreen.SetActive(false);
			yield return new WaitForEndOfFrame();
		}
		yield break;
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x000263A8 File Offset: 0x000247A8
	private void OnTriggerStay(Collider other)
	{
		if (other.transform.name == "Gotta Sweep")
		{
			this.sweeping = true;
			this.sweepingFailsave = 1f;
		}
		else if (other.transform.name == "1st Prize" & this.firstPrize.velocity.magnitude > 5f)
		{
			this.hugging = true;
			this.sweepingFailsave = 1f;
		}
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x00026430 File Offset: 0x00024830
	private void OnTriggerExit(Collider other)
	{
		if (other.transform.name == "Office Trigger")
		{
			this.ResetGuilt("escape", this.door.lockTime);
		}
		else if (other.transform.name == "Gotta Sweep")
		{
			this.sweeping = false;
		}
		else if (other.transform.name == "1st Prize")
		{
			this.hugging = false;
		}
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x000264B9 File Offset: 0x000248B9
	public void ResetGuilt(string type, float amount)
	{
		if (amount >= this.guilt)
		{
			this.guilt = amount;
			this.guiltType = type;
		}
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x000264D5 File Offset: 0x000248D5
	private void GuiltCheck()
	{
		if (this.guilt > 0f)
		{
			this.guilt -= Time.deltaTime;
		}
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x000264F9 File Offset: 0x000248F9
	public void ActivateJumpRope()
	{
		this.jumpRopeScreen.SetActive(true);
		this.jumpRope = true;
		this.frozenPosition = base.transform.position;
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x0002651F File Offset: 0x0002491F
	public void DeactivateJumpRope()
	{
		this.jumpRopeScreen.SetActive(false);
		this.jumpRope = false;
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x00026534 File Offset: 0x00024934
	public void ActivateBoots()
	{
		this.bootsActive = true;
		base.StartCoroutine(this.BootTimer());
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x0002654C File Offset: 0x0002494C
	private IEnumerator BootTimer()
	{
		float time = 15f;
		while (time > 0f)
		{
			time -= Time.deltaTime;
			yield return null;
		}
		this.bootsActive = false;
		yield break;
	}

	// Token: 0x040006E9 RID: 1769
	public GameControllerScript gc;

	// Token: 0x040006EA RID: 1770
	public BaldiScript baldi;

	// Token: 0x040006EB RID: 1771
	public DoorScript door;

	// Token: 0x040006EC RID: 1772
	public PlaytimeScript playtime;

	// Token: 0x040006ED RID: 1773
	public bool gameOver;

	// Token: 0x040006EE RID: 1774
	public bool jumpRope;

	// Token: 0x040006EF RID: 1775
	public bool sweeping;

	// Token: 0x040006F0 RID: 1776
	public bool hugging;

	// Token: 0x040006F1 RID: 1777
	public bool bootsActive;

	// Token: 0x040006F2 RID: 1778
	public int principalBugFixer;

	// Token: 0x040006F3 RID: 1779
	public float sweepingFailsave;

	// Token: 0x040006F4 RID: 1780
	public float fliparoo;

	// Token: 0x040006F5 RID: 1781
	public float flipaturn;

	// Token: 0x040006F6 RID: 1782
	private Quaternion playerRotation;

	// Token: 0x040006F7 RID: 1783
	public Vector3 frozenPosition;

	// Token: 0x040006F8 RID: 1784
	private bool sensitivityActive;

	// Token: 0x040006F9 RID: 1785
	private float sensitivity;

	// Token: 0x040006FA RID: 1786
	public float mouseSensitivity;

	// Token: 0x040006FB RID: 1787
	public float walkSpeed;

	// Token: 0x040006FC RID: 1788
	public float runSpeed;

	// Token: 0x040006FD RID: 1789
	public float slowSpeed;

	// Token: 0x040006FE RID: 1790
	public float maxStamina;

	// Token: 0x040006FF RID: 1791
	public float staminaRate;

	// Token: 0x04000700 RID: 1792
	public float guilt;

	// Token: 0x04000701 RID: 1793
	public float initGuilt;

	// Token: 0x04000702 RID: 1794
	private float moveX;

	// Token: 0x04000703 RID: 1795
	private float moveZ;

	// Token: 0x04000704 RID: 1796
	private Vector3 moveDirection;

	// Token: 0x04000705 RID: 1797
	private float playerSpeed;

	// Token: 0x04000706 RID: 1798
	public float stamina;

	// Token: 0x04000707 RID: 1799
	public CharacterController cc;

	// Token: 0x04000708 RID: 1800
	public NavMeshAgent gottaSweep;

	// Token: 0x04000709 RID: 1801
	public NavMeshAgent firstPrize;

	// Token: 0x0400070A RID: 1802
	public Transform firstPrizeTransform;

	// Token: 0x0400070B RID: 1803
	public Slider staminaBar;

	// Token: 0x0400070C RID: 1804
	public float db;

	// Token: 0x0400070D RID: 1805
	public string guiltType;

	// Token: 0x0400070E RID: 1806
	public GameObject jumpRopeScreen;

	// Token: 0x04000710 RID: 1808
	public float height;

	// Token: 0x04000711 RID: 1809
	public Material blackSky;

	// Token: 0x04000712 RID: 1810
	public Canvas hud;

	// Token: 0x04000713 RID: 1811
	public Canvas mobile1;

	// Token: 0x04000714 RID: 1812
	public Canvas mobile2;
}
