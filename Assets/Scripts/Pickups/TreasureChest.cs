using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] TextMeshPro chestCostText;
    [SerializeField] string nameOfUpgrade;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip collectedSound;
    Bank bank;
    bool isOpen = false;
    int purchaseCost = 7;
    // Start is called before the first frame update
    void Start()
    {

        bank = FindObjectOfType<Bank>();
        chestCostText.text = purchaseCost.ToString();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().treasureChest = (this);
        }
    }

    internal void Open()
    {
        Debug.Log("open");
        if (bank.HaveEnoughCurrency(purchaseCost))
        {
            if (!isOpen)
            {
                AudioSource.PlayClipAtPoint(collectedSound, Camera.main.transform.position);
                bank.SpendCurrency(purchaseCost);
                isOpen = true;
                animator.SetTrigger("open");
                PlayerPrefs.SetInt(nameOfUpgrade, 1);
            }
        }
    }

    public void DestroyGameOject() //called by the animator
    {
        Destroy(gameObject);
    }
}
