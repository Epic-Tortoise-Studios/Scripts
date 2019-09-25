using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbGreen : MonoBehaviour
{
    Animator anim;
    AudioSource audio1;
    bool PlayOnce;
    GameObject Spheres;
    Animator centerSpheres;
    GameObject Room4Animator;
    Animator Room4;

    void Start()
    {
        //anim = GetComponentInParent<Animator>();
        audio1 = GetComponent<AudioSource>();
        PlayOnce = false;
        /*Spheres = GameObject.Find("---------Center Room-----------/CenterPiece/SphereAnimator");
        centerSpheres = Spheres.GetComponent<Animator>();
        Room4Animator = GameObject.Find("-----------Fourth Room------------/Room4Animator");
        Room4 = Room4Animator.GetComponent<Animator>();*/
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && PlayOnce == false)
        {
            /*anim.SetTrigger("RetrievedOrb");
            centerSpheres.SetTrigger("Green");
            Room4.SetTrigger("Blue");*/
            audio1.Play();
            PlayOnce = true;
        }
    }
}
