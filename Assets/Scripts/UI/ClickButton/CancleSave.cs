using UnityEngine;
using System.Collections;

public class CancleSave : BaseClickButton {

    public override void OnClicked()
    {
        ScreenShootManager.Instance.AgainCapture();
        base.OnClicked();
    }
}
