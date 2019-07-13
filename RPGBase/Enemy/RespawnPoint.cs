using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject EnemyTarget;
    
    void Start()
    {
        //SpawnEnemy();
    }
    
    void Update()
    {
        
    }
    
    public void SpawnEnemy()
    {
        Vector3 RandomSpawn = new Vector3 (this.transform.position.x + Random.Range(-20, 20), this.transform.position.y, this.transform.position.z + Random.Range(-20, 20));

        GameObject clone;
        clone = Instantiate(EnemyPrefab, RandomSpawn, Quaternion.identity);
        EnemyTarget = clone;
        EnemyTarget.transform.GetComponent<EnemyStats>().RespawnPointLoc = this.gameObject;

        RaycastHit hit;

        if (Physics.Raycast (EnemyTarget.transform.position, -Vector3.up, out hit))
        {
            EnemyTarget.transform.position = new Vector3(EnemyTarget.transform.position.x, hit.point.y + 5, EnemyTarget.transform.position.z);
        }
    }
}
