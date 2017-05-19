/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/


using IBM.Watson.DeveloperCloud.DataTypes;
using IBM.Watson.DeveloperCloud.Services.SpeechToText.v1;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 414

namespace IBM.Watson.DeveloperCloud.Widgets
{
  /// <summary>
  /// Simple class for displaying the SpeechToText result data in the UI.
  /// </summary>
  public class SpeechDisplayWidget : Widget
  {

    public GameObject o_Parent;
    protected int length;
    protected GameObject[] child;
    protected BaseAnimationManager[] animal;
    #region Inputs
    [SerializeField]
    private Input m_SpeechInput = new Input("SpeechInput", typeof(SpeechToTextData), "OnSpeechInput");
    #endregion

    #region Widget interface
    /// <exclude />
    protected override string GetName()
    {
      return "SpeechDisplay";
    }
    #endregion

    #region Private Data

    [SerializeField]
    private bool m_ContinuousText = false;
    [SerializeField]
    private Text m_Output = null;
    [SerializeField]
    private InputField m_OutputAsInputField = null;
    [SerializeField]
    public Text m_OutputStatus = null;
    [SerializeField]
    private float m_MinConfidenceToShow = 0.5f;

    private string m_PreviousOutputTextWithStatus = "";
    private string m_PreviousOutputText = "";
    private float m_ThresholdTimeFromLastInput = 3.0f; //3 secs as threshold time. After 3 secs from last OnSpeechInput, we are considering input as new input
    private float m_TimeAtLastInterim = 0.0f;
    #endregion
    /*public void Start()
    {
      text_speech.SetActive(false);

      length = o_Parent.transform.childCount;
      child = new GameObject[length];
      animal = new BaseAnimationManager[length];
      for (int i = 0; i < o_Parent.transform.childCount; i++)
      {
        child[i] = o_Parent.transform.GetChild(i).gameObject;
        animal[i] = child[i].transform.GetChild(0).gameObject.GetComponent<BaseAnimationManager>();
        print(child[i].name + animal[i].name);
      }
    }*/
    #region Event Handlers
    private void OnSpeechInput(Data data)
    {
      length = o_Parent.transform.childCount;
      child = new GameObject[length];
      animal = new BaseAnimationManager[length];
      for (int i = 0; i < o_Parent.transform.childCount; i++)
      {
        child[i] = o_Parent.transform.GetChild(i).gameObject;
        animal[i] = child[i].transform.GetChild(0).gameObject.GetComponent<BaseAnimationManager>();
        print(child[i].name + animal[i].name);
      }

      if (m_Output != null || m_OutputAsInputField != null)
      {
        SpeechRecognitionEvent result = ((SpeechToTextData)data).Results;
        if (result != null && result.results.Length > 0)
        {
          string outputTextWithStatus = "";
          string outputText = "";

          if (Time.time - m_TimeAtLastInterim > m_ThresholdTimeFromLastInput)
          {
            if (m_Output != null)
              m_PreviousOutputTextWithStatus = m_Output.text;
            if (m_OutputAsInputField != null)
              m_PreviousOutputText = m_OutputAsInputField.text;
          }

          if (m_Output != null && m_ContinuousText)
            outputTextWithStatus = m_PreviousOutputTextWithStatus;

          if (m_OutputAsInputField != null && m_ContinuousText)
            outputText = m_PreviousOutputText;

          foreach (var res in result.results)
          {
            foreach (var alt in res.alternatives)
            {
              string text = alt.transcript;
              if (m_Output != null)
              {
                m_Output.text = string.Concat(outputTextWithStatus, string.Format(text));
                string word = text;
                if (word != null)
                {
                    for (int i = 0; i < length; i++)
                    {
                      if (animal[i].isActiveAndEnabled)
                      {
                        print("Ten ne: " + animal[i].name);
                        Interactive.Instance.OnShow(animal[i], word.Trim().ToLower());
                        //AudioManager.Instance.PlayAudioEnglishFromResoures(animal[i].name, false);
                      }
                    }                  
                }
              }

              if (m_OutputAsInputField != null)
              {
                if (!res.final || alt.confidence > m_MinConfidenceToShow)
                {
                  m_OutputAsInputField.text = string.Concat(outputText, " ", text);

                  if (m_OutputStatus != null)
                  {
                    m_OutputStatus.text = string.Format("{0}, {1:0.00}", res.final ? "bbbbbFinal" : "Interim", alt.confidence);                    
                  }
                }
              }

              if (!res.final)
                m_TimeAtLastInterim = Time.time;

            }
          }
        }
      }
    }
    #endregion
  }
}
