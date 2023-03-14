using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MaterialKit
{
	// Token: 0x020000BC RID: 188
	[RequireComponent(typeof(Canvas))]
	[ExecuteInEditMode]
	[AddComponentMenu("Layout/DP Canvas Scaler")]
	public class DpCanvasScaler : UIBehaviour
	{
		// Token: 0x0600094B RID: 2379 RVA: 0x00021718 File Offset: 0x0001FB18
		protected DpCanvasScaler()
		{
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0002176D File Offset: 0x0001FB6D
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x00021775 File Offset: 0x0001FB75
		public float referencePixelsPerUnit
		{
			get
			{
				return this.m_ReferencePixelsPerUnit;
			}
			set
			{
				this.m_ReferencePixelsPerUnit = value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0002177E File Offset: 0x0001FB7E
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x00021786 File Offset: 0x0001FB86
		public float fallbackScreenDPI
		{
			get
			{
				return this.m_FallbackScreenDPI;
			}
			set
			{
				this.m_FallbackScreenDPI = value;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0002178F File Offset: 0x0001FB8F
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x00021797 File Offset: 0x0001FB97
		public float defaultSpriteDPI
		{
			get
			{
				return this.m_DefaultSpriteDPI;
			}
			set
			{
				this.m_DefaultSpriteDPI = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x000217A0 File Offset: 0x0001FBA0
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x000217A8 File Offset: 0x0001FBA8
		public float dynamicPixelsPerUnit
		{
			get
			{
				return this.m_DynamicPixelsPerUnit;
			}
			set
			{
				this.m_DynamicPixelsPerUnit = value;
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x000217B1 File Offset: 0x0001FBB1
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_Canvas = base.GetComponent<Canvas>();
			this.Handle();
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x000217CB File Offset: 0x0001FBCB
		protected override void OnDisable()
		{
			this.SetScaleFactor(1f);
			this.SetReferencePixelsPerUnit(100f);
			base.OnDisable();
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000217E9 File Offset: 0x0001FBE9
		protected virtual void Update()
		{
			this.Handle();
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000217F4 File Offset: 0x0001FBF4
		protected virtual void Handle()
		{
			if (this.m_Canvas == null || !this.m_Canvas.isRootCanvas)
			{
				return;
			}
			if (this.m_Canvas.renderMode == RenderMode.WorldSpace)
			{
				this.HandleWorldCanvas();
				return;
			}
			this.HandleConstantPhysicalSize();
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00021841 File Offset: 0x0001FC41
		protected virtual void HandleWorldCanvas()
		{
			this.SetScaleFactor(this.m_DynamicPixelsPerUnit);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0002185C File Offset: 0x0001FC5C
		protected virtual void HandleConstantPhysicalSize()
		{
			float dpi = Screen.dpi;
			float num = (dpi != 0f) ? dpi : this.m_FallbackScreenDPI;
			float num2 = 160f;
			this.SetScaleFactor(num / num2);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit * num2 / this.m_DefaultSpriteDPI);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000218AB File Offset: 0x0001FCAB
		protected void SetScaleFactor(float scaleFactor)
		{
			if (scaleFactor == this.m_PrevScaleFactor)
			{
				return;
			}
			this.m_Canvas.scaleFactor = scaleFactor;
			this.m_PrevScaleFactor = scaleFactor;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x000218CD File Offset: 0x0001FCCD
		protected void SetReferencePixelsPerUnit(float referencePixelsPerUnit)
		{
			if (referencePixelsPerUnit == this.m_PrevReferencePixelsPerUnit)
			{
				return;
			}
			this.m_Canvas.referencePixelsPerUnit = referencePixelsPerUnit;
			this.m_PrevReferencePixelsPerUnit = referencePixelsPerUnit;
		}

		// Token: 0x040005EB RID: 1515
		[Tooltip("If a sprite has this 'Pixels Per Unit' setting, then one pixel in the sprite will cover one unit in the UI.")]
		[SerializeField]
		protected float m_ReferencePixelsPerUnit = 100f;

		// Token: 0x040005EC RID: 1516
		private const float kLogBase = 2f;

		// Token: 0x040005ED RID: 1517
		[Tooltip("The DPI to assume if the screen DPI is not known.")]
		[SerializeField]
		protected float m_FallbackScreenDPI = 96f;

		// Token: 0x040005EE RID: 1518
		[Tooltip("The pixels per inch to use for sprites that have a 'Pixels Per Unit' setting that matches the 'Reference Pixels Per Unit' setting.")]
		[SerializeField]
		protected float m_DefaultSpriteDPI = 96f;

		// Token: 0x040005EF RID: 1519
		[Tooltip("The amount of pixels per unit to use for dynamically created bitmaps in the UI, such as Text.")]
		[SerializeField]
		protected float m_DynamicPixelsPerUnit = 1f;

		// Token: 0x040005F0 RID: 1520
		private Canvas m_Canvas;

		// Token: 0x040005F1 RID: 1521
		[NonSerialized]
		private float m_PrevScaleFactor = 1f;

		// Token: 0x040005F2 RID: 1522
		[NonSerialized]
		private float m_PrevReferencePixelsPerUnit = 100f;
	}
}
