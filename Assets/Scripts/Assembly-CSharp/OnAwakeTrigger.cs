using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200001A RID: 26
public class OnAwakeTrigger : MonoBehaviour
{
	// Token: 0x0600005F RID: 95 RVA: 0x0000379F File Offset: 0x00001B9F
	private void OnEnable()
	{
		this.OnEnableEvent.Invoke();
	}

	// Token: 0x0400006E RID: 110
	public UnityEvent OnEnableEvent;
}
