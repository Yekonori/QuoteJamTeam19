using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private GameManager() { } //au cas o� certains fous tenteraient qd m�me d'utiliser le mot cl� "new"

    // M�thode d'acc�s statique (point d'acc�s global)
    public static GameManager Instance { get { return instance; } }
    public CharacterMovement player;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance pr�c�dente (s�curit�...s�curit�...)

        instance = this;

        if (player == null)
            Debug.LogError("Player is not assigned");
    }

    public void Lose()
    {
        // CharacterMovement.canMove = false;
        print("you lost.");
        player.StopPlayer();
    }
}
