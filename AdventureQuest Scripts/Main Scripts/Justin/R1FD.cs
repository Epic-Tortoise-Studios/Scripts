using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R1FD : MonoBehaviour
{
    Animator anim;
    AudioSource audio1;
    bool PlayOnce;


    void Start()
    {
        anim = GetComponent<Animator>();
        audio1 = GetComponent<AudioSource>();
        PlayOnce = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && PlayOnce == false)
        {
            anim.SetBool("Entered", true);
            audio1.Play();
            PlayOnce = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && PlayOnce == false)
        {
            anim.SetBool("Entered", false);
            audio1.Stop();
        }
    }
}
