using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] static public int currency { get; private set; }
    [SerializeField] AudioClip spendSound;
    TextMeshProUGUI currencyText;
    private void Awake()
    {
        EventMessageBroker.Instance?.Subscribe<AddCurrencyEvent>(AddCurrency);
    }

    void Start()
    {
        currencyText = GetComponent<TextMeshProUGUI>();
        UpdateDisplay();
    }
    private void UpdateDisplay()
    {
        if (currencyText)
        {
            currencyText.text = currency.ToString();
        }
    }


    public bool HaveEnoughCurrency(int amount)
    {
        return currency >= amount;
    }

    public void AddCurrency(AddCurrencyEvent ev)
    {
        currency += ev.addedCurrency;
        UpdateDisplay();
    }
    public int GetCurrency()
    {
        return currency;
    }

    public void SpendCurrency(int amount)
    {
        Debug.Log("SpendCurrency() called");
        if (currency >= amount)
        {
            Debug.Log("currency sufficient");
            AudioSource.PlayClipAtPoint(spendSound, Camera.main.transform.position);
            currency -= amount;
            UpdateDisplay();
        }
    }
    private void OnDestroy()
    {
        EventMessageBroker.Instance?.Unsubscribe<AddCurrencyEvent>(AddCurrency);
    }
}