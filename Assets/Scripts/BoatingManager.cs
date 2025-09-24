using System.Collections.Generic;
using UnityEngine;

public class BoatingManager : MonoBehaviour
{
    public static BoatingManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public GameObject player;
    public Canvas boatingUI;
    public Shop shop;
    public Notification notification;
    public FishingMinigame fishingMinigame;
    public List<MonoBehaviour> canPlayerMoveList = new List<MonoBehaviour>();
}
