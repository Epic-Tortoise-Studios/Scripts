using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Pickup : MonoBehaviour
{
    public GameObject Player;

    public bool HealPlayer = false;

    public bool DestroyItem = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerHealed();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HealPlayer = true;

            if (HealPlayer == true)
            {
                Debug.Log("Player picked up health");

                PlayerHealed();
            }
            else
            {
                HealPlayer = false;
            }
        }
    }

    void PlayerHealed()
    {
        if (HealPlayer == true)
        {
            if (Player.GetComponent<PlayerHealth>().currentHealth == Player.GetComponent<PlayerHealth>().maxHealth)
            {
                
                Player.GetComponent<PlayerHealth>().currentHealth = Player.GetComponent<PlayerHealth>().maxHealth;

                Debug.Log("Player is Already at full life");

                HealPlayer = false;
                DestroyItem = true;
                DestroyPickup();

            }
            else if (Player.GetComponent<PlayerHealth>().currentHealth < Player.GetComponent<PlayerHealth>().maxHealth)
            {
                if (Player.GetComponent<PlayerHealth>().currentHealth <= 80)
                {
                    Player.GetComponent<PlayerHealth>().currentHealth += 40;

                    Debug.Log("Player gained 40 life");

                    HealPlayer = false;
                    DestroyItem = true;
                    DestroyPickup();
                }
                else if ((Player.GetComponent<PlayerHealth>().currentHealth < Player.GetComponent<PlayerHealth>().maxHealth) && (Player.GetComponent<PlayerHealth>().currentHealth > 80))
                {
                    Player.GetComponent<PlayerHealth>().currentHealth = Player.GetComponent<PlayerHealth>().maxHealth;

                    Debug.Log("Player is at max life");

                    HealPlayer = false;
                    DestroyItem = true;
                    DestroyPickup();
                }
                
            }
            
        }
        else
        {
            HealPlayer = false;
        }
    }

    void DestroyPickup()
    {
        if (DestroyItem == true)
        {
            Destroy(this.gameObject);

            DestroyItem = false;
        }
    }

}
