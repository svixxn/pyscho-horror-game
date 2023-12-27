using System;
using System.Collections;
using UnityEngine;

public class ObjectAnimation : MonoBehaviour
{
    public AnimationClip prepareAnimation; 
    public AnimationClip spawnAnimation; 
    public AnimationClip idleAnimation; 
    public AnimationClip exitAnimation; 

    public float spawnDelay = 2f; 
    public bool isIdle = true; 

    public int damageAmount = 10; 
    public float damageInterval = 5f; 

    private Animator animator;
    private bool isPlayerInRange = false;

    public float idleDurationBeforeExit = 3f;

    private DamageZone getDamage;

    void Start()
    {
        animator = GetComponent<Animator>();

        getDamage = GetComponent<DamageZone>();

        if(getDamage == null )
        {
            Debug.LogError("DamageZone script not found!");
        }
        else
        {
            getDamage.isEnabled = false;
        }

        StartCoroutine(SpawnSequence());
        StartCoroutine(ApplyDamage());
    }

    IEnumerator SpawnSequence()
    {
        if (prepareAnimation != null)
        {
            animator.Play(prepareAnimation.name); 
            yield return new WaitForSeconds(prepareAnimation.length); 
        }

        yield return new WaitForSeconds(spawnDelay); 

        if (spawnAnimation != null)
        {
            getDamage.isEnabled = true;
            animator.Play(spawnAnimation.name); 
            yield return new WaitForSeconds(spawnAnimation.length); 
        }

        if (isIdle)
        {
            if (idleAnimation != null)
            {
                animator.Play(idleAnimation.name); 
                yield return new WaitForSeconds(idleDurationBeforeExit); 
            }
        }
        if (exitAnimation != null)
        {
           animator.Play(exitAnimation.name); 
           yield return new WaitForSeconds(exitAnimation.length); 
        }
        

        Destroy(gameObject);
    }

    IEnumerator ApplyDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(damageInterval);

            if (isPlayerInRange)
            {
                Debug.Log("Dealt " + damageAmount + " damage!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
