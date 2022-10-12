using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public Canvas canvas;

    public GameObject circlesPrefab;
    public Transform player,cam;

    public float value;
    public float minCircleDst;
    InteractionCircle activeCircles;

    public Vector2 clampValues;
    public float constrainValue;

    List<Animator> animators = new();

    public bool Clamped
    {
        get
        {
            if (activeCircles)
            {
                var pos = activeCircles.transform.position;
                if (pos.x > 0 + constrainValue && pos.x < clampValues.x - constrainValue && pos.y > 0 + constrainValue && pos.y < clampValues.y - constrainValue)
                    return false;
            }
            return true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dst = Vector3.Distance(transform.position, player.position);
        if(dst < minCircleDst)
        {
            EnableCircles(true);
        }
        else
        {
            EnableCircles(false);
        }

        if(activeCircles != null)
            UpdateCircle();
    }

    void EnableCircles(bool value)
    {
        if(activeCircles == null && value)
        {
            activeCircles = Instantiate(circlesPrefab, Vector3.zero, Quaternion.identity, canvas.transform).GetComponent<InteractionCircle>();

            activeCircles.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
        
        for (int i = 0; i < animators.Count; i++)
        {
            animators[i].SetBool("Active", value);
        }
    }

    void UpdateCircle()
    {
        clampValues = new(canvas.pixelRect.xMax,canvas.pixelRect.yMax);
        print(Clamped);

        Vector3 previousPos = activeCircles.transform.position;
        activeCircles.transform.position = Camera.main.WorldToScreenPoint(transform.position);

        activeCircles.transform.position = new Vector3
        {
            x = Mathf.Clamp(activeCircles.transform.position.x, 0 + constrainValue, clampValues.x - constrainValue),
            y = Mathf.Clamp(activeCircles.transform.position.y, 0 + constrainValue, clampValues.y - constrainValue)
        };
        activeCircles.gameObject.SetActive(Vector3.Dot(cam.forward, (transform.position - cam.position).normalized) >= 0);
        

        //activeCircles.transform.localScale = Vector3.one * (Vector3.Distance(transform.position, player.position) * value);
    }        
}
