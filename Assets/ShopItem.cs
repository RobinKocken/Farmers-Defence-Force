using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public delegate void OnBuyItem(ShopItemData data);

    private OnBuyItem _onBuyItem;
    public OnBuyItem OnBuyItemEvent
    {
        get { return _onBuyItem; }
        set { _onBuyItem = value; }
    }

    public Image icon;
    public TextMeshProUGUI itemNameText, priceText;

    Button button;

    public static ShopItem selected;

    public void SetSelectedItem(ShopItem item)
    {
        if (item != null && item == selected)
        {
            print("a");
            selected.OnBuyItemEvent?.Invoke(selected.data);
        }

        selected = item;

        print("selected changed");
    }
    public Color ButtonColor
    {
        get
        {
            return button.targetGraphic.canvasRenderer.GetColor();
        }
    }   

    public TextMeshProUGUI[] allTexts;
    public Image[] allImages;

    ShopItemData data;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public void Initialize(ShopItemData data)
    {
        icon.sprite = data.item.icon;
        itemNameText.text = data.amount.ToString() + "x " + data.item.name;
        priceText.text = data.price.ToString();
        this.data = data;

        print("initialized");
    }

    public void SetBuyCallbackEvent(OnBuyItem callback) => OnBuyItemEvent += callback;

    private void Update()
    {
        for (int i = 0; i < allImages.Length; i++)
        {
            allImages[i].color = ButtonColor;
        }
        for (int i = 0; i < allTexts.Length; i++)
        {
            allTexts[i].color = ButtonColor;
        }
    }
}
