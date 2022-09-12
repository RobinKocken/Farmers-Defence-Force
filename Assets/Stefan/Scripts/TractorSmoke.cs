using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorSmoke : MonoBehaviour
{
    public GameObject smokePrefab;
    public Transform spawnPoint;
    public float time;
    float timer;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = time;
            Destroy(Instantiate(smokePrefab, spawnPoint.position, Quaternion.identity, spawnPoint),3);
        }
    }
}
