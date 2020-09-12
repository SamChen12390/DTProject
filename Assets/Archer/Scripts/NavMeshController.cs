using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform destination;
    public ArcherFOV archer;
    public bool Stop = false;

    private void Update()
    {
        if (!Stop)
        {
            agent.isStopped = false;
            if (archer.characterAnimator.GetInteger("TargetInRange") >= 1)
            {
                agent.SetDestination(transform.position);
            }
            else
            {
                if (destination != null)
                {
                    if ((transform.position - destination.position).magnitude <= 2f)
                    {
                        agent.SetDestination(transform.position);
                    }
                    else
                    {
                        agent.SetDestination(destination.position);
                    }
                }
            }
        }
        else
        {
            agent.isStopped = true;
            archer.characterAnimator.Play("Idle");
        }
    }

    public void SetTarget(Transform target)
    {
        this.destination = target;
    }

}
