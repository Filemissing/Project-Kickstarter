using System;
using UnityEngine;
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

    public void EnterCombat(Enemy enemy)
    {
        SceneManager.LoadScene("CombatScene");
        currentPlayerMode = playerMode.inCombat;
        CombatManager.instance.StartCombat(enemy);
    }
}
public enum playerMode
{
    boating,
    inCombat
}