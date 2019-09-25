using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWall1 : MonoBehaviour
{
    Animator anim;
    AudioSource audio1;
    bool PlayOnce;
    GameObject Spheres;
    Animator centerSpheres;
    GameObject A1Animator;
    Animator Area1;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        audio1 = GetComponent<AudioSource>();
        PlayOnce = false;
        Spheres = GameObject.Find("-----------Milestone1Week2Area-----------/OrbAnimator");
        centerSpheres = Spheres.GetComponent<Animator>();
        A1Animator = GameObject.Find("-----------Milestone1Week2Area-----------/Area1/R1LightAnimator");
        Area1 = A1Animator.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && PlayOnce == false)
        {
            anim.SetTrigger("SteppedOn");
            centerSpheres.SetTrigger("YellowMove");
            //Area1.SetTrigger("Yellow");
            audio1.Play();
            PlayOnce = true;
        }
    }
}
