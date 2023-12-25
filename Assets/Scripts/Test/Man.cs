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
    public string[] dialogues; // Масив текстів діалогів
    public Sprite[] characterSprites; // Масив зображень обличчя персонажів
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
            Debug.Log("Space key pressed.");
            DisplayNextDialogue();
        }

        if (currentDialogueIndex >= dialogues.Length)
        {
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f; // Повернути час до звичайного
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered dialogue collider.");
        StartDialogue();
    }

    void StartDialogue()
    {
        Debug.Log("Starting dialogue.");
        dialoguePanel.SetActive(true);
        dialogueActive = true;
        Time.timeScale = 0f; // Зупинити час
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
            Time.timeScale = 1f; // Повернути час до звичайного, якщо діалоги закінчилися
        }
    }

    void ActivateObjects(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true); // Активувати об'єкт
        }
    }

    void DeactivateObjects(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.gameObject.SetActive(false); // Вимкнути об'єкт
        }
    }

    void ActivateSprites(SpriteRenderer[] sprites)
    {
        foreach (SpriteRenderer spriteRenderer in sprites)
        {
            spriteRenderer.enabled = true; // Активувати спрайт
        }
    }

    void DeactivateSprites(SpriteRenderer[] sprites)
    {
        foreach (SpriteRenderer spriteRenderer in sprites)
        {
            spriteRenderer.enabled = false; // Вимкнути спрайт
        }
    }
}