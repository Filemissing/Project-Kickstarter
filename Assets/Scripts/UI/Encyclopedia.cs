using System.Collections.Generic;
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


    void Awake()
    {
        UpdateEntrys();
        UpdateFishPanel(entries[0].enemyInfo);
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
            newEntry.transform.GetChild(1).GetComponent<Image>().sprite = enemyInfos[i].sprite150px;
            
            entries.Add(newEntry);
        }
    }
}
