using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    public Transform Spawnpoint;
    public GameObject patrolPoints;
    public int Once;

    void Start()
    {
        Spawnpoint = GetComponent<Transform>();
        Once = 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Once == 1)
        {
            Instantiate(enemy, Spawnpoint.transform.position, Spawnpoint.transform.rotation);
            Instantiate(patrolPoints, Spawnpoint.transform.position, Spawnpoint.transform.rotation);
            print("Spawned Enemy");
            Once = 2;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && Once == 2)
        {
            Destroy(enemy, 1f);
            Destroy(patrolPoints, 1f);
            print("Destroyed Enemy");
            Once = 1;
        }
    }
}
