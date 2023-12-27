using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel; 
    public GameObject secondInventoryPanel; 
    public Button actionButton1; 
    public Button actionButton2; 

    private void Start()
    {
        inventoryPanel.SetActive(false); 
        actionButton1.gameObject.SetActive(false); 
        actionButton2.gameObject.SetActive(false);
        secondInventoryPanel.SetActive(false); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryPanel();
        }
    }

    private void ToggleInventoryPanel()
    {
        bool isInventoryOpen = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isInventoryOpen);
        actionButton1.gameObject.SetActive(isInventoryOpen); 
        actionButton2.gameObject.SetActive(isInventoryOpen); 

        
        if (secondInventoryPanel.activeSelf)
        {
            secondInventoryPanel.SetActive(false);
            actionButton1.gameObject.SetActive(false);
            actionButton2.gameObject.SetActive(false);
        }

        Time.timeScale = isInventoryOpen ? 0f : 1f;

        Cursor.visible = isInventoryOpen;
        Cursor.lockState = isInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void ToggleSecondInventoryPanel()
    {
        bool isSecondInventoryOpen = !secondInventoryPanel.activeSelf;
        inventoryPanel.SetActive(!isSecondInventoryOpen);
        secondInventoryPanel.SetActive(isSecondInventoryOpen);
        Time.timeScale = isSecondInventoryOpen ? 0f : 1f;
        Cursor.visible = isSecondInventoryOpen;
        Cursor.lockState = isSecondInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void ToggleFirstInventoryPanel()
    {
        bool isFirstInventoryOpen = !inventoryPanel.activeSelf;

        inventoryPanel.SetActive(isFirstInventoryOpen);
        if (secondInventoryPanel.activeSelf)
        {
            secondInventoryPanel.SetActive(false);
            actionButton1.gameObject.SetActive(false);
            actionButton2.gameObject.SetActive(false);
        }
        Time.timeScale = isFirstInventoryOpen ? 0f : 1f;
        Cursor.visible = isFirstInventoryOpen;
        Cursor.lockState = isFirstInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}