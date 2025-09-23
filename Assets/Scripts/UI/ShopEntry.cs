using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class ShopEntry : MonoBehaviour
{
    public ShopFocusedPanel.UsedClass usedClass;
    
    [Header("Classes")]
    public Weapon weapon;
    public Item item;
    
    // Data
    string titleString;
    string costString;
    private Sprite iconSprite;
    
    [Header("Instances")]
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text cost;
    [SerializeField] Image icon;
    
    
    void ConvertData(ShopFocusedPanel.UsedClass usedClass)
    {
        switch (usedClass)
        {
            case ShopFocusedPanel.UsedClass.Weapon:
                titleString = weapon.name;
                costString = Convert.ToString(weapon.cost);
                iconSprite = weapon.icon;
                break;
            case ShopFocusedPanel.UsedClass.Item:
                titleString = item.name;
                costString = Convert.ToString(item.cost);
                iconSprite = item.icon;
                break;
            case ShopFocusedPanel.UsedClass.Upgrade:
                
                break;
        }
    }

    [Button]
    public void RefreshVisuals()
    {
        ConvertData(usedClass);
        title.text = titleString;
        cost.text = costString;
        icon.sprite = iconSprite;
    }
    
    public void OnClick()
    {
        ShopFocusedPanel shopFocusedPanel = BoatingManager.instance.shop.shopFocusedPanel;

        switch (usedClass)
        {
            case ShopFocusedPanel.UsedClass.Weapon:
                shopFocusedPanel.weapon = weapon;
                break;
            case ShopFocusedPanel.UsedClass.Item:
                shopFocusedPanel.item = item;
                break;
            case ShopFocusedPanel.UsedClass.Upgrade:
                
                break;
        }
        
        shopFocusedPanel.usedClass = usedClass;
        
        shopFocusedPanel.UpdateVisualData();
        shopFocusedPanel.Appear();
    }
}
