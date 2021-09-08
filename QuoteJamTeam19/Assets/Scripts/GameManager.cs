using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private GameManager() { }
    public static GameManager Instance { get { return instance; } }
    public CharacterMovement player;
    public Timer timer;
    public bool canPlay = true;

    private bool dignityToSet = false;
    public bool DignityToSet { get { return dignityToSet; } }

    private float dignityLeft = 0;
    public float DignityLeft { get { return dignityLeft; } }

    private bool timerToSet = false;
    public bool TimerToSet { get { return timerToSet; } }

    private int minutsLeft = 0;
    public int MinutsLeft { get { return minutsLeft; } }
    private int secondsLeft = 0;
    public int SecondsLeft { get { return secondsLeft; } }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        if (player == null)
            Debug.LogError("Player is not assigned");
    }

    public void Lose()
    {
        canPlay = false;

        player.StopPlayer();
        timer.HardStopTimer();
    }

    public void GetRing()
    {
        dignityToSet = true;
        dignityLeft = DignityBar.Instance.GetDignityValue();

        timerToSet = true;
        minutsLeft = timer.GetMinuts();
        secondsLeft = timer.GetSeconds();

        timer.HardStopTimer();
        player.StopPlayer();
        // On load la scene suivante
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Victory()
    {

    }
}
