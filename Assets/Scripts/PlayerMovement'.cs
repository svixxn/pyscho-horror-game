using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

[RequireComponent(typeof(InventoryBehaviour))]
public class Moving : MonoBehaviour
{
    public float speed = 8f;
    public Animator anim;
    Vector2 movement;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private UIInventory _uiInventory;

    private InventoryBehaviour _inventory;

    private void Start()
    {
        _inventory = GetComponent<InventoryBehaviour>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        var isShowInventory = _uiInventory.gameObject.activeSelf;
        if (Input.GetKeyDown(KeyCode.I))
        {
            isShowInventory = !isShowInventory;
            _uiInventory.gameObject.SetActive(isShowInventory);

            Time.timeScale = isShowInventory ? 0f : 1f;
            Cursor.visible = isShowInventory;
            Cursor.lockState = isShowInventory ? CursorLockMode.None : CursorLockMode.Locked;

            if (isShowInventory)
            {
                _uiInventory.ShowInventory(_inventory);
            }
        }

        if (!isShowInventory && Input.GetKeyDown(KeyCode.E))
        {
            Vector2 origin = transform.position; // Початкова точка луча - може бути змінена на іншу точку залежно від потреб
            Vector2 direction = Vector2.zero; // Напрямок - змініть його відповідно до вашої логіки

            RaycastHit2D hit = Physics2D.Raycast(origin, direction);

            if (hit.collider != null)
            {
                var pickUpItem = hit.collider.GetComponent<PickUpInventoryItem>();
                if (pickUpItem != null)
                {
                    PickUpItem(pickUpItem);
                }

                var otherInventory = hit.collider.GetComponent<InventoryBehaviour>();
                if (otherInventory != null)
                {
                    OnLookOtherInventory(otherInventory);
                }
            }
        }
    }

    private void PickUpItem(PickUpInventoryItem pickUpItem)
    {
        _inventory.AddItem(pickUpItem.Id, 1); // Приклад передачі кількості предметів (1)
        Destroy(pickUpItem.gameObject);
    }

    private void OnLookOtherInventory(InventoryBehaviour otherInventory)
    {
        _uiInventory.gameObject.SetActive(true);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _uiInventory.ShowInventory(_inventory, otherInventory);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
