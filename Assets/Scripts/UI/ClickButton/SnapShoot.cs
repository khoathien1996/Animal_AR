using UnityEngine;
using System.Collections;

public class SnapShoot : BaseClickButton {

    public override void OnClicked()
    {
        ScreenShootManager.Instance.OnClickCapture();
        base.OnClicked();
    }
}
