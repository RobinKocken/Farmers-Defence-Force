using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform cam;
    public GameObject gun;
    public GameObject gunPlayer;
    public GameObject axe;
    public GameObject axePlayer;
    public RaycastHit hit;
    public int treeHp;
    public int treeTrunkHp;
    public int metalScrapTotal;
    public int plankTotal;
    public bool kanKanon1maken;
    public bool kanKanon2maken;
    public bool kanKanon3maken;
    public bool gunIsOpgepakt;
    public bool axeIsOpgepakt;
    public bool gunIsActief;
    public bool axeIsActief;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, 3))
        {
            Tree tree = hit.transform.root.GetComponent<Tree>();
            if (tree != null)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if(axeIsActief == true)
                    {
                        if (tree.canDamageTrunk == false)
                        {
                            tree.ChopTree(1);
                        }
                        else if(hit.transform.gameObject.CompareTag("TreeTrunk"))
                        {
                            tree.ChopTrunk(1);
                        }
                    } 
                }
            }
        }

        Debug.DrawRay(cam.position, cam.forward * 5, Color.red);
        if (Physics.Raycast(cam.position, cam.forward, out hit, 5))
        {
            if (hit.transform.gameObject.tag == "Gun")
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    gun.SetActive(false);
                    gunIsOpgepakt = true;
                }
            }
        }
        if (Physics.Raycast(cam.position, cam.forward, out hit, 5))
        {
            if (hit.transform.gameObject.tag == "Axe")
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    axe.SetActive(false);
                    axeIsOpgepakt = true;
                }
            }
        }

        if (gunIsOpgepakt == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                axePlayer.SetActive(false);
                gunPlayer.SetActive(true);
                axeIsActief = false;
                gunIsActief = true;
            }
        }
        if (axeIsOpgepakt == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                axePlayer.SetActive(true);
                gunPlayer.SetActive(false);
                axeIsActief = true;
                gunIsActief = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            axePlayer.SetActive(false);
            gunPlayer.SetActive(false);
            gunIsActief = false;
            axeIsActief = false;
        }

        if (metalScrapTotal == 5 && plankTotal == 5)
        {
            kanKanon1maken = true;
        }
        if (metalScrapTotal == 10 && plankTotal == 5)
        {
            kanKanon2maken = true;
        }
        if (metalScrapTotal == 5 && plankTotal == 10)
        {
            kanKanon3maken = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MetalScrap")
        {
            Destroy(collision.gameObject);
            metalScrapTotal += 1;
        }

        if (collision.gameObject.tag == "Plank")
        {
            Destroy(collision.gameObject);
            plankTotal += 1;
        }
    }
}
