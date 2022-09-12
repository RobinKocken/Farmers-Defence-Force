using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public MainMenuButton backToMenuButton;
    public GameObject mainMenuHolder;

    void Start()
    {
        backToMenuButton.onButtonClicked += BackToMainMenu;
    }

    void BackToMainMenu()
    {
        mainMenuHolder.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
