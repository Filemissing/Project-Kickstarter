using System;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopFocusedPanel : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Color purchaseButtonColor = Color.green;
    [SerializeField] Color equipButtonColor = Color.yellow;
    [SerializeField] Color equippedButtonColor = Color.grey;
    
    [Header("Instances")]
    CanvasGroup parentCanvasGroup;
    RectTransform rectTransform;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text cost;
    [SerializeField] TMP_Text description;
    [SerializeField] Image icon;
    [SerializeField] private Image buttonImage;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] TMP_Text[] attackTexts;
    [SerializeField] CanvasGroup[] attackCanvasGroups;
    [SerializeField] TMP_Text detailsPanelText;
    [SerializeField] CanvasGroup attacksParentCanvasGroup;

    private Vector2 defaultPosition;

    [Header("Classes")]
    public Weapon weapon;
    public Item item;

    
    // Data
    string titleString;
    string costString;
    string descriptionString;
    private Sprite iconSprite;
    
    public ButtonType buttonType = ButtonType.Purchase;
    public UsedClass usedClass;


    public enum ButtonType
    {
        Purchase,
        Equip,
        Equipped
    }
        
    public enum UsedClass
    {
        Weapon,
        Item,
        Upgrade
    }


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentCanvasGroup = rectTransform.parent.GetComponent<CanvasGroup>();
        defaultPosition = rectTransform.anchoredPosition;
    }

    void UpdateButtonType()
    {
        switch (usedClass)
        {
            case UsedClass.Weapon:
                bool hasWeapon = false;
                bool didAThing = false;
                
                foreach (Weapon wepon in GameManager.instance.playerStats.unlockedWeapons)
                {
                    if (wepon == weapon)
                    {
                        hasWeapon = true;
                    }
                    
                    if (weapon == GameManager.instance.playerStats.currentWeapon)
                    {
                        buttonType = ButtonType.Equipped;
                        didAThing = true;
                        break;
                    }
                }
                
                if (hasWeapon && !didAThing)
                {
                    buttonType = ButtonType.Equip;
                    didAThing = true;
                }
                
                if (!hasWeapon && !didAThing)
                {
                    buttonType = ButtonType.Purchase;
                    didAThing = true;
                }
                
                break;
            case UsedClass.Item:
                buttonType = ButtonType.Purchase;
                break;
            case UsedClass.Upgrade:
                
                break;
        }
    }

    void ConvertData(UsedClass usedClass)
    {
        switch (usedClass)
        {
            case UsedClass.Weapon:
                titleString = weapon.name;
                descriptionString = weapon.description;
                costString = Convert.ToString(weapon.cost);
                iconSprite = weapon.icon;
                break;
            case UsedClass.Item:
                titleString = item.name;
                descriptionString = item.description;
                costString = Convert.ToString(item.cost);
                iconSprite = item.icon;
                break;
            case UsedClass.Upgrade:
                
                break;
        }
    }

    public void UpdateVisualData()
    {
        ConvertData(usedClass);
        
        title.text = titleString;
        cost.text = costString;
        description.text = descriptionString;
        icon.sprite = iconSprite;

        UpdateButtonType();
        switch (buttonType)
        {
            case ButtonType.Purchase:
                buttonText.text = "Purchase";
                buttonImage.color = purchaseButtonColor;
                break;
            case ButtonType.Equip:
                buttonText.text = "Equip";
                buttonImage.color = equipButtonColor;
                break;
            case ButtonType.Equipped:
                buttonText.text = "Equipped";
                buttonImage.color = equippedButtonColor;
                break;
        }

        switch (usedClass)
        {
            case UsedClass.Weapon:
                attacksParentCanvasGroup.alpha = 1;
                
                for (int i = 0; i < attackTexts.Length; i++)
                {
                    TMP_Text attackText = attackTexts[i];
                    CanvasGroup attackCanvasGroup = attackCanvasGroups[i];

                    if (i < weapon.attacks.Count)
                    {
                        // Exists
                        attackText.text = weapon.attacks[i].name;
                        attackCanvasGroup.alpha = 1;
                    }
                    else
                    {
                        // Doesn't exist
                        attackText.text = "";
                        attackCanvasGroup.alpha = 0;
                    }
                }
                break;
            case UsedClass.Item:
                attacksParentCanvasGroup.alpha = 0;
                detailsPanelText.text = item.details;
                break;
            case UsedClass.Upgrade:
                break;
        }
    }

    public void OnClick()
    {
        switch (buttonType)
        {
            case ButtonType.Purchase:
                int price = Convert.ToInt32(costString);
                
                if (GameManager.instance.playerStats.currency >= price)
                {
                    GameManager.instance.playerStats.currency -= price;
                    
                    switch (usedClass)
                    {
                        case UsedClass.Weapon:
                            GameManager.instance.playerStats.unlockedWeapons.Add(weapon);
                            GameManager.instance.playerStats.currentWeapon = weapon;
                            
                            UpdateVisualData();
                            break;
                        case UsedClass.Item:
                            GameManager.instance.playerStats.items.Add(item);
                            
                            UpdateVisualData();
                            break;
                        case UsedClass.Upgrade:
                
                            break;
                    }
                }
                break;
            case ButtonType.Equip:
                switch (usedClass)
                {
                    case UsedClass.Weapon:
                        GameManager.instance.playerStats.currentWeapon = weapon;
                            
                        UpdateVisualData();
                        break;
                    case UsedClass.Item:
                        break;
                    case UsedClass.Upgrade:
                
                        break;
                }
                break;
            case ButtonType.Equipped:
                break;
        }
    }

    [Button]
    public void Appear()
    {
        parentCanvasGroup.alpha = 0;
        parentCanvasGroup.DOFade(1, .2f).SetEase(Ease.OutCubic);
        parentCanvasGroup.interactable = true;
        parentCanvasGroup.blocksRaycasts = true;
        
        rectTransform.localScale = Vector3.one;
        
        Vector2 offsetPosition = defaultPosition + new Vector2(0, 30);
        rectTransform.anchoredPosition = offsetPosition;
        rectTransform.DOAnchorPos(defaultPosition, 0.2f).SetEase(Ease.OutCubic);
    }
    
    public void Disappear()
    {
        parentCanvasGroup.alpha = 1;
        parentCanvasGroup.DOFade(0, .2f).SetEase(Ease.OutCubic);
        parentCanvasGroup.interactable = false;
        parentCanvasGroup.blocksRaycasts = false;
        
        rectTransform.localScale = Vector3.one;
        rectTransform.DOScale(Vector2.one * .9f, .2f).SetEase(Ease.OutCubic);
    }

    public void StartPanel(UsedClass usedClass)
    {
        ConvertData(usedClass);
        UpdateVisualData();
        Appear();
    }
}
