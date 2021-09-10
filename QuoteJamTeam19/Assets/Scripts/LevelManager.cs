using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.StopPlayAll();
        GameManager.Instance.StartParty();
        AudioManager.instance.Play("game_music");
    }
}
