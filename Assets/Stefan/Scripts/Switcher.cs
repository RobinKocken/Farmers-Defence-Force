using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Switcher : MonoBehaviour
{
    public OptionsMainMenu optionsMenu;
    public MainMenuButton leftButton, rightButton;
    public TextMeshProUGUI switchedText, aspectRatio;
    public MainMenuButton applyButton;

    public Resolution[] resolutions;
    int currentResolutionIndex;

    // Start is called before the first frame update
    void Start()
    {
        leftButton.onButtonClicked = OnLeftClick;
        rightButton.onButtonClicked = OnRightClick;
        applyButton.onButtonClicked = Apply;
    }

    void OnLeftClick()
    {
        currentResolutionIndex--;
        if (currentResolutionIndex < 0) currentResolutionIndex = resolutions.Length - 1;

        switchedText.text = $"{resolutions[currentResolutionIndex].x}  x  {resolutions[currentResolutionIndex].y}";
        aspectRatio.text = resolutions[currentResolutionIndex].aspectRatio;
        applyButton.gameObject.SetActive(true);
    }

    void OnRightClick()
    {
        currentResolutionIndex++;
        if (currentResolutionIndex >= resolutions.Length) currentResolutionIndex = 0;

        switchedText.text = $"{resolutions[currentResolutionIndex].x}  x  {resolutions[currentResolutionIndex].y}";
        aspectRatio.text = resolutions[currentResolutionIndex].aspectRatio;
        applyButton.gameObject.SetActive(true);
    }

    void Apply()
    {
        Screen.SetResolution(resolutions[currentResolutionIndex].x, resolutions[currentResolutionIndex].y, optionsMenu.Fullscreen);
        applyButton.gameObject.SetActive(false);
    }

    [System.Serializable]
    public struct Resolution
    {
        public string aspectRatio;

        public int x;
        public int y;

    }
}
