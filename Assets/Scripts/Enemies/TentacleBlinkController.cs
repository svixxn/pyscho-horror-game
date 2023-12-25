using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleBlinkController : MonoBehaviour
{
    public float updateStatesTime = 10.0f;
    private float timer = 0f;
    public Animator eyeAnimator;
    public GameObject watchZone;
    private bool isMouthOpened = false;

    private Coroutine patternChangeCoroutine;


    private void Start()
    {

        
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= updateStatesTime)
        {
            int randomIndex1 = Random.Range(0, 2);
            int randomIndex2;
            if (!isMouthOpened)
            {
               randomIndex2 = Random.Range(0, 2);
               if(randomIndex2 == 1)
                {
                    isMouthOpened = true;
                }
            }
            else
            {
                randomIndex2 = 0;
            }
            
            eyeAnimator.SetInteger("StateAfterIdle", randomIndex1);
            eyeAnimator.SetInteger("StateAfterIdleClosed", randomIndex2);
            timer = 0f;
        }

        if (eyeAnimator.GetBool("IsZone"))
        {
            watchZone.SetActive(true);
        }
        else
        {
            watchZone.SetActive(false);
        }

        if (eyeAnimator.GetBool("IsZone"))
        {
            watchZone.SetActive(true);
        }
        else
        {
            watchZone.SetActive(false);
        }
    }

}
