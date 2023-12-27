using UnityEngine;

public class LevelBasedAnimation : MonoBehaviour
{
    public Animator anim;
    private int playerLevel = 1; // ����������, �� ����� ������ - 1

    void Update()
    {
        switch (playerLevel)
        {
            case 1:
                PlayAnimationForLevel1();
                break;
            case 2:
                PlayAnimationForLevel2();
                break;
            case 3:
                PlayAnimationForLevel2();
                break;
            default:
                PlayDefaultAnimation();
                break;
        }
    }

    void PlayAnimationForLevel1()
    {
        // ��� ��� ���������� ������� ��� ���� 1
        anim.SetTrigger("Level1Animation");
    }

    void PlayAnimationForLevel2()
    {
        // ��� ��� ���������� ������� ��� ���� 2
        anim.SetTrigger("Level2Animation");
    }

    void PlayDefaultAnimation()
    {
        // ��� ��� ���������� ������� �� �������������
        anim.SetTrigger("DefaultAnimation");
    }
}