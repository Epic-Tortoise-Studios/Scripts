using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaStomp : MonoBehaviour
{
    //public AudioClip enemyDeathSound;
    //AudioSource audioSrc;

    public GameObject enemyBody;
    public Vector3 deathSpot;
    

    void Start()
    {
        //audioSrc.GetComponent<AudioSource>();

       
        deathSpot = transform.parent.position;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           Destroy(transform.parent.gameObject);

            //audioSrc.PlayOneShot(enemyDeathSound);
        }
        
    }

    //Kat: This will spawn a body on death, the body prefab is the one that spawns the ghost
   /*private void OnDestroy()
    {
        Instantiate(enemyBody, deathSpot, Quaternion.identity);
        Debug.Log("Spawning Body");
    }*/

}
