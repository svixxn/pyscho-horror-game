using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Image characterImage;
    public TextMeshProUGUI dialogueText;
    public string[] dialogues; // Масив текстів діалогів
    public Sprite[] characterSprites; // Масив зображень обличчя персонажів
    private int currentDialogueIndex = 0;
    private bool dialogueActive = false;

    public GameObject objectToMove; // Об'єкт, який потрібно рухати після завершення діалогу
    public Transform targetDestination; // Цільова позиція, до якої персонаж має рухатися
    public float moveSpeed = 5.0f; // Швидкість руху об'єкта

    public Collider2D dialogueCollider; // Колайдер, який вибирається для взаємодії

    public GameObject[] objectsToActivate;
    public string[] activationDialogues;

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

            // Рухати об'єкт після завершення діалогів
            if (objectToMove != null && targetDestination != null)
            {
                MoveCharacter();
            }
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
        }
    }

    void MoveCharacter()
    {
        Time.timeScale = 1f; // Повернути час до звичайного після закінчення руху
        float step = moveSpeed * Time.deltaTime;
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetDestination.position, step);

        // Перевірка, чи персонаж дійшов до цільової точки і зупинка руху
        if (objectToMove.transform.position == targetDestination.position)
        {
            ActivateObjects();
            characterImage.enabled = false;
            objectToMove.gameObject.SetActive(false);
        }
    }
    void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true); // Активувати об'єкт
        }
    }
}