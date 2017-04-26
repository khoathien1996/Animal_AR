using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Popup : MonoSingleton<Popup> {
    public RectTransform m_rectForEffect;

    private Vector2 m_anchorStart;
    private Vector2 m_anchorMoveTo;

    public delegate void CallBackCloseHandle();
    public CallBackCloseHandle m_CallBackCloseHandle = null;
	void Awake()
    {
        m_anchorStart = m_rectForEffect.anchoredPosition;
        m_anchorMoveTo = new Vector2(m_anchorStart.x,m_anchorStart.y+(m_rectForEffect.rect.height+50));
        m_rectForEffect.anchoredPosition = m_anchorMoveTo;
    }

    void OnEnable()
    {
        if(m_rectForEffect)
        {
            m_rectForEffect.anchoredPosition = m_anchorMoveTo;
            m_rectForEffect.DOAnchorPos(m_anchorStart,0.35f).SetEase(Ease.OutBounce);
        }
    }

    public void ClosePopup()
    {
        if (m_rectForEffect)
        {
            m_rectForEffect.DOAnchorPos(m_anchorMoveTo, 0.35f).OnComplete(CallBackClose);
        }
    }

    public void CallBackClose()
    {
        if (m_CallBackCloseHandle != null)
        {
            m_CallBackCloseHandle();
        }
    }
}
