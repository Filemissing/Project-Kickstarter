using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Encyclopedia : MonoBehaviour
{
    [Header("EnemyInfos")]
    public List<EnemyInfo> enemyInfos = new List<EnemyInfo>();
    [SerializeField] private List<EncyclopediaEntry> entries = new List<EncyclopediaEntry>();
    
    [Header("Instances")]
    [SerializeField] private Image image500px;
    [SerializeField] private TMP_Text enemyNameText;
    [SerializeField] private TMP_Text enemyScientificNameText;
    [SerializeField] private EndangermentChart endangermentChart;
    [SerializeField] private Image naturalHabitat;
    
    [SerializeField] private TMP_Text descriptionText;

    [SerializeField] private EncyclopediaEntry entryPrefab;
    [SerializeField] private RectTransform entriesParent;

    [SerializeField] private RectTransform fishPanel;
    private Vector2 defaultFishPanelPosition;
    
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;


    void Awake()
    {
        defaultFishPanelPosition = fishPanel.anchoredPosition;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        UpdateEntrys();
        UpdateFishPanel(entries[0].enemyInfo);
    }

    [Button]
    public void Appear()
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, .2f).SetEase(Ease.OutCubic);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        
        rectTransform.localScale = Vector3.one * 1.1f;
        rectTransform.DOScale(Vector2.one, .2f).SetEase(Ease.OutCubic);
    }

    public void Disappear()
    {
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0, .2f).SetEase(Ease.OutCubic);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        
        rectTransform.localScale = Vector3.one;
        rectTransform.DOScale(Vector2.one * 1.1f, .2f).SetEase(Ease.OutCubic);
    }

    void FishPanelAppearAnimation()
    {
        Vector2 offsetPosition = defaultFishPanelPosition + new Vector2(0, 30);
        fishPanel.anchoredPosition = offsetPosition;
        fishPanel.DOAnchorPos(defaultFishPanelPosition, 0.2f).SetEase(Ease.OutCubic);
    }
    
    public void UpdateFishPanel(EnemyInfo enemyInfo)
    {
        image500px.sprite = enemyInfo.sprite500px;
        enemyNameText.text = enemyInfo.name;
        enemyScientificNameText.text = enemyInfo.scientificName;
        endangermentChart.selectedEndangerment = enemyInfo.endangerment;
        endangermentChart.Refresh();
        naturalHabitat.sprite = enemyInfo.naturalHabitat;
        
        descriptionText.text = enemyInfo.description;

        FishPanelAppearAnimation();
    }

    [Button]
    public void UpdateEntrys()
    {
        foreach (EncyclopediaEntry entry in entries)
        {
            Destroy(entry.gameObject);
        }
        entries.Clear();
        
        for (int i = 0; i < enemyInfos.Count; i++)
        {
            // Data
            EncyclopediaEntry newEntry = Instantiate(entryPrefab);
            newEntry.enemyInfo = enemyInfos[i];
            newEntry.encyclopedia = this;
            newEntry.transform.SetParent(entriesParent);
            
            // Visuals
            newEntry.transform.GetChild(0).GetComponent<TMP_Text>().text = enemyInfos[i].name;
            newEntry.transform.GetChild(2).GetComponent<Image>().sprite = enemyInfos[i].sprite150px;
            
            entries.Add(newEntry);
        }
    }
}
