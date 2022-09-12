using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Switcher : MonoBehaviour
{
    public OptionsMainMenu optionsMenu;
    public MainMenuButton leftButton, rightButton;
    public TextMeshProUGUI switchedText;
    public Vector2Int[] resolutions;
    int currentResolutionIndex;

    // Start is called before the first frame update
    void Start()
    {
        leftButton.onButtonClicked = OnLeftClick;
        rightButton.onButtonClicked = OnRightClick;
    }

    void OnLeftClick()
    {
        currentResolutionIndex--;
        if (currentResolutionIndex < 0) currentResolutionIndex = resolutions.Length - 1;

        Screen.SetResolution(resolutions[currentResolutionIndex].x, resolutions[currentResolutionIndex].y, optionsMenu.Fullscreen);

        switchedText.text = $"{resolutions[currentResolutionIndex].x}  x  {resolutions[currentResolutionIndex].y}";
    }

    void OnRightClick()
    {
        currentResolutionIndex++;
        if (currentResolutionIndex >= resolutions.Length) currentResolutionIndex = 0;

        Screen.SetResolution(resolutions[currentResolutionIndex].x, resolutions[currentResolutionIndex].y, optionsMenu.Fullscreen);
        switchedText.text = $"{resolutions[currentResolutionIndex].x}  x  {resolutions[currentResolutionIndex].y}";

    }
}
