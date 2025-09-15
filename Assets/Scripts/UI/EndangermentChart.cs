using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndangermentChart : MonoBehaviour
{
    public enum Endangerment
    {
        Least_Concern,
        Near_Threatened,
        Vulnerable,
        Endangered,
        Critically_Endangered,
        Extinct_in_the_Wild,
        Extinct
    }
    
    public Endangerment selectedEndangerment;

    [Header("Settings")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;
    
    [SerializeField] private Image[] images;
    [SerializeField] private TMP_Text[] texts;
    [SerializeField] private string[] textsText;
    
    
    void SetEndangerment(int selectedIndex)
    {
        image.color = images[selectedIndex].color;
        text.color = texts[selectedIndex].color;
        text.text = textsText[selectedIndex];
    }
    
    [Button]
    public void Refresh()
    {
        switch (selectedEndangerment)
        {
            case Endangerment.Least_Concern:
                SetEndangerment(0);
                break;
            case Endangerment.Near_Threatened:
                SetEndangerment(1);
                break;
            case Endangerment.Vulnerable:
                SetEndangerment(2);
                break;
            case Endangerment.Endangered:
                SetEndangerment(3);
                break;
            case Endangerment.Critically_Endangered:
                SetEndangerment(4);
                break;
            case Endangerment.Extinct_in_the_Wild:
                SetEndangerment(5);
                break;
            case Endangerment.Extinct:
                SetEndangerment(6);
                break;
        }
    }
}
