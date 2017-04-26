using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using DG.Tweening;

public class Menu : MonoBehaviour {
    public Transform m_btnStart;
    
    void OnEnable()
    {
        m_btnStart.localScale = Vector3.zero;
        m_btnStart.DOScale(Vector3.one,0.35f).SetEase(Ease.OutBack);
    }

}
