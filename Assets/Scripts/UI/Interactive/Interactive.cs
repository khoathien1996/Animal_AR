using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoSingleton<Interactive>
{
    public BaseAnimationManager animal;
    public Transform transfParent;
    public GameObject btItem;
    public GameObject content;

    public List<GameObject> vuforia;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private List<GameObject> listBt = new List<GameObject>();
    public void LoadAnimation(List<string> list)
    {
        foreach(GameObject obj  in this.listBt)
        {
            Destroy(obj);
        }
        this.listBt.Clear();
        for (int i = 0; i < list.Count; i++ )
        {
            string str = list[i];
            GameObject instance = Instantiate(btItem, transfParent) as GameObject;
            listBt.Add(instance);
            InteractiveItem item = instance.GetComponent<InteractiveItem>();
            if (item != null)
            {
                item.name = str;
                item.txtName.text = str;
                item.index = i;
            }
        }
    }
    public void OnClick(int index)
    {
        List<string> list = info.getListAnimations();
		for(int i = 0; i < list.Count; i++)
			Debug.Log ("Day la list: " + list[i]);
        animal.SetAnimation(list[index]);
    }
    public void SetAnimal(BaseAnimationManager animal)
    {
        this.animal = animal;
    }
    AnimationInfo info;
    public void OnShow(BaseAnimationManager animal)
    {
		//animal.SetAnimationByType (_AnimationState.run);
        if (this.content.active == false)
		{
			Debug.Log ("current state" + animal.currState);
			animal.SetAnimation (animal.getNextState());


            /*Vector3 pos = Input.mousePosition;
            this.transform.position = new Vector3(pos.x + 2, pos.y, 0);
            this.content.SetActive(true);
            SetAnimal(animal);
            info = GameData.Instance.GetAnimationInfoByName(animal.GetName());
            if (info != null)
            {
                List<string> list = info.getListNameVies();
                if (list.Count > 0)
                    LoadAnimation(list);
                else
                {
                    Debug.LogError("Eo get dc phan tu");
                }
            }
            else
            {
                Debug.LogError("Info bang NULL cmnr" + animal.GetName());

            }
			*/

			// chổ này em đổi trạng thái của con đó là tấn công hay chạy gì đó
			// có cái animal -> animal.changeanimation()

        }
    }
    public void OnHide()
    {
        this.content.SetActive(false);
    }
    
    
}
