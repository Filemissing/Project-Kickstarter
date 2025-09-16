using System.Collections;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector2 startPosition = new Vector2(400, 443);
    [SerializeField] private Vector2 endPosition = new Vector2(-20, 443);
    [SerializeField] private Vector2 hidePosition = new Vector2(650, 443);
    [SerializeField] private float appearDuration = .5f;
    [SerializeField] private float disappearDuration = 1.2f;
    [SerializeField] private float duration = 5f;
    
    [Header("Instances")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TMP_Text fishName;
    [SerializeField] private Image fishImage;


    void SetValues(EnemyInfo enemyInfo)
    {
        fishName.text = enemyInfo.name;
        fishImage.sprite = enemyInfo.sprite150px;
    }
    
    void AppearTween()
    {
        IEnumerator AppearTweenIEnumerator()
        {
            rectTransform.anchoredPosition = startPosition;
            rectTransform.localScale = Vector3.one * .6f;
            
            rectTransform.DOAnchorPos(endPosition, appearDuration).SetEase(Ease.OutBack);
            rectTransform.DOScale(Vector3.one, appearDuration).SetEase(Ease.OutBack);
            
            yield return new WaitForSeconds(appearDuration + duration);
            
            rectTransform.DOAnchorPos(hidePosition, disappearDuration).SetEase(Ease.InCubic);
        }
        
        StartCoroutine(AppearTweenIEnumerator());
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = hidePosition;
    }
    
    public void PlayNotification(EnemyInfo enemyInfo)
    {
        SetValues(enemyInfo);
        AppearTween();
    }
}
