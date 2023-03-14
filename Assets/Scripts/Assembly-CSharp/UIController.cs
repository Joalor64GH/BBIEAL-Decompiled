using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000DA RID: 218
public class UIController : MonoBehaviour
{
    // Token: 0x060009FB RID: 2555 RVA: 0x00026AAB File Offset: 0x00024EAB
    private void Start()
    {
        if (this.unlockOnStart & !this.joystickEnabled)
        {
            this.cc.UnlockCursor();
        }
    }

    // Token: 0x060009FC RID: 2556 RVA: 0x00026AE1 File Offset: 0x00024EE1
    private void OnEnable()
    {
        this.dummyButtonPC.Select();
        this.UpdateControllerType();
    }

    // Token: 0x060009FD RID: 2557 RVA: 0x00026AF4 File Offset: 0x00024EF4
    private void Update()
    {
        this.UpdateControllerType();
    }

    // Token: 0x060009FE RID: 2558 RVA: 0x00026AFC File Offset: 0x00024EFC
    public void SwitchMenu()
    {
        this.SelectDummy();
        this.UpdateControllerType();
    }

    // Token: 0x060009FF RID: 2559 RVA: 0x00026B0C File Offset: 0x00024F0C
    private void UpdateControllerType()
    {
        if (!this.joystickEnabled & usingJoystick)
        {
            this.joystickEnabled = true;
            if (this.controlMouse)
            {
                this.cc.LockCursor();
            }
        }
        else if (this.joystickEnabled & !usingJoystick)
        {
            this.joystickEnabled = false;
            if (this.controlMouse)
            {
                this.cc.UnlockCursor();
            }
        }
        this.UIUpdate();
    }

    // Token: 0x06000A00 RID: 2560 RVA: 0x00026B9C File Offset: 0x00024F9C
    private void UIUpdate()
    {
        if (this.uiControlEnabled)
        {
            if (this.joystickEnabled)
            {
                if (EventSystem.current.currentSelectedGameObject.tag != this.buttonTag & this.firstButton != null)
                {
                    this.firstButton.Select();
                    this.firstButton.OnSelect(null);
                }
            }
            else
            {
                this.SelectDummy();
            }
        }
    }

    // Token: 0x06000A01 RID: 2561 RVA: 0x00026C0D File Offset: 0x0002500D
    public void EnableControl()
    {
        this.uiControlEnabled = true;
    }

    // Token: 0x06000A02 RID: 2562 RVA: 0x00026C16 File Offset: 0x00025016
    public void DisableControl()
    {
        this.uiControlEnabled = false;
    }

    // Token: 0x06000A03 RID: 2563 RVA: 0x00026C1F File Offset: 0x0002501F
    private void SelectDummy()
    {
        this.dummyButtonPC.Select();
    }

    // Token: 0x0400072B RID: 1835
    public CursorControllerScript cc;

    // Token: 0x0400072D RID: 1837
    private bool joystickEnabled;

    // Token: 0x0400072E RID: 1838
    public bool controlMouse;

    // Token: 0x0400072F RID: 1839
    public bool unlockOnStart;

    // Token: 0x04000730 RID: 1840
    public bool uiControlEnabled;

    // Token: 0x04000731 RID: 1841
    public Selectable firstButton;

    // Token: 0x04000732 RID: 1842
    public Selectable dummyButtonPC;

    // Token: 0x04000733 RID: 1843
    public Selectable dummyButtonElse;

    // Token: 0x04000734 RID: 1844
    public string buttonTag;

    public bool usingJoystick
    {
        get
        {
            return false;
        }
    }
}
