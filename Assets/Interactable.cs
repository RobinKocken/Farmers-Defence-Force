using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class Interactable : MonoBehaviour
{
    [Header("References")]
    public Canvas canvas;
    public Transform parent;

    public GameObject circlesPrefab;
    public Transform player,cam;

    [Header("Interaction Settings")]
    public float minCircleDst = 12;
    public float minInteractDst = 3;

    public string interactionMethod = "Pickup";
    public KeyCode interactionKey = KeyCode.E;
    InteractionCircle activeCircles;

    Vector2 clampValues;
    public float constrainValue = 10;

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

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        parent = GameObject.FindGameObjectWithTag("Interaction").transform;
        circlesPrefab = Resources.Load("Circle Interactable",typeof(GameObject)) as GameObject;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    // Update is called once per frame
    void Update()
    {
        float dst = Vector3.Distance(transform.position, player.position);

        bool canSee = false;
        if(dst <= minInteractDst)
        {
            if(Physics.Raycast(cam.position,cam.forward,out RaycastHit hit, minInteractDst)) 
            {
                if (hit.transform == transform)
                    canSee = true;
            }
        }
        if (dst < minCircleDst)
        {
            EnableCircles(true, canSee);
        }
        else
        {
            EnableCircles(false,false);
        }

        if(activeCircles != null)
            UpdateCircle();
    }

    void EnableCircles(bool value, bool canPickup)
    {
        if(activeCircles == null && value)
        {
            activeCircles = Instantiate(circlesPrefab, Vector3.zero, Quaternion.identity, parent).GetComponent<InteractionCircle>();

            activeCircles.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
        if (activeCircles == null) return;

        activeCircles.SetState(value, canPickup, interactionKey, interactionMethod);

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
