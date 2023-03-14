using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000004 RID: 4
public class BackButtonScript : MonoBehaviour
{
	// Token: 0x06000009 RID: 9 RVA: 0x000021D6 File Offset: 0x000005D6
	private void Start()
	{
		this.button = base.GetComponent<Button>();
		this.button.onClick.AddListener(new UnityAction(this.CloseScreen));
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002200 File Offset: 0x00000600
	private void CloseScreen()
	{
		this.screen.SetActive(false);
	}

	// Token: 0x0400000A RID: 10
	private Button button;

	// Token: 0x0400000B RID: 11
	public GameObject screen;
}
