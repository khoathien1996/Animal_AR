using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ContentInfo
{
    public int stt;
    public string nameEng;
    public string nameVie;
    public string type;
    public string local;
    public string size;
    public string trongluong;
    public string tuoitho;
    public string thongtin;
    public string link;
    public ContentInfo()
    {

    }
}

public class LoadInfomation : MonoBehaviour {
    public Text txtContent;
    public Transform transfImage;
    public List<ContentInfo> listInfos;
    void Awake()
    {
        GameData.Instance.LoadData(listInfos, "Data/Animal");
    }
	// Use this for initialization
	void Start () {
        TestLoadContent();
        TestLoadImage();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public ContentInfo GetInfoByName(string name)
    {
        foreach(ContentInfo info in this.listInfos)
        {
            if(info.nameEng == name)
            {
                return info;
            }
        }
        return null;
    }
    public void LoadContent(string name)
    {
        ContentInfo info = GetInfoByName(name);
        if(info != null)
        {
            this.txtContent.text = info.thongtin;
        }
    }
    public void LoadImage(string name)
    {
        Sprite[] listSprites = Resources.LoadAll<Sprite>("Image/" + name);
        if(listSprites != null)
        {
            RectTransform rect = (RectTransform)transfImage;
            rect.sizeDelta = new Vector2(0, listSprites.Length * 160);
            foreach(Sprite spr in listSprites)
            {
                GameObject obj = new GameObject();
                obj.transform.SetParent(transfImage);
                Image img = obj.AddComponent<Image>();
                img.preserveAspect = true;
                img.sprite = spr;
            }
        }
    }
    [ContextMenu("Load Content")]
    void TestLoadContent()
    {
        LoadContent("LION");
    }
    [ContextMenu("Load Image")]
    void TestLoadImage()
    {
        LoadImage("LION");
    }
}
