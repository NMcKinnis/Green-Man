using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    [field: SerializeField] public Animator hudAnimator { get; private set; }
    [field: SerializeField] public LevelLoader levelLoader { get; private set; }
    [field: SerializeField] public AudioClip hurtSound { get; private set; }
    [field: SerializeField] public AudioClip deathSound { get; private set; }

    public float currentHealth { get; private set; }
    private bool isDead = false;
    Animator animator;
    private void Awake()
    {
        currentHealth = startingHealth;
        EventMessageBroker.Instance?.Subscribe<TriggerEvent>(UpdateHealth);
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        hudAnimator = GameObject.FindGameObjectWithTag("Hud").GetComponent<Animator>();

    }
    private void OnEnable()
    {
        SubscribeToBroker();
    }

    public void SubscribeToBroker()
    {
        EventMessageBroker.Instance?.Subscribe<TriggerEvent>(UpdateHealth);
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        hudAnimator = GameObject.FindGameObjectWithTag("Hud").GetComponent<Animator>();
    }

    private void OnDisable()
    {
        EventMessageBroker.Instance?.Unsubscribe<TriggerEvent>(UpdateHealth);
    }

    public void UpdateHealth(TriggerEvent ev)
    {
        switch (ev.status)
        {
            case "damaged":

                if (!isDead)
                {
                    AudioSource.PlayClipAtPoint(hurtSound, Camera.main.transform.position);
                    currentHealth -= ev.amount;
                    if (animator) { animator.SetTrigger("hit"); }
                    hudAnimator.SetTrigger("hit");
                    if (currentHealth <= 0)
                    {
                        var player = FindObjectOfType<Player>();
                        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
                        if (animator) { animator.SetTrigger("die"); } else { player.GetComponent<Animator>(); }
                        player.inputAction.Disable();
                        isDead = true;
                        EventMessageBroker.Instance?.Publish(new StatusEvent(true));
                        StartCoroutine("Respawn");
                    }
                }
                break;
            case "healed":
                currentHealth += ev.amount;
                if (currentHealth > startingHealth)
                {
                    currentHealth = startingHealth;
                }
                break;
        }

    }


    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3f);
        currentHealth = startingHealth;
        levelLoader.ReloadScene();
    }
}
