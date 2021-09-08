using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private GameManager() { }
    public static GameManager Instance { get { return instance; } }
    public CharacterMovement player;
    public Timer timer;
    public bool canPlay = true;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;

        if (player == null)
            Debug.LogError("Player is not assigned");
    }

    public void Lose()
    {
        canPlay = false;

        player.StopPlayer();
        timer.HardStopTimer();
    }
}
