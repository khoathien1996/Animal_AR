using UnityEngine;
using System.Collections;

public class SaveImage : BaseClickButton {
    public override void OnClicked()
    {
        ScreenShootManager.Instance.SaveImage();
        base.OnClicked();
    }
}
