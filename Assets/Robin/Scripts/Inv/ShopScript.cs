using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public InventoryManager inventory;
    public Item currency;
    public Buy[] buy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Buy
{
    public Item itemToBuy;
    public int price;
}
