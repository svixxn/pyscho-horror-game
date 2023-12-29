using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObjectOnDestroy1 : MonoBehaviour
{
    public GameObject objectToShow;
    public GameObject tileMapObject;

    private void OnDestroy()
    {
        if (tileMapObject != null)
        {
            tileMapObject.SetActive(false);
        }
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }
    }
}
