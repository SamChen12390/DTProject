using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Building1").transform;
        agent.destination = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.transform.position;
    }
}
