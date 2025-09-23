using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonEffects : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Vector3 originalScale;
    
    private float pressScale = 0.85f;
    private float pressDuration = 0.1f;
    private float releaseDuration = 0.15f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.DOScale(originalScale * pressScale, pressDuration).SetEase(Ease.InCubic);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        rectTransform.DOScale(originalScale, releaseDuration).SetEase(Ease.OutBack);
    }
}
