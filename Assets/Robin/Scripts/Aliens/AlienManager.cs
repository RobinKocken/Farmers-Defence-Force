using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
using Unity.VisualScripting;

public class AlienManager : MonoBehaviour
{
    [Header("Alien")]
    public WaveManager[] waveManager;

    public GameObject alienSpawner;
    public GameObject alien;
    public List<Transform> spawnPos;
    public List<GameObject> currentAliens;

    public HouseScript house;

    bool timerIsRunning;
    int number;
    float disMin;
    float disSec;

    [Header("UI")]
    public Slider healthbarHouse;
    public TextMeshProUGUI rounds;
    public TextMeshProUGUI timer;

    [Header("Raycast")]
    public float distance;
    RaycastHit hit;

    public float offset;
    void Start()
    {
        Setup();
        StartCoroutine(Spawner());

        healthbarHouse.maxValue = house.health;

        
    }

    void Update()
    {
        Timer();
        GameCondition();

        healthbarHouse.value = house.health;
    }


    void Setup()
    {
        offset = alien.GetComponent<NavMeshAgent>().baseOffset;

        for(int i = 0; i < alienSpawner.transform.childCount; i++)
        {
            spawnPos.Add(alienSpawner.transform.GetChild(i));
        }

        
        for(int i = 0; i < spawnPos.Count; i++)
        {
            if(Physics.Raycast(spawnPos[i].position, Vector3.down, out hit))
            {
                distance = hit.distance;

                if(distance != offset + hit.point.y)
                {
                    spawnPos[i].position = new Vector3(spawnPos[i].position.x, offset + hit.point.y, spawnPos[i].position.z);
                }
            }
        }
    }

    void GameCondition()
    {
        if(house.health <= 0)
        {

        }
    }

    void Timer()
    {
        if(timerIsRunning)
        {
            if(waveManager[number].time > 0)
            {
                waveManager[number].time -= Time.deltaTime;
                CountDown(waveManager[number].time);
            }
            else
            {
                disMin = 0;
                disSec = 0;
                timerIsRunning = false;
            }
        }

        timer.text = string.Format("{0:00}:{1:00}", disMin, disSec);
    }

    IEnumerator Spawner()
    {
        for(int i = 0; i < waveManager.Length; i++)
        {
            number = i;
            timerIsRunning = true;
            rounds.text = waveManager[i].description;

            if(waveManager[i].isWave)
            {
                GameStats.rounds = i;
                for(int x = 0; x < waveManager[i].numberOfUfos; x++)
                {
                    GameObject currentAlien = Instantiate(alien, alienSpawner.transform.position, Quaternion.identity);

                    int number = Random.Range(0, spawnPos.Count);

                    StartCoroutine(currentAlien.GetComponent<AlienAi>().Lerping(spawnPos[number].position));

                    yield return new WaitForSeconds(waveManager[i].intervalsToSpawn);
                }
            }
            yield return new WaitUntil(() => !timerIsRunning);
        }
        yield break;
    }

    void CountDown(float timeToDisplay)
    {
        timeToDisplay += 1;
        float min = Mathf.FloorToInt(timeToDisplay / 60);
        float sec = Mathf.FloorToInt(timeToDisplay % 60);

        disMin = min;
        disSec = sec;
    }
}

[System.Serializable]
public class WaveManager
{
    public bool isWave;
    public float time;
    public string description;
    public int numberOfUfos;
    public float intervalsToSpawn;
}