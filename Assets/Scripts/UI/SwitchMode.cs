using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMode : MonoSingleton<SwitchMode> {

    public List<GameObject> vuforias;
    public GameObject mode;
    public Transform transf;
    public string nameAnimal = "";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnShowModeDetail(bool isActive)
    {
        
        Clear();
        foreach (GameObject obj in this.vuforias)
        {
            obj.SetActive(isActive);
        }
        mode.SetActive(!isActive);
        Debug.LogError("Name = " + this.nameAnimal);
        if (this.nameAnimal != "")
        {
            CreateAnimal(this.nameAnimal);

        }
        else
        {
            Debug.LogError("Name = " + this.nameAnimal);
        }
    }
    List<GameObject> listAnimal = new List<GameObject>();
    public void CreateAnimal(string name)
    {
        GameObject obj = Instantiate(Resources.Load("Animal/" + name)) as GameObject;
        obj.transform.SetParent(transf);
        obj.transform.localPosition = Vector3.zero;
        listAnimal.Add(obj);
    }
    public void Clear()
    {
        foreach(GameObject obj in this.listAnimal)
        {
            Destroy(obj);
        }
        this.listAnimal.Clear();
    }
}
