using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBlue : MonoBehaviour
{
    Animator anim;
    AudioSource audio1;
    bool PlayOnce;
    GameObject Spheres;
    Animator centerSpheres;
    GameObject GlowAnimator;
    Animator Glow;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        audio1 = GetComponent<AudioSource>();
        PlayOnce = false;
        Spheres = GameObject.Find("---------Center Room-----------/CenterPiece/SphereAnimator");
        centerSpheres = Spheres.GetComponent<Animator>();
        GlowAnimator = GameObject.Find("---------Center Room-----------/CenterPiece/ZoneVFX");
        Glow = GlowAnimator.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && PlayOnce == false)
        {
            anim.SetTrigger("RetrievedOrb");
            centerSpheres.SetTrigger("Blue");
            Glow.SetTrigger("Blue"); 
            audio1.Play();
            PlayOnce = true;
        }
    }
}
