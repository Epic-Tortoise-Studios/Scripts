using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBody : MonoBehaviour
{
    public GameObject deadPlayerPrefab;
    public GameObject currentPlayer;

    public PlayerHealth playerHealth;

    public Hook3Manager hookManager;
    
    public Transform deathSpot;



    void Start()
    {
        currentPlayer = this.gameObject;
        playerHealth = gameObject.GetComponent<PlayerHealth>();

        hookManager = GameObject.FindGameObjectWithTag("Hook3Respawn").GetComponent<Hook3Manager>();
       
    }


    void Update()
    {
        OnDeath();
    }

    void OnDeath()
    {
        if (hookManager.inHook3)
        {
            if (playerHealth.playerDead)
            {
                deathSpot = currentPlayer.transform;
                Instantiate(deadPlayerPrefab, deathSpot.position, deadPlayerPrefab.transform.rotation);
                Debug.Log("Spawn Body At: " + deathSpot.position);
                playerHealth.playerDead = false;
            }
        }
    }

}
