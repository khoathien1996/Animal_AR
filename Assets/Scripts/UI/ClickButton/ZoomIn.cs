using UnityEngine;
using System.Collections;

public class ZoomIn : BaseClickButton {
    public override void OnClicked()
    {
        Controller.Instance.DoZoomIn();
        base.OnClicked();
    }
}
