using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioController : MonoBehaviour
{
    public AudioSource menuMusic; // Додайте джерело звуку для музики меню
    public AudioSource buttonClickSound; // Додайте джерело звуку для звуку натискання кнопки

    void Start()
    {
        // Відтворення музики меню при старті
        if (menuMusic != null)
        {
            menuMusic.Play();
        }
    }

    // Функція для відтворення звуку натискання кнопки
    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            if (!buttonClickSound.isPlaying) // Перевірка, чи не відтворюється вже звук
            {
                buttonClickSound.Play(); // Відтворення звуку
            }
            else
            {
                buttonClickSound.Stop(); // Зупинка попереднього відтворення
                buttonClickSound.Play(); // Почати нове відтворення
            }
        }
    }

    // Функція для призупинення музики меню
    public void PauseMenuMusic()
    {
        if (menuMusic != null)
        {
            menuMusic.Pause();
        }
    }

    // Функція для відновлення відтворення музики меню
    public void ResumeMenuMusic()
    {
        if (menuMusic != null)
        {
            menuMusic.UnPause();
        }
    }

    public void SetButtonClickVolume(float volume)
    {
        if (buttonClickSound != null)
        {
            buttonClickSound.volume = volume;
        }
    }
}