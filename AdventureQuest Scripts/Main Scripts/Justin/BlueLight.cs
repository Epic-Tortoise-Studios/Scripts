using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueLight : MonoBehaviour
{
    Animator anim;
    AudioSource audio1;
    bool PlayOnce;
    //GameObject AfterCannon1;
    //Animator afterCannon;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        audio1 = GetComponent<AudioSource>();
        PlayOnce = false;
        //AfterCannon1 = GameObject.Find("------------L1---------------/AfterCannonAnimation");
        //afterCannon = AfterCannon1.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && PlayOnce == false)
        {
            anim.SetTrigger("SteppedOn");
            //afterCannon.SetTrigger("Triggered");
            audio1.PlayDelayed(1);
            PlayOnce = true;
        }
    }
}
