using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public class Controller : MonoSingleton<Controller> {

    public List<GameObject> m_listAnimalTracked = new List<GameObject>();

    public Text m_txtAnimalName;
    private RectTransform m_rectAnimalName;
    private Vector2 m_anchorStartOfAnimalName;
    private Vector2 m_anchorMoveToOfAnimalName;
    void Awake()
    {
        m_rectAnimalName = m_txtAnimalName.gameObject.GetComponent<RectTransform>();
        m_anchorStartOfAnimalName = m_rectAnimalName.anchoredPosition;
        m_anchorMoveToOfAnimalName = new Vector2(m_anchorStartOfAnimalName.x, m_anchorStartOfAnimalName.y - (m_rectAnimalName.rect.height + 10));

        m_rectAnimalName.anchoredPosition = m_anchorMoveToOfAnimalName;

    }

    void Start()
    {
        StateButton.Instance.DisableButton(); 
    }
    public void DoZoomIn()
    {
        if(m_listAnimalTracked.Count<=0)
        {
            return;
        }
        for(int i=0;i<m_listAnimalTracked.Count;i++)
        {
            m_listAnimalTracked[i].GetComponent<AnimalManager>().DoZoomIn();
        }
    }

    public void DoZoomOut()
    {
        if (m_listAnimalTracked.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < m_listAnimalTracked.Count; i++)
        {
            m_listAnimalTracked[i].GetComponent<AnimalManager>().DoZoomOut();
        }
    }

    public void AddAnimalTracked(GameObject _animal)
    {
        if (!m_listAnimalTracked.Contains(_animal))
        {
            m_listAnimalTracked.Add(_animal);
        }
        StateButton.Instance.EnableButton();
        SetTextAnimalName(m_listAnimalTracked[m_listAnimalTracked.Count-1].name);
        SwitchMode.Instance.nameAnimal = m_listAnimalTracked[m_listAnimalTracked.Count - 1].name;
    }

    public void RemoveAnimalTracked(GameObject _animal)
    {
        if (m_listAnimalTracked.Contains(_animal))
        {
            m_listAnimalTracked.Remove(_animal);
        }

        m_rectAnimalName.DOAnchorPos(m_anchorMoveToOfAnimalName, 0.5f);
        if (m_listAnimalTracked.Count <= 0)
        {
            StateButton.Instance.DisableButton();
        }
        SwitchMode.Instance.nameAnimal = "";
        Interactive.Instance.OnHide();

    }

    public void PlayAudioWithAnimalName()
    {
        if(m_listAnimalTracked.Count <=0)
        {
            return;
        }
        for(int i=0;i<m_listAnimalTracked.Count;i++)
        {
            AudioManager.Instance.PlayAudioEnglishFromResoures(m_listAnimalTracked[i].name);
        }
    }
    void Disable()
    {
        m_listAnimalTracked.Clear();
    }

    public void SetTextAnimalName(string _animalName)
    {
        m_txtAnimalName.text = _animalName;
       
        m_rectAnimalName.anchoredPosition = m_anchorMoveToOfAnimalName;
        m_rectAnimalName.DOAnchorPos(m_anchorStartOfAnimalName, 0.5f);
    }
}
