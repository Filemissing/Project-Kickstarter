using System.Collections;
using System.Collections.Generic;
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
            Destroy(gameObject);
    }

    public playerMode currentPlayerMode = playerMode.boating;

    public PlayerStats playerStats;

    public List<EnemyInfo> unlockedEnemyInfos = new();

    public void EnterCombat(EnemyInfo enemy, bool wonMinigame)
    {
        StartCoroutine(EnterCombatAsync(enemy, wonMinigame));
    }

    public IEnumerator EnterCombatAsync(EnemyInfo enemy, bool wonMinigame)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("CombatScene");
        while (!op.isDone)
            yield return null;

        currentPlayerMode = playerMode.inCombat;
        CombatManager.instance.StartCombat(enemy, wonMinigame);
    }
}
public enum playerMode
{
    boating,
    inCombat
}