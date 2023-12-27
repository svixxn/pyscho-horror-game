using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagerText : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Image characterImage;
    public TextMeshProUGUI dialogueText;
    public string[] dialogues; 
    public Sprite[] characterSprites;
    private int currentDialogueIndex = 0;
    private bool dialogueActive = false;
    private bool inRange = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!dialogueActive)
        {
            inRange = true;
            StartDialogue();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
    }

    void Update()
    {
        if (inRange && dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextDialogue();
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        dialogueActive = true;
        Time.timeScale = 0f;
        DisplayNextDialogue();
    }

    void DisplayNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
            characterImage.sprite = characterSprites[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            dialogueActive = false;
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}