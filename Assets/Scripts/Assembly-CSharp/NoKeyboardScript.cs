using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C6 RID: 198
public class NoKeyboardScript : InputField
{
	// Token: 0x06000997 RID: 2455 RVA: 0x00024306 File Offset: 0x00022706
	protected override void Start()
	{
		base.keyboardType = (TouchScreenKeyboardType)(-1);
		base.Start();
	}
}
