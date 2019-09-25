using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room4Platforms : MonoBehaviour
{
    Animator anim;
    AudioSource audio1;

    void Start()
    {
        anim = GetComponent<Animator>();
        audio1 = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("HitSwitch");
            audio1.Play();
        }
    }
}
