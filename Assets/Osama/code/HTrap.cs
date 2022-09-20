using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTrap : MonoBehaviour
{
    public RaycastHit hit;
    public GameObject ufo;
    public GameObject bom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.up * 10, Color.blue);
        if (Physics.Raycast(transform.position, transform.up, out hit, 10))
        {
            if (hit.transform.gameObject.tag == "UFO")
            {
                print("hit me up");
                Destroy(ufo);
                Destroy(bom);


            }
        }
    }
}
