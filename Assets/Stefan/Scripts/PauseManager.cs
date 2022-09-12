using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public HUDManager hud;
    public GameObject menu;
    public MainMenuButton @continue, options, quitmenu, quitgame;
    public GameObject optionsHolder;
    public static bool Paused { get; private set; }

    private void Start()
    {
        InitializeButtons();
    }
    /// <summary>
    /// Function used to pause or continue the game
    /// </summary>
    /// <param name="pause"></param>
    public void Pause(bool pause)
    {
        if (pause && Paused == false) OnPause();
        else if (pause == false && Paused) OnContinue();

        Paused = pause;
    }

    /// <summary>
    /// Function to toggle the pause menu on and off
    /// </summary>
    public void TogglePause() => Pause(!Paused);

    public void OnPause()
    {
        Time.timeScale = 0;

        menu.SetActive(true);

        SetCursor(true,CursorLockMode.Confined);
    }

    public void OnContinue()
    {
        Time.timeScale = 1;
        menu.SetActive(false);

        SetCursor(false, CursorLockMode.Locked);
    }

    void SetCursor(bool visisble, CursorLockMode mode)
    {
        Cursor.visible = visisble;
        Cursor.lockState = mode;
    }

    void InitializeButtons()
    {
        @continue.onButtonClicked = ContinueGame;
        options.onButtonClicked = ToOptions;
        quitmenu.onButtonClicked = ToMainMenu;
        quitgame.onButtonClicked = Application.Quit;
    }

    #region Button Methods

    void ContinueGame() => Pause(false);
    void ToOptions()
    {
        menu.SetActive(false);
        optionsHolder.SetActive(true);
    }

    void ToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    #endregion

    private void OnEnable()
    {
        hud.canToggleMenu = true;
    }

    private void OnDisable()
    {
        hud.canToggleMenu = false;
    }
}
