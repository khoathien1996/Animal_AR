using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class BaseClickButton : MonoBehaviour {
	void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(CallWhenClick);
    }

    public virtual void OnClicked()
    {
        AudioManager.Instance.PlayAudioByType(_AudioType.BUTTON);
    }

    public void CallWhenClick()
    {
        transform.DOScale(transform.localScale * 1.1f, 0.06f).SetLoops(2,LoopType.Yoyo).OnComplete(OnClicked);
    }
    void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(CallWhenClick);
    }
}
