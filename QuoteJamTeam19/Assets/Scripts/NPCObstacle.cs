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

        foreach (char character in textToSpeak)
        {
            dialogueText.text += character;

            //if (GameManager.Instance.isInSpeedMode)
            //{
            //    yield return new WaitForSeconds(textSpeedRatio * 100);
            //}
            //else
            //{
            //    yield return new WaitForSeconds(textSpeedRatio);
            //}

            yield return new WaitForSeconds(textSpeedRatio);
        }

        GameManager.Instance.EndDialogue();
        dialogueBox.SetActive(false);
        Destroy(this);
    }
}
