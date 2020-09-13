using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum Status
{
    Idle,
    Propel,
    Sprint,
    Attack,
    Frantic,
    Die
}
public class BerserkerController : MonoBehaviour
{
    public float health = 100;
    public Status state;
    public float hatredRange=30.0f;
    public float sprintRange = 20.0f;
    GameObject target;
    public float attackDamage = 1.0f;
    
    NavMeshAgent agent;
    public bool isWandering;
    void Start()
    {
        state = Status.Idle;
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Target");
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(target.transform.position, transform.position);
        switch (state)
        {
            case Status.Idle:
                 if(distance<=hatredRange)
                {
                    state = Status.Propel;
                }
                 break;

            case Status.Propel:
                agent.SetDestination(target.transform.position);
                transform.GetComponent<Animator>().SetBool("have_a_path_to_enemy", true);
                if(distance<=sprintRange)
                {
                    state = Status.Sprint;
                }
                break;
            case Status.Sprint:
                agent.speed = 6.0f;

                    if (distance <= agent.stoppingDistance)
                    {
                        state = Status.Attack;
                    }

                 break;
            case Status.Attack:
                attack();
                if (health <= 30)
                {
                        state = Status.Frantic;
                }
                break;
            case Status.Frantic:
                transform.GetComponent<Animator>().speed = 5.0f;
                if(health<=0)
                {
                    state = Status.Die;
                }
                break;

            case Status.Die:
                Dead();
                break;
        }
       
    }

    public void LookAtTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, hatredRange);
    }

/*    public void propel()
    {
        LookAtTarget();
        agent.SetDestination(target.transform.position);
        transform.GetComponent<Animator>().SetBool("have_a_path_to_enemy", true);

    }*/

    public void attack()
    {
        transform.GetComponent<Animator>().SetBool("in_attack_range", true);
    }

    public void Dead()
    {
        gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        transform.GetComponent<Animator>().SetBool("die", true);
    }

    public void wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(1, 2);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);



    }
}
