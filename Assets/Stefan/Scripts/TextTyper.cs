using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTyper : MonoBehaviour
{
    public string typedText;

    public float time = 0.1f;

    public bool repeat;

    string m_TypedText = "";
    int currentIndex;
    float currentTimer;

    protected virtual void Update()
    {
        currentTimer -= Time.deltaTime;
        if(currentTimer <= 0)
        {
            currentTimer = time;
            currentIndex++;
            if (currentIndex >= typedText.Length && repeat) m_TypedText = "";
            m_TypedText.Insert(currentIndex,typedText[currentIndex].ToString());
        }
    }
    public string GetNewString()
    {
        return m_TypedText;
    }
}
