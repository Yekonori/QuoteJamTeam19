using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private GameManager() { }
    public static GameManager Instance { get { return instance; } }
    public CharacterMovement player;
    public GameObject PauseMenuObject;
    public bool isGamePaused = false;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;

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
}
