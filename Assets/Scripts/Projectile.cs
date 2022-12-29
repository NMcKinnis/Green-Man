using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 10f;
    public float projectileDamage = 1f;
    [SerializeField] float projectileTimer = 1f;
    [SerializeField] Rigidbody2D rb;
    Animator animator;
    [SerializeField] AudioClip impactSound;

    /*   Vector3 shootDirection;
       public void Setup(Vector3 shootDirection)
       {
           this.shootDirection = -shootDirection;
       }*/

    private void OnDestroy()
    {

        Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") { return; }
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(collision.gameObject.name);
            AudioSource.PlayClipAtPoint(impactSound, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(impactSound, Camera.main.transform.position);
        Destroy(gameObject);

    }


    public void StartTimer()
    {
        StartCoroutine("FadeOutProjectile");
    }

    IEnumerator FadeOutProjectile()
    {
        yield return new WaitForSeconds(projectileTimer);
        Destroy(gameObject);
    }

    internal void SetDirectionAndShoot(bool isFacingRight)
    {
        if (!isFacingRight)
        {
            rb.velocity = new Vector2(-projectileSpeed, 0);

        }
        else
        {
            rb.velocity = new Vector2(projectileSpeed, 0);
        }
    }
}
