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

    public DignityBar dignityBar;

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

        if (instance != this)
        {
            Destroy(gameObject);
        }

        if (player == null)
            Debug.LogError("Player is not assigned");

        if (PauseMenuObject.activeSelf == true && isGamePaused == false)
            PauseMenuObject.SetActive(false);
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Win" && SceneManager.GetActiveScene().name != "Loose")
        {
            SetPause();
        }
    }

    public void SetPause()
    {
        isGamePaused = !isGamePaused;
        print(isGamePaused);
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
        QuitParty();
        print("you lost.");
        player.StopPlayer();
        MenuManager.Instance.LoadThisScene("Loose");
        AudioManager.instance.Play("game_over");
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
        QuitParty();
        MenuManager.Instance.LoadThisScene("Win");
    }
    
    public void EndDialogue()
    {
        player.RestartPlayer();
    }

    public void StartParty()
    {
        PauseMenuObject.SetActive(false);

        timer.StartTimerParty();

        dignityBar.gameObject.SetActive(true);

        hasRing = false;
    }

    public void QuitParty()
    {
        if (dignityBar != null)
        {
            dignityBar.Hide();
        }

        timer.gameObject.SetActive(false);
        timer.HardResetTimer();

        isGamePaused = false;
        PauseMenuObject.SetActive(false);

        AudioManager.instance.StopPlayAll();
    }
}
