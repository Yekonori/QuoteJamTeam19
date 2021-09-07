using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private GameManager() { } //au cas où certains fous tenteraient qd même d'utiliser le mot clé "new"

    // Méthode d'accès statique (point d'accès global)
    public static GameManager Instance { get { return instance; } }
    public CharacterMovement player;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente (sécurité...sécurité...)

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
