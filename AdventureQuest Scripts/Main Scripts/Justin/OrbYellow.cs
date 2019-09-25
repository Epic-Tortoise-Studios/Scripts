using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbYellow : MonoBehaviour
{
    Animator anim;
    AudioSource audio1;
    bool PlayOnce;
    GameObject Spheres;
    Animator centerSpheres;
    GameObject Room3Animator;
    Animator Room3;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        audio1 = GetComponent<AudioSource>();
        PlayOnce = false;
        Spheres = GameObject.Find("---------Center Room-----------/CenterPiece/SphereAnimator");
        centerSpheres = Spheres.GetComponent<Animator>();
        Room3Animator = GameObject.Find("-----------Third Room------------/Room3Animations");
        Room3 = Room3Animator.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && PlayOnce == false)
        {
            anim.SetTrigger("RetrievedOrb");
            centerSpheres.SetTrigger("Yellow");
            Room3.SetTrigger("Green");
            audio1.Play();
            PlayOnce = true;
        }
    }
}
