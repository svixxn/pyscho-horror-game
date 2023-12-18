using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon; // Зображення для основного предмета
    public Image secondIcon; // Додаткове зображення для предмета
    public Item item; // Предмет у слоті
    public Button useButton; // Кнопка "Використати"
    public Button descriptionButton; // Кнопка "Опис"
    public TextMeshProUGUI descriptionText; // Текстове поле для виведення опису предмета
    private bool isOpen = false;
    private static InventorySlot lastClickedSlot; // Поточний відкритий слот

    private void Start()
    {
        HideButtonsAndDescription();
    }

    // Приховати кнопки "Використати" і "Опис" та текстове поле
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

    // Показати кнопки "Використати" і "Опис" та текстове поле
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

    // Показати кнопки "Використати" і "Опис" при кліку на слот
    public void OnClickSlot()
    {
        // Перевірка айді предмета, щоб показати кнопки
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

    // Вивести опис предмета у текстове поле
    public void ShowDescription()
    {
        if (item != null)
        {
            descriptionText.text = item.description;
        }
    }

    // Додати предмет у слот
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

    // Очистити слот
    public void ClearSlot()
    {
        item = null; // Очищаємо посилання на предмет
        if (icon != null)
        {
            icon.sprite = null; // Очищаємо зображення предмета
            icon.enabled = false; // Вимикаємо відображення зображення
        }

        if (secondIcon != null)
        {
            secondIcon.sprite = null; // Очищаємо друге зображення предмета
            secondIcon.enabled = false; // Вимикаємо відображення другого зображення
        }

        HideButtonsAndDescription(); // Приховуємо кнопки "Використати" і "Опис" та текстове поле при очищенні слоту
        isOpen = false; // Слот очищено, тому isOpen повинно бути false
        descriptionText.text = ""; // Очищаємо опис при очищенні слоту
    }
}