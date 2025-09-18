using System;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopFocusedPanel : MonoBehaviour
{
    [Header("Instances")]
    CanvasGroup parentCanvasGroup;
    RectTransform rectTransform;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text cost;
    [SerializeField] TMP_Text description;
    [SerializeField] Image icon;

    private Vector2 defaultPosition;

    [Header("Classes")]
    public Weapon weapon;
    public Item item;

    
    // Data
    string titleString;
    string costString;
    string descriptionString;
    private Sprite iconSprite;
    
    
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

    void UpdateVisualData()
    {
        title.text = titleString;
        cost.text = costString;
        description.text = descriptionString;
        icon.sprite = iconSprite;
    }

    [Button]
    void Appear()
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
