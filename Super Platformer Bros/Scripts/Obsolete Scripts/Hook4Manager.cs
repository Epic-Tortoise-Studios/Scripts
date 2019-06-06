using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook4Manager : MonoBehaviour
{
    public GameObject currentPlayer;
    public PlayerHealth playerHealth;
    public HookSection hookSection;
    public Transform respawnTransform;
    public WallMovement topWallMovement;
    public WallMovement bottomWallMovement;
    public GameObject topWall;
    public GameObject bottomWall;

    public bool inHook4;

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
        if (inHook4)
        {
            if (playerHealth.currentHealth <= 0)
            {
                currentPlayer.transform.position = respawnTransform.position;
                playerHealth.currentHealth = playerHealth.maxHealth;
                inHook4 = false;
            }
        }
    }

    void WallMovement()
    {
        if (inHook4)
        {
            topWallMovement.moving = true;
            bottomWallMovement.moving = true;
        }
        else if (inHook4 == false)
        {
            topWallMovement.moving = false;
            bottomWallMovement.moving = false;
            topWall.transform.position = topWallMovement.startPosition;
            bottomWall.transform.position = bottomWallMovement.startPosition;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inHook4 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inHook4 = false;
            hookSection.hook4Camera.SetActive(false);
            hookSection.hook4Canvas.SetActive(false);
        }
    }
}

