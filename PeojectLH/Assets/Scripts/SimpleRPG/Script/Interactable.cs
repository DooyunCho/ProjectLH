using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string myName;
    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent playerAgent;
    private bool hasInteracted = false;

    public virtual void MoveToInteraction(UnityEngine.AI.NavMeshAgent playerAgent)
    {
        hasInteracted = false;
        this.playerAgent = playerAgent;
        playerAgent.stoppingDistance = 3f;
        playerAgent.destination = this.transform.position;
        AnimationController.Instance.setTrigger("isRun", true);
    }

    void Update()
    {
        if (!hasInteracted && playerAgent != null && !playerAgent.pathPending)
        {
            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
            {
                Interact();
                EnsureLookDirection();
                hasInteracted = true;
            }
        }
    }

    void EnsureLookDirection()
    {
        playerAgent.updateRotation = false;
        Vector3 lookDirection = new Vector3(transform.position.x, playerAgent.transform.position.y, transform.position.z);
        playerAgent.transform.LookAt(lookDirection);
        playerAgent.updateRotation = true;
    }

    public void CancelInteract()
    {
        Debug.Log("CancelInteract.");
        hasInteracted = true;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with base class.");
    }
}
