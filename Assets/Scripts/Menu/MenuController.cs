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

    public Slider volumeSliderMusic;
    public Slider volumeSliderButtonClick;

    public MenuAudioController menuAudioController;

    public GameObject backButton;

    private const int numberOfSaves = 4;
    private bool[] saveExists = new bool[4];

    public Button[] continueButtons;
    public Button[] deleteButtons;

    public Sprite continueGameImage; // Зображення для кнопки продовження гри з сейву
    public Sprite newGameImage; // Зображення для кнопки нової гри

    public Image[] continueButtonImage; // Посилання на Image компонент кнопки продовження гри


    private void Start()
    {
        mainMenuUI.SetActive(true);
        saveMenu.SetActive(false);
        backButton.SetActive(false);
        soundSettingsMenu.SetActive(false);

        volumeSliderMusic.onValueChanged.AddListener(delegate { SetVolume_1(volumeSliderMusic.value); });
        volumeSliderButtonClick.onValueChanged.AddListener(delegate { SetVolume_2(volumeSliderButtonClick.value); });

        CheckSaveExists();
        for (int i = 0; i <= 3; i++)
        {
            if (saveExists[i]) // Перевірка наявності сейву у першому слоті (можна змінити на будь-який інший)
            {
                continueButtonImage[i].sprite = continueGameImage; // Встановлення зображення для продовження гри
            }
            else
            {
                continueButtonImage[i].sprite = newGameImage; // Встановлення зображення для нової гри
            }
        }

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

            //// Зміна кольору та активності кнопки "Продовжити" в залежності від наявності сейву
            //if (!saveExists[i])
            //{
            //    // Отримання поточних кольорів кнопки
            //    ColorBlock colors = continueButtons[i].colors;

            //    // Зміна кольорів для різних станів кнопки
            //    colors.normalColor = Color.red;
            //    colors.highlightedColor = Color.red; // Опціонально: колір при наведенні курсору
            //    colors.pressedColor = Color.red; // Опціонально: колір при натисканні
            //    colors.disabledColor = Color.red; // Опціонально: колір, коли кнопка неактивна

            //    // Застосування нового блоку кольорів до кнопки
            //    continueButtons[i].colors = colors;

            //    // Зробити кнопку неактивною
            //    continueButtons[i].interactable = false;
            //}
        }
    }

    public void OpenSoundSettings()
    {
        soundSettingsMenu.SetActive(true);
        backButton.SetActive(true);
        volumeSliderMusic.value = AudioListener.volume;
        volumeSliderButtonClick.value = AudioListener.volume;
    }

    public void SetVolume_1(float volume)
    {
        AudioListener.volume = volume; // Встановлення гучності за значенням слайдера 1
    }

    public void SetVolume_2(float volume)
    {
        menuAudioController.SetButtonClickVolume(volume);
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
        saveMenu.SetActive(true);
        backButton.SetActive(true);
    }

    public void BackToMainMenu()
    {
        soundSettingsMenu.SetActive(false);
        saveMenu.SetActive(false);
        backButton.SetActive(false); // Ховаємо кнопку "Назад"
    }

    public void DeleteSave(int saveSlot)
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "save_" + (saveSlot - 1) + ".txt");

        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath); // Видалення файлу сейва
            Debug.Log("Сейв " + saveSlot + " було видалено.");
            saveExists[saveSlot - 1] = false; // Змінити прапорець на false, щоб відобразити відсутність сейва
            continueButtonImage[saveSlot - 1].sprite = newGameImage; // Змінити зображення кнопки на нову гру
            continueButtons[saveSlot - 1].interactable = false; // Зробити кнопку неактивною
        }
        else
        {
            Debug.LogError("Файл сейва не знайдено.");
        }
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

            // Створення нового сейва зі стандартними даними
            CreateNewSave(saveSlot);
        }
    }

    private void CreateNewSave(int saveSlot)
    {
        SaveData newSaveData = new SaveData
        {
            playerPosition = Vector3.zero, // Початкова позиція гравця (0, 0, 0)
            playerScore = 0 // Початковий рахунок гравця
        };

        string json = JsonUtility.ToJson(newSaveData);
        string saveFilePath = Path.Combine(Application.persistentDataPath, "save_" + (saveSlot - 1) + ".txt");

        File.WriteAllText(saveFilePath, json); // Записати дані у файл сейва

        Debug.Log("Створено новий сейв " + saveSlot);
        SceneManager.LoadScene(1);
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

