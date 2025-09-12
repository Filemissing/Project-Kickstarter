using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatUIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup actionMenu;
    [SerializeField] CanvasGroup attackMenu;
    [SerializeField] CanvasGroup itemsMenu;
    [SerializeField] CanvasGroup infoMenu;

    [SerializeField] Button buttonPrefab;

    public void ShowActionMenu()
    {
        ShowCanvasGroup(actionMenu);
        HideCanvasGroup(attackMenu);
        HideCanvasGroup(itemsMenu);
        HideCanvasGroup(infoMenu);
    }
    public void ShowAttackMenu()
    {
        ConstructAttckMenu();

        HideCanvasGroup(actionMenu);
        ShowCanvasGroup(attackMenu);
        HideCanvasGroup(itemsMenu);
        HideCanvasGroup(infoMenu);
    }
    public void ShowItemsMenu()
    {
        ConstructItemsMenu();

        HideCanvasGroup(actionMenu);
        HideCanvasGroup(attackMenu);
        ShowCanvasGroup(itemsMenu);
        HideCanvasGroup(infoMenu);
    }
    public void ShowInfoMenu()
    {
        HideCanvasGroup(actionMenu);
        HideCanvasGroup(attackMenu);
        HideCanvasGroup(itemsMenu);
        ShowCanvasGroup(infoMenu);
    }

    void ShowCanvasGroup(CanvasGroup group)
    {
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }
    void HideCanvasGroup(CanvasGroup group)
    {
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }

    void ConstructAttckMenu()
    {
        foreach (Transform child in attackMenu.transform)
        {
            Destroy(child.gameObject);
        }

        Attack[] attacks = GetAttacks();

        Debug.Log("Constructing Attack Menu with " + attacks.Length + " attacks.");

        Button[] buttons = new Button[attacks.Length];
        for (int i = 0; i < attacks.Length; i++)
        {
            buttons[i] = Instantiate(buttonPrefab, attackMenu.transform);
            buttons[i].GetComponentInChildren<TMP_Text>().text = attacks[i].name;
            buttons[i].onClick.AddListener(delegate { GetAttacks()[i].Execute(CombatManager.instance.playerCombat, CombatManager.instance.enemy); });

            Debug.Log("Added button for attack: " + attacks[i].name + " with index: " + i);
        }
    }
    void ConstructItemsMenu()
    {
        foreach (Transform child in attackMenu.transform)
        {
            Destroy(child.gameObject);
        }

        Item[] items = CombatManager.instance.playerCombat.playerStats.items.ToArray();

        Button[] buttons = new Button[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            buttons[i] = Instantiate(buttonPrefab, itemsMenu.transform);
            buttons[i].GetComponentInChildren<TMP_Text>().text = items[i].name;
            buttons[i].onClick.AddListener(delegate { items[i].Use(CombatManager.instance.playerCombat, CombatManager.instance.enemy); });
        }
    }

    Attack[] GetAttacks()
    {
        return CombatManager.instance.playerCombat.playerStats.currentWeapon.attacks.ToArray();
    }
}
