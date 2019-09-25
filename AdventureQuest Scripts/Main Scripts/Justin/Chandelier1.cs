using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chandelier1 : MonoBehaviour
{
    Animator anim;
    AudioSource audio1;
    bool PlayOnce;


    void Start()
    {
        anim = GetComponentInParent<Animator>();
        audio1 = GetComponent<AudioSource>();
        PlayOnce = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && PlayOnce == false)
        {
            anim.SetTrigger("SteppedOn");
            audio1.Play();
            PlayOnce = true;
        }
    }
}
