using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook1Manager : MonoBehaviour
{

    public GameObject currentPlayer;
    public PlayerHealth playerHealth;
    public Transform respawnTransform;
    public WallMovement wallMovement;
    public GameObject wall;

    public bool inHook1;

    void Start()
    {
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        playerHealth = currentPlayer.GetComponent<PlayerHealth>();
    }


    void Update()
    {
        CheckHealth();
        WallMovement();
    }

    void CheckHealth()
    {
        if (inHook1)
        {
            if (playerHealth.currentHealth <= 0)
            {
                currentPlayer.transform.position = respawnTransform.position;
                playerHealth.currentHealth = playerHealth.maxHealth;
                inHook1 = false;
            }
        }
    }

    void WallMovement()
    {
        if (inHook1)
        {
            wallMovement.moving = true;
        }
        else if(inHook1 == false)
        {
            wallMovement.moving = false;
            wall.transform.position = wallMovement.startPosition;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inHook1 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inHook1 = false;
        }
    }
}
