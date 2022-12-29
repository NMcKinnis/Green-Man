using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float startingHealth;
    [SerializeField] GameObject parentObject;
    [SerializeField] Animator animator;
    [SerializeField] EnemyPatrol enemyPatrol;
    public float currentHealth{ get; private set; }
    [HideInInspector] public bool isDead = false;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;

    public float defense = 0;
    private void Awake()
    {
        currentHealth = startingHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            if (deathSound && !isDead)
            {
                AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
            }
            if (enemyPatrol != null)
            {
                enemyPatrol.speed = 0;
            }
            animator.SetBool("dying", true);
            isDead = true;
            DestroyEnemy();
        }
       
    }

    public void TakeDamage(float damage)
    {
        if (animator.GetBool("dying")) { return; }

        if (hitSound)
        {
            AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);
        }
        currentHealth -= (damage - defense);
        animator.SetTrigger("hit");
    }

    public void DestroyEnemy() // called by animator
    {
           Destroy(parentObject);
    }

}
