using System;
using TMPro;
using UnityEngine;

public class MoneyLabel : MonoBehaviour
{
    TMP_Text text;
    private string endString = " Microplastics";
    

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    
    void Update()
    {
        text.text = Convert.ToString(GameManager.instance.playerStats.currency) + endString;
    }
}
