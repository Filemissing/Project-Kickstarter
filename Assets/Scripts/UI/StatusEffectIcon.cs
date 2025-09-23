using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectIcon : MonoBehaviour
{
    public StatusEffect statusEffect;
    public Image image;
    public TMP_Text text;

    private void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();
    }

    public void OnChanged()
    {
        image.sprite = statusEffect.icon;
        text.text = statusEffect.level.ToString();
    }
}
