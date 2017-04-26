using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveItem : MonoBehaviour {

    public Text txtName;
    public string name;
    public int index;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClick()
    {
        Interactive.Instance.OnClick(this.index);
    }
}
