using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroAnimator : MonoBehaviour
{
    Canvas canvas;
    Vector2 resolution;

    [Header("Splash Panel")]
    [SerializeField] Image splashPanel;
    [SerializeField] float splashPanelSpeed;
    [SerializeField] float splashPanelWait;

    [Header("Picture")]
    [SerializeField] Image picture;
    [SerializeField] float pictureFadeDuration;
    [SerializeField] float pictureSpeed;
    [SerializeField] float pictureWait;

    [Header("Titles")]
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text subTitle;
    [SerializeField] float titlesSpeed;
    [SerializeField] float titlesWait;

    [SerializeField] float endSpeed;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        resolution = new Vector2(Screen.width, Screen.height);
    }

    public IEnumerator DoIntro()
    {
        splashPanel.rectTransform.DOMove(Vector3.zero + (Vector3)(resolution / 2), splashPanelSpeed).SetEase(Ease.InExpo);
        yield return new WaitForSeconds(splashPanelSpeed);

        picture.DOFade(1, pictureFadeDuration).SetEase(Ease.InExpo);
        picture.rectTransform.DOScale(1, pictureSpeed).SetEase(Ease.OutElastic);
        yield return new WaitForSeconds(pictureWait);

        title.rectTransform.DOMove(new Vector3(100, 100 + resolution.y / 2, 0), titlesSpeed).SetEase(Ease.InExpo);
        subTitle.rectTransform.DOMove(new Vector3(100, -25 + resolution.y / 2, 0), titlesSpeed).SetEase(Ease.InExpo);
        yield return new WaitForSeconds(titlesWait);

        splashPanel.rectTransform.DOKill();
        picture.rectTransform.DOKill();
        title.rectTransform.DOKill();
        subTitle.rectTransform.DOKill();

        transform.DOMove((Vector3)resolution * 2, endSpeed).SetEase(Ease.InExpo);
        yield return new WaitForSeconds(endSpeed);

        CombatUIManager.instance.ShowActionMenu();
        CombatManager.instance.playerCombat.StartTurn();
    }
}
