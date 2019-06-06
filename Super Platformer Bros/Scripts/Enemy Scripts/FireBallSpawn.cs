using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpawn : MonoBehaviour
{
    
    public GameObject fireBall; //Always Keep
    private bool shotReady; //Always Keep
    //public float fireTimer;  //Set
    
    //4 below are for "Random" fire rate
    public float maxTime = 0;
    public float minTime = 0;
    private float time;
    private float spawnTime;

    void Start()
    {
        shotReady = true; //Always Keep
        time = minTime; //Random
    }

    //Below is for "Random" fire rate
    private void FixedUpdate()
    {
        time += Time.deltaTime;

        if(time >= spawnTime)
        {
            if (shotReady)
            {
                Shoot();
            }
        }
    }

    //Below is for "Set" fire rate
    /*void Update()
    {
            if (shotReady)
            {
                Shoot();
            }
    }*/

    void Shoot()
    {
        time = 0; //Random
        Instantiate(fireBall, transform.position + (transform.forward * 4), transform.rotation); //Always Keep
        shotReady = false; //Always Keep
        //StartCoroutine(FireRate()); //Set
        RandomSpawn(); //Random
    }

    //Below is for "Random" fire rate
    void RandomSpawn()
    {
        spawnTime = Random.Range(minTime, maxTime);
        shotReady = true;
    }

    //Below is for "Set" fire rate 
    /*IEnumerator FireRate()
    {
        yield return new WaitForSeconds(fireTimer);
        shotReady = true;
    }*/
}
