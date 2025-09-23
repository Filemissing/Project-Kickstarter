using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class EncyclopediaEntry : MonoBehaviour
{
    public EnemyInfo enemyInfo;
    public Encyclopedia encyclopedia;
    public bool isHidden = true;

    [Header("Settings")]
    [SerializeField] private float blurRadius = 16;
    [SerializeField] private float blackness = 1;

    [Header("Instances")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private Image image;
    private Material material;


    void Awake()
    {
        material = Instantiate(image.material);
        image.material = material;
    }

    private bool previousIsHidden = false;
    private int previousListSize = 0;
    public void UpdateVisuals()
    {
        if (previousListSize != GameManager.instance.unlockedEnemyInfos.Count)
        {
            foreach (EnemyInfo enmyInfo in GameManager.instance.unlockedEnemyInfos)
            {
                if (enmyInfo == enemyInfo)
                {
                    isHidden = false;
                }
            }

            previousListSize = GameManager.instance.unlockedEnemyInfos.Count;
        }
        
        if (previousIsHidden != isHidden)
        {
            if (isHidden)
            {
                // Hidden
                string titleString = "";
                for (int i = 0; i < enemyInfo.name.Length; i++)
                {
                    if (enemyInfo.name[i] == ' ')
                    {
                        titleString += ' ';
                    }
                    else
                    {
                        titleString += '?';
                    }
                }
            
                title.text = titleString;
                image.sprite = enemyInfo.sprite100px;
                
                material.SetFloat("_BlurRadius", blurRadius);
                material.SetFloat("_Blackness", blackness);
                image.SetMaterialDirty();
            }
            else
            {
                // Visible
                title.text = enemyInfo.name;
                image.sprite = enemyInfo.sprite100px;
                
                material.SetFloat("_BlurRadius", 0);
                material.SetFloat("_Blackness", 0);
                image.SetMaterialDirty();
            }
        
            previousIsHidden = isHidden;
        }
    }
    
    public void OnClick()
    {
        if (!isHidden) { encyclopedia.UpdateFishPanel(enemyInfo); }
    }

    private void Update()
    {
        UpdateVisuals();
    }
}
