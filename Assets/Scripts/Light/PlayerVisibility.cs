using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerVisibility : MonoBehaviour
{
    public float viewRadius = 10f; // Радіус зони видимості
    public float darknessIntensity = 0.5f; // Інтенсивність темряви поза зоною видимості

    private void Update()
    {
        Collider2D[] allObjects = Physics2D.OverlapCircleAll(transform.position, viewRadius);

        List<Collider2D> allObjectsList = allObjects.ToList(); // Перетворюємо масив у список

        foreach (Collider2D obj in allObjects)
        {
            // Робимо всі об'єкти видимими
            obj.gameObject.SetActive(true);
        }

        // Застосовуємо затемнення для об'єктів поза зоною видимості
        Collider2D[] allObjectsInRange = Physics2D.OverlapCircleAll(transform.position, viewRadius + 1f); // Додатковий радіус для обробки об'єктів за межами viewRadius

        foreach (Collider2D obj in allObjectsInRange)
        {
            if (!allObjectsList.Contains(obj))
            {
                // Змінюємо колір спрайту на темніший
                SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // Застосовуємо темніший колір до спрайту
                    spriteRenderer.color = spriteRenderer.color * darknessIntensity;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Візуалізація зони видимості гравця в редакторі Unity
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}