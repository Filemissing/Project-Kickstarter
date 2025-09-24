using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Instances")]
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    [SerializeField] RectTransform[] tabs;
    [SerializeField] RectTransform[] tabPanels;
    [SerializeField] CanvasGroup[] tabPanelCanvasGroups;
    [SerializeField] Image[] tabImages;
    public ShopFocusedPanel shopFocusedPanel;

    [Header("Settings")]
    [SerializeField] private Color tabImageColorSelected;
    [SerializeField] private Color tabImageColorDeselected;

    
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        ChangeTabs(tabs[0]);
    }
    
    [Button]
    public void Appear()
    {
        BoatingManager.instance.canPlayerMoveList.Add(this);
        
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, .2f).SetEase(Ease.OutCubic);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        
        rectTransform.localScale = Vector3.one * 1.1f;
        rectTransform.DOScale(Vector2.one, .2f).SetEase(Ease.OutCubic);
    }

    public void Disappear()
    {
        BoatingManager.instance.canPlayerMoveList.Remove(this);
        
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0, .2f).SetEase(Ease.OutCubic);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        
        rectTransform.localScale = Vector3.one;
        rectTransform.DOScale(Vector2.one * 1.1f, .2f).SetEase(Ease.OutCubic);
    }

    public void ChangeTabs(RectTransform tab)
    {
        int selectedTabIndex = 0;
        for (int i = 0; i < tabs.Length; i++)
        {
            if (tab == tabs[i])
            {
                selectedTabIndex = i;
                break;
            }
        }
        /*
        foreach (Image tabImage in tabImages)
        {
            tabImage.material.color = tabImageColorDeselected;
        }
        */
        foreach (CanvasGroup tabPanelCanvasGroup in tabPanelCanvasGroups)
        {
            tabPanelCanvasGroup.alpha = 0;
            tabPanelCanvasGroup.interactable = false;
            tabPanelCanvasGroup.blocksRaycasts = false;
        }
        
        tabPanelCanvasGroups[selectedTabIndex].alpha = 1;
        tabPanelCanvasGroups[selectedTabIndex].interactable = true;
        tabPanelCanvasGroups[selectedTabIndex].blocksRaycasts = true;
        
        //tabImages[selectedTabIndex].color = tabImageColorSelected;

        for (int i = 0; i < tabPanels[selectedTabIndex].GetChild(0).GetChild(0).childCount; i++)
        {
            ShopEntry entry = tabPanels[selectedTabIndex].GetChild(0).GetChild(0).GetChild(i).GetComponent<ShopEntry>();
            entry.RefreshVisuals();
        }
    }
}
