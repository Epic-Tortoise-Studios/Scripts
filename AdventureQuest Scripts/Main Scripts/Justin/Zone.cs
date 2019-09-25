using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    Animator anim;
    AudioSource audio1;
    bool PlayOnce;
    GameObject Spheres;
    Animator centerSpheres;
    GameObject GlowAnimator;
    Animator Glow;
    GameObject EndAnimator;
    Animator End;

    void Start()
    {
        anim = GetComponent<Animator>();
        audio1 = GetComponent<AudioSource>();
        PlayOnce = false;
        Spheres = GameObject.Find("---------Center Room-----------/CenterPiece/SphereAnimator");
        centerSpheres = Spheres.GetComponent<Animator>();
        GlowAnimator = GameObject.Find("---------Center Room-----------/CenterPiece/ZoneVFX");
        Glow = GlowAnimator.GetComponent<Animator>();
        EndAnimator = GameObject.Find("-----------End Room-------------/EndRoomAnimator");
        End = EndAnimator.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && PlayOnce == false)
        {
            anim.SetTrigger("RetrievedOrb");
            centerSpheres.SetTrigger("Zone");
            Glow.SetTrigger("Blue");
            End.SetTrigger("Zone");
            audio1.Play();
            PlayOnce = true;
        }
    }
}