using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEnemySpawn : MonoBehaviour
{
    public GameObject currentPlayer;
    private PlayerHealth playerHealth;

    //Enemy spawn trigger
    public GameObject[] enemyTrigger;
    private EnemySpawnTrigger enemySpawn;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        playerHealth = currentPlayer.GetComponent<PlayerHealth>();

        for (int i = 0; i > 0; i++)
        {
            enemyTrigger[i] = GameObject.FindGameObjectWithTag("EnemySpawn");
            enemySpawn = enemyTrigger[i].GetComponent<EnemySpawnTrigger>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (playerHealth.playerDead == true)
        {
            foreach (GameObject enemySpawn in enemyTrigger)
            {
                enemySpawn.GetComponent<Collider>().enabled = true;
            }
        }
    }


}
