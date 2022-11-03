using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static int selectedDayTime;

    public MainMenuButton start, options, credits, exit;

    public MainMenuButton day, sunset, night, daySelectBack;

    public GameObject optionsMenu, creditsMenu, loading, dayTimeSelect;

    public TextMeshProUGUI loadingText;

    public float loadingTime;
    public float startLoadTime;

    public GameObject mainTheme, loadingTheme;

    bool loadingGame;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        start.onButtonClicked += ChangeDayTime;
        options.onButtonClicked += ToOptions;
        credits.onButtonClicked += ToCredits;
        exit.onButtonClicked += Application.Quit;

        day.onButtonClicked += SetDay;
        day.onButtonClicked += StartGame;

        sunset.onButtonClicked += SetSunset;
        sunset.onButtonClicked += StartGame;

        night.onButtonClicked += SetNight;
        night.onButtonClicked += StartGame;

        daySelectBack.onButtonClicked += BackToMenu;
    }

    void BackToMenu()
    {
        gameObject.SetActive(true);
        dayTimeSelect.SetActive(false);
    }
    void ChangeDayTime()
    {
        dayTimeSelect.SetActive(true);
        gameObject.SetActive(false);
    }
    void SetDay() => selectedDayTime = 0;

    void SetSunset() => selectedDayTime = 1;
    void SetNight() => selectedDayTime = 2;

    void ToOptions()
    {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    void ToCredits()
    {
        creditsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    void StartGame()
    {
        MainMenuButton.startingGame = true;
        MainMenuFade.Fade(true);
        Invoke(nameof(StartFakeLoadingScreen),startLoadTime);
    }

    void StartFakeLoadingScreen()
    {
        mainTheme.SetActive(false);
        loadingTheme.SetActive(true);   
        loading.SetActive(true);
        loadingGame = true;
        Invoke( nameof(LoadGameScene), Random.Range(3,8));
    }

    void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
