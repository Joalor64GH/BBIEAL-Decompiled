using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x02000018 RID: 24
public class MouseoverScript : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
{
	// Token: 0x06000057 RID: 87 RVA: 0x00003728 File Offset: 0x00001B28
	public void OnSelect(BaseEventData eventData)
	{
		this.mouseOver.Invoke();
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003735 File Offset: 0x00001B35
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.mouseOver.Invoke();
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00003742 File Offset: 0x00001B42
	public void OnDeselect(BaseEventData eventData)
	{
		this.mouseLeave.Invoke();
	}

	// Token: 0x0600005A RID: 90 RVA: 0x0000374F File Offset: 0x00001B4F
	public void OnPointerExit(PointerEventData eventData)
	{
		this.mouseLeave.Invoke();
	}

	// Token: 0x0400006C RID: 108
	public UnityEvent mouseOver;

	// Token: 0x0400006D RID: 109
	public UnityEvent mouseLeave;
}
