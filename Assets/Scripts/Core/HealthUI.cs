using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    Health health;
    TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void OnEnable()
    {
        health = FindObjectOfType<Health>();
       healthText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
   
        healthText.text = health.currentHealth.ToString();
    }

}
