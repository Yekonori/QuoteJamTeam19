using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private static MenuManager instance;
    public static MenuManager Instance { get { return instance; } }

    public GameObject tutorial;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (instance != null && instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        AudioManager.instance.Play("title_screen");    
    }

    public void OpenTutorial()
    {
        tutorial.SetActive(true);
    }
    public void QuitTuto()
    {
        tutorial.SetActive(false);
    }

    public void QuitApp()
    {
        Debug.Log("Quitting App");
        Application.Quit();
    }

    public void LoadThisScene(string levelName)
    {
        try
        {
            SceneManager.LoadScene(levelName);
        }

        catch
        {
            Debug.LogError("Scene not found");
        }

    }

    public void LoadThisScene(int levelIndex)
    {
        try
        {
            SceneManager.LoadScene(levelIndex);
        }

        catch
        {
            Debug.LogError("Scene not found");
        }

    }
}

