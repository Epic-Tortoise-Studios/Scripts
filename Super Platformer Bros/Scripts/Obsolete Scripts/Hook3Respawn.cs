using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook3Respawn : MonoBehaviour
{
    //public GameObject playerPrefab;
    public GameObject currentPlayer;

    public PlayerHealth playerHealth;

    public Transform respawnTransform;

    public bool inHook3 = false;

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
        if(inHook3)
        {
            if (playerHealth.currentHealth <= 0)
            {
                currentPlayer.transform.position = respawnTransform.position;
                playerHealth.currentHealth = playerHealth.maxHealth;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered Hook 3");

            inHook3 = true;
        }
    }
}
