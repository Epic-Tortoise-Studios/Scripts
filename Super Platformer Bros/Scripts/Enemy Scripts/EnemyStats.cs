using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private PlayerHealth playerHealth;

    public string enemyName;
    public int damage;
    public int maxHealth;
    public int currentHealth;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        currentHealth = maxHealth;
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log(enemyName + " did " + damage + " points of damage to the player");
            playerHealth.currentHealth -= damage;
        }
    }

    private void CheckHealth()
    {
        //Kat: This is where we'll check for how much damage the player has dealt to the enemy, and destroy them once currentHealth <= 0
    }
}
