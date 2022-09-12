using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingText : MonoBehaviour
{
    public float time;
    public string[] messages;
    int messageIndex;
    float timer;

    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = time;
            messageIndex++;
            if (messageIndex >= messages.Length) messageIndex = 0;
            text.text = messages[messageIndex];
        }
    }
}
