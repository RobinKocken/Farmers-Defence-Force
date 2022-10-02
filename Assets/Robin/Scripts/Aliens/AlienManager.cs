using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AlienManager : MonoBehaviour
{
    public WaveManager[] waveManager;

    public GameObject alien;
    public List<Transform> spawnPos;
    public List<GameObject> currentAliens;

    void Start()
    {
        Setup();
        StartCoroutine(Spawner());
    }

    void Update()
    {
        
    }

    void Setup()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            spawnPos.Add(gameObject.transform.GetChild(i));
        }
    }

    IEnumerator Spawner()
    {
        for(int i = 0; i < waveManager.Length; i++)
        {
            for(int x = 0; x < waveManager[i].numberOfUfos; x++)
            {
                GameObject currentAlien = Instantiate(alien, transform.position, Quaternion.identity);

                int number = Random.Range(0, spawnPos.Count);

                StartCoroutine(currentAlien.GetComponent<AlienAi>().Lerping(spawnPos[number].position));

                yield return new WaitForSeconds(waveManager[i].timeToSpawn);
            }
        }
        yield break;
    }
}

[System.Serializable]
public class WaveManager
{
    public int numberOfUfos;
    public int timeToSpawn;
}