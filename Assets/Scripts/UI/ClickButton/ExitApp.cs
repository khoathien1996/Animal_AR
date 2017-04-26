using UnityEngine;
using System.Collections;

public class ExitApp : BaseClickButton {
    public override void OnClicked()
    {
        base.OnClicked();
        Application.Quit();
    }
}
