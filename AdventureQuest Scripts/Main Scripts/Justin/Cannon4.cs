using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon4 : MonoBehaviour
{
    Animator anim;
    AudioSource audio1;
    bool PlayOnce;
    GameObject CannonLanding;
    AudioSource CannonLandingAudio;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        audio1 = GetComponent<AudioSource>();
        PlayOnce = false;
        CannonLanding = GameObject.Find("------Scene Essentials-------------/Sounds/CannonBallLanding");
        CannonLandingAudio = CannonLanding.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && PlayOnce == false)
        {
            anim.SetTrigger("SteppedOn");
            CannonLandingAudio.PlayDelayed(1.5f);
            audio1.PlayDelayed(1);
            PlayOnce = true;
        }
    }
}
