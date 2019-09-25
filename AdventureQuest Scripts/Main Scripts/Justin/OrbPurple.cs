using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPurple : MonoBehaviour
{
    Animator anim;
    AudioSource audio1;
    bool PlayOnce;
    GameObject Spheres;
    Animator centerSpheres;
    GameObject Room2Animator;
    Animator Room2;

    void Start()
    {
        //anim = GetComponentInParent<Animator>();
        audio1 = GetComponent<AudioSource>();
        PlayOnce = false;
        /*Spheres = GameObject.Find("---------Center Room-----------/CenterPiece/SphereAnimator");
        centerSpheres = Spheres.GetComponent<Animator>();
        Room2Animator = GameObject.Find("-----------Second Room------------/Room2Animator");
        Room2 = Room2Animator.GetComponent<Animator>();*/
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && PlayOnce == false)
        {
            //anim.SetTrigger("RetrievedOrb");
            //centerSpheres.SetTrigger("Purple");
            //Room2.SetTrigger("Purple");
            audio1.Play();
            PlayOnce = true;
        }
    }
}
