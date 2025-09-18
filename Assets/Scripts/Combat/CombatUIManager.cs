using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatUIManager : MonoBehaviour
{
    public static CombatUIManager instance;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            throw new System.Exception("Multiple instances of CombatUIManager detected!");
    }

    [Header("Menus")]
    [SerializeField] CanvasGroup actionMenu;
    [SerializeField] CanvasGroup attackMenu;
    [SerializeField] CanvasGroup itemsMenu;

    [Header("Intro")]
    [SerializeField] CanvasGroup introGroup;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text subTitle;
    [SerializeField] Image image;

    [Header("Prefabs")]
    [SerializeField] Button buttonPrefab;
    [SerializeField] Button backButtonPrefab;

    public void HideAllMenus()
    {
        HideCanvasGroup(actionMenu);
        HideCanvasGroup(attackMenu);
        HideCanvasGroup(itemsMenu);
        HideCanvasGroup(introGroup);
    }
    public void ShowActionMenu()
    {
        ShowCanvasGroup(actionMenu);
        HideCanvasGroup(attackMenu);
        HideCanvasGroup(itemsMenu);
        HideCanvasGroup(introGroup);
    }
    public void ShowAttackMenu()
    {
        ConstructAttckMenu();

        HideCanvasGroup(actionMenu);
        ShowCanvasGroup(attackMenu);
        HideCanvasGroup(itemsMenu);
        HideCanvasGroup(introGroup);
    }
    public void ShowItemsMenu()
    {
        ConstructItemsMenu();

        HideCanvasGroup(actionMenu);
        HideCanvasGroup(attackMenu);
        ShowCanvasGroup(itemsMenu);
        HideCanvasGroup(introGroup);
    }
    public void ShowIntro(EnemyInfo info)
    {
        title.text = info.name;
        subTitle.text = info.catchPhrase;
        image.sprite = info.sprite500px;

        HideCanvasGroup(actionMenu);
        HideCanvasGroup(attackMenu);
        HideCanvasGroup(itemsMenu);
        ShowCanvasGroup(introGroup);
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

        AddBackButton(attackMenu.transform);

        Attack[] attacks = GetAttacks();
        for (int i = 0; i < attacks.Length; i++)
        {
            Button button = Instantiate(buttonPrefab, attackMenu.transform);
            button.GetComponentInChildren<TMP_Text>().text = attacks[i].name;

            int index = i; // Capture the current value of i
            button.onClick.AddListener(delegate { GetAttacks()[index].Execute(CombatManager.instance.playerCombat, CombatManager.instance.enemy); });
        }
    }
    public void ConstructItemsMenu()
    {
        foreach (Transform child in itemsMenu.transform)
        {
            Destroy(child.gameObject);
        }

        AddBackButton(itemsMenu.transform);

        Item[] items = CombatManager.instance.playerCombat.playerStats.items.ToArray();

        List<Item> filteredItems = new();
        foreach (Item item in items)
        {
            if(item != null && !filteredItems.Contains(item))
                filteredItems.Add(item);
        }

        items = filteredItems.ToArray();

        Button[] buttons = new Button[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            Button button = Instantiate(buttonPrefab, itemsMenu.transform);
            button.GetComponentInChildren<TMP_Text>().text = items[i].name;

            int index = i; // Capture the current value of i
            button.onClick.AddListener(delegate { items[index].Use(CombatManager.instance.playerCombat, CombatManager.instance.enemy); });
        }
    }

    public void AddBackButton(Transform parentMenu)
    {
        Instantiate(backButtonPrefab, parentMenu).onClick.AddListener(ShowActionMenu);
    }

    Attack[] GetAttacks()
    {
        return CombatManager.instance.playerCombat.playerStats.currentWeapon.attacks.ToArray();
    }
}
