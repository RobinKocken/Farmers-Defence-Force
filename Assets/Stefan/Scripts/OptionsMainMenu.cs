using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMainMenu : MonoBehaviour
{
    public OptionsMenuButton audioButton, videoButton, gameplayButton;
    public Slider volume, xsens, ysens;
    public HUDManager hud;

    public bool Fullscreen 
    { 
        get
        {
            return Screen.fullScreen;
        }
        set
        {
            Screen.fullScreen = value;
        }
    }

    public GameObject[] optionsMenu;

    public MainMenuButton backToMenuButton;
    public GameObject mainMenuHolder;
    // Start is called before the first frame update
    void Start()
    {

        backToMenuButton.onButtonClicked += BackToMainMenu;

        audioButton.onClicked += ChangeOptionsMenu;
        videoButton.onClicked += ChangeOptionsMenu;
        gameplayButton.onClicked += ChangeOptionsMenu;
    }

    private void OnEnable()
    {
        if (hud)
        {
            hud.canToggleMenu = false;
        }
        GetValues();
    }

    private void Update()
    {
        if (Input.GetButtonUp("Escape")) backToMenuButton.OnClick();
    }

    private void OnDisable()
    {
        if (hud)
        {
            hud.canToggleMenu = true;
        }
    }

    public void ToggleFullscreen(bool fullscreen)
    {
        this.Fullscreen = fullscreen;
    }
    public void ToggleVsync(bool vSync)
    {
        QualitySettings.vSyncCount = vSync ? 1 : 0;
    }

    void BackToMainMenu()
    {
        mainMenuHolder.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void ChangeOptionsMenu(int index)
    {
        for (int i = 0; i < optionsMenu.Length; i++)
        {
            if (i == index)
                optionsMenu[i].SetActive(true);
            else
                optionsMenu[i].SetActive(false);
        }
    }

    void GetValues()
    {
        volume.value = AudioListener.volume;
        xsens.value = Options.xSens;
        ysens.value = Options.ySens;
    }
}
