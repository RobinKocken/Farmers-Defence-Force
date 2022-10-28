using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int hp;
    public int trunkHp;
    public bool canDamageTrunk;
    public GameObject middle;
    public GameObject stump;
    public GameObject trunk;
    public Transform toRotate;
    public GameObject planksFromTree;
    public GameObject rotatestammetje;
    // Start is called before the first frame update
    void Start()
    {
        toRotate.Rotate(new Vector3(0,Random.Range(0,360),0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hubert()
    {
        Destroy(middle);
        stump.SetActive(true);
        trunk.SetActive(true);
        rotatestammetje.SetActive(true);
    }
    public void ChopTree(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Hubert();
            canDamageTrunk = true;
            trunk.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void ChopTrunk(int damage)
    {
        if (canDamageTrunk == true)
        {
            trunkHp -= damage;
            if(trunkHp <= 0)
            {
                trunk.transform.DetachChildren();
                Destroy(trunk);
                planksFromTree.SetActive(true);
            }
        }
    }
}
