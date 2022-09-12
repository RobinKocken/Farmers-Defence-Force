using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBackground : MonoBehaviour
{
    public Sprite[] pictures;
    int spriteIndex;
    public Image image;

    float currentTimer;
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        currentTimer = waitTime;
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;
        if(currentTimer <= 0)
        {
            spriteIndex = Random.Range(0,pictures.Length);
            if (spriteIndex >= pictures.Length) spriteIndex = 0;
            image.sprite = pictures[spriteIndex];
            currentTimer = waitTime;
        }
    }
}
