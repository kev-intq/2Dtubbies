using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private GameManager GM;
    private PlayerHealthBarScript healthbar;
    private PlayerCurrencyScript currency;
    public CurrencyPickUp currencyPickUp;
    public Attribute[] attributes;

    private float currentHealth;

    private void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        healthbar = GetComponent<PlayerHealthBarScript>();
        currency = GetComponent<PlayerCurrencyScript>();

        currentHealth = maxHealth;
        healthbar.health = maxHealth;
        healthbar.maxHealth = maxHealth;
        currency.coinQuantity = 0.0f;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        healthbar.TakeDamage(amount);

        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
        healthbar.RestoreHealth(amount);
    }

    private void Die()
    {
        Debug.Log("dead");
        this.gameObject.GetComponent<Animator>().SetBool("die", true);
        StartCoroutine(DieMaster());
    }

    IEnumerator DieMaster()
    {
        yield return new WaitForSeconds(2);
        GM.Respawn();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {

        if (other.tag == "Currency") 
        {
            Debug.Log("called");
            if (currencyPickUp.pickedUpCurrency == CurrencyPickUp.Currency.COIN)
            {
                CoinPickUp();
                Debug.Log("iscoin");
            }
            Destroy(other.gameObject);
        }
    }
    public void CoinPickUp()
    {
        currency.coinQuantity += 0.5f;
        currency.PickedUpCoin();
        Debug.Log(currency.coinQuantity);
    }
}
