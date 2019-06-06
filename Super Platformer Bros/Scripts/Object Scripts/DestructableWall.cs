using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour
{
    //public AudioClip ghostFireSound;
    //AudioSource audioSource;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("PlayerFireBall"))
        {
            //audioSource.PlayOneShot(ghostFireSound);
            Destroy(this.gameObject);
        }
    }
}
