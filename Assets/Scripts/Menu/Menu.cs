using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject soundSettingsMenu;
    public Slider volumeSlider_1;
    public Slider volumeSlider_2;
    public GameObject backButton;

    private bool isMainMenuActive = false;
    //private float previousTimeScale;

    private void Start()
    {
        CloseMenus();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isMainMenuActive)
            {
                CloseMenus();
            }
            else
            {
                OpenMainMenu();
            }
        }
    }

    public void OpenSoundSettings()
    {
        mainMenuUI.SetActive(false);
        soundSettingsMenu.SetActive(true);
        backButton.SetActive(true);
        volumeSlider_1.value = AudioListener.volume;
        volumeSlider_2.value = AudioListener.volume;
    }

    private void OpenMainMenu()
    {
        isMainMenuActive = true;
        Cursor.visible = true; // Зробити мишу видимою
        //Time.timeScale = 0f; // При затримці відображення меню, зупинити час гри
        mainMenuUI.SetActive(true);
        soundSettingsMenu.SetActive(false);
        backButton.SetActive(false);
    }

    private void CloseMenus()
    {
        isMainMenuActive = false;
        Cursor.visible = false; // Зробити мишу невидимою
        //Time.timeScale = 1f; // Відновити час гри
        mainMenuUI.SetActive(false);
        soundSettingsMenu.SetActive(false);
        backButton.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void BackToMainMenu()
    {
        OpenMainMenu();
    }

    public void ExitGame()
    {
        Debug.Log("Гра була закрита.");
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}