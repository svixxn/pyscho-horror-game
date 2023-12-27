using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Man : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Image characterImage;
    public TextMeshProUGUI dialogueText;
    public string[] dialogues; 
    public Sprite[] characterSprites;
    private int currentDialogueIndex = 0;
    private bool dialogueActive = false;

    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeactivate;
    public SpriteRenderer[] spritesToActivate;
    public SpriteRenderer[] spritesToDeactivate;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextDialogue();
        }

        if (currentDialogueIndex >= dialogues.Length)
        {
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        StartDialogue();
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
            Debug.Log("Dialogue ended.");
            ActivateObjects(objectsToActivate);
            DeactivateObjects(objectsToDeactivate);
            ActivateSprites(spritesToActivate);
            DeactivateSprites(spritesToDeactivate);
            Time.timeScale = 1f;
        }
    }

    void ActivateObjects(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }
    }

    void DeactivateObjects(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.gameObject.SetActive(false); 
        }
    }

    void ActivateSprites(SpriteRenderer[] sprites)
    {
        foreach (SpriteRenderer spriteRenderer in sprites)
        {
            spriteRenderer.enabled = true; 
        }
    }

    void DeactivateSprites(SpriteRenderer[] sprites)
    {
        foreach (SpriteRenderer spriteRenderer in sprites)
        {
            spriteRenderer.enabled = false; 
        }
    }
}