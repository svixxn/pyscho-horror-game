using UnityEngine;

public class LightZoneController : MonoBehaviour
{
    public Light lightSource; // —силка на джерело св≥тла в Unity
    public LayerMask lightBlockingLayer; // Ўар, що блокуЇ св≥тло

    void Update()
    {
        if (lightSource != null)
        {
            RaycastHit hit;
            Ray ray = new Ray(lightSource.transform.position, lightSource.transform.forward);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, lightBlockingLayer))
            {
                // якщо св≥тло з≥штовхнулос€ з об'Їктом у шар≥, що блокуЇ св≥тло,
                // приховуЇмо цей об'Їкт або зм≥нюЇмо його параметри (наприклад, зм≥нюЇмо матер≥ал на темний)
                hit.transform.gameObject.SetActive(false); // ѕриховати об'Їкт

                // якщо потр≥бно зм≥нити матер≥ал на темний:
                // Renderer renderer = hit.transform.GetComponent<Renderer>();
                // renderer.material.color = Color.black;
            }
            else
            {
                // якщо св≥тло не з≥штовхнулос€ з об'Їктом у шар≥, забезпечуЇмо, що цей об'Їкт видимий
                // hit.transform.gameObject.SetActive(true); // јбо повертаЇмо об'Їкт назад до видимого стану

                // якщо потр≥бно зм≥нити матер≥ал на стандартний:
                // Renderer renderer = hit.transform.GetComponent<Renderer>();
                // renderer.material.color = Color.white;
            }
        }
    }
}