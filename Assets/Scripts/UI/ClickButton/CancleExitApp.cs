using UnityEngine;
using System.Collections;

public class CancleExitApp : BaseClickButton {
    public override void OnClicked()
    {
        Popup.Instance.m_CallBackCloseHandle = HidePopup;
        Popup.Instance.ClosePopup();
        base.OnClicked();
    }

    public void HidePopup()
    {
        ScreenManager.Instance.HideCurrentPopup();
    }
}
