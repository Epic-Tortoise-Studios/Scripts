using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_Pickup_Script : MonoBehaviour
{
    public GameObject Player;

    public bool DamagePlayer = false;

    public bool DestroyItem = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerPoisoned();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DamagePlayer = true;

            if (DamagePlayer == true)
            {
                Debug.Log("Player picked up poison");

                DestroyItem = true;
                DestroyPickup();
                PlayerPoisoned();
            }
            else
            {
                DamagePlayer = false;
            }
        }
    }

    void PlayerPoisoned()
    {
        if (DamagePlayer == true)
        {
            if (Player.GetComponent<PlayerHealth>().currentHealth <= Player.GetComponent<PlayerHealth>().maxHealth)
            {
                Player.GetComponent<PlayerHealth>().currentHealth -= 20;

                Debug.Log("Player lost 20 life");

                DamagePlayer = false;
                DestroyItem = true;
                DestroyPickup();

            }
            else if (Player.GetComponent<PlayerHealth>().currentHealth <= 20)
            {
                Player.GetComponent<PlayerHealth>().currentHealth = 0;
                Debug.Log("Player has died");

                DamagePlayer = false;
                DestroyItem = true;
                DestroyPickup();
            }
            
        }
        else
        {
            DamagePlayer = false;
        }
    }

    void DestroyPickup()
    {
        if (DestroyItem == true)
        {
            Destroy(this.gameObject);

            DestroyItem = false;
        }
        else
        {
            DestroyItem = false;
        }
    }

}
