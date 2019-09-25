using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R3WallTrigger : MonoBehaviour
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
            anim.SetTrigger("Entered");
            audio1.PlayDelayed(1f);
            PlayOnce = true;
        }
    }
}
