using UnityEngine;
using System.Collections;

public class SounsSetting : BaseClickButton{
    private bool m_isMute = false;

    public override void OnClicked()
    {
        m_isMute = !m_isMute;
        AudioManager.Instance.SettingSound(m_isMute);
        base.OnClicked();
    }
}
