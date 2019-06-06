using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGhost : MonoBehaviour
{
    public GameObject ghostPrefab;

    //public AudioClip ghostSpawnSound;

    //AudioSource audioSrc;

    void Start()
    {
        //audioSrc = GetComponent<AudioSource>();
        Spawn();
    }

    void Update()
    {
        
    }

    void Spawn()
    {
        Instantiate(ghostPrefab, this.gameObject.transform.position, ghostPrefab.transform.rotation);
        Debug.Log("Spawning Ghost");

        //audioSrc.PlayOneShot(ghostSpawnSound);
    }
}
