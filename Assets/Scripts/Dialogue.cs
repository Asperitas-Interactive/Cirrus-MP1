using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text m_textBox;

    [SerializeField] private string[] m_dialogues;
    
    public int m_defaultWaitTime { get; set; }
    public int m_defaultDelayTime { get; set; }

    public void SetString(int _count)
    {
        StartCoroutine(eSetString(m_defaultWaitTime, m_defaultDelayTime, m_dialogues[_count]));   
    }

    public void SetString(string _text)
    {
            int count = -1;
            int index = 0;
            foreach (var dialouge in m_dialogues)
            {
                if (dialouge == _text)
                {
                    count = index;
                    break;
                }

                index++;
            }

            if (count == -1)
            {
                throw new ArgumentException(
                    "This dialogue is not found in the Dialogue List, enable force mode to add this diaogue anyway");
            }
            StartCoroutine(eSetString(m_defaultWaitTime, m_defaultDelayTime, m_dialogues[count]));
            
        
    }

    public void ForceString(string _text)
    {
        StartCoroutine(eSetString(m_defaultWaitTime, m_defaultDelayTime, _text));
    }

    private IEnumerator eSetString(int _time, int _delay, string _text)
    
    {
        yield return new WaitForSeconds(_delay);
        m_textBox.SetText(_text);
        yield return new WaitForSeconds(_time);
        m_textBox.SetText("");
    }
    
    void Start()
    {
        m_defaultDelayTime = 0;
        m_defaultWaitTime = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
