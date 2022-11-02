using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuButton : MainMenuButton
{
    public GameObject settingsHolder;

    public delegate void OnClicked(int index);
    public OnClicked onClicked;

    public int optionsIndex;

    public override void OnClick()
    {
        if(onClicked != null)
            onClicked(optionsIndex);
        
        MainAudioSource.Play2DSound(clicked);
    }

    public void Disable()
    {
        settingsHolder.SetActive(false);
    }
}
