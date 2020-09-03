﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherFOV : MonoBehaviour
{
    public Animator characterAnimator;
    public Transform character;
    public Transform bow;

    public float viewRadius;
    private float viewAngle = 360;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    private List<Transform> visibleTargets = new List<Transform>();
    private Transform nearestTarget;
    
    private void Start()
    {
        StartCoroutine("SearchTarget", 0.2f);
    }

    IEnumerator SearchTarget(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindTargetsInRange();
        }
    }

    void FindTargetsInRange()
    {
        visibleTargets.Clear();
        Collider[] targetsInviewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for (int i = 0; i < targetsInviewRadius.Length; i++)
        {
            Transform target = targetsInviewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    SortTarget();
                    ShootingDelayTime();
                }
            }
        }
    }

    public void ShootingDelayTime()
    {
        StartCoroutine("WaitForSeconds", 1f);
    }

    IEnumerator WaitForSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        characterAnimator.SetInteger("TargetInRange", visibleTargets.Count);
    }

    void SortTarget()
    {
        visibleTargets.Sort(delegate (Transform a, Transform b)
        {
            return Vector3.Distance(transform.position, a.position).CompareTo(Vector3.Distance(transform.position, b.position));
        });
        nearestTarget = visibleTargets[0];
    }

    void AimTarget(Transform t)
    {
        Vector3 target = new Vector3(t.position.x, character.position.y, t.position.z);
        bow.LookAt(nearestTarget);
        character.LookAt(target);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        foreach (Transform target in visibleTargets)
        {
            if (nearestTarget != null && target.Equals(nearestTarget))
            {
                Gizmos.color = Color.green;
                AimTarget(nearestTarget);
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawRay(transform.position, (target.position - transform.position).normalized * viewRadius);
        }
    }
}
