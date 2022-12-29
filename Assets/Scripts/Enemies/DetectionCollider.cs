using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCollider : MonoBehaviour
{
    MeleeEnemy meleeEnemy;
    private void Awake()
    {
        meleeEnemy = GetComponentInParent<MeleeEnemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            meleeEnemy.target = collision.gameObject.transform;
            meleeEnemy.ResetTimer();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            meleeEnemy.target = null;
        }
    }
}
