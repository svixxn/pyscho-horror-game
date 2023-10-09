using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    public string Text;
    private GameObject player;
    private bool canShowInfo = false;
    public TextMeshProUGUI textElement;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        textElement.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canShowInfo = true;
            textElement.enabled = true;
            textElement.text = "Press E to show info";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canShowInfo = false;
            textElement.enabled = false;
        }
    }

    private void Update()
    {
        if (canShowInfo && Input.GetKeyDown(KeyCode.E))
        {
            textElement.text = Text;
        }
    }
}
