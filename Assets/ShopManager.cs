using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ShopManager : MonoBehaviour
{
    [Header("Shop Items")]

    public GameObject itemPrefab;

    public List<ShopItemData> _shopItems = new();
    List<ShopItem> shopItems = new();

    List<GameObject> spawnedItems = new();

    public Transform Mask
    {
        get
        {
            return layout.transform;
        }
    }

    [Header("Scrollbar settings")]
    public float min = 20;
    public float Max
    {
        get
        {
            return (_shopItems.Count-1) * -128.20125f * 0.7497297f;
        }
    }
    public VerticalLayoutGroup layout;
    public Scrollbar scrollbar;
    [Range(0.001f,2f)]
    public float scrollMultiplier;

    private void Start()
    {
        CreateButtons();

        for (int i = 0; i < shopItems.Count; i++)
        {
            shopItems[i].SetBuyCallbackEvent(delegate { OnBuyItem(_shopItems[i]); } );
        }
    }
    private void Update()
    {
        scrollbar.value -= Input.mouseScrollDelta.y * scrollMultiplier;
    }
    public void SetSliderValue(float value)
    {
        print("a");
        layout.padding.top = (int)Mathf.Lerp(min, Max, value);
        layout.SetLayoutVertical();
    }

    public void CreateButtons()
    {
        print(Max);

        for (int i = 0; i < spawnedItems.Count; i++)
        {
            DestroyImmediate(spawnedItems[i]);
        }

        spawnedItems.Clear();
        shopItems.Clear();

        for (int i = 0; i < _shopItems.Count; i++)
        {
            var item = Instantiate(itemPrefab, Mask);
            spawnedItems.Add(item);
            shopItems.Add(item.GetComponent<ShopItem>());
            shopItems[i].Initialize(_shopItems[i]);
        }
    }

    /// <summary>
    /// Callback function for when an item is bought
    /// </summary>
    public void OnBuyItem(ShopItemData itemData)
    {
        print("Item gekocht");
    }
}

[Serializable]
public struct ShopItemData
{
    public Item item;

    public int price;
    public int amount;
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(ShopManager))]
public class ShopManagerEditor : Editor
{
    ShopManager shopManager;
    public override void OnInspectorGUI()
    {
        shopManager = (ShopManager)target;
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Buttons"))
        {
            shopManager.CreateButtons();
        }
    }
}
#endif