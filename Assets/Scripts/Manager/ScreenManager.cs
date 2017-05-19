using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public enum eScreenType
{
    MENU,
    GAME_PLAY,
    INTERACTIVE,
    NONE
}

public enum ePopupType
{
    NONE,
    SNAP_SHOOT,
    EXIT
}

[System.Serializable]
public class MyScreen
{
    public eScreenType m_screenType;
    public GameObject m_objectScreen;
}
[System.Serializable]
public class MyPopup
{
    public ePopupType m_popupType;
    public GameObject m_objectPopup;
}
public class ScreenManager : MonoSingleton<ScreenManager>
{
    public MyScreen[] m_arrayMyScreen;
    public MyPopup[] m_arrayMyPopup;
    public Dictionary<eScreenType, GameObject> m_dicScreen = new Dictionary<eScreenType, GameObject>();
    public Dictionary<ePopupType, GameObject> m_dicPopup = new Dictionary<ePopupType, GameObject>();

    private eScreenType m_currentScreen;
    private ePopupType m_currentPopup;
    private Stack m_myStackOfScreen = new Stack();

    
    //public GameObject m_uiBackground;
    public eScreenType CurrentScreen
    {
        get { return m_currentScreen; }
        set { m_currentScreen = value; }
    }

    public ePopupType CurrentPopup
    {
        get
        {
            return m_currentPopup;
        }

        set
        {
            m_currentPopup = value;
        }
    }

    

    // Use this for initialization
    void Awake()
    {
        InitDictionary();
    }

    void Start()
    {
        CurrentPopup = ePopupType.NONE;
        ShowScreenByType(eScreenType.MENU);
    }

    private void InitDictionary()
    {
        if (m_arrayMyScreen.Length > 0)
        {
            for (int i = 0; i < m_arrayMyScreen.Length; i++)
            {
                m_dicScreen.Add(m_arrayMyScreen[i].m_screenType, m_arrayMyScreen[i].m_objectScreen);
                //m_arrayMyScreen[i].m_objectScreen.SetActive(false);
            }
        }

        if(m_arrayMyPopup.Length>0)
        {
            for(int i=0;i< m_arrayMyPopup.Length;i++)
            {
                m_dicPopup.Add(m_arrayMyPopup[i].m_popupType, m_arrayMyPopup[i].m_objectPopup);
                m_arrayMyPopup[i].m_objectPopup.SetActive(false);
            }
        }
    }

    void Update()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(CurrentPopup != ePopupType.NONE)
            {
                HideCurrentPopup();
                return;
            }
        }
#endif
    }

    private GameObject GetScreenByType(eScreenType type)
    {
        if (m_dicScreen.ContainsKey(type))
        {
            return m_dicScreen[type];
        }
#if UNITY_EDITOR
        Debug.Log("ko get duoc man hinh nay!");
#endif
        return null;
    }

    private GameObject GetPopupByType(ePopupType _type)
    {
        if (m_dicPopup.ContainsKey(_type))
        {
            return m_dicPopup[_type];
        }
#if UNITY_EDITOR
        Debug.Log("ko get duoc man hinh nay!");
#endif
        return null;
    }

    //public void DisableAllScreen()
    //{
    //    foreach (GameObject var in m_dicScreen.Values)
    //    {
    //        var.SetActive(false);
    //    }
    //}

    public void ShowScreenByType(eScreenType _type)
    {
        
        GameObject objScreen = GetScreenByType(_type);
        HideScreenByType(CurrentScreen);
        if (objScreen)
        {
            objScreen.SetActive(true);
        }
        
        m_myStackOfScreen.Push(_type);
        CurrentScreen = _type;
        
    }

    public void ShowCurrentScreen()
    {
        GameObject objScreen = GetScreenByType(CurrentScreen);
        if (objScreen)
        {
            objScreen.SetActive(true);
        }
        
    }
    public void ShowScreenPrev()
    {
        //eScreenType screenCurrent = (eScreenType)m_myStackOfScreen.Pop();
        //if (screenCurrent != eScreenType.GAME_PLAY)
        //{
        //    BaseEffectScreen effectScreen = GetScreenByType(screenCurrent).GetComponent<BaseEffectScreen>();
        //    //QuestionManager.Instance.ActiveSpriteMainImage = false;
        //    if (effectScreen)
        //    {
        //        effectScreen.m_myDelegate = CallBackCloseWindow;
        //        effectScreen.CloseWindow();
        //    }
        //}
        //else
        //{
        //    //QuestionManager.Instance.ActiveSpriteMainImage = true;
        //    CallBackCloseWindow();
        //}
    }
    private void CallBackCloseWindow()
    {
        HideCurrentPopup();
        eScreenType screenPrev = (eScreenType)m_myStackOfScreen.Pop();
        ShowScreenByType(screenPrev);
    }
    public void HideScreenByType(eScreenType type)
    {
        GameObject objScreen = GetScreenByType(type);

        if (objScreen)
        {
            objScreen.SetActive(false);
        }
    }

    #region POPUP..........................
    public void HideCurrentPopup()
    {
        if(CurrentPopup == ePopupType.NONE)
        {
            return;
        }
        GameObject objScreenPopup = GetPopupByType(CurrentPopup);
        if (objScreenPopup)
        {
            objScreenPopup.SetActive(false);
            CurrentPopup = ePopupType.NONE;
        }
        //ShowScreenByType(CurrentScreen);
        ShowCurrentScreen();
    }
    public void ShowPopupScreen(ePopupType _type)
    {
        GameObject objScreenPopup = GetPopupByType(_type);
        HideCurrentPopup();        
        if (objScreenPopup)
        {
            objScreenPopup.SetActive(true);
        }
        CurrentPopup = _type; 
    }

    //public void hidePopupScreen(ePopupType _type)
    //{
    //    GameObject objScreenPopup = GetPopupByType(_type);
    //    CurrentPopup = ePopupType.NONE;
    //    if (objScreenPopup)
    //    {
    //        objScreenPopup.SetActive(false);
    //    }
    //}

    public void DestroyPopupByType(ePopupType _type)
    {
        Destroy(GetPopupByType(_type));
        int index = GetIndexOfPopup(_type);
        m_arrayMyPopup = RemoveAt(m_arrayMyPopup, index);
    }

    private int GetIndexOfPopup(ePopupType _type)
    {
        for(int i=0;i<m_arrayMyPopup.Length;i++)
        {
            if (m_arrayMyPopup[i].m_popupType == _type)
            {
                return i;
            }
        }
        return 0;
    }
    public T[] RemoveAt<T>(T[] oArray, int idx)
    {
        T[] nArray = new T[oArray.Length - 1];
        for (int i = 0; i < nArray.Length; ++i)
        {
            nArray[i] = (i < idx) ? oArray[i] : oArray[i + 1];
        }
        return nArray;
    }
    #endregion...
}
