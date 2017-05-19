using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public enum _AudioType
{
    //ELEPHANT =1,
    //LION =2,
    //STAG =3,
    //FOX =14,
    //RABBIT =4,
    //BOAR =5,
    //BEAR =6,
    //SHEEP = 7,
    //COW =8,
    //CHICKEN =9,
    //HIPPOPOTAMUS =10,
    //ZEBRA = 11,
    //RHINOCEROS =12,
    //CROCODILE =13,

    //S_ELEPHANT = 101,
    //S_LION = 102,
    //S_STAG = 103,
    //S_FOX = 104,
    //S_RABBIT = 105,
    //S_BEAR = 106,
    //S_SHEEP = 107,
    //S_COW = 108,
    //S_CHICKEN = 109,
    //S_HIPPOPOTAMUS = 110,
    //S_ZEBRA = 111,
    //S_RHINOCEROS = 112,
    //S_CROCODILE = 113,
    //S_BOAR = 114
    BUTTON
}

[System.Serializable]
public class AudioConfig
{
    public AudioClip a_Clip;
    public _AudioType a_Type;
}
public class AudioManager : MonoSingleton<AudioManager>
{

    public List<AudioConfig> l_ListAudio = new List<AudioConfig>();

    private Dictionary<_AudioType, AudioClip> d_DictionaryAudio = new Dictionary<_AudioType, AudioClip>();

    public AudioSource a_AudioSource;

    private const string m_pathAudioEnglish = "Sound/English/";
    private const string m_pathAudioAnimal = "Sound/AnimalSound/";
    private const string m_pathAudioInteractiveSpeech = "Sound/InteractiveAudio/";

    public AudioClip test;
    // Use this for initialization
    void Start()
    {
        InitDictionary();
    }

    private void InitDictionary()
    {
        foreach (AudioConfig audio in l_ListAudio)
        {
            d_DictionaryAudio.Add(audio.a_Type, audio.a_Clip);
        }
    }

    public AudioClip GetAudioFromResources(string _audioName, string _path)
    {
        print("ten audio : " + _path + _audioName.ToLower());
        return Resources.Load<AudioClip>(_path + _audioName.ToLower());
    }

    public void PlayAudioEnglishFromResoures(string _audioName, bool _isPlayName = true)
    {
        AudioClip audioClip = null;
        if (_isPlayName)
        {
            audioClip = GetAudioFromResources(_audioName, m_pathAudioEnglish);
        }
        else
        {
            audioClip = GetAudioFromResources(_audioName, m_pathAudioAnimal);
        }
        if (a_AudioSource && !a_AudioSource.isPlaying)
        {
            a_AudioSource.clip = audioClip;
            a_AudioSource.PlayOneShot(audioClip);
        }
    }

    public AudioClip GetAudioClipByType(_AudioType type)
    {
        if (d_DictionaryAudio.ContainsKey(type))
        {
            return d_DictionaryAudio[type];
        }
#if UNITY_EDITOR
        Debug.Log("ko co thang audio nay");
#endif
        return null;
    }

    public void PlayAudioByType(_AudioType type)
    {
        AudioClip audioClip = GetAudioClipByType(type);
        if (a_AudioSource && !a_AudioSource.isPlaying)
        {
            a_AudioSource.clip = audioClip;
            a_AudioSource.PlayOneShot(audioClip);
        }
    }

    public Image m_btnSettingSound;
    public Sprite m_sprSound;
    public Sprite m_sprSoundMute;

    public void SettingSound(bool _isMute = true)
    {
        if (_isMute)
        {
            m_btnSettingSound.sprite = m_sprSoundMute;
        }
        else
        {
            m_btnSettingSound.sprite = m_sprSound;
        }
        a_AudioSource.mute = _isMute;
    }

    public void PlayInteractiveSpeech(string name, string word)
    {
        bool isDefault = false;
        GameObject speech_text = Interactive.Instance.text_speech;
        Text text = speech_text.transform.GetChild(0).gameObject.GetComponent<Text>();

        switch (word)
        {
            case "what is your name":
                StartCoroutine(Play_MyNameIs(name));
                text.text = "My name is " + name.ToUpper();
                break;
            case "how are you":
                text.text = "I'm fine, thank you!";
                Play_audio("Iamfine");
                break;
            case "hello":
            case "hi":
                text.text = "Hello!";
                Play_audio("hello");    //file name is hello
                break;
            case "goodbye":
            case "bye":
                text.text = "Goodbye and see you again!";
                Play_audio("good_bye"); //file name is goodbye
                break;
            case "i am sad":
            case "i am in sadness":
                text.text = "Cheer up!";
                Play_audio("cheer_up");
                break;
            case "i am happy":
            case "i am in happiness":
                text.text = "Great! Congratulation!";
                Play_audio("great");
                break;
            default:
                isDefault = true;
                break;
        }
        if (!isDefault)
        {
            speech_text.SetActive(true);
            StartCoroutine(AutoHide(speech_text, 2));
        }

    }

    IEnumerator AutoHide(GameObject gameobject, int second)
    {
        yield return new WaitForSeconds(second);
        gameobject.SetActive(false);
    }
    IEnumerator Play_MyNameIs(string name)
    {
        AudioClip animal_name = null;
        AudioClip audioclip = null;
        audioclip = GetAudioFromResources("mynameis", m_pathAudioInteractiveSpeech);
        animal_name = GetAudioFromResources(name, m_pathAudioEnglish);

        if (a_AudioSource && !a_AudioSource.isPlaying)
        {
            a_AudioSource.clip = animal_name;
            //print(Time.time);
            a_AudioSource.PlayOneShot(audioclip);

            yield return new WaitForSeconds(1);
            //print(Time.time);
            a_AudioSource.PlayOneShot(animal_name);
        }
    }

    public void Play_audio(string word)
    {
        AudioClip audioclip = null;
        audioclip = GetAudioFromResources(word, m_pathAudioInteractiveSpeech);
        if (a_AudioSource && !a_AudioSource.isPlaying)
        {
            a_AudioSource.PlayOneShot(audioclip);
        }
    }
}
