using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    //public Hook3Respawn hook3Respawn;

    //public GameObject deadPlayer;
    public GameObject currentPlayer;
    private PlayerHealth playerHealth;

    //public bool spawnBody;

    void Start()
    {

    }


    void Update()
    {
        //SpawnBody();

        //Check for current player with whatever player object is in the scene.
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        playerHealth = currentPlayer.GetComponent<PlayerHealth>();
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Set the players health to 0 - this is not where the GameObject gets destroyed. Look in the Hook3Respawn Script instead.
            playerHealth.playerDead = true;

            //Will allow the SpawnBody function to work
            //spawnBody = true;
        }
    }*/

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth.currentHealth = 0;
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerHealth.currentHealth = 0;
            Debug.Log("Collision");
            
        }
    }


    //This is for the TestTrap Prefab. It will activate the child object that takes the shape of the players body. It's not needed for the BasicTrap.
    /*void SpawnBody()
    {
        if (spawnBody)
        {
            //Sets the child object (a copy of the players body laying down) to active
            deadPlayer.SetActive(true);           

            spawnBody = false;
        }
    }*/
}
