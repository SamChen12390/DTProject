using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float maxHealth;
    public float attackDamage;
    public HealthBar healthBar;
    public ShotEffect effect1;
    public ShotEffect effect2;
    public AE_AnimatorEvents aE_AnimatorEvents;

    private void Start()
    {
        if (transform.CompareTag("Archer"))
        {
            if (transform.gameObject.layer == LayerMask.NameToLayer("Blue team"))
            {
                aE_AnimatorEvents.Effect1.Prefab = effect1._effect1;
                aE_AnimatorEvents.Effect2.Prefab = effect1._effect2;
            }
            if (transform.gameObject.layer == LayerMask.NameToLayer("Red team"))
            {
                aE_AnimatorEvents.Effect1.Prefab = effect2._effect1;
                aE_AnimatorEvents.Effect2.Prefab = effect2._effect2;
            }
        }
    }

    [System.Serializable]
    public class ShotEffect
    {
        public GameObject _effect1;
        public GameObject _effect2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent.gameObject.CompareTag("Arrow"))
        {
            healthBar.TakeDamage(attackDamage);
            Destroy(collision.transform.parent.gameObject);
        }
    }
}