using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject saveMenu;

    public GameObject soundSettingsMenu;
    public Slider volumeSlider_1;
    public Slider volumeSlider_2;

    public GameObject backButton;

    private const int numberOfSaves = 4;
    private bool[] saveExists = new bool[4];

    public Button[] continueButtons;
    public Button[] deleteButtons;

    private void Start()
    {
        mainMenuUI.SetActive(true);
        saveMenu.SetActive(false);
        backButton.SetActive(false);
        soundSettingsMenu.SetActive(false);

        CheckSaveExists();

        continueButtons = new Button[numberOfSaves];
        deleteButtons = new Button[numberOfSaves];

        for (int i = 0; i < numberOfSaves; i++)
        {
            int saveSlotNumber = i + 1;

            // Отримання посилань на кнопки
            continueButtons[i] = GameObject.Find("ContinueButton" + saveSlotNumber).GetComponent<Button>();
            deleteButtons[i] = GameObject.Find("DeleteButton" + saveSlotNumber).GetComponent<Button>();

            // Підписка на події кнопок
            continueButtons[i].onClick.AddListener(() => ContinueOrNewGame(saveSlotNumber));
            deleteButtons[i].onClick.AddListener(() => DeleteSave(saveSlotNumber));

            // Зміна кольору та активності кнопки "Продовжити" в залежності від наявності сейву
            if (!saveExists[i])
            {
                // Отримання поточних кольорів кнопки
                ColorBlock colors = continueButtons[i].colors;

                // Зміна кольорів для різних станів кнопки
                colors.normalColor = Color.red;
                colors.highlightedColor = Color.red; // Опціонально: колір при наведенні курсору
                colors.pressedColor = Color.red; // Опціонально: колір при натисканні
                colors.disabledColor = Color.red; // Опціонально: колір, коли кнопка неактивна

                // Застосування нового блоку кольорів до кнопки
                continueButtons[i].colors = colors;

                // Зробити кнопку неактивною
                continueButtons[i].interactable = false;
            }
        }
    }

    public void OpenSoundSettings()
    {
        mainMenuUI.SetActive(false);
        soundSettingsMenu.SetActive(true);
        backButton.SetActive(true);
        volumeSlider_1.value = AudioListener.volume; // Встановлюємо значення слайдера гучності з поточного звуку
        volumeSlider_2.value = AudioListener.volume; // Встановлюємо значення слайдера гучності з поточного звуку
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume; // Встановлення гучності за значенням слайдера
    }


    private void CheckSaveExists()
    {
        string saveFolderPath = Application.persistentDataPath; // Отримання шляху до папки з сейвами

        for (int i = 0; i < numberOfSaves; i++)
        {
            string saveFilePath = Path.Combine(saveFolderPath, "save_" + i + ".txt");

            if (File.Exists(saveFilePath))
            {
                saveExists[i] = true; // Якщо файл сейва існує, змінюємо прапорець на true
                Debug.Log("Сейв існує");
            }
            else
            {
                saveExists[i] = false; // Якщо файл сейва не існує, прапорець залишається false
                Debug.Log("Сейв не існує");
            }
        }
    }


    public void StartGame()
    {
        mainMenuUI.SetActive(false);
        saveMenu.SetActive(true);
        backButton.SetActive(true);
    }

    public void BackToMainMenu()
    {
        soundSettingsMenu.SetActive(false);
        mainMenuUI.SetActive(true);
        saveMenu.SetActive(false);
        backButton.SetActive(false); // Ховаємо кнопку "Назад"
    }

    public void DeleteSave(int saveSlot)
    {
        Debug.Log("Сейв " + saveSlot + " було видалено.");
        // Додайте код для видалення сейва
    }

    public void ContinueOrNewGame(int saveSlot)
    {
        if (saveExists[saveSlot - 1]) // Перевірка наявності сейва
        {
            Debug.Log("Продовження гри з сейвом " + saveSlot);

            // Завантаження гри з сейву
            LoadGame(saveSlot);
        }
        else
        {
            Debug.Log("Початок нової гри, бо сейв " + saveSlot + " відсутній.");
            // Код для початку нової гри
            SceneManager.LoadScene(1);
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public Vector3 playerPosition;
        public int playerScore;
    }

    private void LoadGame(int saveSlot)
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "save_" + (saveSlot - 1) + ".txt");



        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            if (saveData != null)
            {
                Vector3 playerPosition = saveData.playerPosition;
                int playerScore = saveData.playerScore;

                // Встановити позицію гравця
                if (saveData != null)
                {
                    GameData.playerPosition = saveData.playerPosition;
                    GameData.playerScore = saveData.playerScore;

                    SceneManager.LoadScene(1);
                }
                else
                {
                    Debug.LogError("Помилка при розборі даних з файлу сейва.");
                }

                Debug.Log("Завантажено гру з сейву " + saveSlot);
            }
            else
            {
                Debug.LogError("Помилка при розборі даних з файлу сейва.");
            }
        }
        else
        {
            Debug.LogError("Файл сейва не знайдено.");
        }
    }

    public void ExitGame()
    {
        Debug.Log("Гра була закрита.");
        Application.Quit();
    }
}

