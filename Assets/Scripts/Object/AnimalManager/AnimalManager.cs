using UnityEngine;
using System.Collections;
using DG.Tweening;

public class AnimalManager : BaseAnimationManager
{
 
    private const float m_minZoom = 1f;
    private const float m_maxZoom = 1.5f;
    [SerializeField]
    private Vector3 m_localScale = Vector3.one;
    [SerializeField]
    private Transform m_trfParent;
    
    public override void onEnable()
    {
        m_trfParent.localScale = Vector3.one;
    }
    // override
    public override void InitGame()
    {
        m_trfParent = transform.parent;
        m_localScale = m_trfParent.localScale;
    }

    // to ra
    public void DoZoomIn()
    {
        if(m_trfParent.localScale.x >= m_maxZoom)
        {
            return;
        }
        m_trfParent.DOScale(m_trfParent.localScale * (m_localScale.x + 0.1f), 0.5f);
    }

    // nho lai
    public void DoZoomOut()
    {
        if (m_trfParent.localScale.x <= m_minZoom)
        {
            return;
        }
        m_trfParent.DOScale(m_trfParent.localScale * (m_localScale.x - 0.1f), 0.5f);
    }
}
