using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosition : StateMachineBehaviour
{
    private float boundary = 15f;

    public LayerMask disallowedLayers;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float randomX = Random.Range(-boundary, boundary);
        float randomZ = Random.Range(-boundary, boundary);

        Vector3 currentPosition = animator.transform.position;

        Vector3 newPosition = new Vector3(currentPosition.x + randomX, currentPosition.y, currentPosition.z + randomZ);

        Collider2D overlap = Physics2D.OverlapCircle(newPosition, 0.1f, disallowedLayers);

        if (overlap == null)
        {
            animator.transform.position = newPosition;
        }
    }
}
