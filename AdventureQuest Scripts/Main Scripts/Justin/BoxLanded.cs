﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLanded : MonoBehaviour
{
    public GameObject PlayerBlock;
    public AudioSource Falling;
    public GameObject FallingLocation;
    public Vector3 PlayerBlocklocation;
    public GameObject LandingSource;
    public AudioSource Landing;

    // Start is called before the first frame update
    void Start()
    {
        LandingSource = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxAudio/Bump");
        Landing = LandingSource.GetComponent<AudioSource>();
        PlayerBlock = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxSpawns/PlayerBlocker");
        FallingLocation = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxAudio/Falling");
        Falling = FallingLocation.GetComponent<AudioSource>();
        PlayerBlocklocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boxes")
        {
            Landing.Play();
            Instantiate(PlayerBlock, PlayerBlocklocation, Quaternion.identity);
            Falling.Play();
            Debug.Log("Box Active");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Boxes")
        {
            Falling.Pause();
            //Debug.Log("Box Active");
        }
    }
}