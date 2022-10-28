using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public bool isDead;

    public Camera deathCam;
    public Animator fadeAnimator;
    public GameObject gameOverCanvas, player;

    public GameObject loading;

    public MainMenuButton retry, toMenu;
    public TextMeshProUGUI survivedTime, rounds, towersPlaced, metalScrapSpend, ufosShotdown;
    bool canClick = true;

    bool loaded;

    public GameObject[] destroyOnGameOver;
    public float fadeWaitTime;

    private void Update()
    {
        if(GameStats.playing)
        GameStats.survivedTime += Time.deltaTime;
    }
    public IEnumerator GameOver()
    {
        if (isDead) yield return null;

        GameStats.playing = false;

        retry.onButtonClicked += Retry;
        toMenu.onButtonClicked += ToMenu;

        string spaces = "     ";

        float timeToDisplay = 1 + GameStats.survivedTime;
        float min = Mathf.FloorToInt(timeToDisplay / 60);
        float sec = Mathf.FloorToInt(timeToDisplay % 60);

        survivedTime.text = $"Time survived{spaces}{min} | {sec}";

        rounds.text = $"Rounds{spaces}{GameStats.rounds}";
        towersPlaced.text = $"Towers Placed{spaces}{GameStats.towersPlaced}";
        metalScrapSpend.text = $"Metal Scrap Spend{spaces}{GameStats.scrapUsed}";
        ufosShotdown.text = $"Ufos shotdown{spaces}{GameStats.ufosShotDown}";

        isDead = true;
        fadeAnimator.SetBool("Dead", true);

        yield return new WaitForSeconds(fadeWaitTime);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        var cameras = Camera.allCameras;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != deathCam) Destroy(cameras[i].gameObject);
        }

        var canvases = Resources.FindObjectsOfTypeAll(typeof(Canvas));

        for (int i = 0; i < canvases.Length; i++)
        {
            if (!canvases[i] == gameOverCanvas) Destroy(cameras[i].gameObject);
        }

        for (int i = 0; i < destroyOnGameOver.Length; i++)
        {
            Destroy(destroyOnGameOver[i]);
        }

        deathCam.gameObject.SetActive(true);

        foreach (Transform item in gameOverCanvas.transform)
        {
            if (item.gameObject == loading) continue;
            item.gameObject.SetActive(true);
        }
    }

    void ToMenu()
    {
        canClick = false;
        if (!loaded)
        {
            StartCoroutine(LoadLoadingScreen(false));

            fadeAnimator.SetBool("Loading", true);
            loaded = true;
        }
    }

    void Retry()
    {
        canClick = false;
        if (!loaded)
        {
            StartCoroutine(LoadLoadingScreen(true));

            fadeAnimator.SetBool("Loading", true);
            loaded = true;
        }
    }

    IEnumerator LoadLoadingScreen(bool retry)
    {
        if (loaded) yield return null;

        yield return new WaitForSeconds(2);
        loading.SetActive(true);


        yield return new WaitForSeconds(UnityEngine.Random.Range(3, 8));

        if (retry)
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }
    }
}
