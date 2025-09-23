using DG.Tweening;
using UnityEngine;

public class ShopButtonArea : MonoBehaviour
{
    [Header("Instances")]
    [SerializeField] RectTransform shopButton;
    [SerializeField] CanvasGroup canvasGroup;

    Vector2 defaultShopButtonPosition;


    void Awake()
    {
        defaultShopButtonPosition = shopButton.anchoredPosition;
    }

    void Appear()
    {
        shopButton.anchoredPosition = defaultShopButtonPosition + new Vector2(0, -80);
        shopButton.localScale = Vector3.one * .8f;
        canvasGroup.alpha = 0;
        
        shopButton.DOAnchorPos(defaultShopButtonPosition, .2f).SetEase(Ease.OutBack);
        shopButton.DOScale(1f, .2f).SetEase(Ease.OutBack);
        canvasGroup.DOFade(1, .2f).SetEase(Ease.OutBack);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    void Disappear()
    {
        shopButton.anchoredPosition = defaultShopButtonPosition;
        shopButton.localScale = Vector3.one;
        canvasGroup.alpha = 1;
        
        shopButton.DOAnchorPos(defaultShopButtonPosition + new Vector2(0, -80), .5f).SetEase(Ease.OutBack);
        shopButton.DOScale(.8f, .5f).SetEase(Ease.OutBack);
        canvasGroup.DOFade(0, .5f).SetEase(Ease.OutBack);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Appear();
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Disappear();
        }
    }
}
