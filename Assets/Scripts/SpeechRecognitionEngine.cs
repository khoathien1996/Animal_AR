using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class SpeechRecognitionEngine : MonoBehaviour
{
    public string[] keywords_action = new string[] { "idle", "walk", "run", "attack", "change" };
    public string[] keywords_communication = new string[] {
                                                "hello", "hi",
                                                "goodbye", "see you",
                                                "How are you?",
                                                "What is your name?",
                                                //"Where are you from?","Where do you come from?",
                                                "Where do you live?",
                                                "How old are you?",                                               
                                                "I am sad", "I am in sadness",
                                                "I am happy", "I am in happiness"
    };

    public ConfidenceLevel confidence = ConfidenceLevel.Low;
    public GameObject o_Parent;
    public BaseAnimationManager[] children;

    protected PhraseRecognizer recognizer, recognizer_action;
    protected string word = "";
    protected int length;
    protected GameObject[] child;
    protected BaseAnimationManager[] animal;

    private void Start()
    {
        length = o_Parent.transform.childCount;
        child = new GameObject[length];
        animal = new BaseAnimationManager[length];
        for (int i = 0; i < o_Parent.transform.childCount; i++){
            child[i] = o_Parent.transform.GetChild(i).gameObject;
            animal[i] = child[i].transform.GetChild(0).gameObject.GetComponent<BaseAnimationManager>();
            print(child[i].name + animal[i].name);
        }
        //print("XONG ROI NHAAAAAAAAAAAAAA");

        recognizer = new KeywordRecognizer(keywords_communication, confidence);

        recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
        recognizer.Start();

        recognizer_action = new KeywordRecognizer(keywords_action, confidence);
        recognizer_action.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
        recognizer_action.Start();

    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        print("You just said " + word);
        if(word != null )
        {
            if (o_Parent)
            {
                for (int i = 0; i < length; i++)
                {
                    if (animal[i].isActiveAndEnabled)
                    {
                        print("Ten ne: " + animal[i].name);
                        Interactive.Instance.OnShow(animal[i], word);
                        //AudioManager.Instance.PlayAudioEnglishFromResoures(animal[i].name, false);
                    }
                    
                }
            }
        }
        
    }

    private void Update()
    {
        

    }

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }

        if (recognizer_action != null && recognizer_action.IsRunning)
        {
            recognizer_action.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer_action.Stop();
        }
    }
}
