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
    public GameObject PauseMenuObject;
    public bool isGamePaused = false;
    public bool hasRing = false;

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

        if (PauseMenuObject.activeSelf == true && isGamePaused == false)
            PauseMenuObject.SetActive(false);
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            print(isGamePaused);
        }

        if (isGamePaused)
        {
            Time.timeScale = 0;
            PauseMenuObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PauseMenuObject.SetActive(false);
        }
    }

    public void Lose()
    {
        print("you lost.");
        player.StopPlayer();
        MenuManager.Instance.LoadThisScene("Loose");
    }

    public void GetRing()
    {
        dignityToSet = true;
        dignityLeft = DignityBar.Instance.GetDignityValue();

        timerToSet = true;
        minutsLeft = timer.GetMinuts();
        secondsLeft = timer.GetSeconds();

        hasRing = true;

        player.StopPlayer();
        // On load la scene suivante
        //MenuManager.Instance.LoadThisScene(SceneManager.GetActiveScene().buildIndex + 1);
        MenuManager.Instance.LoadThisScene("GameScene2");
    }

    public void Victory()
    {
        MenuManager.Instance.LoadThisScene("Win");
    }
}
