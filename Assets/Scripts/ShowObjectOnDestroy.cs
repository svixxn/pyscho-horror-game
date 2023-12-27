using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObjectOnDestroy : MonoBehaviour
{
    public GameObject objectToShow; 

    private void OnDestroy()
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(true); 
        }
    }
}