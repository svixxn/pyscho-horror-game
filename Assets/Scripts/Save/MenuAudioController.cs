using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioController : MonoBehaviour
{
    public AudioSource menuMusic; // ������� ������� ����� ��� ������ ����
    public AudioSource buttonClickSound; // ������� ������� ����� ��� ����� ���������� ������

    void Start()
    {
        // ³��������� ������ ���� ��� �����
        if (menuMusic != null)
        {
            menuMusic.Play();
        }
    }

    // ������� ��� ���������� ����� ���������� ������
    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            if (!buttonClickSound.isPlaying) // ��������, �� �� ������������ ��� ����
            {
                buttonClickSound.Play(); // ³��������� �����
            }
            else
            {
                buttonClickSound.Stop(); // ������� ������������ ����������
                buttonClickSound.Play(); // ������ ���� ����������
            }
        }
    }

    // ������� ��� ������������ ������ ����
    public void PauseMenuMusic()
    {
        if (menuMusic != null)
        {
            menuMusic.Pause();
        }
    }

    // ������� ��� ���������� ���������� ������ ����
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