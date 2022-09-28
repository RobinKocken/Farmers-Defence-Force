using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienManager : MonoBehaviour
{
    public GameObject alien;
    public Transform spawnPos;

    public List<GameObject> currentAliens;

    void Start()
    {
        spawnPos = GameObject.FindGameObjectWithTag("SpawnAlien").transform;
    }

    void Update()
    {
        
    }
}
