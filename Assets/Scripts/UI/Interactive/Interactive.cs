using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoSingleton<Interactive>
{
    public BaseAnimationManager animal;
    public Transform transfParent;
    public GameObject btItem;
    public GameObject content;
    public GameObject text_speech;

    public List<GameObject> vuforia;
    public AudioClip audio_clip;
    AudioSource audio;
    // Use this for initialization
    void Start()
    {
        text_speech.SetActive(false);
        audio = GetComponent<AudioSource>();
        audio.clip = audio_clip;
        //audio.PlayOneShot(audio.clip);

    }

    // Update is called once per frame
    void Update()
    {

    }
    private List<GameObject> listBt = new List<GameObject>();
    public void LoadAnimation(List<string> list)
    {
        foreach (GameObject obj in this.listBt)
        {
            Destroy(obj);
        }
        this.listBt.Clear();
        for (int i = 0; i < list.Count; i++)
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
        for (int i = 0; i < list.Count; i++)
            Debug.Log("Day la list: " + list[i]);
        animal.SetAnimation(list[index]);
    }
    public void SetAnimal(BaseAnimationManager animal)
    {
        this.animal = animal;
    }
    AnimationInfo info;
    public void OnShow(BaseAnimationManager animal, string word)
    {        
        //Khi nao hien len moi noi audio
        if (this.content.active == false)
        {
            
            switch (word)
            {
                case "change":
                case null:
                    animal.SetAnimation(animal.getNextState());
                    break;
                case "idle":
                case "walk":
                case "run":
                case "attack":
                    animal.SetAnimation(word);
                    break;
                default:
                    AudioManager.Instance.PlayInteractiveSpeech(animal.name, word);
                    break;

            }



            Debug.Log("current state" + animal.currState);


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
