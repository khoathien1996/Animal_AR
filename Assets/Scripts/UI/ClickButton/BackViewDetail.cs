using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackViewDetail : BaseClickButton
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void OnClicked()
    {
        GameConfig.m_isStart = true;
        ScreenManager.Instance.ShowScreenByType(eScreenType.GAME_PLAY);
        SwitchMode.Instance.OnShowModeDetail(true);
        base.OnClicked();
    }
}
