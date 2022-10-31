using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    public int health;
    public GameObject hitPoint;

    public GameOverManager gameOverManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0) StartCoroutine(gameOverManager.GameOver(false));
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
