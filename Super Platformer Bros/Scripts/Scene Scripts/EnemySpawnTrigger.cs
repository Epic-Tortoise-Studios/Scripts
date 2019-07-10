using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public GameObject respawnPrefab;
    public GameObject[] respawns;

    
    public void Start()
    {
        if (respawns == null)
            respawns = GameObject.FindGameObjectsWithTag("EnemySpawn");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            foreach (GameObject respawn in respawns)
            {
                Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation);
                //Destroy(gameObject);
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void ResetSpawn()
    {
        gameObject.GetComponent<Collider>().enabled = true;
    }
}