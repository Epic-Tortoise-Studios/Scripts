﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubChandelierAnim : MonoBehaviour
{

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            anim.enabled = false;
            //anim.SetBool("Player", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            anim.enabled = true;
            //anim.SetBool("Player", false);
        }
    }
}
