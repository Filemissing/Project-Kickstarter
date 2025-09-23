using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class MoveNameAnimator : MonoBehaviour
{
    [HideInInspector] public TMP_Text text;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    [Header("Start")]
    [SerializeField] float startYPosition;
    [SerializeField] float startScale;
    [SerializeField] float startDuration;

    [Header("Wait")]
    [SerializeField] float waitTime;

    [Header("End")]
    [SerializeField] float endScale;
    [SerializeField] float endYPosition;
    [SerializeField] float endDuration;

    private IEnumerator Start()
    {
        transform.localScale = Vector3.one * startScale;
        transform.localPosition = Vector3.up * startYPosition;

        transform.DOScale(1, startDuration).SetEase(Ease.OutBack);
        transform.DOLocalMoveY(0, startDuration).SetEase(Ease.OutCubic);
        text.DOFade(1, startDuration).SetEase(Ease.OutCubic);

        yield return new WaitForSeconds(startDuration);

        yield return new WaitForSeconds(waitTime);

        transform.DOScale(endScale, endDuration).SetEase(Ease.InCubic);
        text.DOFade(0, endDuration).SetEase(Ease.InCubic);
        transform.DOLocalMoveY(endYPosition, endDuration).SetEase(Ease.InCubic);

        yield return new WaitForSeconds(endDuration);

        Destroy(gameObject);
    }
}
