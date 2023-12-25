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
    public string[] dialogues; // ����� ������ ������
    public Sprite[] characterSprites; // ����� ��������� ������� ���������
    private int currentDialogueIndex = 0;
    private bool dialogueActive = false;

    public GameObject objectToMove; // ��'���, ���� ������� ������ ���� ���������� ������
    public Transform targetDestination; // ֳ����� �������, �� ��� �������� �� ��������
    public float moveSpeed = 5.0f; // �������� ���� ��'����

    public Collider2D dialogueCollider; // ��������, ���� ���������� ��� �����䳿

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

            // ������ ��'��� ���� ���������� ������
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
        Time.timeScale = 0f; // �������� ���
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
        Time.timeScale = 1f; // ��������� ��� �� ���������� ���� ��������� ����
        float step = moveSpeed * Time.deltaTime;
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetDestination.position, step);

        // ��������, �� �������� ����� �� ������� ����� � ������� ����
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
            obj.SetActive(true); // ���������� ��'���
        }
    }
}