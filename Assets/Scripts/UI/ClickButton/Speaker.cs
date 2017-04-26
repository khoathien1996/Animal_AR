using UnityEngine;
using System.Collections;

public class Speaker :  BaseClickButton{

    public override void OnClicked()
    {
        Controller.Instance.PlayAudioWithAnimalName();
        base.OnClicked();
    }
}
