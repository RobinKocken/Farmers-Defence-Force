using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienManager : MonoBehaviour
{
    public GameObject alien;
    public List<GameObject> currentAliens;

    void Start()
    {
        Instantiate(alien, transform.position = new Vector3(0, 0, 0), Quaternion.identity);
    }

    void Update()
    {
        
    }
}
