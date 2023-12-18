using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon; // ���������� ��� ��������� ��������
    public Image secondIcon; // ��������� ���������� ��� ��������
    public Item item; // ������� � ����
    public Button useButton; // ������ "�����������"
    public Button descriptionButton; // ������ "����"
    public TextMeshProUGUI descriptionText; // �������� ���� ��� ��������� ����� ��������
    private bool isOpen = false;
    private static InventorySlot lastClickedSlot; // �������� �������� ����

    private void Start()
    {
        HideButtonsAndDescription();
    }

    // ��������� ������ "�����������" � "����" �� �������� ����
    private void HideButtonsAndDescription()
    {
        if (useButton != null && descriptionButton != null && descriptionText != null)
        {
            useButton.gameObject.SetActive(false);
            descriptionButton.gameObject.SetActive(false);
            descriptionText.gameObject.SetActive(false);
        }

        if (item != null && item.id == 0)
        {
            if (icon != null)
            {
                icon.enabled = false;
            }

            if (secondIcon != null)
            {
                secondIcon.enabled = false;
            }
        }
    }

    // �������� ������ "�����������" � "����" �� �������� ����
    private void ShowButtonsAndDescription()
    {
        useButton.gameObject.SetActive(true);
        descriptionButton.gameObject.SetActive(true);
        descriptionText.gameObject.SetActive(true);

        if (item != null && item.id != 0)
        {
            if (icon != null && item.icon != null)
            {
                icon.sprite = item.icon;
                icon.enabled = true;
            }

            if (secondIcon != null && item.secondIcon != null)
            {
                secondIcon.sprite = item.secondIcon;
                secondIcon.enabled = true;
            }
        }
    }

    // �������� ������ "�����������" � "����" ��� ���� �� ����
    public void OnClickSlot()
    {
        // �������� ��� ��������, ��� �������� ������
        if (item != null && item.id != 0)
        {
            if (lastClickedSlot != null && lastClickedSlot != this && lastClickedSlot.isOpen)
            {
                lastClickedSlot.HideButtonsAndDescription();
                lastClickedSlot.isOpen = false;
            }

            if (!isOpen)
            {
                ShowButtonsAndDescription();
                isOpen = true;
                lastClickedSlot = this;
            }
            else
            {
                HideButtonsAndDescription();
                isOpen = false;
                lastClickedSlot = null;
            }

            descriptionText.text = "";
        }
    }

    // ������� ���� �������� � �������� ����
    public void ShowDescription()
    {
        if (item != null)
        {
            descriptionText.text = item.description;
        }
    }

    // ������ ������� � ����
    public void AddItem(Item newItem)
    {
        item = newItem;
        if (item != null && item.id != 0)
        {
            if (icon != null && item.icon != null)
            {
                icon.sprite = item.icon;
                icon.enabled = true;
            }

            if (secondIcon != null && item.secondIcon != null)
            {
                secondIcon.sprite = item.secondIcon;
                secondIcon.enabled = true;
            }
        }
        else
        {
            if (icon != null)
            {
                icon.enabled = false;
            }

            if (secondIcon != null)
            {
                secondIcon.enabled = false;
            }
        }
    }

    // �������� ����
    public void ClearSlot()
    {
        item = null; // ������� ��������� �� �������
        if (icon != null)
        {
            icon.sprite = null; // ������� ���������� ��������
            icon.enabled = false; // �������� ����������� ����������
        }

        if (secondIcon != null)
        {
            secondIcon.sprite = null; // ������� ����� ���������� ��������
            secondIcon.enabled = false; // �������� ����������� ������� ����������
        }

        HideButtonsAndDescription(); // ��������� ������ "�����������" � "����" �� �������� ���� ��� ������� �����
        isOpen = false; // ���� �������, ���� isOpen ������� ���� false
        descriptionText.text = ""; // ������� ���� ��� ������� �����
    }
}