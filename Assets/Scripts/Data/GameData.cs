using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
[System.Serializable]
public class AnimationInfo
{
    public int stt;
    public string name;
    public string nameEngs;
    public string nameVies;
    public string animations;
    public AnimationInfo()
    { }
    public AnimationInfo(int stt, string name, string eng, string vie, string animation)
    {
        this.stt = stt;
        this.name = name;
        this.nameEngs = eng;
        this.nameVies = vie;
        this.animations = animation;
    }
    public List<string> getListNameEngs()
    {
        List<string> result = new List<string>();
        string[] context = this.nameEngs.Split(new char[] { ',' });
        for (int i = 0; i < context.Length; i++)
        {
            result.Add(context[i]);
        }
        if(result.Count > 0)
        {
            return result;
        }
        return null;
    }
    public List<string> getListNameVies()
    {
        List<string> result = new List<string>();
        string[] context = this.nameVies.Split(new char[] { ',' });
        for (int i = 0; i < context.Length; i++)
        {
            result.Add(context[i]);
        }
        if(result.Count > 0)
        {
            return result;
        }
        return null;
    }
    public List<string> getListAnimations()
    {
        List<string> result = new List<string>();
        string[] context = this.animations.Split(new char[] { ',' });
        for (int i = 0; i < context.Length; i++)
        {
            result.Add(context[i]);
            Debug.Log("Animation = " + context[i]);
        }
        if (result.Count > 0)
        {
            return result;
        }
        return null;
    }
}
public class GameData : MonoSingleton<GameData> {
    public List<AnimationInfo> listInfo = new List<AnimationInfo>();
	// Use this for initialization
	void Awake () {
        LoadData(this.listInfo, "Data/Data");
	}
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadData<T>(List<T> listName, string assetPath)
    {
        if (listName != null)
        {
            listName.Clear();
        }

        TextAsset textAsset = Resources.Load<TextAsset>(assetPath);

        if (textAsset)
        {
            Type typeOfT = typeof(T);

            //cat dong
            string[] temp = textAsset.text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            //lay danh sach field cua lop
            Assembly a = Assembly.GetAssembly(typeOfT);
            FieldInfo[] fieldInfo = typeOfT.GetFields(BindingFlags.Public | BindingFlags.Instance);

            //bo dong dau tien, key
            for (int i = 1; i < temp.Length; i++)
            {
                T newObject = (T)a.CreateInstance(typeOfT.FullName);

                Debug.Log("Line " + i + " = " + temp[i]);
                string[] context = temp[i].Split(new char[] { '\t' });
                for (int j = 0; j < fieldInfo.Length; j++)
                {
                    //					try {
                    //
                    //					}catch(Exception ex) {
                    //
                    //					}
                    string collumnValue = context[j];
                    if (fieldInfo[j].FieldType == typeof(String))
                    {
                        fieldInfo[j].SetValue(newObject, collumnValue.Substring(0, context[j].Length));
                    }
                    else if (fieldInfo[j].FieldType == typeof(Int32))
                    {
                        int value = 0;
                        if (collumnValue.Length > 0)
                        {
                            value = Int32.Parse(collumnValue);
                        }
                        fieldInfo[j].SetValue(newObject, value);
                    }
                    else if (fieldInfo[j].FieldType == typeof(float))
                    {
                        float value = 0.0f;
                        if (collumnValue.Length > 0)
                        {
                            value = float.Parse(collumnValue);
                        }
                        fieldInfo[j].SetValue(newObject, value);
                    }
                }
                listName.Add(newObject);
            }

        }
    }

    public AnimationInfo GetAnimationInfoByName(string name)
    {
        foreach(AnimationInfo info in this.listInfo)
        {
            if(info.name == name)
            {
                return info;
            }
        }
        return null;
    }
}
