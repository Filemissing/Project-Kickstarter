using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            throw new System.Exception("Multiple instances of GameManager detected!");
    }

    public playerMode currentPlayerMode = playerMode.boating;

    public GameObject player;
    public Canvas boatingUI;
    public Canvas shopUI;
    public Notification notification;
    public FishingMinigame fishingMinigame;
    public bool canPlayerMove = true;

    public int playerMicroPlastics;
    
    public int playerMaxOxygen;

    public void EnterCombat(Enemy enemy, bool wonMinigame)
    {
        StartCoroutine(EnterCombatAsync(enemy, wonMinigame));
    }

    public IEnumerator EnterCombatAsync(Enemy enemy, bool wonMinigame)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("CombatScene");
        while (!op.isDone)
            yield return null;

        currentPlayerMode = playerMode.inCombat;
        Debug.Log(enemy);
        Debug.Log(CombatManager.instance);
        CombatManager.instance.StartCombat(enemy, wonMinigame);
    }
}
public enum playerMode
{
    boating,
    inCombat
}