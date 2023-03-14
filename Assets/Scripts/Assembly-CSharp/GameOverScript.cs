using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020000C1 RID: 193
public class GameOverScript : MonoBehaviour
{
	// Token: 0x0600097F RID: 2431 RVA: 0x00023008 File Offset: 0x00021408
	private void Start()
	{
		this.image = base.GetComponent<Image>();
		this.audioDevice = base.GetComponent<AudioSource>();
		this.delay = 5f;
		this.chance = UnityEngine.Random.Range(1f, 99f);
		if (this.chance < 98f)
		{
			int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 4f));
			this.image.sprite = this.images[num];
		}
		else
		{
			this.image.sprite = this.rare;
		}
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x0002309C File Offset: 0x0002149C
	private void Update()
	{
		this.delay -= 1f * Time.deltaTime;
		if (this.delay <= 0f)
		{
			if (this.chance < 98f)
			{
				SceneManager.LoadScene("MainMenu");
			}
			else
			{
				this.image.transform.localScale = new Vector3(5f, 5f, 1f);
				this.image.color = Color.red;
				if (!this.audioDevice.isPlaying)
				{
					this.audioDevice.Play();
				}
				if (this.delay <= -5f)
				{
					Application.Quit();
				}
			}
		}
	}

	// Token: 0x0400063B RID: 1595
	private Image image;

	// Token: 0x0400063C RID: 1596
	private float delay;

	// Token: 0x0400063D RID: 1597
	public Sprite[] images = new Sprite[5];

	// Token: 0x0400063E RID: 1598
	public Sprite rare;

	// Token: 0x0400063F RID: 1599
	private float chance;

	// Token: 0x04000640 RID: 1600
	private AudioSource audioDevice;
}
