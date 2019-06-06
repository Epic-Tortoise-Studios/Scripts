using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook2Manager : MonoBehaviour
{
    public GameObject currentPlayer;

    public PlayerHealth playerHealth;

    public Transform respawnTransform;

    public bool inHook2;

    void Start()
    {
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        playerHealth = currentPlayer.GetComponent<PlayerHealth>();
    }


    void Update()
    {
        CheckHealth();

        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        playerHealth = currentPlayer.GetComponent<PlayerHealth>();
    }

    void CheckHealth()
    {
        if (inHook2)
        {
            if (playerHealth.currentHealth <= 0)
            {
                currentPlayer.transform.position = respawnTransform.position;
                playerHealth.currentHealth = playerHealth.maxHealth;
                Debug.Log("Should Respawn");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inHook2 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inHook2 = false;
            currentPlayer.GetComponentInChildren<playerShoot>().enabled = false;
        }
    }
}
