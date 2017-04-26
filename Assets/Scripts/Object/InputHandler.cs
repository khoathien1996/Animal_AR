using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {
    private GameObject o_Parent;
	// Use this for initialization
	void Start () {
        o_Parent = transform.parent.gameObject;
	}

	// hàm này bắt sự kiện click vào cái con vật nhé !

    void OnMouseDown()
    {
        if(o_Parent)
        {
            BaseAnimationManager s_ObjectAnimal = o_Parent.GetComponent<BaseAnimationManager>();
            if(s_ObjectAnimal)
            {
                //s_ObjectAnimal.Attack();
                
                Interactive.Instance.OnShow(s_ObjectAnimal);
            }
            AudioManager.Instance.PlayAudioEnglishFromResoures(gameObject.name,false);
            //_AudioType a_Audio = (_AudioType)System.Enum.Parse(typeof(_AudioType), o_Parent.name);
            //AudioManager.Instance.PlayAudioByType(a_Audio);
        }
    }
}
