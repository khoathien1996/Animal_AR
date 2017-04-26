using UnityEngine;
using System.Collections;
using DG.Tweening;

public class UpOptionButton : BaseClickButton {
    public RectTransform m_rectrfContainButton;

    private Vector2 m_anchorPositionContainStart = new Vector2();
    private Vector2 m_anchorToContain = new Vector2();
    private bool m_isMoveUp = false;
    public Transform m_trfIcon;
	// Use this for initialization
	 void Awake()
     {
        m_anchorPositionContainStart = m_rectrfContainButton.anchoredPosition;
        m_anchorToContain = new Vector2(m_anchorPositionContainStart.x,m_anchorPositionContainStart.y-(m_rectrfContainButton.rect.width+50));

        m_rectrfContainButton.anchoredPosition = m_anchorToContain;
	}

    public override void OnClicked()
    {
        if(m_isMoveUp)
        {
            //move down
            m_rectrfContainButton.DOAnchorPos(m_anchorToContain, 0.5f).OnComplete(CallBackMoveDown);
        }
        else
        {
            //move up
            m_rectrfContainButton.DOAnchorPos(m_anchorPositionContainStart, 0.5f).OnComplete(CallBackMoveUp);
        }
        base.OnClicked();
    } 
    private void CallBackMoveUp()
    {
        m_isMoveUp = true;
        m_trfIcon.localScale = new Vector3(m_trfIcon.localScale.x*-1, m_trfIcon.localScale.y , m_trfIcon.localScale.z);
    }
    private void CallBackMoveDown()
    {
        m_isMoveUp = false;
        m_trfIcon.localScale = new Vector3(m_trfIcon.localScale.x *-1, m_trfIcon.localScale.y, m_trfIcon.localScale.z);
    }
    void OnDisable()
    {
        m_rectrfContainButton.anchoredPosition = m_anchorToContain;
        m_trfIcon.localScale = Vector3.one;
        m_isMoveUp = false;
    }
}
