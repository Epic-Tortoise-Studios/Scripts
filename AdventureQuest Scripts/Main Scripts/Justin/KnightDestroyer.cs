using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightDestroyer : MonoBehaviour
{
    public AudioSource knightCrunch;
    public GameObject knightCrunchSource;
    public GameObject knightHitSource;
    public AudioSource knightHit;

    void Start()
    {
        knightCrunchSource = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxAudio/Crunch");
        knightCrunch = knightCrunchSource.GetComponent<AudioSource>();
        knightHitSource = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxAudio/Hit");
        knightHit = knightHitSource.GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Knight")
        {
            knightCrunch.PlayDelayed(1);
            knightHit.Play();
            Destroy(collision.gameObject, 1);
        }

        if(collision.tag == "KnightHead")
        {
            collision.GetComponentInChildren<KnightShoot>();
            Destroy(collision.GetComponentInChildren<KnightShoot>());
        }
    }

}
