using UnityEngine;
using System.Collections;

public class ZoomOut : BaseClickButton {
    public override void OnClicked()
    {
        base.OnClicked();
        Controller.Instance.DoZoomOut();
    }
}
