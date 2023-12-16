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

            // ��������� �������� �� ������
            continueButtons[i] = GameObject.Find("ContinueButton" + saveSlotNumber).GetComponent<Button>();
            deleteButtons[i] = GameObject.Find("DeleteButton" + saveSlotNumber).GetComponent<Button>();

            // ϳ������ �� ��䳿 ������
            continueButtons[i].onClick.AddListener(() => ContinueOrNewGame(saveSlotNumber));
            deleteButtons[i].onClick.AddListener(() => DeleteSave(saveSlotNumber));

            // ���� ������� �� ��������� ������ "����������" � ��������� �� �������� �����
            if (!saveExists[i])
            {
                // ��������� �������� ������� ������
                ColorBlock colors = continueButtons[i].colors;

                // ���� ������� ��� ����� ����� ������
                colors.normalColor = Color.red;
                colors.highlightedColor = Color.red; // �����������: ���� ��� �������� �������
                colors.pressedColor = Color.red; // �����������: ���� ��� ���������
                colors.disabledColor = Color.red; // �����������: ����, ���� ������ ���������

                // ������������ ������ ����� ������� �� ������
                continueButtons[i].colors = colors;

                // ������� ������ ����������
                continueButtons[i].interactable = false;
            }
        }
    }

    public void OpenSoundSettings()
    {
        mainMenuUI.SetActive(false);
        soundSettingsMenu.SetActive(true);
        backButton.SetActive(true);
        volumeSlider_1.value = AudioListener.volume; // ������������ �������� �������� ������� � ��������� �����
        volumeSlider_2.value = AudioListener.volume; // ������������ �������� �������� ������� � ��������� �����
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume; // ������������ ������� �� ��������� ��������
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
        mainMenuUI.SetActive(false);
        saveMenu.SetActive(true);
        backButton.SetActive(true);
    }

    public void BackToMainMenu()
    {
        soundSettingsMenu.SetActive(false);
        mainMenuUI.SetActive(true);
        saveMenu.SetActive(false);
        backButton.SetActive(false); // ������ ������ "�����"
    }

    public void DeleteSave(int saveSlot)
    {
        Debug.Log("���� " + saveSlot + " ���� ��������.");
        // ������� ��� ��� ��������� �����
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

