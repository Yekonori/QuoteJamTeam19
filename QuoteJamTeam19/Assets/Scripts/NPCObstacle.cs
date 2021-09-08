using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCObstacle : MonoBehaviour
{
    public float textSpeed = 1f;

    [TextArea]
    public string textToSpeak;

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    private void Awake()
    {
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
        float textSpeedRatio = textSpeed / textLength;

        float speedMultiplicator = 1f;

        foreach (char character in textToSpeak)
        {
            dialogueText.text += character;

            if (Input.GetButton("Jump"))
            {
                speedMultiplicator = 50f;
            }
            else
            {
                speedMultiplicator = 1f;
            }

            Debug.LogError($"textSpeedRatio : {textSpeedRatio} --- speedMultiplicator : {speedMultiplicator} ---- TOTAL : {textSpeedRatio / speedMultiplicator}");

            yield return new WaitForSeconds(textSpeedRatio / speedMultiplicator);
        }

        GameManager.Instance.EndDialogue();
        dialogueBox.SetActive(false);
        Destroy(this.gameObject);
    }
}
