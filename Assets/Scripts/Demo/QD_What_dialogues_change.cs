using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

namespace QuantumTek.QuantumDialogue.Demo
{
    public class QD_What_dialogues_change : MonoBehaviour
    {
        public QD_DialogueHandler handler;
        public TextMeshProUGUI speakerName;
        public TextMeshProUGUI messageText;
        public Transform choices;
        public TextMeshProUGUI choiceTemplate;
        public Image dialogueImage;

        private List<TextMeshProUGUI> activeChoices = new List<TextMeshProUGUI>();
        private List<TextMeshProUGUI> inactiveChoices = new List<TextMeshProUGUI>();

        private bool ended;
        private bool dialoguePlayed;

        private bool waitForInput = false;

        private bool canShowInfo = false;
        public TextMeshProUGUI textElement;

        private bool triggeredOnce = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!triggeredOnce && collision.CompareTag("Player"))
            {
                WaitForInput();
                triggeredOnce = true;
            }
            if (collision.CompareTag("Player") && !dialoguePlayed)
            {
                WaitForInput();
                dialoguePlayed = true;

            }
            if (collision.CompareTag("Player"))
            {
                canShowInfo = true;
                textElement.enabled = true;
                textElement.text = "Натисніть на E, щоб отримати інформацію";
            }

            // Шлях до файлу
            string filePath = Path.Combine(Application.persistentDataPath, "First_nightmare.txt");

            // Перевірка наявності файлу
            if (File.Exists(filePath))
            {
                // Читання з файлу
                string fileContent = File.ReadAllText(filePath);

                // Перевірка значення Exit from the room
                if (fileContent.Contains("Exit from the room = 1"))
                {
                    handler.SetConversation("Meeting with Bob - Dialog 1");
                }
                else if (fileContent.Contains("Exit from the room = 2"))
                {
                    handler.SetConversation("Meeting with Bob - Dialog 2");
                }
                else
                {
                    Debug.Log("Dialog not changed.");
                }
            }
            else
            {
                Debug.LogError("File not found!");
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                canShowInfo = false;
                textElement.enabled = false;
                ended = false; // Скидання змінної ended, щоб можна було активувати знову
                triggeredOnce = false; // Скидання змінної triggeredOnce
            }
        }
        private void HideImage()
        {
            // Приховати зображення
            if (dialogueImage != null)
                dialogueImage.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        private void WaitForInput()
        {
            // Показати текст для натискання клавіші "E"
            Debug.Log("Натисніть на 'E'");
            messageText.text = "Натисніть на 'E'";
            waitForInput = true;
        }
        private void StartDialogue()
        {
            Time.timeScale = 0f;
            dialogueImage.gameObject.SetActive(true);
            // Тут починається діалог
            Debug.Log("Діалог починається.");
            SetText();
        }
        private void Update()
        {
            // Don't do anything if the conversation is over
            if (ended)
                return;
            if (waitForInput && Input.GetKeyDown(KeyCode.E))
            {
                waitForInput = false;
                StartDialogue(); // Почати діалог після натискання клавіші "E"
            }
            // Check if the space key is pressed and the current message is not a choice
            //if (handler.currentMessageInfo.Type == QD_NodeType.Message && Input.GetKeyUp(KeyCode.Space))
            if (handler.currentMessageInfo.Type == QD_NodeType.Message && Input.GetKeyUp(KeyCode.Space))
            {
                Next();
                // При кінці діалогу ховаємо зображення
                if (ended)
                    HideImage();
            }

        }
        private void ClearChoices()
        {
            for (int i = activeChoices.Count - 1; i >= 0; --i)
            {
                // Use object pooling with the choices to prevent unecessary garbage collection
                activeChoices[i].gameObject.SetActive(false);
                activeChoices[i].text = "";
                inactiveChoices.Add(activeChoices[i]);
                activeChoices.RemoveAt(i);
            }
        }
        private void GenerateChoices()
        {
            // Exit if not a choice
            if (handler.currentMessageInfo.Type != QD_NodeType.Choice)
                return;
            // Clear the old choices
            ClearChoices();
            // Generate new choices
            QD_Choice choice = handler.GetChoice();
            int added = 0;
            // Use inactive choices instead of making new ones, if possible
            while (inactiveChoices.Count > 0 && added < choice.Choices.Count)
            {
                int i = inactiveChoices.Count - 1;
                TextMeshProUGUI cText = inactiveChoices[i];
                cText.text = choice.Choices[added];
                QD_ChoiceButton button = cText.GetComponent<QD_ChoiceButton>();
                button.number = added;
                cText.gameObject.SetActive(true);
                activeChoices.Add(cText);
                inactiveChoices.RemoveAt(i);
                added++;
            }
            // Make new choices if any left to make
            while (added < choice.Choices.Count)
            {
                TextMeshProUGUI newChoice = Instantiate(choiceTemplate, choices);
                newChoice.text = choice.Choices[added];
                QD_ChoiceButton button = newChoice.GetComponent<QD_ChoiceButton>();
                button.number = added;
                newChoice.gameObject.SetActive(true);
                activeChoices.Add(newChoice);
                added++;
            }
        }
        private void SetText()
        {
            // Clear everything
            speakerName.text = "";
            messageText.gameObject.SetActive(false);
            messageText.text = "";
            ClearChoices();

            // If at the end, don't do anything
            if (ended)
                return;

            // Generate choices if a choice, otherwise display the message
            if (handler.currentMessageInfo.Type == QD_NodeType.Message)
            {
                QD_Message message = handler.GetMessage();
                speakerName.text = message.SpeakerName;
                messageText.text = message.MessageText;
                messageText.gameObject.SetActive(true);
            }
            else if (handler.currentMessageInfo.Type == QD_NodeType.Choice)
            {
                speakerName.text = "Player";
                GenerateChoices();
            }
        }
        public void Next(int choice = -1)
        {
            if (ended)
                return;

            // Go to the next message
            handler.NextMessage(choice);
            // Set the new text
            SetText();
            // End if there is no next message
            if (handler.currentMessageInfo.ID < 0)
                ended = true;
        }
        public void Choose(int choice)
        {
            if (ended)
                return;

            Next(choice);
        }

    }
}