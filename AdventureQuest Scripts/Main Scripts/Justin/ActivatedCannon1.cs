using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedCannon1 : MonoBehaviour
{
    Animator anim;
    AudioSource audio1;
    bool PlayOnce;
    GameObject AfterCannon1;
    Animator afterCannon;
    GameObject LandingPillar;
    AudioSource LandingPillarAudio;
    GameObject CannonLanding;
    AudioSource CannonLandingAudio;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        audio1 = GetComponent<AudioSource>();
        PlayOnce = false;
        AfterCannon1 = GameObject.Find("------------L1---------------/AfterCannonAnimation");
        afterCannon = AfterCannon1.GetComponent<Animator>();
        LandingPillar = GameObject.Find("------Scene Essentials-------------/Sounds/PillarLanding");
        LandingPillarAudio = LandingPillar.GetComponent<AudioSource>();
        CannonLanding = GameObject.Find("------Scene Essentials-------------/Sounds/CannonBallLanding");
        CannonLandingAudio = CannonLanding.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && PlayOnce == false)
        {
            anim.SetTrigger("SteppedOn");
            afterCannon.SetTrigger("Triggered");
            LandingPillarAudio.PlayDelayed(5);
            CannonLandingAudio.PlayDelayed(1.5f);
            audio1.PlayDelayed(1);
            PlayOnce = true;
        }
    }
}
