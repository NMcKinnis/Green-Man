using System;
using UnityEngine;


public class MeleeEnemy : MonoBehaviour
{

    [SerializeField] float attackCooldown;
    [SerializeField] float range;
    [SerializeField] float damage;
    [SerializeField] float suspicionTime = 3f;
    [SerializeField] float colliderDistance;
    [SerializeField] BoxCollider2D attackCollider;
    [SerializeField] public EnemyPatrol enemyPatrol;
    [SerializeField] public Animator anim;
    [SerializeField] float pursueSpeed = 2.5f;
    [SerializeField] public Rigidbody2D rb;
    bool facingRight = true;
    [SerializeField] LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    float timeSinceLastSawPlayer = Mathf.Infinity;

    //References

    // Health playerHealth;

    public Transform target = null;

    private void Awake()
    {

    }

    private void Update()
    {

        cooldownTimer += Time.deltaTime;
        timeSinceLastSawPlayer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInRange())
        {
            if (cooldownTimer >= attackCooldown)
            {
                timeSinceLastSawPlayer = 0f;
                cooldownTimer = 0f;
                anim.SetTrigger("attack");
            }
        }
        if (target)
        {
            PursueTarget();
        }
        if (target == null && timeSinceLastSawPlayer > suspicionTime)
        {
            enemyPatrol.isPursuing = false;

        }

     }

    private void PursueTarget()
    {
        enemyPatrol.isPursuing = true;


        anim.SetBool("moving", true);
        if (enemyPatrol.movingLeft)
        {
            if (transform.position.x > target.transform.position.x)
                enemyPatrol.MoveInDirection(-1, pursueSpeed);
            else
                enemyPatrol.DirectionChange();
        }
        else
        {
            {
                if (enemyPatrol.enemy.position.x <= target.transform.position.x)
                    enemyPatrol.MoveInDirection(1, pursueSpeed);
                else
                    enemyPatrol.DirectionChange();
            }
        }
    }

    private void FlipSprite()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

    public void ResetTimer()
    {
        timeSinceLastSawPlayer = 0;
    }
    private bool PlayerInRange()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(attackCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(attackCollider.bounds.size.x * range, attackCollider.bounds.size.y, attackCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(attackCollider.bounds.size.x * range, attackCollider.bounds.size.y, attackCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (attackCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            EventMessageBroker.Instance?.Publish(new TriggerEvent(damage, "damaged"));
        }
     
    }
}