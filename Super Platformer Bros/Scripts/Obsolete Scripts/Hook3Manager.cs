using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook3Manager : MonoBehaviour
{
    public GameObject currentPlayer;
    public GameObject deadPlayerPrefab;

    public PlayerHealth playerHealth;

    public Transform respawnTransform;
    public Transform deathSpot;

    [SerializeField]
    private float respawnTimer = 3f;

    public bool inHook3;

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
        if (inHook3)
        {
            if (playerHealth.playerDead)
            {
                respawnTimer -= Time.deltaTime;

                if(respawnTimer <= 0)
                {
                    //Kat: The order in which everything is in is really important. I tried changing some things around earlier to fix the respawn issue and the body
                    //was respawning at the respawn transform instead of the player for some reason.--
                    respawnTimer = 3f;
                    deathSpot = currentPlayer.transform;
                    Instantiate(deadPlayerPrefab, deathSpot.position, deadPlayerPrefab.transform.rotation);
                    Debug.Log("Spawn Body At: " + deathSpot.position);
                    //currentPlayer.transform.position = respawnTransform.position;
                    //playerHealth.currentHealth = playerHealth.maxHealth;
                    //playerHealth.playerDead = false;
                }

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inHook3 = true;
            currentPlayer.GetComponent<PlayerMovement>().jumpForce = 20;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //inHook3 = false;
        }
    }
}
