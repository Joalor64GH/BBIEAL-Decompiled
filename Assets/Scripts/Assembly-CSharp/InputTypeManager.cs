using System;
//using Rewired;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class InputTypeManager : MonoBehaviour
{
	// Token: 0x06000042 RID: 66 RVA: 0x00003254 File Offset: 0x00001654
	private void Awake()
	{
		Input.simulateMouseWithTouches = false;
		if (InputTypeManager.itm == null)
		{
			InputTypeManager.itm = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else if (InputTypeManager.itm != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x000032A8 File Offset: 0x000016A8
	private void Update()
	{
		if (Input.touchCount > 0 && !InputTypeManager.usingTouch)
		{
			InputTypeManager.usingTouch = true;
		}
		else if (Input.anyKeyDown)
		{
			InputTypeManager.usingTouch = false;
		}
	}

	// Token: 0x04000052 RID: 82
	private static InputTypeManager itm;

	// Token: 0x04000053 RID: 83
	public static bool usingTouch;
}
