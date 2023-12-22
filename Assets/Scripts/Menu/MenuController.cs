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

    public Sprite continueGameImage; // ���������� ��� ������ ����������� ��� � �����
    public Sprite newGameImage; // ���������� ��� ������ ���� ���

    public Image[] continueButtonImage; // ��������� �� Image ��������� ������ ����������� ���


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
            if (saveExists[i]) // �������� �������� ����� � ������� ���� (����� ������ �� ����-���� �����)
            {
                continueButtonImage[i].sprite = continueGameImage; // ������������ ���������� ��� ����������� ���
            }
            else
            {
                continueButtonImage[i].sprite = newGameImage; // ������������ ���������� ��� ���� ���
            }
        }

        continueButtons = new Button[numberOfSaves];
        deleteButtons = new Button[numberOfSaves];

        for (int i = 0; i < numberOfSaves; i++)
        {
            int saveSlotNumber = i + 1;

            // ��������� �������� �� ������
            continueButtons[i] = GameObject.Find("ContinueButton" + saveSlotNumber).GetComponent<Button>();
            deleteButtons[i] = GameObject.Find("DeleteButton" + saveSlotNumber).GetComponent<Button>();

            // ϳ������ �� ��䳿 ������
            continueButtons[i].onClick.AddListener(() => ContinueOrNewGame(saveSlotNumber));
            deleteButtons[i].onClick.AddListener(() => DeleteSave(saveSlotNumber));

            //// ���� ������� �� ��������� ������ "����������" � ��������� �� �������� �����
            //if (!saveExists[i])
            //{
            //    // ��������� �������� ������� ������
            //    ColorBlock colors = continueButtons[i].colors;

            //    // ���� ������� ��� ����� ����� ������
            //    colors.normalColor = Color.red;
            //    colors.highlightedColor = Color.red; // �����������: ���� ��� �������� �������
            //    colors.pressedColor = Color.red; // �����������: ���� ��� ���������
            //    colors.disabledColor = Color.red; // �����������: ����, ���� ������ ���������

            //    // ������������ ������ ����� ������� �� ������
            //    continueButtons[i].colors = colors;

            //    // ������� ������ ����������
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
        AudioListener.volume = volume; // ������������ ������� �� ��������� �������� 1
    }

    public void SetVolume_2(float volume)
    {
        menuAudioController.SetButtonClickVolume(volume);
    }


    private void CheckSaveExists()
    {
        string saveFolderPath = Application.persistentDataPath; // ��������� ����� �� ����� � �������

        for (int i = 0; i < numberOfSaves; i++)
        {
            string saveFilePath = Path.Combine(saveFolderPath, "save_" + i + ".txt");

            if (File.Exists(saveFilePath))
            {
                saveExists[i] = true; // ���� ���� ����� ����, ������� ��������� �� true
                Debug.Log("���� ����");
            }
            else
            {
                saveExists[i] = false; // ���� ���� ����� �� ����, ��������� ���������� false
                Debug.Log("���� �� ����");
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
        backButton.SetActive(false); // ������ ������ "�����"
    }

    public void DeleteSave(int saveSlot)
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "save_" + (saveSlot - 1) + ".txt");

        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath); // ��������� ����� �����
            Debug.Log("���� " + saveSlot + " ���� ��������.");
            saveExists[saveSlot - 1] = false; // ������ ��������� �� false, ��� ���������� ��������� �����
            continueButtonImage[saveSlot - 1].sprite = newGameImage; // ������ ���������� ������ �� ���� ���
            continueButtons[saveSlot - 1].interactable = false; // ������� ������ ����������
        }
        else
        {
            Debug.LogError("���� ����� �� ��������.");
        }
    }

    public void ContinueOrNewGame(int saveSlot)
    {
        if (saveExists[saveSlot - 1]) // �������� �������� �����
        {
            Debug.Log("����������� ��� � ������ " + saveSlot);

            // ������������ ��� � �����
            LoadGame(saveSlot);
        }
        else
        {
            Debug.Log("������� ���� ���, �� ���� " + saveSlot + " �������.");
            // ��� ��� ������� ���� ���

            // ��������� ������ ����� � ������������ ������
            CreateNewSave(saveSlot);
        }
    }

    private void CreateNewSave(int saveSlot)
    {
        SaveData newSaveData = new SaveData
        {
            playerPosition = Vector3.zero, // ��������� ������� ������ (0, 0, 0)
            playerScore = 0 // ���������� ������� ������
        };

        string json = JsonUtility.ToJson(newSaveData);
        string saveFilePath = Path.Combine(Application.persistentDataPath, "save_" + (saveSlot - 1) + ".txt");

        File.WriteAllText(saveFilePath, json); // �������� ��� � ���� �����

        Debug.Log("�������� ����� ���� " + saveSlot);
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

                // ���������� ������� ������
                if (saveData != null)
                {
                    GameData.playerPosition = saveData.playerPosition;
                    GameData.playerScore = saveData.playerScore;

                    SceneManager.LoadScene(1);
                }
                else
                {
                    Debug.LogError("������� ��� ������ ����� � ����� �����.");
                }

                Debug.Log("����������� ��� � ����� " + saveSlot);
            }
            else
            {
                Debug.LogError("������� ��� ������ ����� � ����� �����.");
            }
        }
        else
        {
            Debug.LogError("���� ����� �� ��������.");
        }
    }

    public void ExitGame()
    {
        Debug.Log("��� ���� �������.");
        Application.Quit();
    }
}

