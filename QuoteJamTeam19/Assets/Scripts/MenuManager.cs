using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public enum SceneNames
    {
        Menu = 0,
        Game = 1,
        Win = 2,
        Lose = 3
    }

    private static MenuManager instance;
    public static MenuManager Instance { get { return instance; } }

    public GameObject tutorial;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
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
}

