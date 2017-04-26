using UnityEngine;
using System.Collections;

public class StartGame : BaseClickButton {

    public override void OnClicked()
    {
        GameConfig.m_isStart = true;
        ScreenManager.Instance.ShowScreenByType(eScreenType.GAME_PLAY);
        base.OnClicked();
    }
}
