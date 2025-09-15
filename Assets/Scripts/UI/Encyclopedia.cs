using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Encyclopedia : MonoBehaviour
{
    [Header("Instances")]
    [SerializeField] private Image image500px;
    [SerializeField] private TMP_Text enemyNameText;
    [SerializeField] private TMP_Text enemyScientificNameText;
    [SerializeField] private EndangermentChart endangermentChart;
    [SerializeField] private Image naturalHabitat;
    
    [SerializeField] private TMP_Text descriptionText;
    
    
    public void UpdateFishPanel(EnemyInfo enemyInfo)
    {
        image500px.sprite = enemyInfo.picture500px;
        enemyNameText.text = enemyInfo.name;
        enemyScientificNameText.text = enemyInfo.scientificName;
        endangermentChart.selectedEndangerment = enemyInfo.endangerment;
        endangermentChart.Refresh();
        naturalHabitat.sprite = enemyInfo.naturalHabitat;
        
        descriptionText.text = enemyInfo.description;
    }
}
