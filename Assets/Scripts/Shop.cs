using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    Bank bank;
    [field: SerializeField] public int healthCost { get; private set; }

    private void Start()
    {
       
    }
    public void BuyHealth()
    {
        Debug.Log("BuyingHealth");
        bank = FindObjectOfType<Bank>();
        Debug.Log(bank);
        if(healthCost <= bank.GetCurrency())
        {
            bank.SpendCurrency(healthCost);
            EventMessageBroker.Instance?.Publish(new TriggerEvent(1, "healed"));
        }
    }
}
