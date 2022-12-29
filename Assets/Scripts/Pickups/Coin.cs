using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    [SerializeField] int currencyValue;
    [SerializeField] AudioClip collectedSound;
    Bank bank;
    private void Start()
    {
        bank = FindObjectOfType<Bank>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(collectedSound, Camera.main.transform.position);
            EventMessageBroker.Instance?.Publish(new AddCurrencyEvent(currencyValue));
            Destroy(gameObject);
        }
    }
}
