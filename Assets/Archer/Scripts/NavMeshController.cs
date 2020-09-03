using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform destination;
    public ArcherFOV archer;

    private void Update()
    {
        if (archer.characterAnimator.GetInteger("TargetInRange") >= 1)
        {
            agent.SetDestination(transform.position);
        }
        else
        {
            agent.SetDestination(destination.position);
        }
    }

}
