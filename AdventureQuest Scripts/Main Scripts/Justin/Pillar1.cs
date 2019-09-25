using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar1 : MonoBehaviour
{
    Animator anim;
    AudioSource audio1;
    bool PlayOnce;
    //GameObject AfterCannon;
    //Animator afterCannon;
    //GameObject Room2Animator;
    //Animator Room2;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        audio1 = GetComponent<AudioSource>();
        PlayOnce = false;
        //AfterCannon = GameObject.Find("------------L1---------------/AfterCannonAnimation");
        //afterCannon = AfterCannon.GetComponent<Animator>();
        //Room2Animator = GameObject.Find("-----------Second Room------------/Room2Animator");
        //Room2 = Room2Animator.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && PlayOnce == false)
        {
            anim.SetTrigger("SteppedOn");
            //afterCannon.SetTrigger("Triggered");
            //Room2.SetTrigger("Purple");
            audio1.PlayDelayed(1);
            PlayOnce = true;
        }
    }
}
