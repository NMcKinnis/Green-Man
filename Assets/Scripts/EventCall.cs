using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct CurrencyUsageEvent
{
    public float currencyUsage;


    public CurrencyUsageEvent(int currencyUsage)
    {
        this.currencyUsage = currencyUsage;
    }
}public struct AddCurrencyEvent
{
    public int addedCurrency;


    public AddCurrencyEvent(int addedCurrency)
    {
        this.addedCurrency = addedCurrency;
    }
}

public struct InsufficientCurrencyEvent { }



public struct BuffEvent
{
    public float upKeepSustained;
    public bool status;

    public BuffEvent(float upKeepSustained, bool status)
    {
        this.upKeepSustained = upKeepSustained;
        this.status = status;
    }
}

public struct ActivationEvent
{
    public string activationName;

    public ActivationEvent(string activationName)
    {
        this.activationName = activationName;
    }
}

public struct TriggerEvent
{
    public float amount;

    public string status;

    public TriggerEvent(float amount, string status)
    {
        this.amount = amount;
        this.status = status;
    }
}

public struct StatusEvent
{
    public bool isDead;

    public StatusEvent(bool isDead)
    {
        this.isDead = isDead;
    }
}

public struct EntranceEvent { }

public struct WaterEvent
{
    public bool inWater;

    public float gravity;
    public WaterEvent(bool inWater, float gravity)
    {
        this.inWater = inWater;
        this.gravity = gravity;
    }
}