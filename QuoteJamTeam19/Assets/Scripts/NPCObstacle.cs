using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCObstacle : MonoBehaviour
{
    public float textDuration = 1f;

    [TextArea]
    public string textToSpeak;

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    private float speedMultiplicator = 1f;
    private bool canSpeed = true;
    private float speedCD = 2f;

    private void Awake()
    {
        speedCD = 0.5f;
        dialogueBox.SetActive(false);
        dialogueText.text = "";
    }

    public void TriggerDialogue()
    {
        dialogueBox.SetActive(true);
        StartCoroutine(StartSpeaking());
    }

    private IEnumerator StartSpeaking()
    {
        int textLength = textToSpeak.Length;
        float textSpeedRatio = textDuration / textLength;

        foreach (char character in textToSpeak)
        {
            dialogueText.text += character;

            if (Input.GetButton("Jump"))
            {
                StartCoroutine(StartSpeedSpeak());
            }

            yield return new WaitForSeconds(textSpeedRatio / speedMultiplicator);
        }

        GameManager.Instance.EndDialogue();
        dialogueBox.SetActive(false);
        gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    private IEnumerator StartSpeedSpeak()
    {
        if (canSpeed)
        {
            speedMultiplicator = 50f;
            canSpeed = false;

            yield return new WaitForSeconds(1f);

            StopSpeedSpeak();
        }
    }

    private void StopSpeedSpeak()
    {
        speedMultiplicator = 1f;
        Invoke("CanSpeedAgain", speedCD);
    }

    private void CanSpeedAgain()
    {
        canSpeed = true;
    }
}
