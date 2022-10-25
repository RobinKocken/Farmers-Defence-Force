using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    public float timeToDeath;

    void Update()
    {
        Destroy(gameObject, timeToDeath);
    }
}
