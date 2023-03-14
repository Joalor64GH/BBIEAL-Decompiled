using System;
using TMPro;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public class TextUnderliner : MonoBehaviour
{
    // Token: 0x060009F8 RID: 2552 RVA: 0x00026A87 File Offset: 0x00024E87
    public void Underline()
    {
        this.text.fontStyle = FontStyles.Underline;
    }

    // Token: 0x060009F9 RID: 2553 RVA: 0x00026A95 File Offset: 0x00024E95
    public void Ununderline()
    {
        this.text.fontStyle = FontStyles.Normal;
    }

    // Token: 0x0400072A RID: 1834
    public TMP_Text text;
}
