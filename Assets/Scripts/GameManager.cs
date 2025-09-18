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
            Destroy(gameObject);
    }

    public playerMode currentPlayerMode = playerMode.boating;

    public int playerMicroPlastics;

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