using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int currentWaypointIndex;

    public Transform[] waypoints;

    public Transform waypoint,cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waypoint.transform.position = Camera.main.WorldToScreenPoint(waypoints[currentWaypointIndex].position);
        waypoint.gameObject.SetActive(Vector3.Dot(cam.forward, (waypoint.position - cam.position).normalized) >= 0);
    }
}
