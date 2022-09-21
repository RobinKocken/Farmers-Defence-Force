using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpNotification : MonoBehaviour
{
    public Image[] borders;
    public Image powerUpIcon;

    float maxLifeTime;
    float lifeTime;
    bool initialized;

    float deathTimer;
    public AnimationCurve deathScale;

    public float scaleSmoothTime;
    Vector3 targetScale;
    Vector3 scaleVelocity;
    Vector3 posVelocity;
    public Color Color { 
        
        get
        {
            return borders[0].color;
        }
        set
        {
            for (int i = 0; i < borders.Length; i++)
            {
                borders[i].color = new(value.r,value.g,value.b);
            }
        }
    }

    public float FillAmount
    {
        get
        {
            return borders[0].fillAmount;
        }
        set
        {
            for (int i = 0; i < borders.Length; i++)
            {
                borders[i].fillAmount = value;
            }
        }
    }

    public void Initialize(Color color, Sprite icon, float time)
    {
        targetScale = transform.localScale;

        transform.localScale = Vector3.zero;
        lifeTime = time;
        maxLifeTime = time;

        this.Color = color;
        powerUpIcon.sprite = icon;

        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized) return;

        lifeTime -= Time.deltaTime;

        FillAmount = (Mathf.InverseLerp (0,maxLifeTime,lifeTime) + 0.0001f) / 2;

        if(lifeTime <= 0)
        {
            deathTimer += Time.deltaTime;

            float scale = deathScale.Evaluate(deathTimer);

            transform.localScale = new Vector3(scale, scale, scale);

            if(scale <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale,targetScale,ref scaleVelocity,scaleSmoothTime);
        }

    }
    public void MoveToPos(Vector3 pos,float posSmoothTime)
    {
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref posVelocity, posSmoothTime);
    }
}
